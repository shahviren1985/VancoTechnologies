using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ChangePassword : Page, IRequiresSessionState
{
    
    private string connString = string.Empty;
    private string loggedInUser = string.Empty;
    private Logger logger = new Logger();
    private string logPath = string.Empty;
    private string role = string.Empty;
    
    protected void btnChangePassword_Click(object s, EventArgs e)
    {
        try
        {
            this.error.Attributes.Remove("class");
            this.error.Attributes.Add("class", "alert alert-danger");
            if (this.ValidateControls())
            {
                UserDetailsDAO sdao = new UserDetailsDAO();
                UserDetails details = sdao.IsAuthenticated(this.loggedInUser, this.txtPassword.Text.Trim(), this.logPath);
                if ((details != null) && !string.IsNullOrEmpty(details.UserName))
                {
                    sdao.ChangePassword(this.loggedInUser, this.txtNewPassword.Text.Trim(), this.connString, this.logPath);
                    this.error.InnerHtml = "Your password changed successfully";
                    this.error.Attributes.Remove("class");
                    this.error.Attributes.Add("class", "alert alert-success");
                    this.txtNewPassword.Text = string.Empty;
                    this.txtPassword.Text = string.Empty;
                    this.txtReEnterPassword.Text = string.Empty;
                }
                else
                {
                    this.error.InnerHtml = "Please enter valid old password";
                }
                this.error.Style["display"] = "block";
            }
            else
            {
                this.error.Style["display"] = "block";
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserName"] == null)
        {
            base.Response.Redirect("/Login.apsx", false);
        }
        else
        {
            this.role = this.Session["RoleType"].ToString();
            this.loggedInUser = this.Session["UserName"].ToString();
            this.connString = this.Session["ConnectionString"].ToString();
            this.logPath = this.Session["LogFilePath"].ToString();
        }
    }

    private bool ValidateControls()
    {
        if (string.IsNullOrEmpty(this.txtPassword.Text))
        {
            this.error.InnerHtml = "Please enter password";
            return false;
        }
        if (string.IsNullOrEmpty(this.txtNewPassword.Text))
        {
            this.error.InnerHtml = "Please enter new password";
            return false;
        }
        if (string.IsNullOrEmpty(this.txtReEnterPassword.Text))
        {
            this.error.InnerHtml = "Please re-enter new password";
            return false;
        }
        if (this.txtNewPassword.Text.Trim() != this.txtReEnterPassword.Text.Trim())
        {
            this.error.InnerHtml = "New password and  re-enter password not matched";
            return false;
        }
        return true;
    }
}
