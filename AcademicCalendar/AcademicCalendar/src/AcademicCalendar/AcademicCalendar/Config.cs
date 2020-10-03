using System;
using System.Configuration;

namespace AcademicCalendar
{
    static class Config
    {
        public static DateTime StartDate { get; set; }

        public static void Init()
        {
            var appSettings = ConfigurationManager.AppSettings;

            StartDate = DateTime.Parse(appSettings["StartDate"]);
        }
    }
}
