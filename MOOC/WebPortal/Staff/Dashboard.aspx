<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Staff_Dashboard"
    MasterPageFile="~/Master/StaffMasterPage.master" Title="MOOC Academy - Staff - Dashboard" %>

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
            width: 75px;
            height: 75px;
        }
        .main-div
        {
            float: left;
            margin-top: 5%;
            width: 100%;
        }
    </style>
    <div class="main-div">
        <div class="Box BoxOrange" style="height: 480px">
            <div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/manage-course.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="ManageCourse.aspx">Manage Course</a>
                        <div class="LinkDescription">
                            Manage courses, add new course, edit existing course and delete course</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/manage-chapters.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="ManageChapters.aspx">Manage Chapters</a>
                        <div class="LinkDescription">
                            Manage Chapters</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/manage-sections.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="ManageSection.aspx">Manage Section</a>
                        <div class="LinkDescription">
                            Manage Section</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/manage-quiz.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="ManageQuestions.aspx">Manage Questions</a>
                        <div class="LinkDescription">
                            Manage Questions</div>
                    </div>
                </div>
                <%--<div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/MarksEntry.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="AddQuestion.aspx">Add Questions</a>
                        <div class="LinkDescription">
                            Add Questions</div>
                    </div>
                </div>--%>
                <%--<div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/MarksEntry.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="EditContent.aspx">Edit Content</a>
                        <div class="LinkDescription">
                           Edit Content</div>
                    </div>
                </div>--%>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/manage-test.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="TestDetails.aspx">Manage Test</a>
                        <div class="LinkDescription">
                            Manage Test</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/manage-content.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="ManageContent.aspx">Manage Content</a>
                        <div class="LinkDescription">
                            Manage Content</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/mapp-students-course.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="Student-CourseMapper.aspx">Map Students to Course</a>
                        <div class="LinkDescription">
                            Mapped students to selected course</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Reports.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="StudentTestPerformanceReport.aspx">Student Test Reports</a>
                        <div class="LinkDescription">
                            Reports</div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="../static/images/Reports.png" alt="Course Master" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="GraphicalTestPerformance.aspx">Graphical Student Test Reports</a>
                        <div class="LinkDescription">
                            Reports</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
