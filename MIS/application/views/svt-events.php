<!DOCTYPE html>
<html lang="en">

  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SVT Events – National Seminar on "Reinventing Learning Spaces" - Registration Form</title>
	
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/bootstrap.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/vendor/fontawesome-free/css/all.min.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/style2.css"/>
	
    <!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css"/>


    <link href="https://fonts.googleapis.com/css?family=Roboto:400,400i,500,500i,700,700i,900" rel="stylesheet">

	<link rel="stylesheet" href="<?php echo base_url('assets/css/jquery-ui.css'); ?>">
	<style>h2{color: white;}.error{color: red;}</style>
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
				<h2>
					National Seminar on "Reinventing Learning Spaces"
				</h2>
			</div> 
			<form class="svt_form" id="svt_events" method="POST" action="<?php echo base_url() ?>Events/events_payment" enctype="multipart/form-data" autocomplete="off">
				<div class="card"> 
					<div class="card-header">
						<i class="far fa-credit-card"></i> Event Registration
					</div>
					<div class="card-body">
						<div class="row reverse_grid">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="first_name"><b>Event Date</b>: March 2, 2020 to March 3, 2020</label>
									<label for="first_name"><b>Venue</b>: Mini Auditorium, Sir Vithaldas Thackersey College of Home Science (Autonomous)</label>
								</div>
								
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
									<label for="first_name">Institute Name: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="institute" id="institute" value=""/>
									<span class="text-danger"></span>
								</div>
								<div class="form-group">
									<label for="phone_number">Designation: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="designation" id="v" value=""/>
									<span class="text-danger"></span>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="comment">Mailing address <span class="required-mark">*</span></label>
									<textarea class="form-control" name="residential_address" rows="3" id="comment"></textarea>
								</div>
							</div>
						</div>
						<table class="table">
							<tbody class="aliceblue">
								<tr>
									<th>Fees</th>
									<th>₹ <span id="app_fees_amount">300.00</span></th>
								</tr>
								<tr>
									<td>Online Payment Processing Charge</td>
									<td>₹ <span id="online_processing_fee">10.00</span></td>
								</tr>
								<tr>
									<th>Grand Total</th>
									<th>₹ <span id="total_app_amount">310.00</span></th>
								</tr>
							</tbody>
						</table>
						<input type="hidden" id="product_info" name="product_info" value="National Seminar on Reinventing Learning Spaces">
						<input type="hidden" id="fees_amount" name="fees_amount" value="300">
						<input type="hidden" id="online_amount" name="online_amount" value="10.00">
						<input type="hidden" id="total_amount" name="total_amount" value="310.00"> 
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

<script>
$('#svt_events').validate({
		rules:{
			name: "required",
			residential_address: "required",
			institute: "required",
			designation: "required",
			phone_number:{
				required: true,
				number: true
			},
			email_address:{
				required: true,
				email: true
			}
			
		}
	}); 
	
</script>