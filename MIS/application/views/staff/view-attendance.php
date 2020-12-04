<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('staff/staffs'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">View Attendance</li>
		</ol>
			 		
		<div class="card">
			<div class="card-header" id="parent_feedback">
				View Attendance
			</div>
			<div class="card-body">
				<?php $employee_code = $staff[0]->employee_code; ?>
				<iframe src="https://vancotech.com/Exams/bachelors/ui/Attendance.html?bioEmpCode=<?php echo $employee_code;?>" width="100%" height="500px"></iframe>  
			</div>
		</div>	
	</div>
<!-- /.container-fluid -->  