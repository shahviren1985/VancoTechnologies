angular.module('ExamSystemApp')
Examapp.controller("marksheetsCtrl", function ($scope) {

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy'
    }).on('changeDate', function (e) {
        $(this).datepicker('hide');
    });
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.user.course, 'sem')
    }
    var d = new Date();
    $scope.StudentData = [];
    $.validate({
        form: '#MarksheetGenerate',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            window.open(_CommonurlUI + '/App/Marksheet/Marksheet.html?course=' + $scope.user.course + '&StudentType=' + $scope.user.specialization + '&sem=' + $('#sem').val() + '&CurrentYear=' + CurrentYear + '&ExamType=' + $scope.user.examType + '&date=' + $('#SelectedDate').val(), '_blank');
            //GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.user.course + '-' + $scope.user.specialization + '_sem' + $('#sem').val() + '_' + CurrentYear + '_' + $scope.user.examType + '.csv').done(function (dataresponse) {
            //    console.log(dataresponse)
            //    try {
            //        $scope.StudentData = csvJSON(dataresponse);
            //        $scope.StudentData.forEach(function (v) {
            //            delete v["\r"]
            //        });
            //        //console.log($scope.StudentData);
            //    } catch (e) {
            //        console.log(e)
            //    }
            //    if ($scope.StudentData.length < 1) {
            //        toastr.error("No Record found", "", {
            //            positionClass: "toast-bottom-right",
            //        });
            //    } else {
            //        localStorage.setItem("CurrentStudentRecord",JSON.stringify($scope.StudentData));
            //        window.open(_CommonurlUI+'/GenerateMarkSheet.html?date='+$('#SelectedDate').val(), '_blank');
            //    }
            //});
        }
    });
});
