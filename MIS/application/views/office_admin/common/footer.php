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

    <!-- Excel Modal-->
    <div class="modal fade" id="excelModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title" id="exampleModalLabel">Message</h5>
					<button class="close" type="button" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body modal-excel"></div>
				<div class="modal-footer">
					<button class="btn btn-success" type="button" data-dismiss="modal">Done</button>
				</div>
			</div>
		</div>
    </div>
	
	

    <!-- Bootstrap core JavaScript-->


    <script src="<?php echo base_url('assets/vendor/bootstrap/js/bootstrap.bundle.min.js'); ?>"></script>

    <!-- Core plugin JavaScript-->
    <script src="<?php echo base_url('assets/vendor/jquery-easing/jquery.easing.min.js'); ?>"></script>

    <!-- Page level plugin JavaScript-->
    <!-- <script src="<?php //echo base_url('assets/vendor/chart.js/Chart.min.js'); ?>"></script> -->
	 <script src="<?php echo base_url('assets/vendor/datatables/jquery.dataTables.min.js'); ?>"></script>

	<script src="<?php echo base_url('assets/vendor/datatables/dataTables.bootstrap4.min.js'); ?>"></script>
	
	<!-- <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script> -->
	<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
	<!-- <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
	<script type="text/javascript" src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap4.min.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.bootstrap4.min.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.colVis.min.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script> -->
	<!-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script> -->

	<script src="<?php echo base_url('assets/vendor/jquery/jszip.min.js'); ?>"></script>
	<!-- <script src="<?php echo base_url('assets/vendor/jquery/pdfmake.min.js'); ?>"></script> -->
	<script src="<?php echo base_url('assets/vendor/jquery/vfs_fonts.js'); ?>"></script>
	
	<script src="<?php echo base_url('assets/vendor/datatables/dataTables.buttons.min.js'); ?>"></script>
	<script src="<?php echo base_url('assets/vendor/datatables/buttons.bootstrap4.min.js'); ?>"></script>
	<script src="<?php echo base_url('assets/vendor/datatables/buttons.bootstrap4.min.js'); ?>"></script>
	<script src="<?php echo base_url('assets/vendor/datatables/buttons.colVis.min.js'); ?>"></script>
	<script src="<?php echo base_url('assets/vendor/datatables/buttons.flash.min.js'); ?>"></script>
	<script src="<?php echo base_url('assets/vendor/datatables/buttons.html5.min.js'); ?>"></script>
	<script src="<?php echo base_url('assets/vendor/datatables/buttons.print.min.js'); ?>"></script>

	
	<!--<script type="text/javascript" src="<?php echo base_url('assets/js/jquery.canvasjs.min.js') ?>"></script>-->	
	<script src="<?php echo base_url('assets/js/bootstrap-datepicker.js') ?>"></script>
	<script src="<?php echo base_url('assets/js/jquery.validate.min.js') ?>"></script>

    <!-- Custom scripts for all pages-->
    <script src="<?php echo base_url('assets/js/sb-admin.min.js') ?>"></script>


    <!-- Demo scripts for this page-->
    <script src="<?php echo base_url('assets/js/demo/datatables-demo.js') ?>"></script>
	
	<script src="<?php echo base_url('assets/js/jquery.validate.min.js') ?>"></script>
	<script type="text/javascript" src="<?php echo base_url('assets/js/Chart.min.js') ?>"></script>
	<script type="text/javascript" src="<?php echo base_url('assets/js/utils.js') ?>"></script>	
    <script>
		jQuery(document).ready(function(){
			// datepicker
			//jQuery(".datepicker").datepicker();
			//	jQuery(".datepicker").datepicker( "option", "showAnim", 'clip' );
			$('#startdate').on('changeDate', function(ev){
				var startdateS =$('#startdate').val();
				$('#startdateExcel').val(startdateS);
			});
			$('#enddate').on('changeDate', function(ev){
				var enddateS =$('#enddate').val();
				$('#enddateExcel').val(enddateS);
			});

			jQuery( ".datepicker" ).datepicker({
				format: 'yyyy-mm-dd' 
			});
			
			$('.datepicker').on('changeDate', function(ev){
				$(this).datepicker('hide');
			});
			
			jQuery("#date_of_birth").datepicker({
				//yearRange: '1960:2005',
				format: 'dd-mm-yyyy',
				//defaultDate: new Date(2002, 0, 1)
			});
			jQuery(".searchDate").datepicker({
				//yearRange: '1960:2005',
				format: 'yyyy-mm-dd',
				//defaultDate: new Date(2002, 0, 1)
			});
			
			$('#date_of_birth').on('changeDate', function(ev){
				$(this).datepicker('hide');
			});
			
			jQuery('.s_academic_year').datepicker({
				minViewMode: 'years',
				format: 'yyyy',
				autoclose: true,
			});
			
			$('.s_academic_year').on('changeDate', function(ev){
				$(this).datepicker('hide');
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
			
			// search various modes
			jQuery('.search_type').on('click', function(evt){
				evt.stopPropagation();
				evt.preventDefault();
				var search = jQuery(this).attr('id');
				var reg_id = jQuery( "#reg_id" ).val();
				var year = jQuery( "#year" ).val();
				var specialistaion = jQuery( "#specialistaion" ).val();
				jQuery("#search").val(search);
				$.ajax({
					url: '<?php echo base_url(); ?>search/searchStudent',
					type: 'post',
					data: 
					{
						"reg_id": reg_id,
						"year":year,
						"specialistaion": specialistaion,
						"search" : search,
					},
					success: function( data){
						console.log(data);
					},
				});

				jQuery("#search_form").submit();
			});
			
			jQuery('#left_college').bind('change',function(){
				//jQuery('#left_college_date').prop('disabled',true);
				if(jQuery(this).prop("checked") == true){
					var d = new Date();
					var month = d.getMonth()+1;
					var day = d.getDate();
					var output = d.getFullYear() + '-' +
						((''+month).length<2 ? '0' : '') + month + '-' +
						((''+day).length<2 ? '0' : '') + day;
					var left_date = jQuery('#left_college_date').val();
					if(left_date == '0000-00-00'){
						jQuery('#left_college_date').val(output);
					}					
				}
			});
			
			jQuery('#left_college').trigger('change');
			
			jQuery('.text_disabled :input').attr('disabled',true);
			
			jQuery('#edit_data').on('click',function(){
				jQuery(this).hide();
				jQuery('.text_disabled :input').attr('disabled',false);
				jQuery('.text_disabled').removeClass('text_disabled');
				jQuery('.edit_data').show();
			});
			
			
			jQuery.validator.addMethod("accept", function(value, element, param){
				return value.match(new RegExp(param + "$"));
			});
			
			
			jQuery.validator.addMethod("allowspace", function(phone_number, element) {
				phone_number = phone_number.replace(/\\s+/g, ""); 
				return this.optional(element) || phone_number.length > 9 &&
					phone_number.match(/^[0-9 ]+$/);
			}, "Please enter a valid phone number");
				

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
					disability_type:{
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
					mobile_number:{
						required: true,
						allowspace : true
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
					},
					/* city:{
						required: function(element) {
							if (jQuery("#country").val() == 'India') {
								return true;
							} else {
								return false;
							}
						}
					}, */
					pin:{
						required: function(element) {
							if (jQuery("#country").val() == 'India') {
								return true;
							} else {
								return false;
							}
						}
					},
					permanent_pin:{
						required: function(element) {
							if (jQuery("#country").val() == 'India') {
								return true;
							} else {
								return false;
							}
						}, 
						numeric: true
					},
					college_reg:{
						required:true,
						maxlength:6,
						remote: {
							url: "<?php echo base_url(); ?>office_admin/StudentExcelExport/check_reg_exist",
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
					/*pan_number: { accept: "Not a valid PAN Number" },*/
					college_reg:{remote: "This Customer Registration number already exist"},
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
						url: '<?php echo base_url(); ?>office_admin/StudentExcelExport/change_password',
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
						url: '<?php echo base_url(); ?>office_admin/StudentExcelExport/change_photo',
						type: "POST",
						dataType:"json",
						data: data,
						cache: false,
						contentType: false,
						processData: false,
						success: function(data){
							$('#changePhotoModal').modal('toggle');
							$(".print-error-msg").show();
							if(data.success){		
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
			
		$('#cancel_application').on('click', function(){
			$('#viewDetails').modal('hide');
		 });
		jQuery('#add_application').on('click',function(e){
			e.preventDefault();	
			var $this = jQuery(this); 						
			var data = jQuery("#application_form").serialize();
			jQuery.ajax({				
				url: '<?php echo base_url(); ?>office_admin/StudentExcelExport/update_application_form',
				type: "POST",
				dataType:"json",
				data:data,
				success: function(result){
					$(".print-error-msg").show();
					if(result.success == true){
						$(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(result.msg);
						//jQuery(".print-error-msg").html(result.msg);
							setTimeout(function() {
								$('#viewDetails').modal('hide');
								window.location.reload();
							}, 4000);	
					}else{
						jQuery(".print-error-msg").html(result.error);
					}
				}
			});
		});	
		
		jQuery('#import_update_form').on('submit', function(event){
  			event.preventDefault();
  			jQuery.ajax({
  				url: "<?php echo base_url(); ?>update_user_excel",
  				method: "POST",
  				data: new FormData(this),
  				contentType: false,
  				cache: false,
  				processData: false,
  				beforeSend: function(){
  					// Show image container
  					jQuery("div#divLoading").addClass('show');
  				},
  				success:function(data){					
  					jQuery('#file').val('');
  					jQuery("div#divLoading").removeClass('show');
  					jQuery('#msg').addClass('alert alert-success').html(data);
  					//jQuery("#excel-btn").click();
  					//jQuery(".modal-excel").text(data);
  				}
  			});
  		});

		jQuery("#userID").keyup(function(){
			var userID = jQuery(this).val();
			/*if(!userID){
				return false;
			}*/
			//var spec = jQuery('#spec_shortform').val();
			if(jQuery(this).val().length > 3) {
				//alert(userID);
				jQuery.ajax({
	  				url: "<?php echo base_url(); ?>office_admin/office_admin/get_student_courses",
	  				method: "POST",
					data : {userID:userID},
					dataType:"json",
	  				/*beforeSend: function(){
	  					// Show image container
	  					jQuery("div#divLoading").addClass('show');
	  				},*/
	  				success:function(data){		
	  					//console.log(data);			;
	  					//console.log(data.course_details[0].short_form)			;
	  					jQuery('#spec_shortform').val(data.course_details[0].short_form);
	  				}
	  			});
			}
		});

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
							items.push('<option value="' + val1.paperCode + '" data-credit="'+val1.credits+'" data-subject_name="'+val1.paperTitle+'">' + val1.paperTitle + ' ('+ val1.credits+ ')</option>');
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
			var subject_name = jQuery('#subject option:selected').attr('data-subject_name');
			if(!credit){
				return false;
			}
			jQuery('#sub_credits').val(credit);
			jQuery('#subject_name').val(subject_name);
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
		});

			jQuery('#add_staff').validate({
				rules: {
					member_name:{
						required: true
					},
					employee_code: "required",
					type: "required",
					email:{
						required: true,
						email: true
					}/* ,
					pan:{
						accept: "^[0-9A-Z]+",
					},
					aadhaar:{
						required: true,
						number: true,
						maxlength: 12,
						minlength: 12
					},
					account_number:{
						required: true
					},
					bank_name:{
						required: true
					},
					ifsc_code:{
						required: true
					},
					account_holder_name:{
						required: true
					} */
				}
			});
			
			jQuery('#add_member').on('click',function(e){
				e.preventDefault();	
				var $this = jQuery(this); 	
				if(jQuery("#add_staff").valid()){
					var data1 = jQuery("#add_staff").serialize();
					jQuery.ajax({				
						url: '<?php echo base_url(); ?>office_admin/StaffExcelExport/add_staff_member',
						type: "POST",
						dataType:"json",
						data: data1,
						success: function(data){
							if(data.success == true){
								$(".print-error-msg").html(data.msg);
								setTimeout(function() {
									$('#addstaffModal').modal('hide');
									window.location.reload();
								}, 4000);	
							}else{
								$(".print-error-msg").html(data.msg);
							}
						}
					});
				}
			});
			
			jQuery('#remove_rollNo').on('click',function(e){
				e.preventDefault();	
				var $this = jQuery(this); 	
				var data1 = jQuery("#download_courseexceldf").serialize();
				jQuery.ajax({				
					url: '<?php echo base_url(); ?>office_admin/StudentExcelExport/clean_rollnumber',
					type: "POST",
					dataType:"json",
					data: data1,
					success: function(data){
						//console.log(data);
						jQuery(".print-error-msg").show();
						if(data.success == true){
							$(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(data.msg);
							setTimeout(function() {
								$(".print-error-msg").hide('slow');
								//$('#downloadExcelModal').modal('hide');
							}, 4000);	
						}else{
							$(".print-error-msg").removeClass('alert-success').addClass('alert-danger').html(data.msg);
						}
					}
				});
			});

			$(function() {
			  var options = $('#doc_type option');
				var arr = options.map(function(_, o) { return { t: $(o).text(), v: o.value }; }).get();
				arr.sort(function(o1, o2) { return o1.t > o2.t ? 1 : o1.t < o2.t ? -1 : 0; });
				options.each(function(i, o) {
				  o.value = arr[i].v;
				  $(o).text(arr[i].t);
				});
			});	
			
			jQuery('#physical_disability').on('change',function(){
				if(jQuery(this).prop("checked") == true){
					jQuery('.row[data-type="' + jQuery(this).attr('id') + '"]').toggle();
				} else {
					jQuery('.row[data-type="' + jQuery(this).attr('id') + '"]').toggle();
				}
			})
			
			jQuery('.category').bind('change',function(){				
				if(jQuery(".category:checked").val() == 'Reserved'){
					jQuery('.show_caste').show();
				} else {
					jQuery('.show_caste').hide();
					jQuery('#caste').val('');
					jQuery('#sub_caste').val('');
				}
			});		
			
			jQuery('.category').trigger('change');
			
			jQuery('#country').on('change',function(){
				if(jQuery(this).val() != 'India'){
					jQuery('.cors_address').hide();
				} else {
					jQuery('.cors_address').show();
				}
			});	
			jQuery('#permanent_country').on('change',function(){
				if(jQuery(this).val() != 'India'){
					jQuery('.per_address').hide();
				} else {
					jQuery('.per_address').show();
				}
			});

			jQuery('#marks_obtained, #marks_outof').bind('change', function () {
				var markOb = jQuery('#marks_obtained').val(),
				markOt = jQuery('#marks_outof').val();
				if(markOb !='' && markOt != '' ){
					var result = parseFloat(parseInt(markOb, 10) * 100) / parseInt(markOt, 10);	
					jQuery('#percentage_in_ssc').val(result.toFixed(2));
				}
			});
			jQuery('#marks_obtained, #marks_outof').trigger('change');
			
			jQuery('#course_name, #specialization').bind('change', function () {
				var spec = jQuery('#specialization').val(),
				course = jQuery('#course_name').val();
				if(course){
					courseArr = course.split("-");
					var course_name = courseArr[1];
					var year = courseArr[0];
					if (course_name == null){
						course_name = '';
					}
					jQuery.ajax({
						url: '<?php echo base_url(); ?>office_admin/StudentExcelExport/get_course_id',
						type: "POST",
						dataType:"json",
						data:{year:year,specialization:spec,course_name:course_name},
						success: function(data){
							if(data.course_id){								
								jQuery("#courseId").val(data.course_id);
							}
						}
					});
				}
			});	

			//student json
			jQuery.ajax({			
				url: '<?php echo base_url(); ?>api/students/action/view_spec',
				type: "GET",
				success: function(data){
					drawTable(data,'#specializationTable');
				}
			});	
			
			//caste json
  			jQuery.ajax({
				url: '<?php echo base_url(); ?>api/students_caste',
				type: "GET",
				success: function(data){
					drawTable(data,'#casteTable');
				}
			});	

			
			jQuery('#filter_students select,#filter_students input').on('change',function(){
				var s_caste = jQuery('#s_caste').val(),
					s_religion = jQuery('#s_religion').val(),
					s_state = jQuery('#s_state').val(),
					s_academic_year = jQuery('#s_academic_year').val(),
					s_course_name = jQuery('#s_course_name').val(),
					s_specialization = jQuery('#s_specialization').val();
				jQuery('.export_excel_file input[name="s_caste"]').val(s_caste);
				jQuery('.export_excel_file input[name="s_religion"]').val(s_religion);
				jQuery('.export_excel_file input[name="s_state"]').val(s_state);
				jQuery('.export_excel_file input[name="s_academic_year"]').val(s_academic_year);
				jQuery('.export_excel_file input[name="s_course_name"]').val(s_course_name);
				jQuery('.export_excel_file input[name="s_specialization"]').val(s_specialization);
			});
			
			
			jQuery('#student_tab_list a').on('click', function (e) {
				e.preventDefault();
				jQuery(this).tab('show');
			});	
			jQuery('#feedback_tab_list a').on('click', function (e) {
				e.preventDefault();
				jQuery(this).tab('show');
			});	

			jQuery('#payment_method').on('change',function(){
				jQuery('.cheque_data').hide();
				if(jQuery(this).val() != 'Cash'){
					jQuery('.cheque_data').show();
				}
			});

			jQuery('.change_status').on('change',function(){
				if (confirm("Are you sure you want to update status?")) {
					var datastring = jQuery(this).parent('.change_status_form').serialize();
					jQuery.ajax({
						url: '<?php echo base_url();?>update_transaction_status',
						type: 'post',
						data: datastring,
						dataType:"json",
						success: function( data){
							console.log(data.success);
							jQuery(".print-error-msg").show();
							if(data.success){								
								jQuery(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(data.success);
								setTimeout(function(){
									location.reload();
								},1000);
							}else{
								jQuery(".print-error-msg").removeClass('alert-success').addClass('alert-danger').html(data.error);
							}							
						},
					});
				}
				return false;				
			})	

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
					var student_id = jQuery('#student_id').val();
					var student_academic_year = jQuery('#student_academic_year').val();
					if(student_id == null){
						return;
					}
					jQuery.ajax({
						url: 'https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear='+student_academic_year+'&crn='+student_id+'&docType='+doc_type,  
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
							jQuery('#upload_new_doc')[0].reset();
							if(response){
								jQuery(".print-error-msg").removeClass('alert-danger').addClass('alert-success').html(response);
							}
						}
					});
				}
			});	

		/* $('#addstaffModal').on('click', function(){
			//$('#add_staff').reset();
			$('#add_staff :input').val('');
		});		

		$('#addstaffModal button.close').on('click', function(){
			//$('#add_staff').reset();
			$('#add_staff :input').val('');
		});	 */
			
		jQuery('#member_name').on('change',function(){
			var first_name = jQuery(this).val();
			var new_fname = first_name.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '').toLowerCase();
			var last_name = jQuery('#member_lname').val();
			if(last_name){
				var new_lname = last_name.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '').toLowerCase();
				jQuery('#username').val(new_fname+'.'+new_lname);
			}else{
				jQuery('#username').val(new_fname);
			}
		});
		
		jQuery('#member_lname').on('change',function(){
			var last_name = jQuery(this).val();
			var new_lname = last_name.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '').toLowerCase();
			var first_name = jQuery('#member_name').val();
			if(first_name){
				var new_fname = first_name.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '').toLowerCase();
				jQuery('#username').val(new_fname+'.'+new_lname);
			}else{
				jQuery('#username').val(new_lname);
			}
		});
		});
		
		jQuery(function(){
			var url = document.location.toString();
			if (url.match('#')) {
				jQuery('.nav-tabs a[href="#' + url.split('#')[1] + '"]').tab('show');
			}
			jQuery('.nav-tabs a').on('shown.bs.tab', function (e) {
				window.location.hash = e.target.hash;
			})
		})
		
		function drawTable(data,id) {
			for (var i = 0; i < data.length; i++) {
				drawRow(data[i],id);
			}
		}
		function drawRow(rowData,id) {
			var row = jQuery("<tr />");
			jQuery(id).append(row);
			if(rowData.title != 'Total'){
				row.append(jQuery("<td class='title_heading'><strong>" + rowData.title + "</strong></td>"));
				row.append(jQuery("<td>" + rowData.fy + "</td>"));
				row.append(jQuery("<td>" + rowData.sy + "</td>"));
				row.append(jQuery("<td>" + rowData.ty + "</td>"));
				row.append(jQuery("<td>" + rowData.ts + "</td>"));
			} else {
				row.append(jQuery("<td><strong>" + rowData.title + "</strong></td>"));
				row.append(jQuery("<td><strong>" + rowData.total_fy + "</strong></td>"));
				row.append(jQuery("<td><strong>" + rowData.total_sy + "</strong></td>"));
				row.append(jQuery("<td><strong>" + rowData.total_ty + "</strong></td>"));
				row.append(jQuery("<td><strong>" + rowData.total_ts + "</strong></td>"));
			}
		}
	<?php if(!empty($student['id'])){?>
	var ssc = '<?php echo $student['percentage_in_ssc'];?>',
		 marks_obtained = '<?php echo $student['marks_obtained'];?>',
		 marks_outof = '<?php echo $student['marks_outof'];?>',
		 crn = '<?php echo $student['userID'];?>',
		 grad = '';			
	var hsc = parseFloat(parseInt(marks_obtained, 10) * 100) / parseInt(marks_outof, 10);	
	hsc = hsc.toFixed(2);
	console.log("https://vancotech.com/Exams/bachelors/API/api/student/GetSummaryGraphData?ssc="+ssc+"&hsc="+hsc+"&grad="+grad+"&crn="+crn);
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