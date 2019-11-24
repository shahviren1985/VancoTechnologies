<%@ WebHandler Language="C#" Class="GetAutoInwardNumber" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class GetAutoInwardNumber : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            DocumentDetailsDAO dao = new DocumentDetailsDAO();
            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();
            string doctype = context.Request.Params["doctype"].ToString();

            string configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());
            
            string srNo = "0";

            if (doctype == "inward")
            {
                //srNo = "IR" + new XmlConfiguration().GetInwardDocumentId(logPath, string.Empty);
                srNo = "IR" + new XmlConfiguration().GetInwardDocumentId(logPath, configFilePath);
            }
            else if (doctype == "outward")
            {
                //srNo = "OR" + new XmlConfiguration().GetOutwardDocumentId(logPath, string.Empty);
                srNo = "OR" + new XmlConfiguration().GetOutwardDocumentId(logPath, configFilePath);
            }
            else
            {
                srNo = "" + (dao.GetLastDocument(cnxnString, logPath) + 1);
            }
            
            context.Response.Write(srNo);
        }
        catch (Exception ex)
        {
            logger.Error("GetAutoInwardNumber.ashx", "ProcessRequest", "Error occurred while getting new inward number", ex, logPath);
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