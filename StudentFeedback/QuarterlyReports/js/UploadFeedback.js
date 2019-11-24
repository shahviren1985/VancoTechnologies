$(document).ready(function () {
    $(".loader-img").hide();
    $('#successmsg').hide();
    $('#errormsg').hide();
    $('#validationerrormsg').hide();


    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var RoleType = localStorage.getItem("roleType");

    $("#userName").html("Welcome " + userName);

    if (userCode == null || userCode == undefined || !userCode) {
        localStorage.clear();
        window.location.href = "index.html";
    }

    $("#logout").click(function () {
        localStorage.clear();
        window.location.href = "index.html";

    });

    $("#btnSubmit").click(function () {

        $('#successmsg').hide();
        $('#errormsg').hide();
        $('#validationerrormsg').hide();
        if ($("#file1").val() == undefined || $("#file1").val() == "" || $("#file1").val() == null) {
            $('#validationerrormsg').fadeIn().delay(2000).fadeOut();
        }
        else {
            $(".loader-img").show();
            //stop submit the form, we will post it manually.
            event.preventDefault();

            // Get form
            var form = $('#ImportStudentFeedbackForm')[0];

            // Create an FormData object
            var data = new FormData(form);

            // If you want to add an extra field for the FormData
            //data.append("CustomField", "This is some extra data, testing");

            // disabled the submit button
            //$("#btnSubmit").prop("disabled", true);

            $.ajax({
                type: "POST",
                enctype: 'multipart/form-data',
                url: GloableWebsite + "api/import/ImportStudentFeedback?year=" + $("#ddAcademicYear").find(":selected").val(),
                data: data,
                processData: false,
                contentType: false,
                cache: false,
                timeout: 600000,
                success: function (data) {
                    $(".loader-img").hide();
                    $('#successmsg').fadeIn().delay(800).fadeOut();
                    $("#file").val("");
                },
                error: function (e) {
                    $(".loader-img").hide();
                    $('#errormsg').fadeIn().delay(1200).fadeOut();
                }
            });
        }
    });

    $("#btnStaffSubmit").click(function () {

        $('#successmsg').hide();
        $('#errormsg').hide();
        $('#validationerrormsg').hide();
        if ($("#file2").val() == undefined || $("#file2").val() == "" || $("#file2").val() == null) {
            $('#validationerrormsg').fadeIn().delay(2000).fadeOut();
        }
        else {
            $(".loader-img").show();
            //stop submit the form, we will post it manually.
            event.preventDefault();

            // Get form
            var form = $('#ImportStaffFeedback')[0];

            // Create an FormData object
            var data = new FormData(form);

            // If you want to add an extra field for the FormData
            //data.append("CustomField", "This is some extra data, testing");

            // disabled the submit button
            //$("#btnSubmit").prop("disabled", true);

            $.ajax({
                type: "POST",
                enctype: 'multipart/form-data',
                url: GloableWebsite + "api/import/ImportStaffFeedback?year=" + $("#ddAcademicYear").find(":selected").val(),
                data: data,
                processData: false,
                contentType: false,
                cache: false,
                timeout: 600000,
                success: function (data) {
                    $(".loader-img").hide();
                    $('#successmsg').fadeIn().delay(800).fadeOut();
                    $("#file").val("");
                },
                error: function (e) {
                    $(".loader-img").hide();
                    $('#errormsg').fadeIn().delay(1200).fadeOut();
                }
            });
        }
    });
});



