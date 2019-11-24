<%@ WebHandler Language="C#" Class="SaveUserDetails" %>

using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;

public class SaveUserDetails : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        long ticks = DateTime.Now.Ticks;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        UserDetailsDAO dao = new UserDetailsDAO();
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();

        try
        {
            string userName = context.Request.Params[0];
            string firstName = context.Request.Params[1];
            string lastName = context.Request.Params[2];
            string password = context.Request.Params[3];
            string roleId = context.Request.Params[4];
            string mode = context.Request.Params[5];
            string department = context.Request.Params[6];
            string email = context.Request.Params[7];
            string mobileNo = context.Request.Params[8];

            if (!string.IsNullOrEmpty(mode))
            {
                if (mode.ToLower() == "create")
                    dao.CreateUser(
                            userName, 
                            firstName, 
                            lastName, 
                            int.Parse(roleId), 
                            password, 
                            Util.Utilities.GetCurrentIndianDate(), 
                            true, 
                            department, 
                            email, 
                            mobileNo,
                            cnxnString, 
                            logPath
                        );
                    
                else if (mode.ToLower() == "password")
                    dao.ChangePassword(userName, password, cnxnString, logPath);
                else if (mode.ToLower() == "update")
                {
                    UserDetails user = new UserDetails();
                    dao.ChangeUserDetails(user, cnxnString, logPath);
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error");
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