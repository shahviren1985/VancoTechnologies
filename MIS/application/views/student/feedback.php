<?php defined('BASEPATH') OR exit('No direct script access allowed');
/* echo "<pre>"; print_r($user_details[0]); echo "</pre>";
die; */

$month_list1 = array('Apr','May','Jun','Jul','Aug','Sep');
$month_list2 = array('Oct','Nov','Dec','Jan','Feb','Mar');
$cur_year = date('Y');
$cur_month = date('M');
$admission_year = $user_details[0]->admission_year;
$difference = $cur_year - $admission_year;
$spec = substr($course_details[0]->specialization,0,1);

switch($spec){
    case "D":
        $spec = "DC";   
        break;
    case "E":
        $spec = "ECCE";
        break;
    case "F":
        $spec = "FND";
        break;
    case "H":
        $spec = "HTM";
        break;
    case "I":
        $spec = "IDRM";
        break;
    case "M":
        $spec = "MCE";
        break;
    case "T":
        $spec = "TAD";
        break;
        
}


if($difference == 0){
	if(in_array($cur_month, $month_list1)){
		$sem = 'odd';
	}else{
		$sem = 'even';
	}
}elseif($difference == 1){
	if(in_array($cur_month, $month_list1)){
		$sem = 'odd';
	}else{
		$sem = 'even';
	}
}elseif($difference == 2){
	if(in_array($cur_month, $month_list1)){
		$sem = 'odd';
	}else{
		$sem = 'even';	
	}
}
 ?>
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
				<a class="nav-link active" href="#curr_feed" role="tab" aria-controls="curr_feed" aria-selected="true">Curriculum Feedback</a>
			</li>
			<!--<li class="nav-item">
				<a class="nav-link" href="#students_feed" role="tab" aria-controls="students_feed" aria-selected="true">Students Feedback</a>
			</li>
			<li class="nav-item">
				<a class="nav-link" href="#parent_feed" role="tab" aria-controls="parent_feed" aria-selected="true">Parent Feedback</a>
			</li>
			<li class="nav-item">
				<a class="nav-link" href="#teacher_feed" role="tab" aria-controls="teacher_feed" aria-selected="true">Teachers Feedback</a>
			</li>-->
			<?php if($user_details[0]->course_name =='TY'){ ?>
			<!--<li class="nav-item">
				<a class="nav-link" href="#outcome_students" role="tab" aria-controls="outcome_students" aria-selected="true">Program Outcomes (Students)</a>
			</li>
			<li class="nav-item">
				<a class="nav-link" href="#outcome_parents" role="tab" aria-controls="outcome_parents" aria-selected="true">Program Outcomes (Parents)</a>
			</li>-->
			<?php } ?>
		</ul>			
		<div class="tab-content" id="myTabContent">
			<div class="tab-pane fade show active p-3" id="curr_feed" role="tabpanel" aria-labelledby="students_feed">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=StudentCurriculum<?php echo $user_details[0]->course_name == "TY" ? "SY" : $user_details[0]->course_name == "SY" ? "FY" : "SY" ?>&crn=<?php echo $this->session->userdata('userID');?>&year=<?php echo $user_details[0]->academic_year; ?>&sp=<?php echo $spec; ?>" width="100%" height="500px"></iframe>
				</div>
			</div>
			<div class="tab-pane fade show active p-3" id="students_feed" role="tabpanel" aria-labelledby="students_feed">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=SSS<?php echo $user_details[0]->course_name?>&crn=<?php echo $this->session->userdata('userID');?>&year=<?php echo $user_details[0]->academic_year; ?>&sp=<?php echo $spec; ?>" width="100%" height="500px"></iframe>
				</div>
			</div>
			
			<div class="tab-pane fade p-3" id="parent_feed" role="tabpanel" aria-labelledby="parent_feed">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=Parent&crn=<?php echo $this->session->userdata('userID');?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe>
				</div>
			</div>
			<div class="tab-pane fad p-3" id="teacher_feed" role="tabpanel" aria-labelledby="teacher_feed">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?sem=<?php echo $sem; ?>&specialization=<?php echo $course_details[0]->specialization; ?>&course=<?php echo $course_details[0]->course_type; ?>&cc=102&courseyear=<?php echo $course_details[0]->year; ?>&type=Teacher&crn=<?php echo $this->session->userdata('userID');?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe>
				</div>
			</div>
			<div class="tab-pane fade p-3" id="outcome_students" role="tabpanel" aria-labelledby="outcome_students">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=AlumniPOMCE&crn=<?php echo $this->session->userdata('userID');?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe>
				</div>
			</div>
			<div class="tab-pane fade p-3" id="outcome_parents" role="tabpanel" aria-labelledby="outcome_parents">
				<div class="card-body">
					<iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=ParentsPOMCE&crn=<?php echo $this->session->userdata('userID');?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe>
				</div>
			</div>
		</div>
	</div>
<!-- /.container-fluid -->  