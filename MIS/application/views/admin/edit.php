<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>

<div id="content-wrapper">

  	<div class="container-fluid">
  		<?php echo $this->session->tempdata('message'); ?>
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url("home"); ?>">Dashboard</a>
			</li>
			<li class="breadcrumb-item active">Edit Profile</li>
        </ol>

        <!-- DataTables Example -->
          <div class="card mb-3">
            <div class="card-header">
				<i class="fas fa-industry"></i>
				Edit Profile
			 </div>
            <div class="card-body">
				<form method="post" action="<?php echo base_url("home/profile/update/".$user[0]->id); ?>">
					<div class="form-row">
						<div class="col-md-6 mb-3">
							<label for="first-name">First Name*</label>
							<input type="text" class="form-control" name="first_name" placeholder="First Name" value="<?php echo $user[0]->first_name; ?>">
							<span class="text-danger"><?php echo form_error('first_name'); ?></span>
						</div>
						<div class="col-md-6 mb-3">
							<label for="last-name">Last Name*</label>
							<input type="text" class="form-control" name="last_name" placeholder="Last Name" value="<?php echo $user[0]->last_name; ?>">
							<span class="text-danger"><?php echo form_error('last_name'); ?></span>
						</div>
					</div>
					<div class="form-row">
						<div class="col-md-6 mb-3">
							<label for="first-name">Email*</label>
							<input type="text" class="form-control" name="email" placeholder="Email" value="<?php echo $user[0]->email; ?>" readonly>
							<span class="text-danger"><?php echo form_error('email'); ?></span>
						</div>
					</div>

					<div class="form-row">
						<div class="col-md-6 mb-3">
							<label for="first-name">Password*</label>
							<input type="text" class="form-control cap" name="password" placeholder="Password" value="">
							<span class="text-danger"><?php echo form_error('password'); ?></span>
						</div>
						<div class="col-md-6 mb-3">
							<label for="last-name">Confirm Password*</label>
							<input type="ac_no" class="form-control" name="c_password" placeholder="Confirm Password" value="">
							<span class="text-danger"><?php echo form_error('c_password'); ?></span>
						</div>
					</div>
					<button class="btn btn-success" type="submit">Save</button>
					</form>
				</div>
		  </div>