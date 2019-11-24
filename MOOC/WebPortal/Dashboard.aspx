<%@ Page Title="MOOC Academy - Dashboard" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script src="static/scripts/Chart.js" type="text/javascript"></script>
    <div id="studentreportsbutton" class="btn btn-success" style="margin-top: 2px; float: right; margin-right: 11%; display: none;">
        <a href="StudentPerformanceReport.aspx">Check Your Performance</a>
    </div>
    <div class="content-container" style="margin-left: 8%; padding: 10px; width: 83%">
        <%--style="width: 80%; margin-left: 10%; margin-right: 10%;">--%>
        <div id="divExamState" runat="server" class="ExamStateContainer" style="display: none;">
            <div style="display: none; margin: 1% 0% 1% 0%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; float: left; width: 90%; background: linear-gradient(to bottom, rgb(255, 255, 255) 0%, rgb(229, 229, 229) 100%) repeat scroll 0% 0% transparent; display: none;">
                <img src="static/images/notification_done.png" width="5%" style="float: left" />
                <div style="margin-left: 70px; margin-top: 8px;">
                    Thank you all teachers and students. All the issues which you had reported related
                    to the Quiz have been resolved. Please feel free to contact us if you observe any
                    more issue.
                </div>
            </div>
            <div class="ExamState" runat="server" id="step1">
                Step 1
            </div>
            <div class="ExamState" runat="server" id="step2">
                Step 2
            </div>
            <div class="ExamState" runat="server" id="step3">
                Step 3
            </div>
            <div class="ExamState" runat="server" id="performance" style="background-color: Orange">
                <a href="StudentPerformanceReport.aspx?courseid=1">Performance Reports</a>
            </div>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="display: none">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false">
        </div>
        <canvas id="canvas" style="width: 0px; height: 0px;">
        </canvas>
        <div style="border: 0; box-shadow: none;">
            <%--class="content"--%>
            <div class="content-summary blue" style="display: none;">
                <div class="summary-header">
                </div>
                <div>
                </div>
            </div>
            <div class="content-main">
                <div id="dataContainer" class="box box-orange">
                    <div class="boxHeading">
                        Course
                    </div>
                    <div id="courseLoadder" style="display: none; margin-top: 40%;">
                        <img src="static/images/loading.gif" />
                        Loading please wait...
                    </div>
                    <div id="courseContainer" style="float: left;">
                        <div class="StartCourse" id="StartCourse">
                            <div style="text-align: center;">
                                <span style="margin: 10px">Loading...</span><img src="static/images/loading.gif"
                                    width="40px" />
                            </div>
                        </div>
                        <div class="StartCourse" id="quiz">
                        </div>
                        <div style="text-align: center; float: left; width: 100%; margin-top: 2%;">
                            Course Progress
                        </div>
                        <div>
                            <canvas id="canvasCourseProgressDONUT" width="200" height="180">
                        </canvas>
                        </div>
                        <div id="completePer" class="complete-percent">
                        </div>
                        <div id="pendingPer" class="pending-percent">
                        </div>
                    </div>
                    <div id="course-stats" style="float: left; margin-top: 7%; margin-left: 3%; margin-bottom: 0; display: none; text-align: center; width: 100%;">
                        <a href="CoursesChapterStatus.aspx">View your Course status</a>
                    </div>
                </div>
                <div id="newtests" class="box box-green" style="display: none;">
                    <div class="boxHeading">
                        New Tests
                    </div>
                    <div id="newTestLoader" style="display: none; margin-top: 40%;">
                        <img src="static/images/loading.gif" />
                        Loading please wait...
                    </div>
                    <div id="newTest">
                        <div id="newTestContainer" style="float: left; margin: 5%; width: 90%; text-align: left; line-height: 20px;">
                        </div>
                    </div>
                </div>
                <div id="typingBox" class="box box-green">
                    <div class="boxHeading">
                        Typing Practice
                    </div>
                    <div class="StartCourse">
                        <img src="static/images/typing-tutorial.png" class="typing-image" alt="Typing tutorial" />
                        <a href="TypingTutorial.aspx">Typing tutorial</a>
                    </div>
                    <div id="typing">
                    </div>
                    <canvas id="canvasTypingStatsLine" width="340" height="215" title="Recently attempted typing levels">
                    </canvas>
                    <div id="typing-stats" style="float: left; margin-top: 3%; margin-left: 3%; margin-bottom: 0px; width: 95%; text-align: center; display: none">
                        <a href="TypingStats.aspx">View your typing-level status</a>
                    </div>
                </div>
                <div id="recentActivity" class="box box-red">
                    <div class="boxHeading">
                        Recent Activity
                    </div>
                    <div id="activityContainer" class="activity">
                    </div>
                </div>
				
				<iframe runat="server" id="Video" src="https://www.youtube.com/embed/7Y3_VVXw5_c?autoplay=1&mute=1" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" style="width: 500px; height: 380px;"></iframe>
				
                <asp:HiddenField ID="hfChapterwisePer" runat="server" />
                <asp:HiddenField ID="hfCourseCompletion" runat="server" />
                <asp:HiddenField ID="hfTimeEstimated" runat="server" />
                <asp:HiddenField ID="hfUserActivities" runat="server" />
                <asp:HiddenField ID="hfUserAttemptedFinalQuiz" runat="server" />
                <asp:HiddenField ID="hfRunningCourse" runat="server" Value="[]" />
                <asp:HiddenField ID="hfPendingCourse" runat="server" Value="[]" />
                <asp:HiddenField ID="hfNewTests" runat="server" Value="[]" />
                <asp:HiddenField ID="hfCourseId" runat="server" Value="[]" />
                <asp:HiddenField ID="hfIfCourseHasContent" runat="server" Value="[]" />
            </div>
            <div class="content-main" style="display: none;">
                <div id="Syllabus" class="box box-orange">
                    <div class="boxHeading">
                        Recent Tests
                    </div>
                    <div id="completeTestLoader" style="display: none; margin-top: 40%;">
                        <img src="static/images/loading.gif" />
                        Loading please wait...
                    </div>
                    <div id="completeCourseContainer">
                        <div id="testContainer" style="float: left; margin: 5%; width: 90%; text-align: left; line-height: 20px;">
                        </div>
                    </div>
                </div>
                <div id="Fun" class="box box-green">
                    <div class="boxHeading">
                        Today's Featured Tool
                    </div>
                    <div id="toolsLoader" style="display: none; margin-top: 40%;">
                        <img src="static/images/loading.gif" />
                        Loading please wait...
                    </div>
                    <div id="toolsContainer">
                        <div id="tools" style="float: left; margin: 5%; width: 90%; text-align: left; line-height: 20px;">
                            <div id="emptyTool" style="float: left; width: 100%; margin-bottom: 3%; display: none">
                            </div>
                            <div id="toolTitle" style="float: left; width: 100%; margin-bottom: 3%;">
                            </div>
                            <div id="toolLogo" style="float: left; padding-right: 10px">
                            </div>
                            <div id="toolDesc" style="text-align: justify">
                            </div>
                            <div id="toolWebsite" style="float: left; margin-top: 10%;">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="relatedCourses" class="box box-red" style="display: none;">
                    <div class="boxHeading">
                        Related Courses
                    </div>
                </div>
            </div>
            <!-- Chart-->
            <!--End Chart-->
            <div class="modal fade" id="RegisterStudent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="$('#RegisterStudent').fadeOut(500);">
                                &times;</button>
                            <h4 class="modal-title" id="modal_Title">Paper Details</h4>
                        </div>
                        <div class="modal-body" id="modal_Body">
                        </div>
                        <div class="modal-footer">
                            <a href="Javascript:void(0)" class="btn" data-dismiss="modal" onclick="$('#RegisterStudent').fadeOut(500);"
                                title="click to close popup">Close</a>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <script type="text/javascript">
                function ShowModul(a) {
                    var user = $(a).attr("userId");
                    var pass = $(a).attr("password");
                    $("#Content_txtOldPassword").val(pass);
                    //$("#Content_hfSelectedUserName").val(user);
                    //$("#Content_lblUserName").html("User Name: <b>" + user + "</b>");
                }

                function ChangePassword() {
                    if ($("#Content_txtOldPassword").val() == "") {
                        $("#Content_txtOldPassword").focus();
                        alert("Please enter old password");
                        return false;
                    }

                    if ($("#Content_txtNewPassword").val() == "") {
                        $("#Content_txtNewPassword").focus();
                        alert("Please enter new password");
                        return false;
                    }

                    if ($("#Content_txtRetypeNewPassword").val() == "") {
                        $("#Content_txtRetypeNewPassword").focus();
                        alert("Please re-type new password");
                        return false;
                    }

                    if ($("#Content_txtRetypeNewPassword").val() != $("#Content_txtNewPassword").val()) {
                        alert("New password and retype password not match");
                        $("#Content_txtRetypeNewPassword").val("");
                        $("#Content_txtRetypeNewPassword").focus();
                        return false;
                    }
                }
            </script>
        </div>
    </div>
    <style type="text/css">
        .StartCourse {
            float: left;
            font-size: medium; /*margin-left: 10%;*/
            margin-top: 2%;
            padding-top: 0px;
            padding-left: 0px;
            padding-bottom: 0px;
            width: 99%;
        }

            .StartCourse a {
                color: #08c;
                text-decoration: underline;
            }

                .StartCourse a:hover {
                    color: #08c;
                    text-decoration: none;
                }

        .boxHeading {
            font-size: large;
            text-align: center;
            margin-top: 5%;
            width: 100%;
            float: left;
        }

        .activityContainer {
            border-top: solid 1px #ccc;
            padding: 1%;
            min-height: 51px;
        }

            .activityContainer:hover {
                background-color: #EEE;
                cursor: pointer;
                border-radius: 0px;
            }

            .activityContainer a, #newTestContainer a, #testContainer a {
                color: #08c;
                text-decoration: underline;
            }

                .activityContainer a:hover, #newTestContainer a:hover, #testContainer a:hover {
                    color: #08c;
                    text-decoration: none;
                }

        .box {
            width: 31%;
            min-height: 383px;
            float: left;
            margin-left: 2%;
            text-align: center;
            margin-bottom: 2%;
            border: 1px solid #CCC;
        }

        .box-red {
            border-bottom: 2px solid red;
        }

        .box-green {
            border-bottom: 2px solid green;
        }

        .box-orange {
            border-bottom: 2px solid orange;
        }

        .complete-percent {
            width: 35%;
            float: right;
            margin-right: 2%;
            background-color: #7FFF00;
            color: #666;
            border-radius: 5px;
            padding: 10px;
        }

        .pending-percent {
            width: 35%;
            float: left;
            margin-left: 2%;
            background-color: #F7464A;
            color: #fff;
            border-radius: 5px;
            padding: 10px;
        }

        .activity {
            float: left;
            width: 94%;
            padding: 3%;
            text-align: left;
        }

        .ErrorContainer a {
            color: Blue;
            text-decoration: underline;
        }

            .ErrorContainer a:hover {
                color: Blue;
                text-decoration: none;
            }

        #tools a {
            color: #08c;
            text-decoration: underline;
        }

            #tools a:hover {
                color: #08c;
                text-decoration: none;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".top-navigation").css("padding-bottom", "15px");
            $("#canvasTypingStatsLine").css("width", "98%");
            $("#canvasCourseProgressDONUT").css("width", "60%");

            var test_canvas = document.createElement("canvas") //try and create sample canvas element
            var canvascheck = (test_canvas.getContext) ? true : false //check if object supports getContext() method, a method of the canvas element

            if (!canvascheck) {
                $(".ErrorContainer").html("Your current browser version is not supported. Please download latest version of <a target='_blank' href='http://windows.microsoft.com/en-IN/internet-explorer/download-ie/'>Microsoft Internet Explorer</a>/<a target='_blank' href='https://www.google.com/intl/en/chrome/browser/'>Google Chrome</a>/<a target='_blank' href='http://www.mozilla.org/en-US/firefox/new/'>Mozila Firefox</a>.");
                $(".ErrorContainer").show();
                $(".ErrorContainer").css("text-align", "center");
                $(".ErrorContainer").css("margin-top", "7%");
                $(".ErrorContainer").css("font-size", "15px");
                $(".content-main").hide();
                $("#currentCourse").hide();
                $("#1").hide();
                $("#ChangePassword").css("visibility", "hidden !important");
            }
            else {
                PrepareChart();
            }
        });

        function PrepareChart() {
            var path = "Handler/GetCourseCompletionGraphData.ashx?courseid=1";
            CallHandler(path, function (result) { PopulateChapterWisePerformance(result) });
        }

        function PopulateChapterWisePerformance(data) {
            try {
                if (data.Status != "Ok") {
                    if (data.Message == "Session Expire") {
                        alert("Your Session is Expire you are redirect to login page.");
                        parent.document.location = BASE_URL + "Login.aspx";
                    } else {
                        console.log(data.Message);
                        parent.document.location = BASE_URL + "default.aspx";
                    }

                    return;
                }

                var courseid = data.CourseId;
                var courseName = data.CourseName;

                BuildStartorContinueText(courseid, courseName, data.IsNewCourse)
                PopulateRecentTest(courseid);
                //GetTodaysFeaturedTool(courseid);

                var dbCourseCompChartData = [];
                dbCourseCompChartData = data.GraphData;

                var myDoughnut = new Chart(document.getElementById("canvasCourseProgressDONUT").getContext("2d")).Doughnut(dbCourseCompChartData);

                $("#completePer").html(dbCourseCompChartData[0].value + "% complete");
                $("#pendingPer").html(dbCourseCompChartData[1].value + "% pending");

                if (data.IsEmptyCourse == "true" || data.IsEmptyCourse == true) {
                    $("#dataContainer").css("display", "none");
                    $("#typingBox").css("display", "block");
                    $("#newtests").attr("class", "box box-orange");
                }
                else {
                    $("#dataContainer").css("display", "block");
                    $("#newtests").attr("class", "box box-green");
                }

                var isUserAttemptedQuiz = $("#Content_hfUserAttemptedFinalQuiz").val();

                if (dbCourseCompChartData[0].value == 100) {
                    isUserAttemptedQuiz = false;
                    if (isUserAttemptedQuiz == 'True' || isUserAttemptedQuiz == true) {
                        $("#quiz").html("You have appeared Final Quiz. If you want to reappear <a href='javascript:void(0);' onclick='ReappearInFinalQuiz();'>click here</a");
                    }
                    else {
                        $("#quiz").html("<a href='TakeFinalQuiz.aspx?testid=0&courseid=" + courseid + "'>Take your final quiz for " + courseName + "</a>");
                    }
                    $("#StartCourse").hide();
                }
            } catch (e) {

            }

            $("#course-stats").show();
            $("#course-stats").html('<a href="CoursesChapterStatus.aspx?id=' + courseid + '">View your Course status</a>');

            PopulateUserActivities();
            PopuateUserCourses(courseid, data.MappedCourses);
            //PopulateNewTests();
            PopulateNewTest(courseid);
            PopuateTypingStats();
        }

        function PopulateUserActivities() {
            var path = "Handler/GetUserRecentActivities.ashx";
            CallHandler(path, function (data) {
                if (data.Status == "Ok") {
                    var userActivities = data.UserActivities;
                    //userActivities = JSON.parse($("#Content_hfUserActivities").val());
                    if (userActivities.length > 0) {
                        for (var i = 0; i < userActivities.length; i++) {
                            var div = $("<div/>");

                            var divText = "";
                            if (userActivities[i].ActivityType == "1") {
                                var titles = userActivities[i].Title.split("||");
                                var links = userActivities[i].Link.split("||");
                                var ids = links[0].split(" ");
                                var chid = ids[0].split("=");
                                var secid = ids[1].split("=");

                                divText = "You learnt about <a target='_blank'  href='" + BASE_URL + "HintPage.aspx?chapterId=" + chid[1] + "&sectionId=" + secid[1] + "&courseid=" + userActivities[i].CourseId + "'>" + unescape(titles[0]) + "</a> in <a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chid[1] + "&sectionId=" + secid[1] + "&courseid=" + userActivities[i].CourseId + "'>" + unescape(titles[1]) + "</a>.";
                            }
                            else if (userActivities[i].ActivityType == "2") {
                                userActivities[i].Link.split("||");
                                var ids = userActivities[i].Link.split(" ");
                                var chid = ids[0].split("=");
                                var secid = ids[1].split("=");
                                divText = "You appeared for quiz in <a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chid[1] + "&sectionId=" + secid[1] + "&courseid=" + userActivities[i].CourseId + "'>" + unescape(userActivities[i].Title) + "</a>.";
                            }
                            else if (userActivities[i].ActivityType == "3") {
                                divText = "You have practiced " + unescape(userActivities[i].Title) + " in <a target='_blank' href='" + BASE_URL + userActivities[i].Link + "'> typing tutorial</a>.";
                            }
                            else if (userActivities[i].ActivityType == "4") {
                                divText = "You have contacted support center for your query.";
                            }
                            else if (userActivities[i].ActivityType == "5") {
                                var titles = userActivities[i].Title.split("||");
                                var links = userActivities[i].Link.split("||");
                                var ids = links[0].split(" ");
                                var chid = ids[0].split("=");
                                var secid = ids[1].split("=");

                                divText = "You have reported an issue for <a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chid[1] + "&sectionId=" + secid[1] + "&courseid=" + userActivities[i].CourseId + "'>" + unescape(titles[0]) + "</a> in <a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chid[1] + "&sectionId=" + secid[1] + "&courseid=" + userActivities[i].CourseId + "'>" + unescape(titles[1]) + "</a>.";
                            }

                            var act = $("<div/>");
                            $(act).html(divText);

                            var tme = $("<div/>");
                            $(tme).attr("style", "margin-top: 1%;color: rgb(119, 119, 119);font-size: 11px;");
                            $(tme).html(userActivities[i].Datetime);

                            $(div).attr("class", "activityContainer");
                            $(div).append(act);
                            $(div).append(tme);

                            $("#activityContainer").append(div);
                        }
                    } else {
                        var div = $("<div/>");
                        var act = $("<div/>");
                        $(act).attr("style", "text-align: center;");
                        $(act).html("No recent activity");
                        $(div).attr("class", "activityContainer");
                        $(div).append(act);

                        $("#activityContainer").append(div);
                        //$("#activityContainer").html("No recent activity");
                    }
                }
                else {
                    console.log(data.Message);
                }
                //$("#activityContainer")
            });
        }

        function PopuateUserCourses(courseid, mappedCourse) {
            var runningCourse = [];
            var pendingCourse = [];

            if (mappedCourse != null)
                runningCourse = mappedCourse;

            /*runningCourse = JSON.parse($("#Content_hfRunningCourse").val());
            pendingCourse = JSON.parse($("#Content_hfPendingCourse").val());*/

            // populating running course
            for (var i = 0; i < runningCourse.length; i++) {
                var li = $("<option>");
                $(li).attr("value", runningCourse[i].Id);
                $(li).html(runningCourse[i].CourseName);

                if (courseid == runningCourse[i].Id) {
                    $(li).attr("selected", "true");
                }

                $("#runningCourse").append(li);
            }

            // populating running course
            for (var i = 0; i < pendingCourse.length; i++) {
                var li = $("<option>");
                $(li).attr("value", pendingCourse[i].Id);
                $(li).html(pendingCourse[i].CourseName);

                $("#pendingCourse").append(li);
            }
        }

        // Course Dropdown change event
        function onChange(ddl) {

            $("#course-stats").hide();

            var index = ddl.selectedIndex;
            if (index != 0) {
                // for course box
                $("#courseLoadder").show();
                $("#courseContainer").hide();
                // for new test box
                $("#newTestLoader").show();
                $("#newTest").hide();
                // for compete test box
                $("#completeTestLoader").show();
                $("#completeCourseContainer").hide();

                var courseid = ddl[index].value;
                PopulateGraph(courseid);
                //BuildStartorContinueText(courseid);
                PopulateRecentTest(courseid);
                PopulateNewTest(courseid);

                //GetTodaysFeaturedTool(courseid);
            }
        }

        function PopulateGraph(courseid) {
            var path = "Handler/GetCourseCompletionGraphData.ashx?courseid=" + courseid;
            CallHandler(path, function (result) { onCompletePopulateGraph(result, courseid) });
        }

        function onCompletePopulateGraph(result, courseid) {
            if (result.Status == "Ok") {

                $("#course-stats").show();
                $("#course-stats").html('<a href="CoursesChapterStatus.aspx?id=' + courseid + '">View your Course status</a>');

                BuildStartorContinueText(courseid, result.CourseName, result.IsNewCourse);

                //dbCourseCompChartData = result.GraphData;
                var myDoughnut = new Chart(document.getElementById("canvasCourseProgressDONUT").getContext("2d")).Doughnut(result.GraphData);

                //$("#completePer").html(dbCourseCompChartData[0].value + "% complete");
                //$("#pendingPer").html(dbCourseCompChartData[1].value + "% pending");

                $("#completePer").html(result.GraphData[0].value + "% complete");
                $("#pendingPer").html(result.GraphData[1].value + "% pending");

                //if (dbCourseCompChartData[0].value == 100) {
                if (result.GraphData[0].value == 100) {
                    isUserAttemptedQuiz = false;
                    if (isUserAttemptedQuiz == 'True' || isUserAttemptedQuiz == true) {
                        $("#quiz").html("You have appeared Final Quiz. If you want to reappear <a href='javascript:void(0);' onclick='ReappearInFinalQuiz();'>click here</a");
                    }
                    else {
                        $("#quiz").html("<a href='TakeFinalQuiz.aspx?testid=0&courseid=" + courseid + "'>Take your final quiz for " + $('#runningCourse :selected').text() + "</a>");
                    }
                    $("#quiz").show();
                    $("#StartCourse").hide();
                }
                else {
                    $("#quiz").hide();
                    $("#StartCourse").show();
                }

                /*if (courseid == 1) {
                    $("#newtests").css("display", "none");
                    $("#typingBox").css("display", "block");
                    //$("#dataContainer").css("display", "block");
                }
                else {
                    $("#newtests").css("display", "block");
                    $("#typingBox").css("display", "none");
                }*/

                if (result.IsEmptyCourse || result.IsEmptyCourse == "true") {
                    $("#dataContainer").css("display", "none");
                    $("#typingBox").css("display", "block");
                    $("#newtests").attr("class", "box box-orange");
                }
                else {
                    $("#dataContainer").css("display", "block");
                    $("#newtests").attr("class", "box box-green");
                }
                //$("#courseLoadder").hide();
                //$("#courseContainer").show();
            }
            else if (result.Status == "Error") {
                if (result.Message == "Session Expire") {
                    alert("Your Session is Expire you are redirect to login page.");
                    parent.document.location = BASE_URL + "Login.aspx";
                }
                else {
                    //alert(result.Message);
                    console.log(result.Message);
                    parent.document.location = BASE_URL + "default.aspx";
                }
            }
        }

        function PopulateRecentTest(courseId) {
            var path = "Handler/GetRecentTestsByCourse.ashx?courseid=" + courseId + "&iscomplete=" + true;

            CallHandler(path, onCompletePopulateRecentTest);
        }

        function onCompletePopulateRecentTest(result) {
            $("#completeTestLoader").hide();
            $("#completeCourseContainer").show();

            if (result.Status == "Ok") {
                if (result.CourseTest != null && result.CourseTest != undefined && result.CourseTest.length > 0) {
                    var html = "";
                    for (var i = 0; i < result.CourseTest.length; i++) {
                        html += "<a href='TestStatics.aspx?courseid=" + result.CourseTest[i].CourseId + "&testid=" + result.CourseTest[i].Id + "'>" + result.CourseTest[i].TestName + "</a></br>";
                    }
                    $("#testContainer").html(html);
                }
                else {
                    $("#testContainer").html("Currently you do not have any test!");
                }

            }
            else if (result.Status == "Error") {
                if (result.Message == "Session Expire") {
                    alert("Your Session is Expire you are redirect to login page.");
                    parent.document.location = BASE_URL + "Login.aspx";
                }
                else {
                    //alert(result.Message);
                    console.log(result.Message);
                }
            }
        }

        function PopulateNewTest(courseId) {
            var path = "Handler/GetRecentTestsByCourse.ashx?courseid=" + courseId + "&iscomplete=" + false;

            CallHandler(path, onCompletePopulateNewTest);
        }

        function onCompletePopulateNewTest(result) {
            $("#newTestLoader").hide();
            $("#newTest").show();

            if (result.Status == "Ok") {
                if (result.CourseTest != null && result.CourseTest != undefined && result.CourseTest.length > 0) {
                    var html = "";
                    for (var i = 0; i < result.CourseTest.length; i++) {
                        //html += "<a href='TestStatics.aspx?courseid=" + result.CourseTest[i].CourseId + "&testid=" + result.CourseTest[i].Id + "'>" + result.CourseTest[i].TestName + "</a></br>";
                        html += "<a href='TakeFinalQuiz.aspx?testid=" + result.CourseTest[i].Id + "&courseid=" + result.CourseTest[i].CourseId + "'>" + result.CourseTest[i].TestName + ", attempt before " + result.CourseTest[i].DisplayEndDate + "</a></br>";
                    }
                    $("#newTestContainer").html(html);
                }
                else {
                    $("#newTestContainer").html("Currently you do not have any test!");
                }
            }
            else if (result.Status == "Error") {
                if (result.Message == "Session Expire") {
                    alert("Your Session is Expire you are redirect to login page.");
                    parent.document.location = BASE_URL + "Login.aspx";
                }
                else {
                    //alert(result.Message);
                    console.log(result.Message);
                }
            }
        }

        function PopulateNewTests() {
            var CourseTest = [];
            CourseTest = JSON.parse($("#Content_hfNewTests").val());
            if (CourseTest != null && CourseTest != undefined && CourseTest.length > 0) {
                var html = "";
                for (var i = 0; i < CourseTest.length; i++) {
                    html += "<a href='TakeFinalQuiz.aspx?testid=" + CourseTest[i].Id + "&courseid=" + CourseTest[i].CourseId + "'>" + CourseTest[i].TestName + ", attempt before " + CourseTest[i].DisplayEndDate + "</a></br>";
                }
                $("#newTestContainer").html(html);
            }
            else {
                $("#newTestContainer").html("Currently you do not have any test!");
            }
        }

        function PopuateTypingStats() {
            var path = "Handler/GetTypingStatusChart.ashx";
            CallHandler(path, function (data) {
                if (data.TypingChartData == null || data.TypingChartData == 'null') {
                    var div = $("<div/>");
                    $(div).html("Start taking typing tutorial now!");
                    $("#typing").append(div);
                    $("#canvasTypingStatsLine").hide();
                    $("#typing-stats").hide();
                }
                else {
                    $("#typing-stats").show();
                    var lineTypingStatsChartData = [];
                    lineTypingStatsChartData = data.TypingChartData;
                    var myLine = new Chart(document.getElementById("canvasTypingStatsLine").getContext("2d")).Line(lineTypingStatsChartData);
                }
            });
        }

    </script>
</asp:Content>
