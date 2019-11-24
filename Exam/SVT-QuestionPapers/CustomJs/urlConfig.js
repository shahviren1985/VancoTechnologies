var _CommonUr = "";
var _CommonurlUI = "";

if (window.location.href.indexOf("localhost") != -1) {
    _CommonUr = "http://localhost:7777/ExamService/api/";
    //_CommonUr = "http://vancotech.com/exams/bachelors/API/api/";
    _CommonurlUI = "http://localhost:33311/";
    //ExamSystem
    console.log("localhostServer")
} else if (window.location.href.indexOf("dev") != -1) {
    _CommonUr = window.location.origin + "/Exams/dev/API/api/";
    _CommonurlUI = window.location.origin + "/Exams/dev/ui";
    console.log("Dev Server")
} else {
    _CommonUr = window.location.origin + "/Exams/bachelors/API/api/";
    _CommonurlUI = window.location.origin + "/SVT-Staff/#/ui";
    console.log("Production Server")
}

var CURRENT_YEAR;
var d = new Date();
var n = d.getMonth();
var y = d.getFullYear();
if (n >= 6 && n < 12) {
    CURRENT_YEAR = y;
}
else {
    CURRENT_YEAR = y + 1;
}