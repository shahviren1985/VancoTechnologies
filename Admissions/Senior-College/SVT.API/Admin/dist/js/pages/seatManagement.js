$(function () {
    console.log("From external Js file")

    validateSession();

    $('.sidebar-menu').tree()
    getSeatsJson();

    $('#seatGridSubmitBtn').on('click', (e) => {
        if ($('#seatGrid').find('td.errInput').length > 0) {
            alert('There are errors in grid.Please fix errors before submitting')
            return;
        }


        let requestPayload = [],
            hasError = false
        $('#seatGrid tr.seatRow').each((index, tr) => {
            let course = $(tr).attr('data-course')
            let expectedTotal = Number($($(tr).find('td[data-total]')[0]).text())

            //console.log('Row : ', course, expectedTotal)
            let total = 0;

            let object = {}
            object.specialisation = course
            object.Total = expectedTotal

            $(tr).find('td').each((d, c) => {
                if ($(c).hasAttr('contenteditable')) {
                    let text = Number($(c).text())
                    total += text;
                    let category = $(c).attr('data-category')
                    //console.log(`text : ${text} :: course : ${course} :: cat : ${category}`)
                    object[category] = text
                }
            })

            if (total < expectedTotal) {
                alert(`${course}  course has seats allocation is less than Expected Seats`)
                hasError = true
            } else if (total > expectedTotal) {
                alert(`${course}  course has seats allocation is more than Expected Seats`)
                hasError = true
            } else {
                requestPayload.push(object)
            }
        })

        console.log(requestPayload, 'Request payload')
        if (!hasError) {
            uploadSeatsJSON(requestPayload);
        }

        //getSeatsJson();
    })

})

function uploadSeatsJSON(json) {

    var formData = new FormData();

    // JavaScript file-like object
    var content = JSON.stringify(json)
    //var blob = new Blob([content], 'Seats.json',{ type: "application/json" });
    var f = new File([content], "Seats.json", { type: "application/json", lastModified: new Date() })

    formData.append("test", f);

    var request = new XMLHttpRequest();
    request.open("POST", `${BASE_API_HOST}/File/Upload/data/pdf`);
    request.send(formData);

    request.addEventListener("load", (evt) => {
        getSeatsJson()
    });
    request.addEventListener("error", (evt) => {
        alert('Error while updating Seats Data')
        console.log(evt, 'Error while updating Seats Data')
    });

}

function getSeatsJson() {
    let seatJsonUrl = `${BASE_API_HOST}/File/GetJsonData/data/pdf?fileName=Seats.json`

    /* $.getJSON('../data/Seats.json', function (data) { */
    console.log('Seats Api Url ', seatJsonUrl)

    $.ajax({
        type: 'GET',
        url: seatJsonUrl,
        /* url:`../data/Seats.json`, */
        headers: { 'Content-Type': 'application/json' },
        success: (data) => {
            console.log('JSON Data', data)
            let table = $('#seatGrid tbody')
            SEATS_JSON = data;
            $(table).empty()
            if (data && Array.isArray(data)) {
                data.map((d) => {
                    table.append(`
                  <tr class="seatRow" data-course="${d.specialisation}">                
                    <th scope="row">${d.specialisation}</th>
                    <td data-category="${'SVTOpenInternal'}" contenteditable>${d.SVTOpenInternal}</td>
                    <td data-category="${'SVTReservedInternal'}" contenteditable>${d.SVTReservedInternal}</td>
                    <td data-category="${'ExternalOpen'}" contenteditable>${d.ExternalOpen}</td>
                    <td data-category="${'ExternalReserved'}" contenteditable>${d.ExternalReserved}</td>
                    <td contenteditable>0</td>
                    <td data-total="" >${d.Total}</td>
                  </tr>
                `)
                })


                $('#seatGrid td').on('mouseenter', function () {
                    $(this).closest('table').find('td').removeClass('selected')
                    $(this).addClass('selected')
                });
                $('#seatGrid td').on('mouseleave', function () {
                    $(this).removeClass('selected')
                });

                $('#seatGrid td').on('keyup', function (e) {
                    if (e.keyCode === 9) {
                        $(this).closest('table').find('td').removeClass('selected')
                        $(this).addClass('selected')
                    } else {
                        let text = $(this).text()
                        //console.log(`${isNaN(text)} : ${!(Number(text) % 1 == 0)} : ${Number(text) < 1} : ${Number(text) > 100}`)
                        if (isNaN(text) || !(Number(text) % 1 == 0) || Number(text) < 1 || Number(text) > 100) {
                            $(this).addClass('errInput')
                        } else {
                            $(this).removeClass('errInput')
                        }
                    }
                });

            }
        },
        error: (err) => {
            console.log(err)
        }
    })
    /*  $.getJSON(seatJsonUrl, function (param) {      
     }); */
}

$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};