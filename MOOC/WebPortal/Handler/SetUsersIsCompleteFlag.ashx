<%@ WebHandler Language="C#" Class="SetUsersIsCompleteFlag" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class SetUsersIsCompleteFlag : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;
    string collegeName = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";

            if (context.Session["ConnectionString"] == null)
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));
                //context.Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());

                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Error: Invalid data or parameter!" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());

                new UserDetailsDAO().UpdateIsCompleteFlag(userId, true, cnxnString, logPath);

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("SaveTypingStats-Handler", "ProcessRequest", "Error occured while setting user iscomplete flag", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message }));
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