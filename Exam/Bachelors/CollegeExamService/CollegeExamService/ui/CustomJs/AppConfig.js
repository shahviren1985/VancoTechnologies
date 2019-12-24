
var Enum = {
    MenuDetail: [
        {
            id: 1,
            MenuContent: [{
                Heading: 'Import Student',
                Icon: 'fa fa-upload',
                URL: '#!/importStudent'
            }, {
                Heading: 'Update Student Info',
                Icon: 'fa fa-info',
                URL: '#!/UpdateInfo'
            },
            {
                Heading: 'ATKT Forms',
                Icon: 'fa fa-info',
                URL: 'App/Student/ATKTForm.html'
            }
            ]
        },
        {
            id: 2,
            MenuContent: [{
                Heading: 'GE',
                Icon: 'fa fa-plus',
                URL: '#!/UpdateGE'
            }, {
                Heading: 'DSE',
                Icon: 'fa fa-plus',
                URL: '#!/AddPracticalSubject'
            }, {
                Heading: 'Add On',
                Icon: 'fa fa-plus',
                URL: '#!/AddExtraSubject'
            }, {
                Heading: 'Practicals',
                Icon: 'fa fa-plus',
                URL: '#!/AddPracticalSubject'
            }, {
                Heading: 'Advance Paper Mapping',
                Icon: 'fa fa-plus',
                URL: '#!/AdvancePaper'
            }]
        },
        {
            id: 3,
            MenuContent: [{
                Heading: 'Internal Marks',
                Icon: 'fa fa-pencil',
                URL: '#!/internalMarkEntry'
            }, {
                Heading: 'Upload Marks',
                Icon: 'fa fa-pencil',
                URL: '#!/UploadMarks'
            }, {
                Heading: 'External Marks',
                Icon: 'fa fa-pencil',
                URL: '#!/ExternalMarkEntry'
            }, {
                Heading: 'Practicals Marks',
                Icon: 'fa fa-pencil',
                URL: '#!/PracticalMarkEntry'
            }, {
                Heading: 'Grace Marks',
                Icon: 'fa fa-pencil',
                URL: '#!/GraceMarkEntry'
            }, {
                Heading: 'ATKT Grace Marks',
                Icon: 'fa fa-pencil',
                URL: '#!/GenerateATKTStudent'
            },
            {
                Heading: 'Aggregate Marks Update',
                Icon: 'fa fa-pencil',
                URL: 'App/Student/UpdateAggregateStudentDetails.html'
            }
            ]
        }, {
            id: 4,
            MenuContent: [{
                Heading: 'Exam Forms',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/ExamForms'
            }, {
                Heading: 'Seat Number Report (II)',
                Icon: 'fa fa-sort-numeric-asc',
                URL: '#!/OtherReports'
            }, {
                Heading: 'Hall Tickets',
                Icon: 'fa fa-id-card-o',
                URL: '#!/HallTickets'
            }, {
                Heading: 'Seat Number Report',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/ExamForms'
            }, {
                Heading: 'Internal Marks Entry Sheet',
                Icon: 'fa fa-file-text',
                URL: '#!/internalMarkEntry'
            }, {
                Heading: 'External Marks Entry Sheets',
                Icon: 'fa fa-file-text',
                URL: '#!/ExternalMarkEntry'
            }, {
                Heading: 'External Exam Sheets',
                Icon: 'fa fa-file-text',
                URL: '#!/ExternalExamSheet'
            }, {
                Heading: 'Marksheets',
                Icon: 'fa fa-upload',
                URL: '#!/marksheets'
            }, {
                Heading: 'Ledger Report',
                Icon: 'fa fa-file-text',
                URL: '#!/ledger-report'
            }, {
                Heading: 'Seat Number Report',
                Icon: 'fa fa-sort-numeric-asc',
                URL: '#!/OtherReports'
            }, {
                Heading: 'Attendance Sheet',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/OtherReports'
            }, {
                Heading: 'Aggregate Marksheet',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/GenerateAggregateMarksheet'
            }, {
                Heading: 'Seat Number Report',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/GenerateSeatNumberReport'
            }
            ]
        },
        {
            id: 5,
            MenuContent: [{
                Heading: 'Summary Reports',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/Summaryreport'
            }, {
                Heading: 'Failed Student',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/GenerateATKTStudent'
            }, {
                Heading: 'Toppers Report',
                Icon: 'fa fa-trophy',
                URL: '#!/TopperReport'
            },
            {
                Heading: 'Aggregate Toppers Report',
                Icon: 'fa fa-trophy',
                URL: '#!/AggregateTopperReport'
            },
            {
                Heading: 'Convocation Report',
                Icon: 'fa fa-trophy',
                URL: '#!/ConvocationReport'
            }
            ]
        }, {
            id: 6,
            MenuContent: [{
                Heading: 'Transcripts',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/TranscriptRequest'
            }, {
                Heading: 'Verification Requests',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/'
            }, {
                Heading: 'Reevaluation Requests',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/'
            }, {
                Heading: 'Duplicate Marksheet Requests',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/'
            }, {
                Heading: 'Online ATKT Forms',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/'
            }, {
                Heading: 'Other Queries',
                Icon: 'fa fa-newspaper-o',
                URL: '#!/'
            }
            ]
        }, {
            id: 7,
            MenuContent: [{
                Heading: 'Year',
                Icon: 'fa fa-calendar',
                URL: '#!/YearSetting'
            }, {
                Heading: 'Seat Number Adjustment',
                Icon: 'fa fa-sort-numeric-asc',
                URL: '#!/SeatNumberAdj'
            }]
        }
    ]
};

Examapp.controller("DashboardCtrl", function ($scope) {

});
Examapp.controller("MenuDashboardCtrl", function ($scope) {
    console.log(getUrlVars()["MenuId"]);
    $scope.MenuData = [];
    $scope.MenuData = Enum.MenuDetail.filter(x => x.id == getUrlVars()["MenuId"])[0].MenuContent;
    console.log($scope.MenuData)
});
Examapp.controller("HallTicketsCtrl", function ($scope) {

});
Examapp.controller("OtherReportsCtrl", function ($scope) {
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
    function findWithAttr(array, attr, value) {
        for (var i = 0; i < array.length; i += 1) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }
    $.validate({
        form: '#StudentInternalMark',
        validateOnBlur: false, // disable validation when input looses focus
        errorMessagePosition: 'top', // Instead of 'inline' which is default
        scrollToTopOnError: false, // Set this property to true on longer forms
        onSuccess: function ($form) {
            CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv', JSON.stringify($scope.StudentData)).done(function (respo) {
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
    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }



    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    var SpecializationPaperData = [];
    var SelectedSortSpecialization = '';
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
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

    $scope.OpenReportAttendancesheet = function () {
        window.open(_CommonurlUI + '/AttendanceSheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&ReportType=Attendance Sheet', '_blank');
    }
    $scope.OpenReportSeatnumber = function () {
        window.open(_CommonurlUI + '/OtherReport.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&ReportType=Seat Number', '_blank');
    }
    $scope.OpenReportSeatnumber2 = function () {
        window.open(_CommonurlUI + '/OtherReport.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&ReportType=Seat Number&OnlySeatNumber=1', '_blank');
    }
});
Examapp.controller("LoginCtrl", function ($scope, $location) {
    $scope.LogintoSystem = function () {
        $('#Errormsg').css('display', 'none');
        var data = { username: $scope.usernamemodal, password: $scope.passwordmodal };
        $.ajax({
            "async": true,
            "crossDomain": true,
            "url": _CommonUr + "User/Login",
            "method": "POST",
            "headers": {
                "content-type": "application/json",
                "cache-control": "no-cache",
            },
            "processData": false,
            "data": JSON.stringify(data)
        }).done(function (response) {
            if (response.SuccessMessage == "Success") {
                localStorage.setItem('LoggedInUserDetail', JSON.stringify(response));
                $('.wrapper').css('display', '');
                $('#LoginViewHide').css('display', '');
                $('.content').css('padding', '15px')
                $('.content-wrapper').css('margin-left', '240px')
                //$location.url('/home');
                window.location.href = _CommonurlUI + '/#!/home'
            }
            else {
                $('#Errormsg').html(response.errorMessage);
                $('#Errormsg').css('display', 'block');

            }
        });
    }
});

Examapp.controller("HeaderManageController", function ($scope, $location) {
    $scope.isActive = function (route) {
        $scope.CheckLogin();
        if ($location.path() == '/MenuDashboard') {
            if (route.split("_").length > 1) {
                if ($location.$$search.MenuId == parseInt(route.split("_")[1])) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
        return route === $location.path();
    }
    $scope.CheckLogin = function () {
        if ($location.path() != '/Login') {
            $('.wrapper').css('display', '');
            $('.content-wrapper').css('margin-left', '240px')

        } else {
            $('#LoginViewHide').css('display', 'none');
            $('.content-wrapper').css('margin-left', '0px');
            $('.content').css('padding', '0px')
            setTimeout(function () {
                $('.wrapper').css('display', '');
            }, 200);
        }
    }
    $scope.SignOut = function () {
        localStorage.setItem('LoggedInUserDetail', "");
        $location.url('/Login');
    }
});

Examapp.controller("UpdateInfoCtrl", function ($scope) {
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
    function findWithAttr(array, attr, value) {
        for (var i = 0; i < array.length; i += 1) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }
    $.validate({
        form: '#StudentInternalMark',
        validateOnBlur: false, // disable validation when input looses focus
        errorMessagePosition: 'top', // Instead of 'inline' which is default
        scrollToTopOnError: false, // Set this property to true on longer forms
        onSuccess: function ($form) {
            CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
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
    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }

    $scope.array = [];
    $scope.StudentData = [];
    var InternalMinPassingMark = 0;
    var InternalMaxMark = 0;
    $.validate({
        form: '#InternalMarkEntry',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv').done(function (dataresponse) {
                try {
                    $scope.StudentData = csvJSON(dataresponse);
                } catch (e) {
                    console.log(e)
                }
                if ($scope.StudentData.length > 0) {
                    $scope.SelectedSpecializaion = $("#Specialization option:selected")[0].attributes[1].nodeValue.toUpperCase();
                    $('#InternalMarkEntryForm').css('display', 'block');
                    $scope.array.sort(function (a, b) {
                        return a.RollNumber - b.RollNumber;
                    });
                    $scope.$digest();
                } else {
                    $('#InternalMarkEntryForm').css('display', 'none');
                    toastr.error("No Record found", "", {
                        positionClass: "toast-bottom-right",
                    });
                }
            });
        }
    });

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    var SpecializationPaperData = [];
    var SelectedSortSpecialization = '';


    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
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

    $scope.OpenEntrySheet = function () {
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val() + '&PaperName=' + $("#paper option:selected").text(), '_blank');
    }
});

Examapp.controller("ExternalExamSheetCtrl", function ($scope, $location) {
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
    function findWithAttr(array, attr, value) {
        for (var i = 0; i < array.length; i += 1) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }

    $scope.array = [];
    $scope.StudentData = [];
    $.validate({
        form: '#ExternalMarkEntry',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            window.open(_CommonurlUI + '/ExternalExamSheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val() + '&section=' + $('#Section').val() + '&PaperName=' + $("#paper option:selected").text(), '_blank');

        }
    });

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    var SpecializationPaperData = [];
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
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

    $('#paper').on('change', function () {
        $scope.MinRequredPassingMark = $("#paper option:selected")[0].attributes[5].nodeValue;
        //console.log($("#paper option:selected")[0].attributes[5].nodeValue);
    });

    //Internal Passing mark
    $scope.MinRequredPassingMark = 0;
    $scope.MaxExternal1Mark = 0;
    $scope.MaxExternal2Mark = 0;
    $('#Specialization').on('change', function () {
        for (i = 0; i < SpecializationPaperData.length; i++) {
            if (SpecializationPaperData[i].specialisation == $('#Specialization').val()) {
                var selectedCatrol = $('#paper');
                document.getElementById("paper").options.length = 0;
                selectedCatrol.append('<option disabled="disabled" selected="selected" value="">---Select----</option>');
                selectedCatrol.prop('disabled', false);
                for (var j = 0; j < SpecializationPaperData[i].paperDetails.length; j++) {
                    if (SpecializationPaperData[i].paperDetails[j].theoryInternalMax > 0) {
                        var optionstr = '<option value="' + SpecializationPaperData[i].paperDetails[j].code + '" data-theoryExternalSection2Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection2Max + '" data-theoryExternalSection1Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection1Max + '" data-theoryExternalPassing="' + SpecializationPaperData[i].paperDetails[j].theoryExternalPassing + '" data-InternalMax="' + SpecializationPaperData[i].paperDetails[j].theoryInternalMax + '" data-passingmark="' + SpecializationPaperData[i].paperDetails[j].theoryInternalPassing + '">' + SpecializationPaperData[i].paperDetails[j].paperTitle + '</option>'
                        selectedCatrol.append(optionstr);
                        //$('<option />', { value: SpecializationPaperData[i].paperDetails[j].code, text: SpecializationPaperData[i].paperDetails[j].paperTitle, 'data-passingmark': SpecializationPaperData[i].paperDetails[j].theoryInternalPassing }).appendTo(selectedCatrol);
                    }
                }

            }
        }
    })

    $scope.OpenEntrySheet = function () {
        //console.log($('#Specialization').getAttribute('data-specialisationcode'));
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val() + '&PaperName=' + $("#paper option:selected").text(), '_blank');
    }
})

Examapp.controller("UpdateGECtrl", function ($scope, $location) {
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
    function findWithAttr(array, attr, value) {
        for (var i = 0; i < array.length; i += 1) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }

    $scope.GetSubjectNameFromCode = function (code) {
        for (var j = 0; j < $scope.SubjectData.length; j++) {
            if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                    if ($scope.SubjectData[j].paperDetails[k].code == code) {
                        return $scope.SubjectData[j].paperDetails[k].paperTitle;
                    }
                }
            }
        }
    }

    $scope.CheckValidationForGE = function () {
        var DuplicateResult = true;
        for (var ar = 0; ar < $scope.array.length; ar++) {
            var valueArr = $scope.array[ar].SubjectCount.map(function (item) { return item.Selected });
            var isDuplicate = valueArr.some(function (item, idx) {
                return valueArr.indexOf(item) != idx && item != ''
            });
            if (isDuplicate) {
                return false;
            }
            if ((ar + 1) == $scope.array.length) {
                return DuplicateResult;
            }
        }
    }

    $.validate({
        form: '#FormStudentGE',
        validateOnBlur: false, // disable validation when input looses focus
        errorMessagePosition: 'top', // Instead of 'inline' which is default
        scrollToTopOnError: false, // Set this property to true on longer forms
        onSuccess: function ($form) {
            if ($scope.CheckValidationForGE()) {
                for (var ar = 0; ar < $scope.array.length; ar++) {
                    var index = $scope.StudentData.findIndex(p => p.SeatNumber == $scope.array[ar].SeatNumber);
                    if (index != -1 && index != undefined && index != '') {
                        var PositionData = $scope.array[ar].SubjectCount.map(function (el) { return el.Position; });
                        var GE_Avlbl_Position = PositionData.filter(function (e) { return e != '' });
                        //Check Is There any Blank Code In Student, If there Add for Available to add number
                        for (var prop = 0; prop < Object.keys($scope.StudentData[index]).length; prop++) {
                            var strCode = 'Code' + (prop + 1)
                            if ($scope.StudentData[index][strCode] != undefined) {
                                if ($scope.StudentData[index][strCode] == '') {
                                    GE_Avlbl_Position.push(strCode);
                                } else {
                                    continue;
                                }
                            } else {
                                break;
                            }
                        }

                        var uniq_GE_Avlbl_Position = GE_Avlbl_Position.reduce(function (a, b) {
                            if (a.indexOf(b) < 0) a.push(b);
                            return a;
                        }, []);

                        var To_Add_GE_In_CSV = $scope.array[ar].SubjectCount.filter(function (e) { return e.Selected != '' });

                        if (To_Add_GE_In_CSV > uniq_GE_Avlbl_Position.length) {
                            alert('Add More Code Field')
                        } else {
                            //clear all existing data from available position
                            for (var rm = 0; rm < uniq_GE_Avlbl_Position.length; rm++) {
                                //Clear subjects property here i.e. InternalMarkc1='' if necessary
                                $scope.StudentData[index][uniq_GE_Avlbl_Position[rm]] = '';
                            }

                            for (var TA = 0; TA < To_Add_GE_In_CSV.length; TA++) {
                                $scope.StudentData[index][uniq_GE_Avlbl_Position[0]] = To_Add_GE_In_CSV[TA].Selected;
                                uniq_GE_Avlbl_Position.splice(0, 1);
                            }
                        }
                    }
                    console.log($scope.StudentData[index])
                    if ($scope.array.length == (ar + 1)) {
                        CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
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
                }
            } else {
                toastr.error("Duplicate Subject names on same student, Please correct!", "", {
                    positionClass: "toast-bottom-right",
                });
            }
        }
    });

    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }


    $scope.IsSubjectGeneralElective = function (subjectCode) {
        var _IndexOfDept = $scope.SubjectData.findIndex(x => x.specialisationCode === $("#Specialization option:selected")[0].attributes[1].nodeValue);
        if (_IndexOfDept >= 0) {
            var _IndexOfSubject = $scope.SubjectData[_IndexOfDept].paperDetails.findIndex(x => x.paperCode === subjectCode);
            if (_IndexOfSubject >= 0) {
                if ($scope.SubjectData[_IndexOfDept].paperDetails[_IndexOfSubject].isElective.toLowerCase() == 'yes') {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    $scope.GetGECount = function () {
        var GeCountData = 0;
        if ($scope.SubjectData == undefined) {
            return 0;
        }
        for (var j = 0; j < $scope.SubjectData.length; j++) {
            if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                    if ($scope.SubjectData[j].paperDetails[k].isElective.toLowerCase() == 'yes') {
                        GeCountData++;
                    }
                }
            }
            if ((j + 1) == $scope.SubjectData.length) {
                return Math.ceil(GeCountData / 2);
                //return GeCountData;
            }
        }
    }

    $scope.AddGEWhichAreNotInCSV = function (ExistingArray) {
        var drpToShow = $scope.GetGECount();
        var LogicalReqDrp = 0;

        if (ExistingArray.length == drpToShow) {
            return ExistingArray;
        } else if (ExistingArray.length > drpToShow) {
            return ExistingArray;
        } else {
            var PositionDetails = {};
            PositionDetails.Position = '';
            PositionDetails.Selected = '';
            PositionDetails.Code = '';

            AddingBlankDropDown: for (var AddGE = 0; AddGE < drpToShow; AddGE++) {
                if (ExistingArray.length < drpToShow) {
                    ExistingArray.push(PositionDetails);
                } else {
                    return ExistingArray;
                }
            }
            return ExistingArray;
        }

        for (var j = 0; j < $scope.SubjectData.length; j++) {
            if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                    if ($scope.SubjectData[j].paperDetails[k].isElective.toLowerCase() == 'yes') {
                        var index = ExistingArray.findIndex(p => p.Code == $scope.SubjectData[j].paperDetails[k].code);
                        if (index == -1) {
                            var PositionDetails = {};
                            PositionDetails.Position = '';
                            PositionDetails.Selected = '';
                            PositionDetails.Code = $scope.SubjectData[j].paperDetails[k].code;
                            ExistingArray.push(PositionDetails)
                        }
                    }
                }
            }
            if ((j + 1) == $scope.SubjectData.length) {
                return ExistingArray;
            }
        }
    }

    $scope.array = [];
    $scope.StudentData = [];
    var InternalMinPassingMark = 0;
    var InternalMaxMark = 0;
    $.validate({
        form: '#InternalMarkEntry',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            CallAPI('User/GetPaperList?Course=' + $scope.InternalMark.stream + '&specialization=' + $('#course').val() + '&sem=' + $('#semester').val() + '', 'GET', '').done(function (SubjectData) {
                $scope.SubjectData = SubjectData;
                $scope.TotalGeneralElective = [];
                for (var j = 0; j < $scope.SubjectData.length; j++) {
                    if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                        for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                            if ($scope.SubjectData[j].paperDetails[k].isElective.toLowerCase() == 'yes') {
                                $scope.TotalGeneralElective.push($scope.SubjectData[j].paperDetails[k].code);
                            }
                        }
                    }
                }
                GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv').done(function (dataresponse) {
                    try {
                        $scope.StudentData = csvJSON(dataresponse);
                    } catch (e) {
                        console.log(e)
                    }
                    if ($scope.StudentData.length > 0) {
                        $scope.SelectedSpecializaion = $("#Specialization option:selected")[0].attributes[1].nodeValue.toUpperCase();
                        $('#InternalMarkEntryForm').css('display', 'block');
                        $scope.MaxSubjectCount = 0;
                        $scope.array = [];
                        for (var st = 0; st < $scope.StudentData.length; st++) {
                            if ($scope.SelectedSpecializaion == $scope.StudentData[st].Specialisation.toUpperCase()) {
                                var objArray = {};
                                objArray.FullName = $scope.StudentData[st].LastName + ' ' + $scope.StudentData[st].FirstName + ' ' + $scope.StudentData[st].FatherName + ' ' + $scope.StudentData[st].MotherName;
                                objArray.SeatNumber = $scope.StudentData[st].SeatNumber;
                                objArray.RollNumber = $scope.StudentData[st].RollNumber;
                                objArray.SubjectCount = [];
                                0
                                for (var prop = 0; prop < Object.keys($scope.StudentData[st]).length; prop++) {
                                    var strCode = 'Code' + (prop + 1)
                                    if ($scope.StudentData[st][strCode] != undefined) {
                                        if ($scope.IsSubjectGeneralElective($scope.StudentData[st][strCode]) == true) {
                                            var PositionDetails = {};
                                            PositionDetails.Position = strCode;
                                            PositionDetails.Selected = $scope.StudentData[st][strCode];
                                            PositionDetails.Code = $scope.StudentData[st][strCode];
                                            objArray.SubjectCount.push(PositionDetails);
                                        }
                                    } else {
                                        break;
                                    }
                                }
                                objArray.LogicalSeatNumberKe = $scope.StudentData[st].SeatNumber.substring(4, $scope.StudentData[i].SeatNumber.length);
                                objArray.SubjectCount = $scope.AddGEWhichAreNotInCSV(objArray.SubjectCount);
                                console.log(objArray)
                                $scope.array.push(objArray)
                            }
                        }
                        $scope.array.sort(function (a, b) {
                            return a.LogicalSeatNumberKe - b.LogicalSeatNumberKe;
                        });
                        $scope.$digest();
                    } else {
                        $('#InternalMarkEntryForm').css('display', 'none');
                        toastr.error("No Record found", "", {
                            positionClass: "toast-bottom-right",
                        });
                    }
                });
            });
        }
    });

    $scope.CreateNumberArray = function (num) {
        if (num > 0) {
            return new Array(num);
        }
    }

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    var SpecializationPaperData = [];
    var SelectedSortSpecialization = '';
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
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

    $(".Drp_Cnrtl").change(function () {
        $('#InternalMarkEntryForm').css('display', 'none')
    });

    $scope.OpenEntrySheet = function () {
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val() + '&PaperName=' + $("#paper option:selected").text(), '_blank');
    }
})

Examapp.controller("UploadMarksCtrl", function ($scope, $location) {
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.UploadControl.stream, 'semester')
    }

    function browserSupportFileUpload() {
        var isCompatible = false;
        if (window.File && window.FileReader && window.FileList && window.Blob) {
            isCompatible = true;
        }
        return isCompatible;
    }

    document.getElementById('file').addEventListener('change', upload, false);

    // Method that reads and processes the selected file
    var uploadedArray;
    function upload(evt) {
        if (!browserSupportFileUpload()) {
            alert('The File APIs are not fully supported in this browser!');
        } else {
            var data = null;
            var file = $('#file').prop('files');;
            var reader = new FileReader();
            reader.readAsText(file[0]);
            reader.onload = function (event) {
                var csvData = event.target.result;
                data = csvJSON(csvData);
                if (data && data.length > 0) {
                    uploadedArray = data;
                } else {
                    alert('No data to import!');
                }
            };
            reader.onerror = function () {
                alert('Unable to read ' + file.fileName);
            };
        }
    }
    var MainArray = [];
    $scope.CouldNtFindStudentSubject = [];
    $scope.UpdateFromtoEndArray = function () {
        for (var up = 0; up < uploadedArray.length; up++) {

            if (uploadedArray[up].SeatNumber != undefined && uploadedArray[up].SeatNumber != '' && uploadedArray[up].SeatNumber != null) {
                var index = MainArray.findIndex(p => p.SeatNumber == uploadedArray[up].SeatNumber);
                if (index >= 0) {
                    console.log(uploadedArray[up]);
                    ReadFromUploadedArray: for (var uprop = 0; uprop < Object.keys(uploadedArray[up]).length; uprop++) {
                        var strCode = 'Code' + (uprop + 1)
                        if (uploadedArray[up][strCode] != undefined) {
                            if (uploadedArray[up][strCode] != '') {
                                ReadFromMainArrayArray: for (var prop = 0; prop < Object.keys(MainArray[index]).length; prop++) {
                                    var strCodeMainAr = 'Code' + (prop + 1)
                                    if (MainArray[index][strCodeMainAr] != undefined) {
                                        //not blank & Same code
                                        var strKeySec1 = "ExternalSection1C" + (prop + 1);
                                        var strKeySec2 = "ExternalSection2C" + (prop + 1);
                                        var strKeyTotal = "ExternalTotalC" + (prop + 1);

                                        var UstrKeySec1 = "ExternalSection1C" + (uprop + 1);
                                        var UstrKeySec2 = "ExternalSection2C" + (uprop + 1);
                                        var UstrKeyTotal = "ExternalTotalC" + (uprop + 1);
                                        if (MainArray[index][strCodeMainAr] != '' && uploadedArray[up][strCode] == MainArray[index][strCodeMainAr]) {
                                            MainArray[index][strKeySec1] = uploadedArray[up][UstrKeySec1];
                                            MainArray[index][strKeySec2] = uploadedArray[up][UstrKeySec2];
                                            MainArray[index][strKeyTotal] = uploadedArray[up][UstrKeyTotal];
                                            break ReadFromMainArrayArray;
                                        } else {
                                            continue;
                                        }
                                    } else {
                                        var MissingSubjectobj = {};
                                        MissingSubjectobj.Subject = uploadedArray[up][strCode];
                                        MissingSubjectobj.SeatNumber = uploadedArray[up].SeatNumber;
                                        MissingSubjectobj.ErrorMsg = "subject " + uploadedArray[up][strCode] + " is present in uploaded file but not present in destination file for Seat Number" + uploadedArray[up].SeatNumber;
                                        $scope.CouldNtFindStudentSubject.push(MissingSubjectobj);
                                        break ReadFromMainArrayArray;
                                    }
                                }
                            } else {
                                continue;
                            }
                        } else {
                            break ReadFromUploadedArray;
                        }
                    }
                } else {
                    //If Index couldnt find from main array by uploaded stuent seatnumber;
                    if (uploadedArray[up].SeatNumber != undefined) {
                        var objNotFound = {};
                        objNotFound.SeatNumber = uploadedArray[up].SeatNumber;
                        objNotFound.ErrorMsg = "Seat Number " + uploadedArray[up].SeatNumber + " is not present uploaded file but not present in destination file";
                        $scope.CouldNtFindStudentSubject.push(objNotFound);
                    }
                }
            } else {
                //IF Roll number is blank or null or undefined in Uploaded csv
                if (uploadedArray[up].SeatNumber != undefined) {
                    var objSeatNotFound = {};
                    objSeatNotFound.SeatNumber = uploadedArray[up].SeatNumber;
                    objSeatNotFound.ErrorMsg = "Record number " + (up + 1) + " has no Seat number";
                    $scope.CouldNtFindStudentSubject.push(objSeatNotFound);
                }
            }
            if (uploadedArray.length == (up + 1)) {
                if ($scope.CouldNtFindStudentSubject.length > 0) {
                    $('#UploadMarkResult_div').css('display', '');
                    $scope.$apply();
                } else {
                    $scope.ConfirmSave();
                }
            }
        }
    }

    var currentdate = new Date();
    var datetime = '';
    $.validate({
        form: '#UploadMarkEntryForm',
        validateOnBlur: true, // disable validation when input looses focus
        modules: 'file',
        onSuccess: function ($form) {
            datetime = currentdate.getFullYear() + '-' + (currentdate.getMonth() + 1) + '-' + currentdate.getDate()
                + " " + currentdate.getHours() + ' ' + currentdate.getMinutes() + ' ' + currentdate.getSeconds();
            CallAPISaveFile('Data/SaveMarks?fileName=ImportMark_' + $scope.UploadControl.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.UploadControl.examType + '_' + datetime + '.csv', JSON.stringify(angular.toJson(uploadedArray))).done(function (respo) {
                if (respo.isSuccess) {
                    GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.UploadControl.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.UploadControl.examType + '.csv').done(function (dataresponse) {
                        try {
                            MainArray = csvJSON(dataresponse);
                        } catch (e) {
                            console.log(e)
                        }
                        if (MainArray.length > 0) {
                            console.log(MainArray);
                            $scope.UpdateFromtoEndArray();
                        }
                        else {
                            toastr.error("No Record found", "", {
                                positionClass: "toast-bottom-right",
                            });
                        }
                    });
                } else {
                    toastr.error(respo.ErrorMessage, "", {
                        positionClass: "toast-bottom-right",
                    });
                }
            })
        }
    });

    $scope.ConfirmSave = function () {
        CallAPISaveFile('Data/SaveJsonFiles?fileName=ImportMarkJson_' + $scope.UploadControl.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.UploadControl.examType + '_' + datetime + '.json', JSON.stringify(angular.toJson($scope.CouldNtFindStudentSubject))).done(function (respo) {
            CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.UploadControl.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.UploadControl.examType + '.csv', JSON.stringify(angular.toJson(MainArray))).done(function (respo) {
                if (respo.isSuccess) {
                    toastr.success("File saved successfully", "", {
                        positionClass: "toast-bottom-right",
                    });
                } else {
                    toastr.error(respo.ErrorMessage, "", {
                        positionClass: "toast-bottom-right",
                    });
                }
            });
        });
    }
});

Examapp.controller("GenerateATKTStudentCtrl", function ($scope, $location) {

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.GenerateATKTStudent.stream, 'semester')
    }

    $scope.GetPassAndFailedStudent = function () {
        for (var i = 0; i < MainArray.length; i++) {
            for (var prop = 0; prop < Object.keys(MainArray[i]).length; prop++) {
                var strCode = 'Code' + (prop + 1)
                if (MainArray[i][strCode] != undefined) {
                    if (MainArray[i][strCode] == '') {
                        //Here is the logic to check is he pass or not
                    } else {
                        continue;
                    }
                } else {
                    break;
                }
            }
        }
    }

    var MainArray;
    $.validate({
        form: '#UploadMarkEntryForm',
        validateOnBlur: true, // disable validation when input looses focus
        modules: 'file',
        onSuccess: function ($form) {
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.GenerateATKTStudent.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.GenerateATKTStudent.examType + '.csv').done(function (dataresponse) {
                try {
                    MainArray = csvJSON(dataresponse);
                } catch (e) {
                    console.log(e)
                }
                if (MainArray.length > 0) {
                    console.log(MainArray);
                }
                else {
                    toastr.error("No Record found", "", {
                        positionClass: "toast-bottom-right",
                    });
                }
            });
        }
    });
})

Examapp.controller("AddExtraSubjectCtrl", function ($scope, $location) {
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.AddExtraSubject.stream, 'semester')
    }

    var SpecializationPaperData = [];
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=Honors&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
            var selectedCatrol = $('#Specialization');
            document.getElementById("Specialization").options.length = 0;
            selectedCatrol.append('<option disabled="disabled" selected="selected" value="">---Select----</option>');
            selectedCatrol.prop('disabled', false);
            for (i = 0; i < response.length; i++) {
                var optionstr = '<option value="' + response[i].specialisation + '" data-specialisationCode="' + response[i].specialisationCode + '">' + response[i].specialisation + '</option>'
                selectedCatrol.append(optionstr);
            }
        })
    });

    $scope.GetAllPaper = function () {

    }

    $("#selectall").click(function () {
        var checkAll = $("#selectall").prop('checked');
        if (checkAll) {
            $(".clsStudentCheckBox").prop("checked", true);
        } else {
            $(".clsStudentCheckBox").prop("checked", false);
        }
    });

    $(document).on('click', '.clsStudentCheckBox', function () {
        if ($(".clsStudentCheckBox").length == $(".clsStudentCheckBox:checked").length) {
            $("#selectall").prop("checked", true);
        } else {
            $("#selectall").prop("checked", false);
        }
    });

    $("#selectallSubject").click(function () {
        var checkAll = $("#selectallSubject").prop('checked');
        if (checkAll) {
            $(".clsPaperChkBox").prop("checked", true);
        } else {
            $(".clsPaperChkBox").prop("checked", false);
        }
    });

    $(document).on('click', '.clsPaperChkBox', function () {
        if ($(".clsPaperChkBox").length == $(".clsPaperChkBox:checked").length) {
            $("#selectallSubject").prop("checked", true);
        } else {
            $("#selectallSubject").prop("checked", false);
        }
    });

    $scope.SubjectSelectHonors = [];
    $('#Specialization').on('change', function () {
        for (i = 0; i < SpecializationPaperData.length; i++) {
            if (SpecializationPaperData[i].specialisation == $('#Specialization').val()) {
                for (var j = 0; j < SpecializationPaperData[i].paperDetails.length; j++) {
                    var SubjectSelectobj = {};
                    SubjectSelectobj.Code = SpecializationPaperData[i].paperDetails[j].code;
                    SubjectSelectobj.paperTitle = SpecializationPaperData[i].paperDetails[j].paperTitle;
                    SubjectSelectobj.PaperType = SpecializationPaperData[i].paperDetails[j].paperType;
                    $scope.SubjectSelectHonors.push(SubjectSelectobj);
                }
            }
        }
        console.log($scope.SubjectSelectHonors)
    });

    $scope.StudentData = [];

    $scope.RetriveALlStudent = function () {
        if ($scope.StudentData != undefined && $scope.StudentData.length > 0) {
            $scope.SelectedSpecializaion = $("#Specialization option:selected")[0].attributes[1].nodeValue.toUpperCase();
            $('#AddExtraSubjectForm2').css('display', 'block');
            $scope.MaxSubjectCount = 0;
            $scope.array = [];
            for (var st = 0; st < $scope.StudentData.length; st++) {
                if ($scope.SelectedSpecializaion == $scope.StudentData[st].Specialisation.toUpperCase()) {
                    var objArray = {};
                    objArray.FullName = $scope.StudentData[st].LastName + ' ' + $scope.StudentData[st].FirstName + ' ' + $scope.StudentData[st].FatherName + ' ' + $scope.StudentData[st].MotherName;
                    objArray.SeatNumber = $scope.StudentData[st].SeatNumber;
                    objArray.RollNumber = $scope.StudentData[st].RollNumber;
                    objArray.IsRequiredCheck = false;
                    if (!IsNewFile) {
                        for (var prop = 0; prop < Object.keys($scope.StudentData[st]).length; prop++) {
                            var strCodeExist = 'Code' + (prop + 1);
                            if ($scope.StudentData[st][strCodeExist] != undefined) {
                                if ($scope.StudentData[st][strCodeExist] != '') {
                                    objArray.IsRequiredCheck = true;
                                    $('#paperChk_' + $scope.StudentData[st][strCodeExist]).prop("checked", true);
                                } else {
                                    continue;
                                }
                            } else {
                                break;
                            }
                        }
                    }
                    objArray.SubjectCount = [];
                    objArray.LogicalSeatNumberKe = $scope.StudentData[st].SeatNumber.substring(4, $scope.StudentData[i].SeatNumber.length);
                    $scope.array.push(objArray)
                }
            }
            $scope.array.sort(function (a, b) {
                return a.LogicalSeatNumberKe - b.LogicalSeatNumberKe;
            });
            $scope.$digest();
        } else {
            $('#InternalMarkEntryForm').css('display', 'none');
            toastr.error("No Record found", "", {
                positionClass: "toast-bottom-right",
            });
        }
        console.log($scope.array)
    }

    var TotalCountOfStudent = 0;
    var IsNewFile = true;
    $.validate({
        form: '#AddExtraSubjectForm1',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            $scope.StudentData = [];
            $('.clsPaperChkBox').each(function () {
                $(this).removeAttr('checked');
            })
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.AddExtraSubject.stream + '-Elective_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.AddExtraSubject.examType + '.csv').done(function (dataresponse) {
                try {
                    $scope.StudentData = csvJSON(dataresponse);
                    TotalCountOfStudent = $scope.StudentData.length;
                } catch (e) {
                    console.log(e)
                }
                if ($scope.StudentData != undefined && $scope.StudentData.length > 0) {
                    IsNewFile = false;
                    $scope.RetriveALlStudent();
                } else {
                    IsNewFile = true;
                    GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.AddExtraSubject.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.AddExtraSubject.examType + '.csv').done(function (dataresponse) {
                        try {
                            $scope.StudentData = csvJSON(dataresponse);
                            TotalCountOfStudent = $scope.StudentData.length;
                            var index = 0;
                            for (var prop = 0; prop < Object.keys($scope.StudentData[index]).length; prop++) {
                                var strCode = 'Code' + (prop + 1);
                                if ($scope.StudentData[index][strCode] != undefined) {
                                    var strInternal = 'InternalC' + (prop + 1);
                                    var strExternal1 = 'ExternalSection1C' + (prop + 1);
                                    var strExternal2 = 'ExternalSection2C' + (prop + 1);
                                    var strExternalTotal = 'ExternalTotalC' + (prop + 1);
                                    var strGraceC1 = 'GraceC' + (prop + 1);
                                    var strPracticalMarks = 'PracticalMarksC' + (prop + 1);
                                    var strAttempt = 'Attempt' + (prop + 1);
                                    var strRemarks = 'Remarks' + (prop + 1);
                                    $scope.StudentData.forEach(function (v) { v[strCode] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strInternal] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strExternal1] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strExternal2] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strExternalTotal] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strGraceC1] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strPracticalMarks] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strAttempt] = '' });
                                    $scope.StudentData.forEach(function (v) { v[strRemarks] = '' });
                                } else {
                                    break;
                                }
                            }

                        } catch (e) {
                            console.log(e)
                        }
                        $scope.RetriveALlStudent();
                    });
                }
            });
        }
    })
    $(".Drp_Cnrtl").change(function () {
        $('#AddExtraSubjectForm2').css('display', 'none')
    });
    $scope.SaveAllRecord = function () {
        var clsCount = 0;
        $(".clsStudentCheckBox").each(function () {
            clsCount++;
            var index = $scope.StudentData.findIndex(p => p.SeatNumber == $(this).attr("data-seatnumber"));
            for (var prop = 0; prop < Object.keys($scope.StudentData[index]).length; prop++) {
                var strCode = 'Code' + (prop + 1);
                var strInternal = 'InternalC' + (prop + 1);
                var strExternal1 = 'ExternalSection1C' + (prop + 1);
                var strExternal2 = 'ExternalSection2C' + (prop + 1);
                var strExternalTotal = 'ExternalTotalC' + (prop + 1);
                var strGraceC1 = 'GraceC' + (prop + 1);
                var strPracticalMarks = 'PracticalMarksC' + (prop + 1);
                var strAttempt = 'Attempt' + (prop + 1);
                var strRemarks = 'Remarks' + (prop + 1);
                if ($scope.StudentData[index][strCode] != undefined) {
                    $scope.StudentData[index][strCode] = '';
                    $scope.StudentData[index][strInternal] = '';
                    $scope.StudentData[index][strExternal1] = '';
                    $scope.StudentData[index][strExternal2] = '';
                    $scope.StudentData[index][strExternalTotal] = '';
                    $scope.StudentData[index][strGraceC1] = '';
                    $scope.StudentData[index][strPracticalMarks] = '';
                    $scope.StudentData[index][strAttempt] = '';
                    $scope.StudentData[index][strRemarks] = '';
                } else {
                    break;
                }
            }
            if (this.checked) {
                var count = 1;
                $('.clsPaperChkBox').each(function () {
                    if (this.checked) {
                        var strToAddCode = 'Code' + (count);
                        var strCode = 'Code' + (count);
                        var strInternal = 'InternalC' + (prop + 1);
                        var strExternal1 = 'ExternalSection1C' + (count);
                        var strExternal2 = 'ExternalSection2C' + (count);
                        var strExternalTotal = 'ExternalTotalC' + (count);
                        var strGraceC1 = 'GraceC' + (count);
                        var strPracticalMarks = 'PracticalMarksC' + (count);
                        var strAttempt = 'Attempt' + (count);
                        var strRemarks = 'Remarks' + (count);
                        //var valueFromCheckbox=
                        $scope.StudentData[index][strCode] = $(this).attr("data-Code");
                        $scope.StudentData[index][strInternal] = '';
                        $scope.StudentData[index][strExternal1] = '';
                        $scope.StudentData[index][strExternal2] = '';
                        $scope.StudentData[index][strExternalTotal] = '';
                        $scope.StudentData[index][strGraceC1] = '';
                        $scope.StudentData[index][strPracticalMarks] = '';
                        $scope.StudentData[index][strAttempt] = '';
                        $scope.StudentData[index][strRemarks] = '';
                        count++;
                    }
                });
            }
        });
        if ((clsCount) == $('.clsStudentCheckBox').length) {
            console.log(angular.toJson($scope.StudentData))
            if ($scope.StudentData.length == TotalCountOfStudent) {
                CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.AddExtraSubject.stream + '-Elective_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.AddExtraSubject.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
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
            } else {
                alert("Students record are missing! Please try again")
                $route.reload();
            }
        }
        //save data
    }
})

Examapp.controller("AddPracticalSubjectCtrl", function ($scope, $location) {
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
    function findWithAttr(array, attr, value) {
        for (var i = 0; i < array.length; i += 1) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }

    $scope.GetSubjectNameFromCode = function (code) {
        for (var j = 0; j < $scope.SubjectData.length; j++) {
            if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                    if ($scope.SubjectData[j].paperDetails[k].code == code) {
                        return $scope.SubjectData[j].paperDetails[k].paperTitle;
                    }
                }
            }
        }
    }

    $scope.CheckValidationForGE = function () {
        var DuplicateResult = true;
        for (var ar = 0; ar < $scope.array.length; ar++) {
            var valueArr = $scope.array[ar].SubjectCount.map(function (item) { return item.Selected });
            var isDuplicate = valueArr.some(function (item, idx) {
                return valueArr.indexOf(item) != idx && item != ''
            });
            if (isDuplicate) {
                return false;
            }
            if ((ar + 1) == $scope.array.length) {
                return DuplicateResult;
            }
        }
    }

    $.validate({
        form: '#FormStudentGE',
        validateOnBlur: false, // disable validation when input looses focus
        errorMessagePosition: 'top', // Instead of 'inline' which is default
        scrollToTopOnError: false, // Set this property to true on longer forms
        onSuccess: function ($form) {
            if ($scope.CheckValidationForGE()) {
                for (var ar = 0; ar < $scope.array.length; ar++) {
                    var index = $scope.StudentData.findIndex(p => p.SeatNumber == $scope.array[ar].SeatNumber);
                    if (index != -1 && index != undefined && index != '') {
                        var PositionData = $scope.array[ar].SubjectCount.map(function (el) { return el.Position; });
                        var GE_Avlbl_Position = PositionData.filter(function (e) { return e != '' });
                        //Check Is There any Blank Code In Student, If there Add for Available to add number
                        for (var prop = 0; prop < Object.keys($scope.StudentData[index]).length; prop++) {
                            var strCode = 'Code' + (prop + 1)
                            if ($scope.StudentData[index][strCode] != undefined) {
                                if ($scope.StudentData[index][strCode] == '') {
                                    GE_Avlbl_Position.push(strCode);
                                } else {
                                    continue;
                                }
                            } else {
                                break;
                            }
                        }
                        //remove duplicate if any
                        var uniq_GE_Avlbl_Position = GE_Avlbl_Position.reduce(function (a, b) {
                            if (a.indexOf(b) < 0) a.push(b);
                            return a;
                        }, []);

                        var To_Add_GE_In_CSV = $scope.array[ar].SubjectCount.filter(function (e) { return e.Selected != '' });

                        if (To_Add_GE_In_CSV > uniq_GE_Avlbl_Position.length) {
                            alert('Add More Code Field')
                        } else {
                            //clear all existing data from available position
                            for (var rm = 0; rm < uniq_GE_Avlbl_Position.length; rm++) {
                                //Clear subjects property here i.e. InternalMarkc1='' if necessary
                                $scope.StudentData[index][uniq_GE_Avlbl_Position[rm]] = '';
                            }

                            for (var TA = 0; TA < To_Add_GE_In_CSV.length; TA++) {
                                if (!$.isNumeric(To_Add_GE_In_CSV[TA].Selected)) {
                                    $scope.StudentData[index][uniq_GE_Avlbl_Position[0]] = To_Add_GE_In_CSV[TA].Selected;
                                    uniq_GE_Avlbl_Position.splice(0, 1);
                                }
                            }
                        }
                    }
                    console.log($scope.StudentData[index])
                    if ($scope.array.length == (ar + 1)) {
                        if ($scope.StudentData.length == TotalCountOfStudent) {
                            CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
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
                        } else {
                            alert("Students record are missing! Please try again")
                            $route.reload();
                        }
                    }
                }
            } else {
                toastr.error("Duplicate Subject names on same student, Please correct!", "", {
                    positionClass: "toast-bottom-right",
                });
            }
        }
    });

    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }


    $scope.IsSubjectGeneralElective = function (subjectCode) {
        var _IndexOfDept = $scope.SubjectData.findIndex(x => x.specialisationCode === $("#Specialization option:selected")[0].attributes[1].nodeValue);
        if (_IndexOfDept >= 0) {
            var _IndexOfSubject = $scope.SubjectData[_IndexOfDept].paperDetails.findIndex(x => x.paperCode === subjectCode);
            if (_IndexOfSubject >= 0) {
                if ($scope.SubjectData[_IndexOfDept].paperDetails[_IndexOfSubject].paperType.toLowerCase() == 'practical') {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    $scope.GetGECount = function () {
        var GeCountData = 0;
        if ($scope.SubjectData == undefined) {
            return 0;
        }
        for (var j = 0; j < $scope.SubjectData.length; j++) {
            if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                    if ($scope.SubjectData[j].paperDetails[k].paperType.toLowerCase() == 'practical') {
                        GeCountData++;
                    }
                }
            }
            if ((j + 1) == $scope.SubjectData.length) {
                return GeCountData;
            }
        }
    }

    var plusCount = 1;
    $scope.AddGEWhichAreNotInCSV = function (ExistingArray) {
        var drpToShow = $scope.GetGECount();
        var LogicalReqDrp = 0;

        if (ExistingArray.length == drpToShow) {
            return ExistingArray;
        } else if (ExistingArray.length > drpToShow) {
            return ExistingArray;
        } else {

            AddingBlankDropDown: for (var AddGE = 0; AddGE < drpToShow; AddGE++) {
                if (ExistingArray.length < drpToShow) {
                    var PositionDetails = {};
                    PositionDetails.Position = '';
                    var Randomno = new Date().valueOf();
                    PositionDetails.Selected = (Randomno + plusCount);
                    PositionDetails.Code = '';
                    plusCount++;
                    ExistingArray.push(PositionDetails);
                } else {
                    return ExistingArray;
                }
            }
            return ExistingArray;
        }

        for (var j = 0; j < $scope.SubjectData.length; j++) {
            if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                    if ($scope.SubjectData[j].paperDetails[k].isElective.toLowerCase() == 'yes') {
                        var index = ExistingArray.findIndex(p => p.Code == $scope.SubjectData[j].paperDetails[k].code);
                        if (index == -1) {
                            var PositionDetails = {};
                            PositionDetails.Position = '';
                            PositionDetails.Selected = '';
                            PositionDetails.Code = $scope.SubjectData[j].paperDetails[k].code;
                            ExistingArray.push(PositionDetails)
                        }
                    }
                }
            }
            if ((j + 1) == $scope.SubjectData.length) {
                return ExistingArray;
            }
        }
    }

    $scope.array = [];
    $scope.StudentData = [];
    var InternalMinPassingMark = 0;
    var InternalMaxMark = 0;
    var TotalCountOfStudent;
    $.validate({
        form: '#InternalMarkEntry',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            CallAPI('User/GetPaperList?Course=' + $scope.InternalMark.stream + '&specialization=' + $('#course').val() + '&sem=' + $('#semester').val() + '', 'GET', '').done(function (SubjectData) {
                $scope.SubjectData = SubjectData;
                $scope.TotalGeneralElective = [];
                for (var j = 0; j < $scope.SubjectData.length; j++) {
                    if ($scope.SubjectData[j].specialisationCode.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                        for (var k = 0; k < $scope.SubjectData[j].paperDetails.length; k++) {
                            if ($scope.SubjectData[j].paperDetails[k].paperType.toLowerCase() == 'practical') {
                                $scope.TotalGeneralElective.push($scope.SubjectData[j].paperDetails[k].code);
                            }
                        }
                    }
                }
                GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv').done(function (dataresponse) {
                    try {
                        $scope.StudentData = csvJSON(dataresponse);
                        TotalCountOfStudent = $scope.StudentData.length;
                    } catch (e) {
                        console.log(e)
                    }
                    if ($scope.StudentData.length > 0) {
                        $scope.SelectedSpecializaion = $("#Specialization option:selected")[0].attributes[1].nodeValue.toUpperCase();
                        $('#InternalMarkEntryForm').css('display', 'block');
                        $scope.MaxSubjectCount = 0;
                        $scope.array = [];
                        for (var st = 0; st < $scope.StudentData.length; st++) {
                            if ($scope.SelectedSpecializaion == $scope.StudentData[st].Specialisation.toUpperCase()) {
                                var objArray = {};
                                objArray.FullName = $scope.StudentData[st].LastName + ' ' + $scope.StudentData[st].FirstName + ' ' + $scope.StudentData[st].FatherName + ' ' + $scope.StudentData[st].MotherName;
                                objArray.SeatNumber = $scope.StudentData[st].SeatNumber;
                                objArray.RollNumber = $scope.StudentData[st].RollNumber;
                                objArray.SubjectCount = [];
                                for (var prop = 0; prop < Object.keys($scope.StudentData[st]).length; prop++) {
                                    var strCode = 'Code' + (prop + 1)
                                    if ($scope.StudentData[st][strCode] != undefined) {
                                        if ($scope.IsSubjectGeneralElective($scope.StudentData[st][strCode]) == true) {
                                            var PositionDetails = {};
                                            PositionDetails.Position = strCode;
                                            PositionDetails.Selected = $scope.StudentData[st][strCode];
                                            PositionDetails.Code = $scope.StudentData[st][strCode];
                                            objArray.SubjectCount.push(PositionDetails);
                                        }
                                    } else {
                                        break;
                                    }
                                }
                                objArray.LogicalSeatNumberKe = $scope.StudentData[st].SeatNumber.substring(4, $scope.StudentData[i].SeatNumber.length);
                                objArray.SubjectCount = $scope.AddGEWhichAreNotInCSV(objArray.SubjectCount);
                                console.log(objArray)
                                $scope.array.push(objArray)
                            }
                        }
                        $scope.array.sort(function (a, b) {
                            return a.RollNumber - b.RollNumber;
                        });
                        $scope.$digest();
                    } else {
                        $('#InternalMarkEntryForm').css('display', 'none');
                        toastr.error("No Record found", "", {
                            positionClass: "toast-bottom-right",
                        });
                    }
                });
            });
        }
    });

    $(".Drp_Cnrtl").change(function () {
        $('#InternalMarkEntryForm').css('display', 'none')
    });


    $scope.CreateNumberArray = function (num) {
        if (num > 0) {
            return new Array(num);
        }
    }

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    var SpecializationPaperData = [];
    var SelectedSortSpecialization = '';
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
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

    $scope.OpenEntrySheet = function () {
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val() + '&PaperName=' + $("#paper option:selected").text(), '_blank');
    }
})

Examapp.controller("LadgerReportNewCtrl", ['$scope', '$location', 'MarksheetService', 'PaperService', function ($scope, $location, MarksheetService, PaperService) {
    var Records = $.parseJSON(localStorage.getItem("CurrentStudentRecord"));
    var CurrentSubjectRecord = $.parseJSON(localStorage.getItem("CurrentSubjectRecord"));

    var DistinctSpecialization = alasql('SELECT DISTINCT Specialisation FROM ?', [Records]);
    console.log(DistinctSpecialization)
    var StudentDetail_Obj = {};
    StudentDetail_Obj.Course = "BSc"
    StudentDetail_Obj.ExamType = "Regular"
    StudentDetail_Obj.Specialisation = "Regular";
    StudentDetail_Obj.Semester = 1;
    //Obj,PaperObj,HeaderDetail
    var HeaderDetail = StudentDetail_Obj;
    $scope.StudentMarksheet = [];
    for (let aj = 0; aj < Records.length; aj++) {
        try {
            throw aj;
        } catch (ii) {
            var SpecializationObj = PaperService.GetSpecilizationObj(CurrentSubjectRecord, Records[ii]['Specialisation']);
            var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(Records[ii], SpecializationObj, StudentDetail_Obj);
            MarksheetObjFinal.Semester = StudentDetail_Obj.Semester;
            Records[ii].TotalCredits = MarksheetObjFinal._FinalTotalCredit;
            Records[ii].GPA = MarksheetObjFinal._FinalTotalGradePoint;
            Records[ii].OverallGrade = MarksheetObjFinal._FinalPercentageGrade;
            Records[ii].GrandTotal = MarksheetObjFinal.TotalAllSubjectMark;
            Records[ii].Result = MarksheetObjFinal.FailedCredits;
            Records[ii].Remarks = "";
            Records[ii].Percentage = MarksheetObjFinal._FinalPercentage;
            for (var PaperIndex = 0; PaperIndex < 10; PaperIndex++) {
                var strCode = "Code" + (PaperIndex + 1);
                var PaperTitle = "PaperTitle" + (PaperIndex + 1);
                var PaperType = "PaperType" + (PaperIndex + 1);
                var IsElective = "IsElective" + (PaperIndex + 1);
                var InternalPassingMarks = "InternalPassingMarks" + (PaperIndex + 1);
                var ExternalPassingMarks = "ExternalPassingMarks" + (PaperIndex + 1);
                var ExternalTotal = "ExternalTotal" + (PaperIndex + 1);
                var InternalTotalMarks = "InternalTotalMarks" + (PaperIndex + 1);
                var PracticalMaxMarks = "PracticalMaxMarks" + (PaperIndex + 1);
                var Attempt = "Attempt" + (PaperIndex + 1);
                var Total = "Total" + (PaperIndex + 1);
                var Grade = "Grade" + (PaperIndex + 1);
                if (Records[ii][strCode] != undefined) {
                    if (Records[ii][strCode] != "") {
                        var SubjectIndex = MarksheetObjFinal.SubjectDetail.findIndex(x => x.code == Records[ii][strCode]);
                        if (SubjectIndex != -1) {
                            Records[ii][PaperTitle] = MarksheetObjFinal.SubjectDetail[SubjectIndex].paperTitle;
                            if (MarksheetObjFinal.SubjectDetail[SubjectIndex].paperType.toLowerCase() == 'theory') {
                                Records[ii][PaperType] = 'TH';
                            } else {
                                Records[ii][PaperType] = 'PR';
                            }
                            //Records[ii][PaperType]=MarksheetObjFinal.SubjectDetail[SubjectIndex].paperType;
                            Records[ii][IsElective] = MarksheetObjFinal.SubjectDetail[SubjectIndex].isElective;
                            Records[ii][InternalPassingMarks] = MarksheetObjFinal.SubjectDetail[SubjectIndex].theoryInternalPassing;
                            Records[ii][ExternalPassingMarks] = MarksheetObjFinal.SubjectDetail[SubjectIndex].theoryExternalPassing;
                            Records[ii][ExternalTotal] = MarksheetObjFinal.SubjectDetail[SubjectIndex]._ExternalTotal;
                            Records[ii][InternalTotalMarks] = MarksheetObjFinal.SubjectDetail[SubjectIndex].theoryInternalMax;
                            //Records[ii][InternalTotalMarks]=MarksheetObjFinal.SubjectDetail[SubjectIndex].theoryInternalMax;
                            Records[ii][PracticalMaxMarks] = MarksheetObjFinal.SubjectDetail[SubjectIndex].practicalMaxMarks;
                            Records[ii][Attempt] = 1;
                            Records[ii][Total] = MarksheetObjFinal.SubjectDetail[SubjectIndex]._FinalTotalMarks;
                            Records[ii][Grade] = MarksheetObjFinal.SubjectDetail[SubjectIndex]._Grade;
                            //$scope.StudentMarksheet.push(Records[ii]);
                        } else {
                            alert('Error in finding subject for the Seat Number ' + Records[ii].SeatNumber)
                        }
                    } else {
                        continue;
                    }
                } else {
                    break;
                }
            }
            if ((ii + 1) == (Records.length)) {
                console.log($scope.StudentMarksheet)
                SaveExamResultTodb('Exam/SaveMarksDetailsInDb?admissionYear=' + CurrentYear + '&stream=HTM&examName=SemetereExam&examType=External&semester=4', angular.toJson(Records)).done(function (respo) {
                    if (respo.isSuccess) {

                    } else {

                    }
                });
            }
        }
    }
}])

Examapp.controller("StudentResultCtrl", ['$scope', 'MarksheetService', 'PaperService', function ($scope, MarksheetService, PaperService) {
    $("#txt_SeatNumber").focus();
    $('#SeatNumberForms').submit(function (event) {
        $('#tbl_StudentResult').css('display', 'none');
        $('#row_error').css('display', 'none');

        var seatNumber = $.trim($('#txt_SeatNumber').val());
        var blockNumbers = JSON.parse(localStorage.getItem("Configuration")) ? JSON.parse(localStorage.getItem("Configuration")).blockedStudents : [];
        var displayResult = JSON.parse(localStorage.getItem("Configuration")) ? JSON.parse(localStorage.getItem("Configuration")).displayResult : {};
        var resultDeclarationDate = JSON.parse(localStorage.getItem("Configuration")) ? JSON.parse(localStorage.getItem("Configuration")).resultDeclarationDate : "";
        var validityDate = new Date(resultDeclarationDate);
        $scope.resultDeclarationDate = resultDeclarationDate;

        if (blockNumbers.includes(seatNumber)) {
            alert("Your result is reserved, please contact Examiniation Department.");
            return;
        }

        $("#btn_GetResult").prop('disabled', 'disabled');
        document.getElementById("btn_GetResult").innerHTML = '<i class="fa fa-spinner fa-spin"></i> Loading Result';

        var StudentDetailObj = MarksheetService.GetObjectFromSeatNumber($('#txt_SeatNumber').val());
        if (StudentDetailObj != undefined && StudentDetailObj != null) {
            if ((StudentDetailObj.Semester == 1 && displayResult.semester1 == true) ||
                (StudentDetailObj.Semester == 2 && displayResult.semester2 == true) ||
                (StudentDetailObj.Semester == 3 && displayResult.semester3 == true) ||
                (StudentDetailObj.Semester == 4 && displayResult.semester4 == true) ||
                (StudentDetailObj.Semester == 5 && displayResult.semester5 == true) ||
                (StudentDetailObj.Semester == 6 && displayResult.semester6 == true)
            ) {
                CallAPI("User/GetPaperList?Course=" + StudentDetailObj.Course + "&specialization=" + StudentDetailObj.Specialisation + "&sem=" + StudentDetailObj.Semester).done(function (PaperList) {
                    GetCsvToJsonData('File/Download/Data/SVT?fileName=' + StudentDetailObj.Course + '-' + StudentDetailObj.Specialisation + '_sem' + StudentDetailObj.Semester + '_2019_' + StudentDetailObj.ExamType + '.csv').done(function (dataresponse) {
                        try {
                            var StudentData = csvJSON(dataresponse);
                        } catch (e) {
                            console.log(e)
                        }
                        if (StudentData.length > 0) {
                            var index = StudentData.findIndex(p => p.SeatNumber == $.trim($('#txt_SeatNumber').val()));
                            if (index != undefined && index >= 0) {
                                var SpecializationObj = PaperService.GetSpecilizationObj(PaperList, StudentData[index]['Specialisation']);
                                console.log(SpecializationObj);
                                var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(StudentData[index], SpecializationObj, StudentDetailObj);
                                MarksheetObjFinal.Semester = StudentDetailObj.Semester;
                                console.log(MarksheetObjFinal);
                                $scope.StudentDetailDispl = MarksheetObjFinal;
                                $scope.StudentDetailDispl.Backlog = StudentData[index].Backlog;
                                $('#tbl_StudentResult').css('display', '');
                                $('#row_error').css('display', 'none');
                                document.getElementById("btn_GetResult").innerHTML = 'Get Result';
                                $("#btn_GetResult").prop('disabled', '');
                                $scope.$digest();
                            } else {
                                document.getElementById("btn_GetResult").innerHTML = 'Get Result';
                                $("#btn_GetResult").prop('disabled', '');
                                $('#tbl_StudentResult').css('display', 'none');
                                $('#row_error').css('display', '');
                                $('#Error_Text').html('Seat Number Not Found');
                            }
                        }
                    });
                });
            } else {
                document.getElementById("btn_GetResult").innerHTML = 'Get Result';
                $("#btn_GetResult").prop('disabled', '');
                if (StudentDetailObj.Semester > 6) {
                    alert('Invalid Seat Number')
                } else {
                    alert('Result for the semester ' + StudentDetailObj.Semester + ' is yet to declare, Please contact Exam cell.')
                }
            }
        } else {
            document.getElementById("btn_GetResult").innerHTML = 'Get Result';
            $("#btn_GetResult").prop('disabled', '');
            $('#tbl_StudentResult').css('display', 'none');
            $('#row_error').css('display', '');
            $('#Error_Text').html('Invalid Seat Number!')
        }
        $("#txt_SeatNumber").focus();
    });
}]);

Examapp.controller("StudentResultValidationCtrl", ['$scope', 'MarksheetService', 'PaperService', function ($scope, MarksheetService, PaperService) {
    $("#txt_SeatNumber").focus();
    $('#SeatNumberForms').submit(function (event) {
        $('#tbl_StudentResult').css('display', 'none');
        $('#row_error').css('display', 'none');

        var seatNumber = $.trim($('#txt_SeatNumber').val());
        var blockNumbers = JSON.parse(localStorage.getItem("Configuration")) ? JSON.parse(localStorage.getItem("Configuration")).blockedStudents : [];
        if (blockNumbers.includes(seatNumber)) {
            alert("Your result is reserved, please contact Examiniation Department.");
            return;
        }

        $("#btn_GetResult").prop('disabled', 'disabled');
        document.getElementById("btn_GetResult").innerHTML = '<i class="fa fa-spinner fa-spin"></i> Loading Result';

        var StudentDetailObj = MarksheetService.GetObjectFromSeatNumber($('#txt_SeatNumber').val());
        if (StudentDetailObj != undefined && StudentDetailObj != null) {
            if (StudentDetailObj.Semester >= 1) {
                CallAPI("User/GetPaperList?Course=" + StudentDetailObj.Course + "&specialization=" + StudentDetailObj.Specialisation + "&sem=" + StudentDetailObj.Semester).done(function (PaperList) {
                    GetCsvToJsonData('File/Download/Data/SVT?fileName=' + StudentDetailObj.Course + '-' + StudentDetailObj.Specialisation + '_sem' + StudentDetailObj.Semester + '_2019_' + StudentDetailObj.ExamType + '.csv').done(function (dataresponse) {
                        try {
                            var StudentData = csvJSON(dataresponse);
                        } catch (e) {
                            console.log(e)
                        }
                        if (StudentData.length > 0) {
                            var index = StudentData.findIndex(p => p.SeatNumber == $.trim($('#txt_SeatNumber').val()));
                            if (index != undefined && index >= 0) {
                                var SpecializationObj = PaperService.GetSpecilizationObj(PaperList, StudentData[index]['Specialisation']);
                                //console.log(SpecializationObj);
                                var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(StudentData[index], SpecializationObj, StudentDetailObj);
                                MarksheetObjFinal.Semester = StudentDetailObj.Semester;
                                //console.log(MarksheetObjFinal);
                                $scope.StudentDetailDispl = MarksheetObjFinal;
                                $scope.StudentDetailDispl.Backlog = StudentData[index].Backlog;
                                $('#tbl_StudentResult').css('display', '');
                                $('#row_error').css('display', 'none');
                                document.getElementById("btn_GetResult").innerHTML = 'Get Result';
                                $("#btn_GetResult").prop('disabled', '');
                                $scope.$digest();
                            } else {
                                document.getElementById("btn_GetResult").innerHTML = 'Get Result';
                                $("#btn_GetResult").prop('disabled', '');
                                $('#tbl_StudentResult').css('display', 'none');
                                $('#row_error').css('display', '');
                                $('#Error_Text').html('Seat Number Not Found');
                            }
                        }
                    });
                });
            } else {
                document.getElementById("btn_GetResult").innerHTML = 'Get Result';
                $("#btn_GetResult").prop('disabled', '');
                if (StudentDetailObj.Semester > 6) {
                    alert('Invalid Seat Number')
                } else {
                    alert('Result for the semester ' + StudentDetailObj.Semester + ' is yet to declare, Please contact Exam cell.')
                }
            }
        } else {
            document.getElementById("btn_GetResult").innerHTML = 'Get Result';
            $("#btn_GetResult").prop('disabled', '');
            $('#tbl_StudentResult').css('display', 'none');
            $('#row_error').css('display', '');
            $('#Error_Text').html('Invalid Seat Number!')
        }
        $("#txt_SeatNumber").focus();
    });

    $('#HallTicketForms').submit(function (event) {
    });
}]);

Examapp.controller("ConvocationReportCtrl", function ($scope, StudentCsvFile, PaperService, MarksheetService, $q, $rootScope, TopperDataService) {
    $scope.PageTitle = 'Convocation Report';

    $.validate({
        form: '#PageViewForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            $("#sampleTable tbody tr").remove();
            $("#sampleTable tbody").append("<tr><td>Loading...</td></tr>");
            CallAPI("/exam/GetConvocationList?examType=" + $('#course').val() + "&specialisationCode=" + $('#Specialization').val()).done(function (response) {
                $("#sampleTable tbody tr").remove();
                for (i = 0; i < response.length; i++) {
                    var row = "<tr><td>" + (i + 1) + "</td><td>" + response[i].Crn + "</td><td>" + response[i].Specialisation + "</td><td>" + response[i].StudentName + "</td><td>" + response[i].WeightedPercentage.toFixed(2) + "</td><td>" + response[i].Grade + "</td></tr>";
                    $("#sampleTable tbody").append(row);
                }
            });
        }
    });


});

Examapp.controller("AggregateTopperReportCtrl", function ($scope, StudentCsvFile, PaperService, MarksheetService, $q, $rootScope, TopperDataService) {
    $scope.PageTitle = 'Aggregate Toppers Report';

    $.validate({
        form: '#PageViewForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            $("#sampleTable tbody tr").remove();
            CallAPI("User/GetPaperList?Course=bsc&specialization=regular&sem=1").done(function (papers) {
                CallAPI("/exam/GetCommonMarksheetResponse?examType=" + $('#course').val() + "&specialisationCode=" + $('#Specialization').val() + "&sem=All").done(function (response) {

                    if (response != null && response != undefined && response.length > 0) {
                        for (var i = 0; i < response.length; i++) {
                            response[i]._FinalTotalCredits = 0;
                            response[i]._FinalTotalMarksTiDisp = 0;
                            response[i]._FinalObtainedMarks = 0;
                            response[i]._FinalWeightedMarks = 0;
                            response[i]._FinalWeightedPercentage = 0;
                            response[i]._FinalGrade = 0;
                            response[i]._FinalCountGradePoint = 0;
                            response[i]._FinalGradePoint = 0;
                            response[i].AggregateGradePoint = 0;
                            response[i].AggregateTotalGradePoint = 0;


                            for (var j = 0; j < response[i].Papers.length; j++) {
                                var flotCredit = parseFloat(response[i].Papers[j].Credit) || 0;
                                var fltExternalmaxmark = parseFloat(response[i].Papers[j].externalMaxMarks) || 0;
                                var fltInternalTotalMarks = parseFloat(response[i].Papers[j].InternalTotalMarks) || 0;
                                var fltPracticalMaxMarks = parseFloat(response[i].Papers[j].PracticalMaxMarks) || 0;
                                var fltInternalMarksObtained = parseFloat(response[i].Papers[j].InternalMarksObtained) || 0;
                                var fltPracticalMarksObtained = parseFloat(response[i].Papers[j].PracticalMarksObtained) || 0;
                                var fltExternalTotalMarks = parseFloat(response[i].Papers[j].ExternalTotalMarks) || 0;
                                response[i].Papers[j]._TotalMarksdisp = (fltExternalmaxmark + fltInternalTotalMarks + fltPracticalMaxMarks);
                                response[i].Papers[j]._ObtainedMarks = Math.round(fltInternalMarksObtained + fltPracticalMarksObtained + fltExternalTotalMarks);
                                response[i].Papers[j]._WeightedMarks = ((response[i].Papers[j].Credit * response[i].Papers[j]._ObtainedMarks));

                                if (response[i].Papers[j]._ObtainedMarks >= 90)
                                    response[i].Papers[j].GradePoint = "10";
                                if (response[i].Papers[j]._ObtainedMarks >= 80 && response[i].Papers[j]._ObtainedMarks < 90)
                                    response[i].Papers[j].GradePoint = "9." + ((Math.round(response[i].Papers[j]._ObtainedMarks)) % 10) + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 70 && response[i].Papers[j]._ObtainedMarks < 80)
                                    response[i].Papers[j].GradePoint = "8." + ((Math.round(response[i].Papers[j]._ObtainedMarks)) % 10) + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 60 && response[i].Papers[j]._ObtainedMarks < 70)
                                    response[i].Papers[j].GradePoint = "7." + ((Math.round(response[i].Papers[j]._ObtainedMarks)) % 10) + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 55 && response[i].Papers[j]._ObtainedMarks < 60)
                                    response[i].Papers[j].GradePoint = "6." + ((Math.round(response[i].Papers[j]._ObtainedMarks)) % 5) * 2 + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 50 && response[i].Papers[j]._ObtainedMarks < 55)
                                    response[i].Papers[j].GradePoint = "5." + (((Math.round(response[i].Papers[j]._ObtainedMarks)) % 10) + 5) + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 45 && response[i].Papers[j]._ObtainedMarks < 50)
                                    response[i].Papers[j].GradePoint = "5." + ((Math.round(response[i].Papers[j]._ObtainedMarks)) % 5) + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 40 && response[i].Papers[j]._ObtainedMarks < 45)
                                    response[i].Papers[j].GradePoint = "4." + ((Math.round(response[i].Papers[j]._ObtainedMarks)) % 5) * 2 + "0";
                                if (response[i].Papers[j]._ObtainedMarks >= 0 && response[i].Papers[j]._ObtainedMarks < 40)
                                    response[i].Papers[j].GradePoint = "0";
                                //Credit
                                response[i].Papers[j].fltGradePoint = parseFloat(response[i].Papers[j].GradePoint) || 0;

                                response[i].Papers[j].TOTALGRADEPOINT = response[i].Papers[j].fltGradePoint * flotCredit;

                                response[i].Papers[j].TOTALGRADEPOINT = parseFloat(response[i].Papers[j].TOTALGRADEPOINT).toFixed(2);

                                response[i].AggregateGradePoint = parseFloat(response[i].AggregateGradePoint) + response[i].Papers[j].fltGradePoint;
                                response[i].AggregateTotalGradePoint = parseFloat(response[i].AggregateTotalGradePoint) + parseFloat(response[i].Papers[j].TOTALGRADEPOINT);

                                response[i]._FinalCountGradePoint = parseFloat(response[i]._FinalCountGradePoint) + parseFloat(response[i].Papers[j].TOTALGRADEPOINT);

                                response[i]._FinalTotalCredits += flotCredit;
                                response[i]._FinalTotalMarksTiDisp += response[i].Papers[j]._TotalMarksdisp;
                                response[i]._FinalObtainedMarks += response[i].Papers[j]._ObtainedMarks;
                                response[i]._FinalWeightedMarks += response[i].Papers[j]._WeightedMarks;

                                response[i]._FinalWeightedPercentage = (response[i]._FinalWeightedMarks / response[i]._FinalTotalCredits).toFixed(2);
                                response[i]._FinalGradePoint = (response[i]._FinalCountGradePoint / response[i]._FinalTotalCredits).toFixed(2);

                                if (response[i].Papers[j].RetryCount > 1) {
                                    response[i].AppearedInATKT = true;
                                }

                                if (response[i]._FinalWeightedPercentage < 40) response[i]._FinalGrade = 'F';
                                else if (response[i]._FinalWeightedPercentage >= 40 && response[i]._FinalWeightedPercentage < 45) response[i]._FinalGrade = 'P';
                                else if (response[i]._FinalWeightedPercentage >= 45 && response[i]._FinalWeightedPercentage < 50) response[i]._FinalGrade = 'C';
                                else if (response[i]._FinalWeightedPercentage >= 50 && response[i]._FinalWeightedPercentage < 55) response[i]._FinalGrade = 'B';
                                else if (response[i]._FinalWeightedPercentage >= 55 && response[i]._FinalWeightedPercentage < 60) response[i]._FinalGrade = 'B+';
                                else if (response[i]._FinalWeightedPercentage >= 60 && response[i]._FinalWeightedPercentage < 70) response[i]._FinalGrade = 'A';
                                else if (response[i]._FinalWeightedPercentage >= 70 && response[i]._FinalWeightedPercentage < 80) response[i]._FinalGrade = 'A+';
                                else if (response[i]._FinalWeightedPercentage >= 80 && response[i]._FinalWeightedPercentage < 90) response[i]._FinalGrade = 'O';
                                else if (response[i]._FinalWeightedPercentage >= 90 && response[i]._FinalWeightedPercentage < 101) response[i]._FinalGrade = 'O+';
                            }

                            response[i].AggregateGradePoint = parseFloat(response[i].AggregateGradePoint).toFixed(2);
                            response[i].AggregateTotalGradePoint = parseFloat(response[i].AggregateTotalGradePoint).toFixed(2);
                            if ((i + 1) == response.length) {
                                $scope.StudentData = response;

                                var arr = $scope.StudentData.sort(function (a, b) {
                                    var x = a["_FinalWeightedPercentage"]; var y = b["_FinalWeightedPercentage"];
                                    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
                                });

                                var k = 1;

                                for (var u = arr.length - 1; u >= 0; u--) {
                                    if (!arr[u].AppearedInATKT) {
                                        var row = $("<tr><td>" + arr[u].CollegeRegistrationNumber + "</td><td>" + arr[u].LastName + " " + arr[u].FirstName + " " + arr[u].FatherName + " " + arr[u].MotherName + "</td><td>" + arr[u]._FinalWeightedPercentage + "</td><td></tr>");
                                        $("#sampleTable").find("tbody").append(row);
                                        if (k == 3)
                                            break;

                                        k++;
                                    }
                                }
                            }
                        }
                    }
                });
            });
        }
    });


});

Examapp.controller("TopperReportCtrl", function ($scope, StudentCsvFile, PaperService, MarksheetService, $q, $rootScope, TopperDataService) {
    $scope.PageTitle = 'Toppers Report'
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.PageView.stream, 'semester')
    }
    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }
    function RunObject(objectOfStudent) {
        console.log(objectOfStudent)
        var FinalMainObj = [];
        for (var r = 0; r < objectOfStudent.length; r++) {
            var PassedData = objectOfStudent[r].StudentData.filter(x => x.FailedCredits == 0);
            if (PassedData.length > 0) {
                var TotalAllSubjectMark = '_FinalPercentage';
                var toppersD = sortByKey(PassedData, TotalAllSubjectMark);
                toppersD = toppersD.reverse();
                for (var ra = 0; ra < 3; ra++) {
                    if (isNaN(toppersD[ra]._FinalPercentage))
                        continue;

                    if (toppersD.length > ra) {
                        var StudentObj = {};
                        StudentObj.CollegeReg = toppersD[ra].College_Registration_No_;
                        StudentObj.Name = toppersD[ra].FullName;
                        StudentObj.Percentage = toppersD[ra]._FinalPercentage;
                        StudentObj.Rank = (ra + 1);
                        StudentObj.Spec = toppersD[ra].Specialisation;
                        StudentObj.IsRegularStudent = toppersD[ra].IsRegularStudent;
                        StudentObj.TotalAllSubjectMark = toppersD[ra].TotalAllSubjectMark;
                        StudentObj.Grade = toppersD[ra]._FinalPercentageGrade
                        StudentObj.ActualDetails = toppersD[ra];
                        StudentObj.Semester = $('#semester').val();
                        FinalMainObj.push(StudentObj);
                    }
                }
            }
        }
        $('#AddExtraSubjectForm2').css('display', '');
        console.log(FinalMainObj);
        $scope.FinalObjtoDisp = FinalMainObj;
        if (!$scope.$$phase) {
            //$digest or $apply
            $scope.$apply();
        }
    }
    function GetData(stram, course, sem, examType) {
        return $q(function (resolve, reject) {
            CallAPI("User/GetPaperList?Course=" + stram + "&specialization=" + course + "&sem=" + sem).done(function (response) {
                var CurrentSubjectRecord = response;
                StudentCsvFile.GetFileObj(stram, course, sem, CurrentYear, examType).done(function (dataresponse) {
                    try {
                        var StudentDetail_Obj = {};
                        StudentDetail_Obj.Course = stram;
                        StudentDetail_Obj.ExamType = examType;
                        StudentDetail_Obj.Semester = sem;
                        StudentDetail_Obj.RegularORHonors = course;
                        var MainObject = [];
                        var StudentData = csvJSON(dataresponse);

                        for (var speci = 0; speci < CurrentSubjectRecord.length; speci++) {
                            var StudentCurrentData = StudentData.filter(x => x.Specialisation == CurrentSubjectRecord[speci].specialisationCode);
                            var StudentSubObj = {};
                            StudentSubObj.Specilization = CurrentSubjectRecord[speci].specialisationCode;
                            StudentSubObj.StudentData = [];
                            for (let aj = 0; aj < StudentCurrentData.length; aj++) {
                                try {
                                    throw aj;
                                } catch (ii) {
                                    var SpecializationObj = PaperService.GetSpecilizationObj(CurrentSubjectRecord, StudentCurrentData[ii]['Specialisation']);
                                    var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(StudentCurrentData[ii], SpecializationObj, StudentDetail_Obj);

                                    if (!isNaN(MarksheetObjFinal._FinalPercentage))
                                        StudentSubObj.StudentData.push(MarksheetObjFinal);
                                }
                                if (aj == (StudentCurrentData.length - 1)) {
                                    MainObject.push(StudentSubObj)
                                }
                            }
                            if (speci == (CurrentSubjectRecord.length - 1)) {
                                resolve(MainObject);
                                //return 
                            }
                        }
                    } catch (e) {
                        console.log(e)
                    }
                });
            });
        });
    }

    function MergeRegularAndHonors(RegularData, HonorsData) {
        for (var rd = 0; rd < RegularData.length; rd++) {
            var HSMErge = [];
            if (RegularData[rd].Specilization == 'IDRM') {
                HSMErge = HonorsData.filter((x) => { return x.Specilization == 'IDM' || x.Specilization == 'IDRM' });
            } else {
                HSMErge = HonorsData.filter((x) => { return x.Specilization == RegularData[rd].Specilization });
            }
            RegularData[rd].StudentData = RegularData[rd].StudentData.concat(HSMErge[0].StudentData);
            if ((RegularData.length - 1) == rd) {
                RunObject(RegularData);
            }
        }
    }

    $.validate({
        form: '#PageViewForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            if ($('#course').val() == 'All') {
                GetData($('#stream').val(), 'Regular', $('#semester').val(), $('#examType').val()).then(function (resultRegular) {
                    GetData($('#stream').val(), 'Honors', $('#semester').val(), $('#examType').val()).then(function (resultHonors) {
                        MergeRegularAndHonors(resultRegular, resultHonors);
                    }, function (error) {
                        console.log(error);
                    });
                }, function (error) {
                    console.log(error);
                });
            } else {
                GetData($('#stream').val(), $('#course').val(), $('#semester').val(), $('#examType').val()).then(function (result) {
                    RunObject(result);
                });
            }
        }
    });

    $scope.PrintMeritCerificate = function () {
        localStorage.setItem('MaritCertificateData', JSON.stringify($scope.FinalObjtoDisp));
        window.open(_CommonurlUI + '/View/Certificate/Merit_Certificate.html?month=APRIL&year=' + CurrentYear, '_blank');
    }

});

Examapp.controller("GenerateMeritCertificateCtrl", function (TopperDataService, $scope) {
    var datta = JSON.parse(localStorage.getItem('MaritCertificateData'));
    console.log(datta);

    for (var i = 0; i < datta.length; i++) {
        datta[i].CurrentMonth = localStorage.getItem('CurrentMonth');
        datta[i].CurrentYear = localStorage.getItem('CurrentYear');
    }

    $scope.DisplayData = datta;
});

Examapp.controller("YearSettingCtrl", function ($scope) {
    $scope.CurrentYear = CurrentYear;
    $scope.CurrentMonth = CurrentMonth;
    var d = new Date();
    var _yr = d.getFullYear();

    var config = CallAPI("/Exam/GetGlobalConfiguration", "GET");
    config.success(function (result) {
        localStorage.setItem("Configuration", JSON.stringify(result));
        $("#ResultDate").val(result.resultDeclarationDate);
        $("#seatNumberSeed").val(result.startSeatNumber);
        $("#regenerate").prop("checked", result.regenerateSeatNumber);
        $("#generate").prop("checked", result.seatNumberGenerated);

        $scope.resultDeclarationDate = result.resultDeclarationDate;
        var tags = "";
        for (i = 0; i < result.blockedStudents.length; i++) {
            tags += result.blockedStudents[i] + ",";
        }

        $("#seatNumbers").val(tags);

        $("#semester1").prop("checked", result.displayResult.semester1);
        $("#semester2").prop("checked", result.displayResult.semester2);
        $("#semester3").prop("checked", result.displayResult.semester3);
        $("#semester4").prop("checked", result.displayResult.semester4);
        $("#semester5").prop("checked", result.displayResult.semester5);
        $("#semester6").prop("checked", result.displayResult.semester6);

        $("#HTMRegularCount").val(result.students.HTMRegularCount);
        $("#IDRMRegularCount").val(result.students.IDRMRegularCount);
        $("#FNDRegularCount").val(result.students.FNDRegularCount);
        $("#DCRegularCount").val(result.students.DCRegularCount);
        $("#ECCERegularCount").val(result.students.ECCERegularCount);
        $("#MCERegularCount").val(result.students.MCERegularCount);
        $("#TADRegularCount").val(result.students.TADRegularCount);
    });

    $.formUtils.addValidator({
        name: 'ValidateYear',
        validatorFunction: function (value, $el, config, language, $form) {
            if (value >= 2016 && value <= _yr) {
                return true;
            } else {
                return false;
            }
        },
        errorMessage: 'Invalid Year',
        errorMessageKey: 'InvalidYear'
    });

    $.validate({
        form: '#PageViewForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            localStorage.setItem('CurrentYear', $scope.CurrentYear);
            localStorage.setItem('CurrentMonth', $scope.CurrentMonth);

            CallAPI("Exam/SaveGlobalConfiguration", "POST", localStorage.getItem("Configuration"));
            CurrentYear = $scope.CurrentYear;
            CurrentMonth = $scope.CurrentMonth;
            toastr.success("Saved successfully", "", {
                positionClass: "toast-bottom-right",
            });
        }
    });

    $.formUtils.addValidator({
        name: 'ValidateConfig',
        validatorFunction: function (value, $el, config, language, $form) {

            var json = JSON.parse(localStorage.getItem("Configuration"));
            json.resultDeclarationDate = $form.find("#ResultDate").val();
            json.regenerateSeatNumber = $form.find("#regenerate").is(":checked");
            json.seatNumberGenerated = $form.find("#generate").is(":checked");
            json.startSeatNumber = $form.find("#seatNumberSeed").val();

            json.students = {};

            json.students.HTMRegularCount = $form.find("#HTMRegularCount").val();
            json.students.IDRMRegularCount = $form.find("#IDRMRegularCount").val();

            json.students.FNDRegularCount = $form.find("#FNDRegularCount").val();
            json.students.DCRegularCount = $form.find("#DCRegularCount").val();
            
            json.students.ECCERegularCount = $form.find("#ECCERegularCount").val();
            json.students.MCERegularCount = $form.find("#MCERegularCount").val();

            json.students.TADRegularCount = $form.find("#TADRegularCount").val();

            json.displayResult.semester1 = false;
            json.displayResult.semester2 = false;
            json.displayResult.semester3 = false;
            json.displayResult.semester4 = false;
            json.displayResult.semester5 = false;
            json.displayResult.semester6 = false;
            json.blockedStudents = [];
            $(".bootstrap-tagsinput span").each(function () {
                if ($(this).text() != "") {
                    json.blockedStudents.push($(this).text());
                }
            });

            $.each($(".results input[name='semester']:checked"), function () {
                switch ($(this).val()) {
                    case "1":
                        json.displayResult.semester1 = true;
                        break;
                    case "2":
                        json.displayResult.semester2 = true;
                        break;
                    case "3":
                        json.displayResult.semester3 = true;
                        break;
                    case "4":
                        json.displayResult.semester4 = true;
                        break;
                    case "5":
                        json.displayResult.semester5 = true;
                        break;
                    case "6":
                        json.displayResult.semester6 = true;
                        break;
                }
            });

            localStorage.setItem("Configuration", JSON.stringify(json));
        },
        errorMessage: 'Invalid configuration',
        errorMessageKey: 'InvalidConfig'
    });
});

Examapp.controller("SeatNumberAdjCtrl", function ($scope) {
    var strlstArray = localStorage.getItem('LastSeatNumberGenerated');
    console.log(strlstArray);
    try {
        var ab = JSON.parse(strlstArray);
        var currentYearData = ab.filter(x => x.year == CurrentYear);
        $scope.FinalData = currentYearData;
        var data = ab.filter(x => x.year == CurrentYear && x.specialization == "honors" && x.sem == 3);
        if (data.length > 0) {
            $scope.Sem3Data = data[0];
        } else {
            $scope.Sem3Data = {};
        }
        data = ab.filter(x => x.year == CurrentYear && x.specialization == "honors" && x.sem == 4);
        if (data.length > 0) {
            $scope.Sem4Data = data[0];
        } else {
            $scope.Sem4Data = {};
        }
        data = ab.filter(x => x.year == CurrentYear && x.specialization == "honors" && x.sem == 5);
        if (data.length > 0) {
            $scope.Sem5Data = data[0];
        } else {
            $scope.Sem5Data = {};
        }
        data = ab.filter(x => x.year == CurrentYear && x.specialization == "honors" && x.sem == 6);
        if (data.length > 0) {
            $scope.Sem6Data = data[0];
        } else {
            $scope.Sem6Data = {};
        }
    } catch (e) {

    }

    $scope.SaveData = function () {
        var finaldata = [];
        finaldata.push($scope.Sem3Data);
        finaldata.push($scope.Sem4Data);
        finaldata.push($scope.Sem5Data);
        finaldata.push($scope.Sem6Data);
        console.log($scope.Sem3Data);
        console.log($scope.Sem4Data);
        console.log($scope.Sem5Data);
        console.log($scope.Sem6Data);
        localStorage.setItem('LastSeatNumberGenerated', JSON.stringify(finaldata));
        toastr.success("Saved successfully", "", {
            positionClass: "toast-bottom-right",
        });
    }
});

Examapp.controller("GenerateTranscriptCertificateCtrl", function () {

})

Examapp.factory('TopperDataService', function () {
    return {
        data: [],
        updateData: function (objData) {
            this.data = objData;
        }
    };
});

Examapp.controller("ProcessStudentToDBCtrl", ['$scope', '$location', 'MarksheetService', 'PaperService', function ($scope, $location, MarksheetService, PaperService) {

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.UploadMarks.stream, 'semester')
    }

    document.getElementById('file').addEventListener('change', upload, false);

    function browserSupportFileUpload() {
        var isCompatible = false;
        if (window.File && window.FileReader && window.FileList && window.Blob) {
            isCompatible = true;
        }
        return isCompatible;
    }
    var ImportedData = [];
    function upload(evt) {
        if (!browserSupportFileUpload()) {
            alert('The File APIs are not fully supported in this browser!');
        } else {
            var file = evt.target.files[0];
            var reader = new FileReader();
            reader.readAsText(file);
            reader.onload = function (event) {
                var csvData = event.target.result;
                ImportedData = $.csv.toObjects(csvData);
                if (ImportedData && ImportedData.length > 0) {
                    console.log(ImportedData)
                } else {
                    alert('No data to import!');
                }
            };
            reader.onerror = function () {
                alert('Unable to read ' + file.fileName);
            };
        }
    }

    $.validate({
        form: '#FormProcessStudentToDB',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            $('#btnSubmitProcess').attr('disabled', true);
            $("#btnSubmitProcess").html('Processing...');
            console.log(ImportedData);
            var StudentDetail_Obj = {};
            StudentDetail_Obj.Course = $('#course').val();
            StudentDetail_Obj.ExamType = $('#examType').val();
            StudentDetail_Obj.Specialisation = $('#specialization').val();
            StudentDetail_Obj.Semester = $('#semester').val();
            var Errors = [];
            var TotalPassedStudent = [];
            var TotalFailedStudent = [];

            CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val() + "_Aggregate").done(function (response) {
                var CurrentSubjectRecord = response;
                var Records = ImportedData;
                for (let aj = 0; aj < Records.length; aj++) {
                    try {
                        throw aj;
                    } catch (ii) {
                        var SpecializationObj = PaperService.GetSpecilizationObj(CurrentSubjectRecord, Records[ii]['Specialisation']);
                        var MarksheetObjFinal = MarksheetService.GetMarksheetObjectFromCsvObj(Records[ii], SpecializationObj, StudentDetail_Obj);
                        //if(Records[ii].SeatNumber=='5509' || Records[ii].SeatNumber==5509){
                        //    debugger
                        //}
                        var CommonStudentDetails = {};
                        CommonStudentDetails.CollegeRegistrationNumber = Records[ii].College_Registration_No_;
                        CommonStudentDetails.SeatNumber = Records[ii].SeatNumber;
                        CommonStudentDetails.RollNumbers = Records[ii].RollNumber;
                        CommonStudentDetails.LastName = Records[ii].LastName;
                        CommonStudentDetails.FirstName = Records[ii].FirstName;
                        CommonStudentDetails.FatherName = Records[ii].FatherName;
                        CommonStudentDetails.MotherName = Records[ii].MotherName;
                        CommonStudentDetails.PRN = Records[ii].PRN.replace(new RegExp("'", 'g'), "");
                        CommonStudentDetails.Course = MarksheetObjFinal.Course;
                        //if(MarksheetObjFinal.Specialisation=='HTM')
                        CommonStudentDetails.Specialisation = MarksheetObjFinal.Specialisation;
                        CommonStudentDetails.SubCourse = '';
                        CommonStudentDetails.ExamType = StudentDetail_Obj.ExamType;
                        CommonStudentDetails.Semester = StudentDetail_Obj.Semester;
                        CommonStudentDetails.Date = '';
                        CommonStudentDetails.TotalCredits = MarksheetObjFinal._FinalTotalCredit;
                        CommonStudentDetails.GPA = MarksheetObjFinal._FinalTotalGradePoint;
                        CommonStudentDetails.OverallGrade = MarksheetObjFinal._FinalPercentageGrade;
                        CommonStudentDetails.GrandTotal = MarksheetObjFinal.TotalAllSubjectMark;
                        CommonStudentDetails.Result = '';
                        CommonStudentDetails.Remarks = "";
                        CommonStudentDetails.Percentage = MarksheetObjFinal._FinalPercentage;

                        var FailedStudentSubject = MarksheetObjFinal.SubjectDetail.filter(x => x._Status != '');
                        var PassedStudentSubject = MarksheetObjFinal.SubjectDetail.filter(x => x._Status == '');

                        if (PassedStudentSubject.length > 0) {
                            var currentPassedStudent = angular.copy(CommonStudentDetails);
                            for (var PassedStud = 0; PassedStud < PassedStudentSubject.length; PassedStud++) {
                                var PaperAppeared = 'Paper' + (PassedStud + 1) + 'Appeared';
                                var Code = 'Code' + (PassedStud + 1);
                                var PaperTitle = 'PaperTitle' + (PassedStud + 1);
                                var PaperType = 'PaperType' + (PassedStud + 1);
                                var IsElective = 'IsElective' + (PassedStud + 1);
                                var InternalPassingMarks = 'InternalPassingMarks' + (PassedStud + 1);
                                var ExternalPassingMarks = 'ExternalPassingMarks' + (PassedStud + 1);
                                var InternalTotalMarks = 'InternalTotalMarks' + (PassedStud + 1);
                                var PracticalMaxMarks = 'PracticalMaxMarks' + (PassedStud + 1);
                                var Attempt = 'Attempt' + (PassedStud + 1);
                                var InternalC = 'InternalC' + (PassedStud + 1);
                                var ExternalSection1C = 'ExternalSection1C' + (PassedStud + 1);
                                var ExternalSection2C = 'ExternalSection2C' + (PassedStud + 1);
                                var ExternalTotalC = 'ExternalTotalC' + (PassedStud + 1);
                                var PracticalMarksC = 'PracticalMarksC' + (PassedStud + 1);
                                var GraceC = 'GraceC' + (PassedStud + 1);
                                var Total = 'Total' + (PassedStud + 1);
                                var Grade = 'Grade' + (PassedStud + 1);
                                var Credit = 'Credit' + (PassedStud + 1);
                                var Grace = 'Grace' + (PassedStud + 1);
                                var ExternalMaxMarks = 'ExternalMaxMarks' + (PassedStud + 1);

                                currentPassedStudent[PaperAppeared] = '';
                                currentPassedStudent[Code] = PassedStudentSubject[PassedStud].code;
                                currentPassedStudent[PaperTitle] = PassedStudentSubject[PassedStud].paperTitle;
                                if (PassedStudentSubject[PassedStud].paperType.toLowerCase() == 'theory') {
                                    currentPassedStudent[PaperType] = 'TH';
                                } else {
                                    currentPassedStudent[PaperType] = 'PR';
                                }
                                currentPassedStudent[IsElective] = PassedStudentSubject[PassedStud].isElective;
                                currentPassedStudent[InternalPassingMarks] = PassedStudentSubject[PassedStud].theoryInternalPassing;
                                currentPassedStudent[ExternalPassingMarks] = PassedStudentSubject[PassedStud].theoryExternalPassing;
                                currentPassedStudent[InternalTotalMarks] = PassedStudentSubject[PassedStud].theoryInternalMax;
                                currentPassedStudent[PracticalMaxMarks] = PassedStudentSubject[PassedStud].practicalMaxMarks;
                                currentPassedStudent[Attempt] = 1;
                                currentPassedStudent[InternalC] = PassedStudentSubject[PassedStud]._FloatInternalMark;
                                currentPassedStudent[ExternalSection1C] = PassedStudentSubject[PassedStud]._FloatSection1Mark;
                                currentPassedStudent[ExternalSection2C] = PassedStudentSubject[PassedStud]._FloatSection2Mark;
                                currentPassedStudent[ExternalTotalC] = PassedStudentSubject[PassedStud]._ExternalTotal;
                                currentPassedStudent[PracticalMarksC] = PassedStudentSubject[PassedStud]._FloatPracticalMark;
                                currentPassedStudent[GraceC] = PassedStudentSubject[PassedStud]._FloatGraceMark;
                                currentPassedStudent[Total] = PassedStudentSubject[PassedStud]._FinalTotalMarks;
                                currentPassedStudent[Grade] = PassedStudentSubject[PassedStud]._Grade;
                                currentPassedStudent[Credit] = PassedStudentSubject[PassedStud].credits;
                                var sec1 = parseInt(PassedStudentSubject[PassedStud].theoryExternalSection1Max) || 0;
                                var sec2 = parseInt(PassedStudentSubject[PassedStud].theoryExternalSection2Max) || 0;
                                currentPassedStudent[ExternalMaxMarks] = (sec1 + sec2);
                                if ((PassedStud + 1) == PassedStudentSubject.length) {
                                    TotalPassedStudent.push(currentPassedStudent);
                                }
                            }
                        }
                        if (FailedStudentSubject.length > 0) {
                            var currentFailedStudent = angular.copy(CommonStudentDetails);
                            for (var PassedStud = 0; PassedStud < FailedStudentSubject.length; PassedStud++) {
                                var PaperAppeared = 'Paper' + (PassedStud + 1) + 'Appeared';
                                var Code = 'Code' + (PassedStud + 1);
                                var PaperTitle = 'PaperTitle' + (PassedStud + 1);
                                var PaperType = 'PaperType' + (PassedStud + 1);
                                var IsElective = 'IsElective' + (PassedStud + 1);
                                var InternalPassingMarks = 'InternalPassingMarks' + (PassedStud + 1);
                                var ExternalPassingMarks = 'ExternalPassingMarks' + (PassedStud + 1);
                                var InternalTotalMarks = 'InternalTotalMarks' + (PassedStud + 1);
                                var PracticalMaxMarks = 'PracticalMaxMarks' + (PassedStud + 1);
                                var Attempt = 'Attempt' + (PassedStud + 1);
                                var InternalC = 'InternalC' + (PassedStud + 1);
                                var ExternalSection1C = 'ExternalSection1C' + (PassedStud + 1);
                                var ExternalSection2C = 'ExternalSection2C' + (PassedStud + 1);
                                var ExternalTotalC = 'ExternalTotalC' + (PassedStud + 1);
                                var PracticalMarksC = 'PracticalMarksC' + (PassedStud + 1);
                                var GraceC = 'GraceC' + (PassedStud + 1);
                                var Total = 'Total' + (PassedStud + 1);
                                var Grade = 'Grade' + (PassedStud + 1);
                                var Credit = 'Credit' + (PassedStud + 1);
                                var ExternalMaxMarks = 'ExternalMaxMarks' + (PassedStud + 1);
                                var Grace = 'Grace' + (PassedStud + 1);

                                currentFailedStudent[PaperAppeared] = '';
                                currentFailedStudent[Code] = FailedStudentSubject[PassedStud].code;
                                currentFailedStudent[PaperTitle] = FailedStudentSubject[PassedStud].paperTitle;
                                if (FailedStudentSubject[PassedStud].paperType.toLowerCase() == 'theory') {
                                    currentFailedStudent[PaperType] = 'TH';
                                } else {
                                    currentFailedStudent[PaperType] = 'PR';
                                }
                                currentFailedStudent[IsElective] = FailedStudentSubject[PassedStud].isElective;
                                currentFailedStudent[InternalPassingMarks] = FailedStudentSubject[PassedStud].theoryInternalPassing;
                                currentFailedStudent[ExternalPassingMarks] = FailedStudentSubject[PassedStud].theoryExternalPassing;
                                currentFailedStudent[InternalTotalMarks] = FailedStudentSubject[PassedStud].theoryInternalMax;
                                currentFailedStudent[PracticalMaxMarks] = FailedStudentSubject[PassedStud].practicalMaxMarks;
                                currentFailedStudent[Attempt] = 1;
                                currentFailedStudent[InternalC] = FailedStudentSubject[PassedStud]._FloatInternalMark;
                                currentFailedStudent[ExternalSection1C] = FailedStudentSubject[PassedStud]._FloatSection1Mark;
                                currentFailedStudent[ExternalSection2C] = FailedStudentSubject[PassedStud]._FloatSection2Mark;
                                currentFailedStudent[ExternalTotalC] = FailedStudentSubject[PassedStud]._ExternalTotal;
                                currentFailedStudent[PracticalMarksC] = FailedStudentSubject[PassedStud]._FloatPracticalMark;
                                currentFailedStudent[GraceC] = FailedStudentSubject[PassedStud]._GraceMark;
                                currentFailedStudent[Total] = FailedStudentSubject[PassedStud]._FinalTotalMarks;
                                currentFailedStudent[Grade] = FailedStudentSubject[PassedStud]._Grade;
                                currentFailedStudent[Credit] = FailedStudentSubject[PassedStud].credits;
                                var sec1 = parseInt(FailedStudentSubject[PassedStud].theoryExternalSection1Max) || 0;
                                var sec2 = parseInt(FailedStudentSubject[PassedStud].theoryExternalSection2Max) || 0;
                                currentFailedStudent[ExternalMaxMarks] = (sec1 + sec2);
                                if ((PassedStud + 1) == FailedStudentSubject.length) {
                                    TotalFailedStudent.push(currentFailedStudent);
                                }
                            }
                        }

                        if ((ii + 1) == (Records.length)) {
                            SaveExamResultTodb('Exam/SaveMarksDetailsInDb?admissionYear=' + $('#txtYear').val() + '&stream=' + $('#stream').val() + '&examName=' + $('#txtExamName').val() + '&examType=' + $('#course').val() + '&semester=' + $('#semester').val() + '&year=' + $('#txtExamYear').val() + '&month=' + $('#txtExamMonth').val(), angular.toJson(TotalPassedStudent)).done(function (respo) {
                                SaveExamResultTodb('Exam/SaveATKTMarksDetailsInDb?admissionYear=' + $('#txtYear').val() + '&stream=' + $('#stream').val() + '&examName=' + $('#txtExamName').val() + '&examType=' + $('#course').val() + '&semester=' + $('#semester').val() + '&year=' + $('#txtExamYear').val() + '&month=' + $('#txtExamMonth').val(), angular.toJson(TotalFailedStudent)).done(function (respo) {
                                    toastr.success("Data processed successfully", "", {
                                        positionClass: "toast-bottom-right",
                                    });
                                    $('#btnSubmitProcess').attr('disabled', false);
                                    $("#btnSubmitProcess").html('Process');
                                });

                                if (respo.isSuccess) {

                                } else {
                                    toastr.error(respo.ErrorMessage, "", {
                                        positionClass: "toast-bottom-right",
                                    });
                                    $('#btnSubmitProcess').attr('disabled', false);
                                    $("#btnSubmitProcess").html('Process');
                                }
                            });
                        }
                    }
                }
            })
        }
    });
}])

Examapp.controller('GenerateAggregateMarksheetCtrl', function ($scope) {
    //$scope.GetSemester = function () {
    //    BindSelectSemesterBasedonCourse('BSC', 'semester')
    //};
    //$scope.GetSemester();
    CallAPI("User/GetPaperList?Course=bsc&specialization=Regular&sem=1").done(function (response) {
        var selectedCatrol = $('#Specialization');
        document.getElementById("Specialization").options.length = 0;
        selectedCatrol.append('<option disabled="disabled" selected="selected" value="">---Select----</option>');
        selectedCatrol.prop('disabled', false);
        for (i = 0; i < response.length; i++) {
            var optionstr = '<option value="' + response[i].specialisation + '" data-specialisationCode="' + response[i].specialisationCode + '">' + response[i].specialisation + '</option>'
            selectedCatrol.append(optionstr);
        }
    })


    MarksheetGenerate
    $.validate({
        form: '#MarksheetGenerate',
        validateOnBlur: true,
        addValidClassOnAll: false,
        modules: 'file',
        onSuccess: function ($form) {
            window.open(_CommonurlUI + '/AggregateMarksheet.html?examType=' + $('#examType').val() + '&specialisationCode=' + $("#Specialization option:selected")[0].attributes[1].nodeValue + '&sem=' + $('#semester').val(), '_blank');
        }
    });
})

Examapp.controller("SummaryreportCtrl", function ($scope, StudentCsvFile) {
    $scope.PageTitle = "Summary Reports";
    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.PageView.stream, 'semester')
    }
    var SpecializationPaperData = [];
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
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

    $.validate({
        form: '#PageViewForm',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            window.open(_CommonurlUI + '/SummaryReport.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=' + CurrentYear + '&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&ReportType=Summary Report', '_blank');
        }
    });
});

Examapp.factory('MarksheetService', ['PaperService', '$http', function (PaperService, $http) {
    var Marksheetservice = {};

    Marksheetservice.GetObjectFromSeatNumber = function (_SeatNumber) {
        var StudentDetailObj = {};
        var R1 = _SeatNumber.toUpperCase().charAt(0);//Course
        var R2 = _SeatNumber.toUpperCase().charAt(2);//Exam Type
        var R3 = _SeatNumber.toUpperCase().charAt(3);//Specialization
        var R4 = _SeatNumber.toUpperCase().charAt(4);//Semester
        if (R1 == 'B') {
            StudentDetailObj.Course = "BSc";
        } else if (R1 == 'M') {
            StudentDetailObj.Course = "MSc";
        } else {
            StudentDetailObj = null;
            return StudentDetailObj;
        }

        if (R2 == 'R') {
            StudentDetailObj.ExamType = "Regular";
        } else if (R2 == 'A') {
            StudentDetailObj.ExamType = "ATKT";
        } else {
            StudentDetailObj = null;
            return StudentDetailObj;
        }

        if (R3 == 'R') {
            StudentDetailObj.Specialisation = "Regular";
        } else if (R3 == 'H') {
            StudentDetailObj.Specialisation = "Honors";
        } else {
            StudentDetailObj = null;
            return StudentDetailObj;
        }
        StudentDetailObj.Semester = R4;
        return StudentDetailObj;
    };

    Marksheetservice.RemoveCharfunction = function (val) { return val.replace(/'/g, ""); }

    Marksheetservice.GetNumberInWord = function (amount) {
        var words = new Array();
        words[0] = '';
        words[1] = 'One';
        words[2] = 'Two';
        words[3] = 'Three';
        words[4] = 'Four';
        words[5] = 'Five';
        words[6] = 'Six';
        words[7] = 'Seven';
        words[8] = 'Eight';
        words[9] = 'Nine';
        words[10] = 'Ten';
        words[11] = 'Eleven';
        words[12] = 'Twelve';
        words[13] = 'Thirteen';
        words[14] = 'Fourteen';
        words[15] = 'Fifteen';
        words[16] = 'Sixteen';
        words[17] = 'Seventeen';
        words[18] = 'Eighteen';
        words[19] = 'Nineteen';
        words[20] = 'Twenty';
        words[30] = 'Thirty';
        words[40] = 'Forty';
        words[50] = 'Fifty';
        words[60] = 'Sixty';
        words[70] = 'Seventy';
        words[80] = 'Eighty';
        words[90] = 'Ninety';
        amount = amount.toString();
        var atemp = amount.split(".");
        var number = atemp[0].split(",").join("");
        var n_length = number.length;
        var words_string = "";
        if (n_length <= 9) {
            var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
            var received_n_array = new Array();
            for (var i = 0; i < n_length; i++) {
                received_n_array[i] = number.substr(i, 1);
            }
            for (var i = 9 - n_length, j = 0; i < 9; i++ , j++) {
                n_array[i] = received_n_array[j];
            }
            for (var i = 0, j = 1; i < 9; i++ , j++) {
                if (i == 0 || i == 2 || i == 4 || i == 7) {
                    if (n_array[i] == 1) {
                        n_array[j] = 10 + parseInt(n_array[j]);
                        n_array[i] = 0;
                    }
                }
            }
            value = "";
            for (var i = 0; i < 9; i++) {
                if (i == 0 || i == 2 || i == 4 || i == 7) {
                    value = n_array[i] * 10;
                } else {
                    value = n_array[i];
                }
                if (value != 0) {
                    words_string += words[value] + " ";
                }
                if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                    words_string += "Crores ";
                }
                if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                    words_string += "Lakhs ";
                }
                if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                    words_string += "Thousand ";
                }
                if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                    words_string += "Hundred and ";
                } else if (i == 6 && value != 0) {
                    words_string += "Hundred ";
                }
            }
            words_string = words_string.split("  ").join(" ");
        }
        return words_string;

    }

    Marksheetservice.GetDepartment = function (spe) {
        var strDept = '';
        spe = $.trim(spe.toUpperCase());

        if (spe == 'HOSPITALITY AND TOURISM MANAGEMENT' || spe == 'HOSPITALITY & TOURISM MANAGEMENT' || spe == 'INTERIOR DESIGN & RESOURCE MANAGEMENT' || spe == 'INTERIOR DESIGN AND RESOURCE MANAGEMENT') {
            return 'RESOURCE MANAGEMENT';
        }
        else if (spe == 'DC' || spe == 'DEVELOPMENTAL COUNSELLING' || spe == 'EARLY CHILDHOOD CARE EDUCATION' || spe == 'EARLY CHILDHOOD CARE AND EDUCATION') {
            return 'HUMAN DEVELOPMENT';
        }
        else if (spe == 'MASS COMMUNICATION & EXTENSION' || spe == 'MASS COMMUNICATION AND EXTENSION' || spe == 'TEXTILES & APPAREL DESIGNING' || spe == 'TEXTILES AND APPAREL DESIGNING' || spe == 'FOOD NUTRITION AND DIETETICS' || spe == 'FOOD NUTRITION & DIETETICS') {
            return spe;
        }
        return strDept;
    }
    Marksheetservice.GetYearLogicString = function () {
        /*if (CurrentYear == undefined) {
            alert('please save year details of exam');
        }
        var lastnumber = CurrentYear.toString().substr(-2);
        var intyear = parseFloat(CurrentYear) || 0;
        return (intyear - 1) + '-' + lastnumber;*/

        var sem = getUrlVars()["sem"];
        var year = getUrlVars()["CurrentYear"];
        var month = new Date().getMonth();

        if (sem == undefined || year == undefined) {
            if (CurrentYear == undefined) {
                alert('please save year details of exam');
            }
            var lastnumber = CurrentYear.toString().substr(-2);
            var intyear = parseFloat(CurrentYear) || 0;
            return (intyear - 1) + '-' + lastnumber;
        }

        var lastnumber = year.toString().substr(-2);
        var intyear = parseFloat(year) || 0;

        if (sem == 1 || sem == 3 || sem == 5) {
            if (month < 6)
                intyear = (intyear - 1) + "-" + intyear.toString().substr(-2);
            else
                intyear = intyear + "-" + (parseFloat(intyear.toString().substr(-2)) + 1);

            return intyear;
            //return (intyear) + '-' + parseFloat(parseFloat(lastnumber) + 1);
        }
        else {
            return (intyear - 1) + '-' + lastnumber;
        }
    }

    Marksheetservice.GetMarksheetObjectFromCsvObj = function (Obj, PaperObj, HeaderDetail) {
        var StudentObj = Marksheetservice.GetSubjectDetailStudent(Obj, PaperObj, HeaderDetail.Semester);
        StudentObj.FullName = Obj.LastName + ' ' + Obj.FirstName + ' ' + Obj.FatherName + ' ' + Obj.MotherName;
        StudentObj.Course = Obj.Course;
        if (PaperObj == undefined || PaperObj == null) {
            return StudentObj;
        }

        StudentObj.Specialisation = PaperObj.specialisation;
        StudentObj.PRN = Marksheetservice.RemoveCharfunction(Obj.PRN);
        StudentObj.semester = HeaderDetail.Semester;
        StudentObj.semesterInRoman = Marksheetservice.GetSmesterInRoman(HeaderDetail.Semester);
        StudentObj.SeatNumber = Obj.SeatNumber;
        StudentObj.semesterInWord = Marksheetservice.GetNumberInWord(HeaderDetail.Semester);
        StudentObj.College_Registration_No_ = Obj.College_Registration_No_;
        StudentObj.SerialNumber = Obj.SerialNumber;
        StudentObj.Department = PaperService.GetDepartment(StudentObj.Specialisation);
        StudentObj.MarksheetYear = Marksheetservice.GetYearLogicString();
        StudentObj.IsRegularStudent = true;
        if (HeaderDetail.RegularORHonors != undefined) {
            if (HeaderDetail.RegularORHonors.toLowerCase() == 'regular') {
                StudentObj.IsRegularStudent = true;
            } else {
                StudentObj.IsRegularStudent = false;
            }

        }

        //StudentObj=MarksheetService.GetFinalTotalCalculation(StudentObj);
        return StudentObj;
    }

    Marksheetservice.GetGrade = function (strTotalMarkCode) {
        //strTotalMarkCode = Math.round(strTotalMarkCode);
        if (strTotalMarkCode < 40) return 'F';
        else if (strTotalMarkCode >= 40 && strTotalMarkCode < 45) return 'P';
        else if (strTotalMarkCode >= 45 && strTotalMarkCode < 50) return 'C';
        else if (strTotalMarkCode >= 50 && strTotalMarkCode < 55) return 'B';
        else if (strTotalMarkCode >= 55 && strTotalMarkCode < 60) return 'B+';
        else if (strTotalMarkCode >= 60 && strTotalMarkCode < 70) return 'A';
        else if (strTotalMarkCode >= 70 && strTotalMarkCode < 80) return 'A+';
        else if (strTotalMarkCode >= 80 && strTotalMarkCode < 90) return 'O';
        else if (strTotalMarkCode >= 90 && strTotalMarkCode < 101) return 'O+';
    }

    Marksheetservice.GetGradePoint = function (strTotalMarkCode) {
        strTotalMarkCode = Math.round(strTotalMarkCode);
        if (strTotalMarkCode >= 90)
            return "10";
        if (strTotalMarkCode >= 80 && strTotalMarkCode < 90)
            return "9." + (strTotalMarkCode % 10) + "0";
        if (strTotalMarkCode >= 70 && strTotalMarkCode < 80)
            return "8." + (strTotalMarkCode % 10) + "0";
        if (strTotalMarkCode >= 60 && strTotalMarkCode < 70)
            return "7." + ((strTotalMarkCode) % 10) + "0";
        if (strTotalMarkCode >= 55 && strTotalMarkCode < 60)
            return "6." + (((strTotalMarkCode) % 5) * 2) + "0";
        if (strTotalMarkCode >= 50 && strTotalMarkCode < 55)
            return "5." + ((strTotalMarkCode % 10) + 5) + "0";
        if (strTotalMarkCode >= 45 && strTotalMarkCode < 50)
            return "5." + ((strTotalMarkCode % 5)) + "0";
        if (strTotalMarkCode >= 40 && strTotalMarkCode < 45)
            return "4." + ((strTotalMarkCode % 5) * 2) + "0";
        if (strTotalMarkCode >= 0 && strTotalMarkCode < 40)
            return "0";
    }

    Marksheetservice.GetSubjectDetailStudent = function (CSVObj_obj, SpecializationObj, SemesterDetails) {
        try {
            throw CSVObj_obj
        } catch (CSVObj) {
            var StudentDetailObj = {};
            var ObjStudentSubject = [];
            StudentDetailObj.FailureCount = 0;
            StudentDetailObj.TotalAllSubjectMark = 0;
            StudentDetailObj.TotalGradePoint = 0;
            StudentDetailObj.FailedCredits = 0;
            StudentDetailObj.MissedSubjectFromCSV = 0;

            StudentDetailObj.MissedSubjectFromCSVArray = [];
            for (var propobj = 0; propobj < Object.keys(CSVObj).length; propobj++) {
                try {
                    throw propobj;
                } catch (prop) {
                    var strCode = 'Code' + (prop + 1);
                    var strInternalMarkCode = "InternalC" + (prop + 1);
                    var strExternalSection1Code = "ExternalSection1C" + (prop + 1);
                    var strExternalSection2Code = "ExternalSection2C" + (prop + 1);
                    var strExternalTotalCode = "ExternalTotalC" + (prop + 1);
                    var strPracticalMarkCode = "PracticalMarksC" + (prop + 1);
                    var Grace = 'GraceC' + (prop + 1);
                    var strRemarks = 'Remarks' + (prop + 1);
                    if (CSVObj[strCode] != undefined) {
                        if (CSVObj[strCode] != '' && CSVObj["College_Registration_No_"] != '') {
                            var PaperIndex = SpecializationObj.paperDetails.findIndex(p => p.code == CSVObj[strCode]);
                            if (PaperIndex == -1) {
                                StudentDetailObj.MissedSubjectFromCSV++;
                                StudentDetailObj.MissedSubjectFromCSVArray.push(CSVObj[strCode]);
                                //alert('Subject missing In json ' + CSVObj[strCode])
                                continue;
                            }
                            var PaperObj = {};
                            if (SpecializationObj.paperDetails[PaperIndex].isContinousAssessment == undefined) {
                                SpecializationObj.paperDetails[PaperIndex].isContinousAssessment = false;
                            }
                            PaperObj = SpecializationObj.paperDetails[PaperIndex];

                            if (((parseFloat(PaperObj.practicalMaxMarks) || 0) + parseFloat((PaperObj.theoryExternalSection1Max) || 0) + (parseFloat(PaperObj.theoryExternalSection2Max) || 0) + (parseFloat(PaperObj.theoryInternalMax) || 0)) == 50) {
                                //PaperObj._IsRequiredDouble = true;
                                //PaperObj.DoubleCount = 2;
                                //double calculation disabled 03/05/2019
                                PaperObj._IsRequiredDouble = false;
                                PaperObj.DoubleCount = 1;
                            }
                            else if ((parseFloat(PaperObj.practicalMaxMarks) || 0) > 100) {
                                PaperObj.DoubleCount = 1;
                                PaperObj.IsRequiredToMakeCalculate100 = true;
                            }
                            else {
                                PaperObj._IsRequiredDouble = false;
                                PaperObj.DoubleCount = 1;
                            }
                            PaperObj._SubjectCode = CSVObj[strCode];
                            PaperObj._InternalMark = CSVObj[strInternalMarkCode];
                            PaperObj._FloatInternalMark = Math.round(PaperObj.DoubleCount * (parseFloat(PaperObj._InternalMark) || 0));
                            PaperObj._FloatCredit = (PaperObj.DoubleCount * (parseFloat(PaperObj.credits) || 0));
                            PaperObj._Remarks = CSVObj[strRemarks];
                            if (SemesterDetails == 1 || SemesterDetails == 2) {
                                PaperObj._Section1Mark = CSVObj[strExternalSection1Code];
                                PaperObj._Section2Mark = CSVObj[strExternalSection2Code];

                                if (PaperObj._Section1Mark == "" && PaperObj._Section2Mark == "" && CSVObj[strExternalTotalCode] > 0) {
                                    PaperObj._Section2Mark = CSVObj[strExternalTotalCode];
                                }
                                PaperObj._FloatSection1Mark = ((PaperObj.DoubleCount * (parseFloat(PaperObj._Section1Mark) || 0)));
                                PaperObj._FloatSection2Mark = ((PaperObj.DoubleCount * (parseFloat(PaperObj._Section2Mark) || 0)));
                            } else {
                                //var sec1=CSVObj[strExternalSection1Code];
                                //var sec2=CSVObj[strExternalSection2Code];
                                PaperObj._Section1Mark = 0; //added total as section2 for the sem > 2 only total marks will be there
                                PaperObj._Section2Mark = CSVObj[strExternalTotalCode]; //added total as section2 for the sem > 2 only total marks will be there
                                PaperObj._FloatSection1Mark = ((PaperObj.DoubleCount * (parseFloat(PaperObj._Section1Mark) || 0)));
                                PaperObj._FloatSection2Mark = ((PaperObj.DoubleCount * (parseFloat(PaperObj._Section2Mark) || 0)));
                            }
                            PaperObj._GraceMark = CSVObj[Grace];
                            PaperObj._FloatGraceMark = (PaperObj.DoubleCount * (parseFloat(PaperObj._GraceMark) || 0));

                            PaperObj._PracticalMark = (parseFloat(CSVObj[strPracticalMarkCode]) || 0);
                            PaperObj._FloatPracticalMark = ((PaperObj.DoubleCount * (parseFloat(PaperObj._PracticalMark) || 0)));

                            if (parseFloat(PaperObj.practicalMaxMarks) > 100) {
                                PaperObj._PracticalMark = ((PaperObj._FloatPracticalMark / (PaperObj.practicalMaxMarks / 50)) * 2);
                                PaperObj.theoryExternalPassing = ((100 * PaperObj.theoryExternalPassing) / PaperObj.practicalMaxMarks);
                                PaperObj._FloatCredit = (100 * (parseFloat(PaperObj.credits) || 0)) / PaperObj.practicalMaxMarks;
                                PaperObj.practicalMaxMarks = 100;
                            }

                            PaperObj._ExternalTotal = (PaperObj._FloatSection1Mark + PaperObj._FloatSection2Mark) + PaperObj._FloatGraceMark;
                            PaperObj._ExternalTotalWithOutGrace = PaperObj._FloatSection1Mark + PaperObj._FloatSection2Mark;

                            if (PaperObj._InternalMark % 1 == 0 && PaperObj._ExternalTotal % 1 != 0) {
                                PaperObj._ExternalTotal = Math.round(PaperObj._ExternalTotal);
                                PaperObj._ExternalTotalWithOutGrace = Math.round(PaperObj._FloatSection1Mark + PaperObj._FloatSection2Mark);
                            }
                            else if (PaperObj._ExternalTotal % 1 != 0) {
                                PaperObj._ExternalTotal = Math.floor(PaperObj._ExternalTotal);
                                PaperObj._ExternalTotalWithOutGrace = Math.floor(PaperObj._FloatSection1Mark + PaperObj._FloatSection2Mark);
                            }
                            else if (PaperObj._FloatPracticalMark % 1 != 0) {
                                PaperObj._FloatPracticalMark = Math.floor(PaperObj._FloatPracticalMark);
                            }

                            PaperObj._FinalTotalMarks = Math.round(PaperObj._FloatInternalMark + PaperObj._ExternalTotal + PaperObj._FloatPracticalMark);


                            var IsFailInExternal = false;
                            PaperObj._Status = '';
                            if (CSVObj.ExamType == "ATKT") {
                                PaperObj._Status = "*";
                            }
                            if (PaperObj.isContinousAssessment == false && (PaperObj.DoubleCount * PaperObj.theoryExternalPassing) > 0 && (PaperObj._ExternalTotal + PaperObj._FloatPracticalMark) < (PaperObj.DoubleCount * PaperObj.theoryExternalPassing)) {
                                PaperObj._Status = 'Repeat Final';
                                StudentDetailObj.FailureCount++;
                                IsFailInExternal = true;
                                StudentDetailObj.FailedCredits = parseInt(StudentDetailObj.FailedCredits) + parseInt(PaperObj.credits);
                            }
                            if (PaperObj.isContinousAssessment && (PaperObj.DoubleCount * PaperObj.theoryExternalPassing) > 0 && (PaperObj._ExternalTotal + PaperObj._FloatPracticalMark) < (PaperObj.DoubleCount * PaperObj.theoryExternalPassing)) {
                                PaperObj._Status = 'Repeat Course';
                                StudentDetailObj.FailureCount++;
                                IsFailInExternal = true;
                                StudentDetailObj.FailedCredits = parseInt(StudentDetailObj.FailedCredits) + parseInt(PaperObj.credits);
                            }
                            if ((PaperObj.DoubleCount * PaperObj.theoryInternalPassing) > 0 && (PaperObj._FloatInternalMark) < (PaperObj.DoubleCount * PaperObj.theoryInternalPassing)) {
                                // Added below mentioned line on 14 June to show NP if student is failed in internal
                                PaperObj._FloatInternalMark = 0;
                                PaperObj._InternalMark = (PaperObj._InternalMark == "ABS") ? "ABS" : Math.round(PaperObj._InternalMark);

                                if (PaperObj._InternalMark != "ABS") {
                                    PaperObj._FloatInternalMark = PaperObj._InternalMark;
                                }

                                PaperObj._Status = 'Repeat Course';
                                if (!IsFailInExternal) {
                                    StudentDetailObj.FailedCredits = parseInt(StudentDetailObj.FailedCredits) + parseInt(PaperObj.credits);
                                    StudentDetailObj.FailureCount++;
                                }
                            }

                            var strTotalMarkCode = Math.round(PaperObj._FinalTotalMarks);

                            if (PaperObj._Status == '')
                                PaperObj._Grade = this.GetGrade(strTotalMarkCode);
                            else
                                PaperObj._Grade = 'F';

                            if (PaperObj._Grade == 'F') PaperObj._Grade = 'F';
                            else if (strTotalMarkCode < 40) PaperObj._Grade = 'F';
                            else if (strTotalMarkCode >= 40 && strTotalMarkCode < 44) PaperObj._Grade = 'P';
                            else if (strTotalMarkCode >= 44 && strTotalMarkCode < 50) PaperObj._Grade = 'C';
                            else if (strTotalMarkCode >= 50 && strTotalMarkCode < 55) PaperObj._Grade = 'B';
                            else if (strTotalMarkCode >= 55 && strTotalMarkCode < 60) PaperObj._Grade = 'B+';
                            else if (strTotalMarkCode >= 60 && strTotalMarkCode < 70) PaperObj._Grade = 'A';
                            else if (strTotalMarkCode >= 70 && strTotalMarkCode < 80) PaperObj._Grade = 'A+';
                            else if (strTotalMarkCode >= 80 && strTotalMarkCode < 90) PaperObj._Grade = 'O';
                            else if (strTotalMarkCode >= 90 && strTotalMarkCode < 101) PaperObj._Grade = 'O+';

                            //PaperObj._Grade=Marksheetservice.GetGrade(PaperObj._FinalTotalMarks);
                            PaperObj._TotalGrade = Marksheetservice.GetGradePoint(PaperObj._FinalTotalMarks);
                            PaperObj._TotalGradePoint = (PaperObj._TotalGrade * (PaperObj._FloatCredit));
                            PaperObj._TotalWeightedMarks = (strTotalMarkCode * (PaperObj._FloatCredit));
                            //PaperObj._TotalGradePoint=(PaperObj._TotalGrade*(PaperObj.credits*PaperObj.DoubleCount));
                            StudentDetailObj.TotalAllSubjectMark += (PaperObj._FinalTotalMarks);

                            if (PaperObj.paperTitle.indexOf("Incentive") >= 0) {
                                PaperObj._TotalGrade = "-";
                                PaperObj._TotalGradePoint = "-";
                                PaperObj._Grade = "-";
                            }
                            var NewSubjectObj = {};
                            NewSubjectObj = angular.copy(PaperObj)
                            NewSubjectObj.IsSelected = true;
                            ObjStudentSubject.push(NewSubjectObj);
                        } else {
                            continue;
                        }
                    } else {
                        break;
                    }
                }
            }
            StudentDetailObj.SubjectDetail = ObjStudentSubject;
        }
        return Marksheetservice.GetFinalTotalCalculation(StudentDetailObj);
    }

    Marksheetservice.GetFinalTotalCalculation = function (MarksheetObjFinal) {
        console.log('**********************RCVD from 1 Object*****************************');
        console.log(MarksheetObjFinal);
        console.log('**********************RCVD from 1 Object*****************************');
        MarksheetObjFinal._FinalTotalCredit = 0;
        MarksheetObjFinal._FinalTotalInternalMark = 0;
        MarksheetObjFinal._FinalExternalTotal = 0;
        MarksheetObjFinal._FinalPracticalTotal = 0;
        MarksheetObjFinal._FinalPract_ExternalTotal = 0;
        MarksheetObjFinal._FinalTotalGrade = 0;
        MarksheetObjFinal._FinalTotalGradePoint = 0;
        MarksheetObjFinal._FinalElectiveExternalTotal = 0;
        MarksheetObjFinal._FinalWeightedMarks = 0;

        var totalSubjects = MarksheetObjFinal.SubjectDetail.length;
        for (var i = 0; i < MarksheetObjFinal.SubjectDetail.length; i++) {
            MarksheetObjFinal._FinalTotalCredit += MarksheetObjFinal.SubjectDetail[i]._FloatCredit;
            MarksheetObjFinal._FinalTotalInternalMark += MarksheetObjFinal.SubjectDetail[i]._FloatInternalMark;
            MarksheetObjFinal._FinalExternalTotal += MarksheetObjFinal.SubjectDetail[i]._ExternalTotal;
            MarksheetObjFinal._FinalPracticalTotal += MarksheetObjFinal.SubjectDetail[i]._FloatPracticalMark;
            MarksheetObjFinal._FinalWeightedMarks += MarksheetObjFinal.SubjectDetail[i]._TotalWeightedMarks;

            // Viren - Patch for showing practical total in external total for add on marksheet
            if (MarksheetObjFinal.SubjectDetail[i].isContinousAssessment == false) {
                if (MarksheetObjFinal.SubjectDetail[i]._ExternalTotal > 0) {
                    MarksheetObjFinal._FinalElectiveExternalTotal += MarksheetObjFinal.SubjectDetail[i]._ExternalTotal;
                }
                else if (MarksheetObjFinal.SubjectDetail[i]._ExternalTotal == 0 && MarksheetObjFinal.SubjectDetail[i]._FloatPracticalMark > 0) {
                    MarksheetObjFinal._FinalElectiveExternalTotal += MarksheetObjFinal.SubjectDetail[i]._FloatPracticalMark;
                }
            }
            // else if(MarksheetObjFinal.SubjectDetail[i].isContinousAssessment == true){
            // if(MarksheetObjFinal.SubjectDetail[i]._ExternalTotal == 0 && MarksheetObjFinal.SubjectDetail[i]._FloatPracticalMark > 0){
            // MarksheetObjFinal._FinalElectiveExternalTotal += MarksheetObjFinal.SubjectDetail[i]._FloatPracticalMark;
            // }
            // }
            // if(MarksheetObjFinal._FinalExternalTotal == 0 && MarksheetObjFinal._FinalPracticalTotal > 0){
            // MarksheetObjFinal._FinalExternalTotal = MarksheetObjFinal._FinalPracticalTotal;
            // }

            var TotalExternalAndPracticalMark = (MarksheetObjFinal.SubjectDetail[i]._ExternalTotal + MarksheetObjFinal.SubjectDetail[i]._FloatPracticalMark);;
            MarksheetObjFinal._FinalPract_ExternalTotal += TotalExternalAndPracticalMark;
            MarksheetObjFinal._FinalTotalGrade += (parseFloat(MarksheetObjFinal.SubjectDetail[i]._TotalGrade) || 0);
            MarksheetObjFinal._FinalTotalGradePoint += (parseFloat(MarksheetObjFinal.SubjectDetail[i]._TotalGradePoint) || 0);

            if (MarksheetObjFinal.SubjectDetail[i].paperTitle.indexOf("Incentive") >= 0) {
                totalSubjects--;
            }
        }

        MarksheetObjFinal._FinalPercentage = (MarksheetObjFinal.FailureCount == 0) ? MarksheetObjFinal._FinalWeightedMarks / MarksheetObjFinal._FinalTotalCredit : '-';
        MarksheetObjFinal._FinalPercentageGrade = (MarksheetObjFinal.FailureCount == 0) ? Marksheetservice.GetGrade(MarksheetObjFinal._FinalPercentage) : '';
        MarksheetObjFinal._FinalPercentageGradePoint = (MarksheetObjFinal.FailureCount == 0) ? (MarksheetObjFinal._FinalTotalGradePoint / MarksheetObjFinal._FinalTotalCredit) : '-';
        return MarksheetObjFinal;
    }

    Marksheetservice.sortArray = function (array, property, direction) {
        direction = direction || 1;
        array.sort(function compare(a, b) {
            let comparison = 0;
            if (a[property] > b[property]) {
                comparison = 1 * direction;
            } else if (a[property] < b[property]) {
                comparison = -1 * direction;
            }
            return comparison;
        });
        return array;
    }

    Marksheetservice.AddMissingSubject = function (SubjectArray, StudentsPaper) {
        for (var k = 0; k < SubjectArray.length; k++) {
            var index = StudentsPaper.findIndex(x => x.code == SubjectArray[k].code);
            if (index == -1) {
                var missingSubj = SubjectArray[k];
                missingSubj.IsSelected = false;
                StudentsPaper.push(missingSubj);
            }
        }
        return StudentsPaper;
    }

    Marksheetservice.GetSmesterInRoman = function (sem) {
        var intsam = parseFloat(sem)
        if (intsam == 1) { return 'I' }
        else if (intsam == 2) { return 'II' }
        else if (intsam == 3) { return 'III' }
        else if (intsam == 4) { return 'IV' }
        else if (intsam == 5) { return 'V' }
        else if (intsam == 6) { return 'VI' }
        else if (intsam == 7) { return 'VII' }
        else if (intsam == 8) { return 'VIII' }
    }

    return Marksheetservice;
}]);

Examapp.factory('PaperService', function () {
    var PaperService = {};
    PaperService.GetSpecilizationObj = function (JsonObj, SpecilizationText) {
        var PaperIndex = JsonObj.findIndex(p => p.Specialisation == SpecilizationText || p.specialisationCode == SpecilizationText);
        //var caPapers = ["TG101", "FG101", "FG102", "MG101", "MG102", "MCI03", "FCI03P",
        //    "HCIII09", "HCIII11", "MG201", "LG201", "HCIII27", "MG201", "FCIII08", "FCIII10", "FCIII12", "RCIII08", "RCIII10", "RCIII26", "RCIII27",
        //    "RCIII28", "RCIII37", "MCIII07", "TCIII08", "TCIII09", "TCIII10", "TCIII11",
        //    "RDV33", "RDV02", "FCV20", "FCV21", "FCV04", "TDV17", "TDVI26", "MCV17", "MCVI06",
        //    "RDV34", "RDV04", "RDV11", "FCV06", "FCV02", "FCV05", "FCV07", "HDV08", "HCV19"
        //    , "TDV04", "TDV19", "MCV03", "RDVI14", "RDVI15"
        //];
        //JsonObj[PaperIndex].paperDetails.forEach(function (paper) {
        //    paper.isContAssessment = false;
        //    if (caPapers.indexOf(paper.code) > -1) {
        //        paper.isContAssessment = true;
        //    }
        //});

        return JsonObj[PaperIndex];
    }

    PaperService.GetDepartment = function (spe) {
        if (spe == "" || spe == undefined)
            return spe;
        spe = $.trim(spe.toUpperCase());

        if (spe == 'HOSPITALITY AND TOURISM MANAGEMENT' || spe == 'HOSPITALITY & TOURISM MANAGEMENT' || spe == 'INTERIOR DESIGN & RESOURCE MANAGEMENT' || spe == 'INTERIOR DESIGN AND RESOURCE MANAGEMENT') {
            return 'RESOURCE MANAGEMENT';
        }
        else if (spe == 'DC' || spe == 'DEVELOPMENTAL COUNSELLING' || spe == 'EARLY CHILDHOOD CARE EDUCATION' || spe == 'EARLY CHILDHOOD CARE AND EDUCATION') {
            return 'HUMAN DEVELOPMENT';
        }
        else if (spe == 'MASS COMMUNICATION & EXTENSION' || spe == 'MASS COMMUNICATION AND EXTENSION' || spe == 'TEXTILES & APPAREL DESIGNING' || spe == 'TEXTILES AND APPAREL DESIGNING' || spe == 'FOOD NUTRITION AND DIETETICS' || spe == 'FOOD NUTRITION & DIETETICS') {
            return spe;
        }
        return spe;
    }

    return PaperService;
});

Examapp.service('StudentCsvFile', function ($http) {
    this.GetFileObj = function (stram, StudentType, Semester, CurrentYear, ExamType) {
        return GetCsvToJsonData('File/Download/Data/SVT?fileName=' + stram + '-' + StudentType + '_sem' + Semester + '_' + CurrentYear + '_' + ExamType + '.csv')
    }
    this.SaveFileObj = function (stram, StudentType, Semester, CurrentYear, ExamType, arrayToStore) {
        return CallAPISaveFile('Data/SaveMarks?fileName=' + stram + '-' + StudentType + '_sem' + Semester + '_' + CurrentYear + '_' + ExamType + '.csv', JSON.stringify(angular.toJson(arrayToStore)))

    }
});

Examapp.directive('disableRightClick', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            element.bind('contextmenu', function (e) {
                e.preventDefault();
            })
        }
    }
});

Examapp.filter('RomanNumber', function () {
    return function (num) {
        if (isNaN(num))
            return NaN;
        var digits = String(+num).split(""),
            key = ["", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM",
                "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC",
                "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"],
            roman = "",
            i = 3;
        while (i--)
            roman = (key[+digits.pop() + (i * 10)] || "") + roman;
        return Array(+digits.join("") + 1).join("M") + roman;
    }
})

Examapp.filter('trusted', ['$sce', function ($sce) {
    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
}]);

Examapp.factory('commonService', function () {
    var commonService = {};
    commonService.GetMonthArray = function () {
        return ["January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ];
    }

    commonService.GetnthDate = function (d) {
        if (d > 3 && d < 21) return 'th';
        switch (d % 10) {
            case 1: return "st";
            case 2: return "nd";
            case 3: return "rd";
            default: return "th";
        }
    }

    commonService.GetCurrentAcademicYear = function () {
        var year = new Date().getFullYear();
        var month = new Date().getMonth();
        if (month < 5)
            year = (year - 1) + "-" + year;
        else
            year = year + "-" + (year + 1);

        return year;
    }
    //formate
    //1 - dd/mm/yyyy 2- nth 
    commonService.GetTodayDate = function (formate) {
        var now = new Date();
        var day = (now.getDate());
        var month = ((now.getMonth()));
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        var today = '';
        if (formate == 2) {
            var MonthsArray = commonService.GetMonthArray();
            today = day + commonService.GetnthDate(day) + ' ' + MonthsArray[parseInt(month)] + ' ' + now.getFullYear()
        } else {
            today = (day) + '/' + (month) + '/' + now.getFullYear();
        }
        console.log(today);
        return today;
    }
    return commonService;
})