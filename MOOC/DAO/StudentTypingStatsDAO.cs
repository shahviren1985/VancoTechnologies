using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace ITM.Courses.DAO
{
    public class StudentTypingStatsDAO
    {
        Logger logger = new Logger();

        private string Q_AddStudentTypingStats = "insert into studenttypingstats(userId,level,timespaninsecods,accuracy,grossWPM,netWPM,DateCreated) values({0},{1},{2},{3},{4},{5},'{6}')";
        private string Q_GetTypingStatsByLevelUserId = "select * from studenttypingstats where userId={0} and level={1}";
        private string Select_User_performance_by_Course_and_Level = "Select u.Id,u.Course,u.FirstName,u.LastName,u.FatherName,u.MotherName,u.UserName,s.level, s.timespaninsecods, s.accuracy, s.grossWPM, s.netWPM from studenttypingstats s inner join userdetails u on s.userId = u.id where u.Course <> '0' and u.Course <> 'Course' and s.level = {0} and u.Course = '{1}'";
        private string Q_UpdateTypingStatsId = "update studenttypingstats set timespaninsecods = {0}, accuracy={1}, grossWPM={2}, netWPM={3},DateCreated='{5}'  where id={4}";
        private string Q_GetTypingStatsByUserId = "select * from studenttypingstats where userId={0} order by datecreated desc";
        private string Q_StudentNotStartedTyping = "SELECT * FROM userdetails u where usertype <> 'admin' and id not in (select distinct userid from studenttypingstats) and isactive=true and isEnabled = true and isCertified = false and isprint = false order by lastname,firstName";
        private string Q_StudentNotStartedTypingByCourse = "SELECT * FROM userdetails u where usertype <> 'admin' and id not in (select distinct userid from studenttypingstats) and Course = '{0}' and isactive=true and isEnabled = true and isCertified = false and isprint = false order by lastname,firstName";
        private string Q_CourseWiseStudentCountWhoAttemptingTyping = "SELECT Count(*) Count, Course FROM college.userdetails u where id in (SELECT userid FROM college.studenttypingstats) group by course";

        private string Q_GetAllTypingStats = "select s.* from studenttypingstats s inner join userdetails u on s.userid=u.Id where course = '{0}'";

        public void AddStudentTypingStats(int userId, int level, int timeInSeconds, int accuracy, int grossWPM, int netWPM, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddStudentTypingStats, userId, level, timeInSeconds, accuracy, grossWPM, netWPM, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
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

        public StudentTypingStats GetTypingStatsByLevelUserId(int userId, int level, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetTypingStatsByLevelUserId, userId, level);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    StudentTypingStats studentStats = null;
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                studentStats = new StudentTypingStats();
                                studentStats.Id = Convert.ToInt32(reader["Id"]);
                                studentStats.UserId = Convert.ToInt32(reader["userId"]);
                                studentStats.Level = Convert.ToInt32(reader["level"]);
                                studentStats.TimeSpanInSeconds = Convert.ToInt32(reader["timespaninsecods"]);
                                studentStats.Accuracy = Convert.ToInt32(reader["accuracy"]);
                                studentStats.GrossWPM = Convert.ToInt32(reader["grossWPM"]);
                                studentStats.NetWPM = Convert.ToInt32(reader["netWPM"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return studentStats;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentTypingStats> GetTypingStatsByUserId(int userId, bool isLimit, int limitedRow, string cnxnString, string logPath)
        {
            try
            {
                string limit = string.Empty;
                if (isLimit)
                {
                    limit = " limit " + limitedRow.ToString();

                    Q_GetTypingStatsByUserId += limit;
                }

                string cmdText = string.Format(Q_GetTypingStatsByUserId, userId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);
                    List<StudentTypingStats> studentStatsList = new List<StudentTypingStats>();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            StudentTypingStats studentStats = new StudentTypingStats();
                            studentStats.Id = Convert.ToInt32(row["Id"]);
                            studentStats.UserId = Convert.ToInt32(row["userId"]);
                            studentStats.Level = Convert.ToInt32(row["level"]);
                            studentStats.TimeSpanInSeconds = Convert.ToInt32(row["timespaninsecods"]);
                            studentStats.Accuracy = Convert.ToInt32(row["accuracy"]);
                            studentStats.GrossWPM = Convert.ToInt32(row["grossWPM"]);
                            studentStats.NetWPM = Convert.ToInt32(row["netWPM"]);
                            studentStats.DateCreated = new ITM.Courses.Utilities.TimeFormats().GetIndianStandardTime(Convert.ToDateTime(row["DateCreated"].ToString()));

                            studentStatsList.Add(studentStats);
                        }
                    }

                    con.Close();
                    return studentStatsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<StudentTypingStats> GetAllTypingStats(string courseName, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllTypingStats, courseName);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<StudentTypingStats> studentStatsList = new List<StudentTypingStats>();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            StudentTypingStats studentStats = new StudentTypingStats();
                            studentStats.Id = Convert.ToInt32(row["Id"]);
                            studentStats.UserId = Convert.ToInt32(row["userId"]);
                            studentStats.Level = Convert.ToInt32(row["level"]);
                            studentStats.TimeSpanInSeconds = Convert.ToInt32(row["timespaninsecods"]);
                            studentStats.Accuracy = Convert.ToInt32(row["accuracy"]);
                            studentStats.GrossWPM = Convert.ToInt32(row["grossWPM"]);
                            studentStats.NetWPM = Convert.ToInt32(row["netWPM"]);
                            studentStats.DateCreated = new ITM.Courses.Utilities.TimeFormats().GetIndianStandardTime(Convert.ToDateTime(row["DateCreated"].ToString()));

                            studentStatsList.Add(studentStats);
                        }
                    }

                    con.Close();
                    return studentStatsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentTypingStats> GetTypingStatsByCourseAndLevel(int Level, string Course, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Select_User_performance_by_Course_and_Level, Level, Course);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<StudentTypingStats> studentStatsList = new List<StudentTypingStats>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StudentTypingStats studentStats = new StudentTypingStats();
                                studentStats.UserId = Convert.ToInt32(reader["Id"]);
                                studentStats.Level = Convert.ToInt32(reader["level"]);
                                studentStats.TimeSpanInSeconds = Convert.ToInt32(reader["timespaninsecods"]);
                                studentStats.Accuracy = Convert.ToInt32(reader["accuracy"]);
                                studentStats.GrossWPM = Convert.ToInt32(reader["grossWPM"]);
                                studentStats.NetWPM = Convert.ToInt32(reader["netWPM"]);
                                studentStats.FirstName = (reader["FirstName"].ToString());
                                studentStats.LastName = (reader["LastName"].ToString());
                                studentStats.FatherName = (reader["FatherName"].ToString());
                                studentStats.MotherName = (reader["MotherName"].ToString());
                                studentStats.Course = (reader["Course"].ToString());
                                studentStats.UserName = (reader["UserName"].ToString());
                                if (studentStats.TimeSpanInSeconds >= 60)
                                {
                                    string min;
                                    string sec;

                                    min = (studentStats.TimeSpanInSeconds / 60).ToString();
                                    sec = (studentStats.TimeSpanInSeconds % 60).ToString();

                                    studentStats.TimeSpanInNormal = min + " : " + sec;
                                }
                                else
                                {
                                    studentStats.TimeSpanInNormal = "0 : " + studentStats.TimeSpanInSeconds.ToString();
                                }

                                studentStatsList.Add(studentStats);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return studentStatsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateTypingStatsById(int id, int timeSpanInSeconds, int accuracy, int grossWPM, int netWPM, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateTypingStatsId, timeSpanInSeconds, accuracy, grossWPM, netWPM, id, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
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

        #region Get Students Who Havent Started Typing Yet
        public List<UserDetails> GetStudentNotStartedTyping(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_StudentNotStartedTyping);

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.LastLogin = Convert.ToDateTime(reader["LastLogin"].ToString());
                                user.FirstName = ParameterFormater.UnescapeXML(reader["FirstName"].ToString());
                                user.LastName = ParameterFormater.UnescapeXML(reader["LastName"].ToString());
                                user.FatherName = ParameterFormater.UnescapeXML(reader["FatherName"].ToString());
                                user.MotherName = ParameterFormater.UnescapeXML(reader["MotherName"].ToString());
                                user.EmailAddress = reader["EmailAddress"].ToString();
                                user.UserType = reader["userType"].ToString();
                                user.UserName = reader["username"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                user.IsEnable = Convert.ToBoolean(reader["IsEnabled"]);
                                user.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                                user.IsCertified = Convert.ToBoolean(reader["IsCertified"]);
                                user.VersionRegister = reader["VersionRegistered"].ToString();
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.RollNumber = reader["RollNumber"].ToString();
                                user.IsNewUser = Convert.ToBoolean(reader["IsNewUser"]);
                                user.Course = ParameterFormater.UnescapeXML(reader["Course"].ToString());
                                user.SubCourse = ParameterFormater.UnescapeXML(reader["SubCourse"].ToString());
                                user.BatchYear = Convert.ToInt32(reader["BatchYear"]);
                                userlist.Add(user);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDetails> GetStudentNotStartedTypingByCourse(string course, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_StudentNotStartedTypingByCourse, course);

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            
                            while (reader.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.LastLogin = Convert.ToDateTime(reader["LastLogin"].ToString());
                                user.FirstName = ParameterFormater.UnescapeXML(reader["FirstName"].ToString());
                                user.LastName = ParameterFormater.UnescapeXML(reader["LastName"].ToString());
                                user.FatherName = ParameterFormater.UnescapeXML(reader["FatherName"].ToString());
                                user.MotherName = ParameterFormater.UnescapeXML(reader["MotherName"].ToString());
                                user.EmailAddress = reader["EmailAddress"].ToString();
                                user.UserType = reader["userType"].ToString();
                                user.UserName = reader["username"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                user.IsEnable = Convert.ToBoolean(reader["IsEnabled"]);
                                user.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                                user.IsCertified = Convert.ToBoolean(reader["IsCertified"]);
                                user.VersionRegister = reader["VersionRegistered"].ToString();
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.RollNumber = reader["RollNumber"].ToString();
                                user.IsNewUser = Convert.ToBoolean(reader["IsNewUser"]);
                                user.Course = ParameterFormater.UnescapeXML(reader["Course"].ToString());
                                user.SubCourse = ParameterFormater.UnescapeXML(reader["SubCourse"].ToString());
                                user.BatchYear = Convert.ToInt32(reader["BatchYear"]);
                                userlist.Add(user);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<UserDetails> GetCourseWiseStudentCountWhoAttemptingTyping(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_CourseWiseStudentCountWhoAttemptingTyping);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> users = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.Id = Convert.ToInt32(reader["Count"]);
                                user.Course = reader["Course"].ToString();

                                users.Add(user);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public int GetUsersLastTypingLevel(int userId, string cnxnString, string logPath)
        {
            int lastTypingLevel = 1;

            try
            {
                List<StudentTypingStats> typingStats = GetTypingStatsByUserId(userId, false, 0, cnxnString, logPath);

                if (typingStats != null && typingStats.Count > 0)
                {
                    lastTypingLevel = typingStats[0].Level;
                    lastTypingLevel = lastTypingLevel <= 22 ? lastTypingLevel + 1 : lastTypingLevel;
                }

                int _maxTypingLevel = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TypingLevel"]) ? 23 : Convert.ToInt32(ConfigurationManager.AppSettings["TypingLevel"]);

                if (lastTypingLevel == _maxTypingLevel)
                    lastTypingLevel = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lastTypingLevel;
        }
    }
}
