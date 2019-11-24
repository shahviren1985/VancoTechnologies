using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class LoginController : ApiController
    {
            GetDataFromDatabase db;
            public LoginController()
        {
            db = new GetDataFromDatabase();
        }

        [HttpPost]
        public HttpResponseMessage ValidateUser(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.MobileNumber))
                return Request.CreateResponse(HttpStatusCode.OK, new User() { IsSuccess = false, ErrorMessage = "Invalid Data" });
          
            User userDetails = db.GetUser(user);
            if (userDetails == null)
                return Request.CreateResponse(HttpStatusCode.OK, new User() { IsSuccess = false, ErrorMessage = "Invalid Credentials" });
            else
            {
                userDetails.IsSuccess = true;
                userDetails.SuccessMessage = "Success";
                return Request.CreateResponse(HttpStatusCode.OK, userDetails);
            }
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
                            List<User> users = System.IO.File.ReadAllLines(filePath)
                                                  .Skip(1)
                                                  .Select(v => QuarterlyReports.Models.User.FromCsv(v))
                                                  .ToList();
                            if (users != null && users.Count > 0)
                            {
                                foreach (User response in users)
                                {
                                    if (response.UserId == 0)
                                    {
                                        bool isUserSaved = db.InsertUserDeatils(response) > 0;
                                        if (!isUserSaved)
                                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                                    }
                                    else
                                    {
                                        bool isUserUpdated = db.UpdateUserDeatils(response) > 0;
                                        if (!isUserUpdated)
                                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.OK,  new BaseClass() { IsSuccess = true, SuccessMessage = "Records inserted successfully" }) ;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
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
            writer.Write(db.ExportUsers(collageCode));
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Users.csv" };
            return result;
        }

      
    }
}
