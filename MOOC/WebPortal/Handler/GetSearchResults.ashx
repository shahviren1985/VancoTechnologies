<%@ WebHandler Language="C#" Class="GetSearchResults" %>

using System;
using System.Web;
using System.IO;
using System.Net;

public class GetSearchResults : IHttpHandler
{
    private static string BING_SEARCH_URL = "http://api.search.live.net/json.aspx?Appid=A2486496A832AD756BDEEE1A681550937CA4C9FA&query={0}&sources={1}&image.count={2}&image.offset={3}";
    //private static string GOOGLE_SEARCH_URL = "https://www.googleapis.com/customsearch/v1?key=AIzaSyDkup9FVBJaoovqnffvnEIIewu2ql0yqyU&cx=002090671093959760506:npvmcgucshm&q={0}&fileType={1}&num={2}";
    private static string GOOGLE_SEARCH_URL = "http://ajax.googleapis.com/ajax/services/search/images?v=1.0&hr=en&rsz={1}&q={0}&start={2}";
    //private static string YOUTUBE_SEARCH_URL = "http://gdata.youtube.com/feeds/api/videos?q={0}&max-results=9&v=2&alt=json-in-script&format=5&start-index={1}";
    private static string YOUTUBE_SEARCH_URL = "https://gdata.youtube.com/feeds/api/videos?q={0}&orderby=published&start-index={1}&max-results=10&v=2&alt=json";
    private static string PICASSA_SEARCH_URL = "https://picasaweb.google.com/data/feed/api/all?q={0}&start-index={1}&alt=json&max-results=10";
    private static string IMGUR_SEARCH_URL = "https://gdata.youtube.com/feeds/api/videos?q={0}&orderby=published&start-index={1}&max-results=10&v=2&alt=json";
    private static string FLICKR_SEARCH_URL = "http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=3c20421b26c4faf6c56fabb648de6b3f&tags={0}&per_page=10&format=json&page={1}";
    private static string VIMEO_SEARCH_URL = "https://vimeo.com/api/rest/v2?format=json&method=vimeo.videos.search&query={0}&page={1}&per_page=10&oauth_consumer_key={2}&oauth_version=1.0&oauth_signature_method=HMAC-SHA1&oauth_timestamp={3}&oauth_nonce={4}&oauth_signature={5}";

    public void ProcessRequest(HttpContext context)
    {
        string engine = context.Request["engine"];
        string type = context.Request["type"];
        string query = context.Request["q"];
        string start = context.Request["start"];
        string errorMsg = string.Empty;
                 
        if (string.IsNullOrEmpty(start))
            start = "0";
        string response = "";

        if (string.IsNullOrEmpty(query) || query.ToLower() == "search internet...")
        {
            context.Response.Write("");
            return;
        }

        switch (engine.ToLower())
        {
            case "google":
                response = GetSearchResultsFromGoogle(type, query, start);
                break;
            case "bing":
                response = GetSearchResultsFromBing(type, query, start);
                break;
            case "picassa":
                response = GetSearchResultsFromPicassa(type, query, start);
                break;
            case "imgur":
                break;
            case "flickr":
                string res = GetSearchResultsFromFlickr(type, query, start);
                res = res.Replace("jsonFlickrApi(", "");
                res = res.Remove(res.LastIndexOf(")"));
                response = res;
                break;
            case "vimeo":
                response = GetSearchResultsFromVimeo(query, start);
                break;
            case "youtube":
                response = GetSearchResultsFromYoutube(query, start);
                break;
        }
        context.Response.Write(response);
    }

    private string GetSearchResultsFromYoutube(string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(YOUTUBE_SEARCH_URL, query, start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetSearchResultsFromVimeo(string query, string start)
    {
        /*OAuthentication oAuth = new OAuthentication();

        Uri url = new Uri(string.Format("https://vimeo.com/api/rest/v2?format=json&method=vimeo.videos.search&query={0}&page={1}&per_page=10", query, start));
        string normalizedUrl = string.Empty;
        string normalizedRequestParameters = string.Empty;
        string signatureBase = oAuth.GenerateSignature(url, "069580fa1a41b3ce3e84458dcfec7c1fe11da563", "8b80e8923217a2280aaeebd6f759a306e3d49efa", null, null, "GET", GenerateTimeStamp(), GenerateNonce(), OAuthentication.SignatureTypes.HMACSHA1, out normalizedUrl, out normalizedRequestParameters);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(normalizedUrl + "?" + normalizedRequestParameters + "&oauth_signature=" + signatureBase);
        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }*/
        return string.Empty;
            
    }

    private string GetSearchResultsFromGoogle(string type, string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(GOOGLE_SEARCH_URL, query, "8", start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetSearchResultsFromBing(string type, string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(BING_SEARCH_URL, query, "image", "20", start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetSearchResultsFromPicassa(string type, string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(PICASSA_SEARCH_URL, query, start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetSearchResultsFromImgur(string type, string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(IMGUR_SEARCH_URL, query, "image", "20", start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetSearchResultsFromFlickr(string type, string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(FLICKR_SEARCH_URL, query, start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private string GetSearchResultsFromVimeo(string type, string query, string start)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(VIMEO_SEARCH_URL, query, "image", "8", start));

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Generate the timestamp for the signature        
    /// </summary>
    /// <returns></returns>
    private string GenerateTimeStamp()
    {
        // Default implementation of UNIX time of the current UTC time
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    /// <summary>
    /// Generate a nonce
    /// </summary>
    /// <returns></returns>
    private string GenerateNonce()
    {
        Random random = new Random();
        // Just a simple implementation of a random number between 123400 and 9999999
        return random.Next(123400, 9999999).ToString();
    }
    
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}