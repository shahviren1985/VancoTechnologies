$(document).ready(function(){
	$("#successmsg").hide();
	$("#errormsg").hide();
	$("#txt_a7").hide();
	// hide validation error div
	$("#validationerrormsg").hide();
	var userCode = localStorage.getItem("userCode");
	var userName = localStorage.getItem("userName");
	var collegeCode = localStorage.getItem("collegeCode");
	var isFinalYearStudent = localStorage.getItem("isFinalYearStudent");
	
	if (isFinalYearStudent == 'true')
	{
		$("#exitForm").show();
	}
	$("#userName").html( "Welcome " + userName);
	
	if (userCode == null || userCode == undefined || !userCode)
	{
		localStorage.clear();
		window.location.href="index.html";
	}
	
	$("#logout").click(function(){
		localStorage.clear();
		window.location.href="index.html";
	});
	
	
	$("input[name=rd_a7]").click(function () {
		if($('input[name=rd_a7]:checked').val()=="yes")
		{
			$("#txt_a7").show();
		}
		else{
			$("#txt_a7").hide();
		}
	});
	$("#btnSubmit").click(function () {
		$("#successmsg").hide();
		$("#errormsg").hide();
		$("#validationerrormsg").hide();
		var exitform = {
			userId : userCode,
			a1: $('input[name=rd_a1]:checked').val(),
			a2:
				{
					mostValuable: $('#drp_a2_mostvaluable').val(),
					valuable:$('#drp_a2_valuable').val(),
					lessValuable: $('#drp_a2_lessvaluable').val()
				},
			a3:
				{
					postiveEffect:$('#txt_a3_possitive').val(),
					negativeEffect:$('#txt_a3_negative').val()
				},
			a4:$('input[name=rd_a4]:checked').val(),
			a5:$('input[name=rd_a5]:checked').val(),
			a6:[
				{
					deptName:$('#txt_a6_department1').val(),
					positiveComment:$('#txt_a6_positive1').val(),
					negativeComment:$('#txt_a6_negative1').val(),
				}, 
				{
					deptName:$('#txt_a6_department2').val(),
					positiveComment:$('#txt_a6_positive2').val(),
					negativeComment:$('#txt_a6_negative2').val()
				}
				],
			a7:{
					isParticipated:$('input[name=rd_a7]:checked').val(),
					eventName:$('#txt_a7').val()
				},
			a8:$('#txt_a8').val(),
			a9:[
				{
					deptName:"Canteen",
					a1:$('#drp_a9_canteen_a1').val(),  
					a2:$('#drp_a9_canteen_a2').val(),  
					a3:$('#drp_a9_canteen_a3').val(),  
					a4:$('#drp_a9_canteen_a4').val(),  
					a5:$('#drp_a9_canteen_a5').val()
				},
				{
					deptName:"Library",
					a1:$('#drp_a9_library_a1').val(),  
					a2:$('#drp_a9_library_a2').val(),
					a3:$('#drp_a9_library_a3').val(),
					a4:$('#drp_a9_library_a4').val(),
					a5:$('#drp_a9_library_a5').val()
				},
				{
					deptName:"Office",
					a1:$('#drp_a9_office_a1').val(),
					a2:$('#drp_a9_office_a2').val(),
					a3:$('#drp_a9_office_a3').val(),
					a4:$('#drp_a9_office_a4').val()
				},
				{
					deptName:"Gym/Fitness Center",
					a1:$('#drp_a9_gym_a1').val(),
					a2:$('#drp_a9_gym_a2').val(),
					a3:$('#drp_a9_gym_a3').val(),
					a4:$('#drp_a9_gym_a4').val()
				},
				{
					deptName:"Sport",
					a1:$('#drp_a9_sport_a1').val(),
					a2:$('#drp_a9_sport_a2').val(),
					a3:$('#drp_a9_sport_a3').val()
				},
				{
					deptName:"Health Services",
					a1:$('#drp_a9_health_a1').val(),
					a2:$('#drp_a9_health_a2').val(),
					a3:$('#drp_a9_health_a3').val()
				}
			]
		};
		
		
		
		var validationstatus = true ; 
		
		
		if (exitform.a9[5].a3 == undefined || exitform.a9[5].a3 == "" || exitform.a9[5].a3 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_health_a3").addClass("InvalidError");
			window.location.href = "#dv_a9_health_a3";
		}
		else 
		{
			$("#drp_a9_health_a3").removeClass("InvalidError");
		}
		
		
		if (exitform.a9[5].a2 == undefined || exitform.a9[5].a2 == "" || exitform.a9[5].a2 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_health_a2").addClass("InvalidError");
			window.location.href = "#dv_a9_health_a2";
		}
		else 
		{
			$("#drp_a9_health_a2").removeClass("InvalidError");
		}
		
		if (exitform.a9[5].a1 == undefined || exitform.a9[5].a1 == "" || exitform.a9[5].a1 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_health_a1").addClass("InvalidError");
			window.location.href = "#dv_a9_health_a1";
		}
		else 
		{
			$("#drp_a9_health_a1").removeClass("InvalidError");
		}
		
		if (exitform.a9[4].a3 == undefined || exitform.a9[4].a3 == "" || exitform.a9[4].a3 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_sport_a3").addClass("InvalidError");
			window.location.href = "#dv_a9_sport_a3";
		}
		else 
		{
			$("#drp_a9_sport_a3").removeClass("InvalidError");
		}
		
		
		if (exitform.a9[4].a2 == undefined || exitform.a9[4].a2 == "" || exitform.a9[4].a2 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_sport_a2").addClass("InvalidError");
			window.location.href = "#dv_a9_sport_a2";
		}
		else 
		{
			$("#drp_a9_sport_a2").removeClass("InvalidError");
		}
		
		if (exitform.a9[4].a1 == undefined || exitform.a9[4].a1 == "" || exitform.a9[4].a1 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_sport_a1").addClass("InvalidError");
			window.location.href = "#dv_a9_sport_a1";
		}
		else 
		{
			$("#drp_a9_sport_a1").removeClass("InvalidError");
		}
		
		
		
		if (exitform.a9[3].a4 == undefined || exitform.a9[3].a4 == "" || exitform.a9[3].a4 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_gym_a4").addClass("InvalidError");
			window.location.href = "#dv_a9_gym_a4";
		}
		else 
		{
			$("#drp_a9_gym_a4").removeClass("InvalidError");
		}
		
		if (exitform.a9[3].a3 == undefined || exitform.a9[3].a3 == "" || exitform.a9[3].a3 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_gym_a3").addClass("InvalidError");
			window.location.href = "#dv_a9_gym_a3";
		}
		else 
		{
			$("#drp_a9_gym_a3").removeClass("InvalidError");
		}
		
		
		if (exitform.a9[3].a2 == undefined || exitform.a9[3].a2 == "" || exitform.a9[3].a2 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_gym_a2").addClass("InvalidError");
			window.location.href = "#dv_a9_gym_a2";
		}
		else 
		{
			$("#drp_a9_gym_a2").removeClass("InvalidError");
		}
		
		if (exitform.a9[3].a1 == undefined || exitform.a9[3].a1 == "" || exitform.a9[3].a1 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_gym_a1").addClass("InvalidError");
			window.location.href = "#dv_a9_gym_a1";
		}
		else 
		{
			$("#drp_a9_gym_a1").removeClass("InvalidError");
		}
		
		
		
		
		if (exitform.a9[2].a4 == undefined || exitform.a9[2].a4 == "" || exitform.a9[2].a4 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_office_a4").addClass("InvalidError");
			window.location.href = "#dv_a9_office_a4";
		}
		else 
		{
			$("#drp_a9_office_a4").removeClass("InvalidError");
		}
		
		if (exitform.a9[2].a3 == undefined  || exitform.a9[2].a3 == "" || exitform.a9[2].a3 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_office_a3").addClass("InvalidError");
			window.location.href = "#dv_a9_office_a3";
		}
		else 
		{
			$("#drp_a9_office_a3").removeClass("InvalidError");
		}
		
		
		if (exitform.a9[2].a2 == undefined  || exitform.a9[2].a2 == "" || exitform.a9[2].a2 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_office_a2").addClass("InvalidError");
			window.location.href = "#dv_a9_office_a2";
		}
		else 
		{
			$("#drp_a9_office_a2").removeClass("InvalidError");
		}
		
		if (exitform.a9[2].a1 == undefined  || exitform.a9[2].a1 == "" || exitform.a9[2].a1 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_office_a1").addClass("InvalidError");
			window.location.href = "#dv_a9_office_a1";
		}
		else 
		{
			$("#drp_a9_office_a1").removeClass("InvalidError");
		}
		
		
		
		if (exitform.a9[1].a5 == undefined  || exitform.a9[1].a5 == "" || exitform.a9[1].a5 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_library_a5").addClass("InvalidError");
			window.location.href = "#dv_a9_library_a5";
		}
		else 
		{
			$("#drp_a9_library_a5").removeClass("InvalidError");
		}
		
		if (exitform.a9[1].a4 == undefined || exitform.a9[1].a4 == "" || exitform.a9[1].a4 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_library_a4").addClass("InvalidError");
			window.location.href = "#dv_a9_library_a4";
		}
		else 
		{
			$("#drp_a9_library_a4").removeClass("InvalidError");
		}
		
		if (exitform.a9[1].a3 == undefined || exitform.a9[1].a3 == "" || exitform.a9[1].a3 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_library_a3").addClass("InvalidError");
			window.location.href = "#dv_a9_library_a3";
		}
		else 
		{
			$("#drp_a9_library_a3").removeClass("InvalidError");
		}
		
		
		if (exitform.a9[1].a2 == undefined || exitform.a9[1].a2 == "" || exitform.a9[1].a2 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_library_a2").addClass("InvalidError");
			window.location.href = "#dv_a9_library_a2";
		}
		else 
		{
			$("#drp_a9_library_a2").removeClass("InvalidError");
		}
		
		if (exitform.a9[1].a1 == undefined || exitform.a9[1].a1 == "" || exitform.a9[1].a1 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_library_a1").addClass("InvalidError");
			window.location.href = "#dv_a9_library_a1";
		}
		else 
		{
			$("#drp_a9_library_a1").removeClass("InvalidError");
		}
		
		if (exitform.a9[0].a5 == undefined || exitform.a9[0].a5 == "" || exitform.a9[0].a5 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_canteen_a5").addClass("InvalidError");
			window.location.href = "#dv_a9_canteen_a5";
		}
		else 
		{
			$("#drp_a9_canteen_a5").removeClass("InvalidError");
		}
		
		if (exitform.a9[0].a4 == undefined || exitform.a9[0].a4 == "" || exitform.a9[0].a4 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_canteen_a4").addClass("InvalidError");
			window.location.href = "#dv_a9_canteen_a4";
		}
		else 
		{
			$("#drp_a9_canteen_a4").removeClass("InvalidError");
		}
		
		if (exitform.a9[0].a3 == undefined || exitform.a9[0].a3 == "" || exitform.a9[0].a3 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_canteen_a3").addClass("InvalidError");
			window.location.href = "#dv_a9_canteen_a3";
		}
		else 
		{
			$("#drp_a9_canteen_a3").removeClass("InvalidError");
		}
		
		
		if (exitform.a9[0].a2 == undefined || exitform.a9[0].a2 == "" || exitform.a9[0].a2 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_canteen_a2").addClass("InvalidError");
			window.location.href = "#dv_a9_canteen_a2";
		}
		else 
		{
			$("#drp_a9_canteen_a2").removeClass("InvalidError");
		}
		
		if (exitform.a9[0].a1 == undefined || exitform.a9[0].a1 == "" || exitform.a9[0].a1 == null)
		{
			validationstatus = false ; 
			$("#drp_a9_canteen_a1").addClass("InvalidError");
			window.location.href = "#dv_a9_canteen_a1";
		}
		else 
		{
			$("#drp_a9_canteen_a1").removeClass("InvalidError");
		}
		
		
		if (exitform.a8 == undefined || exitform.a8 == null ||exitform.a8 == "")
		{
			validationstatus = false ; 
			$("#txt_a8").addClass("InvalidError");
			window.location.href = "#txt_a8";
		}
		else 
		{
			$("#txt_a8").removeClass("InvalidError");
		}
		
		if ((exitform.a7.eventName == undefined || exitform.a7.eventName =="" || exitform.a7.eventName== null) && exitform.a7.isParticipated == "yes")
		{
			validationstatus = false ; 
			$("#txt_a7").addClass("InvalidError");
			window.location.href = "#txt_a7";
		}
		else 
		{
			$("#txt_a7").removeClass("InvalidError");
		}
		
		if (exitform.a7.isParticipated == undefined)
		{
			validationstatus = false ; 
			$("#dv_a7").addClass("InvalidError");
			window.location.href = "#dv_a7";
		}
		else 
		{
			$("#dv_a7").removeClass("InvalidError");
		}
		
		if (exitform.a6[1].negativeComment == undefined || exitform.a6[1].negativeComment == null || exitform.a6[1].negativeComment == "")
		{
			validationstatus = false ; 
			$("#txt_a6_negative2").addClass("InvalidError");
			window.location.href = "#txt_a6_negative2";
		}
		else 
		{
			$("#txt_a6_negative2").removeClass("InvalidError");
		}
		
		
		if (exitform.a6[1].positiveComment == undefined || exitform.a6[1].positiveComment == null || exitform.a6[1].positiveComment == "")
		{
			validationstatus = false ; 
			$("#txt_a6_positive2").addClass("InvalidError");
			window.location.href = "#txt_a6_positive2";
		}
		else 
		{
			$("#txt_a6_positive2").removeClass("InvalidError");
		}
		
		if (exitform.a6[1].deptName == undefined || exitform.a6[1].deptName == null || exitform.a6[1].deptName == "")
		{
			validationstatus = false ; 
			$("#txt_a6_department2").addClass("InvalidError");
			window.location.href = "#txt_a6_department2";
		}
		else 
		{
			$("#txt_a6_department2").removeClass("InvalidError");
		}
		
		if (exitform.a6[0].negativeComment == undefined || exitform.a6[0].negativeComment == null || exitform.a6[0].negativeComment == "")
		{
			validationstatus = false ; 
			$("#txt_a6_negative1").addClass("InvalidError");
			window.location.href = "#txt_a6_negative1";
		}
		else 
		{
			$("#txt_a6_negative1").removeClass("InvalidError");
		}
		
		
		if (exitform.a6[0].positiveComment == undefined || exitform.a6[0].positiveComment == null || exitform.a6[0].positiveComment == "")
		{
			validationstatus = false ; 
			$("#txt_a6_positive1").addClass("InvalidError");
			window.location.href = "#txt_a6_positive1";
		}
		else 
		{
			$("#txt_a6_positive1").removeClass("InvalidError");
		}
		
		if (exitform.a6[0].deptName == undefined || exitform.a6[0].deptName == null || exitform.a6[0].deptName == "")
		{
			validationstatus = false ; 
			$("#txt_a6_department1").addClass("InvalidError");
			window.location.href = "#txt_a6_department1";
		}
		else 
		{
			$("#txt_a6_department1").removeClass("InvalidError");
		}
		
		if (exitform.a5 == undefined)
		{
			validationstatus = false ; 
			$("#dv_a5").addClass("InvalidError");
			window.location.href = "#dv_a5";
		}
		else 
		{
			$("#dv_a5").removeClass("InvalidError");
		}
		
		if (exitform.a4 == undefined)
		{
			validationstatus = false ; 
			$("#dv_a4").addClass("InvalidError");
			window.location.href = "#dv_a4";
		}
		else 
		{
			$("#dv_a4").removeClass("InvalidError");
		}
		
		if (exitform.a3.negativeEffect == undefined || exitform.a3.negativeEffect == null || exitform.a3.negativeEffect == "")
		{
			validationstatus = false ; 
			$("#txt_a3_negative").addClass("InvalidError");
			window.location.href = "#txt_a3_negative";
		}
		else 
		{
			$("#txt_a3_negative").removeClass("InvalidError");
		}
		
		if (exitform.a3.postiveEffect == undefined || exitform.a3.postiveEffect == null || exitform.a3.postiveEffect == "")
		{
			validationstatus = false ; 
			$("#txt_a3_possitive").addClass("InvalidError");
			window.location.href = "#txt_a3_possitive";
		}
		else 
		{
			$("#txt_a3_possitive").removeClass("InvalidError");
		}
		
		if (exitform.a2.lessValuable == undefined || exitform.a2.lessValuable == "" || exitform.a2.lessValuable == null)
		{
			validationstatus = false ; 
			$("#drp_a2_lessvaluable").addClass("InvalidError");
			window.location.href = "#dv_a2_lessvaluable";
		}
		else 
		{
			$("#drp_a2_lessvaluable").removeClass("InvalidError");
		}
		
		if (exitform.a2.valuable == undefined || exitform.a2.valuable == "" || exitform.a2.valuable == null)
		{
			validationstatus = false ; 
			$("#drp_a2_valuable").addClass("InvalidError");
			window.location.href = "#dv_a2_valuable";
		}
		else 
		{
			$("#drp_a2_valuable").removeClass("InvalidError");
		}
		
		if (exitform.a2.mostValuable == undefined || exitform.a2.mostValuable == "" || exitform.a2.mostValuable == null)
		{
			validationstatus = false ; 
			$("#drp_a2_mostvaluable").addClass("InvalidError");
			window.location.href = "#dv_a2_mostvaluable";
		}
		else 
		{
			$("#drp_a2_mostvaluable").removeClass("InvalidError");
		}
		
		if (exitform.a1 == undefined)
		{
			validationstatus = false ; 
			$("#dv_a1").addClass("InvalidError");
			window.location.href = "#dv_a1";
		}
		else 
		{
			$("#dv_a1").removeClass("InvalidError");
		}
		
		if (validationstatus== true)
		{
			$(".loader-img").show();
			$.ajax({
				 type: "POST",
				 url: GloableWebsite+"api/Feedback/AddExitForm",
				 data:JSON.stringify(exitform),
				 contentType: "application/json",
				 success: function (data, status, jqXHR) {
				     $(".loader-img").hide();
				     clearform();
				     localStorage.setItem("isExistFormSubmitted", 'true');
				    $('#successmsg').fadeIn().delay(800).fadeOut();
				    $("#studentExitForm").hide();
					//window.location.href = "#successmsg";
					setTimeout(function () { location.href = "Feedback.html"; }, 800);
				 },

				 error: function (jqXHR, status) {
					 $(".loader-img").hide();
					 $("#errormsg").show();
					 console.log(jqXHR);
				 }
          });
		}
		else 
		{
			// show validation error div
			$("#validationerrormsg").show();
			window.location.href="#validationerrormsg";
		}
		
	
	});
});


function clearform()
{
	$("#successmsg").hide();
	$("#errormsg").hide();
	$("#txt_a7").hide();
	// hide validation error div
	$("#validationerrormsg").hide();
	
	$("input[name=rd_a1]:checked").prop('checked', false);
	$('#drp_a2_mostvaluable').val("");
	$('#drp_a2_valuable').val("");
	$('#drp_a2_lessvaluable').val("");
	$('#txt_a3_possitive').val("");
	$('#txt_a3_negative').val("");	
	$('input[name=rd_a4]:checked').prop('checked', false);
	$('input[name=rd_a5]:checked').prop('checked', false);
	
	$('#txt_a6_department1').val("");
	$('#txt_a6_positive1').val("");
	$('#txt_a6_negative1').val("");
	$('#txt_a6_department2').val("");
	$('#txt_a6_positive2').val("");
	$('#txt_a6_negative2').val("");
	$('input[name=rd_a7]:checked').prop('checked', false);
	$('#txt_a7').val("");
	$('#txt_a8').val("");
	$('#drp_a9_canteen_a1').val("");  
	$('#drp_a9_canteen_a2').val("");  
	$('#drp_a9_canteen_a3').val("");  
	$('#drp_a9_canteen_a4').val("");  
	$('#drp_a9_canteen_a5').val("");
				
	$('#drp_a9_library_a1').val("");
	$('#drp_a9_library_a2').val("");
	$('#drp_a9_library_a3').val("");
	$('#drp_a9_library_a4').val("");
	$('#drp_a9_library_a5').val("");
	$('#drp_a9_office_a1').val("");
	$('#drp_a9_office_a2').val("");
	$('#drp_a9_office_a3').val("");
	$('#drp_a9_office_a4').val("");		
	$('#drp_a9_gym_a1').val("");
	$('#drp_a9_gym_a2').val("");
	$('#drp_a9_gym_a3').val("");
	$('#drp_a9_gym_a4').val("");
	$('#drp_a9_sport_a1').val("");
	$('#drp_a9_sport_a2').val("");
	$('#drp_a9_sport_a3').val("");
	$('#drp_a9_health_a1').val("");
	$('#drp_a9_health_a2').val("");
	$('#drp_a9_health_a3').val("");
}

