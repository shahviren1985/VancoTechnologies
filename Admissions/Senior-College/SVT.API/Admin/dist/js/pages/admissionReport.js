


$(function () {
    console.log('in admission report js')
    validateSession();
})


function generateGeneralReport() {


    /* $('.loader').removeClass('hide')
    $('body input,button,select').attr('disabled', true) */

    let url = `${BASE_API_HOST}/Report/GenerateGeneralReport `
    var a = document.createElement("a");
    a.href = url;
    //a.download = filename;
    document.body.appendChild(a);
    a.click();

    /* a.onload = () => {
        alert('link loaded')
        $('.loader').addClass('hide')
        $('body input,button,select,radio').attr('disabled', false)
    }; */


}