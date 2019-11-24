<%@ WebHandler Language="C#" Class="GetUserChapterStatus" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetUserChapterStatus : IHttpHandler, IRequiresSessionState
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
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire", ChapterId = 0, SectionId = 0 }));
                //context.Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

                if (string.IsNullOrEmpty(context.Session["UserId"].ToString()))
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data UserId shall not be empty!", ChapterId = 0, SectionId = 0 }));
                    return;
                }

                int courseId = 0;

                if (!string.IsNullOrEmpty(context.Request.QueryString["courseid"]))
                {
                    courseId = Convert.ToInt32(context.Request.QueryString["courseid"]);
                }

                int userId = Convert.ToInt32(context.Session["UserId"].ToString());

                int chapterId = 0;
                int sectionId = 0;


                UserChapterStatusDAO userTimeDAO = new UserChapterStatusDAO();
                chapterId = userTimeDAO.GetUserMaxChapterSectionIdByUserId(userId, courseId, out sectionId, cnxnString, logPath);

                if (chapterId == 0)
                {
                    List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);
                    if (chapters != null && chapters.Count > 0)
                    {
                        chapterId = chapters[0].Id;

                        List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chapterId, cnxnString, logPath);
                        if (sections != null && sections.Count > 0)
                        {
                            sectionId = sections[0].Id;
                        }
                    }
                    else
                    {
                        chapterId = 1;
                        sectionId = 1;
                        courseId = 1;
                    }
                }                
                
                ChapterDetails chpDetails = new ChapterDetailsDAO().GetChapterDetails(chapterId, cnxnString, logPath);

                CourseDetail courseDetail = new CourseDetailsDAO().GetCourseByCourseId(chpDetails.CourseId, cnxnString, logPath);

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success", ChapterId = chapterId, SectionId = sectionId, CourseId = chpDetails.CourseId, CourseName = courseDetail.CourseName }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("UpdateUserChapterStatus-Handler", "ProcessRequest", "Error occured while getting user chapter status", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "UpdateUserChapterStatus-Handler: " + ex.Message, ChapterId = 0, SectionId = 0 }));
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