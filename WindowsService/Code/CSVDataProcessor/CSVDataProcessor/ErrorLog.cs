using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVDataProcessor
{
    class EventLogs
    {
        Service1 abc = new Service1();
        public static string sLogFormat;
        public static string LogFIleName;
        public static string sYear;
        public static string sMonth;
        public static string sDay;
      

        public static void LogFileCreate()
        {
            sYear = DateTime.Now.Year.ToString();
            sMonth = DateTime.Now.Month.ToString();
            sDay = DateTime.Now.Day.ToString();
            LogFIleName = "Event_Log" + sYear + sMonth + sDay + ".txt";
        }
        public static void ErrorWrite(string sErrMsg)
        {
            LogFileCreate();
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sPathName = ConfigurationManager.AppSettings["Logging"];
            string Filename = Path.Combine(sPathName, LogFIleName);
            if (!Directory.Exists(sPathName))
            {
                Directory.CreateDirectory(sPathName);
            }
            if (!File.Exists(Filename))
            {
                FileStream CreatFile = File.Create(Filename);
                CreatFile.Close();
            }
            StreamWriter sw = new StreamWriter(Filename, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
        public static void MoveToFailed(string FileName)
        {
            Service1 abc = new Service1();
            String ToF = abc.FailedPath + "\\" + FileName;
            if (!Directory.Exists(abc.FailedPath))
            {
                Directory.CreateDirectory(abc.FailedPath);
                ErrorWrite("Failed directory created");
            }
            if (File.Exists(ToF))
            {
                File.Delete(ToF);
                File.Move(abc.SourcePath + "\\" + FileName, ToF);
                ErrorWrite("File moved to Failed folder: overwrite with existing file: " + FileName);
            }
            else
            {
                File.Move(abc.SourcePath + "\\" + FileName, ToF);
                ErrorWrite("File moved to Failed folder: " + FileName);
            }
        }
        public static void MoveToData(string FileName)
        {

            Service1 abc = new Service1();
            if (!Directory.Exists(abc.DataPath))
            {
                Directory.CreateDirectory(abc.DataPath);
                ErrorWrite("Processed directory created");
            }
            string destFileName = Path.GetFileName(FileName);
            String To = Path.Combine(abc.DataPath, destFileName);
            if (File.Exists(To))
            {
                File.Delete(To);
                File.Move(Path.Combine(abc.ProcessingPath, destFileName), To);
                ErrorWrite("File moved to Data Folder: overwrite with existing file: " + destFileName);
            }
            else
            {
                File.Move(Path.Combine(abc.ProcessingPath, destFileName), To);
                ErrorWrite("file moved to Data Folder: " + destFileName);
            }
        }

        public static void MoveToProcessed(string FileName)
        {

            Service1 abc = new Service1();
            if (!Directory.Exists(abc.ProcessedPath))
            {
                Directory.CreateDirectory(abc.ProcessedPath);
                ErrorWrite("Processed directory created");
            }
            string destFileName = Path.GetFileName(FileName);
            String To = Path.Combine(abc.ProcessedPath, destFileName);
            if (File.Exists(To))
            {
                File.Delete(To);
                File.Move(Path.Combine(abc.SourcePath, destFileName), To);
                ErrorWrite("File moved to Processed Folder: overwrite with existing file: " + destFileName);
            }
            else
            {
                File.Move(Path.Combine(abc.SourcePath, destFileName), To);
                ErrorWrite("file moved to Processed Folder: " + destFileName);
            }
        }
        public string MoveToProcessing(string FileName)
        {

            Service1 abc = new Service1();
            string Processingfolder = abc.ProcessingPath;
            if (!Directory.Exists(Processingfolder))
            {
                Directory.CreateDirectory(Processingfolder);
            }
            string FilenameP = Path.Combine(Processingfolder, FileName);
            if (File.Exists(FilenameP))
            {
                File.Delete(FilenameP);
            }
            FileStream CreatFile = File.Create(FilenameP);
            CreatFile.Close();
            return FilenameP;
        }
        
      


       

    }
}
