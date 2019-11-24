<%@ WebHandler Language="C#" Class="SaveUserTestStatus" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class SaveUserTestStatus : IHttpHandler, IRequiresSessionState
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
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));                
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Request.QueryString["testid"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int testId = Convert.ToInt32(context.Request.QueryString["testid"].ToString());
                
                UserCompletedTestTracker obj = new UserCompletedTestTrackerDAO().GetUserCompletedTest(testId, userId, cnxnString, logPath);
                if (obj == null)
                    new UserCompletedTestTrackerDAO().AddUserCompletedTest(testId, userId, cnxnString, logPath);

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("SaveQuiz-Handler", "ProcessRequest", "Error occured while saving user test status", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message }));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}