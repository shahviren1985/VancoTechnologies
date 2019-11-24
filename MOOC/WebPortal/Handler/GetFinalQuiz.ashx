<%@ WebHandler Language="C#" Class="GetFinalQuiz" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetFinalQuiz : IHttpHandler, IRequiresSessionState
{

    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        List<FinalQuizMaster> finalQuizs = new List<FinalQuizMaster>();
        CourseTests courseTest = null;
        
        try
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";

            if (context.Session["ConnectionString"] == null)
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", FinalQuiz = finalQuizs }));
                //context.Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                int testId = 0;
                int courseId = 0;

                if (!string.IsNullOrEmpty(context.Request.QueryString["testid"]) && context.Request.QueryString["testid"].ToString() != "undefined")
                {
                    testId = Convert.ToInt32(context.Request.QueryString["testid"]);
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["courseid"]) && context.Request.QueryString["courseid"].ToString() != "undefined")
                {
                    courseId = Convert.ToInt32(context.Request.QueryString["courseid"]);
                }

                if (courseId == 0)
                {
                    courseId = 1; 
                }
                
                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                
                FinalQuizMasterDAO finalQuizDAO = new FinalQuizMasterDAO();
                
                if (testId == 0)
                {                    
                    //finalQuizs = finalQuizDAO.GetAllQuizs(cnxnString, logPath);
                    finalQuizs = finalQuizDAO.GetAllQuizsByCourse(courseId, cnxnString, logPath);
                }
                else
                {                    
                    courseTest = new CourseTestsDAO().GetCourseTestsById(testId, cnxnString, logPath);
                    finalQuizs = finalQuizDAO.GetQuizbyTest(courseTest.Chapters, cnxnString, logPath);
                }

                List<FinalQuizMaster> randomFinalQuiz = new List<FinalQuizMaster>();

                if (finalQuizs != null)
                {
                    foreach (FinalQuizMaster fqz in finalQuizs)
                    {
                        string json = fqz.QuestionOption.Replace("(", "").Replace(")", "");
                        json = fqz.QuestionOption.Replace("\\", "");

                        fqz.QuestionOptionList = jss.Deserialize(json, typeof(List<QuestionOptions>)) as List<QuestionOptions>;

                        json = fqz.AnswerOption.Replace("(", "").Replace(")", "");
                        json = fqz.AnswerOption.Replace("\\", "");
                        fqz.AnswerOptionList = jss.Deserialize(json, typeof(List<AnswerOptions>)) as List<AnswerOptions>;

                        int count = fqz.AnswerOptionList.FindAll(ss => ss.IsCurrect == true).Count;

                        if (count > 1)
                            fqz.IsMultiTrueAnswer = true;
                        else
                            fqz.IsMultiTrueAnswer = false;
                    }
                    
                    int finalQuizCount = 0;
                    
                    if (testId == 0)
                    {
                        finalQuizCount = Convert.ToInt32(ConfigurationManager.AppSettings["FinalQuizCount"]);
                        
                        // added by vasim on 27-nov-2014
                        if(finalQuizCount > finalQuizs.Count)
                        {
                            finalQuizCount = finalQuizs.Count;
                        }
                    }
                    else
                    {
                        finalQuizCount = finalQuizs.Count;
                    }

                    if (courseTest != null)
                    {
                        if (finalQuizs.Count > courseTest.TotalQuestions)
                            finalQuizCount = courseTest.TotalQuestions;
                    }
                    
                    int totalNoOfQuesion = finalQuizCount;
                    int remianinQuizCount = finalQuizCount;

                    List<FinalQuizResponse> finalQuizRes = new FinalQuizResponseDAO().GetAllFinalQuizResponseByUserId(userId, cnxnString, logPath);
                    
                    if (finalQuizRes != null)
                    {
                        foreach (FinalQuizResponse fqr in finalQuizRes)
                        {
                            FinalQuizMaster fq = finalQuizs.Find(ff => ff.Id == fqr.QuestionId);
                            if (fq != null)
                                randomFinalQuiz.Add(fq);
                        }

                        //remianinQuizCount = totalNoOfQuesion - finalQuizRes.Count;
                        remianinQuizCount = totalNoOfQuesion - randomFinalQuiz.Count;
                    }

                    for (int i = 0; i < remianinQuizCount; i++)                    
                    {
                        int index = new Random().Next(0, finalQuizs.Count);

                        if (!randomFinalQuiz.Exists(fq => fq.Id == finalQuizs[index].Id))
                        {
                            //FinalQuizMaster fq = finalQuizs.Find(ff => ff.Id == index);
                            FinalQuizMaster fq = finalQuizs[index];
                            if (fq != null)
                                randomFinalQuiz.Add(fq);
                            else
                                i--;
                        }
                        else
                        {
                            i--;
                        }
                    }
                }

                //context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetQuiz-Success", FinalQuiz = finalQuizs }));
                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetQuiz-Success", FinalQuiz = randomFinalQuiz }));

            }
        }
        catch (Exception ex)
        {
            logger.Error("GetQuiz-Handler", "ProcessRequest", "Error occured while getting final quiz", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, FinalQuiz = finalQuizs }));
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