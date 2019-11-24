<%@ WebHandler Language="C#" Class="GetOnlineToolByDate" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetOnlineToolByDate : IHttpHandler, IRequiresSessionState
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
                cnxnString = ConfigurationManager.ConnectionStrings["Annonymus_CnxnString"].ToString();
                logPath = ConfigurationManager.AppSettings["Annonymus_LogPath"].ToString();
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());
            }

            if (string.IsNullOrEmpty(context.Request.QueryString["courseid"]) || context.Request.QueryString["courseid"] == "undefined")
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!"}));
                return;
            }

            int courseId = Convert.ToInt32(context.Request.QueryString["courseid"]);

            OnlineTools todaysTools = null;
            
            OnlineToolsDAO onlineToolDAO = new OnlineToolsDAO();
            List<OnlineTools> onlineTools = onlineToolDAO.GetAllOnlineToolsByCourseId(courseId, cnxnString, logPath);
            List<OnlineTools> filteredOnlineTools = new List<OnlineTools>();

            string todayDate = DateTime.Now.ToString("MM/dd/yyyy");
            
            if (onlineTools != null)
            {
                foreach (OnlineTools tools in onlineTools)
                {
                    string toolDisDate = tools.ToolDisplayDate.ToString("MM/dd/yyyy");
                    
                    if (todayDate == toolDisDate)
                    {
                        filteredOnlineTools.Add(tools);
                    }
                }
            }

            if (filteredOnlineTools.Count > 0)
            {
                Random rd = new Random();
                int index = rd.Next(0, filteredOnlineTools.Count);

                todaysTools = filteredOnlineTools[index];
            }
            
            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "GetChaptersByCourse-Success", OnlineTool = todaysTools }));
        }
        catch (Exception ex)
        {
            logger.Error("GetOnlineToolByDate-Handler", "ProcessRequest", "Error occured while Getting online tools", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message  }));
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