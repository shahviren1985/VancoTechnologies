<?php defined('BASEPATH') OR exit('No direct script access allowed');?>
<div id="content-wrapper">
	<div class="<?php echo($this->session->userdata("id")) ? 'container-fluid' : 'container';?>">
		<?php if(isset($_SESSION['error'])){ ?>
		<div class="alert alert-danger alert-dismissible">
			<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
			<strong>Error!</strong> <?php echo $_SESSION['error']; ?>
		</div>
		<?php } ?>
	
		<div class="<?php echo($this->session->userdata("id")) ? 'mb-20' : '';?>">
			<form method="post" id="" enctype="multipart/form-data" action="<?php echo base_url(); ?>pay-online">
			<div class="card <?php echo($this->session->userdata("id")) ? 'mb-20' : '';?>">
				<div class="card-header">
					<i class="far fa-credit-card"></i>
					Examination Payment
				</div>
				<div class="card-body">
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<label for="registration_number">College Registration #: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="registration_number" id="registration_number" value="<?php echo @$auth_student[0]->userID; ?>" <?php echo (!empty($auth_student[0]->userID)) ? 'readonly' : '';?>>
								<span class="text-danger"><?php echo form_error('registration_number'); ?></span>
							</div>
						</div>
					</div>	
					<div class="row">	
						<div class="col-md-5">
							<div class="form-group">
								<label for="admission_year">Admission Year: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="admission_year" id="admission_year" autocomplete="off" value="<?php echo @$auth_student[0]->admission_year; ?>" <?php echo (!empty($auth_student[0]->admission_year)) ? 'readonly' : '';?>>
								<em><small>Year when you got admitted in first year.</small></em>
								<span class="text-danger"><?php echo form_error('admission_year'); ?></span>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-5">
							<div class="form-group student_type_radio">
								<label for="current"><input type="radio" name="student_type" id="current" value="current" <?php echo (!empty($auth_student[0]->userID)) ? 'checked' : '';?>> Current Student</label>
								<span style="margin:0 20px;"></span>
								<label for="alumni"><input type="radio" name="student_type" id="alumni" value="alumni" <?php if($this->session->userdata("id")){echo 'disabled';} else { echo 'checked';};?>> Alumni</label>
							</div>
						</div>	
					</div>
					<div class="row align-items-center">											
						<div class="col-md-5">
							<div class="form-group">
								<label for="first_name">First Name: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="first_name" id="first_name" value="<?php echo @$auth_student[0]->first_name; ?>" <?php echo (!empty($auth_student[0]->first_name)) ? 'readonly' : '';?>>
								<span class="text-danger"><?php echo form_error('first_name'); ?></span>
							</div>
						</div>
						<div class="col-md-5">
							<div class="form-group">
								<label for="last_name">Last Name: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="last_name" id="last_name" value="<?php echo @$auth_student[0]->last_name; ?>" <?php echo (!empty($auth_student[0]->last_name)) ? 'readonly' : '';?>>
								<span class="text-danger"><?php echo form_error('last_name'); ?></span>
							</div>
						</div>
					</div>
					
					<div class="row align-items-center">
						<div class="col-md-5">	
							<div class="form-group">
								<label for="email_address">Email Address: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="email_address" id="email_address" value="<?php echo @$auth_student[0]->email_id; ?>" <?php echo (!empty($auth_student[0]->email_id)) ? 'readonly' : '';?>>
								<span class="text-danger"><?php echo form_error('email_address'); ?></span>
							</div>
						</div>
						<div class="col-md-5">
							<div class="form-group">
								<label for="phone_number">Mobile: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="phone_number" id="phone_number" value="<?php echo @$auth_student[0]->mobile_number; ?>" <?php echo (!empty($auth_student[0]->mobile_number)) ? 'readonly' : '';?>>
								<span class="text-danger"><?php echo form_error('phone_number'); ?></span>
							</div>
						</div>						
					</div>
					<?php if(!$this->session->userdata("id")){?>
					<div class="row">
						<div class="col-md-4">
							<div class="form-group student_field_radio">
								<label for="bachelor"><input type="radio" name="student_field" id="bachelor" value="bachelor" > Bachelors</label>
								<span style="margin:0 20px;"></span>
								<label for="master"><input type="radio" name="student_field" id="master" value="master"> Masters</label>
							</div>
						</div>
					</div>
					<?php }?>
					
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<?php if(!empty($this->session->userdata("id"))){?>
									<label>Course type:</label>
									<input type="text" value="<?php echo $course_name;?>" class="form-control" name="course_type" readonly>
								<?php } else {?>
									<label for="regular"><input type="radio" name="course_type" id="regular" value="Regular" checked> Regular</label>
									<span style="margin:0 20px;"></span>									
									<label for="honors"><input type="radio" name="course_type" id="honors" value="Honors"> Honors</label>									
								<?php }?>
							</div>
						</div>
					</div>
					
					<div class="row align-items-end">
						<div class="col-md-5">
							<div class="form-group">
								<label for="course_year">Course:</label>
								<select name="course_year" id="course_year" class="form-control">
									<option value="">Select year</option>
									<?php foreach($course_year as $year):?>
										<option value="<?php echo $year->year;?>" <?php echo (@$auth_student[0]->course_name == $year->year) ? 'selected="selected"' : ' ';?> <?php echo (!empty($this->session->userdata("id")) && @$auth_student[0]->course_name != $year->year) ? 'disabled' : '';?>><?php echo $year->year;?></option>
									<?php endforeach;?>
								</select>
								<input type="hidden" id="student_course_year" value="<?php echo @$auth_student[0]->course_name;?>" >
								<span class="text-danger"><?php echo form_error('course_year'); ?></span>
							</div>
						</div>
						<div class="col-md-5">
							<div class="form-group">
								<label for="specialisation">Specialisation:</label>
								<?php if(@$auth_student[0]->specialization){?>
									<input type="text" name="specialisation" value="<?php echo $auth_student[0]->specialization;?>" class="form-control" readonly>
									<input type="hidden" name="course_id" value="<?php echo $auth_student[0]->course_id;?>">
								<?php } else {?>
								<select name="specialisation" id="specialisation" class="form-control">
									<option value="">Select Specialisation</option>
									<?php foreach($course_spec as $spec):?>
										<option value="<?php echo $spec->short_form;?>">
											<?php echo $spec->specialization;?>
										</option>
									<?php endforeach;?>
								</select>
								<span class="text-danger"><?php echo form_error('specialisation'); ?></span>
								<?php }?>								
							</div>
						</div>						
					</div>
					<input type="hidden" name="caste" id="caste" value="<?php echo (!empty($this->session->userdata("id"))) ? $auth_student[0]->caste : 'Open';?>">
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<label for="application_type">Application Type: <span class="required-mark">*</span></label>
								<select name="application_type" id="application_type" class="form-control" >
									<option value="">Select Application</option>
									<?php foreach($application_fees as $app):?>
										<option value="<?php echo $app->document_name;?>" data-amount="<?php echo $app->amount;?>" data-quantity="<?php echo $app->quantity;?>" data-variants="<?php echo $app->variants;?>"><?php echo $app->document_name;?></option>
									<?php endforeach;?>
								</select>
								<span class="text-danger"><?php echo form_error('application_type'); ?></span>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<label for="app_fees_quantity">Quantity</label>
								<input type="number" class="form-control" id="app_fees_quantity" min="1" step="1" name="app_fees_quantity" value="1" onkeydown="return false">
							</div>
						</div>
					</div>
					<!--
					<div class="row">						
						<div class="col-md-4">
							<div class="form-group">
								<label for="app_fees_amount">Fee Amount: </label>
								<p><span id="app_fees_amount" class="price_box form-control">0.00</span></p>
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="online_processing_fee">Online Processing Fee:</label>
								<p><span id="online_processing_fee" class="price_box form-control">0.00</span><em><small>Processing Fee: 2%</small></em></p>
							</div>
						</div>			
						<div class="col-md-4">
							<div class="form-group">
								<label for="gst_fee">GST:</label>
								<p><span id="gst_fee" class="price_box form-control">0.00</span><em><small>GST: 18%</small></em></p>
							</div>
						</div>							
					</div>-->
					
					<table class="table">
						<tbody class="aliceblue">
							<tr>
								<th>Fee Head Total</th>
								<th>₹ <span id="app_fees_amount">0.00</span></th>
							</tr>
							<tr>
								<td>Online Payment Processing Charge (2%)</td>
								<td>₹ <span id="online_processing_fee">0.00</span></td>
							</tr>
							<tr>
								<td>GST (18%)</td>
								<td>₹ <span id="gst_fee">0.00</span></td>
							</tr>
							<tr>
								<th>Grand Total</th>
								<th>₹ <span id="total_app_amount">0.00</span></th>
							</tr>
						</tbody>
					</table>
					<input type="hidden" id="fees_amount" name="fees_amount">
					<input type="hidden" id="online_amount" name="online_amount">
					<input type="hidden" id="total_amount" name="total_amount"> 
					<div class="row align-items-end">
						<!--<div class="col-md-4">
							<div class="form-group">
								<label for="total_app_amount">Total Amount: </label>
								<p><span id="total_app_amount" class="price_box form-control">0.00</span></p>
							</div>
						</div>
						-->
						<div class="col-md-4">
							<div class="form-group">
								<button type="submit" class="btn btn-success">Make Payment</button>
							</div>
							<p><a href="https://svt.edu.in/refund-policy.html" target="_blank">Refund Policy</a></p>
						</div>
					</div>
				</div>
			</div>
			</form>
		</div>
	 </div>