<%@ WebHandler Language="C#" Class="AutoSendReports" %>

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
using MySql.Data.MySqlClient;

public class AutoSendReports : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;
    string pdfFolderPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        string userName = System.Configuration.ConfigurationManager.AppSettings["ReportUserAccount"];

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
                cnxnString = userLogin.CnxnString;
                logPath = context.Server.MapPath(userLogin.LogFilePath);
                configFilePath = context.Server.MapPath(userLogin.ReleaseFilePath);

                string emailContent = "MOOC - These students haven't started the course.<br/><br/>";
                string rollNumbers = "BA - I - Gujarati - ";

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    string query = string.Format("SELECT rollnumber FROM `userdetails` WHERE course = 'BA - Gujarati' and BatchYear={0} and id not in (select distinct userid from chapterquizresponse) order by rollnumber", System.Configuration.ConfigurationManager.AppSettings["BatchYear"]);
                    ITM.Courses.DAOBase.Database db = new ITM.Courses.DAOBase.Database();
                    DataSet ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = string.Format("SELECT rollnumber FROM `userdetails` WHERE course = 'BA - English' and BatchYear={0} and id not in (select distinct userid from chapterquizresponse) order by rollnumber", System.Configuration.ConfigurationManager.AppSettings["BatchYear"]);
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BA - I - English - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = string.Format("SELECT rollnumber FROM `userdetails` WHERE course = 'BAFI' and BatchYear={0} and id not in (select distinct userid from chapterquizresponse) order by rollnumber", System.Configuration.ConfigurationManager.AppSettings["BatchYear"]);
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BAFI - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = string.Format("SELECT rollnumber FROM `userdetails` WHERE course = 'bms' and BatchYear={0} and id not in (select distinct userid from chapterquizresponse) order by rollnumber", System.Configuration.ConfigurationManager.AppSettings["BatchYear"]);
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BMS - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = string.Format("SELECT rollnumber FROM `userdetails` WHERE course = 'BCOM' and BatchYear={0} and id not in (select distinct userid from chapterquizresponse) order by rollnumber", System.Configuration.ConfigurationManager.AppSettings["BatchYear"]);
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BCOM - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    new Util().SendMail("viren@itmonarch.com", "viren@itmonarch.com", "viren.shah1985@gmail.com", "Daily Reports - Course Not Started - " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"), emailContent);

                    emailContent = "MOOC - These students haven't completed final quiz.<br/><br/>";
                    rollNumbers = "BA - I - Gujarati - ";

                    query = "select rollnumber, count(userid) Answers from finalquizresponse F inner join userdetails U on U.id = F.userid where course='BA - Gujarati' group by userid having Answers < 100 order by rollnumber asc";
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = "select rollnumber, count(userid) Answers from finalquizresponse F inner join userdetails U on U.id = F.userid where course='BA - English' group by userid having Answers < 100 order by rollnumber asc";
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BA - I - English - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = "select rollnumber, count(userid) Answers from finalquizresponse F inner join userdetails U on U.id = F.userid where course='BAFI' group by userid having Answers < 100 order by rollnumber asc";
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BAFI - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = "select rollnumber, count(userid) Answers from finalquizresponse F inner join userdetails U on U.id = F.userid where course='bms' group by userid having Answers < 100 order by rollnumber asc";
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BMS - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    query = "select rollnumber, count(userid) Answers from finalquizresponse F inner join userdetails U on U.id = F.userid where course='BCOM' group by userid having Answers < 100 order by rollnumber asc";
                    db = new ITM.Courses.DAOBase.Database();
                    ds = db.SelectDataSet(query, logPath, con);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rollNumbers = "BCOM - ";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            rollNumbers += row["rollnumber"] + ", ";
                        }

                        emailContent += rollNumbers + "<br/><br/>";
                    }

                    con.Close();
                }
                new Util().SendMail("viren@itmonarch.com", "viren@itmonarch.com", "viren.shah1985@gmail.com", "Daily Reports - Final Quiz Not Completed - " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"), emailContent);
            }
        }
    }

    public void ProcessRequest2(HttpContext context)
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
                cnxnString = userLogin.CnxnString;
                logPath = context.Server.MapPath(userLogin.LogFilePath);
                configFilePath = context.Server.MapPath(userLogin.ReleaseFilePath);
                pdfFolderPath = context.Server.MapPath(userLogin.PDFFolderPath);

                AutoSendReportConfig reportConfig = new Configurations().GetAutoSendReportConfig(configFilePath);

                if (reportConfig == null)
                {
                    reportConfig = new AutoSendReportConfig { IsSendChapterWiseProgressReport = true, IsSendCourseCompletedReport = false, IsSendNotStartedCourseReport = true, IsSendNotStartedTypingReport = true, IsSendStepWiseProgressReport = false, IsSendTypingStatsReport = true, IsSendCompletedCourseFinalQuizTypingReport = true };
                }

                UserDetailsDAO courseDAO = new UserDetailsDAO();
                List<UserDetails> courses = courseDAO.GetUniqueCoursesList(cnxnString, logPath);
                StudentReportManager studentReportMgr = new StudentReportManager();
                GenerateExcelReports reportGenerator = new GenerateExcelReports();

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
                    }

                    string toAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"]; // reports send on self address
                    string ccAddress = System.Configuration.ConfigurationManager.AppSettings["ToCC_Hrishi"]; // from address used as CC 

                    List<StudentCourseMapper> courseMapper = new StudentCourseMapperDAO().GetStudentCourseMapperByCourseId(courseId, cnxnString, logPath);
                    if (courseMapper == null) courseMapper = new List<StudentCourseMapper>();

                    #region Completed Course Reports
                    if (reportConfig.IsSendCourseCompletedReport)
                    {
                        List<string> completeStudentXLSX = new List<string>();

                        foreach (UserDetails course in courses)
                        {
                            List<UserDetails> filteredUsers = studentReportMgr.GetStudentReportWhoCompletedCourse(course.Course, cnxnString, logPath);

                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Course Name");

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    DataRow row = dtUser.NewRow();

                                    row[0] = srNo;
                                    row[1] = user.UserName;
                                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                                    row[3] = user.Course;

                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-completed-course-report-" + course.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    completeStudentXLSX.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Course Completed report to Admin */
                        /* Send an email to */
                        if (completeStudentXLSX.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Course Completed'" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Course Completed", body, completeStudentXLSX);
                        }
                    }
                    #endregion

                    /* Chapter wise progress report */
                    #region Chapter wise progress report
                    if (reportConfig.IsSendChapterWiseProgressReport)
                    {
                        List<string> attachedFiles = new List<string>();

                        List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);

                        foreach (UserDetails stream in courses)
                        {
                            List<UserDetails> filteredUsers = studentReportMgr.GetStudentProgressReport(stream.Course, courseId, cnxnString, logPath);

                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Chapter Completed");
                                dtUser.Columns.Add("Course Name");

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        int count = 1;
                                        foreach (ChapterDetails chp in chapters)
                                        {
                                            if (chp.Id == user.BatchYear)
                                            {
                                                user.BatchYear = count;
                                                break;
                                            }
                                            count++;
                                        }

                                        DataRow row = dtUser.NewRow();

                                        row[0] = srNo;
                                        row[1] = user.UserName;
                                        row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                                        row[3] = new Util().ChpaterCompletedValue(user.BatchYear, chapters.Count);
                                        row[4] = user.Course;

                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-progress-report-" + stream.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    attachedFiles.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Send an email to */
                        if (attachedFiles.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Chapter wise'" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Chapter wise", body, attachedFiles);
                        }
                    }
                    #endregion

                    /* Students typing stats report */
                    #region Students typing stats report
                    if (reportConfig.IsSendTypingStatsReport)
                    {
                        List<string> attachedFiles = new List<string>();

                        StudentTypingStatsDAO obj = new StudentTypingStatsDAO();

                        foreach (UserDetails stream in courses)
                        {
                            List<UserDetails> filteredUsers = studentReportMgr.GetStudentTypingStatus(stream.Course, cnxnString, logPath);

                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Course Name");

                                int levels = 23; // in future get from configuration file

                                for (int i = 1; i <= levels; i++)
                                {
                                    string column = "Level " + i;
                                    dtUser.Columns.Add(column);
                                }

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    DataRow row = dtUser.NewRow();
                                    row[0] = srNo;
                                    row[1] = user.UserName;
                                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                                    row[3] = user.Course;

                                    int rowCount = 4;

                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        for (int i = 1; i <= levels; i++, rowCount++)
                                        {
                                            StudentTypingStats objTyping = obj.GetTypingStatsByLevelUserId(user.Id, i, cnxnString, logPath);

                                            if (objTyping != null)
                                            {
                                                row[rowCount] = "Accuracy: " + objTyping.Accuracy + "%, WPM: " + objTyping.GrossWPM + ", Time: " + objTyping.TimeSpanInSeconds + " seconds";
                                            }
                                            else
                                            {
                                                row[rowCount] = "Not attempted yet";
                                            }
                                        }

                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-typing-practice-report-" + stream.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    attachedFiles.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Send an email to */
                        if (attachedFiles.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Typing - Stats'" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Typing - Stats", body, attachedFiles);
                        }
                    }
                    #endregion

                    /* Student not started course yet report */
                    #region Student not started course yet report
                    if (reportConfig.IsSendNotStartedCourseReport)
                    {
                        List<string> attachedFiles = new List<string>();

                        StudentTypingStatsDAO obj = new StudentTypingStatsDAO();

                        foreach (UserDetails stream in courses)
                        {
                            List<UserDetails> filteredUsers = studentReportMgr.GetStudentNotStartedCourse(stream.Course, cnxnString, logPath);

                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Course Name");

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    DataRow row = dtUser.NewRow();

                                    row[0] = srNo;
                                    row[1] = user.UserName.Trim();
                                    row[2] = user.LastName.Trim() + " " + user.FirstName.Trim() + " " + user.FatherName.Trim() + " " + user.MotherName.Trim();
                                    row[3] = user.Course.Trim();

                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-havenot-started-report-" + stream.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    attachedFiles.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Send an email to */
                        if (attachedFiles.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Course Not Started'" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Course Not Started", body, attachedFiles);
                        }
                    }
                    #endregion

                    /* Student not started typing yet report */
                    #region Student not started typing yet report
                    if (reportConfig.IsSendNotStartedTypingReport)
                    {
                        List<string> attachedFiles = new List<string>();

                        StudentTypingStatsDAO obj = new StudentTypingStatsDAO();

                        foreach (UserDetails stream in courses)
                        {
                            List<UserDetails> filteredUsers = obj.GetStudentNotStartedTypingByCourse(stream.Course, cnxnString, logPath);
                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Course Name");

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    DataRow row = dtUser.NewRow();

                                    row[0] = srNo;
                                    row[1] = user.UserName;
                                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                                    row[3] = user.Course;

                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-not-started-typing-report-" + stream.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    attachedFiles.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Send an email to */
                        if (attachedFiles.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Typing - Not Started'" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Typing - Not Started", body, attachedFiles);
                        }
                    }
                    #endregion

                    /* Step wise progress report */
                    #region Step wise progress report
                    if (reportConfig.IsSendStepWiseProgressReport)
                    {
                        List<string> attachedFiles = new List<string>();

                        List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);

                        foreach (UserDetails stream in courses)
                        {
                            List<UserDetails> filteredUsers = studentReportMgr.GetStudentProgressReport(stream.Course, courseId, cnxnString, logPath);

                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Course Name");
                                dtUser.Columns.Add("Step 1 (time spent in minutes / %)");
                                dtUser.Columns.Add("Step 2 (time spent in minutes / %)");
                                dtUser.Columns.Add("Step 3 (time spent in minutes / %)");

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    DataRow row = dtUser.NewRow();
                                    row[0] = srNo;
                                    row[1] = user.UserName;
                                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                                    row[3] = user.Course;

                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        row[4] = "Step 1 " + PopulatingUserTimeCompletePercentageStepwise(user.Id, 1, 7);
                                        row[5] = "Step 2 " + PopulatingUserTimeCompletePercentageStepwise(user.Id, 8, 10);
                                        row[6] = "Step 3 " + PopulatingUserTimeCompletePercentageStepwise(user.Id, 11, 14);

                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-stepwise-progress-report-" + stream.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    attachedFiles.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Send an email to */
                        if (attachedFiles.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Step wise'" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Step wise", body, attachedFiles);
                        }
                    }
                    #endregion

                    #region Completed Course, Final-Quiz and Typing
                    if (reportConfig.IsSendCompletedCourseFinalQuizTypingReport)
                    {
                        List<string> attachedFiles = new List<string>();

                        foreach (UserDetails course in courses)
                        {
                            List<UserDetails> filteredUsers = studentReportMgr.GetStudentReportWhoCompletedCourseFinalQuizTyping(course.Course, cnxnString, logPath);

                            if (filteredUsers != null && filteredUsers.Count > 0)
                            {
                                DataTable dtUser = new DataTable();
                                dtUser.Columns.Add("Sr. No.");
                                dtUser.Columns.Add("User Name");
                                dtUser.Columns.Add("Student Name");
                                dtUser.Columns.Add("Course Name");
                                dtUser.Columns.Add("Score");

                                int srNo = 1;

                                foreach (UserDetails user in filteredUsers)
                                {
                                    DataRow row = dtUser.NewRow();

                                    row[0] = srNo;
                                    row[1] = user.UserName;
                                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                                    row[3] = user.Course;
                                    row[4] = user.MobileNo + "%";

                                    if (courseMapper.Find(cm => cm.UserId == user.Id) != null)
                                    {
                                        dtUser.Rows.Add(row);
                                        srNo++;
                                    }
                                }

                                if (dtUser.Rows.Count > 0)
                                {
                                    string fileName = "Student-completed-course-Final-Quiz-Typing-report-" + course.Course + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                                    if (!Directory.Exists(pdfFolderPath))
                                    {
                                        Directory.CreateDirectory(pdfFolderPath);
                                    }

                                    reportGenerator.Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                                    attachedFiles.Add(pdfFolderPath + fileName);
                                }
                            }
                        }

                        /* Full Completed [Course, Final-Quiz and Typing] report to Admin */
                        /* Send an email to */
                        if (attachedFiles.Count > 0)
                        {
                            string body = @"Hello Sir/Ma'am, <br/><br/>" +
                                    "Please find attached reports for 'Weekly Progress Reports - Full Completed [Course, Final-Quiz and Typing] '" +
                                    "<br/><br/><b>PS - This is auto-generated email. Please do not reply.</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            new Util().SendMail(toAddress, "", ccAddress, "Weekly Progress Reports - Full Completed [Course, Final-Quiz and Typing]", body, attachedFiles);
                        }
                    }
                    #endregion

                    context.Response.Write("Successfully generated MOOC Reports and sent");
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


    public string PopulatingUserTimeCompletePercentageStepwise(int userId, int startChapterId, int endChapterId)
    {
        try
        {
            decimal time = 0;
            decimal percentage = 0;

            ChapterDetailsDAO chapterDAO = new ChapterDetailsDAO();
            UserTimeTrackerDAO sectionDAO = new UserTimeTrackerDAO();
            // for step one
            int totalCounter = 0, userCounter = 0;

            List<ChapterDetails> chapters = chapterDAO.GetChaptersBetweenTwoIds(startChapterId, endChapterId, cnxnString, logPath);
            foreach (ChapterDetails chp in chapters)
            {
                time += sectionDAO.GetTimeTakenByUserInMinuts(userId, chp.Id, cnxnString, logPath);

                List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                foreach (ChapterSection sec in sections)
                {
                    totalCounter++;
                    UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(userId, chp.Id, sec.Id, cnxnString, logPath);

                    if (userChapterStatus != null)
                    {
                        userCounter++;
                    }
                }
            }

            percentage = Math.Round((decimal.Parse(userCounter.ToString()) / decimal.Parse(totalCounter.ToString())) * 100, 2);

            return "(" + time + " / " + percentage + "%)";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}