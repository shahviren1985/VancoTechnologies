using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.DAO;
using System.IO;
using System.Configuration;
using ITM.LogManager;
using System.Data;
using ITM.DAOBase;
using System.Data.Common;
using ITM.ExcelGenerator;

public partial class PurchaseOrderReport : PageBase
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
            throw ex;
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
            List<InventoryDetails> inventoryDetails = idd.GetInventoryListForPurchase(department, ddlCategory.SelectedValue, cnxnString, logPath);

            DataTable dt = new DataTable();

            dt.Columns.Add("Department");
            dt.Columns.Add("Category");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Manufacturer");
            dt.Columns.Add("Specification");
            dt.Columns.Add("OrderQuantity");
            dt.Columns.Add("Price per Unit");
            dt.Columns.Add("Total Price");
            dt.Columns.Add("Remark");

            if (inventoryDetails != null)
            {
                foreach (InventoryDetails inventory in inventoryDetails)
                {
                    DataRow row = dt.NewRow();
                    inventory.ModelNo = string.IsNullOrEmpty(inventory.ModelNo) ? "0" : inventory.ModelNo;
                    inventory.Price = string.IsNullOrEmpty(inventory.Price) ? "0" : inventory.Price;

                    inventory.ModelNo = Math.Ceiling(decimal.Parse(inventory.ModelNo)).ToString();
                    inventory.Price = Math.Ceiling(decimal.Parse(inventory.Price)).ToString();

                    row[0] = inventory.DepartmentName;
                    row[1] = inventory.Category;
                    row[2] = inventory.Name;
                    row[3] = inventory.Manufacturer;
                    row[4] = inventory.Specification;
                    row[5] = inventory.QuantityRecommended;
                    row[6] = Math.Ceiling(decimal.Parse(inventory.ModelNo));
                    row[7] = Math.Ceiling(decimal.Parse(inventory.Price));
                    row[8] = inventory.Remarks;

                    dt.Rows.Add(row);
                }
            }


            ViewState["dt"] = dt;

            gvInventory.DataSource = inventoryDetails;
            gvInventory.DataBind();

            if (inventoryDetails != null && inventoryDetails.Count > 0)
            {
                lnkExportExcel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue == "-1")
            {
                Status.InnerText = "Please select department name.";
                Status.Style["color"] = "red";
                ddlCategory.SelectedValue = "-1";
                return;
            }

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

        #region Commneted code
        /*dt.Columns.Add("Department");
        dt.Columns.Add("Category");
        dt.Columns.Add("ItemName");
        dt.Columns.Add("Manufacturer");
        dt.Columns.Add("Specification");
        dt.Columns.Add("OrderQuantity");
        dt.Columns.Add("Price");
        dt.Columns.Add("Remark");

        string cmdText = "select DepartmentName, Category, Name, Manufacturer, Specification, (QuantityRecommended - Quantity) as Quantity, Price from inventorydetails i inner join department d on i.departmentid=d.departmentid where (quantityrecommended - quantity > 0)";

        DbDataReader reader = db.Select(cmdText, cnxnString, logPath);

        try
        {
            DataRow row;
            if (reader != null)
            {

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

                if (!reader.IsClosed)
                    reader.Close();
            }
        }
        catch (Exception ex)
        {
            logger.Error("PurchaseOrderReport", "GetStockDataTable", ex.Message, ex, logPath);
            throw ex;
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }
        }
        */
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

            fileName = "purchase-order-report-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            file = Path.Combine(path, fileName);

            try
            {
                DataTable dt = GetStockDataTable();

                GenerateExcelReports objGenerateExcelRpt = new GenerateExcelReports();

                objGenerateExcelRpt.Create(file, dt, "Purchase Order Report", logPath);
            }
            catch (Exception ex)
            {
                return;
            }

            //string basurl = pdfURL + "InventoryReports/" + fileName;
            string basurl = Util.BASE_URL + pdfURL.Replace("~/", "") + "InventoryReports/" + fileName;

            divDownResUser.InnerHtml = "<a href='" + basurl + "' title='click here to download Purchase Order Report'>click here</a> to download Purchase Order Report";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}