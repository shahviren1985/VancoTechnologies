function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return  encodeURIComponent(vars);
}

function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}

var userType = getUrlParameter("type");
var crn = getUrlParameter("crn");
var collegeCode = getUrlParameter("cc");
var specialisation = getUrlParameter("sp");
var ipAddress = "";
var fwdIPAddresses = "";
$(document).ready(function () {
    $("#exitForm").hide();
    $("#commonfeedbackForm").show();
    $("#successmsg").hide();
    $("#errormsg").hide();
    $("#validationerrormsg").hide();
    var uType = "";
    switch (userType) {
        case "TeacherPO":
            uType = "Teacher Program Outcome";
            break;
        case "AlumniPODC":
            uType = "Alumni (DC) Program Outcome";
            break;
        case "AlumniPOECCE":
            uType = "Alumni (ECCE) Program Outcome";
            break;
        case "AlumniPOFND":
            uType = "Alumni (FND) Program Outcome";
            break;
        case "AlumniPOHTM":
            uType = "Alumni (HTM) Program Outcome";
            break;
        case "AlumniPOIDRM":
            uType = "Alumni (IDRM) Program Outcome";
            break;
        case "AlumniPOMCE":
            uType = "Alumni (MCE) Program Outcome";
            break;
        case "AlumniPOTAD":
            uType = "Alumni (TAD) Program Outcome";
            break;
        case "ParentsPODC":
            uType = "Parents (DC) Program Outcome";
            break;
        case "ParentsPOECCE":
            uType = "Parents (ECCE) Program Outcome";
            break;
        case "ParentsPOFND":
            uType = "Parents (FND) Program Outcome";
            break;
        case "ParentsPOHTM":
            uType = "Parents (HTM) Program Outcome";
            break;
        case "ParentsPOIDRM":
            uType = "Parents (IDRM) Program Outcome";
            break;
        case "ParentsPOMCE":
            uType = "Parents (MCE) Program Outcome";
            break;
        case "ParentsPOTAD":
            uType = "Parents (TAD) Program Outcome";
            break;
		case "SSSFY":
		case "SSSSY":
		case "SSSTY":
		case "SSSMFY":
		case "SSSMSY":
			uType = "Student Satisfaction Survey";
			break;
        default:
            uType = userType;
    }

    $(".panel-title").html(uType + " Feedback Form");
    //$("title").html(userType + " Feedback Form");

    var questions = [];

    $.ajax({
        url: GloableWebsite + 'api/Feedback/GetFeedbackQuestions?CollegeCode=' + collegeCode + '&userType=' + userType,
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            ipAddress = jqXHR.getResponseHeader('ipaddress');
            fwdIPAddresses = jqXHR.getResponseHeader('FwdIPAddresses');
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
        var ansData = '{"userId" : "102' + crn + '","teacherCode" : "" , "subjectCode" :  "", "specialisation" : "'+specialisation+'", "collegeCode" : ' + collegeCode + ', "userType": "' + userType + "\"";

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
                    ansData = ansData + ", " + '"' + questions[i].id + '" : ["' + Ans1 + '"]';
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
                questionList = questionList + '<label class="radio"> <input type="radio" value="' + (j + 1) + '" name="rdo_' + questions[i].id + '"> ' + questions[i].optionValues[j] + ' </label>';
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
    console.log("1",_data);
    var model = JSON.parse(_data);
    console.log("2", _data);
    //model.userType = localStorage.getItem("roleType");
    model.ipAddress = ipAddress;
    model.fwdIPAddresses = fwdIPAddresses;
    _data = JSON.stringify(model);
    console.log("3", _data);
    $.ajax({
        type: "POST",
        url: GloableWebsite + "api/CommonFeedback/AddCommonFeedback",
        data: _data,
        contentType: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('collegeCode', collegeCode);
            xhr.setRequestHeader('userType', userType);
        },
        success: function (data, status, jqXHR) {
            $(".loader-img").hide();
            $('#successmsg').fadeIn().delay(800).fadeOut();
            $("#commonfeedbackForm").hide();
            var feedbackStatus = [];
            alert("Feedback submitted successfully.");
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