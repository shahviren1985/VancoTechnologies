using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Data;
using System.Web.Script.Serialization;

public partial class TestStatics : PageBase
{
    CourseTestsDAO oCourseTestDAO = new CourseTestsDAO();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string logPath;
    string cnxnString;
    string configPath;
    int courseId = 0;
    int testId = 0;

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
            }

            if (Request.QueryString.Count > 0)
            {
                courseId = Convert.ToInt32(Request.QueryString["courseid"]);

                if (!string.IsNullOrEmpty(Request.QueryString["testid"]))
                {
                    testId = Convert.ToInt32(Request.QueryString["testid"]);
                }
            }
            else
            {
                Response.Redirect(Util.BASE_URL + "Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                PopulateTests();

                if (courseId != 0 && testId != 0)
                {
                    CourseDetail course = new CourseDetailsDAO().GetCourseByCourseId(courseId, cnxnString, logPath);
                    lblCourseName.Text = course.CourseName;
                    ddlTest.SelectedValue = testId.ToString();
                    ddlTest_SelectedIndexChanged(sender, e);

                    hfCourseName.Value = course.CourseName;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateCourseName();</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateTests()
    {
        try
        {
            //ddlTest.DataSource = oCourseTestDAO.GetAllCourseTests(cnxnString, logPath);
            ddlTest.DataSource = oCourseTestDAO.GetCompletedorRunningTestsByUser(int.Parse(Session["UserId"].ToString()), courseId, true, cnxnString, logPath);
            ddlTest.DataTextField = "TestName";
            ddlTest.DataValueField = "Id";
            ddlTest.DataBind();

            ddlTest.Items.Add(new ListItem("--Select Class Test--", "0"));
            ddlTest.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTest_SelectedIndexChanged(object s, EventArgs e)
    {
        try
        {
            if (ddlTest.SelectedValue != "0")
            {
                CourseTests courseTest = new CourseTestsDAO().GetCourseTestsById(int.Parse(ddlTest.SelectedValue), cnxnString, logPath);
                CourseDetail courseDetails =new CourseDetailsDAO().GetCourseByCourseId(courseTest.CourseId, cnxnString,logPath);
                if(courseTest != null && courseDetails!= null){
                divBreadCrumb.InnerText = ddlTest.SelectedItem.Text;

                string courseBred = "<a href='CourseDetails.aspx?id=" + courseDetails.Id + "'>" + courseDetails.CourseName + "</a>";
                string testBred = courseTest.TestName;

                divBreadCrumb.InnerHtml = "<li><a href='Dashboard.aspx'>Home</a><span class='divider'>/</span></li><li>" + courseBred + "<span class='divider'>/</span></li><li>" + testBred + "</li>";


                this.Title = "Test report for " + ddlTest.SelectedItem.Text + "";

                
                header.InnerHtml = "Your Performance in " + courseTest.TestName;
                GetStudentTestQuestionWithAnswer(int.Parse(Session["UserId"].ToString()), courseTest.Chapters, cnxnString, logPath);
                }
            }
            else
            {
                divBreadCrumb.InnerText = "Test Report";
                this.Title = "Test Report";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetStudentTestQuestionWithAnswer(int userId, string chapterIds, string cnxnString, string logPath)
    {        
        FinalQuizMasterDAO objQuizDAO = new FinalQuizMasterDAO();
        FinalQuizResponseDAO objQuizResDAO = new FinalQuizResponseDAO();

        try
        {
            //List<FinalQuizMaster> finalQuizs = objQuizDAO.GetQuizbyTest(chapterIds, cnxnString, logPath);
            List<FinalQuizMaster> finalQuizs = objQuizDAO.GetQuizUsersResponseByTest(int.Parse(ddlTest.SelectedValue), userId, chapterIds, cnxnString, logPath);
            //List<FinalQuizResponse> finalQuizRes = objQuizResDAO.GetAllFinalQuizResponseByUserId(userId, cnxnString, logPath);
            
            //if (finalQuizs != null && finalQuizRes != null)
            if (finalQuizs != null)
            {
                int totalQuiz = 0;
                int correctAnswer = 0;
                int wrongAnswer = 0;

                foreach (FinalQuizMaster fqz in finalQuizs)
                {
                    totalQuiz++;
                    //FinalQuizResponse res = finalQuizRes.Find(r => r.QuestionId == fqz.Id && r.UserId == userId);                    
                    
                    string json = fqz.QuestionOption.Replace("(", "").Replace(")", "");
                    json = fqz.QuestionOption.Replace("\\", "");

                    fqz.QuestionOptionList = jss.Deserialize(json, typeof(List<QuestionOptions>)) as List<QuestionOptions>;

                    json = fqz.AnswerOption.Replace("(", "").Replace(")", "");
                    json = fqz.AnswerOption.Replace("\\", "");
                    fqz.AnswerOptionList = jss.Deserialize(json, typeof(List<AnswerOptions>)) as List<AnswerOptions>;

                    int count = fqz.AnswerOptionList.FindAll(ss => ss.IsCurrect == true).Count;

                    if (count > 1)
                        fqz.IsMultiTrueAnswer = true;
                    else
                        fqz.IsMultiTrueAnswer = false;

                    fqz.CorrectAnswer = "";

                    foreach (AnswerOptions ao in fqz.AnswerOptionList)
                    {
                        if (ao.IsCurrect)
                            fqz.CorrectAnswer += ao.AnswerOption + ",";
                    }

                    if (fqz.IsCorrect)
                        correctAnswer++;
                }

                hfTestQuizs.Value = jss.Serialize(finalQuizs);

                wrongAnswer = totalQuiz - correctAnswer;

                decimal completePer = Math.Round((decimal.Parse(correctAnswer.ToString()) / decimal.Parse(totalQuiz.ToString())) * 100, 2);
                decimal pendingPer = Math.Round((decimal.Parse(wrongAnswer.ToString()) / decimal.Parse(totalQuiz.ToString())) * 100, 2);

                List<CourseCompletionPercentChart> courseComlpetionChart = new List<CourseCompletionPercentChart>();

                courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = completePer, color = "#7FFF00" });
                courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = pendingPer, color = "#F7464A" });

                hfTestCompleteGraphData.Value = jss.Serialize(courseComlpetionChart);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateGraphTestQuiz();</script>", false);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        
    }

    public List<AnswerOptions> GetCorrectAnswer(string jAnswerOptons)
    {
        try
        {
            string json = jAnswerOptons.Replace("(", "").Replace(")", "");
            json = jAnswerOptons.Replace("\\", "");
            List<AnswerOptions> answerOptionList = jss.Deserialize(json, typeof(List<AnswerOptions>)) as List<AnswerOptions>;

            return answerOptionList;

            if (answerOptionList != null)
            {
                foreach (AnswerOptions ansOpt in answerOptionList)
                {
                    if (ansOpt.IsCurrect) { }
                        //return ansOpt.AnswerOption;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return null;
    }
}