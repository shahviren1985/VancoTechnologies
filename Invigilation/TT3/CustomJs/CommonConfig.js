var CommonPath = "http://localhost:60091/";
if (window.location.href.indexOf("localhost") != -1) {
    CommonPath = "http://localhost:60091/";
    //console.log("localhostServer")
} else if (window.location.href.indexOf("dev") != -1) {
    CommonPath = "http://vancotech.com/timetable/dev/";
    //console.log("Dev Server")
} else {
    CommonPath = "http://vancotech.com/timetable/prod/";
    //console.log("Production Server")
}



try {

    var currenturl = window.location.href.substr(window.location.href.lastIndexOf('/') + 1);
    var UserGroup = '';


    if (currenturl == '') {
        currenturl = 'Home'
    }
    $(".app-menu__item").removeClass("active");
    $('.' + currenturl).addClass("active");

    $('.app-menu__item').click(function () {
        $('.app-menu__item').removeClass('active');
        $(this).addClass('active');
    });

    (function () {
        "use strict";

        var treeviewMenu = $('.app-menu');

        // Toggle Sidebar
        $('[data-toggle="sidebar"]').click(function (event) {
            event.preventDefault();
            $('.app').toggleClass('sidenav-toggled');
        });

        // Activate sidebar treeview toggle
        $("[data-toggle='treeview']").click(function (event) {
            event.preventDefault();
            if (!$(this).parent().hasClass('is-expanded')) {
                treeviewMenu.find("[data-toggle='treeview']").parent().removeClass('is-expanded');
            }
            $(this).parent().toggleClass('is-expanded');
        });

        // Set initial active toggle
        $("[data-toggle='treeview.'].is-expanded").parent().toggleClass('is-expanded');

        //Activate bootstrip tooltips
        $("[data-toggle='tooltip']").tooltip();

    })();

    var LoggedU = JSON.parse(localStorage.getItem('LoggedUser'));
    console.log(LoggedU)
    if (LoggedU == undefined || LoggedU == null || LoggedU == '') {
        window.location.href = CommonPath + "#!/Login"
    } else {
        UserGroup = LoggedU.UserGroup;
        $('.UsernameOfLogged').html(LoggedU.Username);
        $('.designationOfUser').html(LoggedU.Role);
    }

} catch (e) {
    console.log(e)
}