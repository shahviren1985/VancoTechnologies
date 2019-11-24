using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace WebApplication.MasterPages
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
       public  string base_url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base_url = ConfigurationManager.AppSettings["Base_URL"].ToString();
                if (Session["UserName"] == null)
                {
                    lnkLogout.Text = "";

                }
                else { lnkLogout.Text = "LogOut"; }

            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect(base_url + "Login.aspx", true);
            }
            catch (Exception)
            {
            }
        }
    }
}