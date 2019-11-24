var lastKeyword;
/*
function GetAreaName() {
    var key = $("#search-term").val();

    if (lastKeyword == key)
        return;

    if (key.length < 1)
        $("#search-results").hide();

    try {
        var keyword = parseInt(key);

        if (!$.isNumeric(keyword)) {
            $.ajax({
                url: "handlers/GetAreaName.ashx?q=" + key + "&c=US",
                success: function (result) {
                    BuildSearchResult(result, "area");
                }
            });
        }
        else {
            $.ajax({
                url: "handlers/GetPinCode.ashx?q=" + key + "&c=US",
                success: function (result) {
                    BuildSearchResult(result, "pin");
                }
            });
        }
    }
    catch (Error) {

    }

    lastKeyword = key;
}*/

function BuildSearchResult(json, type) {
    //var areas = JSON.parse(json);
    $("#search-results").html("");
    var arr = [];

    for (i = 0; i < json.length && arr.length < 5; i++) {
        if (arr.indexOf(json[i]) > -1) {
            continue;
        }

        var searchRecord = $("<span/>").attr("class", "search-record").html(json[i]);

        $(searchRecord).hover(function () { $(this).addClass("search-record-highlight"); }, function () { $(this).removeClass("search-record-highlight"); });
        $(searchRecord).click(function () {
            $("#search-term").val($(this).text());
            $("#search-term").attr("areacode", $(this).attr("areacode"));
            $("#search-results").hide();
            trackSearch($(this).text());
            document.location = "Default.aspx?q=" + $(this).text();
        });

        $("#search-results").append(searchRecord);
        arr.push(json[i]);
    }

    if (json.length == 0) {
        searchRecord = $("<span/>").attr("class", "search-record").html("Sorry, no search results found.");
        $("#search-results").append(searchRecord);
    }

    $("#search-results").show();
}