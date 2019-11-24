
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

    $(".divInfoClick").click(function () {
        var spl = $(this).attr("data-specialisation");
        $("#ge-desc-modal .modal-header").html($(this).attr("data-specialisation") + " - Specialisation Description");

        var popup;
        var FND = '<table border="1"><tr><td colspan="3" style="text-align: center"><b>General Elective Courses offered by Department of Food and Nutrition</b></td></tr><tr><td style="text-align: center">Nutrition for Health Promotion</td><td style="border: none">&nbsp;</td><td style="text-align: center">Fundamentals of Baking</td></tr><tr><td style="text-align: left; padding: 10px">Do you want to be fit and healthy? This course provides substantial information on the importance of health, diet, nutrition and fitness. It will help students to ensure wellbeing of self and family. It emphasizes the significance of a healthy, nutritious and balanced diet in achieving good health and fitness for life.</td><td rowspan="2" style="text-align: center; padding: 20px;">OR</td><td style="text-align: left; padding: 10px">This is an attractive hands on training course to develop skills in preparation of baked items like bread, biscuits etc. The course will provide scientific insights and develop skill, art and craft of baking.</td></tr></table>';
        var DC = '<table border="1"><tr><td colspan="3" style="text-align: center"><b>General Elective Courses offered by Department of Human Development</b></td></tr><tr><td style="text-align: center">Personal and Professional Relationships  in Adulthood</td><td style="border: none">&nbsp;</td><td style="text-align: center">Marriage and Parenting</td></tr><tr><td style="text-align: left; padding: 10px">Our interpersonal relations are greatly linked with our overall success in life. This subject focuses on understanding the what, why and how of relationships. Choosing this subject will help you to better understand and maintain your relationships at the personal and professional front. It will also focus on topics like emotional intelligence, team work and conflict resolutions.</td><td rowspan="2" style="text-align: center; padding: 20px;">OR</td><td style="text-align: left; padding: 10px">This is an applied course, designed to help students explore the many facets of marriage, parenting and family interactions. Few of the topics discussed are courtship, mate selection, adjustments in marriage and parenting children of different ages. This subject will also focus on  your own attitudes, values, and expectations about marriage and family life through sensitization games, role plays and discussions</td></tr></table>';
        var TAD = '<table border="1"><tr><td colspan="3" style="text-align: center"><b>General Elective Courses offered by Department of Textiles and Apparel Designing</b></td></tr><tr><td style="text-align: center">Fashion Craft</td><td style="border: none">&nbsp;</td><td style="text-align: center">Fundamentals of Fashion</td></tr><tr><td style="text-align: left; padding: 10px">This course provides students the required skills in various surface embellishment techniques such as smocking, painting, bead work and macramé. The course will also help students in the selection of appropriate material for developing different types of fashion accessories. Students will be proficient in developing contemporary fashion articles and other related textiles crafts such as bead work, designing of earrings, bracelets, necklace, anklets and many more.</td><td rowspan="2" style="text-align: center; padding: 20px;">OR</td><td style="text-align: left; padding: 10px">This course offers students an opportunity to understand about the concepts of fashion designing and fashion styling. This course will give an insight to students about the elements of fashion, stages of fashion cycle, and concepts of garment construction. In addition, students will be competent in selection of appropriate fashion garment and accessories according to personality, figure types and occasions.</td></tr></table>';
        var MCE = '<table border="1"><tr><td colspan="3" style="text-align: center"><b>General Elective Courses offered by Department of Mass Communication and Extension</b></td></tr><tr><td style="text-align: center">Digital Photography</td><td style="border: none">&nbsp;</td><td style="text-align: center">Voice Culture and Modulation</td></tr><tr><td style="text-align: left; padding: 10px">Cameras have become a part of our lives today. The option of taking good and creative pictures is still a dream far-fetched for many. Most of us are unaware of the aesthetics and techniques involved in clicking right image. The said course will not only teach you to handle a DSLR ( Digital Single Lens Reflex) camera but also introduce you to  various professional aspects of photography. These will enable you to capture appropriate images in your area of specialization</td><td rowspan="2" style="text-align: center; padding: 20px;">OR</td><td style="text-align: left; padding: 10px">Speak like a Leader- This is a unique subject for the students who wish to enhance the quality of their voice.  The students would be trained to speak with clarity and correct pronunciation so that they become impactful speakers. The curriculum offers knowledge and skill training in developing their voice potential to the maximum. The focus would be on voice modulation, overcoming common speaking defects, breath control, modification and improvement of voice, good narration techniques, and projection of voice, better breath control, overcoming speech defects, and public speaking.</td></tr></table>';
        var RM = '<table border="1"><tr><td colspan="3" style="text-align: center"><b>General Elective Courses offered by Department of Resource Management</b></td></tr><tr><td style="text-align: center">Consumer Education and Financial Literacy</td><td style="border: none">&nbsp;</td><td style="text-align: center">Travel Arrangements and Formalities</td></tr><tr><td style="text-align: left; padding: 10px">This course offers students an opportunity to understand the rights and responsibilities as consumers. The course will give them insight on manufacturing and trade malpractices and enable them to understand various redressal mechanisms. They will develop critical thinking skills with regards to financial planning by learning income management, savings and banking facilities. It will help them gain knowledge about various credit facilities in finance.</td><td rowspan="2" style="text-align: center; padding: 20px;">OR</td><td style="text-align: left; padding: 10px">This course offers students an orientation to the prominent tourist destinations in India and abroad, as well as the basis for travel arrangements for a leisure tour. Students become aware about the passport, VISA and other documentation and requisite processes for international travel. They also learn the procedures involved in booking of transport and accommodation. This course will enable the students to help family and friends to choose holiday destinations and basic tour planning.</td></tr></table>';
        switch (spl) {
            case "Developmental Counselling":
                popup = MCE + "<br/>" + TAD;
                break;
            case "Early Childhood care & Education":
                popup = MCE + "<br/>" + TAD;
                break;
            case "Food, Nutrition and Dietetics":
                popup = RM + "<br/>" + DC;
                break;
            case "Hospitality & Tourism Management":
                popup = FND + "<br/>" + TAD;
                break;
            case "Interior Design & Resource Management":
                popup = FND + "<br/>" + TAD;
                break;
            case "Mass Communication & Extension":
                popup = FND + "<br/>" + TAD;
                break;
            case "Textiles & Apparel Designing":
                popup = MCE + "<br/>" + HD;
                break;
        }

        $("#ge-desc-modal .modal-body").html(popup);

    });

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

function CheckValidation() {
    var errorInInput = false;
    $('.f1').find('input[type="text"], textarea,select').filter(':visible').each(function (x, ed) {
        if (ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
            errorInInput = true;
            $('#' + ed.id).addClass('input-error');
            // $('#' + ed.id).focus();
        }
        else {
            $('#' + ed.id).removeClass('input-error');
        }
    });

    $('.f1').find('input[type="file"]').filter(':visible').each(function (x, ed) {
        //if (ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
        //    errorInInput = true;
        //    $(this).parent().addClass('divradio input-error');
        //}
        //else {
        //    $(this).parent().addClass('divradio input-error');
        //} 
        if (ed.name = 'fileDp' && $('#hdnPhoto').val() == "" && ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
            $('#' + ed.id).addClass('divradio input-error');
            $('#' + ed.id).focus();
            errorInInput = true;
        }
        else if (ed.name = 'filesignature' && $('#hdnSignature').val() == "" && ed.className.indexOf("validate-input") != -1 && ed.value.trim() == "") {
            errorInInput = true;
            $('#' + ed.id).addClass('divradio input-error');
        }
        else {
            $(this).parent().removeClass('divradio input-error');
        }
    });

    $('.f1').find("input:radio").filter(':visible').each(function (x, ed) {

        var name = ed.name;// $('').attr("name");
        if ($("input:radio[name=" + name + "]:checked").length == 0) {
            //if ($('input:radio[name="+ name + "]:checked').length == 0) {
            errorInInput = true;
            $(this).parent().addClass('divradio input-error');
            $('#' + ed.id).focus();
        }
        else {
            $(this).parent().removeClass('divradio input-error');
        }
    });

    $('.f1').find("input[type=checkbox]").filter(':visible').each(function (x, ed) {
        var className = ed.className;// this.className;
        var classNameList = className.split(" ");
        if (classNameList.length > 1) {
            // if ($('.' + classNameList[0] + ':visible').is("checked").length == 0) {
            if ($('.' + classNameList[0] + ':checked').length == 0) {
                $('.' + classNameList[0]).each(function (x, chk) {
                    $(this).parent().addClass('divErroCheckbox input-error');
                    errorInInput = true;
                });
            }
            if ($('.' + classNameList[1] + ':checked').length == 0) {
                //if ($('.' + classNameList[1] + ':visible').is("checked").length == 0) {
                $('.' + classNameList[1]).each(function (x, chk) {
                    $(this).parent().addClass('divErroCheckbox input-error');
                    errorInInput = true;
                });
            }
        }
    });

    $('.f1').find('.marksobtained').filter(':visible').each(function (x, ed) {
        if (ed.value.trim() != "") {
            var markesTotal = $(ed).parent().parent().find(".markstotal");
            if (parseInt(ed.value.trim()) > parseInt($(markesTotal).val().trim())) {
                errorInInput = true;
                $('#' + ed.id).addClass('input-error');
                $(markesTotal).addClass('input-error');
            }
        }
    });

    if ($('#Email').val() != "") {
        if (!isEmail($('#Email').val())) {
            $('#Email').addClass('input-error');
            errorInInput = true;
        }
    }

    if ($('#category2:checked').length == 1) {
        if ($('#Caste').find(":selected").val() == "") {
            $('#Caste').addClass('input-error');
            errorInInput = true;
        }
    }

    if ($('#drpDwnState:selected').val() == "") {
        $('#drpDwnState').addClass('input-error');
        errorInInput = true;
    }

    if ($('#chkLearningDisability:checked').length == 1) {
        if ($('#DisabilityCardNumber').val().trim() == "") {
            $('#DisabilityCardNumber').addClass('input-error');
            errorInInput = true;
        }

        if ($('#DisabilityType :selected').val() == "" || $('#DisabilityType :selected').length == 0) {
            $('#DisabilityType').addClass('input-error');
            errorInInput = true;
        }

        if ($('#DisabilityPercentage').val().trim() == "") {
            $('#DisabilityPercentage').addClass('input-error');
            errorInInput = true;
        }
    }

    // Voter number is mandatory if voter id is selected
    if ($('#VoterId:checked').length == 1) {
        if ($('#VoterNumber').val().trim() == "") {
            $('#VoterNumber').addClass('input-error');
            errorInInput = true;
        }
    }

    if ($('#MKCLFormNumber').val().trim() == "") {
        $('#MKCLFormNumber').addClass('input-error');
        errorInInput = true;
    }
    
    // Hostel reason is mandatory if ishostelrequired is ticked
    if ($('#IsHostelRequired:checked').length == 1) {
        if ($('#HostelReason').val().trim() == "") {
            $('#HostelReason').addClass('input-error');
            errorInInput = true;
        }
    }
    // About college is mandatory
    if ($('#AboutCollege :selected').val() == "") {
        $('#AboutCollege').addClass('input-error');
        errorInInput = true;
    }

    return errorInInput;

    //if (errorInInput == true) {
    //    e.preventDefault();
    //}
}