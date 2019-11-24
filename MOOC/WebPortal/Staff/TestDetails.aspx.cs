using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public partial class Staff_TestDetails : PageBase
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
                PopulateCourse();
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
    #endregion

    protected void ddlCourse_SelectedIndexChanged(object s, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedValue != "0")
            {
                gvChapterDetails.DataSource = new CourseTestsDAO().GetAllCourseTestsByCourseId(int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);
                gvChapterDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAdd_Click(object s, EventArgs e)
    {
        try
        {
            //if (ddlCourse.SelectedValue != "0")
            {
                string url = "ManageTest.aspx?mode=add&courseid=" + ddlCourse.SelectedValue;
                Response.Redirect(url, false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvChapterDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int courseTestId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit1")
            {
                string url = "ManageTest.aspx?mode=edit&courseid=" + ddlCourse.SelectedValue + "&testid=" + e.CommandArgument.ToString();
                Response.Redirect(url, false);
            }
            else if (e.CommandName == "Delete1")
            {
                new CourseTestsDAO().DeleteCourseTest(courseTestId, cnxnString, logPath);
                Success.InnerText = "Test deleted successfully.";
                Success.Visible = true;

                ddlCourse_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            if (ex.Source == "MySql.Data" || ex.Message.ToLower().Contains("foreign key"))
            {
                errorSummary.InnerText = "Error: there are some questoins refer to this test, so cannot delete it.";
                errorSummary.Visible = true;
            }
            else
            {
                throw ex;
            }
        }
    }

    protected void gvChapterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "Highlight(this)");
                e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");

                Label chapters = e.Row.FindControl("gvlblChapters") as Label;

                if (chapters != null && !string.IsNullOrEmpty(chapters.Text))
                {
                    List<ChapterDetails> lsChapters = new ChapterDetailsDAO().GetChaptersByIds(chapters.Text, cnxnString, logPath);
                    if (lsChapters != null)
                    {
                        string sChapters = string.Empty;

                        foreach (ChapterDetails obj in lsChapters)
                        {
                            sChapters += obj.Title + ", ";
                        }
                        if (sChapters.Length > 1)
                        {
                            sChapters = sChapters.Trim();
                            sChapters = sChapters.Remove(sChapters.Length - 1, 1);
                            //sChapters = sChapters.Remove(sChapters.LastIndexOf(','), 1);
                        }

                        chapters.Text = sChapters;
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