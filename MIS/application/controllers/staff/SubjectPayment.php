<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class SubjectPayment extends CI_Controller {

	public function __construct(){
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
		$this->load->helper('url');    
		$this->load->helper('form');
		$this->load->library('form_validation');
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
	}
	
	public function index() {
		$userID = $this->session->userdata("userID");
		$user_details = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry( $user_details[0]->course_id );
		if($data['course_details'][0]->course_name == 'Honors'){
			redirect(site_url(), 'refresh');
		}
		$this->load->view('student/common/header');
		$this->load->view('student/subject-fee-payment',$data);
		$this->load->view('student/common/footer');
	}
	
	public function pay_online() {
		$this->form_validation->set_rules('registration_number', 'Registration Number', 'trim|required|numeric|max_length[6]');
		$this->form_validation->set_rules('total_amount', 'Total Amount', 'required');
		if ($this->form_validation->run() === FALSE) {
			$this->session->set_flashdata('error', 'Fill all mandatory fields.');
            $this->index();
		} else {
			$user_details = $this->student->get_entry($_POST['registration_number']);
			$course_details = $this->course_detail->get_entry( $user_details[0]->course_id );
			$fees_amount = $this->input->post('subject_fees');
			$amount = $this->input->post('total_amount');
			//$MERCHANT_KEY = "BC50nb";
			$MERCHANT_KEY = "hDkYGPQe";
			//$SALT = "Bwxo1cPe";
			$SALT = "yIEkykqEH3";
			// End point - change to https://secure.payu.in for LIVE mode
			$PAYU_BASE_URL = "https://test.payu.in";
			$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
			$action = '';
			$productinfo = 'Addon Subject Fees';
			$firstname = $user_details[0]->first_name;
			$lastname = $user_details[0]->last_name;
			$email = $user_details[0]->email_id;
			$phone = $user_details[0]->mobile_number;
			$field1 = $this->input->post('student_field');
			$field2 = $this->input->post('semester');
			$field3 = $this->input->post('spec_shortform');
			$field4 = $this->input->post('subject');
			$concate_data = $field1.'__'.$field2.'__'.$field3.'__'.$field4;
			$udf1 = $concate_data;
			$udf2 = '';
			$udf3 = '';
			$udf4 = '';
			$udf5 = '';
			$udf6 = '';

			$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
			$hash = strtolower(hash('sha512', $hashstring));
			$action = $PAYU_BASE_URL . '/_payment';

			$success = base_url() . 'student/SubjectPaymentStatus';
			$fail = base_url() . 'student/SubjectPaymentStatus';
			$cancel = base_url() . 'student/SubjectPaymentStatus';

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
			
			$this->load->view('confirmation', $data);
        }
	}
}?>