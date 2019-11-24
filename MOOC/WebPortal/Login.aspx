<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <title>MOOC Academy - Login</title>
    <link href="static/bootstrap.min.css" rel="stylesheet" />
    <link href="static/styles/style.css" rel="stylesheet" />
    <link rel="Stylesheet" href="static/styles/alice-min.1384336085.css" />
    <script src="static/scripts/jquery-1.9.1.js"></script>
    <script src="static/scripts/AA.core.js"></script>
    <style type="text/css">
        body
        {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
            color: #333;
        }
        
        .form-signin
        {
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
        .form-signin .form-signin-heading, .form-signin .checkbox
        {
            margin-bottom: 10px;
        }
        .form-signin input[type="text"], .form-signin input[type="password"]
        {
            font-size: 16px;
            height: auto;
            margin-bottom: 15px;
            padding: 7px 9px;
        }
        
        #ql_List input[type="text"]
        {
            font-size: 14px;
            padding: 4px 6px;
            margin-bottom: 3%;
        }
        
        @media only screen and (max-width:240px)
        {
            #ql_SideBar
            {
                visibility: hidden !important;
            }
        }
    </style>
</head>
<body>
    <div class="form-signin" style="background-color: pink; text-align: justify; font-weight: bold; display:none"
        visible="true">
        <div style="float: left; margin-left: -8%; margin-right: 3%;">
            <img src="static/images/info_blue.png" alt="" width="50px" /></div>
        Currently website is under maintenance. We will be back in few hours. Thank you
        for using MOOC Academy.
    </div>
    <form id="form1" method="post" runat="server" class="form-signin">
    <div>
        <div class="header">
            <div class="logo" style="margin-bottom: 5%;">
                <img id="logoImage" style="border-radius: 4px;" src="static/images/logo.png" />
            </div>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 94%;" visible="false">
        </div>
        <%--<h2 class="form-signin-heading">
            Please sign in</h2>--%>
        <input id="txtUserName" runat="server" type="text" class="input-block-level" placeholder="Username" />
        <input id="txtPassword" type="password" runat="server" class="input-block-level"
            placeholder="Password" />
        <asp:Button ID="Button1" runat="server" OnClick="Login1_Authenticate" CssClass="btn btn-large btn-primary"
            Text="Sign in" Visible="true" />
        <!-- May I help you section -->
        <div>
            <div id="ql_SideBar">
                <div title="Click here for any help" id="ql_Display" onclick="showMayIHelpYouWindow();"
                    style="background-color: #404040 !important; color: White; background-image: none;
                    border-top-left-radius: 3px; border-top-right-radius: 3px;">
                    <span><span id="ql_ShortlistHeadline" class="arrowUp rfloat"></span>
                        <img id="imgOnlineStatus" style="border-radius: 10px;">May I help you?<span id="ql_Counter"></span>
                    </span>
                </div>
                <div id="ql_Tab" style="display: none">
                    <div id="ql_List" style="text-align: center">
                        <input type="text" id="txtName" placeholder="Full Name" style="min-height: 30px;" /><span
                            style="color: Red">*</span>
                        <input type="text" id="txtMobile" placeholder="Mobile Number" style="min-height: 30px;" /><span
                            style="color: Red">*</span>
                        <input type="text" id="txtEmail" placeholder="Email Address" style="min-height: 30px;" />
                        <textarea id="txtQuery" placeholder="Your question" cols="20" rows="2" style="max-height: 100px;
                            margin-bottom: 3%; margin-left: 2%; padding: 6px; font-size: 14px;"></textarea>
                        <span style="color: Red">*</span>
                        <div id="buttons" style="text-align: left; margin-left: 7%;">
                            <input type="button" id="btnSubmit" value="Submit" class="btn btn-success" onclick="SaveMayIHelpYouQuery();" />
                            <input type="button" id="btnClose" value="Close" class="btn btn-convert" onclick="showMayIHelpYouWindow();"
                                style="text-align: left" />
                        </div>
                        <div id="queryStatus" style="color: Green">
                        </div>
                        <div id="ql_EmptyText">
                        </div>
                    </div>
                    <div id="ql_Foot" style="text-align: justify; height: 0%; text-align: left;">
                        <%-- Note: You will receive call between 10 AM to 7 PM in week days (Monday to Saturday)--%></div>
                </div>
            </div>
        </div>
        <!-- End -->
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //BASE_URL = '<%=Util.BASE_URL %>';
    //alert(BASE_URL);
    var hostName = document.location.hostname;
    if (hostName == "moocacademy." || hostName == "www.moocacademy.") {
        document.location = "../login.aspx";
    }

    var interval = setInterval(function () {
        var loggedin = '<%=Application["IsAdminLoggedId"]%>';
        if (loggedin == 'True') {
            $("#imgOnlineStatus").attr("src", "static/images/status-online.png");
        }
        else {
            $("#imgOnlineStatus").attr("src", "static/images/status-offline.png");
        }
    }, 1000);

</script>
