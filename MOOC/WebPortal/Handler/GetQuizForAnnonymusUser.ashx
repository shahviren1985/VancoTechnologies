<%@ WebHandler Language="C#" Class="GetQuizForAnnonymusUser" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Configuration;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetQuizForAnnonymusUser : IHttpHandler, IRequiresSessionState
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

            cnxnString = ConfigurationManager.ConnectionStrings["Annonymus_CnxnString"].ToString();
            logPath = ConfigurationManager.AppSettings["Annonymus_LogPath"].ToString();

            if (string.IsNullOrEmpty(context.Request.QueryString["chapterid"]) || string.IsNullOrEmpty(context.Request.QueryString["sectionid"]))
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!", Quiz = chapterQuizs }));
                return;
            }
            
            int chapterId = Convert.ToInt32(context.Request.QueryString["chapterid"]);
            int sectionId = Convert.ToInt32(context.Request.QueryString["sectionid"]);

            ChapterQuizMasterDAO chapterQuizDAO = new ChapterQuizMasterDAO();
            chapterQuizs = chapterQuizDAO.GetChapterQuizByChapterSection(chapterId, sectionId, cnxnString, logPath);

            if (chapterQuizs != null)
            {
                ChapterQuizResponseDAO response = new ChapterQuizResponseDAO();

                foreach (ChapterQuizMaster chp in chapterQuizs)
                {
                    string json = chp.QuestionOption.Replace("(", "").Replace(")", "");
                    json = chp.QuestionOption.Replace("\\", "");

                    chp.QuestionOptionList = jss.Deserialize(json, typeof(List<QuestionOptions>)) as List<QuestionOptions>;

                    json = chp.AnswerOption.Replace("(", "").Replace(")", "");
                    json = chp.AnswerOption.Replace("\\", "");
                    chp.AnswerOptionList = jss.Deserialize(json, typeof(List<AnswerOptions>)) as List<AnswerOptions>;

                    int count = chp.AnswerOptionList.FindAll(ss => ss.IsCurrect == true).Count;

                    if (count > 1)
                        chp.IsMultiTrueAnswer = true;
                    else
                        chp.IsMultiTrueAnswer = false;
                }
            }

            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetQuizForAnnonymusUser-Success", Quiz = chapterQuizs }));

        }
        catch (Exception ex)
        {
            logger.Error("GetQuiz-Handler", "ProcessRequest", "Error occured while getting quiz for annonymus user", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, Quiz = chapterQuizs }));
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