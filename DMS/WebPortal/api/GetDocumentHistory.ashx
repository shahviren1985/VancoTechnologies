<%@ WebHandler Language="C#" Class="GetDocumentHistory" %>

using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;
using AA.LogManager;

public class GetDocumentHistory : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();

    JavaScriptSerializer jss = new JavaScriptSerializer();
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    UserDetailsDAO userDao = new UserDetailsDAO();

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();
        string userFullName = context.Session["FirstName"].ToString() + " " + context.Session["LastName"].ToString() + " (" + context.Session["UserName"].ToString() + ")";
        try
        {

            string strDocumentId = context.Request.QueryString["id"];
            int documentId;
            if (!string.IsNullOrEmpty(strDocumentId) && int.TryParse(strDocumentId, out documentId))
            {

                List<DocumentDetails> documemtHistory = new DocumentDetailsDAO().GetDocumentsHistory(documentId, cnxnString, logPath);

                List<DocumentDetails> filterDocumemtHistory = new List<DocumentDetails>();

                foreach (DocumentDetails doc in documemtHistory)
                {
                    doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);

                    MessageHeader mh = jss.Deserialize<MessageHeader>(doc.MessageHeader);
                    mh.From = HttpUtility.UrlDecode(mh.From);
                    doc.MessageHeader = jss.Serialize(mh);

                    if (context.Session["RoleType"].ToString() == "2" || context.Session["RoleType"].ToString() == "3")
                    {
                        filterDocumemtHistory.Add(doc);
                    }
                    else
                    {
                        if (doc.Author == userName || doc.MessageHeader.Contains(userFullName) || doc.TaggedUsers.Contains(userName))
                        {
                            filterDocumemtHistory.Add(doc);
                        }
                    }
                }

                filterDocumemtHistory.Sort(
                    delegate(DocumentDetails p1, DocumentDetails p2)
                    {   
                        return -p1.DateCreated.CompareTo(p2.DateCreated); 
                    });

                context.Response.Write(jss.Serialize(filterDocumemtHistory));
            }
            else
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Empty query string" }));
            }

        }
        catch (Exception ex)
        {
            logger.Error("GetDocumentHistory.ashx", "ProcessRequest", ex.Message, ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Unable to get document history" }));
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