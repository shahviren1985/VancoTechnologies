var techers =  [];
$(document).ready(function(){
	$(".loader-img").hide();
	$('#successmsg').hide();
	$('#errormsg').hide();
	$('#validationerrormsg').hide();
	
	
	var userCode = localStorage.getItem("userCode");
	var userName = localStorage.getItem("userName");
	var RoleType = localStorage.getItem("roleType");
		
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
	
	$("#btnSubmit").click(function(){
		
		$('#successmsg').hide();
		$('#errormsg').hide();
		$('#validationerrormsg').hide();
		if($("#file").val()==undefined || $("#file").val() == "" || $("#file").val() == null)
		{
			$('#validationerrormsg').fadeIn().delay(2000).fadeOut();
		}
		else {
			$(".loader-img").show();
			//stop submit the form, we will post it manually.
			event.preventDefault();

			// Get form
			var form = $('#fileform')[0];

			// Create an FormData object
			var data = new FormData(form);

			// If you want to add an extra field for the FormData
			//data.append("CustomField", "This is some extra data, testing");

			// disabled the submit button
			//$("#btnSubmit").prop("disabled", true);

			$.ajax({
				type: "POST",
				enctype: 'multipart/form-data',
				url: GloableWebsite+"api/Feedback/UploadUsers",
				data: data,
				processData: false,
				contentType: false,
				cache: false,
				timeout: 600000,
				success: function (data) {
					$(".loader-img").hide();
					$('#successmsg').fadeIn().delay(800).fadeOut();	
					$("#file").val("");					
				},
				error: function (e) {
					$(".loader-img").hide();
					$('#errormsg').fadeIn().delay(1200).fadeOut();
				}
			});
		}
	});
});



