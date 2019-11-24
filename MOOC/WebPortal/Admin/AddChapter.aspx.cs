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

public partial class Admin_AddChapter : PageBase//Page
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

    const int FIRST_ELEMENT = 0;
    string TempChapterFileName = "ABC";
    // string TempContentVersion = "1.1";

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
            PopulateDirectoryFiles();
            
        }
    }

    #region Populate Dropdowns
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

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

            logger.Debug("AddChapter.aspx.cs", "btnsubmit_Click", "before adding chapter details", logPath);
            int chapterId = objDAO.AddChapters(int.Parse(ddlCourseId.SelectedValue), ddlLanguage.SelectedItem.Value, contectVersion, hfSelectedChapterFileName.Value, txtChapterName.Text, DateTime.Today,
                "show-db-section-contents.htm", 600, true, cnxnString, logPath);
            logger.Debug("AddChapter.aspx.cs", "btnsubmit_Click", "after adding chapter details", logPath);

            List<SectionProp> sections = jss.Deserialize(hfSectionJson.Value, typeof(List<SectionProp>)) as List<SectionProp>;
            
            if (sections != null)
            {
                foreach (SectionProp section in sections)
                {
                    logger.Debug("AddChapter.aspx.cs", "btnsubmit_Click", "before adding chapter section details: section-name= " + section.SectionName, logPath);
                    sectionDAO.AddChapterSection(chapterId, section.SectionName, section.SectionFileName, "", "show-db-section-contents.htm", 600, true, "", cnxnString, logPath);
                    logger.Debug("AddChapter.aspx.cs", "btnsubmit_Click", "after adding chapter section details: section-name= " + section.SectionName, logPath);
                }
            }

            ClearData();
            Success.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
            ddlCourseId.SelectedIndex = FIRST_ELEMENT;
            ddlLanguage.SelectedIndex = FIRST_ELEMENT;
            txtChapterName.Text = string.Empty;
            txtSectionName.Text = string.Empty;

            if (!string.IsNullOrEmpty(cnxnString))
            {
                PopulateCourse();
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
}