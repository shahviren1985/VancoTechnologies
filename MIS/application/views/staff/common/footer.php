	<?php if(!empty($this->session->userdata("id"))):?>
        <!-- Sticky Footer -->
        <footer class="sticky-footer">
			<div class="container my-auto">
				<div class="copyright text-center my-auto">
					<span>Copyright © <?php echo date('Y') . " " . APPNAME; ?> </span>
				</div>
			</div>
        </footer>

    </div>
    <!-- /.content-wrapper -->

    </div>
    <!-- /#wrapper -->

    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
		<i class="fas fa-angle-up"></i>
    </a>

    <!-- Update Contact Information -->
<?php /*<div class="modal fade" id="basic_details" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Update Contact Information</h5>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
				<div class="modal-body">
					<div class="container">
						<form action="" method="POST" id="basic_detail">
							<div class="form-group">
								<label for="email">Email Address:</label>
								<input type="email" class="form-control" name="email" id="email" value="<?php echo $student_details[0]->email_id;?>" data-rule-required="true" data-rule-email="true">
							</div>
							<div class="form-group">
								<label for="phone">Mobile Number:</label>
								<input type="text" class="form-control" name="mobile" id="mobile" value="<?php echo $student_details[0]->mobile_number;?>" data-rule-required="true">
							</div>
							<div class="form-group">
								<label for="address">Address:</label>
								<textarea class="form-control" name="address" id="address" data-rule-required="true"><?php echo $student_details[0]->permanent_address;?></textarea>
							</div>
							<div class="row">
								<div class="col-md-6">
									<div class="form-group">
										<label for="state">State:</label>
										<?php $statelists = config_item('states_list');echo $student_details[0]->permanent_state;?>
										<select id="state" name="state" class="form-control" data-rule-required="true">
										<option>Please Select State</option>
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
								<div class="col-md-6">
									<div class="form-group">
										<label for="zip">ZIP:</label>
										<input type="text" class="form-control" name="zip" id="zip" value="<?php echo $student_details[0]->pin;?>" data-rule-required="true" data-rule-number="true">
									</div>
								</div>
							</div>
							<input type="hidden" name="userID" value="<?php echo $this->session->userdata("userID"); ?>">
							<button type="submit" class="btn btn-success">Submit</button>
						</form>
					</div>
				</div>
				<div class="modal-footer" style="justify-content: space-between;">
					<span id="update_result"></span>
					<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
				</div>
			</div>
		</div> 
	</div> */?>
	<?php else:?>
	</div>	
	</div>
	<?php endif;?>
    <!-- Bootstrap core JavaScript-->
    <script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>

    <script src="<?php echo base_url('assets/vendor/bootstrap/js/bootstrap.bundle.min.js'); ?>"></script>

    <!-- Core plugin JavaScript-->
    <script src="<?php echo base_url('assets/vendor/jquery-easing/jquery.easing.min.js'); ?>"></script>

    <!-- Page level plugin JavaScript-->
    <script src="<?php echo base_url('assets/vendor/datatables/jquery.dataTables.js'); ?>"></script>

    <script src="<?php echo base_url('assets/vendor/datatables/dataTables.bootstrap4.js'); ?>"></script>

    <!-- Custom scripts for all pages-->
    <script src="<?php echo base_url('assets/js/sb-admin.min.js') ?>"></script>

    <!-- Demo scripts for this page-->
    <script src="<?php echo base_url('assets/js/demo/datatables-demo.js') ?>"></script>	
	<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
	<script src="<?php echo base_url('assets/js/bootstrap-datepicker.js') ?>"></script>
	
	<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.10/js/select2.min.js"></script>
	<script src="<?php echo base_url('assets/js/jquery.validate.min.js') ?>"></script>
	<script type="text/javascript" src="<?php echo base_url('assets/js/Chart.min.js') ?>"></script>
	<script type="text/javascript" src="<?php echo base_url('assets/js/utils.js') ?>"></script>
	<script src="https://cdn.rawgit.com/rainabba/jquery-table2excel/1.1.0/dist/jquery.table2excel.min.js"></script>
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
      	$("#export").click(function () {
			$("#dataTableFeedbackD").table2excel({
                filename: "Feedback.xls"
            });
		});
		jQuery( ".searchDate" ).datepicker({
			format: 'dd-mm-yyyy' 
		});

		jQuery( "#joining_date" ).datepicker({
			format: 'dd-mm-yyyy' 
		});
		
		$('#joining_date').on('changeDate', function(ev){
			$(this).datepicker('hide');
		});
		
		jQuery( "#retire_date" ).datepicker({
			format: 'dd-mm-yyyy' 
		});
		
		$('#retire_date').on('changeDate', function(ev){
			$(this).datepicker('hide');
		});
		jQuery("#change_password").validate({
			rules: {
				new_password:{required: true,minlength:3},
				conf_password:{required: true,equalTo:'#new_password'},	
			}
		});	
		jQuery('#change_pass').on('click',function(e){
			e.preventDefault();	
			var $this = jQuery(this); 	
			if(jQuery("#change_password").valid()){					
				var data = jQuery("#change_password").serialize();
				jQuery.ajax({				
						url: '<?php echo base_url(); ?>staff/staffs/change_password',
						type: "POST",
						dataType:"json",
						data:data,
						success: function(data){
							$('#changePassModal').modal('toggle');
							$(".print-error-msg").show();
							if(data.success){								
								$(".print-error-msg").removeClass('alert-danger').addClass('alert-success');
								$("#msg").html(data.success);
								$('#change_password')[0].reset();
							}else{
								$(".print-error-msg").removeClass('alert-success').addClass('alert-danger');
								$("#msg").html(data.error);
								$('#change_password')[0].reset();
							}
						}
					});
				}
			});

		jQuery('.text_disabled :input').attr('disabled',true);
			
		jQuery('#edit_data').on('click',function(){
			jQuery(this).hide();
			jQuery('.text_disabled :input').attr('disabled',false);
			jQuery('.text_disabled').removeClass('text_disabled');
			jQuery('.edit_data').show();
		});

		$('#approve_elective_form').validate({
			rules:{
				sem: "required",
				year: "required"
			},
			submitHandler: function(form) {
			var approve_form = jQuery("#approve_elective_form");
			 jQuery.ajax({
					url: '<?php echo base_url('staff/staffs/approve_elective'); ?>',
					type: 'post',
					dataType:"json",
					data: approve_form.serialize(),
					success: function(result){
						if(result.success == true){
						/* 	$('#semester').val(result.semester);
							$('#year1').val(result.year);
							$('#program1').val(result.program);
							setTimeout(function() {
								$('.iframe-container.hiddenrow').show();
							}, 2000);	 */
							var dept = $('#department').val();
							var username = $('#username').val();
							var frame_url = 'https://vancotech.com/Exams/bachelors/ui/ApproveElective.html?specialization='+dept+'&semester='+result.semester+'&year='+result.year+'&program='+result.program+'&approvedby='+username;
							$('.iframe-container iframe').attr('src',frame_url);
						}else{
							$('.iframe-container.hiddenrow').hide();
						}
					}
				});
			},
		});
		
		
		
		/*$('#leave_from').datepicker({
			format: 'dd-mm-yyyy',
			startDate: 'today',
		});*/
		
		
		
	/* 	$('#leave_to').datepicker({
			format: 'yyyy-mm-dd',
			startDate: 'today'
		}) */
		
		$('#leave_from').on('change', function(){
			var l_from = $(this).val();
			var l_to = $('#leave_to').val();
			var role = $('#role').val();
			var leave_type = $('#leave_type').val();
			if(leave_type == 'Half-Day-Leave'){
				$('#leave_to').val(l_from);
			}else if(l_from != "" && l_to != "" && l_from == l_to){
				$('.hiddenrow').show();
				$('#leave_to').datepicker({
					format: 'dd-mm-yyyy',
					startDate: l_from
				});
			}else{
				$('.hiddenrow').hide();
				$('#leave_to').datepicker({
					format: 'dd-mm-yyyy',
					startDate: l_from
				}); 
			}
		});	
		
		$('#leave_to').on('change', function(){
			//alert("kjgkdfghfjgkh");
			var l_to = $(this).val();
			var l_from = $('#leave_from').val();
			var role = $('#role').val();
			if(l_from != "" && l_to != "" && l_from == l_to){
				$('.hiddenrow').show();
			}else{
				$('.hiddenrow').hide();
			}
		});

		$('#leave_type').on('change', function(){
			//alert("kjgkdfghfjgkh");
			$('.hiddenrow').hide();

			var leave_type = $(this).val();
			//alert(leave_type);
			if(leave_type=='Sick-Leave'){
				//$('#leave_from').datepicker('setDate', null);
				$('#ToDate').show();
				$('#datefrom').text('From');
				$('#leave_from').attr("placeholder", "Leave From");
				$('#leave_docDiv').show();
				//$('#datefrom').text('Date');
			}else if(leave_type=='Compensate-Leave'){
				//$('#leave_from').datepicker('setDate', null);
				$('#leave_from').datepicker('setStartDate', "02-02-2020");
				$('#ToDate').show();
				$('#leave_docDiv').hide();
				$('#datefrom').text('Comp Off Taken On');
				$('#leave_from').attr("placeholder", "Previous Date for Comp Off Taken On");
				//$('#datefrom').text('Date');
			}else if(leave_type=='Half-Day-Leave'){
				//$('#leave_from').datepicker('setDate', null);
				$('#leave_from').datepicker({
					format: 'yyyy-mm-dd',
					startDate: 'today'
				});
				$('#ToDate').hide();
				$('#leave_docDiv').hide();
				$('#datefrom').text('For which Date');
				$('#leave_from').attr("placeholder", "Date for Half Day Leave");
			}
			 else{
			 	// $('#leave_from').datepicker('setStartDate', "02-02-2020");
			 	$('#leave_from').datepicker({
			 		format: 'yyyy-mm-dd',
					startDate: 'today'
				});
			 	$('#ToDate').show();
				$('#leave_docDiv').hide();
				$('#datefrom').text('From');
				$('#leave_from').attr("placeholder", "Leave From");
				// $('#leave_to').prop("readonly",false);
			}
			
		});
		
		jQuery("#leave_form").validate({
			rules: 
			{
				leave_type:"required",
				leave_from: "required",
				leave_to: "required",
				leave_reason: "required",
				leave_pending_work: "required",
				leave_hand_given: "required",
				leave_type: "required",
			},
			submitHandler: function(form) {
			var leave_form = jQuery("#leave_form");
			var form = $('#leave_form')[0];
			var data = new FormData(form);
			var d1 = $('#leave_from').datepicker('getDate');
			var d2 = $('#leave_to').datepicker('getDate');
			if(d1 == d2){
				var diff = 1;
			}else{
				var oneDay = 24*60*60*1000;
				var diff = 0;
				if (d1 && d2) {
				  diff = Math.round(Math.abs((d2.getTime() - d1.getTime())/(oneDay)));
				  diff = diff + 1;
				}
			}
			var leave = $("input[name='leave_type']:checked").attr('data-value');
			if(leave != 0 && diff > leave){
				alert("You don't have enough leave. Please check your leave balance");
			}else{
				$('#days').val(diff);
				jQuery.ajax({
					url: leave_form.attr('action'),
					type: 'post',
					dataType:"json",
					data: data,
					success: function(result){
						if(result.success == true){
							jQuery(".print-error-msg").html(result.msg);
								setTimeout(function() {
									$('#addLeaveRequest').modal('hide');
									window.location.reload();
								}, 4000);	
						}else{
							jQuery(".print-error-msg").html(result.msg);
						}
					}
				}); 
				}
			},
		});
		
		
		jQuery('#staff_tab_list a').on('click', function (e) {
			e.preventDefault();
			jQuery(this).tab('show');
		});	
		
		jQuery("#logout").click(function(){
			localStorage.removeItem('popup');
		});
		
		
		jQuery.validator.addMethod("fileType", function(value, element, param){
			if(element.files[0].type != param){
				return false;
			} else {
				return true;
			}			
		});
		jQuery.validator.addMethod("fileSize", function(value, element, param){
			if(element.files[0].size >= param){
				return false;
			} else {
				return true; 
			}
		});
		
		
		jQuery('#upload_new_doc').validate({
			rules:{
				doc:{
					required:true,
					fileType:'application/pdf',
					fileSize:5000000,
				}
			},
			messages:{
				doc:{
					fileType:'Only pdf required',
					fileSize:'File size should not exceed 5M',
				}
			},
			submitHandler: function (form) {
				var file = jQuery('#doc')[0].files[0]; // You need to use standard JavaScript object here				
				var doc_type = jQuery('#doc_type').val();
				var doc_name = jQuery('#doc_name').val();
				jQuery.ajax({
                    url: 'https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=staff&crn=<?php echo $this->session->userdata('userID');?>&docType='+doc_type,  
                    type: 'post',
                    data: file,
                    cache:false,
					contentType: false,
					crossDomain :true,
					processData: false,
					headers :{
						'X-File-Name': file.name,
					},
                    success: function(response){
						console.log(response);
						jQuery('#upload_new_doc')[0].reset();
                        if(response){
							jQuery('.uploadMessage').html('<div class="alert alert-success">'+response+'</div>');
						}
                    }
					/* fail:function(jqXHR, exception){
						console.log(jqXHR);
					} */
                });
			}
		});	
		
		jQuery('#feedback_tab_list a').on('click', function (e) {
			e.preventDefault();
			jQuery(this).tab('show');
		});	
		
		jQuery('.student_type_radio input').on('change',function(){
			var auth_student_course = jQuery('#student_course_year').val();
			jQuery("#course_year option").attr("disabled",false);
			if(auth_student_course){
				jQuery('#course_year').val(auth_student_course);
			} else {
				jQuery('#course_year').val('');
			}			
			if(jQuery(this).val() == 'alumni'){
				jQuery('#course_year').val('TY');
				jQuery("#course_year option").attr("disabled","disabled");
				jQuery("#course_year option:contains('TY')").attr("disabled",false);
			}
		});
		
		//calculate application fees
		jQuery(document).on('change','#app_fees_quantity',function(){			
			var app_fees = parseInt(jQuery('#application_type option:selected').attr('data-amount')),
				num_quantity = jQuery('#application_type option:selected').attr('data-quantity');				
			var fees_amount = parseInt(app_fees * (jQuery(this).val()/num_quantity));			
			jQuery('#app_fees_amount').text(fees_amount.toFixed(2));
			var online_charge = parseFloat((fees_amount/100) * 2);
			var gst_charge = parseFloat((online_charge/100) * 18);
			var total_charge = parseFloat(fees_amount + online_charge + gst_charge);
			jQuery('#online_processing_fee').text(online_charge.toFixed(2));			
			jQuery('#total_app_amount').text(total_charge.toFixed(2));
			jQuery('#gst_fee').text(gst_charge.toFixed(2));
			jQuery('#fees_amount').val(fees_amount.toFixed(2));
			jQuery('#online_amount').val(online_charge.toFixed(2));
			jQuery('#total_amount').val(total_charge.toFixed(2));
			
		})
		
		jQuery(document).on('change','#application_type',function(){
			var app_fees = parseInt(jQuery('#application_type option:selected').attr('data-amount')),
				num_quantity = jQuery('#application_type option:selected').attr('data-quantity');
			if(isNaN(app_fees)) {
				app_fees = 0;
			}
			if(num_quantity == ''){
				return false;
			}
			jQuery('#app_fees_quantity').attr('min',num_quantity).attr('step',num_quantity).val(num_quantity);
			jQuery('#app_fees_amount').text(app_fees.toFixed(2));
			var online_charge = parseFloat((app_fees/100) * 2);
			var gst_charge = parseFloat((online_charge/100) * 18);
			var total_charge = parseFloat(app_fees + online_charge + gst_charge);
			jQuery('#online_processing_fee').text(online_charge.toFixed(2));
			jQuery('#total_app_amount').text(total_charge.toFixed(2));
			jQuery('#gst_fee').text(gst_charge.toFixed(2));
			jQuery('#fees_amount').val(app_fees.toFixed(2));
			jQuery('#online_amount').val(online_charge.toFixed(2));
			jQuery('#total_amount').val(total_charge.toFixed(2));
		})
		
		if(jQuery('.student_type_radio input:checked').val() == 'alumni'){
			jQuery('.student_type_radio input').trigger('change');
		}
		jQuery('#admission_year').datepicker({
			minViewMode: 'years',
			format: 'yyyy',
			autoclose: true,
		});
		
		jQuery('.student_field_radio input').on('change',function(){
			jQuery('#course_year').val('');
			jQuery("#semester option").attr("disabled",false);
			if(jQuery(this).val() == 'master'){
				jQuery("#semester option:contains('Sem 5')").attr("disabled","disabled");
				jQuery("#semester option:contains('Sem 6')").attr("disabled","disabled");
			}
		});
		
		if(jQuery('.student_field_radio input:checked').val() == 'master'){
			jQuery('.student_field_radio input').trigger('change');
		}
		
		jQuery("#semester").on('change',function(event){
			var sem = jQuery(this).val();
			if(!sem){
				return false;
			}
			jQuery('#sub_credits').val('');
			jQuery('#sub_fees_amount').text('0.00');
			jQuery('#online_processing_fee').text('0.00');
			jQuery('#total_app_amount').text('0.00');
			jQuery('#subject_fees').val('0.00');
			jQuery('#online_amount').val('0.00');
			jQuery('#total_amount').val('0.00');
			var spec = jQuery('#spec_shortform').val();
			jQuery('#gst_fee').text('0.00');
			//	var data = {spec:spec};
			
			var data = {sem:sem,specialization:'honors',Course:'bsc'};
			//jQuery.getJSON('<?php echo base_url('assets/json/');?>'+sem+'.json',data, function(jd) {	
			jQuery.getJSON('https://vancotech.com/Exams/bachelors/API/api/User/GetPaperList', data, function(jd) {				
				var items = [];
				jQuery.each(jd, function(key, val) {
					if(val.specialisationCode == spec){						
						jQuery.each(val.paperDetails, function(key1, val1) {
							items.push('<option value="' + val1.paperCode + '" data-credit="'+val1.credits+'">' + val1.paperTitle + '</option>');
						});						
					}					
				});
				jQuery('#subject').html('<option value="">Select Subject</option>'+items);
			});
		});
		
		jQuery(document).on('change','#subject',function(){
			if(jQuery(this).val() == ''){
				jQuery('#sub_credits').val('');
			}
			var credit = parseInt(jQuery('#subject option:selected').attr('data-credit'));
			if(!credit){
				return false;
			}
			jQuery('#sub_credits').val(credit);
			var subject_fees = parseInt(750 * credit);
			jQuery('#sub_fees_amount').text(subject_fees.toFixed(2));
			var online_charge = parseFloat((subject_fees/100) * 2);
			var gst_charge = parseFloat((online_charge/100) * 18);
			var total_charge = parseFloat(subject_fees + online_charge + gst_charge);
			jQuery('#online_processing_fee').text(online_charge.toFixed(2));
			jQuery('#total_app_amount').text(total_charge.toFixed(2));
			jQuery('#gst_fee').text(gst_charge.toFixed(2));
			jQuery('#subject_fees').val(subject_fees.toFixed(2));
			jQuery('#online_amount').val(online_charge.toFixed(2));
			jQuery('#total_amount').val(total_charge.toFixed(2));
		})		
	});
	<?php if($this->uri->segment(2)=="performance"){?>
	var ssc = '<?php echo $student[0]->percentage_in_ssc;?>',
		 marks_obtained = '<?php echo $student[0]->marks_obtained;?>',
		 marks_outof = '<?php echo $student[0]->marks_outof;?>',
		 crn = '<?php echo $student[0]->userID;?>',
		 grad = '';			
	var hsc = parseFloat(parseInt(marks_obtained, 10) * 100) / parseInt(marks_outof, 10);	
	hsc = hsc.toFixed(2);
	var marks = [];	
	jQuery.getJSON("https://vancotech.com/Exams/bachelors/API/api/student/GetSummaryGraphData?ssc="+ssc+"&hsc="+hsc+"&grad="+grad+"&crn="+crn, function(data) {
		var name = ['12th', 'Sem1', 'Sem2', 'Sem3', 'Sem4', 'Sem5', 'Sem6'];		
		marks.push(parseFloat(data.hscpercent));
		marks.push(parseFloat(data.sem1percent));
		marks.push(parseFloat(data.sem2percent));
		marks.push(parseFloat(data.sem3percent));
		marks.push(parseFloat(data.sem4percent));
		marks.push(parseFloat(data.sem5percent));
		marks.push(parseFloat(data.sem6percent));		
		var chartdata = {
			labels: name,
			datasets: [
				{
					label: 'Percentage',
					backgroundColor: window.chartColors.red,
					borderColor: window.chartColors.red,
					data: marks,
					fill: false,
				}
			]
		};

		var graphTarget = jQuery("#performanceChart");

		var barGraph = new Chart(graphTarget, {
			type: 'line',
			data: chartdata,
			options: {
				responsive: true,
				title: {
					display: true,
					text: 'Academic Performance'
				},
				tooltips: {
					mode: 'index',
					intersect: false,
				},
				hover: {
					mode: 'nearest',
					intersect: true
				},
				scales: {					
					yAxes: [{
						ticks: {
							min: 30,
							max: 100
						}
					}]
				}
			}
		});
	});
	<?php }?>
    </script>
  </body>
</html>