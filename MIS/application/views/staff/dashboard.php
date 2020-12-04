<?php defined('BASEPATH') or exit('No direct script access allowed'); ?>
<style>
   .dashboardDiv {
   height: 400px;
   overflow-x: hidden;
   overflow-y: scroll;
   margin-bottom: 30px;
   margin-top: 20px;
   }
</style>
<div id="content-wrapper">
<div class="container-fluid">
   <div class="card mb-3">
      <div class="card-header">
         <i class="fas fa-list"></i>
         Dashboard 
      </div>
      <div class="card-body">
         <ul class="nav nav-tabs card-header-tabs" id="feedback_tab_list" role="tablist">
            <li class="nav-item">
               <a class="nav-link <?php if (empty($_POST['feedback_type']))
                  {
                      echo "active";
                  } ?>" href="#attendance_tab" role="tab" aria-controls="attendance_tab" aria-selected="true">Attendance </a>
            </li>
            <li class="nav-item">
               <a class="nav-link" href="#leaves_tab" role="tab" aria-controls="leaves_tab" aria-selected="true">Leaves</a>
            </li>
            <li class="nav-item">
               <a class="nav-link" href="#leaves_analysis_tab" role="tab" aria-controls="leaves_analysis_tab" aria-selected="true">Leave Analysis </a>
            </li>
            <li class="nav-item">
               <a class="nav-link" href="#library_tab" role="tab" aria-controls="library_tab" aria-selected="true">Library</a>
            </li>
            <?php // if($user_details[0]->course_name =='TY'){
               ?>
            <li class="nav-item">
               <a class="nav-link" href="#exams_tab" role="tab" aria-controls="exams_tab" aria-selected="true">Exam</a>
            </li>
            <li class="nav-item">
               <a class="nav-link <?php if (!empty($_POST['feedback_type']))
                  {
                      echo "active";
                  } ?>" href="#feedback_tab" role="tab" aria-controls="feedback_tab" aria-selected="true">Feedback </a>
            </li>
            <?php // }
               ?>
         </ul>
         <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade show active p-3" id="attendance_tab" role="tabpanel" aria-labelledby="attendance_tab">
               <div class="card-body">
                  <div class="dashboardDivs">
                     <form class="filter_leaves_head" id="filter_leave_head" action="<?php echo base_url(); ?>staff/staffs/searchRequestHead" method="post">
                        <div class="row ">
                           <div class="col-md-3">
                              <p style="margin-top: 8px"><strong>Attendance Status : </strong></p>
                           </div>
                           <div class="col-md-2" style="margin-left: -90px" >
                              <select name="s_status" id="table-filter" class="form-control">
                                 <option selected>ABSENT</option>
                                 <option>PRESENT</option>
                              </select>
                           </div>
                           <div class="col-md-1">
                           </div>
                           <div class="col-md-3">
                              <label for="s_type" style="margin-top: 10px"><strong> Date :</strong></label>
                              <input type="text" name="startdate" id="startdate" class="searchDate" value="<?php if (isset($staff_attendance_data[0]->punchin))
                                 {
                                     echo date("d-m-Y", strtotime($staff_attendance_data[0]->punchin));
                                 } ?>" autocomplete="off" required>
                           </div>
                           <div class="col-md-1">
                              <div class="form-group">
                                 <button class="btn btn-primary" id="search_request_head" name="search_request_head">Search</button>
                              </div>
                           </div>
                           <div class="col-md-2">
                              <div class="form-group">
                                 <button type="reset" class="btn btn-secondary reset_filter_search">Clear Filter</button>
                              </div>
                           </div>
                        </div>
                        <br>
                        <div class="processing_loader">Processing...</div>
                     </form>
                     <br>						
                     <table class="table table-bordered" width="100%" id="dataTableAttendanceD" cellspacing="0" style="text-transform:capitalize;">
                        <thead>
                           <tr class="t-heading">
                              <th class="fs-16">Emp Code </th>
                              <th class="fs-16">Emp Name</th>
                              <th class="fs-16">Status </th>
                              <th class="fs-16">Department</th>
                              <th class="fs-16">Teaching</th>
                              <th class="fs-16">Permananent</th>
                              <th class="fs-16">In Time</th>
                              <th class="fs-16">Out Time</th>
                           </tr>
                        </thead>
                        <tbody>
                           <?php foreach ($staff_attendance_data as $attendance)
                              { ?>
                           <tr>
                              <td><?php echo $attendance->employee_code; ?></td>
                              <td><?php echo $attendance->firstname . ' ' . $attendance->lastname; ?></td>
                              <?php if ($attendance->employee_code)
                                 { ?>
                              <td style="background: green;font-weight: bold;">PRESENT</td>
                              <?php
                                 }
                                 else
                                 { ?>
                              <td style="background: red;font-weight: bold;">ABSENT</td>
                              <?php
                                 } ?>
                              <td class="text-center"><?php echo $attendance->department; ?></td>
                              <td><?php echo $attendance->role; ?></td>
                              <td><?php echo $attendance->type; ?></td>
                              <td><?php echo date("d-m-Y H:i:s", strtotime($attendance->punchin)); ?></td>
                              <td><?php echo date("d-m-Y H:i:s", strtotime($attendance->punchout)); ?></td>
                           </tr>
                           <?php
                              } ?>
                        </tbody>
                     </table>
                  </div>
               </div>
            </div>
            <div class="tab-pane fade p-3" id="leaves_tab" role="tabpanel" aria-labelledby="leaves_tab">
               <div class="card-body">
                  <div class="dashboardDivd">
                     <form class="filter_staffs" id="filter_staff" action="" method="post">
                        <div class="row align-item-center">
                           <div class="col-md-4">
                              <label for="s_type"><strong> From Date</strong></label>
                              <input type="text" name="startdate" id="startdate" class="searchDate" value="<?php $Date = date('d-m-Y');
                                 echo date('d-m-Y', strtotime($Date . ' + 1 days')); //if(isset($startDate)){ echo $startDate;}
                                  ?>" autocomplete="off" required>
                           </div>
                           <div class="col-md-4">
                              <label for="s_type"><strong> To Date</strong></label>
                              <input type="text" name="enddate" id="enddate" class="searchDate" value="<?php echo date('d-m-Y', strtotime(date('d-m-Y') . ' + 15 days')); //if(isset($endDate)){ echo $endDate;}
                                 ?>" autocomplete="off" required>
                           </div>
                           <div class="col-md-1">
                              <div class="form-group">
                                 <button class="btn btn-primary" id="" name="submit">Search</button>
                              </div>
                           </div>
                           <div class="col-md-1">
                              <div class="form-group">
                                 <a href="<?php echo base_url('officeadmin/staff-leave-list'); ?>" class="btn btn-secondary reset_filter_search">Clear Filter</a>
                              </div>
                           </div>
                           <div class="col-md-3"></div>
                        </div>
                        <div class="processing_loader">Processing...</div>
                     </form>
                     <table class="table table-bordered" width="100%" id="pendingLeaveDataTable" cellspacing="0" style="text-transform:capitalize;">
                        <thead>
                           <tr class="t-heading">
                              <th class="fs-16">Name</th>
                              <th class="fs-16">Department</th>
                              <th class="fs-16">Leave Type</th>
                              <th class="fs-16">From</th>
                              <th class="fs-16">To</th>
                              <th class="fs-16">Status</th>
                              <th class="fs-16">Leave Balance</th>
                           </tr>
                        </thead>
                        <tbody>
                           <?php foreach ($leave_request as $value): ?>
                           <tr>
                              <td><?php echo $value->firstname . ' ' . $value->lastname; ?></td>
                              <td><?php echo $value->department; ?></td>
                              <td><?php echo $value->leave_type; ?></td>
                              <td><?php echo date('d-m-Y', strtotime($value->leave_from)); ?></td>
                              <td><?php echo date('d-m-Y', strtotime($value->leave_to)); ?></td>
                              <td><?php echo $value->status; ?></td>
                              <td style="text-align: center;">
                                 <?php if ($value->leave_type == 'Casual-Leave')
                                    {
                                        echo "CL (" . $value->casual_leave_balance . ")";
                                    }
                                    elseif ($value->leave_type == 'Paid-Leave')
                                    {
                                        echo "PL (" . $value->paid_leave_balance . ")";
                                    }
                                    elseif ($value->leave_type == 'Sick-Leave')
                                    {
                                        echo "SL (" . $value->sick_leave_balance . ")";
                                    }
                                    else
                                    {
                                        echo '-';
                                    }
                                    ?>
                              </td>
                           </tr>
                           <?php
                              endforeach; ?>
                        </tbody>
                     </table>
                  </div>
               </div>
            </div>
            <div class="tab-pane fad p-3" id="leaves_analysis_tab" role="tabpanel" aria-labelledby="leaves_analysis_tab">
               <div class="card-body">
                  -- LEAVES ANALYSIS CONTENT --
               </div>
            </div>
            <div class="tab-pane fade p-3" id="library_tab" role="tabpanel" aria-labelledby="library_tab">
               <div class="card-body">
                  -- LIBRARY CONTENT --
                  <!-- <iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=AlumniPOMCE&crn=<?php echo $this
                     ->session
                     ->userdata('userID'); ?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe> -->
               </div>
            </div>
            <div class="tab-pane fade p-3" id="exams_tab" role="tabpanel" aria-labelledby="exams_tab">
               <div class="card-body">
                  -- EXAM CONTENT --
                  <!-- <iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=ParentsPOMCE&crn=<?php echo $this
                     ->session
                     ->userdata('userID'); ?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe> -->
               </div>
            </div>
            <div class="tab-pane fade p-3" id="feedback_tab" role="tabpanel" aria-labelledby="feedback_tab">
               <div class="card-body">
                  <!-- <h5 class="card-title bg-secondary text-white text-center mb-3">-- FEEDBACK CONTENT --</h5> -->
                  <!-- <iframe src="https://vancotech.com/StudentFeedback/prod/ParentFeedback.html?cc=102&type=ParentsPOMCE&crn=<?php echo $this
                     ->session
                     ->userdata('userID'); ?>&year=<?php echo $user_details[0]->academic_year; ?>" width="100%" height="500px"></iframe> -->
                  <div class="dashboardDivs">
                     <!-- <form class="filter_attendance_head" id="filter_attendance_head" action="<?php echo base_url(); ?>staff/staffs/searchRequestAttendance" method="post"> -->
                     <form class="filter_feedback_head" id="filter_feedback_head1" name="filter_feedback_head" action="" method="post">
                        <div class="row no-gutters text-right">
                           <div class="col-md-2">
                              <p style="margin-top: 8px;margin-right: 12px;"><strong>Year : </strong></p>
                           </div>
                           <div class="col-md-2" style="" >
                              <select name="feedback_year" id="table-filter-year" class="form-control">
                                 <?php 
							
								for($start=2018; $start<=date('Y'); $start++){
									$key_year= $start.'-'.($start+1);
									?>
								<option value="<?php echo $key_year; ?>" <?php echo set_select('feedback_year',$key_year,($key_year==@$_POST['feedback_year'])?true:false ); ?>   ><?php echo $key_year; ?></option>
								<?php } ?>
                              </select>
                           </div>
                           <div class="col-md-2">
                              <p style="margin-top: 8px;margin-right: 12px;"><strong>Feedback Type : </strong></p>
                           </div>
                           <div class="col-md-3" style="" >
                              <select name="feedback_type"id="table-filter-feedback" class="form-control">
                                 <option value="">Select</option>
                                 <option value="SSS" <?php if (!empty($_POST['feedback_type']) && $_POST['feedback_type'] == 'SSS')
                                    {
                                        echo "selected";
                                    } ?> >SSS</option>
                                 <option value="StudentCurriculum" <?php if (!empty($_POST['feedback_type']) && $_POST['feedback_type'] == 'StudentCurriculum')
                                    {
                                        echo "selected";
                                    } ?> >Student Curriculum</option>
									
									 <option value="PO" <?php if (!empty($_POST['feedback_type']) && $_POST['feedback_type'] == 'PO')
                                    {
                                        echo "selected";
                                    } ?> >Program Outcome</option>
									
									
                                 <option value="TA" <?php if (!empty($_POST['feedback_type']) && $_POST['feedback_type'] == 'TA')
                                    {
                                        echo "selected";
                                    } ?>>TAQ</option>
                              </select>
                           </div>
                           <div class="col-md-3">
                              <div class="form-group">
                                 <button class="btn btn-primary" type="submit" id="submit2" name="feedback">Search</button>
								   <button class="btn btn-primary" type="submit" id="submit2" value="excel" name="feedback">Export</button>
                              </div>
                           </div>
                        </div>
                        <div class="">
                           <div id="stuprogram" style="display: none;width: 100%;">
                              <div class="row no-gutters text-right" style="width: 100%;">
                                 <div class="col-md-2">
                                    <p style="margin-top: 8px;margin-right: 12px;"><strong>Program:</strong></p>
                                 </div>
                                 <div class="col-md-2" style="">
                                    <select name="Program"  id="Program" class="form-control">
                                       <option value="FY" <?php if (!empty($_POST['Program']) && $_POST['Program'] == 'FY')
                                          {
                                              echo "selected";
                                          } ?>>FY</option>
                                       <option value="SY" <?php if (!empty($_POST['Program']) && $_POST['Program'] == 'SY')
                                          {
                                              echo "selected";
                                          } ?>>SY</option>
                                       <option value="TY" <?php if (!empty($_POST['Program']) && $_POST['Program'] == 'TY')
                                          {
                                              echo "selected";
                                          } ?>>TY</option>
                                       <option value="MSY" <?php if (!empty($_POST['Program']) && $_POST['Program'] == 'MSY')
                                          {
                                              echo "selected";
                                          } ?>>MSY</option>
                                       <option value="MFY" <?php if (!empty($_POST['Program']) && $_POST['Program'] == 'MFY')
                                          {
                                              echo "selected";
                                          } ?>>MFY</option>
                                    </select>
                                 </div>
                              </div>
                           </div>
                           <div id="specializationDP" style="display: none;width: 100%;">
                              <div class="row no-gutters text-right" style="width: 100%;">
                                 <div class="col-md-2">
                                    <p style="margin-top: 8px;margin-right: 12px;"><strong>Stakeholder : </strong></p>
                                 </div>
                                 <div class="col-md-2" style="" >
                                    <select name="stakeholder" id="stakeholder" class="form-control">
                                       <option value="">Select</option>
                                       <option value="Student" <?php if (!empty($_POST['stakeholder']) && $_POST['stakeholder'] == 'Student')
                                          {
                                              echo "selected";
                                          } ?>>Student</option>
                                       <option value="Parents" <?php if (!empty($_POST['stakeholder']) && $_POST['stakeholder'] == 'Parents')
                                          {
                                              echo "selected";
                                          } ?>>Parents</option>
                                       <option value="Teacher" <?php if (!empty($_POST['stakeholder']) && $_POST['stakeholder'] == 'Teacher')
                                          {
                                              echo "selected";
                                          } ?>>Teacher</option>
                                       <option value="Alumni" <?php if (!empty($_POST['stakeholder']) && $_POST['stakeholder'] == 'Alumni')
                                          {
                                              echo "selected";
                                          } ?>>Alumni</option>
                                       <option value="Employer" <?php if (!empty($_POST['stakeholder']) && $_POST['stakeholder'] == 'Employer')
                                          {
                                              echo "selected";
                                          } ?>>Employer</option>
                                    </select>
                                 </div>
                                 <div class="col-md-2">
                                    <p style="margin-top: 8px;margin-right: 12px;"><strong>Specialization:</strong></p>
                                 </div>
                                 <div class="col-md-3" style="">
                                    <select name="specialization" id="specialization" class="form-control">
                                     
									  <option value="DC" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'DC')
                                          {
                                              echo "selected";
                                          } ?>>DC</option>
                                       <option value="ECCE" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'ECCE')
                                          {
                                              echo "selected";
                                          } ?>>ECCE</option>
                                       <option value="FND" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'FND')
                                          {
                                              echo "selected";
                                          } ?>>FND</option>
                                       <option value="HTM" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'HTM')
                                          {
                                              echo "selected";
                                          } ?>>HTM</option>
                                       <option value="DRM" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'DRM')
                                          {
                                              echo "selected";
                                          } ?>>DRM</option>
                                       <option value="MCE" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'MCE')
                                          {
                                              echo "selected";
                                          } ?>>MCE</option>
                                       <option value="TAD" <?php if (!empty($_POST['specialization']) && $_POST['specialization'] == 'TAD')
                                          {
                                              echo "selected";
                                          } ?>>TAD</option>
										  
                                    </select>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <br>
                        <div class="processing_loader">Processing...</div>
                     </form>
                     <br>	
                     <div class="table-responsive">
                        
                        <table class="table table-bordered" width="100%" id="dataTableFeedbackD" cellspacing="0" style="text-transform:capitalize;">
                           <thead>
                              <tr class="t-heading">
                                 <th class="fs-16">Sr. No </th>
								 <th class="fs-16">Question</th>
								 <?php  foreach($question_option as $qo){ ?>
								  <th class="fs-16"><?php echo $qo['options']; ?> </th>
								 <?php } ?>
                                 <th class="fs-16">Total Response</th>
                              </tr>
                           </thead>
                           <tbody>
							  <?php  foreach($question_array as  $key=>$qa){
										if($qa['type']!='radio'){
											continue;
										}

								  ?>
                              <tr>
                                 <td><?php echo ++$key; ?></td>
                                 <td><?php echo $qa['question']; ?></td>
								 
								 <?php  $count= 0;
								 foreach($question_option as $qo){ ?>
								  <td><?php $total= @$convert_answer[$qa['id']][$qo['priority']]; 
								  $count=$count+$total;
									echo ($total)>0?$total:0;
								       ?>        </td>
								  <?php } ?>
								   <td><?php echo $count; ?></td>
                              </tr>
                             <?php } ?>
                           </tbody>
                        </table>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</div>
<!-- /.container-fluid --> 
<!-- /.container-fluid -->
<style type="text/css">
   /*@media only screen and (max-width: 600px) {
   }*/
</style>