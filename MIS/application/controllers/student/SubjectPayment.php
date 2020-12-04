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
		$data['addon_subject'] = $this->student->get_addon_subject_by_userid($userID);
		if (empty($data['addon_subject'])) {
			 $this->session->set_flashdata('msg', 'Have you already paid fees for this addon subject or may be your addon subject not added yet, Please contact Admissions Team at svtcollege2019@gmail.com');
                redirect(site_url('student/home'));
		}
		// echo "<pre>";print_r($data['addon_subject']); die;
		if($data['course_details'][0]->course_name == 'Honors'){
                //redirect(site_url('student/home'));
		} 
		$this->load->view('student/common/header', $data);
		// $this->load->view('student/subject-fee-payment',$data);
		$this->load->view('student/subject-fee-payment-new',$data);
		$this->load->view('student/common/footer');
	}
	
	public function pay_online() {
		$this->form_validation->set_rules('registration_number', 'Registration Number', 'trim|required|numeric|max_length[6]');
		$this->form_validation->set_rules('total_amount', 'Total Amount', 'required');
		if ($this->form_validation->run() === FALSE) {
			$this->session->set_flashdata('error', 'Fill all mandatory fields.');
            $this->index();
		} else {
		// echo "<pre>";print_r($_POST); die;
            $userID = $_POST['registration_number'];
			$user_details = $this->student->get_entry($_POST['registration_number']);
			$course_details = $this->course_detail->get_entry( $user_details[0]->course_id );
			$fees_amount = $this->input->post('subject_fees');
       		$commission = $this->input->post('online_amount');
	        $amount_with_comma = $this->input->post('total_amount');
	        $amount = str_replace(',', '', $amount_with_comma); 

	  //       $MERCHANT_KEY = "hDkYGPQe";
			// $SALT = "yIEkykqEH3";

	       /* $firstSplitArr = array("name"=>$userID, "value"=>$fees_amount, "merchantId"=>"4825051", "description"=>"payment splitting", "commission"=>$commission); // TEST ACCOUNT
			$MERCHANT_KEY = "BC50nb"; // TEST ACCOUNT
            $SALT = "Bwxo1cPe"; // TEST ACCOUNT*/

			$firstSplitArr = array("name"=>$userID, "value"=>$fees_amount, "merchantId"=>"6891255", "description"=>"payment splitting", "commission"=>$commission); // LIVE ACCOUNT
		 	$MERCHANT_KEY = "qk9XqxqH"; // LIVE ACCOUNT
            $SALT = "fZGlLKDEO9"; // LIVE ACCOUNT


			$paymentPartsArr = array($firstSplitArr);   
	        $finalInputArr = array("paymentParts" => $paymentPartsArr);
	        $Prod_info = json_encode($finalInputArr);

			// End point - change to https://secure.payu.in for LIVE mode
			// $PAYU_BASE_URL = "https://test.payu.in";
        	$PAYU_BASE_URL = "https://secure.payu.in";

			$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
			$action = '';
			//$productinfo = 'Addon Subject Fees-'.$this->input->post('subject');
			$productinfo = $Prod_info;
			$firstname = $user_details[0]->first_name;
			$lastname = $user_details[0]->last_name;
			$email = $user_details[0]->email_id;
			$phone = $user_details[0]->mobile_number;
			$field0 = $this->input->post('subject_fees');
			$field1 = $this->input->post('student_field');
			$field2 = $this->input->post('semester');
			$field3 = $this->input->post('spec_shortform');
			$field4 = $this->input->post('subject');
			$field5 = $this->input->post('credits');
			$field6 = $this->input->post('subject_id');
			$concate_data = $field0.'__'.$field1.'__'.$field2.'__'.$field3.'__'.$field4.'__'.$field5.'__'.$field6;
			$udf1 = $fees_amount;
			$udf2 = $concate_data;
			$udf3 = '';
			$udf4 = '';
			$udf5 = '';
			//$udf6 = '';

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