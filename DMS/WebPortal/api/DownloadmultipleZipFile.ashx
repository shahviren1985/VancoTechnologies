<%@ WebHandler Language="C#" Class="DownloadmultipleZipFile" %>

using System;
using System.Web;
using System.Configuration;
using System.IO;
using Ionic.Zip;
using AA.DAO;
using System.Web.SessionState;

public class DownloadmultipleZipFile : IHttpHandler, IRequiresSessionState
{
    private bool isUseNetworkPath = false;


    public void ProcessRequest(HttpContext context)
    {
        string fixedpath = string.Empty;
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string college = context.Session["College"].ToString();
        
        if (ConfigurationManager.AppSettings["IsUseNetworkPath"] != null)
        {
            isUseNetworkPath = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseNetworkPath"]);
        }

        if (isUseNetworkPath)
        {
            fixedpath = ConfigurationManager.AppSettings["DMS_PATH"];
        }
        else
        {
            //fixedpath = context.Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]);
            fixedpath = context.Server.MapPath("~/Directories/" + college);
        }
        
        int folderId;
        string id = Convert.ToString(context.Request.QueryString["Id"].ToString());
        
        string serachfolderpath = fixedpath + @"\Serachfile";
        
        string[] filearray = id.Split(',');
        string[] patharray;
        string foldername = string.Empty;

        if (!System.IO.Directory.Exists(serachfolderpath))
        {
            System.IO.Directory.CreateDirectory(serachfolderpath);
        }
        else
        {
            Directory.Delete(serachfolderpath, true);
            Directory.CreateDirectory(serachfolderpath);
        }

        foreach (string folderid in filearray)
        {
            string path = string.Empty;
            if (int.TryParse(folderid, out folderId))
            {
                string file = new FileDetailsDAO().Getfilepath(folderId, cnxnString, logPath, college);
                string filename;
                filename = Path.GetFileName(file);
                string destFile = System.IO.Path.Combine(serachfolderpath, filename);
                File.Copy(file, destFile);
                foldername = filename;
            }
        }

        if (Directory.Exists(serachfolderpath))
        {
            context.Response.ContentType = "application/zip";
            context.Response.AddHeader("content-disposition", "attachment; filename=" + foldername + ".zip");
            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                zip.AddDirectory(serachfolderpath);
                zip.Save(context.Response.OutputStream);
                context.Response.End();
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