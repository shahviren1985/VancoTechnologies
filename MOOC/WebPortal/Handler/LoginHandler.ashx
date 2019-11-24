<%@ WebHandler Language="C#" Class="LoginHandler" %>

using System;
using System.Web;
using ITM.Courses.LogManager;
using System.Web.SessionState;

public class LoginHandler : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    string loggedInUser;
    string connString;
    string logPath;
    string configFilePath;   
    
    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.QueryString.Count > 0)
            {
                context.Session["UserName"] = context.Request.QueryString["UserN"];
                context.Session["ConnectionString"] = context.Request.QueryString["ConnS"];
                context.Session["LogFilePath"] = context.Server.MapPath(context.Request.QueryString["LogFP"]);
                context.Session["ReleaseFilePath"] = context.Request.QueryString["ReleaseFP"];
                context.Session["RedirectURL"] = context.Request.QueryString["RedirectURL"];
                context.Session["CollegeName"] = context.Request.QueryString["CollegeN"];
                context.Session["RelativeConnectionString"] = context.Request.QueryString["RelativeConnS"];

                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                
                context.Response.Redirect(context.Session["RedirectURL"].ToString());
            }
        }
        catch (Exception ex)
        {
            logger.Error("LoginHandler-Handler", "ProcessRequest", "Error Occured While Loading page", ex, logPath);
            throw ex;
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