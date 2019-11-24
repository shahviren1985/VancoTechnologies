<%@ Page Title="Manage Application Logo" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="AddUpdateAppLogoHeader.aspx.cs" Inherits="Admin_AddUpdateAppLogoHeader" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 5%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Manage Application Logo
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
        color: Red; float: left; width: 100%;" visible="false">
    </div>
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>
    <div class="Record">
        <div class="Column1">
            Select Logo Image
        </div>
        <div class="Column2">
            <asp:FileUpload ID="fuLogo" runat="server" /><br />
            <asp:Image ID="imgUpdateLogo" Width=" 100px" Height="100px" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
        </div>
    </div>
    <div class="Record">
        <div class="Column2">
            <asp:TextBox ID="txtLogoText" TextMode="MultiLine" placeholder="Application Logo Header"
                runat="server" Width="435px"></asp:TextBox>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="lnkCancel" runat="server" CssClass="btn btn-convert" Text="Cancel"
                PostBackUrl="~/Admin/Dashboard.aspx"></asp:Button>
        </div>
    </div>
</asp:Content>
