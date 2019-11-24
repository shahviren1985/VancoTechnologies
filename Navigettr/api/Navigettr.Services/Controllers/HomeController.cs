using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GreenNub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            // WriteToEventLog("GreenNub", "NBV", "Bad Request", EventLogEntryType.Error);

            return View();
        }

        public static void WriteToEventLog(string sLog, string sSource, string message, EventLogEntryType level)
        {


            if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, message, level);
        }
    }
}
