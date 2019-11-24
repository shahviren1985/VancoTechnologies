<%@ WebHandler Language="C#" Class="AutoSendOverAllReport" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using ITM.Courses.ExcelGenerator;
using ITM.Courses.ConfigurationsManager;

public class AutoSendOverAllReport : IHttpHandler
{

    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;
    string pdfFolderPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        context.Response.ContentType = "text/plain";

        string userName = context.Request.QueryString["username"];

        if (string.IsNullOrEmpty(userName))
        {
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Please pass user name!" }));
            return;
        }
        else
        {
            UserLogins userLogin = new UserLoginsDAO().GetUserByUserName(userName, "");

            if (userLogin != null)
            {
                Dictionary<string, string> dExcelFilesPath = new Dictionary<string, string>();

                cnxnString = userLogin.CnxnString;
                logPath = context.Server.MapPath(userLogin.LogFilePath);
                configFilePath = context.Server.MapPath(userLogin.ReleaseFilePath);
                pdfFolderPath = context.Server.MapPath(userLogin.PDFFolderPath);

                GenerateExcelReports reportGenerator = new GenerateExcelReports();

                UserDetailsDAO courseDAO = new UserDetailsDAO();
                List<UserDetails> courses = courseDAO.GetUniqueCoursesList(cnxnString, logPath);

                List<UserDetails> students = courseDAO.GetAllUsers(false, cnxnString, logPath);

                if (students == null) students = new List<UserDetails>();

                students = students.FindAll(s => s.IsActive);

                if (courses != null)
                {
                    //remove course
                    var list = courses.FindAll(cm => cm.Course == "0" || cm.Course == "Unknown Course");

                    if (list != null) foreach (var l in list) courses.Remove(l);

                    int courseId = 0;
                    int.TryParse(context.Request.QueryString["courseid"], out courseId);

                    if (courseId == 0)
                    {
                        context.Response.Write("Error: Please supply course id;");
                        return;
                    }

                    List<StudentCourseMapper> courseMapper = new StudentCourseMapperDAO().GetStudentCourseMapperByCourseId(courseId, cnxnString, logPath);
                    if (courseMapper == null) courseMapper = new List<StudentCourseMapper>();

                    List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);

                    if (chapters == null) chapters = new List<ChapterDetails>();

                    //get all sections by course
                    List<ChapterSection> chaptersSections = new ChapterSectionDAO().GetAllSectionsByCourse(courseId, cnxnString, logPath);
                    if (chaptersSections == null) chaptersSections = new List<ChapterSection>();

                    //get all chapter quiz by course
                    List<ChapterQuizMaster> chapterQuiz = new ChapterQuizMasterDAO().GetAllChapterQuizsByCourse(courseId, cnxnString, logPath);
                    if (chapterQuiz == null) chapterQuiz = new List<ChapterQuizMaster>();

                    //get all quiz responce by course                    
                    List<ChapterQuizResponse> quizResponce = new ChapterQuizResponseDAO().GetAllChapterQuizResponse(cnxnString, logPath);
                    if (quizResponce == null) quizResponce = new List<ChapterQuizResponse>();

                    //get all userchapter status by course                    
                    List<UserChapterStatus> userChapters = new UserChapterStatusDAO().GetAllUsersChapterStatus(cnxnString, logPath);
                    if (userChapters == null) userChapters = new List<UserChapterStatus>();

                    //get finalquiz responce
                    List<FinalQuizResponse> finalQuizResponse = new FinalQuizResponseDAO().GetAllFinalQuizResponse(cnxnString, logPath);
                    if (finalQuizResponse == null) finalQuizResponse = new List<FinalQuizResponse>();

                    //get typing status
                    List<StudentTypingStats> typingStats = new StudentTypingStatsDAO().GetAllTypingStats("",cnxnString, logPath);
                    if (typingStats == null) typingStats = new List<StudentTypingStats>();

                    int finalQuizCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FinalQuizCount"]);
                    int typingLevelCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TypingLevel"]);

                    foreach (UserDetails course in courses)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Course");
                        dt.Columns.Add("Roll Number");
                        dt.Columns.Add("First Name");
                        dt.Columns.Add("Last Name");                        
                        dt.Columns.Add("Email Address");
                        dt.Columns.Add("Course Started");
                        dt.Columns.Add("Chapter Pending");
                        dt.Columns.Add("Final Exam Given");
                        dt.Columns.Add("Question Attempted");
                        dt.Columns.Add("Typing Level Pending");

                        List<UserDetails> courseWiseStudents = students.FindAll(cws => cws.Course == course.Course);

                        foreach (UserDetails std in courseWiseStudents)
                        {
                            DataRow row = dt.NewRow();
                            row[0] = std.Course;
                            row[1] = std.RollNumber;
                            row[2] = std.FirstName;
                            row[3] = std.LastName;
                            
                            row[4] = std.EmailAddress;

                            string pendingChapters = string.Empty;
                            int completeChapterCount = 0, pendingChapterCount = 0;

                            foreach (ChapterDetails chapter in chapters)
                            {
                                if (!new PrepareCourseCompletionPercentChart().IsUserCompleteChapter(chapter.Id, std.Id, chaptersSections, chapterQuiz, quizResponce, userChapters, cnxnString, logPath))
                                {
                                    pendingChapters += chapter.Id + ",";
                                    pendingChapterCount++;
                                }
                                else
                                {
                                    completeChapterCount++;
                                }
                            }

                            row[5] = (completeChapterCount == 0) ? "No" : "Yes";
                            row[6] = string.IsNullOrEmpty(pendingChapters) ? "" : pendingChapters.Remove(pendingChapters.Length - 1);

                            List<FinalQuizResponse> userFinalQuizRes = finalQuizResponse.FindAll(fqr => fqr.UserId == std.Id);

                            if (userFinalQuizRes.Count == 0)
                            {
                                row[7] = "No";
                                row[8] = "";
                            }
                            else
                            {
                                row[7] = "Yes";
                                row[8] = userFinalQuizRes.Count;
                            }

                            List<StudentTypingStats> userTypingStats = typingStats.FindAll(ts => ts.UserId == std.Id);

                            string pendingTypingLevel = string.Empty;

                            for (int i = 1; i <= typingLevelCount; i++)
                            {
                                StudentTypingStats sts = userTypingStats.Find(uts => uts.Level == i);

                                if (sts == null)
                                {
                                    pendingTypingLevel += i + ",";
                                }
                            }

                            row[9] = string.IsNullOrEmpty(pendingTypingLevel) ? "" : pendingTypingLevel.Remove(pendingTypingLevel.Length - 1);

                            dt.Rows.Add(row);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            string fileName = "student-overall-report-" + course.Course.ToLower() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                            if (!Directory.Exists(pdfFolderPath))
                            {
                                Directory.CreateDirectory(pdfFolderPath);
                            }

                            reportGenerator.Create(pdfFolderPath + fileName, dt, "Report", logPath);

                            dExcelFilesPath.Add(course.Course, pdfFolderPath + fileName);
                        }
                    }

                }

                if (dExcelFilesPath.Count > 0)
                {
                    List<string> ccAddressess;
                    Dictionary<string, List<string>> dic = new Configurations().GetAutoSendOverAllReportsEmailSettings(configFilePath, out ccAddressess);

                    if (dic != null)
                    {
                        foreach (KeyValuePair<string, string> keyPair in dExcelFilesPath)
                        {
                            try
                            {
                                List<string> lToAddress = dic[keyPair.Key];

                                if (lToAddress != null && lToAddress.Count > 0)
                                {
                                    string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                       "Please find attached reports for 'Weekly Progress Reports - Over All Progress (Course, Final-Test and Typing)'" +
                                       "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                                    new Util().SendMail(lToAddress, "", ccAddressess, "Weekly Progress Reports - Over All Progress", body, new List<string> { keyPair.Value });
                                }
                            }
                            catch (Exception)
                            {

                            }

                        }

                        context.Response.Write("Successfully generated MOOC Reports and sent");
                    }
                    else
                    {
                        context.Response.Write("Error: Please add email configuration in configuration.xml file."); 
                    }
                }
            }
            else
            {
                context.Response.Write("Error: Please pass valid user name");
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}