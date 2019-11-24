using AA.ConfigurationsManager;
using AA.DAO;
using AA.LogManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Util;

public partial class DocumentManagement_InnerFolder : Page, IRequiresSessionState
{
    private bool accessright;
    public string BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];

    private string cnxnString = string.Empty;
    private string college = string.Empty;

    public string isStaff = string.Empty;
    private bool isUseNetworkPath;

    private Logger logger = new Logger();
    private string logPath = string.Empty;
    public int parentId = -1;

    public int selectedFilesCount;

    private string userName = string.Empty;

    private void AddFileHeader()
    {
        HtmlGenericControl child = new HtmlGenericControl("div");
        HtmlGenericControl control2 = new HtmlGenericControl("div");
        HtmlGenericControl control3 = new HtmlGenericControl("div");
        HtmlGenericControl control4 = new HtmlGenericControl("div");
        HtmlGenericControl control5 = new HtmlGenericControl("div");
        HtmlGenericControl control6 = new HtmlGenericControl("div");
        HtmlGenericControl control7 = new HtmlGenericControl("div");
        HtmlGenericControl control8 = new HtmlGenericControl("div");
        control4.Style["width"] = "80px";
        control6.Style["width"] = "163px";
        control5.Style["width"] = "163px";
        control2.InnerHtml = "<u>File Name</u>";
        control2.Attributes.Add("class", "DivSortButton");
        control2.Attributes.Add("onClick", "SortButtonClick('" + this.btnSortFileName.ClientID + "');");
        control6.InnerHtml = "File Description";
        control3.InnerHtml = "History";
        control7.InnerHtml = "<u>Owner</u>";
        control7.Attributes.Add("class", "DivSortButton");
        control7.Attributes.Add("onClick", "SortButtonClick('" + this.btnSortFileOwner.ClientID + "');");
        control5.InnerHtml = "Last Updated On";
        control4.InnerHtml = "<u>Created On</u>";
        control4.Attributes.Add("class", "DivSortButton");
        control4.Attributes.Add("onClick", "SortButtonClick('" + this.btnSortFileDate.ClientID + "');");
        control8.InnerHtml = "Tags";
        child.Controls.Add(control2);
        child.Controls.Add(control6);
        child.Controls.Add(control5);
        child.Controls.Add(control7);
        child.Controls.Add(control8);
        child.Controls.Add(control3);
        child.Attributes.Add("class", "FolderHeader");
        child.Attributes.Add("style", "font-weight: bold; border-bottom: 1px solid #CCC; padding-bottom: 5px; ");
        this.Files.InnerHtml = string.Empty;
        this.Files.Controls.Add(child);
    }

    private void AddFileRecord(FileDetails file, bool isColorBackground = false)
    {
        bool flag;
        string str;
        bool flag2 = this.IsAccessPermission(file.OwnerUserId, file.UserAccess, file.Permission, out flag);
        HtmlGenericControl child = new HtmlGenericControl("div");
        HtmlGenericControl control2 = new HtmlGenericControl("div");
        HtmlGenericControl control3 = new HtmlGenericControl("div");
        HtmlGenericControl control4 = new HtmlGenericControl("div");
        HtmlGenericControl control5 = new HtmlGenericControl("div");
        HtmlGenericControl control6 = new HtmlGenericControl("div");
        HtmlGenericControl control7 = new HtmlGenericControl("div");
        HtmlGenericControl control8 = new HtmlGenericControl("div");
        HtmlGenericControl control9 = new HtmlGenericControl("div");

        control9.Style["width"] = "100px";
        control4.Style["width"] = "100px";
        control6.Style["width"] = "163px";
        string text1 = string.Concat(new object[] { "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFile('", file.Id, "','", file.OriginalFileName, "');\">Rename</div>&nbsp;&nbsp; " });
        string text2 = string.Concat(new object[] { "<div onclick=\"DeleteFile('", file.Id, "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='width:40px;cursor: pointer;'>Delete</div>" });
        file.RelativePath = file.RelativePath.Replace("~/", "");
        if (this.isUseNetworkPath)
        {
            str = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], file.RelativePath);
        }
        else
        {
            str = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), file.RelativePath);
        }
        control2.InnerHtml = "<a style='vertical-align:top;' href='" + this.BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + str + "' title='" + file.OriginalFileName + "'> <img src='" + this.BASE_URL + "/static/images/file.png' width='12px' height='12px' style='padding: 5px; float: left' />" + ((file.OriginalFileName.Length > 30) ? (file.OriginalFileName.Remove(30, file.OriginalFileName.Length - 30) + "...") : file.OriginalFileName) + "</a>";
        control6.InnerHtml = (file.FileDescription.Length > 0x37) ? (file.FileDescription.Remove(0x37, file.FileDescription.Length - 0x37).Trim() + "...") : file.FileDescription;
        control6.Attributes.Add("title", file.FileDescription);
        HtmlGenericControl control10 = new HtmlGenericControl("div");
        control10.Style["width"] = "220px";
        control10.InnerHtml = "<div style='width:90px;cursor: pointer;'> <a href='javascript:void(0);'  onclick=$('#div" + file.Id + "_MoreFile').fadeIn(1000); >Options...</a></div> ";
        string str2 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL,
            "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str3 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('", file.Id,
            "','", file.OriginalFileName, "');\">Rename</div><div onclick=\"DeleteFile('", file.Id, "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div> <div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str4 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('", file.Id,
            "','", file.OriginalFileName, "');\">Rename</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str5 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('", file.Id, "','", file.OriginalFileName, "');\">Rename</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str,
            "'>Download</a></div> </ul> </div>"
        });
        string text3 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul style='padding-left: 75;'><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div> <div onclick=\"DeleteFile('", file.Id,
            "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str6 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div onclick=\"DeleteFile('", file.Id, "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName,
            "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        if (flag)
        {
            control10.InnerHtml = control10.InnerHtml + str3;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (!flag && (file.Permission.ToLower() == "rename"))
        {
            control10.InnerHtml = control10.InnerHtml + str5;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (file.Permission.ToLower() == "delete")
        {
            control10.InnerHtml = control10.InnerHtml + str6;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (file.Permission.ToLower() == "share")
        {
            control10.InnerHtml = control10.InnerHtml + str2;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (file.Permission.ToLower() == "update")
        {
            control10.InnerHtml = control10.InnerHtml + str4;
            control9.InnerHtml = control10.InnerHtml;
        }
        control3.InnerHtml = "<a href='javascript:void(0);' onclick=$('#divF" + file.Id + "').fadeIn(1000); >View History</a>";
        string str7 = string.Concat(new object[] { "<div id='p2' style='top: 3px;'> <div id='divF", file.Id, "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'><img src='", this.BASE_URL, "/static/images/close.png' title='Close File History' onclick=$('#divF", file.Id, "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;margin-left: -30px;margin-top: 6px;'>", this.GetHistory(file.History), "</ul> </div></div>" });
        control3.InnerHtml = control3.InnerHtml + str7;
        UserDetails details = new UserDetailsDAO().GetUserDetailbyId(file.OwnerUserId, this.cnxnString, this.logPath);
        if (details != null)
        {
            if (details.UserName == this.Session["UserName"].ToString())
            {
                control7.InnerHtml = "You";
            }
            else
            {
                control7.InnerHtml = details.FirstName + " " + details.LastName;
            }
        }
        else
        {
            control7.InnerHtml = "Created by Exam or MIS";
        }
        control5.InnerHtml = file.DateUpdated.ToString("dd/MM/yyyy HH:mm tt");
        control4.InnerHtml = file.DateCreated.ToString("dd/MM/yyyy HH:mm tt");
        if (file.Tags == string.Empty)
        {
            control8.InnerHtml = "--";
            control8.Attributes.Add("title", "no any tag present");
        }
        else
        {
            control8.InnerHtml = (file.Tags.Length > 40) ? (file.Tags.Remove(40, file.Tags.Length - 40).Trim() + "...") : file.Tags;
            control8.Attributes.Add("title", file.Tags);
        }
        control3.Style["width"] = "95px";
        child.Controls.Add(control2);
        child.Controls.Add(control6);
        child.Controls.Add(control5);
        child.Controls.Add(control7);
        child.Controls.Add(control8);
        child.Controls.Add(control3);
        child.Controls.Add(control9);
        child.Attributes.Add("class", "FolderHeader");
        if (isColorBackground)
        {
            child.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            child.Attributes.Add("style", "padding-top: 5px;");
        }
        if (flag)
        {
            this.Files.Controls.Add(child);
        }
        else if (flag2 && (file.Permission.ToLower() != "private"))
        {
            this.Files.Controls.Add(child);
        }
        else if (file.Permission.ToLower() == "read")
        {
            this.Files.Controls.Add(child);
        }
    }

    private void AddFileRecordSearch(FileDetails file, bool isColorBackground = false)
    {
        bool flag;
        string str;
        bool flag2 = this.IsAccessPermission(file.OwnerUserId, file.UserAccess, file.Permission, out flag);
        HtmlGenericControl child = new HtmlGenericControl("div");
        HtmlGenericControl control2 = new HtmlGenericControl("div");
        HtmlGenericControl control3 = new HtmlGenericControl("div");
        HtmlGenericControl control4 = new HtmlGenericControl("div");
        HtmlGenericControl control5 = new HtmlGenericControl("div");
        HtmlGenericControl control6 = new HtmlGenericControl("div");
        HtmlGenericControl control7 = new HtmlGenericControl("div");
        HtmlGenericControl control8 = new HtmlGenericControl("div");
        HtmlGenericControl control9 = new HtmlGenericControl("div");
        control9.Style["width"] = "100px";
        control4.Style["width"] = "100px";
        control6.Style["width"] = "163px";
        string text1 = string.Concat(new object[] { "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFile('", file.Id, "','", file.OriginalFileName, "');\">Rename</div>&nbsp;&nbsp; " });
        string text2 = string.Concat(new object[] { "<div onclick=\"DeleteFile('", file.Id, "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='width:40px;cursor: pointer;'>Delete</div>" });
        file.RelativePath = file.RelativePath.Replace("~/", "");
        if (this.isUseNetworkPath)
        {
            str = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], file.RelativePath);
        }
        else
        {
            str = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), file.RelativePath);
        }
        control2.InnerHtml = string.Concat(new object[] { "<input type='checkbox' id='", file.Id, "' style='width: 13px;float:left;' class='filecheckbox' /><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "' title='", file.OriginalFileName, "'> <img src='", this.BASE_URL, "/static/images/file.png' width='12px' height='12px' style='padding: 5px; float: left' />", (file.OriginalFileName.Length > 30) ? (file.OriginalFileName.Remove(30, file.OriginalFileName.Length - 30) + "...") : file.OriginalFileName, "</a>" });
        control6.InnerHtml = (file.FileDescription.Length > 0x23) ? (file.FileDescription.Remove(0x23, file.FileDescription.Length - 0x23).Trim() + "...") : file.FileDescription;
        control6.Attributes.Add("title", file.FileDescription);
        HtmlGenericControl control10 = new HtmlGenericControl("div");
        control10.Style["width"] = "220px";
        control10.InnerHtml = "<div style='width:90px;cursor: pointer;'> <a href='javascript:void(0);'  onclick=$('#div" + file.Id + "_MoreFile').fadeIn(1000); >Options...</a></div> ";

        string str2 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL,
            "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str3 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/image/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('", file.Id,
            "','", file.OriginalFileName, "');\">Rename</div><div onclick=\"DeleteFile('", file.Id, "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div> <div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str4 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('", file.Id,
            "','", file.OriginalFileName, "');\">Rename</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str5 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('", file.Id, "','", file.OriginalFileName, "');\">Rename</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str,
            "'>Download</a></div> </ul> </div>"
        });
        string text3 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul style='padding-left: 75;'><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFile('", file.Id, "','", this.btnCutFile.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('", file.Id, "','", this.btnCopyFile.ClientID, "');\">Copy</div> <div onclick=\"DeleteFile('", file.Id,
            "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName, "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        string str6 = string.Concat(new object[] { 
            "<div id='div", file.Id, "_MoreFile'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px; '> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", file.Id, "_MoreFile').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div onclick=\"DeleteFile('", file.Id, "','", file.OriginalFileName, "','", this.btnDeleteFiles.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='", this.BASE_URL, "/api/FileDownload.ashx?fileName=", file.OriginalFileName,
            "&filePath=", str, "'>Download</a></div></ul> </div>"
        });
        if (flag)
        {
            control10.InnerHtml = control10.InnerHtml + str3;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (!flag && (file.Permission.ToLower() == "rename"))
        {
            control10.InnerHtml = control10.InnerHtml + str5;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (file.Permission.ToLower() == "delete")
        {
            control10.InnerHtml = control10.InnerHtml + str6;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (file.Permission.ToLower() == "share")
        {
            control10.InnerHtml = control10.InnerHtml + str2;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (file.Permission.ToLower() == "update")
        {
            control10.InnerHtml = control10.InnerHtml + str4;
            control9.InnerHtml = control10.InnerHtml;
        }
        control3.InnerHtml = "<a href='javascript:void(0);' onclick=$('#divF" + file.Id + "').fadeIn(1000); >View History</a>";
        string str7 = string.Concat(new object[] { "<div id='p2' style='top: 3px;'> <div id='divF", file.Id, "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'><img src='", this.BASE_URL, "/static/images/close.png' title='Close File History' onclick=$('#divF", file.Id, "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;margin-left: -30px;margin-top: 6px;'>", this.GetHistory(file.History), "</ul> </div></div>" });
        control3.InnerHtml = control3.InnerHtml + str7;
        UserDetails details = new UserDetailsDAO().GetUserDetailbyId(file.OwnerUserId, this.cnxnString, this.logPath);
        if (details != null)
        {
            if (details.UserName == this.Session["UserName"].ToString())
            {
                control7.InnerHtml = "You";
            }
            else
            {
                control7.InnerHtml = details.FirstName + " " + details.LastName;
            }
        }
        else
        {
            control7.InnerHtml = "Created by Exam or MIS";
        }
        control5.InnerHtml = file.DateUpdated.ToString("dd/MM/yyyy HH:mm tt");
        control4.InnerHtml = file.DateCreated.ToString("dd/MM/yyyy HH:mm tt");
        control8.InnerHtml = (file.Tags == string.Empty) ? "--" : file.Tags;
        control3.Style["width"] = "95px";
        child.Controls.Add(control2);
        child.Controls.Add(control6);
        child.Controls.Add(control4);
        child.Controls.Add(control5);
        child.Controls.Add(control7);
        child.Controls.Add(control8);
        child.Controls.Add(control3);
        child.Controls.Add(control9);
        child.Attributes.Add("class", "FolderHeader");
        if (isColorBackground)
        {
            child.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            child.Attributes.Add("style", "padding-top: 5px;");
        }
        if (flag)
        {
            this.Files.Controls.Add(child);
        }
        else if (flag2 && (file.Permission.ToLower() != "private"))
        {
            this.Files.Controls.Add(child);
        }
    }

    private void AddFolderHeader()
    {
        HtmlGenericControl child = new HtmlGenericControl("div");
        HtmlGenericControl control2 = new HtmlGenericControl("div");
        HtmlGenericControl control3 = new HtmlGenericControl("div");
        HtmlGenericControl control4 = new HtmlGenericControl("div");
        HtmlGenericControl control5 = new HtmlGenericControl("div");
        HtmlGenericControl control6 = new HtmlGenericControl("div");
        HtmlGenericControl control7 = new HtmlGenericControl("div");
        HtmlGenericControl control8 = new HtmlGenericControl("div");
        control4.Style["width"] = "80px";
        control6.Style["width"] = "163px";
        control5.Style["width"] = "160px";
        control2.InnerHtml = "<u>Folder Name</u>";
        control2.Attributes.Add("class", "DivSortButton");
        control2.Attributes.Add("title", "click to sort");
        control2.Attributes.Add("onClick", "SortButtonClick('" + this.btnSortFolderName.ClientID + "');");
        control6.InnerHtml = "Folder Description";
        control3.InnerHtml = "History";
        control7.InnerHtml = "<u>Owner</u>";
        control7.Attributes.Add("class", "DivSortButton");
        control7.Attributes.Add("onClick", "SortButtonClick('" + this.btnSortFolderOwner.ClientID + "');");
        control5.InnerHtml = "Last Updated On";
        control4.InnerHtml = "<u>Created On</u>";
        control4.Attributes.Add("class", "DivSortButton");
        control4.Attributes.Add("onClick", "SortButtonClick('" + this.btnSortFolderDate.ClientID + "');");
        control8.InnerHtml = "Tags";
        child.Controls.Add(control2);
        child.Controls.Add(control6);
        child.Controls.Add(control4);
        child.Controls.Add(control5);
        child.Controls.Add(control8);
        child.Controls.Add(control3);
        child.Attributes.Add("class", "FolderHeader");
        child.Attributes.Add("style", "font-weight: bold; border-bottom: 1px solid #CCC; padding-bottom: 5px; ");
        this.Folders.InnerHtml = string.Empty;
        this.Folders.Controls.Add(child);
    }

    private void AddFolderRecord(FolderDetails folder, bool isColorBackground = false)
    {
        bool flag;
        bool flag2 = this.IsAccessPermission(folder.OwnerUserId, folder.UserAccess, folder.Permission, out flag);
        HtmlGenericControl child = new HtmlGenericControl("div");
        HtmlGenericControl control2 = new HtmlGenericControl("div");
        HtmlGenericControl control3 = new HtmlGenericControl("div");
        HtmlGenericControl control4 = new HtmlGenericControl("div");
        HtmlGenericControl control5 = new HtmlGenericControl("div");
        HtmlGenericControl control6 = new HtmlGenericControl("div");
        HtmlGenericControl control7 = new HtmlGenericControl("div");
        HtmlGenericControl control8 = new HtmlGenericControl("div");
        HtmlGenericControl control9 = new HtmlGenericControl("div");
        control9.Style["width"] = "100px";
        control4.Style["width"] = "100px";
        control6.Style["width"] = "163px";
        control5.Style["width"] = "140px";
        string text1 = string.Concat(new object[] { "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div>&nbsp;&nbsp; " });
        string text2 = string.Concat(new object[] { "<div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='width:40px;cursor: pointer;'>Delete</div>" });
        control9.InnerHtml = string.Empty;
        control2.InnerHtml = string.Concat(new object[] { "<a style='vertical-align:top;' href='InnerFolder.aspx?id=", folder.Id, "&name=", folder.FolderName, "'> <img src='", this.BASE_URL, "/static/images/folder.png' width='12px' height='12px' style='padding: 5px; float: left' /> ", folder.Alias, "</a>" });
        control6.InnerHtml = (folder.FolderDescription.Length > 0x37) ? (folder.FolderDescription.Remove(0x37, folder.FolderDescription.Length - 0x37).Trim() + "...") : folder.FolderDescription;
        control6.Attributes.Add("title", folder.FolderDescription);
        string str = string.Concat(new object[] { "<div> <a href='", this.BASE_URL, "/api/DownLoadZip.ashx?folderId=", folder.Id, "'>Download as Zip</a> </div>" });
        HtmlGenericControl control10 = new HtmlGenericControl("div");
        control10.Style["width"] = "110px";
        control10.InnerHtml = "<div style='width:90px;cursor: pointer;'> <a href='javascript:void(0);'  onclick=$('#div" + folder.Id + "_More').fadeIn(1000); >Options...</a></div> ";

        string str2 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li></ul> </div>"
        });
        string str3 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div><div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div>", str, " <div style='padding-bottom: 5px;cursor: pointer;'> <a href='javascript:void(0)' onclick=\"ClickEdit('", folder.Id,
            "','", this.btnEditFolder.ClientID, "');\" style='color:black;'>Edit</a></div> </ul> </div>"
        });
        string text3 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li><div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div>", str, "</ul> </div>"
        });
        string str4 = string.Concat(new object[] { "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div>", str, "</ul> </div>" });
        string str5 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div>", str, "</ul> </div>"
        });
        string str6 = string.Concat(new object[] { "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div>", str, "</ul> </div>" });
        if (flag)
        {
            control10.InnerHtml = control10.InnerHtml + str3;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "rename")
        {
            control10.InnerHtml = control10.InnerHtml + str6;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "delete")
        {
            control10.InnerHtml = control10.InnerHtml + str4;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "share")
        {
            control10.InnerHtml = control10.InnerHtml + str2;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "update")
        {
            control10.InnerHtml = control10.InnerHtml + str5;
            control9.InnerHtml = control10.InnerHtml;
        }
        control3.InnerHtml = "<a href='javascript:void(0);' onclick=$('#div" + folder.Id + "').fadeIn(1000); >View History</a>";
        string str7 = string.Concat(new object[] { "<div id='p2' style='top: 3px;'> <div id='div", folder.Id, "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'><img src='", this.BASE_URL, "/static/images/close.png' title='Close Folder History' onclick=$('#div", folder.Id, "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/><div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;margin-left: -30px;margin-top: 6px;'>", this.GetHistory(folder.History), "</ul> </div></div>" });
        control3.InnerHtml = control3.InnerHtml + str7;
        UserDetails details = new UserDetailsDAO().GetUserDetailbyId(folder.OwnerUserId, this.cnxnString, this.logPath);
        if (details != null)
        {
            if (details.UserName.ToLower() == this.Session["UserName"].ToString().ToLower())
            {
                control7.InnerHtml = "You";
            }
            else
            {
                control7.InnerHtml = details.FirstName + " " + details.LastName;
            }
        }
        else
        {
            control7.InnerHtml = "Created by Exam or MIS";
        }
        control5.InnerHtml = folder.DateUpdated.ToString("dd/MM/yyyy");
        control4.InnerHtml = folder.DateCreated.ToString("dd/MM/yyyy");
        if (folder.Tags == string.Empty)
        {
            control8.InnerHtml = "--";
            control8.Attributes.Add("title", "tags not added");
        }
        else
        {
            control8.InnerHtml = (folder.Tags.Length > 40) ? (folder.Tags.Remove(40, folder.Tags.Length - 40).Trim() + "...") : folder.Tags;
            control8.Attributes.Add("title", folder.Tags);
        }
        control3.Style["width"] = "95px";
        child.Controls.Add(control2);
        child.Controls.Add(control6);
        child.Controls.Add(control4);
        child.Controls.Add(control5);
        child.Controls.Add(control8);
        child.Controls.Add(control3);
        child.Controls.Add(control9);
        child.Attributes.Add("class", "FolderHeader");
        if (isColorBackground)
        {
            child.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            child.Attributes.Add("style", "padding-top: 5px;");
        }
        if (flag)
        {
            this.Folders.Controls.Add(child);
        }
        else if (flag2 && (folder.Permission.ToLower() != "private"))
        {
            this.Folders.Controls.Add(child);
        }
        else if (!flag2 && string.IsNullOrEmpty(folder.Permission))
        {
            this.Folders.Controls.Add(child);
        }
    }

    private void AddFolderRecordSearch(FolderDetails folder, bool isColorBackground = false)
    {
        bool flag;
        bool flag2 = this.IsAccessPermission(folder.OwnerUserId, folder.UserAccess, folder.Permission, out flag);
        HtmlGenericControl child = new HtmlGenericControl("div");
        HtmlGenericControl control2 = new HtmlGenericControl("div");
        HtmlGenericControl control3 = new HtmlGenericControl("div");
        HtmlGenericControl control4 = new HtmlGenericControl("div");
        HtmlGenericControl control5 = new HtmlGenericControl("div");
        HtmlGenericControl control6 = new HtmlGenericControl("div");
        HtmlGenericControl control7 = new HtmlGenericControl("div");
        HtmlGenericControl control8 = new HtmlGenericControl("div");
        HtmlGenericControl control9 = new HtmlGenericControl("div");
        control9.Style["width"] = "100px";
        control4.Style["width"] = "150px";
        control6.Style["width"] = "163px";
        string text1 = string.Concat(new object[] { "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div>&nbsp;&nbsp; " });
        string text2 = string.Concat(new object[] { "<div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='width:40px;cursor: pointer;'>Delete</div>" });
        control9.InnerHtml = string.Empty;
        control2.InnerHtml = string.Concat(new object[] { "<input type='checkbox' id='", folder.Id, "' style='width: 13px;float:left;' class='foldercheckbox' /><a style='vertical-align:top;' href='InnerFolder.aspx?id=", folder.Id, "'> <img src='", this.BASE_URL, "/static/images/folder.png' width='12px' height='12px' style='padding: 5px; float: left' /> ", folder.Alias, "</a>" });
        control6.InnerHtml = (folder.FolderDescription.Length > 0x23) ? (folder.FolderDescription.Remove(0x23, folder.FolderDescription.Length - 0x23).Trim() + "...") : folder.FolderDescription;
        control6.Attributes.Add("title", folder.FolderDescription);
        string str = string.Concat(new object[] { "<div> <a href='", this.BASE_URL, "/api/DownLoadZip.ashx?folderId=", folder.Id, "'>Download as Zip</a> </div>" });
        HtmlGenericControl control10 = new HtmlGenericControl("div");
        control10.Style["width"] = "110px";
        control10.InnerHtml = "<div style='width:90px;cursor: pointer;'> <a href='javascript:void(0);'  onclick=$('#div" + folder.Id + "_More').fadeIn(1000); >Options...</a></div> ";

        string str2 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li></ul> </div>"
        });
        string str3 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div><div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div>", str, " <div style='padding-bottom: 5px;cursor: pointer;'> <a href='javascript:void(0)' onclick=\"ClickEdit('", folder.Id,
            "','", this.btnEditFolder.ClientID, "');\" style='color:black;'>Edit</a></div> </ul> </div>"
        });
        string text3 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li><div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div>", str, "</ul> </div>"
        });
        string str4 = string.Concat(new object[] { "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div onclick=\"DeleteFolder('", folder.Id, "','", folder.Alias, "','", this.btnDelete.ClientID, "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div>", str, "</ul> </div>" });
        string str5 = string.Concat(new object[] { 
            "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('", folder.Id, "','", this.btnCut.ClientID, "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('", folder.Id, "','", this.btnCopy.ClientID, "');\">Copy</div> <div style='padding-bottom: 5px;cursor: pointer;' onclick=\"PasteFolder('", folder.Id,
            "','", this.btnPaste.ClientID, "');\">Paste</div> <li style='padding-bottom: 5px;list-style:none;'><a href='#'> Share</a></li><li style='padding-bottom: 5px;list-style:none;'><a href='#'> Edit Permission</a></li><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div>", str, "</ul> </div>"
        });
        string str6 = string.Concat(new object[] { "<div id='div", folder.Id, "_More'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 150px; position: absolute;background-color: #FFF; left: 1001px;'> <img src='", this.BASE_URL, "/static/images/close.png' title='Close options' onclick=$('#div", folder.Id, "_More').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> <div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('", folder.Id, "','", folder.Alias, "');\">Rename</div>", str, "</ul> </div>" });
        if (flag)
        {
            control10.InnerHtml = control10.InnerHtml + str3;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "rename")
        {
            control10.InnerHtml = control10.InnerHtml + str6;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "delete")
        {
            control10.InnerHtml = control10.InnerHtml + str4;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "share")
        {
            control10.InnerHtml = control10.InnerHtml + str2;
            control9.InnerHtml = control10.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "update")
        {
            control10.InnerHtml = control10.InnerHtml + str5;
            control9.InnerHtml = control10.InnerHtml;
        }
        control3.InnerHtml = "<a href='javascript:void(0);' onclick=$('#div" + folder.Id + "').fadeIn(1000); >View History</a>";
        string str7 = string.Concat(new object[] { "<div id='p2' style='top: 3px;'> <div id='div", folder.Id, "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'><img src='", this.BASE_URL, "/static/images/close.png' title='Close Folder History' onclick=$('#div", folder.Id, "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/><div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;margin-left: -30px;margin-top: 6px;'>", this.GetHistory(folder.History), "</ul> </div></div>" });
        control3.InnerHtml = control3.InnerHtml + str7;
        UserDetails details = new UserDetailsDAO().GetUserDetailbyId(folder.OwnerUserId, this.cnxnString, this.logPath);
        if (details != null)
        {
            if (details.UserName.ToLower() == this.Session["UserName"].ToString().ToLower())
            {
                control7.InnerHtml = "You";
            }
            else
            {
                control7.InnerHtml = details.FirstName + " " + details.LastName;
            }
        }
        else
        {
            control7.InnerHtml = "Created by Exam or MIS";
        }
        control5.InnerHtml = folder.DateUpdated.ToString("dd/MM/yyyy HH:mm tt");
        control4.InnerHtml = folder.DateCreated.ToString("dd/MM/yyyy HH:mm tt");
        control8.InnerHtml = (folder.Tags == string.Empty) ? "-" : folder.Tags;
        control3.Style["width"] = "95px";
        child.Controls.Add(control2);
        child.Controls.Add(control6);
        child.Controls.Add(control4);
        child.Controls.Add(control5);
        child.Controls.Add(control7);
        child.Controls.Add(control8);
        child.Controls.Add(control3);
        child.Controls.Add(control9);
        child.Attributes.Add("class", "FolderHeader");
        if (isColorBackground)
        {
            child.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            child.Attributes.Add("style", "padding-top: 5px;");
        }
        if (flag)
        {
            this.Folders.Controls.Add(child);
        }
        else if (flag2 && (folder.Permission.ToLower() != "private"))
        {
            this.Folders.Controls.Add(child);
        }
    }

    protected void bntCreateGroup_Click(object s, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(this.txtGroupName.Text))
            {
                this.PopulateFiles();
                this.PopulateFolders();
                this.PopulateGroupUsers();
                this.PopulateGroups();
                this.PopulateUsersList();
            }
            else
            {
                List<string> ids = new List<string>();
                foreach (ListItem item in this.chkGroupUserList.Items)
                {
                    if (item.Selected)
                    {
                        ids.Add(item.Value);
                    }
                }
                if (ids.Count > 0)
                {
                    string staffMembersIdXML = StaffMemberManager.GetStaffMembersIdXML(ids);
                    new GroupsDetailsDAO().AddGroups(this.Session["UserName"].ToString(), this.txtGroupName.Text, DateTime.Now, staffMembersIdXML, this.cnxnString, this.logPath);
                    this.PopulateFiles();
                    this.PopulateFolders();
                    this.PopulateGroupUsers();
                    this.PopulateGroups();
                    this.PopulateUsersList();
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                }
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnAddAllFiles_Click(object sender, EventArgs e)
    {
        try
        {
            if (((this.ViewState["ListAllUploadedFiles"] != null) && (this.ViewState["HashAllFileName"] != null)) && (this.ViewState["TempFiles"] != null))
            {
                Hashtable hashtable = (Hashtable)this.ViewState["HashAllFileName"];
                List<string> list = (List<string>)this.ViewState["ListAllUploadedFiles"];
                Hashtable hashtable2 = (Hashtable)this.ViewState["TempFiles"];
                string str = string.Empty;
                if (this.isUseNetworkPath)
                {
                    str = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], this.GetPathHirarchy());
                }
                else
                {
                    str = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), this.GetPathHirarchy());
                }
                int index = 0;
                string[] strArray = this.hdnDocType.Value.Split(new char[] { '|' });
                string[] strArray2 = this.hdnFileDesc.Value.Split(new char[] { '|' });
                string[] strArray3 = this.hdnFileAccess.Value.Split(new char[] { '|' });
                string[] strArray4 = this.hdnFileTags.Value.Split(new char[] { '|' });
                foreach (string str2 in list)
                {
                    string path = hashtable2[str2].ToString();
                    if (!string.IsNullOrEmpty(path.ToString()) && File.Exists(path))
                    {
                        File.Move(path, str + "/" + str2);
                    }
                    string relativePath = this.GetPathHirarchy() + str2;
                    UserDetails details = new UserDetailsDAO().GetUserDetailList(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
                    FolderHistoryManager manager = new FolderHistoryManager();
                    manager.Date = DateTime.Now;
                    manager.Activity = "File Added";
                    manager.User = this.Session["UserName"].ToString();
                    string history = FolderHistoryManager.CreateHistory(manager);
                    FileDetailsDAO sdao2 = new FileDetailsDAO();
                    sdao2.CreateFileDetails(str2, hashtable[str2].ToString(), strArray2[index], relativePath, DateTime.Now, DateTime.Now, details.Id, this.GetFileUserList(), this.parentId, history, strArray4[index], string.IsNullOrEmpty(strArray3[index]) ? "Read" : strArray3[index], strArray[index], this.cnxnString, this.logPath);
                    int maxFileId = sdao2.GetMaxFileId(this.cnxnString, this.logPath);
                    new UserActivityTrackerDAO().CreateUserActivityDetails("File created", hashtable[str2].ToString(), maxFileId, DateTime.Now, details.UserName, this.cnxnString, this.logPath);
                    FolderHistoryManager his = new FolderHistoryManager();
                    his.Activity = "Add File (" + hashtable[str2].ToString() + ")";
                    his.Date = DateTime.Now;
                    his.User = this.Session["UserName"].ToString();
                    this.UpdateLastModifyDate(his);
                    index++;
                }
                this.divFileResult.InnerHtml = "Selected files has been uploaded successfully";
                this.divFileResult.Style.Add("color", "green");
                this.divAllFilesResult.InnerHtml = "Selected files has been uploaded successfully";
                this.divAllFilesResult.Style.Add("color", "green");
                this.divAllFilesResult.Visible = true;
                this.btnAddAllFiles.Visible = false;
                this.btnAllFilesCancel.Visible = false;
                this.PopulateFolders();
                this.PopulateFiles();
                this.ClearFields();
            }
        }
        catch
        {
        }
    }

    protected void btnAddFriends_Click(object s, EventArgs e)
    {
        try
        {
            List<string> ids = new List<string>();
            foreach (ListItem item in this.chkUserFriendList.Items)
            {
                if (item.Selected)
                {
                    ids.Add(item.Value);
                }
            }
            if (ids.Count <= 0)
            {
                this.PopulateFiles();
                this.PopulateFolders();
                this.PopulateGroupUsers();
                this.PopulateGroups();
                this.PopulateUsersList();
            }
            else
            {
                string staffMembersIdXML = StaffMemberManager.GetStaffMembersIdXML(ids);
                FriendsDetailsDAO sdao = new FriendsDetailsDAO();
                FriendsDetails details = sdao.GetFriendsByUserId(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
                if (details == null)
                {
                    sdao.AddFriends(this.Session["UserName"].ToString(), DateTime.Now, staffMembersIdXML, this.cnxnString, this.logPath);
                }
                else
                {
                    sdao.UpdateUsersByUserId(details.Id, staffMembersIdXML, this.cnxnString, this.logPath);
                }
                this.PopulateFiles();
                this.PopulateFolders();
                this.PopulateGroupUsers();
                this.PopulateGroups();
                this.PopulateUsersList();
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnAllFilesCancel_Click(object sender, EventArgs e)
    {
        this.UploadAllFilesPopup.Style["display"] = "none";
        this.ClearFields();
        this.PopulateFolders();
        this.PopulateFiles();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.UploadAllFilesPopup.Style["display"] = "none";
        this.ClearFields();
        this.PopulateFolders();
        this.PopulateFiles();
    }

    protected void btnCancelShare_Click(object sender, EventArgs e)
    {
        this.ClearFields();
        this.PopulateFolders();
        this.PopulateFiles();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            if (int.TryParse(this.hfFolderId.Value, out result))
            {
                this.Session["CopyFolderID"] = result.ToString();
                this.divPasteFolder.Style["display"] = "block";
            }
            this.PopulateFiles();
            this.PopulateFolders();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnCopyFile_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            if (int.TryParse(this.hfFileId.Value, out result))
            {
                this.Session["CopyFileID"] = result.ToString();
                this.divPasteFile.Style["display"] = "block";
            }
            this.PopulateFiles();
            this.PopulateFolders();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string history = string.Empty;
        FolderHistoryManager manager = new FolderHistoryManager();
        manager.Date = DateTime.Now;
        manager.User = this.Session["UserName"].ToString();
        FolderDetailsDAO sdao = new FolderDetailsDAO();
        UserDetails details = new UserDetailsDAO().GetUserDetailList(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
        try
        {
            if ((this.btnAdd.Text.ToLower() == "save") || (this.btnAdd.Text.ToLower() == "add"))
            {
                if (this.ValidateFolderFields())
                {
                    manager.Activity = "Folder Created";
                    history = FolderHistoryManager.CreateHistory(manager);
                    if (this.isUseNetworkPath)
                    {
                        Directory.CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], Path.Combine(this.GetPathHirarchy(), this.txtFolderName.Text)));
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.Combine(base.Server.MapPath("~/Directories/" + this.college), Path.Combine(this.GetPathHirarchy(), this.txtFolderName.Text)));
                    }
                    sdao.CreateFolderDetails(this.txtFolderName.Text, this.txtDescription.Text, this.txtFolderName.Text, DateTime.Now, DateTime.Now, details.Id, this.GetUserList(), this.parentId, history, this.txtFolderTags.Text, this.txtFolderName.Text, "", "Others", "", this.cnxnString, this.logPath);
                    int maxFolderId = sdao.GetMaxFolderId(this.cnxnString, this.logPath);
                    new UserActivityTrackerDAO().CreateUserActivityDetails("Folder created", this.txtFolderName.Text, maxFolderId, DateTime.Now, details.UserName, this.cnxnString, this.logPath);
                    FolderHistoryManager his = new FolderHistoryManager();
                    his.Activity = "Add Child folder (" + this.txtFolderName.Text + ")";
                    his.Date = DateTime.Now;
                    his.User = this.Session["UserName"].ToString();
                    this.UpdateLastModifyDate(his);
                    this.divresult.InnerHtml = "Successfully created folder " + this.txtFolderName.Text;
                    this.divresult.Style.Add("color", "green");
                    this.PopulateFolders();
                    this.PopulateFiles();
                    this.ClearFields();
                }
                else
                {
                    this.PopulateFolders();
                    this.PopulateFiles();
                    this.divresult.Style.Add("color", "red");
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                }
            }
            else if (this.btnAdd.Text.ToLower() == "update")
            {
                int num2;
                if (string.IsNullOrEmpty(this.txtFolderName.Text))
                {
                    this.divresult.Style.Add("color", "red");
                    this.divresult.InnerHtml = "Please enter Folder name";
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                    this.PopulateFolders();
                    this.PopulateFiles();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtDescription.Text))
                {
                    this.divresult.Style.Add("color", "red");
                    this.divresult.InnerHtml = "Please enter folder description";
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                    this.PopulateFolders();
                    this.PopulateFiles();
                    return;
                }
                if (int.TryParse(this.hfFolderId.Value, out num2))
                {
                    manager.Activity = "Folder Updated";
                    FolderDetails folderDetails = sdao.GetSingleFileDetails(num2, this.cnxnString, this.logPath);
                    if (folderDetails != null)
                    {
                        folderDetails.Alias = this.txtFolderName.Text;
                        folderDetails.FolderDescription = this.txtDescription.Text;
                        folderDetails.Permission = "Delete";
                        folderDetails.UserAccess = this.GetUserList();
                        folderDetails.Tags = this.txtFolderTags.Text;
                        folderDetails.DateUpdated = DateTime.Now;
                        folderDetails.History = FolderHistoryManager.AppendHistory(folderDetails.History, manager);
                        sdao.UpdateFileDetails(folderDetails, this.cnxnString, this.logPath);
                    }
                    else
                    {
                        this.PopulateFolders();
                        this.PopulateFiles();
                    }
                }
                else
                {
                    this.PopulateFolders();
                    this.PopulateFiles();
                }
            }
            this.btnAdd.Text = "Add";
            this.PopulateFolders();
            this.PopulateFiles();
            this.ClearFields();
        }
        catch (Exception)
        {
            this.PopulateFolders();
            this.PopulateFiles();
            this.divresult.Style.Add("color", "red");
        }
        this.divresult.Visible = true;
    }

    protected void btnCut_Click(object sender, EventArgs e)
    {
        try
        {
            int num;
            if (int.TryParse(this.hfFolderId.Value, out num))
            {
                this.Session["CutFolderID"] = num.ToString();
                this.divPasteFolder.Style["display"] = "block";
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnCutFile_Click(object sender, EventArgs e)
    {
        try
        {
            int num;
            if (int.TryParse(this.hfFileId.Value, out num))
            {
                this.Session["CutFileID"] = num.ToString();
                this.divPasteFile.Style["display"] = "block";
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        try
        {
            int num;
            if (int.TryParse(this.hfFolderId.Value, out num))
            {
                string str;
                this.parentId = num;
                new FolderDetailsDAO();
                if (this.isUseNetworkPath)
                {
                    str = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], this.GetPathHirarchy());
                }
                else
                {
                    str = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), this.GetPathHirarchy());
                }
                UtilityManager.DeleteDirectory(str);
                this.DeleteDirectory(num);
            }
            this.parentId = this.parentId = Convert.ToInt32(base.Request.QueryString["id"]);
            this.PopulateFolders();
            this.PopulateFiles();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnDeleteFiles_Click(object s, EventArgs e)
    {
        try
        {
            int num;
            if (int.TryParse(this.hfFileId.Value, out num))
            {
                string str;
                FileDetailsDAO sdao = new FileDetailsDAO();
                FileDetails details = sdao.GetSingleFileDetails(num, this.cnxnString, this.logPath);
                if (details == null)
                {
                    return;
                }
                if (this.isUseNetworkPath)
                {
                    str = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], this.GetPathHirarchy());
                }
                else
                {
                    str = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), this.GetPathHirarchy());
                }
                File.Delete(Path.Combine(str, details.FileName));
                sdao.RemoveFileDetails(num, this.cnxnString, this.logPath);
            }
            this.parentId = this.parentId = Convert.ToInt32(base.Request.QueryString["id"]);
            this.PopulateFiles();
            this.PopulateFolders();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnEditFolder_Click(object s, EventArgs e)
    {
        try
        {
            int num;
            this.divresult.InnerHtml = string.Empty;
            if (int.TryParse(this.hfFolderId.Value, out num))
            {
                FolderDetails details = new FolderDetailsDAO().GetSingleFileDetails(num, this.cnxnString, this.logPath);
                if (details != null)
                {
                    this.txtFolderName.Text = details.Alias;
                    this.txtDescription.Text = details.FolderDescription;
                    this.txtFolderTags.Text = details.Tags;
                    this.btnAdd.Text = "Update";
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                }
            }
            this.PopulateFiles();
            this.PopulateFolders();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnFileUploadAll_Click(object sender, EventArgs e)
    {
        if (this.ValidateFileFeilds())
        {
            try
            {
                string str;
                this.divFileResult.InnerHtml = string.Empty;
                this.divAllFilesResult.InnerHtml = string.Empty;
                if (this.isUseNetworkPath)
                {
                    str = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], this.GetPathHirarchy());
                }
                else
                {
                    str = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), this.GetPathHirarchy());
                }
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
                try
                {
                    HttpFileCollection files = base.Request.Files;
                    List<string> list = new List<string>();
                    Hashtable hashtable = new Hashtable();
                    Hashtable hashtable2 = new Hashtable();
                    int count = 1;
                    this.selectedFilesCount = files.Count;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        Path.GetFileName(file.FileName);
                        string item = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.ToString("HHmmss") + Path.GetExtension(file.FileName);
                        string str3 = string.Concat(new object[] { Path.GetFileNameWithoutExtension(file.FileName), "-temp-", DateTime.Now.Ticks, Path.GetExtension(file.FileName) });
                        switch (Path.GetExtension(file.FileName))
                        {
                            case ".doc":
                            case ".docx":
                            case ".pdf":
                            case ".xls":
                            case ".xlsx":
                            case ".ppt":
                            case ".pptx":
                            case ".jpeg":
                            case ".jpg":
                            case ".png":
                            case ".bmp":
                            case ".tiff":
                            case ".txt":
                            case ".pst":
                            case ".zip":
                            case ".rar":
                            case ".7z":
                            case ".sql":
                                if (file.ContentLength > 0)
                                {
                                    string filename = str + "/" + str3;
                                    file.SaveAs(filename);
                                    list.Add(item);
                                    hashtable.Add(item, file.FileName);
                                    hashtable2.Add(item, filename);
                                }
                                break;

                            default:
                                {
                                    string script = "<script>alert('Please upload a valid file with following extensions \"DOC, DOCX, PDF, XLS, XLSX, PPT, PPTX, JPEG, JPG, PNG, BMP, TIFF, TXT, PST, ZIP, RAR, 7Z,  and SQL\"');</script>";
                                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", script, false);
                                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "alert", "<script>$('#FilePopup').fadeIn(1000);</script>", false);
                                    this.fuFile.Focus();
                                    this.PopulateFolders();
                                    this.PopulateFiles();
                                    return;
                                }
                        }
                        this.GenerateUploadedFilesList(file.FileName, count, this.txtFileDesc.Text, this.txtFileTags.Text, this.ddlFileType.SelectedValue, this.ddlFileAccessType.SelectedItem.Text);
                        count++;
                    }
                    this.ViewState["ListAllUploadedFiles"] = list;
                    this.ViewState["HashAllFileName"] = hashtable;
                    this.ViewState["TempFiles"] = hashtable2;
                    this.btnAddAllFiles.Visible = true;
                    this.UploadAllFilesPopup.Style["display"] = "block";
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
                this.divFileResult.Style.Add("color", "red");
            }
        }
        else
        {
            this.PopulateFolders();
            this.PopulateFiles();
            this.divFileResult.Style.Add("color", "red");
        }
    }

    protected void btnPaste_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            if (int.TryParse(this.hfFolderId.Value, out result))
            {
                this.Session["PasteFolderID"] = result.ToString();
            }
            if (this.Session["CutFolderID"] != null)
            {
                int folderId = int.Parse(this.Session["CutFolderID"].ToString());
                new FolderDetailsDAO().GetSingleFileDetails(folderId, this.cnxnString, this.logPath);
                this.parentId = Convert.ToInt32(base.Request.QueryString["id"]);
                string str = "parentId=" + this.parentId + "&folerId=" + folderId;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFolderParent"] + str);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (new StreamReader(response.GetResponseStream()))
                    {
                    }
                }
                this.Session["CutFolderID"] = null;
            }
            if (this.Session["CopyFolderID"] != null)
            {
                int num3 = int.Parse(this.Session["CopyFolderID"].ToString());
                FolderDetailsDAO sdao2 = new FolderDetailsDAO();
                FolderDetails details = sdao2.GetSingleFileDetails(num3, this.cnxnString, this.logPath);
                Convert.ToInt32(base.Request.QueryString["id"]);
                FolderHistoryManager manager = new FolderHistoryManager
                {
                    Date = DateTime.Now,
                    Activity = "File Added",
                    User = this.Session["UserName"].ToString()
                };
                string history = FolderHistoryManager.CreateHistory(manager);
                sdao2.CreateFolderDetails(details.FolderName, details.FolderDescription, details.RelativePath, DateTime.Now, DateTime.Now, details.OwnerUserId, details.UserAccess, int.Parse(this.Session["PasteFolderID"].ToString()), history, details.Tags, details.Alias, details.Permission, details.FolderType, details.Email, this.cnxnString, this.logPath);
                this.Session["CopyFolderID"] = null;
            }
            if (this.Session["CutFileID"] != null)
            {
                int fileID = int.Parse(this.Session["CutFileID"].ToString());
                new FileDetailsDAO().GetSingleFileDetails(fileID, this.cnxnString, this.logPath);
                if (base.Request.QueryString["ParentId"] != null)
                {
                    int.Parse(base.Request.QueryString["ParentId"]);
                }
                string str3 = "parentId=" + int.Parse(this.Session["PasteFolderID"].ToString()) + "&folerId=" + fileID;
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFileParent"] + str3);
                using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                {
                    using (new StreamReader(response2.GetResponseStream()))
                    {
                    }
                }
                this.Session["CutFileID"] = null;
            }
            if (this.Session["CopyFileID"] != null)
            {
                int num5 = int.Parse(this.Session["CopyFileID"].ToString());
                FileDetailsDAO sdao4 = new FileDetailsDAO();
                FileDetails details2 = sdao4.GetSingleFileDetails(num5, this.cnxnString, this.logPath);
                if (base.Request.QueryString["ParentId"] != null)
                {
                    int.Parse(base.Request.QueryString["ParentId"]);
                }
                FolderHistoryManager manager2 = new FolderHistoryManager
                {
                    Date = DateTime.Now,
                    Activity = "File Added",
                    User = this.Session["UserName"].ToString()
                };
                string str4 = FolderHistoryManager.CreateHistory(manager2);
                sdao4.CreateFileDetails(details2.FileName, details2.OriginalFileName, details2.FileDescription, details2.RelativePath, DateTime.Now, DateTime.Now, details2.OwnerUserId, details2.UserAccess, int.Parse(this.Session["PasteFolderID"].ToString()), str4, details2.Tags, details2.Permission, details2.DocumentType, this.cnxnString, this.logPath);
                this.Session["CopyFileID"] = null;
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnPasteFile_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Session["CutFileID"] != null)
            {
                int parentId = 0;
                int fileID = int.Parse(this.Session["CutFileID"].ToString());
                FileDetailsDAO sdao = new FileDetailsDAO();
                FileDetails details = sdao.GetSingleFileDetails(fileID, this.cnxnString, this.logPath);
                parentId = Convert.ToInt32(base.Request.QueryString["id"]);
                sdao.UpdateParentFolderId(parentId, fileID, details.History.UnescapeXML(), this.cnxnString, this.logPath);
                this.Session["CutFileID"] = null;
                this.divPasteFile.Style["display"] = "none";
            }
            if (this.Session["CopyFileID"] != null)
            {
                int parentFolderId = 0;
                int num4 = int.Parse(this.Session["CopyFileID"].ToString());
                FileDetailsDAO sdao2 = new FileDetailsDAO();
                FileDetails details2 = sdao2.GetSingleFileDetails(num4, this.cnxnString, this.logPath);
                parentFolderId = Convert.ToInt32(base.Request.QueryString["id"]);
                FolderHistoryManager manager = new FolderHistoryManager
                {
                    Date = DateTime.Now,
                    Activity = "File Added",
                    User = this.Session["UserName"].ToString()
                };
                string history = FolderHistoryManager.CreateHistory(manager);
                sdao2.CreateFileDetails(details2.FileName, details2.OriginalFileName, details2.FileDescription, details2.RelativePath, DateTime.Now, DateTime.Now, details2.OwnerUserId, details2.UserAccess, parentFolderId, history, details2.Tags, details2.Permission, details2.DocumentType, this.cnxnString, this.logPath);
                this.Session["CopyFolderID"] = null;
                this.divPasteFile.Style["display"] = "none";
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnPasteFolder_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Session["CutFolderID"] != null)
            {
                int num = 0;
                int folderId = int.Parse(this.Session["CutFolderID"].ToString());
                new FolderDetailsDAO().GetSingleFileDetails(folderId, this.cnxnString, this.logPath);
                num = Convert.ToInt32(base.Request.QueryString["id"]);
                string str = "parentId=" + num + "&folerId=" + folderId;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFolderParent"] + str);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (new StreamReader(response.GetResponseStream()))
                    {
                    }
                }
                this.Session["CutFolderID"] = null;
                this.divPasteFolder.Style["display"] = "none";
            }
            if (this.Session["CopyFolderID"] != null)
            {
                int parentFolderId = 0;
                int num4 = int.Parse(this.Session["CopyFolderID"].ToString());
                FolderDetailsDAO sdao2 = new FolderDetailsDAO();
                FolderDetails details = sdao2.GetSingleFileDetails(num4, this.cnxnString, this.logPath);
                parentFolderId = Convert.ToInt32(base.Request.QueryString["id"]);
                FolderHistoryManager manager = new FolderHistoryManager
                {
                    Date = DateTime.Now,
                    Activity = "File Added",
                    User = this.Session["UserName"].ToString()
                };
                string history = FolderHistoryManager.CreateHistory(manager);
                sdao2.CreateFolderDetails(details.FolderName, details.FolderDescription, details.RelativePath, DateTime.Now, DateTime.Now, details.OwnerUserId, details.UserAccess, parentFolderId, history, details.Tags, details.Alias, details.Permission, details.FolderType, details.Email, this.cnxnString, this.logPath);
                this.Session["CopyFolderID"] = null;
                this.divPasteFolder.Style["display"] = "none";
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnReload_Click(object s, EventArgs e)
    {
        try
        {
            this.divSearchText.InnerHtml = string.Empty;
            this.divSearchText.Style["display"] = "none";
            this.txtSearch.Value = string.Empty;
            this.PopulateFiles();
            this.PopulateFolders();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnRename_Click(object s, EventArgs e)
    {
    }

    protected void btnRenameFile_Click(object s, EventArgs e)
    {
        try
        {
            int num;
            if (!string.IsNullOrEmpty(this.txtFileRename.Text) && int.TryParse(this.hfFileId.Value, out num))
            {
                FileDetailsDAO sdao = new FileDetailsDAO();
                FileDetails details = sdao.GetSingleFileDetails(num, this.cnxnString, this.logPath);
                sdao.UpdateFileName(num, this.txtFileRename.Text.Trim(), this.Session["UserName"].ToString(), details.History.UnescapeXML(), this.cnxnString, this.logPath);
                UserDetails details2 = new UserDetailsDAO().GetUserDetailList(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
                new UserActivityTrackerDAO().CreateUserActivityDetails("File Renamed", this.txtFileRename.Text, num, DateTime.Now, details2.UserName, this.cnxnString, this.logPath);
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnRenameFolder_Click(object s, EventArgs e)
    {
        try
        {
            int num;
            if (!string.IsNullOrEmpty(this.txtFolderRename.Text) && int.TryParse(this.hfFolderId.Value, out num))
            {
                FolderDetailsDAO sdao = new FolderDetailsDAO();
                FolderDetails details = sdao.GetSingleFileDetails(num, this.cnxnString, this.logPath);
                sdao.UpdateFolderAlias(num, this.txtFolderRename.Text.Trim(), this.Session["UserName"].ToString(), details.History.UnescapeXML(), this.cnxnString, this.logPath);
                UserDetails details2 = new UserDetailsDAO().GetUserDetailList(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
                new UserActivityTrackerDAO().CreateUserActivityDetails("Folder Renamed", this.txtFolderRename.Text, num, DateTime.Now, details2.UserName, this.cnxnString, this.logPath);
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnShareFile_Click(object sender, EventArgs e)
    {
        try
        {
            int num;
            if (int.TryParse(this.hfFileId.Value, out num))
            {
                FileDetails details = new FileDetailsDAO().GetSingleFileDetails(num, this.cnxnString, this.logPath);
                string[] strArray = this.txtShareFileWith.Text.Split(new char[] { ',' });
                string str = this.Session["UserName"].ToString();
                string str2 = string.Empty;
                if (this.isUseNetworkPath)
                {
                    str2 = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], details.RelativePath);
                }
                else
                {
                    str2 = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), details.RelativePath);
                }
                string str3 = "<a style='vertical-align:top;' href='" + this.BASE_URL + "/api/FileDownload.ashx?fileName=" + details.OriginalFileName + "&filePath=" + str2 + "'> <img src='" + this.BASE_URL + "/static/images/file.png' width='12px' height='12px' style='padding: 5px; float: left' /> Click Here</a>";
                string message = str + " have shared this file with you. Please " + str3 + " here to download the file.";
                foreach (string str5 in strArray)
                {
                    EmailManager.FileSharing_EmailToUser(str5, message, this.logPath);
                }
                this.txtShareFileWith.Text = string.Empty;
                this.txtShareMessFile.Text = string.Empty;
            }
            this.PopulateFolders();
            this.PopulateFiles();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnShareFolder_Click(object sender, EventArgs e)
    {
        try
        {
            int num;
            if (int.TryParse(this.hfFolderId.Value, out num))
            {
                new FolderDetailsDAO().GetSingleFileDetails(num, this.cnxnString, this.logPath);
                string[] strArray = this.txtShareWith.Text.Split(new char[] { ',' });
                string str = this.Session["UserName"].ToString();
                string str2 = "Google.com";
                string message = str + " have shared this file with you. Please click<a href='" + str2 + "'> here </a>to download the file.";
                foreach (string str4 in strArray)
                {
                    EmailManager.FileSharing_EmailToUser(str4, message, this.logPath);
                }
                this.txtMessage.Text = string.Empty;
                this.txtShareWith.Text = string.Empty;
            }
        }
        catch (Exception)
        {
            throw;
        }
        this.PopulateFiles();
        this.PopulateFolders();
    }

    protected void btnSortFileDate_Click(object sender, EventArgs e)
    {
        List<FileDetails> list = this.SortingFileList(new FileDetailsDAO().GetAllFiles(this.parentId, this.cnxnString, this.logPath), "CreateDate");
        if ((list != null) && (list.Count > 0))
        {
            this.AddFileHeader();
            int num = 0;
            foreach (FileDetails details in list)
            {
                this.AddFileRecord(details, (num % 2) == 0);
                num++;
            }
        }
        this.PopulateFolders();
    }

    protected void btnSortFileName_Click(object sender, EventArgs e)
    {
        List<FileDetails> list = this.SortingFileList(new FileDetailsDAO().GetAllFiles(this.parentId, this.cnxnString, this.logPath), "FileName");
        if ((list != null) && (list.Count > 0))
        {
            this.AddFileHeader();
            int num = 0;
            foreach (FileDetails details in list)
            {
                this.AddFileRecord(details, (num % 2) == 0);
                num++;
            }
        }
        this.PopulateFolders();
    }

    protected void btnSortFileOwner_Click(object sender, EventArgs e)
    {
        List<FileDetails> list = this.SortingFileList(new FileDetailsDAO().GetAllFiles(this.parentId, this.cnxnString, this.logPath), "Owner");
        if ((list != null) && (list.Count > 0))
        {
            this.AddFileHeader();
            int num = 0;
            foreach (FileDetails details in list)
            {
                this.AddFileRecord(details, (num % 2) == 0);
                num++;
            }
        }
        this.PopulateFolders();
    }

    protected void btnSortFolderDate_Click(object sender, EventArgs e)
    {
        List<FolderDetails> list = this.SortingFolderList(new FolderDetailsDAO().GetAllChildFolders(this.parentId, this.cnxnString, this.logPath), "CreateDate");
        if ((list != null) && (list.Count > 0))
        {
            this.AddFolderHeader();
            int num = 0;
            foreach (FolderDetails details in list)
            {
                this.AddFolderRecord(details, (num % 2) == 0);
                num++;
            }
        }
        this.PopulateFiles();
    }

    protected void btnSortFolderName_Click(object sender, EventArgs e)
    {
        try
        {
            List<FolderDetails> list = this.SortingFolderList(new FolderDetailsDAO().GetAllChildFolders(this.parentId, this.cnxnString, this.logPath), "FolderName");
            if ((list != null) && (list.Count > 0))
            {
                this.AddFolderHeader();
                int num = 0;
                foreach (FolderDetails details in list)
                {
                    this.AddFolderRecord(details, (num % 2) == 0);
                    num++;
                }
            }
            this.PopulateFiles();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnSortFolderOwner_Click(object sender, EventArgs e)
    {
        try
        {
            List<FolderDetails> list = this.SortingFolderList(new FolderDetailsDAO().GetAllChildFolders(this.parentId, this.cnxnString, this.logPath), "Owner");
            if ((list != null) && (list.Count > 0))
            {
                this.AddFolderHeader();
                int num = 0;
                foreach (FolderDetails details in list)
                {
                    this.AddFolderRecord(details, (num % 2) == 0);
                    num++;
                }
            }
            this.PopulateFiles();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CheckFolderAccess()
    {
        if (this.parentId != 0)
        {
            try
            {
                FolderDetails details = new FolderDetailsDAO().GetSingleFileDetails(this.parentId, this.cnxnString, this.logPath);
                if (details != null)
                {
                    UserDetails details2 = new UserDetailsDAO().GetUserDetailbyId(details.OwnerUserId, this.cnxnString, this.logPath);
                    if ((details2 == null) || (details2.UserName.ToLower() != this.Session["UserName"].ToString().ToLower()))
                    {
                        XmlDocument document = new XmlDocument();
                        if (!string.IsNullOrEmpty(details.UserAccess))
                        {
                            document.LoadXml(details.UserAccess.UnescapeXML());
                            XmlNode node = document.SelectSingleNode("Users");
                            if (node.Attributes["accesstype"].Value == "Read")
                            {
                                this.div3.Visible = false;
                                this.divAddPopup.Visible = false;
                                this.divresult.InnerText = "You do not have any rights to add folder.";
                                this.divresult.Style["color"] = "red";
                                this.divresult.Visible = true;
                                this.divFileResult.InnerText = "You do not have any rights to upload file.";
                                this.divFileResult.Style["color"] = "red";
                                this.divFileResult.Visible = true;
                            }
                            else if (node.Attributes["accesstype"].Value == "Write")
                            {
                                this.div3.Visible = true;
                                this.divAddPopup.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    private void ClearFields()
    {
        this.txtDescription.Text = string.Empty;
        this.txtFolderName.Text = string.Empty;
        this.txtFolderTags.Text = string.Empty;
        this.txtFileDesc.Text = string.Empty;
        this.txtFileTags.Text = string.Empty;
    }

    private void DeleteDirectory(int folderId)
    {
        try
        {
            FolderDetailsDAO sdao = new FolderDetailsDAO();
            FileDetailsDAO sdao2 = new FileDetailsDAO();
            List<FileDetails> list = sdao2.GetAllFiles(folderId, this.cnxnString, this.logPath);
            if (list != null)
            {
                foreach (FileDetails details in list)
                {
                    sdao2.RemoveFileDetails(details.Id, this.cnxnString, this.logPath);
                }
            }
            List<FolderDetails> list2 = sdao.GetAllChildFolders(folderId, this.cnxnString, this.logPath);
            if (list2 != null)
            {
                foreach (FolderDetails details2 in list2)
                {
                    this.DeleteDirectory(details2.Id);
                    sdao.RemoveFileDetails(details2.Id, this.cnxnString, this.logPath);
                }
            }
            sdao.RemoveFileDetails(folderId, this.cnxnString, this.logPath);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void GenerateUploadedFilesList(string fileName, int count, string fileDescription, string fileTags, string docType, string accesstype)
    {
        HtmlGenericControl child = new HtmlGenericControl("div")
        {
            ID = "RowContainer" + count,

        };

        child.Style["padding"] = "15px";

        HtmlGenericControl control2 = new HtmlGenericControl("div")
        {
            ID = "Rowdivfilename" + count,
            InnerText = fileName,
        };
        control2.Style["width"] = "18%";
        control2.Style["float"] = "left";
        control2.Style["clear"] = "both";

        HtmlGenericControl control3 = new HtmlGenericControl("div")
        {
            ID = "Rowdivfiletype" + count,
            InnerText = "Document Type"
        };

        control3.Style["width"] = "5%";
        control3.Style["float"] = "left";

        HtmlGenericControl control4 = new HtmlGenericControl("div")
        {
            ID = "RowdivfiletypeValue" + count
        };

        control4.Style["width"] = "13%";
        control4.Style["float"] = "left";

        DropDownList list = new DropDownList
        {
            ID = "RowddTypeValue" + count,
            DataSource = Configurations.GetDocumnettypes(),
            DataTextField = "Documents",
            DataValueField = "Documents"
        };
        list.DataBind();
        if (!string.IsNullOrEmpty(docType))
        {
            list.SelectedValue = docType;
        }
        control4.Controls.Add(list);
        HtmlGenericControl control5 = new HtmlGenericControl("div")
        {
            ID = "RowdivfileDescription" + count,
            InnerText = "File Description"

        };


        HtmlGenericControl control6 = new HtmlGenericControl("div")
        {
            ID = "RowdivfiledescriptionValue" + count

        };

        control5.Style["width"] = "8%";
        control5.Style["float"] = "left";
        control6.Style["width"] = "13%";
        control6.Style["float"] = "left";

        TextBox box = new TextBox
        {
            ID = "RowtxtFileDescriptionValue" + count,
            Text = fileDescription
        };
        control6.Controls.Add(box);
        HtmlGenericControl control7 = new HtmlGenericControl("div")
        {
            ID = "RowdivAccess" + count,
            InnerText = "Access"
        };
        HtmlGenericControl control8 = new HtmlGenericControl("div")
        {
            ID = "RowdivAccessValue" + count

        };

        control7.Style["width"] = "4%";
        control7.Style["float"] = "left";
        control8.Style["width"] = "14%";
        control8.Style["float"] = "left";

        DropDownList list2 = new DropDownList
        {
            ID = "RowddAccessValue" + count,
            Items = { 
                "Read",
                "Share",
                "Rename",
                "Update",
                "Delete",
                "Private"
            }
        };
        if (!string.IsNullOrEmpty(accesstype))
        {
            list2.SelectedValue = accesstype;
        }
        control8.Controls.Add(list2);
        HtmlGenericControl control9 = new HtmlGenericControl("div")
        {
            ID = "Rowdivtags" + count,
            InnerText = "Tags"

        };
        HtmlGenericControl control10 = new HtmlGenericControl("div")
        {
            ID = "RowdivTagsValue" + count

        };
        TextBox box2 = new TextBox
        {
            ID = "RowtxtTageValue" + count,
            Text = fileTags
        };
        control10.Controls.Add(box2);
        HtmlGenericControl control11 = new HtmlGenericControl("div")
        {
            ID = "RowdivTagsExample" + count,
            InnerText = "(Comma separated tags)"

        };

        control9.Style["width"] = "3%";
        control9.Style["float"] = "left";
        control10.Style["width"] = "13%";
        control10.Style["float"] = "left";
        control11.Style["width"] = "9%";
        control11.Style["float"] = "left";

        child.Controls.Add(control2);
        child.Controls.Add(control3);
        child.Controls.Add(control4);
        child.Controls.Add(control5);
        child.Controls.Add(control6);
        this.accessright = Configurations.GetAccessRights();
        if (this.accessright)
        {
            child.Controls.Add(control7);
            child.Controls.Add(control8);
        }
        else
        {
            control2.Style["width"] = "20%";
            control3.Style["width"] = "9%";
            control4.Style["width"] = "14%";
            control5.Style["width"] = "8%";
            control6.Style["width"] = "14%";
            control9.Style["width"] = "4%";
            control10.Style["width"] = "14%";
            control11.Style["width"] = "15%";
        }
        child.Controls.Add(control9);
        child.Controls.Add(control10);
        child.Controls.Add(control11);
        HtmlGenericControl control12 = new HtmlGenericControl("hr");

        control12.Style["width"] = "100%";
        control12.Style["clear"] = "both";
        control12.Style["float"] = "left";

        child.Controls.Add(control12);
        this.FilesRowContainer.Controls.Add(child);
    }

    public string GetBreadcrumb()
    {
        string str2;
        try
        {
            FolderDetails details;
            FolderDetailsDAO sdao = new FolderDetailsDAO();
            string str = "";
            int parentId = this.parentId;
        Label_0013:
            details = sdao.GetSingleFileDetails(parentId, this.cnxnString, this.logPath);
            if (details == null)
            {
                base.Response.Redirect("Dashboard.aspx", false);
                return null;
            }
            object obj2 = str;
            str = string.Concat(new object[] { obj2, details.Alias, ",", details.Id, "/" });
            if (details.ParentFolderId != -1)
            {
                parentId = details.ParentFolderId;
                goto Label_0013;
            }
            string[] strArray = str.Remove(str.LastIndexOf('/')).Split(new char[] { '/' });
            str = string.Empty;
            str = "<a href='../Dashboard.aspx'>Dashboard</a>&nbsp;>&nbsp;<a href='Dashboard.aspx'>Documents</a>&nbsp;>&nbsp;";
            for (int i = strArray.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(strArray[i]))
                {
                    string[] strArray2 = strArray[i].Split(new char[] { ',' });
                    if (i != 0)
                    {
                        string str3 = str;
                        str = str3 + "<a href='InnerFolder.aspx?id=" + strArray2[1] + "'>" + strArray2[0] + " </a>&nbsp;>&nbsp;";
                    }
                    else
                    {
                        str = str + strArray2[0];
                    }
                }
            }
            str2 = str;
        }
        catch (Exception)
        {
            throw;
        }
        return str2;
    }

    private string GetFileUserList()
    {
        if (!this.accessright)
        {
            return SecurityElement.Escape("<Users accesstype='Delete' usertype='No Access'>None</Users>");
        }
        return SecurityElement.Escape("<Users accesstype='" + this.ddAccessType.SelectedItem.Text + "' usertype='No Access'>None</Users>");
    }

    public string GetHistory(string xml)
    {
        List<FolderHistoryManager> historyList = FolderHistoryManager.GetHistoryList(xml.UnescapeXML());
        string str = "";
        if (historyList == null)
        {
            return "<li> sorry no history has created.</li>";
        }
        foreach (FolderHistoryManager manager in historyList)
        {
            string str2 = str;
            str = str2 + "<li>" + manager.Activity + " by " + manager.User + " on " + manager.Date.ToString("dd/MM/yyyy") + ".</li>";
        }
        return str;
    }

    public string GetPathHirarchy()
    {
        string str2;
        try
        {
            FolderDetailsDAO sdao = new FolderDetailsDAO();
            string str = "/";
            int parentId = this.parentId;
            while (true)
            {
                FolderDetails details = sdao.GetSingleFileDetails(parentId, this.cnxnString, this.logPath);
                str = str + details.FolderName + "/";
                if (details.ParentFolderId == -1)
                {
                    break;
                }
                parentId = details.ParentFolderId;
            }
            string[] strArray = str.Split(new char[] { '/' });
            str = string.Empty;
            str = "";
            for (int i = strArray.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(strArray[i]))
                {
                    str = str + strArray[i] + "/";
                }
            }
            str2 = str;
        }
        catch (Exception)
        {
            throw;
        }
        return str2;
    }

    private string GetUserList()
    {
        if (!this.accessright)
        {
            return SecurityElement.Escape("<Users accesstype='Delete' usertype='No Access'>None</Users>");
        }
        return SecurityElement.Escape("<Users accesstype='" + this.ddAccessType.SelectedItem.Text + "' usertype='No Access'>None</Users>");
    }

    private bool IsAccessPermission(int ownerUserId, string userAccess, string permission, out bool isOwnerUser)
    {
        isOwnerUser = true;
        XmlDocument document = new XmlDocument();
        UserDetails details = new UserDetailsDAO().GetUserDetailbyId(ownerUserId, this.cnxnString, this.logPath);
        isOwnerUser = false;
        if ((details != null) && (details.UserName.ToLower() == this.Session["UserName"].ToString().ToLower()))
        {
            isOwnerUser = true;
            return true;
        }
        else if (details != null && (this.Session["RoleType"].ToString() == "3" || this.Session["RoleType"].ToString() == "4"))
        {
            isOwnerUser = true;
            return true;
        }
        if (!string.IsNullOrEmpty(userAccess) && !string.IsNullOrEmpty(permission))
        {
            document.LoadXml(userAccess.UnescapeXML());
            XmlNode node = document.SelectSingleNode("Users");
            XmlNodeList list = node.SelectNodes("User");
            if (list.Count > 0)
            {
                foreach (XmlNode node2 in list)
                {
                    if (node2.InnerText.ToLower() == this.Session["UserName"].ToString().ToLower())
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (node.InnerText.ToLower() == "all")
                {
                    return true;
                }
                if (node.InnerText.ToLower() == "none")
                {
                    return false;
                }
            }
        }
        return false;
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(this.txtSearch.Value) || (this.txtSearch.Value == "Search..."))
            {
                this.PopulateFiles();
                this.PopulateFolders();
            }
            else
            {
                List<FolderDetails> list = new FolderDetailsDAO().GetFolderDetailsForSearch(this.txtSearch.Value.ToLower(), this.cnxnString, this.logPath);
                if ((this.Session["RoleType"].ToString() == "1") && (list != null))
                {
                    int userId = Convert.ToInt32(this.Session["UserId"]);
                    list = list.FindAll(f => f.OwnerUserId == userId || f.ParentFolderId == this.parentId);
                }
                if ((list != null) && (list.Count > 0))
                {
                    this.AddFolderHeader();
                    int num = 0;
                    foreach (FolderDetails details in list)
                    {
                        this.AddFolderRecord(details, (num % 2) == 0);
                        num++;
                    }
                    this.divSearchText.InnerHtml = "<b>Search Results - " + this.txtSearch.Value.ToUpper() + "</b>. &nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:void();' title='remove search result' onclick=\"ClickReload('" + this.btnReload.ClientID + "');\"> <b>X</b></a>";
                    this.divSearchText.Style["display"] = "block";
                }
                else
                {
                    this.Folders.InnerHtml = string.Concat(new object[] { "Currently you don't have any folders for '", this.txtSearch.Value, "'. <a href='InnerFolder.aspx?id=", this.parentId, "'>Click here</a> to return back to previous screen." });
                }
                List<FileDetails> list2 = new FileDetailsDAO().GetFileDetailsForSearch(this.txtSearch.Value, this.cnxnString, this.logPath);
                if ((this.Session["RoleType"].ToString() == "1") && (list2 != null))
                {
                    int userId = Convert.ToInt32(this.Session["UserId"]);
                    list2 = list2.FindAll(f => f.OwnerUserId == userId);
                }
                if ((list2 != null) && (list2.Count > 0))
                {
                    this.AddFileHeader();
                    int num2 = 0;
                    foreach (FileDetails details2 in list2)
                    {
                        this.AddFileRecord(details2, (num2 % 2) == 0);
                        num2++;
                    }
                    this.divSearchText.InnerHtml = "<b>Search Results - " + this.txtSearch.Value.ToUpper() + "</b>. &nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:void();' title='remove search result' onclick=\"ClickReload('" + this.btnReload.ClientID + "');\"> <b>X</b></a>";
                    this.divSearchText.Style["display"] = "block";
                }
                else
                {
                    this.Files.InnerHtml = string.Concat(new object[] { "There are no file found for '", this.txtSearch.Value, "'. <a href='InnerFolder.aspx?id=", this.parentId, "'>Click here</a> to return back to previous screen." });
                }
            }
            this.txtSearch.Value = string.Empty;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserName"] == null)
        {
            base.Response.Redirect("/Login.apsx", false);
        }
        else
        {
            this.logPath = this.Session["LogFilePath"].ToString();
            this.cnxnString = this.Session["ConnectionString"].ToString();
            this.userName = this.Session["UserName"].ToString();
            this.college = this.Session["College"].ToString();
            if ((base.Request.QueryString.Count > 0) && (base.Request.QueryString["isStaff"] != null))
            {
                this.isStaff = base.Request.QueryString["isStaff"].ToString();
            }
            else if (this.Session["UserName"] == null)
            {
                base.Response.Redirect("../Login.apsx", false);
                return;
            }
            if (ConfigurationManager.AppSettings["IsUseNetworkPath"] != null)
            {
                this.isUseNetworkPath = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseNetworkPath"]);
            }
            if (base.Request.QueryString.Count > 0)
            {
                this.parentId = Convert.ToInt32(base.Request.QueryString["id"]);
            }
            else
            {
                base.Response.Redirect("Dashboard.aspx", false);
                return;
            }
            if (this.isStaff.Equals("true") && !string.IsNullOrEmpty(this.parentId.ToString()))
            {
                FolderDetailsDAO sdao = new FolderDetailsDAO();
                new UserDetailsDAO();
                sdao.GetSingleFileDetails(this.parentId, this.cnxnString, this.logPath);
            }
            this.Breadcrumbs.InnerHtml = this.GetBreadcrumb();
            if (!this.Page.IsPostBack)
            {
                this.CheckFolderAccess();
                this.PopulateFolders();
                this.PopulateFiles();
                this.PopulateUsersList();
                this.PopulateDocumnetType();
                this.PopulateGroupUsers();
                this.PopulateGroups();
                this.accessright = Configurations.GetAccessRights();
                if (!this.accessright)
                {
                    this.trAccessRightFolder.Style["display"] = "none";
                    this.trAccessRightFile.Style["display"] = "none";
                }
                this.txtSearch.Attributes.Add("onKeyPress", "doClick('" + this.lnkSearch.ClientID + "',event)");
                this.divClick.Attributes.Add("onclick", "divClick('" + this.lnkSearch.ClientID + "');");
            }
            if ((this.Session["CutFileID"] != null) || (this.Session["CopyFileID"] != null))
            {
                this.divPasteFile.Style["display"] = "block";
            }
            if ((this.Session["CutFolderID"] != null) || (this.Session["CopyFolderID"] != null))
            {
                this.divPasteFolder.Style["display"] = "block";
            }
        }
    }

    public void PopulateDocumnetType()
    {
        try
        {
            this.ddlFileType.DataSource = Configurations.GetDocumnettypes();
            this.ddlFileType.DataTextField = "Documents";
            this.ddlFileType.DataValueField = "Documents";
            this.ddlFileType.DataBind();
            this.ddlFileType.SelectedValue = "Others";
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void PopulateFiles()
    {
        FileDetailsDAO sdao = new FileDetailsDAO();
        try
        {
            List<FileDetails> list = sdao.GetAllFiles(this.parentId, this.cnxnString, this.logPath);
            if ((list != null) && (list.Count > 0))
            {
                string configFilePath = Server.MapPath(Session["ReleaseFilePath"].ToString());
                int paySlipCount = new XmlConfiguration().GetPayslipCount(logPath, configFilePath);
                this.AddFileHeader();
                int num = 0;
                string folderName = Request.QueryString["name"];

                folderName = string.IsNullOrEmpty(folderName) ? "" : folderName;

                foreach (FileDetails details in list)
                {

                    if (details.FileName.ToLower().Trim().Contains("pay"))
                    {
                        if (num <= paySlipCount)
                        {
                            this.AddFileRecord(details, (num % 2) == 0);
                            num++;
                        }
                        else
                            break;
                    }
                    else
                    {
                        this.AddFileRecord(details, (num % 2) == 0);
                        num++;
                    }
                }
            }
        }
        catch (Exception exception)
        {
            this.logger.Error("DocumentManagement_Dashboard", "PopulateFiles", " Error occurred while Getting the file Details", exception, this.logPath);
            throw new Exception("13334");
        }
    }

    private void PopulateFolders()
    {
        FolderDetailsDAO sdao = new FolderDetailsDAO();
        try
        {
            List<FolderDetails> list = sdao.GetAllChildFolders(this.parentId, this.cnxnString, this.logPath);
            if ((list != null) && (list.Count > 0))
            {
                this.AddFolderHeader();
                int num = 0;
                foreach (FolderDetails details in list)
                {
                    this.AddFolderRecord(details, (num % 2) == 0);
                    num++;
                }
            }
        }
        catch (Exception exception)
        {
            this.logger.Error("DocumentManagement_Dashboard", "PopulateFolders", " Error occurred while Geting the folder Details", exception, this.logPath);
            throw new Exception("13333");
        }
    }

    private void PopulateFriends()
    {
        try
        {
            FriendsDetails details = new FriendsDetailsDAO().GetFriendsByUserId(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
            this.chkUserFriendList.DataSource = new UserDetailsDAO().GetUserList(this.cnxnString, this.logPath);
            this.chkUserFriendList.DataTextField = "UserId";
            this.chkUserFriendList.DataValueField = "UserId";
            this.chkUserFriendList.DataBind();
            if ((details != null) && !string.IsNullOrEmpty(details.Users))
            {
                List<string> staffIdList = StaffMemberManager.GetStaffIdList(details.Users);
                foreach (ListItem item in this.chkUserFriendList.Items)
                {
                    foreach (string str in staffIdList)
                    {
                        if (str == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void PopulateGroups()
    {
        try
        {
            GroupsDetailsDAO sdao = new GroupsDetailsDAO();
            this.lbGroups.DataSource = sdao.GetGroupsByUser(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
            this.lbGroups.DataTextField = "GroupName";
            this.lbGroups.DataValueField = "Id";
            this.lbGroups.DataBind();
            this.lbFileGroups.DataSource = sdao.GetGroupsByUser(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
            this.lbFileGroups.DataTextField = "GroupName";
            this.lbFileGroups.DataValueField = "Id";
            this.lbFileGroups.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void PopulateGroupUsers()
    {
        try
        {
            this.chkGroupUserList.DataSource = new UserDetailsDAO().GetUserList(this.cnxnString, this.logPath);
            this.chkGroupUserList.DataTextField = "UserName";
            this.chkGroupUserList.DataValueField = "Id";
            this.chkGroupUserList.DataBind();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public void PopulateUsersList()
    {
        try
        {
            FriendsDetails details = new FriendsDetailsDAO().GetFriendsByUserId(this.Session["UserName"].ToString(), this.cnxnString, this.logPath);
            List<FriendUsers> list = new List<FriendUsers>();
            if ((details != null) && !string.IsNullOrEmpty(details.Users))
            {
                foreach (string str in StaffMemberManager.GetStaffIdList(details.Users))
                {
                    FriendUsers item = new FriendUsers
                    {
                        UserId = str
                    };
                    list.Add(item);
                }
            }
            this.lbFileUsers.DataSource = list;
            this.lbFileUsers.DataTextField = "UserId";
            this.lbFileUsers.DataValueField = "UserId";
            this.lbFileUsers.DataBind();
            this.lbFolderUser.DataSource = list;
            this.lbFolderUser.DataTextField = "UserId";
            this.lbFolderUser.DataValueField = "UserId";
            this.lbFolderUser.DataBind();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public List<FileDetails> SortingFileList(List<FileDetails> file, string sortOption)
    {
        List<FileDetails> list;
        try
        {
            string str = string.Empty;
            if (SortingDirection.SortDirection == SortDirection.Ascending)
            {
                SortingDirection.SortDirection = SortDirection.Descending;
                str = "DESC";
            }
            else
            {
                SortingDirection.SortDirection = SortDirection.Ascending;
                str = "ASC";
            }
            if (file == null)
            {
                return file;
            }
            string str2 = sortOption;
            if (str2 != null)
            {
                if (str2 != "FileName")
                {
                    if (str2 == "CreateDate")
                    {
                        goto Label_0095;
                    }
                    if (str2 == "Owner")
                    {
                        goto Label_00C2;
                    }
                }
                else if (str == "ASC")
                {
                    File_SortByNameASC comparer = new File_SortByNameASC();
                    file.Sort(comparer);
                }
                else
                {
                    File_SortByNameDES edes = new File_SortByNameDES();
                    file.Sort(edes);
                }
            }
            goto Label_00EF;
        Label_0095:
            if (str == "ASC")
            {
                File_SortByDateByAscendingOrder order = new File_SortByDateByAscendingOrder();
                file.Sort(order);
            }
            else
            {
                File_SortByDateByDescendingOrder order2 = new File_SortByDateByDescendingOrder();
                file.Sort(order2);
            }
            goto Label_00EF;
        Label_00C2:
            if (str == "ASC")
            {
                File_SortByOwnerASC rasc = new File_SortByOwnerASC();
                file.Sort(rasc);
            }
            else
            {
                File_SortByOwnerDES rdes = new File_SortByOwnerDES();
                file.Sort(rdes);
            }
        Label_00EF:
            list = file;
        }
        catch (Exception)
        {
            throw;
        }
        return list;
    }

    public List<FolderDetails> SortingFolderList(List<FolderDetails> folders, string sortOption)
    {
        List<FolderDetails> list;
        try
        {
            string str = string.Empty;
            if (SortingDirection.SortDirection == SortDirection.Ascending)
            {
                SortingDirection.SortDirection = SortDirection.Descending;
                str = "DESC";
            }
            else
            {
                SortingDirection.SortDirection = SortDirection.Ascending;
                str = "ASC";
            }
            if (folders == null)
            {
                return folders;
            }
            string str2 = sortOption;
            if (str2 != null)
            {
                if (str2 != "FolderName")
                {
                    if (str2 == "CreateDate")
                    {
                        goto Label_0095;
                    }
                    if (str2 == "Owner")
                    {
                        goto Label_00C2;
                    }
                }
                else if (str == "ASC")
                {
                    Folder_SortByNameASC comparer = new Folder_SortByNameASC();
                    folders.Sort(comparer);
                }
                else
                {
                    Folder_SortByNameDES edes = new Folder_SortByNameDES();
                    folders.Sort(edes);
                }
            }
            goto Label_00EF;
        Label_0095:
            if (str == "ASC")
            {
                Folder_SortByDateByAscendingOrder order = new Folder_SortByDateByAscendingOrder();
                folders.Sort(order);
            }
            else
            {
                Folder_SortByDateByDescendingOrder order2 = new Folder_SortByDateByDescendingOrder();
                folders.Sort(order2);
            }
            goto Label_00EF;
        Label_00C2:
            if (str == "ASC")
            {
                Folder_SortByOwnerASC rasc = new Folder_SortByOwnerASC();
                folders.Sort(rasc);
            }
            else
            {
                Folder_SortByOwnerDES rdes = new Folder_SortByOwnerDES();
                folders.Sort(rdes);
            }
        Label_00EF:
            list = folders;
        }
        catch (Exception)
        {
            throw;
        }
        return list;
    }

    public void UpdateLastModifyDate(FolderHistoryManager his)
    {
        if (this.parentId != 0)
        {
            try
            {
                string history = "";
                FolderDetailsDAO sdao = new FolderDetailsDAO();
                FolderDetails details = sdao.GetSingleFileDetails(this.parentId, this.cnxnString, this.logPath);
                if ((details != null) && !string.IsNullOrEmpty(details.History))
                {
                    history = details.History;
                }
                history = FolderHistoryManager.AppendHistory(history, his);
                sdao.UpdateModifyDate(this.parentId, history, this.cnxnString, this.logPath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public bool ValidateFileFeilds()
    {
        bool flag = true;
        HttpFileCollection files = base.Request.Files;
        for (int i = 0; i < files.Count; i++)
        {
            HttpPostedFile file = files[i];
            if (file.ContentLength < 1)
            {
                this.divFileResult.InnerHtml = "Please Upload file.";
                flag = false;
            }
        }
        if (string.IsNullOrEmpty(this.txtFileDesc.Text))
        {
            this.divresult.InnerHtml = "Please enter File description";
            flag = false;
        }
        return flag;
    }

    private bool ValidateFolderFields()
    {
        bool flag = true;
        string path = Path.Combine(base.Server.MapPath("~/Directories/" + this.college), Path.Combine(this.GetPathHirarchy(), this.txtFolderName.Text));
        if (string.IsNullOrEmpty(this.txtFolderName.Text))
        {
            this.divresult.InnerHtml = "Please enter Folder name";
            return false;
        }
        if (string.IsNullOrEmpty(this.txtDescription.Text))
        {
            this.divresult.InnerHtml = "Please enter Folder description";
            return false;
        }
        if (Directory.Exists(path))
        {
            this.divresult.InnerHtml = "Folder with same name already exists. Please enter different folder name.";
            flag = false;
        }
        return flag;
    }
}
