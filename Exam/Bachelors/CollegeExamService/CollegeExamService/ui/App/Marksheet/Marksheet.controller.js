Examapp.controller('MarksheetController', function ($scope, PaperService, StudentCsvFile, MarksheetService) {
    //common function
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


    //Read property from url
    var course = getUrlVars()["course"]
    var StudentType = getUrlVars()["StudentType"]
    var sem = getUrlVars()["sem"]
    var CurrentYear = getUrlVars()["CurrentYear"]
    var ExamType = getUrlVars()["ExamType"]
	
	$scope.StudentType = getUrlVars()["StudentType"]
	$scope.ExamType = getUrlVars()["ExamType"]
    $scope.date = getUrlVars()["date"]
    $scope.IsLoadingCompleted = false;
    if (StudentType.toLowerCase() == 'elective') {
        $scope.FinalResultDisplay = false;
        StudentType = 'honors'
    } else {
        $scope.FinalResultDisplay = true;
    }

	var spec = getUrlVars()["StudentType"] == "elective" ? "Honors" : getUrlVars()["StudentType"];
    CallAPI("User/GetPaperList?Course=" + course + "&specialization=" + spec + "&sem=" + sem).done(function (response) {
        var CurrentSubjectRecord = response;
        StudentCsvFile.GetFileObj(course, getUrlVars()["StudentType"], sem, CurrentYear, ExamType).done(function (dataresponse) {
            try {
                var StudentDetail_Obj = {};
                StudentDetail_Obj.Course = course;
                StudentDetail_Obj.ExamType = ExamType;
                StudentDetail_Obj.Semester = sem;
                StudentDetail_Obj.RegularORHonors = StudentType;
                var MainObject = [];
                var StudentData = csvJSON(dataresponse);
                var StudentCurrentData = StudentData;
                var StudentMarksheetData = [];
                for (let aj = 0; aj < StudentCurrentData.length; aj++) {
                    try {
                        throw aj;
                    } catch (ii) {
                        if (StudentCurrentData[aj].College_Registration_No_ == '5977') {
                            debugger;
                        }
                        var SpecializationObj = PaperService.GetSpecilizationObj(CurrentSubjectRecord, StudentCurrentData[ii]['Specialisation']);
                        var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(StudentCurrentData[ii], SpecializationObj, StudentDetail_Obj);
                        StudentMarksheetData.push(MarksheetObjFinal);
                    }
                    if (aj == (StudentCurrentData.length - 1)) {
                        $scope.MarksheetData = StudentMarksheetData;
                        $scope.IsLoadingCompleted = true;
                        $('.DivResult').css('display', 'block');
                        if (!$scope.$$phase) {
                            $scope.$apply();
                        }
                    }
                }
            } catch (e) {
                console.log(e)
            }
        });
    });
});