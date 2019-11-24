<%@ Page Title="View and Response" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ViewandReplayQuery.aspx.cs" Inherits="SuperAdmin_ViewandReplayQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script type="text/javascript" src="../static/scripts/ckeditor.js"></script>
    <style type="text/css">
        #Content_View .Column1
        {
            width: 200px;
        }
    </style>
    <div id="View" runat="server">
        <asp:HiddenField ID="hfUserQuery" runat="server" />
        <asp:HiddenField ID="hfQueryId" runat="server" />
        <asp:HiddenField ID="hfResponseMessage" runat="server" />
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                View Query
            </h3>
        </div>
        <div id="Success" class="SuccessContainer" runat="server" style="display: none; width: 100%;">
        </div>
        <div class="Record">
            <div class="Column1" style="font-weight: bold">
                User Name:
            </div>
            <div class="Column1" id="username">
                
            </div>
            <div class="Column1" style="font-weight: bold">
                Name:
            </div>
            <div class="Column1" id="name">
                
            </div>
        </div>
        <div class="Record">
            <div class="Column1" style="font-weight: bold">
                Mobile Number:
            </div>
            <div class="Column1" id="mobile">
                
            </div>
            <div class="Column1" style="font-weight: bold">
                Date Posted:
            </div>
            <div class="Column1" id="date">
                
            </div>
        </div>
        <div class="Record">
            <div class="Column1" style="font-weight: bold">
                College Name:
            </div>
            <div class="Column1" id="college">
                
            </div>
            <div class="Column1" style="font-weight: bold">
                Email Address:
            </div>
            <div class="Column1" id="email">
                
            </div>
        </div>
        <div class="Record">
            <div class="Column1" style="font-weight: bold">
                Student's Query:
            </div>
            <div class="Column2" id="query" style="text-align: justify">
            </div>
        </div>
        <div class="Record">
            <div id="btnReplay" class="btn btn-success" onclick="ShowRplayDiv();" style="margin-right: 4px;
                padding: 6px;">
                Response
            </div>
            <asp:Button ID="bntCancel" runat="server" Text="Cancel" CssClass="btn" PostBackUrl="~/SuperAdmin/ContactInquiry.aspx" />
        </div>
    </div>
    <div id="Replay" runat="server">
        <div style="margin-top: 5%;">
            <div id="Div1" style="float: left;">
                <asp:Image Style="width: 60%;" ID="Image1" runat="server" />
            </div>
            <h3>
                Response
            </h3>
        </div>
        <div id="errorSummary" class="ErrorContainer" style="display: none; width: 100%;">
        </div>
        <div class="Record">
            <div class="Column1">
                TO</div>
            <div class="Column2">
                <asp:TextBox ID="txtToAddress" runat="server" Width="500px"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Subject</div>
            <div class="Column2">
                <asp:TextBox ID="txtSubject" runat="server" Width="500px"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <%-- <div class="Column1">
                Message</div>
            <div class="Column2">--%>
            <asp:TextBox ID="txtMessage" TextMode="MultiLine" Width="500px" Height="100px" runat="server"
                Visible="false"></asp:TextBox>
            <div id="editor1" name="editor1" rows="10" cols="80">
                <%-- </div>--%>
            </div>
    </div>
    <div class="Record">
        <%--<div class="btn btn-success" onclick="SaveAndSentMail();">Send
                </div>--%>
        <asp:Button ID="btnSaveSend" CssClass="btn btn-success" runat="server" Text="Send Email"
            OnClientClick="return SaveAndSentMail();" OnClick="btnSaveSend_Click" />
        <div class="btn" onclick="HideRplayDiv();" style="padding: 6px;">
            Cancel
        </div>
    </div>
    </div>
    <script type="text/javascript">
        $('#Content_Replay').hide(10);
        function ShowRplayDiv() {
            $('#Content_View').hide(1000);
            $('#Content_Replay').show(1000);
        }
        function HideRplayDiv() {
            $('#Content_View').show(1000);
            $('#Content_Replay').hide(1000);
            $("#errorSummary").css("display", "none");
            $("#errorSummary").html("");
        }

        function PopulateStudentQuery() {
            var userQuery = {};
            userQuery = JSON.parse($("#Content_hfUserQuery").val());

            console.log(userQuery);

            $("#username").html(userQuery.UserName); $("#name").html(userQuery.Name);
            $("#mobile").html(userQuery.MobileNo); $("#date").html(userQuery.DisplayDate);
            $("#college").html(userQuery.CollegeName); $("#email").html(userQuery.Email);
            $("#query").html(userQuery.Query);
            $("#Content_txtToAddress").val(userQuery.Email);
            $("#Content_txtSubject").val("MOOC Academy :: Response to your query");

            //var staticMessageText = '<p><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif"><span style="color:#000000">Hello ' + capitalize(userQuery.Name) + ',</span></span></span></p><p><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif"><span style="color:#000000">Thank you for using MOOC Academy.</span></span></span></p><div>&nbsp;</div><div>&nbsp;</div><div>&nbsp;</div><pre><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif">Thanks<br/>MOOC Academy Team </span></span></pre><p><a href="http://moocacademy.in" target="_blank"><img class="logo" src="' + BASE_URL + 'static/images/logo.png" style="height:80px; width:150px" /></a></p>';
            var staticMessageText = '<p><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif"><span style="color:#000000">Hello ' + capitalize(userQuery.Name) + ',</span></span></span></p><p><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif"><span style="color:#000000">Thank you for using MOOC Academy.</span></span></span></p><p><span style="font-family:arial,helvetica,sans-serif">Response to your query &quot;' + userQuery.Query + '&quot;.</span></p><div>&nbsp;</div><div>&nbsp;</div><div>&nbsp;</div><pre><span style="font-size:14px"><span style="font-family:arial,helvetica,sans-serif">Thanks <br/> MOOC Academy Team </span></span></pre><p><a href="http://moocacademy.in" target="_blank"><img class="logo" src="' + BASE_URL + 'static/images/logo.png" style="height:80px; width:150px" /></a></p>';
            SetContents(staticMessageText);
        }

        function SaveAndSentMail() {
            if ($("#Content_txtToAddress").val() == "") {
                $("#errorSummary").html("Please enter email to-address");
                $("#errorSummary").css("display", "block");
                return false;
            }
            else if (!validateEmail($("#Content_txtToAddress").val())) {
                $("#errorSummary").html("Please enter valid email address");
                $("#errorSummary").css("display", "block");
                return false;
            }

            if ($("#Content_txtSubject").val() == "") {
                $("#errorSummary").html("Please enter email subject");
                $("#errorSummary").css("display", "block");
                return false;
            }
            /*if ($("#Content_txtMessage").val() == "") {
                $("#errorSummary").html("Please enter email message");
                $("#errorSummary").css("display", "block");
                return false;
            }*/

            var message = GetContents();

            if (message == "") {
                $("#errorSummary").html("Please enter email message");
                $("#errorSummary").css("display", "block");
                return false;
            }

            $("#Content_hfResponseMessage").val(message);

            var toAddress = $("#Content_txtToAddress").val();
            var subject = $("#Content_txtSubject").val();
            

            return true;
        }

    </script>
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

            $("#Content_hfPageContent").val(data);
            return true;
        }
    </script>
</asp:Content>
