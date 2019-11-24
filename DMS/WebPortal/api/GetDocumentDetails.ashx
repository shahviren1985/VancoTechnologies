<%@ WebHandler Language="C#" Class="GetDocumentDetails" %>

using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;

public class GetDocumentDetails : IHttpHandler, IRequiresSessionState
{
    long ticks = DateTime.Now.Ticks;
    JavaScriptSerializer jss = new JavaScriptSerializer();
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    Logger logger = new Logger();

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string docId = context.Request.QueryString["id"];
        string author = context.Session["UserName"].ToString();

        try
        {
            if (string.IsNullOrEmpty(docId) || docId == "0")
            {
                context.Response.Write("{\"Error\":\"Invalid document id.\"}");
                return;
            }

            DocumentDetails dd = dao.GetDocumentDetails(int.Parse(docId), cnxnString, logPath);

            if (dd == null || dd.Id == 0 || dd.FriendlyName == null)
            {
                logger.Debug("GetDocumentDetails-handler", "ProcessRequest", "Invalid document-id", logPath);
                context.Response.Write("{\"Error\":\"Invalid\"}");
            }
            
            dd.FriendlyName = HttpUtility.UrlDecode(dd.FriendlyName);
            dd.FileTags = HttpUtility.UrlDecode(dd.FileTags);

            //if (!string.IsNullOrEmpty(dd.ScanPath))
            //dd.ScanPath = author + "/" + dd.ScanPath;

            dd.StoreRoomLocation = HttpUtility.UrlDecode(dd.StoreRoomLocation);
            dd.Address = HttpUtility.UrlDecode(dd.Address);
            MessageHeader mh = jss.Deserialize<MessageHeader>(dd.MessageHeader);
            mh.From = HttpUtility.UrlDecode(mh.From);
            mh.Subject = HttpUtility.UrlDecode(mh.Subject);
            // added by wasim
            mh.OutwardNumber = HttpUtility.UrlDecode(mh.OutwardNumber);
            mh.InwardNumber = HttpUtility.UrlDecode(mh.InwardNumber);

            dd.MessageHeader = jss.Serialize(mh);
            context.Response.Write(jss.Serialize(dd));
        }
        catch (Exception ex)
        {
            logger.Error("GetDocumentDetails-handler", "ProcessRequest", ex.Message, ex, logPath);            
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