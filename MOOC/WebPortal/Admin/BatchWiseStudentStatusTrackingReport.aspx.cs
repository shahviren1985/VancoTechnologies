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

public partial class Admin_BatchWiseStudentStatusTrackingReport : PageBase
{
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;
    string pdfFolderPath;

    protected void Page_Load(object sender, EventArgs e)
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
    }

    protected void gvUserDetails_PageIndexChanging(object s, GridViewPageEventArgs e)
    {

    }

    protected void ddlBatch_Change(object s, EventArgs e)
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
            if (ddlBatch.SelectedValue == "0")
            {
                errorSummary.InnerHtml = "Please select batch-year.";
                errorSummary.Visible = true;
                return;
            }

            List<UserDetails> users = new UserDetailsDAO().GetAllUsers(false, cnxnString, logPath);
            List<UserDetails> filteredUsers = new List<UserDetails>();

            if (users == null)
            {
                errorSummary.InnerHtml = "You don't added any student yet.";
                errorSummary.Visible = true;
                return;
            }

            int currentYear = Convert.ToInt32(DateTime.Now.ToString("yyyy")) + 1;
            int selectedBatchYear = Convert.ToInt32(ddlBatch.SelectedValue);

            if (selectedBatchYear == (currentYear - 3))
            {
                List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChapterDetails(cnxnString, logPath);
                if (chapters != null)
                {
                    foreach (UserDetails user in users)
                    {
                        foreach (ChapterDetails chp in chapters)
                        {
                            List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                            foreach (ChapterSection sec in sections)
                            {
                                UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(user.Id, chp.Id, sec.Id, cnxnString, logPath);

                                if (userChapterStatus == null)
                                {
                                    if (filteredUsers.Find(u => u.Id == user.Id) == null)
                                        filteredUsers.Add(user);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (selectedBatchYear == (currentYear - 2))
            {
                List<ChapterDetails> chapters = new ChapterDetailsDAO().GetChaptersBetweenTwoIds(1, 10, cnxnString, logPath);
                if (chapters != null)
                {
                    foreach (UserDetails user in users)
                    {
                        foreach (ChapterDetails chp in chapters)
                        {
                            List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                            foreach (ChapterSection sec in sections)
                            {
                                UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(user.Id, chp.Id, sec.Id, cnxnString, logPath);

                                if (userChapterStatus == null)
                                {
                                    if (filteredUsers.Find(u => u.Id == user.Id) == null)
                                        filteredUsers.Add(user);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (selectedBatchYear == (currentYear - 1))
            {
                List<ChapterDetails> chapters = new ChapterDetailsDAO().GetChaptersBetweenTwoIds(1, 7, cnxnString, logPath);
                if (chapters != null)
                {
                    foreach (UserDetails user in users)
                    {
                        foreach (ChapterDetails chp in chapters)
                        {
                            List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                            foreach (ChapterSection sec in sections)
                            {
                                UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(user.Id, chp.Id, sec.Id, cnxnString, logPath);

                                if (userChapterStatus == null)
                                {
                                    if (filteredUsers.Find(u => u.Id == user.Id) == null)
                                        filteredUsers.Add(user);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (filteredUsers.Count > 0)
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
                    // show message
                    errorSummary.InnerHtml = "Please select students.";
                    errorSummary.Visible = true;
                    return;
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

                string fileName = "Batch-wise-student-status-" + ddlBatch.SelectedValue + "-" + DateTime.Now.Ticks + ".pdf";

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
                    PDFBatchwiseStudentStatusReport.CreateNPStudentHeader(ref writer, ref document, appConfig, "Batchwise Student Status");
                    //PDFBatchwiseStudentStatusReport.GetTopperListContent(ref writer, ref document, users, 20);
                    PDFBatchwiseStudentStatusReport.GetTopperListContent(ref writer, ref document, filterdUsers, 20, appConfig, "Batchwise Student Status");
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }

                string postbackurl = "~/Admin/BatchWiseStudentStatusTrackingReport.aspx";

                Response.Redirect("~/PrintCertificate.aspx?path=" + Session["PDFFolderPath"].ToString().Replace("~/", "") + fileName + "&url=" + postbackurl + "&page=batchwisestatus", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}