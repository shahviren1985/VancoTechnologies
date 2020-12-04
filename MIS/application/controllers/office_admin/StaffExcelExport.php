<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class StaffExcelExport extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
        $this->load->model('office_admin/StaffExcelExport_model');
		$this->load->model('student/account_history_model');
	}

	public function index() {	
	    //$data['result'] = $this->StudentExcelExport_model->fetch_data();
		$data['courseList'] = $this->StaffExcelExport_model->fetch_course_name();
		$data['specialization'] = $this->StaffExcelExport_model->fetch_specialization();
		$data['staff_data'] = $this->StaffExcelExport_model->get_staff_details();
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/staff_export_excel',$data);
		$this->load->view('office_admin/common/footer');
	}
	
	
	public function add_staff_member(){
		$this->load->helper('form');
        $this->load->library('form_validation');
		$employee_data = $this->StaffExcelExport_model->get_staff_details();
		$this->form_validation->set_rules('member_name', 'First Name', 'required');
		$this->form_validation->set_rules('member_lname', 'Last Name', 'required');
		$this->form_validation->set_rules('employee_code', 'Employee Code', 'trim|required');
		$this->form_validation->set_rules('type', 'Type', 'required');
		$this->form_validation->set_rules('email', 'Email Id', 'required');
		$this->form_validation->set_rules('mobile_num', 'Mobile Number', 'trim|required|numeric');
		if ($this->form_validation->run() === FALSE) {
            $this->load->view('office_admin/common/header');
            $this->load->view('office_admin/staff_export_excel');
            $this->load->view('office_admin/common/footer');
        }else{
			$cstring = '';
			$connectionString = $this->session->userdata("connectionString");
			$cstring = SelectDB($connectionString);
			$password = 123;
			$encrypted_pwd = password_hash($password, PASSWORD_BCRYPT);
			
			$employee_code = $_POST['employee_code'];
			$employee_id = array();
			foreach($employee_data as $employee){
				$employee_id[] = $employee->employee_code;
			}
			if(empty($employee_id)){
				$result = $this->StaffExcelExport_model->add();
				if($result){
					$data = array(
						//'id' => '',
						'userID' => $_POST['username'],
						'password' => $encrypted_pwd,
						'connectionString' => $cstring,
						'role' => 4
					);
					$data = $this->security->xss_clean($data);
					$this->StaffExcelExport_model->insert_login_details($data);
					echo json_encode(['success'=> true, 'msg' => 'Staff Member Successfully Added']);
					$this->session->set_flashdata('msg', 'Staff Member Successfully Added');
				}else{
					echo json_encode(['success'=> false, 'msg' => 'Error occured! Please fill up the form carefully.']);
					$this->session->set_flashdata('msg', 'Error occured! Please fill up the form carefully.');
				} 
			}elseif(in_array($employee_code, $employee_id)){
				echo json_encode(['success'=> false, 'msg' => 'Employee code already exists.']);
			}else{
				$result = $this->StaffExcelExport_model->add();
				if($result){
					$data = array(
					//'id' => '',
					'userID' => $_POST['username'],
					'password' => $encrypted_pwd,
					'connectionString' => $cstring,
					'role' => 4
					);
					$data = $this->security->xss_clean($data);
					$this->StaffExcelExport_model->insert_login_details($data);
					echo json_encode(['success'=> true, 'msg' => 'Staff Member Successfully Added']);
					$this->session->set_flashdata('msg', 'Staff Member Successfully Added');
				}else{
					echo json_encode(['success'=> false, 'msg' => 'Error occured! Please fill up the form carefully.']);
					$this->session->set_flashdata('msg', 'Error occured! Please fill up the form carefully.');
				} 
			}
        }
	}
	
		
	public function searchStaff() {
		$records = $this->StaffExcelExport_model->fetch_data();
		$draw = $this->input->post('draw');
		## Fetch records
		$data = array();
		$count = $_POST['start'];
		$n = 1;
		foreach($records as $record){
			$count++;
			
			if(!empty($record->date_of_joining) && $record->date_of_joining != '0000-00-00'){
				$date_of_joining =  date('d-m-Y',strtotime($record->date_of_joining));
			}else{
				$date_of_joining = "";
			}
			
			if(!empty($record->date_of_retire) && $record->date_of_retire != '0000-00-00'){
				$date_of_retire =  date('d-m-Y',strtotime($record->date_of_retire));
			}else{
				$date_of_retire = "";
			}
			if ($record->type=='Permanent Non Teaching' || $record->type=='Permanent Teaching') {
				$employee_leave = '<a href="'.site_url('staff_annual_leave_export/'.$record->employee_code).'">Download</a>';				
			}else {
				$employee_leave='';
			}
			//$course_type = (empty($record->course_type)) ? 'Regular': 'Honors';
			//$course_name = $record->year . '-'.$course_type;
			$data[] = array(
				"s_no"=>$n,
				"employee_code"=>'<a href="'.site_url('officeadmin/stafflist/staffdetails/edit/'.$record->id).'">'.$record->employee_code.'</a>',
				"firstname"=>$record->firstname,
				"lastname"=>$record->lastname,
				"username"=>$record->username,
				"type"=>$record->type,
				"department"=>$record->department,
				"email"=> $record->email,
				"state" => $record->state,
				"mobile_number"=>@$record->mobile_number,
				"date_of_joining"=>@$date_of_joining,
				"date_of_retire"=>@$date_of_retire,
				"qualification"=>@$record->qualification,
				"role"=>@$record->role,
				"pan_number" => @$record->pan_number,
				"aadhaar_number" => @$record->aadhaar_number,
				"bank_account_number" => @$record->bank_account_number,
				"bank_name" => @$record->bank_name,
				"ifsc_code" => @$record->ifsc_code,
				"account_holder_name" => @$record->account_holder_name,
				"employee_leave" => @$employee_leave,
			);
			$n++;
		}

		## Response
		$response = array(
			"draw" => intval($draw),
			"recordsTotal" => $this->StaffExcelExport_model->count_all(),
			"recordsFiltered" => $this->StaffExcelExport_model->count_filtered(),
			"aaData" => $data
		);
		echo json_encode($response);
	}
	
	public function edit($id) {
		if (empty($id)) {
			show_404();
        }
        $this->load->helper('form');
        $this->load->library('form_validation');
        //$data['courseList'] = $this->StaffExcelExport_model->fetch_course_name();
		//$data['specialization'] = $this->StaffExcelExport_model->fetch_specialization();
		$data['staff'] = $this->StaffExcelExport_model->get_staff_by_id($id);
		$userID = $data['staff']['employee_code'];
		//$data['transaction_details'] = $this->account_history_model->get_transaction($userID);
		//$data['cur_course'] = $this->StaffExcelExport_model->popuplate_course_data($data['student']['course_id']);
       	$this->form_validation->set_rules('member_name', 'First Name', 'required');
       	$this->form_validation->set_rules('member_lname', 'First Name', 'required');
		$this->form_validation->set_rules('employee_code', 'Employee Code', 'trim|required');
		$this->form_validation->set_rules('type', 'Type', 'required');
		$this->form_validation->set_rules('email', 'Email Id', 'required');
		$this->form_validation->set_rules('mobile_number', 'Mobile Number', 'trim|required|numeric');
		
		if ($this->form_validation->run() === FALSE) {
            $this->load->view('office_admin/common/header');
            $this->load->view('office_admin/edit_staff',$data);
            $this->load->view('office_admin/common/footer');
        } else {
            $this->StaffExcelExport_model->update($id);
			$this->session->set_flashdata('msg', 'Member successfully Updated');
			//redirect('controller_name/addRoom');
			redirect(site_url('officeadmin/export-staff-list'));
        }
    }
    
    public function staff_leave_list()
    {
		$this->load->model('staff/staff');
		/* Old created for showing all staff list with leave
		$data['staff_data'] = $this->StaffExcelExport_model->get_staff_details();
		$data['leave_request'] = $this->StaffExcelExport_model->get_all_leave_detail_for_export();
		*/

		/* New created for list and Excel */
		$data['leave_request'] = $this->StaffExcelExport_model->get_all_leave_detail_for_list();
		
		if (isset($_POST['submit'])) {
			if ($_POST['startdate']!="" && $_POST['enddate']!="") {
				$startDate = $_POST['startdate'];
				$endDate = $_POST['enddate'];
				$data['startDate'] = $startDate;
				$data['endDate'] = $endDate;
				//echo "<pre>"; print_r($_POST); die;
				$data['leave_request'] = $this->StaffExcelExport_model->get_all_leave_detail_for_list($startDate,$endDate);
			} else {
				$this->session->set_flashdata('msg', 'Please select both Dates for search...');
				redirect(site_url('officeadmin/staff-leave-list'));
			}
		}
		// echo "<pre>"; print_r($data); die;
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/staff_leave_list',$data);
		$this->load->view('office_admin/common/footer');
    }

    public function staff_leave_list_export() {	
		$startDate = $this->input->post('startdateExcel');
		$endDate = $this->input->post('enddateExcel');
		if(!empty($startDate) && !empty($endDate)){
			$month = date("F-Y", strtotime($startDate));
			$fileName = "Staff-Leave-$month.xlsx";
		}else{
			$month = date('F-Y');
			$fileName = "Staff-Leave-$month.xlsx";
		}
		
		// load excel library
        $this->load->library('excel');
       /* $staff_data = $this->StaffExcelExport_model->get_staff_details_for_leave_excel();
		$leave_request = $this->StaffExcelExport_model->get_all_leave_detail_for_export();*/
		$leave_request = $this->StaffExcelExport_model->get_all_leave_detail_for_exportE();

		$emptyArray = $newArray= array();
		foreach ($leave_request AS $key => $line ) { 
		    if ( !in_array($line->staff_id, $emptyArray) ) { 
		        $emptyArray[] = $line->staff_id; 
		        $newArray[] = $line; 
		    } 
		}

        // echo "<pre>"; 
        //print_r($_POST);
        // print_r($leave_request);
        //die;
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);
		// set Header
		$row = 1;
		$removeCols = array('staff_id','leave_type','leave_from','leave_to');	
		foreach($newArray as $header_data ){
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){
					if(!in_array($key,$removeCols)){
						if($key == 'employee_code'){
							$key = 'Employee Code';
						}
						if($key == 'firstname'){
							$key = 'First Name';
						}
						if($key == 'lastname'){
							$key = 'Last Name';
						}
						if($key == 'type'){
							$key = 'Staff Member Type';
						}
						$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
						$col++;
					}					
				}
			}
			$row++;
		}		
        // set Row        
		$rowCount = 2;
		foreach($newArray as $row_data ){
			$col = 0;
			$totalcount = $slCount= $clCount= $plCount = 0;
			$str = "";
			$leaveDetail =array();
			foreach($row_data as $key=>$value) {
				if(!in_array($key,$removeCols)){
					foreach ($leave_request as $val) {
						if ($val->staff_id==$value) {
							$leaveDetail[]=$val;
							$totalcount++;
							if ($val->leave_type=="Sick-Leave") {
								$slCount++;
							}
							if ($val->leave_type=="Casual-Leave") {
								$clCount++;
							}
							if ($val->leave_type=="Paid-Leave") {
								$plCount++;
							}
						}
					}
					if($key == 'SL'){
						$value = $slCount;
					}
					if($key == 'CL'){
						$value = $clCount;
					}
					if($key == 'PL'){
						$value = $plCount;
					}
					if($key == 'Total Leave Taken'){
						$value = $totalcount;
					}
					if($key == 'Leave Details'){
						foreach ($leaveDetail as $keys => $vals) {
							$str .= $vals->leave_type."-".date("d-m-Y", strtotime($vals->leave_from))." to ".date("d-m-Y", strtotime($vals->leave_to)).",";
							// $str .= $vals->leave_type."-".$vals->leave_from.",".$vals->leave_type."-".$vals->leave_to.",";
						}
						$str = trim($str, ',');
						$value = $str;
					}
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
					$col++;
				}
			}
			$rowCount++;
		}
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
        $objWriter->save($fileName);
		// download file
        header("Content-Type: application/vnd.ms-excel");
        redirect($fileName);
	}
	
	public function staff_annual_leave_export() {	
		$employee_code = $this->uri->segment(2);
		$fileName = "Leave-Report-$employee_code.xlsx";
		// load excel library
        $this->load->library('excel');
       /* $staff_data = $this->StaffExcelExport_model->get_staff_details_for_leave_excel();
		$leave_request = $this->StaffExcelExport_model->get_all_leave_detail_for_export();*/
		$leave_request = $this->StaffExcelExport_model->get_all_leave_detail_for_exportAnnual($employee_code);
		if (!empty($leave_request)) {
			
			$emptyArray = $newArray= array();
			foreach ($leave_request AS $key => $line ) { 
			    if ( !in_array($line->staff_id, $emptyArray) ) { 
			        $emptyArray[] = $line->staff_id; 
			        $newArray[] = $line; 
			    } 
			}
			$ar = array("0" => array(
						            'staff_id' => '',
						            'employee_code' => '',
						            'firstname' => '',
						            'lastname' => '',
						            'type' => '',
						            'Total Leave Taken' => '',
						            'CL' => '',
						            'SL' => '',
						            'PL' => '',
						            'Comp Off' => '',
						            'Half Pay' => '',
						            'Unpaid' => '',
						            'Leave Details' => '',
						            'leave_type' => '',
						            'leave_from' => '',
						            'leave_to' => ''
						        )

						);
	      //  echo "<pre>"; 
	      // print_r($employee_code);
	      // print_r($ar);
	      //  print_r($leave_request);
	      //  die;
			$objPHPExcel = new PHPExcel();
	        $objPHPExcel->setActiveSheetIndex(0);
	        // set Main Header

	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 1, 'Academic Year');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 1, '2019-2020'); 

	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 3, 'Employee Code');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 3, $newArray[0]->employee_code);
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 4, 'Employee Name');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 4, $newArray[0]->firstname.' '.$newArray[0]->lastname);
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 5, 'Staff Member Type');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 5, $newArray[0]->type);

	        if ($newArray[0]->type=='Permanent Teaching') {
	        	$position ='Permanent';
	        } else {
	        	$position ='Non Permanent';
	        }
	        
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 5, 'Position');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 5, $position);
			
			// set Header
			$row = 7;
			$removeCols = array('staff_id','leave_type','leave_from','leave_to');	
			foreach($ar as $header_data ){
				$col = 0;
				foreach($header_data as $key=>$value) {
					if($row == 7){
						if(!in_array($key,$removeCols)){
							if($key == 'employee_code'){
								$key = 'Employee Code';
							}
							if($key == 'firstname'){
								$key = 'First Name';
							}
							if($key == 'lastname'){
								$key = 'Last Name';
							}
							if($key == 'type'){
								$key = 'Leave Type';
							}
							$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
							$col++;
						}					
					}
				}
				$row++;
			}		
	        // set Row        
			$rowCount = 8;
			foreach($newArray as $row_data ){
				$col = 0;
				$totalcount = $slCount= $clCount= $plCount = 0;
				$str = "";
				$leaveDetail =array();
				foreach($row_data as $key=>$value) {
					if(!in_array($key,$removeCols)){
						foreach ($leave_request as $val) {
							if ($val->staff_id==$value) {
								$leaveDetail[]=$val;
								$totalcount++;
								if ($val->leave_type=="Sick-Leave") {
									$slCount++;
								}
								if ($val->leave_type=="Casual-Leave") {
									$clCount++;
								}
								if ($val->leave_type=="Paid-Leave") {
									$plCount++;
								}
							}
						}
						if($key == 'SL'){
							$value = $slCount;
						}
						if($key == 'CL'){
							$value = $clCount;
						}
						if($key == 'PL'){
							$value = $plCount;
						}
						if($key == 'Total Leave Taken'){
							$value = $totalcount;
						}
						if($key == 'Leave Details'){
							foreach ($leaveDetail as $keys => $vals) {
								$str .= $vals->leave_type."-".$vals->leave_from." to ".$vals->leave_to.",";
								// $str .= $vals->leave_type."-".$vals->leave_from.",".$vals->leave_type."-".$vals->leave_to.",";
							}
							$str = trim($str, ',');
							$value = $str;
						}
						$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
						$col++;
					}
				}
				$rowCount++;
			}
			$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
	        $objWriter->save($fileName);
			// download file
	        header("Content-Type: application/vnd.ms-excel");
	        redirect($fileName);
		}else {
			$objPHPExcel = new PHPExcel();
	        $objPHPExcel->setActiveSheetIndex(0);
	        // set Main Header

	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 1, 'Academic Year');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 1, '2019-2020'); 

	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 2, 'Employee Code');
	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 2, $employee_code);

	        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 4, 'No Leave taken');
	        $objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
	        $objWriter->save($fileName);
			// download file
	        header("Content-Type: application/vnd.ms-excel");
	        redirect($fileName);
		}
	}
	public function testmail() 
	{
		
		// $email_id = "viren.shah1985@gmail.com";	
		//$email_id = "apncoders@gmail.com";
		$email_id = "abhishek.apncoders@gmail.com";
		$config = Array(
            'protocol'  => 'smtp',
            'smtp_host' => 'ssl://smtp.googlemail.com',
            'smtp_port' => '465',
            'smtp_user' => 'viren.shah1985@gmail.com',
            'smtp_pass' => 'HelloWorld@2013',
            'mailtype'  => 'html',
            'starttls'  => true,
            'newline'   => "\r\n"
        );

        $this->load->library('email', $config);		
		$message = "Test message - Sending from gmail server";
		$this->email->from('svtcollege2019@gmail.com', 'SVT College');
		$this->email->to($email_id);
		//$this->email->cc('svtcollege2019@gmail.com');
		//$this->email->cc('shah.viren1985@gmail.com');
		$this->email->subject("Sending from gmail server");
		$this->email->message($message);
		//$this->email->attach($pdf_path.'/'.$filename);
		if($this->email->send())
		{
			echo "Email sent from gmail server";
		}
		else
		{
			//echo "fail";
			echo $this->email->print_debugger();
		}		
		die();
	}
	
	
}?>