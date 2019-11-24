var FormBuilder = function () {
    FormBuilder.prototype.GetTubeContainer = function (id, className, title) {
        var sectionTitle = $("<div/>").attr("id", id + "-section-container").html(title).attr("class", "section-title");
        var tube = $("<div/>").attr("id", id + "-tube-container").attr("class", "tube-container " + className);
        return $(tube).append(sectionTitle);
    }

    FormBuilder.prototype.GetField = function (id, placeholder, isMandatory, validationMsg, maxLength, isMarathiButtonRequired) {
        var txtContainer = $("<div/>").attr("id", id + "-container");
        var errMsg = $("<span/>").attr("id", id + "-error-message").attr("class", "error-message");
        var txt = $("<input/>").attr("id", id).attr("placeholder", placeholder).attr("isMandatory", isMandatory).attr("maxlength", maxLength).attr("title", placeholder);

        $(errMsg).html("* " + validationMsg);

        $(txtContainer).append(txt);

        if (isMarathiButtonRequired != "undefined" && isMarathiButtonRequired != null && isMarathiButtonRequired == true) {
            var marButton = $("<div/>").attr("id", "marathi-key-pop").html("M").addClass("marathi-key").attr("title", "Type in मराठी");

            $(marButton).click(function () {
                window.open("https://translate.google.com/?hl=mr#mr/en", "_blank", "height=450, width=700");
            });

            $(txtContainer).append(marButton);
        }

        $(txtContainer).append(errMsg);

        return txtContainer;
    };

    FormBuilder.prototype.GetMultiLineField = function (id, placeholder, isMandatory, validationMsg, maxLength) {
        var txtContainer = $("<div/>").attr("id", id + "-container");
        var errMsg = $("<span/>").attr("id", id + "-error-message").attr("class", "error-message");
        var txt = $("<textarea/>").attr("id", id).attr("placeholder", placeholder).attr("isMandatory", isMandatory).attr("maxlength", maxLength).attr("rows", "4").attr("columns", "25");

        $(errMsg).html("* " + validationMsg);

        $(txtContainer).append(txt);
        $(txtContainer).append(errMsg);

        return txtContainer;
    };

    FormBuilder.prototype.GetButton = function (id, text, className) {
        var btnContainer = $("<div/>").attr("id", id + "-container");
        var btn = $("<div/>").attr("id", id).html(text).attr("class", className);
        $(btnContainer).append(btn);
        return btnContainer;
    };

    FormBuilder.prototype.GetDateControl = function (id, placeholder) {
        var dateContainer = $("<input/>").attr("id", id + "-date-control").attr("class", "date-container");

        if (placeholder != null && placeholder != "undefined") {
            $(dateContainer).attr("placeholder", placeholder);
        }

        return dateContainer;
    }

    FormBuilder.prototype.GetCheckField = function (id, text, isChecked) {
        var chkContainer = $("<div/>").attr("id", id + "-container").attr("class", "check-container");
        var chk = $("<input/>").attr("id", id).attr("type", "checkbox").attr("class", "check").attr("checked", isChecked);
        var chkText = $("<span/>").html(text).attr("class", "check-text");

        $(chk).click(function () {
            if ($(this).prop("checked")) {
                //$(this).parent().find(".date-container").show();
                $(this).parent().find(".date-container").parent().show();
            }
            else {
                //$(this).parent().find(".date-container").hide();
                $(this).parent().find(".date-container").parent().hide();
            }
        });

        $(chkContainer).append(chk);
        $(chkContainer).append(chkText);
        $(chkContainer).append(this.GetDateControl(id, text + " Date"));
        return chkContainer;
    };

    FormBuilder.prototype.GetCheckBox = function (id, text, isChecked) {
        var chkContainer = $("<div/>").attr("id", id + "-container").attr("class", "check-container");
        var chk = $("<input/>").attr("id", id).attr("type", "checkbox").attr("class", "check").attr("checked", isChecked);
        var checkText = $("<label/>").html(text).attr("class", "radio-text").attr("for", id);

        $(chk).click(function () {
            $(".document").hide();

            switch ($(this).attr("id")) {
                case "prepare":
                    if ($(this).prop("checked")) {
                        $("#new-document-container").show();
                    }
                    break;
                case "scan":
                    if ($(this).prop("checked")) {
                        $("#inward-document-container").show();
                    }
                    break;
                case "marathi":
                    if ($(this).prop("checked")) {
                        $("#marathi-document-container").show();
                        //$("#iframe-document-container").show();
                    }
                    break;
            }
        });

        $(chkContainer).append(chk);
        $(chkContainer).append(checkText);
        return chkContainer;
    };


    FormBuilder.prototype.GetRadioField = function (id, text, groupName, defaultChecked) {
        var radioContainer = $("<div/>").attr("id", id + "-container").attr("class", "radio-container");
        var radio = $("<input/>").attr("id", id).attr("type", "radio").attr("class", "radio").attr("name", groupName).attr("checked", defaultChecked);
        var radioText = $("<span/>").html(text).attr("class", "radio-text");

        $(radio).click(function () {
            $(".document").hide();

            switch ($(this).attr("id")) {
                case "prepare":
                    if ($(this).prop("checked")) {
                        $("#new-document-container").show();
                    }
                    break;
                case "scan":
                    if ($(this).prop("checked")) {
                        $("#inward-document-container").show();
                    }
                    break;
                case "marathi":
                    if ($(this).prop("checked")) {
                        $("#marathi-document-container").show();
                        $("#iframe-document-container").show();
                    }
                    break;
            }
        });

        $(radioContainer).append(radio);
        $(radioContainer).append(radioText);
        //$(radioContainer).append(this.GetDateControl(id + "-date"));
        return radioContainer;
    };

    FormBuilder.prototype.GetFileUploadControl = function (id, cssClass, type, isValidateFileType, fileType) {
        var fileContainer = $("<div/>").attr("id", id + "-container").attr("class", cssClass);
        var file = $("<input/>").attr("id", id).attr("type", "file").attr("class", "file-upload").change(function () {
            UploadSelectedFile(this, type, isValidateFileType, fileType);
        });

        var fileStatus = $("<div/>");
        if (type == "")
            $(fileStatus).attr("class", "DocUploadStatus").hide();
        else {
            $(fileStatus).attr("class", "MarathiDocUploadStatus").hide();
        }

        var commanStatus = $("<div/>");
        $(commanStatus).attr("class", "CommanUploadStatus");

        $(fileContainer).append(file);
        $(fileContainer).append(fileStatus);
        $(fileContainer).append(commanStatus);
        return fileContainer;
    }

    FormBuilder.prototype.GetRichTextEditor = function (id, cssClass) {
        var editorContainer = $("<div/>").attr("id", id + "-container").attr("class", "new-document" + " " + cssClass);
        var editor = $("<textarea class=\"ckeditor\" cols=\"80\" id=\"editor1\" name=\"editor1\" rows=\"10\"></textarea>");
        $(editorContainer).append(editor);
        return editorContainer;
    }

    FormBuilder.prototype.GetTagField = function (id) {
        var tagContainer = $("<div/>").attr("id", id + "-container");
        var tags = $("<ul/>").attr("id", id);

        $(tagContainer).append(tags);

        return tagContainer;
    };

    FormBuilder.prototype.GetCommentsCloseButton = function () {
        var tagContainer = $("<div/>").attr("class", "close-button").html("-").attr("title", "Hide Previous Comments");

        $(tagContainer).click(function () {
            if ($(this).html() == "-") {
                $(".previous-comments").hide();
                $("#user-comments-container").hide();
                $("#user-save-comments-container").hide();
                $("#previous-comments-tube-container").css("height", "60px");
                $(this).html("+").attr("title", "Show Previous Comments");
            }
            else {
                $(".previous-comments").show();
                $("#user-comments-container").show();
                $("#user-save-comments-container").show();
                $(this).html("-").attr("title", "Hide Previous Comments")
                $("#previous-comments-tube-container").css("height", "595px");
            }
        });

        return tagContainer;
    };

};

$(document).ready(function () {

    if (window.location.href.indexOf("Documents.aspx") <= -1) {
        return;
    }

    var showLoading = $("<div/>").html("Loading...").attr("class", "loading");
    var tubeHeader = new FormBuilder().GetTubeContainer("section-header", "full-row", "Document Header");
    var tubeDocumentHeader = new FormBuilder().GetTubeContainer("document-details-header", "full-row", "Details");
    var tubePreviousComments = new FormBuilder().GetTubeContainer("previous-comments", "half-row", "Comments");
    var tubeDocument = new FormBuilder().GetTubeContainer("document-header", "half-row", "Document");
    //var tubeComments = new FormBuilder().GetTubeContainer("document-comments-header", "full-row", "Comments");

    var srNo = new FormBuilder().GetField("serial-number", "Serial Number", true, "Serial Number is mandatory field", 10);
    $(tubeHeader).append(srNo);

    if (GetDocumentType() == "inward") {
        var to = new FormBuilder().GetTagField("to");
        $(tubeHeader).append(to);
    }

    var from = new FormBuilder().GetField("from", "From", true, "From is mandatory field", 500, true);
    $(tubeHeader).append(from);

    var subject = new FormBuilder().GetField("subject", "Subject", true, "Subject is mandatory field", 500, true);
    $(tubeHeader).append(subject);

    var received = new FormBuilder().GetCheckField("received", "Received", true);
    var forwarded = new FormBuilder().GetCheckField("forward", "Forwarded", true);
    var deadline = new FormBuilder().GetCheckField("deadline", "Deadline", true);

    var inward;
    var inwardDate;
    if (GetDocumentType() == "inward") {
        inward = new FormBuilder().GetField("inward", "Inward #", true, "Inward number is mandatory field", 100);
        inwardDate = new FormBuilder().GetField("inward-date", "Inward Date", true, "Inward date is mandatory field", 20);
    }
    else if (GetDocumentType() == "outward") {
        inward = new FormBuilder().GetField("inward", "Outward #", true, "Outward number is mandatory field", 100);
        inwardDate = new FormBuilder().GetField("inward-date", "Outward Date", true, "Outward date is mandatory field", 20);
    }
    else if (GetDocumentType() == "student") {
        inward = new FormBuilder().GetField("inward", "Outward #", true, "Outward number is mandatory field", 100).hide();
        inwardDate = new FormBuilder().GetField("inward-date", "Outward Date", true, "Outward date is mandatory field", 20).hide();
    }
    else {
        inward = new FormBuilder().GetField("inward", "Outward #", true, "Outward number is mandatory field", 100);
        inwardDate = new FormBuilder().GetField("inward-date", "Outward Date", true, "Outward date is mandatory field", 20);
    }

    var department = new FormBuilder().GetTagField("department");
    var fileTags = new FormBuilder().GetTagField("file-tags");
    var userTags = new FormBuilder().GetTagField("user-tags");
    var saveButton = new FormBuilder().GetButton("save-button", "Save Document", "btn btn-primary").click(function () { GetData(false); });
    var approveButton = new FormBuilder().GetButton("approve-button", "Approve Document", "btn btn-success").click(function () {
        //ApproveDocument();
        $('html').addClass('overlay');
        var activePopup = $(this).attr('data-popup-target');
        $(activePopup).addClass('visible');
    });

    $(approveButton).find(".btn").each(function () {
        $(this).attr("data-popup-target", "#credential-popup")
    });

    var discardButton = new FormBuilder().GetButton("discard-button", "Discard Document", "btn btn-warning").click(function () {
        //AuthenticateUser("DiscardDocument");
        $(this).find(".btn").each(function () {
            $("#credential-popup").find(".popup-title").html("Discard Document");
            var activePopup = $(this).attr('data-popup-target');
            $('html').addClass('overlay');
            $(activePopup).addClass('visible');
        });
    });

    $(discardButton).find(".btn").each(function () {
        $(this).attr("data-popup-target", "#credential-popup");
    });

    $(tubeDocumentHeader).append(received);
    $(tubeDocumentHeader).append(forwarded);
    $(tubeDocumentHeader).append(deadline);

    $(tubeDocumentHeader).append(inward);
    $(tubeDocumentHeader).append(inwardDate);

    $(tubeDocumentHeader).append(department);
    $(tubeDocumentHeader).append(fileTags);
    $(tubeDocumentHeader).append(userTags);

    $(tubeDocumentHeader).append(saveButton);
    $(tubeDocumentHeader).append(approveButton);
    $(tubeDocumentHeader).append(discardButton);

    var documentTitle = new FormBuilder().GetField("documenttitle", "Document Title", true, "Document Title is mandatory field", 500, true);

    var storeLocation = new FormBuilder().GetTagField("store-location");

    /*var empty = new FormBuilder().GetRadioField("empty", "Empty entry", "group-document", true);
    var uploadScanned = new FormBuilder().GetRadioField("scan", "Upload scanned document", "group-document", false);
    var prepareNew = new FormBuilder().GetRadioField("prepare", "Prepare new document", "group-document", false);
    var uploadMarathi = new FormBuilder().GetRadioField("marathi", "Upload marathi document", "group-document", false);*/

    var empty = new FormBuilder().GetCheckBox("empty", "Empty entry", true);
    var uploadScanned = new FormBuilder().GetCheckBox("scan", "Upload scanned document", false);
    var prepareNew = new FormBuilder().GetCheckBox("prepare", "Prepare new document", false);
    var uploadMarathi = new FormBuilder().GetCheckBox("marathi", "Upload marathi document", false);

    var fileControl = new FormBuilder().GetFileUploadControl("inward-document", "document", "", true, "pdf");
    var marathifileControl = new FormBuilder().GetFileUploadControl("marathi-document", "document", "marathi", true, "pdf");

    var popupButton = new FormBuilder().GetButton("marathi-popup", "M", "marathi-key");

    $(popupButton).click(function () {
        window.open("https://translate.google.com/?hl=mr#mr/en", "_blank", "height=450, width=700");
    });

    var editor = new FormBuilder().GetRichTextEditor("new-document", "document");
    var comments = new FormBuilder().GetMultiLineField("user-comments", "Enter your new comments...", false, "", 2000);
    var saveComments = new FormBuilder().GetButton("user-save-comments", "Save Comment");

    //createing iframe;
    //var iFrame = $("<div/>").attr("id", "iframe-document-container").attr("class", "document").attr("style", "float: left;width: 100%;top: 15px;").html("<iframe id=\"marathi-iframe\" width=\"750\" height=\"350\" src=\"http://localhost:45117/docs/vasim/harshada_635418335179727676.pdf\" frameborder=\"0\" allowfullscreen></iframe>");
    var iFrame = $("<div/>").attr("id", "iframe-document-container").attr("class", "document").attr("style", "float: left;width: 100%;top: 15px;").html("<iframe id=\"marathi-iframe\" width=\"750\" height=\"350\" src=\"\" frameborder=\"0\" allowfullscreen></iframe>");

    $(tubeDocument).append(documentTitle);

    if (GetDocumentType() == "outward") {
        var documentAddress = new FormBuilder().GetMultiLineField("documentaddress", "To Address", false, "", 2000);
        $(tubeDocument).append(documentAddress);
        //$(forwarded).hide();
    }

    $(tubeDocument).append(storeLocation);
    $(tubeDocument).append(empty);
    $(tubeDocument).append(uploadScanned);
    $(tubeDocument).append(prepareNew);
    $(tubeDocument).append(uploadMarathi); //added by vasim
    $(tubeDocument).append(fileControl);
    $(tubeDocument).append(marathifileControl); //added by vasim

    $(tubeDocument).append(popupButton);
    $(tubeDocument).append(editor);
    $(tubeDocument).append(iFrame);

    $(saveComments).click(function () {
        AddComments(GetDocumentId(), $("#user-comments").val());
    });

    var prevoiusComments = new GetCommentsControl("previous-comments", null);
    $(tubePreviousComments).append(prevoiusComments);

    $("#form-container").append(tubeHeader);
    $("#form-container").append(tubeDocumentHeader);

    $("#form-container").append(tubeDocument);

    $("#form-container").append(tubePreviousComments);
    $("#form-container").append(comments);
    $("#form-container").append(new FormBuilder().GetCommentsCloseButton());
    $("#form-container").append(saveComments);

    $(".document").hide();

    $('#to').tagit();

    $('#file-tags').tagit();
    $('#user-tags').tagit();
    $("#store-location").tagit();

    $("#to li.tagit-new").find("input").attr("placeholder", "To");
    $("#file-tags li.tagit-new").find("input").attr("placeholder", "File Tags");
    $("#user-tags li.tagit-new").find("input").attr("placeholder", "User Tags");
    $("#store-location li.tagit-new").find("input").attr("placeholder", "Store Location");

    $('#inward-date').Zebra_DatePicker({ format: 'd-m-Y' });
    $('#forward-date-control').Zebra_DatePicker({ format: 'd-m-Y' });
    $('#received-date-control').Zebra_DatePicker({ format: 'd-m-Y' });
    $('#deadline-date-control').Zebra_DatePicker({ format: 'd-m-Y' });

    $(".doc-container").append(showLoading);

    var docId = GetParameterByName("id");

    if (docId == "")
        $("#user-comments").attr("readonly", "readonly").attr("title", "Please save the document inorder to add comments");
    else
        $("#user-comments").removeAttr("readonly").attr("title", "Please add comments for this document");
});
