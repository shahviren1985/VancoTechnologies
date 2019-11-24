<%@ WebHandler Language="C#" Class="SearchDocuments" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class SearchDocuments : IHttpHandler, IRequiresSessionState
{

    JavaScriptSerializer jss = new JavaScriptSerializer();
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    UserDetailsDAO userDao = new UserDetailsDAO();


    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();
        string searchTerm = context.Request.QueryString["q"];
        string role = context.Session["RoleType"].ToString();

        UserDetails user = userDao.GetUserDetailList(userName, cnxnString, logPath);
        string[] depts = user.Departments.Split(',');
        string query = string.Empty;

        if (string.IsNullOrEmpty(searchTerm))
        {
            if (role == "1")
            {
                query = "(SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,StoreroomLoc FROM documents d where author='" + userName + "' or messageheader like '%" + context.Session["FirstName"].ToString() + " " + context.Session["LastName"].ToString() + " (" + context.Session["UserName"].ToString() + ")" + "%') " +
              " union " +
              "(SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,StoreroomLoc FROM documents d where taggeduser like '%" + userName + "%') "
              ;

                foreach (string dept in depts)
                {
                    string joinQuery = " union (SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,StoreroomLoc FROM documents d where departmentid like '%" + dept + "%') ";
                    query += joinQuery;
                }

                query += " order by lastmodified desc limit 100";
            }
            else
            {
                query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,StoreroomLoc FROM documents d  order by lastmodified desc limit 100";
            }
        }
        else
        {
            searchTerm = context.Server.UrlEncode(searchTerm);
            
            // Currently we are not considering departmentid in search result - as user might have tagged the department name
            // instead of user name and - user table might be associated with that department 
            if (role == "1")
            {
                query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,StoreroomLoc, Address FROM documents d where (friendlyname like '%" + searchTerm + "%' or filestag like '%" + searchTerm + "%' or messageheader like '%" + searchTerm + "%') and (author = '" + userName + "' or taggeduser like '%" + userName + "%')";
                query += " order by lastmodified desc limit 100";
            }
            else
            {
                query = "SELECT ParentId, SerialNumber, id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy,MessageHeader,DepartmentId,TaggedUser,StoreroomLoc, Address FROM documents d where friendlyname like '%" + searchTerm + "%' or filestag like '%" + searchTerm + "%' or messageheader like '%" + searchTerm + "%'";
                query += " order by lastmodified desc limit 100";
            }
        }
        /*string query = "(SELECT id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy FROM documents d where author='" + searchTerm + "') " +
            " union " +
            "(SELECT id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy FROM documents d where taggeduser like '%" + searchTerm + "%') "
            ;

        foreach (string dept in depts)
        {
            string joinQuery = " union (SELECT id,friendlyname,author,documentstatus,filestag,lastmodified,LastModifiedBy FROM documents d where departmentid like '%" + dept + "%') ";
            query += joinQuery;
        }*/

        List<DocumentDetails> documents = dao.GetDocumentDetails(query, cnxnString, logPath);

        foreach (DocumentDetails doc in documents)
        {
            doc.FriendlyName = HttpUtility.UrlDecode(doc.FriendlyName);
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