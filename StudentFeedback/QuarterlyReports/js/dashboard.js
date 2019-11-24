var techers = [];
var studentData = [];
$(document).ready(function () {
    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var RoleType = localStorage.getItem("roleType");
    var collegeCode = localStorage.getItem("collegeCode");
    $('#tblSDPending').hide();
    $('#tblSDInComplete').hide();
  
    $("#userName").html("Welcome " + userName);

    if (userCode == null || userCode == undefined || !userCode) {
        localStorage.clear();
        window.location.href = "index.html";
    }

    $("#logout").click(function () {
        localStorage.clear();
        window.location.href = "index.html";

    });

    $.ajax({
        url: GloableWebsite + 'api/feedback/GetAllCourse?CollegeCode=' + collegeCode,
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            CourseData = data;
            $('#txt_Cource').find('option').remove().end().append('<option value="">Select Course</option>');
            for (i = 0; i < data.length; i++) {
                $("#txt_Cource").append("<option value=\"" + data[i].Course + "\">" + data[i].Course + "</option>");
            }
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });


    $("#btnFeedbackReport").click(function () {
        var url = GloableWebsite + "api/Feedback/ExportFeedbackDetails?collageCode="+ collegeCode+"&fromDate=2000-01-01T00:00:00&toDate=";
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var hh = today.getHours();
        if (hh < 10) {
            hh = '0' + hh;
        }
        var mi = today.getMinutes();
        if (mi < 10) {
            mi = '0' + mi;
        }
        var ss = today.getSeconds();
        if (ss < 10) {
            ss = '0' + ss;
        }
        var today = yyyy + '-' + mm + '-' + dd + "T" + hh + ":" + mi + ":" + ss;
        console.log(today);;
        url = url + today;
        window.open(url, '_blank');
    });

    $("#btnGetPendingRecords").click(function () {
        $('#tblSDPending').hide();
        $('#tblSDInComplete').hide();

        var isValidForm = true;
        var course = $('#txt_Cource').val();

        if (course == undefined || course == "" || course == null) {
            isValidForm = false;
            $("#txt_Cource").addClass("InvalidError");
        }
        else {
            $("#txt_Cource").removeClass("InvalidError");
        }
        if (!isValidForm)
        {
            return;
        }

        var semester = $('#drp_Semester').val();
        $.ajax({
            type: "Get",
            url: GloableWebsite + "api/feedback/GetMissingStudentFeedbackList?collageCode=" + collegeCode + "&course=" + course + "&sem=" + semester + "",
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                    studentData = data;
                    StudentListBind(studentData);
                    $("#tblSDPending").show();
            },

            error: function (jqXHR, status) {
                console.log(jqXHR);
            }
        });
    });

    $("#btnGetInCompeleteRecords").click(function () {
        $('#tblSDPending').hide();
        $('#tblSDInComplete').hide();
        var isValidForm = true;
        var course = $('#txt_Cource').val();

        if (course == undefined || course == "" || course == null) {
            isValidForm = false;
            $("#txt_Cource").addClass("InvalidError");
        }
        else {
            $("#txt_Cource").removeClass("InvalidError");
        }
        if (!isValidForm) {
            return;
        }
        var semester = $('#drp_Semester').val();
        $.ajax({
            type: "Get",
            url: GloableWebsite + "api/feedback/GetMissingSFeedbackTeacherWise?collageCode=" + collegeCode + "&course=" + course + "&sem=" + semester + "",
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                    studentData = data;
                    StudentInCompeleteListBind(studentData);
                    $("#tblSDInComplete").show();
              },

            error: function (jqXHR, status) {
                console.log(jqXHR);
            }
        });
    });


    function StudentListBind(studentData) {
        var StudentDataList = "";
        if (studentData == undefined || studentData == null || studentData.length == 0) {
            StudentDataList = StudentDataList + '<tr> <td colspan="9"> No record found <td> ';
            StudentDataList = StudentDataList + '</tr>';
        }
        else {
            for (var i = 0; i < studentData.length; i++) {
                StudentDataList = StudentDataList + '<tr>';
                StudentDataList = StudentDataList + '<td class="text-center">' + (i + 1) + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].userId + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].firsName + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].mobileNumber + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].lastName + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].course + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].subCourse + '</td>';
                StudentDataList = StudentDataList + '</tr>';
            }
        }
        $("#studentDtList").html(StudentDataList);
    }

    function StudentInCompeleteListBind(studentData) {
        var StudentDataList = "";
        if (studentData == undefined || studentData == null || studentData.length == 0) {
            StudentDataList = StudentDataList + '<tr> <td colspan="9"> No record found <td> ';
            StudentDataList = StudentDataList + '</tr>';
        }
        else {
            for (var i = 0; i < studentData.length; i++) {
                StudentDataList = StudentDataList + '<tr>';
                StudentDataList = StudentDataList + '<td class="text-center">' + (i + 1) + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].userId + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].firsName + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].mobileNumber + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].lastName + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].course + '</td>';
                StudentDataList = StudentDataList + '<td>' + studentData[i].subCourse + '</td>';

                var tCodeArray = studentData[i].teacherCodes.join();
                var tNameArray = studentData[i].teacherNames.join();

                StudentDataList = StudentDataList + '<td>' + tCodeArray + '</td>';
                StudentDataList = StudentDataList + '<td>' + tNameArray + '</td>';
                StudentDataList = StudentDataList + '</tr>';
            }
        }
        $("#studentDtCompeleteList").html(StudentDataList);
    }


    $("#btnMissingFeedbackReport").click(function () {
        var url = GloableWebsite + "api/Feedback/ExportMissingStudentFeedback?collageCode=" + collegeCode + "";
        window.open(url, '_blank');
    });

    $("#btnExitFormReport").click(function () {
        var url = GloableWebsite + "api/Feedback/ExportExiFormDetails?collageCode=" + collegeCode + "&fromDate=2000-01-01T00:00:00&toDate=";
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var hh = today.getHours();
        if (hh < 10) {
            hh = '0' + hh;
        }
        var mi = today.getMinutes();
        if (mi < 10) {
            mi = '0' + mi;
        }
        var ss = today.getSeconds();
        if (ss < 10) {
            ss = '0' + ss;
        }
        var today = yyyy + '-' + mm + '-' + dd + "T" + hh + ":" + mi + ":" + ss;
        console.log(today);
        url = url + today;
        window.open(url, '_blank');
    });

    $("#btnMissingExitFormReport").click(function () {
        var url = GloableWebsite + "api/Feedback/ExportNotInFeedbackUserFinalYear?Coursename=" + collegeCode + "";
        window.open(url, '_blank');
    });
});


