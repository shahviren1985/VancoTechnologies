<%@ Page Title="MOOC Academy - Typing tutor" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="TypingTutorial.aspx.cs" Inherits="TypingTutorial" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <link rel="Stylesheet" href="static/styles/chapter-style.css" />
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>
    <div style="float: left; width: 100%; text-align: center;">
        <iframe id="ifTypingTutor" style="width: 80%; min-height: 530px; border: none;" src="TypingTutor.htm">
        </iframe>
    </div>
    <script type="text/javascript">
        //BASE_URL = '<%=Util.BASE_URL%>';
        var drop = $("#drop");
        $(drop).css("display", "none");

        //function PopulateCourseName() {
        var drop = $("#label");
        $(drop).css("display", "block");
        $(drop).html("You are learning - Typing Tutorial");
    </script>
</asp:Content>
