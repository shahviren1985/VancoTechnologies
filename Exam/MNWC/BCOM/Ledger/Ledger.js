var GrandCredit = 0;
var GrandInternal = 0;
var GrandExternal = 0;
var GrandTotal = 0;
var GrandGrace = 0;
var GrandGrade = "";
var GrandGP = 0;
var GrandResult = "PASS";
var ATKTCount = 0;
var CurrentStudentRecord = 0;
var GrandPaperAppear = "";
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
        
        for (i = CurrentStudentRecord; i < json.length; i++) {
				var header = $("<div id='HeaderDetails"+i+"' class='HeaderDetails'><div><img src='logo.gif' class='Logo' alt='Logo' width='80' height='80' /></div><div class='HeaderDetailsName'>SNDT WOMEN'S UNIVERSITY, MUMBAI</div><div class='CollegeName'>NAME OF THE COLLEGE: MANIBEN NANAVATI WOMEN'S COLLEGE, VILE PARLE (WEST) MUMBAI - 400 056.</div><div class='Results'>COLLEGE RESULT SHEET FOR <span id='CourseName'>" + CourseExamName + "</span>, Semester - I, EXAMINATION HELD IN <span id='ExamYear'>"+ExamYear+"</span>.</div></div>");
				
				var footer = $("<table id='FooterDetails'+i+'' class='FooterDetails'><tr><td align='left' style='text-align: left;' class='footerplace'>PLACE </td><td align='left' style='text-align: left;'>: &nbsp;<span id='Place'> Hrishi</span></td><td align='center' rowspan='2'>PRINCIPAL <br/> MANIBEN NANAVATI WOMEN'S COLLEGE <BR /> Vile Parle (W), Mumbai</td><td align='center' rowspan='2'>DIRECTOR <BR />BOARD OF EXAMINATIONS AND EVALUATION<BR/>S.N.D.T. WOMEN'S UNIVERSITY</td></tr><tr><td align='left' style='text-align: left;'>DATE </td><td align='left' style='text-align: left;'>: &nbsp;<span id='MarkSheetDate'>Date</span></td></tr><tr><td colspan='4' align='left'  style='text-align: left; font-size:10px;'>Note: PP=Pass, Ex=Exempted, RR=Result Reserved, AB=Absent, FF=Failed, *Indicates current appearance, +Indicates Grace marks given</td></tr></table><footer />");
				
				var commonTable = $("<div id='marksheet'></div>");
		
                var studentName = json[i].LastName.toUpperCase() + " " + json[i].FirstName.toUpperCase() + " " + json[i].FatherName.toUpperCase() + " " + json[i].MotherName.toUpperCase();
                var seatNumber = json[i].SeatNumber;
                var PRN = json[i].PRN.replace("'","").replace(",","");

                //document.title = json[i].SeatNumber + "-" + json[i].LastName.toUpperCase() + " " + json[i].FirstName.toUpperCase();
                var studentRecordHeader = "<div id='StudentRecord'><div class='sname'><span>NAME: </span><div id='StudentName'>" + studentName + "</div></div><div class='pdetails'><span>PRN:</span><div id='PerRegNumber'>" + PRN + "</div></div><div class='mdetails'><span>MEDIUM:</span><div id='Medium'>" + Medium + "</div></div><div class='cdetails'><span>CENTRE CODE:</span><div id='CenterName'  style='width: 150px'>" + Center + "</div></div><div class='idetails'><span>INSTITUTION:</span><div id='Institution'>" + Institution + "</div></div><div class='sdetails'><span>SEAT NO:</span><div id='SeatNumber'>" + seatNumber + "</div></div></div>";
                //var studentRecordHeader = "<div id='StudentRecord'><div class='sname'><span>NAME:</span><div id='StudentName'>" + studentName + "</div></div><div class='sdetails'><span>SEAT NO:</span><div id='SeatNumber'>" + seatNumber + "</div></div><div class='idetails'><span>INSTITUTION:</span><div id='Institution'>" + Institution + "</div></div><div class='cdetails'><span>CENTRE:</span><div id='CenterName'  style='width: 150px'>" + Center + "</div></div><div class='mdetails'><span>MEDIUM:</span><div id='Medium'>" + Medium + "</div></div><div class='pdetails'><span>PER.REG.NO.:</span><div id='PerRegNumber'>" + PRN + "</div></div></div>";
                //var studentRecordHeader = "<div id='StudentRecord'><div class='sname'><span>NAME:</span><div id='StudentName'>" + studentName + "</div></div><divclass='sdetails'><span>SEAT NO:</span><div id='SeatNumber'>" + seatNumber + "</div></div><div><span>INSTITUTION:</span><div id='Institution'>" + Institution + "</div></div><div><span>CENTRE:</span><div id='CenterName'  style='width: 150px'>" + Center + "</div></div><div><span>MEDIUM:</span><div id='Medium'>" + Medium + "</div></div><div style='float:right;'><span>PER.REG.NO.:</span><div id='PerRegNumber'>" + PRN + "</div></div></div>";
                var studentMarksHeader = "<div class='markswrapper'><div class='studentmarkstable'><table class='markstable'><thead><tr><td width='10%;'>Subject Code</td><td width='50%;'>Subject</td><td width='5%;'>Credits</td><td width='10%;'>Internal Marks (25)</td><td width='10%;'>External Marks (75)</td><td width='5%;'>Grace</td><td width='10%;'>Total Marks (100)</td><td width='5%;'>Grade</td><td width='5%;'>GP</td><td width='20%;'>GP X CR</td></tr></thead><tbody>";

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
                    studentMarksHeader = studentMarksHeader + row;
                }
                GrandGP = GrandGP / GrandCredit;
                
				/*if (GrandGrade != "F")
					GrandGrade = is35Passing ? GetGPA35(GrandGP) : GetGPA40(GrandGP);
                */
                var tPercent = ((GrandTotal / TotalMarks) * 100).toFixed(2);
                if (GrandGrade != "F"){
                    //GrandGrade = is35Passing ? GetGPA35(GrandGP) : GetGPA40(GrandGP);
                    GrandGrade = is35Passing ? CalculateGrade35(tPercent) : CalculateGrade40(tPercent);
                }        
                
				var GrandGPCR = GrandGP.toFixed(2) * GrandCredit;

                GrandGPCR = GrandGPCR.toFixed(2);
				GrandGrace = GrandGrace == 0 ? "-" : GrandGrace;
                GrandResult = (ATKTCount == 0) ? GrandResult : ATKTCount < 6 ? "ATKT" : "FAIL";
                var percent = ((GrandTotal / TotalMarks) * 100).toFixed(2);
                studentMarksHeader = studentMarksHeader + "<tr class='TotalRecord'><td colspan='2'>Total</td><td>" + json[i].TotalCredits + "</td><td>" + GrandInternal + "</td><td>" + GrandExternal + "</td><td>" + GrandGrace + "</td><td>" + GrandTotal + "</td><td>" + GrandGrade + "</td><td>" + GrandGP.toFixed(2) + "</td><td>" + GrandGPCR + "</td></tr></tbody></table></div>"

				if(GrandPaperAppear.length <2)
					GrandPaperAppear = "-";
				
                //studentMarksHeader = studentMarksHeader + "<div class='summarydiv'><table id='ResultSummary'><tr><td>TOTAL CREDITS:</td><td>" + GrandCredit + "</td></tr><tr><td>GPA:</td><td>" + GrandGP.toFixed(2) + "</td></tr><tr><td>GRADE:</td><td>" + GrandGrade + "</td></tr><tr><td>TOTAL MARKS:</td><td>" + GrandTotal + "</td></tr><tr><td>PERCENTAGE:</td><td>" + percent + "%</td></tr><tr><td>RESULT:</td><td>" + GrandResult + "</td></tr><tr><td>To Appear In (Sub code):</td><td>&nbsp;"+GrandPaperAppear+"</td></tr></table></div></div>";
                studentMarksHeader = studentMarksHeader + "<div class='summarydiv'><table id='ResultSummary'><tr><td width='50%'>TOTAL CREDITS</td><td>: " + GrandCredit + "</td></tr><tr><td width='50%'>GPA</td><td>: " + GrandGP.toFixed(2) + "</td></tr><tr><td width='50%'>GRADE</td><td>: " + GrandGrade + "</td></tr><tr><td width='50%'>TOTAL MARKS</td><td>: " + GrandTotal + "</td></tr><tr><td width='50%'>PERCENTAGE</td><td>: " + percent + "%</td></tr><tr><td width='50%'>RESULT</td><td>: " + GrandResult + "</td></tr><tr><td>To Appear In (Sub code) :</td></tr><tr><td colspan='2'>"+GrandPaperAppear+"</td></tr></table></div></div>";
                $(commonTable).append(studentRecordHeader);
                $(commonTable).append(studentMarksHeader);
                GrandCredit = 0;
                GrandInternal = 0;
                GrandExternal = 0;
                GrandTotal = 0;
                GrandGrade = "";
				GrandGrace = 0;
                GrandGP = 0;
                ATKTCount = 0;
                GrandResult = "PASS";
				GrandPaperAppear  = "";
                CurrentStudentRecord++;
            
			if(CurrentStudentRecord%3 == 1)
				$("#MarksheetContainer").append(header);

            $("#MarksheetContainer").append(commonTable);
			
			if(CurrentStudentRecord%3==0 || CurrentStudentRecord == LEDGER_LAST_RECORD){
				$(footer).find("#Place").html(Place);
				$(footer).find("#MarkSheetDate").html(MarkSheetDate);
				$("#MarksheetContainer").append(footer);
			}
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
