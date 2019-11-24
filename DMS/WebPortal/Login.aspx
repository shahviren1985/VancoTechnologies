<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>DMS - Login</title>
    <link href="static/scripts/bootstrap.min.css" rel="stylesheet" />
    <script src="static/scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }

        .form-signin {
            max-width: 300px;
            padding: 19px 29px 29px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }

            .form-signin .form-signin-heading, .form-signin .checkbox {
                margin-bottom: 10px;
            }

            .form-signin input[type="text"], .form-signin input[type="password"] {
                font-size: 16px;
                height: auto;
                margin-bottom: 15px;
                padding: 7px 9px;
            }
    </style>
</head>
<body>
    <form id="form1" class="form-signin" method="post" runat="server">
        <div>
            <h2 class="form-signin-heading">Please sign in</h2>
            <div id="Error" style="color: red" runat="server"></div>
            <input id="txtUserName" runat="server" type="text" class="input-block-level" placeholder="Username" />
            <input id="txtPassword" type="password" runat="server" class="input-block-level"
                placeholder="Password" />
            <asp:Button ID="Button1" runat="server" OnClick="Login1_Authenticate" CssClass="btn btn-large btn-primary"
                Text="Sign in" />
        </div>
    </form>
</body>
</html>
