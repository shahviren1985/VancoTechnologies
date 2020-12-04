<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class SubjectPaymentStatus extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('url');
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
		$this->load->model('student/ExamPaymentModel');
	}	
	
	public function index() {
		$SALT = "yIEkykqEH3";		
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
				$student_field = $udf1_array[0];
				$semester = $udf1_array[1];
				$spec_shortform =  $udf1_array[2];
				$subject = $udf1_array[3];
				$userID = $this->session->userdata('userID');
				$result = $this->student->get_entry($userID);
				$course_detail = $this->course_detail->get_entry( $result[0]->course_id );
				$caste = $result[0]->caste;
				$caste_category = $result[0]->caste_category;
				if(strtolower($caste_category) == 'open'){
					$challan_number = $this->generate_challan_number($result[0]->course_name,$course_detail[0]->course_name,$caste_category);
				} else {
					$challan_number = $this->generate_challan_number($result[0]->course_name,$course_detail[0]->course_name,$caste);
				}
				
				$transaction[] = array(
					'student_id'=>$userID,
					'course_id'=>$result[0]->course_id,
					'category'=>$caste_category,
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
					'total_paid'=>@number_format($net_amount_debit,2),
				);
				
				$this->ExamPaymentModel->insert_transaction($transaction);						
				$student_field = $udf1_array[0];
				$semester = $udf1_array[1];
				$spec_shortform =  $udf1_array[2];
				$subject = $udf1_array[3];
				$message = '';
				$message .= '<table style="width:30%;">
					<tr><td>Name: </td><td>'.$firstname.'</td></tr>
					<tr><td>Email: </td><td>'.$email.'</td></tr>
					<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
					<tr><td>Fee Type: </td><td>Addon Subject Fee</td></tr>
					<tr><td>Student Type: </td><td>'.ucfirst($student_field).'</td></tr>
					<tr><td>Semester: </td><td>'.ucfirst($semester).'</td></tr>
					<tr><td>Specialization Code: </td><td>'.$spec_shortform.'</td></tr>
					<tr><td>Subject Code: </td><td>'.$subject.'</td></tr>
					<tr><td>Year/Course: </td><td>'.$course_detail[0]->year."/".$course_detail[0]->course_type.'</td></tr>
					<tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
					</table>
					<div style="height:20px;"></div>';
				$this->load->library('email');
				$config['mailtype'] = 'html';
				$this->email->initialize($config);
				$this->email->from($email, $firstname);
				$this->email->to('hyderextrovert1@gmail.com');
				$this->email->subject('Addon Subject Payment Transaction');
				$this->email->message($message);
				$this->email->send();		

				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$data['semester'] = $semester;
				$data['subject'] = $subject;
				$data['spec'] = $spec_shortform;
				// redirect to success page
				$this->load->view('student/common/header');
				$this->load->view('success', $data);
				$this->load->view('student/common/footer');				
			} else {			
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$this->load->view('student/common/header');
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
			$challan_number = $year.$name_n.$caste_n.'000'.($challan->id + 1);
		} else {
			$challan_number = $year.$name_n.$caste_n.'0001';
		}
		return $challan_number;
	}

}