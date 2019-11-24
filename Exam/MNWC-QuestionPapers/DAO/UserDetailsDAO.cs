using ITM.DAOBase;
using ITM.LogManager;
using ITM.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace ITM.DAO
{

    public class UserDetailsDAO
    {
        Logger logger = new Logger();

        public string Q_SelectUserbyUserName = "select count(*) as count from userdetails where UserId='{0}' and Email='{1}' ";
        public string Q_SelectUserbyUserNamePassword = "select count(*) as count from dmslogindetails where username='{0}' and password='{1}'";
        public string SELECT_USER_DETAILS = "SELECT Id, UserId,FirstName,LastName,Email,Password,LastLogin,IsActive,RoleId,StaffId,History FROM userdetails";
        public string SELECT_USER_DETAILS_BY_ID = "SELECT Id,UserId,FirstName,LastName,Email,Password,LastLogin,IsActive,RoleId,StaffId,History FROM userdetails Where UserId = '{0}'";
        public string INSERT_USER_DETAILS = "INSERT INTO userdetails (UserId,FirstName,LastName,Email,Password,LastLogin,IsActive,RoleId,StaffId,History,UserStatus,VerificationId) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},'{9}','{10}','{11}')";
        public string UPDATE_USER_DETAILS = "UPDATE  userdetails set FirstName = '{0}',LastName = '{1}',Email = '{2}',LastLogin = '{3}',IsActive ={4},RoleId ={5},StaffId={6},History='{7}' WHERE Id ={8}";
        public string UPDATE_USERSTATUS = "UPDATE userdetails set UserStatus='Verified', IsActive=true Where UserId='{0}' and VerificationId='{1}'";
        public string DELETE_USER_DETAILS = "DELETE FROM userdetails WHERE Id ={0}";
        public string GET_USER_BY_ID = "SELECT FirstName, LastName FROM userdetails WHERE Id ={0}";
        public string GET_USER_IF_EXISTS = "Select count(*) as count from userdetails where UserId='{0}'";
        public string Q_GetUserById = "SELECT Id,UserId,FirstName,LastName,Email,Password,LastLogin,IsActive,RoleId,StaffId,History FROM userdetails Where Id = '{0}'";
        public string UPDATE_USER_PASSWORD = "UPDATE userdetails set Password='{1}' WHERE Id={0}";
        public string UPDATE_USER_Password = "UPDATE userdetails set Password='{1}' WHERE UserId='{0}'";
      

      

        #region Select
        //public bool ChangePassword(int userId, string newPassword, string connString, string logPath)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(newPassword))
        //        {

        //        //    Logger.Debug("UserDetailsDAO", "ChangePassword", "Updating User Password.");
        //            string updateUser = string.Format(UPDATE_USER_PASSWORD, userId, newPassword);
        //            Database db = new Database();
        //        //    Logger.Debug("UserDetailsDAO", "ChangePassword", "Updating User Password");
        //            db.Update(updateUser, connString, logPath);

        //         //   Logger.Debug("UserDetailsDAO", "ChangePassword", "User Password Saved");
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //       // Logger.Error("UserDetailsDAO", "ChangePassword", "Error occurred while Update User Password", ex);
        //        throw new Exception("11384");
        //    }
        //    finally
        //    {
        //        Database.KillConnections(connString, logPath);
        //    }
        //}
        //public string CreateUserHistory(string username, string userId, string firstName, string lastName, string email, int roleId, string password, DateTime lastLogin, bool isActive, int facultyId, bool isFaculty)
        //{
        //    try
        //    {
        //        Hashtable table = GetHashTable(0, userId, firstName, lastName, email, password, lastLogin, isActive, roleId, facultyId, string.Empty);
        //       // string history = HistoricalDataManager.CreateHistory(username, table);
        //        return history;
        //    }
        //    catch (Exception ex)
        //    {
        //       // Logger.Error("UserDetailsDAO", "CreateUserHistory", "Error occurred while Creating User History", ex);
        //        throw new Exception("11386");
        //    }
        //    finally
        //    {
        //        Database.KillConnections();
        //    }
        //}
       
        
        public List<UserDetails> GetUserList(string connString, string logPath)
        {
            try
            {
             //   Logger.Debug("UserDetailsDAO", "GetUserList", "Selecting User Details list from database ");
                string result = string.Format(SELECT_USER_DETAILS);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {
                    List<UserDetails> userlist = new List<UserDetails>();
                    while (reader.Read())
                    {
                        UserDetails user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserId = reader["UserId"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.RoleId = Convert.ToInt32(reader["RoleId"]);
                     //   user.UserFirstLastName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                        if (!string.IsNullOrEmpty(reader["StaffId"].ToString()))
                        {
                         //   user.StaffId = Convert.ToInt32(reader["StaffId"]);
                        }
                        user.History = reader["History"].ToString();
                        userlist.Add(user);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                  //  Logger.Debug("UserDetailsDAO", "GetUserList", " returning User Details list  from database ");
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error("UserDetailsDAO", "GetUserList", " Error occurred while Getting User list", ex);
                throw new Exception("11377");
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }
            return null;
        }

        public UserDetails GetUserDetailList(string userid, string connString, string logPath)
        {
            try
            {
               // Logger.Debug("UserDetailsDAO", "GetUserDetailList", "Selecting User Details from database by User id ");
                string result = string.Format(SELECT_USER_DETAILS_BY_ID, userid);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        UserDetails user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserId = reader["UserId"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.RoleId = Convert.ToInt32(reader["RoleId"]);

                        if (!string.IsNullOrEmpty(reader["StaffId"].ToString()))
                        {
                           // user.StaffId = Convert.ToInt32(reader["StaffId"]);
                        }

                        user.History = reader["History"].ToString();
                    //    Logger.Debug("UserDetailsDAO", "GetUserDetailList", " Returning User Details from database by user id");
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
              //  Logger.Error("UserDetailsDAO", "GetUserDetailList", " Error occurred while Getting User Details", ex);
                throw new Exception("11381");
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }
            return null;
        }

        public UserDetails GetUserDetailbyId(int id, string connString, string logPath)
        {
            try
            {
            //    Logger.Debug("UserDetailsDAO", "GetUserDetailList", "Selecting User Details from database by User id ");
                string result = string.Format(Q_GetUserById, id);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        UserDetails user = new UserDetails();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.UserId = reader["UserId"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                        user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        user.RoleId = Convert.ToInt32(reader["RoleId"]);
                        if (!string.IsNullOrEmpty(reader["StaffId"].ToString()))
                        {
                           // user.StaffId = Convert.ToInt32(reader["StaffId"]);
                        }
                        user.History = reader["History"].ToString();
                     //   Logger.Debug("UserDetailsDAO", "GetUserDetailList", " Returning User Details from database by user id");
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
               // Logger.Error("UserDetailsDAO", "GetUserDetailList", " Error occurred while Getting User Details", ex);
                throw new Exception("11381");
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }

            return null;
        }

        #endregion
        public bool IsUserExists(string userId, string connString, string logPath)
        {
            bool isUserExist = false;
            try
            {
             //   Logger.Debug("UserDetailsDAO", "IsUserExists", " Checking whether User is alredy present or not");
                string cmdText = string.Format(GET_USER_IF_EXISTS, userId);
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        isUserExist = int.Parse(reader["count"].ToString()) != 0;
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
             //   Logger.Error("UserDetailsDAO", "IsUserExists", " Error occurred while Checking whether User record is already present", ex);
                throw new Exception("11383");
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }
            return isUserExist;
        }
        public bool IsUserIdExists(string userId , string connString, string logPath)
        {
            try
            {
                string cmdText = string.Format(GET_USER_IF_EXISTS, userId);
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }
            return true;
        }
        public bool IsAuthenticated(string userName, string password, string connString, string logPath)
        {
            try
            {
                string pwd = GetHashString(password);
                Hashtable args = GetHashTable(0, userName, string.Empty, string.Empty, string.Empty, pwd, DateTime.Now, false, 0, 0, string.Empty);
               // Logger.Debug("UserDetailsDAO", "IsAuthenticated", " Authenticating User ", args);
                string cmdText = string.Format(Q_SelectUserbyUserNamePassword, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {
                           // Logger.Debug("UserDetailsDAO", "IsAuthenticated", " User not Authenticated ");
                            return false;
                        }
                        else
                        {
                          //  Logger.Debug("UserDetailsDAO", "IsAuthenticated", " User  Authenticated ");
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
              //  Logger.Error("UserDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", ex);
                //throw new Exception("11382");
                throw ex;
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }
            return false;
        }
        //public bool RemoveUser(int Id, string connString, string logPath)
        //{
        //    try
        //    {

        //        Hashtable args = GetHashTable(Id, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, false, 0, 0, string.Empty);
        //  //      Logger.Debug("UserDetailsDAO", "RemoveUser", " Delete User Details from database by Id", args);
        //        string removeUser = string.Format(DELETE_USER_DETAILS, Id);
        //        Database db = new Database();
        //   //     Logger.Debug("UserDetailsDAO", "RemoveUser", "Saving Changes in User Details");
        //        db.Delete(removeUser, connString, logPath);
        //       // Logger.Debug("UserDetailsDAO", "RemoveUser", "Saved Changes in User Details");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //    //    Logger.Error("UserDetailsDAO", "RemoveUser", " Error occurred while Delete User", ex);
        //        throw new Exception("11378");
        //    }
        //    finally
        //    {
        //        Database.KillConnections(connString, logPath);
        //    }
        //}
        //public bool ChangeUserDetails(UserDetails userDetails, string connString, string logPath)
        //{
        //    try
        //    {
        //        if (userDetails != null)
        //        {
        //            Hashtable args = GetHashTable(userDetails.Id, "", userDetails.FirstName, userDetails.LastName, userDetails.Email, "", userDetails.LastLogin, userDetails.IsActive, userDetails.RoleId, userDetails.StaffId, userDetails.History);
        //           // Logger.Debug("UserDetailsDAO", "ChangeUserDetails", "Updating User Details", args);
        //            string updateUser = string.Format(UPDATE_USER_DETAILS, ParameterFormater.FormatParameter(userDetails.FirstName), ParameterFormater.FormatParameter(userDetails.LastName),
        //                ParameterFormater.FormatParameter(userDetails.Email), userDetails.LastLogin.ToString("yyyy/MM/dd hh:mm:ss.fff"), userDetails.IsActive, userDetails.RoleId, userDetails.StaffId,
        //                ParameterFormater.FormatParameter(userDetails.History), userDetails.Id);
        //            Database db = new Database();
        //           // Logger.Debug("UserDetailsDAO", "ChangeUserDetails", "Saving User Details Saved");
        //            db.Update(updateUser, connString, logPath);

        //          //  Logger.Debug("UserDetailsDAO", "ChangeUserDetails", "User Details Saved");
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //      //  Logger.Error("UserDetailsDAO", "ChangeUserDetails", " Error occurred while Update User Details", ex);
        //        throw new Exception("11379");
        //    }
        //    finally
        //    {
        //        Database.KillConnections(connString, logPath);
        //    }
        //}



        //public bool ChangeUserStatus(string username, string verifyid)
        //{
        //    try
        //    {
        //        if (username != null)
        //        {
        //            Hashtable args = new Hashtable();
        //            args.Add("Username", username);
        //            args.Add("VerificationID", verifyid);
        //         //   Logger.Debug("UserDetailsDAO", "ChangeUserDetails", "Updating User Details", args);
        //            string updateUser = string.Format(UPDATE_USERSTATUS, ParameterFormater.FormatParameter(username), verifyid);

        //            Database db = new Database();
        //       //     Logger.Debug("UserDetailsDAO", "ChangeUserDetails", "Saving User Details Saved");
        //            db.Update(updateUser);

        //       //     Logger.Debug("UserDetailsDAO", "ChangeUserDetails", "User Details Saved");
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //    //    Logger.Error("UserDetailsDAO", "ChangeUserDetails", " Error occurred while Update User Details", ex);
        //        throw new Exception("");
        //    }
        //    finally
        //    {
        //        Database.KillConnections();
        //    }
        //}
       
        
        
        public bool checkUser(string userName, string email, string connString, string logPath)
        {
            bool isAuthenticate = false;
            try
            {


                string cmdText = string.Format(Q_SelectUserbyUserName, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(email));
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, connString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {

                            //return false;
                        }
                        else
                        {

                            isAuthenticate = true;
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

                throw new Exception("");
            }
            finally
            {
                Database.KillConnections(connString, logPath);
            }
            return isAuthenticate;
        }
        //public void Updatepassword(string userName, string pwd)
        //{
        //    try
        //    {

        //        string password = GetHashString(pwd);
        //      //  Logger.Debug("UserDetails", "UpdateLastLogin", "Updating Users password.");
        //        string updateUser = string.Format(UPDATE_USER_Password, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));
        //        Database db = new Database();
        //    //    Logger.Debug("UserDetails", "UpdateLastLogin", "Updating User password");
        //        db.Update(updateUser);

        //      //  Logger.Debug("UserDetails", "UpdateLastLogin", "User password Saved");
        //    }
        //    catch (Exception ex)
        //    {
        //     //   Logger.Error("UserDetails", "UpdateLastLogin", "Error occurred while Updating user password", ex);
        //        throw new Exception("");
        //    }
        //    finally
        //    {
        //        Database.KillConnections();
        //    }
        //}

        //public string CreateUserHistory(string username, string userId, string firstName, string lastName, string email, string password, DateTime lastLogin, bool isActive, int roleId, int staffId)
        //{
        //    Hashtable table = GetHashTable(0, userId, firstName, lastName, email, password, lastLogin, isActive, roleId, staffId, string.Empty);
        //    string history = HistoricalDataManager.CreateHistory(username, table);
        //    return history;
        //}
        //public string UpdateUserHistory(string username, UserDetails olduserdetails, UserDetails newuserdetails)
        //{
        //    Hashtable oldtable = GetHashTable(olduserdetails.Id, olduserdetails.UserId, olduserdetails.FirstName, olduserdetails.LastName, olduserdetails.Email, olduserdetails.Password, olduserdetails.LastLogin, olduserdetails.IsActive, olduserdetails.RoleId, olduserdetails.StaffId, olduserdetails.History);
        //    Hashtable newtable = GetHashTable(newuserdetails.Id, newuserdetails.UserId, newuserdetails.FirstName, newuserdetails.LastName, newuserdetails.Email, newuserdetails.Password, newuserdetails.LastLogin, newuserdetails.IsActive, newuserdetails.RoleId, newuserdetails.StaffId, string.Empty);
        //    string history = HistoricalDataManager.UpdateHistory(username, oldtable, newtable);
        //    return history;
        //}

        //public string AppendUserHistory(string username, string originalXml, UserDetails user)
        //{
        //    Hashtable table = GetHashTable(user.Id, user.UserId, user.FirstName, user.LastName, user.Email, user.Password, user.LastLogin, user.IsActive, user.RoleId, user.StaffId, string.Empty);
        //    string history = HistoricalDataManager.AppendHistory(username, originalXml, table);
        //    return history;
        //}

        public static string GetHashString(string password)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] b = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                   // Logger.Debug("UserInfo", "GetHashString", "Generated Hash Password from password and returned value " + b.ToString());
                    return Convert.ToBase64String(b);
                }

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static string GetActualPassword(string password)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] b = Convert.FromBase64String(password);
                    byte[] p = md5.ComputeHash(b);

                    string pp = Encoding.ASCII.GetString(p);

                    return pp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Hashtable GetHashTable(int id, string userId, string firstName, string lastName, string email, string password, DateTime lastLogin, bool isActive, int roleId, int staffId, string history)
        {
            try
            {
                Hashtable args = new Hashtable();
             //   Logger.Debug("UserDetailsDAO", "GetHashTable", "Adding data to hash table", args);
                if (id != 0)
                {
                    args.Add("Id", id);
                }
                if (userId != string.Empty)
                {
                    args.Add("UserId", userId);
                }
                if (firstName != string.Empty)
                {
                    args.Add("FirstName", firstName);
                }
                if (lastName != string.Empty)
                {
                    args.Add("LastName", lastName);
                }
                if (email != string.Empty)
                {
                    args.Add("Email", email);
                }
                if (password != string.Empty)
                {
                    args.Add("Password", password);
                }
                if (lastLogin != null)
                {
                    args.Add("LastLogin", lastLogin);
                }
                if (isActive != null)
                {
                    args.Add("IsActive", isActive);
                }
                if (roleId != 0)
                {
                    args.Add("RoleId", roleId);
                }
                if (staffId != 0)
                {
                    args.Add("StaffId", staffId);
                }
                if (history != string.Empty)
                {
                    args.Add("History", history);
                }
                //Logger.Debug("UserDetailsDAO", "GetHashTable", "Added data to hash table");
                return args;
            }
            catch (Exception ex)
            {
               // Logger.Error("UserDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex);
                throw new Exception("11380");

            }

        }
    }

    public class UserDetail
    {
        public int CollegeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CollegeName { get; set; }
        public string LastLogin { get; set; }
        public string ConnectionString { get; set; }
        public string ReleaseFilePath { get; set; }
        public string PDFFolderPath { get; set; }
        public bool IsAuthenticated { get; set; }
        public string LogFilePath { get; set; }
        public string RoleType { get; set; }
        public string ApplicationType { get; set; }
        public string RedirectUrl { get; set; }
    }
}
