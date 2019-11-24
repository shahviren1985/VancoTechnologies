<%@ WebHandler Language="C#" Class="DiscardDocument" %>

using System;
using System.Web;
using AA.DAO;
using System.Web.SessionState;
using System.Web.Script.Serialization;
public class DiscardDocument : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string docId = string.Empty;

        DocumentDetailsDAO dao = new DocumentDetailsDAO();
        JavaScriptSerializer jss = new JavaScriptSerializer();

        try
        {
            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();

            foreach (string item in context.Request.Form)
            {
                switch (item)
                {
                    case "DocumentId":
                        docId = context.Request.Params[item];
                        break;
                }
            }

            dao.UpdateDocumentDetails(int.Parse(docId), context.Session["UserName"].ToString(), "DISCARDED", cnxnString, logPath);
            context.Response.Write(jss.Serialize("DISCARDED"));
        }
        catch (Exception ex)
        {
            //context.Response.Write("FAILED DISCARDING");
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