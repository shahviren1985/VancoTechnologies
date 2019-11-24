using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.DAO;
using ITM.LogManager;
using System.IO;

public partial class InventoryIssue : Page
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
                txtIssuerName.Text = Convert.ToString(Session["UserName"]);
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
            ddlTeacher.Items.Clear();
            ddlInventoryItem.Items.Clear();
            ddlInventoryItem.Items.Add(new ListItem("--Select Inventory Item--", "0", true));
            ddlTeacher.Items.Add(new ListItem("--Select Issue to Teacher--", "0", true));

            ddlDepartment.DataSource = exam.GetDepartmentList(cnxnString, logPath);

            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0", true));
            ddlDepartment.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            logger.Error("Admin_StaffDetails", "PopulateDepartmentType", "Error Occured While Populating department type list", ex, logPath);
            throw new Exception("11878");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            InventoryIssuesDAO inventory = new InventoryIssuesDAO();

            inventory.CreateInventoryIssue(int.Parse(ddlDepartment.SelectedItem.Value),
                                        int.Parse(ddlInventoryItem.SelectedValue.Split('|')[0]),
                                        ddlTeacher.SelectedItem.Value.ToString(),
                                        int.Parse(txtQuantity.Text),
                                        0, // int.Parse(Convert.ToString(Session["CollegeId"]))
                                        txtIssuerName.Text,
                                        DateTime.Parse(txtDateOfIssue.Text),
                                        cnxnString,
                                        logPath
                                        );
            clearfields();
            divresult.Visible = true;
        }
        catch (Exception ex)
        {
            divError.InnerText = "Error in Save : " + ex.Message;
            divError.Visible = true;
            throw;
        }

    }
    public void clearfields()
    {
        try
        {
            PopulateDepartmentType();
            txtAvailableQuantity.Text = "";
            txtQuantity.Text = "";
            txtDateOfIssue.Text = "";
            txtIssuerName.Text = Convert.ToString(Session["UserName"]);
        }
        catch (Exception ex)
        {
            throw;
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


            ddlTeacher.Items.Clear();
            ddlTeacher.DataSource = ReadTeachers(ddlDepartment.SelectedItem.Text);
            ddlTeacher.DataBind();

            ddlTeacher.Items.Insert(0, new ListItem("--Select Issue to Teacher--", "0", true));
            ddlTeacher.SelectedValue = "0";

        }
        catch (Exception ex)
        {
            logger.Error("InventoryIssue", "ddlDepartment_SelectedIndexChanged", "Error Occured While Populating Inventory list", ex, logPath);
            throw new Exception("11878");
        }
    }

    protected void ddlInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlInventoryItem.SelectedValue.Split('|').Count() > 1)
        {
            var qty = ddlInventoryItem.SelectedValue.Split('|')[1];
            txtAvailableQuantity.Text = qty;
            CompareValidator1.ValueToCompare = qty;
        }
    }

    public List<string> ReadTeachers(string deptName)
    {
        List<TeachersList> teachersList = new List<TeachersList>();
        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/data/teachers.json")))
        {
            String JSONtxt = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/teachers.json"));
            teachersList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TeachersList>>(JSONtxt);
            if (teachersList != null)
            {
                TeachersList teacher = teachersList.Where(m => m.department == deptName).FirstOrDefault();
                if (teacher!= null)
                    return teacher.staff;
            }
        }
        return null;
    }

    public class TeachersList
    {
        public string department { get; set; }
        public List<string> staff { get; set; }
    }
}