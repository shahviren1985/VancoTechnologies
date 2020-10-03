$(document).ready(function () {
    $("#exitForm").hide();
    $("#feedbackForm").hide();
    $("#successmsg").hide();
    $("#errormsg").hide();
    // hide validation error div
    $("#validationerrormsg").hide();

    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var collegeCode = localStorage.getItem("collegeCode");
    var isFinalYearStudent = localStorage.getItem("isFinalYearStudent");
    var isExistFormSubmitted = localStorage.getItem("isExistFormSubmitted");

    if (isFinalYearStudent == 'true' && isExistFormSubmitted == 'false') {
        $("#exitForm").show();
    }
    $("#userName").html("Welcome " + userName);

    if (userCode == null || userCode == undefined || !userCode) {
        localStorage.clear();
        window.location.href = "index.html";
    }

    $("#logout").click(function () {
        localStorage.clear();
        window.location.href = "index.html";
    });

    var questions = [];
    var techers = [];
    
    if (localStorage.getItem("course") == "nonteachingstaff") {
        $.ajax({
            url: GloableWebsite+'api/feedback/GetAllNonTeachingStaff?CollegeCode=' + collegeCode,
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                var techers = data;
                var indexes = $.map(data, function (obj, index) {
                    if (obj.teacherName == localStorage.getItem("userName")) {
                        return index;   
                    }
                })
                console.log(indexes)
                var test = [];
                techers.splice(indexes, 1);
                console.log(techers)
                TecherListBind(techers);
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
            },
            type: 'GET'
        });
    } else {
        $.ajax({
            url: GloableWebsite+'api/Feedback/GetTeacherDetails/' + collegeCode,
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                var techers = data;
                TecherListBind(techers);
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
            },
            type: 'GET'
        });
    }
    

    $.ajax({
        url: GloableWebsite + 'api/Feedback/GetFeedbackDetails?CollegeCode=' + collegeCode + '&userType=Teacher',
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            questions = data;
            FeedbackQuestionListBind(questions);
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });


    $("#btnSubmitAns").click(function () {

        $("#successmsg").hide();
        $("#errormsg").hide();
        // hide validation error div
        $("#validationerrormsg").hide();
        var value = $('input[name=rdoteacher]:checked').val().split('_');
        var teacherCode = value[0];
        var subjectCode = value[1];

        var ansData = '{"userId" : "' + userCode + '","teacherCode" : "' + teacherCode + '" , "subjectCode" :  "' + subjectCode + '", "collegeCode" : "' + collegeCode + '"';

        var validationArraray = [];

        for (var i = 0; i < questions.length; i++) {
            if (questions[i].type == 'radio') {
                var ans = $('input[name=rdo_' + questions[i].id + ']:checked').val();
                if (ans == undefined) {
                    ans = '';
                    validationArraray.push('dv_' + questions[i].id);
                }
                else {
                    $("#dv_" + questions[i].id).removeClass("InvalidError");
                }
                ansData = ansData + ", " + '"' + questions[i].id + '" : "' + ans + '"';
            }
            else if (questions[i].type == 'dropdown') {
                var ansDd = $('select[id=dd_' + questions[i].id + ']').children("option:selected").val();
                if (ansDd == undefined) {
                    ansDd = '';
                    validationArraray.push('dv_' + questions[i].id);
                }
                else {
                    $("#dv_" + questions[i].id).removeClass("InvalidError");
                }
                ansData = ansData + ", " + '"' + questions[i].id + '" : "' + ansDd + '"';
            }
            else if (questions[i].type == 'text') {
                var Ans1 = $("#txt_" + questions[i].id + "_1").val();
                if (Ans1 == undefined || Ans1 == null || Ans1 == "") {
                    validationArraray.push("txt_" + questions[i].id + "_1");
                }
                else {
                    $("#txt_" + questions[i].id + "_1").removeClass("InvalidError");
                }
                /*var Ans2 = $("#txt_" + questions[i].id + "_2").val();
                if (Ans2 == undefined || Ans2 == null || Ans2 == "") {
                    validationArraray.push("txt_" + questions[i].id + "_2");
                }
                else {
                    $("#txt_" + questions[i].id + "_2").removeClass("InvalidError");
                }*/
                //ansData = ansData + ", " + '"' + questions[i].id + '" : ["' + Ans1 + '", "' + Ans2 + '"]';
                ansData = ansData + ", " + '"' + questions[i].id + '" : "' + Ans1 + '"';
            }
        }

        ansData = ansData + '}';
        if (validationArraray.length == 0) {

            PostAnsData(ansData, teacherCode, subjectCode);
        }
        else {
            // Show validation error div

            for (var i = 0; i < validationArraray.length; i++) {
                $("#" + validationArraray[i]).addClass("InvalidError");
            }
            //window.location.href = "#" + validationArraray[0];
            $("#validationerrormsg").show();
            window.location.href = "#validationerrormsg";
        }
    });
});


function FeedbackQuestionListBind(questions) {
    var questionList = '<div class="form-group">';
    for (var i = 0; i < questions.length; i++) {

        if (questions[i].id == "a0") {
            questionList = questionList + '<div class="ques"> <strong>' + questions[i].question + ' </strong> </div> <div id="dv_' + questions[i].id + '">';
            continue;
        }
        else {
            questionList = questionList + '<div class="ques"> <strong> <span>' + (i) + '. </span>' + questions[i].question + ' </strong> </div> <div id="dv_' + questions[i].id + '">';
        }
        if (questions[i].type == 'radio') {
            for (var j = 0; j < questions[i].optionValues.length; j++) {
                questionList = questionList + '<label class="radio"> <input type="radio" value="' + questions[i].optionValues[j] + '" name="rdo_' + questions[i].id + '"> ' + questions[i].optionValues[j] + ' </label>';
            }
        }
        else if (questions[i].type == 'dropdown') {
            questionList += "<label class='dropdown'><select id='dd_" + questions[i].id+"'><option value='-1' selected='selected'>---Select---</option>";
            for (var q = 0; q < questions[i].optionValues.length; q++) {
                questionList = questionList + '<option value="' + questions[i].optionValues[q] + '">' + questions[i].optionValues[q] + ' </option>';
            }

            questionList += "</select>";
        }
        else if (questions[i].type == 'text') {
            questionList = questionList + '<textarea class="form-control" id="txt_' + questions[i].id + '_1" name="txt_' + questions[i].id + '" rows="2" placeholder="' + questions[i].placeHolder + '"></textarea>';
            //questionList = questionList + '<textarea class="form-control" id="txt_' + questions[i].id + '_2" name="txt_' + questions[i].id + '" rows="2" placeholder="' + questions[i].placeHolder + '"></textarea>';
        }

        questionList = questionList + '</div></div>';
    }

    $("#feedbackQuestion").html(questionList);
}

function TecherListBind(techers) {
    var techerList = "";
    var feedbackStatus = JSON.parse(localStorage.getItem("feedbackStatus"));

    for (var i = 0; i < techers.length; i++) {
        var isDisabled = "";
        if (feedbackStatus != null || feedbackStatus != undefined) {
            for (var j = 0; j < feedbackStatus.length; j++) {
                if (feedbackStatus[j].TeacherCode == techers[i].teacherCode && feedbackStatus[j].SubjectCode == techers[i].subjectCode) {
                    isDisabled = "disabled";
                }
            }
        }

        if (localStorage.course.toLowerCase() == techers[i].course.toLowerCase() && localStorage.subCourse.toLowerCase() == techers[i].subCourse.toLowerCase() && localStorage.currentSemester.toLowerCase() == techers[i].semester.toLowerCase()) {
            var code = techers[i].subjectCode + "-" + techers[i].teacherCode;
            techerList = techerList + "<li index ='" + i + "' code='" + code + "'> <a><div><input  " + isDisabled + "  onclick='techerSelection(this)' type='radio' id='" + code + "' value='" + techers[i].teacherCode + '_' + techers[i].subjectCode + '_' + techers[i].teacherName + '_' + techers[i].subjectName + "' text='" + code + "'  name='rdoteacher' /><label for='" + code + "'><span>" + techers[i].teacherName + "</span> <span>" + techers[i].subjectCode + "  </span> </label></div></a></li>";
        }
    }

    $("#techerList").html(techerList);
}

function PostAnsData(_data, teacherCode, subjectCode) {
    $(".loader-img").show();
    $.ajax({
        type: "POST",
        url: GloableWebsite+"api/Feedback/AddTeacherFeedbacks",
        data: _data,
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            $(".loader-img").hide();
            $('#successmsg').fadeIn().delay(800).fadeOut();
            $("#feedbackForm").hide();
            var feedbackStatus = JSON.parse(localStorage.getItem("feedbackStatus"));

            if (feedbackStatus == undefined || feedbackStatus == null) {
                feedbackStatus = [];
            }
            feedbackStatus.push({ 'TeacherCode': teacherCode, 'SubjectCode': subjectCode });
            localStorage.setItem("feedbackStatus", JSON.stringify(feedbackStatus))
            setTimeout(function () { location.reload(true); }, 800);

            //techerList();
            //QuestionList();
        },

        error: function (jqXHR, status) {
            $(".loader-img").hide();
            $("#errormsg").show();
            console.log(jqXHR);
        }
    });
}

function techerSelection(obj) {
    $("#successmsg").hide();
    $("#errormsg").hide();
    $("#validationerrormsg").hide();
    var techer = obj.value.split("_");
    $("#techerCode").html("Subject Code: " + techer[1]);
    $("#techerName").html("Teacher Name: " + techer[2]);
    $("#subject").html("Subject: " + techer[3]);
    $("#feedbackForm").show();
}

function techerList() {

    $.ajax({
        url: GloableWebsite+'api/Feedback/GetTeacherDetails',
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            var techers = data;
            TecherListBind(techers);
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });
}

function QuestionList() {

    $.ajax({
        url: GloableWebsite+'api/Feedback/GetFeedbackDetails',
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            questions = data;
            FeedbackQuestionListBind(questions);
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });
}