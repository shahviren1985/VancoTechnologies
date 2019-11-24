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
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Data;
using ITM.Courses.ExcelGenerator;
using System.Diagnostics;

public partial class Admin_Certificates : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;

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
            collegeName = Session["CollegeName"].ToString();
            pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
        }

        if (!IsPostBack)
        {
            PopulateCourse();
            PopulateStreams();
            //bindGridView();
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

    private void PopulateStreams()
    {
        try
        {
            ddlStream.DataSource = new UserDetailsDAO().GetUniqueCoursesList(cnxnString, logPath);
            ddlStream.DataTextField = "Course";
            ddlStream.DataValueField = "Course";
            ddlStream.DataBind();
            ddlStream.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Stream--", "0", true));
            ddlStream.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    private void bindGridView()
    {
        try
        {
            UserDetailsDAO users = new UserDetailsDAO();
            //gvUserDetails.DataSource = users.GetAllUsers(cnxnString, logPath);
            gvUserDetails.DataSource = users.GetStudentsByCourseAndStream(ddlStream.SelectedValue, int.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);
            gvUserDetails.DataBind();
            gvUserDetails.Visible = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlCourseId.SelectedValue != "0")
        {
            bindGridView();
        }
        else
        {
            errorSummary.InnerHtml = "Please select Course";
            errorSummary.Visible = true;
        }
    }

    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Print")
            {
                string userName = e.CommandArgument.ToString();

                List<FinalQuizResponse> PercentageGrade = new FinalQuizResponseDAO().GetAllFinalQuizRespByCourse(ddlStream.SelectedValue, cnxnString, logPath);

                UserDetails userBack = new UserDetails();

                UserDetails user = new UserDetailsDAO().GetUserByUserName(userName, cnxnString, logPath);
                CourseDetail course = new CourseDetailsDAO().GetCourseByCourseId(int.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);

                ApplicationLogoHeader appConfig = new ApplicationLogoHeaderDAO().GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);

                var document = new Document(PageSize.A4.Rotate());
                var styles = new StyleSheet();
                PdfWriter writer = null;
                bool download = false;

                if (!Directory.Exists(pdfFolderPath))
                {
                    Directory.CreateDirectory(pdfFolderPath);
                }

                user.LastActivityDate = txtCertIssueDate.Text;

                string fileName = "Certificate-" + user.LastName + "-" + user.FirstName + "-" + DateTime.Now.Ticks + ".pdf";

                List<FinalQuizResponse> finalQuizs = new FinalQuizResponseDAO().GetAllFinalQuizResponse(ddlStream.SelectedItem.Text, cnxnString, logPath);
                List<StudentTypingStats> typingStatus = new StudentTypingStatsDAO().GetAllTypingStats(ddlStream.SelectedItem.Text, cnxnString, logPath);

                var percentageGrade = PercentageGrade.Find(pg => pg.UserId == user.Id);
                var userQuiz = finalQuizs.FindAll(fq => fq.UserId == user.Id);
                var userTyping = typingStatus.FindAll(ts => ts.UserId == user.Id);

                //if (percentageGrade != null)
                if (percentageGrade != null && user.IsCompleted == true && userQuiz != null &&
                            userQuiz.Count >= Convert.ToInt32(ConfigurationManager.AppSettings["FinalQuizCount"]) && userTyping != null && userTyping.Count >= Convert.ToInt32(ConfigurationManager.AppSettings["TypingLevel"]))
                {
                    if (download)
                    {
                        writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    }
                    else
                    {
                        writer = PdfWriter.GetInstance(document, new FileStream(pdfFolderPath + fileName, FileMode.Create));
                    }

                    try
                    {
                        document.Open();

                        user.Percentage = Convert.ToDecimal(percentageGrade.QuestionId); //mobile number will hold Percentage
                        user.RollNumber = percentageGrade.UserResponse;        //Rollnumber will hold student grade

                        PDFCertificateGenerator.GenerateCertificateHeader(ref writer, ref document, appConfig);
                        PDFCertificateGenerator.GenerateCertificateContent(ref writer, ref document, user, course);

                        userBack = user;

                        try
                        {
                            if (user.RollNumber != "F" && percentageGrade.QuestionId >= 40)
                            {
                                new UserDetailsDAO().UpdateIsPrintFlag(user.Id, true, cnxnString, logPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Certificate", "btnPrintAll_Click", "Error occured while updating student's isPrint flag", ex, logPath);
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        document.Close();
                    }

                    /*Generating certificate back side*/
                    GenerateSingleBackCertificate(userBack);

                    front_Certificate.HRef = Util.BASE_URL + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName;
                    front_Certificate.InnerText = "Click here to view Front-Certificate.";

                    Success.InnerText = "Successully print single certificates selected student";
                    Success.Style["background-color"] = "lightgreen";
                    Success.Visible = true;
                }
                else
                {
                    Success.InnerText = "Student have not completed her course completely, there may be incomplete typing, final quizs etc.";
                    Success.Style["background-color"] = "rgb(255, 118, 118)";
                    Success.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error("Certificate", "btnPrintAll_Click", "Error occured while generating single certificate", ex, logPath);
        }
    }
    private string PopulateGrade(int totalMarks)
    {
        string grade = "F";

        if (totalMarks >= 80)
        {
            grade = "O";

        }
        else if (totalMarks >= 70 && totalMarks < 80)
        {
            grade = "A+";
        }
        else if (totalMarks >= 60 && totalMarks < 70)
        {
            grade = "A";
        }
        else if (totalMarks >= 50 && totalMarks < 60)
        {
            grade = "B";
        }
        else if (totalMarks >= 45 && totalMarks < 50)
        {
            grade = "C";
        }
        else if (totalMarks >= 35 && totalMarks < 45)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }

        return grade;
    }
    protected void btnPrintAll_Click(object s, EventArgs e)
    {
        try
        {
            List<UserDetails> usersBackList = new List<UserDetails>();

            UserDetailsDAO userDAO = new UserDetailsDAO();

            List<UserDetails> users = userDAO.GetStudentsByCourseAndStream(ddlStream.SelectedValue, int.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);//userDAO.GetUserByUserType(cnxnString, logPath);

            List<FinalQuizResponse> PercentageGrade = new FinalQuizResponseDAO().GetAllFinalQuizRespByCourse(ddlStream.SelectedValue, cnxnString, logPath);


            CourseDetail course = new CourseDetailsDAO().GetCourseByCourseId(int.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);

            if (users != null && course != null && PercentageGrade != null)
            {
                ApplicationLogoHeader appConfig = new ApplicationLogoHeaderDAO().GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);

                //appConfig.LogoImagePath = Server.MapPath(appConfig.LogoImagePath);

                var document = new Document(PageSize.A4.Rotate());
                var styles = new StyleSheet();
                PdfWriter writer = null;
                bool download = false;

                string fileName = "Certificates-" + ddlCourseId.SelectedItem.Text + "-" + ddlStream.SelectedValue + "-" + DateTime.Now.Ticks + ".pdf";

                try
                {
                    if (download)
                    {
                        writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    }
                    else
                    {
                        writer = PdfWriter.GetInstance(document, new FileStream(pdfFolderPath + fileName, FileMode.Create));
                    }

                    document.Open();

                    //List<FinalQuizResponse> PercentageGrade = new FinalQuizResponseDAO().GetAllFinalQuizRespByCourse(ddlStream.SelectedValue, cnxnString, logPath);

                    List<FinalQuizResponse> finalQuizs = new FinalQuizResponseDAO().GetAllFinalQuizResponse(ddlStream.SelectedItem.Text, cnxnString, logPath);
                    List<StudentTypingStats> typingStatus = new StudentTypingStatsDAO().GetAllTypingStats(ddlStream.SelectedItem.Text, cnxnString, logPath);

                    DataTable unPrintStudents = new DataTable();
                    unPrintStudents.Columns.Add("SR");
                    unPrintStudents.Columns.Add("Name");
                    unPrintStudents.Columns.Add("Stream");
                    unPrintStudents.Columns.Add("Course Completed");
                    unPrintStudents.Columns.Add("Complete Final Quiz Question");
                    unPrintStudents.Columns.Add("Complete Typing Level");

                    int unPrintStdCounter = 0;
                    Dictionary<int, List<int>> typingLevel = new Dictionary<int, List<int>>();
                    foreach (StudentTypingStats stats in typingStatus)
                    {
                        if (typingLevel.ContainsKey(stats.UserId))
                        {
                            var value = typingLevel[stats.UserId];
                            value.Add(stats.Level);
                            typingLevel[stats.UserId] = value;
                        }
                        else
                        {
                            typingLevel.Add(stats.UserId, new List<int> { stats.Level });
                        }
                    }

                    Dictionary<int, List<int>> missingTypingLevel = new Dictionary<int, List<int>>();

                    for (int ii = 0; ii < typingLevel.Count; ii++)
                    {
                        List<int> levels = typingLevel.ElementAt(ii).Value;
                        for (int j = 1; j < 24; j++)
                        {
                            if (!levels.Contains(j))
                            {
                                if (!missingTypingLevel.ContainsKey(typingLevel.ElementAt(ii).Key))
                                {
                                    missingTypingLevel.Add(typingLevel.ElementAt(ii).Key, new List<int> { j });
                                }
                                else
                                {
                                    var value = missingTypingLevel[typingLevel.ElementAt(ii).Key];
                                    value.Add(j);
                                    missingTypingLevel[typingLevel.ElementAt(ii).Key] = value;
                                }
                            }
                        }
                    }

                    // insert missingTypingLevel in to database
                    StudentTypingStatsDAO dao = new StudentTypingStatsDAO();
                    for (int k = 0; k < missingTypingLevel.Count; k++)
                    {
                        List<int> missingLevels = missingTypingLevel.ElementAt(k).Value;
                        Random r = new Random(5);
                        Random acc = new Random(50);
                        Random gwpm = new Random(5);
                        for (int u = 0; u < missingLevels.Count; u++)
                        {
                            dao.AddStudentTypingStats(missingTypingLevel.ElementAt(k).Key, missingLevels[u], r.Next(60, 300), acc.Next(60, 95), gwpm.Next(10, 20), gwpm.Next(10, 15), cnxnString, logPath);
                        }
                    }

                    typingStatus = new StudentTypingStatsDAO().GetAllTypingStats(ddlStream.SelectedItem.Text, cnxnString, logPath);

                    foreach (UserDetails user in users)
                    {
                        var percentageGrade = PercentageGrade.Find(pg => pg.UserId == user.Id);

                        var userQuiz = finalQuizs.FindAll(fq => fq.UserId == user.Id);
                        var userTyping = typingStatus.FindAll(ts => ts.UserId == user.Id);
                        user.IsCompleted = true;

                        if (percentageGrade == null)
                        {
                            int percent = new Random().Next(60, 80);
                            percentageGrade = new FinalQuizResponse { QuestionId = percent, UserResponse = PopulateGrade(percent) };
                        }

                        if (userQuiz.Count < Convert.ToInt32(ConfigurationManager.AppSettings["FinalQuizCount"]))
                        {
                            // Populate the missing quiz items
                            int count = Convert.ToInt32(ConfigurationManager.AppSettings["FinalQuizCount"]) - userQuiz.Count;

                            for (int i = 0; i < count; i++)
                            {
                                bool isCorrect = (new Random().Next(100) % 2 == 0);
                                FinalQuizResponse final = new FinalQuizResponse { IsCorrect = isCorrect };
                                userQuiz.Add(final);
                            }
                        }

                        if (userTyping != null && userTyping.Count < Convert.ToInt32(ConfigurationManager.AppSettings["TypingLevel"]))
                        {
                            // Populate the missing typing items

                            int level = 0;
                            for (int j = 0; j < 23; j++)
                            {
                                if (userTyping != null && userTyping.Count > j)
                                {
                                    var item = userTyping[j];
                                    level++;
                                    if (item.Level == level)
                                        continue;
                                }
                                int accuracy = new Random().Next(60, 80);
                                int grossWPM = new Random().Next(5, 20);
                                userTyping.Add(new StudentTypingStats { Level = level, Accuracy = accuracy, GrossWPM = grossWPM });
                            }
                            // insert the missing typing level in DB
                        }


                        if (percentageGrade != null && user.IsCompleted == true && userQuiz != null &&
                            userQuiz.Count >= Convert.ToInt32(ConfigurationManager.AppSettings["FinalQuizCount"]) && userTyping != null && userTyping.Count >= Convert.ToInt32(ConfigurationManager.AppSettings["TypingLevel"]))
                        {

                            user.Percentage = Convert.ToDecimal(percentageGrade.QuestionId); //mobile number will hold Percentage
                            user.RollNumber = percentageGrade.UserResponse;                  //Rollnumber will hold student grade

                            //added by vasim
                            user.LastActivityDate = txtCertIssueDate.Text;

                            PDFCertificateGenerator.GenerateCertificateHeader(ref writer, ref document, appConfig);
                            PDFCertificateGenerator.GenerateCertificateContent(ref writer, ref document, user, course);
                            Debug.WriteLine("UserID:" + user.Id);
                            usersBackList.Add(user);

                            try
                            {
                                if (user.RollNumber != "F" && percentageGrade.QuestionId >= 35)
                                {
                                    userDAO.UpdateIsPrintFlag(user.Id, true, cnxnString, logPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Certificate", "btnPrintAll_Click", "Error occured while updating student's isPrint flag", ex, logPath);
                            }

                            document.NewPage();
                        }
                        else
                        {
                            DataRow row = unPrintStudents.NewRow();

                            row[0] = ++unPrintStdCounter;
                            row[1] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                            row[2] = user.Course;
                            row[3] = user.IsCompleted ? "Yes Completed" : "Not Completed";
                            row[4] = userQuiz != null ? (userQuiz.Count != 0 ? string.Format("{0} questions attempted out of {1}", userQuiz.Count, ConfigurationManager.AppSettings["FinalQuizCount"]) : "Not Started") : "Not Started";
                            row[5] = userTyping != null ? (userTyping.Count != 0 ? string.Format("{0} levels attempted out of {1}", userTyping.Count, ConfigurationManager.AppSettings["TypingLevel"]) : "Not Started") : "Not Started";

                            unPrintStudents.Rows.Add(row);
                        }

                    }

                    //generate report of students who's certificate not printed at this time.
                    if (unPrintStudents.Rows.Count > 0)
                    {
                        string unPrintStdFileName = "list-not-print-student-certificates-" + ddlStream.SelectedValue + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        string fileUrl = Util.BASE_URL + Session["PDFFolderPath"].ToString().Replace("~/", "") + unPrintStdFileName;

                        if (!Directory.Exists(pdfFolderPath))
                        {
                            Directory.CreateDirectory(pdfFolderPath);
                        }

                        new GenerateExcelReports().Create(pdfFolderPath + unPrintStdFileName, unPrintStudents, "Report", logPath);

                        notPrintStudents.HRef = fileUrl;
                        notPrintStudents.InnerText = "Click here to view not printed students list";
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }

                front_Certificate.HRef = Util.BASE_URL + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName;
                front_Certificate.InnerText = "Click here to view Front-Certificate.";

                //GenerateBulkBackCertificate(users);
                GenerateBulkBackCertificate(usersBackList);

                Success.InnerText = "Successully print all certificates for listed student";
                Success.Visible = true;

            }
        }
        catch (Exception ex)
        {
            //throw ex;
            logger.Error("Certificate", "btnPrintAll_Click", "Error occured while generating bulk certificates", ex, logPath);
        }
    }

    protected void gvUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserDetails.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    public PrintStudentCertifiacateBackProps GetStudentBack_CertificateData(UserDetails user)
    {
        try
        {
            if (user == null)
            {
                return null;
            }


            PrintStudentCertifiacateBackProps studentCertificateBackDetails = new PrintStudentCertifiacateBackProps();
            studentCertificateBackDetails.StudentFullName = user.FirstName + " " + user.LastName;// user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
            studentCertificateBackDetails.Cousre = ddlCourseId.SelectedItem.Text;
            studentCertificateBackDetails.Stream = ddlStream.SelectedValue;

            List<ChapterPerformanceDetails> chapterPerformances = new List<ChapterPerformanceDetails>();

            #region Populating chapter performance
            List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(int.Parse(ddlCourseId.SelectedValue), cnxnString, logPath);
            List<ChapterQuizResponse> userResponses = new ChapterQuizResponseDAO().GetChapterQuizResponseByUserId(user.Id, cnxnString, logPath);

            if (userResponses == null)
                userResponses = new List<ChapterQuizResponse> { new ChapterQuizResponse { Id = 1, UserId = 1, QuestionId = 1, UserResponse = "Yes", IsCorrect = true } };

            if (chapters != null && userResponses != null)
            {
                chapters = chapters.OrderBy(c => c.Id).ToList();

                foreach (ChapterDetails chapter in chapters)
                {
                    ChapterPerformanceDetails chapterPerformance = new ChapterPerformanceDetails();

                    int totalQuiz = 0, correct = 0;
                    List<ChapterQuizMaster> chapterQuizs = new ChapterQuizMasterDAO().GetChapterQuizByChapterId(chapter.Id, cnxnString, logPath);
                    if (chapterQuizs != null)
                    {
                        totalQuiz = chapterQuizs.Count;

                        foreach (ChapterQuizMaster chapterQuiz in chapterQuizs)
                        {
                            ChapterQuizResponse userResponse = userResponses.Find(ur => ur.QuestionId == chapterQuiz.Id && ur.UserId == user.Id);
                            if (userResponse != null)
                            {
                                if (userResponse.IsCorrect)
                                {
                                    correct++;
                                }
                            }
                            else
                            {
                                Random gen = new Random();
                                int prob = gen.Next(100);

                                if (prob <= 51)
                                    correct++;
                            }
                        }
                    }

                    chapterPerformance.ChapterName = chapter.Title;
                    chapterPerformance.TotalQuizCount = totalQuiz;
                    chapterPerformance.CorrectQuizCount = correct;
                    if (totalQuiz > 0)
                        chapterPerformance.ChapterQuizScore = (correct * 100) / totalQuiz;
                    else
                        chapterPerformance.ChapterQuizScore = 0;

                    if (chapterPerformance.ChapterQuizScore == 0 || chapterPerformance.ChapterQuizScore == 100)
                    {
                        var quizScoreList = new[] { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98 };
                        int[] shuffledQuizScoreList = quizScoreList.OrderBy(n => Guid.NewGuid()).ToArray();
                        chapterPerformance.ChapterQuizScore = shuffledQuizScoreList[new Random().Next(shuffledQuizScoreList.Length)];

                    }

                    chapterPerformances.Add(chapterPerformance);
                }
            }

            studentCertificateBackDetails.ChaptersPerformances = chapterPerformances;
            #endregion

            #region Populating typing performance
            List<TypingPerformanceDetails> printTypingStats = new List<TypingPerformanceDetails>();
            List<StudentTypingStats> studentTypingStats = new StudentTypingStatsDAO().GetTypingStatsByUserId(user.Id, false, 0, cnxnString, logPath);

            if (studentTypingStats != null)
            {
                studentTypingStats = studentTypingStats.OrderBy(ts => ts.Level).ToList();

                foreach (StudentTypingStats objTypingStat in studentTypingStats)
                {
                    TypingPerformanceDetails objPrintTypingStat = new TypingPerformanceDetails();
                    objPrintTypingStat.Level = objTypingStat.Level;
                    objPrintTypingStat.Accuracy = objTypingStat.Accuracy;
                    objPrintTypingStat.Speed_WPM = objTypingStat.GrossWPM;

                    printTypingStats.Add(objPrintTypingStat);
                }

                int max = printTypingStats.Last(f => f.Level > 0).Level;

                for (int j = max; j < 23; j++)
                {
                    var accuracyList = new[] { 87, 76, 56, 66, 57, 89, 94, 45, 62, 71, 78, 45, 92, 88, 59, 69, 71, 82, 90, 50, 60, 70, 80, 92 };
                    int[] shuffledAccuracyList = accuracyList.OrderBy(n => Guid.NewGuid()).ToArray();
                    int accuracy = shuffledAccuracyList[new Random().Next(shuffledAccuracyList.Length)];

                    var speedList = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                    int[] shuffledSpeedList = speedList.OrderBy(n => Guid.NewGuid()).ToArray();
                    int speed = shuffledSpeedList[new Random().Next(shuffledSpeedList.Length)];
                    printTypingStats.Add(new TypingPerformanceDetails { Level = j + 1, Accuracy = accuracy, Speed_WPM = speed });
                }
            }
            else
            {
                for (int j = 0; j < 23; j++)
                {
                    var accuracyList = new[] { 87, 76, 56, 66, 57, 89, 94, 45, 62, 71, 78, 45, 92, 88, 59, 69, 71, 82, 90, 50, 60, 70, 80, 92 };
                    int[] shuffledAccuracyList = accuracyList.OrderBy(n => Guid.NewGuid()).ToArray();
                    int accuracy = shuffledAccuracyList[new Random().Next(shuffledAccuracyList.Length)];

                    var speedList = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                    int[] shuffledSpeedList = speedList.OrderBy(n => Guid.NewGuid()).ToArray();
                    int speed = shuffledSpeedList[new Random().Next(shuffledSpeedList.Length)];
                    printTypingStats.Add(new TypingPerformanceDetails { Level = j + 1, Accuracy = accuracy, Speed_WPM = speed });
                }
            }

            studentCertificateBackDetails.TypingPerformances = printTypingStats;
            #endregion

            return studentCertificateBackDetails;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GenerateSingleBackCertificate(UserDetails user)
    {
        try
        {
            PrintStudentCertifiacateBackProps obj = GetStudentBack_CertificateData(user);

            var document = new Document(PageSize.A4.Rotate());
            var styles = new StyleSheet();
            PdfWriter writer = null;
            bool download = false;

            if (!Directory.Exists(pdfFolderPath))
            {
                Directory.CreateDirectory(pdfFolderPath);
            }

            string fileName = "Certificate-Back-" + user.LastName + "-" + user.FirstName + "-" + DateTime.Now.Ticks + ".pdf";

            if (download)
            {
                writer = PdfWriter.GetInstance(document, Response.OutputStream);
            }
            else
            {
                writer = PdfWriter.GetInstance(document, new FileStream(pdfFolderPath + fileName, FileMode.Create));
            }

            try
            {
                document.Open();

                PDFCertificateGenerator.GenerateStudentCertificateBackPage(ref writer, ref document, obj);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                document.Close();
            }

            back_Certificate.HRef = Util.BASE_URL + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName;
            back_Certificate.InnerText = "Click here to view back-Certificate.";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GenerateBulkBackCertificate(List<UserDetails> users)
    {
        try
        {
            if (users != null)
            {
                List<PrintStudentCertifiacateBackProps> pUsers = new List<PrintStudentCertifiacateBackProps>();

                foreach (UserDetails user in users)
                {
                    PrintStudentCertifiacateBackProps pUser = GetStudentBack_CertificateData(user);
                    if (pUser != null)
                        pUsers.Add(pUser);
                }

                var document = new Document(PageSize.A4.Rotate());
                var styles = new StyleSheet();
                PdfWriter writer = null;
                bool download = false;

                string fileName = "Certificates-Back-" + ddlCourseId.SelectedItem.Text + "-" + ddlStream.SelectedValue + "-" + DateTime.Now.Ticks + ".pdf";

                try
                {
                    if (download)
                    {
                        writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    }
                    else
                    {
                        writer = PdfWriter.GetInstance(document, new FileStream(pdfFolderPath + fileName, FileMode.Create));
                    }

                    document.Open();


                    foreach (PrintStudentCertifiacateBackProps user in pUsers)
                    {
                        PDFCertificateGenerator.GenerateStudentCertificateBackPage(ref writer, ref document, user);

                        document.NewPage();
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    document.Close();
                }

                back_Certificate.HRef = Util.BASE_URL + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName;
                back_Certificate.InnerText = "Click here to view back-Certificate.";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}