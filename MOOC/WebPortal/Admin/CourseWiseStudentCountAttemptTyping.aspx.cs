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

public partial class Admin_CourseWiseStudentCountAttemptTyping : PageBase
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
                pdfFolderPath = Server.MapPath(Session["PDFFolderPath"].ToString());
            }

            if (!IsPostBack)
            {                
                PopulateGridView();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void PopulateGridView()
    {
        try
        {
            List<UserDetails> users = new StudentTypingStatsDAO().GetCourseWiseStudentCountWhoAttemptingTyping(cnxnString, logPath);
            gvStudentCount.DataSource = users;
            gvStudentCount.DataBind();

            ViewState["UserList"] = users;
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
                
                ApplicationLogoHeader appConfig = new ApplicationLogoHeaderDAO().GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);

                var document = new Document(PageSize.A4);
                var styles = new StyleSheet();
                PdfWriter writer = null;
                bool download = false;

                if (!Directory.Exists(pdfFolderPath))
                {
                    Directory.CreateDirectory(pdfFolderPath);
                }

                string fileName = "Course-wise-student-counts-who-attempting-typingtutorial-" + DateTime.Now.Ticks + ".pdf";

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
                    PDFCourseWiseStudentsTypingCount.CreatePDFHeader(ref writer, ref document, appConfig);
                    PDFCourseWiseStudentsTypingCount.CreatePDFMainContent(ref writer, ref document, users, 20);
                    //PDFBatchwiseStudentStatusReport.GetTopperListContent(ref writer, ref document, filterdUsers, 20);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }

                string postbackurl = "~/Admin/CourseWiseStudentCountAttemptTyping.aspx";

                Response.Redirect("~/PrintCertificate.aspx?path=" + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName + "&url=" + postbackurl + "&page=batchwisestatus", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}