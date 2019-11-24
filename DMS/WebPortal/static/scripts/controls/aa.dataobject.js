function GetData(isApproved) {
    /* added by vasim for disbaling submit button when user save a record on 12-Aug-14*/
    console.log($("#save-button").attr("disabled"));
    if ($("#save-button").attr("disabled") != undefined) {
        return;
    }
    /*end*/

    if (!IsValid()) {
        $("#error-container").show();
        return;
    }

    var dataobj = {
        DocumentId: DOCUMENT_ID,
        To: GetToList(),
        From: GetFromName(),
        Subject: GetSubject(),
        DocumentTitle: GetDocumentTitle(),
        IsForwarded: IsForwarded(),
        DateForwarded: GetForwardDate(),
        IsReceived: IsReceived(),
        DateReceived: GetReceivedDate(),
        InwardDocumentId: GetNewInwardNumber(),
        DateInward: GetInwardDate(),
        DepartmentId: GetDepartments(),
        FilesTag: GetFileTags(),
        TaggedUser: GetTaggedUsers(),
        IsEmpty: IsEmptyEntry(),
        IsScanned: IsScannedDocument(),
        IsContent: IsContent(),
        ScanPath: GetUploadedDocumentPath(),
        Content: GetDocumentContent(),
        SerialNumber: GetSerialNumber(),
        StoreRoomLocation: GetStoreRoomLocation(),
        DocumentType: GetDocumentType(),
        Approved: isApproved,
        Address: GetDocumentAddress(),
        ParentId: GetParentDocumentId(),
        IsDeadlineMentioned: IsDeadlineMentioned(),
        Deadline: GetDeadlineDate(),
        IsScannedMarathi: IsScannedMarathiDocument(),
        ScanMarathiPath: GetUploadedMarathiDocumentPath()
    };

    $("#save-button").attr("disabled", "disabled");

    var fullUrl = BASE_URL + "api/SaveDocumentDetails.ashx";
    $.ajax({
        url: fullUrl,
        data: dataobj,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            try {
                //$("#save-button").removeAttr("disabled");
                if (isApproved) {
                    var path = data;
                    var msg = $("<div/>").html("Document details saved and approved successfully.").css("color", "green").css("float", "left").css("width", "100%");
                    $(msg).insertBefore($(".doc-container"));

                    $("#form-container").hide();
                    // Show PDF file here
                    var iframe = $("<iframe/>").attr("src", BASE_URL + "directories/" + College + "/outward/" + path[0]).attr("class", "pdfdoc");
                    $(iframe).insertAfter($(".doc-container"));

                    /* added print/email/discard buttons */
                    var discardButton = new FormBuilder().GetButton("discard-button", "Discard", "btn btn-warning").click(function () {
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

                    var emailButton = new FormBuilder().GetButton("email-button", "Email", "btn btn-primary").click(function () {
                        $(this).find(".btn").each(function () {
                            var activePopup = $(this).attr('data-popup-target');
                            $('html').addClass('overlay');
                            $(activePopup).addClass('visible');
                        });
                    });

                    var printButton = new FormBuilder().GetButton("print-button", "Print", "btn btn-success").click(function () {
                        window.open("api/GeneratePrintPdfFile.ashx?docid=" + DOCUMENT_ID, '_blank');
                    });

                    if (CURRENT_USER.RoleId != "1" || CURRENT_USER.RoleId != "1") {
                        $(printButton).insertAfter($(".doc-container"));
                        $(emailButton).insertAfter($(".doc-container"));
                        $(discardButton).insertAfter($(".doc-container"));

                        $(emailButton).find(".btn").each(function () {
                            $(this).attr("data-popup-target", "#email-popup");
                        });
                    }
                    /* end */
                }
                else {
                    var id = parseInt(data);
                    DOCUMENT_ID = id;
                    var msg = $("<div/>").html("Document details saved successfully.").css("color", "green").css("position", "fixed").css("top", "50px").css("right", "70px");
                    $(msg).insertBefore($("#save-button-container"));
                }

                $("#user-comments").removeAttr("readonly").attr("title", "Please add comments for this document");

            } catch (Error) {
                alert(data);
            }
        }
    });
}

function IsValid() {
    var isValid = true;
    var error = "<ul>";
    if (GetDocumentType() == "inward" && GetToList().length <= 0)
        error += "<li>Please specify To</li>";
    if (GetFromName().length <= 0)
        error += "<li>Please specify From</li>";
    if (GetSubject().length <= 0)
        error += "<li>Please specify Subject</li>";
    if (GetDocumentTitle().length <= 0)
        error += "<li>Please specify Document Title</li>";
    if (IsForwarded() == true) {
        if (GetForwardDate().length <= 0) {
            error += "<li>Please specify Forwarded date</li>";
        }
    }
    if (IsReceived() == true) {
        if (GetReceivedDate().length <= 0) {
            error += "<li>Please specify Received date</li>";
        }
    }
    if (IsDeadlineMentioned() == true) {
        if (GetDeadlineDate().length <= 0) {
            error += "<li>Please specify deadline date</li>";
        }
    }
    if (GetParameterByName("doctype") != "student" && GetNewInwardNumber().length <= 0)
        error += "<li>Please specify " + GetParameterByName("doctype") + " Number</li>";
    if (GetParameterByName("doctype") != "student" && GetInwardDate().length <= 0)
        error += "<li>Please specify " + GetParameterByName("doctype") + " Date</li>";

    if (GetDepartments().length <= 0)
        error += "<li>Please specify Departments</li>";
    if (GetTaggedUsers().length <= 0)
        error += "<li>Please specify Users</li>";
    if (GetFileTags().length <= 0)
        error += "<li>Please specify File Tags</li>";
    if (GetStoreRoomLocation().length <= 0)
        error += "<li>Please specify Store Room Location</li>";
    if (GetDocumentType() == "outward" && $("#documentaddress").val().length <= 0)
        error += "<li>Please specify To address to mention in outward document</li>";
    error += "</ul>";

    isValid = !(error.length > 10);

    $("#form-container").find("#error-container").remove();

    var closeButton = $("<div/>").html("X").attr("title", "Close Errors").attr("class", "error-close").click(function () {
        $("#form-container").find("#error-container").remove();
    });

    var errorDiv = $("<div/>").attr("id", "error-container").html("Errors:").append(error).append(closeButton);
    $("#form-container").append(errorDiv);

    return isValid;
}

function GetToList() {
    var to = [];
    $("#to").find("li").each(function () {
        var name = $(this).find("span.tagit-label").html();
        to.push(name);
    });

    return to;
}

function GetFromName() {
    return $("#from").val();
}

function GetSubject() {
    return $("#subject").val();
}

function GetDocumentTitle() {
    return $("#documenttitle").val();
}

function GetDocumentAddress() {
    return $("#documentaddress").val();
}

function GetUploadedDocumentPath() {
    var link = $(".DocUploadStatus a");
    if (link != null && link.length > 0) {
        var href = $(link).attr("href");
        var start = href.lastIndexOf("/");
        var fileName = href.substring(start + 1);
        return fileName;
    }
    else {
        return "";
    }
}

function GetUploadedMarathiDocumentPath() {
    var link = $(".MarathiDocUploadStatus a");
    if (link != null && link.length > 0) {
        var href = $(link).attr("href");
        var start = href.lastIndexOf("/");
        var fileName = href.substring(start + 1);
        return fileName;
    }
    else {
        return "";
    }
}

function GetParentDocumentId() {
    var parentId = GetParameterByName("parentid");
    if (parentId != null && parentId != undefined && parentId != "") {
        return parentId;
    }
    else {
        return PARENT_ID;
    }
}

function GetDepartments() {
    var dept = [];
    $("#department").find("li").each(function () {
        var name = $(this).find("span.tagit-label").html();
        dept.push(name);
    });

    return dept;
}

function GetFileTags() {
    var tags = [];
    $("#file-tags").find("li").each(function () {
        var tag = $(this).find("span.tagit-label").html();
        tags.push(tag);
    });

    return tags;
}

function GetTaggedUsers() {
    var users = [];
    $("#user-tags").find("li").each(function () {
        var user = $(this).find("span.tagit-label").html();
        users.push(user);
    });

    return users;
}

function IsForwarded() {
    return $("#forward").prop("checked");
}

function IsReceived() {
    return $("#received").prop("checked");
}

function IsDeadlineMentioned() {
    return $("#deadline").prop("checked");
}

function IsEmptyEntry() {
    return $("#empty").prop("checked");
}

function IsScannedDocument() {
    return $("#scan").prop("checked");
}

function IsScannedMarathiDocument() {
    return $("#marathi").prop("checked");
}

function IsContent() {
    return $("#prepare").prop("checked");
}

function GetDocumentContent() {
    return encodeURIComponent(CKEDITOR.instances.editor1.getData());
}

function IsForwarded() {
    return $("#forward").prop("checked");
}

function IsReceived() {
    return $("#received").prop("checked");
}

function GetForwardDate() {
    return $("#forward-date-control").val();
}

function GetReceivedDate() {
    return $("#received-date-control").val();
}

function GetDeadlineDate() {
    return $("#deadline-date-control").val();
}

function GetNewInwardNumber() {
    return $("#inward").val();
}

function GetInwardDate() {
    return $("#inward-date").val();
}

function GetSerialNumber() {
    return $("#serial-number").val();
}

function GetStoreRoomLocation() {
    var locations = [];
    /*$("#store-location").find("li").each(function () {
        var location = $(this).find("span.tagit-label").html();
        locations.push(location);
    });*/


    /*$("#store-location-container").find("select").each(function () {
        var location = $(this + "option:selected").text();
        
    }); */

    locations.push($("#dd-sl-Room option:selected").text());
    locations.push($("#dd-sl-Cupboard option:selected").text());
    locations.push($("#dd-sl-Shelf option:selected").text());
    locations.push($("#dd-sl-File option:selected").text());

    return locations;
}

function GetDocumentType() {
    var doctype = GetParameterByName("doctype");

    if (doctype != null && doctype != "") {
        return doctype;
    }
    else {
        return DOC_TYPE;
    }
}