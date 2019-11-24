$(document).ready(function () {
    var userCode = localStorage.getItem("userCode");
    var userName = localStorage.getItem("userName");
    var RoleType = localStorage.getItem("roleType");

    $("#userName").html("Welcome " + userName);

    if (userCode == null || userCode == undefined || !userCode) {
        localStorage.clear();
        window.location.href = "index.html";
    }

    $("#logout").click(function () {
        localStorage.clear();
        window.location.href = "index.html";
    });
});