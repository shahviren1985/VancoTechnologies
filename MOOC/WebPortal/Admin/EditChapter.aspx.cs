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
using System.Web.UI;
using System.Web.Script.Serialization;

public partial class Admin_EditChapter : PageBase
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath;
    string cnxnString;
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
            //configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
        }

        Edit.Visible = false;

        if (!IsPostBack)
        {
            bindGridView();
            PopulateDirectoryFiles();
        }
    }

    private void bindGridView()
    {
        try
        {
            ChapterDetailsDAO chapter = new ChapterDetailsDAO();
            gvChapterDetails.DataSource = chapter.GetChapterDetailsById(cnxnString, logPath);
            gvChapterDetails.DataBind();
            gvChapterDetails.Visible = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvChapterDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditDetails")
        {
            View.Visible = false;
            btnCancel.Visible = false;
            Edit.Visible = true;
            Success.Visible = false;
            getChapterDetail(Convert.ToInt32(e.CommandArgument));
            hfChapterId.Value = e.CommandArgument.ToString();
        }
    }

    private void PopulateCourse()
    {
        try
        {
            CourseDetailsDAO Course = new CourseDetailsDAO();
            ddlCourseId.DataSource = Course.GetAllCoursesDetails(cnxnString, logPath);
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

    private void getChapterDetail(int id)
    {
        ChapterDetails chapter;
        List<ChapterSection> sectionList = new List<ChapterSection>();
        string courseId = string.Empty;
        try
        {
            ChapterDetailsDAO objChapterDetailsDAO = new ChapterDetailsDAO();
            ChapterSectionDAO objChapterSectionDAO = new ChapterSectionDAO();
            chapter = objChapterDetailsDAO.GetChapterDetails(id, cnxnString, logPath);
            sectionList = objChapterSectionDAO.GetChapterSectionsByChapterId(id, cnxnString, logPath);
            //Temp.Text = sectionList.Id.ToString();
            PopulateCourse();
            ddlCourseId.SelectedValue = chapter.CourseId.ToString();
            ddlLanguage.SelectedValue = chapter.Language;
            txtChapterName.Text = chapter.Title;
            lblSelectedChapterFiles.Text = chapter.PageName;
            hfOldSelectedChapterFileName.Value = chapter.PageName;
            //foreach (ChapterSection item in sectionList)
            //{
            //   // test += item.Selected ? item.Value + ", " : "";
            //  lblSelectedChapterFiles.Text = item.SectionFileName;
            //}
            hfSectionJson.Value = "[]";
            hfSectionListCount.Value = "0";
            if (sectionList != null)
            {
                hfSectionJson.Value = jss.Serialize(sectionList);
                hfSectionListCount.Value = sectionList.Count.ToString();
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "PopulateSections('true');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        errorSummary.Visible = false;

        try
        {
            if (!IsValidForm(out errorMessage))
            {
                errorSummary.InnerHtml = errorMessage;
                errorSummary.Visible = true;
                return;
            }

            ChapterDetailsDAO objDAO = new ChapterDetailsDAO();
            ChapterSectionDAO sectionDAO = new ChapterSectionDAO();

            string contectVersion = objDAO.GetLatestContectVersion(cnxnString, logPath);

            logger.Debug("EditChapter.aspx.cs", "btnUpdate_Click", "before updating chapter details", logPath);

            // int chapterId = objDAO.AddChapters(int.Parse(ddlCourseId.SelectedValue), ddlLanguage.SelectedItem.Value, contectVersion, hfSelectedChapterFileName.Value, txtChapterName.Text, DateTime.Today, cnxnString, LogPath);
            objDAO.UpdateChapters(int.Parse(hfChapterId.Value), int.Parse(ddlCourseId.SelectedValue), ddlLanguage.SelectedItem.Value, contectVersion, hfSelectedChapterFileName.Value, txtChapterName.Text, DateTime.Today, cnxnString, logPath);

            logger.Debug("EditChapter.aspx.cs", "btnUpdate_Click", "after updating chapter details", logPath);

            List<ChapterSection> sections = jss.Deserialize(hfSectionJson.Value, typeof(List<ChapterSection>)) as List<ChapterSection>;
            List<ChapterSection> deletedSecId = null;
            deletedSecId = jss.Deserialize(hfDeletedSectionId.Value, typeof(List<ChapterSection>)) as List<ChapterSection>;

            if (sections != null)
            {
                foreach (ChapterSection section in sections)
                {
                    ChapterSection chapterSectionDetail = sectionDAO.GetSectionsByChapterIdAndSectionId(int.Parse(hfChapterId.Value), section.Id, cnxnString, logPath);
                    if (chapterSectionDetail != null)
                    {
                        logger.Debug("EditChapter.aspx.cs", "btnUpdate_Click", "before updating chapter section details: section-name= " + section.Title, logPath);
                        sectionDAO.UpdateChapterSection(section.Id, section.Title, section.PageName, cnxnString, logPath);
                        logger.Debug("EditChapter.aspx.cs", "btnUpdate_Click", "after updating chapter section details: section-name= " + section.Title, logPath);
                    }
                    else
                    {
                        logger.Debug("AddChapter.aspx.cs", "btnUpdate_Click", "before adding chapter section details: section-name= " + section.Title, logPath);
                        sectionDAO.AddChapterSection(int.Parse(hfChapterId.Value), section.Title, section.PageName, "", "show-db-section-contents.htm", 600, true, "", cnxnString, logPath);
                        logger.Debug("AddChapter.aspx.cs", "btnUpdate_Click", "after adding chapter section details: section-name= " + section.Title, logPath);
                    }
                }
            }

            foreach (ChapterSection secId in deletedSecId)
            {
                logger.Debug("EditChapter.aspx.cs", "btnUpdate_Click", "before deleting section details: section-name= " + secId.Title, logPath);
                sectionDAO.DeleteSectionRecord(secId.Id, cnxnString, logPath);
                logger.Debug("EditChapter.aspx.cs", "btnUpdate_Click", "after deleting section details: section-name= " + secId.Title, logPath);
            }

            ClearData();
            Success.Visible = true;
            View.Visible = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void gvChapterDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChapterDetails.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    #region Validation Function
    private bool IsValidForm(out string errorMessage)
    {
        bool isValid = true;
        errorMessage = "Success";

        if (string.IsNullOrEmpty(cnxnString))
        {
            errorMessage = "Unable to connect to database. Please re-login.";
            isValid = false;
        }
        else if (ddlCourseId.SelectedValue == "0")
        {
            errorMessage = "Please select valid course";
            isValid = false;
        }

        else if (ddlLanguage.SelectedValue == "0")
        {
            errorMessage = "Please select valid language";
            isValid = false;
        }
        else if (string.IsNullOrEmpty(txtChapterName.Text))
        {
            errorMessage = "Please enter chapter name";
            isValid = false;
        }
        else if (string.IsNullOrEmpty(txtSectionName.Text))
        {
            errorMessage = "Please enter section name";
            isValid = false;
        }
        //else if (string.IsNullOrEmpty(fuSectionFileName.FileName))
        //{
        //    errorMessage = "Please enter section file name";
        //    isValid = false;
        //}
        return isValid;
    }
    #endregion

    #region Clear Data Function
    public void ClearData()
    {
        try
        {
            if (!string.IsNullOrEmpty(cnxnString))
            {
                View.Visible = true;
                btnCancel.Visible = true;
                Edit.Visible = false;
                bindGridView();
            }

            errorSummary.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ClearData(object sender, EventArgs e)
    {
        ClearData();
    }
    #endregion

    #region Populate Directory Files Function
    private void PopulateDirectoryFiles()
    {
        try
        {
            DirectoryFiles dirFiles = new DirectoryFiles();
            rblFiles.DataSource = dirFiles.GetDirectoryFiles("html");
            rblFiles.DataTextField = "FileName";
            rblFiles.DataValueField = "FileName";
            rblFiles.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


}