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
    </style>
    <body>
	<?php
		$caste_category = strtolower($student_details[0]->caste_category);
        $student_name = $student_details[0]->first_name.' '.$student_details[0]->middle_name.' '.$student_details[0]->last_name;
        $student_id = $student_details[0]->userID;
        $total_fee = $fees_form_fields['total_fee'] ;

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
					$rettxt .= $ones[$decnum];
				}elseif($decnum < 100){
					$rettxt .= $tens[substr($decnum,0,1)];
					$rettxt .= " ".$ones[substr($decnum,1,1)];
				}
			}
			return $rettxt;
		}
		$words =  numberTowords($total_fee);
        $bank_name = $fees_form_fields['bank_name'] ;
        $branch_name= $fees_form_fields['branch_name'] ;
        $cheque_date= $fees_form_fields['cheque_date'] ;
        $cheque = $fees_form_fields['cheque'] ;
        $course = $course_details[0]->course_type;
        $course_id = $course_details[0]->id;
        $specialization = $course_details[0]->specialization;
        $year = $course_details[0]->year;
        $course_specilization = $year . $specialization;?>

		<!--<h2 align="center" style="margin-bottom: 15px;">DETAILS OF YEARLY FEES</h2>-->
       
			<table style="text-align:center;width:100%;margin-left:-40px; vertical-align:top;margin-right:-40px;">
				<tr>
					<td style="padding:0 5px">
						<p>(College Administration Copy)<br>(To be preserved as reciept)</p>
						<p><strong style="text-transform: uppercase;">CANARA BANK</strong><br>Santacruz (W) Mumbai - 54</p>
						<strong><?php echo $fees_form_fields['challan_number'];?></strong><br><br>
						<p style="font-size:10px;"><u>Reciept should be produced at the time of claiming deposits.After final year Result collect deposit fee <b>within one month</b></u></p>
						<table style="margin:10px 0;">
							<tr>
								<td><img src="<?php echo base_url(). 'assets/img/logo.png' ?>" width="20" /></td>
								<td><h4 style="font-size:13px;">Sir Vithaldas Thackersey College of Home Science</h4></td>
							</tr>
						</table>						
						<p>S.N.D.T Women University,Sir Vithaldas Vidya Vihar,Juhu Road, Santacruz (West) Mumbai - 400 049</p><br>
						<p><strong><?php echo $year .' . '. $specialization ?></strong></p>
						<h5 style="font-size:10px;"><?php 
						if($caste_category == 'Open'){
							echo 'Open';
						} else {
							echo 'Reserved';
						}?> Category Student Fee Year <br>
						<?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h5><br>
						<p>Paid into credit of Sir Vithaldas Thackersey College of Home Science</p>
						<p style="font-size:11px"><strong>S.B Account No. 4634101000018</strong></p><br>
						<h3 style="font-size:13px">A SUM OF RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3><br>
						<table style="font-size:11px;text-align:left;">
							<tr>
								<td colspan="2"><strong>Rupees in words: </strong><?php echo $words ?> ONLY</td>
							</tr>
							<tr>
								<td colspan="2"><strong>Name of candidate: </strong> <?php echo $student_name; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Department: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Specilization: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Division: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Roll No: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Clerk/Cashier/College: </strong></td>
							</tr>
							<tr>
								<td><strong>Date: </strong></td>
								<td><?php echo $cheque_date; ?></td>
							</tr>
							<tr>
								<td><strong>D.D.No.: </strong></td>
								<td><?php echo $cheque;?></td>
							</tr>
							<tr>
								<td><strong>Name of the Bank: </strong></td>
								<td><?php echo $bank_name; ?></td>
							</tr>						
						</table>
						<table style="width:100%;font-size:10px;margin-top:20px">
							<tr>
								<td>For S.V.T College of Home Science</td>
								<td>Reserving Cashier<br>Seal of Bank</td>
							</tr>
						</table>
					</td>
					<td style="padding:0 5px">						
						<p>(For Bank Copy)<br>(To be preserved as reciept)</p>
						<p><strong style="text-transform: uppercase;">CANARA BANK</strong><br>Santacruz (W) Mumbai - 54</p>
						<strong><?php echo $fees_form_fields['challan_number'];?></strong><br><br>
						<p style="font-size:10px;"><u>Reciept should be produced at the time of claiming deposits.After final year Result collect deposit fee <b>within one month</b></u></p>
						<table style="margin:10px 0;">
							<tr>
								<td><img src="<?php echo base_url(). 'assets/img/logo.png' ?>" width="20" /></td>
								<td><h4 style="font-size:13px;">Sir Vithaldas Thackersey College of Home Science</h4></td>
							</tr>
						</table>						
						<p>S.N.D.T Women University,Sir Vithaldas Vidya Vihar,Juhu Road, Santacruz (West) Mumbai - 400 049</p><br>
						<p><strong><?php echo $year .' . '. $specialization ?></strong></p>
						<h5 style="font-size:10px;"><?php 
						if($caste_category == 'Open'){
							echo 'Open';
						} else {
							echo 'Reserved';
						}?> Category Student Fee Year <br>
						<?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h5><br>
						<p>Paid into credit of Sir Vithaldas Thackersey College of Home Science</p>
						<p style="font-size:11px"><strong>S.B Account No. 4634101000018</strong></p><br>
						<h3 style="font-size:13px">A SUM OF RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3><br>
						<table style="font-size:11px;text-align:left;">
							<tr>
								<td colspan="2"><strong>Rupees in words: </strong><?php echo $words ?> ONLY</td>
							</tr>
							<tr>
								<td colspan="2"><strong>Name of candidate: </strong> <?php echo $student_name; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Department: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Specilization: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Division: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Roll No: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Clerk/Cashier/College: </strong></td>
							</tr>
							<tr>
								<td><strong>Date: </strong></td>
								<td><?php echo $cheque_date; ?></td>
							</tr>
							<tr>
								<td><strong>D.D.No.: </strong></td>
								<td><?php echo $cheque;?></td>
							</tr>
							<tr>
								<td><strong>Name of the Bank: </strong></td>
								<td><?php echo $bank_name; ?></td>
							</tr>						
						</table>
						<table style="width:100%;font-size:10px;margin-top:20px">
							<tr>
								<td>For S.V.T College of Home Science</td>
								<td>Reserving Cashier<br>Seal of Bank</td>
							</tr>
						</table>
					</td>
					<td style="padding:0 5px">						
						<p>(For Account Copy)<br>(To be preserved as reciept)</p>
						<p><strong style="text-transform: uppercase;">CANARA BANK</strong><br>Santacruz (W) Mumbai - 54</p>
						<strong><?php echo $fees_form_fields['challan_number'];?></strong><br><br>
						<p style="font-size:10px;"><u>Reciept should be produced at the time of claiming deposits.After final year Result collect deposit fee <b>within one month</b></u></p>
						<table style="margin:10px 0;">
							<tr>
								<td><img src="<?php echo base_url(). 'assets/img/logo.png' ?>" width="20" /></td>
								<td><h4 style="font-size:13px;">Sir Vithaldas Thackersey College of Home Science</h4></td>
							</tr>
						</table>						
						<p>S.N.D.T Women University,Sir Vithaldas Vidya Vihar,Juhu Road, Santacruz (West) Mumbai - 400 049</p><br>
						<p><strong><?php echo $year .' . '. $specialization ?></strong></p>
						<h5 style="font-size:10px;"><?php 
						if($caste_category == 'Open'){
							echo 'Open';
						} else {
							echo 'Reserved';
						}?> Category Student Fee Year <br>
						<?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h5><br>
						<p>Paid into credit of Sir Vithaldas Thackersey College of Home Science</p>
						<p style="font-size:11px"><strong>S.B Account No. 4634101000018</strong></p><br>
						<h3 style="font-size:13px">A SUM OF RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3><br>
						<table style="font-size:11px;text-align:left;">
							<tr>
								<td colspan="2"><strong>Rupees in words: </strong><?php echo $words ?> ONLY</td>
							</tr>
							<tr>
								<td colspan="2"><strong>Name of candidate: </strong> <?php echo $student_name; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Department: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Specilization: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Division: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Roll No: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Clerk/Cashier/College: </strong></td>
							</tr>
							<tr>
								<td><strong>Date: </strong></td>
								<td><?php echo $cheque_date; ?></td>
							</tr>
							<tr>
								<td><strong>D.D.No.: </strong></td>
								<td><?php echo $cheque;?></td>
							</tr>
							<tr>
								<td><strong>Name of the Bank: </strong></td>
								<td><?php echo $bank_name; ?></td>
							</tr>						
						</table>
						<table style="width:100%;font-size:10px;margin-top:20px">
							<tr>
								<td>For S.V.T College of Home Science</td>
								<td>Reserving Cashier<br>Seal of Bank</td>
							</tr>
						</table>
					</td>
					<td style="padding:0 5px">						
						<p>(For Office Copy)<br>(To be preserved as reciept)</p>
						<p><strong style="text-transform: uppercase;">CANARA BANK</strong><br>Santacruz (W) Mumbai - 54</p>
						<strong><?php echo $fees_form_fields['challan_number'];?></strong><br><br>
						<p style="font-size:10px;"><u>Reciept should be produced at the time of claiming deposits.After final year Result collect deposit fee <b>within one month</b></u></p>
						<table style="margin:10px 0;">
							<tr>
								<td><img src="<?php echo base_url(). 'assets/img/logo.png' ?>" width="20" /></td>
								<td><h4 style="font-size:13px;">Sir Vithaldas Thackersey College of Home Science</h4></td>
							</tr>
						</table>
						<p>S.N.D.T Women University,Sir Vithaldas Vidya Vihar,Juhu Road, Santacruz (West) Mumbai - 400 049</p><br>
						<p><strong><?php echo $year .' . '. $specialization ?></strong></p>
						<h5 style="font-size:10px;"><?php 
						if($caste_category == 'Open'){
							echo 'Open';
						} else {
							echo 'Reserved';
						}?> Category Student Fee Year <br>
						<?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h5><br>
						<p>Paid into credit of Sir Vithaldas Thackersey College of Home Science</p>
						<p style="font-size:11px"><strong>S.B Account No. 4634101000018</strong></p><br>
						<h3 style="font-size:13px">A SUM OF RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3><br>
						<table style="font-size:11px;text-align:left;">
							<tr>
								<td colspan="2"><strong>Rupees in words: </strong><?php echo $words ?> ONLY</td>
							</tr>
							<tr>
								<td colspan="2"><strong>Name of candidate: </strong> <?php echo $student_name; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Department: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Specilization: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Division: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Roll No: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Clerk/Cashier/College: </strong></td>
							</tr>
							<tr>
								<td><strong>Date: </strong></td>
								<td><?php echo $cheque_date; ?></td>
							</tr>
							<tr>
								<td><strong>D.D.No.: </strong></td>
								<td><?php echo $cheque;?></td>
							</tr>
							<tr>
								<td><strong>Name of the Bank: </strong></td>
								<td><?php echo $bank_name; ?></td>
							</tr>						
						</table>	
						<table style="width:100%;font-size:10px;margin-top:20px">
							<tr>
								<td>For S.V.T College of Home Science</td>
								<td>Reserving Cashier<br>Seal of Bank</td>
							</tr>
						</table>
					</td>
					<td style="padding:0 5px">						
						<p>(For Student Copy)<br>(To be preserved as reciept)</p>
						<p><strong style="text-transform: uppercase;">CANARA BANK</strong><br>Santacruz (W) Mumbai - 54</p>
						<strong><?php echo $fees_form_fields['challan_number'];?></strong><br><br>
						<p style="font-size:10px;"><u>Reciept should be produced at the time of claiming deposits.After final year Result collect deposit fee <b>within one month</b></u></p>
						<table style="margin:10px 0;">
							<tr>
								<td><img src="<?php echo base_url(). 'assets/img/logo.png' ?>" width="20" /></td>
								<td><h4 style="font-size:13px;">Sir Vithaldas Thackersey College of Home Science</h4></td>
							</tr>
						</table>						
						<p>S.N.D.T Women University,Sir Vithaldas Vidya Vihar,Juhu Road, Santacruz (West) Mumbai - 400 049</p><br>
						<p><strong><?php echo $year .' . '. $specialization ?></strong></p>
						<h5 style="font-size:10px;"><?php 
						if($caste_category == 'Open'){
							echo 'Open';
						} else {
							echo 'Reserved';
						}?> Category Student Fee Year <br>
						<?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h5><br>
						<p>Paid into credit of Sir Vithaldas Thackersey College of Home Science</p>
						<p style="font-size:11px"><strong>S.B Account No. 4634101000018</strong></p><br>
						<h3 style="font-size:13px">A SUM OF RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3><br>
						<table style="font-size:11px;text-align:left;">
							<tr>
								<td colspan="2"><strong>Rupees in words: </strong><?php echo $words ?> ONLY</td>
							</tr>
							<tr>
								<td colspan="2"><strong>Name of candidate: </strong> <?php echo $student_name; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Department: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Specilization: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Division: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Roll No: </strong><?php echo $specialization; ?></td>
							</tr>
							<tr>
								<td colspan="2"><strong>Clerk/Cashier/College: </strong></td>
							</tr>
							<tr>
								<td><strong>Date: </strong></td>
								<td><?php echo $cheque_date; ?></td>
							</tr>
							<tr>
								<td><strong>D.D.No.: </strong></td>
								<td><?php echo $cheque;?></td>
							</tr>
							<tr>
								<td><strong>Name of the Bank: </strong></td>
								<td><?php echo $bank_name; ?></td>
							</tr>						
						</table>
						<table style="width:100%;font-size:10px;margin-top:20px">
							<tr>
								<td>For S.V.T College of Home Science</td>
								<td>Reserving Cashier<br>Seal of Bank</td>
							</tr>
						</table>
					</td>			
				</tr>			
			</table>
		
		
         	<!--<div class="content-wrapper">
				<div class="reciept_copies" >(For Account Copy)</div>
				<div class="preserved copies ">(To Be Preserved As Reciept)</div>
				<div class="bank_name"  style="text-transform: uppercase;"><?php echo $bank_name; ?><br>
					<span>Santacruz (W) Mumbai - 54</span>	
				</div>
				<div class="reciept_heading"><u>Reciept should be produced at the time of claiming deposits.<br>
				After final year Result collect deposit fee <b>within one month</b></u>	
				</div>
				<div class="college-header">
					<div class="logo"><img src ="<?php echo base_url(). 'assets/img/logo.png' ?>"></div>
					<div class="title"><h4>Sir Vithaldas Thackersey<br>
						College of Home Science
					</h4></div>
					<div class="address">
						S.N.D.T Women University,Sir Vithaldas Vidya Vihar,<br>
						Juhu Road, Santacruz (West) Mumbai - 400 049
					</div>
				</div>
				
				<div class="course"><?php echo $year .' . '. $specialization ?></div>
				
				<div class="category">
					<h5><?php 
					if($caste_category == 'Open'){
						echo 'Open';
					} else {
						echo 'Reserved';
					}?> 
					Category Student Fee Year <?php echo date("Y"); ?>-<?php echo date('Y', strtotime('+1 year')); ?></h5>
				</div>
				<div class="paid">
					<h5>Paid into credit of Sir Vithaldas Thackersey<br>College of Home Science</h5>
				</div>
				<div class="account">S.B Account No. 4634101000018</div>
				<div class="rupees-sum"><h3>A SUM OF RS. : <?php echo number_format($total_fee,'2') ; ?>/-</h3></div>
				<div class="reciept_table">
					<table>
						<tbody>
							<tr>
								<td>Rupees in words</td><td><?php echo $words ?> ONLY</td>	
							</tr>
							<tr>
								<td>Name of candidate(in Full)Block Letters & Name First</td>
								<td><?php echo $student_name; ?> </td>
							</tr>

							<tr>
								<td>Department</td>
								<td><?php echo $specialization; ?></td>	
							</tr>

							<tr>
								<td>Specilization</td>
								<td><?php echo $specialization; ?></td>	
							</tr>

							<tr>
								<td>Division</td>
								<td><?php echo $student_details[0]->division; ?></td>	
							</tr>

							<tr>
								<td>Roll No</td>
								<td><?php echo $student_id; ?></td>	
							</tr>
							Clerk/Cashier/College
							<tr>	
								<td>Date</td>
								<td><?php echo $cheque_date; ?></td>	
							</tr>

							<tr>
								<td>D.D.No.</td>
								<td><?php echo $cheque;?> </td>	
							</tr>

							<tr>
								<td>Name of the Bank</td>
								<td><?php echo $bank_name; ?></td>	
							</tr>
						</tbody>
					</table>
				</div>
				<div class="footer">
					<div class="footer-title">
						<h3>For Sir Vithaldas Thackersey<br>College of Home Science</h3>
					</div>
					<div class="cashier">
						<h3>Reserving Cashier<br>Seal of Bank</h3>
					</div>
				</div>
			</div>-->
    </body>
</html>