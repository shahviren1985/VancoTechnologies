<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Admin_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: Manage Users</title>
    <link href="../static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="../static/stylesheets/jquery.tagit.css" rel="stylesheet" type="text/css" />
    <link href="../static/stylesheets/tagit.ui-zendesk.css" rel="stylesheet" type="text/css" />
    <link href="../static/stylesheets/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../static/scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../static/scripts/tag-it.js" type="text/javascript"></script>
    <script src="../static/scripts/controls/aa.controls.js" type="text/javascript"></script>
    <script src="../static/scripts/controls/aa.communicate.js" type="text/javascript"></script>

    <style type="text/css">
        .form-container {
            margin: 20px;
            margin-left: 10%;
        }

        #add-user, #update-user, .active-user {
            margin-right: 10px !important;
            margin-top: -5px;
        }

        .grid {
            margin-top: 20px;
            display: none;
        }

        .form {
            margin-top: 20px;
        }

        #department {
            margin: 0px;
            margin-top: 10px;
        }

        ul.tagit {
            width: 60%;
        }

        #radioContainer input[type=radio] {
            float: left;
        }

        #radioContainer label {
            margin-left: 20px;
        }
    </style>
    <style type="text/css">
        .form-container {
            margin: 20px;
            margin-left: 10%;
        }

        #add-user, #update-user, .active-user {
            margin-right: 10px !important;
            margin-top: -5px;
        }

        .grid {
            margin-top: 20px;
            display: none;
        }

        .form {
            margin-top: 20px;
        }

        #department {
            margin: 0px;
            margin-top: 10px;
        }

        ul.tagit {
            width: 60%;
        }
    </style>
    <%--<script>
        $(function () {
            $("#ContentPlaceHolder1_txtStartDate").datepicker({ dateFormat: "dd-mm-yy" });
            $("#ContentPlaceHolder1_txtEndDate").datepicker({ dateFormat: "dd-mm-yy" });
        });

        $(document).ready(function () {

            $("#ContentPlaceHolder1_txtStartDate").attr("readonly", "readonly").attr("style", "cursor: auto;background-color: #fff;");
            $("#ContentPlaceHolder1_txtEndDate").attr("readonly", "readonly").attr("style", "cursor: auto;background-color: #fff;");

            $("#ContentPlaceHolder1_btnGenerate").click(function () {
                if ($("#ContentPlaceHolder1_txtStartDate").val() == "") {
                    alert("Please select start date");
                    return false;
                }

                if ($("#ContentPlaceHolder1_txtEndDate").val() == "") {
                    alert("Please select end date");
                    return false;
                }

                var sd = $("#ContentPlaceHolder1_txtStartDate").val();
                var ed = $("#ContentPlaceHolder1_txtEndDate").val();

                var startDate = new Date(sd.split('-')[2], sd.split('-')[1], sd.split('-')[0]);
                var endDate = new Date(ed.split('-')[2], ed.split('-')[1], ed.split('-')[0]);

                if (endDate < startDate) {
                    alert("End date should be greatter then start date");
                    $("#ContentPlaceHolder1_txtEndDate").val("");
                    $("#ContentPlaceHolder1_txtEndDate").focus();
                    return false;
                }

                return true;
            });
        });

    </script>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-container" style="width: 50%; float: left;">
        <h3>Documets Reports</h3>
        <div class="record" id="radioContainer">
            <div id="inward-container" class="radio-container">
                <asp:RadioButton ID="rbInward" CssClass="radio-text" Checked="true" runat="server" Text="Inward Date" GroupName="Ward" />
            </div>
            <div id="outward-container" class="radio-container">
                <asp:RadioButton ID="rbOutward" CssClass="radio-text" runat="server" Text="Outward Date" GroupName="Ward" />
            </div>
            <div id="received-date-container" class="radio-container">
                <asp:RadioButton ID="rbReceivedDate" CssClass="radio-text" runat="server" Text="Received Date" GroupName="Ward" />
            </div>
        </div>
        <div class="record">
            <div class="column1">
                <asp:TextBox runat="server" ID="txtStartDate" TextMode="Date" placeholder="Start date"></asp:TextBox>
            </div>
            <div class="column1">
                <asp:TextBox runat="server" ID="txtEndDate" TextMode="Date" placeholder="End date"></asp:TextBox>
            </div>
        </div>
        <div class="record">
            <asp:Button runat="server" ID="btnGenerate" Text="Generate Report" CssClass="btn btn-success" OnClick="btnGenerate_Click" />
            <asp:Button runat="server" ID="btnGenerateInward" Text="Generate Inward Report" CssClass="btn btn-success" OnClick="btnGenerateInward_Click" />
        </div>
        <div id="outputLink" runat="server" class="record">
        </div>
    </div>
</asp:Content>
