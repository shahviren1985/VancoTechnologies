using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string redirectPath = "default.aspx";
            string redirectPath = "Dashboard.aspx";

            string path = Request.UrlReferrer.AbsolutePath;            

            if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/admin/"))
            {
                redirectPath = "Admin/Dashboard.aspx";
            }
            else if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/staff/"))
            {
                redirectPath = "Staff/Dashboard.aspx";
            }
            else if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/superadmin/"))
            {
                redirectPath = "SuperAdmin/Dashboard.aspx";
            }

            //aHome.HRef = Util.BASE_URL + redirectPath;
        }
        catch { }
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        if (Session["UserType"] != null && Session["UserType"].ToString().ToLower() == "admin")
        {
            Application["IsAdminLoggedId"] = false;
            Util.IsAdminLoggedIn = false;
        }

        Session["CollegeId"] = null;
        Session["CollegeName"] = null;
        Session["ConnectionString"] = null;
        Session["UserName"] = null;
        Session["ReleaseFilePath"] = null;
        Session["PDFFolderPath"] = null;
        // added by vasim
        Session["LogFilePath"] = null;
        Session["RoleType"] = null;
        Session["ApplicationType"] = null;
        Session["RedirectURL"] = null;
        Session["Name"] = null;

        Session.Abandon();
        //Response.Redirect(Util.BASE_URL + "Login.aspx");
        Response.Redirect(Util.BASE_URL);
    }
}