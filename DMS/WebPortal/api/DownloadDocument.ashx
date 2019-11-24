<%@ WebHandler Language="C#" Class="DownloadDocument" %>

using System;
using System.Web;

public class DownloadDocument : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string admissionYear = context.Request.QueryString["admissionYear"].ToString();
        string collegeRegistrationNumber = context.Request.QueryString["crn"].ToString();
        string p = context.Request.QueryString["p"].ToString();

        if (string.IsNullOrEmpty(admissionYear) || string.IsNullOrEmpty(collegeRegistrationNumber) || string.IsNullOrEmpty(p))
        {
            context.Response.Write("Error: Please provide College Registration Number/Admission Year/File Name");
            return;
        }

        try
        {
            string fullPath = context.Server.MapPath("~/docs/SVT/Students/" + admissionYear + "/" + collegeRegistrationNumber + "/" + p);

            context.Response.ContentType = "application/pdf";
            context.Response.AppendHeader("content-disposition", "attachment; filename=\"" + (new System.IO.FileInfo(fullPath)).Name + "\"");

            context.Response.WriteFile(fullPath);
            context.Response.End();
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: While downloading the document. " + ex.Message);
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