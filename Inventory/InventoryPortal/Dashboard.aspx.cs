using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.LogManager;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    Logger logger = new Logger();

    string cnxnString;
    string logPath;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Session["LogFilePath"].ToString();
                configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}