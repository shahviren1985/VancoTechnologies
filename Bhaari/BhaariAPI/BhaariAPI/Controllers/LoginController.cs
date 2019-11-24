using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace BhaariAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("api/login/check")]
        public IHttpActionResult PersonLogin(string code, string state)
        {
            try
            {
                string authUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string clientKey = ConfigurationManager.AppSettings["ClientKey"];
                string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
                string redirectUrl = HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["RedirectUrl"]);
                var sign = "grant_type=authorization_code&code=" + HttpUtility.UrlEncode(code) + "&redirect_uri=" + redirectUrl + "&client_id=" + clientKey + "&client_secret=" + clientSecret;

                HttpWebRequest webRequest = WebRequest.Create(authUrl + "?" + sign) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                Stream dataStream = webRequest.GetRequestStream();

                string postData = string.Empty;
                byte[] postArray = Encoding.ASCII.GetBytes(postData);

                dataStream.Write(postArray, 0, postArray.Length);
                dataStream.Close();

                WebResponse response = webRequest.GetResponse();
                dataStream = response.GetResponseStream();

                StreamReader responseReader = new StreamReader(dataStream);
                string returnVal = responseReader.ReadToEnd().ToString();

                LinkedINVM linkedINVM = JsonConvert.DeserializeObject<LinkedINVM>(returnVal);

                responseReader.Close();
                dataStream.Close();
                response.Close();

                //Get Profile Details
                RestClient client = new RestClient("https://api.linkedin.com/v2/me?oauth2_access_token=" + linkedINVM.access_token);
                var request = new RestRequest(Method.GET);
                IRestResponse response1 = client.Execute(request);
                var content = response1.Content;
                var person = JsonConvert.DeserializeObject<MyProfile>(content);
                client = new RestClient("https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))&oauth2_access_token=" + linkedINVM.access_token);
                request = new RestRequest(Method.GET);
                response1 = client.Execute(request);
                content = response1.Content;
                JObject email = JObject.Parse(content);
                person.emailAddress = ((JValue)email["elements"].First["handle~"]["emailAddress"]).Value.ToString();

                string ipAddress = GetClientIp();

                DatabaseManager dm = new DatabaseManager();
                dm.RegisterUser(person.localizedFirstName, person.localizedLastName, person.emailAddress, person.id, "LinkedIn", ipAddress);

                redirectUrl = redirectUrl.Replace("api/login/check", "Home/Login?t=");
                var uri = new Uri(redirectUrl + person.id + "&f=" + person.localizedFirstName + "&l=" + person.localizedLastName + "&e=" + person.emailAddress);
                return Redirect(uri);
            }
            catch
            {
                throw;
            }
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = request.Properties["MS_HttpContext"] as HttpContextBase;
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            return null;
        }
    }

    public class LinkedINVM
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class LinkedINResVM
    {
        public string firstName { get; set; }
        public string headline { get; set; }
        public string id { get; set; }
        public string lastName { get; set; }
        public Sitestandardprofilerequest siteStandardProfileRequest { get; set; }
    }

    public class Sitestandardprofilerequest
    {
        public string url { get; set; }
    }

    public class MyProfile
    {
        public string localizedLastName { get; set; }
        public string id { get; set; }
        public string localizedFirstName { get; set; }
        public string emailAddress { get; set; }
    }

    public class DatabaseManager
    {
        BhaariEntities db = null;

        public DatabaseManager()
        {
            db = new BhaariEntities();
        }

        public void RegisterUser(string firstName, string lastName, string emailAddress, string id, string source, string ipAddress)
        {
            DbSqlQuery<UserDetail> users = db.UserDetails.SqlQuery("select * from UserDetails where SourceId='" + id + "'");

            UserLoginHistory login = new UserLoginHistory();
            login.IPAddress = ipAddress;
            login.LoginDate = DateTime.UtcNow;
            login.UserId = 0;

            foreach (UserDetail user in users)
            {
                login.UserId = user.Id;
            }

            if (login.UserId == 0)
            {
                UserDetail ud = new UserDetail();
                ud.FirstName = firstName;
                ud.LastName = lastName;
                ud.EmailAddress = emailAddress;
                ud.Source = source;
                ud.SourceId = id;
                ud.RegistrationDate = DateTime.UtcNow;
                ud = db.UserDetails.Add(ud);
                login.UserId = ud.Id;
            }

            db.UserLoginHistories.Add(login);
            db.SaveChanges();
        }

        public string ApplyToJob(string jobId, string sourceUrl, string jobTitle, string companyName, int userId, string resumePath)
        {
            JobApplication jobApplication = new JobApplication();
            jobApplication.JobSource = "Indeed";
            jobApplication.JobId = jobId;
            jobApplication.SourceUrl = sourceUrl;
            jobApplication.JobTitle = jobTitle;
            jobApplication.CompanyName = companyName;
            jobApplication.UserId = userId;
            jobApplication.ResumePath = resumePath;
            jobApplication.ApplicationDate = DateTime.UtcNow;

            db.JobApplications.Add(jobApplication);
            db.SaveChanges();
            return "Successfully applied to the job";
        }

        public void SaveFavouriteJob(string jobTitle, string companyName, string datePosted, string jobId, string userId)
        {
            FavouriteJob job = new FavouriteJob();
            job.JobId = jobId;
            job.UserId = userId;
            job.JobSource = "Indeed";
            job.JobTitle = jobTitle;
            job.CompanyName = companyName;
            job.DateFavorited = DateTime.UtcNow;
            job.DatePosted = datePosted;

            db.FavouriteJobs.Add(job);
            db.SaveChanges();
        }

        public List<FavouriteJob> GetFavouriteJobs(string userId)
        {
            DbSqlQuery<FavouriteJob> favJobs = db.FavouriteJobs.SqlQuery("select * from FavouriteJobs where UserId='" + userId + "' and DatePosted > DATEADD(DAY, -30, getdate())  ORDER BY id Desc");
            List<FavouriteJob> jobs = new List<FavouriteJob>();

            foreach (FavouriteJob f in favJobs)
            {
                jobs.Add(f);
            }

            return jobs;
        }
    }
}
