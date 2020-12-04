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
			 		
		<div class="card">
			<div class="card-header" id="parent_feedback">
				Applications
			</div>
			<div class="card-body">
				<?php $data = $this->session->userdata();
					if($data['connectionString'] == 'clg_db2'){ ?>
						<iframe src="https://vancotech.com/Exams/bachelors/ui/Applications.html?crn=<?php echo $data['userID'];?>" width="100%" height="500px"></iframe>
					<?php }elseif($data['connectionString'] == 'clg_db1'){ ?>
						<iframe src="https://vancotech.com/Exams/masters/ui/Applications.html?crn=<?php echo $data['userID'];?>" width="100%" height="500px"></iframe>
					<?php }	 ?> 
			</div>
		</div>	
	</div>
<!-- /.container-fluid -->  