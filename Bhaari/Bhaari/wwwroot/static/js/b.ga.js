(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-55842578-1', 'auto');
ga('require', 'displayfeatures');
ga('send', 'pageview');

var trackHomePageSearch = function (q) {
    ga('send', 'event', 'search', 'home', q);
}

var trackSearchEnter = function (page,q) {
    ga('send', 'event', 'search-enter', page, q);
}

var trackSearch = function (searchTerm) {
    ga('send', 'event', 'search', 'default', searchTerm);
}

var trackMapView = function () {
    ga('send', 'event', 'view', 'map');
}

var trackListView = function () {
    ga('send', 'event', 'view', 'list');
}

var trackMapList = function (jobID, companyName) {
    ga('send', 'event', 'maplist', companyName, jobID);
}

var trackMapMarker = function (searchterm, companyName) {
    ga('send', 'event', 'marker', searchterm, companyName);
}

var trackJobApply = function (companyName, jobid) {
    ga('send', 'event', 'jobapply', companyName, jobid);
}

var trackGetInvited = function () {
    ga('send', 'event', 'getinvited', 'click');
}

var trackContactUs = function () {
    ga('send', 'event', 'contactus', 'click');
}

var trackFeedbackOpen = function () {
    ga('send', 'event', 'feedback', 'click');
}

var trackFeedbackSubmited = function () {
    ga('send', 'event', 'feedbacksubmit', 'click');
}

var trackJobUrl = function (url, view) {
    // Map view or list view
    ga('send', 'event', 'joburl', view, url, {
        'hitCallback':
          function () {

          }
    });
}