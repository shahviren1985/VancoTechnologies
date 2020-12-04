<!DOCTYPE html>
<html lang="en">
   <head>
      <meta charset="utf-8">
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
      <meta name="description" content="">
      <meta name="author" content="">
      <title>APPLICATION FORM FOR HOSTEL ACCOMMODATION</title>
      <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/bootstrap/3.2.0/css/bootstrap.min.css">
      <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/fontawesome/4.1.0/css/font-awesome.min.css">
      <!-- BootstrapValidator CSS -->
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css" />
      <!-- custom css -->
      <!-- <link rel="stylesheet" type="text/css" href="css/style.css">
         <link rel="stylesheet" type="text/css" href="css/responsive.css"> -->
      <!-- <link rel="stylesheet" type="text/css" href="<?php echo base_url('assets/css/registration-form/style.css" rel="stylesheet'); ?>">
         <link rel="stylesheet" type="text/css" href="<?php echo base_url('assets/css/registration-form/responsive.css" rel="stylesheet'); ?>"> -->
      <link href="https://fonts.googleapis.com/css?family=Roboto:400,400i,500,500i,700,700i,900" rel="stylesheet">
      <!--<link rel="stylesheet" href="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"> -->
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css">
   </head>
   <style type="text/css">
   .form-group {
    margin-bottom: 35px !important;
}
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
      .form_grid {
      margin: 0px 150px;
      background-color: #fff;
      }
      .logo {
      padding-top: 17px;
      }
      form#registrationForm {
      padding: 50px 0px;
      }
      .photo {
    width: 160px;
    height: 170px;
    border: 1px solid #5c5c5c;
    display: -webkit-box;
    display: -moz-box;
    display: -ms-flexbox;
    display: -webkit-flex;
    justify-content: center;
    -webkit-justify-content: center;
    -webkit-box-pack: center;
    -webkit-box-align: center;
    align-items: center;
    margin: 0 auto;}
    .sign .signature {
    width: 160px;
    height: 95px;
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
                           <img src="<?php echo base_url('assets/img/vedanta_logo.png'); ?>" alt="img" />
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-10 col-xl-10">
                        <div class="text-head text-center">
                           <h3 style="font-size: 28px;color: #fff;line-height:30px;padding: 40px 0;">APPLICATION FORM FOR HOSTEL ACCOMMODATION</h3>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </header>
         <div class="form_grid">
            <div class="container">
               <form id="registrationForm" method="post" enctype="multipart/form-data">
                  <div class="row ">
                     <u>
                        <h4 style="text-align:center;padding-bottom:35px ">NEW /RENEWAL</h4>
                     </u>
                  </div>
                  <div class="row clearfix">
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Academic Year</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder="Academic Year" name="academic_year" value="2020-2021" disabled>
                              </div>
                           </div>
                        </div>
                        <p><em><b>Note:</b></em> Please upload your Recent passport size photograph. Photo should be of size 15 cm * 15 cm. Make sure your photograph size does not exceed 4KB - 50KB.</p>
                        <h6>Photograph should be recent & good quality, it will be used for all official purpose.</h6>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                       <div class="photo text-right">
                       <label style="text-align: center;">
                       <span id="pspan" aria-hidden="true">Uploaded Photograph will be display here</span>
                       <img src="" id="photo" width="160" style="display: none;">
                       </label>
                       </div>
                        <!-- <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4">
                               <div class="form-group">
                                   <label>Form No:</label>
                               </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                               <div class="form-group">
                                     <input type="text" class="form-control" placeholder="Form Number" name="form_no" value="">
                               </div>
                           </div>
                           </div> -->
                     </div>
                  </div>
                  <div class="row clearfix">
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Type of hostel applied</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder="Type of hostel applied" name="hostel_type" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4">
                           </div>
                           <div class="col-sm-12 col-md-5 col-lg-5">
                              <div class="form-group">
                                 <input style="border: none !important;" type="file" class="form-control" name="photo" id="upload" size="50" accept="image/jpg,image/png,image/jpeg">
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <div class="row clearfix">
                     <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="row">
                           <p style="font-size: 15px;margin-bottom: 5px;margin-top: 15px;color: green;"><em><b>Note:</b></em> <br>
                           <ol type="1">
                              <li>The accommodation inhostel is limited and will be allotted subject to availability and according to the appropriate criteria decided by the college authority. This application thus does not guarantee Hostel Accommodation.</li>
                              <li>In case you have failed or not appeared in the Examination during the last academic year, then you are not eligible for hostel accommodation during upcoming academic year. </li>
                              <li>The students who have not paid hostel charges of previous academic year in-time and/or the students who have failed to comply with the rules &regulations of hostel during last academic year, may not be given hostel accommodation in this academic year. </li>
                              <li>Please write in CAPITAL LETTERS only & tick √ wherever applicable.</li>
                           </ol>
                           </p>
                        </div>
                     </div>
                  </div>

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
                                 <input type="text" class="form-control" placeholder=" First Name of the candidate" name="first_name" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Last Name</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder=" Last Name of the candidate" name="last_name" value="">
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
                                <input  type="text" id="txtBirthDate" readonly="readonly" class="form-control input-group-lg validate-input" placeholder="Date of Birth" name="date_of_birth" value="">
                             </div>
                          </div>
                       </div>
                    </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Age</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                <input name="CalculatedAge" type="text" id="CalculatedAge" placeholder="Calculated Age" class="form-control input-group-lg" maxlength="4" name="age" readonly
                                                          onblur="return ExtractNumber(this, 0, false);" onkeypress="return BlockNonNumbers(this, event, true, false);">
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
                                 <label>Gender</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <div class="radio" style="">
                                    <label class="form-check-label">
                                    <input type="radio" class="form-check-input" name="gender" value="Male" checked /> Male
                                    </label>
                                 </div>
                                 <div class="radio">
                                    <label class="form-check-label">
                                    <input type="radio" class="form-check-input" name="gender" value="Female" style="font-weight: 700" /> Female
                                    </label>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Blood Group</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" name="blood_group" placeholder="Blood Group">
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
                                 <label>Course</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <select name="Course" id="Course" class="form-control">
                                    <option value="">Select Specialisation</option>
                                    <option value="DC">Developmental Counseling</option>
                                    <option value="ECCE">Early Childhood Care and Education</option>
                                    <option value="FND">Food, Nutrition and Dietitics</option>
                                    <option value="HTM">Hospitality & Tourism Management</option>
                                    <option value="IDRM">Interior Designing and Resource Management</option>
                                    <option value="MCE">Mass Communication & Extension</option>
                                    <option value="TAD">Textile & Apparel Designing</option>
                                 </select>
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Year of Study</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" name="study_year" placeholder="Year of Study">
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
                                 <label>Date of Admission</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="tel" class="form-control datepicker" placeholder="Date of Admission" maxlength="10" minlength="10" name="date_of_admission" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>G.R Number</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" name="gr_num" placeholder="G.R Number">
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
                                 <label>Contact No. of the Candidate</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="tel" class="form-control" placeholder="Contact No. of the Candidate" maxlength="10" minlength="10" name="contact_number" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>E-mail</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="email" class="form-control" name="email" placeholder="E-mail">
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
                                <label>Married Status</label>
                             </div>
                          </div>
                          <div class="col-sm-12 col-md-8 col-lg-8">
                             <div class="form-group">
                                <div class="radio" style="">
                                   <label class="form-check-label">
                                   <input type="radio" class="form-check-input" name="married_status" value="Single" checked /> Single
                                   </label>
                                </div>
                                <div class="radio">
                                   <label class="form-check-label">
                                   <input type="radio" class="form-check-input" name="married_status" value="Married" style="font-weight: 700" /> Married
                                   </label>
                                </div>
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
                                 <label>Permanent Address (Home)</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <textarea rows="3"  class="form-control" placeholder="Permanent Address (Home)" name="permanent_address" value=""></textarea>
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Name of Parent/Guardian</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder="Name of Parent/Guardian" name="father_name" value="">
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
                                 <label>Occupation</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder="Occupation" name="occupation" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Address of Business/Service</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <textarea class="form-control" rows="3" placeholder="Address of Business/Service" name="service_address"></textarea>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <h5>Tel. No</h5>
                  <div class="row clearfix">
                     <div class="col-sm-12 col-md-4 col-lg-4">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>(Res.)</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="tel" class="form-control" placeholder="(Res.)" maxlength="10" minlength="10" name="resnum" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-4 col-lg-4">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>(off.)</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="tel" class="form-control" placeholder="(Off.)" maxlength="10" minlength="10" name="offnum" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>(Mob.)</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="tel" class="form-control" placeholder="(Mob.)" maxlength="10" minlength="10" name="mobnum" value="">
                              </div>
                           </div>
                        </div>
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


                  <h2>Guardian Details</h2>
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
                                 <label>Fax of Guardian</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder="Fax of Guardian" name="guardian_fax" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <h3>Correspondence</h3>
                  <div class="row clearfix">
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Address for Correspondence</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <textarea class="form-control" rows="3" placeholder="Address for Correspondence" name="correspondence_address"></textarea>
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Fax for Correspondence</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="text" class="form-control" placeholder="Fax for Correspondence" name="correspondence_fax" value="">
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
                                 <label>Email Address for Correspondence</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="email" class="form-control" placeholder="Email for Correspondence" name="correspondence_email" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-12 col-md-6 col-lg-6">
                        <div class="row">
                           <div class="col-sm-12 col-md-4 col-lg-4">
                              <div class="form-group">
                                 <label>Mobile Number for Correspondence</label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <input type="tel" class="form-control" placeholder="Mobile Number for Correspondence" maxlength="10" minlength="10" name="correspondence_mobile" value="">
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>

                        <div class="row clearfix">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Whether the candidate has stayed in hostel before </label>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <div class="radio" style="">
                                    <label class="form-check-label">
                                    <input type="radio" class="form-check-input" name="hostel_stay" value="Yes"  /> Yes
                                    </label>
                                 </div>
                                 <div class="radio">
                                    <label class="form-check-label">
                                    <input type="radio" class="form-check-input" name="hostel_stay" value="No" checked style="font-weight: 700" /> No
                                    </label>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <div class="row clearfix">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>Whether the candidate has any medical history of ailments <label>
                              </div>

                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group">
                                 <div class="radio" style="">
                                    <label class="form-check-label">
                                    <input type="radio" class="form-check-input" name="candidate_medical" value="Yes" id="medYes"  /> Yes
                                    </label>
                                 </div>
                                 <div class="radio">
                                    <label class="form-check-label">
                                    <input type="radio" class="form-check-input" name="candidate_medical" value="No" id="medNo" style="font-weight: 700" checked/> No
                                    </label>
                                 </div>

                              </div>
                           </div>
                        </div>
                        <div class="row clearfix">
                          <div class="medical_history" id="med_his" style="display:none;">
                           <div class="col-sm-12 col-md-6 col-lg-6">
                              <div class="row">
                                 <div class="col-sm-12 col-md-4 col-lg-4 ">
                                    <div class="form-group">
                                       <label>Please State Briefly</label>
                                    </div>
                                 </div>
                                 <div class="col-sm-12 col-md-8 col-lg-8">
                                    <div class="form-group">
                                      <textarea rows="3"  class="form-control" placeholder="Please State Briefly " name="medical_brief" value=""></textarea>
                                    </div>
                                 </div>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-6 col-lg-6">
                              <div class="row">
                                 <div class="col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group">
                                       <label>Attach medical certificate</label>
                                    </div>
                                 </div>
                                 <div class="col-sm-12 col-md-8 col-lg-8">
                                    <div class="form-group">
                                       <input type="file" class="form-control"  placeholder="Attach medical certificate" name="medical_certificate" >
                                    </div>
                                 </div>
                              </div>
                           </div>
                         </div>
                        </div>
                        <div class="row clearfix">
                           <div class="col-sm-12 col-md-4 col-lg-4 ">
                              <div class="form-group">
                                 <label>If you were punished for misconduct/ violation of Hostel rules/indiscipline etc. give particulars<label>
                              </div>

                           </div>
                           <div class="col-sm-12 col-md-8 col-lg-8">
                              <div class="form-group" >
                                 <textarea  name="mis_conduct" style="width: 443px;
    height: 100px;"></textarea>
                              </div>
                           </div>
                        </div>
                         <div class="row clearfix">
                           <div class="col-sm-12 col-md-6 col-lg-6">
                              <div class="row">
                                 <div class="col-sm-12 col-md-4 col-lg-4 ">
                                    <div class="form-group">
                                       <label>Any Vehicle? </label>
                                    </div>
                                 </div>
                                 <div class="col-sm-12 col-md-8 col-lg-8">
                                    <div class="form-group">
                                       <select name="vehicle_type" id="vehicle_type" class="form-control">
                                          <option value="">Select</option>
                                          <option value="Motorcycle">Motorcycle </option>
                                          <option value="Scooter">Scooter </option>
                                          <option value="Moped">Moped </option>
                                          <option value="Car">Car </option>
                                          <option value="Cycle">Cycle</option>
                                       </select>
                                    </div>
                                 </div>
                              </div>
                           </div>
                           <div class="col-sm-12 col-md-6 col-lg-6">
                              <div class="row">
                                 <div class="col-sm-12 col-md-4 col-lg-4 ">
                                    <div class="form-group">
                                       <label>Make / Model of the Vehicle: </label>
                                    </div>
                                 </div>
                                 <div class="col-sm-12 col-md-8 col-lg-8">
                                    <div class="form-group">
                                       <input type="text" class="form-control" name="vehicle_year" placeholder="Make / Model of the Vehicle">
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
                                       <label>Vehicle Reg. No. </label>
                                    </div>
                                 </div>
                                 <div class="col-sm-12 col-md-8 col-lg-8">
                                    <div class="form-group">
                                       <input type="text" class="form-control" name="vehicle_reg_no" placeholder="Vehicle Reg. No. ">
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <div class="row">
                           <button type="submit" class="btn btn-success">Submit</button>
                        </div>
               </form>
            </div>
         </div>
      </div>
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


        $("input[name='candidate_medical']").click(function() {
    if ($("#medYes").is(":checked")) {
      $("#med_his").show();
    } else {
      $("#med_his").hide();
    }
  });

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

      $("#txtBirthDate").datepicker(
            {

                minDate: new Date(1900, 1 - 1, 1),
                maxDate: '-10Y',
                defaultDate: new Date(2020, 5 - 1, 15),
                changeMonth: true,
                changeYear: true,
                yearRange: '-50:-10',
                dateFormat: 'dd-MM-yy',
                setDate: new Date(2020, 5 - 1, 15),
                onSelect: function (date, ui)
                {
                    var dob = new Date(date);
                    var today = new Date();
                    var age = today.getFullYear() - ui.selectedYear;
                    $('#CalculatedAge').val(age);
                }
            });

    $("#btnBirthDate").click(function ()
    {
        $("#txtBirthDate").focus();
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
                  hostel_type: {
                      required: true
                  },
                  academic_year: {
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
                  age: {
                      required: true
                  },
                  Course: {
                      required: true
                  },
                  study_year: {
                      required: true
                  },
                  date_of_admission: {
                      required: true
                  },
                  gr_num: {
                      required: true
                  },
                  contact_number: {
                      required: true
                  },
                  email: {
                      required: true
                  },
                  permanent_address: {
                      required: true
                  },
                  occupation: {
                      required: true
                  },
                  service_address: {
                      required: true
                  },
                  resnum: {
                      required: true
                  },
                  offnum: {
                      required: true
                  },
                  mobnum: {
                      required: true
                  },
                  gender: {
                      required: true
                  },
                  guardian_fax: {
                      required: true
                  },
                  correspondence_address: {
                      required: true
                  },
                  parent_signature: {
                      required: true
                  },
                  correspondence_fax: {
                      required: true
                  },
                  correspondence_email: {
                      required: true
                  },
                  correspondence_mobile: {
                      required: true
                  },
                  hostel_stay: {
                      required: true
                  },
                  vehicle_type: {
                      required: true
                  },
                  candidate_medical: {
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
                  relationship_to_guardian: {
                      required: true,
                      letters: true
                  }
              },
              messages: {
                  choice_subject_XI: "Choice of subject is required",
                  specialization: "Stream is required",
                  language_of_choice: "Language of choice is required",
                  category_sought: "Category is required",
                  academic_year:"Academic Year is Required",
                  hostel_type:"Hostel Type is Required",
                  full_name:"Full Name is Required",
                  age:"Age is Required",
                  study_year:"Year of Study is Required",
                  date_of_admission:"Date of Admission is Required",
                  gr_num:"G.R Number is Required",
                  service_address:"Address of Business/Service is Required",
                  occupation:"Occupation is Required",
                  permanent_address:"Permanent Address is Required",
                  resnum:"Residential Number is Required",
                  offnum:"Office Number is Required",
                  mobnum:"Mobile Number is Required",
                  correspondence_address:"Address for Correspondence is Required",
                  contact_number:"Contact Number of the Candidate is Required",
                  gender:"Please select the Gender",
                  Course:"Please select the Course",
                  photo: {
                      required: "Photograph is required",
                      accept: "Only image type jpg/png/jpeg is allowed"
                  },
                  signature_photo: {
                      required: "Photograph is required",
                      accept: "Only image type jpg/png/jpeg is allowed"
                  },
                  parent_signature: {
                      required: "Photograph is required",
                      accept: "Only image type jpg/png/jpeg is allowed"
                  },
                  first_name: {
                      required: "The First name is required and cannot be empty",
                      letters: "The First name can only consist of alphabetical"
                  },
                  last_name: {
                      required: "The Last name is required and cannot be empty",
                      letters: "The Last name can only consist of alphabetical"
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
                  relationship_to_guardian: {
                      required: "The Relationship to guardain is required and cannot be empty",
                      letters: "The Relationship to guardain can only consist of alphabetical."
                  },

              },
         submitHandler: function (form) {
           var formData = new FormData(form);
           $('#divLoading').addClass('show');
           $('#divLoading').show();
           $.ajax({
             type: "POST",
             url: "<?php echo base_url('generate-hostel-pdf'); ?>",
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
