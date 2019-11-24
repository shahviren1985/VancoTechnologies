using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using QuarterlyReports.Helpers;
using QuarterlyReports.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class CommonFeedbackController : ApiController
    {
        CommonFeedbackDBOperations op;
        DatabaseOperations dbOp;
        public CommonFeedbackController()
        {
            op = new CommonFeedbackDBOperations();
            dbOp = new DatabaseOperations();
        }

        public bool AllPropertiesValid(object obj)
        {
            foreach (var p in obj.GetType().GetProperties())
            {
                var value = p.GetValue(obj);
                if (p.PropertyType == typeof(string))
                {
                    if (!string.IsNullOrEmpty((string)value))
                    {
                        p.SetValue(obj, value.ToString().Replace("'", "~~~").Replace(",", "|"));
                    }
                }
            }
            return true;
        }

        [HttpGet]
        public List<string> GetAcademicYearsForAdmins(string collegeCode)
        {
            DataSet years = op.GetAcademicYearsAdministrators(collegeCode);
            List<string> yearList = new List<string>();
            foreach (DataRow row in years.Tables[0].Rows)
            {
                yearList.Add(row[0].ToString());
            }

            return yearList;
        }

        [HttpGet]
        public List<string> GetDepartmentsForPeerReview(string collegeCode)
        {
            DataSet departments = op.GetDepartmentsForPeerReview(collegeCode);
            List<string> departmentList = new List<string>();
            departmentList.Add("All Departments");
            foreach (DataRow row in departments.Tables[0].Rows)
            {
                departmentList.Add(row[0].ToString());
            }

            return departmentList;
        }

        [HttpGet]
        public List<string> GetAcademicAdministrators(string collegeCode)
        {
            DataSet admins = op.GetAcademicAdministrators(collegeCode);
            List<string> peopleList = new List<string>();
            peopleList.Add("All Administrators");
            foreach (DataRow row in admins.Tables[0].Rows)
            {
                peopleList.Add(row[0].ToString());
            }

            return peopleList;
        }


        [HttpGet]
        public void BatchProcessing()
        {
            string folderPath = Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"], "BatchProcessing\\");
            string archivedPath = Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"], "ArchivedFiles\\");
            string[] directories = Directory.GetDirectories(folderPath);
            foreach (string dir in directories)
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    TeacherFeedback model = JsonConvert.DeserializeObject<TeacherFeedback>(System.IO.File.ReadAllText(file));

                    if (string.IsNullOrEmpty(model.UserType))
                    {
                        string userType = op.GetUserDetails(model.UserId, model.CollegeCode);
                        model.UserType = userType;
                    }

                    bool isInserted = op.InsertFeedback(model) > 0;
                    if (isInserted)
                    {
                        DirectoryInfo d = new DirectoryInfo(dir);
                        FileInfo f = new FileInfo(file);

                        if (!Directory.Exists(archivedPath + "\\" + d.Name))
                        {
                            Directory.CreateDirectory(archivedPath + "\\" + d.Name);
                        }

                        File.Move(file, Path.Combine(archivedPath + "\\" + d.Name + "\\", f.Name));
                    }
                }
            }
        }


        [HttpPost]
        public HttpResponseMessage AddFeedback(TeacherFeedback model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                if (model.A21 != null && model.A21.Count > 0)
                {
                    model.A_21 = string.Join("|", model.A21);
                }
                if (model.A22 != null && model.A22.Count > 0)
                {
                    model.A_22 = string.Join("|", model.A22);
                }

                AllPropertiesValid(model);

                //string userType = op.GetUserDetails(model.UserId, model.CollegeCode);
                //model.UserType = userType;
                model.IPAddress = HttpContext.Current.Request.UserHostAddress;
                model.FwdIPAddresses = HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];

                string json = JsonConvert.SerializeObject(model);
                string folderPath = Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["NNWCFeedbackFolderPath"], "BatchProcessing\\" + model.CollegeCode);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = folderPath + "\\" + model.UserType + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";
                System.IO.File.WriteAllText(fileName, json);

                /*bool isInserted = op.InsertFeedback(model) > 0;
                if (!isInserted)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data");
                */
                return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddAluminiFeedback(ClsAlumini model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                AllPropertiesValid(model);

                int cId = op.InsertClsAlumini(model);
                if (cId <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Alumini");
                }
                else
                {
                    if (model.Positions != null && model.Positions.Count > 0)
                    {
                        foreach (ClsPosition position in model.Positions)
                        {
                            AllPropertiesValid(position);

                            position.ClsAluminiId = cId;
                            bool isInserted = op.InsertClsPosition(position) > 0;
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Position");
                        }
                    }
                    if (model.Professional != null && model.Professional.Count > 0)
                    {
                        foreach (ClsProfessional professional in model.Professional)
                        {
                            AllPropertiesValid(professional);

                            professional.ClsAluminiId = cId;
                            bool isInserted = op.InsertClsProfessional(professional) > 0;
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Professional");
                        }
                    }
                    if (model.Rating != null)
                    {
                        ClsRating rating = model.Rating;
                        AllPropertiesValid(rating);

                        rating.ClsAluminiId = cId;
                        bool isInserted = op.InsertClsRating(rating) > 0;
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Rating");
                    }
                    if (model.Activities != null)
                    {
                        ClsActivities activity = model.Activities;
                        AllPropertiesValid(activity);

                        activity.ClsAluminiId = cId;
                        bool isInserted = op.InsertClsActivities(activity) > 0;
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Activity");
                    }

                }
                return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage ExportBarGraph(int CollegeCode, string ReportType)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_" + ReportType + "_Questions.json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();

                DatabaseOperations rdo = new DatabaseOperations();
                System.Data.DataSet ldata = new System.Data.DataSet();
                ldata = rdo.GetStaffFeedback(CollegeCode, ReportType);
                var ienumlist = ldata.Tables[0].AsEnumerable();
                //Get AllQuestion List
                var lstOfChart = new List<AnwerChartReport>();

                foreach (FeedbackQuestions fque in ListRadioQuestion)
                {
                    AnwerChartReport ch1 = new AnwerChartReport();
                    ch1.Title = fque.Question;
                    ch1.DicAnwers = new Dictionary<string, int>();
                    int IndexstrData = 1;
                    foreach (string strData in fque.OptionValues)
                    {
                        var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => x == Convert.ToString(IndexstrData));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                        IndexstrData++;
                    }
                    lstOfChart.Add(ch1);
                }
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string ff = folderPath + "\\" + CollegeCode + "\\" + ReportType + "\\";
                string filename = "CTemplate.xlsx";
                // Use Path class to manipulate file and directory paths.
                string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
                string destFile = System.IO.Path.Combine(ff, filename);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!System.IO.Directory.Exists(ff))
                {
                    System.IO.Directory.CreateDirectory(ff);
                }
                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);

                GraphsInExcel.GenerateChartsForQuestions(ff, filename, lstOfChart);
                FileStream fileStream = null;
                fileStream = File.Open(ff + "\\" + filename, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = filename;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                response.Content.Headers.ContentLength = fileStream.Length;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        public HttpResponseMessage GetGraphResult(int CollegeCode, string ReportType, string DepartmentName, string academicYear, string subjectName)
        {
            try
            {
                var lstOfChart = new List<GraphResult>();
                DepartmentName = HttpContext.Current.Server.UrlDecode(DepartmentName);
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_" + ReportType + "_Questions.json");
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
                }
                string allText = System.IO.File.ReadAllText(filePath);
                List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();
                DatabaseOperations rdo = new DatabaseOperations();
                System.Data.DataSet ldata = new System.Data.DataSet();
                ldata = rdo.GetStaffFeedback(CollegeCode, ReportType, DepartmentName, academicYear, subjectName);
                var ienumlist = ldata.Tables[0].AsEnumerable();
                foreach (FeedbackQuestions fque in ListRadioQuestion)
                {
                    GraphResult gResult = new GraphResult();
                    List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                    if (CollegeCode != 105)
                    {
                        int IndexstrData = 1;
                        foreach (string strData in fque.OptionValues)
                        {
                            GraphDectionary gd = new GraphDectionary();
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => x == Convert.ToString(IndexstrData));
                            gd.Key = strData;
                            gd.Value = dRows.Count();
                            gDictlst.Add(gd);
                            IndexstrData++;
                        }
                    }
                    else
                    {
                        foreach (string strData in fque.OptionValues)
                        {
                            GraphDectionary gd = new GraphDectionary();
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id).Trim()).Where(x => x == Convert.ToString(strData));
                            gd.Key = strData;
                            gd.Value = dRows.Count();
                            gDictlst.Add(gd);
                        }
                    }
                    gResult.ListOfDictionary = gDictlst;
                    gResult.Question = fque.Question;
                    lstOfChart.Add(gResult);
                }
                return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetCommonGraphResult(int CollegeCode, string ReportType, string tableName, string adminName = null, string academicYear = null)
        {
            try
            {
                var lstOfChart = new List<GraphResult>();
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_" + ReportType + "_Questions.json");
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
                }
                string allText = System.IO.File.ReadAllText(filePath);
                List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();
                DatabaseOperations rdo = new DatabaseOperations();
                System.Data.DataSet ldata = new System.Data.DataSet();
                ldata = rdo.GetFeedbackResponseByUserAndCollege(CollegeCode, ReportType, tableName, adminName, academicYear);
                var ienumlist = ldata.Tables[0].AsEnumerable();
                foreach (FeedbackQuestions fque in ListRadioQuestion)
                {
                    GraphResult gResult = new GraphResult();
                    List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                    if (CollegeCode != 105)
                    {
                        int IndexstrData = 1;
                        foreach (string strData in fque.OptionValues)
                        {
                            GraphDectionary gd = new GraphDectionary();
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => x == Convert.ToString(IndexstrData));
                            gd.Key = strData;
                            gd.Value = dRows.Count();
                            gDictlst.Add(gd);
                            IndexstrData++;
                        }
                    }
                    else
                    {
                        foreach (string strData in fque.OptionValues)
                        {
                            GraphDectionary gd = new GraphDectionary();
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id).Trim()).Where(x => x == Convert.ToString(strData));
                            gd.Key = strData;
                            gd.Value = dRows.Count();
                            gDictlst.Add(gd);
                        }
                    }
                    gResult.ListOfDictionary = gDictlst;
                    gResult.Question = fque.Question;
                    lstOfChart.Add(gResult);
                }
                return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //Code for Getting Pie Chart in Excel
        [HttpGet]
        public HttpResponseMessage ExportBarGraphUsingSpreadSheet(int CollegeCode, string ReportType, string DepartmentName)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_" + ReportType + "_Questions.json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();

                DatabaseOperations rdo = new DatabaseOperations();
                System.Data.DataSet ldata = new System.Data.DataSet();
                ldata = rdo.GetStaffFeedback(CollegeCode, ReportType, DepartmentName);
                //ldata = rdo.GetStaffFeedback(CollegeCode, ReportType);
                var ienumlist = ldata.Tables[0].AsEnumerable();
                //Get AllQuestion List
                var lstOfChart = new List<AnwerChartReport>();

                foreach (FeedbackQuestions fque in ListRadioQuestion)
                {
                    AnwerChartReport ch1 = new AnwerChartReport();
                    ch1.Title = fque.Question;
                    ch1.DicAnwers = new Dictionary<string, int>();
                    if (CollegeCode != 105)
                    {
                        int IndexstrData = 1;
                        foreach (string strData in fque.OptionValues)
                        {
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => x == Convert.ToString(IndexstrData));
                            ch1.DicAnwers.Add(strData, dRows.Count());
                            IndexstrData++;
                        }
                    }
                    else
                    {
                        foreach (string strData in fque.OptionValues)
                        {
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id).Trim()).Where(x => x == Convert.ToString(strData));
                            ch1.DicAnwers.Add(strData, dRows.Count());
                        }
                    }
                    lstOfChart.Add(ch1);
                }
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string ff = folderPath + "\\" + CollegeCode + "\\" + ReportType + "\\";
                string filename = CollegeCode + "_" + ReportType + "_" + DepartmentName + ".xlsx";
                if (string.IsNullOrEmpty(DepartmentName))
                {
                    filename = CollegeCode + "_" + ReportType + ".xlsx";
                }
                // Use Path class to manipulate file and directory paths.
                string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/CTemplate.xlsx");
                string destFile = System.IO.Path.Combine(ff, filename);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!System.IO.Directory.Exists(ff))
                {
                    System.IO.Directory.CreateDirectory(ff);
                }
                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);

                //GraphsInExcel.GenerateChartsForQuestions(ff, filename, lstOfChart);
                SpreadSheetLightHelper.CreatePieChart(ff, filename, lstOfChart, DepartmentName);
                FileStream fileStream = null;
                fileStream = File.Open(ff + "\\" + filename, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = filename;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                response.Content.Headers.ContentLength = fileStream.Length;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Download All Reports in ZIP File
        [HttpGet]
        public HttpResponseMessage DownloadAllAdminReportCard1(string adminName, string collageCode, string academicYear)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();

                string currentTimeStamp = "AcademicAdmin_Excel_Report1_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
                List<string> adminNames = adminName.Split(',').ToList();

                if (!Directory.Exists(Path.Combine(folderPath, currentTimeStamp)))
                {
                    Directory.CreateDirectory(Path.Combine(folderPath, currentTimeStamp));
                }

                foreach (string an in adminNames)
                {
                    if (string.IsNullOrEmpty(an))
                        continue;

                    OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    string name = an.Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("\\", "").Replace("/", "");
                    System.Data.DataTable[] dtArray = dbOp.AcademicAdministratorReportCard(an, collageCode, academicYear);
                    fileName = collageCode + "_" + name + "_" + currentTimeStamp + ".xlsx";
                    fullPath = Path.Combine(Path.Combine(folderPath, currentTimeStamp), fileName);

                    dic.Add("adminName", an);
                    dic.Add("acedemicYear", academicYear);

                    helper.ExportDataSet(dtArray, fullPath, dic, collageCode);
                }


                string zipFilePath = currentTimeStamp + ".zip";
                ZipFile.CreateFromDirectory(folderPath + currentTimeStamp, folderPath + "\\" + zipFilePath);

                FileStream fs = File.Open(folderPath + "\\" + zipFilePath, FileMode.Open);
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

        [HttpGet]
        public HttpResponseMessage DownloadAllAdminReportCard2(string adminName, string collageCode, string academicYear)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();

                string currentTimeStamp = "AcademicAdmin_Excel_Report2_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
                List<string> adminNames = adminName.Split(',').ToList();

                if (!Directory.Exists(Path.Combine(folderPath, currentTimeStamp)))
                {
                    Directory.CreateDirectory(Path.Combine(folderPath, currentTimeStamp));
                }

                foreach (string an in adminNames)
                {
                    if (string.IsNullOrEmpty(an))
                        continue;

                    string name = an.Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("\\", "").Replace("/", "");

                    ReportCard rc = dbOp.AcademicAdministratorReportCard2(an, collageCode, academicYear);
                    if (rc == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "No Data Found");
                    }

                    rc.TeacherName = an.Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(".", "").Replace("\\", "").Replace("/", "");
                    rc.AcedemicYear = academicYear;

                    fileName = collageCode + "_" + name + "_" + currentTimeStamp + ".xlsx";
                    fullPath = Path.Combine(Path.Combine(folderPath, currentTimeStamp), fileName);
                    SpreadSheetLightHelper.CreateSpreadSheetData(Path.Combine(folderPath, currentTimeStamp), fileName, rc);

                    /*Workbook workbook = new Workbook();
                    workbook.LoadFromFile(fullPath, ExcelVersion.Version2010);
                    Worksheet sheet = workbook.Worksheets[0];
                    System.Drawing.Image[] imgs = workbook.SaveChartAsImage(sheet);
                    for (int i = 0; i < imgs.Length; i++)
                    {
                        imgs[i].Save(folderPath + "\\" + string.Format("img-{0}.png", i), System.Drawing.Imaging.ImageFormat.Png);
                    }*/
                }


                string zipFilePath = currentTimeStamp + ".zip";
                ZipFile.CreateFromDirectory(folderPath + currentTimeStamp, folderPath + "\\" + zipFilePath);

                FileStream fs = File.Open(folderPath + "\\" + zipFilePath, FileMode.Open);
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

        [HttpGet]
        public HttpResponseMessage DownloadAllAdminReportCard1PDF(string adminName, string collageCode, string academicYear)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;

                HttpResponseMessage response = new HttpResponseMessage();

                string currentTimeStamp = "AcademicAdmin_PDF_Report1_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");

                List<string> adminNames = adminName.Split(',').ToList();
                OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();

                foreach (string an in adminNames)
                {
                    if (string.IsNullOrEmpty(an))
                        continue;

                    System.Data.DataTable[] dtArray = dbOp.AcademicAdministratorReportCard(an, collageCode, academicYear);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("adminName", an);
                    dic.Add("acedemicYear", academicYear);
                    GeneratePDFReport(collageCode, dic, dtArray, currentTimeStamp);
                }

                string zipFolderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string zipFilePath = currentTimeStamp + ".zip";
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

        [HttpGet]
        public HttpResponseMessage DownloadAllAdminReportCard2PDF(string adminName, string collageCode, string academicYear)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/" + collageCode + "/reports/"));
                string fileName = string.Empty;

                HttpResponseMessage response = new HttpResponseMessage();

                string currentTimeStamp = "AcademicAdmin_PDF_Report2_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");

                List<string> adminNames = adminName.Split(',').ToList();
                OpenXmlHelperMultipleDt helper = new OpenXmlHelperMultipleDt();

                if (!Directory.Exists(Path.Combine(folderPath, currentTimeStamp)))
                {
                    Directory.CreateDirectory(Path.Combine(folderPath, currentTimeStamp));
                }

                foreach (string an in adminNames)
                {
                    if (string.IsNullOrEmpty(an))
                        continue;

                    ReportCard rc = dbOp.AcademicAdministratorReportCard2(an, collageCode, academicYear);
                    rc.TeacherName = an;
                    rc.AcedemicYear = academicYear;
                    string name = an.Replace("=", "").Replace(":", "").Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("\\", "").Replace("/", "");

                    fileName = collageCode + "_" + name + "_" + currentTimeStamp + ".xlsx";

                    SpreadSheetLightHelper.CreateSpreadSheetData(Path.Combine(folderPath, currentTimeStamp), fileName, rc);

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
                string zipFilePath = currentTimeStamp + ".zip";
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
                        var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/templates/AcademicAdministrator.html"));
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
                        var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/templates/AcademicAdministrator2.html"));
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

            Dictionary<string, List<string>> comments = GetAcademicComments(collegeCode, dic["acedemicYear"]);

            string li = "<ul>";
            foreach (string comment in comments["Leadership Skills"].Distinct(StringComparer.InvariantCultureIgnoreCase).ToList())
            {
                li += "<li>" + comment + "</li>";
            }
            li += "</ul>";
            html = html.Replace("{35}", li);

            li = "<ul>";
            foreach (string comment in comments["Administration Skills"].Distinct(StringComparer.InvariantCultureIgnoreCase).ToList())
            {
                li += "<li>" + comment + "</li>";
            }
            li += "</ul>";
            html = html.Replace("{36}", li);

            li = "<ul>";
            foreach (string comment in comments["Faculty and Programme Development Skills"].Distinct(StringComparer.InvariantCultureIgnoreCase).ToList())
            {
                li += "<li>" + comment + "</li>";
            }
            li += "</ul>";
            html = html.Replace("{37}", li);

            li = "<ul>";
            foreach (string comment in comments["Communication and Interpersonal Skills"].Distinct(StringComparer.InvariantCultureIgnoreCase).ToList())
            {
                li += "<li>" + comment + "</li>";
            }
            li += "</ul>";
            html = html.Replace("{38}", li);

            return html;
        }

        private string PopulateValuesinHTMLReport2(string collegeCode, string html, ReportCard rc, List<string> imagePaths, string name)
        {
            html = html.Replace("{0}", name);
            html = html.Replace("{1}", rc.AcedemicYear);
            html = html.Replace("{2}", name.Replace("_", " "));
            html = html.Replace("{3}", rc.TSMaxScore.ToString());
            html = html.Replace("{4}", rc.SKMaxScore.ToString());
            html = html.Replace("{5}", rc.PIMaxScore.ToString());

            html = html.Replace("{6}", rc.TSScoreObtain.ToString());
            html = html.Replace("{7}", rc.SKScoreObtain.ToString());
            html = html.Replace("{8}", rc.PIScoreObtain.ToString());

            html = html.Replace("{9}", rc.TSPerformance.ToString());
            html = html.Replace("{10}", rc.SKPerformance.ToString());
            html = html.Replace("{11}", rc.PIPerformance.ToString());

            html = html.Replace("{13}", imagePaths[0]);
            html = html.Replace("{14}", imagePaths[1]);

            html = html.Replace("{15}", rc.GrandMaxScore.ToString());
            html = html.Replace("{16}", rc.GrandMean.ToString());
            html = html.Replace("{17}", rc.GrandPercentage.ToString());

            return html;
        }
        private Dictionary<string, List<string>> GetAcademicComments(string collegeCode, string academicYear)
        {
            Dictionary<string, List<string>> comments = new Dictionary<string, List<string>>();

            string query = string.Format("SELECT a27, a28, a29, a30 FROM academicfeedback where collegecode={0} and a32='{1}'", collegeCode, academicYear);
            DataSet table = dbOp.GetQuery(query);

            comments.Add("Leadership Skills", new List<string>());
            comments.Add("Administration Skills", new List<string>());
            comments.Add("Faculty and Programme Development Skills", new List<string>());
            comments.Add("Communication and Interpersonal Skills", new List<string>());

            foreach (DataRow row in table.Tables[0].Rows)
            {
                if (!comments["Leadership Skills"].Contains(row[0].ToString().Trim()))
                {
                    comments["Leadership Skills"].Add(row[0].ToString().Trim());
                }
                if (!comments["Administration Skills"].Contains(row[1].ToString().Trim()))
                {
                    comments["Administration Skills"].Add(row[1].ToString().Trim());
                }
                if (!comments["Faculty and Programme Development Skills"].Contains(row[2].ToString().Trim()))
                {
                    comments["Faculty and Programme Development Skills"].Add(row[2].ToString().Trim());
                }
                if (!comments["Communication and Interpersonal Skills"].Contains(row[3].ToString().Trim()))
                {
                    comments["Communication and Interpersonal Skills"].Add(row[3].ToString().Trim());
                }
            }

            return comments;
        }
        #endregion

        #region Download Individual Report
        [HttpGet]
        public HttpResponseMessage ExportAdminReportCard(string adminName, string collageCode, string academicYear)
        {
            try
            {
                FileStream fileStream = null;
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();
                bool isTrue = false;
                System.Data.DataTable[] dtArray = dbOp.AcademicAdministratorReportCard(adminName, collageCode, academicYear);

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("adminName", adminName);
                dic.Add("acedemicYear", academicYear);

                fileName = adminName + "_" + collageCode + "_" + academicYear + "_" + DateTime.Now.Ticks + ".xlsx";
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
        public HttpResponseMessage ExportAdminReportCard2(string adminName, string collageCode, string academicYear)
        {
            try
            {
                FileStream fileStream = null;
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();
                bool isTrue = false;
                ReportCard rc = dbOp.AcademicAdministratorReportCard2(adminName, collageCode, academicYear);
                if (rc == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No Data Found");
                }

                rc.TeacherName = adminName;
                rc.AcedemicYear = academicYear;

                fileName = adminName + "_" + collageCode + "_" + academicYear + "_" + DateTime.Now.Ticks + ".xlsx";
                fullPath = folderPath + fileName;
                isTrue = SpreadSheetLightHelper.CreateSpreadSheetData(folderPath, fileName, rc);

                /*Workbook workbook = new Workbook();
                workbook.LoadFromFile(fullPath, ExcelVersion.Version2010);
                Worksheet sheet = workbook.Worksheets[0];
                System.Drawing.Image[] imgs = workbook.SaveChartAsImage(sheet);
                for (int i = 0; i < imgs.Length; i++)
                {
                    imgs[i].Save(folderPath + "\\" + string.Format("img-{0}.png", i), System.Drawing.Imaging.ImageFormat.Png);
                }*/

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

        #endregion

        [HttpGet]
        public HttpResponseMessage ExportCommonBarGraphUsingSpreadSheet(int CollegeCode, string ReportType, string tableName, string adminName = null, string academicYear = null)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_" + ReportType + "_Questions.json");
                string allText = System.IO.File.ReadAllText(filePath);

                //adminName = string.IsNullOrEmpty(adminName) ? "All-Administrator" : adminName;
                List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
                List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();

                DatabaseOperations rdo = new DatabaseOperations();
                System.Data.DataSet ldata = new System.Data.DataSet();
                ldata = rdo.GetFeedbackResponseByUserAndCollege(CollegeCode, ReportType, tableName, adminName, academicYear);
                var ienumlist = ldata.Tables[0].AsEnumerable();
                //Get AllQuestion List
                var lstOfChart = new List<AnwerChartReport>();

                foreach (FeedbackQuestions fque in ListRadioQuestion)
                {
                    AnwerChartReport ch1 = new AnwerChartReport();
                    ch1.Title = fque.Question;
                    ch1.DicAnwers = new Dictionary<string, int>();
                    if (CollegeCode != 105)
                    {
                        int IndexstrData = 1;
                        foreach (string strData in fque.OptionValues)
                        {
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => x == Convert.ToString(IndexstrData));
                            ch1.DicAnwers.Add(strData, dRows.Count());
                            IndexstrData++;
                        }
                    }
                    else
                    {
                        foreach (string strData in fque.OptionValues)
                        {
                            var dRows = ienumlist.Select(s => s.Field<string>(fque.Id).Trim()).Where(x => x == Convert.ToString(strData));
                            ch1.DicAnwers.Add(strData, dRows.Count());
                        }
                    }
                    lstOfChart.Add(ch1);
                }
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string sourceFile = folderPath + "\\" + CollegeCode + "\\" + ReportType + "\\CTemplate.xlsx";

                string currentTimeStamp = DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
                string filename = CollegeCode + "_" + ReportType + "_" + adminName + "_" + currentTimeStamp + ".xlsx";
                if (string.IsNullOrEmpty(adminName))
                {
                    filename = CollegeCode + "_" + ReportType + "_" + currentTimeStamp + ".xlsx";
                }

                // Use Path class to manipulate file and directory paths.
                //string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
                string destFile = System.IO.Path.Combine(folderPath + "\\" + CollegeCode + "\\" + ReportType, filename);


                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);

                //GraphsInExcel.GenerateChartsForQuestions(ff, filename, lstOfChart);
                SpreadSheetLightHelper.CreatePieChart(folderPath + "\\" + CollegeCode + "\\" + ReportType, filename, lstOfChart, adminName);
                FileStream fileStream = null;
                fileStream = File.Open(destFile, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = filename;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                response.Content.Headers.ContentLength = fileStream.Length;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Web Graph
        [HttpGet]
        public HttpResponseMessage GetTeacherGraphResult(string teacherCode, string CurrentSemester, int CollegeCode, string year = null)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_FeedbackQuestions.json");
            string allText = System.IO.File.ReadAllText(filePath);
            List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
            List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();
            ldata = rdo.GetTeacherFeedbackByTeacher(teacherCode, CurrentSemester, CollegeCode, year);
            var ienumlist = ldata.Tables[0].AsEnumerable();

            var lstOfChart = new List<GraphResult>();
            foreach (FeedbackQuestions fque in ListRadioQuestion)
            {
                GraphResult gResult = new GraphResult();
                List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                foreach (string strData in fque.OptionValues)
                {
                    GraphDectionary gd = new GraphDectionary();
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        gd.Key = strData;
                        gd.Value = dRows2.Count();
                        gDictlst.Add(gd);
                    }
                    else
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => strData.ToLower().StartsWith(x.Trim().ToLower()));
                        gd.Key = strData;
                        gd.Value = dRows2.Count();
                        gDictlst.Add(gd);
                    }
                }
                gResult.ListOfDictionary = gDictlst;
                gResult.Question = fque.Question;
                lstOfChart.Add(gResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
        }

        //Export in Excel pie Graph
        [HttpGet]
        public HttpResponseMessage GetTeacherGraphResult_Excel(string teacherCode, string CurrentSemester, int CollegeCode)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_FeedbackQuestions.json");
            string allText = System.IO.File.ReadAllText(filePath);
            List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
            List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();
            ldata = rdo.GetTeacherFeedbackByTeacher(teacherCode, CurrentSemester, CollegeCode);
            var ienumlist = ldata.Tables[0].AsEnumerable();

            //var lstOfChart = new List<GraphResult>();
            var lstOfChart = new List<AnwerChartReport>();
            foreach (FeedbackQuestions fque in ListRadioQuestion)
            {
                AnwerChartReport gResult = new AnwerChartReport();
                List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                AnwerChartReport gd = new AnwerChartReport();
                gd.Title = fque.Question;
                gd.DicAnwers = new Dictionary<string, int>();
                foreach (string strData in fque.OptionValues)
                {
                    //GraphDectionary gd = new GraphDectionary();
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        gd.DicAnwers.Add(strData, dRows2.Count());
                    }
                    else
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => strData.ToLower().Contains(x.ToLower()));
                        gd.DicAnwers.Add(strData, dRows2.Count());
                    }
                    //lstOfChart.Add(gd);
                }
                lstOfChart.Add(gd);
            }
            string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
            string ff = folderPath + "\\" + CollegeCode + "\\Teacher\\";
            string filename = "CTemplate.xlsx";
            string destFileName = CollegeCode + "_" + teacherCode + "_" + CurrentSemester + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss") + ".xlsx";
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
            string destFile = System.IO.Path.Combine(ff, destFileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(ff))
            {
                System.IO.Directory.CreateDirectory(ff);
            }

            System.IO.File.Copy(sourceFile, destFile, true);
            SpreadSheetLightHelper.CreatePieChart_BigSize(ff, destFileName, lstOfChart);
            FileStream fileStream = null;
            fileStream = File.Open(ff + "\\" + filename, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = destFileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
            //return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
        }

        //Export in Excel bar Graph
        [HttpGet]
        public HttpResponseMessage ExportTeacherGraphResult(string teacherCode, string CurrentSemester, int CollegeCode)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_FeedbackQuestions.json");
            string allText = System.IO.File.ReadAllText(filePath);
            List<FeedbackQuestions> ListOfQuestion = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);
            List<FeedbackQuestions> ListRadioQuestion = ListOfQuestion.Where(x => x.Type.ToLower() == "radio").ToList();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();
            ldata = rdo.GetTeacherFeedbackByTeacher(teacherCode, CurrentSemester, CollegeCode);
            var ienumlist = ldata.Tables[0].AsEnumerable();

            var lstOfChart = new List<AnwerChartReport>();
            foreach (FeedbackQuestions fque in ListRadioQuestion)
            {
                AnwerChartReport ch1 = new AnwerChartReport();
                ch1.Title = fque.Question;
                ch1.DicAnwers = new Dictionary<string, int>();
                foreach (string strData in fque.OptionValues)
                {
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                    }
                    else
                    {
                        var dRows = ienumlist.Select(s => s.Field<string>(fque.Id)).Where(x => strData.ToLower().Contains(x.ToLower()));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                    }
                }
                lstOfChart.Add(ch1);
            }
            string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
            string ff = folderPath + "\\" + CollegeCode + "\\Teacher\\";
            string filename = "CTemplate.xlsx";
            string destfilename = "Teacher_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".xlsx";
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
            string destFile = System.IO.Path.Combine(ff, destfilename);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(ff))
            {
                System.IO.Directory.CreateDirectory(ff);
            }
            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
            GraphsInExcel.GenerateChartsForQuestions(ff, destfilename, lstOfChart);
            FileStream fileStream = null;
            fileStream = File.Open(ff + "\\" + destfilename, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = destfilename;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
        }

        //web graph
        public HttpResponseMessage GetAlumaniTabGraphData(int CollegeCode, string tab)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_Alumni_MCQ_AnswerColumns.json");
            string allText = System.IO.File.ReadAllText(filePath);
            RootAlumaniObject ListOfRootQuestion = JsonConvert.DeserializeObject<RootAlumaniObject>(allText);
            List<AluminiInnerQuestion> currentListOfQue = new List<AluminiInnerQuestion>();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();

            if (tab.ToLower() == "clsactivities")
            {
                currentListOfQue = ListOfRootQuestion.clsactivities;
                ldata = rdo.GetclsActivityData(CollegeCode);
            }
            else if (tab.ToLower() == "clsalumini")
            {
                currentListOfQue = ListOfRootQuestion.clsalumini;
                ldata = rdo.GetclsAlumniData(CollegeCode);
            }
            else if (tab.ToLower() == "clsrating")
            {
                currentListOfQue = ListOfRootQuestion.clsrating;
                ldata = rdo.GetclsRatingData(CollegeCode);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tab is not valid");
            }
            var ienumlist = ldata.Tables[0].AsEnumerable();
            List<AluminiInnerQuestion> ListRadioQuestion = currentListOfQue.Where(x => x.type.ToLower() == "radio").ToList();
            var lstOfChart = new List<GraphResult>();
            foreach (AluminiInnerQuestion fque in ListRadioQuestion)
            {
                GraphResult gResult = new GraphResult();
                List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                foreach (string strData in fque.optionValues)
                {
                    GraphDectionary gd = new GraphDectionary();
                    //var dRows = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => x == Convert.ToString(IndexstrData));
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower()));
                        gd.Key = strData;
                        gd.Value = dRows2.Count();
                        gDictlst.Add(gd);
                    }
                    else
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Equals(x.ToLower()));
                        gd.Key = strData;
                        gd.Value = dRows2.Count();
                        gDictlst.Add(gd);
                    }
                }
                gResult.ListOfDictionary = gDictlst;
                gResult.Question = fque.question;
                lstOfChart.Add(gResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
        }

        //Export excel pie char graph
        public HttpResponseMessage GetAlumaniTabGraphData_Excel(int CollegeCode, string tab)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_Alumni_MCQ_AnswerColumns.json");
            string allText = System.IO.File.ReadAllText(filePath);
            RootAlumaniObject ListOfRootQuestion = JsonConvert.DeserializeObject<RootAlumaniObject>(allText);
            List<AluminiInnerQuestion> currentListOfQue = new List<AluminiInnerQuestion>();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();

            if (tab.ToLower() == "clsactivities")
            {
                currentListOfQue = ListOfRootQuestion.clsactivities;
                ldata = rdo.GetclsActivityData(CollegeCode);
            }
            else if (tab.ToLower() == "clsalumini")
            {
                currentListOfQue = ListOfRootQuestion.clsalumini;
                ldata = rdo.GetclsAlumniData(CollegeCode);
            }
            else if (tab.ToLower() == "clsrating")
            {
                currentListOfQue = ListOfRootQuestion.clsrating;
                ldata = rdo.GetclsRatingData(CollegeCode);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tab is not valid");
            }
            var ienumlist = ldata.Tables[0].AsEnumerable();
            List<AluminiInnerQuestion> ListRadioQuestion = currentListOfQue.Where(x => x.type.ToLower() == "radio").ToList();
            //var lstOfChart = new List<GraphResult>();
            var lstOfChart = new List<AnwerChartReport>();
            foreach (AluminiInnerQuestion fque in ListRadioQuestion)
            {
                AnwerChartReport gResult = new AnwerChartReport();
                List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                AnwerChartReport gd = new AnwerChartReport();
                gd.Title = fque.question;
                gd.DicAnwers = new Dictionary<string, int>();
                foreach (string strData in fque.optionValues)
                {
                    //GraphDectionary gd = new GraphDectionary();
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        gd.DicAnwers.Add(strData, dRows2.Count());
                    }
                    else
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower()));
                        gd.DicAnwers.Add(strData, dRows2.Count());
                    }
                }
                lstOfChart.Add(gd);
            }
            string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
            string ff = folderPath + "\\" + CollegeCode + "\\Alumni\\";
            string filename = "CTemplate.xlsx";
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
            string destFile = System.IO.Path.Combine(ff, filename);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(ff))
            {
                System.IO.Directory.CreateDirectory(ff);
            }

            System.IO.File.Copy(sourceFile, destFile, true);
            SpreadSheetLightHelper.CreatePieChart(ff, filename, lstOfChart);
            FileStream fileStream = null;
            fileStream = File.Open(ff + "\\" + filename, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = filename;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
            //return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
        }


        [HttpGet]
        public HttpResponseMessage ExportAlumaniTabGraphData(int CollegeCode, string tab)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_Alumni_MCQ_AnswerColumns.json");
            string allText = System.IO.File.ReadAllText(filePath);
            RootAlumaniObject ListOfRootQuestion = JsonConvert.DeserializeObject<RootAlumaniObject>(allText);
            List<AluminiInnerQuestion> currentListOfQue = new List<AluminiInnerQuestion>();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();

            if (tab.ToLower() == "clsactivities")
            {
                currentListOfQue = ListOfRootQuestion.clsactivities;
                ldata = rdo.GetclsActivityData(CollegeCode);
            }
            else if (tab.ToLower() == "clsalumini")
            {
                currentListOfQue = ListOfRootQuestion.clsalumini;
                ldata = rdo.GetclsAlumniData(CollegeCode);
            }
            else if (tab.ToLower() == "clsrating")
            {
                currentListOfQue = ListOfRootQuestion.clsrating;
                ldata = rdo.GetclsRatingData(CollegeCode);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tab is not valid");
            }
            var ienumlist = ldata.Tables[0].AsEnumerable();
            List<AluminiInnerQuestion> ListRadioQuestion = currentListOfQue.Where(x => x.type.ToLower() == "radio").ToList();
            var lstOfChart = new List<AnwerChartReport>();
            foreach (AluminiInnerQuestion fque in ListRadioQuestion)
            {
                AnwerChartReport ch1 = new AnwerChartReport();
                ch1.Title = fque.question;
                ch1.DicAnwers = new Dictionary<string, int>();
                foreach (string strData in fque.optionValues)
                {
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                    }
                    else
                    {
                        var dRows = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower()));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                    }
                }
                lstOfChart.Add(ch1);
            }
            string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
            string ff = folderPath + "\\" + CollegeCode + "\\Alumini\\";
            string filename = "CTemplate.xlsx";
            string destfilename = "Alumini_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".xlsx";
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
            string destFile = System.IO.Path.Combine(ff, destfilename);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(ff))
            {
                System.IO.Directory.CreateDirectory(ff);
            }
            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);


            GraphsInExcel.GenerateChartsForQuestions(ff, destfilename, lstOfChart);
            FileStream fileStream = null;
            fileStream = File.Open(ff + "\\" + destfilename, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = destfilename;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
        }

        [HttpGet]
        public HttpResponseMessage ExportGetExitFormGraph(int CollegeCode, string tab)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_ExitForm_MCQ_AnswerColumns.json");
            string allText = System.IO.File.ReadAllText(filePath);
            var ListOfRootQuestion = JsonConvert.DeserializeObject<ExitFormObject>(allText);
            List<AluminiInnerQuestion> currentListOfQue = new List<AluminiInnerQuestion>();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();
            ldata = rdo.GetExitFormReport(CollegeCode, tab);
            if (tab.ToLower() == "a2tab")
            {
                currentListOfQue = ListOfRootQuestion.a2;
            }
            else if (tab.ToLower() == "a3tab")
            {
                currentListOfQue = ListOfRootQuestion.a9;
            }
            else if (tab.ToLower() == "exitformtab")
            {
                currentListOfQue = ListOfRootQuestion.exitform;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tab is not valid");
            }
            var ienumlist = ldata.Tables[0].AsEnumerable();
            List<AluminiInnerQuestion> ListRadioQuestion = currentListOfQue.Where(x => x.type.ToLower() == "radio").ToList();
            var lstOfChart = new List<AnwerChartReport>();
            foreach (AluminiInnerQuestion fque in ListRadioQuestion)
            {
                AnwerChartReport ch1 = new AnwerChartReport();
                ch1.Title = fque.question;
                ch1.DicAnwers = new Dictionary<string, int>();
                foreach (string strData in fque.optionValues)
                {
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                    }
                    else
                    {
                        var dRows = ienumlist.Select(s => s.Field<object>(fque.id)).Where(x => strData.ToLower().Contains(x.ToString().ToLower()));
                        ch1.DicAnwers.Add(strData, dRows.Count());
                    }
                }
                lstOfChart.Add(ch1);
            }
            string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
            string ff = folderPath + "\\" + CollegeCode + "\\Alumini\\";
            string filename = "CTemplate.xlsx";
            string destfilename = "Alumini_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".xlsx";
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + filename);
            string destFile = System.IO.Path.Combine(ff, destfilename);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(ff))
            {
                System.IO.Directory.CreateDirectory(ff);
            }
            System.IO.File.Copy(sourceFile, destFile, true);
            SpreadSheetLightHelper.CreatePieChart(ff, filename, lstOfChart);
            FileStream fileStream = null;
            fileStream = File.Open(ff + "\\" + filename, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = filename;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetExitFormGraph(int CollegeCode, string tab)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + CollegeCode + "_ExitForm_MCQ_AnswerColumns.json");
            string allText = System.IO.File.ReadAllText(filePath);
            var ListOfRootQuestion = JsonConvert.DeserializeObject<ExitFormObject>(allText);
            List<AluminiInnerQuestion> currentListOfQue = new List<AluminiInnerQuestion>();
            DatabaseOperations rdo = new DatabaseOperations();
            System.Data.DataSet ldata = new System.Data.DataSet();
            ldata = rdo.GetExitFormReport(CollegeCode, tab);
            if (tab.ToLower() == "a2tab")
            {
                currentListOfQue = ListOfRootQuestion.a2;
            }
            else if (tab.ToLower() == "a3tab")
            {
                currentListOfQue = ListOfRootQuestion.a9;
            }
            else if (tab.ToLower() == "exitformtab")
            {
                currentListOfQue = ListOfRootQuestion.exitform;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tab is not valid");
            }
            var ienumlist = ldata.Tables[0].AsEnumerable();
            List<AluminiInnerQuestion> ListRadioQuestion = currentListOfQue.Where(x => x.type.ToLower() == "radio").ToList();
            var lstOfChart = new List<GraphResult>();
            foreach (AluminiInnerQuestion fque in ListRadioQuestion)
            {
                GraphResult gResult = new GraphResult();
                List<GraphDectionary> gDictlst = new List<GraphDectionary>();
                foreach (string strData in fque.optionValues)
                {
                    GraphDectionary gd = new GraphDectionary();
                    int number;
                    bool success = Int32.TryParse(strData, out number);
                    if (success)
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<string>(fque.id)).Where(x => strData.ToLower().Contains(x.ToLower() + " -"));
                        gd.Key = strData;
                        gd.Value = dRows2.Count();
                        gDictlst.Add(gd);
                    }
                    else
                    {
                        var dRows2 = ienumlist.Select(s => s.Field<object>(fque.id)).Where(x => strData.ToLower().Contains(x.ToString().ToLower()));
                        gd.Key = strData;
                        gd.Value = dRows2.Count();
                        gDictlst.Add(gd);
                    }
                }
                gResult.ListOfDictionary = gDictlst;
                gResult.Question = fque.question;
                lstOfChart.Add(gResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, lstOfChart);
        }

        [HttpGet]
        public HttpResponseMessage InsertTestRecord()
        {
            for (int a = 1; a < 100; a++)
            {
                TeacherFeedback model = new TeacherFeedback();
                Random rnd = new Random();
                model.UserType = "Employer";
                model.UserId = a.ToString();
                model.CollegeCode = "102";

                model.A1 = Convert.ToString(rnd.Next(1, 5));
                model.A2 = Convert.ToString(rnd.Next(1, 5));
                model.A3 = Convert.ToString(rnd.Next(1, 5));
                model.A4 = Convert.ToString(rnd.Next(1, 5));
                model.A5 = Convert.ToString(rnd.Next(1, 5));
                model.A6 = Convert.ToString(rnd.Next(1, 5));
                model.A7 = Convert.ToString(rnd.Next(1, 5));
                model.A8 = Convert.ToString(rnd.Next(1, 5));
                model.A9 = Convert.ToString(rnd.Next(1, 5));
                model.A10 = Convert.ToString(rnd.Next(1, 5));
                model.A11 = Convert.ToString(rnd.Next(1, 5));
                model.A12 = Convert.ToString(rnd.Next(1, 5));
                model.A13 = Convert.ToString(rnd.Next(1, 5));
                model.A14 = Convert.ToString(rnd.Next(1, 5));
                model.A15 = Convert.ToString(rnd.Next(1, 5));
                model.A16 = Convert.ToString(rnd.Next(1, 5));
                model.A17 = Convert.ToString(rnd.Next(1, 5));
                model.A18 = Convert.ToString(rnd.Next(1, 5));
                model.A19 = Convert.ToString(rnd.Next(1, 5));
                model.A20 = Convert.ToString(rnd.Next(1, 5));
                model.CreatedDate = DateTime.Now;
                bool isInserted = op.InsertFeedback(model) > 0;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Record Inserted");
        }

        [HttpGet]
        public HttpResponseMessage GetPeerPersonList(string collegeCode)
        {
            return Request.CreateResponse(HttpStatusCode.OK, dbOp.GetPeerViewPeopleList(collegeCode));
        }

        [HttpGet]
        public HttpResponseMessage GetSubjectList(string collegeCode, string department)
        {
            return Request.CreateResponse(HttpStatusCode.OK, dbOp.GetPeerViewPeopleList(collegeCode, department));
        }

        [HttpGet]
        public HttpResponseMessage GetPeerReviewAnalysis(string collegeCode, string personName)
        {
            return Request.CreateResponse(HttpStatusCode.OK, dbOp.GetPeerReviewOwnDepartmentList(collegeCode, personName));
        }

        [HttpGet]
        public HttpResponseMessage GetPeerReviewQuestions(string collegeCode)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), collegeCode + "_PeerOwnDepartment_Questions.json");

            try
            {
                List<FeedbackQuestions> questions = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(System.IO.File.ReadAllText(path));
                return Request.CreateResponse(HttpStatusCode.OK, questions);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    class GraphResult
    {
        public List<GraphDectionary> ListOfDictionary { get; set; }
        public string Question { get; set; }
    }
    class GraphDectionary
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }

    public class AluminiInnerQuestion
    {
        public string id { get; set; }
        public string question { get; set; }
        public string type { get; set; }
        public List<string> optionValues { get; set; }
    }

    public class RootAlumaniObject
    {
        public List<AluminiInnerQuestion> clsactivities { get; set; }
        public List<AluminiInnerQuestion> clsalumini { get; set; }
        public List<AluminiInnerQuestion> clsrating { get; set; }
    }
}
