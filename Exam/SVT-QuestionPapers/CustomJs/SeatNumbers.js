angular.module('ExamSystemApp')
Examapp.controller("SeatNumbersCtrl", function ($scope) {
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.user.course, 'sem')
    }
    var d = new Date();
    $.validate({
        form: '#SeatNumber',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            //console.log($(document.activeElement).val());
            if ($(document.activeElement).val() == 'Generate') {
                $scope.GenerateRollnumber($form.serialize());
            } else if ($(document.activeElement).val() == 'PrintWithRollNumber') {

            } else if ($(document.activeElement).val() == 'Print') {

            }
        }
    });
    $scope.StudentData = [];
    var LastSeatNumber=0;
    $scope.GenerateRollnumber = function (formvalue) {
        GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.user.course + '-' + $scope.user.specialization + '_sem' + $('#sem').val() + '_' + CurrentYear + '_' + $scope.user.examType + '.csv').done(function (dataresponse) {
            //console.log(dataresponse)
            LastSeatNumber=0;
            try {
                $scope.StudentData = csvJSON(dataresponse);
                $scope.StudentData.forEach(function(v){ 
                    delete v["\r"] });
                //console.log($scope.StudentData);
            } catch (e) {
                console.log(e)
            }
            if ($scope.StudentData.length < 1) {
                toastr.error("No Record found", "", {
                    positionClass: "toast-bottom-right",
                });
                $('#SaveRecordbtn').css('display', 'none')
            } else {
                $('#SaveRecordbtn').css('display','block')
            }
            
            
            var R1 = $scope.user.course.toUpperCase().charAt(0)
            var R2 = $scope.user.examType.toUpperCase().charAt(0);
            var R3 = $scope.user.specialization.toUpperCase().charAt(0);
            var R4 = $('#sem').val();
            $scope.StudentData=sortBy($scope.StudentData, item =>[item.Specialisation, item.LastName]);
            
            //console.log($scope.StudentData)
            var LogicNumber = 419;
            if($('#sem').val()>2 && $scope.user.specialization=='regular'){
                var _strArray=localStorage.getItem('LastSeatNumberGenerated');
                var _array=[];
                try {
                    _array=JSON.parse(_strArray);
                    var _objarr=_array.filter(x=>x.year==CurrentYear && x.sem==$('#sem').val() && x.course==$scope.user.course && x.specialization=='honors' && x.examType==$scope.user.examType)
                    if(_objarr.length>0){
                        LogicNumber=(parseInt(_objarr[0].LastSeatNumber)+ parseInt(_objarr[0].PlusNumber));
                    }
                } catch (e) {
    
                }
            }

            var Index = 0; 
            for (i = 0; i < $scope.StudentData.length; i++) {
                if ($scope.StudentData[i].RollNumber == undefined || ($scope.StudentData[i].LastName == '')) {
                    continue;
                }
                $scope.StudentData[i].SeatNumber = R1 + '-' + R2 + R3 + R4 + LogicNumber;
                LastSeatNumber=LogicNumber;
                LogicNumber = LogicNumber + 1;
                //Index = Index + 1;
            }
            $scope.StudentData=sortBy($scope.StudentData, item =>[item.Specialisation, item.RollNumber]);
            $scope.FinalStudentData = $scope.StudentData;
            $scope.$digest();
            //save all last seatnumber storage
        });
    }

    $scope.SaveRecord = function () {
        //console.log(JSON.stringify($scope.StudentData))
        CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.user.course + '-' + $scope.user.specialization + '_sem' + $('#sem').val() + '_' + CurrentYear + '_' + $scope.user.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
            if (respo.isSuccess) {
                var strlstArray=localStorage.getItem('LastSeatNumberGenerated');
                try {
                    var lstArray=JSON.parse(strlstArray)
                } catch (e) {
                    lstArray=[];
                }
                var IndexOflst=-1;
                if(lstArray==undefined){
                    lstArray=[];
                }else{
                    IndexOflst=lstArray.findIndex(x=>x.year==CurrentYear && x.sem==$('#sem').val() && x.course==$scope.user.course && x.specialization==$scope.user.specialization && x.examType==$scope.user.examType);
                }
                
                if(IndexOflst==-1){
                    var lstObj={};
                    lstObj.year=CurrentYear;
                    lstObj.course=$scope.user.course;
                    lstObj.specialization=$scope.user.specialization;
                    lstObj.examType=$scope.user.examType;
                    lstObj.LastSeatNumber=LastSeatNumber;
                    lstObj.PlusNumber=10;
                    lstObj.sem=$('#sem').val();
                    lstArray.push(lstObj);
                }else{
                    lstArray[IndexOflst].LastSeatNumber=LastSeatNumber;
                }
                localStorage.setItem('LastSeatNumberGenerated',JSON.stringify(lstArray));
                toastr.success("File uploaded successfully", "", {
                    positionClass: "toast-bottom-right",
                });
            } else {
                toastr.error(respo.ErrorMessage, "", {
                    positionClass: "toast-bottom-right",
                });
            }
        })
    }
});