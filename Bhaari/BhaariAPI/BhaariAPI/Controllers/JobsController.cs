using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BhaariAPI.Controllers
{

    public class RootObject
    {
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class IPtoCountry
    {
        public string city { get; set; }
        public string country { get; set; }
    }

    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Index()
        {
            string query = RouteData.Values["action"].ToString();
            return View();
        }

        public ActionResult JobDetails(string country, string city)
        {
            string cityName = string.Empty;
            try
            {
                if (country.LastIndexOf("-in-") > 1)
                {
                    cityName = country.Substring(country.LastIndexOf("-in-") + 4);
                    country = country.Substring(0, country.LastIndexOf("-in-"));
                }

                ViewBag.JobCategory = country;
                ViewBag.City = cityName;

                string url = "http://indeed.com/viewjob?jk=" + city.Substring(city.IndexOf("quertyuiop") + 11);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();

                    var doc = new HtmlDocument();
                    doc.LoadHtml(data);
                    string description = doc.GetElementbyId("jobDescriptionText").InnerHtml;


                    string companyName = doc.DocumentNode.SelectNodes("/html[1]/head[1]/title[1]").FirstOrDefault().InnerHtml;
                    companyName = companyName.Replace(" - Indeed.com", "");
                    ViewBag.JobDetails = "<b>" + companyName + "</b><br/><br/>" + description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //ViewBag.JobDetails = string.Empty;
            return View();
        }


        // GET: Jobs/GovernmentJobs/Mumbai
        public ActionResult GovernmentJobs(string country, string city)
        {
            ViewBag.PageTitle = "Government Jobs, Sarkari Naukri in " + city + ", " + country + " - Bhaari.com";
            ViewBag.PageKeywords = "Government Jobs, Sarkari Naukri " + DateTime.Now.Year + ", Government Jobs in " + DateTime.Now.Year + ", Government Jobs in " + country + ", Government Jobs in " + city;
            string cityName = city;

            if (string.IsNullOrEmpty(city))
            {
                if (country.LastIndexOf("-in-") > 1)
                {
                    city = country.Substring(country.LastIndexOf("-in-") + 4);
                    cityName = city;
                    city = ", " + country.Replace("-", " ") + " jobs in " + city;
                }
            }

            country = GetCountry(cityName);

            ViewBag.City = cityName;
            ViewBag.Jobs = jobs(city, country, "Government Jobs");
            ViewBag.JsonJobs = JsonConvert.SerializeObject(ViewBag.Jobs, Newtonsoft.Json.Formatting.None);
            ViewBag.FirstJobUrl = ViewBag.Jobs[0].SourceUrl;
            ViewBag.FirstJobCssClass = "background-color: rgb(240, 243, 247);";
            ViewBag.JobDetails = GetJobdetails(ViewBag.Jobs[0].SourceUrl);
            return View();
        }

        // GET: Jobs/GovernmentJobs/Mumbai
        public ActionResult NearMe()
        {
            string location = string.Empty;
            string cityName = string.Empty;
            if (RouteData.Values["country"] != null)
            {
                location = RouteData.Values["country"].ToString();
            }
            else
            {
                // GEt the location of the user using third party API
            }

            string city = string.Empty;
            if (location.LastIndexOf("-in-") > 1)
            {
                city = location.Substring(location.LastIndexOf("-in-") + 4);
                cityName = city;
                location = location.Substring(0, location.LastIndexOf("-in-"));
                city = ", " + location.Replace("-", " ") + " jobs in " + city;
            }

            if (string.IsNullOrEmpty(cityName))
            {
                string api = "http://ip-api.com/json/" + GetClientIp();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string data = string.Empty;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));

                    data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }

                var ip = JsonConvert.DeserializeObject<IPtoCountry>(data);
                cityName = ip.city;
            }

            string country = GetCountry(cityName);
            location = location.Replace("-", " ");
            ViewBag.PageTitle = location + " jobs in " + cityName + " by Bhaari.com";
            ViewBag.PageKeywords = location + " jobs near me, " + location + " jobs in " + DateTime.Now.Year + cityName;
            ViewBag.City = cityName;
            ViewBag.Jobs = jobs(cityName, country, location);
            ViewBag.JsonJobs = JsonConvert.SerializeObject(ViewBag.Jobs, Newtonsoft.Json.Formatting.None);
            ViewBag.FirstJobCssClass = "background-color: rgb(240, 243, 247);";

            if (ViewBag.Jobs != null && ViewBag.Jobs.Count > 0)
            {
                ViewBag.FirstJobUrl = ViewBag.Jobs[0].SourceUrl;
                ViewBag.JobDetails = GetJobdetails(ViewBag.Jobs[0].SourceUrl);
            }
            else
            {
                ViewBag.FirstJobUrl = string.Empty;
                ViewBag.JobDetails = string.Empty;
            }

            return View();
        }

        private List<IndeedJobsProperties> jobs(string city, string country, string query)
        {
            string ipAddress = GetClientIp();
            string url = "http://api.indeed.com/ads/apisearch?publisher=132558381004159&start={0}&limit=25&q=" + query + "&l=" + city + "&co=" + country + "&radius=100&latlong=1&userip=" + ipAddress + "&useragent=Mozilla/%2F4.0%28Firefox%29&v=2";

            List<IndeedJobsProperties> jobList = new List<IndeedJobsProperties>();

            if (string.IsNullOrEmpty(query) || string.IsNullOrEmpty(city))
            {
                return null;
            }

            for (int i = 0; i < 2; i++)
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
                        readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();

                    var jobs = GetJobsFromXML(data);
                    jobList.AddRange(jobs);
                }
            }

            return jobList;
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
                    job.JobDescription = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("snippet").InnerText.Replace("\"", ""));
                    job.SourceUrl = jobResult.SelectSingleNode("url").InnerText;
                    job.SourceUrl = HttpUtility.HtmlEncode(job.SourceUrl.Substring(job.SourceUrl.IndexOf("jk=") + 3));
                    job.Latitude = jobResult.SelectSingleNode("latitude").InnerText;
                    job.Longitude = jobResult.SelectSingleNode("longitude").InnerText;

                    job.SourceId = HttpUtility.HtmlEncode(jobResult.SelectSingleNode("jobkey").InnerText);
                    job.SourceName = "Indeed";
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
                        job.UniqueCompanyName = HttpUtility.HtmlEncode(job.CompanyName.Trim().ToLower().Replace(" ", "-"));
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

                    job.Location = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
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

        private string GetCountry(string city)
        {
            string api = "https://maps.googleapis.com/maps/api/geocode/json?address=" + city + "&key=AIzaSyCdtzsR9COkbXaY49oFTS4lziV_VhH40Y8";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = string.Empty;


            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));

                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                var json = JsonConvert.DeserializeObject<RootObject>(data);
                if (json.results != null && json.results.Count > 0)
                {
                    foreach (AddressComponent address in json.results[0].address_components)
                    {
                        if (address.types.Contains("country"))
                        {
                            return address.short_name;
                        }
                    }
                }


            }

            return "india";
        }



        private string GetClientIp()
        {
            string result = Request.UserHostAddress;

            if (result == "::1" || result == "127.0.0.1")
                result = "115.96.242.171";

            return result;
        }

        private string GetJobdetails(string url)
        {
            url = "http://indeed.com/viewjob?jk=" + url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));

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
    }
}
