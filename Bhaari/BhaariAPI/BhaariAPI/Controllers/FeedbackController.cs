using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;

namespace BhaariAPI.Controllers
{
    public class FeedbackController : ApiController
    {
        [HttpGet]
        [Route("api/SaveFeedback")]
        public string SaveFeedback(string n, string e, string f, string t, string l, string r)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("viren@bhaari.com");
            message.To.Add(new MailAddress("shah.viren1985@gmail.com"));
            message.Subject = "Bhaari.com - Feedback";
            message.IsBodyHtml = true;
            message.Body = "<html><body><b>Name:</b> " + n + "<br/><b>Email:</b> " + e + "<br/><b>Feedback:</b> " + f + "<br/><b>Technologies:</b> " + t + "<br/><b>Locations:</b> " + l + "<br/><b>Rating:</b> " + r + "<br/></body></html>";
            smtp.Port = 25;
            smtp.Host = "relay-hosting.secureserver.net";
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

            return HttpStatusCode.OK.ToString();
        }

        [HttpGet]
        [Route("api/ContactUs")]
        public string ContactUs(string n, string e, string f)
        {
            HttpRequest request = HttpContext.Current.Request;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("viren@bhaari.com");
            message.To.Add(new MailAddress("shah.viren1985@gmail.com"));
            message.Subject = "Bhaari.com - Feedback";
            message.IsBodyHtml = true;
            message.Body = "<html><body><b>Name:</b> " + n + "<br/><b>Email:</b> " + e + "<br/><b>Feedback:</b> " + f + "</body></html>";
            smtp.Port = 25;
            smtp.Host = "relay-hosting.secureserver.net";
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            return HttpStatusCode.OK.ToString();

        }
    }
}
