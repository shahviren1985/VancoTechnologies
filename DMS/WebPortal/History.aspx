<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: History</title>
    <link href="static/stylesheets/reset.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.css" rel="stylesheet" />
    <link href="static/stylesheets/style.css" rel="stylesheet" />
    <script src="static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.document.history.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="GridContainer">
        <div id="SearchContainer">
            <div class="search-bar">
                <input type="text" placeholder="Search documents..." class="search-text" />
                <div class="search-button btn btn-success" onclick="SearchDocuments();">Search</div>
                <%--<a href="Search.aspx" title="Advanced Search" style="margin-left: 20px;">Advance Search</a>--%>
                <a href="Search.aspx" id="aAdvancedLink" runat="server" title="Advanced Search" style="margin-left: 20px;">Advance Search</a>
            </div>
        </div>
        <div id="Grid"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var id = document.location.href.substring(document.location.href.lastIndexOf("?") + 4);
            GetHistoryGridContent(id.trim());
        });
    </script>
</asp:Content>