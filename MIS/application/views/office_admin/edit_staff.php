<?php defined('BASEPATH') OR exit('No direct script access allowed'); 
if(!empty($staff['date_of_joining']) && $staff['date_of_joining'] != '0000-00-00'){
	$date_of_joining = date('d-m-Y',strtotime($staff['date_of_joining']));
}else{
	$date_of_joining = "";
}

if(!empty($staff['date_of_retire']) && $staff['date_of_retire'] != '0000-00-00'){
	$date_of_retire = date('d-m-Y',strtotime($staff['date_of_retire']));
}else{
	$date_of_retire = "";
}


?>
<style>
.semster-tabs{
	margin-top:20px;
}
</style>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
		</li>
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('officeadmin/export-staff-list'); ?>">Staff List</a>
		</li>
		<li class="breadcrumb-item active"> Edit Staff Details</li>
    </ol>

    <!-- Area Chart Example-->
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	
	<div class="alert alert-danger print-error-msg  alert-dismissible" style="display:none">
		<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
			<span id="msg"></span> 
	</div>		
	<div class="card mb-3">
		<div class="container editstaff">
		<i class="fas fa-edit"></i><span class="edit_data">Edit</span> Staff Details <span id="edit_data">Edit</span>
		<div class="row float-right" style="margin-right: 10px;">
			<button type="button" data-toggle="modal" data-target="#changePassModal" class="btn btn-info btn-sm text-center">Change Password</button>
		</div>
		<div class="small text-muted text_disabled">
			<?php if( isset($staff) && count($staff) ) {?>			
			<?php $attributes = array('id' => 'update_staff_details');
			echo form_open_multipart(site_url('/officeadmin/stafflist/staffdetails/edit/'.$staff['id']), $attributes); ?>	
			<div class="row">
				<div class="col-md-12">
					<h3 class="add-info">Staff Information</h3>	
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="employee_code" class="control-label">Employee Code</label>
						<input type="text" name="employee_code" data-old-reg="<?php echo $staff['employee_code'] ?>" id="employee_code" class="form-control" value="<?php echo $staff['employee_code'] ?>" readonly />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="member_name" class="control-label">First Name</label>
						<input type="text" name="member_name" class="form-control" value="<?php echo $staff['firstname'] ?>" data-rule-required="true" data-rule-maxlength="3" data-rule-number="true" />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="member_lname" class="control-label">Last Name</label>
						<input type="text" name="member_lname" class="form-control" value="<?php echo $staff['lastname'] ?>" data-rule-required="true" data-rule-maxlength="3" data-rule-number="true" />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="username" class="control-label">User Name</label>
						<input type="text" name="username" class="form-control" value="<?php echo $staff['username'] ?>" data-rule-required="true" data-rule-maxlength="3" data-rule-number="true" />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="type" class="control-label">Type</label>
						<select name="type" data-rule-required="true" id="type" class="form-control">
							<option value="">Select Type</option>
							<option value="Permanent Teaching" <?php echo ($staff['type'] == 'Permanent Teaching') ? 'selected="selected"' : ''; ?>>Permanent Teaching</option>
							<option value="Permanent Non Teaching" <?php echo ($staff['type'] == 'Permanent Non Teaching') ? 'selected="selected"' : ''; ?>>Permanent Non Teaching</option>
							<option value="Contract" <?php echo ($staff['type'] == 'Contract') ? 'selected="selected"' : ''; ?>>Contract</option>
							<option value="Visiting Teaching" <?php echo ($staff['type'] == 'Visiting Teaching') ? 'selected="selected"' : ''; ?>>Visiting Teaching</option>
							<option value="Visiting Non Teaching" <?php echo ($staff['type'] == 'Visiting Non Teaching') ? 'selected="selected"' : ''; ?>>Visiting Non Teaching</option>
						</select>
					</div>	
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="email" class="control-label">Email</label>
						<input type="text" name="email" class="form-control" value="<?php echo $staff['email'] ?>" data-rule-required="true" />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="department" class="control-label">Department</label>
						<select name="department" data-rule-required="true" id="department" class="form-control">
							<option value="">Select Department</option>
							<option value="HD" <?php echo ($staff['department'] == 'HD') ? 'selected="selected"' : ''; ?>>Human Development</option>
							<option value="RM" <?php echo ($staff['department'] == 'RM') ? 'selected="selected"' : ''; ?>>Resource Management</option>
							<option value="FND" <?php echo ($staff['department'] == 'FND') ? 'selected="selected"' : ''; ?>>Food, Nutrition and Dietetics</option>
							<option value="MCE" <?php echo ($staff['department'] == 'MCE') ? 'selected="selected"' : ''; ?>>Mass Communications and Extensions</option>
							<option value="TAD" <?php echo ($staff['department'] == 'TAD') ? 'selected="selected"' : ''; ?>>Textile & Apparel Designing</option>
							
							<option value="SCI" <?php echo ($staff['department'] == 'SCI') ? 'selected="selected"' : ''; ?>>Science</option>
							<option value="ENG" <?php echo ($staff['department'] == 'ENG') ? 'selected="selected"' : ''; ?>>English</option>
							<option value="OA" <?php echo ($staff['department'] == 'OA') ? 'selected="selected"' : ''; ?>>Office Administration</option>
						</select>
					</div>	
				</div>				
				<div class="col-md-6">
					<div class="form-group">
						<label for="type" class="control-label">State</label>
						<select name="state" data-rule-required="true" id="state" class="form-control">
							<option value="">Select State</option>
							<option value="Maharashtra" <?php echo ($staff['state'] == 'Maharashtra') ? 'selected="selected"' : ''; ?>>Maharashtra</option>
							<option value="Outside Maharashtra" <?php echo ($staff['type'] == 'Outside Maharashtra') ? 'selected="selected"' : ''; ?>>Outside Maharashtra</option>
						</select>
					</div>	
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="mobile_number" class="control-label">Mobile Number</label>
						<input type="text" name="mobile_number" class="form-control" value="<?php echo $staff['mobile_number'] ?>" data-rule-required="true" />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="joining_date" class="control-label">Date of Joining</label>
						<input type="text" data-rule-required="true" name="joining_date" id="joining_date" class="form-control" value="<?php echo $date_of_joining; ?>" />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="retire_date" class="control-label">Date of Retire</label>
						<input type="text" data-rule-required="true" name="retire_date" id="retire_date" class="form-control" value="<?php echo $date_of_retire; ?>" />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="qualification" class="control-label">Qualification</label>
						<input type="text" name="qualification" class="form-control" id="qualification" value="<?php echo $staff['qualification'] ?>" data-rule-required="true" />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group"> 
						<?php 
							if($staff['created_at'] == '0000-00-00' && $staff['updated_at'] != '0000-00-00'){
								$date1 = date($staff['updated_at']);
							}elseif($staff['created_at'] != '0000-00-00' && $staff['updated_at'] == '0000-00-00'){
								$date1 = date($staff['created_at']);
							}elseif($staff['created_at'] != '0000-00-00' && $staff['updated_at'] != ''){
								$date1 = date($staff['created_at']);
							}else{
								$date1 = date('Y-m-d');
							}
							if(empty($staff['total_experience']) || $staff['total_experience'] == ''){
								$staff['total_experience'] = 0;
							}
							$date2 = date('Y-m-d');
							$date_diff = strtotime($date2)-strtotime($date1);
							$differnce_in_months = floor(($date_diff)/2628000);
							if($differnce_in_months > 0){
								$total_experience = $staff['total_experience'] + $differnce_in_months;	
							}else{
								$total_experience = $staff['total_experience'];
							}
						?>
						<label for="total_experience" class="control-label">Total Teaching Experience (In months) </label>
						<input type="number" name="total_experience" class="form-control" id="total_experience" value="<?php echo $total_experience; ?>" />
					</div>
				</div>	<div class="col-md-6">
					<div class="form-group"> 
						<label for="industry_experience" class="control-label">Total Industry Experience (In months)</label>
						<input type="number" name="industry_experience" class="form-control" id="industry_experience" value="<?php echo $staff['industry_experience'] ?>" />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="role" class="control-label">Role</label>
						<select class="form-control" name="role" id="role" data-rule-required="true">
							<option value="">Select Role</option>
							<option value="Principal" <?php echo ($staff['role'] == 'Principal')? 'selected="selected"' : ''; ?>>Principal</option>
							<option value="Vice Principal" <?php echo ($staff['role'] == 'Vice Principal')? 'selected="selected"' : ''; ?>>Vice Principal</option>
							<option value="Office Supretendent" <?php echo ($staff['role'] == 'Office Supretendent')? 'selected="selected"' : ''; ?>>Office Supretendent</option>
							<option value="CoE" <?php echo ($staff['role'] == 'CoE')? 'selected="selected"' : ''; ?>>CoE</option>
							<option value="HOD" <?php echo ($staff['role'] == 'HOD')? 'selected="selected"' : ''; ?>>HOD</option>
							<option value="Teaching Staff" <?php echo ($staff['role'] == 'Teaching Staff')? 'selected="selected"' : ''; ?>>Teaching Staff</option>
							<option value="Non-teaching Staff" <?php echo ($staff['role'] == 'Non-teaching Staff')? 'selected="selected"' : ''; ?>>Non-teaching Staff</option>
						</select>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="pan" class="control-label">PAN Card Number</label>
						<input type="text" name="pan" id="pan" class="form-control" value="<?php echo $staff['pan_number']; ?>">
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group">
						<label for="aadhaar" class="control-label">Aadhar Number</label>
						<input type="text" name="aadhaar" id="aadhaar" class="form-control" value="<?php echo $staff['aadhaar_number']; ?>">
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="account_number" class="control-label">Bank Account Number</label>
						<input type="text" name="account_number" id="account_number" class="form-control" value="<?php echo $staff['bank_account_number']; ?>">
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group">
						<label for="bank_name" class="control-label">Bank Name</label>
						<input type="text" name="bank_name" id="bank_name" class="form-control" value="<?php echo $staff['bank_name']; ?>">
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="ifsc_code" class="control-label">IFSC Code</label>
						<input type="text" name="ifsc_code" id="ifsc_code" class="form-control" value="<?php echo $staff['ifsc_code']; ?>">
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group">
						<label for="account_holder_name" class="control-label">Account Holder Name</label>
						<input type="text" name="account_holder_name" id="account_holder_name" class="form-control" value="<?php echo $staff['account_holder_name']; ?>">
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="casual_balance" class="control-label">Casual Leave Balance</label>
						<input type="text" name="casual_balance" class="form-control" id="casual_balance" value="<?php if($staff['casual_leave_balance'] > 0){echo $staff['casual_leave_balance']; }else{ echo 0; } ?>" data-rule-required="true" />
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="sick_balance" class="control-label">Sick Leave Balance</label>
						<input type="text" name="sick_balance" class="form-control" id="sick_balance" value="<?php if($staff['sick_leave_balance'] > 0){ echo $staff['sick_leave_balance']; }else{ echo 0; } ?>" data-rule-required="true" />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="paid_balance" class="control-label">Paid Leave Balance</label>
						<input type="text" name="paid_balance" class="form-control" id="paid_balance" value="<?php if($staff['paid_leave_balance'] > 0){ echo $staff['paid_leave_balance']; }else{echo 0; } ?>" data-rule-required="true" />
					</div>
				</div>
				<div class="col-md-12">
					<div class="form-group">
						<input type="submit" name="submit" class="btn btn-info btn-md" value="Save Staff Details" />
					</div>
				</div>
			</div>
			<input type="hidden" name="user_id" value="<?php echo $staff['id']; ?>" />    
		</form>
		<?php }?>
		</div>	
		</div>
	</div>
</div>
  
  
  
	<div class="modal fade" id="changePassModal" tabindex="-1" role="dialog" aria-labelledby="changePassModal" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Change Password</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="change_password">
						<div class="row">					
							<div class="col-md-12">
								<div class="form-group">
									<input type="password" name="new_password" id="new_password" class="form-control" placeholder="New Password" />
								</div>
							</div>
							<div class="col-md-12">
								<div class="form-group">
									<input type="password" name="conf_password" class="form-control" placeholder="Confirm Password" />
								</div>
							</div>
							<input type="hidden" name="cur_user_id" value="<?php echo $staff['username']; ?>">
							<div class="col-md-12">
								<div class="form-group">
									<input type="button" class="btn btn-primary" name="change_pass" id="change_pass" value="Change password">
								</div>
							</div>					
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>	
	
	<div class="modal fade" id="changePhotoModal" tabindex="-1" role="dialog" aria-labelledby="changePhotoModal" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Change Photo</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="change_profile_photo" enctype="multipart/form-data">
						<div class="row">					
							<div class="col-md-12">
								<div class="form-group">
									<input type="file" name="new_photo" id="new_photo" />
								</div>
							</div>
							<input type="hidden" name="old_photo" value="<?php echo $student['photo_path']; ?>">
							<input type="hidden" name="student_academic_year" value="<?php echo $student['academic_year']; ?>">
							<input type="hidden" name="cur_user_id" value="<?php echo $student['userID']; ?>">
							<div class="col-md-12">
								<div class="form-group">
									<input type="button" class="btn btn-primary" name="change_photo" id="change_photo" value="Upload Photo">
								</div>
							</div>					
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>
	<!-- /.container-fluid -->
