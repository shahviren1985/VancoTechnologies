<%@ WebHandler Language="C#" Class="GetUserQuizResStatus" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetUserQuizResStatus : IHttpHandler, IRequiresSessionState
{

    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        List<ChapterQuizMaster> chapterQuizs = new List<ChapterQuizMaster>();
        try
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";

            if (context.Session["ConnectionString"] == null)
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", IsUserFilledAllQuiz = false }));
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()) || string.IsNullOrEmpty(context.Request.QueryString["chapterid"])
                    || string.IsNullOrEmpty(context.Request.QueryString["sectionid"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!", IsUserFilledAllQuiz = false }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int chapterId = Convert.ToInt32(context.Request.QueryString["chapterid"]);
                int sectionId = Convert.ToInt32(context.Request.QueryString["sectionid"]);

                ChapterQuizMasterDAO chapterQuizDAO = new ChapterQuizMasterDAO();
                chapterQuizs = chapterQuizDAO.GetChapterQuizByChapterSection(chapterId, sectionId, cnxnString, logPath);

                bool isUserFillQuizChapSect = false;

                int quizCount = chapterQuizDAO.GetQuizCountByChapterSectionId(chapterId, sectionId, cnxnString, logPath);
                int quizRespCount = new ChapterQuizResponseDAO().GetQuizResponceCountByChapterSectionId(userId, chapterId, sectionId, cnxnString, logPath);

                if (quizCount == quizRespCount)
                {
                    isUserFillQuizChapSect = true;
                }

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetQuiz-Success", IsUserFilledAllQuiz = isUserFillQuizChapSect }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetQuiz-Handler", "ProcessRequest", "Error occured while getting user quiz response status", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, IsUserFilledAllQuiz = false }));
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