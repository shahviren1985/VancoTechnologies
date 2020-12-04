<!DOCTYPE html>
<html lang="en">

  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SVT Refund Policy</title>
	
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/bootstrap.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/vendor/fontawesome-free/css/all.min.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/style2.css"/>
	
    <!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css"/>


    <link href="https://fonts.googleapis.com/css?family=Roboto:400,400i,500,500i,700,700i,900" rel="stylesheet">

	<link rel="stylesheet" href="<?php echo base_url('assets/css/jquery-ui.css'); ?>">
	<style>
	.wrapper_bg_img {
		position: absolute;
		top: 0;
		left: 0;
		right: 0;
		bottom: 0;
	}
	</style>
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
				<a class="navbar-brand" href="https://svt.edu.in"><img src="<?php echo base_url(); ?>assets/img/logo-white.png" alt="SVT Privacy Policy"></a>
				<h1>
					<strong>SVT</strong> Refund Policy
				</h1>
			</div> 
			<div class="card">	
				<div class="refund-policy-wrapper">
					<div class="card-header">
						<i class="far fa-credit-card"></i> SVT Event Refund Policy
					</div>
					<div class="card-body">
						<ol>
							<li> Registration fees is one time event fees.</li>
							<li> Event registration fees is for an Event "National Seminar on: Reinventing Learning Spaces" on March 2 & 3, 2020.</li>
							<li> After making a payment for an event - User won't be able to cancel the registration.</li>
							<li> User's are not going to get any refund post payment for membership.</li>
							<li> Event registration fees are non-refundable. </li>
						</ol>
					</div>
				</div>
				
				<div class="refund-policy-wrapper">
					<div class="card-header">
						<i class="far fa-credit-card"></i> Alumni Refund Policy
					</div>
					<div class="card-body">
						<ol>
							<li> Alumni registration fees is life time membership.</li>
							<li> Alumni event registration fees is for SVT Alumni Event on January 11, 2020.</li>
							<li> After making a payment for membership - Alumni won't be able to cancel the membership.</li>
							<li> Member's are not going to get any refund post payment for membership.</li>
							<li> Event registration fees are non-refundable. </li>
						</ol>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>
<script src="https://cdn.jsdelivr.net/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="<?php echo base_url('assets/js/jquery-ui.js') ?>"></script>


<!-- BootstrapValidator JS -->
<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>
<script src="<?php echo base_url('assets/js/custom.js') ?>"></script> 
</body>
</html>