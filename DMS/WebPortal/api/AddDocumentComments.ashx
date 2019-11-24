<%@ WebHandler Language="C#" Class="AddDocumentComments" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class AddDocumentComments : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();
            string docId = context.Request.QueryString["id"];
            string userName = context.Session["UserName"].ToString();
            string comment = context.Request.QueryString["c"];

            if (string.IsNullOrEmpty(docId))
            {
                context.Response.Write("{\"Error\":\"Please save the document in order to add the associated comments.\"}");
            }
            else if (string.IsNullOrEmpty(comment))
            {
                context.Response.Write("{\"Error\":\"Please add some comment to save for this document. Currently comments field is left empty\"}");
            }
            else
            {
                // Save the comment
                CommentDetailsDAO dao = new CommentDetailsDAO();
                DocumentDetailsDAO docDao = new DocumentDetailsDAO();

                DocumentDetails dd = new DocumentDetailsDAO().GetDocumentDetails(int.Parse(docId), cnxnString, logPath);

                if (dd == null || dd.Id == 0 || dd.FriendlyName == null)
                {
                    logger.Debug("AddDocumentComments", "ProcessRequest", "Documents does not exists", logPath);
                    context.Response.Write("{\"Error\":\"It seems this is incorrect document. You can not add comments for this document\"}");
                    return;
                }
                
                CommentDetails cd = dao.InsertComments(userName, int.Parse(docId), comment, Util.Utilities.GetCurrentIndianDate().ToString("yyyy-MM-dd hh:mm:ss"), true, false, userName, cnxnString, logPath);
                docDao.UpdateDocumentDetails(int.Parse(docId), userName, "COMMENT ADDED", cnxnString, logPath);

                context.Response.Write(jss.Serialize(cd));
            }
        }
        catch (Exception ex)
        {
            logger.Error("AddDocumentComments", "ProcessRequest", ex, logPath);
            context.Response.Write("{\"Error\":\"An error occurred while adding new comment for this document.\"}");
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