<?php defined('BASEPATH') OR exit('No direct script access allowed');?>
<div id="content-wrapper">
	<div class="container-fluid">
		<?php if(isset($_SESSION['error'])){ ?>
		<div class="alert alert-danger alert-dismissible">
			<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
			<strong>Error!</strong> <?php echo $_SESSION['error']; ?>
		</div>
		<?php } ?>
<?php
	$subject_fees = 750 * $addon_subject[0]->credit;
	$online_charge = ($subject_fees/100) * 2;
	$gst_charge = ($online_charge/100) * 18;
	$total_charge = $subject_fees + $online_charge + $gst_charge;

?>	
		<div class="mb-20">
			<form method="post" id="" enctype="multipart/form-data" action="<?php echo base_url(); ?>student/pay-online">
			<div class="card mb-20">
				<div class="card-header">
					<i class="far fa-credit-card"></i>
					Subject Fee Payment
				</div>
				<div class="card-body">
					<div class="row align-items-end">
						<div class="col-md-6">
							<div class="form-group">
								<label for="registration_number">College Registration #: <span class="required-mark">*</span></label>
								<input type="text" class="form-control" name="registration_number" id="registration_number" value="<?php echo $this->session->userdata("userID"); ?>" readonly>
								<span class="text-danger"><?php echo form_error('registration_number'); ?></span>
							</div>
						</div>
					
						<!--<div class="col-md-6">		
							<div class="form-group student_field_radio">
								<label for="bachelor">Bachelors: </label>
								<input type="radio" name="student_field" id="bachelor" value="bachelor" checked>								
								<label for="master">Masters: </label>
								<input type="radio" name="student_field" id="master" value="master">
							</div>
						</div>-->							
					</div>					
					
					<div class="row align-items-end">
						<div class="col-md-4">
							<div class="form-group">
								<label for="semester">Semester:</label>
								<input type="text" class="form-control" name="semester" id="semester" value="<?php echo $addon_subject[0]->semester; ?>" readonly>
								<!-- <select name="semester" id="semester" class="form-control">
									<option value="">Select Sem</option>
									<option value="3">Sem 3</option>
									<option value="4">Sem 4</option>
									<option value="5">Sem 5</option>
									<option value="6">Sem 6</option>
								</select> -->
								<span class="text-danger"><?php echo form_error('semester'); ?></span>
							</div>
							<input type="hidden" id="spec_shortform" name="spec_shortform" value="<?php echo $course_details[0]->short_form;?>">
						</div>
						<div class="col-md-5">
							<div class="form-group">
								<label for="subject">Subject:</label>
								<input type="text" class="form-control" name="subject" id="subject" value="<?php echo $addon_subject[0]->subject; ?>" readonly>
								<!-- <select name="subject" id="subject" class="form-control">
									<option value="">Select Subject</option> 
								</select> -->
								<span class="text-danger"><?php echo form_error('subject'); ?></span>
							</div>
						</div>
						
						<div class="col-md-3">
							<div class="form-group">
								<label for="credits">Credits:</label>
								<input type="text" name="credits" id="sub_credits" class="form-control" value="<?php echo $addon_subject[0]->credit; ?>" readonly>
							</div>
						</div>
					</div>
					
					<table class="table">
						<tbody class="aliceblue">
							<tr>
								<th>Fee Head Total</th>
								<th>₹ <span id="sub_fees_amount"><?php echo number_format($subject_fees,'2'); ?></span></th>
							</tr>
							<tr>
								<td>Online Payment Processing Charge</td>
								<td>₹ <span id="online_processing_fee">
									<?php
									$online_charge = $online_charge + $gst_charge;
									 echo number_format($online_charge,'2'); ?></span></td>
							</tr>
							<!-- <tr>
								<td>GST (18%)</td>
								<td>₹ <span id="gst_fee"><?php echo number_format($gst_charge,'2'); ?></span></td>
							</tr> -->
							<tr>
								<th>Grand Total</th>
								<th>₹ <span id="total_app_amount"><?php echo number_format($total_charge,'2'); ?></span></th>
							</tr>
						</tbody>
					</table>
					
					<?php /*<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label for="sub_fees_amount">Fee Amount: </label>
								<p><span id="sub_fees_amount" class="form-control">0.00</span></p>
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
					</div>
					<hr>*/?>
					
					<div class="row align-items-end">
						<input type="hidden" id="subject_fees" name="subject_fees" value="<?php echo $subject_fees; ?>">
						<input type="hidden" id="online_amount" name="online_amount" value="<?php echo number_format($online_charge,'2'); ?>">
						<input type="hidden" id="total_amount" name="total_amount" value="<?php echo number_format($total_charge,'2'); ?>"> 
						<input type="hidden" id="student_field" name="student_field" value="bachelor"> 
						<input type="hidden" id="subject_id" name="subject_id" value="<?php echo $addon_subject[0]->id; ?>"> 
						<div class="col-md-4">
							<div class="form-group">
								<button type="submit" class="btn btn-success">Make Payment</button>
							</div>
						</div>
					</div>
				</div>
			</div>
			</form>
		</div>
	 </div>