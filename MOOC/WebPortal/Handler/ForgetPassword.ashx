<%@ WebHandler Language="C#" Class="ForgetPassword" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class ForgetPassword : IHttpHandler
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    UserLoginsDAO userLoginDAO = new UserLoginsDAO();
    UserDetailsDAO userDetailsDAO = new UserDetailsDAO();

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string type = System.Uri.UnescapeDataString(context.Request.QueryString["t"].ToString());
            string userName = System.Uri.UnescapeDataString(context.Request.QueryString["un"].ToString());

            UserLogins userLogin = userLoginDAO.GetUserByUserName(userName, "");

            if (userLogin != null)
            {
                UserDetails userDetails = userDetailsDAO.IsAuthenticated(userName, userLogin.Password, userLogin.CnxnString, context.Server.MapPath(userLogin.LogFilePath));

                if (userDetails != null)
                {
                    if (type.ToLower().Trim() == "email")
                    {
                        if (!string.IsNullOrEmpty(userDetails.EmailAddress))
                        {
                            string body = @"Hello " + userDetails.FirstName.ToUpper() + " " + userDetails.LastName.ToUpper() + ", <br/> Thank you for contacting MOOC Academy.<br/><br/>" +
                            "Your password is : <b>" + userDetails.Password + "</b><br/><br/><br/>Thank you <br/> MOOC Academy Team";

                            string ccAddress = System.Configuration.ConfigurationManager.AppSettings["ToAddress"];

                            new Util().SendMail(userDetails.EmailAddress, "", ccAddress, "MOOC-Academy :: Help-Desk", body);

                            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "An Email sent to '" + userDetails.EmailAddress + "', which contains your password." }));
                        }
                        else
                        {
                            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "You do not have email-address. Please choose other option to get your password." }));
                        }
                    }
                    else
                    {
                        string fName = System.Uri.UnescapeDataString(context.Request.QueryString["fn"].ToString()).ToLower().Trim();
                        string lName = System.Uri.UnescapeDataString(context.Request.QueryString["ln"].ToString()).ToLower().Trim();
                        string father = System.Uri.UnescapeDataString(context.Request.QueryString["f"].ToString()).ToLower().Trim();
                        string mother = System.Uri.UnescapeDataString(context.Request.QueryString["m"].ToString()).ToLower().Trim();
                        string mobile = System.Uri.UnescapeDataString(context.Request.QueryString["mn"].ToString());

                        if (fName == userDetails.FirstName.ToLower().Trim() &&
                            lName == userDetails.LastName.ToLower().Trim() &&
                            father == userDetails.FatherName.ToLower().Trim() &&
                            mother == userDetails.MotherName.ToLower().Trim() &&
                            mobile == userDetails.MobileNo)
                        {
                            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "Your Password is: " + userDetails.Password }));
                        }
                        else
                        {
                            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "User dosent found by giving details. Please check details." }));
                        }
                    }
                }
                else
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "User name dosent exist." }));
                }
            }
            else
            {
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "User name dosent exist." }));
            }
        }
        catch (Exception ex)
        {
            logger.Error("ForgetPassword-Handler", "ProcessRequest", "Error occured while sending email", ex, "");
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Unable to send password. Please try again after some time." }));
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