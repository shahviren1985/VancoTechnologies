<!DOCTYPE html>
<html lang="en">

  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SVT Alumni– Registration Form</title>
	
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/bootstrap.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/vendor/fontawesome-free/css/all.min.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/style2.css"/>
	
    <!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css"/>


    <link href="https://fonts.googleapis.com/css?family=Roboto:400,400i,500,500i,700,700i,900" rel="stylesheet">

	<link rel="stylesheet" href="<?php echo base_url('assets/css/jquery-ui.css'); ?>">
  </head>
<body>
<div id="content-wrapper" class="wrapper_bg_img">
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	<div class="alert alert-danger print-error-msg" style="display:none; color:#fff;"></div>	
		
	<div class="wrapper">
		<div class="container">
			<div class="top_title">
				<a class="navbar-brand" href="https://svt.edu.in"><img src="<?php echo base_url(); ?>assets/img/logo-white.png" alt="SVT Payment Fees"></a>
				<h1>
					<strong>SVT</strong> Alumni Registration
				</h1>
			</div> 
			<form class="svt_form" id="alumini_registration" method="POST" action="<?php echo base_url() ?>AluminiCopy/alumini_payment" enctype="multipart/form-data" autocomplete="off">
				<div class="card"> 
					<div class="card-header">
						<i class="far fa-credit-card"></i> Alumni Registration
					</div>
					<div class="card-body">
						<div class="row reverse_grid">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="first_name">Name: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="name" id="name" value=""/>
									<span class="text-danger"></span>
								</div>
								<div class="form-group">
									<label for="phone_number">Mobile: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="phone_number" id="phone_number" value=""/>
									<span class="text-danger"></span>
								</div>
								<div class="form-group">
									<label for="phone_number">Landline:</label>
									<input type="text" class="form-control" name="landline" id="landline" />
									<span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="photo_upload text-center">
									<span class="pspan">Uploaded photograph will appear here.</span>
									 <img src="" id="photo" width="160" style="display: none;">
								</div>
							</div>
						</div>
						<div class="row">
							 <div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="email_address">Email Address: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="email_address" id="email_address" value="">
									<span class="text-danger"></span>
								</div>		
							 </div>
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="comment">Residential address <span class="required-mark">*</span></label>
									<textarea class="form-control" name="residential_address" rows="3" id="comment"></textarea>
								</div>
							</div>
						</div>
						<div class="row photo_notice">
							<div class="col-md-12">
								<div class="form-group">
									<label class="control-label">Photograph</label>
									<div class="">
										<input type="file" name="photo" id="upload">
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="dob">Date of Birth:</label>
									<input type="text" class="form-control" name="dob" id="dob"/>
									<span class="text-danger"></span>
								</div>
							</div>	
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="age">Age:</label>
									<input type="text" class="form-control" name="age" id="age" readonly />
									<span class="text-danger"></span>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
								<div class="form-group student_field_radio">
									<label for="student_field"><input type="radio" name="student_field" id="bachelor" value="Bachelor"> Bachelors</label>
									<span style="margin:0 20px;"></span>
									<label for="student_field"><input type="radio" name="student_field" id="master" value="Master"> Masters</label>
								</div>
							</div>
						</div>
						<h5 class="form_heading">Academic Information - Degree/Diploma</h5>
						<div class="row">
							<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
								<div class="form-group">
								   <label for="">Name of the Department:</label>
								   <select name="department" id="department" class="form-control">
									  <option value="">Select</option>
								   </select>
								   <span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
								<div class="form-group">
								   <label for="">Name of the Specialization:</label>
								   <select name="specialization" id="specialization" class="form-control">
									  <option value="">Select</option>
									 
								   </select>
								   <span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
								<div class="form-group">
								   <label for="">Year of passing:</label>
								   <select name="year_of_passing" id="year_of_passing" class="form-control"></select>
								   <span class="text-danger"></span>
								</div>
							</div>
						</div>
						
						<h5 class="form_heading">PARTICIPATION IN EXTRACURRICULAR Activities</h5>
						<div class="row">
							<div class="col-sm-12 col-md-3 col-lg-3 col-xl-3">
								<div class="custom-control custom-checkbox">
									<input type="checkbox" name="extra_activity" class="custom-control-input" id="customCheck1" value="Gymkhana" />
									<label class="custom-control-label" for="customCheck1">Gymkhana</label>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-3 col-lg-3 col-xl-3">
								<div class="custom-control custom-checkbox">
									<input type="checkbox" name="extra_activity" class="custom-control-input" id="customCheck2" value="NSS" />
									<label class="custom-control-label" for="customCheck2">NSS</label>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-3 col-lg-3 col-xl-3">
								<div class="custom-control custom-checkbox">
									<input type="checkbox" name="extra_activity" class="custom-control-input" id="customCheck3" value="Extension work" />
									<label class="custom-control-label" for="customCheck3">Extension work</label>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-3 col-lg-3 col-xl-3">
								<div class="custom-control custom-checkbox">
									<input type="checkbox" name="extra_activity" class="custom-control-input" id="customCheck4" value="Research/Project" />
									<label class="custom-control-label" for="customCheck4">Research/Project</label>
								</div>
							</div>
						</div>
						<br>
						<div class="form-group">
							<label for="">I would like to contribute towards the growth of SVT by</label>
							<textarea class="form-control" name="alumini-message" id="alumini-message" placeholder=""></textarea>
						</div>
						<?php /*<div class="form-group sig_grid">
							<label for="">Signature of the Student </label>
							<input class="form-control" name="signature" id="signature" placeholder="" type="text"/>
							<p>Life membership fee: Rs 500/-</p>
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="">Receipt Number</label>
									<input class="form-control" name="receipt-number" id="receipt-number" placeholder="" type="text"/>
								</div>
							</div>
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="">Date of Payment</label>
									<input class="form-control" name="date-of-payment" id="date-of-payment" placeholder="" type="text"/>
								</div>
							</div>
						</div>	*/?>
						
						<h5 class="form_heading">If you know some other batch-mates/alumni please give their names and email id, phone number and postal address here.</h5>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="batch-mates_name">Name of batch-mates/alumni: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="alumini_name" id="alumini_name" value="">
									<span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="contact_details">Residential Address</label>
									<textarea class="form-control" rows="2" name="contact_details" id="contact_details"></textarea>
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="email_address">Email Address: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="alumini_email_address" id="alumini_email_address" value="">
									<span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="phone_number2">Mobile: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="alumini_mobile_number" id="alumini_mobile_number" value=""/>
									<span class="text-danger"></span>
								</div>
							</div>
						</div>
						<div class="alumini-mate-row">
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<button id="add_batch_mate" class="btn btn-success" title="Add More Alumini" data-id="1"><i class="fas fa-plus"></i></button>
							</div>
						</div>
						<br>
						<table class="table">
							<tbody class="aliceblue">
								<tr>
									<th>Fee Head Total</th>
									<th>₹ <span id="app_fees_amount">500.00</span></th>
								</tr>
								<tr>
									<td>Online Payment Processing Charge + GST</td>
									<td>₹ <span id="online_processing_fee">13.00</span></td>
								</tr>
								<!--<tr>
									<td>GST (18%)</td>
									<td>₹ <span id="gst_fee">0.00</span></td>
								</tr> -->
								<tr>
									<th>Grand Total</th>
									<th>₹ <span id="total_app_amount">513.00</span></th>
								</tr>
							</tbody>
						</table>
						<input type="hidden" id="fee_head_total" name="fee_head_total" value="500">
						<input type="hidden" id="commission" name="commission" value="13.00">
						<input type="hidden" id="amount" name="amount" value="513.00"> 
						<div class="row">
							<div class="col-sm-12 col-md-12 col-lg-12 col-xl-12">
								<input type="hidden" name="hidden_data" id="hidden_data" value="1">
								<input type="submit" id="alumini-submit" class="btn btn-success" name="submit" value="Make Payment">
								<div class="refund-policy"><a href="<?php echo base_url() ?>refund-policy" target="_blank">Refund Policy</a></div>
							</div>
						</div>
					</div>
				</div>
			</form>
		</div>
	</div>
</div>
</body>
<script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>
<script src="https://cdn.jsdelivr.net/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="<?php echo base_url('assets/js/jquery-ui.js') ?>"></script>


<!-- BootstrapValidator JS -->
<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>
<script src="<?php echo base_url('assets/js/custom.js') ?>"></script> 
<script>
$('#alumini_registration').validate({
		rules:{
			name: "required",
			residential_address: "required",
			photo: "required",
			phone_number:{
				required: true,
				number: true
			},
			email_address:{
				required: true,
				email: true
			},
			dob:{
				required: true,
				date: true
			},
			department: "required",
			specialization: "required",
			alumini_email_address:{
				required: function(element) {
					if ($("#alumini_name").val() != '') {
						return true;
					} else{
						return false;
					}
				}
			},
			alumini_mobile_number:{
				required: function(element){
					if($("#alumini_name").val() != '') {
						return true;
					} else{
						return false;
					}
				}
			}
		}/* ,
		submitHandler: function(form){
			var $form = jQuery("#alumini_registration");
			var formData = new FormData(jQuery('#alumini_registration')[0]);
			jQuery.ajax({				
				url: '<?php echo base_url() ?>Alumini/alumini_payment',
				type: "POST",
				dataType:"json",
				data: formData,
				cache: false,
				contentType: false,
				processData: false,
				success: function(data){
					if(data.success == true){
						$(".print-error-msg").html(data.msg);
					}else{
						$(".print-error-msg").html(data.msg);
					}
				}
			});
		} */
	}); 
	
</script>