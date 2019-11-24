<%@ Page Title="Fundamentals of Computer" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="CourseContent.aspx.cs" Inherits="CourseContent" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script>
        var drop = $("#drop");
        $(drop).css("display", "none");
    </script>
    <style>
        .hfChap, hfSec
        {
        }
    </style>
    <link rel="Stylesheet" href="static/styles/chapter-style.css" />
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>
    <div style="float: left; width: 100%; text-align: center;" id="ifContainer">
        <asp:HiddenField ID="hfChapters" Value="[]" runat="server" />
        <asp:HiddenField ID="hfSections" Value="[]" runat="server" />
        <asp:HiddenField ID="hfCourseName" Value="[]" runat="server" />
        <div id="fblikebutton">
        </div>
        <iframe id="ifContent" style="width: 80%; min-height: 1100px; border: none; margin-bottom: 3%;">
        </iframe>
    </div>
    <script type="text/javascript"> 

        var drop = $("#drop");
        $(drop).css("display", "none");

        function PopulateCourseName() {
            var drop = $("#label");
            $(drop).css("display", "block");
            $(drop).html("You are learning - " + $("#Content_hfCourseName").val());
            document.title = $("#Content_hfCourseName").val();
        }

        function ShowModul(a) {
            var user = $(a).attr("userId");
            var pass = $(a).attr("password");
            $("#Content_txtOldPassword").val(pass);
            //$("#Content_hfSelectedUserName").val(user);
            //$("#Content_lblUserName").html("User Name: <b>" + user + "</b>");
        }

        function ChangePassword() {
            if ($("#Content_txtOldPassword").val() == "") {
                $("#Content_txtOldPassword").focus();
                alert("Please enter old password");
                return false;
            }

            if ($("#Content_txtNewPassword").val() == "") {
                $("#Content_txtNewPassword").focus();
                alert("Please enter new password");
                return false;
            }

            if ($("#Content_txtRetypeNewPassword").val() == "") {
                $("#Content_txtRetypeNewPassword").focus();
                alert("Please re-type new password");
                return false;
            }

            if ($("#Content_txtRetypeNewPassword").val() != $("#Content_txtNewPassword").val()) {
                alert("New password and retype password not match");
                $("#Content_txtRetypeNewPassword").val("");
                $("#Content_txtRetypeNewPassword").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //BASE_URL = '<%=Util.BASE_URL%>';
            var qs = getQueryStrings();
            //alert(1);
            //alert(qs);
            var courseid = qs["courseid"];

            //alert(courseid);
            AssingUrlToIFrame(courseid);
        });        
    </script>
</asp:Content>
