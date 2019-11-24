<%@ WebHandler Language="C#" Class="UpdateDocumentReadStatus" %>
using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;

public class UpdateDocumentReadStatus : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();
        string documentID = context.Request.Params["DocumentId"];

        DocumentReadStatusDAO dao = new DocumentReadStatusDAO();
        List<DocumentReadStatus> documents = dao.GetDocumentStatusDetails(int.Parse(documentID), cnxnString, logPath);
        bool isInserted = false;
        foreach (DocumentReadStatus status in documents)
        {
            if (status.ReadBy == userName)
            {
                isInserted = true;
                break;
            }
        }

        if (!isInserted)
            dao.AddDocumentStatusDetails(int.Parse(documentID), Util.Utilities.GetCurrentIndianDate().ToString("yyyy-MM-dd hh:mm:ss"), userName, cnxnString, logPath);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }

    }

}