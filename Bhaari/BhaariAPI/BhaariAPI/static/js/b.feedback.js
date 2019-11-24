var UserPreferences = function () {
    var GetPreferenceButton = function () {
        $('body').append(GetUserFeedbackScreen());

        $(".feedback-next,.feedback-prev").click(function () {
            $('.screen').hide();
            $("#" + $(this).attr("show-id")).show();
        });

        $(".job-tech li").click(function () {
            var className = $(this).attr("class");
            if (className.indexOf("-active") <= -1) {
                $(this).attr("class", $(this).attr("class") + "-active");
                $(this).attr("selected", "true");
            }
            else {
                $(this).attr("class", $(this).attr("class").substring(0, $(this).attr("class").indexOf("-active")));
                $(this).attr("selected", "false");
            }
        });

        $(".feedback-rating span").mouseover(function () {
            var id = parseInt($(this).html());
            var counter = 1;

            $(".feedback-rating span").each(function () {
                if (counter < id) {
                    $(this).attr("class", "feedback-" + counter);
                }
                else if (counter == id) {
                    $(this).attr("class", "feedback-" + counter);
                    $("#user-pref-screen .feedback-content").attr("class", "feedback-content feedback-" + counter);
                    $("#user-pref-screen .feedback-content div.feedback-rate").hide();
                    $("#user-pref-screen .feedback-content").find(".smily-" + counter).show().parent().show();
                    $("#user-pref-screen .feedback-content").find(".smily-" + counter + " .feedback-smily").show();
                }
                else {
                    $(this).attr("class", "feedback-default");
                }

                if (counter == 10 && id == counter) {
                    $(this).attr("class", "feedback-last feedback-" + counter);
                }
                else if (counter == 10) {
                    $(this).attr("class", "feedback-last");
                }

                counter++;
            });

            $("#user-pref-screen .feedback-next").show();
            $('#fav-tech-screen .feedback-content').attr('class', 'feedback-content');
            $('#fav-loc-screen .feedback-content').attr('class', 'feedback-content');
            $('#contact-pref-screen .feedback-content').attr('class', 'feedback-content');
        });

        return;
    }

    var GetUserFeedbackScreen = function () {
        var feedback = $("<div class=\"modal fade\" id=\"user-pref\" role=\"dialog\" aria-labelledby=\"myModalLabel2\" aria-haspopup=\"false\">" +
            "<div class=\"modal-dialog\" id=\"user-popup\">" +
            "<div class=\"modal-content\">" +
            "<div class=\"modal-header\" id=\"popup-header\">" +
            "<button type=\"button\" class=\"close\" data-dismiss=\"modal\"><span aria-hidden=\"true\">&times;</span></button>" +
            "<span class=\"icon-feedback\"></span><h3 class=\"modal-title\">What do you like?</h3><span>Your feedback help us know where we can do better.</span>" +
            "</div>" +
            "<div class=\"modal-body\" style=\"background-color: #fff;\">" +
            "<div class=\"screen\" id=\"user-pref-screen\">" +
            "<div class=\"feedback-title\">Would you recommend Bhaari.com to your friends?</div>" +
            "<div class=\"feedback-rating\"><span class=\"one\">1</span><span class=\"two\">2</span><span class=\"three\">3</span><span class=\"four\">4</span><span class=\"five\">5</span><span class=\"six\">6</span><span class=\"seven\">7</span><span class=\"eight\">8</span><span class=\"nine\">9</span><span class=\"ten feedback-last\">10</span></div>" +
            "<div class=\"feedback-content\">" +
            "<div class=\"feedback-rate\" style=\"display: block\"><div class=\"smily-default\"></div><div>Start over.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-1\"></div><div class=\"feedback-smily\">Hell No!</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-2\"></div><div class=\"feedback-smily\">I won't.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-3\"></div><div class=\"feedback-smily\">Not Yet.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-4\"></div><div class=\"feedback-smily\">Maybe.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-5\"></div><div class=\"feedback-smily\">Hmmm..</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-6\"></div><div class=\"feedback-smily\">Fine.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-7\"></div><div class=\"feedback-smily\">Okay!</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-8\"></div><div class=\"feedback-smily\">Yes, I will.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-9\"></div><div class=\"feedback-smily\">Definitely.</div></div>" +
            "<div class=\"feedback-rate\"><div class=\"smily smily-10\"></div><div class=\"feedback-smily\">I already have!</div></div>" +
            GetUserPreferencePager(1) +
            "<div style=\"display: none\" class=\"feedback-next\" show-id=\"fav-tech-screen\"><div>></div></div>" +
            "</div></div>" +
            "<div class=\"screen\" id=\"fav-tech-screen\">" +
            "<div class=\"feedback-title\">Your favorite technologies:</div>" +
            "<div class=\"feedback-content\">" +
            "<div class=\"feedback-prev\" show-id=\"user-pref-screen\"><div><</div></div>" +
            "<ul class=\"job-tech\">" +
            "<li class=\"orange\"><div class=\"job-tech\">Data Science</div></li>" +
            "<li class=\"green\"><div class=\"job-tech\">AI/ML</div></li>" +
            "<li class=\"blue\"><div class=\"job-tech\">AR/VR</div></li>" +
            "<li class=\"red\"><div class=\"job-tech\">IoT</div></li>" +
            "<li class=\"maroon\"><div class=\"job-tech\">Enterprise</div></li>" +
            "<li class=\"yellow\"><div class=\"job-tech\">Games</div></li></ul>" +
            GetUserPreferencePager(2) +
            "<div class=\"feedback-next\" show-id=\"fav-loc-screen\"><div>></div></div>" +
            "</div></div>" +
            "<div class=\"screen\" id=\"fav-loc-screen\">" +
            "<div class=\"feedback-title\">Your favorite country for work:</div>" +
            "<div class=\"feedback-content\">" +
            "<div class=\"feedback-prev\" show-id=\"fav-tech-screen\"><div><</div></div>" +
            "<ul class=\"job-tech\">" +
            "<li class=\"orange\"><div class=\"job-tech\" title=\"United States of America\">USA</div></li>" +
            "<li class=\"green\"><div class=\"job-tech\" title=\"United Kingdom\">UK</div></li>" +
            "<li class=\"blue\"><div class=\"job-tech\">India</div></li>" +
            "<li class=\"red\"><div class=\"job-tech\">Australia</div></li>" +
            "<li class=\"maroon\"><div class=\"job-tech\">Canada</div></li>" +
            "<li class=\"yellow\"><div class=\"job-tech\">Singapore</div></li>" +
            "</ul>" +
            GetUserPreferencePager(3) +
            "<div class=\"feedback-next\" show-id=\"feedback-pref-screen\"><div>></div></div>" +
            "</div></div>" +
            "<div class=\"screen\" id=\"feedback-pref-screen\">" +
            "<div class=\"feedback-title\">Your personal details:</div><div id=\"feedback-error\"></div><div id=\"feedback-success\"></div>" +
            "<div class=\"feedback-content\">" +
            "<div class=\"feedback-prev\" show-id=\"fav-loc-screen\"><div><</div></div>" +
            "<div class=\"user-input\">" +
            "<input placeholder=\"First Name\" type=\"text\" class=\"firstname\" id=\"txtFeedbackFirstName\" /><br/>" +
            "<input placeholder=\"Email Address\" type=\"text\" class=\"firstname\" id=\"txtFeedbackEmail\" /><br/>" +
            "<textarea placeholder=\"Feedback...\" style=\"width: 210px; height: 50px\" id=\"txtFeedbackComment\"></textarea>" +
            "</div>" +
            GetUserPreferencePager(4) +
            "<div class=\"btn btn-success save-button\" onclick=\"SubmitFeedback();\">Save</div>" +
            "</div></div>" +

            "</div></div>" +
            "</div></div></div>");
        return feedback;
    }

    var GetUserPreferencePager = function (activeID) {
        var pager = "<div class=\"feedback-pager\">";

        for (i = 1; i < 5; i++) {
            if (i <= activeID)
                pager += "<a class=\"active\" id=\"feedback-pager-" + i + "\"></a>";
            else
                pager += "<a class=\"inactive\" id=\"feedback-pager-" + i + "\"></a>";
        }

        pager += "</div>";
        return pager;
    }

    return GetPreferenceButton();
}

function isValidEmail(sEmail) {
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (reg.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}

function SubmitFeedback() {
    // validate
    var error = "<ul>";
    var isValid = true;
    if ($("#txtFeedbackFirstName").val().trim() == "") {
        error += "<li>First Name is mandatory</li>";
        isValid = false;
    }
    if ($("#txtFeedbackEmail").val().trim() == "") {
        error += "<li>Email is mandatory</li>";
        isValid = false;
    }
    if ($("#txtFeedbackComment").val().trim() == "") {
        error += "<li>Feedback/suggestion is mandatory</li>";
        isValid = false;
    }
    if (!isValidEmail($("#txtFeedbackEmail").val().trim())) {
        error += "<li>Enter valid email</li>";
        isValid = false;
    }

    error += "</ul>";
    if (!isValid) {
        $("#feedback-error").html(error).show();
        return;
    }
    else {
        // encode
        var name = encodeURIComponent($("#txtFeedbackFirstName").val().trim());
        var email = encodeURIComponent($("#txtFeedbackEmail").val().trim());
        var feed = encodeURIComponent($("#txtFeedbackComment").val().trim());
        var r = 0, rate = 0;;
        var favTech = [];
        var favLoc = [];

        $("#user-pref-screen .feedback-content .feedback-rate").each(function () {
            r++;
            if ($(this).css("display") == "block") {
                rate = r;
            }
        });

        $("#fav-tech-screen ul.job-tech li").each(function () {
            if ($(this).attr("selected") == "selected") {
                favTech.push($(this).text());
            }
        });

        $("#fav-loc-screen ul.job-tech li").each(function () {
            if ($(this).attr("selected") == "selected") {
                favLoc.push($(this).text());
            }
        });

        // Post
        trackFeedbackSubmited();
        $.ajax({
            url: BASE_URL + "SaveFeedback?n=" + name + "&e=" + email + "&f=" + feed + "&t=" + JSON.stringify(favTech) + "&l=" + JSON.stringify(favLoc) + "&r=" + rate,
            success: function (result) {
                // Show success & error messages
                if (result == "OK") {
                    $("#feedback-success").html("Thank you for giving your valuable feedback.").show();
                    $("#txtFeedbackFirstName").val("");
                    $("#txtFeedbackEmail").val("");
                    $("#txtFeedbackComment").val("");
                    $("#fav-tech-screen ul.job-tech li").each(function () {
                        $(this).removeAttr("selected");
                    });

                    $("#fav-loc-screen ul.job-tech li").each(function () {
                        $(this).removeAttr("selected");
                    });

                    $("#feedback-error").hide();

                    setTimeout(function () {
                        $("#feedback-success").hide();
                    }, 3000);
                }
                else {
                    var error = "<ul><li>" + result + "</li></ul>";
                    $("#feedback-error").html(error).show();
                }
            }
        });
    }

}