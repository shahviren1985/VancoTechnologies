<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<!DOCTYPE html>
<html lang="en">

<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
	<meta name="description" content="">
	<meta name="author" content="">

	<title>SVT First Year Academic Fees</title>

	<!-- Bootstrap core CSS-->
	<link href="<?php echo base_url('assets/vendor/bootstrap/css/bootstrap.min.css'); ?>" rel="stylesheet">

	<!-- Custom fonts for this template-->
	<link href="<?php echo base_url('assets/vendor/fontawesome-free/css/all.min.css'); ?>" rel="stylesheet">

	<!-- Page level plugin CSS-->
	<link href="<?php echo base_url('assets/vendor/datatables/css/dataTables.bootstrap4.css'); ?>" rel="stylesheet">
	
	<link rel="stylesheet" href="<?php echo base_url('assets/css/datepicker3.css" rel="stylesheet'); ?>">

	<!-- Custom styles for this template-->
	<link href="<?php echo base_url('assets/css/student.css" rel="stylesheet'); ?>">
</head>

<body>
	<style>
	.wrapper_bg_img{
		background:url(<?php echo base_url('assets/img/bg_1.jpg'); ?>) no-repeat center center / cover;
		background-attachment:fixed;
		padding:20px 0;
		height:100%;
	}
	.top_title {
		display: flex;
		align-items: center;
		position: relative;
		padding: 20px 0;
		justify-content: center;
		flex-wrap:wrap;
	}
	.top_title a.navbar-brand {
		position: absolute;
		left: 0;
	}
	.top_title h1 {
		color: #fff;
	}
	@media (max-width: 768px) {
		.top_title a.navbar-brand{
			position:relative;
			width:100%;
			display:block;
			text-align:center;
		}
	}
	</style>
	<div class="wrapper_bg_img">
		<div class="container">
			<div class="top_title">
				<a class="navbar-brand" href="https://svt.edu.in"><img src="<?php echo base_url('assets/img/logo-white.png'); ?>" alt="SVT Examination Fees Payment"></a>
				<h1>
					<strong>SVT</strong> Academic Fees
				</h1>
			</div>
		</div>
		<div id="wrapper">
		