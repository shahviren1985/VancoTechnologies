//const BASE_API_HOST = 'http://adminapps.in/SVTExams/SVTExam/api'
//const BASE_API_HOST = 'https://vancotech.com/Admissions/Graduates/api';
//const BASE_HOST = 'https://vancotech.com/Admissions/Graduates';

const BASE_API_HOST = 'http://localhost:50076/api';
const BASE_HOST = 'http://localhost:50076/';

//const BASE_API_HOST = 'http://admissions.svt.edu.in/api';
//const BASE_HOST = 'http://admissions.svt.edu.in/';

let SPECIALISATION = ["Developmental Counselling", "Early Childhood care & Education", "Food, Nutrition and Dietetics", "Hospitality & Tourism Management", "Interior Design & Resource Management", "Mass Communication & Extension", "Textiles & Apparel Designing"]
/* let CUT_OFF_LIST = [] */
function login(data, successCb) {
  let url = `${BASE_API_HOST}/User/Login`

  $.ajax({
    type: 'POST',
    url: url,
    data: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json'
    },
    success: (response) => {
      console.log(response, 'Success Response');
      if (response.isSuccess) {
        localStorage.setItem("LoggedInUser", JSON.stringify(response));
        localStorage.setItem("LoggedIn", response.isSuccess);
        successCb();
      } else {
        alert(response.errorMessage);
      }
    },
    error: (err) => {
      console.error(err, 'Login error response')
    }
  })
}

function logout() {
  localStorage.setItem("LoggedIn",false);
  localStorage.setItem("LoggedInUser",null);
  window.location.href = "login.html"
}

function renderSidebar() {
  let path = window.location.pathname
  let links = [{
      path: 'CutOffManagement.html',
      label: 'Cut-off Percentage '
    },
    {
      path: 'SeatManagement.html',
      label: 'Seat Management'
    },
    {
      path: 'MeritList.html',
      label: 'Merit List Management'
    },
    {
      path: 'StudentList.html',
      label: 'Admission Forms'
    },
    {
      label: 'Report Management',
      hasChildren: true,
      children: [{
          path: 'AdmissionFormsReport.html',
          label: 'Form Summary Report'
        },
        {
          path: 'AdmissionFormSummary.html',
          label: 'Admission Summary Report'
        },
        {
          path: 'FormReport.html',
          label: 'Form Report'
        },
        {
          path: 'MeritListReport.html',
          label: 'Merit List'
        },
        {
          path: 'RollCallReport.html',
          label: 'Roll Call'
        },
        {
          path: 'IDCardReport.html',
          label: 'ID Card'
        },
      ]
    }
  ]
  let sidebarHtml = `<!-- sidebar: style can be found in sidebar.less -->
      <section class="sidebar">
        <!-- Sidebar Menu -->
        <ul class="sidebar-menu" data-widget="tree">`



  links.map(linkItem => {
    if (linkItem.hasChildren) {


      let className = 'treeview';
      if (path.indexOf(linkItem.path) > -1) {
        className += ' active'
      }
      sidebarHtml += `<li class="${className}">
       <a href="#">
         <i class="fa fa-link"></i>
         <span>${linkItem.label}</span>
          <span class="pull-right-container">
            <i class="fa fa-angle-left pull-right"></i>
          </span>
       </a>
       <ul class="treeview-menu">
       `

      linkItem.children.map((childItem) => {

        let className = '';
        if (path.indexOf(childItem.path) > -1) {
          className = 'active'
        }
        sidebarHtml += `<li class="${className}">
         <a href="${childItem.path}">
           <i class="fa fa-link"></i>
           <span>${childItem.label}</span>
         </a>
       </li>`

      })

      sidebarHtml += '</ul></li>'

    } else {

      let className = '';
      if (path.indexOf(linkItem.path) > -1) {
        className = 'active'
      }
      sidebarHtml += `<li class="${className}">
         <a href="${linkItem.path}">
           <i class="fa fa-link"></i>
           <span>${linkItem.label}</span>
         </a>
       </li>`
    }
  })

  sidebarHtml += `</ul>
        <!-- /.sidebar-menu -->
      </section>
      <!-- /.sidebar -->`

  $('.main-sidebar').append(sidebarHtml)

}

function uploadJson(json, fileName, cb) {

  var formData = new FormData();

  // JavaScript file-like object
  var content = JSON.stringify(json)
  //var blob = new Blob([content], 'Seats.json',{ type: "application/json" });
  var f = new File([content], fileName, {
    type: "application/json",
    lastModified: new Date()
  })

  formData.append("test", f);

  var request = new XMLHttpRequest();
  request.open("POST", `${BASE_API_HOST}/File/Upload/data/pdf`);
  request.send(formData);

  request.addEventListener("load", (evt) => {
    if (cb) {
      cb()
    }
  });
  request.addEventListener("error", (evt) => {
    alert('Error while updating Seats Data')
    console.log(evt, 'Error while updating Seats Data')
  });

}

function validateSession() {
  let loggedIn = localStorage.getItem("LoggedIn");
  if (!(loggedIn == "true")) {
    window.location.href = "login.html";
  }
}

/* function getCutOffList() {
  let cutoffJsonUrl = `${BASE_API_HOST}/File/GetJsonData/data/pdf?fileName=Round1.json`

  $.ajax({
      type: 'GET',
      url: cutoffJsonUrl,
      headers: { 'Content-Type': 'application/json' },
      success: (data) => {
        CUT_OFF_LIST = data
      }})

} */

$(function () {
  //render a sidebar - remove sidebar code from each page.
  renderSidebar()
})
/* $.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};


Number.isInteger = Number.isInteger || function (value) {
    return typeof value === "number" &&
        isFinite(value) &&
        Math.floor(value) === value;
}; */

/* 


      <!-- sidebar: style can be found in sidebar.less -->
      <section class="sidebar">
        <!-- Sidebar Menu -->
        <ul class="sidebar-menu" data-widget="tree">
          <!-- Optionally, you can add icons to the links -->
          <li>
            <a href="CutOffManagement.html">
              <i class="fa fa-link"></i>
              <span>CutOff Management</span>
            </a>
          </li>
          <li class="active">
            <a href="SeatManagement.html">
              <i class="fa fa-link"></i>
              <span>Seat Management</span>
            </a>
          </li>
        </ul>
        <!-- /.sidebar-menu -->
      </section>
      <!-- /.sidebar -->


                <li class="active">
            <a href="SeatManagement.html">
              <i class="fa fa-link"></i>
              <span>Seat Management</span>
            </a>
          </li>
          <li>
            <a href="MeritList.html">
              <i class="fa fa-link"></i>
              <span>MeritList Management</span>
            </a>
          </li>
*/