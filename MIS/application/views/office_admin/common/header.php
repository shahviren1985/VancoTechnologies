<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<!DOCTYPE html>
<html lang="en">

  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SVT MIS - Office Admin</title>
    <script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>
    <!-- Bootstrap core CSS-->
    <link href="<?php echo base_url('assets/vendor/bootstrap/css/bootstrap.min.css'); ?>" rel="stylesheet">

    <!-- Custom fonts for this template-->
    <link href="<?php echo base_url('assets/vendor/fontawesome-free/css/all.min.css'); ?>" rel="stylesheet" type="text/css">

    <!-- Page level plugin CSS-->
    <link href="<?php echo base_url('assets/vendor/datatables/css/dataTables.bootstrap4.min.css'); ?>" rel="stylesheet">
    <link href="<?php echo base_url('assets/vendor/datatables/css/buttons.bootstrap4.min.css'); ?>" rel="stylesheet">

    <!-- <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap4.min.css"/> -->
	<!-- <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.6/css/buttons.bootstrap4.min.css"/> -->
	<link rel="stylesheet" href="<?php echo base_url('assets/css/datepicker3.css" rel="stylesheet'); ?>">


    <!-- Custom styles for this template-->
    <link href="<?php echo base_url('assets/css/sb-admin.css" rel="stylesheet'); ?>">

   <!-- <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">-->
    
  </head>

  <body id="page-top">

    <nav class="navbar navbar-expand navbar-dark bg-dark static-top">
      <a class="navbar-brand mr-1" href="<?php echo base_url('officeadmin/home'); ?>">
        <img src="<?php echo base_url('assets/img/logo.png');?>" height="50px"  /> - Office Admin
      </a>

      <button class="btn btn-link btn-sm text-dark order-1 order-sm-0" id="sidebarToggle" href="#">
        <i class="fas fa-bars"></i>
      </button>

      <!-- Navbar -->
      <ul class="navbar-nav ml-auto">
        <!-- <li class="nav-item dropdown no-arrow mx-1">
          <a class="nav-link dropdown-toggle" href="#" id="alertsDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fas fa-bell fa-fw"></i>
            <span class="badge badge-danger">9+</span>
          </a>
          <div class="dropdown-menu dropdown-menu-right" aria-labelledby="alertsDropdown">
            <a class="dropdown-item" href="#">Action</a>
          </div>
        </li>
        <li class="nav-item dropdown no-arrow mx-1">
          <a class="nav-link dropdown-toggle" href="#" id="messagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fas fa-envelope fa-fw"></i>
            <span class="badge badge-danger">7</span>
          </a>
          <div class="dropdown-menu dropdown-menu-right" aria-labelledby="messagesDropdown">
            <a class="dropdown-item" href="#">Action</a>
          </div>
        </li> -->
        <li class="nav-item dropdown no-arrow">
          <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Welcome, <?php echo strtoupper($this->session->userdata("userID")); ?>
            <i class="fas fa-user-circle fa-fw"></i>
          </a>
          <div class="dropdown-menu dropdown-menu-right" aria-labelledby="userDropdown">
            <!-- <a class="dropdown-item" href="<?php //echo base_url("home/profile/edit/".$this->session->userdata("id")); ?>">Edit Profile</a> -->
            <!-- <div class="dropdown-divider"></div> -->
            <a class="dropdown-item" href="<?php echo base_url("logout"); ?>">Logout</a>
          </div>
        </li>
      </ul>
    </nav>

    <div id="wrapper">
        <!-- Sidebar -->
        <ul class="sidebar navbar-nav">
            <li class="nav-item <?php echo($this->uri->segment(2)=="home") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/home'); ?>">
                <i class="fas fa-fw fa-home"></i>
                <span>Dashboard</span>
              </a>
            </li>
            <li class="nav-item <?php echo($this->uri->segment(2)=="search") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/search'); ?>">
                <i class="fas fa-search"></i>
                <span>Search Students</span>
              </a>
            </li>
            <li class="nav-item <?php echo($this->uri->segment(3)=="excel") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/upload/excel'); ?>">
                <i class="fas fa-file-excel"></i>
                <span>Import Students</span>
              </a>
            </li>  
            <li class="nav-item <?php echo($this->uri->segment(3)=="excel_update") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/update/excel_update'); ?>">
                <i class="fas fa-file-excel"></i>
                <span>Update Students</span>
              </a>
            </li>
            <li class="nav-item <?php echo($this->uri->segment(2)=="application_form") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/application_form'); ?>">
                <i class="fas fa-file-excel"></i>
                <span>Applications</span>
              </a>
            </li>            
            <li class="nav-item <?php echo($this->uri->segment(2)=="export-student-list") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/export-student-list'); ?>">
                <i class="fas fa-paste"></i>
                <span>Student List</span>
              </a>
            </li> 
			<li class="nav-item <?php echo($this->uri->segment(2)=="export-staff-list") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/export-staff-list'); ?>">
                <i class="fas fa-paste"></i>
                <span>Staff List</span>
              </a>
            </li> 
            <li class="nav-item <?php echo($this->uri->segment(2)=="staff-leave-list") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/staff-leave-list'); ?>">
                <i class="fas fa-paste"></i>
                <span>Leave Report</span>
              </a>
            </li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="reporting" || $this->uri->segment(2) == "search-report") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/reporting'); ?>">
                <i class="fas fa-paste"></i>
                <span>Reports</span>
              </a>
            </li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="leaving-certificate" || $this->uri->segment(2) == "leaving-certificate") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/leaving-certificate'); ?>">
                <i class="fas fa-paste"></i>
                <span>Leaving Certificate</span>
              </a>
            </li>
            <li class="nav-item <?php echo($this->uri->segment(2)=="addon-subject-student" || $this->uri->segment(2) == "addon-subject-student") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/addon-subject-student'); ?>">
                <i class="fas fa-user"></i>
                <span>Addon Students</span>
              </a>
            </li>
            <li class="nav-item <?php echo($this->uri->segment(2)=="upload_transaction") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('officeadmin/upload_transaction'); ?>">
                <i class="fas fa-file-excel"></i>
                <span>Import Students Transaction</span>
              </a>
            </li> 
			
			 <li class="nav-item <?php echo($this->uri->segment(2)=="feedback") ? 'active' : '';?>">
              <a class="nav-link" href="<?php echo base_url('office_admin/feedback'); ?>">
                <i class="fas fa-comments"></i>
                <span>Feedback</span>
              </a>
            </li> 
			
			
        </ul>
