using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BhaariAPI.Controllers
{
    public class FileManagerController : ApiController
    {
        [HttpPost]
        [Route("api/upload")]
        public HttpResponseMessage UploadResume(int userId, string jobId, string sourceUrl, string jobTitle, string companyName)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                string fileName = string.Empty;
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    fileName = postedFile.FileName.Replace(" ", "-") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/" + fileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }

                DatabaseManager dm = new DatabaseManager();
                dm.ApplyToJob(jobId, sourceUrl, jobTitle, companyName, userId, fileName);
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        [HttpGet]
        [Route("api/savejob")]
        public HttpResponseMessage SaveFavJob(string jobId, string sourceUrl, string jobTitle, string companyName, string userId, string datePosted)
        {
            DatabaseManager dm = new DatabaseManager();
            dm.SaveFavouriteJob(jobTitle, companyName, datePosted, jobId, userId);
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, "Job saved successfully");
            return result;
        }

        [HttpGet]
        [Route("api/getsavedjobs")]
        public HttpResponseMessage GetFavJob(string userId)
        {
            DatabaseManager dm = new DatabaseManager();
            List<FavouriteJob> jobs = dm.GetFavouriteJobs(userId);
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, jobs);
            return result;
        }
    }
}
