using System;
using System.Web.UI;
using System.Configuration;
using ITM.Courses.LogManager;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : Page
{
    Logger logger = new Logger();

    public PageBase()
    {
        try
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.Error += new System.EventHandler(this.Page_Error);
        }
        catch (Exception)
        {
            throw new Exception("15261");
        }

    }

    public void Page_Load(object sender, EventArgs e)
    {
        string path = Request.Url.AbsolutePath;
        string pageName = path.Substring(path.LastIndexOf("/") + 1);

        if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/admin/"))
        {
            if (Session["UserName"] == null)
            {
            }
        }

    }

    public void Page_Error(object sender, EventArgs e)
    {        
        // TO DO handle this error event
        // 1. Read the error code, read error xml file and render correct user friendly error message
        Exception objErr = Server.GetLastError();

        string logPath = ConfigurationManager.AppSettings["LogPath"];
        logger.Error("Error", "PageLoad", "Error occurred while loading dashboard page", objErr, logPath);

        Server.ClearError();
        Response.Redirect(Util.BASE_URL + "ErrorPage.aspx");
    }
}