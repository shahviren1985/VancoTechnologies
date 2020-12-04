<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class MiscPaymentStatus extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('url');
		$this->load->model('student/ExamPaymentModel');
	}	
	
	public function index() {
		$SALT = "yIEkykqEH3"; /// mLXPoA2jsp
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
				$userID = $udf1_array[0];
				$course_year = $udf1_array[1];
				$course_spec =  $udf1_array[2];
				$course_name = $udf1_array[3];
				$category =  $udf1_array[4];
				$academic_year = $udf1_array[5];
				$course_id =   $udf1_array[6];		
				$quantity = $udf1_array[7];	
				$curr_student = $udf1_array[8];	
				$lastname = $udf1_array[9];	
				$student_degree = $udf1_array[10];	
				if(!$course_id){
					$course_id = $this->ExamPaymentModel->fetch_course_id($course_year,$course_spec,$course_name);
				}
				$course_detail = $this->ExamPaymentModel->fetch_course_detail($course_id);		
				if(!empty($this->session->userdata('userID'))){
					$result = $this->ExamPaymentModel->get_entry($userID);
				}
				if(empty($category)){
					$category = $result[0]->caste_category;
					$caste = $result[0]->caste;
					if(strtolower($category) == 'open'){
						$challan_number = $this->generate_challan_number($course_year,$course_name,$category);
					} else {
						$challan_number = $this->generate_challan_number($course_year,$course_name,$caste);
					}
				} else {
					$challan_number = $this->generate_challan_number($course_year,$course_name,$category);
				}				
				
				$transaction[] = array(
					'student_id'=>$userID,
					'course_id'=>$course_id,
					'category'=>$category,
					'email_id'=>$email,
					'challan_number'=>$challan_number,
					'fee_paid_date'=>date('Y-m-d',strtotime($_POST['addedon'])),
					'firstname'=>(!empty(@$result[0]->first_name))? $result[0]->first_name : $firstname,
					'middlename'=>@$result[0]->middle_name,
					'lastname'=>(!empty(@$result[0]->last_name))? $result[0]->last_name : $lastname,
					'mothername'=>@$result[0]->mother_first_name,
					'payment_mode'=>($mode) ? $mode : '',
					'payment_type'=>'Online',
					'transaction_ref_number'=>$txnid,
					'transaction_status'=>'Paid',
					'total_amount'=>$amount,
					'remark'=>$productInfo,
					'phone'=>$phone,
					'total_paid'=>@number_format($net_amount_debit,2),
				);
				
				//$this->ExamPaymentModel->insert_transaction($transaction);
				$fp = fopen('./uploads/logs/response_'.$txnid.'.json', 'w');
                fwrite($fp, json_encode($transaction));
	 		
				if($status == 'success'){
					$message = '';
					$message .= '<table style="width:30%;">
						<tr><td>Name: </td><td>'.$firstname.'</td></tr>
						<tr><td>Email: </td><td>'.$email.'</td></tr>
						<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
						<tr><td>Fee Type: </td><td>Academic</td></tr>
						<tr><td>Year/Course: </td><td>'.$course_detail[0]->year."/".$course_detail[0]->course_type.'</td></tr>
						<tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
						</table>
						<div style="height:20px;"></div>';
					$this->load->library('email');
					$config['mailtype'] = 'html';
					$this->email->initialize($config);
					$this->email->from($email, $firstname);
					$this->email->to('shah.viren1985@gmail.com');
					$this->email->subject('Payment Transaction');
					$this->email->message($message);
					$this->email->send();
					
					date_default_timezone_set('Asia/Kolkata');
					$request_date = date('Y-m-d h:m:s A');
					$current_year = date('Y');
					if($academic_year == $current_year){
						$current_student = "true";
					}else{
						$current_student = "false";
					}
					if($student_degree == 'bachelor'){
						$course_type = $course_name;
					}else{
						$coursename = 'Regular';
					}
					
					if($course_spec == 'DC'){
						$course_spec = 'Developmental Counseling';
					}elseif($course_spec == 'ECCE'){
						$course_spec = 'Early Childhood Care and Education';
					}elseif($course_spec == 'FND'){
						$course_spec = 'Food, Nutrition and Dietitics';
					}elseif($course_spec == 'HTM'){
						$course_spec = 'Hospitality & Tourism Management';
					}elseif($course_spec == 'IDRM'){
						$course_spec = 'Interior Designing and Resource Management';
					}elseif($course_spec == 'MCE'){
						$course_spec = 'Mass Communication & Extension';
					}elseif($course_spec == 'TAD'){
						$course_spec = 'Textile & Apparel Designing';
					}elseif($course_spec == 'DCN'){
						$course_spec = 'Diabetes & Cardiac Nutrition';	
					}elseif($course_spec == 'RN'){
						$course_spec = 'Renal Nutrition';	
					}elseif($course_spec == 'PN'){
						$course_spec = 'Pediatric Nutrition';	
					}elseif($course_spec == 'FD'){
						$course_spec = 'Fashion Design';	
					}elseif($course_spec == 'Regular'){
						$course_spec = 'Regular';	
					}
					
					$url = 'https://vancotech.com/Exams/bachelors/API/api/student/SubmitOnlineQuery';
					$data = array(
						"FirstName" => $firstname,
						"LastName" =>$lastname,
						"Email" => $email,
						"Mobile" => $phone,
						"RequestDate" => $request_date,
						"PaymentAmount" => $amount,
						"PaymentDate" => $request_date,
						"TransactionNumber" => $txnid,
						"QueryType" => $productInfo,
						"Quantity" => $quantity,
						"QueryStatus" => "New",
						"AdmissionYear" => $academic_year,
						"Specialisation" => $course_spec,
						"Course" => $course_year.' '.$course_spec.'-'.$course_type,
						"CollegeRegistrationNumber" => $userID,
						"CurrentStudent" => $current_student
					);
					
					$data_string = json_encode($data);
					$ch=curl_init($url);
					curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");                                                                     
					curl_setopt($ch, CURLOPT_POSTFIELDS, $data_string);                                                                  
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);                                                                      
					curl_setopt($ch, CURLOPT_HTTPHEADER, array(                                                                          
						'Content-Type: application/json',                                                                                
						'Content-Length: ' . strlen($data_string))                                                                       
					);

					$result = curl_exec($ch);
					curl_close($ch);
				} 
				

				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$data['course_details'] = $this->ExamPaymentModel->fetch_course_detail($course_id);
				// redirect to success page
				if($this->session->userdata("id") != null){
					$this->load->view('student/common/header', $data);
				} else {
					$this->load->view('student/common/header-mis', $data);
				}
				$this->load->view('success', $data);
				$this->load->view('student/common/footer');				
			} else {			
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				if($this->session->userdata("id") != null){
					$this->load->view('student/common/header');
				} else {
					$this->load->view('student/common/header-mis');
				}
				$this->load->view('failure', $data);
				$this->load->view('student/common/footer');		
			}
		}    
	}
	
	public function generate_challan_number($year,$name,$caste){
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
	}

}