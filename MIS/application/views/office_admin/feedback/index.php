<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('student/home'); ?>">Home</a>
		</li>
		<li class="breadcrumb-item">Feedback Form list</li>
		
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('office_admin/feedback/add'); ?>">Add New </a>
		</li>
		
    </ol>

	<?php if($this->session->flashdata('msg')): ?>
		<div class="alert alert-success">
			<?php echo $this->session->flashdata('msg'); ?>
		</div> 
	<?php endif; ?>    
    <!-- DataTables Example -->
    <div class="card mb-3">
     
      <div class="card-body">
	  
	  
	  			<form class="" method="get" action="">
				<div class="row align-item-center">
					<div class="col-md-12">
						<p><strong>Filter Search:</strong></p>
					</div>
					
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
							<select name="feedback_type" id="table-filter-feedback" class="form-control" >
							<?php $feedback_type=array('SSS'=>'SSS','StudentCurriculum'=>'StudentCurriculum','PO'=>'Program Outcome','TA'=>'TAQ'); ?>
								<option value="">Select</option>
								<?php 
								foreach($feedback_type as $key=>$fq){ ?>
								<option value="<?php echo $key; ?>" <?php echo set_select('feedback_type',$key,($key==@$_GET['feedback_type'])?true:false ); ?>   ><?php echo $fq; ?></option>
								<?php } ?>
							</select>
						</div>
					</div>
					
					
					<div class="col-md-1">
						<div class="form-group">
						<label ><strong> &nbsp;  &nbsp;  </strong></label>
							<button class="btn btn-primary" type="Submit">Search</button>
						</div>
					</div>
					<div class="col-md-1">
						<div class="form-group">
								<label for="s_academic_year"><strong>  &nbsp;   &nbsp; &nbsp;   &nbsp; &nbsp; </strong></label>
								<a href="<?php echo site_url('office_admin/feedback'); ?>" class="btn btn-secondary reset_filter_search">Reset</a>
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
			</form>
	  
        <div class="table-responsive">
          <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0" style="text-transform:capitalize;">
            <thead>
              <tr>
			  <th>Id</th>
				<th>Type</th>
				<th>Question</th>
				<th>Placeholder</th>
				<th>Operation</th>
              </tr>
            </thead>          
			<?php if(!empty($feedbacks)){?>
				<tbody>
				<?php 
				foreach ($feedbacks as $key=>$qr) {?>
				<tr>
					<td><?php echo $qr['id'] ?></td>
					<td><?php echo $qr['type'] ?></td>
					<td><?php echo $qr['question'] ?></td>
					<td><?php echo $qr['placeholder'] ?></td>
					<td><a href="<?php echo site_url('office_admin/feedback/edit/'.$qr['id']); ?>" /><i class="fa fa-edit "></i></a> </td>
				</tr>
				<?php } ?>              
				</tbody>
			<?php }else{ ?>
				<tbody>     
					<tr class="odd"><td valign="top" colspan="5" class="dataTables_empty">No Record found</td></tr>
				</tbody>
			<?php } ?>
          </table>
        </div>
      </div>
      <div class="card-footer"></div>
    </div>
  </div>
  <!-- /.container-fluid -->
  
  <script>
	jQuery(document).ready(function(){
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
  