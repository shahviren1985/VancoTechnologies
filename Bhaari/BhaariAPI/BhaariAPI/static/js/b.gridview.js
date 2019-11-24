var GetJobTile = function (jobDetails) {
    // Header - Logo, Company Name, Region, Date Posted
    // Content - Job Descrition - which fits in the tile w/scroll?
    // Footer - Source, Apply now
    var tile = $("<div/>").addClass("job-tile");
    $(tile).append(GetJobTileHeader(jobDetails)).append(GetJobTileContent(jobDetails)).append(GetJobTileFooter(jobDetails));
    return $(tile);
}

var GetMapView = function () {
    HideJobGrid();
    trackMapView();
    //google.maps.event.addDomListener(window, 'load', init);
    $("#map").show();
    if (isMobile) {
        $(".left-nav").hide();
        $(".mobile-view #map-view").hide();
        $(".mobile-view #grid-view").show();
        $(".Note").show();
    }

    $("#grid-view").removeClass("active-view");
    $("#map-view").addClass("active-view");
};

var GetJobGrid = function () {
    $(".left-nav").show();
    $(".Loading-map").hide();
    $("#map").hide();
    $("#grid").hide();
    $("#grid-view").addClass("active-view");
    $("#map-view").removeClass("active-view");

    if (isMobile) {
        $(".mobile-view #map-view").show();
        $(".mobile-view #grid-view").hide();
        $(".Note").hide();
    }

    $(".slider-label,.icon-chevron-left,.icon-chevron-right").show();
    $("#slider").show();
    $(".slider-label").show();
    $(".slider-label").show();
}

var GetJobGrid2 = function () {
    ShowJobGrid();
    trackListView();
    var JOB_LIST = JSON.parse($("#hfJobs").val());
     
    for (i = 0; i < JOB_LIST.length; i++) {
        $("#grid").append(GetJobTile(JOB_LIST[i]));
    }
}

function GetJobTileHeader(json) {
    var address = JSON.parse(json.Location);
    var region = "";
    var logo = json.CompanyLogo != null ? json.CompanyLogo : "default.jpg";
    if (address && address[0].Street != null && address[0].City != null && address[0].PostalCode != null)
        region = address[0].Street + ", " + address[0].City + ", " + address[0].PostalCode;

    var headerContainer = $("<div/>").attr("class", "tile-header-container");
    var logoContainer = $("<div/>").attr("class", "tile-logo-container");
    var logoImg = $("<img/>").attr("class", "company-logo").attr("src", "logo/" + logo);
    var companyName = $("<div/>").attr("class", "company-name").html(json.CompanyName);
    var companyAddress = $("<div/>").attr("class", "company-address").html(region);

    //var companyWebsite = $("<div/>").attr("class", "company-website").html(json.WebsiteURL);

    //$(logoContainer).append(logoImg);
    $(headerContainer).append(logoContainer);
    $(headerContainer).append(companyName);
    /*if (region != "")
        $(headerContainer).append(companyAddress);*/
    //$(headerContainer).append(companyWebsite);

    return $(headerContainer);
}

function GetJobTileContent(json) {
    var jobContainer = $("<div/>").attr("class", "job-tile-container");
    var jobTitle = $("<div/>").attr("class", "job-tile-title").html("<b>Job Title:</b> " + json.JobTitle);

    var jobDescription = $("<div/>").attr("class", "job-desc").append(json.JobDescription.replace(/€™/g, "'").replace(/€˜/g, "\"").replace(/Â/g, "").replace(/â/g, "").replace(/€¢/g, "").replace(/€“/g, "").replace(/€/g, "\""));

    $(jobContainer).append(jobTitle);
    $(jobContainer).append(jobDescription);
    //$(jobContainer).slimScroll({ height: '380px', width: '100%' });
    return $(jobContainer);
}

function GetJobTileFooter(json) {
    var jobSource = $("<div/>").attr("class", "tile-job-source").html("Source: ");
    var jobSourceUrl;
    
    if (json.sourceName == "Indeed")
        jobSourceUrl = $("<a/>").attr("class", "job-source-url").html("<a link=\"" + json.SourceUrl + "\" onclick=\"CheckUserSignup();trackJobLink('" + json.SourceUrl + "')\" target=\"_blank\"><img src=\"http://www.indeed.com/p/jobsearch.gif\" style=\"border: 0;\" alt=\"Powered by Indeed Job Search\"></a>");
    else if (json.sourceName == "Glassdoor")
        jobSourceUrl = $("<a/>").attr("class", "job-source-url").html("<a link=\"" + json.SourceUrl + "\" onclick=\"CheckUserSignup();trackJobLink('" + json.SourceUrl + "')\" target=\"_blank\"></a>");
    else
        jobSourceUrl = $("<a/>").attr("class", "job-source-url").html("<a link=\"" + json.SourceUrl + "\" onclick=\"CheckUserSignup();trackJobLink('" + json.SourceUrl + "')\" target=\"_blank\">" + json.SourceName + "</a>");

    var apply = $("<div/>").attr("class", "job-apply btn").html("<a link=\"" + json.SourceUrl + "\" onclick=\"CheckUserSignup();trackJobLink('" + json.SourceUrl + "')\" target=\"_blank\" style=\"color: white\">Apply</a>");

    $(jobSource).append(jobSourceUrl);
    $(jobSource).append(apply);
    return $(jobSource);
}

var ShowJobGrid = function () {
    // show all grid related components
    // hide all the map related components
    $(".left-nav").hide();
    $(".Loading-map").hide();
    $("#map").hide();//removeClass("active-view")
    $("#grid").show();//.addClass("active-view")
    $("#map-view").removeClass("active-view");
    $("#grid-view").addClass("active-view");

    $(".slider-label,.icon-chevron-left,.icon-chevron-right").hide();
    $("#slider").hide();
    $(".slider-label").hide();
    $(".slider-label").hide();
}

var HideJobGrid = function () {
    $(".left-nav").show();
    $(".Loading-map").hide();
    $("#map").show();
    $("#grid").hide();
    $("#map-view").addClass("active-view");
    $("#grid-view").removeClass("active-view");

    $(".slider-label,.icon-chevron-left,.icon-chevron-right").show();
    $("#slider").show();
    $(".slider-label").show();
    $(".slider-label").show();
}