using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.LogManager;
using ITM.Courses.DAO;

public partial class Staff_ManageCourse : PageBase
{
    Logger logger = new Logger();
    CourseDetailsDAO objCourseDAO = new CourseDetailsDAO();
    string logPath;
    string cnxnString;
    string configPath;
    int staffId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Success.Visible = false;
        errorSummary.Visible = false;

        try
        {
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
                staffId = Convert.ToInt32(Session["UserId"]);
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
            //gvCourseDetails.DataSource = new CourseDetailsDAO().GetAllCoursesDetails(cnxnString, logPath);
            gvCourseDetails.DataSource = new CourseDetailsDAO().GetAllCoursesByStaff(staffId, cnxnString, logPath);
            gvCourseDetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAddUpdate_Click(object s, EventArgs e)
    {
        try
        {
            string message = "";

            if (string.IsNullOrEmpty(txtCourseName.Text))
            {
                errorSummary.InnerHtml = "Please enter course-name";
                errorSummary.Visible = true;
                return;
            }


            if (btnAddUpdate.Text == "Add New Course") //add new course
            {
                objCourseDAO.AddCourse(txtCourseName.Text, DateTime.Now, staffId, cnxnString, logPath);
                message = "New course added successfully.";
            }
            else if (btnAddUpdate.Text == "Save Changes") //edit existing course
            {
                objCourseDAO.UpdateCourse(int.Parse(hfCourseId.Value), txtCourseName.Text, DateTime.Now, staffId, cnxnString, logPath);

                btnAddUpdate.Text = "Add New Course";
                message = "Course updated successfully.";
            }

            PopulateCourses();

            txtCourseName.Text = string.Empty;
            Success.InnerHtml = message;
            Success.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvCourseDetails_PageIndexChanging(object s, GridViewPageEventArgs e)
    {
        try
        {
            gvCourseDetails.PageIndex = e.NewPageIndex;
            PopulateCourses();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvCourseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                hfCourseId.Value = e.CommandArgument.ToString();
                CourseDetail course = objCourseDAO.GetCourseByCourseId(int.Parse(e.CommandArgument.ToString()), cnxnString, logPath);
                if (course != null)
                {
                    txtCourseName.Text = course.CourseName;
                    btnAddUpdate.Text = "Save Changes";
                }
            }
            else if (e.CommandName == "DeleteDetails")
            {
                int courseId = Convert.ToInt32(e.CommandArgument);

                new CourseDetailsDAO().DeleteCourse(courseId, cnxnString, logPath);
                Success.InnerText = "Course deleted successfully.";
                Success.Visible = true;

                PopulateCourses();
            }
        }
        catch (Exception ex)
        {
            if (ex.Source == "MySql.Data" || ex.Message.ToLower().Contains("foreign key"))
            {
                errorSummary.InnerText = "Error: there are some chapters refer to this course, so cannot delete it.";
                errorSummary.Visible = true;
            }
            else
            {
                throw ex;
            }
        }
    }

    protected void gvCourseDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onMouseOver", "Highlight(this)");

            e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");
        }
    }
}