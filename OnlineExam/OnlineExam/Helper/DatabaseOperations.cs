using OnlineExam.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace OnlineExam.Helpers
{
    public class DatabaseOperations
    {

        OdbcConnection obcon;
        OdbcDataAdapter odbAdp;
        OdbcCommand odbCommand;
        string getIdQuery = "Select @@Identity";
        int ID;

        public OdbcConnection CreateConnection()
        {
            string connection = ConfigurationManager.AppSettings.Get("mySqlConn").ToString();
            return new OdbcConnection(connection);
        }

        public int ExcuteQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    odbCommand.ExecuteNonQuery();
                    odbCommand.CommandText = getIdQuery;
                    ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                    return ID;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public int ExcuteUpdateQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    odbCommand.ExecuteNonQuery();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public DataSet GetQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    DataSet dsItems = new DataSet();
                    odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                    odbAdp.Fill(dsItems);
                    return dsItems;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        private string RemoveCharacters(string original)
        {
            original = string.IsNullOrEmpty(original) ? "" : original;
            return original.Replace("'", "").Replace("=", "");
        }

        public ExamValidation IsValidExam(string examId)
        {
            DataSet dsItems = new DataSet();
            examId = RemoveCharacters(examId);
            string query = string.Format("SELECT s.id,s.examid,starttime,status,sum(elaspedtime) TimeSpent FROM studentdetails s left join examtiming e on s.examid=e.examid where s.examid = '{0}' group by e.examid", examId);
            dsItems = GetQuery(query);
            ExamValidation exam = new ExamValidation();

            if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
            {
                exam.IsExamValid = (dsItems.Tables[0].Rows.Count > 0);
                var startTime = dsItems.Tables[0].Rows[0]["starttime"];
                bool isExpired = false;

                if (!string.IsNullOrEmpty(startTime.ToString()))
                {
                    var dateDiff = DateTime.Now - DateTime.Parse(startTime.ToString());
                    isExpired = (dateDiff.TotalMinutes >= 120);
                }
                else
                {
                    startTime = new DateTime();
                }

                //bool isExamExpired = (dsItems.Tables[0].Rows[0]["starttime"] == "") ? false : (DateTime.UtcNow - Convert.ToDateTime(dsItems.Tables[0].Rows[0]["starttime"]));
                exam.IsExamExpired = (Convert.ToString(dsItems.Tables[0].Rows[0]["status"]) == "Expired") || isExpired;
                exam.TimeRemaining = (DateTime.Now - DateTime.Parse(startTime.ToString())).TotalSeconds;
                //Int32.Parse((dsItems.Tables[0].Rows[0]["TimeSpent"].ToString() == "") ? "0" : dsItems.Tables[0].Rows[0]["TimeSpent"].ToString());
                exam.StartTime = string.IsNullOrEmpty(startTime.ToString()) ? new DateTime() : DateTime.Parse(startTime.ToString());
                exam.IsFinished = Convert.ToString(dsItems.Tables[0].Rows[0]["status"]) == "Finished" ? true : false;
                string queryStatus = "SELECT count(*) CorrectResponse FROM userresponse u where examid='" + examId + "' and correctresponse=userresponse group by examid";
                dsItems = GetQuery(queryStatus);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    exam.IsExamValid = (dsItems.Tables[0].Rows.Count > 0);
                    exam.Result = int.Parse(dsItems.Tables[0].Rows[0]["CorrectResponse"].ToString());
                }
            }

            return exam;
        }

        public bool FinishExam(string examId)
        {
            string updateUserQuery = "UPDATE studentdetails SET status='Finished' WHERE examid = '" + RemoveCharacters(examId) + "'";
            ExcuteUpdateQuery(updateUserQuery);
            return true;
        }

        public bool StartExam(string examId)
        {
            string updateUserQuery = "UPDATE studentdetails SET starttime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', status = 'Started' WHERE examid = '" + RemoveCharacters(examId) + "' and status='Not Started';";
            ExcuteUpdateQuery(updateUserQuery);
            return true;
        }

        public bool UpdateExamTime(string examId)
        {
            string updateTime = "INSERT INTO examtiming (examid,createdon,elaspedtime) VALUES ('" + RemoveCharacters(examId) + "','" + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss") + "',60);";
            ExcuteQuery(updateTime);
            return true;
        }

        public bool SaveAnswer(Answer answer)
        {
            string deleteQuery = "DELETE FROM userresponse WHERE questionid='" + RemoveCharacters(answer.qid) + "' AND examid='" + RemoveCharacters(answer.examid) + "'";
            ExcuteQuery(deleteQuery);

            string query = "INSERT INTO userresponse (questionid,userresponse,correctresponse,status,createdon,examid,category) VALUES ('" + RemoveCharacters(answer.qid) + "','" + RemoveCharacters(answer.answer) + "','" + RemoveCharacters(answer.correctAnswer) + "','Submitted','" + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss") + "','" + RemoveCharacters(answer.examid) + "','" + answer.category + "');";
            ExcuteQuery(query);
            return true;
        }

        public bool CreateUser(StudentDetails student)
        {
            string query = "INSERT INTO studentdetails (firstname,lastname,membership,status,examid,email,mobile,collegecode) VALUES ('" + RemoveCharacters(student.FirstName) + "','" + RemoveCharacters(student.LastName) + "','" + RemoveCharacters(student.Membership) + "','Not Started','" + student.ExamId + "','" + student.Email + "','" + student.Mobile + "','" + student.CollegeCode + "')";
            ExcuteQuery(query);
            return true;
        }

        public DataSet GetStudents(string subjectCode)
        {
            string query = "select firstname,lastname,membership,s.status,s.examid,email,mobile,certificatepdflink,starttime,count(userresponse) correctanswers from studentdetails s inner join userresponse u on u.examid = s.examid where userresponse=correctresponse and s.collegecode='"+ subjectCode + "' group by u.examid";
            return GetQuery(query);
        }
    }
}