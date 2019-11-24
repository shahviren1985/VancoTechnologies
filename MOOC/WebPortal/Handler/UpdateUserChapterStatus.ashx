<%@ WebHandler Language="C#" Class="UpdateUserChapterStatus" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class UpdateUserChapterStatus : IHttpHandler, IRequiresSessionState
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
                context.Response.Redirect("../Login.htm", false);
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserId"].ToString()) || string.IsNullOrEmpty(context.Request.QueryString["chapterid"]) ||
                                string.IsNullOrEmpty(context.Request.QueryString["sectionid"]) || string.IsNullOrEmpty(context.Request.QueryString["contentversion"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Invalid Data" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"]);
                int chapterId = Convert.ToInt32(context.Request.QueryString["chapterid"]);
                int sectionId = Convert.ToInt32(context.Request.QueryString["sectionid"]);
                string contentVersion = context.Request.QueryString["contentVersion"];

                UserChapterStatusDAO userTimeDAO = new UserChapterStatusDAO();
                UserChapterStatus userChapterStatus = userTimeDAO.GetUserChapterSectionByUserChapterSectionId(userId, chapterId, sectionId, cnxnString, logPath);

                if (userChapterStatus == null)
                {
                    userTimeDAO.AddUserChapterStatus(userId, chapterId, sectionId, contentVersion, DateTime.Now, cnxnString, logPath);
                }                

                context.Response.Write(jss.Serialize(new { Status = "Ok" }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("UpdateUserChapterStatus-Handler", "ProcessRequest", "Error occured while updating user chapter status", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error" }));
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