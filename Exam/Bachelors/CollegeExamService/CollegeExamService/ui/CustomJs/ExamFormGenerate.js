var ExamSystem = angular.module("ExamFormGenerateApp", []);
ExamSystem.controller("ExamFormGenerateCtrl", function ($scope) {
    var Course = getUrlVars()["course"];
    var specialization = getUrlVars()["specialization"];
    var sem = getUrlVars()["sem"];
    var examType = getUrlVars()["examType"];
    var year = getUrlVars()["year"];
    $scope.StudentData = [];
    $scope.SubjectData = []
    CallAPI('User/GetPaperList?Course=' + Course
        + '&specialization=' + specialization + '&sem=' + sem + '', 'GET', '').done(function (Subject) {
            $scope.SubjectData = Subject;
            console.log(Subject);
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + Course + '-' + examType + '_sem' + sem
                + '_' + year + '_' + examType + '.csv').done(function (dataresponse) {
                    $scope.StudentData = csvJSON(dataresponse);

                    for (a = 0; a < $scope.StudentData.length; a++) {
                        $scope.StudentData[a].paperDetails = [];
                        for (b = 0; b < $scope.SubjectData.length; b++) {
                            if ($scope.StudentData[a].Specialisation != undefined && ($scope.StudentData[a].Specialisation.toLowerCase() == $scope.SubjectData[b].specialisation.toLowerCase() || $scope.StudentData[a].Specialisation.toLowerCase() == $scope.SubjectData[b].specialisationCode.toLowerCase())) {
                                for (c = 1; c < 10; c++) {
                                    var strCode = 'Code' + c;
                                    if ($scope.StudentData[a][strCode] != undefined) {
                                        for (d = 0; d < $scope.SubjectData[b].paperDetails.length; d++) {
                                            if ($scope.StudentData[a][strCode].toLowerCase() == $scope.SubjectData[b].paperDetails[d].code.toLowerCase()) {
                                                var StudentPaper = {};
                                                StudentPaper.PaperCode = $scope.StudentData[a][strCode]
                                                StudentPaper.PaperName = $scope.SubjectData[b].paperDetails[d].paperTitle;
                                                $scope.StudentData[a].paperDetails.push(StudentPaper);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if ($scope.StudentData.length == (a + 1)) {
                            $scope.StudentData = SortArrayWithSeatNumber($scope.StudentData, 'SeatNumber')
                            $scope.$digest();
                        }
                    }
                })
        })
});