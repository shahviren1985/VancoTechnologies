
var GloableWebsite = 'https://vancotech.com/StudentFeedback/dev/'

if (window.location.href.indexOf("localhost") != -1) {
    GloableWebsite = "http://localhost:51135/";
    console.log("localhostServer")
} else if (window.location.href.indexOf("dev") != -1) {
    GloableWebsite = "https://vancotech.com/StudentFeedback/dev/";
    console.log("Dev Server")
} else {
    GloableWebsite = "https://vancotech.com/StudentFeedback/prod/";
    console.log("Production Server")
}