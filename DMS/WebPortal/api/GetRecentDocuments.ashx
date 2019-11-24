<%@ WebHandler Language="C#" Class="GetRecentDocuments" %>
using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;


public class GetRecentDocuments : IHttpHandler, IRequiresSessionState
{
    long ticks = DateTime.Now.Ticks;
    JavaScriptSerializer jss = new JavaScriptSerializer();
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    UserDetailsDAO userDao = new UserDetailsDAO();

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();

        UserDetails user = userDao.GetUserDetailList(userName, cnxnString, logPath);
        string[] depts = user.Departments.Split(',');
        string query = string.Empty;

        if (context.Session["RoleType"].ToString() == "2" || context.Session["RoleType"].ToString() == "3")
        {
            query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser, StoreroomLoc, Address FROM documents d  order by lastmodified desc limit 100";
        }
        else //
        {
            query = "(SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser, StoreroomLoc, Address FROM documents d where author='" + userName + "' or messageheader like '%" + context.Session["FirstName"].ToString() + " " + context.Session["LastName"].ToString() + " (" + context.Session["UserName"].ToString() + ")" + "%') " +
           " union " +
           "(SELECT ParentId, SerialNumber,id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser, StoreroomLoc, Address FROM documents d where taggeduser like '%" + userName + "%') "
           ;

            foreach (string dept in depts)
            {
                if (!string.IsNullOrEmpty(dept))
                {
                    string joinQuery = " union (SELECT ParentId, SerialNumber,id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser, StoreroomLoc, Address FROM documents d where departmentid like '%" + dept + "%') ";
                    query += joinQuery;
                }
            }

            query += " order by lastmodified desc limit 100";
        }

        List<DocumentDetails> documents = dao.GetDocumentDetails(query, cnxnString, logPath);

        foreach (DocumentDetails doc in documents)
        {
            doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
            //doc.LastModified = DateTime.Parse(doc.LastModified).AddHours(12).ToString();
            MessageHeader mh = jss.Deserialize<MessageHeader>(doc.MessageHeader);
            //mh.Subject = HttpUtility.UrlDecode(mh.Subject);
            mh.From = HttpUtility.UrlDecode(mh.From);
            doc.MessageHeader = jss.Serialize(mh);
        }

        context.Response.Write(jss.Serialize(documents));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}