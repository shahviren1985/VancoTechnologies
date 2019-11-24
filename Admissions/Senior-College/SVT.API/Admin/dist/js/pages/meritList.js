let cutOffFilter = {
    specialisation: '',
    student: '',
    category: ''
}

let CUT_OFF_LIST = [], studentList = [], filteredStudentList = []; SEATS_JSON = null;

$(function () {
    console.log('External JS in meritList')

    validateSession();

    SPECIALISATION.map((s) => {
        $('#drpDwnSpecialisationFilter').append(`<option value="${s}">${s}</option>`)
    })

    $('.getCutOff').on('change', onInputChange)
    $('#drpDwnRound').on('change', (e) => {
        console.log("Name : ", e.target.name, " : value :", e.target.value)
        let round = e.target.value
        if (CUT_OFF_LIST[round]) {
            getCutOff(cutOffFilter)
        } else {
            getCutOffListApi(e.target.value)
        }
    })
    $('#prvMeritListBtn').on('click', previewMeritList)
    $('#genMeritListBtn').on('click', generateMeritList)

    $('#txtCutOff').on('change', updateCutOff)
    getCutOffListApi("Round1");

    getSeatsJson();

    $('#drpDwnSpecialisationFilter').on('change', (evt) => {
        filterMeritList(evt.target.value)
    })

    $('#btnFltMeritList').on('click', resetMeritList)
})

function getCutOffListApi(round) {
    let cutoffJsonUrl = `${BASE_API_HOST}/File/GetJsonData/data/pdf?fileName=${round}.json`

    $.ajax({
        type: 'GET',
        url: cutoffJsonUrl,
        headers: { 'Content-Type': 'application/json' },
        success: (data) => {
            CUT_OFF_LIST[round] = data
            console.log(CUT_OFF_LIST, 'Cutoff list')
            getCutOff(cutOffFilter)
        }
    })

}

function onInputChange(eve) {
    let { name, value, id } = eve.target
    console.log(name, value)
    cutOffFilter[name] = value
    getCutOff(cutOffFilter)
}

function getCutOff(filter) {

    let list = CUT_OFF_LIST[$('#drpDwnRound').val()]
    //filter based on round
    if (!_u.isEmpty(list)) {
        //filter based on specilisation
        let courseToCutOff = _u.filter(list, (c) => c.specialisation == filter.specialisation)
        console.log(courseToCutOff, 'Course to Cut off')
        if (!_u.isEmpty(courseToCutOff)) {
            courseToCutOff = courseToCutOff[0]
            let categoryName = getCategoryName(filter)
            $('#txtCutOff').val(courseToCutOff[categoryName])

        }
    }
}

function getCategoryName(filter) {
    if (filter.category == 'open') {
        if (filter.student == "internal") {
            return "SVTOpenInternal"
        } else {
            return "ExternalOpen"
        }
    } else if (filter.category == "reserved") {
        if (filter.student == "internal") {
            return "SVTReservedInternal"
        } else {
            return "ExternalReserved"
        }
    }
}

function updateCutOff(evt) {
    console.log("value", evt.target.value)
    let value = evt.target.value;
    

    let list = CUT_OFF_LIST[$('#drpDwnRound').val()]
    //filter based on round
    if (!_u.isEmpty(list)) {
        //filter based on specilisation
        let courseToCutOff = _u.filter(list, (c) => c.specialisation == cutOffFilter.specialisation)
        console.log(courseToCutOff, 'Course to Cut off')
        if (!_u.isEmpty(courseToCutOff)) {
            courseToCutOff = courseToCutOff[0]
            let categoryName = getCategoryName(cutOffFilter)
            courseToCutOff[categoryName] = evt.target.value
            console.log(list, '*********************************')
            let round = $('#drpDwnRound').val()

            $('.loader').removeClass('hide')
            $('body input,button,select').attr('disabled', true)

            uploadJson(list, `${round}.json`, () => {
                console.log('after updating json')

                $('.loader').addClass('hide')
                $('body input,button,select,radio').attr('disabled', false)
            })
            //$('#txtCutOff').val(courseToCutOff[categoryName])
        }
    }

}

function previewMeritList() {
    let round = $('#drpDwnRound').val().toUpperCase()
    let category = cutOffFilter.category.toUpperCase()
    let isInternalStudent = cutOffFilter.student == 'internal' ? true : false
    if (_u.isEmpty(round) || _u.isEmpty(category) || _u.isEmpty(cutOffFilter.student)) {
        alert('Please Select Round, Category and Student Dropdown before previewing Merit List')
        return;
    }
    let url = `${BASE_API_HOST}/Form/PreviewMeritList?isSVT=${isInternalStudent}&category=${category}&round=${round}`
    $('#tbMeritListGrid tbody').empty()
    $('.loader').removeClass('hide')
    $('body input,button,select').attr('disabled', true);
    $('.courseInfoDiv').addClass('hide');
    $('#drpDwnSpecialisationFilter').val("");
    $("#lblTotalMeritListStudents").text(0);
    $.ajax({
        type: 'GET',
        url: url,
        headers: { 'Content-Type': 'application/json' },
        success: (response) => {
            $('.loader').addClass('hide')
            $('body input,button,select,radio').attr('disabled', false)

            /* PossibleCourseAdmitted */

            if (response.isSuccess) {
                console.log('Student List', response.StudentList)
                studentList = response.StudentList
                renderStudentGrid(response.StudentList)
                $("#lblTotalMeritListStudents").text(studentList.length);
            } else {
                alert('Error in fetching Merit list')
                $('#meritListGrid').addClass('hide')
                studentList = []
            }
        }
    })
}

function generateMeritList() {
    let round = $('#drpDwnRound').val().toUpperCase()
    let category = cutOffFilter.category.toUpperCase()
    let isInternalStudent = cutOffFilter.student == 'internal' ? true : false
    if (_u.isEmpty(round) || _u.isEmpty(category) || _u.isEmpty(cutOffFilter.student)) {
        alert('Please Select Round, Category and Student Dropdown before Generating Merit List')
        return;
    }

    let url = `${BASE_API_HOST}/Form/MeritListLogic?isSVT=${isInternalStudent}&category=${category}&round=${round}`
    $('.loader').removeClass('hide')
    $('body input,button,select').attr('disabled', true)
    $.ajax({
        type: 'POST',
        url: url,
        /* headers: { 'Content-Type': 'application/json' }, */
        success: (response) => {
            alert(response)

            $('.loader').addClass('hide')
            $('body input,button,select,radio').attr('disabled', false)
        }

    })
}

function renderStudentGrid(studentList) {
    $('#tbMeritListGrid tbody').empty();
    studentList.map((s) => {
        $('#tbMeritListGrid tbody').append(`
            <tr>
                <td>${s.SerialNumber}</td>                            
                <td>${s.FullName}</td>
                <td>${s.Percentage}</td>
                <td>${s.Category}</td>
                <td>${s.Caste}</td>
                <td>${s.PossibleCourseAdmitted}</td>
                <td>${s.InternalExternal}</td>
            </tr>
        `)
    })
    $('#meritListGrid').removeClass('hide')
}

function filterMeritList(specialisation) {
    let filteredList = _u.filter(studentList, (s) => {
        return s.PossibleCourseAdmitted == specialisation
    })
    if (specialisation != "") {
        $('.courseInfoDiv').removeClass('hide')
    } else {
        $('.courseInfoDiv').addClass('hide')
    }
    if (SEATS_JSON) {
        let seatsForSpecialization = (_u.filter(SEATS_JSON, s => s.specialisation == specialisation)[0]).Total;

        $("#lblCourseSeatsOccupied").text(filteredList.length);
        $("#lblTotalCourseSeats").text(seatsForSpecialization);
    }

    renderStudentGrid(filteredList)
}

function resetMeritList() {
    renderStudentGrid(studentList)
}

function getSeatsJson(round) {
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
        },
        error: (err) => {
            console.log(err)
        }
    })
    /*  $.getJSON(seatJsonUrl, function (param) {      
     }); */
}

function downloadPreviewMeritList(){
    
    let round = $('#drpDwnRound').val().toUpperCase()
    let category = cutOffFilter.category.toUpperCase()
    let isInternalStudent = cutOffFilter.student == 'internal' ? true : false
    if (_u.isEmpty(round) || _u.isEmpty(category) || _u.isEmpty(cutOffFilter.student)) {
        alert('Please Select Round, Category and Student Dropdown before previewing Merit List')
        return;
    }
    let url = `${BASE_API_HOST}/Form/GetPreviewMeritListReport?isSVT=${isInternalStudent}&category=${category}&round=${round}`;
    
    var a = document.createElement("a");
    a.href = url;
    a.target= "_blank";
    //a.download = filename;
    document.body.appendChild(a);
    a.click();

}