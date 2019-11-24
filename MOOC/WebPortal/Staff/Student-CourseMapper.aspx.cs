using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public partial class Staff_Student_CourseMapper : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    List<StudentCourseMapper> studentCourseMapper = new List<StudentCourseMapper>();

    protected void Page_Load(object sender, EventArgs e)
    {
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
            }

            if (!IsPostBack)
            {
                PopulateCourse();
                PopulateStudentCourses();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Populate Dropdowns
    private void PopulateCourse()
    {
        try
        {
            CourseDetailsDAO Course = new CourseDetailsDAO();
            ddlCourse.DataSource = Course.GetAllCoursesByStaff(int.Parse(Session["UserId"].ToString()), cnxnString, logPath);
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

    private void PopulateStudentCourses()
    {
        try
        {
            ddlStream.DataSource = new UserDetailsDAO().GetUniqueCoursesList(cnxnString, logPath);
            ddlStream.DataTextField = "Course";
            ddlStream.DataValueField = "Course";
            ddlStream.DataBind();

            ddlStream.Items.Add(new ListItem("--Stream--", "0"));
            ddlStream.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    protected void PopulateStudentsByStream()
    {
        try
        {
            if (ddlStream.SelectedValue != "0" && ddlCourse.SelectedValue != "0")
            {
                studentCourseMapper = new StudentCourseMapperDAO().GetStudentCourseMapperByCourseId(int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);

                List<UserDetails> users = new UserDetailsDAO().GetStudentByCourse(ddlStream.SelectedValue, cnxnString, logPath);
                gvUserDetails.DataSource = users;
                gvUserDetails.DataBind();

                ViewState["Users"] = users;

                if (users != null && users.Count > 0)
                {
                    btnMap.Visible = true;
                }
            }
            else
            {
                errorSummary.InnerText = "Please select course or stream.";
                errorSummary.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlStream_OnChange(object s, EventArgs e)
    {
        try
        {
            PopulateStudentsByStream();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void gvUserDetails_PageIndexChanging(object s, GridViewPageEventArgs e)
    {
        if (ViewState["Users"] != null)
        {
            List<UserDetails> users = (List<UserDetails>)ViewState["Users"];

            gvUserDetails.PageIndex = e.NewPageIndex;
            gvUserDetails.DataSource = users;
            gvUserDetails.DataBind();
        }
    }

    protected void btnMap_Click(object s, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedValue == "0")
            {
                errorSummary.InnerHtml = "Please select course to mapp students.";
                errorSummary.Visible = true;
                return;
            }

            StudentCourseMapperDAO objMapper = new StudentCourseMapperDAO();

            bool isUserSelected = false;
            List<int> userIds = new List<int>();

            List<UserDetails> users = new List<UserDetails>();

            if (ViewState["Users"] != null)
            {
                users = (List<UserDetails>)ViewState["Users"];
            }

            foreach (GridViewRow row in gvUserDetails.Rows)
            {
                CheckBox chkBox = row.FindControl("gvchkSelectStudent") as CheckBox;

                if (chkBox != null && chkBox.Checked)
                {
                    Label lblId = row.FindControl("gvlblId") as Label;
                    if (lblId != null)
                    {
                        isUserSelected = true;

                        int userId = Convert.ToInt32(lblId.Text);
                        userIds.Add(userId);
                    }
                }
            }

            if (isUserSelected && userIds.Count > 0)
            {
                foreach (int id in userIds)
                {
                    StudentCourseMapper mapper = objMapper.GetStudentCourseMapperRecordsByCourseUserId(id, int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);
                    if (mapper == null)
                        objMapper.AddStudentCourseMapper(id, int.Parse(ddlCourse.SelectedValue), true, cnxnString, logPath);
                }
            }
            else
            {
                foreach (UserDetails user in users)
                {
                    StudentCourseMapper mapper = objMapper.GetStudentCourseMapperRecordsByCourseUserId(user.Id, int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);
                    if (mapper == null)
                        objMapper.AddStudentCourseMapper(user.Id, int.Parse(ddlCourse.SelectedValue), true, cnxnString, logPath);
                }
            }

            errorSummary.InnerHtml = "selected students mapped successfully.";
            errorSummary.Visible = true;
            errorSummary.Style["background"] = "lightgreen";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "Highlight(this)");
                e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");

                Label lblId = e.Row.FindControl("gvlblId") as Label;
                CheckBox chkboxIsMapped = e.Row.FindControl("gvchkSelectStudent") as CheckBox;

                if (lblId != null && chkboxIsMapped != null && studentCourseMapper != null)
                {
                    int studentId = Convert.ToInt32(lblId.Text);
                    StudentCourseMapper obj = studentCourseMapper.Find(ss => ss.UserId == studentId);
                    if (obj != null)
                    {
                        chkboxIsMapped.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}