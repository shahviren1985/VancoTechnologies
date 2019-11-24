using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.DAO;
using ITM.LogManager;
using System.Data;

public partial class InventoryBills : Page
{
    Logger logger = new Logger();

    string cnxnString;
    string logPath;
    string configFilePath;
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
            }

            if (!Page.IsPostBack)
            {
                PopulateDepartmentType();
                DataTable dt = new DataTable();
                dt.Columns.Add("BillNo");
                dt.Columns.Add("VendorName");
                dt.Columns.Add("BillDate");
                dt.Columns.Add("DepartmentId");
                dt.Columns.Add("DepartmentName");
                dt.Columns.Add("ItemId");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("QuantityReceived");
                dt.Columns.Add("PricePurchased");
                ViewState["dt"] = dt;
                BindGrid();
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    protected void BindGrid()
    {
        gvInventoryBill.DataSource = ViewState["dt"] as DataTable;
        gvInventoryBill.DataBind();
        DataTable dt = ViewState["dt"] as DataTable;
        if (dt.Rows.Count != 0)
        {
            btnSubmitBill.Visible = true;
        }
        else
        {
            btnSubmitBill.Visible = false;
        }
    }

    private void PopulateDepartmentType()
    {
        try
        {
            DepartmentDetailsDAO exam = new DepartmentDetailsDAO();
            ddlDepartment.Items.Clear();

            ddlDepartment.DataSource = exam.GetDepartmentList(cnxnString, logPath);

            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0", true));
            ddlDepartment.SelectedValue = "0";

            ddlInventoryItem.Items.Clear();
            ddlInventoryItem.Items.Add(new ListItem("--Select Inventory Item--", "0", true));
        }
        catch (Exception ex)
        {
            logger.Error("Admin_StaffDetails", "PopulateDepartmentType", "Error Occured While Populating department type list", ex, logPath);
            throw new Exception("11878");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            InventoryDetailsDAO exam = new InventoryDetailsDAO();
            ddlInventoryItem.Items.Clear();

            ddlInventoryItem.DataSource = exam.GetInventoryList(ddlDepartment.SelectedValue, cnxnString, logPath);

            ddlInventoryItem.DataTextField = "Name";
            ddlInventoryItem.DataValueField = "Id_Quantity";
            ddlInventoryItem.DataBind();

            ddlInventoryItem.Items.Insert(0, new ListItem("--Select Inventory Item--", "0", true));
            ddlInventoryItem.SelectedValue = "0";

        }
        catch (Exception ex)
        {
            logger.Error("InventoryIssue", "ddlDepartment_SelectedIndexChanged", "Error Occured While Populating Inventory list", ex, logPath);
            throw new Exception("11878");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {


    }
    public void clearfields()
    {
        try
        {
            PopulateDepartmentType();
            txtVendorName.Text = "";
            txtQuantity.Text = "";
            txtDateOfBill.Text = "";
            txtBillNo.Text = "";
            txtPricePurchased.Text = "";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvInventoryBill.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void OnUpdate(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        string BillNo = ((TextBox)row.Cells[1].Controls[0]).Text;
        string VendorName = ((TextBox)row.Cells[2].Controls[0]).Text;
        string BillDate = ((TextBox)row.Cells[3].Controls[0]).Text;
        string DepartmentName = ((TextBox)row.Cells[4].Controls[0]).Text;
        string ItemName = ((TextBox)row.Cells[5].Controls[0]).Text;
        string QuantityReceived = ((TextBox)row.Cells[6].Controls[0]).Text;
        string PricePurchased = ((TextBox)row.Cells[7].Controls[0]).Text;

        DataTable dt = ViewState["dt"] as DataTable;
        dt.Rows[row.RowIndex]["BillNo"] = BillNo;
        dt.Rows[row.RowIndex]["VendorName"] = VendorName;
        dt.Rows[row.RowIndex]["BillDate"] = BillDate;
        dt.Rows[row.RowIndex]["ItemName"] = ItemName;
        dt.Rows[row.RowIndex]["ItemName"] = QuantityReceived;
        dt.Rows[row.RowIndex]["PricePurchased"] = PricePurchased;
        ViewState["dt"] = dt;
        gvInventoryBill.EditIndex = -1;
        BindGrid();
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        gvInventoryBill.EditIndex = -1;
        BindGrid();
    }
    protected void OnDelete(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt = ViewState["dt"] as DataTable;
        dt.Rows.RemoveAt(row.RowIndex);
        ViewState["dt"] = dt;
        BindGrid();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["dt"] as DataTable;
        dt.Rows.Add(txtBillNo.Text,txtVendorName.Text,txtDateOfBill.Text,ddlDepartment.SelectedValue, ddlDepartment.SelectedItem.Text, ddlInventoryItem.SelectedValue, ddlInventoryItem.SelectedItem.Text, txtQuantity.Text, txtPricePurchased.Text);
        ViewState["dt"] = dt;
        BindGrid();
        ddlInventoryItem.SelectedIndex = -1;
        txtQuantity.Text = string.Empty;
        txtPricePurchased.Text = string.Empty;
        ddlInventoryItem.Focus();
    }

    protected void btnSubmitBill_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string BillNo = row["BillNo"].ToString();
                string VendorName = row["VendorName"].ToString();
                string BillDate = row["BillDate"].ToString();
                string DepartmentId = row["DepartmentId"].ToString();
                string DepartmentName = row["DepartmentName"].ToString();
                string ItemId = row["ItemId"].ToString();
                string ItemName = row["ItemName"].ToString();
                string QuantityReceived = row["QuantityReceived"].ToString();
                string PricePurchased = row["PricePurchased"].ToString();
                InventoryBillsDAO inventory = new InventoryBillsDAO();
                inventory.CreateInventoryBill(VendorName, BillNo, int.Parse(DepartmentId),
                                            int.Parse(ItemId.Split('|')[0]),
                                            int.Parse(QuantityReceived),
                                            DateTime.Parse(BillDate),
                                            PricePurchased,
                                            cnxnString,
                                            logPath
                                            );
                divresult.Visible = true;
            }

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("BillNo");
            dt2.Columns.Add("VendorName");
            dt2.Columns.Add("BillDate");
            dt2.Columns.Add("DepartmentId");
            dt2.Columns.Add("DepartmentName");
            dt2.Columns.Add("ItemId");
            dt2.Columns.Add("ItemName");
            dt2.Columns.Add("QuantityReceived");
            dt2.Columns.Add("PricePurchased");
            ViewState["dt"] = dt2;
            txtVendorName.Text = string.Empty;
            txtBillNo.Text = string.Empty;
            txtDateOfBill.Text = string.Empty;
            ddlInventoryItem.SelectedIndex = -1;
            txtQuantity.Text = string.Empty;
            txtPricePurchased.Text = string.Empty;
            clearfields();
            BindGrid();
        }
        catch (Exception ex)
        {
            divError.InnerText = "Error in Save : " + ex.Message;
            divError.Visible = true;
            throw;
        }

    }

}