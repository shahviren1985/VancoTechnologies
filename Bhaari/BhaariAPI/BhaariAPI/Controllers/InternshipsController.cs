using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhaariAPI.Controllers
{
    public class InternshipsController : Controller
    {
        // GET: Internships
        public ActionResult Index()
        {
            return View();
        }

        // GET: Internships/Details/5
        public ActionResult GovernmentJobs(string location)
        {
            return View();
        }
    }
}
