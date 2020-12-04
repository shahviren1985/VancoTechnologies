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
        //$('#BachelorTotalPercent').addClass('required');
        //$('#BachelorGrade').addClass('required');
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
	$("#HscMarksheet").change(function ()
    {
       checkpdffile("HscMarksheet", 2048);
    });	
	$("#SscMarksheet").change(function ()
    {
       checkpdffile("SscMarksheet", 2048);
    });	
	$("#LeavingCertificate").change(function ()
    {
       checkpdffile("LeavingCertificate", 2048);
    });	
	$("#LeavingCertificate").change(function ()
    {
       checkpdffile("LeavingCertificate", 2048);
    });	
	$("#EligibilityCertificate").change(function ()
    {
       checkpdffile("EligibilityCertificate", 2048);
    });	
	$("#AadharCard").change(function ()
    {
       checkpdffile("AadharCard", 2048);
    });	
	$("#DomCert").change(function ()
    {
       checkpdffile("DomCert", 2048);
    });
	$("#PanCard").change(function ()
    {
       checkpdffile("PanCard", 2048);
    });
	$("#GapCertificate").change(function ()
    {
       checkpdffile("GapCertificate", 2048);
    });
	$("#RationCard").change(function ()
    {
       checkpdffile("RationCard", 2048);
    });
	$("#MigrationCertificate").change(function ()
    {
       checkpdffile("MigrationCertificate", 2048);
    });	
	$("#Marksheet1").change(function ()
    {
       checkpdffile("Marksheet1", 2048);
    });
	$("#Marksheet2").change(function ()
    {
       checkpdffile("Marksheet2", 2048);
    });
	$("#Marksheet3").change(function ()
    {
       checkpdffile("Marksheet3", 2048);
    });
	$("#Marksheet4").change(function ()
    {
       checkpdffile("Marksheet4", 2048);
    });
	$("#Marksheet5").change(function ()
    {
       checkpdffile("Marksheet5", 2048);
    });	
	$("#Marksheet6").change(function ()
    {
       checkpdffile("Marksheet6", 2048);
    });
	
	
	
    $.validator.addMethod('lessThanEqual', function (value, element, param) {
        return this.optional(element) || parseInt(value) <= 11
        //}, "The value {0} must be less than 12");
    }, function (param, element) {
        return 'Spcical Characters are not allow'
    });

    $.validator.addMethod('speicalChar', function (value, element, param) {
        return this.optional(element) || /^[A-Za-z0-9 ]*$/i.test(value);
    }, "Speical Characters are not allow.");

    $.validator.addMethod("dollarsscents", function (value, element) {
        return this.optional(element) || /^\d{0,4}(\.\d{0,2})?$/i.test(value);
    }, "You must include two decimal places");
    $('#admissionFormData').validate(
            {
                rules:
                        {
                            LastName: {required: true, speicalChar: true},
                            FirstName:  {required: true, speicalChar: true},
                            FatherName:  {required: true, speicalChar: true},
                            FatherLastName:  {required: true, speicalChar: true},
                            FatherFirstName:  {required: true, speicalChar: true},
                            FatherMiddleName:  {required: true, speicalChar: true},
                            MotherLastName:  {required: true, speicalChar: true},
                            MotherFirstName:  {required: true, speicalChar: true},
                            MotherMiddleName:  {required: true, speicalChar: true},
                            AadharNumber: {required: true, number: true, maxlength: 12, minlength: 12},
                            //PANNumber: {required: true, maxlength: 10, minlength: 10},
                            txtBirthDate: "required",
                            Nationality: {required: true, speicalChar: true},
                            MotherTongue: {required: true, speicalChar: true},
                            Religion: "required",
                            Category: "required",
                            Caste: "required",
                            SubCaste: {required: true, speicalChar: true},
                            CastCert: "required",
                            AadharCard: "required",
                            HscMarksheet: "required",
                            SscMarksheet: "required",
                            PanCard: {required: false, speicalChar: true},
                            CurrentAddress: {required: true, speicalChar: true},
                            PermanentAddress: {required: true, speicalChar: true},
                            ResContactNo: "required",
                            MobileNumber: "required",
                            Email: {required: true, email: true},
                            GuardianMotherName: {required: true, speicalChar: true},
                            GuardianFatherName: {required: true, speicalChar: true},
                            OccupationofMother: {required: true, speicalChar: true},
                            OccupationofFather: {required: true, speicalChar: true},
                            EducationofMother: {required: true, speicalChar: true},
                            EducationofFather: {required: true, speicalChar: true},
                            GuardianAddress: {required: true, speicalChar: true},
                            AnnualIncome: {required: true, speicalChar: true},
                            //GuardianTelephoneNo: "required",
                            //GuardianOffice: "required",
                            GuardianMobile: "required",
                            GuardianEmergencyConactNo: "required",
                            GuardianEmail: {required: true, email: true},
                            NativePlaceAddress: {required: false, speicalChar: true},
                            "fileDp[]": {required: true},
                            OrganisationName: {required: false, speicalChar: true},
                            Designation: {required: false, speicalChar: true},
                            //TotalExperienceInYear: {required: true},
                            TotalExperienceInMonth: {lessThanEqual: true, max: 11, min: 1},
                            BankName: {required: true, speicalChar: true},
                            BankAddress: {required: true, speicalChar: true},
                            BankAccountNumber: {required: true, speicalChar: true},
                            AccountType: {required: true, speicalChar: true},
                            IFSCCode: {required: true, speicalChar: true},
                            MICRNumber: {required: true, speicalChar: true},
                            KnowAboutCourse: "required",
                            HobbiesOrSpecailInterest: {required: false, speicalChar: true},
                            HonorPrizeName: {required: false, speicalChar: true},
                            Note: {required: false, speicalChar: true},
                            SemPer1: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer2: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer3: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer4: {required: true, number: true, max: 100, dollarsscents: true},
                            SemPer5: {number: true, max: 100, dollarsscents: true},
                            SemPer6: {number: true, max: 100, dollarsscents: true},
                            Marksheet1: "required",
                            Marksheet2: "required",
                            Marksheet3: "required",
                            Marksheet4: "required",
                            //Marksheet5: "required",
                            GPA1: "required",
                            GPA2: "required",
                            GPA3: "required",
                            GPA4: "required",
                            //GPA5: "required",
                            BachelorYearofPassing: {required: true, speicalChar: true},
                            BachelorSchoolName: {required: true, speicalChar: true},
                            BachelorMedium: {required: true, speicalChar: true},
                            BachelorBoardName: {required: true, speicalChar: true},
                            //BachelorTotalPercent: {required: true},
                            //BachelorGrade: {required: true},
                            HscYearofPassing: {required: true, speicalChar: true},
                            HscSchoolName: {required: true, speicalChar: true},
                            HscMedium: {required: true, speicalChar: true},
                            HscBoardName: {required: true, speicalChar: true},
                            HscTotalPercent: {required: true},
                            HscGrade: {required: true},
                            
                            SscYearofPassing: {required: true, speicalChar: true},
                            SscSchoolName: {required: true, speicalChar: true},
                            SscMedium: {required: true, speicalChar: true},
                            SscBoardName: {required: true, speicalChar: true},
                            SscTotalPercent: "required",
                            SscGrade: "required",
                        },
                messages:
                        {
                            LastName: {required: "Please enter your last name.", speicalChar: "Special Characters are not allowed"},
                            FirstName: {required:"Please enter your first name.", speicalChar: "Special Characters are not allowed"},
                            FatherName: {required:"Please enter your father/husband name.",speicalChar: "Special Characters are not allowed"},
                            FatherLastName: {required:"Please enter your father/husband last name.",speicalChar: "Special Characters are not allowed"},
                            FatherFirstName: {required:"Please enter your father/husband first name.",speicalChar: "Special Characters are not allowed"},
                            FatherMiddleName: {required:"Please enter your father/husband middle name.",speicalChar: "Special Characters are not allowed"},
                            MotherLastName: {required:"Please enter your mother last name.",speicalChar: "Special Characters are not allowed"},
                            MotherFirstName: {required:"Please enter your mother first name.",speicalChar: "Special Characters are not allowed"},
                            MotherMiddleName: {required:"Please enter your mother middle name.",speicalChar: "Special Characters are not allowed"},
                            AadharNumber: {required: "Please enter your aadhar number."},
                            //PANNumber: 
                            txtBirthDate: "Please enter your date of birth.",
                            "fileDp[]": "Please upload signature and photo",
                            Nationality: {required: "Please enter your nationality.",speicalChar: "Special Characters are not allowed"},
                            MotherTongue: {required: "Please enter your mother tongue.",speicalChar: "Special Characters are not allowed"},
                            Religion: "Please select your religion.",
                            Category: "Please select your Category.",
                            Caste: "Please select your caste.",
                            SubCaste: {required:"Please select your sub-caste.",speicalChar: "Special Characters are not allowed"},
                            CastCert: "Please upload your Caste Certificate.",
                            DomCert: "Please upload your Domicile Certificate.",
                            PanCard: {speicalChar: "Special Characters are not allowed"},
                            AadharCard: "Please upload your Aadhar Card in PDF Format.",
                            HscMArksheet: "Please upload your HSC Marksheet in PDF Format.",
                            SscMArksheet: "Please upload your SSC Marksheet in PDF Format.",
                            CurrentAddress: {required:"Please enter your current address.",speicalChar: "Special Characters are not allowed"},
                            PermanentAddress: {required:"Please enter your premanent address.",speicalChar: "Special Characters are not allowed"},
                            ResContactNo: "Please enter your resedential contact number.",
                            MobileNumber: "Please enter your mobile number.",
                            Email: {required: "Please enter a valid email address.", email: "Please enter a valid email address."},
                            GuardianMotherName: {required:"Please enter your guardian mother name.",speicalChar: "Special Characters are not allowed"},
                            GuardianFatherName: {required:"Please enter your guardian father name.",speicalChar: "Special Characters are not allowed"},
                            OccupationofMother: {required:"Please enter occupation of mother.",speicalChar: "Special Characters are not allowed"},
                            OccupationofFather: {required:"Please enter occupation of father.",speicalChar: "Special Characters are not allowed"},
                            EducationofMother: {required:"Please enter education of mother.",speicalChar: "Special Characters are not allowed"},
                            EducationofFather: {required:"Please enter education of father.",speicalChar: "Special Characters are not allowed"},
                            GuardianAddress: {required:"Please enter guardian address.",speicalChar: "Special Characters are not allowed"},
                            AnnualIncome: {required:"Please enter annual income.",speicalChar: "Special Characters are not allowed"},
                            //GuardianTelephoneNo: "Please enter guardian telephone number.",
                            //GuardianOffice: "Please enter guardian office number.", 
                            GuardianMobile: "Please enter guardian mobile number.",
                            GuardianEmergencyConactNo: "Please enter guardian emergency contact number.",
                            GuardianEmail: {required: "Please enter a valid email address.", email: "Please enter a valid email address."},
                            NativePlaceAddress: {required:"Please enter guardian native place address.",speicalChar: "Special Characters are not allowed"},
                            OrganisationName: {speicalChar: "Special Characters are not allowed"},
                            Designation: {speicalChar: "Special Characters are not allowed"},
                            TotalExperienceInYear: "Please enter total experience in year.",
                            TotalExperienceInMonth: {required: "Please enter total experience in month.", max: "Total Experience month cannot be greather than {0}", min: "Total Experience month cannot be less than {0}"},
                            BankName: {required:"Please enter bank name.",speicalChar: "Special Characters are not allowed"},
                            BankAddress: {required:"Please enter bank address.",speicalChar: "Special Characters are not allowed"},
                            BankAccountNumber: {required:"Please enter Bank Account Number.",speicalChar: "Special Characters are not allowed"},
                            AccountType: {required:"Please enter Account Type.",speicalChar: "Special Characters are not allowed"},
                            IFSCCode: {required:"Please enter IFSC Code.",speicalChar: "Special Characters are not allowed"},
                            MICRNumber: {required:"Please enter MICR Number.",speicalChar: "Special Characters are not allowed"},
                            KnowAboutCourse: "Please select.",
                            HobbiesOrSpecailInterest: {speicalChar: "Special Characters are not allowed"},
                            HonorPrizeName: {speicalChar: "Special Characters are not allowed"},
                            Note: {speicalChar: "Special Characters are not allowed"},
                            SemPer1: "Please enter percentage upto two decimal places.",
                            SemPer2: "Please enter percentage upto two decimal places.",
                            SemPer3: "Please enter percentage upto two decimal places.",
                            SemPer4: "Please enter percentage upto two decimal places.",
                            //SemPer5: "Please enter percentage upto two decimal places.",
                            Marksheet1: "Please upload first semester marksheet",
                            Marksheet2: "Please upload second semester marksheet",
                            Marksheet3: "Please upload third semester marksheet",
                            Marksheet4: "Please upload forth semester marksheet",
                            //Marksheet5: "Please upload fifth semester marksheet",
                            GPA1: "GPA is required",
                            GPA2: "GPA is required",
                            GPA3: "GPA is required",
                            GPA4: "GPA is required",
                            //GPA5: "GPA is required",
                            BachelorYearofPassing: "Year is required",
                            BachelorSchoolName: {required:"School name is required",speicalChar: "Special Characters are not allowed"},
                            BachelorMedium: {required:"Medium is required",speicalChar: "Special Characters are not allowed"},
                            BachelorBoardName: {required:"Board name is required",speicalChar: "Special Characters are not allowed"},
                            BachelorTotalPercent: "Percentage is required",
                            BachelorGrade: "Grade is required",
                            HscYearofPassing: "Year is required",
                            HscSchoolName: {required:"School name is required",speicalChar: "Special Characters are not allowed"},
                            HscMedium: {required:"Medium is required",speicalChar: "Special Characters are not allowed"},
                            HscBoardName: {required:"Board name is required",speicalChar: "Special Characters are not allowed"},
                            HscTotalPercent: "Percentage is required",
                            HscGrade: "Grade is required",
                            SscYearofPassing: "Year is required",
                            SscSchoolName: {required:"School name is required",speicalChar: "Special Characters are not allowed"},
                            SscMedium: {required:"Medium is required",speicalChar: "Special Characters are not allowed"},
                            SscBoardName: {required:"Board name is required",speicalChar: "Special Characters are not allowed"},
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
                            var msg = 'Your Application ID is : ' + response.ApplicationID;
                            alert(msg);
                            if (response.status == 1) {
                                var url = 'https://svt.vancotech.com/selffinance/' + response.urlD;
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

jQuery.validator.addMethod("allRequired", function (value, elem) {
    // Use the name to get all the inputs and verify them
    var name = elem.name;
    return  $('input[name="' + name + '"]').map(function (i, obj) {
        return $(obj).val();
    }).get().every(function (v) {
        return v;
    });
});