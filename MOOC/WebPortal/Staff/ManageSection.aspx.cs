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

public partial class Staff_ManageSection : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    const int FIRST_ELEMENT = 0;

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
            
        }
    }

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
        AddSection.Visible = true;
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

    protected void btnAddUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ChapterSectionDAO objDao = new ChapterSectionDAO();

            if (hfIsEdit.Value == "false")
            {
                objDao.AddChapterSection(int.Parse(ddlChapterId.SelectedValue), txtNewSectionName.Text, "show-db-section-contents.htm", "", "show-db-section-contents.htm", 600, true, "", cnxnString, logPath);
            }
            else if (hfIsEdit.Value == "true")
            {
                objDao.UpdateChapterSection(int.Parse(hfSelectedSectionId.Value), txtNewSectionName.Text.Trim(), "show-db-section-contents.htm", cnxnString, logPath);
            }

            bindGridView();
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
        ddlChapterId.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Chapter--","0",true));
        ddlChapterId.SelectedValue = "0";
    }

    #endregion

    protected void gvSectionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int sectionId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteSection")
            {
                new ChapterSectionDAO().DeleteSectionRecord(sectionId, cnxnString, logPath);
                Success.InnerText = "Section deleted successfully.";
                Success.Visible = true;

                bindGridView();
            }
        }
        catch (Exception ex)
        {
            if (ex.Source == "MySql.Data" || ex.Message.ToLower().Contains("foreign key"))
            {
                errorSummary.InnerText = "Error: there are some questions refer to this section, so cannot delete it.";
                errorSummary.Visible = true;
            }
            else
            {
                throw ex;
            }
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