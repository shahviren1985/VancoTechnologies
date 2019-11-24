using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.DAOBase;
using ITM.Courses.ConfigurationsManager;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using System.Configuration;
using System.Web.Script.Serialization;

public partial class Staff_ManageContent : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    const int FIRST_ELEMENT = 0;
    int sectionId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("../Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
        }

        if (!IsPostBack)
        {
            PopulateCourse();

            if (Request.QueryString.Count > 0)
            {
                sectionId = Convert.ToInt32(Request.QueryString["section"]);
                PopulateCourseChapters();
            }
        }

        //if (Request.QueryString.Count > 0)
        //{
        //    sectionId = Convert.ToInt32(Request.QueryString["section"]);
        //    PopulateCourseChapters();
        //}
    }

    private void PopulateCourseChapters()
    {
        try
        {
            ChapterDetails objChapter = new ChapterDetailsDAO().GetChapterBySecitonId(sectionId, cnxnString, logPath);

            if (objChapter != null)
            {
                ddlCourseId.SelectedValue = objChapter.CourseId.ToString();
                ddlCourseId_SelectedIndexChanged(new object(), new EventArgs());
                ddlChapterId.SelectedValue = objChapter.Id.ToString();
                ddlChapterId_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Populate Dropdowns
    private void PopulateCourse()
    {
        try
        {
            CourseDetailsDAO Course = new CourseDetailsDAO();
            ddlCourseId.DataSource = Course.GetAllCoursesByStaff(int.Parse(Session["UserId"].ToString()), cnxnString, logPath);
            ddlCourseId.DataTextField = "CourseName";
            ddlCourseId.DataValueField = "Id";
            ddlCourseId.DataBind();
            ddlCourseId.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Course--", "0", true));
            ddlCourseId.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void PopulateChapters()
    {
        ChapterDetailsDAO objChapterDetailsDAO = new ChapterDetailsDAO();
        List<ChapterDetails> chapterList = new List<ChapterDetails>();
        chapterList = objChapterDetailsDAO.GetAllChaptersByCourse(Int32.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);
        ddlChapterId.DataSource = chapterList;
        ddlChapterId.DataTextField = "Title";
        ddlChapterId.DataValueField = "Id";
        ddlChapterId.DataBind();
        ddlChapterId.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Chapter--", "0", true));
        ddlChapterId.SelectedValue = "0";
    }

    #endregion

    protected void gvSectionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSectionDetails.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    protected void ddlCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateChapters();
        // AddSection.Visible = true;
    }

    protected void ddlChapterId_SelectedIndexChanged(object sender, EventArgs e)
    {
        // PopulateChapters();
        //AddSection.Visible = true;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            ChapterSectionDAO objChapterSectionDAO = new ChapterSectionDAO();
            List<ChapterSection> sectionList = new List<ChapterSection>();
            sectionList = objChapterSectionDAO.GetChapterSectionsByChapterId(Int32.Parse(ddlChapterId.SelectedValue), cnxnString, logPath);
            gvSectionDetails.DataSource = sectionList;
            gvSectionDetails.DataBind();
            gvSectionDetails.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvSectionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int sectionId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditContent")
            {
                Response.Redirect(Util.BASE_URL + "Staff/ContentCreation.aspx?section=" + sectionId.ToString(), false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvSectionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Highlight(this)");
            e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");
        }
    }
}