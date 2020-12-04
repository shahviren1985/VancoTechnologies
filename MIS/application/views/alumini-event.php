<!DOCTYPE html>
<html lang="en">

  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SVT Alumni– Registration Form</title>
	
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/bootstrap.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/vendor/fontawesome-free/css/all.min.css"/>
	<link rel="stylesheet" type="text/css" href="<?php echo base_url() ?>assets/css/style2.css"/>
	
    <!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css"/>


    <link href="https://fonts.googleapis.com/css?family=Roboto:400,400i,500,500i,700,700i,900" rel="stylesheet">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  </head>
<body>
<div id="content-wrapper" class="wrapper_bg_img">
	<div class="wrapper">
		<div class="container">
			<div class="top_title">
				<a class="navbar-brand" href="https://svt.edu.in"><img src="<?php echo base_url(); ?>assets/img/logo-white.png" alt="SVT Payment Fees"></a>
				<h1>
					<strong>SVT Alumni Program Registration</strong>
				</h1>
			</div>
			<!--form class="svt_form"-->
			<?php echo form_open('alumini/alumini_event/',array('id' => 'svtaluminieventform'));?>
				<div class="card">
					<div class="card-header">
						<i class="far fa-credit-card"></i> Alumni Program
					</div>
					<div class="card-body">
						<?php if($this->session->flashdata('msg')): ?>
							<div class="alert alert-success">
								<?php echo $this->session->flashdata('msg'); ?>
							</div> 
						<?php endif; ?> 
						<div class="row reverse_grid">
							<div class="col-sm-12 col-md-12 col-lg-12 col-xl-12">
								<div class="form-group">
									<label for="first_name">Name: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="name" id="first_name" value=""/>
									<span class="text-danger"></span>
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="phone_number">Mobile: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="phone_number" id="phone_number" value=""/>
									<span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
									<label for="email_address">Email Address: <span class="required-mark">*</span></label>
									<input type="text" class="form-control" name="email_address" id="email_address" value="">
									<span class="text-danger"></span>
								</div>
							</div>
						</div>
					
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<label for="comment">Residential address <span class="required-mark">*</span></label>
									<textarea class="form-control" name="residential_address" rows="3" id="comment"></textarea>
								</div>
							</div>
						</div>	

						<h5 class="form_heading">ACADEMIC INFORMATION - Degree/Diploma</h5>
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group student_field_radio">
									<label for="student_field"><input type="radio" name="student_field" id="bachelor" value="Bachelor"> Bachelors</label>
									<span style="margin:0 20px;"></span>
									<label for="student_field"><input type="radio" name="student_field" id="master" value="Master"> Masters</label>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
								   <label for="">Name of the Specialization:</label>
								   <select name="specialization" id="specialization" class="form-control">
									  <option value="">Select</option>
									 
								   </select>
								   <span class="text-danger"></span>
								</div>
							</div>
							
							<div class="col-sm-12 col-md-6 col-lg-6 col-xl-6">
								<div class="form-group">
								   <label for="">Year of passing:</label>
								   <select name="year_of_passing" id="year_of_passing" class="form-control"></select>
								   <span class="text-danger"></span>
								</div>
							</div>
						</div>
						
						
						<div class="row">
							<div class="col-sm-12">
								<p class="">Are you working in industry/teaching?</p> 
								<div class="form-group student_field_radio">	

									
									<label for="student_field"><input type="radio" name="working_status" id="working_yes" value="yes"> Yes</label>
									<span style="margin:0 20px;"></span>
									<label for="student_field"><input type="radio" name="working_status" id="working_no" value="no" checked="checked"> No</label>  
									
								</div>
								<div id="curent_org_section"> 
									<div class="row">
										<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
											<div class="form-group">
												<label for="current_org">Current Organization Name <span class="required-mark">*</span></label>
												<input type="text" class="form-control" name="current_org" id="current_org" value=""/>
												<span class="text-danger"></span>
											</div>	
										</div>	
										<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
											<div class="form-group">
												<label for="current_designation">Current Designation  <span class="required-mark">*</span></label>
												<input type="text" class="form-control" name="current_designation" id="current_designation" value=""/>
												<span class="text-danger"></span>
											</div>	
										</div>
										<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
											<div class="form-group">
												<label for="total_exp">Overall Years Of Experience <span class="required-mark">*</span></label>
												<input type="number" class="form-control" name="total_exp" id="total_exp" value=""/>
												<span class="text-danger"></span>
											</div>	
										</div> 
									</div>
								</div>
							</div>
						</div>
						
						<div class="row">
							<div class="col-sm-12">
								<p class="">Have you enrolled for higher education / pursuing higher education?</p> 
								<div class="form-group student_field_radio">
									
									<label for="higher_education_status"><input type="radio" name="higher_education_status" id="higher_education_status_yes" value="yes"> Yes</label>
									<span style="margin:0 20px;"></span>								
									<label for="higher_education_status"><input type="radio" name="higher_education_status" id="higher_education_status_no" value="no" checked="checked"> No</label>  
									
								</div>
								<div id="curent_edu_section"> 
									<div class="row">
										<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
											<div class="form-group">
												<label for="name_of_qualification">Name Of Qualification<span class="required-mark">*</span></label>
												<input type="text" class="form-control" name="name_of_qualification" id="name_of_qualification" value=""/>
												<span class="text-danger"></span>
											</div>	
										</div>	
										<div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
											<div class="form-group">
												<label for="college_university_name">College Name / University Name.<span class="required-mark">*</span></label>
												<input type="text" class="form-control" name="college_university_name" id="college_university_name" value=""/>
												<span class="text-danger"></span>
											</div>	
										</div>
										
									</div>
								</div>
							</div>
						</div>
						<table class="table">
							<tbody class="aliceblue">
								<tr>
									<th>Program Registration Fees</th>
									<th>₹ <span id="app_fees_amount">1000.00</span></th>
								</tr>
								<tr>
									<td>Online Payment Processing Charges + GST </td>
									<td>₹ <span id="online_processing_fee">25.00</span></td>
								</tr>
								<!--<tr>
									<td>GST (18%)</td>
									<td>₹ <span id="gst_fee">0.00</span></td>
								</tr> -->
								<tr>
									<th>Grand Total</th>
									<th>₹ <span id="total_app_amount">1025.00</span></th>
								</tr>
							</tbody>
						</table>
						<input type="hidden" id="fees_amount" name="fees_amount" value="1000">
						<input type="hidden" id="online_amount" name="online_amount" value="25.00">
						<input type="hidden" id="total_amount" name="total_amount" value="1025.00">
						
						
						<div class="row">
							<div class="col-sm-12 col-md-12 col-lg-12 col-xl-12">
								<!--input type="submit" id="alumini-submit" class="btn btn-primary" name="submit" value="Save"-->
								<input type="hidden" name="hidden_data" id="hidden_data" value="1">
								<input type="submit" class="btn btn-success" value="Register">
								<div class="refund-policy"><a href="<?php echo base_url() ?>refund-policy" target="_blank">Refund Policy</a></div>
							</div>
						</div>
					</div>
				</div>
			</form>
		</div>
	</div>
</div>
</body>
<script src="https://cdn.jsdelivr.net/jquery/1.11.1/jquery.min.js"></script>  
<script src="https://cdn.jsdelivr.net/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<!-- BootstrapValidator JS -->
<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<script>
	$(document).ready(function(){
		var start = 1960;
		var end = new Date().getFullYear();
		var end2 = end + 1;
		var options = "";
		for(var year = start ; year <=end2; year++){	
		  options += "<option>"+ year +"</option>";
		}
		document.getElementById("year_of_passing").innerHTML = options; 
		
		$('.student_field_radio input[type="radio"]').on('change', function(){
			 if (this.value == 'Bachelor') {
				$('#specialization').html('<option value="Developmental Counseling">Developmental Counseling</option><option value="Early Childhood Care and Education">Early Childhood Care and Education</option><option value="Food, Nutrition and Dietitics">Food, Nutrition and Dietitics</option><option value="Hospitality & Tourism Management">Hospitality & Tourism Management</option><option value="Interior Designing and Resource Management">Interior Designing and Resource Management</option><option value="Mass Communication & Extension">Mass Communication & Extension</option><option value="Textile & Apparel Designing">Textile & Apparel Designing</option><option value="Composite Home Science">Composite Home Science</option>');
			}
			else if (this.value == 'Master') {
				$('#specialization').html('<option value="Diabetes & Cardiac Nutrition">Diabetes & Cardiac Nutrition</option><option value="Renal Nutrition">Renal Nutrition</option><option value="Pediatric Nutrition">Pediatric Nutrition</option><option value="Fashion Design">Fashion Design</option><option value="Regular">Regular</option>');
			}
		});
		
		$("#bachelor").click();
    
		$("#curent_edu_section").css("display", "none");
		$("#curent_org_section").css("display", "none");
		$('input[type=radio][name=working_status]').change(function() 
		{
			if (this.value == 'yes') {
				//alert("Yes");			
				$("#curent_org_section").css("display", "block");				
			}
			else if (this.value == 'no') {
				//alert("No");
				$("#curent_org_section").css("display", "none");
			}
		});
		
		$('input[type=radio][name=higher_education_status]').change(function() 
		{
			if (this.value == 'yes') {
				//alert("Yes");			
				$("#curent_edu_section").css("display", "block");				
			}
			else if (this.value == 'no') {
				//alert("No");
				$("#curent_edu_section").css("display", "none");
			}
		});
		
		
		

		
		
	});
</script>	 