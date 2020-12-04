<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style type="text/css">
  #divLoading {
    display : none;
  }
  #divLoading.show {
    display : block;
    position : fixed;
    z-index: 100;
    background-image : url('<?php echo base_url(); ?>assets/img/loader.gif');
    background-color: #000;
    opacity: 0.7;
    background-repeat : no-repeat;
    background-position : center;
    left : 0;
    bottom : 0;
    right : 0;
    top : 0;
  }
  #loadinggif.show {
    left : 50%;
    top : 50%;
    position : absolute;
    z-index : 101;
    width : 32px;
    height : 32px;
    margin-left : -16px;
    margin-top : -16px;
  }
  div.content {
    width : 1000px;
    height : 1000px;
  }
  .form-group label
  {
	  *display:none;
  }
</style>
<div id="content-wrapper">
	<div class="container-fluid"> 
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
      </li>
	  <li class="breadcrumb-item"><a href="<?php echo base_url('officeadmin/leaving-certificate'); ?>">Leaving Certificate</a></li>
      <li class="breadcrumb-item active">Generate Leaving Certificate</li>
    </ol>
	
	<?php if($this->session->flashdata('msg')): ?>
		<div class="alert alert-success">
			<?php echo $this->session->flashdata('msg'); ?>
		</div> 
	<?php endif; ?> 
	
	<div class="card mb-3">
		<div class="card-header">
        <i class="fas fa-file-excel"></i>
        Generate Leaving Certificate</div>
		<div class="card-body">
			<!--form id="add_staff" enctype="multipart/form-data"-->
			<?php echo form_open('officeadmin/leaving-certificate/add/',array('id' => 'leavecertificateform','target' => '_blank'));?>
				<div class="row">					
							<div class="col-md-6">
								<div class="form-group">
									<label for="registration_number" class="control-label">Registration Number</label>
									<input type="text" name="registration_number" id="registration_number" value="<?php echo $student['userID']; ?>" class="form-control" placeholder="Registration Number">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="name_of_the_student" class="control-label">Name of the Student</label>
									<input type="text" name="name_of_the_student" id="name_of_the_student" class="form-control" placeholder="Name of the Student" value="<?php echo $student['last_name']." ".$student['first_name']." ".$student['father_name']." ".$student['mother_first_name']  ?>">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="caste_and_sub_caste" class="control-label">Caste and Sub-Caste</label>
										<select name="caste_and_sub_caste" id="caste_and_sub_caste" class="form-control">
											<option value="">Select</option>
											<option value="EWS" <?php echo($student['caste_category'] == 'EWS') ? 'selected="selected"': '';?>>EWS (Economically Weaker Section)</option>
											<option value="NT1" <?php echo($student['caste_category'] == 'NT1') ? 'selected="selected"': '';?>>NT (1)</option>
											<option value="NT2" <?php echo($student['caste_category'] == 'NT2') ? 'selected="selected"': '';?>>NT (2)</option>
											<option value="NT3" <?php echo($student['caste_category'] == 'NT3') ? 'selected="selected"': '';?>>NT (3)</option>
											<option value="OBC" <?php echo($student['caste_category'] == 'OBC') ? 'selected="selected"': '';?>>OBC</option>
											<option value="SBC" <?php echo($student['caste_category'] == 'SBC') ? 'selected="selected"': '';?>>SBC</option>
											<option value="SC" <?php echo($student['caste_category'] == 'SC') ? 'selected="selected"': '';?>>SC</option>
											<option value="SEBC" <?php echo($student['caste_category'] == 'SEBC') ? 'selected="selected"': '';?>>SEBC (Socially &amp; Educationally Backward Class)</option>
											<option value="ST" <?php echo($student['caste_category'] == 'ST') ? 'selected="selected"': '';?>>ST</option>
											<option value="VJ" <?php echo($student['caste_category'] == 'VJ') ? 'selected="selected"': '';?>>VJ</option>
											<option value="Open" <?php echo($student['caste_category'] == 'Open') ? 'selected="selected"': '';?>>OPEN</option>
										</select>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="date_of_birth" class="control-label">Date Of Birth</label>
									<input type="text" name="date_of_birth" id="date_of_birth" class="form-control" placeholder="Date Of Birth" value="<?php echo date('d-m-Y',strtotime($student['date_of_birth'])); ?>">
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label for="place_of_birth" class="control-label">Place Of Birth</label>
									<input type="text" name="place_of_birth" id="place_of_birth" class="form-control" placeholder="Place Of Birth" value="<?php echo $student['birth_place'] ?>">
								</div>
							</div>
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="last_college_attended" class="control-label">Last College Attended</label>
									<input type="text" name="last_college_attended" id="last_college_attended" class="form-control" placeholder="Last College Attended" value="<?php echo $student['school_college'] ?>">
								</div>
							</div> 
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="classTxt" class="control-label">Class</label>
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
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="date_of_admission" class="control-label">Date of Admission</label>
									<input type="text" name="date_of_admission" id="date_of_admission" class="form-control" placeholder="Date of Admission" value="<?php echo date('d-m-Y',strtotime(@$student['date_of_admission']))/* echo @$student['date_of_admission'] */ ?>">
								</div>
							</div>
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="date_of_leaving" class="control-label">Date of Leaving</label>
									<input type="text" name="date_of_leaving" id="date_of_leaving" class="form-control" placeholder="Date of Leaving" value="<?php /* echo @$student['date_of_leaving']; */ 
									echo date('d-m-Y',strtotime(@$student['date_of_leaving']));
									?>">
								</div>
							</div>

							<div class="col-md-6">
								<div class="form-group">
									<label for="qualification" class="control-label">Area Of Specialization</label>
									<?php $cur_course[0]->specialization; if($specialization): ?>
									<select name="area_of_specialization" data-rule-required="true" id="area_of_specialization" class="form-control">
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
							<div class="col-md-6">
								<div class="form-group">
									<label for="grade" class="control-label">Grade</label>
									<select name="grade" data-rule-required="true" id="grade" class="form-control">
										<option value="">Select Grade</option>										
										<option value="O+" <?php echo ($student['division'] == 'O+') ? 'selected="selected"' : ''; ?>>O+</option>
										<option value="O" <?php echo ($student['division'] == 'O') ? 'selected="selected"' : ''; ?>>O</option>
										<option value="A+" <?php echo ($student['division'] == 'A+') ? 'selected="selected"' : ''; ?>>A+</option>
										<option value="A" <?php echo ($student['division'] == 'A') ? 'selected="selected"' : ''; ?>>A</option>
										<option value="B+" <?php echo ($student['division'] == 'B+') ? 'selected="selected"' : ''; ?>>B+</option>
										<option value="B" <?php echo ($student['division'] == 'B') ? 'selected="selected"' : ''; ?>>B</option>
										<option value="C" <?php echo ($student['division'] == 'C') ? 'selected="selected"' : ''; ?>>C</option>
										<option value="D" <?php echo ($student['division'] == 'D') ? 'selected="selected"' : ''; ?>>D</option>
										<option value="P" <?php echo ($student['division'] == 'P') ? 'selected="selected"' : ''; ?>>P</option>
										<option value="F" <?php echo ($student['division'] == 'F') ? 'selected="selected"' : ''; ?>>F</option>
										
										<?php /* ?><option value="A-I" <?php echo ($student['division'] == 'A-I') ? 'selected="selected"' : ''; ?>>A - I</option>
										<option value="A-II" <?php echo ($student['division'] == 'A-II') ? 'selected="selected"' : ''; ?>>A - II</option>
										<option value="B" <?php echo ($student['division'] == 'B') ? 'selected="selected"' : ''; ?>>B</option>
										<option value="C-I" <?php echo ($student['division'] == 'C-I') ? 'selected="selected"' : ''; ?>>C - I</option>
										<option value="C-II" <?php echo ($student['division'] == 'C-II') ? 'selected="selected"' : ''; ?>>C - II</option>
										<option value="D-I" <?php echo ($student['division'] == 'D-I') ? 'selected="selected"' : ''; ?>>D - I</option>
										<option value="D-II" <?php echo ($student['division'] == 'D-II') ? 'selected="selected"' : ''; ?>>D - II</option><?php */ ?>
									</select>
								</div>
							</div>	
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="conduct" class="control-label">Conduct</label>
									<input type="text" name="conduct" id="conduct" class="form-control" placeholder="Conduct" value="<?php echo (isset($student['conduct'])) ? $student['conduct'] : 'Good'?>">
								</div>
							</div>	
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="reason_for_leaving" class="control-label">Reason For Leaving</label>
									<input type="text" name="reason_for_leaving" id="reason_for_leaving" class="form-control" placeholder="Reason For Leaving" value="<?php echo (isset($student['reason_for_leaving'])) ? $student['reason_for_leaving'] : ''?>">
								</div>
							</div>	
							
							<div class="col-md-6">
								<div class="form-group">
									<label for="remarks" class="control-label">Remarks</label>
									<input type="text" name="remarks" id="remarks" class="form-control" placeholder="Remarks" value="<?php echo (isset($student['remarks'])) ? $student['remarks'] : ''?>">
								</div>
							</div>	
							<div class="col-md-6">
								<div class="form-group">
									<label for="certificate_issued_date" class="control-label">Certificate issued date</label>
									<input type="text" name="certificate_issued_date" id="certificate_issued_date" class="form-control" placeholder="Certificate issued date" value="<?php echo (isset($student['certificate_issued_date'])) ? $student['certificate_issued_date'] : ''?>">
								</div>
							</div>								
							<div class="col-md-12">
								<div class="form-group">
									<input type="submit" class="btn btn-primary" value="Save Leaving Certificate">
									<div class="print-error-msg text-center"></div>
								</div>
							</div>					
						</div>
					</form>		
		</div>
	</div>

    <!-- Image loader -->
    <div id='divLoading'></div>
    <!-- Image loader -->    

  </div>
  <!-- /.container-fluid -->
  <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
  <script>
    jQuery(document).ready(function()
	{
		jQuery( "#date_of_birth, #date_of_admission, #date_of_leaving, #certificate_issued_date").datepicker({
				format: 'dd-mm-yyyy' 
			});
		jQuery("#date_of_birth, #date_of_admission, #date_of_leaving, #certificate_issued_date").on('changeDate', function(ev){
			jQuery(this).datepicker('hide');
		});
		
		jQuery("#leavecertificateform").validate({
			rules: {
				registration_number: {
					required: true,
					number: true
				} ,
				name_of_the_student: "required",
				caste_and_sub_caste: "required",
				date_of_birth: {
					required: true
				},
				place_of_birth: "required",
				last_college_attended: "required",
				course_name: "required",
				date_of_admission: {
					required: true					
				},
				date_of_leaving: {
					required: true					
				},
				area_of_specialization: "required",
				grade: "required",
				reason_for_leaving: "required",
				remarks: "required",
				certificate_issued_date: {
					required: true					
				},
			},
			messages: {
				registration_number: {
					required: "Please enter a registration number"
				} ,
				name_of_the_student: "Please enter name of the student",
				caste_and_sub_caste: "Please select caste/sub-caste",
				date_of_birth: {
					required: "Please enter date of birth"
				},
				place_of_birth: "Please enter place of birth",
				last_college_attended: "Please enter last college attended",
				course_name: "Please select class",
				date_of_admission: {
					required: "Please enter date of admission"
				},
				date_of_leaving: {
					required: "Please enter date of leaving"
				},
				course_name: "Please select area of specialization",
				grade: "Please select grade",
				reason_for_leaving: "Please enter reason for leaving",
				remarks: "Please enter remarks",
				certificate_issued_date: {
					required: "Please enter certificate issued date"
				},
			}
		});
    });
  </script>