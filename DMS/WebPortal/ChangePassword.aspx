<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: Change Password</title>
    <link href="static/stylesheets/reset.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.css" rel="stylesheet" />
    <link href="static/stylesheets/style.css" rel="stylesheet" />
    <link href="static/stylesheets/aa.group.css" rel="stylesheet" />
    <script src="static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.dashboard.js" type="text/javascript"></script>

    <style type="text/css">
        .form-container {
            margin: 20px;
            width: 90%;
            float: left;
            text-align: left;
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-container" style="width: 35%; float: left;">
        <h3>Change Password</h3>
        <div id="error" runat="server" style="display: none" class="alert alert-danger">
            
        </div>

        <div class="record">
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" runat="server" ForeColor="Red" ValidationGroup="Change">Please enter password</asp:RequiredFieldValidator>
        </div>
        <div class="record">
            <asp:TextBox ID="txtNewPassword" runat="server" placeholder="New Password" TextMode="Password" ValidationGroup="Change"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNewPassword" ForeColor="Red" runat="server" ValidationGroup="Change">Please enter new password</asp:RequiredFieldValidator>
        </div>
        <div class="record">
            <asp:TextBox ID="txtReEnterPassword" runat="server" placeholder="Re-type Password" TextMode="Password" ValidationGroup="Change"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtReEnterPassword" ForeColor="Red" runat="server" ValidationGroup="Change">Please re-type new password</asp:RequiredFieldValidator>
        </div>
        <div class="record">
            <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-success" ValidationGroup="Change" OnClick="btnChangePassword_Click"/>
        </div>
    </div>
</asp:Content>
