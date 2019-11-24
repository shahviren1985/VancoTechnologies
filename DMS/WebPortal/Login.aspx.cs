using AA.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*protected void Login1_Authenticate(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtUserName.Value) && !string.IsNullOrEmpty(txtPassword.Value))
            {
                UserDetailsDAO ud = new UserDetailsDAO();
                UserDetails user = ud.IsAuthenticated(txtUserName.Value, txtPassword.Value, ConfigurationManager.AppSettings["LogPath"]);

                if (user != null)
                {
                    Session["ConnectionString"] = ConfigurationManager.ConnectionStrings["con"].ToString();
                    Session["UserName"] = user.UserName;
                    Session["FirstName"] = user.FirstName;
                    Session["LastName"] = user.LastName;
                    Session["LogFilePath"] = Server.MapPath(ConfigurationManager.AppSettings["LogPath"]);
                    Session["RoleType"] = user.RoleId;

                    if (Session["RoleType"].ToString() == "3")
                    {
                        Response.Redirect("~/Dashboard.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Dashboard.aspx", false);
                    }
                }
                else
                {
                    Error.InnerHtml = "Invalid user name or password";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }*/

    protected void Login1_Authenticate(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(this.txtUserName.Value) && !string.IsNullOrEmpty(this.txtPassword.Value))
            {
                UserLogins logins = new UserLoginsDAO().IsAuthenticated(this.txtUserName.Value, this.txtPassword.Value, ConfigurationManager.AppSettings["LogPath"]);
                if ((logins != null) && logins.IsAuthenticated)
                {
                    UserDetails details = new UserDetailsDAO().IsAuthenticated(this.txtUserName.Value, this.txtPassword.Value, logins.ConnectionString, base.Server.MapPath(logins.LogFilePath));
                    if (details != null)
                    {
                        this.Session["ConnectionString"] = logins.ConnectionString;
                        this.Session["LogFilePath"] = base.Server.MapPath(logins.LogFilePath);
                        this.Session["UserName"] = details.UserName;
                        this.Session["FirstName"] = details.FirstName;
                        this.Session["LastName"] = details.LastName;
                        this.Session["RoleType"] = details.RoleId;
                        this.Session["UserId"] = details.Id;
                        this.Session["College"] = logins.CollegeName;
                        this.Session["ExcelReportURL"] = logins.ExcelReportURL;
                        this.Session["LogFileUrl"] = logins.LogFilePath;
                        this.Session["ReleaseFilePath"] = logins.ReleaseFilePath;
                        this.Session["PDFFolderPath"] = logins.PDFFolderPath;
                        this.Session["RoleId"] = details.RoleId;
                        string str = base.Request.QueryString["id"];
                        if (!string.IsNullOrEmpty(str))
                        {
                            base.Response.Redirect("~/Documents.aspx?id=" + str);
                        }
                        if (this.Session["RoleType"].ToString() == "3")
                        {
                            base.Response.Redirect("~/Dashboard.aspx", false);
                        }
                        else
                        {
                            base.Response.Redirect("~/Dashboard.aspx", false);
                        }
                    }
                    else
                    {
                        this.Error.InnerHtml = "Invalid user name or password";
                    }
                }
                else
                {
                    this.Error.InnerHtml = "Invalid user name or password";
                }
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

}