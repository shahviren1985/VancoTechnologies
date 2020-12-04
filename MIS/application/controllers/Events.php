<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Events extends CI_Controller {

	public function __construct(){
		parent::__construct();
		if( $this->session->userdata("logged_in") ) {
			if($this->session->userdata("role") != '3'){
				redirect(site_url(), 'refresh');
			}
        }
		$this->load->helper('form');
		$this->load->library('form_validation');
		$this->load->model('Events_model');
	}
	
	public function index() {
		$data['transaction_id'] = $this->Events_model->get_transaction_number();
		$this->load->view('svt-events', $data);
	}

	public function events_payment() {		
		$this->form_validation->set_rules('name', 'Name', 'required');
		$this->form_validation->set_rules('residential_address', 'Residential Address', 'required');
		$this->form_validation->set_rules('institute', 'Institute', 'required');
		$this->form_validation->set_rules('designation', 'Designation', 'required');
		$this->form_validation->set_rules('total_amount', 'Total Amount', 'required');
		$this->form_validation->set_rules('email_address', 'Email Address', 'trim|required|valid_email');
		$this->form_validation->set_rules('phone_number', 'Mobile Number', 'trim|required|numeric');
		
		if($this->form_validation->run() === TRUE){	
		    $fees_amount = $this->input->post('fees_amount');
			$amount = $this->input->post('total_amount');
			if($amount == 310.00){
			
        			$MERCHANT_KEY = "7aizVQa5";//"hDkYGPQe";
        			//$SALT = "Bwxo1cPe";
        			$SALT = "mLXPoA2jsp";//"yIEkykqEH3";
        			// End point - change to https://secure.payu.in for LIVE mode
        			
        			$PAYU_BASE_URL = "https://secure.payu.in";//"https://test.payu.in";
        			$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
        			$action = '';
        			$productinfo = $this->input->post('product_info');
        			
        			$firstname = $this->input->post('name');
        			$lastname = '';
        			$address = $this->input->post('residential_address');
        			
        			$email = $this->input->post('email_address');
        			$phone = $this->input->post('phone_number');
        			$landline = $this->input->post('landline');

        			$institute = $this->input->post('institute');
        			$designation = $this->input->post('designation');
        			//$receipt_number = $this->input->post('receipt-number');
        			//$date_of_payment = $this->input->post('date-of-payment');
        			
        			$concate_data = $landline.'__'.$institute.'__'.$designation.'__'.$address;
        						
        			$udf1 = $concate_data;
        			$udf2 = '';
        			$udf3 = '';
        			$udf4 = '';
        			$udf5 = '';
        			$udf6 = '';
        
        			/*$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1  . '|||||||||' . $SALT;
        			*/
        			$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;

        			$hash = strtolower(hash('sha512', $hashstring));
        			$action = $PAYU_BASE_URL . '/_payment';			

        			$success = base_url() . 'EventPaymentStatus';
        			$fail = base_url() . 'EventPaymentStatus';
        			$cancel = base_url() . 'EventPaymentStatus';
        
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

						$action = $PAYU_BASE_URL . '/_payment';			

						$fp = fopen('./uploads/logs/events/'.$txnid.'.json', 'w');
						fwrite($fp, json_encode($data));
						
						$this->load->view('eventconfirmation', $data);
					}else{
						$this->session->set_flashdata('error', 'Please check amount.');
						$this->index();
					}
		}else{
			$this->session->set_flashdata('error', 'Fill all mandatory fields.');
            $this->index();
		}
	}
	
	
	public function refund_policy(){
		$this->load->view('refund-policy');
	}
}?>