<%@ WebHandler Language="C#" Class="UpdateFileParent" %>

using System;
using System.Web;
using AA.DAO;
using System.Collections.Generic;
using Util;
using System.Web.SessionState;

public class UpdateFileParent : IHttpHandler, IRequiresSessionState
{
    int parentId = -1;
    public void ProcessRequest(HttpContext context)
    {
        string logPath = string.Empty;
        string cnxnString = string.Empty;
        if (context.Session["LogFilePath"] == null)
        {
            cnxnString = context.Request.QueryString["conn"];
            logPath = context.Request.QueryString["log"];
        }
        else
        {
            logPath = context.Session["LogFilePath"].ToString();
            cnxnString = context.Session["ConnectionString"].ToString();
        }


        int parentId = int.Parse(context.Request.QueryString["parentId"]);
        if (parentId == null)
        {
            parentId = -1;
        }

        int fileId = int.Parse(context.Request.QueryString["folerId"]);


        FileDetailsDAO fileDetails = new FileDetailsDAO();
        FileDetails file = fileDetails.GetSingleFileDetails(fileId, cnxnString, logPath);
        fileDetails.UpdateParentFolderId(parentId, fileId, UnescapeFormat.UnescapeXML(file.History), cnxnString, logPath);
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