using System;
using System.Web.UI;
using ITM.LogManager;

public partial class Master : MasterPage
{
    Logger logger = new Logger();

    string cnxnString;
    string logPath;
    string configFilePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                logPath = Session["LogFilePath"].ToString();
                configFilePath = Server.MapPath(Session["ReleaseFilePath"].ToString());
                cnxnString = Session["ConnectionString"].ToString();
            }

            logger.Debug("Master", "Page_Load", "page loading", logPath);            
        }
        catch (Exception ex)
        {
            logger.Error("Master", "Page_Load", "Error occurred while Loading page", ex, logPath);
            throw new Exception();
        }
    }

    protected void OnLogOut(object sender, EventArgs e)
    {
        try
        {
            logger.Debug("Master", "OnLogOut", "Distroying the session", logPath);

            Session.Abandon();
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

            Response.Redirect("~/Login.aspx", false);
            logger.Debug("Master", "OnLogOut", "Session destroyed", logPath);
        }
        catch (Exception ex)
        {
            logger.Error("Master", "OnLogOut", "Error occurred while Logging out", ex, logPath);
            throw new Exception();
        }
    }
}
