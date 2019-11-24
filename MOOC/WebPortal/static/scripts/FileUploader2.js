var dropZone;
var originalImageWidth = "360";
var originalImageHeight = "200";

// Show the upload progress
function uploadProgress(event) {
    var percent = parseInt(event.loaded / event.total * 100);
    $('#ThumbnailContainer').text('Uploading: ' + percent + '%');
}

// Show upload complete or upload failed depending on result
function stateChange(event) {
    if (event.target.readyState == 4) {
        if (event.target.status == 200 || event.target.status == 304) {
            $('#dropZone').text('Upload Complete!');
            location.reload(true);
        }
        else {
            dropZone.text('Upload Failed!');
            dropZone.addClass('error');
        }
    }
}

function UploadSelectedFile(element, progressBarId, placeholder, type, cancelButton, event) {
    if (placeholder == "clLocalImages") {
        if (!CheckPermissions("upload_image")) {
            element.value = "";
            return;
        }
    }
    else if (placeholder == "clLocalVideos") {
        if (!CheckPermissions("upload_video")) {
            element.value = "";
            return;
        }
    }

    PMLQ("#" + cancelButton).css("visibility", "visible");
    var file = element.files[0];
    if (file.size > 10000000000) {
        alert('File Too Large!');
        return false;
    }
    /*else if (file.size > SPACE_REMAIN) {
    alert('You do not have enough space to upload the selected file');
    return false;
    }*/

    PMLQ("#" + cancelButton).show();
    PMLQ("#" + cancelButton).attr("uploadstarted", "true");
    xhr.upload.addEventListener('progress', function (event) { uploadProgress(event, progressBarId); }, false);
    xhr.onreadystatechange = function (event) { stateChange(event, placeholder, progressBarId, cancelButton); };
    xhr.open('POST', 'FileUpload.ashx?type=' + type, true);
    xhr.setRequestHeader('X-FILE-NAME', file.name);
    xhr.send(file);
}

function CancelUpload(elementId, progressBarId, buttonId) {
    if (PMLQ("#" + buttonId).attr("uploadstarted") == "true") {
        xhr.abort();

        var fileUpload = document.getElementById(elementId);
        var progressBar = document.getElementById(progressBarId);
        progressBar.style.display = "none";
        fileUpload.value = "";
        PMLQ("#" + buttonId).removeAttr("uploadstarted");

        PMLQ("#" + buttonId).css("visibility", "hidden");
    }
}

function uploadProgress(event, id) {
    var percent = parseInt(event.loaded / event.total * 100);
    if (percent > 90) {
        PMLQ("#CancelUploadImage").hide();
    }
    var progressBar = document.getElementById(id);
    if (progressBar != null && typeof (progressBar) != undefined) {
        progressBar.style.display = "block";
    }
    PMLQ("#" + id).progressbar({ value: percent });
}


function stateChange(event, id, progressBarId, cancelButton) {
    var target = event.target ? event.target : event.srcElement;
    if (target.readyState == 4) {
        PMLQ("#" + cancelButton).removeAttr("uploadstarted");
        if (event.target.responseText.indexOf("Error") > -1) {
            PMLQ("#" + cancelButton).css("visibility", "hidden");
            var progressBar = document.getElementById(progressBarId);
            if (progressBar != null && typeof (progressBar) != undefined) {
                var fileUploadId = (progressBarId.indexOf("Video") > -1) ? "#FileUpload2" : (progressBarId.indexOf("Document") > -1) ? "#FileUpload3" : "#FileUpload";
                PMLQ(progressBar).hide();
                PMLQ(fileUploadId).val("");
            }

            PmlAlert(event.target.responseText);
            return;
        }

        if (target.status == 200 || target.status == 304) {
            PMLQ("#" + cancelButton).css("visibility", "hidden");
            var progressBar = document.getElementById(progressBarId);
            if (progressBar != null && typeof (progressBar) != undefined) {
                var fileUploadId = (progressBarId.indexOf("Video") > -1) ? "#FileUpload2" : (progressBarId.indexOf("Document") > -1) ? "#FileUpload3" : "#FileUpload1";
                PMLQ(progressBar).hide();
                PMLQ(fileUploadId).val("");
            }

            var element = document.getElementById(id);
            var attach = document.createElement('span');
            PMLQ(element).children("img").each(function () { PMLQ(this).remove(); });
            PMLQ(element).children("span").each(function () { PMLQ(this).remove(); });

            var path = event.target.responseText;
            var imageElement = document.createElement("img");
            var url = path.substring(0, path.indexOf("?"));
            var poster;
            var source;
            var type;

            var errorIndex = path.indexOf("&errorcode=");

            var attachCssClass = "attachAddImage";
            if (errorIndex > 0) {
                var code = path.substr(errorIndex + 11);
                alert(eval("Error_" + code));
                return;
            }

            if (path.indexOf("type=Video") > 0) {
                //               alert(path);
                source = url;
                url = path.substring(path.indexOf("url=") + 4);
                attachCssClass = "attachVideoImage";
                type = "video";
                poster = url;
            }
            else if (path.indexOf("type=document") > 0) {
                attachCssClass = "attachDocumentImage";
                type = "document";
                url = path.substring(path.indexOf("url=") + 4);
                source = url.replace("_thumb.jpg", "");
                url = GetDocumentImageUrl(url);
            }
            else {
                url = url.replace("_thumb.jpg", "");
                type = "image";
            }

            imageElement.setAttribute("width", "40%");
            imageElement.setAttribute("height", "40%");
            imageElement.setAttribute("originalUrl", url);
            imageElement.setAttribute("class", "ThumbnailImage");
            imageElement.setAttribute("style", "float: left");
            imageElement.setAttribute("src", url);
            imageElement.setAttribute("type", type);
            imageElement.setAttribute("source", source);
            imageElement.setAttribute("url", url);

            var ul = PMLQ("<ul/>");
            var li = PMLQ("<li/>");
            var icons = PMLQ("<div/>");
            var infoIcon = PMLQ("<i/>");
            var closeIcon = PMLQ("<i/>");
            var attachIcon = PMLQ("<i/>");

            PMLQ(icons).attr("class", "content-icon-container");
            PMLQ(attachIcon).attr("class", "icon-plus content-icon");

            PMLQ(infoIcon).attr("class", "icon-info-sign content-icon");
            PMLQ(closeIcon).attr("class", "icon-remove-sign content-icon");

            PMLQ(attachIcon).attr("title", "Attach image to current PML");
            PMLQ(infoIcon).attr("title", "More information about this image...");
            PMLQ(closeIcon).attr("title", "Close image");

            PMLQ(attachIcon).click(function () { Attach(PMLQ(this).parent().parent().find("img")); });

            PMLQ(closeIcon).click(function (event) {
                PMLQ("#clLocalImages").find("ul").each(function () { PMLQ(this).remove(); });
            });

            PMLQ(ul).attr("class", "stack");
            PMLQ(ul).attr("data-count", "3");

            attach.innerHTML = 'Attach';
            attach.setAttribute("class", attachCssClass);
            
            //element.appendChild(attach);
            imageElement.setAttribute("originalheight", originalImageHeight);
            imageElement.setAttribute("originalwidth", originalImageWidth);

            PMLQ(li).append(imageElement);
            PMLQ(icons).append(attachIcon);
            PMLQ(icons).append(infoIcon);
            PMLQ(icons).append(closeIcon);
            PMLQ(li).append(icons);
            PMLQ(ul).append(li);
            PMLQ(element).append(PMLQ(ul));

            PMLQ(attach).mouseover(function () { PMLQ(this).show(); });
            PMLQ(attach).mouseout(function () { PMLQ(this).hide(); });

            PMLQ(imageElement).mouseover(
                function () {
                    PMLQ(imageElement).siblings('span').show();
                });

            PMLQ(imageElement).mouseout(
                function () {
                    PMLQ(imageElement).siblings('span').hide();
                }
            );

            PMLQ(imageElement).click(function () {
                Attach(imageElement);
            });

            PMLQ(attach).click(function () {
                Attach(imageElement);
            });
        }
    }
}

function UploadFile(elementId, progressBarId, type) {
    var dropZone = document.getElementById(elementId);

    //removeClass(dropZone, 'error');

    // Check if window.FileReader exists to make 
    // sure the browser supports file uploads
    /*if (typeof (window.FileReader) == 'undefined') {
        dropZone.innerHTML = 'Browser Not Supported!';
        addClass(dropZone, 'error');
        return;
    }*/

    // Add a nice drag effect
    dropZone.ondragenter = dropZone.ondragover = function (e) {
        e.preventDefault();
        return false;
    }

    dropZone.ondragend = dropZone.ondragstop = function (e) {
        e.preventDefault();
        return false;
    }

    // The drop event handles the file sending
    dropZone.ondrop = function (e) {
        e.preventDefault();
        if (!CheckPermissions("upload_image")) {
            return;
        }

        if (e.dataTransfer == null || typeof (e.dataTransfer) == undefined) {
            return;
        }

        var areMultipleFiles = false;
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            // Get the file and the file reader
            var file = e.dataTransfer.files[i];

            if (file == null || typeof (file) == undefined) {
                return;
            }

            // Validate file size
            if (file.size > 10000000000) {
                dropZone.text('File Too Large!');
                addClass(dropZone, 'error');
                return false;
            }
            /*else if (file.size > SPACE_REMAIN) {
            alert('You do not have enough space to upload the selected file');
            return false;
            }*/

            var req = new XMLHttpRequest();
            req.upload.addEventListener('progress', function (e) { uploadProgress(e, progressBarId); }, false);
            req.onreadystatechange = function (e) { stateChange(e, elementId, progressBarId); };
            req.open('POST', 'FileUpload.ashx?type=' + type, true);
            req.setRequestHeader('X-FILE-NAME', file.name);
            req.send(file);
            areMultipleFiles = true;
        }

        // Send the file
        if (!areMultipleFiles) {
            if (file == null || typeof (file) == undefined) {
                return;
            }

            xhr.upload.addEventListener('progress', function (e) { uploadProgress(e, progressBarId); }, false);
            xhr.onreadystatechange = function (e) { stateChange(e, elementId, progressBarId); };
            xhr.open('POST', 'FileUpload.ashx', true);
            xhr.setRequestHeader('X-FILE-NAME', file.name);
            xhr.send(file);
        }

        e.preventDefault();
        return false;
    }
}

function GetDocumentImageUrl(src) {
    var link = "static/images/{THUMBNAIL_NAME}";

    if (src.toLowerCase().indexOf(".doc") > -1) {
        link = link.replace("{THUMBNAIL_NAME}", "document-thumb-1.jpg");
    }
    else if (src.toLowerCase().indexOf(".xls") > -1) {
        link = link.replace("{THUMBNAIL_NAME}", "excel_icon.gif");
    }
    else if (src.toLowerCase().indexOf(".pdf") > -1) {
        link = link.replace("{THUMBNAIL_NAME}", "adobepdficon.jpg");
    }
    else if (src.toLowerCase().indexOf(".ppt") > -1) {
        link = link.replace("{THUMBNAIL_NAME}", "ppt_icon.gif");
    }
    else {
        link = link.replace("{THUMBNAIL_NAME}", "file.png");
    }

    return link;
}

