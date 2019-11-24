
using System.Web;
namespace Util
{
    public static class UnescapeFormat
    {
        public static string UnescapeXML(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            return HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(s)));
            //string returnString = s;

            //returnString = returnString.Replace("&apos;", "'");

            //returnString = returnString.Replace("&quot;", "\"");

            //returnString = returnString.Replace("&gt;", ">");

            //returnString = returnString.Replace("&lt;", "<");

            //returnString = returnString.Replace("&amp;", "&");

            //return returnString;
        }
    }
}
