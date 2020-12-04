<?php defined('BASEPATH') OR exit('No direct script access allowed'); 
//echo "<pre>"; print_r($cur_course); echo "</pre>";
?>
<div id="content-wrapper">
  <div class="container-fluid">
   
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('student/home'); ?>">Home</a>
      </li>
    </ol>
	
	    <!-- Area Chart Example-->
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	
	<?php $flash_msg = $this->session->flashdata('msg');
    	if(isset($flash_msg)){ ?>
        <div class="alert alert-success alert-dismissible alertbox">
          <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
           <?php echo $flash_msg;?>.
        </div>
    <?php } ?>
	
	<div class="alert alert-danger print-error-msg  alert-dismissible" style="display:none"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span id="msg"></span> </div>
	
	<?php
		/* echo "<pre>";
			print_r($student_details[0]);
		echo "</pre>"; */
	
      //echo $student_details[0]->userID;
      $college_registration = $student_details[0]->userID;
      $roll_number = $student_details[0]->roll_number;
      $name = $student_details[0]->first_name.' '.$student_details[0]->middle_name.' '.$student_details[0]->last_name;
      $firstname = $student_details[0]->first_name;
      $middle_name = $student_details[0]->middle_name;
      $lastname = $student_details[0]->last_name;
      $email = $student_details[0]->email_id;
      $mobile = $student_details[0]->mobile_number;
      $caste_category = $student_details[0]->caste_category;
	  $caste = $student_details[0]->caste;
	  $sub_caste = $student_details[0]->sub_caste;
      $PRN = $student_details[0]->PRN_number;
      $address = $student_details[0]->correspondance_address;
      $course = $course_details[0]->course_type;
      //$specialization = $cur_course[0]->specialization;
      $year = $course_details[0]->year;
	  $blood_group = @$student_details[0]->blood_group;
	  $guardian_mobile = @$student_details[0]->guardian_mobile;
	  $division = @$student_details[0]->division;
	  $mother_first_name = @$student_details[0]->mother_first_name;
	  $date_of_birth = @$student_details[0]->date_of_birth;
	  $admission_year = @$student_details[0]->admission_year;
	  $academic_year = @$student_details[0]->academic_year;
	  $photo_path = @$student_details[0]->photo_path;
	  $emergency_number = @$student_details[0]->emergency_number;
	  $gender = @$student_details[0]->gender;
    ?>

	<div class="container">	
	<i class="fas fa-edit"></i><span class="edit_data">Edit</span> Your Details
			<span id="edit_data">Edit</span>
			<div class="small text-muted text_disabled">		
				<?php $attributes = array('id' => 'update_student_details');
					echo form_open_multipart(site_url('/student/details/edit/'.$student_details[0]->id), $attributes); ?>	
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
										<input type="text" name="PRN_number" class="form-control" value="<?php echo $PRN; ?>"  data-rule-number="true" readonly/>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group"> 
										<label for="college_reg" class="control-label">College Reg. No.</label>
										<input type="text" name="college_reg" data-old-reg="<?php echo $college_registration; ?>" id="college_reg" class="form-control" value="<?php echo $college_registration ?>" readonly />
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group"> 
										<label for="roll_number" class="control-label">Roll Number</label>
										<input type="text" name="roll_number" class="form-control" value="<?php echo $roll_number; ?>" data-rule-required="true" data-rule-maxlength="3" data-rule-number="true" readonly />
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label for="lastname" class="control-label">Lastname</label>
										<input type="text" name="lastname" class="form-control" value="<?php echo $lastname; ?>" data-rule-required="true" />
									</div>	
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label for="firstname" class="control-label">Firstname</label>
										<input type="text" name="firstname" class="form-control" value="<?php echo $firstname; ?>" data-rule-required="true" />
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label for="division" class="control-label">Division</label>
										<select name="division" id="division" class="form-control">
											<option value="">Select Division</option>
											<option value="A-I" <?php echo ($division == 'I') ? 'selected="selected"' : ''; ?> disabled>I</option>
											<option value="A-I" <?php echo ($division == 'A-I') ? 'selected="selected"' : ''; ?> disabled>A - I</option>
											<option value="A-II" <?php echo ($division == 'A-II') ? 'selected="selected"' : ''; ?> disabled>A - II</option>
											<option value="B" <?php echo ($division == 'B') ? 'selected="selected"' : ''; ?> disabled>B</option>
											<option value="C-I" <?php echo ($division == 'C-I') ? 'selected="selected"' : ''; ?> disabled>C - I</option>
											<option value="C-II" <?php echo ($division == 'C-II') ? 'selected="selected"' : ''; ?> disabled>C - II</option>
											<option value="C-III" <?php echo ($division == 'C-III') ? 'selected="selected"' : ''; ?> disabled>C - III</option>
											<option value="D-I" <?php echo ($division == 'D') ? 'selected="selected"' : ''; ?> disabled>D</option>
											<option value="D-I" <?php echo ($division == 'D-I') ? 'selected="selected"' : ''; ?> disabled>D - I</option>
											<option value="D-II" <?php echo ($division == 'D-II') ? 'selected="selected"' : ''; ?> disabled>D - II</option>
										</select>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label for="middlename" class="control-label">Father's Name</label>
										<input type="text" name="middlename" class="form-control" value="<?php echo $middle_name; ?>" data-rule-required="true" />
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label for="mother_first_name" class="control-label">Mother's Firstname</label>
										<input type="text" name="mother_first_name" class="form-control" value="<?php echo $mother_first_name; ?>" data-rule-required="true" />
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group"> 
										<label for="date_of_birth" class="control-label">Date of birth</label>
										<input type="text" data-rule-required="true" name="date_of_birth" id="date_of_birth" class="form-control" value="<?php echo date('d-m-Y',strtotime($date_of_birth)); ?>" />
									</div>
								</div>
							</div>
						</div>
						<div class="col-md-4 text-center">
							<?php if($photo_path):?>
								<input type="hidden" name="photo_path" value="<?php echo $photo_path; ?>">
								<figure>
									<img src="<?php echo base_url('uploads/senior/'.$admission_year.'/Photos/'.$photo_path);?>" class="img-responsive" width="150" height="150" />
									<!--<img src="<?php echo $photo_path;?>" class="img-responsive" width="150" height="150" />-->
								</figure>
							<?php else:?>	
								<figure>
									<img src="<?php echo base_url('assets/img/student.png');?>" width="150" height="150" class="img-responsive"  />
								</figure>
							<?php endif;?>
							<h6>AY - <?php echo $academic_year;?>-<?php echo $academic_year+1;?></h6>
							<button type="button" data-toggle="modal" data-target="#changePhotoModal" class="btn btn-primary" style="margin-bottom:10px;">Change Photo</button><br>
							<button type="button" data-toggle="modal" data-target="#changePassModal" class="btn btn-primary">Change Password</button><br><br>
							<?php if($student_details[0]->pdf_path):?>
								<a class="btn btn-primary" target="_blank" href="<?php echo base_url('uploads/senior/'.$admission_year.'/PDF/'.$student_details[0]->pdf_path);?>">Show PDF</a>
							<?php endif;?>								
						</div>					
					</div>	
					
					<div class="row">
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="blood_group" class="control-label">Blood Group (with Rh)</label>
								<select name="blood_group" id="blood_group" class="form-control">
									<option value="">Blood group</option>
									<option value="O+" <?php echo ($blood_group == 'O+') ? 'selected="selected"' : ''; ?>>O +ve</option>
									<option value="A+" <?php echo ($blood_group == 'A+') ? 'selected="selected"' : ''; ?>>A +ve</option>
									<option value="B+" <?php echo ($blood_group == 'B+') ? 'selected="selected"' : ''; ?>>B +ve</option>
									<option value="AB+" <?php echo ($blood_group == 'AB+') ? 'selected="selected"' : ''; ?>>AB +ve</option>
									<option value="O-" <?php echo ($blood_group == 'O-') ? 'selected="selected"' : ''; ?>>O -ve</option>
									<option value="A-" <?php echo ($blood_group == 'A-') ? 'selected="selected"' : ''; ?>>A -ve</option>
									<option value="B-" <?php echo ($blood_group == 'B-') ? 'selected="selected"' : ''; ?>>B -ve</option>
								</select>
							</div>
						</div>	
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="emergency_contact" class="control-label">Emergency contact</label>
								<input type="text" name="emergency_contact" class="form-control" value="<?php echo $emergency_number; ?>"  />
							</div>							
						</div>
					</div>
							
					<div class="row">
						<div class="col-md-6">
							<div class="form-group"> 
								<label for="gender" class="control-label">Gender</label>
								<select name="gender" id="gender" class="form-control">
									<option value="Male" <?php if($gender == 'Male' || $gender == '2'){ echo 'selected'; }?> disabled>Male</option>
									<option value="Female" <?php if($gender == 'Female' || $gender == '1'){ echo 'selected'; } ?> disabled>Female</option>
									<option value="Other" <?php if($gender == 'Other' || $gender == '3'){ echo 'selected'; } ?> disabled>Other</option>
								</select>
							</div>
						</div>	
						<div class="col-md-6">
							<div class="form-group"> 
								<label for="mother_tongue" class="control-label">Mother Tongue</label>
								<input type="text" data-rule-required="true" name="mother_tongue" class="form-control" value="<?php echo $student_details[0]->mother_tongue; ?>" />
							</div>
						</div>
					</div>
							
					<div class="row">
						<div class="col-md-6">
							<div class="form-group"> 
								<label for="marital_status" class="control-label">Marital Status</label>
								<select name="marital_status" data-rule-required="true" class="form-control">
									<option value="Married" <?php echo($student_details[0]->marital_status == 'Married') ? 'selected="selected"': '';?>>Married</option>
									<option value="Unmarried" <?php echo($student_details[0]->marital_status == 'Unmarried') ? 'selected="selected"': '';?>>UnMarried</option>
									<option value="Divorced" <?php echo($student_details[0]->marital_status == 'Divorced') ? 'selected="selected"': '';?>>Divorced</option>
									<option value="Widowed" <?php echo($student_details[0]->marital_status == 'Widowed') ? 'selected="selected"': '';?>>Widowed</option>
								</select>
							</div>
						</div>					
						<div class="col-md-6">
							<div class="form-group"> 
								<label for="birth_place" class="control-label">Place of Birth</label>
								<input type="text" name="birth_place" data-rule-required="true" class="form-control" value="<?php echo $student_details[0]->birth_place; ?>" />
							</div>
						</div>									
					</div>
					
					<div class="row">
						<div class="col-md-4">
							<div class="form-group"> 
								<input type="checkbox" name="physical_disability" id="physical_disability" <?php echo ($student_details[0]->physical_disability == 'Yes') ? 'checked' : '';?> readonly />
								<label for="physical_disability" class="control-label">Disability</label>
							</div>

							<div class="form-group">
								<input type="checkbox" name="left_college" id="left_college" <?php echo ($student_details[0]->left_college == 'Yes') ? 'checked' : '';?> readonly />
								<label for="left_college" class="control-label">Left College</label>
								<input type="hidden" name="left_college_date" id="left_college_date" value="<?php echo $student_details[0]->left_college_date; ?>">
							</div>
							<div class="form-group">
								<input type="checkbox" name="dropped_college" id="dropped_college" <?php if($student_details[0]->dropped == 'Yes' || $student_details[0]->dropped == 'YES' || $student_details[0]->dropped == 1){ echo 'checked'; }?> readonly />		
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
								<select name="course_name" id="course_name" class="form-control">
									<option value="">Course Name</option>
									<?php 
									foreach($courseList as $crs):?>
										<?php 
										$selected = '';
										if($crs->year == $cur_course[0]->year && $crs->course_name == $cur_course[0]->course_name){
											$selected = 'selected="selected"';
										}?>
										<option value="<?php echo $crs->year;?><?php echo ($crs->course_name) ? '-'.$crs->course_name : '';?>" <?php echo $selected;?> disabled><?php echo $crs->year;?>-<?php echo ($crs->course_name) ? $crs->course_name : 'Regular';?></option>
									<?php endforeach;?>
								</select>
								<?php endif;?>
							</div>
						</div>
						
						<input type="hidden" name="courseId" id="courseId" class="form-control" value="<?php echo $student_details[0]->course_id;?>">
						
						<div class="col-md-6">
							<div class="form-group">
								<label for="specialization" class="control-label">Specialization</label>
								<?php $cur_course[0]->specialization; if($specialization): ?>
								<select name="specialization" id="specialization" class="form-control">
									<option value="">Specialization</option>
									<?php 
									foreach($specialization as $spec):?>
										<?php $selected = '';
										if($spec->specialization == $cur_course[0]->specialization){
											$selected = 'selected="selected"';
										}?>
										<option value="<?php echo $spec->specialization;?>" <?php echo $selected;?> disabled><?php echo $spec->specialization;?></option>
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
								<input type="text" name="aadhaar_number" data-rule-required="true" data-rule-number="true" data-rule-minlength="12" data-rule-maxlength="12" class="form-control" value="<?php echo $student_details[0]->aadhaar_number; ?>" />
							</div>
						</div>		
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="pan_number" class="control-label">PAN Number</label>
								<!--<input type="text" name="pan_number" id="pan_number" data-rule-required="true" data-rule-minlength="10" data-rule-maxlength="10" class="form-control" value="<?php //echo $student['pan_number'] ?>" />-->
								<input type="text" name="pan_number" id="pan_number" class="form-control" value="<?php echo $student_details[0]->pan_number; ?>" />
							</div>
						</div>	
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="voter_id" class="control-label">Voter Id</label>
								<input type="text" name="voter_id" id="voter_id" class="form-control" value="<?php echo $student_details[0]->voter_id; ?>" />
							</div>
						</div>						
					</div>
					
					<div class="row" data-type="physical_disability" style="display:<?php if($student_details[0]->physical_disability == 'Yes'){echo 'flex';}else{echo 'none'; } ?>">
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="disability_type" class="control-label">Disability Type</label>
								<select id="disability_type" name="disability_type" class="form-control">
									<option value=""> Please Select Disability Type</option>
									<option value="Visually Impaired" <?php echo($student_details[0]->disability_type == 'Visually Impaired') ? 'selected="selected"': '';?>>Visually Impaired</option>
									<option value="Speech and Hearing Impaired" <?php echo($student_details[0]->disability_type == 'Speech and Hearing Impaired') ? 'selected="selected"': '';?>>Speech and/or Hearing Impaired</option>
									<option value="Orthopedic Disorder or Mentally Retarded" <?php echo($student_details[0]->disability_type == 'Orthopedic Disorder or Mentally Retarded') ? 'selected="selected"': '';?>>Orthopedic Disorder or Mentally Retarded</option>
									<option value="Learning Disability" <?php echo($student_details[0]->disability_type == 'Learning Disability') ? 'selected="selected"': '';?>>Learning Disability</option>
									<option value="Dyslexia" <?php echo($student_details[0]->disability_type == 'Dyslexia') ? 'selected="selected"': '';?>>Dyslexia</option>
								</select>				
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="disability_percentage" class="control-label">Disability Percentage</label>
								<input type="text" name="disability_percentage" class="form-control" value="<?php echo $student_details[0]->disability_percentage; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group"> 
								<label for="disability_number" class="control-label">Enter your Disability Card Number</label>
								<input type="text" name="disability_number" class="form-control" value="<?php echo $student_details[0]->disability_number; ?>" />
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
									<input type="radio" id="category1" class="category" name="caste_category" value="Open" <?php echo ($student['caste_category'] == 'Open' || $student['caste_category'] == 'OPEN' || $student['caste_category'] == 'open')? 'checked' : ''; ?> readonly >
									<label for="Open" class="control-label">Open</label>&nbsp;&nbsp;
									<input type="radio" id="category2" class="category" name="caste_category" value="Reserved" <?php echo ($student['caste_category'] == 'Reserved' || $student['caste_category'] == 'RESERVED' || $student['caste_category'] == 'reserved')? 'checked' : ''; ?> readonly>
									<label for="Reserved" class="control-label">Reserved</label>
								</div>
							</div>		
						</div>
					</div>
					<?php if($student['caste_category'] == 'Reserved' || $student['caste_category'] == 'RESERVED' || $student['caste_category'] == 'reserved'){ ?>
						<div class="row show_caste">
							<div class="col-md-4">
								<div class="form-group">
									<label for="caste" class="control-label">Caste</label>
									<select class="form-control" name="caste" id="caste">
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
									<input type="text" name="sub_caste" id="sub_caste" class="form-control" value="<?php echo $student_details[0]->sub_caste; ?>" readonly />
								</div>
							</div>
						</div>
					<?php } ?>	
							
					<div class="row">	
						<div class="col-md-12">
							<h3 class="add-info">Contact Details</h3>
						</div>  
					</div>	
					
					<div class="row">	
						<div class="col-md-6">
							<div class="form-group">
								<label for="mobile_number" class="control-label">Mobile Number</label>
								<input type="text" name="mobile_number" class="form-control" value="<?php echo $student_details[0]->mobile_number; ?>" data-rule-required="true" data-rule-number="true" />
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label for="email_id" class="control-label">Email Id</label>
								<input type="text" name="email_id" class="form-control" value="<?php echo $student_details[0]->email_id; ?>" data-rule-required="true" data-rule-email="true"/>
							</div>
						</div>
					</div>
					
					<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label for="guardian_email" class="control-label">Guardian Email</label>
								<input type="email" name="guardian_email" class="form-control" value="<?php echo $student_details[0]->guardian_email; ?>"/>
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="guardian_mobile" class="control-label">Guardian Mobile</label>
								<input type="text" name="guardian_mobile" class="form-control" value="<?php echo $student_details[0]->guardian_mobile; ?>"  />
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
								<textarea name="correspondance_address" class="form-control"><?php echo  $student_details[0]->correspondance_address; ?></textarea>
							</div>
						</div>
						<div class="col-md-3 cors_address">
							<div class="form-group">
								<?php $statelists = config_item('states_list');?>
								<label for="state" class="control-label">State Of Residence</label>
								<select id="state" name="state" class="form-control">
									<option value=''>Please Select State</option>
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
								<input type="text" name="city" class="form-control" value="<?php echo $student_details[0]->city; ?>" />
							</div>
						</div>
						<div class="col-md-4 cors_address">
							<div class="form-group">
								<label for="pin" class="control-label">Pincode</label>
								<input type="text" name="pin" class="form-control" value="<?php echo $student_details[0]->pin; ?>" />
							</div>
						</div>
						
						<div class="col-md-4">
							<div class="form-group">
								<label for="country" class="control-label">Country</label>
								<?php $countries = config_item('country_list');?>
								<select name="country" id="country" class="form-control">
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
								<select class="form-control" name="location_category">
									<option value="">Select</option>
									<option value="Urban" <?php if($student_details[0]->location_category == '' || $student['location_category'] == 'Urban'){ echo 'selected="selected"'; } ?>>Urban</option>
									<option value="Rural" <?php echo ($student_details[0]->location_category == 'Rural')? 'selected="selected"' : ''; ?>>Rural</option>
									<option value="Not specified" <?php echo ($student_details[0]->location_category == 'Not specified')? 'selected="selected"' : ''; ?>>Not specified</option>																													 
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
								<textarea name="permanent_address" class="form-control"><?php echo $student_details[0]->permanent_address; ?></textarea>
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
										if(strtoupper($value) == strtoupper(trim($student_details[0]->permanent_state))){
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
								<input type="text" name="permanent_city" class="form-control" value="<?php echo $student_details[0]->permanent_city; ?>" />
							</div>
						</div>
						<div class="col-md-4 per_address">
							<div class="form-group">
								<label for="permanent_taluka" class="control-label">Permanent Taluka</label>
								<input type="text" name="permanent_taluka" class="form-control" value="<?php echo $student_details[0]->permanent_taluka; ?>" />
							</div>
						</div>
						<div class="col-md-4 per_address">
							<div class="form-group">
								<label for="permanent_pin" class="control-label">Permanent Pincode</label>
								<input type="text" name="permanent_pin" class="form-control" value="<?php echo $student_details[0]->permanent_pin; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="permanent_country" class="control-label">Permanent Country</label>
								<select name="permanent_country" id="permanent_country" class="form-control">
									<option value="">Select Country</option>
									<?php 
									foreach($countries as $key=>$value):
										$selected = NULL;
										if(strtoupper($value) == strtoupper($student_details[0]->permanent_country)){
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
								<input type="text" name="guardian_name" class="form-control" value="<?php echo $student_details[0]->guardian_name; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="guardian_profession" class="control-label">Occupation of the Guardian</label>
								<input type="text" name="guardian_profession" class="form-control" value="<?php echo $student_details[0]->guardian_profession; ?>" />
							</div>
						</div>
					</div>
					
					<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label for="guardian_income" class="control-label">Annual Income of the Guardian</label>
								<input type="text" name="guardian_income" data-rule-required="true" class="form-control" value="<?php echo $student_details[0]->guardian_income; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="relationship_to_guardian" class="control-label">Relation with the Guardian</label>
								<input type="text" name="relationship_to_guardian" class="form-control" value="<?php echo $student_details[0]->relationship_to_guardian; ?>" />
							</div>
						</div>
					</div>
					
					<div class="row">
						<div class="col-md-8">
							<div class="form-group">
								<label for="guardian_address" class="control-label">Guardian Address</label>
								<textarea name="guardian_address" class="form-control"><?php echo $student_details[0]->guardian_address; ?></textarea>
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
								<input type="text" name="account_holder_name" class="form-control" value="<?php echo $student_details[0]->acc_holder_name; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="bank_acc_no" class="control-label">Account number</label>
								<input type="text" name="bank_acc_no" class="form-control" value="<?php echo $student_details[0]->bank_acc_no; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="ifsc_code" class="control-label">IFSC Code</label>
								<input type="text" name="ifsc_code" class="form-control" value="<?php echo $student_details[0]->ifsc_code; ?>" />
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
								<input type="text" name="name_of_school" class="form-control" value="<?php echo $student_details[0]->name_of_school; ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="address_of_school" class="control-label">School/College Address</label>
								<textarea name="address_of_school" class="form-control"><?php echo $student_details[0]->address_of_school; ?></textarea>
							</div>
						</div>
					</div>
					
					<div class="row">	
						<div class="col-md-12">
							<h3 class="add-info">Last Exam Details</h3>
							<h4 class="subheading-info">If you are B.Sc. student - please enter details about HSC. If you are M.Sc. student - please enter details about Final year B.Sc. If you are Junior college student - please enter details about SSC.</h4>
						</div>  
					</div>
						
					<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label for="name_of_examination" class="control-label">Name of Examination</label>
								<input type="text" name="name_of_examination" class="form-control" value="<?php echo $student_details[0]->name_of_examination; ?>" />
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
								<select name="year_of_passing" id="year_of_passing" class="form-control">
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
								<input type="number" id="marks_obtained" name="marks_obtained"  class="form-control" value="<?php echo $student['marks_obtained'] ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="marks_outof" class="control-label">Total Marks</label>
								<input type="number" id="marks_outof" name="marks_outof" class="form-control" data-rule-number="true" value="<?php echo $student['marks_outof'] ?>" />
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label for="percentage_in_ssc" class="control-label">Percentage</label>
								<input type="number" id="percentage_in_ssc" name="percentage_in_ssc" class="form-control" value="<?php echo $student['percentage_in_ssc'] ?>" readonly />
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
								<select name="academic_year" id="academic_year" class="form-control">
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
								<input type="text" name="stream" class="form-control" value="<?php echo $student['stream'] ?>" readonly />
							</div>
						</div>
					</div>				
					
					<div class="row">
						<div class="col-md-12">
							<div class="form-group">
								<input type="hidden" name="photo_path" value="<?php echo $student['photo_path']; ?>">
								<input type="hidden" name="curr_gender" value="<?php echo $student['gender']; ?>">
								<input type="hidden" name="curr_specialization" value="<?php echo $student['specialization']; ?>">
								<input type="hidden" name="curr_division" value="<?php echo $student['division']; ?>">
								<input type="hidden" name="curr_course" value="<?php echo $student['course_name']; ?>">
								<input type="hidden" name="curr_academic_year" value="<?php echo $student['academic_year']; ?>">
								<input type="hidden" name="curr_caste" value="<?php echo $student['caste']; ?>">
								<input type="submit" name="submit" class="btn btn-info btn-md" value="Save Student Details" />
							</div>
						</div>
					</div>
					<input type="hidden" name="user_id" value="<?php echo $student['id']; ?>" />    
				</form>
			</div>	
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
							<input type="hidden" name="admission_year" value="<?php echo $student['admission_year']; ?>">
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
	
<?php /* <a id="up" data-target="#basic_details" data-toggle="modal" data-backdrop="static" data-keyboard="false">kkkk</a> */?>
  <!-- /.container-fluid -->

	<script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
	<script>
   /* jQuery(document).ready(function(){
      
    <?php 	
		$ftl = $this->session->userdata("first_time_login");
        if(!$ftl){?>		
            // getting popup state
            var state = localStorage.getItem('popup');
            //console.log("POPUP STATE : " + state);
			// checking popup state
            if(state == null){
				jQuery('#basic_details').modal({
					show: true,
					backdrop: 'static',
					keyboard: false,
				});
            }

			jQuery('#basic_detail').validate();
			
            // Update Contact Information ajax call
            jQuery('#basic_detail').on('submit', function(event){
				alert("22");
				event.preventDefault();
				if(jQuery("#basic_detail").valid()){
					alert("33");
					jQuery.ajax({
						url: "<?php echo base_url(); ?>update_basic_details",
						method: "POST",
						data: new FormData(this),
						contentType: false,
						cache: false,
						processData: false,            
						success:function(data){
							var id = <?php echo $this->session->userdata("id"); ?>;
							//console.log("user_id: " + id);
							// updating first time login
							alert(data);
							jQuery("#update_result").html(data);
							jQuery.ajax({
								url: "<?php echo base_url(); ?>change_first_time_login",
								method: "POST",
								data: { id: id },
								dataType: "json",
								success:function(data){
									alert(data);
									localStorage.setItem('popup','displayed');
									jQuery('#basic_details').modal({backdrop: 'static', keyboard: false});
									setTimeout(function(){ // wait for 5 secs
										location.reload(); // then reload the page.
									}, 5000);
								}
							});							
						}
					});
				}
            });
          <?php
        }?>      
    }); */
  </script>
  