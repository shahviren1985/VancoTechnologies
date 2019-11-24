<%@ WebHandler Language="C#" Class="SendContactUsMail" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using ITM.Courses.LogManager;


public class SendContactUsMail : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string name = System.Uri.UnescapeDataString(context.Request.QueryString["name"].ToString());
            string mobileNo = System.Uri.UnescapeDataString(context.Request.QueryString["mobileNo"].ToString());
            string email = System.Uri.UnescapeDataString(context.Request.QueryString["email"].ToString());
            string city = System.Uri.UnescapeDataString(context.Request.QueryString["city"].ToString());
            string query = System.Uri.UnescapeDataString(context.Request.QueryString["query"].ToString());

            // preparing body
            string body = @"Hello " + name.ToUpper() + ", <br/> Thank you for contacting MOOC Academy.<br/><br/> Your query:<br/>" + query + "<br/><br/>" +
                            "Your contact details: <br/><br/> Email : " + email +
                            "<br/> Mobile No# : " + mobileNo +
                            "<br/> City : " + city +
                            "<br/><br/><br/>Thank you <br/> MOOC Academy Team";

            string toAddress = System.Configuration.ConfigurationManager.AppSettings["ToAddress"];
            string ccAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"]; // from address used as CC 

            new Util().SendMail(email, "", ccAddress, "MOOC-Academy :: User Query", body);

            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Success" }));
        }
        catch (Exception ex)
        {
            logger.Error("SaveStudentsQuery-Handler", "ProcessRequest", "Error occured while sending email", ex, "");

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