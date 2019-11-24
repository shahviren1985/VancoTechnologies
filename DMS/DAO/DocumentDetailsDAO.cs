using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Util;

namespace AA.DAO
{
    public class DocumentDetailsDAO
    {
        // Id, UniqueName, FriendlyName, DepartmentId, Author, LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, Content, FolderId, AllowAccess, TaggedUsers, MessageHeader, DateCreated
        Logger logger = new Logger();

        public string SELECT_DOCUMENT_DETAILS = "SELECT Id, UniqueName, FriendlyName, DepartmentId, Author, LastModified, LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, Content, FolderId, Filestag, AllowAccess, TaggedUser, MessageHeader, DateCreated, SerialNumber, StoreroomLoc, Address, ParentId, IsDeadlineMentioned, Deadline FROM documents WHERE Id={0}";
        public string SELECT_ALL_DOCUMENT_DETAILS = "SELECT Id, UniqueName, FriendlyName, DepartmentId, Author, LastModified,LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, Content, FolderId, Filestag, AllowAccess, TaggedUser, MessageHeader, DateCreated, SerialNumber, StoreroomLoc, Address, ParentId, IsDeadlineMentioned, Deadline, IsScannedMarathi, ScanMarathiPath FROM documents order by lastmodified desc";
        public string SELECT_RELATED_DOCUMENTS_DETAILS = "SELECT Id, UniqueName, FriendlyName, DepartmentId, Author, LastModified,LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, Content, FolderId, Filestag, AllowAccess, TaggedUser, MessageHeader, DateCreated, SerialNumber, StoreroomLoc, Address, ParentId, IsDeadlineMentioned, Deadline FROM documents WHERE ParentId = (SELECT ParentId FROM documents WHERE Id={0})";
        public string SELECT_LAST_DOC = "SELECT max(Id) as Id FROM documents";
        public string SELECT_CHILD_DOCUMENT_DETAILS = "SELECT Id, UniqueName, FriendlyName, DepartmentId, Author, LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, Content, FolderId, Filestag, AllowAccess, TaggedUser, MessageHeader, DateCreated, SerialNumber, StoreroomLoc, Address, ParentId, IsDeadlineMentioned, Deadline, IsScannedMarathi, ScanMarathiPath, LastModified FROM documents WHERE ParentId={0}";

        public int GetLastDocument(string cxnString, string logPath)
        {
            int id = 0;
            try
            {
                logger.Debug("DocumentDetailsDAO", "GetLastDocument", " Getting last document details", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_LAST_DOC, cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = int.Parse(reader["Id"].ToString());
                    }
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("DocumentDetailsDAO", "GetLastDocument", " Error occurred while getting last document", ex, logPath);
                //throw ex;
            }

            return id;
        }





        public DocumentDetails GetDocumentDetails(int id, string cxnString, string logPath)
        {
            DocumentDetails document = new DocumentDetails();

            try
            {
                logger.Debug("DocumentDetailsDAO", "GetDocumentDetails", " Getting document details", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(string.Format(SELECT_DOCUMENT_DETAILS, id), cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        document.Id = int.Parse(reader["Id"].ToString());
                        document.UniqueName = reader["UniqueName"].ToString();
                        document.FriendlyName = reader["FriendlyName"].ToString();
                        document.DepartmentId = reader["DepartmentId"].ToString();
                        document.Author = reader["Author"].ToString();
                        document.LastModified = reader["LastModified"].ToString();
                        document.LastModifiedBy = reader["LastModifiedBy"].ToString();
                        document.DocumentStatus = reader["DocumentStatus"].ToString();
                        document.IsScanned = reader["IsScanned"].ToString() == "1" ? true : false;
                        document.DocumentType = reader["DocumentType"].ToString();
                        document.ScanPath = reader["ScanPath"].ToString();
                        document.IsContent = reader["IsContent"].ToString() == "1" ? true : false;
                        document.Content = reader["Content"].ToString();
                        document.FolderId = reader["FolderId"].ToString();
                        document.AllowAccess = reader["AllowAccess"].ToString();
                        document.TaggedUsers = reader["TaggedUser"].ToString();
                        document.MessageHeader = reader["MessageHeader"].ToString();
                        document.SerialNumber = reader["SerialNumber"].ToString();
                        document.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        document.StoreRoomLocation = reader["StoreroomLoc"].ToString();
                        document.FileTags = reader["Filestag"].ToString();
                        document.Address = reader["Address"].ToString();
                        document.ParentId = int.Parse(reader["ParentId"].ToString());
                        document.IsDeadlineMentioned = reader["IsDeadlineMentioned"].ToString() == "1" ? true : false;
                        document.Deadline = reader["Deadline"].ToString();
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error("DocumentDetailsDAO", "GetDocumentDetails", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return document;
        }

        public List<DocumentDetails> GetRelatedDocuments(int id, string cxnString, string logPath)
        {
            List<DocumentDetails> documents = new List<DocumentDetails>();
            Database db = new Database();
            try
            {
                DbDataReader reader = db.Select(string.Format(SELECT_RELATED_DOCUMENTS_DETAILS, id), cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentDetails document = new DocumentDetails();
                        document.Id = int.Parse(reader["Id"].ToString());
                        document.UniqueName = reader["UniqueName"].ToString();
                        document.FriendlyName = reader["FriendlyName"].ToString();
                        document.DepartmentId = reader["DepartmentId"].ToString();
                        document.Author = reader["Author"].ToString();
                        document.LastModified = reader["LastModified"].ToString();
                        document.LastModifiedBy = reader["LastModifiedBy"].ToString();
                        document.DocumentStatus = reader["DocumentStatus"].ToString();
                        document.IsScanned = reader["IsScanned"].ToString() == "1" ? true : false;
                        document.DocumentType = reader["DocumentType"].ToString();
                        document.ScanPath = reader["ScanPath"].ToString();
                        document.IsContent = reader["IsContent"].ToString() == "1" ? true : false;
                        document.Content = reader["Content"].ToString();
                        document.FolderId = reader["FolderId"].ToString();
                        document.AllowAccess = reader["AllowAccess"].ToString();
                        document.TaggedUsers = reader["TaggedUser"].ToString();
                        document.MessageHeader = reader["MessageHeader"].ToString();
                        document.SerialNumber = reader["SerialNumber"].ToString();
                        document.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        document.StoreRoomLocation = reader["StoreroomLoc"].ToString();
                        document.FileTags = reader["Filestag"].ToString();
                        document.Address = reader["Address"].ToString();
                        document.ParentId = int.Parse(reader["ParentId"].ToString());
                        document.IsDeadlineMentioned = reader["IsDeadlineMentioned"].ToString() == "1" ? true : false;
                        document.Deadline = reader["Deadline"].ToString();
                        documents.Add(document);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return documents;
        }

        public List<DocumentDetails> GetDocumentDetails(string query, string cnxnString, string logPath)
        {
            Database database = new Database();
            List<DocumentDetails> list = new List<DocumentDetails>();
            try
            {
                DbDataReader reader = database.Select(query, cnxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentDetails item = new DocumentDetails
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            FriendlyName = reader["FriendlyName"].ToString(),
                            Author = reader["Author"].ToString(),
                            DocumentStatus = reader["DocumentStatus"].ToString(),
                            FileTags = reader["FilesTag"].ToString(),
                            LastModified = reader["LastModified"].ToString(),
                            LastModifiedBy = reader["LastModifiedBy"].ToString(),
                            MessageHeader = reader["MessageHeader"].ToString(),
                            DepartmentId = reader["DepartmentId"].ToString(),
                            TaggedUsers = reader["TaggedUser"].ToString(),
                            ParentId = int.Parse(reader["ParentId"].ToString()),
                            SerialNumber = reader["SerialNumber"].ToString(),
                            StoreRoomLocation = reader["StoreroomLoc"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                        list.Add(item);
                    }
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public void UpdateDocumentDetails(string friendlyName, string departmentId, string author, string lastModifiedBy, string status, bool isScanned, string documentType, string scanPath, bool isContent, string fileTags, string folderId, string content, string storeRoomLoc, string allowAccess, string taggedUser, string messageHeader, int documentId, string serialNumber, string address, bool isDeadlineMentioned, string deadline, string isScannedMarathi, string scanMarathiPath, string cnxnString, string logPath)
        {
            Database db = new Database();

            string cmdText = string.Format(

                "UPDATE documents SET FriendlyName='{0}', DepartmentId='{1}', Author='{2}', LastModifiedBy='{3}'," +
                "DocumentStatus='{4}', IsScanned={5}, DocumentType='{6}', ScanPath='{7}', IsContent={8}, FolderId='{9}', Filestag='{10}'," +
                " Content='{11}', StoreroomLoc='{12}', AllowAccess='{13}', TaggedUser='{14}', MessageHeader='{15}', SerialNumber='{16}', LastModified=CURRENT_TIMESTAMP, Address='{18}', IsDeadlineMentioned={19}, Deadline='{20}',IsScannedMarathi={21}, ScanMarathiPath='{22}' where id={17}",
                friendlyName,
                departmentId,
                author,
                lastModifiedBy,
                status,
                isScanned,
                documentType,
                scanPath,
                isContent,
                folderId,
                fileTags,
                content,
                storeRoomLoc,
                allowAccess,
                taggedUser,
                messageHeader,
                serialNumber,
                documentId,
                address,
                isDeadlineMentioned,
                deadline,
                isScannedMarathi,
                scanMarathiPath
            );

            db.Update(cmdText, cnxnString, logPath);
        }

        public void UpdateDocumentDetails(int documentId, string lastModifiedBy, string status, string cnxnString, string logPath)
        {
            Database db = new Database();
            string cmdText = string.Format("UPDATE documents SET LastModifiedBy='{0}', LastModified=CURRENT_TIMESTAMP,DocumentStatus='{1}' WHERE Id={2}", lastModifiedBy, status, documentId);
            db.Update(cmdText, cnxnString, logPath);
        }

        public void InsertDocumentDetails(string uniqueName, string friendlyName, string departmentId, string author, string lastModifiedBy, string status, bool isScanned, string documentType, string scanPath, bool isContent, string folderId, string content, string storeRoomLoc, string allowAccess, string taggedUser, string messageHeader, string filesTag, string serialNumber, string address, int parentId, bool isDeadlineMentioned, string deadline, string isScannedMarathi, string scanMarathiPath, string cnxnString, string logPath)
        {
            Database db = new Database();
            // INSERT INTO documents (UniqueName, FriendlyName, DepartmentId, Author, LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, FolderId, Filestag, Content, StoreroomLoc, AllowAccess, TaggedUser, MessageHeader, DateCreated) VALUES ()
            string cmdText = string.Format("INSERT INTO documents (UniqueName, FriendlyName, DepartmentId, Author, LastModifiedBy, DocumentStatus, IsScanned, DocumentType, ScanPath, IsContent, FolderId, Filestag, Content, StoreroomLoc, AllowAccess, TaggedUser, MessageHeader, DateCreated, SerialNumber, Address, ParentId, IsDeadlineMentioned, Deadline, IsScannedMarathi, ScanMarathiPath) VALUES (" +
                                                                     "'{0}',    '{1}',          '{2}',      '{3}',      '{4}',          '{5}',          {6},        '{7}',      '{8}',  {9},        '{10}',   '{11}', '{12}',     '{13}',     '{14}',         '{15}',     '{16}',     '{17}',        '{18}',    '{19}', {20},           {21} ,           '{22}' ,   {23}       ,  '{24}'       )",

                                                                     uniqueName, friendlyName, departmentId, author, lastModifiedBy, status, isScanned, documentType, scanPath, isContent, folderId, filesTag, content, storeRoomLoc, allowAccess, taggedUser, messageHeader, Utilities.GetCurrentIndianDate().ToString("yyyy-MM-dd hh:mm:ss"), serialNumber, address, parentId, isDeadlineMentioned, deadline, (isScannedMarathi.ToLower() == "true" ? 1 : 0), scanMarathiPath
            );
            db.Insert(cmdText, cnxnString, logPath);
        }

        public List<DocumentDetails> GetFullDocumentDetails(string cxnString, string logPath)
        {
            List<DocumentDetails> documents = new List<DocumentDetails>();

            try
            {
                logger.Debug("DocumentDetailsDAO", "GetDocumentDetails", " Getting document details", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_ALL_DOCUMENT_DETAILS, cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentDetails document = new DocumentDetails();
                        document.Id = int.Parse(reader["Id"].ToString());
                        document.UniqueName = reader["UniqueName"].ToString();
                        document.FriendlyName = reader["FriendlyName"].ToString();
                        document.DepartmentId = reader["DepartmentId"].ToString();
                        document.Author = reader["Author"].ToString();
                        document.LastModified = reader["LastModified"].ToString();
                        document.LastModifiedBy = reader["LastModifiedBy"].ToString();
                        document.DocumentStatus = reader["DocumentStatus"].ToString();
                        document.IsScanned = reader["IsScanned"].ToString() == "1" ? true : false;
                        document.DocumentType = reader["DocumentType"].ToString();
                        document.ScanPath = reader["ScanPath"].ToString();
                        document.IsContent = reader["IsContent"].ToString() == "1" ? true : false;
                        document.Content = reader["Content"].ToString();
                        document.FolderId = reader["FolderId"].ToString();
                        document.AllowAccess = reader["AllowAccess"].ToString();
                        document.TaggedUsers = reader["TaggedUser"].ToString();
                        document.MessageHeader = reader["MessageHeader"].ToString();
                        document.SerialNumber = reader["SerialNumber"].ToString();
                        document.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        document.StoreRoomLocation = reader["StoreroomLoc"].ToString();
                        document.FileTags = reader["Filestag"].ToString();
                        document.Address = reader["Address"].ToString();
                        document.ParentId = int.Parse(reader["ParentId"].ToString());
                        document.IsDeadlineMentioned = reader["IsDeadlineMentioned"].ToString() == "1" ? true : false;
                        document.Deadline = reader["Deadline"].ToString();
                        document.ScanMarathiPath = reader["IsScannedMarathi"].ToString();
                        document.IsScannedMarathi = reader["IsScannedMarathi"].ToString();
                        documents.Add(document);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error("DocumentDetailsDAO", "GetDocumentDetails", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return documents;


        }

        public List<DocumentDetails> GetDocumentsHistory(int currentDocId, string cnxnString, string logPath)
        {
            List<DocumentDetails> list = new List<DocumentDetails>();
            bool flag = true;
            bool flag2 = true;
            DocumentDetails item = this.GetDocumentDetails(currentDocId, cnxnString, logPath);
            if (item != null)
            {
                list.Add(item);
                int parentId = item.ParentId;
                while (flag)
                {
                    if (parentId != -1)
                    {
                        DocumentDetails details2 = this.GetDocumentDetails(parentId, cnxnString, logPath);
                        list.Add(details2);
                        parentId = details2.ParentId;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                parentId = currentDocId;
                while (flag2)
                {
                    if (parentId > 0)
                    {
                        DocumentDetails details3 = this.GetChildDocumentDetails(parentId, cnxnString, logPath);
                        if (details3 != null)
                        {
                            list.Add(details3);
                            parentId = details3.Id;
                        }
                        else
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        flag2 = false;
                    }
                }
            }
            return list;
        }

        public DocumentDetails GetChildDocumentDetails(int parentId, string cxnString, string logPath)
        {
            try
            {
                this.logger.Debug("DocumentDetailsDAO", "GetDocumentDetails", " Getting document details", logPath);
                DbDataReader reader = new Database().Select(string.Format(this.SELECT_CHILD_DOCUMENT_DETAILS, parentId), cxnString, logPath);
                if (reader.HasRows)
                {
                    DocumentDetails details = new DocumentDetails();
                    while (reader.Read())
                    {
                        details.Id = int.Parse(reader["Id"].ToString());
                        details.UniqueName = reader["UniqueName"].ToString();
                        details.FriendlyName = reader["FriendlyName"].ToString();
                        details.DepartmentId = reader["DepartmentId"].ToString();
                        details.Author = reader["Author"].ToString();
                        details.LastModifiedBy = reader["LastModifiedBy"].ToString();
                        details.DocumentStatus = reader["DocumentStatus"].ToString();
                        details.IsScanned = reader["IsScanned"].ToString() == "1";
                        details.DocumentType = reader["DocumentType"].ToString();
                        details.ScanPath = reader["ScanPath"].ToString();
                        details.IsContent = reader["IsContent"].ToString() == "1";
                        details.Content = reader["Content"].ToString();
                        details.FolderId = reader["FolderId"].ToString();
                        details.AllowAccess = reader["AllowAccess"].ToString();
                        details.TaggedUsers = reader["TaggedUser"].ToString();
                        details.MessageHeader = reader["MessageHeader"].ToString();
                        details.SerialNumber = reader["SerialNumber"].ToString();
                        details.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        details.StoreRoomLocation = reader["StoreroomLoc"].ToString();
                        details.FileTags = reader["Filestag"].ToString();
                        details.Address = reader["Address"].ToString();
                        details.ParentId = int.Parse(reader["ParentId"].ToString());
                        details.IsDeadlineMentioned = reader["IsDeadlineMentioned"].ToString() == "1";
                        details.Deadline = reader["Deadline"].ToString();
                        details.IsScannedMarathi = (reader["IsScannedMarathi"].ToString() == "1").ToString();
                        details.ScanMarathiPath = reader["ScanMarathiPath"].ToString();
                        details.LastModified = reader["LastModified"].ToString();
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    return details;
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("DocumentDetailsDAO", "GetDocumentDetails", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return null;
        }


    }
}
