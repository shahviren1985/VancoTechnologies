<%@ WebHandler Language="C#" Class="CreateStaffFolder" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;
using AA.DAO;
using Util;
using System.Web.SessionState;

public class CreateStaffFolder : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();
        string firstName = context.Request.QueryString["firstName"];
        string lastname = context.Request.QueryString["lastName"];
        string middleName = context.Request.QueryString["middleName"];
        string college = context.Session["College"].ToString();

        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastname) && !string.IsNullOrEmpty(middleName))
        {
            firstName = firstName.Trim().ToLower();
            lastname = lastname.Trim().ToLower();
            middleName = middleName.Trim().ToLower();
            //firstName + "_" + middleName + "_" + lastname

            UserDetailsDAO dao = new UserDetailsDAO();
            UserDetails ud = dao.GetUserDetailList(userName, cnxnString, logPath);

            CreateStaffFolders createfolders = new CreateStaffFolders();
            int memberId = createfolders.CrateStaffFlodersSubFolders(firstName.Trim() + "_" + middleName.Trim() + "_" + lastname.Trim(), "Staff_Admin".ToLower(), ud.Id, cnxnString, logPath, college);
            context.Response.Write(memberId);
        }
        else
        {
            context.Response.Write("Error: Some of the parameter is empty");
        }
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}