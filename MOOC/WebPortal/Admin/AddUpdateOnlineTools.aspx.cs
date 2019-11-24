using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.LogManager;
using ITM.Courses.DAO;
using System.IO;
using System.Configuration;

public partial class Admin_AddUpdateOnlineTools : System.Web.UI.Page
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

    string mode = string.Empty;
    int courseId = 0;
    int toolId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
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

            #region Getting QueryString
            if (Request.QueryString.Count > 0)
            {
                //Staff/ManageTest.aspx?mode=edit&testid=1&courseid=2
                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && !string.IsNullOrEmpty(Request.QueryString["courseid"]))
                {
                    mode = Request.QueryString["mode"];
                    int.TryParse(Request.QueryString["courseid"], out courseId);

                    if (mode.ToLower() == "edit")
                    {
                        this.Title = "Update Online Tools";
                        header.InnerText = "Update Online Tools";
                        btnSave.Text = "Save Changes";

                        if (!string.IsNullOrEmpty(Request.QueryString["toolid"]))
                        {
                            int.TryParse(Request.QueryString["toolid"], out toolId);
                        }
                        else
                        {
                            // TODO redirect to main page
                            Response.Redirect(Util.BASE_URL + "Admin/ManageOnlineTools.aspx", false);
                            return;
                        }
                    }
                    else
                    {
                        this.Title = "Create Online Tools";
                        header.InnerText = "Create Online Tools";
                    }
                }
                else
                {
                    // TODO redirect to main page
                    Response.Redirect(Util.BASE_URL + "Admin/ManageOnlineTools.aspx", false);
                    return;
                }
            }
            else
            {
                // TODO redirect to main page
                //Response.Redirect(Util.BASE_URL + "Admin/ManageOnlineTools.aspx", false);
                //return;
            }
            #endregion

            if (!IsPostBack)
            {
                PopulateCourse();
                ddlCourse.SelectedValue = courseId.ToString();

                if (mode.ToLower() == "edit")
                {
                    PopulateOnlineTool();
                }
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

    private void PopulateOnlineTool()
    {
        try
        {
            if (toolId != 0)
            {
                OnlineTools oOnlineTool = new OnlineToolsDAO().GetOnlineToolsById(toolId, cnxnString, logPath);
                if (oOnlineTool != null)
                {
                    txtTitle.Text = oOnlineTool.Title;
                    txtDesc.Text = oOnlineTool.Description.ToString();
                    txtToolDisplayDate.Text = oOnlineTool.ToolDisplayDate.ToString();
                    txtToolUrl.Text = oOnlineTool.ToolURL.ToString();
                    ddlCourse.SelectedValue = oOnlineTool.RelatedCourseId.ToString();
                    chkIsActive.Checked = oOnlineTool.IsActive;

                    if (!string.IsNullOrEmpty(oOnlineTool.LogoImageURL))
                    {
                        //imgLogoPre.ImageUrl = Util.BASE_URL + oOnlineTool.LogoImageURL.Replace("~/", "");
                        imgLogoPre.ImageUrl = oOnlineTool.LogoImageURL;
                        imgLogoPre.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string logoImgName = string.Empty;
            string logoImgURL = string.Empty;

            if (!ValidateControls())
            {
                return;
            }

            if (fuLogo.HasFile)
            {
                string fileExtention = Path.GetExtension(fuLogo.FileName);

                if (fileExtention == ".jpg" || fileExtention == ".tif" || fileExtention == ".gif" || fileExtention == ".png" || fileExtention == ".jfif" || fileExtention == ".bmp")
                {
                    logoImgName = Path.GetFileNameWithoutExtension(fuLogo.FileName) + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(fuLogo.FileName);
                    string folderPath = ConfigurationManager.AppSettings["BASE_PATH"] + "online-tools\\" + ddlCourse.SelectedValue + "\\";

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    folderPath = Path.Combine(folderPath, logoImgName);

                    if (File.Exists(folderPath))
                    {
                        FileInfo info = new FileInfo(folderPath);
                        logoImgName = logoImgName.Replace(info.Extension, "") + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + info.Extension;
                        File.Copy(folderPath, folderPath.Replace(info.Extension, "") + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + info.Extension);
                    }
                    else
                    {
                        fuLogo.SaveAs(folderPath);
                    }

                    logoImgURL = "~/online-tools/" + ddlCourse.SelectedValue + "/" + logoImgName;
                }
                else
                {
                    errorSummary.InnerText = "Please upload a valid image(logo) file.";
                    errorSummary.Visible = true;
                }
            }

            OnlineToolsDAO onlineToolsDAO = new OnlineToolsDAO();

            DateTime displayDate = DateTime.Parse(txtToolDisplayDate.Text);

            if (!txtToolUrl.Text.Contains("http://") && !txtToolUrl.Text.Contains("https://"))
            {
                if (!string.IsNullOrEmpty(txtToolUrl.Text))
                    txtToolUrl.Text = "http://" + txtToolUrl.Text;
            }

            if (btnSave.Text == "Add Online Tool") //for adding new record
            {
                onlineToolsDAO.AddOnlineTools(int.Parse(ddlCourse.SelectedValue), txtTitle.Text, txtDesc.Text, logoImgURL, logoImgName, txtToolUrl.Text, displayDate, chkIsActive.Checked, cnxnString, logPath);

                Success.InnerText = "Successfully added new online tool";
                Success.Visible = true;
                ClearControls();
            }
            else    // for modifying existing record
            {
                OnlineTools tools = onlineToolsDAO.GetOnlineToolsById(toolId, cnxnString, logPath);

                if (tools != null)
                {
                    if (string.IsNullOrEmpty(logoImgName))
                    {
                        logoImgName = tools.LogoImageName;
                    }

                    if (string.IsNullOrEmpty(logoImgURL))
                    {
                        logoImgURL = tools.LogoImageURL;
                    }

                    onlineToolsDAO.UpdateOnlineTools(tools.Id, int.Parse(ddlCourse.SelectedValue), txtTitle.Text, txtDesc.Text, logoImgURL, logoImgName, txtToolUrl.Text, displayDate, chkIsActive.Checked, cnxnString, logPath);
                }

                Success.InnerText = "Successfully updated online tool";
                Success.Visible = true;

                btnSave.Text = "Add Online Tool";
                ClearControls();
            }
        }
        catch (Exception ex)
        {
            logger.Error("AddUpdateOnlineTools", "btnSave_Click", "Error Occured while adding online tools", ex, logPath);
            throw ex;
        }
    }

    public bool ValidateControls()
    {
        if (ddlCourse.SelectedValue == "0")
        {
            errorSummary.InnerText = "Please select course";
            errorSummary.Visible = true;
            return false;
        }

        if (string.IsNullOrEmpty(txtTitle.Text))
        {
            errorSummary.InnerText = "Please enter tool title or name";
            errorSummary.Visible = true;
            return false;
        }

        if (string.IsNullOrEmpty(txtDesc.Text))
        {
            errorSummary.InnerText = "Please enter tool description";
            errorSummary.Visible = true;
            return false;
        }

        if (string.IsNullOrEmpty(txtToolDisplayDate.Text))
        {
            errorSummary.InnerText = "Please enter tool display date";
            errorSummary.Visible = true;
            return false;
        }

        return true;
    }

    public void ClearControls()
    {
        txtDesc.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtToolDisplayDate.Text = string.Empty;
        txtToolUrl.Text = string.Empty;

        chkIsActive.Checked = false;
        imgLogoPre.Visible = false;
    }
}