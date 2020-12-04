<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
  #search td { width: 20% !important; }
  #search .form-control { width: 100% !important; }
  .export_excel_file{float:right;margin-bottom:10px;}
</style>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
      </li>
      <li class="breadcrumb-item active">Staff List</li>
    </ol>
	
	<?php if($this->session->flashdata('msg')): ?>
		<div class="alert alert-success">
			<?php echo $this->session->flashdata('msg'); ?>
		</div> 
	<?php endif; ?>    
	
	<?php //if( isset($result) && count($result) ) {?>
      <!-- DataTables Example -->
      <div class="card mb-3">
        <div class="card-header">
			<div class="row">
				<div class="col-sm-6 col-md-3">
				  <i class="fas fa-list"></i>
					Staff List 
				</div>
				<div class="col-sm-6 col-md-9 text-right">
					<button type="button" data-toggle="modal" data-target="#addstaffModal" class="btn btn-primary">Add Staff</button>
				</div>
			</div>
        </div>
        <div class="card-body">
			<form class="filter_staffs" id="filter_staff" action="<?php echo base_url(); ?>office_admin/StaffExcelExport/searchStaff" method="post">
				<div class="row align-item-center">
					<div class="col-md-12">
						<p><strong>Filter Search:</strong></p>
					</div>
					<div class="col-md-4">
						<label for="s_type"><strong>Type</strong></label>
						<select name="s_type" id="s_type" class="form-control">
							<option value="">Select Type</option>
							<option value="Permanent Teaching">Permanent Teaching</option>
							<option value="Permanent Non Teaching">Permanent Non Teaching</option>
							<option value="Contract">Contract</option>
							<option value="Visiting Teaching">Visiting Teaching</option>
							<option value="Visiting Non Teaching">Visiting Non Teaching</option>
						</select>
					</div>
					<div class="col-md-4">
						<div class="form-group">
							<label for="s_state"><strong>State</strong></label>
							<select name="s_state" id="s_state" class="form-control">
								<option value="">State</option>
								<option value="Maharashtra" selected>Maharashtra</option>
								<option value="Outside Maharashtra">Outside Maharashtra</option> 
							</select>
						</div>
					</div>
					<div class="col-md-4">
						<label for="s_degree"><strong>Latest Degree</strong></label>
						<select name="s_degree" id="s_degree" class="form-control">
							<option value="">Degree</option>
							<option value="A">A</option>
							<option value="B">B</option> 
							<option value="C">C</option> 
							<option value="D">D</option> 
						</select>
					</div>
					<div class="col-md-1">
						<div class="form-group">
							<button class="btn btn-primary" id="search_staff" name="search_staff">Search</button>
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
			<?php /*<form method="post" action="<?php echo base_url(); ?>export_user_details" class="export_excel_file" id="export_excel_file">
				<input type="hidden" name="all_export" id="all_export" value="1">
				<input type="hidden" name="s_caste">
				<input type="hidden" name="s_religion">
				<input type="hidden" name="s_state">
				<input type="hidden" name="s_academic_year" value="<?php echo date('Y');?>">
				<input type="hidden" name="s_course_name">
				<input type="hidden" name="s_specialization">
				<input type="submit" class="btn btn-primary" id="export_excel_file1" value="Export to Excel" name="export_excel_file">
			</form> */?>
			<br>
			<div class="table-responsive">
				<table class="table table-bordered" id="dataTableStaff" width="100%" cellspacing="0" style="text-transform:capitalize;">
					<thead>
						<tr class="t-heading">
							<th class="fs-16">Sr.No</th>  
							<th class="fs-16">Employee Code</th>							
							<th class="fs-16">First Name</th>        
							<th class="fs-16">Last Name</th>        
							<th class="fs-16">User Name</th>        
							<th class="fs-16">Type</th>
							<th class="fs-16">Department</th>
							<th class="fs-16">Email</th>
							<th class="fs-16">State</th>
							<th class="fs-16">Mobile Number</th>
							<th class="fs-16">Date of Joining</th>
							<th class="fs-16">Date of Retire</th>
							<th class="fs-16">Qualification</th>
							<th class="fs-16">Role</th>
							<th class="fs-16">Pan Number</th>
							<th class="fs-16">Aadhaar Number</th>
							<th class="fs-16">Bank Account Number</th>
							<th class="fs-16">Bank Name</th>
							<th class="fs-16">IFSC Code</th>
							<th class="fs-16">Account Holder Name</th>
							<th class="fs-16">Leave Report</th>
						</tr>
					</thead>  
					<?php /*<tbody>
			 
					  <?php foreach($staff_data  as $row):
						//echo "<pre>"; print_r($row); echo "<pre>";
					  ?>
					  <tr>
						<td><?php echo $row->id; ?></td>        
						<td><?php echo $row->employee_code; ?></td>
						<td><?php echo $row->type; ?></td>
						<td><?php echo $row->email; ?></td>
						<td><?php echo $row->mobile_number; ?></td>
						<td><?php echo $row->date_of_joining; ?></td>
						<td><?php echo $row->date_of_retire; ?></td>
						<td><?php echo $row->qualification; ?></td>
						<td><?php echo $row->role; ?></td>
					  </tr>
					  <?php endforeach; ?>
					  
					</tbody> */?>	 			  
				</table>
			</div>
        </div>
        <div class="card-footer"></div>
      </div>
    <?php //} ?>
	
	<div class="modal fade" id="addstaffModal" tabindex="-1" role="dialog" aria-labelledby="addstaffModal" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Add Member</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="add_staff" enctype="multipart/form-data" autocomplete="off">
						<div class="row">					
							<div class="col-md-6">
								<div class="form-group">
									<label for="member_name" class="control-label">First Name</label>
									<input type="text" name="member_name" id="member_name" class="form-control" placeholder="First Name">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="member_lname" class="control-label">Last Name</label>
									<input type="text" name="member_lname" id="member_lname" class="form-control" placeholder="Last Name">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="username" class="control-label">UserName</label>
									<input type="text" name="username" id="username" class="form-control" value="User Name" readonly>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="employee_code" class="control-label">Employee Code</label>
									<input type="text" name="employee_code" id="employee_code" class="form-control" placeholder="Employee Code">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="department" class="control-label">Department</label>
									<select name="department" id="department" class="form-control">
										<option value="">Select Department</option>
										<option value="HD">Human Development</option>
										<option value="RM">Resource Management</option>
										<option value="FND">Food, Nutrition and Dietetics</option>
										<option value="MCE">Mass Communications and Extensions</option>
										<option value="TAD">Textile & Apparel Designing</option>
										<option value="SCI">Science</option>
										<option value="ENG">English</option>
										<option value="OA">Office Administration</option>
									</select>
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="type" class="control-label">Type</label>
									<select name="type" data-rule-required="true" id="type" class="form-control">
										<option value="">Select Type</option>
										<option value="Permanent Teaching">Permanent Teaching</option>
										<option value="Permanent Non Teaching">Permanent Non Teaching</option>
										<option value="Contract">Contract</option>
										<option value="Visiting Teaching">Visiting Teaching</option>
										<option value="Visiting Non Teaching">Visiting Non Teaching</option>
									</select>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="email" class="control-label">Email</label>
									<input type="email" name="email" id="email" class="form-control" placeholder="Email">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="mobile_num" class="control-label">Mobile Number</label>
									<input type="text" name="mobile_num" id="mobile_num" class="form-control" placeholder="Mobile Number">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="state" class="control-label">State</label>
									<select name="state" data-rule-required="true" id="state" class="form-control">
										<option value="">State</option>
										<option value="Maharashtra">Maharashtra</option>
										<option value="Outside Maharashtra">Outside Maharashtra</option>
									</select>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="joining_date" class="control-label">Date of Joining</label>
									<input type="text" name="joining_date" id="joining_date" class="form-control" placeholder="Date of joining">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="retire_date" class="control-label">Date of Retire</label>
									<input type="text" name="retire_date" id="retire_date" class="form-control" placeholder="Date of retire">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="qualification" class="control-label">Qualification</label>
									<input type="text" name="qualification" id="qualification" class="form-control" placeholder="Qualification">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="total_experience" class="control-label">Total Teaching Experience (In months)</label>
									<input type="number" name="total_experience" id="total_experience" class="form-control" placeholder="Total Teaching Experience">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="industry_experience" class="control-label">Total Industry Experience (In months)</label>
									<input type="number" name="industry_experience" id="industry_experience" class="form-control" placeholder="Total Industry Experience">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="role" class="control-label">Role</label>
									<select name="role" data-rule-required="true" id="role" class="form-control">
										<option value="">Select Role</option>
										<option value="Principal">Principal</option>
										<option value="Vice Principal">Vice Principal</option>
										<option value="Office Supretendent">Office Supretendent</option>
										<option value="CoE">CoE</option>
										<option value="HOD">HOD</option>
										<option value="Teaching Staff">Teaching Staff</option>
										<option value="Non-teaching Staff">Non-teaching Staff</option>
										<option value="Office-Admin">Office Admin</option>
									</select>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="pan" class="control-label">Pan Card Number</label>
									<input type="text" name="pan" id="pan" class="form-control" placeholder="Pan Card Number">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="aadhaar" class="control-label">Aadhaar Number</label>
									<input type="text" name="aadhaar" id="aadhaar" class="form-control" placeholder="Aadhaar Number">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="account_number" class="control-label">Bank Account Number</label>
									<input type="text" name="account_number" id="account_number" class="form-control" placeholder="Bank Account Number">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="bank_name" class="control-label">Bank Name</label>
									<input type="text" name="bank_name" id="bank_name" class="form-control" placeholder="Bank Name">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="ifsc_code" class="control-label">IFSC Code</label>
									<input type="text" name="ifsc_code" id="ifsc_code" class="form-control" placeholder="IFSC Code">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="account_holder_name" class="control-label">Account Holder Name</label>
									<input type="text" name="account_holder_name" id="account_holder_name" class="form-control" placeholder="Account Holder Name">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="casual_balance" class="control-label">Casual Leaves Balance</label>
									<input type="text" name="casual_balance" id="casual_balance" class="form-control" value="0">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="sick_balance" class="control-label">Sick Leaves Balance</label>
									<input type="text" name="sick_balance" id="sick_balance" class="form-control" value="0">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="paid_balance" class="control-label">Paid Leaves Balance</label>
									<input type="text" name="paid_balance" id="paid_balance" class="form-control" value="0">
								</div>
							</div>
							<div class="col-md-12">
								<div class="form-group">
									<input type="button" class="btn btn-primary" name="add_member" id="add_member" value="Add Member">
									<div class="print-error-msg text-center"></div>
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
  <!--<script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>--> 