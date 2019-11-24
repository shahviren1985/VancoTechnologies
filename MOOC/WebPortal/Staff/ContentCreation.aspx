<%@ Page Title="Content Creation" Language="C#" MasterPageFile="~/Master/StaffMasterPage.master"
    AutoEventWireup="true" CodeFile="ContentCreation.aspx.cs" Inherits="Staff_ContentCreation" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script type="text/javascript" src="../static/scripts/ckeditor.js"></script>
    <%--<link rel="Stylesheet" href="../static/new.bootstrap.css" />--%>
    <link rel="Stylesheet" href="../static/bootstrap.css" />
    <link rel="Stylesheet" href="../static/pml/NewStyle.css" />
    <%--<link rel="Stylesheet" href="../static/pml/StyleSheet.css" />--%>
    <link rel="Stylesheet" href="../static/pml/CreatePml5.css" />
    <%--<script src="../static/scripts/ContentManager2.js" type="text/javascript"></script>
    <script src="../static/scripts/SearchManager2.js" type="text/javascript"></script>
    <link rel="Stylesheet" href="../static/pml/jquery.jscrollpane.css" />
    <script src="../static/scripts/jquery.jscrollpane.min.js" type="text/javascript"></script>
    <script src="../static/scripts/FileUploader2.js" type="text/javascript"></script>--%>
    <!-- for jquery tab-->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <!-- for jquery tab-->
    <script src="../static/scripts/AA.core.js"></script>
    <script src="../static/scripts/mooc.gallery.core.js"></script>
    <style type="text/css">
        #cke_editor1
        {
            width: 97% !important;
        }
        
        #sideButtons
        {
            float: right;
            width: 25px;
            margin-top: -30%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var BASE_URL = "";
        var SERVER_BASE = "";
        var TINY_URL = "";
        var BE_URL = "";
        var DirectEdit = "";
        var POWER_LINK_NAME = "";
        var BE_REDIRECT_URL = "";
        var ACTUAL_PAGE_URL = "";
        var TabToolBarList = [false];
        var BE_SHARE_URL = "";
        var createPmluserId = "";
        var pmlUid = "";
        var PML_UID = pmlUid;
        var PML_USER = "";
        var BE_KEYWORD = '';
        var isPinned = "false";
        var EDIT_MODE = false; /* 
        
        if (BE_KEYWORD.indexOf(escape('<a')) == 0 || BE_KEYWORD.indexOf(escape('<A')) == 0) {
            BE_KEYWORD = PMLQ(unescape(BE_KEYWORD)).html();
        } 
        if (BE_KEYWORD.indexOf(escape('<img')) == 0 || BE_KEYWORD.indexOf(escape('<img')) == 0) {
            BE_KEYWORD = PMLQ(unescape(BE_KEYWORD)).attr("title");
        }*/
        var MODE = "";
        var THEME = { "RedGradientColor": "#fe1f20", "GreenGradientColor": "#86c400", "YellowGradientColor": "#fff200", "BlueGradientColor": "#71a9d3", "Gradient": "#333" };
        var g_BuildingPml = false
        var permissions = '';
        var UPLOAD_LIMIT_REACHED = '';
        var SPACE_REMAIN = '';
        //var PERMISSIONS;



        if (permissions != null && permissions.length > 0)
            permissions = JSON.parse(permissions);


        var currentImageObject;
        //var JQ = jQuery.noConflict();
        var currentVideoStartCount = 1;
        var currentImageStartCount = 1;
        //var currentTabIndex = 0;
        var minHeight;
        var GlobalTop = 0;
        var GlobalLeft = 0;
        var TextTop = 0;
        var TextLeft = 0;
        var itemnumber = 0;
        // Array to hold upto 10 tab's content
        //var Global_Item_List = new Array(10);
        //var itemCounter = 0;
    </script>
    <style type="text/css">
        div.close
        {
            display: none;
        }
        .NextVideoButton
        {
            float: right;
            margin: 20px;
            background: #CCC;
            color: #000;
            padding: 4px;
            border: 1px solid #666;
            border-radius: 4px;
            cursor: pointer;
        }
        .ImageDeleteOptions
        {
            float: left;
            background-color: #CCC;
        }
        #search_results ul li, #search_results2 ul li
        {
            float: left;
            width: 125px;
            height: 110px;
        }
        .SearchResultImage
        {
            float: left;
            margin-bottom: 5px;
        }
        .SearchResultTitle
        {
            float: left;
            font-weight: normal;
            padding-top: 10px;
            text-align: left;
            padding-left: 5px;
            margin-bottom: 5px;
        }
        .SearchResultDescription
        {
            font-weight: normal;
            float: left;
            text-align: left;
            padding-left: 5px;
            margin-bottom: 5px;
        }
        /*.attachAddImage
        {
            position: relative;
            top: -25px;
            left: -84px;
            padding: 25px;
        }*/
        .attachDocumentImage
        {
            position: relative;
            top: 50px;
            left: -240px;
            padding: 25px;
        }
        .attachVideoImage
        {
            top: 115px;
        }
        .attachImage
        {
            position: relative;
            top: 20px;
            left: 20px;
        }
        .ImageItem
        {
            position: relative;
            cursor: pointer;
        }
        .ImageItem2
        {
            /*position: relative;
            list-style: none;
            float: left;
            width: 80px;
            height: 60px;*/
            cursor: pointer;
        }
        .content-icon-container
        {
            text-align: center;
        }
        .content-icon
        {
            margin-left: 10px;
        }
        
        ul
        {
            float: left;
            padding-left: 10px;
            list-style: none;
        }
        ul.Thumbnails
        {
            margin: 10px;
            padding: 0px;
        }
        .attach2, .attach3
        {
            top: -2px;
            height: 102px;
            width: 134px;
        }
        .attach3
        {
            top: 20px;
        }
        .FileUpload
        {
            float: left;
            width: 60%;
        }
        .Highlight
        {
            background-color: Red;
        }
        .EnterText
        {
        }
        .Normal
        {
            background-color: transparent;
        }
    </style>
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Content Creation
        </h3>
    </div>
    <div>
        <ul class="breadcrumb" style="width: 97%">
            <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
            <li>Content Creation</li>
        </ul>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
        color: Red; float: left; width: 100%;" visible="false">
    </div>
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>
    <asp:HiddenField ID="hfPageContent" runat="server" />
    <div class="Record">
        <div class="Column1" style="width: 49%">
            <b>Chapter: </b>
            <asp:Label ID="lblChapterName" runat="server"></asp:Label>
        </div>
        <div class="Column1" style="width: 49%">
            <b>Section: </b>
            <asp:Label ID="lblSectionName" runat="server"></asp:Label>
        </div>
    </div>
    <div class="Record">
        <div id="editor1" name="editor1" rows="10" cols="80">
        </div>
        <div id="sideButtons" style="margin-right: -15px;">
            <a data-toggle="modal" href="#getEarlyAccess" role="menuitem" tabindex="-1" style="float: right;
                width: 30px; padding: 2px 4px;" title="add image" class="btn">
                <img src="../static/images/icon-picture.png" alt="icon-picture" style="width:30px" />
            </a><a data-toggle="modal" href="#video" role="menuitem" tabindex="-1" style="float: right;width: 30px; padding: 2px 4px;"
                title="add video" class="btn">
                <img src="../static/images/icon-video.png" alt="icon-picture" style="width:30px"/>
            </a>
            <div class="btn btn-group" style="float: right; display: none">
                Ex
            </div>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            Estimate Time</div>
        <div class="Column2">
            <asp:TextBox ID="txtEstimateTime" runat="server" placeholder="Estimate time"></asp:TextBox>
            in minutes</div>
    </div>
    <div class="Record">
        <div class="Column1">
            Select Page Color</div>
        <div class="Column2">
            <asp:DropDownList ID="ddlPageColor" runat="server">
                <asp:ListItem Text="Blue" Value="blue" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Green" Value="green"></asp:ListItem>
                <asp:ListItem Text="Pink" Value="pink"></asp:ListItem>
                <asp:ListItem Text="Orange" Value="orange"></asp:ListItem>
                <asp:ListItem Text="Purple" Value="purple"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="Record">
        <asp:Button ID="btnSaveContent" CssClass="btn btn-primary" runat="server" Text="Save"
            OnClientClick="return ValidateEditor();" OnClick="btnSaveContent_Click" />
        <asp:Button ID="btnSave_Finish" CssClass="btn btn-success" runat="server" Text="Save & Finish"
            OnClientClick="return ValidateEditor();" OnClick="btnSaveContent_Click" />
        <asp:Button ID="btnCancel" PostBackUrl="~/Staff/ManageContent.aspx" CssClass="btn"
            runat="server" Text="Cancel" />
    </div>
    <div id="PML">
        <div class="modal fade" id="getEarlyAccess" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2"
            style="display: none; width: 80%; margin-left: 10%; left: 0px; top: 5%;" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="ResetImagePopup();">
                            &times;</button>
                        <h4 class="modal-title">
                            Manage your image gallery</h4>
                    </div>
                    <div class="modal-body" style="min-height: 500px;">
                        <div id="tabs" style="float: left; width: 65%; min-height: 490px;">
                            <ul style="width: 99%;">
                                <li><a href="#web-image"><i class="icon-globe"></i><span style="margin-left: 10px;">
                                    Search</span></a></li>
                                <li><a href="#local-image"><i class="icon-folder-open"></i><span style="margin-left: 10px;">
                                    Upload</span></a></li>
                                <li onclick="GetImagesFromMoocServer();"><a href="#mooc-lib"><i class="icon-camera">
                                </i><span style="margin-left: 10px;">Library</span></a></li>
                            </ul>
                            <div style="float: left; width: 99%; margin-top: 10px;">
                                <div id="web-image">
                                    <div class="Record">
                                        <div class="Column2" style="width: 100%">
                                            <input type="text" id="txtSearchImage" placeholder="Enter keywords to search on google..."
                                                style="width: 60%" onkeyup="SearchImages(1);" />
                                            <button id="btnSearchImg" class="btn btn-primary" type="button" style="margin-top: -10px;"
                                                onclick="SearchImages(1);">
                                                Search
                                            </button>
                                        </div>
                                    </div>
                                    <div id="loaderImg" style="float: left; width: 100%; min-height: 200px; text-align: center;
                                        padding-top: 100px; display: none">
                                        <img src="../static/images/loading.gif" />
                                        Searching Images...
                                    </div>
                                    <div id="NavigationButtons"></div>
                                    <div class="Record" id="searchResult" style="width: 100%; overflow: auto; float: left">
                                    </div>
                                </div>
                                <div id="local-image">
                                    <div class="Record">
                                        <span>Images from your local machine</span></div>
                                    <div class="ErrorContainer" id="status" style="display: none">
                                    </div>
                                    <div class="Record">
                                        <div class="Column2" style="width: 100%">
                                            <input type="file" id="fuUploadImg" onchange="UploadSelectedFile(this);" />
                                            <button id="btnUploadImg" class="btn btn-primary" type="button" style="margin-top: -10px;">
                                                Upload
                                            </button>
                                        </div>
                                    </div>
                                    <div id="localImg" style="display: none">
                                        <div class="Record">
                                            <img alt="alter text" id="imgLocal" width="200px" height="150px" />
                                        </div>
                                        <div class="Record">
                                            <div class="Column1" style="width: 200px; text-align: center; padding-left: 60px;">
                                                <div onclick="ShowImagePropertyBlockAndAddToEditor('imgLocal');" style="cursor: pointer;
                                                    float: left;">
                                                    <i class="icon-plus content-icon" title="add this image to content"></i>
                                                </div>
                                                <%--<div onclick="$('#imgProp').attr('src',$('#imgLocal').attr('src'));" style="cursor: pointer;
                                                    float: left;">--%>
                                                <div onclick="ShowImagePropertyBlock('imgLocal');" style="cursor: pointer; float: left;">
                                                    <i class="icon-info-sign content-icon" title="modify image property"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="mooc-lib">
                                    <div class="Record">
                                        <span>Images from your mooc server account</span>
                                    </div>
                                    <div id="loaderServerImg" style="float: left; width: 100%; min-height: 200px; text-align: center;
                                        padding-top: 100px; display: none">
                                        <img src="../static/images/loading.gif" />
                                        Loading Images...
                                    </div>
                                    <div class="Record" id="serverImg" style="width: 100%; overflow: auto; float: left">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="imgProperty" style="float: right; padding: 10px; border: 1px solid #aaaaaa;
                            border-radius: 4px; width: 30%; min-height: 470px;">
                            <div class="Record">
                                <span style="font-weight: bold; font-size: 16px; margin-left: 30%;">Image Properties</span>
                            </div>
                            <div class="Record">
                                <img id="imgProp" alt="mooc image" style="width: 320px; height: 200px;" />
                            </div>
                            <div class="Record">
                                <input type="text" id="txtImgWidth" placeholder="Width" style="width: 50px;" />
                            </div>
                            <div class="Record">
                                <input type="text" id="txtImgHeight" placeholder="Height" style="width: 50px;" /></div>
                            <div class="Record">
                                <input type="text" id="txtAltText" placeholder="Alternate Text" style="width: 95%;" /></div>
                            <div class="Record">
                                <input type="text" id="txtLink" placeholder="Link" style="width: 95%;" /></div>
                            <div class="Record">
                                <div id="btnPropOk" class="btn btn-success" onclick="AddImageToCkEditor();">
                                    Ok</div>
                                <a href="Javascript:void(0)" class="btn" data-dismiss="modal" title="click to close popup"
                                    onclick="ResetImagePopup();">Close</a>
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
                        $("#tabs").tabs();
                    </script>
                </div>
                <div class="modal-footer">
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
    <div class="modal fade" id="video" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2"
        style="display: none; width: 80%; margin-left: 10%; left: 0px; top: 5%;" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="ResetVideoPopup();">
                        &times;</button>
                    <h4 class="modal-title">
                        Search Video</h4>
                </div>
                <div class="modal-body" style="min-height: 500px;">
                    <div id="tabs_video" style="float: left; width: 65%; min-height: 490px;">
                        <ul style="width: 99%;">
                            <li><a href="#web-video"><i class="icon-globe"></i><span style="margin-left: 10px;">
                                Search</span></a></li>
                        </ul>
                        <div style="float: left; width: 99%; margin-top: 10px;">
                            <div id="web-video">
                                <div class="Record">
                                    <div class="Column2" style="width: 100%">
                                        <input type="text" id="searchkey" placeholder="Enter text to search YouTube video..."
                                            style="width: 60%" onkeyup="SearchYoutube(1);" />
                                        <button id="Button1" class="btn btn-primary" type="button" style="margin-top: -10px;"
                                            onclick="SearchYoutube(1);">
                                            Search
                                        </button>
                                    </div>
                                </div>
                                <div id="loading_vid" style="float: left; width: 100%; min-height: 200px; text-align: center;
                                    padding-top: 100px; display: none">
                                    <img src="../static/images/loading.gif" />
                                    Searching Videos...
                                </div>
                                <div  id="VideoNavigationButtons">
                                </div>
                                <div class="Record" id="search_video" style="width: 100%; overflow: auto; float: left">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="videoProperty" style="float: right; padding: 10px; border: 1px solid #aaaaaa;
                        border-radius: 4px; width: 30%; min-height: 470px;">
                        <div class="Record">
                            <span style="font-weight: bold; font-size: 16px; margin-left: 30%;">Video Properties</span>
                        </div>
                        <div class="Record">
                            <img id="imgVideoProp" alt="mooc image" style="width: 320px; height: 200px;" />
                        </div>
                        <div class="Record">
                            <input type="text" id="txtVideoWidth" placeholder="Width" style="width: 50px;" />
                        </div>
                        <div class="Record">
                            <input type="text" id="txtVideoHeight" placeholder="Height" style="width: 50px;" /></div>
                        <div class="Record">
                            <input type="text" id="txtVideoName" placeholder="Name" style="width: 95%;" /></div>
                        <div class="Record">
                            <input type="text" id="txtVideoLongDesc" placeholder="Long Description" style="width: 95%;" /></div>
                        <div class="Record">
                            <div id="Div2" class="btn btn-success" onclick="AddVideoToCkEditor();">
                                Ok</div>
                            <a href="Javascript:void(0)" class="btn" data-dismiss="modal" title="click to close popup"
                                onclick="ResetVideoPopup();">Close</a>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <script type="text/javascript">
                    $("#tabs_video").tabs();
                </script>
                <div class="modal-footer">
                </div>
                <!-- /.modal-dialog -->
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Replace the <textarea id="editor1"> with a CKEditor
        // instance, using default configuration.
        CKEDITOR.replace('editor1');

        function GetContents() {
            var editor = CKEDITOR.instances.editor1;
            // Get editor contents
            // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-getData
            //alert(editor.getData());
            //alert(Encoding(editor.getData()));

            return Encoding(editor.getData());
        }

        function SetContents(value) {
            //CKEDITOR.replace('editor1');
            var editor = CKEDITOR.instances.editor1;
            //var value = document.getElementById('htmlArea').value;

            // Set editor contents (replace current contents).
            // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-setData
            editor.setData(unescape(value));
        }

        function Encoding(Data) {
            var EncodeData = escape(Data);
            return EncodeData;
        }

        function ValidateEditor() {
            var data = GetContents();
            if (data == "") {
                return false;
            }

            if ($("#Content_txtEstimateTime").val() == "") {
                alert("Please enter section content");
                $("#Content_txtEstimateTime").focus();
                return false;
            }
            var time;
            if (!parseInt($("#Content_txtEstimateTime").val(), time)) {
                alert("Please enter numeric value");
                $("#Content_txtEstimateTime").focus();
                $("#Content_txtEstimateTime").val("");
                return false;
            }

            $("#Content_hfPageContent").val(data);
            return true;
        }
    </script>
    <!-- PML Javascripts functions-->
    <script type="text/javascript">
        function AddImageToCkEditor() {
            var isAllEmpty = true;
            var imgProp = $("imgProp");

            //var imgNode = "<img src='" + $("#imgProp").attr("src") + "' ";
            var imgNode = "<img src='" + $("#imgProp").attr("orignal") + "' ";

            if ($("#imgProp").attr("orignal") == undefined || $("#imgProp").attr("orignal") == null) {
                return;
            }

            if ($("#txtAltText").val() != "") {
                imgNode += "alt='" + escape($("#txtAltText").val()) + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "alt='" + $("#imgProp").attr("alt") + "' ";
            }

            if ($("#txtImgWidth").val() != "") {
                var width = $("#txtImgWidth").val();
                width = width.replace("'", "");
                if (!parseInt(width)) {
                    width = 300;
                }
                imgNode += "width='" + width + "' ";

                //imgNode += "width='" + $("#txtImgWidth").val() + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "width='300' ";
            }

            if ($("#txtImgHeight").val() != "") {
                var height = $("#txtImgHeight").val();
                height = height.replace("'", "");
                if (!parseInt(height)) {
                    height = 300;
                }
                imgNode += "height='" + height + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "height='250' ";
            }

            imgNode += ">";

            if (isAllEmpty)
                return;

            if ($("#txtLink").val() != "") {
                var link = "<a target='_top' href='" + escape($("#txtLink").val()) + "'>" + imgNode + "</a>";

                InsertHTMLtoEditor(link);
            }
            else {
                InsertHTMLtoEditor(imgNode);
            }
            ResetImagePopup();
            $('#getEarlyAccess').fadeOut(500);
            //$('.modal-backdrop fade in').fadeOut(500);
            $('.modal-backdrop').fadeOut(500);
        }

        function AddVideoToCkEditor() {
            var isAllEmpty = true;
            var imgProp = $("#imgVideoProp");

            //var imgNode = "<img src='" + $("#imgProp").attr("src") + "' ";
            var imgNode = "<iframe align='top' frameborder='0' scrolling='no' src='" + $("#imgVideoProp").attr("orignal") + "' ";

            if ($("#imgVideoProp").attr("orignal") == undefined || $("#imgVideoProp").attr("orignal") == null) {
                return;
            }

            if ($("#txtVideoWidth").val() != "") {
                var width = $("#txtVideoWidth").val();
                width = width.replace("'", "");
                if (!parseInt(width)) {
                    width = 300;
                }
                imgNode += "width='" + width + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "width='300' ";
            }

            if ($("#txtVideoHeight").val() != "") {
                var height = $("#txtVideoHeight").val();
                height = height.replace("'", "");
                if (!parseInt(height)) {
                    height = 250;
                }

                imgNode += "height='" + height + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "height='250' ";
            }

            if ($("#txtVideoName").val() != "") {
                imgNode += "name='" + escape($("#txtVideoName").val()) + "' ";
                imgNode += "title='" + escape($("#txtVideoName").val()) + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "name='youtube video' ";
                imgNode += "title='youtube video' ";
            }

            if ($("#txtVideoLongDesc").val() != "") {
                imgNode += "longdesc='" + escape($("#txtVideoLongDesc").val()) + "' ";
                isAllEmpty = false;
            }
            else {
                imgNode += "longdesc='youtube video' ";
            }

            imgNode += "</iframe>";

            if (isAllEmpty)
                return;

            InsertHTMLtoEditor(imgNode);

            ResetVideoPopup();
            $('#video').fadeOut(500);
            $('.modal-backdrop').fadeOut(500);
        }

        function InsertHTMLtoEditor(value) {
            var editor = CKEDITOR.instances.editor1;
            // Check the active editing mode.
            if (editor.mode == 'wysiwyg') {
                // Insert HTML code.
                // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-insertHtml
                editor.insertHtml(value);
            }
            else {
                alert('You must be in WYSIWYG mode!');
            }
        }
    </script>
</asp:Content>
