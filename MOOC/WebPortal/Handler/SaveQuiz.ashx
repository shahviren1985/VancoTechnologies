<%@ WebHandler Language="C#" Class="SaveQuiz" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class SaveQuiz : IHttpHandler, IRequiresSessionState
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
                //context.Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());

                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()) || string.IsNullOrEmpty(context.Request.QueryString["questionId"]) ||
                    string.IsNullOrEmpty(context.Request.QueryString["useranswer"]) || string.IsNullOrEmpty(context.Request.QueryString["iscurrect"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int questionId = Convert.ToInt32(context.Request.QueryString["questionId"].ToString());
                string userAnswer = context.Request.QueryString["useranswer"].ToString();
                bool isCurrect = Convert.ToBoolean(context.Request.QueryString["iscurrect"]);

                ChapterQuizResponseDAO chapterQuizDAO = new ChapterQuizResponseDAO();
                ChapterQuizResponse quizResp = chapterQuizDAO.GetChapterQuizByUserQuesionId(userId, questionId, cnxnString, logPath);
                
                if (quizResp == null)
                {
                    chapterQuizDAO.AddChaptersQuizResponse(userId, questionId, userAnswer, isCurrect, DateTime.Now, cnxnString, logPath);
                }
                
                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("SaveQuiz-Handler", "ProcessRequest", "Error occured while saving quiz", ex, logPath);
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