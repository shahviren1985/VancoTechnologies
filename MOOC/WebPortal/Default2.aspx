<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>MOOC Academy</title>
    <meta name="description" content="MOOC Academy - E-learning platform to create, conduct the online courses, track student performance, generate reports and award certificates">
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" href="static/styles/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="static/styles/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="static/styles/bootstrap-responsive.min.css" type="text/css" />
    <link rel="stylesheet" id="rgs-css" href="static/styles/rgs.css" type="text/css"
        media="all" />
    <link rel="stylesheet" href="static/styles/style-index.css" type="text/css" />
    <link rel="stylesheet" href="static/styles/demo.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="static/styles/flexslider.css" type="text/css" media="screen" />
    <link rel="Stylesheet" href="static/styles/alice-min.1384336085.css" />
    <link rel="stylesheet" href="static/styles/mooc.mobile.version.css" type="text/css" />
    <script defer src="static/scripts/jquery.flexslider.js"></script>
    <script type="text/javascript" src="static/scripts/jquery.js"></script>
    <script type="text/javascript" src="static/scripts/prettyPhoto.js"></script>
    <script type="text/javascript" src="static/scripts/init.js"></script>
    <script type="text/javascript" src="static/scripts/main.js"></script>
    <script type="text/javascript" src="static/scripts/AA.core.js"></script>

    <script type="text/javascript">
        $(window).load(function () {
            $('.flexslider').flexslider({
                animation: "slide",
                start: function (slider) {
                    $('body').removeClass('loading');
                }
            });
        });

        //GetEarlyAccess
        function ShowGetEarlyAccessPopup() {
            $("#getEarlyAccessModelBack").show(500);
            $('#getEarlyAccess').attr("style", "opacity: 1 !important;top: 50%");
            $("#txtGEAFirstName").focus();
        }

        function HideGetEarlyAccessPopup() {
            $('#getEarlyAccess').fadeOut(100);
            $("#getEarlyAccessModelBack").hide(500);

            $("#getEarlyAccessStatus").html("");
            $("#getEarlyAccessStatus").attr("style", "display:none");

            $("#txtGEAFirstName").val("");
            $("#txtGEALastName").val("");
            $("#txtGEAEmail").val("");
        }

        //Schedule a demonstrations
        function ShowScheduleDemoPopup() {
            $("#getEarlyAccessModelBack").show(500);
            $('#scheduleDemo').attr("style", "opacity: 1 !important;top: 50%");
            $("#txtSDFirstName").focus();
        }

        function HideScheduleDemoPopup() {
            $('#scheduleDemo').fadeOut(100);
            $("#getEarlyAccessModelBack").hide(500);

            $("#scheduleDemoStatus").html("");
            $("#scheduleDemoStatus").attr("style", "display:none");

            $("#txtSDFirstName").val("");
            $("#txtSDLastName").val("");
            $("#txtSDCollegeName").val("");
            $("#txtSDEmail").val("");
            $("#txtCity").val("");
        }

        function ClearGEAData() {
            $("#txtGEAFirstName").val("");
            $("#txtGEALastName").val("");
            $("#txtGEAEmail").val("");
        }

        function ClearSDData() {
            $("#txtSDFirstName").val("");
            $("#txtSDLastName").val("");
            $("#txtSDCollegeName").val("");
            $("#txtSDEmail").val("");
            $("#txtCity").val("");
        }

        function SubmitGetEarlyAccess() {
            $("#getEarlyAccessStatus").html("");
            $("#getEarlyAccessStatus").attr("style", "display:none");

            var fName = $("#txtGEAFirstName").val();
            var lName = $("#txtGEALastName").val();
            var email = $("#txtGEAEmail").val();

            if (fName == "" || lName == "" || email == "") {
                $("#getEarlyAccessStatus").html("All fields are mandatory.");
                $("#getEarlyAccessStatus").attr("style", "display:block;");
                return;
            }
            else if (!validateEmail(email)) {
                $("#getEarlyAccessStatus").html("Please enter valid e-mail address.");
                $("#getEarlyAccessStatus").attr("style", "display:block");
                $("#txtGEAEmail").focus();
            }
            else {
                $("#GEALoadder").attr("style", "display:block");
                $("#GEAButtons").attr("style", "display:none");

                fName = changeSpecialCharsQS(fName);
                lName = changeSpecialCharsQS(lName);
                //alert(fName + " " + lName + " " + email);
                //alert($("#hfEncryptData").val());
                var key = decodeURI($("#hfEncryptData").val());

                key = key.replace("+", "%2B")
                key = key.replace("+", "%2B");
                key = key.replace("+", "%2B");

                //alert(key);

                var path = "Handler/SendGetEarlyAccesAndScheduleDemoMail.ashx?fname=" + fName + "&lname=" + lName + "&email=" + email + "&type=gea&key=" + key;
                CallHandler(path, onSuccessGEA);
            }
        }

        function onSuccessGEA(result) {
            $("#GEALoadder").attr("style", "display:none");
            $("#GEAButtons").attr("style", "display:block");

            if (result.Status == "Ok") {
                $("#getEarlyAccessStatus").attr("style", "display:block;background-color: lightgreen");
                $("#getEarlyAccessStatus").html("Your request submited successfully. Our team contact you shortly.");

                /*var intverval = setInterval(
                function () {
                $('#getEarlyAccess').fadeOut(100);
                $("#getEarlyAccessStatus").html("");
                $("#getEarlyAccessStatus").attr("style", "display:none");
                $("#getEarlyAccessModelBack").hide(500);
                clearInterval(intverval);
                }, 5000);
                */
                ClearGEAData();
            }
            else if (result.Status == "Error") {
                if (result.Message == "Session Expire") {
                    alert("Your Session is Expire you are redirect to login page.");
                    parent.document.location = BASE_URL + "Login.aspx";
                }
                else {
                    $("#getEarlyAccessStatus").attr("style", "display:block");
                    $("#getEarlyAccessStatus").html("Error: Currently we are unable to receive your early access request. Please try again after some time.");
                }
            }
        }

        function SubmitScheduleDemo() {
            $("#scheduleDemoStatus").html("");
            $("#scheduleDemoStatus").attr("style", "display:none");

            var fName = $("#txtSDFirstName").val();
            var lName = $("#txtSDLastName").val();
            var collegeName = $("#txtSDCollegeName").val();
            var city = $("#txtCity").val();
            var email = $("#txtSDEmail").val();

            if (fName == "" || lName == "" || email == "" || collegeName == "" || city == "") {
                $("#scheduleDemoStatus").html("All fields are mandatory.");
                $("#scheduleDemoStatus").attr("style", "display:block");
                return;
            }
            else if (!validateEmail(email)) {
                $("#scheduleDemoStatus").html("Please enter valid e-mail address.");
                $("#scheduleDemoStatus").attr("style", "display:block");
                $("#txtSDEmail").focus();
            }
            else {
                $("#SDLoadder").attr("style", "display:block");
                $("#SDButtons").attr("style", "display:none");

                fName = changeSpecialCharsQS(fName);
                lName = changeSpecialCharsQS(lName);
                collegeName = changeSpecialCharsQS(collegeName);

                var key = decodeURI($("#hfEncryptData").val());

                key = key.replace("+", "%2B");
                key = key.replace("+", "%2B");
                key = key.replace("+", "%2B");
                //alert(key);

                //alert(fName + " " + lName + " " + email + " " + collegeName);
                var path = "Handler/SendGetEarlyAccesAndScheduleDemoMail.ashx?fname=" + fName + "&lname=" + lName + "&email=" + email + "&college=" + collegeName + "&city=" + city + "&type=sd&key=" + key;
                CallHandler(path, onSuccessSD);
            }
        }

        function onSuccessSD(result) {
            $("#SDLoadder").attr("style", "display:none");
            $("#SDButtons").attr("style", "display:block");

            if (result.Status == "Ok") {
                $("#scheduleDemoStatus").attr("style", "display:block;background-color: lightgreen");
                $("#scheduleDemoStatus").html("Your request submited successfully. Our team contact you shortly.");

                /*var intverval = setInterval(
                function () {
                $('#scheduleDemo').fadeOut(100);
                $("#scheduleDemoStatus").html("");
                $("#scheduleDemoStatus").attr("style", "display:none");
                $("#getEarlyAccessModelBack").hide(500);
                clearInterval(intverval);
                }, 5000);
                */
                ClearSDData();
            }
            else if (result.Status == "Error") {
                if (result.Message == "Session Expire") {
                    alert("Your Session is Expire you are redirect to login page.");
                    parent.document.location = BASE_URL + "Login.aspx";
                }
                else {
                    $("#scheduleDemoStatus").attr("style", "display:block");
                    $("#scheduleDemoStatus").html("Error: Currently we are unable to receive your demonstration request. Please try again after some time.");
                }
            }
        }


    </script>
    <style type="text/css">
        #ql_List input[type="text"] {
            font-size: 14px;
            padding: 4px 6px;
            margin-bottom: 3%;
            width: 90%;
            background-color: #fff;
            border: 1px solid #ccc;
        }

        #ql_List textarea {
            width: 90%;
            min-height: 50px;
        }

        #ql_List input[type="button"] {
            padding: 4px 12px;
        }

        #ql_List div, span, input[type="text"] {
            font-family: Arial,Helvetica,sans-serif;
        }

        #modals h4, h5, .h4, .h5 {
            font-size: 12px !important;
            font-size: 1.2rem !important;
            font-weight: bold !important;
            color: #242424;
            font-family: Arial,Helvetica,sans-serif;
        }

        #modals input[type="text"] {
            font-size: 14px;
            padding: 4px 6px;
            margin-bottom: 3%;
            width: 70%;
            background-color: #fff;
            border: 1px solid #ccc;
        }

        #modals div, span, input[type="text"] {
            font-family: Arial,Helvetica,sans-serif;
        }

        #modals input[type="button"], .btn {
            padding: 4px 12px;
        }

        .featured-services ul {
            list-style: disc inside none !important;
        }

        .ErrorContainer, .SuccessContainer {
            padding: 10px;
            float: left;
            border-radius: 5px;
            float: left;
            background-color: pink;
            margin-bottom: 10px;
            width: 96%;
        }

        .SuccessContainer {
            background-color: lightgreen;
        }

        @media only screen and (max-width:240px) {
            #ql_SideBar {
                visibility: hidden !important;
            }
        }

        .featured-services ul li {
            line-height: 50px;
        }

        .featured-services {
            float: left;
            margin: 45px 0 0px 16px;
            padding: 20px 20px;
            font-size: 30px;
            /* text-align: justify; */
            line-height: 41px !important;
            /* margin-bottom: 12px; */
            /*color: #000;*/
            color: rgb(0, 147, 212);
            width: 60%;
        }

        #course-tile-list {
            float: left;
            width: 100%;
        }

            #course-tile-list ul {
                float: left;
                list-style: none;
                padding: 0;
                margin: 0;
                width: 100%;
                height: 340px;
                margin-left: 25px;
            }

                #course-tile-list ul li {
                    float: left;
                    list-style: none;
                    width: 250px;
                    margin-right: 10px;
                    margin-bottom: 1%;
                    text-align: left;
                    position: relative;
                    border: 1px solid rgb(236, 230, 230);
                    min-height: 350px;
                }

        .title {
            text-align: center;
            font-size: 20px;
            padding: 10px;
            border-bottom: 1px solid rgb(234, 221, 221);
        }

        .tile-content {
            padding: 10px;
            font-size: 15px;
            text-align: justify;
        }

        /* new css */
        .courseContainer {
            font-family: 'Roboto',sans-serif !important;
            margin-bottom: 30px;
        }

        .courses {
            margin-top: 2%;
            margin-bottom: 2%;
        }

        .courseText, div.courseContainer > a > div {
            background: white;
            padding: 10px 15px;
        }

        div.courseText {
            border-bottom: 1px solid #ddd;
        }

        .courseText, div.courseContainer > a > div {
            background: white;
            padding: 10px 15px;
            min-height: 170px;
        }

        .courseContainer > div > a > h2.lead, .courseContainer > a > div > h2.lead {
            text-decoration: none;
            margin-bottom: 10px;
            margin-top: 0px;
            text-align: left;
            color: #e96e78;
            font-size: 22px;
            line-height: 24.639999389648438px;
            font-weight: 300;
            height: 50px;
        }

        a.courseLink, a.courseLink:hover {
            text-decoration: none;
            color: inherit;
        }

        .courseLink > p, .courseText > p {
            text-align: justify;
        }

        div.courseContainer:hover {
            box-shadow: 0px 0px 10px rgb(0, 0, 0);
            transition: box-shadow 0.3s ease-in-out 0s;
            cursor: pointer;
        }

        .courseContainer {
            font-family: 'Roboto',sans-serif !important;
            margin-bottom: 30px;
        }
    </style>
</head>

<body data-header-color="light" data-smooth-scrolling="1" data-responsive="0" data-spy="scroll" style="background-color: #f5f5f5;">
    <%--<form runat="server" id="form">--%>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                    class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                </a>
                <img src="static/images/logo.png" class="logo" style="width: 20%;" />
                <div class="nav-collapse collapse">
                    <%--<ul class="nav" id="top-nav">
                        <li class="active"><a href="#section-1">Home</a></li>
                        <li><a href="#section-2">Students</a></li>
                        <li><a href="#section-3">Colleges</a></li>
                        <li><a href="#section-4">Syllabus</a></li>
                        <li><a href="#section-7">Typing Tutorial</a></li>
                        <li><a href="#section-5">Success Stories</a></li>
                        <%--<li><a href="#section-6">Contact Us</a></li>
                        <li id="contact-us"><a href="#">Contact Us</a></li>
                        <li class="invite-link"><a onclick="ShowGetEarlyAccessPopup();" href="javascript:void(0);"
                            style="background-color: orange; color: #fff!important; text-decoration: none; border-radius: 6px;">Sign up</a> </li>
                    </ul>--%>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    <div class="container" id="section-1">
        <div class="slider">
            <div class="flexslider">
                <div class="login_form">
                    <div class="start-learn">
                        Start Learning
                    </div>
                    <form method="post" runat="server" action="" id="form1" class="form-signin">
                        <asp:HiddenField ID="hfEncryptData" runat="server" />
                        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left; color: Red; float: left; width: 94%;"
                            visible="false">
                        </div>
                        <div class="form-signin-heading">
                            Please sign in
                        </div>
                        <input name="txtUserName" id="txtUserName" runat="server" class="input-block-level"
                            placeholder="Username" type="text" />
                        <input name="txtPassword" id="txtPassword" runat="server" class="input-block-level"
                            placeholder="Password" type="password" />
                        <br />
                        <asp:Button ID="Button1" runat="server" OnClick="Login1_Authenticate" CssClass="btn btn-large btn-primary"
                            Text="Sign in" Visible="true" />
                        <br />
                        <br />
                        <a href="Forgot-Password.aspx" title="forgot password">Forgot your password?</a>
                    </form>
                </div>
                <div class="featured-services">
                    <ul>
                        <li>Easy management of course material by author</li>
                        <li>Save costs on infrastructure development and maintenance</li>
                        <li>Automated progress tracking and reporting</li>
                        <li>Time tracking across the application</li>
                        <li>Online support offered by MOOC executives</li>
                        <li>Mobile compatible content delivery</li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="span10 span offset1 mainContainer" style="margin-top: 180px;">
            <div class="row-fluid courses">
                <div class="span4" style="padding: 0 7px; margin-left: 0;">
                    <div class="courseContainer">
                        <a class="a-course courseLink" href="Course/fundamentals-of-computer/computer-basics/introduction.htm" corusename="Fundamental of computer">
                            <img src="static/images/a-foc.jpg" style="width: 100%; max-width: 300px; min-width: 150px;">
                            <div class="text-center courseText">
                                <h2 class="lead overflow-h">Fundamental of Computers</h2>
                                <p class="description">
                                    A computer is a device that takes raw data as input from the user, processes these this data and gives the result (output) and saves output for the future use.
                                </p>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="span4" style="padding: 0 7px;">
                    <div class="courseContainer">
                        <a class="a-course courseLink" href="CourseDetails.aspx?id=2" corusename="Fundamental of computer">
                            <img src="static/images/foc.jpg" style="width: 100%; max-width: 300px; min-width: 150px;">
                            <div class="text-center courseText">
                                <h2 class="lead overflow-h">Advanced Fundamental of Computers</h2>
                                <p class="description">
                                    Media is an instrument on communication, like a newspaper or a radio, so social media would be a social instrument of communication.
                                </p>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="span4" style="padding: 0 7px;">
                    <div class="courseContainer">
                        <a class="a-course courseLink" href="guest-typing-tutorial.htm" corusename="Fundamental of computer">
                            <img src="static/images/typing-manual.png" style="width: 100%; max-width: 300px; min-width: 150px;">
                            <div class="text-center courseText">
                                <h2 class="lead overflow-h">Typing Tutorial</h2>
                                <p class="description">
                                    The free typing lessons on "How to type". Colorful keyboard layout are used to correct mis-typing by showing the right way to type for your learning and practice experience. <a href="guest-typing-tutorial.htm">take a trail</a>
                                </p>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="block" style="margin-top: 100px;">
            <h2 class="page-header"></h2>
        </div>
    </div>
    <!-- May I help you section -->
    <div>
        <div id="ql_SideBar">
            <div title="Click here for any help" id="ql_Display" onclick="showMayIHelpYouWindow();"
                style="background-color: #404040 !important; color: White; background-image: none; border-top-left-radius: 3px; border-top-right-radius: 3px;">
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
                    <input type="text" id="txtEmail" placeholder="Email Address" style="min-height: 30px;" /><span
                        style="color: Red">*</span>
                    <textarea id="txtQuery" placeholder="Your question" cols="20" rows="2" style="max-height: 100px; margin-bottom: 3%; margin-left: 2%; padding: 6px; font-size: 14px;"></textarea>
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
                    <%-- Note: You will receive call between 10 AM to 7 PM in week days (Monday to Saturday)--%>
                </div>
            </div>
        </div>
    </div>
    <!-- End -->
    <div id="modals">
        <!-- /.Get Early Access modal-dialog -->
        <div class="modal fade" id="getEarlyAccess" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2"
            style="display: none; top: 50%" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="HideGetEarlyAccessPopup();">
                            &times;</button>
                        <h4 class="modal-title">Get Invitation</h4>
                    </div>
                    <div class="modal-body">
                        <div class="Record">
                            <div class="ErrorContainer" id="getEarlyAccessStatus" style="display: none; width: 96%;">
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="FirstName" id="txtGEAFirstName" class="input-block-level" placeholder="First Name"
                                    type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="LastName" id="txtGEALastName" class="input-block-level" placeholder="Last Name"
                                    type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="Email" id="txtGEAEmail" class="input-block-level" placeholder="Email Address"
                                    type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div id="GEAButtons">
                            <a href="javascript:void(0);" class="btn" data-dismiss="modal" onclick="HideGetEarlyAccessPopup();"
                                title="click to close popup">Close</a> <a href="javascript:void(0);" class="btn btn-primary"
                                    onclick="SubmitGetEarlyAccess();">Submit</a>
                        </div>
                        <div id="GEALoadder" style="display: none">
                            <img src="static/images/waiting-loader.gif" />
                            please wait...
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div id="getEarlyAccessModelBack" style="display: none;" class="modal-backdrop fade in">
        </div>
        <!-- end get early access modal -->
        <!-- Schedule a demonstration modal-dialog -->
        <div class="modal fade" id="scheduleDemo" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2"
            style="display: none; top: 50%" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="HideScheduleDemoPopup();">
                            &times;</button>
                        <h4 class="modal-title">Schedule a demonstration</h4>
                    </div>
                    <div class="modal-body">
                        <div class="Record">
                            <div class="ErrorContainer" id="scheduleDemoStatus" style="display: none; width: 96%;">
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="FirstName" id="txtSDFirstName" class="input-block-level" placeholder="First Name"
                                    type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="LastName" id="txtSDLastName" class="input-block-level" placeholder="Last Name"
                                    type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="Email" id="txtSDCollegeName" class="input-block-level" placeholder="College Name"
                                    type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="City" id="txtCity" class="input-block-level" placeholder="City" type="text" />
                            </div>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                                <input name="Email" id="txtSDEmail" class="input-block-level" placeholder="Email Address"
                                    type="text" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div id="SDButtons">
                            <a href="javascript:void(0);" class="btn" data-dismiss="modal" onclick="HideScheduleDemoPopup();"
                                title="click to close popup">Close</a> <a href="javascript:void(0);" class="btn btn-primary"
                                    onclick="SubmitScheduleDemo();">Submit</a>
                        </div>
                        <div id="SDLoadder" style="display: none">
                            <img src="static/images/waiting-loader.gif" />
                            please wait...
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- end get early access modal -->
    </div>
    <!-- /container -->
    <%--<script type="text/javascript" src="static/scripts/plugins.js"></script>--%>
    <%--<script type="text/javascript">
        $('#top-nav').onePageNav({
            currentClass: 'active',
            changeHash: true,
            scrollSpeed: 1200
        });
    </script>--%>
    <script type="text/javascript">
        function goToByScroll(id) {
            // Remove "link" from the ID

            // Scroll
            $('html,body').animate({
                scrollTop: $(id).offset().top - 150
            },
        'slow');
        }

        $(".nav-collapse > ul > li > a").click(function (e) {
            // Prevent a page reload when a link is pressed
            e.preventDefault();
            // Call the scroll function
            goToByScroll($(this).attr("href"));

            $("#top-nav").find("li").each(function () {
                if ($(this).attr("class") != "invite-link") {
                    $(this).removeAttr("class");
                }
            });
            $(this).parent().attr("class", "active");
        });

        // adjust side menu
        var topRange = 200,     // measure from the top of the viewport to X pixels down
        edgeMargin = 20,        // margin above the top or margin from the end of the page
        animationTime = 1200,   // time in milliseconds
        contentTop = [];

        // Set up content an array of locations
        $('#top-nav').find('a').each(function () {
            var id = $(this).attr('href');
            if (id.length > 2)
                contentTop.push($(id).offset().top);
        });

        $(window).scroll(function () {

            var winTop = $(window).scrollTop(),
        bodyHt = $(document).height(),
        vpHt = $(window).height();  // viewport height + margin

            $.each(contentTop, function (i, loc) {
                if ((loc > winTop - edgeMargin && (loc < winTop + topRange || (winTop + vpHt) >= bodyHt))) {
                    $('#top-nav li').removeClass('active').eq(i).addClass('active');
                }
            });
        });
    </script>
</body>
</html>
<script type="text/javascript">
    //BASE_URL = '<%=Util.BASE_URL %>';
    //alert(BASE_URL);
    var hostName = document.location.hostname;
    if (hostName == "moocacademy." || hostName == "www.moocacademy.") {
        document.location = "../default.aspx";
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


    $("#contact-us").click(function () {
        document.location = "Contact-us.aspx";
    });

</script>
<script type="text/javascript">
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

    ga('create', 'UA-46034211-1', 'auto');
    ga('send', 'pageview');

</script>
