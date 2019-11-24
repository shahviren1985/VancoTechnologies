
function CheckLogoFile(source, arguments) {

    var UploadDoc = document.getElementById('<%= fuLogoUpload.ClientID %>');
    var myfile = UploadDoc.value;
    var format = new Array();
    var Extension = myfile.substring(myfile.lastIndexOf('.') + 1).toLowerCase();
    if (Extension == "jpeg" || Extension == "png" || Extension == "jpg" || Extension == "gif") {
        arguments.IsValid = true;
    }
    else {
        if (UploadDoc.value == '')
            alert('Please browse image to upload.');
        else
            alert('Please upload only .jpg, jpeg, png or gif file.');
        arguments.IsValid = false;
    }
}

function CheckCertificateFile(source, arguments) {

    var UploadDoc = document.getElementById('<%= fuCertificates.ClientID %>');
    var myfile = UploadDoc.value;
    var format = new Array();
    var Extension = myfile.substring(myfile.lastIndexOf('.') + 1).toLowerCase();
    if (Extension == "txt" || Extension == "docx" || Extension == "doc" || Extension == "pdf") {
        arguments.IsValid = true;
    }
    else {
        if (UploadDoc.value == '')
            alert('Please browse file to upload.');
        else
            alert('Please upload only .txt, doc, docx or pdf file.');
        arguments.IsValid = false;
    }
}

function ShowMenu(id) {
    if ($('#' + id).css('display') == 'none') {
        $('.MenuItem').hide();
        $('#' + id).show('slide', { direction: 'up' }, 500);
    }

    setTimeout(function () {
        if ($('#' + id).css('display') != 'none') {
            $('#' + id).hide('slide', { direction: 'up' }, 500);
        }
    }, 4000);
}

function ShowLinkDescription(element, className) {
    var timer = -1;
    var e;
    $(element).find(".LinkDescription").each(function () {
        e = $(this);

        timer = setTimeout(function () {
            if ($(e).attr("timer") != undefined) {
                $(e).removeAttr("timer");
                $(e).show();

                $(e).attr("class", className + " LinkDescription");
                //setTimeout(function () { $(e).fadeOut(500); timer = -1; }, 3000);
            }
        }, 1000);

        $(e).attr("timer", timer);
    });
}

function HideLinkDescription(element) {
    $(element).find(".LinkDescription").each(function () {
        $(this).removeAttr("timer");
        $(this).fadeOut(100);
    });
}

function SetGridPageStyle(isPostBack) {
    $("table.Grid").each(function () {
        $(this).find("table tr td span").each(function () {

            $(this).attr("class", "SelectedPage");
        });

        $(this).find("table tr td a").each(function () {

            $(this).attr("class", "PageLinks");
        });
    });
    if (isPostBack == "true") {
        if ($("#GroupListDialog").css("display") == "none") {
            $("#UserListDialog").show();
        }
    }
}

//Sheetal
function RichTextArea() {
    var Details = document.getElementById('ctl00_Content_hfPageContent').value;
    document.getElementById('txtText').value = Details;
    //alert(Details);
    $("textarea").htmlarea();
    PrepareImageContainer();
    PrepareLinkContainer();
    
    //alert("Hi");
}
//end

function GetSelectedUsers() {
    $("table.Grid").find("tr").each(function () {
        $(this).find("td").each(function () {
            var checkBoxContainer = $(this);
            $(this).find("input").each(function () {
                if ($(this).attr("type") == "checkbox" && $(this).attr("checked") == "checked") {
                    var userName = $(checkBoxContainer).next();
                    if ($(userName).html() != undefined && $(userName).html() != "") {
                        if ($("#ctl00_Content_txtUser").val() == "") {
                            $("#ctl00_Content_txtUser").val($(userName).html());
                        }
                        else {
                            userName = $("#ctl00_Content_txtUser").val() + "," + $(userName).html();
                            $("#ctl00_Content_txtUser").val(userName);
                        }
                    }
                }
            });
        });
    });

    $("#UserListDialog").hide();
    return false;
}