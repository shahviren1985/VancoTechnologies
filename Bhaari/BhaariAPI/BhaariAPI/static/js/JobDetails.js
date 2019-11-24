function GetJobDetails(jobId, url, companyName) {
    /*var jobDetails = $("<div/>").attr("id", "job-details");
    var loader = $("<img/>").attr("src", BASE + "static/img/load.gif").attr("class", "load-jobs");
    $(jobDetails).append(loader);
    $('#information_div').html("");
    $('#information_div').show().append(jobDetails);
    */
    //$("ul.job-list").slimScroll({ scrollTo: $("ul.job-list li[jid=\"" + jobId + "\"]").offset().top - $("ul.job-list li:first").offset().top - 120 });
    var jobQuery = { "Url": url, "JobId": jobId, "CompanyName": companyName };

    if (isMobile) {
        $(".left-nav").hide();
        if (isMapView) {
            $(".filter.mobile-view").show();
        }
        else {
            $(".filter").hide();
        }
        $('#information_div').hide("");
        $("#grid").html('<div class="Loading-Job-Details">Loading Job Details...</div>');
        $("#grid").show();
    }

    $.ajax({
        url: BASE_URL + "search/jobdetails",
        contentType: 'application/json',
        type: 'POST',
        data: JSON.stringify(jobQuery),
        success: function (result) {
            $(".load-jobs").remove();
            //var prev = GetPreviousJobId(jobId);
            //var nextJobId = GetNextJobId(jobId);

            if (!isMobile) {
                $("#grid").removeClass("Mobile-Job-Details");
                //BuildJobPopup(result, jobDetails, prev, nextJobId, companyName);
                $("#JobDetails").html(result);
                var className = $("#grid-view").attr("class");
                /*if (className == "active-view") {
                    $('.job-desc').slimScroll({ height: '500px', width: '595px', alwaysVisible: true });
                }
                else {
                    $('.job-desc').slimScroll({ height: '400px', width: '395px', alwaysVisible: true });
                }*/
            }
            else {

                var cn = $("<div/>").attr("class", "company-name").html(decodeURIComponent(companyName));
                //var jobTitle = $("<div/>").attr("class", "job-title").html(decodeURIComponent(jobDetails.find("#job-title").html()));
                var jobDescription = $("<div/>").attr("class", "job-desc").html(result);
                var close = $("<span/>").addClass("close-job-details").html("X").click(function () {
                    if (isMapView) {
                        $(".filter.mobile-view").show();
                        $("#grid").hide();
                    }
                    else {
                        $(".left-nav.mobile-view").show();
                        $(".filter.mobile-view").show();
                        $("#grid").hide();
                    }
                    $('#information_div').hide("");
                });

                $("#grid").append(cn);
                //$("#grid").append(jobTitle);
                $("#grid").html("");
                $("#grid").append(jobDescription);
                $("#grid").append(close);
                $("#grid").addClass("Mobile-Job-Details");

            }
        }
    });

    return false;
}

function BuildJobPopup(json, jobDetails, prevJobId, nextJobId, companyName) {
    // Header - Logo - Company Name, Address, Website
    // Tab 1 - Job title, Job description, 
    // Tab 2 - Experience, Salary, Role/Industry
    // Tab 3 - About company
    // Tab 4 - Other jobs for this company in this office
    // Tab 5 - Video for this company
    // Footer - Source - Source Link - Apply
    //var jobDetails = $("<div/>").attr("id", "job-details");
    $(jobDetails).append(GetHeaderElement(json, prevJobId, nextJobId, companyName));
    $(jobDetails).append(GetJobDetailsTab(json));
    //$(jobDetails).append(GetJobExpTab(json));
    //$(jobDetails).append(PopulateOtherJobs(json));
    //$(jobDetails).append(PopulateCompanyVideo(json));
    //$(jobDetails).append(GetJobDetailsFooter(json));
    return $(jobDetails);
}

function GetHeaderElement(json, prevJobId, nextJobId, companyName) {

    /*var address = JSON.parse(json.Location);
 
    if (address[0].Street != null && address[0].City != null && address[0].PostalCode != null)
        address = address[0].Street + ", " + address[0].City + ", " + address[0].PostalCode;
    else
        address = "";
     */
    var closeButton = $("<span onclick='hide_info_div()' style='position:absolute;right:10px;top:10px;cursor:pointer;' align='' class=\"icon-remove\"></span>");
    var nextJobButton = $("<span id=\"btn-next-job\" title=\"View Next Job\" style=\"background-image: url(static/img/next.png); background-size: 16px 16px; min-width: 16px; min-height: 16px; position: absolute; cursor: pointer; font-size: 10px; right: 30px;\" onclick='GetJobDetails(" + nextJobId + ")'></span>");
    var prevJobButton = $("<span id=\"btn-prev-job\" title=\"View Previous Job\" style=\"background-image: url(static/img/prev.png); background-size: 16px 16px; min-width: 16px; min-height: 16px; position: absolute; cursor: pointer; font-size: 10px; right: 50px;\" onclick='GetJobDetails(" + prevJobId + ")'></span>");

    var headerContainer = $("<div/>").attr("id", "header-container");
    var cn = $("<div/>").attr("class", "company-name").html(decodeURIComponent(companyName));
    /*var logoContainer = $("<div/>").attr("class", "logo-container");
    var logoImg = $("<img/>").attr("class", "company-logo").attr("src", "logo/" + json.LogoUrl);
    
    var companyAddress = $("<div/>").attr("class", "company-address").html(address);
    var companyPhone = $("<div/>").attr("class", "company-phone").html(json.Telephone);
    var companyWebsite = $("<div/>").attr("class", "company-website").html(json.WebsiteURL);
 
    $(logoContainer).append(logoImg);
    $(headerContainer).append(logoContainer);
    
    $(headerContainer).append(companyAddress);
    //$(headerContainer).append(companyPhone);
    */
    $(headerContainer).append(cn);
    //if (prevJobId != null)
    //    $(headerContainer).append(prevJobButton);

    //if (nextJobId != null)
    //    $(headerContainer).append(nextJobButton);

    $(headerContainer).append(closeButton);
    return $(headerContainer);
}
function GetJobList(currentJobId) {
    var companyId;
    var jobIds = [];
    var JOB_LIST = JSON.parse($("#hfJobs").val());

    JOB_LIST.forEach(function (job) {
        if (job.Id == currentJobId)
            companyId = job.CompanyId;
    });

    JOB_LIST.forEach(function (job) {
        if (job.CompanyId == companyId)
            jobIds.push(job.Id);
    });

    return jobIds;
}
function GetPreviousJobId(jobId) {
    var previousId = null;
    var isSet = false;
    var jobList = GetJobList(jobId);

    jobList.forEach(function (job) {
        if (job != jobId && !isSet) {
            previousId = job;
            isSet = true;
        }
        else if (job == jobId)
            isSet = true;
    });

    return previousId;
}

function GetNextJobId(jobId) {
    var counter = 1;
    var nextId;
    var jobList = GetJobList(jobId);

    jobList.forEach(function (job) {
        if (job == jobId && jobList[counter]) {
            nextId = jobList[counter];
        }

        counter++;
    });

    return nextId;
}

function GetJobDetailsTab(json) {

    var jobContainer = $("<div/>").attr("id", "job-container");
    //var jobTitle = $("<div/>").attr("class", "job-title").html("<b>Title:</b> " + json.JobTitle);

    var jobDescription = $("<div/>").attr("class", "job-desc").append(json.replace(/€™/g, "'").replace(/€˜/g, "\"").replace(/Â/g, "").replace(/â/g, "").replace(/€¢/g, "").replace(/€“/g, "").replace(/€/g, "\""));

    //var jobDescription = $("<div/>").attr("class", "job-desc").append(json.JobDescription.replace(/€™/g, "'").replace(/€˜/g, "\"").replace(/Â/g, "").replace(/â/g, "").replace(/€¢/g, "").replace(/€“/g, "").replace(/€/g, "\""));
    //var jobCategory = $("<div/>").attr("class", "job-cat").html("<b>Job category: </b>" + json.JobCategory);

    //$(jobContainer).append(jobTitle);
    //$(jobContainer).append(jobCategory);
    $(jobContainer).append(jobDescription);
    //$(jobContainer).append(jobSource);

    return $(jobContainer);
}

function GetJobExpTab(json) {
    var jobContainer = $("<div/>").attr("id", "job-exp-container");
    var jobSkills = $("<div/>").attr("class", "job-skills").html(json.SkillSetRequired);
    var jobExperience = $("<div/>").attr("class", "job-exp").html(json.Experience);
    var jobEducation = $("<a/>").attr("class", "job-education").html(json.Education);
    var jobSalary = $("<div/>").attr("class", "job-salary").html(json.SalaryRange);
    var jobCategory = $("<div/>").attr("class", "job-category").html("<b>Category:</b> " + json.JobCategory + " - " + json.JobSubCategory);
    var jobTags = $("<div/>").attr("class", "job-tags").html(json.Tags);

    $(jobContainer).append(jobSkills);
    $(jobContainer).append(jobExperience);
    $(jobContainer).append(jobEducation);
    $(jobContainer).append(jobSalary);
    $(jobContainer).append(jobCategory);
    $(jobContainer).append(jobTags);

    return $(jobContainer);
}

// Other open jobs in same location
function GetOtherJobs(companyId, location) {
    $.ajax({
        url: "handlers/GetOtherJobs.ashx?cid=" + companyId + "&loc=" + location,
        success: function (result) {
            PopulateOtherJobs(result);
        }
    });
}

function PopulateOtherJobs(json) {
    var otherJobContainer = $("<div/>").attr("id", "other-job-container");

    for (i = 0; i < json.length; i++) {
        var otherJobRecord = $("<div/>").attr("class", "other-job-row");
        var otherJobTitle = $("<div/>").attr("class", "other-job-title");
        var otherJobDate = $("<div/>").attr("class", "other-job-date");
        var otherJobCat = $("<div/>").attr("class", "other-job-cat");
        var otherJobTags = $("<div/>").attr("class", "other-job-tags");

        $(otherJobRecord).append(otherJobTitle);
        $(otherJobRecord).append(otherJobDate);
        $(otherJobRecord).append(otherJobCat);
        $(otherJobRecord).append(otherJobTags);
        $(otherJobContainer).append(otherJobRecord);
    }

    return $(otherJobContainer);
}

function PopulateCompanyVideo(youtubeUrl) {
    var videoContainer = $("<div/>").attr("id", "company-video");
    var vid = $("<iframe/>").attr("src", youtubeUrl).attr("class", "company-video");

    $(videoContainer).append($(vid));
    return $(videoContainer);
}

function GetJobDetailsFooter(json) {
    var jobSource = $("<div/>").attr("class", "job-source").html("Source: ");
    var jobSourceUrl;
    if (json.JobSourceName == "Indeed")
        jobSourceUrl = $("<a/>").attr("class", "job-source-url").attr("link", json.SourceUrl).attr("target", "_blank").html("<img src=\"http://www.indeed.com/p/jobsearch.gif\" style=\"border: 0;\" alt=\"Powered by Indeed Job Search\" />");
    else {
        jobSourceUrl = $("<a/>").attr("class", "job-source-url").attr("link", json.SourceUrl).attr("target", "_blank").html(json.JobSourceName).click(function () {
            trackJobApply(json.CompanyId, json.Id);
            trackJobUrl(json.SourceUrl, $(".active-view").html());
        });
    }

    var apply = $("<div/>").attr("class", "job-apply btn-primary").html("<a link=\"" + json.SourceUrl + "\" onclick=\"CheckUserSignup();trackJobLink('" + json.SourceURL + "')\" target=\"_blank\" style=\"color: white\">Apply</a>");

    $(jobSource).append(jobSourceUrl);
    $(jobSource).append(apply);
    return $(jobSource);
}