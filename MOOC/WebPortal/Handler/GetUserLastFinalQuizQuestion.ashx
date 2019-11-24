<%@ WebHandler Language="C#" Class="GetUserLastFinalQuizQuestion" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetUserLastFinalQuizQuestion : IHttpHandler, IRequiresSessionState
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
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", LastAttemptQuestion = 0 }));
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()) || string.IsNullOrEmpty(context.Request.QueryString["testid"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!", LastAttemptQuestion = 0 }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int testId = Convert.ToInt32(context.Request.QueryString["testid"]);
                
                FinalQuizResponseDAO chapterQuizDAO = new FinalQuizResponseDAO();
                int lastQuizId = chapterQuizDAO.GetLastAttemptFinalQuizQuestion(userId, testId, cnxnString, logPath);

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetQuiz-Success", LastAttemptQuestion = lastQuizId }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetUserLastFinalQuizQuestion-Handler", "ProcessRequest", "Error occured while getting user last quiz id", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, LastAttemptQuestion = 0 }));
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