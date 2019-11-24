$(document).ready(function () {

    localStorage.clear();

    $(".btnLogin").click(function () {
        var mobileNo = $("#mobileNo").val();
        var lastName = $("#lastName").val();
        var data = { "mobileNumber": mobileNo, "lastName": lastName };
        $(".loader-img").show();
        Login(data);
    });
});


function Login(data) {// pass your data in method
    $.ajax({
        type: "POST",
        url: GloableWebsite + "api/Feedback/Login",
        data: JSON.stringify(data),
        contentType: "application/json",

        success: function (data, status, jqXHR) {
            $(".loader-img").hide();
            var student = data;
            localStorage.setItem("userCode", student.userId);
            localStorage.setItem("userName", student.firstName + " " + student.lastName);
            localStorage.setItem("roleType", student.roleType);
            localStorage.setItem("collegeCode", student.collegeCode);
            if (student.roleType == "Customer") {
                localStorage.setItem("course", student.course);
                localStorage.setItem("subCourse", student.subCourse);
                localStorage.setItem("isActive", student.isActive);
                localStorage.setItem("currentSemester", student.currentSemester);
                localStorage.setItem("isFinalYearStudent", student.isFinalYearStudent);
                localStorage.setItem("feedbackStatus", JSON.stringify(student.feedbackStatus));
                localStorage.setItem("isExistFormSubmitted", student.isExistFormSubmitted);
                window.location.href = "Feedback.html";
            }
            else if (student.roleType == "Admin") {
                if (student.mobileNumber == "mdshah") {
                    window.location.href = "StaffDashboard.html";
                }
                else {
                    window.location.href = "GraphDashboard.html";
                }
            }
            else if (student.roleType == "Employer" || student.roleType == "Parent" || student.roleType == "Teacher" || student.roleType == "Alumni" || student.roleType == "Student") {
                window.location.href = "StakeholderFeedback.html";
            }
            else if (student.roleType == "PeerOwnDepartment" || student.roleType == "PeerOtherDepartment" || student.roleType == "PeerAnyDepartment" ||
                student.roleType == "Library" || student.roleType == "CurriculumEvaluation" || student.roleType == "JobSatisfactionWorkPlace" ||
                student.roleType == "JobSatisfactionEmployeeRelation" || student.roleType == "JobSatisfactionTechnologyResource" ||
                student.roleType == "Academic Administrator") {
                window.location.href = "MDSMC-Dashboard.html";
            }
        },

        error: function (jqXHR, status) {
            $(".loader-img").hide();
            console.log(jqXHR);
            $("#info").html("Login failed, please try again!!!");
        }
    });
}



