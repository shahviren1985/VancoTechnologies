using System;
using System.Collections.Generic;
using System.Collections;
using AA.LogManager;
using AA.DAOBase;
using Util;
using System.Data.Common;
using System.IO;
using System.Web;
using AA.Util;
using System.Configuration;

namespace AA.DAO
{
    public class FileDetailsDAO
    {
        public string SELECT_FILE = "SELECT Id, FileName,OriginalFileName,FileDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,UserAccess,ParentFolderId,Tags,Permission,DocumentType FROM filedetails";
        public string SELECT_FILE_FOR_FOLDER = "SELECT Id, FileName,OriginalFileName,FileDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,UserAccess,ParentFolderId,Tags, History,Permission,DocumentType FROM filedetails where ParentFolderId={0} order by DateCreated DESC";
        public string SELECT_SINGLE_FILE = "SELECT Id, FileName,OriginalFileName,FileDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,UserAccess,ParentFolderId,Tags, History,Permission,DocumentType FROM filedetails where Id={0}";

        public string INSERT_FILE = "INSERT INTO filedetails (FileName,OriginalFileName,FileDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,UserAccess,ParentFolderId,History,Tags, Permission,DocumentType) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},'{9}','{10}','{11}','{12}')";
        public string UPDATE_FILE = "UPDATE  filedetails set FileName = '{1}',OriginalFileName = '{2}',FileDescription = '{3}',RelativePath='{4}',DateCreated='{5}',DateUpdated='{6}',OwnerUserId={7},UserAccess='{8}',ParentFolderId={9},Tags='{10}',History='{11}',Permission='{12}',DocumentType='{13}' WHERE Id ={0}";
        public string DELETE_FILE = "DELETE FROM filedetails WHERE Id ={0}";
        public string SELECT_FILE_ID = "SELECT Id, FileName,OriginalFileName,FileDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,UserAccess,ParentFolderId,Tags, History, Permission FROM filedetails WHERE Id={0}";

        public string Q_SearchFiles = "SELECT Id, FileName,OriginalFileName,FileDescription,RelativePath,DateCreated,DateUpdated,OwnerUserId,UserAccess,ParentFolderId,Tags, History, Permission,DocumentType FROM filedetails " +
                            "where OriginalFileName like '%{0}%' or FileDescription like '%{0}%' or Tags like '%{0}%' or OwnerUserId in (select id from userdetails where userid like '%{0}%')";
        public string UPDATE_FILE_PARENT = "Update filedetails set ParentFolderId = {0}, History='{1}' where Id = {2} ";
        public string UPDATE_FILE_NAME = "UPDATE  filedetails set OriginalFileName = '{0}', History='{1}' WHERE Id ={2}";
        public string COUNTFORFILESCREATED = "SELECT count(*) as Count FROM filedetails  where OwnerUserId='{1}'";
        public string COUNTFOR_FILES_SHARED = "SELECT count(*) as Count FROM filedetails  where Shared=1";
        public string CURRENT_USER_FILE_NAMES = "SELECT Id,FileName,FileDescription,OriginalFileName,OwnerUserId,ParentFolderId FROM filedetails  where OwnerUserId={0}";

        public string SELECT_MAX_FILEID = "SELECT max(Id) as count FROM filedetails";

        public string SELECT_FILE_EXIST = "SELECT Id FROM filedetails where FileName = '{0}'";
        Logger logger = new Logger();
        private Database db;

        public FileDetailsDAO()
        {
            this.db = new Database();

        }

        public FileDetails CreateFileDetails(string fileName, string originalFileName, string fileDescription, string relativePath, DateTime dateCreated, DateTime dateUpdated, int ownerUserId, string userAccess, int parentFolderId, string history, string tags, string permission, string doctype, string cxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(0, fileName, originalFileName, fileDescription, relativePath, dateCreated, dateUpdated, ownerUserId, userAccess, parentFolderId, history, tags, doctype, logPath);
                logger.Debug("FileDetailsDAO", "CreateFileDetails", "Inserting File Details", args);
                string result = string.Format(INSERT_FILE, ParameterFormater.FormatParameter(fileName), ParameterFormater.FormatParameter(originalFileName),
                    ParameterFormater.FormatParameter(fileDescription), ParameterFormater.FormatParameter(relativePath), dateCreated.ToString("yyyy/MM/dd hh:mm:ss.fff"),
                    dateUpdated.ToString("yyyy/MM/dd hh:mm:ss.fff"), ownerUserId, ParameterFormater.FormatParameter(userAccess), parentFolderId,
                    ParameterFormater.FormatParameter(history), ParameterFormater.FormatParameter(tags), permission, ParameterFormater.FormatParameter(doctype));
                Database db = new Database();
                db.Insert(result, cxnString, logPath);

                FileDetails fileDetails = new FileDetails();
                fileDetails.FileName = fileName;
                fileDetails.OriginalFileName = originalFileName;
                fileDetails.OwnerUserId = ownerUserId;
                fileDetails.ParentFolderId = parentFolderId;
                fileDetails.RelativePath = relativePath;
                fileDetails.Tags = tags;
                fileDetails.UserAccess = userAccess;
                fileDetails.DateCreated = dateUpdated;
                fileDetails.DateUpdated = dateUpdated;
                fileDetails.FileDescription = fileDescription;
                fileDetails.History = history;
                fileDetails.Permission = permission;
                fileDetails.DocumentType = doctype;

                logger.Debug("FileDetailsDAO", "CreateCourse", "Department Details Saved", logPath);
                return fileDetails;
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "CreateCourse", " Error occurred while Add Department", ex, logPath);
                throw new Exception("11276");
            }
            finally
            {
                Database.KillConnections();
            }
        }

        public List<FileDetails> GetAllFiles(string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FileDetailsDAO", "GetAllFiles", "Selecting File Details from database ", logPath);
                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_FILE, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FileDetails> fileList = new List<FileDetails>();
                    while (reader.Read())
                    {
                        FileDetails file = new FileDetails();
                        file.Id = Convert.ToInt32(reader["Id"]);
                        file.FileName = reader["FileName"].ToString();
                        file.FileDescription = reader["FileDescription"].ToString();
                        file.OriginalFileName = reader["OriginalFileName"].ToString();
                        file.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        file.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        file.RelativePath = reader["RelativePath"].ToString();
                        file.Tags = reader["Tags"].ToString();
                        file.UserAccess = reader["UserAccess"].ToString();
                        file.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        file.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        file.History = reader["History"].ToString();
                        file.Permission = reader["Permission"].ToString();
                        file.DocumentType = reader["DocumentType"].ToString();
                        fileList.Add(file);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FileDetailsDAO", "GetAllFiles", " returning File Details from database ", logPath);
                    return fileList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetAllFiles", " Error occurred while Get File Details", ex, logPath);
                throw new Exception("11277");
            }
            finally
            {
                Database.KillConnections();
            }
            return null;

        }

        public List<FileDetails> GetAllFiles(int folderId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FileDetailsDAO", "GetAllFiles", "Selecting File Details from database based of folder id=" + folderId, logPath);
                string result = string.Format(SELECT_FILE_FOR_FOLDER, folderId);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FileDetails> fileList = new List<FileDetails>();
                    while (reader.Read())
                    {
                        FileDetails file = new FileDetails();
                        file.Id = Convert.ToInt32(reader["Id"]);
                        file.FileName = reader["FileName"].ToString();
                        file.FileDescription = reader["FileDescription"].ToString();
                        file.OriginalFileName = reader["OriginalFileName"].ToString();
                        file.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        file.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        file.RelativePath = reader["RelativePath"].ToString();
                        file.Tags = reader["Tags"].ToString();
                        file.UserAccess = reader["UserAccess"].ToString();
                        file.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        file.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        file.Permission = reader["Permission"].ToString();
                        file.History = reader["History"].ToString();
                        file.DocumentType = reader["DocumentType"].ToString();
                        fileList.Add(file);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FileDetailsDAO", "GetAllFiles", " returning File Details from database based on folderid=" + folderId, logPath);
                    return fileList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetAllFiles", " Error occurred while Get File Details", ex, logPath);
                throw new Exception("11278");
            }
            finally
            {
                Database.KillConnections();
            }
            return null;
        }

        public FileDetails GetSingleFileDetails(int fileID, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FileDetailsDAO", "GetSingleFileDetails", "Selecting File Details from database based on file id=" + fileID, logPath);
                string result = string.Format(SELECT_SINGLE_FILE, fileID);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    FileDetails file = new FileDetails();
                    while (reader.Read())
                    {
                        file.Id = Convert.ToInt32(reader["Id"]);
                        file.FileName = reader["FileName"].ToString();
                        file.FileDescription = reader["FileDescription"].ToString();
                        file.OriginalFileName = reader["OriginalFileName"].ToString();
                        file.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        file.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        file.RelativePath = reader["RelativePath"].ToString();
                        file.Tags = reader["Tags"].ToString();
                        file.UserAccess = reader["UserAccess"].ToString();
                        file.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        file.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        file.Permission = reader["Permission"].ToString();
                        file.History = reader["History"].ToString();
                        file.DocumentType = reader["DocumentType"].ToString();
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FileDetailsDAO", "GetSingleFileDetails", " returning File Details from database based on file id=" + fileID, logPath);
                    return file;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetSingleFileDetails", " Error occurred while Get Single File Details", ex, logPath);
                throw new Exception("11279");
            }
            finally
            {
                Database.KillConnections();
            }

            return null;
        }

        public bool GetSingleFileExist(string fileName, string cxnString, string logPath)
        {
            try
            {
                bool isExist = false;
                logger.Debug("FileDetailsDAO", "GetSingleFileExist", "Selecting File existance from database based on file name=" + fileName, logPath);
                string result = string.Format(SELECT_FILE_EXIST, fileName);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            isExist = true;
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }

                    logger.Debug("FileDetailsDAO", "GetSingleFileExist", " returning File existance from database based on file id=" + fileName, logPath);
                    return isExist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetSingleFileExist", " Error occurred while Get Single File existance", ex, logPath);
                throw new Exception("11279");
            }
            finally
            {
                Database.KillConnections();
            }

            return false;
        }

        public bool UpdateFileDetails(FileDetails fileDetails, string cxnString, string logPath)
        {
            if (fileDetails == null)
            {
                logger.Error("FileDetailsDAO", "UpdateFileDetails", " Error occurred while updating Single File Details because input argument was supplied null", logPath);
                return false;
            }

            try
            {
                if (fileDetails != null)
                {
                    Hashtable args = GetHashTable(fileDetails.Id, fileDetails.FileName, fileDetails.OriginalFileName, fileDetails.FileDescription, fileDetails.RelativePath, fileDetails.DateCreated, fileDetails.DateUpdated, fileDetails.OwnerUserId, fileDetails.UserAccess, fileDetails.ParentFolderId, fileDetails.History, fileDetails.Tags, fileDetails.DocumentType, logPath);
                    logger.Debug("FileDetailsDAO", "UpdateFileDetails", "Updating File Details", args);
                    //UPDATE  filedetails set FileName = '{1}',                                                                         OriginalFileName = '{2}',                                                       FileDescription = '{3}',RelativePath='{4}',DateCreated='{5}',DateUpdated='{6}',OwnerUserId={7},UserAccess='{8}',ParentFolderId={9},Tags='{10}' WHERE Id ={0}"
                    string result = string.Format(UPDATE_FILE, fileDetails.Id, ParameterFormater.FormatParameter(fileDetails.FileName), ParameterFormater.FormatParameter(fileDetails.OriginalFileName),
                        ParameterFormater.FormatParameter(fileDetails.FileDescription), ParameterFormater.FormatParameter(fileDetails.RelativePath), fileDetails.DateCreated,
                        fileDetails.DateUpdated, fileDetails.OwnerUserId, ParameterFormater.FormatParameter(fileDetails.UserAccess), fileDetails.ParentFolderId,
                        ParameterFormater.FormatParameter(fileDetails.Tags), ParameterFormater.FormatParameter(fileDetails.History), fileDetails.Permission, ParameterFormater.FormatParameter(fileDetails.DocumentType));
                    Database db = new Database();
                    logger.Debug("FileDetailsDAO", "UpdateFileDetails", "Saving File Details Saved", logPath);
                    db.Update(result, cxnString, logPath);

                    logger.Debug("FileDetailsDAO", "UpdateFileDetails", "File Details Saved", logPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "UpdateFileDetails", " Error occurred while Update Department Master", ex, logPath);
                throw new Exception("11283");
            }
            finally
            {
                Database.KillConnections();
            }
        }

        public bool RemoveFileDetails(int fileId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("FileDetailsDAO", "RemoveFileDetails", "Deleting File Details. FileId=" + fileId, logPath);

                string result = string.Format(DELETE_FILE, fileId);
                Database db = new Database();
                logger.Debug("FileDetailsDAO", "RemoveFileDetails", "Removing File Details", logPath);
                db.Delete(result, cxnString, logPath);

                logger.Debug("FileDetailsDAO", "RemoveFileDetails", "File Details Deleted", logPath);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "RemoveFileDetails", " Error occurred while Removing File Detail", ex, logPath);
                throw new Exception("11284");
            }
            finally
            {
                Database.KillConnections();
            }
        }

        /// <summary>
        /// Update the name of the file for display purpuse
        /// </summary>
        /// <param name="fileId">file id</param>
        /// <param name="fileName">name of the file</param>
        public void UpdateFileName(int fileId, string fileName, string userName, string previousHistory, string cxnString, string logPath)
        {
            try
            {

                //string history = FolderHistoryManager.CreateHistory(fHM);
                FolderHistoryManager fHM = new FolderHistoryManager();
                fHM.Date = DateTime.Now;
                fHM.Activity = "File Renamed";
                fHM.User = userName;
                string history = FolderHistoryManager.AppendHistory(previousHistory, fHM);
                string cmd = string.Format(UPDATE_FILE_NAME, fileName, history, fileId);
                Database db = new Database();
                db.Update(cmd, cxnString, logPath);
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

        /// <summary>
        /// Get List of FileDetails object according to the search text
        /// </summary>
        /// <param name="searchText">This is text which is used to filtered files</param>
        /// <returns>List of FileDetails</returns>
        public List<FileDetails> GetFileDetailsForSearch(string searchText, string cxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_SearchFiles, searchText);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FileDetails> fileList = new List<FileDetails>();
                    while (reader.Read())
                    {
                        FileDetails file = new FileDetails();
                        file.Id = Convert.ToInt32(reader["Id"]);
                        file.FileName = reader["FileName"].ToString();
                        file.FileDescription = reader["FileDescription"].ToString();
                        file.OriginalFileName = reader["OriginalFileName"].ToString();
                        file.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        file.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        file.RelativePath = reader["RelativePath"].ToString();
                        file.Tags = reader["Tags"].ToString();
                        file.UserAccess = reader["UserAccess"].ToString();
                        file.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        file.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        file.Permission = reader["Permission"].ToString();
                        file.History = reader["History"].ToString();
                        file.DocumentType = reader["DocumentType"].ToString();
                        fileList.Add(file);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("FileDetailsDAO", "GetFileDetailsForSearch", " returning File Details from database based on search text=" + searchText, logPath);
                    return fileList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetFileDetailsForSearch", " Error occurred while Get File Details", ex, logPath);
                throw new Exception("11278");
            }
            finally
            {
                Database.KillConnections();
            }
            return null;
        }

        public void UpdateParentFolderId(int parentId, int id, string originalHistory, string cxnString, string logPath)
        {
            try
            {
                if (id != 0)
                {
                    FolderHistoryManager fHM = new FolderHistoryManager();
                    fHM.Date = DateTime.Now;
                    fHM.Activity = "Folder Paste";
                    // fHM.User = userName;
                    string history = FolderHistoryManager.AppendHistory(originalHistory, fHM);

                    string result = string.Format(UPDATE_FILE_PARENT, parentId, history, id);
                    Database db = new Database();
                    db.Update(result, cxnString, logPath);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Database.KillConnections();
            }
        }


        public Hashtable GetHashTable(int id, string fileName, string originalFileName, string fileDescription, string relativePath, DateTime dateCreated, DateTime dateUpdated, int ownerUserId, string userAccess, int parentFolderId, string history, string tags, string doctype, string logPath)
        {
            try
            {
                Hashtable args = new Hashtable();
                logger.Debug("FileDetailsDAO", "GetHashTable", "Adding data to hash table", args);
                if (id != 0)
                {
                    args.Add("Id", id);
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    args.Add("FileName", fileName);
                }

                if (!string.IsNullOrEmpty(originalFileName))
                {
                    args.Add("OriginalFileName", originalFileName);
                }

                if (!string.IsNullOrEmpty(fileDescription))
                {
                    args.Add("FileDescription", fileDescription);
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
                if (doctype != string.Empty)
                {
                    args.Add("DocumentType", doctype);
                }
                logger.Debug("FileDetailsDAO", "GetHashTable", "Added data to hash table", logPath);
                return args;
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11281");
            }
        }

        public int filesCreatedByCurrentUser(string UserId, string cxnString, string logPath)
        {
            try
            {
                string result = string.Format(COUNTFORFILESCREATED, UserId);
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

                    logger.Debug("FileDetailsDAO", "filesCreatedByCurrentUser", " returning no of File from database ", logPath);

                }
                return count;
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11281");
            }
            finally
            {
                Database.KillConnections();
            }

        }

        public int NoOffilesShared(string cxnString, string logPath)
        {
            try
            {
                string result = COUNTFOR_FILES_SHARED;
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

                    logger.Debug("FileDetailsDAO", "NoOffilesShared", " returning no of File shared from database ", logPath);

                }
                return count;
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "NoOffilesShared", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11281");
            }
            finally
            {
                Database.KillConnections();
            }
        }
        /*public string Getfilepath(int fileID, string cxnString, string logPath)
        {
            try
            {
                string filepath = string.Empty;
                logger.Debug("FileDetailsDAO", "GetSingleFileDetails", "Selecting File Details from database based on file id=" + fileID, logPath);
                string result = string.Format(SELECT_SINGLE_FILE, fileID);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    FileDetails file = new FileDetails();
                    while (reader.Read())
                    {
                        file.Id = Convert.ToInt32(reader["Id"]);
                        file.FileName = reader["FileName"].ToString();
                        file.FileDescription = reader["FileDescription"].ToString();
                        file.OriginalFileName = reader["OriginalFileName"].ToString();
                        file.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        file.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        file.RelativePath = reader["RelativePath"].ToString();
                        file.Tags = reader["Tags"].ToString();
                        file.UserAccess = reader["UserAccess"].ToString();
                        file.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        file.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        file.Permission = reader["Permission"].ToString();
                        file.History = reader["History"].ToString();
                        file.DocumentType = reader["DocumentType"].ToString();
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    //filepath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"], );
                    filepath = Path.Combine(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"] + GetPathHirarchy(file.ParentFolderId, cxnString, logPath)), file.FileName);

                    logger.Debug("FileDetailsDAO", "GetSingleFileDetails", " returning File Details from database based on file id=" + fileID, logPath);

                }
                return filepath;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Database.KillConnections();
            }
        }*/

        public string Getfilepath(int fileID, string cnxnString, string logPath, string college)
        {
            string str3;
            try
            {
                string str = string.Empty;
                this.logger.Debug("FileDetailsDAO", "GetSingleFileDetails", "Selecting File Details from database based on file id=" + fileID, logPath);
                string str2 = string.Format(this.SELECT_SINGLE_FILE, fileID);
                DbDataReader reader = new Database().Select(str2, cnxnString, logPath);
                if (reader.HasRows)
                {
                    FileDetails details = new FileDetails();
                    while (reader.Read())
                    {
                        details.Id = Convert.ToInt32(reader["Id"]);
                        details.FileName = reader["FileName"].ToString();
                        details.FileDescription = reader["FileDescription"].ToString();
                        details.OriginalFileName = reader["OriginalFileName"].ToString();
                        details.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        details.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());
                        details.RelativePath = reader["RelativePath"].ToString();
                        details.Tags = reader["Tags"].ToString();
                        details.UserAccess = reader["UserAccess"].ToString();
                        details.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        details.DateUpdated = Convert.ToDateTime(reader["DateUpdated"]);
                        details.Permission = reader["Permission"].ToString();
                        details.History = reader["History"].ToString();
                        details.DocumentType = reader["DocumentType"].ToString();
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    str = Path.Combine(Path.Combine(HttpContext.Current.Server.MapPath("~/Directories/" + college), this.GetPathHirarchy(details.ParentFolderId, cnxnString, logPath)), details.FileName);
                    this.logger.Debug("FileDetailsDAO", "GetSingleFileDetails", " returning File Details from database based on file id=" + fileID, logPath);
                }
                str3 = str;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.db.KillSleepingConnections(cnxnString, logPath);
            }
            return str3;
        }



        public float StorageSpaceConsumed(string UserId, out int fileCount, string cxnString, string logPath)
        {
            try
            {

                UserDetailsDAO userclass = new UserDetailsDAO();
                UserDetails user = userclass.GetUserDetailList(UserId, cxnString, logPath);

                string result = string.Format(CURRENT_USER_FILE_NAMES, user.Id);
                fileCount = 0;
                float TotalSize = 0;
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<FileDetails> fileList = new List<FileDetails>();
                    while (reader.Read())
                    {
                        FileDetails file = new FileDetails();
                        file.Id = Convert.ToInt32(reader["Id"]);
                        file.FileName = reader["FileName"].ToString();
                        file.FileDescription = reader["FileDescription"].ToString();
                        file.OriginalFileName = reader["OriginalFileName"].ToString();
                        file.OwnerUserId = int.Parse(reader["OwnerUserId"].ToString());
                        file.ParentFolderId = int.Parse(reader["ParentFolderId"].ToString());

                        fileList.Add(file);
                        fileCount++;
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    foreach (FileDetails filename in fileList)
                    {
                        //string filepath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"], GetPathHirarchy(filename.ParentFolderId));
                        string filepath = Path.Combine(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"] + GetPathHirarchy(filename.ParentFolderId, cxnString, logPath)), filename.FileName);
                        FileInfo objfile = new FileInfo(filepath);

                        //if (objfile != null && objfile.Length != null)
                        if (objfile.Exists)
                        {
                            TotalSize = TotalSize + objfile.Length;
                        }

                    }

                    //To Calculate size in mb or kb
                    if (System.Configuration.ConfigurationManager.AppSettings["fileSize"].ToString() == "mb")
                    {
                        TotalSize /= 1024;
                        TotalSize /= 1024;
                    }
                    else
                    {
                        TotalSize /= 1024;
                    }

                    TotalSize = (float)Math.Round(TotalSize, 2);
                    logger.Debug("FileDetailsDAO", "filesCreatedByCurrentUser", " returning no of File from database ", logPath);

                }
                return TotalSize;
            }
            catch (Exception ex)
            {
                logger.Error("FileDetailsDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11281");
            }
            finally
            {
                Database.KillConnections();
            }
        }

        public bool IsStorageConsumed(string UserId, string cxnString, string logPath)
        {
            float fileSize = 0;
            int fileCount = 0;
            fileSize = StorageSpaceConsumed(UserId, out fileCount, cxnString, logPath);

            if (System.Configuration.ConfigurationManager.AppSettings["fileSize"].ToString() == "mb")
            {
                fileSize *= 1024;
                fileSize *= 1024;
            }
            else
            {
                fileSize *= 1024;
            }

            if (fileSize > int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileSize"]))
            {
                return true;
            }

            return false;

        }

        public string GetPathHirarchy(int Id, string cxnString,string logPath)
        {
            try
            {
                FolderDetailsDAO fDetails = new FolderDetailsDAO();
                string path = "/";
                int id = Id;
                for (; ; )
                {
                    FolderDetails folder = fDetails.GetSingleFileDetails(id, cxnString, logPath);
                    if (folder == null) return "";

                    path += folder.FolderName + "/";

                    if (folder.ParentFolderId == -1)
                    {
                        break;
                    }
                    id = folder.ParentFolderId;
                }

                string[] pathArry = path.Split(new char[] { '/' });

                path = string.Empty;
                path = "";

                for (int i = pathArry.Length - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(pathArry[i]))
                    {
                        path += pathArry[i] + "/";
                    }
                }

                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Database.KillConnections();
            }
        }

        public int GetMaxFileId(string cxnString, string logPath)
        {
            try
            {
                int maxCount = 0;
                string cmd = string.Format(SELECT_MAX_FILEID);

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

                return maxCount;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Database.KillConnections();
            }
        }
    }
}
