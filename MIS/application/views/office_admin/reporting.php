<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style>
  #search td { width: 20% !important; }
  #search .form-control { width: 100% !important; }
</style>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
      </li>
      <li class="breadcrumb-item active">Reporting</li>
    </ol>
	
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	
	<?php if($this->session->flashdata('error')): ?>
		<div class="alert alert-danger formValidation" role="alert">
			<?php echo $this->session->flashdata('error'); ?>
		</div>
	<?php endif; ?> 

    <!-- Area Chart Example-->
    <div class="card mb-3">
      <div class="card-header">
        <i class="fas fa-paste"></i>
        Reporting</div>
      <div class="card-body">
        <form id="search-report" action="<?php echo base_url("report/search-report"); ?>" class="form-inline" method="post" autocomplete="off">
            <table id="search" class="table table-striped table-bordered" style="width:100%">
                <thead>
                    <tr>
                        <th>Student Last Name</th>
                        <th>Challan No.</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><input type="text" name="name" id="name" class="form-control" placeholder="Enter Last Name"></td>
                        <td><input type="text" name="challan_no" id="challan_no" class="form-control" placeholder="eg. FYHO0001"></td>
                        <td><input type="text" name="start_date" id="start_date" class="form-control datepicker" placeholder="Start Date" autocomplete="off"></td>
                        <td><input type="text" name="end_date" id="end_date" class="form-control datepicker" placeholder="End Date" autocomplete="off"></td>
                        <td>
							<input type="submit" name="search_report" id="search-report-btn" class="btn btn-info btn-sm" value="Search">
							<input type="submit" name="export-report" id="export-report-btn" class="btn btn-success btn-sm" value="Export">
							<button type="reset" name="clear" class="btn btn-danger btn-sm" value=""><i class="fas fa-times"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
      </div>
      <div class="card-footer small text-muted"></div>
    </div>

    <?php if( isset($result) && count($result) ) { ?>
      <!-- DataTables Example -->
      <div class="card mb-3">
        <div class="card-header">
          <i class="fas fa-history"></i>
          Reporting Data 
        </div>
        <div class="card-body">
          <div class="table-responsive">
            <table class="table table-bordered" id="reportingDatatable" width="100%" cellspacing="0" style="text-transform:capitalize;">
              <thead>
                <tr class="t-heading">
				<th class="fs-16">Sr.No</th>
				<th class="fs-16">College Registration Number</th>
				<th class="fs-16">Course</th>
				<th class="fs-16">Category</th>
				<th class="fs-16">Challan Number</th>
				<th class="fs-16">Fee Paid Date</th>
				<th class="fs-16">Last Name</th>
				<th class="fs-16">First Name</th>
				<th class="fs-16">Father's Name</th>
				<th class="fs-16">Mother's Name</th>
				<th class="fs-16">Payment Mode</th>
				<th class="fs-16">Payment Type</th>
				<th class="fs-16">Transaction Ref. Number</th>
				<th class="fs-16">Transaction Status</th>
				<th class="fs-16">Bank Name</th>
				<th class="fs-16">Branch Name</th>
				<th class="fs-16">Cheque Number</th>
				<th class="fs-16">Cheque Date</th>
				<th class="fs-16">Remark</th>
				<th class="fs-16">Total Paid</th>
                </tr>
              </thead>         
              <tbody>
                <?php 
				$count = 0;                  
                foreach($result as $row) {                  
					$count++;
					$type = '';
					if($row->course_type){
						$type = $row->course_type.' - ';
					}
					$course_name = $row->year.' - '.$type.$row->short_form;?>
                  <tr class="t-row">
                    <td class="fs-16" align="center"><?php echo $count; ?></td>
					<td class="fs-16" align="center"><a href="<?php echo site_url('officeadmin/studentID/'.$row->student_id);?>"><?php echo strtoupper($row->student_id); ?></a></td>
                    <td class="fs-16" align="center"><?php echo $course_name; ?></td>
                    <td class="fs-16" align="center"><?php echo strtoupper($row->category); ?></td>
                    <td class="fs-16" align="center"><?php echo strtoupper($row->challan_number); ?></td>
                    <td class="fs-16" align="center"><?php echo date('d/m/Y',strtotime($row->fee_paid_date)); ?></td>
					<td class="fs-16" align="center"><?php echo ucfirst($row->lastname); ?></td>
                    <td class="fs-16" align="center"><?php echo ucfirst($row->firstname); ?></td>
                    <td class="fs-16" align="center"><?php echo ucfirst($row->middlename); ?></td>
                    <td class="fs-16" align="center"><?php echo ucfirst($row->mothername); ?></td>
                    <td class="fs-16" align="center"><?php echo strtoupper($row->payment_mode); ?></td>
                    <td class="fs-16" align="center"><?php echo strtoupper($row->payment_type); ?></td>
                    <td class="fs-16" align="center"><?php echo $row->transaction_ref_number; ?></td>
                    <td class="fs-16" align="center"><?php echo strtoupper($row->transaction_status); ?></td>
                    <td class="fs-16" align="center"><?php echo strtoupper($row->bank_name); ?></td>
                    <td class="fs-16" align="center"><?php echo $row->branch_name; ?></td>
                    <td class="fs-16" align="center"><?php echo $row->cheque_number; ?></td>
                    <td class="fs-16" align="center"><?php echo ($row->cheque_date != 0) ? date('d/m/Y',strtotime($row->cheque_date)) : ''; ?></td>
                    <td class="fs-16" align="center"><?php echo $row->remark; ?></td>
                    <td class="fs-16" align="center"><?php echo $row->total_paid; ?></td>
                  </tr>
                <?php } ?>
              </tbody>
            </table>
          </div>
        </div>
        <div class="card-footer"></div>
      </div>
    <?php } ?>
  </div>
  <!-- /.container-fluid -->