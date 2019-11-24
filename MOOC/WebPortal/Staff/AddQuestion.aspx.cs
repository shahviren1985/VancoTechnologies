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

public partial class Staff_AddQuestion : PageBase
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath;
    string cnxnString;
    private string user = string.Empty;
    string mode;
    string courseId;
    string queId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect(Util.BASE_URL + "Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            //configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
            mode = Request.QueryString["mode"];
            courseId = Request.QueryString["courserid"];
            queId = Request.QueryString["qid"];
        }

        user = Session["UserName"].ToString();

        try
        {
            if (!IsPostBack)
            {
                if (mode == "add")
                {
                    //PopulateCourse();
                    PopulateChapter();
                }
                else if (mode == "edit")
                {
                    PopulateChapter();
                    PopulateChapterAndQctionAndQuestion();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Populate Dropdowns
    //private void PopulateCourse()
    //{
    //    //    try
    //    //    {
    //    //        if (mode == "edit" || mode == "add" && Int32.Parse(courseId) != 0)
    //    //        {
    //    //            CourseDetailsDAO Course = new CourseDetailsDAO();
    //    //            ddlCourseId.DataSource = Course.GetAllCoursesDetails(cnxnString, logPath);
    //    //            ddlCourseId.DataTextField = "CourseName";
    //    //            ddlCourseId.DataValueField = "Id";
    //    //            ddlCourseId.DataBind();
    //    //            ddlCourseId.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Course--", "0", true));
    //    //            ddlCourseId.SelectedValue = "0";
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }
    //}

    private void PopulateChapterAndQctionAndQuestion()
    {
        ChapterQuizMasterDAO objChapterQuizMasterDAO = new ChapterQuizMasterDAO();
        ChapterQuizMaster questionDetails = new ChapterQuizMaster();
        questionDetails = objChapterQuizMasterDAO.GetQuestionByQuestionId(Int32.Parse(queId), cnxnString, logPath);

        if (questionDetails != null)
        {
            hfOldQuestionText.Value = questionDetails.QuestionText;

            ddlChapterName.SelectedValue = questionDetails.ChapterId.ToString();
            ddlChapter_Change(new object(), new EventArgs());
            ddlChapterName.Enabled = false;
            ddlSectionId.SelectedValue = questionDetails.SectionId.ToString();
            ddlSectionId.Enabled = false;
            txtQuestion.Text = questionDetails.QuestionText;

            string json = questionDetails.QuestionOption.Replace("(", "").Replace(")", "");
            json = questionDetails.QuestionOption.Replace("\\", "");

            questionDetails.QuestionOptionList = jss.Deserialize(json, typeof(List<QuestionOptions>)) as List<QuestionOptions>;

            json = questionDetails.AnswerOption.Replace("(", "").Replace(")", "");
            json = questionDetails.AnswerOption.Replace("\\", "");
            questionDetails.AnswerOptionList = jss.Deserialize(json, typeof(List<AnswerOptions>)) as List<AnswerOptions>;



            //hfSectionJson.Value = "[]";
            hfQuestionOptionCount.Value = "[]";
            hfAnswerOptionCount.Value = "[]";
            if (questionDetails != null)
            {
                hfSectionJson.Value = jss.Serialize(questionDetails);
                hfQuestionOptionCount.Value = jss.Serialize(questionDetails.QuestionOptionList);
                hfAnswerOptionCount.Value = jss.Serialize(questionDetails.AnswerOptionList);
            }
            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "PopulateSections('true');", true);
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "PopulateSections();", true);
        }
    }

    private void PopulateChapter()
    {
        try
        {
            if (!string.IsNullOrEmpty(courseId) && courseId != "0")
            {
                ChapterDetailsDAO chapterDAO = new ChapterDetailsDAO();
                ddlChapterName.DataSource = chapterDAO.GetAllChaptersByCourse(Int32.Parse(courseId), cnxnString, logPath);
                ddlChapterName.DataTextField = "Title";
                ddlChapterName.DataValueField = "Id";
                ddlChapterName.DataBind();
                ddlChapterName.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Chapter--", "0", true));
                ddlChapterName.SelectedValue = "0";
            }
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
            if (!string.IsNullOrEmpty(ddlChapterName.SelectedValue))
            {
                ChapterSectionDAO sectionDAO = new ChapterSectionDAO();
                ddlSectionId.DataSource = sectionDAO.GetChapterSectionsByChapterId(int.Parse(ddlChapterName.SelectedValue), cnxnString, logPath);
                ddlSectionId.DataTextField = "Title";
                ddlSectionId.DataValueField = "Id";
                ddlSectionId.DataBind();

                ddlSectionId.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Section--", "0", true));
                ddlSectionId.SelectedValue = "0";
            }
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

            int val = (rbYes.Checked) ? 1 : 2;

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>ShowHide('" + val + "');</script>", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ChapterQuizMasterDAO objChapterQuizDAO = new ChapterQuizMasterDAO();

        if (mode == "add")
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

                FinalQuizMasterDAO objFinalQuizMasterDAO = new FinalQuizMasterDAO();
                ChapterDetailsDAO objChapterDetailsDAO = new ChapterDetailsDAO();
                string contectVersion = objChapterDetailsDAO.GetLatestContectVersion(cnxnString, logPath);


                string questionOptions = hfMoreOptionValue.Value;
                string answerOptions = hfMoreAnswerValue.Value;

                //objFinalQuizMasterDAO.AddFinalQuiz(int.Parse(courseId), int.Parse(ddlChapterName.SelectedValue), int.Parse(ddlSectionId.SelectedValue),
                //    txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, DateTime.Now, user, cnxnString, logPath);
                /* Adding questions to final quiz */
                objFinalQuizMasterDAO.AddFinalQuiz(Int32.Parse(courseId), Int32.Parse(ddlChapterName.SelectedValue), Int32.Parse(ddlSectionId.SelectedValue), 0, 0, txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, user, 0, cnxnString, logPath);
                Success.Visible = true;
                /* Adding questions to Chapter quiz table */
                objChapterQuizDAO.AddChaptersQuizMaster(Int32.Parse(courseId), Int32.Parse(ddlChapterName.SelectedValue), Int32.Parse(ddlSectionId.SelectedValue),
                    txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, DateTime.Now, user, 0, cnxnString, logPath);

                ClearControls();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        else if (mode == "edit")
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

                FinalQuizMasterDAO objFinalQuizMasterDAO = new FinalQuizMasterDAO();
                ChapterDetailsDAO objChapterDetailsDAO = new ChapterDetailsDAO();
                string contectVersion = objChapterDetailsDAO.GetLatestContectVersion(cnxnString, logPath);

                // string questionOptions = jss.Deserialize(hfMoreOption.Value, typeof(string)) as string;
                // string answerOptions = jss.Deserialize(hfMoreAnswer.Value, typeof(string)) as string;
                string questionOptions = hfMoreOptionValue.Value;
                string answerOptions = hfMoreAnswerValue.Value;

                //objFinalQuizMasterDAO.AddFinalQuiz(int.Parse(courseId), int.Parse(ddlChapterName.SelectedValue), int.Parse(ddlSectionId.SelectedValue),
                //    txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, DateTime.Now, user, cnxnString, logPath);
                /* updating questions to final quiz */
                objFinalQuizMasterDAO.UpdateFinalQuiz(Int32.Parse(courseId), Int32.Parse(ddlChapterName.SelectedValue), Int32.Parse(ddlSectionId.SelectedValue), 0, 0, txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, user, Int32.Parse(queId), cnxnString, logPath);

                ChapterQuizMaster chpQm = objChapterQuizDAO.GetChapterQuestionByQuestionText(hfOldQuestionText.Value, Int32.Parse(ddlChapterName.SelectedValue), Int32.Parse(ddlSectionId.SelectedValue), cnxnString, logPath);

                if (chpQm != null)
                {
                    objChapterQuizDAO.UpdateChapterQuizMaster(chpQm.Id, Int32.Parse(courseId), Int32.Parse(ddlChapterName.SelectedValue), Int32.Parse(ddlSectionId.SelectedValue), txtQuestion.Text, rbYes.Checked, questionOptions, answerOptions, contectVersion, DateTime.Now, user, cnxnString, logPath);
                }

                Success.InnerText = "Question updated successfully.";
                mainContentDiv.Visible = false;
                Success.Visible = true;
                ClearControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public void ClearControls()
    {
        txtAnswerOption.Text = string.Empty;
        //txtCorrectAns.Text = string.Empty;
        txtOptionValue.Text = string.Empty;
        txtQuestion.Text = string.Empty;

        ddlChapterName.SelectedValue = "0";
        ddlSectionId.SelectedValue = "0";
        brNo.Checked = true;
        rbYes.Checked = false;
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
        //else if (ddlCourseId.SelectedValue == "0")
        //{
        //    errorMessage = "Please select valid course";
        //    isValid = false;
        //}
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
        else if (txtAnswerOption.Text == "")
        {
            errorMessage = "Please enter answer value";
            isValid = false;
        }
        /*else if (txtCorrectAns.Text == "")
        {
            errorMessage = "Please enter correct answer value";
            isValid = false;
        }*/

        return isValid;
    }
    #endregion


}