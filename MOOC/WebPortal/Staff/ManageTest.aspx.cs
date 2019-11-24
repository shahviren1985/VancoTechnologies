using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public partial class Staff_ManageTest : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

    string mode = string.Empty;
    int courseId = 0;
    int testId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        selected_tab.Value = Request.Form[selected_tab.UniqueID];

        try
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

            #region Getting QueryString
            if (Request.QueryString.Count > 0)
            {
                //Staff/ManageTest.aspx?mode=edit&testid=1&courseid=2
                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && !string.IsNullOrEmpty(Request.QueryString["courseid"]))
                {
                    mode = Request.QueryString["mode"];
                    int.TryParse(Request.QueryString["courseid"], out courseId);
                    if (mode.ToLower() == "edit")
                    {
                        this.Title = "Manage class tests";
                        header.InnerText = "Manage class tests";
                        bread.InnerText = "Manage class tests";

                        if (!string.IsNullOrEmpty(Request.QueryString["testid"]))
                        {
                            int.TryParse(Request.QueryString["testid"], out testId);
                        }
                        else
                        { // TODO redirect to main page
                            Response.Redirect(Util.BASE_URL + "Staff/TestDetails.aspx", false);
                            return;
                        }
                    }
                    else
                    {
                        this.Title = "Create new class test";
                        header.InnerText = "Create new class test";
                        bread.InnerText = "Create new class test";
                    }
                }
                else
                {
                    // TODO redirect to main page
                    Response.Redirect(Util.BASE_URL + "Staff/TestDetails.aspx", false);
                    return;
                }
            }
            else
            {
                // TODO redirect to main page
                Response.Redirect(Util.BASE_URL + "Staff/TestDetails.aspx", false);
                return;
            }
            #endregion

            if (!IsPostBack)
            {
                PopulateCourse();
                ddlCourse.SelectedValue = courseId.ToString();
                ddlCourse_SelectedIndexChanged(sender, e);
                if (mode.ToLower() == "edit")
                {
                    PopulateTest();
                }
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
            ddlCourse.DataSource = Course.GetAllCoursesByStaff(int.Parse(Session["UserId"].ToString()), cnxnString, logPath);
            ddlCourse.DataTextField = "CourseName";
            ddlCourse.DataValueField = "Id";
            ddlCourse.DataBind();
            ddlCourse.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Course--", "0", true));
            ddlCourse.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    private void PopulateTest()
    {
        try
        {
            if (testId != 0)
            {
                CourseTests oCourseTest = new CourseTestsDAO().GetCourseTestsById(testId, cnxnString, logPath);
                if (oCourseTest != null)
                {
                    txtTestName.Text = oCourseTest.TestName;
                    txtNoOfQuestion.Text = oCourseTest.TotalQuestions.ToString();
                    txtTimeLimit.Text = oCourseTest.TimeLimit.ToString();
                    txtStartDate.Text = oCourseTest.StartDate.ToString();
                    txtEndDate.Text = oCourseTest.EndDate.ToString();

                    chkStatus.Checked = oCourseTest.IsTestActive;
                    chkTimeBound.Checked = oCourseTest.IsTimebound;

                    ddlCourse.SelectedValue = oCourseTest.CourseId.ToString();
                    ddlCourse_SelectedIndexChanged(new object(), new EventArgs());

                    string[] chapters = oCourseTest.Chapters.Split(new char[] { ',' });

                    foreach (ListItem items in lbChapters.Items)
                    {
                        if (oCourseTest.Chapters.Contains(items.Value))
                        {
                            items.Selected = true;
                        }
                    }

                    if (oCourseTest.IsTimebound)
                    {
                        timediv.Style["display"] = "block";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ChapterDetailsDAO oChapterDAO = new ChapterDetailsDAO();
            lbChapters.DataSource = oChapterDAO.GetAllChaptersByCourse(int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);
            lbChapters.DataTextField = "Title";
            lbChapters.DataValueField = "Id";
            lbChapters.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSave_Click(object s, EventArgs e)
    {
        try
        {
            if (Validate())
            {
                CourseTestsDAO oCourseTestDAO = new CourseTestsDAO();

                string chapters = string.Empty;
                foreach (ListItem item in lbChapters.Items)
                {
                    if (item.Selected)
                        chapters += item.Value + ",";
                }
                if (chapters.Length > 1)
                {
                    chapters = chapters.Remove(chapters.Length - 1, 1);
                }

                if (string.IsNullOrEmpty(txtTimeLimit.Text) || !chkTimeBound.Checked)
                    txtTimeLimit.Text = "0";

                DateTime dtStartDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime dtEndDate;

                if (txtEndDate.Text.Contains("11:59:59 PM"))
                {
                    dtEndDate = Convert.ToDateTime(txtEndDate.Text);
                }
                else
                {
                    dtEndDate = Convert.ToDateTime(txtEndDate.Text + " 23:59:59");
                }

                if (mode.ToLower() == "edit")
                {
                    oCourseTestDAO.UpdateCourseTests(testId, txtTestName.Text, int.Parse(ddlCourse.SelectedValue), chapters, chkTimeBound.Checked, int.Parse(txtTimeLimit.Text),
                                           int.Parse(txtNoOfQuestion.Text), chkStatus.Checked, dtStartDate, dtEndDate, cnxnString, logPath);
                    errorSummary.InnerHtml = "Successfully updated test.";
                }
                else
                {
                    oCourseTestDAO.AddCourseTests(txtTestName.Text, int.Parse(ddlCourse.SelectedValue), chapters, chkTimeBound.Checked, int.Parse(txtTimeLimit.Text),
                        int.Parse(txtNoOfQuestion.Text), chkStatus.Checked, dtStartDate, dtEndDate, cnxnString, logPath);
                    errorSummary.InnerHtml = "Successfully added new test.";
                }

                errorSummary.Visible = true;
                errorSummary.Style["background-color"] = "lightgreen";

                ClearData();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected bool Validate()
    {
        bool result = true;

        if (ddlCourse.SelectedValue == "0")
        {
            errorSummary.InnerHtml = "Please select course.";
            errorSummary.Visible = true;
            result = false;
        }

        if (string.IsNullOrEmpty(txtTestName.Text))
        {
            errorSummary.InnerHtml = "Please enter test name.";
            errorSummary.Visible = true;
            result = false;
        }

        bool chckLB = false;

        foreach (ListItem item in lbChapters.Items)
        {
            if (item.Selected)
                chckLB = true;
        }

        if (!chckLB)
        {
            errorSummary.InnerHtml = "Please select chapters.";
            errorSummary.Visible = true;
            result = false;
        }

        if (string.IsNullOrEmpty(txtNoOfQuestion.Text))
        {
            errorSummary.InnerHtml = "Please enter number of questions.";
            errorSummary.Visible = true;
            result = false;
        }
        else
        {
            int test;
            if (!int.TryParse(txtNoOfQuestion.Text, out test))
            {
                errorSummary.InnerHtml = "Please enter numeric value in number of question textbox.";
                errorSummary.Visible = true;
                result = false;
            }
        }

        if (string.IsNullOrEmpty(txtStartDate.Text))
        {
            errorSummary.InnerHtml = "Please enter test start date.";
            errorSummary.Visible = true;
            result = false;
        }

        if (string.IsNullOrEmpty(txtEndDate.Text))
        {
            errorSummary.InnerHtml = "Please enter test end date.";
            errorSummary.Visible = true;
            result = false;
        }

        if (chkTimeBound.Checked)
        {
            if (string.IsNullOrEmpty(txtTimeLimit.Text))
            {
                errorSummary.InnerHtml = "Please enter time limit of test.";
                errorSummary.Visible = true;
                result = false;
            }
            else
            {
                int test;
                if (!int.TryParse(txtTimeLimit.Text, out test))
                {
                    errorSummary.InnerHtml = "Please enter numeric value in time limit textbox.";
                    errorSummary.Visible = true;
                    result = false;
                }
            }
        }

        return result;
    }

    protected void ClearData()
    {
        PopulateCourse();
        txtEndDate.Text = string.Empty;
        txtNoOfQuestion.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtTestName.Text = string.Empty;
        txtTimeLimit.Text = string.Empty;
        lbChapters.Items.Clear();
        chkStatus.Checked = false;
        chkTimeBound.Checked = false;
    }
}