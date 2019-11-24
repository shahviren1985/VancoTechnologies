<%@ WebHandler Language="C#" Class="GetUserRecentActivities" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetUserRecentActivities : IHttpHandler, IRequiresSessionState
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

                List<UserActivityTraker> userActivites = new UserActivityTrackerDAO().GetTop5ActivitiesUserId(userId, cnxnString, logPath);
                List<UserActivityObject> userActList = new List<UserActivityObject>();
                
                if (userActivites != null)
                {
                    foreach (UserActivityTraker obj in userActivites)
                    {
                        obj.UserActivity = new UserActivityObject();
                        obj.UserActivity = jss.Deserialize(obj.Activity, typeof(UserActivityObject)) as UserActivityObject;

                        obj.UserActivity.Datetime = obj.DateCreated.ToString("MMMM dd") + " at " + obj.DateCreated.ToString("hh:mm tt");

                        if (obj.UserActivity.ActivityType == "1" || obj.UserActivity.ActivityType == "2" || obj.UserActivity.ActivityType == "5")
                        {
                            string[] arrayChapterSectionsIds = obj.UserActivity.Link.Split(new string[] { "||" }, StringSplitOptions.None);
                            string[] arrayChapterId = arrayChapterSectionsIds[0].Split(new char[] { ' ' });
                            string[] arrayChpId = arrayChapterId[0].Split(new char[] { '=' });
                            int chapterId = Convert.ToInt32(arrayChpId[1]);
                            ChapterDetails chp = new ChapterDetailsDAO().GetChapterDetails(chapterId, cnxnString, logPath);

                            if (chp != null)
                                obj.UserActivity.CourseId = chp.CourseId;
                        }

                        userActList.Add(obj.UserActivity);
                    }
                }

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success", UserActivities = userActList }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetUserRecentActivities-Handler", "ProcessRequest", "Error occured while getting recently completed tests", ex, logPath);
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