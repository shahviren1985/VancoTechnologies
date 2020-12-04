<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('student/home'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Feedback</li>
		</ol>
			 		
		<ul class="nav nav-tabs card-header-tabs" id="feedback_tab_list" role="tablist">
			<li class="nav-item">
				<a class="nav-link active" href="#students_feed" role="tab" aria-controls="students_feed" aria-selected="true">Students Feedback</a>
			</li>
			<li class="nav-item">
				<a class="nav-link" href="#parent_feed" role="tab" aria-controls="parent_feed" aria-selected="true">Parent Feedback</a>
			</li>
		</ul>			
		
		<div class="tab-content" id="myTabContent">
			<div class="tab-pane fade show active p-3" id="students_feed" role="tabpanel" aria-labelledby="students_feed">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=Student&crn=<?php echo $this->session->userdata('userID');?>" width="100%" height="500px"></iframe>
				</div>
			</div>
			<div class="tab-pane fade p-3" id="parent_feed" role="tabpanel" aria-labelledby="parent_feed">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=Parent&crn=<?php echo $this->session->userdata('userID');?>" width="100%" height="500px"></iframe>
				</div>
			</div>
		</div>
	</div>
<!-- /.container-fluid -->  