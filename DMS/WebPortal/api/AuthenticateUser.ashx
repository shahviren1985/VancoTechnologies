<%@ WebHandler Language="C#" Class="AuthenticateUser" %>

using System;
using System.Web;
using AA.DAO;
using System.Web.SessionState;

public class AuthenticateUser : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string userName = string.Empty;
        string password = string.Empty;
        UserDetailsDAO dao = new UserDetailsDAO();
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        foreach (string item in context.Request.Form)
        {
            switch (item)
            {
                case "User":
                    userName = context.Request.Params[item];
                    break;
                case "Password":
                    password = context.Request.Params[item];
                    break;
            }
        }

        UserDetails ud = dao.IsAuthenticated(userName, password, cnxnString, logPath);
        if (!string.IsNullOrEmpty(ud.UserName) && (ud.RoleId == 2) || ud.RoleId == 3)
            context.Response.Write("true");
        else
            context.Response.Write("false");

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}