using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Data;
using ITM.Courses.ExcelGenerator;
using System.IO;

public partial class Staff_StudentTestPerformanceReport : PageBase
{
    CourseTestsDAO courseTestDAO = new CourseTestsDAO();

    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
                collegeName = Session["CollegeName"].ToString();
                //pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
                pdfFolderPath = Session["PDFFolderPath"].ToString();
            }

            if (!IsPostBack)
            {
                PopulateTests();
                PopulateCourse();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void PopulateTests()
    {
        try
        {
            ddlTests.DataSource = courseTestDAO.GetAllTestsByStaffId(int.Parse(Session["UserId"].ToString()), cnxnString, logPath);
            ddlTests.DataTextField = "TestName";
            ddlTests.DataValueField = "Id";
            ddlTests.DataBind();

            ddlTests.Items.Add(new ListItem("--Select Test--", "0"));
            ddlTests.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void PopulateCourse()
    {
        try
        {
            ddlCourse.DataSource = new UserDetailsDAO().GetUniqueCoursesList(cnxnString, logPath);
            ddlCourse.DataTextField = "Course";
            ddlCourse.DataValueField = "Course";
            ddlCourse.DataBind();

            ddlCourse.Items.Add(new ListItem("--Select Course--", "0"));
            ddlCourse.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGenerate_Click(object s, EventArgs e)
    {
        try
        {
            if (ddlTests.SelectedValue != "0" && ddlCourse.SelectedValue != "0")
            {
                List<UserDetails> users = new StudentCourseMapperDAO().GetStudentsByTest(int.Parse(ddlTests.SelectedValue), ddlCourse.SelectedValue, cnxnString, logPath);

                if (users != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id");
                    dt.Columns.Add("Sr No");
                    dt.Columns.Add("Student");
                    dt.Columns.Add("UserName");
                    dt.Columns.Add("Course");

                    dt.Columns.Add("Total");
                    dt.Columns.Add("Attempt");
                    dt.Columns.Add("Correct");
                    dt.Columns.Add("Incorrect");
                    int counter = 1;
                    foreach (UserDetails user in users)
                    {
                        DataRow row = dt.NewRow();

                        row["Id"] = user.Id;
                        row["Sr No"] = counter; counter++;
                        row["Student"] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                        row["UserName"] = user.UserName;
                        row["Course"] = user.Course;
                        int total, attempt, inncorrect, correct;
                        correct = GetTestCorrectAnswerCount(user.Id, out total, out attempt, out inncorrect);
                        row["Total"] = total;
                        row["Attempt"] = attempt;
                        row["Correct"] = correct;
                        row["Incorrect"] = inncorrect;

                        dt.Rows.Add(row);
                    }

                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
                    ViewState["Users"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        divPrintButton.Style["display"] = "block";
                    }
                }
                else
                {
                    errorSummary.InnerText = "Students do not registered to selected test.";
                    errorSummary.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetTestCorrectAnswerCount(int userId, out int total, out int attempt, out int inncorrect)
    {
        total = 0; attempt = 0; inncorrect = 0;
        int correct = 0;
        try
        {
            CourseTests courseTest = courseTestDAO.GetCourseTestsById(int.Parse(ddlTests.SelectedValue), cnxnString, logPath);

            FinalQuizMasterDAO objQuizDAO = new FinalQuizMasterDAO();
            FinalQuizResponseDAO objQuizResDAO = new FinalQuizResponseDAO();

            List<FinalQuizMaster> finalQuizs = objQuizDAO.GetQuizbyTest(courseTest.Chapters, cnxnString, logPath);
            List<FinalQuizResponse> finalQuizRes = objQuizResDAO.GetResponseByUserTestId(userId, int.Parse(ddlTests.SelectedValue), cnxnString, logPath);

            if (finalQuizs != null)
            {
                //total = finalQuizs.Count;
                total = courseTest.TotalQuestions;

                if (finalQuizRes != null)
                {
                    attempt = finalQuizRes.Count;

                    foreach (FinalQuizResponse quizRes in finalQuizRes)
                    {
                        if (quizRes.IsCorrect)
                            correct++;
                        else
                            inncorrect++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return correct;
    }

    protected void btnAsPDF_Click(object s, EventArgs e)
    {

    }

    protected void btnAsExcel_Click(object s, EventArgs e)
    {
        try
        {
            if (ViewState["Users"] != null)
            {
                DataTable dtUser = ViewState["Users"] as DataTable;

                string fileName = "Student-test-progress-report-" + ddlCourse.SelectedValue + "-" + ddlTests.SelectedItem.Text + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                string fileUrl = pdfFolderPath + fileName;

                pdfFolderPath = Server.MapPath(pdfFolderPath);

                if (!Directory.Exists(pdfFolderPath))
                {
                    Directory.CreateDirectory(pdfFolderPath);
                }

                dtUser.Columns.RemoveAt(0);

                new GenerateExcelReports().Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                Response.Redirect(fileUrl, false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "Highlight(this)");
            e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");
        }
    }
}