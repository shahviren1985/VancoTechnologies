using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.Utilities
{
    public class TimeFormats
    {
        public DateTime GetIndianStandardTime(DateTime time)
        {
            DateTime dt = DateTime.UtcNow;

            //TimeZoneInfo usEasternZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time"); // say
            TimeZoneInfo usEasternZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone.CurrentTimeZone.StandardName); // say    
            TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"); // say
            DateTime usEasternTime = TimeZoneInfo.ConvertTimeFromUtc(dt, usEasternZone);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(dt, indianZone);
            TimeSpan timeDiff = indianTime - usEasternTime;
            //usEasternTime = new DateTime(2013, 11, 27, 00, 06, 00); // or whatever the datetime to convert is
            usEasternTime = time; // or whatever the datetime to convert is

            indianTime = usEasternTime + timeDiff;
            return indianTime;
        }
    }
}
