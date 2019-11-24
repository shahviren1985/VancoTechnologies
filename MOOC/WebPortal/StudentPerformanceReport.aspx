<%@ Page Language="C#" Title="MOOC Academy - Student Performance Report" AutoEventWireup="true"
    CodeFile="StudentPerformanceReport.aspx.cs" MasterPageFile="~/Master/MasterPage.master"
    Inherits="StudentPerformanceReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="content-container" style="margin-left: 8%; padding: 10px; width: 83%;">
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li><li>
                    Performance Report</li></ul>
        </div>
        <div class="ChartContainer">
            <div id="HeadingDiv1" class="HeadingDiv">
                <div class="SubHead3" style="margin-left: 5%">
                    Chapter-wise Performance Graph
                </div>
            </div>
            <div id="dataContainerReport" class="box box-orange">
                <canvas id="chapterWisePermChart" width="900" height="400"></canvas>
            </div>
        </div>
        <div class="ChartContainer" style="margin-top: 2%;">
            <div id="HeadingDiv2" class="HeadingDiv">
                <div class="SubHead1">
                    Estimated Time
                </div>
                <div class="SubHead2">
                    Vs</div>
                <div class="SubHead3">
                    Time Taken
                </div>
            </div>
            <div id="Div1" class="box box-orange">
                <canvas id="timeEstimateChart" width="900" height="400"></canvas>
            </div>
        </div>
    </div>
    <div id="hfContainer">
        <asp:HiddenField ID="hfChapterwisePer" runat="server" />
        <asp:HiddenField ID="hfCourseCompletion" runat="server" />
        <asp:HiddenField ID="hfTimeEstimated" runat="server" />
        <asp:HiddenField ID="hfUserActivities" runat="server" />
    </div>
    <script type="text/javascript">

        var drop = $("#drop");
        $(drop).css("display", "none");

        //function PopulateCourseName() {
        var drop = $("#label");
        $(drop).css("display", "block");
        $(drop).html("You are learning - Fundamental of Computers");

        $(document).ready(function () {
            $("#timeEstimateChart").css("width", "98%");
            $("#chapterWisePermChart").css("width", "98%");
        });

        function PopulateChapterWisePerformance() {
            try {
                //creating Bar chart for time taken by student
                var dbTimeEstimateChartData = [];
                dbTimeEstimateChartData = JSON.parse($("#Content_hfTimeEstimated").val());
                var myBarLine = new Chart(document.getElementById("timeEstimateChart").getContext("2d")).Bar(dbTimeEstimateChartData);

                //creating Line chart for Chapterwise performance
                var lineTypingStatsChartData = [];
                lineTypingStatsChartData = JSON.parse($("#Content_hfChapterwisePer").val());
                var myLine = new Chart(document.getElementById("chapterWisePermChart").getContext("2d")).Line(lineTypingStatsChartData);

            } catch (e) {

            }
        }
    </script>
</asp:Content>
