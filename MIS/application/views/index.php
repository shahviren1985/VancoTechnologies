<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <meta name="description" content="">
        <meta name="author" content="">
        <title>SVT MIS - Login</title>
        <!-- Bootstrap core CSS-->
        <link href="<?php echo base_url('assets/vendor/bootstrap/css/bootstrap.min.css'); ?>" rel="stylesheet">
        <!-- Custom fonts for this template-->
        <link href="<?php echo base_url('assets/vendor/fontawesome-free/css/all.min.css" rel="stylesheet'); ?>" type="text/css">
        <!-- Custom styles for this template-->
        <link href="<?php echo base_url('assets/css/sb-admin.css" rel="stylesheet'); ?>">
    </head>
    <style>
        .bg{
        background-image: url('<?php echo base_url("assets/img/login-bg.jpg"); ?>');
        background-size: cover;
        background-repeat: no-repeat;
        }
        .card-bg{
        background-color: #ffffffc9 !important;
        }
    </style>
    <body>
    	<div class="account_login">
	        <div class="container">
	            <div class="card card-login card-bg">
				<div class="text-center">
					<img src="<?php echo base_url('assets/img/logo.png');?>" height="50px"  />
				</div>
	            	<div class="form_title"><h4>Account <span>Login</span></h4></div>
	            	<div class="bg_inner">
		                <div class="card-header text-center medium">Sign in to start your session</div>
		                <div class="card-body">
		                    <form action="user_login" method="post">
		                        <div class="form-group">
		                            <div class="form-label-group">
		                                <input type="text" id="userID" name="userID" class="form-control" value="" placeholder="College Registration Number" autofocus="autofocus">
		                                <label for="userID">Username</label>
		                                <span class="text-danger"><?php echo form_error('userID'); ?></span>
		                            </div>
		                        </div>
		                        <div class="form-group">
		                            <div class="form-label-group">
		                                <input type="password" id="password" name="password" class="form-control" value="" placeholder="Password" >
		                                <label for="password">Password</label>
		                                <span class="text-danger"><?php echo form_error('password'); ?></span>
		                            </div>
		                        </div>
		                        <div class="submit_btn clearfix"><button class="btn btn-primary btn-block">Login</button></div>
		                    </form>
		                    <!-- <div class="text-center">
		                        <a class="d-block small mt-3" href="<?php //echo base_url('register'); ?>">Register an Account</a>
		                        </div> -->
		                    <div class="text-center mt-3" style="margin: 0px !important;">
		                        <span class="text-danger">
		                        <?php if(isset($_SESSION['error'])){ echo $_SESSION['error']; } ?>
		                        </span>
		                    </div>
		                </div>
		            </div>
	            </div>
	        </div>
	    </div>
        <!-- Bootstrap core JavaScript-->
        <script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>
        <script src="<?php echo base_url('assets/vendor/bootstrap/js/bootstrap.bundle.min.js'); ?>"></script>
        <!-- Core plugin JavaScript-->
        <script src="<?php echo base_url('assets/vendor/jquery-easing/jquery.easing.min.js'); ?>"></script>
    </body>
</html>