angular.module('ExamSystemApp')
Examapp.controller("ledgerreportCtrl", function ($scope) {

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
            window.open(_CommonurlUI + '/App/Marksheet/LadgerReport.html?course=' + $scope.user.course + '&StudentType=' + $scope.user.specialization + '&sem=' + $('#sem').val() + '&CurrentYear=' + CurrentYear + '&ExamType=' + $scope.user.examType + '&date=' + $('#SelectedDate').val(), '_blank');
        }
    });
});
