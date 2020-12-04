<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
	.mh-98 { min-height: 98px !important; }
	.custom-icon { opacity: 0.3 !important; font-size: 4rem !important; }
	.b-bg { background: #0000005e; }
</style>
<div id="content-wrapper">

  <div class="container-fluid">

	<!-- Breadcrumbs-->
	<ol class="breadcrumb">
	  <li class="breadcrumb-item">
		<a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
	  </li>
	  <li class="breadcrumb-item active">Search</li>
	</ol>

	<?php if(isset($_SESSION['error'])){ ?>
	<div class="alert alert-danger alert-dismissible">
	  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
	  <strong>Error!</strong> <?php echo $_SESSION['error']; ?>
	</div>
	<?php } ?>

	<div class="card mb-3">
	  <div class="card-header">
		<i class="fas fa-search"></i>
		Search
	  </div>
	  <div class="card-body">
		<form method="post" id="search_form" action="<?php echo base_url().'search_student' ?>">
		 <!-- 	<div class="form-group">
			    <label class="control-label col-sm-2" for="name">Name:</label>
			    <div class="col-sm-10">
			      <input type="text" class="form-control" id="name" name="name" placeholder="Enter name"><span class="text-danger"><?php //echo form_error('name'); ?></span>
			    </div>
		  	</div> -->
		  	 	<div class="form-group">
					<label class="control-label col-sm-2" for="reg_id">Registration ID:</label>
					<div class="col-sm-10"> 
					  <input type="text" class="form-control" id="reg_id" name="reg_id" placeholder="Registration ID"><span class="text-danger"><?php echo form_error('reg_id'); ?></span>
					</div>
				</div>
				<div class="form-group">
					<label class="control-label col-sm-2" for="year">Year:</label>
					<div class="col-sm-10"> 
						<select class="form-control" id="year" name="year">
							<option value="FY">FY</option>
							<option value="SY">SY</option>
							<option value="TY">TY</option>
						</select>
						<span class="text-danger"><?php echo form_error('year'); ?></span>
					</div>
				</div>
				<div class="form-group">
					<label class="control-label col-sm-2" for="specialisation">Specialisation:</label>
					<div class="col-sm-10"> 
						<?php if($specialization): ?>
							<select name="specialisation" id="specialisation" class="form-control">
								<option value="">Specialization</option>
								<?php foreach($specialization as $spec):?>
									<option value="<?php echo $spec->specialization;?>"><?php echo $spec->specialization;?></option>
								<?php endforeach;?>
							</select>
						<?php endif;?>					  
						<span class="text-danger"><?php echo form_error('specialisation'); ?></span>
					</div>
				</div>
  			<div class="form-group">
  				<label class="control-label col-sm-2" for="select">Select:</label>
  				<div class="col-sm-10">
				    <div class="row">
		              <div class="col-xl-3 col-sm-6 mb-3">
		                <div id="fees" class="search_type card text-white bg-primary o-hidden mh-98">
		                  <div class="card-body b-bg">
		                    <div class="card-body-icon">
		                      <i class="fas fa-fw fa-rupee-sign"></i>
		                    </div>
		                    <h5>Fees!</h5>
		                  </div>
		                </div>
		              </div>
		              <div class="col-xl-3 col-sm-6 mb-3">
		                <div id="documents" class="search_type card text-white bg-warning o-hidden mh-98">
		                  <div class="card-body b-bg">
		                    <div class="card-body-icon custom-icon">
		                      <i class="fas fa-fw fa-list"></i>
		                    </div>
		                    <h5>Documents!</h5>
		                  </div>
		                </div>
		              </div>
		              <div class="col-xl-3 col-sm-6 mb-3">
		                <div id="academic_performance" class="search_type card text-white bg-success o-hidden mh-98">
		                  <div class="card-body b-bg">
		                    <div class="card-body-icon">
		                      <i class="fas fa-fw fa-shopping-cart"></i>
		                    </div>
		                    <h5>Academic Performance!</h5>
		                  </div>
		                </div>
		              </div>
		              <div class="col-xl-3 col-sm-6 mb-3">
		                <div id="feedback" class="search_type card text-white bg-danger o-hidden mh-98">
		                  <div class="card-body b-bg">
		                    <div class="card-body-icon">
		                      <i class="fas fa-fw fa-life-ring"></i>
		                    </div>
		                    <h5>Feedback!</h5>
		                  </div>
		                </div>
		              </div>
		            </div>
		        </div>
  			</div>
  			<input type="hidden" name="search" id="search" value="">
		</form>
	  </div>
	  <div class="card-footer small text-muted"></div>
	</div>
</div>
  <!-- /.container-fluid -->
  