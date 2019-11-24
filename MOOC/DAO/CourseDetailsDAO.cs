using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using System.Data.Common;
using ITM.Courses.Utilities;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class CourseDetailsDAO
    {
        Logger logger = new Logger();
        public string Select_All_Courses = "Select * from coursedetails";
        public string Add_Course = "insert into coursedetails(CourseName, DateCreated, StaffId, OrignalCourseName) values('{0}', '{1}', {2},'{0}')";
        public string Update_Course = "update coursedetails set CourseName = '{0}', DateCreated ='{1}', StaffId={2} where id={3}";
        private string Q_GetCourseByCourseId = "Select * from coursedetails where id={0}";
        private string Q_GetCoursesAttenedNotAttenedByUser = "select * from coursedetails where id {0} (select distinct courseid from chapterdetails where id in (select chapterid from userchapterstatus where userid={1}))";
        private string Q_DeleteCourseById = "delete from coursedetails where id={0}";
        private string Q_GetAllCoursesByStaff = "Select * from coursedetails where StaffId={0}";
        private string Q_GetCourseByCourseName = "Select * from coursedetails where CourseName='{0}'";

        #region Select Function
        public List<CourseDetail> GetAllCoursesByStaff(int staffId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("CourseDetailsDAO", "GetAllCoursesDetails", "Selecting Course Details list from database", logPath);
                string result = string.Format(Q_GetAllCoursesByStaff, staffId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseDetail> courselist = new List<CourseDetail>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseDetail course = new CourseDetail();
                                course.Id = Convert.ToInt32(reader["Id"]);
                                course.CourseName = ParameterFormater.UnescapeXML(reader["CourseName"].ToString());
                                course.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                course.StaffId = Convert.ToInt32(reader["StaffId"]);
                                // Add properties for migration
                                course.MigratedCourseId = Convert.ToInt32(reader["MigratedCourseId"]);
                                course.OrignalCourseName = ParameterFormater.UnescapeXML(reader["OrignalCourseName"].ToString());
                                course.IsPublic = Convert.ToBoolean(reader["IsCoursePublic"]);
                                courselist.Add(course);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courselist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "GetAllCoursesDetails", " Error occurred while Getting Courses list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<CourseDetail> GetAllCoursesDetails(string cnxnString, string logPath)
        {
            try
            {

                string result = Select_All_Courses;
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(result, logPath, con);
                    List<CourseDetail> courselist = new List<CourseDetail>();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            CourseDetail course = new CourseDetail();
                            course.Id = Convert.ToInt32(row["Id"]);
                            course.CourseName = ParameterFormater.UnescapeXML(row["CourseName"].ToString());
                            course.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            course.StaffId = Convert.ToInt32(row["StaffId"]);
                            // Add properties for migration
                            course.MigratedCourseId = Convert.ToInt32(row["MigratedCourseId"]);
                            course.OrignalCourseName = ParameterFormater.UnescapeXML(row["OrignalCourseName"].ToString());
                            course.IsPublic = Convert.ToBoolean(row["IsCoursePublic"]);
                            courselist.Add(course);
                        }
                    }

                    con.Close();
                    return courselist;
                }

            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "GetAllCoursesDetails", " Error occurred while Getting Courses list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<CourseDetail> GetCoursesAttenedNotAttenedByUser(int userid, bool isRunningCourse, string cnxnString, string logPath)
        {
            try
            {
                string sOperator = isRunningCourse ? "in" : "not in";
                string sCmdText = string.Format(Q_GetCoursesAttenedNotAttenedByUser, sOperator, userid);

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseDetail> courselist = new List<CourseDetail>();
                    using (DbDataReader reader = db.Select(sCmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseDetail course = new CourseDetail();
                                course.Id = Convert.ToInt32(reader["Id"]);
                                course.CourseName = ParameterFormater.UnescapeXML(reader["CourseName"].ToString());
                                course.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                course.StaffId = Convert.ToInt32(reader["StaffId"]);
                                // Add properties for migration
                                course.MigratedCourseId = Convert.ToInt32(reader["MigratedCourseId"]);
                                course.OrignalCourseName = ParameterFormater.UnescapeXML(reader["OrignalCourseName"].ToString());
                                course.IsPublic = Convert.ToBoolean(reader["IsCoursePublic"]);
                                courselist.Add(course);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courselist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CourseDetail GetCourseByCourseId(int courseId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("CourseDetailsDAO", "GetAllCoursesDetails", "Selecting Course Details list from database", logPath);
                string result = string.Format(Q_GetCourseByCourseId, courseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(result, logPath, con);
                    CourseDetail course = new CourseDetail();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            course.Id = Convert.ToInt32(row["Id"]);
                            course.CourseName = ParameterFormater.UnescapeXML(row["CourseName"].ToString());
                            course.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            course.StaffId = Convert.ToInt32(row["StaffId"]);
                            // Add properties for migration
                            course.MigratedCourseId = Convert.ToInt32(row["MigratedCourseId"]);
                            course.OrignalCourseName = ParameterFormater.UnescapeXML(row["OrignalCourseName"].ToString());
                            course.IsPublic = Convert.ToBoolean(row["IsCoursePublic"]);
                        }
                    }

                    con.Close();
                    return course;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "GetAllCoursesDetails", " Error occurred while Getting Courses list", ex, logPath);
                throw ex;
            }
        }

        public CourseDetail GetCourseByCourseName(string courseName, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("CourseDetailsDAO", "GetAllCoursesDetails", "Selecting Course Details list from database", logPath);
                string result = string.Format(Q_GetCourseByCourseName, ParameterFormater.FormatParameter(courseName));
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    CourseDetail course = new CourseDetail();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                course.Id = Convert.ToInt32(reader["Id"]);
                                course.CourseName = ParameterFormater.UnescapeXML(reader["CourseName"].ToString());
                                course.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                course.StaffId = Convert.ToInt32(reader["StaffId"]);
                                // Add properties for migration
                                course.MigratedCourseId = Convert.ToInt32(reader["MigratedCourseId"]);
                                course.OrignalCourseName = ParameterFormater.UnescapeXML(reader["OrignalCourseName"].ToString());
                                course.IsPublic = Convert.ToBoolean(reader["IsCoursePublic"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return course;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "GetAllCoursesDetails", " Error occurred while Getting Courses list", ex, logPath);
                throw ex;
            }
        }

        #endregion

        #region Insert Function
        public int AddCourse(string courseName, DateTime dateCreated, int staffId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Add_Course, ParameterFormater.FormatParameter(courseName), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), staffId);
                    Database db = new Database();
                    int lastInsetedId = db.Insert(cmdText, logPath, con);
                    con.Close();
                    return lastInsetedId;
                }


            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "AddCourse", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateCourse(int id, string courseName, DateTime dateCreated, int staffId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_Course, ParameterFormater.FormatParameter(courseName), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), staffId, id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "UpdateCourse", "Error Occured while updating Chapter Details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Delete function
        public void DeleteCourse(int courseId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_DeleteCourseById, courseId);
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

        #region Migrate Course Functionality
        #region DB Queries
        private string Q_AddMigCourse = "insert into coursedetails(CourseName, DateCreated, StaffId, OrignalCourseName, MigratedCourseId) values('{0}', '{1}', {2},'{3}',{4})";
        private string Q_GetMigCourseByMigratedCourseId = "Select * from coursedetails where MigratedCourseId={0}";
        #endregion
        #region Select
        public CourseDetail GetMigCourseByMigratedCourseId(int migratedCourseId, string cnxnString, string logPath)
        {
            try
            {

                string result = string.Format(Q_GetMigCourseByMigratedCourseId, migratedCourseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    CourseDetail course = new CourseDetail();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                course.Id = Convert.ToInt32(reader["Id"]);
                                course.CourseName = ParameterFormater.UnescapeXML(reader["CourseName"].ToString());
                                course.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                course.StaffId = Convert.ToInt32(reader["StaffId"]);

                                course.MigratedCourseId = Convert.ToInt32(reader["MigratedCourseId"]);
                                course.OrignalCourseName = ParameterFormater.UnescapeXML(reader["OrignalCourseName"].ToString());
                                course.IsPublic = Convert.ToBoolean(reader["IsCoursePublic"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();

                            }
                        }
                    }

                    con.Close();
                    return course;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "GetAllCoursesDetails", " Error occurred while Getting Courses list", ex, logPath);
                throw new Exception("11377");
            }
        }
        #endregion
        #region Insert
        public int AddMigCourse(string courseName, DateTime dateCreated, int staffId, string orignalCourseName, int migratedCourseId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddMigCourse, ParameterFormater.FormatParameter(courseName), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), staffId, ParameterFormater.FormatParameter(orignalCourseName), migratedCourseId);
                    Database db = new Database();
                    int lastInsetedId = db.Insert(cmdText, logPath, con);
                    con.Close();
                    return lastInsetedId;
                }

            }
            catch (Exception ex)
            {
                logger.Error("CourseDetailsDAO", "AddCourse", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update
        #endregion
        #endregion
    }
}
