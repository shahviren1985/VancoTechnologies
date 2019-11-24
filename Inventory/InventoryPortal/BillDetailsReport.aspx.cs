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

public partial class BillDetailsReport : System.Web.UI.Page
{
    Logger logger = new Logger();

    string cnxnString;
    string logPath;
    string configFilePath;
    string pdfFolderPath;
    string pdfURL;

    protected void Page_Load(object sender, EventArgs e)
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

        if (!IsPostBack)
        {
            PopulateDepartmentType();
        }

    }

    private void PopulateDepartmentType()
    {
        try
        {
            DepartmentDetailsDAO exam = new DepartmentDetailsDAO();
            ddlDepartment.Items.Clear();

            List<DepartmentDetails> deptList = exam.GetDepartmentList(cnxnString, logPath);
            deptList.Add(new DepartmentDetails { DepartmentName = "All Departments", History = string.Empty, ShortForm = "All", DepartmentId = 0 });

            ddlDepartment.DataSource = deptList;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new ListItem("-Select Department-", "-1", true));
            ddlDepartment.SelectedValue = "-1";
        }
        catch (Exception ex)
        {
            logger.Error("Admin_StaffDetails", "PopulateDepartmentType", "Error Occured While Populating department type list", ex, logPath);
            throw new Exception("11878");
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateGridView();

        //gvBillDetails.DataSource = null;
        //gvBillDetails.DataBind();
    }

    public void PopulateGridView()
    {
        if (ddlDepartment.SelectedValue == "-1")
        {
            Status.InnerText = "Please select Department.";
            Status.Style["color"] = "red";
            return;
        }

        try
        {
            InventoryBillsDAO idd = new InventoryBillsDAO();
            string department = ddlDepartment.SelectedValue == "0" ? "" : ddlDepartment.SelectedValue;
            List<ITM.DAO.InventoryBills> inventoryBills = idd.GetInventoryBills(department, cnxnString, logPath);

            gvBillDetails.DataSource = inventoryBills;
            gvBillDetails.DataBind();

            if (inventoryBills != null && inventoryBills.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Department");
                dt.Columns.Add("InventoryName");
                dt.Columns.Add("BillNo");
                dt.Columns.Add("Vendor");
                dt.Columns.Add("ReceivedQuantity");
                dt.Columns.Add("BillDate");
                dt.Columns.Add("PurchasedPrice");

                foreach (ITM.DAO.InventoryBills inventory in inventoryBills)
                {
                    DataRow row = dt.NewRow();

                    row[0] = inventory.DepartmentName;
                    row[1] = inventory.InventoryName;
                    row[2] = inventory.BillNo;
                    row[3] = inventory.VendorName;
                    row[4] = inventory.ReceivedQuantity;
                    row[5] = inventory.BillDate;
                    row[6] = inventory.PurchasedPrice;
                    dt.Rows.Add(row);
                }

                ViewState["dt"] = dt;

                lnkExportExcel.Visible = true;
            }
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
            string path = Path.Combine(pdfFolderPath, "BillDetailsReport");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //fileName = "CurrentStockReport_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.ToString("HHmmss") + ".xlsx";
            fileName = "bill-details-report-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            file = Path.Combine(path, fileName);

            logger.Debug("BillDetailsReport", "GetBillDetailsDataTable", "File name=" + fileName, logPath);

            try
            {
                logger.Debug("BillDetailsReport", "Excel click", "Preparing Datatable", logPath);

                DataTable dt = GetBillDetailsDataTable();

                logger.Debug("BillDetailsReport", "Excel click", "Total Rows=" + dt.Rows.Count, logPath);

                GenerateExcelReports objGenerateExcelRpt = new GenerateExcelReports();

                objGenerateExcelRpt.Create(file, dt, "Bill Details Report", logPath);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "Error: " + ex.Message;
                logger.Error("BillDetailsReport", "Excel click", "Excel click error occurred", ex, logPath);
                return;
            }

            //string basurl = ConfigurationManager.AppSettings["BASE_URL"] + "/InventoryReports/" + fileName;
            string basurl = Util.BASE_URL + pdfURL.Replace("~/", "") + "BillDetailsReport/" + fileName;

            divDownResUser.InnerHtml = "<a href='" + basurl + "' title='click here to download Bill Details Report'>click here</a> to download Bill Details Report";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private DataTable GetBillDetailsDataTable()
    {
        DataTable dt = new DataTable();

        if (ViewState["dt"] != null)
        {
            dt = ViewState["dt"] as DataTable;
        }

        return dt;
     }

}