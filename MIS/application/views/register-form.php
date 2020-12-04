<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SVT Junior College – <?php if($_GET['standard']==11){echo "XI Standard";}elseif($_GET['standard']==12){echo 'XII Standard';} ?> Registration Form</title>

    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/bootstrap/3.2.0/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/fontawesome/4.1.0/css/font-awesome.min.css">

    <!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css" />

    <!-- custom css -->
    <!-- <link rel="stylesheet" type="text/css" href="css/style.css">
    <link rel="stylesheet" type="text/css" href="css/responsive.css"> -->

    <link rel="stylesheet" type="text/css" href="<?php echo base_url('assets/css/registration-form/style.css" rel="stylesheet'); ?>">
    <link rel="stylesheet" type="text/css" href="<?php echo base_url('assets/css/registration-form/responsive.css" rel="stylesheet'); ?>">

    <link href="https://fonts.googleapis.com/css?family=Roboto:400,400i,500,500i,700,700i,900" rel="stylesheet">

    <!--<link rel="stylesheet" href="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"> -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css">
</head>

<style type="text/css">
    #result_output {
        font-size: 16px;
        display: inline-block;
        margin-left: 10px;
    }
    
    #result_output .alert-success {
        padding: 6px 35px 6px 10px;
        margin-bottom: 0px;
    }
    /*pdf download button*/
    
    a#pdf_link {
        font-weight: 700;
        letter-spacing: 1px;
    }
    
    #divLoading {
        display: none;
    }
    
    #divLoading.show {
        display: block;
        position: fixed;
        z-index: 100;
        background-color: #fff;
        opacity: 0.7;
        background-repeat: no-repeat;
        background-position: center;
        background-size: 70px;
        left: 0;
        bottom: 0;
        right: 0;
        top: 0;
    }
    .showgif{
    	display: flex;
	    align-items: center;
	    justify-content: center;
	    height: 100%;
    }
    #loadinggif.show {
        left: 50%;
        top: 50%;
        position: absolute;
        z-index: 101;
        width: 32px;
        height: 32px;
        margin-left: -16px;
        margin-top: -16px;
    }
    
    div.content {
        width: 1000px;
        height: 1000px;
    }
    
    label.error {
        color: red;
        font-size: 13px;
		font-weight:normal;
    }
</style>

<body>

    <div class="wrapper" style="background: url('<?php echo base_url('assets/img/bg_1.jpg'); ?>') !important;">
        <header>
            <div class="container contspace">
                <div class="main2">
                    <div class="row">
                        <div class="col-sm-12 col-md-2 col-xl-2 ">
                            <div class="logo">
                                <img src="<?php echo base_url('assets/img/logo-new.png'); ?>" alt="img" />
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-10 col-xl-10">
                            <div class="text-head text-center">
                                <h3>SVT Junior College – <span><?php if($_GET['standard']==11){echo "XI Standard";}elseif($_GET['standard']==12){echo 'XII Standard';} ?>  Registration Form</span> </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <div class="form_grid">
            <div class="container">
                <div class="mainb">
                    <?php if($_GET['standard']==11 || $_GET['standard']==12){ ?>


                    <div class="alert alert-success alert-dismissible" id="alertt" style="display:none"> <span id="alertt1"></span>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div> 
                    <form id="registrationForm" method="post" enctype="multipart/form-data">
                        <input type="hidden" name="standard" value="<?php if($_GET['standard']==11){echo "XI";}else{echo 'XII';} ?>">
                        <div class="row maintop">
                            <div class="col-sm-12 col-md-12 col-xl-12">
                                <h5>General Details</h5>
                            </div>
                            <div class="col-sm-12 col-md-9 col-xl-9">
                                <div class="row choice_subject">
                                    <div class="col-sm-12 col-md-4 col-xl-4">
                                        <p>
                                            <label>Are you Eligible ? </label>
                                        </p>
                                    </div>

                                    <div class="col-sm-12 col-md-8 col-xl-8">
                                        <div class="form-check form-group custom-radio-bootrsap" style="display:block;">
                                            <div class="radio" style="">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="course_type" value="Eligible" checked /> Yes
                                                </label>
                                            </div>
                                            <div class="radio">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="course_type" value="Non eligible" style="font-weight: 700" /> No
                                                </label>
                                            </div>
                                            
                                            <p style="font-size: 15px;margin-bottom: 5px;margin-top: 15px;color: green;"><em><b>Note:</b></em> <br>
                                                <b>*Eligible Candidates - </b>Your (parents) are resident of Maharashtra State for last 15 years and you are not 4th or 5th daughter of your parents.<br><br>
                                                <b>*Non-Eligible Candidates - </b>Your (parents) are not resident of Maharashtra State for last 15 years or you 4th or 5th daughter of your parents.</p>
                                            <label id="course_name-error" class="error" for="course_name"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row choice_subject">
                                    <div class="col-sm-12 col-md-4 col-xl-4">
                                        <p>
                                            <label>Stream </label>
                                        </p>
                                    </div>

                                    <div class="col-sm-12 col-md-8 col-xl-8">
                                        <div class="form-check form-group custom-radio-bootrsap" style="display:block;">
                                            <div class="radio" style="">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="specialization" value="Home Science" /> Home Science 
                                                </label>
                                            </div>
                                            <div class="radio">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="specialization" value="Science" style="font-weight: 700" /> Pure Science 
                                                </label>
                                            </div>
                                            
                                            <label id="specialization-error" class="error" for="specialization"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row choice_subject">
                                    <div class="col-sm-12 col-md-4 col-xl-4">
                                        <p>
                                            <label>Choice of subject For Std. <?php if($_GET['standard']==11){echo "XI";}else{echo 'XII';} ?></label>
                                        </p>
                                    </div>

                                    <div class="col-sm-12 col-md-8 col-xl-8">
                                        <div class="form-check form-group custom-radio-bootrsap" style="display:block;">
                                            <div class="radio">
                                                <label class="form-check-label">
                                                    <?php if($_GET['standard']==12){ ?>
                                                    <input type="radio" class="form-check-input" name="choice_subject_XI" value="Chem/Bio/phy/Socio/psy" style="font-weight: 700" /> Chemistry / Biology / Physics / Sociology / Psychology<br/>
                                                    <input type="radio" class="form-check-input" name="choice_subject_XI" value="Chem/Bio/Socio/psy" style="font-weight: 700" /> Chemistry / Biology / Sociology / Psychology
                                                    <?php }else{ ?>
                                                    <input type="radio" class="form-check-input" name="choice_subject_XI" value="Chem/Bio/Socio/psy" style="font-weight: 700" /> Chemistry / Biology / Sociology / Psychology
                                                        
                                                    <?php } ?>
                                                </label>
                                            </div>
                                            <div class="radio" style="">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="choice_subject_XI" value="Chem/Bio/phy/Maths" /> Chemistry / Biology / Physics / Mathematics
                                                </label>
                                            </div>
                                            <label id="choice_subject_XI-error" class="error" for="choice_subject_XI"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px;">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <label>Language of choice </label>
                                    </div>

                                    <div class="col-sm-12 col-md-8 col-xl-8">
                                        <div class="form-check form-group custom-radio-bootrsap custom-radio-language" style="display:block;">
                                            <div class="radio">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="language_of_choice" value="Hindi" style="font-weight: 700" /> Hindi
                                                </label>
                                            </div>
                                            <div class="radio" style="">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="language_of_choice" value="Marathi" /> Marathi
                                                </label>
                                            </div>
                                            <label id="language_of_choice-error" class="error" for="language_of_choice"></label>
                                        </div>
                                    </div>

                                </div>

                                <div class="row" style="margin-top: 10px;">
                                    <div class="col-sm-12 col-md-4 col-xl-4">
                                        <p class="admission">
                                            <label>Category under which admission is sought</label>
                                        </p>
                                    </div>

                                    <div class="col-sm-12 col-md-8 col-xl-8">
                                        <div class="form-check form-group custom-radio-bootrsap custom-radio-category" style="display:block;">
                                            <div class="radio">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="category_sought" value="General" id="general" style="font-weight: 700" /> General
                                                </label>
                                            </div>
                                            <div class="radio" style="">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="category_sought" value="Reservation" id="other" /> Reservation
                                                </label>
                                            </div>
											<label id="category_sought-error" class="error" for="category_sought"></label>
											
                                        </div>

                                        <div class="" style="" id="general_cat">
                                            <div class="form-check-inline form-group">
                                                <label class="form-check-label" for="radio3">
                                                    <input type="checkbox" class="form-check-input" name="category_sought_general[]" value="Sports cultural"> Sports cultural
                                                </label>
                                            </div>
                                            <div class="form-check-inline form-group">
                                                <label class="form-check-label" for="radio4">
                                                    <input type="checkbox" class="form-check-input handi" name="category_sought_general[]" value="Handicapped" id="hg"> Handicapped
                                                </label>
                                            </div>
                                            <div class="form-check-inline form-group">
                                                <label class="form-check-label" for="radio5">
                                                    <input type="checkbox" class="form-check-input" name="category_sought_general[]" value="Govt.transfers/defence/freedom fighters"> Govt.transfers/defence/freedom fighters
                                                </label>
                                            </div>
                                        </div>

                                        <div class="" style="" id="other_cat">
                                            <div class="form-check-inline form-group caste">
                                                <label class="form-check-label" for="radio2">
                                                    <input type="checkbox" class="form-check-input" id="caste" name="category_sought_other[]" value="Caste"> Caste
                                                </label>
                                            </div>
                                            <div class="form-check-inline form-group">
                                                <label class="form-check-label" for="radio3">
                                                    <input type="checkbox" class="form-check-input" name="category_sought_other[]" value="Sports cultural"> Sports cultural
                                                </label>
                                            </div>
                                            <div class="form-check-inline form-group">
                                                <label class="form-check-label" for="radio4">
                                                    <input type="checkbox" class="form-check-input handicapped" id="ho" name="category_sought_other[]" value="Handicapped"> Handicapped
                                                </label>
                                            </div>
                                            <div class="form-check-inline form-group">
                                                <label class="form-check-label" for="radio5">
                                                    <input type="checkbox" class="form-check-input" name="category_sought_other[]" value="Govt.transfers/defence/freedom fighters"> Govt.transfers/defence/freedom fighters
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="disability" style="display: none; margin-bottom: 25px; margin-top: 10px;">

                                    <div class="col-md-12">

                                        <div class="col-md-5">
                                            <div class="form-group dt">
                                                <label>Disability Type</label>
                                                <select name="DisabilityType" id="DisabilityType" class="form-control">
                                                    <option value="">Please Select Disability Type</option>
                                                    <option value="Visually Impaired">Visually Impaired</option>
                                                    <option value="Speech and Hearing Impaired">Speech and/or Hearing Impaired</option>
                                                    <option value="Orthopedic Disorder or Mentally Retarded">Orthopedic Disorder or Mentally Retarded</option>
                                                    <option value="Learning Disability">Learning Disability</option>
                                                    <option value="Dyslexia">Dyslexia</option>
                                                </select>
                                                <small class="help-block" data-bv-validator="notEmpty" data-bv-for="DisabilityType" data-bv-result="INVALID" style="">Disability Type is required</small>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group dp">
                                                <label>Disability Percentage</label>
                                                <input type="text" placeholder="Disability Percentage" name="DisabilityPercentage" id="DisabilityPercentage" class=" DisabilityPercentage form-control">
                                                <small class="help-block" data-bv-validator="notEmpty" data-bv-for="DisabilityPercentage" data-bv-result="INVALID" style="">Disability Percentage is required</small>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group dn">
                                                <label>Disability Certificate Number</label>
                                                <input type="text" placeholder="Disability Certificate Number" name="DisabilityNumber" id="DisabilityNumber" class="form-control">
                                                <small class="help-block" data-bv-validator="notEmpty" data-bv-for="DisabilityNumber" data-bv-result="INVALID" style="">Disability Certificate Number is required</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-12 col-md-3 col-xl-3">
                                <div class="photo text-right">
                                    <label style="text-align: center;">
                                        <span id="pspan" aria-hidden="true">Uploaded Photograph will be display here</span>
                                        <img src="" id="photo" width="160" style="display: none;">
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div class="row photo_notice">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="control-label">Photograph</label>
                                    <div class="">
                                        <input style="border: none !important;" type="file" class="form-control" name="photo" id="upload" size="50" accept="image/jpg,image/png,image/jpeg">
                                    </div>
                                </div>
                                <div id="photo_error" class="error" style="display: none;margin-bottom: 15px;color: red;font-size: 16px;"><span id="photo_error_msg"></span></div>
                                <p><em><b>Note:</b></em> Please upload your Recent passport size photograph. Photo should be of size 15 cm * 15 cm. Make sure your photograph size does not exceed 4KB - 50KB.</p>
                                <h6>Photograph should be recent & good quality, it will be used for all official purpose.</h6>
                            </div>
                        </div>
                        <div class="row photo_notice">
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label class="control-label">Signature Upload</label>
                                    <div class="">
                                        <input style="border: none !important;" type="file" class="form-control" name="signature_photo" id="signature_upload" size="50" accept="image/jpg,image/png,image/jpeg">
                                    </div>
                                    <label id="signature_upload-error" class="error" for="signature_photo"></label>
                                </div>
                                <div id="signature_error" class="error" style="display: none;margin-bottom: 15px;color: red;font-size: 16px;"><span id="signature_error_msg"></span></div>
                                <h6>Please upload your signature. Signature image should be of size 15 cm * 15 cm. Make sure your signature image size does not exceed 50KB.</h6>
                            </div>
                            <div class="col-md-3 sign">
                                <div class="photo signature text-right">
                                    <label style="text-align: center;">
                                        <span id="sspan" aria-hidden="true">Uploaded Signature will be display here</span>
                                        <img src="" id="signature_photo" width="160" style="display: none;">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row photo_notice">
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label class="control-label">Parent Signature Upload</label>
                                    <div class="">
                                        <input style="border: none !important;" type="file" class="form-control" name="parent_signature" id="parent_signature" size="50" accept="image/jpg,image/png,image/jpeg">
                                    </div>
                                    <label id="parent_signature_upload-error" class="error" for="parent_signature"></label>
                                </div>
                                <div id="parent_signature_error" class="error" style="display: none;margin-bottom: 15px;color: red;font-size: 16px;"><span id="parent_signature_error_msg"></span></div>
                                <h6>Please upload parent's signature. Signature image should be of size 15 cm * 15 cm. Make sure parent's signature image size does not exceed 50KB.</h6>
                            </div>
                            <div class="col-md-3 sign">
                                <div class="photo signature text-right">
                                    <label style="text-align: center;">
                                        <span id="psspan" aria-hidden="true">Uploaded Parent Signature will be display here</span>
                                        <img src="" id="parent_signature_photo" width="160" style="display: none;">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <h5>PERSONAL DETAILS</h5>
                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>First Name</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="First Name" name="first_name" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Last Name</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Last Name" name="last_name" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix ">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Father Name</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Father Name" name="father_name" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Mother Name</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Mother Name" name="mother_name">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Mother Tongue</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Mother Tongue" name="mother_tongue" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Aadhar Card No.</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Aadhar Card No." maxlength="12" minlength="12" name="aadhar_no" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Blood Group</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Blood Group" name="blood_group">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Married Status</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <select class="form-control" name="married_status">
                                                <option value="" selected>Select</option>
                                                <option value="Married">Married</option>
                                                <option value="Unmarried">Unmarried</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Date of Birth</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" placeholder="Date of Birth" class="form-control datepicker" name="date_of_birth" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Place of Birth</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" placeholder="Place of Birth" class="form-control" name="place_of_birth" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix" id="castes">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <!-- <p class="admission"> -->
                                        <label>Caste</label>
                                        <!-- </p> -->
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group uc">
                                            <select class="form-control" name="under_caste" id="under_caste">
                                                <option value="" selected>Select</option>
                                                <option value="SC">SC</option>
                                                <option value="ST">ST</option>
                                                <option value="VJ">VJ</option>
                                                <option value="DT">DT</option>
                                                <option value="NT1">NT1</option>
                                                <option value="NT2">NT2</option>
                                                <option value="NT3">NT3</option>
                                                <option value="OBC">OBC</option>
                                                <option value="SBC">SBC</option>
                                            </select>
                                            <small class="help-block" data-bv-validator="notEmpty" data-bv-for="under_caste" data-bv-result="INVALID" style="">Caste is required</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Sub caste</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" placeholder="Sub caste" class="form-control" name="sub_caste" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">

                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Religion</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <select class="form-control" name="religion">
                                                <option value="" selected>Select</option>
                                                <option value="Buddhist">Buddhist </option>
                                                <option value="Christian">Christian </option>
                                                <option value="Hindu">Hindu</option>
                                                <option value="Jain">Jain </option>
                                                <option value="Muslim">Muslim </option>
                                                <option value="Parsi">Parsi </option>
                                                <option value="Sikh">Sikh </option>
                                                <option value="Other">Other </option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- <div class="col-sm-12 col-md-6 col-lg-6" id="castes">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <label>Caste</label>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group uc">
                                            <select class="form-control" name="under_caste" id="under_caste">
                                                <option value="" selected>Select</option>
                                                <option value="SC">SC</option>
                                                <option value="ST">ST</option>
                                                <option value="VJ">VJ</option>
                                                <option value="DT">DT</option>
                                                <option value="NT1">NT1</option>
                                                <option value="NT2">NT2</option>
                                                <option value="NT3">NT3</option>
                                                <option value="OBC">OBC</option>
                                                <option value="SBC">SBC</option>
                                            </select>
                                            <small class="help-block" data-bv-validator="notEmpty" data-bv-for="under_caste" data-bv-result="INVALID" style="">Caste is required</small>
                                        </div>
                                    </div>
                                </div>
                            </div> -->
                        </div>

                        <h5>Contact Details</h5>
                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Address</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <textarea class="form-control" rows="3" placeholder="Address" id="comment" name="address"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Address (Native)</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <textarea class="form-control" rows="3" placeholder="Address (Native)" id="commentnative" name="address_native"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Student's Email</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="email" class="form-control" placeholder="Student's Email" name="student_email" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Student's Mobile No.</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="tel" class="form-control" placeholder="Student's Mobile No." maxlength="10" minlength="10" name="student_mobile" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h5>Bank Details</h5>
                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Bank A/c NO.</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Bank A/c NO." name="bank_acc_no" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>IFSC code</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="IFSC code" name="ifsc_code" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h5>Guardian Details</h5>
                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Name of father/guardian</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Name of father/guardian" name="guardian_name" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Address of Guardian</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <textarea class="form-control" rows="3" placeholder="Address of Guardian" name="guardian_address"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Email Address of Guardian</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="email" class="form-control" placeholder="Email of Guardian" name="guardian_email" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Mobile Number of Guardian</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="tel" class="form-control" placeholder="Mobile Number of Guardian" maxlength="10" minlength="10" name="guardian_mobile" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Guardian’s Profession</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Guardian’s Profession" name="guardian_profession" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Annual Income</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Annual Income" name="annual_income" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Applicant's relationship to guardian</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Applicant's relationship to guardian" name="relationship_to_guardian" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                            </div>
                        </div>

                        <h5>S.S.C. Details</h5>
                        <div class="row">
                            <div class="col-sm-12 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <label>Total Marks obtained</label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-4 col-lg-4">
                                <div class="form-group">
                                    <input type="text" class="form-control" id="obtained" placeholder="Total Marks obtained" name="total_marks_obtained" value="">
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <label>Out of</label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-4 col-lg-4">
                                <div class="form-group">
                                    <input type="text" class="form-control" id="outof" placeholder="out of" name="out_of" value="">
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Percentage in SSC (correct to two decimal places)</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control number" id="ssc" placeholder="Percentage in SSC" name="percentage_in_ssc" value="" readonly>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Name of examination</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Name of examination" name="name_of_examination" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Year of Passing</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <select class="form-control" id="year_of_passing" name="year_of_passing">
                                                <option value="" selected>Select</option>
                                                <option value="2020">2020</option>
                                                <option value="2019">2019</option>
                                                <option value="2018">2018</option>
                                                <option value="2017">2017 </option>
                                                <option value="2016">2016 </option>
                                                <option value="2015">2015 </option>
                                                <option value="2014">2014 </option>
                                                <option value="2013">2013 </option>
                                                <option value="2012">2012 </option>
                                                <option value="2011">2011 </option>
                                                <option value="2010">2010 </option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label>Name of Board </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Name of Board" name="name_of_board" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Name of School</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Name of School" name="name_of_school" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4 ">
                                        <div class="form-group">
                                            <label> Address of School </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <textarea class="form-control" rows="3" placeholder="Address of School" name="address_of_school"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row clearfix">
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Exam seat No.</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-8 col-lg-8">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Exam seat No." name="exam_seat_no" value="">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6 col-lg-6"> </div>
                        </div>

                        <h5>Miscellaneous </h5>
                        <div class="row">
                            <div class="col-sm-12 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <label>Extra curricular achivements</label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-10 col-lg-10">
                                <div class="form-group">
                                    <textarea class="form-control" rows="3" name="extra_curricular_achivements" placeholder="Extra curricular achivements" name="address_of_school"></textarea>
                                </div>
                            </div>
                        </div>

                        <div class="form-check form-group">
                            <label class="form-check-label" style="display:block;">
                                <input type="checkbox" class="form-check-input" name="terms_conditions" value="agree"> I have read all information entered in this registration form. Information provided is correct as per my knowledge.
                            </label>
							<label id="terms_conditions-error" class="error" for="terms_conditions"></label>
                        </div>

                        <div class="form-group">
                            <input type="submit" class="btn btn-success" id="submit_button" name="submit" value="Register">
                            <!-- result output div after success -->
                            <div id="result_output" style="display: none;">
                                <!-- success message after ajax call -->
                                <div class="alert alert-success alert-dismissible" role="alert">
                                    <span id="result_output_span"></span>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                            </div>
                            <!-- error message validate all fields -->
                            <div class="alert alert-danger alert-dismissible alert-form" role="alert" style="display: none !important;">
                                <strong>Error!</strong> Please fill in information required in the form.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>

                        <!-- pdf download button on success -->
                        <div class="google_form_link" style="display: none; margin-top: 10px !important;">
                            <span class="form-check-label" style="font-size: 15px;"> Please upload all your documents in this <a class="" href="<?php if($_GET['standard']==11){echo "https://forms.gle/UXY8udir3bFhoVjv6";}else{echo "https://forms.gle/8PcD9ywzzyk9eCJY7";} ?>" target="_blank">Google Form </a>. Make sure you use same mobile number which you have used in admission form submission.
                            </span>
                        </div>
                        <!-- pdf download button on success -->
                        <div class="pdf_download_link" style="display: none; margin-top: 10px !important;">
                            <a class="btn btn-success btn-sm" href="" id="pdf_link" target="_blank">Click here to download your Admission Form.</a>
                            <a href="" id="pdf_dlink" download></a>
                        </div>

                    </form>
                <?php } else { ?>
                   <div style="height: 600px">Incorrect Standard, Please use correct link to register....</div> <!-- echo "Incorrect Standard, Please use correct link to register...."; -->
                  <?php } ?>
                </div>
            </div>
        </div>
    </div>

    <div id='divLoading'><div class="showgif"><span>Your Form data is uploading, please wait...</span><img src="<?php echo base_url('assets/img/loader.gif'); ?>"></div></div>

</body>

<script src="https://cdn.jsdelivr.net/jquery/1.11.1/jquery.min.js"></script>
<script src="https://cdn.jsdelivr.net/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<!-- BootstrapValidator JS -->
<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" integrity="sha256-sPB0F50YUDK0otDnsfNHawYmA5M0pjjUf4TvRJkGFrI=" crossorigin="anonymous"></script>
<script>
    jQuery.validator.addMethod("letters", function(value, element) {
        return this.optional(element) || value == value.match(/^[a-zA-Z\s]*$/);
    });
    jQuery.validator.addMethod("addressExp", function(value, element) {
        return this.optional(element) || value == value.match(/^[a-zA-Z0-9.,:\-\)\(\[\]\}\{\n ]+$/);
    });
    jQuery.validator.addMethod("alphanumeric", function(value, element) {
        return this.optional(element) || value == value.match(/^[a-zA-Z0-9]+$/);
    });
    jQuery.validator.addMethod("bloodgroup", function(value, element) {
        return this.optional(element) || value == value.match(/^[a-zA-Z+-]+$/);
    });
    jQuery.validator.addMethod("alphanumericWithSpace", function(value, element) {
        return this.optional(element) || value == value.match(/^[a-zA-Z0-9-. ]+$/);
    });

    jQuery(document).ready(function() {

        jQuery(".uc .help-block").hide();
        // datepicker
        var a = new Date(new Date().setFullYear(new Date().getFullYear() - 15));
        //alert(currentYear);

         jQuery( ".datepicker" ).datepicker({
             changeMonth: true,
             changeYear: true,
             yearRange: "1995:2020"
         });

        // jQuery(".datepicker").datepicker();
        // jQuery(".datepicker").datepicker("option", "showAnim", 'clip');
        // jQuery(".datepicker").datepicker("option", "dateFormat", 'yy-mm-dd');
        // jQuery(".datepicker").datepicker("option", "defaultDate", a);
    
     // caste hide/show
     jQuery('#caste').on('click init-cast-format', function() {
       jQuery('#castes').toggle(jQuery('#caste').prop('checked'));
     }).trigger('init-cast-format');

     jQuery('input[name=category_sought]').on('click init-cast-format', function() {
       jQuery('#general_cat').toggle(jQuery('#general').prop('checked'));
     }).trigger('init-cast-format');

     jQuery('input[name=category_sought]').on('click init-cast-format', function() {
       jQuery('#other_cat').toggle(jQuery('#other').prop('checked'));
     }).trigger('init-cast-format');

     jQuery('.handi').on('click init-cast-format', function() {
       jQuery('#disability').toggle(jQuery('.handi').prop('checked'));
     }).trigger('init-cast-format');

     jQuery('.handicapped').on('click init-cast-format', function() {
       jQuery('#disability').toggle(jQuery('.handicapped').prop('checked'));
     }).trigger('init-cast-format');
     
     // photo file display
    function readURL(input) {
      if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function(e) {
          jQuery("#pspan").hide();
          jQuery('#photo').attr('src', e.target.result);
          jQuery("#photo").show();
        }
        reader.readAsDataURL(input.files[0]);
      }
    }
    jQuery("#upload").change(function() {
      var ext = $('#upload').val().split('.').pop().toLowerCase();
		if($.inArray(ext, ['png','jpg','jpeg']) == -1) {
			$('#upload').val(null);
		    alert('Please upload your photo with jpeg,jpg,png file extension only...');
		    return false;
		}else{
      		readURL(this);
  		}
    });

    // photo file display
    function signature(input) {
      if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function(e) {
          jQuery("#sspan").hide();
          jQuery('#signature_photo').attr('src', e.target.result);
          jQuery("#signature_photo").show();
        }
        reader.readAsDataURL(input.files[0]);
      }
    }
    jQuery("#signature_upload").change(function() {
      var ext = $('#signature_upload').val().split('.').pop().toLowerCase();
		if($.inArray(ext, ['png','jpg','jpeg']) == -1) {
			$('#signature_upload').val(null);
		    alert('Please upload your signature with jpeg,jpg,png file extension only...');
		    return false;
		}else{
      		signature(this);
  		}
    });

    // parentsignature file display
    function parentsignature(input) {
      if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function(e) {
          jQuery("#psspan").hide();
          jQuery('#parent_signature_photo').attr('src', e.target.result);
          jQuery("#parent_signature_photo").show();
        }
        reader.readAsDataURL(input.files[0]);
      }
    }
    jQuery("#parent_signature").change(function() {
    	var ext = $('#parent_signature').val().split('.').pop().toLowerCase();
		if($.inArray(ext, ['png','jpg','jpeg']) == -1) {
			$('#parent_signature').val(null);
		    alert('Please upload parent signature with jpeg,jpg,png file extension only...');
		    return false;
		}else{
      		parentsignature(this);
  		}
    });

    // ssc percentage 
        jQuery(function() {
          jQuery("#obtained, #outof").change(function() { // input on change
            var obtained = parseInt(jQuery("#obtained").val());
            var outof = parseInt(jQuery("#outof").val());
            if(obtained && outof){
              if(obtained < outof){
              var result = parseFloat(parseInt(jQuery("#obtained").val(), 10) * 100)/ parseInt(jQuery("#outof").val(), 10);
                if(jQuery("#obtained").val() && jQuery("#outof").val()){
                  jQuery('#ssc').val(result.toFixed(2)||''); //shows value
                }
                else{
                  jQuery('#ssc').val('');
                }
            }else {
               alert('Out of marks should be greater than Obtained marks');
              jQuery("#outof").val('');
              console.log(jQuery("#outof").parent());
              jQuery("#outof").parent().removeClass("has-success");
            }
            } 
          })
        });


        jQuery("#registrationForm").validate({
            rules: {
                choice_subject_XI: {
                    required: true
                },
                specialization: {
                    required: true
                },
                language_of_choice: {
                    required: true
                },
                category_sought: {
                    required: true
                },
                photo: {
                    required: true
                },
                signature_photo: {
                    required: true
                },
                first_name: {
                    required: true,
                    letters: true
                },
                last_name: {
                    required: true,
                    letters: true
                },
                father_name: {
                    required: true,
                    letters: true
                },
                mother_name: {
                    required: true,
                    letters: true
                },
                religion: {
                    required: true
                },
                mother_tongue: {
                    required: true,
                    letters: true
                },
                aadhar_no: {
                    required: true,
                    minlength: 12,
                    maxlength: 12,
                    digits: true
                },
                blood_group: {
                    required: true,
                    bloodgroup: true
                },
                married_status: {
                    required: true
                },
                date_of_birth: {
                    required: true
                },
                place_of_birth: {
                    required: true,
                    letters: true
                },
                address: {
                    required: true,
                    addressExp: true
                },
                address_native: {
                    required: true,
                    addressExp: true
                },
                student_email: {
                    required: true,
                    email: true
                },
                student_mobile: {
                    required: true,
                    digits: true,
                    minlength:10,
                    maxlength:10
                },
                bank_acc_no: {
                    required: true,
                    digits: true
                },
                ifsc_code: {
                    required: true,
                    alphanumeric: true
                },
                guardian_name: {
                    required: true,
                    letters: true
                },
                guardian_address: {
                    required: true,
                    addressExp: true
                },
                guardian_email: {
                    required: true,
                    email: true
                },
                guardian_mobile: {
                    required: true,
                    digits: true,
                    minlength:10,
                    maxlength:10
                },
                guardian_profession: {
                    required: true
                },
                annual_income: {
                    required: true,
                    minlength:1,
                    digits: true
                },
                relationship_to_guardian: {
                    required: true,
                    letters: true
                },
                total_marks_obtained: {
                    required: true,
                    digits: true
                },
                out_of: {
                    required: true,
                    digits: true
                },
                name_of_examination: {
                    required: true,
                    alphanumericWithSpace: true
                },
                year_of_passing: {
                    required: true
                },
                name_of_board: {
                    required: true,
                    alphanumericWithSpace: true
                },
                name_of_school: {
                    required: true,
                    alphanumericWithSpace: true
                },
                address_of_school: {
                    required: true,
                    addressExp: true
                },
                exam_seat_no: {
                    required: true,
                    alphanumericWithSpace: true
                },
                DisabilityPercentage: {
                    required: true,
                    digits: true,
					minlength:1,
                    maxlength:2
                },
                terms_conditions: {
                    required: true
                }
            },
            messages: {
                choice_subject_XI: "Choice of subject is required",
                specialization: "Stream is required",
                language_of_choice: "Language of choice is required",
                category_sought: "Category is required",
                photo: {
                    required: "Photograph is required",
                    accept: "Only image type jpg/png/jpeg is allowed"
                },
                signature_photo: {
                    required: "Photograph is required",
                    accept: "Only image type jpg/png/jpeg is allowed"
                },
                first_name: {
                    required: "The First name is required and cannot be empty",
                    letters: "The First name can only consist of alphabetical"
                },
                last_name: {
                    required: "The First name is required and cannot be empty",
                    letters: "The First name can only consist of alphabetical"
                },
                father_name: {
                    required: "The Father name is required and cannot be empty",
                    letters: "The Father name can only consist of alphabetical."
                },
                mother_name: {
                    required: "The Mother name is required and cannot be empty",
                    letters: "The Mother name can only consist of alphabetical."
                },
                religion: {
                    required: "The Religion name is required and cannot be empty"
                },
                mother_tongue: {
                    required: "The Mother Tongue is required and cannot be empty",
                    letters: "The Mother Tongue can only consist of alphabetical."
                },
                aadhar_no: {
                    required: "The Adhaar number is required and cannot be empty",
                    minlength: "The Aadhar Number must be 12 characters long.",
					maxlength: "The Aadhar Number must be 12 characters long."
                },
                blood_group: {
                    required: "The Blood Group is required and cannot be empty",
                    bloodgroup: "The Blood Group can only consist of alphabetical"
                },
                married_status: {
                    required: "The Married Status is required and cannot be empty"
                },
                date_of_birth: {
                    required: "The date of birth is required and cannot be empty"
                },
                place_of_birth: {
                    required: "The Place of birth is required and cannot be empty",
                    letters: "The Place of birth can only consist of alphabetical"
                },
                address: {
                    required: "The Address is required and cannot be empty",
                    addressExp: "The Address can only consist of alphabetical & number"
                },
                address_native: {
                    required: "The Address is required and cannot be empty",
                    addressExp: "The Address can only consist of alphabetical & number"
                },
                student_email: {
                    required: "The student email address is required and cannot be empty",
                    email: "The student email address is not a valid"
                },
                student_mobile: {
                    required: "The student mobile number is required and cannot be empty",
                    minlength: "The student mobile must be 10 characters long",
                    maxlength: "The student mobile must be 10 characters long",
                    digits: 'The student mobile can only consist numbers'
                },
                bank_acc_no: {
                    required: "The Bank account number is required and cannot be empty",
                    digits: "The Bank account number can only consist numbers"
                },
                ifsc_code: {
                    required: "The Bank IFSC number is required and cannot be empty",
                    alphanumeric: "The IFSC number can only consist alphabetical & number"
                },
                guardian_name: {
                    required: "The Guardian name is required and cannot be empty",
                    letters: "The Guardian name can only consist of alphabetical."
                },
                guardian_address: {
                    required: "The guardian Address is required and cannot be empty",
                    addressExp: "The guardian Address can only consist of alphabetical & number"
                },
                guardian_email: {
                    required: "The guardain email address is required and cannot be empty",
                    email: "The guardain email address is not a valid"
                },
                guardian_mobile: {
                    required: "The guardain mobile number is required and cannot be empty",
                    minlength: "The guardain mobile must be 10 characters long",
                    maxlength: "The guardain mobile must be 10 characters long",
                    digits: 'The guardain mobile can only consist numbers'
                },
                guardian_profession: {
                    required: "The Guardian Profession is required and cannot be empty"
                },
                annual_income: {
                    required: "The Annual Income is required and cannot be empty",
                    minlength: "The Annual Income must be grater than 0",
                    digits: 'The Annual Income can only consist numbers'
                },
                relationship_to_guardian: {
                    required: "The Relationship to guardain is required and cannot be empty",
                    letters: "The Relationship to guardain can only consist of alphabetical."
                },
                total_marks_obtained: {
                    required: "The Total marks is required and cannot be empty",
                    digits: "The Total marks can only consist numbers"
                },
                out_of: {
                    required: "The Out of marks is required and cannot be empty",
                    digits: "The Out of marks can only consist numbers"
                },
                name_of_examination: {
                    required: "The Name of examination is required and cannot be empty",
                    alphanumericWithSpace: "The Name of examination can only consist of alphabetical."
                },
                year_of_passing: {
                    required: "Year of Passing is required and cannot be empty"
                },
                name_of_board: {
                    required: "The Board of school is required and cannot be empty",
                    alphanumericWithSpace: "The Board of school can only consist alphabetical & number"
                },
                name_of_school: {
                    required: "The Name of school is required and cannot be empty",
                    alphanumericWithSpace: "The Name of school can only consist alphabetical & number"
                },
                address_of_school: {
                    required: "The school address is required and cannot be empty",
                    addressExp: "The school address can only consist of alphabetical & number"
                },
                exam_seat_no: {
                    required: "The Exam seat number is required and cannot be empty",
                    alphanumericWithSpace: "The Exam seat number can only consist of alphabetical & number"
                },
                terms_conditions: {
                    required: "You must accept this before submitting the form"
                }

            },
       submitHandler: function (form) {
         var formData = new FormData(form);
         $('#divLoading').addClass('show'); 
         $('#divLoading').show(); 
         $.ajax({
           type: "POST",
           url: "<?php echo base_url('generate-pdf'); ?>",
           data: formData,
           contentType: false,
           cache: false,
           processData: false,
           success: function (data) {
             //alert(data);
                var obj = jQuery.parseJSON(data);
                $('#divLoading').removeClass('show'); 
                $('#divLoading').hide(); 
                console.log(obj);
                if(obj.status=='success'){
                    var message = obj.message ;
                    jQuery("#registrationForm").trigger("reset");
        			jQuery("#result_output_span").text(message);
                    jQuery("#alertt1").text(message);
    			    jQuery("#alertt").show();
                    jQuery(".google_form_link").show();
                    jQuery(".pdf_download_link").show();
                    jQuery("#result_output").show();
                    jQuery("#pdf_link").attr('href', obj.pdf_download);
                    jQuery("#pdf_dlink").attr('href', obj.pdf_download);
                    jQuery('#photo').attr('src', '');
                    jQuery('#signature_photo').attr('src', '');
                    jQuery('#parent_signature').attr('src', '');
                    jQuery('#photo').hide();
                    jQuery('#signature_photo').hide();
                    jQuery('#parent_signature').hide();
                    jQuery("#pdf_dlink").click();
                }else if(obj.status=='error'){
                    if (obj.error=='photo') {
                       // alert(obj.error);
                        jQuery("#photo_error").show();
                        jQuery("#photo_error_msg").text(obj.message);
                    }
                    if (obj.error=='signature') {
                        //alert(obj.error);
                        jQuery("#signature_error").show();
                        jQuery("#signature_error_msg").text(obj.message);
                        /* $('html, body').animate({
                            scrollTop: $('#signature_error').offset().top + 20
                          }, 200);*/
                        
                    } 
                    if (obj.error=='parent_signature') {
                       // alert(obj.error);
                        jQuery("#parent_signature_error").show();
                        jQuery("#parent_signature_error_msg").text(obj.message);
                    }
                    $(window).scrollTop($('#photo_error').offset().top-20);

                }
            }
         });
         return false; // required to block normal submit since you used ajax
       }

        });
        // end if handicapped id checked      
        // end if  caste is checked
    });
</script>

</html>