using QuarterlyReports.Helpers;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QuarterlyReports.Controllers
{
    public class ImportController : ApiController
    {
        DatabaseOperations op;
        CommonFeedbackDBOperations dbop;
        public ImportController()
        {
            op = new DatabaseOperations();
            dbop = new CommonFeedbackDBOperations();
        }

        [HttpPost]
        public HttpResponseMessage ImportStudentFeedback()
        {
            try
            {
                if (HttpContext.Current.Request.QueryString["year"] == "2016-2017")
                {
                    ImportLegacyData();
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Success" });
                }

                SubjectTeacherMapper subjectTeacherMapper = null;
                List<ExcelTableMapper> excelTableMapper = null;
                List<TeacherDetails> teachers = null;
                Dictionary<string, string> subjectList = new Dictionary<string, string>();
                List<User> users = new List<User>();
                bool teacherExist = false;
                List<TeacherFeedback> feedbackList = new List<TeacherFeedback>();
                string currentSem = System.Web.Configuration.WebConfigurationManager.AppSettings["CurrentSem"];
                string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + "teachers_105.json";
                string targetPath = SaveFile();

                using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/105_Subject_Teacher_Mapper.json")))
                {
                    string json = r.ReadToEnd();
                    subjectTeacherMapper = JsonConvert.DeserializeObject<SubjectTeacherMapper>(json);
                }

                using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/105_Excel_Table_Mapper.json")))
                {
                    string json = r.ReadToEnd();
                    excelTableMapper = JsonConvert.DeserializeObject<List<ExcelTableMapper>>(json);
                }

                if (File.Exists(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string json = r.ReadToEnd();
                        teachers = JsonConvert.DeserializeObject<List<TeacherDetails>>(json);
                        teacherExist = true;
                    }
                }
                else
                {
                    teachers = new List<TeacherDetails>();
                }

                // Read Excel file
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(targetPath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();


                    var sheets = workbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    var relationshipId = sheets.First().Id.Value;
                    worksheetPart = (WorksheetPart)workbookPart.GetPartById(relationshipId);
                    var workSheet = worksheetPart.Worksheet;
                    var sheetData = workSheet.GetFirstChild<SheetData>();
                    var rows = sheetData.Descendants<Row>().ToList();

                    // 1. Read excel and add new teachers - Write method to compare & insert the teacher
                    for (int i = 2; i < rows.Count; i++)
                    {
                        string subjectCode = string.Empty;
                        string teacherName = string.Empty;
                        User user = new User();
                        TeacherFeedback feedback = new TeacherFeedback();
                        foreach (var cell in rows.ElementAt(i - 1).Cast<Cell>())
                        {
                            string columnName = GetColumnName(cell.CellReference);

                            if (columnName == subjectTeacherMapper.SubjectCode)
                            {
                                subjectCode = cell.CellValue.InnerXml;

                            }
                            else if (columnName == subjectTeacherMapper.TeacherName)
                            {
                                teacherName = GetCellValue(spreadsheetDocument, cell);
                            }

                            ExcelTableMapper t = excelTableMapper.Find(f => f.ExcelColumn == columnName);
                            if (t != null && t.TableName.ToLower() == "tbluser")
                            {
                                switch (t.DatabaseColumnName)
                                {
                                    case "academicyear":
                                        user.AcedemicYear = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "userid":
                                        user.UserId = long.Parse(GetCellValue(spreadsheetDocument, cell));
                                        feedback.UserId = user.UserId.ToString();
                                        feedback.CollegeCode = "105";
                                        feedback.A24 = HttpContext.Current.Request.QueryString["year"];
                                        break;
                                    case "firstname":
                                        user.FirstName = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "lastname":
                                        user.LastName = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "subcourse":
                                        Cell c = GetCell(workSheet, t.ExcelColumn, i);
                                        if (c != null)
                                        {
                                            user.Course = t.CourseName;
                                            user.SubCourse = GetCellValue(spreadsheetDocument, c);
                                        }
                                        break;
                                }
                            }
                            if (t != null && t.TableName.ToLower() == "teacherfeedback")
                            {

                                switch (t.ExcelColumn)
                                {
                                    case "AG":
                                        feedback.A1 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AH":
                                        feedback.A2 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AI":
                                        feedback.A3 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AJ":
                                        feedback.A4 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AK":
                                        feedback.A5 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AL":
                                        feedback.A6 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AM":
                                        feedback.A7 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AN":
                                        feedback.A8 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AO":
                                        feedback.A9 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AP":
                                        feedback.A10 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AQ":
                                        feedback.A11 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AR":
                                        feedback.A12 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AS":
                                        feedback.A13 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AT":
                                        feedback.A14 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AU":
                                        feedback.A15 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AV":
                                        feedback.A16 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AW":
                                        feedback.A17 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AX":
                                        feedback.A18 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AY":
                                        feedback.A19 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AZ":
                                        feedback.A20 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "BA":
                                        feedback.A23 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(subjectCode))
                        {
                            continue;
                        }
                        // Add this list in JSON file teachers_105.json
                        // TODO : Anil - Create new file - teachers_105.json - sem - even, subject name should be blank,
                        // subject code, teacher name, course, subcourse should be from excel
                        // teacher code - dynamically assign value of "course"N - remove all special characters from course name.

                        /*if (!subjectList.ContainsKey(subjectCode))
                        {
                            TeacherDetails td = new TeacherDetails();
                            td.Course = user.Course;
                            td.SubCourse = user.SubCourse;
                            td.SubjectCode = subjectCode;
                            td.TeacherName = teacherName;
                            td.Semester = currentSem;
                            td.SubjectName = string.Empty;
                            td.TeacherCode = "TCode" + (subjectList.Count + 1);
                            //teachers.Add(td);
                            subjectList.Add(subjectCode, teacherName);
                        }*/

                        string sem = System.Web.Configuration.WebConfigurationManager.AppSettings["CurrentSem"];
                        // 2. Read excel and add new students - GetUserDetails(userID), InsertUserDeatils(LoginResponse)
                        if (users.Find(u => u.UserId == user.UserId) == null)
                        {
                            user.CollegeCode = "105";
                            user.CurrentSemester = sem;
                            user.IsActive = "true";
                            user.IsFinalYearStudent = "false";
                            user.UserType = "Customer";
                            if (string.IsNullOrEmpty(user.MobileNumber))
                            {
                                user.MobileNumber = (users.Count + 1).ToString();
                            }
                            if (string.IsNullOrEmpty(user.LastName))
                            {
                                user.LastName = "123";
                            }
                            users.Add(user);
                        }

                        // 3. Read excel and add new feedback - DatabaseOperations.InsertTeacherFeedback(TeacherFeedback)
                        if (feedbackList.Find(f => f.CollegeCode == feedback.UserId) == null)
                        {
                            feedback.SubjectCode = subjectCode;
                            feedback.CollegeCode = "105";
                            if (teachers.FirstOrDefault(t => t.SubjectCode == subjectCode) != null)
                            {
                                var teacher = teachers.FirstOrDefault(t => t.SubjectCode == subjectCode && t.Semester == sem && t.Course == user.Course && t.SubCourse == user.SubCourse && t.TeacherName == teacherName);

                                if (teacher == null)
                                {
                                    System.Diagnostics.Debug.WriteLine("Subject:" + subjectCode + ",Course:" + user.Course + ", SubCourse:" + user.SubCourse);
                                }
                                else
                                {
                                    feedback.TeacherCode = teacher.TeacherCode;
                                    feedbackList.Add(feedback);
                                }
                            }
                        }

                        // TODO : Anil - Save users in tblusertable. Check if user already exists. If no then insert. If yes dont do anything
                        // TODO : Anil - Save feedback in teacherfeedback table. check if feedback exists. Rely on feedback.collegecode property. It should have unique value

                        int newUserid = 0;
                        newUserid = op.GetSubmissionId(Convert.ToString(user.UserId));
                        if (newUserid == 0)
                        {
                            LoginResponse u = new LoginResponse();
                            u.FirstName = user.FirstName;
                            u.LastName = user.LastName;
                            u.MobileNumber = user.MobileNumber;
                            u.CollegeCode = user.CollegeCode;
                            u.Course = user.Course;
                            u.SubCourse = user.SubCourse;
                            u.CurrentSemester = sem;
                            u.IsActive = user.IsActive;
                            u.IsFinalYearStudent = user.IsFinalYearStudent;
                            u.RoleType = user.UserType;
                            u.AcedemicYear = user.AcedemicYear;
                            u.SubmissionId = user.UserId;
                            newUserid = op.InsertUserDeatils(u);

                            feedback.UserId = newUserid.ToString();
                            bool isFeedbackInsertd = op.InsertTeacherFeedback(feedback) > 0;

                        }
                    }

                    if (!teacherExist)
                    {
                        //Save Teachers
                        System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(teachers, Formatting.Indented));
                    }

                }
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Success" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ImportLegacyData()
        {
            try
            {
                SubjectTeacherMapper subjectTeacherMapper = null;
                List<ExcelTableMapper> excelTableMapper = null;
                List<TeacherDetails> teachers = null;
                Dictionary<string, string> subjectList = new Dictionary<string, string>();
                List<User> users = new List<User>();
                bool teacherExist = false;
                List<TeacherFeedback> feedbackList = new List<TeacherFeedback>();
                string currentSem = System.Web.Configuration.WebConfigurationManager.AppSettings["CurrentSem"];
                string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + "teachers_105.json";
                string targetPath = SaveFile();

                using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/105_Subject_Teacher_Mapper.json")))
                {
                    string json = r.ReadToEnd();
                    subjectTeacherMapper = JsonConvert.DeserializeObject<SubjectTeacherMapper>(json);
                }

                using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/105_Excel_Table_Mapper.json")))
                {
                    string json = r.ReadToEnd();
                    excelTableMapper = JsonConvert.DeserializeObject<List<ExcelTableMapper>>(json);
                }

                if (File.Exists(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string json = r.ReadToEnd();
                        teachers = JsonConvert.DeserializeObject<List<TeacherDetails>>(json);
                        teacherExist = true;
                    }
                }
                else
                {
                    teachers = new List<TeacherDetails>();
                }

                // Read Excel file
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(targetPath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();


                    var sheets = workbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    var relationshipId = sheets.First().Id.Value;
                    worksheetPart = (WorksheetPart)workbookPart.GetPartById(relationshipId);
                    var workSheet = worksheetPart.Worksheet;
                    var sheetData = workSheet.GetFirstChild<SheetData>();
                    var rows = sheetData.Descendants<Row>().ToList();

                    // 1. Read excel and add new teachers - Write method to compare & insert the teacher
                    for (int i = 2; i < rows.Count; i++)
                    {
                        string subjectCode = string.Empty;
                        string teacherName = string.Empty;
                        User user = new User();
                        TeacherFeedback feedback = new TeacherFeedback();
                        foreach (var cell in rows.ElementAt(i).Cast<Cell>())
                        {
                            string columnName = GetColumnName(cell.CellReference);

                            if (columnName == subjectTeacherMapper.SubjectCode)
                            {
                                subjectCode = cell.CellValue.InnerXml;

                            }
                            else if (columnName == subjectTeacherMapper.TeacherName)
                            {
                                teacherName = GetCellValue(spreadsheetDocument, cell);
                            }

                            ExcelTableMapper t = excelTableMapper.Find(f => f.ExcelColumn == columnName);
                            if (t != null && t.TableName.ToLower() == "tbluser")
                            {
                                switch (t.DatabaseColumnName)
                                {
                                    case "academicyear":
                                        user.AcedemicYear = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "userid":
                                        user.UserId = long.Parse(GetCellValue(spreadsheetDocument, cell));
                                        feedback.UserId = user.UserId.ToString();
                                        feedback.CollegeCode = "105";
                                        feedback.A24 = HttpContext.Current.Request.QueryString["year"];
                                        break;
                                    case "firstname":
                                        user.FirstName = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "lastname":
                                        user.LastName = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "subcourse":
                                        Cell c = GetCell(workSheet, t.ExcelColumn, i);
                                        if (c != null)
                                        {
                                            user.Course = t.CourseName;
                                            user.SubCourse = GetCellValue(spreadsheetDocument, c);
                                        }
                                        break;
                                }
                            }
                            if (t != null && t.TableName.ToLower() == "teacherfeedback")
                            {

                                switch (t.ExcelColumn)
                                {
                                    case "AG":
                                    case "AZ":
                                    case "BR":
                                    case "CI":
                                    case "DB":
                                    case "DT":
                                    case "EL":
                                    case "FD":
                                        feedback.A1 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AH":
                                    case "BA":
                                    case "BS":
                                    case "CJ":
                                    case "DC":
                                    case "DU":
                                    case "EM":
                                    case "FE":
                                        feedback.A2 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AI":
                                    case "BB":
                                    case "BT":
                                    case "CK":
                                    case "DD":
                                    case "DV":
                                    case "EN":
                                    case "FF":
                                        feedback.A3 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AJ":
                                    case "BC":
                                    case "BU":
                                    case "CL":
                                    case "DE":
                                    case "DW":
                                    case "EO":
                                    case "FI":
                                        feedback.A4 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AK":
                                    case "BD":
                                    case "BV":
                                    case "CM":
                                    case "DF":
                                    case "DX":
                                    case "EP":
                                    case "FJ":
                                        feedback.A5 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AL":
                                    case "BE":
                                    case "BW":
                                    case "CN":
                                    case "DG":
                                    case "DY":
                                    case "EQ":
                                    case "FK":
                                        feedback.A6 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AM":
                                    case "BF":
                                    case "BX":
                                    case "CO":
                                    case "DH":
                                    case "DZ":
                                    case "ER":
                                    case "FL":
                                        feedback.A7 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AN":
                                    case "BG":
                                    case "BY":
                                    case "CP":
                                    case "DI":
                                    case "EA":
                                    case "ES":
                                    case "FM":
                                        feedback.A8 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AO":
                                    case "BH":
                                    case "BZ":
                                    case "CQ":
                                    case "DJ":
                                    case "EB":
                                    case "ET":
                                    case "FN":
                                        feedback.A9 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AP":
                                    case "BI":
                                    case "CA":
                                    case "CR":
                                    case "DK":
                                    case "EC":
                                    case "EU":
                                    case "FO":
                                        feedback.A10 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AQ":
                                    case "BJ":
                                    case "CB":
                                    case "CS":
                                    case "DL":
                                    case "ED":
                                    case "EV":
                                    case "FP":
                                        feedback.A11 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AR":
                                    case "BK":
                                    case "CC":
                                    case "CT":
                                    case "DM":
                                    case "EE":
                                    case "EW":
                                    case "FQ":
                                        feedback.A12 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AS":
                                    case "BL":
                                    case "CD":
                                    case "CU":
                                    case "DN":
                                    case "EF":
                                    case "EX":
                                    case "FR":
                                        feedback.A13 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AT":
                                    case "BM":
                                    case "CE":
                                    case "CV":
                                    case "DO":
                                    case "EG":
                                    case "EY":
                                    case "FS":
                                        feedback.A14 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AU":
                                    case "BN":
                                    case "CF":
                                    case "CW":
                                    case "DP":
                                    case "EH":
                                    case "EZ":
                                    case "FT":
                                        feedback.A15 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AV":
                                    case "BO":
                                    case "CG":
                                    case "CX":
                                    case "DQ":
                                    case "EI":
                                    case "FA":
                                    case "FU":
                                        feedback.A16 = GetCellValue(spreadsheetDocument, cell);
                                        break;
                                    case "AW":
                                    case "BP":
                                    case "CH":
                                    case "CY":
                                        feedback.A17 = GetCellValue(spreadsheetDocument, cell);
                                        InsertNewFeedback(teachers, users, feedbackList, subjectCode, user, feedback);
                                        break;
                                        /*case "AX":
                                            feedback.A18 = GetCellValue(spreadsheetDocument, cell);
                                            break;
                                        case "AY":
                                            feedback.A19 = GetCellValue(spreadsheetDocument, cell);
                                            break;
                                        case "AZ":
                                            feedback.A20 = GetCellValue(spreadsheetDocument, cell);
                                            break;
                                        case "BA":
                                            feedback.A23 = GetCellValue(spreadsheetDocument, cell);
                                            break;*/
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(subjectCode))
                        {
                            continue;
                        }


                    }


                }
                //return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Success" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        private void InsertNewFeedback(List<TeacherDetails> teachers, List<User> users, List<TeacherFeedback> feedbackList, string subjectCode, User user, TeacherFeedback feedback)
        {
            string sem = System.Web.Configuration.WebConfigurationManager.AppSettings["CurrentSem"];
            // 2. Read excel and add new students - GetUserDetails(userID), InsertUserDeatils(LoginResponse)
            if (users.Find(u => u.UserId == user.UserId) == null)
            {
                user.CollegeCode = "105";
                user.CurrentSemester = sem;
                user.IsActive = "true";
                user.IsFinalYearStudent = "false";
                user.UserType = "Customer";
                if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    user.MobileNumber = (users.Count + 1).ToString();
                }
                if (string.IsNullOrEmpty(user.LastName))
                {
                    user.LastName = "123";
                }
                users.Add(user);
            }

            // 3. Read excel and add new feedback - DatabaseOperations.InsertTeacherFeedback(TeacherFeedback)
            if (feedbackList.Find(f => f.CollegeCode == feedback.UserId) == null)
            {
                feedback.SubjectCode = subjectCode;
                feedback.CollegeCode = "105";
                if (teachers.FirstOrDefault(t => t.SubjectCode == subjectCode) != null)
                {
                    feedback.TeacherCode = teachers.FirstOrDefault(t => t.SubjectCode == subjectCode).TeacherCode;
                    feedbackList.Add(feedback);
                }
            }

            // TODO : Anil - Save users in tblusertable. Check if user already exists. If no then insert. If yes dont do anything
            // TODO : Anil - Save feedback in teacherfeedback table. check if feedback exists. Rely on feedback.collegecode property. It should have unique value

            int newUserid = 0;
            newUserid = op.GetSubmissionId(Convert.ToString(user.UserId));
            if (newUserid == 0)
            {
                LoginResponse u = new LoginResponse();
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.MobileNumber = user.MobileNumber;
                u.CollegeCode = user.CollegeCode;
                u.Course = user.Course;
                u.SubCourse = user.SubCourse;
                u.CurrentSemester = sem;
                u.IsActive = user.IsActive;
                u.IsFinalYearStudent = user.IsFinalYearStudent;
                u.RoleType = user.UserType;
                u.AcedemicYear = user.AcedemicYear;
                u.SubmissionId = user.UserId;
                newUserid = op.InsertUserDeatils(u);

                feedback.UserId = newUserid.ToString();
                bool isFeedbackInsertd = op.InsertTeacherFeedback(feedback) > 0;

            }
        }
        [HttpPost]
        public HttpResponseMessage ImportStaffFeedback()
        {
            StaffExcelColumnMapper excelColumnMapper = null;
            try
            {
                using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/105_Staff_Excel_Column_Mapper.json")))
                {
                    string json = r.ReadToEnd();
                    excelColumnMapper = JsonConvert.DeserializeObject<StaffExcelColumnMapper>(json);
                }

                string filePath = SaveFile();
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();


                    var sheets = workbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    var relationshipId = sheets.First().Id.Value;
                    worksheetPart = (WorksheetPart)workbookPart.GetPartById(relationshipId);
                    var workSheet = worksheetPart.Worksheet;
                    var sheetData = workSheet.GetFirstChild<SheetData>();
                    var rows = sheetData.Descendants<Row>().ToList();

                    string searchType = string.Empty;
                    string aaUserId = op.GetUserIdByUserType("105", "AcademicAdministrator").ToString();
                    string ceUserId = op.GetUserIdByUserType("105", "CurriculumEvaluation").ToString();
                    string jseUserId = op.GetUserIdByUserType("105", "JobSatisfactionEmployeeRelation").ToString();
                    string jswUserId = op.GetUserIdByUserType("105", "JobSatisfactionWorkPlace").ToString();
                    string jstrUserId = op.GetUserIdByUserType("105", "JobSatisfactionTechnologyResource").ToString();
                    string lUserId = op.GetUserIdByUserType("105", "Library").ToString();
                    string powUserId = op.GetUserIdByUserType("105", "PeerOwnDepartment").ToString();
                    string podUserId = op.GetUserIdByUserType("105", "PeerOtherDepartment").ToString();
                    string padUserId = op.GetUserIdByUserType("105", "PeerAnyDepartment").ToString();

                    // 1. Read excel and add new teachers - Write method to compare & insert the teacher
                    Cell c = null;
                    for (int i = 2; i < rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(searchType))
                        {
                            c = GetCell(workSheet, "F", i);
                            searchType = GetCellValue(spreadsheetDocument, c);
                        }

                        switch (searchType)
                        {
                            case "Academic Administrator":
                                #region AA
                                AcademicAdministrator aa = new AcademicAdministrator();
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[0], i);
                                aa.IPAddress = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[1], i);
                                aa.Id = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[2], i);
                                aa.A1 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[3], i);
                                aa.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[4], i);
                                aa.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[5], i);
                                aa.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[6], i);
                                aa.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[7], i);
                                aa.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[8], i);
                                aa.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[9], i);
                                aa.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[10], i);
                                aa.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[11], i);
                                aa.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[12], i);
                                aa.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[13], i);
                                aa.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[14], i);
                                aa.A13 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[15], i);
                                aa.A14 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[16], i);
                                aa.A15 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[17], i);
                                aa.A16 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[18], i);
                                aa.A17 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[19], i);
                                aa.A18 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[20], i);
                                aa.A19 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[21], i);
                                aa.A20 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[22], i);
                                aa.A21 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[23], i);
                                aa.A22 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[24], i);
                                aa.A23 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[25], i);
                                aa.A24 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[26], i);
                                aa.A25 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[27], i);
                                aa.A26 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[28], i);
                                aa.A27 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[29], i);
                                aa.A28 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[30], i);
                                aa.A29 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[31], i);
                                aa.A30 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.AcademicAdmin[32], i);
                                aa.A31 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    aa.A32 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    aa.A32 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                aa.UserType = "AcademicAdministrator";
                                aa.UserId = aaUserId;
                                aa.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(aa.A1) && string.IsNullOrEmpty(aa.A2) && string.IsNullOrEmpty(aa.A3)))
                                    dbop.InsertAcedemicFeedback(aa);
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - new table.
                                // Check it should be unique
                                break;
                            #endregion
                            case "Curriculum Evaluation":
                                // Type 5
                                #region Curriculum
                                TeacherFeedback cefeedback = new TeacherFeedback();
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[0], i);
                                cefeedback.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[2], i);
                                cefeedback.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[3], i);
                                cefeedback.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[4], i);
                                cefeedback.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[5], i);
                                cefeedback.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[6], i);
                                cefeedback.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[7], i);
                                cefeedback.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[8], i);
                                cefeedback.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[9], i);
                                cefeedback.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[10], i);
                                cefeedback.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[11], i);
                                cefeedback.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[12], i);
                                cefeedback.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[13], i);
                                cefeedback.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.CurriculumEvaluation[14], i);
                                cefeedback.A13 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    cefeedback.A14 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    cefeedback.A14 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                cefeedback.UserType = "CurriculumEvaluation";
                                cefeedback.UserId = ceUserId;
                                cefeedback.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(cefeedback.A1) && string.IsNullOrEmpty(cefeedback.A2) && string.IsNullOrEmpty(cefeedback.A3)))
                                    dbop.InsertFeedback(cefeedback);
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique
                                // FeedbackType = PeerOwnDepartment, PeerOtherDepartment, PeerAnyDepartment, Library, CurriculumEvaluation, JobSatisfactionWorkPlace, JobSatisfactionEmployeeRelation, JobSatisfactionWorkPlace
                                break;
                            #endregion
                            case "JOB SATISFACTION SURVEY":
                                #region Job
                                // JobSatisfactionEmployeeRelation = Type 6
                                TeacherFeedback jsFeedback1 = new TeacherFeedback();
                                TeacherFeedback jsFeedback2 = new TeacherFeedback();
                                TeacherFeedback jsFeedback3 = new TeacherFeedback();

                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[0], i);
                                jsFeedback1.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[2], i);
                                jsFeedback1.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[3], i);
                                jsFeedback1.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[4], i);
                                jsFeedback1.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[5], i);
                                jsFeedback1.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[6], i);
                                jsFeedback1.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[7], i);
                                jsFeedback1.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[8], i);
                                jsFeedback1.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[9], i);
                                jsFeedback1.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[10], i);
                                jsFeedback1.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[11], i);
                                jsFeedback1.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[12], i);
                                jsFeedback1.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionEmployeeRelation[13], i);
                                jsFeedback1.A12 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    jsFeedback1.A13 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    jsFeedback1.A13 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                jsFeedback1.UserType = "JobSatisfactionEmployeeRelation";
                                jsFeedback1.UserId = jseUserId;
                                jsFeedback1.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(jsFeedback1.A1) && string.IsNullOrEmpty(jsFeedback1.A2) && string.IsNullOrEmpty(jsFeedback1.A3)))
                                    dbop.InsertFeedback(jsFeedback1);

                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[0], i);
                                jsFeedback2.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[2], i);
                                jsFeedback2.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[3], i);
                                jsFeedback2.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[4], i);
                                jsFeedback2.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[5], i);
                                jsFeedback2.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[6], i);
                                jsFeedback2.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[7], i);
                                jsFeedback2.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[8], i);
                                jsFeedback2.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[9], i);
                                jsFeedback2.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[10], i);
                                jsFeedback2.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[11], i);
                                jsFeedback2.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[12], i);
                                jsFeedback2.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[13], i);
                                jsFeedback2.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[14], i);
                                jsFeedback2.A13 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[15], i);
                                jsFeedback2.A14 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[16], i);
                                jsFeedback2.A15 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[17], i);
                                jsFeedback2.A16 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[18], i);
                                jsFeedback2.A17 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionWorkPlace[19], i);
                                jsFeedback2.A18 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    jsFeedback2.A19 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    jsFeedback2.A19 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                jsFeedback2.UserType = "JobSatisfactionWorkPlace";
                                jsFeedback2.UserId = jswUserId;
                                jsFeedback2.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(jsFeedback2.A1) && string.IsNullOrEmpty(jsFeedback2.A2) && string.IsNullOrEmpty(jsFeedback2.A3)))
                                    dbop.InsertFeedback(jsFeedback2);
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique

                                // JobSatisfactionTechnologyResource = Type 8
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[0], i);
                                jsFeedback3.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[2], i);
                                jsFeedback3.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[3], i);
                                jsFeedback3.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[4], i);
                                jsFeedback3.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[5], i);
                                jsFeedback3.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[6], i);
                                jsFeedback3.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[7], i);
                                jsFeedback3.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[8], i);
                                jsFeedback3.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[9], i);
                                jsFeedback3.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[10], i);
                                jsFeedback3.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[11], i);
                                jsFeedback3.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[12], i);
                                jsFeedback3.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[13], i);
                                jsFeedback3.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[14], i);
                                jsFeedback3.A13 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[15], i);
                                jsFeedback3.A14 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[16], i);
                                jsFeedback3.A15 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[17], i);
                                jsFeedback3.A16 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[18], i);
                                jsFeedback3.A17 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.JobSatisfactionTechnologyResource[19], i);
                                jsFeedback3.A18 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    jsFeedback3.A19 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    jsFeedback3.A19 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                jsFeedback3.UserType = "JobSatisfactionTechnologyResource";
                                jsFeedback3.UserId = jstrUserId;
                                jsFeedback3.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(jsFeedback3.A1) && string.IsNullOrEmpty(jsFeedback3.A2) && string.IsNullOrEmpty(jsFeedback3.A3)))
                                    dbop.InsertFeedback(jsFeedback3);
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique
                                break;
                            #endregion
                            case "Library Assessment":
                                #region Library
                                // Type 4
                                TeacherFeedback feedback = new TeacherFeedback();
                                c = GetCell(workSheet, excelColumnMapper.Library[0], i);
                                feedback.IPAddress = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[2], i);
                                feedback.A1 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[3], i);
                                feedback.A2 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[4], i);
                                feedback.A3 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[5], i);
                                feedback.A4 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[6], i);
                                feedback.A5 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[7], i);
                                feedback.A6 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[8], i);
                                feedback.A7 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[9], i);
                                feedback.A8 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[10], i);
                                feedback.A9 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[11], i);
                                feedback.A10 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[12], i);
                                feedback.A11 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[13], i);
                                feedback.A12 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[14], i);
                                feedback.A13 = GetCellValue(spreadsheetDocument, c);

                                c = GetCell(workSheet, excelColumnMapper.Library[15], i);
                                feedback.A14 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    feedback.A15 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    feedback.A15 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                feedback.UserType = "Library";
                                feedback.UserId = lUserId;
                                feedback.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(feedback.A1) && string.IsNullOrEmpty(feedback.A2) && string.IsNullOrEmpty(feedback.A3)))
                                    dbop.InsertFeedback(feedback);
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique
                                break;
                            #endregion
                            case "Peer Review":
                                // PeerOwnDepartment = Type 1, PeerOtherDepartment = Type 2, PeerAnyDepartment = Type 3
                                TeacherFeedback feedback1 = new TeacherFeedback();
                                TeacherFeedback feedback2 = new TeacherFeedback();
                                TeacherFeedback feedback3 = new TeacherFeedback();
                                // Read data for own department

                                #region Own Department
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[0], i);
                                feedback1.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[2], i);
                                feedback1.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[3], i);
                                feedback1.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[4], i);
                                feedback1.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[5], i);
                                feedback1.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[6], i);
                                feedback1.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[7], i);
                                feedback1.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[8], i);
                                feedback1.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[9], i);
                                feedback1.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[10], i);
                                feedback1.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[11], i);
                                feedback1.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[12], i);
                                feedback1.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[13], i);
                                feedback1.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[14], i);
                                feedback1.A13 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[15], i);
                                feedback1.A14 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[16], i);
                                feedback1.A15 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOwnDepartment[17], i);
                                feedback1.A16 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    feedback1.A17 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    feedback1.A17 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }
                                // Feedback given by which user
                                c = GetCell(workSheet, excelColumnMapper.PeerReviewOwnDepartmentNames[0], i);
                                feedback1.A18 = GetCellValue(spreadsheetDocument, c);
                                feedback1.A18 = feedback1.A18.Replace(" ", "").Trim().ToUpper();
                                // Feedback given by (department name)
                                c = GetCell(workSheet, excelColumnMapper.PeerReviewOwnDepartmentNames[1], i);
                                feedback1.A19 = GetCellValue(spreadsheetDocument, c);

                                // Feedback given by (department name)
                                for (int k = 2; k < 18; k++)
                                {
                                    string departmentName = GetDepartmentName(excelColumnMapper.PeerReviewOwnDepartmentNames[k]);
                                    c = GetCell(workSheet, excelColumnMapper.PeerReviewOwnDepartmentNames[k], i);
                                    string departmentPerson = GetCellValue(spreadsheetDocument, c);
                                    if (!string.IsNullOrEmpty(departmentPerson))
                                    {
                                        feedback1.A20 = departmentName;
                                        feedback1.A23 = departmentPerson;
                                        break;
                                    }
                                }

                                feedback1.UserType = "PeerOwnDepartment";
                                feedback1.UserId = powUserId;
                                feedback1.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(feedback1.A1) && string.IsNullOrEmpty(feedback1.A2) && string.IsNullOrEmpty(feedback1.A3)))
                                    dbop.InsertFeedback(feedback1);
                                #endregion
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique

                                // Read data for other department
                                #region Other Department
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[0], i);
                                feedback2.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[2], i);
                                feedback2.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[3], i);
                                feedback2.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[4], i);
                                feedback2.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[5], i);
                                feedback2.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[6], i);
                                feedback2.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[7], i);
                                feedback2.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[8], i);
                                feedback2.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[9], i);
                                feedback2.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[10], i);
                                feedback2.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[11], i);
                                feedback2.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[12], i);
                                feedback2.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[13], i);
                                feedback2.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[14], i);
                                feedback2.A13 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[15], i);
                                feedback2.A14 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[16], i);
                                feedback2.A15 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerOtherDepartment[17], i);
                                feedback2.A16 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    feedback2.A17 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    feedback2.A17 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                // Feedback given by which user
                                c = GetCell(workSheet, excelColumnMapper.PeerReviewOtherDepartmentNames[0], i);
                                feedback2.A18 = GetCellValue(spreadsheetDocument, c);
                                feedback2.A18 = feedback2.A18.Replace(" ", "").Trim().ToUpper();
                                // Feedback given by (department name)
                                c = GetCell(workSheet, excelColumnMapper.PeerReviewOtherDepartmentNames[1], i);
                                feedback2.A19 = GetCellValue(spreadsheetDocument, c);

                                // Feedback given by (department name)
                                for (int k = 2; k < 18; k++)
                                {
                                    string departmentName = GetDepartmentName(excelColumnMapper.PeerReviewOtherDepartmentNames[k]);
                                    c = GetCell(workSheet, excelColumnMapper.PeerReviewOtherDepartmentNames[k], i);
                                    string departmentPerson = GetCellValue(spreadsheetDocument, c);
                                    if (!string.IsNullOrEmpty(departmentPerson))
                                    {
                                        feedback2.A20 = departmentName;
                                        feedback2.A23 = departmentPerson;
                                        break;
                                    }
                                }

                                feedback2.UserType = "PeerOtherDepartment";
                                feedback2.UserId = podUserId;
                                feedback2.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(feedback2.A1) && string.IsNullOrEmpty(feedback2.A2) && string.IsNullOrEmpty(feedback2.A3)))
                                    dbop.InsertFeedback(feedback2);
                                #endregion
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique

                                #region Any Department
                                // Read data for any department
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[0], i);
                                feedback3.IPAddress = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[2], i);
                                feedback3.A1 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[3], i);
                                feedback3.A2 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[4], i);
                                feedback3.A3 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[5], i);
                                feedback3.A4 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[6], i);
                                feedback3.A5 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[7], i);
                                feedback3.A6 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[8], i);
                                feedback3.A7 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[9], i);
                                feedback3.A8 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[10], i);
                                feedback3.A9 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[11], i);
                                feedback3.A10 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[12], i);
                                feedback3.A11 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[13], i);
                                feedback3.A12 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[14], i);
                                feedback3.A13 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[15], i);
                                feedback3.A14 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[16], i);
                                feedback3.A15 = GetCellValue(spreadsheetDocument, c);
                                c = GetCell(workSheet, excelColumnMapper.PeerAnyDepartment[17], i);
                                feedback3.A16 = GetCellValue(spreadsheetDocument, c);

                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    feedback3.A17 = (DateTime.Now.Year - 1) + "-" + (DateTime.Now.Year);
                                }
                                else
                                {
                                    feedback3.A17 = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1);
                                }

                                // Feedback given by which user
                                c = GetCell(workSheet, excelColumnMapper.PeerReviewAnyDepartmentNames[0], i);
                                feedback3.A18 = GetCellValue(spreadsheetDocument, c);
                                feedback3.A18 = feedback3.A18.Replace(" ", "").Trim().ToUpper();
                                // Feedback given by (department name)
                                c = GetCell(workSheet, excelColumnMapper.PeerReviewAnyDepartmentNames[1], i);
                                feedback3.A19 = GetCellValue(spreadsheetDocument, c);

                                // Feedback given by (department name)
                                for (int k = 2; k < 18; k++)
                                {
                                    string departmentName = GetDepartmentName(excelColumnMapper.PeerReviewAnyDepartmentNames[k]);
                                    c = GetCell(workSheet, excelColumnMapper.PeerReviewAnyDepartmentNames[k], i);
                                    string departmentPerson = GetCellValue(spreadsheetDocument, c);
                                    if (!string.IsNullOrEmpty(departmentPerson))
                                    {
                                        feedback3.A20 = departmentName;
                                        feedback3.A23 = departmentPerson;
                                        break;
                                    }
                                }

                                feedback3.UserType = "PeerAnyDepartment";
                                feedback3.UserId = padUserId;
                                feedback3.CollegeCode = "105";
                                if (!(string.IsNullOrEmpty(feedback3.A1) && string.IsNullOrEmpty(feedback3.A2) && string.IsNullOrEmpty(feedback3.A3)))
                                    dbop.InsertFeedback(feedback3);
                                // TODO : Anil - Add this to list & at the end of loop, insert the list in database - CommonFeedbackTable.
                                // Check it should be unique
                                #endregion
                                break;
                        }

                        searchType = string.Empty;
                    }
                    // TODO : Anil - Add the list to database
                }
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Success" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string SaveFile()
        {
            var time = DateTime.Parse(DateTime.Now.ToString());

            var clientZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(time, clientZone).ToString("MM/dd/yyyy HH:mm:ss");
            string targetFolder = HttpContext.Current.Server.MapPath("~/uploads");
            string targetPath = string.Empty;

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var postedFile = HttpContext.Current.Request.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName).Replace(" ", "").Replace("(", "").Replace(")", "");
                        string extension = Path.GetExtension(postedFile.FileName);

                        targetPath = Path.Combine(targetFolder, fileName + "_" + utcTime.ToString().Replace(":", "").Replace("/", "").Replace(" ", "-") + extension);
                        postedFile.SaveAs(targetPath);
                    }
                }
            }

            return targetPath;
        }
        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string finalValue = string.Empty;
            if (cell == null || cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                finalValue = stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                finalValue = value;
            }

            //finalValue = string.IsNullOrEmpty(finalValue) ? "Good" : finalValue;

            return finalValue.Replace("'", "").Replace("1=", "").Replace("2=", "").Replace("3=", "").Replace("4=", "").Replace("5=", "").Trim();

        }

        private string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }

        public static int? GetColumnIndexFromName(string columnName)
        {
            int? columnIndex = null;

            string[] colLetters = Regex.Split(columnName, "([A-Z]+)");
            colLetters = colLetters.Where(s => !string.IsNullOrEmpty(s)).ToArray();

            if (colLetters.Count() <= 2)
            {
                int index = 0;
                foreach (string col in colLetters)
                {
                    List<char> col1 = colLetters.ElementAt(index).ToCharArray().ToList();
                    int? indexValue = col.IndexOf(col1.ElementAt(index));

                    if (indexValue != -1)
                    {
                        // The first letter of a two digit column needs some extra calculations
                        if (index == 0 && colLetters.Count() == 2)
                        {
                            columnIndex = columnIndex == null ? (indexValue + 1) * 26 : columnIndex + ((indexValue + 1) * 26);
                        }
                        else
                        {
                            columnIndex = columnIndex == null ? indexValue : columnIndex + indexValue;
                        }
                    }

                    index++;
                }
            }

            return columnIndex;
        }

        private static Cell GetCell(Worksheet worksheet, string columnName, int rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null) return null;

            var FirstRow = row.Elements<Cell>().Where(c => string.Compare
            (c.CellReference.Value, columnName +
            rowIndex, true) == 0).FirstOrDefault();

            if (FirstRow == null) return null;

            return FirstRow;
        }

        private static Row GetRow(Worksheet worksheet, int rowIndex)
        {
            Row row = worksheet.GetFirstChild<SheetData>().
            Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            if (row == null)
            {
                throw new ArgumentException(String.Format("No row with index {0} found in spreadsheet", rowIndex));
            }
            return row;
        }

        private string GetDepartmentName(string columnName)
        {
            switch (columnName)
            {
                case "AM":
                case "BU":
                case "DC":
                    return "English";
                case "AN":
                case "BV":
                case "DD":
                    return "Gujarati";
                case "AO":
                case "BW":
                case "DE":
                    return "Hindi";
                case "AP":
                case "BX":
                case "DF":
                    return "Marathi";
                case "AQ":
                case "BY":
                case "DG":
                    return "Economics";
                case "AR":
                case "BZ":
                case "DH":
                    return "History";
                case "AS":
                case "CA":
                case "DI":
                    return "Psychology";
                case "AT":
                case "CB":
                case "DJ":
                    return "Sociology";
                case "AU":
                case "CC":
                case "DK":
                    return "Child Care";
                case "AV":
                case "CD":
                case "DL":
                    return "Music";
                case "AW":
                case "CE":
                case "DM":
                    return "Accountancy & Statistics";
                case "AX":
                case "CF":
                case "DN":
                    return "Commerce & Business Law";
                case "AY":
                case "CG":
                case "DO":
                    return "BMS";
                case "AZ":
                case "CH":
                case "DP":
                    return "BCA";
                case "BA":
                case "CI":
                case "DQ":
                    return "BMM";
                case "BB":
                case "CJ":
                case "DR":
                    return "BAFI";
            }

            return "";

        }

    }
}
