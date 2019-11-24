using Newtonsoft.Json;
using QuarterlyReports.Helpers;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.IO.Compression;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Spire.Xls;

namespace QuarterlyReports.Controllers
{
    public class FeedbackController : ApiController
    {
        DatabaseOperations op;
        public FeedbackController()
        {
            op = new DatabaseOperations();
        }

        [HttpGet]
        public HttpResponseMessage test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "It Works");
        }
        [HttpGet]
        public void ConvertExcelToHTML()
        {
            //string filePath = @"D:\VancoTechnologies\Code\StudentFeedback\Code\QuarterlyReports\UserData\ReportData\105\reports\01022019_191126\TCode1_01022019_191127.xlsx";
            //DocumentFormat.OpenXml.Opene
        }

        [HttpGet]
        public HttpResponseMessage GetFeedbackSummary(string id, string type)
        {
            string fileName = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                fileName = id + "_" + type + "_Questions.json";
            }

            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + fileName);

            string allText = System.IO.File.ReadAllText(filePath);
            List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
            Dictionary<string, Dictionary<string, string>> tables = null;
            switch (type.ToLower())
            {
                case "parent":
                    tables = op.GetSummary(id, type, response.ElementAt(1).OptionValues as List<string>);
                    break;
                case "student":
                    tables = op.GetSummary(id, type, response.ElementAt(1).OptionValues as List<string>);
                    break;
                //case "alumni":
                //    break;
            }

            return Request.CreateResponse(HttpStatusCode.OK, tables);
        }

        [HttpGet]
        public HttpResponseMessage GetFilledReport(string collegeCode, string type)
        {
            string parentQuery = string.Format("SELECT c.userid, u.lastname UserName,u.course Specialization,a1,a2,a3 Year FROM commonfeedback c inner join tbluser u on c.userid = u.userid where c.collegecode={0} and u.course!='SVT' and usertype='{1}' and c.a3 like '%-%'  order by UserName asc", collegeCode, type);
            string studentQuery = string.Format("SELECT c.userid, u.lastname UserName,u.course Specialization,a1,a20 Year FROM commonfeedback c inner join tbluser u on c.userid = u.userid where c.collegecode={0} and usertype='{1}' and c.a1!='' and c.a20 like '%-%' order by UserName asc", collegeCode, type);
            DataSet result = null;
            if (type.ToLower() == "student")
            {
                result = op.GetQuery(studentQuery);
            }
            else if (type.ToLower() == "parent")
            {
                result = op.GetQuery(parentQuery);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public HttpResponseMessage GetAnalysisReport(string collegeCode, string type, string year)
        {
            DataSet result = null;

            int[] svtStudentQuestionIds = { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
            int[] svtParentQuestionIds = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int[] svtAlumniQuestionIds = { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

            int[] ids = type.ToLower() == "student" ? svtStudentQuestionIds : type.ToLower() == "parent" ? svtParentQuestionIds : svtAlumniQuestionIds;
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collegeCode + "_" + type + "_Questions.json");
            string allText = System.IO.File.ReadAllText(filePath);
            List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
            List<Analysis> analysis = new List<Analysis>();
            for (int i = 0; i < ids.Length; i++)
            {
                Analysis a = new Analysis();

                int qid = ids[i] > 20 ? ids[i] - 2 : ids[i];

                a.Question = response[qid].Question;
                int points = 0;
                for (int j = 1; j < 6; j++)
                {
                    string query = string.Empty;

                    if (type.ToLower() == "student")
                    {
                        query = string.Format("SELECT count(a" + ids[i] + ") FROM commonfeedback c where collegecode={0} and usertype='{1}' and a1 <> '' and a" + ids[i] + "=" + j + " and a3 = '1 - F.Y.'", collegeCode, type);
                    }
                    else if (type.ToLower() == "parent")
                    {
                        query = string.Format("SELECT count(a" + ids[i] + ") FROM commonfeedback c where collegecode={0} and usertype='{1}' and a1 <> '' and a" + ids[i] + "=" + j + " and a3='{2}'", collegeCode, type, year);
                    }
                    else
                    {
                        query = string.Format("SELECT count(a" + ids[i] + ") FROM commonfeedback c where collegecode={0} and usertype='{1}' and a1 <> '' and a" + ids[i] + "=" + j + " and a2='{2}'", collegeCode, type, year);
                    }

                    result = op.GetQuery(query);
                    switch (j)
                    {
                        case 1:
                            a.Excellent = result.Tables[0].Rows[0].ItemArray[0].ToString();
                            points += int.Parse(a.Excellent) * 5;
                            break;
                        case 2:
                            a.VeryGood = result.Tables[0].Rows[0].ItemArray[0].ToString();
                            points += int.Parse(a.VeryGood) * 4;
                            break;
                        case 3:
                            a.Good = result.Tables[0].Rows[0].ItemArray[0].ToString();
                            points += int.Parse(a.Good) * 3;
                            break;
                        case 4:
                            a.Average = result.Tables[0].Rows[0].ItemArray[0].ToString();
                            points += int.Parse(a.Average) * 2;
                            break;
                        case 5:
                            a.BelowAverage = result.Tables[0].Rows[0].ItemArray[0].ToString();
                            points += int.Parse(a.BelowAverage) * 1;
                            break;
                    }
                }
                a.TotalPoints = points.ToString();
                a.Total = (int.Parse(a.Excellent) + int.Parse(a.VeryGood) + int.Parse(a.Good) + int.Parse(a.Average) + int.Parse(a.BelowAverage)).ToString();
                if (a.Total != "0")
                {
                    a.Grade = Math.Round(decimal.Parse((points / decimal.Parse(a.Total)).ToString()), 2).ToString();
                    analysis.Add(a);
                }


            }
            return Request.CreateResponse(HttpStatusCode.OK, analysis);
        }

        [HttpGet]
        public HttpResponseMessage GetPendingReport(string collegeCode, string type)
        {
            string parentQuery = string.Format("select course Specialization,lastname UserName from tbluser where roletype='Parent'  and lastname != 'Parent' and userid not in (SELECT distinct userid FROM commonfeedback c where collegecode={0} and usertype='{1}' and a3 like '%-%')  order by UserName asc", collegeCode, type);
            string studentQuery = string.Format("select course Specialization,lastname UserName from tbluser where course != 'NA' and roletype='Student' and userid not in (SELECT distinct userid FROM commonfeedback c where collegecode={0} and usertype='{1}' and a20 like '%-%')  order by UserName asc", collegeCode, type);
            DataSet result = null;
            if (type.ToLower() == "student")
            {
                result = op.GetQuery(studentQuery);
            }
            else if (type.ToLower() == "parent")
            {
                result = op.GetQuery(parentQuery);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage AddTeacherFeedbacks(TeacherFeedback model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string json = JsonConvert.SerializeObject(model);
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"];
                string fileName = folderPath + typeof(TeacherFeedback).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";
                //write string to file
                System.IO.File.WriteAllText(fileName, json);

                if (model.A21 != null && model.A21.Count > 0)
                {
                    model.A_21 = string.Join("|", model.A21);
                    string replaced = model.A_21.Replace("'", "~~~");
                    model.A_21 = replaced;
                }
                if (model.A22 != null && model.A22.Count > 0)
                {
                    model.A_22 = string.Join("|", model.A22);
                    string replaced = model.A_21.Replace("'", "~~~");
                    model.A_22 = replaced;
                }

                model.IPAddress = HttpContext.Current.Request.UserHostAddress;
                model.FwdIPAddresses = HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];

                bool isInserted = op.InsertTeacherFeedback(model) > 0;
                if (!isInserted)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for GuestLectureTalk");

                return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Login(User user)
        {
            LoginResponse response = new LoginResponse();

            if (user == null || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.MobileNumber))
            {
                response.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }

            response = op.GetUserDetails(user);
            if (response == null)
            {
                response = new LoginResponse();
                response.Success = false;
                return Request.CreateResponse(HttpStatusCode.Unauthorized, response);
            }
            else
            {
                response.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetTeacherDetails()
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers.json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<TeacherDetails> response = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetTeacherDetails(string id)
        {
            try
            {
                string fileName = "teachers.json";
                if (!string.IsNullOrEmpty(id))
                {
                    fileName = "teachers_" + id + ".json";
                }

                string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + fileName;
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "File not found");
                }
                string allText = System.IO.File.ReadAllText(filePath);
                List<TeacherDetails> response = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public HttpResponseMessage SaveTeacherDetails([FromUri] string collegeCode, [FromBody] List<TeacherDetails> teacherList)
        {
            try
            {
                if (collegeCode == null || teacherList == null || teacherList.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + "teachers_" + collegeCode + ".json";

                //string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_"+collegeCode+".json");
                if (File.Exists(filePath))
                {
                    string archivePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + "teachers_" + collegeCode + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";
                    System.IO.File.Move(filePath, archivePath);
                }
                System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(teacherList));
                return Request.CreateResponse(HttpStatusCode.OK, "Records added successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetFeedbackDetails(string CollegeCode = null, string userType = null)
        {
            try
            {
                if (string.IsNullOrEmpty(CollegeCode) || string.IsNullOrEmpty(userType))
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/FeedbackQuestions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_" + userType + "_Questions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public HttpResponseMessage AddExitForm(ExitForm model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string json = JsonConvert.SerializeObject(model);
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"];
                string fileName = folderPath + typeof(ExitForm).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";
                //write string to file
                System.IO.File.WriteAllText(fileName, json);

                model.IPAddress = HttpContext.Current.Request.UserHostAddress;
                model.FwdIPAddresses = HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];

                bool isInserted = op.InsertExitForm(model) > 0;
                if (!isInserted)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data");

                return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public HttpResponseMessage ExportFeedbackDetails(string collageCode, DateTime fromDate, DateTime toDate)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(op.ExportFeedbackFormDetails(collageCode, fromDate, toDate));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
            return result;
        }

        [HttpGet()]
        public HttpResponseMessage ExportExiFormDetails(string collageCode, DateTime fromDate, DateTime toDate)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(op.ExportExitFormDetails(collageCode, fromDate, toDate));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
            return result;
        }

        [HttpPost]
        public HttpResponseMessage UploadUsers()
        {
            try
            {
                string filePath = string.Empty;
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    foreach (string file in HttpContext.Current.Request.Files)
                    {
                        var postedFile = HttpContext.Current.Request.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"];
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            filePath = path + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(filePath);

                            //Execute a loop over the rows.
                            List<LoginResponse> users = System.IO.File.ReadAllLines(filePath)
                                                  .Skip(1)
                                                  .Select(v => LoginResponse.FromCsv(v))
                                                  .ToList();
                            if (users != null && users.Count > 0)
                            {
                                foreach (LoginResponse response in users)
                                {
                                    if (string.IsNullOrEmpty(response.UserId))
                                    {
                                        bool isUserSaved = op.InsertUserDeatils(response) > 0;
                                        if (!isUserSaved)
                                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data");
                                    }
                                    else
                                    {
                                        bool isUserUpdated = op.UpdateUserDeatils(response) > 0;
                                        if (!isUserUpdated)
                                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data");
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage DownloadUsers(string collageCode)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(op.ExportUsers(collageCode));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Users.csv" };
            return result;
        }

        [HttpGet]
        public HttpResponseMessage ExportGivenFeedback(string collageCode)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(op.ExportStudenTeacherFeedback(collageCode));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Users.csv" };
            return result;
        }

        [HttpGet]
        public HttpResponseMessage GetAllCourse(string CollegeCode)
        {
            string startupPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UserData/Course_" + CollegeCode + ".json");
            List<CourseData> items = new List<CourseData>();
            using (StreamReader r = new StreamReader(startupPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<CourseData>>(json);
            }
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
        [HttpGet]
        public HttpResponseMessage GetDistinctSubjectCOde(string CollegeCode)
        {
            string startupPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UserData/teachers_" + CollegeCode + ".json");
            List<TeacherDetails> items = new List<TeacherDetails>();
            using (StreamReader r = new StreamReader(startupPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<TeacherDetails>>(json);
            }

            var distinctFYBrands = (from y in items
                                    select new
                                    {
                                        y.SubjectCode,
                                        y.SubjectName
                                    }).Distinct().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, distinctFYBrands);
        }

        [HttpGet]
        public HttpResponseMessage ExportNotInFeedbackUser(string Coursename)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            string JsonString = System.Web.Hosting.HostingEnvironment.MapPath("~/UserData/SubjectCount.json");
            List<SubjectWithCount> items = new List<SubjectWithCount>();
            using (StreamReader r = new StreamReader(JsonString))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<SubjectWithCount>>(json);
            }
            string count = items.FirstOrDefault(x => x.SubjectName == Coursename).Count;


            writer.Write(op.ExportNotInFeedbackUser(Coursename, Convert.ToInt32(count)));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = Coursename + "_MissingFeedbackUsers.csv" };
            return result;
        }

        [HttpGet]
        public HttpResponseMessage ExportNotInFeedbackUserFinalYear(string Coursename)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(op.ExportNotInFeedbackFinalYearUser(Coursename));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = Coursename + "_MissingFeedbackFinalYearUsers.csv" };
            return result;
        }

        [HttpGet]
        public HttpResponseMessage SelectAllCourseFromJson()
        {
            string JsonString = System.Web.Hosting.HostingEnvironment.MapPath("~/UserData/SubjectCount.json");
            List<SubjectWithCount> items = new List<SubjectWithCount>();
            using (StreamReader r = new StreamReader(JsonString))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<SubjectWithCount>>(json);
            }
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpGet]
        public HttpResponseMessage GetAnonymousMenuList(string id)
        {
            try
            {
                string fileName = string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    fileName = "a" + id + ".json";
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid request" });
                }

                string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + fileName;
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "File not found" });
                }
                string allText = System.IO.File.ReadAllText(filePath);
                AnonymousMenuList response = JsonConvert.DeserializeObject<AnonymousMenuList>(allText);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetSecureMenuList(string id)
        {
            try
            {
                string fileName = string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    fileName = "s" + id + ".json";
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid request" });
                }

                string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"] + fileName;
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "File not found" });
                }
                string allText = System.IO.File.ReadAllText(filePath);
                SecureMenuList response = JsonConvert.DeserializeObject<SecureMenuList>(allText);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public HttpResponseMessage ExportMissingStudentFeedback(string collageCode)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(op.GetMissingStudentFeedbackData(collageCode));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "MissingFeedbackUsers.csv" };
            return result;
        }

        [HttpGet]
        public HttpResponseMessage GetMissingStudentFeedbackList(string collageCode, string course, string sem)
        {
            try
            {
                List<User> users = op.GetMissingStudentFeedbackData(collageCode, course, sem);
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetMissingSFeedbackTeacherWise(string collageCode, string course, string sem)
        {
            try
            {
                List<User> users = op.GetMissingFeedbackTeacherWise(collageCode, course, sem);
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage ExportTeacherReportCard(string teacherCode, string collageCode, string CurrentSemester, string year)
        {
            try
            {
                FileStream fileStream = null;
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();
                bool isTrue = false;
                System.Data.DataTable[] dtArray = null;

                if (collageCode == "101" || collageCode == "103")
                    dtArray = op.TeacherReportCard(teacherCode, collageCode, CurrentSemester);
                else
                    dtArray = op.TeacherReportCardMDShah(teacherCode, collageCode, CurrentSemester, year);

                if (dtArray != null && dtArray.Length > 1 && dtArray[1].Rows.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No Data Found");
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();


                if (collageCode == "101")
                {
                    if (CurrentSemester.ToLower().Equals("odd"))
                        dic.Add("acedemicYear", "2018-19");
                    else
                        dic.Add("acedemicYear", "2017-18");
                }
                else
                    dic.Add("acedemicYear", "2018-19");

                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_" + collageCode + ".json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<TeacherDetails> teacherList = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                string teacherName = teacherList.FirstOrDefault(t => t.TeacherCode == teacherCode).TeacherName;
                teacherName = teacherCode + "-" + teacherName;
                dic.Add("teacherCode", teacherName);
                fileName = teacherName + "_" + collageCode + "_" + CurrentSemester + "_" + DateTime.Now.Ticks + ".xlsx";
                fullPath = folderPath + fileName;
                OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();
                isTrue = helper.ExportDataSet(dtArray, fullPath, dic, collageCode);
                if (isTrue)
                {
                    fileStream = File.Open(fullPath, FileMode.Open);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                    response.Content.Headers.ContentLength = fileStream.Length;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel file generation");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage ExportTeacherReportCard2(string teacherCode, string collageCode, string CurrentSemester, string year)
        {
            try
            {
                FileStream fileStream = null;
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();
                bool isTrue = false;
                ReportCard rc = null;

                if (collageCode == "101" || collageCode == "103")
                    rc = op.TeacherReportCard2(teacherCode, collageCode, CurrentSemester, year);
                else
                    rc = op.TeacherReportCard2MDShah(teacherCode, collageCode, CurrentSemester, year);

                if (rc == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No Data Found");
                }

                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_" + collageCode + ".json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<TeacherDetails> teacherList = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                string teacherName = teacherList.FirstOrDefault(t => t.TeacherCode == teacherCode).TeacherName;
                teacherName = teacherCode + "-" + teacherName;

                rc.TeacherCode = teacherName;
                if (collageCode == "101")
                {
                    if (CurrentSemester.ToLower().Equals("odd"))
                        rc.AcedemicYear = "2018-19";
                    else
                        rc.AcedemicYear = "2017-18";
                }
                else
                    rc.AcedemicYear = "2018-19";

                fileName = teacherName + "_" + collageCode + "_" + CurrentSemester + "_" + DateTime.Now.Ticks + ".xlsx";
                fullPath = folderPath + fileName;

                if (collageCode == "101" || collageCode == "103")
                    isTrue = SpreadSheetLightHelper.CreateSpreadSheetData(folderPath, fileName, rc);
                else
                    isTrue = SpreadSheetLightHelper.CreateSpreadSheetData2(folderPath, fileName, rc);

                if (isTrue)
                {
                    fileStream = File.Open(fullPath, FileMode.Open);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                    response.Content.Headers.ContentLength = fileStream.Length;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel file generation");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllReports1InPDF(string teacherCode, string collageCode, string CurrentSemester, string year)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;

                HttpResponseMessage response = new HttpResponseMessage();

                string currentTimeStamp = DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");

                List<string> adminNames = teacherCode.Split(',').ToList();
                OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();

                foreach (string an in adminNames)
                {
                    if (string.IsNullOrEmpty(an))
                        continue;

                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_" + collageCode + ".json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<TeacherDetails> teacherList = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                    string teacherName = teacherList.FirstOrDefault(t => t.TeacherCode == an).TeacherName;
                    teacherName = an + "-" + teacherName;


                    try
                    {
                        System.Data.DataTable[] dtArray = op.TeacherReportCardMDShah(an, collageCode, CurrentSemester, year);
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("adminName", teacherName);
                        dic.Add("acedemicYear", year);
                        GeneratePDFReport(collageCode, dic, dtArray, currentTimeStamp);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                string zipFolderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string zipFilePath = "StudentFeedback_PDF_Report1_" + currentTimeStamp + ".zip";
                ZipFile.CreateFromDirectory(zipFolderPath + currentTimeStamp, zipFolderPath + "\\" + zipFilePath);

                FileStream fs = File.Open(zipFolderPath + "\\" + zipFilePath, FileMode.Open);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fs);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = zipFilePath;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                response.Content.Headers.ContentLength = fs.Length;

                return response;
            }
            catch (Exception ex) { throw ex; }
        }

        private string PopulateValuesinHTMLReport(string collegeCode, string html, Dictionary<string, string> dic, DataTable[] table)
        {
            html = html.Replace("{0}", dic["adminName"]);
            html = html.Replace("{1}", ConfigurationManager.AppSettings[collegeCode]);
            html = html.Replace("{2}", dic["acedemicYear"]);
            html = html.Replace("{3}", dic["adminName"]);

            html = html.Replace("{4}", table[1].Rows[0].ItemArray[2].ToString());
            html = html.Replace("{5}", table[1].Rows[1].ItemArray[2].ToString());
            html = html.Replace("{6}", table[1].Rows[2].ItemArray[2].ToString());
            html = html.Replace("{7}", table[1].Rows[3].ItemArray[2].ToString());
            html = html.Replace("{8}", table[1].Rows[4].ItemArray[2].ToString());
            html = html.Replace("{9}", table[1].Rows[5].ItemArray[2].ToString());
            html = html.Replace("{10}", table[1].Rows[6].ItemArray[2].ToString());
            html = html.Replace("{11}", table[1].Rows[7].ItemArray[2].ToString());
            html = html.Replace("{12}", table[1].Rows[8].ItemArray[2].ToString());
            html = html.Replace("{13}", table[1].Rows[9].ItemArray[2].ToString());
            html = html.Replace("{14}", table[1].Rows[10].ItemArray[2].ToString());
            html = html.Replace("{15}", table[1].Rows[11].ItemArray[2].ToString());
            html = html.Replace("{16}", table[1].Rows[12].ItemArray[2].ToString());
            html = html.Replace("{17}", table[1].Rows[13].ItemArray[2].ToString());
            html = html.Replace("{18}", table[1].Rows[14].ItemArray[2].ToString());
            html = html.Replace("{19}", table[1].Rows[15].ItemArray[2].ToString());

            // Grad Mean
            html = html.Replace("{20}", table[1].Rows[17].ItemArray[2].ToString());
            html = html.Replace("{21}", table[1].Rows[22].ItemArray[2].ToString());
            html = html.Replace("{22}", table[1].Rows[22].ItemArray[3].ToString());
            html = html.Replace("{23}", table[1].Rows[23].ItemArray[2].ToString());
            html = html.Replace("{24}", table[1].Rows[23].ItemArray[3].ToString());
            html = html.Replace("{25}", table[1].Rows[24].ItemArray[2].ToString());
            html = html.Replace("{26}", table[1].Rows[24].ItemArray[3].ToString());
            html = html.Replace("{27}", table[1].Rows[25].ItemArray[2].ToString());
            html = html.Replace("{28}", table[1].Rows[25].ItemArray[3].ToString());
            return html;
        }

        private void GeneratePDFReport(string collegeCode, Dictionary<string, string> dic, DataTable[] table, string folderName)
        {
            Byte[] bytes;

            using (var ms = new MemoryStream())
            {

                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/templates/Teachers.html"));
                        html = PopulateValuesinHTMLReport(collegeCode, html, dic, table);
                        var css = @".center{text-align: center;}.left{text-align: left;}tr{border-bottom: 1px solid #000;}td{padding:3px;text-align:left;}";

                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }

                        doc.Close();
                    }
                }

                bytes = ms.ToArray();
            }

            string path = Path.Combine(ConfigurationManager.AppSettings["NNWCFeedbackFolderPath"], "ReportData\\" + collegeCode + "\\reports\\" + folderName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string name = dic["adminName"].Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("\\", "").Replace("/", "");
            var testFile = path + "\\" + name + ".pdf";
            System.IO.File.WriteAllBytes(testFile, bytes);
        }

        public HttpResponseMessage GetAllReports2InPDF(string teacherCode, string collageCode, string CurrentSemester, string year)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string fileName = string.Empty;

                HttpResponseMessage response = new HttpResponseMessage();

                string currentTimeStamp = DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");

                List<string> teachers = teacherCode.Split(',').ToList();
                OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();

                if (!Directory.Exists(Path.Combine(folderPath, currentTimeStamp)))
                {
                    Directory.CreateDirectory(Path.Combine(folderPath, currentTimeStamp));
                }

                foreach (string teacher in teachers)
                {
                    if (string.IsNullOrEmpty(teacher))
                        continue;

                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_" + collageCode + ".json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<TeacherDetails> teacherList = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                    string teacherName = teacherList.FirstOrDefault(t => t.TeacherCode == teacher).TeacherName;
                    teacherName = teacher + "_" + teacherName;
                    ReportCard rc = op.TeacherReportCard2MDShah(teacher, collageCode, CurrentSemester, year);

                    if (rc == null)
                        continue;

                    rc.TeacherName = teacherName;
                    rc.AcedemicYear = year;
                    string name = teacher.Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("\\", "").Replace("/", "");

                    fileName = collageCode + "_" + teacherName + "_" + currentTimeStamp + ".xlsx";

                    SpreadSheetLightHelper.CreateSpreadSheetData2(Path.Combine(folderPath, currentTimeStamp), fileName, rc);

                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(Path.Combine(folderPath, currentTimeStamp) + "\\" + fileName, ExcelVersion.Version2010);
                    Worksheet sheet = workbook.Worksheets[0];
                    System.Drawing.Image[] imgs = workbook.SaveChartAsImage(sheet);
                    List<string> imageNames = new List<string>();
                    for (int i = 0; i < imgs.Length; i++)
                    {
                        string imgName = string.Format("img-{0}.png", i);
                        imgs[i].Save(Path.Combine(folderPath, currentTimeStamp) + "\\" + imgName, System.Drawing.Imaging.ImageFormat.Png);
                        imageNames.Add(folderPath + "\\" + currentTimeStamp + "\\" + imgName);
                    }

                    GeneratePDFReport2(collageCode, rc, imageNames, currentTimeStamp, name);

                    string[] paths = Directory.GetFiles(Path.Combine(folderPath, currentTimeStamp), "*.png");
                    foreach (string path in paths)
                    {
                        File.Delete(path);
                    }

                    File.Delete(Path.Combine(folderPath, currentTimeStamp) + "\\" + fileName);
                }

                string zipFolderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string zipFilePath = "StudentFeedback_PDF_Report2_" + currentTimeStamp + ".zip";
                ZipFile.CreateFromDirectory(zipFolderPath + currentTimeStamp, zipFolderPath + "\\" + zipFilePath);

                FileStream fs = File.Open(zipFolderPath + "\\" + zipFilePath, FileMode.Open);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fs);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = zipFilePath;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                response.Content.Headers.ContentLength = fs.Length;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GeneratePDFReport2(string collegeCode, ReportCard rc, List<string> imagePaths, string folderName, string name)
        {
            Byte[] bytes;

            using (var ms = new MemoryStream())
            {

                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/templates/Teachers2.html"));
                        html = PopulateValuesinHTMLReport2(collegeCode, html, rc, imagePaths, name);
                        var css = @".center{text-align: center;}.left{text-align: left;}tr{border-bottom: 1px solid #000;}td{padding:3px;text-align:left;}";

                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }

                        doc.Close();
                    }
                }

                bytes = ms.ToArray();
            }

            string path = Path.Combine(ConfigurationManager.AppSettings["NNWCFeedbackFolderPath"], "ReportData\\" + collegeCode + "\\reports\\" + folderName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //string name = rc.TeacherName.Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("\\", "").Replace("/", "");
            var testFile = path + "\\" + name + ".pdf";
            System.IO.File.WriteAllBytes(testFile, bytes);
        }

        /*private string PopulateValuesinHTMLReport(string collegeCode, string html, Dictionary<string, string> dic, DataTable[] table)
        {
            html = html.Replace("{0}", dic["adminName"]);
            html = html.Replace("{1}", ConfigurationManager.AppSettings[collegeCode]);
            html = html.Replace("{2}", dic["acedemicYear"]);
            html = html.Replace("{3}", dic["adminName"]);

            html = html.Replace("{4}", table[1].Rows[0].ItemArray[2].ToString());
            html = html.Replace("{5}", table[1].Rows[1].ItemArray[2].ToString());
            html = html.Replace("{6}", table[1].Rows[2].ItemArray[2].ToString());
            html = html.Replace("{7}", table[1].Rows[3].ItemArray[2].ToString());
            html = html.Replace("{8}", table[1].Rows[4].ItemArray[2].ToString());
            html = html.Replace("{9}", table[1].Rows[5].ItemArray[2].ToString());
            html = html.Replace("{10}", table[1].Rows[6].ItemArray[2].ToString());
            html = html.Replace("{11}", table[1].Rows[7].ItemArray[2].ToString());
            html = html.Replace("{12}", table[1].Rows[8].ItemArray[2].ToString());
            html = html.Replace("{13}", table[1].Rows[9].ItemArray[2].ToString());
            html = html.Replace("{14}", table[1].Rows[10].ItemArray[2].ToString());
            html = html.Replace("{15}", table[1].Rows[11].ItemArray[2].ToString());
            html = html.Replace("{16}", table[1].Rows[12].ItemArray[2].ToString());
            html = html.Replace("{17}", table[1].Rows[13].ItemArray[2].ToString());
            html = html.Replace("{18}", table[1].Rows[14].ItemArray[2].ToString());
            html = html.Replace("{19}", table[1].Rows[15].ItemArray[2].ToString());
            html = html.Replace("{20}", table[1].Rows[16].ItemArray[2].ToString());
            html = html.Replace("{21}", table[1].Rows[17].ItemArray[2].ToString());
            html = html.Replace("{22}", table[1].Rows[18].ItemArray[2].ToString());
            html = html.Replace("{23}", table[1].Rows[19].ItemArray[2].ToString());
            html = html.Replace("{24}", table[1].Rows[20].ItemArray[2].ToString());
            html = html.Replace("{25}", table[1].Rows[21].ItemArray[2].ToString());
            html = html.Replace("{26}", table[1].Rows[22].ItemArray[2].ToString());
            html = html.Replace("{27}", table[1].Rows[23].ItemArray[2].ToString());
            html = html.Replace("{28}", table[1].Rows[25].ItemArray[2].ToString());

            html = html.Replace("{29}", table[1].Rows[30].ItemArray[2].ToString());
            html = html.Replace("{30}", table[1].Rows[30].ItemArray[3].ToString());
            html = html.Replace("{31}", table[1].Rows[31].ItemArray[2].ToString());
            html = html.Replace("{32}", table[1].Rows[31].ItemArray[3].ToString());
            html = html.Replace("{33}", table[1].Rows[32].ItemArray[2].ToString());
            html = html.Replace("{34}", table[1].Rows[32].ItemArray[3].ToString());

            return html;
        }*/

        private string PopulateValuesinHTMLReport2(string collegeCode, string html, ReportCard rc, List<string> imagePaths, string name)
        {
            html = html.Replace("{0}", name);
            html = html.Replace("{1}", rc.AcedemicYear);
            html = html.Replace("{2}", name.Replace("_", " "));
            html = html.Replace("{3}", rc.TSMaxScore.ToString());
            html = html.Replace("{4}", rc.SKMaxScore.ToString());
            html = html.Replace("{5}", rc.PIMaxScore.ToString());
            html = html.Replace("{6}", rc.OIMaxScore.ToString());

            html = html.Replace("{7}", rc.TSScoreObtain.ToString());
            html = html.Replace("{8}", rc.SKScoreObtain.ToString());
            html = html.Replace("{9}", rc.PIScoreObtain.ToString());
            html = html.Replace("{10}", rc.OIScoreObtain.ToString());

            html = html.Replace("{11}", rc.TSPerformance.ToString());
            html = html.Replace("{12}", rc.SKPerformance.ToString());
            html = html.Replace("{13}", rc.PIPerformance.ToString());
            html = html.Replace("{14}", rc.OIPerformance.ToString());

            html = html.Replace("{15}", imagePaths[0]);
            html = html.Replace("{16}", imagePaths[1]);

            html = html.Replace("{17}", rc.GrandMaxScore.ToString());
            html = html.Replace("{18}", rc.GrandMean.ToString());
            html = html.Replace("{19}", rc.GrandPercentage.ToString());

            return html;
        }

        [HttpGet]
        public HttpResponseMessage GetAllReports1InExcel(string teacherCode, string collageCode, string CurrentSemester, string year)
        {
            try
            {
                List<string> teacherCodes = teacherCode.Split(',').ToList();
                string folderName = "StudentFeedback_Excel_Report1_" + DateTime.Now.ToString("ddmmyyyy") + "_" + DateTime.Now.ToString("hhmmss");

                string fileName = string.Empty;
                string fullPath = string.Empty;

                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/" + folderName));

                string zipFilePath = folderPath + ".zip";

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                fullPath = folderPath + fileName;
                HttpResponseMessage response = new HttpResponseMessage();

                foreach (string tc in teacherCodes)
                {
                    if (string.IsNullOrEmpty(tc))
                    {
                        continue;
                    }

                    System.Data.DataTable[] dtArray = null;

                    if (collageCode == "101" || collageCode == "103")
                        dtArray = op.TeacherReportCard(tc, collageCode, CurrentSemester, year);
                    else
                        dtArray = op.TeacherReportCardMDShah(tc, collageCode, CurrentSemester, year);

                    if (dtArray != null && dtArray[1].Rows.Count <= 0)
                        continue;

                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_" + collageCode + ".json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<TeacherDetails> teacherList = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                    string teacherName = teacherList.FirstOrDefault(t => t.TeacherCode == tc).TeacherName;
                    teacherName = tc + "-" + teacherName;


                    dic.Add("teacherCode", teacherName);
                    dic.Add("acedemicYear", year);

                    fileName = collageCode + "_" + teacherName + "_" + CurrentSemester + "_" + folderName + ".xlsx";
                    fullPath = folderPath + "\\" + fileName;
                    OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();
                    helper.ExportDataSet(dtArray, fullPath, dic, collageCode);
                }

                // Generate Zip file for the given folder
                ZipFile.CreateFromDirectory(folderPath, zipFilePath);

                FileStream fileStream = File.Open(zipFilePath, FileMode.Open);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = folderName + ".zip";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                response.Content.Headers.ContentLength = fileStream.Length;

                return response;
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel files generation");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public HttpResponseMessage GetAllReports2InExcel(string teacherCode, string collageCode, string CurrentSemester, string year)
        {
            try
            {
                List<string> teacherCodes = teacherCode.Split(',').ToList();
                string folderName = "StudentFeedback_Excel_Report2_" + DateTime.Now.ToString("ddmmyyyy") + "_" + DateTime.Now.ToString("hhmmss");

                string fileName = string.Empty;
                string fullPath = string.Empty;

                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/" + folderName));

                string zipFilePath = folderPath + ".zip";

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                fullPath = folderPath + fileName;
                HttpResponseMessage response = new HttpResponseMessage();

                foreach (string tc in teacherCodes)
                {
                    if (string.IsNullOrEmpty(tc))
                    {
                        continue;
                    }

                    ReportCard rc = null;

                    if (collageCode == "101" || collageCode == "103")
                        rc = op.TeacherReportCard2(tc, collageCode, CurrentSemester);
                    else
                        rc = op.TeacherReportCard2MDShah(tc, collageCode, CurrentSemester, year);

                    if (rc == null)
                    {
                        continue;
                    }

                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/teachers_" + collageCode + ".json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<TeacherDetails> teacherList = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                    string teacherName = teacherList.FirstOrDefault(t => t.TeacherCode == tc).TeacherName;
                    teacherName = tc + "-" + teacherName;

                    rc.TeacherCode = teacherName;
                    rc.AcedemicYear = ConfigurationManager.AppSettings["AcademicYear"];

                    fileName = teacherName + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss") + ".xlsx";

                    if (collageCode == "101" || collageCode == "103")
                        SpreadSheetLightHelper.CreateMNWCSpreadSheetData2(folderPath, fileName, rc);
                    else
                        SpreadSheetLightHelper.CreateSpreadSheetData2(folderPath, fileName, rc);

                    // Generate Report Card 1 in same folder
                }

                // Generate Zip file for the given folder
                ZipFile.CreateFromDirectory(folderPath, zipFilePath);

                FileStream fileStream = File.Open(zipFilePath, FileMode.Open);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = folderName + ".zip";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                response.Content.Headers.ContentLength = fileStream.Length;

                return response;
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel files generation");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

    public class CourseData
    {
        public string Course { get; set; }
        public List<string> SubCourse { get; set; }
    }

    public class SubjectWithCount
    {
        public string SubjectName { get; set; }
        public string Count { get; set; }
        public string IsFinalYear { get; set; }
    }
}
