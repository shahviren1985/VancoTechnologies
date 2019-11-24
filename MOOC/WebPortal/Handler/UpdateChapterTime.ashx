<%@ WebHandler Language="C#" Class="UpdateChapterTime" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class UpdateChapterTime : IHttpHandler, IRequiresSessionState
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
                                string.IsNullOrEmpty(context.Request.QueryString["sectionid"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Invalid Data" }));
                    return;     
                }

                int userId = Convert.ToInt32(context.Session["UserId"]);
                int chapterId = Convert.ToInt32(context.Request.QueryString["chapterid"]);
                int sectionId = Convert.ToInt32(context.Request.QueryString["sectionid"]);

                UserTimeTrackerDAO userTimeDAO = new UserTimeTrackerDAO();
                UserTimeTracker userTimeTracker = userTimeDAO.GetUserTimeTakerByUserChapterSectionId(userId, chapterId, sectionId, cnxnString, logPath);

                if (userTimeTracker == null)
                {
                    userTimeDAO.AddTimingDetails(userId, chapterId, sectionId, 10, cnxnString, logPath);
                }
                else
                {
                    userTimeDAO.UpdateUserTimeTracker(userTimeTracker.Id, (userTimeTracker.TimeSpent + 10), cnxnString, logPath);
                }
                
                context.Response.Write(jss.Serialize(new { Status = "Ok" }));     
            }
        }
        catch (Exception ex)
        {
            logger.Error("UpdateChapterTime-Handler", "ProcessRequest", "Error occured while updating chapter time", ex, logPath);
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