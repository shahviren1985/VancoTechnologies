
var app = angular.module('LadgerReport', []);

app.controller('LadgerController', function ($scope) {
    var DetailsScrapper = [];
    $(".newDatePicker").datepicker({
        autoclose: true,
        todayHighlight: true
    }).datepicker('update', new Date());

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }
    $scope.DateSelectedval = getUrlVars()["date"];
    var YearSelected = parseFloat($scope.DateSelectedval.substr($scope.DateSelectedval.length - 4));


    //var MonthSelected = $scope.DateSelectedval.slice(3, 5);
    var reader = new FileReader();
    $scope.StudentData = [];
    $scope.dataItem = [];
    var ReportType = $('input[name=Report]:checked').val();
    console.log(ReportType)
    var strHtml = '';
    var ExamDetailJson;
    var GeneralElectiveInfo = [];
    var SubjectCount = parseFloat($('#SubjectCount').val());
    $('#htmlTextToppers').html('');
    $('#htmlText').html('');
    var SemesterSubSpecialization;
    $scope.CreateLadgerReport = function () {
        //$('#LadgerHeader').css('display', 'none');
        ReportType = 1;
        $scope.DateSelectedval = '01/01/2018';
        YearSelected = $scope.DateSelectedval.substr($scope.DateSelectedval.length - 4);
        MonthSelected = $scope.DateSelectedval.slice(3, 5);
        reader = new FileReader();
        //$scope.StudentData = [];
        $scope.dataItem = [];
        strHtml = '';
        if (YearSelected == 2017) {
            var YearSelected = '17-18 October';
        } else {
            var YearSelected = '18-19 October';
        }
        if (SubjectCount > 0) {
            $('.spnSemester').html(': ' + GetSmesterInRoman($scope.StudentData[0].Semester));
            if ($scope.StudentData[0].Class == undefined) { $('.spnCourse').html(': ' + $scope.StudentData[0].Course); }
            else { $('.spnCourse').html(': ' + $scope.StudentData[0].Class); }

            $('.spnYear').html(': ' + YearSelected);
            if ($scope.StudentData[0].Semester == 1) {
                SemesterSub = Sem1JsonValue;
            } else if ($scope.StudentData[0].Semester == 2) {
                SemesterSub = Sem2JsonValue;
            } else if ($scope.StudentData[0].Semester == 3) {
                SemesterSub = Sem3JsonValue;
            } else if ($scope.StudentData[0].Semester == 4) {
                SemesterSub = Sem4JsonValue;
            }
            var SemesterSub = $.parseJSON(localStorage.getItem("CurrentSubjectRecord"));
            for (k = 0; k < SemesterSub.length; k++) {
                if ((SemesterSub[k].specialisation.toUpperCase() == $scope.StudentData[0].Specialisation.toUpperCase()) || (SemesterSub[k].specialisationCode.toUpperCase() == $scope.StudentData[0].Specialisation.toUpperCase())) {
                    SemesterSubSpecialization = SemesterSub[k];
                    ExamDetailJson = SemesterSub[k].paperDetails; 
                    break;
                }
            }

            if (ReportType == 1) {
                var StudentGeneralElective = [];
                for (g = 0; g < GeneralElectiveInfo.length; g++) {
                    if (SemesterSubSpecialization.specialisation == GeneralElectiveInfo[g].Specialization || SemesterSubSpecialization.specialisationCode == GeneralElectiveInfo[g].specialisationCode) {
                        StudentGeneralElective = GeneralElectiveInfo[g];
                    }
                }
                if (StudentGeneralElective.ElectiveSubject != undefined && StudentGeneralElective.ElectiveSubject.length > 0) {

                    //code for distinct group
                    var result = [];
                    var GroupElective = [];
                    var ElectiveSubjects = [];
                    var first = true;
                    console.log(StudentGeneralElective)
                    $.each(StudentGeneralElective.ElectiveSubject, function (i, val) {
                        for (i = 0; i < val.Subject.length; i++) {
                            ElectiveSubjects.push(val.Subject[i].Code)
                        }
                        if (first == true) {
                            $.each(val.Subject, function (i, valItems) {
                                result.push(valItems.Code);
                            });
                            first = false;
                        }
                        else {
                            var newresult = [];
                            $.each(val.Subject, function (i, valItems) {
                                $.each(result, function (i, valMain) {
                                    newresult.push(valItems.Code + ',' + valMain);
                                });
                            });
                            result = newresult;
                        }
                    });

                    var StudentGroupByElectiveSubject = [];
                    for (s = 0; s < $scope.StudentData.length; s++) {
                        var StudentSubject = [];
                        for (c = 0; c < SubjectCount; c++) {
                            var intcount = c + 1;
                            var code = 'Code' + intcount;
                            StudentSubject.push($scope.StudentData[s][code]);
                        }
                        for (e = 0; e < result.length; e++) {
                            resultInnerArray = [];
                            resultInnerArray = result[e].split(',');
                            const finalarray = [];
                            StudentSubject.forEach((e1) =>resultInnerArray.forEach((e2) => {if($.trim(e1).toUpperCase() == $.trim(e2).toUpperCase()) { finalarray.push(e1.toUpperCase()) }}))
                            if (resultInnerArray.length == finalarray.length) {
                                $scope.StudentData[s].ElectiveGroupNumber = 1;
                            }
                }
            }

            var StudentMain = [];
            StudentMain = $scope.StudentData;
            var htmlstringFinal = '';
            var IsFirsthtml = true;
            for (ai = 0; ai < result.length; ai++) {
                $scope.StudentData = [];
                for (us = 0; us < StudentMain.length; us++) {
                    if (StudentMain[us].ElectiveGroupNumber == ai) {
                        $scope.StudentData.push(StudentMain[us]);
                        //StudentMain.splice(us, 1);
                    }
                }

                if (IsFirsthtml) {
                    if ($scope.StudentData.length > 0) {
                        var headerstr = $('#HeaderLadgerScraper').html();
                        headerstr=headerstr.replace('_Specialization', SemesterSubSpecialization.specialisation)
                        htmlstringFinal += headerstr;
                        htmlstringFinal += GenerateReportBasedOnChoice();
                        IsFirsthtml = false;
                    }
                }
                else {
                    if ($scope.StudentData.length > 0) {
                        var headerstr = $('#HeaderLadgerScraper').html();
                        headerstr=headerstr.replace('_Specialization', SemesterSubSpecialization.specialisation)
                        headerstr += GenerateReportBasedOnChoice();
                        htmlstringFinal += headerstr;
                    }
                }
                console.log(result[ai]);
                console.log($scope.StudentData);
            }
            $('#htmlText').append(htmlstringFinal);
        } else {
            var headerstr = $('#HeaderLadgerScraper').html();
            headerstr=headerstr.replace('_Specialization', SemesterSubSpecialization.specialisation)
            headerstr += GenerateReportBasedOnChoice();
            if(htmlstringFinal==undefined){
                htmlstringFinal='';
            }
            htmlstringFinal += headerstr;
            $('#htmlText').append(htmlstringFinal);
        }
    } else {
        var headerstr = $('#HeaderLadgerScraper').html();
        headerstr=headerstr.replace('_Specialization', SemesterSubSpecialization.specialisation)
        headerstr += GenerateReportBasedOnChoice();
        if(htmlstringFinal==undefined){
            htmlstringFinal='';
        }
        htmlstringFinal += headerstr;
        $('#htmlText').append(htmlstringFinal);
    }

    //console.log($scope.StudentData);
    //$scope.$apply();
    //};
    //reader.readAsBinaryString(csvFile.files[0]);
}
};

function GenerateReportBasedOnChoice() {
    if (ReportType == 1) {
        DetailsScrapper = [];
        strHtml = '<table class="BordredTable"  style="text-align:center;font-family:Arial;color:black;border:2px solid black">';
        $('#LadgerHeader').css('display', '');
        strHtml += '<tr><td rowspan="6" class="rotate">Sr. No.</td><td rowspan="6" class="rotate">Regd. No.</td><td rowspan="6">PRN No.</td><td rowspan="6">NAME OF THE STUDENT</td>'
        

        loop1: for (j = 0; j < SemesterSubSpecialization.paperDetails.length; j++) {
            strHtml += '<td colspan="3" rowspan="2" style="padding:5px"><div style="width:110px">' + SemesterSubSpecialization.paperDetails[j].paperTitle +'</div></td>';
        }


        strHtml += '<td rowspan="6" class="rotate">REMARK</td><td rowspan="6" class="rotate">Weighted %</td><td rowspan="6" class="rotate">Grade</td><td rowspan="6" class="rotate">Grade Point</td><td rowspan="6" class="rotate">Credit Balance</td>'
        strHtml += '</tr><tr>';
        //for (m = 1; m <= SubjectCount; m++) {
        //    var strCode = "Code" + m;
        //    for (j = 0; j < SemesterSubSpecialization.paperDetails.length; j++) {
        //        if (SemesterSubSpecialization.paperDetails[j].code.toUpperCase() == $scope.StudentData[0][strCode].toUpperCase()) {
        //            if (SemesterSubSpecialization.paperDetails[j].isElective.toUpperCase() == "YES") {
        //                strHtml += '<td colspan="3" style="width:100px;padding:5px;white-space: unset;"><div style="width:110px">' + SemesterSubSpecialization.paperDetails[j].paperTitle + '</div></td>'
        //            }
        //        }
        //    }
        //}
        strHtml += '</tr><tr>';
        //Get Credit
        console.log(SemesterSubSpecialization.paperDetails)
        for (j = 0; j < SemesterSubSpecialization.paperDetails.length; j++) {
            strHtml += '<td colspan="2">Cr.</td><td>' + SemesterSubSpecialization.paperDetails[j].credits + '</td>'
        }

        strHtml += '</tr><tr>';
        //Theory Or Practicle                
        for (j = 0; j < SemesterSubSpecialization.paperDetails.length; j++) {
            
            strHtml += '<td colspan="3">' + SemesterSubSpecialization.paperDetails[j].paperType  + '</td>'
            
        }
        strHtml += '</tr><tr>';
        // Th PR Total Mark

        loop2: for (j = 0; j < ExamDetailJson.length; j++) {
            var InternalMark = parseFloat(ExamDetailJson[j].theoryInternalMax) || 0;
            var ExternalMark1 = parseFloat(ExamDetailJson[j].theoryExternalSection1Max) || 0;
            var ExternalMark2 = parseFloat(ExamDetailJson[j].theoryExternalSection2Max) || 0;
            var ExternalTotal = ExternalMark1 + ExternalMark2;
            var PracticleMark = parseFloat(ExamDetailJson[j].practicalMaxMarks) || 0;
            var IsRequiredDoubleForMHead = false;
            if ((InternalMark + ExternalTotal + PracticleMark) == 50) {
                IsRequiredDoubleForMHead = true;
            } else {
                IsRequiredDoubleForMHead = false;
            }
			
            if(PracticleMark > 0)
                PracticleMark = 100;
			
            //if (InternalMark == 100 || ExternalTotal == 100 || PracticleMark == 100) {
            if (InternalMark >= 100 || ExternalTotal >= 100 || PracticleMark >= 100 || (IsRequiredDoubleForMHead == true && (InternalMark == 50 || ExternalTotal == 50 || PracticleMark == 50))) {
                strHtml += '<td colspan="3">'+(InternalMark+ExternalTotal+PracticleMark)+'</td>'
            } else if (InternalMark != 0 && ExternalTotal != 0) {
                strHtml += '<td>' + InternalMark + '</td><td>' + ((ExternalTotal == 0) ? "ABS" : ExternalTotal) + '</td><td>' + (InternalMark + ExternalTotal + PracticleMark) + '</td>'
            } else { 
                strHtml += '<td colspan="3">NA</td>' 
            }
        }




        strHtml += '</tr><tr>';
        //for (m = 1; m <= SubjectCount; m++) {
        //    var strCode = "Code" + m;
        //    var IsFound = false;
            
        //}
        loop3: for (j = 0; j < ExamDetailJson.length; j++) {
            if (ExamDetailJson[j].paperCode.toUpperCase() != '') {
                var DetailScrp = {};
                DetailScrp.InternalMark = parseFloat(ExamDetailJson[j].theoryInternalMax) || 0;
                DetailScrp.ExternalMark1 = parseFloat(ExamDetailJson[j].theoryExternalSection1Max) || 0;
                DetailScrp.ExternalMark2 = parseFloat(ExamDetailJson[j].theoryExternalSection2Max) || 0;
                DetailScrp.ExternalTotal = DetailScrp.ExternalMark1 + DetailScrp.ExternalMark2;
                DetailScrp.PracticleMark = parseFloat(ExamDetailJson[j].practicalMaxMarks) || 0;
                DetailScrp.theoryInternalPassing=parseFloat(ExamDetailJson[j].theoryInternalPassing) || 0;
                DetailScrp.theoryExternalPassing=parseFloat(ExamDetailJson[j].theoryExternalPassing) || 0;

                if ((DetailScrp.InternalMark + DetailScrp.ExternalTotal + DetailScrp.PracticleMark) == 50) {
                    DetailScrp.IsRequiredDouble = true;
                } else {
                    DetailScrp.IsRequiredDouble = false;
                }
                DetailScrp.Code = strCode;
                DetailScrp.PaperCode = ExamDetailJson[j].paperCode;
                DetailScrp.Credit = ExamDetailJson[j].credits;
                DetailScrp.IsFound = true;
                DetailsScrapper.push(DetailScrp);
                if (DetailScrp.InternalMark >= 100 || DetailScrp.ExternalTotal >= 100 || DetailScrp.PracticleMark >= 100 || (DetailScrp.IsRequiredDouble == true && (DetailScrp.InternalMark == 50 || DetailScrp.ExternalTotal == 50 || DetailScrp.PracticleMark == 50))) {
                    if (DetailScrp.InternalMark >= 100 || DetailScrp.InternalMark == 50) { strHtml += '<td colspan="3">I</td>' }
                    else if (DetailScrp.ExternalTotal >= 100 || DetailScrp.ExternalTotal == 50) strHtml += '<td colspan="3">F</td>'
                    else if (DetailScrp.PracticleMark >= 100 || DetailScrp.PracticleMark == 50) strHtml += '<td colspan="3">P</td>'
                    else { '<td>else</td>' }
                } else if (DetailScrp.InternalMark != 0 && DetailScrp.ExternalTotal != 0) {
                    strHtml += '<td>I</td><td>F</td><td>T</td>'
                }
                IsFound = true;
            } 
        }
        strHtml += '</tr><tr class="BodyTable">';
        var Count = 1;
        for (a = 0; a < $scope.StudentData.length; a++) {

            strHtml += '<td style="padding-left: 15px;padding-right: 15px;">' + Count + '</td><td><div style="width: 65px;">' + $scope.StudentData[a].SeatNumber + '<div></td><td style="padding-left: 5px;padding-right: 5px;">' + $scope.RemoveChar($scope.StudentData[a].PRN) + '</td>' +
                '<td><div style="text-align: left;padding-left: 10px;;padding-right: 10px;">' + $scope.StudentData[a].LastName + ' ' + $scope.StudentData[a].FirstName + ' ' + $scope.StudentData[a].FatherName + ' ' + $scope.StudentData[a].MotherName + '</div>';
            //<td style="padding-left: 24px;padding-right: 24px;">' + $scope.StudentData[a].RollNumber + '</td>
            //for (b = 0; b < SubjectCount; b++) {
            Count++;
            //parseInt(ExternalSection1C) || 0;
            var TotalSubjectmark = 0;
            var TotalGrade = 0;
            var TotalCredit = 0;
            var GradePoint = 0;
            var TotalGradePoint = 0;
            var StudentFailedCount=0;
            var StudentFailedCreditTotal=0;
            for (c = 0; c < DetailsScrapper.length; c++) {
                var key=getKeyByValue($scope.StudentData[a],DetailsScrapper[c].PaperCode);
                DetailsScrapper[c].IsMyPaper=true;
                if(key==undefined){
                    DetailsScrapper[c].IsMyPaper=false;
                    if (DetailsScrapper[c].InternalMark >= 100 || DetailsScrapper[c].ExternalTotal >= 100 || DetailsScrapper[c].PracticleMark >= 100 || (DetailsScrapper[c].IsRequiredDouble == true && (DetailsScrapper[c].InternalMark == 50 || DetailsScrapper[c].ExternalTotal == 50 || DetailsScrapper[c].PracticleMark == 50))) {
                        strHtml += '<td  colspan="3">-</td>';
                    }else if (DetailsScrapper[c].InternalMark != 0 && DetailsScrapper[c].ExternalTotal != 0) {
                        strHtml += '<td>-</td><td>-</td><td>-</td>';
                    }
                    continue;
                }
                var lastChar = key[key.length -1];
                //TotalCredit += parseFloat(DetailsScrapper[c].Credit);
                var intc = parseFloat(lastChar);
                //intc += 1;
                var strCode = 'Code' + intc;
                var InternalMarkNaming = 'InternalC' + intc;
                var ExternalSection1C = 'ExternalSection1C' + intc;
                var ExternalSection2C = 'ExternalSection2C' + intc;
                var ExternalTotalC = 'ExternalTotalC' + intc;
                var Grace = 'GraceC' + intc;
                var GraceAlternate = 'Grace' + intc;
                var PracticalMarksC = 'PracticalMarksC' + intc;
                //alert(PracticalMarksC)
                //if (DetailsScrapper[c].Code == $scope.StudentData[a][strCode]) {

                if (DetailsScrapper[c].IsFound) {
                    if (DetailsScrapper[c].InternalMark >= 100 || DetailsScrapper[c].ExternalTotal >= 100 || DetailsScrapper[c].PracticleMark >= 100 || (DetailsScrapper[c].IsRequiredDouble == true && (DetailsScrapper[c].InternalMark == 50 || DetailsScrapper[c].ExternalTotal == 50 || DetailsScrapper[c].PracticleMark == 50))) {
                        var intto =Math.floor(parseFloat($scope.StudentData[a][InternalMarkNaming]) || 0)
                        //var Externtot = parseFloat($scope.StudentData[a][ExternalTotalC]) || 0;
                        var Externtot =Math.ceil((parseFloat($scope.StudentData[a][ExternalSection1C])||0) + (parseFloat($scope.StudentData[a][ExternalSection2C])||0));
						
                        if(Externtot < parseFloat($scope.StudentData[a][ExternalTotalC])){
                            Externtot =Math.ceil(parseFloat($scope.StudentData[a][ExternalTotalC]) || 0);
                        }
                        var Praptot =Math.ceil(parseFloat($scope.StudentData[a][PracticalMarksC]) || 0);
                        var GraceInt = 0;
                        if ($scope.StudentData[a][Grace] == undefined) {
                            GraceInt = parseFloat($scope.StudentData[a][GraceAlternate]) || 0;
                        } else {
                            GraceInt = parseFloat($scope.StudentData[a][Grace]) || 0;
                        }

                        var toto = (intto +  Math.ceil(Externtot + Praptot + GraceInt));
                        
					    
                        if($scope.StudentData[a][InternalMarkNaming] == "ABS"){
                            StudentFailedCount++;
                            StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                        }
                        else if((DetailsScrapper[c].theoryInternalPassing>0&&intto<DetailsScrapper[c].theoryInternalPassing)||(DetailsScrapper[c].theoryExternalPassing>0&&(Externtot+GraceInt+Praptot)<DetailsScrapper[c].theoryExternalPassing)){
                            StudentFailedCount++;
                            StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                        }
                        if ((DetailsScrapper[c].PracticleMark == 50 && parseFloat(Praptot)+GraceInt<20) || (DetailsScrapper[c].PracticleMark == 100 && parseFloat(Praptot)+GraceInt<40)||(DetailsScrapper[c].PracticleMark == 150 && parseFloat(Praptot)+GraceInt<60))
                        {
                            StudentFailedCount++;
                            StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                        }

                        var DisplayTotal = intto + Externtot + Praptot;
                        DisplayTotal=Math.round(DisplayTotal);
                        var GraceIntDisplay = GraceInt;
                        if (DetailsScrapper[c].IsRequiredDouble) {
                            toto = toto * 2;
                            DisplayTotal = DisplayTotal * 2;
                            GraceIntDisplay = GraceInt * 2;
                        }
                        if(DetailsScrapper[c].PracticleMark > 100){
                            toto = (toto/(DetailsScrapper[c].PracticleMark/50))*2;
                        }
                        toto=Math.round(toto);
                        TotalSubjectmark += toto;
                        GradePoint = parseFloat($scope.GetGradePoint(toto, 0)) || 0;//got Grade point for Subject
                        var intcred = parseFloat(DetailsScrapper[c].Credit) || 0;//Credit of the subject
                        intcred=Math.round((100*(parseFloat(intcred) || 0))/DetailsScrapper[c].PracticleMark)
                        TotalGradePoint += (GradePoint * intcred);
                        TotalCredit+=intcred;
                        //var Credit = parseFloat(Credit) || 0;
                        //TotalCrdit += Credit;
                        TotalGrade += parseFloat($scope.GetGradePoint(toto + Praptot)) || 0

                        ////Debug code
                        //if (GraceInt == 0) { strHtml += '<td colspan="3">' + toto + '+' + GraceInt +'?'+intcred+'-'+'<'+GradePoint+'>'+(GradePoint * intcred)+'?'+ '</td>' }
                        //else { strHtml += '<td colspan="3">' + DisplayTotal + '+' + GraceInt +'?'+intcred+'-'+'<'+GradePoint+'>'+(GradePoint * intcred)+'?'+ '</td>' }

                        if (GraceInt == 0) { strHtml += '<td colspan="3">' + toto+'</td>' }
                        else { strHtml += '<td colspan="3">' + DisplayTotal + '+' + GraceInt + '</td>' }


                        //'</td><td>' + toto+'?'+GradePoint+'?'+ '</td>';
                        //if ((detailsscrapper[c].internalmark == 100) || (detailsscrapper[c].isrequireddouble == true && detailsscrapper[c].internalmark == 50)) { strhtml += '<td colspan="3">' + intto + '</td>' }
                        //else if ((detailsscrapper[c].externaltotal == 100) || (detailsscrapper[c].isrequireddouble == true && detailsscrapper[c].externaltotal == 50)) {
                        //    if (graceint == 0) { strhtml += '<td colspan="3">' + externtot + '</td>' }
                        //    else { strhtml += '<td colspan="3">' + displaytotal + '+' + graceint + '</td>' }
                        //}
                        //else if ((DetailsScrapper[c].PracticleMark == 100) || (DetailsScrapper[c].IsRequiredDouble == true && DetailsScrapper[c].PracticleMark == 50)) {
                        //    if (GraceInt == 0) {
                        //        strHtml += '<td colspan="3">' + Praptot + '</td>'
                        //    } else {
                        //        strHtml += '<td colspan="3">' + DisplayTotal + '+' + GraceInt + '</td>'
                        //    }
                        //}
                        //else { '<td colspan="3">No match</td>' }
                    } else if (DetailsScrapper[c].InternalMark != 0 && DetailsScrapper[c].ExternalTotal != 0) {
                        var intto =Math.floor(parseFloat($scope.StudentData[a][InternalMarkNaming]) || 0)
                        //var Externtot = parseFloat($scope.StudentData[a][ExternalTotalC]) || 0;
                        var Externtot =Math.ceil((parseFloat($scope.StudentData[a][ExternalSection1C])||0) + (parseFloat($scope.StudentData[a][ExternalSection2C])||0));
						
                        if(Externtot < parseFloat($scope.StudentData[a][ExternalTotalC])){
                            Externtot = parseFloat($scope.StudentData[a][ExternalTotalC]) || 0;
                        }
						
                        var Praptot =Math.ceil(parseFloat($scope.StudentData[a][PracticalMarksC]) || 0)

                        var GraceInt = 0;
                        if ($scope.StudentData[a][Grace] == undefined) {
                            GraceInt = parseFloat($scope.StudentData[a][GraceAlternate]) || 0;
                        } else {
                            GraceInt = parseFloat($scope.StudentData[a][Grace]) || 0;
                        }
						
                        if($scope.StudentData[a][InternalMarkNaming] == "ABS"){
                            StudentFailedCount++;
                            StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                        }
                        else if((DetailsScrapper[c].theoryInternalPassing>0&&intto<DetailsScrapper[c].theoryInternalPassing)||(DetailsScrapper[c].theoryExternalPassing>0&&Math.ceil(Externtot+GraceInt+Praptot)<DetailsScrapper[c].theoryExternalPassing)){
                            StudentFailedCount++;
                            StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                        }
                        if ((DetailsScrapper[c].PracticleMark == 50 && parseFloat(Praptot)+GraceInt<20) || (DetailsScrapper[c].PracticleMark == 100 && parseFloat(Praptot)+GraceInt<40)||(DetailsScrapper[c].PracticleMark == 150 && parseFloat(Praptot)+GraceInt<60))
                        {
                            StudentFailedCount++;
                            StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                        }

                        var toto = intto + (Math.ceil(Externtot + Praptot + GraceInt));
                        var DisplayGrace = GraceInt;
                        var DisplayTotal = intto + Externtot + Praptot;
                        DisplayTotal=Math.round(DisplayTotal);
                        var intcred = parseFloat(DetailsScrapper[c].Credit) || 0;
                        if (DetailsScrapper[c].IsRequiredDouble) {
                            DisplayTotal = DisplayTotal * 2;
                            DisplayGrace = GraceInt * 2;
                            intcred=(intcred*2);
                            if (intto != 0) {
                                intto = intto * 2;
                            } if (Externtot != 0) {
                                Externtot = (Externtot + GraceInt) * 2;
                            } if (Praptot != 0) {
                                Externtot = Praptot * 2;
                            }
                        }
                        TotalCredit+=intcred;
                        toto=Math.round(toto);
                        TotalSubjectmark += toto;
                        TotalGrade += parseFloat($scope.GetGradePoint(toto + Praptot)) || 0
                        GradePoint = parseFloat($scope.GetGradePoint(toto, 0)) || 0;

                        
                        TotalGradePoint += (GradePoint * intcred);
						
                        if($scope.StudentData[a][InternalMarkNaming] == "ABS"){
                            intto = "ABS";
                            Externtot = "NP";
                        }
                        //var Credit = parseFloat(Credit) || 0; 
                        //TotalCrdit += Credit;
                        //else { strHtml += '<td colspan="3">' + DisplayTotal + '+' + GraceInt +'?'+intcred+'-'+(GradePoint * intcred)+'?'+ '</td>' }

                        
                        //debug
                        //if (GraceInt != 0) {
                        //    strHtml += '<td>' + intto +
                        //        '</td><td>' + Externtot + '+' + DisplayGrace +
                        //        '</td><td>' + (DisplayTotal+GraceInt) +'?'+intcred+'-'+(GradePoint * intcred)+'?'+'</td>';
                        //}
                        //else {
                        //    strHtml += '<td>' + intto +
                        //         '</td><td>' + ((Externtot==0)?"ABS":Externtot) +
                        //        '</td><td>' + (DisplayTotal+GraceInt)+'?'+intcred+'-'+'<'+GradePoint+'>'+(GradePoint * intcred)+'?'+'</td>';
                        //}

                        if (GraceInt != 0) {
                            strHtml += '<td>' + intto +
                                '</td><td>' + Externtot + '+' + DisplayGrace +
                                '</td><td>' + (DisplayTotal+GraceInt) +'</td>';
                        }
                        else {
                            strHtml += '<td>' + intto +
                                 '</td><td>' + ((Externtot==0)?"ABS":Externtot) +
                                '</td><td>' + (DisplayTotal+GraceInt)+'</td>';
                        }

                    }
                } else {
                    var intto =Math.floor(parseFloat($scope.StudentData[a][InternalMarkNaming]) || 0);
                    //var Externtot = parseFloat($scope.StudentData[a][ExternalTotalC]) || 0
                    var Externtot =Math.ceil((parseFloat($scope.StudentData[a][ExternalSection1C])|| 0) + (parseFloat($scope.StudentData[a][ExternalSection2C])||0));
						
                    if(Externtot < parseFloat($scope.StudentData[a][ExternalTotalC])){
                        Externtot = parseFloat($scope.StudentData[a][ExternalTotalC]) || 0;
                    }
                    var Praptot =Math.ceil(parseFloat($scope.StudentData[a][PracticalMarksC]) || 0)
                    var GraceInt = 0;
                    if ($scope.StudentData[a][Grace] == undefined) {
                        GraceInt = parseFloat($scope.StudentData[a][GraceAlternate]) || 0;
                    } else {
                        GraceInt = parseFloat($scope.StudentData[a][Grace]) || 0;
                    }
                    var GraceInt = parseFloat($scope.StudentData[a][Grace]) || 0;

                    if($scope.StudentData[a][InternalMarkNaming] == "ABS"){
                        StudentFailedCount++;
                        StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                    }
                    else if((DetailsScrapper[c].theoryInternalPassing>0&&intto<DetailsScrapper[c].theoryInternalPassing)||(DetailsScrapper[c].theoryExternalPassing>0&&Math.ceil(Externtot+GraceInt+Praptot)<DetailsScrapper[c].theoryExternalPassing)){
                        StudentFailedCount++;
                        StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                    }
                    if ((DetailsScrapper[c].PracticleMark == 50 && parseFloat(Praptot)+GraceInt<20) || (DetailsScrapper[c].PracticleMark == 100 && parseFloat(Praptot)+GraceInt<40)||(DetailsScrapper[c].PracticleMark == 150 && parseFloat(Praptot)+GraceInt<60))
                    {
                        StudentFailedCount++;
                        StudentFailedCreditTotal+=(parseFloat(DetailsScrapper[c].Credit) || 0)
                    }
					
                    
                    var DisplayTotal = (intto + Math.ceil(Externtot + Praptot));
                    DisplayTotal=Math.round(DisplayTotal);

                    var toto = intto + Externtot + Praptot + GraceInt;
                    toto=Math.round(toto);
                    TotalSubjectmark += toto;

                    TotalGrade += parseFloat($scope.GetGradePoint(toto + Praptot)) || 0
                    GradePoint = parseFloat($scope.GetGradePoint(TotalSubjectmark, 0)) || 0;
                    var intcred = parseFloat(DetailsScrapper[c].Credit) || 0;
                    TotalGradePoint += (GradePoint * intcred);
                    TotalCredit+=intcred;
                    var Credit = parseFloat(Credit) || 0;
                    //TotalCrdit += Credit;
                    if($scope.StudentData[a][InternalMarkNaming] == "ABS"){
                        intto = "ABS";
                        Externtot = "NP";
                    }

                    if (GraceInt == 0) { strHtml += '<td colspan="3">' + toto + '+' + GraceInt +'?'+intcred+'-'+(GradePoint * intcred)+'?'+ '</td>' }
                    else { strHtml += '<td colspan="3">' + DisplayTotal + '+' + GraceInt +'?'+intcred+'-'+(GradePoint * intcred)+'?'+ '</td>' }

                    //grace mark will get added
                    //if (GraceInt != 0) {
                    //    strHtml += '<td>' + intto +
                    //        '</td><td>' + Externtot +' + '+ DisplayGrace +
                    //        '</td><td>' + (DisplayTotal+GraceInt) +'?'+intcred+'-'+(GradePoint * intcred)+'?'+'</td>';
                            
                    //}
                    //else {
                    //    strHtml += '<td>' + intto +
                    //        '</td><td>' + ((Externtot==0)?"ABS":Externtot) +
                    //        //'</td><td>' + toto + '</td>';
                    //        '</td><td>' + toto+'?'+intcred+'-'+'<'+GradePoint+'>'+(GradePoint * intcred)+'?'+'</td>';
                    //}

                    if (GraceInt != 0) {
                        strHtml += '<td>' + intto +
                            '</td><td>' + Externtot +' + '+ DisplayGrace +
                            '</td><td>' + (DisplayTotal+GraceInt) +'</td>';
                            
                    }
                    else {
                        strHtml += '<td>' + intto +
                            '</td><td>' + ((Externtot==0)?"ABS":Externtot) +
                            //'</td><td>' + toto + '</td>';
                            '</td><td>' + toto+'</td>';
                    }


                }
            }

            var TotalMySubjectCount=DetailsScrapper.filter(x=>x.IsMyPaper==true);
            var weightage = parseFloat(TotalSubjectmark) / TotalMySubjectCount.length;
            var TotalGradePointCouned = TotalGradePoint / TotalCredit;

            //code for remark check
            if (StudentFailedCreditTotal != undefined) {
                if (StudentFailedCreditTotal == 0) {
                    $scope.StudentData[a].Remarks = 'PASS';
                } else if (StudentFailedCreditTotal > 0 && StudentFailedCreditTotal < 13) {
                    $scope.StudentData[a].Remarks = 'Passes with ATKT'
                } else {
                    $scope.StudentData[a].Remarks = 'FAIL'
                }
            }
            //code over for remarks check
            
            if ($scope.StudentData[a].Remarks == undefined) {
                $scope.StudentData[a].Remarks = $scope.StudentData[a].Remark;
            }
            strHtml += '<td>' + $scope.StudentData[a].Remarks + '</td>';
            if ($scope.StudentData[a].Remarks.toUpperCase() == 'PASS') {
                strHtml += '<td>' + weightage.toFixed(2) + '</td>';
                strHtml += '<td>' + $scope.GetGrade((TotalSubjectmark / TotalMySubjectCount.length), 0) + '</td><td>'+ TotalGradePointCouned.toFixed(2) +'</td><td></td></tr>'
            } else {
                strHtml += '<td>-</td><td>-</td><td>-</td><td></td></tr>'
            }
        }
        return strHtml + '</table><div style="padding-top:100px"><table style="min-width:100%"><tr><td></td><td></td><td> <!--<img style="padding:5px" src="Principal.png" />--><img src="MarksheetModuleJs/ControllerOfExam.png" /></td></tr><tr><td style="width:40%"></td><td style="width:40%"></td><td style="width:20%">Controller of Examinations</td></tr></table></div><div class="DivResult"></div>';
    }
    else {
        strHtml += '<tr><td colspan="6" style="font-family: Broadway;font-size: 25px;padding: 8px;border: 0px;">S.V.T. COLLEGE OF  HOME  SCIENCE (AUTONOMOUS)</td><tr>' +
        '<tr><td colspan="6" style="font-size: 19px;padding: 8px;border: 0px;">List of Toppers  ' + $scope.StudentData[0].Course + ' Semester - ' + GetSmesterInRoman($scope.StudentData[0].Semester) + ' (' + month_name(parseFloat(MonthSelected)) + ' - ' + YearSelected + ')</td></tr>' +
        '<tr><td colspan="6"><div></div></td></tr>' +
        '<tr style="font-size: 15px;padding: 8px;text-align:left;border-left: 2px solid;border-right: 2px solid;"><td style="text-align:center;height:45px">No.</td><td style="padding:5px">Specialization</td><td style="padding:5px">Name of the student</td><td style="text-align:center">Grade</td><td style="text-align:center">G.P.</td><td style="text-align:center;padding-left: 10px;padding-right: 10px;">Weighted %</td></tr>';

        //code

        for (m = 1; m <= SubjectCount; m++) {
            var strCode = "Code" + m;
            loop4: for (j = 0; j < SemesterSubSpecialization.paperDetails.length; j++) {
                var StudentCode = $scope.StudentData[0][strCode];
                if (SemesterSubSpecialization.paperDetails[j].code.toUpperCase() == StudentCode.toUpperCase()) {
                    //console.log(SemesterSubSpecialization.paperDetails[j].code);
                    var Student = getMax($scope.StudentData, m);
                    var InternalMark = 'InternalC' + m;
                    var External = 'ExternalTotalC' + m;
                    var Practicale = 'PracticalMarksC' + m;
                    var intint = parseFloat(Student[InternalMark]) || 0;
                    var intExt = parseFloat(Student[External]) || 0;
                    var intPrct = parseFloat(Student[Practicale]) || 0;
                    var FinalTotal = intint + intExt + intPrct;
                    strHtml += '<tr style="border-left: 2px solid; border-right: 2px solid;"><td style="width: 50px;height: 45px;">' + m + '</td><td style="text-align: left;padding-left: 5px;">' + SemesterSubSpecialization.paperDetails[j].paperTitle + '</td>' +
                        '<td style="text-align: left;padding-left: 5px;">' + Student.LastName + ' ' + Student.FirstName + ' ' + Student.FatherName + '' + Student.MotherName + '</td>' +
                        '<td style="width: 90px;">' + $scope.GetGrade((FinalTotal), 0) + '</td>' +
                        '<td style="width: 90px;">' + $scope.GetGradePoint(FinalTotal) + '</td>' +
                        '<td style="width: 150px;padding-left: 10px;padding-right: 10px;">' + (FinalTotal).toFixed(2) + '</td></tr>';
                    break loop4;

                }
            }
        }
        //end code
        $('.BordredTable').css('font-size', '15px');
        strHtml += '<tr style="text-align: end;padding: 15px;padding-bottom: 5px;border-top:2px solid black"><td colspan="6" style="text-align: end;padding: 15px;padding-bottom: 5px;border: 0px solid black;"><div style="height:60px"></div></td></tr>'
        strHtml += '<tr><td colspan="6" style="text-align: end;padding: 15px;padding-bottom: 5px;border: 0px solid black;">' + $('#ControllerName').val() + '</td></tr>'
        strHtml += '<tr><td colspan="6" style="text-align: end;padding: 15px;padding-top: 5px;border: 0px solid black;">Exam Controller</td></tr>'
        //$('#ToppersContainer').append('<div style="height:30px;"></div><div style="text-align:right">Exam Controller</div>')
        $('#htmlTextToppers').html(strHtml)
    }

}

function GetSmesterInRoman(sem) {
    var intsam = parseFloat(sem)
    if (intsam == 1) { return 'I' }
    else if (intsam == 2) { return 'II' }
    else if (intsam == 3) { return 'III' }
    else if (intsam == 4) { return 'IV' }
    else if (intsam == 5) { return 'V' }
    else if (intsam == 6) { return 'VI' }
    else if (intsam == 7) { return 'VII' }
    else if (intsam == 8) { return 'VIII' }
}
var month_name = function (dt) {
    mlist = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    return mlist[(dt - 1)];
};
function getMax(arr, prop) {
    var InternalMark = 'InternalC' + prop;
    var External = 'ExternalTotalC' + prop;
    var Practicale = 'PracticalMarksC' + prop;
    var max = {}
    max.InternalC1 = 0;
    max.ExternalTotalC1 = 0;
    max.PracticalMarksC1 = 0;
    for (var i = 0 ; i < arr.length ; i++) {
        var intint = parseFloat(arr[i][InternalMark]) || 0;
        var intExt = parseFloat(arr[i][External]) || 0;
        var intPrct = parseFloat(arr[i][Practicale]) || 0;
        var Existingintint = parseFloat(max[InternalMark]) || 0;
        var ExistingintExt = parseFloat(max[External]) || 0;
        var ExistingintPrct = parseFloat(max[Practicale]) || 0;
        if (!max || (intint + intExt + intPrct) > (Existingintint + ExistingintExt + ExistingintPrct))
            //arr[i].FinalTotalMark = (intint + intExt + intPrct);
            max = arr[i];
    }
    return max;
}

$scope.GetGeneralElectiveInfo = function (code) {
    for (i = 0; i < GeneralElectiveInfo.length; i++) {
        if (GeneralElectiveInfo[i].Specialization.toUpperCase() == $scope.StudentData[0].Specialisation.toUpperCase() || GeneralElectiveInfo[i].specialisationCode.toUpperCase() == $scope.StudentData[0].Specialisation.toUpperCase()) {
            for (j = 0; j < GeneralElectiveInfo[i].ElectiveSubject.length; j++) {
                for (k = 0; k < GeneralElectiveInfo[i].ElectiveSubject[j].Subject.length; k++) {
                    if (code.toUpperCase() == GeneralElectiveInfo[i].ElectiveSubject[j].Subject[k].Code.toUpperCase()) {
                        return GeneralElectiveInfo[i].ElectiveSubject[j].Title;
                    }
                }
            }
        }
    }
    return 'GE';
}

$scope.GetCreditsLadger = function (subjctCode) {
    var credit = 0;
    for (i = 0; i < ExamDetailJson.length; i++) {
        if (subjctCode.toUpperCase() == ExamDetailJson[i].paperCode.toUpperCase()) {
            credit = parseFloat(ExamDetailJson[i].credits);
        }
    }
    return credit;
}
$scope.GetpaperTypeLadger = function (subjctCode) {
    for (i = 0; i < ExamDetailJson.length; i++) {
        if (subjctCode.toUpperCase() == ExamDetailJson[i].paperCode.toUpperCase()) {
            if (ExamDetailJson[i].paperType == 'Practical')
            { return 'PR' } else { return 'Theory' }
        }
    }
}
$scope.TotalGradePoint = function (GradePoint, credit) {
    var GradePointCN = parseFloat(GradePoint) || 0;
    var creditCN = parseFloat(credit) || 0;
    return GradePointCN * creditCN;
}
$scope.GetGradePoint = function (Marks) {
    Marks=Math.round(Marks);
    if (Marks >= 90)
        return parseFloat("10");
    if (Marks >= 80 && Marks < 90)
        return parseFloat("9." + ((Math.round(Marks)) % 10) + "0");
    if (Marks >= 70 && Marks < 80)
        return parseFloat("8." + ((Math.round(Marks)) % 10) + "0");
    if (Marks >= 60 && Marks < 70)
        return parseFloat("7." + ((Math.round(Marks)) % 10) + "0");
    if (Marks >= 55 && Marks < 60)
        return parseFloat("6." + ((Math.round(Marks)) % 5) * 2 + "0");
    if (Marks >= 50 && Marks < 55)
        return parseFloat("5." + (((Math.round(Marks)) % 10) + 5) + "0");
    if (Marks >= 45 && Marks < 50)
        return parseFloat("5." + ((Math.round(Marks)) % 5) * 2 + "0");
    if (Marks >= 40 && Marks < 45)
        return parseFloat("4." + ((Math.round(Marks)) % 5) * 2 + "0");
    if (Marks >= 0 && Marks < 40)
        return 0;
}
$scope.RemoveChar = function (val)
{ return val.replace(/'/g, ""); }

$scope.GetGrade = function (val1, val2) {
    var InternalCN = parseFloat(val1) || 0;
    var ExternalTotalCN = parseFloat(val2) || 0;
    if (InternalCN + ExternalTotalCN < 40) return 'F';
    else if (InternalCN + ExternalTotalCN >= 40 && InternalCN + ExternalTotalCN < 44) return 'P'
    else if (InternalCN + ExternalTotalCN >= 44 && InternalCN + ExternalTotalCN < 50) return 'C';
    else if (InternalCN + ExternalTotalCN >= 50 && InternalCN + ExternalTotalCN < 55) return 'B';
    else if (InternalCN + ExternalTotalCN >= 55 && InternalCN + ExternalTotalCN < 60) return 'B+';
    else if (InternalCN + ExternalTotalCN >= 60 && InternalCN + ExternalTotalCN < 70) return 'A';
    else if (InternalCN + ExternalTotalCN >= 70 && InternalCN + ExternalTotalCN < 80) return 'A+';
    else if (InternalCN + ExternalTotalCN >= 80 && InternalCN + ExternalTotalCN < 90) return 'O';
    else if (InternalCN + ExternalTotalCN >= 90 && InternalCN + ExternalTotalCN < 101) return 'O+';
}

var SemesterSub;
var ExamDetailJson;
var Records = $.parseJSON(localStorage.getItem("CurrentStudentRecord"));
var UpdatedSubjectCount = 0;
var StackCount;
CheckStudentSubjectCOunt: for (var rec = 0; rec < Object.keys(Records[0]).length; rec++) {
    var IntCode = 1 + rec;
    var key = 'Code' + IntCode;
    if (Records[0][key] == undefined) {
        SubjectCount = rec;
        StackCount = rec;
        break CheckStudentSubjectCOunt;
    }
}
console.log(Records)
var DistinctSpecialization = alasql('SELECT DISTINCT Specialisation FROM ?', [Records]);
console.log(DistinctSpecialization)

for (var dist = 0; dist < DistinctSpecialization.length; dist++) {
    if (DistinctSpecialization[dist] != '') {
        SubjectCount = StackCount;
        var ResultForCurrentStud = alasql('SELECT * FROM ? WHERE Specialisation=?', [Records, DistinctSpecialization[dist].Specialisation])
        $scope.StudentData = ResultForCurrentStud;
        UpdateStudentSubjectCount: for (var subCo = 1; subCo < (SubjectCount + 1) ; subCo++) {
            var strcodetocheck = "Code" + subCo
            if ($scope.StudentData[0][strcodetocheck] == undefined || $scope.StudentData[0][strcodetocheck] == '' || $scope.StudentData[0][strcodetocheck] == null) {
                SubjectCount = subCo-1;
            }
        }

        $scope.CreateLadgerReport();
    }
    if (dist == DistinctSpecialization.length) {
        $scope.$apply();
    }
}
});