using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using AA.DAOBase;
using AA.LogManager;
using Util;
using System.Data;
using AA.Util;
using AA.ConfigurationsManager;
//using ITM.Exam.ConfigurationManager;

namespace AA.DAO
{
    public class FolderDetailsDAO
    {
        public string SELECT_FOLDERS = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,History,Alias,Permission, FolderType,EmailId FROM folderdetails";
        public string SELECT_CHILD_FOLDERS = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,History, Alias,Permission, FolderType,EmailId FROM folderdetails where ParentFolderId={0}";
        public string SELECT_SINGLE_FOLDER = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,History, Alias,Permission, FolderType,EmailId FROM folderdetails where Id={0}";
        public string INSERT_FOLDER = "INSERT INTO folderdetails (FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,History,Tags,Alias,Permission,FolderType,EmailId) VALUES ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}')";
        public string UPDATE_FOLDER = "UPDATE  folderdetails set FolderName = '{1}',FolderDescription='{2}',RelativePath='{3}',DateCreated='{4}',DateUpdated='{5}',OwnerUserId={6},IsActive={7},UserAccess='{8}',ParentFolderId={9},Tags='{10}',History='{11}', Alias='{12}',Permission='{13}' WHERE Id ={0}";
        public string DELETE_FOLDER = "DELETE FROM folderdetails WHERE Id ={0}";
        public string SELECT_FOLDER_ID = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,Alias,Permission,FolderType,EmailId FROM folderdetails WHERE Id={0}";

        public string UPDATE_DATEUPDATED = "Update folderdetails set DateUpdated='{0}', History='{1}' where Id='{2}'";

        public string Q_SearchFolders = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,History, Alias, Permission, FolderType,EmailId FROM folderdetails " +
                        "where FolderName like '%{0}%' or Alias like '%{0}%' or FolderDescription like '%{0}%' or Tags like '%{0}%' or OwnerUserId = (select id from userdetails where userid like '%{0}%')";

        public string SELECT_FOLDER_NAME = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,Alias,Permission, FolderType,EmailId FROM folderdetails WHERE FolderName='{0}'";

        public string UPDATE_FOLDER_ALIAS = "Update folderdetails set Alias = '{0}', History='{1}'  where Id='{2}'";
        public string COUNTFOR_FOLDER_CREATED = "SELECT count(*) as Count FROM folderdetails  where OwnerUserId='{0}'";
        public string COUNTFOR_FOLDER_SHARED = "SELECT count(*) as Count FROM folderdetails  where Shared=1";

        public string UPDATE_FOLDER_PARENTID = "Update folderdetails set ParentFolderId = {0}, History='{1}' where Id = {2} ";

        public string DELETE_USER_FOLDER = "update folderdetails set ParentFolderId = -2, IsActive = 0 WHERE ParentFolderId = -1 and OwnerUserId= (select id from users where username = '{0}')";

        public string SELECT_MAX_FOLDERID = "SELECT max(Id) as count FROM folderdetails";
        public string SelectAllChildCount = "Select Count(*) as Count from folderdetails where ParentFolderId={0}";

        public string Q_GetStudentList = "select Id, Alias from folderdetails ";
        public string Q_GetStudentDocuments = "select DocumentType from filedetails where ParentFolderId = {0} ";

        public string Q_FolderEmail = "SELECT Id, FolderName,FolderDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,IsActive,UserAccess,ParentFolderId,Tags,History, Alias, Permission, FolderType,EmailId FROM folderdetails where FolderType='{0}'";

        Logger logger = new Logger();

        public int GetFolderCount(int parentId, string cxnString, string logPath)
        {
            try
            {
                string cmd = string.Format(SelectAllChildCount, parentId);
                int count = 0;
                Database db = new Database();
                DbDataReader reader = db.Select(cmd, cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = int.Parse(reader["Count"].ToString());
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FolderDetailsDAO", "NoOfFolderShared", "Counting No of shared folders from database ", logPath);
                }
                Database.KillConnections();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetStudentUro(int parentId, string cxnString, string logPath)
        {
            try
            {
                int uro = GetFolderCount(parentId, cxnString, logPath);

                string parId = "0";
                string count = "0";

                if (uro >= 100)
                {
                    count = uro.ToString();
                }
                else if (uro >= 10 && uro <= 99)
                {
                    count = "0" + uro.ToString();
                }
                else if (uro >= 0 && uro <= 9)
                {
                    if (uro == 0) uro = uro + 1;
                    count = "00" + uro.ToString();
                }

                if (parentId >= 100)
                {
                    parId = parentId.ToString();
                }
                else if (parentId >= 10 && parentId <= 99)
                {
                    parId = "0" + parentId.ToString();
                }
                else if (parentId >= 0 && parentId <= 9)
                {
                    parId = "00" + parentId.ToString();
                }

                string uroNo = DateTime.Now.ToString("yyyy") + parId + count;

                for (int i = 0; i < 200; i++)
                {
                    FolderDetails foder = GetSingleFileDetails(uroNo, cxnString, logPath);
                    if (foder == null)
                    {
                        return uroNo;
                    }
                    else
                    {
                        uroNo = (int.Parse(uroNo) + 1).ToString();
                    }
                    Database.KillConnections();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "0";
        }

        public int FolderCreatedByCurrentUser(int UserId, string cxnString, string logPath)
        {
            try
            {
                string result = string.Format(COUNTFOR_FOLDER_CREATED, UserId);
                int count = 0;
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = int.Parse(reader["Count"].ToString());
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FolderDetailsDAO", "FolderCreatedByCurrentUser", "Counting No of folders from database ", logPath);

                }
                Database.KillConnections();
                return count;
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "FolderCreatedByCurrentUser", " Counting No of folders from database", ex, logPath);
                throw new Exception("11281");
            }

        }

        public int NoOfFolderShared(string cxnString, string logPath)
        {
            try
            {
                string result = COUNTFOR_FOLDER_SHARED;
                int count = 0;
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = int.Parse(reader["Count"].ToString());
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FolderDetailsDAO", "NoOfFolderShared", "Counting No of shared folders from database ", logPath);

                }
                Database.KillConnections();
                return count;
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "NoOfFolderShared", " Counting No of shared folders from database", ex, logPath);
                throw new Exception("11281");
            }

        }

        public FolderDetails CreateFolderDetails(string folderName, string folderDescription, string relativePath, DateTime dateCreated, DateTime dateUpdated, int ownerUserId, string userAccess, int parentFolderId, string history, string tags, string alias, string permission, string folderType, string email, string cxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(0, folderName, folderDescription, relativePath, dateCreated, dateUpdated, ownerUserId, userAccess, parentFolderId, history, tags, logPath);
                logger.Debug("FolderDetailsDAO", "CreateFolderDetails", "Inserting Folder Details", args);
                string result = string.Format(INSERT_FOLDER, ParameterFormater.FormatParameter(folderName), ParameterFormater.FormatParameter(folderDescription),
                    ParameterFormater.FormatParameter(relativePath), dateCreated.ToString("yyyy-MM-dd hh:mm:ss"), dateUpdated.ToString("yyyy-MM-dd hh:mm:ss"), ownerUserId, "1",
                    ParameterFormater.FormatParameter(userAccess), parentFolderId, ParameterFormater.FormatParameter(history), ParameterFormater.FormatParameter(tags),
                    ParameterFormater.FormatParameter(alias), ParameterFormater.FormatParameter(permission), ParameterFormater.FormatParameter(folderType), ParameterFormater.FormatParameter(email));
                Database db = new Database();
                db.Insert(result, cxnString, logPath);

                FolderDetails folderDetails = new FolderDetails();
                folderDetails.FolderName = folderName;

                folderDetails.OwnerUserId = ownerUserId;
                folderDetails.ParentFolderId = parentFolderId;
                folderDetails.RelativePath = relativePath;
                folderDetails.Tags = tags;
                folderDetails.UserAccess = userAccess;
                folderDetails.DateCreated = dateUpdated;
                folderDetails.DateUpdated = dateUpdated;
                folderDetails.FolderDescription = folderDescription;
                folderDetails.History = history;
                folderDetails.Permission = permission;

                logger.Debug("FolderDetailsDAO", "CreateFolderDetails", "Folder Details Saved", logPath);
                return folderDetails;
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "CreateFolderDetails", " Error occurred while Add Department", ex, logPath);
                throw new Exception("11276");
            }
        }

        public List<FolderDetails> GetAllFolders(string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetAllFolders", "Selecting Folder Details from database ", logPath);
                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_FOLDERS, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FolderDetails> folderList = new List<FolderDetails>();
                    while (reader.Read())
                    {
                        FolderDetails folder = new FolderDetails();
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();
                        //file.OriginalFileName = reader["OriginalFileName"].ToString();
                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                        folderList.Add(folder);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    Database.KillConnections();
                    logger.Debug("FolderDetailsDAO", "GetAllFiles", " returning File Details from database ", logPath);
                    return folderList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetAllFiles", " Error occurred while Get File Details", ex, logPath);
                throw new Exception("11277");
            }

            return null;

        }

        public List<FolderDetails> GetAllFolders(int folderId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetAllFolders", "Selecting Folder Details from database based of folder id=" + folderId, logPath);
                string result = string.Format(SELECT_FOLDER_ID, folderId);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FolderDetails> folderList = new List<FolderDetails>();
                    while (reader.Read())
                    {
                        FolderDetails folder = new FolderDetails();
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();

                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        //folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                        folderList.Add(folder);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    Database.KillConnections();
                    logger.Debug("FolderDetailsDAO", "GetAllFiles", " returning File Details from database based on folderid=" + folderId, logPath);
                    return folderList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetAllFiles", " Error occurred while Get File Details", ex, logPath);
                throw new Exception("11278");
            }

            return null;
        }

        public List<FolderDetails> GetAllChildFolders(int parentId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetAllChildFolders", "Selecting Child Folder Details from database based of folder", logPath);

                Database db = new Database();
                string cmd = string.Format(SELECT_CHILD_FOLDERS, parentId);
                DbDataReader reader = db.Select(cmd, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FolderDetails> folderList = new List<FolderDetails>();
                    while (reader.Read())
                    {
                        FolderDetails folder = new FolderDetails();
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();
                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                        folderList.Add(folder);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    Database.KillConnections();

                    logger.Debug("FolderDetailsDAO", "GetAllChildFolders", " returning Folder Details from database", logPath);
                    return folderList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetAllChildFolders", " Error occurred while Get folder Details", ex, logPath);
                throw new Exception("11278");
            }

            return null;

        }

        public FolderDetails GetSingleFileDetails(int folderId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetSingleFolderDetails", "Selecting Folder Details from database based on Folder id=" + folderId, logPath);
                string result = string.Format(SELECT_SINGLE_FOLDER, folderId);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    FolderDetails folder = new FolderDetails();
                    while (reader.Read())
                    {
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();
                        //folder.OriginalFileName = reader["OriginalFileName"].ToString();
                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    Database.KillConnections();
                    logger.Debug("FolderDetailsDAO", "GetSingleFolderDetails", " returning Folder Details from database based on Folder id=" + folderId, logPath);
                    return folder;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetSingleFolderDetails", " Error occurred while Get Single Folder Details", ex, logPath);
                throw new Exception("11279");
            }

            return null;
        }

        public FolderDetails GetSingleFileDetails(string folderName, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetSingleFolderDetails", "Selecting Folder Details from database based on Folder folderName=" + folderName, logPath);
                string result = string.Format(SELECT_FOLDER_NAME, folderName);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    FolderDetails folder = new FolderDetails();
                    while (reader.Read())
                    {
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();
                        //folder.OriginalFileName = reader["OriginalFileName"].ToString();
                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        //folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    Database.KillConnections();
                    logger.Debug("FolderDetailsDAO", "GetSingleFolderDetails", " returning Folder Details from database based on Folder id=" + folderName, logPath);
                    return folder;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetSingleFolderDetails", " Error occurred while Get Single Folder Details", ex, logPath);
                throw new Exception("11279");
            }

            return null;
        }

        public bool UpdateFileDetails(FolderDetails folderDetails, string cxnString, string logPath)
        {
            if (folderDetails == null)
            {
                logger.Error("FolderDetailsDAO", "UpdateFileDetails", " Error occurred while updating Single File Details because input argument was supplied null", logPath);
                return false;
            }

            try
            {
                if (folderDetails != null)
                {
                    Hashtable args = GetHashTable(folderDetails.Id, folderDetails.FolderName, folderDetails.FolderDescription, folderDetails.RelativePath, folderDetails.DateCreated, folderDetails.DateUpdated, folderDetails.OwnerUserId, folderDetails.UserAccess, folderDetails.ParentFolderId, folderDetails.History, folderDetails.Tags, logPath);
                    logger.Debug("FolderDetailsDAO", "UpdateFileDetails", "Updating Folder Details", args);

                    string result = string.Format(UPDATE_FOLDER, folderDetails.Id, ParameterFormater.FormatParameter(folderDetails.FolderName), ParameterFormater.FormatParameter(folderDetails.FolderDescription),
                        ParameterFormater.FormatParameter(folderDetails.RelativePath), folderDetails.DateCreated.ToString("yyyy-MM-dd hh:mm:ss"), folderDetails.DateUpdated.ToString("yyyy-MM-dd hh:mm:ss"), folderDetails.OwnerUserId, folderDetails.IsActive,
                        ParameterFormater.FormatParameter(folderDetails.UserAccess), folderDetails.ParentFolderId, ParameterFormater.FormatParameter(folderDetails.Tags),
                        ParameterFormater.FormatParameter(folderDetails.History), ParameterFormater.FormatParameter(folderDetails.Alias), ParameterFormater.FormatParameter(folderDetails.Permission));
                    Database db = new Database();
                    logger.Debug("FolderDetailsDAO", "UpdateFolderDetails", "Saving Folder Details Saved", logPath);
                    db.Update(result, cxnString, logPath);

                    logger.Debug("FolderDetailsDAO", "UpdateFolderDetails", "Folder Details Saved", logPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "UpdateFileDetails", " Error occurred while Update Department Master", ex, logPath);
                throw new Exception("11283");
            }
        }

        public bool RemoveFileDetails(int folderId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "RemoveFolderDetails", "Deleting Folder Details. folderid=" + folderId, logPath);

                string result = string.Format(DELETE_FOLDER, folderId);
                Database db = new Database();
                logger.Debug("FolderDetailsDAO", "RemoveFolderDetails", "Removing Folder Details", logPath);
                db.Delete(result, cxnString, logPath);

                logger.Debug("FolderDetailsDAO", "RemoveFolderDetails", "Folder Details Deleted", logPath);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "RemoveFolderDetails", " Error occurred while Removing File Detail", ex, logPath);
                throw new Exception("11284");
            }
        }


        // functions added by vasim
        public void UpdateModifyDate(int folderId, string history, string cxnString, string logPath)
        {
            try
            {
                if (folderId != 0)
                {
                    string result = string.Format(UPDATE_DATEUPDATED, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), history, folderId);
                    Database db = new Database();
                    db.Update(result, cxnString, logPath);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Update folder-Name (Alias) 
        /// </summary>
        /// <param name="folderId">Folderid</param>
        /// <param name="alias">Folder alias this is used to display in place of FolderName</param>
        public void UpdateFolderAlias(int folderId, string alias, string userName, string originalHistory, string cxnString, string logPath)
        {
            try
            {
                if (folderId != 0)
                {
                    /* Create Folder History */
                    FolderHistoryManager fHM = new FolderHistoryManager();
                    fHM.Date = DateTime.Now;
                    fHM.Activity = "Folder Renamed";
                    fHM.User = userName;
                    string history = FolderHistoryManager.AppendHistory(originalHistory, fHM);

                    string result = string.Format(UPDATE_FOLDER_ALIAS, alias, history, folderId);
                    Database db = new Database();
                    db.Update(result, cxnString, logPath);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<FolderDetails> GetFolderEmailForReminder(string type, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetFolderEmailForReminder", "Selecting Folder Details from database based of type", logPath);

                Database db = new Database();
                string cmd = string.Format(Q_FolderEmail, type);
                DbDataReader reader = db.Select(cmd, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FolderDetails> folderList = new List<FolderDetails>();
                    while (reader.Read())
                    {
                        FolderDetails folder = new FolderDetails();
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();

                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                        folderList.Add(folder);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    Database.KillConnections();
                    logger.Debug("FolderDetailsDAO", "GetFolderEmailForReminder", " returning Folder Details from database", logPath);
                    return folderList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetFolderDetailsForSearch", " Error occurred while Get folder Details", ex, logPath);
                throw new Exception("11278");
            }
            return null;
        }

        public List<FolderDetails> GetFolderDetailsForSearch(string searchText, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FolderDetailsDAO", "GetFolderDetailsForSearch", "Selecting Child Folder Details from database based of folder", logPath);

                Database db = new Database();
                string cmd = string.Format(Q_SearchFolders, searchText);
                DbDataReader reader = db.Select(cmd, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FolderDetails> folderList = new List<FolderDetails>();
                    while (reader.Read())
                    {
                        FolderDetails folder = new FolderDetails();
                        folder.Id = Convert.ToInt32(reader["Id"]);
                        folder.FolderName = reader["FolderName"].ToString();
                        folder.FolderDescription = reader["FolderDescription"].ToString();

                        folder.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        folder.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        folder.RelativePath = reader["RelativePath"].ToString();
                        folder.Tags = reader["Tags"].ToString();
                        folder.UserAccess = reader["UserAccess"].ToString();
                        folder.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        folder.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        folder.History = UnescapeFormat.UnescapeXML(reader["History"].ToString());
                        folder.Alias = reader["Alias"].ToString();
                        folder.Permission = reader["Permission"].ToString();
                        folder.FolderType = reader["FolderType"].ToString();//FolderType,EmailId
                        folder.Email = reader["EmailId"].ToString();
                        folderList.Add(folder);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    Database.KillConnections();
                    logger.Debug("FolderDetailsDAO", "GetFolderDetailsForSearch", " returning Folder Details from database", logPath);
                    return folderList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetFolderDetailsForSearch", " Error occurred while Get folder Details", ex, logPath);
                throw new Exception("11278");
            }

            return null;
        }

        public void UpdateParentFolderId(int parentId, int id, string originalHistory, string cnxnString, string logPath)
        {
            try
            {
                if (id != 0)
                {
                    Database db = new Database();
                    FolderHistoryManager manager = new FolderHistoryManager
                    {
                        Date = DateTime.Now,
                        Activity = "Folder Paste"
                    };
                    string str = FolderHistoryManager.AppendHistory(originalHistory, manager);
                    string str2 = string.Format(this.UPDATE_FOLDER_PARENTID, parentId, str, id);
                    db.Update(str2, cnxnString, logPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(string userName, string cnxnString, string logPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    Database db = new Database();
                    string str = string.Format(DELETE_USER_FOLDER, userName);
                    db.Update(str, cnxnString, logPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        //----------end-----------

        public int GetMaxFolderId(string cxnString, string logPath)
        {
            try
            {
                int maxCount = 0;
                string cmd = string.Format(SELECT_MAX_FOLDERID);

                Database db = new Database();
                DbDataReader reader = db.Select(cmd, cxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maxCount = int.Parse(reader["count"].ToString());
                    }
                }
                else
                {
                    maxCount = 1;
                }
                Database.KillConnections();
                return maxCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Database.KillConnections();
            }
        }

        public Hashtable GetHashTable(int id, string folderName, string folderDescription, string relativePath, DateTime dateCreated, DateTime dateUpdated, int ownerUserId, string userAccess, int parentFolderId, string history, string tags, string logPath)
        {
            try
            {
                Hashtable args = new Hashtable();
                logger.Debug("FolderDetailsDAO", "GetHashTable", "Adding data to hash table", logPath, args);
                if (id != 0)
                {
                    args.Add("Id", id);
                }

                if (!string.IsNullOrEmpty(folderName))
                {
                    args.Add("FolderName", folderName);
                }

                if (!string.IsNullOrEmpty(folderDescription))
                {
                    args.Add("FolderDescription", folderDescription);
                }

                if (!string.IsNullOrEmpty(relativePath))
                {
                    args.Add("RelativePath", relativePath);
                }

                if (dateCreated != null)
                {
                    args.Add("DateCreated", dateCreated.ToString());
                }

                if (dateUpdated != null)
                {
                    args.Add("DateUpdated", dateUpdated.ToString());
                }

                if (ownerUserId != 0)
                {
                    args.Add("OwnerUserId", ownerUserId);
                }

                if (!string.IsNullOrEmpty(userAccess))
                {
                    args.Add("UserAccess", userAccess);
                }

                if (parentFolderId != 0)
                {
                    args.Add("ParentFolderId", parentFolderId);
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    args.Add("Tags", tags);
                }

                if (history != string.Empty)
                {
                    args.Add("History", history);
                }

                logger.Debug("FolderDetailsDAO", "GetHashTable", "Added data to hash table", logPath);
                return args;
            }
            catch (Exception ex)
            {
                logger.Error("FolderDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11281");
            }
        }

        public DataTable GetDocumentReports(string cxnString, string logPath)
        {
            DataTable dt = new DataTable();
            List<Configurations.DocumentType> documentlist = Configurations.GetDocumnettypes();
            dt.Columns.Add("Sr.No.");
            dt.Columns.Add("Documents");
            foreach (Configurations.DocumentType doctype in documentlist)
            {
                dt.Columns.Add(doctype.Documents);
            }
            DbDataReader reader = new Database().Select(Q_GetStudentList, cxnString, logPath);

            try
            {
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        int Count = 1;
                        DataRow row;
                        while (reader.Read())
                        {
                            string birthDate = string.Empty;
                            List<string> str = new List<string>();
                            str.Add(Count.ToString());
                            str.Add(reader["Alias"].ToString());
                            Hashtable hTable = GetDocumentReports(int.Parse(reader["Id"].ToString()), cxnString, logPath);

                            foreach (Configurations.DocumentType doctype in documentlist)
                            {
                                if (hTable[doctype.Documents] != null)
                                    str.Add("Yes");
                                else
                                    str.Add("No");
                            }
                            Count++;
                            row = dt.NewRow();
                            row.ItemArray = str.ToArray();
                            dt.Rows.Add(row);
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }

                        Database.KillConnections();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.WriteLogEntry("Error in Caste_Wise_Report class GetAccountDetails method. :" + ex.Message);
                //LogManager.WriteLogEntry(ex.StackTrace);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                Database.KillConnections();
            }

            return dt;
        }

        public Hashtable GetDocumentReports(int folderId, string cxnString, string logPath)
        {
            Hashtable hTable = new Hashtable();
            string cmdText = string.Format(Q_GetStudentDocuments, folderId);
            DbDataReader reader = new Database().Select(cmdText, cxnString, logPath);

            try
            {
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            hTable.Add(reader["DocumentType"].ToString(), reader["DocumentType"].ToString());
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }

                        Database.KillConnections();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.WriteLogEntry("Error in Caste_Wise_Report class GetAccountDetails method. :" + ex.Message);
                //LogManager.WriteLogEntry(ex.StackTrace);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                Database.KillConnections();
            }

            return hTable;
        }


    }
}
