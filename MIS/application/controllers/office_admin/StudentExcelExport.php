<?php
defined('BASEPATH') OR exit('No direct script access allowed'); 
class StudentExcelExport extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
        $this->load->model('office_admin/StudentExcelExport_model');
		$this->load->model('student/account_history_model');
	}

	public function index() {	
	    //$data['result'] = $this->StudentExcelExport_model->fetch_data();
		$data['courseList'] = $this->StudentExcelExport_model->fetch_course_name();
		$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/student_export_excel',$data);
		$this->load->view('office_admin/common/footer');
	}
	
	public function edit($id) {
		if (empty($id)) {
			show_404();
        }
        $this->load->helper('form');
        $this->load->library('form_validation');
        $data['courseList'] = $this->StudentExcelExport_model->fetch_course_name();
		$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		$data['student'] = $this->StudentExcelExport_model->get_student_by_id($id);
		$userID = $data['student']['userID'];
		$data['transaction_details'] = $this->account_history_model->get_transaction($userID);
		$data['cur_course'] = $this->StudentExcelExport_model->popuplate_course_data($data['student']['course_id']);
        $this->form_validation->set_rules('firstname', 'Firstname', 'required');
        $this->form_validation->set_rules('lastname', 'Lastname', 'required');
		$this->form_validation->set_rules('roll_number', 'Roll Number', 'trim|required|numeric|max_length[3]');
		$this->form_validation->set_rules('aadhaar_number', 'Aadhar Number', 'trim|required|numeric|min_length[12]|max_length[12]');
		$this->form_validation->set_rules('college_reg', 'Registration Number', 'trim|required|max_length[6]');
		//$this->form_validation->set_rules('mobile_number', 'Mobile Number', 'required|numeric');
		$this->form_validation->set_rules('pin', 'Pincode', 'trim|required|numeric');
		$this->form_validation->set_rules('permanent_pin', 'Permanent Pincode', 'trim|required|numeric');
		$this->form_validation->set_rules('marks_obtained', 'Marks Obtained', 'trim|required|numeric');
		$this->form_validation->set_rules('marks_outof', 'Marks Outof', 'trim|required|numeric');
		$this->form_validation->set_rules('percentage_in_ssc', 'Percentage in SSC', 'trim|required|decimal');
		$this->form_validation->set_rules('email_id', 'Email Id', 'required|valid_email');
		$this->form_validation->set_rules('percentage_in_ssc', 'Percentage in SSC', 'trim|required|decimal');
		
		if ($this->form_validation->run() === FALSE) {
            $this->load->view('office_admin/common/header');
            $this->load->view('office_admin/edit_student',$data);
            $this->load->view('office_admin/common/footer');
        } else{
			$this->StudentExcelExport_model->update($id);
			$this->session->set_flashdata('msg', 'Student successfully Updated');
			//redirect('controller_name/addRoom');
			redirect(site_url('officeadmin/export-student-list'));
        }
    }
	
	public function studentID($userID) {
		$this->load->helper('url');
		if (empty($userID)) {
			show_404();
        }
		$userDetail = $this->StudentExcelExport_model->get_student_by_userId($userID);
		if($userDetail['id']){
			redirect('/officeadmin/studentlist/studentdetails/edit/'.$userDetail['id'], 'refresh');
		} else {
			redirect('/officeadmin/reporting', 'refresh');
		}
	}
	
	public function searchStudents() {
		$records = $this->StudentExcelExport_model->fetch_data();
		$draw = $this->input->post('draw');
		## Fetch records
		$data = array();
		$count = $_POST['start'];
		$current_user = $this->session->userdata('connectionString');
		foreach($records as $record){
			//echo "<pre>"; print_r($record); echo "</pre>";
			$count++;
			if($current_user == 'officeadmin1'){
				//$course_type = (empty($record->course_type)) ? 'Regular': 'Honors';
				$course_name = $record->year . '-Regular';	
			}else{
				$course_type = (empty($record->course_type)) ? 'Regular': 'Honors';
				$course_name = $record->year . '-'.$course_type;
			}
			
			$data[] = array(
				"s_no"=>$count,
				"userID"=>'<a href="'.site_url('officeadmin/studentlist/studentdetails/edit/'.$record->id).'">'.$record->userID.'</a>',
				"roll_number"=>$record->roll_number,
				"course_name"=>$course_name,
				"short_form"=>@$record->short_form,
				"full_name"=>$record->last_name. ' ' .$record->first_name . ' ' .$record->middle_name. ' '.$record->mother_first_name,
				"caste"=>@$record->caste,
				"religion"=>@$record->religion,
				"blood_group"=>@$record->blood_group,
				"mobile_number"=>@$record->mobile_number,
				"permanent_state"=>@$record->permanent_state,
				"physical_disability"=>@$record->physical_disability,
				"academic_year"=>@$record->academic_year,
				"percentage"=>($record->marks_obtained && $record->marks_outof) ? round(($record->marks_obtained/$record->marks_outof) * 100,2) : '',
			);
		}

		## Response
		$response = array(
			"draw" => intval($draw),
			"recordsTotal" => $this->StudentExcelExport_model->count_all(),
			"recordsFiltered" => $this->StudentExcelExport_model->count_filtered(),
			"aaData" => $data
		);
		echo json_encode($response);
	}
	
	public function searchstudentcertificates() {
		$records = $this->StudentExcelExport_model->fetch_data();
		$draw = $this->input->post('draw');
		## Fetch records
		$data = array();
		$count = $_POST['start'];
		foreach($records as $record){
			$count++;
			$course_type = (empty($record->course_type)) ? 'Regular': 'Honors';
			$course_name = $record->year . '-'.$course_type;
			$data[] = array(
				"s_no"=>$count,
				"userID"=>'<a href="'.site_url('officeadmin/leaving-certificate/add/'.$record->id).'">'.$record->userID.'</a>',
				"roll_number"=>$record->roll_number,
				"course_name"=>$course_name,
				"short_form"=>@$record->short_form,
				"full_name"=>$record->last_name. ' ' .$record->first_name . ' ' .$record->middle_name. ' '.$record->mother_first_name,
				"mobile_number"=>@$record->mobile_number
			);
		}

		## Response
		$response = array(
			"draw" => intval($draw),
			"recordsTotal" => $this->StudentExcelExport_model->count_all(),
			"recordsFiltered" => $this->StudentExcelExport_model->count_filtered(),
			"aaData" => $data
		);
		echo json_encode($response);
	}
	
	
	public function change_password() {
		$this->load->helper('form');
        $this->load->library('form_validation');
		$this->form_validation->set_rules('new_password', 'New Password', 'required|min_length[3]|max_length[20]');
		$this->form_validation->set_rules('conf_password', 'Confirm Password', 'required|min_length[3]|max_length[20]');
		if($this->form_validation->run()){			
			$this->StudentExcelExport_model->update_password();
			echo json_encode(['success'=>'Password Changed Succesfully']);
		} else {
			$errors = validation_errors();
            echo json_encode(['error'=>$errors]);
		}		
	}	
	
	
	public function change_photo() {
		$this->load->helper('form');
        $this->load->library('form_validation');
		$this->form_validation->set_rules('new_photo', '', 'callback_file_check');
		if($this->form_validation->run()){	
			if(($_FILES['new_photo']['name'] != "" )){
				$new_name = time();
				$file_ext = pathinfo($_FILES["new_photo"]['name'],PATHINFO_EXTENSION);
				$file_name = $new_name.".".$file_ext;
				$config = array(
					'file_name' => $file_name,
					'upload_path' => './uploads/senior/'.$_POST['student_admission_year'].'/Photos',
					'allowed_types' => 'jpg|png',
					'overwrite' => TRUE,
					'max_size' => "30270",
				);
				$this->load->library('upload', $config);
				if(!$this->upload->do_upload('new_photo')){
					$error = array('error' => $this->upload->display_errors());
					echo json_encode($error);
				}else{
					$result = $this->StudentExcelExport_model->update_photo($file_name);
					if($result){
						$path = base_url().'uploads/senior/'.$_POST['student_admission_year'].'/Photos';
						echo json_encode(['success'=>'Photo Changed Succesfully', 'file_path' => $path.'/'.$file_name]);
					}
				}
			}
		}	
	}
	
	public function file_check($str){
        $allowed_mime_type_arr = array('image/jpeg','image/pjpeg','image/png','image/x-png');
        $mime = get_mime_by_extension($_FILES['new_photo']['name']);
        if(isset($_FILES['new_photo']['name']) && $_FILES['new_photo']['name']!=""){
            if(in_array($mime, $allowed_mime_type_arr)){
                return true;
            }else{
                $this->form_validation->set_message('file_check', 'Please select only jpg/png file.');
                return false;
            }
        }else{
            $this->form_validation->set_message('file_check', 'Please choose a file to upload.');
            return false;
        }
    }
	
	
	public function get_course_id() {
		$data['course_id'] = $this->StudentExcelExport_model->get_course_id();
		echo json_encode(array('course_id'=>$data['course_id'][0]->id));
		exit;
	}
	
	public function check_reg_exist() {
		$college_reg = $this->input->post('college_reg');
		$college_old_id = $this->input->post('college_old_id');
		if($college_reg == $college_old_id){
			echo 'true';
		} else {
			$count = $this->StudentExcelExport_model->check_reg_exist();
			if(count($count) >= 1){
				echo 'false';
			} else {
				echo 'true';
			}
		}
		exit;
	}

	public function export() {	
		$all_export = $this->input->post('all_export');
		$fileName = 'StudentDetails.xlsx';
		// load excel library
        $this->load->library('excel');
        $student_info = $this->StudentExcelExport_model->export_data();
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);
		// set Header
		$row = 1;
		if(isset($all_export) && !empty($all_export)){
			$removeCols = array('password','course_name','specialization','father_name','left_college_date');
		} else {
			$removeCols = array('course_name','specialization','father_name','left_college_date');
		}		
		foreach($student_info as $header_data ){
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){
					if(!in_array($key,$removeCols)){
						if($key == 'userID'){
							$key = 'College Registration Number';
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
		foreach($student_info as $row_data ){
			$col = 0;
			foreach($row_data as $key=>$value) {
				if(!in_array($key,$removeCols)){
					if($key == 'date_of_birth'){
						$value = date('d-m-Y',strtotime($value));
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
	
	public function export_students_details(){
		$fileName = 'Student_details.xlsx';
		// load excel library
        $this->load->library('excel');
        $student_info = $this->StudentExcelExport_model->export_data();
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);
		// set Header
		$row = 1;
		foreach($student_info as $header_data ){
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){				
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
					$col++;		
				}
			}
			$row++;
		}		
        // set Row        
		$rowCount = 2;
		foreach($student_info as $row_data ){
			$col = 0;
			foreach($row_data as $key=>$value) {				
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
				$col++;
			}
			$rowCount++;
		}
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
		// download file
        //ob_end_clean();
		header("Content-Disposition: attachment; filename=\"$fileName\"");
		header("Content-Type: application/vnd.ms-excel");
		$objWriter->save("php://output");
        //redirect($fileName);
	}
	
	public function export_update_excel(){
		$fileName = 'Student_Update_Excel.xlsx';
		// load excel library
        $this->load->library('excel');
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);

        // SELECT `userID`,`PRN_number`,`left_college`,`left_college_date`,`dropped`,`library_card_no` FROM `student_details` WHERE `userID` BETWEEN '6101' AND '6105' ORDER BY `userID` ASC;

		// set Header
        //$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 1, 'collageRegistrationID');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 1, 'PRN Number');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 1, 'Left College');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 1, 'Left College Date');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 1, 'Dropped');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 1, 'Library Card No.');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 1, 'Final Grade');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 1, 'FeedBack Received');

        // set Content

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 2, '6101');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 2, '6101345');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 2, 'Yes');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 2, '2017-04-23');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 2, 'Yes');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 2, '6101-L01');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 2, 'A');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 2, 'Yes');

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 3, '6102');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 3, '6102346');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 3, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 3, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 3, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 3, 'A');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 3, 'No');

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 4, '6103');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 4, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 4, 'No');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 4, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 4, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 4, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 4, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 4, '');

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 5, '6104');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 5, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 5, 'Yes');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 5, '2017-03-20');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 5, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 5, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 5, 'O+');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 5, 'Yes');

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 6, '6105');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 6, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 6, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 6, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 6, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 6, '6105-L05');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 6, '');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 6, '');


		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
		// download file
        //ob_end_clean();
		header("Content-Disposition: attachment; filename=\"$fileName\"");
		header("Content-Type: application/vnd.ms-excel");
		$objWriter->save("php://output");
        //redirect($fileName);
	}
	

	public function export_userRollno_excel(){
		
		// load excel library
        $this->load->library('excel');
		//echo "<pre>"; 
		// print_r($_POST);
		$course_name = $this->input->post('course_nameE'); // course_name='FY';
        $student_info = $this->StudentExcelExport_model->export_userRollno_data();
        $fileName = $course_name.'-Student_Update_Excel.xlsx';
        // print_r($student_info);die;
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);

        // SELECT `userID`,`PRN_number`,`left_college`,`left_college_date`,`dropped`,`library_card_no` FROM `student_details` WHERE `userID` BETWEEN '6101' AND '6105' ORDER BY `userID` ASC;

		// set Header
        //$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);

        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 1, 'collageRegistrationID');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 1, 'PRN Number');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 1, 'Left College');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 1, 'Left College Date');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 1, 'Dropped');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 1, 'Library Card No.');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 1, 'Final Grade');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 1, 'FeedBack Received');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(8, 1, 'Roll no.');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(9, 1, 'Course Name');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(10, 1, 'Course Id');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(11, 1, 'First Name');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(12, 1, 'Last Name');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(13, 1, 'Course Name');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(14, 1, 'Course Type');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(15, 1, 'Specialization');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(16, 1, 'Division');

      /*  // set Content
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 2, '6101');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 2, '6101345');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 2, 'Yes');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, 2, '2017-04-23');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, 2, 'Yes');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, 2, '6101-L01');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, 2, 'A');
        $objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, 2, 'Yes');*/

        // set Row        
		$rowCount = 2;
		foreach($student_info as $row_data ){
			$col = 0; $cc=11;
			foreach($row_data as $key=>$value) {
				//echo $value." ===== $col ***** $key <br>"; //print_r($row_data);

				if($key == 'course_name_type'){
					if($value == 'Honors'){
						$value = 'Honors';
					} else {
						$value= 'Regular';	
					}
				}

				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
			/*	$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(3, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(4, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(5, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(6, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(7, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(8, $rowCount,'');
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(9, $rowCount,'');*/
				
				$col=$cc;
				//$col++;
				$cc++;
			}
			// die;
			$rowCount++;
		}
		//die;
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
		// download file
        //ob_end_clean();
		header("Content-Disposition: attachment; filename=\"$fileName\"");
		header("Content-Type: application/vnd.ms-excel");
		$objWriter->save("php://output");
        //redirect($fileName);
	}

	public function application_form() {
		$this->load->model('student/student');
		$data['applications'] = $this->student->get_applications_by_id();
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/application_form', $data);
		$this->load->view('office_admin/common/footer');
	}
	public function update_application_form() {
		$this->load->helper('form');
        $this->load->library('form_validation');
		$this->form_validation->set_rules('status', 'Application Status', 'required');
		if($this->form_validation->run()){			
			$this->StudentExcelExport_model->update_application_data();
			echo json_encode(['success'=> true, 'msg'=>'Application status changed Succesfully']);
		} else {
			$errors = validation_errors();
            echo json_encode(['error'=>$errors]);
		}	
	}
	public function clean_rollnumber(){
    	//die('in AK');
		$course_nameE = $this->input->post('course_nameE');
		if(empty($course_nameE)){
			return false;
		}
		if( $this->StudentExcelExport_model->clean_all_rollnumber()) {
			echo json_encode(['success'=> true, 'msg'=>'Roll number cleaned successfully']);
		} else {
			echo json_encode(['success'=> false, 'msg'=>'Error while updating.']);			
		}		
	}
	
}?>