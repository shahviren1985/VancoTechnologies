using CollegeExamService.Helpers;
using CollegeExamService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace CollegeExamService.Controllers
{
    public class FileController : ApiController
    {
        ExamDbOperations db = null;
        public FileController()
        {
            db = new ExamDbOperations();
        }

        [HttpPost]
        public HttpResponseMessage Upload()
        {
            try
            {
                string folderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
                string subFolderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["subid"];
                string filePath = string.Empty;
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    foreach (string file in HttpContext.Current.Request.Files)
                    {
                        var postedFile = HttpContext.Current.Request.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + folderName + "/" + subFolderName + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            filePath = path + Path.GetFileName(postedFile.FileName);
                            if (File.Exists(filePath))
                            {
                                string archivePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + folderName + "/" + subFolderName + "/" + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(postedFile.FileName));
                                System.IO.File.Move(filePath, archivePath);
                            }
                            postedFile.SaveAs(filePath);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, ErrorMessage = "File saved successfully" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please upload the file" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadExamPaper(string year, string month, string semester, string specialisation, string paperCode, string paperTitle, string course, string examType, string uploadedBy)
        {
            ExamPaperDataModel paper = new ExamPaperDataModel();
            paper.ExamYear = year;
            paper.ExamMonth = month;
            paper.Semester = semester;
            paper.Specialisation = specialisation;
            paper.PaperCode = paperCode;
            paper.PaperTitle = paperTitle;
            paper.UploadedBy = uploadedBy;
            paper.UploadedDate = DateTime.UtcNow;
            paper.Course = course;
            paper.ExamType = examType;

            try
            {
                string folderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
                string subFolderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["subid"];
                string filePath = string.Empty;
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    int counter = 1;
                    foreach (string file in HttpContext.Current.Request.Files)
                    {
                        var postedFile = HttpContext.Current.Request.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            string dbPath = "data/" + folderName + "/" + subFolderName + "/" + year + "/" + month + "/Sem" + semester + "/" + specialisation;
                            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + dbPath);
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            filePath = path + "/" + paperCode + "_" + file + "_" + DateTime.Now.ToString("ddMMyyyy HHmmss").Replace(" ", "-") + ".pdf";
                            dbPath = dbPath + "/" + paperCode + "_" + file + "_" + DateTime.Now.ToString("ddMMyyyy HHmmss").Replace(" ", "-") + ".pdf";

                            if (File.Exists(filePath))
                            {
                                string archivePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + folderName + "/" + subFolderName + "/" + year + "/" + month + "/" + semester + "/" + specialisation + "/" + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(postedFile.FileName));
                                System.IO.File.Move(filePath, archivePath);
                            }
                            postedFile.SaveAs(filePath);

                            if (counter == 1)
                                paper.Paper1 = dbPath;
                            else if (counter == 2)
                                paper.Paper2 = dbPath;
                            else if (counter == 3)
                                paper.Paper3 = dbPath;

                            counter++;
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                        }
                    }

                    db.SaveExamPaper(paper);
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, ErrorMessage = "File saved successfully" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please upload the file" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage Download(string fileName)
        {
            string folderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
            string subFolderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["subid"];

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(folderName) && !string.IsNullOrEmpty(subFolderName))
            {
                string fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + folderName + "/" + subFolderName + "/" + fileName);
                if (File.Exists(fullPath))
                {

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(fullPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    return response;
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "File saved successfully" });

            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "File saved successfully" });

        }

    }
}
