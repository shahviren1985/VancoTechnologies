using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_ManageStoreLocations : Page, IRequiresSessionState
{
     
    private string connString = string.Empty;
     
    private string loggedInUser = string.Empty;
    private Logger logger = new Logger();
    private string logPath = string.Empty;
     
    private string role = string.Empty;
    

    public void BindGridView()
    {
        this.gvLocations.DataSource = new LocationDetailsDAO().GetLocationDetails(this.connString, this.logPath);
        this.gvLocations.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        LocationDetailsDAO sdao = new LocationDetailsDAO();
        try
        {
            sdao.AddLocationDetails(this.txtRoomNumber.Text, this.txtCupbord.Text, this.txtShelf.Text, this.txtFileName.Text, this.txtComments.Text, this.connString, this.logPath);
            this.txtRoomNumber.Text = string.Empty;
            this.txtCupbord.Text = string.Empty;
            this.txtShelf.Text = string.Empty;
            this.txtFileName.Text = string.Empty;
            this.txtComments.Text = string.Empty;
            this.Error.Style.Add("color", "green");
            this.Error.InnerHtml = "Storage location created successfully.";
            this.BindGridView();
        }
        catch (Exception exception)
        {
            this.logger.Error("ManageLocation", "btnSave_Click", exception.Message, this.logPath);
            this.Error.InnerHtml = "An Error occured while adding new storage location.";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        LocationDetailsDAO sdao = new LocationDetailsDAO();
        try
        {
            sdao.UpdateLocationDetails(int.Parse(this.hfLocationId.Value), this.txtEditRoomNumber.Text, this.txtEditCupbord.Text, this.txtEditShelf.Text, this.txtEditFileName.Text, this.txtEditComments.Text, this.connString, this.logPath);
            this.Error.InnerHtml = "User updated successfully.";
            this.Error.Style.Add("color", "green");
            this.EditForm.Visible = false;
            this.BindGridView();
        }
        catch (Exception exception)
        {
            this.logger.Error("ManageLocation", "btnUpdate_Click", exception.Message, this.logPath);
            this.Error.InnerHtml = "An Error occured while updating old storage location.";
        }
    }

    protected void gvLocations_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count >= 7)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gvLocations_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvLocations.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }

    protected void gvLocations_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "OnEdit")
            {
                this.EditForm.Visible = true;
                Location location = new LocationDetailsDAO().GetLocationDetailById(Convert.ToInt32(e.CommandArgument.ToString()), this.connString, this.logPath);
                this.txtEditRoomNumber.Text = location.RoomNumber;
                this.txtEditCupbord.Text = location.Cupboard;
                this.txtEditShelf.Text = location.Shelf;
                this.txtEditFileName.Text = location.FileName;
                this.txtEditComments.Text = location.Comments;
                this.hfLocationId.Value = location.Id.ToString();
            }
        }
        catch (Exception exception)
        {
            this.logger.Error("ManageLocation", "gvLocations_OnRowCommand", exception.Message, this.logPath);
            this.Error.InnerHtml = "An Error occured while populating old storage location.";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserName"] == null)
        {
            base.Response.Redirect("/Login.apsx", false);
        }
        else
        {
            this.role = this.Session["RoleType"].ToString();
            this.loggedInUser = this.Session["UserName"].ToString();
            this.connString = this.Session["ConnectionString"].ToString();
            this.logPath = this.Session["LogFilePath"].ToString();
            if (!base.IsPostBack)
            {
                this.BindGridView();
            }
        }
    }
}
