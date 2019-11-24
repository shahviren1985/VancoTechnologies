angular.module('ExamSystemApp')
Examapp.controller("HallTicketsCtrl", function ($scope) {
    //alert()
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.user.course, 'sem')
    }
    var d = new Date();
    $.validate({
        form: '#HallTicket',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            $form.serialize();
            window.open(_CommonurlUI+'/hallticket.html?' + $form.serialize() + '&year=' + d.getFullYear(), '_blank');
        }
    });
});