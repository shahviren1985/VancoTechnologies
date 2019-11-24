<%@ WebHandler Language="C#" Class="MoveFilesToServer" %>

using System;
using System.Web;
using System.Web.SessionState;
using AA.DAO;
using AA.LogManager;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public class MoveFilesToServer : IHttpHandler, IRequiresSessionState
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
                List<FolderDetails> folders = new FolderDetailsDAO().GetAllChildFolders(-1, cnxnString, logPath);
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