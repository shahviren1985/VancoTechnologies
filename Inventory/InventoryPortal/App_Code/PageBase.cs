using System;
using System.Web.UI;
using System.Configuration;

public class PageBase : Page
{
    public string BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];

    public PageBase()
    {
        try
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.Error += new System.EventHandler(this.Page_Error);
        }
        catch (Exception)
        {

            throw new Exception();
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
                //Response.Redirect("~/Login.aspx?ref=" + Request.Url.AbsolutePath);
            }            
        }        
    }

    public void Page_Error(object sender, EventArgs e)
    {
        // TO DO handle this error event
        // 1. Read the error code, read error xml file and render correct user friendly error message
        Exception objErr = Server.GetLastError();
        Server.ClearError();
        Response.Redirect("~/ErrorPage.aspx?errorcode=" + objErr.Message);

    }    
}