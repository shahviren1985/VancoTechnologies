using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BhaariAPI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Jobs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        //public string GetClientIPAddress()
        //{
        //    return GetClientIp();
        //}

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = null;

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
}
