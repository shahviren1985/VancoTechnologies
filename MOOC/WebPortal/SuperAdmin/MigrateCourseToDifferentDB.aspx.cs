using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.LogManager;
using ITM.Courses.DAO;

public partial class SuperAdmin_MigrateCourseToDifferentDB : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Success.Visible = false;
            errorSummary.Visible = false;

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
                PopulateSourceDestinationCnxnStringDropDown();
            }
        }
        catch (Exception ex)
        {
            logger.Error("MigrateCourseToDifferentDB", "PageLoad", "Error occured while loading page", ex, logPath);
            throw ex;
        }
    }

    private void PopulateSourceDestinationCnxnStringDropDown()
    {
        try
        {
            List<UserLogins> uniqueCnxnStrings = new UserLoginsDAO().GetUniqueCollageCnxnStrFromController(cnxnString, logPath);

            //ddlSourceCnxnString.DataSource = new UserLoginsDAO().GetUniqueCollageCnxnStrFromController(cnxnString, logPath);
            ddlSourceCnxnString.DataSource = uniqueCnxnStrings;
            ddlSourceCnxnString.DataTextField = "CollegeName";
            ddlSourceCnxnString.DataValueField = "CnxnString";
            ddlSourceCnxnString.DataBind();

            ddlSourceCnxnString.Items.Add(new ListItem("--Select Sourse College--", "0"));
            ddlSourceCnxnString.SelectedValue = "0";
            /*--------------------------------------------------------------*/
            ddlDestinationCnxnString.DataSource = uniqueCnxnStrings;
            ddlDestinationCnxnString.DataTextField = "CollegeName";
            ddlDestinationCnxnString.DataValueField = "CnxnString";
            ddlDestinationCnxnString.DataBind();

            ddlDestinationCnxnString.Items.Add(new ListItem("--Select Destination College--", "0"));
            ddlDestinationCnxnString.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateCourse()
    {
        try
        {
            if (ddlSourceCnxnString.SelectedValue != "0")
            {
                string sourceCnxnString = ddlSourceCnxnString.SelectedValue;

                CourseDetailsDAO Course = new CourseDetailsDAO();
                ddlCourse.DataSource = Course.GetAllCoursesDetails(sourceCnxnString, logPath);
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "Id";
                ddlCourse.DataBind();
                ddlCourse.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Course--", "0", true));
                ddlCourse.SelectedValue = "0";
            }
            else
            {
                errorSummary.InnerText = "Please select source college";
                errorSummary.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlSourceCnxnString_SelectedIndexChanged(object s, EventArgs e)
    {
        try
        {
            PopulateCourse();
        }
        catch (Exception ex)
        {
            logger.Error("MigrateCourseToDifferentDB", "ddlSourceCnxnString_SelectedIndexChanged", "Error occured while changing sourse-dropdown value", ex, logPath);
            throw ex;
        }
    }

    protected void btnMoveCourse_Click(object s, EventArgs e)
    {
        try
        {
            if (ddlSourceCnxnString.SelectedValue == "0" || ddlDestinationCnxnString.SelectedValue == "0" || ddlCourse.SelectedValue == "0" || string.IsNullOrEmpty(ddlCourse.SelectedValue))
            {
                errorSummary.InnerText = "All fields are mandatory.";
                errorSummary.Visible = true;
                return;
            }

            if (ddlSourceCnxnString.SelectedValue == ddlDestinationCnxnString.SelectedValue)
            {
                errorSummary.InnerText = "Source and destination college should be different.";
                errorSummary.Visible = true;
                return;
            }

            string sourseCnxnString = ddlSourceCnxnString.SelectedValue;
            string destinationCnxnString = ddlDestinationCnxnString.SelectedValue;

            CourseDetailsDAO courseDAO = new CourseDetailsDAO();
            ChapterDetailsDAO chapterDAO = new ChapterDetailsDAO();
            ChapterSectionDAO chapterSectionDAO = new ChapterSectionDAO();
            ChapterQuizMasterDAO chapterQuizDAO = new ChapterQuizMasterDAO();
            FinalQuizMasterDAO finalQuizMasterDAO = new FinalQuizMasterDAO();

            CourseDetail sourseCourse = courseDAO.GetCourseByCourseId(int.Parse(ddlCourse.SelectedValue), sourseCnxnString, logPath);

            if (sourseCourse != null)
            {
                #region Getting Course, Chapters, Sections, Quiz data from Sourse database
                List<ChapterDetails> sourseChapters = chapterDAO.GetAllChaptersByCourse(sourseCourse.Id, sourseCnxnString, logPath);
                List<ChapterSection> sourseSections = chapterSectionDAO.GetAllSectionsByCourse(sourseCourse.Id, sourseCnxnString, logPath);
                List<ChapterQuizMaster> sourseChapterQuiz = chapterQuizDAO.GetAllChapterQuizsByCourse(sourseCourse.Id, sourseCnxnString, logPath);
                List<FinalQuizMaster> sourseFinalQuiz = finalQuizMasterDAO.GetAllQuizsByCourse(sourseCourse.Id, sourseCnxnString, logPath);
                #endregion

                /* Checking course and dumping data into destination database */
                //CourseDetail destinationCourse = courseDAO.GetCourseByCourseName(sourseCourse.CourseName, destinationCnxnString, logPath);
                CourseDetail destinationCourse = courseDAO.GetMigCourseByMigratedCourseId(sourseCourse.Id, destinationCnxnString, logPath);

                if (destinationCourse == null)
                {
                    destinationCourse = courseDAO.GetMigCourseByMigratedCourseId(sourseCourse.Id, destinationCnxnString, logPath);
                }

                if (destinationCourse == null)      // adding course to sourse database
                {
                    int newCourseId = courseDAO.AddMigCourse(sourseCourse.CourseName, sourseCourse.DateCreated, 0, sourseCourse.OrignalCourseName, sourseCourse.Id, destinationCnxnString, logPath);

                    if (newCourseId != 0 && sourseChapters != null)
                    {
                        foreach (ChapterDetails objChp in sourseChapters)
                        {
                            // inserting chapter in destination database
                            int newChapterId = chapterDAO.AddMigChapters(newCourseId, objChp.Language, objChp.ContentVersion, objChp.PageName, objChp.Title,
                                objChp.DateCreated, objChp.Link, objChp.Time, objChp.IsDB, objChp.OrignalName, objChp.Id, destinationCnxnString, logPath);

                            if (newChapterId != 0 && sourseSections != null)
                            {
                                List<ChapterSection> sourseChapterSections = sourseSections.FindAll(sf => sf.ChapterId == objChp.Id).ToList();

                                if (sourseChapterSections != null)
                                {
                                    foreach (ChapterSection objSec in sourseChapterSections)
                                    {
                                        // inserting sections in destination database
                                        int newSectionId = chapterSectionDAO.AddMigChapterSection(newChapterId, objSec.Title, objSec.PageName, objSec.Description, objSec.Link,
                                            objSec.Time, objSec.IsDB, objSec.PageContent, objSec.OrignalName, objSec.Id, destinationCnxnString, logPath);

                                        if (newSectionId != 0 && sourseChapterQuiz != null)
                                        {
                                            // adding chaperquiz
                                            List<ChapterQuizMaster> sourseChpQuz = sourseChapterQuiz.FindAll(scq => scq.ChapterId == objChp.Id && scq.SectionId == objSec.Id).ToList();
                                            if (sourseChpQuz != null)
                                            {
                                                foreach (ChapterQuizMaster objChpQuz in sourseChpQuz)
                                                {
                                                    chapterQuizDAO.AddChaptersQuizMaster(newCourseId, newChapterId, newSectionId, objChpQuz.QuestionText, objChpQuz.IsQuestionOptionPresent, objChpQuz.QuestionOption,
                                                        objChpQuz.AnswerOption, objChpQuz.ContentVersion, objChpQuz.DateCreated, objChpQuz.CreatedBy, objChpQuz.Id, destinationCnxnString, logPath);

                                                }
                                            }
                                            //adding final quiz
                                            List<FinalQuizMaster> sourseFnlQuz = sourseFinalQuiz.FindAll(scq => scq.ChapterId == objChp.Id && scq.SectionId == objSec.Id).ToList();
                                            if (sourseFnlQuz != null)
                                            {
                                                foreach (FinalQuizMaster objChpQuz in sourseFnlQuz)
                                                {
                                                    finalQuizMasterDAO.AddFinalQuiz(newCourseId, newChapterId, newSectionId, objChpQuz.GroupNo, objChpQuz.Complexity, objChpQuz.QuestionText, objChpQuz.IsQuestionOptionPresent, objChpQuz.QuestionOption,
                                                        objChpQuz.AnswerOption, objChpQuz.ContentVersion, objChpQuz.CreatedBy, objChpQuz.Id, destinationCnxnString, logPath);

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Update course which is already present in destinatin database
                    #region Getting Course, Chapters, Sections, Quiz data from Sourse database
                    List<ChapterDetails> destChapters = chapterDAO.GetAllChaptersByCourse(destinationCourse.Id, destinationCnxnString, logPath);
                    List<ChapterSection> destSections = chapterSectionDAO.GetAllSectionsByCourse(destinationCourse.Id, destinationCnxnString, logPath);
                    List<ChapterQuizMaster> destChapterQuiz = chapterQuizDAO.GetAllChapterQuizsByCourse(destinationCourse.Id, destinationCnxnString, logPath);
                    List<FinalQuizMaster> destFinalQuiz = finalQuizMasterDAO.GetAllQuizsByCourse(destinationCourse.Id, destinationCnxnString, logPath);
                    #endregion

                    if (sourseChapters != null)
                    {
                        foreach (ChapterDetails objChp in sourseChapters)
                        {
                            ChapterDetails objDestChp = null;//destChapters.Find(dc => dc.MigratedChapterId == objChp.Id);

                            if (destChapters != null)
                                objDestChp = destChapters.Find(dc => dc.MigratedChapterId == objChp.Id);

                            int newChapterId;

                            if (objDestChp != null)
                            {
                                chapterDAO.Update_ChaptersTitle(objDestChp.Id, objChp.Title, destinationCnxnString, logPath);
                                newChapterId = objDestChp.Id;
                            }
                            else
                            {
                                newChapterId = chapterDAO.AddMigChapters(destinationCourse.Id, objChp.Language, objChp.ContentVersion, objChp.PageName, objChp.Title, objChp.DateCreated, objChp.Link, objChp.Time, objChp.IsDB, objChp.OrignalName, objChp.Id, destinationCnxnString, logPath);
                            }

                            if (newChapterId != 0 && sourseSections != null)
                            {
                                List<ChapterSection> sourseChapterSections = sourseSections.FindAll(sf => sf.ChapterId == objChp.Id).ToList();

                                if (sourseChapterSections != null)
                                {
                                    foreach (ChapterSection objSec in sourseChapterSections)
                                    {
                                        ChapterSection objDestSec = null;//destSections.Find(ds => ds.MigratedSectionId == objSec.Id);
                                        if (destSections != null)
                                            objDestSec = destSections.Find(ds => ds.MigratedSectionId == objSec.Id);

                                        int newSectionId;

                                        if (objDestSec != null)
                                        {
                                            newSectionId = objDestSec.Id;
                                            chapterSectionDAO.UpdateChapterSection(objDestSec.Id, objSec.Title, objSec.PageName, destinationCnxnString, logPath);
                                            chapterSectionDAO.UpdateSectionPageContent(objDestSec.Id, objSec.PageContent, objSec.Time, objSec.Description, destinationCnxnString, logPath);
                                        }
                                        else
                                        {
                                            newSectionId = chapterSectionDAO.AddMigChapterSection(newChapterId, objSec.Title, objSec.PageName, objSec.Description, objSec.Link, objSec.Time, objSec.IsDB, objSec.PageContent, objSec.OrignalName, objSec.Id, destinationCnxnString, logPath);
                                        }

                                        if (newSectionId != 0 && sourseChapterQuiz != null)
                                        {
                                            // adding chaperquiz
                                            List<ChapterQuizMaster> sourseChpQuz = sourseChapterQuiz.FindAll(scq => scq.ChapterId == objChp.Id && scq.SectionId == objSec.Id).ToList();
                                            if (sourseChpQuz != null)
                                            {
                                                foreach (ChapterQuizMaster objChpQuz in sourseChpQuz)
                                                {
                                                    ChapterQuizMaster objDestChpQuz = null;//destChapterQuiz.Find(dcq => dcq.MigratedChapterQuizId == objChpQuz.Id);
                                                    if (destChapterQuiz != null)
                                                        objDestChpQuz = destChapterQuiz.Find(dcq => dcq.MigratedChapterQuizId == objChpQuz.Id);

                                                    if (objDestChpQuz != null)
                                                    {
                                                        chapterQuizDAO.UpdateChapterQuizMaster(objDestChpQuz.Id, destinationCourse.Id, newChapterId, newSectionId, objChpQuz.QuestionText, objChpQuz.IsQuestionOptionPresent, objChpQuz.QuestionOption, objChpQuz.AnswerOption, objChpQuz.ContentVersion, objChpQuz.DateCreated, objChpQuz.CreatedBy, destinationCnxnString, cnxnString);
                                                    }
                                                    else
                                                    {
                                                        chapterQuizDAO.AddChaptersQuizMaster(destinationCourse.Id, newChapterId, newSectionId, objChpQuz.QuestionText, objChpQuz.IsQuestionOptionPresent, objChpQuz.QuestionOption, objChpQuz.AnswerOption, objChpQuz.ContentVersion, objChpQuz.DateCreated, objChpQuz.CreatedBy, objChpQuz.Id, destinationCnxnString, logPath);
                                                    }
                                                }
                                            }
                                        }

                                        if (newSectionId != 0 && sourseFinalQuiz != null)
                                        {
                                            List<FinalQuizMaster> sourseFnlQuz = sourseFinalQuiz.FindAll(scq => scq.ChapterId == objChp.Id && scq.SectionId == objSec.Id).ToList();
                                            if (sourseFnlQuz != null)
                                            {
                                                foreach (FinalQuizMaster objFnlQuz in sourseFnlQuz)
                                                {
                                                    FinalQuizMaster objDestFnlQuz = null;//destFinalQuiz.Find(sfq => sfq.MigratedFinalQuizId == objChpQuz.Id);

                                                    if (objDestFnlQuz != null)
                                                        objDestFnlQuz = destFinalQuiz.Find(sfq => sfq.MigratedFinalQuizId == objFnlQuz.Id);

                                                    if (objDestFnlQuz != null)
                                                    {
                                                        finalQuizMasterDAO.UpdateFinalQuiz(objDestFnlQuz.CourseId, newChapterId, newSectionId, objDestFnlQuz.GroupNo, objDestFnlQuz.Complexity, objFnlQuz.QuestionText, objFnlQuz.IsQuestionOptionPresent, objFnlQuz.QuestionOption, objFnlQuz.AnswerOption, objFnlQuz.ContentVersion, objFnlQuz.CreatedBy, objDestFnlQuz.Id, destinationCnxnString, logPath);
                                                    }
                                                    else
                                                    {
                                                        finalQuizMasterDAO.AddFinalQuiz(destinationCourse.Id, newChapterId, newSectionId, objFnlQuz.GroupNo, objFnlQuz.Complexity, objFnlQuz.QuestionText, objFnlQuz.IsQuestionOptionPresent, objFnlQuz.QuestionOption,
                                                            objFnlQuz.AnswerOption, objFnlQuz.ContentVersion, objFnlQuz.CreatedBy, objFnlQuz.Id, destinationCnxnString, logPath);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Success.InnerText = "Successfully migrated course.";
            Success.Visible = true;

        }
        catch (Exception ex)
        {
            logger.Error("MigrateCourseToDifferentDB", "btnMoveCourse_Click", "Error occured while moving course", ex, logPath);
            throw ex;
        }
    }
}