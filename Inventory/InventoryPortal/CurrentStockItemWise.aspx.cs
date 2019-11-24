using ITM.DAO;
using ITM.ExcelGenerator;
using ITM.LogManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CurrentStockItemWise : System.Web.UI.Page
{
    Logger logger = new Logger();

    string cnxnString;
    string logPath;
    string configFilePath;
    string pdfFolderPath;
    string pdfURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                logPath = Session["LogFilePath"].ToString();
                configFilePath = Server.MapPath(Session["ReleaseFilePath"].ToString());
                cnxnString = Session["ConnectionString"].ToString();
                pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
                pdfURL = Session["PDFFolderPath"].ToString();
            }

            if (!Page.IsPostBack)
            {
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            PopulateGridView(DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string file;
            string fileName = string.Empty;
            //string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"]) + "InventoryReports";
            string path = Path.Combine(pdfFolderPath, "CurrentStockReport");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //fileName = "CurrentStockReport_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.ToString("HHmmss") + ".xlsx";
            fileName = "currentStock-details-report-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            file = Path.Combine(path, fileName);

            logger.Debug("CurrentStockReport", "GetCurrentStockDataTable", "File name=" + fileName, logPath);

            try
            {
                logger.Debug("CurrentStockReport", "Excel click", "Preparing Datatable", logPath);

                DataTable dt = GetStockDetailsDataTable();

                logger.Debug("CurrentStockReport", "Excel click", "Total Rows=" + dt.Rows.Count, logPath);

                GenerateExcelReports2 objGenerateExcelRpt = new GenerateExcelReports2();

                List<int> col = new List<int>() { 8, 20, 20, 10, 12, 10, 10, 10, 10, 10, 10, 10, 
                    15, 10, 10, 10, 10, 10, 10,20,15,15,15,12,12 };

                objGenerateExcelRpt.Create(file, dt, "Issue Details Report", logPath, col);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "Error: " + ex.Message;
                logger.Error("CurrentStockReport", "Excel click", "Excel click error occurred", ex, logPath);
                return;
            }

            //string basurl = ConfigurationManager.AppSettings["BASE_URL"] + "/InventoryReports/" + fileName;
            string basurl = Util.BASE_URL + pdfURL.Replace("~/", "") + "CurrentStockReport/" + fileName;

            divDownResUser.InnerHtml = "<a href='" + basurl + "' title='click here to download Item Wise Stock Report'>click here</a> to download Issue Details Report";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void PopulateGridView(DateTime startDate, DateTime endDate)
    {
        try
        {
            InventoryDetailsDAO idd = new InventoryDetailsDAO();
            DataTable dt = idd.GetCurrentStockReport(startDate, endDate, cnxnString, logPath);
            ViewState["dt"] = dt;

            //lnkExportExcel_Click(null, null);

            gvIssueDetails.DataSource = dt;
            gvIssueDetails.DataBind();

            lnkExportExcel.Visible = true;
            lnkCancel.Visible = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private DataTable GetStockDetailsDataTable()
    {
        DataTable dt = new DataTable();

        if (ViewState["dt"] != null)
        {
            dt = ViewState["dt"] as DataTable;
        }

        return dt;
    }
}