using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class UserLoginsDAO
    {
        Logger logger = new Logger();

        private string cnxnString = ConfigurationManager.ConnectionStrings["AdminApp_cnxnString"].ToString();

        private string Q_GetUserLoginDetailsByUserName = "select * from userlogins where username = '{0}' and password = '{1}'";
        private string Q_AddUserLogin = "insert into userlogins (CollegeName, UserName, Password, LastLogin, ConnectionString, PDFFolderPath, ReleaseFilePath, LogFilePath, userType, ApplicationType, RedirectUrl,IsStatusTrackerRequired) " +
                                                                    "values('{0}','{1}', '{2}', '{3}','{4}','{5}', '{6}', '{7}','{8}','{9}', '{10}',{11})";

        private string Q_UpdatePassword = "update userlogins set password = '{0}' where username = '{1}' and password='{2}'";
        private string Q_GetUserByUserName = "Select * from  userlogins where username='{0}'";
        private string Q_GetUserCountByUserName = "Select Count(*) as UserNameCount from  userlogins where username like '{0}%'";
        private string Q_GetUniqueCollageCnxnStrFromController = "SELECT distinct CollegeName, ConnectionString FROM userlogins u where usertype = 'admin'";


        public void AddUserLoginsDetail(string collegeName, string userName, string password, DateTime lastLogin, string userCnxnString, string pdfFolderPath, string configFilePath, string logFilePath, string userType, string applicationType, string redirectURL, bool isStatusTrackerRequired, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmd = string.Format(Q_AddUserLogin, collegeName, userName, password, lastLogin.ToString("yyyy/MM/dd HH:mm:ss.fff"), userCnxnString, pdfFolderPath,
                    configFilePath, logFilePath, userType, applicationType, redirectURL, isStatusTrackerRequired);
                    Database db = new Database();
                    db.Insert(cmd, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserLogins GetUserLoginByUserNamePassword(string userName, string password, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetUserLoginDetailsByUserName, userName, password);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    UserLogins userLoginDetails = new UserLogins();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userLoginDetails.CollegeId = Convert.ToInt32(reader["CollegeId"]);
                                userLoginDetails.CollegeName = reader["CollegeName"].ToString();
                                userLoginDetails.UserName = reader["UserName"].ToString();
                                userLoginDetails.Password = reader["Password"].ToString();
                                userLoginDetails.CnxnString = reader["ConnectionString"].ToString();
                                userLoginDetails.PDFFolderPath = reader["PDFFolderPath"].ToString();
                                userLoginDetails.ReleaseFilePath = reader["ReleaseFilePath"].ToString();
                                userLoginDetails.LogFilePath = reader["LogFilePath"].ToString();
                                userLoginDetails.UserType = reader["userType"].ToString();
                                userLoginDetails.ApplicationType = reader["ApplicationType"].ToString();
                                userLoginDetails.RedirectURL = reader["RedirectUrl"].ToString();
                                userLoginDetails.IsStatusTrackerRequired = Convert.ToBoolean(reader["IsStatusTrackerRequired"]);
                                userLoginDetails.IsAuthenticated = true;
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            } 
                        }
                    }

                    con.Close();
                    return userLoginDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public UserLogins GetUserByUserName(string userName, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetUserByUserName, userName);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    UserLogins userLoginDetails = new UserLogins();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userLoginDetails.CollegeId = Convert.ToInt32(reader["CollegeId"]);
                                userLoginDetails.CollegeName = reader["CollegeName"].ToString();
                                userLoginDetails.UserName = reader["UserName"].ToString();
                                userLoginDetails.Password = reader["Password"].ToString();
                                userLoginDetails.CnxnString = reader["ConnectionString"].ToString();
                                userLoginDetails.PDFFolderPath = reader["PDFFolderPath"].ToString();
                                userLoginDetails.ReleaseFilePath = reader["ReleaseFilePath"].ToString();
                                userLoginDetails.LogFilePath = reader["LogFilePath"].ToString();
                                userLoginDetails.UserType = reader["userType"].ToString();
                                userLoginDetails.ApplicationType = reader["ApplicationType"].ToString();
                                userLoginDetails.RedirectURL = reader["RedirectUrl"].ToString();
                                userLoginDetails.IsStatusTrackerRequired = Convert.ToBoolean(reader["IsStatusTrackerRequired"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userLoginDetails;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetAllUsers", " Error occurred while Getting User list", ex, logPath);
                throw ex;
            }
        }

        public int GetUserCountByUserName(string userName, string logPath)
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

        public void ChangePassword(string userName, string oldPassword, string newPassword, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdatePassword, newPassword, userName, oldPassword);
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

        public List<UserLogins> GetUniqueCollageCnxnStrFromController(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetUniqueCollageCnxnStrFromController);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserLogins> userLoginDetailsList = new List<UserLogins>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserLogins userLoginDetails = new UserLogins();

                                userLoginDetails.CollegeName = reader["CollegeName"].ToString();
                                userLoginDetails.CnxnString = reader["ConnectionString"].ToString();

                                userLoginDetailsList.Add(userLoginDetails);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userLoginDetailsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUserName(int userId, string newUsername, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmd = string.Format("update userlogins set username = '{0}' where CollegeId={1}", newUsername, userId);

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
    }
}
