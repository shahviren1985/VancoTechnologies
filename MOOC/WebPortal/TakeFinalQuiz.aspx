<%@ Page Title="MOOC Academy - Final Quiz" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeFile="TakeFinalQuiz.aspx.cs" Inherits="TakeFinalQuiz" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <link rel="Stylesheet" href="static/styles/chapter-style.css" />
    <asp:HiddenField ID="hfCourseName" Value="[]" runat="server" />
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>
    <div style="float: left; width: 100%; text-align: center;">
        <iframe id="finalquizcontainer" class="final-quiz-container" src="FinalQuiz.htm">
        </iframe>
    </div>
    <script type="text/javascript">
        //BASE_URL = '<%=Util.BASE_URL%>';
        var drop = $("#drop");
        $(drop).css("display", "none");

        //function PopulateCourseName() {
        var drop = $("#label");
        $(drop).css("display", "block");
        $(drop).html("You are learning - " + $("#Content_hfCourseName").val());
    </script>
</asp:Content>
