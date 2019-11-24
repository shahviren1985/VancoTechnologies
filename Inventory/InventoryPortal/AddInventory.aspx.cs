using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.DAO;
using ITM.LogManager;

public partial class AddInventory : Page
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

            ddlDepartment.DataSource = exam.GetDepartmentList(cnxnString, logPath);

            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new ListItem("--Select Department--", "0", true));
            ddlDepartment.SelectedValue = "0";
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            InventoryDetailsDAO inventory = new InventoryDetailsDAO();

            inventory.CreateInventory(int.Parse(ddlDepartment.SelectedItem.Value), 
                                        ddlCategory.SelectedValue, 
                                        txtName.Text, 
                                        txtSpecifiation.Text, 
                                        int.Parse(txtQuantity.Text),
                                        int.Parse(txtRecQuantity.Text), 
                                        txtManufacturer.Text, 
                                        txtVendor.Text,
                                        int.Parse(txtPrice.Text), 
                                        txtModelNo.Text, 
                                        txtLocation.Text, 
                                        txtRemarks.Text,
                                        cnxnString,
                                        logPath
                                        );


            clearfields();
            divresult.Visible = true;

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    public void clearfields()
    {
        try
        {

            txtRemarks.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtModelNo.Text = string.Empty;
            txtSpecifiation.Text = string.Empty;
            txtVendor.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            txtRecQuantity.Text = string.Empty;
            PopulateDepartmentType();
            ddlCategory.SelectedValue = "0";
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}