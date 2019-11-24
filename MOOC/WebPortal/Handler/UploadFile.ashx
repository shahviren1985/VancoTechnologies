<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using ITM.Courses.LogManager;

public class UploadFile : IHttpHandler, IRequiresSessionState
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();
    Logger logger = new Logger();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request.QueryString["type"];
        string banner = context.Request.QueryString["banner"];

        context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        
        if (!string.IsNullOrEmpty(context.Request.Headers["X-File-Name"]))
        {
            try
            {
                if (context.Session["ConnectionString"] == null)
                {
                    context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Session Expire" }));
                    return;
                }
                else
                {
                    cnxnString = context.Session["ConnectionString"].ToString();
                    logPath = context.Server.MapPath(context.Session["LogFilePath"].ToString());
                    configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());
                }

                string path = string.Empty;

                //string folderPath = ConfigurationManager.AppSettings["BASE_PATH"] + "mooc-gellary\\" + context.Session["CollegeName"] + "\\" + context.Session["UserType"] + "\\" + context.Session["UserId"] + "\\";
                string folderPath = context.Server.MapPath("../mooc-gellary\\" + context.Session["CollegeName"] + "\\" + context.Session["UserType"] + "\\" + context.Session["UserId"]);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); 
                }
                
                path = Path.Combine(folderPath, context.Request.Headers["X-File-Name"]);

                if (File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    //File.Move(path, path.Replace(info.Extension, "") + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + info.Extension);
                }

                Stream inputStream = context.Request.InputStream;
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                inputStream.CopyTo(fileStream);
                fileStream.Close();

                string fileUrl = Util.BASE_URL + "mooc-gellary/" + context.Session["CollegeName"] + "/staff/" + context.Session["UserId"] + "/" + context.Request.Headers["X-File-Name"];

                context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "file uploaded successfully", FileUrl = fileUrl }));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                logger.Error("UploadFile-Handler", "ProcessRequest", "Error occured while uploading image on mooc server", ex, logPath);
                context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Sorry! An error occured while uploading image on mooc server" }));
            }
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