using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;

public partial class TakeFinalQuiz : PageBase
{
    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect(Util.BASE_URL + "Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Session["LogFilePath"].ToString();
                configFilePath = Server.MapPath(Session["ReleaseFilePath"].ToString());
            }

            UserDetails user = new UserDetailsDAO().GetUserByUserName(Session["UserName"].ToString(), cnxnString, logPath);

            if (!user.IsCompleted)
            {
                //Response.Redirect(Util.BASE_URL + "CourseContent.aspx", false);
            }

            if (Request.QueryString.Count > 0)
            {
                int testId = Convert.ToInt32(Request.QueryString["testid"]);
                if (testId != 0)
                {
                    CourseTests test = new CourseTestsDAO().GetCourseTestsById(testId, cnxnString, logPath);
                    if (test != null)
                    {
                        hfCourseName.Value = test.CourseName;
                    }
                    else
                    {
                        hfCourseName.Value = "Taking final quiz";
                    }
                }
                else
                {
                    int courseId = Convert.ToInt32(Request.QueryString["courseid"]);

                    CourseDetail course = new CourseDetailsDAO().GetCourseByCourseId(courseId, cnxnString, logPath);
                    if (course != null)
                    {
                        hfCourseName.Value = course.CourseName;
                    }
                    else
                    {
                        hfCourseName.Value = "Taking final quiz";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}