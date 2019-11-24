
$(function () {
    validateSession();

    $("#txtFormDate").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '2018:2019',
        dateFormat: 'dd-mm-yy',
        defaultDate: new Date()
    });
	
	$("#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '2018:2019',
        dateFormat: 'dd-mm-yy',
        defaultDate: new Date()
    });
})

function generateReport() {
    
    let formType = $('input[name=rdoFormType]:checked').val() == "FormSubmitted" ? "GetFormSubmittedReport" : "GetFeesPaidReport";
    let date = $("#txtFormDate").datepicker( 'getDate' );    
	let endDate = $("#txtToDate").datepicker( 'getDate' );    
	
    let dateFormat = $.datepicker.formatDate( "dd/mm/yy", date )
	let endDateFormat =  $.datepicker.formatDate( "dd/mm/yy", endDate )
    let url = `${BASE_API_HOST}/Report/${formType}?startDate=${dateFormat}&endDate=${endDateFormat}`;
    var a = document.createElement("a");
    a.href = url;
    a.target = "_blank";
    //a.download = filename;
    document.body.appendChild(a);
    a.click();

}

function generateHostelReport() {
    
    let url = `${BASE_API_HOST}/Report/GetHostelReport`;
    var a = document.createElement("a");
    a.href = url;
    a.target = "_blank";
    //a.download = filename;
    document.body.appendChild(a);
    a.click();

}