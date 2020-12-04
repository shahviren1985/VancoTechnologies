<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class AluminiRegistrationStatus extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('url');
		$this->load->model('Alumini_model');
	}	
	
	public function index() {
		//$SALT = "yIEkykqEH3";
		$SALT = "mLXPoA2jsp";//;"yIEkykqEH3";
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
			$mihpayid			=	$postdata['mihpayid'];
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
			if ($status == 'success'  && $resphash == $CalcHashString) {
				
				$udf1_array = explode('__',$udf1);
				$specialization = $udf1_array[0];
				$year = $udf1_array[1];
				$current_org = $udf1_array[2];
				$current_designation = $udf1_array[3];
				$total_exp = $udf1_array[4];
				$residential_address = $udf1_array[5];
				$name_of_qualification = $udf1_array[6];
				$college_university_name = $udf1_array[7];	
				$student_field = $udf1_array[8];	
				$higher_education_status = $udf1_array[9];	
				
				
				$data[] = array(
					'name' => $firstname,
					'phone_number' => $phone,
					'email_address' => $email,
					'residential_address' => $udf1_array[5],
					'student_field' => $student_field,
					'specialization' => $specialization,
					'year_of_passing' => $year,
					'working_status' => $status,
					'current_org' => $current_org,
					'current_designation' => $current_designation,
					'total_exp' => $total_exp,
					'higher_education_status' => $higher_education_status,
					'name_of_qualification' => $name_of_qualification,
					'college_university_name' => $college_university_name
				);
				
				$transaction_id = $this->Alumini_model->get_transaction_number();
				$trans_id = array();	
				foreach($transaction_id as $taxid){
					$trans_id[] = $taxid->transaction_ref_number;
				}
				if(!in_array($txnid,$trans_id)){
					$last_id = $this->Alumini_model->alumini_event_registration($data);	
				
					$fp = fopen('./uploads/response/response_'.$txnid.'.json', 'w');
					fwrite($fp, json_encode($data));
					
					//$last_id = $this->db->insert_id();
					//die("last id = ".$last_id);
					$transaction[] = array(
						'email_id'=> $email,
						'alumini_event_registration_id' =>$last_id,
						'name'=> $firstname,
						'fee_paid_date'=> date('Y-m-d'),
						'payment_mode'=>($mode) ? $mode : '',
						'payment_type'=> 'Online',
						'transaction_ref_number'=> $txnid,
						'transaction_status'=> 'Paid',
						'total_amount'=> $amount,
						'remark'=> $productInfo,
						'total_paid'=> @number_format($net_amount_debit,2),
						'mobile'=> $phone
					);
					$this->Alumini_model->insert_alumini_event_registration_transaction($transaction);			
					
					$fp = fopen('./uploads/response/transaction_'.$txnid.'.json', 'w');
					fwrite($fp, json_encode($transaction));	

					$message = '';
					$message .= '<table>
						<tr><td colspan=2>Hello '.$firstname.',</td></tr>
						<tr><td colspan=2 style="padding-bottom:15px;">Thank you for registration for SVT Alumni Association Event. Please find your payment details as mentioned below:</td></tr>
						<tr><td>Name: </td><td>'.$firstname.'</td></tr>
						<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
						<tr><td>Address: </td><td>'.$residential_address.'</td></tr>
						<tr><td>Payment Amount: </td><td>'.$amount.'</td></tr>
						<tr><td>Transaction Id: </td><td>'.$txnid.'</td></tr>
						<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please reach out to us on svtcollege2019@gmail.com in case of any queries. </td></tr>
						<tr><td colspan=2>Thank You,</td></tr>
						<tr><td colspan=2>SVT Alumni Association.</td></tr>
						</table>
						<div style="height:20px;"></div>';
					$this->load->library('email');
					$config['mailtype'] = 'html';
					$this->email->initialize($config);
					$this->email->from('svtcollege2019@gmail', 'SVT College');
					$this->email->to($email);
					$this->email->cc('svtcollege2019@gmail.com');
					$this->email->subject('SVT Alumni Event Registration');
					$this->email->message($message);
					$this->email->send();
					
					
					$message_alumni_association = '';
					$message_alumni_association .= '<table>
						<tr><td colspan=2>Hello Admin,</td></tr>
						<tr><td colspan=2 style="padding-bottom:15px;">'.$firstname.' is registered with our Alumni Association Program. Please find payment details as mentioned below:</td></tr>
						<tr><td>Name: </td><td>'.$firstname.'</td></tr>
						<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
						<tr><td>Address: </td><td>'.$residential_address.'</td></tr>
						<tr><td>Payment Amount: </td><td>'.$amount.'</td></tr>
						<tr><td style="padding-bottom:15px;">Transaction Id: </td><td>'.$txnid.'</td></tr>
						<tr><td colspan=2>Thank You,</td></tr>
						<tr><td colspan=2>'.$firstname.'</td></tr>
						</table>
						<div style="height:20px;"></div>';
					$this->load->library('email');
					$config['mailtype'] = 'html';
					$this->email->initialize($config);
					$this->email->from($email, 'SVT College');
					$this->email->to('shah.viren1985@gmail.com');
					$this->email->cc('svtcollege2019@gmail.com');
					$this->email->subject('Alumni Event Registration');
					$this->email->message($message_alumni_association);
					$this->email->send();
				
				}
				
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				// redirect to success page
				$this->load->view('alumini-success', $data);		
			}else {			
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$this->load->view('alumini-failure', $data);
			}
		}    
	}
	
/* 	public function generate_challan_number($year,$name,$caste){
		$challan = $this->ExamPaymentModel->get_challan_number();
		if($caste == 'SC' || $caste == 'ST'){
			$caste_n = 'S';
		} else {
			$caste_n = 'O';
		}
		if($name == 'Regular' || $name == ''){
			$name_n = 'R';
		} else {
			$name_n = 'H';
		}
		
		if($challan){
			// Sum 1 + last id
			$challan_number = $year.$name_n.$caste_n.'000'.($challan->id + 1);
		} else {
			$challan_number = $year.$name_n.$caste_n.'0001';
		}
		return $challan_number;
	} */

}