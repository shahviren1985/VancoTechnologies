<%@ WebHandler Language="C#" Class="GetQuiz" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetQuiz : IHttpHandler, IRequiresSessionState
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
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", Quiz = chapterQuizs }));
                //context.Response.Redirect("../Login.aspx", false);
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
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!", Quiz = chapterQuizs }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int chapterId = Convert.ToInt32(context.Request.QueryString["chapterid"]);
                int sectionId = Convert.ToInt32(context.Request.QueryString["sectionid"]);

                ChapterQuizMasterDAO chapterQuizDAO = new ChapterQuizMasterDAO();
                chapterQuizs = chapterQuizDAO.GetChapterQuizByChapterSection(chapterId, sectionId, cnxnString, logPath);

                if (chapterQuizs != null)
                {
                    ChapterQuizResponseDAO response = new ChapterQuizResponseDAO();

                    foreach (ChapterQuizMaster chp in chapterQuizs)
                    {

                        //added by anup on 13/11/2013 for reterving user response of user on previous click.          
                        ChapterQuizResponse userResponse = response.GetChapterQuizByUserQuesionId(userId, chp.Id, cnxnString, logPath);

                        if (userResponse == null)
                        {
                            chp.IsAnsGiven = false;
                        }
                        else
                        {
                            chp.IsAnsGiven = true;
                            chp.IsCorrect = userResponse.IsCorrect;
                            chp.AnswerText = userResponse.UserResponse;
                        }
                        // end

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
                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetQuiz-Success", Quiz = chapterQuizs }));

            }
        }
        catch (Exception ex)
        {
            logger.Error("GetQuiz-Handler", "ProcessRequest", "Error occured while getting chapter quizs", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, Quiz = chapterQuizs }));
        }
        finally
        { 
            /* killing sleeping connections */
            ITM.Courses.DAOBase.Database.KillConnections(cnxnString, logPath);
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