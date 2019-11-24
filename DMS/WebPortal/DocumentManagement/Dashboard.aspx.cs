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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Util;


public partial class DocumentManagement_Dashboard : Page
{
    public int selectedFilesCount = 0;
    private bool isUseNetworkPath = false;
    private bool accessright = false;

    public string BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];
    string logPath = string.Empty;
    string cnxnString = string.Empty;
    string userName = string.Empty;
    Logger logger = new Logger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("/Login.apsx", false);
            return;
        }

        if (ConfigurationManager.AppSettings["IsUseNetworkPath"] != null)
        {
            isUseNetworkPath = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseNetworkPath"]);
        }

        logPath = Session["LogFilePath"].ToString();
        cnxnString = Session["ConnectionString"].ToString();
        userName = Session["UserName"].ToString();

        //ApplicationTitle.InnerHtml = "Document Management Application";

        if (!Page.IsPostBack)
        {
            PopulateFolders();
            PopulateFiles();

            PopulateUsersList();
            PopulateDocumnetType();
            PopulateGroupUsers();
            PopulateGroups();
            PopulateFriends();
            accessright = Configurations.GetAccessRights();
            if (!accessright)
            {
                trAccessRightFolder.Style["display"] = "none";
                trAccessRightFile.Style["display"] = "none";
            }


            txtSearch.Attributes.Add("onKeyPress", "doClick('" + lnkSearch.ClientID + "',event)");

            divClick.Attributes.Add("onclick", "divClick('" + lnkSearch.ClientID + "');");
        }
        if (Session["CutFileID"] != null || Session["CopyFileID"] != null)
        {
            divPasteFile.Style["display"] = "block";
        }
        if (Session["CutFolderID"] != null || Session["CopyFolderID"] != null)
        {
            divPasteFolder.Style["display"] = "block";
        }
    }
    public void PopulateDocumnetType()
    {
        try
        {
            ddlFileType.DataSource = Configurations.GetDocumnettypes();
            ddlFileType.DataTextField = "Documents";
            ddlFileType.DataValueField = "Documents";
            ddlFileType.DataBind();

            ddlFileType.SelectedValue = "Others";
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public void PopulateUsersList()
    {
        try
        {
            FriendsDetailsDAO friendDAO = new FriendsDetailsDAO();

            FriendsDetails friends = friendDAO.GetFriendsByUserId(Session["UserName"].ToString(), cnxnString, logPath);

            List<FriendUsers> friendUsers = new List<FriendUsers>();

            if (friends != null && !string.IsNullOrEmpty(friends.Users))
            {
                List<string> users = StaffMemberManager.GetStaffIdList(friends.Users);

                foreach (string str in users)
                {
                    FriendUsers fu = new FriendUsers();
                    fu.UserId = str;
                    friendUsers.Add(fu);
                }
            }

            lbFileUsers.DataSource = friendUsers;
            lbFileUsers.DataTextField = "UserId";
            lbFileUsers.DataValueField = "UserId";
            lbFileUsers.DataBind();

            lbFolderUser.DataSource = friendUsers;
            lbFolderUser.DataTextField = "UserId";
            lbFolderUser.DataValueField = "UserId";
            lbFolderUser.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string history = string.Empty;

        FolderHistoryManager fHM = new FolderHistoryManager();
        fHM.Date = DateTime.Now;
        fHM.User = Session["UserName"].ToString();

        FolderDetailsDAO folder = new FolderDetailsDAO();
        UserDetailsDAO userDetails = new UserDetailsDAO();
        UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);

        UserActivityTrackerDAO act = new UserActivityTrackerDAO();

        try
        {
            if (btnAdd.Text.ToLower() == "save" || btnAdd.Text.ToLower() == "add")
            {
                if (ValidateFolderFields())
                {
                    fHM.Activity = "Folder Created";

                    history = FolderHistoryManager.CreateHistory(fHM);

                    if (isUseNetworkPath)
                        Directory.CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], txtFolderName.Text));
                    else
                        //Directory.CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"], txtFolderName.Text));
                        Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath("~/Directories"), txtFolderName.Text));

                    //  folder.CreateFolderDetails(txtFolderName.Text, txtDescription.Text, txtFolderName.Text, DateTime.Now, DateTime.Now, userDetail.Id, GetUserList(), -1, history, txtFolderTags.Text, txtFolderName.Text);
                    folder.CreateFolderDetails(txtFolderName.Text, txtDescription.Text, txtFolderName.Text, DateTime.Now, DateTime.Now, userDetail.Id, GetUserList(), -1, history, txtFolderTags.Text, txtFolderName.Text, "Delete", "Others", "", cnxnString, logPath);

                    int maxCount = folder.GetMaxFolderId(cnxnString, logPath);

                    act.CreateUserActivityDetails("Folder created", txtFolderName.Text, maxCount, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                    divresult.InnerHtml = "Successfully created folder " + txtFolderName.Text;
                    divresult.Style.Add("color", "green");

                    btnAdd.Text = "Add";

                    Response.Redirect("Dashboard.aspx", false);

                }
                else
                {
                    divresult.Style.Add("color", "red");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                    PopulateFolders();
                    PopulateFiles();
                }
            }
            else if (btnAdd.Text.ToLower() == "update")
            {
                if (string.IsNullOrEmpty(txtFolderName.Text))
                {
                    divresult.Style.Add("color", "red");
                    divresult.InnerHtml = "Please enter Folder name";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                    PopulateFolders();
                    PopulateFiles();
                    return;
                }

                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    divresult.Style.Add("color", "red");
                    divresult.InnerHtml = "Please enter folder description";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                    PopulateFolders();
                    PopulateFiles();
                    return;
                }

                int folderId;

                if (int.TryParse(hfFolderId.Value, out folderId))
                {
                    fHM.Activity = "Folder Updated";
                    FolderDetails uFolder = folder.GetSingleFileDetails(folderId, cnxnString, logPath);
                    if (uFolder != null)
                    {
                        uFolder.Alias = txtFolderName.Text;
                        uFolder.FolderDescription = txtDescription.Text;
                        uFolder.Permission = "Delete";
                        uFolder.UserAccess = GetUserList();
                        uFolder.Tags = txtFolderTags.Text;
                        uFolder.DateUpdated = DateTime.Now;
                        uFolder.History = FolderHistoryManager.AppendHistory(uFolder.History, fHM);

                        folder.UpdateFileDetails(uFolder, cnxnString, logPath);
                    }
                    else
                    {
                        PopulateFolders();
                        PopulateFiles();
                    }
                }
                else
                {
                    PopulateFolders();
                    PopulateFiles();
                }
            }

            btnAdd.Text = "Add";
            PopulateFolders();
            PopulateFiles();
            ClearFields();
        }
        catch (Exception ex)
        {
            PopulateFolders();
            PopulateFiles();
            divresult.Style.Add("color", "red");
        }

        divresult.Visible = true;
    }

    private bool ValidateFolderFields()
    {
        bool isValid = true;

        if (string.IsNullOrEmpty(txtFolderName.Text))
        {
            divresult.InnerHtml = "Please enter folder name";
            isValid = false;

        }
        else if (string.IsNullOrEmpty(txtDescription.Text))
        {
            divresult.InnerHtml = "Please enter folder description";
            isValid = false;
        }
        else if (Directory.Exists(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), txtFolderName.Text)))
        {
            divresult.InnerHtml = "Folder with same name already exists. Please enter different folder name";
            isValid = false;
        }

        return isValid;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UploadAllFilesPopup.Style["display"] = "none";
        ClearFields();
        PopulateFolders();
        PopulateFiles();
    }

    private void ClearFields()
    {
        txtDescription.Text = string.Empty;
        txtFolderName.Text = string.Empty;
        txtFolderTags.Text = string.Empty;
        //rblChooseUsers.SelectedValue = "0";

        txtFileDesc.Text = string.Empty;
        txtFileTags.Text = string.Empty;
        //txtFileUser.Text = string.Empty;
        //rblFileChoseUser.SelectedValue = "0";
    }

    private void PopulateFolders()
    {
        FolderDetailsDAO folderDetails = new FolderDetailsDAO();
        try
        {
            List<FolderDetails> folders = folderDetails.GetAllChildFolders(-1, cnxnString, logPath);

            if (folders != null && folders.Count > 0)
            {
                AddFolderHeader();
                int counter = 0;
                foreach (FolderDetails folder in folders)
                {
                    AddFolderRecord(folder, (counter % 2 == 0));
                    counter++;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("DocumentManagement_Dashboard", "PopulateFolders", " Error occurred while Geting the folder Details", ex, logPath);
            throw new Exception("13333");
        }
    }

    private void PopulateFiles()
    {
        FileDetailsDAO fileDetails = new FileDetailsDAO();
        try
        {
            List<FileDetails> files = fileDetails.GetAllFiles(-1, cnxnString, logPath);

            if (files != null && files.Count > 0)
            {
                AddFileHeader();
                int counter = 0;
                foreach (FileDetails file in files)
                {
                    AddFileRecord(file, (counter % 2 == 0));
                    counter++;
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error("DocumentManagement_Dashboard", "PopulateFiles", " Error occurred while Getting the file Details", ex, logPath);
            throw new Exception("13334");
        }
    }

    private void AddFolderHeader()
    {
        HtmlGenericControl header = new HtmlGenericControl("div");
        HtmlGenericControl folderName = new HtmlGenericControl("div");
        HtmlGenericControl history = new HtmlGenericControl("div");
        HtmlGenericControl dateCreated = new HtmlGenericControl("div");
        HtmlGenericControl dateUpdated = new HtmlGenericControl("div");
        HtmlGenericControl folderDescription = new HtmlGenericControl("div");
        HtmlGenericControl ownerUser = new HtmlGenericControl("div");
        HtmlGenericControl tags = new HtmlGenericControl("div");

        dateCreated.Style["width"] = "100px";
        folderDescription.Style["width"] = "163px";

        folderName.InnerHtml = "<u>Folder Name</u>";
        folderName.Attributes.Add("class", "DivSortButton");
        folderName.Attributes.Add("title", "click to sort");
        folderName.Attributes.Add("onClick", "SortButtonClick('" + btnSortFolderName.ClientID + "');");

        folderDescription.InnerHtml = "Folder Description";
        history.InnerHtml = "History";

        ownerUser.InnerHtml = "<u>Owner</u>";
        ownerUser.Attributes.Add("class", "DivSortButton");
        ownerUser.Attributes.Add("onClick", "SortButtonClick('" + btnSortFolderOwner.ClientID + "');");

        dateUpdated.InnerHtml = "Updated";

        dateCreated.InnerHtml = "<u>Created On</u>";
        dateCreated.Attributes.Add("class", "DivSortButton");
        dateCreated.Attributes.Add("onClick", "SortButtonClick('" + btnSortFolderDate.ClientID + "');");

        tags.InnerHtml = "Tags";

        header.Controls.Add(folderName);
        header.Controls.Add(folderDescription);
        header.Controls.Add(dateCreated);
        //header.Controls.Add(dateUpdated);
        header.Controls.Add(ownerUser);
        header.Controls.Add(tags);
        header.Controls.Add(history);
        header.Attributes.Add("class", "FolderHeader");
        header.Attributes.Add("style", "font-weight: bold; border-bottom: 1px solid #CCC; padding-bottom: 5px; ");
        Folders.InnerHtml = string.Empty;
        Folders.Controls.Add(header);
    }

    private void AddFolderRecord(FolderDetails folder, bool isColorBackground = false)
    {
        bool isOwnerUser;
        bool isAccessPermission = IsAccessPermission(folder.OwnerUserId, folder.UserAccess, folder.Permission, out isOwnerUser);

        HtmlGenericControl record = new HtmlGenericControl("div");
        HtmlGenericControl folderName = new HtmlGenericControl("div");
        HtmlGenericControl history = new HtmlGenericControl("div");
        HtmlGenericControl dateCreated = new HtmlGenericControl("div");
        HtmlGenericControl dateUpdated = new HtmlGenericControl("div");
        HtmlGenericControl folderDescription = new HtmlGenericControl("div");
        HtmlGenericControl ownerUser = new HtmlGenericControl("div");
        HtmlGenericControl tags = new HtmlGenericControl("div");


        // added by vasim
        HtmlGenericControl buttons = new HtmlGenericControl("div");
        buttons.Style["width"] = "100px";
        dateCreated.Style["width"] = "100px";
        folderDescription.Style["width"] = "163px";

        HtmlGenericControl editButton = new HtmlGenericControl("div");
        editButton.Style["width"] = "100px";
        editButton.InnerHtml = "<div style='width:90px;cursor: pointer;'> <a href='javascript:void(0)' onclick=\"ClickEdit('" + folder.Id + "','" + btnEditFolder.ClientID + "');\" style='color:black;'>Edit Folder</a></div>";


        string rename = "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div>&nbsp;&nbsp; ";
        string delete = "<div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>";

        buttons.InnerHtml = string.Empty;
        //buttons.InnerHtml = rename + delete;

        folderName.InnerHtml = "<a style='vertical-align: top;' href='InnerFolder.aspx?id=" + folder.Id + "'> <img src='" + BASE_URL + "/static/images/folder.png' width='12px' height='12px' style='padding: 5px; float: left' /> " + folder.Alias + "</a>";


        folderDescription.InnerHtml = (folder.FolderDescription.Length > 55 ? folder.FolderDescription.Remove(55, folder.FolderDescription.Length - 55).Trim() + "..." : folder.FolderDescription);
        folderDescription.Attributes.Add("title", folder.FolderDescription);


        string MoreOptionExport = "<div style='padding-bottom: 5px;cursor: pointer;'> <a href='" + BASE_URL + "/api/DownLoadZip.ashx?folderId=" + folder.Id + "'>Download as Zip</a> </div>";

        HtmlGenericControl MoreOption = new HtmlGenericControl("div");
        MoreOption.Style["width"] = "50px";
        MoreOption.Style["min-width"] = "50px";
        MoreOption.InnerHtml = "<div style='width:50px;min-width: 50px;cursor: pointer;'> <a href='javascript:void(0)'  onclick=$('#div" + folder.Id + "_More" + "').fadeIn(1000); >Options...</a></div> ";

        //string MoreOptionMenu = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 115px; position: absolute;background-color: #FFF; left: 1151px;'> <img src='" + BASE_URL + "/static/image/closeIcon1.png' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        //"<ul><div style='cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div> </ul> </div>";
        string MoreOptionMenuAll = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
               "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='cursor: pointer;color: gray;padding-bottom: 5px;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='cursor: pointer;padding-bottom: 5px;'>Delete</div>" + MoreOptionExport + "<div style='padding-bottom: 5px;cursor: pointer;'> <a href='javascript:void(0)' onclick=\"ClickEdit('" + folder.Id + "','" + btnEditFolder.ClientID + "');\" style='color:black;'>Edit</a></div> </ul> </div>";

        string MoreOptionMenuRename = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='padding-bottom: 5px;cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div>" + MoreOptionExport + " </ul></div>"; //<div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>

        string MoreOptionMenuDelete = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='padding-bottom: 5px;cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='cursor: pointer;padding-bottom: 5px;'>Delete</div> " + MoreOptionExport + " </ul> </div>";

        string MoreOptionMenu = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='padding-bottom: 5px;cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='cursor: pointer;padding-bottom: 5px;'>Delete</div>" + MoreOptionExport + " </ul> </div>";

        string MoreOptionMenuOnlyRename = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div>" + MoreOptionExport + " </ul> </div>";

        string MoreOptionMenuOnlyDelete = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div> " + MoreOptionExport + "</ul> </div>";

        //MoreOption.InnerHtml += MoreOptionMenu;

        if (isOwnerUser)
        {
            MoreOption.InnerHtml += MoreOptionMenuAll;// +MoreOptionExpoert;
            //buttons.InnerHtml = rename + delete + MoreOption.InnerHtml;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (!isOwnerUser && folder.Permission.ToLower() == "rename")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyRename;// +MoreOptionExpoert;
            //buttons.InnerHtml += rename;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "delete")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyDelete;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "share")
        {
            MoreOption.InnerHtml += MoreOptionMenu;// +MoreOptionExpoert;
            buttons.InnerHtml += MoreOption.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "update")
        {
            MoreOption.InnerHtml += MoreOptionMenuRename;// +MoreOptionExpoert;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }

        history.InnerHtml = "<a href='javascript:void(0);' onclick=$('#div" + folder.Id + "').fadeIn(1000); >View</a>"; //folder.History;

        string divs = "<div id='p2' style='top: 3px;'> <div id='div" + folder.Id + "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'>" +
            "<img src='" + BASE_URL + "/static/images/close.png' title='Close folder history' onclick=$('#div" + folder.Id + "').fadeOut(500);" +
                    " style='float: right; cursor: pointer; position: relative;height: 15px;'/><div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;'>" + GetHistory(folder.History) + "</ul> </div></div>";
        history.InnerHtml += divs;


        UserDetails user = new UserDetailsDAO().GetUserDetailbyId(folder.OwnerUserId, cnxnString, logPath);
        if (user != null)
        {
            if (user.UserName.ToLower() == Session["UserName"].ToString().ToLower())
                ownerUser.InnerHtml = "You";
            else
                ownerUser.InnerHtml = user.FirstName + " " + user.LastName;
        }
        else
        {
            ownerUser.InnerHtml = "MIS/Exam User";
        }
        dateUpdated.InnerHtml = folder.DateUpdated.ToString("dd/MM/yyyy");
        dateCreated.InnerHtml = folder.DateCreated.ToString("dd/MM/yyyy");

        //tags.InnerHtml = (folder.Tags == string.Empty) ? "-" : folder.Tags;
        if (folder.Tags == string.Empty)
        {
            tags.InnerHtml = "--";
            tags.Attributes.Add("title", "no any tag present");
        }
        else
        {
            tags.InnerHtml = (folder.Tags.Length > 40 ? folder.Tags.Remove(40, folder.Tags.Length - 40).Trim() + "..." : folder.Tags);
            tags.Attributes.Add("title", folder.Tags);
        }

        history.Style["width"] = "50px";
        history.Style["min-width"] = "50px";

        record.Controls.Add(folderName);
        record.Controls.Add(folderDescription);
        record.Controls.Add(dateCreated);
        //record.Controls.Add(dateUpdated);
        record.Controls.Add(ownerUser);
        record.Controls.Add(tags);
        record.Controls.Add(history);
        record.Controls.Add(buttons);

        record.Attributes.Add("class", "FolderHeader");

        if (isColorBackground)
        {
            record.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            record.Attributes.Add("style", "padding-top: 5px;");
        }

        if (isOwnerUser || Session["RoleType"].ToString() == "3" || Session["RoleType"].ToString() == "4")
        {
            //record.Controls.Add(editButton);
            Folders.Controls.Add(record);
        }
        else if (isAccessPermission && folder.Permission.ToLower() != "private")
            Folders.Controls.Add(record);
        //Folders.Controls.Add(record);
    }

    public string GetHistory(string xml)
    {
        List<FolderHistoryManager> folderHistoryList = FolderHistoryManager.GetHistoryList(UnescapeFormat.UnescapeXML(xml));

        string li = "";
        int counter = 1;

        if (folderHistoryList != null)
        {
            foreach (FolderHistoryManager obj in folderHistoryList)
            {
                li += "<li>(" + counter + ") " + obj.Activity + " by " + obj.User + " on " + obj.Date.ToString("dd/MM/yyyy") + ".</li>";
                counter++;
            }
        }
        else
        {
            li = "<li>Currently you do not have any history for this item.</li>";
        }

        return li;
    }

    private void AddFileHeader()
    {
        HtmlGenericControl header = new HtmlGenericControl("div");
        HtmlGenericControl fileName = new HtmlGenericControl("div");
        HtmlGenericControl history = new HtmlGenericControl("div");
        HtmlGenericControl dateCreated = new HtmlGenericControl("div");
        HtmlGenericControl dateUpdated = new HtmlGenericControl("div");
        HtmlGenericControl fileDescription = new HtmlGenericControl("div");
        HtmlGenericControl ownerUser = new HtmlGenericControl("div");
        HtmlGenericControl tags = new HtmlGenericControl("div");

        dateCreated.Style["width"] = "100px";
        fileDescription.Style["width"] = "163px";

        fileName.InnerHtml = "<u>File Name</u>";
        fileName.Attributes.Add("class", "DivSortButton");
        fileName.Attributes.Add("onClick", "SortButtonClick('" + btnSortFileName.ClientID + "');");



        fileDescription.InnerHtml = "File Description";
        history.InnerHtml = "History";
        //ownerUser.InnerHtml = "Owner";
        ownerUser.InnerHtml = "<u>Owner</u>";
        ownerUser.Attributes.Add("class", "DivSortButton");
        ownerUser.Attributes.Add("onClick", "SortButtonClick('" + btnSortFileOwner.ClientID + "');");


        dateUpdated.InnerHtml = "Updated";

        dateCreated.InnerHtml = "<u>Created On</u>";
        dateCreated.Attributes.Add("class", "DivSortButton");
        dateCreated.Attributes.Add("onClick", "SortButtonClick('" + btnSortFileDate.ClientID + "');");

        tags.InnerHtml = "Tags";

        //header.Attributes.Add("class", "FolderHeader");

        header.Controls.Add(fileName);
        header.Controls.Add(fileDescription);
        header.Controls.Add(dateCreated);
        //header.Controls.Add(dateUpdated);
        header.Controls.Add(ownerUser);
        header.Controls.Add(tags);
        header.Controls.Add(history);
        header.Attributes.Add("class", "FolderHeader");
        header.Attributes.Add("style", "font-weight: bold; border-bottom: 1px solid #CCC; padding-bottom: 5px; ");
        Files.InnerHtml = string.Empty;
        Files.Controls.Add(header);
    }

    private void AddFileRecord(FileDetails file, bool isColorBackground = false)
    {
        bool isOwnerUser;
        bool isAccessPermission = IsAccessPermission(file.OwnerUserId, file.UserAccess, file.Permission, out isOwnerUser);


        HtmlGenericControl record = new HtmlGenericControl("div");
        HtmlGenericControl fileName = new HtmlGenericControl("div");
        HtmlGenericControl history = new HtmlGenericControl("div");
        HtmlGenericControl dateCreated = new HtmlGenericControl("div");
        HtmlGenericControl dateUpdated = new HtmlGenericControl("div");
        HtmlGenericControl fileDescription = new HtmlGenericControl("div");
        HtmlGenericControl ownerUser = new HtmlGenericControl("div");
        HtmlGenericControl tags = new HtmlGenericControl("div");

        HtmlGenericControl buttons = new HtmlGenericControl("div");
        buttons.Style["width"] = "100px";
        dateCreated.Style["width"] = "80px";
        fileDescription.Style["width"] = "170px";

        //buttons.InnerHtml = "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div>&nbsp;&nbsp; " +
        //    "<div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>";

        string rename = "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div>&nbsp;&nbsp; ";
        string delete = "<div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>";


        file.RelativePath = file.RelativePath.Replace("~/", "");
        string path;

        if (isUseNetworkPath)
            path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], file.RelativePath);
        else
            path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), file.RelativePath);

        fileName.InnerHtml = "<a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "' title='" + file.OriginalFileName + "'> <img src='" + BASE_URL + "/static/images/file.png' width='12px' height='12px' style='padding: 5px; float: left' />" + (file.OriginalFileName.Length > 30 ? file.OriginalFileName.Remove(30, file.OriginalFileName.Length - 30) + "..." : file.OriginalFileName) + "</a>";

        //fileDescription.InnerHtml = file.FileDescription;
        fileDescription.InnerHtml = (file.FileDescription.Length > 55 ? file.FileDescription.Remove(55, file.FileDescription.Length - 55).Trim() + "..." : string.IsNullOrEmpty(file.FileDescription) ? "-" : file.FileDescription);
        fileDescription.Attributes.Add("title", file.FileDescription);


        HtmlGenericControl MoreOption = new HtmlGenericControl("div");
        MoreOption.Style["width"] = "50px";
        MoreOption.Style["min-width"] = "50px";
        MoreOption.InnerHtml = "<div style='width:50px;min-width: 50px;cursor: pointer;'> <a href='javascript:void(0);'  onclick=$('#div" + file.Id + "_MoreFile" + "').fadeIn(1000); >Options...</a></div> ";

        //Share string=<div style='cursor: pointer;padding-bottom: 5px;' onclick=\"FileShare('" + file.Id + "','" + file.OriginalFileName + "','" + Session["UserName"].ToString() + "');\">Share</div>
        string MoreOptionMenu = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/>" +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        string MoreOptionMenuAll = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div style='padding-bottom: 5px;cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div><div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";
        string MoreOptionMenuWithRename = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>    <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        string MoreOptionMenuDelete = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div> <div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        string MoreOptionMenuOnlyRename = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div><div style='cursor: pointer;padding-bottom: 5px;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";
        string MoreOptionMenuOnlyDelete = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";



        //MoreOption.InnerHtml += MoreOptionMenu;

        if (isOwnerUser)
        {
            MoreOption.InnerHtml += MoreOptionMenuAll;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (!isOwnerUser && file.Permission.ToLower() == "rename")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyRename;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (file.Permission.ToLower() == "delete")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyDelete;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (file.Permission.ToLower() == "share")
        {
            MoreOption.InnerHtml += MoreOptionMenu;
            buttons.InnerHtml += MoreOption.InnerHtml;
        }
        else if (file.Permission.ToLower() == "update")
        {
            MoreOption.InnerHtml += MoreOptionMenuWithRename;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }

        history.InnerHtml = "<a href='javascript:void(0);' onclick=$('#divF" + file.Id + "').fadeIn(1000); >View</a>"; //folder.History;

        string divs = "<div id='p2' style='top: 3px;'> <div id='divF" + file.Id + "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'>" +
            "<img src='" + BASE_URL + "/static/images/close.png' title='Close file history' onclick=$('#divF" + file.Id + "').fadeOut(500);" +
                    " style='float: right; cursor: pointer; position: relative;height: 15px;'/><div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;'>" + GetHistory(file.History) + "</ul> </div></div>";
        history.InnerHtml += divs;

        UserDetails user = new UserDetailsDAO().GetUserDetailbyId(file.OwnerUserId, cnxnString, logPath);
        if (user != null)
        {
            if (user.UserName.ToLower() == Session["UserName"].ToString().ToLower())
            {
                ownerUser.InnerHtml = "You";
            }
            else
            {
                ownerUser.InnerHtml = user.FirstName + " " + user.LastName;
            }
        }
        else
            ownerUser.InnerHtml = user.FirstName + " " + user.LastName;

        dateUpdated.InnerHtml = file.DateUpdated.ToString("dd/MM/yyyy");
        dateCreated.InnerHtml = file.DateCreated.ToString("dd/MM/yyyy");

        //tags.InnerHtml = (file.Tags == string.Empty) ? "-" : file.Tags;
        if (file.Tags == string.Empty)
        {
            tags.InnerHtml = "--";
            tags.Attributes.Add("title", "no any tag present");
        }
        else
        {
            tags.InnerHtml = (file.Tags.Length > 40 ? file.Tags.Remove(40, file.Tags.Length - 40).Trim() + "..." : file.Tags);
            tags.Attributes.Add("title", file.Tags);
        }

        history.Style["width"] = "50px";
        history.Style["min-width"] = "50px";

        record.Controls.Add(fileName);
        record.Controls.Add(fileDescription);
        record.Controls.Add(dateCreated);
        //record.Controls.Add(dateUpdated);
        record.Controls.Add(ownerUser);
        record.Controls.Add(tags);
        record.Controls.Add(history);
        record.Controls.Add(buttons);
        record.Attributes.Add("class", "FolderHeader");

        if (isColorBackground)
        {
            record.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            record.Attributes.Add("style", "padding-top: 5px;");
        }

        //Files.Controls.Add(record);
        if (isOwnerUser || Session["RoleType"].ToString() == "3" || Session["RoleType"].ToString() == "4")
            Files.Controls.Add(record);
        else if (isAccessPermission && file.Permission.ToLower() != "private")
            Files.Controls.Add(record);
    }

    private string GetUserList()
    {
        //Below code is used as no access rights given to user
        if (!accessright)
        {
            return SecurityElement.Escape("<Users accesstype='Delete' usertype='No Access'>None</Users>");
        }
        else
        {
            return SecurityElement.Escape("<Users accesstype='" + ddAccessType.SelectedItem.Text + "' usertype='No Access'>None</Users>");
        }

        /*Sharat-This Code is Used When Access rights are given
         if (rblChooseUsers.SelectedItem.Value == "0")
        {
            return SecurityElement.Escape("<Users accesstype='" + ddAccessType.SelectedItem.Text + "' usertype='" + rblChooseUsers.SelectedValue + "'>All</Users>");
        }
        else if (rblChooseUsers.SelectedItem.Value == "1")
        {
            string userString = "<Users accesstype='" + ddAccessType.SelectedItem.Text + "' usertype='" + rblChooseUsers.SelectedValue + "'>";

            foreach (ListItem group in lbGroups.Items)
            {
                if (group.Selected)
                {
                    GroupsDetails groupDetails = new GroupsDetailsDAO().GetGroupById(int.Parse(group.Value));
                    if (groupDetails != null)
                    {
                        List<string> users = StaffMemberManager.GetStaffIdList(groupDetails.Users);
                        if (users != null)
                        {
                            foreach (string user in users)
                            {
                                userString += "<User>" + user + "</User>";
                            }
                        }
                    }
                }
            }

            userString += "</Users>";
            return SecurityElement.Escape(userString);
        }
        else if (rblChooseUsers.SelectedItem.Value == "2")
        {
            string[] users = txtUsers.Text.Split(',');

            string userString = "<Users accesstype='" + ddAccessType.SelectedItem.Text + "' usertype='" + rblChooseUsers.SelectedValue + "'>";

            foreach (ListItem user in lbFolderUser.Items)
            {
                if (user.Selected)
                {
                    userString += "<User>" + user.Text + "</User>";
                }
            }

            userString += "</Users>";
            return SecurityElement.Escape(userString);
        }
        else if (rblChooseUsers.SelectedItem.Value == "3")
        {
            return SecurityElement.Escape("<Users accesstype='" + ddAccessType.SelectedItem.Text + "' usertype='" + rblChooseUsers.SelectedValue + "'>None</Users>");
        }

        return string.Empty;*/
    }
    //private string GetUserList()
    //{
    //    if (rblChooseUsers.SelectedItem.Value == "0")
    //    {
    //        return SecurityElement.Escape("<Users accesstype='" + ddAccessType.SelectedItem.Text + "'>All</Users>");
    //    }
    //    else if (rblChooseUsers.SelectedItem.Value == "3")
    //    {
    //        return SecurityElement.Escape("<Users accesstype='" + ddAccessType.SelectedItem.Text + "'>No Access</Users>");
    //    }
    //    else if (rblChooseUsers.SelectedItem.Value == "2")
    //    {
    //        string[] users = txtUsers.Text.Split(',');
    //        string userString = "<Users accesstype='" + ddAccessType.SelectedItem.Text + "'>";

    //        foreach (ListItem user in lbFolderUser.Items)
    //        {
    //            if (user.Selected)
    //            {
    //                userString += "<User>" + user.Text + "</User>";
    //            }
    //        }

    //        userString += "</Users>";
    //        return SecurityElement.Escape(userString);
    //    }

    //    return string.Empty;
    //}

    protected void btnAddAllFiles_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ListAllUploadedFiles"] != null && ViewState["HashAllFileName"] != null && ViewState["TempFiles"] != null)
            {
                Hashtable hashAllFileName = (Hashtable)ViewState["HashAllFileName"];
                List<string> listAllFiles = (List<string>)ViewState["ListAllUploadedFiles"];
                Hashtable hashTempFiles = (Hashtable)ViewState["TempFiles"];
                string path = string.Empty;

                if (isUseNetworkPath)
                    path = ConfigurationManager.AppSettings["DMS_PATH"];
                else
                    path = Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]);

                int count = 0;

                string[] docs = hdnDocType.Value.Split('|');
                string[] desc = hdnFileDesc.Value.Split('|');
                string[] access = hdnFileAccess.Value.Split('|');
                string[] tags = hdnFileTags.Value.Split('|');

                foreach (string renamedfile in listAllFiles)
                {
                    string sfile = hashTempFiles[renamedfile].ToString();

                    if (!string.IsNullOrEmpty(sfile.ToString()))
                    {
                        if (File.Exists(sfile))
                        {
                            File.Move(sfile, path + "/" + renamedfile);
                        }
                    }

                    //File.Create(path + "/" + renamedfile);

                    //FastZip archiveFile = new FastZip();
                    //archiveFile.ExtractZip(Server.MapPath("compressed") + "/" + renamedfile, Server.MapPath("extracted"), "");


                    string relativePath = renamedfile;
                    UserDetailsDAO userDetails = new UserDetailsDAO();
                    UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);

                    FolderHistoryManager fHM = new FolderHistoryManager();
                    fHM.Date = DateTime.Now;
                    fHM.Activity = "File Added";
                    fHM.User = Session["UserName"].ToString();
                    string history = FolderHistoryManager.CreateHistory(fHM);

                    FileDetailsDAO file = new FileDetailsDAO();
                    file.CreateFileDetails(renamedfile, hashAllFileName[renamedfile].ToString(), desc[count], relativePath, DateTime.Now, DateTime.Now, userDetail.Id, GetFileUserList(), -1, history, tags[count], (string.IsNullOrEmpty(access[count]) ? "Read" : access[count]), docs[count], cnxnString, logPath);

                    int maxCount = file.GetMaxFileId(cnxnString, logPath);

                    UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                    act.CreateUserActivityDetails("File created", hashAllFileName[renamedfile].ToString(), maxCount, DateTime.Now, userDetail.UserName, cnxnString, logPath);
                    count++;
                }

                divFileResult.InnerHtml = "Selected files has been uploaded successfully";
                divFileResult.Style.Add("color", "green");

                divAllFilesResult.InnerHtml = "Selected files has been uploaded successfully";
                divAllFilesResult.Style.Add("color", "green");
                divAllFilesResult.Visible = true;
                btnAddAllFiles.Visible = false;
                btnAllFilesCancel.Visible = false;

                PopulateFolders();
                PopulateFiles();
                ClearFields();
            }
        }
        catch { }
    }

    protected void btnFileAdd_Click(object sender, EventArgs e)
    {
        if (ValidateFileFeilds())
        {
            try
            {
                divFileResult.InnerHtml = string.Empty;
                divAllFilesResult.InnerHtml = string.Empty;

                string path;// = Path.Combine(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"], GetPathHirarchy());//ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"];

                if (isUseNetworkPath)
                    path = ConfigurationManager.AppSettings["DMS_PATH"];
                else
                    path = Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    // Get the HttpFileCollection
                    HttpFileCollection hfc = Request.Files;
                    List<string> listAllFiles = new List<string>();
                    Hashtable hashFileName = new Hashtable();
                    Hashtable hashTempFile = new Hashtable();
                    int count = 1;
                    selectedFilesCount = hfc.Count;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];

                        string fileName = Path.GetFileName(hpf.FileName);
                        string renamedfile = Path.GetFileNameWithoutExtension(hpf.FileName) + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.ToString("HHmmss") + Path.GetExtension(hpf.FileName);
                        string tempfile = Path.GetFileNameWithoutExtension(hpf.FileName) + "-temp-" + DateTime.Now.Ticks + Path.GetExtension(hpf.FileName);

                        string extention = Path.GetExtension(hpf.FileName);

                        if (extention == ".doc" || extention == ".docx" || extention == ".pdf" || extention == ".xls" || extention == ".xlsx" || extention == ".ppt" || extention == ".pptx" ||
                            extention == ".jpeg" || extention == ".jpg" || extention == ".png" || extention == ".bmp" || extention == ".tiff" || extention == ".txt" || extention == ".pst" || extention == ".zip"
                            || extention == ".rar" || extention == ".7z" || extention == ".sql")
                        {
                            if (hpf.ContentLength > 0)
                            {
                                //hpf.SaveAs(path + "/" + renamedfile);
                                string fullPath = path + "/" + tempfile;
                                hpf.SaveAs(fullPath);
                                listAllFiles.Add(renamedfile);
                                hashFileName.Add(renamedfile, hpf.FileName);
                                hashTempFile.Add(renamedfile, fullPath);
                            }
                        }
                        else
                        {

                            string Msg = "<script>alert('Please upload a valid file with following extensions \"DOC, DOCX, PDF, XLS, XLSX, PPT, PPTX, JPEG, JPG, PNG, BMP, TIFF, TXT, PST, ZIP, RAR, 7Z,  and SQL\"');</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FilePopup').fadeIn(1000);</script>", false);
                            fuFile.Focus();
                            PopulateFolders();
                            PopulateFiles();
                            return;
                        }
                        GenerateUploadedFilesList(hpf.FileName, count, txtFileDesc.Text, txtFileTags.Text, ddlFileType.SelectedValue, ddlFileAccessType.SelectedItem.Text);
                        count++;
                    }
                    ViewState["ListAllUploadedFiles"] = listAllFiles;
                    ViewState["HashAllFileName"] = hashFileName;
                    ViewState["TempFiles"] = hashTempFile;

                    btnAddAllFiles.Visible = true;
                    btnAllFilesCancel.Visible = true;
                    //UploadAllFilesPopup.Visible = true;
                    UploadAllFilesPopup.Style["display"] = "block";
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                divFileResult.Style.Add("color", "red");
            }
        }
        else
        {
            PopulateFolders();
            PopulateFiles();
            divFileResult.Style.Add("color", "red");
        }
    }

    public void GenerateUploadedFilesList(string fileName, int count, string fileDescription, string fileTags, string docType, string accesstype)
    {

        HtmlGenericControl divRowContainer = new HtmlGenericControl("div");
        divRowContainer.ID = "RowContainer" + count;
        divRowContainer.Style["padding"] = "15px";

        /* 1st Column*/
        HtmlGenericControl RowdivFileName = new HtmlGenericControl("div");
        RowdivFileName.ID = "Rowdivfilename" + count;

        RowdivFileName.Attributes["title"] = fileName;

        if (fileName.Length > 38)
        {
            fileName = fileName.Substring(0, 38) + "...";
        }

        RowdivFileName.InnerText = fileName;

        RowdivFileName.Style["width"] = "18%";
        RowdivFileName.Style["float"] = "left";
        RowdivFileName.Style["clear"] = "both";

        /* 2nd Column*/
        HtmlGenericControl RowdivFileType = new HtmlGenericControl("div");
        RowdivFileType.ID = "Rowdivfiletype" + count;
        RowdivFileType.InnerText = "Document Type";
        RowdivFileType.Style["width"] = "5%";
        RowdivFileType.Style["float"] = "left";

        /* 3rd Column*/
        HtmlGenericControl RowdivFileTypeValue = new HtmlGenericControl("div");
        RowdivFileTypeValue.ID = "RowdivfiletypeValue" + count;
        RowdivFileTypeValue.Style["width"] = "13%";
        RowdivFileTypeValue.Style["float"] = "left";

        DropDownList RowddTypeValue = new DropDownList();
        RowddTypeValue.ID = "RowddTypeValue" + count;
        RowddTypeValue.DataSource = Configurations.GetDocumnettypes();
        RowddTypeValue.DataTextField = "Documents";
        RowddTypeValue.DataValueField = "Documents";
        RowddTypeValue.DataBind();
        if (!string.IsNullOrEmpty(docType))
        {
            RowddTypeValue.SelectedValue = docType;
        }
        RowdivFileTypeValue.Controls.Add(RowddTypeValue);

        /* 4th Column*/
        HtmlGenericControl RowdivFileDescription = new HtmlGenericControl("div");
        RowdivFileDescription.ID = "RowdivfileDescription" + count;
        RowdivFileDescription.InnerText = "File Description";
        RowdivFileDescription.Style["width"] = "8%";
        RowdivFileDescription.Style["float"] = "left";

        /* 5th Column*/
        HtmlGenericControl RowdivFileDescriptionValue = new HtmlGenericControl("div");
        RowdivFileDescriptionValue.ID = "RowdivfiledescriptionValue" + count;
        RowdivFileDescriptionValue.Style["width"] = "13%";
        RowdivFileDescriptionValue.Style["float"] = "left";

        TextBox RowtxtFileDescriptionValue = new TextBox();
        RowtxtFileDescriptionValue.ID = "RowtxtFileDescriptionValue" + count;
        RowtxtFileDescriptionValue.Text = fileDescription;
        RowdivFileDescriptionValue.Controls.Add(RowtxtFileDescriptionValue);

        /* 6th Column*/
        HtmlGenericControl RowdivAccess = new HtmlGenericControl("div");
        RowdivAccess.ID = "RowdivAccess" + count;
        RowdivAccess.InnerText = "Access";
        RowdivAccess.Style["width"] = "4%";
        RowdivAccess.Style["float"] = "left";

        /* 7th Column*/
        HtmlGenericControl RowdivAccessValue = new HtmlGenericControl("div");
        RowdivAccessValue.ID = "RowdivAccessValue" + count;
        RowdivAccessValue.Style["width"] = "14%";
        RowdivAccessValue.Style["float"] = "left";


        DropDownList RowddAccessValue = new DropDownList();
        RowddAccessValue.ID = "RowddAccessValue" + count;
        RowddAccessValue.Items.Add("Read");
        RowddAccessValue.Items.Add("Share");
        RowddAccessValue.Items.Add("Rename");
        RowddAccessValue.Items.Add("Update");
        RowddAccessValue.Items.Add("Delete");
        RowddAccessValue.Items.Add("Private");

        if (!string.IsNullOrEmpty(accesstype))
        {
            RowddAccessValue.SelectedValue = accesstype;
        }

        RowdivAccessValue.Controls.Add(RowddAccessValue);

        /* 8th Column*/
        HtmlGenericControl RowdivTags = new HtmlGenericControl("div");
        RowdivTags.ID = "Rowdivtags" + count;
        RowdivTags.InnerText = "Tags";
        RowdivTags.Style["width"] = "3%";
        RowdivTags.Style["float"] = "left";

        /* 9th Column*/
        HtmlGenericControl RowdivTagsValue = new HtmlGenericControl("div");
        RowdivTagsValue.ID = "RowdivTagsValue" + count;
        RowdivTagsValue.Style["width"] = "13%";
        RowdivTagsValue.Style["float"] = "left";

        TextBox RowtxtTageValue = new TextBox();
        RowtxtTageValue.ID = "RowtxtTageValue" + count;
        RowtxtTageValue.Text = fileTags;
        RowdivTagsValue.Controls.Add(RowtxtTageValue);

        /* 10th Column*/
        HtmlGenericControl RowdivTagsExample = new HtmlGenericControl("div");
        RowdivTagsExample.ID = "RowdivTagsExample" + count;
        RowdivTagsExample.InnerText = "(Comma separated tags)";
        RowdivTagsExample.Style["width"] = "9%";
        RowdivTagsExample.Style["float"] = "left";

        /* Adding above div in a row */

        divRowContainer.Controls.Add(RowdivFileName);
        divRowContainer.Controls.Add(RowdivFileType);
        divRowContainer.Controls.Add(RowdivFileTypeValue);
        divRowContainer.Controls.Add(RowdivFileDescription);
        divRowContainer.Controls.Add(RowdivFileDescriptionValue);

        accessright = Configurations.GetAccessRights();
        if (accessright)
        {
            divRowContainer.Controls.Add(RowdivAccess);
            divRowContainer.Controls.Add(RowdivAccessValue);
        }
        else
        {
            RowdivFileName.Style["width"] = "15%";
            RowdivFileType.Style["width"] = "10%";
            RowdivFileTypeValue.Style["width"] = "15%";
            RowdivFileDescription.Style["width"] = "10%";
            RowdivFileDescriptionValue.Style["width"] = "14%";
            RowdivTags.Style["width"] = "4%";
            RowdivTagsValue.Style["width"] = "14%";
            RowdivTagsExample.Style["width"] = "15%";
        }
        divRowContainer.Controls.Add(RowdivTags);
        divRowContainer.Controls.Add(RowdivTagsValue);
        divRowContainer.Controls.Add(RowdivTagsExample);

        HtmlGenericControl HRLine = new HtmlGenericControl("hr");
        HRLine.Style["float"] = "left";
        HRLine.Style["clear"] = "both";
        HRLine.Style["width"] = "100%";
        divRowContainer.Controls.Add(HRLine);

        /* Adding created row into the page control */

        FilesRowContainer.Controls.Add(divRowContainer);

    }

    protected void btnAllFilesCancel_Click(object sender, EventArgs e)
    {
        UploadAllFilesPopup.Style["display"] = "none";
        ClearFields();
        PopulateFolders();
        PopulateFiles();
    }

    private string GetFileUserList()
    {
        //This Code is used as there is no access right given to user
        if (!accessright)
        {
            return SecurityElement.Escape("<Users accesstype='Delete' usertype='All Users'>All</Users>");
        }
        else
        {
            return SecurityElement.Escape("<Users accesstype='" + ddlFileAccessType.SelectedItem.Text + "' usertype='All Users'>All</Users>");
        }

        /*Sharat-Below Code is used when there is access rights to given users
        if (rblFileChoseUser.SelectedItem.Value == "0")
        {
            return SecurityElement.Escape("<Users accesstype='" + ddlFileAccessType.SelectedItem.Text + "' usertype='" + rblFileChoseUser.SelectedValue + "'>All</Users>");
        }
        else if (rblFileChoseUser.SelectedItem.Value == "1")
        {
            string userString = "<Users accesstype='" + ddlFileAccessType.SelectedItem.Text + "' usertype='" + rblFileChoseUser.SelectedValue + "'>";

            foreach (ListItem group in lbFileGroups.Items)
            {
                if (group.Selected)
                {
                    GroupsDetails groupDetails = new GroupsDetailsDAO().GetGroupById(int.Parse(group.Value));
                    if (groupDetails != null)
                    {
                        List<string> users = StaffMemberManager.GetStaffIdList(groupDetails.Users);
                        if (users != null)
                        {
                            foreach (string user in users)
                            {
                                userString += "<User>" + user + "</User>";
                            }
                        }
                    }
                }
            }

            userString += "</Users>";
            return SecurityElement.Escape(userString);
        }
        else if (rblFileChoseUser.SelectedItem.Value == "2")
        {
            string[] users = txtUsers.Text.Split(',');
            string userString = "<Users accesstype='" + ddlFileAccessType.SelectedItem.Text + "' usertype='" + rblFileChoseUser.SelectedValue + "'>";

            foreach (ListItem user in lbFileUsers.Items)
            {
                if (user.Selected)
                {
                    userString += "<User>" + user.Text + "</User>";
                }
            }

            userString += "</Users>";
            return SecurityElement.Escape(userString);
        }
        else if (rblFileChoseUser.SelectedItem.Value == "3")
        {
            return SecurityElement.Escape("<Users accesstype='" + ddlFileAccessType.SelectedItem.Text + "' usertype='" + rblFileChoseUser.SelectedValue + "'>None</Users>");
        }

        return string.Empty;*/
    }

    public bool ValidateFileFeilds()
    {
        bool isValid = true;
        HttpFileCollection hfc = Request.Files;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];
            if (hpf.ContentLength < 1)
            {
                divFileResult.InnerHtml = "Please Upload file.";
                isValid = false;
            }
        }
        // by akram
        //if (!fuFile.HasFile)
        //{
        //    divFileResult.InnerHtml = "Please Upload file.";
        //    isValid = false;

        //}
        if (string.IsNullOrEmpty(txtFileDesc.Text))
        {
            divresult.InnerHtml = "Please enter File description";
            isValid = false;
        }

        return isValid;
    }
    private void AddFolderRecordSearch(FolderDetails folder, bool isColorBackground = false)
    {
        bool isOwnerUser;
        bool isAccessPermission = IsAccessPermission(folder.OwnerUserId, folder.UserAccess, folder.Permission, out isOwnerUser);

        HtmlGenericControl record = new HtmlGenericControl("div");
        HtmlGenericControl folderName = new HtmlGenericControl("div");
        HtmlGenericControl history = new HtmlGenericControl("div");
        HtmlGenericControl dateCreated = new HtmlGenericControl("div");
        HtmlGenericControl dateUpdated = new HtmlGenericControl("div");
        HtmlGenericControl folderDescription = new HtmlGenericControl("div");
        HtmlGenericControl ownerUser = new HtmlGenericControl("div");
        HtmlGenericControl tags = new HtmlGenericControl("div");
        HtmlGenericControl checkbox = new HtmlGenericControl("div");
        //checkbox.Style["width"] = "100px";
        //checkbox.InnerHtml = "<div> <input type='checkbox' id='" + folder.Id + "' /></div>";

        // added by vasim
        HtmlGenericControl buttons = new HtmlGenericControl("div");
        buttons.Style["width"] = "100px";
        dateCreated.Style["width"] = "100px";
        folderDescription.Style["width"] = "163px";

        HtmlGenericControl editButton = new HtmlGenericControl("div");
        editButton.Style["width"] = "100px";
        editButton.InnerHtml = "<div style='width:90px;cursor: pointer;'> <a href='javascript:void(0)' onclick=\"ClickEdit('" + folder.Id + "','" + btnEditFolder.ClientID + "');\" style='color:black;'>Edit Folder</a></div>";


        string rename = "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div>&nbsp;&nbsp; ";
        string delete = "<div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>";

        buttons.InnerHtml = string.Empty;
        //buttons.InnerHtml = rename + delete;

        folderName.InnerHtml = "<input type='checkbox' id='" + folder.Id + "' style='width: 13px;float:left;' class='foldercheckbox' /><a style='vertical-align: top;' href='InnerFolder.aspx?id=" + folder.Id + "'> <img src='" + BASE_URL + "/static/images/folder.png' width='12px' height='12px' style='padding: 5px; float: left' /> " + folder.Alias + "</a>";


        folderDescription.InnerHtml = (folder.FolderDescription.Length > 35 ? folder.FolderDescription.Remove(35, folder.FolderDescription.Length - 35).Trim() + "..." : folder.FolderDescription);
        folderDescription.Attributes.Add("title", folder.FolderDescription);


        string MoreOptionExport = "<div style='padding-bottom: 5px;cursor: pointer;'> <a href='" + BASE_URL + "/api/DownLoadZip.ashx?folderId=" + folder.Id + "'>Download as Zip</a> </div>";

        HtmlGenericControl MoreOption = new HtmlGenericControl("div");
        MoreOption.Style["width"] = "50px";
        MoreOption.Style["min-width"] = "50px";
        MoreOption.InnerHtml = "<div style='width:50px;cursor: pointer;'> <a href='javascript:void(0)'  onclick=$('#div" + folder.Id + "_More" + "').fadeIn(1000); >Options...</a></div> ";

        //string MoreOptionMenu = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 115px; position: absolute;background-color: #FFF; left: 1151px;'> <img src='" + BASE_URL + "/static/image/closeIcon1.png' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        //"<ul><div style='cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div> </ul> </div>";
        string MoreOptionMenuAll = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
               "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='cursor: pointer;color: gray;padding-bottom: 5px;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='cursor: pointer;padding-bottom: 5px;'>Delete</div>" + MoreOptionExport + "<div style='padding-bottom: 5px;cursor: pointer;'> <a href='javascript:void(0)' onclick=\"ClickEdit('" + folder.Id + "','" + btnEditFolder.ClientID + "');\" style='color:black;'>Edit</a></div> </ul> </div>";

        string MoreOptionMenuRename = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='padding-bottom: 5px;cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div>" + MoreOptionExport + " </ul></div>"; //<div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>

        string MoreOptionMenuDelete = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='padding-bottom: 5px;cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='cursor: pointer;padding-bottom: 5px;'>Delete</div> " + MoreOptionExport + " </ul> </div>";

        string MoreOptionMenu = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div> <ul><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CutFolder('" + folder.Id + "','" + btnCut.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFolder('" + folder.Id + "','" + btnCopy.ClientID + "');\">Copy</div> <div id='divPaste' style='padding-bottom: 5px;cursor: pointer;color: gray;' onclick=\"PasteFolder('" + folder.Id + "','" + btnPaste.ClientID + "');\">Paste</div><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='cursor: pointer;padding-bottom: 5px;'>Delete</div>" + MoreOptionExport + " </ul> </div>";

        string MoreOptionMenuOnlyRename = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul> <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFolder('" + folder.Id + "','" + folder.Alias + "');\">Rename</div>" + MoreOptionExport + " </ul> </div>";

        string MoreOptionMenuOnlyDelete = "<div id='div" + folder.Id + "_More" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%;'> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + folder.Id + "_More" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div onclick=\"DeleteFolder('" + folder.Id + "','" + folder.Alias + "','" + btnDelete.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div> " + MoreOptionExport + "</ul> </div>";

        //MoreOption.InnerHtml += MoreOptionMenu;

        if (isOwnerUser)
        {
            MoreOption.InnerHtml += MoreOptionMenuAll;// +MoreOptionExpoert;
            //buttons.InnerHtml = rename + delete + MoreOption.InnerHtml;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (!isOwnerUser && folder.Permission.ToLower() == "rename")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyRename;// +MoreOptionExpoert;
            //buttons.InnerHtml += rename;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "delete")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyDelete;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "share")
        {
            MoreOption.InnerHtml += MoreOptionMenu;// +MoreOptionExpoert;
            buttons.InnerHtml += MoreOption.InnerHtml;
        }
        else if (folder.Permission.ToLower() == "update")
        {
            MoreOption.InnerHtml += MoreOptionMenuRename;// +MoreOptionExpoert;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }

        history.InnerHtml = "<a href='javascript:void(0);' onclick=$('#div" + folder.Id + "').fadeIn(1000); >View</a>"; //folder.History;

        string divs = "<div id='p2' style='top: 3px;'> <div id='div" + folder.Id + "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'>" +
            "<img src='" + BASE_URL + "/static/images/close.png' title='Close folder history' onclick=$('#div" + folder.Id + "').fadeOut(500);" +
                    " style='float: right; cursor: pointer; position: relative;height: 15px;'/><div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;'>" + GetHistory(folder.History) + "</ul> </div></div>";
        history.InnerHtml += divs;


        UserDetails user = new UserDetailsDAO().GetUserDetailbyId(folder.OwnerUserId, cnxnString, logPath);
        if (user != null)
        {
            if (user.UserName.ToLower() == Session["UserName"].ToString().ToLower())
                ownerUser.InnerHtml = "You";
            else
                ownerUser.InnerHtml = user.FirstName + " " + user.LastName;
        }
        else
        {
            ownerUser.InnerHtml = "MIS/Exam User";
        }
        dateUpdated.InnerHtml = folder.DateUpdated.ToString("dd/MM/yyyy");
        dateCreated.InnerHtml = folder.DateCreated.ToString("dd/MM/yyyy");
        tags.InnerHtml = (folder.Tags == string.Empty) ? "-" : folder.Tags;

        history.Style["width"] = "115px";
        //record.Controls.Add(checkbox);
        record.Controls.Add(folderName);
        record.Controls.Add(folderDescription);
        record.Controls.Add(dateCreated);
        //record.Controls.Add(dateUpdated);
        record.Controls.Add(ownerUser);
        record.Controls.Add(tags);
        record.Controls.Add(history);
        record.Controls.Add(buttons);

        record.Attributes.Add("class", "FolderHeader");

        if (isColorBackground)
        {
            record.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            record.Attributes.Add("style", "padding-top: 5px;");
        }

        if (isOwnerUser || Session["RoleType"].ToString() == "3" || Session["RoleType"].ToString() == "4")
        {
            //record.Controls.Add(editButton);
            Folders.Controls.Add(record);
        }
        else if (isAccessPermission && folder.Permission.ToLower() != "private")
            Folders.Controls.Add(record);
        //Folders.Controls.Add(record);
    }

    private void AddFileRecordSearch(FileDetails file, bool isColorBackground = false)
    {
        bool isOwnerUser;
        bool isAccessPermission = IsAccessPermission(file.OwnerUserId, file.UserAccess, file.Permission, out isOwnerUser);


        HtmlGenericControl record = new HtmlGenericControl("div");
        HtmlGenericControl fileName = new HtmlGenericControl("div");
        HtmlGenericControl history = new HtmlGenericControl("div");
        HtmlGenericControl dateCreated = new HtmlGenericControl("div");
        HtmlGenericControl dateUpdated = new HtmlGenericControl("div");
        HtmlGenericControl fileDescription = new HtmlGenericControl("div");
        HtmlGenericControl ownerUser = new HtmlGenericControl("div");
        HtmlGenericControl tags = new HtmlGenericControl("div");

        HtmlGenericControl buttons = new HtmlGenericControl("div");
        buttons.Style["width"] = "100px";
        dateCreated.Style["width"] = "100px";
        fileDescription.Style["width"] = "150px";

        //buttons.InnerHtml = "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div>&nbsp;&nbsp; " +
        //    "<div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>";

        string rename = "<div style='width:48px;margin-right:5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div>&nbsp;&nbsp; ";
        string delete = "<div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='width:40px;cursor: pointer;'>Delete</div>";


        file.RelativePath = file.RelativePath.Replace("~/", "");
        string path;

        if (isUseNetworkPath)
            path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], file.RelativePath);
        else
            path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), file.RelativePath);

        fileName.InnerHtml = "<input type='checkbox' id='" + file.Id + "' style='width: 13px;float:left;' class='filecheckbox' /><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "' title='" + file.OriginalFileName + "'> <img src='" + BASE_URL + "/static/images/file.png' width='12px' height='12px' style='padding: 5px; float: left' />" + (file.OriginalFileName.Length > 30 ? file.OriginalFileName.Remove(30, file.OriginalFileName.Length - 30) + "..." : file.OriginalFileName) + "</a>";

        fileDescription.InnerHtml = string.IsNullOrEmpty(file.FileDescription) ? "-" : file.FileDescription;

        HtmlGenericControl MoreOption = new HtmlGenericControl("div");
        MoreOption.Style["width"] = "50px";
        MoreOption.Style["min-width"] = "50px";
        MoreOption.InnerHtml = "<div style='width:50px;cursor: pointer;'> <a href='javascript:void(0);'  onclick=$('#div" + file.Id + "_MoreFile" + "').fadeIn(1000); >Options...</a></div> ";

        //Share string=<div style='cursor: pointer;padding-bottom: 5px;' onclick=\"FileShare('" + file.Id + "','" + file.OriginalFileName + "','" + Session["UserName"].ToString() + "');\">Share</div>
        string MoreOptionMenu = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/>" +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>   <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='padding-bottom: 5px;cursor: pointer;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        string MoreOptionMenuAll = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div style='padding-bottom: 5px;cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div><div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";
        string MoreOptionMenuWithRename = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>    <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div>  <div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        string MoreOptionMenuDelete = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
       "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CutFile('" + file.Id + "','" + btnCutFile.ClientID + "');\">Cut</div><div style='cursor: pointer;padding-bottom: 5px;' onclick=\"CopyFile('" + file.Id + "','" + btnCopyFile.ClientID + "');\">Copy</div> <div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        string MoreOptionMenuOnlyRename = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div style='padding-bottom: 5px;cursor: pointer' onclick=\"RenameFile('" + file.Id + "','" + file.OriginalFileName + "');\">Rename</div><div style='cursor: pointer;padding-bottom: 5px;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";
        string MoreOptionMenuOnlyDelete = "<div id='div" + file.Id + "_MoreFile" + "'class='SmallBox BoxBlue' style='display: none; padding-bottom: 10px; width: 115px; position: absolute;background-color: #FFF; right: 10%; '> <img src='" + BASE_URL + "/static/images/close.png' title='Close options' onclick=$('#div" + file.Id + "_MoreFile" + "').fadeOut(500); style='float: right; cursor: pointer; position: relative;height: 15px;'/> " +
        "<div style='padding: 0px 0px 10px 0px; text-align: center;width: 96px;' class='SmallHeaderText'> Options</div>  <ul><div onclick=\"DeleteFile('" + file.Id + "','" + file.OriginalFileName + "','" + btnDeleteFiles.ClientID + "');\" style='padding-bottom: 5px;cursor: pointer;'>Delete</div><div style='padding-bottom: 5px;cursor: pointer;'><a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'>Download</a></div></ul></div>";

        //MoreOption.InnerHtml += MoreOptionMenu;
        if (isOwnerUser)
        {
            MoreOption.InnerHtml += MoreOptionMenuAll;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (!isOwnerUser && file.Permission.ToLower() == "rename")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyRename;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (file.Permission.ToLower() == "delete")
        {
            MoreOption.InnerHtml += MoreOptionMenuOnlyDelete;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }
        else if (file.Permission.ToLower() == "share")
        {
            MoreOption.InnerHtml += MoreOptionMenu;
            buttons.InnerHtml += MoreOption.InnerHtml;
        }
        else if (file.Permission.ToLower() == "update")
        {
            MoreOption.InnerHtml += MoreOptionMenuWithRename;
            buttons.InnerHtml = MoreOption.InnerHtml;
        }

        history.InnerHtml = "<a href='javascript:void(0);' onclick=$('#divF" + file.Id + "').fadeIn(1000); >View</a>"; //folder.History;

        string divs = "<div id='p2' style='top: 3px;'> <div id='divF" + file.Id + "' class='SmallBox BoxBlue' style='display: none; padding-bottom: 0px; width: 260px; position: absolute;background-color: #FFF; right: 14%;'>" +
            "<img src='" + BASE_URL + "/static/images/close.png' title='Close file history' onclick=$('#divF" + file.Id + "').fadeOut(500);" +
                    " style='float: right; cursor: pointer; position: relative;height: 15px;'/><div style='padding: 0px 10px 10px 10px; text-align: center;width: 225px;' class='SmallHeaderText'> History</div> <ul style='list-style: none;'>" + GetHistory(file.History) + "</ul> </div></div>";
        history.InnerHtml += divs;

        UserDetails user = new UserDetailsDAO().GetUserDetailbyId(file.OwnerUserId, cnxnString, logPath);
        if (user != null)
        {
            if (user.UserName.ToLower() == Session["UserName"].ToString().ToLower())
            {
                ownerUser.InnerHtml = "You";
            }
            else
            {
                ownerUser.InnerHtml = user.FirstName + " " + user.LastName;
            }
        }
        else
            ownerUser.InnerHtml = user.FirstName + " " + user.LastName;

        dateUpdated.InnerHtml = file.DateUpdated.ToString("dd/MM/yyyy");
        dateCreated.InnerHtml = file.DateCreated.ToString("dd/MM/yyyy");
        tags.InnerHtml = (file.Tags == string.Empty) ? "-" : file.Tags;

        history.Style["width"] = "115px";

        record.Controls.Add(fileName);
        record.Controls.Add(fileDescription);
        record.Controls.Add(dateCreated);
        //record.Controls.Add(dateUpdated);
        record.Controls.Add(ownerUser);
        record.Controls.Add(tags);
        record.Controls.Add(history);
        record.Controls.Add(buttons);
        record.Attributes.Add("class", "FolderHeader");

        if (isColorBackground)
        {
            record.Attributes.Add("style", "padding-top: 5px; background-color: lightgoldenrodyellow;");
        }
        else
        {
            record.Attributes.Add("style", "padding-top: 5px;");
        }

        //Files.Controls.Add(record);
        if (isOwnerUser || Session["RoleType"].ToString() == "3" || Session["RoleType"].ToString() == "4")
            Files.Controls.Add(record);
        else if (isAccessPermission && file.Permission.ToLower() != "private")
            Files.Controls.Add(record);
    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSearch.Value) || txtSearch.Value == "Search...")
            {
                PopulateFiles();
                PopulateFolders();
            }
            else
            {
                //populate folders
                List<FolderDetails> folders = new FolderDetailsDAO().GetFolderDetailsForSearch(txtSearch.Value, cnxnString, logPath);

                if (folders != null && folders.Count > 0)
                {
                    divsSearchBtnFolder.Style["display"] = "block";
                    AddFolderHeader();
                    int counter = 0;
                    foreach (FolderDetails folder in folders)
                    {
                        AddFolderRecordSearch(folder, (counter % 2 == 0));

                        counter++;
                    }

                    divSearchText.InnerHtml = "<b>Search Results - " + txtSearch.Value.ToUpper() + "</b>. &nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:void();' title='remove search result' onclick=\"ClickReload('" + btnReload.ClientID + "');\"> <b>X</b></a>";
                    divSearchText.Style["display"] = "block";
                }
                else
                {
                    Folders.InnerHtml = "There are no folder found for '" + txtSearch.Value + "'. <a href='Dashboard.aspx'>Click here</a> to return back to previous screen.";
                }

                // populate files
                List<FileDetails> files = new FileDetailsDAO().GetFileDetailsForSearch(txtSearch.Value, cnxnString, logPath);

                if (files != null && files.Count > 0)
                {
                    AddFileHeader();
                    divSerchbtnfile.Style["display"] = "block";
                    int counter = 0;
                    foreach (FileDetails file in files)
                    {
                        AddFileRecordSearch(file, (counter % 2 == 0));
                        counter++;
                    }

                    divSearchText.InnerHtml = "<b>Search Results - " + txtSearch.Value.ToUpper() + "</b>. &nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:void();' title='remove search result' onclick=\"ClickReload('" + btnReload.ClientID + "');\"> <b>X</b></a>";
                    divSearchText.Style["display"] = "block";
                }
                else
                {
                    Files.InnerHtml = "There are no file found for '" + txtSearch.Value + "'. <a href='Dashboard.aspx'>Click here</a> to return back to previous screen.";
                }
            }

            txtSearch.Value = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public List<FolderDetails> SortingFolderList(List<FolderDetails> folders, string sortOption)
    {
        try
        {
            string sortExpression = sortOption;

            string direction = string.Empty;

            if (SortingDirection.SortDirection == SortDirection.Ascending)
            {
                SortingDirection.SortDirection = SortDirection.Descending;
                direction = "DESC";
            }
            else
            {
                SortingDirection.SortDirection = SortDirection.Ascending;
                direction = "ASC";
            }

            //List<FolderDetails> folders = new FolderDetailsDAO().GetAllChildFolders(-1);

            if (folders == null)
            {
                return folders;
            }

            switch (sortOption)
            {
                case "FolderName":
                    if (direction == "ASC")
                    {
                        Folder_SortByNameASC fAsc = new Folder_SortByNameASC();
                        folders.Sort(fAsc);
                    }
                    else
                    {
                        Folder_SortByNameDES fDES = new Folder_SortByNameDES();
                        folders.Sort(fDES);
                    }
                    break;
                case "CreateDate":
                    if (direction == "ASC")
                    {
                        Folder_SortByDateByAscendingOrder dAsc = new Folder_SortByDateByAscendingOrder();
                        folders.Sort(dAsc);
                    }
                    else
                    {
                        Folder_SortByDateByDescendingOrder dDes = new Folder_SortByDateByDescendingOrder();
                        folders.Sort(dDes);
                    }
                    break;
                case "Owner":
                    if (direction == "ASC")
                    {
                        Folder_SortByOwnerASC oAsc = new Folder_SortByOwnerASC();
                        folders.Sort(oAsc);
                    }
                    else
                    {
                        Folder_SortByOwnerDES oDes = new Folder_SortByOwnerDES();
                        folders.Sort(oDes);
                    }
                    break;
            }

            return folders;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public List<FileDetails> SortingFileList(List<FileDetails> file, string sortOption)
    {
        try
        {
            string sortExpression = sortOption;

            string direction = string.Empty;

            if (SortingDirection.SortDirection == SortDirection.Ascending)
            {
                SortingDirection.SortDirection = SortDirection.Descending;
                direction = "DESC";
            }
            else
            {
                SortingDirection.SortDirection = SortDirection.Ascending;
                direction = "ASC";
            }

            if (file == null)
            {
                return file;
            }

            switch (sortOption)
            {
                case "FileName":
                    if (direction == "ASC")
                    {
                        File_SortByNameASC fAsc = new File_SortByNameASC();
                        file.Sort(fAsc);
                    }
                    else
                    {
                        File_SortByNameDES fDES = new File_SortByNameDES();
                        file.Sort(fDES);
                    }
                    break;
                case "CreateDate":
                    if (direction == "ASC")
                    {
                        File_SortByDateByAscendingOrder dAsc = new File_SortByDateByAscendingOrder();
                        file.Sort(dAsc);
                    }
                    else
                    {
                        File_SortByDateByDescendingOrder dDes = new File_SortByDateByDescendingOrder();
                        file.Sort(dDes);
                    }
                    break;
                case "Owner":
                    if (direction == "ASC")
                    {
                        File_SortByOwnerASC oAsc = new File_SortByOwnerASC();
                        file.Sort(oAsc);
                    }
                    else
                    {
                        File_SortByOwnerDES oDes = new File_SortByOwnerDES();
                        file.Sort(oDes);
                    }
                    break;
            }

            return file;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSortFolderName_Click(object sender, EventArgs e)
    {
        try
        {
            List<FolderDetails> folders = this.SortingFolderList(new FolderDetailsDAO().GetAllChildFolders(-1, cnxnString, logPath), "FolderName");
            if (folders != null && folders.Count > 0)
            {
                AddFolderHeader();
                int counter = 0;
                foreach (FolderDetails folder in folders)
                {
                    AddFolderRecord(folder, (counter % 2 == 0));
                    counter++;
                }
            }
            else
            {
                Folders.InnerHtml = "There are no result found for '" + txtSearch.Value + "'.";
            }

            PopulateFiles();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSortFolderDate_Click(object sender, EventArgs e)
    {
        List<FolderDetails> folders = this.SortingFolderList(new FolderDetailsDAO().GetAllChildFolders(-1, cnxnString, logPath), "CreateDate");

        if (folders != null && folders.Count > 0)
        {
            AddFolderHeader();
            int counter = 0;
            foreach (FolderDetails folder in folders)
            {
                AddFolderRecord(folder, (counter % 2 == 0));
                counter++;
            }
        }
        else
        {
            Folders.InnerHtml = "There are no result found for '" + txtSearch.Value + "'.";
        }

        PopulateFiles();
    }

    protected void btnSortFolderOwner_Click(object sender, EventArgs e)
    {
        try
        {
            List<FolderDetails> folders = this.SortingFolderList(new FolderDetailsDAO().GetAllChildFolders(-1, cnxnString, logPath), "Owner");

            if (folders != null && folders.Count > 0)
            {
                AddFolderHeader();
                int counter = 0;
                foreach (FolderDetails folder in folders)
                {
                    AddFolderRecord(folder, (counter % 2 == 0));
                    counter++;
                }
            }
            else
            {
                Folders.InnerHtml = "There are no result found for '" + txtSearch.Value + "'.";
            }

            PopulateFiles();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSortFileName_Click(object sender, EventArgs e)
    {
        List<FileDetails> files = this.SortingFileList(new FileDetailsDAO().GetAllFiles(-1, cnxnString, logPath), "FileName");

        if (files != null && files.Count > 0)
        {
            AddFileHeader();
            int counter = 0;
            foreach (FileDetails file in files)
            {
                AddFileRecord(file, (counter % 2 == 0));
                counter++;
            }
        }
        else
        {

        }

        PopulateFolders();
    }

    protected void btnSortFileDate_Click(object sender, EventArgs e)
    {
        List<FileDetails> files = this.SortingFileList(new FileDetailsDAO().GetAllFiles(-1, cnxnString, logPath), "CreateDate");

        if (files != null && files.Count > 0)
        {
            AddFileHeader();
            int counter = 0;
            foreach (FileDetails file in files)
            {
                AddFileRecord(file, (counter % 2 == 0));
                counter++;
            }
        }
        else
        {

        }
        PopulateFolders();
    }

    protected void btnSortFileOwner_Click(object sender, EventArgs e)
    {
        List<FileDetails> files = this.SortingFileList(new FileDetailsDAO().GetAllFiles(-1, cnxnString, logPath), "Owner");

        if (files != null && files.Count > 0)
        {
            AddFileHeader();
            int counter = 0;
            foreach (FileDetails file in files)
            {
                AddFileRecord(file, (counter % 2 == 0));
                counter++;
            }
        }
        else
        {

        }
        PopulateFolders();
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        try
        {
            int folderId;

            if (int.TryParse(hfFolderId.Value, out folderId))
            {
                FolderDetailsDAO folderDAO = new FolderDetailsDAO();
                FolderDetails folder = folderDAO.GetSingleFileDetails(folderId, cnxnString, logPath);

                if (folder == null) return;

                //string path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], folder.FolderName);
                string path = string.Empty;
                //bool isUserNetworkPath = false;

                //if (ConfigurationManager.AppSettings["IsUseNetworkPath"] != null)
                //{
                //    isUserNetworkPath = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseNetworkPath"]);
                //}

                if (isUseNetworkPath)
                    path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], folder.FolderName);
                else
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), folder.FolderName);


                UtilityManager.DeleteDirectory(path);

                DeleteDirectory(folderId);


                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("Folder Deleted", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        PopulateFiles();
        PopulateFolders();
    }

    private void DeleteDirectory(int folderId)
    {
        try
        {
            FolderDetailsDAO folderDAO = new FolderDetailsDAO();
            FileDetailsDAO fileDAO = new FileDetailsDAO();
            List<FileDetails> files = fileDAO.GetAllFiles(folderId, cnxnString, logPath);

            if (files != null)
            {
                foreach (FileDetails file in files)
                {
                    fileDAO.RemoveFileDetails(file.Id, cnxnString, logPath);
                }
            }

            List<FolderDetails> folderList = folderDAO.GetAllChildFolders(folderId, cnxnString, logPath);

            if (folderList != null)
            {
                foreach (FolderDetails objFolder in folderList)
                {
                    DeleteDirectory(objFolder.Id);
                }
            }

            folderDAO.RemoveFileDetails(folderId, cnxnString, logPath);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDeleteFiles_Click(object s, EventArgs e)
    {
        try
        {
            int fileId;

            if (int.TryParse(hfFileId.Value, out fileId))
            {
                //parentId = fileId;
                FileDetailsDAO fileDAO = new FileDetailsDAO();
                FileDetails file = fileDAO.GetSingleFileDetails(fileId, cnxnString, logPath);
                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("File Deleted", file.FileName, fileId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                if (file == null) return;

                string path = string.Empty;

                if (isUseNetworkPath)
                    path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], file.FileName);
                else
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), file.FileName);


                try
                {
                    File.Delete(path);
                }
                catch
                { }

                fileDAO.RemoveFileDetails(fileId, cnxnString, logPath);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        PopulateFiles();
        PopulateFolders();
    }

    protected void btnRenameFile_Click(object s, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtFileRename.Text))
            {
                int fileId;
                if (int.TryParse(hfFileId.Value, out fileId))
                {
                    FileDetailsDAO fileDao = new FileDetailsDAO();
                    FileDetails file = fileDao.GetSingleFileDetails(fileId, cnxnString, logPath);
                    UserDetailsDAO userDetails = new UserDetailsDAO();
                    UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                    UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                    act.CreateUserActivityDetails("File Renamed", file.FileName, fileId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                    fileDao.UpdateFileName(fileId, txtFileRename.Text.Trim(), Session["UserName"].ToString(), UnescapeFormat.UnescapeXML(file.History), cnxnString, logPath);
                }
            }
            Response.Redirect("Dashboard.aspx");
        }
        catch (Exception ex)
        {
            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnRenameFolder_Click(object s, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtFolderRename.Text))
            {
                int folderId;
                if (int.TryParse(hfFolderId.Value, out folderId))
                {
                    UserDetailsDAO userDetails = new UserDetailsDAO();
                    UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                    FolderDetailsDAO folderDao = new FolderDetailsDAO();
                    FolderDetails folder = folderDao.GetSingleFileDetails(folderId, cnxnString, logPath);
                    UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                    act.CreateUserActivityDetails("Folder Renamed", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);
                    folderDao.UpdateFolderAlias(folderId, txtFolderRename.Text.Trim(), Session["UserName"].ToString(), UnescapeFormat.UnescapeXML(folder.History), cnxnString, logPath);


                }
            }
        }
        catch (Exception ex)
        { }
        PopulateFiles();
        PopulateFolders();
    }

    protected void bntCreateGroup_Click(object s, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtGroupName.Text))
            {
                PopulateFiles();
                PopulateFolders();
                PopulateGroupUsers();
                PopulateGroups();
                PopulateUsersList();

                return;
            }

            List<string> users = new List<string>();

            foreach (ListItem item in chkGroupUserList.Items)
            {
                if (item.Selected)
                {
                    users.Add(item.Value);
                }
            }

            if (users.Count <= 0)
            {
                return;
            }

            string groupUsers = StaffMemberManager.GetStaffMembersIdXML(users);

            GroupsDetailsDAO groupDAO = new GroupsDetailsDAO();

            groupDAO.AddGroups(Session["UserName"].ToString(), txtGroupName.Text, DateTime.Now, groupUsers, cnxnString, logPath);


            PopulateFiles();
            PopulateFolders();
            PopulateGroupUsers();
            PopulateGroups();
            PopulateUsersList();

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script> ShowListBox('" + rblChooseUsers.ClientID + "', 'Content_lbFolderUser'); </script>", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void PopulateGroupUsers()
    {
        try
        {
            UserDetailsDAO userDAO = new UserDetailsDAO();
            chkGroupUserList.DataSource = userDAO.GetUserList(cnxnString, logPath);
            chkGroupUserList.DataTextField = "UserName";
            chkGroupUserList.DataValueField = "Id";
            chkGroupUserList.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void PopulateGroups()
    {
        try
        {
            GroupsDetailsDAO groupDAO = new GroupsDetailsDAO();
            lbGroups.DataSource = groupDAO.GetGroupsByUser(Session["UserName"].ToString(), cnxnString, logPath);
            lbGroups.DataTextField = "GroupName";
            lbGroups.DataValueField = "Id";
            lbGroups.DataBind();

            lbFileGroups.DataSource = groupDAO.GetGroupsByUser(Session["UserName"].ToString(), cnxnString, logPath);
            lbFileGroups.DataTextField = "GroupName";
            lbFileGroups.DataValueField = "Id";
            lbFileGroups.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /*
    private bool IsAccessPermission(FolderDetails folder, out bool isOwnerUser)
    {
        XmlDocument doc = new XmlDocument();

        UserDetails userDetails = new UserDetailsDAO().GetUserDetailbyId(folder.OwnerUserId);
        isOwnerUser = false;

        if (userDetails == null)
        { }
        else
        {
            if (userDetails.UserId == Session["UserName"].ToString())
            {
                isOwnerUser = true;
                return true;
            }
        }

        if (!string.IsNullOrEmpty(folder.UserAccess) && !string.IsNullOrEmpty(folder.Permission))
        {
            doc.LoadXml(UnescapeFormat.UnescapeXML(folder.UserAccess));

            XmlNode root = doc.SelectSingleNode("Users");
            XmlNodeList users = root.SelectNodes("User");

            foreach (XmlNode node in users)
            {
                if (node.InnerText.ToLower() == Session["UserName"].ToString().ToLower())
                {
                    return true;
                }
            }
        }

        return false;
    }
    */

    private bool IsAccessPermission(int ownerUserId, string userAccess, string permission, out bool isOwnerUser)
    {
        try
        {
            XmlDocument doc = new XmlDocument();

            UserDetails userDetails = new UserDetailsDAO().GetUserDetailbyId(ownerUserId, cnxnString, logPath);
            isOwnerUser = false;


            if (userDetails == null)
            { }
            else
            {
                if (userDetails.UserName.ToLower() == Session["UserName"].ToString().ToLower())
                {
                    isOwnerUser = true;
                    return true;
                }

                if (!string.IsNullOrEmpty(userAccess) && !string.IsNullOrEmpty(permission))
                {
                    doc.LoadXml(UnescapeFormat.UnescapeXML(userAccess));

                    XmlNode root = doc.SelectSingleNode("Users");
                    XmlNodeList users = root.SelectNodes("User");

                    if (users.Count > 0)
                    {
                        foreach (XmlNode node1 in users)
                        {


                            if (node1.InnerText.ToLower() == Session["UserName"].ToString().ToLower())
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (root.InnerText.ToLower() == "all")
                        {
                            return true;

                        }
                        else if (root.InnerText.ToLower() == "none")
                        {
                            return false;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnCut_Click(object sender, EventArgs e)
    {
        try
        {
            int folderId;
            if (int.TryParse(hfFolderId.Value, out folderId))
            {
                Session["CutFolderID"] = folderId.ToString();
                string parameter = string.Format("parentId={0}&folerId={1}", 0, folderId);
                string outPut = string.Empty;

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFolderParent"] + parameter);
                //using (var response = (HttpWebResponse)request.GetResponse())
                //{
                //    using (var result = new StreamReader(response.GetResponseStream()))
                //    {
                //        outPut = result.ReadToEnd();
                //    }

                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);

                FolderDetailsDAO f = new FolderDetailsDAO();
                FolderDetails folder = f.GetSingleFileDetails(folderId, cnxnString, logPath);

                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("Folder Cut", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                //}

                divPasteFolder.Style["display"] = "block";
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            int folderId = 0;
            if (int.TryParse(hfFolderId.Value, out folderId))
            {
                Session["CopyFolderID"] = folderId.ToString();
                //  divPaste.Style["Color"] = "Black";
                divPasteFolder.Style["display"] = "block";
            }


            UserDetailsDAO userDetails = new UserDetailsDAO();
            UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
            UserActivityTrackerDAO act = new UserActivityTrackerDAO();
            FolderDetailsDAO f = new FolderDetailsDAO();
            FolderDetails folder = f.GetSingleFileDetails(folderId, cnxnString, logPath);
            act.CreateUserActivityDetails("Folder Copy", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

        }
        catch (Exception ex)
        {

            throw;
        }

        PopulateFiles();
        PopulateFolders();
    }

    protected void btnPaste_Click(object sender, EventArgs e)
    {
        try
        {
            int ParentfolderId = 0;

            if (int.TryParse(hfFolderId.Value, out ParentfolderId))
            {
                Session["PasteFolderID"] = ParentfolderId.ToString();
            }

            if (Session["CutFolderID"] != null)
            {
                int parentId = 0;
                int folderId = int.Parse(Session["CutFolderID"].ToString());
                FolderDetailsDAO folderDao = new FolderDetailsDAO();
                FolderDetails folder = folderDao.GetSingleFileDetails(folderId, cnxnString, logPath);
                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();

                act.CreateUserActivityDetails("Folder Paste", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);
                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                string parameter = string.Format("parentId={0}&folerId={1}&con={2}&log={3}", Session["PasteFolderID"].ToString(), folderId, Session["ConnectionString"].ToString(), Session["LogFilePath"].ToString());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFolderParent"] + parameter);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var result = new StreamReader(response.GetResponseStream()))
                    {
                        // outPut = result.ReadToEnd();
                    }


                }
                // Response.Redirect("UpdateFileParent.ashx?")
                Session["CutFolderID"] = null;
                divPasteFolder.Style["display"] = "none";
            }
            if (Session["CopyFolderID"] != null)
            {
                int parentId = 0;
                int folderId = int.Parse(Session["CopyFolderID"].ToString());
                FolderDetailsDAO folderDao = new FolderDetailsDAO();
                FolderDetails folder = folderDao.GetSingleFileDetails(folderId, cnxnString, logPath);
                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("Folder Paste", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);
                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                // string history = string.Empty;
                FolderHistoryManager fHM = new FolderHistoryManager();
                fHM.Date = DateTime.Now;
                fHM.Activity = "File Added";
                fHM.User = Session["UserName"].ToString();
                string history = FolderHistoryManager.CreateHistory(fHM);
                // AddFolderRecord(folder, true);
                folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, int.Parse(Session["PasteFolderID"].ToString()), history, folder.Tags, folder.Alias, folder.Permission, folder.FolderType, folder.Email, cnxnString, logPath);
                // folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias);
                Session["CopyFolderID"] = null;
                divPasteFolder.Style["display"] = "none";
            }
            if (Session["CutFileID"] != null)
            {
                int parentId = 0;
                int fileId = int.Parse(Session["CutFileID"].ToString());

                FileDetailsDAO fileDetails = new FileDetailsDAO();
                FileDetails file = fileDetails.GetSingleFileDetails(fileId, cnxnString, logPath);
                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                string parameter = string.Format("parentId={0}&folerId={1}", int.Parse(Session["PasteFolderID"].ToString()), fileId);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFileParent"] + parameter);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var result = new StreamReader(response.GetResponseStream()))
                    {
                        // outPut = result.ReadToEnd();
                    }
                }
                // Response.Redirect("UpdateFileParent.ashx?")
                Session["CutFileID"] = null;
                divPasteFolder.Style["display"] = "none";
            }
            if (Session["CopyFileID"] != null)
            {
                int parentId = 0;
                int FileId = int.Parse(Session["CopyFileID"].ToString());
                FileDetailsDAO fileDetails = new FileDetailsDAO();
                FileDetails file = fileDetails.GetSingleFileDetails(FileId, cnxnString, logPath);
                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                // string history = string.Empty;
                FolderHistoryManager fHM = new FolderHistoryManager();

                fHM.Date = DateTime.Now;
                fHM.Activity = "File Added";
                fHM.User = Session["UserName"].ToString();
                string history = FolderHistoryManager.CreateHistory(fHM);
                // AddFolderRecord(folder, true);



                fileDetails.CreateFileDetails(file.FileName, file.OriginalFileName, file.FileDescription, file.RelativePath, DateTime.Now, DateTime.Now, file.OwnerUserId, file.UserAccess, int.Parse(Session["PasteFolderID"].ToString()), history, file.Tags, file.Permission, file.DocumentType, cnxnString, logPath);
                //    folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias);

                Session["CopyFileID"] = null;
                divPasteFolder.Style["display"] = "none";
            }


        }
        catch (Exception ex)
        {

            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnPasteFile_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["CutFileID"] != null)
            {
                int parentId = 0;
                int fileId = int.Parse(Session["CutFileID"].ToString());

                FileDetailsDAO fileDetails = new FileDetailsDAO();
                FileDetails file = fileDetails.GetSingleFileDetails(fileId, cnxnString, logPath);

                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("File Paste", file.FileName, fileId, DateTime.Now, userDetail.UserName, cnxnString, logPath);


                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }

                fileDetails.UpdateParentFolderId(parentId, fileId, UnescapeFormat.UnescapeXML(file.History), cnxnString, logPath);
                /*string parameter = string.Format("parentId={0}&folerId={1}", parentId, fileId);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFileParent"] + parameter);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var result = new StreamReader(response.GetResponseStream()))
                    {
                        // outPut = result.ReadToEnd();
                    }
                }*/
                // Response.Redirect("UpdateFileParent.ashx?")
                Session["CutFileID"] = null;
                divPasteFile.Style["display"] = "none";
            }
            if (Session["CopyFileID"] != null)
            {
                int parentId = 0;
                int FileId = int.Parse(Session["CopyFileID"].ToString());
                FileDetailsDAO fileDetails = new FileDetailsDAO();
                FileDetails file = fileDetails.GetSingleFileDetails(FileId, cnxnString, logPath);
                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("File Paste", file.FileName, FileId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                // string history = string.Empty;
                FolderHistoryManager fHM = new FolderHistoryManager();

                fHM.Date = DateTime.Now;
                fHM.Activity = "File Added";
                fHM.User = Session["UserName"].ToString();
                string history = FolderHistoryManager.CreateHistory(fHM);
                // AddFolderRecord(folder, true);

                fileDetails.CreateFileDetails(file.FileName, file.OriginalFileName, file.FileDescription, file.RelativePath, DateTime.Now, DateTime.Now, file.OwnerUserId, file.UserAccess, parentId, history, file.Tags, file.Permission, file.DocumentType, cnxnString, logPath);
                //    folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias);

                Session["CopyFileID"] = null;
                divPasteFile.Style["display"] = "none";
            }

        }
        catch (Exception ex)
        {

            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnCancelShare_Click(object sender, EventArgs e)
    {
        ClearFields();
        PopulateFolders();
        PopulateFiles();
    }

    protected void btnPasteFolder_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["CutFolderID"] != null)
            {
                int parentId = 0;
                int folderId = int.Parse(Session["CutFolderID"].ToString());
                FolderDetailsDAO folderDao = new FolderDetailsDAO();
                FolderDetails folder = folderDao.GetSingleFileDetails(folderId, cnxnString, logPath);
                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("Folder Paste", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                string parameter = string.Format("parentId={0}&folerId={1}", parentId, folderId);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFolderParent"] + parameter);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var result = new StreamReader(response.GetResponseStream()))
                    {
                        // outPut = result.ReadToEnd();
                    }
                }
                // Response.Redirect("UpdateFileParent.ashx?")
                Session["CutFolderID"] = null;
                divPasteFolder.Style["display"] = "block";
            }
            if (Session["CopyFolderID"] != null)
            {
                int parentId = 0;
                int folderId = int.Parse(Session["CopyFolderID"].ToString());
                FolderDetailsDAO folderDao = new FolderDetailsDAO();
                FolderDetails folder = folderDao.GetSingleFileDetails(folderId, cnxnString, logPath);

                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("Folder Paste", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);

                if (Request.QueryString["ParentId"] != null)
                {
                    parentId = int.Parse(Request.QueryString["ParentId"]);
                }
                else
                {
                    parentId = -1;
                }
                // string history = string.Empty;
                FolderHistoryManager fHM = new FolderHistoryManager();
                fHM.Date = DateTime.Now;
                fHM.Activity = "File Added";
                fHM.User = Session["UserName"].ToString();
                string history = FolderHistoryManager.CreateHistory(fHM);
                // AddFolderRecord(folder, true);
                folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias, folder.Permission, folder.FolderType, folder.Email, cnxnString, logPath);
                // folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias);
                Session["CopyFolderID"] = null;
                divPasteFolder.Style["display"] = "none";
            }

        }
        catch (Exception ex)
        {

            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnCutFile_Click(object sender, EventArgs e)
    {
        try
        {
            int fileId;
            if (int.TryParse(hfFileId.Value, out  fileId))
            {
                Session["CutFileID"] = fileId.ToString();

                //string parameter = string.Format("parentId={0}&folerId={1}", 0, fileId);
                //string outPut = string.Empty;
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFileParent"] + parameter);
                //using (var response = (HttpWebResponse)request.GetResponse())
                //{
                //    using (var result = new StreamReader(response.GetResponseStream()))
                //    {
                //        outPut = result.ReadToEnd();
                //    }
                //}
                //Session["CutFileID"] = null;
                divPasteFile.Style["display"] = "block";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnCopyFile_Click(object sender, EventArgs e)
    {
        try
        {
            int fileId = 0;
            if (int.TryParse(hfFileId.Value, out  fileId))
            {
                Session["CopyFileID"] = fileId.ToString();
                divPasteFile.Style["display"] = "block";
            }
            PopulateFiles();
            PopulateFolders();
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    protected void btnAddFriends_Click(object s, EventArgs e)
    {
        try
        {
            List<string> users = new List<string>();

            foreach (ListItem item in chkUserFriendList.Items)
            {
                if (item.Selected)
                {
                    users.Add(item.Value);
                }
            }

            if (users.Count <= 0)
            {
                PopulateFiles();
                PopulateFolders();
                PopulateGroupUsers();
                PopulateGroups();
                PopulateUsersList();
                return;
            }

            string usersId = StaffMemberManager.GetStaffMembersIdXML(users);

            FriendsDetailsDAO friendsDAO = new FriendsDetailsDAO();
            FriendsDetails userFriends = friendsDAO.GetFriendsByUserId(Session["UserName"].ToString(), cnxnString, logPath);

            if (userFriends == null)
            {
                friendsDAO.AddFriends(Session["UserName"].ToString(), DateTime.Now, usersId, cnxnString, logPath);
            }
            else
            {
                friendsDAO.UpdateUsersByUserId(userFriends.Id, usersId, cnxnString, logPath);
            }

            PopulateFiles();
            PopulateFolders();
            PopulateGroupUsers();
            PopulateGroups();
            PopulateUsersList();

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script> ShowListBox('" + rblChooseUsers.ClientID + "', 'Content_lbFolderUser'); </script>", false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PopulateFriends()
    {
        try
        {
            FriendsDetailsDAO friendDAO = new FriendsDetailsDAO();

            FriendsDetails friends = friendDAO.GetFriendsByUserId(Session["UserName"].ToString(), cnxnString, logPath);

            chkUserFriendList.DataSource = new UserDetailsDAO().GetUserList(cnxnString, logPath);
            chkUserFriendList.DataTextField = "UserName";
            chkUserFriendList.DataValueField = "Id";
            chkUserFriendList.DataBind();

            if (friends != null && !string.IsNullOrEmpty(friends.Users))
            {
                List<string> users = StaffMemberManager.GetStaffIdList(friends.Users);

                foreach (ListItem item in chkUserFriendList.Items)
                {
                    foreach (string str in users)
                    {
                        if (str == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShareFolder_Click(object sender, EventArgs e)
    {
        try
        {
            int folderId;
            if (int.TryParse(hfFolderId.Value, out folderId))
            {
                FolderDetailsDAO folderDao = new FolderDetailsDAO();
                FolderDetails folder = folderDao.GetSingleFileDetails(folderId, cnxnString, logPath);
                string[] users = txtShareWith.Text.Split(new char[] { ',' });
                string userName = Session["UserName"].ToString();

                // string path = ConfigurationManager.AppSettings["BASE_URL"] + "/Directories/" + folder.FolderName;
                string path = string.Empty;
                if (isUseNetworkPath)
                    path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], folder.RelativePath);
                else
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), folder.RelativePath);

                string address = "<a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + folder.FolderName + "&filePath=" + path + "'> <img src='" + BASE_URL + "/static/images/file.png' width='25px' /> Click Here</a>";
                string Message = userName + " have shared this file with you. Please " + address + " here to download the file.";
                foreach (string to in users)
                {
                    EmailManager.FileSharing_EmailToUser(to, Message, logPath);
                }
                txtMessage.Text = string.Empty;
                txtShareWith.Text = string.Empty;

                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();

                act.CreateUserActivityDetails("Folder Shared", folder.FolderName, folderId, DateTime.Now, userDetail.UserName, cnxnString, logPath);
            }

        }
        catch (Exception ex)
        {

            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }

    protected void btnShareFile_Click(object sender, EventArgs e)
    {
        try
        {
            int fileId;
            if (int.TryParse(hfFileId.Value, out fileId))
            {
                FileDetailsDAO fileDao = new FileDetailsDAO();
                FileDetails file = fileDao.GetSingleFileDetails(fileId, cnxnString, logPath);
                string[] users = txtShareFileWith.Text.Split(new char[] { ',' });
                string userName = Session["UserName"].ToString();
                string path = string.Empty;
                // string path = ConfigurationManager.AppSettings["BASE_URL"] + "/Directories/" + file.FileName;
                if (isUseNetworkPath)
                    path = Path.Combine(ConfigurationManager.AppSettings["DMS_PATH"], file.RelativePath);
                else
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["USER_FOLDER_BASE_PATH"]), file.RelativePath);

                string address = "<a style='vertical-align:top;' href='" + BASE_URL + "/api/FileDownload.ashx?fileName=" + file.OriginalFileName + "&filePath=" + path + "'> <img src='" + BASE_URL + "/static/images/file.png' width='25px' /> Click Here</a>";
                // string path = "Google.com";
                string Message = userName + " have shared this file with you. Please " + address + " here to download the file.";
                foreach (string to in users)
                {
                    EmailManager.FileSharing_EmailToUser(to, Message, logPath);
                }
                txtShareFileWith.Text = string.Empty;
                txtShareMessFile.Text = string.Empty;

                UserDetailsDAO userDetails = new UserDetailsDAO();
                UserDetails userDetail = userDetails.GetUserDetailList(Session["UserName"].ToString(), cnxnString, logPath);
                UserActivityTrackerDAO act = new UserActivityTrackerDAO();
                act.CreateUserActivityDetails("File Shared", file.FileName, fileId, DateTime.Now, userDetail.UserName, cnxnString, logPath);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        PopulateFiles();
        PopulateFolders();
    }
    protected void btndownloadfile_Click(object s, EventArgs e)
    {
        try
        {
            string files = hfdownloadfileid.Value;
            string[] filearray = files.Split(',');

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btndownloadfolders_Click(object s, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    protected void btnEditFolder_Click(object s, EventArgs e)
    {
        try
        {
            int folderId;
            if (int.TryParse(hfFolderId.Value, out folderId))
            {
                FolderDetails folder = new FolderDetailsDAO().GetSingleFileDetails(folderId, cnxnString, logPath);
                if (folder != null)
                {
                    txtFolderName.Text = folder.Alias;
                    txtDescription.Text = folder.FolderDescription;
                    txtFolderTags.Text = folder.Tags;

                    //ddAccessType.SelectedItem.Text = folder.Permission;
                    /*
                         foreach (ListItem ddItems in ddAccessType.Items)
                         {
                             ddItems.Selected = false;
                         }

                         ddAccessType.Items.FindByText(folder.Permission).Selected = true;

                         XmlDocument doc = new XmlDocument();
                         doc.LoadXml(ParameterFormater.UnescapeXML(folder.UserAccess));

                         XmlNode root = doc.SelectSingleNode("Users");
                         if (root != null)
                         {
                             rblChooseUsers.SelectedValue = root.Attributes["usertype"].Value;

                             XmlNodeList userList = root.SelectNodes("User");

                             if (root.Attributes["usertype"].Value == "1")
                             {
                                 foreach (ListItem item in lbGroups.Items)
                                 {

                                 }
                             }
                             else if (root.Attributes["usertype"].Value == "2")
                             {
                                 foreach (ListItem item in lbFolderUser.Items)
                                 {
                                     foreach (XmlNode node in userList)
                                     {
                                         if (node.InnerText == item.Text)
                                         {
                                             item.Selected = true;
                                         }
                                     }
                                 }
                             }
                         }
                     * */
                    btnAdd.Text = "Update";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "<script>$('#FolderPopup').fadeIn(1000);</script>", false);
                }
            }

            PopulateFiles();
            PopulateFolders();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // code by vasim
    protected void btnReload_Click(object s, EventArgs e)
    {
        try
        {
            divSearchText.InnerHtml = string.Empty;
            divSearchText.Style["display"] = "none";
            txtSearch.Value = string.Empty;

            PopulateFiles();
            PopulateFolders();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void btnPasteFile_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["CutFileID"] != null)
    //        {
    //            int parentId = 0;
    //            int fileId = int.Parse(Session["CutFileID"].ToString());

    //            FileDetailsDAO fileDetails = new FileDetailsDAO();
    //            FileDetails file = fileDetails.GetSingleFileDetails(fileId);
    //            if (Request.QueryString["ParentId"] != null)
    //            {
    //                parentId = int.Parse(Request.QueryString["ParentId"]);
    //            }
    //            else
    //            {
    //                parentId = -1;
    //            }
    //            string parameter = string.Format("parentId={0}&folerId={1}", parentId, fileId);

    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFileParent"] + parameter);
    //            using (var response = (HttpWebResponse)request.GetResponse())
    //            {
    //                using (var result = new StreamReader(response.GetResponseStream()))
    //                {
    //                    // outPut = result.ReadToEnd();
    //                }
    //            }
    //            // Response.Redirect("UpdateFileParent.ashx?")
    //            Session["CutFileID"] = null;
    //            divPasteFile.Style["display"] = "none";

    //        }
    //        if (Session["CopyFileID"] != null)
    //        {
    //            int parentId = 0;
    //            int FileId = int.Parse(Session["CopyFileID"].ToString());
    //            FileDetailsDAO fileDetails = new FileDetailsDAO();
    //            FileDetails file = fileDetails.GetSingleFileDetails(FileId);
    //            if (Request.QueryString["ParentId"] != null)
    //            {
    //                parentId = int.Parse(Request.QueryString["ParentId"]);
    //            }
    //            else
    //            {
    //                parentId = -1;
    //            }
    //            // string history = string.Empty;
    //            FolderHistoryManager fHM = new FolderHistoryManager();

    //            fHM.Date = DateTime.Now;
    //            fHM.Activity = "File Added";
    //            fHM.User = Session["UserName"].ToString();
    //            string history = FolderHistoryManager.CreateHistory(fHM);
    //            // AddFolderRecord(folder, true);

    //            // fileDetails.CreateFileDetails(file.FileName, file.OriginalFileName, file.FileDescription, file.RelativePath, DateTime.Now, DateTime.Now, file.OwnerUserId, file.UserAccess, parentId, history, file.Tags);
    //            //    folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias);

    //            fileDetails.CreateFileDetails(file.FileName, file.OriginalFileName, file.FileDescription, file.RelativePath, DateTime.Now, DateTime.Now, file.OwnerUserId, file.UserAccess, parentId, history, file.Tags, file.Permission);
    //            //    folderDao.CreateFolderDetails(folder.FolderName, folder.FolderDescription, folder.RelativePath, DateTime.Now, DateTime.Now, folder.OwnerUserId, folder.UserAccess, parentId, history, folder.Tags, folder.Alias);

    //            Session["CopyFileID"] = null;
    //            divPasteFile.Style["display"] = "none";
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //    PopulateFiles();
    //    PopulateFolders();
    //}
    #region CommentedCode


    //protected void btnCut_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int folderId;
    //        if (int.TryParse(hfFolderId.Value, out folderId))
    //        {
    //            Session["CutFolderID"] = folderId.ToString();
    //            string parameter = string.Format("parentId={0}&folerId={1}", 0, folderId);
    //            string outPut = string.Empty;
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Handler_UpdateFolderParent"] + parameter);
    //            using (var response = (HttpWebResponse)request.GetResponse())
    //            {
    //                using (var result = new StreamReader(response.GetResponseStream()))
    //                {
    //                    outPut = result.ReadToEnd();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //    PopulateFiles();
    //    PopulateFolders();
    //}

    //protected void btnShare_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }

    //}
    #endregion

}