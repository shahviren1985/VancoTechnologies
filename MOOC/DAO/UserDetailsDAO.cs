using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using System.Data.Common;
using ITM.Courses.Utilities;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{

    public class UserDetailsDAO
    {
        Logger logger = new Logger();
        private string Select_All_Users = "Select * from userdetails";
        private string ADD_USER_DETAILS = "INSERT INTO userdetails (DateCreated,LastLogin,firstName,LastName,EmailAddress,userType,username,password,IsActive,IsEnabled,IsCompleted,IsCertified,VersionRegistered,MobileNo, FatherName, MotherName,RollNumber,Course,SubCourse,BatchYear) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19})";
        private string Select_Users_By_UserType = "SELECT * FROM userdetails where iscompleted = true and usertype = 'user' and isPrint = false and isactive=true and isEnabled = true order by lastname,firstName";
        private string Select_Users_By_UserType_And_OtherStatus = "SELECT * FROM userdetails where iscompleted = {0} and usertype = 'user' and isactive=true and isEnabled = true order by lastname,firstName";
        private string Select_Cousre_Details_From_UserDetails = "select Distinct(course) from userdetails where course <> '0' and course <> 'Course' and isactive=true and isEnabled = true";
        public string UPDATE_USER_DETAILS = "UPDATE  userdetails set DateCreated = '{1}',LastLogin = '{2}',firstName = '{3}',LastName = '{4}',EmailAddress = {5},userType = '{6}',username = '{7}', password = '{8}',IsActive = {9}, IsEnabled = {10}, IsCompleted = {11}, IsCertified = {12}, VersionRegistered = '{13}', MobileNo = {14},FatherName='{15}',MotherName='{16}', RollNumber='{17}',Course='{18}',SubCourse='{19}',BatchYear={20}  WHERE Id = {0}";
        public string UPDATE_IsACTIVE_STATUS = " UPDATE userdetails set IsActive = {0} where id = {1}";
        private string Q_IsAuthenticated = "Select * from  userdetails where username='{0}' and password='{1}'";

        private string Q_GetUserByUserName = "Select * from  userdetails where username='{0}'";
        private string Q_GetUserCountByUserName = "Select Count(*) as UserNameCount from  userdetails where username like '{0}%'";
        private string Q_UpdatePassword = "update userdetails set password = '{0}' where username = '{1}' and password='{2}'";
        private string Q_SelectAllUserWithoutAdmin = "Select * from userdetails where usertype = 'User'";
        private string Q_GetUserByUserId = "Select * from  userdetails where id={0}";
        private string Q_UpdateIsPrintFlag = "update userdetails set isPrint = {0} where id={1}";
        private string Q_UpdateIsCompleteFlag = "update userdetails set IsCompleted = {0} where id={1}";
        private string Q_GetStudentByCourse = "Select * from userdetails where usertype = 'User' and course='{0}' and isactive=true and isEnabled = true";

        #region Search Queries
        private string Q_GetSudentByUserNameandFirstorLastName = "SELECT * FROM userdetails u where username like '%{0}%' || firstname like '%{1}%' || lastname like '%{2}%'";
        private string Q_GetSudentByUserName = "SELECT * FROM userdetails u where username like '%{0}%'";
        private string Q_GetSudentByFirstorLastName = "SELECT * FROM userdetails u where firstname like '%{0}%' || lastname like '%{1}%'";
        #endregion

        private string Q_GetStudentsByCourseAndStream = "SELECT * FROM userdetails u inner join studentcoursemapper sm on u.id = sm.userid where u.course = '{0}' and sm.courseid={1} and batchyear={2} and u.iscompleted=1";
        //private string Q_GetStudentsByCourseAndStream = "SELECT * FROM userdetails u inner join studentcoursemapper sm on u.id = sm.userid where u.course = '{0}' and sm.courseid={1} and batchyear={2} and u.iscompleted=0 and u.id in (1188, 1257, 1215, 1304, 1311, 1098, 1122, 1148, 1154, 1336, 1282, 1081, 1348, 1087, 1319, 1021, 1421, 1273, 1059, 1033, 1050, 1339, 1418, 1158, 1097, 1099, 1413, 1111, 1185, 1157, 1207)";

        #region Search Functions
        public List<UserDetails> GetSudentByUserNameandFirstorLastName(string userName, string firstLastName, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserDetailsDAO", "GetAllUsers", "Selecting User Details list from database", logPath);
                string result = string.Format(Q_GetSudentByUserNameandFirstorLastName, userName, firstLastName, firstLastName);

                Database db = new Database();
                List<UserDetails> userlist = new List<UserDetails>();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
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

                                user.StudentFullName = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;

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
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public List<UserDetails> GetSudentByUserName(string userName, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetSudentByUserName, userName);

                Database db = new Database();
                List<UserDetails> userlist = new List<UserDetails>();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
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

                                user.StudentFullName = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;

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
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public List<UserDetails> GetSudentByFirstorLastName(string firstLastName, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetSudentByFirstorLastName, firstLastName, firstLastName);

                Database db = new Database();
                List<UserDetails> userlist = new List<UserDetails>();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
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

                                user.StudentFullName = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;

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
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Select Function
        public List<UserDetails> GetStudentsByCourseAndStream(string stream, int courseId, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetStudentsByCourseAndStream, stream, courseId, ConfigurationManager.AppSettings["BatchYear"]);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
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
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public List<UserDetails> GetAllUsers(bool isIncludeAdmin, string cnxnString, string logPath)
        {
            try
            {
                
                string result = string.Empty;

                if (isIncludeAdmin)
                    result = Select_All_Users;
                else
                    result = Q_SelectAllUserWithoutAdmin;

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
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
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public List<UserDetails> GetStudentByCourse(string course, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetStudentByCourse, course); ;

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
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
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            } 
        }

        public UserDetails GetUserByUserName(string userName, string cnxnString, string logPath)
        {
            try
            {
                
                string result = string.Format(Q_GetUserByUserName, userName);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    UserDetails user = new UserDetails();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
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
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public UserDetails GetUserByUserId(int userId, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserDetailsDAO", "GetAllUsers", "Selecting User Details list from database", logPath);
                string result = string.Format(Q_GetUserByUserId, userId);
                Database db = new Database();
                UserDetails user = new UserDetails();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
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
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public int GetUserCountByUserName(string userName, string cnxnString, string logPath)
        {
            int count = 0;

            try
            {
                string result = string.Format(Q_GetUserCountByUserName, userName);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader["UserNameCount"]);
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
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }

            return count;
        }

        public UserDetails IsAuthenticated(string userName, string password, string cnxnString, string logPath)
        {
            UserDetails user = new UserDetails();
            user.IsAuthenticated = false;
            try
            {
                string cmdText = string.Format(Q_IsAuthenticated, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                user.Id = Convert.ToInt32(reader["Id"].ToString());
                                user.UserName = reader["UserName"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.EmailAddress = reader["EmailAddress"].ToString();
                                user.FirstName = ParameterFormater.UnescapeXML(reader["FirstName"].ToString());
                                user.LastName = ParameterFormater.UnescapeXML(reader["LastName"].ToString());
                                user.FatherName = ParameterFormater.UnescapeXML(reader["FatherName"].ToString());
                                user.MotherName = ParameterFormater.UnescapeXML(reader["MotherName"].ToString());
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.UserType = reader["userType"].ToString();
                                user.IsNewUser = Convert.ToBoolean(reader["IsNewUser"]);
                                user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                user.IsEnable = Convert.ToBoolean(reader["IsEnabled"]);
                                user.IsAuthenticated = true;
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
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return user;
        }

        public List<UserDetails> GetUserByUserType(string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Select_Users_By_UserType);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userList = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
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
                                userList.Add(user);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetUserByUserType", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public List<UserDetails> GetUserByUserTypeAndOtherStatus(bool isCompleted, string cnxnString, string logPath)
        {
            try
            {
                
                string result = string.Format(Select_Users_By_UserType_And_OtherStatus, isCompleted);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userList = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
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
                                userList.Add(user);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetUserByUserType", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        // Select function for selecting course from user details table for generating user typing status report. 
        public List<UserDetails> GetUniqueCoursesList(string cnxnString, string logPath)
        {
            try
            {
                string result = Select_Cousre_Details_From_UserDetails;
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> CourseList = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserDetails Course = new UserDetails();
                                Course.Course = ParameterFormater.UnescapeXML(reader["Course"].ToString());
                                CourseList.Add(Course);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return CourseList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetCourseDetails", " Error occurred while Getting Course list", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Insert Statement
        public void CreateUser(DateTime datecreated, DateTime lastlogin, string firstname, string lastname, string emailaddress, string usertype, string username,
            string password, bool isactive, bool isenable, bool iscompleted, bool iscertifited, string versionregistered, string mobile, string father, string mother, string rollNo,
            string course, string subcourse, int batchYear, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserDetailsDAO", "CreateUser", "Inserting User Details", logPath);
                string result = string.Format(ADD_USER_DETAILS,
                    datecreated.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    lastlogin.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    ParameterFormater.FormatParameter(firstname),
                    ParameterFormater.FormatParameter(lastname),
                    emailaddress, ParameterFormater.FormatParameter(usertype),
                    username, password, isactive, isenable, iscompleted, iscertifited,
                    ParameterFormater.FormatParameter(versionregistered), mobile,
                    ParameterFormater.FormatParameter(father),
                    ParameterFormater.FormatParameter(mother), rollNo,
                    ParameterFormater.FormatParameter(course),
                    ParameterFormater.FormatParameter(subcourse), batchYear);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    db.Insert(result, logPath, con);
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "CreateUser", " Error occurred while adding new  User details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Statement
        //update all details of user
        public void UpdateUser(int id, DateTime datecreated, DateTime lastlogin, string firstname, string lastname, string emailaddress, string usertype, string username,
            string password, bool isactive, bool isenable, bool iscompleted, bool iscertifited, string versionregistered, string mobile, string father, string mother,
            string rollNo, string course, string subCourse, int batchYear, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(UPDATE_USER_DETAILS, id, datecreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), lastlogin.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    ParameterFormater.FormatParameter(firstname),
                    ParameterFormater.FormatParameter(lastname),
                    emailaddress, username, password, isactive, isenable, iscompleted, iscertifited,
                    ParameterFormater.FormatParameter(versionregistered), mobile,
                    ParameterFormater.FormatParameter(father),
                    ParameterFormater.FormatParameter(mother), rollNo,
                    ParameterFormater.FormatParameter(course),
                    ParameterFormater.FormatParameter(subCourse), batchYear);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    db.Update(result, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "UpdateUser", " Error occurred while Updating User Details", ex, logPath);
                throw ex;
            }
        }

        //update IsActive status
        public void UpdateIsActiveStatus(int id, bool isactive, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(UPDATE_IsACTIVE_STATUS, isactive, id);
                    Database db = new Database();
                    db.Update(result, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "UpdateIsActiveStatus", " Error occurred while Updating User Details", ex, logPath);
                throw ex;
            }

        }

        public void ChangePassword(string userName, string oldPassword, string newPassword, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdatePassword, newPassword, userName, oldPassword);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateIsNewUserFlag(int userId, bool isNewUser, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format("update userdetails set IsNewUser = {0} where id = {1}", isNewUser, userId);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateIsPrintFlag(int userId, bool isPrint, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateIsPrintFlag, isPrint, userId);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateIsCompleteFlag(int userId, bool isComplete, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateIsCompleteFlag, isComplete, userId);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateUserNameEmail(int userId, string newUsername, string email, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmd = string.Format("update userdetails set username = '{0}', emailaddress = '{1}' where id={2}", ParameterFormater.FormatParameter(newUsername), email, userId);
                    Database db = new Database();
                    db.Update(cmd, logPath, con);
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

