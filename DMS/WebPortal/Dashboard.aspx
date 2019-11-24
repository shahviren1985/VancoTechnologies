<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: Dashboard</title>
    <link href="static/stylesheets/reset.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.css" rel="stylesheet" />
    <link href="static/stylesheets/style.css" rel="stylesheet" />
    <link href="static/stylesheets/StyleSheet.css" rel="stylesheet" />
    <script src="static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.dashboard.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div id="PrincipalLinks" class="DashboardLinks" runat="server" visible="false">
        <ul>
            <li><a href="Documents.aspx?doctype=inward">Inward Documents</a></li>
            <li><a href="Documents.aspx?doctype=outward">Outward Documents</a></li>
            <li><a href="DocumentManagement/Dashboard.aspx">Employee Records</a></li>
        </ul>
    </div>
    <div id="OfficeAdminStaffLinks" class="DashboardLinks" runat="server" visible="false">
        <ul>
            <li><a href="Documents.aspx?doctype=inward">Inward Documents</a></li>
            <li><a href="Documents.aspx?doctype=outward">Outward Documents</a></li>
            <li><a href="DocumentManagement/Dashboard.aspx">Employee Records</a></li>
        </ul>
    </div>
    <div id="TeachingStaffLinks" class="DashboardLinks" runat="server" visible="false">
        <ul>
            <li><a href="Documents.aspx?doctype=outward">Outward Documents</a></li>
            <li><a href="DocumentManagement/Dashboard.aspx">Employee Records</a></li>
        </ul>
    </div>--%>
    <div id="GridContainer">
        <div id="SearchContainer">
            <div class="search-bar">
                <input type="text" placeholder="Search documents..." class="search-text" />
                <div id="marathi-key-pop" title="Type in मराठी" class="marathi-key">M</div>
                <div class="search-button btn btn-success" onclick="SearchDocuments();" style="  margin-left: 10px; margin-top: 10px;">Search</div>
                <a href="Search.aspx" title="Advance Search" style="margin-left: 20px;">Advance Search</a>
            </div>
        </div>
        <div id="Grid"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            GetGridContent();
        });

        $("#marathi-key-pop").click(function () {
            window.open("https://translate.google.com/?hl=mr#mr/en", "_blank", "height=450, width=700");
        });
    </script>
</asp:Content>

