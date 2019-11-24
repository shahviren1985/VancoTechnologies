
function scroll_to_class(element_class, removed_height) {
    var scroll_to = $(element_class).offset().top - removed_height;
    if ($(window).scrollTop() != scroll_to) {
        $('html, body').stop().animate({ scrollTop: scroll_to }, 0);
    }
}

function bar_progress(progress_line_object, direction) {
    var number_of_steps = progress_line_object.data('number-of-steps');
    var now_value = progress_line_object.data('now-value');
    var new_value = 0;
    if (direction == 'right') {
        new_value = now_value + (100 / number_of_steps);
    }
    else if (direction == 'left') {
        new_value = now_value - (100 / number_of_steps);
    }
    progress_line_object.attr('style', 'width: ' + new_value + '%;').data('now-value', new_value);
}

jQuery(document).ready(function () {
    $('.f1 fieldset:first').fadeIn('slow');

    $("input[type=checkbox]").on("click", function () {
        var className = this.className;
        var checkedPref = $(this).is(":checked");
        var classNameList = className.split(" ");
        if (classNameList.length > 1) {

            //Check row wise
            $('.' + classNameList[0]).each(function (x, chk) {
                var chkClassName = chk.className;
                var chkClassNameList = chk.className.split(" ");
                if (checkedPref == true && chkClassName != className) {
                    $(this).attr("disabled", true);
                }
                else {
                    if ($('.' + chkClassNameList[1] + ':checked').length == 0) {
                        $(this).removeAttr("disabled");
                    }
                }
                $(this).parent().removeClass('divErroCheckbox input-error');
            });

            //Check column wise
            $('.' + classNameList[1]).each(function (x, chk) {
                var chkClassName = chk.className;
                var chkClassNameList = chk.className.split(" ");
                if (checkedPref == true && chkClassName != className) {
                    $(this).attr("disabled", true);
                }
                else {
                    if ($('.' + chkClassNameList[0] + ':checked').length == 0) {
                        $(this).removeAttr("disabled");
                    }
                }
                $(this).parent().removeClass('divErroCheckbox input-error');
            });
        }
    });

    $('.f1 input[type="text"], .f1 input[type="password"], .f1 textarea').on('focus', function () {
        $(this).removeClass('input-error');
    });

    $('.f1 input[type="file"]').on('focus', function () {
        $(this).parent().removeClass('divradio input-error');
    });


    $('.f1').on('keyup', function (e) {
        CheckStepsData();
    });
    $('.f1').on('blur', function (e) {
        CheckStepsData();
    });
    $('.f1').on('click', function (e) {
        CheckStepsData();
    }); 
});

function CheckStepsData() {

    $("fieldset").each(function () {
        var errorInInput = false;
        var fieldId = "#" + $(this).attr("id");
        $(this).find('input[type="text"], input[type="file"], textarea,select').filter(':visible').each(function (x, ed) {
            if (ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
                errorInInput = true;
            }
        });
        $(this).find("input:radio").filter(':visible').each(function () {
            var name = $(this).attr("name");
            if ($("input:radio[name=" + name + "]:checked").length == 0) {
                errorInInput = true;
            }
        });
        $(this).find("input[type=checkbox]").filter(':visible').each(function () {
            var className = this.className;
            var classNameList = className.split(" ");
            if (classNameList.length > 1) {
                if ($('.' + classNameList[0] + ':visible').is("checked").length == 0) {
                    $('.' + classNameList[0]).each(function (x, chk) {
                        errorInInput = true;
                    });
                }
                if ($('.' + classNameList[1] + ':visible').is("checked").length == 0) {
                    $('.' + classNameList[1]).each(function (x, chk) {
                        errorInInput = true;
                    });
                }
            }
        });
        var fieldIdSplit = fieldId.split("_");
        if (fieldIdSplit.length > 1) {
            if (errorInInput == false) {
                $("#divStep" + fieldIdSplit[1]).addClass("active");
            }
            else {
                $("#divStep" + fieldIdSplit[1]).removeClass("active");
            }
        }
    });
}

function CheckValidation(name) {    
    var errorInInput = false;
    $('.f1').find('input[type="text"], textarea,select').filter(':visible').each(function (x, ed) {
        if (ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
            errorInInput = true;
            $('#' + ed.id).addClass('input-error');
        }
        else {
            $('#' + ed.id).removeClass('input-error');
        }
    });

    $('.f1').find('input[type="file"]').filter(':visible').each(function (x, ed) {
        if (ed.name = 'fileDp' && $('#hdnPhoto').val() == "" && ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
            $('#' + ed.id).addClass('divradio input-error'); $('#' + ed.id).focus(); errorInInput = true;
        }
        else if (parseInt($('#CalculatedAge').val()) < 18 &&
            (ed.name = 'filesignatureparent' && $('#hdnParentSignature').val() == "" && ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "")) {
            errorInInput = true; $('#' + ed.id).addClass('divradio input-error');
        }
        else if ((ed.name = 'filesignatureparent' && $('#hdnParentSignature').val() == "" && ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") == false
            && parseInt($('#CalculatedAge').val()) >= 18 &&
            (ed.name = 'filesignature' && $('#hdnSignature').val() == "" && ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "")) {
            errorInInput = true; $('#' + ed.id).addClass('divradio input-error');
        }
        else {
            $(this).parent().removeClass('divradio input-error');
        }
    });

    $('.f1').find("input:radio").filter(':visible').each(function (x, ed) {
        var name = ed.name;
        if ($("input:radio[name=" + name + "]:checked").length == 0) {
            errorInInput = true;
            $(this).parent().addClass('divradio input-error');
            $('#' + ed.id).focus();
        }
        else {
            $(this).parent().removeClass('divradio input-error');
        }
    });

    if ($('#CalculatedAge').val() != undefined && $('#CalculatedAge').val() != "") { 
        if (parseInt($('#CalculatedAge').val()) > 18) {
            $('.f1').find('input[type="text"], textarea,select').filter(':visible').each(function (x, ed) {
                if (ed.className.indexOf("guardian") != -1) {
                    $('#' + ed.id).removeClass('input-error');
                }
            });
        }
        else {
            $('.f1').find('input[type="text"], textarea,select').filter(':visible').each(function (x, ed) {
                if (ed.className.indexOf("guardian") != -1) {
                    if (ed.value.trim() == "") {
                        errorInInput = true;
                        $('#' + ed.id).addClass('input-error');
                    }
                    else {
                        $('#' + ed.id).removeClass('input-error');
                    }
                }
            });
        }
    }

    if ($('#Email').val() != "") {
        if (!isEmail($('#Email').val())) {
            $('#Email').addClass('input-error');
            errorInInput = true;
        }
    }

    if ($('#GuardianEmail').val() != "") {
        if (!isEmail($('#GuardianEmail').val())) {
            $('#GuardianEmail').addClass('input-error');
            errorInInput = true;
        }
    }

    if (name != "" && name != undefined) {
        errorInInput = CheckCourseWiseValidation(name);
    }

    return errorInInput;
}

function CheckCourseWiseValidation(name) {

    var errorInInput = false;
    var className = null;
    var subclass = null;

    //id wise remove the validation
    RemoveCoruseValidation('ssc');
    RemoveCoruseValidation('hsc');
    RemoveCoruseValidation('bachelor');

    //check Course Value wise validation
    if (name == 'MASTERS IN SPECIALIZED DEITETICS' || name == 'MASTERS IN FASHION DESIGN') {
        className = 'validatemaontwo';
        subclass = 'err1';
        $('#ExaminationType').addClass('input-error');
    }
    else if (name == 'DIPLOMA IN FASHION DESIGN') {
        className = 'validatemaontwo';
        subclass = 'err2';
    }
    else if (name == 'CERTIFICATE AND DIPLOMA IN COMPUTER AIDED INTERIOR DESIGN MANAGEMENT') {
        className = 'ssc';
        subclass = 'err3';
    }

    if (name != undefined && name != '') {
        $('.' + className + '').find('input[type="text"]').filter(':visible').each(function (x, ed) {
            if (ed.className.indexOf(subclass) != -1 && ed.value.trim() == "") {
                errorInInput = true;
                $('#' + ed.id).addClass('input-error');
            }
            else
            {
                $('#' + ed.id).removeClass('input-error');
            }
        });
    }
 
    if ($('#PGTotalPercent').val() != "" && $('#PGTotalPercent').val() != undefined)
    {
        if (parseInt($('#PGTotalPercent').val()) < 30)
        {
            errorInInput = true;
            $('#PGTotalPercent').addClass('input-error');
        }
        else
        {
            $('#PGTotalPercent').removeClass('input-error');
        }
    }

    if ($('#BachelorTotalPercent').val() != "" && $('#BachelorTotalPercent').val() != undefined)
    {
        if (parseInt($('#BachelorTotalPercent').val()) < 30)
        {
            errorInInput = true;
            $('#BachelorTotalPercent').addClass('input-error');
        }
        else
        {
            $('#BachelorTotalPercent').removeClass('input-error');
        }
    }

    if ($('#HscTotalPercent').val() != "" && $('#HscTotalPercent').val() != undefined)
    {
        if (parseInt($('#HscTotalPercent').val()) < 30)
        {
            errorInInput = true;
            $('#HscTotalPercent').addClass('input-error');
        }
        else
        {
            $('#HscTotalPercent').removeClass('input-error');
        }
    }

    if ($('#SscTotalPercent').val() != "" && $('#SscTotalPercent').val() != undefined)
    {
        if (parseInt($('#SscTotalPercent').val()) < 30)
        {
            errorInInput = true;
            $('#SscTotalPercent').addClass('input-error');
        }
        else
        {
            $('#SscTotalPercent').removeClass('input-error');
        }
    }

    if (name == 'MASTERS IN SPECIALIZED DEITETICS' || name == 'MASTERS IN FASHION DESIGN')
    {
        if ($('#PGTotalPercent').val() != "" || $('#PGTotalPercent').val() != undefined)
        {
            if ($('#PGTotalPercent').val() != "" && $('#PGTotalPercent').val() < 55)
            {
                $("#ErrorMessage").html('PG percentage must be require greater than or eqal to 55.');
                $('#PGTotalPercent').addClass('input-error');
                errorInInput = true;
            }
            else
            {
                $('#PGTotalPercent').removeClass('input-error');
            }
        }

        if ($('#BachelorTotalPercent').val() != "")
        {
            if ($('#BachelorTotalPercent').val() < 60)
            {
                $("#ErrorMessage").html('Bachelor percentage must be require greater than or eqal to 60.');
                $('#BachelorTotalPercent').addClass('input-error');
                errorInInput = true;
            }
            else
            {
                $('#BachelorTotalPercent').removeClass('input-error');
            }
        }
        else
        {
            $('#BachelorTotalPercent').addClass('input-error');
        }
        var examType = $('#ExaminationType option:selected').val();

        if (examType != "" && examType != undefined)
        {
            if (examType == 'OTHER' && $('#OtherExaminationType').val() == "")
            {
                $('#OtherExaminationType').addClass('input-error');
                errorInInput = true;
            }
            else
            {
                $('#OtherExaminationType').removeClass('input-error');
            }
            $('#ExaminationType').removeClass('input-error');
        }
        else
        {
            $('#ExaminationType').addClass('input-error');
        }
    }
    return errorInInput;
}

function RemoveCoruseValidation(name) {
    $('#OtherExaminationType').removeClass('input-error');
    $('#BachelorTotalPercent').removeClass('input-error');
    $('#' + name + 'ExaminationType').removeClass('input-error');
    $('#' + name + 'YearofPassing').removeClass('input-error');
    $('#' + name + 'SchoolName').removeClass('input-error');
    $('#' + name + 'Medium').removeClass('input-error');
    $('#' + name + 'BoardName').removeClass('input-error');
    $('#' + name + 'TotalPercent').removeClass('input-error');
    $('#' + name + 'Grade').removeClass('input-error');
    $('#ExaminationType').removeClass('input-error');
}

function CheckWithoutSubmitValidation(name) {
    var errorInInput = false;
    $('.f1').find('input[type="text"], textarea,select').filter(':visible').each(function (x, ed) {
        if (ed.className.indexOf("mandatory") != -1 && ed.value.trim() == "") {
            errorInInput = true;
            $('#' + ed.id).addClass('input-error');
        }
        else { $('#' + ed.id).removeClass('input-error'); }
    });
    return errorInInput;
}