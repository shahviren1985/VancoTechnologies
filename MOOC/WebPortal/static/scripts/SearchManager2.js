var videoCounter = 1;
var vimeoUl;
var counter = 0;
function FillYoutubeSearchResults(data) {
    
    var ul = $('<ul/>');
    var width = parseInt($("#VideoLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#VideoLibraryContent").css("height").replace("px", ""));

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    $("#search_results").empty();

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
    
    $("#search_results").css("height", height - 50 + "px");
    $("#search_results").append($(ul));
    $('#search_results').jScrollPane(); 
    SetSearchResultButtons("#search_results","#VideoLibraryContent","#VideoNavigationButtons");
}

function VimeoLoadingThumb(id){    
    var url = "http://vimeo.com/api/v2/video/" + id + ".json?callback=ShowVimeoThumb";
    var script = document.createElement( 'script' );
    
    script.type = 'text/javascript';
    script.src = url;
    document.getElementsByTagName("head")[0].appendChild(script);
}

function ShowVimeoThumb(data){
    var img = {
            unescapedUrl: data[0].url,
            width: data[0].width,
            //time: vid["media$group"]["media$thumbnail"][0].time,
            height: data[0].height,
            title: data[0].title,
            tbUrl: data[0].thumbnail_medium,
            video: true,
            vimeo: true,
            duration: data[0].duration
           };

    FillImage(img,vimeoUl);
    $("#search_results").append($(vimeoUl));
    counter = counter + 1;
    if(counter > 9)
    {
        AddFooterButtonsForVideo();
    }
}

function FillVimeoSearchResults(data) {
    if(data.videos == undefined || data.videos.video == undefined){
        return;
    }
    
    vimeoUl = $('<ul/>');
    var width = parseInt($("#VideoLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#VideoLibraryContent").css("height").replace("px", ""));

    $(vimeoUl).attr("class", "stack");
    $(vimeoUl).attr("data-count", "3");
    $("#search_results").empty();

    for (var i = 0; i < data.videos.video.length; i++) {
        VimeoLoadingThumb(data.videos.video[i].id);
    }

    counter = 0;
    $("#search_results").css("height", height - 50 + "px");
    $("#search_results").append($(ul));
    $('#search_results').jScrollPane(); 
    SetSearchResultButtons("#search_results","#VideoLibraryContent","#VideoNavigationButtons");
}

function AddFooterButtonsForVideo(){
    $("#search_results2").find(".btn-mini").each(function(){
        $(this).remove();
    });
    
    var nextButton = $("<div/>");
    var previousButton = $("<div/>");
    nextButton.html("Next");
    previousButton.html("Previous");

    nextButton.addClass("btn btn-inverse btn-mini");
    previousButton.addClass("btn btn-inverse btn-mini");

    nextButton.click(function () { currentVideoStartCount = currentVideoStartCount + 10; SearchYoutube(currentVideoStartCount); });
    previousButton.click(function () { currentVideoStartCount = currentVideoStartCount - 10; SearchYoutube(currentVideoStartCount); });

    $("#search_results2").append(nextButton);
    if (currentVideoStartCount > 10) {
        $("#search_results2").append(previousButton);
    }
}

function SetSearchEngine(engine){
    $('#image_search_engine').attr('engine',engine);
    $('#image_search_engine').hide();
    SearchImages(1);
    $("#search_results2").css("height",$("#ImageLibraryContent").css("height"));
}

function SetSearchResultButtons(buttonContainer, libraryContainer, navigationButtons){
    
    if(buttonContainer == undefined){
        buttonContainer = "#search_results2";
        libraryContainer = "#ImageLibraryContent";
        navigationButtons = "#NavigationButtons";
    }

    $(navigationButtons).empty();
    $(buttonContainer).attr("class","scroll-pane scroll-pane-min");
    var nextButton = $("<div/>");
    var previousButton = $("<div/>");
    
    nextButton.html("Next");
    previousButton.html("Previous");

    nextButton.addClass("btn btn-inverse btn-mini");
    previousButton.addClass("btn btn-inverse btn-mini");
    $(nextButton).css("float","left").css("margin-left", "5px");
    $(previousButton).css("float","left").css("margin-left", "10px");

    nextButton.click(function () { 
        $(navigationButtons).empty();
        if(buttonContainer == undefined){
            currentImageStartCount = currentImageStartCount + 10; 
            SearchImages(currentImageStartCount);
        }
        else
        {
            currentImageStartCount = currentImageStartCount + 10; 
            SearchVideos(currentImageStartCount);
        }

        
        $(buttonContainer).css("height",$(libraryContainer).css("height"));
    });

    previousButton.click(function () { 
        $(navigationButtons).empty();
        if(buttonContainer == undefined){
            currentImageStartCount = currentImageStartCount - 10; 
            SearchImages(currentImageStartCount);
        }
        else
        {
            currentImageStartCount = currentImageStartCount - 10; 
            SearchVideos(currentImageStartCount);
        }

        $(buttonContainer).css("height",$(libraryContainer).css("height")); 
    });

    if (currentImageStartCount > 10) {
        $(navigationButtons).append(previousButton);
    }
    $(navigationButtons).append(nextButton);
}

function FillGoogleSearchResults(data) {
    
    var isScrollSet = false;
    /*$("#search_results2").find(".jspPane").each(
        function(){ 
            isScrollSet = true;
            $(this).empty();
        }
    );*/

    SetSearchResultButtons();

    var ul = $('<ul/>');
    var width = parseInt($("#ImageLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#ImageLibraryContent").css("height").replace("px", ""));

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    
    if(data == null ||   data.responseData == null || data.responseData.results == null)
        return;
        
    for (var i = 0; i < data.responseData.results.length; i++) {
        var img = data.responseData.results[i];
        if (img != null && typeof (img) != undefined) {
            FillImage(img, ul);
        }
    }
    
    $("#search_results2").css("height", height - 50 + "px");
    //$("#search_results2").css("width", width - 20 + "px");

    $("#search_results2").empty();
    $("#search_results2").append($(ul));
    $('#search_results2').jScrollPane(); 
}

function FillPicassaSearchResults(data) {
    var ul = $('<ul/>');
    var width = parseInt($("#ImageLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#ImageLibraryContent").css("height").replace("px", ""));

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    
    /*
    var isScrollSet = false;
    $("#search_results2").find(".jspPane").each(
        function(){ 
            isScrollSet = true;
            $(this).empty();
        }
    );*/

    SetSearchResultButtons();

    for (var i = 0; i < data.feed.entry.length; i++) {
        
        var vid = data.feed.entry[i];
        if (vid != null && typeof (vid) != undefined && vid != undefined) {
            var img = {
                unescapedUrl: vid["media$group"]["media$content"][0].url,
                width: vid["media$group"]["media$thumbnail"][0].width,
                height: vid["media$group"]["media$thumbnail"][0].height,
                title: vid.title["$t"],
                content: vid.summary["$t"],
                tbUrl: vid["media$group"]["media$thumbnail"][0].url,
                video: false,
            };

            FillImage(img, ul);
        }
    }

    $("#search_results2").css("height", height - 50 + "px");
    //$("#search_results2").css("width", width - 20 + "px");
    /*if(!isScrollSet){
        $("#search_results2").empty();
        $("#search_results2").append($(ul));
        $('#search_results2').jScrollPane(); 
    }
    else{
        $("#search_results2").find(".jspPane").append($(ul));
    }*/

    $("#search_results2").empty();
    $("#search_results2").append($(ul));
    $('#search_results2').jScrollPane(); 
}

function FillImgurSearchResults(data) {
    var ul = $('<ul/>');
    var width = parseInt($("#ImageLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#ImageLibraryContent").css("height").replace("px", ""));

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");

    /*
    var isScrollSet = false;
    $("#search_results2").find(".jspPane").each(
        function(){ 
            isScrollSet = true;
            $(this).empty();
        }
    );
    */
    SetSearchResultButtons();

    for (var i = 0; i < data.responseData.results.length; i++) {
        var img = data.responseData.results[i];
        if (img != null && typeof (img) != undefined) {
            FillImage(img, ul);
        }
    }

    $("#search_results2").css("height", height - 50 + "px");
    $("#search_results2").empty();
    $("#search_results2").append($(ul));
    $('#search_results2').jScrollPane(); 
    
    /*if(!isScrollSet){
        $("#search_results2").empty();
        $("#search_results2").append($(ul));
        $('#search_results2').jScrollPane(); 
    }
    else{
        $("#search_results2").find(".jspPane").append($(ul));
    }*/
    
}

function FillFlickrSearchResults(data) {
    var ul = $('<ul/>');
    var width = parseInt($("#ImageLibraryContent").css("width").replace("px", ""));
    var height = parseInt($("#ImageLibraryContent").css("height").replace("px", ""));

    $(ul).attr("class", "stack");
    $(ul).attr("data-count", "3");
    /*
    var isScrollSet = false;
    $("#search_results2").find(".jspPane").each(
        function(){ 
            isScrollSet = true;
            $(this).empty();
        }
    );*/

    SetSearchResultButtons();

    for (var i = 0; i < data.photos.photo.length; i++) 
    {
        var vid = data.photos.photo[i];
        if (vid != null && typeof (vid) != undefined && vid != undefined) {
            var img = {
                unescapedUrl: "http://farm"+ vid.farm +".static.flickr.com/"+ vid.server +"/"+ vid.id +"_"+ vid.secret +".jpg",
                title: vid.title,
                content: vid.title,
                width: '120px',
                height: '120px',
                tbUrl: "http://farm"+ vid.farm +".static.flickr.com/"+ vid.server +"/"+ vid.id +"_"+ vid.secret +"_m.jpg",
                video: false
            };

            FillImage(img, ul);
        }
    }

    $("#search_results2").css("height", height - 50 + "px");
    $("#search_results2").empty();
    $("#search_results2").append($(ul));
    $('#search_results2').jScrollPane(); 
    /*if(!isScrollSet){
        $("#search_results2").empty();
        $("#search_results2").append($(ul));
        $('#search_results2').jScrollPane(); 
    }
    else{
        $("#search_results2").find(".jspPane").append($(ul));
    }*/
}

function SearchImages(start) {
    $("#search_results2").empty();
    $('#NavigationButtons').html('');
    if(document.getElementById("txtImageSearchKeywords") == null){
        return;
    }

    /*if(!CheckPermissions("search_image")){
        return;
    }*/

    if (document.getElementById("txtImageSearchKeywords").value == "" || document.getElementById("txtImageSearchKeywords").value.toLowerCase() == "enter keywords to search...") {
        $("#ImageUrlContainer").fadeIn(1000);

        $("#search_results2").empty();
        //$("#search_results2").css("height", "190px");
        return;
    }
    document.getElementById('image_search_engine').style.display='none';
    /*var el = document.getElementsByName("ImageSearchEngineOption");
    var searchEngine = "";
    for (var i = 0; i < el.length; i++) {
        if (el[i].checked) {
            searchEngine = el[i].value;
        }
    }*/

    var searchEngine = $('#image_search_engine').attr('engine');
    if(searchEngine == undefined) 
        searchEngine = "Google";

    $("#ImageUrlContainer").fadeOut(1000);
    var height = parseInt($("#search_results2").css("height").replace("px",""));
    
    if(height == 0){
        $("#search_results2").css("height", "400px");
    }
    else
    {
       $("#search_results2").css("height", $("#ImageLibraryContent").css("height"));
    }

    $("#loading_image").show();
    var xhr = new XMLHttpRequest();
    
    xhr.open("GET", "GetSearchResults.ashx?engine=" + searchEngine + "&type=images&q=" + document.getElementById("txtImageSearchKeywords").value + "&start=" + start, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if(this.responseText.indexOf("Error") > -1){
                $("#loading_image").hide();
                $("#search_results2").html(this.responseText);
            } 
            else if (this.responseText != "") {
                switch (searchEngine) {
                    case "Google":
                        FillGoogleSearchResults(eval("(" + this.responseText + ")"));
                        break;
                    case "Picassa":
                        FillPicassaSearchResults(eval("(" + this.responseText + ")"));
                        break;
                    case "Imgur":
                        FillImgurSearchResults(eval("(" + this.responseText + ")"));
                        break;
                    case "Flickr":
                        FillFlickrSearchResults(eval("(" + this.responseText + ")"));
                        break;
                }


                $("#loading_image").hide();
                var objDiv = document.getElementById('search_results2');
                objDiv.scrollTop = 0;
            }
            //FillGoogleSearchResults(eval('(' + this.responseText + ')'));
        }
    };
    xhr.send(null);
}

function SearchVideos(start){
    $('#video_search_engine').hide();
    
     

    var searchEngine = $("#video_search_engine").attr("engine");
    if(searchEngine == undefined)
        searchEngine = "Youtube";
     

    $("#search_results").empty();
    //$("#VideoNavigationButtons").empty();
    switch (searchEngine) {
        case "Youtube":
            SearchYoutube(start);
            break;
        case "Vimeo":
            SearchVimeo(start);
            break;
    }
}

function SearchYoutube(start) {
    var key = $("#searchkey").val();

    if (key == "") {
        $("#searchkey").val("Enter text to search YouTube video...");
        $("#searchkey").blur();
        $("#search_results").empty();
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
    if($("#search_results").css("height")=="228px"){
        $("#search_results").css("height", "215px");
    }
    $("#search_results").empty();
    $("#loading_vid").show();
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "GetSearchResults.ashx?engine=youtube&type=video&q=" + document.getElementById("searchkey").value + "&start=" + start, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if(this.responseText.indexOf("Error") > -1){
                $("#loading_vid").hide();
                //$("#search_results").html(this.responseText);
            } 
            else { 
                //var res = this.responseText.substring(this.responseText.indexOf('('));
                FillYoutubeSearchResults(eval('(' + this.responseText + ')'));
                $("#loading_vid").hide();
                var objDiv = document.getElementById('search_results');
                objDiv.scrollTop = 0;
            }
        }
    };
    xhr.send(null);
}

function SearchVimeo(start){
    var key = $("#searchkey").val();

    if (key == "") {
        $("#searchkey").val("Enter text to search Vimeo video...");
        $("#searchkey").blur();
        $("#search_results").empty();
        $("#VideoUrlContainer").fadeIn(1000);
        return;
    }

    if (key == null || key.toLowerCase() == "enter text to search Vimeo video...") {
        //alert("Please enter keywords to search Youtube video");
        $("#VideoUrlContainer").fadeIn(1000);
        return;
    }
    $('#video_search_engine').hide();
    $("#VideoUrlContainer").fadeOut(1000);
    if($("#search_results").css("height")=="228px"){
        $("#search_results").css("height", "215px");
    }
    $("#search_results").empty();
    $("#loading_vid").show();
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "GetSearchResults.ashx?engine=vimeo&type=video&q=" + document.getElementById("searchkey").value + "&start=" + start, true);
            
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            $('#search_results').html("");
            $("#loading_vid").hide();
            FillVimeoSearchResults(eval('(' + this.responseText + ')'));
            $("#loading_vid").hide();
            var objDiv = document.getElementById('search_results');
            objDiv.scrollTop = 0;
        }
    };
    xhr.send(null);
}

function FillImage(img, ul) {
    var image = $('<img/>');
    var li = $('<li/>');
    var icons = $("<div/>");

    var infoIcon = $("<i/>");
    var attachIcon = $("<i/>");

    $(icons).attr("class", "content-icon-container");
    $(icons).attr("style","margin-left: 35px; float: left");
    $(attachIcon).attr("class", "icon-plus content-icon");

    $(infoIcon).attr("class", "icon-info-sign content-icon");
    
    $(attachIcon).attr("title", "Attach image to current PML");
    $(infoIcon).attr("title", "More information about this image...");
    
    $(image).click(function () { Attach($(image)); });
    $(attachIcon).click(function () { Attach($(image)); });

    //var attach = $('<span/>');
    var title = $('<div/>');
    var description = $('<div/>');

    $(li).attr("class", "ImageItem2");
    
    $(image).attr("class", "SearchResultImage");
    
    if (img == null)
        return;

    $(image).attr("src", img.tbUrl);

    $(image).attr("originalUrl", img.unescapedUrl);
    $(image).attr("width", 150);
    $(image).attr("height", 120);
    if (img.video != undefined && img.video == true) {
        $(image).attr("video", "true");
        var dur = img.duration;
        var duration = "" + Math.round((dur / 3600)) + ":" + Math.round(((dur % 3600) / 60)) + ":" + Math.round((dur % 60));
        $(image).attr("title", img.title.substring(0, 20) + " Duration: " + duration);
        $(image).attr("time", duration);
        title.html(img.title);
    }
    else {
        $(image).attr("video", "false");
        $(image).attr("title", img.title.substring(0, 20));
        
        $(image).attr("originalwidth", img.width);
        $(image).attr("originalheight", img.height);
        title.html(img.titleNoFormatting);
    }
    
    $(image).css({ "cursor": "pointer" });

    title.addClass('SearchResultTitle');
    description.addClass('SearchResultDescription');

    description.html(img.content);

    /*attach.html('Attach');
    attach.append(title);
    attach.append(description);
    attach.addClass('attach');

    $(attach).mouseover(function () { $(this).show(); });
    $(attach).mouseout(function () { $(this).hide(); });
    
    $(image).mouseover(
                function () {
                    $(image).siblings('span').show();
                });
    $(image).mouseout(
                function () {
                    $(image).siblings('span').hide();
                }
            );

    $(attach).click(function () {
        Attach($(image));
    });
    
    $(image).click(function () {

        Attach($(image));
    });
    */
    $(li).append(image);
    $(icons).append(attachIcon);
    $(icons).append(infoIcon);
    $(li).append(icons);
    //$(li).append(attach);
    //$(li).append(title);
    //$(li).append(description);

    $(ul).append(li);
}

function AddVideo(vimeoThumbUrl) {
    var url = $("#youtubeUrl").val();
    var subtabId = GetCurrentOpenContainer().replace("_GenericContent", "_editor");

    if (url == null || url.toLowerCase() == "enter video url..." || url.length < 10) {
        alert("The url doesn't appear to be valid");
        return;
    } else {
        url = ProcessVideoUrl(url);
        if(url == null){
            alert("The url doesn't appear to be valid video url");
            return;
        }

        if(vimeoThumbUrl != undefined){
            $("#" + GetCurrentOpenContainer()).append(GetPPModeYoutubeElement(url, vimeoThumbUrl, null));
        }
        else
        {
            $("#" + GetCurrentOpenContainer()).append(GetPPModeYoutubeElement(url, GetThumbnailUrl(url), null));
        }
    }
}

function ProcessVideoUrl(url) {
    if (url != null && typeof (url) != undefined && url.length > 10) {
        if (url.toLowerCase().indexOf("youtube.com") > -1) {
            url = url.replace("/v/", "/embed/");
            url = url.replace("/watch?v=", "/embed/");
            return url;
        }
        else if (url.toLowerCase().indexOf("vimeo.com") > -1) {
            var videoIdStart = url.lastIndexOf("/");
            url = "http://player.vimeo.com/video/" + url.substring(videoIdStart + 1);
            return url;
        }
        else
        {
            return null;
        }
    }

    return url;
}

function GetThumbnailUrl(url) {
    var type = "";
    if (url.toLowerCase().indexOf("youtube") > -1)
        type = "youtube";
    else if (url.toLowerCase().indexOf("vimeo") > -1)
        type = "vimeo";

    switch (type) {
        case "youtube":
            var id = url.substring(url.indexOf("/embed/") + 7);
            return "http://img.youtube.com/vi/" + id + "/hqdefault.jpg";
            break;
        case "vimeo":
            // TODO: handle thumbnail URL
            return "";
            break;
    }
}

function UpdateYouTubeUrl() {
    var frames = document.getElementsByTagName("iframe");
    for (var i = 0; i < frames.length; i++) {
        frames[i].src += "&wmode=transparent";
    }
}

function AddImage() {
    if (document.getElementById('imageUrl').value == "" || document.getElementById('imageUrl').value.toLowerCase() == "add image url...") {
        alert("Please enter URL to add image");
        return;
    }

    // Clear the previous content.
    document.getElementById('search_results2').innerHTML = "";

    var img = document.createElement("img");
    var attach = document.createElement('span');
    attach.innerHTML = 'Attach';
    attach.setAttribute("class", "attachAddImage");

    img.setAttribute("onError","alert('Image url is not valid. Please enter valid image url');document.getElementById('search_results2').innerHTML=''");
    img.setAttribute("src", document.getElementById('imageUrl').value);
    img.setAttribute("originalUrl", document.getElementById('imageUrl').value);
    img.setAttribute("class", "DragContent ThumbnailImage");
    img.setAttribute("width", "150");
    img.setAttribute("height", "120");
    document.getElementById('search_results2').appendChild(img);
    document.getElementById('search_results2').appendChild(attach);
    img.setAttribute("originalheight", originalImageHeight);
    img.setAttribute("originalwidth", originalImageWidth);

    $(attach).mouseover(function () { $(this).show(); });
    $(attach).mouseout(function () { $(this).hide(); });

    $(img).mouseover(
                function () {
                    $(img).siblings('span').show();
                });

    $(img).mouseout(
                function () {
                    $(img).siblings('span').hide();
                }
            );

    $(img).click(function () {
        Attach(img);
    });

    $(attach).click(function () {
        Attach(img);
    });
}

function Attach(img) {
    var image;
    topElement = img;
    
    
    /*
    if($("#" + GetCurrentOpenContainer()).find(".mceEditor").length > 0){
      var id = $("#" + GetCurrentOpenContainer()).find(".mceEditor").attr("id");
      var ed = tinyMCE.getInstanceById(id.substr(0, id.indexOf("_parent")));
      var img_width = 100;
      var img_height = 100;
      if($(img).attr("originalwidth") != null){
        var orig_width = parseInt($(img).attr("originalwidth"));
        img_height = parseInt($(img).attr("originalHeight")) * 100/orig_width;
      }
      
      ed.setContent(ed.getContent() + "<img src='" + $(img).attr("originalUrl") + "' width='" + img_width + "' height ='" + img_height +"'/>");
      return;
    }*/

    if(currentVideoElement != null){
         
        $(currentVideoElement).find("a").each(function (){
            $(this).attr("OptionalImageUrl", $(img).attr("originalUrl"));
        });

        $(currentVideoElement).find("iframe").each(function (){
            $(this).attr("OptionalImageUrl", $(img).attr("originalUrl"));
        });

        clearInterval(videoOptionalImageInterval);
        videoOptionalImageInterval = null;
        currentOpenDialog = "Video";
        RestoreDefaultIcons();
        ActiveElement(this, 'video_icon');
        ActiveContentTab($('#VideoMenuOptions').find('li:first'));
        HideImageMenuOptions();
        currentVideoElement = null;
        $("#OptionalVideoText1").hide();
        $("#OptionalVideoText2").hide();
        $("#OptionalVideoText3").css("visibility", "hidden");
        GetPreview();
        return;
    }

    var subtabId = "";//GetCurrentOpenContainer().replace("_GenericContent", "_editor");
    UpdateMediaCoordinates();
    DeselectMediaElements();
    var elem;
    if ($(img).attr("video") == "true") {
        if($(img).attr("originalUrl").indexOf("vimeo.com")>-1){
            $("#youtubeUrl").val($(img).attr("originalUrl"));   
            AddVideo($(img).attr("src"));
            $("#youtubeUrl").val("");
        }
        else{
            var index = $(img).attr("originalUrl").indexOf("?");
            var url;
            if(index > 0){
                url = $(img).attr("originalUrl").substring(0, index).replace("/v/", "/embed/");
            }
            else{
                url = $(img).attr("originalUrl");
            }
            elem = $("#" + GetCurrentOpenContainer()).append(GetPPModeYoutubeElement(url, $(img).attr("src"), $(img).attr("time"), img));
        }
    }
    else if ($(img).attr("type") == "video") {
        elem = $("#" + GetCurrentOpenContainer()).append(GetPPModeVideoElement($(img).attr("url"), $(img).attr("source"), img));
        //$("#" + GetCurrentOpenContainer()).append(GetPPModeYoutubeElement($(img).attr("url"), $(img).attr("source")));
    }
    else if ($(img).attr("type") == "document") {
        elem = $("#" + GetCurrentOpenContainer()).append(GetPPModeDocumentElement($(img).attr("url"), $(img).attr("source")));
    }
    else {
        elem = null;//$("#" + GetCurrentOpenContainer()).append(GetPPModeImageElement(img));
    }

    GetPreview();
    return elem;
}

function GetPPModeYoutubeElement(url, src, time, img) {
    var element = document.createElement("div");
    var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");
    var caption = $(img).attr("caption");
    var link = $(img).attr("link");
    var zIndex = $(img).attr("zi");

    if(zIndex == null ||
    zIndex == "undefined")
    zIndex = UpdateZIndexCounter();
    var imagename = $(img).attr("imagename");
    if(autoplay == null || autoplay == "undefined")
      autoplay = "";
     if(link == null || link == "undefined")
        link="";

        var autoplay = "";
      if($(img).attr("pmlautoplay") == "true" ||
        $(img).attr("pmlautoplay") == "autoplay")
        autoplay = " pmlautoplay='true' ";
        if($(img).attr("pmlmute") == "true")
          autoplay += " pmlmute='true' "; 
    var autoRedirect = $(img).attr("pmlautoredir");
    var autoredir = "";
    if(autoRedirect == "true")
      {
        autoredir= " autoredir='true' ";
      }
    if(IsFirstMediaItem() || (img != null && $(img).attr("background") == "true"))
    {    
        $(element).html("<div caption='" + caption + "' link='" + link + "' class='ImageDeleteOptions' background='true' style='position: absolute; top: 0px;left: 0px; border: inherit; cursor: default; z-index: 1; width: 100%;height: 100%;'><iframe  " + autoplay + " class='resizeiframe' style='width:100%;height:100%' src='" + url + "?wmode=opaque" + "' time='"+time+"' " + autoredir + "></iframe></div>");
        $(element).find('.resizeiframe').load( function() {
            PrepareImageOptions(this.parentNode,"video");
            SetVideoBackground(this.parentNode);
              if($(this).attr("pmlautoplay") == "true" ||
              $(this).attr("pmlautoplay") == "autoplay"){
                $(this).parent(".ImageDeleteOptions").find("#chkAutoPlay").attr("checked", "true");
              }
              
              if($(this).attr("pmlmute") == "true"){
                $(this).parent(".ImageDeleteOptions").find("#chkMute").attr("checked", "true");
              }
              
              if($(this).parent().attr("caption") != "undefined")
                $(this).parent().find("#txtImageCaption").val($(this).parent().attr("caption"));
              if($(this).parent().attr("link") != "undefined")
                $(this).parent().find("#txtImageLink").val($(this).parent().attr("link"));
                
               if($(this).attr("autoredir") == "true"){
                $(this).parent(".ImageDeleteOptions").find("#chkAutoRedirect").attr("checked", "true");
              }     
            });
    }
    else
    {        
        $(element).html("<div caption='" + caption + "' link='" + link + "'  class='ImageDeleteOptions' style='position: absolute; top:" + GlobalTop + "px;left:" + GlobalLeft + "px;width: 300px;height: 150px; padding:6px;cursor:move;z-index:" + zIndex + "'><iframe " + autoplay + " class='resizeiframe' style='width:100%;height:100%' src='" + url + "?wmode=opaque" + "' time='"+time+"' " + autoredir + "></iframe></div>");
        $(element).find('.resizeiframe').each(function(){
            $(this).load(function(){
                // $(this).parent().resizable({iframeFix: true});
                var width = $("#" + GetCurrentOpenContainer()).css("width").replace("px", "");
                var height = $("#" + GetCurrentOpenContainer()).css("height").replace("px", "");
                var top = $(this).parent().css("top").replace("px", "");
                var left = $(this).parent().css("left").replace("px", "");
                var maxWidth = width - left;
                var maxHeight = height - top;
                $(this).parent().resizable({
                        maxWidth: maxWidth,
                        maxHeight: maxHeight
                    });

                $(this).parent().draggable({containment: $(this).parents(".SubTabContent"), iframeFix:true });
                PrepareImageOptions(this.parentNode,"video");
                HighlightImage(this.parentNode);
                if($(this).parent().attr("caption") != "undefined")
                        $(this).parent().find("#txtImageCaption").val($(this).parent().attr("caption"));
                    if($(this).parent().attr("link") != "undefined")
                            $(this).parent().find("#txtImageLink").val($(this).parent().attr("link"));
                    
                if($(this).attr("pmlautoplay") == "true" ||
                  $(this).attr("pmlautoplay") == "autoplay"){
                    $(this).parent(".ImageDeleteOptions").find("#chkAutoPlay").attr("checked", "true");
                }
                if($(this).attr("pmlmute") == "true"){
                  $(this).parent(".ImageDeleteOptions").find("#chkMute").attr("checked", "true");
                }
              
                if($(this).attr("autoredir") == "true"){
                  $(this).parent(".ImageDeleteOptions").find("#chkAutoRedirect").attr("checked", "true");
                } 
            });                
        });
    }

    //TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] = TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] + 1;
    AddMediaIteminGlobalArray(element, null, src);
    return element;
}

function GetPPModeImageElement(img) {
    var element = document.createElement("div");
    var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");
    var path = $(img).attr("originalUrl");

    
    if(path.indexOf(".swf") > -1){
        return GetFlashElement(img);
    }
    else{
     var caption = $(img).attr("caption");
     var link = $(img).attr("link");
    var imagename = $(img).attr("imagename");
    var description = $(img).attr("description");
    var zIndex = $(img).attr("zi");
    if(zIndex == null ||
    zIndex == "undefined")
      zIndex = UpdateZIndexCounter();
     if(caption == null || caption == "undefined")
        caption="";
    
     if(link == null || link == "undefined")
        link="";

     if(imagename == null || imagename == "undefined")
        imagename="";

     if(description == null || description == "undefined")
        description="";

        
        $(img).attr("originalwidth", img.naturalWidth);
        $(img).attr("originalHeight", img.naturalHeight);

        if(IsFirstMediaItem() || $(img).attr("background") == "true")
        {

            $(element).html("<div class='ImageDeleteOptions' background='true' style='position: absolute; top: 0px;left: 0px; border: inherit; cursor: default;width: 100%;height: 100%;'><img class='resizeimage' src='" + $(img).attr("originalUrl") + "' width='100%' height='100%' originalwidth='" + $(img).attr("originalwidth") + "' originalheight='" + $(img).attr("originalheight") + 
                            "' caption='" + caption  + "' link='" + link + "' imagename='"+imagename+"' description='"+description+"'  starttime='" + $(img).attr("starttime") + "' endtime='" + $(img).attr("endtime") + "' /></div>");
            $(element).find('.resizeimage').each(function(){
                $(this).attr("opform", $(img).attr("opform"));                
                $(this).attr("op-top", $(img).attr("op-top"));
                $(this).attr("op-left", $(img).attr("op-left"));
                $(this).attr("op-w", $(img).attr("op-w"));
                $(this).attr("op-h", $(img).attr("op-h"));
                $(this).attr("op-title", $(img).attr("op-title"));
                if($(img).attr("op-opts") != null){
                    $(this).attr("op-opts", $(img).attr("op-opts"));
                    var count = parseInt($(img).attr("op-opts"));
                    for(var op = 0; op < count; op++){
                        $(this).attr("op-"+op, $(img).attr("op-" + op));
                    }
                }
            $(this).load(
                function(){ 
                    DefaultBackgroundMediaItem($(this)[0]);
                    if($(this).attr("caption") != "undefined")
                        $(this).parent().find("#txtImageCaption").val($(this).attr("caption"));
                    if($(this).attr("link") != "undefined")
                            $(this).parent().find("#txtImageLink").val($(this).attr("link"));
                    var st = $(this).attr("starttime");
                    if(st != null && st != "undefined"){
                        var parts = st.split(":");                        
                        var startTime = parseFloat(parts[0]) * 60 + parseFloat(parts[1]);
                        var end = $(this).attr("endtime");
                        var endTime = 305;
                        parts = end.split(":");
                        if(parts.length == 2)
                            endTime = parseFloat(parts[0]) * 60 + parseFloat(parts[1]);
                        $(this).parent().find("#SliderRange").slider("values", 0, startTime);
                        $(this).parent().find("#SliderRange").slider("values", 1, endTime);
                        var left = $($(this).parent().find("#SliderRange").find("a")[0]).css("left");
                        var top = $($(this).parent().find("#SliderRange").find("a")[0]).css("top");
                        $(this).parent().find("#PmlStartTimeHandle").css({left: left});
                        $(this).parent().find("#PmlStartTimeHandle").html(st);
                        
                        left = $($(this).parent().find("#SliderRange").find("a")[1]).css("left");
                        top = $($(this).parent().find("#SliderRange").find("a")[1]).css("top");
                        $(this).parent().find("#PmlEndTimeHandle").css({left: left});
                        $(this).parent().find("#PmlEndTimeHandle").html(end);
                        if($(this).attr("opform") == "true" && !EDIT_MODE){
                            AddOptInForm($(this).parent().find(".ImageOption")[0], $(this));
                            var form = $(this).parent().find(".PmlFormContainer");
                            $(form).resizable('destroy');
                            $(form).draggable('destroy');
                            $(form).width($(this).attr("op-w"));
                            $(form).height($(this).attr("op-h"));
                            $(form).css({position:"absolute", top: $(this).attr("op-top") + "px", left: $(this).attr("op-left") + "px"});
                            $(form).resizable({containment: $(this)});
                            $(form).draggable({containment: $(this)});
                        }
                    }
                });
                $(this).error(function(){
                    $(this).resizable({containment: "#" + GetCurrentOpenContainer()});
                    $($(this)[0].parentNode.parentNode).draggable({containment: $(this).parents(".SubTabContent")});
                    PrepareImageOptions($(this)[0].parentNode,"image");
                });
            });                
        }
        else
        {
            var top = GlobalTop;
            var left = GlobalLeft;

            if($(img).attr("pos-top") != undefined && $(img).attr("pos-top") != "undefined"){
                    top = $(img).attr("pos-top");
                    top = top.replace('px', '');
            }
           
            if($(img).attr("pos-left") != undefined && $(img).attr("pos-left") != "undefined")
            {
                 left  = $(img).attr("pos-left");
                 left = left.replace('px', '');
            }
            
        
        $(element).html("<div class='ImageDeleteOptions' style='display: none; position: absolute; top:" + top + "px;left:" + left + "px;cursor:move; z-index:" + zIndex + "'><img class='resizeimage' src='" + $(img).attr("originalUrl") + "' originalwidth='" + $(img).attr("originalwidth") + "' originalheight='" + $(img).attr("originalheight") + 
                    "' caption='" + $(img).attr("caption") + "' link='" + $(img).attr("link") + "' imagename='"+$(img).attr("imagename")+"' description='"+$(img).attr("description")+"' starttime='" + $(img).attr("starttime") + "' endtime='" + $(img).attr("endtime") + "' pos-w='" + $(img).attr("width") + "' pos-h='" + $(img).attr("height") + "' pos-top='" + $(img).attr("pos-top") + "' pos-left='" + $(img).attr("pos-left") + "'/></div>");
            
            $(element).find('.resizeimage').each(function(){
                
                if($(this).attr("pos-left") != "undefined"){
                    $(this).css("width", $(img).attr("originalwidth") );
                    $(this).css("height", $(img).attr("originalheight"));
                    $(this).position({ left: $(img).attr("pos-left"), top: $(img).attr("pos-top") });
                }

                if($(this).attr("pos-w") != undefined){
                    $(this).css("width", $(img).attr("pos-w"));
                    $(this).css("height", $(img).attr("pos-h"));
                }

                $(this).load(
                    function(){
                        pmlOnImageLoaded($(this));

                    });
                
                $(this).error(function(){
                    AdjustImageSize($(this)[0]);
                    $(this).resizable({containment: "#" + GetCurrentOpenContainer()});
                    $($(this)[0].parentNode.parentNode).draggable({containment: $(this).parents(".SubTabContent")});
                    PrepareImageOptions($(this)[0].parentNode,"image");
                });
            });
        }

        //TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] = TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] + 1;
        AddMediaIteminGlobalArray(element, img, null);
        return element;
    }
}
function pmlOnImageLoaded(img){

    if($(img).attr("pos-left") != "undefined")
    {                     
        $(img).parent(".ImageDeleteOptions").show();
    }else{
        
        AdjustImageSize($(img)[0]);
    }
    
    $($(img)[0].parentNode).draggable({containment: $(img).parents(".SubTabContent") });
    PrepareImageOptions($(img)[0].parentNode,"image");
    
    var width = $("#" + GetCurrentOpenContainer()).css("width").replace("px", "");
    var height = $("#" + GetCurrentOpenContainer()).css("height").replace("px", "");
    var top = $(img).parent().parent().css("top").replace("px", "");
    var left = $(img).parent().parent().css("left").replace("px", "");
    var maxWidth = width;
    var maxHeight = height;
    
    if(top != "auto" && left != "auto"){
        maxWidth = width - left;
        maxHeight = height - top;
    } 

    $(img).parent().resizable({ maxWidth: maxWidth, maxHeight: maxHeight});
    
    //alert(GetCurrentOpenContainer());
    if($(img).attr("caption") != "undefined")
        $(img).parent().find("#txtImageCaption").val($(img).attr("caption"));
    if($(img).attr("link") != "undefined")
        $(img).parent().find("#txtImageLink").val($(img).attr("link"));
    var st = $(img).attr("starttime");
    if(st != "undefined" && st != null){
        var parts = st.split(":");
        var startTime = parseFloat(parts[0]) * 60 + parseFloat(parts[1]);
        var end = $(img).attr("endtime");
        var endTime = 305;
        parts = end.split(":");
        if(parts.length == 2)
            endTime = parseFloat(parts[0]) * 60 + parseFloat(parts[1]);
        $(img).parent().parent().find("#SliderRange").slider("values", 0, startTime);
        $(img).parent().parent().find("#SliderRange").slider("values", 1, endTime);
        var left = $($(img).parent().parent().find("#SliderRange").find("a")[0]).css("left");
        var top = $($(img).parent().parent().find("#SliderRange").find("a")[0]).css("top");
        $(img).parent().parent().find("#PmlStartTimeHandle").css({left: left});
        $(img).parent().parent().find("#PmlStartTimeHandle").html(st);
                        
        left = $($(img).parent().parent().find("#SliderRange").find("a")[1]).css("left");
        top = $($(img).parent().parent().find("#SliderRange").find("a")[1]).css("top");
        $(img).parent().parent().find("#PmlEndTimeHandle").css({left: left});
        $(img).parent().parent().find("#PmlEndTimeHandle").html(end);                        
    }
}
function GetFlashElement(img){
    var element = document.createElement("div");
    var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");
    
    var path = $(img).attr("originalUrl");
    var width = $(img).attr("width");
    var height = $(img).attr("height");

    /*if(IsFirstMediaItem())
    {
        width = "100%";
        height = "100%";
    }*/
    
     var obj = "<div class='ImageDeleteOptions' style='position: absolute; top:" + GlobalTop + "px;left:" + GlobalLeft + "px;cursor:move; z-index:" + UpdateZIndexCounter() + "'>" +
     "<object codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab' width='" + width + "' height='" + height + "' class='resizeimage'>" +
	"<param name='wmode' value='transparent'>" +
	"<param name='movie' value='" + path + "'>" +
	"<embed src='" + path + "' quality='high' pluginspage='http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width='" + width + "' height='" + height + "' wmode='transparent'>" + 
	"<param name='wmode' value='transparent'></object></div>";

    $(element).html(obj);
    $(element).find(".ImageDeleteOptions").each(function(){
        $(this).mouseover(function(){
            if($(this).attr("optionsset")==undefined){
                $(this).attr("optionsset","true");
                $(this).draggable({containment: $(this).parents(".SubTabContent") });
                PrepareImageOptions(this,"flash");
            } 
        });
    });
    //TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] = TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] + 1;
    AddMediaIteminGlobalArray(element, img, null);
    return element;
}

function AdjustImageSize(image){
    if(IsImageBiggerThanWindow(image)){
        var aspectRatio = image.naturalWidth / image.naturalHeight;
        CalculateNewSizeForImage(image, aspectRatio);
    }
    else
    {
        /*if(image.naturalWidth > 0){
            $(image).css("width",image.naturalWidth);
            $(image).css("height",image.naturalHeight);
            $(image).parent().css("width",image.naturalWidth);
            $(image).parent().css("height",image.naturalHeight);
        }*/
    }

    SetImageCoordinates(image);
    $(image).parent().css("display","block");
}

function SetImageCoordinates(image){    
    var imageContainer = image.parentNode;
    var windowWidth = parseInt($("#" + GetCurrentOpenContainer()).css("width").replace("px"));
    var windowHeight = parseInt($("#" + GetCurrentOpenContainer()).css("height").replace("px"));
    
    var posX =   Math.ceil((windowWidth - parseInt($(image).css("width").replace("px"))- 20) /2);
    var posY = Math.ceil(( windowHeight - parseInt($(image).css("height").replace("px"))- 20) /2);
    
    $(imageContainer).css("top",Math.floor(Math.random()*posY) + "px");
    $(imageContainer).css("left",Math.floor(Math.random()*posX) + "px");
        
}

function CalculateNewSizeForImage(image, aspectRatio){
    var windowWidth = parseInt($("#" + GetCurrentOpenContainer()).css("width").replace("px"));
    var windowHeight = parseInt($("#" + GetCurrentOpenContainer()).css("height").replace("px"));

    var windowImageWidthRatio = image.naturalWidth/windowWidth  ;
    var windowImageHeightRatio = image.naturalHeight/windowHeight;
    var isSizeMatched = false;

    if(windowImageWidthRatio > 1 || windowImageHeightRatio > 1){
        
        // Image does not fit into the window, need to resize it
        var previousSize = { Width: image.naturalWidth, Height: image.naturalHeight}; 
        while(!isSizeMatched){
            var previousSize = GetNewSize(previousSize, aspectRatio);
            
            if(windowWidth > previousSize.Width && windowHeight > previousSize.Height){
                isSizeMatched = true;            
            }
        }
        
        $(image).css("width",previousSize.Width);
        $(image).css("height",previousSize.Height);
    }
}

function GetNewSize(previousSize, aspectRatio){
    previousSize.Width = Math.ceil (previousSize.Width * 0.66);
    previousSize.Height = Math.ceil(previousSize.Width * aspectRatio);
    return previousSize;
}

function IsImageBiggerThanWindow(image){
    var windowWidth = parseInt( $("#" + GetCurrentOpenContainer()).css("width").replace("px"));
    var windowHeight = parseInt($("#" + GetCurrentOpenContainer()).css("height").replace("px"));
    
    if(windowWidth < image.naturalWidth){
        return true;
    }
    else if(windowHeight < image.naturalHeight){
        return true;
    }
    
    return false;
}

function GetPPModeVideoElement(posterPath, source, img) {
    var element = document.createElement("div");
    var startIndex = GetCurrentOpenContainer().indexOf("_SubTab");

    var caption = $(img).attr("caption");
    var videoname = $(img).attr("videoename");
    var description = $(img).attr("description");
    var link = $(img).attr("linkurl");
   
   var autoplay = "";
    if($(img).attr("pmlautoplay") == "true" ||
       $(img).attr("pmlautoplay") == "autoplay")
        autoplay = " pmlautoplay='true' ";

     if(caption == null || caption == "undefined")
        caption="";
    
     if(link == null || link == "undefined")
        link="";

     if(videoname == null || videoname == "undefined")
        videoname="";

     if(description == null || description == "undefined")
        description="";


    if(IsFirstMediaItem()|| (img != null && $(img).attr("background") == "true"))
    {
        $(element).html("<div caption='"+ caption+"' link='"+link+"' description='"+description+"' videoname='"+videoname+"' class='ImageDeleteOptions' background='true'  style='position: absolute; top: 0px;left: 0px;width: 100%;height: 100%;  border: inherit; cursor: default; z-index: 1;'><a id='pmlvid_"+videoCounter+"' style='display: none' href='" + source + "'></a><img " + autoplay + " onclick='PlayVideo(\"pmlvid_"+videoCounter+"\",this)'" + "class='resizevideo' width='100%' height='100%' src='"+posterPath + "'></img></div>");
        $(element).find('.resizevideo').each(function(){
            $(this).load(
                function(){ 
                    PrepareImageOptions(this.parentNode,"video");
                    SetVideoBackground(this.parentNode);
                    if($(this).parent().attr("caption") != "undefined")
                        $(this).parent().find("#txtImageCaption").val($(this).parent().attr("caption"));
                    if($(this).parent().attr("link") != "undefined")
                            $(this).parent().find("#txtImageLink").val($(this).parent().attr("link"));
                    
                if($(this).attr("pmlautoplay") == "true" ||
                  $(this).attr("pmlautoplay") == "autoplay"){
                    $(this).parent(".ImageDeleteOptions").find("#chkAutoPlay").attr("checked", "true");
                }
                });
                $(this).error(function(){
                    $(this).resizable({containment: "#" + GetCurrentOpenContainer()});
                    $($(this)[0].parentNode.parentNode).draggable({containment: $(this).parents(".SubTabContent")});
                    PrepareImageOptions($(this)[0].parentNode.parentNode,"video");
                });                
            });  
    }
    else
    {
        var top = GlobalTop;
        if($(img).attr("pos-top") != undefined &&
            $(img).attr("pos-top") != "undefined")
            top = $(img).attr("pos-top");
        var left = GlobalLeft;
        if($(img).attr("pos-left") != undefined &&
            $(img).attr("pos-left") != "undefined")
            left  = $(img).attr("pos-left");
        var width = 300;
        if($(img).attr("pos-w") != undefined &&
            $(img).attr("pos-w") != "undefined")
            width = $(img).attr("pos-w");
        var height = 150;
        if($(img).attr("pos-h") != undefined &&
            $(img).attr("pos-h") != "undefined")
            height = $(img).attr("pos-h");
        $(element).html("<div caption='"+ caption+"' link='"+link+"' description='"+description+"' videoname='"+videoname+"' class='ImageDeleteOptions' style='position: absolute; top:" + top + "px;left:" + left + "px;width:"+width +"px;height:"+ height +"px; padding:6px;cursor:move;z-index:" + UpdateZIndexCounter() + "'><a id='pmlvid_"+videoCounter+"' style='display: none' href='" + source + "'></a><img " + autoplay + " onclick='PlayVideo(\"pmlvid_"+videoCounter+"\",this)'" + "class='resizevideo' width='100%' height='100%' src='"+posterPath + "'></img></div>");
        $(element).find('.resizevideo').each(function(){
                $(this).load(
                    function()
                    {
                    var ele = $(this).parent();
                    var width = $("#" + GetCurrentOpenContainer()).css("width").replace("px", "");
                    var height = $("#" + GetCurrentOpenContainer()).css("height").replace("px", "");
                    var top = $(this).parent().css("top").replace("px", "");
                    var left = $(this).parent().css("left").replace("px", "");
                    var maxWidth = width - left;
                    var maxHeight = height - top;
                    $(ele).resizable({
                        maxWidth: maxWidth,
                        maxHeight: maxHeight
                    });
                    $(this).parent().draggable({containment: $(this).parents(".SubTabContent")});
                    PrepareImageOptions($(this)[0].parentNode,"video");
                    if($(this).parent().attr("caption") != "undefined")
                        $(this).parent().find("#txtImageCaption").val($(this).parent().attr("caption"));
                    if($(this).parent().attr("link") != "undefined")
                            $(this).parent().find("#txtImageLink").val($(this).parent().attr("link"));
                    
                if($(this).attr("pmlautoplay") == "true" ||
                  $(this).attr("pmlautoplay") == "autoplay"){
                    $(this).parent(".ImageDeleteOptions").find("#chkAutoPlay").attr("checked", "true");
                }
                });
                $(this).error(function(){
                    $(this).resizable({containment: "#" + GetCurrentOpenContainer()});
                    $(this).parent().draggable({containment: $(this).parents(".SubTabContent")});
                    PrepareImageOptions($(this)[0].parentNode,"video");
                });
            });
    }
    videoCounter++;
    //TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] = TotalElements[GetCurrentOpenContainer().substr(startIndex - 1, 1)] + 1;
    AddMediaIteminGlobalArray(element, null, posterPath);
    return element;
}

function GetDocumentElement(thumbnailPath, source) {
    return "<div style='position: relative'><div class='ImageDeleteOptions' style='position: absolute; padding: 5px;background-color: #CCC;cursor:move;z-index:" + UpdateZIndexCounter() + "'' onclick='SetZIndex(this);' ondrag='SetZIndex(this)' onmousedown='SetZIndex(this)'><a href='" + source + "'><img src='" + thumbnailPath + "' height='40px' width='50' /></a></div></div>";
}

function UpdateMediaCoordinates() {
    GlobalTop = (GlobalTop < 80) ? (GlobalTop + 20) : 20;
    GlobalLeft = (GlobalLeft < 190) ? (GlobalLeft + 20) : 0;
}

function UpdateTextCoordinates() {
    TextTop = (TextTop < 110) ? (TextTop + 45) : 45;
    TextLeft = (TextLeft < 190) ? (TextLeft + 45) : 0;
}

function AddMediaIteminGlobalArray(element, img, src) {
    if (element != null && element != undefined) {
        /*if (Global_Item_List[currentTabIndex] == undefined) {
            Global_Item_List[currentTabIndex] = [];
        }

        Global_Item_List[currentTabIndex].push(element.childNodes[0]);*/

        
        var url = $(img).attr("originalUrl");
        var num;
        if (img != null) {
            if(url.indexOf(".swf")>-1){
                num = AddThumbnail($(img).attr("src"));
            }
            else{
                num = AddThumbnail($(img).attr("originalUrl"));
            }
        }
        else if (src != null) {
            num = AddThumbnail(src);
        }
        $(element).attr("itemnumber", "" + num);
        $(element).css("zIndex", $(element).children().first().css("zIndex"));
    }
}
function AddOptinIteminGlobalArray(form)
{
    if (form != null && form != undefined) {
        var num = AddOptinThumbnail(form);
        $(form).attr("itemnumber", "" + num);
    }
}
function AddTextIteminGlobalArray(element) {
    if (element != null && element != undefined) {
        /*if (Global_Item_List[currentTabIndex] == undefined) {
            Global_Item_List[currentTabIndex] = [];
        }
        Global_Item_List[currentTabIndex].push(element);*/
        var num = AddTextElement(element);
        $(element).attr("itemnumber", "" + num);
    }
}
function PlayVideo(id,element) {
    $(element).css("display", "none");
    $(element).siblings("a").css("display", "block");
    flowplayer(id, {src: SERVER_BASE + "static/scripts/flowplayer-3.2.8.swf",wmode: "transparent"});
}