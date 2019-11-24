var isFileSelected = false;
var bUploadStarted = false;
var uploadedFilePath = "";
var xhr = new XMLHttpRequest();

function UploadSelectedFile(element, type, isValidate, fileType) {
    var file = element.files[0];
    /*if (file.size > MAX_FILE_SIZE) {
        alert('Please upload Mark/Logo which is less than 2MB in size.');
        return false;
    }*/
    $(".CommanUploadStatus").html("").hide();
    if (isValidate && file.name != undefined && fileType != "all") {
        var extenstion = file.name.substring(file.name.lastIndexOf(".") + 1);

        if (fileType.toLowerCase() != extenstion.toLowerCase()) {

            $(".CommanUploadStatus").html("Please upload valid " + fileType + " file").show().css("color", "red");
            return;
        }
    }


    xhr.upload.addEventListener('progress', function (event) { UploadProgress(event, type); }, false);
    xhr.onreadystatechange = function (event) { FileUploaded(event, type); };
    xhr.open('POST', BASE_URL + 'api/FileUploader.ashx?t=' + type, true);
    xhr.setRequestHeader('X-FILE-NAME', file.name);
    xhr.send(file);

    /*var ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf("msie") > -1) {
        var iframe = document.getElementById('IEUploadImageControl');
        var innerDoc = iframe.contentDocument || iframe.contentWindow.document;
        var formElement = innerDoc.getElementById("upload_form");

        if (formElement && element.value) {
            formElement.submit();
        }
        else if (formElement && element.value == "" && !isFileSelected) {
            window.event.cancelBubble = true;
            isFileSelected = true;
        }

        return;
    }
    else {
        var file = element.files[0];
        if (file.size > MAX_FILE_SIZE) {
            alert('Please upload Mark/Logo which is less than 2MB in size.');
            return false;
        }

        xhr.upload.addEventListener('progress', function (event) { UploadProgress(event); }, false);
        xhr.onreadystatechange = function (event) { FileUploaded(event); };
        xhr.open('POST', BASE_PATH + '/FileUpload.ashx', true);
        xhr.setRequestHeader('X-FILE-NAME', file.name);
        xhr.send(file);
    }*/
}

function UploadProgress(event, type) {
    var percent = parseInt(event.loaded / event.total * 100);

    $(".CommanUploadStatus").html("Please wait...Uploading your Mark/Logo..." + percent + "%").show();
}

function FileUploaded(event, type) {
    var target = event.target ? event.target : event.srcElement;

    if (target.readyState == 4) {
        $(".CommanUploadStatus").html("").hide();
        if (target.status == 200 || target.status == 304) {
            if (event.target.responseText.indexOf("Error") > -1) {
                alert("An error occurred while uploading the document");
                console.log(event.target.responseText);
            }
            else {
                //
                if (type == "") {
                    $(".DocUploadStatus").html("").hide();
                    $("#inward-document").val("");
                    UPLOADED_DOC_PATH = event.target.responseText;
                    $(".DocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + UPLOADED_DOC_PATH + "\">Uploaded Document Link</a>");
                }
                else if (type == "marathi") {
                    $(".MarathiDocUploadStatus").html("").hide();
                    $("#marathi-document").val("");
                    UPLOADED_DOC_PATH = event.target.responseText;
                    $(".MarathiDocUploadStatus").show().html("<a target=\"_new\" href=\"" + BASE_URL + UPLOADED_DOC_PATH + "\">Uploaded Document Link</a>");
                    $("#marathi-iframe").attr("src", BASE_URL + UPLOADED_DOC_PATH);
                    $("#iframe-document-container").show();
                }
                else {
                    $(".pdfdoc").attr("scr", UPLOADED_DOC_PATH);
                    document.location.href = document.location.href;
                }
            }
        }
    }
}