<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: Advanced Search</title>
    <link href="static/stylesheets/reset.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.css" rel="stylesheet" />
    <link href="static/stylesheets/style.css" rel="stylesheet" />
    <script src="static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.dashboard.js" type="text/javascript"></script>
    <script type="text/javascript" src="static/scripts/zebra_datepicker.src.js"></script>
    <script type="text/javascript" src="static/scripts/controls/aa.store.location.js"></script>
    <link href="static/stylesheets/StyleSheet.css" rel="stylesheet" />

    <style type="text/css">
        .Search-Option-Row {
            float: left;
            width: 100%;
            margin: 5px;
            margin-left: 8%;
            text-align: left;
            min-height: 30px;
        }

            .Search-Option-Row span {
                width: 150px;
                float: left;
            }

        select {
            width: auto;
            margin-bottom: 0px;
        }

        input[type="radio"], input[type="checkbox"] {
            margin-top: 0px;
            margin-right: 5px;
        }

        .doc-type-container input[type="radio"] {
            width: 20px;
        }

        .location-container {
            display: none;
        }
    </style>

    <link href="../static/stylesheets/bootstrap.icon-large.min.css" rel="stylesheet" />
    <script type="text/javascript">
        var BASE_URL = "";
        var College = 'mnwc';

        if (window.location.host.indexOf("localhost") > -1) {
            BASE_URL = "http://localhost:45117/";
        }
        else {
            //BASE_URL = "http://myclasstest.com/dms/";
            BASE_URL = "https://vancotech.com/dms/";
        }

        LoadNotifications();

        $(document).ready(function () {
            $(".nav").find("li a").each(function () {
                var href = $(this).attr("href");
                if (href != "undefined" && href != null && href.indexOf(".aspx") > -1) {
                    $(this).attr("href", BASE_URL + $(this).attr("href"));
                }
            });

            $("#a-not").click(function () {
                if ($("#notifications").attr("display") == "block") {
                    $("#notifications").hide(500);
                    $("#notifications").attr("display", "none");
                }
                else {
                    $("#notifications").show(500);
                    $("#notifications").attr("display", "block");
                }
            });

            $("#btnclose").click(function () {
                $("#notifications").hide(500);
                $("#notifications").attr("display", "none");
            });

            $(".globe").attr("src", BASE_URL + "static/images/notification-globe.png");
        });

        function LoadNotifications() {
            var fullUrl = BASE_URL + "api/AdvancedSearch.ashx?t=nt";
            $.ajax({
                url: fullUrl,
                type: 'GET',
                dataType: 'text',
                success: PopulateNotifications
            });
        }

        function PopulateNotifications(data) {
            data = JSON.parse(data);

            $("#not-body").empty();

            if (data.length > 0) {
                $("#not-link").attr("style", "background-color:red");
                for (var i = 0; i < data.length; i++) {

                    var title = data[i].FriendlyName;
                    if (data[i].FriendlyName.length > 26)
                        data[i].FriendlyName = data[i].FriendlyName.substring(0, 26) + " [...]";

                    var ahref = "<a title='" + title + "' href='" + BASE_URL + "Documents.aspx?id=" + data[i].Id + "&doctype=" + data[i].DocumentType + "'>" + data[i].FriendlyName + " (" + data[i].Deadline + ")</a>";
                    $("#not-body").append($("<div/>").attr("class", "not-row").html(ahref));
                }
            }
            else {
                $("#not-link").attr("style", "background-color:transparent");
                $("#not-body").append($("<div/>").attr("class", "not-row").html("No documents founds."));
            }
        }
    </script>
    <style>
        #notifications {
            width: 300px;
            /* height: 200px; */
            position: absolute;
            /* float: right; */
            right: 0;
            border: 1px solid #ddd;
            background-color: #fff;
            text-align: left;
            cursor: pointer;
        }

        #not-header {
            float: left;
            width: 97%;
            padding: 5px;
            border-bottom: 1px solid #ddd;
        }

        #not-body {
            float: left;
            width: 100%;
            /* border-bottom: 1px solid #ddd; */
        }

        .not-row {
            padding: 5px;
            width: 97%;
            border-bottom: 1px solid #ddd;
            padding: 10px 5px 10px 5px;
        }

            .not-row:hover {
                background-color: aliceblue;
            }

        #not-counter {
            background-color: red;
            padding: 2px 4px;
            color: white;
            border-radius: 4px;
            font-size: 10px;
            position: absolute;
            top: 10px;
            right: 5px;
        }

        .notification-globe {
            display: inline-block;
            height: 28px;
            line-height: 28px;
            vertical-align: text-bottom;
            width: 28px;
        }

        .search-text, input[type='text'] {
            height: 20px;
            margin-bottom: 0px;
            width: 300px;
        }

        .marathi-key {
            float: left;
            margin-left: 5px;
            margin-top: 10px;
            padding: 5px;
            border: 1px solid #333;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="GridContainer">
        <div id="SearchContainer">
            <div class="search-bar">
                <input type="text" id="searchText" placeholder="Search documents..." class="search-text" />
                <div id="marathi-key-pop" title="Type in मराठी" class="marathi-key">M</div>
            </div>
            <!-- search by General search -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" name="search-filter" value="0" tp="gs" />General Search</span>
            </div>

            <!-- search by stored cupboard locatoin -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="cb" name="search-filter" value="0" />By Cupboard</span>
                <div class="location-container" id="cupbord-options">
                    <span class="cupboard-container">
                        <select id="cupboards">
                        </select></span>
                    <span class="shelf-container">
                        <select id="shelfs">
                        </select></span>
                </div>

            </div>
            <!-- search by stored file location -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="fl" name="search-filter" value="0" />By File</span>
                <div class="location-container" id="file-options">
                </div>

            </div>

            <!-- search by Department -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="dp" name="search-filter" value="0" />By Department</span>
                <div class="location-container" id="depart-options">
                    <span class="cupboard-container">
                        <select id="department" multiple="multiple">
                        </select></span>
                </div>
            </div>

            <!-- search by Assigned User-->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="au" name="search-filter" value="0" />Assigned to User</span>
                <div class="location-container" id="assigned-user-options">
                    <span class="cupboard-container">
                        <select id="assignedUser" multiple="multiple">
                        </select>
                    </span>
                </div>
            </div>

            <!-- search by Document type (Inward/Outward) -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="dt" name="search-filter" value="0" />By Document Type</span>
                <span class="doc-type-container" style="width: 500px; float: left; display: none">
                    <input type="radio" name="doc-type" value="all" checked="checked" />All                 
                    <input type="radio" name="doc-type" value="inward" />Inward                 
                    <input type="radio" name="doc-type" value="outward" />Outward
                </span>
            </div>

            <!-- search by Deadline -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="dl" name="search-filter" value="0" />By Deadline</span>
                <div class="location-container" id="dead-line-options">
                    <span class="cupboard-container">
                        <input type="text" id="txtDeadline" style="width: 100px;" placeholder="Deadline date" />
                    </span>
                </div>
            </div>

            <!-- search by Date Range -->
            <div class="Search-Option-Row">
                <span>
                    <input type="radio" tp="dr" name="search-filter" value="0" />By Date Range</span>
                <div class="location-container" id="date-range-options">
                    <span class="cupboard-container">
                        <input type="text" id="txtStartDate" style="width: 100px;" placeholder="Start date" />
                    </span>
                    <span class="shelf-container">
                        <input type="text" id="txtEndDate" style="width: 100px;" placeholder="End date" />
                    </span>
                </div>
            </div>
            <div class="search-button btn btn-success" style="text-align: left; float: left; margin-left: 8%; margin-top: 20px;" onclick="AdvancedSearchDocuments();">Search</div>
        </div>
        <div id="Grid"></div>
    </div>
    <script type="text/javascript">
        var STORE_LOCATION;
        $(document).ready(function () {
            CallHandler("", "api/GetStoreLocations.ashx", PopulateLocations);
            CallHandler("", "api/GetDepartments.ashx", PopulateDepartments);
            CallHandler("type=all", "api/GetUsers.ashx", PopulateUsers);
            //GetGridContent();

            $('#txtStartDate').Zebra_DatePicker({ format: 'd-m-Y' });
            $('#txtEndDate').Zebra_DatePicker({ format: 'd-m-Y' });
            $('#txtDeadline').Zebra_DatePicker({ format: 'd-m-Y' });

            $("#marathi-key-pop").click(function () {
                window.open("https://translate.google.com/?hl=mr#mr/en", "_blank", "height=450, width=700");
            });

            $("#SearchContainer").find(".Search-Option-Row").each(function () {
                $(this).find("input[type='radio']").each(function () {
                    $(this).click(function () {
                        if ($(this).prop("checked") == true) {
                            //alert($(this).attr("tp"));
                            if ($(this).attr("tp") == "gs") {
                                $('#cupbord-options').hide();
                                $('#depart-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').hide();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').hide();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "cb") {
                                $('#cupbord-options').show();
                                $('#depart-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').hide();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').hide();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "dp") {
                                $('#depart-options').show();
                                $('#cupbord-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').hide();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').hide();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "dr") {
                                $('#depart-options').hide();
                                $('#cupbord-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').show();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').hide();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "dl") {
                                $('#depart-options').hide();
                                $('#cupbord-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').hide();
                                $('#dead-line-options').show();
                                $('#assigned-user-options').hide();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "au") {
                                $('#depart-options').hide();
                                $('#cupbord-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').hide();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').show();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "dt") {
                                $('#depart-options').hide();
                                $('#cupbord-options').hide();
                                $('.doc-type-container').show();
                                $('#date-range-options').hide();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').hide();

                                $('#file-options').hide();
                            }
                            else if ($(this).attr("tp") == "fl") {
                                $('#depart-options').hide();
                                $('#cupbord-options').hide();
                                $('.doc-type-container').hide();
                                $('#date-range-options').hide();
                                $('#dead-line-options').hide();
                                $('#assigned-user-options').hide();

                                $('#file-options').show();
                            }

                        }
                    });
                });
            });
        });

        function AdvancedSearchDocuments() {
            var searchType = "gs";

            $("#SearchContainer").find(".Search-Option-Row").each(function () {
                if ($(this).find("input[type='radio']").prop("checked") == true) {
                    searchType = $(this).find("input[type='radio']").attr("tp");
                }
            });

            var searchQuery = "";

            if (searchType == "gs") {
                CallHandler("q=" + $(".search-text").val(), "api/SearchDocuments.ashx", PopulateSearchResults)
                return;
            }
            else if (searchType == "cb") {
                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&cup=" + $("#cupboards").find("option:selected").html() + "&shelf=" + $("#shelfs").find("option:selected").html();
            }
            else if (searchType == "fl") {
                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&rm=" + $("#dd-sl-Room").find("option:selected").html() + "&cup=" + $("#dd-sl-Cupboard").find("option:selected").html() + "&shelf=" + $("#dd-sl-Shelf").find("option:selected").html() + "&file=" + $("#dd-sl-File").find("option:selected").html();
            }
            else if (searchType == "dp") {
                var departmets = "";
                $('#department :selected').each(function (i, selected) {
                    departmets += $(selected).text() + ",";
                });

                if (departmets == "") {
                    alert("please select department");
                    return;
                }

                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&dept=" + departmets;
            }
            else if (searchType == "dr") {
                var sDate = $("#txtStartDate").val();
                var eDate = $("#txtEndDate").val();

                if (sDate == "" || eDate == "") {
                    alert("Please select date");
                    return;
                }

                var startDate = new Date(sDate.split('-')[2], sDate.split('-')[1] - 1, sDate.split('-')[0]);
                var endDate = new Date(eDate.split('-')[2], eDate.split('-')[1] - 1, eDate.split('-')[0]);

                startDate = Date.parse(sDate);
                endDate = Date.parse(eDate);


                if (endDate < startDate) {
                    alert("End date should be greater then equal to start date");
                    return;
                }

                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&sd=" + sDate + "&ed=" + eDate;

            }
            else if (searchType == "dl") {
                var deadLineDate = $("#txtDeadline").val();

                if (deadLineDate == "") {
                    alert("Please select date");
                    return;
                }

                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&dld=" + deadLineDate;
            }
            else if (searchType == "au") {
                var assignedUser = "";
                $('#assignedUser :selected').each(function (i, selected) {
                    assignedUser += $(selected).text() + ",";
                });

                if (assignedUser == "") {
                    alert("please select user");
                    return;
                }

                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&user=" + assignedUser;
            }
            else if (searchType == "dt") {
                var docType = "all"
                $(".doc-type-container").find("input[type='radio']").each(function () {
                    if ($(this).prop("checked") == true) {
                        docType = $(this).attr("value");
                    }
                });

                searchQuery = "t=" + searchType + "&q=" + $(".search-text").val() + "&dt=" + docType;
            }

            if (searchQuery != "") {
                CallHandler(searchQuery, "api/AdvancedSearch.ashx", PopulateSearchResults);
            }
        }

        function PopulateSearchResults(data) {
            DOCS = JSON.parse(data);
            var counter = 1;
            $("#Grid .grid").remove();

            if (DOCS.length > 0) {

                // Prepare grid header
                var grid = $("<div/>").attr("class", "grid");
                $(grid).append(GetSearchGridHeader());
                $("#Grid").append(grid);

                // Prepare grid
                DOCS.forEach(function (doc) {
                    try {
                        var row = GetGridRecord(doc);
                        $(grid).append(row);
                    }
                    catch (e) {
                        console.log(e.message);
                        console.log(doc);
                    }

                    counter++;
                });
            }
            else {
                var grid = $("<div/>").attr("class", "grid").html("No documents found for search query").css("text-align", "left");
                $("#Grid").append(grid);
            }
        }

        function StripChars(input, len) {
            var retValue = input;
            if (input && input.length > len) {
                retValue = retValue.substring(0, len) + "...";
            }

            return retValue;
        }

        function CleanChars(input) {
            return input.replace("%", "");
        }

        function GetGridRecord(record) {
            var msg = JSON.parse(decodeURIComponent(record.MessageHeader));

            var row = $("<div/>").attr("class", "grid-row");
            var ahref = "<a href='Documents.aspx?id=" + record.Id + "&doctype=" + msg.DocumentType + "'>View</a>"
            var friendlyName = CleanChars(record.FriendlyName);
            var id = $("<div/>").attr("class", "grid-cell").html(ahref).css("text-align", "center").css("width", "60px");

            var documentTitle = $("<div/>").attr("class", "grid-cell").html(StripChars(decodeURIComponent(friendlyName), 40)).css("width", "300px").attr("title", decodeURIComponent(friendlyName));
            var dateCreated = $("<div/>").attr("class", "grid-cell").html(GetDate(record.LastModified)).css("text-align", "center").css("width", "150px");

            var department = $("<div/>").attr("class", "grid-cell").html(StripChars(record.DepartmentId, 10)).css("text-align", "center").css("width", "120px").attr("title", decodeURIComponent(record.DepartmentId));
            var users = $("<div/>").attr("class", "grid-cell").html(StripChars(record.TaggedUsers, 10)).css("width", "120px").css("text-align", "center").attr("title", decodeURIComponent(record.TaggedUsers));

            var fileTags = $("<div/>").attr("class", "grid-cell").html(StripChars(decodeURIComponent(CleanChars(record.FileTags)), 10)).css("text-align", "center").css("width", "120px").attr("title", decodeURIComponent(record.FileTags));
            var inwardOutward = $("<div/>").attr("class", "grid-cell").html(msg.DocumentType).css("text-align", "center").css("width", "80px");

            if (record.DocumentStatus == "DISCARDED") {
                $(row).attr("class", "grid-row discarded");
            }

            $(row).append(id);
            $(row).append(documentTitle);
            $(row).append(dateCreated);
            $(row).append(department);
            $(row).append(users);
            $(row).append(fileTags);
            $(row).append(inwardOutward);

            return row;
        }

        function GetSearchGridHeader() {
            var header = $("<div/>").attr("class", "document-grid-header");
            var id = $("<div/>").attr("class", "grid-header-cell").html("View").css("width", "60px");
            var documentTitle = $("<div/>").attr("class", "grid-header-cell").html("Document Title").css("width", "300px");
            var dateCreated = $("<div/>").attr("class", "grid-header-cell").html("Last Modified").css("width", "150px");
            var department = $("<div/>").attr("class", "grid-header-cell").html("Department").css("width", "120px");
            var users = $("<div/>").attr("class", "grid-header-cell").html("Users").css("width", "120px");
            var fileTags = $("<div/>").attr("class", "grid-header-cell").html("File Tags").css("width", "120px");
            var inwardOutward = $("<div/>").attr("class", "grid-header-cell").html("I/O").css("width", "80px");

            $(header).append(id);
            $(header).append(documentTitle);
            $(header).append(dateCreated);
            $(header).append(department);
            $(header).append(users);
            $(header).append(fileTags);
            $(header).append(inwardOutward);
            return header;
        }

        function PopulateLocations(data) {
            var STORE_LOCATION = JSON.parse(data);
            console.log(STORE_LOCATION);
            if (STORE_LOCATION != null) {

                new GetStoreLocationControl("#file-options", STORE_LOCATION);

                for (i = 0; i < STORE_LOCATION.length; i++) {
                    var exist = false;
                    $("#cupboards").find("option").each(function () {
                        if (STORE_LOCATION[i].Cupboard == $(this).html()) {
                            exist = true;
                        }
                    });

                    if (!exist) {
                        var opt = $("<option/>").html(STORE_LOCATION[i].Cupboard);
                        $("#cupboards").append(opt);
                    }
                    //arr.push("Room (" + json[i].RoomNumber + ") -> Cupboard (" + json[i].Cupboard + ") -> Shelf (" + json[i].Shelf + ") -> File -> (" + json[i].FileName + ")");
                }

                var allCup = $("<option/>").html("All Cupboards").attr("selected", "selected");
                $("#cupboards").append(allCup);
                $("#cupboards").change(function () {
                    $("#shelfs").find("option").remove();
                    for (i = 0; i < STORE_LOCATION.length; i++) {
                        if (STORE_LOCATION[i].Cupboard == $(this).find("option:selected").html()) {
                            var opt = $("<option/>").html(STORE_LOCATION[i].Shelf);
                            $("#shelfs").append(opt);
                        }
                    }

                    var allShelfs = $("<option/>").html("All Shelfs").attr("selected", "selected");
                    $("#shelfs").append(allShelfs);
                });
            }
        }

        function PopulateDepartments(data) {
            var DEPARTMENTS = JSON.parse(data);
            console.log(DEPARTMENTS);
            if (DEPARTMENTS != null) {
                for (i = 0; i < DEPARTMENTS.length; i++) {
                    var exist = false;
                    $("#department").find("option").each(function () {
                        if (DEPARTMENTS[i].Name == $(this).html()) {
                            exist = true;
                        }
                    });

                    if (!exist) {
                        var opt = $("<option/>").html(DEPARTMENTS[i].DepartmentName);
                        $("#department").append(opt);
                    }
                }
            }
        }

        function PopulateUsers(data) {
            var USERS = JSON.parse(data);
            console.log(USERS);
            if (USERS != null) {
                for (i = 0; i < USERS.length; i++) {
                    var exist = false;
                    $("#assignedUser").find("option").each(function () {
                        if (USERS[i].UserName == $(this).html()) {
                            exist = true;
                        }
                    });

                    if (!exist) {
                        var opt = $("<option/>").html(USERS[i].UserName);
                        $("#assignedUser").append(opt);
                    }
                }
            }
        }
    </script>
</asp:Content>

