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
				<th class="fs-16">Total Amount</th>
				<th class="fs-16">Balance Amount</th>
				<th class="fs-16">Remark</th>
				<th class="fs-16">Total Paid</th>
			</tr>
		</thead>
		<tbody>
			<?php
				$count = 0;				
				foreach($result as $row) {
				$count++;?>
				<tr class="t-row">
					<td class="fs-16" align="center"><?php echo $count; ?></td>
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
					<td class="fs-16" align="center"><?php echo $row->total_amount; ?></td>
					<td class="fs-16" align="center"><?php echo $row->balance_amount; ?></td>
					<td class="fs-16" align="center"><?php echo $row->remark; ?></td>
					<td class="fs-16" align="center"><?php echo $row->total_paid; ?></td>
				</tr>
			<?php } ?>				
		</tbody>
	</table>
<?php } ?>			