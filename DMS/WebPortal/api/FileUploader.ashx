<%@ WebHandler Language="C#" Class="FileUploader" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

public class FileUploader : IHttpHandler, IRequiresSessionState
{
    string userName = string.Empty;
    string college = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        userName = context.Session["UserName"].ToString();
        //added by vasim for SaaS
        college = context.Session["College"].ToString();

        long ticks = DateTime.Now.Ticks;

        if (!string.IsNullOrEmpty(context.Request.Headers["X-File-Name"]))
        {
            try
            {
                string type = context.Request.QueryString["t"];

                string fileName = context.Request.Headers["X-File-Name"];
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);

                if (!string.IsNullOrEmpty(fileName))
                {
                    if (string.IsNullOrEmpty(type))
                    {
                        if (!Directory.Exists(context.Server.MapPath("~/docs/" + college + "/" + userName)))
                        {
                            Directory.CreateDirectory(context.Server.MapPath("~/docs/" + college + "/" + userName));
                        }

                        string path = context.Server.MapPath("~/docs/" + college + "/" + userName + "/" + fileNameWithoutExt + "_" + ticks + ext);

                        Stream inputStream = context.Request.InputStream;
                        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                        Util.Utilities.CopyFile(inputStream, fileStream);

                        fileStream.Close();
                        
                        context.Response.Write("docs/" + college + "/" + userName + "/" + fileNameWithoutExt + "_" + ticks + ext);
                    }
                    else if (type.ToLower() == "marathi")
                    {
                        if (!Directory.Exists(context.Server.MapPath("~/marathi-docs/" + college + "/" + userName)))
                        {
                            Directory.CreateDirectory(context.Server.MapPath("~/marathi-docs/" + college + "/" + userName));
                        }

                        string path = context.Server.MapPath("~/marathi-docs/" + college + "/" + userName + "/" + fileNameWithoutExt + "_" + ticks + ext);

                        Stream inputStream = context.Request.InputStream;
                        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                        Util.Utilities.CopyFile(inputStream, fileStream);

                        fileStream.Close();
                        
                        context.Response.Write("marathi-docs/" + college + "/" + userName + "/" + fileNameWithoutExt + "_" + ticks + ext);
                    }
                    else
                    {
                        string filePath = "~/Directories/" + college + "/outward/";

                        if (!Directory.Exists(context.Server.MapPath(filePath)))
                        {
                            Directory.CreateDirectory(context.Server.MapPath(filePath));
                        }

                        filePath = Path.Combine(context.Server.MapPath(filePath), type);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        Stream inputStream = context.Request.InputStream;
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        Util.Utilities.CopyFile(inputStream, fileStream);

                        fileStream.Close();

                        context.Response.Write("~/Directories/" + college + "/outward/" + type);
                    }
                }
                else
                {
                    context.Response.Write("Error : Please select the scanned document.");
                    return;
                }

            }
            catch (Exception ex)
            {
                context.Response.Write("Error : An error occurred while uploading the file." + ex);
            }
        }
        else if (context.Request.Files.Count > 0)
        {

            // IFrame based saving/uploading the images. Currently commenting it out - will use it later
            #region Commented Code
            /*try
            {
                if (context.Request.Files.Count == 0)
                    return;

                HttpPostedFile file = context.Request.Files[0];
                FileInfo fileInfo = new FileInfo(file.FileName);
                string path = context.Server.MapPath("~/docs/" + userName + "/" + fileInfo.Name);

                Stream inputStream = file.InputStream;
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                Util.Utilities.CopyFile(inputStream, fileStream);
                string extension = Path.GetExtension(path);
                string ticks = DateTime.Now.Ticks.ToString();
                string newPath = path + "_" + ticks + extension;
                fileStream.Close();
                File.Move(path, newPath);
                string imgSrc = "docs/" + userName + "/" + fileInfo.Name + "_" + ticks + extension;
                context.Response.Redirect("FileUpload.htm?p=" + imgSrc);
            }
            catch (Exception ex)
            {
                context.Response.Write("Error : An error occurred while upload your Logo.");
            }
            */
            #endregion
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