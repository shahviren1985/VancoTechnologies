<%@ WebHandler Language="C#" Class="SaveTypingStats" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class SaveTypingStats : IHttpHandler, IRequiresSessionState
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

                if (string.IsNullOrEmpty(context.Session["UserName"].ToString()) || string.IsNullOrEmpty(context.Request.QueryString["timespan"]) || string.IsNullOrEmpty(context.Request.QueryString["level"]) ||
                    string.IsNullOrEmpty(context.Request.QueryString["accuracy"]) || string.IsNullOrEmpty(context.Request.QueryString["grossWPM"]) || string.IsNullOrEmpty(context.Request.QueryString["netWPM"]))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Error: Invalid data or parameter!" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int level = Convert.ToInt32(context.Request.QueryString["level"]);
                
                string timeSpan = context.Request.QueryString["timespan"].ToString();
                int accuracy = Convert.ToInt32(context.Request.QueryString["accuracy"].ToString());
                int grossWPM = Convert.ToInt32(context.Request.QueryString["grossWPM"]);
                int netWPM = Convert.ToInt32(context.Request.QueryString["netWPM"]);

                int timeSpanInSeconds = 0;
                string[] times = timeSpan.Split(new char[] { ':' });
                int m = Convert.ToInt32(times[0].Replace("m", ""));
                int s = Convert.ToInt32(times[1].Replace("s", ""));

                if (m > 0)
                {
                    timeSpanInSeconds = (m * 60) + s;
                }
                else
                {
                    timeSpanInSeconds = s;
                }
                
                
                StudentTypingStatsDAO statsDAO = new StudentTypingStatsDAO();

                StudentTypingStats studentStats = statsDAO.GetTypingStatsByLevelUserId(userId, level, cnxnString, logPath);

                if (studentStats == null)
                {
                    statsDAO.AddStudentTypingStats(userId, level, timeSpanInSeconds, accuracy, grossWPM, netWPM, cnxnString, logPath);
                }
                else
                {
                    statsDAO.UpdateTypingStatsById(studentStats.Id, timeSpanInSeconds, accuracy, grossWPM, netWPM, cnxnString, logPath);
                }
                
                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("SaveTypingStats-Handler", "ProcessRequest", "Error occured while saving typing stats", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message }));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}