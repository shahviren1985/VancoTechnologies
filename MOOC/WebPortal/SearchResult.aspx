<%@ Page Title="Search-Result" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="SearchResult" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="width: 80%; float: left; background-color: #fff; margin: 2% 10% 4% 10%;">
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a><a></a> <span class="divider">/</span></li><li>
                    Search Result</li></ul>
        </div>
        <asp:HiddenField ID="hfSearchResult" runat="server" />
        <div style="float: left; width: 100%">
            <div class="content-summary green" style="float: left; width: 97%; font-weight: bold;
                padding: 10px 0px 12px 5px; background-color: #fff; color: black;" id="searchHeader">
                Search Result
            </div>
            <div class="content-summary blue" id="searchContainer" style="float: left; width: 97%;
                padding: 13px 15px 13px 15px; background-color: #fff; color: Black; border: 1px solid #ccc;
                box-shadow: 0 2px 2px #999;">
                
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //BuilLeftNavigationForSylabus();
    </script>
    <style>
        .activityContainer
        {
            border-top: solid 1px #ccc;
            padding: 1%;
        }
        
        .activityContainer:hover
        {
            background-color: #EEE;
            cursor: pointer;
            border-radius: 0px;
        }
        
        .activityContainer a
        {
            color: #08c;
            text-decoration: underline;
        }
        .activityContainer a:hover
        {
            color: #08c;
            text-decoration: none;
        }
    </style>
</asp:Content>
