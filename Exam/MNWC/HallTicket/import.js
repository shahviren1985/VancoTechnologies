function uploadFile() {
    var files = $("#csvFile")[0].files;
    var file = files[0];
    csvToJson(file, 'students.json');
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
        
        for (i = 0; i < json.length; i++) {
			
            //var commonTable = $("<div id='marksheet'><div id='HeaderDetails'><div class='HeaderDetails'>Statement of Marks</div><div class='CourseDetails' id='CourseName'></div><div class='ExamDetails'>Examination held in <span id='ExamYear'></span></div></div><div id='StudentDetails'><span style='font-size:12pt; font-weight:bold;'>NAME OF THE STUDENT:</span><span id='StudentName'></span><div class='Medium'>MEDIUM: <span id='Medium'></span></div></div><div id='ExamCenterDetails'><div style='width: 20%;'>SEAT NO: <span id='SeatNumber'></span></div><div style='width: 20%;'>INSTITUTION: <span id='InstituteName'></span></div><div style='width: 32%;'>CENTRE: <span id='CenterName'></span></div><div id='PRNContainer' style='width: 25%; float:right; text-align:right;' >PRN: &nbsp; <span id='PRN'></span></div></div><div id='AdmissionForm'></div><br /><div id='SummaryDetails'><table border='1'><thead><td>Total Credits</td><td>G.P.A.</td><td>Grade</td><td>Grand Total</td><td>Result</td><td>Percentage</td></thead><tr><td id='TotalCredits'></td><td id='GPA'>-</td><td id='Grade'>-</td><td id='GrandTotal'>-</td><td id='Result'>-</td><td id='Percentage'>-</td></tr></table></div><div class='Place'>Place&nbsp;: &nbsp;<span id='Place'></span></div><footer />");
            var commonTable = $("<div id='marksheet'><div id='AdmissionForm' class='AdmissionForm'></div></div>");
            var header = $("<div class='header'><div class='logo'><img src='logo1.GIF' width='' height='' /></div><div class='header1'>MANIBEN NANAVATI WOMEN'S COLLEGE</div><div class='header2'>Affiliated to Shreemati Nathibai Damodar Thackersey Women's University</div></div>");
			//var className = $("");
			//var subCourse = $("<div id='SubCourse'><span>Sub-Course</span></div>");
			//var rollNo = $("");
			
			var studentName = $("<div id='StudentName'><span>Student Name:</span><span id='StudentNameValue'></span></div>");
			var seatNumber = $("<div id='SeatNumber'><span>Seat Number: </span><span id='SeatNumberValue'></span></div>");
			
			var courseName = $("<div id='CourseName'><span>Course Name:</span><span id='CourseNameValue'></span></div>");
			//var parents = $("<div id='Parents'><span>Parents</span><span id='ParentsValue'></span></div>");
			var examName = $("<div id='ExamName'><span>Exam Name:</span><span id='ExamNameValue'></span></div><div id='StudentPhoto'> Photograph &nbsp;</div>");
            
            var  marksDetails = $("<div id='MarksDetails'><table border='1' cellspacing='0' width='100%' style='border-collapse:collapse; border:solid black 1.0pt'><thead><th>Sr.</th>	<th>Code</th><th>Paper Title</th>	<th>Date</th>	<th>Sign</th></thead><tbody></tbody></table></div>");
            
            var index = 1;
            for(var j=1;j<=PaperPerMarksheet;j++){
                
                if(json[i]["Paper"+j+"Appeared"] == "*"){
                    var paperTitle = "";
					var isPermitted = "";
                    for(var p in papers1){
                        if(papers1[p].PaperCode == json[i]["Code"+j]){
                            paperTitle = papers1[p].PaperTitle;
							isPermitted = json[i]["ExternalMarks"+j];
                            break;
                        }
                    }
                    
                    $(marksDetails).find("tbody").append("<tr><td style='font-size: 13px'>"+index+"</td><td>"+json[i]["Code"+j]+"</td><td style='text-align: left; padding-left: 5px;font-size: 13px; min-width: 300px;'>"+paperTitle+"</td><td style='min-width: 150px;'>&nbsp;</td><td style='font-size: 13px; min-width: 150px;'>"+isPermitted+"</td></tr>");
                    index++;
                } 
            }


			var date = $("<div class='footerdetails'><div>College Seal And Authorized Signature<span>&nbsp;</span></div><div style='width: 20%;'>Student's Sign</div></div>");
			

            var footer = $("<footer />");
            
            $(studentName).find("#StudentNameValue").html(json[i].LastName.toUpperCase()+" "+json[i].FirstName.toUpperCase()+" "+json[i].FatherName.toUpperCase()+" "+json[i].MotherName.toUpperCase());
			
			$(seatNumber).find("#SeatNumberValue").html(json[i].SeatNumber);
			$(courseName).find("#CourseNameValue").html(json[i].Course + " - " + json[i].SubCourse);
			$(examName).find("#ExamNameValue").html(ExamYear);

			$(commonTable).find("#AdmissionForm").append(header);
			

			$(commonTable).find("#AdmissionForm").append(studentName);
			$(commonTable).find("#AdmissionForm").append(seatNumber);
            
			$(commonTable).find("#AdmissionForm").append(examName);
            $(commonTable).find("#AdmissionForm").append(courseName);
			$(commonTable).find("#AdmissionForm").append(marksDetails);
			
            $(commonTable).find("#AdmissionForm").append(date);
            
            

			$("#MarksheetContainer").append(commonTable);
if(i%2==1){
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
