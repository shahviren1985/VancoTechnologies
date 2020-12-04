<?php defined('BASEPATH') OR exit('No direct script access allowed');
//echo "<pre>"; print_r($staff_details); echo "</pre>";
$casual_leave = $staff_details[0]->casual_leave_balance;
$sick_leave = $staff_details[0]->sick_leave_balance;
$paid_leave = $staff_details[0]->paid_leave_balance;
if(!empty($casual_leave) && empty($sick_leave) && empty($paid_leave)){
	$total = $staff_details[0]->casual_leave_balance;
}elseif(!empty($casual_leave) && !empty($sick_leave) && empty($paid_leave)){
	$total = $casual_leave + $sick_leave;
}elseif(!empty($sick_leave) && empty($casual_leave) && empty($paid_leave)){
	$total = $sick_leave;
}elseif(!empty($sick_leave) && !empty($casual_leave) && empty($paid_leave)){
	$total = $sick_leave + $casual_leave;
}elseif(!empty($paid_leave)&& empty($casual_leave) && empty($sick_leave)){
	$total = $paid_leave;
}elseif(!empty($paid_leave) && !empty($casual_leave) && empty($sick_leave)){
	$total = $paid_leave + $casual_leave;
}elseif(!empty($paid_leave) && !empty($casual_leave) && !empty($sick_leave)){
	$total = $paid_leave + $casual_leave + $sick_leave;
}else{
	$total = "";
}
?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('staff/staffs'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Leave Application</li>
		</ol>
		
		<div class="alert alert-danger print-error-msg" style="display:none"></div>	
		
		<div class="wrapper">
			<div class="card mb-3">
				<div class="card-header">
					<div class="row">
						<div class="col-sm-6 col-md-3">
							<i class="fas fa-list"></i>
							Your Leave Request List 
						</div>
						<div class="col-sm-6 col-md-9 text-right">
							<button type="button" data-toggle="modal" data-target="#addLeaveRequest" class="btn btn-primary">Apply for Leave</button>
						</div>
					</div>
				</div>
				<div class="card-body">
					<div class="leave_balance_container">
						<p><strong>Your Leave Balance:</strong></p>
						<div class="table-responsive">
							<table class="table table-bordered">
								<thead>
									<tr>
										<th>Casual Leave</th>
										<th>Sick Leave</th>
										<th>Paid Leave</th>
										<th>Total Leave Balance</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td><?php echo $staff_details[0]->casual_leave_balance; ?></td>
										<td><?php echo $staff_details[0]->sick_leave_balance; ?></td>
										<td><?php echo $staff_details[0]->paid_leave_balance; ?></td>
										<td><?php echo $total; ?></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
					<form class="filter_leaves" id="filter_leave" action="<?php echo base_url(); ?>staff/staffs/searchRequest" method="post">
						<div class="row align-item-center">
							<div class="col-md-12">
								<p><strong>Filter Search:</strong></p>
							</div>
							<div class="col-md-4">
								<select name="s_status" id="s_status" class="form-control">
									<option value="">Select Status</option>
									<option value="Pending" selected>Pending</option>
									<option value="HoD Approved">HoD Approved</option>
									<option value="OS Approved">OS Approved</option>
									<option value="All">All</option>
								</select>
							</div>				
						</div>
						<br>
						<div class="row align-item-center">
							<div class="col-md-1">
								<div class="form-group">
									<button class="btn btn-primary" id="search_request" name="search_request">Search</button>
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
					<div class="table-responsive">
						<table class="table table-bordered" id="dataTableLeaveRequest" width="100%" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Sr.No</th>  
									<th class="fs-16">Staff Name</th>													
									<th class="fs-16">Leave From</th>        
									<th class="fs-16">Leave To</th>          
									<th class="fs-16">HoD Approval Status</th> 
									<th class="fs-16">OS Approval Status</th> 
									<th class="fs-16">Principal Approval Status</th> 
									<th class="fs-16">Status</th> 
									<th class="fs-16">View Details</th> 
									<th class="fs-16">Date Applied</th> 
									<th class="fs-16">Action</th>	
								</tr>
							</thead>  
						</table>
					</div>
				</div>
				<div class="card-footer"></div>
			</div>
			<div class="modal fade" id="addLeaveRequest" tabindex="-1" role="dialog" aria-labelledby="addLeaveRequest" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<?php if($staff_details[0]->casual_leave_balance == 0 && $staff_details[0]->sick_leave_balance == 0 && $staff_details[0]->paid_leave_balance == 0){
								echo '<h5 class="modal-title" id="exampleModalLabel">No pending leave</h5>';
							}else{ ?>
								<h5 class="modal-title" id="exampleModalLabel">Apply for Leave</h5>
							<?php } ?>
							<button class="close" type="button" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">×</span>
							</button>
						</div>
						<div class="modal-body">
							<?php if($staff_details[0]->casual_leave_balance == 0 && $staff_details[0]->sick_leave_balance == 0 && $staff_details[0]->paid_leave_balance == 0){
								echo "<div class='no-balance'><h5 class='text-center' style='color:red;'>You don't have any pending leave.</h5></div>";
							}else{ ?>
								<form class="grid_form" method="post" id="leave_form" action="<?php echo base_url('leaveSubmit'); ?>">
									<div class="row">
										<div class="col-md-4">
											<div class="form-group">
												<input type="radio" class="" id="leave_type" name="leave_type" data-value="<?php echo $staff_details[0]->casual_leave_balance; ?>" value="Casual-Leave" <?php if($staff_details[0]->casual_leave_balance == 0){echo 'disabled';}else{ echo 'checked'; } ?>/>
												<label for="leave_type" class="control-label">Casual Leave</label>
											</div>

											<div class="form-group">
												<input type="radio" class="" id="leave_type" name="leave_type" data-value="<?php echo $staff_details[0]->sick_leave_balance; ?>" value="Sick-Leave" <?php if($staff_details[0]->sick_leave_balance == 0){echo 'disabled';}else{} ?> />
												<label for="leave_type" class="control-label">Sick Leave</label>
											</div>
											
											<div class="form-group">
												<input type="radio" class="" id="leave_type" name="leave_type" data-value="<?php echo $staff_details[0]->paid_leave_balance; ?>" value="Paid-Leave" <?php if($staff_details[0]->paid_leave_balance == 0){echo 'disabled';}else{} ?>/>
												<label for="leave_type" class="control-label">Paid Leave</label>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-6">
											<div class="form-group"> 
												<label for="leave_from" class="control-label">From</label>
												<input type="text" class="form-control" name="leave_from" id="leave_from" placeholder="Leave From"/>
											</div>
										</div>
										<div class="col-md-6">
											<div class="form-group"> 
												<label for="leave_to" class="control-label">To</label>
												<input type="text" class="form-control" name="leave_to" id="leave_to" placeholder="Leave To"/>
											</div>
										</div>
									</div>
										
									<div class="row hiddenrow">
										<div class="col-md-6">
											<div class="form-group"> 
												<div class="staff_detail">
													<input type="radio" class="" id="leave_for" name="leave_for" value="Half-Day" checked/>
													<label for="leave_for" class="control-label">Half Day</label>
												</div>
												<div class="staff_detail">
													<input type="radio" class="" id="leave_for" name="leave_for" value="Full-Day"/>
													<label for="leave_for" class="control-label">Full Day</label>
												</div>
											</div>
										</div>
									</div>
										
									<div class="row">
										<div class="col-md-12">
											<div class="form-group">
												<label for="leave_reason" class="control-label">Reason</label>
												<textarea name="leave_reason" id="leave_reason" class="form-control" placeholder="Reason"></textarea>
											</div>
										</div>
									</div>
										
									<div class="row">
										<div class="col-md-12">
											<div class="form-group">
												<label for="leave_pending_work" class="control-label">Backlog/Pending work</label>
												<textarea name="leave_pending_work" id="leave_pending_work" class="form-control" placeholder="Backlog/Pending work"></textarea>
											</div>
										</div>
									</div>
										
									<div class="row">
										<div class="col-md-12">
											<div class="form-group">
												<label for="leave_hand_given" class="control-label">Hand over given to</label>
												<textarea name="leave_hand_given" id="leave_hand_given" class="form-control" placeholder="Hand over given to"></textarea>
											</div>
										</div>
									</div>
									<input type="hidden" name="staff_id" id="staff_id" value="<?php echo $staff_details[0]->employee_code; ?>">
									<input type="hidden" name="role" id="role" value="<?php echo $staff_details[0]->role; ?>">
									<input type="hidden" name="days" id="days" value="">
									<div class="BTN_grid">
										<input type="submit" class="btn btn-info leave_application" value="Apply">
										<input type="button" class="btn btn-info" id="cancel_leave_request" value="Cancel">
										<div class="print-error-msg text-center" style="color:red;"></div>
									</div>
								</form>
							<?php } ?>
						</div>
					</div>
				</div>			
				<br>
			</div>			
			
			<div class="modal fade" id="viewDetails" tabindex="-1" role="dialog" aria-labelledby="viewDetails" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title" id="exampleModalLabel">Leave Details</h5>
								<button class="close" type="button" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">×</span>
							</button>
						</div>
						<div class="modal-body">
						<?php if(!empty($leave_request)){
								$leave_type = $leave_request[0]->leave_type;
								if(!empty($leave_request[0]->leave_from) && $leave_request[0]->leave_from != '0000-00-00'){
									$leave_from = date("d-m-Y", strtotime($leave_request[0]->leave_from));
								}else{
									$leave_from = "";
								}
								
								if(!empty($leave_request[0]->leave_to) && $leave_request[0]->leave_to != '0000-00-00'){
									$leave_to =  date('d-m-Y',strtotime($leave_request[0]->leave_to));
								}else{
									$leave_to = "";
								}
								//$leave_from = date('d-m-Y',strtotime($leave_request[0]->leave_from));
								//$leave_to = date('d-m-Y',strtotime($leave_request[0]->leave_to));
								$leave_reason = $leave_request[0]->leave_reason;
								$leave_pending_work = $leave_request[0]->leave_pending_work;
								$leave_hand_given = $leave_request[0]->leave_hand_given;
							}
						?>
							<form class="grid_form" method="post" id="view_leave_detail">
								<div class="row">	
									<div class="col-md-6">
										<div class="form-group">
											<label for="leave_type" class="control-label">Leave Type</label>
											<input type="text" class="form-control" value="<?php if(!empty($leave_type)){echo $leave_type;} ?>" readonly>
										</div>
									</div>	
									<div class="col-md-6">
										<div class="form-group">
											<label for="leave_for" class="control-label">Leave Starting Date</label>
											<input type="text" class="form-control" value="<?php if(!empty($leave_from)){echo $leave_from; }?>" readonly>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label for="leave_to" class="control-label">Leave End Date</label>
											<input type="text" class="form-control" value="<?php if(!empty($leave_to)){echo $leave_to; }?>" readonly>
										</div>
									</div>	
									<div class="col-md-6">
										<div class="form-group">
											<label for="leave_for" class="control-label">Reason</label>
											<input type="text" class="form-control" value="<?php if(!empty($leave_reason)){echo $leave_reason;} ?>" readonly>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label for="leave_pending_work" class="control-label">Backlog/Pending work</label>
											<input type="text" class="form-control" value="<?php if(!empty($leave_pending_work)){echo $leave_pending_work;} ?>" readonly>
										</div>
									</div>	
									<div class="col-md-6">
										<div class="form-group">
											<label for="leave_hand_given" class="control-label">Hand over given to</label>
											<input type="text" class="form-control" value="<?php if(!empty($leave_hand_given)){echo $leave_hand_given;} ?>" readonly>
										</div>
									</div>
								</div>
							</form>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
						</div>
					</div>
				</div>
			</div>			
			<br>
		</div>
	</div>
</div>
<!-- /.container-fluid -->  

<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<script>
$(document).ready(function(){
  $('#cancel_leave_request').on('click', function(){
	  $('#addLeaveRequest').modal('hide');
  });
});
$(document).on("click","#dataTableLeaveRequest td .remove",function(e) {
	e.preventDefault();
	var id = $(this).attr('data-id');
	var delete_url = $(this).attr('href');
	if(confirm('Are you sure you want to delete this record?')){
		$.ajax({
			url: delete_url,
			dataType:"json",
			type: 'DELETE',
			success: function(result){
				if(result.success == true){
					alert(result.msg);
					setTimeout(function() {
						window.location.reload();
					}, 2000);
				}else{
					alert(result.msg);
				} 
			}
		})
	}
	return false;
});
</script>
 