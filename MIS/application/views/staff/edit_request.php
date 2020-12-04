<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('staff/staffs'); ?>">Dashboard</a>
		</li>
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('staff/leave_application'); ?>">Leave Request List</a>
		</li>
		<li class="breadcrumb-item active">View Leave Details</li>
    </ol>

    <!-- Area Chart Example-->
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	
	<div class="alert alert-danger print-error-msg" style="display:none"></div>	
	<div class="card mb-3">
		<div class="container editrequest">
		<i class="fas fa-edit"></i><span class="edit_data">Delete</span> Leave Details
			<?php 
			echo form_open(site_url('/staff/requestlist/requestdetails/edit/'.$request['id'])); ?>	
			<div class="row">
				<div class="col-md-12">
					<h3 class="add-info">Leave Information</h3>	
				</div>		
				<div class="col-md-6">
					<div class="form-group">
						<label for="leave_type" class="control-label">Leave Type</label>
						<input type="text" class="form-control" value="<?php echo $request['leave_type']; ?>" readonly>
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group">
						<label for="leave_for" class="control-label">Leave Starting Date</label>
						<input type="text" class="form-control" value="<?php echo $request['leave_from']; ?>" readonly>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="leave_to" class="control-label">Leave End Date</label>
						<input type="text" class="form-control" value="<?php echo $request['leave_to']; ?>" readonly>
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group">
						<label for="leave_for" class="control-label">Reason</label>
						<input type="text" class="form-control" value="<?php echo $request['leave_reason']; ?>" readonly>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="leave_pending_work" class="control-label">Backlog/Pending work</label>
						<input type="text" class="form-control" value="<?php echo $request['leave_pending_work']; ?>" readonly>
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group">
						<label for="leave_hand_given" class="control-label">Hand over given to</label>
						<input type="text" class="form-control" value="<?php echo $request['leave_hand_given']; ?>" readonly>
					</div>
				</div>
				<div class="col-md-6">
					<input type="submit" class="btn btn-danger delete_application" value="Delete Application">
					<div class="print-error-msg text-center" style="color:red;"></div>
				</div>
			</div>
			</div>
			<input type="hidden" name="user_id" value="<?php echo $request['id']; ?>" />    
		</form>
		</div>	
		</div>
	</div>
</div>
  
  
  
<!-- /.container-fluid -->
