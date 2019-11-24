using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public partial class Staff_ContentCreation : System.Web.UI.Page
{
    int sectionId = 0;
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

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
            }

            if (Request.QueryString.Count == 0)
            {
                Response.Redirect(Util.BASE_URL + "Staff/ManageContent.aspx", false);
                return;
            }

            sectionId = Convert.ToInt32(Request.QueryString["section"]);

            if (!IsPostBack)
            {
                PopulateSectionContent();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateSectionContent()
    {
        try
        {
            if (sectionId != 0)
            {
                ChapterSection section = new ChapterSectionDAO().GetChapterSectionById(sectionId, cnxnString, logPath);

                if (section != null && section.PageContent != null)
                {
                    lblSectionName.Text = section.Title;

                    lblChapterName.Text = new ChapterDetailsDAO().GetChapterDetails(section.ChapterId, cnxnString, logPath).Title;

                    //hfPageContent.Value = section.PageContent;
                    if (section.Time != null && !string.IsNullOrEmpty(section.Time.ToString()))
                        txtEstimateTime.Text = (section.Time / 60).ToString();

                    if (!string.IsNullOrEmpty(section.Description))
                        ddlPageColor.SelectedValue = section.Description;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>SetContents('" + section.PageContent + "');</script>", false);
                }
                else
                {
                    errorSummary.InnerText = "Requested section does not exist. Please check.";
                    errorSummary.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSaveContent_Click(object s, EventArgs e)
    {
        try
        {
            if (sectionId != 0 && !string.IsNullOrEmpty(hfPageContent.Value) && !string.IsNullOrEmpty(txtEstimateTime.Text))
            {
                int time = Convert.ToInt32(txtEstimateTime.Text) * 60; // convert to in minuts

                new ChapterSectionDAO().UpdateSectionPageContent(sectionId, hfPageContent.Value, time, ddlPageColor.SelectedValue, cnxnString, logPath);

                txtEstimateTime.Text = "";
                ddlPageColor.SelectedValue = "blue";
                Success.InnerText = "Successfully updated section content.";
                Success.Visible = true;

                Button button = s as Button;

                if (button != null && button.Text == "Save & Finish")
                {
                    Response.Redirect(Util.BASE_URL + "Staff/ManageContent.aspx?section=" + sectionId.ToString(), false);
                    return;
                }

                PopulateSectionContent();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}