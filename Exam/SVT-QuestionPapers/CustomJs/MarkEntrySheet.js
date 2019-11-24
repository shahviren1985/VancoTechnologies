var ExamSystem = angular.module("EnteredSheet", []);
ExamSystem.controller("MarkEntryCtrl", function ($scope) {
    $scope.stream = getUrlVars()["stream"];
    $scope.Course = getUrlVars()["course"];
    $scope.specialization = getUrlVars()["Specialization"];
    $scope.sem = getUrlVars()["sem"];
    $scope.examType = getUrlVars()["examType"];
    $scope.year = getUrlVars()["year"];
    $scope.UearToShow = getUrlVars()["year"];
    $scope.paper = getUrlVars()["paper"];
    var fucheck = getUrlVars()["IsInternal"];
    $scope.IsInternal=parseInt(fucheck)||0;
    $scope.PaperName=decodeURI(getUrlVars()["PaperName"]);
    $scope.StudentData = [];

    function findWithAttr(array, attr, value) {
        for (var i = 0; i < array.length; i += 1) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
    $scope.MarkEntryArray = [];
    GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.stream + '-' + $scope.Course + '_sem' + $scope.sem
     + '_' + $scope.year + '_' + $scope.examType + '.csv').done(function (dataresponse) {
         $scope.StudentData = csvJSON(dataresponse);
         console.log($scope.StudentData)
         
         for (var i = 0; i < $scope.StudentData.length; i++) {
             if ($scope.StudentData[i].Specialisation != undefined && $scope.StudentData[i].Specialisation != '' && $scope.StudentData[i].Specialisation.toLowerCase() == $scope.specialization.toLowerCase()) {
                 var Key = getKeyByValue($scope.StudentData[i], $scope.paper);
                 if (Key == undefined) {
                     continue;
                 }
                 var lastChar = Key[Key.length - 1];
                 var Number = parseInt(lastChar);
                 //var Number=1;
                 var internalmarkKey = 'InternalC' + Number;
                 var ExternalMark1Key = 'ExternalSection1C' + Number;
                 var ExternalMark2Key = 'ExternalSection2C' + Number;
                 var PracticalMarkKey = 'PracticalMarksC' + Number;
                 var GraceKey = 'GraceC' + Number;
                 $scope.CurrentStudent = {};
                 $scope.CurrentStudent.FullNameOfStudent = $scope.StudentData[i].LastName + ' ' + $scope.StudentData[i].FirstName + ' ' + $scope.StudentData[i].FatherName + ' ' + $scope.StudentData[i].MotherName;
                 $scope.CurrentStudent.SeatNumber = $scope.StudentData[i].SeatNumber;
                 $scope.CurrentStudent.RollNumber = $scope.StudentData[i].RollNumber;
                 $scope.CurrentStudent.InternalMark = $scope.StudentData[i][internalmarkKey];
                 $scope.CurrentStudent.ExternalMark1 = $scope.StudentData[i][ExternalMark1Key];
                 $scope.CurrentStudent.ExternalMark2 = $scope.StudentData[i][ExternalMark2Key];
                 $scope.CurrentStudent.PracticalMark = $scope.StudentData[i][PracticalMarkKey];
                 $scope.CurrentStudent.Int1=parseFloat($scope.CurrentStudent.ExternalMark1) || 0;
                 $scope.CurrentStudent.Int2=parseFloat($scope.CurrentStudent.ExternalMark2) || 0;
                 $scope.CurrentStudent.Int3=parseFloat($scope.CurrentStudent.PracticalMark) || 0;
                 $scope.CurrentStudent.TotalMark=$scope.CurrentStudent.Int1+$scope.CurrentStudent.Int2 + $scope.CurrentStudent.Int3;
                 $scope.CurrentStudent.GraceMark = $scope.StudentData[i][GraceKey];
                 $scope.MarkEntryArray.push($scope.CurrentStudent);
             }

         }
         $scope.MarkEntryArray.sort(function (a, b) {
             return a.RollNumber - b.RollNumber;
         });
         $scope.$apply();
         
     });
});
