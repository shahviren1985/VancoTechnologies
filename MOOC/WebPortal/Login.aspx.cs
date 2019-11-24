using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class Login : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util(Request.Url);
        try
        {
            Response.Redirect(Util.BASE_URL + "default.aspx", false);
            return;
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    protected void Login1_Authenticate(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtUserName.Value) && !string.IsNullOrEmpty(txtPassword.Value))
            {
                string logPath = Server.MapPath("Logs");

                UserLogins userLoginDetails = new UserLoginsDAO().GetUserLoginByUserNamePassword(txtUserName.Value, txtPassword.Value, logPath);

                if (userLoginDetails != null)
                {
                    UserDetailsDAO ud = new UserDetailsDAO();
                    UserDetails user = ud.IsAuthenticated(txtUserName.Value, txtPassword.Value, userLoginDetails.CnxnString, Server.MapPath(userLoginDetails.LogFilePath));

                    if (user != null && user.IsAuthenticated && userLoginDetails.IsAuthenticated)
                    {
                        if (!user.IsActive || !user.IsEnable)
                        {
                            errorSummary.InnerHtml = "Your account disabled or deactivated";
                            errorSummary.Visible = true;
                            return;
                        }

                        // insert entry in userlogger table
                        UserLoggerDAO userLoggerDAO = new UserLoggerDAO();
                        userLoggerDAO.AddLogedUser(user.Id, DateTime.Now, Request.UserHostAddress, userLoginDetails.CnxnString, Server.MapPath(userLoginDetails.LogFilePath));

                        Session["UserId"] = user.Id;
                        Session["BASE_URL"] = Util.BASE_URL;
                        Session["UserName"] = user.UserName;
                        Session["UserType"] = user.UserType;
                        Session["IsNewUser"] = user.IsNewUser;

                        Session["ConnectionString"] = userLoginDetails.CnxnString;
                        Session["ReleaseFilePath"] = userLoginDetails.ReleaseFilePath;
                        Session["PDFFolderPath"] = userLoginDetails.PDFFolderPath;
                        Session["LogFilePath"] = userLoginDetails.LogFilePath;
                        Session["CollegeName"] = userLoginDetails.CollegeName;
                        Session["ApplicationType"] = userLoginDetails.ApplicationType;

                        Session["IsStatusTrackerRequired"] = userLoginDetails.IsStatusTrackerRequired;

                        if (Session["UserType"].ToString().ToLower() == "superadmin")
                        {
                            Application["IsAdminLoggedId"] = true;
                            Util.IsAdminLoggedIn = true;
                            Response.Redirect(Util.BASE_URL + "SuperAdmin/Dashboard.aspx", false);
                        }
                        else if (Session["UserType"].ToString().ToLower() == "admin")
                        {
                            Application["IsAdminLoggedId"] = true;
                            Util.IsAdminLoggedIn = true;
                            Response.Redirect(Util.BASE_URL + "Admin/Dashboard.aspx", false);
                        }
                        else if (Session["UserType"].ToString().ToLower() == "staff")
                        {
                            Application["IsAdminLoggedId"] = true;
                            Util.IsAdminLoggedIn = true;
                            Response.Redirect(Util.BASE_URL + "Staff/Dashboard.aspx", false);
                        }
                        else
                        {
                            if (user.IsNewUser)
                            {
                                Response.Redirect(Util.BASE_URL + "ChangePassword.aspx", false);
                            }
                            else
                            {
                                Response.Redirect(Util.BASE_URL + "Dashboard.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        errorSummary.InnerHtml = "Invalid username or password";
                        errorSummary.Visible = true;
                    }
                }
                else
                {
                    errorSummary.InnerHtml = "Invalid username or password";
                    errorSummary.Visible = true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtUserName.Value) && string.IsNullOrEmpty(txtPassword.Value))
                {
                    errorSummary.InnerHtml = "Please enter username and password";
                    txtUserName.Style["border"] = "1px solid red";
                    txtPassword.Style["border"] = "1px solid red";
                }
                else if (string.IsNullOrEmpty(txtUserName.Value))
                {
                    errorSummary.InnerHtml = "Please enter username";
                    txtUserName.Style["border"] = "1px solid red";
                }
                else if (string.IsNullOrEmpty(txtPassword.Value))
                {
                    errorSummary.InnerHtml = "Please enter password";
                    txtPassword.Style["border"] = "1px solid red";
                }

                errorSummary.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}