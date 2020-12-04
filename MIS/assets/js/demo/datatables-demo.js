// Call the dataTables jQuery plugin
$(document).ready(function() {
	$('#dataTable').DataTable({
		"order": [ 0, "desc" ]
	});
	var dataTableAttendanceD = $('#dataTableAttendanceD').DataTable({
		/*fixedHeader: {
            header: true,
            dom: 'lrtip'
        }*/
        dom: 'lrtip',
        fixedHeader: true,
        "pageLength": 50
	});
	dataTableAttendanceD.search('ABSENT').draw();  
	$('#table-filter').on('change', function(){
       dataTableAttendanceD.search(this.value).draw();   
    });
    
	var pendingLeaveDataTable = $('#pendingLeaveDataTable').DataTable( {
        fixedHeader: {
            header: true,
            footer: true
        
        },
        "pageLength": 50
    } );

	//$('#dataTableStaff').DataTable();
	
	$('#reportingDatatable').DataTable({
		//"order": [ 0, "desc" ]
	});
	
	var dataTableFeedbackD = $('#dataTableFeedbackD').DataTable({
        dom: 'lfrtip',
        fixedHeader: true,
        "pageLength": 50
	});

	var datatable1 = $('#dataTable1').DataTable({
		dom: 'Bfrtip',
		'processing': true,
		'serverSide': true,
		'serverMethod': 'post',
		 "paginationType": "full_numbers",
		'ajax': {
			'url' : $('#filter_students').attr('action'),
			'data': function(data){
				var caste = $('#s_caste').val();
				var religion = $('#s_religion').val();
				var state = $('#s_state').val();
				var acad = $('#s_academic_year').val();
				var course_year = $('#s_course_name').val();				
				var spec = $('#s_specialization').val();
				// Append to data
				data.searchByCaste = caste;
				data.searchByReligion = religion;
				data.searchByState = state;
				data.searchByAcademic = acad;
				data.searchByCourseYear = course_year;
				data.searchBySpec = spec;
			}
		},
		'columns': [
			{ data: 's_no' },
			{ data: 'userID' },
			{ data: 'roll_number' },
			{ data: 'course_name' },
			{ data: 'short_form' },
			{ data: 'full_name' },
			{ data: 'caste' },
			{ data: 'religion' },
			{ data: 'blood_group' },
			{ data: 'mobile_number' },
			{ data: 'permanent_state' },
			{ data: 'physical_disability' },
			{ data: 'academic_year' },
			{ data: 'percentage' },
		],
		dom: "<'row'<'col-sm-3'l><'col-sm-3'f><'col-sm-6'p>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-5'i><'col-sm-7'p>>",
		"columnDefs": [
            {
                "targets": [12],
                "visible": false,
                //"searchable": false
            },
			{ 
				"width": "100px", 
				"targets": 5 
			}
        ],		
		"pageLength": 100,
		"order": [ 2, "ASC" ],
		//"deferRender": true
	});	
	
	var datatable2 = $('#dataTable2').DataTable({
		dom: 'Bfrtip',
		'processing': true,
		'serverSide': true,
		'serverMethod': 'post',
		 "paginationType": "full_numbers",
		'ajax': {
			'url' : $('#filter_students_certificate').attr('action'),
			'data': function(data){
				//var caste = $('#s_caste').val();
				//var religion = $('#s_religion').val();
				//var state = $('#s_state').val();
				var acad = $('#s_academic_year').val();
				var course_year = $('#s_course_name').val();				
				var spec = $('#s_specialization').val();
				// Append to data
				//data.searchByCaste = caste;
				//data.searchByReligion = religion;
				//data.searchByState = state;
				data.searchByAcademic = acad;
				data.searchByCourseYear = course_year;
				data.searchBySpec = spec;
			}
		},
		'columns': [
			{ data: 's_no' },
			{ data: 'userID' },
			{ data: 'roll_number' },
			{ data: 'course_name' },
			{ data: 'short_form' },
			{ data: 'full_name' },
			{ data: 'mobile_number' }
		],
		dom: "<'row'<'col-sm-3'l><'col-sm-3'f><'col-sm-6'p>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-5'i><'col-sm-7'p>>",
		"columnDefs": [
        { 
            "targets": [ 0 ], //first column / numbering column
        },
        ],		
		"pageLength": 100,
		"order": [ 2, "ASC" ],
	});	
	
	var dataTableStaff = $('#dataTableStaff').DataTable({
		dom: 'Bfrtip',
		'processing': true,
		'serverSide': true,
		'serverMethod': 'post',
		 "paginationType": "full_numbers",
		'ajax': {
			'url' : $('#filter_staff').attr('action'),
			'data': function(data){
				var type = $('#s_type').val();
				var state = $('#s_state').val();
				var degree = $('#s_degree').val();
				// Append to data
				data.searchByType = type;
				data.searchByState = state;
				data.searchByDegree = degree;

			}
		},
		'columns': [
			{ data: 's_no' },
			{ data: 'employee_code' },
			{ data: 'firstname' },
			{ data: 'lastname' },
			{ data: 'username' },
			{ data: 'type' },
			{ data: 'department' },
			{ data: 'email' },
			{ data: 'state' },
			{ data: 'mobile_number' },
			{ data: 'date_of_joining' },
			{ data: 'date_of_retire' },
			{ data: 'qualification' },
			{ data: 'role' },
			{ data: 'pan_number' },
			{ data: 'aadhaar_number' },
			{ data: 'bank_account_number' },
			{ data: 'bank_name' },
			{ data: 'ifsc_code' },
			{ data: 'account_holder_name' },
			{ data: 'employee_leave' },
		],
		dom: "<'row'<'col-sm-3'l><'col-sm-3'f><'col-sm-6'p>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-5'i><'col-sm-7'p>>",
		"columnDefs": [
        { 
            "targets": [ 0 ], //first column / numbering column
        },
        ],		
		"pageLength": 100,
		"order": [ 2, "ASC" ],
		//"deferRender": true
	}); 	
	
	
	var dataTableLeaveRequest = $('#dataTableLeaveRequest').DataTable({
		dom: 'Bfrtip',
		'processing': true,
		'serverSide': true,
		'serverMethod': 'post',
		 "paginationType": "full_numbers",
		'ajax': {
			'url' : $('#filter_leave').attr('action'),
			'data': function(data){
				var status = $('#s_status').val();
				// Append to data
				data.searchByStatus = status;

			}
		},
		'columns': [
			{ data: 's_no' },
			{ data: 'staff_id' },
			{ data: 'leave_from' },
			{ data: 'leave_to' },
			{ data: 'hod_approval_status' },
			{ data: 'os_approval_status' },
			{ data: 'principal_approval_status' },
			{ data: 'status' },
			{ data: 'view_details' },
			{ data: 'date_applied' },
			{ data: 'action' },
		],
		dom: "<'row'<'col-sm-3'l><'col-sm-3'f><'col-sm-6'p>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-5'i><'col-sm-7'p>>",
		"columnDefs": [
        { 
            "targets": [ 0 ], //first column / numbering column
        },
        ],		
		"pageLength": 100,
		"order": [ 2, "ASC" ],
		//"deferRender": true
	}); 	
	
	var dataTableLeaveRequestHead = $('#dataTableLeaveRequestHead').DataTable({
		dom: 'Bfrtip',
		'processing': true,
		'serverSide': true,
		'serverMethod': 'post',
		 "paginationType": "full_numbers",
		'ajax': {
			'url' : $('#filter_leave_head').attr('action'),
			'data': function(data){
				var status = $('#s_status').val();
				// Append to data
				data.searchByStatus = status;

			}
		},
		'columns': [
			{ data: 's_no' },
			{ data: 'staff_id' },
			{ data: 'leave_from' },
			{ data: 'leave_to' },
			{ data: 'status' },
			{ data: 'view_details' },
			{ data: 'approve_reject' },
			{ data: 'date_applied' },
		],
		dom: "<'row'<'col-sm-3'l><'col-sm-3'f><'col-sm-6'p>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-5'i><'col-sm-7'p>>",
		"columnDefs": [
        { 
            "targets": [ 0 ], //first column / numbering column
        },
        ],		
		"pageLength": 100,
		"order": [ 2, "ASC" ],
		//"deferRender": true
	}); 
	
	$('.reset_filter_search').on('click',function(){		
		setTimeout(function(){
			$('#search_student').trigger('click');	
			$('#export_excel_file input[name="s_caste"],#export_excel_file input[name="s_religion"],#export_excel_file input[name="s_state"],#export_excel_file input[name="s_course_name"],#export_excel_file input[name="s_specialization"]').val("");
		},1000);
	})
	
	/* $("#ExportReporttoExcel").on("click", function() {
		datatable1.button( '.buttons-excel' ).trigger();
	}); */
		
	$('#search_student').on('click',function(e) {
		e.preventDefault();
		$('.filter_students .processing_loader').show();
		datatable1.draw();
		setTimeout(function(){
			$('.filter_students .processing_loader').hide();
		},1000);
    });	
	
	$('#search_staff').on('click',function(e) {
		e.preventDefault();
		$('.filter_staffs .processing_loader').show();
		dataTableStaff.draw();
		setTimeout(function(){
			$('.filter_staffs .processing_loader').hide();
		},1000);
    });
	
	$('#filter_students_certificate').on('click',function(e) {
		e.preventDefault();
		$('.filter_students .processing_loader').show();
		datatable2.draw();
		setTimeout(function(){
			$('.filter_students .processing_loader').hide();
		},1000);
    });	

	$('#search_request').on('click',function(e) {
		e.preventDefault();
		$('.filter_requests .processing_loader').show();
		dataTableLeaveRequest.draw();
		setTimeout(function(){
			$('.filter_requests .processing_loader').hide();
		},1000);
    });
	
	$('#search_request_head').on('click',function(e) {
		e.preventDefault();
		$('.filter_leaves_head .processing_loader').show();
		dataTableLeaveRequestHead.draw();
		setTimeout(function(){
			$('.filter_leaves_head .processing_loader').hide();
		},1000);
    });
});
