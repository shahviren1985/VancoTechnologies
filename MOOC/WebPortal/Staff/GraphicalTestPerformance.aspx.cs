using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Data;
using System.Web.Script.Serialization;

public partial class Staff_GraphicalTestPerformance : PageBase
{
    CourseTestsDAO courseTestDAO = new CourseTestsDAO();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        errorSummary.Visible = false;
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
            if (ddlCourse.SelectedValue != null && ddlTests.SelectedValue != "0")
            {
                int totalStudents = 0, running = 0, completed = 0, notStarted = 0;

                List<UserDetails> users = new StudentCourseMapperDAO().GetStudentsByTest(int.Parse(ddlTests.SelectedValue), ddlCourse.SelectedValue, cnxnString, logPath);

                CourseTests courseTest = courseTestDAO.GetCourseTestsById(int.Parse(ddlTests.SelectedValue), cnxnString, logPath);

                FinalQuizMasterDAO objQuizDAO = new FinalQuizMasterDAO();
                FinalQuizResponseDAO objQuizResDAO = new FinalQuizResponseDAO();

                List<FinalQuizMaster> finalQuizs = objQuizDAO.GetQuizbyTest(courseTest.Chapters, cnxnString, logPath);

                if (users != null)
                {
                    totalStudents = users.Count;

                    foreach (UserDetails user in users)
                    {
                        List<FinalQuizResponse> finalQuizRes = objQuizResDAO.GetResponseByUserTestId(user.Id, int.Parse(ddlTests.SelectedValue), cnxnString, logPath);

                        if (finalQuizs != null)
                        {
                            totalStudents = users.Count;

                            if (finalQuizRes != null)
                            {
                                //if (finalQuizs.Count == finalQuizRes.Count)
                                if (courseTest.TotalQuestions == finalQuizRes.Count)
                                {
                                    completed++;
                                }
                                else
                                {
                                    running++;
                                }
                            }
                            else
                            {
                                notStarted++;
                            }
                        }
                    }
                }
                else
                {
                    errorSummary.InnerText = "Students do not registered to selected test.";
                    errorSummary.Visible = true;
                }

                if (totalStudents > 0)
                {
                    decimal completePer = Math.Round((decimal.Parse(completed.ToString()) / decimal.Parse(totalStudents.ToString())) * 100, 2);
                    decimal runnigPer = Math.Round((decimal.Parse(running.ToString()) / decimal.Parse(totalStudents.ToString())) * 100, 2);
                    decimal notStartedPer = Math.Round((decimal.Parse(notStarted.ToString()) / decimal.Parse(totalStudents.ToString())) * 100, 2);

                    List<CourseCompletionPercentChart> courseComlpetionChart = new List<CourseCompletionPercentChart>();

                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = completePer, color = "#7FFF00" }); // completed
                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = runnigPer, color = "#69D2E7" }); // appered
                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = notStartedPer, color = "#F7464A" }); // not started

                    hfChartData.Value = jss.Serialize(courseComlpetionChart);
                    hfSummary.Value = jss.Serialize(new { Total = totalStudents, Completed = completed, Running = running, NotStarted = notStarted });


                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateGraph();</script>", false);
                }
            }
            else
            {
                errorSummary.InnerText = "Please select course and test";
                errorSummary.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}