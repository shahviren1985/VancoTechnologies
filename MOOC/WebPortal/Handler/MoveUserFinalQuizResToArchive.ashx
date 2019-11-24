<%@ WebHandler Language="C#" Class="MoveUserFinalQuizResToArchive" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class MoveUserFinalQuizResToArchive : IHttpHandler, IRequiresSessionState
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

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                
                FinalQuizResponseDAO finalQuizDAO = new FinalQuizResponseDAO();
                List<FinalQuizResponse> finalQuizResp = finalQuizDAO.GetAllFinalQuizResponseByUserId(userId, cnxnString, logPath);

                ArchiveFinalQuizResponseDAO archiveFinalQuizDAO = new ArchiveFinalQuizResponseDAO();
                if (finalQuizResp != null)
                {
                    foreach (FinalQuizResponse quizRes in finalQuizResp)
                    {
                        archiveFinalQuizDAO.AddFinalQuizResponse(userId, quizRes.QuestionId, quizRes.UserResponse, quizRes.IsCorrect, quizRes.DateTime, cnxnString, logPath);
                    }

                    finalQuizDAO.DeleteFinalQiuzRespByUserId(userId, cnxnString, logPath);
                }

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("MoveUserFinalQuizResToArchive-Handler", "ProcessRequest", "Error occured while moving final quiz response to archive", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message }));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}