
using System.Web;
namespace ITM.Courses.Utilities
{
    public static class ParameterFormater
    {
        public static string FormatParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return parameter;
            string returnString = parameter;
            return HttpUtility.HtmlEncode(parameter);
            ////returnString = returnString.Replace("%", "%25");
            //returnString = returnString.Replace("'", "&apos;");
            //returnString = returnString.Replace("?", "%3F");
            //returnString = returnString.Replace("\"", "%22");
            //returnString = returnString.Replace(".", "%2E");
            //returnString = returnString.Replace("|", "%7C");
            //return returnString;
        }

        public static string UnescapeXML(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;


            s = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(s));

            s = s.Replace("%2E", ".");
            s = s.Replace("%2e", ".");

            return s;
            //s = s.Replace("&apos;", "'");
            //s = s.Replace("%3F", "?");
            //s = s.Replace("%22", "\"");
            //s = s.Replace("%2E", ".");
            //s = s.Replace("%7C", "|");
            ////s = s.Replace("%25;", "%");
            //return s;
        }

        public static string RemoveCharacters(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return parameter;
            string returnString = parameter;

            returnString = returnString.Replace("'", "");
            returnString = returnString.Replace("?", "");
            returnString = returnString.Replace("\"", "");
            returnString = returnString.Replace(".", "");
            returnString = returnString.Replace("|", "");
            returnString = returnString.Replace("~", "");
            returnString = returnString.Replace("!", "");
            returnString = returnString.Replace("#", "");
            returnString = returnString.Replace("@", "");
            returnString = returnString.Replace("$", "");
            returnString = returnString.Replace("%", "");
            returnString = returnString.Replace("^", "");
            returnString = returnString.Replace("&", "");
            returnString = returnString.Replace("*", "");
            returnString = returnString.Replace("(", "");
            returnString = returnString.Replace(")", "");
            returnString = returnString.Replace("_", "");
            returnString = returnString.Replace("+", "");
            returnString = returnString.Replace("{", "");
            returnString = returnString.Replace("}", "");
            returnString = returnString.Replace("[", "");
            returnString = returnString.Replace("]", "");
            returnString = returnString.Replace(";", "");
            returnString = returnString.Replace(":", "");
            returnString = returnString.Replace(",", "");
            returnString = returnString.Replace("<", "");
            returnString = returnString.Replace(">", "");

            return returnString;
        }

        public static string GetSpecialCharsQS(string text)
        {
            return text.Replace(" aaa ", "&");
        }
        //public static string FormatParameter(string parameter)
        //{
        //    if (string.IsNullOrEmpty(parameter)) return parameter;
        //    string returnString = parameter;

        //    return HttpUtility.HtmlEncode(parameter);
        //}

        //public static string UnescapeXML(this string s)
        //{
        //    if (string.IsNullOrEmpty(s))
        //        return s;

        //    return HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(s));
        //}

        public static string GetOSNameByUserAgent(string userAgent)
        {
            string os = "Unknown OS";

            if (userAgent.Contains("Win16"))
            {
                os = "Windows 3.11";
            }
            else if (userAgent.Contains("Win16") || userAgent.Contains("Win16") || userAgent.Contains("Win16"))
            {
                os = "Windows 95";
            }
            else if (userAgent.Contains("Windows 98") || userAgent.Contains("Win98"))
            {
                os = "Windows 98";
            }
            else if (userAgent.Contains("Windows NT 5.0") || userAgent.Contains("Windows 2000"))
            {
                os = "Windows 2000";
            }
            else if (userAgent.Contains("Windows NT 5.1") || userAgent.Contains("Windows XP"))
            {
                os = "Windows XP";
            }
            else if (userAgent.Contains("Windows NT 5.2"))
            {
                os = "Windows Server 2003";
            }
            else if (userAgent.Contains("Windows NT 6.0"))
            {
                os = "Windows Vista";
            }
            else if (userAgent.Contains("Windows NT 6.1"))
            {
                os = "Windows 7";
            }
            else if (userAgent.Contains("Windows NT 4.0") || userAgent.Contains("WinNT4.0") || userAgent.Contains("WinNT") || userAgent.Contains("Windows NT"))
            {
                os = "Windows NT 4.0";
            }
            else if (userAgent.Contains("Windows ME"))
            {
                os = "Windows ME";
            }
            else if (userAgent.Contains("OpenBSD"))
            {
                os = "Open BSD";
            }
            else if (userAgent.Contains("SunOS"))
            {
                os = "Sun OS";
            }
            else if (userAgent.Contains("Linux") || userAgent.Contains("X11"))
            {
                os = "Linux";
            }
            else if (userAgent.Contains("Mac_PowerPC") || userAgent.Contains("Macintosh"))
            {
                os = "Mac OS";
            }
            else if (userAgent.Contains("QNX"))
            {
                os = "QNX";
            }
            else if (userAgent.Contains("BeOS"))
            {
                os = "BeOS";
            }
            else if (userAgent.Contains("OS/2"))
            {
                os = "OS/2";
            }

            return os;
        }
    }
}
