let filterQuery = ''
$(function () {
    console.log('External JS in student List')

    validateSession();

    getStudentList(1)

    $('#drpDwnPageSize').on('change', () => {
        getStudentList(1, filterQuery)
    })

    $('#filterDivCollapsible').on('click', () => {
        $("#filterCollapsible").collapse("toggle")
    })

    $('#btnFilterStudent').on('click', () => {
        let firstName = $('#fltFirstname').val()
        let lastName = $('#fltLastname').val()
        let dob = $('#fltDob').val()

        if (!_u.isEmpty(firstName)) {
            filterQuery += `firstname=${firstName}&`
        }

        if (!_u.isEmpty(lastName)) {
            filterQuery += `lastName=${lastName}&`
        }

        if (!_u.isEmpty(dob)) {
            filterQuery += `dob=${dob}&`
        }

        getStudentList(1, filterQuery)
    })

    $('#btnFilterStudentReset').on('click', () => {
        filterQuery = ''

        $('#fltFirstname').val('')
        $('#fltLastname').val('')
        $('#fltDob').val('')

        getStudentList(1, filterQuery)
    })
})

function getStudentList(pageNo, filterQuery = "") {
    let pageSize = $('#drpDwnPageSize').val()

    let url = `${BASE_API_HOST}/Report/GetStudentWithPagination?${filterQuery}pageNo=${pageNo}&pageSize=${pageSize}`
    $('.studentOperation').off('change')

    $('.loader').removeClass('hide')
    $('body input,button,select').attr('disabled', true)

    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => {
            let table = $('#tbStudentListGrid tbody').empty()
            response.data.map((student) => {
                $(table).append(`
                    <tr>
                    <td> ${student["Serial Number"]} </td>
                    <td> ${student["Id"]} </td>
                    <td> ${student["Student Full Name"]} </td>
                    <td> ${student["Date Of Birth"]} </td>
                    <td> ${student["Percentage"]} </td>                    
                    <td> ${student["Caste"]} </td>
                    <td>                        
                        <input type="checkbox" id="IsFormSubmitted" class="studentOperation" data-formid="${student.Id}" data-formNumber="${student.MKCLFormNumber}" ${student["Form Submitted"] == "Yes" ? "checked" : ""}/>
                    </td>
                    <td>                        
                        <input type="checkbox" id="isCancelled" class="studentOperation" data-formid="${student.Id}" data-formNumber="${student.MKCLFormNumber}" ${student["Cancelled Student"] == "Yes" ? "checked" : ""}/>
                    </td>
                    <td>
                        <input type="checkbox" id="isDuplicate" class="studentOperation" data-formid="${student.Id}" data-formNumber="${student.MKCLFormNumber}" ${student["Duplicate Student"] == "Yes" ? "checked" : ""}/>
                    </td>
                    <td>
                        <input type="checkbox" id="IsFeesPaid" class="studentOperation" data-formid="${student.Id}" data-formNumber="${student.MKCLFormNumber}" ${student["Fees Paid"] == "Yes" ? "checked" : ""} />
                    </td>
                    <td> <a href="${BASE_HOST}/data/pdf/${student.PDFPath}" target="_blank" ><i class="fa fa-download"></i></a> </td>
                    </tr>
                `)
            })
            renderPaginationLinks(response.Count, pageSize, pageNo)


            $('.loader').addClass('hide')
            $('body input,button,select,radio').attr('disabled', false)

            $('.studentOperation').on('change', (evt) => {
                let { id, checked, dataset } = evt.target
                console.log(id, "  : ", checked, dataset.formid)
                updateStudentDetails(dataset.formid, id, checked)
            })
        },
        error: (err) => {
            console.error('Error fetching Student List', err)
        }
    })
}

function renderPaginationLinks(total, pageSize, activePageNo) {

    let paginationLinkEle = $('#paginationLinks').empty()

    //$('#drpDwnPageNo').val(activePageNo)
    $('#drpDwnPageNo').empty()

    let linkCount = Math.ceil((total / pageSize) + 1)
    let className = ""

    /* if (linkCount > 10) { */

    $('#paginationDiv').addClass('hide')
    $("#drpDwnPageNoDiv").removeClass('hide');
    $('#drpDwnPageNo').off('change')

    _u.range(1, linkCount).map((i) => {
        if (activePageNo == i) {
            $("#drpDwnPageNo").append(`<option value="${i}" selected>${i}</option>`)
        } else {
            $("#drpDwnPageNo").append(`<option value="${i}">${i}</option>`)
        }

    })

    $('#drpDwnPageNo').on('change', (evt) => {
        getStudentList(evt.target.value, filterQuery)
    })

    /* } else {
        $('#paginationDiv').removeClass('hide')
        $("#drpDwnPageNoDiv").addClass('hide')

        //render prev. page links 
        $(paginationLinkEle).append(`<li onclick="getStudentList(1);" ><a><i class="fa fa-angle-double-left"></i></a></li>`)

        for (let i = 1; i < linkCount; i++) {
            if (i == activePageNo) {
                className = "active"
            } else {
                className = ""
            }
            $(paginationLinkEle).append(`<li onclick = "getStudentList(${i});" class= "${className}" > <a>${i}</a></li > `)
        }
        //render next page links
        $(paginationLinkEle).append(`<li onclick="getStudentList(${linkCount - 1});" ><a><i class="fa fa-angle-double-right"></i></a></li>`)
    } */



}


function updateStudentDetails(formId, operation, value) {
    let url = `${BASE_API_HOST}/Form/UpdateStudentValues?id=${formId}&operation=${operation}&value=${value}`
    console.log(operation, ":ddddddd");
    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => {
            if (response.isSuccess) {
                //alert('Student info updated successfully')
                let ele = $(`<div class="box box-success box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">Success</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="remove">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">
                  ${getResponseMsg(formId, operation, value)}
                </div>
            </div>`)
                $('.messageDiv').append(ele).removeClass('hide')

                setTimeout(() => {
                    ele.remove()
                }, 4000)

                $('.box-success').boxWidget()
            }
        }
    })

}

function getResponseMsg(formId, operation, value) {
    return `Form Number 
            <strong>${formId}</strong> is marked as 
            ${value ? '' : 'Not '} 
            <strong>${getOperationtextMsg(operation)}</strong> `
}

function getOperationtextMsg(operation) {
    switch (operation.trim()) {
        case "isDuplicate":
            return "Duplicated";
        case "isCancelled":
            return "Cancelled"
        case "IsFormSubmitted":
            return "Submitted"
        case "isAdmitted":
            return "Admitted";
        case "IsFeesPaid":
            return "Fees Paid";
    }
}