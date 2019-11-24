using System;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Master_Master : MasterPage
{
    
    public string College;
    
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        base.Session["ConnectionString"] = null;
        base.Session["UserName"] = null;
        base.Session["LogFilePath"] = null;
        base.Session.Abandon();
        base.Response.Redirect("~/Login.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string scheme = base.Request.Url.Scheme;
        string host = base.Request.Url.Host;
        int port = base.Request.Url.Port;
        string absolutePath = base.Request.Url.AbsolutePath;
        if (base.Session["UserName"] == null)
        {
            string str = base.Request.QueryString["id"];
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                str2 = "?id=" + str;
            }
            base.Response.Redirect("~/Login.aspx" + str2);
        }
        else
        {
            if (((base.Session["RoleType"].ToString() == "2") || (base.Session["RoleType"].ToString() == "3")) || (base.Session["RoleType"].ToString() == "4"))
            {
                this.Users.Visible = true;
                this.Locations.Visible = true;
                this.Reports.Visible = true;
                this.Inward.Visible = true;
                this.Outward.Visible = true;
                this.ChangePassword.Visible = false;
            }
            else
            {
                this.ChangePassword.Visible = true;
                this.Users.Visible = false;
                this.Locations.Visible = false;
                this.Reports.Visible = false;
                this.Inward.Visible = false;
                this.Outward.Visible = false;
            }
            this.imgLogo.ImageUrl = "../static/images/logo1.gif";
            this.imgProfile.ImageUrl = "../static/images/user.png";
            this.PersonName.InnerHtml = base.Session["FirstName"].ToString() + " " + base.Session["LastName"].ToString();
            this.College = base.Session["College"].ToString();
        }
    }
}
