using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class FileController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            try
            {
                string folderName =(string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
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
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "File saved successfully");
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
        public HttpResponseMessage Download(string fileName)
        {
            string folderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
            string subFolderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["subid"];

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(folderName) && !string.IsNullOrEmpty(subFolderName))
            {
                string fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + folderName + "/" + subFolderName + "/"+fileName);
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
        }

    }
}
