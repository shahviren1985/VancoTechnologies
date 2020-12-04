<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('student/home'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Choose Elective</li>
		</ol>
			 		
		<div class="card">
			<div class="card-header" id="parent_feedback">
				Choose Elective
			</div>
			<?php if($this->session->flashdata('msg')): ?>
				<div class="alert alert-success">
					<?php echo $this->session->flashdata('msg'); ?>
				</div> 
			<?php endif; ?>   
		
			<div class="card-body">
			
				<?php 
					
					$course = $course_details[0]->course_name;
					if($course == ""){
						$course = "Regular";
					}
					
					if($user_details[0]->first_name == ""){
						$fname = "";
					}else{
						$fname = $user_details[0]->first_name;
					}
					
					if($user_details[0]->middle_name == ""){
						$mname = "";
					}else{
						$mname = $user_details[0]->middle_name;
					}
					
					if($user_details[0]->last_name == ""){
						$lname = '';
					}else{
						$lname = $user_details[0]->last_name;
					}
					
					$name = $fname.' '.$mname.' '.$lname;
					$month_list1 = array('Apr','May','Jun','Jul','Aug','Sep');
					$month_list2 = array('Oct','Nov','Dec','Jan','Feb','Mar');
					//$month_list2 = array('Apr','May','Jun','Jul','Aug','Sep');
					$cur_year = date('Y');
					$cur_month = date('M');
					$course_name = $user_details[0]->course_name;
					if($course_name == 'FY'){
						if(in_array($cur_month, $month_list1)){
							$sem = 1;
							$year = date('Y'); 	
						}elseif(in_array($cur_month, $month_list2)){
							$sem = 2;
							$year = date('Y')+1;
						}
					}elseif($course_name == 'SY'){
						if(in_array($cur_month, $month_list1)){
							$sem = 3;
							$year = date('Y'); 	
						}elseif(in_array($cur_month, $month_list2)){
							$sem = 4;
							$year = date('Y'); 	
							//$year = date('Y')+1;
						}
					}elseif($course_name == 'TY'){
						if(in_array($cur_month, $month_list1)){
							$sem = 5;
							$year = date('Y'); 	
						}elseif(in_array($cur_month, $month_list2)){
							$sem = 6;
							$year = date('Y'); 	
							//$year = date('Y')+1;
						}
					}
					
				?>
				
				<!--- code -->
				
				
			<form  action="<?php echo site_url('student/choose_elective'); ?>" method="get">
			<div class="row" style="margin:0 auto;padding:15px">
				<div class="col-md-12">
					
						<div class="row">
							<h2>Please Choose Elective</h2>
						</div>
						
						<div class="row">
							<div class="col-md-3">Student Name</div>
							<div class="col-md-4"><?php echo $user_details[0]->first_name; ?> <?php echo $user_details[0]->middle_name; ?> <?php echo $user_details[0]->last_name; ?></div>
						</div>
						<br/>
						<div class="row">
							<div class="col-md-3">Roll Number</div>
							<div class="col-md-4"><?php echo $user_details[0]->roll_number; ?></div>
						</div>
						<br/>
					   <div class="row">
							<div class="col-md-3">Program</div>
							<div class="col-md-4"> 
									<select name="program" class="form-control"  required >
									<option value="">--Select Program--</option>
									<?php $program =array('Regular','Honors' ); 
									foreach($program as $pg){?>
										<option value="<?php echo $pg; ?>" <?php echo  ( @$_REQUEST['program']==$pg  )?'selected':'';  ?> ><?php echo $pg; ?></option>
									<?php  }  ?>
									</select>
							</div>
						</div>
						<br/>
						
					   <div class="row">
							<div class="col-md-3"> <?php if($course_name == 'FY'){ ?>Choose Semester for General Electives <?php }elseif($course_name == 'SY'){ ?> Choose Semester Discipline Specific Electives (DSE)  <?php } ?></div>
							<div class="col-md-4"> 
								<select name="semester" class="form-control"  required >
									<option value="">--Select Semester--</option>
									<?php $semester =array('1'=>'Semester 1','2'=>'Semester 2','3'=>'Semester 3','4'=>'Semester 4','5'=>'Semester 5','6'=>'Semester 6'); 
									foreach($semester as $key=>$pg){?>
										<option value="<?php echo $key; ?>" <?php echo  ( @$_REQUEST['semester']==$key  )?'selected':'';  ?> ><?php echo $pg; ?></option>
									<?php  }  ?>
								</select>
							</div>
						</div>
						<br/>
						
						<div class="row">
							<div class="col-md-3"><input class="btn btn-success" type="submit" value="Search"></div>
							
						</div>
						<br/>

					</form>
				</div>
			</div>
		</form>		
			<?php  if(@$_REQUEST['program']!=''  && $_REQUEST['semester']!='') { ?>	
			<form  action="<?php echo site_url('student/choose_elective'); ?>" method="post">	
			<input type="hidden" name="post" value="post" />
			<input type="hidden" name="program" value="<?php echo @$_REQUEST['program'] ?>" />
			<input type="hidden" name="semester" value="<?php echo @$_REQUEST['semester'] ?>" />
			<div class="row">
				
				
				
				<?php  foreach($json as $jso){
						if($jso['specialisationCode']==$course_details[0]->short_form){   
						
						$department= array( 'HTM'=>'RM','IDRM'=>'RM','FND'=>'FND','MCE'=>'MCE','TAD'=>'TAD','DC'=>'HD','ECCE'=>'HD');
						?>
						<input type="hidden" name="specialisation" value="<?php echo $jso['specialisationCode']; ?>" />
						<input type="hidden" name="department" value="<?php echo $department[$jso['specialisationCode']]; ?>" />
						
					<?php 	 foreach($jso['ElectiveSubject'] as $key=>$display){ ?>
						<div class="col-md-12">	<b>Please choose Elective    <?php if(  count($jso['ElectiveSubject'])>1  && $_REQUEST['semester']<5    ){   echo $key+1;  }   ?> </b>
						</div>
						
						<?php 	 foreach($display['Subject'] as $elect=>$display_res){ 
									?>	
									<div class="col-md-12">
									<?php if($_REQUEST['semester'] > 0  && $_REQUEST['semester']<5  ){ ?>
									<input type="radio" name="elective<?php echo ($key+1) ?>"  <?php echo ($feedback['elective'.($key+1)] == $display_res['Code'] )?'checked':'';  ?>     value="<?php echo $display_res['Code'];  ?>">  <label >  <?php echo $display_res['Title'];  if( isset($display_res['Credit'])  ) {   ?> (Credits: <?php echo $display_res['Credit'];  ?>) <?php } ?>    </label> <br/>
									<?php }else{ ?>
									<input type="checkbox" name="elective<?php echo $elect+1; ?>" <?php echo ($feedback['elective'.($elect+1)] == $display_res['Code'] )?'checked':'';  ?>  value="<?php echo $display_res['Code'];  ?>">  <label for="RCVI10">  <?php echo $display_res['Title'];  if( isset($display_res['Credit'])  ) {   ?> (Credits: <?php echo $display_res['Credit'];  ?>) <?php } ?>  </label> <br/>
									<?php } ?>
									</div>
							<?php } ?>
							
				<?php }   ?>
				
				<div class="col form-group" style="float: left; margin-top: 30px;">
					<input type="submit" class="btn btn-success" value="Save" >
				</div>
				
				
			<?php 	}  } ?>
				
				
				
				
			</div>
			</form>
			<?php } ?>
			<!--- code -->	

			</div>
		</div>	
	</div>
	<script>
	    function Loadiframe(){
	        var semester = $("#ddSemester").children("option:selected").val();
	        var path = $("#iframe").attr("src");
	        path = path.substring(0,path.lastIndexOf("=")+1);
	        $("#iframe").attr("src",path+semester).show();
	    }
	</script>
<!-- /.container-fluid -->  