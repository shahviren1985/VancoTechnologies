<%@ Page Title="MOOC Academy :: Courses" Language="C#" MasterPageFile="~/Master/AnonymusMaster.master"
    AutoEventWireup="true" CodeFile="ViewContent.aspx.cs" Inherits="ViewContent" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="frameComtainer" style="float: left; width: 100%; text-align: center;">
        <iframe id="ifHintContent" style="width: 80%; min-height: 1100px; border: none;">
        </iframe>
    </div>
    <script type="text/javascript">
        var chapterId;
        var sectionId;
        var courseId;
        $(document).ready(function () {
            //BASE_URL = '<%=Util.BASE_URL%>';
            getQueryStrings();
            var qs = getQueryStrings();
            chapterId = qs["chapterId"];
            sectionId = qs["sectionId"];
            courseId = qs["courseid"];

            if (chapterId == undefined || sectionId == undefined || courseId == undefined) {
                $("#frameComtainer").html("Sorry! there are some error in this page.");
                $("#frameComtainer").css("font-size", "30px");
                $("#frameComtainer").css("margin-top", "50px");
            }
            else {
                PopulateChaptersByCourse(courseId, callAssignUrlToHintPage);
                //AssignUrlToHintPage(chapterId, sectionId);
            }
        });

        function callAssignUrlToHintPage() {
            AssignUrlToHintPage(chapterId, sectionId, courseId);
        }

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

        //hideButtons_Questions();

        function hideButtons_Questions() {
            alert(1);
            $(".content-navigation").hide();
        }    
    </script>
</asp:Content>
