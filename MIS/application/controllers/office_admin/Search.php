<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Search extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
		$this->load->helper('select_db');
		$this->load->library('form_validation');
		$this->load->model('office_admin/search_model');
		$this->load->model('office_admin/StudentExcelExport_model');
	}

	public function index() {	
		$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/search',$data);
		$this->load->view('office_admin/common/footer');
	}

	public function searchStudent() {
		$this->form_validation->set_rules('reg_id', 'Registration ID', 'trim|required');
		$this->form_validation->set_rules('specialisation', 'Specialisation', 'required');

		//validate form input
		if ($this->form_validation->run() == FALSE) {
			// fails
			$this->session->set_flashdata('error', 'All fields are required!, please try again!');
            $this->index();
		} else {    		
			$reg_id = $this->input->post('reg_id');
			$year = $this->input->post('year');
			$specialisation = $this->input->post('specialisation');
			$search = $this->input->post('search');

			$data['form_fields']  = array(
				'reg_id' => $reg_id ,
				'year' => $year,
				'specialisation' => $specialisation ,
			);

			if($search == 'fees'){
				$userID = $reg_id;
				$this->session->set_userdata('posted_userid', $userID);
				$data['student_details'] = $this->search_model->get_student( $userID );
				$course_id = $data['student_details'][0]->course_id;
				$data['course_details'] = $this->search_model->get_course( $course_id );
				$data['fee_details'] = $this->search_model->get_fees( $course_id );
				$this->load->view('office_admin/common/header');
				$this->load->view('office_admin/search_fees', $data);
				$this->load->view('office_admin/common/footer');
			}else{
				$this->load->view('office_admin/common/header');
				$this->load->view('office_admin/search_document',$data);
			}	       
		}
	}


	public function fees_view(){
		$userID = $this->session->userdata('posted_userid');
		$data['student_details'] = $this->search_model->get_student( $userID );
		$course_id = $data['student_details'][0]->course_id;
		$data['course_details'] = $this->search_model->get_course( $course_id );
		$data['fee_details'] = $this->search_model->get_fees( $course_id );
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/search_fees', $data);
		$this->load->view('office_admin/common/footer');
	}

	public function generate_challan(){
		$this->form_validation->set_rules('payment_method','Payment Method','required');		
		if($_POST['payment_method'] != 'Cash'){
			$this->form_validation->set_rules('cheque', 'Cheque', 'required|numeric');
			$this->form_validation->set_rules('cheque_date', 'Cheque Date', 'required');
			$this->form_validation->set_rules('bank_name', 'Bank Name', 'required|regex_match[/^[a-zA-Z ]*$/]');
			$this->form_validation->set_rules('branch_name', 'Branch Name', 'required|regex_match[/^[a-zA-Z ]*$/]');
		}
    
		if ($this->form_validation->run() == FALSE) {
			// fails
			$this->session->set_flashdata('validate_error', 'All fields are required!!!',1);
			$this->fees_view();
		}else{				
	 		$bank_name = ($this->input->post('bank_name')) ? $this->input->post('bank_name') : '';
	        $branch_name = ($this->input->post('branch_name')) ? $this->input->post('branch_name') : '';
	        $cheque_date = ($this->input->post('cheque_date')) ? $this->input->post('cheque_date') : '';
	        $total_fee = $this->input->post('total_amount');
	        $late_fee = $this->input->post('late_fee');
	        $cheque = ($this->input->post('cheque')) ? $this->input->post('cheque') : '';
			$payment_method = $this->input->post('payment_method');
			$userID = $this->session->userdata('posted_userid');
			$data['student_details'] = $this->search_model->get_student( $userID );
			$course_id = $data['student_details'][0]->course_id;
			$data['course_details'] = $this->search_model->get_course( $course_id );
			$data['fee_details'] = $this->search_model->get_fees( $course_id );
			$caste_category = $data['student_details'][0]->caste_category;
			$caste = $data['student_details'][0]->caste;			
			if(strtolower($caste_category) == 'open'){
				$challan_number = $this->generate_challan_number($data['course_details'][0]->year,$data['course_details'][0]->course_name,$caste_category);
			} else {
				$challan_number = $this->generate_challan_number($data['course_details'][0]->year,$data['course_details'][0]->course_name,$caste);
			}
			
			$transaction[] = array(
				'student_id'=>$userID,
				'course_id'=>$course_id,
				'category'=>$caste_category,
				'email_id'=>$data['student_details'][0]->email_id,
				'challan_number'=>$challan_number,
				'fee_paid_date'=>date('Y-m-d'),
				'firstname'=>$data['student_details'][0]->first_name,
				'middlename'=>$data['student_details'][0]->middle_name,
				'lastname'=>$data['student_details'][0]->last_name,
				'mothername'=>$data['student_details'][0]->mother_first_name,
				'payment_mode'=>$payment_method,
				'payment_type'=>'Offline',
				'transaction_status'=>'Unpaid',
				'bank_name'=>$bank_name,
				'branch_name'=>$branch_name,
				'cheque_number'=>$cheque,
				'cheque_date'=>$cheque_date,
				'total_amount'=>$total_fee,
				'late_fee'=>$late_fee,
				'remark'=>'',
				'total_paid'=>$total_fee,
			);
			
			$this->search_model->insert_transaction($transaction);
			
			$data['fees_form_fields'] = array(
	        	"branch_name" => $branch_name,
	        	"bank_name"   => $bank_name,
	        	"cheque"      => $cheque,
	        	"cheque_date" => $cheque_date,
	        	"total_fee"   => $total_fee,
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
			$html = $this->load->view('office_admin/print_reciept' ,$data,true);
			$mpdf->WriteHTML($html);
		 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
			$mpdf->Output($pdf_path.'/'.$filename, 'F');
			$mpdf->Output($filename, 'I');

			$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$filename);
					// your CURL here
					
			$ch = curl_init();					
			curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=".$data['student_details'][0]->admission_year."&crn=".$userID."&docType=Fee-Receipt");
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
	        $message = '';
			$message .= '<table>
				<tr><td colspan=2>Hello '.$stuname.'</td></tr>
				<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous). 
</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
				<tr><td colspan=2>Thank You,</td></tr>
				</table>
				<div style="height:20px;"></div>';

			$email_id = $data['student_details'][0]->email_id;//'abhishek.apncoders@gmail.com';//$data['student_details'][0]->email_id;		
			$this->load->library('email');
			$config['mailtype'] = 'html';
			$this->email->initialize($config);
			$this->email->from('svtcollege2019@gmail', 'SVT College');
			$this->email->to($email_id);
			$this->email->cc('svtcollege2019@gmail.com');
			$this->email->subject('SVT challan');
			$this->email->message($message);
			$this->email->attach($pdf_path.'/'.$filename);
			$this->email->send();

		}
	}

	public function generate_challan_offline(){
		if (!isset($_GET['challan_no'])) {
			echo "<p style='text-align:center;padding:100px'>Challan number required to generate Fee Receipt....</p>";
			die;
		}else{
			$data['challan_no'] = $challan_number = $_GET['challan_no']; //='SYRO00056'; // SYHO00070===reserved category
		}
		$data['transaction_data'] = $this->search_model->fetch_transaction_data($data);
		if (empty($data['transaction_data'])) {
			echo "<p style='text-align:center;padding:100px'>No Transaction found for this challan no.</p>";
			die;
		}	
        $total_amount = $data['transaction_data'][0]->total_amount;
        $total_paid = $data['transaction_data'][0]->total_paid;
        $payment_method = $data['transaction_data'][0]->payment_type;
        $payment_mode = $data['transaction_data'][0]->payment_mode;
        $name = $data['transaction_data'][0]->firstname;
        $lname = $data['transaction_data'][0]->lastname;
        $transaction_status = $data['transaction_data'][0]->transaction_status;
        $fee_paid_date = $data['transaction_data'][0]->fee_paid_date;
        $late_fee = $data['transaction_data'][0]->late_fee;
		$userID = $data['transaction_data'][0]->student_id;
		$data['student_details'] = $this->search_model->get_student($userID);
		$course_id = @$data['transaction_data'][0]->course_id;
		$data['course_details'] = $this->search_model->get_course( $course_id );
		$caste_category = @$data['student_details'][0]->caste_category;
		$caste = @$data['student_details'][0]->caste;			
		
		$data['fee_details'] = $this->search_model->get_fees( $data['transaction_data'][0]->course_id );
		
		/*$transaction[] = array(
			'student_id'=>$userID,
			'course_id'=>$course_id,
			'category'=>$caste_category,
			'email_id'=>$data['student_details'][0]->email_id,
			'challan_number'=>$challan_number,
			'fee_paid_date'=>$data['transaction_data'][0]->fee_paid_date,
			'firstname'=>$data['student_details'][0]->first_name,
			'middlename'=>$data['student_details'][0]->middle_name,
			'lastname'=>$data['student_details'][0]->last_name,
			'mothername'=>$data['student_details'][0]->mother_first_name,
			'payment_mode'=>$payment_method,
			'payment_type'=>$payment_mode,
			'transaction_status'=>$transaction_status,
			'total_amount'=>$total_amount,
			'late_fee'=>$late_fee,
			'remark'=>'',
			'total_paid'=>$total_paid,
		);*/
		/*print_r($data['transaction_data']);
		print_r($transaction);
		print_r($data['fee_details']);
		die;*/

		//  GET Fee Receipt and email with attachment and upload it into student Document

		$pay_percentage = $gst = $grand_total = 0;
		$pay_percentage = ($total_amount/100) * 2;
		/*if($pay_percentage >= '500'){ 
			$pay_percentage = number_format('500', 2); 
		}else{
			$pay_percentage = number_format($pay_percentage, 2);
		}*/
		$gst = (18/100)*($pay_percentage);
		$online_total = $pay_percentage+$gst;

		$data['fees_form_fields'] = array(
        	"bank_name"   => '',
			"branch_name" => '',
        	"cheque"      => '',
        	"cheque_date" => '',
        	"total_fee"   => $total_paid,
        	"college_fee"   => $total_amount,
        	"online_total"   => $online_total,
			"challan_number"=>$challan_number,
			"sname"=>$name . " " . $lname
        );
        //echo "<pre>"; print_r($data); die;
		$mpdf = new \Mpdf\Mpdf([
			'format' => 'A5',
			'orientation' => 'L',
			'default_font_size' => 7,
			'default_font'=>'helvetica'
		]);
		$filename = 'SVT-'.$challan_number.'-'.time().'.pdf';
		//$mpdf->SetHTMLHeader('');			
		$html = $this->load->view('office_admin/print_reciept_offline' ,$data,true);
		$mpdf->WriteHTML($html);
	 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
		$mpdf->Output($pdf_path.'/'.$filename, 'F'); // to save in local folder
		$mpdf->Output($filename, 'I');


		// your CURL here
		$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$filename);		
		$ch = curl_init();					
		curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=".$data['student_details'][0]->admission_year."&crn=".$userID."&docType=Fee-Receipt&fileName=".$filename);
		curl_setopt($ch, CURLOPT_POST, 1);
		curl_setopt($ch, CURLOPT_POSTFIELDS, ['pdf' => $pdf_file]);					
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		$server_output = curl_exec($ch);
		if(curl_errno($ch))
		{
			echo 'Request Error:' . curl_error($ch);
		}					
		curl_close ($ch);

/* // ========= UNCOMMENT TO SEND EMAIL ========
		// Send Email to Student of Fee Receipt with attachment
		$stuname = $data['student_details'][0]->first_name.' '.$data['student_details'][0]->middle_name.' '.$data['student_details'][0]->last_name;
        $message = '';
        $message .= '<table>
			<tr><td colspan=2>Hello '.$data['student_details'][0]->first_name.',</td></tr>
			<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
			<tr><td>Name: </td><td>'.$stuname.'</td></tr>
			<tr><td>Email: </td><td>'.$data['student_details'][0]->email_id.'</td></tr>
			<tr><td>Mobile No.: </td><td>'.$data['student_details'][0]->mobile_number.'</td></tr>
			<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
			<tr><td>Year/Program: </td><td>'.$data['course_details'][0]->year."/".$data['course_details'][0]->course_type.'</td></tr>
			<tr><td>Grand Total: </td><td>'.$total_paid.'</td></tr>
			<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous). 
</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
			<tr><td colspan=2>Thank You</td></tr>
			</table>
			<div style="height:20px;"></div>';

		$email_id = $data['student_details'][0]->email_id;
		// $email_id = "viren.shah1985@gmail.com";	
		$this->load->library('email');

		// ======== START CUSTOM EMAIL SETTING TO GMALI SERVER ====
		//$config = array();
		//$config['protocol'] = 'smtp';
		//$config['smtp_host'] = 'smtp.googlemail.com';
		//$config['smtp_user'] = 'viren.shah1985@gmail.com';
		//$config['smtp_pass'] = 'HelloWorld@2013';
		//$config['smtp_port'] = 587;
		// ======== END CUSTOM EMAIL SETTING TO GMALI SERVER ====
		
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
*/
	}
	
	// ================ START- Fee Receipt for FY Junior College registration MIS3 Db=====
	public function generate_challan_junior(){

		if (!isset($_GET['challan_no'])) {
			echo "<p style='text-align:center;padding:150px'>Challan number required to generate Fee Receipt....</p>";
			die;
		}else{
			$data['challan_no'] = $challan_number = $_GET['challan_no']; //='SYRO00056'; // SYHO00070===reserved category
		}
		
		$data['transaction_data'] = $this->search_model->get_junior_transaction_data($data);
		if (empty($data['transaction_data'])) {
			echo "<p style='text-align:center;padding:150px'>No payment found for this challan no....</p>";
			die;
		}	
        $total_amount = $data['transaction_data'][0]->total_amount;
        $total_paid = $data['transaction_data'][0]->total_paid;
        $payment_method = $data['transaction_data'][0]->payment_type;
        $payment_mode = $data['transaction_data'][0]->payment_mode;
        $transaction_status = $data['transaction_data'][0]->transaction_status;
        $fee_paid_date = $data['transaction_data'][0]->fee_paid_date;
        $late_fee = $data['transaction_data'][0]->late_fee;
		$userID = $data['transaction_data'][0]->student_id;
		$data['student_details'] = $this->search_model->get_student_junior($userID);
		$course_id = $data['student_details'][0]->course_id;
		$data['course_details'] = $this->search_model->get_course_junior( $course_id );
		$caste_category = $data['student_details'][0]->caste_category;
		$caste = $data['student_details'][0]->caste;
		$data['fee_details'] = $this->search_model->get_junior_fees( $course_id );
		/*echo "<pre>";			
		print_r($data['transaction_data']);
		print_r($data);
		die;*/
		
		/*$transaction[] = array(
			'student_id'=>$userID,
			'course_id'=>$course_id,
			'category'=>$caste_category,
			'email_id'=>$data['student_details'][0]->email_id,
			'challan_number'=>$challan_number,
			'fee_paid_date'=>$data['transaction_data'][0]->fee_paid_date,
			'firstname'=>$data['student_details'][0]->first_name,
			'middlename'=>$data['student_details'][0]->middle_name,
			'lastname'=>$data['student_details'][0]->last_name,
			'mothername'=>$data['student_details'][0]->mother_first_name,
			'payment_mode'=>$payment_method,
			'payment_type'=>$payment_mode,
			'transaction_status'=>$transaction_status,
			'total_amount'=>$total_amount,
			'late_fee'=>$late_fee,
			'remark'=>'',
			'total_paid'=>$total_paid,
		);*/
		/*print_r($data['transaction_data']);
		print_r($transaction);
		print_r($data['fee_details']);
		die;*/

		//  GET Fee Receipt and email with attachment and upload it into student Document

		$pay_percentage = $gst = $grand_total = 0;
		$pay_percentage = ($total_amount/100) * 2;
		/*if($pay_percentage >= '500'){ 
			$pay_percentage = number_format('500', 2); 
		}else{
			$pay_percentage = number_format($pay_percentage, 2);
		}*/
		$gst = (18/100)*($pay_percentage);
		$online_total = $pay_percentage+$gst;

		$data['fees_form_fields'] = array(
        	"bank_name"   => '',
			"branch_name" => '',
        	"cheque"      => '',
        	"cheque_date" => '',
        	"total_fee"   => $total_paid,
        	"college_fee"   => $total_amount,
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
		$html = $this->load->view('office_admin/print_reciept_junior' ,$data,true);
		$mpdf->WriteHTML($html);
	 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
		//$mpdf->Output($pdf_path.'/'.$filename, 'F'); // to save in local folder
		$mpdf->Output($filename, 'I');


	/*	// your CURL here
		$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$filename);		
		$ch = curl_init();					
		curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=".$data['student_details'][0]->admission_year."&crn=".$userID."&docType=Fee-Receipt&fileName=".$filename);
		curl_setopt($ch, CURLOPT_POST, 1);
		curl_setopt($ch, CURLOPT_POSTFIELDS, ['pdf' => $pdf_file]);					
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		$server_output = curl_exec($ch);
		if(curl_errno($ch))
		{
			echo 'Request Error:' . curl_error($ch);
		}					
		curl_close ($ch);*/

/* // ========= UNCOMMENT TO SEND EMAIL ========
		// Send Email to Student of Fee Receipt with attachment
		$stuname = $data['student_details'][0]->first_name.' '.$data['student_details'][0]->middle_name.' '.$data['student_details'][0]->last_name;
        $message = '';
        $message .= '<table>
			<tr><td colspan=2>Hello '.$data['student_details'][0]->first_name.',</td></tr>
			<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
			<tr><td>Name: </td><td>'.$stuname.'</td></tr>
			<tr><td>Email: </td><td>'.$data['student_details'][0]->email_id.'</td></tr>
			<tr><td>Mobile No.: </td><td>'.$data['student_details'][0]->mobile_number.'</td></tr>
			<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
			<tr><td>Year/Program: </td><td>'.$data['course_details'][0]->year."/".$data['course_details'][0]->course_type.'</td></tr>
			<tr><td>Grand Total: </td><td>'.$total_paid.'</td></tr>
			<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous). 
</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
			<tr><td colspan=2>Thank You</td></tr>
			</table>
			<div style="height:20px;"></div>';

		$email_id = $data['student_details'][0]->email_id;
		// $email_id = "viren.shah1985@gmail.com";	
		$this->load->library('email');

		// ======== START CUSTOM EMAIL SETTING TO GMALI SERVER ====
		//$config = array();
		//$config['protocol'] = 'smtp';
		//$config['smtp_host'] = 'smtp.googlemail.com';
		//$config['smtp_user'] = 'viren.shah1985@gmail.com';
		//$config['smtp_pass'] = 'HelloWorld@2013';
		//$config['smtp_port'] = 587;
		// ======== END CUSTOM EMAIL SETTING TO GMALI SERVER ====
		
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
*/
	}
	

	public function generate_mixed_challan_offline(){
		if (!isset($_GET['challan_no'])) {
			echo "<p style='text-align:center;padding:100px'>Challan number required to generate Fee Receipt....</p>";
			die;
		}else{
			$data['challan_no'] = $challan_number = $_GET['challan_no']; //='SYRO00056'; // SYHO00070===reserved category
		}
		$data['transaction_data'] = $this->search_model->fetch_transaction_data($data);
		if (empty($data['transaction_data'])) {
			echo "<p style='text-align:center;padding:100px'>No Transaction found for this challan no.</p>";
			die;
		}	
        $total_amount = $data['transaction_data'][0]->total_amount;
        $total_paid = $data['transaction_data'][0]->total_paid;
        $payment_method = $data['transaction_data'][0]->payment_type;
        $payment_mode = $data['transaction_data'][0]->payment_mode;
        $name = $data['transaction_data'][0]->firstname;
        $lname = $data['transaction_data'][0]->lastname;
        $transaction_status = $data['transaction_data'][0]->transaction_status;
        $fee_paid_date = $data['transaction_data'][0]->fee_paid_date;
        $late_fee = $data['transaction_data'][0]->late_fee;
		$userID = $data['transaction_data'][0]->student_id;
		$data['student_details'] = $this->search_model->get_student($userID);
		$course_id = @$data['transaction_data'][0]->course_id;
		$data['course_details'] = $this->search_model->get_course( $course_id );
		$caste_category = @$data['transaction_data'][0]->category;
		$caste = @$data['student_details'][0]->caste;			
		$specialization = @$data['student_details'][0]->specialization;			
		$division = @$data['student_details'][0]->division;			
		$year = @$data['course_details'][0]->year;			
		
		$data['fee_details'] = $this->search_model->get_fees( $data['transaction_data'][0]->course_id );
		
		/*$transaction[] = array(
			'student_id'=>$userID,
			'course_id'=>$course_id,
			'category'=>$caste_category,
			'email_id'=>$data['student_details'][0]->email_id,
			'challan_number'=>$challan_number,
			'fee_paid_date'=>$data['transaction_data'][0]->fee_paid_date,
			'firstname'=>$data['student_details'][0]->first_name,
			'middlename'=>$data['student_details'][0]->middle_name,
			'lastname'=>$data['student_details'][0]->last_name,
			'mothername'=>$data['student_details'][0]->mother_first_name,
			'payment_mode'=>$payment_method,
			'payment_type'=>$payment_mode,
			'transaction_status'=>$transaction_status,
			'total_amount'=>$total_amount,
			'late_fee'=>$late_fee,
			'remark'=>'',
			'total_paid'=>$total_paid,
		);*/
		/*
		echo "<pre>";
		print_r($data['transaction_data']);
		//print_r($transaction);
		print_r($data['fee_details']);
		die;*/

		//  GET Fee Receipt and email with attachment and upload it into student Document

		$pay_percentage = $gst = $grand_total = 0;
		$pay_percentage = ($total_amount/100) * 2;
		/*if($pay_percentage >= '500'){ 
			$pay_percentage = number_format('500', 2); 
		}else{
			$pay_percentage = number_format($pay_percentage, 2);
		}*/
		$gst = (18/100)*($pay_percentage);
		$online_total = $pay_percentage+$gst;

		$data['fees_form_fields'] = array(
        	"bank_name"   => '',
			"branch_name" => '',
        	"cheque"      => '',
        	"cheque_date" => '',
        	"total_fee"   => $total_paid,
        	"college_fee"   => $total_amount,
        	"online_total"   => $online_total,
			"challan_number"=>$challan_number,
			"sname"=>$name . " " . $lname,
			"first_name"=>$name,
			"last_name"=>$lname,
			"caste_category"=>$caste_category,
			"division"=>@$division,
			"specialization"=>@$specialization,
			"program"=>@$student_degree,
			"course_name"=>@$course_name,
			"year"=>@$year,
        );
        //echo "<pre>"; print_r($data); die;
		$mpdf = new \Mpdf\Mpdf([
			'format' => 'A5',
			'orientation' => 'L',
			'default_font_size' => 7,
			'default_font'=>'helvetica'
		]);
		$filename = 'SVT-'.$challan_number.'-'.time().'.pdf';
		//$mpdf->SetHTMLHeader('');		
		if ($data['transaction_data'][0]->remark && $data['transaction_data'][0]->remark=='Hostel Mess Payment')
		{
			$data['fees_form_fields']['college_fee'] = 5250;
			$data['fees_form_fields']['online_total'] = 130;
			$html = $this->load->view('office_admin/print_reciept_hostel_mess' ,$data,true);
		}else {	
			$html = $this->load->view('office_admin/print_reciept_offline' ,$data,true);
		}
		$mpdf->WriteHTML($html);
	 	$pdf_path = realpath(APPPATH . '../uploads/pdf');
		//$mpdf->Output($pdf_path.'/'.$filename, 'F'); // to save in local folder
		$mpdf->Output($filename, 'I');

/*	// ========= UNCOMMENT TO SEND RECEIPT IN STUDENT DOCUMENT ON API ========
		// your CURL here
		$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$filename);		
		$ch = curl_init();					
		curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=".$data['student_details'][0]->admission_year."&crn=".$userID."&docType=Fee-Receipt&fileName=".$filename);
		curl_setopt($ch, CURLOPT_POST, 1);
		curl_setopt($ch, CURLOPT_POSTFIELDS, ['pdf' => $pdf_file]);					
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		$server_output = curl_exec($ch);
		if(curl_errno($ch))
		{
			echo 'Request Error:' . curl_error($ch);
		}					
		curl_close ($ch);*/

/* // ========= UNCOMMENT TO SEND EMAIL ========
		// Send Email to Student of Fee Receipt with attachment
		$stuname = $data['student_details'][0]->first_name.' '.$data['student_details'][0]->middle_name.' '.$data['student_details'][0]->last_name;
        $message = '';
        $message .= '<table>
			<tr><td colspan=2>Hello '.$data['student_details'][0]->first_name.',</td></tr>
			<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;"></td></tr>
			<tr><td>Name: </td><td>'.$stuname.'</td></tr>
			<tr><td>Email: </td><td>'.$data['student_details'][0]->email_id.'</td></tr>
			<tr><td>Mobile No.: </td><td>'.$data['student_details'][0]->mobile_number.'</td></tr>
			<tr><td>Fee Type: </td><td>Academic fees payment</td></tr>
			<tr><td>Year/Program: </td><td>'.$data['course_details'][0]->year."/".$data['course_details'][0]->course_type.'</td></tr>
			<tr><td>Grand Total: </td><td>'.$total_paid.'</td></tr>
			<tr><td colspan=2 style="padding-bottom:15px;margin-top:10px;">Thank you for payment of fees to Sir Vithaldas Thackersey College of Home Science (Autonomous). 
</td></tr><tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Please find attached receipt of your fees payment. </td></tr>
			<tr><td colspan=2>Thank You</td></tr>
			</table>
			<div style="height:20px;"></div>';

		$email_id = $data['student_details'][0]->email_id;
		// $email_id = "viren.shah1985@gmail.com";	
		$this->load->library('email');

		// ======== START CUSTOM EMAIL SETTING TO GMALI SERVER ====
		//$config = array();
		//$config['protocol'] = 'smtp';
		//$config['smtp_host'] = 'smtp.googlemail.com';
		//$config['smtp_user'] = 'viren.shah1985@gmail.com';
		//$config['smtp_pass'] = 'HelloWorld@2013';
		//$config['smtp_port'] = 587;
		// ======== END CUSTOM EMAIL SETTING TO GMALI SERVER ====
		
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
*/
	}
	

	public function generate_challan_number($year,$name,$caste){
		$challan = $this->search_model->get_challan_number();
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
	
	public function update_transaction_status(){
		$trans_id = $this->input->post('trans_id');
		$data['transaction_status'] = $this->input->post('transaction_status');
		if(empty($trans_id)){
			return false;
		}		
		if(empty($data['transaction_status'])){
			return false;
		}
		if( $this->search_model->update_transaction_status($data,$trans_id)) {
			echo json_encode(['success'=>'Transaction updated successfully']);
		} else {
			echo json_encode(['error'=>'Error while updating.']);			
		}		
	}
	public function abhishek()
	{
		//ob_start();
    	$data = array();
 		// ini_set('max_execution_time', '300');
		//ini_set("pcre.backtrack_limit", "5000000");
		$filename = 'SVT-Hostel-'.time().'.pdf';

		$data['fees_form_fields'] = array(
        	"bank_name"   => '',
			"branch_name" => '',
        	"cheque"      => '',
        	"cheque_date" => '',
        	"total_fee"   => 5380,
        	"college_fee"   => 5250,
        	"online_total"   => 130,
			"challan_number"=>'FYRO0001',
        	"caste_category"=>'Open',
			"division"=>'A-I',
			"first_name"=>'KHUSHBU',
			"last_name"=>'NIMESHBHAI BAROT',
			"specialization"=>'M.Sc. Specialized Dietetics',
			"program"=>'bachelor',
			"course_name"=>'M.Sc. Specialized Dietetics',
        );
		//$mpdf->SetHTMLHeader('');			
		$html = $this->load->view('office_admin/print_reciept_hostel_mess',$data,true);
		ob_clean(); // cleaning the buffer before Output()
		/*$mpdf = new \Mpdf\Mpdf([
			'mode' => 'utf-8',
			 'format' => 'A4',
			'autoPageBreak' => true
		]);*/
		$mpdf = new \Mpdf\Mpdf([
			'format' => 'A5',
			'orientation' => 'L',
			'default_font_size' => 7,
			'default_font'=>'helvetica'
		]);
		$mpdf->WriteHTML($html);
		// error_reporting(E_ALL);
	 	//$pdf_path = realpath(APPPATH . '../uploads/pdf');
		//$mpdf->Output($pdf_path.'/'.$filename, 'F');
		$mpdf->Output($filename, 'I');
    	//ob_flush();
			//ob_end_clean();
    	//====================== HOSTEL FOEM PDF ======================
    	

	}	
}?>