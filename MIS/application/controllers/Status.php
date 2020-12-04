<?php  defined('BASEPATH') OR exit('No direct script access allowed');

class Status extends CI_Controller {

	public function __construct() {
        parent::__construct();
        $this->load->helper('url');
        $this->load->helper('select_db_helper');
        $this->load->model('student/student');
        $this->load->model('student/course_detail');	
		$this->load->model('office_admin/search_model');	
    }

	public function index() {
		$SALT = "Bwxo1cPe";
		$postdata = $_POST;
		$msg = '';
		$remark = '';
		$balance_amount = '';
    	$partialStudentFlag = 0;
		$PaymentType = 'Full Payment';
		$partialStudent  = array(1111,
                        /* TY Honors */

						/* TY Regular 5690 */ 

						/* TY NRI */

						/* SY Honors */

						/* SY Regular */

						/* SY NRI */
                        
                    );
		
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
			$userID = $d["paymentParts"][0]["name"];//$this->session->userdata('userID');

			// CONDITION FOR PARTIAL PAYMENT STudents
	        if(in_array($userID, $partialStudent)){
	        	$partialStudentFlag = 1;
	        	$remark = 'Partial Payment';
				$balance_amount = $udf1;
	        	$postdata['remark'] = $remark;
	        	$postdata['balance_amount'] = $balance_amount;
				$PaymentType = 'Partial Payment';
	        }

			//echo $this->session->userdata("connectionString");
			/*print_r ($d["paymentParts"][0]["name"]);*/
			$fp = fopen('./uploads/response/api_response_'.$userID.'_'.$txnid.'.json', 'w');
			fwrite($fp, json_encode($postdata));
			
			//Calculate response hash to verify	
			$keyString 	  		=  	$key.'|'.$txnid.'|'.$amount.'|'.$productInfo.'|'.$firstname.'|'.$email.'|'.$udf1.'|||||||||';
			$keyArray 	  		= 	explode("|",$keyString);
			$reverseKeyArray 	= 	array_reverse($keyArray);
			$reverseKeyString	=	implode("|",$reverseKeyArray);
			$CalcHashString 	= 	strtolower(hash('sha512', $salt.'|'.$status.'|'.$reverseKeyString));

			//$userID = $this->session->userdata('userID');
			$result = $this->student->get_entry($userID);
			if ($status == 'success'/*   && $resphash == $CalcHashString */) {
				 
				$caste_category = @$result[0]->caste_category;
				$caste = @$result[0]->caste;
				$data['course_id'] = @$result[0]->course_id;
				$result2 = $this->course_detail->get_entry($data['course_id']);

				$year = @$result2[0]->year;
				$course_name = @$result2[0]->course_name;
				$course_type = @$result2[0]->course_type;
				if(strtolower($caste_category) == 'open'){
					$challan_number = $this->generate_challan_number($year,$course_name,$caste_category);
				} else {
					$challan_number = $this->generate_challan_number($year,$course_name,$caste);
				}	
				if ($partialStudentFlag==1) {							
					$transaction[] = array(
						'student_id'=>$userID,
						'course_id'=>@$result[0]->course_id,
						'category'=>$caste_category,
						'email_id'=>$email,
						'challan_number'=>$challan_number,
						'fee_paid_date'=>date('Y-m-d',strtotime($_POST['addedon'])),
						'firstname'=>@$result[0]->first_name,
						'middlename'=>@$result[0]->middle_name,
						'lastname'=>@$result[0]->last_name,
						'mothername'=>@$result[0]->mother_first_name,
						'payment_mode'=>($mode) ? $mode : '',
						'payment_type'=>'Online',
						'transaction_ref_number'=>$txnid,
						'transaction_status'=>'Paid',
						'total_amount'=>$udf1,
						'balance_amount'=>$balance_amount,
						'late_fee'=>$udf2,
						'remark'=>$remark,
						'total_paid'=>$amount,
					);
				}else{
					$transaction[] = array(
						'student_id'=>$userID,
						'course_id'=>@$result[0]->course_id,
						'category'=>$caste_category,
						'email_id'=>$email,
						'challan_number'=>$challan_number,
						'fee_paid_date'=>date('Y-m-d',strtotime($_POST['addedon'])),
						'firstname'=>@$result[0]->first_name,
						'middlename'=>@$result[0]->middle_name,
						'lastname'=>@$result[0]->last_name,
						'mothername'=>@$result[0]->mother_first_name,
						'payment_mode'=>($mode) ? $mode : '',
						'payment_type'=>'Online',
						'transaction_ref_number'=>$txnid,
						'transaction_status'=>'Paid',
						'total_amount'=>$udf1,
						'late_fee'=>$udf2,
						'total_paid'=>$amount,
					);
				}
				
				$fp = fopen('./uploads/response/response_'.$userID.'.json', 'w');
				fwrite($fp, json_encode($transaction));
				$count = 0;
				$student_transaction = $this->student->get_transaction($userID);
				if (empty($student_transaction)) {
					
					$this->student->insert_transaction($transaction);

					if($status == 'success'){
						/*$message = '';
						$message .= '<table style="width:30%;">
							<tr><td>Name: </td><td>'.$firstname.'</td></tr>
							<tr><td>Email: </td><td>'.$email.'</td></tr>
							<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
							<tr><td>Fee Type: </td><td>Academic</td></tr>
							<tr><td>Year/Course: </td><td>'.$year."/".$course_type.'</td></tr>
							<tr><td>Fee Head Total: </td><td>'.$udf1.'</td></tr>
							<tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
							</table>
							<div style="height:20px;"></div>';
						//$message .= test($data);

						$this->load->library('email');

						$config['mailtype'] = 'html';
						$this->email->initialize($config);
						$this->email->from($email, $firstname);
						$this->email->to('shah.viren1985@gmail.com');
						$this->email->cc('svtcollege2019@gmail.com');
						$this->email->subject('Payment Transaction');
						$this->email->message($message);
						$this->email->send(); */
						
						date_default_timezone_set('Asia/Kolkata');
						$request_date = date('Y-m-d h:m:s A');
						$admission_year = @$result[0]->admission_year;
						$academic_year = @$result[0]->academic_year;
						$current_year = date('Y');
						if($academic_year == $current_year){
							$current_student = "true";
						}else{
							$current_student = "false";
						}
						if(empty($course_name)){
							$course_name = 'Regular';
						}
						$count++;
						$url = 'https://vancotech.com/Exams/bachelors/API/api/student/SubmitOnlineQuery';
						$data = array(
							"FirstName" => $firstname,
							"LastName" =>@$result[0]->last_name,
							"Email" => $email,
							"Mobile" => $phone,
							"RequestDate" => $request_date,
							"PaymentAmount" => $amount,
							"PaymentDate" => $request_date,
							"TransactionNumber" => $txnid,
							"QueryType" => "Transcript Fee",
							"Quantity" => "1",
							"QueryStatus" => "New",
							"AdmissionYear" => @$result[0]->admission_year,
							"Specialisation" => @$result[0]->specialization,
							"Course" => $year.' '.$course_type.'-'.$course_name,
							"CollegeRegistrationNumber" => @$result[0]->userID,
							"CurrentStudent" => $current_student,
							"PaymentType" => $PaymentType,
							'balance_amount'=>$balance_amount,
							"Count" => $count
						);
						
						$fp = fopen('./uploads/response/response_'.$userID.'.json', 'w');
						fwrite($fp, json_encode($data));
						
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
						
						$data['status'] = $status;
						$data['txnid'] =  $txnid;
						$data['amount'] = $amount;
						// redirect to success page
						$result = $this->student->get_entry($userID); 
						$data['course_details'] = $this->course_detail->get_entry(@$result[0]->course_id);
						$data['fee_details'] = $this->search_model->get_fees(@$result[0]->course_id);

						//  GET Fee Receipt and email with attachment and upload it into student Document

						$pay_percentage = $gst = $grand_total = 0;
						$pay_percentage = ($udf1/100) * 2;
						/*if($pay_percentage >= '500'){ 
							$pay_percentage = number_format('500', 2); 
						}else{
							$pay_percentage = number_format($pay_percentage, 2);
						}*/
						$gst = (18/100)*($pay_percentage);
						$online_total = $pay_percentage+$gst;

						$data['student_details'] = @$result;
						$data['fees_form_fields'] = array(
				        	"bank_name"   => '',
		        			"branch_name" => '',
				        	"cheque"      => '',
				        	"cheque_date" => '',
				        	"total_fee"   => $amount,
				        	"college_fee"   => $udf1,
				        	"online_total"   => $online_total,
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
						if ($partialStudentFlag==1) {
							$html = $this->load->view('office_admin/print_reciept_partial' ,$data,true);
						}else{
							$html = $this->load->view('office_admin/print_reciept' ,$data,true);
						}
						$mpdf->WriteHTML($html);
					 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
						$mpdf->Output($pdf_path.'/'.$filename, 'F');
						//$mpdf->Output($filename, 'D');

						$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$filename);
								// your CURL here
								
						$ch = curl_init();					
						curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=".$data['student_details'][0]->admission_year."&crn=".$userID."&docType=Fee-Receipt&fileName=".$filename);
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

						// Send Email to Student of Fee Receipt with attachment
						$stuname = $data['student_details'][0]->first_name.' '.$data['student_details'][0]->middle_name.' '.$data['student_details'][0]->last_name;
				        // Send Email to Student of Fee Receipt with attachment
						//$stuname = $firstname.' '.$lastname;
				        $message = '';
				        if ($partialStudentFlag==1) {

				        	$total_college_fee = $udf1*2;
					        $message .= '<table>
								<tr><td colspan=2>Hello '.$firstname.',</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
								<tr><td>Name: </td><td>'.$stuname.'</td></tr>
								<tr><td>Email: </td><td>'.$email.'</td></tr>
								<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
								<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
								<tr><td>Year/Program: </td><td>'.$year."/".$course_type.'</td></tr>
								<tr><td>Total College Fees : </td><td>'.$total_college_fee.'</td></tr>
								<tr><td>Amount Paid : </td><td>'.$udf1.'</td></tr>
								<tr><td>Balance Amount to be paid : </td><td>'.$udf1.'</td></tr>
								<tr><td>Online Payment Processing Charges: </td><td>'.$online_total.'</td></tr>
								<tr><td>Total Paid: </td><td>'.$amount.'</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous).
								</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
								<tr><td colspan=2>Thank You</td></tr>
								</table>
								<div style="height:20px;"></div>';
						}else{
					        $message .= '<table>
								<tr><td colspan=2>Hello '.$firstname.',</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
								<tr><td>Name: </td><td>'.$stuname.'</td></tr>
								<tr><td>Email: </td><td>'.$email.'</td></tr>
								<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
								<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
								<tr><td>Year/Program: </td><td>'.$year."/".$course_type.'</td></tr>
								<tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous). 
				</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
								<tr><td colspan=2>Thank You</td></tr>
								</table>
								<div style="height:20px;"></div>';
						}

						$email_id = $email; //$data['student_details'][0]->email_id;
						
						// $email_id = "viren.shah1985@gmail.com";	
						//$email_id = "viren.shah1985@gmail.com";	
						$this->load->library('email');

						/* ======== START CUSTOM EMAIL SETTING TO GMALI SERVER ====
						$config = array();
						$config['protocol'] = 'smtp';
						$config['smtp_host'] = 'smtp.googlemail.com';
						$config['smtp_user'] = 'viren.shah1985@gmail.com';
						$config['smtp_pass'] = 'HelloWorld@2013';
						$config['smtp_port'] = 587;
						// ======== END CUSTOM EMAIL SETTING TO GMALI SERVER ====*/
						
						//$this->email->initialize($config);
						$config['mailtype'] = 'html';
						$this->email->initialize($config);
						$this->email->from('svtcollege2019@gmail.com', 'SVT College');
						$this->email->to($email_id);
						$this->email->cc('svtcollege2019@gmail.com');
						//$this->email->cc('shah.viren1985@gmail.com');
						$this->email->subject("SVT: Thank you for fees payment for Academic Year - 2020-21");
						$this->email->message($message);
						$this->email->attach($pdf_path.'/'.$filename);
						$this->email->send();

						  // SEND SMS TO STUDENT
						$apiKey = urlencode('bJoR6cXhY6o-jrhAMKEZmaHH6KF5j2eVu7mMej06Wu');
						$numbers = array($phone);
						$sender = urlencode(' SVTCHS');
						$message = rawurlencode('Thank you '.$firstname.' for Payment of Rs. '.$amount .' for academic fees 2020-21 to SVT College of Home Science (Autonomous).');

						$numbers = implode(',', $numbers);

						// Prepare data for POST request
						$datasms = array('apikey' => $apiKey, 'numbers' => $numbers, "sender" => $sender, "message" => $message);

						// Send the POST request with cURL
						$chsms = curl_init('https://api.textlocal.in/send/');
						curl_setopt($chsms, CURLOPT_POST, true);
						curl_setopt($chsms, CURLOPT_POSTFIELDS, $datasms);
						curl_setopt($chsms, CURLOPT_RETURNTRANSFER, true);
						$responsesms = curl_exec($chsms);
						curl_close($chsms);
						
	                    

						$this->load->view('student/common/header', $data);
						//$this->load->view('student/common/header');
						$this->load->view('success', $data);
						$this->load->view('student/common/footer');
					}
				}
			} else {
				$data['status'] = $status;
				$data['txnid'] =  $txnid;
				$data['amount'] = $amount;
				//$data['course_details'] = $this->course_detail->get_entry( $result[0]->course_id );
				$this->load->view('student/common/header', $data);
				$this->load->view('failure', $data);
				$this->load->view('student/common/footer');
			}			
		} else {
			redirect('student/home');
		}
	}
	
	public function generate_challan_number($year,$name,$caste){
		$challan = $this->student->get_challan_number();
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
			// Sum 1 + last id
			$challan_number = $year.$name_n.$caste_n.'000'.($challan->id + 1);
		} else {
			$challan_number = $year.$name_n.$caste_n.'0001';
		}
		return $challan_number;
	}
 
}