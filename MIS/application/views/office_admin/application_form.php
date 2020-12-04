<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
		</li>
		<li class="breadcrumb-item">Applications</li>
    </ol>

    <!-- DataTables Example -->
    <div class="card mb-3">
      <div class="card-header">
      	<div class="row">
				<div class="col-sm-6 col-md-3">
				  <i class="fa fa-file"></i> View Applications  
				</div>
				<!-- <div class="col-sm-6 col-md-9 text-right">
					<button type="button" data-toggle="modal" data-target="#addapplicationsModal" class="btn btn-primary">Add Application</button>
				</div> -->
			</div>
       
      </div>
      <div class="card-body">
        <div class="table-responsive">
          <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0" style="text-transform:capitalize;">
            <thead>
              <tr>
                <th>UserID</th>
                <th>User Name</th>
                <th>Application Type</th>             
                <th>Content</th>
                <th>Upload</th>
                <th>Status</th>
                <th>Action</th>
              </tr>
            </thead>          
			<?php if(!empty($applications)){?>
				<tbody>
				<?php 
				foreach ($applications as $key=>$value) {?>
				<tr>
					<td><?php echo $value->user_id; ?></td>
					<td><?php echo $value->user_name; ?></td>
					<td><?php echo $value->application_type; ?></td>
					<td><?php echo $value->application_reason ?></td>
					<td><a target="_blank" href="<?php echo base_url('uploads/applications/').$value->application_doc; ?>">
					<?php echo $value->application_doc ?></a></td>
					<td><?php echo $value->status ?></td>
					<td><button type="button" id="<?php echo $value->id; ?>" data-toggle="modal" data-id="<?php echo $value->id; ?>" data-userid="<?php echo $value->user_id; ?>" data-username="<?php echo $value->user_name; ?>" data-applicationtype="<?php echo $value->application_type; ?>" data-reason="<?php echo $value->application_reason; ?>" data-status="<?php echo $value->status; ?>" data-doc="<?php echo $value->application_doc; ?>" class="details_modal_button btn btn-success">Details</button></td>
					
					</tr>
				<?php  } ?>              
				</tbody>
			<?php }else{ ?>
				<tbody>     
					<tr class="odd"><td valign="top" colspan="5" class="dataTables_empty">You haven't any Applications filled yet</td></tr>
				</tbody>
			<?php } ?>
          </table>
        </div>
      </div>
      <div class="card-footer"></div>
    </div>
    <div class="modal fade" id="viewDetails" tabindex="-1" role="dialog" aria-labelledby="viewDetails" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Add Application</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="application_form" class="grid_form" autocomplete="off">
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label for="user_id" class="control-label">User ID</label>
									<input name="user_id" id="user_id" class="form-control" readonly>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="user_name" class="control-label">User Name</label>
									<input name="user_name" id="user_name" class="form-control" readonly>
								</div>
							</div>
						</div>
						<div class="row">					
							<div class="col-md-8">
								<div class="form-group">
									<label for="application_type" class="control-label">Application Type</label>
									<input name="application_type" id="application_type" class="form-control" readonly >
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label for="application_reason" class="control-label">Reason</label>
									<textarea name="application_reason" id="application_reason" class="form-control" readonly></textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label class="control-label" for="application_doc">Uploaded Document : </label>
										<a id="href_doc" target="_blank" href=""><span id="application_doc"></span></a>
								</div>	
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label class="control-label col-sm-6" for="status">Status : </label>
										<select name="status" data-rule-required="true" id="status" class="form-control" aria-required="true">
										<option value="Pending">Pending</option>
										<option value="Resolved">Resolved</option>
									</select>
								</div>	
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<input type="hidden" name="id" id="id">
									<input type="submit" class="btn btn-primary" name="add_application" id="add_application" value="Submit">
									<input type="button" class="btn btn-info" id="cancel_application" value="Cancel">
									<div class="print-error-msg text-center mt-4"></div>
								</div>
							</div>					
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>
  </div>
  <!-- /.container-fluid -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<script>
$(document).ready(function(){
	$(document).on('click', '.details_modal_button', function(e){
		e.preventDefault();
		var id = $(this).attr('data-id');
		var application_type = $(this).attr('data-applicationtype');
		var user_id = $(this).attr('data-userid');
		var user_name = $(this).attr('data-username');
		var application_reason = $(this).attr('data-reason');
		var status = $(this).attr('data-status');
		var application_doc = $(this).attr('data-doc');
		//console.log(application_reason);
		$('#application_type').val(application_type);
		$('#id').val(id);
		$('#user_id').val(user_id);
		$('#user_name').val(user_name);
		$('#application_reason').val(application_reason);
		$('#status').val(status);
		$('#application_doc').html(application_doc);
		$("#href_doc").attr("href", "<?php echo base_url('uploads/applications/'); ?>"+application_doc);
		$('#viewDetails').modal({
			show: true
		}); 
	});
	
})

</script>