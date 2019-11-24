angular.module('ExamSystemApp')
Examapp.controller("AdvancePaperCtrl", function ($scope, StudentCsvFile) {

    $scope.PageDetail = {
        PageTitle: "Advance Paper Mapping",
        Course: true,
        Stram: true,
        Semester: true,
        ExamType: true,
        Specialization: true
    }
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.PageDetail._Stram, 'semester')
    }
    $scope.GetString = function () {
        return _CommonurlUI + "/App/SVTForm/Form.html";
    }
    var SpecializationData = [];
    $(document).on("change keyup", "#semester", function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationData = response;
            var selectedCatrol = $('#Specialization');
            document.getElementById("Specialization").options.length = 0;
            selectedCatrol.append('<option disabled="disabled" selected="selected" value="">---Select----</option>');
            selectedCatrol.prop('disabled', false);
            for (i = 0; i < response.length; i++) {
                var optionstr = '<option value="' + response[i].specialisation + '" data-specialisationCode="' + response[i].specialisationCode + '">' + response[i].specialisation + '</option>'
                selectedCatrol.append(optionstr);
                //$('<option />', { value: response[i].specialisation, text: response[i].specialisation }).appendTo(selectedCatrol);
            }
        })
    });

    $scope.GetHeader = function () {
        var IndexSpe = SpecializationData.findIndex(x => x.specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase())
        $scope.HeaderData = SpecializationData[IndexSpe].paperDetails;
        $scope.HeaderData = sortByKey($scope.HeaderData, 'code');
        console.log($scope.HeaderData)
    }

    $scope.SubjectForStudent = function (Stud, subjectCode) {
        var StudInd = Stud.Subjects.findIndex(x => x._Code == subjectCode);
        if (StudInd == -1) {
            return false;
        } else {
            return true;
        }
    }
    var StudentData = [];
    var stramSelected = '';
    $scope.PageDetailFormSubmit = function () {
        stramSelected = this.PageDetail._Stram;
        GetCsvToJsonData('File/Download/Data/SVT?fileName=' + this.PageDetail._Stram + '-' + $scope.PageDetail._Course + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.PageDetail._ExamType + '.csv').done(function (dataresponse) {
            $('#PageDetailData').css('display', 'block');
            StudentData = csvJSON(dataresponse);
            $scope.GetHeader();
            var Spec = $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase();
            $scope.array = [];
            for (var st = 0; st < StudentData.length; st++) {
                if (Spec.toUpperCase() == StudentData[st].Specialisation.toUpperCase()) {
                    var objArray = {};
                    objArray = angular.copy(StudentData[st]);
                    objArray.Subjects = [];
                    for (var prop = 0; prop < Object.keys(StudentData[st]).length; prop++) {
                        var _PaperAppeared = ('Paper' + (prop + 1) + 'Appeared')
                        var _Code = 'Code' + (prop + 1);
                        var _InternalC = 'InternalC' + (prop + 1);
                        var _ExternalSection1C = 'ExternalSection1C' + (prop + 1);
                        var _ExternalSection2C = 'ExternalSection2C' + (prop + 1);
                        var _ExternalTotalC = 'ExternalTotalC1' + (prop + 1);
                        var _GraceC = 'GraceC' + (prop + 1);
                        var _PracticalMarksC = 'PracticalMarksC' + (prop + 1);
                        var _Attempt = 'Attempt' + (prop + 1);
                        var _Remarks = 'Remarks' + (prop + 1);

                        if (StudentData[st][_Code] != undefined) {
                            var SubjectDetail = {};
                            if (StudentData[st][_Code] != '' && StudentData[st][_Code] != undefined) {
                                SubjectDetail._PaperAppeared = StudentData[st][_PaperAppeared];;
                                SubjectDetail._Code = StudentData[st][_Code];
                                SubjectDetail._InternalC = StudentData[st][_InternalC];
                                SubjectDetail._ExternalSection1C = StudentData[st][_ExternalSection1C];
                                SubjectDetail._ExternalSection2C = StudentData[st][_ExternalSection2C];
                                SubjectDetail._ExternalTotalC = StudentData[st][_ExternalTotalC];
                                SubjectDetail._GraceC = StudentData[st][_GraceC];
                                SubjectDetail._PracticalMarksC = StudentData[st][_PracticalMarksC];
                                SubjectDetail._Attempt = StudentData[st][_Attempt];
                                SubjectDetail._Remarks = StudentData[st][_Remarks];
                                SubjectDetail._IsChecked = true;
                                objArray.Subjects.push(SubjectDetail);
                            }
                        } else {
                            for (var missing = 0; missing < $scope.HeaderData.length; missing++) {
                                var indexMissing = objArray.Subjects.findIndex(x => x._Code == $scope.HeaderData[missing].paperCode)
                                if (indexMissing == -1 && $scope.HeaderData[missing].paperCode != undefined) {
                                    var SubjectDetail = {};
                                    SubjectDetail._PaperAppeared = '*';
                                    SubjectDetail._Code = $scope.HeaderData[missing].code;
                                    SubjectDetail._InternalC = '';
                                    SubjectDetail._ExternalSection1C = '';
                                    SubjectDetail._ExternalSection2C = '';
                                    SubjectDetail._ExternalTotalC = '';
                                    SubjectDetail._GraceC = '';
                                    SubjectDetail._PracticalMarksC = '';
                                    SubjectDetail._Attempt = '';
                                    SubjectDetail._Remarks = '';
                                    SubjectDetail._IsChecked = false;
                                    objArray.Subjects.push(SubjectDetail);
                                }
                            }

                            objArray.Subjects = sortByKey(objArray.Subjects, '_Code');
                            break;
                        }
                    }
                    //objArray.LogicalSeatNumberKe = StudentData[st].SeatNumber.substring(4, StudentData[i].SeatNumber.length);

                    $scope.array.push(objArray)
                }
            }
            $scope.$apply();
        });
    }

    $scope.IsMarkEntryDone = function (subjectDetails) {
        if (!subjectDetails._IsChecked && ((subjectDetails._ExternalSection1C != '' && subjectDetails._ExternalSection1C != undefined)
            || (subjectDetails._ExternalSection2C != '' && subjectDetails._ExternalSection2C != undefined)
            || (subjectDetails._ExternalSection2C != '' && subjectDetails._ExternalTotalC != undefined)
            || (subjectDetails._InternalC != '' && subjectDetails._InternalC != undefined)
            || (subjectDetails._PracticalMarksC != '' && subjectDetails._PracticalMarksC != undefined))) {
            alert('Marks entry for this student is already done, You will loose marks once you save');
        }
    }

    $.validate({
        form: '#StudentInternalMark',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            CreateFormat();
        }
    });

    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }

    var CreateFormat = function () {
        console.log(StudentData);
        $.each($scope.array, function (i, val) {
            var StudIndex = StudentData.findIndex(x => x.SeatNumber == val.SeatNumber);
            if (StudIndex != -1) {
                for (var prop = 0; prop < Object.keys(StudentData[StudIndex]).length; prop++) {
                    var _PaperAppeared = ('Paper' + (prop + 1) + 'Appeared');
                    var _Code = 'Code' + (prop + 1);
                    var _InternalC = 'InternalC' + (prop + 1);
                    var _ExternalSection1C = 'ExternalSection1C' + (prop + 1);
                    var _ExternalSection2C = 'ExternalSection2C' + (prop + 1);
                    var _ExternalTotalC = 'ExternalTotalC' + (prop + 1);
                    var _GraceC = 'GraceC' + (prop + 1);
                    var _PracticalMarksC = 'PracticalMarksC' + (prop + 1);
                    var _Attempt = 'Attempt' + (prop + 1);
                    var _Remarks = 'Remarks' + (prop + 1);
                    if (StudentData[StudIndex][_Code] != undefined) {
                        delete StudentData[StudIndex][_PaperAppeared];
                        delete StudentData[StudIndex][_Code];
                        delete StudentData[StudIndex][_InternalC];
                        delete StudentData[StudIndex][_ExternalSection1C];
                        delete StudentData[StudIndex][_ExternalSection2C];
                        delete StudentData[StudIndex][_ExternalTotalC];
                        delete StudentData[StudIndex][_GraceC];
                        delete StudentData[StudIndex][_PracticalMarksC];
                        delete StudentData[StudIndex][_Attempt];
                        delete StudentData[StudIndex][_Remarks];
                    } else {
                        break;
                    }
                }
                var count = 0;
                for (var a = 0; a < val.Subjects.length; a++) {
                    if (val.Subjects[a]._IsChecked) {
                        count++;
                        var _PaperAppeared = ('Paper' + (count) + 'Appeared');
                        var _Code = 'Code' + (count);
                        var _InternalC = 'InternalC' + (count);
                        var _ExternalSection1C = 'ExternalSection1C' + (count);
                        var _ExternalSection2C = 'ExternalSection2C' + (count);
                        var _ExternalTotalC = 'ExternalTotalC' + (count);
                        var _GraceC = 'GraceC' + (count);
                        var _PracticalMarksC = 'PracticalMarksC' + (count);
                        var _Attempt = 'Attempt' + (count);
                        var _Remarks = 'Remarks' + (count);
                        StudentData[StudIndex][_PaperAppeared] = val.Subjects[a]['_PaperAppeared'];
                        StudentData[StudIndex][_Code] = val.Subjects[a]['_Code'];
                        StudentData[StudIndex][_InternalC] = val.Subjects[a]['_InternalC'];
                        StudentData[StudIndex][_ExternalSection1C] = val.Subjects[a]['_ExternalSection1C'];
                        StudentData[StudIndex][_ExternalSection2C] = val.Subjects[a]['_ExternalSection2C'];
                        StudentData[StudIndex][_ExternalTotalC] = val.Subjects[a]['_ExternalTotalC'];
                        StudentData[StudIndex][_GraceC] = val.Subjects[a]['_GraceC'];
                        StudentData[StudIndex][_PracticalMarksC] = val.Subjects[a]['_PracticalMarksC'];
                        StudentData[StudIndex][_Attempt] = val.Subjects[a]['_Attempt'];
                        StudentData[StudIndex][_Remarks] = val.Subjects[a]['_Remarks'];
                    }
                }
            }
        });

        StudentCsvFile.SaveFileObj(stramSelected, $scope.PageDetail._Course, $('#semester').val(), CurrentYear, $scope.PageDetail._ExamType, StudentData).done(function (respo) {
            if (respo.isSuccess) {
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