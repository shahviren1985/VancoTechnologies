angular.module('ExamSystemApp')
Examapp.controller("ImportStudentCtrl", function ($scope) {

    $scope.GetSemester = function (ContrllName, value) {
        BindSelectSemesterBasedonCourse($scope.user.course, 'sem');
    }

    $.validate({
        form: '#ImportStudentForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            //console.log($('#ImportStudentForm').serializeArray())
            /*var d = new Date();
            var data = new FormData();
            jQuery.each(jQuery('#file')[0].files, function (i, file) {
                data.append('file' + i, file);
            });*/
            //SaveCoreSubjects
            CallAPI("Data/SaveCoreSubjects?type=" + $("#specialization :selected").val()+"&semester=" + $("#sem :selected").val() + "&year=" + $("#year").val(), "GET").done(function (updatedResopose) {
                console.log(updatedResopose);
                if (updatedResopose == "Success" || updatedResopose.isSuccess) {
                    toastr.success("Students imported successfully", "", {
                        positionClass: "toast-bottom-right"
                    });
                } else {
                    toastr.error(updatedResopose.ErrorMessage, "An error occurred. " + updatedResopose, {
                        positionClass: "toast-bottom-right"
                    });
                }
            });
            /*CallAPIWithFile('Data/GenerateExamCsv?' + $form.serialize() + '&year=' + d.getFullYear(), data).done(function (updatedResopose) {
                console.log(updatedResopose)
                if (updatedResopose.isSuccess) {
                    toastr.success("File uploaded successfully", "", {
                        positionClass: "toast-bottom-right",
                    });
                } else {
                    toastr.error(updatedResopose.ErrorMessage, "", {
                        positionClass: "toast-bottom-right",
                    });
                }
            })*/
        }
    });



});