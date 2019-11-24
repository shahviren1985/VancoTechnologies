angular.module('ExamSystemApp')
Examapp.controller("ExternalMarkEntryCtrl", function ($scope) {

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
            var Result=true;
            $('.StudentExternalMark').each(function (e) {
                if($scope.SemesterSelected=='1' || $scope.SemesterSelected=='2')
                {
                    var SeatNumber=$(this).data('rollnumber');
                    var IndexOfArray = findWithAttr($scope.array, "RollNumber", SeatNumber);
                    var SetValue = findWithAttr($scope.StudentData, "SeatNumber", SeatNumber);

                    var SectionOneMark= $('#txt_ExternalSection1_'+SeatNumber).val();
                    var SectionTwoMark= $('#txt_ExternalSection2_'+SeatNumber).val();

                    var IntSectionOneMark=parseFloat(SectionOneMark)||0;
                    var IntSectionTwoMark=parseFloat(SectionTwoMark)||0;

                    //Set Error if Section1 or 2 has More marks than limit
                    var IntMaxSection1Mark=parseFloat($scope.MaxExternal1Mark)||0;
                    var IntMaxSection2Mark=parseFloat($scope.MaxExternal2Mark)||0;
                    if(IntSectionOneMark>IntMaxSection1Mark || IntSectionTwoMark>IntMaxSection2Mark){
                        Result=false;
                        toastr.error($scope.StudentData[SetValue]["SeatNumber"]+' exceed to max mark', {
                            positionClass: "toast-bottom-right",
                        });
                    }

                    //Add Data To CSV Array
                    $scope.StudentData[SetValue]["ExternalSection1C"+$scope.array[IndexOfArray].PositionOfSubject] = SectionOneMark;
                    $scope.StudentData[SetValue]["ExternalSection2C"+$scope.array[IndexOfArray].PositionOfSubject] = SectionTwoMark;
                    $scope.StudentData[SetValue]["ExternalTotalC"+$scope.array[IndexOfArray].PositionOfSubject] = IntSectionOneMark+IntSectionTwoMark;
                }
                else{
                    var SeatNumber=$(this).data('rollnumber');
                    var IndexOfArray = findWithAttr($scope.array, "RollNumber", SeatNumber);
                    var SetValue = findWithAttr($scope.StudentData, "SeatNumber", SeatNumber);

                    var SectionOneMark= $('#txt_ExternalSection1_'+SeatNumber).val();
                    //var SectionTwoMark= $('#txt_ExternalSection2_'+SeatNumber).val();

                    var IntSectionOneMark=parseFloat(SectionOneMark)||0;
                    //var IntSectionTwoMark=parseFloat(SectionTwoMark)||0;

                    //Set Error if Section1 or 2 has More marks than limit
                    var IntMaxSection1Mark=parseFloat($scope.MaxExternal1Mark)||0;
                    var IntMaxSection2Mark=parseFloat($scope.MaxExternal2Mark)||0;
                    var TotalMark=IntMaxSection1Mark+IntMaxSection2Mark;
                    if(IntSectionOneMark>TotalMark){
                        Result=false;
                        toastr.error($scope.StudentData[SetValue]["SeatNumber"]+' exceed to max mark', {
                            positionClass: "toast-bottom-right",
                        });
                    }

                    //Add Data To CSV Array
                    //$scope.StudentData[SetValue]["ExternalSection1C"+$scope.array[IndexOfArray].PositionOfSubject] = SectionOneMark;
                    //$scope.StudentData[SetValue]["ExternalSection2C"+$scope.array[IndexOfArray].PositionOfSubject] = SectionTwoMark;
                    $scope.StudentData[SetValue]["ExternalTotalC"+$scope.array[IndexOfArray].PositionOfSubject] = SectionOneMark;
                }
                
            });
            if(Result){
                if($scope.StudentData.length==TotalCountOfStudent){
                    CallAPISaveFile('Data/SaveMarks?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv', JSON.stringify(angular.toJson($scope.StudentData))).done(function (respo) {
                        if (respo.isSuccess) {
                            toastr.success("File saved successfully", "", {
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
    
    $(".Drp_Cnrtl").change(function() {
        $('#TableForInsertMark').css('display','none')
    });

    $(document).on('change','.attendance_dd',function(e){
        var SelectedValue=$(this).data('rollnumber');
        if($(this).val().toLowerCase()=='abs'){
            $('.txt_ExternalMark_'+$(this).data('rollnumber')).val('ABS');
            $('.txt_ExternalMark_'+$(this).data('rollnumber')).prop('disabled', true);  
        }
        else if($(this).val().toLowerCase()=='cc'){            
            $('.txt_ExternalMark_'+$(this).data('rollnumber')).val('CC');
            $('.txt_ExternalMark_'+$(this).data('rollnumber')).prop('disabled', true);  
        }
        else{
            $('.txt_ExternalMark_'+$(this).data('rollnumber')).val('');
            $('.txt_ExternalMark_'+$(this).data('rollnumber')).prop('disabled', false);  
        }
    });


    $scope.array = [];
    $scope.StudentData = [];
    var TotalCountOfStudent;
    $.validate({
        form: '#ExternalMarkEntry',
        validateOnBlur: true,
        addValidClassOnAll: false,
        onSuccess: function ($form) {
            $scope.MaxExternal1Mark=$("#paper option:selected")[0].attributes[2].nodeValue;
            $scope.MaxExternal2Mark=$("#paper option:selected")[0].attributes[1].nodeValue
            $scope.SemesterSelected=$('#semester').val();
            GetCsvToJsonData('File/Download/Data/SVT?fileName=' + $scope.InternalMark.stream + '-' + $('#course').val() + '_sem' + $('#semester').val() + '_' + CurrentYear + '_' + $scope.InternalMark.examType + '.csv').done(function (dataresponse) {
                try {
                    $scope.StudentData = csvJSON(dataresponse);
                    TotalCountOfStudent=$scope.StudentData.length;
                } catch (e) {
                    console.log(e)
                }
                if ($scope.StudentData.length > 0) {
                    $('#TableForInsertMark').css('display', 'block')
                } else {
                    $('#TableForInsertMark').css('display', 'none');
                    toastr.error("No Record found", "", {
                        positionClass: "toast-bottom-right",
                    });
                }
                $('#TitleForSubject').text('External Mark For the Subject ' + $("#paper option:selected").text() + '(' + $('#paper').val()+')');
                $scope.SelectedPaperNameToShow=$("#paper option:selected").text()
                var PaperToEnter = $('#paper').val();
                $scope.array = [];
                for (var i = 0; i < $scope.StudentData.length; i++) {
                    if($scope.StudentData[i].Specialisation==undefined ){
                        continue;   
                    }
                    if($scope.StudentData[i].Specialisation.toLowerCase()==$("#Specialization").val().toLowerCase() ||  $scope.StudentData[i].Specialisation.toLowerCase()==$("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase()){
                        var Key = getKeyByValue($scope.StudentData[i], PaperToEnter);
                        if (Key == undefined) {
                            continue;
                        }
                        var lastChar = Key.replace("Code", "");//[Key.length - 1];
                        var Number = parseInt(lastChar);
                        var internalmark = 'InternalC' + Number;
                        var ExternalMarkf = 'ExternalSection' + $('#Section').val() + 'C' + Number;
                        var arrayonj = {};
                        arrayonj.RollNumber = $scope.StudentData[i].SeatNumber;
                        arrayonj.LastName = $scope.StudentData[i].LastName;
                        arrayonj.FirstName = $scope.StudentData[i].FirstName;
                        arrayonj.FatherName = $scope.StudentData[i].FatherName;
                        arrayonj.MotherName = $scope.StudentData[i].MotherName;
                        arrayonj.SeatNumber=$scope.StudentData[i].SeatNumber;
                        arrayonj.SubjectCode = $scope.StudentData[i][Key];
                        arrayonj.InternalMark = $scope.StudentData[i][internalmark];
                        var intintmark = parseFloat(arrayonj.InternalMark) || 0;
                        var IntMinRequ = parseFloat($scope.MinRequredPassingMark) || 0;

                        if (intintmark >= IntMinRequ) {
                            arrayonj.IsRequiredDisble = false;
                        }else{
                            arrayonj.IsRequiredDisble = true;
                        }
                        //arrayonj.ExternalMark = $scope.StudentData[i][ExternalMarkf];
                        if($('#semester').val()==1||$('#semester').val()==2){
                            arrayonj.ExternalMarkSection1 = $scope.StudentData[i]['ExternalSection1C' + Number];
                        }else{
                            arrayonj.ExternalMarkSection1 = $scope.StudentData[i]['ExternalTotalC' + Number];
                        }
                        
                        arrayonj.ExternalMarkSection2=$scope.StudentData[i]['ExternalSection2C' + Number];
                        arrayonj.Key = ExternalMarkf;
                        arrayonj.PositionOfSubject=Number;
                        if(arrayonj.ExternalMarkSection1=="ABS"){
                            arrayonj.AttendenceValue="ABS"
                            arrayonj.IsRequiredDisble = true;
                        }
                        else if(arrayonj.ExternalMarkSection1=="CC"){
                            arrayonj.AttendenceValue="CC"
                            arrayonj.IsRequiredDisble = true;
                        }else{
                            arrayonj.AttendenceValue="P"
                        }

                        //Seat Number
                        arrayonj.LogicalSeatNumberKe = $scope.StudentData[i].SeatNumber.substring(4, $scope.StudentData[i].SeatNumber.length);

                        $scope.array.push(arrayonj);
                    }
                }
                $scope.array.sort(function(a, b) {
                    return a.LogicalSeatNumberKe - b.LogicalSeatNumberKe;
                });
                $scope.$digest();
            });
        }
    });

    $scope.GetSemester = function () {
        BindSelectSemesterBasedonCourse($scope.InternalMark.stream, 'semester')
    }

    var SpecializationPaperData = [];
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
                var optionstr = '<option value="' + response[i].specialisation + '" data-specialisationCode="' + response[i].specialisationCode + '">' + response[i].specialisation+ '</option>'
                selectedCatrol.append(optionstr);
                //$('<option />', { value: response[i].specialisation, text: response[i].specialisation }).appendTo(selectedCatrol);
            }
        })
    });

    $('#paper').on('change', function () {
        $scope.MinRequredPassingMark=$("#paper option:selected")[0].attributes[5].nodeValue;
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
                    if(SpecializationPaperData[i].paperDetails[j].theoryExternalSection2Max>0 || SpecializationPaperData[i].paperDetails[j].theoryExternalSection1Max>0)
                    {
                        var optionstr = '<option value="' + SpecializationPaperData[i].paperDetails[j].code + '" data-theoryExternalSection2Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection2Max + '" data-theoryExternalSection1Max="' + SpecializationPaperData[i].paperDetails[j].theoryExternalSection1Max + '" data-theoryExternalPassing="' + SpecializationPaperData[i].paperDetails[j].theoryExternalPassing + '" data-InternalMax="' + SpecializationPaperData[i].paperDetails[j].theoryInternalMax + '" data-passingmark="' + SpecializationPaperData[i].paperDetails[j].theoryInternalPassing + '">' + SpecializationPaperData[i].paperDetails[j].paperTitle + '</option>'
                        selectedCatrol.append(optionstr);
                        //$('<option />', { value: SpecializationPaperData[i].paperDetails[j].code, text: SpecializationPaperData[i].paperDetails[j].paperTitle, 'data-passingmark': SpecializationPaperData[i].paperDetails[j].theoryInternalPassing }).appendTo(selectedCatrol);
                    }
                }

            }
        }
    })

    $scope.OpenEntrySheet = function () {
        window.open(_CommonurlUI + '/markshentrysheet.html?stream=' + $('#stream').val() + '&course=' + $('#course').val() + '&sem=' + $('#semester').val() + '&examType=' + $('#examType').val() + '&year=2018&Specialization=' + $("#Specialization option:selected")[0].attributes[1].nodeValue.toLowerCase() + '&paper=' + $('#paper').val()+'&PaperName='+$("#paper option:selected").text(), '_blank');
    }
});