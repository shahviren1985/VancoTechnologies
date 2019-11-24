<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="InnerFolder.aspx.cs" Inherits="DocumentManagement_InnerFolder" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: Manage Folders</title>
    <link href="<%=BASE_URL%>/static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="<%=BASE_URL%>/static/stylesheets/StyleSheets.css" rel="stylesheet"
        type="text/css" />
    <link rel="Stylesheet" href="<%=BASE_URL%>/static/StyleSheets/Form.css" />
    <script src="<%=BASE_URL%>/static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="<%=BASE_URL%>/static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetSelectedFilesValue() {

            var count = <%=selectedFilesCount%>;

            var docType = $("#<%=hdnDocType.ClientID%>");
            var FileDesc = $("#<%=hdnFileDesc.ClientID%>");
            var FileAccess= $("#<%=hdnFileAccess.ClientID%>");
            var FileTags = $("#<%=hdnFileTags.ClientID%>");
            var initials = "ContentPlaceHolder1_";
            
            docType.value="";
            FileDesc.value="";
            FileAccess.value="";
            FileTags.value="";

            for (var i = 1; i <= count; i++) {
                //docType = document.getElementById('Content_hdnDocType');
                //FileDesc = document.getElementById('Content_hdnFileDesc');
                //FileAccess = document.getElementById('Content_hdnFileAccess');
                //FileTags = document.getElementById('Content_hdnFileTags');
                
                docType.value += $('#<%=FilesRowContainer.ClientID%>').find('#'+initials+'RowddTypeValue' + i + " option:selected").text() + "|";
                
                FileDesc.value += $('#<%=FilesRowContainer.ClientID%>').find('#'+initials+'RowtxtFileDescriptionValue' + i).val() + "|";               

                FileAccess.value += $('#<%=FilesRowContainer.ClientID%>').find('#'+initials+'RowddAccessValue' + i + " option:selected").text() + "|";                

                FileTags.value += $('#<%=FilesRowContainer.ClientID%>').find('#'+initials+'RowtxtTageValue' + i).val() + "|";

            }    

            $('#<%=hdnDocType.ClientID%>').val(docType.value);
            $('#<%=hdnFileDesc.ClientID%>').val(FileDesc.value);
            $('#<%=hdnFileAccess.ClientID%>').val(FileAccess.value);
            $('#<%=hdnFileTags.ClientID%>').val(FileTags.value);
            return true;
            
        }
        function ShowListBox(radio, ls, divFriend, divGroup) {

            var radio = document.getElementById(radio);

            var inputArr = radio.getElementsByTagName('input');

            var labelArr = radio.getElementsByTagName('label');

            var lss = document.getElementById(ls);

            for (var i = 0; i < inputArr.length; i++) {

                if (inputArr[i].checked) {

                    if (inputArr[i].value == "1") {
                        //document.getElementById('divFriends').style.display = 'none'
                        //document.getElementById('divGroups').style.display = 'block'
                        document.getElementById(divFriend).style.display = 'none'
                        document.getElementById(divGroup).style.display = 'block'
                    }
                    else if (inputArr[i].value == "2") {
                        //document.getElementById('divFriends').style.display = 'block'
                        //document.getElementById('divGroups').style.display = 'none'
                        document.getElementById(divFriend).style.display = 'block'
                        document.getElementById(divGroup).style.display = 'none'
                    }
                    else {
                        //document.getElementById('divFriends').style.display = 'none'
                        //document.getElementById('divGroups').style.display = 'none'
                        document.getElementById(divFriend).style.display = 'none'
                        document.getElementById(divGroup).style.display = 'none'
                    }
                    /*if (inputArr[i].value == "2") {
                    document.getElementById(ls).style.display = 'block';
                    }
                    else {
                    document.getElementById(ls).style.display = "none";
                    }*/
                }
            }
        }
        /* Function for validate controls data in Add file and folder popup */

        function Validation(radio, friendList, groupList, folderName, file, desc, type, label, button) {

            if (!ValidateDetails(folderName, file, desc, type)) {
                return false;
            }

            var radio = document.getElementById(radio);

            var inputArr = radio.getElementsByTagName('input');

            var labelArr = radio.getElementsByTagName('label');

            var lss = document.getElementById(friendList);

            for (var i = 0; i < inputArr.length; i++) {

                if (inputArr[i].checked) {

                    if (inputArr[i].value == "1") {

                        elem = document.getElementById(groupList);

                        for (var j = 0; j < elem.length; j++) {

                            if (elem.options[j].selected) {
                                HideButton(label, button);
                                return true;
                            }
                        }
                    }
                    else if (inputArr[i].value == "2") {

                        elem1 = document.getElementById(friendList);

                        for (var k = 0; k < elem1.length; k++) {
                            if (elem1.options[k].selected) {
                                HideButton(label, button);
                                return true;
                            }
                        }
                    }
                    else {
                        HideButton(label, button);
                        return true;
                    }
                }
            }

            alert("Please select atleast one Friend/Group.");
            return false
        }


        function ValidateDetails(txtName, file, txtDesc, type) {

            var desc = document.getElementById(txtDesc);

            if (type == "folder") {

                var name = document.getElementById(txtName);
                if (name.value == "") {
                    alert("Please Enter Folder Name");
                    name.focus();
                    return false;
                }
            }
            else if (type == "file") {
                var id_value = document.getElementById(file).value;
                if (id_value == '') {
                    alert("Please upload file");
                    return false;
                }
            }

            if (desc.value == "") {
                alert("Please Enter Desciption");
                desc.focus();
                return false;
            }

            return true;
        }

        function HideButton(label, button) {
            document.getElementById(button).style.display = 'none';
            document.getElementById(label).style.display = 'block';
        }

        function ShowButton1(label, button) {
            document.getElementById(button).style.display = 'block';
            document.getElementById(label).style.display = 'none';
        }


        /*
        function Validate(radio, ls) {

        var radio = document.getElementById(radio);

        var inputArr = radio.getElementsByTagName('input');

        var labelArr = radio.getElementsByTagName('label');

        var lss = document.getElementById(ls);

        for (var i = 0; i < inputArr.length; i++) {

        if (inputArr[i].checked) {

        if (inputArr[i].value == "2") {

        elem = document.getElementById(ls);

        for (var i = 0; i < elem.length; i++) {

        if (elem.options[i].selected) {

        return true;
        }
        }
        }
        else {
        return true;
        }
        }
        }

        alert("Please select atleast one user.");
        return false
        }
        */
        function SortButtonClick(button) {
            var btn = document.getElementById(button);
            if (btn != null) { //If we find the button click it
                btn.click();
                //alert("");
                event.keyCode = 0
            }
        }

        function doClick(buttonName, e) {
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox


            if (key == 13) {
                //Get the button the user wants to have clicked
                var btn = document.getElementById(buttonName);
                //alert(btn);
                if (btn != null) { //If we find the button click it
                    btn.click();
                    //alert("");
                    event.keyCode = 0
                }
            }
        }

        function divClick(button) {
            var btn = document.getElementById(button);
            //alert(btn);
            if (btn != null) { //If we find the button click it
                btn.click();
                //alert("");
                event.keyCode = 0
            }
        }

        function DeleteFolder(folderid, foldername, button) {
            var val = confirm("Are you sure want to delete " + foldername + " ?");
            if (val) {
                $('#DivDeleting').fadeIn(100);
                $('#<%=hfFolderId.ClientID%>').val(folderid);
                var btn = document.getElementById(button);
                if (btn != null) {
                    btn.click();
                    event.keyCode = 0
                }
            }
        }

        function DeleteFile(fileid, fileName, button) {
            var val = confirm("Are you sure want to delete " + fileName + " ?");
            if (val) {
                $('#DivDeleting').fadeIn(100);
                $('#<%=hfFileId.ClientID%>').val(fileid);
                var btn = document.getElementById(button);
                if (btn != null) {
                    btn.click();
                    event.keyCode = 0
                }
            }
        }
        function CutFile(fileId, button) {
            $('#<%=hfFileId.ClientID%>').val(fileId);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }

        function CopyFile(fileId, button) {
            $('#<%=hfFileId.ClientID%>').val(fileId);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }

        //        function PasteFile(fileId, button) {
        //            document.getElementById('Content_hfFileId').value = fileId;
        //            var btn = document.getElementById(button);
        //            if (btn != null) {
        //                btn.click();
        //                event.keyCode = 0
        //            }
        //        }

        function PasteFile(button) {
            //document.getElementById('Content_hfFileId').value = fileId;
            $("#<%=btnPasteFile.ClientID%>").click();
            
            /*var btn = document.getElementById(button);

            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }*/
        }

        function PasteFolderHere(button) {
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }
        function CopyFolder(folderid, button) {
            $('#<%=hfFolderId.ClientID%>').val(folderid);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }

        function PasteFolder(folderid, button) {
            $('#<%=hfFolderId.ClientID%>').val(folderid);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }

        function FolderShare(folderId, folderName) {
            $('#<%=hfFolderId.ClientID%>').val(folderid);
            $('#<%=txtFolderRename.ClientID%>').val(folderName);
            $('#FolderShare').fadeIn(1000);
        }

        function FileShare(fileId, fileName, user) {
            $('#<%=hfFileId.ClientID%>').val(fileId);
            $('#<%=txtShareMessFile.ClientID%>').val(user + " have shared this file " + fileName + " with you.");

            $('#FileShare').fadeIn(1000);
        }

        function CutFolder(folderid, button) {
            $('#<%=hfFolderId.ClientID%>').val(folderid);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }

        function RenameFile(fileId, fileName) {
            $('#<%=hfFileId.ClientID%>').val(fileId);
            $('#<%=txtFileRename.ClientID%>').val(fileName);
            $('#FileRename').fadeIn(1000);
        }

        function RenameFolder(folderId, folderName) {
            $('#<%=hfFolderId.ClientID%>').val(folderid);
            $('#<%=txtFolderRename.ClientID%>').val(folderName);
            $('#FolderRename').fadeIn(1000);
        }

        function Validate(textbox) {
            var txtbox = document.getElementById(textbox);
            if (txtbox.value == "") {
                alert("Please enter name.");
                txtbox.focus();
                return false;
            }
            return true;
        }

        function ClickEdit(folderId, button) {
            $('#<%=hfFolderId.ClientID%>').val(folderid);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }

        function ClickReload(button) {
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
                event.keyCode = 0
            }
        }
        function downloadzipFolder(button) {
            var folders = "";
            $('.foldercheckbox').each(function () {
                if ($(this).attr("checked") == "checked") {
                    folders += $(this).attr("id") + ",";
                }

            });
            if (folders == "") {

                alert("Please select the folder to download");
            }
            else {
                alert("folders =" + folders);

                window.location = BASE_PATH + "/api/DownloadmultipleZip.ashx?Id=" + folders;
                alert("folders =" + folders);

            }
        }
        function downloadzipFile(button) {
            var file = "";
            $('.filecheckbox').each(function () {
                if ($(this).attr("checked") == "checked") {
                    file += $(this).attr("id") + ",";
                }

            });
            if (file == "") {

                alert("Please select the folder to download");
            }
            else {
                alert("file =" + file);
                //                $('#hfdownloadfileid').value(file);
                window.location = BASE_PATH + "/api/DownloadmultipleZipFile.ashx?Id=" + file;
                alert("folders =" + file);

            }
        }
        function checkAll(chkbox, chkclass) {
            var chk = document.getElementById(chkbox);
            if (chk.checked) {
                $(chkclass).each(function () {
                    $(this).attr('checked', 'checked');

                });

            }
            else {
                $(chkclass).each(function () {
                    $(this).removeAttr('checked');

                });
            }
        }
    </script>
    <style type="text/css">
        .subHeader {
            height: 4%;
            float: left;
            width: 90%;
            border-bottom: 1px solid #999;
            text-align: left;
            background-color: #FFF;
            font-size: 18px;
            color: #000000;
            text-align: left;
        }

        .PlusIcon {
            border: 2px solid #333;
            background-color: White;
            color: #333;
            font-size: 16px;
            cursor: pointer;
            width: 20px;
            height: 20px;
            text-align: center;
            font-weight: bolder;
            border-radius: 40px;
        }

            .PlusIcon:hover {
                background-color: #333;
                color: White;
            }

        .FolderPlus {
            float: left;
            top: 23%;
        }

        .FilePlus {
            float: left;
            margin-top: 2%;
        }

        #FolderPopup {
            display: none;
        }

        #FilePopup {
            display: none;
        }

        #FolderRename {
            display: none;
        }

        #FileRename {
            display: none;
        }

        #AddGroups, #AddFriends, #FolderShare, #FileShare, #DivDeleting {
            display: none;
        }

        #Content_Folders div {
            float: left;
            width: 134px;
        }

        #Content_Files div {
            float: left;
            width: 134px;
        }

        .FolderHeader {
            float: left;
            width: 100% !important;
        }

            .FolderHeader div {
                float: left;
                margin-right: 15px;
                width: 150px;
            }

        .SubmitButton {
            background-color: #2E87C8;
            color: #FFF !important;
            border-radius: 4px;
            padding: 5px;
            min-width: 20px !important;
            min-height: 15px;
            text-align: center;
            cursor: pointer;
        }

        .DivSortButton {
            color: Black !important;
            /*min-width: 20px !important;*/
            min-height: 15px;
            cursor: pointer;
        }

        input[type="text"] {
            color: #CCC;
            height: 20px !important;
        }

            input[type="text"]:focus {
                color: Black;
            }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div runat="server" id="ApplicationTitle">
    </div>--%>
    <div style="float: left; margin-left: 7%; margin-top: 30px; width: 86%">
        <input type="text" runat="server" id="txtSearch" style="border: 1px solid #CCC; border-radius: 4px; float: left; margin-right: 10px; width: 200px;"
            placeholder="Search Documents..." />
        <div id="divClick" runat="server" class="SubmitButton" style="float: left; width: 16px; font-size: 12px; background-color: #2E87C8; color: #FFF !important;">
            Go
        </div>
    </div>
    <div runat="server" id="Breadcrumbs" style="position: absolute; margin-left: 7%; margin-top: 75px;">
    </div>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:LinkButton runat="server" Style="display: none;" Font-Size="12px" ID="lnkSearch"
                OnClick="lnkSearch_Click" Text="Go" CssClass="SubmitButton"></asp:LinkButton>
            <asp:Button ID="btnSortFolderName" runat="server" Text="Button" Style="display: none;"
                OnClick="btnSortFolderName_Click" />
            <asp:Button ID="btnSortFolderDate" runat="server" Text="Button" Style="display: none;"
                OnClick="btnSortFolderDate_Click" />
            <asp:Button ID="btnSortFolderOwner" runat="server" Text="Button" Style="display: none;"
                OnClick="btnSortFolderOwner_Click" />
            <asp:Button ID="btnSortFileName" runat="server" Text="Button" Style="display: none;"
                OnClick="btnSortFileName_Click" />
            <asp:Button ID="btnSortFileDate" runat="server" Text="Button" Style="display: none;"
                OnClick="btnSortFileDate_Click" />
            <asp:Button ID="btnSortFileOwner" runat="server" Text="Button" Style="display: none;"
                OnClick="btnSortFileOwner_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Button" Style="display: none;" OnClick="btnDelete_Click" />
            <asp:Button ID="btnDeleteFiles" runat="server" Text="Button" Style="display: none;"
                OnClick="btnDeleteFiles_Click" />
            <asp:Button ID="btnCut" runat="server" Text="Button" Style="display: none;" OnClick="btnCut_Click" />
            <asp:Button ID="btnPaste" Text="Button" Style="display: none;" runat="server" OnClick="btnPaste_Click" />
            <asp:Button ID="btnPasteFolder" Text="Button" runat="server" OnClick="btnPasteFolder_Click"
                Style="display: none;" />
            <asp:Button ID="btnCopy" Text="Button" runat="server" Style="display: none;" OnClick="btnCopy_Click" />
            <asp:Button ID="btnCutFile" Text="Button" runat="server" OnClick="btnCutFile_Click"
                Style="display: none;" />
            <asp:Button ID="btnCopyFile" Text="Button" runat="server" OnClick="btnCopyFile_Click"
                Style="display: none;" />
            <asp:Button ID="btnPasteFile" Text="Button" runat="server" OnClick="btnPasteFile_Click"
                Style="display: none;" />
            <asp:Button ID="btnEditFolder" Text="Button" runat="server" OnClick="btnEditFolder_Click"
                Style="display: none;" />
            <asp:Button ID="btnReload" runat="server" OnClick="btnReload_Click" Style="display: none;" />
            <asp:HiddenField ID="hfFolderId" runat="server" />
            <asp:HiddenField ID="hfFileId" runat="server" />
            <div style="float: left; margin-left: 7%;">
                <div id="divSearchText" runat="server" style="padding: 5px 0px 5px 0px; width: 100%; display: none">
                </div>
                <div class="subHeader" style="margin-bottom: 20px; float: left;">
                    Folders
                    <%--<input type="checkbox" id="chkboxfolder" onclick="checkAll('chkboxfolder','.foldercheckbox') " />--%>
                    <div runat="server" id="divsSearchBtnFolder" title="Download Folder Here" onclick="downloadzipFolder('<%=btndownloadfolders.ClientID%>')"
                        style="float: right; font-size: 12pt; cursor: pointer; display: none; margin-right: 15px">
                        Download
                    </div>
                    <div runat="server" id="divPasteFolder" title="Paste Folder Here" onclick="PasteFolderHere('btnPasteFolder')"
                        style="float: right; font-size: 12pt; cursor: pointer; display: none; margin-right: 15px">
                        Paste
                    </div>
                </div>
                <div class="PlusIcon FolderPlus" title="Add New Folder" onclick="$('#FolderPopup').fadeIn(10);">
                    +
                </div>
                <div id="Folders" runat="server" style="float: left; width: 90%; height: 30%; max-height: 30%; min-height: 30%; overflow: auto">
                    You haven't added any folders yet. Add new folders by clicking on '+' sign.
                </div>
                <div class="subHeader" style="margin-top: 20px; margin-bottom: 20px; float: left;">
                    Files
                    <%--<input type="checkbox" id="chkboxFiles" onclick="checkAll('chkboxFiles','.filecheckbox') " />--%>
                    <div runat="server" id="divSerchbtnfile" title="Download File Here" onclick="downloadzipFile('<%=btndownloadfile.ClientID%>')"
                        style="float: right; font-size: 12pt; cursor: pointer; display: none; margin-right: 15px">
                        Download
                    </div>
                    <div runat="server" id="divPasteFile" title="Paste File Here" onclick="PasteFile('btnPasteFile')"
                        style="float: right; font-size: 12pt; cursor: pointer; display: none; margin-right: 15px">
                        Paste
                    </div>
                </div>
                <div class="PlusIcon FilePlus" title="Add New File" onclick="$('#FilePopup').fadeIn(10);">
                    +
                </div>
                <div id="Files" runat="server" style="float: left; width: 90%; height: 30%; max-height: 30%; min-height: 30%; overflow: auto;">
                    You haven't added any files yet. Add new files by clicking on '+' sign.
                </div>
            </div>
            <asp:HiddenField ID="hfFolderHistory" runat="server" />
            <asp:HiddenField ID="hfFileHistory" runat="server" />
            <div id="FolderPopup">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <%--<asp:ImageButton ID="btnClose" ToolTip="Close" ImageUrl="../static/images/closeIcon1.png"
                AlternateText="Close" OnClientClick="$('#FolderPopup').fadeOut(500);" Style="width: 30px;
                float: right;" runat="server"></asp:ImageButton>--%>
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#FolderPopup').fadeOut(500);"
                            title="Close Create Folder" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Create new folder
                        </div>
                        <div id="divresult" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="divAddPopup" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>Folder Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFolderName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Text="*" ID="rfvFolderName" runat="server" Display="Dynamic"
                                            ValidationGroup="Validate" Style="color: Red;" ControlToValidate="txtFolderName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Folder Description
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="rfvDescription" runat="server" Display="Dynamic" ValidationGroup="Validate"
                                            Style="color: Red;" Text="*" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr runat="server" id="trAccessRightFolder">
                                    <td>Access
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddAccessType" runat="server">
                                            <asp:ListItem Text="Read" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Share" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Rename" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Update" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Delete" Value="4"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Private" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--This row is for Folder Access
                                <tr>
                                    <td>
                                        Choose users
                                    </td>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rblChooseUsers" onClick="ShowListBox(this.id, 'Content_lbFolderUser','divFriends','divGroups');">
                                            <asp:ListItem Text="All Users" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Groups" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Friends" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="No Access" Value="3" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:TextBox runat="server" ID="txtUsers" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                --%>
                                <tr>
                                    <td></td>
                                    <td valign="middle">
                                        <div id="divFriends" style="display: none; width: 100%">
                                            <div style="float: left">
                                                <asp:ListBox ID="lbFolderUser" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                            <div style="float: left; vertical-align: middle; margin-left: 5px;">
                                                <div class="SubmitButton" onclick="$('#AddFriends').fadeIn(1000);">
                                                    Add Friends
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divGroups" style="display: none; width: 100%">
                                            <div style="float: left">
                                                <asp:ListBox ID="lbGroups" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                            <div style="float: left; vertical-align: middle; margin-left: 5px;">
                                                <div class="SubmitButton" onclick="$('#AddGroups').fadeIn(500);">
                                                    Add Groups
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <%--<td>
                                        <asp:ListBox ID="lbFolderUser" runat="server" SelectionMode="Multiple" Style="display: none;">
                                        </asp:ListBox>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>Tags
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFolderTags" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <br />
                                        (Comma separated tags)
                                    </td>
                                </tr>
                            </table>
                            <div id="divButtons">
                                <asp:Button ID="btnAdd" CssClass="SubmitButton" runat="server" Text="Add" ValidationGroup="Validate"
                                    OnClick="btnCreate_Click" OnClientClick="return Validation('<%=rblChooseUsers.ClientID%>', '<%=lbFolderUser.ClientID%>','<%=lbGroups.ClientID%>','<%=txtFolderName.ClientID%>','<%=fuFile.ClientID%>','<%=txtDescription.ClientID%>','folder','divProgress','divButtons');" />&nbsp;
                                <asp:Button ID="btnCancel" CssClass="SubmitButton" OnClientClick="$('#FolderPopup').fadeOut(500);"
                                    runat="server" Text="Cancel" OnClick="btnCancel_Click" ToolTip="Close Create Folder" />
                            </div>
                            <div id="divProgress" style="display: none;">
                                <asp:Label ID="lable1" runat="server" Text="Creating... Please wait" CssClass="SubmitButton"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="FilePopup">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#FilePopup').fadeOut(500);"
                            title="Close Upload File" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Upload file
                        </div>
                        <div id="divFileResult" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="div3" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>File
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                        <%--<asp:FileUpload ID="fuFile" runat="server" />--%>
                                        <input type="file" multiple="multiple" runat="server" name="fuFile" id="fuFile" />
                                        <asp:RequiredFieldValidator Text="*" ID="RequiredFieldValidator1" runat="server"
                                            Display="Dynamic" ValidationGroup="File" Style="color: Red;" ControlToValidate="fuFile"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Document Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFileType" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>File Description
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileDesc" TextMode="MultiLine" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="File"
                                            Style="color: Red;" Text="*" ControlToValidate="txtFileDesc"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <%-- This row is for file Access--%>
                                <tr runat="server" id="trAccessRightFile">
                                    <td>Access
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFileAccessType" runat="server">
                                            <asp:ListItem Text="Read" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Share" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Rename" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Update" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Delete" Value="4"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Private" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%-- This row is for Sharing file
                                <tr>
                                    <td>
                                        Choose users
                                    </td>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rblFileChoseUser" onClick="ShowListBox(this.id, 'Content_lbFileUsers','divFileFriends','divFileGroups');">
                                            <asp:ListItem Text="All Users" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Groups" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Friends" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="No Access" Value="3" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:TextBox runat="server" ID="txtFileUser" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                --%>
                                <tr>
                                    <td></td>
                                    <td valign="middle">
                                        <div id="divFileFriends" style="display: none; width: 100%">
                                            <div style="float: left">
                                                <asp:ListBox ID="lbFileUsers" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                            <div style="float: left; vertical-align: middle; margin-left: 5px;">
                                                <div class="SubmitButton" onclick="$('#AddFriends').fadeIn(1000);">
                                                    Add Friends
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divFileGroups" style="display: none; width: 100%">
                                            <div style="float: left">
                                                <asp:ListBox ID="lbFileGroups" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                            <div style="float: left; vertical-align: middle; margin-left: 5px;">
                                                <div class="SubmitButton" onclick="$('#AddGroups').fadeIn(500);">
                                                    Add Groups
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <%--<td>
                                        <asp:ListBox ID="lbFileUsers" runat="server" SelectionMode="Multiple" Style="display: none;">
                                        </asp:ListBox>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>Tags
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileTags" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <br />
                                        (Comma separated tags)
                                    </td>
                                </tr>
                            </table>
                            <div id="divFileButton">
                                <asp:Button ID="btnFileAdd" CssClass="SubmitButton" OnClick="btnFileUploadAll_Click"
                                    runat="server" Text="Add" ValidationGroup="File" />&nbsp;
                                <asp:Button ID="btnFileCancel" CssClass="SubmitButton" OnClick="btnCancel_Click"
                                    OnClientClick="$('#FilePopup').fadeOut(500);" runat="server" Text="Cancel" title="Close Upload File" />
                            </div>
                            <div id="divFileProgress" style="display: none;">
                                <asp:Label ID="Label1" runat="server" Text="Uploading... Please wait" CssClass="SubmitButton"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="UploadAllFilesPopup" runat="server" style="display: none;">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 5%; border-radius: 4px; background-color: #fff; width: 90%; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#<%=UploadAllFilesPopup.ClientID%>').fadeOut(500);"
                            title="Close Upload File" style="float: right; cursor: pointer; position: relative;" />
                        <div style='width: 370px; text-align: left; float: left; padding: 10px 15px;' class='SmallHeaderText'>
                            Selected files
                        </div>
                        <div id="divAllFilesResult" runat="server" style="float: left; color: green; clear: both; padding: 15px;"
                            visible="false">
                        </div>
                        <div id="FilesRowContainer" runat="server" style="float: left; clear: both; width: 99%;">
                        </div>
                        <div id="divAllAddButton" style="float: right; clear: both; padding: 18px;">
                            <asp:Button ID="btnAddAllFiles" CssClass="SubmitButton" OnClick="btnAddAllFiles_Click"
                                runat="server" Text="Upload All" ValidationGroup="Validat" OnClientClick="GetSelectedFilesValue(); HideButton('divUploading', 'divAllAddButton'); " />&nbsp;
                            <asp:Button ID="btnAllFilesCancel" CssClass="SubmitButton" OnClick="btnAllFilesCancel_Click"
                                OnClientClick="$('#<%=UploadAllFilesPopup.ClientID%>').fadeOut(500);" runat="server"
                                Text="Cancel" title="Close Upload File" />
                            <asp:HiddenField ID="hdnDocType" runat="server" />
                            <asp:HiddenField ID="hdnFileDesc" runat="server" />
                            <asp:HiddenField ID="hdnFileAccess" runat="server" />
                            <asp:HiddenField ID="hdnFileTags" runat="server" />
                        </div>
                        <div id="divUploading" style="float: right; clear: both; padding: 18px; display: none;">
                            <asp:Label ID="lbluploading" runat="server" Text="Uploading... Please wait" CssClass="SubmitButton"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div id="FolderRename">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#FolderRename').fadeOut(500);"
                            title="Close Rename Folder" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Rename folder
                        </div>
                        <div id="div2" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="div4" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>Folder-Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFolderRename" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnRenameFolder" CssClass="SubmitButton" OnClick="btnRenameFolder_Click"
                                runat="server" Text="Rename" OnClientClick="return Validate('<%=txtFolderRename.ClientID%>');" />&nbsp;
                            <asp:Button ID="Button2" CssClass="SubmitButton" OnClick="btnCancel_Click" OnClientClick="$('#FolderRename').fadeOut(500);"
                                runat="server" Text="Cancel" title="Close Rename Folder" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="FileRename">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#FileRename').fadeOut(500);"
                            title="Close Rename File" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Rename folder
                        </div>
                        <div id="div5" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="div6" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>File-Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileRename" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnRenameFile" CssClass="SubmitButton" OnClick="btnRenameFile_Click"
                                runat="server" Text="Rename" OnClientClick="return Validate('<%=txtFileRename.ClientID%>');" />&nbsp;
                            <asp:Button ID="Button3" CssClass="SubmitButton" OnClick="btnCancel_Click" OnClientClick="$('#FileRename').fadeOut(500);"
                                runat="server" Text="Cancel" tille="Close popup" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="AddFriends">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#AddFriends').fadeOut(500);"
                            title="Close Add Friends" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Rename folder
                        </div>
                        <div id="div7" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="div8" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>File-Name
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="chkUserFriendList" runat="server" RepeatDirection="Vertical">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnAddFriends" CssClass="SubmitButton" OnClick="btnAddFriends_Click"
                                runat="server" Text="Add" OnClientClick="return Validate('<%=txtFileRename.ClientID%>');" />&nbsp;
                            <asp:Button ID="Button4" CssClass="SubmitButton" OnClick="btnCancel_Click" OnClientClick="$('#AddFriends').fadeOut(500);"
                                runat="server" Text="Cancel" title="Close Add Friends" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="AddGroups">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#AddGroups').fadeOut(500);"
                            title="Close Add Groups" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Create Group
                        </div>
                        <div id="div9" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="div10" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>Group Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Select Users
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="chkGroupUserList" runat="server" RepeatDirection="Vertical">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                            <%--<asp:Button ID="bntCreateGroup" CssClass="SubmitButton" runat="server" Text="Create"
                                OnClientClick="return Validate('Content_txtGroupName');" />--%>&nbsp;
                            <asp:Button ID="bntCreateGroup" CssClass="SubmitButton" OnClick="bntCreateGroup_Click"
                                runat="server" Text="Create" OnClientClick="return Validate('<%=txtGroupName.ClientID%>');" />&nbsp;
                            <asp:Button ID="Button6" CssClass="SubmitButton" OnClick="btnCancel_Click" OnClientClick="$('#AddGroups').fadeOut(500);"
                                runat="server" Text="Cancel" title="Close Add Groups" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="FolderShare">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#FolderShare').fadeOut(500);"
                            title="Close Share Folder" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Share folder
                        </div>
                        <div id="divFolder" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="divShare" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>Share With
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShareWith" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Message
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnShareFolder" CssClass="SubmitButton" runat="server" Text="Share"
                                OnClientClick="return Validate('<%=txtShareWith.ClientID%>');" OnClick="btnShareFolder_Click" />&nbsp;
                            <asp:Button ID="btnCancelShare" CssClass="SubmitButton" OnClientClick="$('#FolderShare').fadeOut(500);"
                                runat="server" Text="Cancel" OnClick="btnCancelShare_Click" title="Close Share Folder" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="FileShare">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <img src="<%=BASE_URL%>/static/images/close.png" onclick="$('#FileShare').fadeOut(500);"
                            title="Close Share Files" style="float: right; cursor: pointer; position: relative;" />
                        <div style='padding: 5px 0px 10px 0px; text-align: center; width: 370px;' class='SmallHeaderText'>
                            Share file
                        </div>
                        <div id="div11" runat="server" style="float: left; margin-left: 35px; padding: 5px;"
                            visible="false">
                        </div>
                        <div id="div12" runat="server" style="padding: 25px;">
                            <table style="text-align: left; font-size: 13px;">
                                <tr>
                                    <td>Share With
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShareFileWith" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Message
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShareMessFile" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnShareFile" CssClass="SubmitButton" runat="server" Text="Share"
                                OnClientClick="return Validate('<%=txtShareMessFile.ClientID%>');" OnClick="btnShareFile_Click" />&nbsp;
                            <asp:Button ID="Button7" CssClass="SubmitButton" OnClientClick="$('#FileShare').fadeOut(500);"
                                runat="server" Text="Cancel" OnClick="btnCancelShare_Click" title="Close Share Files" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivDeleting">
                <div style="opacity: 0.94; z-index: 11; height: 100%; position: fixed; width: 100%; top: 0%; left: 0%; background-color: #666;">
                    <div style="position: absolute; top: 20%; left: 35%; border-radius: 4px; background-color: #fff; width: 400px; text-align: left;">
                        <div id="div14" runat="server" style="padding: 25px; text-align: center; font-size: 20px;">
                            Deleting... Please Wait!
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileAdd" />
            <asp:AsyncPostBackTrigger ControlID="btnAddAllFiles" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>