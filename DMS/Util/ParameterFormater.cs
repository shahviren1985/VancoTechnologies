
using System.Web;
namespace AA.Util
{
    public static class ParameterFormater
    {
        public static string FormatParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return parameter;
            string returnString = parameter;
            return HttpUtility.HtmlEncode(parameter);
        }

        public static string UnescapeXML(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;


            s = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(s));

            s = s.Replace("%2E", ".");
            s = s.Replace("%2e", ".");

            return s;
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
            returnString = returnString.Replace(" ", "");
            return returnString;
        }
    }
}
