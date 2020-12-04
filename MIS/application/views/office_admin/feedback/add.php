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
			<a href="<?php echo base_url('office_admin/feedback'); ?>">Feedback question List</a>
		</li>
		<li class="breadcrumb-item active"> Add Question</li>
    </ol>

    <!-- Area Chart Example-->
	<?php if(validation_errors()):?>
	<div class="alert alert-danger formValidation" role="alert">
		<?php echo validation_errors();?> 
	</div>
	<?php endif;?>
	
	
	<div class="card mb-3">
		<div class="container editstaff">
		
		<div class="small text-muted ">
			<?php ?>			
			<?php echo form_open_multipart(site_url('office_admin/feedback/add')); ?>	
			<div class="row">
				<div class="col-md-12">
					<h3 class="add-info">Feedback Information</h3>	
				</div>
			</div>
			<div class="row">
			
				<div class="col-md-2">
						<div class="form-group">
							<label for="feedback_type"><strong>Year: </strong></label>
							<select name="feedback_year" id="feedback_year" class="form-control" required>
								<option value="">Select</option>
								<?php 
							
								for($start=2018; $start<=date('Y'); $start++){
									$key_year= $start.'-'.($start+1);
									?>
								<option value="<?php echo $key_year; ?>" <?php echo set_select('feedback_year',$key_year,($key_year==@$_GET['feedback_year'])?true:false ); ?>   ><?php echo $key_year; ?></option>
								<?php } ?>
							</select>
						</div>
					</div>
					
					<div class="col-md-2">
						<div class="form-group">
							<label for="feedback_type"><strong>Feedback Type: </strong></label>
							<select name="feedback_type" id="table-filter-feedback" class="form-control" required >
							<?php $feedback_type=array('SSS'=>'SSS','StudentCurriculum'=>'StudentCurriculum','PO'=>'Program Outcome','TA'=>'TAQ'); ?>
								<option value="">Select</option>
								<?php 
								foreach($feedback_type as $key=>$fq){ ?>
								<option value="<?php echo $key; ?>" <?php echo set_select('feedback_type',$key,($key==@$_GET['feedback_type'])?true:false ); ?>   ><?php echo $fq; ?></option>
								<?php } ?>
							</select>
						</div>
					</div>
					
					<div id="stuprogram" style="display: none;width: 100%;">
						<div class="col-md-2">
							<div class="form-group">
								<label for="feedback_type"><strong>Program: </strong></label>
								<select name="program" id="program" class="form-control" >
								<?php $program_type=array('FY'=>'FY','SY'=>'SY','TY'=>'TY','MSY'=>'MSY','MFY'=>'MFY'); ?>
									<option value="">Select</option>
									<?php 
									foreach($program_type as $key=>$fq){ ?>
									<option value="<?php echo $key; ?>" <?php echo set_select('program',$key,($key==@$_GET['program'])?true:false ); ?>   ><?php echo $fq; ?></option>
									<?php } ?>
								</select>
							</div>
						</div>
					</div>
					
					<div id="specializationDP" style="display: none;width: 100%;">
						<div class="col-md-2">
							<div class="form-group">
								<label for="feedback_type"><strong>Stakeholder: </strong></label>
								<select name="stakeholder" id="stakeholder" class="form-control" >
								<?php $stakeholder_type=array('Student'=>'Student','Parents'=>'Parents','Teacher'=>'Teacher','Alumni'=>'Alumni','Employer'=>'Employer'); ?>
									<option value="">Select</option>
									<?php 
									foreach($stakeholder_type as $key=>$fq){ ?>
									<option value="<?php echo $key; ?>" <?php echo set_select('stakeholder',$key,($key==@$_GET['stakeholder'])?true:false ); ?>   ><?php echo $fq; ?></option>
									<?php } ?>
								</select>
							</div>
						</div>
						
						<div class="col-md-2">
							<div class="form-group">
								<label for="feedback_type"><strong>Specialization: </strong></label>
								<select name="specialization" id="specialization" class="form-control" >
								<?php $specialization_type=array('DC'=>'DC','ECCE'=>'ECCE','FND'=>'FND','HTM'=>'HTM','DRM'=>'DRM','MCE'=>'MCE','TAD'=>'TAD'); ?>
									<option value="">Select</option>
									<?php 
									foreach($specialization_type as $key=>$fq){ ?>
									<option value="<?php echo $key; ?>" <?php echo set_select('specialization',$key,($key==@$_GET['specialization'])?true:false ); ?>   ><?php echo $fq; ?></option>
									<?php } ?>
								</select>
							</div>
						</div>
					</div>
			</div>
			
			<div class="row">
				<div class="col-md-8">
					<div class="form-group">
						<label for="feedback_type"><strong>Type: </strong></label><br/>
						<input type="radio" name="type" class="radio_val" value="text"  checked /> Text
						<input type="radio" name="type"  class="radio_val" value="radio"  /> Radio
						<input type="radio" name="type"  class="radio_val" value="header"  /> Header
					</div>
				</div>
				
				
				<div class="col-md-8 question">
					<div class="form-group">
						<label for="question"><strong>Title: </strong></label><br/>
						<input type="text" name="question" required class="form-control" />
					</div>
				</div>
				
				<div class="col-md-8 option_value" style="display:none">
					<div class="form-group">
						<label for="option_value"><strong>Value: </strong></label><br/>
						<input type="text" name="option_value" placeholder="Enter value separated by commas"  class="form-control" />
					</div>
				</div>
				
				
				<div class="col-md-8 placeholder">
					<div class="form-group">
						<label for="placeholder"><strong>Placeholder: </strong></label><br/>
						<input type="text" name="placeholder"  class="form-control"  />
					</div>
				</div>
				
				
			</div>
				
			
			<div class="row">
					<div class="col-md-1">
						<div class="form-group">
						<label ><strong> &nbsp;  &nbsp;  </strong></label>
							<button class="btn btn-primary" type="Submit">Submit</button>
						</div>
					</div>
			</div>
			
		</form>
		
		</div>	
		</div>
	</div>
</div>
   <script>
	jQuery(document).ready(function(){
		
		$(".radio_val").click(function () {
			
			 var radio_val = $(this).val();
			 if(radio_val == "text"){ 
			
					$('.question').css("display","block");
					$('.placeholder').css("display","block");
					$('.option_value').css("display","none");
			 }
			 else if(radio_val == "radio"){
					$('.question').css("display","block");
					$('.placeholder').css("display","none");
					$('.option_value').css("display","block");
			 }
			 else if(radio_val == "header"){
					$('.question').css("display","block");
					$('.placeholder').css("display","none");
					$('.option_value').css("display","none");
			 }
			 
			/* */
		 });
		
		 var feedback_type = $("#table-filter-feedback").val();
        if (feedback_type!='') {
        	$('#feedback_tab').addClass('active show');
        	$('#attendance_tab').removeClass('active show');
        	$('#leaves_tab').removeClass('active show');
        	$('#leaves_analysis_tab').removeClass('active show');
        	$('#library_tab').removeClass('active show');
        	$('#exams_tab').removeClass('active show');
        }else{
        	$('#feedback_tab').removeClass('active show');
        }
        if ((feedback_type=='SSS') || ( feedback_type=='StudentCurriculum')){
        	$("#stuprogram").show();
            $("#stuprogram").css("display","flex");
            $("#specializationDP").hide();
        } else if(feedback_type=='PO'){
            $("#specializationDP").show();
            $("#specializationDP").css("display","flex");
            $("#stuprogram").hide();
        }else{
            $("#specializationDP").hide();
            $("#stuprogram").hide();
        }

        $("#table-filter-feedback").change(function () {

            if ( ( $(this).val() == "SSS" ) || ( $(this).val()=='StudentCurriculum')) {
                $("#stuprogram").show();
                $("#stuprogram").css("display","flex");
                $("#specializationDP").hide();

            } else if ($(this).val() == "PO"){
                $("#specializationDP").show();
                $("#specializationDP").css("display","flex");
                $("#stuprogram").hide();
            }else {
                $("#specializationDP").hide();
                $("#stuprogram").hide();

            }
        });
	});
</script>
  