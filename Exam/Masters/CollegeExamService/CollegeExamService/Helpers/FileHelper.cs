using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CollegeExamService.Helpers
{
    public static class FileHelper
    {
        public static String GetFile(string fileName = null, string basePath = null, String extension = ".csv")
        {
            String TempFilePath = String.Empty;
            String FullPath = String.Empty;
            String ArchivePath = String.Empty;
            string FileName = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
                FileName = fileName + extension;
            else
                FileName = "Document_" + Guid.NewGuid().ToString() + extension;
            if (!string.IsNullOrEmpty(basePath))
            {
                TempFilePath = basePath + "\\";
            }

            if (String.IsNullOrEmpty(TempFilePath))
            {
                TempFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~") + Guid.NewGuid().ToString() + "\\";
            }
            //Files
            try
            {
                FullPath = TempFilePath + FileName;
                if (!Directory.Exists(TempFilePath))
                {
                    Directory.CreateDirectory(TempFilePath);
                }
                else
                {
                    if (File.Exists(TempFilePath + FileName))
                    {
                        ArchivePath = TempFilePath + fileName + "_" + DateTime.Now.Ticks + extension;
                        System.IO.File.Move(FullPath, ArchivePath);
                    }
                }
                return FullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in filehelper {0} ", ex.Message);
            }

            return null;
        }

        public static void ExportDataTableToCSV(DataTable dt, string fileName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(dt.Columns[i].ColumnName + ',');
                }
                sb.Append(Environment.NewLine);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        sb.Append(dt.Rows[j][k].ToString() + ',');
                    }
                    sb.Append(Environment.NewLine);
                }
                System.IO.File.WriteAllText(fileName, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ExportDataTableToCSV(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + ',');
            }
            sb.Append(Environment.NewLine);

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Rows[j][k].ToString() + ',');
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader csv = new StreamReader(strFilePath);
            string[] headers = csv.ReadLine().Split(',');
            DataTable dtCSV = new DataTable();
            foreach (string header in headers)
            {
                dtCSV.Columns.Add(header);
            }
            while (!csv.EndOfStream)
            {
                string[] rows = csv.ReadLine().Split(',');
                DataRow dr = dtCSV.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dtCSV.Rows.Add(dr);
            }
            csv.Close();
            return dtCSV;
        }

    }
}