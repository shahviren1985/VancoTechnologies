using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class PrintCertificate : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Logger.Debug("PrintExamFeeReceipt", "Page_Load", "Loading Print Exam Fee Receipt page");

            string path = Request.QueryString["path"];
            string postbackurl = Request.QueryString["url"];
            string page = Request.QueryString["page"];
            string file = Request.QueryString["file"];

            if (!string.IsNullOrEmpty(page))
            {
                switch (page)
                {
                    case "inactive":
                        this.Title = "Print Inactive Student Report";
                        break;
                    case "certificate":
                        this.Title = "Print Inactive Student Report";
                        break;
                    case "batchwisestatus":
                        this.Title = "Print Batch wise Student Status Report";
                        break;
                }

            }

            if (!Page.IsPostBack)
            {
                PrintMarksheet.Attributes.Add("src", path);
                lnkBack.PostBackUrl = postbackurl;
            }

            //Logger.Debug("PrintExamFeeReceipt", "Page_Load", "PrintExamFeeReceipt page loaded succesfully");
        }
        catch (Exception ex)
        {
            // Logger.Error("PrintExamFeeReceipt", "Page_Load", "Error occurred while loading PrintExamFeeReceipt page", ex);
            throw new Exception("12501");
        }
    }

}