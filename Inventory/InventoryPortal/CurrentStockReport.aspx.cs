using ITM.DAO;
using ITM.DAOBase;
using ITM.ExcelGenerator;
using ITM.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CurrentStockReport : PageBase
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

            if (!IsPostBack)
            {
                PopulateDepartmentType();
                PopulateCategories();
            }
        }
        catch (Exception ex)
        {
            throw;
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

    public void PopulateCategories()
    {
        try
        {
            InventoryCategory ic = new InventoryCategory();
            ddlCategory.DataSource = ic.GetAllItemCategories(cnxnString, logPath);
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "Category";
            ddlCategory.DataBind();

            ddlCategory.Items.Add(new ListItem("--Select Category--", "0"));
            ddlCategory.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void PopulateGridView()
    {
        if (ddlDepartment.SelectedValue == "-1" || ddlCategory.SelectedValue == "0")
        {
            Status.InnerText = "Please select Category.";
            Status.Style["color"] = "red";
            return;
        }

        try
        {
            InventoryDetailsDAO idd = new InventoryDetailsDAO();
            string department = ddlDepartment.SelectedValue == "0" ? "" : ddlDepartment.SelectedValue;
            List<InventoryDetails> inventoryDetails = idd.GetInventoryList(department, ddlCategory.SelectedValue, cnxnString, logPath);

            gvInventory.DataSource = inventoryDetails;
            gvInventory.DataBind();

            if (inventoryDetails != null && inventoryDetails.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Department");
                dt.Columns.Add("Category");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("Manufacturer");
                dt.Columns.Add("Specification");
                dt.Columns.Add("CurrentStock");
                dt.Columns.Add("Price");
                dt.Columns.Add("Remark");

                foreach (InventoryDetails inventory in inventoryDetails)
                {
                    DataRow row = dt.NewRow();

                    row[0] = inventory.DepartmentName;
                    row[1] = inventory.Category;
                    row[2] = inventory.Name;
                    row[3] = inventory.Manufacturer;
                    row[4] = inventory.Specification;
                    row[5] = inventory.Quantity;
                    row[6] = inventory.Price;
                    row[7] = inventory.Remarks;

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

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateCategories();
        PopulateGridView();

        gvInventory.DataSource = null;
        gvInventory.DataBind();

    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue == "-1")
            {
                Status.InnerText = "Please select department name.";
                Status.Style["color"] = "red";
                ddlCategory.SelectedValue = "0";
                return;
            }
            Status.InnerText = "";
            PopulateGridView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private DataTable GetStockDataTable()
    {
        DataTable dt = new DataTable();

        if (ViewState["dt"] != null)
        {
            dt = ViewState["dt"] as DataTable;
        }

        return dt;

        #region COmmented Code
        /* Database db = new Database();
        DataTable dt = new DataTable();
        dt.Columns.Add("Department");
        dt.Columns.Add("Category");
        dt.Columns.Add("ItemName");
        dt.Columns.Add("Manufacturer");
        dt.Columns.Add("Specification");
        dt.Columns.Add("CurrentStock");
        dt.Columns.Add("Price");
        dt.Columns.Add("Remark");

        string cmdText = "select DepartmentName, Category, Name, Manufacturer, Specification, Quantity, Price from inventorydetails i inner join department d on i.departmentid=d.departmentid";
        DbDataReader reader = db.Select(cmdText, cnxnString, logPath);

        try
        {
            DataRow row;

            if (reader != null)
            {
                logger.Debug("CurrentStockReport", "GetStockDataTable", "Records found", logPath);
                while (reader.Read())
                {
                    List<string> currentStock = new List<string>();
                    row = dt.NewRow();
                    currentStock.Add(reader["DepartmentName"].ToString());
                    currentStock.Add(reader["Category"].ToString());
                    currentStock.Add(reader["Name"].ToString());
                    currentStock.Add(reader["Manufacturer"].ToString());
                    currentStock.Add(reader["Specification"].ToString());
                    currentStock.Add(reader["Quantity"].ToString());
                    currentStock.Add(reader["Price"].ToString());

                    row.ItemArray = currentStock.ToArray();
                    dt.Rows.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("CurrentStockReport", "GetStockDataTable", "error occurred", ex, logPath);
            throw;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }
        }

        return dt;*/
        #endregion
    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string file;
            string fileName = string.Empty;
            //string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"]) + "InventoryReports";
            string path = Path.Combine(pdfFolderPath, "InventoryReports");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //fileName = "CurrentStockReport_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.ToString("HHmmss") + ".xlsx";
            fileName = "current-stock-report-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            file = Path.Combine(path, fileName);

            logger.Debug("CurrentStockReport", "GetStockDataTable", "File name=" + fileName, logPath);

            try
            {
                logger.Debug("CurrentStockReport", "Excel click", "Preparing Datatable", logPath);

                DataTable dt = GetStockDataTable();

                logger.Debug("CurrentStockReport", "Excel click", "Total Rows=" + dt.Rows.Count, logPath);

                GenerateExcelReports objGenerateExcelRpt = new GenerateExcelReports();

                objGenerateExcelRpt.Create(file, dt, "Current Stock Report", logPath);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "Error: " + ex.Message;
                logger.Error("CurrentStockReport", "Excel click", "Excel click error occurred", ex, logPath);
                return;
            }

            //string basurl = ConfigurationManager.AppSettings["BASE_URL"] + "/InventoryReports/" + fileName;
            string basurl = Util.BASE_URL + pdfURL.Replace("~/", "") + "InventoryReports/" + fileName;

            divDownResUser.InnerHtml = "<a href='" + basurl + "' title='click here to download Current Stock Report'>click here</a> to download Current Stock Report";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}