<%@ Page Title="Add Online Tool" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="AddUpdateOnlineTools.aspx.cs" Inherits="Admin_AddUpdateOnlineTools" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div style="margin-top: 0%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                <div id="header" runat="server">
                    Add Online Tool
                </div>
            </h3>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            Online tools added successfully
        </div>
        <div class="Record">
            <div class="Column1">
                Select Course
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlCourse" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlCourse" runat="server" ControlToValidate="ddlCourse"
                    InitialValue="0" ForeColor="Red" ErrorMessage="Please select college" ValidationGroup="Search">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Title
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtTitle" runat="server" ControlToValidate="txtTitle"
                    ForeColor="Red" ErrorMessage="" ValidationGroup="Search">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Description
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtDesc" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtDesc" runat="server" ControlToValidate="txtDesc"
                    ForeColor="Red" ErrorMessage="" ValidationGroup="Search">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Tool Logo
            </div>
            <div class="Column2">
                <asp:FileUpload ID="fuLogo" runat="server" />
                <asp:Image ID="imgLogoPre"  runat="server" Height="75" Width="75" Visible="false" />
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Tool URL
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtToolUrl" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Tool Display Date
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtToolDisplayDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender runat="server" ID="ceDisplayDate" TargetControlID="txtToolDisplayDate"
                    PopupButtonID="txtToolDisplayDate" PopupPosition="Right">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvtxtToolDisplayDate" runat="server" ControlToValidate="txtToolDisplayDate"
                    ForeColor="Red" ErrorMessage="" ValidationGroup="Search">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Is Active
            </div>
            <div class="Column2">
                <asp:CheckBox ID="chkIsActive" runat="server" />
            </div>
        </div>
        <div class="Record">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Add Online Tool"
                ValidationGroup="Search" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" PostBackUrl="~/Admin/ManageOnlineTools.aspx" />
        </div>
    </div>
</asp:Content>
