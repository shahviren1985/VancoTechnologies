using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;
using ITM.DAO;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblpath.Text = System.Web.HttpContext.Current.Server.MapPath("~/data/teachers.json"); 
    }

    protected void Login1_Authenticate(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtUserName.Value) && !string.IsNullOrEmpty(txtPassword.Value))
            {
                UserLogin ud = new UserLogin();
                ud = ud.UserAuthentication(txtUserName.Value, txtPassword.Value, ConfigurationManager.AppSettings["LogPath"]);

                if (ud.IsAuthenticated)
                {
                    Session["CollegeId"] = ud.CollegeId;
                    Session["CollegeName"] = ud.CollegeName;
                    Session["ConnectionString"] = ud.ConnectionString;
                    Session["UserName"] = ud.UserName;
                    Session["ReleaseFilePath"] = ud.ReleaseFilePath;
                    Session["PDFFolderPath"] = ud.PDFFolderPath;
                    // added by vasim
                    Session["LogFilePath"] = Server.MapPath(ud.LogFilePath);
                    Session["RoleType"] = ud.RoleType;
                    Session["ApplicationType"] = ud.ApplicationType;
                    Session["RedirectURL"] = ud.RedirectUrl;

                    string redirectURL = Session["RedirectURL"].ToString().Trim();

                    if (Session["RoleType"].ToString().ToLower() == "admin")
                    {
                        Response.Redirect("~/Dashboard.aspx", false);
                    }
                    else if (string.IsNullOrEmpty(redirectURL))
                    {
                        Response.Redirect("~/Dashboard.aspx", false);
                    }
                    else
                    {
                        Response.Redirect(redirectURL, false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}