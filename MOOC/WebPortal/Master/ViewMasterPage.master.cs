using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;

public partial class Master_ViewMasterPage : System.Web.UI.MasterPage
{
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                //Response.Redirect("~/Login.aspx", false);
                //return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Server.MapPath(Session["LogFilePath"].ToString());
                configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
                collegeName = Session["CollegeName"].ToString();

                ApplicationLogoHeader appLogo = new ApplicationLogoHeaderDAO().GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);
                if (appLogo != null)
                {                    
                    logoImage.ImageUrl = Util.BASE_URL + appLogo.LogoImagePath.Replace("~/", "");
                    divLogoHeader.InnerHtml = appLogo.LogoText;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        if (Session["UserType"] != null && Session["UserType"].ToString().ToLower() == "admin")
        {
            Application["IsAdminLoggedId"] = false;
            Util.IsAdminLoggedIn = false;
        }

        Session["UserId"] = null;
        Session["UserType"] = null;
        Session["IsNewUser"] = false;
        Session["IsStatusTrackerRequired"] = false;

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
