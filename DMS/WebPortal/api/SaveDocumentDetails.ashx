<%@ WebHandler Language="C#" Class="SaveDocumentDetails" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;

public class SaveDocumentDetails : IHttpHandler, IRequiresSessionState
{
    string logPath;
    string cnxnString;
    Logger logger = new Logger();
    string college = string.Empty;
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        //Exam,Socio,Psy
        //one,two,three
        //Naresh Lad (naresh),Harshada Rathod (harshada)
        long ticks = DateTime.Now.Ticks;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        DocumentDetailsDAO dao = new DocumentDetailsDAO();
        logPath = context.Session["LogFilePath"].ToString();
        cnxnString = context.Session["ConnectionString"].ToString();
        college = context.Session["College"].ToString();

        configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

        try
        {
            string data = HttpUtility.UrlDecode(new System.IO.StreamReader(context.Request.InputStream).ReadToEnd());

            string documentId = string.Empty;
            string toList = string.Empty;
            string from = string.Empty;
            string subject = string.Empty;
            string documentTitle = string.Empty;
            string isForwarded = string.Empty;
            string dateForwarded = string.Empty;
            string isReceived = string.Empty;
            string dateReceived = string.Empty;
            string inwardDocumentId = string.Empty;
            string dateInward = string.Empty;
            string departmentId = string.Empty;
            string fileTags = string.Empty;
            string taggedUsers = string.Empty;
            string isEmptyDocument = string.Empty;
            string isDocumentScanned = string.Empty;
            string isContentDocument = string.Empty;
            string scanPath = string.Empty;
            string content = string.Empty;
            string serialNumber = string.Empty;
            string storeRoomLoc = string.Empty;
            string userDocumentType = string.Empty;
            string approved = string.Empty;
            string address = string.Empty;
            string parentId = "-1";
            string isDeadlineMentioned = string.Empty;
            string deadline = string.Empty;
            string isMarathiDocumentScanned = string.Empty;
            string scanMarathiPath = string.Empty;

            foreach (string item in context.Request.Form)
            {
                switch (item)
                {
                    case "DocumentId":
                        documentId = context.Request.Params[item];
                        break;
                    case "To[]":
                        toList = context.Request.Params[item];
                        break;
                    case "From":
                        from = HttpUtility.UrlEncode(context.Request.Params[item]);
                        break;
                    case "Subject":
                        subject = HttpUtility.UrlEncode(context.Request.Params[item]);
                        break;
                    case "DocumentTitle":
                        documentTitle = HttpUtility.UrlEncode(context.Request.Params[item]);
                        break;
                    case "IsForwarded":
                        isForwarded = context.Request.Params[item];
                        break;
                    case "DateForwarded":
                        dateForwarded = context.Request.Params[item];
                        break;
                    case "IsReceived":
                        isReceived = context.Request.Params[item];
                        break;
                    case "DateReceived":
                        dateReceived = context.Request.Params[item];
                        break;
                    case "InwardDocumentId":
                        inwardDocumentId = HttpUtility.UrlEncode(context.Request.Params[item]);
                        break;
                    case "DateInward":
                        dateInward = context.Request.Params[item];
                        break;
                    case "DepartmentId[]":
                        departmentId = context.Request.Params[item];
                        break;
                    case "FilesTag[]":
                        fileTags = HttpUtility.UrlEncode(context.Request.Params[item]);
                        break;
                    case "TaggedUser[]":
                        taggedUsers = Util.DocumentManager.GetUserNames(context.Request.Params[item]);
                        break;
                    case "IsEmpty":
                        isEmptyDocument = context.Request.Params[item];
                        break;
                    case "IsScanned":
                        isDocumentScanned = context.Request.Params[item];
                        break;
                    case "IsContent":
                        isContentDocument = context.Request.Params[item];
                        break;
                    case "ScanPath":
                        scanPath = context.Request.Params[item];
                        break;
                    case "Content":
                        content = context.Request.Params[item];
                        break;
                    case "SerialNumber":
                        serialNumber = context.Request.Params[item];
                        break;
                    case "StoreRoomLocation[]":
                        storeRoomLoc = context.Request.Params[item];
                        break;
                    case "DocumentType":
                        userDocumentType = context.Request.Params[item];
                        break;
                    case "Approved":
                        approved = context.Request.Params[item];
                        break;
                    case "Address":
                        address = HttpUtility.UrlEncode(context.Request.Params[item]);
                        break;
                    case "ParentId":
                        parentId = string.IsNullOrEmpty(context.Request.Params[item]) ? "-1" : context.Request.Params[item];
                        break;
                    case "IsDeadlineMentioned":
                        isDeadlineMentioned = context.Request.Params[item];
                        break;
                    case "Deadline":
                        deadline = context.Request.Params[item];
                        break;
                    case "IsScannedMarathi":
                        isMarathiDocumentScanned = context.Request.Params[item];
                        break;
                    case "ScanMarathiPath":
                        scanMarathiPath = context.Request.Params[item];
                        break;
                }
            }

            // Populate the details as required for DB
            string friendlyName = documentTitle;
            string departments = departmentId;
            string lastModifiedBy = context.Session["UserName"].ToString();

            bool isEmpty = isEmptyDocument == "true" ? true : false;
            bool isScanned = isDocumentScanned == "true" ? true : false;
            bool isContent = isContentDocument == "true" ? true : false;
            bool isDeadline = isDeadlineMentioned == "true" ? true : false;
            bool isScannedMarathi = isMarathiDocumentScanned == "true" ? true : false;

            string documentType = Util.DocumentManager.GetDocumentType(isEmpty, isScanned, isContent, scanPath);

            #region Prepare Message Header
            AA.DAO.MessageHeader mh = new AA.DAO.MessageHeader();

            if (userDocumentType == "inward")
            {
                mh.InwardDate = dateInward;
                mh.InwardNumber = inwardDocumentId;
                mh.OutwardDate = string.Empty;
                mh.OutwardNumber = string.Empty;
            }
            else if (userDocumentType == "outward")
            {
                mh.InwardDate = string.Empty;
                mh.InwardNumber = string.Empty;
                mh.OutwardDate = dateInward;
                mh.OutwardNumber = inwardDocumentId;
            }
            else
            {
                mh.OutwardDate = string.Empty;
                mh.OutwardNumber = string.Empty;
                mh.InwardDate = string.Empty;
                mh.InwardNumber = string.Empty;
            }

            mh.To = toList;
            mh.From = from;
            mh.Subject = subject;
            mh.DocumentType = userDocumentType;
            mh.IsForwaded = isForwarded;
            mh.IsReceived = isReceived;
            mh.ForwardDate = dateForwarded;
            mh.ReceivedDate = dateReceived;

            #endregion
            string messageHeader = jss.Serialize(mh);
            string author = context.Session["UserName"].ToString();

            // Insert flow
            if (string.IsNullOrEmpty(documentId) || documentId == "0")
            {
                string uniqueName = AA.Util.ParameterFormater.RemoveCharacters(context.Session["UserName"].ToString()) + "_" + ticks;

                string status = "CREATE";
                dao.InsertDocumentDetails(
                    uniqueName,
                    friendlyName,
                    departmentId,
                    author,
                    lastModifiedBy,
                    status,
                    isScanned,
                    documentType,
                    scanPath,
                    isContent,
                    string.Empty, // FolderId 
                    content,
                    storeRoomLoc,  // StoreRoomLoc
                    string.Empty, // AllowAccess 
                    taggedUsers,
                    messageHeader,
                    fileTags,
                    serialNumber,
                    address,
                    int.Parse(parentId),
                    isDeadline,
                    deadline,
                    isScannedMarathi.ToString(),
                    scanMarathiPath,
                    cnxnString,
                    logPath);

                int id = dao.GetLastDocument(cnxnString, logPath);

                int number;

                XmlConfiguration xmlConfig = new XmlConfiguration();

                if (serialNumber.ToUpper().StartsWith("IR"))
                {
                    if (int.TryParse(serialNumber.Substring(2), out number))
                    {
                        if (xmlConfig.GetInwardDocumentId(logPath, configFilePath) == number)
                        {
                            xmlConfig.SaveDocumentId("inward", logPath, configFilePath);
                        }
                    }
                }
                else if (serialNumber.ToUpper().StartsWith("OR"))
                {
                    if (int.TryParse(serialNumber.Substring(2), out number))
                    {
                        if (xmlConfig.GetOutwardDocumentId(logPath, configFilePath) == number)
                        {
                            xmlConfig.SaveDocumentId("outward", logPath, configFilePath);
                        }
                    }
                }

                // Send new document created email
                SendDocumentCreatedEmail(id);
                context.Response.Write(jss.Serialize(id));
            }
            else
            {
                // update flow
                string status = "UPDATE";

                if (!string.IsNullOrEmpty(approved) && approved.ToLower().Trim() == "true")
                {
                    status = "APPROVED";
                }

                DocumentDetails oldDoc = dao.GetDocumentDetails(int.Parse(documentId), cnxnString, logPath);
                if (oldDoc != null) author = oldDoc.Author;

                dao.UpdateDocumentDetails(
                    friendlyName,
                    departmentId,
                    author,
                    lastModifiedBy,
                    status,
                    isScanned,
                    documentType,
                    scanPath,
                    isContent,
                    fileTags,
                    string.Empty, // FolderId 
                    content,
                    storeRoomLoc,  // StoreRoomLoc
                    string.Empty,//"Not Specified", // AllowAccess 
                    taggedUsers,
                    messageHeader,
                    int.Parse(documentId),
                    serialNumber,
                    address,
                    isDeadline,
                    deadline,
                    isScannedMarathi.ToString(),
                    scanMarathiPath,
                    cnxnString,
                    logPath);

                // Send new document updated email
                SendDocumentUpdateEmail(int.Parse(documentId));

                if (status == "APPROVED")
                {
                    DocumentDetails doc = dao.GetDocumentDetails(int.Parse(documentId), cnxnString, logPath);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BASE_URL"] + "/api/GeneratePdfFile.ashx?docid=" + documentId + "&name=" + doc.UniqueName + ".pdf&user=" + context.Session["UserName"].ToString() + "&con=" + cnxnString + "&log=" + logPath + "&college=" + college + "&configPath=" + configFilePath);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string path = string.Empty;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                        path = readStream.ReadToEnd();

                        response.Close();
                        readStream.Close();
                    }

                    string uploadedDocs = string.Empty;

                    if (!string.IsNullOrEmpty(scanPath))
                        uploadedDocs = author + "/" + scanPath;

                    List<string> paths = new List<string>() { path, uploadedDocs };

                    // Send new document approved email
                    //context.Response.Write(jss.Serialize(path));
                    context.Response.Write(jss.Serialize(paths));
                }
                else
                {
                    context.Response.Write(int.Parse(documentId));
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private bool SendDocumentUpdateEmail(int docId)
    {
        try
        {
            string docUrl = ConfigurationManager.AppSettings["BASE_URL"] + "/Documents.aspx?id=" + docId;

            #region Variable Declaration & Initialization
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DocumentDetailsDAO dao = new DocumentDetailsDAO();
            UserDetailsDAO userDao = new UserDetailsDAO();
            List<UserDetails> tempUsers = new List<UserDetails>();
            DocumentDetails doc = dao.GetDocumentDetails(docId, cnxnString, logPath);
            MessageHeader mh = jss.Deserialize<MessageHeader>(doc.MessageHeader);
            EmailConfig emailConfig = GetEmailConfig();
            EmailConfig sendEmail = GetEmailSendConfig();
            string[] taggedUsers = { };
            bool isDeptPresent = false;
            #endregion

            if (!sendEmail.IsSendUpdateMail)
                return false;


            string query = "SELECT  Id, UserName,FirstName,LastName,Email,UserPassword,IsActive,department FROM users WHERE IsActive='1' ";

            // Populate department in query
            if (!string.IsNullOrEmpty(doc.DepartmentId))
            {
                string[] departments = doc.DepartmentId.Split(',');

                if (departments != null && departments.Length > 0)
                {
                    query += "AND (";
                }

                foreach (string dept in departments)
                {
                    query += " department LIKE '%" + dept + "%' or";
                }

                if (departments != null && departments.Length > 0)
                {
                    query = query.Substring(0, query.Length - 2);
                    query += ")";
                }

                isDeptPresent = true;
            }

            // Populate users in query
            if (!string.IsNullOrEmpty(doc.TaggedUsers))
            {
                taggedUsers = doc.TaggedUsers.Split(',');

                #region Get User Details
                if (taggedUsers != null && taggedUsers.Length > 0)
                {

                    query += isDeptPresent ? " OR (" : " AND (";
                }

                foreach (string tag in taggedUsers)
                {
                    query += " UserName LIKE '%" + tag + "%' or";
                }

                if (taggedUsers != null && taggedUsers.Length > 0)
                {
                    query = query.Substring(0, query.Length - 2); // remove last "or"
                    query += " )";
                }

                // added by vasim
                tempUsers = userDao.GetUsersByDepartment(query, cnxnString, logPath);
                #endregion
            }

            // comentted by vasim, it send mail to all users when user tag field is empty.
            //tempUsers = userDao.GetUsersByDepartment(query, cnxnString, logPath);

            /*if (!string.IsNullOrEmpty(doc.DepartmentId))
            {
                string[] departments = doc.DepartmentId.Split(',');

                if (!string.IsNullOrEmpty(doc.TaggedUsers))
                    taggedUsers = doc.TaggedUsers.Split(',');

                #region Get User Details
                string query = "SELECT  Id, UserName,FirstName,LastName,Email,UserPassword,IsActive,department FROM users WHERE IsActive='1' ";

                if (departments != null && departments.Length > 0)
                {
                    query += "AND (";
                }

                foreach (string dept in departments)
                {
                    query += " department LIKE '%" + dept + "%'";
                }

                if (departments != null && departments.Length > 0)
                {
                    query += ")";
                }
                if (taggedUsers != null && taggedUsers.Length > 0)
                {
                    query += " OR (";
                }

                foreach (string tag in taggedUsers)
                {
                    query += " UserName LIKE '%" + tag + "%'";
                }

                if (taggedUsers != null && taggedUsers.Length > 0)
                {
                    query += " )";
                }

                tempUsers = userDao.GetUsersByDepartment(query, cnxnString, logPath);
                #endregion
            }*/

            foreach (UserDetails user in tempUsers)
            {
                // if user doesnot have email address so we do not want to proecess further
                if (string.IsNullOrEmpty(user.Email))
                {
                    logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentUpdateEmail", "Empty email address found for user " + user.FirstName, logPath);
                    continue;
                }

                #region Send Email to Users
                logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentUpdateEmail", "Sending email to " + user.FirstName, logPath);
                List<string> to = new List<string>();
                to.Add(user.Email);
                //string path = doc.IsScanned ? "<b>Attachment Link:</b> <a href='{6}' target='_new'>{6}</a><br/><br/><br/>" : "";
                string path = doc.IsScanned ? "<b>Attachment Document:</b> <span>{6}</span></br></br></br>" : "";
                string msg = "Hello {0}, <br/><br/>Document titled {1} is updated. You could check this document by clicking on this <a href='{2}' target='_new'>Link</a>" +
                    "<br/><br/><b>Document Title:</b> {1}<br/>" +
                    "<br/><b>Document Type:</b> {3}<br/>" +
                    "<br/><b>Date {3}:</b> {4}</br><br/>" +
                    "<br/><b>Has Attachments:</b> {5}<br/><br/>" +
                    path +
                    "<br/>PS - This is an automatically generated email, please do not reply to this email.<br/>" +
                    "<br/>Thank You,<br/><br/>Office Administrator<br/><br/>Maniben Nanavati Women's College";

                string dt = mh.DocumentType == "inward" ? mh.InwardDate : mh.OutwardDate;
                string hasAttach = doc.IsScanned ? "Yes" : "No";
                //msg = string.Format(msg, user.FirstName, docUrl, HttpUtility.UrlDecode(doc.FriendlyName), mh.DocumentType, dt, hasAttach, doc.ScanPath);
                //msg = string.Format(msg, user.FirstName, docUrl, HttpUtility.UrlDecode(doc.FriendlyName), mh.DocumentType, dt, hasAttach, doc.SerialNumber + "_" + mh.DocumentType);
                msg = string.Format(msg, user.FirstName, HttpUtility.UrlDecode(doc.FriendlyName), docUrl, mh.DocumentType, dt, hasAttach, doc.SerialNumber + "_" + mh.DocumentType);
                emailConfig.Subject = "Document Update: " + HttpUtility.UrlDecode(doc.FriendlyName);
                emailConfig.Message = msg;
                emailConfig.ToAddress = to;
                bool isMailSent = new MailManager().SendEmail(emailConfig, false, null, logPath);

                if (isMailSent)
                    logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentUpdateEmail", "Successfully sent email to " + user.FirstName, logPath);
                else
                    logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentUpdateEmail", "Email sending failed for " + user.FirstName, logPath);
                #endregion
            }
        }
        catch (Exception ex)
        {
            logger.Error("SaveDocumentsDetails-Handler", "SendDocumentUpdateEmail", ex.Message, ex, logPath);
        }

        return false;
    }

    private bool SendDocumentCreatedEmail(int docId)
    {

        try
        {
            string docUrl = ConfigurationManager.AppSettings["BASE_URL"] + "/Documents.aspx?id=" + docId;

            #region Variable Declaration & Initialization
            JavaScriptSerializer jss = new JavaScriptSerializer();
            DocumentDetailsDAO dao = new DocumentDetailsDAO();
            UserDetailsDAO userDao = new UserDetailsDAO();
            List<UserDetails> tempUsers = new List<UserDetails>();
            DocumentDetails doc = dao.GetDocumentDetails(docId, cnxnString, logPath);
            MessageHeader mh = jss.Deserialize<MessageHeader>(doc.MessageHeader);
            EmailConfig emailConfig = GetEmailConfig();
            EmailConfig sendEmail = GetEmailSendConfig();
            string[] taggedUsers = { };
            bool isDeptPresent = false;
            #endregion

            if (!sendEmail.IsSendCreateMail)
                return false;

            string query = "SELECT  Id, UserName,FirstName,LastName,Email,UserPassword,IsActive,department FROM users WHERE IsActive='1' ";

            // Populate department in query
            if (!string.IsNullOrEmpty(doc.DepartmentId))
            {
                string[] departments = doc.DepartmentId.Split(',');

                if (departments != null && departments.Length > 0)
                {
                    query += "AND (";
                }

                foreach (string dept in departments)
                {
                    if (!string.IsNullOrEmpty(dept))
                        query += " department LIKE '%" + dept + "%' or";
                }

                if (departments != null && departments.Length > 0)
                {
                    query = query.Substring(0, query.Length - 2);
                    query += ")";
                }

                isDeptPresent = true;
            }

            // Populate users in query
            if (!string.IsNullOrEmpty(doc.TaggedUsers))
            {
                taggedUsers = doc.TaggedUsers.Split(',');

                #region Get User Details
                if (taggedUsers != null && taggedUsers.Length > 0)
                {
                    query += isDeptPresent ? " OR (" : " AND (";
                }

                foreach (string tag in taggedUsers)
                {
                    if (!string.IsNullOrEmpty(tag))
                        query += " UserName LIKE '%" + tag + "%' or";
                }

                if (taggedUsers != null && taggedUsers.Length > 0)
                {
                    query = query.Substring(0, query.Length - 2); // remove last "or"
                    query += " )";
                }

                // added by vasim
                tempUsers = userDao.GetUsersByDepartment(query, cnxnString, logPath);

                #endregion
            }

            // comentted by vasim, it send mail to all users when user tag field is empty.
            //tempUsers = userDao.GetUsersByDepartment(query, cnxnString, logPath);

            foreach (UserDetails user in tempUsers)
            {
                // if user doesnot have email address so we do not want to proecess further
                if (string.IsNullOrEmpty(user.Email))
                {
                    logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentCreatedEmail", "Empty email address found for user " + user.FirstName, logPath);
                    continue;
                }

                #region Send Email to Users
                logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentCreatedEmail", "Sending email to " + user.FirstName, logPath);
                List<string> to = new List<string>();
                to.Add(user.Email);
                //string path = doc.IsScanned ? "<b>Attachment Link:</b> <a href='{6}' target='_new'>{6}</a></br></br></br>" : "";
                string path = doc.IsScanned ? "<b>Attachment Document:</b> <span>{6}</span></br></br></br>" : "";
                string msg = "Hello {0}, <br/><br/>New document is created and assigned to you. You could check this document by clicking on this <a href='{1}' target='_new'>Link</a>" +
                    "<br/><br/><b>Document Title:</b> {2}<br/>" +
                    "<br/><b>Document Type:</b> {3}</br>" +
                    "<br/><b>Date {3}:</b> {4}</br><br/>" +
                    "<br/><b>Has Attachments:</b> {5}<br/><br/>" +
                    path +
                    "<br/>PS - This is an automatically generated email, please do not reply to this email.<br/>" +
                    "<br/>Thank You,<br/><br/>Office Administrator<br/><br/>Maniben Nanavati Women's College";

                string dt = mh.DocumentType == "inward" ? mh.InwardDate : mh.OutwardDate;
                string hasAttach = doc.IsScanned ? "Yes" : "No";
                //msg = string.Format(msg, user.FirstName, docUrl, HttpUtility.UrlDecode(doc.FriendlyName), mh.DocumentType, dt, hasAttach, doc.ScanPath);
                msg = string.Format(msg, user.FirstName, docUrl, HttpUtility.UrlDecode(doc.FriendlyName), mh.DocumentType, dt, hasAttach, doc.SerialNumber + "_" + mh.DocumentType);
                emailConfig.Subject = "New Document: " + HttpUtility.UrlDecode(doc.FriendlyName);
                emailConfig.Message = msg;
                emailConfig.ToAddress = to;

                bool isMailSent = new MailManager().SendEmail(emailConfig, false, null, logPath);

                if (isMailSent)
                    logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentCreatedEmail", "Successfully sent email to " + user.FirstName, logPath);
                else
                    logger.Debug("SaveDocumentsDetails-Handler", "SendDocumentCreatedEmail", "Email sending failed to " + user.FirstName, logPath);

                #endregion
            }
        }
        catch (Exception ex)
        {
            logger.Error("SaveDocumentsDetails-Handler", "SendDocumentCreatedEmail", ex.Message, ex, logPath);
        }

        return false;
    }

    private EmailConfig GetEmailConfig()
    {
        XmlConfiguration config = new XmlConfiguration();
        return config.GetEmailConfig(logPath, configFilePath);
    }

    private EmailConfig GetEmailSendConfig()
    {
        XmlConfiguration config = new XmlConfiguration();
        return config.GetEmailSendConfig(logPath, configFilePath);
    }
}