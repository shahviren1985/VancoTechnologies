<%@ WebHandler Language="C#" Class="GetDocumentComments" %>


using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class GetDocumentComments : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            // Check the document id - if it is null then return error code
            // if the document does not exist then return the error code
            // if document exist but no comments yet added, return appropriate message
            // if comments exist then return the comments json

            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();
            string docId = context.Request.QueryString["id"];

            if (string.IsNullOrEmpty(docId) || docId == "0")
            {
                context.Response.Write("{\"Error\":\"Please save the document in order to get the associated comments.\"}");
                return;
            }

            CommentDetailsDAO dao = new CommentDetailsDAO();
            List<CommentDetails> comments = dao.GetCommentsByDocumentId(int.Parse(docId), cnxnString, logPath);

            if (comments == null || comments.Count <= 0)
            {
                context.Response.Write("{\"Error\":\"Current document does not have any associated comments. Please add new comments.\"}");
                return;
            }
            else
            {
                context.Response.Write(jss.Serialize(comments));
                return;
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetDocumentComments.ashx", "ProcessRequest", "Error occurred while getting document comments", ex, logPath);
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