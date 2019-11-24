<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-signin">
        <h2 class="form-signin-heading">
            Please sign in</h2>
        <%--<div id="errorSummary" runat="server" style="color: red; display: none; padding-bottom: 10px;"></div>
            <input id="txtUserName" runat="server" type="text" class="input-block-level" placeholder="Username" />
            <input id="txtPassword" type="password" runat="server" class="input-block-level"
                placeholder="Password" />
            <asp:Button ID="Button1" runat="server"  CssClass="btn btn-large btn-primary"
                Text="Sign in" />--%>
        <div class="form-group">
            <div id="errorSummary" runat="server" style="color: red; display: none; padding-bottom: 10px;">
            </div>
        </div>
        <div class="form-group" style="margin-left: 10%;">
            <label for="exampleInputEmail1">
                UserName</label>
            <input type="text" class="form-control" id="txtUserName" runat="server" placeholder="UserName">
        </div>
        <div class="form-group" style="margin-left: 10%;">
            <label for="exampleInputPassword1">
                Password</label>
            <input type="password" class="form-control" id="txtPassword" runat="server" placeholder="Password">
        </div>
         <asp:Button ID="Button1" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" style="margin-left: 10%;"
                Text="Sign in" />
      
    </div>
</asp:Content>
