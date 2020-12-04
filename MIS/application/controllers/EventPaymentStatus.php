<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class EventPaymentStatus extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('url');
		$this->load->model('Events_model');
	}	
	
	public function index() {
	    $SALT = "mLXPoA2jsp";//"yIEkykqEH3"; //"mLXPoA2jsp";

		$postdata = $_POST;
		$msg = '';
		if (isset($postdata ['key'])) {
			$key				=   $postdata['key'];
			$salt				=   $SALT;
			$txnid 				= 	$postdata['txnid'];
			$amount      		= 	$postdata['amount'];
			$productInfo  		= 	$postdata['productinfo'];
			$firstname    		= 	$postdata['firstname'];
			$lastname    		= 	$postdata['lastname'];
			
			$email        		=	$postdata['email'];
			$phone        		=	$postdata['phone'];
			$udf1				=   $postdata['udf1'];
            //$mihpayid			=	$postdata['mihpayid'];
			$status				= 	$postdata['status'];
			$resphash			= 	$postdata['hash'];
			$mode				= 	$postdata['mode'];
			$net_amount_debit	= 	@$postdata['net_amount_debit'];
			
			//Calculate response hash to verify	
			$keyString 	  		=  	$key.'|'.$txnid.'|'.$amount.'|'.$productInfo.'|'.$firstname.'|'.$email.'|'.$udf1.'|||||||||';
			$keyArray 	  		= 	explode("|",$keyString);
			$reverseKeyArray 	= 	array_reverse($keyArray);
			$reverseKeyString	=	implode("|",$reverseKeyArray);
			$CalcHashString 	= 	strtolower(hash('sha512', $salt.'|'.$status.'|'.$reverseKeyString));
			
			$udf1_array = explode('__',$udf1);
				$landline = $udf1_array[0];
				$institute = $udf1_array[1];
				$desgination = $udf1_array[2];
				//$receipt_number = $udf1_array[3];
				//$date_of_payment = $udf1_array[4];
				$address = $udf1_array[3];
				$eventname = $productInfo;
				
				//$residential_address = $udf1_array[11];
				//$contact_details = $udf1_array[13];

				$data[] = array(
					'name' => $firstname,
					'eventname'=>$eventname,
					'residential_address' => $address,
					'phone_number' => $phone,
					'landline' => $landline,
					'email_address' => $email,
					'designation' => $desgination,
					'institute' => $institute
				);
				
				//$this->Event_model->insert_event_registration_data($data);
				
				$fp = fopen('./uploads/logs/events/response_'.$txnid.'.json', 'w');
                fwrite($fp, json_encode($data));
			
			if ($status == 'success'  && $resphash == $CalcHashString) {
				$transaction_id = $this->Events_model->get_transaction_number_event_registration();
				$trans_id = array();	
				foreach($transaction_id as $taxid){
					$trans_id[] = $taxid->transaction_ref_number;
				}
				if(!in_array($txnid,$trans_id)){
					
					$last_id = $this->Events_model->insert_event_registration_data($data);

					$transaction[] = array(
						'name'=> $firstname,
						'mobile'=> $phone,
						'email_id'=> $email,
						'eventname'=> $productInfo,
						'event_registration_id' => $last_id,
						'fee_paid_date'=> date('Y-m-d'),
						'payment_mode'=>($mode) ? $mode : '',
						'payment_type'=> 'Online',
						'transaction_ref_number'=> $txnid,
						'transaction_status'=> 'Paid',
						'total_amount'=> $amount,
						'remark'=> $productInfo,
						'total_paid'=> @number_format($net_amount_debit,2)
						
						//'date_of_payment' => $date_of_payment
					);
					
					$this->Events_model->insert_transaction($transaction);			
				}

				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				//$data['contact_details'] = $contact_details;
				$data['udf1_array'] = $udf1_array;
				$this->load->view('event-success', $data);		
			}else{			
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$this->load->view('event-failure', $data);
			}
		}    
	}
}