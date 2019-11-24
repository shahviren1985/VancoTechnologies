<%@ Page Title="Course Summary Report" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="SummaryReport.aspx.cs" Inherits="Admin_SummaryReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Course Summary Report
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" visible="false">
    </div>
    <div class="Record">
        <div id="lodder" style="width: 100%; float: left; text-align: center">
            <img alt="" src="../static/images/loading.gif" />
            Loading please wait...
        </div>
        <div class="Column1" style="width: 48%" id="complete">
        </div>
        <div class="Column1" style="width: 48%" id="notStarted">
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var path = "Handler/GetSummaryReportData.ashx";

            CallHandler(path, PopulateSummaryReport);
        });

        function PopulateSummaryReport(result) {
            if (result.Status == "Ok") {
                // populate complete counts
                if (result.CompletedCourse.length > 0) {

                    var record = $("<div>");
                    $(record).attr("class", "Record");
                    $(record).html("<b>Completed the Course</b>");
                    $("#complete").append(record);
                    PopulateCompleteHeaders();
                    for (var i = 0; i < result.CompletedCourse.length; i++) {
                        PopulateCompleteCount(result.CompletedCourse[i]);
                    }
                }
                else
                { $("#lodder").html("Not record found"); }

                // populate not started counts
                if (result.NotStartedCourse.length > 0) {

                    var record = $("<div>");
                    $(record).attr("class", "Record");
                    $(record).html("<b>Not-Started the Course</b>");
                    $("#notStarted").append(record);
                    PopulateNotStartedHeaders();
                    for (var j = 0; j < result.NotStartedCourse.length; j++) {
                        PopulateNotStartedCount(result.NotStartedCourse[j]);
                    }
                }
                else
                { $("#lodder").html("Not record found"); }

                $("#lodder").css("display", "none");
            }
            else if (result.Status == "Error") {
                if (result.Message == "Session Expire") {
                    alert("Your Session is Expire you are redirect to login page.");
                    parent.document.location = BASE_URL + "Login.aspx";
                }
                else {
                    //alert(result.Message);
                    $("#lodder").html(result.Message);
                }
            }
        }

        function PopulateCompleteHeaders() {
            var record = $("<div>");
            $(record).attr("class", "Record");

            var column1 = $("<div>");
            $(column1).attr("class", "Column1");
            $(column1).css("width", "38%")
            $(column1).html("<b><u>Course</u></b>");


            var column2 = $("<div>");
            $(column2).attr("class", "Column2");
            $(column2).html("<b><u>Count (" + GetDate() + ")</u></b>");

            $(record).append(column1);
            $(record).append(column2);

            $("#complete").append(record);
        }

        function PopulateNotStartedHeaders() {
            var record = $("<div>");
            $(record).attr("class", "Record");

            var column1 = $("<div>");
            $(column1).attr("class", "Column1");
            $(column1).css("width", "38%")
            $(column1).html("<b><u>Course</u></b>");


            var column2 = $("<div>");
            $(column2).attr("class", "Column2");
            $(column2).html("<b><u>Count (" + GetDate() + ")</u></b>");

            $(record).append(column1);
            $(record).append(column2);

            $("#notStarted").append(record);
        }

        function PopulateCompleteCount(obj) {
            var record = $("<div>");
            $(record).attr("class", "Record");

            var column1 = $("<div>");
            $(column1).attr("class", "Column1");
            $(column1).css("width", "38%")
            $(column1).html(obj.Course);


            var column2 = $("<div>");
            $(column2).attr("class", "Column2");
            $(column2).html(obj.NumberOfStudents);

            if (obj.Course == "Total") {
                $(column1).html("<b>" + obj.Course + "<b/>");
                $(column2).html("<b>" + obj.NumberOfStudents + "</b>");
            }

            $(record).append(column1);
            $(record).append(column2);

            $("#complete").append(record);
        }

        function PopulateNotStartedCount(obj) {
            var record = $("<div>");
            $(record).attr("class", "Record");

            var column1 = $("<div>");
            $(column1).attr("class", "Column1");
            $(column1).css("width", "38%")
            $(column1).html(obj.Course);


            var column2 = $("<div>");
            $(column2).attr("class", "Column2");
            $(column2).html(obj.NumberOfStudents);

            if (obj.Course == "Total") {
                $(column1).html("<b>" + obj.Course + "<b/>");
                $(column2).html("<b>" + obj.NumberOfStudents + "</b>");
            }

            $(record).append(column1);
            $(record).append(column2);

            $("#notStarted").append(record);
        }

        function GetDate() {

            var month = new Array();
            month[0] = "January";
            month[1] = "February";
            month[2] = "March";
            month[3] = "April";
            month[4] = "May";
            month[5] = "June";
            month[6] = "July";
            month[7] = "August";
            month[8] = "September";
            month[9] = "October";
            month[10] = "November";
            month[11] = "December";

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!`

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm }
            var today = dd + '-' + month[today.getMonth()] + '-' + yyyy;
            return today;
        }
    </script>
</asp:Content>
