using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.DAOBase;
using ITM.Courses.ConfigurationsManager;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using System.Configuration;
using System.Web.Script.Serialization;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Data;
using ITM.Courses.ExcelGenerator;

public partial class Admin_NotStartedTypingTest : PageBase
{
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;
    string pdfPath;

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
                pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
                pdfPath = Session["PDFFolderPath"].ToString();
            }

            if (!IsPostBack)
            {
                PopulateCourse();
                PopulateGridview();
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

    public void PopulateGridview()
    {
        try
        {
            List<UserDetails> users =new StudentTypingStatsDAO().GetStudentNotStartedTyping(cnxnString, logPath);
            gvUserDetails.DataSource = users;
            gvUserDetails.DataBind();

            ViewState["UserList"]=users;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void gvUserDetails_PageIndexChanging(object s, GridViewPageEventArgs e)
    {

    }

    protected void ddlCourse_Change(object s, EventArgs e)
    {
        try
        {
            divGenerateButton.Style["display"] = "block";
            divPrintButton.Style["display"] = "none";

            gvUserDetails.DataSource = null;
            gvUserDetails.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnGenerate_Click(object s, EventArgs e)
    {
        errorSummary.Visible = false;
        try
        {
            if (ddlCourse.SelectedValue == "0")
            {
                errorSummary.InnerHtml = "Please select batch-year.";
                errorSummary.Visible = true;
                return;
            }

            List<UserDetails> filteredUsers = new StudentTypingStatsDAO().GetStudentNotStartedTypingByCourse(ddlCourse.SelectedValue, cnxnString, logPath);

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
            {
                List<UserDetails> users = (List<UserDetails>)ViewState["UserList"];
                List<UserDetails> filterdUsers = new List<UserDetails>();

                bool isSelected = false;
                foreach (GridViewRow row in gvUserDetails.Rows)
                {
                    CheckBox chk = row.FindControl("gvchkSelectStudent") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        isSelected = true;
                        Label lblId = row.FindControl("gvlblId") as Label;

                        int id = Convert.ToInt32(lblId.Text);

                        filterdUsers.Add(users.Find(u => u.Id == id));
                    }
                }

                if (!isSelected || filterdUsers.Count == 0)
                {
                    filterdUsers = users;
                }

                ApplicationLogoHeader appConfig = new ApplicationLogoHeaderDAO().GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);

                var document = new Document(PageSize.A4.Rotate());
                var styles = new StyleSheet();
                PdfWriter writer = null;
                bool download = false;

                if (!Directory.Exists(pdfFolderPath))
                {
                    Directory.CreateDirectory(pdfFolderPath);
                }

                string fileName = "Student-not-started-typing-" + ddlCourse.SelectedValue + "-" + DateTime.Now.Ticks + ".pdf";

                if (download)
                {
                    writer = PdfWriter.GetInstance(document, Response.OutputStream);
                }
                else
                {
                    writer = PdfWriter.GetInstance(document, new FileStream(pdfFolderPath + fileName, FileMode.Create));
                }

                appConfig.LogoImagePath = Server.MapPath(appConfig.LogoImagePath);

                try
                {
                    document.Open();
                    PDFBatchwiseStudentStatusReport.CreateNPStudentHeader(ref writer, ref document, appConfig, "Not Started Typings Report");
                    //PDFBatchwiseStudentStatusReport.GetTopperListContent(ref writer, ref document, users, 20);
                    PDFBatchwiseStudentStatusReport.GetTopperListContent(ref writer, ref document, filterdUsers, 20, appConfig, "Not Started Typings Report");
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }

                string postbackurl = "~/Admin/NotStartedTypingTest.aspx";

                Response.Redirect("~/PrintCertificate.aspx?path=" + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName + "&url=" + postbackurl + "&page=batchwisestatus", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGenerateExcel_Click(object s, EventArgs e)
    {
        try
        {
            if (ViewState["UserList"] != null)
            {
                List<UserDetails> users = ViewState["UserList"] as List<UserDetails>;

                DataTable dtUser = new DataTable();
                dtUser.Columns.Add("Sr. No.");
                dtUser.Columns.Add("User Name");
                dtUser.Columns.Add("Student Name");
                dtUser.Columns.Add("Course Name");

                int srNo = 1;

                foreach (UserDetails user in users)
                {
                    DataRow row = dtUser.NewRow();

                    row[0] = srNo;
                    row[1] = user.UserName;
                    row[2] = user.LastName + " " + user.FirstName + " " + user.FatherName + " " + user.MotherName;
                    row[3] = user.Course;

                    dtUser.Rows.Add(row);
                    srNo++;
                }

                string fileName = "Student-not-started-typing-report-" + ddlCourse.SelectedValue + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                string fileUrl = pdfPath + fileName;

                //pdfPath = Server.MapPath(pdfPath);

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