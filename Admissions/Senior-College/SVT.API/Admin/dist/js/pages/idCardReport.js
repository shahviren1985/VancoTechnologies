


$(function () {
    validateSession();
    SPECIALISATION.map((sp)=>{
        $('#drpDwnSpecialisation').append(`<option value="${sp}">${sp}</option>`)
    })
    $('#generateReportBtn').on('click',(evt)=>{
        generateReport()
    })
   
})


function generateReport() {
    let specialisation = $('#drpDwnSpecialisation').val()
    if(_u.isEmpty(specialisation)){
        alert('Please Select Specialisation For Generating Report')
        return;
    }
    console.log(specialisation)

    let url = encodeURI(`${BASE_API_HOST}/Report/GenerateStudentIdCardReport?Specialization=${specialisation}`).replace(/&/g,"%26");

    console.log(url, '**********')

    var a = document.createElement("a");
    a.href = url;
    //a.download = filename;
    document.body.appendChild(a);
    a.click();

}