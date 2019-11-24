<%@ WebHandler Language="C#" Class="SaveSiteAnalyticsDetails" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;

public class SaveSiteAnalyticsDetails : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;
    string collegeName = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";

            int userId = 0;

            if (context.Session["ConnectionString"] == null)
            {
                UserLogins userLogin = new UserLoginsDAO().GetUserLoginByUserNamePassword("temp_user", "temp_user@123", "");

                if (userLogin != null)
                {
                    UserDetails user = new UserDetailsDAO().GetUserByUserName(userLogin.UserName, userLogin.CnxnString, context.Server.MapPath(userLogin.LogFilePath));

                    if (user != null)
                    {
                        userId = user.Id;
                        cnxnString = userLogin.CnxnString;
                        logPath = userLogin.LogFilePath;
                        collegeName = userLogin.CollegeName;
                        configFilePath = context.Server.MapPath(userLogin.ReleaseFilePath);
                    }
                }
                else
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));
                    return;
                }
            }
            else
            {
                cnxnString = context.Session["ConnectionString"].ToString();
                logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                collegeName = context.Session["CollegeName"].ToString();
                configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());
                userId = Convert.ToInt32(context.Session["UserId"].ToString());
            }

            if (string.IsNullOrEmpty(context.Request.QueryString["time"]) || userId == 0 || string.IsNullOrEmpty(context.Request.QueryString["page"]) ||
                string.IsNullOrEmpty(context.Request.QueryString["pagetitle"]) || string.IsNullOrEmpty(context.Request.QueryString["refferpage"]))
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data!" }));
                return;
            }
            
            int time = Convert.ToInt32(context.Request.QueryString["time"].ToString());
            string ipAddress = context.Request.UserHostAddress;
            string browser = context.Request.Browser.Browser;
            string language = context.Request.UserLanguages[0];
            string os = ParameterFormater.GetOSNameByUserAgent(context.Request.UserAgent);
            string pageName = context.Request.QueryString["page"].ToString();
            string pageTitle = context.Request.QueryString["pagetitle"].ToString();
            string refferPage = context.Request.QueryString["refferpage"].ToString();
            string userAgent = context.Request.UserAgent;
            bool isMobile = context.Request.Browser.IsMobileDevice;
            string mobileDevice = context.Request.Browser.MobileDeviceModel;
            string comment = context.Request.QueryString["comment"].ToString();
            string screenResulotion = context.Request.QueryString["screenresulotion"].ToString();

            new SiteAnalyticsDAO().AddSiteAnalyticsDetails(time, ipAddress, browser, language, os, pageName, pageTitle, refferPage, userAgent,
                isMobile, mobileDevice, comment, screenResulotion, cnxnString, logPath);
            
            //context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = DateTime.Now.ToString() }));
        }
        catch (Exception ex)
        {
            logger.Error("SaveSiteAnalyticsDetails-Handler", "ProcessRequest", "Error occured while inserting Site-Analytics details", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = ex.Message }));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}