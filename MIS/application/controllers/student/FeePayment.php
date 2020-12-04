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
		$this->load->view('student/common/header', $data);
		$this->load->view('student/fee_payment', $data);
		$this->load->view('student/common/footer');
	}

	// public function check()
	// {

	// 	   $amount =  $this->input->post('amount');
	//     $product_info = 'SVT MIS';
	//     $firstname = $this->input->post('firstname');
	//     $email = $this->input->post('email');
	//     $mobile = $this->input->post('mobile');
	//     $address = 'Lane 5, Dehradun';	

	// 	// $amount =  $this->input->post('amount');
	//  //    $product_info = 'SVT MIS';
	//  //    $firstname = $this->input->post('firstname');
	//  //    $email = $this->input->post('email');
	//  //    $mobile = $this->input->post('mobile');
	//  //    $address = 'Lane 5, Dehradun';	    
 //    	//payumoney details
    
    
 //        $MERCHANT_KEY = "3QsYGn1L"; //change  merchant with yours
 //        $SALT = "7Z5m5kjEu2";  //change salt with yours 

 //        $txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);

 //        //optional udf values 
 //        $udf1 = $this->input->post('fee_type');
 //        $udf2 = $this->input->post('course');
 //        $udf3 = $this->input->post('year');
 //        $udf4 = $this->input->post('caste');
        
 //        $hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $firstname . '|' . $product_info . '|' . $email . '|' . $mobile . '|' . $address . '|' . '||||||' . $SALT;

 //        $hash = strtolower(hash('sha512', $hashstring));
         
 //       	$success = base_url() . 'success';  
 //        $fail = base_url() . 'fail';
 //        $cancel = base_url() . 'cancel';
        
        
 //        $data = array(
 //        	'mkey' => $MERCHANT_KEY,
 //            'tid' => $txnid,
 //            'hash' => $hash, 
 //            'amount' => $amount,
 //            'firstname' => $firstname,
 //            'productinfo' => $product_info, 
 //            'email' => $email,
 //            'mobile' => $mobile,
 //            'action' => "https://test.payu.in", //for live change action  https://secure.payu.in
 //            'sucess' => $success,
 //            'failure' => $fail,
 //            'cancel' => $cancel            
 //        );

 //        //print("<pre>".print_r($data,true)."</pre>");
 //        //die;
 //        $this->load->view('confirmation', $data);   
	// }

	public function check()
	{
		$amount =  $this->input->post('amount');
	    //$product_info = 'SVT MIS';

        $firstSplitArr = array("name"=>"splitID1", "value"=>"6", "merchantId"=>"396446", "description"=>"test description", "commission"=>"2");
        $paymentPartsArr = array($firstSplitArr);   
        $finalInputArr = array("paymentParts" => $paymentPartsArr);
        $product_info = json_encode($finalInputArr);

	    $customer_name = $this->input->post('firstname');
	    $customer_emial = $this->input->post('email');
	    $customer_mobile = $this->input->post('mobile');
    	//payumoney details
    
    
        $MERCHANT_KEY = "gtKFFx"; //change  merchant with yours
        $SALT = "eCwWELxi";  //change salt with yours 
        $txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
        //optional udf values 
        $udf1 = $this->input->post('fee_type');
        $udf2 = $this->input->post('course');
        $udf3 = $this->input->post('year');
        $udf4 = $this->input->post('category');;
        $udf5 = $this->input->post('course_id');;

        
        //print_r($_POST['result']);die;
        // $a = $_POST['result'];
        // $fee_head_udf = count($_POST['result']);
        // $count = 3;
        // foreach ($a as $key => $value) {
        //     $count++;
        //     ${"udf$count"} = $value.'</br>';
           
        // }

        //echo 'udf4'.$udf4;
        
        
        $hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $product_info . '|' . $customer_name . '|' . $customer_emial . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
        $hash = strtolower(hash('sha512', $hashstring));
         
        $success = base_url() . 'Status';  
        $fail = base_url() . 'Status';
        $cancel = base_url() . 'Status';
        
        
        $data = array(
            'mkey' => $MERCHANT_KEY,
            'tid' => $txnid,
            'hash' => $hash,
            'amount' => $amount,           
            'name' => $customer_name,
            'productinfo' => $product_info,
            'mailid' => $customer_emial,
            'phoneno' => $customer_mobile,
            'udf1' => $udf1,
            'udf2' => $udf2,
            'udf3' => $udf3,
            'udf4' => $udf4,
            'udf5' => $udf5,
            'action' => "https://test.payu.in", //for live change action  https://secure.payu.in
            'success' => $success,
            'failure' => $fail,
            'cancel' => $cancel            
        );
        $this->load->view('confirmation', $data);   
     
	}

}
