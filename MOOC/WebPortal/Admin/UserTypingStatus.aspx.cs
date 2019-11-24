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

public partial class Admin_UserTypingStatus : PageBase
{

    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;
    const int FIRST_ELEMENT = 0;

    protected void Page_Load(object sender, EventArgs e)
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
            collegeName = Session["CollegeName"].ToString();
            pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
        }

        if (!IsPostBack)
        {
            PopulateCourse();
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
    protected void btnGetUser_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLevel.SelectedValue == "0" || ddlCourse.SelectedValue == "0")
            {
                errorSummary.Visible = true;
                errorSummary.InnerText = "Opps you miss Level or Course !!!";
            }
            else 
            {
                bindGridView();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void gvTypingPerformance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTypingPerformance.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            StudentTypingStatsDAO typingStatus = new StudentTypingStatsDAO();
            List<StudentTypingStats> studentTypingStatus = typingStatus.GetTypingStatsByCourseAndLevel(Int32.Parse(ddlLevel.SelectedValue), ddlCourse.SelectedItem.Text, cnxnString, logPath);
            //gvTypingPerformance.DataSource = typingStatus.GetTypingStatsByCourseAndLevel(Int32.Parse(ddlLevel.SelectedValue),ddlCourse.SelectedItem.Text,cnxnString,logPath);
            gvTypingPerformance.DataSource = studentTypingStatus;
            gvTypingPerformance.DataBind();
            gvTypingPerformance.Visible = true;
            btnSavePDF.Visible = true;

            ViewState["UserList"] = studentTypingStatus ;
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
                List<StudentTypingStats> users = (List<StudentTypingStats>)ViewState["UserList"];

                //List<StudentTypingStats> filterdUsers = new List<StudentTypingStats>();

                //bool isSelected = false;
                //foreach (GridViewRow row in gvTypingPerformance.Rows)
                //{
                //    CheckBox chk = row.FindControl("gvchkSelectStudent") as CheckBox;
                //    if (chk != null && chk.Checked)
                //    {
                //        isSelected = true;
                //        Label lblId = row.FindControl("gvlblId") as Label;

                //        int id = Convert.ToInt32(lblId.Text);

                //        filterdUsers.Add(users.Find(u => u.Id == id));
                //    }
                //}

                //if (!isSelected || filterdUsers.Count == 0)
                //{
                //    filterdUsers = users;
                //}

                ApplicationLogoHeader appConfig = new ApplicationLogoHeaderDAO().GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);

                var document = new Document(PageSize.A4.Rotate());
                var styles = new StyleSheet();
                PdfWriter writer = null;
                bool download = false;

                if (!Directory.Exists(pdfFolderPath))
                {
                    Directory.CreateDirectory(pdfFolderPath);
                }

                string fileName = "Student-Typing-Stats-" + ddlCourse.SelectedValue + "-" + DateTime.Now.Ticks + ".pdf";

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
                    PDFStudentTypingStatus.CreateNPStudentHeader(ref writer, ref document, appConfig, "Level-wise Typings Status");
                    PDFStudentTypingStatus.GetTopperListContent(ref writer, ref document, users, 20, appConfig, "Level-wise Typings Status");
                    //PDFBatchwiseStudentStatusReport.GetTopperListContent(ref writer, ref document, filterdUsers, 20);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }

                string postbackurl = "~/Admin/UserTypingStatus.aspx";

                Response.Redirect("~/PrintCertificate.aspx?path=" + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName + "&url=" + postbackurl + "&page=batchwisestatus", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}