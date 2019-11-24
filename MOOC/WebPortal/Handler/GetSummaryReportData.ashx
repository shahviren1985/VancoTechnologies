<%@ WebHandler Language="C#" Class="GetSummaryReportData" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class GetSummaryReportData : IHttpHandler, IRequiresSessionState
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
            }

            List<UserDetails> userCourses = new UserDetailsDAO().GetUniqueCoursesList(cnxnString, logPath);

            if (userCourses != null)
            {
                List<CompletedCoursesCount> completedCourseCounts = new List<CompletedCoursesCount>();
                List<NotStartedCourseCount> notStartedCourseCounts = new List<NotStartedCourseCount>();
                int completeTotal = 0, notStartedTotal = 0;
                
                foreach (UserDetails userCourse in userCourses)
                {
                    int completeCount = 0, notStartedCount = 0;

                    // completed course
                    List<UserDetails> usersCompleteCourse = new StudentReportManager().GetStudentReportWhoCompletedCourse(userCourse.Course, cnxnString, logPath);

                    if (usersCompleteCourse != null)
                    {
                        completeCount = usersCompleteCourse.Count;
                    }

                    completeTotal += completeCount;
                    completedCourseCounts.Add(new CompletedCoursesCount { Course = userCourse.Course, NumberOfStudents = completeCount });
                    
                    List<UserDetails> usersNotStartedCourse = new StudentReportManager().GetStudentNotStartedCourse(userCourse.Course, cnxnString, logPath);

                    if (usersNotStartedCourse != null)
                    {
                        notStartedCount = usersNotStartedCourse.Count;
                    }
                    
                    notStartedTotal += notStartedCount;
                    notStartedCourseCounts.Add(new NotStartedCourseCount { Course = userCourse.Course, NumberOfStudents = notStartedCount });
                }
                
                completedCourseCounts.Add(new CompletedCoursesCount { Course = "Total", NumberOfStudents = completeTotal });
                notStartedCourseCounts.Add(new NotStartedCourseCount { Course = "Total", NumberOfStudents = notStartedTotal });

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Successfully Generated", CompletedCourse = completedCourseCounts, NotStartedCourse = notStartedCourseCounts }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetSummaryReportData-Handler", "ProcessRequest", "Error occured while Getting summary report data", ex, logPath);
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