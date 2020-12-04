<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class SubjectPaymentStatus extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('url');
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
		$this->load->model('student/ExamPaymentModel');
		$this->load->model('office_admin/search_model');
	}	
	
	public function index() {
		//$SALT = "yIEkykqEH3";		
		$SALT = "Bwxo1cPe";		
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
			$udf2				=   $postdata['udf2'];
			$mihpayid			=	$postdata['mihpayid'];
			$status				= 	$postdata['status'];
			$resphash			= 	$postdata['hash'];
			$mode				= 	$postdata['mode'];
			$net_amount_debit	= 	@$postdata['net_amount_debit'];

			$d = json_decode($productInfo,true);
			$userID = $d["paymentParts"][0]["name"];

			//Calculate response hash to verify	
			$keyString 	  		=  	$key.'|'.$txnid.'|'.$amount.'|'.$productInfo.'|'.$firstname.'|'.$email.'|'.$udf1.'|||||||||';
			$keyArray 	  		= 	explode("|",$keyString);
			$reverseKeyArray 	= 	array_reverse($keyArray);
			$reverseKeyString	=	implode("|",$reverseKeyArray);
			$CalcHashString 	= 	strtolower(hash('sha512', $salt.'|'.$status.'|'.$reverseKeyString));			
			if ($status == 'success' /* && $resphash == $CalcHashString*/) {
				$udf2_array = explode('__',$udf2);
				$subject_fees = $udf2_array[0];
				$student_field = $udf2_array[1];
				$semester = $udf2_array[2];
				$spec_shortform =  $udf2_array[3];
				$subject = $udf2_array[4];
				$credit = $udf2_array[5];
				$subject_id = $udf2_array[6];

				//$userID = $this->session->userdata('userID');
				$result = $this->student->get_entry($userID);
				$course_detail = $this->course_detail->get_entry( $result[0]->course_id );
				$caste = $result[0]->caste;
				$caste_category = $result[0]->caste_category;
				
				$year = $course_detail[0]->year;
				$course_name = $course_detail[0]->course_name;
				$course_type = $course_detail[0]->course_type;
				
				$remark = 'Addon Subject Fees-'.$subject;

				if(strtolower($caste_category) == 'open'){
					$challan_number = $this->generate_challan_number($year,$course_detail[0]->course_name,$caste_category);
				} else {
					$challan_number = $this->generate_challan_number($year,$course_detail[0]->course_name,$caste);
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
					'total_amount'=>$subject_fees,
					'remark'=>$remark,
					'total_paid'=>$amount,
				);
				
				$fp = fopen('./uploads/response/response_'.$userID.'.json', 'w');
				fwrite($fp, json_encode($transaction));

				$student_transaction = $this->student->get_transaction_by_transactionId($userID);
				
				if (empty($student_transaction)) {
					
					$this->ExamPaymentModel->insert_transaction($transaction);
					$this->student->update_addon_subject_by_subject_id($subject_id);
					if($status == 'success'){
					
				/* 	$message = '';
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
					$this->email->to('shah.viren1985@gmail.com');
					$this->email->subject('Addon Subject Payment Transaction');
					$this->email->message($message);
					$this->email->send();	 */	
				
					date_default_timezone_set('Asia/Kolkata');
					$request_date = date('Y-m-d h:m:s A');
					$admission_year = $result[0]->admission_year;
					$academic_year = $result[0]->academic_year;
					$current_year = date('Y');
					if($academic_year == $current_year){
						$current_student = "true";
					}else{
						$current_student = "false";
					}
					if(empty($course_name)){
						$course_name = 'Regular';
					}
					
					$url = 'https://vancotech.com/Exams/bachelors/API/api/student/SubmitOnlineQuery';
					$data = array(
						"FirstName" => $firstname,
						"LastName" =>$result[0]->last_name,
						"Email" => $email,
						"Mobile" => $phone,
						"RequestDate" => $request_date,
						"PaymentAmount" => $amount,
						"PaymentDate" => $request_date,
						"TransactionNumber" => $txnid,
						"QueryType" => $subject,
						"Quantity" => 1,
						"QueryStatus" => "New",
						"AdmissionYear" => $result[0]->admission_year,
						"Specialisation" => $result[0]->specialization,
						"Course" => $year.' '.$course_type.'-'.$course_name,
						"CollegeRegistrationNumber" => $result[0]->userID,
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

					$resultj = curl_exec($ch);
					curl_close($ch);

					//  GET Fee Receipt and email with attachment and upload it into student Document

						$pay_percentage = $gst = $grand_total = 0;
						$pay_percentage = ($subject_fees/100) * 2;
						$gst = (18/100)*($pay_percentage);
						$online_total = $pay_percentage+$gst;

						$data['student_details'] = @$result;
						$data['fees_form_fields'] = array(
				        	"bank_name"   => '',
		        			"branch_name" => '',
				        	"cheque"      => '',
				        	"cheque_date" => '',
				        	"total_fee"   => $amount,
				        	"college_fee"   => $subject_fees,
				        	"online_total"   => $online_total,
							"subject_name"=>$subject,
							"semester"=>$semester,
							"credit"=>$credit,
							"challan_number"=>$challan_number,
				        );
						$mpdf = new \Mpdf\Mpdf([
							'format' => 'A5',
							'orientation' => 'L',
							'default_font_size' => 7,
							'default_font'=>'helvetica'
						]);
						$filename = 'SVT-'.$challan_number.'-'.time().'.pdf';
						//$mpdf->SetHTMLHeader('');			
						//print_r ($data);
						$html = $this->load->view('office_admin/print_reciept_addon' ,$data,true);
						$mpdf->WriteHTML($html);
					 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
						$mpdf->Output($pdf_path.'/'.$filename, 'F');
						//$mpdf->Output($filename, 'D');

						$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$filename);
								// your CURL here
								
						$ch = curl_init();					
						curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=".$data['student_details'][0]->admission_year."&crn=".$userID."&docType=Addon-Subject-Fee-Receipt&fileName=".$filename);
						curl_setopt($ch, CURLOPT_POST, 1);
						//curl_setopt($ch, CURLOPT_POSTFIELDS, array('name' => 'pdf', 'file' => $pdf_path.'/'.$filename));
						curl_setopt($ch, CURLOPT_POSTFIELDS, ['pdf' => $pdf_file]);					
						curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
						$server_output = curl_exec($ch);
						if(curl_errno($ch))
						{
							echo 'Request Error:' . curl_error($ch);
						}					
						curl_close ($ch);	

				
					}
				}
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$data['semester'] = $semester;
				$data['subject'] = $subject;
				$data['spec'] = $spec_shortform;
				// redirect to success page
				$data['course_details'] = $course_detail;
				$this->load->view('student/common/header', $data);
				$this->load->view('success', $data);
				$this->load->view('student/common/footer');				
			} else {			
				$data['status'] = $status;
				$data['amount'] = $amount;
				$data['txnid'] = $txnid;
				$data['course_details'] = $this->course_detail->get_entry( $result[0]->course_id );
				$this->load->view('student/common/header', $data);
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
		if(empty($name)){
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