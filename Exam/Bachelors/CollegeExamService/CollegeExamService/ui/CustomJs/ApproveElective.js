var ExamSystem = angular.module("ApproveElectiveCtrlApp", []);
ExamSystem.controller("ApproveElectiveCtrl", function ($scope) {
    var months = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    var program = getUrlVars()["program"];
    var specialization = getUrlVars()["specialization"];
    var sem = getUrlVars()["semester"];
    var year = getUrlVars()["year"];
    var spl = $("#ddSpecial option:selected").val();
    $("#Specialisation").html("Department - " + specialization);
    $("#Semester").html("Semester - " + sem);
    PrepareSpecialisationDropdown(specialization);

    CallAPI('User/GetGEList?Course=bsc&specialization=' + program + '&sem=' + sem + '', 'GET', '').done(function (data) {
        var totalPaper = PrepareFYElectives(data);
        GetStudentElectives(program, spl, sem, year, totalPaper);
    });
});

function ReloadStudentData() {
    var program = getUrlVars()["program"];
    var sem = getUrlVars()["semester"];
    var year = getUrlVars()["year"];
    //var spl = $("#ddSpecial option:selected").val();

    $("#electives").children().remove();

    CallAPI('User/GetGEList?Course=bsc&specialization=' + program + '&sem=' + sem + '', 'GET', '').done(function (data) {
        var totalPaper = PrepareFYElectives(data);
        var spl = $("#ddSpecial option:selected").val();
        GetStudentElectives(program, spl, sem, year);
    });
    
}

function GetStudentElectives(program,specialization, sem, year, totalPaper) {
    
    CallAPI('Attendance/GetStudentElectives?program=' + program + '&specialisation=' + specialization + '&semester=' + sem + '&year=' + year, 'GET', '').done(function (data) {
        var counter = 1;
        var papers = [];
        $("#electives tbody td").each(function () {
            var code = $(this).attr("code");
            if (code != undefined) {
                papers.push(code);
            }
        });

        //console.log(data);

        var t = "";
        for (i = 0; i < data.length; i++) {
            var row = "<tr crn='" + data[i].crn + "'><td>" + counter + "</td><td>" + data[i].rollnumber + "</td><td class='large'>" + data[i].studentname + "</td>";
            t = "";
            for (j = 0; j < papers.length; j++) {
                var electivePresent = false;
                var ae1 = data[i]["approvedelective1"] != "" && data[i]["approvedelective1"] != null;
                var ae2 = data[i]["approvedelective2"] != "" && data[i]["approvedelective2"] != null;
                var ae3 = data[i]["approvedelective3"] != "" && data[i]["approvedelective3"] != null;
                var ae4 = data[i]["approvedelective4"] != "" && data[i]["approvedelective4"] != null;
                var ae5 = data[i]["approvedelective5"] != "" && data[i]["approvedelective5"] != null;
                var ae6 = data[i]["approvedelective6"] != "" && data[i]["approvedelective6"] != null;
                var ae7 = data[i]["approvedelective7"] != "" && data[i]["approvedelective7"] != null;

                if (ae1 || ae2 || ae3 || ae4 || ae5 || ae6 || ae7) {
                    electivePresent = data[i]["approvedelective1"] == papers[j] || data[i]["approvedelective2"] == papers[j] || data[i]["approvedelective3"] == papers[j] || data[i]["approvedelective4"] == papers[j] || data[i]["approvedelective5"] == papers[j] || data[i]["approvedelective6"] == papers[j] || data[i]["approvedelective7"] == papers[j];
                }
                else {
                    electivePresent = data[i]["elective1"] == papers[j] || data[i]["elective2"] == papers[j] || data[i]["elective3"] == papers[j] || data[i]["elective4"] == papers[j] || data[i]["elective5"] == papers[j] || data[i]["elective6"] == papers[j] || data[i]["elective7"] == papers[j];
                }

                var checked = electivePresent ? "checked='checked'" : "";
                row += "<td><input type='checkbox' code='" + papers[j] + "' id='" + data[i].rollnumber + "_" + papers[j] + "' value='" + papers[j] + "' " + checked + " /></td>";
                t += "<td code='" + papers[j] + "'>&nbsp;</td>";
            }
            row += "</tr>";
            $("#electives tbody").append(row);
            counter++;
        }

        $("#electives tbody").append("<tr class='footer'><td>&nbsp;</td><td>&nbsp;</td><td><b>Total</b></td>" + t + "</tr>");

        $("#electives input").change(function () {
            CalculateTotal();
        });

        CalculateTotal();
    });
}

function CalculateTotal() {
    $("#electives .header td").each(function () {
        var code = $(this).attr("code");
        if (code != undefined) {
            var c = $("input[code='" + code + "' ]").filter(':checked').length;
            $("#electives .footer td[code='" + code + "']").html("<b>" + c + "</b>");
        }
    });
}

function PrepareSpecialisationDropdown(dept) {
    var dd = "<select id='ddSpecial' onchange='ReloadStudentData()'><option value='-1'>---Select---</option>";
    switch (dept) {
        case "HD":
            dd += "<option value='DC'>Developmental Counseling</option>";
            dd += "<option value='ECCE'>Early Childhood Care & Education</option>";
            break;
        case "FND":
            dd += "<option value='FND'>Food, Nutrition & Dietetics</option>";
            break;
        case "RM":
            dd += "<option value='HTM'>Hospitality & Tourism Management</option>";
            dd += "<option value='IDRM'>Interior Designing & Resource Management</option>";
            break;
        case "MCE":
            dd += "<option value='MCE'>Mass Communication & Extensions</option>";
            break;
        case "TAD":
            dd += "<option value='TAD'>Textile & Apparel Designing</option>";
            break;
    }

    dd += "</select>";

    $("#Spl").append(dd);
}

function PrepareFYElectives(data) {
    var specialization = $("#ddSpecial option:selected").val();//getUrlVars()["specialization"];
    var totalPaper = 0;

    var table = "<table border='1' id='electives' class='electives'><tr class='header'><td><b>Serial #</b></td><td><b>Roll #</b></td><td class='large'><b>Student Name</b></td>";
    for (i = 0; i < data.length; i++) {
        if (data[i].specialisationCode.toLowerCase() == specialization.toLowerCase()) {
            var electives = data[i].ElectiveSubject;
            for (k = 0; k < electives.length; k++) {
                var subjects = electives[k].Subject;
                for (j = 0; j < subjects.length; j++) {
                    table += "<td code='" + subjects[j].Code + "'><b>" + subjects[j].Title + "</b></td>";
                }

                totalPaper += subjects.length;
            }
        }
    }

    $("#electives").append(table + "</tr></table>");

    $("#electives").append("<div class='col form-group' style='float: left; margin-top: 30px;'><input type='button' class='btn btn-success' value='Approve' onclick='ApproveElectives(this);' /></div>");
    return totalPaper;
}

function ApproveElectives(element) {
    var students = [];
    $(element).attr("disabled", "disabled");

    $("#electives").find("tr").each(function () {
        var code1 = $(this).find("td:nth-child(4) input").attr('code');
        var code2 = $(this).find("td:nth-child(5) input").attr('code');
        var code3 = $(this).find("td:nth-child(6) input").attr('code');
        var code4 = $(this).find("td:nth-child(7) input").attr('code');
        var code5 = $(this).find("td:nth-child(8) input").attr('code');
        var code6 = $(this).find("td:nth-child(9) input").attr('code');
        var code7 = $(this).find("td:nth-child(10) input").attr('code');

        var electives = {
            "collegeregistartionnumber": $(this).attr("crn"),
            "semester": getUrlVars()["semester"],
            "Specialisation": decodeURIComponent(getUrlVars()["specialization"]),
            "year": getUrlVars()["year"],
            "ApprovedBy": getUrlVars()["approvedby"],
            "ApprovedElective1": code1 != undefined ? $(this).find("td:nth-child(4) input").prop("checked") == true ? code1 : "" : "",
            "ApprovedElective2": code2 != undefined ? $(this).find("td:nth-child(5) input").prop("checked") == true ? code2 : "" : "",
            "ApprovedElective3": code3 != undefined ? $(this).find("td:nth-child(6) input").prop("checked") == true ? code3 : "" : "",
            "ApprovedElective4": code4 != undefined ? $(this).find("td:nth-child(7) input").prop("checked") == true ? code4 : "" : "",
            "ApprovedElective5": code5 != undefined ? $(this).find("td:nth-child(8) input").prop("checked") == true ? code5 : "" : "",
            "ApprovedElective6": code6 != undefined ? $(this).find("td:nth-child(9) input").prop("checked") == true ? code6 : "" : "",
            "ApprovedElective7": code7 != undefined ? $(this).find("td:nth-child(10) input").prop("checked") == true ? code7 : "" : ""
        };

        if ($(this).attr("crn") != undefined)
            students.push(electives);
    });

    CallAPI('Attendance/SaveApprovedElectives', 'POST', JSON.stringify(students)).done(function (data) {
        $(element).removeAttr("disabled");
        alert(data.SuccessMessage);
    });
}

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        var specialization = $("#ddSpecial option:selected").val();
        var program = getUrlVars()["program"];
        var sem = getUrlVars()["semester"];
        var year = getUrlVars()["year"];

        table = "electives";
        name = "Sem-" + sem + "-" + program + "-Electives-" + specialization + "-" + year;
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})()