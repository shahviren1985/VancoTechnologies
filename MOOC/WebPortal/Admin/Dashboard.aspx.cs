using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Dashboard : PageBase
{
    string logPath;
    string cnxnString;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("../Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
        }
    }
}