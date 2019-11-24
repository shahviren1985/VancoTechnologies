angular.module('ExamSystemApp')
Examapp.controller("ExamFormsCtrl", function ($scope) {
    
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.user.course, 'sem')
    }
    var d = new Date();
    $.validate({
        form: '#ExamForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            $form.serialize();
            window.open(_CommonurlUI+'/ExaminationFormGenerate.html?' + $form.serialize()+'&year='+d.getFullYear(), '_blank');            
        }
    });
});