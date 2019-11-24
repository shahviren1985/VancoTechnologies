using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class StudentQueriesDAO
    {
        Logger logger = new Logger();
        public string Q_AddStudentQuery = "insert into studentsqueries(UserId,CollegeName,Name,MobileNo,Query,DatePosted) values({0},'{1}','{2}','{3}','{4}','{5}')";
        public string Select_All_Student_Queries = "Select * from studentsqueries";
        public string Select_Connection_String = "Select distinct connectionString from userlogins";
        public string Select_All_Students = "Select sq.*, u.username from studentsqueries sq inner join userdetails u on sq.userid = u.id where queryStatus <> 2"; // 1 = new query 2= closed query
        public string Q_UpdateQueryStatus = "update studentsqueries set queryStatus = {0} where id= {1}";
        public string Q_GetStudentQueryById = "Select sq.*, u.username from studentsqueries sq inner join userdetails u on sq.userid = u.id where sq.id={0}";

        # region Insert functions.
        public void AddStudentQuery(int userId, string collegeName, string name, string mobileNo, string query, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddStudentQuery, userId, collegeName, ParameterFormater.FormatParameter(name), mobileNo,
                    ParameterFormater.FormatParameter(query), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
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
        # endregion

        #region Select Functions
        // Function to select connection string on the basics of user name only.
        public List<string> GetDistinctConnectionStrings(string adminAppcnxnString, string logPath)
        {
            try
            {
                //logger.Debug("StudentQueriesDAO", "GetConnectionString", "Selecting Connection string from database", logPath);
                string result = string.Format(Select_Connection_String);
                Database db = new Database();
                List<string> cnxnStringList = new List<string>();

                using (MySqlConnection con = new MySqlConnection(adminAppcnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnxnStringList.Add(reader["ConnectionString"].ToString());
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return cnxnStringList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Function to select all the student who register their query.
        public List<StudentQueries> GetAllStudent(string cnxnString, string logPath, string adminAppConn)
        {
            try
            {
                List<string> cnxnStringList = new List<string>();
                cnxnStringList = GetDistinctConnectionStrings(adminAppConn, logPath);

                List<StudentQueries> studentquerylist = new List<StudentQueries>();

                foreach (string query in cnxnStringList)
                {
                    if (query != null)
                    {
                        logger.Debug("StudentQueriesDAO", "GetAllStudent", "Selecting Student list from database", logPath);
                        string result = Select_All_Students;
                        Database db = new Database();
                        using (MySqlConnection con = new MySqlConnection(query))
                        {
                            con.Open();
                            try
                            {
                                using (DbDataReader reader = db.Select(result, logPath, con))
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            StudentQueries studentQuery = new StudentQueries();
                                            studentQuery.Id = Convert.ToInt32(reader["Id"]);
                                            studentQuery.UserId = Convert.ToInt32(reader["UserId"]);
                                            studentQuery.CollegeName = reader["CollegeName"].ToString();
                                            studentQuery.Name = ParameterFormater.UnescapeXML(reader["Name"].ToString());
                                            studentQuery.MobileNo = reader["MobileNo"].ToString();
                                            studentQuery.Query = ParameterFormater.UnescapeXML(reader["Query"].ToString());
                                            studentQuery.DatePosted = Convert.ToDateTime(reader["DatePosted"].ToString());
                                            studentQuery.DatePosted = new TimeFormats().GetIndianStandardTime(studentQuery.DatePosted);
                                            studentQuery.QueryStatus = Convert.ToInt32(reader["QueryStatus"]);
                                            studentQuery.UserName = reader["username"].ToString();
                                            studentquerylist.Add(studentQuery);
                                        }
                                        if (!reader.IsClosed)
                                        {
                                            reader.Close();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error("StudentQueriesDAO", "GetAllStudentQueries", " returning student details list from different databases.", ex, logPath);
                            }

                            con.Close();
                        }
                    }
                }

                return studentquerylist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StudentQueries GetStudentQueryById(int id, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentQueryById, id);
                Database db = new Database();
                StudentQueries studentQuery = new StudentQueries();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                studentQuery.Id = Convert.ToInt32(reader["Id"]);
                                studentQuery.UserId = Convert.ToInt32(reader["UserId"]);
                                studentQuery.CollegeName = reader["CollegeName"].ToString();
                                studentQuery.Name = ParameterFormater.UnescapeXML(reader["Name"].ToString());
                                studentQuery.MobileNo = reader["MobileNo"].ToString();
                                //studentQuery.Query = ParameterFormater.UnescapeXML(reader["Query"].ToString());
                                string query = ParameterFormater.UnescapeXML(reader["Query"].ToString());
                                studentQuery.DatePosted = Convert.ToDateTime(reader["DatePosted"].ToString());
                                studentQuery.DatePosted = new TimeFormats().GetIndianStandardTime(studentQuery.DatePosted);
                                studentQuery.QueryStatus = Convert.ToInt32(reader["QueryStatus"]);
                                studentQuery.UserName = reader["username"].ToString();
                                if (query.LastIndexOf("||") > 0)
                                {
                                    studentQuery.Query = query.Substring(0, query.LastIndexOf("||"));
                                    string emailContener = query.Substring(query.LastIndexOf("||"));
                                    string[] emails = emailContener.Split(new char[] { ':' });

                                    if (emails.Length > 1)
                                    {
                                        studentQuery.Email = emails[1];
                                    }
                                }
                                else
                                {
                                    studentQuery.Query = query;
                                }
                                //studentQuery.DisplayDate = studentQuery.DatePosted.ToString("MMMM dd, yyyy at hh:mm tt");
                                studentQuery.DisplayDate = studentQuery.DatePosted.ToString("MMMM dd, yyyy") + " at " + studentQuery.DatePosted.ToString("hh:mm tt");
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return studentQuery;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        #endregion

        #region Update Query Status
        public void UpdateQueryStatus(int id, int queryStatus, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateQueryStatus, queryStatus, id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
