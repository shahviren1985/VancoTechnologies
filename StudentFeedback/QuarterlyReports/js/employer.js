$(document).ready(function () {
    $("#exitForm").hide();
    $("#feedbackForm").show();
    $("#successmsg").hide();
    $("#errormsg").hide();
    $("#validationerrormsg").hide();

    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var collegeCode = localStorage.getItem("collegeCode");
    var isFinalYearStudent = localStorage.getItem("isFinalYearStudent");
    var isExistFormSubmitted = localStorage.getItem("isExistFormSubmitted");

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
    
    /*if (localStorage.getItem("course") == "nonteachingstaff") {
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
    }*/
    

    $.ajax({
        url: GloableWebsite + 'api/Feedback/GetFeedbackDetails?CollegeCode=' + collegeCode + '&userType=Employer',
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
            else if (questions[i].type == 'text') {
                var Ans1 = $("#txt_" + questions[i].id + "_1").val();
                if (Ans1 == undefined || Ans1 == null || Ans1 == "") {
                    validationArraray.push("txt_" + questions[i].id + "_1");
                }
                else {
                    $("#txt_" + questions[i].id + "_1").removeClass("InvalidError");
                }
                var Ans2 = $("#txt_" + questions[i].id + "_2").val();
                if (Ans2 == undefined || Ans2 == null || Ans2 == "") {
                    validationArraray.push("txt_" + questions[i].id + "_2");
                }
                else {
                    $("#txt_" + questions[i].id + "_2").removeClass("InvalidError");
                }
                ansData = ansData + ", " + '"' + questions[i].id + '" : ["' + Ans1 + '", "' + Ans2 + '"]';
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
        questionList = questionList + '<div class="ques"> <strong> <span>' + (i + 1) + '. </span>' + questions[i].question + ' </strong> </div> <div id="dv_' + questions[i].id + '">';

        if (questions[i].type == 'radio') {
            for (var j = 0; j < questions[i].optionValues.length; j++) {
                questionList = questionList + '<label class="radio"> <input type="radio" value="' + (j + 1) + '" name="rdo_' + questions[i].id + '"> ' + questions[i].optionValues[j] + ' </label>';
            }
        }
        else if (questions[i].type == 'text') {
            questionList = questionList + '<textarea class="form-control" id="txt_' + questions[i].id + '_1" name="txt_' + questions[i].id + '" rows="2" placeholder="' + questions[i].placeHolder + '"></textarea>';
            questionList = questionList + '<textarea class="form-control" id="txt_' + questions[i].id + '_2" name="txt_' + questions[i].id + '" rows="2" placeholder="' + questions[i].placeHolder + '"></textarea>';
        }

        questionList = questionList + '</div></div>';
    }

    $("#feedbackQuestion").html(questionList);
}

function PostAnsData(_data, teacherCode, subjectCode) {
    $(".loader-img").show();
    $.ajax({
        type: "POST",
        url: GloableWebsite + "api/CommonFeedback/AddFeedback",
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