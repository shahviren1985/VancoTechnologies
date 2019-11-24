using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication.MasterPages
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {

       public string base_url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base_url = ConfigurationManager.AppSettings["Base_URL"].ToString();
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
                Response.Redirect("~/Login.aspx");
            }
            catch (Exception)
            {
            }
        }
    }
}