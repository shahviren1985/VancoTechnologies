using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Util;
using System.Configuration;
using System.Web;

namespace ITM.DAO
{
    public class DocumentDetailsDAO
    {
        public string Base_Url = ConfigurationManager.AppSettings["Base_URL"].ToString();
        private string SelectQ = "Select * from  uploadeddocumentdetails";
        private string SelectNewQ = "Select * from  uploadeddocumentdetails where id>{0}";
        private string SelectQuery = "Select * from  uploadeddocumentdetails where userid = '{0}' and id>{1}";
        private string SelectQueryByAdmin = "Select * from uploadeddocumentdetails where `Course` = '{0}' and `Subject` = '{1}' and `Paper` like '{2}%'";
        private string SelectNewQueryByAdmin = "Select * from uploadeddocumentdetails where `Course` = '{0}' and `Subject` = '{1}' and `Paper` = '{2}' and id>{3}";
        private string InsertQuery = "Insert into uploadeddocumentdetails(`Course`, `SubCourse`, `Subject`, `Paper`, `DocumentFirst`, `DocumentSecond`, `IsDeleteRequested`, `IsUsed`, `Date`, `UserId`) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')";
        private string UpdateRequest = "Update uploadeddocumentdetails set `IsDeleteRequested` = '{0}' where id = '{1}' ";
        private string UpdateUsed = "Update uploadeddocumentdetails set `IsUsed` = '{0}' where id = '{1}' ";
        public List<DocumentDetails> SelectPaperListByCourseUserId(string userId, string connString, string logPath)
        {
            try
            {
                string cmdText = string.Format(SelectQuery, userId, ConfigurationManager.AppSettings["MaxId"]);
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                List<DocumentDetails> documentList = new List<DocumentDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentDetails dd = new DocumentDetails();
                        dd.Id = Convert.ToInt32(reader["Id"].ToString());
                        dd.Paper = reader["Paper"].ToString();
                        dd.Course = reader["Course"].ToString();
                        dd.SubCourse = reader["SubCourse"].ToString();
                        dd.Subject = reader["Subject"].ToString();
                        dd.IsDeleteRequested = reader["IsDeleteRequested"].ToString();
                        dd.Date = reader["Date"].ToString();
                        dd.DocumentFirst = Base_Url + reader["DocumentFirst"].ToString();
                        dd.DocumentSecond = Base_Url + reader["DocumentSecond"].ToString();



                        if (dd.IsDeleteRequested != "1" || dd.IsDeleteRequested != "2")
                        {
                            documentList.Add(dd);
                        }

                    }
                }

                if (reader.IsClosed)
                {
                    reader.Close();
                }

                return documentList;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }


        //this is for admin


        public List<DocumentDetails> SelectPaperListByAdmin(string connString, string logPath, bool isNewQuery = false)
        {
            try
            {
                SelectNewQ = string.Format(SelectNewQ, ConfigurationManager.AppSettings["MaxId"]);
                string cmdText = isNewQuery ? SelectNewQ : SelectQ;
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                List<DocumentDetails> documentList = new List<DocumentDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentDetails dd = new DocumentDetails();
                        dd.Id = Convert.ToInt32(reader["Id"].ToString());
                        dd.Paper = reader["Paper"].ToString();
                        dd.Course = reader["Course"].ToString();
                        dd.SubCourse = reader["SubCourse"].ToString();
                        dd.Subject = reader["Subject"].ToString();
                        dd.IsDeleteRequested = reader["IsDeleteRequested"].ToString();
                        dd.Date = reader["Date"].ToString();
                        dd.DocumentFirst = Base_Url + reader["DocumentFirst"].ToString();
                        dd.DocumentSecond = Base_Url + reader["DocumentSecond"].ToString();
                        dd.IsUsed = reader["IsUsed"].ToString();
                        if (dd.IsUsed == "1")
                        {
                            dd.UsedStatus = "Used";
                        }
                        else if (dd.IsUsed == "0")
                        {
                            dd.UsedStatus = "Not-Used";
                        }

                        if (dd.IsDeleteRequested != "2") { documentList.Add(dd); }

                    }
                }

                if (reader.IsClosed)
                {
                    reader.Close();
                }

                return documentList;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public List<DocumentDetails> SelectDeleteRequestPaperListByAdmin(string connString, string logPath, bool isNewQuery = false)
        {
            try
            {
                SelectNewQ = string.Format("Select * from  uploadeddocumentdetails where id>{0} and isdeleterequested>0", ConfigurationManager.AppSettings["MaxId"]);
                string cmdText = SelectNewQ;
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                List<DocumentDetails> documentList = new List<DocumentDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentDetails dd = new DocumentDetails();
                        dd.Id = Convert.ToInt32(reader["Id"].ToString());
                        dd.Paper = reader["Paper"].ToString();
                        dd.Course = reader["Course"].ToString();
                        dd.SubCourse = reader["SubCourse"].ToString();
                        dd.Subject = reader["Subject"].ToString();
                        dd.IsDeleteRequested = reader["IsDeleteRequested"].ToString();
                        dd.Date = reader["Date"].ToString();
                        dd.DocumentFirst = Base_Url + reader["DocumentFirst"].ToString();
                        dd.DocumentSecond = Base_Url + reader["DocumentSecond"].ToString();
                        dd.IsUsed = reader["IsUsed"].ToString();
                        if (dd.IsUsed == "1")
                        {
                            dd.UsedStatus = "Used";
                        }
                        else if (dd.IsUsed == "0")
                        {
                            dd.UsedStatus = "Not-Used";
                        }

                        if (dd.IsDeleteRequested != "2") { documentList.Add(dd); }

                    }
                }

                if (reader.IsClosed)
                {
                    reader.Close();
                }

                return documentList;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }


        public List<DocumentDetails> SelectPaperList(string course, string subject, string paper, string connString, string logPath)
        {
            try
            {
                if (paper.IndexOf("&") > 0)
                    paper = paper.Substring(0, paper.IndexOf("&"));

                string cmdText = string.Format(SelectQueryByAdmin, course, subject, paper);
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                List<DocumentDetails> documentList = new List<DocumentDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string date = reader["Date"].ToString();

                        try
                        {
                            date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy");
                        }
                        catch (Exception)
                        {

                        }
                        DocumentDetails dd = new DocumentDetails();
                        dd.Id = Convert.ToInt32(reader["Id"].ToString());
                        dd.Paper = reader["Paper"].ToString();
                        dd.Course = reader["Course"].ToString();
                        dd.SubCourse = reader["SubCourse"].ToString();
                        dd.Subject = reader["Subject"].ToString();
                        dd.IsDeleteRequested = reader["IsDeleteRequested"].ToString();
                        dd.Date = date;
                        dd.DocumentFirst = Base_Url + reader["DocumentFirst"].ToString();
                        dd.DocumentSecond = Base_Url + reader["DocumentSecond"].ToString();
                        dd.IsUsed = reader["IsUsed"].ToString();
                        if (dd.IsUsed == "1")
                        {
                            dd.UsedStatus = "Used";
                        }
                        else if (dd.IsUsed == "0")
                        {
                            dd.UsedStatus = "Not-Used";
                        }

                        if (dd.IsDeleteRequested != "2") { documentList.Add(dd); }
                    }
                }

                if (reader.IsClosed)
                {
                    reader.Close();
                }

                return documentList;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public List<DocumentDetails> SelectNewPaperList(string course, string subject, string paper, string connString, string logPath)
        {
            try
            {
                string id = ConfigurationManager.AppSettings["MaxId"];
                string cmdText = string.Format(SelectNewQueryByAdmin, course, subject, paper, id);
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                List<DocumentDetails> documentList = new List<DocumentDetails>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string date = reader["Date"].ToString();

                        try
                        {
                            date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy");
                        }
                        catch (Exception)
                        {

                        }
                        DocumentDetails dd = new DocumentDetails();
                        dd.Id = Convert.ToInt32(reader["Id"].ToString());
                        dd.Paper = reader["Paper"].ToString();
                        dd.Course = reader["Course"].ToString();
                        dd.SubCourse = reader["SubCourse"].ToString();
                        dd.Subject = reader["Subject"].ToString();
                        dd.IsDeleteRequested = reader["IsDeleteRequested"].ToString();
                        dd.Date = date;
                        dd.DocumentFirst = Base_Url + reader["DocumentFirst"].ToString();
                        dd.DocumentSecond = Base_Url + reader["DocumentSecond"].ToString();
                        dd.IsUsed = reader["IsUsed"].ToString();
                        if (dd.IsUsed == "1")
                        {
                            dd.UsedStatus = "Used";
                        }
                        else if (dd.IsUsed == "0")
                        {
                            dd.UsedStatus = "Not-Used";
                        }

                        if (dd.IsDeleteRequested != "2") { documentList.Add(dd); }
                    }
                }

                if (reader.IsClosed)
                {
                    reader.Close();
                }

                return documentList;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }


        public void InsertDocumentDetails(DocumentDetails dd, string connString, string logPath)
        {
            try
            {
                if (string.IsNullOrEmpty(dd.SubCourse))
                {
                    dd.SubCourse = string.Empty;
                }

                string cmdText = string.Format(InsertQuery, dd.Course, dd.SubCourse, dd.Subject, dd.Paper, dd.DocumentFirst, dd.DocumentSecond, dd.IsDeleteRequested, dd.IsUsed, dd.Date, dd.UserId);
                Database db = new Database();
                db.Insert(cmdText, connString, logPath);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateRequestDocumentDetails(DocumentDetails dd, string connString, string logPath)
        {
            try
            {
                string cmdText = string.Format(UpdateRequest, dd.IsDeleteRequested, dd.Id);
                Database db = new Database();
                db.Update(cmdText, connString, logPath);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateUsedDocumentDetails(DocumentDetails dd, string connString, string logPath)
        {
            try
            {
                string cmdText = string.Format(UpdateUsed, dd.IsUsed, dd.Id);
                Database db = new Database();
                db.Update(cmdText, connString, logPath);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
