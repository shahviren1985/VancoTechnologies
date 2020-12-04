<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
	.dashboardDiv {
		height: 400px;
		overflow-x: hidden;
		overflow-y: scroll;
		margin-bottom: 30px;
		margin-top: 20px;
	}
</style>
<div id="content-wrapper">
	<div class="container-fluid">
        <div class="card mb-3">
			<div class="card-header">
				<i class="fas fa-list"></i>
				Summary List 
			</div>
			<div class="card-body">
				<div class="row summary_table">				
					<div class="col-md-7">
						<h5>Staff Attendance- <?php echo date('d-m-Y'); ?></h5>
						<div class="dashboardDiv">	
							<form class="filter_leaves_head" id="filter_leave_head" action="<?php echo base_url(); ?>staff/staffs/searchRequestHead" method="post">
								<div class="row align-item-center">
									<div class="col-md-12">
										<p><strong>Filter Search:</strong></p>
									</div>
									<div class="col-md-4">
										<select name="s_status" id="s_status" class="form-control">
											<option value="">Select Status</option>
											<option value="Absent" selected>Absent</option>
											<option value="Present">Present</option>
										</select>
									</div>				
								</div>
								<br>
								<div class="row align-item-center">
									<div class="col-md-1">
										<div class="form-group">
											<button class="btn btn-primary" id="search_request_head" name="search_request_head">Search</button>
										</div>
									</div>
									<div class="col-md-1">
										<div class="form-group">
											<button type="reset" class="btn btn-secondary reset_filter_search">Clear Filter</button>
										</div>
									</div>	
								</div>
								<div class="processing_loader">Processing...</div>
							</form>
							<br>						
							<table class="table table-bordered" width="100%" id="dataTableAttendanceD" cellspacing="0" style="text-transform:capitalize;">
								<thead>
									<tr class="t-heading">
										<th class="fs-16">Employee Code </th>
										<th class="fs-16">Name</th>
										<th class="fs-16">Department</th>
										<th class="fs-16">Teaching</th>
										<th class="fs-16">Permananent</th>
										<th class="fs-16">Status </th>
									</tr>
								</thead>	
								<tbody>
									<?php foreach($staff_attendance_data as $attendance){ ?>
									<tr>
										<td><?php echo $attendance->employee_code;?></td>
										<td><?php echo $attendance->firstname.' '.$attendance->lastname;?></td>
										<td><?php echo $attendance->department;?></td>
										<td><?php echo $attendance->role;?></td>
										<td><?php echo $attendance->type;?></td>
										<td><?php echo 'Present'; // $attendance->type;?></td>
									</tr>
									<?php } ?>
								</tbody>						
							</table>
						</div>
						<h5>Upcoming Leaves</h5>
						<div class="dashboardDiv">						
							<table class="table table-bordered" width="100%" id="example" cellspacing="0" style="text-transform:capitalize;">
								<thead>
									<tr class="t-heading">
										<th class="fs-16">Name</th>
										<th class="fs-16">Department</th>
										<th class="fs-16">Leave Type</th>
										<th class="fs-16">From</th>
										<th class="fs-16">To</th>
										<th class="fs-16">Status</th>
									</tr>
								</thead>							
								<tbody>
									<?php foreach($leave_request as $value):?>
									<tr>
										<td><?php echo $value->firstname.' '.$value->lastname;?></td>
										<td><?php echo $value->department;?></td>
										<td><?php echo $value->leave_type;?></td>
										<td><?php echo date('d-m-Y',strtotime($value->leave_from));?></td>
										<td><?php echo date('d-m-Y',strtotime($value->leave_to));?></td>
										<td><?php echo $value->status;?></td>
									</tr>
									<?php endforeach;?>
								</tbody>
							</table>
						</div>
					</div>
					
					<div class="col-md-5">
						
						<h5>Leave Ananlysis</h5>						
						<table class="table table-bordered" width="100%" id="transactionTable" cellspacing="0" <table class="table table-bordered" width="100%" id="casteTable" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Graph</th>
								</tr>
							</thead>							
						</table>
						
						<h5>Library Track Record</h5>						
						<?php if(!empty($recent_transaction[0])):?>
						<table class="table table-bordered" width="100%" id="transactionTable" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Name</th>
									<th class="fs-16">Challan No.</th>
									<th class="fs-16">Amount</th>
									<th class="fs-16">Date</th>
								</tr>
							</thead>
							<tbody>
								<?php foreach($recent_transaction as $rcnt_trans):?>
								<tr>
									<td><?php echo $rcnt_trans->firstname.' '.$rcnt_trans->lastname;?></td>
									<td><?php echo $rcnt_trans->challan_number;?></td>
									<td>&#x20b9; <?php echo number_format($rcnt_trans->total_amount, 2); ?></td>
									<td><?php echo date('d-m-Y',strtotime($rcnt_trans->fee_paid_date));?></td>
								</tr>
								<?php endforeach;?>
							</tbody>
						</table>
						<?php else:?>
						No Record found
						<?php endif;?>
					</div>					
				</div>
			</div>
		</div>
	</div>
	<!-- /.container-fluid --> 