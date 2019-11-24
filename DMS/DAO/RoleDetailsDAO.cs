using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using AA.LogManager;
using Util;
using AA.DAOBase;
using System.Data;
using AA.Util;

namespace AA.DAO
{
    public class RoleDetailsDAO
    {
        //public string SELECT_ROLE_DETAILS = "SELECT RoleId,RoleName FROM Role";
        public string INSERT_ROLE_DETAILS = "INSERT INTO Role (Name) VALUES ('{0}')";
        public string UPDATE_ROLE_DETAILS = "UPDATE  Role set Name = '{1}' WHERE Id ={0}";
        public string DELETE_ROLE_DETAILS = "DELETE FROM Role WHERE Id ={0}";
        public string GET_ROLE_BY_ID = "SELECT Id,Name FROM Role WHERE Id ={0}";

        public string SELECT_ROLE_DETAILS = "SELECT Id, Name FROM role";
        public string SELECT_ROLE_DETAILS_BY_ID = "SELECT Id,Name FROM role Where id={0}";


        Logger logger = new Logger();
        #region Insert
        public RoleDetails CreateRole(string roleName, string cxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(0, roleName, logPath);
                logger.Debug("RoleDetailsDAO", "CreateRole", "Inserting Role", args);
                string result = string.Format(INSERT_ROLE_DETAILS, ParameterFormater.FormatParameter(roleName));
                Database db = new Database();
                db.Insert(result, cxnString, logPath);
                RoleDetails role = new RoleDetails();
                role.Name = roleName;
                logger.Debug("RoleDetailsDAO", "CreateRole", " Role Details Saved", string.Empty);
                return role;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "CreateRole", " Error occurred while Adding role", ex, logPath);
                throw new Exception("11401");
            }
        }
        #endregion
        #region Select
        public List<RoleDetails> GetRoleDetails(string cxnString, string logPath)
        {
            List<RoleDetails> list = new List<RoleDetails>();
            try
            {
                this.logger.Debug("RoleDetailsDAO", "GetRoleDetails", " Getting role by id", logPath);
                DbDataReader reader = new Database().Select(this.SELECT_ROLE_DETAILS, cxnString, logPath);
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    RoleDetails item = new RoleDetails
                    {
                        Name = reader["Name"].ToString(),
                        Id = int.Parse(reader["Id"].ToString())
                    };
                    list.Add(item);
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("RoleDetailsDAO", "GetRoleDetails", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return list;
        }

        public RoleDetails GetRoleDetails(int roleId, string cxnString, string logPath)
        {
            RoleDetails details = new RoleDetails();
            try
            {
                this.logger.Debug("RoleDetailsDAO", "GetRoleDetails", " Getting role by id for " + roleId, logPath);
                string str = string.Format(this.SELECT_ROLE_DETAILS_BY_ID, roleId);
                DbDataReader reader = new Database().Select(str, cxnString, logPath);
                if (!reader.HasRows)
                {
                    return details;
                }
                while (reader.Read())
                {
                    if (int.Parse(reader["count"].ToString()) == 0)
                    {
                        this.logger.Debug("RoleDetailsDAO", "GetRoleDetails", " Unable to get roles", logPath);
                    }
                    else
                    {
                        this.logger.Debug("UserDetailsDAO", "IsAuthenticated", " User  Authenticated ", logPath);
                        details.Name = reader["Name"].ToString();
                        details.Id = int.Parse(reader["Id"].ToString());
                    }
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("RoleDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return details;
        }
        public RoleDetails GetRoleListById(int id, string cxnString, string logPath)
        {
            RoleDetails role = null;
            try
            {
                Hashtable args = GetHashTable(id, string.Empty, logPath);
                logger.Debug("RoleDetailsDAO", "GetRoleListById", "Selecting Role from database ", logPath, args);
                string result = string.Format(GET_ROLE_BY_ID, id);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        role = new RoleDetails();
                        role.Id = Convert.ToInt32(reader["Id"]);
                        role.Name = reader["Name"].ToString();
                        logger.Debug("RoleDetailsDAO", "GetRoleListById", " returning Roles from database ", logPath);
                        //return role;
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "GetRoleListById", " Error occurred while Getting Role list", ex, logPath);
                throw new Exception("11406");
            }

            return role;
        }
        public List<RoleDetails> GetRoleList1(string cxnString, string logPath)
        {
            try
            {
                logger.Debug("RoleDetailsDAO", "GetRoleList", "Selecting Role from database ", logPath);
                string result = string.Format(SELECT_ROLE_DETAILS);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<RoleDetails> rolelist = new List<RoleDetails>();
                    while (reader.Read())
                    {
                        RoleDetails role = new RoleDetails();
                        role.Id = Convert.ToInt32(reader["Id"]);
                        role.Name = reader["Name"].ToString();
                        rolelist.Add(role);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    logger.Debug("RoleDetailsDAO", "GetRoleList", " returning Roles from database ", logPath);
                    return rolelist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "GetRoleList", " Error occurred while Getting Role list", ex, logPath);
                throw new Exception("11402");
            }

            return null;
        }
        public DataTable GetRoleListforgrid(string cxnString, string logPath)
        {
            try
            {
                logger.Debug("RoleDetailsDAO", "GetRoleListforgrid", "Getting Role list for gridview", logPath);
                logger.Debug("RoleDetailsDAO", "GetRoleListforgrid", "Selecting Role form database", logPath);
                string result = string.Format(SELECT_ROLE_DETAILS);
                Database db = new Database();
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Name");
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    DataRow row;
                    while (reader.Read())
                    {
                        List<string> str = new List<string>();
                        str.Add(reader["Id"].ToString());
                        str.Add(reader["Name"].ToString());

                        row = dt.NewRow();
                        row.ItemArray = str.ToArray();
                        dt.Rows.Add(row);

                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    logger.Debug("RoleDetailsDAO", "GetRoleListforgrid", "Returning Role form database", logPath);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "GetRoleListforgrid", "Error occurred while Getting role list for gridview", ex, logPath);
                throw new Exception("11407");
            }

            return null;
        }
        #endregion

        #region DELETE
        public bool RemoveRole(int id, string cxnString, string logPath)
        {
            try
            {

                Hashtable args = GetHashTable(id, string.Empty, logPath);
                logger.Debug("RoleDetailsDAO", "RemoveRole", " Delete Role from database by Id", args);
                string removeRole = string.Format(DELETE_ROLE_DETAILS, id);
                Database db = new Database();
                logger.Debug("RoleDetailsDAO", "RemoveRole", "Removing Role", logPath);
                db.Delete(removeRole, cxnString, logPath);
                logger.Debug("RoleDetailsDAO", "RemoveRole", "Removed Role", logPath);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "RemoveRole", " Error occurred while Deleting Role", ex, logPath);
                throw new Exception("11403");
            }
        }
        #endregion

        #region Update
        public bool ChangeRole(RoleDetails role, string cxnString, string logPath)
        {
            try
            {
                if (role != null)
                {
                    Hashtable args = GetHashTable(role.Id, role.Name, logPath);
                    logger.Debug("RoleDetailsDAO", "ChangeRole", "Updating Role", args);
                    string updateRole = string.Format(UPDATE_ROLE_DETAILS, role.Id, ParameterFormater.FormatParameter(role.Name));
                    Database db = new Database();
                    logger.Debug("RoleDetailsDAO", "ChangeRole", "Saving Role Saved", logPath);
                    db.Update(updateRole, cxnString, logPath);

                    logger.Debug("RoleDetailsDAO", "ChangeRole", "Roles Saved", logPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "ChangeRole", " Error occurred while Updating Roles", ex, logPath);
                throw new Exception("11404");
            }
        }
        #endregion

        #region History Related Method
        public string CreateRoleHistory(int id, string rolename, string logPath)
        {
            try
            {
                return rolename;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "CreateRoleHistory", "Error occurred while Creating Role History", ex, logPath);
                throw new Exception("11408");
            }
        }

        public string UpdateRoleHistory(int idOld, string rolenameOld, int idNew, string rolenameNew, string logPath)
        {
            try
            {
                return rolenameNew;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "UpdateRoleHistory", "Error occurred while Updating Role History", ex, logPath);
                throw new Exception("11409");
            }

        }
        public string AppendUserHistory(string originalXml, string logPath)
        {
            try
            {
                return originalXml;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "UpdateRoleHistory", "Error occurred while Appending Role History", ex, logPath);
                throw new Exception("11410");
            }
        }
        public Hashtable GetHashTable(int id, string roleName, string logPath)
        {
            try
            {
                Hashtable args = new Hashtable();
                logger.Debug("RoleDetailsDAO", "GetHashTable", "Adding data to hash table", args);
                if (id != 0)
                {
                    args.Add("RoleId", id);
                }
                if (roleName != string.Empty)
                {
                    args.Add("Name", roleName);
                }

                logger.Debug("RoleDetailsDAO", "GetHashTable", "Added data to hash table", logPath);
                return args;
            }
            catch (Exception ex)
            {
                logger.Error("RoleDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11405");

            }

        }
        #endregion
    }
}
