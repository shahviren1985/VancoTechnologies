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

public partial class Admin_AddQuestions : PageBase
{
    Logger logger = new Logger();

    string logPath;
    string cnxnString;
    private string user = string.Empty;


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

        user = Session["UserName"].ToString();

        try
        {
            if (!IsPostBack)
            {
                PopulateCourse();
                PopulateChapter();
            }
        }
        catch (Exception ex)
        {
            throw;
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

    private void PopulateChapter()
    {
        try
        {
            ChapterDetailsDAO chapterDAO = new ChapterDetailsDAO();
            ddlChapterName.DataSource = chapterDAO.GetAllChapterDetails(cnxnString, logPath);
            ddlChapterName.DataTextField = "Title";
            ddlChapterName.DataValueField = "Id";
            ddlChapterName.DataBind();

            ddlChapterName.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Chapter--", "0", true));
            ddlChapterName.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateChapterSection()
    {
        try
        {
            ddlSectionId.Items.Clear();

            ChapterSectionDAO sectionDAO = new ChapterSectionDAO();
            ddlSectionId.DataSource = sectionDAO.GetChapterSectionsByChapterId(int.Parse(ddlChapterName.SelectedValue), cnxnString, logPath);
            ddlSectionId.DataTextField = "Title";
            ddlSectionId.DataValueField = "Id";
            ddlSectionId.DataBind();

            ddlSectionId.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Section--", "0", true));
            ddlSectionId.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void ddlChapter_Change(object s, EventArgs e)
    {
        try
        {
            PopulateChapterSection();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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

             ChapterQuizMasterDAO objChapterQuizMasterDAO = new ChapterQuizMasterDAO();
             ChapterDetailsDAO objChapterDetailsDAO = new ChapterDetailsDAO();
             string contectVersion = objChapterDetailsDAO.GetLatestContectVersion(cnxnString, logPath);

             string questionOptions = hfMoreOptionValue.Value;
             string answerOptions = hfMoreAnswerValue.Value;

             objChapterQuizMasterDAO.AddChaptersQuizMaster(int.Parse(ddlCourseId.SelectedValue), int.Parse(ddlChapterName.SelectedValue), int.Parse(ddlSectionId.SelectedValue),
                 txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, DateTime.Now, user, 0,cnxnString, logPath);

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
        else if (ddlChapterName.SelectedValue == "0")
        {
            errorMessage = "Please enter chapter name";
            isValid = false;
        }
        else if (ddlSectionId.SelectedValue == "0" || string.IsNullOrEmpty(ddlSectionId.SelectedValue))
        {
            errorMessage = "Please enter section id";
            isValid = false;
        }
        else if (txtQuestion.Text == "")
        {
            errorMessage = "Please enter question";
            isValid = false;
        }
        //else if (txtOptionValue.Text == "")
        //{
        //    errorMessage = "Please enter option value";
        //    isValid = false;
        //}
        else if (txtAnswerOption.Text == "")
        {
            errorMessage = "Please enter answer value";
            isValid = false;
        }
        else if (txtCorrectAns.Text == "")
        {
            errorMessage = "Please enter correct answer value";
            isValid = false;
        }

        return isValid;
    }
    #endregion
}