<%@ WebHandler Language="C#" Class="SendGetEarlyAccesAndScheduleDemoMail" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Configuration;

public class SendGetEarlyAccesAndScheduleDemoMail : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    string cnxnString = string.Empty;
    string logPath = ConfigurationManager.AppSettings["Annonymus_LogPath"];
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string fName = context.Request.QueryString["fname"];
            string lName = context.Request.QueryString["lname"];
            string email = context.Request.QueryString["email"];
            string collegeName = context.Request.QueryString["college"];
            string city = context.Request.QueryString["city"];
            string type = context.Request.QueryString["type"];
            string key = context.Request.QueryString["key"];

            logger.Debug("SendGetEarlyAccesAndScheduleDemoMail-Handler", "ProcessRequest", 
            string.Format("Email Parameters for demo and early access: first Name:{0}, last-name:{1}, email:{2}, type:{3}, college:{4}, city:{5}", fName,lName,email, (type.ToLower() == "gea")? "Get Early Access":"Schedule a demo", collegeName, city),
                logPath);

            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(lName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(key))
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid parameters" }));
                return;
            }

            DateTime dt;

            key = key.Replace("%2B", "+");
            
            key = Util.Decrypt(key);

            if (DateTime.TryParse(key, out dt))
            {
                string toAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"];

                string body = string.Empty;

                if (type.ToLower() == "gea")
                {
                    body = "Hi admin, <br/> This is request for Get Early Access. Following details: <br/> Name : " + fName + " " + lName + "<br/> Email : " + email + ".";
                    new Util().SendMailForScheduleDemo(toAddress, "", "Request for 'Get Early Access'", body, "MOOC Academy :: Get Early Access");
                }
                else if (type.ToLower() == "sd")
                {
                    if (string.IsNullOrEmpty(collegeName) || string.IsNullOrEmpty(city))
                    {
                        context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid parameters" }));
                        return;
                    }

                    body = "Hi admin, <br/> This is request for Schedule a demonstration. Following details: <br/> Name : " + fName + " " + lName + "<br/> Email : " + email + "<br/>" +
                        "College : " + collegeName + "<br/> City : " + city;

                    new Util().SendMailForScheduleDemo(toAddress, "", "Request for 'Schedule a demonstration'", body, "MOOC Academy :: Schedule a demonstration");
                }

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Successully sent e-mail" }));
            }
            else
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Invalid key" }));
                return;
            }
        }
        catch (Exception ex)
        {
            logger.Error("SendGetEarlyAccesAndScheduleDemoMail-Handler", "ProcessRequest", "Error occured while sending mail", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Some error" }));
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