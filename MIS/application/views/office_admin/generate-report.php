<?php 
defined('BASEPATH') OR exit('No direct script access allowed'); 
	ob_start();
	header("Content-type: application/vnd.ms-excel");
	header("Content-Disposition: inline; filename=Reporting-file-".date('d-M-Y').".xls");
	if( isset($result) && count($result) ) { ?>
	<style>
	    .t-heading{ background-color: #3f96d8; color:#fff; }
	    .t-row { background-color: #def1ff; border-bottom:1px solid #dee2e6; }
	    .fs-16{ font-size:16px; }
	</style>
	<table border="0" class="table table-bordered table-responsive">
		<thead>
			<tr class="t-heading">
				<th class="fs-16">Sr.No</th>
				<th class="fs-16">Student ID</th>
				<th class="fs-16">Course ID</th>
				<th class="fs-16">Category</th>
				<th class="fs-16">Challan Number</th>
				<th class="fs-16">Previous Challan Number</th>
				<th class="fs-16">Fee Paid Date</th>
				<th class="fs-16">Firstname</th>
				<th class="fs-16">Middlename</th>
				<th class="fs-16">Lastname</th>
				<th class="fs-16">Mother's Name</th>
				<th class="fs-16">Payment Mode</th>
				<th class="fs-16">Payment Type</th>
				<th class="fs-16">Transaction Ref. Number</th>
				<th class="fs-16">Transaction Status</th>
				<th class="fs-16">Bank Name</th>
				<th class="fs-16">Branch Name</th>
				<th class="fs-16">Cheque Number</th>
				<th class="fs-16">Cheque Date</th>	
				<?php 
					$all_fees_head = $this->search_model->get_all_fees_head();
					$fee_head_Count = count($all_fees_head);
					 $headings = array_column($all_fees_head, 'fee_head');
					 $finalA = array();
					foreach ($headings as $k=> $h) {
						$finalA[$h] =0;
					}
					//echo "<pre>".$fee_head_Count."<br>"; print_r($all_fees_head); 
					//die;
					foreach ($all_fees_head as $k=> $head) {
				?>
					<th class="fs-16"><?php echo $head->fee_head; ?></th>
				<?php } ?>

				<th class="fs-16">Total #</th>
				<th class="fs-16">Total Amount</th>
				<th class="fs-16">Balance Amount</th>
				<th class="fs-16">Remark</th>
				<th class="fs-16">Total Paid</th>
				<th class="fs-16">Miscelleneous Fees</th>
			</tr>
		</thead>
		<tbody>
			<?php
				//echo "<pre>";
				$countResult = 0;	
				$totalSum = 0;			
				$totalAmount = 0;			
				$totalPaid = 0;			
				$totalBalance = 0;			
				$totalMiscelleneous = 0;			
				$totalCount = count($result);				
				foreach($result as $row) {
					$countResult++;
			?>
				<?php 
					$student_details = $this->search_model->get_student($row->student_id);
					$fee_details = $this->search_model->get_fees($row->course_id);

					//echo "<pre>".count($result); print_r($fee_details);echo "<br>".count($fee_details); 
					//die;
					$count = 0;
					$count2 = 0;
					$fee_head_total = 0;
					$pay_percentage = 0;
					$gst = 0;
					$grand_total = 0;
					if (empty($row->late_fee) || $row->late_fee=='') {
						$late_fee = 0;
					}else{
						$late_fee = $row->late_fee;
					}
					$category = strtolower($row->category);
					$caste = strtolower(@$student_details[0]->caste);
					$nri = @$student_details[0]->nri;
					if($nri){
						$permanent_country = strtoupper(trim(@$student_details[0]->permanent_country));
					}
					$saarc = array('Afghanistan', 'Bangladesh', 'Bhutan', 'India', 'Sri Lanka', 'Maldives', 'Nepal', 'Pakistan');
					$saarc_upper = array_change_key_case($saarc,CASE_UPPER);
					//print_r(json_decode( json_encode($at), true)); 
					//print_r(array_column($all_fees_head, 'fee_head'));
					// die;
				?>
				<tr class="t-row">
					<td class="fs-16" align="center"><?php echo $countResult; ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->student_id); ?></td>
					<td class="fs-16" align="center"><?php echo $row->course_id; ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->category); ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->challan_number); ?></td>
					<td class="fs-16" align="center"><?php echo $row->previous_challan_number; ?></td>
					<td class="fs-16" align="center"><?php echo $row->fee_paid_date; ?></td>
					<td class="fs-16" align="center"><?php echo ucfirst($row->firstname); ?></td>
					<td class="fs-16" align="center"><?php echo ucfirst($row->middlename); ?></td>
					<td class="fs-16" align="center"><?php echo ucfirst($row->lastname); ?></td>
					<td class="fs-16" align="center"><?php echo ucfirst($row->mothername); ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->payment_mode); ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->payment_type); ?></td>
					<td class="fs-16" align="center"><?php echo $row->transaction_ref_number; ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->transaction_status); ?></td>
					<td class="fs-16" align="center"><?php echo strtoupper($row->bank_name); ?></td>
					<td class="fs-16" align="center"><?php echo $row->branch_name; ?></td>
					<td class="fs-16" align="center"><?php echo $row->cheque_number; ?></td>
					<td class="fs-16" align="center"><?php echo $row->cheque_date; ?></td>
					<?php
						$tdCount=0;
						$rowSum=0;
						$headingArr = array();
						foreach ($all_fees_head as $all_fees) {
							$match =0;
							//echo $all_fees->fee_head."<br>";
							if(!empty($row->remark) && $row->remark != 'Partial Payment'){ ?>
								<td class="fs-16" align="center">-</td>
						 <?php	//}else if(empty($row->remark)){
								}else{
									$partialPayment = 1;
									if(!empty($row->remark) && $row->remark == 'Partial Payment'){
										$partialPayment = 2;
									}
								$tdCount++;
								if($nri == 'Yes'){
									if(in_array($permanent_country, $saarc_upper)){
										foreach( $fee_details as $key => $fee_detail ) {
											if ($fee_detail->fee_head == $all_fees->fee_head) {
												 $match =1;
												if ($fee_detail->fee_head == 'Late Fee') {
													$count++;
													$fee_head_total += $late_fee;
													$finalA[$fee_detail->fee_head] += $late_fee;
													?>	<td class="fs-16" align="center"><?php echo number_format($late_fee);?></td> <?php
												 }else{ 
												 	$count++;
													$fee_head_total += $fee_detail->nri_developing_country/$partialPayment;
													$finalA[$fee_detail->fee_head] += $fee_detail->nri_developing_country/$partialPayment;
													?>	<td class="fs-16" align="center"><?php echo number_format($fee_detail->nri_developing_country/$partialPayment);?></td> <?php 
												}//die;?>
											
									<?php 	}
										}
										if($match==0){ //echo $count."***"; ?>
											<td class="fs-16" align="center"><?php echo number_format(0);?></td>
										<?php }
									}else{
										foreach( $fee_details as $key => $fee_detail ) {
											if ($fee_detail->fee_head == $all_fees->fee_head) {
												 $match =1;
												if ($fee_detail->fee_head == 'Late Fee') {
													$count++;
													$fee_head_total += $late_fee;
													$finalA[$fee_detail->fee_head] += $late_fee;
													?>	<td class="fs-16" align="center"><?php echo number_format($late_fee);?></td> <?php 
												 }else{ 
												 	$count++;
													$fee_head_total += $fee_detail->nri_developed_country/$partialPayment;
													$finalA[$fee_detail->fee_head] += $fee_detail->nri_developed_country/$partialPayment;
													?>	<td class="fs-16" align="center"><?php echo number_format($fee_detail->nri_developed_country/$partialPayment);?></td> <?php 
												}//die;?>
											<?php }
										}
										if($match==0){ //echo $count."***"; ?>
											<td class="fs-16" align="center"><?php echo number_format(0);?></td>
										<?php }
									}
								} else {
			                    // price for open category
									if($category == 'open') {
										foreach( $fee_details as $key => $fee_detail ) {
											if ($fee_detail->fee_head == $all_fees->fee_head) {
												$match =1;
												//echo "<pre>"; print_r($fee_detail); echo $all_fees->fee_head.$count;
												//echo $fee_detail->fee_head;
												 //die;
												if ($fee_detail->fee_head == 'Late Fee') {
													//echo 'late'.$count;
													$count++;
													 //echo "<pre>"; print_r($fee_detail); echo $all_fees->fee_head.$count; die;
													$fee_head_total += $late_fee;
													$finalA[$fee_detail->fee_head] += $late_fee;
													?>	<td class="fs-16" align="center"><?php echo number_format($late_fee);?></td> <?php 
												 }else{ 
													$count++; 
													$fee_head_total += $fee_detail->amount/$partialPayment;
													$finalA[$fee_detail->fee_head] += $fee_detail->amount/$partialPayment;
													?>	<td class="fs-16" align="center"><?php echo number_format($fee_detail->amount/$partialPayment);?></td> <?php 
												}//die;
												?>
									<?php 	}
										}
										if($match==0){ //echo $count."***"; ?>
											<td class="fs-16" align="center"><?php echo number_format(0);?></td>
										<?php }
										} else { // price for reserved category with sc and st
										if(($caste == 'sc' || $caste == 'st') && $category == 'reserved') {
											foreach( $fee_details as $key => $fee_detail ) {
												if ($fee_detail->fee_head == $all_fees->fee_head) {
													$match =1;
													if ($fee_detail->fee_head == 'Late Fee') {
														
														$count++;
														$fee_head_total += $late_fee;
														$finalA[$fee_detail->fee_head] += $late_fee;
														?>	<td class="fs-16" align="center"><?php echo number_format($late_fee);?></td> <?php
													 }else{ 
													 	$count++;
														$fee_head_total += $fee_detail->reserved_amount/$partialPayment;
														$finalA[$fee_detail->fee_head] += $fee_detail->reserved_amount/$partialPayment;
														?>	<td class="fs-16" align="center"><?php echo number_format($fee_detail->reserved_amount);?></td> <?php 
													} //die;?>
										<?php 	}
											} 
											if($match==0){ ?>
											<td class="fs-16" align="center"><?php echo number_format(0);?></td>
										<?php }
										} else {// price for reserved category without sc and st
											foreach( $fee_details as $key => $fee_detail ) {
												if ($fee_detail->fee_head == $all_fees->fee_head) {
													$match =1;
													if ($fee_detail->fee_head == 'Late Fee') {
														
														$count++;
														$fee_head_total += $late_fee;
														$finalA[$fee_detail->fee_head] += $late_fee;
														?>	<td class="fs-16" align="center"><?php echo number_format($late_fee);?></td> <?php
													 }else{ 
													 	$count++;
														$fee_head_total += $fee_detail->amount/$partialPayment;
														$finalA[$fee_detail->fee_head] += $fee_detail->amount/$partialPayment;
														?>	<td class="fs-16" align="center"><?php echo number_format($fee_detail->amount);?></td> <?php 
													} //die;?>
										<?php 	}
											}
											if($match==0){ ?>
											<td class="fs-16" align="center"><?php echo number_format(0);?></td>
										<?php }
										}
									}
								}
							} //else{
						?>

					<?php 	//} 
						}  ?>
					<?php /* for ($j=$count; $j < $fee_head_Count; $j++) { 
							 if ($j == $fee_head_Count-1) {
								die('late j');
								$fee_head_total += $late_fee;
								$finalA[$fee_detail->fee_head] += $late_fee;
								?>	<td class="fs-16" align="center"><?php echo number_format($late_fee);?></td> <?php 
							 }else{  ?>
							<td class="fs-16" align="center"><?php echo number_format(0);?></td>
					<?php 	//} 
						} */?>
					<td class="fs-16" align="center"><?php echo number_format($fee_head_total, '2'); ?></td>
					<td class="fs-16" align="center"><?php echo $row->total_amount; ?></td>
					<td class="fs-16" align="center"><?php echo $row->balance_amount; ?></td>
					<td class="fs-16" align="center"><?php echo $row->remark; ?></td>
					<td class="fs-16" align="center"><?php echo $row->total_paid; ?></td>
					<td class="fs-16" align="center"><?php if(!empty($row->remark)){$totalMiscelleneous += $row->total_paid; echo $row->total_paid;}else{echo number_format(0);} ?></td>
					<?php 
						$totalSum += $fee_head_total;
						$totalAmount += $row->total_amount;
						if (!empty($row->balance_amount)) {
							$totalBalance += $row->balance_amount;
						}else{
							$totalBalance += 0;
						}
						$totalPaid += str_replace(',', '', $row->total_paid);
						if($totalCount>=$countResult) {
							if ($totalCount==$countResult) {
								$last_date = $result[0]->fee_paid_date;
							}else{
								$last_date = $result[$countResult]->fee_paid_date;	
							}
							if ($last_date!=$row->fee_paid_date || $totalCount==$countResult) {
						 ?>
							<tr>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<td class="fs-16" align="center"></td>
								<?php
								foreach ($all_fees_head as $all_fees) {
									$match2 =0;
									if(!empty($row->remark) && $row->remark != 'Partial Payment'){ ?>
										<td class="fs-16" align="center">-</td>
								 <?php	//}else if(empty($row->remark)){
										}else{
										if($nri == 'Yes'){
											if(in_array($permanent_country, $saarc_upper)){
												foreach( $fee_details as $key => $fee_detail ) {
													if ($fee_detail->fee_head == $all_fees->fee_head){
														$match2 =1;
														if ($fee_detail->fee_head == 'Late Fee') {
															$count2++;
															?>	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td> <?php
														 }else{ 
															$count2++;?>
															<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td>
												<?php 	}
													}
												}
												if($match2==0){ ?>
												<td class="fs-16" align="center"><?php echo number_format(0);?></td>
											<?php }
											}else{
												foreach( $fee_details as $key => $fee_detail ) {
													if ($fee_detail->fee_head == $all_fees->fee_head) {
														$match2 =1;
														if ($fee_detail->fee_head == 'Late Fee') {
															$count2++;
															?>	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td> <?php
														}else{ 
														$count2++; ?>
															<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td>
													<?php }
													}
												}
												if($match2==0){ ?>
													<td class="fs-16" align="center"><?php echo number_format(0);?></td>
												<?php }

											}
										} else {
					                    // price for open category
											if($category == 'open') {
												foreach( $fee_details as $key => $fee_detail ) {
													if ($fee_detail->fee_head == $all_fees->fee_head) {
														$match2 =1;
														if ($fee_detail->fee_head == 'Late Fee') {
															$count2++;
															?>	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td> <?php
														 }else{ 
															$count2++; ?>
																<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td>
												<?php 	}
													}
												}
												if($match2==0){ ?>
											<td class="fs-16" align="center"><?php echo number_format(0);?></td>
										<?php }
											} else { // price for reserved category with sc and st
												if(($caste == 'sc' || $caste == 'st') && $category == 'reserved') {
													foreach( $fee_details as $key => $fee_detail ) {
														if ($fee_detail->fee_head == $all_fees->fee_head) {
															$match2 =1;
															if ($fee_detail->fee_head == 'Late Fee') {
																$count2++;
															?>	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td> <?php
															 }else{ 
																$count2++; ?>
																	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td>
														<?php 	}
														}
													}
													if($match2==0){ ?>
													<td class="fs-16" align="center"><?php echo number_format(0);?></td>
												<?php }													
												} else {// price for reserved category without sc and st
													foreach( $fee_details as $key => $fee_detail ) {
														if ($fee_detail->fee_head == $all_fees->fee_head) {
															$match2 =1;
															if ($fee_detail->fee_head == 'Late Fee'){
																$count2++;
															?>	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td> <?php
															}else{ 
																$count2++; ?>
																	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head],'2');?></td>
														<?php 	}
														}
													}
													if($match2==0){ ?>
													<td class="fs-16" align="center"><?php echo number_format(0);?></td>
												<?php }
												}
											}
										}
									} //else{
								?>

							<?php 	//} 
								}  ?>
								<?php /* for ($j=$count2; $j < $fee_head_Count; $j++) { 
									if ($j == $fee_head_Count-1) {
										//$fee_head_total += $late_fee;
										//$finalA[$fee_detail->fee_head] += $late_fee;
										?>	<td class="fs-16" align="center"><?php echo number_format($finalA[$fee_detail->fee_head]);?></td> <?php 
									 }else{ ?>
									<td class="fs-16" align="center"><?php echo number_format(0);?></td>
							<?php 	} 
								} */?>
								<td class="fs-16" align="center"><?php echo number_format($totalSum, '2'); ?></td>
							<td class="fs-16" align="center"><?php echo number_format($totalAmount, '2'); ?></td>
							<td class="fs-16" align="center"><?php echo number_format($totalBalance, '2'); ?></td>
							<td class="fs-16" align="center"><?php //echo $row->remark; ?></td>
							<td class="fs-16" align="center"><?php echo number_format($totalPaid, '2'); ?></td>
							<td class="fs-16" align="center"><?php if(!empty($row->remark)){echo number_format($totalMiscelleneous, '2');}else{echo number_format(0);} ?></td>
							</tr>

					<?php 	$finalA = array();
							$totalSum =0;
							$totalAmount =0;
							$totalBalance =0;
							$totalPaid =0;
							$totalMiscelleneous =0;
							foreach ($headings as $k=> $h) {
								$finalA[$h] =0;
							}

								}
							}
					//} ?>

				</tr>
				<?php //die('end 1');
				} ?>				
		</tbody>
	</table>
<?php } ?>			