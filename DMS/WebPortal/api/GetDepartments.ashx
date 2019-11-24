<%@ WebHandler Language="C#" Class="GetDepartments" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class GetDepartments : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        //context.Response.ContentType = "application/json";
        //context.Response.Charset = "UTF-8";
        
        try
        {
            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();
            DepartmentDetailsDAO dao = new DepartmentDetailsDAO();
            List<DepartmentDetails> depts = dao.GetActiveDepartmentDetails(cnxnString, logPath);
            context.Response.Write(jss.Serialize(depts));
        }
        catch (Exception ex)
        {
            logger.Error("GetDepartments.ashx", "ProcessRequest", "Error occurred while getting all the departments", ex, logPath);
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