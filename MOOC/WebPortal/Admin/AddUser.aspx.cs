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
using System.Web.Script.Serialization;

public partial class Admin_AddUser : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    const int FIRST_ELEMENT = 0;

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

        if (!IsPostBack)
        {

        }
    }

    protected void btnCreateUser_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        errorSummary.Visible = false;
        Success.Visible = false;
        DateTime tempLastLogin = DateTime.Now;

        try
        {
            if (!IsValidForm(out errorMessage))
            {
                errorSummary.InnerHtml = errorMessage;
                errorSummary.Visible = true;
                return;
            }

            ChapterDetailsDAO objChapterDetailsDAO = new ChapterDetailsDAO();
            UserDetailsDAO objUserDetailsDAO = new UserDetailsDAO();
            UserLoggerDAO objUserLoggerDAO = new UserLoggerDAO();

            string contentVersion = objChapterDetailsDAO.GetLatestContectVersion(cnxnString, logPath);

            UserLogins users = new UserLoginsDAO().GetUserByUserName(txtUserName.Text, logPath);
            if (users == null)
            {
                logger.Debug("AddUser.aspx.cs", "btnsubmit_Click", "before adding new user ", logPath);
                objUserDetailsDAO.CreateUser(DateTime.Now, DateTime.Now, txtFirstName.Text, txtLastName.Text, txtEmailAddress.Text, ddlUserType.SelectedValue, txtUserName.Text,
                                    txtPassword.Text, chkActive.Checked, chkEnabled.Checked, false, false,
                                    contentVersion, txtMobile.Text, txtFather.Text, txtMother.Text, txtRollNumber.Text, "Unknown Course", "Unknown Sub-course", DateTime.Now.Year, cnxnString, logPath);
                logger.Debug("AddUser.aspx.cs", "btnsubmit_Click", "after adding adding new user", logPath);

                // inserting one record in addmin apps database for login purpuse
                new UserLoginsDAO().AddUserLoginsDetail(Session["CollegeName"].ToString(), txtUserName.Text, txtPassword.Text, DateTime.Now, Session["ConnectionString"].ToString(), Session["PDFFolderPath"].ToString(),
                    Session["ReleaseFilePath"].ToString(), Session["LogFilePath"].ToString(), "User", Session["ApplicationType"].ToString(), "~/Dashboard.aspx", false, logPath);

                // inserting one record in studentcoursemapper table for default foc course
                UserDetails user = objUserDetailsDAO.GetUserByUserName(txtUserName.Text, cnxnString, logPath);
                if (user != null)
                {
                    StudentCourseMapper courseMapper = new StudentCourseMapperDAO().GetStudentCourseMapperRecordsByCourseUserId(user.Id, 1, cnxnString, logPath);
                    if (courseMapper == null)
                    {
                        new StudentCourseMapperDAO().AddStudentCourseMapper(user.Id, 1, true, cnxnString, logPath);
                    }
                }

                ClearData();
                Success.Visible = true;
            }
            else
            {
                errorSummary.InnerHtml = "Username aleady occupied by other user. Please choose other username";
                errorSummary.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Validation Function
    private bool IsValidForm(out string errorMessage)
    {
        bool isValid = true;
        errorMessage = "Success";

        if (string.IsNullOrEmpty(cnxnString))
        {
            errorMessage = "Unable to connect to database. Please re-login.";
            isValid = false;
        }
        else if (string.IsNullOrEmpty(txtFirstName.Text))
        {
            errorMessage = "Please enter first name!";
            isValid = false;
        }

        else if (string.IsNullOrEmpty(txtLastName.Text))
        {
            errorMessage = "Please enter last name!";
            isValid = false;
        }
        else if (ddlUserType.SelectedValue == "0")
        {
            errorMessage = "Please select user type!";
            isValid = false;
        }
        else if (string.IsNullOrEmpty(txtUserName.Text))
        {
            errorMessage = "Please enter user name!";
            isValid = false;
        }
        else if (string.IsNullOrEmpty(txtPassword.Text))
        {
            errorMessage = "Please enter password!";
            isValid = false;
        }

        return isValid;
    }
    #endregion

    #region Clear Data Function
    public void ClearData()
    {
        try
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            ddlUserType.SelectedIndex = FIRST_ELEMENT;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRollNumber.Text = string.Empty;
            txtFather.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtMother.Text = string.Empty;
            errorSummary.Visible = false;
            chkActive.Checked = false;
            chkEnabled.Checked = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    #endregion
}