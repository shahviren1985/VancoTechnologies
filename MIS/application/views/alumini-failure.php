<?php defined('BASEPATH') OR exit('No direct script access allowed');?>
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

	<link rel="stylesheet" href="<?php echo base_url('assets/css/datepicker3.css" rel="stylesheet'); ?>">
	<style>
		.wrapper_bg_img{
			position: absolute;
			top: 0;
			bottom: 0;
			left: 0;
			right: 0;
		}
	</style>
  </head>
<body>
<div id="content-wrapper" class="wrapper_bg_img">
	<div class="wrapper">
		<div class="container">
			<div class="container mt-5">
				<div class="row">
					 <div class="col-md-12">
						<div class="card">
							<h4 class="card-header badge-danger">Fee Payment Failed. <i class="fas fa-times-circle"></i></h4>
							<div class="card-body">
								<?php 
								echo "<h4>Your payment status is ". $status .".</h4>";
								echo "<h6>Your transaction id for this transaction is ".$txnid.".</h6>"; ?>
							</div>
						</div>
					 </div> 
					<div class="col-md-2"></div>
				</div>
			</div> 
		</div>	
	</div>	
</div>	
</body>
<script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>
<script src="https://cdn.jsdelivr.net/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="<?php echo base_url('assets/js/bootstrap-datepicker.js') ?>"></script>


<!-- BootstrapValidator JS -->
<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js"></script>
<script src="<?php echo base_url('assets/js/custom.js') ?>"></script> 
<script>
