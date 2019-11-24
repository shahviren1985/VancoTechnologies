var DOCS;
var CURRENTID;
var BASE_URL;

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
    var id = $("<div/>").attr("class", "grid-header-cell").html("View").css("width", "60px");
    var documentTitle = $("<div/>").attr("class", "grid-header-cell").html("Document Title").css("width", "300px");
    var from = $("<div/>").attr("class", "grid-header-cell").html("From").css("width", "200px");
    var lastModified = $("<div/>").attr("class", "grid-header-cell").html("Last Modified").css("width", "200px");
    var fileTags = $("<div/>").attr("class", "grid-header-cell").html("Last Modified By").css("width", "150px");
    var status = $("<div/>").attr("class", "grid-header-cell").html("Status").css("width", "100px");

    $(header).append(id);
    $(header).append(documentTitle);
    $(header).append(from);
    $(header).append(lastModified);
    $(header).append(fileTags);
    $(header).append(status);

    return header;
}

function GetGridRecord(record) {
    var msg = JSON.parse(record.MessageHeader);
    var row = $("<div/>").attr("class", "grid-row");

    if (CURRENTID == record.Id)
        $(row).attr("style", "background-color: #F8E4E4;");

    var ahref = "<a href='Documents.aspx?id=" + record.Id + "&doctype=" + msg.DocumentType + "'>View</a>"

    var id = $("<div/>").attr("class", "grid-cell").html(ahref).css("text-align", "center").css("width", "60px");
    //var documentTitle = $("<div/>").attr("class", "grid-cell").html(decodeURIComponent(record.FriendlyName)).css("width", "300px");
    var documentTitle = $("<div/>").attr("class", "grid-cell").html(record.FriendlyName).css("width", "300px");
    var from = $("<div/>").attr("class", "grid-cell").html(decodeURIComponent(msg.From)).css("text-align", "center").css("width", "200px");
    var lastModified = $("<div/>").attr("class", "grid-cell").html(GetDate(record.LastModified)).css("text-align", "center").css("width", "200px");
    var fileTags = $("<div/>").attr("class", "grid-cell").html(record.LastModifiedBy).css("width", "150px").css("text-align", "center");
    var status = $("<div/>").attr("class", "grid-cell").html(record.DocumentStatus).css("text-align", "center").css("width", "100px");

    if (record.DocumentStatus == "DISCARDED") {
        $(row).attr("class", "grid-row discarded");
    }

    $(row).append(id);
    $(row).append(documentTitle);
    $(row).append(from);
    $(row).append(lastModified);
    $(row).append(fileTags);
    $(row).append(status);
    return row;
}

function GetUserDocuments(docId) {
    CURRENTID = docId;
    CallHandler("id=" + docId, "api/GetDocumentHistory.ashx", PrepareGrid);
}

function GetHistoryGridContent(docId) {
    GetUserDocuments(docId);
}

function GetDate(date) {
    var d = date.split(' ');
    var newDate = d[0];
    var dd = newDate.split('/');

    var month = dd[0];
    var year = dd[2];
    var day = dd[1];

    return dd[1] + "-" + dd[0] + "-" + dd[2] + " " + d[1] + " " + d[2];

}

function SearchDocuments() {
    CallHandler("q=" + $(".search-text").val(), "api/SearchDocuments.ashx", PrepareGrid);
}

