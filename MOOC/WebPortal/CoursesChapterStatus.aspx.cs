using ITM.Courses.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CoursesChapterStatus : System.Web.UI.Page
{
    string cnxnString;
    string logPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("~/Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
        }

        if (Request.QueryString.Count == 0)
        {
            return;
        }

        try
        {
            CourseDetail course = new CourseDetailsDAO().GetCourseByCourseId(int.Parse(Request.QueryString[0]), cnxnString, logPath);
            hfCourseName.Value = course.CourseName;

            if (!IsPostBack)
            {
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindGridView()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SR");
            dt.Columns.Add("Course");
            dt.Columns.Add("Chapter");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");
            dt.Columns.Add("Completed Date");
            dt.Columns.Add("Review Section");

            int courseId = int.Parse(Request.QueryString[0]);
            int userId = int.Parse(Session["UserId"].ToString());

            List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);

            if (chapters != null)
            {
                //get all sections by course
                List<ChapterSection> allChaptersSections = new ChapterSectionDAO().GetAllSectionsByCourse(courseId, cnxnString, logPath);
                if (allChaptersSections == null) allChaptersSections = new List<ChapterSection>();

                //get all chapter quiz by course
                List<ChapterQuizMaster> allChapterQuiz = new ChapterQuizMasterDAO().GetAllChapterQuizsByCourse(courseId, cnxnString, logPath);
                if (allChapterQuiz == null) allChapterQuiz = new List<ChapterQuizMaster>();

                //get all quiz responce by course
                List<ChapterQuizResponse> allQuizResponce = new ChapterQuizResponseDAO().GetAllChapterQuizResponseByUserCourse(courseId, userId, cnxnString, logPath);
                if (allQuizResponce == null) allQuizResponce = new List<ChapterQuizResponse>();

                //get all userchapter status by course
                List<UserChapterStatus> userChapters = new UserChapterStatusDAO().GetChapterStatusByCourseUserId(courseId, userId, cnxnString, logPath);
                if (userChapters == null) userChapters = new List<UserChapterStatus>();

                foreach (ChapterDetails chapter in chapters)
                {
                    List<ChapterSection> chapterSections = allChaptersSections.FindAll(s => s.ChapterId == chapter.Id);

                    if (chapterSections != null)
                    {
                        foreach (ChapterSection section in chapterSections)
                        {
                            DataRow row = dt.NewRow();
                            row["Course"] = hfCourseName.Value;
                            row["Chapter"] = chapter.Title;
                            row["Section"] = section.Title;

                            string completedDate;

                            if (new PrepareCourseCompletionPercentChart().IsUserCompleteChapterSection(chapter.Id, userId, section.Id, allChapterQuiz, allQuizResponce, userChapters, cnxnString, logPath, out completedDate))
                            {
                                row["Status"] = "Completed";                                
                            }
                            else
                            {
                                row["Status"] = "Pending";
                            }

                            row["Completed Date"] = completedDate;
                            row["Review Section"] = string.Format("/hintpage.aspx?courseid={0}&chapterId={1}&sectionId={2}", courseId, chapter.Id, section.Id);
                            dt.Rows.Add(row);
                        }
                    }
                }
            }

            gvUserDetails.DataSource = dt;
            gvUserDetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = ((Label)e.Row.FindControl("gvlblStatus")).Text;

                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "Pending")
                    {
                        e.Row.Style["background-color"] = "rgb(234, 18, 18)";
                        e.Row.Style["color"] = "white";                        
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}