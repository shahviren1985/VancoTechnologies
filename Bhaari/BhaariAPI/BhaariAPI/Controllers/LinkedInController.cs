using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;

namespace BhaariAPI.Controllers
{
    public class LinkedInController : ApiController
    {
        [HttpGet]
        [Route("api/login/check2")]
        public void LinkedInAuth(string code, string state)
        {
            try
            {
                string authUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string clientKey = ConfigurationManager.AppSettings["ClientKey"];
                string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
                string redirectUrl = HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["RedirectUrl"]);
                var sign = "grant_type=authorization_code&code=" + HttpUtility.UrlEncode(code) + "&redirect_uri=" + redirectUrl + "&client_id="+ clientKey + "&client_secret=" + clientSecret;

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
                return;
            }
            catch
            {
                throw;
            }
        }
    }
}
