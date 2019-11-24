var ExamSystem = angular.module("SelectElectiveCtrlApp", []);
ExamSystem.controller("SelectElectiveCtrl", function ($scope) {
    var months = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    var program = getUrlVars()["program"]; // Regular - Honors
    var specialization = getUrlVars()["specialization"];
    var sem = getUrlVars()["semester"];
    var rollNumber = decodeURIComponent(getUrlVars()["rollNumber"]);
    var name = decodeURIComponent(getUrlVars()["name"]);
    //var crn = getUrlVars()["crn"];

    $("#StudentName").html(name);
    $("#RollNumber").html(rollNumber);

    if (program.toLowerCase() == "regular") {
        //$("#ddProgram :selected").val("Regular");
        $("#ddProgram option[value='Regular']").attr("selected", true);
        $("#ddProgram").attr("disabled", "disabled");
    }
    else if (program.toLowerCase() == "honors") {
        $("#ddProgram option[value='Honors']").attr("selected", true);
        $("#ddProgram").attr("disabled", "disabled");
    }

    if (sem > 0 && sem < 7) {
        //$("#ddSemester :selected").val(sem);
        $("#ddSemester option[value='" + sem + "']").attr("selected", true);
        $("#ddSemester").attr("disabled", "disabled");
    }

    CallAPI('User/GetGEList?Course=bsc&specialization=' + program + '&sem=' + sem + '', 'GET', '').done(function (data) {
        PrepareFYElectives(data);
    });
});

function GetSpecialization() {
    var specialization = decodeURIComponent(getUrlVars()["specialization"]);

    switch (specialization) {
        case "Developmental Counseling":
            specialization = "DC";
            break;
        case "Early Childhood Care and Education":
            specialization = "ECCE";
            break;
        case "Food, Nutrition and Dietitics":
            specialization = "FND";
            break;
        case "Hospitality & Tourism Management":
            specialization = "HTM";
            break;
        case "Interior Designing and Resource Management":
            specialization = "IDRM";
            break;
        case "Mass Communication & Extension":
            specialization = "MCE";
            break;
        case "Textile & Apparel Designing":
            specialization = "TAD";
            break;
    }

    return specialization;
}

function PrepareFYElectives(data) {
    var specialization = GetSpecialization();

    var sem = getUrlVars()["semester"];
    for (i = 0; i < data.length; i++) {
        if (data[i].specialisationCode.toLowerCase() == specialization.toLowerCase()) {
            var electives = data[i].ElectiveSubject;
            for (k = 0; k < electives.length; k++) {
                var subjects = electives[k].Subject;
                var electiveNo = sem < 3 ? (k + 1) : "";
                var radioList = "<div class='elective'><div><b>Please choose Elective " + electiveNo + ":</b></div>";
                for (j = 0; j < subjects.length; j++) {
                    if (sem > 0 && sem < 5) {
                        radioList += "<input type='radio' id='" + subjects[j].Code + "' name='elective" + k + "' value='" + subjects[j].Code + "' /><label for='" + subjects[j].Code + "'>" + subjects[j].Title + "</label>";
                    }
                    else if (sem == 5 || sem == 6) {
                        radioList += "<input type='checkbox' id='" + subjects[j].Code + "' name='elective" + k + "' value='" + subjects[j].Code + "' /><label for='" + subjects[j].Code + "'>" + subjects[j].Title + "</label>";
                    }
                }

                $("#electives").append(radioList + "</div>");
            }
        }
    }

    $("#electives").append("<div class='col form-group' style='float: left; margin-top: 30px;'><input type='button' class='btn btn-success' value='Submit' onclick='SaveElective(this);' /></div>");
}

function SaveElective(element) {
    $(element).attr("disabled", "disabled");
    var department = {
        "HTM": "RM",
        "IDRM": "RM",
        "FND": "FND",
        "MCE": "MCE",
        "TAD": "TAD",
        "DC": "HD",
        "ECCE": "HD"
    };

    var electives = {
        "collegeregistartionnumber": getUrlVars()["crn"],
        "semester": getUrlVars()["semester"],
        "Specialisation": GetSpecialization(),
        "rollNumber": decodeURIComponent(getUrlVars()["rollNumber"]),
        "studentName": decodeURIComponent(getUrlVars()["name"]),
        "department": department[getUrlVars()["specialization"]],
        "year": getUrlVars()["year"],
        "elective1": "",
        "elective2": "",
        "elective3": "",
        "elective4": "",
        "elective5": "",
        "elective6": "",
        "elective7": ""
    };

    var sem = getUrlVars()["semester"];
    var count = 1;

    if (sem == 5 || sem == 6) {
        $("input:checkbox:checked").each(function () {
            electives["elective" + count] = $(this).val();
            count = count + 1;
        });
    }
    else {
        $('input:radio:checked').each(function () {
            electives["elective" + count] = $(this).val();
            count = count + 1;
        });
    }

    CallAPI('Attendance/SaveStudentElectives', 'POST', JSON.stringify(electives)).done(function (data) {
        $(element).removeAttr("disabled");
        alert(data.SuccessMessage);
    });
}
