
function IsJobQualify(job) {
    var val = $("#slider").slider("option", "values");
    var jobOlder = GetDateDifference(job.datePosted);
    var l = 60 - Math.round(val[0] * 0.60);
    var h = 60 - Math.round(val[1] * 0.60);
    
    if (jobOlder >= h && jobOlder <= l)
        return true;
    //else if (jobOlder > 60 && h == 0)
    //    return true;
    else
        return false;
}

function GetDateDifference(datePosted) {
    var one_day = 1000 * 60 * 60 * 24;
    var posted;

    if (datePosted == undefined)
        return 0;

    if (datePosted.indexOf("Date") > -1) {
        posted = eval(datePosted.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
        
        //posted = new Date("\"" + currentDate + "\"");
    }
    else {
        posted = parseDate(datePosted).getTime();
    }

    
    var currentDate = new Date().getTime();

    var difference_ms = currentDate - posted;
    
    var diff = Math.round(Math.round(difference_ms / one_day));
    return diff;
}

var GetQualifiedJobs = function (jobList) {
    //var jobList = JSON.parse($("#hfJobs").val());

}


