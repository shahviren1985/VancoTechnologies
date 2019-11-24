<%@ WebHandler Language="C#" Class="GetCourseCompletionGraphData" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetCourseCompletionGraphData : IHttpHandler, IRequiresSessionState
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
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", GraphData = "" }));
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserId"].ToString()))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", GraphData = "" }));
                    return;
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());
                int courseId = 0;
                string courseName = string.Empty;
                bool isNewCourse = true;

                List<CourseDetail> mappedCourses = new StudentCourseMapperDAO().GetMappedCourseByStudent(userId, cnxnString, logPath);

                if (mappedCourses == null)
                {
                    mappedCourses = new List<CourseDetail>();
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["courseid"]))
                {
                    courseId = Convert.ToInt32(context.Request.QueryString["courseid"]);
                    int secid;
                    int chpterId = new UserChapterStatusDAO().GetUserMaxChapterSectionIdByUserId(userId, courseId, out secid, cnxnString, logPath);

                    if (chpterId != 0)
                    {
                        isNewCourse = false;
                    }
                }
                else
                {
                    int secid;
                    int chpterId = new UserChapterStatusDAO().GetUserMaxChapterSectionIdByUserId(userId, courseId, out secid, cnxnString, logPath);
                    ChapterDetails chp = new ChapterDetailsDAO().GetChapterDetails(chpterId, cnxnString, logPath);

                    if (chp != null && mappedCourses.Find(mc => mc.Id == chp.CourseId) != null)
                    {
                        courseId = chp.CourseId;
                        courseName = mappedCourses.Find(mc => mc.Id == chp.CourseId).CourseName;

                        isNewCourse = false;
                    }
                    else
                    {
                        if (mappedCourses.Count > 0)
                        {
                            courseId = mappedCourses[0].Id;
                            courseName = mappedCourses[0].CourseName;

                            isNewCourse = true;
                        }
                    }
                }

                if (courseId == 0)
                {
                    // TO DO handle user not registered to any course 
                    // below is a tempary code for assigning courseid = 1 for our default course
                    courseId = 1;
                }

                List<CourseCompletionPercentChart> courseComlChart = new PrepareCourseCompletionPercentChart().GetCourseCompletionChartData(userId, courseId, cnxnString, logPath);

                bool isEmptyCourse = false;

                int sectionCount = new ChapterSectionDAO().GetTotalSectionCountByCourse(courseId, cnxnString, logPath);
                int emptySectionCount = new ChapterSectionDAO().GetEmptySecitonCountByCourse(courseId, cnxnString, logPath);

                if (sectionCount == emptySectionCount)
                {
                    isEmptyCourse = true;
                }

                if (string.IsNullOrEmpty(courseName))
                    courseName = new CourseDetailsDAO().GetCourseByCourseId(courseId, cnxnString, logPath).CourseName;

                context.Response.Write(jss.Serialize(new
                {
                    Status = "Ok",
                    Message = "Success",
                    GraphData = courseComlChart,
                    IsEmptyCourse = isEmptyCourse,
                    CourseId = courseId,
                    CourseName = courseName,
                    MappedCourses = mappedCourses,
                    IsNewCourse = isNewCourse
                }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetCourseCompletionGraphData-Handler", "ProcessRequest", "Error occured while getting course completion graph data", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message, ChapterId = 0, SectionId = 0 }));
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