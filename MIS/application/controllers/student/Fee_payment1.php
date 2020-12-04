<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Fee_payment extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
        $this->load->helper('url');    
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
		$this->load->model('student/fee_detail');
	}

	public function index() {
		$userID = $this->session->userdata("userID");
		$data['student_details'] = $this->student->get_entry( $userID );
		$course_id = $data['student_details'][0]->course_id;
		$data['course_details'] = $this->course_detail->get_entry( $course_id );
		$data['fee_details'] = $this->fee_detail->get_entry( $course_id );
		//print("<pre>".print_r($data,true)."</pre>"); die;
		$this->load->view('student/common/header' $data);
		$this->load->view('student/fee_payment', $data);
		$this->load->view('student/common/footer');
	}

    public function check() {
        $fee_head_total = $this->input->post('fee_head_total');
        $amount_with_comma = $this->input->post('amount');
        $amount = str_replace(',', '', $amount_with_comma);        
		$commission = $this->input->post('commission');

        $firstSplitArr = array("name"=>"splitID1", "value"=>$fee_head_total, "merchantId"=>"4825050", "description"=>"payment splitting", "commission"=>$commission);
        // payu child merchant id - 4825050

        $paymentPartsArr = array($firstSplitArr);   
        $finalInputArr = array("paymentParts" => $paymentPartsArr);
        $Prod_info = json_encode($finalInputArr);

        // Merchant key here as provided by Payu
        $MERCHANT_KEY = "BC50nb";
        //$MERCHANT_KEY = "qk9XqxqH";
        $SALT = "Bwxo1cPe";
        //$SALT = "fZGlLKDEO9";
        // End point - change to https://secure.payu.in for LIVE mode
        $PAYU_BASE_URL = "https://test.payu.in";
        $txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
        $action = '';

        $productinfo = $Prod_info;
        $firstname = $this->input->post('firstname');
		$lastname = '';
        $email = $this->input->post('email');
        $phone = $this->input->post('mobile');
        $udf1 = '';
        $udf2 = '';
        $udf3 = '';
        $udf4 = '';
        $udf5 = '';

        $hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
		
		$hash = strtolower(hash('sha512', $hashstring));
        $action = $PAYU_BASE_URL . '/_payment';

        $success = base_url() . 'Status';  
        $fail = base_url() . 'Status';
        $cancel = base_url() . 'Status';

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