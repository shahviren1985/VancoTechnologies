<?php defined('BASEPATH') OR exit('No direct script access allowed');
//echo "<pre>"; print_r($leave_request); echo "</pre>";
?>
<div id="content-wrapper">
	<div class="container-fluid">
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('staff/staffs'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Leave Request List</li>
		</ol>
		<?php if(validation_errors()):?>
		<div class="alert alert-danger formValidation" role="alert">
			<?php echo validation_errors();?> 
		</div>
		<?php endif;?>
		<div class="alert alert-danger print-error-msg" style="display:none"></div>	
		
		<div class="wrapper">
			<div class="card mb-3">
				<div class="card-header">
					<div class="row">
						<div class="col-sm-6 col-md-3">
							<i class="fas fa-list"></i>
							 Leave Request List 
						</div>
					</div>
				</div>
				<div class="card-body">
					<form class="filter_leaves_head" id="filter_leave_head" action="<?php echo base_url(); ?>staff/staffs/searchRequestHead" method="post">
						<div class="row align-item-center">
							<div class="col-md-12">
								<p><strong>Filter Search:</strong></p>
							</div>
							<div class="col-md-4">
								<select name="s_status" id="s_status" class="form-control">
									<option value="">Select Status</option>
									<option value="Pending" selected>Pending</option>
									<option value="All">All</option>
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
					<div class="table-responsive">
						<table class="table table-bordered" id="dataTableLeaveRequestHead" width="100%" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Sr.No</th>  
									<th class="fs-16">Staff Name</th>													
									<th class="fs-16">Leave From</th>        
									<th class="fs-16">Leave To</th>          
									<th class="fs-16">Status</th> 
									<th class="fs-16">View Details</th> 
									<th class="fs-16">Approve/Reject</th>  
									<th class="fs-16">Date Applied</th> 
								</tr>
							</thead>  
						</table>
					</div>
				</div>
				<div class="card-footer"></div>
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
							$leave_from = $leave_request[0]->leave_from;
							$leave_to = $leave_request[0]->leave_to;
							$leave_reason = $leave_request[0]->leave_reason;
							$leave_pending_work = $leave_request[0]->leave_pending_work;
							$leave_hand_given = $leave_request[0]->leave_hand_given;
							$leave_doc = $leave_request[0]->leave_doc;
						}
						?>
							<form class="grid_form" method="post" id="view_leave_detail">
								<div class="row">	
									<div class="col-md-12">
										<div class="form-group">
											<label for="leave_type" class="control-label">Leave Type</label>
											<input type="text" id="leave_type" class="form-control" value="<?php if(!empty($leave_type)){echo $leave_type;} ?>" readonly>
										</div>
									</div>	
									<div class="col-md-12">
										<div class="form-group">
											<label for="leave_for" class="control-label">Leave Starting Date</label>
											<input type="text" id="leave_for" class="form-control" value="<?php if(!empty($leave_from)){echo $leave_from; }?>" readonly>
										</div>
									</div>
									<div class="col-md-12">
										<div class="form-group">
											<label for="leave_to" class="control-label">Leave End Date</label>
											<input type="text" id="leave_to" class="form-control" value="<?php if(!empty($leave_to)){echo $leave_to; }?>" readonly>
										</div>
									</div>	
									<div class="col-md-12">
										<div class="form-group">
											<label for="leave_reason" class="control-label">Reason</label>
											<textarea id="leave_reason" class="form-control"><?php if(!empty($leave_reason)){echo $leave_reason;} ?></textarea>
										</div>
									</div>
									<div class="col-md-12">
										<div class="form-group">
											<label for="leave_pending_work" class="control-label">Backlog/Pending work</label>
											<textarea id="leave_pending_work" class="form-control"><?php if(!empty($leave_pending_work)){echo $leave_pending_work;} ?></textarea>
										</div>
									</div>	
									<div class="col-md-12">
										<div class="form-group">
											<label for="leave_hand_given" class="control-label">Hand over given to</label>
											<textarea id="leave_hand_given" class="form-control"><?php if(!empty($leave_hand_given)){echo $leave_hand_given;} ?></textarea>
										</div>
									</div>	
									<div class="col-md-12" id="leave_docDiv" style="display: none;">
										<div class="form-group">
											<label for="leave_doc" class="control-label">Leave Document :</label>
											<a href="" target="_blank" id="leave_dochref"> <span id="leave_doc"></span></a>
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

<!-- <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script> -->
<script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js');?>"></script>

<script>
$(document).ready(function(){
	$(document).on('change', '#approve_reject .radio', function(){
		var cval = $(this).val();
		var comment = $(this).parent().parent().find("textarea");
		if(cval == 'Rejected'){
			comment.attr("required", "required");
		}else{
			comment.removeAttr("required");
		}
	})
	
	$(document).on('click', '#apv_reject_save', function(e){
		e.preventDefault();	
		var value = $('apv_rej:checked').val();
		if ($('#apv_rej:checked').length == 0) {
			alert("Please select Approve or Reject option");
		}else{
			var form = $(this).parent().parent();
			if(form.valid()){					
				var data = form.serialize();
				$('#apv_reject_save').hide();
				jQuery.ajax({				
					url: '<?php echo base_url(); ?>staff/staffs/application_reject_approve',
					type: "POST",
					dataType:"json",
					data:data,
					success: function(data){
						alert(data.msg);
						setTimeout(function() {
							//window.location.reload();
						}, 2000);
					}
				}); 
			}
		}
	})
	
	$(document).on('click', '.details_modal_button', function(e){
		//alert('sd ak');
		e.preventDefault();
		var leave_type = $(this).attr('data-leavetype');
		var leave_from = $(this).attr('data-from');
		var leave_to = $(this).attr('data-to');
		var leave_to = $(this).attr('data-to');
		var leave_reason = $(this).attr('data-reason');
		var leave_pending_work = $(this).attr('data-backlog');
		var hand_given = $(this).attr('data-handover');
		var leave_doc = $(this).attr('data-leavedoc');
		
		$('#leave_type').val(leave_type);
		$('#leave_for').val(leave_from);
		$('#leave_to').val(leave_to);
		$('#leave_reason').val(leave_reason);
		$('#leave_pending_work').val(leave_pending_work);
		$('#leave_hand_given').val(hand_given);
		if (leave_doc!='') {
			$('#leave_docDiv').show();
			$('#leave_doc').html(leave_doc);	
			$('#leave_dochref').attr('href',"<?php echo base_url('uploads/leave_documents/');?>"+leave_doc);
		}else {
			$('#leave_docDiv').hide();
			$('#leave_doc').html('');
			$('#leave_dochref').attr('href',"");
		}
		
		$('#viewDetails').modal({
			show: true
		}); 
	})
	
})

/*$(document).on("click","#dataTableLeaveRequest td .remove",function(e) {
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
}); */
</script>
 