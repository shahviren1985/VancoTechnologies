using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using ITM.Courses.LogManager;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class StudentReportManager
    {
        Logger logger = new Logger();

        private string Q_GetStudentReportWhoCompletedCourse = "SELECT * FROM userdetails u where course = '{0}' and isCompleted = true and isactive=true and isEnabled = true and isCertified = false and isprint = false order by lastname,firstName";
        private string Q_GetStudentProgressReport = "SELECT * FROM userdetails u where id in (SELECT distinct userid FROM userchapterstatus) and course = '{0}' and isactive=true and isEnabled = true and isCertified = false and isprint = false order by lastname,firstName";
        private string Q_GetStudentNotStartedCourse = "SELECT * FROM userdetails u where id not in (SELECT distinct userid FROM userchapterstatus) and course = '{0}' and isactive=true and isEnabled = true and isCertified = false and isprint = false order by lastname,firstName";
        private string Q_GetStudentTypingStatus = "SELECT * FROM userdetails where id in (SELECT distinct userid FROM studenttypingstats) and Course = '{0}' and isactive=true and isEnabled = true and isCertified = false and isprint = false order by lastname,firstName";

        // Get Students list who completed their course
        public List<UserDetails> GetStudentReportWhoCompletedCourse(string course, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentReportWhoCompletedCourse, course);
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

        public List<UserDetails> GetStudentProgressReport(string stream, int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentProgressReport, stream);
                Database db = new Database();

                List<UserDetails> userlist = new List<UserDetails>();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            UserChapterStatusDAO userTimeDAO = new UserChapterStatusDAO();

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

                                // batch year use as CurrentChapterId
                                int section;
                                user.BatchYear = userTimeDAO.GetUserMaxChapterSectionIdByUserId(user.Id, courseId, out section, cnxnString, logPath);

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

        public List<UserDetails> GetStudentNotStartedCourse(string course, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentNotStartedCourse, course);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            UserChapterStatusDAO userTimeDAO = new UserChapterStatusDAO();
                            
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

        public List<UserDetails> GetStudentTypingStatus(string course, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentTypingStatus, course);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            UserChapterStatusDAO userTimeDAO = new UserChapterStatusDAO();
                            
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

        public List<UserDetails> GetStudentReportWhoCompletedCourseFinalQuizTyping(string course, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentReportWhoCompletedCourse, course);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            Dictionary<int, string> dicCorrectQuizCount;
                            Dictionary<int, string> dicTypingLevel = GetUsersAndTheirTypingLevels(cnxnString, logPath);
                            Dictionary<int, string> dicFinalQuizRes = GetUsersAndTheirFinalQuizResCount(cnxnString, logPath, out dicCorrectQuizCount);
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

                                string quizResCount = string.Empty;
                                string typingLevel = string.Empty;

                                try
                                {
                                    quizResCount = dicFinalQuizRes[user.Id];
                                    typingLevel = dicTypingLevel[user.Id];
                                    user.MobileNo = dicCorrectQuizCount[user.Id]; // user mobile numbers use as Correct quiz count
                                }
                                catch (Exception)
                                {
                                }

                                if (!string.IsNullOrEmpty(quizResCount) && !string.IsNullOrEmpty(typingLevel) && quizResCount == "100" && typingLevel == "23")
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

        private Dictionary<int, string> GetUsersAndTheirTypingLevels(string cnxnString, string logPath)
        {
            Dictionary<int, string> dicStudent = new Dictionary<int, string>();

            try
            {
                string cmdText = "SELECT count(*) CompletedLevel, UserId FROM studenttypingstats group by userid";

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    DbDataReader reader = new Database().Select(cmdText, logPath, con);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dicStudent.Add(int.Parse(reader["UserId"].ToString()), reader["CompletedLevel"].ToString());
                        }

                        if (!reader.IsClosed)
                        {
                            reader.Close();
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dicStudent;
        }

        private Dictionary<int, string> GetUsersAndTheirFinalQuizResCount(string cnxnString, string logPath, out Dictionary<int, string> dicCorrectQuizCount)
        {
            Dictionary<int, string> dicStudent = new Dictionary<int, string>();
            dicCorrectQuizCount = new Dictionary<int, string>();

            try
            {
                string cmdText = "SELECT UserId, count(questionid) FinalResCount, sum(case when iscorrect = true then 1 else 0 end) CorrectQuizCount FROM finalquizresponse group by userid";

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    DbDataReader reader = new Database().Select(cmdText, logPath, con);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dicStudent.Add(int.Parse(reader["UserId"].ToString()), reader["FinalResCount"].ToString());
                            dicCorrectQuizCount.Add(int.Parse(reader["UserId"].ToString()), reader["CorrectQuizCount"].ToString());
                        }

                        if (!reader.IsClosed)
                        {
                            reader.Close();
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dicStudent;
        }
    }
}
