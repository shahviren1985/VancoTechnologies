<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class New_payment extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") ) {
            if($this->session->userdata("role") != '3'){
                redirect(site_url(), 'refresh');
            }
        }
        $this->load->helper('form');
        $this->load->library('form_validation');
        $this->load->helper('url');    
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
		$this->load->model('student/fee_detail');
        $this->load->model('office_admin/search_model');
        $this->load->library('user_agent');
	}

	public function index() {
		$data['query_data'] = $_GET;
        // if (!isset($_GET['n']) && !isset($_GET['t']) && !isset($_GET['r']) && !isset($_GET['p']) && !isset($_GET['s'])) {
        if (count($_GET)<5) {
            $this->session->set_flashdata('error', 'Required Query values..');
        }
        //echo "<pre>";print_r($data); die;
		$this->load->view('student/common/header-FY', $data);
        if(isset($_GET['e']) && $_GET['e']==1){
            $this->load->view('student/fee_payment_form_exception', $data);
        }else if(isset($_GET['m']) && $_GET['m']==1){ // Hostel mess payment
            $this->load->view('student/fee_payment_form_hostel_mess', $data);
        }else if(isset($_GET['c']) && $_GET['c']==1){ // cancellation payment
            $this->load->view('student/fee_payment_form_cancellation', $data);
        }else{
            $this->load->view('student/fee_payment_form', $data);  
        }
		$this->load->view('student/common/footer');
	}

    public function fee_payment_post() {
        $this->form_validation->set_rules('registration_number', 'Admission Form Number', 'required');
        $this->form_validation->set_rules('first_name', 'Firstname', 'required');
        $this->form_validation->set_rules('last_name', 'Lastname', 'required');
        $this->form_validation->set_rules('amount', 'Total Amount', 'required');
        $this->form_validation->set_rules('email', 'Email Address', 'trim|required|valid_email');
        $this->form_validation->set_rules('phone_number', 'Mobile Number', 'trim|required|numeric');
        if ($this->form_validation->run() === FALSE) {
            $this->session->set_flashdata('error', 'Fill all mandatory fields.');
            redirect($this->agent->referrer());
        } else { 
            $reg_num = $this->input->post('registration_number');
            
            $fee_head_total = $this->input->post('fee_head_total');
            $amount_with_comma = $this->input->post('amount');
            $amount = str_replace(',', '', $amount_with_comma);        
            $commission_with_comma = $this->input->post('commission');
            $commission = str_replace(',', '', $commission_with_comma); 
            $connectionString = $this->session->userdata("connectionString");

            //echo "<pre>".$commission."<br>".$amount; print_r($_POST); die;

            if($connectionString == 'clg_db2' || $this->input->post('program')=='bachelor'){

                // ==== LIVE KEYS ====
                $firstSplitArr = array("name"=>$reg_num, "value"=>$fee_head_total, "merchantId"=>"6888893", "description"=>"payment splitting", "commission"=>$commission); // LIVE ACCOUNT
                $MERCHANT_KEY = "qk9XqxqH"; // LIVE ACCOUNT
                $SALT = "fZGlLKDEO9"; // LIVE ACCOUNT
                $PAYU_BASE_URL = "https://secure.payu.in";
                

                // ==== TEST KEYS ====
                // $firstSplitArr = array("name"=>$reg_num, "value"=>$fee_head_total, "merchantId"=>"4825050", "description"=>"payment splitting", "commission"=>$commission); // TEST ACCOUNT
                // $MERCHANT_KEY = "BC50nb"; // TEST ACCOUNT
                // $SALT = "Bwxo1cPe"; // TEST ACCOUNT
                // $PAYU_BASE_URL = "https://test.payu.in";

              } elseif ($connectionString == 'clg_db1' || $this->input->post('program')=='master'){

                // ==== LIVE KEYS ====
                $firstSplitArr = array("name"=>$reg_num, "value"=>$fee_head_total, "merchantId"=>"6891255", "description"=>"payment splitting", "commission"=>$commission); // LIVE ACCOUNT
                $MERCHANT_KEY = "qk9XqxqH"; // LIVE ACCOUNT
                $SALT = "fZGlLKDEO9"; // LIVE ACCOUNT
                $PAYU_BASE_URL = "https://secure.payu.in";
                

               // ==== TEST KEYS ====
               // $firstSplitArr = array("name"=>$reg_num, "value"=>$fee_head_total, "merchantId"=>"4825051", "description"=>"payment splitting", "commission"=>$commission); // TEST ACCOUNT
               //  $MERCHANT_KEY = "BC50nb"; // TEST ACCOUNT
               //  $SALT = "Bwxo1cPe"; // TEST ACCOUNT
               //  $PAYU_BASE_URL = "https://test.payu.in";
            }

           /* $late_fee = $this->input->post('late_fee');
            if (empty($late_fee)) {
                $late_fee = '';
            }*/
    		

            $paymentPartsArr = array($firstSplitArr);   
            $finalInputArr = array("paymentParts" => $paymentPartsArr);
            $Prod_info = json_encode($finalInputArr);
            $txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
            $action = '';

            $productinfo = $Prod_info;
            $email = $this->input->post('email');
            $firstname = $this->input->post('first_name');
            $lastname = $this->input->post('last_name');
            $phone = $this->input->post('phone_number');
            //$c_year = $this->input->post('course_year');
           // $c_spec = $this->input->post('specialisation');
            //$c_type = $this->input->post('course_type');
            $caste = $this->input->post('caste');
            //$ad_year = $this->input->post('admission_year');
            //$quantity = $this->input->post('app_fees_quantity');
           // $student_type = $this->input->post('student_type');
            $student_degree = $this->input->post('program');
            $partial_payment = 0;
            $hostel_mess_payment = 0;
            $cancellation_payment = 0;
            $course_id = 0;
            if(!empty($this->input->post('course_id'))){
                $course_id = $this->input->post('course_id');
            }
            if(!empty($this->input->post('partial_payment'))){
                $partial_payment = $this->input->post('partial_payment');
            }
            if(!empty($this->input->post('hostel_mess_payment'))){
                $hostel_mess_payment = $this->input->post('hostel_mess_payment');
            }
            if(!empty($this->input->post('cancellation_payment'))){
                $cancellation_payment = $this->input->post('cancellation_payment');
            }
            $concate_data = $reg_num.'__'.$caste.'__'.$course_id.'__'.$lastname.'__'.$student_degree.'__'.$partial_payment.'__'.$hostel_mess_payment.'__'.$cancellation_payment;


            $udf1 = $concate_data;
            $udf2 = '';
            $udf3 = '';
            $udf4 = '';
            $udf5 = '';

            $hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
    		
    		$hash = strtolower(hash('sha512', $hashstring));
            $action = $PAYU_BASE_URL . '/_payment';

            $success = base_url() . 'StatusOffline';  
            $fail = base_url() . 'StatusOffline';
            $cancel = base_url() . 'StatusOffline';

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
    		
    		$fp = fopen('./uploads/logs/'.$txnid.'.json', 'w');
            fwrite($fp, json_encode($data));
    		
    		$this->load->view('confirmation', $data);
        }   
    }
}