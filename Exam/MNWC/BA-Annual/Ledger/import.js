var GrandCredit = 0;
var GrandInternal = 0;
var GrandExternal = 0;
var GrandTotal = 0;
var GrandGrade = "";
var GrandGP = 0;
var GrandGrace = 0;
var GrandResult = "PASS";
var ATKTCount = 0;
var CurrentStudentRecord = 0;

function uploadFile() {
    var files = $("#csvFile")[0].files;
    var file = files[0];
    csvToJson(file, 'marks.json');
}

function csvToJson(file, fileName) {
    var reader = new FileReader();
    reader.onload = function (progressEvent) {
        var data = "";
        var lines = this.result.split('\n');
        for (var line = 0; line < lines.length; line++) {
            data += lines[line];
        }
        var json = JSON.parse(CSV2JSON(data));

        $("#csvFile").hide();
        //$("#marksheet").show();
        //$("#Marks").find("tr").remove();

        //var header = "<tr><td>Code</td><td>Subject</td><td>Credits</td><td>Internal Marks <br/>(25)</td><td>External Marks <br/>(75)</td><td>Total Marks <br/>(100)</td><td>Grade</td></tr>";
        //$("#Marks").append(header);
		
		
        /*if (CurrentStudentRecord == 0) {
            $("#Previous").hide();
        }
        else {
            $("#Previous").show();
        }*/

        /*if (CurrentStudentRecord == json.length - 1)
            $("#Next").hide();
        else
            $("#Next").show();
        // for each row - update data on button click*/

        for (i = 0; i < json.length; i++) {
            /*if (i < CurrentStudentRecord)
                continue;
            else if (i > CurrentStudentRecord)
                break;*/
			var commonTable = $("<div id='marksheet'><div id='HeaderDetails'><div><img src='logo.gif' class='Logo' alt='Logo' width='80' height='80' /></div><div class='HeaderDetails'>SNDT WOMEN'S UNIVERSITY, MUMBAI</div><div class='CollegeName'>NAME OF THE COLLEGE: MANIBEN NANAVATI WOMEN'S COLLEGE, VILE PARLE(WEST) MUMBAI - 400 056.</div><div class='Results'>COLLEGE RESULT SHEET FOR BACHELOR OF <span id='CourseName'>ARTS (B.A.) SEMESTER - II (REGULAR)</span> - EXAMINATION HELD IN <span id='ExamYear'>MARCH – 2017</span>.</div></div><div id='StudentOne'></div><table id='FooterDetails'><tr><td>PLACE: <span id='Place'></span></td><td style='padding-left: 80px;'>PRINCIPAL</td><td>CONTROLLER OF EXAMINATIONS</td></tr><tr><td>DATE: <span id='MarkSheetDate'></span></td><td>MANIBEN NANAVATI WOMEN'S COLLEGE</td><td>SNDT WOMEN'S UNIVERSITY</td></tr><tr><td colspan='3'>Note: PP=Pass, Ex=Exempted, RR=Result Reserved, AB=Absent, FF=Failed, *Indicates current appearance, +Indicates Grace marks given</td></tr></table></div>");
		
            //document.title = json[i].SeatNumber + "-" + json[i].LastName.toUpperCase() + " " + json[i].FirstName.toUpperCase();
            $(commonTable).find("#CourseName").html(CourseExamName);
            $(commonTable).find("#ExamYear").html(ExamYear);
            $(commonTable).find("#StudentName").html(json[i].LastName.toUpperCase() + " " + json[i].FirstName.toUpperCase() + " " + json[i].FatherName.toUpperCase() + " " + json[i].MotherName.toUpperCase());
            $(commonTable).find("#Medium").html(Medium);
          
            $(commonTable).find("#InstituteName").html(Institution);
            $(commonTable).find("#CenterName").html(Center);
            
            $(commonTable).find("#PRN").html(json[i].PRN);
            $(commonTable).find("#SeatNumber").html(json[i].SeatNumber);

            for (j = 0; j < PaperPerMarksheet; j++) {
                var index = j + 1;
                var key1 = "Paper" + index + "Appeared";
                var key2 = "Code" + index;
                var key3 = "InternalMarks" + index;
                var key4 = "InternalMarks" + index + "Ex";
                var key5 = "ExternalMarks" + index;
                var key6 = "ExternalMarks" + index + "Ex";
                var key7 = "GraceMarks" + index;
                var key8 = "TotalMarks" + index;
                var key9 = "Grade" + index;
                var marks = [];
                var markList = {};

                markList[key1] = json[i]["Paper" + index + "Appeared"];
                markList[key2] = json[i]["Code" + index];
                markList[key3] = json[i]["InternalMarks" + index];
                markList[key4] = json[i]["InternalMarks" + index + "Ex"];
                markList[key5] = json[i]["ExternalMarks" + index];
                markList[key6] = json[i]["ExternalMarks" + index + "Ex"];
                markList[key7] = json[i]["GraceMarks" + index];
                markList[key8] = json[i]["TotalMarks" + index];
                markList[key9] = json[i]["Grade" + index];

                marks.push(markList);

                var row = PrepareMarksTable(marks[0], json[i]["Code" + index], index);
                $(commonTable).find("#Marks").append(row);
            }

            GrandGP = GrandGP / GrandCredit;

            if (GrandGrade != "F")
                GrandGrade = GetGPA35(GrandGP);

            var tr = PrepareTotalRecord(GrandCredit, GrandInternal, GrandExternal, GrandTotal, GrandGrade);
            $(commonTable).find("#Marks").append(tr);

            // Populate footer
            $("#TotalCredits").html("<b>" + json[i].TotalCredits + "</b>");
            $("#GrandTotal").html("<b>" + GrandTotal + " / " + TotalMarks + "</b>");

            GrandResult = (ATKTCount == 0) ? GrandResult : ATKTCount < 6 ? "ATKT" : "FAIL";

            $(commonTable).find("#Result").html("<b>" + GrandResult + "</b>");

            if (GrandGrade != "F") {
                $(commonTable).find("#GPA").html("<b>" + GrandGP.toFixed(2) + "</b>");
                $(commonTable).find("#Grade").html("<b>" + GrandGrade + "</b>");
               
                var percent = ((GrandTotal / TotalMarks) * 100).toFixed(2);
                $(commonTable).find("#Percentage").html("<b>" + percent + "&#37;" + "</b>");
            }
			else {
                $(commonTable).find("#GPA").html("<b>-</b>");
                $(commonTable).find("#Grade").html("<b>" + GrandGrade + "</b>");
                var percent = ((GrandTotal / TotalMarks) * 100).toFixed(2);
                $(commonTable).find("#Percentage").html("<b>-</b>");
            }

            $(commonTable).find("#Place").html(Place);
            $(commonTable).find("#Date").html(MarkSheetDate);

            GrandCredit = 0;
            GrandInternal = 0;
            GrandExternal = 0;
            GrandTotal = 0;
            GrandGrade = "";
            GrandGP = 0;
            ATKTCount = 0;
            GrandResult = "PASS";
			
			$("#MarksheetContainer").append(commonTable);
        }


    };
    reader.readAsText(file);
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

function CSV2JSON(csv) {
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

    return str;
}

function browserSupportFileUpload() {
    var isCompatible = false;
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        isCompatible = true;
    }
    return isCompatible;
}
