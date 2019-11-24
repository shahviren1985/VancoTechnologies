var BASE_URL = "http://localhost:6328/";
//var BASE_URL = "http://adminapps.in/course/";
//var BASE_URL = "http://moocacademy.in/"; //for mooc
//var BASE_URL = "http://myclasstest.com/mooc/"; //for godady


// creating ChapterIndex
var Chapters = [];
var Secions = [];


var chapterId = 0;
var sectionId = 0;
var courseId = 0;

//-----------------populating Chapters and Sections accourding to the course-------------------
//function PopulateChaptersByCourse(courseId) {
function PopulateChaptersByCourse(courseId, CallFunctionAfterChaptersSectionsLoaded) {
    var path = "Handler/GetChaptersByCourse.ashx?courseid=" + courseId;
    CallHandler(path, function (result) { onSuccessPopulateChaptersByCourse(result, courseId, CallFunctionAfterChaptersSectionsLoaded); });
}

function onSuccessPopulateChaptersByCourse(result, courseId, CallFunctionAfterChaptersSectionsLoaded) {
    if (result.Status == "Ok") {
        //console.log(result);
        Chapters = result.Chapters;

        PopulateSecionsByCourse(courseId, CallFunctionAfterChaptersSectionsLoaded);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            //parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}

function PopulateSecionsByCourse(courseId, CallFunctionAfterChaptersSectionsLoaded) {
    var path = "Handler/GetSectionsByCourse.ashx?courseid=" + courseId;
    CallHandler(path, function (result) { onSuccessPopulateSecionsByCourse(result, CallFunctionAfterChaptersSectionsLoaded) });
}

function onSuccessPopulateSecionsByCourse(result, CallFunctionAfterChaptersSectionsLoaded) {
    if (result.Status == "Ok") {
        console.log(result);
        Secions = result.Sections;
        //Init1();
        CallFunctionAfterChaptersSectionsLoaded();
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            //parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}
//---------------------------------------end---------------------------------------------------

//-------------------------Manupulating Chapters and Sections----------------------------------
function GetChapterObject(chapterId) {
    if (chapterId == 1 || chapterId == 0) {
        return Chapters[0];
    }

    for (var i = 0; i < Chapters.length; i++) {
        if (Chapters[i].Id == chapterId) {
            return Chapters[i];
        }
    }
}

function GetChapterSectionObject(chapterId, sectionId) {
    var sectionObject = GetChapterSectionList(chapterId);

    if (sectionId == 1 || sectionId == 0) {
        return sectionObject[0];
    }

    for (var i = 0; i < sectionObject.length; i++) {
        if (sectionObject[i].Id == sectionId) {
            return sectionObject[i];
        }
    }
}

function GetChapterSectionList(chapterId) {
    var ChapterSection = [];

    if (chapterId == 1) {
        for (var i = 0; i < Secions.length; i++) {
            if (Secions[i].ChapterId == Chapters[0].Id) {
                ChapterSection.push(Secions[i]);
            }
        }
        return ChapterSection;
    }

    for (var i = 0; i < Secions.length; i++) {
        if (Secions[i].ChapterId == chapterId) {
            ChapterSection.push(Secions[i]);
        }
    }

    return ChapterSection;
}
//---------------------------------End---------------------------------------------------------------

//buildBASE_URL();

function buildBASE_URL() {
    var baseURL = BASE_URL.split('/');

    var protocol = document.location.protocol;
    var hostName = document.location.hostname;

    var newURL = protocol + "//" + hostName + "/" + baseURL[3];
    if (baseURL[3] != "") {
        newURL += "/";
    }

    BASE_URL = newURL;
}

function Init() {
    $(".content-container").css("display", "none");
    //Scroll page to top when loding image is show    
    parent.window.scrollTo(0, 0);

    //Add Loader image while page is rendered
    var loaderContain = $("<div/>");
    $(loaderContain).attr("id", "loaderMainContain");
    $(loaderContain).attr("style", "width: 100%;float: left;text-align: center;font-size:24px;margin-top: 20%;");
    $(loaderContain).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading... Please wait ");
    var ele = $(".content-container").parent();
    $(ele).append(loaderContain);

    getQueryStrings();
    var qs = getQueryStrings();
    //chapterId = qs["chapterId"];
    //sectionId = qs["sectionId"];
    courseId = qs["courseid"];
    PopulateChaptersByCourse(courseId, Init1);
}

function Init1() {
    // Hide Main-Container till completely rendered    
    $(".content-main").find("a").each(function () {
        $(this).attr("target", "_blank");
    });

    $("#dbHtmlContainer").find("a").each(function () {
        $(this).attr("target", "_blank");
    });

    $(".content-container").css("display", "none");

    //Add Loader image while page is rendered
    var loaderContain = $("<div/>");
    $(loaderContain).attr("id", "loaderMainContain");
    $(loaderContain).attr("style", "width: 100%;float: left;text-align: center;font-size:24px;margin-top: 20%;");
    $(loaderContain).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading... Please wait ");
    var ele = $(".content-container").parent();
    $(ele).append(loaderContain);

    var i = 0;
    $(".content-main").find(".content-navigation").each(function () {
        i++;
        $(this).attr("id", "content_navigation_" + i);
    });

    // adding Quiz loader image
    var loaderQuiz = $("<div/>");
    $(loaderQuiz).attr("id", "loaderQuiz");
    $(loaderQuiz).attr("style", "width: 100%;float: left;text-align: center;")
    $(loaderQuiz).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading Quiz... Please wait");
    $(".content-main").append(loaderQuiz);

    // adding Modul-Popup for Report-Issue
    var divModal = $("<div/>");
    $(divModal).html('<div class="modal fade" id="ReportIssue" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true"> ' +
        '<div class="modal-dialog"> <div class="modal-content"> <div class="modal-header"> <button type="button" onclick="HideReportIssuePopup();" class="close" data-dismiss="modal" aria-hidden="true"> ' +
         ' &times;</button> <h4 class="modal-title">Report an issue</h4></div><div class="modal-body">' +
         '<div id="reportIssueStatus" class="ErrorContainer" style="display:none"></div>' +
         '<div class="Record"><div class="Column2"><input type="text" id="txtTitle" placeholder="Title" /><span style="color: Red">*</span></div></div><div class="Record"><div class="Column2"> ' +
         '<input type="text" id="txtDescription" placeholder="Description" /><span style="color: Red">*</span></div></div><div class="Record"><div class="Column2"> ' +
         '<input type="text" id="txtEmail" placeholder="E-mail" /><span style="color: Red">*</span></div></div><div class="Record"><div class="Column2"> ' +
         '<textarea id="txtExpectedContent" placeholder="Expected content" cols="20" rows="2" style="max-height: 100px;margin-bottom: 3%; padding: 6px;"></textarea><span style="color: Red">*</span></div></div><div class="Record"> ' +
         '<div class="Column2"></div></div></div><div class="modal-footer"><a href="Javascript:void(0);" class="btn" data-dismiss="modal" onclick="HideReportIssuePopup();" ' +
         'title="click to close popup">Close</a><input id="btnSave" type="button"  value="Submit" class="btn btn-primary" OnClick="SaveReportIssue();" />' +
         '</div></div><!-- /.modal-content --></div><!-- /.modal-dialog --></div> <div id="ReportModelBack" style="display:none;" class="modal-backdrop fade in"></div>');

    $(".content-main").append(divModal);

    //adding link-button for report issue
    var reportIssue = $("<div/>");
    $(reportIssue).attr("style", "float: left;");
    $(reportIssue).attr("id", "reportissuelink");
    $(reportIssue).html("<a style='color: white' data-toggle='modal' onClick='ShowReportIssuePopup();' href='javascript:void(0);'>Report Issue</a>");
    $("#content_navigation_1").append(reportIssue);

    /*var facebookShare = $("<div/>");
    $(facebookShare).attr("style", "float: left;margin-left:5px;");
    $(facebookShare).attr("id", "fbShare");
    $(facebookShare).html("<a style='color: white' href='https://www.facebook.com/sharer/sharer.php?u=http://adminapps.in/course/Course/fundamental-of-computers/1/Introduction.htm' target='_blank'>Share on Facebook </a>");
    $("#content_navigation_1").append(facebookShare);*/


    getQueryStrings();
    var qs = getQueryStrings();
    chapterId = qs["chapterId"];
    sectionId = qs["sectionId"];
    courseId = qs["courseid"];

    //PopulateChaptersByCourse(courseId);
    //console.log(window.navigator);

    //SaveSiteAnalyticsDetails();

    var ref = document.location.href.indexOf(BASE_URL);
    if (ref >= 0) {
        try {
            var values = parent.document.location.href.substring(BASE_URL.length).split("/");
            var value = values[0].split("?");
            $("#loaderMainContain").remove();
            if (value[0].toLowerCase() == "hintpage.aspx") {
                //BuilLeftNavigation();
                BuilLeftNavigationForAnnonymusUser();
                BuildBreadcrumbs(chapterId, sectionId);

                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Previous") {
                        $(this).hide();
                    }
                    $(this).hide();
                });

                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Next") {
                        //$(this).attr("onClick", "nextClick();");next_previusClick
                        $(this).hide();
                    }

                });

                $(".content-container").css("display", "block");
                $("#loaderMainContain").remove();                
                $("#loaderQuiz").remove();
            }
            else if (value[0].toLowerCase() == "viewcontent.aspx") {

                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Previous") {
                        $(this).html("Next");
                        $(this).attr("onClick", "next_previusClick(true);");
                        $(this).attr("style", "float:right");
                    }
                    else if ($(this).html().trim() == "Next") {
                        $(this).html("Previous");
                        $(this).attr("onClick", "preClick();");
                        $(this).attr("style", "float:right;margin-right:10px");
                    }

                    $(this).attr("class", "btn btn-primary");
                });

                //CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseId, OnComplete);
                var chapterobj = GetChapterObject(chapterId);
                var section = GetChapterSectionObject(chapterId, sectionId);

                var qs = GetQueryStringsForHtmPage();
                chapterId = qs["chpid"];
                sectionId = qs["secid"];
                //chapterId = section.ChapterId;
                //sectionId = section.Id;

                /*var ref = document.location.href.indexOf(BASE_URL);
                if (ref >= 0) {
                try {
                var values = document.location.href.substring(BASE_URL.length).split("/");
                chapterId = values[2];
                sectionId = GetChapterSectionID(chapterId, values[3]);
                //alert(sectionId);
                }
                catch (e) {
                alert(e);
                }
                }*/

                // creating left navigation
                BuilLeftNavigation();
                //creating breadcrumbs
                BuildBreadcrumbs(chapterId, sectionId);
                // creating and assinging next previus button functionality
                BuildNextPreviusButtonFunctional(chapterId, sectionId);
                // get Chapter and section Quiz
                GetQuizFromDatabase();
                // check if user browser support csss3 and html5
                CheckUserBrowerSupportCSS3HTML5();

                if (chapterId == Chapters[Chapters.length - 1].Id) {
                    secionList = GetChapterSectionList(chapterId);
                    if (sectionId == secionList[secionList.length - 1].Id) {
                        $(".content-navigation").find("div").each(function () {
                            if ($(this).html().trim() == "Next") {
                                $(this).html("Finish");
                                $(this).attr("onClick", "next_previusClick(true);");
                            }
                        });
                    }
                }

                $(".content-container").css("display", "block");
                $("#loaderMainContain").remove();
            }
            else {
                document.location = BASE_URL + "CourseContent.aspx";
            }

            var hostName = document.location.hostname;
            if (hostName == "moocacademy.in" || hostName == "www.moocacademy.in") {
                (function (i, s, o, g, r, a, m) {
                    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                        (i[r].q = i[r].q || []).push(arguments)
                    }, i[r].l = 1 * new Date(); a = s.createElement(o), m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
                })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

                ga('create', 'UA-46034211-1', 'moocacademy.in');
                ga('send', 'pageview');
            }
        }
        catch (e) {
            //alert(e);
        }
    }

}


function getQueryStrings() {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = parent.location.search.substring(1);

    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}

function GetQueryStringsForHtmPage() {

    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = document.location.search.substring(1);

    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}

//var path = BASE_URL + "Handler/GetUserChapterStatus.ashx";

var currentId;

function CallHandler(queryString, OnComp) {

    var path = BASE_URL + queryString;

    $.ajax({
        url: path,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        responseType: "json",
        cache: false,
        //success: OnComplete,
        success: OnComp,
        error: OnFail
    });
    return false;
}

function OnComplete(result) {
    if (result.Status == "Ok") {
        //PopulateChaptersByCourse(courseId);
        chapterId = result.ChapterId;
        sectionId = result.SectionId;

        var chapterobj = GetChapterObject(chapterId);
        var section = GetChapterSectionObject(chapterId, sectionId);

        var qs = GetQueryStringsForHtmPage();
        chapterId = qs["chpid"];
        sectionId = qs["secid"];
        //chapterId = section.ChapterId;
        //sectionId = section.Id;

        /*var ref = document.location.href.indexOf(BASE_URL);
        if (ref >= 0) {
        try {
        var values = document.location.href.substring(BASE_URL.length).split("/");
        chapterId = values[2];
        sectionId = GetChapterSectionID(chapterId, values[3]);
        //alert(sectionId);
        }
        catch (e) {
        alert(e);
        }
        }*/

        // creating left navigation
        BuilLeftNavigation();
        //creating breadcrumbs
        BuildBreadcrumbs(chapterId, sectionId);
        // creating and assinging next previus button functionality
        BuildNextPreviusButtonFunctional(chapterId, sectionId);
        // get Chapter and section Quiz
        GetQuizFromDatabase();
        // update chapter time e.g. how much time a user spend on a perticuler chapter
        UpdateChapterTime(chapterId, sectionId);
        // udpate chapter status
        UpdateUserChapterStatus(chapterId, sectionId);
        // check if user browser support csss3 and html5
        CheckUserBrowerSupportCSS3HTML5();

        if (chapterId == Chapters[Chapters.length - 1].Id) {
            secionList = GetChapterSectionList(chapterId);
            if (sectionId == secionList[secionList.length - 1].Id) {
                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Next") {
                        $(this).html("Finish");
                        $(this).attr("onClick", "next_previusClick(true);");
                    }
                });
            }
        }

        $(".content-container").css("display", "block");
        $("#loaderMainContain").remove();
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

function SaveSiteAnalyticsDetails() {
    var intverval = setInterval(function () {
        var time = 2;
        var title = document.title;
        var page = document.location.href.substring(document.location.href.lastIndexOf('/') + 1);
        var refferPage = document.referrer;
        var screenResulotion;

        if ($(window.parent) != undefined) {
            screenResulotion = $(window.parent).width() + " x " + $(window.parent).height();
        }
        else {
            screenResulotion = $(window).width() + " x " + $(window).height();
        }

        var path = "Handler/SaveSiteAnalyticsDetails.ashx?time=" + time + "&page=" + page + "&pagetitle=" + title + "&refferpage=" + refferPage + "&screenresulotion=" + screenResulotion + "&comment=";

        CallHandler(path, function (result) { })
    }, 2000);

}

function CheckUserBrowerSupportCSS3HTML5() {

    var test_canvas = document.createElement("canvas") //try and create sample canvas element
    var canvascheck = (test_canvas.getContext) ? true : false //check if object supports getContext() method, a method of the canvas element
    //alert(canvascheck) //alerts true if browser supports canvas element
    if (!canvascheck) {

        $(".breadcrumb").parent().closest('div').attr("id", "brdParent");
        $(".breadcrumb").parent().closest('div').attr("style", "float: left;width: 99%;");

        $(".content-container").find("#brdParent").each(function () {
            $("<div class='ErrorContainer' style='text-align:center;'>Please view this page in modern browsers. You can download it from <a href='#'> Chrome </a>, <a href='#'> Firfox </a> and <a href='#'> Internet Explorer  </a> .</div>").insertBefore(this);
        });
    }
}

function OnFail(result) { }

function BuilLeftNavigation() {
    var lnClass = "left-nav";
    var cc = $(".content-summary").attr("class").split(" ");
    if (cc.length >= 2) {
        lnClass = lnClass + " " + cc[1];
    }

    var ul = $("<ul/>");
    //$(ul).attr("class", "left-nav blue");
    $(ul).attr("class", lnClass);

    var chapterSection = GetChapterSectionList(chapterId);

    for (var i = 0; i < chapterSection.length; i++) {
        var li = $("<li/>");

        if (chapterSection[i].Id == sectionId) {
            $(li).attr("class", "active");
        }

        //$(li).html("<a href='javascript:void(0);' onClick='leftNavigationClick(\"" + chapterSection[i].Link + "\");'> " + chapterSection[i].Title + "</a>");
        $(li).html("<a href='javascript:void(0);' onClick='leftNavigationClick(" + chapterSection[i].ChapterId + "," + chapterSection[i].Id + ");'> " + chapterSection[i].Title + "</a>");
        $(ul).append(li);
    }

    $(".left-navigation").append(ul);
}

// build left navigation for Annonymus user 
function BuilLeftNavigationForAnnonymusUser() {
    var lnClass = "left-nav";
    var cc = $(".content-summary").attr("class").split(" ");
    if (cc.length >= 2) {
        lnClass = lnClass + " " + cc[1];
    }

    var ul = $("<ul/>");
    //$(ul).attr("class", "left-nav blue");
    $(ul).attr("class", lnClass);

    var chapterSection = GetChapterSectionList(chapterId);

    for (var i = 0; i < chapterSection.length; i++) {
        var li = $("<li/>");

        if (chapterSection[i].Id == sectionId) {
            $(li).attr("class", "active");
        }

        $(li).html(chapterSection[i].Title);
        $(ul).append(li);
    }

    $(".left-navigation").append(ul);
}

function BuildBreadcrumbs(chapterId, sectionId) {
    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    // integrate facebook like & share buttons
    ///var _URL = BASE_URL + "HintPage.aspx?chapterid=" + chapterId + "&sectionid=" + sectionId;
    //var _URL = BASE_URL + "course/fundamentals-of-computer/" + chapterId + "/" + sectionObject.PageName;
    var _URL = BASE_URL + "ViewContent.aspx?chapterId=" + chapterId + "&sectionId=" + sectionId + "&courseid=" + courseId;

    var ele = $("#content_navigation_1");
    $('<iframe src="//www.facebook.com/plugins/like.php?href=' + _URL + '&amp;width&amp;layout=button_count&amp;action=like&amp;show_faces=true&amp;share=true&amp;height=21&amp;appId=137095629816827" scrolling="no" frameborder="0" style="border:none; overflow:hidden; height:30px;" allowTransparency="true"></iframe>').insertBefore(ele);
    // end


    //build bredbrumbs
    li = $("<li/>");
    $(li).html("<a href='" + BASE_URL + "Dashboard.aspx" + "' target='_top'>" + "Home" + "<a/> <span class='divider'>/</span>");
    $(".breadcrumb").append(li);

    var li = $("<li/>");
    //$(li).html("<a href='" + BASE_URL + chapterObject.Link + "'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    //$(li).html("<a href='javascript:void(0);' onClick='ClickBreacrubms(\"" + chapterObject.Link + "\");'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    $(li).html("<a target='_blank' href='" + BASE_URL + "CourseDetails.aspx?id=" + courseId + "'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    $(".breadcrumb").append(li);


    li = $("<li/>");
    //$(li).html("<a href='" + BASE_URL + sectionObject.Link + "'>" + sectionObject.Title + " <a/>");
    $(li).html(sectionObject.Title);
    $(".breadcrumb").append(li);

    // creating page header    
    $(".summary-header").html(sectionObject.Title);

    var div = $("<div/>");
    //$(div).html(sectionObject.Description);
    $(".content-summary").append(div)

    var time = parseInt(sectionObject.Time) / 60;

    var divTime = $("<div/>");
    $(divTime).attr("style", "float:right");
    $(divTime).html("Estimated time to complete: " + time + " minutes");
    $(".content-summary").append(divTime)

    $(".content-summary").attr("chpId", chapterId);
    $(".content-summary").attr("secId", sectionId);

    var currentChapterId = 0;
    for (var k = 0; k < Chapters.length; k++) {
        if (chapterObject.Id == Chapters[k].Id) {
            currentChapterId = k + 1;
            //break;
        }
    }

    $(".content-summary").find(".summary-header").each(function () {
        //$("<div style='margin-bottom: 3px;'>Chapter " + chapterObject.Id + " : " + chapterObject.Title + "</div>").insertBefore(this);
        $("<div style='margin-bottom: 3px;'>Chapter " + currentChapterId + " : " + chapterObject.Title + "</div>").insertBefore(this);
    });

    $("#dbHtmlContainer").html(unescape(sectionObject.PageContent));

    if (sectionObject.Description != "") {
        var divClass = "content-summary " + sectionObject.Description;
        var ulClass = "left-nav " + sectionObject.Description;

        var cc = $(".content-summary").attr("class", divClass);
        var ul = $(".left-nav").attr("class", ulClass);
    }
    //disable rigth click on page
    //$(".content-container").attr("oncontextmenu", "return false");
}

function ClickBreacrubms(link) {
    document.location = BASE_URL + link
}

function BuildNextPreviusButtonFunctional(chapterId, sectionId) {
    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = sectionId + 1;
    var preSectionId = sectionId - 1;

    if (preSectionId <= 0) {
        preSectionId = 1;
    }


    //if (sectionId == 1) {
    //if (sectionId == sectionList[0].Id) {
    if (Chapters[0].Id == chapterId && sectionId == sectionList[0].Id) {
        $(".content-navigation").find("div").each(function () {
            if ($(this).html().trim() == "Previous") {
                $(this).hide();
            }

        });

    }
    else if (sectionId == sectionList.length) {
        $(".content-navigation").find("div").each(function () {
            if ($(this).html().trim() == "Next") {
                //$(this).hide();
            }
        });
    }
}

function BuilLeftNavigationForSylabus() {
    var ul = $("<ul/>");
    $(ul).attr("class", "left-nav blue");

    var liHeader = $("<li/>");
    $(liHeader).attr("class", "active");
    $(liHeader).html("Chapters");
    $(ul).append(liHeader);

    for (var i = 0; i < Chapters.length; i++) {
        var li = $("<li/>");

        //if (Chapters[i].Id == sectionId) {
        $(li).attr("class", "active");
        //}

        $(li).html(Chapters[i].Title);
        //$(li).attr("onClick", "ShowSection(" + Chapters[i].Id + ")");
        $(li).attr("onClick", "ShowSection(this)");
        $(li).attr("chpId", Chapters[i].Id);

        $(ul).append(li);
        if (i == 0)
            ShowSection(li);
    }

    //$(".left-navigation").append(ul);chapters
    $("#chapters").append(ul);
}

//functio Show Sections
function ShowSection(link) {

    $("#chapters").find("li").each(function () {
        $(this).attr("class", "active");
    });

    var chpId = $(link).attr("chpId");
    $(link).attr("class", "head");
    $(link).css("text-transform", "none");

    $("#secions").html("");
    var chapObj = GetChapterObject(chpId);
    var secList = GetChapterSectionList(chpId);

    var ul = $("<ul/>");
    $(ul).attr("class", "left-nav blue");

    var liHeader = $("<li/>");
    $(liHeader).attr("class", "head");
    $(liHeader).html("Overview");
    $(ul).append(liHeader);

    for (var i = 0; i < secList.length; i++) {
        var li = $("<li/>");
        $(li).attr("class", "active");
        //$(li).html("<a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chpId + "&sectionId=" + secList[i].Id + "'>" + secList[i].Title + "</a>");
        $(li).html("<a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chpId + "&sectionId=" + secList[i].Id + "&courseid=" + courseId + "'>" + secList[i].Title + "</a>");
        $(ul).append(li);
    }

    $("#secions").append(ul);

    $("#chapterIntro").html( unescape(secList[0].PageContent));

    /*for (var i = 0; i < Chapter_Indroduction.length; i++) {
    if (Chapter_Indroduction[i].ChapterId == chpId) {
    $("#chapterIntro").html(Chapter_Indroduction[i].Introduction);
    }
    }*/

}

// Dashboard Start and Continue functionality
function BuildStartorContinueText() {
    CallHandler("Handler/GetUserChapterStatus.ashx", OnCompleteStartContinue);
}

function OnCompleteStartContinue(result) {
    if (result.Status == "Ok") {
        chapterId = result.ChapterId;
        sectionId = result.SectionId;

        var chapterSection = GetChapterSectionObject(chapterId, sectionId);

        var title;
        if (chapterId == 1 && sectionId == 1) {
            title = "Start your course";
        }
        else {
            title = "Continue your course";
        }

        $("#StartCourse").html("<img id='imgStartContinue' src='static/images/c.png' alt='continue' style='margin-right:10px' /><a id='hyperlink' href='" + BASE_URL + 'CourseContent.aspx' + "'>" + title + " - FOC" + "</a>");
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

function AssingUrlToIFrame(courseid) {
    //PopulateChaptersByCourse(courseid);
    //CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseid, function (result) { onCompAssingUrlToIFrame(result, courseid); });
    PopulateChaptersByCourse(courseid, function () { CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseid, function (result) { onCompAssingUrlToIFrame(result, courseid); }); });
}

function onCompAssingUrlToIFrame(result, courseid) {
    if (result.Status == "Ok") {
        chapterId = result.ChapterId;
        sectionId = result.SectionId;

        var chapterSection = GetChapterSectionObject(chapterId, sectionId);

        var link = BASE_URL + chapterSection.Link + "?courseid=" + courseid + "&chpid=" + chapterSection.ChapterId + "&secid=" + chapterSection.Id;
        $("#ifContent").attr("src", link);
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

// assign url to hint page

function AssignUrlToHintPage(chapterId, sectionId, courseid) {
    var chapterSection = GetChapterSectionObject(chapterId, sectionId);
    if (chapterSection.IsDB) {
        var link = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseid + "&chpid=" + chapterSection.ChapterId + "&secid=" + chapterSection.Id;
        $("#ifHintContent").attr("src", link);
    }
    else {
        var link = BASE_URL + "Course/fundamentals-of-computer/" + chapterSection.ChapterId + "/" + chapterSection.PageName + "?courseid=" + courseId + "&chpid=" + chapterSection.ChapterId + "&secid=" + chapterSection.Id;
        $("#ifHintContent").attr("src", link);
    }
}

// Getting Quiz
function GetQuizFromDatabase() {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    //CallHandler("Handler/GetQuiz.ashx?chapterid=" + chId + "&sectionid=" + secId, onCompletePopulateQuiz);
    CallHandler("Handler/GetQuizForAnnonymusUser.ashx?chapterid=" + chId + "&sectionid=" + secId, onCompletePopulateQuiz);
}

function onCompletePopulateQuiz(result) {
    $("#loaderQuiz").remove();
    if (result.Status == "Ok") {

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");
        $("#loaderQuiz").remove();

        if (result.Quiz != null) {
            if (result.Quiz.length > 0) {
                for (var i = 0; i < result.Quiz.length; i++) {
                    if (result.Quiz[i].ChapterId == chId && result.Quiz[i].SectionId == secId)
                        PopulateQuiz(result.Quiz[i], i);
                }
            }
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

function PopulateQuiz(quiz, counter) {
    //alert(1);
    // creating maincontainer
    var maincontainer = $("<div/>");
    $(maincontainer).attr("class", "quizContainer");
    //end

    var errorStatus = $("<div/>");
    $(errorStatus).attr("class", "ErrorContainer");
    $(errorStatus).attr("id", "errorStatus_" + counter);
    $(errorStatus).html("Please select appropriate answer.");
    $(errorStatus).attr("style", "display:none");
    $(maincontainer).append(errorStatus);

    // creating question text container
    var questionText = $("<div/>");
    $(questionText).attr("id", "QuesionText_" + counter);
    $(questionText).attr("class", "questionText");
    $(questionText).attr("quesId", quiz.Id);
    $(questionText).html("Question " + (parseInt(counter) + 1) + " : " + decodeURIComponent(quiz.QuestionText));

    $(maincontainer).append(questionText);
    // end

    // creating question option text
    var questionOption = $("<div/>");
    $(questionOption).attr("id", "QuestionOption_" + counter);
    $(questionOption).attr("class", "questionText");

    for (var i = 0; i < quiz.QuestionOptionList.length; i++) {

        var div = $("<div/>");
        $(div).attr("id", "QO" + i);
        $(div).html((i + 1) + ".  " + quiz.QuestionOptionList[i].QuestionOption);

        $(questionOption).append(div);
    }

    $(maincontainer).append(questionOption);
    // end

    // creating answer option text
    var answerOption = $("<div/>");
    $(answerOption).attr("id", "AnswerOption_" + counter);
    $(answerOption).attr("class", "answerOption");

    var html = "";
    for (var i = 0; i < quiz.AnswerOptionList.length; i++) {

        var div = $("<div/>");

        $(div).attr("id", "AO" + i);
        //$(div).attr("class", "radioDiv");

        // check if question has mulitiple correct answer
        if (!quiz.IsMultiTrueAnswer) {
            // populating single correct answer mode
            var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");

            if (s == quiz.AnswerText) {
                //$(div).html(s + "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/>");
                html += "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
            }
            else {
                //$(div).html(s + "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
                html += "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
            }
        }
        else {
            // populating multi correct answer mode
            var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");

            if (s == quiz.AnswerText) {            
                //$(div).html(s + "<input type='checkbox' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/>");
                html += "<input type='checkbox' id='Ans_" + i + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
            }
            else {
                //$(div).html(s + "<input type='checkbox' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
                html += "<input type='checkbox' id='Ans_" + i + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
            }
        }

        //$(answerOption).append(div);
    }

    $(answerOption).html(html);

    $(maincontainer).append(answerOption);

    // creating submit button in populate quiz function
    var btnField = $("<div/>");
    $(btnField).attr("id", "btnField_" + counter);
    $(btnField).attr("style", "margin-top: 25px; width: 100%; float: left;");
    //added by anup
    if (quiz.IsAnsGiven) {
        var status = $("<div/>");
        $(status).attr("id", "status_" + counter);

        if (quiz.IsCorrect) {
            $(status).html("Your answer is correct.<br/>Your answer is '<b>" + quiz.AnswerText + "'</b>");
            $(status).attr("style", "color: green");
        }
        else {
            $(status).html("Your answer is incorrect.<br/>Your answer is '<b>" + quiz.AnswerText + "'</b>");
            $(status).attr("style", "color: red");
        }

        $(btnField).append(status);
    }
    else {

        var btn = $("<div/>");
        $(btn).attr("id", "btn_" + counter);
        $(btn).attr("class", "btn btn-primary");
        $(btn).html("Submit");
        $(btn).attr("onclick", "SaveQuiz(" + counter + ")");
        $(btnField).append(btn);

        var status = $("<div/>");
        $(status).attr("id", "status_" + counter);
        $(btnField).append(status);

        var loader = $("<div/>");
        $(loader).attr("id", "loader_" + counter);
        $(loader).html("<img src='" + BASE_URL + "static/images/waiting-loader.gif' />");
        $(loader).hide();
        $(btnField).append(loader);
    }


    $(maincontainer).append(btnField);
    //end
    //$(".content-main").append(maincontainer);
    $("#content_navigation_2").attr("style", "padding-top:15px");
    $(maincontainer).insertBefore("#content_navigation_2");
}

// save quiz function
function SaveQuiz(counter) {

    var userAnswer;
    var isCurrect;
    var isChecked;

    var isCurrectAnswer;
    var currectAnswer;

    $("#errorStatus_" + counter).attr("style", "display:none");

    $("#AnswerOption_" + counter).find(".ansRadio").each(function () {

        if ($(this).prop("checked")) {
            userAnswer = $(this).attr("answer");
            //userAnswer = userAnswer.replace("+", "%2B");
            isCurrect = $(this).attr("iscurrect");
            isChecked = true;
        }

        if ($(this).attr("iscurrect") == 'true') {
            currectAnswer = $(this).attr("answer");
            currectAnswer = currectAnswer.replace("%2B", "+");
        }
    });

    var questionId = $("#QuesionText_" + counter).attr("quesId");

    if (!isChecked) {
        $("#errorStatus_" + counter).attr("style", "display:block");
    }
    else {
        $("#btn_" + counter).hide();
        $("#loader_" + counter).show();

        if (isCurrect == 'true') {
            $("#status_" + counter).html("Your answer is correct.");
            $("#status_" + counter).attr("style", "color: green");
        }
        else {
            $("#status_" + counter).html("Your answer is incorrect.<br/>The currect answer is '<b>" + currectAnswer + "'</b>");
            $("#status_" + counter).attr("style", "color: red");
        }
        $("#loader_" + counter).hide();
    }
}

//end

function next_previusClick(isNext) {
    $(".content-navigation").find("div").each(function () {
        $(this).hide();
    });

    var d = $("<div/>");
    $(d).html("Loading next content....")
    $(d).attr("id", "loadingStatus");
    $(".content-navigation").append(d);

    nextClick();
}



var RemoveErrorMessage20SecInterval;
//
function nextClick() {
    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = parseInt(sectionId) + 1;
    var preSectionId = parseInt(sectionId) - 1;

    if (preSectionId <= 0) {
        preSectionId = 1;
    }

    if (nxtSectionId > sectionList[sectionList.length - 1].Id) {
        chapterId++;
        if (chapterId > Chapters[Chapters.length - 1].Id) {
            chapterId--;
            secionList = GetChapterSectionList(chapterId);
            //while (sectionList == null) {
            while (sectionList == null || sectionList.length == 0) {
                chapterId--;
                sectionList = GetChapterSectionList(chapterId);
            }

            if (sectionId == secionList[secionList.length - 1].Id) {
                parent.document.location = BASE_URL + "Dashboard.aspx";
            }
            else {
                nxtSectionId = sectionList[0].Id;
                sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
                if (sectionObject.IsDB) {
                    document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
                }
                else {
                    var link = BASE_URL + "Course/fundamentals-of-computer/" + sectionObject.ChapterId + "/" + sectionObject.PageName + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
                    document.location = link;
                }
            }
        }
        else {
            sectionList = GetChapterSectionList(chapterId);

            while (sectionList == null || sectionList.length == 0) {
                chapterId++;
                sectionList = GetChapterSectionList(chapterId);
            }

            nxtSectionId = sectionList[0].Id;
            sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
            if (sectionObject.IsDB) {
                document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
            }
            else {
                var link = BASE_URL + "Course/fundamentals-of-computer/" + sectionObject.ChapterId + "/" + sectionObject.PageName + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
                document.location = link;
            }
        }
    }
    else {
        sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
        while (sectionObject == null) {
            nxtSectionId++;
            sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
        }
        //document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        if (sectionObject.IsDB) {
            document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        }
        else {
            var link = BASE_URL + "Course/fundamentals-of-computer/" + sectionObject.ChapterId + "/" + sectionObject.PageName + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
            document.location = link;
        }
    }
}

function ClearAnswerFillMessage20Sec() {
    $("#qeusError_1").remove();
    $("#qeusError_2").remove();
}

function preClick() {
    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = sectionId + 1;
    var preSectionId = sectionId - 1;

    if (preSectionId < sectionList[0].Id) {
        //preSectionId = 1;
        chapterId--;
        sectionList = GetChapterSectionList(chapterId);

        //while (sectionList == null) {
        while (sectionList == null || sectionList.length == 0) {
            chapterId--;
            sectionList = GetChapterSectionList(chapterId);
        }

        preSectionId = sectionList[0].Id;
        sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        //document.location = BASE_URL + sectionObject.Link;
        if (sectionObject.IsDB) {
            document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        }
        else {
            var link = BASE_URL + "Course/fundamentals-of-computer/" + sectionObject.ChapterId + "/" + sectionObject.PageName + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
            document.location = link;
        }
    }
    else {
        sectionObject = GetChapterSectionObject(chapterId, preSectionId);

        while (sectionObject == null) {
            preSectionId--;
            sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        }

        if (sectionObject.IsDB) {
            document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        }
        else {
            var link = BASE_URL + "Course/fundamentals-of-computer/" + sectionObject.ChapterId + "/" + sectionObject.PageName + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
            document.location = link;
        }
    }
}


function leftNavigationClick(newChpId, navSecId) {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    sectionObject = GetChapterSectionObject(newChpId, navSecId);

    if (sectionObject.IsDB) {
        document.location = BASE_URL + "anonymus-user-show-db-section-contents.htm?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
    }
    else {
        var link = BASE_URL + "Course/fundamentals-of-computer/" + sectionObject.ChapterId + "/" + sectionObject.PageName + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        document.location = link;
    }

}

//Report issue
function ShowReportIssuePopup() {
    $('#ReportIssue').attr("style", "opacity: 1 !important;top: 2%");
    $("#ReportModelBack").attr("style", "display:block");
    //$(this).find("#ReportModelBack").attr("style", "display:block");
    //$('#ReportIssue').fadeIn(100);
}

function HideReportIssuePopup() {
    //$('#ReportIssue').attr("style", "opacity: 1 !important;top: 10%");
    $('#ReportIssue').fadeOut(100);
    $("#txtTitle").val("");
    $("#txtDescription").val("");
    $("#txtEmail").val("");
    $("#txtExpectedContent").val("");
    $("#reportIssueStatus").html("");
    $("#reportIssueStatus").attr("style", "display:none");
    $("#ReportModelBack").attr("style", "display:none");
}

//function SaveReportIssue(title, desc, email, expContent) {
function SaveReportIssue() {
    $("#reportIssueStatus").html("");
    $("#reportIssueStatus").attr("style", "display:none");

    var title = $("#txtTitle").val();
    var desc = $("#txtDescription").val();
    var email = $("#txtEmail").val();
    var expContent = $("#txtExpectedContent").val();

    if (title == "" || desc == "" || email == "" || expContent == "") {
        $("#reportIssueStatus").html("All fields are mandatory.");
        $("#reportIssueStatus").attr("style", "display:block");
        return;
    }
    else if (!validateEmail(email)) {
        $("#reportIssueStatus").html("Please enter valid e-mail address.");
        $("#reportIssueStatus").attr("style", "display:block");
    }
    else {
        title = changeSpecialCharsQS(title);
        desc = changeSpecialCharsQS(desc);
        expContent = changeSpecialCharsQS(expContent);

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");

        var path = "Handler/SaveReportIssue.ashx?title=" + title + "&description=" + desc + "&email=" + email + "&expectedContent=" + expContent + "&chapterId=" + chId + "&sectionId=" + secId;
        CallHandler(path, onSaveReportIssueSuccess);
    }
}

function onSaveReportIssueSuccess(result) {
    if (result.Status == "Ok") {
        $("#reportIssueStatus").attr("style", "display:block;background-color: lightgreen");
        $("#reportIssueStatus").html("your query submited successfully. Our technical team contact you shortly.");
        //$('#ReportIssue').fadeOut(1000);

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");

        var chpObj = GetChapterObject(chId);
        var secObj = GetChapterSectionObject(chId, secId);

        var title = secObj.Title + " || " + chpObj.Title;
        //var link = secObj.Link + " || " + chpObj.Link;
        var link = "chapterId=" + chId + " sectionId=" + secId + "||" + "chapterId=" + chId + " sectionId=" + secId;
        var activit = { "Title": title, "Link": link, "ActivityType": "5" };
        //activit.push();
        SaveUserActivity(activit);

        var intverval = setInterval(
        function () {
            $('#ReportIssue').fadeOut(100);
            $("#reportIssueStatus").html("");
            $("#reportIssueStatus").attr("style", "display:none");
            $("#ReportModelBack").attr("style", "display:none");
            clearInterval(intverval);
        }
        , 5000);

        $("#txtTitle").val("");
        $("#txtDescription").val("");
        $("#txtEmail").val("");
        $("#txtExpectedContent").val("");
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message);
            $("#reportIssueStatus").attr("style", "display:block");
            $("#reportIssueStatus").html("Error: " + result.Message);
        }
    }
}

// Validation Email
function validateEmail(emailText) {
    var pattern = /^[a-zA-Z0-9\-_]+(\.[a-zA-Z0-9\-_]+)*@[a-z0-9]+(\-[a-z0-9]+)*(\.[a-z0-9]+(\-[a-z0-9]+)*)*\.[a-z]{2,4}$/;
    if (pattern.test(emailText)) {
        return true;
    } else {
        return false;
    }
}
// end

//Replace special chars
function changeSpecialCharsQS(text) {
    text = text.replace('&', ' aaa ');
    return text;
}

function getSpectionChars(text) {
    text = text.replace(' aaa ', '&');
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
//end

//----------May I Help you Funcationality--------------
function SaveStudentQuery(name, mobile, query) {
    var path = "Handler/SaveStudentsQuery.ashx?name=" + name + "&mobileNo=" + mobile + "&query=" + query;
    CallHandler(path, onSaveStudentQuerySuccess);
}

function onSaveStudentQuerySuccess(result) {
    if (result.Status == "Ok") {
        $("#buttons").hide();
        $("#queryStatus").html("your query submited successfully. Our technical team will contact you shortly.");

        /*var activit = [];
        activit.push({ "Title": "You have contacted support center for your query", "Link": "", "ActivityType": "4" });*/
        var activit = { "Title": "You have contacted support center for your query", "Link": "", "ActivityType": "4" };
        SaveUserActivity(activit);

        var intverval = setInterval(
        function () {
            $("#buttons").show();
            $("#queryStatus").html("");

            clearInterval(intverval);
        }
        , 10000);
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

function showMayIHelpYouWindow() {
    $("#txtName").val("");
    $("#txtMobile").val("");
    $("#txtQuery").val("");
    $("#txtEmail").val("");

    $("#txtName").css("border-color", "#ccc");
    $("#txtMobile").css("border-color", "#ccc");
    $("#txtQuery").css("border-color", "#ccc");
    $("#txtEmail").css("border-color", "#ccc");

    $("#buttons").show();
    $("#queryStatus").html("");

    if ($("#ql_ShortlistHeadline").attr("class") == "rfloat arrowDown") {
        $("#ql_Tab").attr("style", "display:none");
        //$("#ql_Tab").hide(100);
        $("#ql_ShortlistHeadline").attr("class", "rfloat arrowUp");
    }
    else {
        $("#ql_Tab").attr("style", "display:block");
        //$("#ql_Tab").show(100);
        $("#ql_ShortlistHeadline").attr("class", "rfloat arrowDown");
        $("#txtName").focus();
    }

}

function SaveMayIHelpYouQuery() {
    $("#txtName").css("border-color", "#ccc");
    $("#txtMobile").css("border-color", "#ccc");
    $("#txtQuery").css("border-color", "#ccc");
    $("#txtEmail").css("border-color", "#ccc");

    if ($("#txtName").val() == "") {
        $("#txtName").css("border-color", "red");
        $("#txtName").focus();
        return;
    }

    if ($("#txtMobile").val() == "") {
        $("#txtMobile").css("border-color", "red");
        $("#txtMobile").focus();
        return;
    }
    if ($("#txtQuery").val() == "") {
        $("#txtQuery").css("border-color", "red");
        $("#txtQuery").focus();
        return;
    }



    var name = $("#txtName").val();
    var mobile = $("#txtMobile").val();
    //var query = $("#txtQuery").val() + " || Email Address:" + $("#txtEmail").val();

    if (!isNumber(mobile)) {
        $("#txtMobile").css("border-color", "red");
        $("#txtMobile").focus();
        return;
    }

    if (!validateEmail($("#txtEmail").val())) {
        $("#txtEmail").css("border-color", "red");
        $("#txtEmail").focus();
        return;
    }

    var query = $("#txtQuery").val() + " || Email Address:" + $("#txtEmail").val();

    SaveStudentQuery(name, mobile, query);

    $("#txtName").val("");
    $("#txtMobile").val("");
    $("#txtQuery").val("");
    $("#txtEmail").val("");
}
//-----------End May I Help you Functionality---------

//-----------Search Functionality--------------
function Search() {
    if ($("#txtSearch").val() != "") {
        parent.document.location = BASE_URL + "SearchResult.aspx?searchText=" + $("#txtSearch").val();
    }
}

function PopulateSearchResult(searchText) {
    if ($("#Content_hfSearchResult").val() != "" && $("#Content_hfSearchResult").val() != "null") {
        var searchResult = [];
        searchResult = JSON.parse($("#Content_hfSearchResult").val());

        $("#searchHeader").html("Search Result for '" + searchText + "'");

        if (searchResult.SearchedFiles.length > 0) {

            for (var i = 0; i < searchResult.SearchedFiles.length; i++) {
                var chpId;
                var secId;

                var values = searchResult.SearchedFiles[i].split("/");
                if (values[2] != "Images") {
                    chpId = values[2];
                    secId = GetChapterSectionID(chpId, values[3]);
                    var sectionObject = GetChapterSectionObject(chpId, secId);

                    var div = $("<div/>");
                    var act = $("<div/>");
                    $(act).html("<a target='_blank'  href='" + BASE_URL + "HintPage.aspx?chapterId=" + chpId + "&sectionId=" + secId + "'>" + sectionObject.Title + "</a>");

                    $(div).attr("class", "activityContainer");
                    $(div).append(act);

                    $("#searchContainer").append(div);
                }
            }
        }
        else {
            var div = $("<div/>");
            var act = $("<div/>");
            $(act).html("No search result found for '" + searchText + "'.");

            $(div).attr("class", "activityContainer");
            $(div).append(act);

            $("#searchContainer").append(div);
        }
    }
}
//-----------End Search Functionality--------------

function SearchKeyPress(key) {
    if (key.charCode == "13" || key.keyCode == "13") {
        Search();
    }
}

function ClearPasswordFields() {
    $("#txtOldPassword").val("");
    $("#txtNewPassword").val("");
    $("#txtRetypeNewPassword").val("");
    $("#changePasswordStatus").css("display", "none");
    $("#changePasswordStatus").html("");
}

// Re-Appear in final Quiz
function ReappearInFinalQuiz() {
    var path = "Handler/MoveUserFinalQuizResToArchive.ashx";
    CallHandler(path, onCompleteMoveQuizResponse);
}

function onCompleteMoveQuizResponse(result) {
    if (result.Status == "Ok") {
        parent.document.location = BASE_URL + "TakeFinalQuiz.aspx";
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