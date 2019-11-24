using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.DAO;
using ITM.LogManager;
using System.IO;
using Newtonsoft.Json;
using System.Data;

public partial class MangeInventory : Page
{
    Logger logger = new Logger();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            if (!IsPostBack)
            {

                ddlDepartment.DataSource = SelectAllDepartment();

                ddlDepartment.DataBind();
                ddlDepartment.Items.Add(new ListItem("-Select Department-", "-1", true));
                ddlDepartment.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }
    private void PopulateGridView()
    {
        try
        {
            if (ddlDepartment.SelectedValue == "-1" || ddlDepartment.SelectedValue == "0")
            {
                Status.InnerText = "Please select Category.";
                Status.Style["color"] = "red";
                DataTable dt = new DataTable();
                gvTeacherDetails.DataSource = dt;
                gvTeacherDetails.DataBind();
            }
            else
            {
                List<TeacherClass> items = new List<TeacherClass>();
                items = GetTeacherListFromJson();
                List<string> dept = new List<string>();
                foreach (var dpt in items.Where(x => x.department == ddlDepartment.SelectedValue))
                {
                    foreach (var teacher in dpt.staff)
                    {
                        dept.Add(teacher);
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("TeacherName");
                dt.Columns.Add("DepartmentName");


                foreach (var teacher in dept)
                {
                    DataRow row = dt.NewRow();

                    row[0] = teacher;
                    row[1] = ddlDepartment.SelectedValue;
                    dt.Rows.Add(row);
                }

                gvTeacherDetails.DataSource = dt;
                gvTeacherDetails.DataBind();
                ViewState["dt"] = dt;
                TecherPanel.Update();
            }

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
            if (ddlDepartment.SelectedValue == "-1")
            {
                Status.InnerText = "Please select department name.";
                Status.Style["color"] = "red";                
            }
            PopulateGridView();
            Status.InnerText = "";
            divresult.InnerText = "";
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private List<string> SelectAllDepartment()
    {
        try
        {
            List<TeacherClass> items = new List<TeacherClass>();
            items = GetTeacherListFromJson();
            List<string> dept = new List<string>();
            foreach (var dpt in items)
            {
                dept.Add(dpt.department);
            }
            return dept;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public class TeacherClass
    {
        public string department { get; set; }
        public List<string> staff { get; set; }
    }





    protected void gvTeacherDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTeacherDetails.EditIndex = -1;
        PopulateGridView();
    }

    protected void gvTeacherDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //gvTeacherDetails.EditIndex = e.NewEditIndex;
            List<TeacherClass> items = new List<TeacherClass>();
            items = GetTeacherListFromJson();
            List<string> dept = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                //if (items[i].department == department)
                //{
                //    items[i].staff.Remove(teachername);
                //    break;
                //}
            }
            UpdateJsonFile(items);
            divresult.InnerText = "Record Deleted Successfully";
            divresult.Visible = true;
            PopulateGridView();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void gvTeacherDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTeacherDetails.EditIndex = e.NewEditIndex;
        Session["EditIndex"] = e.NewEditIndex;
        divresult.InnerText = "";
        divresult.Visible = false;
        PopulateGridView();

    }

    protected void gvTeacherDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            string UpdatedTeacherName = ((TextBox)gvTeacherDetails.Rows[e.RowIndex].FindControl("txtEditTeacherName")).Text;
            int EditIndex = Convert.ToInt32(Session["EditIndex"]);
            List<TeacherClass> items = new List<TeacherClass>();
            items = GetTeacherListFromJson();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].department == ddlDepartment.SelectedValue)
                {
                    items[i].staff[EditIndex] = UpdatedTeacherName;
                }
            }
            UpdateJsonFile(items);
            gvTeacherDetails.EditIndex = -1;
            divresult.InnerText = "Record Updated Successfully";
            divresult.Visible = true;
            PopulateGridView();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        LinkButton lnkRemove = (LinkButton)sender;
        string RemoveTeacherName = lnkRemove.CommandArgument;
        try
        {
            List<TeacherClass> items = new List<TeacherClass>();
            items = GetTeacherListFromJson();
            List<string> dept = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].department == ddlDepartment.SelectedValue)
                {
                    items[i].staff.Remove(RemoveTeacherName);
                    break;
                }
            }
            UpdateJsonFile(items);
            divresult.InnerText = "Record Deleted Successfully";
            divresult.Visible = true;
            PopulateGridView();
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        try
        {
            List<TeacherClass> items = new List<TeacherClass>();
            items = GetTeacherListFromJson();
            string TeacherValue = (gvTeacherDetails.FooterRow.FindControl("txtTeacherName") as TextBox).Text;
            var result = items.FirstOrDefault(s => s.department == ddlDepartment.SelectedValue);
            result.staff.Add(TeacherValue);
            UpdateJsonFile(items);
            (gvTeacherDetails.FooterRow.FindControl("txtTeacherName") as TextBox).Text = "";
            divresult.InnerText = "Record Added Successfully";
            divresult.Visible = true;
            PopulateGridView();
            TecherPanel.Update();
        }
        catch (Exception ex)
        {
            divresult.InnerText = "Error in Save : " + ex.Message;
            divresult.Visible = true;
            throw;
        }
    }
    public List<TeacherClass> GetTeacherListFromJson()
    {
        string startupPath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/teachers.json");
        List<TeacherClass> items = new List<TeacherClass>();
        using (StreamReader r = new StreamReader(startupPath))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<TeacherClass>>(json);
        }
        return items;
    }
    public void UpdateJsonFile(List<TeacherClass> data)
    {
        string startupPath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/teachers.json");
        string jsonfinal = JsonConvert.SerializeObject(data,Formatting.Indented);
        System.IO.File.WriteAllText(startupPath, jsonfinal);
    }
}


