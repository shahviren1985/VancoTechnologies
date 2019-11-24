using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;

public partial class ChangePassword : PageBase
{
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;

    string qRef = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                collegeName = Session["CollegeName"].ToString();
            }

            qRef = Request.QueryString["ref"];
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnChangePwd_Click(object s, EventArgs e)
    {
        errorSummary.Visible = false;
        Success.Visible = false;

        try
        {
            if (string.IsNullOrEmpty(txtoldPassword.Text) || string.IsNullOrEmpty(txtNewPassword.Text) || string.IsNullOrEmpty(Session["UserName"].ToString()))
            {
                errorSummary.InnerHtml = "All fields are mandatory.";
                errorSummary.Visible = true;
                return;
            }

            if (txtNewPassword.Text != txtRetypeNewPassword.Text)
            {
                errorSummary.InnerHtml = "New password and re-type should be match.";
                errorSummary.Visible = true;
                return;
            }

            UserDetails user = new UserDetailsDAO().GetUserByUserName(Session["UserName"].ToString(), cnxnString, logPath);

            if (user != null)
            {
                if (user.Password.Trim() == txtoldPassword.Text.Trim())
                {
                    new UserDetailsDAO().ChangePassword(Session["UserName"].ToString(), txtoldPassword.Text, txtNewPassword.Text, cnxnString, logPath);
                    new UserLoginsDAO().ChangePassword(Session["UserName"].ToString(), txtoldPassword.Text, txtNewPassword.Text, logPath);

                    new UserDetailsDAO().UpdateIsNewUserFlag(user.Id, false, cnxnString, logPath);

                    Success.InnerHtml = "Your password successfully changed.";
                    Success.Visible = true;
                    
                    if (!string.IsNullOrEmpty(qRef) && qRef.ToLower().Trim() == "ps")
                    {
                        Session["IsNewUser"] = false;
                        Response.Redirect(Util.BASE_URL + "Dashboard.aspx", false);
                    }
                }
                else
                {
                    errorSummary.InnerHtml = "Please enter right password.";
                    errorSummary.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}