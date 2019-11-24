<%@ WebHandler Language="C#" Class="GetUsers" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class GetUsers : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();
            string userType = context.Request.QueryString["type"];
            UserDetailsDAO dao = new UserDetailsDAO();

            if (userType == "all")
            {
                List<UserDetails> users = dao.GetUserList(cnxnString, logPath);
                context.Response.Write(jss.Serialize(users));
            }
            else if (userType == "current")
            {
                UserDetails ud = new UserDetails();
                ud.FirstName = context.Session["FirstName"].ToString();
                ud.LastName = context.Session["LastName"].ToString();
                ud.RoleId = int.Parse(context.Session["RoleType"].ToString());
                                
                context.Response.Write(jss.Serialize(ud));
            }
        }
        catch (Exception ex)
        {
            logger.Error("GetUsers.ashx", "ProcessRequest", "Error occurred while getting all the users", ex, logPath);
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