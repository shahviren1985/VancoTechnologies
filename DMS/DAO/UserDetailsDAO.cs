using AA.DAOBase;
using AA.LogManager;
using AA.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Util;
namespace AA.DAO
{
    public class UserDetailsDAO
    {
        Logger logger = new Logger();

        #region SELECT
        public string IS_AUTHORIZED = "select Id, UserName, FirstName, LastName, RoleId, count(*) as count from users where UserName='{0}' and UserPassword='{1}'";
        public string SELECT_USER_DETAILS = "SELECT u.Id, UserName,FirstName,LastName,Email,RoleId,r.Name as RoleName,UserPassword,IsActive,department FROM users u inner join role r on u.roleid=r.id";
        public string SELECT_USER_DETAILS_BY_ID = "SELECT Id,UserName,FirstName,LastName,RoleId,UserPassword,LastLoggedin,IsActive,Department,Email FROM users Where UserName = '{0}'";

        public UserDetails IsAuthenticated(string userName, string password, string logPath)
        {
            UserDetails user = new UserDetails();
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

                logger.Debug("UserDetailsDAO", "IsAuthenticated", " Authenticating User", logPath);

                string cmdText = string.Format(IS_AUTHORIZED, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));

                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connectionString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {
                            logger.Debug("UserDetailsDAO", "IsAuthenticated", " User Authentication failed ", logPath);
                            //return false;
                        }
                        else
                        {
                            logger.Debug("UserDetailsDAO", "IsAuthenticated", " User  Authenticated ", logPath);

                            user.UserName = reader["UserName"].ToString();
                            //user.Password = reader["Password"].ToString();
                            //user.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.RoleId = int.Parse(reader["RoleId"].ToString());
                            //user.IsActive = bool.Parse(reader["IsActive"].ToString());
                        }
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return user;
        }

        public UserDetails IsAuthenticated(string userName, string password, string cnxnString, string logPath)
        {
            UserDetails user = new UserDetails();
            try
            {
                logger.Debug("UserDetailsDAO", "IsAuthenticated", " Authenticating User", logPath);

                string cmdText = string.Format(IS_AUTHORIZED, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));

                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, cnxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {
                            logger.Debug("UserDetailsDAO", "IsAuthenticated", " User Authentication failed ", logPath);
                            //return false;
                        }
                        else
                        {
                            logger.Debug("UserDetailsDAO", "IsAuthenticated", " User  Authenticated ", logPath);

                            user.UserName = reader["UserName"].ToString();
                            user.Id = int.Parse(reader["Id"].ToString());
                            //user.Password = reader["Password"].ToString();
                            //user.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.RoleId = int.Parse(reader["RoleId"].ToString());
                            //user.IsActive = bool.Parse(reader["IsActive"].ToString());
                        }
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return user;
        }
        #endregion

        #region CREATE
        public string INSERT_USER_DETAILS = "INSERT INTO users (UserName,FirstName,LastName,RoleId,UserPassword,LastLoggedin,IsActive,Department,Email,MobileNo,DateCreated) VALUES ('{0}','{1}','{2}',{3},'{4}','{5}',{6},'{7}','{8}','{9}','{10}')";
        public UserDetails CreateUser(string userId, string firstName, string lastName, int roleId, string password, DateTime lastLogin, bool isActive, string departments, string email, string mobileNo, string cnxnString, string logPath)
        {
            UserDetails details2;
            try
            {
                string str = string.Format(this.INSERT_USER_DETAILS, new object[] { ParameterFormater.FormatParameter(userId), ParameterFormater.FormatParameter(firstName), ParameterFormater.FormatParameter(lastName), roleId, ParameterFormater.FormatParameter(password), lastLogin.ToString("yyyy-MM-dd hh:mm:ss"), isActive, ParameterFormater.FormatParameter(departments), email, mobileNo, DateTime.Now.ToString("yyyy-MM-dd") });
                new Database().Insert(str, cnxnString, logPath);
                UserDetails details = new UserDetails
                {
                    UserName = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    RoleId = roleId,
                    Password = password,
                    LastLogin = lastLogin,
                    IsActive = isActive,
                    Email = email,
                    MobileNo = mobileNo
                };
                this.logger.Debug("UserDetailsDAO", "CreateUser", " New User Details Saved and returning user details", logPath);
                details2 = details;
            }
            catch (Exception exception)
            {
                this.logger.Error("UserDetailsDAO", "CreateUser", " Error occurred while Adding New  User details", exception, logPath);
                throw new Exception("11376");
            }
            return details2;
        }


        #endregion

        #region UPDATE
        public string UPDATE_USER_PASSWORD = "UPDATE users set UserPassword='{1}' WHERE UserName='{0}'";
        public string UPDATE_USER_LASTLOGIN = "UPDATE users set LastLogin='{1}' WHERE UserName='{0}'";
        public string UPDATE_USER_DETAILS = "UPDATE  users set FirstName = '{1}',LastName = '{2}', LastLoggedin = '{3}',IsActive = {4}, RoleId={5}, Department='{6}',Email='{7}' WHERE UserName ='{0}'";
        public string DELETE_USER_DETAILS = "DELETE FROM users WHERE Id ={0}";
        #endregion

        public List<UserDetails> GetUserList(string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserDetailsDAO", "GetUserList", "Selecting User Details list from database", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_USER_DETAILS, cnxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserDetails> userlist = new List<UserDetails>();
                    while (reader.Read())
                    {
                        UserDetails user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.RoleId = Convert.ToInt32(reader["RoleId"]);
                        user.RoleName = reader["RoleName"].ToString();
                        //user.Password = reader["Password"].ToString();
                        //user.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.Departments = reader["Department"].ToString();
                        userlist.Add(user);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserDetailsDAO", "GetUserList", " returning User Details list  from database", logPath);
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetUserList", " Error occurred while Getting User list", ex, logPath);
                throw new Exception("11377");
            }

            return null;
        }
        public UserDetails GetUserDetailList(string userid, string cnxnString, string logPath)
        {
            UserDetails user = null;
            try
            {
                logger.Debug("UserDetailsDAO", "GetUserDetailList", "Selecting User Details from database by User id ", logPath);
                string result = string.Format(SELECT_USER_DETAILS_BY_ID, userid);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();

                        user.RoleId = Convert.ToInt32(reader["RoleId"]);
                        user.Password = reader["UserPassword"].ToString();
                        user.LastLogin = DateTime.Parse(reader["LastLoggedin"].ToString());
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.Departments = reader["Department"].ToString();
                        user.Email = reader["Email"].ToString();
                        logger.Debug("UserDetailsDAO", "GetUserDetailList", " Returning User Details from database by user id", logPath);
                        //return user;
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetUserDetailList", " Error occurred while Getting User Details", ex, logPath);
                throw new Exception("11381");
            }

            return user;
        }

        public UserDetails GetUserDetailbyId(int id, string cnxnString, string logPath)
        {
            Database db = new Database();

            try
            {
                logger.Debug("UserDetailsDAO", "GetUserDetailList", "Selecting User Details from database by User id ", logPath);
                string result = string.Format("SELECT Id,UserName,FirstName,LastName,Email,UserPassword,LastLoggedin,IsActive,RoleId,Department FROM users Where Id = {0}", id);

                DbDataReader reader = db.Select(result, cnxnString, logPath);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        UserDetails user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["UserPassword"].ToString();
                        user.LastLogin = DateTime.Parse(reader["LastLoggedin"].ToString());
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.RoleId = Convert.ToInt32(reader["RoleId"]);

                        logger.Debug("UserDetailsDAO", "GetUserDetailList", " Returning User Details from database by user id", logPath);
                        return user;
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetUserDetailList", " Error occurred while Getting User Details", ex, logPath);
                throw new Exception("11381");
            }
            finally
            {
                db.KillSleepingConnections(cnxnString, logPath);
            }

            return null;
        }

        public bool RemoveUser(int Id, string cnxnString, string logPath)
        {
            try
            {
                string removeUser = string.Format(DELETE_USER_DETAILS, Id);
                Database db = new Database();
                logger.Debug("UserDetailsDAO", "RemoveUser", "Saving Changes in User Details", logPath);
                db.Delete(removeUser, cnxnString, logPath);
                logger.Debug("UserDetailsDAO", "RemoveUser", "Saved Changes in User Details", logPath);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "RemoveUser", " Error occurred while Delete User", ex, logPath);
                throw new Exception("11378");
            }
        }

        public bool ChangeUserDetails(UserDetails userDetails, string cnxnString, string logPath)
        {
            try
            {
                if (userDetails != null)
                {
                    string updateUser = string.Format(UPDATE_USER_DETAILS, userDetails.UserName, ParameterFormater.FormatParameter(userDetails.FirstName), ParameterFormater.FormatParameter(userDetails.LastName), userDetails.LastLogin.ToString("yyyy/MM/dd hh:mm:ss.fff"), userDetails.IsActive, userDetails.RoleId, userDetails.Departments, userDetails.Email);
                    Database db = new Database();
                    logger.Debug("UserDetailsDAO", "ChangeUserDetails", "Saving User Details Saved", logPath);
                    db.Update(updateUser, cnxnString, logPath);

                    logger.Debug("UserDetailsDAO", "ChangeUserDetails", "User Details Saved", logPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "ChangeUserDetails", " Error occurred while Update User Details", ex, logPath);
                throw new Exception("11379");
            }
        }

        public bool ChangePassword(string userId, string newPassword, string cnxnString, string logPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(newPassword))
                {

                    logger.Debug("UserDetailsDAO", "ChangePassword", "Updating User Password.", logPath);
                    string updateUser = string.Format(UPDATE_USER_PASSWORD, userId, newPassword);
                    Database db = new Database();
                    logger.Debug("UserDetailsDAO", "ChangePassword", "Updating User Password", logPath);
                    db.Update(updateUser, cnxnString, logPath);

                    logger.Debug("UserDetailsDAO", "ChangePassword", "User Password Saved", logPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "ChangePassword", "Error occurred while Update User Password", ex, logPath);
                throw new Exception("11384");
            }
        }
        public void UpdateLastLogin(string userName, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserDetailsDAO", "UpdateLastLogin", "Updating Users last Login.", logPath);
                string updateUser = string.Format(UPDATE_USER_LASTLOGIN, userName, Utilities.GetCurrentIndianDate().ToString("yyyy/MM/dd hh:mm:ss.fff"));
                Database db = new Database();
                logger.Debug("UserDetailsDAO", "UpdateLastLogin", "Updating User Last Login", logPath);
                db.Update(updateUser, cnxnString, logPath);

                logger.Debug("UserDetailsDAO", "UpdateLastLogin", "User Last Login Saved", logPath);
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "UpdateLastLogin", "Error occurred while Updating user Last Login", ex, logPath);
                throw new Exception("");
            }
        }

        public List<UserDetails> GetUsersByDepartment(string query, string cnxnString, string logPath)
        {
            //Id, UserName,FirstName,LastName,Email,UserPassword,IsActive,department
            try
            {
                logger.Debug("UserDetailsDAO", "GetUsersByDepartment", "Selecting User Details list from database", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(query, cnxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserDetails> userlist = new List<UserDetails>();
                    while (reader.Read())
                    {
                        UserDetails user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["UserPassword"].ToString();
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.Departments = reader["Department"].ToString();
                        userlist.Add(user);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserDetailsDAO", "GetUsersByDepartment", " returning User Details list  from database", logPath);
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "GetUsersByDepartment", " Error occurred while Getting User list", ex, logPath);
                throw new Exception("11377");
            }

            return null;
        }
    }
}
