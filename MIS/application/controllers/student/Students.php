<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Students extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
		$this->load->library('form_validation');
		$this->load->model('office_admin/StudentExcelExport_model');
		$this->load->model('student/account_history_model');
	}

	public function index() {
		$userID = $this->session->userdata("userID");
		$data['student'] = $this->student->get_student_by_id($userID);
		$data['student_details'] = $this->student->get_entry($userID );
		$data['cur_course'] = $this->StudentExcelExport_model->popuplate_course_data($data['student']['course_id']);
		$course_id = $data['student_details'][0]->course_id;
		$data['course_details'] = $this->course_detail->get_entry( $course_id );
		$data['courseList'] = $this->StudentExcelExport_model->fetch_course_name();
		$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		$this->load->view('student/common/header', $data);
		$this->load->view('student/home', $data);
		$this->load->view('student/common/footer');
	}

	public function ftl() {
		$user_id = $this->input->post('id');
		$this->student->ftl($user_id);
	}

	public function update_basic_details() {
		$userID = $this->input->post('userID');
		$data['email_id'] = $this->input->post('email');
		$data['mobile_number'] = $this->input->post('mobile');
		$data['permanent_address'] = $this->input->post('address');
		$data['state'] = $this->input->post('state');
		$data['pin'] = $this->input->post('zip');
		//$this->student->update_basic_details($data, $id);

		if( $this->student->update_basic_details($data, $userID) ) {
			echo $msg = '<div class="alert alert-success alert-dismissible pop-notice"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Success:</strong> Details updated successfully</div>';
		} else {
			echo $msg = '<div class="alert alert-danger alert-dismissible pop-notice"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Success:</strong> Account cannot updated! Please try again.</span></div>';
		}
	}
	
	public function edit($id) {
	/* 	echo $id; */
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
		$data['student_details'] = $this->student->get_entry($userID );
		$course_id = $data['student_details'][0]->course_id;
		$data['course_details'] = $this->course_detail->get_entry( $course_id );
		$data['cur_course'] = $this->StudentExcelExport_model->popuplate_course_data($data['student']['course_id']);
		$this->form_validation->set_rules('firstname', 'Firstname', 'required');
        $this->form_validation->set_rules('lastname', 'Lastname', 'required');
		$this->form_validation->set_rules('roll_number', 'Roll Number', 'trim|required|numeric|max_length[3]');
		$this->form_validation->set_rules('aadhaar_number', 'Aadhar Number', 'trim|required|numeric|min_length[12]|max_length[12]');
		$this->form_validation->set_rules('college_reg', 'Registration Number', 'trim|required|max_length[6]');
		$this->form_validation->set_rules('mobile_number', 'Mobile Number', 'trim|required|numeric');
		$this->form_validation->set_rules('pin', 'Pincode', 'trim|required|numeric');
		$this->form_validation->set_rules('permanent_pin', 'Permanent Pincode', 'trim|required|numeric');
		$this->form_validation->set_rules('marks_obtained', 'Marks Obtained', 'trim|required|numeric');
		$this->form_validation->set_rules('marks_outof', 'Marks Outof', 'trim|required|numeric');
		$this->form_validation->set_rules('percentage_in_ssc', 'Percentage in SSC', 'trim|required|decimal');
		$this->form_validation->set_rules('email_id', 'Email Id', 'required|valid_email');
		$this->form_validation->set_rules('percentage_in_ssc', 'Percentage in SSC', 'trim|required|decimal'); 
		if($_POST['caste_category'] == 'Reserved'){
			$this->form_validation->set_rules('account_holder_name','Account Holder Name', 'required');
			$this->form_validation->set_rules('bank_acc_no', 'Account Number', 'required');
			$this->form_validation->set_rules('ifsc_code', 'IFSC Code', 'required');
		}
		
		if ($this->form_validation->run() === FALSE) {
            $this->load->view('student/common/header', $data);
            $this->load->view('student/home',$data);
            $this->load->view('student/common/footer');
        } else { 
            //$this->session->set_flashdata('msg', 'Unable to update your data at this moment. Please contact svtcollege2019@gmail.com');
            $this->student->update($userID);
			$this->session->set_flashdata('msg', 'Record successfully Updated');
			//redirect('controller_name/addRoom');
			redirect(site_url('student/home'));
        }
    }
		
	public function change_password() {
		$this->load->helper('form');
        $this->load->library('form_validation');
		$this->form_validation->set_rules('new_password', 'New Password', 'required|min_length[3]|max_length[20]');
		$this->form_validation->set_rules('conf_password', 'Confirm Password', 'required|min_length[3]|max_length[20]');
		if($this->form_validation->run()){			
			$this->student->update_password();
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
					'upload_path' => './uploads/senior/'.$_POST['admission_year'].'/Photos',
					'allowed_types' => 'jpg|png',
					'overwrite' => TRUE,
					'max_size' => "30270",
				);
				$this->load->library('upload', $config);
				if(!$this->upload->do_upload('new_photo')){
					$error = array('error' => $this->upload->display_errors());
					echo json_encode($error);
				}else{
					$result = $this->student->update_photo($file_name);
					if($result){
						$path = base_url().'uploads/senior/'.$_POST['admission_year'].'/Photos';
						echo json_encode(['success'=> true, 'msg'=>'Photo Changed Succesfully', 'file_path' => $path.'/'.$file_name]);
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
			$count = $this->student->check_reg_exist();
			if(count($count) >= 1){
				echo 'false';
			} else {
				echo 'true';
			}
		}
		exit;
	}
	public function application_form() {
		$userID = $this->session->userdata("userID");
		$data['student'] = $this->student->get_student_by_id($userID);
		$data['applications'] = $this->student->get_applications_by_id($userID);
		// echo "<pre>";print_r($data); die;
		$this->load->view('student/common/header');
		$this->load->view('student/application_form', $data);
		$this->load->view('student/common/footer');
	}
	public function add_application_form() {
		$this->load->helper('form');
        $this->load->library('form_validation');
		$this->form_validation->set_rules('application_type', 'Application type', 'required');
		$this->form_validation->set_rules('application_reason', 'Application Reason', 'required');
		if($this->form_validation->run()){	
			 // echo "<pre>";print_r($_FILES);print_r($_POST); die;
			if(($_FILES['application_doc']['name'] != "" )){
				$new_name = time();
				$file_ext = pathinfo($_FILES["application_doc"]['name'],PATHINFO_EXTENSION);
				$file_name = $new_name.".".$file_ext;
				$config = array(
					'file_name' => $file_name,
					'upload_path' => './uploads/applications',
					'allowed_types' => 'jpg|jpeg|png|pdf|doc|docx',
					'overwrite' => TRUE,
					'max_size' => "30270",
				);
				$this->load->library('upload', $config);
				if(!$this->upload->do_upload('application_doc')){
					$error = array('error' => $this->upload->display_errors());
					echo json_encode($error);
				}else{
					$_POST['application_doc'] = $file_name;
					$result = $this->student->add_application();
					if($result){
						echo json_encode(['success'=> true, 'msg'=>'Application added Succesfully']);
					}
				}
			}else {
				$result = $this->student->add_application();
				if($result){
					echo json_encode(['success'=> true, 'msg'=>'Application added Succesfully']);
				}
			}
		}	
	}
}