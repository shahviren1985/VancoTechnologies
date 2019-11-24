using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.IO;
using System.Configuration;

public partial class Admin_AddUpdateAppLogoHeader : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;
    string collegeName;

    string IMG_NAME = string.Empty;
    string IMG_URL = string.Empty;

    ApplicationLogoHeaderDAO appDAO = new ApplicationLogoHeaderDAO();

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
                collegeName = Session["CollegeName"].ToString();
            }

            if (!IsPostBack)
            {
                ApplicationLogoHeader appLogo = appDAO.GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);
                if (appLogo != null)
                {
                    txtLogoText.Text = appLogo.LogoText;
                    imgUpdateLogo.ImageUrl = Util.BASE_URL + appLogo.LogoImagePath.Replace("~/", "");
                    btnSave.Text = "Update";                    
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSave_Click(object s, EventArgs e)
    {
        errorSummary.Visible = false;
        Success.Visible = false;

        try
        {
            if (string.IsNullOrEmpty(txtLogoText.Text))
            {
                errorSummary.InnerHtml = "Please enter Logo Header";
                errorSummary.Visible = true;
            }

            if (btnSave.Text == "Update")
            {
                if (!fuLogo.HasFile && imgUpdateLogo.ImageUrl == "")
                {
                    errorSummary.InnerHtml = "Please select logo image.";
                    errorSummary.Visible = true;
                }
                else
                {
                    ApplicationLogoHeader appLogo = appDAO.GetLogoHeaderbyCollege(collegeName, cnxnString, logPath);

                    if (fuLogo.HasFile)
                    {
                        string extention = Path.GetExtension(fuLogo.FileName).ToLower();

                        if (extention == ".jpg" || extention == ".jpeg" || extention == ".btm" || extention == ".gif" || extention == ".png")
                        {
                            string imgPath = ConfigurationManager.AppSettings["BASE_PATH"] + "LogoImage/" + collegeName + "/";

                            //string imgPath = Server.MapPath("LogoImage/" + collegeName + "/");

                            if (!Directory.Exists(imgPath))
                            {
                                Directory.CreateDirectory(imgPath);
                            }

                            fuLogo.SaveAs(imgPath + fuLogo.FileName);

                            string fileName = "logo-" + collegeName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + extention;
                            string imgUrl = "~/LogoImage/" + collegeName + "/" + fileName;

                            File.Move(imgPath + fuLogo.FileName, imgPath + fileName);

                            appDAO.UpdateLogoHeader(appLogo.Id, fileName, imgUrl, txtLogoText.Text, cnxnString, logPath);

                            Success.InnerHtml = "Logo Image and Text Updated successfully.";
                            Success.Visible = true;
                        }
                        else
                        {
                            errorSummary.InnerHtml = "Please select valid image.";
                            errorSummary.Visible = true;
                        }
                    }
                    else
                    {                        
                        if (appLogo != null)
                        {
                            appDAO.UpdateLogoHeader(appLogo.Id, appLogo.LogoImageName, appLogo.LogoImagePath, txtLogoText.Text, cnxnString, logPath);

                            Success.InnerHtml = "Logo Image and Text Updated successfully.";
                            Success.Visible = true;
                        }                        
                    }

                    btnSave.Text = "Save";
                }
            }
            else
            {
                if (fuLogo.HasFile)
                {
                    string extention = Path.GetExtension(fuLogo.FileName).ToLower();
                    if (extention == ".jpg" || extention == ".jpeg" || extention == ".btm" || extention == ".gif" || extention == ".png")
                    {
                        string imgPath = ConfigurationManager.AppSettings["BASE_PATH"] + "LogoImage/" + collegeName + "/";                        
                        //string imgPath = Server.MapPath("LogoImage/" + collegeName + "/");

                        if (!Directory.Exists(imgPath))
                        {
                            Directory.CreateDirectory(imgPath);
                        }

                        fuLogo.SaveAs(imgPath + fuLogo.FileName);

                        string fileName = "logo-" + collegeName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + extention;
                        string imgUrl = "~/LogoImage/" + collegeName + "/" + fileName;

                        File.Move(imgPath + fuLogo.FileName, imgPath + fileName);

                        appDAO.AddLogoHeaderDetails(collegeName, fileName, imgUrl, txtLogoText.Text, cnxnString, logPath);

                        Success.InnerHtml = "Logo Image and Text Added successfully.";
                        Success.Visible = true;
                    }
                    else
                    {
                        errorSummary.InnerHtml = "Please select valid image.";
                        errorSummary.Visible = true;
                    }
                }
                else
                {
                    errorSummary.InnerHtml = "Please select logo image.";
                    errorSummary.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}