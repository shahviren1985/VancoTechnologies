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

public partial class Admin_StudentTypingStatus : PageBase
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
                PopulateCourse();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Populate Dropdowns
    private void PopulateCourse()
    {
        try
        {
            UserDetailsDAO Course = new UserDetailsDAO();
            ddlCourse.DataSource = Course.GetUniqueCoursesList(cnxnString, logPath);
            ddlCourse.DataTextField = "Course";
            ddlCourse.DataValueField = "Course";
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
            if (ddlCourse.SelectedValue == "0")
            {
                errorSummary.InnerHtml = "Please select course.";
                errorSummary.Visible = true;
                return;
            }

            List<UserDetails> filteredUsers = new StudentReportManager().GetStudentTypingStatus(ddlCourse.SelectedValue, cnxnString, logPath);

            if (filteredUsers != null && filteredUsers.Count > 0)
            {
                ViewState["UserList"] = filteredUsers;
                gvUserDetails.DataSource = filteredUsers;
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
            if (ViewState["UserList"] != null)
            {
                StudentTypingStatsDAO obj = new StudentTypingStatsDAO();

                List<UserDetails> users = ViewState["UserList"] as List<UserDetails>;

                DataTable dtUser = new DataTable();
                dtUser.Columns.Add("Sr. No.");
                dtUser.Columns.Add("User Name");
                dtUser.Columns.Add("Student Name");
                dtUser.Columns.Add("Course Name");

                int levels = 23; // in future get from configuration file

                for (int i = 1; i <= levels; i++)
                {
                    string column = "Level " + i;
                    dtUser.Columns.Add(column);
                }

                int srNo = 1;

                foreach (UserDetails user in users)
                {
                    DataRow row = dtUser.NewRow();
                    row[0] = srNo;
                    row[1] = user.UserName;
                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                    row[3] = user.Course;
                    int rowCount = 4;
                    for (int i = 1; i <= levels; i++, rowCount++)
                    {
                        StudentTypingStats objTyping = obj.GetTypingStatsByLevelUserId(user.Id, i, cnxnString, logPath);

                        if (objTyping != null)
                        {
                            row[rowCount] = "Accuracy: " + objTyping.Accuracy + "%, WPM: " + objTyping.GrossWPM + ", Time: " + objTyping.TimeSpanInSeconds + " seconds";
                        }
                        else
                        {
                            row[rowCount] = "Not attempted yet";
                        }
                    }

                    dtUser.Rows.Add(row);
                    srNo++;
                }

                string fileName = "Student-typing-practice-report-" + ddlCourse.SelectedValue + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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