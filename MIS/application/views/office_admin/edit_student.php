<?php defined('BASEPATH') OR exit('No direct script access allowed');
	//echo "<pre>"; print_r($student); echo "</pre>";
 ?>
<style>
.semster-tabs{
	margin-top:20px;
}
</style>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
		</li>
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('officeadmin/export-student-list'); ?>">Students List</a>
		</li>
		<li class="breadcrumb-item active"> Edit Student Details</li>
    </ol>

    <!-- Area Chart Example-->
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	
	<div class="alert alert-danger print-error-msg" style="display:none"></div>	
	
    <div class="card mb-3">
		<div class="card-header">	
			<ul class="nav nav-tabs card-header-tabs" id="student_tab_list" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" href="#details" role="tab" aria-controls="details" aria-selected="true">Students Details</a>
				</li>
				<li class="nav-item">
					<a class="nav-link"  href="#accounts" role="tab" aria-controls="accounts" aria-selected="false">Accounts</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" href="#documents" role="tab" aria-controls="documents" aria-selected="false">Documents</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" href="#feedback" role="tab" aria-controls="feedback" aria-selected="false">Feedback</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" href="#performance" role="tab" aria-controls="performance" aria-selected="false">Academic Performance</a>
				</li>
				<?php /*<li class="nav-item">
					<a class="nav-link" href="#applications" role="tab" aria-controls="applications" aria-selected="false">Applications</a>
				</li> */?>
			</ul>			
		</div>
				
		<div class="tab-content" id="myTabContent">
			<div class="tab-pane fade show active p-3" id="details" role="tabpanel" aria-labelledby="details">				
				<i class="fas fa-edit"></i><span class="edit_data">Edit</span> Student Details
				<span id="edit_data">Edit</span>
				<div class="small text-muted text_disabled">
				<?php if( isset($student) && count($student) ) {?>			
					<?php $attributes = array('id' => 'update_student_details');
						echo form_open_multipart(site_url('/officeadmin/studentlist/studentdetails/edit/'.$student['id']), $attributes); ?>	
						<?php //echo "<pre>"; print_r($student); echo "</pre>";
						?>
						<div class="row">
							<div class="col-md-12">
								<h3 class="add-info">Personal Information</h3>	
							</div>
						</div>
						<div class="row">
							<div class="col-md-8">
								<div class="row">
									<div class="col-md-4">
										<div class="form-group"> 
											<label for="PRN_number" class="control-label">PRN Number</label>
											<input type="text" name="PRN_number" class="form-control" value="<?php echo $student['PRN_number'] ?>"  data-rule-number="true" />
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group"> 
											<label for="college_reg" class="control-label">College Reg. No.</label>
											<input type="text" name="college_reg" data-old-reg="<?php echo $student['userID'] ?>" id="college_reg" class="form-control" value="<?php echo $student['userID'] ?>" readonly />
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group"> 
											<label for="roll_number" class="control-label">Roll Number</label>
											<input type="text" name="roll_number" class="form-control" value="<?php echo $student['roll_number'] ?>" data-rule-required="true" data-rule-maxlength="3" data-rule-number="true" />
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label for="lastname" class="control-label">Lastname</label>
											<input type="text" name="lastname" class="form-control" value="<?php echo $student['last_name'] ?>" data-rule-required="true" />
										</div>	
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label for="firstname" class="control-label">Firstname</label>
											<input type="text" name="firstname" class="form-control" value="<?php echo $student['first_name'] ?>" data-rule-required="true" />
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label for="division" class="control-label">Division</label>
											<select name="division" data-rule-required="true" id="division" class="form-control">
												<option value="">Select Division</option>
												<option value="A-I" <?php echo ($student['division'] == 'I') ? 'selected="selected"' : ''; ?>>I</option>
												<option value="A-I" <?php echo ($student['division'] == 'A-I') ? 'selected="selected"' : ''; ?>>A - I</option>
												<option value="A-II" <?php echo ($student['division'] == 'A-II') ? 'selected="selected"' : ''; ?>>A - II</option>
												<option value="B" <?php echo ($student['division'] == 'B') ? 'selected="selected"' : ''; ?>>B</option>
												<option value="C-I" <?php echo ($student['division'] == 'C-I') ? 'selected="selected"' : ''; ?>>C - I</option>
												<option value="C-II" <?php echo ($student['division'] == 'C-II') ? 'selected="selected"' : ''; ?>>C - II</option>
												<option value="C-III" <?php echo ($student['division'] == 'C-III') ? 'selected="selected"' : ''; ?>>C - III</option>
												<option value="D" <?php echo ($student['division'] == 'D') ? 'selected="selected"' : ''; ?>>D</option>
												<option value="D-I" <?php echo ($student['division'] == 'D-I') ? 'selected="selected"' : ''; ?>>D - I</option>
												<option value="D-II" <?php echo ($student['division'] == 'D-II') ? 'selected="selected"' : ''; ?>>D - II</option>
											</select>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label for="middlename" class="control-label">Father's Name</label>
											<input type="text" name="middlename" class="form-control" value="<?php echo $student['middle_name'] ?>" data-rule-required="true" />
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label for="mother_first_name" class="control-label">Mother's Firstname</label>
											<input type="text" name="mother_first_name" class="form-control" value="<?php echo $student['mother_first_name'] ?>" data-rule-required="true" />
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group"> 
											<label for="date_of_birth" class="control-label">Date of birth</label>
											<input type="text" data-rule-required="true" name="date_of_birth" id="date_of_birth" class="form-control" value="<?php echo date('d-m-Y',strtotime($student['date_of_birth'])); ?>" />
										</div>
									</div>
								</div>
							</div>
							<div class="col-md-4 text-center">
								<?php if($student['photo_path']):?>
									<figure>
										<?php if(!empty($student['academic_year'])){ ?>
											<img src="<?php echo base_url('uploads/senior/'.$student['academic_year'].'/Photos/'.$student['photo_path']);?>" class="img-responsive" width="150" height="150" />
											<!--<img src="<?php echo $student['photo_path'];?>" class="img-responsive" width="150" height="150" />-->
										<?php }else{ ?>
											<img src="<?php echo base_url('uploads/senior/'.$student['admission_year'].'/Photos/'.$student['photo_path']);?>" class="img-responsive" width="150" height="150" />
										<?php } ?>
									</figure>
								<?php else:?>
									<figure>
										<img src="<?php echo base_url('assets/img/student.png');?>" width="150" height="150" class="img-responsive"  />
									</figure>
								<?php endif;?>
								<h6>AY - <?php echo $student['academic_year'];?>-<?php echo $student['academic_year']+1;?></h6>
								<button type="button" data-toggle="modal" data-target="#changePhotoModal" class="btn btn-primary" style="margin-bottom:10px;">Change Photo</button><br>
								<button type="button" data-toggle="modal" data-target="#changePassModal" class="btn btn-primary">Change Password</button>
								<?php if($student['pdf_path']):?>
									<br><br><a class="btn btn-primary" target="_blank" href="<?php echo base_url('uploads/senior/'.$student['academic_year'].'/PDF/'.$student['pdf_path']);?>">Show PDF</a>
								<?php endif;?>								
							</div>					
						</div>	
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="blood_group" class="control-label">Blood Group (with Rh)</label>
									<select name="blood_group" data-rule-required="true" id="blood_group" class="form-control">
										<option value="">Blood group</option>
										<option value="O+" <?php echo ($student['blood_group'] == 'O+') ? 'selected="selected"' : ''; ?>>O +ve</option>
										<option value="A+" <?php echo ($student['blood_group'] == 'A+') ? 'selected="selected"' : ''; ?>>A +ve</option>
										<option value="B+" <?php echo ($student['blood_group'] == 'B+') ? 'selected="selected"' : ''; ?>>B +ve</option>
										<option value="AB+" <?php echo ($student['blood_group'] == 'AB+') ? 'selected="selected"' : ''; ?>>AB +ve</option>
										<option value="O-" <?php echo ($student['blood_group'] == 'O-') ? 'selected="selected"' : ''; ?>>O -ve</option>
										<option value="A-" <?php echo ($student['blood_group'] == 'A-') ? 'selected="selected"' : ''; ?>>A -ve</option>
										<option value="B-" <?php echo ($student['blood_group'] == 'B-') ? 'selected="selected"' : ''; ?>>B -ve</option>
									</select>
								</div>
							</div>	
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="emergency_contact" class="control-label">Emergency contact</label>
									<input type="text" name="emergency_contact" class="form-control" value="<?php echo $student['emergency_number'] ?>"  />
								</div>							
							</div>
						</div>
								
						<div class="row">
							<div class="col-md-6">
								<div class="form-group"> 
									<label for="gender" class="control-label">Gender</label>
									<select name="gender" data-rule-required="true" class="form-control">
										<option value="Male" <?php if($student['gender'] == 'Male' || $student['gender'] == 'MALE' || $student['gender'] == '2'){ echo 'selected'; }?>>Male</option>
										<option value="Female" <?php if($student['gender'] == 'Female' || $student['gender'] == 'FEMALE' || $student['gender'] == '1'){ echo 'selected'; } ?>>Female</option>
										<option value="Other" <?php if($student['gender'] == 'Other' || $student['gender'] == 'OTHER' || $student['gender'] == '3'){ echo 'selected'; } ?>>Other</option>
									</select>
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group"> 
									<label for="mother_tongue" class="control-label">Mother Tongue</label>
									<input type="text" data-rule-required="true" name="mother_tongue" class="form-control" value="<?php echo $student['mother_tongue'] ?>" />
								</div>
							</div>
						</div>
								
						<div class="row">
							<div class="col-md-6">
								<div class="form-group"> 
									<label for="marital_status" class="control-label">Marital Status</label>
									<select name="marital_status" data-rule-required="true" class="form-control">
										<option value="Married" <?php echo($student['marital_status'] == 'Married' || $student['marital_status'] == 'married') ? 'selected="selected"': '';?>>Married</option>
										<option value="Unmarried" <?php echo($student['marital_status'] == 'Unmarried' || $student['marital_status'] == 'UnMarried') ? 'selected="selected"': '';?>>UnMarried</option>
										<option value="Divorced" <?php echo($student['marital_status'] == 'Divorced' || $student['marital_status'] == 'divorced') ? 'selected="selected"': '';?>>Divorced</option>
										<option value="Widowed" <?php echo($student['marital_status'] == 'Widowed' || $student['marital_status'] == 'widowed') ? 'selected="selected"': '';?>>Widowed</option>
									</select>
								</div>
							</div>					
							<div class="col-md-6">
								<div class="form-group"> 
									<label for="birth_place" class="control-label">Place of Birth</label>
									<input type="text" name="birth_place" data-rule-required="true" class="form-control" value="<?php echo $student['birth_place'] ?>" />
								</div>
							</div>									
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group"> 
									<input type="checkbox" name="physical_disability" id="physical_disability" <?php echo ($student['physical_disability'] == 'Yes') ? 'checked' : '';?> />
									<label for="physical_disability" class="control-label">Disability</label>
								</div>

								<div class="form-group">
									<input type="checkbox" name="left_college" id="left_college" <?php echo ($student['left_college'] == 'Yes') ? 'checked' : '';?> />
									<label for="left_college" class="control-label">Left College</label>
									<input type="hidden" name="left_college_date" id="left_college_date" value="<?php echo $student['left_college_date'];?>">
								</div>
								<div class="form-group">
									<input type="checkbox" name="dropped_college" id="dropped_college" <?php echo ($student['dropped'] == 'Yes') ? 'checked' : '';?> />		
									<label for="dropped_college" class="control-label">Dropped College</label>
								</div>
							</div>	
							<div class="col-md-4">
								
							</div>
							<div class="col-md-4">
								
							</div>
						</div>
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label for="course_name" class="control-label">Course Name</label>
									<?php if($courseList): ?>
									<select name="course_name" data-rule-required="true" id="course_name" class="form-control">
										<option value="">Course Name</option>
										<?php 
										foreach($courseList as $crs):?>
											<?php 
											$selected = '';
											if($crs->year == $cur_course[0]->year && $crs->course_name == $cur_course[0]->course_name){
												$selected = 'selected="selected"';
											}?>
											<option value="<?php echo $crs->year;?><?php echo ($crs->course_name) ? '-'.$crs->course_name : '';?>" <?php echo $selected;?>><?php echo $crs->year;?>-<?php echo ($crs->course_name) ? $crs->course_name : 'Regular';?></option>
										<?php endforeach;?>
									</select>
									<?php endif;?>
								</div>
							</div>
							
							<input type="hidden" name="courseId" id="courseId" class="form-control" value="<?php echo $student['course_id'];?>">
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="specialization" class="control-label">Specialization</label>
									<?php $cur_course[0]->specialization; if($specialization): ?>
									<select name="specialization" data-rule-required="true" id="specialization" class="form-control">
										<option value="">Specialization</option>
										<?php 
										foreach($specialization as $spec):?>
											<?php $selected = '';
											if($spec->specialization == $cur_course[0]->specialization){
												$selected = 'selected="selected"';
											}?>
											<option value="<?php echo $spec->specialization;?>" <?php echo $selected;?>><?php echo $spec->specialization;?></option>
										<?php endforeach;?>
									</select>
									<?php endif;?>
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-md-12">
								<h3 class="add-info">Identity Details</h3>
							</div>
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="aadhaar_number" class="control-label">Aadhaar Number</label>
									<input type="text" name="aadhaar_number" data-rule-required="true" data-rule-number="true" data-rule-minlength="12" data-rule-maxlength="12" class="form-control" value="<?php echo $student['aadhaar_number'] ?>" />
								</div>
							</div>		
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="pan_number" class="control-label">PAN Number</label>
									<!--<input type="text" name="pan_number" id="pan_number" data-rule-required="true" data-rule-minlength="10" data-rule-maxlength="10" class="form-control" value="<?php //echo $student['pan_number'] ?>" />-->
									<input type="text" name="pan_number" id="pan_number" class="form-control" value="<?php echo $student['pan_number'] ?>" />
								</div>
							</div>	
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="voter_id" class="control-label">Voter Id</label>
									<input type="text" name="voter_id" id="voter_id" class="form-control" value="<?php echo $student['voter_id'] ?>" />
								</div>
							</div>						
						</div>
						<div class="row" data-type="physical_disability" style="display:<?php if($student['physical_disability'] == 'Yes'){echo 'flex'; }else{echo 'none';} ?>">
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="disability_type" class="control-label">Disability Type</label>
									<select id="disability_type" name="disability_type" class="form-control">
										<option value=""> Please Select Disability Type</option>
										<option value="Visually Impaired" <?php echo($student['disability_type'] == 'Visually Impaired') ? 'selected="selected"': '';?>>Visually Impaired</option>
										<option value="Speech and Hearing Impaired" <?php echo($student['disability_type'] == 'Speech and Hearing Impaired') ? 'selected="selected"': '';?>>Speech and/or Hearing Impaired</option>
										<option value="Orthopedic Disorder or Mentally Retarded" <?php echo($student['disability_type'] == 'Orthopedic Disorder or Mentally Retarded') ? 'selected="selected"': '';?>>Orthopedic Disorder or Mentally Retarded</option>
										<option value="Learning Disability" <?php echo($student['disability_type'] == 'Learning Disability') ? 'selected="selected"': '';?>>Learning Disability</option>
										<option value="Dyslexia" <?php echo($student['disability_type'] == 'Dyslexia') ? 'selected="selected"': '';?>>Dyslexia</option>
									</select>				
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="disability_percentage" class="control-label">Disability Percentage</label>
									<input type="text" name="disability_percentage" class="form-control" value="<?php echo $student['disability_percentage'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group"> 
									<label for="disability_number" class="control-label">Enter your Disability Card Number</label>
									<input type="text" name="disability_number" class="form-control" value="<?php echo $student['disability_number'] ?>" />
								</div>
							</div>				
						</div>
						
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Religion/Caste Details</h3>
							</div>  
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="religion" class="control-label">Religion</label>
									<select name="religion" class="form-control" data-rule-required="true">
										<option value="">Select</option>
										<option value="Buddhist" <?php echo($student['religion'] == 'Buddhist' || $student['religion'] == 'BUDDHIST') ? 'selected="selected"': '';?>>Buddhist</option>
										<option value="Christian" <?php echo($student['religion'] == 'Christian' || $student['religion'] == 'CHRISTIAN') ? 'selected="selected"': '';?>>Christian</option>
										<option value="Hindu" <?php echo($student['religion'] == 'Hindu' || $student['religion'] == 'HINDU') ? 'selected="selected"': '';?>>Hindu</option>
										<option value="Jain" <?php echo($student['religion'] == 'Jain' || $student['religion'] == 'JAIN') ? 'selected="selected"': '';?>>Jain</option>
										<option value="Muslim" <?php echo($student['religion'] == 'Muslim' || $student['religion'] == 'MUSLIM') ? 'selected="selected"': '';?>>Muslim</option>
										<option value="Other" <?php echo($student['religion'] == 'Other' || $student['religion'] == 'OTHER') ? 'selected="selected"': '';?>>Other</option>
										<option value="Parsi" <?php echo($student['religion'] == 'Parsi' || $student['religion'] == 'PARSI') ? 'selected="selected"': '';?>>Parsi</option>
										<option value="Sikh" <?php echo($student['religion'] == 'Sikh' || $student['religion'] == 'SIKH') ? 'selected="selected"': '';?>>Sikh</option>
									</select>  
								</div>		
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="caste_category" class="control-label">Category</label>
									<div style="height:40px;">
										<input type="radio" id="category1" class="category" name="caste_category" value="Open" <?php echo ($student['caste_category'] == 'Open' || $student['caste_category'] == 'OPEN' || $student['caste_category'] == 'open')? 'checked' : ''; ?> >
										<label for="Open" class="control-label">Open</label>&nbsp;&nbsp;
										<input type="radio" id="category2" class="category" name="caste_category" value="Reserved" <?php echo ($student['caste_category'] == 'Reserved' || $student['caste_category'] == 'RESERVED' || $student['caste_category'] == 'reserved')? 'checked' : ''; ?> >
										<label for="Reserved" class="control-label">Reserved</label>
									</div>
								</div>		
							</div>
						</div>
							
						<div class="row show_caste">
							<div class="col-md-4">
								<div class="form-group">
									<label for="caste" class="control-label">Caste</label>
									<select class="form-control" name="caste" id="caste" data-rule-required="true">
										<option value="">Select</option>
										<option value="EWS" <?php if($student['caste'] == 'EWS'){echo 'selected'; } ?>>EWS (Economically Weaker Section)</option>
										<option value="NT1" <?php if($student['caste'] == 'NT1'){echo 'selected'; } ?>>NT (1)</option>
										<option value="NT2" <?php if($student['caste'] == 'NT2'){echo 'selected'; } ?>>NT (2)</option>
										<option value="NT3" <?php if($student['caste'] == 'NT3'){echo 'selected'; } ?>>NT (3)</option>
										<option value="OBC" <?php if($student['caste'] == 'OBC'){echo 'selected'; } ?>>OBC</option>
										<option value="SBC" <?php if($student['caste'] == 'SBC'){echo 'selected'; } ?>>SBC</option>
										<option value="SC" <?php if($student['caste'] == 'SC'){echo 'selected'; } ?>>SC</option>
										<option value="SEBC" <?php if($student['caste'] == 'SEBC'){echo 'selected'; } ?>>SEBC (Socially &amp; Educationally Backward Class)</option>
										<option value="ST" <?php if($student['caste'] == 'ST'){echo 'selected'; } ?>>ST</option>
										<option value="VJ" <?php if($student['caste'] == 'VJ'){echo 'selected'; } ?>>VJ</option>
									</select>
								</div>		
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="sub_caste" class="control-label">Sub Caste</label>
									<input type="text" name="sub_caste" id="sub_caste" class="form-control" value="<?php echo $student['sub_caste'] ?>" />
								</div>
							</div>
						</div>
							
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Contact Details</h3>
							</div>  
						</div>	
						
						<div class="row">	
							<div class="col-md-6">
								<div class="form-group">
									<label for="mobile_number" class="control-label">Mobile Number</label>
									<input type="text" name="mobile_number" class="form-control" value="<?php echo $student['mobile_number'] ?>" />
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="email_id" class="control-label">Email Id</label>
									<input type="text" name="email_id" class="form-control" value="<?php echo $student['email_id'] ?>" data-rule-required="true" data-rule-email="true"/>
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="guardian_email" class="control-label">Guardian Email</label>
									<input type="email" name="guardian_email" class="form-control" value="<?php echo $student['guardian_email'] ?>" data-rule-email="true"/>
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="guardian_mobile" class="control-label">Guardian Mobile</label>
									<input type="text" name="guardian_mobile" class="form-control" value="<?php echo $student['guardian_mobile'] ?>" data-rule-number="true" />
								</div>
							</div>	
						</div>
						
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Correspondence Address</h3>
							</div>  
						</div>
						
						<div class="row">
							<div class="col-md-6 cors_address">
								<div class="form-group">
									<label for="correspondance_address" class="control-label">Correspondence Address / Temporary Address</label>
									<textarea name="correspondance_address" class="form-control"><?php echo $student['correspondance_address'] ?></textarea>
								</div>
							</div>
							<div class="col-md-3 cors_address">
								<div class="form-group">
									<?php $statelists = config_item('states_list');?>
									<label for="state" class="control-label">State Of Residence</label>
									<select id="state" name="state" class="form-control">
										<option value=""> Please Select State</option>
										<?php 
										foreach($statelists as $key=>$value):
											$selected = NULL;
											if(strtoupper($value) == strtoupper(trim($student['state']))){
												$selected = "selected='selected'";
											}?>
											<option value="<?php echo $value;?>" <?php echo $selected;?>><?php echo $value;?></option> 
										<?php endforeach;?>									
									</select>				
								</div>
							</div>
							<div class="col-md-3 cors_address">
								<div class="form-group">
									<label for="city" class="control-label">City</label>
									<input type="text" name="city" class="form-control" value="<?php echo $student['city'] ?>" />
								</div>
							</div>
							<div class="col-md-4 cors_address">
								<div class="form-group">
									<label for="pin" class="control-label">Pincode</label>
									<input type="text" name="pin" class="form-control" value="<?php echo $student['pin'] ?>" />
								</div>
							</div>
							
							<div class="col-md-4">
								<div class="form-group">
									<label for="country" class="control-label">Country</label>
									<?php $countries = config_item('country_list');?>
									<select name="country" id="country" class="form-control" data-rule-required="true">
										<?php 
										foreach($countries as $key=>$value):
											$selected = NULL;
											if(strtoupper($value) == strtoupper($student['country'])){
												$selected = "selected='selected'";
											}?>
											<option value="<?php echo $value;?>" <?php echo $selected;?>><?php echo $value;?></option> 
										<?php endforeach;?>
									</select>							
								</div>
							</div>
							<div class="col-md-4 cors_address">
								<div class="form-group">
									<label for="location_category" class="control-label">Location Category</label>
									<select class="form-control" name="location_category" data-rule-required="true">
										<option value="">Select</option>
										<option value="Urban" <?php if($student['location_category'] == '' || $student['location_category'] == 'Urban'){ echo 'selected="selected"'; } ?>>Urban</option>
										<option value="Rural" <?php echo ($student['location_category'] == 'Rural')? 'selected="selected"' : ''; ?>>Rural</option>
										<option value="Not specified" <?php echo ($student['location_category'] == 'Not specified')? 'selected="selected"' : ''; ?>>Not specified</option>																													 
									</select>
								</div>
							</div>
						</div>
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Permanent Address</h3>
							</div>  
						</div>
						
						<div class="row">	
							<div class="col-md-6 per_address">
								<div class="form-group">
									<label for="permanent_address" class="control-label">Permanent Address</label>
									<textarea name="permanent_address" class="form-control"><?php echo $student['permanent_address'] ?></textarea>
								</div>
							</div>
							<div class="col-md-3 per_address">
								<div class="form-group">							
									<label for="permanent_state" class="control-label">State Of Residence</label>
									<select id="permanent_state" name="permanent_state" class="form-control">
										<option value=""> Please Select State</option>
										<?php 
										foreach($statelists as $key=>$value):
											$selected = NULL;
											if(strtoupper($value) == strtoupper(trim($student['permanent_state']))){
												$selected = "selected='selected'";
											}?>
											<option value="<?php echo $value;?>" <?php echo $selected;?>><?php echo $value;?></option> 
										<?php endforeach;?>
									</select>				
								</div>
							</div>
							<div class="col-md-3 per_address">
								<div class="form-group">
									<label for="permanent_city" class="control-label">Permanent City</label>
									<input type="text" name="permanent_city" class="form-control" value="<?php echo $student['permanent_city'] ?>" />
								</div>
							</div>
							<div class="col-md-4 per_address">
								<div class="form-group">
									<label for="permanent_taluka" class="control-label">Permanent Taluka</label>
									<input type="text" name="permanent_taluka" class="form-control" value="<?php echo $student['permanent_taluka'] ?>" />
								</div>
							</div>
							<div class="col-md-4 per_address">
								<div class="form-group">
									<label for="permanent_pin" class="control-label">Permanent Pincode</label>
									<input type="text" name="permanent_pin" class="form-control" value="<?php echo $student['permanent_pin'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="permanent_country" class="control-label">Permanent Country</label>
									<select name="permanent_country" id="permanent_country" class="form-control">
										<option value=''>Select Country</option>
										<?php 
										foreach($countries as $key=>$value):
											$selected = NULL;
											if(strtoupper($value) == strtoupper($student['permanent_country'])){
												$selected = "selected='selected'";
											}?>
											<option value="<?php echo $value;?>" <?php echo $selected;?>><?php echo $value;?></option> 
										<?php endforeach;?>
									</select>							
								</div>
							</div>
						</div>
						
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Guardian Details</h3>
							</div>  
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="guardian_name" class="control-label">Guardian Name</label>
									<input type="text" name="guardian_name" data-rule-required="true" class="form-control" value="<?php echo $student['guardian_name'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="guardian_profession" class="control-label">Occupation of the Guardian</label>
									<input type="text" name="guardian_profession" class="form-control" value="<?php echo $student['guardian_profession'] ?>" />
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="guardian_income" class="control-label">Annual Income of the Guardian</label>
									<input type="text" name="guardian_income" data-rule-required="true" class="form-control" value="<?php echo $student['guardian_income'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="relationship_to_guardian" class="control-label">Relation with the Guardian</label>
									<input type="text" name="relationship_to_guardian" data-rule-required="true" class="form-control" value="<?php echo $student['relationship_to_guardian'] ?>" />
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-md-8">
								<div class="form-group">
									<label for="guardian_address" class="control-label">Guardian Address</label>
									<textarea name="guardian_address" class="form-control"><?php echo $student['guardian_address'] ?></textarea>
								</div>
							</div>
						</div>
						
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Bank Account Details</h3>
							</div>  
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="account_holder_name" class="control-label">Account Holder name</label>
									<input type="text" name="account_holder_name" class="form-control" value="<?php echo $student['acc_holder_name'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="bank_acc_no" class="control-label">Account number</label>
									<input type="text" name="bank_acc_no" class="form-control" value="<?php echo $student['bank_acc_no'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="ifsc_code" class="control-label">IFSC Code</label>
									<input type="text" name="ifsc_code" class="form-control" value="<?php echo $student['ifsc_code'] ?>" />
								</div>
							</div>
						</div>
						
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Previous Education Details</h3>
							</div>  
						</div>
						
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="name_of_school" class="control-label">Previous College/School Name</label>
									<input type="text" name="name_of_school" class="form-control" value="<?php echo $student['name_of_school'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="address_of_school" class="control-label">School/College Address</label>
									<textarea name="address_of_school" class="form-control"><?php echo $student['address_of_school'] ?></textarea>
								</div>
							</div>
						</div>
						
						<div class="row">	
							<div class="col-md-12">
								<h3 class="add-info">Last Exam Details</h3>
							</div>  
						</div>
							
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="name_of_examination" class="control-label">Name of Examination</label>
									<input type="text" name="name_of_examination" class="form-control" value="<?php echo $student['name_of_examination'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="name_of_board" class="control-label">Last Examination Board</label>
									<input type="text" name="name_of_board" data-rule-required="true" class="form-control" value="<?php echo $student['name_of_board'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="year_of_passing" class="control-label">Year of Passing</label>
									<?php 
									
									$start_date = date('Y', strtotime('-25 years'));
									$yearPass = range($start_date, date('Y'));?>
									<select name="year_of_passing" class="form-control">
										<option value="">Select Year</option>
										<?php
										
										foreach ($yearPass as $year) {
											$selected = ($year == $student['year_of_passing']) ? 'selected' : '';
											echo '<option '.$selected.' value="'.$year.'">'.$year.'</option>';
										}?>
									</select>				
								</div>
							</div>	
						</div>	

						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label for="exam_seat_no" class="control-label">Examination Seat Number</label>
									<input type="text" name="exam_seat_no" class="form-control" value="<?php echo $student['exam_seat_no'] ?>" />
								</div>
							</div>								
							<div class="col-md-4">
								<div class="form-group">
									<label for="previous_exam_state" class="control-label">Previous Exam State</label>
									<select name="previous_exam_state" class="form-control">
										<option value="">Select State</option>
										<?php 
										foreach($statelists as $key=>$value):
											$selected = NULL;
											if($value === $student['previous_exam_state']){
												$selected = "selected='selected'";
											}?>
											<option value="<?php echo $value;?>" <?php echo $selected;?>><?php echo $value;?></option> 
										<?php endforeach;?>
									</select>							
								</div>
							</div>
						</div>	

						<div class="row">		
							<div class="col-md-4">
								<div class="form-group">
									<label for="marks_obtained" class="control-label">Marks Obtained</label>
									<input type="number" id="marks_obtained" name="marks_obtained"  data-rule-number="true" class="form-control" value="<?php echo $student['marks_obtained'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="marks_outof" class="control-label">Total Marks</label>
									<input type="number" id="marks_outof" name="marks_outof" class="form-control" data-rule-required="true" data-rule-number="true" value="<?php echo $student['marks_outof'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="percentage_in_ssc" class="control-label">Percentage in SSC</label>
									<input type="number" id="percentage_in_ssc" name="percentage_in_ssc" class="form-control" value="<?php echo $student['percentage_in_ssc'] ?>" disabled />
								</div>
							</div>
						</div>							
						
						<div class="row">					
							<div class="col-md-4">
								<div class="form-group">
									<label for="school_college" class="control-label">School/College</label>
									<input type="text" name="school_college" class="form-control" value="<?php echo $student['school_college'] ?>" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label for="academic_year" class="control-label">Academic Year</label>
									<?php $academicYear = range(1981, date('Y'));?>
									<select name="academic_year" class="form-control">
										<option value="">Select Year</option>
										<?php
										foreach ($academicYear as $ayear) {
											$selected = ($ayear == $student['academic_year']) ? 'selected' : '';
											echo '<option '.$selected.' value="'.$ayear.'" disabled>'.$ayear.'</option>';
										}?>
									</select>
								</div>
							</div>	
							<div class="col-md-4">
								<div class="form-group">
									<label for="stream" class="control-label">Stream</label>
									<input type="text" name="stream" class="form-control" value="<?php echo $student['stream'] ?>" />
								</div>
							</div>
						</div>				
						
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<input type="hidden" name="photo_path" value="<?php echo $student['photo_path']; ?>">
									<input type="hidden" name="academic_year" value="<?php echo $student['academic_year']; ?>">
									<input type="submit" name="submit" class="btn btn-info btn-md" value="Save Student Details" />
								</div>
							</div>
						</div>
						<input type="hidden" name="user_id" value="<?php echo $student['id']; ?>" />    
					</form>
				<?php }?>
				</div>
			</div>
			<div class="tab-pane fade p-3" id="accounts" role="tabpanel" aria-labelledby="accounts">
				<div class="small">
					<form action="<?php echo base_url(); ?>search_student" method="post" class="clearfix mb-3">
						<input type="hidden" name="reg_id" value="<?php echo $student['userID'];?>">
						<input type="hidden" name="year" value="<?php echo $student['course_name'];?>">
						<input type="hidden" name="specialisation" value="<?php echo $student['specialization'];?>">
						<input type="hidden" name="search" value="fees">
						<h5 class="card-title">Accounts<button type="submit" class="btn btn-primary" style="float:right;">Make Offline Payment</button></h5>						
					</form>				
									
					<table class="table table-bordered" id="dataTable" width="100%" cellspacing="0" style="text-transform:capitalize;">
						<thead>
							<tr>
								<th>Challan No.</th>
								<th>Date Paid</th>             
								<th>Amount</th>
								<th>Payment Mode</th>
								<th>Status</th>
								<th>Action</th>
							</tr> 
						</thead>          
						<?php if(!empty($transaction_details)){ ?>	
						<tbody>
							<?php 
							foreach ($transaction_details as $transaction_detail) { ?>
								<tr>
									<td><?php echo $transaction_detail->challan_number ?></td>
									<td><?php echo date('d-m-Y',strtotime($transaction_detail->fee_paid_date)); ?></td>
									<td><?php echo $transaction_detail->total_paid ?></td>
									<td><?php echo $transaction_detail->payment_mode ?></td>
									<?php if($transaction_detail->transaction_status =='Success' || $transaction_detail->transaction_status =='Paid'){?>
										<td><span class="label badge badge-success"><?php echo $transaction_detail->transaction_status;?></span></td>
									<?php } else {?>
										<td><span class="badge badge-danger"><?php echo $transaction_detail->transaction_status;?></span></td>
									<?php }?>
									<td>
										<?php if($transaction_detail->payment_type == 'Offline'):?>
										<form class="change_status_form" action="<?php echo base_url(); ?>update_status">
											<select class="change_status" name="transaction_status">
												<option value="">Change</option>
												<option value="Unpaid">Unpaid</option>
												<option value="Paid">Paid</option>
											</select>
											<input type="hidden" value="<?php echo $transaction_detail->id;?>" name="trans_id">
										</form>
										<?php else: ?>
											<em>PAID</em>
										<?php endif;?>
									</td>
								</tr>	
							<?php }?>					
						</tbody>
							<?php }else{ ?>
							<tbody>     
								<tr class="odd"><td valign="top" colspan="5" class="dataTables_empty">You haven't done any transactions yet</td></tr>
							</tbody>
						<?php } ?>
					</table>
				</div>      
			</div>
			<div class="tab-pane fade p-3" id="documents" role="tabpanel" aria-labelledby="documents">
				<div class="small">					
					<div class="accordion" id="accordionExample">
						<div class="card">
							<div class="card-header" id="uploadDoc">
								<a data-toggle="collapse" data-target="#uploadDocCont" aria-expanded="true" aria-controls="uploadDocCont">
									<i class="fas fa-folder-open"></i> Upload New Document
									<span class="toggleCard"><i class="fas fa-sort-down"></i></span>
								</a>
							</div>
							<div id="uploadDocCont" class="collapse" aria-labelledby="uploadDoc" data-parent="#accordionExample">
								<div class="card-body">
									<form method="post" class="form-horizontal upload_new_doc" id="upload_new_doc" enctype="multipart/form-data"> 
										<div class="form-group">
											<label class="control-label col-sm-6" for="doc_type">Document Type:</label>
											<div class="col-sm-6">
												<select name="doc_type" id="doc_type" class="form-control" data-rule-required="true">
														<option value="">--Choose--</option>
														<option value="Marksheet">Marksheet</option>
														<option value="Domicile-Certificate">Domicile Certificate</option>
														<option value="Birth Certificate">Birth Certificate</option>
														<option value="Bonafide Certificate">Bonafide Certificate</option>
														<option value="Passing-Certificate">Passing Certificate</option>
														<option value="Eligibility-Certificate">Eligibility Certificate</option>
														<option value="Caste-Certificate">Caste Certificate</option>
														<option value="Disability-Certificate">Disability Certificate</option>
														<option value="Ration-Card">Ration Card</option>
														<option value="Transfer-Certificate">Transfer Certificate</option>
														<option value="Aadhar-Card">Aadhar Card</option>
														<option value="Pan-Card">PAN Card</option>
														<option value="Voter-Card">Voter Card</option>
														<option value="Migration-Certificate">Migration Certificate</option>
														<option value="Name-Change-Certificate">Name Change Certificate</option>
														<option value="Internship-Certificate">Internship Certificate</option>
														<option value="LC">LC</option>
														<option value="Sports-Certificate">Sports Certificate</option>
														<option value="Bank-Statement">Bank Statement</option>
														<option value="Passbook">Passbook</option>
														<option value="Gap-Certificate">Gap Certificate</option>
												</select>
											</div>
										</div>
										<div class="form-group">
											<label class="control-label col-sm-6" for="file">Upload Document:</label>
											<div class="col-sm-6"> 
												<input type="file" class="form-control" name="doc" id="doc" data-rule-required="true"/>
											</div>
										</div>										
										<div class="form-group"> 
											<div class="col-sm-offset-2 col-sm-10">
												<input type="hidden" id="student_id" name="student_id" value="<?php echo $student['userID'];?>">
												<input type="hidden" id="student_academic_year" name="student_academic_year" value="<?php echo $student['academic_year'];?>">
												<input type="submit" class="btn btn-success" value="Upload"/>
											</div>
										</div>
									</form>
								</div>
							</div>
						</div>
						<div class="card">
							<div class="card-header" id="prevuploadDoc">
								<a data-toggle="collapse" data-target="#prevuploadCont" aria-expanded="true" aria-controls="prevuploadCont">
									<i class="fas fa-folder-open"></i> Documents
									<span class="toggleCard"><i class="fas fa-sort-down"></i></span>
								</a>
							</div>
							<div id="prevuploadCont" class="collapse show" aria-labelledby="prevuploadDoc" data-parent="#accordionExample">
								<div class="card-body">
									<?php $documents = @file_get_contents('https://vancotech.com/dms/api/GetDocuments.ashx?admissionYear='.$student['academic_year'].'&crn='.$student['userID']);
									$docs = json_decode($documents);						
									if($docs):
										echo '<div class="list_files row">';
										foreach($docs as $doc){
											$doc_array = explode('/',$doc);?>
											<div class="col-md-2">
												<a href="https://vancotech.com/dms/api/DownloadDocument.ashx?admissionYear=<?php echo $student['academic_year']; ?>&crn=<?php echo $student['userID'];?>&p=<?php echo $doc;?>"><i class="fas fa-file-pdf"></i> <?php echo $doc_array[0];?>									
												</a>
											</div>
											<?php
										}
										echo '</div>';
									endif;?>													
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			
			<div class="tab-pane fade p-3" id="feedback" role="tabpanel" aria-labelledby="feedback">
				<div class="">
					<h5 class="card-title">Feedback</h5>
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
								<iframe src="https://vancotech.com/StudentFeedback/prod/viewfeedback.html?cc=102&type=Student&crn=<?php echo $student['userID'];?>" width="100%" height="500px"></iframe>
							</div>
						</div>
						<div class="tab-pane fade p-3" id="parent_feed" role="tabpanel" aria-labelledby="parent_feed">
							<div class="card-body">
								<iframe src="https://vancotech.com/StudentFeedback/prod/viewfeedback.html?cc=102&type=Parent&crn=<?php echo $student['userID'];?> " width="100%" height="500px"></iframe>
							</div>
						</div>
					</div>	
				</div>				
			</div>
			
			<div class="tab-pane fade p-3" id="performance" role="tabpanel" aria-labelledby="performance">
				<div style="width:75%;">
					<canvas id="performanceChart"></canvas>					
				</div>
				<div class="semster-tabs">
					<div class="card-header">	
						<ul class="nav nav-tabs card-header-tabs" role="tablist">
							<li class="nav-item active"><a id="sem1-link" class="nav-link" data-toggle="tab" href="#sem1">Sem1</a></li>
							<li class="nav-item"><a id="sem2-link" class="nav-link" data-toggle="tab" href="#sem2">Sem2</a></li>
							<li class="nav-item"><a id="sem3-link" class="nav-link" data-toggle="tab" href="#sem3">Sem3</a></li>
							<li class="nav-item"><a id="sem4-link" class="nav-link" data-toggle="tab" href="#sem4">Sem4</a></li>
							<li class="nav-item"><a id="sem5-link" class="nav-link" data-toggle="tab" href="#sem5">Sem5</a></li>
							<li class="nav-item"><a id="sem6-link" class="nav-link"data-toggle="tab" href="#sem6">Sem6</a></li>
						</ul>
					</div>
					<div class="tab-content">
						<div id="sem1" class="tab-pane fade in active show">
							<div class="card-body">
								<canvas id="graphcanvas1"></canvas>
							</div>
						</div>
						<div id="sem2" class="tab-pane fade">
							<div class="card-body">
								<canvas id="graphcanvas2"></canvas>
							</div>
						</div>
						<div id="sem3" class="tab-pane fade">
							<div class="card-body">
								<canvas id="graphcanvas3"></canvas>
							</div>
						</div>
						<div id="sem4" class="tab-pane fade">
							<div class="card-body">
								<canvas id="graphcanvas4"></canvas>
							</div>
						</div>
						<div id="sem5" class="tab-pane fade">
							<div class="card-body">
								<canvas id="graphcanvas5"></canvas>
							</div>
						</div>
						<div id="sem6" class="tab-pane fade">
							<div class="card-body">
								<canvas id="graphcanvas6"></canvas>
							</div>
						</div>
					</div>
				</div>
			</div>
			<?php /*<div class="tab-pane fade p-3" id="applications" role="tabpanel" aria-labelledby="applications">
				<div class="small">
					<h5 class="card-title">Applications</h5>
					<?php $userid = $this->session->userdata('userID');
					if($userid == 'officeadmin2'){ ?>
						<iframe src="https://vancotech.com/Exams/bachelors/ui/Applications.html?crn=<?php echo $student['userID'];?>" width="100%" height="500px"></iframe>
					<?php }elseif($userid == 'officeadmin1'){ ?>
						<iframe src="https://vancotech.com/Exams/masters/ui/Applications.html?crn=<?php echo $student['userID'];?>" width="100%" height="500px"></iframe>
					<?php }	 ?>  
				</div>
			</div> */?>
		</div>	
    </div>
  </div>
  
  
  
	<div class="modal fade" id="changePassModal" tabindex="-1" role="dialog" aria-labelledby="changePassModal" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Change Password</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="change_password">
						<div class="row">					
							<div class="col-md-12">
								<div class="form-group">
									<input type="password" name="new_password" id="new_password" class="form-control" placeholder="New Password" />
								</div>
							</div>
							<div class="col-md-12">
								<div class="form-group">
									<input type="password" name="conf_password" class="form-control" placeholder="Confirm Password" />
								</div>
							</div>
							<input type="hidden" name="cur_user_id" value="<?php echo $student['userID']; ?>">
							<div class="col-md-12">
								<div class="form-group">
									<input type="button" class="btn btn-primary" name="change_pass" id="change_pass" value="Change password">
								</div>
							</div>					
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>	
	
	<div class="modal fade" id="changePhotoModal" tabindex="-1" role="dialog" aria-labelledby="changePhotoModal" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Change Photo</h5>
						<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<form id="change_profile_photo" enctype="multipart/form-data">
						<div class="row">					
							<div class="col-md-12">
								<div class="form-group">
									<input type="file" name="new_photo" id="new_photo" />
								</div>
							</div>
							<input type="hidden" name="old_photo" value="<?php echo $student['photo_path']; ?>">
							<input type="hidden" name="student_admission_year" value="<?php echo $student['admission_year']; ?>">
							<input type="hidden" name="cur_user_id" value="<?php echo $student['userID']; ?>">
							<div class="col-md-12">
								<div class="form-group">
									<input type="button" class="btn btn-primary" name="change_photo" id="change_photo" value="Upload Photo">
								</div>
							</div>					
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>
	<!-- /.container-fluid -->
