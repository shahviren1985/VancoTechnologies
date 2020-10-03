using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SVT.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace SVT.API.Controllers
{
    public class FileController : ApiController
    {
        /// <summary>
        /// File Upload api
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            try
            {
                //string guid = Guid.NewGuid().ToString();
                string folderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
                string subFolderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["subid"];
                string filePath = string.Empty;
                
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    string fileName = string.Empty;
                    foreach (string file in HttpContext.Current.Request.Files)
                    {
                        var postedFile = HttpContext.Current.Request.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/temp/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string time = DateTime.Now.ToString("yyyy-MM-dd HH mm ss");
                            fileName = folderName + "_" + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + time.Replace(" ", "_") + Path.GetExtension(postedFile.FileName);
                            filePath = path + folderName + "_" + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + time.Replace(" ", "_") + Path.GetExtension(postedFile.FileName);
                            if (File.Exists(filePath))
                            {
                                string archivePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/PDF/" + folderName + "/" + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(postedFile.FileName));
                                System.IO.File.Move(filePath, archivePath);
                            }
                            postedFile.SaveAs(filePath);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new FileClass() { IsSuccess = true, FileType = folderName, File = fileName,  SuccessMessage = "File saved successfully" });
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

        /// <summary>
        /// file download api
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// GET JSON data of file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetJsonData(string fileName)
        {
            string folderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["id"];
            string subFolderName = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["subid"];

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(folderName) && !string.IsNullOrEmpty(subFolderName))
            {
                string fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + folderName + "/" + subFolderName + "/" + fileName);
                if (File.Exists(fullPath))
                {
                    JArray jArray;
                    using (StreamReader file = File.OpenText(fullPath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        jArray = (JArray)JToken.ReadFrom(reader);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, jArray);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "File not found." });

            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please provide valid data." });

        }

    }
}
