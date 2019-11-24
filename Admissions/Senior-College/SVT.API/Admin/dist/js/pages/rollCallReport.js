


$(function () {
    validateSession();
    SPECIALISATION.map((sp) => {
        $('#drpDwnSpecialisation').append(`<option value="${sp}">${sp}</option>`)
    })

    $('#generateReportBtn').on('click', generateReport)
})


function generateReport() {
    /* let specialisation = $('#drpDwnSpecialisation').val()
    if (_u.isEmpty(specialisation)) {
        alert("You need to select specialisation before generating report.")
        return;
    } */

    let url = `${BASE_API_HOST}/form/UpdateFinalAdmitted`
    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => {
            if (response.isSuccess) {
                let genUrl = `${BASE_API_HOST}/form/GenerateRollNumbers`
                $.ajax({
                    type: 'GET',
                    url: genUrl,
                    success: (genResp) => {
                        let ele = $(getMsgModal('success', genResp))
                        $('.messageDiv').append(ele).removeClass('hide')
                        setTimeout(() => {
                            ele.remove()
                        }, 4000)
                        $('.box-success').boxWidget()
                    },
                    error: (err) => {

                    }
                })

            } else {
                //alert('Student info updated successfully'
                let ele = $(getMsgModal('warning', response))
                $('.messageDiv').append(ele).removeClass('hide')
                setTimeout(() => {
                    ele.remove()
                }, 4000)
                $('.box-success').boxWidget()
            }

        },
        error: (err) => {

        }
    })



    //let url = `${BASE_API_HOST}//form/GenerateRollNumbers`

    /*   var a = document.createElement("a");
      a.href = url;
      //a.download = filename;
      document.body.appendChild(a);
      a.click(); */
}

function getMsgModal(modalType, data) {
    return `<div class="box box-${modalType} box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">${modalType}</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="remove">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">
                ${modalType == 'warning' ? 'Error while Generating Roll Numbers For Students' : 'Roll Numbers Successfully generated.'}
                </div>
            </div>`
}