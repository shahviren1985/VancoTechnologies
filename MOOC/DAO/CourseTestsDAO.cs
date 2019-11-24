using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class CourseTestsDAO
    {
        #region DB Queries
        private string Q_AddCourseTests = "insert into coursetests(testname,courseId,chapters,IsTimebound,timelimit,totalquestions,teststatus,startdate,enddate,datecreated,lastupdated) values('{0}',{1},'{2}',{3},{4},{5},{6},'{7}','{8}','{9}','{10}')";
        private string Q_GetAllCourseTests = "select * from coursetests";
        private string Q_GetAllCourseTestsByCourseId = "select * from coursetests where courseId={0}";
        private string Q_GetCourseTestsById = "select ct.*, c.CourseName from coursetests ct inner join coursedetails c on ct.courseid=c.id where ct.id={0}";
        private string Q_UpdateCourseTests = "update coursetests set testname='{1}',courseId={2},chapters='{3}',IsTimebound={4},timelimit={5},totalquestions={6},teststatus={7},startdate='{8}',enddate='{9}',lastupdated='{10}' where id={0}";
        private string Q_GetCompletedorRunningTestsByUser = "SELECT * FROM coursetests c where id {0} (select testid from completedtesttracker where userid={1}) and courseid= {2}";
        private string Q_DeleteCourseTest = "delete from coursetests where id={0}";
        private string Q_GetAllTestsByStaffId = "SELECT * FROM coursetests where courseid in (select id from coursedetails where staffid={0})";
        #endregion

        #region Insert functions
        public void AddCourseTests(string testName, int courseId, string chapters, bool isTimeBound, int timeLimit, int totalQuestions, bool testStatus, DateTime startDate, DateTime endDate, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddCourseTests, ParameterFormater.FormatParameter(testName), courseId, chapters, isTimeBound, timeLimit, totalQuestions, testStatus, startDate.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    endDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
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
        #endregion

        #region Select functions
        public List<CourseTests> GetAllTestsByStaffId(int staffId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllTestsByStaffId, staffId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseTests> courseTests = new List<CourseTests>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseTests courseTest = new CourseTests();
                                courseTest.Id = Convert.ToInt32(reader["Id"]);
                                courseTest.TestName = ParameterFormater.UnescapeXML(reader["testname"].ToString());
                                courseTest.CourseId = Convert.ToInt32(reader["courseId"]);
                                courseTest.Chapters = reader["chapters"].ToString();
                                courseTest.IsTimebound = Convert.ToBoolean(reader["IsTimebound"]);
                                courseTest.TimeLimit = Convert.ToInt32(reader["timelimit"]);
                                courseTest.TotalQuestions = Convert.ToInt32(reader["totalquestions"]);
                                courseTest.IsTestActive = Convert.ToBoolean(reader["teststatus"]);
                                courseTest.StartDate = Convert.ToDateTime(reader["startdate"]);
                                courseTest.EndDate = Convert.ToDateTime(reader["enddate"]);
                                courseTest.DateCreated = Convert.ToDateTime(reader["datecreated"]);
                                courseTest.LastUpdated = Convert.ToDateTime(reader["lastupdated"]);

                                courseTests.Add(courseTest);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseTests;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CourseTests> GetAllCourseTests(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllCourseTests);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseTests> courseTests = new List<CourseTests>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseTests courseTest = new CourseTests();
                                courseTest.Id = Convert.ToInt32(reader["Id"]);
                                courseTest.TestName = ParameterFormater.UnescapeXML(reader["testname"].ToString());
                                courseTest.CourseId = Convert.ToInt32(reader["courseId"]);
                                courseTest.Chapters = reader["chapters"].ToString();
                                courseTest.IsTimebound = Convert.ToBoolean(reader["IsTimebound"]);
                                courseTest.TimeLimit = Convert.ToInt32(reader["timelimit"]);
                                courseTest.TotalQuestions = Convert.ToInt32(reader["totalquestions"]);
                                courseTest.IsTestActive = Convert.ToBoolean(reader["teststatus"]);
                                courseTest.StartDate = Convert.ToDateTime(reader["startdate"]);
                                courseTest.EndDate = Convert.ToDateTime(reader["enddate"]);
                                courseTest.DateCreated = Convert.ToDateTime(reader["datecreated"]);
                                courseTest.LastUpdated = Convert.ToDateTime(reader["lastupdated"]);

                                courseTests.Add(courseTest);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseTests;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CourseTests> GetAllCourseTestsByCourseId(int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllCourseTestsByCourseId, courseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseTests> courseTests = new List<CourseTests>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseTests courseTest = new CourseTests();
                                courseTest.Id = Convert.ToInt32(reader["Id"]);
                                courseTest.TestName = ParameterFormater.UnescapeXML(reader["testname"].ToString());
                                courseTest.CourseId = Convert.ToInt32(reader["courseId"]);
                                courseTest.Chapters = reader["chapters"].ToString();
                                courseTest.IsTimebound = Convert.ToBoolean(reader["IsTimebound"]);
                                courseTest.TimeLimit = Convert.ToInt32(reader["timelimit"]);
                                courseTest.TotalQuestions = Convert.ToInt32(reader["totalquestions"]);
                                courseTest.IsTestActive = Convert.ToBoolean(reader["teststatus"]);
                                courseTest.StartDate = Convert.ToDateTime(reader["startdate"]);
                                courseTest.EndDate = Convert.ToDateTime(reader["enddate"]);
                                courseTest.DateCreated = Convert.ToDateTime(reader["datecreated"]);
                                courseTest.LastUpdated = Convert.ToDateTime(reader["lastupdated"]);

                                courseTests.Add(courseTest);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseTests;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CourseTests GetCourseTestsById(int id, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetCourseTestsById, id);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    CourseTests courseTest = new CourseTests();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                courseTest.Id = Convert.ToInt32(reader["Id"]);
                                courseTest.TestName = ParameterFormater.UnescapeXML(reader["testname"].ToString());
                                courseTest.CourseId = Convert.ToInt32(reader["courseId"]);
                                courseTest.CourseName = reader["CourseName"].ToString();
                                courseTest.Chapters = reader["chapters"].ToString();
                                courseTest.IsTimebound = Convert.ToBoolean(reader["IsTimebound"]);
                                courseTest.TimeLimit = Convert.ToInt32(reader["timelimit"]);
                                courseTest.TotalQuestions = Convert.ToInt32(reader["totalquestions"]);
                                courseTest.IsTestActive = Convert.ToBoolean(reader["teststatus"]);
                                courseTest.StartDate = Convert.ToDateTime(reader["startdate"]);
                                courseTest.EndDate = Convert.ToDateTime(reader["enddate"]);
                                courseTest.DateCreated = Convert.ToDateTime(reader["datecreated"]);
                                courseTest.LastUpdated = Convert.ToDateTime(reader["lastupdated"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseTest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CourseTests> GetCompletedorRunningTestsByUser(int userId, int courseId, bool isCompleted, string cnxnString, string logPath)
        {
            Database db = new Database();
            try
            {
                string option = "not in";

                if (isCompleted)
                    option = "in";

                string cmdText = string.Format(Q_GetCompletedorRunningTestsByUser, option, userId, courseId);
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);
                    List<CourseTests> courseTests = new List<CourseTests>();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            CourseTests courseTest = new CourseTests();
                            courseTest.Id = Convert.ToInt32(row["Id"]);
                            courseTest.TestName = ParameterFormater.UnescapeXML(row["testname"].ToString());
                            courseTest.CourseId = Convert.ToInt32(row["courseId"]);
                            courseTest.Chapters = row["chapters"].ToString();
                            courseTest.IsTimebound = Convert.ToBoolean(row["IsTimebound"]);
                            courseTest.TimeLimit = Convert.ToInt32(row["timelimit"]);
                            courseTest.TotalQuestions = Convert.ToInt32(row["totalquestions"]);
                            courseTest.IsTestActive = Convert.ToBoolean(row["teststatus"]);
                            courseTest.StartDate = Convert.ToDateTime(row["startdate"]);
                            courseTest.EndDate = Convert.ToDateTime(row["enddate"]);
                            courseTest.DateCreated = Convert.ToDateTime(row["datecreated"]);
                            courseTest.LastUpdated = Convert.ToDateTime(row["lastupdated"]);

                            courseTests.Add(courseTest);
                        }
                    }

                    con.Close();
                    return courseTests;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Update Function
        public void UpdateCourseTests(int id, string testName, int courseId, string chapters, bool isTimeBound, int timeLimit, int totalQuestions, bool testStatus, DateTime startDate, DateTime endDate, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateCourseTests, id, ParameterFormater.FormatParameter(testName), courseId, chapters, isTimeBound, timeLimit, totalQuestions, testStatus, startDate.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    endDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
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

        #region Delete Test
        public void DeleteCourseTest(int courseTestId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_DeleteCourseTest, courseTestId);
                    Database db = new Database();
                    db.Delete(cmdText, logPath, con);
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
