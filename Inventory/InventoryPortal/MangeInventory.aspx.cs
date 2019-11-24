using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.DAO;
using ITM.LogManager;


public partial class MangeInventory : Page
{
    Logger logger = new Logger();

    string cnxnString;
    string logPath;
    string configFilePath;
    string pdfFolderPath;

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
            }

            if (!IsPostBack)
            {
                PopulateDepartmentType();
                BindGridView();
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }
    public void PopulateDepartmentType()
    {
        try
        {
            DepartmentDetailsDAO exam = new DepartmentDetailsDAO();
            ddlDepartment.Items.Clear();

            ddlDepartment.DataSource = exam.GetDepartmentList(cnxnString, logPath);

            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new ListItem("Select Department Name", "0", true));
            ddlDepartment.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            logger.Error("ManageInventory", "PopulateDepartment", "Error Occured While Populating data in grid", ex, logPath);
            throw new Exception("");
        }
    }
    public void BindGridView()
    {
        try
        {
            InventoryDetailsDAO inventoryclass = new InventoryDetailsDAO();
            gridedit.DataSource = inventoryclass.GetInventoryList(cnxnString, logPath);
            gridedit.DataBind();

            InventoryPanel.Update();
        }
        catch (Exception ex)
        {
            logger.Error("ManageInventory", "BindGridView", "Error Occured While Populating data in grid", ex, logPath);
            throw new Exception("11677");
        }
    }
    protected void gridedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Remove")
            {

                string id = e.CommandArgument.ToString();
                InventoryDetailsDAO inventorydetails = new InventoryDetailsDAO();

                inventorydetails.RemoveInventoryDetails(int.Parse(id), cnxnString, logPath);

                BindGridView();
                divupdatestatus.Style["color"] = "green";

            }
            if (e.CommandName == "ViewUpdate")
            {
                logger.Debug("ManageInventory", "gridedit_RowCommand", "Displaying  User Details", logPath);

                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                txtid.Text = Convert.ToString(((Label)gvrow.FindControl("gvlblId")).Text);
                int intid = int.Parse(txtid.Text);

                InventoryDetailsDAO inventoryDAO = new InventoryDetailsDAO();
                InventoryDetails inventory = inventoryDAO.GetInventoryDetailById(intid, cnxnString, logPath);

                if (inventory != null)
                {
                    ddlDepartment.SelectedValue = inventory.DepartmentId.ToString();
                    txtName.Text = inventory.Name;
                    txtSpecifiation.Text = inventory.Specification;
                    txtPrice.Text = inventory.Price.ToString();
                    txtQuantity.Text = inventory.Quantity.ToString();
                    txtModelNo.Text = inventory.ModelNo;
                    txtRemarks.Text = inventory.Remarks;
                    txtVendor.Text = inventory.Vendor;
                    txtManufacturer.Text = inventory.Manufacturer;
                    txtRecQuantity.Text = inventory.QuantityRecommended.ToString();
                    ddlCategory.SelectedValue = inventory.Category;

                    divgrid.Visible = false;
                    InventoryDetailsForm.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error("ManageInventory", "GridView_RowCommnad", "Error Occured While Populating data in grid", ex, logPath);
            throw new Exception("");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            InventoryDetailsDAO updateinventory = new InventoryDetailsDAO();
            InventoryDetails inventorydetails = new InventoryDetails();
            inventorydetails.Id = Convert.ToInt32(txtid.Text);
            inventorydetails.DepartmentId = int.Parse(ddlDepartment.SelectedValue);
            inventorydetails.Name = txtName.Text;
            inventorydetails.Specification = txtSpecifiation.Text;
            inventorydetails.Quantity = Convert.ToInt32(txtQuantity.Text);
            inventorydetails.Price = txtPrice.Text;
            inventorydetails.Vendor = txtVendor.Text;
            inventorydetails.Remarks = txtRemarks.Text;
            inventorydetails.ModelNo = txtModelNo.Text;
            inventorydetails.Manufacturer = txtManufacturer.Text;
            inventorydetails.QuantityRecommended = Convert.ToInt32(txtRecQuantity.Text);
            inventorydetails.Category = ddlCategory.SelectedValue;

            updateinventory.ChangeInventoryDetails(inventorydetails, cnxnString, logPath);

            BindGridView();
            divresult.Visible = true;
            clearfields();
            divgrid.Visible = true;
            InventoryDetailsForm.Visible = false;

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>SetGridPageStyle('true');</script>", false);
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
            txtRecQuantity.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            ddlCategory.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lbtnGoBack_Click(object sender, EventArgs e)
    {
        InventoryDetailsForm.Visible = false;
        divgrid.Visible = true;
    }

    protected void gridedit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridedit.PageIndex = e.NewPageIndex;
            BindGridView();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>SetGridPageStyle('true');</script>", false);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gridedit_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}