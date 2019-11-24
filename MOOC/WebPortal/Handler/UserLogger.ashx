<%@ WebHandler Language="C#" Class="UserLogger" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class UserLogger : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();
    
    public void ProcessRequest(HttpContext context)
    {
        string cnxnString = string.Empty;
        string logPath = string.Empty;
        string configFilePath = string.Empty;

        try
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";
            
            logger.Debug("UserLogger-Handler", "ProcessRequest", "Checking sessions variables", logPath);
            
            if (context.Session["ConnectionString"] == null)
            {
                logger.Debug("UserLogger-Handler", "ProcessRequest", "sessions variables are empty thats way redirect to login.html page", logPath);
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));                
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());
            }

            string clientIpAddress = context.Request.UserHostAddress;
            string userId = context.Request.QueryString["userid"];

            UserLoggerDAO userLogger = new UserLoggerDAO();
            userLogger.AddLogedUser(int.Parse(userId), DateTime.Now, clientIpAddress, cnxnString, logPath);

            context.Response.Write(jss.Serialize(new { Status = "Ok" }));            
        }
        catch (Exception ex)
        {
            logger.Error("UserLogger-Handler", "ProcessRequest", "Error occured while inserting UserLogger details", ex, logPath);            
            context.Response.Write(jss.Serialize(new { Status = "Error" }));            
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