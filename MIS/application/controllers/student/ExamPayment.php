<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class ExamPayment extends CI_Controller {

	public function __construct(){
		parent::__construct();
		if( $this->session->userdata("logged_in") ) {
			if($this->session->userdata("role") != '3'){
				redirect(site_url(), 'refresh');
			}
        }
		$this->load->helper('form');
		$this->load->library('form_validation');
		$this->load->model('student/ExamPaymentModel');
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
	}
	
	public function index() {
		if($this->session->userdata("id") != null){
			$this->load->model('student/Student');
			$userID = $this->session->userdata("userID");
			$data['auth_student'] = $this->Student->get_entry($userID);			
		} else {
			$data['auth_student'] = array('');
		}
		$data['course_year'] = $this->ExamPaymentModel->fetch_course_year();
		$data['course_spec'] = $this->ExamPaymentModel->fetch_course_spec();
		if(@$data['auth_student'][0]->course_id){
			$data['course_name'] = $this->ExamPaymentModel->fetch_course_name($data['auth_student'][0]->course_id);
		}
		$data['application_fees'] = $this->ExamPaymentModel->fetch_application_fee();
		
		if($this->session->userdata("id") != null){
			$userID = $this->session->userdata("userID");
			$user_data = $this->student->get_entry($userID);
			$data['course_details'] = $this->course_detail->get_entry($user_data[0]->course_id );
			$this->load->view('student/common/header', $data);
		} else {
			$this->load->view('student/common/header-mis');
		}
		$this->load->view('student/miscellaneous-payment',$data);
		$this->load->view('student/common/footer');
	}
	
	public function pay_online() {		
		$this->form_validation->set_rules('registration_number', 'Registration Number', 'trim|required');
		$this->form_validation->set_rules('admission_year', 'Admission Year', 'required');
		$this->form_validation->set_rules('first_name', 'Firstname', 'required');
        $this->form_validation->set_rules('last_name', 'Lastname', 'required');
		$this->form_validation->set_rules('application_type', 'Application Type', 'required');
		$this->form_validation->set_rules('app_fees_quantity', 'Quantity', 'required');
		$this->form_validation->set_rules('total_amount', 'Total Amount', 'required');
		$this->form_validation->set_rules('email_address', 'Email Address', 'trim|required|valid_email');
		$this->form_validation->set_rules('phone_number', 'Mobile Number', 'trim|required|numeric');
		if ($this->form_validation->run() === FALSE) {
			$this->session->set_flashdata('error', 'Fill all mandatory fields.');
            $this->index();
		} else {
            $fees_amount = $this->input->post('fees_amount');
			$amount = $this->input->post('total_amount');
			
			$connectionString = $this->session->userdata("connectionString");
			if($connectionString == 'clg_db2' || $this->input->post('student_field')=='bachelor'){
				/*$MERCHANT_KEY = "7aizVQa5"; // LIVE ACCOUNT
		        $SALT = "mLXPoA2jsp"; // LIVE ACCOUNT*/

                $MERCHANT_KEY = "hDkYGPQe"; // TEST ACCOUNT
                $SALT = "yIEkykqEH3"; // TEST ACCOUNT

			}elseif ($connectionString == 'clg_db1' || $this->input->post('student_field')=='master'){
				/*$MERCHANT_KEY = "ie7pMMR8"; // LIVE ACCOUNT
		        $SALT = "tvhLzHpdhu"; // LIVE ACCOUNT*/

                $MERCHANT_KEY = "hDkYGPQe"; // TEST ACCOUNT
                $SALT = "yIEkykqEH3"; // TEST ACCOUNT
		 	}

			

        	// $PAYU_BASE_URL = "https://secure.payu.in"; // LIVE ACCOUNT
        	$PAYU_BASE_URL = "https://test.payu.in"; // TEST ACCOUNT
			
			$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
			$action = '';
			$productinfo = $this->input->post('application_type');
			$firstname = $this->input->post('first_name');
			$lastname = $this->input->post('last_name');
			$email = $this->input->post('email_address');
			$phone = $this->input->post('phone_number');
			$reg_num = $this->input->post('registration_number');
			$c_year = $this->input->post('course_year');
			$c_spec = $this->input->post('specialisation');
			$c_type = $this->input->post('course_type');
			$caste = $this->input->post('caste');
			$ad_year = $this->input->post('admission_year');
			$quantity = $this->input->post('app_fees_quantity');
			$student_type = $this->input->post('student_type');
			$student_degree = $this->input->post('student_field');
			$course_id = 0;
			if(!empty($this->input->post('course_id'))){
				$course_id = $this->input->post('course_id');
			}
			$concate_data = $reg_num.'__'.$c_year.'__'.$c_spec.'__'.$c_type.'__'.$caste.'__'.$ad_year.'__'.$course_id.'__'.$quantity.'__'.$student_type.'__'.$lastname.'__'.$student_degree;
			$udf1 = $concate_data;
			$udf2 = '';
			$udf3 = '';
			$udf4 = '';
			$udf5 = '';
			$udf6 = '';

			$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
			$hash = strtolower(hash('sha512', $hashstring));
			$action = $PAYU_BASE_URL . '/_payment';			
			
			$success = base_url() . 'student/MiscPaymentStatus';
			$fail = base_url() . 'student/MiscPaymentStatus';
			$cancel = base_url() . 'student/MiscPaymentStatus';

			$data = array(
				'MERCHANT_KEY' =>   $MERCHANT_KEY,
				'txnid' =>          $txnid,
				'hash' =>           $hash,
				'amount' =>         $amount,
				'productinfo' =>    $productinfo,
				'firstname' =>      $firstname,
				'lastname' =>      	$lastname,
				'email' =>          $email,
				'phone' =>          $phone,
				'salt' =>           $SALT,
				'udf1' =>           $udf1,
				'udf2' =>           $udf2,
				'udf3' =>           $udf3,
				'udf4' =>           $udf4,
				'udf5' =>           $udf5,
				'action' =>         $action,
				'success' =>        $success,
				'failure' =>        $fail,
				'cancel' =>         $cancel
			);
			$fp = fopen('./uploads/logs/request_'.$txnid.'.json', 'w');
                fwrite($fp, json_encode($data));
			$this->load->view('confirmation', $data);
        }
	}
	
	public function refund_policy(){
		$this->load->view('miscellaneous-refund-policy');
	}
}?>