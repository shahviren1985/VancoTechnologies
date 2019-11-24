<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script language="javascript">
        function ShowLinkDescription(element, className) {
            var timer = -1;
            var e;
            $(element).find(".LinkDescription").each(function () {
                e = $(this);
                timer = setTimeout(function () {
                    if ($(e).attr("timer") != undefined) {
                        $(e).removeAttr("timer");
                        $(e).show();

                        $(e).attr("class", className + " LinkDescription");
                        //setTimeout(function () { $(e).fadeOut(500); timer = -1; }, 3000);
                    }
                }, 1000);

                $(e).attr("timer", timer);
            });
        }

        function HideLinkDescription(element) {
            $(element).find(".LinkDescription").each(function () {
                $(this).removeAttr("timer");
                $(this).fadeOut(100);
            });
        }
    </script>
    <style>
        .Image
        {
            width: 60px;
            height: 60px;
        }
        .main-div
        {
            float: left;
            margin-top: 5%;
            width: 100%;
        }
    </style>
    <div class="main-div">
        <div class="Box BoxOrange" style="height:480px">
            <div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/MarksEntry.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="AddChapter.aspx">Add Chapter</a>
                        <div class="LinkDescription">
                            Add new chapter</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Marksheet.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="EditChapter.aspx">Manage Chapter</a>
                        <div class="LinkDescription">
                            Edit old chapter details, modify existing chapter details</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Paper_exam.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="AddQuestions.aspx">Add Question</a>
                        <div class="LinkDescription">
                            Add new Quesions for specific chapter and section</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Migrate_student.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="AddUser.aspx">Add Students</a>
                        <div class="LinkDescription">
                            Enter new student details, modify existing student details</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/StudentDetails.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="ManageUser.aspx">Manage Students</a>
                        <div class="LinkDescription">
                            Edit student details, modify existing student details.</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Ledger.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="AddUpdateAppLogoHeader.aspx">Manage Logo</a>
                        <div class="LinkDescription">
                            Add or Change Application logo and logo-image</div>
                    </div>
                </div>

                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/online-tools.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="ManageOnlineTools.aspx">Manage Onlie Tools</a>
                        <div class="LinkDescription">
                            Add or Change online tools which available on the internet</div>
                    </div>
                </div>

                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/TopperList.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="Certificates.aspx">Certificates</a>
                        <div class="LinkDescription">
                            Generate Students Certificates, print this list.</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/status.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="ImportStudents.aspx">Import Students</a>
                        <div class="LinkDescription">
                            Import student by excel files and save.</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="ChapterwiseReport.aspx">Performance Report</a>
                        <div class="LinkDescription">
                            Generate chapter wise chart for viewing Students progress.</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Last activity" class="Image" />
                    </div>
                    <div class="text">
                        <a href="LastActivityStatus.aspx">View Last Activity</a>
                        <div class="LinkDescription">
                            View Last Activity Perform by student</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="UserTypingStatus.aspx">View User Typing Status</a>
                        <div class="LinkDescription">
                            View User Typing Status</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="NotStartedTypingTest.aspx">User Not Started Typing Test</a>
                        <div class="LinkDescription">
                            View User Who have Not Started Typing Test</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="BatchWiseStudentStatusTrackingReport.aspx">Batch-wise Students Status</a>
                        <div class="LinkDescription">
                            View users who have not started chapter batch wise.</div>
                    </div>
                </div>

                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="CompletedCourseStudentsReport.aspx">Completed Student Course Report</a>
                        <div class="LinkDescription">
                            View students list who have completed their course.</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="StudentProgressReport.aspx">Student Progress Report</a>
                        <div class="LinkDescription">
                            View students progress report.</div>
                    </div>
                </div>
                
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="StudentNotStartedCourse.aspx">Students who haven't started</a>
                        <div class="LinkDescription">
                            View report of Students who haven't started the course.</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="StudentStepwiseProgressReport.aspx">Student step wise progress report</a>
                        <div class="LinkDescription">
                            View Student step wise progress report.</div>
                    </div>
                </div> 
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="StudentTypingStatus.aspx">Student typing practice status</a>
                        <div class="LinkDescription">
                            View Student typing practice status - level wise accuracy, WPM, time spent.</div>
                    </div>
                </div>

                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="not comleted final quiz" class="Image" />
                    </div>
                    <div class="text">
                        <a href="NotCompleteFinalQuiz.aspx">Not completed final quiz</a>
                        <div class="LinkDescription">
                            View Student list who not completed their final quiz</div>
                    </div>
                </div>

                <%--<div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Seat_numbers.png" alt="Course Master" class="Image" />
                    </div>
                    <div class="text">
                        <a href="../SuperAdmin/ContactInquiry.aspx">Contact Inquiry </a>
                        <div class="LinkDescription">
                            Generate chapter wise chart for viewing Students progress.</div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
