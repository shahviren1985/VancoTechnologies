var GrandCredit = 0;
var GrandInternal = 0;
var GrandExternal = 0;
var GrandTotal = 0;
var GrandGrade = "";
var GrandGP = 0;
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
        $("#marksheet").show();
        $("#Marks").find("tr").remove();
		

        /*if (CurrentStudentRecord == 0) {
            $("#Previous").hide();
        }
        else {
            $("#Previous").show();
        }*/

        /*if (CurrentStudentRecord == json.length - 1)
            $("#Next").hide();
        else
            $("#Next").show();*/
        // for each row - update data on button click

        for (i = 0; i < json.length; i++) {
			
            /*if (i < CurrentStudentRecord)
                continue;
            else if (i > CurrentStudentRecord)
                break;*/
            //document.title = json[i].SeatNumber + "-" + json[i].LastName.toUpperCase() + " " + json[i].FirstName.toUpperCase();
			
            var commonTable = $("<div id='marksheet'><div id='HeaderDetails'><div id='CollegeDetails'></div><div class='HeaderDetails'>Statement of Marks</div><div class='CourseDetails' id='CourseName'></div><div class='ExamDetails'>First Year Examination held in <span id='ExamYear'></span></div></div><div id='StudentDetails'><span style='font-size:12pt; font-weight:bold;'>NAME OF THE STUDENT: &nbsp;</span><span id='StudentName'></span><div class='Medium'>MEDIUM: <span id='Medium'></span></div></div><div id='ExamCenterDetails'><div style='width: 20%;'>SEAT NO: <span id='SeatNumber'></span></div><div style='width: 20%;'>INSTITUTION: <span id='InstituteName'></span></div><div style='width: 32%;'>CENTRE: <span id='CenterName'></span></div></div><table border='1' id='Marks'><thead><td><strong>Code</strong></td><td><b>Subject</b></td><td><b>Internal Marks<br/>(Minimum 12 out of 30)</b></td><td><b>External Marks<br/>(Minimum 28 out of 30)</b></td><td><b>Total Marks<br/>(Minimum 40 out of 100)</b></td></thead></table><br /><div id='SummaryDetails'><table border='1'><thead><td>Total Internal Marks</td><td>Total External Marks</td><td>Grand Total</td><td>Result</td></thead><tr><td id='TotalCredits'></td><td id='GPA'>-</td><td id='Grade'>-</td><td id='GrandTotal'>-</td></tr></table></div><div class='Place'>Place&nbsp;: &nbsp;<span id='Place'></span></div><div class='Date'>Date &nbsp;&nbsp;:  &nbsp;<span id='Date'></span></div><div><div class='Principal'>Principal <br/> Maniben Nanavati Women's College<br/>Vile Parle (W), Mumbai</div></div><div><div class='CollegeName'></div><div class='University'></div><div class='Controller'>Director <br/> Board Of Examinations and Evaluation <br/> S.N.D.T Women's University</div></div>	<div id='FooterDetails'>Note: PP=Pass, Ex=Exempted, RR=Result Reserved, ABS=Absent, F=Failed, *Indicates current appearance, +Indicates Grace marks given, **Indicates Incentive marks for National Service Scheme</div></div><footer />");
            //var commonTable = $("<div id='marksheet'><div id='HeaderDetails'><div class='HeaderDetails'>Statement of Marks</div><div class='CourseDetails' id='CourseName'></div><div class='ExamDetails'>Examination held in <span id='ExamYear'></span></div></div><div id='StudentDetails'><span style='font-size:10pt; font-weight:bold;'>NAME OF THE STUDENT: &nbsp;</span><span id='StudentName'></span><div class='Medium'>MEDIUM: <span id='Medium'></span></div></div><div id='ExamCenterDetails'><div style='width: 20%;'>SEAT NO: <span id='SeatNumber'></span></div><div style='width: 20%;'>INSTITUTION: <span id='InstituteName'></span></div><div style='width: 32%;'>CENTRE: <span id='CenterName'></span></div><div id='PRNContainer' style='width: 25%; float:right; text-align:right;' >PRN: &nbsp; <span id='PRN'></span></div></div><table border='1' id='Marks'><thead><td><strong>Code</strong></td><td><b>Subject</b></td><td><b>Credits</b></td><td><b>Internal Marks <br/>(25)</b></td><td><b>External Marks <br/>(75)</b></td><td><b>Total Marks <br/>(100)</b></td><td><b>Grade</b></td></thead></table><br /><div id='SummaryDetails'><table border='1'><thead><td>Total Credits</td><td>G.P.A.</td><td>Grade</td><td>Grand Total</td><td>Result</td><td>Percentage</td></thead><tr><td id='TotalCredits'></td><td id='GPA'>-</td><td id='Grade'>-</td><td id='GrandTotal'>-</td><td id='Result'>-</td><td id='Percentage'>-</td></tr></table></div><div class='Place'>Place&nbsp;: &nbsp;<span id='Place'></span></div><div class='Date'>Date &nbsp;&nbsp;:  &nbsp;<span id='Date'></span></div><div><div class='Principal'>Principal <br/> Maniben Nanavati Women's College </div><div class='Controller'>Director <br/> Board Of Examinations and Evaluation <br/> S.N.D.T Women's University</div></div><div><div class='CollegeName'></div><div class='University'></div></div>	<div id='FooterDetails'>Note: PP=Pass, Ex=Exempted, RR=Result Reserved, AB=Absent, FF=Failed, *Indicates current appearance, +Indicates Grace marks given</div></div><footer />");

			//var header = "<tr class='marksheads'><td style='width:10%'><b>Code</b></td><td style='width:49%'><b>Subject</b></td><td style='width:6%'><b>Credits</b></td><td style='width:10%'><b>Internal Marks<br/>(25)</b></td><td style='width:10%'><b>External Marks <br/>(75)</b></td><td style='width:10%'><b>Total Marks <br/>(100)</b></td><td style='width:5%'><b>Grade</b></td></tr>";
			//$(commonTable).find("#Marks").append(header);
		
			$(commonTable).find("#CourseName").html(CourseExamName);
            $(commonTable).find("#ExamYear").html(ExamYear);
            $(commonTable).find("#StudentName").html(json[i].LastName.toUpperCase() + " " + json[i].FirstName.toUpperCase() + " " + json[i].FatherName.toUpperCase() + " " + json[i].MotherName.toUpperCase());
            $(commonTable).find("#Medium").html(json[i].Medium.toUpperCase());
			//$(commonTable).find("#Medium").html(Medium);
            
            $(commonTable).find("#InstituteName").html(Institution);
            $(commonTable).find("#CenterName").html(Center);
            var prn = json[i].PRN == "" ? "-" :json[i].PRN.replace("'","").replace(",","");
            $(commonTable).find("#PRN").html(prn);
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
            var tPercent = ((GrandTotal / TotalMarks) * 100).toFixed(2);
            if (GrandGrade != "F"){
                //GrandGrade = is35Passing ? GetGPA35(GrandGP) : GetGPA40(GrandGP);
                GrandGrade = is35Passing ? CalculateGrade35(tPercent) : CalculateGrade40(tPercent);
            }
            var tr = PrepareTotalRecord(GrandCredit, GrandInternal, GrandExternal, GrandTotal, GrandGrade);
            $(commonTable).find("#Marks").append(tr);

            // Populate footer
			
			//$(commonTable).find("#NSS").html("<b>5</b>");
            $(commonTable).find("#TotalCredits").html("<b>" + GrandInternal + "</b>");
            $(commonTable).find("#GrandTotal").html("<b>Second Class</b>");

            GrandResult = (ATKTCount == 0) ? GrandResult : ATKTCount < 6 ? "ATKT" : "FAIL";

            $(commonTable).find("#Result").html("<b>" + GrandResult + "</b>");
            //console.log(GrandGrade);
            if (GrandGrade != "F") {
                $(commonTable).find("#GPA").html("<b>" + GrandExternal + "</b>");
                //$(commonTable).find("#GPA").html("<b>3.6</b>");
                //$(commonTable).find("#Grade").html("<b>-</b>");
               $(commonTable).find("#Grade").html("<b>**" + (parseInt(GrandTotal) + (5)) + " / " + TotalMarks + "</b>");
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
			PrepareCollegeDetails();
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
