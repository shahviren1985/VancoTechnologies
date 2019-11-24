
let REPORT_IDS = {
    "Round1": {
        "Developmental Counselling": {
            "SVTOpenInternal": "1",
            "SVTReservedInternal": "2",
            "ExternalOpen": "3",
            "ExternalReserved": "4"
        },
        "Early Childhood care & Education": {
            "SVTOpenInternal": "5",
            "SVTReservedInternal": "6",
            "ExternalOpen": "7",
            "ExternalReserved": "8"
        },
        "Interior Design & Resource Management": {
            "SVTOpenInternal": "9",
            "SVTReservedInternal": "10",
            "ExternalOpen": "11",
            "ExternalReserved": "12"
        },
        "Textiles & Apparel Designing": {
            "SVTOpenInternal": "13",
            "SVTReservedInternal": "14",
            "ExternalOpen": "15",
            "ExternalReserved": "16"
        },
        "Hospitality & Tourism Management": {
            "SVTOpenInternal": "17",
            "SVTReservedInternal": "18",
            "ExternalOpen": "19",
            "ExternalReserved": "20"
        },

        "Mass Communication & Extension": {
            "SVTOpenInternal": "21",
            "SVTReservedInternal": "22",
            "ExternalOpen": "23",
            "ExternalReserved": "24"
        },
        "Food, Nutrition and Dietetics": {
            "SVTOpenInternal": "25",
            "SVTReservedInternal": "26",
            "ExternalOpen": "27",
            "ExternalReserved": "28"
        }
    },
    "Round2": {
        "Developmental Counselling": {
            "SVTOpenInternal": "29",
            "SVTReservedInternal": "30",
            "ExternalOpen": "31",
            "ExternalReserved": "32"
        },
        "Early Childhood care & Education": {
            "SVTOpenInternal": "33",
            "SVTReservedInternal": "34",
            "ExternalOpen": "35",
            "ExternalReserved": "36"
        },
        "Interior Design & Resource Management": {
            "SVTOpenInternal": "37",
            "SVTReservedInternal": "38",
            "ExternalOpen": "39",
            "ExternalReserved": "40"
        },
        "Textiles & Apparel Designing": {
            "SVTOpenInternal": "41",
            "SVTReservedInternal": "42",
            "ExternalOpen": "43",
            "ExternalReserved": "44"
        },
        "Hospitality & Tourism Management": {
            "SVTOpenInternal": "45",
            "SVTReservedInternal": "46",
            "ExternalOpen": "47",
            "ExternalReserved": "48"
        },
        "Mass Communication & Extension": {
            "SVTOpenInternal": "49",
            "SVTReservedInternal": "50",
            "ExternalOpen": "51",
            "ExternalReserved": "52"
        },
        "Food, Nutrition and Dietetics": {
            "SVTOpenInternal": "53",
            "SVTReservedInternal": "54",
            "ExternalOpen": "55",
            "ExternalReserved": "56"
        }
    },
    "Round3": {
        "Developmental Counselling": {
            "SVTOpenInternal": "57",
            "SVTReservedInternal": "58",
            "ExternalOpen": "59",
            "ExternalReserved": "60"
        },
        "Early Childhood care & Education": {
            "SVTOpenInternal": "61",
            "SVTReservedInternal": "62",
            "ExternalOpen": "63",
            "ExternalReserved": "64"
        },
        "Interior Design & Resource Management": {
            "SVTOpenInternal": "65",
            "SVTReservedInternal": "66",
            "ExternalOpen": "67",
            "ExternalReserved": "68"
        },
        "Textiles & Apparel Designing": {
            "SVTOpenInternal": "69",
            "SVTReservedInternal": "70",
            "ExternalOpen": "71",
            "ExternalReserved": "72"
        },
        "Hospitality & Tourism Management": {
            "SVTOpenInternal": "73",
            "SVTReservedInternal": "74",
            "ExternalOpen": "75",
            "ExternalReserved": "76"
        },
        "Mass Communication & Extension": {
            "SVTOpenInternal": "77",
            "SVTReservedInternal": "78",
            "ExternalOpen": "79",
            "ExternalReserved": "80"
        },
        "Food, Nutrition and Dietetics": {
            "SVTOpenInternal": "81",
            "SVTReservedInternal": "82",
            "ExternalOpen": "83",
            "ExternalReserved": "84"
        }
    }
}
let specialisation = ["Developmental Counselling", "Early Childhood care & Education", "Food, Nutrition and Dietetics", "Hospitality & Tourism Management", "Interior Design & Resource Management", "Mass Communication & Extension", "Textiles & Apparel Designing"]
$(function () {
    validateSession();
    renderReportGrid("Round1");
})

function renderReportGrid(round) {
    //get list of specilisation.right now just get json of rounds and take spcialisation from that. ask viren to provide   list of specilisation
    let cutoffJsonUrl = `${BASE_API_HOST}/File/GetJsonData/data/pdf?fileName=${round}.json`

    /* $.ajax({
        type: 'GET',
        url: cutoffJsonUrl,
        headers: { 'Content-Type': 'application/json' },
        success: (data) => { */
    let table = $('#tbReportMgmtGrid tbody')
    if (SPECIALISATION && Array.isArray(SPECIALISATION)) {
        SPECIALISATION.map((d) => {
            table.append(`
                <tr class="courseRow" data-course="${d}">                
                  <th scope="row">${d}</th>
                  <td data-category="${'SVTOpenInternal'}" ><button class="btn btn-info" onclick="generateReport('${d}','SVTOpenInternal')">Generate</button></td>
                  <td data-category="${'SVTReservedInternal'}" ><button class="btn btn-info" onclick="generateReport('${d}','SVTReservedInternal')">Generate</button></td>
                  <td data-category="${'ExternalOpen'}" ><button class="btn btn-info" onclick="generateReport('${d}','ExternalOpen')">Generate</button></td>
                  <td data-category="${'ExternalReserved'}" ><button class="btn btn-info" onclick="generateReport('${d}','ExternalReserved')">Generate</button></td> 
                </tr>
              `)
        })
    }
    /*   }
  }) */

}

function generateReport(specialisation, category) {
    console.log(specialisation, category, $('#drpDwnRound').val())
    let round = $('#drpDwnRound').val()
    let reportId = REPORT_IDS[round][specialisation][category]
    let url = `${BASE_API_HOST}/Report/GenerateReport?reportId=${reportId}`
    var a = document.createElement("a");
    a.href = url;
    a.target = "_blank";
    //a.download = filename;
    document.body.appendChild(a);
    a.click();

}