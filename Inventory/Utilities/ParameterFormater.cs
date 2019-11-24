
using System;
using System.Web;

namespace ITM.Utilities
{
    public static class ParameterFormater
    {
        public static string FormatParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return parameter;
            string returnString = parameter;

            return HttpUtility.HtmlEncode(parameter);
            //returnString = returnString.Replace("'", "&apos;");

            //returnString = returnString.Replace("\"", "&quot;");

            //returnString = returnString.Replace(">", "&gt;");

            //returnString = returnString.Replace("<", "&lt;");

            //returnString = returnString.Replace("&", "&amp;");

            //return returnString;
        }

        public static string UnescapeXML(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            return HttpUtility.HtmlDecode(s);
            //return s.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");

        }

        public static DateTime GetCurrentIndianDate()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        }
    }
}
