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
	<div class="modal fade" id="basic_details" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
	</div>
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
	<script src="<?php echo base_url('assets/js/bootstrap-datepicker.js') ?>"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.10/js/select2.min.js"></script>
	<script src="<?php echo base_url('assets/js/jquery.validate.min.js') ?>"></script>
	<script type="text/javascript" src="<?php echo base_url('assets/js/Chart.min.js') ?>"></script>
	<script type="text/javascript" src="<?php echo base_url('assets/js/utils.js') ?>"></script>	
    <script>
	jQuery(document).ready(function(){
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
		
		jQuery('.text_disabled :input').attr('disabled',true);
			
		jQuery('#edit_data').on('click',function(){
			jQuery(this).hide();
			jQuery('.text_disabled :input').attr('disabled',false);
			jQuery('.text_disabled').removeClass('text_disabled');
			jQuery('#division').attr('disabled', true);
			jQuery('#course_name').attr('disabled', true);
			jQuery('#specialization').attr('disabled', true);
			jQuery('#left_college').attr('disabled', true);
			jQuery('#dropped_college').attr('disabled', true);
			jQuery('#academic_year').attr('disabled', true);
			jQuery('#gender').attr('disabled', true);
			//jQuery('#year_of_passing').attr('disabled', true);
			jQuery('#caste').attr('disabled', true);
			jQuery('.edit_data').show();
		});
			
		
	/* 	jQuery.validator.addMethod("accept", function(value, element, param){
			return value.match(new RegExp(param + "$"));
		}); 
		
		jQuery('#update_student_details').validate({
			rules: {
				lastname:{
					accept: "^[A-Za-z']+",
				},
				firstname:{
					accept: "^[A-Za-z']+",
				},
				/*pan_number:{
					accept: "^[0-9A-Z]+",
				},	*/				
				/* disability_type:{
					required: function(element) {
						if (jQuery("#physical_disability").prop("checked") == true) {
							return true;
						} else {
							return false;
						}
					}
				},
				disability_percentage:{
					required: function(element) {
						if (jQuery("#physical_disability").prop("checked") == true) {
							return true;
						} else {
							return false;
						}
					}
				},
				disability_number:{
					required: function(element) {
						if (jQuery("#physical_disability").prop("checked") == true) {
							return true;
						} else {
							return false;
						}
					}
				},
				sub_cat:{
					required: function(element) {
						if (jQuery(".category:checked").val() == 'Reserved') {
							return true;
						} else {
							return false;
						}
					}
				},
				caste:{
					required: function(element) {
						if (jQuery(".category:checked").val() == 'Reserved') {
							return true;
						} else {
							return false;
						}
					}
				},
				account_holder_name:{
					required: function(element) {
						if (jQuery(".category:checked").val() == 'Reserved') {
							return true;
						} else {
							return false;
						}
					}
				},
				bank_acc_no:{
					required: function(element) {
						if (jQuery(".category:checked").val() == 'Reserved') {
							return true;
						} else {
							return false;
						}
					}
				},
				ifsc_code:{
					required: function(element) {							
						if (jQuery(".category:checked").val() == 'Reserved') {
							return true;
						} else {
							return false;
						}
					}
				},
				correspondance_address:{
					required: function(element) {
						if (jQuery("#country").val() == 'India') {
							return true;
						} else {
							return false;
						}
					}
				},
				state:{
					required: function(element) {
						if (jQuery("#country").val() == 'India') {
							return true;
						} else {
							return false;
						}
					}
				} ,*/
				/* city:{
					required: function(element) {
						if (jQuery("#country").val() == 'India') {
							return true;
						} else {
							return false;
						}
					}
				}, 
				pin:{
					required: function(element) {
						if (jQuery("#country").val() == 'India') {
							return true;
						} else {
							return false;
						}
					}
				},
				college_reg:{
					required:true,
					maxlength:6,
					number:true,
					remote: {
						url: "<?php echo base_url(); ?>student/students/check_reg_exist",
						type: "POST",
						data:{
							college_old_id: jQuery('#college_reg').attr('data-old-reg'),
						}
					}
				}
			},
			messages:{
				lastname: { accept: "Not a valid last name" },
				firstname: { accept: "Not a valid first name" },
				/*pan_number: { accept: "Not a valid PAN Number" },
				college_reg:{remote: "This Customer Registration number already exist"},
			},
			submitHandler: function (form) {
				jQuery('#update_student_details').submit();
			}
			
		});	 */	
		
		jQuery.validator.addMethod("allowspace", function(phone_number, element) {
				phone_number = phone_number.replace(/\\s+/g, ""); 
				return this.optional(element) || phone_number.length > 9 &&
					phone_number.match(/^[0-9 ]+$/);
			}, "Please enter a valid phone number");

		
		$('#update_student_details').validate({
			rules:{
				firstname: "required",
				lastname: "required",
				middlename: "required",
				mother_first_name: "required",
				emergency_contact: "required",
				aadhaar_number: "required",
				mobile_number: {
					required: true,
					allowspace: true
				},
				email_id: "required",
				guardian_email: "required",
				guardian_mobile: "required",
				correspondance_address: "required",
				state: "required",
				city: "required",
				pin: "required",
				country: "required",
				location_category: "required",
				permanent_address: "required",
				state: "required",
				permanent_city: "required",
				permanent_pin: "required",
				permanent_country: "required",
				guardian_name: "required",
				relationship_to_guardian: "required",
				name_of_school: "required",
				name_of_examination: "required",
				previous_exam_state: "required",
				marks_obtained: "required",
				marks_outof: "required",
				school_college: "required"
				
			}
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
						url: '<?php echo base_url(); ?>student/students/change_password',
						type: "POST",
						dataType:"json",
						data:data,
						success: function(data){
							$('#changePassModal').modal('toggle');
							$(".print-error-msg").show();
							if(data.success){								
								$(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(data.success);
							}else{
								$(".print-error-msg").removeClass('alert-success').addClass('alert-danger').html(data.error);
							}
						}
					});
				}
			});	
			
		jQuery("#change_profile_photo").validate({
			rules: {
				new_photo:{
					required: true
				}
			}
		});	
			
			
		jQuery('#change_photo').on('click',function(e){
			e.preventDefault();	
			var $this = jQuery(this); 	
			if(jQuery("#change_profile_photo").valid()){
				var form = $('#change_profile_photo')[0];
				var data = new FormData(form);
				jQuery.ajax({				
					url: '<?php echo base_url(); ?>student/students/change_photo',
					type: "POST",
					dataType:"json",
					data: data,
					cache: false,
					contentType: false,
					processData: false,
					success: function(data){
						$('#changePhotoModal').modal('toggle');
						$(".print-error-msg").show();
						if(data.success == true){		
							$('#update_student_details .col-md-4.text-center img').attr('src', data.file_path);
							$('#update_student_details input[name="photo_path"]').val(data.file_path);
							$(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(data.msg);
						}else{
							$(".print-error-msg").removeClass('alert-success').addClass('alert-danger').html(data.error);
						}
					}
				});
			}
		});	
		
		jQuery('.form-check-input').on('change',function(){
			if(jQuery(this).prop("checked") == true){
				var payment_method = jQuery(this).val();

				if (payment_method=='netbanking') {
					//console.log(payment_method);
					var fees_amount = parseFloat(jQuery('#fee_head_total').val());
					var online_charge = 350;
					// var gst_charge = parseFloat((online_charge/100) * 18);
					var gst_charge = 0;
					online_charge = online_charge + gst_charge;
					gst_charge = 0;
					var total_charge = parseFloat(fees_amount+online_charge+gst_charge);
					//console.log(total_charge);
					jQuery('#online_processing_fee').text(online_charge.toFixed(2));
					jQuery('#total_app_amount').text(total_charge.toFixed(2));
					jQuery('#online_amount').val(online_charge.toFixed(2));
					jQuery('#total_amount').val(total_charge.toFixed(2));
				} else {
					//console.log(payment_method);
					var fees_amount = parseFloat(jQuery('#fee_head_total').val());
					var online_charge = parseFloat((fees_amount/100) * 2);
					var gst_charge = parseFloat((online_charge/100) * 18);
					online_charge = online_charge + gst_charge;
					gst_charge = 0;
					var total_charge = parseFloat(fees_amount+online_charge+gst_charge);
					
					//console.log(total_charge);
					jQuery('#online_processing_fee').text(online_charge.toFixed(2));
					jQuery('#total_app_amount').text(total_charge.toFixed(2));
					jQuery('#online_amount').val(online_charge.toFixed(2));
					jQuery('#total_amount').val(total_charge.toFixed(2));
				}
			} 
		})

		$('#cancel_application').on('click', function(){
			$('#addapplicationsModal').modal('hide');
		 });

		jQuery("#application_form").validate({
			rules: 
			{
				application_type:"required",
				application_reason: "required"
			},
			submitHandler: function(form) {
				var form = $('#application_form')[0];
				var data = new FormData(form);
				jQuery.ajax({				
				url: '<?php echo base_url(); ?>student/students/add_application_form',
				type: "POST",
				dataType:"json",
				data: data,
				cache: false,
				contentType: false,
				processData: false,
					success: function(result){
						console.log(result);
						$(".print-error-msg").show();
						if(result.success == true){
							$(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(result.msg);
								setTimeout(function() {
									$('#addapplicationsModal').modal('hide');
									window.location.reload();
								}, 4000);	
						}else{
							jQuery(".print-error-msg").html(result.error);
						}
					}
				}); 
			},
		});
		
		jQuery('#physical_disability').on('change',function(){
			if(jQuery(this).prop("checked") == true){
				jQuery('.row[data-type="' + jQuery(this).attr('id') + '"]').toggle();
			} else {
				jQuery('.row[data-type="' + jQuery(this).attr('id') + '"]').toggle();
			}
		})
		
		jQuery('#marks_obtained, #marks_outof').bind('change', function () {
			var markOb = jQuery('#marks_obtained').val(),
			markOt = jQuery('#marks_outof').val();
			if(markOb !='' && markOt != '' ){
				var result = parseFloat(parseInt(markOb, 10) * 100) / parseInt(markOt, 10);	
				jQuery('#percentage_in_ssc').val(result.toFixed(2));
			}
		});
		jQuery('#marks_obtained, #marks_outof').trigger('change');
		
		$(function() {
		  var options = $('#doc_type option');
			var arr = options.map(function(_, o) { return { t: $(o).text(), v: o.value }; }).get();
			arr.sort(function(o1, o2) { return o1.t > o2.t ? 1 : o1.t < o2.t ? -1 : 0; });
			options.each(function(i, o) {
			  o.value = arr[i].v;
			  $(o).text(arr[i].t);
			});
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
				var academic_year = jQuery('#student_academic_year').val();
				jQuery.ajax({
                    url: 'https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear='+academic_year+'&crn=<?php echo $this->session->userdata('userID');?>&docType='+doc_type,  
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
			online_charge = online_charge + gst_charge;
			gst_charge = 0;
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
			online_charge = online_charge + gst_charge;
			gst_charge = 0;
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

		if(data.sem1percent > 0){
			jQuery('#sem1').attr('data-url',1);
		}else{
			jQuery('#sem1').attr('data-url',0);
			jQuery('#sem1 canvas').hide();
			jQuery('#sem1').append('<div class="no-data-message"> Student yet to appear for examination </div>');
		}
		if(data.sem2percent > 0){
			jQuery('#sem2').attr('data-url',1);
		}else{
			jQuery('#sem2').attr('data-url',0);
			jQuery('#sem2 canvas').hide();
			jQuery('#sem2').append('<div class="no-data-message"> Student yet to appear for examination </div>');
		}
		if(data.sem3percent > 0){
			jQuery('#sem3').attr('data-url',1);
		}else{
			jQuery('#sem3').attr('data-url',0);
			jQuery('#sem3 canvas').hide();
			jQuery('#sem3').append('<div class="no-data-message"> Student yet to appear for examination </div>');
		}
		if(data.sem4percent > 0){
			jQuery('#sem4').attr('data-url',1);
		}else{
			jQuery('#sem4').attr('data-url',0);
			jQuery('#sem4 canvas').hide();
			jQuery('#sem4').append('<div class="no-data-message"> Student yet to appear for examination </div>');
		}
		if(data.sem5percent > 0){
			jQuery('#sem5').attr('data-url',1);
		}else{
			jQuery('#sem5').attr('data-url',0);
			jQuery('#sem5 canvas').hide();
			jQuery('#sem5').append('<div class="no-data-message"> Student yet to appear for examination </div>');
		}
		if(data.sem6percent > 0){
			jQuery('#sem6').attr('data-url',1);
		}else{
			jQuery('#sem6').attr('data-url',0);
			jQuery('#sem6 canvas').hide();
			jQuery('#sem6').append('<div class="no-data-message"> Student yet to appear for examination </div>');
		}
		
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
	
	var sem1_data_url = jQuery('#sem1').attr('data-url'); 

	sem1_url = 'https://vancotech.com/Exams/bachelors/API/api/student/GetSemesterGraphData?semester=1&crn='+crn;

	if(sem1_data_url != 0){
		jQuery.getJSON(sem1_url, function(data){
			var subject = [];
			var marks = [];
			for(var i = 0; i < data.Papers.length; i++){
				subject.push(data.Papers[i].Papertitle);
				marks.push(parseFloat(data.Papers[i].TotalMarksObtained)); 
			} 

			var chartdata = {
				labels: subject,
				datasets: [
					{
						label: 'Semester1 Performance',
						backgroundColor: [
						  "#49e2ff",
						  "#A9A9A9",
						  "#DC143C",
						  "#fbb703",
						  "#2E8B57",
						  "#8b0092",
						],
						borderColor: [
						  "#46d5f1",
						  "#989898",
						  "#fbb703",
						  "#8b0092",
						  "#1D7A46",
						  "#F4A460",
						],
						hoverBackgroundColor: '#CCCCCC',
						hoverBorderColor: '#666666',
						data: marks
					}
				]
			};
			
			var graphTarget = $("#graphcanvas1");

			var barGraph = new Chart(graphTarget, {
				type: 'bar',
				data: chartdata,
				options: {
					responsive: true,
					title: {
						display: true,
						text: 'Semester1 Academic Performance'
					},
					tooltips: {
						callbacks: {
							mode: 'index',
							intersect: false,
							label: function(tooltipItem) {
								return "Marks: " + Number(tooltipItem.yLabel);
							}
						}
					},
					hover: {
						mode: 'nearest',
						intersect: true
					},
					scales: {					
						yAxes: [{
							ticks: {
								min: 0,
								max: 100
							}
						}],
						xAxes: [{
							barThickness : 60
						}]
					}
				}
			});
		});
	}
	
	var sem2_data_url = jQuery('#sem2 .card-body').attr('data-url'); 
		sem2_url = 'https://vancotech.com/Exams/bachelors/API/api/student/GetSemesterGraphData?semester=2&crn='+crn;

	if(sem2_data_url != 0){
		jQuery.getJSON(sem2_url, function(data){
			var subject = [];
			var marks = [];
			for(var i = 0; i < data.Papers.length; i++){
				subject.push(data.Papers[i].Papertitle);
				marks.push(parseFloat(data.Papers[i].TotalMarksObtained)); 
			} 

			var chartdata = {
				labels: subject,
				datasets: [
					{
						label: 'Semester2 Performance',
						backgroundColor: [
						  "#46d5f1",
						  "#989898",
						  "#CB252B",
						  "#E39371",
						  "#1D7A46",
						  "#F4A460",
						],
						borderColor: [
						  "#49e2ff",
						  "#A9A9A9",
						  "#DC143C",
						  "#F4A460",
						  "#2E8B57",
						  "#1D7A46",
						],
						hoverBackgroundColor: '#CCCCCC',
						hoverBorderColor: '#666666',
						data: marks
					}
				]
			};
			
			var graphTarget = $("#graphcanvas2");

			var barGraph = new Chart(graphTarget, {
				type: 'bar',
				data: chartdata,
				options: {
					responsive: true,
					title: {
						display: true,
						text: 'Semester2 Academic Performance'
					},
					tooltips: {
						callbacks: {
							mode: 'index',
							intersect: false,
							label: function(tooltipItem) {
								return "Marks: " + Number(tooltipItem.yLabel);
							}
						}
					},
					hover: {
						mode: 'nearest',
						intersect: true
					},
					scales: {					
						yAxes: [{
							ticks: {
								min: 0,
								max: 100
							}
						}],
						xAxes: [{
							barThickness : 60
						}]
					}
				}
			});
		});
	}
	
	var sem3_data_url = jQuery('#sem3 .card-body').attr('data-url'); 
		sem3_url = 'https://vancotech.com/Exams/bachelors/API/api/student/GetSemesterGraphData?semester=3&crn='+crn;

	if(sem3_data_url != 0){
		jQuery.getJSON(sem3_url, function(data){
			var subject = [];
			var marks = [];
			for(var i = 0; i < data.Papers.length; i++){
				subject.push(data.Papers[i].Papertitle);
				marks.push(parseFloat(data.Papers[i].TotalMarksObtained)); 
			} 

			var chartdata = {
				labels: subject,
				datasets: [
					{
						label: 'Semester3 Performance',
						backgroundColor: [
						  "#49e2ff",
						  "#A9A9A9",
						  "#DC143C",
						  "#F4A460",
						  "#2E8B57",
						  "#1D7A46",
						],
						borderColor: [
						  "#46d5f1",
						  "#989898",
						  "#CB252B",
						  "#E39371",
						  "#1D7A46",
						  "#F4A460",
						],
						hoverBackgroundColor: '#CCCCCC',
						hoverBorderColor: '#666666',
						data: marks
					}
				]
			};
			
			var graphTarget = $("#graphcanvas3");

			var barGraph = new Chart(graphTarget, {
				type: 'bar',
				data: chartdata,
				options: {
					responsive: true,
					title: {
						display: true,
						text: 'Semester3 Academic Performance'
					},
					tooltips: {
						callbacks: {
							mode: 'index',
							intersect: false,
							label: function(tooltipItem) {
								return "Marks: " + Number(tooltipItem.yLabel);
							}
						}
					},
					hover: {
						mode: 'nearest',
						intersect: true
					},
					scales: {					
						yAxes: [{
							ticks: {
								min: 0,
								max: 100
							}
						}],
						xAxes: [{
							barThickness : 60
						}]
					}
				}
			});
		});
	}
	
	var sem4_data_url = jQuery('#sem4 .card-body').attr('data-url'); 
		sem4_url = 'https://vancotech.com/Exams/bachelors/API/api/student/GetSemesterGraphData?semester=4&crn='+crn;

	if(sem4_data_url != 0){
		jQuery.getJSON(sem4_url, function(data){
			var subject = [];
			var marks = [];
			for(var i = 0; i < data.Papers.length; i++){
				subject.push(data.Papers[i].Papertitle);
				marks.push(parseFloat(data.Papers[i].TotalMarksObtained)); 
			} 

			var chartdata = {
				labels: subject,
				datasets: [
					{
						label: 'Semester4 Performance',
						backgroundColor: [
						  "#46d5f1",
						  "#989898",
						  "#CB252B",
						  "#E39371",
						  "#1D7A46",
						  "#F4A460",
						],
						borderColor: [
						  "#49e2ff",
						  "#A9A9A9",
						  "#DC143C",
						  "#F4A460",
						  "#2E8B57",
						  "#1D7A46",
						],
						hoverBackgroundColor: '#CCCCCC',
						hoverBorderColor: '#666666',
						data: marks
					}
				]
			};
			
			var graphTarget = $("#graphcanvas4");

			var barGraph = new Chart(graphTarget, {
				type: 'bar',
				data: chartdata,
				options: {
					responsive: true,
					title: {
						display: true,
						text: 'Semester4 Academic Performance'
					},
					tooltips: {
						callbacks: {
							mode: 'index',
							intersect: false,
							label: function(tooltipItem) {
								return "Marks: " + Number(tooltipItem.yLabel);
							}
						}
					},
					hover: {
						mode: 'nearest',
						intersect: true
					},
					scales: {					
						yAxes: [{
							ticks: {
								min: 0,
								max: 100
							}
						}],
						xAxes: [{
							barThickness : 60
						}]
					}
				}
			});
		});
	}
	
	var sem5_data_url = jQuery('#sem5 .card-body').attr('data-url'); 
		sem5_url = 'https://vancotech.com/Exams/bachelors/API/api/student/GetSemesterGraphData?semester=5&crn='+crn;

	if(sem5_data_url != 0){
		jQuery.getJSON(sem5_url, function(data){
			var subject = [];
			var marks = [];
			for(var i = 0; i < data.Papers.length; i++){
				subject.push(data.Papers[i].Papertitle);
				marks.push(parseFloat(data.Papers[i].TotalMarksObtained)); 
			} 

			var chartdata = {
				labels: subject,
				datasets: [
					{
						label: 'Semester5 Performance',
						backgroundColor: [
						  "#49e2ff",
						  "#A9A9A9",
						  "#DC143C",
						  "#F4A460",
						  "#2E8B57",
						  "#1D7A46",
						],
						borderColor: [
						  "#46d5f1",
						  "#989898",
						  "#CB252B",
						  "#E39371",
						  "#1D7A46",
						  "#F4A460",
						],
						hoverBackgroundColor: '#CCCCCC',
						hoverBorderColor: '#666666',
						data: marks
					}
				]
			};
			
			var graphTarget = $("#graphcanvas5");

			var barGraph = new Chart(graphTarget, {
				type: 'bar',
				data: chartdata,
				options: {
					responsive: true,
					title: {
						display: true,
						text: 'Semester5 Academic Performance'
					},
					tooltips: {
						callbacks: {
							mode: 'index',
							intersect: false,
							label: function(tooltipItem) {
								return "Marks: " + Number(tooltipItem.yLabel);
							}
						}
					},
					hover: {
						mode: 'nearest',
						intersect: true
					},
					scales: {					
						yAxes: [{
							ticks: {
								min: 0,
								max: 100
							}
						}],
						xAxes: [{
							barThickness : 60
						}]
					}
				}
			});
		});
	}
	
	var sem6_data_url = jQuery('#sem6 .card-body').attr('data-url'); 
		sem6_url = 'https://vancotech.com/Exams/bachelors/API/api/student/GetSemesterGraphData?semester=6&crn='+crn;

	if(sem6_data_url != 0){
		jQuery.getJSON(sem6_url, function(data){
			var subject = [];
			var marks = [];
			for(var i = 0; i < data.Papers.length; i++){
				subject.push(data.Papers[i].Papertitle);
				marks.push(parseFloat(data.Papers[i].TotalMarksObtained)); 
			} 

			var chartdata = {
				labels: subject,
				datasets: [
					{
						label: 'Semester6 Performance',
						backgroundColor: [
						  "#46d5f1",
						  "#989898",
						  "#CB252B",
						  "#E39371",
						  "#1D7A46",
						  "#F4A460",
						],
						borderColor: [
						  "#49e2ff",
						  "#A9A9A9",
						  "#DC143C",
						  "#F4A460",
						  "#2E8B57",
						  "#1D7A46",
						],
						hoverBackgroundColor: '#CCCCCC',
						hoverBorderColor: '#666666',
						data: marks
					}
				]
			};
			
			var graphTarget = $("#graphcanvas6");

			var barGraph = new Chart(graphTarget, {
				type: 'bar',
				data: chartdata,
				options: {
					responsive: true,
					title: {
						display: true,
						text: 'Semester6 Academic Performance'
					},
					tooltips: {
						callbacks: {
							mode: 'index',
							intersect: false,
							label: function(tooltipItem) {
								return "Marks: " + Number(tooltipItem.yLabel);
							}
						}
					},
					hover: {
						mode: 'nearest',
						intersect: true
					},
					scales: {					
						yAxes: [{
							ticks: {
								min: 0,
								max: 100
							}
						}],
						xAxes: [{
							barThickness : 60
						}]
					}
				}
			});
		});
	}
	
	
		
	<?php }?>
    </script>
  </body>
</html>