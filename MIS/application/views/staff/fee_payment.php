<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
  label { font-size: 17px !important; }
  /*.bg-gradient { background-image: linear-gradient(to right top, #008793, #00bf72, #a8eb12); color: #fff; }*/
</style>
<div id="content-wrapper">
	<div class="container-fluid">

	<!-- Breadcrumbs-->
	<ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('student/home'); ?>">Home</a>
		</li>
		<li class="breadcrumb-item">Fee Payment</li>
	</ol>

	<?php 
	$count = 0;
	$fee_head_total = 0;
	$pay_percentage = 0;
	$gst = 0;
	$grand_total = 0;
	
	$name = $student_details[0]->first_name.' '.$student_details[0]->middle_name.' '.$student_details[0]->last_name;
	$email = $student_details[0]->email_id;
	$mobile = $student_details[0]->mobile_number;
	$caste = $student_details[0]->caste;
	$category = strtolower($student_details[0]->caste);
	$caste_category = strtolower($student_details[0]->caste_category);
	$course = $course_details[0]->course_type;
	$courseName = ($course_details[0]->course_name) ? $course_details[0]->course_name : 'Regular';
	$course_id = $course_details[0]->id;
	$specialization = $course_details[0]->specialization;
	$year = $course_details[0]->year;
	$nri = $student_details[0]->nri;
	if($nri){
		$permanent_country = strtoupper(trim($student_details[0]->permanent_country));
	}
	$saarc = array('Afghanistan', 'Bangladesh', 'Bhutan', 'India', 'Sri Lanka', 'Maldives', 'Nepal', 'Pakistan');
	$saarc_upper = array_change_key_case($saarc,CASE_UPPER);	 
	?>

    <div class="mb-20">    
		<form method="post" id="fee_pay" enctype="multipart/form-data" action="<?php echo base_url(); ?>check_payment">
        <div class="card mb-20">
			<div class="card-header">
				<i class="far fa-credit-card"></i>
				Payment Details
			</div>
          <div class="card-body">
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label for="feetype">Fee Type: </label>
                  <label for="feetype"><b>Adacemic</b></label>
                  <input type="hidden" name="fee_type" id="fee_type" value="Adacemic">
                </div>
                <div class="form-group">
                  <label for="name">Name: </label>
                  <label for="name"><b><?php echo $name; ?></b></label>
                  <input type="hidden" class="form-control" name="name" id="name" value="<?php echo $name; ?>" readonly>
                </div>
                <div class="form-group">
                  <label for="name">Year / Course: </label>
                  <label for="name"><b><?php echo $year. "-".$courseName." " . $course; ?></b></label>
                  <input type="hidden" class="form-control" id="year" name="year" value="<?php echo $year; ?>" readonly>
                  <input type="hidden" class="form-control" name="course" id="course" value="<?php echo $course; ?>" readonly>
                </div>
                <div class="form-group">
                  <label for="name">Caste: </label>
                  <label for="name"><b><?php echo strtoupper($caste); ?></b></label>
                  <input type="hidden" class="form-control" name="category" id="category" value="<?php echo $caste; ?>" readonly>
                </div>
              </div>
              <div class="col-md-6">
              </div>
            </div>
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label for="email"><b>Email:</b></label>
                  <input type="email" class="form-control" id="email" name="email" value="<?php echo $email; ?>" required>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label for="year"><b>Mobile:</b></label>
                  <input type="tel" class="form-control" id="mobile" name="mobile" value="<?php echo $mobile; ?>" required>
                </div>
              </div>
            </div>
            <div class="row grand_total_div">
              <div class="col-md-12">
                <div class="form-group">
                  <em style="color: crimson; font-weight: 400;">Note: Email and SMS will be sent to you for payment confirmation.</em>
                </div>
                <div class="form-group">
                  <label for="year" style="font-size: 20px !important;"><b>Grand Total:</b></label>
                  <label for="year" style="font-size: 20px !important;"><span><b>₹ </b></span><b id="grand_total"></b></label>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <button type="submit" class="btn btn-success">Make Payment</button>
                </div>
              </div>
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
					<?php
					if( isset($fee_details) && count($fee_details) > 0 ) {
						if($nri == 'Yes'){//if nri
							if(in_array($permanent_country, $saarc_upper)){
								foreach( $fee_details as $key => $fee_detail ) {
									$count++; 
									$fee_head_total += $fee_detail->nri_developing_country;?>
									<tr>
										<td><?php echo $count; ?></td>
										<td><?php echo $fee_detail->fee_head; ?></td>
										<td><?php echo number_format($fee_detail->nri_developed_country);?></td>
									</tr>
								<?php }
							}else{
								foreach( $fee_details as $key => $fee_detail ) {
									$count++; 
									$fee_head_total += $fee_detail->nri_developed_country;?>
									<tr>
										<td><?php echo $count; ?></td>
										<td><?php echo $fee_detail->fee_head; ?></td>
										<td><?php echo number_format($fee_detail->nri_developed_country);?></td>
									</tr>
								<?php }
							}
						} else {
							// loop for open category
							if($caste_category == 'open') {
								foreach( $fee_details as $key => $fee_detail ) {
									$count++; 
									$fee_head_total += $fee_detail->amount;?>
									<tr>
										<td><?php echo $count; ?></td>
										<td><?php echo $fee_detail->fee_head; ?></td>
										<td><?php echo number_format($fee_detail->amount);?></td>
									</tr>
								<?php }
							}?>

							<!-- loop for reserved category -->
							<?php
							if($caste_category == 'reserved' && ($category == 'sc' || $category == 'st')) {
								foreach( $fee_details as $key => $fee_detail ) {
									$count++; 
									$fee_head_total += $fee_detail->reserved_amount;?>
									<tr>
										<td><?php echo $count; ?></td>
										<td><?php echo $fee_detail->fee_head; ?></td>
										<td><?php echo number_format($fee_detail->reserved_amount);?></td>
									</tr>
								<?php 
								}
							}
						} 
					}?>
					</tbody>
					<tfoot class="aliceblue">
						<tr>
							<td></td>
							<th>Fee Head Total</th>
							<th>₹ <?php echo number_format($fee_head_total,2); ?></th>
						</tr>
						<tr>
							<td></td>
							<td>Online Payment Processing Charge (2%)</td>
							<td>₹ <?php $pay_percentage = ($fee_head_total/100) * 2;
								//echo number_format($pay_percentage, 2);
								if($pay_percentage >= '500'){ 
									$pay_percentage = number_format('500', 2); 
								}else{
									$pay_percentage = number_format($pay_percentage, 2);
								}
								echo $pay_percentage;?>
							</td>
						</tr>
						<tr>
							<td></td>
							<td>GST (18%)</td>
							<td>₹ <?php $gst = (18/100)*($pay_percentage);
								echo number_format($gst, '2');?>
							</td>
						</tr>
						<tr>
							<td></td>
							<th>Grand Total</th>
							<th>₹ <?php $grand_total = $fee_head_total+$pay_percentage+$gst;
							echo number_format($grand_total, '2').'/-';?>
							</th>
						</tr>
					</tfoot>
				</table>
			</div>
        </div>
        
        <input type="hidden" name="fee_head_total" value="<?php echo $fee_head_total; ?>">
        <input type="hidden" name="amount" value="<?php echo number_format($grand_total, '2'); ?>">
        <input type="hidden" name="firstname" value="<?php echo $student_details[0]->first_name; ?>">
        <input type="hidden" name="category" value="<?php echo $category; ?>">
        <input type="hidden" name="course_id" value="<?php echo $course_id; ?>">
        <input type="hidden" name="commission" value="<?php echo number_format($pay_percentage+$gst, '2'); ?>">
        <button type="submit" class="btn btn-success">Make Payment</button>
      </form>
    </div>
  </div>

  <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
  <script>
    jQuery(document).ready(function(){
      var grand_total = "<?php echo number_format($grand_total, '2').'/-'; ?>";
      jQuery("#grand_total").text(grand_total);
    });
  </script>
  <!-- /.container-fluid -->

  