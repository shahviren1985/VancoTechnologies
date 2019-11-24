using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Web.Hosting;
using CollegeExamService.Models;
using System.IO;
using System.Threading;

namespace CollegeExamService.Helpers
{
    public class LiteOperation
    {
        string FilePath = HostingEnvironment.ApplicationPhysicalPath;
        private void CreateNewDatabase()
        {
            SQLiteConnection.CreateFile(FilePath + "ExamData.db");
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + FilePath + "ExamData.db" + ";Version=3;Password=54321;");
            m_dbConnection.Open();
            m_dbConnection.ChangePassword("54321");
            string sql = "create table tbl_TranscriptRequest (ID varchar(50), FirstName varchar(30), LstName varchar(30),PassingYear varchar(10),CollegeRegNo varchar(100),DateOfRecieved varchar(100),Status int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        private string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }

        private string GetUniqueId()
        {
            Thread.Sleep(100);
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public int AddTranscriptRequest(TranscriptModel tp)
        {
            if (!File.Exists(FilePath + "ExamData.db"))
            {
                CreateNewDatabase();
            }
            tp.Id = GetUniqueId();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + FilePath + "ExamData.db" + ";Version=3;Password=54321;");
            string Query = "INSERT INTO tbl_TranscriptRequest (ID,FirstName,LstName,PassingYear,CollegeRegNo,DateOfRecieved,Status) VALUES ('" + tp.Id + "','" + tp.FirstName + "','" + tp.LastName + "','" + tp.PassingYear + "','" + tp.CollegeRegNo + "','" + DateTimeSQLite(DateTime.Now) + "',0)";
            SQLiteCommand command = new SQLiteCommand(Query, m_dbConnection);
            m_dbConnection.Open();
            int RecordAffected = command.ExecuteNonQuery();
            m_dbConnection.Close();
            return RecordAffected;
        }
    }
}