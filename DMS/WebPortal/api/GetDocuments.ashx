<%@ WebHandler Language="C#" Class="GetDocuments" %>

using System;
using System.Web;
using System.IO;
public class GetDocuments : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string admissionYear = context.Request.QueryString["admissionYear"].ToString();
        string collegeRegistrationNumber = context.Request.QueryString["crn"].ToString();

        string fullPath = context.Server.MapPath("~/docs/SVT/Students/" + admissionYear + "/" + collegeRegistrationNumber + "/");
        string[] folderPaths = Directory.GetDirectories(fullPath);

        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
        context.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");

        System.Collections.Generic.List<string> filePaths = new System.Collections.Generic.List<string>();
        System.Collections.Generic.List<string> fileList = new System.Collections.Generic.List<string>();

        foreach (string folder in folderPaths)
        {
            string[] files = Directory.GetFiles(folder);
            filePaths.AddRange(files);
        }

        foreach (string file in filePaths)
        {
            string f = file.Replace(fullPath, "").Replace(@"\", "/");
            fileList.Add(f);
        }

        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

        context.Response.Write(jss.Serialize(fileList));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}