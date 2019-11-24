<%@ Page Title="Graphical Test Performance Report" Language="C#" MasterPageFile="~/Master/StaffMasterPage.master"
    AutoEventWireup="true" CodeFile="GraphicalTestPerformance.aspx.cs" Inherits="Staff_GraphicalTestPerformance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script type="text/javascript" src="../static/scripts/Chart.js"></script>
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Graphical Test Performance Report
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" visible="false">
    </div>
    <div class="Record">
        <div class="Column1">
            Select Stream
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlCourse" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select course" ForeColor="Red" ValidationGroup="Gen"
                ControlToValidate="ddlCourse" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            Select Test
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlTests" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select test" ForeColor="Red" ValidationGroup="Gen"
                ControlToValidate="ddlTests" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record" id="divGenerateButton" runat="server">
        <div class="Column2">
            <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Generate" OnClick="btnGenerate_Click" ValidationGroup="Gen"/>
            <asp:Button ID="btnCancle" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Staff/Dashboard.aspx" />
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            <canvas id="canvas" height="300" width="300"></canvas>
        </div>
    </div>
    <div style="width: 98%; float: left; display: none;" id="summary">
        <div id="total" class="complete-percent" style="background-color: #eee;">
            25
        </div>
        <div id="completePer" class="complete-percent">
            25
        </div>
        <div id="runnig" class="runnig-percent">
            45
        </div>
        <div id="pendingPer" class="pending-percent">
            30
        </div>
    </div>
    <asp:HiddenField ID="hfChartData" Value="[]" runat="server" />
    <asp:HiddenField ID="hfSummary" Value="[]" runat="server" />
    <style>
        .complete-percent, .runnig-percent, .pending-percent
        {
            width: 10%;
            float: left;
            margin-right: 2%;
            color: #666;
            border-radius: 5px;
            padding: 10px;
            background-color: #7FFF00;
        }
        .runnig-percent
        {
            background-color: #69D2E7;
        }
        
        .pending-percent
        {
            background-color: #F7464A;
            color: #fff;
        }
    </style>
    <script type="text/javascript">
        function PopulateGraph() {
            var dbCourseCompChartData = [];
            dbCourseCompChartData = JSON.parse($("#Content_hfChartData").val());
            var myDoughnut = new Chart(document.getElementById("canvas").getContext("2d")).Pie(dbCourseCompChartData);

            var summary = [];
            summary = JSON.parse($("#Content_hfSummary").val());
            $("#total").html(summary.Total + " Student(s)");
            $("#completePer").html(summary.Completed + " Completed");
            $("#runnig").html(summary.Running + " In-progress");
            $("#pendingPer").html(summary.NotStarted + " Not Started");

            $("#summary").show();
        }
    </script>
</asp:Content>
