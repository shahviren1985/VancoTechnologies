<%@ WebHandler Language="C#" Class="GetRecentTestsByCourse" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetRecentTestsByCourse : IHttpHandler, IRequiresSessionState
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

                if (string.IsNullOrEmpty(context.Session["UserId"].ToString()))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int courseId = 1;
                bool isCompetedTest = true;

                if (!string.IsNullOrEmpty(context.Request.QueryString["iscomplete"]))
                {
                    isCompetedTest = Convert.ToBoolean(context.Request.QueryString["iscomplete"]);
                }
                
                if (!string.IsNullOrEmpty(context.Request.QueryString["courseid"]))
                {
                    courseId = Convert.ToInt32(context.Request.QueryString["courseid"]);
                }
                else
                {
                    int secid;
                    int chpterId = new UserChapterStatusDAO().GetUserMaxChapterSectionIdByUserId(userId, courseId, out secid, cnxnString, logPath);
                    ChapterDetails chp = new ChapterDetailsDAO().GetChapterDetails(chpterId, cnxnString, logPath);
                    if (chp != null)
                        courseId = chp.CourseId;
                }

                List<CourseTests> courseTest = new CourseTestsDAO().GetCompletedorRunningTestsByUser(userId, courseId, isCompetedTest, cnxnString, logPath);

                List<CourseTests> filteredCourseTests = new List<CourseTests>();
                
                if (!isCompetedTest)
                {
                    if (courseTest != null)
                    {
                        foreach (CourseTests ct in courseTest)
                        {
                            if (DateTime.Now <= ct.EndDate)
                            {
                                ct.DisplayEndDate = ct.EndDate.ToString("dd MMM yyyy");
                                filteredCourseTests.Add(ct);
                            }
                        }
                        
                    }

                    context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success", CourseTest = filteredCourseTests }));
                }
                else
                {
                    context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success", CourseTest = courseTest }));
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetRecentTestsByCourse-Handler", "ProcessRequest", "Error occured while getting recently completed tests", ex, logPath);
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