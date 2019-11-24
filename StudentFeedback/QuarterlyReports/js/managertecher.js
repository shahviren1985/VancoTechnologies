var techers = [];
var CourseData = [];
$(document).ready(function () {
    $('#successmsg').hide();
    $('#errormsg').hide();
    $('#validationerrormsg').hide();

    $("#techerIndex").hide();
    $("#techerRemoveIndex").hide();
    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var RoleType = localStorage.getItem("roleType");
    var collegeCode = localStorage.getItem("collegeCode");


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
        url: GloableWebsite+'api/feedback/GetTeacherDetails/' + collegeCode,
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            techers = data;
            TecherListBind(techers);
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });

    $.ajax({
        url: '/api/feedback/GetAllCourse?CollegeCode=' + collegeCode,
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


    $('#txt_Cource').on('change', function (e) {
        $('#txt_SubCourse').find('option').remove().end().append('<option value="">Select SubCourse</option>');
        for (i = 0; i < CourseData.length; i++) {
            if (CourseData[i].Course == $("#txt_Cource").val()) {
                for (j = 0; j < CourseData[i].SubCourse.length; j++) {
                    $("#txt_SubCourse").append("<option value=\"" + CourseData[i].SubCourse[j] + "\">" + CourseData[i].SubCourse[j] + "</option>");
                }
            }
        }
    });

    $("#btnNewTecher").click(function () {
        $('#successmsg').hide();
        $('#errormsg').hide();
        $('#validationerrormsg').hide();
        ClearControl();
    });
    $("#btnClose").click(function () {
        ClearControl();
    });
    $("#btnClose1").click(function () {

        ClearControl();
    });

    $("#btnRemoveTeacher").click(function () {
        var index = parseInt($("#techerRemoveIndex").html());
        techers.splice(index, 1);
        TecherListBind(techers);
        $("#btn_Close2").click();
    });


    $("#btnSaveChanges").click(function () {
        $('#successmsg').hide();
        $('#errormsg').hide();
        $('#validationerrormsg').hide();
        $(".loader-img").show();

        $.ajax({
            type: "POST",
            url: GloableWebsite+"api/Feedback/SaveTeacherDetails?collegeCode=" + collegeCode,
            data: JSON.stringify(techers),
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                $(".loader-img").hide();
                $('#successmsg').fadeIn().delay(800).fadeOut();
            },

            error: function (jqXHR, status) {
                $(".loader-img").hide();
                $("#errormsg").show();
                console.log(jqXHR);
            }
        });
    });


    $("#btnAddEditTeacher").click(function () {
        $('#successmsg').hide();
        $('#errormsg').hide();
        $('#validationerrormsg').hide();

        var isValidForm = true;
        var techer = {
            teacherCode: $('#txt_TechderCode').val(),
            teacherName: $('#txt_TechderName').val(),
            subjectCode: $('#txt_SubjectCode').val(),
            subjectName: $('#txt_SubjectName').val(),
            course: $('#txt_Cource').val(),
            subCourse: $('#txt_SubCourse').val(),
            semester: $('#drp_Semester').val(),
        }

        if (techer.teacherCode == undefined || techer.teacherCode == "" || techer.teacherCode == null) {
            isValidForm = false;
            $("#txt_TechderCode").addClass("InvalidError");
        }
        else {
            $("#txt_TechderCode").removeClass("InvalidError");
        }

        if (techer.teacherName == undefined || techer.teacherName == "" || techer.teacherName == null) {
            isValidForm = false;
            $("#txt_TechderName").addClass("InvalidError");
        }
        else {
            $("#txt_TechderName").removeClass("InvalidError");
        }

        if (techer.subjectCode == undefined || techer.subjectCode == "" || techer.subjectCode == null) {
            isValidForm = false;
            $("#txt_SubjectCode").addClass("InvalidError");
        }
        else {
            $("#txt_SubjectCode").removeClass("InvalidError");
        }

        if (techer.subjectName == undefined || techer.subjectName == "" || techer.subjectName == null) {
            isValidForm = false;
            $("#txt_SubjectName").addClass("InvalidError");
        }
        else {
            $("#txt_SubjectName").removeClass("InvalidError");
        }

        if (techer.course == undefined || techer.course == "" || techer.course == null) {
            isValidForm = false;
            $("#txt_Cource").addClass("InvalidError");
        }
        else {
            $("#txt_Cource").removeClass("InvalidError");
        }

        if (techer.subCourse == undefined || techer.subCourse == "" || techer.subCourse == null) {
            isValidForm = false;
            $("#txt_SubCourse").addClass("InvalidError");
        }
        else {
            $("#txt_SubCourse").removeClass("InvalidError");
        }

        if (techer.semester == undefined || techer.semester == "" || techer.semester == null) {
            isValidForm = false;
            $("#drp_Semester").addClass("InvalidError");
        }
        else {
            $("#drp_Semester").removeClass("InvalidError");
        }
        if (isValidForm == true) {
            var index = parseInt($("#techerIndex").html());

            if (index == -1) {
                techers.push(techer);
            }
            else {
                techers[index].teacherCode = techer.teacherCode;
                techers[index].teacherName = techer.teacherName;
                techers[index].subjectCode = techer.subjectCode;
                techers[index].subjectName = techer.subjectName;
                techers[index].course = techer.course;
                techers[index].subCourse = techer.subCourse;
                techers[index].semester = techer.semester;
            }
            TecherListBind(techers);
            ClearControl();
            $("#btnClose").click();
        }
        else {
            $('#validationerrormsg').show();
        }
    });
});

function ClearControl() {
    $("#techerIndex").html("-1");
    $('#txt_TechderCode').val("");
    $('#txt_TechderName').val("");
    $('#txt_SubjectCode').val("");
    $('#txt_SubjectName').val("");
    $('#txt_Cource').val("");
    $('#txt_SubCourse').val("");

    $("#txt_TechderCode").removeClass("InvalidError");
    $("#txt_TechderName").removeClass("InvalidError");
    $("#txt_SubjectCode").removeClass("InvalidError");
    $("#txt_SubjectName").removeClass("InvalidError");
    $("#txt_Cource").removeClass("InvalidError");
    $("#txt_SubCourse").removeClass("InvalidError");
    //$('#drp_Semester').val();
}

function BindControl(index) {
    $('#successmsg').hide();
    $('#errormsg').hide();
    $('#validationerrormsg').hide();

    $("#techerIndex").html(index);
    $('#txt_TechderCode').val(techers[parseInt(index)].teacherCode);
    $('#txt_TechderName').val(techers[parseInt(index)].teacherName);
    $('#txt_SubjectCode').val(techers[parseInt(index)].subjectCode);
    $('#txt_SubjectName').val(techers[parseInt(index)].subjectName);
    $('#txt_Cource').val(techers[parseInt(index)].course);
    $('#txt_SubCourse').val(techers[parseInt(index)].subCourse);
    $('#drp_Semester').val(techers[parseInt(index)].semester);
}

function BindIndex(index) {
    $("#techerRemoveIndex").html(index);
}
function TecherListBind(techers) {
    var techerList = "";
    for (var i = 0; i < techers.length; i++) {
        techerList = techerList + '<tr>';
        techerList = techerList + '<td class="text-center">' + (i + 1) + '</td>';
        techerList = techerList + '<td>' + techers[i].teacherCode + '</td>';
        techerList = techerList + '<td>' + techers[i].teacherName + '</td>';
        techerList = techerList + '<td>' + techers[i].subjectCode + '</td>';
        techerList = techerList + '<td>' + techers[i].subjectName + '</td>';
        techerList = techerList + '<td>' + techers[i].course + '</td>';
        techerList = techerList + '<td>' + techers[i].subCourse + '</td>';
        techerList = techerList + '<td>' + techers[i].semester + '</td>';
        techerList = techerList + '<td class="text-center" > <a onclick="BindControl(' + i + ')" title="Edit" data-toggle="modal" data-target="#myModal" class="glyphicon glyphicon-edit"></a> <a  onclick="BindIndex(' + i + ')" title="Delete" data-toggle="modal" data-target="#deleterec" class="glyphicon glyphicon-trash"></a> </td>';
        techerList = techerList + '</tr>';
    }

    if (techers == undefined || techers == null || techers.length == 0) {
        techerList = techerList + '<tr> <td colspan="9"> No record found <td> ';
        techerList = techerList + '</tr>';
    }

    $("#techerList").html(techerList);


}

