<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" MasterPageFile="~/Master/AdminMasterPage.master"
    Inherits="Admin_AddUser" Title="Add Students" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Add New User
            </h3>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            New User Added Succefully.
        </div>
        <asp:HiddenField ID="hfCounter" Value="0" runat="server" />
        <asp:HiddenField ID="hfSectionJson" Value="" runat="server" />
        <asp:HiddenField ID="hfSelectedChapterFileName" Value="" runat="server" />
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtRollNumber" Width="170px" runat="server" placeholder="Roll Number"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtFirstName" Width="170px" runat="server" placeholder="First Name"></asp:TextBox>
                <span style="color: Red;">*</span>
                <asp:RequiredFieldValidator ID="rvftxtFirstName" ForeColor="Red" ValidationGroup="VGSubmit"
                    ControlToValidate="txtFirstName" runat="server" ErrorMessage="Please provide first name!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtLastName" Width="170px" runat="server" placeholder="Last Name"></asp:TextBox>
                <span style="color: Red;">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtLastName" ForeColor="Red" ValidationGroup="VGSubmit"
                    ControlToValidate="txtLastName" runat="server" ErrorMessage="Please provide last name!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtFather" Width="170px" runat="server" placeholder="Father Name"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtMother" Width="170px" runat="server" placeholder="Mother Name"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtEmailAddress" Width="170px" Placeholder="Email Address" MaxLength="255"
                    runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                    ValidationGroup="VGSubmit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtMobile" Width="170px" Placeholder="Mobile No" MaxLength="10"
                    runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator"
                     ValidationGroup="VGSubmit" Operator="DataTypeCheck" ControlToValidate="txtMobile"
                    Type="Integer">*</asp:CompareValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:DropDownList ID="ddlUserType" runat="server" Width="182px">
                    <asp:ListItem Text="--Select User Type--" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="SuperAdmin" Value="SuperAdmin"></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                    <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                    <asp:ListItem Text="User" Value="User"></asp:ListItem>
                </asp:DropDownList>
                <span style="color: Red;">*</span>
                <asp:RequiredFieldValidator ID="rfvddlUserType" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlUserType" runat="server" ErrorMessage="Please provide user type!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtUserName" Width="170px" Placeholder="User Name" MaxLength="255"
                    runat="server"></asp:TextBox>
                <span style="color: Red;">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtUserName" ForeColor="Red" ValidationGroup="VGSubmit"
                    ControlToValidate="txtUserName" runat="server" ErrorMessage="Please Provide User Name!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:TextBox ID="txtPassword" Width="170px" Placeholder="Password" MaxLength="255"
                    TextMode="Password" runat="server"></asp:TextBox>
                <span style="color: Red;">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtPassword" ForeColor="Red" ValidationGroup="VGSubmit"
                    ControlToValidate="txtPassword" runat="server" ErrorMessage="Please Provide Password!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1" style="width: 200px;">
                Is User Active?
            </div>
            <div class="Column2" style="width: 60%;">
                <asp:CheckBox ID="chkActive" runat="server" />
            </div>
        </div>
        <div class="Record">
            <div class="Column1" style="width: 200px;">
                Is User Enabled?
            </div>
            <div class="Column2" style="width: 60%;">
                <asp:CheckBox ID="chkEnabled" runat="server" />
            </div>
        </div>
        <%--<div class="Record">
            <div class="Column1" style="width: 200px;">
                Is Completed?
            </div>
            <div class="Column2" style="width: 60%;">
                <asp:CheckBox ID="chkCompleted" runat="server" />
            </div>
        </div>
        <div class="Record">
            <div class="Column1" style="width: 200px;">
                Is Certified?
            </div>
            <div class="Column2" style="width: 60%;">
                <asp:CheckBox ID="chkCertified" runat="server" />
            </div>
        </div>--%>
        <div class="Record">
            <div class="Column2">
                <asp:Button ID="btnCreateUser" runat="server" CssClass="btn btn-success" CausesValidation="true"
                    ValidationGroup="VGSubmit" Text="Create User" OnClick="btnCreateUser_Click" />
                <asp:Button ID="btnClear" runat="server" CssClass="btn convert" Text="Clear" OnClick="btnClear_Click" />
            </div>
        </div>
    </div>
</asp:Content>
