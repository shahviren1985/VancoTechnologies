<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('staff/staffs'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Approve Elective</li>
		</ol>
			 		
		<div class="card1">
			<div class="card-header" id="approve_elective">
				Approve Elective
			</div>
			<div class="card-body">
			
			<?php if($this->session->flashdata('msg')): ?>
				<div class="alert alert-success">
					<?php echo $this->session->flashdata('msg'); ?>
				</div> 
			<?php endif; ?> 
			
			
				<form method="POST" id="approve_elective_form">
				<input type="hidden" name="department" id="department" value="<?php echo $staff_details[0]->department; ?>">
				<input type="hidden" name="username" id="username" value="<?php echo $this->session->userdata('userID'); ?>">
					<div class="row">
						<div class="col-sm-3">
							<div class="form-group">
								<label for="program" class="control-label">Program</label><br>
								<input type="radio" name="program" id="program" value="Regular" checked>
								<label for="regular" class="form-check-label">Regular</label> &nbsp;
								<input type="radio" name="program" id="program" value="Honors">
								<label for="Honors" class="form-check-label">Honors</label>
							</div>
						</div>	
						<div class="col-sm-3">
							<div class="form-group">
								<label for="sem" class="control-label">Semester</label>
								<select name="sem" id="sem" class="form-control">
									<option selected>Select Semester</option>
									<option value="1" selected>1</option>
									<option value="2">2</option>
									<option value="3">3</option>
									<option value="4">4</option>
									<option value="5">5</option>
									<option value="6">6</option>
								</select>
							</div>
						</div>	
						<div class="col-sm-3">
							<div class="form-group">
								<label for="year" class="control-label">Year</label>
								<select name="year" class="form-control">
									<option selected>Select Year</option>
									<?php  
										$cur_year = date('Y');
										$next_year = $cur_year + 1;
										for($i=2019; $i <= $next_year; $i++){
											echo '<option value='.$i.'>'.$i.'</option>'; 
										}
									?>	
								</select>
							</div>
						</div>
						<div class="col-sm-2">
							<br>
							<input type="submit" class="btn btn-primary" name="submit" value="Submit">
						</div>
					</div>
				</form>
				
				
				
				<form  action="<?php echo site_url('staff/approve_elective'); ?>" method="get">
					<div class="row">
						<div class="col-sm-3">
							<div class="form-group">
								<label for="program" class="control-label">Program</label><br>
								<input type="radio" required  <?php echo  ( @$_REQUEST['program']=='Regular'  )?'checked':'';  ?> name="program"  value="Regular" >
								<label for="regular" class="form-check-label">Regular</label> &nbsp;
								<input type="radio" required  <?php echo  ( @$_REQUEST['program']=='Honors'  )?'checked':'';  ?>    name="program" value="Honors">
								<label for="Honors" class="form-check-label">Honors</label>
							</div>
						</div>	
						<div class="col-sm-3">
							<div class="form-group">
								<label for="sem" class="control-label">Semester</label>
								<select name="semester"  required class="form-control">
									<option value="" >Select Semester</option>
									<?php $semester =array('1'=>'Semester 1','2'=>'Semester 2','3'=>'Semester 3','4'=>'Semester 4','5'=>'Semester 5','6'=>'Semester 6'); 
									foreach($semester as $key=>$pg){?>
										<option value="<?php echo $key; ?>" <?php echo  ( @$_REQUEST['semester']==$key  )?'selected':'';  ?> ><?php echo $pg; ?></option>
									<?php  }  ?>
								</select>
							</div>
						</div>	
						<div class="col-sm-3">
							<div class="form-group">
								<label for="year" class="control-label">Year</label>
								<select name="year" required  class="form-control">
									<option value="" >Select Year</option>
									<?php  
										$cur_year = date('Y');
										$next_year = $cur_year + 1;
										for($i=2019; $i <= $next_year; $i++){ ?>
											<option value="<?php echo $i; ?>" <?php echo  ( @$_REQUEST['year']==$i  )?'selected':'';  ?> ><?php echo $i; ?></option>
										<?php  }  ?>
									
								</select>
							</div>
						</div>
						<div class="col-sm-2">
							<br/>
							<input type="submit" class="btn btn-primary" value="Search">
						</div>
					</div>
				</form>
				
				<?php  if(@$_REQUEST['year']!=''  && $_REQUEST['semester']!='' && $_REQUEST['program']!='') { ?>	
				<hr/>
				<form  action="<?php echo site_url('staff/approve_elective'); ?>" method="get">
				<input type="hidden" name="semester" value="<?php echo $_REQUEST['semester'];  ?>" />
				<input type="hidden" name="year" value="<?php echo $_REQUEST['year'];  ?>" />
				<input type="hidden" name="program" value="<?php echo $_REQUEST['program'];  ?>" />
 					
					<div class="row" >
						<div class="row">
								<div class="col-sm-12">
									<h2>Approve Electives for Students</h2> 
								</div>
								
								<div class="col-sm-6">
									<?php
										$specialization_arr= array();
										if($staff_details[0]->department=='HD'){
											$specialization_arr= array( 'DC'=>'Developmental Counseling','ECCE'=>'Early Childhood Care & Education' );
										}
										elseif($staff_details[0]->department=='FND'){
											$specialization_arr= array( 'FND'=>'Food, Nutrition & Dietetics' );
										}
										elseif($staff_details[0]->department=='RM'){
											$specialization_arr= array( 'HTM'=>'Hospitality & Tourism Management','IDRM'=>'Interior Designing & Resource Management' );
										}
										elseif($staff_details[0]->department=='MCE'){
											$specialization_arr= array( 'MCE'=>'Mass Communication & Extensions' );
										}
										elseif($staff_details[0]->department=='MCE'){
											$specialization_arr= array( 'TAD	'=>'Textile & Apparel Designing' );
										}
									?>
								
									
									<select name="specialization" class="form-control">
									<option value="-1">---Select---</option>
									<?php foreach($specialization_arr as $key=>$pg){?>
										<option value="<?php echo $key; ?>" <?php echo  ( @$_REQUEST['specialization']==$key  )?'selected':'';  ?> ><?php echo $pg; ?></option>
									<?php  }  ?>
									</select>
								</div>
								
							<div class="col-sm-6">
								
								<input type="submit" class="btn btn-primary" name="search" value="Search">
							</div>
								
								
							<?php if(@$_REQUEST['search']=='Search' ||  @$_REQUEST['search']=='Save'   ){  ?> 	
								
								<div class="col-sm-12">
								  <br/>
								
								<table  class="table table-bordered table-hover">
								<tbody>
								<tr>
								<th>Serial #</th>
								<th>Roll #</th>
								<th>Student Name</th>
								
								<?php  
								
								$total_code=array();
								foreach($json as $jso){
								if($jso['specialisationCode']==$_REQUEST['specialization']){    ?>
								
										<?php 	 foreach($jso['ElectiveSubject'] as $key=>$display){ ?>
										
											<?php 	 foreach($display['Subject'] as $elect=>$display_res){  
											$total_code[$display_res['Code']]=$display_res['Code'];
											?>
													<th><?php echo $display_res['Title'];  if( isset($display_res['Credit'])  ) {   ?> (Credits: <?php echo $display_res['Credit'];  ?>) <?php } ?> </th>
								<?php } } }}

								$new_code= array_keys($total_code);
								?>
								</tr>
								
								
								<?php 

									$total=array();
									

								foreach($student as $key=>$stu){ ?>
								<tr>
									<td><?php echo $key+1; ?></td>
									<td><?php echo $stu['rollnumber'] ?></td>
									<td><?php echo $stu['studentname'] ?></td>
									
								<?php  
								
								
								foreach($json as $jso){
								if($jso['specialisationCode']==$_REQUEST['specialization']){    ?>
								
										<?php 	 foreach($jso['ElectiveSubject'] as $key=>$display){ ?>
										
											<?php 	 foreach($display['Subject'] as $elect=>$display_res){ 

													$total_code= count($display['Subject']);	
											
											$ae1= $stu['approvedelective1'];
											$ae2= $stu['approvedelective2'];
											$ae3= $stu['approvedelective3'];
											$ae4= $stu['approvedelective4'];
											$ae5= $stu['approvedelective5'];
											$ae6= $stu['approvedelective6'];
											$ae7= $stu['approvedelective7'];
											
											if ($ae1 || $ae2 || $ae3 || $ae4 || $ae5 || $ae6 || $ae7) {
												 $electivePresent = $stu["approvedelective1"] ==$display_res['Code'] || $stu["approvedelective2"] == $display_res['Code'] || $stu["approvedelective3"] == $display_res['Code'] || $stu["approvedelective4"] == $display_res['Code'] || $stu["approvedelective5"] == $display_res['Code'] || $stu["approvedelective6"] == $display_res['Code'] || $stu["approvedelective7"] ==$display_res['Code'];
											}
											else{
												$electivePresent = $stu["elective1"] == $display_res['Code'] || $stu["elective2"] == $display_res['Code'] || $stu["elective3"] == $display_res['Code'] || $stu["elective4"] == $display_res['Code'] || $stu["elective5"] == $display_res['Code'] || $stu["elective6"] == $display_res['Code'] || $stu["elective7"] == $display_res['Code'];
											}
											$checked='';
											if($electivePresent!=''){
												
												$total[$display_res['Code']]  =  @$total[$display_res['Code']]+1;
												$checked='checked';
											}
											
											
											?>
											
													<td align="center"><input type="checkbox" name="elective[<?php echo $stu['id'] ?>][approvedelective<?php echo ($elect+1) ?>]" <?php echo $checked; ?>   value="<?php echo $display_res['Code']; ?>"></td>
								<?php } } }} ?>
									
								</tr>
								

								
								
								<?php }   ?>
									<tr class="footer">
										<td align="center">&nbsp;</td>
										<td align="center">&nbsp;</td>
										<td align="center"><b>Total</b></td>
										<?php foreach($new_code as $tot){  ?>
										<td align="center" ><b><?php  echo $total[$tot]; ?></b></td>
										<?php  } ?>
									</tr>
								
								</tbody>
								</table>
								
								</div>
								
								<div class="col-sm-2">
								<br/>
								<input type="submit" class="btn btn-primary"  name="search"  value="Save">  
								
								<!--<input type="submit" class="btn btn-primary"  name="search"  value="Export">  -->
								</div>
								
							<?php } ?>	
							
						</div>
					</div>
					</form>
				<hr/>
				
				<?php  }  ?>
				
				
				
				<p>&nbsp;</p>
				<div class="iframe-container">
					<?php
					 $username = $this->session->userdata('userID'); 
					//echo "<pre>"; print_r($staff_details); echo "</pre>";	
						$department = $staff_details[0]->department;
					 ?>
					<iframe src="https://vancotech.com/Exams/bachelors/ui/ApproveElective.html?specialization=<?php echo $department; ?>&semester=1&year=2019&program=regular&approvedby=<?php echo $username; ?>" width="100%" height="500px"></iframe>  
				</div>
			</div>
		</div>	
	</div>
<!-- /.container-fluid -->  
