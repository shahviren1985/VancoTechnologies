using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.DAOBase;
using ITM.Courses.ConfigurationsManager;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using System.Configuration;
using System.Web.UI;
using System.Web.Script.Serialization;

public partial class Admin_ManageUser : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    const int FIRST_ELEMENT = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        errorSummary.Visible = false;
        Success.Visible = false;

        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("../Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            //configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
        }

        if (!IsPostBack)
        {
            //bindGridView();
            PopulateCollegeDropDown();
            // PopulateDirectoryFiles();
        }

    }

    private void bindGridView()
    {
        try
        {
            UserDetailsDAO users = new UserDetailsDAO();
            gvUserDetails.DataSource = users.GetAllUsers(true, cnxnString, logPath);
            gvUserDetails.DataBind();
            gvUserDetails.Visible = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateCollegeDropDown()
    {
        try
        {
            ddlCollege.DataSource = new UserLoginsDAO().GetUniqueCollageCnxnStrFromController(cnxnString, logPath);
            ddlCollege.DataTextField = "CollegeName";
            ddlCollege.DataValueField = "CnxnString";
            ddlCollege.DataBind();

            ddlCollege.Items.Add(new ListItem("--Select College--", "0"));
            ddlCollege.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (ViewState["Users"] != null)
            {
                gvUserDetails.PageIndex = e.NewPageIndex;
                gvUserDetails.DataSource = (List<UserDetails>)ViewState["Users"];
                gvUserDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            UserDetailsDAO objUserDetailsDAO = new UserDetailsDAO();
            UserLoggerDAO objUserLoggerDAO = new UserLoggerDAO();

            foreach (GridViewRow row in gvUserDetails.Rows)
            {
                Label Id = ((Label)row.FindControl("gvlblId"));
                CheckBox chkIsActiveStatus = ((CheckBox)row.FindControl("gvchkIsActive"));

                logger.Debug("ManageUser.aspx.cs", "btnUpdate_Click", "before updating IsActive status ", logPath);
                //objUserDetailsDAO.CreateUser(DateTime.Now, DateTime.Now, ParameterFormater.FormatParameter(txtFirstName.Text), ParameterFormater.FormatParameter(txtLastName.Text), ParameterFormater.FormatParameter(txtEmailAddress.Text), ddlUserType.SelectedValue, ParameterFormater.FormatParameter(txtUserName.Text), txtPassword.Text, chkActive.Checked, chkEnabled.Checked, chkCompleted.Checked, chkCertified.Checked, contentVersion, cnxnString, LogPath);
                objUserDetailsDAO.UpdateIsActiveStatus(Int32.Parse(Id.Text), chkIsActiveStatus.Checked, cnxnString, logPath);
                logger.Debug("ManageUser.aspx.cs", "btnUpdate_Click", "after updating IsActive status", logPath);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
        Success.Visible = true;
    }

    protected void btnChangePwd_Click(object s, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtOldPassword.Text) || string.IsNullOrEmpty(txtNewPassword.Text) || string.IsNullOrEmpty(hfSelectedUserName.Value))
            {
                errorSummary.InnerHtml = "Please fill mandatory fields.";
                errorSummary.Visible = true;
                return;
            }

            cnxnString = ddlCollege.SelectedValue;

            new UserDetailsDAO().ChangePassword(hfSelectedUserName.Value, txtOldPassword.Text, txtNewPassword.Text, cnxnString, logPath);
            new UserLoginsDAO().ChangePassword(hfSelectedUserName.Value, txtOldPassword.Text, txtNewPassword.Text, logPath);

            Success.InnerHtml = "Password successfully changed for " + hfSelectedUserName.Value;
            Success.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Search Functionality
    protected void btnSearch_Click(object s, EventArgs e)
    {
        gvUserDetails.DataSource = null;
        gvUserDetails.DataBind();

        try
        {
            if (ddlCollege.SelectedValue == "0" || (string.IsNullOrEmpty(txtUserName.Text.Trim()) && string.IsNullOrEmpty(txtFirstLastName.Text.Trim())))
            {
                errorSummary.InnerText = "Please enter either username or first name or last name";
                errorSummary.Visible = true;
                return;
            }

            cnxnString = ddlCollege.SelectedValue;

            UserDetailsDAO userDAO = new UserDetailsDAO();
            List<UserDetails> users = new List<UserDetails>();

            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) && !string.IsNullOrEmpty(txtFirstLastName.Text.Trim()))
            {
                //if both textboxes filled then get record by username and first or last name
                users = userDAO.GetSudentByUserNameandFirstorLastName(txtUserName.Text, txtFirstLastName.Text, cnxnString, logPath);

            }
            else if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                //IF username not empty then get user by username
                users = userDAO.GetSudentByUserName(txtUserName.Text, cnxnString, logPath);
            }
            else if (!string.IsNullOrEmpty(txtFirstLastName.Text.Trim()))
            {
                //IF firstname or last name  not empty then get user by first or last name
                users = userDAO.GetSudentByFirstorLastName(txtFirstLastName.Text, cnxnString, logPath);
            }

            if (users != null && users.Count > 0)
            {
                btnUpdate.Visible = true;
                ViewState["Users"] = users;
            }
            else { btnUpdate.Visible = false; }

            gvUserDetails.DataSource = users;
            gvUserDetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    /* Change username */
    #region Change Username
    protected void btnChangeUsername_Click(object s, EventArgs e)
    {
        Success.Visible = false;
        errorSummary.Visible = false;

        try
        {
            if (string.IsNullOrEmpty(txtOldUsername.Text) || string.IsNullOrEmpty(txtNewUsername.Text) || string.IsNullOrEmpty(txtConfirmUsername.Text) || string.IsNullOrEmpty(hfUserPassword.Value))
            {
                errorSummary.InnerHtml = "Please fill mandatory fields.";
                errorSummary.Visible = true;
                return;
            }

            if (txtNewUsername.Text.Trim() != txtConfirmUsername.Text.Trim())
            {
                errorSummary.InnerHtml = "New username and confirm username not matched.";
                errorSummary.Visible = true;
                return;
            }

            UserLogins userLogin = new UserLoginsDAO().GetUserLoginByUserNamePassword(txtOldUsername.Text, hfUserPassword.Value, logPath);

            if (userLogin != null)
            {
                UserDetails userDetails = new UserDetailsDAO().IsAuthenticated(txtOldUsername.Text, hfUserPassword.Value, userLogin.CnxnString, Server.MapPath(userLogin.LogFilePath));

                if (userDetails != null)
                {
                    UserLogins users = new UserLoginsDAO().GetUserByUserName(txtNewUsername.Text, Server.MapPath(userLogin.LogFilePath));

                    if (users == null)
                    {
                        // change username and email-address
                        new UserDetailsDAO().UpdateUserNameEmail(userDetails.Id, txtNewUsername.Text, txtNewUsername.Text, userLogin.CnxnString, Server.MapPath(userLogin.LogFilePath));
                        new UserLoginsDAO().UpdateUserName(userLogin.CollegeId, txtNewUsername.Text, Server.MapPath(userLogin.LogFilePath));

                        Success.InnerText = "Successfully changed username";
                        Success.Visible = true;

                        txtUserName.Text = txtNewUsername.Text;

                        txtOldUsername.Text = string.Empty;
                        txtNewUsername.Text = string.Empty;
                        txtConfirmUsername.Text = string.Empty;
                        hfUserPassword.Value = string.Empty;

                        btnSearch_Click(s, e);
                    }
                    else
                    {
                        errorSummary.InnerHtml = "Username already exists please use other username.";
                        errorSummary.Visible = true;
                        return;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}