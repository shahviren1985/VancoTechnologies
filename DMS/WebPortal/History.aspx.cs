using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class History : Page, IRequiresSessionState
{
    //protected HtmlAnchor aAdvancedLink;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["RoleType"] == null)
        {
            base.Response.Redirect("/Login.apsx", false);
        }
        else if (this.Session["RoleType"].ToString() != "1")
        {
            this.aAdvancedLink.Visible = true;
        }
        else
        {
            this.aAdvancedLink.Visible = false;
        }
    } 
}
