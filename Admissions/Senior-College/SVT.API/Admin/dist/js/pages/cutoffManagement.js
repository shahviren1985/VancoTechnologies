let SUMMARY_JSON = {
    "totalForms": 856,
    "openInternal": {
        "totalForms": 125,
        "maxPercent": 89.98,
        "minPercent": 76.34,
        "avgPercent": 81.56
    },
    "openExternal": {
        "totalForms": 462,
        "maxPercent": 96.98,
        "minPercent": 56.34,
        "avgPercent": 74.37
    },
    "reservedInternal": {
        "totalForms": 64,
        "maxPercent": 69.98,
        "minPercent": 46.34,
        "avgPercent": 61.56
    },
    "reservedExternal": {
        "totalForms": 205,
        "maxPercent": 78.98,
        "minPercent": 66.34,
        "avgPercent": 73.56
    }
}
let SPECILISATION_JSON;
let SPECILISATION_JSON1 = [
    {
        "name": "Food, Nutrition and Dietetics",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    },
    {
        "name": "Hospitality & Tourism Management",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    },
    {
        "name": "Mass Communication & Extension",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    },
    {
        "name": "Early Childhood care & Education",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    },
    {
        "name": "Textiles & Apparel Designing",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    },
    {
        "name": "Interior Design & Resource Management",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    },
    {
        "name": "Developmental Counselling",
        "openInternal": {
            "totalForms": 64,
            "maxPercent": 89.98,
            "minPercent": 76.34,
            "avgPercent": 81.56
        },
        "openExternal": {
            "totalForms": 123,
            "maxPercent": 96.98,
            "minPercent": 56.34,
            "avgPercent": 74.37
        },
        "reservedInternal": {
            "totalForms": 10,
            "maxPercent": 69.98,
            "minPercent": 46.34,
            "avgPercent": 61.56
        },
        "reservedExternal": {
            "totalForms": 34,
            "maxPercent": 78.98,
            "minPercent": 66.34,
            "avgPercent": 73.56
        }
    }

]

$(function () {
	
    validateSession();

    $('.sidebar-menu').tree()
    getSummaryJSON();
    $('#cutoffGridSubmitBtn').on('click', (e) => {
        if ($('#cutoffGrid').find('td.errInput').length > 0) {
            alert('There are errors in grid.Please fix errors before submitting')
            return;
        }


        let requestPayload = [],
            hasError = false
        $('#cutoffGrid tr.seatRow').each((index, tr) => {
            let course = $(tr).attr('data-course')
            let object = {}
            object.specialisation = course
            $(tr).find('td').each((d, c) => {
                if ($(c).hasAttr('contenteditable')) {
                    let text = Number($(c).text()).toFixed(2)
                    let category = $(c).attr('data-category')
                    object[category] = text
                }
            })
            requestPayload.push(object)
        })

        console.log(requestPayload, 'Request payload')
        //maek ajax call to upload json 
        let round = $('#drpDwnRound').val()

        uploadJson(requestPayload, `${round}.json`, getCutoffJson(round))

    })
    $('.openMoreInfo').on('click', (eve) => {
    })
})

function openMoreInfo(specialisation, index, eve) {
    if ($(eve.target).hasClass('fa-angle-down')) {
        $($(eve.target).parents('tr').siblings('.moreInfo')[index]).removeClass('hide');
        $(eve.target).removeClass('fa-angle-down').addClass('fa-angle-up');
    } else if ($(eve.target).hasClass('fa-angle-up')) {
        $($(eve.target).parents('tr').siblings('.moreInfo')[index]).addClass('hide');
        $(eve.target).addClass('fa-angle-down').removeClass('fa-angle-up');
    }
}

function getCutoffJson(round) {
    let cutoffJsonUrl = `${BASE_API_HOST}/File/GetJsonData/data/pdf?fileName=${round}.json`

    $.ajax({
        type: 'GET',
        url: cutoffJsonUrl,
        headers: { 'Content-Type': 'application/json' },
        success: (data) => {
            console.log('JSON Data', data)
            let table = $('#cutoffGrid tbody')
            $(table).empty()
            $(table).append(`<tr>
                <th class="col-md-4"></th>
                <th class="col-md-2" scope="col">SVT Internal Open</th>
                <th class="col-md-2" scope="col">SVT Internal Reserved</th>
                <th class="col-md-2" scope="col">External Open</th>
                <th class="col-md-2" scope="col">External Reserved</th>
              </tr>`);
            if (data && Array.isArray(data)) {
                data.map((d, index) => {
                    table.append(`
                <tr class="seatRow" data-course="${d.specialisation}">                
                  <th class="col-md-4" scope="row">${d.specialisation} <span class="openMoreInfo" style="cursor:pointer" onclick="openMoreInfo('${d.specialisation}','${index}',event);"><i class="fa fa-angle-down"></i></span> </th>
                  <td class="col-md-2" data-category="${'SVTOpenInternal'}" contenteditable>${d.SVTOpenInternal}</td>
                  <td class="col-md-2" data-category="${'SVTReservedInternal'}" contenteditable>${d.SVTReservedInternal}</td>
                  <td class="col-md-2" data-category="${'ExternalOpen'}" contenteditable>${d.ExternalOpen}</td>
                  <td class="col-md-2" data-category="${'ExternalReserved'}" contenteditable>${d.ExternalReserved}</td>                
                </tr>

                <tr style="padding:0 ;" class="hide moreInfo" id="spcialisationInfo_${d.specialisation}" data-course="${d.specialisation}">                
                  <td colspan="5" style="padding:0 ;">
                    ${renderSpecialisationSummaryGrid(d.specialisation)}                  
                  </td>
                </tr>
              `)
                })


                $('#cutoffGrid td').on('mouseenter', function () {
                    $(this).closest('table').find('td').removeClass('selected')
                    $(this).addClass('selected')
                });
                $('#cutoffGrid td').on('mouseleave', function () {
                    $(this).removeClass('selected')
                });

                $('#cutoffGrid td').on('keyup', function (e) {
                    if (e.keyCode === 9) {
                        $(this).closest('table').find('td').removeClass('selected')
                        $(this).addClass('selected')
                    } else {
                        let text = $(this).text()
                        //console.log(`${isNaN(text)} : ${!(Number(text) % 1 == 0)} : ${Number(text) < 1} : ${Number(text) > 100}`)
                        /*  if (isNaN(text) || !(Number(text) % 1 == 0) || Number(text) < 1 || Number(text) > 100) { */
                        if (isNaN(text) || !text.match(/^((?:|0|[1-9]\d?|100)(?:\.\d{1,2})?)$/)) {
                            $(this).addClass('errInput')
                        } else {
                            $(this).removeClass('errInput')
                        }
                    }
                });

            }
        },
        error: (err) => {
            console.log('Error fetching cutoff json', err)
        }
    })
}

function renderCutOffGrid() {
    getCutoffJson($('#drpDwnRound').val())
}

function renderSpecialisationSummaryGrid(specialisation) {
    let summaryJson = _u.filter(SPECILISATION_JSON, e => e.name == specialisation)

    console.log('Summary JSON', summaryJson)

    let table = `<table class="specialisationSummaryTable table table-bordered" id='specialisationSummaryGrid'>`;
    if (summaryJson && summaryJson.length > 0) {
        summaryJson = summaryJson[0];
        let { openExternal, openInternal, reservedExternal, reservedInternal } = summaryJson;

        for (let i = 0; i < 4; i++) {
            let columnName = getSummaryColumnName(i);
            let col1 = openInternal ? (i==0 ? openInternal[columnName] : openInternal[columnName].toFixed(2)) : '-';
            let col2 = reservedInternal ? (i==0 ? reservedInternal[columnName] : reservedInternal[columnName].toFixed(2) ) : '-';
            let col3 = openExternal ? ( i==0 ? openExternal[columnName] : openExternal[columnName].toFixed(2)) : '-';
            let col4 = reservedExternal ? (i ==0 ? reservedExternal[columnName] : reservedExternal[columnName].toFixed(2)) : '-';
            table += (`
            <tr>
                <th class="col-md-4" scope="row">${getSummaryRowHeader(i)}</th>
                <td class="col-md-2">${col1}</td>                
                <td class="col-md-2">${col2}</td>
                <td class="col-md-2">${col3}</td>
                <td class="col-md-2">${col4}</td>
            </tr>
           `)
        }
        return table += "</table>";
    } else {
        return table += (`<tr><td colspan="5">No Summary Information Found for this course</td></tr></table>`);
    }


}

function renderSummaryGrid() {

    let table = $('#summaryGrid tbody')
    $(table).empty()
    $(table).append(`<tr>
                <th></th>                
                <th scope="col">Open Internal</th>
                <th scope="col">Reserved Internal</th>
                <th scope="col">Open External</th>                
                <th scope="col">Reserved External</th>
              </tr>`);

    let { openExternal, openInternal, reservedExternal, reservedInternal } = SUMMARY_JSON;

    for (let i = 0; i < 4; i++) {
        let columnName = getSummaryColumnName(i);

        let col1 = openInternal ? (i==0 ? openInternal[columnName] : openInternal[columnName].toFixed(2)) : '-';
        let col2 = reservedInternal ? (i==0 ? reservedInternal[columnName] : reservedInternal[columnName].toFixed(2) ) : '-';
        let col3 = openExternal ? ( i==0 ? openExternal[columnName] : openExternal[columnName].toFixed(2)) : '-';
        let col4 = reservedExternal ? (i ==0 ? reservedExternal[columnName] : reservedExternal[columnName].toFixed(2)) : '-';

        table.append(`
        <tr class="seatRow">
            <th scope="row">${getSummaryRowHeader(i)}</th>
            <td>${col1}</td>                        
            <td>${col2}</td>
            <td>${col3}</td>
            <td>${col4}</td>
        </tr>
       `)
    }



}

function getSummaryColumnName(index) {
    switch (index) {
        case 0:
            return "totalForms";
        case 1:
            return "maxPercent";
        case 2:
            return "minPercent";
        case 3:
            return "avgPercent";
    }
}

function getSummaryRowHeader(index) {
    switch (index) {
        case 0:
            return "Total Forms";
        case 1:
            return "Max. Percentage";
        case 2:
            return "Min. Percentage";
        case 3:
            return "Avg. Percentage";
    }
}

function getSummaryJSON() {
    $.ajax({
        type: 'GET',
        url: `${BASE_API_HOST}/Form/GetAdmissionFormSummary`,
        success: (respone) => {
            console.log('Response for SUmmary Json', respone);
            if (respone.isSuccess) {

                let { totalForms, openInternal, openExternal, reservedExternal, reservedInternal } = respone
                SUMMARY_JSON = {
                    totalForms, openInternal, openExternal, reservedExternal, reservedInternal
                };
                SPECILISATION_JSON = respone.specialisation;

                renderSummaryGrid();

                getCutoffJson('Round1');
            }

        },
        err: (err) => {
            console.log('Error while fetching Summary Json', err);
        }
    })
}



$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};


Number.isInteger = Number.isInteger || function (value) {
    return typeof value === "number" &&
        isFinite(value) &&
        Math.floor(value) === value;
};