<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
  #search td { width: 20% !important; }
  #search .form-control { width: 100% !important; }
  .export_excel_file{float:right;margin-bottom:10px;}
  .leaveTd p{
  	margin-bottom: 0;
  }
  
ul.leaveUl {
    padding: 0 0 0 25px;
    margin: 0;
    text-align: left;
}
ul.leaveUl li {
    list-style-type: none;
}
</style>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
      </li>
      <li class="breadcrumb-item active">Staff Leave List</li>
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
					Staff Leave List 
				</div>
				<div class="col-sm-6 col-md-9 text-right">
					<!-- <button type="button" data-toggle="modal" data-target="#addstaffModal" class="btn btn-primary">Add Staff</button> -->
				</div>
			</div>
        </div>
        <div class="card-body">
			<form class="filter_staffs" id="filter_staff" action="" method="post">
				<div class="row align-item-center">
					<div class="col-md-12">
						<p><strong>Filter Search:</strong></p>
					</div>
					<div class="col-md-4">
						<label for="s_type"><strong> Start Date</strong></label>
						<input type="text" name="startdate" id="startdate" class="searchDate" value="<?php if(isset($startDate)){ echo $startDate;} ?>" autocomplete="off" required>
					</div>
					<div class="col-md-4">
						<label for="s_type"><strong> End Date</strong></label>
						<input type="text" name="enddate" id="enddate" class="searchDate" value="<?php if(isset($endDate)){ echo $endDate;} ?>" autocomplete="off" required>
					</div>
					<div class="col-md-1">
						<div class="form-group">
							<button class="btn btn-primary" id="" name="submit">Search</button>
						</div>
					</div>
					<div class="col-md-1">
						<div class="form-group">
							<a href="<?php echo base_url('officeadmin/staff-leave-list'); ?>" class="btn btn-secondary reset_filter_search">Clear Filter</a>
						</div>
					</div>					
				</div>
				<div class="processing_loader">Processing...</div>
			</form>
			<form method="post" action="<?php echo base_url(); ?>export_staff_leave_details" class="export_excel_files" id="export_excel_files">
				<input type="hidden" name="startdateExcel" id="startdateExcel">
				<input type="hidden" name="enddateExcel" id="enddateExcel">
				<input type="submit" class="btn btn-primary" id="export_excel_file1" value="Export to Excel" name="export_excel_file">
			</form>
			<script type="text/javascript">
				
			</script>
			<!-- <div class="col-md-12 mb-3 text-right">
				<a href="<?php // echo base_url(); ?>export_staff_leave_details" class="btn btn-primary">Export To Excel</a>
			</div> -->
			
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
				<table class="table table-bordered" id="dataTableStaffLeave" width="100%" cellspacing="0" style="text-transform:capitalize;">
					<thead>
						<tr class="t-heading">
							<th class="fs-16">Sr.No</th>  
							<th class="fs-16">Employee Code</th>							
							<th class="fs-16">Name</th>             
							<th class="fs-16">Type</th>
							<th class="fs-16">Leave Details</th>
						</tr>
					</thead>  
					<tbody>
			 
					  <?php 
					  	$emptyArray = $newArray= array();
						foreach ($leave_request AS $key => $line ) { 
						    if ( !in_array($line->staff_id, $emptyArray) ) { 
						        $emptyArray[] = $line->staff_id; 
						        $newArray[] = $line; 
						    } 
						}
					  	$i=1; 
					  foreach($newArray  as $row):
						//echo "<pre>"; print_r($row); echo "<pre>";
						// if ($row->type=='Permanent Teaching' || $row->type=='Permanent Non Teaching') {
							$totalcount=0;$str='';
							$leaveDetail = $toImplode = array();
							foreach ($leave_request as $value) {
								if ($value->staff_id==$row->employee_code) {
									$leaveDetail[]=$value;
									//$str .= $value->leave_type."-".$value->leave_from." to ".$value->leave_to.",";
									$totalcount++;	
								}
							}
							//$str = trim($str, ',');
							//echo "<pre>$str"; 
							
					  ?>
					  <tr>
						<td><?php echo $i; ?></td>        
						<td><?php echo $row->employee_code; ?></td>
						<td><?php echo $row->firstname." ".$row->lastname; ?></td>
						<td><?php echo $row->type; ?></td>
						<td style="" class="leaveTd"><?php echo "<p><b>Total Leave : ".$totalcount."</b></p>";
							$k=1;
							echo "<ul class='leaveUl'>";
							foreach ($leaveDetail as $key => $val) {
								echo "<li><strong>$k . Leave Type : </strong>".$val->leave_type."</li>
								<li style='margin-left: 16px; margin-bottom:3px'><strong>Leave Date : </strong>".date("d-m-Y", strtotime($val->leave_from))." - ".date("d-m-Y", strtotime($val->leave_to))."</li>";
								$k++;
							}
							echo "</ul>";
						//	echo "<pre>"; print_r($leaveDetail); ?></td>
					  </tr>
					  <?php $i++;
					  			
					   //}
					    endforeach; ?>
					  
					</tbody>	 			  
				</table>
			</div>
        </div>
        <div class="card-footer"></div>
      </div>
    <?php //} ?>

  </div>
 
	
 
  <!-- /.container-fluid -->
  <!--<script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>--> 