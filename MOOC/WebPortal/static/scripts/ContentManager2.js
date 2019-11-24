var zIndexCounter = 0;
var topElement;
var imageChatEnabled = false;
var currentOpenImageDialog = null;
var currentOpenVideoDialog = null;
var currentOpenFlashDialog = null;
var currentOpenOffersDialog = null;

var activeDialog = "";
var currentOpenDialog = "";
var imageNameSortOrder = "asc";
var imageDateSortOrder = "desc";
var videoNameSortOrder = "asc";
var videoDateSortOrder = "desc";
var zoomPercentage = 100;
var tempZIndex;
var videoOptionalImageInterval = null;
var currentVideoElement = null;
var optInZIndex = 100;

//var TotalElements = [-1, -1, -1, -1];

function LoadContent(functionName, contentType, contentPlaceHolder, sortkey, order) {
    xmlHttp.onreadystatechange = functionName;
    if (sortkey != undefined) {
        //xmlHttp.open('Get', 'GetContent.ashx?type=' + contentType + '&sortkey=' + sortkey + '&order=' + order, true);
    }
    else {
        //xmlHttp.open('Get', 'GetContent.ashx?type=' + contentType, true);
    }
    document.getElementById(contentPlaceHolder).innerHTML = "<img src='static/images/Loading.gif' alt='Loading...' width='16px' height='16px' class='Loading' style='position: relative;top: 100px; left: 130px' /><span style='position: relative;top: 95px; left: 150px'>Loading....</span>";
    //xmlHttp.send(null);
}

function LoadFlash(imageContent) {
    xmlHttp.onreadystatechange = function () { FlashLoaded(event, imageContent); };
    //xmlHttp.open('Get', 'GetContent.ashx?type=flash', true);
    document.getElementById("clImagesContent").innerHTML = "<img src='static/images/Loading.gif' alt='Loading...' width='16px' height='16px' class='Loading' style='position: relative;top: 100px; left: 130px' /><span style='position: relative;top: 95px; left: 150px'>Loading....</span>";
    //xmlHttp.send(null);
}

function DocumentsLoaded(event) {
    var target = event.target ? event.target : event.srcElement;
    if (target.readyState == 4) {
        if (target.status == 200 || target.status == 304) {
            document.getElementById('clDocuments').innerHTML = "";
            var str = xmlHttp.responseText.split("\"");
            var ul = $("<ul/>");

            for (var i = str.length - 1; i > -1; i--) {
                if (str[i] != null && typeof (str[i]) != undefined && str[i] != "") {
                    var paths = str[i].split(":::");
                    SetDocumentOptions(paths[0], paths[1], ul);
                }
            }

            $("#clDocuments").append(ul);
            if (str == "") {
                $("#clDocuments").text("No documents/files uploaded in your content library. Please upload some.");
                document.getElementById("clDocuments").style.padding = "10px";
            }
        }
    }
}

function VideosLoaded(data) {

    var width = parseInt($("#VideoLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#VideoLibraryContent").css("height").replace("px", ""));

    var videos = data.Data.Videos;
    var ul = $("<ul/>");

    for (var i = videos.length - 1; i > -1; i--) {
        SetVideoOptions(videos[i].Thumbnail, videos[i], videos[i].FileName, ul);
    }

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    $("#clVideosContent").empty();
    $("#clVideosContent").css("height", height - 50 + "px");
    $("#clVideosContent").css("width", width + 10 + "px");
    $("#clVideos").css("width", width + 10 + "px");
    
    $("#clVideosContent").append(ul);
    $("#clVideosHeader").show();
    

    $(ul).css("height", height + 60 + "px");
    $(ul).css("width", width + 10 + "px");

    var scroll = $('#clVideosContent').data('jsp');

    if (scroll != undefined) {
        scroll.destroy();
        $("#clVideosContent").jScrollPane({ hideFocus: true });
    }
    else {
        $("#clVideosContent").jScrollPane({ hideFocus: true });
    }

    if (videos.length == 0) {
        $("#clVideosContent").text("No videos uploaded in your content library. Please upload some.");
        $("#clVideosContent").css("padding", "10px");
    }

    $("#clVideosContent").show();
}

function OffersLoaded(event) {
    var target = event.target ? event.target : event.srcElement;
    if (target.readyState == 4) {
        if (target.status == 200 || target.status == 304) {

            document.getElementById('clOffers').innerHTML = "<div style='float: left; font-size: 11px; color: #CCC; padding: 10px;'>Offers by PowerMyLink:</div>";

            var str = xmlHttp.responseText.split("\"");
            var ul = $("<ul/>");
            for (var i = str.length - 1; i > -1; i--) {
                if (str[i] != null && typeof (str[i]) != undefined && str[i] != "") {
                    var paths = str[i].split(":::");
                    if (paths[0].indexOf('mp4') > -1) {
                        SetVideoOptions(paths[0], paths[1], paths[2], ul);
                    }
                    else {
                        SetContentImageOptions(str[i], ul);
                        //SetContentImageOptions(str[i], ul, "offers");
                    }
                }
            }

            $("#clOffers").append(ul);
        }
    }
}

function OptInLoaded(event) {
    var target = event.target ? event.target : event.srcElement;
    if (target.readyState == 4) {
        if (target.status == 200 || target.status == 304) {

            document.getElementById('clLocalOffers').innerHTML = "<div style='float: left; font-size: 11px; color: #CCC; padding: 10px;'>Offers by PowerMyLink:</div>";

            var str = xmlHttp.responseText.split("\"");
            var ul = $("<ul/>");
            for (var i = str.length - 1; i > -1; i--) {
                if (str[i] != null && typeof (str[i]) != undefined && str[i] != "") {
                    var paths = str[i].split(":::");
                    if (paths[0].indexOf('mp4') > -1) {
                        SetVideoOptions(paths[0], paths[1], paths[2], ul);
                    }
                    else {
                        SetContentImageOptions(str[i], ul);
                        //SetContentImageOptions(str[i], ul, "offers");
                    }
                }
            }

            $("#clLocalOffers").append(ul);
        }
    }
}

function FlashLoaded(event, imageContent) {
    var target = event.target ? event.target : event.srcElement;
    if (target.readyState == 4) {
        if (target.status == 200 || target.status == 304) {
            document.getElementById('clImagesContent').innerHTML = "<div style='float: left; font-size: 11px; color: #CCC; padding: 10px;'>Images from PML Library:</div><div style='float: right; margin-top: 10px;'>Sort by: <span style='padding-right: 5px'><a href='#' onclick='LoadPmlContent(ImagesLoaded, \"images\", \"clImagesContent\",\"name\", GetSortOrder(\"image\",\"name\"));'>Name</a></span>|<span style='padding-left: 5px'><a href='#' onclick='LoadPmlContent(ImagesLoaded, \"images\", \"clImagesContent\",\"date\", GetSortOrder(\"image\",\"date\"));'>Date</a></span><span style='padding-left: 5px; margin-right: 5px;'><a href='#' onclick='LoadPmlContent(ImagesLoaded, \"images\", \"clImagesContent\");'><img src='static/images/Reload_hover.png' width='12' height='12' title='Reload Library Content' /></a></span></div><span id='OptionalVideoText3' style='float: right; color: #CCC; font-weight: bold; visibility: hidden; margin-right: 10px; position: relative; top: 5px;'>Attach image to Video</span>";

            var str = JSON.stringify(xmlHttp.responseText);
            str = str.substring(8);
            str = str.split("\"");

            var ul = $("<ul/>");
            for (var i = str.length - 1; i > -1; i--) {
                if (str[i] != null && typeof (str[i]) != undefined && str[i] != "") {
                    if (str[i] == "\\" || str[i] == "\\\\\\" || str[i] == "})" || str[i] == "a\\" || str[i] == ":\\") {
                        continue;
                    }


                    var paths = str[i].split("::");
                    SetContentImageOptions(paths[0].replace("png", "jpg"), ul, paths[1], paths[2], paths[3], paths[4]);
                }
            }

            var str1 = JSON.stringify(imageContent);
            str1 = str1.substring(8);
            str1 = str1.split("\"");

            for (var i = str1.length - 1; i > -1; i--) {

                if (str1[i] != null && typeof (str1[i]) != undefined && str1[i] != "") {
                    if (str1[i] == "\\" || str1[i] == "\\\\\\" || str1[i] == "})" || str[i] == "a\\" || str[i] == ":\\") {
                        continue;
                    }

                    var s = str1[i];
                    if (str1[i].length > 3) {
                        s = str1[i].substring(0, str1[i].length - 3);
                    }

                    SetContentImageOptions(s, ul);
                }
            }

            $("#clImagesContent").append(ul);
            /*
            document.getElementById('clFlash').innerHTML = "<div style='float: left; font-size: 11px; color: #CCC; padding: 10px;'>Flash files from your PML library:</div>";

            var str = xmlHttp.responseText.split("\"");
            var ul = $("<ul/>");
            for (var i = str.length - 1; i > -1; i--) {
            if (str[i] != null && typeof (str[i]) != undefined && str[i] != "") {
            SetContentImageOptions(str[i], ul);
            }
            }

            $("#clFlash").append(ul);*/
        }
    }
}

function ImagesLoaded(data) {
    /*var target = event.target ? event.target : event.srcElement;
    if (target.readyState == 4) {
    if (target.status == 200 || target.status == 304) {
    LoadFlash(xmlHttp.responseText);
    }
    }*/

    //LoadFlash(data.Data.Images);
    var width = parseInt($("#ImageLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#ImageLibraryContent").css("height").replace("px", ""));

    var images = data.Data.Images;
    //document.getElementById('clImages').innerHTML = "<div style='float: left; font-size: 11px; color: #CCC; padding: 10px;'>Images from PML Library:</div><div style='float: right; margin-top: 10px;'>Sort by: <span style='padding-right: 5px'><a href='#' onclick='LoadPmlContent(ImagesLoaded, \"images\", \"clImages\",\"name\", GetSortOrder(\"image\",\"name\"));'>Name</a></span>|<span style='padding-left: 5px'><a href='#' onclick='LoadPmlContent(ImagesLoaded, \"images\", \"clImages\",\"date\", GetSortOrder(\"image\",\"date\"));'>Date</a></span><span style='padding-left: 5px; margin-right: 5px;'><a href='#' onclick='LoadPmlContent(ImagesLoaded, \"images\", \"clImages\");'><img src='static/images/Reload_hover.png' width='12' height='12' title='Reload Library Content' /></a></span></div><span id='OptionalVideoText3' style='float: right; color: #CCC; font-weight: bold; visibility: hidden; display: none; margin-right: 10px; position: relative; top: 5px;'>Attach image to Video</span>";
    var ul = $("<ul/>");
    for (var i = images.length - 1; i > -1; i--) {
        SetContentImageOptions(images[i], ul);
    }


    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    /*$(ul).css("margin", "0px");
    $(ul).css("position", "absolute");*/
    $("#clImagesContent").empty();
    //$("#clImagesContent").jScrollPane({ destroy: true });
    $("#clImagesContent").css("height", height - 50 + "px");
    $("#clImagesContent").css("width", width + 10 + "px");

    $("#clImagesContent").append(ul);
    $("#clImagesHeader").show();

    $(ul).css("height", height + 60 + "px");
    $(ul).css("width", width + 10 + "px");

    //alert(parseInt($("#clImages").css("width").replace("px", "")) + "px");
    var scroll = $('#clImagesContent').data('jsp');

    if (scroll != undefined) {
        scroll.destroy();
        //scroll.reinitialise();
        $("#clImagesContent").jScrollPane({ hideFocus: true });
    }
    else {
        $("#clImagesContent").jScrollPane({ hideFocus: true });
    }
    /*$(ul).slimScroll({
    height: parseInt($("#clImages").css("height").replace("px", "")) - 50 + "px",
    width: parseInt($("#clImages").css("width").replace("px", "")) + "px",
    color: THEME.Gradient,
    size: '5px'
    });*/
}

function GetImageContentDetailsContainer(attrCaption, attrImageName, attrDescription, attrUrl) {
    var container = $("<div/>");
    var caption = $("<div/>");
    var name = $("<div/>");
    var description = $("<div/>");
    var link = $("<div/>");

    var captionText = $("<input/>");
    var nameText = $("<input/>");
    var descriptionText = $("<input/>");
    var linkText = $("<input/>");

    var saveButton = $("<input/>");
    var cancelButton = $("<input/>");

    $(saveButton).attr("type", "button").attr("value", "Save");
    $(saveButton).css("float", "left").css("margin-left", "10px");
    $(saveButton).attr("class", "btn btn-info");
    $(cancelButton).attr("class", "btn btn-info");
    $(cancelButton).attr("type", "button").attr("value", "Cancel"); ;
    $(cancelButton).css("float", "left").css("margin-left", "10px");
    $(cancelButton).click(function () {
        $("#PmlImageDetails").hide("slide", { direction: "left" }, 500);
    });

    $(caption).html("Caption");
    $(name).html("Name");
    $(description).html("Description");
    $(link).html("Link");

    $(captionText).attr("type", "text").attr("class", "span").attr("size", "16").attr("placeholder", "Caption...").attr("type", "text");
    $(nameText).attr("type", "text").attr("class", "span").attr("size", "16").attr("placeholder", "Name...").attr("type", "text");
    $(descriptionText).attr("type", "text").attr("class", "span").attr("size", "16").attr("placeholder", "Description...").attr("type", "text");
    $(linkText).attr("type", "text").attr("class", "span").attr("size", "16").attr("placeholder", "Link...").attr("type", "text");

    if (attrCaption != null && attrCaption != undefined && attrCaption != "null") {
        $(captionText).attr("value", attrCaption);
    }

    if (attrImageName != null && attrImageName != undefined && attrImageName != "null") {
        $(nameText).attr("value", attrImageName);
    }

    if (attrDescription != null && attrDescription != undefined && attrDescription != "null") {
        $(descriptionText).attr("value", attrDescription);
    }
    if (attrUrl != null && attrUrl != undefined && attrUrl != "null") {
        $(linkText).attr("value", attrUrl);
    }

    /*$(caption).css("width", "80px").css("float", "left");
    $(name).css("width", "80px").css("float", "left");
    $(description).css("width", "80px").css("float", "left");
    $(link).css("width", "80px").css("float", "left");
    */

    $(captionText).css("width", "300px");
    $(nameText).css("width", "300px");
    $(descriptionText).css("width", "300px");
    $(linkText).css("width", "300px");

    $(container).css("float", "left").css("width", "100%").css("position", "absolute").css("top", "70%").css("left", "10%");

    //$(container).append(caption);
    $(container).append(captionText);
    //$(container).append(name);
    $(container).append(nameText);
    //$(container).append(description);
    $(container).append(descriptionText);
    //$(container).append(link);
    $(container).append(linkText);
    $(container).append(saveButton);
    $(container).append(cancelButton);


    $("#PmlImageDetails").append(container);
}

function SetContentImageOptions(image, ul, flashFileUrl, caption, width, height) {
    var li = $("<li/>");
    var orgUrl = "";
    $(li).attr("class", "ImageItem2");

    var icons = $("<div/>");
    var infoIcon = $("<i/>");
    var deleteIcon = $("<i/>");
    var attachIcon = $("<i/>");

    $(icons).attr("class", "content-icon-container");
    $(attachIcon).attr("class", "icon-plus content-icon");

    $(infoIcon).attr("class", "icon-info-sign content-icon");
    $(deleteIcon).attr("class", "icon-trash content-icon");

    $(attachIcon).attr("title", "Attach image to current PML");
    $(infoIcon).attr("title", "More information about this image...");
    $(deleteIcon).attr("title", "Delete image");

    $(attachIcon).click(function () { Attach($(img)); });

    $(infoIcon).click(function () {
        $("#PmlImageDetails").empty();
        var close = $("<span/>");
        $(close).html("x");
        $(close).attr("style", "margin: 10px; margin-left: 530px; position: absolute; cursor: pointer");
        $(close).attr("class", "inf_link");
        $(close).click(function () {
            //$("#PmlImageDetails").empty();
            $("#PmlImageDetails").hide("slide", { direction: "left" }, 1000);
        });

        $("#PmlImageDetails").show("slide", { direction: "left" }, 1000);
        var clone = $(this).parent().parent().find("img").clone();
        $(clone).css("width", "450px").css("height", "325px").css("position", "absolute").css("top", "5%").css("left", "10%");
        $("#PmlImageDetails").append(close);
        $("#PmlImageDetails").append(clone);

        GetImageContentDetailsContainer($(clone).attr("caption"), $(clone).attr("imagename"), $(clone).attr("description"), $(clone).attr("linkurl"));
    });

    $(deleteIcon).click(function (event) {
        /*ShowPmlAlertMessage("Delete Image", "Are you sure you want to delete this file?", function () {
        var url = $(this).parent().parent().find("img").attr("originalUrl");
        DeleteContent(url, false);
        });*/

        if (confirm("Are you sure you want to delete this file?")) {
            var url = $(this).parent().parent().find("img").attr("originalUrl");
            DeleteContent(url, false);
        }

        event.stopPropagation();
        event.preventDefault();
    });

    //var names = src.split("::");
    var img = document.createElement("img");
/* AWS
    
    img.setAttribute("src", names[0]);
*/
    img.setAttribute("src", image.Path + "_thumb.jpg");


    if (flashFileUrl != undefined) {
        img.setAttribute("originalUrl", flashFileUrl);
        img.setAttribute("width", width);
        img.setAttribute("height", height);
        img.setAttribute("title", caption);
        img.setAttribute("alt", caption);
        orgUrl = flashFileUrl;
    }
    else {
        img.setAttribute("originalUrl", image.Path);
        img.setAttribute("width", "150");
        img.setAttribute("height", "120");
        orgUrl = image.Path;
    }

    img.setAttribute("class", "DragContent ThumbnailImage");

    /*if (isOffers != undefined) {
    img.setAttribute("isOffers", "true");
    }*/

    img.setAttribute("originalheight", originalImageHeight);
    img.setAttribute("originalwidth", originalImageWidth);
    img.setAttribute("originalname", image.FileName);

    img.setAttribute("caption", image.Caption);
    img.setAttribute("imagename", image.FileName);
    img.setAttribute("description", image.Description);
    img.setAttribute("linkurl", image.Url);

    $(img).click(function () {
        Attach(img);
    });

    var attach = document.createElement('span');
    var deleteButton = document.createElement("div");
    deleteButton.setAttribute("class", "ContentDeleteButton");
    deleteButton.setAttribute("title", "Delete this content from PML Content Library");
    deleteButton.onclick = function (event) {
        if (confirm("Are you sure you want to delete this file?")) {
            var url = $(this).parent().parent().find("img").attr("originalUrl");
            DeleteContent(url, false);
        }
        event.stopPropagation();
        event.preventDefault();
    }
    attach.innerHTML = 'Attach <br /><br />Name - ' + image.FileName;
    attach.setAttribute("class", "attach2");
    $(attach).mouseover(function () { $(this).show(); });
    $(attach).mouseout(function () { $(this).hide(); });
    if (orgUrl.indexOf("/content") > -1) {
        $(attach).append(deleteButton);
    }
    //$(li).append(attach);

    $(img).mouseover(
            function () {
                $(img).siblings('span').show();
            }
            );

    $(img).mouseout(
            function () {
                $(img).siblings('span').hide();
            }
            );

    $(attach).click(function () {
        Attach(img);
    });

    $(li).append(img);
    $(icons).append(attachIcon);
    $(icons).append(infoIcon);
    $(icons).append(deleteIcon);
    $(li).append(icons);
    $(ul).append(li);

}

function SetVideoOptions(src, video, name, ul) {
    var li = $("<li/>");
    $(li).attr("class", "ImageItem2");

    var img = document.createElement("img");
    img.setAttribute("src", video.Thumbnail);
    img.setAttribute("originalUrl", video.Path);
    img.setAttribute("url", video.Thumbnail);
    if (src.indexOf('.flv') > 0)
        img.setAttribute("source", src);
    else
        img.setAttribute("source", video.Path);
    img.setAttribute("class", "DragContent ThumbnailImage");
    img.setAttribute("type", "video");
    img.setAttribute("width", "150");
    img.setAttribute("height", "120");
    img.setAttribute("originalheight", originalImageHeight);
    img.setAttribute("originalwidth", originalImageWidth);
    img.setAttribute("originalname", name);

    img.setAttribute("caption", video.Caption);
    img.setAttribute("videoname", video.FileName);
    img.setAttribute("description", video.Description);
    img.setAttribute("linkurl", video.Url);

    var icons = $("<div/>");
    var infoIcon = $("<i/>");
    var deleteIcon = $("<i/>");
    var attachIcon = $("<i/>");
    var playIcon = $("<i/>");

    $(icons).attr("class", "content-icon-container");
    $(attachIcon).attr("class", "icon-plus content-icon");
    $(playIcon).attr("class", "icon-play content-icon");

    $(infoIcon).attr("class", "icon-info-sign content-icon");
    $(deleteIcon).attr("class", "icon-trash content-icon");

    $(attachIcon).attr("title", "Attach Video To Current PML");
    $(playIcon).attr("title", "Play Video");
    $(infoIcon).attr("title", "More Information About This Video...");
    $(deleteIcon).attr("title", "Delete Video");

    $(attachIcon).click(function () { Attach($(img)); });
    $(playIcon).click(function () { alert("Video will be played later"); });

    $(infoIcon).click(function () {
        $("#PmlVideoDetails").empty();
        var close = $("<span/>");
        $(close).html("x");
        $(close).attr("style", "margin: 10px; margin-left: 530px; position: absolute; cursor: pointer");
        $(close).attr("class", "inf_link");
        $(close).click(function () {
            //$("#PmlImageDetails").empty();
            $("#PmlVideoDetails").hide("slide", { direction: "left" }, 1000);
        });

        $("#PmlVideoDetails").show("slide", { direction: "left" }, 1000);
        var clone = $(this).parent().parent().find("img").clone();
        $(clone).css("width", "450px").css("height", "325px").css("position", "absolute").css("top", "5%").css("left", "10%");
        $("#PmlVideoDetails").append(close);
        $("#PmlVideoDetails").append(clone);

        GetImageContentDetailsContainer($(clone).attr("caption"), $(clone).attr("imagename"), $(clone).attr("description"), $(clone).attr("linkurl"));
    });

    $(deleteIcon).click(function (event) {
        /*ShowPmlAlertMessage("Delete Image", "Are you sure you want to delete this file?", function () {
        var url = $(this).parent().parent().find("img").attr("originalUrl");
        DeleteContent(url, false);
        });*/

        if (confirm("Are you sure you want to delete this file?")) {
            var url = $(this).parent().parent().find("img").attr("originalUrl");
            DeleteContent(url, true);
        }

        event.stopPropagation();
        event.preventDefault();
    });

    $(img).click(function () {
        Attach(img);
    });

    /*var attach = document.createElement('span');
    var deleteButton = document.createElement("div");
    deleteButton.setAttribute("class", "ContentDeleteButton");
    deleteButton.setAttribute("title", "Delete this content from PML Content Library");
    deleteButton.onclick = function (event) {
    if (confirm("Are you sure you want to delete this file?")) {
    var url = $(this).parent().parent().find("img").attr("url");
    DeleteContent(url, true);
    }
    event.stopPropagation();
    event.preventDefault();
    }

    if (name == "" || name == undefined) {
    attach.innerHTML = 'Attach';
    }
    else {
    attach.innerHTML = 'Attach<br /><br /> Name - ' + name;
    }
    attach.setAttribute("class", "attach2");
    $(attach).mouseover(function () { $(this).show(); });
    $(attach).mouseout(function () { $(this).hide(); });
    $(attach).append(deleteButton);
    //$(li).append(attach);

    $(img).mouseover(
    function () {
    $(img).siblings('span').show();
    }
    );

    $(img).mouseout(
    function () {
    $(img).siblings('span').hide();
    }
    );

    $(attach).click(function () {
    Attach(img);
    });*/

    $(icons).append(attachIcon);
    $(icons).append(playIcon);
    $(icons).append(infoIcon);
    $(icons).append(deleteIcon);

    $(li).append(img);
    $(li).append(icons);
    $(ul).append(li);

}

function DeleteContent(originalUrl, isVideo) {
    var xhr = new XMLHttpRequest();

    xhr.open("GET", "DeleteContent.ashx?url=" + originalUrl + "&video=" + isVideo, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (!isVideo) {
                LoadPmlContent(ImagesLoaded, "images", "clImagesContent");
            }
            else {
                LoadPmlContent(VideosLoaded, "videos", "clVideosContent");
            }
        }
    };
    xhr.send(null);
}

function SetDocumentOptions(src, thumbnailUrl, ul) {
    var li = $("<li/>");
    $(li).attr("class", "ImageItem2");

    var img = document.createElement("img");
    img.setAttribute("src", thumbnailUrl);
    img.setAttribute("originalUrl", src);
    img.setAttribute("url", thumbnailUrl);
    img.setAttribute("source", src);
    img.setAttribute("type", "document");
    img.setAttribute("class", "DragContent ThumbnailImage");
    img.setAttribute("width", "80");
    img.setAttribute("height", "60");
    img.setAttribute("originalheight", originalImageHeight);
    img.setAttribute("originalwidth", originalImageWidth);

    $(img).click(function () {
        Attach(img);
    });

    var attach = document.createElement('span');
    attach.innerHTML = 'Attach';
    attach.setAttribute("class", "attach2");
    $(attach).mouseover(function () { $(this).show(); });
    $(attach).mouseout(function () { $(this).hide(); });
    $(li).append(attach);

    $(img).mouseover(
            function () {
                $(img).siblings('span').show();
            }
            );

    $(img).mouseout(
            function () {
                $(img).siblings('span').hide();
            }
            );

    $(attach).click(function () {
        Attach(img);
    });

    $(li).append(img);
    $(ul).append(li);
}

function SaveImageOptions() {
    var imageOptions = document.getElementById("ImageOptions");
    var img = imageOptions.getElementsByTagName("img");
    currentImageObject.setAttribute("width", document.getElementById('txtImageWidth').value);
    currentImageObject.setAttribute("height", document.getElementById('txtImageHeight').value);
    currentImageObject.setAttribute("title", document.getElementById('txtImageCaption').value);
    currentImageObject.setAttribute("description", document.getElementById('txtImageDescription').value);
    var link = document.getElementById('txtLink').value;
    currentImageObject.setAttribute("link", document.getElementById('txtLink').value);
    if (link != null && link.length > 7) {
        currentImageObject.setAttribute("onclick", "window.open('" + link + "')");
        $(currentImageObject).css('cursor', 'pointer');
    }

    if (imageChatEnabled) {
        var chatUserIdvar = document.getElementById("ChatUserId");
        if (chatUserIdvar != null && chatUserIdvar.length > 4) {
            currentImageObject.setAttribute("onclick", '"OpenChatDialog("Me","' + chatUserIdvar + '"');
            $(currentImageObject).css('cursor', 'pointer');
        }
    }

    $(currentImageObject).css('width', document.getElementById('txtImageWidth').value + 'px');
    $(currentImageObject).css('height', document.getElementById('txtImageHeight').value + 'px');
    //imageOptions.removeChild(img[0]);
    document.getElementById('txtImageWidth').value = "";
    document.getElementById('txtImageHeight').value = "";
    document.getElementById('txtImageCaption').value = "";
    document.getElementById('txtImageDescription').value = "";
    document.getElementById('txtLink').value = "";
    $("#ImageOptions").dialog("close");
    GetPreview();
}

function DeleteImage(img) {
    if (confirm("Are you sure, you want to delete this image?")) {
        var parent = img.parentNode;
        parent.removeChild(img);
        //ResetOfferBankValue();
    }
}

function SetImageOptions(img) {
    if (img.tagName == 'img' ||
        img.tagName == 'IMG') {
    } else {
        img = $(img).find('img');
    }
    var width = $(img).width() + "";
    var originalwidth = $(img).attr("originalwidth");
    var style = $(img).attr("style");
    var height = $(img).height() + "";
    var originalHeight = $(img).attr("originalheight");
    var title = $(img).attr("title");
    var link = $(img).attr("link");
    var description = $(img).attr("description");
    var clone = $(img)[0].cloneNode(true);
    clone.style.display = "none";

    if (width != null && typeof (width) != undefined) {
        document.getElementById('txtImageWidth').value = width.replace("px", "");
    }
    else if (originalwidth != null && typeof (originalwidth) != undefined) {

        document.getElementById('txtImageWidth').value = originalwidth.replace("px", "");
    }

    if (height != null && typeof (height) != undefined) {
        document.getElementById('txtImageHeight').value = height.replace("px", "");
    }
    else if (originalHeight != null && typeof (originalHeight) != undefined) {
        document.getElementById('txtImageHeight').value = originalHeight.replace("px", "");
    }
    if (title != undefined)
        document.getElementById('txtImageCaption').value = title;
    if (description != undefined)
        document.getElementById('txtImageDescription').value = description;
    if (link != undefined)
        document.getElementById('txtLink').value = link;
    $("#OptionImages").empty();
    document.getElementById("OptionImages").appendChild(clone);
    currentImageObject = $(img)[0];

    $("#ImageOptions").dialog("open");
}

function AddDocument() {
    var url = $("#txtDocumentUrl").val();
    if (url == null || url.toLowerCase() == "enter document url..." || url.length < 10) {
        alert("The url doesn't appear to be valid");
        return;
    }
    else {

        var link = "<a href='" + url + "'><img src='static/images/{THUMBNAIL_NAME}' width='40px' height='40px' /></a>";

        if (link.toLowerCase().indexOf(".doc") > -1) {
            link = link.replace("{THUMBNAIL_NAME}", "document-thumb-1.jpg");
        }
        else if (link.toLowerCase().indexOf(".xls") > -1) {
            link = link.replace("{THUMBNAIL_NAME}", "excel_icon.gif");
        }
        else if (link.toLowerCase().indexOf(".pdf") > -1) {
            link = link.replace("{THUMBNAIL_NAME}", "adobepdficon.jpg");
        }
        else if (link.toLowerCase().indexOf(".ppt") > -1) {
            link = link.replace("{THUMBNAIL_NAME}", "ppt_icon.gif");
        }
        else {
            link = link.replace("{THUMBNAIL_NAME}", "file.png");
        }

        jHtmlArea($(".editor")).textarea.val(jHtmlArea($(".editor")).textarea.val() + link);
        jHtmlArea($(".editor")).updateHtmlArea();
        GetPreview();
    }
}

function SetZIndex(currentElement) {
    $(topElement).css("zIndex", currentElement.style.zIndex);
    currentElement.style.zIndex = zIndexCounter - 1;
    topElement = currentElement;
    if ($(topElement).attr("mediatype") != undefined && $(topElement).attr("mediatype") == "text") {
        $("#" + GetCurrentOpenContainer()).find("#RichToolBar").each(function () { $(this).css("zIndex", currentElement.style.zIndex); });
    }
}

function UpdateZIndexCounter() {
    zIndexCounter = zIndexCounter + 1;
    return zIndexCounter;
}

function ResetOfferBankValue() {
    // Viren - Aug 23, 2012
    // This length will be approximately 8700 without any html elements inside the container. When any of the media elements are added the length will be much larger than 8800.
    // This is temporary/quick fix. In order to change have better solution: we should check for individual elements - images/videos/text/flash/forms. 
    // If we dont find any of these elements, we should remove the attribute - isoffer
    if ($("#" + GetCurrentOpenContainer()).html().length < 8800) {
        $("#" + GetCurrentOpenContainer()).removeAttr("isoffer");
    }
}

function Reset() {
    RestoreDefaultIcons();
    HideVideoMenuOptions();
    HideImageMenuOptions();
    HideFlashMenuOptions();
    HideOfferBankMenuOptions();
    //HideOfferBankMenuOptions();
}
function ResetImageMenu() {
    RestoreDefaultIcons();
}

function SetImageMenu(elem) {
    currentOpenDialog = "Image";
    RestoreDefaultIcons();
    ActiveElement(elem, 'image_icon');
    ActiveContentTab($('#ImageMenuOptions').find('li:first'));
    HideVideoMenuOptions();
    HideFlashMenuOptions();
    HideOfferBankMenuOptions();
    ShowImageMenuOptions();
}
function ResetVideoMenu() {
    RestoreDefaultIcons();
}
function SetVideoMenu(elem) {
    if (videoOptionalImageInterval == null) {
        currentOpenDialog = "Video";
        RestoreDefaultIcons();
        ActiveElement(elem, 'video_icon');
        ActiveContentTab($('#VideoMenuOptions').find('li:first'));
        HideImageMenuOptions();
        HideFlashMenuOptions();
        HideOfferBankMenuOptions();
        ShowVideoMenuOptions();
    }
    else {
        if (HideImageMenuOptions()) {
            currentOpenDialog = "Video";
            RestoreDefaultIcons();
            ActiveElement(elem, 'video_icon');
            ActiveContentTab($('#VideoMenuOptions').find('li:first'));
            ShowVideoMenuOptions();
        }
    }
}

function SetOfferBankMenu(elem) {
    currentOpenDialog = "OfferBank";
    RestoreDefaultIcons();
    ActiveElement(elem, 'offerbank_icon');
    ActiveContentTab($('#OfferBankMenuOptions').find('li:first'));
    HideImageMenuOptions();
    HideVideoMenuOptions();
    HideFlashMenuOptions();
    ShowOfferBankMenuOptions();
}

function ResetOfferBankMenu() {
    RestoreDefaultIcons();
}
function SetFlashMenu(elem) {
    currentOpenDialog = "Flash";
    RestoreDefaultIcons();
    ActiveElement(elem, 'flash_icon');
    ActiveContentTab($('#FlashMenuOptions').find('li:first'));
    HideImageMenuOptions();
    HideVideoMenuOptions();
    HideOfferBankMenuOptions();
    ShowFlashMenuOptions();
}

function ResetFlashMenu() {
    RestoreDefaultIcons();
}
function GetPowerPointMenuBar(menuContainer) {
    // Text button
    var addTextList = document.createElement("li");
    var addTextLink = document.createElement("a");
    addTextLink.setAttribute("href", "#");
    addTextLink.setAttribute("target", "_self");
    //addTextLink.setAttribute("class", "icon_r_row3");
    addTextLink.setAttribute("class", "text_icon");
    addTextLink.setAttribute("title", "Add Text");
    addTextLink.setAttribute("onclick", "add_text();RestoreDefaultIcons();HideVideoMenuOptions();HideImageMenuOptions();HideFlashMenuOptions();HideOfferBankMenuOptions();return false;");
    addTextList.appendChild(addTextLink);

    // Add images
    var image = document.createElement("li");
    var imageLink = document.createElement("a");
    var currentSpan = document.createElement("span");

    imageLink.setAttribute("href", "#");
    imageLink.setAttribute("target", "_self");
    imageLink.setAttribute("class", "image_icon");
    //imageLink.setAttribute("class", "icon_o_row1");
    imageLink.setAttribute("title", "Add Image");
    //currentSpan.setAttribute("class", "current");
    //imageLink.appendChild(currentSpan);
    image.appendChild(imageLink);

    var imageFlag = false;
    var videoFlag = false;
    image.setAttribute("onmouseover", "SetImageMenu(this)");
    image.setAttribute("onmouseout", "ResetImageMenu(this)");

    // Add Videos
    var video = document.createElement("li");
    var videoLink = document.createElement("a");

    videoLink.setAttribute("href", "#");
    videoLink.setAttribute("target", "_self");
    videoLink.setAttribute("class", "video_icon");
    //videoLink.setAttribute("class", "icon_b_row2");
    videoLink.setAttribute("title", "Add Video");
    video.appendChild(videoLink);

    // Add Offer Bank
    var offerBank = document.createElement("li");
    var offerBankLink = document.createElement("a");

    offerBankLink.setAttribute("href", "#");
    offerBankLink.setAttribute("target", "_self");
    offerBankLink.setAttribute("class", "offerbank_icon");
    //offerBankLink.setAttribute("class", "icon_gy_row20");
    offerBankLink.setAttribute("title", "Add from Offer Bank");
    offerBank.appendChild(offerBankLink);

    video.setAttribute("onmouseover", "SetVideoMenu(this)");
    video.setAttribute("onmouseout", "ResetVideoMenu()");

    offerBank.setAttribute("onmouseover", "SetOfferBankMenu(this)");
    offerBank.setAttribute("onmouseour", "ResetOfferBankMenu(this)");
    menuContainer.appendChild(image);
    menuContainer.appendChild(video);
    //menuContainer.appendChild(addTextList);

    /* Permission based access */
    /*if (PERMISSIONS.add_image == true || PERMISSIONS.add_image == "true") {
    menuContainer.appendChild(image);
    }
    if (PERMISSIONS.add_video == true || PERMISSIONS.add_video == "true") {
    menuContainer.appendChild(video);
    }
    if (PERMISSIONS.add_text == true || PERMISSIONS.add_text == "true") {
    menuContainer.appendChild(addTextList);
    }
    if (PERMISSIONS.add_offers == true || PERMISSIONS.add_offers == "true") {
    //menuContainer.appendChild(offerBank);
    }*/
    var optin = document.createElement("li");
    var optinLink = document.createElement("a");

    optinLink.setAttribute("href", "#");
    optinLink.setAttribute("target", "_self");
    optinLink.setAttribute("class", "offerbank_icon");
    //offerBankLink.setAttribute("class", "icon_gy_row20");
    optinLink.setAttribute("title", "Add Optin Form");
    optin.appendChild(optinLink);
    optin.setAttribute("onclick", "AddOptInForm()");

    //menuContainer.appendChild(optin);
    PrepareImageMenuOptions();
    PrepareVideoMenuOptions();
    PrepareOfferBankMenuOptions();
}

function AddOptInForm(frm) {
    //var c = "#" + GetCurrentOpenContainer();
    //if (!CheckPermissions("make_optin_form")) {
    //return;
    //}

    var element = document.getElementById(GetCurrentOpenContainer());
    var items = ["Enter Field", "Enter Field"];
    var title = "Enter Details";
    var width;
    var height;
    var top = GlobalTop;
    var left = GlobalLeft;
    var zi = 0;
    var min = 0;
    var max = 305;
    if (frm != null) {
        var count = parseInt($(frm).attr("op-opts"));
        if (count > 0)
            items = [];
        for (var i = 0; i < count; i++) {
            items.push($(frm).attr("op-" + i));
        }
        title = $(frm).attr("op-title");
        top = $(frm).attr("pos-top");
        left = $(frm).attr("pos-left");
        width = $(frm).attr("pos-w");
        height = $(frm).attr("pos-h");


        zi = $(frm).attr("zi");
        if (zi == null ||
          zi == "undefined")
            zi = 0;
        if ($(frm).attr("stime") != null) {
            var stime = $(frm).attr("stime");
            var parts = stime.split(':');
            min = parseInt(parts[0]) * 60 + parseInt(parts[1]);
        }
        if ($(frm).attr("etime") != null &&
              $(frm).attr("etime") != "end") {
            var etime = $(frm).attr("etime");
            var parts = etime.split(':');
            max = parseInt(parts[0]) * 60 + parseInt(parts[1]);
        }
    }

    if (title == null ||
        title == "undefined")
        title = "";
    var form = GetBasicForm(items);
    AddOptinIteminGlobalArray(form);

    if ($(frm).attr("period") != null) {
        $(form).attr("period", $(frm).attr("period"));
        $(form).find("#pml_add_timer").find('img').attr('src', 'static/images/timer-on.png');
    }
    //$(form).css("z-index", UpdateZIndexCounter());
    $(form).find(".pml_opt_title").val(title);

    $(form).css("position", "absolute");
    $(form).css("top", top + "px");
    $(form).css("left", left + "px");
    $(form).css("width", width + "px");
    $(form).css("height", height + "px");

    AddOptinTime($(form), min, max);
    // since opt-in form shall be always on top of other media element, we need to make different z-index variable to keep track of optin form overlays
    $(form).css("zIndex", optInZIndex);
    $(form).attr("zi", optInZIndex);
    optInZIndex++;

    UpdateMediaCoordinates();
    var previousSize = 100;
    $(form).draggable({ containment: $(form).parents(".SubTabContent") });
    $(form).resizable({
        minWidth: 120,
        minHeight: 60,
        containment: "#" + GetCurrentOpenContainer(),
        start: function () {
            $(this).find("input").each(function () {
                $(this).css("font-size", "11px");
                $(this).focus();
                $(this).blur();
            });
        },
        resize: function () {
            $(this).find(".PmlFormName").parent().find('a').css("top", "");
            ResizeOptinForm($(this));
            //$(this).find("table").css({width: $(this).width(), height: $(this).height()});
        }
    });

    //AddTextIteminGlobalArray(form);
    $(element).append(form);
    $(form).find('#pml_remove_form').bind('click', function () {
        if ($(this).parents('.PmlFormContainer').length > 0) {
            if (confirm("Are you sure you want to remove this form?")) {
                var item = $(this).parents('.PmlFormContainer').attr("itemnumber");
                $(this).parents('.PmlFormContainer').remove();
                RemoveThumbnail(parseInt(item));
            }
        }
    });

    $(form).find('#pml_add_timer').bind('click', function () {
        var period = $(form).attr("period");
        if (period == undefined || period == "undefined")
            period = "01:00";
        period = prompt("Please enter the timer period (mm:ss) : ", period);
        $(form).attr("period", period);
        if (period != "") {
            $(this).find('img').attr('src', 'static/images/timer-on.png');
        } else {
            $(this).find('img').attr('src', 'static/images/timer.png');
        }
    });

    $(form).find('#pml_add_field').bind('click', function () {
        if ($(this).parents('.PmlFormContainer').find('input[type="text"]').length == 3) {
            alert('Maximum 3 fields allowed.');
            return;
        }
        $("<div class='field-row' style='padding:5px;height:25px;'><input type='text' value='Enter Field' class='PmlFormName' name='pmlform_field' style='height:100%'><a style='float:right' id='pml_remove_field' href='javascript:void(0);'>" + GetImage("static/images/minus.gif", '').outerHTML + "</a></div>").insertBefore($(this).parents('.PmlFormContainer').find(".FormButton"));
        ResizeOptinForm($(this).parents(".PmlFormContainer"));
        $(form).find('.PmlFormName').unbind('focus');
        $(form).find('.PmlFormName').bind('focus', function () {
            if ($(this).val() == 'Enter Field') {
                $(this).val('');
            }
        });
    });

    $(form).find('.PmlFormName').bind('focus', function () {
        if ($(this).val() == 'Enter Field') {
            $(this).val('');
        }
    });

    $(form).find('.PmlFormName').bind('blur', function () {
        if ($(this).val() == '') {
            $(this).val('Enter Field');
        }
    });
    //$("#" + GetCurrentOpenContainer()).attr("isoffer", "false");
    //GetPreview();

    HideFlashMenuOptions();
    HideOfferBankMenuOptions();
    HideImageMenuOptions();
    HideVideoMenuOptions();
}

function AddOptinTime(form, min, max) {

    $(form).find("#TimerSlider").slider({
        range: true,
        min: 0,
        max: 305,
        values: [min, max],
        slide: function (event, ui) {
            var start = CalculateTime(ui.values[0]);
            $(this).parent().find("#StartTime").html(start);
            $(this).parent().find("#StartTime").css('left', $($(this).find('a')[0]).css('left'));
            var end = CalculateTime(ui.values[1]);
            $(this).parent().find("#EndTime").html(end);
            $(this).parent().find("#EndTime").css('left', $($(this).find('a')[1]).css('left'));
        }
    });

    var start = CalculateTime(min);
    $(form).find("#StartTime").html(start);
    $(form).find("#StartTime").css('left', $($(form).find("#TimerSlider").find('a')[0]).css('left'));
    var end = CalculateTime(max);
    $(form).find("#EndTime").html(end);
    $(form).find("#EndTime").css('left', $($(form).find("#TimerSlider").find('a')[1]).css('left'));
}

function ActiveElement(element, imageClassName) {

    $(element).find("a").each(function () {
        switch (imageClassName) {
            case "image_icon":
                $(this).css("background-position", "-53px -6px");
                break;
            case "video_icon":
                $(this).css("background-position", "-53px -39px");
                break;
            case "flash_icon":
                $(this).css("background-position", "-53px -39px");
                break;
            case "offerbank_icon":
                $(this).attr("class", "OfferBankIcon");
                break;
        }

    });
}

function RestoreDefaultIcons() {
    $("#SideMenuBar").find("a").each(function () {
        var linkClass = $(this).attr("class");
        switch (linkClass) {
            case "image_icon":
                if (currentOpenDialog != "Image") {
                    $(this).css("background-position", "-13px -6px");
                }
                break;
            case "video_icon":
                if (currentOpenDialog != "Video") {
                    $(this).css("background-position", "-13px -39px");
                }
                break;
            case "offerbank_icon":
            case "OfferBankIcon":
                if (currentOpenDialog != "OfferBank") {
                    $(this).attr("class", "InactiveOfferBank");
                }
                break;
            case "flash_icon":
                if (currentOpenDialog != "Flash") {
                    $(this).css("background-position", "-13px -39px");
                }
                break;
        }
    });
}

function DeactivateElement(elementId, imageName) {
    $("#" + elementId).find("img").attr("src", "static/images/" + imageName + ".png");
}

function ShowImageMenuOptions() {
    $("#ImageMenuOptions").show("slide", {}, 500);
    SetUploadOptions();
    SetActiveImagesTab('searchImages');
    //DisplayImageOptions('searchImages');

    /*if (PERMISSIONS.search_image == true || PERMISSIONS.search_image == "true") {
    DisplayImageOptions('searchImages');
    }
    else if (PERMISSIONS.upload_image == true || PERMISSIONS.upload_image == "true") {
    DisplayImageOptions('clLocalImages')
    }
    else {
    DisplayImageOptions('clImages');
    }*/

    LoadPmlContent(ImagesLoaded, 'images', 'clImagesContent');
    /*$("#ImageLibraryContent").css("width", "350px");
    $("#ImageLibraryContent").css("height", "300px");*/
    if ($("#search_results2").html() == "") {
        //$("#search_results2").css("height", "238px");
        $("#ImageLibraryContent").css("width", "540px");
        $("#ImageLibraryContent").css("height", "510px");
        $("#clImagesContent").css("height", "290px");
    }
}

function ShowOfferBankMenuOptions() {
    $("#OfferBankMenuOptions").show("slide", {}, 500);
    LoadPmlContent(OffersLoaded, 'offers', 'clOffers');
    DisplayOfferBankOptions('clOffers');
    //DisplayOfferBankOptions('clLocalOffers');

    //LoadPmlContent(OptInLoaded, 'offers', 'clLocalOffers');
    /*$("#OfferBankLibraryContent").css("width", "350px");
    $("#OfferBankLibraryContent").css("height", "300px");
    $("#clOffers").css("height", "290px");*/
}

function ShowFlashMenuOptions() {
    $("#FlashMenuOptions").show("slide", {}, 500);
    DisplayFlashOptions('clFlash');
    LoadPmlContent(FlashLoaded, 'flash', 'clFlash');
    $("#FlashLibraryContent").css("width", "350px");
    $("#FlashLibraryContent").css("height", "300px");
    $("#clFlash").css("height", "290px");
}

function ShowVideoMenuOptions() {
    $("#VideoMenuOptions").show("slide", {}, 500);
    SetUploadOptions();
    DisplayVideoOptions('searchVideos');
    SetActiveVideosTab('searchVideos');

    /*if (PERMISSIONS.search_video == true || PERMISSIONS.search_video == "true") {
    DisplayVideoOptions('searchVideos');
    }
    else if (PERMISSIONS.upload_video == true || PERMISSIONS.upload_video == "true") {
    DisplayVideoOptions('clLocalVideos')
    }
    else {
    DisplayVideoOptions('clVideos');
    }*/

    LoadPmlContent(VideosLoaded, 'videos', 'clVideosContent');

    if ($("#search_results").html() == "") {
        $("#search_results").css("height", "228px");
        $("#VideoLibraryContent").css("width", "530px");
        $("#VideoLibraryContent").css("height", "450px");
        $("#clVideosContent").css("height", "290px");
    }
}

function HideImageMenuOptions() {
    if ($("#ImageMenuOptions").css("display") != "none") {
        if (videoOptionalImageInterval != null) {
            if (confirm("Are you sure, you do not want to attach optional image to video?")) {
                clearInterval(videoOptionalImageInterval);
                videoOptionalImageInterval = null;
                $("#OptionalVideoText1").hide();
                $("#OptionalVideoText2").hide();
                $("#OptionalVideoText3").css("visibility", "hidden");

                $("#ImageLibraryContent").resizable("destroy");
                $("#ImageMenuOptions").hide();
                $("#ImageLibraryContent").hide();

                currentOpenImageDialog = null;
                return true;
            }
            else {
                return false;
            }
        }
        else {
            $("#ImageLibraryContent").resizable("destroy");
            $("#ImageMenuOptions").hide();
            $("#ImageLibraryContent").hide();

            currentOpenImageDialog = null;
            return true;
        }
    }

}

function HideVideoMenuOptions() {
    if ($("#VideoMenuOptions").css("display") != "none") {
        $("#VideoLibraryContent").resizable("destroy");
        $("#VideoMenuOptions").hide();
        $("#VideoLibraryContent").hide();
        currentOpenVideoDialog = null;
    }
}

function HideFlashMenuOptions() {
    if ($("#FlashMenuOptions").css("display") != "none") {
        $("#FlashLibraryContent").resizable("destroy");
        $("#FlashMenuOptions").hide();
        $("#FlashLibraryContent").hide();
        currentOpenFlashDialog = null;
    }
}

function HideOfferBankMenuOptions() {
    if ($("#OfferBankMenuOptions").css("display") != "none") {
        $("#OfferBankLibraryContent").resizable("destroy");
        $("#OfferBankMenuOptions").hide();
        $("#OfferBankLibraryContent").hide();
        currentOpenOffersDialog = null;
    }
}

function PrepareImageMenuOptions() {
    return;
    var optionContainer = document.getElementById("ImageMenuOptions");

    /* top Links */
    var top_link = document.createElement("div");
    top_link.setAttribute("class", "top_link");
    var aClose = document.createElement("a");
    aClose.setAttribute("href", "#");
    aClose.setAttribute("onclick", "RestoreDefaultIcons();");
    aClose.setAttribute("target", "_self");

    var imgClose = document.createElement("span");
    imgClose.setAttribute("class", "inf_link");
    imgClose.setAttribute("style", "color: white; float: right; margin-top: -15px; margin-right: -15px; position: relative");
    imgClose.innerHTML = "x";
    imgClose.setAttribute("onclick", "HideImageMenuOptions();currentOpenDialog='Video'");

    /*var imgClose = document.createElement("img");
    imgClose.setAttribute("src", "static/images/close_icon.png");
    imgClose.setAttribute("width", "11px");
    imgClose.setAttribute("height", "11px");
    imgClose.setAttribute("border", "0");
    imgClose.setAttribute("align", "right");
    imgClose.setAttribute("style", "opacity: 0.7;");

    // TODO- Opacity needs support for IE compatibility 
    imgClose.setAttribute("onmouseover", "this.style.opacity=1;");
    imgClose.setAttribute("onmouseout", "this.style.opacity=0.7;");
    imgClose.setAttribute("onclick", "HideImageMenuOptions();currentOpenDialog='Video'");*/
    aClose.appendChild(imgClose);
    top_link.appendChild(aClose);

    /* Main Links */
    var mainLinks = document.createElement("div");
    mainLinks.setAttribute("class", "main_links");

    var unorderedList = document.createElement("ul");
    var web = document.createElement("li");
    var local = document.createElement("li");
    var pml = document.createElement("li");
    var webLink = document.createElement("a");
    var localLink = document.createElement("a");
    var pmlLink = document.createElement("a");
    pml.setAttribute("style", "background: none");

    webLink.setAttribute("href", "#");
    webLink.setAttribute("target", "_self");
    webLink.setAttribute("class", "current");
    /* webLink.setAttribute("class", "icon_o_row6");
    webLink.setAttribute("html", "Web"); */
    webLink.innerHTML = "Web";

    localLink.setAttribute("href", "#");
    localLink.setAttribute("target", "_self");
    localLink.setAttribute("class", "local_icon");
    /*localLink.setAttribute("class", "icon_o_row7");
    localLink.setAttribute("html", "Local");*/
    localLink.innerHTML = "Local";

    pmlLink.setAttribute("href", "#");
    pmlLink.setAttribute("target", "_self");
    pmlLink.setAttribute("class", "pml_icon");
    /*pmlLink.setAttribute("class", "icon_o_row8");
    pmlLink.setAttribute("html", "PML Lib");*/
    pmlLink.innerHTML = "PML Lib";

    /*web.setAttribute("style", "float: left");
    local.setAttribute("style", "float: left");
    pml.setAttribute("style", "float: left");*/

    web.appendChild(webLink);
    local.appendChild(localLink);

    /* Permission based features */
    unorderedList.appendChild(web);
    unorderedList.appendChild(local);
    /*
    if (PERMISSIONS.search_image == true || PERMISSIONS.search_image == "true") {
    unorderedList.appendChild(web);
    }
    else {
    $("#searchImages").remove();
    }

    if (PERMISSIONS.upload_image == true || PERMISSIONS.upload_image == "true") {
    unorderedList.appendChild(local);
    }
    */
    pml.appendChild(pmlLink);
    unorderedList.appendChild(pml);

    web.setAttribute("onmouseover", "ActiveContentTab(this);");
    web.setAttribute("onmouseout", "DeactivateContentTabs();");
    local.setAttribute("onmouseover", "ActiveContentTab(this);");
    local.setAttribute("onmouseout", "DeactivateContentTabs();");
    pml.setAttribute("onmouseover", "ActiveContentTab(this);");
    pml.setAttribute("onmouseout", "DeactivateContentTabs();");

    web.setAttribute("onclick", "ActiveContentTab(this);DisplayImageOptions('searchImages')");
    local.setAttribute("onclick", "ActiveContentTab(this);DisplayImageOptions('clLocalImages');");
    pml.setAttribute("onclick", "ActiveContentTab(this);DisplayImageOptions('clImages');LoadPmlContent(ImagesLoaded, 'images', 'clImages');");

    mainLinks.appendChild(unorderedList);
    optionContainer.appendChild(top_link);
    optionContainer.appendChild(mainLinks);
    //$(web).click();
    //return optionContainer;
}

function PrepareOfferBankMenuOptions() {
    return;
    var optionContainer = document.getElementById("OfferBankMenuOptions");

    /* top Links */
    var top_link = document.createElement("div");
    top_link.setAttribute("class", "top_link");
    var aClose = document.createElement("a");
    aClose.setAttribute("href", "#");
    aClose.setAttribute("onclick", "RestoreDefaultIcons();");
    aClose.setAttribute("target", "_self");

    var imgClose = document.createElement("img");
    imgClose.setAttribute("src", "static/images/close_icon.png");
    imgClose.setAttribute("width", "11px");
    imgClose.setAttribute("height", "11px");
    imgClose.setAttribute("border", "0");
    imgClose.setAttribute("align", "right");
    imgClose.setAttribute("style", "opacity: 0.7;");

    // TODO- Opacity needs support for IE compatibility 
    imgClose.setAttribute("onmouseover", "this.style.opacity=1;");
    imgClose.setAttribute("onmouseout", "this.style.opacity=0.7;");
    imgClose.setAttribute("onclick", "HideOfferBankMenuOptions();currentOpenDialog='Video'");
    aClose.appendChild(imgClose);
    top_link.appendChild(aClose);

    /* Main Links */
    var mainLinks = document.createElement("div");
    mainLinks.setAttribute("class", "main_links");

    var unorderedList = document.createElement("ul");
    var web = document.createElement("li");
    var local = document.createElement("li");
    var pml = document.createElement("li");
    var webLink = document.createElement("a");
    var localLink = document.createElement("a");
    var pmlLink = document.createElement("a");
    pml.setAttribute("style", "background: none");

    webLink.setAttribute("href", "#");
    webLink.setAttribute("target", "_self");
    webLink.setAttribute("class", "current");
    /*webLink.setAttribute("class", "icon_gd_row6");
    webLink.setAttribute("html", "Offer Web");*/
    webLink.innerHTML = "Web";

    localLink.setAttribute("href", "#");
    localLink.setAttribute("target", "_self");
    localLink.setAttribute("class", "local_icon");
    /*localLink.setAttribute("class", "icon_gd_row7");
    localLink.setAttribute("html", "Offer Opt-in");*/
    localLink.innerHTML = "Opt-in";

    pmlLink.setAttribute("href", "#");
    pmlLink.setAttribute("target", "_self");
    pmlLink.setAttribute("class", "pml_icon");
    /*pmlLink.setAttribute("class", "icon_gd_row6");
    pmlLink.setAttribute("html", "Offers");*/
    pmlLink.innerHTML = "Offers";

    /*web.setAttribute("style", "float: left");
    local.setAttribute("style", "float: left");
    pml.setAttribute("style", "float: left");*/

    web.appendChild(webLink);
    //unorderedList.appendChild(web);

    pml.appendChild(pmlLink);
    unorderedList.appendChild(pml);

    local.appendChild(localLink);

    /*Permission based access*/
    //if (PERMISSIONS.make_optin == true|| PERMISSIONS.make_optin == "true") {
    unorderedList.appendChild(local);
    //}


    web.setAttribute("onmouseover", "ActiveContentTab(this);");
    web.setAttribute("onmouseout", "DeactivateContentTabs();");
    local.setAttribute("onmouseover", "ActiveContentTab(this);");
    local.setAttribute("onmouseout", "DeactivateContentTabs();");
    pml.setAttribute("onmouseover", "ActiveContentTab(this);");
    pml.setAttribute("onmouseout", "DeactivateContentTabs();");


    web.setAttribute("onclick", "ActiveContentTab(this);DisplayOfferBankOptions('searchOffers')");
    local.setAttribute("onclick", "ActiveContentTab(this);DisplayOfferBankOptions('clLocalOffers')");
    pml.setAttribute("onclick", "ActiveContentTab(this);DisplayOfferBankOptions('clOffers');");


    mainLinks.appendChild(unorderedList);
    optionContainer.appendChild(top_link);
    optionContainer.appendChild(mainLinks);

}

function DeactivateContentTabs() {
    $("#ImageMenuOptions").find("a").each(function () {
        if (activeDialog != $(this).html()) {
            $(this).parent()._removeClass("ActiveContentTab");
        }
    });

    $("#ImageMenuOptions").find("a").each(function () {
        //$(this)._removeClass("current");
        if (activeDialog != $(this).html()) {
            if ($(this).html() == "Web") {
                $(this).css("background-position", "10px -2px");
            }
            else if ($(this).html() == "Local") {
                $(this).css("background-position", "10px -56px");
            }
            else if ($(this).html() == "PML Lib") {
                $(this).css("background-position", "10px -109px");
            }
        }
    });

    $("#VideoMenuOptions").find("a").each(function () {
        //$(this)._removeClass("current");

        if (activeDialog != $(this).html()) {
            if ($(this).html() == "Web") {
                $(this).css("background-position", "10px -2px");
            }
            else if ($(this).html() == "Local") {
                $(this).css("background-position", "10px -56px");
            }
            else if ($(this).html() == "PML Lib") {
                $(this).css("background-position", "10px -109px");
            }
        }
    });

    $("#VideoMenuOptions").find("a").each(function () {
        if (activeDialog != $(this).html()) {
            $(this).parent()._removeClass("ActiveContentTab");
        }
    });

    /*$("#FlashMenuOptions").find("a").each(function () {
    if (activeDialog != $(this).html()) {
    if ($(this).html() == "Web") {
    $(this).css("background-position", "10px -2px");
    }
    else if ($(this).html() == "Local") {
    $(this).css("background-position", "10px -56px");
    }
    else if ($(this).html() == "PML Lib") {
    $(this).css("background-position", "10px -109px");
    }
    }
    });

    $("#FlashMenuOptions").find("a").each(function () {
    if (activeDialog != $(this).html()) {
    $(this).parent()._removeClass("ActiveContentTab");
    }
    });*/

    $("#OfferBankMenuOptions").find("a").each(function () {
        if (activeDialog != $(this).html()) {

            if ($(this).html() == "Web") {
                $(this).css("background-position", "10px -2px");
            }
            else if ($(this).html() == "Opt-in") {
                $(this).css("background-position", "10px -56px");
            }
            else if ($(this).html() == "Offers") {
                $(this).css("background-position", "10px -109px");
            }
        }
    });

    /*$("#OfferBankMenuOptions").find("a").each(function () {
    if (activeDialog != $(this).html()) {
    $(this).parent()._removeClass("ActiveContentTab");
    }
    });*/
}

function ActiveContentTab(element) {
    DeactivateContentTabs();
    $(element)._addClass("ActiveContentTab");
    $(element).find("a").each(function () {
        if ($(this).html() == "Web") {
            $(this).css("background-position", "-48px -2px");
        }
        else if ($(this).html() == "Local" || $(this).html() == "Opt-in") {
            $(this).css("background-position", "-48px -56px");
        }
        else if ($(this).html() == "PML Lib" || $(this).html() == "Offers") {
            $(this).css("background-position", "-48px -109px");
        }
    });
}

function SetActiveVideosTab(tabId) {
    $("#clVideosContent").hide();
    $("#searchVideos").hide();
    $("#clLocalVideos").hide();
    $("#PmlImageDetails").hide();
    $("#VideoMenuOptions").find("li").each(function () {
        $(this).removeAttr("class");
    });

    $("#VideoMenuOptions").find("li").each(function () {
        var list = $(this);
        $(this).removeAttr("class");

        $(this).find("a").each(function () {
            var href = $(this).attr("href").replace("#", "");
            if (href == tabId) {
                $(list).attr("class", "active");

                $("#" + tabId).show();
                if (tabId == "clVideos") {
                    var scroll = $('#clVideosContent').data('jsp');
                    if (scroll != undefined) {
                        scroll.destroy();
                        $("#clVideosContent").jScrollPane({ hideFocus: true });
                    }
                }
            }
        });
    });
    $("#clVideosContent").show();
    DisplayVideoOptions(tabId);
}

function SetActiveImagesTab(tabId) {
    $("#clImages").hide();
    $("#searchImages").hide();
    $("#clLocalImages").hide();
    $("#PmlImageDetails").hide();
    $("#ImageMenuOptions").find("li").each(function () {
        $(this).removeAttr("class");
    });

    $("#ImageMenuOptions").find("li").each(function () {
        var list = $(this);
        $(this).removeAttr("class");

        $(this).find("a").each(function () {
            var href = $(this).attr("href").replace("#", "");
            if (href == tabId) {
                $(list).attr("class", "active");

                $("#" + tabId).show();
                if (tabId == "clImages") {
                    var scroll = $('#clImagesContent').data('jsp');
                    if (scroll != undefined) {
                        scroll.destroy();
                        $("#clImagesContent").jScrollPane({ hideFocus: true });
                    }

                }


            }
        });
    });

    DisplayImageOptions(tabId);
}

function PrepareVideoMenuOptions() {
    return;
    var optionContainer = document.getElementById("VideoMenuOptions");

    var top_link = document.createElement("div");
    top_link.setAttribute("class", "top_link");
    var aClose = document.createElement("a");
    aClose.setAttribute("href", "#");
    aClose.setAttribute("onclick", "RestoreDefaultIcons();");
    aClose.setAttribute("target", "_self");


    var imgClose = document.createElement("span");
    imgClose.setAttribute("class", "inf_link");
    imgClose.setAttribute("style", "color: white; float: right; margin-top: -15px; margin-right: -15px; position: relative");
    imgClose.innerHTML = "x";
    imgClose.setAttribute("onclick", "HideVideoMenuOptions();currentOpenDialog='Image'");

    /*var imgClose = document.createElement("img");
    imgClose.setAttribute("src", "static/images/close_icon.png");
    imgClose.setAttribute("width", "11px");
    imgClose.setAttribute("height", "11px");
    imgClose.setAttribute("border", "0");
    imgClose.setAttribute("align", "right");
    imgClose.setAttribute("style", "opacity: 0.7;");

    // TODO- Opacity needs support for IE compatibility 
    imgClose.setAttribute("onmouseover", "this.style.opacity=1;");
    imgClose.setAttribute("onmouseout", "this.style.opacity=0.7;");
    imgClose.setAttribute("onclick", "HideVideoMenuOptions();currentOpenDialog='Image'");*/
    aClose.appendChild(imgClose);
    top_link.appendChild(aClose);

    /* Main Links */
    var mainLinks = document.createElement("div");
    mainLinks.setAttribute("class", "main_links");

    var unorderedList = document.createElement("ul");
    var web = document.createElement("li");
    var local = document.createElement("li");
    var pml = document.createElement("li");
    var webLink = document.createElement("a");
    var localLink = document.createElement("a");
    var pmlLink = document.createElement("a");
    pml.setAttribute("style", "background: none");

    webLink.setAttribute("href", "#");
    webLink.setAttribute("target", "_self");
    webLink.setAttribute("class", "current");
    /*webLink.setAttribute("class", "icon_b_row6");
    webLink.setAttribute("html", "Video Web");*/
    webLink.innerHTML = "Web";

    localLink.setAttribute("href", "#");
    localLink.setAttribute("target", "_self");
    localLink.setAttribute("class", "local_icon");
    /*localLink.setAttribute("class", "icon_b_row7");
    localLink.setAttribute("html", "Video Local");*/
    localLink.innerHTML = "Local";

    pmlLink.setAttribute("href", "#");
    pmlLink.setAttribute("target", "_self");
    pmlLink.setAttribute("class", "pml_icon");
    /*pmlLink.setAttribute("class", "icon_b_row8");
    pmlLink.setAttribute("html", "Video PML Lib");*/
    pmlLink.innerHTML = "PML Lib";

    web.appendChild(webLink);
    local.appendChild(localLink);
    unorderedList.appendChild(web);
    unorderedList.appendChild(local);

    /* Permission based access */
    /*if (PERMISSIONS.search_video == true || PERMISSIONS.search_video == "true") {
    unorderedList.appendChild(web);
    }
    if (PERMISSIONS.upload_video == true || PERMISSIONS.upload_video == "true") {
    unorderedList.appendChild(local);
    }*/
    /*
    web.setAttribute("style", "float: left");
    local.setAttribute("style", "float: left");
    pml.setAttribute("style", "float: left");*/

    pml.appendChild(pmlLink);
    unorderedList.appendChild(pml);

    web.setAttribute("onmouseover", "ActiveContentTab(this);");
    web.setAttribute("onmouseout", "DeactivateContentTabs();");
    local.setAttribute("onmouseover", "ActiveContentTab(this);");
    local.setAttribute("onmouseout", "DeactivateContentTabs();");
    pml.setAttribute("onmouseover", "ActiveContentTab(this);");
    pml.setAttribute("onmouseout", "DeactivateContentTabs();");

    web.setAttribute("onclick", "ActiveContentTab(this);DisplayVideoOptions('searchVideos')");
    local.setAttribute("onclick", "ActiveContentTab(this);DisplayVideoOptions('clLocalVideos');");
    pml.setAttribute("onclick", "ActiveContentTab(this);DisplayVideoOptions('clVideos');");

    mainLinks.appendChild(unorderedList);

    optionContainer.appendChild(top_link);
    optionContainer.appendChild(mainLinks);

    //return optionContainer;
}

function ShowPmlAlertMessage(title, message, okCallback, cancelCallback) {
    var alertContainer = $("<div/>");
    var closeButton = $("<button/>");
    var alertTitle = $("<h4/>");
    var alertMessage = $("<p/>");
    var okButton = $("<a/>");
    var cancelButton = $("<a/>");
    var clearDiv = $("<div/>");

    $(closeButton).attr("class", "close");
    $(closeButton).attr("data-dismiss", "alert");
    $(closeButton).attr("type", "button");
    $(closeButton).html("x");
    $(alertContainer).attr("id", "PmlAlert");
    $(alertContainer).attr("class", "alert alert-block alert-error fade in");
    $(title).attr("class", "alert-titleing");

    $(okButton).click(function () {
        if (okCallback != undefined) {
            $("#PmlAlert").remove();
            okCallback();
        }
    });

    $(cancelButton).click(function () {
        if (cancelCallback != undefined) {
            cancelCallback();
        }
    });

    $(alertContainer).append($(closeButton));
    $(alertContainer).append($(alertTitle));
    $(alertContainer).append($(alertMessage));
    $(alertContainer).append($(okButton));
    $(alertContainer).append($(cancelButton));
    $(alertContainer).append($(clearDiv));

    $(alertContainer).show();
    //$(document).append(alertContainer);

}

function DisplayImageOptions(id) {
    if (id == currentOpenImageDialog) {
        return;
    }

    var flag = false;
    if ($("#clImages").css("display") != "none") {
        $("#clImages").hide("slide", { direction: "right" }, 100, function () { if (!flag) { flag = true; $("#" + id).show("slide", { direction: "left" }, 100); } });
    }

    if ($("#clLocalImages").css("display") != "none") {
        $("#clLocalImages").hide("slide", { direction: "right" }, 100, function () { if (!flag) { flag = true; $("#" + id).show("slide", { direction: "left" }, 100); } });
    }

    if ($("#searchImages").css("display") != "none") {
        $("#searchImages").hide("slide", { direction: "right" }, 100, function () { if (!flag) { $("#" + id).show("slide", { direction: "left" }, 100); } });
    }

    /*$("#ImageLibraryContent").resizable({
    minWidth: "380",
    minHeight: "340",
    resize: function (e) {
    var height = parseInt($(this).css("height").replace("px", ""));
    var width = parseInt($(this).css("width").replace("px", "")) - 10;

    $("#clImagesContent").css("height", height - 20);
    $('#clImagesContent ul').parent().css("width", parseInt($("#ImageLibraryContent").css("width").replace("px", "")) + "px");
    $('#clImagesContent ul').css("width", parseInt($("#ImageLibraryContent").css("width").replace("px", "")) + "px");

    $("#search_results2").css("height", height - 100);
    $("#search_results2").css("width", width);
    $("#imageUrl").css("width", width - 100);
    $("#txtImageSearchKeywords").css("width", width - 100);
    $("#image_search_engine").find("ul.clear_ul").css("left", width - 155);

    var scroll = $('#search_results2').data('jsp');
    scroll.reinitialise();
    },
    stop: function (e) {
    var height = parseInt($(this).css("height").replace("px", ""));
    var width = parseInt($(this).css("width").replace("px", "")) - 10;

    $("#clImagesContent").css("height", height - 20);
    $('#clImagesContent ul').parent().css("width", parseInt($("#ImageLibraryContent").css("width").replace("px", "")) + "px");
    $('#clImagesContent ul').css("width", parseInt($("#ImageLibraryContent").css("width").replace("px", "")) + "px");
    $('#clImagesContent ul').parent().css("height", parseInt($("#ImageLibraryContent").css("height").replace("px", "")) - 60 + "px");
    $('#clImagesContent ul').css("height", parseInt($("#ImageLibraryContent").css("height").replace("px", "")) - 60 + "px");

    $('#clImagesContent').jScrollPane();
    /*$('#clImagesContent ul').slimScroll({
    height: parseInt($("#clImages").css("height").replace("px", "")) - 50 + "px",
    width: parseInt($("#clImages").css("width").replace("px", "")) + "px",
    color: GetCurrentTheme(),
    size: '5px'
    });
    $("#imageUrl").css("width", width - 100);
    $("#txtImageSearchKeywords").css("width", width - 100);
    $("#search_results2").css("width", width);
    $("#image_search_engine").find("ul.clear_ul").css("left", width - 155);
    $("#search_results2").css("height", height - 100);
    var scroll = $('#search_results2').data('jsp');
    scroll.reinitialise();
    }
    });*/

    $("#ImageLibraryContent").show();

    $("#imageUrl").val("Add image url...");
    var element = document.getElementById("clLocalImages");
    $(element).children("img").each(function () { $(this).remove(); });
    $(element).children("span").each(function () { $(this).remove(); });

    if ($("#txtImageSearchKeywords").val() == "" || $("#txtImageSearchKeywords").val() == "Enter keywords to search...") {
        $("#txtImageSearchKeywords").val("Enter keywords to search...");
        $("#search_results2").html("");
        $("#ImageUrlContainer").fadeIn(1000);
    }

    switch (id) {
        case "clImages":
            activeDialog = "PML Lib";
            break;
        case "clLocalImages":
            activeDialog = "Local";
            break;
        case "searchImages":
            activeDialog = "Web";
            break;
    }
    DeactivateContentTabs();
    currentOpenImageDialog = id;
}

function GetCurrentTheme(cssClass) {
    var scrollColor = THEME.Gradient;

    if (cssClass != "undefined" && cssClass != undefined) {
        switch (cssClass) {
            case "RedGradientColor":
                scrollColor = THEME.RedGradientColor;
                break;
            case "GreenGradientColor":
                scrollColor = THEME.GreenGradientColor;
                break;
            case "YellowGradientColor":
                scrollColor = THEME.YellowGradientColor;
                break;
            case "BlueGradientColor":
                scrollColor = THEME.BlueGradientColor;
                break;

        }
    }

    return scrollColor;
}

function DisplayVideoOptions(id) {
    if (id == currentOpenVideoDialog) {
        return;
    }

    if ($("#clVideos").css("display") != "none") {
        $("#clVideos").hide("slide", { direction: "right" }, 100, function () { $("#" + id).show("slide", { direction: "left" }, 100); });
    }

    if ($("#clLocalVideos").css("display") != "none") {
        $("#clLocalVideos").hide("slide", { direction: "right" }, 100, function () { $("#" + id).show("slide", { direction: "left" }, 100); });
    }

    if ($("#searchVideos").css("display") != "none") {
        $("#searchVideos").hide("slide", { direction: "right" }, 100, function () { $("#" + id).show("slide", { direction: "left" }, 100); });
    }

    /*$("#VideoLibraryContent").resizable({
    minWidth: "350",
    minHeight: "300",

    resize: function (e) {
    var height = parseInt($(this).css("height").replace("px", ""));

    $("#search_results").css("height", height - 80);
    //$("#clVideos").css("height", height - 20);
    $('#clVideos ul').parent().css("width", parseInt($("#VideoLibraryContent").css("width").replace("px", "")) + "px");
    $('#clVideos ul').css("width", parseInt($("#VideoLibraryContent").css("width").replace("px", "")) + "px");

    },
    stop: function (e) {
    var height = parseInt($(this).css("height").replace("px", ""));
    $("#search_results").css("height", height - 80);
    //$("#clVideos").css("height", height - 20);
    $('#clVideos ul').parent().css("width", parseInt($("#VideoLibraryContent").css("width").replace("px", "")) + "px");
    $('#clVideos ul').css("width", parseInt($("#VideoLibraryContent").css("width").replace("px", "")) + "px");

    $('#clVideos ul').parent().css("height", parseInt($("#VideoLibraryContent").css("height").replace("px", "")) - 30 + "px");
    $('#clVideos ul').css("height", parseInt($("#VideoLibraryContent").css("height").replace("px", "")) - 30 + "px");

    $('#clVideos ul').slimScroll({
    height: parseInt($("#clVideos").css("height").replace("px", "")) - 10 + "px",
    width: parseInt($("#clVideos").css("width").replace("px", "")) + "px",
    color: GetCurrentTheme(),
    size: '5px'
    });
    }
    });*/
    $("#VideoLibraryContent").show();

    $("#youtubeUrl").val("Enter video url...");

    if ($("#searchkey").val() == "" || $("#searchkey").val() == "Enter text to search YouTube video...") {
        $("#searchkey").val("Enter text to search YouTube video...");
        $("#search_results").html("");
        $("#VideoUrlContainer").fadeIn(1000);
    }

    var element = document.getElementById("clLocalVideos");
    $(element).children("img").each(function () { $(this).remove(); });
    $(element).children("span").each(function () { $(this).remove(); });
    switch (id) {
        case "clVideos":
            activeDialog = "PML Lib";
            break;
        case "clLocalVideos":
            activeDialog = "Local";
            break;
        case "searchVideos":
            activeDialog = "Web";
            break;
    }
    DeactivateContentTabs();
    currentOpenVideoDialog = id;
}

function DisplayFlashOptions(id) {
    if (id == currentOpenFlashDialog) {
        return;
    }

    var flag = false;
    if ($("#clFlash").css("display") != "none") {
        $("#clFlash").hide("slide", { direction: "right" }, 100, function () { if (!flag) { flag = true; $("#" + id).show("slide", { direction: "left" }, 100); } });
    }

    if ($("#clLocalFlash").css("display") != "none") {
        $("#clLocalFlash").hide("slide", { direction: "right" }, 100, function () { if (!flag) { flag = true; $("#" + id).show("slide", { direction: "left" }, 100); } });
    }

    if ($("#searchFlash").css("display") != "none") {
        $("#searchFlash").hide("slide", { direction: "right" }, 100, function () { if (!flag) { $("#" + id).show("slide", { direction: "left" }, 100); } });
    }
    $("#FlashLibraryContent").resizable({
        minWidth: "350",
        minHeight: "300",
        resize: function (e) {
            var height = parseInt($(this).css("height").replace("px", ""));
            $("#search_results3").css("height", height - 130);
            $("#clFlash").css("height", height - 20);
        },
        stop: function (e) {
            var height = parseInt($(this).css("height").replace("px", ""));
            $("#search_results3").css("height", height - 130);
            $("#clFlash").css("height", height - 20);
        }
    });

    $("#FlashLibraryContent").show();


    var element = document.getElementById("clLocalFlash");
    $(element).children("img").each(function () { $(this).remove(); });
    $(element).children("span").each(function () { $(this).remove(); });

    switch (id) {
        case "clFlash":
            activeDialog = "PML Lib";
            break;
        case "clLocalFlash":
            activeDialog = "Local";
            break;
        case "searchFlash":
            activeDialog = "Web";
            break;
    }
    DeactivateContentTabs();
    currentOpenFlashDialog = id;
}

function DisplayOfferBankOptions(id) {
    if (id == currentOpenOffersDialog) {
        return;
    }

    var flag = false;
    if ($("#clOffers").css("display") != "none") {
        $("#clOffers").hide("slide", { direction: "right" }, 100, function () { if (!flag) { flag = true; $("#" + id).show("slide", { direction: "left" }, 100); } });
    }


    if ($("#clLocalOffers").css("display") != "none") {
        $("#clLocalOffers").hide("slide", { direction: "right" }, 100, function () { if (!flag) { flag = true; $("#" + id).show("slide", { direction: "left" }, 100); } });
    }

    if ($("#searchOffers").css("display") != "none") {
        $("#searchOffers").hide("slide", { direction: "right" }, 100, function () { if (!flag) { $("#" + id).show("slide", { direction: "left" }, 100); } });
    }
    $("#OfferBankLibraryContent").resizable({
        minWidth: "350",
        minHeight: "300",
        resize: function (e) {
            var height = parseInt($(this).css("height").replace("px", ""));
            $("#clLocalOffers").css("height", height - 20);
            $("#clOffers").css("height", height - 20);
        },
        stop: function (e) {
            var height = parseInt($(this).css("height").replace("px", ""));
            $("#clLocalOffers").css("height", height - 20);
            $("#clOffers").css("height", height - 20);
        }
    });

    $("#OfferBankLibraryContent").show();

    var element = document.getElementById("clLocalOffers");
    $(element).children("img").each(function () { $(this).remove(); });
    $(element).children("span").each(function () { $(this).remove(); });

    switch (id) {
        case "clOffers":
            activeDialog = "Offers";
            break;
        case "clLocalOffers":
            activeDialog = "Opt-in";
            break;
        case "searchOffers":
            activeDialog = "Web";
            break;
    }
    DeactivateContentTabs();
    currentOpenOffersDialog = id;
}

function PrepareImageOptions(container, type) {
    //if ($(container).find("#ImageOptionContainer").length > 0)
    //return;
    var userLinkText = "";
    var userCaptionText = "";

    var imageOptionContainer = $(container).find("#ImageOptionContainer").length > 0 ? $(container).find("#ImageOptionContainer")[0] : null;
    if (imageOptionContainer == null) {
        imageOptionContainer = document.createElement("div");
        imageOptionContainer.setAttribute("id", "ImageOptionContainer");
        imageOptionContainer.setAttribute("class", "ImageOptionContainer");
    }
    var captionContainer = $(container).find("#CaptionContainer").length > 0 ? $(container).find("#CaptionContainer")[0] : null;
    if (captionContainer == null) {
        captionContainer = document.createElement("div");
        captionContainer.setAttribute("id", "CaptionContainer");
    }
    var linkContainer = $(container).find("#LinkContainer").length > 0 ? $(container).find("#LinkContainer")[0] : null;
    if (linkContainer == null) {
        linkContainer = document.createElement("div");
        linkContainer.setAttribute("id", "LinkContainer");
    }

    if (imageChatEnabled) {
        var chatContainer = $(container).find("#ChatContainer").length > 0 ? $(container).find("#ChatContainer")[0] : null;
        if (chatContainer == null) {
            chatContainer = document.createElement("div");
            chatContainer.setAttribute("id", "ChatContainer");
        }
    }

    var autoCheckContainer = $(container).find("#AutoCheckContainer").length > 0 ? $(container).find("#AutoCheckContainer")[0] : null;
    if (autoCheckContainer == null) {
        autoCheckContainer = document.createElement("div");
        autoCheckContainer.setAttribute("id", "AutoCheckContainer");
    }
    var checkContainer = $(container).find("#CheckContainer").length > 0 ? $(container).find("#CheckContainer")[0] : null;
    if (checkContainer == null) {
        checkContainer = document.createElement("div");
        checkContainer.setAttribute("id", "CheckContainer");
    }
    var autoPlayOptionContainer = $(container).find("#AutoPlayOptionContainer").length > 0 ? $(container).find("#AutoPlayOptionContainer")[0] : null;
    if (autoPlayOptionContainer == null) {
        autoPlayOptionContainer = document.createElement("div");
        autoPlayOptionContainer.setAttribute("id", "AutoPlayOptionContainer");
    }

    var caption = $(container).find("#ImageCaption").length > 0 ? $(container).find("#ImageCaption")[0] : null;
    if (caption == null) {
        caption = document.createElement("div");
        caption.setAttribute("id", "ImageCaption");
    }
    var link = $(container).find("#ImageLink").length > 0 ? $(container).find("#ImageLink")[0] : null;
    if (link == null) {
        link = document.createElement("div");
        link.setAttribute("id", "ImageLink");
    }

    var autoPlay = $(container).find("#AutoplayCheck").length > 0 ? $(container).find("#AutoplayCheck")[0] : null;
    if (autoPlay == null) {
        autoPlay = document.createElement("div");
        autoPlay.setAttribute("id", "AutoplayCheck");
    }
    var mute = $(container).find("#MuteAudio").length > 0 ? $(container).find("#MuteAudio")[0] : null;
    if (mute == null) {
        mute = document.createElement("div");
        mute.setAttribute("id", "MuteAudioCheck");
    }
    var autoPlayText = $(container).find("#AutoPlayLabel").length > 0 ? $(container).find("#AutoPlayLabel")[0] : null;
    if (autoPlayText == null) {
        autoPlayText = document.createElement('label');
        autoPlayText.setAttribute("id", "AutoPlayLabel");
    }

    var muteText = $(container).find("#MuteLabel").length > 0 ? $(container).find("#MuteLabel")[0] : null;
    if (muteText == null) {
        muteText = document.createElement('label');
        muteText.setAttribute("id", "MuteLabel");
    }
    var autoPlayOptions = $(container).find("#AutoPlayImageOptions").length > 0 ? $(container).find("#AutoPlayImageOptions")[0] : null;
    if (autoPlayOptions == null) {
        autoPlayOptions = document.createElement("div");
        autoPlayOptions.setAttribute("id", "AutoPlayImageOptions");
    }
    var originalSize = $(container).find("#ImageOriginalSize").length > 0 ? $(container).find("#ImageOriginalSize")[0] : null;
    if (originalSize == null) {
        originalSize = document.createElement("div");
        originalSize.setAttribute("id", "ImageOriginalSize");
    }
    var makeBackground = $(container).find("#ImageBackground").length > 0 ? $(container).find("#ImageBackground")[0] : null;
    if (makeBackground == null) {
        makeBackground = document.createElement("div");
        makeBackground.setAttribute("id", "ImageBackground");
    }

    var attachOptionImage = $(container).find("#AttachOptionalImage").length > 0 ? $(container).find("#AttachOptionalImage")[0] : null;
    if (attachOptionImage == null) {
        attachOptionImage = document.createElement("div");
        attachOptionImage.setAttribute("id", "AttachOptionalImage");
    }
    var fitWindow = $(container).find("#FitWindow").length > 0 ? $(container).find("#FitWindow")[0] : null;
    if (fitWindow == null) {
        fitWindow = document.createElement("div");
        fitWindow.setAttribute("id", "ImageFitWindow");
    }
    var makeItem = $(container).find("#ImageItem").length > 0 ? $(container).find("#ImageItem")[0] : null;
    if (makeItem == null) {
        makeItem = document.createElement("div");
        makeItem.setAttribute("id", "ImageItem");
    }
    /*var optInForm = $(container).find("#ImageOptInForm").length > 0 ? $(container).find("#ImageOptInForm")[0] : null;
    if (optInForm == null) {
    optInForm = document.createElement("div");
    optInForm.setAttribute("id", "ImageOptInForm");
    }*/
    var remove = $(container).find("#ImageRemove").length > 0 ? $(container).find("#ImageRemove")[0] : null;
    if (remove == null) {
        remove = document.createElement("div");
        remove.setAttribute("id", "ImageRemove");
    }

    var captionText = $(container).find("#txtImageCaption").length > 0 ? $(container).find("#txtImageCaption")[0] : null;
    if (captionText == null) {
        captionText = document.createElement("input");
        captionText.setAttribute("id", "txtImageCaption");
    }

    var linkText = $(container).find("#txtImageLink").length > 0 ? $(container).find("#txtImageLink")[0] : null;
    if (linkText == null) {
        linkText = document.createElement("input");
        linkText.setAttribute("id", "txtImageLink");
    }

    var autoRedir = $(container).find("#chkAutoRedirect").length > 0 ? $(container).find("#chkAutoRedirect")[0] : null;
    if (autoRedir == null) {
        autoRedir = document.createElement("input");
        autoRedir.setAttribute("type", "checkbox");
        autoRedir.setAttribute("id", "chkAutoRedirect");
        autoRedir.setAttribute("title", "Auto redirect at the end of video");
        autoRedir.setAttribute("class", "ShowText");
        autoRedir.setAttribute("style", "top:45px;left:130px");
        //<input type="checkbox" class="ShowText" style="display: inline-block;top:45px;left:130px" title="Auto Redirect at the end of Video">
    }

    var autoPlayCheckBox = $(container).find("#chkAutoPlay").length > 0 ? $(container).find("#chkAutoPlay")[0] : null;
    if (autoPlayCheckBox == null) {
        autoPlayCheckBox = document.createElement("input");
        autoPlayCheckBox.setAttribute("id", "chkAutoPlay");
    }

    var muteCheckBox = $(container).find("#chkMute").length > 0 ? $(container).find("#chkMute")[0] : null;
    if (muteCheckBox == null) {
        muteCheckBox = document.createElement("input");
        muteCheckBox.setAttribute("id", "chkMute");
    }
    if (imageChatEnabled) {
        var chat = $(container).find("#Chat").length > 0 ? $(container).find("#Chat")[0] : null;
        if (chat == null) {
            chat = document.createElement("div");
            chat.setAttribute("id", "Chat");
        }
        var chatUserId = $(container).find("#ChatUserId").length > 0 ? $(container).find("#ChatUserId")[0] : null;
        if (chatUserId == null) {
            chatUserId = document.createElement("input");
            chatUserId.setAttribute("id", "ChatUserId");
        }
    }

    //var moreOptions = document.createElement("div");



    /*
    moreOptions.setAttribute("id", "MoreOptions");
    moreOptions.setAttribute("class", "MoreOptions");
    moreOptions.innerHTML = "<span style='float:left; padding: 2px; margin-right: 10px;cursor:pointer'>Original aspect ratio</span><span style='float:left; padding: 2px; margin-right: 10px;cursor:pointer'>Delete " + type + "</span>";
    */

    if (type == "video") {
        remove.setAttribute("title", "Remove this video");
        makeItem.setAttribute("title", "Revert video to an item");
        makeBackground.setAttribute("title", "Make this video as background video");

        if (captionText.value == null || captionText.value.length == 0 || $(container).attr("caption") == undefined) {
            if ($(container).attr("caption") == undefined) {
                userCaptionText = "Enter caption...";
            }
            else {
                userCaptionText = $(container).attr("caption");
            }
        }
        else {
            if (captionText.getAttribute("value") != null)
                userCaptionText = captionText.getAttribute("value");
        }

        if (linkText.value == null || linkText.value.length == 0 ||
            $(container).attr("link") == undefined || $(container).attr("link") == "undefined") {
            if ($(container).attr("link") == undefined) {
                userLinkText = "Enter link...";
            }
            else {
                userLinkText = $(container).attr("link");
            }
        }
        else {
            if (linkText.getAttribute("value") != null)
                userLinkText = linkText.getAttribute("value");
        }

        //moreOptions.setAttribute("onclick", "RemoveImage(this.parentNode.parentNode);");
    }
    else if (type == "flash") {
        remove.setAttribute("title", "Remove this flash animation");
    }
    else {
        remove.setAttribute("title", "Remove this image");
        makeItem.setAttribute("title", "Revert image to an item");
        makeBackground.setAttribute("title", "Make this image as background image");
        //moreOptions.setAttribute("onclick", "RemoveImage(this.parentNode.parentNode.parentNode);");
        $(container).find("img").each(function () {
            if (captionText.value == null || captionText.value.length == 0) {
                if ($(this).attr("caption") == undefined) {
                    userCaptionText = "Enter caption...";
                }
                else {
                    userCaptionText = $(this).attr("caption");
                }
            }
            else {
                if (captionText.getAttribute("value") != null)
                    userCaptionText = captionText.getAttribute("value");
            }

            if (linkText.value == null || linkText.value.length == 0) {
                if ($(this).attr("link") == undefined) {
                    userLinkText = "Enter link...";
                }
                else {
                    userLinkText = $(this).attr("link");
                }
            }
            else {
                if (linkText.getAttribute("value") != null)
                    userLinkText = linkText.getAttribute("value");
            }
        });

    }

    //caption.setAttribute("id", "ImageCaption");
    caption.setAttribute("class", "ImageOption");
    caption.setAttribute("title", "Caption");
    caption.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    caption.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    $(caption).mousedown(function (e) {
        $(this).parent().find('#txtImageCaption').fadeIn(500, function () {
            $(this).parent().find('#txtImageCaption').focus();
        });
    });
    caption.innerHTML = "<img src='static/images/Caption_hover.png' alt='C' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Caption_Click.png' alt='C' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";


    //captionText.setAttribute("id", "txtImageCaption");
    captionText.setAttribute("type", "text");
    captionText.setAttribute("class", "ImageOptionCaptionText ShowText");
    captionText.onkeypress = function (e) {
        CheckEnterKeyForOptions(event, '" + type + "');
    }
    captionText.onkeyup = function (e) {
        CheckKeyPressEvent(event, '" + type + "');
    }

    captionText.value = (userCaptionText == "undefined") ? "Enter caption..." : userCaptionText;
    linkText.value = userLinkText;

    captionText.onfocus = function (e) {
        SetTextValue(this, 'enter caption...');
    }
    captionText.onblur = function (e) {
        SetImageContentOptions('caption', this.value, this, type);
        ResetTextValue(this, 'Enter caption...');
    }


    linkText.setAttribute("class", "ImageOptionLinkText ShowText");
    linkText.onkeypress = function (e) {
        CheckEnterKeyForOptions(e, type);
    }
    //linkText.setAttribute("onkeypress", "CheckEnterKeyForOptions(event,'" + type + "');");
    linkText.onkeyup = function (e) {
        CheckKeyPressEvent(e, type);
    }
    //linkText.setAttribute("onkeyup", "CheckKeyPressEvent(event,'" + type + "');");
    linkText.onkeyup = function (e) {
        CheckKeyPressEvent(e, type);
    }
    //linkText.setAttribute("onfocus", "SetTextValue(this,'enter link...');");
    linkText.onfocus = function (e) {
        SetTextValue(this, 'enter link...');
    }
    //linkText.setAttribute("onblur", "SetImageContentOptions('link',this.value,this,'" + type + "');ResetTextValue(this,'Enter link...');");
    linkText.onblur = function (e) {
        SetImageContentOptions('link', this.value, this, type);
        ResetTextValue(this, 'Enter link...');
    }

    checkContainer.setAttribute("class", "AutoPlayLabel");

    //autoPlayCheckBox.setAttribute("id", "chkAutoPlay");
    autoPlayCheckBox.setAttribute("type", "checkbox");
    autoPlayCheckBox.setAttribute("text", "Auto-play");

    autoPlayCheckBox.setAttribute("onchange", "SetAutoPlayOption(this,$(this).prop('checked'));");

    muteCheckBox.setAttribute("type", "checkbox");
    muteCheckBox.setAttribute("text", "Mute");
    //muteCheckBox.setAttribute("onchange", "SetAutoPlayOption(this,$(this).prop('checked'));");

    //autoPlayCheckBox.setAttribute("class", "ImageOptionAutoPlay ShowText");

    //autoPlayText.setAttribute('class', 'AutoPlayLabel');    
    //autoPlayText.appendChild(document.createTextNode('Auto-play this video'));
    $(autoPlayText).html('Auto-play');
    $(muteText).html('Mute');
    $(autoPlayText).mousedown(function (event) {
        event.preventDefault();
        var checked = false;
        $(this).siblings("input").each(function () {
            $(this).click();
            checked = $(this).prop("checked");
        });
        SetAutoPlayOption($(this), checked);
    });

    autoPlay.setAttribute("title", "Autoplay this video");
    //autoPlay.setAttribute("id", "AutoplayCheck");
    autoPlay.setAttribute("class", "ImageOption");
    //autoPlay.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    autoPlay.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    //autoPlay.setAttribute("onmouseout", "$(this).find('.ContentOptionClick').css('display','none');$(this).find('.ContentOptionHover').css('display','block');");
    autoPlay.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    autoPlay.innerHTML = "<img src='static/images/Original_hover.png' alt='A' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Original_Click.png' alt='A' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";

    link.setAttribute("title", "Hyperlink");
    //link.setAttribute("onclick", "$(this).parent().find('#txtImageLink').fadeIn(500);");
    $(link).mousedown(function (e) {
        $(this).parent().find('#txtImageLink').fadeIn(500, function () {
            $(this).parent().find('#txtImageLink').focus();
        });
        $(this).parent().find('#chkAutoRedirect').fadeIn(500);
    });
    //link.setAttribute("id", "ImageLink");
    link.setAttribute("class", "ImageOption");
    //link.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    link.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }

    //link.setAttribute("onmouseout", "$(this).find('.ContentOptionClick').css('display','none');$(this).find('.ContentOptionHover').css('display','block');");
    link.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    link.innerHTML = "<img src='static/images/Hyperlink_hover.png' alt='L' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Hyperlink_Click.png' alt='L' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";

    //originalSize.setAttribute("id", "ImageOriginalSize");
    originalSize.setAttribute("title", "Original Size");
    //originalSize.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    originalSize.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    //originalSize.setAttribute("onmouseout", "$(this).find('.ContentOptionClick').css('display','none');$(this).find('.ContentOptionHover').css('display','block');");
    originalSize.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    originalSize.innerHTML = "<img src='static/images/Original_hover.png' alt='O' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Original_Click.png' alt='O' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    originalSize.setAttribute("class", "ImageOption");
    originalSize.onclick = function (e) {
        OriginalSize(this.parentNode.parentNode);
    }

    originalSize.setAttribute("style", "display: none");

    //optInForm.setAttribute("id", "ImageOptInForm");
    //optInForm.setAttribute("title", "Opt in form");
    //optInForm.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    /*optInForm.onmouseover = function (e) {
    $(this).find('.ContentOptionHover').css('display', 'none');
    $(this).find('.ContentOptionClick').css('display', 'block');
    }
    optInForm.onmouseout = function (e) {
    $(this).find('.ContentOptionClick').css('display', 'none');
    $(this).find('.ContentOptionHover').css('display', 'block');
    }
    optInForm.innerHTML = "<img src='static/images/Original_hover.png' alt='O' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Original_Click.png' alt='O' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    optInForm.setAttribute("class", "ImageOption");
    optInForm.onclick = function (e) {
    if ($(this).parent().parent().parent().find(".PmlFormContainer").length > 0) {
    return;
    }
    AddOptInForm(this);
    }
    optInForm.setAttribute("style", "display: block");
    */
    //makeItem.setAttribute("id", "ImageItem");

    //makeItem.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    makeItem.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    //makeItem.setAttribute("onmouseout", "$(this).find('.ContentOptionClick').css('display','none');$(this).find('.ContentOptionHover').css('display','block');");
    makeItem.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    makeItem.innerHTML = "<img src='static/images/Item_hover.png' alt='I' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Item_Click.png' alt='I' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    makeItem.setAttribute("class", "ImageOption");
    makeItem.setAttribute("style", "display: none");

    //fitWindow.setAttribute("id", "ImageFitWindow");
    fitWindow.setAttribute("title", "Fit to current window size");
    //fitWindow.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    fitWindow.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    fitWindow.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    fitWindow.innerHTML = "<img src='static/images/FitWindow_hover.png' alt='F' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/FitWindow_Click.png' alt='F' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    fitWindow.setAttribute("class", "ImageOption");
    fitWindow.setAttribute("style", "display: none");

    //makeBackground.setAttribute("id", "ImageBackground");

    //makeBackground.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
    makeBackground.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    makeBackground.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    makeBackground.innerHTML = "<img src='static/images/Background_hover.png' alt='B' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Background_Click.png' alt='B' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    makeBackground.setAttribute("class", "ImageOption");

    //attachOptionImage.setAttribute("id", "AttachOptionalImage");
    attachOptionImage.innerHTML = "<img src='static/images/Original_hover.png' alt='O' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Original_Click.png' alt='O' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    attachOptionImage.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    attachOptionImage.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }

    attachOptionImage.setAttribute("class", "ImageOption");
    attachOptionImage.setAttribute("title", "Attach some image to this video");
    attachOptionImage.onclick = function (e) {
        GetOptionalImage(this);
    }

    //autoPlayOptions.setAttribute("id", "AutoPlayImageOptions");
    autoPlayOptions.innerHTML = "<img src='static/images/Original_hover.png' alt='O' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Original_Click.png' alt='O' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    autoPlayOptions.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    autoPlayOptions.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    autoPlayOptions.setAttribute("class", "ImageOption");
    autoPlayOptions.setAttribute("title", "Autoplay video options...");
    autoPlayOptions.onclick = function (e) {
        $('.AutoPlayOptionDropdown').fadeIn(1000);
    }

    //remove.setAttribute("id", "ImageRemove");
    remove.onmouseover = function (e) {
        $(this).find('.ContentOptionHover').css('display', 'none');
        $(this).find('.ContentOptionClick').css('display', 'block');
    }
    remove.onmouseout = function (e) {
        $(this).find('.ContentOptionClick').css('display', 'none');
        $(this).find('.ContentOptionHover').css('display', 'block');
    }
    remove.innerHTML = "<img src='static/images/Remove_hover.png' alt='B' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/Remove_Click.png' alt='B' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";
    remove.setAttribute("class", "ImageOption");


    if (imageChatEnabled) {

        chat.setAttribute("title", "Chat");
        chat.onclick = function (e) {
            $(this).parent().find('#ChatUserId').fadeIn(500, function () {
                $(this).parent().find('#ChatUserId').focus();
            });
        }
        chat.setAttribute("class", "ImageOption");
        //link.setAttribute("onmouseover", "$(this).find('.ContentOptionHover').css('display','none');$(this).find('.ContentOptionClick').css('display','block');");
        chat.onmouseover = function (e) {
            $(this).find('.ContentOptionHover').css('display', 'none');
            $(this).find('.ContentOptionClick').css('display', 'block');
        }

        //link.setAttribute("onmouseout", "$(this).find('.ContentOptionClick').css('display','none');$(this).find('.ContentOptionHover').css('display','block');");
        chat.onmouseout = function (e) {
            $(this).find('.ContentOptionClick').css('display', 'none');
            $(this).find('.ContentOptionHover').css('display', 'block');
        }
        chat.innerHTML = "<img src='static/images/chat.png' alt='L' width='16px' height='16px' class='ContentOptionHover' style='display: block' /><img src='static/images/chat_click.png' alt='L' width='16px' height='16px' class='ContentOptionClick' style='display: none' />";

        chatUserId.setAttribute("type", "text");
        if (chatUserId.value == null || chatUserId.value.length == 0)
            chatUserId.value = "Enter email address";

        chatUserId.setAttribute("class", "ImageOptionLinkText ShowText");
        chatUserId.onkeypress = function (e) {
            CheckEnterKeyForOptions(e, type);
        }
        chatUserId.onkeyup = function (e) {
            CheckKeyPressEvent(e, type);
        }
        chatUserId.onkeyup = function (e) {
            CheckKeyPressEvent(e, type);
        }
        chatUserId.onfocus = function (e) {
            SetTextValue(this, 'Enter email address');
        }
        chatUserId.onblur = function (e) {
            SetImageContentOptions('chatuser', this.value, this, type);
            ResetTextValue(this, 'Enter email address');
        }
    }

    if (type == "video") {
        remove.setAttribute("title", "Remove this video");
        //remove.setAttribute("onclick", "RemoveImage(this.parentNode.parentNode);");
        /*$(remove).mousedown(function () {
        RemoveVideo($(this).parent().parent()[0]);
        });*/
        $(remove).mouseup(function () {
            RemoveVideo($(this).parent().parent()[0]);
        });
        $(makeBackground).mousedown(function () {
            SetVideoBackground($(this).parent().parent()[0]);
        });
        $(makeItem).mousedown(function (e) {
            SetVideoItem($(this).parent().parent()[0]);
        });
    }
    else if (type == "flash") {
        remove.setAttribute("title", "Remove this flash animation");
        remove.onclick = function (e) {
            RemoveImage(this.parentNode.parentNode.parentNode);
        }
    }
    else {
        remove.setAttribute("title", "Remove this image");
        remove.onclick = function (e) {
            RemoveImage(this.parentNode.parentNode.parentNode);
        }
        makeBackground.onclick = function (e) {
            SetWindowBackground(this.parentNode.parentNode);
            HideImageOptions(this.parentNode.parentNode);
            ShowImageOptions(this.parentNode.parentNode)
        }
        fitWindow.onclick = function (e) {
            FitInWindow(this.parentNode.parentNode);
        }
        makeItem.onclick = function (e) {
            SetImageItem(this.parentNode.parentNode);
        }
    }

    if ($(captionContainer).find("#Caption").length == 0)
        captionContainer.appendChild(caption);

    if ($(captionContainer).find("#txtImageCaption").length == 0)
        captionContainer.appendChild(captionText);

    if ($(linkContainer).find("#ImageLink").length == 0)
        linkContainer.appendChild(link);
    if ($(linkContainer).find("#txtImageLink").length == 0)
        linkContainer.appendChild(linkText);


    if ($(checkContainer).find("#chkAutoPlay").length == 0) {
        checkContainer.appendChild(autoPlayCheckBox);
        checkContainer.appendChild(autoPlayText);
        checkContainer.appendChild(muteCheckBox);
        checkContainer.appendChild(muteText);

    }

    if (imageChatEnabled) {
        if ($(chatContainer).find("#Chat").length == 0)
            chatContainer.appendChild(chat);

        if ($(chatContainer).find("#ChatUserId").length == 0)
            chatContainer.appendChild(chatUserId);

    }
    //autoPlayOptionContainer.appendChild(autoPlayOptions);
    BuildAutoPlayOptionPopup(autoPlayOptionContainer);
    /*if ($(autoPlayOptionContainer).find(".AutoPlayOptionDropdown").length == 0) {        
    autoPlayOptionContainer.appendChild();
    }*/
    //    autoPlayOptionContainer.appendChild(GetImageAutoPlayOptions());

    //autoCheckContainer.appendChild(autoPlay);
    if ($(autoCheckContainer).find("#AutoCheckContainer").length == 0)
        autoCheckContainer.appendChild(checkContainer);

    if ($(imageOptionContainer).find("#CaptionContainer").length == 0)
        imageOptionContainer.appendChild(captionContainer);

    if ($(imageOptionContainer).find("#LinkContainer").length == 0)
        imageOptionContainer.appendChild(linkContainer);

    if (imageChatEnabled) {
        if ($(imageOptionContainer).find("#ChatContainer").length == 0)
            imageOptionContainer.appendChild(chatContainer);

    }

    if (type == "video") {
        if ($(imageOptionContainer).find("#ImageOriginalSize").length == 0)
            imageOptionContainer.appendChild(originalSize);
        if ($(imageOptionContainer).find("#ImageFitWindow").length == 0)
            imageOptionContainer.appendChild(fitWindow);
        if ($(imageOptionContainer).find("#ImageItem").length == 0)
            imageOptionContainer.appendChild(makeItem);
        if ($(imageOptionContainer).find("#ImageBackground").length == 0)
            imageOptionContainer.appendChild(makeBackground);
        //imageOptionContainer.appendChild(attachOptionImage);
        if ($(imageOptionContainer).find("#AutoCheckContainer").length == 0)
            imageOptionContainer.appendChild(autoCheckContainer);
        if ($(linkContainer).find("#chkAutoRedirect").length == 0)
            linkContainer.appendChild(autoRedir);
    }
    else if (type == "image") {
        /*if ($(imageOptionContainer).find("#ImageOriginalSize").length == 0)
        imageOptionContainer.appendChild(originalSize);*/
        if ($(imageOptionContainer).find("#ImageFitWindow").length == 0)
            imageOptionContainer.appendChild(fitWindow);
        if ($(imageOptionContainer).find("#ImageItem").length == 0)
            imageOptionContainer.appendChild(makeItem);
        if ($(imageOptionContainer).find("#ImageBackground").length == 0)
            imageOptionContainer.appendChild(makeBackground);

        //imageOptionContainer.appendChild(autoPlayOptions);
        //if ($(imageOptionContainer).find("#ImageOptInForm").length == 0)
        //imageOptionContainer.appendChild(optInForm);

        if ($(imageOptionContainer).find("#AutoPlayOptionContainer").length == 0)
            imageOptionContainer.appendChild(autoPlayOptionContainer);
    }
    if ($(imageOptionContainer).find("#ImageRemove").length == 0)
        imageOptionContainer.appendChild(remove);
    //imageOptionContainer.appendChild(moreOptions);

    //UpdateZIndexCounter();
    $(container).mouseup(function () {
        DeselectMediaElements();
        HighlightImage(this);
    });

    /*$(container).drag(function () {
    DeselectMediaElements(); HighlightImage(this);
    });*/

    $(container).hover(
        function () {
            /*tempZIndex = $(this).css("z-index");
            $(this).css("z-index", zIndexCounter + 2);*/
            ShowImageOptions(this);
        },
        function () {
            /*if ($(this).attr("highlight") == undefined) {
            $(this).css("z-index", tempZIndex);
            }*/
            HideImageOptions(this);
        });


    currentImageObject = container;
    //$(".ImageDeleteOptions").each(function () { $(this).css("border", ""); });
    if (container != null) {
        if ($(container).find("#ImageOptionContainer").length == 0)
            container.appendChild(imageOptionContainer);
    }
    if ($(imageOptionContainer).parent().attr("StartTime") == null ||
    $(imageOptionContainer).parent().attr("StartTime") == "undefined")
        $(imageOptionContainer).parent().attr("StartTime", "00:00");
    if ($(imageOptionContainer).parent().attr("EndTime") == null ||
    $(imageOptionContainer).parent().attr("EndTime") == "undefined")
        $(imageOptionContainer).parent().attr("EndTime", "05:00");
    if ($(imageOptionContainer).parent().attr("PlayUntilEnd") == null ||
    $(imageOptionContainer).parent().attr("PlayUntilEnd") == "undefined")
        $(imageOptionContainer).parent().attr("PlayUntilEnd", "false");
    //DeselectMediaElements();
    HighlightImage(container);
}

function IsFirstMediaItem() {
    var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");
    if ($("#" + GetCurrentOpenContainer()).find(".ImageDeleteOptions").length <= 0) {
        if (g_BuildingPml) {
            return false;
        }
        return true;
    }
    else {
        return false;
    }
}

function DefaultBackgroundMediaItem(element) {
    PrepareImageOptions(element.parentNode, "image");
    //FitInWindow(element);

    $(element.parentNode.parentNode).find(".ImageOptionContainer").each(function () {
        $(this).css("position", "absolute");
        //$(this).css("position", "fixed");
        //$(this).css("top", "85px");
        //$(this).css("left", "232px");
        $(this).find("#ImageItem").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageFitWindow").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageBackground").each(function () {
            $(this).css("display", "none");
        });

        $(this).find("#ImageOriginalSize").each(function () {
            $(this).css("display", "block");
        });

        $(this).find(".AutoPlayOptionDropdown").each(function () {
            var width = $("#PmlContainer").css("width").replace("px", "");
            var height = $("#PmlContainer").css("height").replace("px", "");

            $(this).css("width", (width - 15) + "px");
            $(this).css("top", (height - 75) + "px");
            //$(this).css("bottom", "53%");
            //$(this).css("width", "37.3%");
        });
    });

    //Kiran: 
    //$(element).removeAttr("onload");
}

/*function Highlight(c) {
DeselectMediaElements();
$(c).attr("highlight", "true");
$(c).css("border", "7px solid #CCC");
$(c).css("border-radius", "4px");
$(c).css("box-shadow", "4px 4px 4px #DDD");
$(c).find(".ImageOptionContainer").each(function () { $(this).show(); });

$(topElement).css("z-index", zIndexCounter);
$(c).css("z-index", zIndexCounter + 1);
topElement = c;
}*/

function HighlightImage(c) {
    //DeselectMediaElements();

    if ($(c).attr("background") == "true") {
        $(c).css('z-index', "");
    }

    if ($(c).attr("background") != "true") {
        $(c).find(".ImageOptionContainer").each(function () {
            $(this).show();

            var width = $("#" + GetCurrentOpenContainer()).css("width").replace("px", "");
            var height = $("#" + GetCurrentOpenContainer()).css("height").replace("px", "");
            var top = $(this).parent().css("top").replace("px", "");
            var left = $(this).parent().css("left").replace("px", "");
            var maxWidth = width - left;
            var maxHeight = height - top;
            var isVideo = false;

            $(this).parent().find(".resizeimage").resizable({ maxWidth: maxWidth, maxHeight: maxHeight });

            $(c).find(".resizevideo").each(function () {
                isVideo = true;
            });

            if (isVideo) {
                $(this).parent().resizable(
                {
                    maxWidth: maxWidth,
                    maxHeight: maxHeight
                });
            }
        });
    }
    /*else {
    $(c).css('z-index', UpdateZIndexCounter());
    }*/

    if ($(c).attr("background") == undefined || $(c).attr("background") == "false") {
        $(c).css("border", "7px solid #CCC");
        $(c).css("border-radius", "4px");
        $(c).css("box-shadow", "4px 4px 4px #DDD");

        if ($(topElement).length > 0) {
            if ($(topElement)[0].tagName == 'TEXTAREA') {
                $(topElement.id + "_parent").css("z-index", UpdateZIndexCounter());
            }
            else if ($(topElement).attr("background") != "true") {
                $(topElement).css("z-index", UpdateZIndexCounter());
            }
        }

        $(c).css("z-index", UpdateZIndexCounter());
        topElement = c;
    }
    $(c).find(".ImageOptionContainer").show();
}

function ShowImageOptions(container) {
    $(container).find(".ImageOptionContainer").each(function () { $(this).show() });
}

function RemoveImage(element) {
    if (confirm("Are you sure you want to delete this image?")) {
        RemoveItemFromGloablArray(element);
        $(element).remove();
        GetPreview();
        var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");
        //TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] = TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] - 1;
    }
}

function RemoveVideo(element) {
    if (confirm("Are you sure you want to delete this video?")) {
        RemoveItemFromGloablArray(element);
        $(element).remove();
        GetPreview();
        var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");
        //TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] = TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] - 1;
    }
}

/*function GetGlobalArrayElements() {
var tabCount = 0;
var elements = "";

$("#TabContainer").find("input").each(function () {
tabCount++;
});

for (j = 0; j < tabCount; j++) {
if (Global_Item_List[j] != undefined) {
for (i = 0; i < Global_Item_List[j].length; i++) {
elements += $("<div><div tab='" + j + "' content='" + i + "' class='ThumbnailsStatus'>" + $(Global_Item_List[j][i]).html() + "</div></div>").html();
}
}
}

return elements;
}*/

function RemoveItemFromGloablArray(element) {

    /*for (i = 0; i < Global_Item_List[currentTabIndex].length; i++) {
    if ($(Global_Item_List[currentTabIndex][i]).html().indexOf("iframe") > -1) {
    if (Global_Item_List[currentTabIndex][i].innerHTML == element.innerHTML) {
    Global_Item_List[currentTabIndex].splice(i, 1);
    RemoveThumbnail(i);
    break;
    }
    }
    else {
    //alert(Global_Item_List[currentTabIndex][i].innerHTML);
    if (element.childNodes[0].innerHTML == undefined) {
    if (Global_Item_List[currentTabIndex][i].innerHTML == element.innerHTML) {
    Global_Item_List[currentTabIndex].splice(i, 1);
    RemoveThumbnail(i);
    break;
    }
    }
    else if (Global_Item_List[currentTabIndex][i].innerHTML == element.childNodes[0].innerHTML) {
    Global_Item_List[currentTabIndex].splice(i, 1);
    RemoveThumbnail(i);
    break;
    }
    else if (Global_Item_List[currentTabIndex][i].innerHTML == element.innerHTML) {
    Global_Item_List[currentTabIndex].splice(i, 1);
    RemoveThumbnail(i);
    break;
    }
    if (Global_Item_List[currentTabIndex][i].innerHTML == "") {
    Global_Item_List[currentTabIndex].splice(i, 1);
    RemoveThumbnail(i);
    break;
    }
    }
    }*/

    //var curTab = GetCurrentOpenContainer();
    var itemnumber = $(element).attr("itemnumber");
    if (itemnumber == null)
        itemnumber = $(element).parent().attr("itemnumber");
    RemoveThumbnail(parseInt(itemnumber));
    return;
}

function SetVideoBackground(element) {
    // Reset any background elements to its normal position
    $("#" + GetCurrentOpenContainer()).find(".ImageDeleteOptions").each(function () {
        if ($(this).attr("background") == "true") {

            $(this).find("img.resizeimage").each(function () {
                SetImageItem($(this).parent());
            });
            $(this).find("iframe").each(function () {
                SetVideoItem($(this).parent());
            });
        }
    });

    $("#" + GetCurrentOpenContainer()).css("overflow", "hidden");

    $(element).find("iframe").each(function () {
        $(this).css("z-index", "1");
        //$(this).removeAttr("class");
        $(this).resizable('destroy');
    });

    $(element).css("width", "100%");
    $(element).css("height", "100%");
    $(element).css("border", "inherit");
    $(element).css("box-shadow", "inherit");
    $(element).css("padding", "0px");
    $(element).css("top", "0px");
    $(element).css("left", "0px");
    $(element).css("z-index", "1");
    $(element).css("cursor", "default");
    $(element).attr("background", "true");

    $(element).removeAttr("onclick");
    $(element).removeAttr("ondrag");
    $(element).draggable("destroy");
    $(element).resizable('destroy');

    $(element).find(".ImageOptionContainer").each(function () {
        $(this).css("position", "absolute");
        //$(this).css("position", "fixed");
        //$(this).css("top", "85px");
        //$(this).css("left", "232px");
        $(this).find("#ImageItem").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageBackground").each(function () {
            $(this).css("display", "none");
        });
    });

    $(element).off('hover');

    $(element).hover(
        function () {
            ShowImageOptions(this);
        },
        function () {
            HideImageOptions(this);
        });
}

function SetWindowBackground(element) {
    SetBackground(element);
    FitInWindow(element);
}

function SetBackground(element) {
    var width;
    var height;

    // Reset any background elements to its normal position
    $("#" + GetCurrentOpenContainer()).find(".ImageDeleteOptions").each(function () {
        if ($(this).attr("background") == "true") {
            $(this).find("img.resizeimage").each(function () {
                SetImageItem($(this).parent());
            });
            $(this).find("iframe").each(function () {
                SetVideoItem($(this).parent());
            });
        }
    });

    $("#" + GetCurrentOpenContainer()).css("overflow", "hidden");

    $(element).find("img.resizeimage").each(function () {
        width = $(this).attr("originalwidth");
        height = $(this).attr("originalheight");
        $(this).css("z-index", "1");
        $(this).resizable('destroy');
        $(this).css("width", $(this).attr("originalwidth"));
        $(this).css("height", $(this).attr("originalheight"));
    });

    $(element).attr("background", "true");
    $(element).css("width", width);
    $(element).css("height", height);
    $(element).css("z-index", "");
    $(element).css("border", "inherit");
    $(element).css("box-shadow", "inherit");
    $(element).css("top", "0px");
    $(element).css("left", "0px");
    $(element).css('cursor', 'default');

    $(element).find(".ImageOptionContainer").each(function () {
        $(this).css("position", "fixed");
        $(this).css("top", "85px");
        $(this).css("left", "232px");
        $(this).find("#ImageItem").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageFitWindow").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageBackground").each(function () {
            $(this).css("display", "none");
        });

        $(this).find("#ImageOriginalSize").each(function () {
            $(this).css("display", "block");
        });

        $(this).find(".AutoPlayOptionDropdown").each(function () {
            var width = $("#PmlContainer").css("width").replace("px", "");
            var height = $("#PmlContainer").css("height").replace("px", "");

            $(this).css("width", (width - 15) + "px");
            $(this).css("top", (height - 75) + "px");
            //$(this).css("bottom", "53%");
            //$(this).css("width", "37.3%");
        });
    });

    $(element).off('hover');

    $(element).hover(
        function () {
            ShowImageOptions(this);
        },
        function () {
            HideImageOptions(this);
        });

    $(element).removeAttr("onclick");
    $(element).removeAttr("ondrag");
    $(element).draggable("destroy");

    SetBackgroundDraggable($(element));
}

function SetBackgroundDraggable(element) {
    var productHeadOffset = $("#" + GetCurrentOpenContainer()).offset();
    var productHeadHeight = $("#" + GetCurrentOpenContainer()).height();
    var productHeadWidth = $("#" + GetCurrentOpenContainer()).width();
    var productHeadImageHeight = $(element).height();

    var right = productHeadOffset.left;
    var bottom = productHeadOffset.top;
    var left = ($(element).width() > productHeadWidth) ? (productHeadWidth + productHeadOffset.left) - $(element).width() : 0;
    var top = (productHeadHeight + productHeadOffset.left) - $(element).height() - 142;

    //jQuery('.productHeadImage').draggable();

    $(element).draggable({ containment: [left, top, right, bottom] });
}

function SetVideoItem(element) {
    //$(element).attr("onclick", "DeselectMediaElements();HighlightImage(this);");
    //$(element).attr("ondrag", "DeselectMediaElements();HighlightImage(this);");
    $(element).draggable("destroy");

    var width = $("#" + GetCurrentOpenContainer()).css("width").replace("px", "");
    var height = $("#" + GetCurrentOpenContainer()).css("height").replace("px", "");
    var top = $(element).parent().css("top").replace("px", "");
    var left = $(element).parent().css("left").replace("px", "");
    var maxWidth = width - left;
    var maxHeight = height - top;

    $(element).resizable({ iframeFix: true, maxWidth: maxWidth, maxHeight: maxHeight });
    $(element).draggable({ containment: $(element).parents(".SubTabContent") });
    $(element).css("top", "25px");
    $(element).css("left", "15px");
    $(element).css("cursor", "move");
    $(element).css("padding", "6px");
    $(element).attr("background", "false");

    $(element).find(".ImageOptionContainer").each(function () {
        $(this).css("position", "absolute");
        $(this).css("top", "0px");
        $(this).css("left", "0px");

        $(this).find("#ImageItem").each(function () {
            $(this).css("display", "none");
        });

        $(this).find("#ImageFitWindow").each(function () {
            $(this).css("display", "none");
        });

        $(this).find("#ImageBackground").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageOriginalSize").each(function () {
            $(this).css("display", "none");
        });

    });

    $(element).css("width", "300px");
    $(element).css("height", "150px");
    HighlightImage(element);
}

function SetImageItem(element) {
    $(element).find(".resizeimage").resizable('destroy');
    $(element).css("width", "");
    $(element).css("height", "");

    var width = $("#" + GetCurrentOpenContainer()).css("width").replace("px", "");
    var height = $("#" + GetCurrentOpenContainer()).css("height").replace("px", "");
    var top = $(element).parent().css("top").replace("px", "");
    var left = $(element).parent().css("left").replace("px", "");
    var maxWidth = width - left;
    var maxHeight = height - top;

    $(element).find("img").each(function () {
        if ($(this).attr("class") != "ContentOptionHover" &&
        $(this).attr("class") != "ContentOptionClick" &&
        $(this).parent().attr("id") != "pml_remove_form" &&
        $(this).parent().attr("id") != "pml_add_field" &&
        $(this).parent().attr("id") != "pml_remove_field") {
            $(this).attr("class", "resizeimage");
            AdjustImageSize(this);
            /*$(this).css("width", "260px");
            $(this).css("height", "180px");*/
            $(this).resizable({ maxWidth: maxWidth, maxHeight: maxHeight });
        }
    });

    $(element).find(".ImageOptionContainer").each(function () {
        $(this).css("position", "absolute");
        $(this).css("top", "0px");
        $(this).css("left", "0px");

        $(this).find("#ImageItem").each(function () {
            $(this).css("display", "none");
        });

        $(this).find("#ImageFitWindow").each(function () {
            $(this).css("display", "none");
        });

        $(this).find("#ImageBackground").each(function () {
            $(this).css("display", "block");
        });

        $(this).find("#ImageOriginalSize").each(function () {
            $(this).css("display", "none");
        });


        $(this).find(".AutoPlayOptionDropdown").each(function () {
            $(this).css("bottom", "0px");
            $(this).css("width", "90%");
            $(this).css("top", "inherit");
        });
    });
    $(element).attr("background", "false");
    //$(element).attr("onclick", "DeselectMediaElements();HighlightImage(this);");
    //$(element).attr("ondrag", "DeselectMediaElements();HighlightImage(this);");
    $(element).draggable("destroy");
    $(element).draggable({ containment: $(element).parents(".SubTabContent") });
    $(element).css("top", "0px");
    $(element).css("left", "0px");
    //
    /*$(element).css("width", "auto");
    $(element).css("height", "auto");*/

    $(element).hover(
        function () {
            /*tempZIndex = $(this).css("z-index");
            $(this).css("z-index", zIndexCounter + 2);*/
            ShowImageOptions(this);
        },
        function () {
            /*if ($(this).attr("highlight") == undefined) {
            $(this).css("z-index", tempZIndex);
            }*/
            HideImageOptions(this);
        });

    HighlightImage(element);
}

function FitInWindow(element) {
    $(element).find(".resizeimage").each(function () {
        if ($(this).attr("class") != "ContentOptionHover" &&
          $(this).attr("class") != "ContentOptionClick" &&
          $(this).attr("class") != "RemoveForm" &&
          $(this).parent().attr("id") != "pml_remove_form" &&
          $(this).parent().attr("id") != "pml_add_field" &&
          $(this).parent().attr("id") != "pml_remove_field") {
            $(this).css("width", "100%");
            $(this).css("height", "100%");
        }
    });

    $(element).css("top", "0px");
    $(element).css("left", "0px");
    $(element).css("width", "100%");
    $(element).css("height", "100%");
    $(element).css('cursor', 'default');
    $(element).draggable("destroy");
}

function OriginalSize(element) {
    var width;
    var height;

    $(element).css("top", "0");
    $(element).css("left", "0");

    $(element).find("img").each(function () {
        if ($(this).attr("class") != "ContentOptionHover" && $(this).attr("class") != "ContentOptionClick" && $(this).attr("class") != "RemoveForm") {
            width = $(this).attr("originalwidth");
            height = $(this).attr("originalheight");
            $(this).css("width", width);
            $(this).css("height", height);
        }
    });

    $(element).css("top", "0px");
    $(element).css("left", "0px");
    $(element).css("width", width);
    $(element).css("height", height);
    $(element).draggable("destroy");
    SetBackgroundDraggable($(element));
    //$(element).draggable({ containment: false });
}

function HideText(node) {
    $(node).fadeOut(200);
}

function CheckKeyPressEvent(evt, type) {
    var evt = (evt) ? evt : ((event) ? event : null);
    if (evt.charCode == 0)
        return;
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);

    // TODO - Last character is not entered
    switch (node.id) {
        case "txtImageLink":
            SetImageContentOptions("link", node.value, node, type);
            break;
        case "txtImageCaption":
            SetImageContentOptions("caption", node.value, node, type);
            break;
        case "ChatUserId":
            SetImageContentOptions("chatUserID", node.value, node, type);
            break;
    }


}

function CheckEnterKeyForOptions(evt, type) {
    var evt = (evt) ? evt : ((event) ? event : null);

    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);

    if ((evt.keyCode == 13) && (node.type == "text")) {
        switch (node.id) {
            case "txtImageLink":
                // Set image link attribute for given image
                SetImageContentOptions("link", node.value, node, type);
                HideText(node);
                break;
            case "txtImageCaption":
                // Set image caption attribute for given image
                SetImageContentOptions("caption", node.value, node, type);
                HideText(node);
                break;
            case "ChatUserId":
                SetImageContentOptions("chatUserID", node.value, node, type);
                HideText(node);

            default:
                return false;
        }

        return false;
    } else if ((evt.keyCode == 13) && (node.type == "file")) {
        return false;
    }
}

function GetImageOptions(c) {
    $(c).find(".ImageOptionContainer").each(function () { $(this).fadeIn(200); });
}

function HideImageOptions(c) {
    if ($(c).css("box-shadow").indexOf("none") > -1) {
        $(c).find(".ImageOptionContainer").each(function () { $(this).fadeOut(200); });
    }
}

function DeselectMediaElements() {
    $(".ImageDeleteOptions").each(function () {
        $(this).css("border", "none");
        $(this).css("box-shadow", "none");
        $(this).find(".resizeimage").resizable('destroy');
        //$(this).removeAttr("highlight");
        $(this).find(".ImageOptionContainer").each(function () { $(this).hide(); });
    });

    $("div.resize").each(function () {
        $(this).css("background", "transparent");
    });
}

function SetImageContentOptions(type, value, txtElement, option) {
    var element = (option == "image") ? "img.resizeimage" : "iframe.resizeiframe";
    $(txtElement).parent().parent().parent().find(element).each(function () {
        $(this).attr(type, value);
    });
    $(txtElement).attr("value", value);
}

function SetTextValue(element, defaultText) {
    if ($(element).attr("value").toLowerCase() == defaultText.toLowerCase()) {
        element.value = "";
    }
}
function ResetTextValue(element, defaultText) {
    if ($(element).attr("value") == "") {
        element.value = defaultText;
    }
}

function GetSortOrder(type, key) {
    switch (type) {
        case "image":
            switch (key) {
                case "name":
                    imageNameSortOrder = (imageNameSortOrder == "asc") ? "desc" : "asc";
                    return imageNameSortOrder;
                case "date":
                    imageDateSortOrder = (imageDateSortOrder == "asc") ? "desc" : "asc";
                    return imageDateSortOrder;
            }
            break;
        case "video":
            switch (key) {
                case "name":
                    videoNameSortOrder = (videoNameSortOrder == "asc") ? "desc" : "asc";
                    return videoNameSortOrder;
                case "date":
                    videoDateSortOrder = (videoDateSortOrder == "asc") ? "desc" : "asc";
                    return videoDateSortOrder;
            }
            break;
    }
}

// Container = image/video container, direction = zoomin/zoom out
function ZoomImages(containerId, direction) {
    if (direction == "zoomin") {
        zoomPercentage = (zoomPercentage == 150) ? 151 : zoomPercentage + 25;
    }
    else {
        zoomPercentage = (zoomPercentage == 50) ? 49 : zoomPercentage - 25;
    }


    $("#" + containerId).find("img.DragContent").each(function () {
        if (zoomPercentage >= 50 && zoomPercentage <= 150) {
            var width = parseInt($(this).attr("width").replace("px", ""));
            var height = parseInt($(this).attr("height").replace("px", ""));
            var newWidth = Math.ceil(width * (zoomPercentage / 100));
            var newHeight = Math.ceil(height * (zoomPercentage / 100));
        }
        else {
            var newWidth = parseInt($(this).attr("width").replace("px", ""));
            var newHeight = parseInt($(this).attr("height").replace("px", ""));
        }

        $(this).parent().css("width", "auto");
        $(this).parent().css("height", "auto");
        $(this).parent().attr("width", newWidth + "px");
        $(this).parent().attr("height", newHeight + "px");
        $(this).attr("width", newWidth + "px");
        $(this).attr("height", newHeight + "px");
    });
}

function GetOptionalImage(element) {
    currentVideoElement = $(element).parent().parent().parent();

    if (currentOpenDialog != "Image") {
        currentOpenDialog = "Image";
        RestoreDefaultIcons();
        ActiveElement(this, 'image_icon');
        ActiveContentTab($('#ImageMenuOptions').find('li:first'));
        HideVideoMenuOptions();
        ShowImageMenuOptions();
    }

    if (videoOptionalImageInterval == null) {
        videoOptionalImageInterval = setInterval(function () {
            if ($("#OptionalVideoText1").css("display") == "block") {
                $("#OptionalVideoText1").hide();
                $("#OptionalVideoText2").hide();
                $("#OptionalVideoText3").css("visibility", "hidden");
            }
            else {
                $("#OptionalVideoText1").show();
                $("#OptionalVideoText2").show();
                $("#OptionalVideoText3").css("visibility", "visible");
            }
        }, 1000);
    }
}

function GetImageAutoPlayOptions(elem, create) {

    var container, sliderRange, startTime, endTime;
    var min = 0;
    var max = 305;
    if (create) {
        container = document.createElement("div");
        sliderRange = document.createElement("div");
        sliderRange.setAttribute("id", "SliderRange");
        container.setAttribute("id", "SliderContainer");
        startTime = document.createElement("span");
        endTime = document.createElement("span");
        startTime.setAttribute("id", "PmlStartTimeHandle");
        startTime.innerHTML = "00:00";
        endTime.innerHTML = "end";
        endTime.setAttribute("id", "PmlEndTimeHandle");
        container.appendChild(sliderRange);
        container.appendChild(startTime);
        container.appendChild(endTime);
        elem.appendChild(container);

    } else {
        container = $(elem).find("#SliderContainer")[0];
        sliderRange = $(elem).find("#SliderRange")[0];
        startTime = $(elem).find("#PmlStartTimeHandle")[0];
        endTime = $(elem).find("#PmlEndTimeHandle")[0];
        var minT = $(elem).parent().parent().parent().attr("StartTime");
        var times = minT.split(":");
        min = parseInt(times[0]) * 60 + parseInt(times[1]);
        var maxT = $(elem).parent().parent().parent().attr("EndTime");
        times = maxT.split(":");
        max = parseInt(times[0]) * 60 + parseInt(times[1]);
    }



    $(sliderRange).slider({
        range: true,
        min: 0,
        max: 305,
        values: [min, max],
        slide: function (event, ui) {
            // Show Values on UI as well
            var first = false;

            $(this).parent().find("a").each(function () {
                var left = $(this).css("left");
                var top = $(this).css("top");

                if (!first) {
                    $(this).parent().parent().find("#PmlStartTimeHandle").each(function () {
                        $(this).html(CalculateTime(ui.values[0]));
                        var imageContainer = $(this).parent().parent().parent().parent().parent();

                        if ($(imageContainer).attr("background") == "true") {
                            if (parseFloat(left.replace("%", "")) > 75) {
                                $(this).css("left", "75%");
                            }
                            else {
                                $(this).css("left", left);
                            }
                        }
                        else {
                            if (parseFloat(left.replace("%", "")) > 60) {
                                $(this).css("left", "58%");
                            }
                            else {
                                $(this).css("left", left);
                            }
                        }
                    });

                    first = true;
                }
                else {
                    $(this).parent().parent().find("#PmlEndTimeHandle").each(function () {
                        if (CalculateTime(ui.values[1]) == "end") {
                            $(this).html(CalculateTime(ui.values[1]));
                            if (parseFloat(left.replace("%", "")) > 95) {
                                $(this).css("left", "95%");
                            }
                            else {
                                $(this).css("left", left);
                            }
                        }
                        else {
                            $(this).html(CalculateTime(ui.values[1]));
                            var imageContainer = $(this).parent().parent().parent().parent().parent();
                            if ($(imageContainer).attr("background") == "true") {
                                if (parseFloat(left.replace("%", "")) > 75) {
                                    $(this).css("left", "75%");
                                }
                                else {
                                    $(this).css("left", left);
                                }
                            }
                            else {

                                if (parseFloat(left.replace("%", "")) > 65) {
                                    $(this).css("left", "65%");
                                }
                                else {
                                    $(this).css("left", left);
                                }
                            }
                        }


                    });
                }
            });

            SetTimeEntered(this, CalculateTime(ui.values[0]), CalculateTime(ui.values[1]));
        }
    });


    return container;
}

function BuildAutoPlayOptionPopup(options) {
    var container;
    var create = false;
    if ($(options).find(".AutoPlayOptionDropdown").length == 0) {
        container = document.createElement("div");
        //var done = document.createElement("div");
        container.setAttribute("class", "AutoPlayOptionDropdown");
        options.appendChild(container);
        create = true;
    } else
        container = $(options).find(".AutoPlayOptionDropdown")[0];
    //container.appendChild(GetImageAutoPlayOptions());
    GetImageAutoPlayOptions(container, create);

    //return container;
}

function CalculateTime(value) {
    if (value > 300)
        return "end";
    if (isNaN(value))
        return "0:00";
    var min = Math.floor(value / 60);
    var sec = Math.floor(value % 60);

    if (min < 10) {
        min = "0" + min;
    }
    if (sec < 10) {
        sec = "0" + sec;
    }
    return min + ":" + sec;
}

function SetElementAutoPlayTime(doneButton, startTime, endTime) {
    $(doneButton).parent().parent().parent().parent().attr("StartTime", startTime);
    $(doneButton).parent().parent().parent().parent().attr("EndTime", endTime);
}

// This function shall validate the time entered by the user. End time shall be greater than the start time.
function SetTimeEntered(element, startTime, endTime) {
    $(element).parent().parent().parent().parent().parent().attr("StartTime", startTime);
    if (endTime == "end") {
        $(element).parent().parent().parent().parent().parent().attr("PlayUntilEnd", "true");
        $(element).parent().parent().parent().parent().parent().attr("EndTime", "5:00");
    }
    else {
        $(element).parent().parent().parent().parent().parent().attr("PlayUntilEnd", "false");
        $(element).parent().parent().parent().parent().parent().attr("EndTime", endTime);
    }
}

function SetAutoPlayOption(element, checked) {
    $(element).parent().parent().parent().parent().find("iframe").each(function () {
        $(this).attr("pmlautoplay", checked);
    });

    $(element).parent().parent().parent().parent().find("img.resizevideo").each(function () {
        $(this).attr("pmlautoplay", checked);
    });
}

function SetUploadOptions() {
    if (UPLOAD_LIMIT_REACHED == true || UPLOAD_LIMIT_REACHED == "true") {
        $("#clLocalVideos").html("<div style='font-size: 12px;'>You have reached upload limit. Please upgrade your account to upload more files.</div>");
        $("#clLocalImages").html("<div style='font-size: 12px;'>You have reached upload limit. Please upgrade your account to upload more files.</div>");
    }
}