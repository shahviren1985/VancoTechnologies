<%@ WebHandler Language="C#" Class="GetTypingStatusChart" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetTypingStatusChart : IHttpHandler, IRequiresSessionState
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

            if (context.Session["ConnectionString"] == null || context.Session["UserId"] == null)
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());

                TypingStatsChart tChart = new PrepareTypingStatsChart().GetTypingStatsChartData(userId, true, 9, cnxnString, logPath);

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success", TypingChartData = tChart }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetTypingStatusChart-Handler", "ProcessRequest", "Error occured while getting recently completed tests", ex, logPath);
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