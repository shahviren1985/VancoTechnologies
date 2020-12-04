<?php defined('BASEPATH') OR exit('No direct script access allowed'); 
//echo "<pre>"; print_r($staff_details); echo "</pre>";
?>
<div id="content-wrapper">
  <div class="container-fluid">
   
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('staff/staffs'); ?>">Home</a>
      </li>
    </ol>
    <?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	<?php $flash_msg = $this->session->flashdata('msg');
    	if(isset($flash_msg)){ ?>
        <div class="alert alert-success alert-dismissible alertbox">
          <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
           <?php echo $flash_msg;?>.
        </div>
    <?php } ?>
	
	<div class="alert alert-danger print-error-msg  alert-dismissible" style="display:none"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span id="msg"></span> </div>	
    <?php 
		$firstname = $staff_details[0]->firstname;
		$lastname = $staff_details[0]->lastname;
		$username = $staff_details[0]->username;
		$email = $staff_details[0]->email;
		$employee_code = $staff_details[0]->employee_code;
		$department = $staff_details[0]->department;
		$type = $staff_details[0]->type;
		$state = $staff_details[0]->state;
		$mobile_number = $staff_details[0]->mobile_number;
		if(!empty($staff_details[0]->date_of_joining) && $staff_details[0]->date_of_joining != '0000-00-00'){
			$date_of_joining =  date('d-m-Y',strtotime($staff_details[0]->date_of_joining));
		}else{
			$date_of_joining = "";
		}
		
		if(!empty($staff_details[0]->date_of_retire) && $staff_details[0]->date_of_retire != '0000-00-00'){
			$date_of_retire =  date('d-m-Y',strtotime($staff_details[0]->date_of_retire));
		}else{
			$date_of_retire = "";
		}
		if(empty($staff_details[0]->total_experience) || $staff_details[0]->total_experience == ''){
			$staff_details[0]->total_experience = 0;
		}
		$qualification = $staff_details[0]->qualification;
		$role = $staff_details[0]->role;
		$date1 = date('Y-m-d');
		if($staff_details[0]->created_at == '0000-00-00' && $staff_details[0]->updated_at != '0000-00-00'){
			$date1 = date($staff_details[0]->updated_at);
		}elseif($staff_details[0]->created_at != '0000-00-00' && $staff_details[0]->updated_at == '0000-00-00'){
			$date1 = date($staff_details[0]->created_at);
		}elseif($staff_details[0]->created_at == '0000-00-00' && $staff_details[0]->updated_at == '0000-00-00'){
			$date1 = date('Y-m-d');
		}elseif($staff_details[0]->created_at == '' && $staff_details[0]->updated_at == ''){
			$date1 = date('Y-m-d');
		}
		
		$date2 = date('Y-m-d');
		$date_diff = strtotime($date2)-strtotime($date1);
		$differnce_in_months = floor(($date_diff)/2628000);
		if($differnce_in_months > 0){
			$total_experience = $staff_details[0]->total_experience + $differnce_in_months;	
		}else{
			$total_experience = $staff_details[0]->total_experience;
		}
		
		$industry_experience = $staff_details[0]->industry_experience;
		$pan_number = $staff_details[0]->pan_number;
		$aadhaar_number = $staff_details[0]->aadhaar_number;
		$bank_account_number = $staff_details[0]->bank_account_number;
		$bank_name = $staff_details[0]->bank_name;
		$ifsc_code = $staff_details[0]->ifsc_code;
		$account_holder_name = $staff_details[0]->account_holder_name;
		$account_holder_name = $staff_details[0]->account_holder_name;
		$casual_leave_balance = $staff_details[0]->casual_leave_balance;
		$sick_leave_balance = $staff_details[0]->sick_leave_balance;
		$paid_leave_balance = $staff_details[0]->paid_leave_balance;
    ?>
	
    <div class="container">
	<i class="fas fa-edit"></i><span class="edit_data">Edit</span> Your Details
		<span id="edit_data">Edit</span>
		<div class="row float-right" style="margin-right: 10px;">
			<button type="button" data-toggle="modal" data-target="#changePassModal" class="btn btn-info btn-sm text-center">Change Password</button>
		</div>
	<div class="text-muted text_disabled" style="margin-top: 20px">		
	<?php $attributes = array('id' => 'update_staff_details');
				echo form_open_multipart(site_url('/staff/details/edit/'.$staff_details[0]->id), $attributes); ?>
      <div class="row">
        <div class="col-md-12 col-sm-12">
			<div class="row">
			  <div class="col-md-6">
				<div class="form-group">
				  <label for="name">First Name:</label>
				  <input type="text" class="form-control" id="firstname" name="firstname" value="<?php echo $firstname; ?>" readonly>
				</div>
			  </div>	
			  <div class="col-md-6">
				<div class="form-group">
				  <label for="name">Last Name:</label>
				  <input type="text" class="form-control" id="lastname" name="lastname" value="<?php echo $lastname; ?>" readonly>
				</div>
			  </div> 
			  <div class="col-md-6">
				<div class="form-group">
				  <label for="name">User Name:</label>
				  <input type="text" class="form-control" id="username" name="username" value="<?php echo $username; ?>" readonly>
				</div>
			  </div>
			  <div class="col-md-6">
				<div class="form-group">
				  <label for="email">Email:</label>
				  <input type="email" class="form-control" id="mail" name="email" value="<?php echo $email; ?>" data-rule-required="true">
				</div>
			  </div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
					  <label for="employee_code">Employee Code:</label>
					  <input type="text" class="form-control" id="employee_code" name="employee_code" value="<?php echo $employee_code; ?>" readonly>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
					  <label for="type">Type:</label>
					  <input type="text" class="form-control" id="type" name="type" value="<?php echo $type; ?>" readonly>
					</div>
				</div>
			</div>	
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
					  <label for="department">Department:</label>
					  <input type="text" class="form-control" id="department" name="department" value="<?php echo $department; ?>" readonly>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
					  <label for="state">State:</label>
					  <input type="text" class="form-control" id="state" name="state" value="<?php echo $state; ?>" data-rule-required="true">
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
					  <label for="mobile_number">Mobile Number:</label>
					  <input type="text" class="form-control" id="mobile_number" name="mobile_number" value="<?php echo $mobile_number; ?>" data-rule-required="true">
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
					  <label for="joining_date">Date of Joining:</label>
					  <input type="text" class="form-control" id="joining_date" name="joining_date" value="<?php echo $date_of_joining; ?>" data-rule-required="true">
					</div>
				</div>
			</div>	
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
					  <label for="retire_date">Date of Retire:</label>
					  <input type="text" class="form-control" id="retire_date" name="retire_date" value="<?php echo $date_of_retire; ?>" data-rule-required="true">
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
					  <label for="qualification">Qualification:</label>
					  <input type="text" class="form-control" id="qualification" name="qualification" value="<?php echo $qualification; ?>" data-rule-required="true">
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="total_experience" class="control-label">Total Teaching Experience (In months) </label>
						<input type="number" name="total_experience" class="form-control" id="total_experience" value="<?php echo $total_experience; ?>" data-rule-required="true" />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="industry_experience" class="control-label">Total Industry Experience (In months)</label>
						<input type="number" name="industry_experience" class="form-control" id="industry_experience" value="<?php echo $industry_experience; ?>" data-rule-required="true" />
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
					  <label for="role">Role:</label>
					  <input type="text" class="form-control" id="role" name="role" value="<?php echo $role; ?>" readonly>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="pan" class="control-label">Pan Card Number</label>
						<input type="text" name="pan" id="pan" class="form-control" value="<?php echo $pan_number; ?>" data-rule-required="true" >
					</div>
				</div>	
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
						<label for="aadhaar" class="control-label">Aadhaar Number</label>
						<input type="text" name="aadhaar" id="aadhaar" class="form-control" value="<?php echo $aadhaar_number; ?>" data-rule-required="true" >
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="account_number" class="control-label">Bank Account Number</label>
						<input type="text" name="account_number" id="account_number" class="form-control" value="<?php echo $bank_account_number; ?>" data-rule-required="true" >
					</div>
				</div>	
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
						<label for="bank_name" class="control-label">Bank Name</label>
						<input type="text" name="bank_name" id="bank_name" class="form-control" value="<?php echo $bank_name; ?>" data-rule-required="true" >
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="ifsc_code" class="control-label">IFSC Code</label>
						<input type="text" name="ifsc_code" id="ifsc_code" class="form-control" value="<?php echo $ifsc_code; ?>" data-rule-required="true" >
					</div>
				</div>	
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
						<label for="account_holder_name" class="control-label">Account Holder Name</label>
						<input type="text" name="account_holder_name" id="account_holder_name" class="form-control" value="<?php echo $account_holder_name; ?>" data-rule-required="true" >
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="casual_balance" class="control-label">Casual Leave Balance</label>
						<input type="text" name="casual_balance" class="form-control" id="casual_balance" value="<?php if($casual_leave_balance > 0){echo $casual_leave_balance; }else{ echo 0; } ?>" readonly />
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="sick_balance" class="control-label">Sick Leave Balance</label>
						<input type="text" name="sick_balance" class="form-control" id="sick_balance" value="<?php if($sick_leave_balance > 0){ echo $sick_leave_balance; }else{ echo 0; } ?>" readonly />
					</div>
				</div>	
				<div class="col-md-6">
					<div class="form-group"> 
						<label for="paid_balance" class="control-label">Paid Leave Balance</label>
						<input type="text" name="paid_balance" class="form-control" id="paid_balance" value="<?php if($paid_leave_balance > 0){ echo $paid_leave_balance; }else{echo 0; } ?>" readonly />
					</div>
				</div>
			</div>
			<input type="hidden" name="user_id" value="<?php echo $staff_details[0]->id; ?>" />
            <button type="submit" class="btn btn-success">Submit</button>
        </div>
        <!-- <div class="col-md-4 col-sm-12">
          <div class="img_wrap">
            <img src="https://cdn.pixabay.com/photo/2015/03/04/22/35/head-659652_960_720.png" width="200" height="200">
          </div>
        </div> -->
      </div>
  </form>
  </div>
    </div>

  </div>
  <!-- <a id="up" data-target="#basic_details" data-toggle="modal" data-backdrop="static" data-keyboard="false"></a>` -->
  <!-- /.container-fluid -->
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
							<input type="hidden" name="cur_user_id" value="<?php echo $userID; ?>">
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
<script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
  