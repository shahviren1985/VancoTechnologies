<%@ WebHandler Language="C#" Class="FileDownload" %>

using System;
using Util;
using System.Web;
using System.IO;

public class FileDownload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        var filePath = HttpUtility.UrlDecode(context.Request.QueryString["filePath"].ToString());
        var fileName = context.Request.QueryString["fileName"].ToString();
        try
        {
            string mediaName = fileName;//"myFile.zip"; // 600MB in file size
            if (string.IsNullOrEmpty(mediaName))
            {
                return;
            }

            //string destPath = context.Server.MapPath("~/Downloads/" + mediaName);
            string destPath = filePath;
            // Check to see if file exist
            FileInfo fi = new FileInfo(destPath);

            try
            {
                if (fi.Exists)
                {
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ClearContent();
                    //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.TransmitFile(fi.FullName);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write(exception.Message);
            }
            finally
            {
                HttpContext.Current.Response.End();
            }                  
        }
        catch (Exception ex)
        {
            
        }          
    }
     
    public bool IsReusable {
        get {
            return false;
        }
    }
}