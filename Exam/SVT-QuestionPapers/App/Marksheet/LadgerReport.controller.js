Examapp.controller('LadgerReportController', function ($scope, PaperService, StudentCsvFile, MarksheetService) {
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
    $scope.ExamType = getUrlVars()["ExamType"]
	$scope.date = getUrlVars()["date"]
    $scope.IsLoadingCompleted = false;
    if (StudentType.toLowerCase() == 'elective') {
        $scope.FinalResultDisplay = false;
        StudentType = 'honors'
    } else {
        $scope.FinalResultDisplay = true;
    }

    $scope.comparestringToIntValue = function (value) {
        return parseFloat(value) || 0;
    }

    CallAPI("User/GetPaperList?Course=" + course + "&specialization=" + getUrlVars()["StudentType"] + "&sem=" + sem).done(function (response) {
        var CurrentSubjectRecord = response;
        StudentCsvFile.GetFileObj(course, getUrlVars()["StudentType"], sem, CurrentYear, ExamType).done(function (dataresponse) {
            StudentCsvFile.GetFileObj(course, StudentType, sem, CurrentYear, ExamType).done(function (dataresponse) {
                try {
                    var StudentDetail_Obj = {};
                    StudentDetail_Obj.Course = course;
                    StudentDetail_Obj.ExamType = ExamType;
                    StudentDetail_Obj.Semester = sem;
                    StudentDetail_Obj.RegularORHonors = StudentType;
                    var MainObject = [];
                    var StudentData = csvJSON(dataresponse);

                    for (var speci = 0; speci < CurrentSubjectRecord.length; speci++) {
                        //for (var speci = 0; speci < 1; speci++) {
                        var StudentCurrentData = StudentData.filter(x => x.Specialisation == CurrentSubjectRecord[speci].specialisationCode);
                        var StudentSubObj = {};
                        StudentSubObj.Specilization = CurrentSubjectRecord[speci];
						 
								var nss = -1;
								var sports = -1;
						for( var i = 0; i < StudentSubObj.Specilization.paperDetails.length; i++){ 
								   if(StudentSubObj.Specilization.paperDetails[i].paperTitle === "Incentive Marks for N.S.S. Activities")
										nss = i;
									if(StudentSubObj.Specilization.paperDetails[i].paperTitle === "Incentive Marks for Sports Activities") 
										sports = i;
						}
						
                        if(nss != -1)
							StudentSubObj.Specilization.paperDetails.splice(nss, 1); 

                        if (sports != -1)
						    StudentSubObj.Specilization.paperDetails.splice(sports-1, 1); 
						
                        StudentSubObj.Specilization.paperDetails = MarksheetService.sortArray(StudentSubObj.Specilization.paperDetails, 'code', 1);
                        StudentSubObj.StudentData = [];
                        for (let aj = 0; aj < StudentCurrentData.length; aj++) {
                            try {
                                throw aj;
                            } catch (ii) {
                                var SpecializationObj = PaperService.GetSpecilizationObj(CurrentSubjectRecord, StudentCurrentData[ii]['Specialisation']);
                                var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(StudentCurrentData[ii], SpecializationObj, StudentDetail_Obj);
                                MarksheetObjFinal.SubjectDetail = MarksheetService.AddMissingSubject(StudentSubObj.Specilization.paperDetails, MarksheetObjFinal.SubjectDetail);
                                
								 nss = -1;
								 sports = -1;
								
								for( var i = 0; i < MarksheetObjFinal.SubjectDetail.length; i++){ 
								   if(MarksheetObjFinal.SubjectDetail[i].paperTitle === "Incentive Marks for N.S.S. Activities")
										nss = i;
									if(MarksheetObjFinal.SubjectDetail[i].paperTitle === "Incentive Marks for Sports Activities") 
										sports = i;
								}
								
								if(nss != -1)
                                    MarksheetObjFinal.SubjectDetail.splice(nss, 1); 

								if(sports != -1)
									MarksheetObjFinal.SubjectDetail.splice(sports-1, 1); 
								
								MarksheetObjFinal.SubjectDetail = MarksheetService.sortArray(MarksheetObjFinal.SubjectDetail, 'code', 1);
                                StudentSubObj.StudentData.push(MarksheetObjFinal);
                            }
                            if (aj == (StudentCurrentData.length - 1)) {
                                MainObject.push(StudentSubObj)
                            }
                        }
                        if (speci == (CurrentSubjectRecord.length - 1)) {
                            $scope.FinalObject = MainObject;
                            if (!$scope.$$phase) {
                                $scope.$apply();
                            }
                            setTimeout(function () {
                                $scope.IsLoadingCompleted = true;
                                $('#div_LoaderMarksheet').css('display', 'none')
                                $('.AddPageBreak').css('display', '')
                            }, 500)

                        }
                    }
                } catch (e) {
                    console.log(e)
                }
            });
        });
    });
});