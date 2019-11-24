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

public partial class Staff_ManageQuestions : PageBase
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

    protected void ddlCourseId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourseId.SelectedValue != "0")
            {
                AddQuestions.Visible = true;
                errorSummary.Visible = false;
                bindGridView();
            }
            else
            {
                errorSummary.Visible = true;
                errorSummary.InnerText = "Plaese Select Course!";
                gvQuestionsDetails.Visible = false;
                AddQuestions.Visible = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void gvQuestionsDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvQuestionsDetails.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            ChapterQuizMasterDAO objChapterQuizMasterDAO = new ChapterQuizMasterDAO();
            List<ChapterQuizMaster> questionList = new List<ChapterQuizMaster>();
            questionList = objChapterQuizMasterDAO.GetQuestionsByCourse(Int32.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);

            gvQuestionsDetails.DataSource = questionList;
            gvQuestionsDetails.DataBind();
            gvQuestionsDetails.Visible = true;

            /*if (questionList != null)
            {
                gvQuestionsDetails.DataSource = questionList;
                gvQuestionsDetails.DataBind();
                gvQuestionsDetails.Visible = true;
            }
            else
            {
                //AddQuestions.Visible = false;
                //gvQuestionsDetails.Visible = false;
            }*/

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
            ddlCourseId.DataSource = Course.GetAllCoursesByStaff(int.Parse(Session["UserId"].ToString()),cnxnString, logPath);
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
    #endregion
    protected void btnAddMoreQuestion_Click(object sender, EventArgs e)
    {
        try
        {
            string courseid = ddlCourseId.SelectedValue;
            string url = "AddQuestion.aspx?courserid=" + courseid + "&mode=add" + "&qid=0";
            Response.Redirect(url);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    protected void gvQuestionsDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int quizId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditQuestions")
            {
                string courseId = ddlCourseId.SelectedValue;
                
                string url = "AddQuestion.aspx?courserid=" + courseId + "&mode=edit" + "&qid=" + quizId;
                Response.Redirect(url);
            }
            else if (e.CommandName == "DeleteQuestion")
            {
                new FinalQuizMasterDAO().DeleteFinalQuiz(quizId, cnxnString, logPath);
                Success.InnerText = "Question deleted successfully.";
                Success.Visible = true;
                
                bindGridView();
            }
        }            
        catch (Exception ex)
        {
            if (ex.Source == "MySql.Data" || ex.Message.ToLower().Contains("foreign key"))
            {
                errorSummary.InnerText = "Error: there are some student's responses refer to this question, so cannot delete it.";
                errorSummary.Visible = true;
            }
            else
            {
                throw ex;
            }
        }
    }

    protected void gvQuestionsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Highlight(this)");
            e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");
        }
    }
}