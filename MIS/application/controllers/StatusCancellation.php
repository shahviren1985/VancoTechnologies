<?php  defined('BASEPATH') OR exit('No direct script access allowed');

class StatusOffline extends CI_Controller {

	public function __construct() {
        parent::__construct();
        $this->load->helper('url');
        $this->load->helper('select_db');
        $this->load->model('student/student');
        $this->load->model('student/course_detail');	
		$this->load->model('student/ExamPaymentModel');	
		$this->load->model('office_admin/search_model');
    }

	public function index() {
		$SALT = "Bwxo1cPe";
		$postdata = $_POST;
		$msg = '';
		$remark = '';
		$balance_amount = '';
    	$partialStudentFlag = 0;
    	$hostel_mess_Flag = 0;
		$PaymentType = 'Full Payment';
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
			$fee_head_total = $d["paymentParts"][0]["value"]; 
			$online_total = $d["paymentParts"][0]["commission"];

			$udf1_array = explode('__',$udf1);
			$form_no = $udf1_array[0];
			$category =  $udf1_array[1]; // Open or Reserved
			$course_id =   $udf1_array[2];		
			$lastname = $udf1_array[3];	
			$student_degree = $udf1_array[4];
			$partial_payment = $udf1_array[5]; // 0=Full, 1=Half payment
			$hostel_mess_payment = $udf1_array[6]; // 0=Full, 1= hostel mess payment

			// CONDITION FOR PARTIAL PAYMENT STudents
	        if($partial_payment==1){
	        	$partialStudentFlag = 1;
	        	$remark = 'Partial Payment';
				$balance_amount = $fee_head_total;
	        	$postdata['remark'] = $remark;
	        	$postdata['balance_amount'] = $fee_head_total;
				$PaymentType = 'Partial Payment';
	        }

			// CONDITION FOR hostel_mess_payment PAYMENT
	        if($hostel_mess_payment==1){
	        	$hostel_mess_Flag = 1;
	        	$remark = 'Hostel Mess Payment';
	        	$postdata['remark'] = $remark;
				$PaymentType = 'Hostel Mess Payment';
	        }

			//echo $this->session->userdata("connectionString");
			/*print_r ($d["paymentParts"][0]["name"]);*/
			$fp = fopen('./uploads/response/api_response_'.$form_no.'_'.$txnid.'.json', 'w');
			fwrite($fp, json_encode($postdata));

			//Calculate response hash to verify	
			$keyString 	  		=  	$key.'|'.$txnid.'|'.$amount.'|'.$productInfo.'|'.$firstname.'|'.$email.'|'.$udf1.'|||||||||';
			$keyArray 	  		= 	explode("|",$keyString);
			$reverseKeyArray 	= 	array_reverse($keyArray);
			$reverseKeyString	=	implode("|",$reverseKeyArray);
			$CalcHashString 	= 	strtolower(hash('sha512', $salt.'|'.$status.'|'.$reverseKeyString));
			if ($status == 'success'/*   && $resphash == $CalcHashString */) {
				
				
				$course_year = 'FY';
				$course_name = 'Regular';
				$academic_year = '2020';
				$admission_year = '2020';
				$division = '';
				$course_type = '';
				if ($category=='Reserved') {
					$caste = 'SC';
				}else {
					$caste = 'Open';
				}
				
				if ($student_degree=='bachelor') {				
					if($course_id == 1){
						$userID = 1001;
						//$course_name = 'DC';
						$specialization = 'Developmental Counseling';
						$division = 'C-I';
					}elseif($course_id == 2){
						$userID = 1002;
						//$course_name = 'ECCE';
						$specialization = 'Early Childhood Care and Education';
						$division = 'C-II';
					}elseif($course_id == 3){
						$userID = 1003;
						//$course_name = 'FND';
						$specialization = 'Food, Nutrition and Dietitics';
						$division = 'B';
					}elseif($course_id == 4){
						$userID = 1004;
						//$course_name = 'HTM';
						$specialization = 'Hospitality & Tourism Management';
						$division = 'A-I';
					}elseif($course_id == 5){
						$userID = 1005;
						//$course_name = 'IDRM';
						$division = 'A-II';
						$specialization = 'Interior Designing and Resource Management';
					}elseif($course_id == 6){
						$userID = 1006;
						//$course_name = 'MCE';
						$division = 'C-III';
						$specialization = 'Mass Communication & Extension';
					}elseif($course_id == 7){
						$userID = 1007;
						//$course_name = 'TAD';
						$specialization = 'Textile & Apparel Designing';
						$division = 'D-II';
					}
				}elseif ($student_degree=='master') {
					if($course_id == 1){
						$userID = 1001;
						//$course_name = 'DC';
						$specialization = 'M.Sc. Specialized Dietetics ';
					}elseif($course_id == 4){
						$userID = 1002;
						//$course_name = 'ECCE';
						$specialization = 'M. Design (Fashion Design)';
					}elseif($course_id == 10){
						$userID = 1003;
						//$course_name = 'FND';
						$specialization = 'Diploma In Fashion Design';
					}elseif($course_id == 11){
						$userID = 1004;
						//$course_name = 'HTM';
						$specialization = 'Certificate in Computer Aided Interior Space Design Management';
					}elseif($course_id == 12){
						$userID = 1005;
						//$course_name = 'IDRM';
						$specialization = 'Diploma in Computer Aided Interior Space Design Management';
					}
				}
				
				if ($student_degree=='bachelor') {
					$databaseName = 'clg_db2';
				}elseif ($student_degree=='master') {
					$databaseName = 'clg_db1';
				}

				if($hostel_mess_payment==1){
					$userID = $form_no; // user entered id
		        }
		        
				$result2 = $this->ExamPaymentModel->fetch_course_detail_FY($course_id,$databaseName);

				if(empty($category)){
					if(strtolower($category) == 'open'){
						$challan_number = $this->generate_challan_number($course_year,$course_name,$category,$databaseName);
					} else {
						$challan_number = $this->generate_challan_number($course_year,$course_name,$caste,$databaseName);
					}
				} else {
					$challan_number = $this->generate_challan_number($course_year,$course_name,$category,$databaseName);
				}				
				
				//$caste = $result[0]->caste;
				//$result2 = $this->course_detail->get_entry($data['course_id']);

				$year = 'FY';
				
				$course_name = (!empty($result2[0]->course_name))? $result2[0]->course_name : $course_name;
				$course_type = @$result2[0]->course_type;

				$transaction[] = array(
					'student_id'=>$userID,
					'course_id'=>$course_id,
					'category'=>$category,
					'email_id'=>$email,
					'challan_number'=>$challan_number,
					'fee_paid_date'=>date('Y-m-d',strtotime($_POST['addedon'])),
					'firstname'=>(!empty(@$firstname))? $firstname : $firstname,
					'middlename'=>'',
					'lastname'=>(!empty(@$lastname))? $lastname : $lastname,
					'mothername'=>'',
					'payment_mode'=>($mode) ? $mode : '',
					'payment_type'=>'Online',
					'transaction_ref_number'=>$txnid,
					'transaction_status'=>'Paid',
					'total_amount'=>$fee_head_total,
					'balance_amount'=>$balance_amount,
					'late_fee'=>0,
					'remark'=>$remark,
					'total_paid'=>$amount,
				);
				//echo "<pre>"; print_r($transaction); die;
				//$fp = fopen('./uploads/response/response_'.$userID.'.json', 'w');
				$fp = fopen('./uploads/response/response_'.$form_no.'.json', 'w');
				fwrite($fp, json_encode($transaction));
				
				
				$student_transaction = $this->ExamPaymentModel->get_transaction_FY($txnid,$databaseName);
				if (empty($student_transaction)) {
					$this->ExamPaymentModel->insert_transaction_FY($transaction,$databaseName);
					if($status == 'success'){
						/*$message = '';
						$message .= '<table style="width:30%;">
							<tr><td>Name: </td><td>'.$firstname.'</td></tr>
							<tr><td>Email: </td><td>'.$email.'</td></tr>
							<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
							<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
							<tr><td>Year/Course: </td><td>'.$year."/".$course_type.'</td></tr>
							<tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
							</table>
							<div style="height:20px;"></div>';
						//$message .= test($data);

						$this->load->library('email');

						$config['mailtype'] = 'html';
						$this->email->initialize($config);
						$this->email->from($email, $firstname);
						$this->email->to('shah.viren1985@gmail.com');
						// $this->email->to('abhishek.apncoders@gmail.com');
						// $this->email->cc('svtcollege2019@gmail.com');
						$this->email->cc('abhishek.apncoders@gmail.com');
						$this->email->subject('SVT: Thank you for fees payment');
						$this->email->message($message);
						$this->email->send(); */
						
						date_default_timezone_set('Asia/Kolkata');
						$request_date = date('Y-m-d h:m:s A');
						//$admission_year = $admission_year;
						//$academic_year = $academic_year;
						$current_year = date('Y');
						if($academic_year == $current_year){
							$current_student = "true";
						}else{
							$current_student = "false";
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
							"QueryType" => "Transcript Fee",
							"Quantity" => "1",
							"QueryStatus" => "New",
							"AdmissionYear" => $academic_year,
							"Specialisation" => $specialization,
							"Course" => $year.' '.$course_type.'-'.$course_name,
							"CollegeRegistrationNumber" => $userID,
							"CurrentStudent" => $current_student,
							"PaymentType" => $PaymentType,
							'balance_amount'=>$balance_amount,
						);
						
						$fp = fopen('./uploads/response/response_'.$form_no.'.json', 'w');
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
											
						$data['course_id'] = $course_id;
						$data['course_details'] = $result2;
						$data2['fee_details'] = $this->ExamPaymentModel->get_fees_FY($course_id,$databaseName);
						
						//$pay_percentage = $gst = $grand_total = 0;
						//$pay_percentage = ($fee_head_total/100) * 2;
						/*if($pay_percentage >= '500'){ 
							$pay_percentage = number_format('500', 2); 
						}else{
							$pay_percentage = number_format($pay_percentage, 2);
						}*/
						//$gst = (18/100)*($pay_percentage);
						//$online_total = $pay_percentage+$gst;

						$data2['fees_form_fields'] = array(
				        	/*"bank_name"   => '',
		        			"branch_name" => '',
				        	"cheque"      => '',
				        	"cheque_date" => '',*/
				        	"total_fee"   => $amount,
				        	"college_fee"   => $fee_head_total,
				        	"online_total"   => $online_total,
							"challan_number"=>$challan_number,
							"caste_category"=>$category,
							"division"=>$division,
							"first_name"=>$firstname,
							"last_name"=>$lastname,
							"specialization"=>$specialization,
							"program"=>$student_degree,
							"course_name"=>$course_name,
				        );
						$mpdf = new \Mpdf\Mpdf([
							'format' => 'A5',
							'orientation' => 'L',
							'default_font_size' => 7,
							'default_font'=>'helvetica'
						]);
						$filename = 'SVT-'.$challan_number.'-'.time().'.pdf';
						//$mpdf->SetHTMLHeader('');
						if ($partialStudentFlag==1) {
							$html = $this->load->view('office_admin/print_reciept_partial_FY' ,$data2,true);
						}else if ($hostel_mess_payment==1) {
							$html = $this->load->view('office_admin/print_reciept_hostel_mess' ,$data2,true);
						} else {
							$html = $this->load->view('office_admin/print_reciept_FY' ,$data2,true);
						}			
						$mpdf->WriteHTML($html);
					 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
						$mpdf->Output($pdf_path.'/'.$filename, 'F');
						//$mpdf->Output($filename, 'D');

						// Send Email to Student of Fee Receipt with attachment
						$stuname = $firstname.' '.$lastname;
				        $message = '';
				        if ($partialStudentFlag==1) {

				        	$total_college_fee = $fee_head_total*2;
					        $message .= '<table>
								<tr><td colspan=2>Hello '.$firstname.',</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
								<tr><td>Name: </td><td>'.$stuname.'</td></tr>
								<tr><td>Email: </td><td>'.$email.'</td></tr>
								<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
								<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
								<tr><td>Year/Program: </td><td>'.$year."/".$course_type.'</td></tr>
								<tr><td>Total College Fees : </td><td>'.$total_college_fee.'</td></tr>
								<tr><td>Making Payment of (50%): </td><td>'.$fee_head_total.'</td></tr>
								<tr><td>Online Payment Processing Charges: </td><td>'.$online_total.'</td></tr>
								<tr><td>Total Paid: </td><td>'.$amount.'</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous).
								</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
								<tr><td colspan=2>Thank You</td></tr>
								</table>
								<div style="height:20px;"></div>';
						}else if ($hostel_mess_payment==1) {
					        $message .= '<table>
								<tr><td colspan=2>Hello '.$firstname.',</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
								<tr><td>Name: </td><td>'.$stuname.'</td></tr>
								<tr><td>Email: </td><td>'.$email.'</td></tr>
								<tr><td>Mobile No.: </td><td>'.$phone.'</td></tr>
								<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
								<tr><td>Year/Program: </td><td>'.$year."/".$course_type.'</td></tr>
								<tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
								<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of Hostel Mess Fees to Sir Vithaldas Thackersey College of Home Science (Autonomous). 
				</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
								<tr><td colspan=2>Thank You</td></tr>
								</table>
								<div style="height:20px;"></div>';
						} else {
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

						$email_id = $email;
						// $email_id = 'abhishek.apncoders@gmail.com';
						//$email_id = "shah.viren1985@gmail.com";	
						//$this->load->library('email');
						$config['mailtype'] = 'html';
						$this->email->initialize($config);
						$this->email->from('svtcollege2019@gmail', 'SVT College');
						$this->email->to($email_id);
						$this->email->cc('svtcollege2019@gmail.com');
						// $this->email->cc('shah.viren1985@gmail.com');
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

						$data['status'] = $status;
						$data['txnid'] =  $txnid;
						$data['amount'] = $amount;
						$data['pdf_download'] = base_url()."uploads/pdf/$filename";

						// redirect to success page
						$this->load->view('student/common/header-FY', $data);
						//$this->load->view('student/common/header');
						$this->load->view('success', $data);
						$this->load->view('student/common/footer');
					}
				}
			} else {
				$data['status'] = $status;
				$data['txnid'] =  $txnid;
				$data['amount'] = $amount;
				$this->load->view('student/common/header-FY', $data);
				$this->load->view('failure', $data);
				$this->load->view('student/common/footer');
			}			
		}
	}
	
	public function generate_challan_number($year,$name,$caste,$databaseName){
		$challan = $this->get_challan_number($databaseName);
		if($caste == 'SC' || $caste == 'ST'){
			$caste_n = 'S';
		} else {
			$caste_n = 'O';
		}
		
		$name_n = 'R';
		
		
		if($challan){
			// Sum 1 + last id
			$challan_number = $year.$name_n.$caste_n.'000'.($challan->id + 1);
		} else {
			$challan_number = $year.$name_n.$caste_n.'0001';
		}
		return $challan_number;
	}

	public function get_challan_number($databaseName){
		$database = '';
		if(empty($databaseName)){
			$connectionString = 'clg_db2';
		}else{
			$connectionString = $databaseName;
		}
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);		
		$query = $db_query->order_by('id',"desc")
            ->limit(1)
            ->get('transaction_details')
            ->row();
		return $query;
	}
 
}