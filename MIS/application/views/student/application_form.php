<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('student/home'); ?>">Home</a>
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
				<div class="col-sm-6 col-md-9 text-right">
					<button type="button" data-toggle="modal" data-target="#addapplicationsModal" class="btn btn-primary">Add Application</button>
				</div>
			</div>
       
      </div>
      <div class="card-body">
        <div class="table-responsive">
          <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0" style="text-transform:capitalize;">
            <thead>
              <tr>
                <th>Sr. No.</th>
                <th>Application Type</th>             
                <th>Content</th>
                <th>Upload</th>
                <th>Status</th>
              </tr>
            </thead>          
			<?php if(!empty($applications)){?>
				<tbody>
				<?php 
				foreach ($applications as $key=>$value) {?>
				<tr>
					<td><?php echo $key+1; ?></td>
					<td><?php echo $value->application_type; ?></td>
					<td><?php echo $value->application_reason ?></td>
					<td><?php // echo $value->payment_mode ?></td>
					<td><?php echo $value->status ?></td>
					
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
    <div class="modal fade" id="addapplicationsModal" tabindex="-1" role="dialog" aria-labelledby="addapplicationsModal" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Add Application</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="application_form" class="grid_form" enctype="multipart/form-data" autocomplete="off">
						<div class="row">					
							<div class="col-md-8">
								<div class="form-group">
									<label for="application_type" class="control-label">Application Type</label>
									<select name="application_type" data-rule-required="true" id="application_type" class="form-control" aria-required="true">
										<option value="">---Select Application Type---</option>
										<option value="Type1">Type1</option>
										<option value="Type2">Type2</option>
									</select>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label for="application_reason" class="control-label">Reason</label>
									<textarea name="application_reason" id="application_reason" class="form-control" placeholder="Reason" data-rule-required="true"></textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label class="control-label col-sm-6" for="application_doc">Upload Document : </label>
										<input type="file" class="form-control" name="application_doc" id="application_doc" />
								</div>	
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<input type="hidden" name="user_id" value="<?php echo $student['userID']; ?>">
									<input type="hidden" name="user_name" value="<?php echo $student['first_name'].' '.$student['last_name']; ?>">
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