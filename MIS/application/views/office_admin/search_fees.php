<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
	.mh-98 { min-height: 98px !important; }
	.custom-icon { opacity: 0.3 !important; font-size: 4rem !important; }
	.b-bg { background: #0000005e; }
</style>

<?php 
	$count = 0;
	$fee_head_total = 0;
	$pay_percentage = 0;
	$gst = 0;
	$grand_total = 0;

	$category = strtolower($student_details[0]->caste_category);
	$caste = strtolower($student_details[0]->caste);
	$student_name = $student_details[0]->first_name.' '.$student_details[0]->middle_name.' '.$student_details[0]->last_name;
	$course = $course_details[0]->course_type;
	$course_id = $course_details[0]->id;
	$specialization = $course_details[0]->specialization;
	$year = $course_details[0]->year;
	$nri = $student_details[0]->nri;
	if($nri){
		$permanent_country = strtoupper(trim($student_details[0]->permanent_country));
	}
	$saarc = array('Afghanistan', 'Bangladesh', 'Bhutan', 'India', 'Sri Lanka', 'Maldives', 'Nepal', 'Pakistan');
	$saarc_upper = array_change_key_case($saarc,CASE_UPPER);

	$late_fee = $this->search_model->get_late_fees_amount();
	//die;
?>


<div id="content-wrapper">
  <div class="container-fluid">
	<p><a href="<?php echo $_SERVER['HTTP_REFERER'];?>#accounts"><i class="fa fa-angle-double-left"></i> Back to Student Account</a></p>
	<!-- Breadcrumbs-->
	<ol class="breadcrumb">
	  <li class="breadcrumb-item">
		<a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
	  </li>
	  <li class="breadcrumb-item active">Fees</li>
	</ol>

	<?php if(isset($_SESSION['validate_error'])){ ?>
	<div class="alert alert-danger alert-dismissible">
	  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
	  <strong>Error!</strong> <?php echo $_SESSION['validate_error']; ?>
	</div>
	<?php } ?>
	
	
	<div class="card mb-3">
		<div class="card-header">
			<i class="fas fa-search"></i>Fees
		</div>
		
		<div class="card-body">
			<form method="post" id="fees_form" target="_blank" action="<?php echo base_url(); ?>generate_challan">
			<div class="row">
				<div class="col-md-6">	
					<div class="form-group">
						<label class="control-label" for="name">Name</label> 
						<input type="text" class="form-control" disabled id="name" name="name" placeholder="Name" value="<?php echo $student_name ?>"><span class="text-danger"><?php echo form_error('name'); ?></span>
					</div>
				</div>
				<div class="col-md-6">
					<div class="form-group">
						<label for="select_reserved" class="control-label">Is Reserved:</label>
						<select class="form-control" id="reserved" disabled>
						<?php 
						if($category == 'open'){
							echo '<option>No</option>';
						} elseif($category == 'reserved' && ($caste == 'sc' || $caste == 'st')) {
							echo '<option>Yes</option>';
						} elseif($category == 'reserved' && ($caste != 'sc' || $caste != 'st')){
							echo '<option>Yes</option>';
						}?>
						</select>
					</div>
				</div>
			</div>

			<div class="row">
				<div class="col-md-6">
					<div class="form-group">
						<label class="control-label" for="year">Year / Specialisation:</label>
						  <input type="text" class="form-control" id="year" name="year" placeholder="Year" value="<?php echo $year .' / '. $specialization ?>" disabled><span class="text-danger"><?php echo form_error('year'); ?></span>
					</div>
				</div>
				
				<div class="col-md-6">
					<div class="form-group">
						<label for="payment_method" class="control-label">Payment Method:</label>
						<select class="form-control" id="payment_method" name="payment_method">
							<option value="Cash">Cash</option>
							<option value="Cheque">Cheque</option>
							<option value="DD">DD</option>
						</select><span class="text-danger"><?php echo form_error('payment_method'); ?></span>
					</div>
				</div>
			</div>


			<div class="row cheque_data" style="display:none;">
				<!--   <div class="form-group">
					<label class="control-label col-sm-2" for="payment_method">Payment Method:</label>
					<div class="col-sm-10"> 
					  <input type="text" class="form-control" id="payment_method" 
					  name="payment_method" placeholder="Payment Method"><span class="text-danger"><?php //echo form_error('payment_method'); ?></span>
					</div>
				</div>  -->

				<div class="col-md-6">
					<div class="form-group">
						<label class="control-label" for="bank_name">Bank Name:</label>
						<input type="text" class="form-control" id="bank_name" name="bank_name" placeholder="Bank Name"><span class="text-danger"><?php echo form_error('bank_name'); ?></span>
					</div>
				</div>

				<div class="col-md-6">
					<div class="form-group">
						<label class="control-label " for="branch_name">Branch Name:</label>
						  <input type="text" class="form-control" id="branch_name" name="branch_name" placeholder="Branch Name"><span class="text-danger"><?php echo form_error('branch_name'); ?></span>
					</div>
				</div>
			</div>


			<div class="row cheque_data" style="display:none;">
				<div class="col-md-6">
					<div class="form-group">
						<label class="control-label" for="cheque">Cheque:</label>
						  <input type="text" class="form-control" id="cheque_number" name="cheque" placeholder="Cheque Number"><span class="text-danger"><?php echo form_error('cheque'); ?></span>
					</div>
				</div>

				<div class="col-md-6">
					<div class="form-group">
						<label class="control-label" for="cheque_date">Cheque Date:</label> 
						<input type="text" class="form-control datepicker" id="cheque_date" name="cheque_date" placeholder="Cheque Date"><span class="text-danger"><?php echo form_error('cheque_date'); ?></span>
					</div>
				</div>
			</div>

		<div class="row">
          <div class="col-md-12">
            <table class="table">
				<thead class="aliceblue">
					<tr>
						<th>S. No.</th>
						<th>Fee Head</th>
						<th>Amount</th>
					</tr>
				</thead>
              <tbody>
                <?php if( isset($fee_details) && count($fee_details) > 0 ) { ?>
					<?php 
					if($nri == 'Yes'){
						if(in_array($permanent_country, $saarc_upper)){
							foreach( $fee_details as $key => $fee_detail ) {
								$count++; 
								if ($fee_detail->fee_head == 'Late Fee') {
									$fee_head_total += $late_fee;
								 }else{ 
									$fee_head_total += $fee_detail->nri_developing_country;
								}
								?> 
								<tr>
									<td><?php echo $count; ?></td>
									<td><?php echo $fee_detail->fee_head; ?></td>
									<td><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developing_country);}?></td>
								</tr>
							<?php }
						}else{
							foreach( $fee_details as $key => $fee_detail ) {
								$count++; 
								if ($fee_detail->fee_head == 'Late Fee') {
									$fee_head_total += $late_fee;
								 }else{ 
									$fee_head_total += $fee_detail->nri_developed_country;
								}
								?>
								<tr>
									<td><?php echo $count; ?></td>
									<td><?php echo $fee_detail->fee_head; ?></td>
									<td><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developed_country);}?></td>
								</tr>
							<?php }
						}
					} else {
                    // price for open category
						if($category == 'open') {
							foreach( $fee_details as $key => $fee_detail ) {
								$count++;
								if ($fee_detail->fee_head == 'Late Fee') {
									$fee_head_total += $late_fee;
								 }else{ 
									$fee_head_total += $fee_detail->amount;
								}
								?>
								<tr>
									<td><?php echo $count; ?></td>
									<td><?php echo $fee_detail->fee_head; ?></td>
									<td><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->amount);}?></td>
								</tr>
							<?php }
						} else { // price for reserved category with sc and st
							if(($caste == 'sc' || $caste == 'st') && $category == 'reserved') {
								foreach( $fee_details as $key => $fee_detail ) {
									$count++; 
								if ($fee_detail->fee_head == 'Late Fee') {
									$fee_head_total += $late_fee;
								 }else{ 
									$fee_head_total += $fee_detail->reserved_amount;
								}
								?>
									<tr>
										<td><?php echo $count; ?></td>
										<td><?php echo $fee_detail->fee_head; ?></td>
									<td><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->reserved_amount);}?></td>
									</tr>
								<?php } 
							} else {// price for reserved category without sc and st
								foreach( $fee_details as $key => $fee_detail ) {
								$count++; 
								if ($fee_detail->fee_head == 'Late Fee') {
									$fee_head_total += $late_fee;
								 }else{ 
									$fee_head_total += $fee_detail->reserved_amount;
								}
								?>
								<tr>
									<td><?php echo $count; ?></td>
									<td><?php echo $fee_detail->fee_head; ?></td>
									<td><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->reserved_amount);}?></td>
								</tr>
							<?php }
							}
						}
					}
				}?>
				</tbody>
				
				<tfoot class="aliceblue">
					<tr>
						<th></th>
						<th>Fee Head Total</th>
						<th>₹ <?php echo number_format($fee_head_total, '2'); ?></th>
					</tr>      
					<tr> 
						<th></th>
						<th>Grand Total</th>
						<th>₹ <?php $grand_total = $fee_head_total;
						echo number_format($grand_total, '2').'/-';?></th>
					</tr>
				</tfoot>
            </table>
          </div>
        </div>
        <input type="hidden" value="<?php echo $grand_total;  ?>" name="total_amount" id="amount">
        <input type="hidden" value="<?php echo $late_fee;  ?>" name="late_fee" id="late_fee">
        <button type="submit" class="btn btn-success">Generate Challan</button>


		</form>
	  </div>
	  <div class="card-footer small text-muted"></div>
	</div>

			

  </div>
  <!-- /.container-fluid -->
  