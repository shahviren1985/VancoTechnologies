angular.module('ExamSystemApp')
Examapp.controller("GraceMarkEntryCtrl", function ($scope) {
    
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
            var Result = true;
            $('.StudentInternalMark').each(function (e) {
                var IdOfStudentMark = $(this).attr("id");
                var Rollnumber = IdOfStudentMark.replace("InternalMark_", "");
                var IndexOfArray = findWithAttr($scope.array, "SeatNumber", Rollnumber);
                var SetValue = findWithAttr($scope.StudentData, "SeatNumber", Rollnumber);
                var convertedIntStudentMark = parseInt($(this).val()) || 0;
                var convertedInternalMaxMark = parseInt($scope.array[IndexOfArray].MaxAllowedGraceMark) || 0;
                if (convertedIntStudentMark > convertedInternalMaxMark) {
                    toastr.error($scope.StudentData[SetValue]["FirstName"] + ' mark cannot be more than ' + convertedInternalMaxMark, "", {
                        positionClass: "toast-bottom-right",
                    });
                    Result = false;
                }else{
                    $scope.StudentData[SetValue][$scope.array[IndexOfArray].Key] = $(this).val();
                }
            });
            if (Result) {
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
            InternalMaxMark = $("#paper option:selected")[0].attributes[4].nodeValue;
            InternalMinPassingMark = $("#paper option:selected")[0].attributes[5].nodeValue;
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv').done(function (dataresponse) {
                try {
                    $scope.StudentData = csvJSON(dataresponse);
                    console.log('viren');
                    console.log($scope.StudentData);
                } catch (e) {
                    console.log(e)
                }
                if ($scope.StudentData.length > 0) {
                    $('#InternalMarkEntryForm').css('display', 'block');
                    $('#TitleForSubject1').text('Grace Mark For the Subject ' + $("#paper option:selected").text()+'('+$('#paper').val()+')' );
                    $scope.SelectedPaperNameToShow = $("#paper option:selected").text()
                } else {
                    $('#InternalMarkEntryForm').css('display', 'none');
                    toastr.error("No Record found", "", {
                        positionClass: "toast-bottom-right",
                    });
                }

                var PaperToEnter = $('#paper').val();
                $scope.array = [];
                for (var i = 0; i < $scope.StudentData.length; i++) {
                    if ($scope.StudentData[i].Specialisation == undefined) {
                        continue;
                    }
                    if ($scope.StudentData[i].Specialisation.toLowerCase() == $("#Specialization").val().toLowerCase() || $scope.StudentData[i].Specialisation.toLowerCase() == $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()) {
                        var Key = getKeyByValue($scope.StudentData[i], PaperToEnter);
                        if (Key == undefined) {
                            continue;
                        }
                        var lastChar = Key.replace("Code", "");//[Key.length - 1];
                        var Number = parseInt(lastChar);
                        var internalmark = 'GraceC' + Number;
                        var internalmarkReal = 'InternalC' + Number;
                        var ExternalReal1 = 'ExternalSection1C' + Number;
                        var ExternalReal2 = 'ExternalSection2C' + Number;
						var ExternalTotal = 'ExternalTotalC' + Number;
                        var PracticalReal='PracticalMarksC'+Number;
                        var arrayonj = {};
                        arrayonj.TotalAllMark=0;
                        arrayonj.TotalAllGrace=0;
                        
                        //Count Total Marks In all Subject vs GraceMark
                        var CountKey=Object.keys($scope.StudentData[i]).length;
                        $scope.TotalAllSubjectMaxMark=0;
                        KeyToCkeck:for(var j=0;j<CountKey;j++){
                            var CodeStr='Code'+(j+1);
                            if($scope.StudentData[i][CodeStr]!=undefined){
                                //Code for TotalMark of All Subject
                                if($scope.StudentData[i][CodeStr]!=''){
                                    var IndexOfPaper=$scope.SubjectArraOfCurrentSelection.findIndex(x => x.code==$('#paper').val());
                                    var prcmrk= $scope.SubjectArraOfCurrentSelection[IndexOfPaper].practicalMaxMarks;
                                    var ExtSec1= $scope.SubjectArraOfCurrentSelection[IndexOfPaper].theoryExternalSection1Max;
                                    var ExtSec2= $scope.SubjectArraOfCurrentSelection[IndexOfPaper].theoryExternalSection2Max;
                                    var Intermrk= $scope.SubjectArraOfCurrentSelection[IndexOfPaper].theoryInternalMax;
                                    var in1=parseFloat(prcmrk)||0;
                                    var in2=parseFloat(ExtSec1)||0;
                                    var in3=parseFloat(ExtSec2)||0;
                                    var in4=parseFloat(Intermrk)||0;
                                    $scope.TotalAllSubjectMaxMark+=(in1+in2+in3+in4);
                                    var ExternalSection1Cstr='ExternalSection1C'+(j+1);
                                    var ExternalSection2Cstr='ExternalSection2C'+(j+1);
                                    var GraceCstr='GraceC'+(j+1);
                                    var InternalC='InternalC'+(j+1);
                                    var PracticalMarksC='PracticalMarksC'+(j+1);
                                    var Int1Internal=parseFloat($scope.StudentData[i][InternalC])||0;
                                    var Int1Ext1=parseFloat($scope.StudentData[i][ExternalSection1Cstr]) || 0;
                                    var Int1Ext2=parseFloat($scope.StudentData[i][ExternalSection2Cstr]) || 0;
                                    var Int1Grace=parseFloat($scope.StudentData[i][GraceCstr]) || 0;
                                    var IntPract=parseFloat($scope.StudentData[i][PracticalMarksC]) || 0;
                                    if($scope.StudentData[i][CodeStr]==$('#paper').val()){
                                        arrayonj.ThisSubjectAllTotal=Int1Internal+Int1Ext1+Int1Ext2+IntPract;
                                    }
                                    arrayonj.TotalAllMark+=(Int1Internal+Int1Ext1+Int1Ext2+IntPract);
                                    arrayonj.TotalAllGrace+=(Int1Grace);
                                }else{
                                    continue;
                                }
                            }else{
                                break KeyToCkeck;
                            }

                        }
                        arrayonj.RollNumber = $scope.StudentData[i].RollNumber;
                        arrayonj.LastName = $scope.StudentData[i].LastName;
                        arrayonj.FirstName = $scope.StudentData[i].FirstName;
                        arrayonj.SeatNumber = $scope.StudentData[i].SeatNumber;
                        arrayonj.FatherName = $scope.StudentData[i].FatherName;
                        arrayonj.MotherName = $scope.StudentData[i].MotherName;
                        arrayonj.LD = $scope.StudentData[i].LD;
                        arrayonj.SubjectCode = $scope.StudentData[i][Key];
                        var GraceMarkForS= $scope.StudentData[i][internalmark];
                        arrayonj.GraceMark=parseFloat(GraceMarkForS)|| 0;
                        arrayonj.InternalMark=parseFloat($scope.StudentData[i][internalmarkReal])|| 0;
                        arrayonj.External1=parseFloat($scope.StudentData[i][ExternalReal1])|| 0;
                        arrayonj.External2=parseFloat($scope.StudentData[i][ExternalReal2])|| 0;
						arrayonj.ExternalTotal = parseFloat($scope.StudentData[i][ExternalTotal])|| 0;
                        arrayonj.PracticalMark=parseFloat($scope.StudentData[i][PracticalReal])|| 0;
                        arrayonj.LogicalSeatNumberKe = $scope.StudentData[i].SeatNumber.substring(4, $scope.StudentData[i].SeatNumber.length);

                        if ($scope.InternalMark.examType == "ATKT") {
                            if (arrayonj.LD.toLowerCase() == "true") {
                                arrayonj.MaxAllowedGraceMark = ((15 - arrayonj.TotalAllGrace) + arrayonj.GraceMark);
                            }
                            else {
                                arrayonj.MaxAllowedGraceMark = 6;
                            }
                        } else {
                            if (arrayonj.LD.toLowerCase() == "true") {
                                arrayonj.MaxAllowedGraceMark = ((15 - arrayonj.TotalAllGrace) + arrayonj.GraceMark);
                            }
                            else {
                                arrayonj.MaxAllowedGraceMark = (((($scope.TotalAllSubjectMaxMark * 1) / 100) - arrayonj.TotalAllGrace) + arrayonj.GraceMark);
                            }
                        }

                        
                        arrayonj.Key = internalmark;
                        $scope.array.push(arrayonj);
                    }
                }

                $scope.array.sort(function (a, b) {
                    return a.RollNumber - b.RollNumber;
                });
                console.log($scope.array)
                $scope.$digest();
            });
        }
    });

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester');
    }

    var SpecializationPaperData = [];
    var SelectedSortSpecialization = '';
    $('#semester').on('change', function () {
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + $('#course').val() + "&sem=" + $('#semester').val()).done(function (response) {
            SpecializationPaperData = response;
            console.log(SpecializationPaperData)
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

    $scope.SubjectArraOfCurrentSelection=[];
    $('#Specialization').on('change', function () {
        for (i = 0; i < SpecializationPaperData.length; i++) {
            if (SpecializationPaperData[i].specialisation == $('#Specialization').val()) {
                $scope.SubjectArraOfCurrentSelection=SpecializationPaperData[i].paperDetails;
                var selectedCatrol = $('#paper');
                document.getElementById("paper").options.length = 0;
                selectedCatrol.append('<option disabled="disabled" selected="selected" value="">---Select----</option>');
                selectedCatrol.prop('disabled', false);
                for (var j = 0; j < SpecializationPaperData[i].paperDetails.length; j++) {
                    //if (SpecializationPaperData[i].paperDetails[j].theoryInternalMax > 0) {
                    var optionstr = '<option value="' + SpecializationPaperData[i].paperDetails[j].code + '" data-theoryExternalSection2Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection2Max + '" data-theoryExternalSection1Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection1Max + '" data-theoryExternalPassing="' + SpecializationPaperData[i].paperDetails[j].theoryExternalPassing + '" data-InternalMax="' + SpecializationPaperData[i].paperDetails[j].theoryInternalMax + '" data-passingmark="' + SpecializationPaperData[i].paperDetails[j].theoryInternalPassing + '">' + SpecializationPaperData[i].paperDetails[j].paperTitle + '</option>'
                    selectedCatrol.append(optionstr);
                    //$('<option />', { value: SpecializationPaperData[i].paperDetails[j].code, text: SpecializationPaperData[i].paperDetails[j].paperTitle, 'data-passingmark': SpecializationPaperData[i].paperDetails[j].theoryInternalPassing }).appendTo(selectedCatrol);
                    //}
                }
            }
        }
    })

    $scope.OpenEntrySheet = function () {
        //console.log($('#Specialization').getAttribute('data-specialisationcode'));
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=2018&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val()+'&PaperName='+$("#paper option:selected").text(), '_blank');
    }
});