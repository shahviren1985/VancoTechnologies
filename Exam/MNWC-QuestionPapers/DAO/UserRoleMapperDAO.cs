using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using ITM.LogManager;
using ITM.Util;
using ITM.DAOBase;

namespace ITM.DAO
{
    public class UserRoleMapperDAO
    {
        Logger logger = new Logger();

        public string SELECT_USERROLE_MAPPER_BY_ID = "SELECT Id,UserId,RoleId FROM UserRoleMapper where Id={0}";
        public string SELECT_USERROLE_MAPPER = "SELECT Id,UserId,RoleId FROM UserRoleMapper";
        public string INSERT_USERROLE_MAPPER = "INSERT INTO UserRoleMapper (UserId,RoleId) VALUES ({0},{1})";
        public string UPDATE_USERROLE_MAPPER = "UPDATE  UserRoleMapper set UserId = {1}, RoleId = {2}WHERE Id ={0}";
        public string DELETE_USERROLE_MAPPER = "DELETE FROM UserRoleMapper WHERE Id ={0}";
        public string GET_USERROLE_BY_USERID = "SELECT Id,UserId,RoleId FROM UserRoleMapper WHERE UserId ={0}";
        #region Insert
        public UserRoleMapper CreateUserRoleMapper(int userId, int roleId, string cnxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(0, userId, roleId, logPath);
                logger.Debug("UserRoleMapperDAO", "CreateUserRoleMapper", "Inserting UserRoleMapper Details", args, logPath);
                string createuser = string.Format(INSERT_USERROLE_MAPPER, userId, roleId);
                Database db = new Database();
                db.Insert(createuser, cnxnString, logPath);
                UserRoleMapper userrolemapper = new UserRoleMapper();
                userrolemapper.UserId = userId;
                userrolemapper.RoleId = roleId;
                logger.Debug("UserRoleMapperDAO", "CreateUserRoleMapper", " UserRoleMapper Details Saved", logPath);
                return userrolemapper;
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "CreateUserRoleMapper", " Error occurred while Adding UserRoleMapper", ex, logPath);
                throw new Exception("11451");
            }
        }
        #endregion
        #region Select
        public List<UserRoleMapper> GetUserRoleMapperList(string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", "Selecting UserRoleMapper from database", logPath);
                string userlist = string.Format(SELECT_USERROLE_MAPPER);
                Database db = new Database();
                DbDataReader reader = db.Select(userlist, cnxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserRoleMapper> userrolemapperlist = new List<UserRoleMapper>();
                    while (reader.Read())
                    {
                        UserRoleMapper userrolemapper = new UserRoleMapper();
                        userrolemapper.Id = Convert.ToInt32(reader["Id"]);
                        userrolemapper.UserId = Convert.ToInt32(reader["UserId"]);
                        userrolemapper.RoleId = Convert.ToInt32(reader["RoleId"]);
                        userrolemapperlist.Add(userrolemapper);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", " returning UserRoleMapper from database", logPath);
                    return userrolemapperlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "GetUserRoleMapperList", " Error occurred while Getting UserRoleMapper list", ex, logPath);
                throw new Exception("11452");
            }
            return null;
        }
        public List<UserRoleMapper> GetUserRoleMapperList(int userid, string cnxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(0, userid, 0, logPath);
                logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", "Selecting UserRoleMapper from database by user id ", logPath);
                string userlist = string.Format(GET_USERROLE_BY_USERID, userid);
                Database db = new Database();
                DbDataReader reader = db.Select(userlist, cnxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserRoleMapper> userrolemapperlist = new List<UserRoleMapper>();
                    while (reader.Read())
                    {
                        UserRoleMapper userrolemapper = new UserRoleMapper();
                        userrolemapper.Id = Convert.ToInt32(reader["Id"]);
                        userrolemapper.UserId = Convert.ToInt32(reader["UserId"]);
                        userrolemapper.RoleId = Convert.ToInt32(reader["RoleId"]);
                        userrolemapperlist.Add(userrolemapper);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", " returning UserRoleMapper from database by userid ", logPath);
                    return userrolemapperlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "GetUserRoleMapperList", " Error occurred while Getting UserRoleMapper list by userid", ex, logPath);
                throw new Exception("11457");
            }
            return null;
        }
        public UserRoleMapper GetUserRoleMapperListById(int id, string cnxnString, string logPath)
        {
            UserRoleMapper userrolemapper = null;
            try
            {
                Hashtable args = GetHashTable(id, 0, 0, logPath);
                logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", "Selecting UserRoleMapper from database ", args, logPath);
                string user = string.Format(SELECT_USERROLE_MAPPER_BY_ID, id);
                Database db = new Database();
                DbDataReader reader = db.Select(user, cnxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserRoleMapper> userrolemapperlist = new List<UserRoleMapper>();
                    while (reader.Read())
                    {
                        userrolemapper = new UserRoleMapper();
                        userrolemapper.Id = Convert.ToInt32(reader["Id"]);
                        userrolemapper.UserId = Convert.ToInt32(reader["UserId"]);
                        userrolemapper.RoleId = Convert.ToInt32(reader["RoleId"]);
                        //return userrolemapper;
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", " returning UserRoleMapper from database", logPath);
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "GetUserRoleMapperList", " Error occurred while Getting UserRoleMapper list", ex, logPath);
                throw new Exception("11456");
            }

            return userrolemapper;
        }
        #endregion
        public bool RemoveUserRoleMapper(int id, string cnxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(id, 0, 0, logPath);
                logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", " Delete UserRoleMapper from database by Id", args, logPath);
                string removeuser = string.Format(DELETE_USERROLE_MAPPER, id);
                Database db = new Database();
                logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", "Saving Changes in UserRoleMapper Details", logPath);
                db.Delete(removeuser, cnxnString, logPath);
                logger.Debug("UserRoleMapperDAO", "GetUserRoleMapperList", "Saved Changes in UserRoleMapper Details", logPath);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "GetUserRoleMapperList", " Error occurred while Delete UserRoleMapper", ex, logPath);
                throw new Exception("11453");
            }
        }
        public bool ChangeUserRoleMapper(UserRoleMapper userrolemapper, string cnxnString, string logPath)
        {
            try
            {
                if (userrolemapper != null)
                {
                    Hashtable args = GetHashTable(userrolemapper.Id, userrolemapper.UserId, userrolemapper.RoleId, logPath);
                    logger.Debug("UserRoleMapperDAO", "ChangeUserRoleMapper", "Updating User rolemapper", args, logPath);
                    string updateuser = string.Format(UPDATE_USERROLE_MAPPER, userrolemapper.Id, userrolemapper.UserId, userrolemapper.RoleId);
                    Database db = new Database();
                    logger.Debug("UserRoleMapperDAO", "ChangeUserRoleMapper", "Saving User RoleMapper Saved", logPath);
                    db.Update(updateuser, cnxnString, logPath);
                    logger.Debug("UserRoleMapperDAO", "ChangeUserRoleMapper", "User RoleMapper Saved", logPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "ChangeUserRoleMapper", " Error occurred while Update User RoleMapper", ex, logPath);
                throw new Exception("11454");
            }
        }
        public int CreateUserRoleMapperHistory(int id, int userId, int roleId, string logPath)
        {
            try
            {
                return userId;
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "CreateUserRoleMapperHistory", " Error occurred while Creating User RoleMapper History", ex, logPath);
                throw new Exception("11458");
            }

        }

        public int UpdateUserRoleMapperHistory(int idOld, int userIdOld, int roleIdOld, int idNew, int userIdNew, int roleIdNew, string logPath)
        {
            try
            {
                return roleIdNew;
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "UpdateUserRoleMapperHistory", " Error occurred while Updating User RoleMapper History", ex, logPath);
                throw new Exception("11459");
            }


        }
        public string AppendUserRoleMapperHistory(string originalXml, string logPath)
        {
            try
            {
                return originalXml;
            }
            catch (Exception ex)
            {
                logger.Error("UserRoleMapperDAO", "AppendUserRoleMapperHistory", " Error occurred while Appending User RoleMapper History", ex, logPath);
                throw new Exception("11460");
            }
        }
        public Hashtable GetHashTable(int id, int userId, int roleId, string logPath)
        {
            try
            {
                Hashtable args = new Hashtable();
                logger.Debug("RoleDetailsDAO", "GetHashTable", "Adding data to hash table", args, logPath);
                if (id != 0)
                {
                    args.Add("Id", id);
                }
                if (userId != 0)
                {
                    args.Add("UserId", userId);
                }
                if (roleId != 0)
                {
                    args.Add("RoleId", roleId);
                }
                logger.Debug("RoleDetailsDAO", "GetHashTable", "Added data to hash table", logPath);
                return args;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11455");
            }
        }
    }
}
