angular.module('ExamSystemApp')
Examapp.controller("PracticalMarkEntryCtrl", function ($scope) {

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

    $(".Drp_Cnrtl").change(function() {
        $('#PracticleEntryDiv').css('display','none')
    });

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
                var convertedIntStudentMark = parseFloat($(this).val()) || 0;
                var convertedInternalMaxMark = parseFloat(InternalMaxMark) || 0;
                if (convertedIntStudentMark > convertedInternalMaxMark) {
                    toastr.error($scope.StudentData[SetValue]["FirstName"] + ' mark cannot be more than ' + convertedInternalMaxMark, "", {
                        positionClass: "toast-bottom-right",
                    });
                    Result = false;
                }
                $scope.StudentData[SetValue][$scope.array[IndexOfArray].Key] = $(this).val();
            });
            if (Result) {
                if($scope.StudentData.length==TotalCountOfStudent){
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
                }else{
                    alert("Students record are missing! Please try again")
                    $route.reload();
                }
            }
        }
    });
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
            InternalMaxMark = $("#paper option:selected")[0].attributes[6].nodeValue
            //InternalMinPassingMark = $("#paper option:selected")[0].attributes[5].nodeValue
            console.log($("#paper option:selected")[0].attributes);

            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv').done(function (dataresponse) {
                $scope.StudentData = csvJSON(dataresponse);
                TotalCountOfStudent=$scope.StudentData.length;
                console.log($scope.StudentData)
                if($scope.StudentData.length>0){
                    $('#TitleForSubject').text('Practical Mark For the Subject ' + $("#paper option:selected").text()+ '(' + $('#paper').val()+')');
                    $('#PracticleEntryDiv').css('display','block');
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
                        var internalmark = 'PracticalMarksC' + Number;
                        var arrayonj = {};
                        arrayonj.RollNumber = $scope.StudentData[i].RollNumber;
                        arrayonj.LastName = $scope.StudentData[i].LastName;
                        arrayonj.FirstName = $scope.StudentData[i].FirstName;
                        arrayonj.SeatNumber = $scope.StudentData[i].SeatNumber;
                        arrayonj.FatherName = $scope.StudentData[i].FatherName;
                        arrayonj.MotherName = $scope.StudentData[i].MotherName;
                        arrayonj.SubjectCode = $scope.StudentData[i][Key];
                        arrayonj.SubjectName = $("#paper option:selected").text();
                        arrayonj.InternalMark = $scope.StudentData[i][internalmark];
                        arrayonj.Key = internalmark;
                        if(arrayonj.InternalMark=="ABS"){
                            arrayonj.AttendenceValue="ABS"
                        }
                        else if(arrayonj.InternalMark=="CC"){
                            arrayonj.AttendenceValue="CC"
                        }else{
                            arrayonj.AttendenceValue="P"
                        }
                        $scope.array.push(arrayonj);
                    }
                }
                $scope.array.sort(function(a, b) {
                    return a.RollNumber - b.RollNumber;
                });
                $scope.$digest();
            });
        }
    });

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    $(document).on('change','.attendance_dd',function(e){
        var SelectedValue=$(this).data('rollnumber');
        //alert( $(this).data('rollnumber') );
        if($(this).val().toLowerCase()=='abs'){
            $('[name=InternalMark_'+$(this).data('rollnumber')+']').val('ABS');
            $('[name=InternalMark_'+$(this).data('rollnumber')+']').prop('disabled', true);    
        }
        else if($(this).val().toLowerCase()=='cc'){
            $('[name=InternalMark_'+$(this).data('rollnumber')+']').val('CC');
            $('[name=InternalMark_'+$(this).data('rollnumber')+']').prop('disabled', true);    
        }
        else{
            $('[name=InternalMark_'+$(this).data('rollnumber')+']').prop('disabled', false);    
            $('[name=InternalMark_'+$(this).data('rollnumber')+']').val('');
        }
    });

    var SpecializationPaperData = [];
    var SelectedSortSpecialization = '';
    $('#semester').on('change', function () {
        var selectedCourse=$('#course').val();
        if($('#course').val()=='Elective'){
            selectedCourse='Honors';
        }
        CallAPI("User/GetPaperList?Course=" + $('#stream').val() + "&specialization=" + selectedCourse + "&sem=" + $('#semester').val()).done(function (response) {
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
        });
    });
    $('#Specialization').on('change', function () {
        for (i = 0; i < SpecializationPaperData.length; i++) {
            if (SpecializationPaperData[i].specialisation == $('#Specialization').val()) {
                var selectedCatrol = $('#paper');
                document.getElementById("paper").options.length = 0;
                selectedCatrol.append('<option disabled="disabled" selected="selected" value="">---Select----</option>');
                selectedCatrol.prop('disabled', false);
                for (var j = 0; j < SpecializationPaperData[i].paperDetails.length; j++) {
                    console.log(SpecializationPaperData[i].paperDetails)
                    if (SpecializationPaperData[i].paperDetails[j].practicalMaxMarks > 0) {
                        var optionstr = '<option value="' + SpecializationPaperData[i].paperDetails[j].code + '" data-theoryExternalSection2Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection2Max + '" data-theoryExternalSection1Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection1Max + '" data-theoryExternalPassing="' + SpecializationPaperData[i].paperDetails[j].theoryExternalPassing + '" data-InternalMax="' + SpecializationPaperData[i].paperDetails[j].theoryInternalMax + '" data-passingmark="' + SpecializationPaperData[i].paperDetails[j].theoryInternalPassing + '" data-MaxPracticalMark="' + SpecializationPaperData[i].paperDetails[j].practicalMaxMarks + '">' + SpecializationPaperData[i].paperDetails[j].paperTitle + '</option>'
                        selectedCatrol.append(optionstr);
                        //$('<option />', { value: SpecializationPaperData[i].paperDetails[j].code, text: SpecializationPaperData[i].paperDetails[j].paperTitle, 'data-passingmark': SpecializationPaperData[i].paperDetails[j].theoryInternalPassing }).appendTo(selectedCatrol);
                    }

                }

            }
        }
    })


    $scope.OpenEntrySheet = function () {
        //console.log($('#Specialization').getAttribute('data-specialisationcode'));
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=2018&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val()+'&PaperName='+$("#paper option:selected").text()+'&IsInternal='+1, '_blank');
    }
});