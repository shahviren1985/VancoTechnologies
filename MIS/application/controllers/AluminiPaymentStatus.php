<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class AluminiPaymentStatus extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('url');
		$this->load->model('Alumini_model');
	}	
	
	public function index() {
		$SALT = "mLXPoA2jsp";//;"yIEkykqEH3";
		//$SALT = "yIEkykqEH3";
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
				$landline = $udf1_array[0];
				$photo = $udf1_array[1];
				$dob =  $udf1_array[2];
				$age = $udf1_array[3];
				$specialization = $udf1_array[4];
				$year = $udf1_array[5];
				$extra_activity = $udf1_array[6];
				$alumini_message = $udf1_array[7];
				$signature = $udf1_array[8];
				$receipt_number = $udf1_array[9];
				$date_of_payment = $udf1_array[10];
				$residential_address = $udf1_array[11];
				$alumini_name = $udf1_array[12];
				$contact_details = $udf1_array[13];
				$alumini_email_address = $udf1_array[14];
				$alumini_mobile_number = $udf1_array[15];
				$alumini_mate_id = $udf1_array[16];			
				
				$data[] = array(
					'name' => $firstname,
					'residential_address' => $residential_address,
					'phone_number' => $phone,
					'landline' => $landline,
					'email_address' => $email,
					'dob' => $dob,
					'age' => $age,
					'department' => $productInfo,
					'specialization' => $specialization,
					'year_of_passing' => $year,
					'extra_activity' => $extra_activity,
					'alumini_message' => $alumini_message,
					'photo' => $photo,
					'signature' => $signature,
					'receipt_number' => $receipt_number,
					'date_of_payment' => $date_of_payment
				);
				
				//$this->Alumini_model->insert_alumini_registration_data($data);
				
				$fp = fopen('./uploads/logs/response_'.$udf1_array[9].'.json', 'w');
                fwrite($fp, json_encode($data));
                
				if($alumini_mate_id > 0){
					for($i=1; $i<= $alumini_mate_id; $i++){
						if($i == 1){
							if($alumini_name != ''){
								$alumini_data = array(
									'name' => $alumini_name,
									'email' => $alumini_email_address,
									'address' => $contact_details,
									'mobile' => $alumini_mobile_number
								);
								$alumini_data = $this->security->xss_clean($alumini_data);
								$this->Alumini_model->insert_alumini_mate_details($alumini_data);
								
								$fp = fopen('./uploads/logs/mates_'.$udf1_array[9].'.json', 'w');
                                fwrite($fp, json_encode($data));
							}
							$j = 16;
						}else{
							$alumini_data = array(
								'name' => $udf1_array[$j+1],
								'address' => $udf1_array[$j+2],
								'email' => $udf1_array[$j+3],
								'mobile' => $udf1_array[$j+4]
							);
							$j = $j + 4;
							$alumini_data = $this->security->xss_clean($alumini_data);
							$this->Alumini_model->insert_alumini_mate_details($alumini_data);
							
							//$fp = fopen('./uploads/mates/'.$receipt_number.$j.'.json', 'w');
                            $fp = fopen('./uploads/logs/mates_'.$udf1_array[9].'_'.$j.'.json', 'w');
                            fwrite($fp, json_encode($data));
						}
					}
					
				}

				$transaction_id = $this->Alumini_model->get_transaction_number_alumini_registration();
				$trans_id = array();	
				foreach($transaction_id as $taxid){
					$trans_id[] = $taxid->transaction_ref_number;
				}
				if(!in_array($txnid,$trans_id)){
					
					$last_id = $this->Alumini_model->insert_alumini_registration_data($data);

					$transaction[] = array(
						'email_id'=> $email,
						'alumini_registration_id' => $last_id,
						'fee_paid_date'=> date('Y-m-d'),
						'name'=> $firstname,
						'payment_mode'=>($mode) ? $mode : '',
						'payment_type'=> 'Online',
						'transaction_ref_number'=> $txnid,
						'transaction_status'=> 'Paid',
						'total_amount'=> $amount,
						'remark'=> $productInfo,
						'total_paid'=> @number_format($net_amount_debit,2),
						'mobile'=> $phone,
						'date_of_payment' => $date_of_payment
					);
					
					$this->Alumini_model->insert_transaction($transaction);			
					
					
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
					$this->email->subject('SVT Alumni Registration');
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
					$this->email->subject('Alumni Registration');
					$this->email->message($message_alumni_association);
					$this->email->send();
				}

				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$data['alumini_mate_id'] = $alumini_mate_id;
				$data['alumini_name'] = $alumini_name;
				$data['alumini_email_address'] = $alumini_email_address;
				$data['contact_details'] = $contact_details;
				$data['alumini_mobile_number'] = $alumini_mobile_number;
				$data['udf1_array'] = $udf1_array;
				// redirect to success page
				$this->load->view('alumini-success', $data);		
			}else{			
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