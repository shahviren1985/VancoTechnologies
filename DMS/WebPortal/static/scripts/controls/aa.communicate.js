/* GLOBAL Data Objects */
var DEPT;
var USERS;
var LOCATION;
var FILE_TAGS;
var COMMENTS;
var FOLDERS;
var DOCUMENTS;
var UPLOADED_DOC_PATH;
var DOCUMENT_ID = 0;
var PARENT_ID = -1;
var DOC_TYPE;
var ALL_USERS;
var CURRENT_USER = null;
var s_Department = false, s_Users = false, s_Inward = false, s_Store = false;
var BASE_URL;

if (window.location.host.indexOf("localhost") > -1) {
    BASE_URL = "http://localhost:45117/";
}
else {
    //BASE_URL = "http://myclasstest.com/dms/";
    BASE_URL = "https://vancotech.com/dms/";
}

function CallHandler(searchText, url, OnComplete) {
    var fullUrl = BASE_URL + url + "?" + searchText;
    $.ajax({
        url: fullUrl,
        type: 'GET',
        dataType: 'text',
        success: OnComplete
    });

    return false;
}

function GetParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function GetDocumentId() {
    var docId = GetParameterByName("id");
    if (docId == "")
        docId = DOCUMENT_ID;
    else
        DOCUMENT_ID = docId;

    return docId;
}

function GetDepartmentList() {
    CallHandler("", "api/GetDepartments.ashx", PopulateDepartments);
}

function GetUsers() {
    CallHandler("type=all", "api/GetUsers.ashx", PopulateUsers);
}

function GetCurrentUser() {
    CallHandler("type=current", "api/GetUsers.ashx", PopulateCurrentUser);
}

function GetInwardNumber() {
    if (DOCUMENT_ID == 0)
        CallHandler("doctype=" + GetParameterByName("doctype"), "api/GetAutoInwardNumber.ashx", PopulateInwardNumber);
    else
        s_Inward = true;
    //    PopulateInwardNumber(DOCUMENT_ID);
}

function GetComments(docId) {
    if (docId == undefined || docId == null) {
        docId = "";
    }

    CallHandler("id=" + docId, "api/GetDocumentComments.ashx", PopulateComments);
}

function AddComments(docId, comment) {
    CallHandler("id=" + docId + "&c=" + comment, "api/AddDocumentComments.ashx", CommentsSaved);
}

function GetStoreLocations() {
    CallHandler("", "api/GetStoreLocations.ashx", PopulateStoreLocations);
}

function PopulateDepartments(data) {
    var json = JSON.parse(data);
    var arr = [];
    if (json != null) {

        for (i = 0; i < json.length; i++)
            arr.push(json[i].DepartmentName);

        $('#department').tagit({ "availableTags": arr });
        $("#department li.tagit-new").find("input:first").attr("placeholder", "Department");
        s_Department = true;
    }
}

function PopulateUsers(data) {
    var json = JSON.parse(data);
    var arr = [];
    if (json != null) {

        for (i = 0; i < json.length; i++)
            arr.push(json[i].FirstName + " " + json[i].LastName + " (" + json[i].UserName + ")");

        $('#user-tags').tagit({ "availableTags": arr });
        $("#user-tags li.tagit-new").find("input:first").attr("placeholder", "User Tags");

        $('#to').tagit({ "availableTags": arr });
        $("#to li.tagit-new").find("input:first").attr("placeholder", "To");
        ALL_USERS = json;
        s_Users = true;
    }
}

function PopulateCurrentUser(data) {
    var json = JSON.parse(data);
    if (json != null) {
        CURRENT_USER = json;
    }
}

function PopulateStoreLocations(data) {
    var json = JSON.parse(data);
    var arr = [];
    if (json != null) {

        new GetStoreLocationControl("#store-location-container", json);
        s_Store = true;
        // Previous code to populate the location
        /*for (i = 0; i < json.length; i++) {
            arr.push("Room (" + json[i].RoomNumber + ") -> Cupboard (" + json[i].Cupboard + ") -> Shelf (" + json[i].Shelf + ") -> File -> (" + json[i].FileName + ")");
        }

        $('#store-location').tagit({ "availableTags": arr });
        $("#store-location li.tagit-new").find("input:first").attr("placeholder", "Store Location");
        s_Store = true;*/
    }
}

function PopulateInwardNumber(number) {
    $("#serial-number").val(number);
    s_Inward = true;
}

function PopulateComments(data) {

    var json = JSON.parse(data);

    var previous = $("#previous-comments-tube-container .previous-comments");

    if (previous == null || previous == undefined || previous.html() == undefined) {
        //$("#previous-comments-tube-container").append($("<div/>").attr("class", "arrow_container"));
        $("#previous-comments-tube-container").append($("<div/>").attr("class", "previous-comments").append($("<div/>").attr("class", "arrow_container").attr("style", "display:none;")));
    }

    if (json != null && json.Error != null) {
        //$(".previous-comments").html("");
        $(".previous-comments").find("div").each(function () {
            if ($(this).html() == "Current document does not have any associated comments. Please add new comments." ||
                $(this).html() == "Please add some comment to save for this document. Currently comments field is left empty") {
                $(this).remove();
            }
        });

        $(".previous-comments").append($("<div/>").html(json.Error));
        $(".close-button").click();
    }
    else {
        $(".previous-comments").html("");
        var prevoiusComments = new GetCommentsControl("previous-comments", json);
        $(".previous-comments").append(prevoiusComments);
    }
}

function GetDocumentDetails() {
    CallHandler("id=" + DOCUMENT_ID, "api/GetDocumentDetails.ashx", PopulateDocumentDetails);
}

function AuthenticateUser(funcName) {
    var fullUrl = BASE_URL + "api/AuthenticateUser.ashx";
    var obj = { User: $("#approve-user").val(), Password: $("#approve-password").val() };
    $.ajax({
        url: fullUrl,
        data: obj,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data == true) {
                eval(funcName + "();");// ApproveDocument();
                $('.popup-exit').click();
            }
            else
                $("#approve-error").html("Invalid user name/password or you don't have right to approve the document.").show();
        }
    });
}

function ApproveDocument() {
    GetData(true);
}

function DiscardDocument() {
    var fullUrl = BASE_URL + "api/DiscardDocument.ashx";

    var obj = { DocumentId: DOCUMENT_ID };
    $.ajax({
        url: fullUrl,
        data: obj,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data == "DISCARDED") {
                // Document Discarded
                var msg = $("<div/>").html("This document is discarded. You can't modify the content of this document anymore.").css("color", "red").css("float", "left").css("width", "100%").css("position", "absolute").css("top", "65px");
                $(msg).insertAfter($(".doc-container"));
                $("#save-button-container").hide();
                $("#approve-button-container").hide();
                $("#discard-button-container").hide();
            }
            else {
                alert("Unable to discard the document");
                // Failed discarding document
            }
        }
    });
}

function PopulateDocumentDetails(data) {
    var json = JSON.parse(data);

    if (json != null && json != undefined) {

        if (json.Error != undefined && json.Error == "Invalid") {
            $(".loading").html("It seems this is incorrect document. Please <a href='Dashboard.aspx'>click here</a> to go back to dashboard page.").css("width", "24%");
            $(".loading").show();
            //alert(json.Error);
            return;
        }

        $(".loading").html("Loading..").css("width", "100px");

        PARENT_ID = json.ParentId;
        if (json.DocumentStatus == "APPROVED") {
            var msg = $("<div/>").html("This document is already approved. You can't modify the content of this document anymore.").css("color", "green").css("float", "left").css("width", "100%");
            var docx = $("<div/>").html("<a href='" + BASE_URL + "/directories/outward/" + json.UniqueName + ".docx'>Click here</a> to download document file.").css("float", "right").css("width", "49%").css("text-align", "left").css("padding-right", "10px");
            var upload = $("<div/>").html("<div>You can upload the pdf file.</div><div><input type='file' id='fuUpload' onchange=\"UploadSelectedFile(this,'" + json.UniqueName + ".pdf');\" style='margin-left: 38%'/></div>").css("float", "left").css("width", "100%").css("padding-top", "5px");

            $("#form-container").hide();

            var discardButton = new FormBuilder().GetButton("discard-button", "Discard", "btn btn-warning").click(function () {
                //AuthenticateUser("DiscardDocument");
                $(this).find(".btn").each(function () {
                    $("#credential-popup").find(".popup-title").html("Discard Document");

                    //$("#credential-popup").find("#approve-document").click(function () {

                    //    $(this).preventDefault();
                    //});
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
            var linkButton = new FormBuilder().GetButton("link-button", "Link Document", "btn btn-danger").click(function () { alert("redirect to link outward doc"); });

            var iframe = $("<iframe/>").attr("src", BASE_URL + "directories/" + College + "/outward/" + json.UniqueName + ".pdf").attr("class", "pdfdoc");

            //$(this).attr("data-popup-target", "#email-popup");

            $(iframe).insertAfter($(".doc-container"));
            //$(upload).insertAfter($(".doc-container"));
            //$(docx).insertAfter($(".doc-container"));

            if (json.ScanPath != undefined && json.ScanPath != null && json.ScanPath != "") {
                var attachedDoc = $("<div/>").html("<a href='" + BASE_URL + "docs/" + College + "/" + json.Author + "/" + json.ScanPath + "'>click here</a> to download attached document.").css("float", "left").css("width", "49%").css("text-align", "right");
                //$(attachedDoc).insertAfter($(".doc-container"));
            }

            $(msg).insertAfter($(".doc-container"));
            //$(linkButton).insertAfter($(".doc-container"));

            if (CURRENT_USER.RoleId != "1" || CURRENT_USER.RoleId != "1") {
                $(printButton).insertAfter($(".doc-container"));
                $(emailButton).insertAfter($(".doc-container"));
                $(discardButton).insertAfter($(".doc-container"));

                $(emailButton).find(".btn").each(function () {
                    $(this).attr("data-popup-target", "#email-popup");
                });
            }
        }

        else {
            var message = JSON.parse(json.MessageHeader);
            $("#serial-number").val(json.SerialNumber);

            $("#from").val(decodeURIComponent(message.From));
            $("#subject").val(decodeURIComponent(message.Subject));

            DOC_TYPE = message.DocumentType;
            if (message.DocumentType == "inward") {
                $("#inward").val(decodeURIComponent(message.InwardNumber));
                $("#inward-date").val(decodeURIComponent(message.InwardDate));
            }
            else if (message.DocumentType == "outward") {
                $("#inward").val(decodeURIComponent(message.OutwardNumber));
                $("#inward-date").val(decodeURIComponent(message.OutwardDate));
            }
            else if (message.DocumentType == "student") {
                $("#inward-container").hide();
                $("#inward-date-container").hide();
            }

            // OutwardNumber
            // OutwardDate
            $("#documenttitle").val(decodeURIComponent(json.FriendlyName));


            $("#documentaddress").val(decodeURIComponent(json.Address));

            CKEDITOR.instances['editor1'].setData(decodeURIComponent(json.Content))

            if (message.IsForwaded == "true") {
                $("#forward").attr("checked", "checked");
                $("#forward-date-control").val(decodeURIComponent(message.ForwardDate));
            }
            else {
                $("#forward").click();
                $("#forward").removeAttr("checked");
                $("#forward-date-control").val();
            }

            if (message.IsReceived == "true") {
                $("#received").attr("checked", "checked");
                $("#received-date-control").val(decodeURIComponent(message.ReceivedDate));
            }
            else {
                $("#received").click();
                $("#received").removeAttr("checked");
                $("#received-date-control").val();
            }


            /*if (json.IsScanned) {
                $("#scan").attr("checked", "checked");
                $("#scan").click();

                $(".DocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + "docs/" + json.Author + "/" + json.ScanPath + "\">Uploaded Document Link</a>");
            }
            else if (json.IsContent) {
                $("#prepare").attr("checked", "checked");
                $("#prepare").click();
            }
            else if (json.IsScannedMarathi) {
                $("#marathi").attr("checked", "checked");
                $("#marathi").click();
                $(".MarathiDocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + "marathi-docs/" + json.Author + "/" + json.ScanMarathiPath + "\">Uploaded Document Link</a>");
                $("#marathi-iframe").attr("src", BASE_URL + "marathi-docs/" + json.Author + "/" + json.ScanMarathiPath);
                $("#iframe-document-container").show();
            }
            else {
                $("#empty").attr("checked", "checked");
                //$("#empty").click();
            }*/

            var isEmpty = true;

            if (json.IsScanned) {
                isEmpty = false;
                $("#scan").attr("checked", "checked");
                //$("#scan").click();
                $("#inward-document-container").show();

                //$(".DocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + "docs/" + json.Author + "/" + json.ScanPath + "\">Uploaded Document Link</a>");
                $(".DocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + "docs/" + College + "/" + json.Author + "/" + json.ScanPath + "\">Uploaded Document Link</a>");
            }

            if (json.IsContent) {
                isEmpty = false;
                $("#new-document-container").show();
                $("#prepare").attr("checked", "checked");
                //$("#prepare").click();
            }

            if (json.IsScannedMarathi && json.ScanMarathiPath != "") {
                isEmpty = false;
                $("#marathi").attr("checked", "checked");
                //$("#marathi").click();
                $("#marathi-document-container").show();
                $(".MarathiDocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + "marathi-docs/" + College + "/" + json.Author + "/" + json.ScanMarathiPath + "\">Uploaded Document Link</a>");
                $("#marathi-iframe").attr("src", BASE_URL + "marathi-docs/" + College + "/" + json.Author + "/" + json.ScanMarathiPath);
                $("#iframe-document-container").show();
            }

            if (isEmpty) {
                $("#empty").attr("checked", "checked");
                $("#empty").click();
            }
            else {
                $("#empty").removeAttr("checked");
            }



            if (json.IsDeadlineMentioned) {
                $("#deadline").attr("checked", "checked");
                $("#deadline-date-control").val(decodeURIComponent(json.Deadline));
            }
            else {
                $("#deadline").click();
                $("#deadline").removeAttr("checked");
                $("#deadline-date-control").val();
            }

            // Populate tags
            // users
            // departments
            var departments = json.DepartmentId.split(',');
            var taggedUsers = json.TaggedUsers.split(',');
            var to = message.To.split(',');
            var fileTags = json.FileTags.split(',');
            var storeLocations = json.StoreRoomLocation.split(',');

            for (k = 0; k < departments.length; k++) {
                $(GetTagElement(departments[k])).insertBefore("#department li:first");
            }

            if (ALL_USERS != undefined && ALL_USERS.length > 0) {
                for (i = 0; i < taggedUsers.length; i++) {
                    for (j = 0; j < ALL_USERS.length; j++) {
                        if (ALL_USERS[j].UserName.toLowerCase() == taggedUsers[i].toLowerCase()) {
                            $(GetTagElement(ALL_USERS[j].FirstName + " " + ALL_USERS[j].LastName + " (" + ALL_USERS[j].UserName + ")")).insertBefore("#user-tags li:first");
                        }
                    }
                }
            }
            else {
                setTimeout(function () {
                    for (i = 0; i < taggedUsers.length; i++) {
                        for (j = 0; j < ALL_USERS.length; j++) {
                            if (ALL_USERS[j].UserName.toLowerCase() == taggedUsers[i].toLowerCase()) {
                                $(GetTagElement(ALL_USERS[j].FirstName + " " + ALL_USERS[j].LastName + " (" + ALL_USERS[j].UserName + ")")).insertBefore("#user-tags li:first");
                            }
                        }
                    }
                }, 2000);
            }

            for (i = 0; i < to.length; i++) {
                $(GetTagElement(to[i])).insertBefore("#to li:first");
            }

            for (i = 0; i < fileTags.length; i++) {
                $(GetTagElement(fileTags[i])).insertBefore("#file-tags li:first");
            }

            // Write code to populate the store location
            new SetStoreLocation("#store-location", storeLocations);
            /*for (i = 0; i < storeLocations.length; i++) {
                if (storeLocations[i] != "") {
                    $(GetTagElement(storeLocations[i])).insertBefore("#store-location li:first");
                }
            }*/
        }

        if (json.DocumentStatus == "DISCARDED") {
            var msg = $("<div/>").html("This document is already discarded. You can't modify the content of this document anymore.").css("color", "red").css("float", "left").css("width", "100%").css("position", "absolute").css("top", "65px");
            $(msg).insertAfter($(".doc-container"));
            $("#save-button-container").hide();
            $("#approve-button-container").hide();
            $("#discard-button-container").hide();
        }
    }

    CallHandler("DocumentId=" + DOCUMENT_ID, "api/UpdateDocumentReadStatus.ashx");
}

function GetTagElement(val) {
    var li = $("<li class=\"tagit-choice ui-widget-content ui-state-default ui-corner-all tagit-choice-editable\"><span class=\"tagit-label\">" + val + "</span><a class=\"tagit-close\"><span class=\"text-icon\">×</span><span class=\"ui-icon ui-icon-close\"></span></a><input type=\"hidden\" value=\"dep\" name=\"tags\" class=\"tagit-hidden-field\"></li>");
    return li;
}

function CommentsSaved(data) {
    var json = JSON.parse(data);

    if (json != null && json.Error != null) {
        //$(".previous-comments").html("");
        $(".previous-comments").find("div").each(function () {
            if ($(this).html() == "Current document does not have any associated comments. Please add new comments." ||
                $(this).html() == "It seems this is incorrect document. You can not add comments for this document" ||
                $(this).html() == "Please add some comment to save for this document. Currently comments field is left empty") {
                $(this).remove();
            }
        });

        $(".previous-comments").append($("<div/>").html(json.Error));
    }
    else {
        var prevoiusComments = new GetCommentsControl("previous-comments", json);
        $(".previous-comments").append(prevoiusComments); // instead of append, we shall prepend - as first comment of the div
    }
}

$(document).ready(function () {
    GetDepartmentList();

    if (window.location.href.indexOf("Documents.aspx") > -1) {
        GetUsers();
        GetComments(GetDocumentId());
        GetInwardNumber();
        GetStoreLocations();
        GetCurrentUser();

        var interval = setInterval(function () {
            if (s_Department && s_Inward && s_Store && s_Users && CURRENT_USER != null) {
                $(".loading").hide();
                $(".Zebra_DatePicker_Icon").css("top", "8px").css("left", "163px");
                $("#inward-date-container .Zebra_DatePicker_Icon").css("top", "18px").css("left", "143px");
                GetDocumentDetails();
                $("#related-document-links").show();
                if (CURRENT_USER.RoleId == "2") {
                    $("#approve-button").show();
                    $("#discard-button").hide();
                }
                else if (CURRENT_USER.RoleId == "3") {
                    $("#approve-button").hide();
                    $("#discard-button").show();
                }
                else {
                    $("#save-button").hide();
                    $("#approve-button").hide();
                    $("#discard-button").hide();
                    $("#related-document-links").hide();
                    $("#print-button-container").hide();
                    $("#email-button-container").hide();
                    $("#discard-button-container").hide();
                }

                $("#form-container").show();
                clearInterval(interval);
            }
        }, 1000);
    }
});