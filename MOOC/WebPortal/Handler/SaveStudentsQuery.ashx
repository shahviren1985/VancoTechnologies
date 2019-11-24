<%@ WebHandler Language="C#" Class="SaveStudentsQuery" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class SaveStudentsQuery : IHttpHandler, IRequiresSessionState
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
                UserLogins userLogin = new UserLoginsDAO().GetUserLoginByUserNamePassword("temp_user", "123", "");
                
                if(userLogin == null)
                    userLogin = new UserLoginsDAO().GetUserByUserName("temp_user", "");

                if (userLogin != null)
                {
                    UserDetails user = new UserDetailsDAO().GetUserByUserName(userLogin.UserName, userLogin.CnxnString, context.Server.MapPath(userLogin.LogFilePath));

                    if (user != null)
                    {
                        userId = user.Id;
                        cnxnString = userLogin.CnxnString;
                        logPath = userLogin.LogFilePath ;
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

            if (string.IsNullOrEmpty(context.Request.QueryString["name"]) || string.IsNullOrEmpty(context.Request.QueryString["mobileNo"]) ||
                string.IsNullOrEmpty(context.Request.QueryString["query"]) || userId == 0)
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid data!" }));
                return;
            }

            string name = context.Request.QueryString["name"].ToString();
            string mobileNo = context.Request.QueryString["mobileNo"].ToString();
            string query = context.Request.QueryString["query"].ToString();

            StudentQueriesDAO studentQueryDAO = new StudentQueriesDAO();

            studentQueryDAO.AddStudentQuery(userId, collegeName, name, mobileNo, query, cnxnString, logPath);

            try
            {
                string toAddress = System.Configuration.ConfigurationManager.AppSettings["ToAddress"];
                new Util().SendMail(toAddress, "", "MOOC-Academy :: Student Query", query);
            }
            catch (Exception ex)
            {
                logger.Error("SaveStudentsQuery-Handler", "ProcessRequest", "Error occured while sending email", ex, logPath);
            }
            
            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));

        }
        catch (Exception ex)
        {
            logger.Error("SaveStudentsQuery-Handler", "ProcessRequest", "Error occured while saving student query", ex, logPath);
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