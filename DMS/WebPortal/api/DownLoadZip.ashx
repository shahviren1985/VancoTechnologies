<%@ WebHandler Language="C#" Class="DownLoadZip" %>

using System;
using System.Web;
using System.Configuration;
using System.IO;
using Ionic.Zip;
using AA.DAO;
using System.Web.SessionState;
public class DownLoadZip : IHttpHandler, IRequiresSessionState
{
    private bool isUseNetworkPath = false;

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();
        string college = context.Session["College"].ToString();

        string path = string.Empty;

        if (ConfigurationManager.AppSettings["IsUseNetworkPath"] != null)
        {
            isUseNetworkPath = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseNetworkPath"]);
        }

        if (isUseNetworkPath)
        {
            path = ConfigurationManager.AppSettings["DMS_PATH"];
        }
        else
        {
            //path = context.Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]);
            path = context.Server.MapPath("~/Directories/" + college);
            
        }

        int folderId;
        string id = Convert.ToString(context.Request.QueryString["folderId"].ToString());

        if (int.TryParse(id, out folderId))
        {
            FolderDetails folder = new FolderDetailsDAO().GetSingleFileDetails(folderId, cnxnString, logPath);

            path = Path.Combine(path, new FileDetailsDAO().GetPathHirarchy(folder.Id, cnxnString, logPath));

            context.Response.ContentType = "application/zip";
            context.Response.AddHeader("content-disposition", "attachment; filename=" + folder.Alias + ".zip");

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                //zip.AddSelectedFiles("*.*", path, "", false);
                //zip.AddSelectedFiles("*.*", path, folder.Alias);
                zip.AddDirectory(path);
                //zip.AddDirectory(path);
                zip.Save(context.Response.OutputStream);
            }
            //context.Response.Close();
        }
        //context.Response.ContentType = "text/plain";
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