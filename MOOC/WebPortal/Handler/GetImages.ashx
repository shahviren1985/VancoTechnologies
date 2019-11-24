<%@ WebHandler Language="C#" Class="GetImages" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;

public class GetImages : IHttpHandler, IRequiresSessionState
{
    JavaScriptSerializer jss = new JavaScriptSerializer();
    Logger logger = new Logger();

    string cnxnString = string.Empty;
    string logPath = string.Empty;
    string configFilePath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {        
        context.Response.AppendHeader("Access-Control-Allow-Origin", "*");

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
            
            //string folderPath = ConfigurationManager.AppSettings["BASE_PATH"] + "mooc-gellary\\" + context.Session["CollegeName"] + "\\" + context.Session["UserType"] + "\\" + context.Session["UserId"];
            string folderPath = context.Server.MapPath("../mooc-gellary\\" + context.Session["CollegeName"] + "\\" + context.Session["UserType"] + "\\" + context.Session["UserId"]);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            FileInfo[] files = dirInfo.GetFiles();

            List<ImageDetails> imageList = new List<ImageDetails>();
            int counter = 1;
            foreach (FileInfo file in files)
            {
                ImageDetails imageDetails = new ImageDetails();                
                imageDetails.ImagePath = Util.BASE_URL + "mooc-gellary/" + context.Session["CollegeName"] + "/" + context.Session["UserType"] + "/" + context.Session["UserId"] + "/" + file.Name;
                imageDetails.Id = counter.ToString();
                imageList.Add(imageDetails);
                counter++;
            }

            context.Response.Write(jss.Serialize(new { Status = "Ok", Message = "images successfully retirved", Images = imageList }));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            logger.Error("GetImages-Handler", "ProcessRequest", "Error occured while getting images from mooc server", ex, logPath);
            context.Response.Write(jss.Serialize(new { Status = "Error", Message = "Sorry! An error occured while getting images from mooc server" }));
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