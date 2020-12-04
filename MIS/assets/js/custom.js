$(document).ready(function ()
{

    var $radios = $('input:radio[name=isSvt]');
    if ($radios.is(':checked') === false) {
        $radios.filter('[value=No]').prop('checked', true);
    }

    var selectedcourse = $("#CourseName").val();
    //alert(selectedcourse);
    $("#KnowAboutCourse").change(function ()
    {
        if (this.value == 'ANY OTHER')
        {
            $('#pleaseSpecify').show();
        } else
        {
            $('#pleaseSpecify').hide();
        }
    });

    $("#ExaminationType").change(function ()
    {
        if (this.value == 'OTHER')
        {
            $('#OtherExaminationType').show();
            $('#OtherExaminationType').addClass('input-error');
        } else
        {
            $('#OtherExaminationType').hide();
            $('#OtherExaminationType').removeClass('input-error');
        }
    });

    $("input[name='isSvt']").click(function ()
    {
        if ($("#YES").is(":checked"))
        {
            $("#DivIsSvtSpecification").show();
            if ($("input:radio[name=IsSvtKnowRefrence]").is(":checked")) {
                $("input:radio[name=IsSvtKnowRefrence]").rules("remove", "required");
            } else {
                $("input:radio[name=IsSvtKnowRefrence]").rules("add", "required");
            }
        } else
        {
            $("#DivIsSvtSpecification").hide();
            $("input:radio[name=IsSvtKnowRefrence]").prop('checked', false);
            $("input:radio[name=IsSvtKnowRefrence]").rules("remove", "required");
        }
    });
    if (selectedcourse == 3 || selectedcourse == 4)
    {
        $('#ExaminationType').val("OTHER").trigger('change');
        $('#OtherExaminationType').show();
        $('#OtherExaminationType').addClass('input-error');
        $('#HscYearofPassing').removeClass('required');
        $('#HscSchoolName').removeClass('required');
        $('#HscMedium').removeClass('required');
        $('#HscBoardName').removeClass('required');
        $('#HscTotalPercent').removeClass('required');
        $('#HscGrade').removeClass('required');

        $('#BachelorYearofPassing').removeClass('required');
        $('#BachelorSchoolName').removeClass('required');
        $('#BachelorMedium').removeClass('required');
        $('#BachelorBoardName').removeClass('required');
        $('#BachelorTotalPercent').removeClass('required');
        $('#BachelorGrade').removeClass('required');

        //$("#SscYearofPassing").rules("add", "required");  
    }

    if (selectedcourse == 1 || selectedcourse == 2)
    {
        $('#ExaminationType').addClass('required');
        $('#HscYearofPassing').addClass('required');
        $('#HscSchoolName').addClass('required');
        $('#HscMedium').addClass('required');
        $('#HscBoardName').addClass('required');
        $('#HscTotalPercent').addClass('required');
        $('#HscGrade').addClass('required');

        $('#BachelorYearofPassing').addClass('required');
        $('#BachelorSchoolName').addClass('required');
        $('#BachelorMedium').addClass('required');
        $('#BachelorBoardName').addClass('required');
        $('#BachelorTotalPercent').addClass('required');
        $('#BachelorGrade').addClass('required');
    }

    $("#CourseNameddn").attr("disabled", "disabled");
    $("#category1").prop("checked", true);



    $("#category1").on("click", function ()
    {
        $("#divReservedCategory").hide();
        $("select[id*=Caste]").rules("remove", "required");
        $("input[id*=SubCaste]").rules("remove", "required");
        $("#divCasteCertificate").hide();
    });
    $("#category2").on("click", function ()
    {
        $("#divReservedCategory").show();
        $("#divCasteCertificate").show();

        $("select[id*=Caste]").rules("add", "required");
        $("input[id*=SubCaste]").rules("add", "required");
        $("input[id*=CastCert]").rules("add", "required");
    });
    $("#chkSamepermanentaddress").click(function ()
    {
        if ($(this).is(":checked"))
        {
            $("#PermanentAddress").val($("#CurrentAddress").val());
            $('#PermanentAddress').attr('readonly', 'readonly');
            $("textarea[id*=PermanentAddress]").rules("remove", "required");
        } else
        {
            $("#PermanentAddress").val(null);
            $('#PermanentAddress').attr('readonly', '');
            $("#PermanentAddress").attr("readonly", false);
            $("textarea[id*=PermanentAddress]").rules("add", "required");
        }
    });
    if ($('#Samepermanentaddress').is(':checked') == true)
    {
        $('#textInput').prop('disabled', true);
    }
    var start = new Date();
    //alert(start.setFullYear(start.getFullYear() - 18));
    //var end = new Date();
    //end.setFullYear(end.getFullYear());
    $("#txtBirthDate").datepicker(
            {

                minDate: new Date(1900, 1 - 1, 1),
                maxDate: '-10Y',
                defaultDate: new Date(2020, 5 - 1, 15),
                changeMonth: true,
                changeYear: true,
                yearRange: '-50:-10',
                dateFormat: 'dd-MM-yy',
                setDate: new Date(2020, 5 - 1, 15),
                onSelect: function (date, ui)
                {
                    var dob = new Date(date);
                    var today = new Date();
                    var age = today.getFullYear() - ui.selectedYear;
                    $('#CalculatedAge').val(age);
                }
            });

    $("#btnBirthDate").click(function ()
    {
        $("#txtBirthDate").focus();
    });
    $("#fileDp").change(function ()
    {
        if (checkfile("fileDp", 500))
        {
            var fileDp = $("#fileDp")[0];
            if (fileDp.files && fileDp.files[0])
            {
                var reader = new FileReader();
                reader.onload = function (e)
                {
                    $('#photo').attr('src', e.target.result);
                    $('#photo').show();
                }
                reader.readAsDataURL(fileDp.files[0]);
            }
        } else
        {
            $('#photo').hide();
        }
    });

    $("#filesignature").change(function ()
    {
        if (checkfile("filesignature", 500))
        {
            var filesignature = $("#filesignature")[0];
            if (filesignature.files && filesignature.files[0])
            {
                var reader = new FileReader();
                reader.onload = function (e)
                {
                    $('#photoSignature').attr('src', e.target.result);
                    $('#photoSignature').show();
                }
                reader.readAsDataURL(filesignature.files[0]);
            }
        } else {
            $('#photoSignature').hide();
        }
    });

    $("#filesignatureparent").change(function ()
    {
        if (checkfile("filesignatureparent", 500))
        {
            var filesignatureparent = $("#filesignatureparent")[0];
            if (filesignatureparent.files && filesignatureparent.files[0])
            {
                var reader = new FileReader();
                reader.onload = function (e)
                {
                    $('#photoSignatureParent').attr('src', e.target.result);
                    $('#photoSignatureParent').show();
                }
                reader.readAsDataURL(filesignatureparent.files[0]);
            }
        } else {
            $('#photoSignature').hide();
        }
    });
    $.validator.addMethod('lessThanEqual', function (value, element, param) {
        return this.optional(element) || parseInt(value) <= 11
    //}, "The value {0} must be less than 12");
    }, "This value must be less than 12");

    $.validator.addMethod("dollarsscents", function (value, element) {
        return this.optional(element) || /^\d{0,4}(\.\d{0,2})?$/i.test(value);
    }, "You must include two decimal places");
    $('#admissionFormData').validate(
            {
                rules:
                        {
                            LastName: "required",
                            FirstName: "required",
                            FatherName: "required",
                            FatherLastName: "required",
                            FatherFirstName: "required",
                            FatherMiddleName: "required",
                            MotherLastName: "required",
                            MotherFirstName: "required",
                            MotherMiddleName: "required",
                            AadharNumber: {required: true, number: true, maxlength: 12, minlength: 12},
//                            PANNumber: {required: true, maxlength: 10, minlength: 10},
                            txtBirthDate: "required",
                            Nationality: "required",
                            MotherTongue: "required",
                            Religion: "required",
                            Category: "required",
                            Caste: "required",
                            SubCaste: "required",
                            CastCert: "required",
                            AadharCard: "required",
//                            PanCard: "required",
                            CurrentAddress: "required",
                            PermanentAddress: "required",
                            /*ResContactNo: "required",*/
                            MobileNumber: "required",
                            Email: {required: true, email: true},
                            GuardianMotherName: "required",
                            GuardianFatherName: "required",
                            OccupationofMother: "required",
                            OccupationofFather: "required",
                            EducationofMother: "required",
                            EducationofFather: "required",
                            GuardianAddress: "required",
                            AnnualIncome: "required",
//                            GuardianTelephoneNo: "required",
//                            GuardianOffice: "required",
                            GuardianMobile: "required",
                            GuardianEmergencyConactNo: "required",
                            GuardianEmail: {required: true, email: true},
                            NativePlaceAddress: "required",
                            "fileDp[]": {required: true},
//                            OrganisationName: "required",
//                            Designation: "required",
                            TotalExperienceInYear: {required: true},
                            TotalExperienceInMonth: {required: true, lessThanEqual: true, max: 11, min: 0},
                            BankName: "required",
                            BankAddress: "required",
                            BankAccountNumber: "required",
                            AccountType: "required",
                            IFSCCode: "required",
                            MICRNumber: "required",
                            KnowAboutCourse: "required",
//                            HobbiesOrSpecailInterest: "required",
//                            HonorPrizeName: "required",
                            //Note: "required",
                            SemPer1: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer2: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer3: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer4: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer5: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer6: {number: true, max: 100, dollarsscents: true},
                            Marksheet1: "required",
                            Marksheet2: "required",
                            Marksheet3: "required",
                            Marksheet4: "required",
                            Marksheet5: "required",
                            GPA1: "required",
                            GPA2: "required",
                            GPA3: "required",
                            GPA4: "required",
                            GPA5: "required",
                            SscYearofPassing: "required",
                            SscSchoolName: "required",
                            SscMedium: "required",
                            SscBoardName: "required",
                            SscTotalPercent: "required",
                            SscGrade: "required",
                        },
                messages:
                        {
                            LastName: "Please enter your last name.",
                            FirstName: "Please enter your first name.",
                            FatherName: "Please enter your father/husband name.",
                            FatherLastName: "Please enter your father/husband last name.",
                            FatherFirstName: "Please enter your father/husband first name.",
                            FatherMiddleName: "Please enter your father/husband middle name.",
                            MotherLastName: "Please enter your mother last name.",
                            MotherFirstName: "Please enter your mother first name.",
                            MotherMiddleName: "Please enter your mother middle name.",
                            AadharNumber: {required: "Please enter your aadhar number."},
//                            PANNumber: {required: "Please enter your PAN number."},

                            txtBirthDate: "Please enter your date of birth.",
                            "fileDp[]": "Please upload signature and photo",
                            Nationality: "Please enter your nationality.",
                            MotherTongue: "Please enter your mother tongue.",
                            Religion: "Please select your religion.",
                            Category: "Please select your Category.",
                            Caste: "Please select your caste.",
                            SubCaste: "Please select your sub-caste.",
                            CastCert: "Please upload your Caste Certificate.",
                            DomCert: "Please upload your Domicile Certificate.",
//                            PanCard: "Please upload your PAN Card PDF Format.", 
                            AadharCard: "Please upload your Aadhar Card PDF Format.",
                            CurrentAddress: "Please enter your current address.",
                            PermanentAddress: "Please enter your premanent address.",
                            ResContactNo: "Please enter your resedential contact number.",
                            MobileNumber: "Please enter your mobile number.",
                            Email: {required: "Please enter a valid email address.", email: "Please enter a valid email address."},
                            GuardianMotherName: "Please enter your guardian mother name.",
                            GuardianFatherName: "Please enter your guardian father name.",
                            OccupationofMother: "Please enter occupation of mother.",
                            OccupationofFather: "Please enter occupation of father.",
                            EducationofMother: "Please enter education of mother.",
                            EducationofFather: "Please enter education of father.",
                            GuardianAddress: "Please enter guardian address.",
                            AnnualIncome: "Please enter annual income.",
//                            GuardianTelephoneNo: "Please enter guardian telephone number.",
//                            GuardianOffice: "Please enter guardian office number.", 
                            GuardianMobile: "Please enter guardian mobile number.",
                            GuardianEmergencyConactNo: "Please enter guardian emergency contact number.",
                            GuardianEmail: {required: "Please enter a valid email address.", email: "Please enter a valid email address."},
                            NativePlaceAddress: "Please enter guardian native place address.",
//                            OrganisationName: "Please enter organisation name.",
//                            Designation: "Please enter designation.",
                            TotalExperienceInYear: "Please enter total experience in year.",
                            TotalExperienceInMonth: {required: "Please enter total experience in month.", max: "Total Experience month cannot be greather than {0}", min: "Total Experience month cannot be less than {0}"},
                            BankName: "Please enter bank name.",
                            BankAddress: "Please enter bank address.",
                            BankAccountNumber: "Please enter Bank Account Number.",
                            AccountType: "Please enter Account Type.",
                            IFSCCode: "Please enter IFSC Code.",
                            MICRNumber: "Please enter MICR Number.",
                            KnowAboutCourse: "Please select.",
//                            HobbiesOrSpecailInterest: "Please enter hobbies or special interests.",
//                            HonorPrizeName: "Please enter hoonor or prize name.",
//                            Note: "Please enter note.",
                            SemPer1: "Please enter percentage upto two decimal places.",
                            SemPer2: "Please enter percentage upto two decimal places.",
                            SemPer3: "Please enter percentage upto two decimal places.",
                            SemPer4: "Please enter percentage upto two decimal places.",
                            SemPer5: "Please enter percentage upto two decimal places.",
                            Marksheet1: "Please upload first semester marksheet",
                            Marksheet2: "Please upload second semester marksheet",
                            Marksheet3: "Please upload third semester marksheet",
                            Marksheet4: "Please upload forth semester marksheet",
                            Marksheet5: "Please upload fifth semester marksheet",
                            GPA1: "GPA is required",
                            GPA2: "GPA is required",
                            GPA3: "GPA is required",
                            GPA4: "GPA is required",
                            GPA5: "GPA is required",
                            BachelorYearofPassing: "Year is required",
                            BachelorSchoolName: "School name is required",
                            BachelorMedium: "Medium is required",
                            BachelorBoardName: "Board name is required",
                            BachelorTotalPercent: "Percentage is required",
                            BachelorGrade: "Grade is required",
                            HscYearofPassing: "Year is required",
                            HscSchoolName: "School name is required",
                            HscMedium: "Medium is required",
                            HscBoardName: "Board name is required",
                            HscTotalPercent: "Percentage is required",
                            HscGrade: "Grade is required",
                            SscYearofPassing: "Year is required",
                            SscSchoolName: "School name is required",
                            SscMedium: "Medium is required",
                            SscBoardName: "Board name is required",
                            SscTotalPercent: "Percentage is required",
                            SscGrade: "Grade is required",
                            ExaminationType: "Examination type is required",
                        },
                submitHandler: function (form)
                {
                    $.ajax({
                        dataType: "json",
                        url: "postformdata.php",
                        type: form.method,
                        data: new FormData(form),
                        processData: false,
                        contentType: false,
                        success: function (response)
                        {
                            var msg = 'Your Application ID is : '+response.ApplicationID;
                            alert(msg);
                            if (response.status == 1) {
                                var url = 'http://svt.vancotech.com/selffinance/' + response.urlD;
                                // console.log(url);
                                // window.open(url, '_blank');
                                window.location.href = url;
                            }
                            //$('#answers').html(response);
                        }
                    });
                }
            });

});

jQuery.validator.addMethod("allRequired", function(value, elem){
        // Use the name to get all the inputs and verify them
        var name = elem.name;
        return  $('input[name="'+name+'"]').map(function(i,obj){return $(obj).val();}).get().every(function(v){ return v; });
    });