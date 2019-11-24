<%@ WebHandler Language="C#" Class="AdvancedSearch" %>
using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class AdvancedSearch : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    string logPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();
        string searchType = context.Request.QueryString["t"];
        string searchTerm = context.Request.QueryString["q"];
        string cupboard = context.Request.QueryString["cup"];
        string shelf = context.Request.QueryString["shelf"];
        string room = context.Request.QueryString["rm"];
        string file = context.Request.QueryString["file"];


        // added due to getting error while serialize large data
        jss.MaxJsonLength = int.MaxValue;

        /*string role = context.Session["RoleType"].ToString();
        UserDetails user = userDao.GetUserDetailList(userName, cnxnString, logPath);
        string[] depts = user.Departments.Split(',');*/
        string query = string.Empty;
        if (searchType == "gs")
        {
        }
        else if (searchType == "cb")
        {
            if (!string.IsNullOrEmpty(cupboard) && !string.IsNullOrEmpty(shelf))
            {
                if (cupboard == "All Cupboards")
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag,StoreroomLoc, Address FROM documents d  order by lastmodified desc";
                else if (shelf == "All Shelfs")
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag,StoreroomLoc, Address FROM documents d WHERE StoreroomLoc like '%" + cupboard + "%' order by lastmodified desc";
                else
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag,StoreroomLoc, Address FROM documents d WHERE StoreroomLoc like '%" + cupboard + " -&gt; Shelf (" + shelf + ")%' union SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag,StoreroomLoc, Address FROM documents d WHERE StoreroomLoc like '%" + cupboard + "," + shelf + "%' order by lastmodified desc";

                List<DocumentDetails> documents = dao.GetDocumentDetails(query, cnxnString, logPath);

                if (documents == null) documents = new List<DocumentDetails>();

                foreach (DocumentDetails doc in documents)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                    doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    documents = documents.FindAll(dc => dc.FriendlyName.ToLower().Contains(searchTerm.ToLower()) || dc.FileTags.ToLower().Contains(searchTerm.ToLower()));
                }

                context.Response.Write(jss.Serialize(documents));
            }
        }
        else if (searchType == "fl")
        {
            if (!string.IsNullOrEmpty(room) && !string.IsNullOrEmpty(cupboard) && !string.IsNullOrEmpty(shelf) && !string.IsNullOrEmpty(file))
            {
                if (room == "---Room---")
                {
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag, StoreroomLoc, Address  FROM documents d  order by lastmodified desc";
                }
                else if (cupboard == "---Cupboard---")
                {
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag, StoreroomLoc , Address FROM documents d WHERE StoreroomLoc like '%Room (" + room + ")%' order by lastmodified desc";
                }
                else if (shelf == "---Shelf---")
                {
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag, StoreroomLoc, Address  FROM documents d WHERE StoreroomLoc like '%Room (" + room + ") -&gt; Cupboard (" + cupboard + ")%' order by lastmodified desc";
                }
                else if (file == "---File---")
                {
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag, StoreroomLoc, Address FROM documents d WHERE StoreroomLoc like '%Room (" + room + ") -&gt; Cupboard (" + cupboard + ") -&gt; Shelf (" + shelf + ")%' order by lastmodified desc";
                }
                else
                {
                    query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag, StoreroomLoc, Address FROM documents d WHERE StoreroomLoc like '%Room (" + room + ") -&gt; Cupboard (" + cupboard + ") -&gt; Shelf (" + shelf + ") -&gt; File -&gt; (" + file + ")%'  union SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,Filestag, StoreroomLoc, Address FROM documents d WHERE StoreroomLoc like '%" + room + "," + cupboard + "," + shelf + "," + file + "%' order by lastmodified desc";
                }

                List<DocumentDetails> documents = dao.GetDocumentDetails(query, cnxnString, logPath);

                if (documents == null) documents = new List<DocumentDetails>();

                foreach (DocumentDetails doc in documents)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                    doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    documents = documents.FindAll(dc => dc.FriendlyName.ToLower().Contains(searchTerm.ToLower()) || dc.FileTags.ToLower().Contains(searchTerm.ToLower()));
                }

                context.Response.Write(jss.Serialize(documents));
            }
        }


        else if (searchType == "dp")
        {
            string departments = context.Request.QueryString["dept"];

            if (!string.IsNullOrEmpty(departments))
            {
                departments = departments.Substring(0, departments.LastIndexOf(","));

                string[] depts = departments.Split(',');

                query = "Select * from documents d where DepartmentId like '%" + departments + "%' order by lastmodified desc limit 500";

                var docs = dao.GetDocumentDetails(query, cnxnString, logPath);

                List<DocumentDetails> documents = new List<DocumentDetails>();

                foreach (DocumentDetails doc in docs)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                    doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                    foreach (string dep in depts)
                    {
                        if (doc.DepartmentId.ToLower().Contains(dep.ToLower()))
                        {
                            if (!string.IsNullOrEmpty(searchTerm))
                            {
                                if (doc.Author.ToLower().Contains(searchTerm) || doc.FileTags.ToLower().Contains(searchTerm) || doc.TaggedUsers.ToLower().Contains(searchTerm) || doc.MessageHeader.ToLower().Contains(searchTerm) || doc.FriendlyName.ToLower().Contains(searchTerm))
                                {
                                    documents.Add(doc);
                                }
                            }
                            else
                                documents.Add(doc);
                        }
                    }
                }

                context.Response.Write(jss.Serialize(documents));
            }
        }
        if (searchType == "dr")
        {
            string strStartDate = context.Request.QueryString["sd"];
            string strEndDate = context.Request.QueryString["ed"];

            if (!string.IsNullOrEmpty(strStartDate) && !string.IsNullOrEmpty(strEndDate))
            {
                DateTime dStartDate = DateTime.Parse(strStartDate); //Util.Utilities.ParseDate(strStartDate);
                DateTime dEndDate = DateTime.Parse(strEndDate);//Util.Utilities.ParseDate(strEndDate);

                //dEndDate = Convert.ToDateTime(dEndDate.ToString("MM/dd/yyyy") + " 11:59:59 PM");

                if (dEndDate >= dStartDate)
                {
                    query = "Select * from documents d where DateCreated >= '" + dStartDate.ToString("yyyy-MM-dd") + "' and DateCreated<='" + dEndDate.ToString("yyyy-MM-dd") + "' order by lastmodified desc";

                    var docs = dao.GetFullDocumentDetails(cnxnString, logPath);

                    List<DocumentDetails> documents = new List<DocumentDetails>();

                    foreach (DocumentDetails doc in docs)
                    {
                        doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                        doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                        if (doc.DateCreated >= dStartDate && doc.DateCreated <= dEndDate)
                        {
                            if (!string.IsNullOrEmpty(searchTerm))
                            {
                                if (doc.Author.ToLower().Contains(searchTerm) || doc.FileTags.ToLower().Contains(searchTerm) || doc.TaggedUsers.ToLower().Contains(searchTerm) || doc.MessageHeader.ToLower().Contains(searchTerm) || doc.FriendlyName.ToLower().Contains(searchTerm))
                                {
                                    documents.Add(doc);
                                }
                            }
                            else
                                documents.Add(doc);
                        }
                    }

                    context.Response.Write(jss.Serialize(documents));
                }
            }
        }
        if (searchType == "dl")
        {
            string strDeadLineDate = context.Request.QueryString["dld"];

            if (!string.IsNullOrEmpty(strDeadLineDate))
            {
                DateTime deadLineDate = Util.Utilities.ParseDate(strDeadLineDate);
                DateTime todayDate = Util.Utilities.ParseDate(DateTime.Now.ToString("dd-MM-yyyy"));

                if (deadLineDate >= todayDate)
                {
                    query = "Select * from documents d  order by lastmodified desc";

                    var docs = dao.GetFullDocumentDetails(cnxnString, logPath);

                    List<DocumentDetails> documents = new List<DocumentDetails>();

                    foreach (DocumentDetails doc in docs)
                    {
                        doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                        doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                        if (doc.IsDeadlineMentioned && Util.Utilities.ParseDate(doc.Deadline) <= deadLineDate.AddDays(3))
                        {
                            if (!string.IsNullOrEmpty(searchTerm))
                            {
                                if (doc.Author.ToLower().Contains(searchTerm) || doc.FileTags.ToLower().Contains(searchTerm) || doc.TaggedUsers.ToLower().Contains(searchTerm) || doc.MessageHeader.ToLower().Contains(searchTerm) || doc.FriendlyName.ToLower().Contains(searchTerm))
                                {
                                    documents.Add(doc);
                                }
                            }
                            else
                                documents.Add(doc);
                        }
                    }

                    context.Response.Write(jss.Serialize(documents));
                }
            }
        }
        if (searchType == "au")
        {
            string assignedUsers = context.Request.QueryString["user"];

            if (!string.IsNullOrEmpty(assignedUsers))
            {
                assignedUsers = assignedUsers.Substring(0, assignedUsers.LastIndexOf(","));

                string[] users = assignedUsers.Split(',');

                query = "Select * from documents d order by lastmodified desc";

                var docs = dao.GetFullDocumentDetails(cnxnString, logPath);

                List<DocumentDetails> documents = new List<DocumentDetails>();

                foreach (DocumentDetails doc in docs)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                    doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                    foreach (string user in users)
                    {
                        if (doc.TaggedUsers.ToLower().Contains(user.ToLower()))
                        {
                            if (!string.IsNullOrEmpty(searchTerm))
                            {
                                if (doc.Author.ToLower().Contains(searchTerm) || doc.FileTags.ToLower().Contains(searchTerm) || doc.TaggedUsers.ToLower().Contains(searchTerm) || doc.MessageHeader.ToLower().Contains(searchTerm) || doc.FriendlyName.ToLower().Contains(searchTerm))
                                {
                                    documents.Add(doc);
                                }
                            }
                            else
                                documents.Add(doc);
                        }
                    }
                }

                context.Response.Write(jss.Serialize(documents));
            }
        }
        if (searchType == "dt")
        {
            string documentType = context.Request.QueryString["dt"];

            query = "Select * from documents d order by lastmodified desc limit 200";

            var docs = dao.GetDocumentDetails(query, cnxnString, logPath);

            List<DocumentDetails> documents = new List<DocumentDetails>();

            if (documentType == "all")
            {

                foreach (DocumentDetails doc in docs)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                    doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                    MessageHeader msgHeader = jss.Deserialize<MessageHeader>(doc.MessageHeader);

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        if (doc.Author.ToLower().Contains(searchTerm) || doc.FileTags.ToLower().Contains(searchTerm) || doc.TaggedUsers.ToLower().Contains(searchTerm) || doc.MessageHeader.ToLower().Contains(searchTerm) || doc.FriendlyName.ToLower().Contains(searchTerm))
                        {
                            documents.Add(doc);
                        }
                    }
                    else
                        documents.Add(doc);
                }
            }
            else
            {
                foreach (DocumentDetails doc in docs)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                    doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                    MessageHeader msgHeader = jss.Deserialize<MessageHeader>(doc.MessageHeader);

                    if (msgHeader.DocumentType.ToLower() == documentType)
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            if (doc.Author.ToLower().Contains(searchTerm) || doc.FileTags.ToLower().Contains(searchTerm) || doc.TaggedUsers.ToLower().Contains(searchTerm) || doc.MessageHeader.ToLower().Contains(searchTerm) || doc.FriendlyName.ToLower().Contains(searchTerm))
                            {
                                documents.Add(doc);
                            }
                        }
                        else
                            documents.Add(doc);
                    }
                }
            }

            context.Response.Write(jss.Serialize(documents));
        }

        else if (searchType == "nt") // for notifications
        {
            DateTime todayDate = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy"));

            query = "Select * from documents d  order by lastmodified desc";

            var docs = dao.GetFullDocumentDetails(cnxnString, logPath);

            List<DocumentDetails> documents = new List<DocumentDetails>();

            foreach (DocumentDetails doc in docs)
            {
                doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
                doc.FileTags = HttpUtility.UrlDecode(doc.FileTags);

                if (doc.IsDeadlineMentioned && Util.Utilities.ParseDate(doc.Deadline) <= todayDate.AddDays(7) && Util.Utilities.ParseDate(doc.Deadline) >= todayDate)
                {
                    doc.DateCreated = Util.Utilities.ParseDate(doc.Deadline);

                    MessageHeader msgHeader = jss.Deserialize<MessageHeader>(doc.MessageHeader);

                    doc.DocumentType = msgHeader.DocumentType;
                    documents.Add(doc);
                }
            }

            documents.Sort(delegate(DocumentDetails d1, DocumentDetails d2) { return d1.DateCreated.CompareTo(d2.DateCreated); });

            context.Response.Write(jss.Serialize(documents));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}