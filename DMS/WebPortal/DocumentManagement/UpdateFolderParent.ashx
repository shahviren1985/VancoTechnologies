<%@ WebHandler Language="C#" Class="UpdateFileParent" %>

using System;
using System.Web;
using AA.DAO;
using System.Collections.Generic;
using AA.Util;

public class UpdateFileParent : IHttpHandler
{
    int parentId = -1;
    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();

        int parentId = int.Parse(context.Request.QueryString["parentId"]);
        if (parentId == null)
        {
            parentId = -1;
        }
        int folderId = int.Parse(context.Request.QueryString["folerId"]);

        FolderDetailsDAO folder = new FolderDetailsDAO();
        FolderDetails folder1 = folder.GetSingleFileDetails(folderId, cnxnString, logPath);
        folder.UpdateParentFolderId(parentId, folderId, Util.UnescapeFormat.UnescapeXML(folder1.History), cnxnString, logPath);
        // context.Response.ContentType = "text/plain";
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