using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class SiteAnalytics
    {
        public int Id { get; set; }
        public int TimeInSecond { get; set; }
        public int TimeInMinutes { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string Langauge { get; set; }
        public string OperatingSystem { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string RefferPage { get; set; }
        public string UserAgent { get; set; }
        public bool IsMobile { get; set; }
        public string MobileDivice { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
