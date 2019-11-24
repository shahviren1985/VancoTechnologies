


$(function () {
    console.log('in admission report js');
    validateSession();
})


function generateGeneralReport() {
    let url = `${BASE_API_HOST}/Report/AdmittedEnrollmentStudentDetail`
    var a = document.createElement("a");
    a.href = url;    
    document.body.appendChild(a);
    a.click();
}