using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace ITM.Courses.DAO
{
    public class UserLoggerDAO
    {
        Logger logger = new Logger();
        private string Select_All_Loged_In_User = "Select * from userlogger";
        private string Select_Last_Login_Date = "Select * from userlogger where userid = {0}";
        private string Add_Loged_In_User = "INSERT INTO userlogger(userId, LoginDate, IpAddress) VALUES ({0}, '{1}', '{2}')";
        private string Update_Loged_In_User = "UPDATE userlogger set LoginDate = '{1}',IpAddress = {2} WHERE Id = {0}";

        #region Select function
        List<UserLogger> GetAllLogedInUser(string cnxnString, string logPath)
        {
            try
            {
                string result = Select_All_Loged_In_User;
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserLogger> userlist = new List<UserLogger>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserLogger user = new UserLogger();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userId"]);
                                user.LoginDate = Convert.ToDateTime(reader["LoginDate"].ToString());
                                user.IpAddress = reader["IpAddress"].ToString();
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
                logger.Error("UserLoggerDAO", "GetAllLogedInUser", " Error occurred while Getting Loged In User list", ex, logPath);
                throw ex;
            }
        }

        // function for collecting last login details by user id.
        public UserLogger GetLastLoginDateById(int userId, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Select_Last_Login_Date, userId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    UserLogger lastLogin = new UserLogger();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserLogger loginDetail = new UserLogger();
                                loginDetail.Id = Convert.ToInt32(reader["Id"]);
                                loginDetail.UserId = Convert.ToInt32(reader["userId"]);
                                loginDetail.LoginDate = Convert.ToDateTime(reader["LoginDate"].ToString());
                                loginDetail.IpAddress = reader["IpAddress"].ToString();
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return lastLogin;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserLoggerDAO", "GetLastLoginDateById", " Error occurred while collecting last login details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Insert Statement
        public void AddLogedUser(int userId, DateTime loginDate, string ipAddress, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(Add_Loged_In_User, userId, loginDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), ipAddress);
                    Database db = new Database();
                    db.Insert(result, logPath, con);
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error("UserLoggerDAO", "InsertLogedUser", " Error occurred while saving sign-in user details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Insert Statement
        public void UpdateLogedUser(int id, DateTime loginDate, DateTime ipAddressrname, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(Update_Loged_In_User, id, loginDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), ipAddressrname.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    Database db = new Database();
                    db.Insert(result, logPath, con);
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error("UserLoggerDAO", "UpdateLogedUser", " Error occurred while updating user details", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
