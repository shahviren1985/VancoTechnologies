angular.module('ExamSystemApp')
Examapp.controller("SeatNumbersCtrl", function ($scope) {
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.user.course, 'sem')
    }

    var configApi = CallAPI("/Exam/GetGlobalConfiguration", "GET");
    configApi.success(function (result) {
        localStorage.setItem("Configuration", JSON.stringify(result));
    });

    config = JSON.parse(localStorage.getItem("Configuration"));

    var d = new Date();
    $.validate({
        form: '#SeatNumber',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            //console.log($(document.activeElement).val());
            if ($(document.activeElement).val() == 'Generate') {

                if (config.seatNumberGenerated == false || (config.seatNumberGenerated == true && config.regenerateSeatNumber == true)) {
                    $scope.GenerateRollnumber($form.serialize());
                }
                else {
                    alert("Seat numbers are already generated for this semester, you can't regerate it now. Contact admin.");
                }
            } else if ($(document.activeElement).val() == 'PrintWithRollNumber') {

            } else if ($(document.activeElement).val() == 'Print') {

            }
        }
    });
    $scope.StudentData = [];
    var LastSeatNumber = 0;
    $scope.GenerateRollnumber = function (formvalue) {
        GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.user.course + '-' + $scope.user.specialization + '_sem' + $('#sem').val() + '_' + CurrentYear + '_' + $scope.user.examType + '.csv').done(function (dataresponse) {
            //console.log(dataresponse)
            LastSeatNumber = 0;
            try {
                $scope.StudentData = csvJSON(dataresponse);
                $scope.StudentData.forEach(function (v) {
                    delete v["\r"]
                });
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
                $('#SaveRecordbtn').css('display', 'block')
            }


            var R1 = $scope.user.course.toUpperCase().charAt(0)
            var R2 = $scope.user.examType.toUpperCase().charAt(0);
            var R3 = $scope.user.specialization.toUpperCase().charAt(0);
            var R4 = $('#sem').val();
            $scope.StudentData = sortBy($scope.StudentData, item => [item.Division, item.FirstName]);
            var config = JSON.parse(localStorage.getItem("Configuration"));
            //console.log($scope.StudentData)
            var LogicNumber = new Date().getFullYear().toString().slice(-2) + "" + parseInt(config.startSeatNumber);
            /*if ($('#sem').val() > 2 && $scope.user.specialization == 'regular') {
                var _strArray = localStorage.getItem('LastSeatNumberGenerated');
                var _array = [];
                try {
                    _array = JSON.parse(_strArray);
                    var _objarr = _array.filter(x => x.year == CurrentYear && x.sem == $('#sem').val() && x.course == $scope.user.course && x.specialization == 'honors' && x.examType == $scope.user.examType)
                    if (_objarr.length > 0) {
                        LogicNumber = (parseInt(_objarr[0].LastSeatNumber) + parseInt(_objarr[0].PlusNumber));
                    }
                } catch (e) {

                }
            }*/


            var Index = 0;
            var specialisation = "";
            var gap = 0;



            for (i = 0; i < $scope.StudentData.length; i++) {
                if ($scope.StudentData[i].RollNumber == undefined) {
                    continue;
                }
                var sw = $scope.user.specialization == 'regular' ? $scope.StudentData[i].Specialisation : specialisation;

                switch (sw) {
                    case "HTM":
                        gap = config.students.HTMRegularCount;
                        break;
                    case "IDRM":
                        gap = config.students.IDRMRegularCount;
                        break;
                    case "FND":
                        gap = config.students.FNDRegularCount;
                        break;
                    case "DC":
                        gap = config.students.DCRegularCount;
                        break;
                    case "ECCE":
                        gap = config.students.ECCERegularCount;
                        break;
                    case "MCE":
                        gap = config.students.MCERegularCount;
                        break;
                    case "TAD":
                        gap = config.students.TADRegularCount;
                        break;
                }
                if ($('#sem').val() > 2) {
                    if ((specialisation == "" && $scope.user.specialization == 'regular')) {
                        LogicNumber = parseInt(LogicNumber) + gap;
                    }
                    else if ($scope.StudentData[i].Specialisation != specialisation && $scope.user.specialization == 'regular') {
                        LogicNumber = parseInt(LogicNumber) + gap + 5;
                    }
                    else if (specialisation != "" && $scope.StudentData[i].Specialisation != specialisation && $scope.user.specialization == 'honors') {
                        LogicNumber = parseInt(LogicNumber) + gap + 5;
                    }
                }
                else {
                    if (specialisation != "" && $scope.StudentData[i].Specialisation != specialisation)
                        LogicNumber = parseInt(LogicNumber) + 5;
                }

                $scope.StudentData[i].SeatNumber = R1 + '-' + R2 + R3 + R4 + LogicNumber;
                LastSeatNumber = LogicNumber;
                LogicNumber = parseInt(LogicNumber) + 1;
                specialisation = $scope.StudentData[i].Specialisation;
                //Index = Index + 1;
            }
            $scope.StudentData = sortBy($scope.StudentData, item => [item.SeatNumber]);
            $scope.FinalStudentData = $scope.StudentData;
            $scope.$digest();
            //save all last seatnumber storage
        });
    }

    $scope.SaveRecord = function () {
        //console.log(JSON.stringify($scope.StudentData))
        CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.user.course + '-' + $scope.user.specialization + '_sem' + $('#sem').val() + '_' + CurrentYear + '_' + $scope.user.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
            if (respo.isSuccess) {
                var strlstArray = localStorage.getItem('LastSeatNumberGenerated');
                var c = JSON.parse(localStorage.getItem("Configuration"));
                c.seatNumberGenerated = true;
                localStorage.setItem('Configuration', JSON.stringify(c));
                CallAPI("Exam/SaveGlobalConfiguration", "POST", localStorage.getItem("Configuration"));

                try {
                    var lstArray = JSON.parse(strlstArray)
                } catch (e) {
                    lstArray = [];
                }
                var IndexOflst = -1;
                if (lstArray == undefined) {
                    lstArray = [];
                } else {
                    IndexOflst = lstArray.findIndex(x => x.year == CurrentYear && x.sem == $('#sem').val() && x.course == $scope.user.course && x.specialization == $scope.user.specialization && x.examType == $scope.user.examType);
                }

                if (IndexOflst == -1) {
                    var lstObj = {};
                    lstObj.year = CurrentYear;
                    lstObj.course = $scope.user.course;
                    lstObj.specialization = $scope.user.specialization;
                    lstObj.examType = $scope.user.examType;
                    lstObj.LastSeatNumber = LastSeatNumber;
                    lstObj.PlusNumber = 10;
                    lstObj.sem = $('#sem').val();
                    lstArray.push(lstObj);
                } else {
                    lstArray[IndexOflst].LastSeatNumber = LastSeatNumber;
                }
                localStorage.setItem('LastSeatNumberGenerated', JSON.stringify(lstArray));
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