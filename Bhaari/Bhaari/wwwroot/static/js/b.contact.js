var Contact = function () {
    var GetContactScreen = function () {

        var container = $("<div/>").addClass("contact-list");
        var contact = "<div><h3>Contact Bhaari:</h3></div>" +
                     "<div class=\"user-name\"><input placeholder=\"Your Name\" type=\"text\" class=\"firstname\" id=\"txtFirstName\" autocomplete=\"off\" /></div>" +
                     "<div class=\"email-address\"><input placeholder=\"Email Address\" type=\"text\" class=\"firstname\" id=\"txtEmailAddress\" autocomplete=\"off\" /></div>" +
                     "<div class=\"contact-query\"><textarea placeholder=\"Query/Feedback...\" style=\"width: 210px; height: 50px\" id=\"txtFeedback\"></textarea></div>" +
                     "<div class=\"btn btn-primary\" style=\"float: left;\" onclick=\"SubmitContactQuery();\">Save</div><div class=\"btn\" style=\"margin-left: 10px; width: 50px !important\" onclick=\"$('.contact-list').hide();\">Close</div><div id=\"invite-error\"></div><div id=\"invite-success\">You have successfully applied to get an invitation. We will get back to you soon.</div>" +
                     "<div id=\"contact-error\"></div><div id=\"contact-success\"></div>";

        return $(container).append(contact);
    }

    $('body').append(GetContactScreen());
}

function SubmitContactQuery() {
        var error = "<ul>";
        var isValid = true;
        if ($("#txtFirstName").val().trim() == "") {
            error += "<li>First Name is mandatory</li>";
            isValid = false;
        }
        if ($("#txtEmailAddress").val().trim() == "") {
            error += "<li>Email is mandatory</li>";
            isValid = false;
        }
        if ($("#txtFeedback").val().trim() == "") {
            error += "<li>Feedback/query is mandatory</li>";
            isValid = false;
        }
        if (!isValidEmail($("#txtEmailAddress").val().trim())) {
            error += "<li>Enter valid email</li>";
            isValid = false;
        }
        error += "</ul>";
        if (!isValid) {
            $("#contact-error").html(error).show();
            return;
        }
        else {
            $("#contact-error").hide();

            // Encode the data
            var name = encodeURIComponent($("#txtFirstName").val().trim());
            var email = encodeURIComponent($("#txtEmailAddress").val().trim());
            var feedback = encodeURIComponent($("#txtFeedback").val().trim());
            // Make post statement
            $.ajax({
                url: BASE_URL + "ContactUs?n=" + name + "&e=" + email + "&f=" + feedback,
                success: function (result) {
                    // Show success & error messages
                    if (result == "OK") {
                        $("#contact-success").html("Your query/feedback is submitted successfully").show();
                        $("#txtFirstName").val("");
                        $("#txtEmailAddress").val("");
                        $("#txtFeedback").val("");

                        $("#contact-error").hide();

                        setTimeout(function () {
                            $("#contact-success").hide();
                        }, 3000);
                    }
                    else {
                        var error = "<ul><li>" + result + "</li></ul>";
                        $("#contact-error").html(error).show();
                    }
                }
            });
        }
    
}