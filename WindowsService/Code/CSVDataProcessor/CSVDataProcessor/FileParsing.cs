using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace CSVDataProcessor
{
    class FileParsing
    {
        EventLogs errlg = new EventLogs();
        Service1 config = new Service1();
        List<string> All_Records = new List<string>();
        int Record_Count;
        
        public void ProcessCSV(string FileName)
        {
            try
            {
                string Single_line;
                System.IO.StreamReader file = new System.IO.StreamReader(FileName);
                Record_Count = File.ReadAllLines(FileName).Where(line => line != "").Count();
                string FileN = Path.GetFileName(FileName);
                string FileS = Path.GetFileNameWithoutExtension(FileName);
                EventLogs.ErrorWrite("Processing start...");
                //EventLogs.ErrorWrite("Total records Found :" + Record_Count.ToString());
                if (Record_Count == 0)
                {
                    EventLogs.ErrorWrite("No data found in the file: " + FileN);
                    file.Close();
                    EventLogs.MoveToFailed(FileN);
                }
                else
                {

                    String[] Name = FileS.Split('_');

                    while ((Single_line = file.ReadLine()) != null)
                    {
                        if (Single_line != "")
                        {
                            All_Records.Add(Single_line);
                        }

                    }
                    file.Close();


                    #region Filter Records
                    int lastProcessedDay = Convert.ToInt16(config.LastProcessedDate.Substring(0, 2)); // Get Last Processed day from app.config

                    Dictionary<int, List<string>> dicDateRecords = new Dictionary<int, List<string>>();

                    foreach (string rec in All_Records)
                    {
                        string temRec = rec;
                        List<string> tokens = rec.Split(",".ToCharArray()).ToList();
                        int day = Convert.ToInt16(tokens[1].Substring(0, 2));

                        if (!dicDateRecords.ContainsKey(day))
                        {
                            List<string> lstTmp = new List<string>();
                            lstTmp.Add(temRec);
                            dicDateRecords.Add(day, lstTmp);

                        }
                        else
                            dicDateRecords[day].Add(temRec);

                    }
                    Dictionary<int, List<string>> dicDateRecords_Updated = new Dictionary<int, List<string>>();

                    int currDay = Convert.ToInt16(System.DateTime.Now.Day);
                    foreach (KeyValuePair<int, List<string>> kvp in dicDateRecords)
                    {
                        int day = kvp.Key;
                        if (kvp.Key > lastProcessedDay && kvp.Key < currDay)
                            dicDateRecords_Updated.Add(kvp.Key, kvp.Value);

                    }
                    
                    #endregion
                    
                                       
                    //All_Records = All_Records.FindAll(x =>  x.Split(",".ToCharArray())[1].Contains(config.LastProcessedDate));



                    #region Update CSV
                    FileS = string.Format("{0}_{1}.csv", FileS, DateTime.Now.ToString("ddMMyyyyhhmmsss"));
                    string ProccessedCSV = errlg.MoveToProcessing(FileS);
                    StreamWriter csvP = new StreamWriter(ProccessedCSV);

                    StringBuilder csvContents = new StringBuilder();

                    foreach (KeyValuePair<int, List<string>> kvp in dicDateRecords_Updated)
                    {

                        List<string> rows = kvp.Value;

                        foreach (string row in rows)
                        {
                            csvP.WriteLine(row);
                            csvContents.Append(row);
                        }
                    }
                    csvP.Flush();
                    csvP.Close();
                    dicDateRecords_Updated.Clear();
                    dicDateRecords.Clear(); 
                    #endregion



                    EventLogs.MoveToData(FileS);
                    EventLogs.MoveToProcessed(FileName);

                    #region Post CSV
                    string processedDate = string.Format("{0}/{1}/{2}", System.DateTime.Now.Day, System.DateTime.Now.Month, System.DateTime.Now.Year);
                    if (PostCSV(config.PostFileAPI, "csv=" + csvContents.ToString()))
                    {
                        SaveLastProcessedDateAppConfig(processedDate);
                    } 
                    #endregion

                }


                EventLogs.ErrorWrite("Processing complete...");

                //Service1.StopService("CSVDataProcessor", 2000);
                
            }
            catch (Exception ex)
            {
                EventLogs.ErrorWrite(ex.ToString());
            }
        }

        public static bool PostCSV(string url, object postData)
        {

            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);

                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, postData);

                byte[] data = ms.ToArray();

                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream newStream = httpWReq.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }
                string responseFromServer = string.Empty;
                using (HttpWebResponse webResponse = (HttpWebResponse)httpWReq.GetResponse())
                {
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.Default))
                        {
                            responseFromServer = reader.ReadToEnd();
                        }
                    }
                }

                if (Contains_IgnoreCase(responseFromServer, "&lt;ERROR&gt;"))
                {
                    EventLogs.ErrorWrite("Some error in Http Response");
                    EventLogs.ErrorWrite(responseFromServer);
                    return false;
                }
                else
                {
                    EventLogs.ErrorWrite("Some error in Http Response");
                    EventLogs.ErrorWrite(responseFromServer);
                    return true;

                    //return Status;
                }
            }
            catch (Exception ex)
            {
                EventLogs.ErrorWrite(ex.ToString());
                return true;
 
            }


            
        }

       

        public static bool Contains_IgnoreCase(string source, string target)
        {
            return source.ToUpper().Contains(target.ToUpper());
        }
        
        private void SaveLastProcessedDateAppConfig(string date)
        {

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("LastProcessedDate");
            config.AppSettings.Settings.Add("LastProcessedDate", date);
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }


        
    }

   
}