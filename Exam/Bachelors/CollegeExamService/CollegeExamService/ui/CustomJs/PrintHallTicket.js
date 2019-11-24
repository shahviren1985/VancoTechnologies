var ExamSystem = angular.module("HallTicketApp", []);
ExamSystem.controller("HallTicketCtrl", function ($scope) {
    var months = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    var Course = getUrlVars()["course"];
    var specialization = getUrlVars()["specialization"];
    $scope.specializationToShow = getUrlVars()["specialization"];
    var sem = getUrlVars()["sem"];
    var rollNumber = getUrlVars()["rollNumber"];
    $scope.SemData = getUrlVars()["sem"];
    var examType = getUrlVars()["examType"];
    var year = getUrlVars()["year"];
    $scope.UearToShow = getUrlVars()["year"];
    $scope.StudentData = [];
    $scope.SubjectData = [];
    $scope.CurrentMonth = months[new Date().getMonth()];
    CallAPI('User/GetPaperList?Course=' + Course
        + '&specialization=' + specialization + '&sem=' + sem + '', 'GET', '').done(function (Subject) {
            $scope.SubjectData = Subject;
            console.log(Subject);
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + Course + '-' + specialization + '_sem' + sem
                + '_' + year + '_' + examType + '.csv').done(function (dataresponse) {
                    $scope.StudentData = csvJSON(dataresponse);
                    console.log($scope.StudentData)
                    for (a = 0; a < $scope.StudentData.length; a++) {

                        if ($scope.StudentData[a].RollNumber != rollNumber)
                            continue;

                        $scope.StudentData[a].paperDetails = [];
                        for (b = 0; b < $scope.SubjectData.length; b++) {
                            if ($scope.StudentData[a].Specialisation != undefined && ($scope.StudentData[a].Specialisation.toLowerCase() == $scope.SubjectData[b].specialisation.toLowerCase() || $scope.StudentData[a].Specialisation.toLowerCase() == $scope.SubjectData[b].specialisationCode.toLowerCase())) {
                                for (var propcount = 0; propcount < Object.keys($scope.StudentData[a]).length; propcount++) {
                                    var strCode = "Code" + (propcount + 1);
                                    if ($scope.StudentData[a][strCode] == undefined) {
                                        break;
                                    } else {
                                        var index = $scope.SubjectData[b].paperDetails.findIndex(x => x.code.toLowerCase() == $scope.StudentData[a][strCode].toLowerCase());
                                        if (index > -1 && $scope.SubjectData[b].paperDetails[index].paperType.toLowerCase() == 'theory') {
                                            var StudentPaper = {};
                                            //StudentPaper.PaperCode = $scope.StudentData[a][strCode]
                                            StudentPaper.PaperCode = $scope.SubjectData[b].paperDetails[index].code;
                                            StudentPaper.PaperName = $scope.SubjectData[b].paperDetails[index].paperTitle;
                                            StudentPaper.ExamStatus = $scope.StudentData[a]["InternalC" + (propcount + 1)] >= 20 ? "P" : "NP";

                                            if ($scope.StudentData[a].paperDetails.filter(e => e.PaperCode === StudentPaper.PaperCode).length > 0) {
                                                /* vendors contains the element we're looking for */
                                                console.log('duplicate')
                                            } else {
                                                if ($scope.SubjectData[b].paperDetails[index].isContinousAssessment != true) {
                                                    $scope.StudentData[a].paperDetails.push(StudentPaper);
                                                }
                                            }
                                        }
                                    }
                                    console.log($scope.StudentData[a])
                                }
                            }
                        }

                        // Add Value Added Courses
                        if (sem > 2) {
                            var pap = {};
                            pap.PaperCode = "";
                            pap.PaperName = "Value Added Paper";
                            pap.ExamStatus = "";
                            $scope.StudentData[a].paperDetails.push(pap);
                        }

                        $scope.StudentData2 = [];
                        $scope.StudentData2.push($scope.StudentData[a]);
                        $scope.$digest();
                        break;
                    }
                })
        })
    $scope.GetClassName = function (classname, sem) {
        if ((classname.toLowerCase() == 'fy b.sc. in home science') && (sem.toString() == '1' || sem.toString() == '2')) {
            return 'F.Y.B.Sc.(Home Sc.)';
        } else if ((classname.toLowerCase() == 'sy b.sc. in home science') && (sem.toString() == '3' || sem.toString() == '4')) {
            return 'S.Y.B.Sc.(Home Sc.)';
        } else if ((classname.toLowerCase() == 'sy b.sc. in home science (honors)') && (sem.toString() == '3' || sem.toString() == '4')) {
            return 'S. Y. B.Sc.(Home Sci.) Honors';
        } else if ((classname.toLowerCase() == 'ty b.sc. in home science') && (sem.toString() == '5' || sem.toString() == '6')) {
            return 'T. Y. B.Sc.(Home Sci.)';
        } else if ((classname.toLowerCase() == 'ty b.sc. in home science (honors)') && (sem.toString() == '5' || sem.toString() == '6')) {
            return 'T. Y. B.Sc.(Home Sci.) Honors';
        }
    }
    $scope.GetSpecializationFullName = function (spec) {
        var index = $scope.SubjectData.findIndex(x => x.specialisationCode.toLowerCase() == spec.toLowerCase());
        if (index == -1) {
            console.log(spec)
        } else {
            return $scope.SubjectData[index].specialisation
        }

    }
});

ExamSystem.filter('AdjustSize', function () {
    return function (x) {
        if (x > 9 || x == 9) {
            return '7%';
        } else if (x == 8) {
            return '10%';
        } else if (x == 7) {
            return '12%';
        } else if (x == 6) {
            return '17%';
        }
    };
});

ExamSystem.filter('GetClassName', function () {
    return function (x) {
        if (x.toString().toLowerCase() == 'BSc - Regular 1' || x.toString() == 'BSc - Regular 2') {
            return 'F.Y.B.Sc.(Home Sc.)'
        } else if (x.toString() == 'BSc - Regular 3' || x.toString() == 'BSc - Regular 4') {
            return 'S.Y.B.Sc.(Home Sc.)'
        }
    };
});

