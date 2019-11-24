<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseDetails.aspx.cs" MasterPageFile="~/Master/AnonymusMaster.master"
    Inherits="CourseDetails" Title="MOOC Academy - Course name" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="SyllbusContainer">
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li><li>
                    <div id="divBreadCrumb" runat="server">Syllabus - Fundamentals Of Computer</div>
                </li>
            </ul>
        </div>
        <div style="float: left; width: 100%" id="frameComtainer">
            <div class="left-navigation" id="chapters" style="float: left; width: 25%">
            </div>
            <div class="left-navigation" id="secions" style="float: left; width: 25%">
                <ul class="left-nav blue">
                    <li class="active">Sections</li>
                </ul>
            </div>
            <div id="introheader" class="content-summary green" style="float: left; width: 46%;
                font-weight: bold; padding: 1%; border-bottom: 1px solid #AAA; border: 1px solid #89C35C;">
                BRIEF OVERVIEW
            </div>
            <div class="content-summary blue" id="chapterIntro" style="float: left; width: 46%;
                padding: 1%; background-color: #fff; color: black; border: 1px solid #ccc; box-shadow: 0 2px 2px #999;
                text-align: justify;">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //BASE_URL = '<%=Util.BASE_URL%>';
        function getQueryStrings() {
            var assoc = {};
            var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
            var queryString = location.search.substring(1);
            var keyValues = queryString.split('&');

            for (var i in keyValues) {
                var key = keyValues[i].split('=');
                if (key.length > 1) {
                    assoc[decode(key[0])] = decode(key[1]);
                }
            }

            return assoc;
        }

        var qs = getQueryStrings();

        var courseId = qs["id"];
        if (courseId != undefined && courseId != 0) {
            PopulateChaptersByCourse(courseId, BuilLeftNavigationForSylabus);
        }
        else {
            $("#frameComtainer").html("Sorry! there are some error in this page.");
            $("#frameComtainer").css("font-size", "30px");
            $("#frameComtainer").css("margin-top", "50px");
            $("#frameComtainer").css("text-align", "center");
        }
    </script>
</asp:Content>
