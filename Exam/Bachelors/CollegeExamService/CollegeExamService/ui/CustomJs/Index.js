

var LoggedUser = localStorage.getItem('LoggedInUserDetail');
$('#IsAuthoUser').css('display', 'block');
if (LoggedUser != undefined) {
    try {
        LoggedUser = JSON.parse(LoggedUser);
        $('.UsrnmTextspn').html(LoggedUser.firstName + ' ' + LoggedUser.lastName);
        $('#IsAuthoUser').css('display', 'block');
    } catch (e) {
        console.log(e);
        if (window.location.href.indexOf("PrintHallTicket.html") <= -1) {
            window.location.href = _CommonurlUI + '/#!/Login';
            $('body').css('display', 'block');
        }
    }
} else {
    if (window.location.href.indexOf("PrintHallTicket.html") <= -1 && window.location.href.indexOf("ApproveElective.html") <= -1 && window.location.href.indexOf("SelectElective.html") <= -1) {
        window.location.href = _CommonurlUI + '/#!/Login';
        $('body').css('display', 'block');
    }
}


function CallAPI(url, RequestType, data) {
    return $.ajax({
        cache: false,
        url: _CommonUr + url,
        dataType: "json",
        contentType: "application/json",
        type: RequestType,
        data: data
    });
}
function CallAPISaveFile(url, data) {
    return $.ajax({
        "async": true,
        cache: false,
        processData: false,
        "url": _CommonUr + url,
        "headers": {
            "content-type": "application/json",
            "cache-control": "no-cache",
        },
        "method": "POST",
        "processData": false,
        data: data
    });
}

function SaveExamResultTodb(url, data) {
    return $.ajax({
        "async": true,
        "crossDomain": true,
        "url": _CommonUr + url,
        "method": "POST",
        "headers": {
            "content-type": "application/json",
            "cache-control": "no-cache"
        },
        "processData": false,
        "method": "POST",
        "processData": false,
        data: data
    });
}

function CallAPIWithFile(url, data) {
    return jQuery.ajax({
        url: _CommonUr + url,
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        method: 'POST',
        type: 'POST', // For jQuery < 1.9
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function BindSelectSemesterBasedonCourse(CourseSeleted, ControllId) {
    if (CourseSeleted != '') {
        var selectedCatrol = $('#' + ControllId)
        document.getElementById(ControllId).options.length = 0;
        selectedCatrol.append('<option selected="selected" value="">---Select----</option>')
        if (CourseSeleted == 'msc') {
            for (i = 0; i < 2; i++) {
                $('<option />', { value: (i + 1), text: 'Semester ' + (i + 1) }).appendTo(selectedCatrol);
            }
        } else {
            for (i = 0; i < 6; i++) {
                $('<option />', { value: (i + 1), text: 'Semester ' + (i + 1) }).appendTo(selectedCatrol);
            }
        }
    }
}


function csvJSON_0(csv) {
    var lines = csv.split("\n");
    var result = [];
    var headers = lines[0].split(",");
    for (var i = 1; i < lines.length; i++) {
        var obj = {};
        var currentline = lines[i].split(",");
        for (var j = 0; j < headers.length; j++) {
            obj[headers[j]] = currentline[j];
        }
        result.push(obj);
    }
    return result; //JavaScript object
    //return JSON.stringify(result); //JSON
}


var csvJSON_2 = function (csv) {

    var lines = csv.split("\n")
    var result = []
    var headers = lines[0].split(",")

    lines.map(function (line, indexLine) {
        if (indexLine < 1) return // Jump header line

        var obj = {}
        var currentline = line.split(",")

        headers.map(function (header, indexHeader) {
            obj[header] = currentline[indexHeader]
        })

        result.push(obj)
    })

    result.pop() // remove the last item because undefined values

    return result // JavaScript object
}

function CSVToArray(strData, strDelimiter) {
    // Check to see if the delimiter is defined. If not,
    // then default to comma.
    strData = strData.replace(/^\s\s*/, '').replace(/\s\s*$/, '');;
    strDelimiter = (strDelimiter || ",");
    // Create a regular expression to parse the CSV values.
    var objPattern = new RegExp((
        // Delimiters.
        "(\\" + strDelimiter + "|\\r?\\n|\\r|^)" +
        // Quoted fields.
        "(?:\"([^\"]*(?:\"\"[^\"]*)*)\"|" +
        // Standard fields.
        "([^\"\\" + strDelimiter + "\\r\\n]*))"), "gi");
    // Create an array to hold our data. Give the array
    // a default empty first row.
    var arrData = [[]];
    // Create an array to hold our individual pattern
    // matching groups.
    var arrMatches = null;
    // Keep looping over the regular expression matches
    // until we can no longer find a match.
    while (arrMatches = objPattern.exec(strData)) {
        // Get the delimiter that was found.
        var strMatchedDelimiter = arrMatches[1];
        // Check to see if the given delimiter has a length
        // (is not the start of string) and if it matches
        // field delimiter. If id does not, then we know
        // that this delimiter is a row delimiter.
        if (strMatchedDelimiter.length && (strMatchedDelimiter != strDelimiter)) {
            // Since we have reached a new row of data,
            // add an empty row to our data array.
            arrData.push([]);
        }
        // Now that we have our delimiter out of the way,
        // let's check to see which kind of value we
        // captured (quoted or unquoted).
        if (arrMatches[2]) {
            // We found a quoted value. When we capture
            // this value, unescape any double quotes.
            var strMatchedValue = arrMatches[2].replace(
                new RegExp("\"\"", "g"), "\"");
        } else {
            // We found a non-quoted value.
            var strMatchedValue = arrMatches[3];
        }
        // Now that we have our value string, let's add
        // it to the data array.
        arrData[arrData.length - 1].push(strMatchedValue);
    }
    // Return the parsed data.
    return (arrData);
}

function csvJSON(csv) {
    var array = CSVToArray(csv);
    var objArray = [];
    for (var i = 1; i < array.length; i++) {
        for (var k = 0; k < array[0].length && k < array[i].length; k++) {
            var key = array[0][k];
            objArray[i - 1] = objArray[i - 1] || {};
            objArray[i - 1][key] = array[i][k]
        }
    }
    var json = JSON.stringify(objArray);
    var str = json.replace(/},/g, "},\r\n");
    return JSON.parse(str);
}


function csvJSON_1(csv) {
    var array = CSVToArray(csv);
    var objArray = [];
    for (var i = 1; i < array.length; i++) {
        objArray[i - 1] = {};
        for (var k = 0; k < array[0].length && k < array[i].length; k++) {
            var key = array[0][k];
            objArray[i - 1][key] = array[i][k]
        }
    }

    var json = JSON.stringify(objArray);
    var str = json.replace(/},/g, "},\r\n");

    return str;
}


var CurrentDate = new Date();
var CurrentYear = localStorage.getItem('CurrentYear');
var CurrentMonth = localStorage.getItem('CurrentMonth');

function getKeyByValue(object, value) {
    return Object.keys(object).find(key => object[key] === value);
}

function GetCsvToJsonData(url) {
    return $.ajax({
        type: "GET",
        url: _CommonUr + url,
    })
}

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        table = "sampleTable"
        name = "Report"
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})()

function SortArrayWithSeatNumber(ArrayObjectSort, keystr) {
    for (var ao = 0; ao < ArrayObjectSort.length; ao++) {
        try {
            ArrayObjectSort[ao]['IntSeatNumber'] = ArrayObjectSort[ao][keystr].substring(4, ArrayObjectSort[ao][keystr].length)
        } catch (e) {
            console.log(e);
        }
    }
    ArrayObjectSort.sort(dynamicSort("IntSeatNumber"));
    return ArrayObjectSort;
}

function dynamicSort(property) {
    var sortOrder = 1;
    if (property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a, b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    }
}



