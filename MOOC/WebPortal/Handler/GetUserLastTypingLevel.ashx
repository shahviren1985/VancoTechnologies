<%@ WebHandler Language="C#" Class="GetUserLastTypingLevel" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetUserLastTypingLevel : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";

            if (context.Session["ConnectionString"] == null)
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", LastTypingLevel = 1 }));
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!", LastTypingLevel = 1 }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());

                StudentTypingStatsDAO studentTypingDAO = new StudentTypingStatsDAO();
                int lastTypingLevel = studentTypingDAO.GetUsersLastTypingLevel(userId, cnxnString, logPath);

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Get last typing level-Success", LastTypingLevel = lastTypingLevel }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetUserLastTypingLevel-Handler", "ProcessRequest", "Error occured while getting user last typing level", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, LastTypingLevel = 1 }));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}