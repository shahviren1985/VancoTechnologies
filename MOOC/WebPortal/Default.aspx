<%@ Page Language="C#" Inherits="_Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" %>

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
    <meta name="description" content="MOOC Academy - E-learning platform to learn skill development courses online">
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

        @media only screen and (max-width: 768px) {
            #ql_SideBar, .btn-navbar {
                display: none !important;
            }

            .navbar {
                position: fixed;
            }

            .login_form {
                margin: 0px !important;
            }

            .mobile-video {
                display: block !important;
                margin: 10px;
                position: absolute;
                left: 10px;
            }
            .mobile-video h4{
                font-weight: bold !important;
                font-size: 24px;
                text-align: center;
                text-decoration: underline;
            }
            .mobile-video ul{
                margin-left: 40px;
                list-style-type: circle;
            }
            .mobile-video ul li {
                font-size: 18px;
                padding-top: 10px;
            }
        }
    </style>
</head>
<body data-header-color="light" data-smooth-scrolling="1" data-responsive="0" data-spy="scroll">
    <%--<form runat="server" id="form">--%>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                    class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                </a>
                <img src="static/images/logo.png" class="logo" style="width: 20%;" />
                <div class="nav-collapse collapse">
                    <ul class="nav" id="top-nav">
                        <li class="active"><a href="#section-1">Home</a></li>
                        <li><a href="#section-2">Students</a></li>
                        <li><a href="#section-3">Colleges</a></li>
                        <li><a href="#section-4">Syllabus</a></li>
                        <li><a href="#section-7">Typing Tutorial</a></li>
                        <li><a href="#section-5">Success Stories</a></li>
                        <%--<li><a href="#section-6">Contact Us</a></li>--%>
                        <li id="contact-us"><a href="#">Contact Us</a></li>
                        <li class="invite-link"><a onclick="ShowGetEarlyAccessPopup();" href="javascript:void(0);"
                            style="background-color: orange; color: #fff!important; text-decoration: none; border-radius: 6px;">Sign up</a> </li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    <div class="container" id="section-1">
        <div class="slider">
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
                <div class="mobile-video" style="display: none">
                    <div>
                        <h4>Basics of Computer</h4>
                        <ul>
                            <li>Learn basics of computer, internet & social media</li>
                            <li>Understand the current industry standards and get expertise</li>
                            <li>Follow the best practices</li>
                            <li>Build your online presence in social and professional media</li>
                            <li>Get certified and make yourself ready for an industry</li>
                            <li>Keep yourself updated with latest trends</li>
                        </ul>
                    </div>
                    <p>
                        <a class="featured-btn" href="Course/fundamentals-of-computer/computer-basics/introduction.htm">Explore
                                the course</a><br />
                        <br />
                        <br />
                        <br />
                    </p>
                    <div id="player">

                    </div>
                    <%--<iframe src="https://www.youtube.com/embed/7Y3_VVXw5_c?autoplay=1&mute=1" frameborder="0" style="width: 440px; height: 320px; margin-bottom: 30px;"></iframe>--%>
                </div>
            </div>
            <div class="flexslider">

                <ul class="slides">
                    <li>
                        <img src="static/images/operating-systems.png" title="" alt="" class="medium" style="margin-left: 10%; height: 45% !important; width: 55% !important;" /></li>
                    <li>
                        <img src="static/images/lan.png" title="" alt="" class="medium" style="margin-left: 10%; height: 50% !important; width: 60% !important;" /></li>
                    <li>
                        <img src="static/images/password-safety.png" title="" alt="" style="height: 65% !important; width: 65% !important;" /></li>
                    <li>
                        <img src="static/images/antivirus.png" title="" alt="" style="margin-left: 10%; height: 55% !important; width: 60% !important;" /></li>
                    <li>
                        <img src="static/images/security-recommendation.png" title="" alt="" style="margin-left: 10%; height: 55% !important; width: 60% !important;" /></li>
                    <li>
                        <img src="static/images/social-media website.png" title="" alt="" style="margin-left: 10%; height: 65% !important; width: 60% !important;" /></li>
                    <li>
                        <img src="static/images/social-media safety.png" title="" alt="" style="margin-left: 1%; height: 75% !important; width: 70% !important;" /></li>
                </ul>
            </div>
        </div>
        <div class="block" id="section-2" style="padding-top: 10px; padding-bottom: 40px">
            <h2 class="page-header"></h2>
            <div id="students" class="container">
                <div class="col full-width-section parallax_section" style="background-image: url(&quot;static/images/student-benifits.jpg&quot;); background-position: 80% 60%; background-size: 35% 60%; background-repeat: no-repeat; border-top: 3px solid #CCC;">
                    <div class="col magn-left">
                        <h3 class="featured-services">Students</h3>
                        <div class="featured-services">
                            <ul>
                                <li>Learn basics of computer, internet & social media</li>
                                <li>Understand the current industry standards and get expertise</li>
                                <li>Follow the best practices</li>
                                <li>Build your online presence in social and professional media</li>
                                <li>Get certified and make yourself ready for an industry</li>
                                <li>Keep yourself updated with latest trends</li>
                            </ul>
                        </div>
                        <p>
                            <a class="featured-btn" href="Course/fundamentals-of-computer/computer-basics/introduction.htm">Explore
                                the course</a><br />
                            <br />
                            <br />
                            <br />
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!-- end block -->
        <div class="block" id="section-3" style="padding-top: 10px; padding-bottom: 40px">
            <div id="colleges" class="container">
                <div class="col full-width-section parallax_section" style="background-image: url(&quot;static/images/college-analytics.png&quot;); background-position: 15% 50%; background-size: 40% 48%; background-repeat: no-repeat; background-color: #fff; visibility: visible; border-top: 3px solid #CCC;">
                    <div class="mobile-conpact" style="float: right; margin-right: 90px; margin-top: 20px;">
                        <h3 class="featured-services">Colleges</h3>
                        <div class="featured-services">
                            <ul>
                                <li>Minimum involvement by the college staff members</li>
                                <li>Save costs on infrastructure development and maintenance</li>
                                <li>Increase your profit margin on the computer fees</li>
                                <li>Easy integration with existing IT systems for registration process</li>
                                <li>Student queries are handled by MOOC Academy's technical experts</li>
                                <li>Available to staff members</li>
                                <li>Onboard training is provided by MOOC Academy's technical experts</li>
                            </ul>
                        </div>
                        <p>
                            <a class="featured-btn" href="javascript:void(0);" onclick="ShowScheduleDemoPopup();">Schedule a demonstration</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!-- end block -->
        <div class="block" id="section-4">
            <div id="syllabus" class="container" style="padding-bottom: 40px;">
                <div class="syllabus-left">
                    <div class="col">
                        <h3 class="featured-services" style="font-size: 42px;">Syllabus - Fundamentals of computer, internet & social media</h3>
                        <div class="syllabus-div-text">
                            Basic
                        </div>
                        <div class="syllabus-div-text">
                            Intermediate
                        </div>
                        <div class="syllabus-div-text">
                            Advance
                        </div>
                        <div class="syllabus-left-text">

                            <p>
                                <img alt="" src="static/images/software.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/computer-basics/Introduction.htm">Computer Basics</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/hardware.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/computer-hardware/introduction.htm">Hardware</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/operating-system.png" height="35" width="35"><span> <a
                                    href="Course/fundamentals-of-computer/operating-system/Introduction.htm">Operating
                                    system</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/networks.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/computer-networks/computer-networks.htm">Networks</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/word.png" height="35" width="35"><span> <a href="#">Word,
                                    Excel, PowerPoint</a></span>
                            </p>
                        </div>
                        <div class="syllabus-left-text">
                            <p>
                                <img alt="" src="static/images/internet.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/internet/introduction.htm">Internet</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/protocol.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/computer-networks/protocols.htm">Protocols</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/tools.png" height="35" width="35"><span> <a href="#">Tools</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/anitvirus.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/security/antivirus.htm">Antivirus</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/best-practices.png" height="35" width="35"><span> <a
                                    href="Course/fundamentals-of-computer/security/best-practices.htm">Best practices</a></span>
                            </p>
                        </div>
                        <div class="syllabus-left-text">
                            <p>
                                <img alt="" src="static/images/social-media.png" height="35" width="35"><span> <a
                                    href="Course/fundamentals-of-computer/social-media/introduction.htm">Social media</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/email.png" height="35" width="35"><span> <a href="Course/fundamentals-of-computer/internet/email-best-practices.htm">Email writing tips</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/social-engineering.png" height="35" width="35"><span>
                                    <a href="Course/fundamentals-of-computer/internet/social-engineering-emails.htm">Social
                                        engineering</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/best-practices-2.png" height="35" width="35"><span> <a
                                    href="Course/fundamentals-of-computer/social-media/best-practices.htm">Best practices</a></span>
                            </p>
                            <p>
                                <img alt="" src="static/images/online-tools.png" height="35" width="35"><span> <a
                                    href="Course/fundamentals-of-computer/internet/google-tricks.htm">Useful online
                                    tools</a></span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end block -->
        <div class="block" id="section-7" style="padding-top: 10px; padding-bottom: 40px;">
            <div id="typing" class="container">
                <div class="col full-width-section parallax_section" style="background-image: url(&quot;static/images/typing-tutor.png&quot;); background-position: 15% 50%; background-size: 40% 48%; background-repeat: no-repeat; background-color: #fff; visibility: visible; border-top: 3px solid #CCC; margin-top: -15px !important">
                    <div style="float: right; margin-right: 90px; margin-top: 20px; margin-right: 0%; width: 50%;">
                        <h3 class="featured-services">Typing Tutorial</h3>
                        <div class="featured-services" style="padding: 5px 66px 0px 10px;">
                            <p style="color: #595858; font-family: Roboto; font-size: 18px; line-height: 20px; margin-top: 7px;">
                                The free typing lessons on "How to type". Colorful keyboard layout are used to correct
                                mis-typing by showing the right way to type for your learning and practice experience.
                            </p>
                            <p style="color: #595858; font-family: Roboto; font-size: 18px; line-height: 20px; margin-top: 7px; margin-bottom: 50px;">
                                Lessons' difficulty gradually raises as it starts from 4 characters and ends with
                                complex combinations of characters. When the lesson ends, you can learn a lot from
                                the practice trends like WPM and accuracy.
                            </p>
                        </div>
                        <p>
                            <a class="featured-btn" href="guest-typing-tutorial.htm">Test your typing speed now</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!-- end block-->
        <div class="block" id="section-5">
            <%--<h2 class="page-header">
            </h2>--%>
            <div id="success" class="container">
                <div class="col syllabus-left">
                    <iframe src="https://www.youtube.com/embed/7Y3_VVXw5_c?autoplay=1&mute=1" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" style="width: 500px; height: 380px;"></iframe>
                </div>
            </div>
        </div>
        <!--end block-->
        <div class="block" id="section-6">
            <h2 class="page-header"></h2>
            <div id="contactus" class="footer-outer" style="padding-bottom: 40px; padding-top: 30px;">
                <div id="copyright">
                    <div class="col contact-left">
                        <h3 class="featured-services">Contact Us</h3>
                        <div class="cont-text">
                            <br />
                            &copy; 2019. MOOC Academy
                        </div>
                    </div>
                    <div class="col span_5 cont-about">
                        <h3 class="featured-services">About Us</h3>
                        <p style="font-family: Roboto; font-size: 18px; line-height: 20px;">
                            MOOC Academy is an e-learning platform. We are committed to deliver quality education
                            through online courses. This platform will enable online education by means of training,
                            courses and classes.
                        </p>
                        <br />
                        <p style="font-family: Roboto; font-size: 18px; line-height: 20px;">
                            Currently MOOC Academy offers computer education by distance learning class. The
                            online computer course syllabus includes basics of computer, internet, security
                            and social media.
                        </p>
                        <br />
                        <p style="font-family: Roboto; font-size: 18px; line-height: 20px;">
                            Please mail us your feedback on info at moocacademy dot in. You could also request
                            early access by sending an email on info at moocacademy dot in.
                        </p>
                    </div>
                    <!--/container-->
                </div>
                <!--/row-->
            </div>
        </div>
        <div class="block" style="margin-top: 150px;">
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
  // 2. This code loads the IFrame Player API code asynchronously.
  var tag = document.createElement('script');

  tag.src = "https://www.youtube.com/iframe_api";
  var firstScriptTag = document.getElementsByTagName('script')[0];
  firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

  // 3. This function creates an <iframe> (and YouTube player)
  //    after the API code downloads.
  var player;
  function onYouTubeIframeAPIReady() {
    player = new YT.Player('player', {
      height: '210',
      width: '330',
      videoId: '7Y3_VVXw5_c',
      events: {
        'onReady': onPlayerReady,
        'onStateChange': onPlayerStateChange
        },
        playerVars: {
            autoplay: 1,
            controls: 1,
            mute: 1
        }
    });
  }

  // 4. The API will call this function when the video player is ready.
  function onPlayerReady(event) {
      event.target.playVideo();
      
  }

  // 5. The API calls this function when the player's state changes.
  //    The function indicates that when playing a video (state=1),
  //    the player should play for six seconds and then stop.
  var done = false;
  function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.PLAYING && !done) {
      setTimeout(stopVideo, 6000);
      done = true;
    }
  }
  function stopVideo() {
    player.stopVideo();
  }
</script>
<script type="text/javascript">
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
