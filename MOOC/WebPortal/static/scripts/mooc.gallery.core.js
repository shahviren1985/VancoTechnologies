
//var BASE_URL = "http://localhost:6328/WebPortal/";


// image prop functions
function ShowImagePropertyBlock(img) {
    var imgId = $("#" + img);
    $("#imgProp").attr("src", $(imgId).attr("src"));

    if ($(imgId).attr("orignal") != undefined)
        $("#imgProp").attr("orignal", $(imgId).attr("orignal"));

    $("#txtAltText").val($(imgId).attr("alt"));
    $("#txtImgWidth").val($(imgId).attr("width"));
    $("#txtImgHeight").val($(imgId).attr("height"));
}

function ShowImagePropertyBlockAndAddToEditor(img) {
    var imgId = $("#" + img);
    $("#imgProp").attr("src", $(imgId).attr("src"));

    if ($(imgId).attr("orignal") != undefined)
        $("#imgProp").attr("orignal", $(imgId).attr("orignal"));

    $("#txtAltText").val($(imgId).attr("alt"));
    $("#txtImgWidth").val($(imgId).attr("width"));
    $("#txtImgHeight").val($(imgId).attr("height"));

    AddImageToCkEditor();
}

// video prop functions
function ShowVideoPropertyBlock(img) {
    var imgId = $("#" + img);
    $("#imgVideoProp").attr("src", $(imgId).attr("src"));

    if ($(imgId).attr("orignal") != undefined)
        $("#imgVideoProp").attr("orignal", $(imgId).attr("orignal"));

    //$("#txtVideoName").val($(imgId).attr("alt"));
    $("#txtVideoWidth").val("300");
    $("#txtVideoHeight").val("250");
}

function ShowVideoPropertyBlockAndAddToEditor(img) {
    var imgId = $("#" + img);
    $("#imgVideoProp").attr("src", $(imgId).attr("src"));

    if ($(imgId).attr("orignal") != undefined)
        $("#imgVideoProp").attr("orignal", $(imgId).attr("orignal"));

    //$("#txtVideoName").val($(imgId).attr("alt"));
    $("#txtVideoWidth").val("300");
    $("#txtVideoHeight").val("250");

    AddVideoToCkEditor();
}

/*---------------------------------Search images from google---------------------------------------*/

function SearchImages(start) {
    //BASE_URL = "http://localhost:6328/WebPortal/";
    $("#searchResult").empty();
    if ($("#txtSearchImage") == null) {
        return;
    }

    if ($("#txtSearchImage").val() == "" || $("#txtSearchImage").val().toLowerCase() == "enter keywords to search on google...") {
        $("#searchResult").empty();
        return;
    }

    var searchEngine = "Google";

    if (searchEngine == undefined)
        searchEngine = "Google";

    var height = parseInt($("#searchResult").css("height").replace("px", ""));

    if (height == 0) {
        $("#searchResult").css("height", "400px");
    }
    else {
        $("#searchResult").css("height", "300px");
    }

    $("#loaderImg").show();
    /*var xhr = new XMLHttpRequest();

    xhr.open("GET", BASE_URL + "Handler/GetSearchResults.ashx?engine=" + searchEngine + "&type=images&q=" + $("#txtSearchImage").val() + "&start=" + start, true);
    xhr.onreadystatechange = function () {
    if (this.readyState == 4) {
    if (this.responseText.indexOf("Error") > -1) {
    $("#loaderImg").hide();
    $("#searchResult").html(this.responseText);
    }
    else if (this.responseText != "") {
    switch (searchEngine) {
    case "Google":
    FillGoogleSearchResults(eval("(" + this.responseText + ")"));
    break;
    }


    $("#loaderImg").hide();
    var objDiv = document.getElementById('searchResult');
    objDiv.scrollTop = 0;
    }
    }
    };

    xhr.send(null);*/

    var path = "Handler/GetSearchResults.ashx?engine=" + searchEngine + "&type=images&q=" + $("#txtSearchImage").val() + "&start=" + start;

    CallHandler(path, function (result) {
        if (result.responseData != null || result.responseData == null) {
            FillGoogleSearchResults(result);
            $("#loaderImg").hide();
            var objDiv = document.getElementById('searchResult');
            objDiv.scrollTop = 0;
        }
        else {
            $("#loaderImg").hide();
            $("#searchResult").html("An error occured while searching images from google. <span onClick='SearchImages(1);' class='btn btn-primary'>Refresh</span>");
        }
    });
}

function FillGoogleSearchResults(data) {

    var isScrollSet = false;

    SetSearchResultButtons("#searchResult", "#ImageLibraryContent", "#NavigationButtons", "image");

    var ul = $('<ul/>');
    
    var width = parseInt(540);
    var height = parseInt(384);

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");

    if (data == null || data.responseData == null || data.responseData.results == null) {
        currentImageStartCount = 1;
        return;
    }

    for (var i = 0; i < data.responseData.results.length; i++) {
        var img = data.responseData.results[i];
        if (img != null && typeof (img) != undefined) {
            FillImage(img, ul);
        }
    }

    $("#searchResult").css("height", height - 50 + "px");
    //$("#searchResult").css("width", width - 20 + "px");

    $("#searchResult").empty();
    $("#searchResult").append($(ul));
}

var counter = 1;

function FillImage(img, ul) {
    var image = $('<img/>');
    $(image).attr("id", "reImg_" + counter);

    var li = $('<li/>');
    var icons = $("<div/>");

    var infoIcon = $("<i/>");
    var attachIcon = $("<i/>");

    $(icons).attr("class", "content-icon-container");
    $(icons).attr("style", "margin-left: 35px; float: left");
    $(attachIcon).attr("class", "icon-plus content-icon");

    $(infoIcon).attr("class", "icon-info-sign content-icon");

    $(attachIcon).attr("title", "Attach image to editor");
    $(infoIcon).attr("title", "More information about this image...");

    var title = $('<div/>');
    var description = $('<div/>');

    $(li).attr("class", "ImageItem2");

    $(image).attr("class", "SearchResultImage");

    if (img == null)
        return;

    $(image).attr("src", img.tbUrl);

    $(image).attr("originalUrl", img.unescapedUrl);
    $(image).attr("orignal", img.unescapedUrl);
    $(image).attr("width", 150);
    $(image).attr("height", 120);

    if (img.video != undefined && img.video == true) {
        $(image).attr("video", "true");
        var dur = img.duration;
        var duration = "" + Math.round((dur / 3600)) + ":" + Math.round(((dur % 3600) / 60)) + ":" + Math.round((dur % 60));
        $(image).attr("title", img.title.substring(0, 20) + " Duration: " + duration);
        $(image).attr("time", duration);
        title.html(img.title);
        // bind show video property fucntions
        $(infoIcon).attr("onClick", "ShowVideoPropertyBlock('reImg_" + counter + "')");
        $(attachIcon).attr("onClick", "ShowVideoPropertyBlockAndAddToEditor('reImg_" + counter + "')");
        counter++;
    }
    else {
        $(image).attr("video", "false");
        $(image).attr("title", img.title.substring(0, 20));

        $(image).attr("originalwidth", img.width);
        $(image).attr("originalheight", img.height);
        title.html(img.titleNoFormatting);
        // bind show image property fucntions
        $(infoIcon).attr("onClick", "ShowImagePropertyBlock('reImg_" + counter + "')");
        $(attachIcon).attr("onClick", "ShowImagePropertyBlockAndAddToEditor('reImg_" + counter + "')");
        counter++;
    }

    $(image).css({ "cursor": "pointer" });

    title.addClass('SearchResultTitle');
    description.addClass('SearchResultDescription');

    description.html(img.content);

    $(li).append(image);
    $(icons).append(attachIcon);
    $(icons).append(infoIcon);
    $(li).append(icons);

    $(ul).append(li);
}

function GetImagesFromMoocServer() {
    $("#loaderServerImg").show();
    $("#serverImg").empty();
    var path = "Handler/GetImages.ashx";
    CallHandler(path, OnSuccessGetImagesFromMoocServer);
}

function OnSuccessGetImagesFromMoocServer(result) {
    //$("#loaderServerImg").hide();
    if (result.Status == "Ok") {
        var ul = $('<ul/>');
        var width = parseInt(540);
        var height = parseInt(384);

        $(ul).attr("class", "stack");
        $(ul).attr("data-count", "3");
        for (var i = 0; i < result.Images.length; i++) {
            var img = result.Images[i];
            if (img != null && typeof (img) != undefined) {
                PopulateMoocServerImages(img, ul);
            }
        }

        $("#serverImg").css("height", height - 50 + "px");
        //$("#searchResult").css("width", width - 20 + "px");

        $("#serverImg").empty();
        //$("#serverImg").append($(ul));

        $("#loaderServerImg").hide();

        if (result.Images.length < 1) {
            $("#serverImg").html("You have not uploaded any image to mooc server.");
        } else {
            $("#serverImg").append($(ul));
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}

function PopulateMoocServerImages(img, ul) {
    var image = $('<img/>');
    $(image).attr("id", "reImg_" + counter);

    var li = $('<li/>');
    var icons = $("<div/>");

    var infoIcon = $("<i/>");
    var attachIcon = $("<i/>");

    $(icons).attr("class", "content-icon-container");
    $(icons).attr("style", "margin-left: 35px; float: left");
    $(attachIcon).attr("class", "icon-plus content-icon");

    $(infoIcon).attr("class", "icon-info-sign content-icon");

    $(attachIcon).attr("title", "Attach image to editor");
    $(infoIcon).attr("title", "More information about this image...");

    $(infoIcon).attr("onClick", "ShowImagePropertyBlock('reImg_" + counter + "')");
    $(attachIcon).attr("onClick", "ShowImagePropertyBlockAndAddToEditor('reImg_" + counter + "')");
    counter++;

    //$(image).click(function () { Attach($(image)); });
    //$(attachIcon).click(function () { Attach($(image)); });

    //var attach = $('<span/>');
    var title = $('<div/>');
    var description = $('<div/>');

    $(li).attr("class", "ImageItem2");

    $(image).attr("class", "SearchResultImage");

    if (img == null)
        return;

    $(image).attr("src", img.ImagePath);

    $(image).attr("originalUrl", img.ImagePath);
    $(image).attr("orignal", img.ImagePath);
    $(image).attr("width", 150);
    $(image).attr("height", 120);

    $(image).css({ "cursor": "pointer" });

    title.addClass('SearchResultTitle');
    description.addClass('SearchResultDescription');

    description.html(img.content);

    $(li).append(image);
    $(icons).append(attachIcon);
    $(icons).append(infoIcon);
    $(li).append(icons);

    $(ul).append(li);
}

/*------------------------------Search Video--------------------------------------*/

function SearchVideos(start) {
    $('#video_search_engine').hide();

    var searchEngine = "Youtube"; //$("#video_search_engine").attr("engine");
    if (searchEngine == undefined)
        searchEngine = "Youtube";


    $("#search_results").empty();
    //$("#VideoNavigationButtons").empty();
    switch (searchEngine) {
        case "Youtube":
            SearchYoutube(start);
            break;
        case "Vimeo":
            //SearchVimeo(start);
            break;
    }
}

function SearchYoutube(start) {
    //BASE_URL = "http://localhost:6328/WebPortal/";
    var key = $("#searchkey").val();

    if (key == "") {
        //$("#searchkey").val("Enter text to search YouTube video...");
        $("#searchkey").focus();
        $("#search_video").empty();
        $("#VideoUrlContainer").fadeIn(1000);
        return;
    }

    if (key == null || key.toLowerCase() == "enter text to search youtube video...") {
        //alert("Please enter keywords to search Youtube video");
        $("#VideoUrlContainer").fadeIn(1000);
        return;
    }

    $('#video_search_engine').hide();
    $("#VideoUrlContainer").fadeOut(1000);
    if ($("#search_video").css("height") == "228px") {
        $("#search_video").css("height", "215px");
    }
    $("#search_video").empty();
    $("#loading_vid").show();

    /*var xhr = new XMLHttpRequest();
    xhr.open("GET", BASE_URL + "Handler/GetSearchResults.ashx?engine=youtube&type=video&q=" + document.getElementById("searchkey").value + "&start=" + start, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.responseText.indexOf("Error") > -1) {
                $("#loading_vid").hide();
                //$("#search_results").html(this.responseText);
            }
            else {
                //var res = this.responseText.substring(this.responseText.indexOf('('));
                FillYoutubeSearchResults(eval('(' + this.responseText + ')'));
                $("#loading_vid").hide();
                var objDiv = document.getElementById('search_video');
                objDiv.scrollTop = 0;
            }
        }
    };
    xhr.send(null);*/

    var path = "Handler/GetSearchResults.ashx?engine=youtube&type=video&q=" + document.getElementById("searchkey").value + "&start=" + start;

    CallHandler(path, function (result) {
        if (result.feed.entry != null || result.feed.entry == null) {
            FillYoutubeSearchResults(result);
            $("#loading_vid").hide();
            var objDiv = document.getElementById('search_video');
            objDiv.scrollTop = 0;
        }
        else {
            $("#loading_vid").hide();
            $("#search_video").html("An error occured while searching videos from youtube. <span onClick='SearchImages(1);' class='btn btn-primary'>Refresh</span>");
        }
    });

}

function FillYoutubeSearchResults(data) {

    var ul = $('<ul/>');
    var width = parseInt(510); //parseInt($("#VideoLibraryContent").css("width").replace("px", ""));
    var height = parseInt(387); //parseInt($("#VideoLibraryContent").css("height").replace("px", ""));

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    $("#search_video").empty();

    for (var i = 0; i < data.feed.entry.length; i++) {
        var vid = data.feed.entry[i];
        if (vid != null && typeof (vid) != undefined && vid != undefined) {
            var img = {
                unescapedUrl: vid["media$group"]["media$content"][0].url,
                width: vid["media$group"]["media$thumbnail"][0].width,
                time: vid["media$group"]["media$thumbnail"][0].time,
                height: vid["media$group"]["media$thumbnail"][0].height,
                title: vid.title["$t"],
                tbUrl: vid["media$group"]["media$thumbnail"][0].url,
                video: true,
                duration: vid["media$group"]["media$content"][0].duration
            };
            FillImage(img, ul);
        }

        //var row = $('<div style="clear:left"/>');
        //$(row).append($(ul));

    }

    $("#search_video").css("height", height - 50 + "px");
    $("#search_video").append($(ul));
    //$('#search_video').jScrollPane();
    SetSearchResultButtons("#search_video", "#VideoLibraryContent", "#VideoNavigationButtons", "video");
}

function ResetImagePopup() {
    $("#txtSearchImage").val("");
    $("#searchResult").empty();
    $("#serverImg").empty();
    $("#imgProp").attr("src", "");
    $("#imgProp").attr("alt", "mooc image");
    $("#txtImgWidth").val("");
    $("#txtImgHeight").val("");
    $("#txtAltText").val("");
    $("#txtLink").val("");
    $("#localImg").hide();
}

function ResetVideoPopup() {
    $("#searchkey").val("");
    $("#search_video").empty();
    $("#imgVideoProp").attr("src", "");
    $("#imgVideoProp").attr("alt", "mooc image");
    $("#txtVideoWidth").val("");
    $("#txtVideoHeight").val("");
    $("#txtVideoName").val("");
    $("#txtVideoLongDesc").val("");
}

function SetSearchResultButtons(buttonContainer, libraryContainer, navigationButtons, type) {

    if (buttonContainer == undefined) {
        buttonContainer = $("#searchResult");
        libraryContainer = $("#ImageLibraryContent");
        navigationButtons = $("#NavigationButtons");
    }

    $(navigationButtons).empty();
    $(buttonContainer).attr("class", "scroll-pane scroll-pane-min");
    var nextButton = $("<div/>");
    var previousButton = $("<div/>");

    nextButton.html("Next");
    previousButton.html("Previous");

    nextButton.addClass("btn btn-inverse btn-mini");
    previousButton.addClass("btn btn-inverse btn-mini");

    $(nextButton).css("float", "left").css("margin-left", "5px");
    $(previousButton).css("float", "left").css("margin-left", "10px");

    nextButton.click(function () {
        $(navigationButtons).empty();

        //if (buttonContainer == undefined) {
        if (type == "image") {
            currentImageStartCount = currentImageStartCount + 10;
            SearchImages(currentImageStartCount);
        }
        else {
            currentImageStartCount = currentImageStartCount + 10;
            SearchVideos(currentImageStartCount);
        }

        $(buttonContainer).css("height", $(libraryContainer).css("height"));
    });

    previousButton.click(function () {
        $(navigationButtons).empty();
        //if (buttonContainer == undefined) {
        if (type == "image") {
            currentImageStartCount = currentImageStartCount - 10;
            SearchImages(currentImageStartCount);
        }
        else {
            currentImageStartCount = currentImageStartCount - 10;
            SearchVideos(currentImageStartCount);
        }

        $(buttonContainer).css("height", $(libraryContainer).css("height"));
    });

    if (currentImageStartCount > 10) {
        $(navigationButtons).append(previousButton);
    }
    $(navigationButtons).append(nextButton);
}