<%@ WebHandler Language="C#" Class="DownloadmultipleZip" %>

using System;
using System.Web;
using System.Configuration;
using System.IO;
using Ionic.Zip;
using System.Web.SessionState;
using AA.DAO;
public class DownloadmultipleZip : IHttpHandler, IRequiresSessionState
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
        string serachfolderpath = fixedpath + @"\SerachFolder";
        string[] filearray = id.Split(',');
        string foldername = string.Empty;
        System.Collections.Generic.List<string> fileIdCollection = new System.Collections.Generic.List<string>();
        foreach (string folderid in filearray)
        {
            string path = string.Empty;
            if (int.TryParse(folderid, out folderId))
            {
                FolderDetails folder = new FolderDetailsDAO().GetSingleFileDetails(folderId, cnxnString, logPath);
                path = Path.Combine(fixedpath, new FileDetailsDAO().GetPathHirarchy(folder.Id, cnxnString, logPath));
                new CopyDirectories().DirectoryCopy(path, serachfolderpath, true);
                fileIdCollection.Add(path);
                foldername = folder.Alias;
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