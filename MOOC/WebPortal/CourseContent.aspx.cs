using ITM.Courses.DAO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;

public partial class CourseContent : PageBase
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString;
    string logPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        var use = Request.QueryString.Count;
        string name = RegionInfo.CurrentRegion.DisplayName;

        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("~/Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            //configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
        }

        if (Request.QueryString.Count == 0)
        {
            return;
        }


        CourseDetail course = new CourseDetailsDAO().GetCourseByCourseId(int.Parse(Request.QueryString[0]), cnxnString, logPath);
        hfCourseName.Value = course.CourseName;

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateCourseName();</script>", false);

        UserDetails user = new UserDetailsDAO().GetUserByUserName(Session["UserName"].ToString(), cnxnString, logPath);

        if (user != null && user.IsNewUser)
        {
            Response.Redirect("~/ChangePassword.aspx", false);
        }

        List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(int.Parse(Request.QueryString[0]), cnxnString, logPath);
        List<ChapterSection> sections = new ChapterSectionDAO().GetAllSectionsByCourse(int.Parse(Request.QueryString[0]), cnxnString, logPath);
        if (chapters == null || sections == null)
        {
            Response.Redirect(Util.BASE_URL + "Dashboard.aspx", false);
            return;
        }

        hfChapters.Value = jss.Serialize(chapters);
        hfSections.Value = jss.Serialize(sections);
    }
}