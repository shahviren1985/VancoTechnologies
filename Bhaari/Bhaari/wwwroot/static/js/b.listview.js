// 1. Get Job List
// 2. Get Company List
// 3. Prepare List
// 4. Click Handler & select marker & open popup
var JobList = function () {
    var JOB_LIST = JSON.parse($("#hfJobs").val());

    var PrepareFavoriteList = function () {
        $(".fav-message").remove();
        $(".left-nav").append("<span class=\"fav-message\">Launching soon. You will be able to save your favorite jobs.</span>");
    }

    var PrepareJobList = function () {
        var searchTerm = "<div>Search results for: " + getQueryStringValue("q") + "</div>";
        var list = "<ul class=\"job-list\">";
        var userId = localStorage.getItem("token");
        for (var job in JOB_LIST) {
            JOB_LIST[job].CompanyLogo = (JOB_LIST[job].CompanyLogo == "" || JOB_LIST[job].CompanyLogo == null) ? "default.png" : JOB_LIST[job].CompanyLogo;
            list += "<li cid=\"" + JOB_LIST[job].CompanyId + "\" jid=\"" + JOB_LIST[job].Id + "\" onclick=\"LoadJobDetailsPopup(this," + JOB_LIST[job].Id + ",'" + escape(JOB_LIST[job].CompanyName) + "','" + JOB_LIST[job].SourceUrl + "')\">" +
                "<div class=\"job-title\">" + JOB_LIST[job].JobTitle + "</div>" +
                "<div class=\"company-name\">" + JOB_LIST[job].CompanyName + "</div>" +
                "<span class='mark-fav' onclick='return SaveFav(event,\"" + JOB_LIST[job].JobTitle + "\",\"" + JOB_LIST[job].CompanyName + "\",\"" + JOB_LIST[job].SourceUrl + "\",\"" + JOB_LIST[job].Id + "\",\"" + userId + "\",\"" + JOB_LIST[job].DatePosted + "\")'><img src='static/img/empty-heart.png' /></span>" +
                "</li>";
        }

        list += "</ul>";

        //$(".left-nav").append(searchTerm);
        $(".left-nav").append(list);

        $(".left-nav ul.job-list li").mouseover(function () {
            $(this).find(".job-arrow").show();
        });

        $(".left-nav ul.job-list li").mouseout(function () {
            $(this).find(".job-arrow").hide();
        });

        var copy = $("<div/>").addClass("copy").html("&copy; 2019. Bhaari.com");
        var creditContainer = $("<div class=\"credit-container\"></div>");
        var aboutContainer = $("<div class=\"about-container\"></div>");

        var about = $("<div/>").addClass("footer-link").html("About us").click(function () {
            $(".about-container").show();
        });

        var credits = $("<div/>").addClass("footer-link-2").html("Credits").click(function () {
            $(".credit-container").show();
        });

        $(".left-nav").append(copy);
        /*$(".left-nav").append(about);
        $(".left-nav").append(credits);
        $(".left-nav").append(creditContainer);
        $(".left-nav").append(aboutContainer);
        PrepareCreditsList();
        PrepareAboutus();*/
    }

    var PrepareFilterList = function () {
        var container = $("<div/>").addClass("filter-list");
        var filter = "<div><h3>By Date Posted</h3></div>" +
            "<div><span class=\"flag-recent\"></span><span>Recently posted</span></div>" + // Recently Posted
            "<div><span class=\"flag-moderate\"></span><span>Posted with in last 30 days</span></div>" + // Posted with in Last Month
            "<div><span class=\"flag-old\"></span><span>Older than a month</span></div>" + // Older than Month
            "<div class=\"soon\"><i>(Launching soon)</i></div>" +
            "<div class=\"btn btn-success\">Apply</div>" +
            "<div><h3>By Experience Level</h3></div>" +
            "<div><input type=\"radio\" checked=\"checked\" name=\"experience\" value=\"0\" />All</div>" +
            "<div><input type=\"radio\" name=\"experience\" value=\"1\" />Internship</div>" +
            "<div><input type=\"radio\" name=\"experience\" value=\"2\" />Entry Level</div>" +
            "<div><input type=\"radio\" name=\"experience\" value=\"3\" />Experienced</div>" +
            "<div class=\"soon\"><i>(Launching soon)</i></div>" +
            "<div class=\"btn btn-success\">Apply</div>";

        return $(".left-nav").append($(container).append(filter));
    }

    var PrepareLeftNavOptions = function () {
        var list = "<ul class=\"nav-option-list\">";

        list += "<li class=\"active-tab\"><img src=\"static/img/jobs-icon.png\" width=\"16px\" height=\"16px\" style=\"margin-right: 10px;\" />Jobs</li>";
        list += "<li><img src=\"static/img/fav-icon.png\" width=\"16px\" height=\"16px\" style=\"margin-right: 10px;\" />Favorite</li>";

        list += "</ul>";

        $(".left-nav").append(list);
        $("ul.nav-option-list li").click(function () {


            // alert($(this).html());
            var ca = $(this).html().indexOf("Jobs") > -1 ? "Jobs" : "Favorite";

            if (isMobile) {
                $("ul.nav-option-list li").removeAttr("class");
                $(".job-list").hide();
                $(".filter-list").hide();
                $(this).addClass("active-tab");

                switch (ca) {
                    case "Jobs":
                        $(".job-list").show();
                        $(".fav-message").hide();
                        break;
                    case "Favorite":
                        $(".job-list").hide();
                        $(".fav-message").show();
                        break;
                }
            }
            else {
                $("ul.nav-option-list li").removeAttr("class");
                $(".job-list").parent().hide();
                $(".filter-list").hide();
                $(this).addClass("active-tab");

                switch (ca) {
                    case "Jobs":
                        $(".job-list").parent().show();
                        $(".fav-message").hide();
                        break;
                    case "Favorite":
                        $(".job-list").parent().hide();
                        $(".fav-message").show();
                        break;
                    /*case "Get Invited":
                        $(".invite-list").show();
                        break;*/
                }
            }

        });
    }

    PrepareLeftNavOptions();
    PrepareJobList();
    PrepareFavoriteList();
    //PrepareFilterList();
    //PrepareGetInvitationList();
    //$('.job-list').slimScroll({ height: '515px', width: '100%', alwaysVisible: true });
}

var LoadJobDetailsPopup = function (jobItem, jobId, companyName, url) {

    var className = $("#grid-view").attr("class");

    if (previous_val == '' || previous_val != jobId) {
        $("ul.job-list li").css({ "background-color": "#fff", "color": "black" });
        $('#information_div #job-details').remove();

        if (className == "active-view") {
            // grid view
            $('#information_div').attr("style", "top: 13%; left: 27%; position: absolute; width: 600px; border: 1px solid #AAA; border-radius: 2px; padding: 10px 0px 0px 7px; background-color: #ffffff; min-height: 500px");
        }
        else {
            // map view 
            $('#information_div').attr("style", "top: 25%; position: absolute; z-index: 1; right: 20px; width: 400pxtop: 25%; position: absolute; z-index: 1; right: 20px; border: 1px solid #AAA; border-radius: 2px; padding: 10px 0px 0px 7px; background-color: #ffffff; width: 400px; min-height: 430px");
        }

        var getMarkerName = companyName;
        var getMarkerLabelContent = companyName;

        $(jobItem).css({ "background-color": "#f0f3f7" });
        isMapView = false;
        GetJobDetails(jobId, url, companyName);

        //trackJobDetails(jobId);
        trackMapList(jobId, companyName);

        $("div.bubble").each(function () {
            if ($(this).text() == companyName) {
                previous_val = $(this);
                /*$(this).attr("oc", $(this).attr("class"));
                $(this).attr("oz", $(this).css("z-index"));
                $(this).attr("class", "bubble bubble-blue");*/
            }
            /*else {
                if ($(this).attr("oc") != undefined) {
                    $(this).attr("class", $(this).attr("oc"));
                    $(this).css("z-index", $(this).attr("oz"));
                }

            }*/
        });

        return false;
    }
}

function GetSignupInvitation(link) {
    var error = "<ul>";
    var isValid = true;
    if ($("#txtInviteUserName").val().trim() == "") {
        error += "<li>First Name is mandatory</li>";
        isValid = false;
    }
    if ($("#txtInviteEmail").val().trim() == "") {
        error += "<li>Email is mandatory</li>";
        isValid = false;
    }
    if (!isValidEmail($("#txtInviteEmail").val().trim())) {
        error += "<li>Enter valid email</li>";
        isValid = false;
    }
    error += "</ul>";
    if (!isValid) {
        $("#invite-pref-error").html(error).show();
        return;
    }
    else {
        $("#invite-pref-error").hide();

        // Encode the data
        var name = encodeURIComponent($("#txtInviteUserName").val().trim());
        var email = encodeURIComponent($("#txtInviteEmail").val().trim());

        // Make post statement
        $.ajax({
            url: "handlers/GetInvite.ashx?n=" + name + "&e=" + email,
            success: function (result) {
                // Show success & error messages
                if (result == "ok") {
                    $("#invite-pref-success").show();
                    $("#txtInviteUserName").val("");
                    $("#txtInviteEmail").val("");
                    $("#invite-pref-error").hide();
                    setTimeout(function () {
                        $("#invite-pref-success").hide();
                    }, 3000);
                    //link

                    setCookie("uname", name, 30);
                    setCookie("email", email, 30);
                    trackJobUrl(link, $(".active-view").html());
                    document.location.href = link;
                }
                else {
                    var error = "<ul><li>" + result + "</li></ul>";
                    $("#invite-pref-error").html(error).show();
                }
            }
        });
    }
}

function GetInvitation() {
    // validate the first name and email fields
    var error = "<ul>";
    var isValid = true;
    if ($("#txtUserName").val().trim() == "") {
        error += "<li>First Name is mandatory</li>";
        isValid = false;
    }
    if ($("#txtEmail").val().trim() == "") {
        error += "<li>Email is mandatory</li>";
        isValid = false;
    }
    if (!isValidEmail($("#txtEmail").val().trim())) {
        error += "<li>Enter valid email</li>";
        isValid = false;
    }
    error += "</ul>";
    if (!isValid) {
        $("#invite-error").html(error).show();
        return;
    }
    else {
        $("#invite-error").hide();

        // Encode the data
        var name = encodeURIComponent($("#txtUserName").val().trim());
        var email = encodeURIComponent($("#txtEmail").val().trim());

        // Make post statement
        $.ajax({
            url: "handlers/GetInvite.ashx?n=" + name + "&e=" + email,
            success: function (result) {
                // Show success & error messages
                if (result == "ok") {
                    $("#invite-success").show();
                    $("#txtUserName").val("");
                    $("#txtEmail").val("");
                    $("#invite-error").hide();
                    setTimeout(function () {
                        $("#invite-success").hide();
                    }, 3000);
                }
                else {
                    var error = "<ul><li>" + result + "</li></ul>";
                    $("#invite-error").html(error).show();
                }
            }
        });
    }
}

function SaveFav(event, jobTitle, companyName, sourceUrl, id, userId, datePosted) {
    userId = localStorage.getItem("token");
    var element = $(event.target);
    if (userId == null || userId == "" || userId == "null") {
        var url = "login.html?t=login"
        window.open(url, "_blank");
    }
    else {
        $(element).attr("src", "static/img/fav-icon.png");

        $.ajax({
            url: BASE_URL + "savejob?jobId=" + id + "&sourceUrl=" + encodeURIComponent(sourceUrl) + "&jobTitle=" + jobTitle + "&companyName=" + companyName + "&userId=" + userId + "&datePosted=" + datePosted,
            success: function (result) {
                console.log("Job saved as fav.");
            }
        });
    }

    event.stopPropagation();
    return false;
}

function ShowSearchJobs() {
    $("ul.nav-option-list li").each(function () {
        if ($(this).text() == "Jobs") {
            $(this).click();
        }
    });

    if ($('input[name=search]:checked').val() == "0") {
        $("ul.job-list li").each(function () {
            if ($(this).attr("isorigin") == "true") {
                $(this).css("display", "list-item");
            }
            else {
                $(this).css("display", "list-item");
            }
        });
    }
    else {
        $("ul.job-list li").each(function () {
            if ($(this).css("display") == "list-item") {
                $(this).attr("isorigin", "true");
            }

            $(this).css("display", "list-item");
        });

        $(".bubble").each(function () {
            if ($(this).css("display") == "block") {
                $(this).attr("isorigin", "true");
            }

            $(this).css("display", "block");
        });
    }
}