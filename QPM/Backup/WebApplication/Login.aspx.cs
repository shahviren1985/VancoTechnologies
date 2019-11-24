using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ITM.DAO;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Util;

namespace WebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        string connString = string.Empty;
        string logPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                connString = ConfigurationManager.ConnectionStrings["DMSConnString"].ToString();
                logPath = ConfigurationManager.AppSettings["LogPath"].ToString();
            }
            catch (Exception)
            {
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e) 
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Value) || string.IsNullOrEmpty(txtPassword.Value))
                {

                    errorSummary.InnerText = "Please enter Username or Password.";
                    errorSummary.Style["display"] = "block";
                    return;
                }


                UserDetailsDAO userclass = new UserDetailsDAO();

                if (userclass.IsAuthenticated(txtUserName.Value, txtPassword.Value, connString, logPath))
                {
                    Session["UserName"] = txtUserName.Value;
                    //UserDetails user = userclass.GetUserDetailList(txtUserName.Value, connString, logPath);
                   // Session["RoleId"] = user.RoleId;
                   Response.Redirect("~/User/UploadControl.aspx", false);
                }
                else if (txtUserName.Value == ConfigurationManager.AppSettings["admin"] && txtPassword.Value == ConfigurationManager.AppSettings["adminpwd"])
                {
                    Session["UserNameAdmin"] = txtUserName.Value;
                    Session["RoleId"] = 5;
                    Response.Redirect("~/Admin/Dashboard.aspx", false);
                }
                else
                {
                    errorSummary.InnerText = "Invalid Username or Password.";
                    errorSummary.Style["display"] = "block";
                }
            }
            catch (Exception)
            {
            }
        }

    }
}