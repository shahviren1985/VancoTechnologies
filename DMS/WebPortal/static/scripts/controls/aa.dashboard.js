var DOCS;
if (window.location.host.indexOf("localhost") > -1) {
    BASE_URL = "http://localhost:45117/";
}
else {
    //BASE_URL = "http://myclasstest.com/dms/";
    BASE_URL = "https://vancotech.com/dms/";
}

function CallHandler(searchText, url, OnComplete) {
    var fullUrl = BASE_URL + url + "?" + searchText;
    $.ajax({
        url: fullUrl,
        type: 'GET',
        dataType: 'text',
        success: OnComplete
    });

    return false;
}

function PrepareGrid(data) {
    DOCS = JSON.parse(data);

    $(".loading").hide();

    $("#Grid .grid").remove();

    if (DOCS.length > 0) {
        var grid = $("<div/>").attr("class", "grid");
        $(grid).append(GetGridHeader());


        DOCS.forEach(function (doc) {
            var row = GetGridRecord(doc);
            $(grid).append(row);
        });

        $("#Grid").append(grid);
    }
    else {
        var grid = $("<div/>").attr("class", "grid").html("Currently you don't have any document.").css("text-align", "left");
        $("#Grid").append(grid);
    }
}

function GetGridHeader() {
    var header = $("<div/>").attr("class", "document-grid-header");
    var id = $("<div/>").attr("class", "grid-header-cell").html("View").css("width", "50px");
    var documentTitle = $("<div/>").attr("class", "grid-header-cell").html("Document Title").css("width", "280px");
    var from = $("<div/>").attr("class", "grid-header-cell").html("From").css("width", "180px");
    var lastModified = $("<div/>").attr("class", "grid-header-cell").html("Last Modified").css("width", "180px");
    var fileTags = $("<div/>").attr("class", "grid-header-cell").html("Last Modified By").css("width", "130px");
    var status = $("<div/>").attr("class", "grid-header-cell").html("Status").css("width", "80px");
    var correspondance = $("<div/>").attr("class", "grid-header-cell").html("Correspondance").css("width", "80px");

    $(header).append(id);
    $(header).append(documentTitle);
    $(header).append(from);
    $(header).append(lastModified);
    $(header).append(fileTags);
    $(header).append(status);
    $(header).append(correspondance);

    return header;
}

function GetGridRecord(record) {
    var msg = JSON.parse(record.MessageHeader);
    var row = $("<div/>").attr("class", "grid-row");
    var ahref = "<a href='Documents.aspx?id=" + record.Id + "&doctype=" + msg.DocumentType + "'>View</a>";
    //console.log(record.FriendlyName);
    var id = $("<div/>").attr("class", "grid-cell").html(ahref).css("text-align", "center").css("width", "50px");
    var documentTitle = $("<div/>").attr("class", "grid-cell").html(record.FriendlyName).css("width", "280px");
    var from = $("<div/>").attr("class", "grid-cell").html(decodeURIComponent(msg.From)).css("text-align", "center").css("width", "180px");
    var lastModified = $("<div/>").attr("class", "grid-cell").html(GetDate(record.LastModified)).css("text-align", "center").css("width", "180px");
    var fileTags = $("<div/>").attr("class", "grid-cell").html(record.LastModifiedBy).css("width", "130px").css("text-align", "center");
    var status = $("<div/>").attr("class", "grid-cell").html(record.DocumentStatus).css("text-align", "center").css("width", "80px");

    var chref = "<a href='History.aspx?id=" + record.Id + "'>View</a>";
    //console.log(record.ParentId);
    if (record.ParentId == -1 || record.ParentId == 0)
        chref = "-";
    var correspondance = $("<div/>").attr("class", "grid-cell").html(chref).css("text-align", "center").css("width", "100px");

    if (record.DocumentStatus == "DISCARDED") {
        $(row).attr("class", "grid-row discarded");
    }

    $(row).append(id);
    $(row).append(documentTitle);
    $(row).append(from);
    $(row).append(lastModified);
    $(row).append(fileTags);
    $(row).append(status);
    $(row).append(correspondance);
    return row;
}

function GetUserDocuments() {    
    $(".loading").show();
    CallHandler("", "api/GetRecentDocuments.ashx", PrepareGrid);
}

function GetGridContent() {
    GetUserDocuments();
}

function GetDate(date) {
    var d = date.split(' ');
    var newDate = d[0];
    var dd = newDate.split('/');

    if (newDate.indexOf('-') > 0) {
        dd = newDate.split('-');
    }

    var month = dd[0];
    var year = dd[2];
    var day = dd[1];

    if (!d[2])
        d[2] = "";

    //return dd[1] + "-" + dd[0] + "-" + dd[2] + " " + d[1] + " " + d[2];
    return dd[0] + "-" + dd[1] + "-" + dd[2] + " " + d[1] + " " + d[2];

}

function SearchDocuments() {
    $(".loading").show();
    CallHandler("q=" + $(".search-text").val(), "api/SearchDocuments.ashx", PrepareGrid);
}

