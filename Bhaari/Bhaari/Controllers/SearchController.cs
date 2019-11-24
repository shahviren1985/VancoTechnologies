using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.HttpOverrides;

namespace Bhaari.Controllers
{
    public class JobQueryModel
    {
        public string Query { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    [ApiController]
    public class SearchController : ControllerBase
    {
        // GET api/values
        [HttpPost]
        [Route("api/search/jobs")]
        public List<IndeedJobsProperties> jobs(JobQueryModel jobQuery)
        {
            string ipAddress = GetIPAddress();

            string city = jobQuery.City, country = jobQuery.Country;
            string url = "http://api.indeed.com/ads/apisearch?publisher=132558381004159&start={0}&limit=25&q=" + jobQuery.Query + "&l=" + city + "&radius=100&latlong=1&co=" + country + "&userip=" + ipAddress + "&useragent=Mozilla/%2F4.0%28Firefox%29&v=2";

            List<IndeedJobsProperties> jobList = new List<IndeedJobsProperties>();

            if (string.IsNullOrEmpty(jobQuery.Query) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            {
                return null;
            }

            for (int i = 0; i < 4; i++)
            {
                string newUrl = string.Format(url, i * 25);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newUrl);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();

                    var jobs = GetJobsFromXML(data);
                    jobList.AddRange(jobs);
                }
            }

            return jobList;
        }

        //[HttpGet]
        //[Route("api/search/skills")]
        //public IEnumerable<string> searchskills(string query)
        //{
        //    return LuceneSearchLibrary.LuceneSearch.Search(query);
        //}
        [HttpGet]
        [Route("api/getIPAddress")]
        public string getMyIP()
        {
            return Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        [HttpPost]
        [Route("api/search/jobdetails")]
        public string getJobDetails(JobDetails job)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.indeed.com/viewjob?" + job.Url);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                var doc = new HtmlDocument();
                doc.LoadHtml(data);
                string description = doc.GetElementbyId("jobDescriptionText").InnerHtml;


                string companyName = doc.DocumentNode.SelectNodes("/html[1]/head[1]/title[1]").FirstOrDefault().InnerHtml;
                companyName = companyName.Replace(" - Indeed.com", "");
                return "<b>" + companyName + "</b><br/><br/>" + description;

            }
            return string.Empty;
        }

        private List<IndeedJobsProperties> GetJobsFromXML(string data)
        {
            List<IndeedJobsProperties> jobs = new List<IndeedJobsProperties>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);

                XmlNodeList jobResults = doc.SelectNodes("response/results/result");

                foreach (XmlNode jobResult in jobResults)
                {
                    IndeedJobsProperties job = new IndeedJobsProperties();

                    if (doc.SelectSingleNode("response/query") != null)
                        job.Tags = doc.SelectSingleNode("response/query").InnerText.ToLower();

                    job.JobTitle = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("jobtitle").InnerText);
                    job.CompanyName = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("company").InnerText);
                    job.City = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("city").InnerText);
                    job.State = jobResult.SelectSingleNode("state").InnerText;
                    job.Country = jobResult.SelectSingleNode("country").InnerText;
                    //job.DatePosted = jobResult.SelectSingleNode("date").InnerText;
                    job.JobDescription = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("snippet").InnerText);
                    job.SourceUrl = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("url").InnerText);
                    job.SourceUrl = job.SourceUrl.Substring(job.SourceUrl.IndexOf("/viewjob?"), 9);

                    job.Latitude = jobResult.SelectSingleNode("latitude").InnerText;
                    job.Longitude = jobResult.SelectSingleNode("longitude").InnerText;

                    job.SourceId = jobResult.SelectSingleNode("jobkey").InnerText;
                    job.SourceName = string.Empty;
                    job.Experience = (job.JobTitle.ToLower().IndexOf("intern") > -1 || job.JobDescription.ToLower().IndexOf("intern") > -1) ? "Internship" : "Experience";
                    // Process unique company name, country (US), date posted

                    string jobUniqeId = DateTime.Now.Ticks.ToString().Remove(0, 13) + new Random().Next(1, 999).ToString();

                    if (jobs.Find(jui => jui.Id == int.Parse(jobUniqeId)) == null)
                        job.Id = int.Parse(jobUniqeId);
                    else
                    {
                        bool isNotFindUniqeId = true;

                        while (isNotFindUniqeId)
                        {
                            //jobUniqeId = new Random().Next(1, 99999);
                            jobUniqeId = DateTime.Now.Ticks.ToString().Remove(0, 13) + new Random().Next(1, 999).ToString();

                            if (jobs.Find(jui => jui.Id == int.Parse(jobUniqeId)) == null)
                            {
                                job.Id = int.Parse(jobUniqeId);
                                isNotFindUniqeId = false;
                            }
                        }
                    }

                    try
                    {
                        job.Country = job.Country == "US" ? "United States" : job.Country;
                        job.UniqueCompanyName = job.CompanyName.Trim().ToLower().Replace(" ", "-");
                        job.DatePosted = DateTime.Parse(jobResult.SelectSingleNode("date").InnerText).ToString("yyyy/MM/dd");
                    }
                    catch (Exception)
                    {

                    }

                    var obj = new[] { new
                                                                    {
                                                                        City = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("city").InnerText),
                                                                        Latitude = jobResult.SelectSingleNode("latitude").InnerText,
                                                                        Longitude = jobResult.SelectSingleNode("longitude").InnerText
                                                                    }}.ToList();

                    job.Location = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
                    //job.Latitude = jobResult.SelectSingleNode("latitude").InnerText;
                    //job.Longitude = jobResult.SelectSingleNode("longitude").InnerText;

                    jobs.Add(job);
                }

                jobs = jobs.OrderByDescending(jb => jb.DatePosted).ToList();
            }

            catch (Exception ex)
            {

            }

            return jobs;
        }

        private string GetIPAddress()
        {
            string IPAddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
    }

    public class JobDetails
    {
        public string Url { get; set; }
        public string CompanyName { get; set; }
        public string JobId { get; set; }
    }

    public class IndeedJobsProperties
    {
        [JsonProperty(propertyName: "Id")]
        public int Id { get; set; }

        [JsonProperty(propertyName: "JobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty(propertyName: "JobDescription")]
        public string JobDescription { get; set; }

        [JsonProperty(propertyName: "SkillSetRequired")]
        public string SkillSetRequired { get; set; }

        [JsonProperty(propertyName: "JobTitleShortForm")]
        public string JobTitleShortForm { get; set; }

        [JsonProperty(propertyName: "DatePosted")]
        public string DatePosted { get; set; }

        [JsonProperty(propertyName: "ViewCount")]
        public int ViewCount { get; set; }

        [JsonProperty(propertyName: "SearchCount")]
        public int SearchCount { get; set; }

        [JsonProperty(propertyName: "SourceName")]
        public string SourceName { get; set; }

        [JsonProperty(propertyName: "CompanyId")]
        public string CompanyId { get; set; }

        [JsonProperty(propertyName: "CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty(propertyName: "CompanyLogo")]
        public string CompanyLogo { get; set; }

        [JsonProperty(propertyName: "Location")]
        public string Location { get; set; }

        [JsonProperty(propertyName: "SourceUrl")]
        public string SourceUrl { get; set; }

        [JsonProperty(propertyName: "SourceId")]
        public string SourceId { get; set; }

        [JsonProperty(propertyName: "City")]
        public string City { get; set; }

        [JsonProperty(propertyName: "State")]
        public string State { get; set; }

        [JsonProperty(propertyName: "Country")]
        public string Country { get; set; }

        [JsonProperty(propertyName: "Experience")]
        public string Experience { get; set; }

        [JsonProperty(propertyName: "UniqueCompanyName")]
        public string UniqueCompanyName { get; set; }

        [JsonProperty(propertyName: "Latitude")]
        public string Latitude { get; set; }

        [JsonProperty(propertyName: "Longitude")]
        public string Longitude { get; set; }

        [JsonProperty(propertyName: "Tags")]
        public string Tags { get; set; }
    }
}
