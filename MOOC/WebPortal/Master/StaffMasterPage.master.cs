using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_StaffMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            //Response.Redirect(Util.BASE_URL + "Login.aspx");
            Response.Redirect(Util.BASE_URL);
            return;
        }
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        if (Session["UserType"] == null)
        {
            Response.Redirect(Util.BASE_URL + "Login.aspx");
            return;
        }

        if (Session["UserType"].ToString().ToLower() == "admin")
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
