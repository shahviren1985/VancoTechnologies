
Examapp.controller("GenerateTranscriptCertificateCtrl", function ($scope, $http, commonService, MarksheetService) {
    var prn = getUrlVars()['CollegeRegistrationNumber'];
    var sem1 = getUrlVars()['AdmissionYear'];
    var sem2 = parseInt(sem1) + 1;
    var sem3 = sem2;
    var sem4 = parseInt(sem1) + 2;
    var sem5 = sem4;
    var sem6 = parseInt(sem1) + 3;

    var Validation = false;
    if (prn == undefined || prn == null || prn == '') {
        alert('Invalid College Registration Number, Please try again!');
        return;
    } else {
        Validation = true;
    }
    $scope.IsLoadingCompleted = false;
    $scope.DateOfToday = commonService.GetTodayDate(2);
    $scope.InwardNumber = getUrlVars()['InwardNumber'];
    var y = commonService.GetCurrentAcademicYear();
    y = y.split("-")[0] + "-" + y.split("-")[1].substring(2);

    $scope.CurrentAcademicYear = y;
    function ConvertName(string) {
        return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
    }

    function sortByKey(arr, key) {
        return arr.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }

    var getExamData = function () {
        $http.get(_CommonUr + 'exam/GetStudentsTranscript?CollegeRegistrationNumber=' + prn + '&semester=4')
            .then(function (response) {
                console.log(response);
                if (response.status == 200) {
                    $scope.TranscriptExamData = { "Semester1": sem1, "Semester2": sem2, "Semester3": sem3, "Semester4": sem4, "Semester5": sem5, "Semester6": sem6 };
                    $scope.TranscriptData = response.data.cm;
                    $scope.TranscriptData.LastName = ConvertName($scope.TranscriptData.LastName);
                    $scope.TranscriptData.FirstName = ConvertName($scope.TranscriptData.FirstName);
                    $scope.TranscriptData.FatherName = ConvertName($scope.TranscriptData.FatherName);
                    $scope.TranscriptData.MotherName = ConvertName($scope.TranscriptData.MotherName);

                    $scope.TranscriptData.DepartmentName = MarksheetService.GetDepartment($scope.TranscriptData.Specialisation);

                    $scope.TranscriptData.Sem1 = {};
                    $scope.TranscriptData.Sem2 = {};
                    $scope.TranscriptData.Sem3 = {};
                    $scope.TranscriptData.Sem4 = {};
                    

                    $scope.TranscriptData.Sem1.Papers = response.data.ExamTable.filter(x => x.semester == 1);
                    $scope.TranscriptData.Sem2.Papers = response.data.ExamTable.filter(x => x.semester == 2);
                    $scope.TranscriptData.Sem3.Papers = response.data.ExamTable.filter(x => x.semester == 3);
                    $scope.TranscriptData.Sem4.Papers = response.data.ExamTable.filter(x => x.semester == 4);
                    
                    $scope.TranscriptData = ProcessObjArrayToResult($scope.TranscriptData);
                    console.log($scope.TranscriptData);
                    $scope.IsLoadingCompleted = true;
                } else {
                    alert(response.data);
                    $('#ImgLoader').css('display', 'none')
                    $('#marksheetStatusText').html(response.data)
                }
            }).catch(function (error) {
                if (typeof error.data === 'string') {
                    alert(error.data);
                    $('#ImgLoader').css('display', 'none')
                    $('#marksheetStatusText').html(error.data)
                } else {
                    console.log(error)
                    $('#ImgLoader').css('display', 'none')
                    alert('Error while retriving data');
                }
            })
    }
    getExamData();

    var ProcessObjArrayToResult = function (transcriptData) {
        for (var pa = 1; pa < 5; pa++) {
            if (pa == 1)
                transcriptData.Sem1 = angular.copy(ProcessObjArrayToResultSem2(transcriptData.Sem1, pa, transcriptData.AdmissionYear));
            if (pa == 2)
                transcriptData.Sem2 = angular.copy(ProcessObjArrayToResultSem2(transcriptData.Sem2, pa, transcriptData.AdmissionYear));
            if (pa == 3)
                transcriptData.Sem3 = angular.copy(ProcessObjArrayToResultSem2(transcriptData.Sem3, pa, transcriptData.AdmissionYear));
            if (pa == 4)
                transcriptData.Sem4 = angular.copy(ProcessObjArrayToResultSem2(transcriptData.Sem4, pa, transcriptData.AdmissionYear));
        }
        transcriptData.TotalOverAllPercentage = (TotalWeightedMarksObtained / TotalCredits).toFixed(2);
        transcriptData.OverAllGrade = MarksheetService.GetGrade(transcriptData.TotalOverAllPercentage);
        transcriptData.OverAllGradePOint = TotalOverAllGradePoint / 4;
        return transcriptData;
    }

    var TotalOverAllPercentage = 0;
    var TotalCredits = 0;
    var TotalWeightedMarksObtained = 0;
    var TotalOverAllGradePoint = 0;
    var ProcessObjArrayToResultSem2 = function (transcriptData, semester, admissionYear) {
        if (transcriptData.Papers.length > 0) {
            transcriptData.Papers = sortByKey(transcriptData.Papers, "papercode");

            var objToConsider = angular.copy(transcriptData);
            var totalPercentage = 0;
            var totalGradePoint = 0;
            var weightedPercent = 0;
            var totalCredit = 0;
            var hasSportsNss = false;
            for (var p = 0; p < objToConsider.Papers.length; p++) {
                if (objToConsider.Papers[p].papertitle.toLowerCase().indexOf("incentive") > -1) {
                    hasSportsNss = true;
                }

                objToConsider.Papers[p]._TotalMaxMark = $scope.GetCountTotalMark(objToConsider.Papers[p]);
                objToConsider.Papers[p]._TotalObtainedMark = $scope.GetCountObtainedTotalMark(objToConsider.Papers[p]);
                objToConsider.Papers[p]._percentage = ((objToConsider.Papers[p]._TotalObtainedMark * 100) / objToConsider.Papers[p]._TotalMaxMark);
                objToConsider.Papers[p]._Grade = MarksheetService.GetGradePoint(objToConsider.Papers[p]._TotalObtainedMark);

                objToConsider.Papers[p]._GradePoint = (objToConsider.Papers[p]._Grade * (parseFloat(objToConsider.Papers[p].credit)));
                if (objToConsider.Papers[p]._TotalMaxMark != 10) {
                    totalPercentage += objToConsider.Papers[p]._percentage;
                } else {
                    totalPercentage += objToConsider.Papers[p]._TotalMaxMark;
                }

                
                totalGradePoint += objToConsider.Papers[p]._GradePoint;
                var fltCredit = parseFloat(objToConsider.Papers[p].credit) || 0;
                totalCredit += fltCredit;
                TotalCredits += fltCredit;
                TotalWeightedMarksObtained += (objToConsider.Papers[p]._TotalObtainedMark * objToConsider.Papers[p].credit);
            }

            if (hasSportsNss && semester == 6) {
                objToConsider._percentege = totalPercentage / (objToConsider.Papers.length - 1);
            } else {
                objToConsider._percentege = totalPercentage / objToConsider.Papers.length;
            }

            var intperc = parseFloat(objToConsider._percentege) || 0;
            //TotalOverAllPercentage += intperc;
            //TotalOverAllPercentage = (weightedPercent / totalCredit).toFixed(2);
            objToConsider._percentegeGrade = MarksheetService.GetGrade(objToConsider._percentege);
            objToConsider._FinalPercentageGradePoint = totalGradePoint / totalCredit;

            // This line is added to support sem 1/2/3 GP issues for 2016 batch of students. 
            /*if (semester < 4 && admissionYear == "2017") {
                if (objToConsider._FinalPercentageGradePoint.toString().indexOf(".") > 0) {
                    objToConsider._FinalPercentageGradePoint = parseFloat(objToConsider._FinalPercentageGradePoint.toString().substring(0,objToConsider._FinalPercentageGradePoint.toString().indexOf(".")));
                }
            }*/

            TotalOverAllGradePoint += objToConsider._FinalPercentageGradePoint;
            return objToConsider;
        } else {
            alert('No subject found for the semester ' + semester);
            return;
        }
    }

    $scope.GetCountTotalMark = function (array) {
        var count = 0;
        var externalmaxmarks = parseFloat(array.externalmaxmarks) || 0;
        var internaltotalmarks = parseFloat(array.internaltotalmarks) || 0;
        var practicalmaxmarks = parseFloat(array.practicalmaxmarks) || 0;
        return (externalmaxmarks + internaltotalmarks + practicalmaxmarks);
    }

    $scope.GetCountObtainedTotalMark = function (array) {
        var count = 0;
        var externaltotalmarks = (parseFloat(array.externaltotalmarks) + parseFloat(array.gracemarks)) || 0;
        var internalmarksobtained = Math.round(parseFloat(array.internalmarksobtained) || 0);
        var practicalmarksobtained = parseFloat(array.practicalmarksobtained) || 0;

        if (internalmarksobtained % 1 == 0 && externaltotalmarks % 1 != 0) {
            externaltotalmarks = Math.round(externaltotalmarks);
        }
        else if (externaltotalmarks % 1 != 0) {
            externaltotalmarks = Math.floor(externaltotalmarks);
        }
        else if (practicalmarksobtained % 1 != 0) {
            practicalmarksobtained = Math.floor(practicalmarksobtained);
        }

        return Math.round(externaltotalmarks + internalmarksobtained + practicalmarksobtained);
    }

    var getPercentageValue = function (ArrayObj) {
        var Total = 0;
        for (var i = 0; i < ArrayObj.length; i++) {

        }
    }
})