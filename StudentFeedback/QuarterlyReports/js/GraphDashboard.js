var techers = [];
$(document).ready(function () {
    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var RoleType = localStorage.getItem("roleType");

    $("#userName").html("Welcome " + userName);

    if (userCode == null || userCode == undefined || !userCode) {
        localStorage.clear();
        window.location.href = "index.html";
    }

    $("#logout").click(function () {
        localStorage.clear();
        window.location.href = "index.html";
    });


    function removeDuplicates(originalArray, prop) {
        var newArray = [];
        var lookupObject = {};
        for (var i in originalArray) {
            if (originalArray[i].semester.toLowerCase() == $('#txt_Cource').val().toLowerCase()) {
                lookupObject[originalArray[i][prop]] = originalArray[i];
            }
        }
        for (i in lookupObject) {
            newArray.push(lookupObject[i]);
        }
        return newArray;
    }

    function AdminBind(admins) {
        var ddAdminLabel = $("<label for='ddAdmin' style='width: 100%; text-align: left;'>Choose Academic Administrator:</label>");
        var adminDD = $("<select id='ddAdmin' class='form-control' style='margin-left: 30px; margin-bottom: 30px; width: 30%;' />").attr("onchange", "GetAdmins('AcademicAdministrator')");

        for (var i = 0; i < admins.length; i++) {
            var option = $("<option/>").attr("value", admins[i]).html(admins[i]);
            $(adminDD).append(option);
        }

        $("#GraphAppendGrid_AcademicAdministrator").append(ddAdminLabel);
        $("#GraphAppendGrid_AcademicAdministrator").append(adminDD);

        $("#btnDownloadAllAcadPDF").show();
        $("#btnDownloadAllAcadExcel").show();
        $("#btnDownloadAllAcadPDF2").show();
        $("#btnDownloadAllAcadExcel2").show();
        $("#btnDownloadStudent").hide();
        $("#btnDownloadAcadReport1").hide();
        $("#btnDownloadAcadReport2").hide();
    }

    function BindAcademicYear() {
        var report1 = "<button type='button' style='margin-left: 10px;' id='btnDownloadAcadReport1' onclick='GetAdminReportCard1()' class='btn btn-primary'>Download Report Card 1</button>";
        var report2 = "<button type='button' style='margin-left: 10px;' id='btnDownloadAcadReport2' onclick='GetAdminReportCard2()' class='btn btn-primary'>Download Report Card 2</button>";

        var ddAdminLabel = $("<label for='ddAdmin' style='float:left;'>Choose Academic Year for Report Download:</label>");
        var adminDD = $("<select id='ddAcadYear' class='form-control' style='float: left; margin-left: 10px; width: 12%;' />");

        $("#GraphAppendGrid_AcademicAdministrator").append(ddAdminLabel);
        $("#GraphAppendGrid_AcademicAdministrator").append(adminDD);
        //$("#GraphAppendGrid_AcademicAdministrator").append(report1);
        //$("#GraphAppendGrid_AcademicAdministrator").append(report2);
    }

    function DepartmentBind(depts) {
        var dd1Label = $("<label for='ddOwnDept'>Choose Department for Peer Review:</label>");
        var dd2Label = $("<label for='ddOtherDept'>Choose Department for Peer Review:</label>");
        var dd3Label = $("<label for='ddAnyDept'>Choose Department for Peer Review:</label>");

        var departmentDD1 = $("<select id='ddOwnDept' class='form-control' style='margin-left: 30px; margin-bottom: 30px; width: 30%;' />").attr("onchange", "GetPeerReviewData('PeerOwnDepartment')");
        var departmentDD2 = $("<select id='ddOtherDept' class='form-control'  style='margin-left: 30px; margin-bottom: 30px; width: 30%;' />").attr("onchange", "GetPeerReviewData('PeerOtherDepartment')");
        var departmentDD3 = $("<select id='ddAnyDept' class='form-control' style='margin-left: 30px; margin-bottom: 30px; width: 30%;'  />").attr("onchange", "GetPeerReviewData('PeerAnyDepartment')");

        for (var i = 0; i < depts.length; i++) {
            var option1 = $("<option/>").attr("value", depts[i]).html(depts[i]);
            var option2 = $("<option/>").attr("value", depts[i]).html(depts[i]);
            var option3 = $("<option/>").attr("value", depts[i]).html(depts[i]);
            $(departmentDD1).append(option1);
            $(departmentDD2).append(option2);
            $(departmentDD3).append(option3);
        }

        $("#GraphAppendGrid_PeerOwnDepartment").append(dd1Label);
        $("#GraphAppendGrid_PeerOwnDepartment").append(departmentDD1);
        $("#GraphAppendGrid_PeerOtherDepartment").append(dd2Label);
        $("#GraphAppendGrid_PeerOtherDepartment").append(departmentDD2);
        $("#GraphAppendGrid_PeerAnyDepartment").append(dd3Label);
        $("#GraphAppendGrid_PeerAnyDepartment").append(departmentDD3);
    }

    GetTeacherDetails = function () {
        if ($('#txt_Cource').val() == '') {
            $('#dd_Teacher').find('option').remove().end().append('<option value="">Select Teacher</option>');
            $('#dd_Teacher').prop("disabled", "disabled");
            return false;
        }
        $.ajax({
            url: GloableWebsite + 'api/feedback/GetTeacherDetails?id=' + localStorage.getItem('collegeCode'),
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                var newarray = removeDuplicates(data, 'teacherCode');
                console.log(newarray)
                $('#dd_Teacher').find('option').remove().end().append('<option value="">Select Teacher</option>');
                $('#dd_Teacher').prop("disabled", "");
                for (i = 0; i < newarray.length; i++) {
                    if (newarray[i].semester.toLowerCase() == $('#txt_Cource').val().toLowerCase())
                        $("#dd_Teacher").append("<option value=\"" + newarray[i].teacherCode + "\">" + newarray[i].teacherName + " - " + newarray[i].teacherCode + "</option>");
                }
            }
        });
    }

    GetPeerDetails = function () {
        $.ajax({
            url: GloableWebsite + 'api/CommonFeedback/GetPeerPersonList?collegeCode=' + localStorage.getItem('collegeCode'),
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                $('#dd_Peer').find('option').remove().end().append('<option value="">Select Peer</option>');
                $('#dd_Peer').prop("disabled", "");
                for (i = 0; i < data.length; i++) {
                    $("#dd_Peer").append("<option value=\"" + data[i] + "\">" + data[i] + "</option>");
                }
            }
        });
    }

    GetPeerReportDetails = function (personName) {
        $(".reviewAnalysis").find("table").remove();
        $("#btnDownloadPeerReviewReport").remove();
        $.ajax({
            url: GloableWebsite + 'api/CommonFeedback/GetPeerReviewAnalysis?collegeCode=' + localStorage.getItem('collegeCode') + '&personName=' + personName,
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                console.log(data);
                // Build table to display the points
                var table = "<table id='PeerReviewAnalysis' style='margin-top: 20px; float: left; width: 100%; border: 1px solid #000;'><tr><td colspan='4'><b>Name: " + personName + "</b></td></tr><tr><td style='text-align: center'><b>Question</b></td><td style='text-align: center'><b>Own Department</b></td><td style='text-align: center'><b>Other Department</b></td><td style='text-align: center'><b>Any Department</b></td></tr>";

                for (i = 0; i < peerReviewQuestions.length; i++) {
                    table += "<tr><td>" + peerReviewQuestions[i].question + "</td>";
                    table += "<td style='text-align: center'>" + data.Questions.PeerOwnDepartment.Percentage["a" + (i + 1)].Grade + "%</td>";
                    table += "<td style='text-align: center'>" + data.Questions.PeerOtherDepartment.Percentage["a" + (i + 1)].Grade + "%</td>";
                    table += "<td style='text-align: center'>" + data.Questions.PeerAnyDepartment.Percentage["a" + (i + 1)].Grade + "%</td></tr>";
                }

                table += "<tr><td colspan='4'><b>Note: Percentages are calculated from weighted average of the feedback.</b></td></tr></table>";
                table += "<button type='button' style='float:left' id='btnDownloadPeerReviewReport' onclick='DownloadPeerReviewExcelReport()' class='btn btn-primary'>Download</button>";
                $(".reviewAnalysis").append(table);
            }
        });
    }

    BindTeacherDetails = function () {
        GetTeacherDetails();
    }

    GetAcedemicAdminGraph = function (userType, tablename, admin) {
        $('#' + userType + '_DownloadButton').css('display', 'none');
        $('#GraphAppendGrid_' + userType).find('div').remove();
        var academicYear = $("#ddAcadYear").find(":selected").text();
        $.ajax({
            url: GloableWebsite + 'api/CommonFeedback/GetCommonGraphResult?CollegeCode=' + localStorage.getItem('collegeCode') + '&ReportType=' + userType + '&tableName=' + tablename + '&adminName=' + admin + "&academicYear=" + academicYear,
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                //console.log(data)
                $('#' + userType + '_DownloadButton').css('display', 'block');

                if ($("#ddAdmin").find("option").length <= 0) {
                    AdminBind(admins);
                }
                $('#GraphAppendGrid_' + userType).find('div').remove();
                //$('#GraphAppendGrid_' + userType).html('');
                for (var i = 0; i < data.length; i++) {
                    if (i % 2 == 0) {
                        var rowDiv = document.createElement("div");
                        rowDiv.classList.add("row");
                        document.getElementById('GraphAppendGrid_' + userType).appendChild(rowDiv);
                    }

                    var listOfPieces = [];
                    var pieceFirst = [];
                    pieceFirst.push('Question');
                    pieceFirst.push('Count');
                    listOfPieces.push(pieceFirst);
                    var TotalResponse = 0;
                    for (var j = 0; j < data[i].ListOfDictionary.length; j++) {
                        var piece = [];
                        piece.push(data[i].ListOfDictionary[j].Key);
                        piece.push(data[i].ListOfDictionary[j].Value);
                        TotalResponse += data[i].ListOfDictionary[j].Value;
                        listOfPieces.push(piece);
                    }

                    var divColMd4 = document.createElement("div");
                    divColMd4.classList.add("col-md-6");
                    var divWhiteSpace = document.createElement("div");
                    divWhiteSpace.classList.add("white-space_Chart");
                    //Add Question Div
                    var QuestionDiv = document.createElement("div");
                    QuestionDiv.classList.add("QuestionDivClass");
                    var t = document.createTextNode(data[i].Question + ' (' + TotalResponse + ')');
                    QuestionDiv.appendChild(t);

                    divWhiteSpace.appendChild(QuestionDiv);
                    var CanvasNode = document.createElement("div");
                    CanvasNode.setAttribute("style", "width:100%; height:100%");
                    CanvasNode.id = userType + "_mychart" + i;//Set Id OfChart
                    divWhiteSpace.appendChild(CanvasNode);
                    divColMd4.appendChild(divWhiteSpace);
                    document.getElementById('GraphAppendGrid_' + userType).appendChild(divColMd4);
                    var ctx1 = document.getElementById(userType + "_mychart" + i);

                    CreateGoogleChart(listOfPieces, data[i].Question, ctx1)

                }
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
            },
            type: 'GET'
        });
    }

    GetEmployerGraph = function (userType, dept, subject) {
        $('#' + userType + '_DownloadButton').css('display', 'none');
        
        $('#GraphAppendGrid_' + userType).find('div').remove();
        if (dept == "All Departments" || dept == undefined)
            dept = "";
        else
            dept = encodeURIComponent(dept);
        $.ajax({
            url: GloableWebsite + 'api/CommonFeedback/GetGraphResult?CollegeCode=' + localStorage.getItem('collegeCode') + '&ReportType=' + userType + '&DepartmentName=' + dept + "&subjectName="+subject + "&academicYear=2018-2019",
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                //console.log(data)
                $('#' + userType + '_DownloadButton').css('display', 'block');
                //$('#GraphAppendGrid_' + userType).html('');

                if ($("#ddOwnDept").find("option").length <= 0) {
                    DepartmentBind(departments);
                }

                for (var i = 0; i < data.length; i++) {
                    if (i % 2 == 0) {
                        var rowDiv = document.createElement("div");
                        rowDiv.classList.add("row");
                        document.getElementById('GraphAppendGrid_' + userType).appendChild(rowDiv);
                    }

                    var listOfPieces = [];
                    var pieceFirst = [];
                    pieceFirst.push('Question');
                    pieceFirst.push('Count');
                    listOfPieces.push(pieceFirst);
                    var TotalResponse = 0;
                    for (var j = 0; j < data[i].ListOfDictionary.length; j++) {
                        var piece = [];
                        piece.push(data[i].ListOfDictionary[j].Key);
                        piece.push(data[i].ListOfDictionary[j].Value);
                        TotalResponse += data[i].ListOfDictionary[j].Value;
                        listOfPieces.push(piece);
                    }

                    var divColMd4 = document.createElement("div");
                    divColMd4.classList.add("col-md-6");
                    var divWhiteSpace = document.createElement("div");
                    divWhiteSpace.classList.add("white-space_Chart");
                    //Add Question Div
                    var QuestionDiv = document.createElement("div");
                    QuestionDiv.classList.add("QuestionDivClass");
                    var t = document.createTextNode(data[i].Question + ' (' + TotalResponse + ')');
                    QuestionDiv.appendChild(t);

                    divWhiteSpace.appendChild(QuestionDiv);
                    var CanvasNode = document.createElement("div");
                    CanvasNode.setAttribute("style", "width:100%; height:100%");
                    CanvasNode.id = userType + "_mychart" + i;//Set Id OfChart
                    divWhiteSpace.appendChild(CanvasNode);
                    divColMd4.appendChild(divWhiteSpace);
                    document.getElementById('GraphAppendGrid_' + userType).appendChild(divColMd4);
                    var ctx1 = document.getElementById(userType + "_mychart" + i);

                    CreateGoogleChart(listOfPieces, data[i].Question, ctx1)

                }
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
            },
            type: 'GET'
        });
    }

    GetExitFormGraph = function (userType) {
        $('#' + userType + '_DownloadButton').css('display', 'none');
        $.ajax({
            url: GloableWebsite + 'api/CommonFeedback/GetExitFormGraph?CollegeCode=' + localStorage.getItem('collegeCode') + '&tab=' + userType,
            contentType: "application/json",
            success: function (data, status, jqXHR) {
                console.log(data)
                $('#' + userType + '_DownloadButton').css('display', 'block');
                $('#' + userType + '_divResult').html('');
                //$('#GraphAppendGrid_' + userType).html('');
                for (var i = 0; i < data.length; i++) {
                    if (i % 2 == 0) {
                        var rowDiv = document.createElement("div");
                        rowDiv.classList.add("row");
                        document.getElementById(userType + '_divResult').appendChild(rowDiv);
                    }

                    var listOfPieces = [];
                    var pieceFirst = [];
                    pieceFirst.push('Question');
                    pieceFirst.push('Count');
                    listOfPieces.push(pieceFirst);
                    var TotalResponse = 0;
                    for (var j = 0; j < data[i].ListOfDictionary.length; j++) {
                        var piece = [];
                        piece.push(data[i].ListOfDictionary[j].Key);
                        piece.push(data[i].ListOfDictionary[j].Value);
                        TotalResponse += data[i].ListOfDictionary[j].Value;
                        listOfPieces.push(piece);
                    }

                    var divColMd4 = document.createElement("div");
                    divColMd4.classList.add("col-md-6");
                    var divWhiteSpace = document.createElement("div");
                    divWhiteSpace.classList.add("white-space_Chart");
                    //Add Question Div
                    var QuestionDiv = document.createElement("div");
                    QuestionDiv.classList.add("QuestionDivClass");
                    var t = document.createTextNode(data[i].Question + ' (' + TotalResponse + ')');
                    QuestionDiv.appendChild(t);

                    divWhiteSpace.appendChild(QuestionDiv);
                    var CanvasNode = document.createElement("div");
                    CanvasNode.setAttribute("style", "width:100%; height:100%");
                    CanvasNode.id = userType + "_mychart" + i;//Set Id OfChart
                    divWhiteSpace.appendChild(CanvasNode);
                    divColMd4.appendChild(divWhiteSpace);
                    document.getElementById(userType + '_divResult').appendChild(divColMd4);
                    var ctx1 = document.getElementById(userType + "_mychart" + i);

                    CreateGoogleChart(listOfPieces, data[i].Question, ctx1)

                }
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
            },
            type: 'GET'
        });
    }

    GetTeacherGraphByTeacherAndCourse = function () {
        var userType = 'Customer'
        $('#GraphAppendGrid_' + userType).html('');
        $('#StudentDownloadButton').css('display', 'none');
        if ($('#txt_Cource').val() != '' && $('#dd_Teacher').val() != '') {
            var _teacherCode = $('#dd_Teacher').val();
            var _CurrentSemester = $('#txt_Cource').val();
            var _CollegeCode = localStorage.getItem('collegeCode');
            var year = $("#ddAcademicYear").find(":selected").val();

            if (year == undefined || year == null) {
                year = "";
            }
            //var _teacherCode = 'BM3';
            //var _SubjectCode = 240419;
            //var _CollegeCode = 101;
            $.ajax({
                url: GloableWebsite + '/api/CommonFeedback/GetTeacherGraphResult?teacherCode=' + _teacherCode + '&CurrentSemester=' + _CurrentSemester + '&CollegeCode=' + _CollegeCode + '&year=' + year,
                contentType: "application/json",
                success: function (data, status, jqXHR) {
                    $('#StudentDownloadButton').css('display', 'block');
                    console.log(data);
                    $('#GraphAppendGrid_' + userType).html('');
                    for (var i = 0; i < data.length; i++) {
                        if (i % 2 == 0) {
                            var rowDiv = document.createElement("div");
                            rowDiv.classList.add("row");
                            document.getElementById('GraphAppendGrid_' + userType).appendChild(rowDiv);
                        }

                        var listOfPieces = [];
                        var pieceFirst = [];
                        pieceFirst.push('Question');
                        pieceFirst.push('Count');
                        listOfPieces.push(pieceFirst);
                        var TotalNoOfResponse = 0;
                        for (var j = 0; j < data[i].ListOfDictionary.length; j++) {
                            var piece = [];
                            piece.push(data[i].ListOfDictionary[j].Key);
                            piece.push(data[i].ListOfDictionary[j].Value);
                            TotalNoOfResponse += data[i].ListOfDictionary[j].Value;
                            listOfPieces.push(piece);
                        }


                        var divColMd4 = document.createElement("div");
                        divColMd4.classList.add("col-md-6");
                        var divWhiteSpace = document.createElement("div");
                        divWhiteSpace.classList.add("white-space_Chart");
                        //Add Question Div
                        var QuestionDiv = document.createElement("div");
                        QuestionDiv.classList.add("QuestionDivClass");
                        var t = document.createTextNode(data[i].Question + ' (' + TotalNoOfResponse + ')');
                        QuestionDiv.appendChild(t);

                        divWhiteSpace.appendChild(QuestionDiv);
                        var CanvasNode = document.createElement("div");
                        CanvasNode.setAttribute("style", "width:100%; height:100%");
                        CanvasNode.id = userType + "_mychart" + i;//Set Id OfChart
                        divWhiteSpace.appendChild(CanvasNode);
                        divColMd4.appendChild(divWhiteSpace);
                        document.getElementById('GraphAppendGrid_' + userType).appendChild(divColMd4);
                        var ctx1 = document.getElementById(userType + "_mychart" + i);


                        CreateGoogleChart(listOfPieces, data[i].Question, ctx1)
                    }
                }
            });
        }

    }
    //GetTeacherGraphByTeacherAndCourse();

    CreateGoogleChart = function (arraylistOfPieces, strQuestion, divChartContext) {
        var ChartData = google.visualization.arrayToDataTable(arraylistOfPieces);
        var options = {
            backgroundColor: 'transparent',
            colors: ["cornflowerblue",
                      "olivedrab",
                      "orange",
                      "tomato",
                      "crimson",
                      "purple",
                      "turquoise",
                      "forestgreen",
                      "navy",
                      "gray"],
            //pieSliceText: 'value',
            tooltip: {
                text: 'percentage'
            },
            fontName: 'Open Sans',
            chartArea: {
                width: '100%',
                height: '94%'
            },
            legend: {
                textStyle: {
                    fontSize: 13
                }
            },
            title: strQuestion
        };

        var chart = new google.visualization.PieChart(divChartContext);
        chart.draw(ChartData, options);
    }

    google.charts.load('current', { 'packages': ['corechart'] });
    //google.charts.setOnLoadCallback(GetTeacherGraphByTeacherAndCourse);

    GetAlumniTabInformation = function (tabInfo) {
        //$('#' + tabInfo + '_DownloadButton').css('display', 'none');
        var userType = tabInfo;
        $.ajax({
            //url: GloableWebsite + '/api/CommonFeedback/GetAlumaniTabGraphData?CollegeCode=' + localStorage.getItem('collegeCode') + '&tab=' + tabInfo,
            url: GloableWebsite + 'api/CommonFeedback/GetGraphResult?CollegeCode=' + localStorage.getItem('collegeCode') + '&ReportType=' + userType + '&DepartmentName=',
            contentType: "application/json",
            type: 'GET',
            success: function (data, status, jqXHR) {
                $('#' + tabInfo + '_DownloadButton').css('display', 'block');
                console.log(data);
                $('#GraphAppendGrid_Alumni').html('');
                for (var i = 0; i < data.length; i++) {
                    if (i % 2 == 0) {
                        var rowDiv = document.createElement("div");
                        rowDiv.classList.add("row");
                        document.getElementById('GraphAppendGrid_Alumni').appendChild(rowDiv);
                    }

                    var listOfPieces = [];
                    var pieceFirst = [];
                    pieceFirst.push('Question');
                    pieceFirst.push('Count');
                    listOfPieces.push(pieceFirst);
                    var TotalRepsonse = 0;
                    for (var j = 0; j < data[i].ListOfDictionary.length; j++) {
                        var piece = [];
                        piece.push(data[i].ListOfDictionary[j].Key);
                        piece.push(data[i].ListOfDictionary[j].Value);
                        TotalRepsonse += data[i].ListOfDictionary[j].Value;
                        listOfPieces.push(piece);
                    }


                    var divColMd4 = document.createElement("div");
                    divColMd4.classList.add("col-md-6");
                    var divWhiteSpace = document.createElement("div");
                    divWhiteSpace.classList.add("white-space_Chart");
                    //Add Question Div
                    var QuestionDiv = document.createElement("div");
                    QuestionDiv.classList.add("QuestionDivClass");
                    var t = document.createTextNode(data[i].Question + ' (' + TotalRepsonse + ')');
                    QuestionDiv.appendChild(t);

                    divWhiteSpace.appendChild(QuestionDiv);
                    var CanvasNode = document.createElement("div");
                    CanvasNode.setAttribute("style", "width:100%; height:100%");
                    CanvasNode.id = userType + "_mychart" + i;//Set Id OfChart
                    divWhiteSpace.appendChild(CanvasNode);
                    divColMd4.appendChild(divWhiteSpace);
                    document.getElementById('GraphAppendGrid_Alumni').appendChild(divColMd4);
                    var ctx1 = document.getElementById(userType + "_mychart" + i);

                    CreateGoogleChart(listOfPieces, data[i].Question, ctx1)

                }
            }
        });
    }

    //Downloading Excel Ajax
    DownloadExcelStudentTab = function () {
        var _teacherCode = $('#dd_Teacher').val();
        var _CurrentSemester = $('#txt_Cource').val();
        var _CollegeCode = localStorage.getItem('collegeCode');
        //window.open(GloableWebsite + '/api/CommonFeedback/ExportTeacherGraphResult?teacherCode=' + _teacherCode + '&CurrentSemester=' + _CurrentSemester + '&CollegeCode=' + _CollegeCode, '_blank');
        window.open(GloableWebsite + '/api/CommonFeedback/GetTeacherGraphResult_Excel?teacherCode=' + _teacherCode + '&CurrentSemester=' + _CurrentSemester + '&CollegeCode=' + _CollegeCode, '_blank');
    }

    DownloadExcelCommonFeedbackTab = function (repoType) {
        var dept = null;
        switch (repoType) {
            case "PeerOwnDepartment":
                dept = $("#ddOwnDept").find(":selected").text();
                break;
            case "PeerOtherDepartment":
                dept = $("#ddOtherDept").find(":selected").text();
                break;
            case "PeerAnyDepartment":
                dept = $("#ddAnyDept").find(":selected").text();
                break;
        }
        if (dept == "All Departments" || dept == undefined)
            dept = "";
        window.open(GloableWebsite + '/api/commonfeedback/ExportBarGraphUsingSpreadSheet?CollegeCode=' + localStorage.getItem('collegeCode') + '&ReportType=' + repoType + '&DepartmentName=' + dept);
        //window.open(GloableWebsite + '/api/commonfeedback/ExportBarGraphByReportType?CollegeCode=' + localStorage.getItem('collegeCode') + '&ReportType=' + repoType);
    }

    ExportGetExitFormGraph = function (repoType) {
        //window.open(GloableWebsite + '/api/commonfeedback/ExportAlumaniTabGraphData?CollegeCode=' + localStorage.getItem('collegeCode') + '&tab=' + repoType);
        window.open(GloableWebsite + '/api/commonfeedback/ExportGetExitFormGraph?CollegeCode=' + localStorage.getItem('collegeCode') + '&tab=' + repoType);
    }

    ExportCommonBarGraphUsingSpreadSheet = function (repoType, tableName) {
        var ad = $("#ddAdmin").find(":selected").text();
        if (ad == "All Administrators" || ad == undefined)
            ad = "";
        var academicYear = $("#ddAcadYear").find(":selected").text();
        window.open(GloableWebsite + '/api/commonfeedback/ExportCommonBarGraphUsingSpreadSheet?CollegeCode=' + localStorage.getItem('collegeCode') + '&ReportType=' + repoType + '&tableName=' + tableName + '&adminName=' + ad + '&academicYear=' + academicYear);
    }

    DownloadExcelAlumini = function (repoType) {
        //window.open(GloableWebsite + '/api/commonfeedback/ExportAlumaniTabGraphData?CollegeCode=' + localStorage.getItem('collegeCode') + '&tab=' + repoType);
        window.open(GloableWebsite + '/api/commonfeedback/GetAlumaniTabGraphData_Excel?CollegeCode=' + localStorage.getItem('collegeCode') + '&tab=' + repoType);
    }
    $("ul.nav-tabs a").click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });

    GetTeacherReportCard1 = function () {
        var isValidForm = true;
        var _teacherCode = $('#dd_Teacher').val();
        var _CurrentSemester = $('#txt_Cource').val();

        if (_teacherCode == undefined || _teacherCode == "" || _teacherCode == null) {
            isValidForm = false;
            $("#dd_Teacher").addClass("InvalidError");
        }
        else {
            $("#dd_Teacher").removeClass("InvalidError");
        }
        if (_CurrentSemester == undefined || _CurrentSemester == "" || _CurrentSemester == null) {
            isValidForm = false;
            $("#txt_Cource").addClass("InvalidError");
        }
        else {
            $("#txt_Cource").removeClass("InvalidError");
        }
        if (!isValidForm) {
            return;
        }
        var _CollegeCode = localStorage.getItem('collegeCode');
        var year = $("#ddAcademicYear").find(":selected").val();
        if (year == undefined || year == null) {
            year = "";
        }
        window.open(GloableWebsite + '/api/Feedback/ExportTeacherReportCard?teacherCode=' + _teacherCode + '&collageCode=' + _CollegeCode + '&CurrentSemester=' + _CurrentSemester + '&year=' + year, '_blank');
    }

    GetTeacherReportCard2 = function () {
        var isValidForm = true;
        var _teacherCode = $('#dd_Teacher').val();
        var _CurrentSemester = $('#txt_Cource').val();

        if (_teacherCode == undefined || _teacherCode == "" || _teacherCode == null) {
            isValidForm = false;
            $("#dd_Teacher").addClass("InvalidError");
        }
        else {
            $("#dd_Teacher").removeClass("InvalidError");
        }
        if (_CurrentSemester == undefined || _CurrentSemester == "" || _CurrentSemester == null) {
            isValidForm = false;
            $("#txt_Cource").addClass("InvalidError");
        }
        else {
            $("#txt_Cource").removeClass("InvalidError");
        }
        if (!isValidForm) {
            return;
        }
        var _CollegeCode = localStorage.getItem('collegeCode');
        var year = $("#ddAcademicYear").find(":selected").val();
        if (year == undefined || year == null) {
            year = "";
        }

        window.open(GloableWebsite + '/api/Feedback/ExportTeacherReportCard2?teacherCode=' + _teacherCode + '&collageCode=' + _CollegeCode + '&CurrentSemester=' + _CurrentSemester + '&year=' + year, '_blank');
    }

    GetAllReports2InExcel = function () {
        var teacherCodes = "";
        var currentSemester = $('#txt_Cource').val();
        var _CollegeCode = localStorage.getItem('collegeCode');

        if ($("#dd_Teacher > option").length < 2) {
            alert("Please wait until Teacher names are populated in dropdown");
            return;
        }

        $("#dd_Teacher > option").each(function () {
            var val = $(this).val();
            if (val != "") {
                teacherCodes += val + ",";
            }
        });

        window.open(GloableWebsite + '/api/Feedback/GetAllReports2InExcel?teacherCode=' + teacherCodes + '&collageCode=' + _CollegeCode + '&CurrentSemester=' + currentSemester + '&year=' + $("#ddAcademicYear").find(":selected").val(), '_blank');
    }

    GetAllReports1InExcel = function () {
        var teacherCodes = "";
        var currentSemester = $('#txt_Cource').val();
        var _CollegeCode = localStorage.getItem('collegeCode');

        if ($("#dd_Teacher > option").length < 2) {
            alert("Please wait until Teacher names are populated in dropdown");
            return;
        }

        $("#dd_Teacher > option").each(function () {
            var val = $(this).val();
            if (val != "") {
                teacherCodes += val + ",";
            }
        });

        window.open(GloableWebsite + '/api/Feedback/GetAllReports1InExcel?teacherCode=' + teacherCodes + '&collageCode=' + _CollegeCode + '&CurrentSemester=' + currentSemester + '&year=' + $("#ddAcademicYear").find(":selected").val(), '_blank');
    }

    GetAllReports2InPDF = function () {
        var teacherCodes = "";
        var currentSemester = $('#txt_Cource').val();
        var _CollegeCode = localStorage.getItem('collegeCode');
        $("#dd_Teacher > option").each(function () {
            var val = $(this).val();
            if (val != "") {
                teacherCodes += val + ",";
            }
        });

        window.open(GloableWebsite + '/api/Feedback/GetAllReports2InPDF?teacherCode=' + teacherCodes + '&collageCode=' + _CollegeCode + '&CurrentSemester=' + currentSemester + '&year=' + $("#ddAcademicYear").find(":selected").val(), '_blank');
    }

    GetAllReports1InPDF = function () {
        var teacherCodes = "";
        var currentSemester = $('#txt_Cource').val();
        var _CollegeCode = localStorage.getItem('collegeCode');
        $("#dd_Teacher > option").each(function () {
            var val = $(this).val();
            if (val != "") {
                teacherCodes += val + ",";
            }
        });

        window.open(GloableWebsite + '/api/Feedback/GetAllReports1InPDF?teacherCode=' + teacherCodes + '&collageCode=' + _CollegeCode + '&CurrentSemester=' + currentSemester + '&year=' + $("#ddAcademicYear").find(":selected").val(), '_blank');
    }

    GetAdminReportCard1 = function () {
        var isValidForm = true;

        var adminName = $('#ddAdmin').val();
        var acadYear = $('#ddAcadYear').val();

        if (adminName == undefined || adminName == "" || adminName == null) {
            isValidForm = false;
            $("#ddAdmin").addClass("InvalidError");
        }
        else {
            $("#ddAdmin").removeClass("InvalidError");
        }
        if (acadYear == undefined || acadYear == "" || acadYear == null) {
            isValidForm = false;
            $("#ddAcadYear").addClass("InvalidError");
        }
        else {
            $("#ddAcadYear").removeClass("InvalidError");
        }
        if (!isValidForm) {
            return;
        }
        var collegeCode = localStorage.getItem('collegeCode');

        window.open(GloableWebsite + '/api/CommonFeedback/ExportAdminReportCard?adminName=' + adminName + '&collageCode=' + collegeCode + '&academicYear=' + acadYear, '_blank');
    }

    GetAdminReportCard2 = function () {
        var isValidForm = true;

        var adminName = $('#ddAdmin').val();
        var acadYear = $('#ddAcadYear').val();

        if (adminName == undefined || adminName == "" || adminName == null) {
            isValidForm = false;
            $("#ddAdmin").addClass("InvalidError");
        }
        else {
            $("#ddAdmin").removeClass("InvalidError");
        }
        if (acadYear == undefined || acadYear == "" || acadYear == null) {
            isValidForm = false;
            $("#ddAcadYear").addClass("InvalidError");
        }
        else {
            $("#ddAcadYear").removeClass("InvalidError");
        }
        if (!isValidForm) {
            return;
        }
        var collegeCode = localStorage.getItem('collegeCode');

        window.open(GloableWebsite + '/api/CommonFeedback/ExportAdminReportCard2?adminName=' + adminName + '&collageCode=' + collegeCode + '&academicYear=' + acadYear, '_blank');
    }


    $.ajax({
        url: GloableWebsite + 'api/CommonFeedback/GetAcademicYearsForAdmins?collegeCode=' + localStorage.getItem('collegeCode'),
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            var years = data;
            BindAcademicYear();
            for (i = 0; i < years.length; i++) {
                var option = $("<option/>").attr("value", years[i]).html(years[i]);
                var option1 = $("<option/>").attr("value", years[i]).html(years[i]);
                $("#ddAcadYear").append(option);
                $("#ddAcademicYearCurriculam").append(option1);
            }
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });

    $.ajax({
        url: GloableWebsite + 'api/CommonFeedback/GetDepartmentsForPeerReview?collegeCode=' + localStorage.getItem('collegeCode'),
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            departments = data;
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });

    $.ajax({
        url: GloableWebsite + 'api/CommonFeedback/GetAcademicAdministrators?collegeCode=' + localStorage.getItem('collegeCode'),
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            admins = data;
            //$("#AcadList").click();

        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
        },
        type: 'GET'
    });

    GetPeerDetails();

    $.ajax({
        url: GloableWebsite + 'api/CommonFeedback/GetPeerReviewQuestions?collegeCode=' + localStorage.getItem('collegeCode'),
        contentType: "application/json",
        success: function (data, status, jqXHR) {
            peerReviewQuestions = data;
        },
        error: function (jqXHR, status) {
            console.log(jqXHR);
        },
        type: 'GET'
    });

    
});



