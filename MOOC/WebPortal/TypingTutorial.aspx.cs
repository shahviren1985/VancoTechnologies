using ITM.Courses.DAO;
using System;


public partial class TypingTutorial : PageBase
{
    string logPath;
    string cnxnString;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("~/Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());

        }

        UserDetails user = new UserDetailsDAO().GetUserByUserName(Session["UserName"].ToString(), cnxnString, logPath);

        if (user != null && user.IsNewUser)
        {
            Response.Redirect("~/ChangePassword.aspx", false);
        }
    }
}