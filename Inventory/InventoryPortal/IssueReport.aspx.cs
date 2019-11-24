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

public partial class IssueReport : System.Web.UI.Page
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
            PopulateGridView(DateTime.Parse(txtDateOfIssue.Text), DateTime.Parse(txtIssueEndDate.Text));
        }
        catch (Exception ex)
        {
            
            throw;
        }

    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string file;
            string fileName = string.Empty;
            //string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"]) + "InventoryReports";
            string path = Path.Combine(pdfFolderPath, "IssueDetailsReport");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //fileName = "CurrentStockReport_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.ToString("HHmmss") + ".xlsx";
            fileName = "issue-details-report-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            file = Path.Combine(path, fileName);

            logger.Debug("IssueDetailsReport", "GetIssueDetailsDataTable", "File name=" + fileName, logPath);

            try
            {
                logger.Debug("IssueDetailsReport", "Excel click", "Preparing Datatable", logPath);

                DataTable dt = GetIssueDetailsDataTable();

                logger.Debug("IssueDetailsReport", "Excel click", "Total Rows=" + dt.Rows.Count, logPath);

                GenerateExcelReports objGenerateExcelRpt = new GenerateExcelReports();

                objGenerateExcelRpt.Create(file, dt, "Issue Details Report", logPath);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "Error: " + ex.Message;
                logger.Error("IssueDetailsReport", "Excel click", "Excel click error occurred", ex, logPath);
                return;
            }

            //string basurl = ConfigurationManager.AppSettings["BASE_URL"] + "/InventoryReports/" + fileName;
            string basurl = Util.BASE_URL + pdfURL.Replace("~/", "") + "IssueDetailsReport/" + fileName;

            divDownResUser.InnerHtml = "<a href='" + basurl + "' title='click here to download Issue Details Report'>click here</a> to download Issue Details Report";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void PopulateGridView(DateTime startDate,DateTime endDate)
    {
        try
        {
            InventoryIssuesDAO idd = new InventoryIssuesDAO();
            List<InventoryIssues> inventoryIssues = idd.GetIssueList(startDate, endDate, cnxnString, logPath);

            gvIssueDetails.DataSource = inventoryIssues;
            gvIssueDetails.DataBind();

            if (inventoryIssues != null && inventoryIssues.Count > 0)
            {
                int srNo = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("Serial Number");
                dt.Columns.Add("Department");
                dt.Columns.Add("Issue To Teacher Name");
                dt.Columns.Add("Inventory Item");
                dt.Columns.Add("Issue Quantity");
                dt.Columns.Add("Date of Issue");
                dt.Columns.Add("Issuer Name");

                foreach (InventoryIssues inventory in inventoryIssues)
                {
                    DataRow row = dt.NewRow();

                    row[0] = srNo;
                    row[1] = inventory.DepartmentName;
                    row[2] = inventory.TeacherName;
                    row[3] = inventory.InventoryName;
                    row[4] = inventory.IssueQuantity;
                    row[5] = inventory.IssueDate;
                    row[6] = inventory.IssuerName;
                    dt.Rows.Add(row);
                    srNo++;
                }

                ViewState["dt"] = dt;

                lnkExportExcel.Visible = true;
                lnkCancel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private DataTable GetIssueDetailsDataTable()
    {
        DataTable dt = new DataTable();

        if (ViewState["dt"] != null)
        {
            dt = ViewState["dt"] as DataTable;
        }

        return dt;
    }
}