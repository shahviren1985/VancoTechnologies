<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTest.aspx.cs" Inherits="Staff_ManageTest"
    MasterPageFile="~/Master/StaffMasterPage.master" Title="Manage Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="pageContentHolder" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var selected_tab = 1;
        $(function () {
            var tabs = $("#tabs").tabs({
                select: function (e, i) {
                    selected_tab = i.index;
                }
            });
            selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
            tabs.tabs('select', selected_tab);
            $("form").submit(function () {
                $("[id$=selected_tab]").val(selected_tab);
            });
        });
   
    </script>
    <div class="Record">
        <div>
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                <div id="header" runat="server">
                    Manage Test</div>
            </h3>
        </div>
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li>
                    <div id="bread" runat="server">
                        Manage Test</div>
                </li>
            </ul>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            float: left; width: 100%;" visible="false">
        </div>
        <div class="Record">
            <div class="Column1">
                Select Course
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlCourse" runat="server" Width="220px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlCourse" runat="server" ErrorMessage="Please select course.">*
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="tabs" style="margin-top: 7%;">
            <ul style="padding-top: 2%;">
                <li><a href="#tabs-1">Create New Test</a></li>
                <li><a href="#tabs-2">Tests in-progress</a></li>
                <li><a href="#tabs-3">Completed Tests</a></li>
            </ul>
            <div id="tabs-1" style="height: 450px;">
                <div class="Record">
                    <div class="Column1">
                        Test Name
                    </div>
                    <div class="Column2">
                        <asp:TextBox ID="txtTestName" runat="server" placeholder="Test Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="txtTestName" runat="server" ErrorMessage="Please enter test-name.">*
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        Chapters
                    </div>
                    <div class="Column2">
                        <asp:ListBox ID="lbChapters" runat="server" SelectionMode="Multiple"></asp:ListBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="" ForeColor="Red"
                            ValidationGroup="VGSubmit" ControlToValidate="lbChapters" runat="server" ErrorMessage="Please select chapters.">*
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        Time Bound
                    </div>
                    <div class="Column2">
                        <asp:CheckBox ID="chkTimeBound" runat="server" onclick="ShowHideTextBox(this);" Text="Yes/No" />
                    </div>
                </div>
                <div class="Record" id="timediv" style="display: none;" runat="server">
                    <div class="Column1">
                        Time Limit
                    </div>
                    <div class="Column2">
                        <asp:TextBox ID="txtTimeLimit" runat="server" placeholder="Time Limit"></asp:TextBox>
                        <asp:CompareValidator ID="cvTimeLimit" runat="server" ErrorMessage="Please enter numeric value"
                            ForeColor="Red" ValidationGroup="VGSubmit" ControlToValidate="txtTimeLimit" Type="Integer"
                            Operator="DataTypeCheck">*</asp:CompareValidator>
                        (in minutes)
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        Number of Questions
                    </div>
                    <div class="Column2">
                        <asp:TextBox ID="txtNoOfQuestion" runat="server" placeholder="No. of Questions"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="txtNoOfQuestion" runat="server" ErrorMessage="Please enter number of questions.">*
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvNoofQuestions" runat="server" ErrorMessage="Please enter numeric value"
                            ForeColor="Red" ValidationGroup="VGSubmit" ControlToValidate="txtNoOfQuestion"
                            Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        Status
                    </div>
                    <div class="Column2">
                        <asp:CheckBox ID="chkStatus" runat="server" Text="Active/Inactive" />
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        Start Date
                    </div>
                    <div class="Column2">
                        <asp:TextBox ID="txtStartDate" runat="server" placeholder="Start Date"></asp:TextBox>
                        <cc1:CalendarExtender runat="server" ID="ceStartDate" TargetControlID="txtStartDate"
                            PopupPosition="Right" PopupButtonID="txtStartDate">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="txtStartDate" runat="server" ErrorMessage="Please enter start date.">*
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        End Date
                    </div>
                    <div class="Column2">
                        <asp:TextBox ID="txtEndDate" runat="server" placeholder="End Date"></asp:TextBox>
                        <cc1:CalendarExtender runat="server" ID="ceEndDate" TargetControlID="txtEndDate"
                            PopupButtonID="txtEndDate" PopupPosition="Right">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="txtEndDate" runat="server" ErrorMessage="Please enter end date.">*
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click"
                            ValidationGroup="VGSubmit" />
                        <asp:Button ID="btnCancel" PostBackUrl="~/Staff/TestDetails.aspx" CssClass="btn"
                            runat="server" Text="Cancel" />
                    </div>
                </div>
            </div>
            <div id="tabs-2">
                Tests in-progress
            </div>
            <div id="tabs-3">
                Completed Tests
            </div>
        </div>
        <asp:HiddenField ID="selected_tab" runat="server" />
    </div>
    <style>
        .Record input[type="checkbox"]
        {
            float: left;
            margin-right: 10px;
        }
        
        .CustomTabStyle .ajax__tab_header
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            background: url(images/tab-line.gif) repeat-x bottom;
        }
    </style>
    <script>
        function ShowHideTextBox(chk) {
            if ($(chk).attr("checked") == "checked") {
                $("#Content_timediv").css("display", "block");
            }
            else {
                $("#Content_timediv").css("display", "none");
            }
        }
    </script>
</asp:Content>
