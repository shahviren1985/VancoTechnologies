using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Configuration;

public partial class HintPage : PageBase
{
    string logPath;
    string cnxnString;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = string.Empty;

        if (Session["ConnectionString"] == null)
        {
            cnxnString = ConfigurationManager.ConnectionStrings["Annonymus_CnxnString"].ToString();
            logPath = ConfigurationManager.AppSettings["Annonymus_LogPath"].ToString();
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
            userName = Session["UserName"].ToString();
        }

        if (Request.QueryString.Count > 0)
        {
            int courseId = Convert.ToInt32(Request.QueryString["id"]);

            ITM.Courses.DAO.CourseDetail courseDetails = new CourseDetailsDAO().GetCourseByCourseId(courseId, cnxnString, logPath);

            if (courseDetails != null)
            {
                //divBreadCrumb.InnerHtml = courseDetails.CourseName;
            }
        }

        UserDetails user = new UserDetailsDAO().GetUserByUserName(userName, cnxnString, logPath);

        if (user != null && user.IsNewUser)
        {
            Response.Redirect("~/ChangePassword.aspx", false);
        }
    }
}