<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MOOC Academy :: Error</title>
    <link rel="Stylesheet" href="static/styles/chapter-style.css" />
    <link rel="Stylesheet" href="static/bootstrap.min.css" />
    <script src="static/scripts/jquery-1.9.1.js"></script>
    <script src="static/scripts/AA.core.js"></script>
    <script type="text/javascript" src="static/scripts/bootstrap.min.js"></script>
    <link rel="Stylesheet" href="static/styles/alice-min.1384336085.css" />
    <style>
        a
        {
            color: #08c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="page">
            <div class="header-container">
                <div class="top-navigation">
                    <div id="currentCourse" style="float: left; color: white; padding-top: 7px; padding-left: 10%;
                        position: absolute; z-index: 1100;">
                        <div id="label" style="display: none;">
                            You are learning - FOC
                        </div>
                    </div>
                    <ul class="top-nav">
                        <li class="dropdown top"><a href="#" id="drop2" role="button" class="dropdown-toggle top"
                            data-toggle="dropdown">Setting <b class="icon-white  caret"></b></a>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="drop2">
                                <li role="presentation">
                                    <asp:LinkButton runat="server" ID="lbLogout" Text="Logout" OnClick="lbLogout_Click"></asp:LinkButton></li>
                            </ul>
                        </li>
                        <li class="top"><a href="Dashboard.aspx" class="top" id="homeLink">Home</a></li>
                        <li class="user-name top">Welcome,
                            <%= Session["Name"] == null ? "" : Session["Name"].ToString().ToUpper()%></li>
                    </ul>
                </div>
                <div class="header">
                    <div class="logo">
                        <asp:Image ID="logoImage" runat="server" Style="height: 100%; border-radius: 4px;"
                            ImageUrl="~/static/images/logo.png" />
                    </div>
                    <div class="college-name" runat="server" id="divLogoHeader">
                    </div>
                    <div class="search-container" style="display: none">
                        <input type="text" class="simplebox" placeholder="Search..." style="min-height: 30px"
                            id="txtSearch" onkeypress="SearchKeyPress(event);" />
                        <div class="btn btn-success" onclick="Search();">
                            Go</div>
                    </div>
                </div>
            </div>
            <div class="content-container" style="margin-left: 10%; width: 80%; padding: 0%;
                font-size: 22px; line-height: 30px;">
                An error has occurred while accessing your account. Please try again after some time. You can contact us and report this issue by using May I help you section at bottom right corner. Thank you.
            </div>
            <!-- May I help you section -->
            <div>
                <div id="ql_SideBar" style="filter: none;">
                    <div title="Click here for any help" id="ql_Display" onclick="showMayIHelpYouWindow();"
                        style="background-color: #404040 !important; color: White; background-image: none;
                        border-top-left-radius: 3px; border-top-right-radius: 3px;">
                        <span><span id="ql_ShortlistHeadline" class="arrowUp rfloat"></span>
                            <img id="imgOnlineStatus" style="border-radius: 10px;">
                            May I help you? <span id="ql_Counter"></span></span>
                    </div>
                    <div id="ql_Tab" style="display: none">
                        <div id="ql_List" style="text-align: center">
                            <input type="text" id="txtName" placeholder="Full Name" style="min-height: 30px;" /><span
                                style="color: Red">*</span>
                            <input type="text" id="txtMobile" placeholder="Mobile Number" style="min-height: 30px;" /><span
                                style="color: Red">*</span>
                            <input type="text" id="txtEmail" placeholder="Email Address" style="min-height: 30px;" />
                            <textarea id="txtQuery" placeholder="Your question" cols="20" rows="2" style="max-height: 100px;
                                margin-bottom: 3%; margin-left: 2%; padding: 6px;"></textarea>
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
    </div>
    </form>
</body>
</html>
