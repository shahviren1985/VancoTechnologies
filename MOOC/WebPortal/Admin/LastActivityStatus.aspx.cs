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

public partial class Admin_LastActivityStatus : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;
    const int FIRST_ELEMENT = 0;
    private int numberOfDays = 0;


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
            // bindGridView();
            // PopulateDirectoryFiles();
        }
    }

    protected void gvActivityDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditDetails")
        {
            //View.Visible = false;
            //Edit.Visible = true;
            // Success.Visible = false;
            // getChapterDetail(Convert.ToInt32(e.CommandArgument));
            // hfChapterId.Value = e.CommandArgument.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            numberOfDays = Int32.Parse(ddlNumOfDays.SelectedValue);
            DateTime date = DateTime.Now.AddDays(-numberOfDays);

            List<UserDetails> filteredUser = new List<UserDetails>();

            List<UserDetails> userList = new UserDetailsDAO().GetAllUsers(false, cnxnString, logPath);

            if (userList != null)
            {
                foreach (UserDetails user in userList)
                {
                    UserActivityTraker userActivity = new UserActivityTrackerDAO().GetUserActivityByDescDate(user.Id, cnxnString, logPath);

                    if (userActivity == null || userActivity.DateCreated < date)
                    {
                        if (userActivity != null)
                        {
                            user.LastActivityDate = userActivity.DateCreated.ToString("dd-MMM-yyyy hh:mm ttt");
                        }
                        else
                        {
                            user.LastActivityDate = "Not started yet";
                        }

                        filteredUser.Add(user);
                    }
                }
            }

            gvActivityDetails.DataSource = filteredUser;
            gvActivityDetails.DataBind();

            if (filteredUser.Count > 0)
            {
                saveButtons.Style["display"] = "block";
                getButtons.Style["display"] = "none";
                ViewState["UserList"] = filteredUser;
            }
            else
            {
                saveButtons.Style["display"] = "none";
                getButtons.Style["display"] = "block";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvActivityDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvActivityDetails.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    protected void btnCancelSave_Click(object s, EventArgs e)
    {
        saveButtons.Style["display"] = "none";
        getButtons.Style["display"] = "block";
    }

    protected void btnSaveAsPDF_Click(object s, EventArgs e)
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

                string fileName = "Inactive-Student-Last-" + ddlNumOfDays.SelectedItem.Text + "-" + DateTime.Now.Ticks + ".pdf";

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

                    PDFInactiveStudentsReport.GetTopperListContent(ref writer, ref document, users, 20);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }

                string postbackurl = "~/Admin/Certificates.aspx";

                Response.Redirect("~/PrintCertificate.aspx?path=" + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName + "&url=" + postbackurl + "&page=inactive", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}