<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Receipt PDF</title>
    </head>
     <style>    	
    	.content-wrapper{
    		align-items: center;
    		text-align: center;
    	}
		table{
			text-transform: uppercase;
		}
		body 
		{
			font-size: 1em;
			line-height:1.375em;
		}
		 table.borderTable {
            border-collapse: collapse;
        }

        table.borderTable {
            border: 1px solid black;
        }

        th.borderTable, td.borderTable {
            border: 1px solid black;
            padding: 5px;
        }

        .marks td {
            font-size: 12px;
            text-align: center;
        }

        tr.bottombor {
            border: 1px solid black;
        }
    </style>
    <body>
	<?php
		$count = 0;
		$fee_head_total = 0;
		$pay_percentage = 0;
		$gst = 0;
		$grand_total = 0;

		$caste_category = strtolower(@$transaction_data[0]->category);
		$category = strtolower(@$student_details[0]->caste);
		$roll_number = @$student_details[0]->roll_number;
		$division = @$student_details[0]->division;
        $student_name = @$student_details[0]->first_name.' '.@$student_details[0]->middle_name.' '.@$student_details[0]->last_name;
        $student_id = @$student_details[0]->userID;
        $total_fee = $fees_form_fields['total_fee'] ;
        $college_fee =  $fees_form_fields['college_fee'] ;
       // $online_total = $fees_form_fields['online_total'] ;
        $online_total = $total_fee - $college_fee ;
        $sname = $fees_form_fields['sname'];
        $nri = @$student_details[0]->nri;
		if($nri){
			$permanent_country = strtoupper(trim(@$student_details[0]->permanent_country));
		}
		$saarc = array('Afghanistan', 'Bangladesh', 'Bhutan', 'India', 'Sri Lanka', 'Maldives', 'Nepal', 'Pakistan');
		$saarc_upper = array_change_key_case($saarc,CASE_UPPER);
		$late_fee = $this->search_model->get_late_fees_amount();
      

        function numberTowords($total_fee){
			$num = $total_fee;
			$ones = array(
			0 =>"ZERO",
			1 => "ONE",
			2 => "TWO",
			3 => "THREE",
			4 => "FOUR",
			5 => "FIVE",
			6 => "SIX",
			7 => "SEVEN",
			8 => "EIGHT",
			9 => "NINE",
			10 => "TEN",
			11 => "ELEVEN",
			12 => "TWELVE",
			13 => "THIRTEEN",
			14 => "FOURTEEN",
			15 => "FIFTEEN",
			16 => "SIXTEEN",
			17 => "SEVENTEEN",
			18 => "EIGHTEEN",
			19 => "NINETEEN",
			"014" => "FOURTEEN"
			);
			$tens = array( 
			0 => "ZERO",
			1 => "TEN",
			2 => "TWENTY",
			3 => "THIRTY", 
			4 => "FORTY", 
			5 => "FIFTY", 
			6 => "SIXTY", 
			7 => "SEVENTY", 
			8 => "EIGHTY", 
			9 => "NINETY" 
			); 
			$hundreds = array( 
			"HUNDRED", 
			"THOUSAND", 
			"MILLION", 
			"BILLION", 
			"TRILLION", 
			"QUARDRILLION" 
			); /*limit t quadrillion */
			$num = number_format($num,2,".",","); 
			$num_arr = explode(".",$num); 
			$wholenum = $num_arr[0]; 
			$decnum = $num_arr[1]; 
			$whole_arr = array_reverse(explode(",",$wholenum)); 
			krsort($whole_arr,1); 
			$rettxt = ""; 
			foreach($whole_arr as $key => $i){				
				while(substr($i,0,1)=="0")
				$i=substr($i,1,5);
				if($i < 20){ 
					/* echo "getting:".$i; */
					$rettxt .= $ones[$i]; 
				}elseif($i < 100){ 
					if(substr($i,0,1)!="0")  $rettxt .= $tens[substr($i,0,1)]; 
					if(substr($i,1,1)!="0") $rettxt .= " ".$ones[substr($i,1,1)]; 
				}else{ 
					if(substr($i,0,1)!="0") $rettxt .= $ones[substr($i,0,1)]." ".$hundreds[0]; 
					if(substr($i,1,1)!="0")$rettxt .= " ".$tens[substr($i,1,1)]; 
					if(substr($i,2,1)!="0")$rettxt .= " ".$ones[substr($i,2,1)]; 
				} 
				if($key > 0){ 
					$rettxt .= " ".$hundreds[$key]." "; 
				}
			} 
			if($decnum > 0){
				$rettxt .= " and ";
				if($decnum < 20){
					$rettxt .= @$ones[$decnum];
				}elseif($decnum < 100){
					$rettxt .= $tens[substr($decnum,0,1)];
					$rettxt .= " ".$ones[substr($decnum,1,1)];
				}
			}
			return $rettxt;
		}
		$words =  numberTowords($total_fee);
        $bank_name = $fees_form_fields['bank_name'] ;
        //$name = $fees_form_fields['name'] ;
         $branch_name= $fees_form_fields['branch_name'] ;
        $cheque_date= $fees_form_fields['cheque_date'] ;
        $cheque = $fees_form_fields['cheque'] ;
        $course = @$course_details[0]->course_type;
        $course_id = @$course_details[0]->id;
        $specialization = @$course_details[0]->specialization;
        $year = @$course_details[0]->year;
               $course_specilization = $year . $specialization;?>
<table cellpadding="5" cellspacing ="20" style="text-align:center;width:100%;page-break-after:always">
	<tr>
		<td>
			<table style="text-align:center;width:100%;margin-left:-5px; vertical-align:top;margin-right:-5px; border:2px solid black;">
				<tr>
					<td style="padding:0 5px">
						<table width="95%" style="margin:10px 0;">
							<tr>
								<td colspan="2">		
									<img style="height: 80px" src="data:image/png;base64, iVBORw0KGgoAAAANSUhEUgAAAV0AAAEVCAYAAABDgza2AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAxRpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTM4IDc5LjE1OTgyNCwgMjAxNi8wOS8xNC0wMTowOTowMSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6QTA2NjZDQTc4RThGMTFFOUE1NTBGRDg0RkE3QUE0ODYiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6QTA2NjZDQTY4RThGMTFFOUE1NTBGRDg0RkE3QUE0ODYiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTcgV2luZG93cyI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSI1RDZGOUE0QUYzNjg0QUFFMzk1MUQ5NDUwQTRCMUUzMiIgc3RSZWY6ZG9jdW1lbnRJRD0iNUQ2RjlBNEFGMzY4NEFBRTM5NTFEOTQ1MEE0QjFFMzIiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7pYaxjAAArm0lEQVR42uxdjVLcRhokHGe76u4JEv8QAg55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"  />
								
									<h2>Sir Vithaldas Thackersey College of Home Science (Autonomous)</h2>
									<h2>S.N.D.T Women University, Sir Vithaldas Vidya Vihar, Juhu Road, Santacruz (West) Mumbai - 400049</h2>
								</td>
							</tr>
						</table>						
						<table border="0" width="85%" style="font-size:13px;text-align:left;">
							<tr>
								<td colspan="2" style="text-align:center;">
									<p><strong><?php echo $year .' . '. $specialization ?></strong></p>
									<h3> <?php if($caste_category == 'reserved'){echo 'Reserved';			} else {echo 'Open';}?> Category Student<br>Academic Year - <?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h3><br>
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:left;"><strong>Receipt Number: </strong> 
									<?php echo $fees_form_fields['challan_number'];?>
											&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;
											&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;
											&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp; &nbsp;&nbsp;	&nbsp;	&nbsp;	&nbsp; &nbsp;	 &nbsp;		&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;	&nbsp;  <strong>Date: </strong><?php echo date('d/m/Y',strtotime($transaction_data[0]->fee_paid_date))	;?>
										</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">	
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">
									<h3>Paid into credit of Sir Vithaldas </br>Thackersey College of Home Science (Autonomous)</h3>
								</td>
							</tr>
									&nbsp;
							
							<tr>
								<td colspan="2" style="text-align:center;">
										<h3 style="font-size:13px">TOTAL COLLEGE FEES RS. : <?php echo number_format($college_fee,'2') ; ?>/-</h3>
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">
										<h3 style="font-size:13px">ONLINE PAYMENT PROCESSING CHARGES RS. : <?php echo number_format($online_total,'2') ; ?>/-</h3>
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">
										<h3 style="font-size:13px"> TOTAL SUM PAID RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3>
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;"><strong>Rupees in words: <?php echo $words ?> ONLY</strong>
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">
									&nbsp;
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:left;"><strong>Name of Student: </strong> <?php echo $sname 
								/*$student_name;*/ ?>
								</td>
							</tr>
						<!-- <tr>
								<td colspan="2" style="text-align:left;"><strong>Department: </strong><?php echo $specialization; ?></td>
							</tr> -->
							<tr>
								<td colspan="2" style="text-align:left;"><strong>Specialization: </strong><?php echo $specialization; ?>
							</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:left;"><strong>Division: </strong><?php echo	$division ?></td>
							</tr>
							<!-- <tr>
								<td colspan="2" style="text-align:left;"><strong>Roll No: </strong>
									<?php echo $roll_number;?>
									</td>
							</tr> -->
							<tr>
								<td colspan="2" style="text-align:center;">
									&nbsp;
								</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">
									<h3>Note: This is automated generated receipt. Fee Payment is subject to realisation.</h3>
									</td>
							</tr>
							<tr>
								<td colspan="2" style="text-align:center;">
									&nbsp;
								</td>
							</tr>			
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<table style="width:100%; font-family: Arial;">
	<!-- <thead style="margin-top: 10px;">
		<tr>
			<th style="vertical-align: bottom;border-bottom: 2px solid black;padding: 2px">S. No.</th>
			<th style="vertical-align: bottom;border-bottom: 2px solid black;padding: 2px">Fee Head</th>
			<th style="vertical-align: bottom;border-bottom: 2px solid black;padding: 2px">Amount</th>
		</tr>
	</thead> -->
	<!-- <tbody> -->
	<tr>
        <td style="">
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
				<tr>
					<td style="width: 50%;">
						<table cellpadding="0" cellspacing="0" class="borderTable" style="width: 100%;">
							<tr>
								<th style="width: 60%; font-weight: bold; font-size: 18px; padding: 5px;">Fee Head</th>
								<th style="width: 40%; font-weight: bold; font-size: 18px; padding: 5px;">Amount</th>
							</tr>
							<?php
							if( isset($fee_details) && count($fee_details) > 0 ) {
								if($nri == 'Yes'){//if nri
									if(in_array($permanent_country, $saarc_upper)){
										foreach( $fee_details as $key => $fee_detail ) {
											$count++;  
											if ($fee_detail->fee_head == 'Late Fee') {
												$fee_head_total += $late_fee;
											 }else{ 
												$fee_head_total += $fee_detail->nri_developing_country;
											}
											if ($key == 0 || $key%2 == 0){
											?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td style="width: 40%; font-size: 16px; padding: 5px; border-top: 1px solid #000; border-right: 1px solid #000;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developing_country);}?></td>
											</tr>
										<?php }
										}
									}else{
										foreach( $fee_details as $key => $fee_detail ) {
											$count++;  
											if ($fee_detail->fee_head == 'Late Fee') {
												$fee_head_total += $late_fee;
											 }else{ 
												$fee_head_total += $fee_detail->nri_developed_country;
											}
											if ($key == 0 || $key%2 == 0){
											?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td style="width: 40%; font-size: 16px; padding: 5px; border-top: 1px solid #000; border-right: 1px solid #000;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developed_country);}?></td>
											</tr>
										<?php }
										}
									}
								} else {
									// loop for open category === main 
									if($caste_category == 'open' || ($caste_category == 'reserved' || ($category != 'sc' && $category != 'st'))) {
										foreach( $fee_details as $key => $fee_detail ) {
											$count++; 
											if ($fee_detail->fee_head == 'Late Fee') {
												$fee_head_total += $late_fee;
											 }else{ 
												$fee_head_total += $fee_detail->amount;
											}
											if ($key == 0 || $key%2 == 0){
											?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td style="width: 40%; font-size: 16px; padding: 5px; border-top: 1px solid #000; border-right: 1px solid #000;text-align: center"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->amount);}?></td>
											</tr>
										<?php }
										}
									}?>

									<!-- loop for reserved category -->
									<?php
									if($caste_category == 'reserved' && ($category == 'sc' || $category == 'st')) {
										foreach( $fee_details as $key => $fee_detail ) {
											$count++; 
										if ($fee_detail->fee_head == 'Late Fee') {
											$fee_head_total += $late_fee;
										 }else{ 
											$fee_head_total += $fee_detail->reserved_amount;
										}
											if ($key == 0 || $key%2 == 0){
										?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td style="width: 40%; font-size: 16px; padding: 5px; border-top: 1px solid #000; border-right: 1px solid #000;text-align: center"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->reserved_amount);}?></td>
											</tr>
										<?php }
										}
									}
								} 
							}?>
						</table>
					</td>
					
					<td style="width: 50%;">
						<table cellpadding="0" cellspacing="0" class="borderTable" style="width: 100%;">
							<tr>
								<th style="width: 60%; font-weight: bold; font-size: 18px; padding: 5px;">Fee Head</th>
								<th style="width: 40%; font-weight: bold; font-size: 18px; padding: 5px;">Amount</th>
							</tr>
							<?php
							if( isset($fee_details) && count($fee_details) > 0 ) {
								if($nri == 'Yes'){//if nri
									if(in_array($permanent_country, $saarc_upper)){
										foreach( $fee_details as $key => $fee_detail ) {
											$count++;  
											if ($fee_detail->fee_head == 'Late Fee') {
												$fee_head_total += $late_fee;
											 }else{ 
												$fee_head_total += $fee_detail->nri_developing_country;
											}
											if ($key%2 == 1){
											?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td class="borderTable" style="width: 40%; font-size: 16px; padding: 5px;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developing_country);}?></td>
											</tr>
										<?php }
										}
									}else{
										foreach( $fee_details as $key => $fee_detail ) {
											$count++;  
											if ($fee_detail->fee_head == 'Late Fee') {
												$fee_head_total += $late_fee;
											 }else{ 
												$fee_head_total += $fee_detail->nri_developed_country;
											}
											if ($key%2 == 1){
											?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td class="borderTable" style="width: 40%; font-size: 16px; padding: 5px;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developed_country);}?></td>
											</tr>
										<?php }
										}
									}
								} else {
									// loop for open category === main
									if($caste_category == 'open' || ($caste_category == 'reserved' || ($category != 'sc' && $category != 'st'))) {
										foreach( $fee_details as $key => $fee_detail ) {
											$count++; 
											if ($fee_detail->fee_head == 'Late Fee') {
												$fee_head_total += $late_fee;
											 }else{ 
												$fee_head_total += $fee_detail->amount;
											}
											if ($key%2 == 1){
											?>
											<tr>
												<!-- <td style="border:1px solid black;padding:2px;"><?php echo $count; ?></td> -->
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td class="borderTable" style="width: 40%; font-size: 16px; padding: 5px;text-align: center"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->amount);}?></td>
											</tr>
										<?php }
										}
									}?>

									<!-- loop for reserved category -->
									<?php
									if($caste_category == 'reserved' && ($category == 'sc' || $category == 'st')) {
										foreach( $fee_details as $key => $fee_detail ) {
											$count++; 
										if ($fee_detail->fee_head == 'Late Fee') {
											$fee_head_total += $late_fee;
										 }else{ 
											$fee_head_total += $fee_detail->reserved_amount;
										}
											if ($key%2 == 1){
										?>
											<tr>
												<td class="borderTable" style="width: 60%; font-weight: bold; font-size: 16px; padding: 5px;"><?php echo $fee_detail->fee_head; ?></td>
												<td class="borderTable" style="width: 40%; font-size: 16px; padding: 5px;text-align: center"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->reserved_amount);}?></td>
											</tr>
										<?php 
											}
										}
									}
								} 


										if(count($fee_details)%2 == 1){ ?>
											<tr>
												<td class="borderTable"  style="width: 100%; font-weight: bold; font-size: 16px; padding: 5px;"><br></td>
											</tr>
										<?php }


							}?>
						</table>
					</td>
				</tr>
				<tr>
					<?php $fee_head_total = $fee_head_total/2; ?>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">Fee Head Total</th>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">₹ <?php echo number_format($fee_head_total,2); ?></th>
				</tr>
				<tr>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">Online Payment Processing Charge </th>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">₹ <?php $pay_percentage = ($fee_head_total/100) * 2;
						//echo number_format($pay_percentage, 2);
						/*if($pay_percentage >= '500'){ 
							$pay_percentage = number_format('500', 2); 
						}else{
							$pay_percentage = number_format($pay_percentage, 2);
						}*/
						$gst = (18/100)*($pay_percentage);
						//echo $pay_percentage+$gst;
						echo $online_total;
						?>
					</th>
				</tr>
				<!-- <tr>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">GST (18%)</th>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black;  font-size: 18px;">₹ <?php $gst = (18/100)*($pay_percentage);
						echo number_format($gst, '2');?>
					</th>
				</tr> -->
				<tr>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">Grand Total</th>
					<th style="width: 50%; padding: 10px 10px 10px 0px; border: 1px solid black; font-size: 18px; ">₹ <?php $grand_total = $fee_head_total+$pay_percentage+$gst;
					//echo number_format($grand_total, '2').'/-';
					echo number_format($total_fee, '2').'/-';
					?>
					</th>
				</tr>
            </table>
        </td>
	</tr>
	<?php /*
	if( isset($fee_details) && count($fee_details) > 0 ) {
		if($nri == 'Yes'){//if nri
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
						<td style="border:1px solid black;padding:2px;"><?php echo $count; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php echo $fee_detail->fee_head; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developing_country);}?></td>
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
						<td style="border:1px solid black;padding:2px;"><?php echo $count; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php echo $fee_detail->fee_head; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->nri_developed_country);}?></td>
					</tr>
				<?php }
			}
		} else {
			// loop for open category
			if($caste_category == 'open' || ($caste_category == 'reserved' && ($category != 'sc' && $category != 'st'))) {
				foreach( $fee_details as $key => $fee_detail ) {
					$count++; 
					if ($fee_detail->fee_head == 'Late Fee') {
						$fee_head_total += $late_fee;
					 }else{ 
						$fee_head_total += $fee_detail->amount;
					}
					?>
					<tr>
						<!-- <td style="border:1px solid black;padding:2px;"><?php echo $count; ?></td> -->
						<td style="border:1px solid black;padding:2px;"><?php echo $fee_detail->fee_head; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->amount);}?></td>
					</tr>
				<?php }
			}?>

			<!-- loop for reserved category -->
			<?php
			if($caste_category == 'reserved' && ($category == 'sc' || $category == 'st')) {
				foreach( $fee_details as $key => $fee_detail ) {
					$count++; 
				if ($fee_detail->fee_head == 'Late Fee') {
					$fee_head_total += $late_fee;
				 }else{ 
					$fee_head_total += $fee_detail->reserved_amount;
				}
				?>
					<tr>
						<td style="border:1px solid black;padding:2px;"><?php echo $count; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php echo $fee_detail->fee_head; ?></td>
						<td style="border:1px solid black;padding:2px;"><?php if ($fee_detail->fee_head == 'Late Fee'){echo number_format($late_fee);}else{echo number_format($fee_detail->reserved_amount);}?></td>
					</tr>
				<?php 
				}
			}
		} 
	} */?>
	<!-- </tbody> -->
	
</table>
    </body>
</html>