using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Data;
using ITM.Courses.ExcelGenerator;
using System.IO;
using System.Configuration;

public partial class Admin_NotCompleteFinalQuiz : System.Web.UI.Page
{
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect(Util.BASE_URL + "Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Server.MapPath(Session["LogFilePath"].ToString());
                configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
                collegeName = Session["CollegeName"].ToString();
                //pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
                pdfFolderPath = Session["PDFFolderPath"].ToString();
            }

            if (!IsPostBack)
            {
                PopulateStream();
                PopulateCourse();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Populate Dropdowns
    private void PopulateStream()
    {
        try
        {
            UserDetailsDAO Course = new UserDetailsDAO();
            ddlStream.DataSource = Course.GetUniqueCoursesList(cnxnString, logPath);
            ddlStream.DataTextField = "Course";
            ddlStream.DataValueField = "Course";
            ddlStream.DataBind();
            ddlStream.Items.Add(new System.Web.UI.WebControls.ListItem("--Select Stream--", "0", true));
            ddlStream.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateCourse()
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
    #endregion

    protected void btnGenerate_Click(object s, EventArgs e)
    {
        errorSummary.Visible = false;
        try
        {
            if (ddlStream.SelectedValue == "0" || ddlCourse.SelectedValue == "0")
            {
                errorSummary.InnerHtml = "Please select course and stream.";
                errorSummary.Visible = true;
                return;
            }

            List<UserDetails> filteredUsers = new StudentReportManager().GetStudentProgressReport(ddlStream.SelectedValue, int.Parse(ddlCourse.SelectedValue), cnxnString, logPath);

            List<FinalQuizResponse> finalQuizs = new FinalQuizResponseDAO().GetAllFinalQuizResponse(cnxnString, logPath);

            List<UserDetails> newList = new List<UserDetails>();

            int counter = 0;

            foreach (UserDetails u in filteredUsers)
            {
                int count = finalQuizs.Count(fq => fq.UserId == u.Id);

                if (count == 0 || count < Convert.ToInt32(ConfigurationManager.AppSettings["FinalQuizCount"]))
                {
                    u.BatchYear = ++counter;

                    u.UserType = count.ToString();
                    newList.Add(u);
                }
            }


            if (filteredUsers != null && filteredUsers.Count > 0)
            {
                ViewState["UserList"] = newList;
                gvUserDetails.DataSource = newList;
                gvUserDetails.DataBind();
                divGenerateButton.Style["display"] = "none";
                divPrintButton.Style["display"] = "block";
            }
            else
            {
                errorSummary.InnerHtml = "Sorry! none of student completed for selected course.";
                errorSummary.Visible = true;

                gvUserDetails.DataSource = null;
                gvUserDetails.DataBind();
                divGenerateButton.Style["display"] = "block";
                divPrintButton.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnPrint_Click(object s, EventArgs e)
    {
        try
        {
            if (ViewState["UserList"] != null)
            { }
        }
        catch (Exception ex)
        { }
    }

    protected void btnGenerateExcel_Click(object s, EventArgs e)
    {
        try
        {
            if (ViewState["UserList"] != null && ddlCourse.SelectedValue != "0")
            {
                List<UserDetails> users = ViewState["UserList"] as List<UserDetails>;

                DataTable dtUser = new DataTable();
                dtUser.Columns.Add("Sr. No.");
                dtUser.Columns.Add("User Name");
                dtUser.Columns.Add("Student Name");                
                dtUser.Columns.Add("Course Name");
                dtUser.Columns.Add("Question Attempt");

                int srNo = 1;

                foreach (UserDetails user in users)
                {
                    DataRow row = dtUser.NewRow();

                    row[0] = srNo;
                    row[1] = user.UserName;
                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                    row[3] = user.Course;
                    row[4] = user.UserType;

                    dtUser.Rows.Add(row);
                    srNo++;
                }

                string fileName = "Student-not-completed-finalquiz-report-" + ddlStream.SelectedValue + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                string fileUrl = pdfFolderPath + fileName;

                pdfFolderPath = Server.MapPath(pdfFolderPath);

                if (!Directory.Exists(pdfFolderPath))
                {
                    Directory.CreateDirectory(pdfFolderPath);
                }

                new GenerateExcelReports().Create(pdfFolderPath + fileName, dtUser, "Report", logPath);

                Response.Redirect(fileUrl, false);
            }
        }
        catch (Exception ex)
        { }
    }
}