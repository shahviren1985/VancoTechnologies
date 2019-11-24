<%@ Page Title="Super Admin Dashboard" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="SuperAdmin_Dashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script language="javascript">
        function ShowLinkDescription(element, className) {
            var timer = -1;
            var e;
            $(element).find(".LinkDescription").each(function () {
                e = $(this);
                timer = setTimeout(function () {
                    if ($(e).attr("timer") != undefined) {
                        $(e).removeAttr("timer");
                        $(e).show();

                        $(e).attr("class", className + " LinkDescription");
                        //setTimeout(function () { $(e).fadeOut(500); timer = -1; }, 3000);
                    }
                }, 1000);

                $(e).attr("timer", timer);
            });
        }

        function HideLinkDescription(element) {
            $(element).find(".LinkDescription").each(function () {
                $(this).removeAttr("timer");
                $(this).fadeOut(100);
            });
        }
    </script>
    <style>
        .Image
        {
            width: 60px;
            height: 60px;
        }
        .main-div
        {
            float: left;
            margin-top: 5%;
            width: 100%;
        }
    </style>
    <div class="main-div">
        <div class="Box BoxOrange">
            <div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/MarksEntry.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="ContactInquiry.aspx">Student Queries Details</a>
                        <div class="LinkDescription">
                            View Users Queries and resolve that queries</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Marksheet.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="ContentReportIssue.aspx">Content Report Issues Details</a>
                        <div class="LinkDescription">
                            View Content Report Issues by sent by users</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/networks.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="MigrateCourseToDifferentDB.aspx">Migrate Course to Different Databases</a>
                        <div class="LinkDescription">
                            Move course to selected databases </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
