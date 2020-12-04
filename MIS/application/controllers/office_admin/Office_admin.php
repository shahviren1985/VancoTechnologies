<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Office_admin extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		$this->load->helper(array('form'));
		$this->load->library('excel');
		$this->load->model('office_admin/excel_import_model');
		$this->load->library('session');
		$this->load->model('office_admin/search_model');
		$this->load->model('office_admin/StudentExcelExport_model');
	}

	public function index() {
		$data['todays_transaction'] = $this->search_model->get_todays_transaction(); 
		$data['recent_transaction'] = $this->search_model->get_recent_transaction(10); 
		//$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/home', $data);
		$this->load->view('office_admin/common/footer');
	}

	public function import_excel_view() {
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/upload_user_excel');
		$this->load->view('office_admin/common/footer');
	}

	public function get_student_courses() {
		$userID = $_POST['userID'];
		$data['student_details'] = $this->StudentExcelExport_model->get_student_by_userId($userID);
		$data['course_details'] = $this->StudentExcelExport_model->get_course_details($data['student_details']['course_id']);
		// echo "<pre>";print_r($_POST);print_r($data); die;
		echo json_encode($data);
		exit;
	}

	public function addon_subject_student() {
		
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/addon_subject_student');
		$this->load->view('office_admin/common/footer');
	}

	public function addon_subject_student_post() {
		$this->load->helper('form');
		$this->load->library('form_validation');
		$this->form_validation->set_rules('userID', 'Registration Number', 'trim|required|numeric|max_length[6]');
		$this->form_validation->set_rules('semester','Semester','required');
		$this->form_validation->set_rules('subject','Subject code','required');
		$this->form_validation->set_rules('subject_name','Subject name','required');
		$this->form_validation->set_rules('credits','Credits','required');
		$this->form_validation->set_rules('active','Payment status','required');
		
		if ($this->form_validation->run() === FALSE) {
			$this->session->set_flashdata('error', 'Fill all mandatory fields.');
			redirect("officeadmin/addon-subject-student");	
		} else {
			// echo "<pre>";print_r($_POST); die;
			if($this->StudentExcelExport_model->add_addon_subject_student()){
				$this->session->set_flashdata('msg', 'Added successfully.');
				redirect("officeadmin/addon-subject-student");					
			}else{
				$this->session->set_flashdata('error', 'Fail to add..');
				redirect("officeadmin/addon-subject-student");	
			}
		}
	}

	// importing excel data into MYSQL
	public function import() {
		if(isset($_FILES["file"]["name"])) {
			$filename = $_FILES["file"]["name"];
		    $path = $_FILES["file"]["tmp_name"];
		    $file_directory = "uploads/userDetails";
			$new_file_name = date("d-m-Y")."_".rand(000000, 999999);

			$config['upload_path'] = $file_directory;
            $config['allowed_types'] = 'xls|xlsx';
            $config['file_name'] = $new_file_name;
            $this->load->library('upload', $config);
            if ( ! $this->upload->do_upload('file')) {
                $error = array('error' => $this->upload->display_errors());
                print_r($error);
            }
            else {
                // $data = array('upload_data' => $this->upload->data());
                // print_r($data);

                $object = PHPExcel_IOFactory::load($path);
			    foreach($object->getWorksheetIterator() as $worksheet) {
			        $highestRow = $worksheet->getHighestRow();
			        $highestColumn = $worksheet->getHighestColumn();
			        for($row=2; $row<=$highestRow; $row++) {
			            //$id = $worksheet->getCellByColumnAndRow(0, $row)->getValue();
			            $course_id = $worksheet->getCellByColumnAndRow(1, $row)->getValue();
			            $college_registration_number = $worksheet->getCellByColumnAndRow(2, $row)->getValue();
			            $roll_number = $worksheet->getCellByColumnAndRow(3, $row)->getValue();
			            $last_name = $worksheet->getCellByColumnAndRow(4, $row)->getValue();
			            $first_name = $worksheet->getCellByColumnAndRow(5, $row)->getValue();
			            $middle_name = $worksheet->getCellByColumnAndRow(6, $row)->getValue();
			            $mother_first_name = $worksheet->getCellByColumnAndRow(7, $row)->getValue();
			            $date_of_birth = $worksheet->getCellByColumnAndRow(8, $row)->getValue();
			            $birth_place = $worksheet->getCellByColumnAndRow(9, $row)->getValue();
			            $gender = $worksheet->getCellByColumnAndRow(10, $row)->getValue();
			            $religion = $worksheet->getCellByColumnAndRow(11, $row)->getValue();
			            $category = $worksheet->getCellByColumnAndRow(12, $row)->getValue();
			            $caste = $worksheet->getCellByColumnAndRow(13, $row)->getValue();
			            $blood_group = $worksheet->getCellByColumnAndRow(14, $row)->getValue();
			            $correspondance_address = $worksheet->getCellByColumnAndRow(15, $row)->getValue();
			            $city = $worksheet->getCellByColumnAndRow(16, $row)->getValue();
			            $location_category = $worksheet->getCellByColumnAndRow(17, $row)->getValue();
			            $pin = $worksheet->getCellByColumnAndRow(18, $row)->getValue();
			            $state = $worksheet->getCellByColumnAndRow(19, $row)->getValue();
			            $country = $worksheet->getCellByColumnAndRow(20, $row)->getValue();
			            $permanent_address = $worksheet->getCellByColumnAndRow(21, $row)->getValue();
			            $permanent_city = $worksheet->getCellByColumnAndRow(22, $row)->getValue();
			            $permanent_taluka = $worksheet->getCellByColumnAndRow(23, $row)->getValue();
			            $permanent_pin = $worksheet->getCellByColumnAndRow(24, $row)->getValue();
			            $permanent_state = $worksheet->getCellByColumnAndRow(25, $row)->getValue();
			            $permanent_country = $worksheet->getCellByColumnAndRow(26, $row)->getValue();
			            $mobile_number = $worksheet->getCellByColumnAndRow(27, $row)->getValue();
			            $email_id = $worksheet->getCellByColumnAndRow(28, $row)->getValue();
			            $guardian_income = $worksheet->getCellByColumnAndRow(29, $row)->getValue();
			            $physical_disability = $worksheet->getCellByColumnAndRow(30, $row)->getValue();
			            $previous_exam_state = $worksheet->getCellByColumnAndRow(31, $row)->getValue();
			            $school_college = $worksheet->getCellByColumnAndRow(32, $row)->getValue();
			            $PRN_umber = $worksheet->getCellByColumnAndRow(33, $row)->getValue();
			            $marital_status = $worksheet->getCellByColumnAndRow(34, $row)->getValue();
			            $aadhaar_number = $worksheet->getCellByColumnAndRow(35, $row)->getValue();
			            $marks_obtained = $worksheet->getCellByColumnAndRow(36, $row)->getValue();
			            $marks_outof = $worksheet->getCellByColumnAndRow(37, $row)->getValue();
			            $academic_year = $worksheet->getCellByColumnAndRow(38, $row)->getValue();
			            $left_college = $worksheet->getCellByColumnAndRow(39, $row)->getValue();
			            $photo_path = $worksheet->getCellByColumnAndRow(40, $row)->getValue();

			            $data[] = array(
			                'id' => '',
			                'course_id'   => $course_id,
			                'college_registration_number' => $college_registration_number,
			                'roll_number' => $roll_number,
			                'last_name' => $last_name,
			                'first_name' => $first_name,
			                'middle_name' => $middle_name,
			                'mother_first_name' => $mother_first_name,
			                'date_of_birth' => $date_of_birth,
			                'birth_place' => $birth_place,
			                'gender' => $gender,
			                'religion' => $religion,
			                'category' => $category,
			                'caste' => $caste,
			                'blood_group' => $blood_group,
			                'correspondance_address' => $correspondance_address,
			                'city' => $city,
			                'location_category' => $location_category,
			                'pin' => $pin,
			                'state' => $state,
			                'country' => $country,
			                'permanent_address' => $permanent_address,
			                'permanent_city' => $permanent_city,
			                'permanent_taluka' => $permanent_taluka,
			                'permanent_pin' => $permanent_pin,
			                'permanent_state' => $permanent_state,
			                'permanent_country' => $permanent_country,
			                'mobile_number' => $mobile_number,
			                'email_id' => $email_id,
			                'guardian_income' => $guardian_income,
			                'physical_disability' => $physical_disability,
			                'previous_exam_state' => $previous_exam_state,
			                'school_college' => $school_college,
			                'PRN_umber' => $PRN_umber,
			                'marital_status' => $marital_status,
			                'aadhaar_number' => $aadhaar_number,
			                'marks_obtained' => $marks_obtained,
			                'marks_outof' => $marks_outof,
			                'academic_year' => $academic_year,
			                'left_college' => $left_college,
			                'photo_path' => $photo_path

			            );
			            //print_r($data);
			            //die;
			        }
			    }
			    $this->excel_import_model->insert($data);
			    echo 'Data Imported successfully';
            }
		}
	}
}

?>