using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.LogManager;
using ITM.Courses.DAO;

public partial class Admin_ManageOnlineTools : System.Web.UI.Page
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            errorSummary.Visible = false;
            Success.Visible = false;

            if (Session["ConnectionString"] == null)
            {
                Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Server.MapPath(Session["LogFilePath"].ToString());
                configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());                
            }

            if (!IsPostBack)
            {
                PopulateCourses();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void PopulateCourses()
    {
        try
        {
            CourseDetailsDAO Course = new CourseDetailsDAO();
            ddlCourse.DataSource = Course.GetAllCoursesDetails(cnxnString, logPath);
            ddlCourse.DataTextField = "CourseName";
            ddlCourse.DataValueField = "Id";
            ddlCourse.DataBind();
            ddlCourse.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Course--", "0", true));
            ddlCourse.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlCourse_Change(object s, EventArgs e)
    {
        try
        {
            AddChapter.Visible = true;

            if (ddlCourse.SelectedValue != "0")
            {
                gvOnlineTools.DataSource = new OnlineToolsDAO().GetAllOnlineToolsByCourseId(int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);
                gvOnlineTools.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvOnlineTools_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label desc = e.Row.FindControl("gvlblDescription") as Label;
                if (desc != null && !string.IsNullOrEmpty(desc.Text))
                {
                    if (desc.Text.Length > 40)
                    {
                        desc.ToolTip = desc.Text;
                        desc.Text = desc.Text.Substring(0, 45) + "...";
                    }
                }
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    protected void gvOnlineTools_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                string url = Util.BASE_URL + "Admin/AddUpdateOnlineTools.aspx?mode=edit&courseid=" + ddlCourse.SelectedValue + "&toolid=" + e.CommandArgument;
                Response.Redirect(url, false);
            }
            else if (e.CommandName == "DeleteDetails")
            {
 
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    protected void btnAddNewTool_Click(object s, EventArgs e)
    {
        try
        {
            string url = Util.BASE_URL + "Admin/AddUpdateOnlineTools.aspx?mode=add&courseid=" + ddlCourse.SelectedValue;
            Response.Redirect(url, false);
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }
}