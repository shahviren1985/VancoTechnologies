using AA.DAO;
using AA.DAOBase;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageUsers : Page
{
    string role = string.Empty;
    string loggedInUser = string.Empty;
    string connString = string.Empty;
    string logPath = string.Empty;
    Database db = new Database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("/Login.apsx", false);
            return;
        }

        role = Session["RoleType"].ToString();
        loggedInUser = Session["UserName"].ToString();
        connString = Session["ConnectionString"].ToString();
        logPath = Session["LogFilePath"].ToString();

        if (!IsPostBack)
        {
            BindGridView();
            PopulateRoles();
        }
    }

    public void BindGridView()
    {
        UserDetailsDAO dao = new UserDetailsDAO();
        gvUsers.DataSource = dao.GetUserList(connString, logPath);
        gvUsers.DataBind();

        ChangePasswordForm.Visible = false;
    }

    public void PopulateRoles()
    {
        RoleDetailsDAO dao = new RoleDetailsDAO();
        ddRole.DataSource = dao.GetRoleDetails(connString, logPath);
        ddRole.DataTextField = "Name";
        ddRole.DataValueField = "Id";
        ddRole.DataBind();

        ddRole.Items.Add(new ListItem("--Select Role--", "-1"));
        ddRole.SelectedValue = "-1";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserDetailsDAO dao = new UserDetailsDAO();
        UserDetails ud = dao.GetUserDetailList(txtUserName.Text, connString, logPath);
        if (ud == null)
        {
            dao.CreateUser(txtUserName.Text, txtFirstName.Text, txtLastName.Text, int.Parse(ddRole.SelectedItem.Value), txtPassword.Text, DateTime.Now, chkActive.Checked, hfDepartments.Value, txtEmail.Text, txtPassword.Text, connString, logPath);
            txtEmail.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            chkActive.Checked = false;
            ddRole.SelectedValue = "-1";
            Error.Style.Add("color", "green");
            Error.InnerHtml = "User created successfully.";
        }
        else
        {
            // show error message that user already exists - choose different user name
            Error.InnerHtml = "User name already exist. Please choose different user name";
        }
    }

    protected void btnChangePassword_Click(object s, EventArgs e)
    {
        this.cpStatus.InnerHtml = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(this.txtUPassword.Text) && !string.IsNullOrEmpty(this.txtURePassword.Text) && !string.IsNullOrEmpty(this.hfUserName.Value))
            {
                if (this.txtUPassword.Text == this.txtURePassword.Text)
                {
                    new UserDetailsDAO().ChangePassword(this.hfUserName.Value, this.txtUPassword.Text, this.connString, this.logPath);
                    new UserLoginsDAO().ChangePassword(this.hfUserName.Value, this.txtOldPassword.Text, this.txtUPassword.Text, this.logPath);
                    this.cpStatus.InnerHtml = "Password changed successfully";
                    this.cpStatus.Style["color"] = "green";
                    this.txtUPassword.Text = string.Empty;
                    this.txtURePassword.Text = string.Empty;
                    this.txtOldPassword.Text = string.Empty;
                }
                else
                {
                    this.cpStatus.InnerHtml = "Password and retype password does not match";
                    this.cpStatus.Style["color"] = "red";
                }
            }
            else
            {
                this.cpStatus.InnerHtml = "All fields are mandatory";
                this.cpStatus.Style["color"] = "red";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnDeleteUser_Click(object s, EventArgs e)
    {
        try
        {
            // 1. Change password
            new UserDetailsDAO().ChangePassword(this.hfUserName.Value, "Admin@123", this.connString, this.logPath);
            new UserLoginsDAO().ChangePassword(this.hfUserName.Value, this.txtOldPassword.Text, "Admin@123", this.logPath);

            // 2. Change the parent id of the current user's folder id
            new FolderDetailsDAO().DeleteUser(this.hfUserName.Value, this.connString, this.logPath);
            this.cpStatus.InnerHtml = "Selected user's folder is deleted & password is changed.";
            this.cpStatus.Style["color"] = "green";
        }
        catch (Exception)
        {
            this.cpStatus.InnerHtml = "An error occurred while deleting a user";
            this.cpStatus.Style["color"] = "red";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UserDetailsDAO dao = new UserDetailsDAO();

        UserDetails previousUser = dao.GetUserDetailList(txtEditUserName.Text, connString, logPath);

        UserDetails ud = new UserDetails();

        ud.Email = txtEditEmailAddress.Text;
        ud.FirstName = txtEditFirstName.Text;
        ud.LastName = txtEditLastName.Text;
        ud.UserName = txtEditUserName.Text;
        ud.IsActive = chkEditActiveUser.Checked;
        ud.RoleId = int.Parse(ddEditRole.SelectedValue);
        ud.Departments = previousUser.Departments;
        ud.LastLogin = previousUser.LastLogin;
        //ud.Departments = string.Empty; 
        dao.ChangeUserDetails(ud, connString, logPath);
        Error.InnerHtml = "User updated successfully.";
        Error.Style.Add("color", "green");
        EditForm.Visible = false;
        ChangePasswordForm.Visible = false;
        BindGridView();
    }

    protected void gvUsers_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count >= 7)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;
            //e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
    }

    protected void gvUsers_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void gvUsers_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "OnEdit")
        {
            EditForm.Visible = true;
            ChangePasswordForm.Visible = true;

            // Load the user details with id as 
            UserDetailsDAO dao = new UserDetailsDAO();
            UserDetails ud = dao.GetUserDetailList(e.CommandArgument.ToString(), connString, logPath);

            // Populate it in form
            txtEditUserName.Text = ud.UserName;
            txtEditFirstName.Text = ud.FirstName;
            txtEditLastName.Text = ud.LastName;
            txtEditEmailAddress.Text = ud.Email;
            chkEditActiveUser.Checked = ud.IsActive;

            hfUserName.Value = ud.UserName;
            txtOldPassword.Text = ud.Password;

            RoleDetailsDAO roleDao = new RoleDetailsDAO();
            ddEditRole.DataSource = roleDao.GetRoleDetails(connString, logPath);
            ddEditRole.DataTextField = "Name";
            ddEditRole.DataValueField = "Id";
            ddEditRole.DataBind();

            ddEditRole.Items.Add(new ListItem("--Select Role--", "-1"));
            ddEditRole.SelectedValue = ud.RoleId.ToString();
        }
    }


}