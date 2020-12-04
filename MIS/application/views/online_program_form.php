<?php
//include_once 'dbconfig.php';
if (isset($_GET['course'])) 
{
    $course = $_GET['course'];
}
?>
<!DOCTYPE html>
<html lang="en">

    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>SVT- Self Financed Programs - Admissions Form</title>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:400,100,300,500">
        <link rel="stylesheet" href="assets/online-program/assets/bootstrap/css/bootstrap.min.css">
        <link rel="stylesheet" href="assets/online-program/assets/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" href="assets/online-program/assets/css/form-elements.css">
        <link rel="stylesheet" href="assets/online-program/assets/css/style.css">
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->

        <link rel="shortcut icon" href="assets/online-program/assets/ico/favicon.png">
        <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/online-program/assets/ico/svt-icon-144.png">
        <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/online-program/assets/ico/svt-icon-114.png">
        <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/online-program/assets/ico/svt-icon-72.png">
        <link rel="apple-touch-icon-precomposed" href="assets/online-program/assets/ico/svt-icon-57.png">

        <style>
            .ui-datepicker {
                width: 20em;
                padding: .2em .2em 0;
                display: none;
            }
            td {
                text-align: left;
            }
        </style>

    </head>

    <body>

        <nav class="navbar navbar-inverse navbar-no-bg" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#top-navbar-1">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="index.php">SVT- Self Financed Programs - Admissions Form</a>
                </div>
                <div class="collapse navbar-collapse" id="top-navbar-1">
                    <h1>  <strong>SVT</strong> - Self Financed Programs - Admissions Form</h1>
                </div>
            </div>
        </nav>

        <div class="top-content">
            <div class="container">

                <div class="dialog-background" style="display:none;">
                    <div class="" style="color: green;font-weight: bolder;margin-top: 100px;">
                        <p>Please wait while your form is getting uploaded...</p>
                        <p>This may take few minutes as we are uploading all your documents.</p>
                    </div>
                    <div class="dialog-loading-wrapper">
                        <img src="assets/online-program/assets/ico/ajax-loading.gif" title='Loading.....' />
                    </div>
                </div>

                <div class="row" id="divMain">
                    <div class="col-sm-12 col-sm-offset-0 col-md-12 col-md-offset-0 col-lg-12 col-lg-offset-0 form-box">
                        <form role="form" action="edit.php" method="post" enctype="multipart/form-data" class="f1" id="admissionFormData" name="admissionFormData">

                            <div id="divMenu">
                                <ul class="remove-ul-bullet-icon">
                                    <li class="li-font-set">
                                        <a href="index.php" style="color: #000000">Home</a> > <span style="color:red"> New Admission Form</span>
                                    </li>
                                </ul>
                                <div>Please reach out to <span style="color:red">svt.admissions@gmail.com</span> in case you face any technical difficulty in filling up the form.</div>
                            </div>

                            <div class="f1-steps">
                                <div class="f1-progress">
                                    <div class="f1-progress-line" data-now-value="0" data-number-of-steps="5" style="width: 0%;"></div>
                                </div>
                                <div class="f1-step" id="divStep1">
                                    <div class="f1-step-icon">
                                        <i class="fa fa-user"></i>
                                    </div>
                                    <p>Personal Details</p>
                                </div>

                                <div class="f1-step" id="divStep2">
                                    <div class="f1-step-icon"><i class="fa fa-bank"></i></div>
                                    <p>Bank Details</p>
                                </div>

                                <div class="f1-step" id="divStep3">
                                    <div class="f1-step-icon"><i class="fa fa-upload"></i></div>
                                    <p>Documents Upload</p>
                                </div>
                                <div class="f1-step" id="divStep4">
                                    <div class="f1-step-icon"><i class="fa fa-graduation-cap"></i></div>
                                    <p>Education Details </p>
                                </div>
                                <div class="f1-step" id="divStep5">
                                    <div class="f1-step-icon"><i class="fa fa-info"></i></div>
                                    <p>Other Details </p>
                                </div>

                            </div>

                            <div id="form-detail">
                                <input id="id" name="id" type="hidden" />

                                <fieldset id="fieldset_1">
                                    <h4>Personal Details</h4>

                                    <div class="form-detail">

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label for="CourseName" class="control-label">Program</label>
                                                    <input type="hidden" id="CourseName" name="CourseName" value="<?php echo $course; ?>" />
                                                    <select name="CourseNameddn" id="CourseNameddn" class="form-control input-group-lg validate-input mandatory">
                                                        <option value="">Select Course</option>
                                                        <option value="1" <?php
                                                        if ($course == 1) {
                                                            echo "selected";
                                                        }
                                                        ?> >M.Sc. Specialized Dietetics</option>
                                                        <option value="2" <?php
                                                        if ($course == 2) {
                                                            echo "selected";
                                                        }
                                                        ?>>M.Design (Fashion Design)</option>
                                                        <option value="3" <?php
                                                        if ($course == 3) {
                                                            echo "selected";
                                                        }
                                                        ?>>Diploma in Fashion Design</option>
                                                        <?php  ?><option value="4" <?php
                                                        if ($course == 4) {
                                                            echo "selected";
                                                        }
                                                        ?>>Diploma in Computer Aided Interior Design Management</option>
                                                        <?php  ?><option value="5" <?php
                                                        if ($course == 5) {
                                                            echo "selected";
                                                        }
                                                        ?>>Certificate in Computer Aided Interior Design Management</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <br />
                                            <h3 class="add-info">Personal Information</h3>
                                            <label class="control-label">Name of the Student</label>
                                            <div class="form-group">
                                                <div class="col-sm-4">
                                                    <label for="LastName" class="sr-only">Last Name</label>
                                                    <input id="LastName" class="form-control input-group-lg validate-input mandatory" type="text" name="LastName" title="Last Name" placeholder="Last Name" maxlength="100">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="FirstName" class="sr-only">First Name</label>
                                                    <input id="FirstName" class="form-control input-group-lg validate-input mandatory" type="text" name="FirstName" title="First Name" placeholder="First Name" maxlength="100">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="FatherhusbandsName" class="sr-only">Father / husbands Name</label>
                                                    <input id="FatherName" class="form-control input-group-lg validate-input mandatory" type="text" name="FatherName" title="Father / husband Name" placeholder="Father / husband Name" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <label class="control-label">Father / husband Full Name</label>
                                            <div class="form-group">
                                                <div class="col-sm-4">
                                                    <label for="FatherLastName" class="sr-only">Father / husband Last Name</label>
                                                    <input id="FatherLastName" class="form-control input-group-lg validate-input" type="text" name="FatherLastName" title="Father / husband Last Name" placeholder="Father / husband Last Name" maxlength="100">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="FatherFirstName" class="sr-only">Father / husband First Name</label>
                                                    <input id="FatherFirstName" class="form-control input-group-lg validate-input" type="text" name="FatherFirstName" title="Father / husband First Name" placeholder="Father / husband First Name" maxlength="100">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="FatherMiddleName" class="sr-only">Father / husband Middle Name</label>
                                                    <input id="FatherMiddleName" class="form-control input-group-lg validate-input" type="text" name="FatherMiddleName" title="Father / husband Middle Name" placeholder="Father / husband Middle Name" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <label class="control-label">Mother Full Name</label>
                                            <div class="form-group">
                                                <div class="col-sm-4">
                                                    <label for="MotherLastName" class="sr-only">Mother Last Name</label>
                                                    <input id="MotherLastName" class="form-control input-group-lg validate-input" type="text" name="MotherLastName" title="Mother Last Name" placeholder="Mother Last Name" maxlength="100">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="MotherFirstName" class="sr-only">Mother First Name</label>
                                                    <input id="MotherFirstName" class="form-control input-group-lg validate-input" type="text" name="MotherFirstName" title="Mother First Name" placeholder="Mother First Name" maxlength="100">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="MotherMiddleName" class="sr-only">Mother Middle Name</label>
                                                    <input id="MotherMiddleName" class="form-control input-group-lg validate-input" type="text" name="MotherMiddleName" title="Mother Middle Name" placeholder="Mother Middle Name" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <br />
                                                <h3 class="add-info">Identity Details</h3>
                                                <div class="col-md-6">
                                                    <label for="Aadhar Card Number" class="control-label">Aadhar Card Number</label>
<!--                                                    <input id="AadharNumber" class="form-control input-group-lg validate-input mandatory" type="text" name="AadharNumber" title="Aadhar Card Number" placeholder="Aadhar Card Number" maxlength="12" minlength="12" onblur="return CheckExistorNotAddharNumberNo(this);" onkeypress="return BlockNonNumbers(this, event, true, false);">-->
                                                    <input id="AadharNumber" class="form-control input-group-lg validate-input mandatory" type="text" name="AadharNumber" title="Aadhar Card Number" placeholder="Aadhar Card Number" maxlength="12" minlength="12" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label for="PAN Card Number" class="control-label">PAN Card Number</label>
                                                    <input id="PANNumber" class="form-control input-group-lg  " type="text" name="PANNumber" title="PAN Card Number" placeholder="PAN Card Number" maxlength="10">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Date of birth</label>
                                                    <div class="form-group internal input-group">
                                                        <input name="txtBirthDate" type="text" id="txtBirthDate" readonly="readonly" class="form-control input-group-lg validate-input" style="background-color: white; ">
                                                        <span class='input-group-addon' id="btnBirthDate">
                                                            <i class='glyphicon glyphicon-calendar'></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Age (as on 15th May 2020)</label>
                                                    <input name="CalculatedAge" type="text" id="CalculatedAge" placeholder="Calculated Age" class="form-control input-group-lg" maxlength="4" readonly
                                                           onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Nationality</label>
                                                    <input name="Nationality" type="text" id="Nationality" placeholder="Nationality" class="form-control input-group-lg validate-input" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Mother Tongue</label>
                                                    <input name="MotherTongue" type="text" id="MotherTongue" placeholder="Mother Tongue" class="form-control input-group-lg validate-input" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <br />
                                                <h3 class="add-info">Religion/Caste Details</h3>
                                                <div class="col-md-6">
                                                    <label class="control-label">Religion </label>
                                                    <select name="Religion" id="Religion" class="form-control input-group-lg validate-input">
                                                        <option value="">Select</option>
                                                        <option value="BUDDHIST">Buddhist</option>
                                                        <option value="CHRISTIAN">Christian</option>
                                                        <option value="HINDU">Hindu</option>
                                                        <option value="JAIN">Jain</option>
                                                        <option value="MUSLIM">Muslim</option>
                                                        <option value="OTHER">Other</option>
                                                        <option value="PARSI">Parsi</option>
                                                        <option value="SIKH">Sikh</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Category </label>
                                                    <div style="height:40px;">
                                                        <input type="radio" id="category1" name="Category" value="OPEN">
                                                        <label for="Open" class="control-label">Open</label>&nbsp;&nbsp;
                                                        <input type="radio" id="category2" name="Category" value="RESERVED">
                                                        <label for="Reserved" class="control-label">Reserved</label>
                                                    </div>
                                                    <label id="Category-error" class="error" for="Category"></label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" id="divReservedCategory" style="display:none;">
                                            <div class="form-group">
                                                <h5 class="control-label add-info">Additional Documentation Required</h5>
                                                <div class="col-md-6">
                                                    <label class="control-label">Reservation Category </label>
                                                    <select class="form-control" name="Caste" id="Caste">
                                                        <option value="">Select</option>
                                                        <option value="BC">BC</option>
                                                        <option value="OBC">OBC</option>
                                                        <option value="SC">SC</option>
                                                        <option value="ST">ST</option>
                                                    </select>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="control-label">Sub-Caste </label>
                                                    <input id="SubCaste" class="form-control input-group-lg " type="text" name="SubCaste" title="Sub Caste" placeholder="Sub Caste" maxlength="50">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <br />
                                                <h3 class="add-info">Contact Details</h3>
                                                <div class="col-md-6">
                                                    <label class="control-label">Correspondence Address / Temporary Address</label>
                                                    <textarea rows="4" name="CurrentAddress" maxlength="500" id="CurrentAddress" placeholder="Correspondence Address / Temporary Address" class="form-control input-group-lg validate-input"></textarea>
                                                    <input type="checkbox" id="chkSamepermanentaddress" name="Samepermanentaddress">
                                                    <label for="Samepermanentaddress">Same permanent address</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Permanent Address</label>
                                                    <textarea rows="4" name="PermanentAddress" placeholder="Permanent Address" id="PermanentAddress" maxlength="500" class="form-control input-group-lg validate-input"></textarea>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Contact No.(Residential)</label>
                                                    <input type="text" placeholder="Contact No.(Residential)" id="ResContactNo" name="ResContactNo" class="form-control input-group-lg validate-input" maxlength="15" minlength="10"
                                                           onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Mobile</label>
                                                    <input type="text" placeholder="Mobile" id="MobileNumber" name="MobileNumber" class="form-control input-group-lg validate-input" maxlength="11" minlength="10"
                                                           onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Email Address</label>
                                                    <input type="text" placeholder="Email Address" id="Email" name="Email" class="form-control input-group-lg validate-input" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <br />
                                                <h3 class="add-info">Guardian Details</h3>
                                                <div class="col-md-6">
                                                    <label class="control-label">Mother's Name</label>
                                                    <input name="GuardianMotherName" placeholder="Mother's Name" type="text" id="GuardianMotherName" class="form-control input-group-lg guardian" maxlength="100">
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="control-label">Father's Name</label>
                                                    <input name="GuardianFatherName" placeholder="Father's Name" type="text" id="GuardianFatherName" class="form-control input-group-lg guardian" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Occupation of Mother</label>
                                                    <input name="OccupationofMother" placeholder="Occupation of Mother" type="text" id="OccupationofMother" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="control-label">Occupation of Father</label>
                                                    <input name="OccupationofFather" placeholder="Occupation of Father" type="text" id="OccupationofFather" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="control-label">Education of Mother</label>
                                                    <input name="EducationofMother" placeholder="Education of Mother" type="text" id="EducationofMother" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="control-label">Education of Father</label>
                                                    <input name="EducationofFather" placeholder="Education of Father" type="text" id="EducationofFather" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="control-label">Guardian Address</label>
                                                    <textarea rows="4" name="GuardianAddress" placeholder="Guardian Address" id="GuardianAddress" maxlength="500" class="form-control input-group-lg  guardian"></textarea>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="control-label">Annual Income</label>
                                                    <input name="AnnualIncome" placeholder="Annual Income" type="text" id="AnnualIncome" class="form-control input-group-lg guardian" maxlength="20"
                                                           onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <label class="control-label">Tel. No.(Res)</label>
                                                    <input name="GuardianTelephoneNo" placeholder="Tel. No.(Res)" type="text" id="GuardianTelephoneNo" class="form-control input-group-lg " maxlength="12" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                                <div class="col-md-4">
                                                    <label class="control-label">Tel. No. (Office)</label>
                                                    <input name="GuardianOffice" placeholder="office" type="text" id="GuardianOffice" class="form-control input-group-lg" maxlength="20" minlength="10" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                                <div class="col-md-4">
                                                    <label class="control-label">Mobile</label>
                                                    <input name="GuardianMobile" placeholder="Mobile" type="text" id="GuardianMobile" class="form-control input-group-lg" maxlength="20" minlength="10" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="control-label">Emergency Contact No</label>
                                                    <input name="GuardianEmergencyConactNo" placeholder="Emergency Contact No" type="text" id="GuardianEmergencyConactNo" class="form-control input-group-lg guardian" maxlength="20" minlength="10" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="control-label">Email</label>
                                                    <input name="GuardianEmail" placeholder="Guardian Email" type="text" id="GuardianEmail" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="control-label">Native Place Address (For availing travel concession)</label>
                                                    <input name="NativePlaceAddress" placeholder="Native Place Address" type="text" id="NativePlaceAddress" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <br />
                                                <h3 class="add-info">Work Experience (Excluding Internship Experience)</h3>
                                                <div class="col-md-6">
                                                    <label class="control-label">Name of Organisation</label>
                                                    <input name="OrganisationName" placeholder="Organisation Name" type="text" id="OrganisationName" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="control-label">Designation</label>
                                                    <input name="Designation" placeholder="Designation" type="text" id="Designation" class="form-control input-group-lg" maxlength="100">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <label class="control-label">Experience(Y/M)</label>
                                            <div class="form-group">
                                                <div class="col-sm-6">
                                                    <label for="FatherLastName" class="sr-only">Year</label>
                                                    <input name="TotalExperienceInYear" placeholder="Year" type="text" id="TotalExperienceInYear" class="form-control input-group-lg " maxlength="2"
                                                           onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                                <div class="col-sm-6">
                                                    <label for="FatherFirstName" class="sr-only">Month</label>
                                                    <input name="TotalExperienceInMonth" placeholder="Month" type="text" id="TotalExperienceInMonth" class="form-control input-group-lg" maxlength="2"
                                                           onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </fieldset>

                                <fieldset id="fieldset_2">
                                    <h4>Bank Details</h4>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <label class="control-label">Bank Name</label>
                                                <input name="BankName" placeholder="Bank Name" type="text" id="BankName" class="form-control input-group-lg validate-input" maxlength="100">
                                            </div>
                                            <div class="col-md-6">
                                                <label class="control-label">Bank Address</label>
                                                <input name="BankAddress" placeholder="Bank Address" type="text" id="BankAddress" class="form-control input-group-lg validate-input" maxlength="100">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <label class="control-label">Bank Account Number</label>
                                                <input name="BankAccountNumber" placeholder="Bank Account Number" type="text" id="BankAccountNumber" class="form-control input-group-lg validate-input" maxlength="18"
                                                       onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
                                            </div>
                                            <div class="col-md-6">
                                                <label class="control-label">Account Type</label>
                                                <input name="AccountType" placeholder="Account Type" type="text" id="AccountType" class="form-control input-group-lg validate-input" maxlength="20">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <label class="control-label">IFSC Code</label>
                                                <input name="IFSCCode" placeholder="IFSC Code" type="text" id="IFSCCode" class="form-control input-group-lg validate-input" maxlength="11">
                                            </div>
                                            <div class="col-md-6">
                                                <label class="control-label">MICR Number</label>
                                                <input name="MICRNumber" placeholder="MICR Number" type="text" id="MICRNumber" class="form-control input-group-lg " maxlength="14">
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>

                                <fieldset id="fieldset_3">
                                    <h4>Documents Upload</h4>
                                    <div class="form-detail col-md-6">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>Photograph (Mandatory)</label>
                                                <div><input type="file" name="fileDp[]" id="fileDp" class="input-group-lg validate-input" accept=".jpg" /></div>
                                            </div>
                                            <div class="col-md-6">
                                                <img id="photo" src="#" style="display:none; width:100px" alt="Photograph">
                                            </div>
                                        </div>

                                        <div class="col-md-12 padding-top-10px">
                                            <p><i>Please upload your passport size photograph. Photo should be of size 15 cm * 15 cm. Make sure your photograph size does not exceed 500KB.</i></p>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>Signature (Mandatory)</label>
                                                <div>
                                                    <input type="file" name="fileDp[]" id="filesignature" class="input-group-lg validate-input" accept=".jpg" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <img id="photoSignature" src="#" alt="Signature" style=" display: none; width: 100px">
                                            </div>
                                        </div>
                                        <div class="col-md-12 padding-top-10px">
                                            <p><i>Please upload your signature. Signature image should be of size 15 cm * 15 cm. Make sure your signature image size does not exceed 500KB.</i></p>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>Parent Signature (Mandatory)</label>
                                                <div>
                                                    <input type="file" name="fileDp[]" id="filesignatureparent" class="input-group-lg validate-input" accept=".jpg" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <img id="photoSignatureParent" src="#" alt="Parent Signature" style=" display: none; width: 100px">
                                            </div>
                                        </div>

                                        <div class="col-md-12 padding-top-10px">
                                            <p><i>Please upload your parents signature. Parents signature image should be of size 15 cm * 15 cm. Make sure your signature image size does not exceed 500KB.</i></p>
                                        </div>
                                    </div>
                                    
                                </fieldset>

                                <fieldset>
                                    <div id="fileDpError" style="display:none;" class="alert alert-danger"><strong>Please!</strong> select valid photo file as per the description.</div>
                                    <div id="filesignatureError" style="display:none;" class="alert alert-danger"><strong>Please!</strong> select valid signature file as per the description.</div>
                                </fieldset>

                                <fieldset id="fieldset_4">
                                    <h4>Education Details</h4>
                                    <div class="form-detail">

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="control-label">Were you student of SVT Jr. College / Senior College (BSc.Home Science)</label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="YES" name="isSvt" value="YES">
                                                    <label for="YES" class="control-label">YES</label>&nbsp;&nbsp;
                                                    <input type="radio" id="No" name="isSvt" value="No">
                                                    <label for="No" class="control-label">NO</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" id="DivIsSvtSpecification" style="display:none">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <input type="radio" id="JuniorCollege" name="IsSvtKnowRefrence" value="Junior College">
                                                    <label for="JuniorCollege" class="control-label">Junior College</label>&nbsp;&nbsp;
                                                    <input type="radio" id="SeniorCollege" name="IsSvtKnowRefrence" value="Senior College">
                                                    <label for="SeniorCollege" class="control-label">Senior College</label>
                                                    <br>
                                                    <label id="IsSvtKnowRefrence-error" class="error" for="IsSvtKnowRefrence" style="display: inline-block;"></label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">

                                                <div class="col-md-6">
                                                    <label class="control-label">How did you get to know about the Master/Diploma/Certificate Courses</label>
                                                    <select class="form-control input-group-lg validate-input" name="KnowAboutCourse" id="KnowAboutCourse">
                                                        <option value="">Select</option>
                                                        <option value="ACADEMIC EXHIBITION">Academic Exhibition</option>
                                                        <option value="VISIT TO COLLEGE">Visit to College</option>
                                                        <option value="COLLEGE WEBSITE">College Website</option>
                                                        <option value="TELEPHONE">TelePhone</option>
                                                        <option value="NEWS PAPER">News Paper</option>
                                                        <option value="ANY OTHER">Any Other</option>
                                                    </select>
                                                </div>

                                                <div class="col-md-6" id="pleaseSpecify" style="display:none">
                                                    <label class="control-label">Please Specify</label>
                                                    <input id="OtherSpecifyHowYouknowCourses" class="form-control input-group-lg validate-input" type="text" name="OtherSpecifyHowYouknowCourses" title="How did you get to know about that?" placeholder="How did you get to know about the Master/Diploma/Certificate Courses" maxlength="150">
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <br />
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <table class="table">
                                                        <thead class="thead-dark" style="background-color:#9d9d9d;color:#ffffff">
                                                            <tr>
                                                                <th scope="col">Examination</th>
                                                                <th scope="col">Year of Passing</th>
                                                                <th scope="col">Name of the School/College</th>
                                                                <th scope="col">Medium of Instruction English/Hindi</th>
                                                                <th scope="col">Name of Board</th>
                                                                <th scope="col">Total %</th>
                                                                <th scope="col">Grade</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="validatemaontwo">
                                                            <tr class="pg" <?php
                                                            if ($course == 3 || $course == 4 || $course == 5) {
                                                                echo 'style="display:none;"';
                                                            }
                                                            ?>>
                                                                <th scope="row">PG DIPLOMA</th>
                                                                <td><input id="PGYearofPassing" name="PGYearofPassing" class="form-control input-group-lg pgerr" type="text" maxlength="4" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                <td><input id="PGSchoolName" name="PGSchoolName" class="form-control input-group-lg pgerr" type="text" maxlength="50"></td>
                                                                <td><input id="PGMedium" name="PGMedium" class="form-control input-group-lg pgerr" type="text" maxlength="50"></td>
                                                                <td><input id="PGBoardName" name="PGBoardName" class="form-control input-group-lg pgerr" type="text" maxlength="50"></td>
                                                                <td><input id="PGTotalPercent" name="PGTotalPercent" class="form-control input-group-lg pgerr" type="text"></td>
                                                                <td><input id="PGGrade" name="PGGrade" class="form-control input-group-lg" type="text" maxlength="10"></td>
                                                            </tr>
                                                            <tr class="bachelor">
                                                                <th scope="row">
                                                                    <?php if ($course == 1) { ?>
                                                                    <select class="form-control input-group-lg" name="ExaminationType" id="ExaminationType">
                                                                        <option value="">Select Bachelors</option>
                                                                        <option value="B.Sc. Food Nutrition and Dietetics">B.Sc. Food, Nutrition and Dietetics</option>
                                                                        <option value="B.Sc. Food Science & Nutrition">B.Sc. Food Science & Nutrition</option>
                                                                        <option value="B.Sc. Applied Nutrition">Applied Nutrition</option>
                                                                        <option value="OTHER">Other</option>
                                                                    </select>
                                                                    <br />
                                                                    <input id="OtherExaminationType" name="OtherExaminationType" class="form-control input-group-lg" style="display:none" type="text" maxlength="50">
                                                                    <?php } ?>
                                                                    <?php if ($course == 2) { ?>
                                                                    <select class="form-control input-group-lg" name="ExaminationType" id="ExaminationType">
                                                                        <option value="">Select Bachelors</option>
                                                                        <option value="B.Sc. Textiles & Apparel Design">B.Sc. Textiles & Apparel Design</option>
                                                                        <option value="B.Sc. Textile & Clothing">B.Sc. Textile & Clothing</option>
                                                                        <option value="B.Sc. Textile Science in Apparel Design">B.Sc. Textile Science in Apparel Design</option>
                                                                        <option value="B.Sc. Textiles & Fashion Technology">B.Sc. Textiles & Fashion Technology</option>
                                                                        <option value="B.A Fashion Design">B.A Fashion Design</option>
                                                                        <option value="B. Design (Fashion)">B. Design (Fashion)</option>
                                                                        <option value="B.F.Tech and B. Sc .Fashion Design">B.F.Tech and B.Sc. Fashion Design</option>
                                                                        
                                                                        <option value="OTHER">Other</option>
                                                                    </select>
                                                                    <br />
                                                                    <input id="OtherExaminationType" name="OtherExaminationType" class="form-control input-group-lg" style="display:none" type="text" maxlength="50">
                                                                    <?php } ?>
                                                                    <?php if ($course == 3 || $course == 4 || $course == 5) { ?>
                                                                    Bachelors
                                                                    <?php } ?>
                                                                </th>
                                                                <td><input id="BachelorYearofPassing" name="BachelorYearofPassing" class="form-control input-group-lg err1" type="text" maxlength="4" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                <td><input id="BachelorSchoolName" name="BachelorSchoolName" class="form-control input-group-lg err1" type="text" maxlength="50"></td>
                                                                <td><input id="BachelorMedium" name="BachelorMedium" class="form-control input-group-lg err1" type="text" maxlength="50"></td>
                                                                <td><input id="BachelorBoardName" name="BachelorBoardName" class="form-control input-group-lg err1" type="text" maxlength="50"></td>
                                                                <td><input id="BachelorTotalPercent" name="BachelorTotalPercent" class="form-control input-group-lg err1" type="text"></td>
                                                                <td><input id="BachelorGrade" name="BachelorGrade" class="form-control input-group-lg" type="text" maxlength="10"></td>
                                                            </tr>
                                                            <tr class="hsc">
                                                                <th scope="row">H.S.C.</th>
                                                                <td><input id="HscYearofPassing" name="HscYearofPassing" class="form-control" type="text" maxlength="4"></td>
                                                                <td><input id="HscSchoolName" name="HscSchoolName" class="form-control input-group-lg err1 err2" type="text" maxlength="50"></td>
                                                                <td><input id="HscMedium" name="HscMedium" class="form-control input-group-lg err1 err2" type="text" maxlength="50"></td>
                                                                <td><input id="HscBoardName" name="HscBoardName" class="form-control input-group-lg err1 err2" type="text" maxlength="50"></td>
                                                                <td><input id="HscTotalPercent" name="HscTotalPercent" class="form-control input-group-lg err1 err2" type="text"></td>
                                                                <td><input id="HscGrade" name="HscGrade" class="form-control input-group-lg" type="text" maxlength="10"></td>
                                                            </tr>
                                                            <tr class="ssc">
                                                                <th scope="row">S.S.C.</th>
                                                                <td><input id="SscYearofPassing" name="SscYearofPassing" class="form-control input-group-lg err1 err2 err3" type="text" maxlength="4" onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                <td><input id="SscSchoolName" name="SscSchoolName" class="form-control input-group-lg err1 err2 err3" type="text" maxlength="50"></td>
                                                                <td><input id="SscMedium" name="SscMedium" class="form-control input-group-lg err1 err2 err3" type="text" maxlength="50"></td>
                                                                <td><input id="SscBoardName" name="SscBoardName" class="form-control input-group-lg err1 err2 err3" type="text" maxlength="50"></td>
                                                                <td><input id="SscTotalPercent" name="SscTotalPercent" class="form-control input-group-lg err1 err2 err3" type="text"></td>
                                                                <td><input id="SscGrade" name="SscGrade" class="form-control input-group-lg" type="text" maxlength="10"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <br />
                                            <?php
                                            if ($course == 1 || $course == 2) {
                                                ?>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <table class="table">
                                                            <thead class="thead-dark" style="background-color:#9d9d9d;color:#ffffff">
                                                                <tr>
                                                                    <th scope="col">Semester</th>
                                                                    <th scope="col">Percentage</th>
                                                                    <th scope="col">GPA</th>
                                                                    <th scope="col" style="display: none">Had ATKT?</th>                                   
                                                                </tr>
                                                            </thead>
                                                            <tbody class="validatemaontwo">
                                                                <tr>
                                                                    <th scope="row">1</th>
                                                                    <td><input id="SemPer1" name="SemPer1" class="form-control input-group-lg pgerr" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>

                                                                    <td><input id="GPA1" name="GPA1" class="form-control input-group-lg pgerr" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td style="display: none"><input id="ISATKT1" name="ISATKT1" class="form-control input-group-lg" type="checkbox" maxlength="50"></td>                                                                
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">2</th>
                                                                    <td><input id="SemPer2" name="SemPer2" class="form-control input-group-lg" type="text" maxlength="50"  onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td><input id="GPA2" name="GPA2" class="form-control input-group-lg" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td style="display: none"><input id="ISATKT2" name="ISATKT2" class="form-control input-group-lg" type="checkbox" maxlength="50"></td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">3</th>
                                                                    <td><input id="SemPer3" name="SemPer3" class="form-control input-group-lg" type="text" maxlength="50"  onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td><input id="GPA3" name="GPA3" class="form-control input-group-lg" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td style="display: none"><input id="ISATKT3" name="ISATKT3" class="form-control input-group-lg" type="checkbox" maxlength="50"></td>                                                                
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">4</th>
                                                                    <td><input id="SemPer4" name="SemPer4" class="form-control input-group-lg" type="text" maxlength="50"  onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td><input id="GPA4" name="GPA4" class="form-control input-group-lg" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td style="display: none"><input id="ISATKT4" name="ISATKT4" class="form-control input-group-lg" type="checkbox" maxlength="50"></td>                                                                
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">5</th>
                                                                    <td><input id="SemPer5" name="SemPer5" class="form-control input-group-lg" type="text" maxlength="50"  onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td><input id="GPA5" name="GPA5" class="form-control input-group-lg" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td style="display: none"><input id="ISATKT5" name="ISATKT5" class="form-control input-group-lg" type="checkbox" maxlength="50"></td>                                                                
                                                                </tr>                                                            

                                                                <tr>
                                                                    <th scope="row">6</th>
                                                                    <td><input id="SemPer6" name="SemPer6" class="form-control input-group-lg" type="text" maxlength="50"  onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td><input id="GPA6" name="GPA6" class="form-control input-group-lg" type="text" maxlength="50" onkeypress="return BlockNonNumbers(this, event, true, false);"></td>
                                                                    <td style="display: none"><input id="ISATKT6" name="ISATKT6" class="form-control input-group-lg" type="checkbox" maxlength="50"></td>                                                                
                                                                </tr> 
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            <?php } ?>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset id="fieldset_5">
                                    <h4>Other Details</h4>
                                   <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label">Hobbies or special interests</label>
                                                <input name="HobbiesOrSpecailInterest" placeholder="Hobbies or special interests" type="text" id="HobbiesOrSpecailInterest" class="form-control input-group-lg validate-input" maxlength="200">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label">Honor / Prizes won at School / College </label>
                                                <input name="HonorPrizeName" placeholder="Honor /Prizes won at School / College" type="text" id="HonorPrizeName" class="form-control input-group-lg" maxlength="200">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label">Note</label>
                                                <textarea rows="4" name="Note" placeholder="Note" id="Note" class="form-control input-group-lg "></textarea>
                                            </div>
                                        </div>
                                    </div>

                                    <!--<div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label">Declaration</label>
                                                <p>I certify thati wil be abide by the rules and regulation of the college.</p>
                                            </div>
                                        </div>
                                    </div>-->

                                </fieldset>

                                <fieldset>
                                    <br />
                                    <div class="col-md-8">
                                        <div id="ErrorMessage" class="hidden alert alert-danger"></div>
                                        <div id="SuccessMessage" class="hidden alert alert-success"></div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="alert alert-danger">You need to upload marksheets and other documents in next page, after your application id is successfully generated. </div>
                                        
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group f1-buttons">
                                            <input type="submit" id="btnSave" value="Save" class="btn btn-info">
                                            <!--button type="button" id="btnSave" onclick="return SaveData(this, 0)" data-post="postformdata.php" class="btn btn-info">Save</button>
                                            <button type="button" name="button" id="submitBtn" data-toggle="modal" data-target="#confirm-submit" class="btn btn-next">Save & Submit</button-->

                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <input type="hidden" id="isSubmit" name="isSubmit" />
                            <input type="hidden" id="hdnPhoto" name="hdnPhoto" />
                            <input type="hidden" id="AppformId" name="AppformId" value="" />
                            <input type="hidden" id="hdnSignature" name="hdnSignature" />
                            <input type="hidden" id="hdnParentSignature" name="hdnParentSignature" />
                            <button type="button" style="display:none" name="button" id="btnSubmitconfirm" data-toggle="modal" data-target="#confirm-submit-message" class="btn btn-next">Confirm Popup</button>
                            <button type="button" style="display:none" name="button" id="btnSaveAndSubmitconfirm" data-toggle="modal" data-target="#confirm-save-submit-message" class="btn btn-next">Submit Confirm Popup</button>
                            <button type="button" style="display:none" name="button" id="btnAadharCardconfirm" data-toggle="modal" data-target="#confirm-aadharcard-message" class="btn btn-next">Confirm Aadhar Popup</button>
                        </form>
                    </div>
                </div>

                <div class="row" id="divViewPdf" style="display:none;">
                    <div class="col-sm-12 col-sm-offset-0 col-md-12 col-md-offset-0 col-lg-12 col-lg-offset-0 form-box">
                        <form role="form" action="" method="post" class="f1" name="studentDetail">
                            <div>
                                <ul class="remove-ul-bullet-icon">
                                    <li class="li-font-set">
                                        <a href="index.php" style="color: #000000">Home</a> > <span style="color:red"> New Admission Form</span>
                                    </li>
                                </ul>
                            </div>
                            <div id="form-detail">
                                <fieldset>
                                    <div class="form-detail">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-sm-12" style="padding-top: 34px;">
                                                    <div class='embed-responsive' style='padding-bottom:150%;'>
                                                        <object id="viewPdf" type='application/pdf' width='100%' height='100%'></object>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </form>

                    </div>
                </div>

            </div>
        </div>

        <div class="modal fade" id="confirm-submit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width:1000px">
                <div class="modal-content">
                    <div class="modal-header" style="text-align:left;font-weight:500">
                        Confirm Submit
                    </div>
                    <div class="modal-body" style="text-align:left">
                        <p>When you click submit you application you can not modify the appliction.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-next" data-dismiss="modal">Cancel</button>
                        <button id="submitFromPopup" type="button" onclick="return SubmitButton(this, 1)" data-post="postformdata.php" class="btn btn-info">Submit</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="confirm-submit-message" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body" id="divMsg" style="text-align:left">
                    </div>
                    <input type="hidden" id="modelUrlValue" name="modelUrlValue">
                    <div class="modal-footer">
                        <button type="button" class="btn btn-info" id="btnClose">Ok</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="confirm-save-submit-message" tabindex=" -1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width:500px">
                <div class="modal-content">
                    <div class="modal-body" style="font-size: 14px; font-weight: 500;">
                        <div class="modal-body" id="divSuccessMsg" style="text-align:left">
                        </div>
                    </div>
                    <input type="hidden" id="PdfmodelUrlValue" name="PdfmodelUrlValue">
                    <div class="modal-footer">
                        <button type="button" class="btn btn-info" id="btnSubmitClose">Ok</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="confirm-aadharcard-message" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body" style="text-align:left">
                        <p>You have already submitted one form with same Aadhar card number.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-info" data-dismiss="modal">Ok</button>
                    </div>
                </div>
            </div>
        </div>

        <script src="assets/online-program/assets/js/jquery-1.11.1.min.js"></script>
        <script src="assets/online-program/assets/bootstrap/js/bootstrap.min.js"></script>
        <script src="assets/online-program/assets/js/retina-1.1.0.min.js"></script>
        <script src="assets/online-program/assets/js/scripts.js?v=1.4"></script>
        <script src="assets/online-program/assets/js/common.js?v=1.4"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script src="assets/online-program/assets/js/jquery.validate.min.js"></script>
        <script src="assets/online-program/assets/js/customnew.js"></script>

        <div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 100%; width: 100%; z-index: -999999; position: fixed;">
            <img style="position: absolute; margin: 0px; padding: 0px; border: medium none; width: 100%; height: 100%; max-height: none; max-width: none; z-index: -999999; left: 0px; top: 0px;" src="assets/online-program/assets/ico/bg_1.jpg">
        </div>
        <script type="text/javascript">
                            function ValidatePer(str)
                            {
                                var x = parseFloat(str.value);

                                if (isNaN(x) || x < 0 || x > 100)
                                {
                                    alert("Total Percetange Cannot be greater than 100");
                                }
//                                } else
//                                {
//                                    var decimalSeparator = ".";
//                                    var val = "" + x;
//                                    if (val.indexOf(decimalSeparator) < val.length - 3)
//                                    {
//                                        alert("too much decimal");
//                                    }
//                                }
                            }
        </script>    
    </body>
</html>