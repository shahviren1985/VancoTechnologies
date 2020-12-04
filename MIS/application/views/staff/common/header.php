<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<!DOCTYPE html>
<html lang="en">

	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta name="description" content="">
		<meta name="author" content="">

		<title>Staff</title>

		<!-- Bootstrap core CSS-->
		<link href="<?php echo base_url('assets/vendor/bootstrap/css/bootstrap.min.css'); ?>" rel="stylesheet">

		<!-- Custom fonts for this template-->
		<link href="<?php echo base_url('assets/vendor/fontawesome-free/css/all.min.css'); ?>" rel="stylesheet">

		<!-- Page level plugin CSS-->
		<link href="<?php echo base_url('assets/vendor/datatables/dataTables.bootstrap4.css'); ?>" rel="stylesheet">
			
		<link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.10/css/select2.min.css" rel="stylesheet" />
		
		<link rel="stylesheet" href="<?php echo base_url('assets/css/datepicker3.css" rel="stylesheet'); ?>">
		<!-- Custom styles for this template-->
		<link href="<?php echo base_url('assets/css/sb-admin.css" rel="stylesheet'); ?>">

		<!-- Custom styles for this template-->
		<link href="<?php echo base_url('assets/css/student.css" rel="stylesheet'); ?>">
	</head>

  <body id="page-top">
	
    <nav class="navbar navbar-expand navbar-dark bg-dark static-top">	
		
		<a class="navbar-brand mr-1" href="<?php echo base_url('staff/home'); ?>">
			<img src="<?php echo base_url('assets/img/logo.png');?>" height="50px"  /> - Staff
		</a>

		<button class="btn btn-link btn-sm text-dark order-1 order-sm-0" id="sidebarToggle" href="#">
			<i class="fas fa-bars"></i>
		</button>

		<!-- Navbar -->		
		<ul class="navbar-nav ml-auto">
			<li class="nav-item dropdown no-arrow">
				<a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
				Welcome, <?php echo strtoupper($this->session->userdata("staff_name")); ?>
					<i class="fas fa-user-circle fa-fw"></i>
				</a>
				<div class="dropdown-menu dropdown-menu-right" aria-labelledby="userDropdown">
					<a class="dropdown-item" href="<?php echo base_url("logout"); ?>">Logout</a>
				</div>
			</li>
		</ul>
    </nav>
	
    <div id="wrapper">

        <!-- Sidebar -->		
        <ul class="sidebar navbar-nav">
			<li class="nav-item <?php echo($this->uri->segment(2)=="home") ? 'active' : '';?>">
				<a class="nav-link"  href="<?php echo base_url('staff/staffs'); ?>">
					<i class="fas fa-fw fa-tachometer-alt"></i>
					<span>Home</span>
				</a>
			</li>
			<li class="nav-item <?php echo($this->uri->segment(3)=="view_papers") ? 'active' : '';?>">
				<a class="nav-link" href="#">
					<i class="fa fa-eye" aria-hidden="true"></i>
					<span>View Papers</span>
				</a>
			</li>
			<?php /*<li class="nav-item <?php echo($this->uri->segment(3)=="view_ge") ? 'active' : '';?>">
				<a class="nav-link" href="#">
					<i class="fas fa-history"></i>
					<span>View GE</span>
				</a>
			</li> 
			<li class="nav-item <?php echo($this->uri->segment(2)=="internal_marks") ? 'active' : '';?>">
				<a class="nav-link" href="#">
					<i class="far fa-folder-open"></i>
					<span>Internal Marks</span>
				</a>
			</li> */?>
			<li class="nav-item <?php echo($this->uri->segment(2)=="view_attendance") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('staff/view_attendance'); ?>">
					<i class="fa fa-eye" aria-hidden="true"></i>
					<span>View Attendance</span>
				</a>
			</li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="leave_application") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('staff/leave_application'); ?>">
					<i class="fas fa-chart-bar"></i>
					<span>Leave Applications</span>
				</a>
			</li>	
			<?php if($staff_details[0]->role == "HOD" || $staff_details[0]->role == "Office Supretendent" || $staff_details[0]->role == "Principal"){ ?>
			<li class="nav-item <?php echo($this->uri->segment(2)=="leave_request_list") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('staff/leave_request_list'); ?>">
					<i class="fa fa-eye" aria-hidden="true"></i>
					<span>View Leave Request</span>
				</a>
			</li>
			<?php } ?>
			<li class="nav-item <?php echo($this->uri->segment(2)=="documents") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('staff/documents'); ?>">
					<i class="far fa-credit-card"></i>
					<span>Documents</span>
				</a>
			</li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="feedback") ? 'active' : '';?>">
				<a class="nav-link" href="#">
					<i class="far fa-comments"></i>
					<span>Feedback</span>
				</a>
			</li>
			<?php if($staff_details[0]->role == "HOD"){ ?>
			<li class="nav-item <?php echo($this->uri->segment(2)=="approve_elective") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('staff/approve_elective'); ?>">
					<i class="far fa-credit-card"></i>
					<span>Approve Elective</span>
				</a>
			</li>
			<?php } ?>
        </ul>
