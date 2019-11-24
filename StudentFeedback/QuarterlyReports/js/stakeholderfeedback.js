$(document).ready(function () {
    $("#exitForm").hide();
    $("#commonfeedbackForm").show();
    $("#successmsg").hide();
    $("#errormsg").hide();
    $("#validationerrormsg").hide();

    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var collegeCode = localStorage.getItem("collegeCode");
    var isFinalYearStudent = localStorage.getItem("isFinalYearStudent");
    var isExistFormSubmitted = localStorage.getItem("isExistFormSubmitted");
    var role = localStorage.getItem("course") == "M.Sc." ? "PG" : localStorage.getItem("roleType");

    $("#userName").html("Welcome " + userName);
    $(".panel-title").html(role + " Feedback Form");
    $("title").html(role + " Feedback Form");
    if (userCode == null || userCode == undefined || !userCode) {
        localStorage.clear();
        window.location.href = "index.html";
    }

    $("#logout").click(function () {
        localStorage.clear();
        window.location.href = "index.html";
    });

    var questions = [];
    
    

    $.ajax({
        url: GloableWebsite + 'api/Feedback/GetFeedbackDetails?CollegeCode=' + collegeCode + '&userType=' + role,
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

        /*var value = $('input[name=rdoteacher]:checked').val().split('_');
        var teacherCode = value[0];
        var subjectCode = value[1];
        */
        var ansData = '{"userId" : "' + userCode + '","teacherCode" : "" , "subjectCode" :  "", "collegeCode" : "' + collegeCode + '"';

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
                var Ans2 = $("#txt_" + questions[i].id + "_2")[0];
                if (Ans2 != undefined) {
                    Ans2 = $("#txt_" + questions[i].id + "_2").val();
                    if (Ans2 == undefined || Ans2 == null || Ans2 == "") {
                        validationArraray.push("txt_" + questions[i].id + "_2");
                    }

                    ansData = ansData + ", " + '"' + questions[i].id + '" : ["' + Ans1 + '", "' + Ans2 + '"]';
                }
                else {
                    $("#txt_" + questions[i].id + "_2").removeClass("InvalidError");
                    ansData = ansData + ", " + '"' + questions[i].id + '" : "' + Ans1 + '"';
                }
            }
            else if (questions[i].type == 'dropdown') {
                var ans = $('select[name=dd_' + questions[i].id + ']').val();
                if (ans == undefined || ans == "-1") {
                    ans = '';
                    validationArraray.push('dv_' + questions[i].id);
                }
                else {
                    $("#dv_" + questions[i].id).removeClass("InvalidError");
                }
                ansData = ansData + ", " + '"' + questions[i].id + '" : "' + ans + '"';
            }
        }

        ansData = ansData + '}';
        if (validationArraray.length == 0) {

            PostAnsData(ansData);
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
    var serialNumber = 1;
    for (var i = 0; i < questions.length; i++) {

        if (questions[i].type != 'header' && questions[i].type != 'label') {
            questionList = questionList + '<div class="ques"> <strong> <span>' + serialNumber + '. </span>' + questions[i].question + ' </strong> </div> <div id="dv_' + questions[i].id + '">';
            serialNumber++;
        }
        if (questions[i].type == 'radio') {
            for (var j = 0; j < questions[i].optionValues.length; j++) {
                questionList = questionList + '<label class="radio"> <input type="radio" value="' + questions[i].optionValues[j] + '" name="rdo_' + questions[i].id + '"> ' + questions[i].optionValues[j] + ' </label>';
            }
        }
        else if (questions[i].type == 'text') {
            questionList = questionList + '<textarea class="form-control" id="txt_' + questions[i].id + '_1" name="txt_' + questions[i].id + '" rows="2" placeholder="' + questions[i].placeHolder + '"></textarea>';
            //questionList = questionList + '<textarea class="form-control" id="txt_' + questions[i].id + '_2" name="txt_' + questions[i].id + '" rows="2" placeholder="' + questions[i].placeHolder + '"></textarea>';
        }
        else if (questions[i].type == 'dropdown') {
            var element = "<select name='dd_" + questions[i].id + "'><option selected value='-1'>--Select Year--</option>"
            for (var j = 0; j < questions[i].optionValues.length; j++) {
                element += "<option value='" + questions[i].optionValues[j] + "'>" + questions[i].optionValues[j] + "</option>";
            }
            questionList = questionList + element + "</select>";
        }
        else if (questions[i].type == 'header') {
            questionList = questionList + '<div rows="2"><h3>' + questions[i].question + '</h3></div>';
        }

        questionList = questionList + '</div></div>';
    }

    $("#feedbackQuestion").html(questionList);
}

function PostAnsData(_data) {
    $(".loader-img").show();

    var model = JSON.parse(_data);
    model.userType = localStorage.getItem("roleType");
    _data = JSON.stringify(model);

    $.ajax({
        type: "POST",
        url: GloableWebsite + "api/CommonFeedback/AddFeedback",
        data: _data,
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            $(".loader-img").hide();
            $('#successmsg').fadeIn().delay(800).fadeOut();
            $("#commonfeedbackForm").hide();
            var feedbackStatus = [];
            alert(localStorage.getItem("roleType") + " Feedback submitted successfully.");
            setTimeout(function () { location.reload(true); }, 800);
        },

        error: function (jqXHR, status) {
            $(".loader-img").hide();
            $("#errormsg").show();
            alert("An error occurred while submitting " + localStorage.getItem("roleType") + " Feedback.");
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
    $("#commonfeedbackForm").show();
}