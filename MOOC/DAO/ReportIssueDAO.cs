using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using ITM.Courses.LogManager;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class ReportIssueDAO
    {
        Logger logger = new Logger();

        private string Q_AddReportIssue = "insert into reportissue(UserId,Title,description,email,Expectedcontent,DatePosted,chapterId,sectionId) values({0},'{1}','{2}','{3}','{4}','{5}',{6},{7})";
        private string Q_GetAllReportIssues = "Select ri.*, u.username, ch.ChapterName, sc.SectionName from reportissue ri inner join userdetails u on ri.userid = u.id inner join chapterdetails ch on ri.chapterid=ch.id inner join chaptersection sc on ri.sectionid= sc.id"; 

        public void AddReportIssue(int userId, string title, string description, string email, string expectedContent, int chapterId, int sectionId,
            string cnxnString, string logPath)
        {
            try
            {
                title = ParameterFormater.FormatParameter(ParameterFormater.GetSpecialCharsQS(title));
                description = ParameterFormater.FormatParameter(ParameterFormater.GetSpecialCharsQS(description));
                expectedContent = ParameterFormater.FormatParameter(ParameterFormater.GetSpecialCharsQS(expectedContent));

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddReportIssue, userId, title, description, email, expectedContent, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"), chapterId, sectionId);
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReportIssue> GetAllReportIssues(string cnxnstring, string logPath, string adimAppCnxnString)
        {
            try
            {
                List<string> cnxnStringList = new List<string>();
                cnxnStringList = new StudentQueriesDAO().GetDistinctConnectionStrings(adimAppCnxnString, logPath);

                List<ReportIssue> studentquerylist = new List<ReportIssue>();

                foreach (string query in cnxnStringList)
                {
                    if (query != null)
                    {
                        Database db = new Database();
                        using (MySqlConnection con = new MySqlConnection(query))
                        {
                            con.Open();
                            using (DbDataReader reader = db.Select(Q_GetAllReportIssues, logPath, con))
                            {

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReportIssue studentQuery = new ReportIssue();
                                        studentQuery.Id = Convert.ToInt32(reader["Id"]);
                                        studentQuery.UserId = Convert.ToInt32(reader["UserId"]);
                                        studentQuery.UserName = reader["username"].ToString();
                                        studentQuery.ChapterName = ParameterFormater.UnescapeXML(reader["ChapterName"].ToString());
                                        studentQuery.SectionName = reader["SectionName"].ToString();
                                        studentQuery.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                        studentQuery.Description = ParameterFormater.UnescapeXML(reader["Description"].ToString());
                                        studentQuery.ExpectedContent = ParameterFormater.UnescapeXML(reader["ExpectedContent"].ToString());
                                        studentQuery.DatePosted = Convert.ToDateTime(reader["DatePosted"].ToString());
                                        studentQuery.Email = reader["Email"].ToString();

                                        studentquerylist.Add(studentQuery);
                                    }
                                    if (!reader.IsClosed)
                                    {
                                        reader.Close();
                                    }
                                }
                            }

                            con.Close();
                        }
                    }
                }

                return studentquerylist;
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
    }
}
