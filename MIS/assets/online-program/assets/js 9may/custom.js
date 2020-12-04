$(document).ready(function ()
{

    var $radios = $('input:radio[name=isSvt]');
    if ($radios.is(':checked') === false) {
        $radios.filter('[value=No]').prop('checked', true);
    }

    var selectedcourse = $("#CourseName").val();
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
        } else
        {
            $("#DivIsSvtSpecification").hide();
        }
    });
    if (selectedcourse == 3 || selectedcourse == 4)
    {
        $('#ExaminationType').val("OTHER").trigger('change');
        $('#OtherExaminationType').show();
        $('#OtherExaminationType').addClass('input-error');
    }
    $("#CourseNameddn").attr("disabled", "disabled");
    $("#category1").prop("checked", true);
    $("#category1").on("click", function ()
    {
        $("#divReservedCategory").hide();
        $("select[id*=Caste]").rules("remove", "required");
        $("input[id*=SubCaste]").rules("remove", "required");
        $("#divCasteCertificate").hide;
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
            $("textarea[id*=PermanentAddress]").rules("add", "required");
        }
    });
    if ($('#Samepermanentaddress').is(':checked') == true)
    {
        $('#textInput').prop('disabled', true);
    }
    $("#txtBirthDate").datepicker(
            {
                changeMonth: true,
                changeYear: true,
                yearRange: '1960:2001',
                dateFormat: 'dd-MM-yy',
                setDate: new Date(),
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
                            PANNumber: {required: true, maxlength: 10, minlength: 10},
                            txtBirthDate: "required",
                            Nationality: "required",
                            MotherTongue: "required",
                            Religion: "required",
                            Category: "required",
                            Caste: "required",
                            SubCaste: "required",
                            CastCert: "required",
                            AadharCard: "required",
                            PanCard: "required",
                            CurrentAddress: "required",
                            PermanentAddress: "required",
                            ResContactNo: "required",
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
                            GuardianTelephoneNo: "required",
                            GuardianOffice: "required",
                            GuardianMobile: "required",
                            GuardianEmergencyConactNo: "required",
                            GuardianEmail: {required: true, email: true},
                            NativePlaceAddress: "required",
                            OrganisationName: "required",
                            Designation: "required",
                            TotalExperienceInYear: "required",
                            TotalExperienceInMonth: "required",
                            BankName: "required",
                            BankAddress: "required",
                            BankAccountNumber: "required",
                            AccountType: "required",
                            IFSCCode: "required",
                            MICRNumber: "required",
                            KnowAboutCourse: "required",
                            HobbiesOrSpecailInterest: "required",
                            HonorPrizeName: "required",
                            Note: "required",
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
                            PANNumber: {required: "Please enter your PAN number."},
                            txtBirthDate: "Please enter your date of birth.",
                            Nationality: "Please enter your nationality.",
                            MotherTongue: "Please enter your mother tongue.",
                            Religion: "Please select your religion.",
                            Category: "Please select your Category.",
                            Caste: "Please select your caste.",
                            SubCaste: "Please select your sub-caste.",
                            CastCert: "Please upload your Caste Certificate.",
                            DomCert: "Please upload your Domicile Certificate.",
                            PanCard: "Please upload your PAN Card PDF Format.",
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
                            GuardianTelephoneNo: "Please enter guardian telephone number.",
                            GuardianOffice: "Please enter guardian office number.",
                            GuardianMobile: "Please enter guardian mobile number.",
                            GuardianEmergencyConactNo: "Please enter guardian emergency contact number.",
                            GuardianEmail: {required: "Please enter a valid email address.", email: "Please enter a valid email address."},
                            NativePlaceAddress: "Please enter guardian native place address.",
                            OrganisationName: "Please enter organisation name.",
                            Designation: "Please enter designation.",
                            TotalExperienceInYear: "Please enter total experience in year.",
                            TotalExperienceInMonth: "Please enter total experience in month.",
                            BankName: "Please enter bank name.",
                            BankAddress: "Please enter bank address.",
                            BankAccountNumber: "Please enter Bank Account Number.",
                            AccountType: "Please enter Account Type.",
                            IFSCCode: "Please enter IFSC Code.",
                            MICRNumber: "Please enter MICR Number.",
                            KnowAboutCourse: "Please select.",
                            HobbiesOrSpecailInterest: "Please enter hobbies or special interests.",
                            HonorPrizeName: "Please enter hoonor or prize name.",
                            Note: "Please enter note.",
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
                            alert(response.message);
                            //$('#answers').html(response);
                        }
                    });
                }
            });

});