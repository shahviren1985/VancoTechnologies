<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
  #search td { width: 20% !important; }
  #search .form-control { width: 100% !important; }
  .export_excel_file{float:right;margin-bottom:10px;}
</style>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
      </li>
      <li class="breadcrumb-item active">Leaving Certificate</li>
    </ol>
	
	<?php if($this->session->flashdata('msg')): ?>
		<div class="alert alert-success">
			<?php echo $this->session->flashdata('msg'); ?>
		</div> 
	<?php endif; ?>    
	
	<?php //if( isset($result) && count($result) ) {?>
      <!-- DataTables Example -->
      <div class="card mb-3">
        <div class="card-header">
          <i class="fas fa-list"></i>
          Select Student to Generate Leaving Certificate
        </div>
        <div class="card-body">
	<form class="filter_students" id="filter_students_certificate" action="<?php echo base_url(); ?>office_admin/StudentExcelExport/searchstudentcertificates">
				<div class="row align-item-center">
					<div class="col-md-12">
						<p><strong>Filter Search:</strong></p>
					</div>					
					<div class="col-md-2">
						<div class="form-group">
							<?php 
							$current_year = date('Y');
							$current_month = date('n');
							if($current_month < 5){
								$current_year = date('Y', strtotime('-1 years'));
							}?>
							<label for="s_academic_year"><strong>Admission Year</strong></label>
							<input type="text" name="s_academic_year" id="s_academic_year" class="form-control s_academic_year" value="<?php echo $current_year;?>">
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<label for="s_course_name"><strong>Course Name</strong></label>
							<?php if($courseList): ?>
							<select name="s_course_name" id="s_course_name" class="form-control">
								<option value="">Course Name</option>
								<?php foreach($courseList as $crs):?>
								<option value="<?php echo $crs->year;?>-<?php echo ($crs->course_name) ? $crs->course_name : 'Regular';?>"><?php echo $crs->year;?>&nbsp;<?php echo $crs->course_type;?>-<?php echo ($crs->course_name) ? $crs->course_name : 'Regular';?></option>
								<?php endforeach;?>
							</select>
							<?php endif;?>
						</div>
					</div>
				
					<div class="col-md-2">
						<div class="form-group">
						<label for="s_specialization"><strong>Specialization</strong></label>
							<?php if($specialization): ?>
							<select name="s_specialization" id="s_specialization" class="form-control">
								<option value="">Specialization</option>
								<?php foreach($specialization as $spec):?>
									<option value="<?php echo $spec->specialization;?>"><?php echo $spec->specialization;?></option>
								<?php endforeach;?>
							</select>
							<?php endif;?>
						</div>
					</div>
					<div class="col-md-1">
						<div class="form-group">
							<button class="btn btn-primary" id="search_student" name="search_student">Search</button>
						</div>
					</div>
					<div class="col-md-1">
						<div class="form-group">
							<button type="reset" class="btn btn-secondary reset_filter_search">Clear Filter</button>
						</div>
					</div>					
				</div>
				<div class="processing_loader">Processing...</div>
			</form>
			<div class="table-responsive">
				<table class="table table-bordered" id="dataTable2" width="100%" cellspacing="0" style="text-transform:capitalize;">
				  <thead>
					<tr class="t-heading">
						<th class="fs-16">Sr.No</th>        
						<th class="fs-16">College Registration Number</th>
						<th class="fs-16">Roll Number</th>
						<th class="fs-16">Course Name</th>
						<th class="fs-16">Specialization</th>
						<th class="fs-16">Full Name</th>
						<th class="fs-16">Mobile Number</th>
					</tr>
				  </thead>             
				</table>
			</div>
        </div>
        <div class="card-footer"></div>
      </div>
    <?php //} ?>

  </div>
  <!-- /.container-fluid -->
  <!--<script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>--> 