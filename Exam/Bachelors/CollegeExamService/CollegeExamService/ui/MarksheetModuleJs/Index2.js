
var app = angular.module('MarksheetApp', []);

app.controller('MarksheetController', function ($scope, $filter, $q) {
    
    $scope.DateOfExamToPrint = getUrlVars()["date"];

    $scope.readFile = function () {
        $scope.DateSelectedval = $('#txtnewDatePicker').val();
        var reader = new FileReader();
        $scope.StudentData = [];
        $scope.dataItem = [];
        var SubjectCount = 0;
        $scope.YearSelected = $scope.DateSelectedval.substr($scope.DateSelectedval.length - 4);
    };

    $scope.GetCredits = function (subjctCode) {
        var credit = 0;
        for (i = 0; i < ExamDetailJson.length; i++) {
            if (subjctCode.toUpperCase() == ExamDetailJson[i].paperCode.toUpperCase()) {
                credit = parseFloat(ExamDetailJson[i].credits);
            }
        }
        return credit;
    }

    $scope.FinalStudenDetailsWithAllSpe = [];
    $scope.FinalResultDisplay = 0;
    var StudentType = getUrlVars()["StudentType"];
    if (StudentType.toLowerCase() == 'elective') {
        $scope.FinalResultDisplay = 1;
        StudentType = 'honors'
    }
    $scope.GenerateStudentSpecializedMarksheet = function (CurrentRecord_ToProceess) {
        var d = $q.defer();
        $scope.IsSamesterOneTwo = getUrlVars()["sem"];
        
        CallAPI("User/GetPaperList?Course=" + getUrlVars()["course"] + "&specialization=" + StudentType + "&sem=" + getUrlVars()["sem"]).done(function (SubjectResponse) {
            $scope.StudentData = CurrentRecord_ToProceess;
            SemesterSub = SubjectResponse;
            //pasted code 12/08/2018
            for (var i = 0; i < $scope.StudentData.length; i++) {
                var InternalMarkTotal = 0;
                var ExternalMarkTotal = 0;
                var FinalMarkTotal = 0;
                $scope.StudentData[i].MarkSheetArray = new Array();
                if ($scope.StudentData[i].Remarks == undefined) {
                    $scope.StudentData.Remarks = $scope.StudentData[i].Remark;
                }
                for (var k = 0; k < SemesterSub.length; k++) {
                    if ((SemesterSub[k].specialisation.toUpperCase() == $scope.StudentData[i].Specialisation.toUpperCase() && SemesterSub[k].courseShortName.toUpperCase() != "HONORS") || (SemesterSub[k].specialisationCode.toUpperCase() == $scope.StudentData[i].Specialisation.toUpperCase() && SemesterSub[k].courseShortName.toUpperCase() != "HONOURS")) {
                        ExamDetailJson = SemesterSub[k].paperDetails;//Only implimented in 4th Sem
                        for (var j = 0; j < SemesterSub[k].paperDetails.length; j++) {
                            $scope.StudentData[i].Specialisation = SemesterSub[k].specialisation.toUpperCase();
                            for (var m = 1; m <= SubjectCount; m++) {
                                var strCode = "Code" + m;
                                if (SemesterSub[k] == undefined) {
                                    console.log('Erro')
                                }
                                if (SemesterSub[k].paperDetails == undefined) {
                                    console.log('Erro')
                                }
                                if (SemesterSub[k].paperDetails[j] == undefined) {
                                    console.log('Erro')
                                }
                                if (SemesterSub[k].paperDetails[j].code == undefined) {
                                    console.log('Erro')
                                }
                                if ($scope.StudentData[i][strCode] != undefined && $scope.StudentData[i][strCode] == SemesterSub[k].paperDetails[j].code) {
                                    $scope.MarksheetVariable = {};

                                    var strInternalMarkCode = "InternalC" + m;
                                    var strExternalMarkCode = "ExternalTotalC" + m;
                                    var strPracticalMarkCode = "PracticalMarksC" + m;
                                    var Grace = 'GraceC' + m;
                                    var GraceAlternate = 'Grace' + m;
                                    var intInternalMark = Math.floor(parseFloat($scope.StudentData[i][strInternalMarkCode]) || 0);
                                    var intExternalMark = Math.ceil(parseFloat($scope.StudentData[i][strExternalMarkCode]) || 0);

                                    var intPracticleMark = Math.ceil(parseFloat($scope.StudentData[i][strPracticalMarkCode]) || 0);
                                    var intGraceMark = 0;
                                    if ($scope.StudentData[i][Grace] != undefined) {
                                        intGraceMark = parseFloat($scope.StudentData[i][Grace]) || 0;
                                    } else {
                                        intGraceMark = parseFloat($scope.StudentData[i][GraceAlternate]) || 0;
                                    }

                                    intExternalMark += intPracticleMark;
                                    intExternalMark += intGraceMark;//Grace Mark will get added into External Mark 

                                    var strRemarksCode = "Remarks" + m;

                                    //Get Passing Result Status Remark;
                                    for (var rem = 0; rem < ExamDetailJson.length; rem++) {
                                        if (ExamDetailJson[rem].paperCode == $scope.StudentData[i][strCode]) {
                                            var IntMarkReq = (ExamDetailJson[rem].theoryInternalPassing) || 0;
                                            var ExtMarkReq = Math.floor(parseFloat(ExamDetailJson[rem].theoryExternalPassing) || 0);
                                            var PractMarkReq = Math.floor(parseFloat(ExamDetailJson[rem].practicalMaxMarks) || 0);
                                            $scope.MarksheetVariable.StudentInternalAttended = true;
                                            $scope.MarksheetVariable.StudentExternalAttended = true;

                                            var IntMarkMax = parseFloat(ExamDetailJson[rem].theoryInternalMax) || 0;
                                            var ExtMark1Max = parseFloat(ExamDetailJson[rem].theoryExternalSection1Max) || 0;
                                            var ExtMark2Max = parseFloat(ExamDetailJson[rem].theoryExternalSection2Max) || 0;
                                            var ExtMaxMarkRe = ExtMark1Max + ExtMark2Max;
                                            $scope.MarksheetVariable.CreditScore = ExamDetailJson[rem].credits;
                                            if ((IntMarkMax + ExtMaxMarkRe + PractMarkReq) == 50) {//If total mark is out of 50 than it will double mark from 100 
                                                $scope.MarksheetVariable.CreditScore = Math.round((100 * (parseFloat($scope.MarksheetVariable.CreditScore) || 0)) / (IntMarkMax + ExtMaxMarkRe + PractMarkReq));
                                                if (IntMarkMax != 0) {
                                                    intInternalMark = intInternalMark * 2;
                                                    IntMarkReq = IntMarkReq * 2;
                                                }
                                                if (ExtMaxMarkRe != 0 || PractMarkReq != 0) {
                                                    intExternalMark = intExternalMark * 2;
                                                    ExtMarkReq = ExtMarkReq * 2;
                                                }
                                            }
                                            if (PractMarkReq > 100) {
                                                //intExternalMark = Math.round((RecievedMark / (MaxMarkInJson / 50)) * 2);
                                                intExternalMark = Math.round((intExternalMark / (PractMarkReq / 50)) * 2);
                                                //PractMarkReq=practicalMaxMarks as per Json Object
                                                //ExtMarkReq=External Min Passing Marks Required as per Json Object
                                                ExtMarkReq = Math.round(((100 * ExtMarkReq) / PractMarkReq));
                                                $scope.MarksheetVariable.CreditScore = Math.round((100 * (parseFloat($scope.MarksheetVariable.CreditScore) || 0)) / PractMarkReq);
                                                //ExtMarkReq = 100;
                                            }

                                            if (IntMarkMax > 0 && intInternalMark == 0) {
                                                $scope.MarksheetVariable.StudentInternalAttended = false;
                                                $scope.MarksheetVariable.StudentInternalStatus = 'ABS';
                                                $scope.MarksheetVariable.StudentExternalStatus = 'NP';
                                            }
                                            if (((ExtMark1Max + ExtMark2Max + PractMarkReq) > 0) && intExternalMark == 0) {
                                                $scope.MarksheetVariable.StudentExternalAttended = false;
                                                if ($scope.MarksheetVariable.StudentInternalAttended) {
                                                    $scope.MarksheetVariable.StudentExternalStatus = 'ABS';
                                                } else {
                                                    $scope.MarksheetVariable.StudentExternalStatus = 'NP';
                                                }

                                            }

                                            $scope.MarksheetVariable.IsAssesment = ExamDetailJson[rem].isContinousAssessment;

                                            if ($scope.MarksheetVariable.IsAssesment == undefined) {
                                                $scope.MarksheetVariable.IsAssesment = 0;
                                            } else if ($scope.MarksheetVariable.IsAssesment == "true") {
                                                $scope.MarksheetVariable.IsAssesment = 1;
                                            } else {
                                                $scope.MarksheetVariable.IsAssesment = 0;
                                            }
                                            

                                            if ($scope.StudentData[i].SeatNumber == 'B-RR3294') {
                                                debugger
                                            }
                                            if (ExtMarkReq > 0 && intExternalMark < ExtMarkReq) {
                                                $scope.MarksheetVariable.Remark = 'Repeat Final';
                                            }
                                            if (IntMarkReq > 0 && intInternalMark < IntMarkReq) {
                                                $scope.MarksheetVariable.Remark = 'Repeat Course';
                                            }

                                        }
                                    }
                                    $scope.StudentData[i].Remarks = 'PASS'
                                    var strTotalMarkCode = intInternalMark + intExternalMark;
                                    strTotalMarkCode = Math.round(strTotalMarkCode);
                                    if ($scope.StudentData[i].Remarks.toUpperCase() == 'ATKT') {
                                        $scope.StudentData[i].Remarks = 'Passes with ATKT';
                                    }
                                    $scope.MarksheetVariable.TotalMark = Math.round($scope.MarksheetVariable.TotalMark);
                                    //Get Passing Result Status Remark;
                                    $scope.MarksheetVariable.SubjectName = SemesterSub[k].paperDetails[j].paperTitle;
                                    $scope.MarksheetVariable.PaperCodeFromJson = SemesterSub[k].paperDetails[j].code;
                                    $scope.MarksheetVariable.InternalMark = intInternalMark;
                                    $scope.MarksheetVariable.ExternalMark = intExternalMark;
                                    $scope.MarksheetVariable.TotalMark = strTotalMarkCode;

                                    //Get Grade start
                                    if (strTotalMarkCode < 40) $scope.MarksheetVariable.Grade = 'F';
                                    else if (strTotalMarkCode >= 40 && strTotalMarkCode < 44) $scope.MarksheetVariable.Grade = 'P';
                                    else if (strTotalMarkCode >= 44 && strTotalMarkCode < 50) $scope.MarksheetVariable.Grade = 'C';
                                    else if (strTotalMarkCode >= 50 && strTotalMarkCode < 55) $scope.MarksheetVariable.Grade = 'B';
                                    else if (strTotalMarkCode >= 55 && strTotalMarkCode < 60) $scope.MarksheetVariable.Grade = 'B+';
                                    else if (strTotalMarkCode >= 60 && strTotalMarkCode < 70) $scope.MarksheetVariable.Grade = 'A';
                                    else if (strTotalMarkCode >= 70 && strTotalMarkCode < 80) $scope.MarksheetVariable.Grade = 'A+';
                                    else if (strTotalMarkCode >= 80 && strTotalMarkCode < 90) $scope.MarksheetVariable.Grade = 'O';
                                    else if (strTotalMarkCode >= 90 && strTotalMarkCode < 101) $scope.MarksheetVariable.Grade = 'O+';
                                    //Get Grade 


                                    $scope.MarksheetVariable.PaperTypeIs = "";
                                    for (var typei = 0; typei < ExamDetailJson.length; typei++) {
                                        if ($scope.StudentData[i][strCode].toUpperCase() == ExamDetailJson[typei].paperCode.toUpperCase()) {
                                            if (ExamDetailJson[typei].paperType == 'Practical')
                                            { $scope.MarksheetVariable.PaperTypeIs = 'PR' } else { $scope.MarksheetVariable.PaperTypeIs = 'TH' }
                                        }
                                    }
                                    //$scope.MarksheetVariable.CreditScore = $scope.GetCredits($scope.StudentData[i][strCode]);


                                    //Grade Point
                                    if (strTotalMarkCode >= 90)
                                        $scope.MarksheetVariable.GradePoint = "10";
                                    if (strTotalMarkCode >= 80 && strTotalMarkCode < 90)
                                        $scope.MarksheetVariable.GradePoint = "9." + ((Math.round(strTotalMarkCode)) % 10) + "0";
                                    if (strTotalMarkCode >= 70 && strTotalMarkCode < 80)
                                        $scope.MarksheetVariable.GradePoint = "8." + ((Math.round(strTotalMarkCode)) % 10) + "0";
                                    if (strTotalMarkCode >= 60 && strTotalMarkCode < 70)
                                        $scope.MarksheetVariable.GradePoint = "7." + ((Math.round(strTotalMarkCode)) % 10) + "0";
                                    if (strTotalMarkCode >= 55 && strTotalMarkCode < 60)
                                        $scope.MarksheetVariable.GradePoint = "6." + ((Math.round(strTotalMarkCode)) % 5) * 2 + "0";
                                    if (strTotalMarkCode >= 50 && strTotalMarkCode < 55)
                                        $scope.MarksheetVariable.GradePoint = "5." + (((Math.round(strTotalMarkCode)) % 10) + 5) + "0";
                                    if (strTotalMarkCode >= 45 && strTotalMarkCode < 50)
                                        $scope.MarksheetVariable.GradePoint = "5." + ((Math.round(strTotalMarkCode)) % 5) * 2 + "0";
                                    if (strTotalMarkCode >= 40 && strTotalMarkCode < 45)
                                        $scope.MarksheetVariable.GradePoint = "4." + ((Math.round(strTotalMarkCode)) % 5) * 2 + "0";
                                    if (strTotalMarkCode >= 0 && strTotalMarkCode < 40)
                                        $scope.MarksheetVariable.GradePoint = "0";
                                    //Credit
                                    $scope.MarksheetVariable.Credit = 0;
                                    $scope.MarksheetVariable.TOTALGRADEPOINT = $scope.MarksheetVariable.GradePoint * $scope.MarksheetVariable.CreditScore;
                                    //TotalGradePoint
                                    $scope.StudentData[i].MarkSheetArray.push($scope.MarksheetVariable);
                                    InternalMarkTotal += intInternalMark;
                                    ExternalMarkTotal += intExternalMark;
                                    var TempTotal = Math.round((intInternalMark + intExternalMark))
                                    FinalMarkTotal += Math.round(TempTotal)
                                }
                            }
                        }
                    }
                }
                $scope.StudentData[i].Index = $scope.FinalStudenDetailsWithAllSpe.length;
                $scope.StudentData[i].InternalMarkTotal = InternalMarkTotal;
                $scope.StudentData[i].ExternalMarkTotal = ExternalMarkTotal;
                $scope.StudentData[i].TotalMarks = FinalMarkTotal;

                $scope.StudentData[i].Percentage = parseFloat($scope.StudentData[i].TotalMarks) / parseInt($scope.StudentData[i].MarkSheetArray.length);
                if ($scope.StudentData[i].Percentage < 40) $scope.StudentData[i].Grade = 'F';
                else if ($scope.StudentData[i].Percentage >= 40 && $scope.StudentData[i].Percentage < 44) $scope.StudentData[i].Grade = 'P';
                else if ($scope.StudentData[i].Percentage >= 44 && $scope.StudentData[i].Percentage < 50) $scope.StudentData[i].Grade = 'C';
                else if ($scope.StudentData[i].Percentage >= 50 && $scope.StudentData[i].Percentage < 55) $scope.StudentData[i].Grade = 'B';
                else if ($scope.StudentData[i].Percentage >= 55 && $scope.StudentData[i].Percentage < 60) $scope.StudentData[i].Grade = 'B+';
                else if ($scope.StudentData[i].Percentage >= 60 && $scope.StudentData[i].Percentage < 70) $scope.StudentData[i].Grade = 'A';
                else if ($scope.StudentData[i].Percentage >= 70 && $scope.StudentData[i].Percentage < 80) $scope.StudentData[i].Grade = 'A+';
                else if ($scope.StudentData[i].Percentage >= 80 && $scope.StudentData[i].Percentage < 90) $scope.StudentData[i].Grade = 'O';
                else if ($scope.StudentData[i].Percentage >= 90 && $scope.StudentData[i].Percentage < 101) $scope.StudentData[i].Grade = 'O+';

                var FailedCountArray = $scope.StudentData[i].MarkSheetArray.filter(function (item) { return item.Remark != undefined; });

                
                var total_FailedCredit = 0
                for (var ajj = 0; ajj < $scope.StudentData[i].MarkSheetArray.length; ajj++) {
                    if ($scope.StudentData[i].MarkSheetArray[ajj].Remark != undefined) {
                        total_FailedCredit += parseFloat($scope.StudentData[i].MarkSheetArray[ajj].CreditScore) || 0;
                    }
                }


                if (total_FailedCredit != undefined) {
                    if (total_FailedCredit == 0) {
                        $scope.StudentData[i].Remarks = 'PASS'
                    } else if (total_FailedCredit > 0 && total_FailedCredit < 13) {
                        $scope.StudentData[i].Remarks = 'Passes with ATKT'
                    } else {
                        $scope.StudentData[i].Remarks = 'Fail'
                    }
                }
                if ($scope.StudentData[i].MarkSheetArray.length > 0) {
                    $scope.FinalStudenDetailsWithAllSpe.push($scope.StudentData[i]);
                }
                console.log($scope.FinalStudenDetailsWithAllSpe);
                if ($scope.StudentData.length == (i + 1)) {
                    d.resolve();
                    $scope.$apply();
                }
            }
        });
    }

    var SemesterSub;
    var ExamDetailJson;
    //Retrive data of students.
    GetCsvToJsonData('File/Download/Data/SVT?fileName=' + getUrlVars()["course"] + '-' + getUrlVars()["StudentType"] + '_sem' + getUrlVars()["sem"] + '_' + getUrlVars()["CurrentYear"] + '_' + getUrlVars()["ExamType"] + '.csv').done(function (dataresponse) {
        try {
            var Records = csvJSON(dataresponse);
            var UpdatedSubjectCount = 0;
            CheckStudentSubjectCOunt: for (var rec = 0; rec < Object.keys(Records[0]).length; rec++) {
                var IntCode = 1 + rec;
                var key = 'Code' + IntCode;
                if (Records[0][key] == undefined) {
                    SubjectCount = rec;
                    break CheckStudentSubjectCOunt;
                }
            }
            console.log(Records)
            var DistinctSpecialization = alasql('SELECT DISTINCT Specialisation FROM ?', [Records]);
            console.log(DistinctSpecialization)
            //Creating function array which calls Async
            //var funcs_Array = [];
            var promises = [];
            for (var dist = 0; dist < DistinctSpecialization.length; dist++) {
                if (DistinctSpecialization[dist] != '') {
                    var ResultForCurrentStud = alasql('SELECT * FROM ? WHERE Specialisation=?', [Records, DistinctSpecialization[dist].Specialisation])
                    //$scope.StudentData = ResultForCurrentStud;
                    promises.push(
                      $scope.GenerateStudentSpecializedMarksheet(ResultForCurrentStud)
                    );
                }
                //if ((dist + 1) == DistinctSpecialization.length) {
                //    $scope.$apply();
                //}
            }
            $q.all(promises).then(function (emails) {
                var a = this;
                //this.isEmailValidList = emails.filter(function (e) { return e; });
                $('#div_LoaderMarksheet').css('display', 'none');
            }.bind(this));

        } catch (e) {
            console.log(e)
        }
    });
    //var Records = $.parseJSON(localStorage.getItem("CurrentStudentRecord"));


    $scope.GetTotal = function (val1, val2, val3, val4, val5, val6) {
        var intval1 = parseFloat(val1) || 0;
        var intval2 = parseFloat(val2) || 0;
        var intval3 = parseFloat(val3) || 0;
        var intval4 = parseFloat(val4) || 0;
        var intval5 = parseFloat(val5) || 0;
        var intval6 = parseFloat(val6) || 0;
        return (intval1) + (intval2) + (intval3) + (intval4) + (intval5) + (intval6);
    }
    $scope.ConvertInt = function (val1) {
        return parseFloat(val1) || 0;
    }

    $scope.TotalBasedOnClass = function (classname) {
        var sum = 0;
        $(".GradePoint" + classname).each(function () {
            sum = sum + parseFloat($(this).text());
        });
        return sum;
    }
    $scope.TotalGradePointBasedonClass = function (classname) {
        var sum = 0;
        $(".TotalGradePoint" + classname).each(function () {
            sum = sum + parseFloat($(this).text());
        });
        return sum;
    }
    $scope.SumOfCredit = function (classname) {
        var sum = 0;
        $(".Credit" + classname).each(function () {
            sum = sum + parseFloat($(this).text());
        });
        return sum;
    }
    $scope.SumOfInternalMark = function (classname) {
        var sum = 0;
        $(".InternalMark-" + classname).each(function () {
            var Float_Sum = parseFloat($(this).text()) || 0;
            sum = sum + Float_Sum;
        });
        return sum;
    }
    $scope.SumOfExternalMark = function (classname) {
        var sum = 0;
        $(".ExternalMark" + classname).each(function () {
            sum = sum + parseFloat($(this).text());
        });
        return sum;
    }
    var SumOfTotalMarkGlobalUser = 0;
    $scope.SumOfTotalMark = function (classname) {
        SumOfTotalMarkGlobalUser = 0;
        $(".TotalMark" + classname).each(function () {
            SumOfTotalMarkGlobalUser = SumOfTotalMarkGlobalUser + parseFloat($(this).text());
        });
        return SumOfTotalMarkGlobalUser;
    }

    $scope.GetTotalMarks = function (val1, val2) {
        var intvalue1 = parseInt(val1) || 0;
        var intvalue2 = parseInt(val2) || 0;
        return intvalue1 + intvalue2;
    }

    $scope.RemoveChar = function (val)
    { return val.replace(/'/g, ""); }

    $scope.GetDepartment = function (spe) {
        var strDept = '';
        spe = $.trim(spe.toUpperCase());

        if (spe == 'HOSPITALITY AND TOURISM MANAGEMENT' || spe == 'HOSPITALITY & TOURISM MANAGEMENT' || spe == 'INTERIOR DESIGN & RESOURCE MANAGEMENT' || spe == 'INTERIOR DESIGN AND RESOURCE MANAGEMENT') {
            return 'RESOURCE MANAGEMENT';
        }
        else if (spe == 'DC' || spe == 'DEVELOPMENTAL COUNSELLING' || spe == 'EARLY CHILDHOOD CARE EDUCATION' || spe == 'EARLY CHILDHOOD CARE AND EDUCATION') {
            return 'HUMAN DEVELOPMENT';
        }
        else if (spe == 'MASS COMMUNICATION & EXTENSION' || spe == 'MASS COMMUNICATION AND EXTENSION' || spe == 'TEXTILES & APPAREL DESIGNING' || spe == 'TEXTILES AND APPAREL DESIGNING' || spe == 'FOOD NUTRITION AND DIETETICS' || spe == 'FOOD NUTRITION & DIETETICS') {
            return spe;
        }
        return strDept;
    }


    $scope.GetPaperTitle = function (code, specialization) {
        for (i = 0; i < SemesterSub.length; i++) {
            if (SemesterSub[i].specialisation.toUpperCase() == specialization.toUpperCase()) {
                //alert(i)
                for (j = 0; j < SemesterSub[i].paperDetails.length; j++) {
                    if (code == SemesterSub[i].paperDetails[j].code) {
                        return SemesterSub[i].paperDetails[j].paperTitle;
                    }
                }
            }
        }
    }
    $scope.GetpaperType = function (subjctCode) {
        for (i = 0; i < ExamDetailJson.length; i++) {
            if (subjctCode.toUpperCase() == ExamDetailJson[i].paperCode.toUpperCase()) {
                if (ExamDetailJson[i].paperType == 'Practical')
                { return 'PR' } else { return 'TH' }
            }
        }
    }

    $scope.ChecktheoryInternalMax = function (subjctCode) {
        for (i = 0; i < ExamDetailJson.length; i++) {
            if (subjctCode.toUpperCase() == ExamDetailJson[i].paperCode.toUpperCase()) {
                if (ExamDetailJson[i].theoryInternalMax != '0') {
                    return 1;
                } else {
                    return 0;
                }
            }
        }
    }

    $scope.ChecktheoryExternalMax = function (subjctCode) {
        for (i = 0; i < ExamDetailJson.length; i++) {
            if (subjctCode.toUpperCase() == ExamDetailJson[i].paperCode.toUpperCase()) {
                if (ExamDetailJson[i].theoryExternalSection1Max != '0' || theoryExternalSection2Max != '0') {
                    return 1;
                } else {
                    return 0;
                }
            }
        }
    }

    $scope.TotalGradeCounter = function (Index) {
        var TotalMark = parseInt($scope.SumOfTotalMark(Index)) / parseInt($('#SubjectCount').val());
        if (TotalMark < 40) return 'F';
        else if (TotalMark >= 40 && TotalMark < 44) return 'P'
        else if (TotalMark >= 44 && TotalMark < 50) return 'C';
        else if (TotalMark >= 50 && TotalMark < 55) return 'B';
        else if (TotalMark >= 55 && TotalMark < 60) return 'B+';
        else if (TotalMark >= 60 && TotalMark < 70) return 'A';
        else if (TotalMark >= 70 && TotalMark < 80) return 'A+';
        else if (TotalMark >= 80 && TotalMark < 90) return 'O';
        else if (TotalMark >= 90 && TotalMark < 101) return 'O+';
    }
    $scope.FinalGradePoint = function (Index) {
        return $scope.TotalGradePointBasedonClass(Index) / $scope.SumOfCredit(Index);
    }
    $scope.GetPercentageOfStudent = function (Index) {
        return parseInt($scope.SumOfTotalMark(Index)) / parseInt($('#SubjectCount').val());
    }
    $scope.GetGrade = function (val1, val2) {
        var InternalCN = parseInt(val1) || 0;
        var ExternalTotalCN = parseInt(val2) || 0;
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

    $scope.TotalGradePoint = function (GradePoint, credit) {
        var GradePointCN = parseFloat(GradePoint) || 0;
        var creditCN = parseFloat(credit) || 0;
        return GradePointCN * creditCN;
    }
    $scope.GetGradePoint = function (val1, val2) {
        var InternalCN = parseInt(val1) || 0;
        var ExternalTotalCN = parseInt(val2) || 0;
        var marks = InternalCN + ExternalTotalCN;
        if (marks >= 90)
            return "10";
        if (marks >= 80 && marks < 90)
            return "9." + (marks % 10) + "0";
        if (marks >= 70 && marks < 80)
            return "8." + (marks % 10) + "0";
        if (marks >= 60 && marks < 70)
            return "7." + (marks % 10) + "0";
        if (marks >= 55 && marks < 60)
            return "6." + (marks % 5) * 2 + "0";
        if (marks >= 50 && marks < 55)
            return "5." + ((marks % 10) + 5) + "0";
        if (marks >= 45 && marks < 50)
            return "5." + (marks % 5) * 2 + "0";
        if (marks >= 40 && marks < 45)
            return "4." + (marks % 5) * 2 + "0";
        if (marks >= 0 && marks < 40)
            return "0";
    }

    var month_name = function (dt) {
        mlist = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        return mlist[dt.getMonth()];
    };

    $scope.GetYearLogicString = function (year) {
        var lastnumber = year.toString().substr(-2);
        var intyear = parseFloat(year) || 0;
        return intyear - 1 + '-' + lastnumber;
    }
    $scope.GetWordFromNumber = function (amount) {
        var words = new Array();
        words[0] = '';
        words[1] = 'One';
        words[2] = 'Two';
        words[3] = 'Three';
        words[4] = 'Four';
        words[5] = 'Five';
        words[6] = 'Six';
        words[7] = 'Seven';
        words[8] = 'Eight';
        words[9] = 'Nine';
        words[10] = 'Ten';
        words[11] = 'Eleven';
        words[12] = 'Twelve';
        words[13] = 'Thirteen';
        words[14] = 'Fourteen';
        words[15] = 'Fifteen';
        words[16] = 'Sixteen';
        words[17] = 'Seventeen';
        words[18] = 'Eighteen';
        words[19] = 'Nineteen';
        words[20] = 'Twenty';
        words[30] = 'Thirty';
        words[40] = 'Forty';
        words[50] = 'Fifty';
        words[60] = 'Sixty';
        words[70] = 'Seventy';
        words[80] = 'Eighty';
        words[90] = 'Ninety';
        amount = amount.toString();
        var atemp = amount.split(".");
        var number = atemp[0].split(",").join("");
        var n_length = number.length;
        var words_string = "";
        if (n_length <= 9) {
            var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
            var received_n_array = new Array();
            for (var i = 0; i < n_length; i++) {
                received_n_array[i] = number.substr(i, 1);
            }
            for (var i = 9 - n_length, j = 0; i < 9; i++, j++) {
                n_array[i] = received_n_array[j];
            }
            for (var i = 0, j = 1; i < 9; i++, j++) {
                if (i == 0 || i == 2 || i == 4 || i == 7) {
                    if (n_array[i] == 1) {
                        n_array[j] = 10 + parseInt(n_array[j]);
                        n_array[i] = 0;
                    }
                }
            }
            value = "";
            for (var i = 0; i < 9; i++) {
                if (i == 0 || i == 2 || i == 4 || i == 7) {
                    value = n_array[i] * 10;
                } else {
                    value = n_array[i];
                }
                if (value != 0) {
                    words_string += words[value] + " ";
                }
                if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                    words_string += "Crores ";
                }
                if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                    words_string += "Lakhs ";
                }
                if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                    words_string += "Thousand ";
                }
                if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                    words_string += "Hundred and ";
                } else if (i == 6 && value != 0) {
                    words_string += "Hundred ";
                }
            }
            words_string = words_string.split("  ").join(" ");
        }
        return words_string;

    }

    $scope.GetGradeSVT = function (Marks) {
        var Grade = 0;
        if (Marks > 89) return 10.00
        else if (Marks == 89) return 9.90
        else if (Marks == 88) return 9.80
        else if (Marks == 87) return 9.70
        else if (Marks == 86) return 9.60
        else if (Marks == 85) return 9.50
        else if (Marks == 84) return 9.40
        else if (Marks == 83) return 9.30
        else if (Marks == 82) return 9.20
        else if (Marks == 81) return 9.10
        else if (Marks == 80) return 9.0
        else if (Marks == 79) return 8.90
        else if (Marks == 78) return 8.80
        else if (Marks == 77) return 8.70
        else if (Marks == 76) return 8.60
        else if (Marks == 75) return 8.50
        else if (Marks == 74) return 8.40
        else if (Marks == 73) return 8.30
        else if (Marks == 72) return 8.20
        else if (Marks == 71) return 8.10
        else if (Marks == 70) return 8.00
        else if (Marks == 69) return 7.90
        else if (Marks == 68) return 7.80
        else if (Marks == 67) return 7.70
        else if (Marks == 66) return 7.60
        else if (Marks == 65) return 7.50
        else if (Marks == 64) return 7.40
        else if (Marks == 63) return 7.30
        else if (Marks == 62) return 7.20
        else if (Marks == 61) return 7.10
        else if (Marks == 60) return 7.00
        else if (Marks == 59) return 6.80
        else if (Marks == 58) return 6.60
        else if (Marks == 57) return 6.40
        else if (Marks == 56) return 6.20
        else if (Marks == 55) return 6.00
        else if (Marks == 54) return 5.90
        else if (Marks == 53) return 5.80
        else if (Marks == 52) return 5.70
        else if (Marks == 51) return 5.60
        else if (Marks == 50) return 5.50
        else if (Marks == 49) return 5.40
        else if (Marks == 48) return 5.30
        else if (Marks == 47) return 5.20
        else if (Marks == 46) return 5.10
        else if (Marks == 45) return 5.00
        else if (Marks == 44) return 4.80
        else if (Marks == 43) return 4.60
        else if (Marks == 42) return 4.40
        else if (Marks == 41) return 4.20
        else if (Marks == 40) return 4.00
        else return 0.00;
    }
});