function PrepareMarksTable(marks,paperCode,index){
	var key = "marks.Code" + index;
	var current = "Paper"+ index + "Appeared";
	var internalMarks = "InternalMarks" + index;
	var externalMarks = "ExternalMarks" + index;
	var graceMarks = "GraceMarks" + index;
	var total = "";
	
    //var paperCode = marks["Paper"+index+"Appeared"] + Object.keys(PaperCodes)[index-1];
	var currentPaper = marks[current];
	console.log(paperCode);
	var paperTitle = PaperCodes[paperCode].PaperTitle;
	var credits = PaperCodes[paperCode].Credits;
	var internal = marks[internalMarks] + marks[internalMarks + "Ex"];
	
	var external = parseInt(marks[externalMarks]) +parseInt($.isNumeric(marks[graceMarks]) ?  marks[graceMarks] : 0) + marks[externalMarks + "Ex"];
	var grace = marks[graceMarks];
	
	if(marks[externalMarks] == "ABS")
		external = "ABS";
	else 
	{
		if(marks[externalMarks]=="CC")
			external = "COPY CASE";
		else
			external = external == "NaN" ? "NP" : external;
	}
	var tempInternal = $.isNumeric(marks[internalMarks]) ?  marks[internalMarks] : 0;
	
	var tempExternal = $.isNumeric(marks[externalMarks]) ?  marks[externalMarks] : 0;
	var tempGrace = $.isNumeric(marks[graceMarks]) ?  marks[graceMarks] : 0;
	
	var total = parseInt(tempInternal) + parseInt(tempExternal) + parseInt(tempGrace);
	
	GrandCredit = parseInt(GrandCredit)+ parseInt(credits);
	GrandInternal = parseInt(GrandInternal)+parseInt(tempInternal);
	GrandExternal = parseInt(GrandExternal) +parseInt(tempExternal) +parseInt(tempGrace) ; // changed by Hrishi
	GrandTotal = parseInt(GrandTotal) + parseInt(total);
	var gp = is35Passing ? GetGradePoint35(parseInt(total)) : GetGradePoint40(parseInt(total));
	//console.log("Before:"+GrandGP);
	GrandGP = parseFloat(GrandGP) + (parseFloat(gp) * parseInt(credits)); 
	//console.log("After:"+gp);
	var grade = is35Passing ? CalculateGrade35(total) : CalculateGrade40(total);
	if(grade == "F"){
		GrandGrade = "F";
		ATKTCount = ATKTCount + 1;
	}
	//total = parseInt(total);// + (parseInt(tempGrace) > 0) ? "+" : "";
	if(parseInt(tempGrace))
		total = total + "+";
	
	var row = "<tr><td><span style='font-size: 16px;'>" + currentPaper + "</span>" + paperCode + "</td>" +
				"<td style='text-align: left;'>"+paperTitle+"</td>"+
				"<td>"+credits+"</td>"+
				"<td>"+internal+"</td>"+
				"<td>"+external+"</td>"+
				"<td>"+total+"</td>"+
				"<td>"+grade+"</td></tr>";
	
	return row;
}

function PrepareTotalRecord(CreditTotal,InternalTotal, ExternalTotal,GrandTotal,GrandGrade){
		var row = "<tr style='height:50px;'>"+
			"<td colspan='2' style='height:50px;font-size: 14pt;'><b>Total</b></td>" + 
			"<td style='height:50px;font-size: 12pt;'><b>"+CreditTotal+"</b></td>" + 
			"<td style='height:50px;font-size: 12pt;'><b>"+InternalTotal+"</b></td>" + 
			"<td style='height:50px;font-size: 12pt;'><b>"+ExternalTotal+"</b></td>" + 
			"<td style='height:50px;font-size: 12pt;'><b>"+GrandTotal+"</b></td>" + 
			//"<td style='font-size: 12pt;'><b>-</b></td></tr>";
			"<td style='height:50px;font-size: 12pt;'><b>"+GrandGrade+"</b></td></tr>";
			return row;
}

function CalculateGrade35(marksObtained){
	if(marksObtained >= 90 && marksObtained <= 100)
		return "O+";
	if(marksObtained >= 80 && marksObtained < 90)
		return "O";
	if(marksObtained >= 70 && marksObtained < 80)
		return "A+";
	if(marksObtained >= 60 && marksObtained < 70)
		return "A";
	if(marksObtained >= 55 && marksObtained < 60)
		return "B";
	if(marksObtained >= 50 && marksObtained < 55)
		return "B";
	if(marksObtained >= 40 && marksObtained < 50)
		return "C";
	if(marksObtained >= 35 && marksObtained < 40)
		return "P";
	if(marksObtained >= 0 && marksObtained < 35)
		return "F";
}

function CalculateGrade40(marksObtained){
	if(marksObtained >= 90 && marksObtained <= 100)
		return "O+";
	if(marksObtained >= 80 && marksObtained < 90)
		return "O";
	if(marksObtained >= 70 && marksObtained < 80)
		return "A+";
	if(marksObtained >= 60 && marksObtained < 70)
		return "A";
	if(marksObtained >= 55 && marksObtained < 60)
		return "B+";
	if(marksObtained >= 50 && marksObtained < 55)
		return "B";
	if(marksObtained >= 45 && marksObtained < 50)
		return "C";
	if(marksObtained >= 40 && marksObtained < 45)
		return "P";
	if(marksObtained >= 0 && marksObtained < 40)
		return "F";
}

function GetGradePoint35(mark){
		var marks = parseInt(mark);
		if(marks >= 90)
			return "10";
		if(marks >= 80 && marks < 90)
			return "9." + (marks % 10) + "0";
		if(marks >= 70 && marks < 80)
			return "8." + (marks % 10) + "0";
		if(marks >= 60 && marks < 70)
			return "7." + (marks % 10) + "0";
		if(marks >= 55 && marks < 60)
			return "6." + (marks % 5) * 2 + "0";
		if(marks >= 50 && marks < 55)
			return "5." + ((marks % 10) + 5) + "0";
		if(marks >= 40 && marks < 50){
			if(marks == 40)
				return "5.0";
			if(marks == 41)
				return "5.05";
			if(marks == 42)
				return "5.10";
			if(marks == 43)
				return "5.15";
			if(marks == 44)
				return "5.20";
			if(marks == 45)
				return "5.25";
			if(marks == 46)
				return "5.30";
			if(marks == 47)
				return "5.35";
			if(marks == 48)
				return "5.40";
			if(marks == 49)
				return "5.45";
			
		
		}
		if(marks >= 35 && marks < 40)
			return "4." + ((marks % 5) * 2)+ "0";
		if(marks >= 0 && marks < 35)
			return "0";
}

function GetGradePoint40(mark){
		var marks = parseInt(mark);
		if(marks >= 90)
			return "10";
		if(marks >= 80 && marks < 90)
			return "9." + (marks % 10) + "0";
		if(marks >= 70 && marks < 80)
			return "8." + (marks % 10) + "0";
		if(marks >= 60 && marks < 70)
			return "7." + (marks % 10) + "0";
		if(marks >= 55 && marks < 60)
			return "6." + (marks % 5) * 2 + "0";
		if(marks >= 50 && marks < 55)
			return "5." + ((marks % 10) + 5) + "0";
		if(marks >= 45 && marks < 50)
			return "5." + (marks % 5) * 2 +"0";
		if(marks >= 40 && marks < 45)
			return "4." + (marks % 5) * 2 +"0";
		if(marks >= 0 && marks < 40)
			return "0";
}

function GetGPA35(gp){
	if(gp>=10)
		return "O+";
	if(gp>=9 && gp<=9.99)
		return "O";
	if(gp>=8 && gp<=8.99)
		return "A+";
	if(gp>=7 && gp<=7.99)
		return "A";
	if(gp>=6 && gp<=6.99)
		return "B+";
	if(gp>=5.5 && gp<=5.99)
		return "B";
	if(gp>=5 && gp<=5.49)
		return "C";
	if(gp>=4 && gp<=4.99)
		return "P";
	if(gp>=0 && gp<=3.99)
		return "F";
	
}

function GetGPA40(gp){
	
	if(gp>=10)
		return "O+";
	if(gp>=9 && gp<=9.99)
		return "O";
	if(gp>=8 && gp<=8.99)
		return "A+";
	if(gp>=7 && gp<=7.99)
		return "A";
	if(gp>=6 && gp<=6.99)
		return "B+";
	if(gp>=5.5 && gp<=5.99)
		return "B";
	if(gp>=5 && gp<=5.49)
		return "C";
	if(gp>=4 && gp<=4.99)
		return "P";
	if(gp>=0 && gp<=3.99)
		return "F";
}
