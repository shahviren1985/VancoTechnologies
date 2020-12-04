<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class AluminiCopy extends CI_Controller {

	public function __construct(){
		parent::__construct();
		if( $this->session->userdata("logged_in") ) {
			if($this->session->userdata("role") != '3'){
				redirect(site_url(), 'refresh');
			}
        }
		$this->load->helper('form');
		$this->load->library('form_validation');
		$this->load->model('Alumini_model_copy');
	}
	
	public function index() {
		$this->load->view('alumini-copy');
	}
	
	/* public function alumini_payment(){
		$this->form_validation->set_rules('photo', '', 'callback_file_check');
		if($this->form_validation->run()){	
		if(($_FILES['photo']['name'] != "" )){
			$new_name = time();
			$file_ext = pathinfo($_FILES["photo"]['name'],PATHINFO_EXTENSION);
			$file_name = $new_name.".".$file_ext;
			$config = array(
				'file_name' => $file_name,
				'upload_path' => './uploads/student_photo/',
				'allowed_types' => 'jpg|png',
				'overwrite' => TRUE,
				'max_size' => "302700",
			);
			$this->load->library('upload', $config);
				if(!$this->upload->do_upload('photo')){
				$error = array('error' => $this->upload->display_errors());
				echo json_encode($error);
				}else{
					$fees_amount = $this->input->post('fees_amount');
					$amount = $this->input->post('total_amount');
					//$MERCHANT_KEY = "BC50nb";
					$MERCHANT_KEY = "hDkYGPQe";
					//$SALT = "Bwxo1cPe";
					$SALT = "yIEkykqEH3";
					// End point - change to https://secure.payu.in for LIVE mode
					$PAYU_BASE_URL = "https://test.payu.in";
					$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
					$action = '';
					$productinfo = $this->input->post('department');
					$firstname = $this->input->post('name');
					$lastname = $this->input->post('residential_address');
					$email = $this->input->post('email_address');
					$phone = $this->input->post('phone_number');
					$landline = $this->input->post('landline');
					$photo = $file_name;
					$dob = $this->input->post('dob');
					$age = $this->input->post('age');
					$extra_activity = $this->input->post('extra_activity');
					$alumini_message = $this->input->post('alumini-message');
					$c_spec = $this->input->post('specialization');
					$c_year = $this->input->post('year_of_passing');
					$c_signature = $this->input->post('signature');
					$c_receipt_number = $this->input->post('receipt-number');
					$c_date_payment = $this->input->post('date-of-payment');
					$c_alumini_name = $this->input->post('alumini_name');
					$c_contact_details = $this->input->post('contact_details');
					$c_alumini_email_address = $this->input->post('alumini_email_address');
					$c_alumini_mobile_number = $this->input->post('alumini_mobile_number');
					$concate_data = $landline.'__'.$photo.'__'.$dob.'__'.$age.'__'.$extra_activity.'__'.$alumini_message.'__'.$c_spec.'_'.$c_year.'_'.$c_signature.'_'.$c_receipt_number.'_'.$c_date_payment.'_'.$c_alumini_name.'_'.$c_contact_details.'_'.$c_alumini_email_address.'_'.$c_alumini_mobile_number;
					$udf1 = $concate_data;
					$udf2 = '';
					$udf3 = '';
					$udf4 = '';
					$udf5 = '';
					$udf6 = '';

					$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
					$hash = strtolower(hash('sha512', $hashstring));
					$action = $PAYU_BASE_URL . '/_payment';			
					
					$success = base_url() . 'AluminiPaymentStatus';
					$fail = base_url() . 'AluminiPaymentStatus';
					$cancel = base_url() . 'AluminiPaymentStatus';

					$data = array(
						'MERCHANT_KEY' =>   $MERCHANT_KEY,
						'txnid' =>          $txnid,
						'hash' =>           $hash,
						'amount' =>         $amount,
						'productinfo' =>    $productinfo,
						'firstname' =>      $firstname,
						'lastname' =>      	$lastname,
						'email' =>          $email,
						'phone' =>          $phone,
						'salt' =>           $SALT,
						'udf1' =>           $udf1,
						'udf2' =>           $udf2,
						'udf3' =>           $udf3,
						'udf4' =>           $udf4,
						'udf5' =>           $udf5,
						'action' =>         $action,
						'success' =>        $success,
						'failure' =>        $fail,
						'cancel' =>         $cancel
					);
					
					$this->load->view('confirmation', $data);
				}
			} 
		}
	} */
	
	public function alumini_payment() {		
		$this->form_validation->set_rules('name', 'Name', 'required');
		$this->form_validation->set_rules('residential_address', 'Residential Address', 'required');
		$this->form_validation->set_rules('dob', 'Date of Birth', 'required');
		$this->form_validation->set_rules('department', 'Department', 'required');
		$this->form_validation->set_rules('specialization', 'Specialization', 'required');
		$this->form_validation->set_rules('amount', 'Amount', 'required');
		$this->form_validation->set_rules('email_address', 'Email Address', 'trim|required|valid_email');
		$this->form_validation->set_rules('phone_number', 'Mobile Number', 'trim|required|numeric');
		$this->form_validation->set_rules('photo', '', 'callback_file_check');
		if($this->form_validation->run() === TRUE){	
			if(($_FILES['photo']['name'] != "" )){
			$new_name = time();
			$file_ext = pathinfo($_FILES["photo"]['name'],PATHINFO_EXTENSION);
			$file_name = $new_name.".".$file_ext;
			$config = array(
				'file_name' => $file_name,
				'upload_path' => './uploads/student_photo/',
				'allowed_types' => 'jpg|png',
				'overwrite' => TRUE,
				'max_size' => "302700",
			);
			$this->load->library('upload', $config);
				if(!$this->upload->do_upload('photo')){
					$error = array('error' => $this->upload->display_errors());
					echo json_encode($error);
				}else {
					$fee_head_total = $this->input->post('fee_head_total');
					$amount_with_comma = $this->input->post('amount');
					$amount = str_replace(',', '', $amount_with_comma);        
					$commission = $this->input->post('commission');

					$firstSplitArr = array("name"=>"splitID1", "value"=>$fee_head_total, "merchantId"=>"4825050", "description"=>"payment splitting", "commission"=>$commission);
					//$firstSplitArr = array("name"=>"splitID1", "value"=>$fee_head_total, "merchantId"=>"6890980", "description"=>"payment splitting", "commission"=>$commission);
					// payu child merchant id - 4825050

					$paymentPartsArr = array($firstSplitArr);   
					$finalInputArr = array("paymentParts" => $paymentPartsArr);
					$Prod_info = json_encode($finalInputArr);

					// Merchant key here as provided by Payu
					$MERCHANT_KEY = "BC50nb";
					//$MERCHANT_KEY = "qk9XqxqH";
					$SALT = "Bwxo1cPe";
				   // $SALT = "fZGlLKDEO9";
					// End point - change to https://secure.payu.in for LIVE mode
					$PAYU_BASE_URL = "https://test.payu.in";
					//$PAYU_BASE_URL = "https://secure.payu.in";
					$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
					$action = '';
					
					
					$productinfo = $Prod_info;
					$firstname = $this->input->post('name');
					$lastname = $this->input->post('residential_address');
					$email = $this->input->post('email_address');
					$phone = $this->input->post('phone_number');
					$landline = $this->input->post('landline');
					$c_photo = $file_name;
					$c_dob = $this->input->post('dob');
					$c_age = $this->input->post('age');
					$c_spec = $this->input->post('specialization');
					$c_year = $this->input->post('year_of_passing');
					$extra_activity = $this->input->post('extra_activity');
					$alumini_message = $this->input->post('alumini-message');
					$signature = $this->input->post('signature');
					$receipt_number = $this->input->post('receipt-number');
					$date_of_payment = $this->input->post('date-of-payment');
					$residential_address = $this->input->post('residential_address');
					$alumini_name = $this->input->post('alumini_name');
					$contact_details = $this->input->post('contact_details');
					$alumini_email_address = $this->input->post('alumini_email_address');
					$alumini_mobile_number = $this->input->post('alumini_mobile_number');
					$alumini_mate_id = $this->input->post('hidden_data');

					$concate_data = $landline.'__'.$c_photo.'__'.$c_dob.'__'.$c_age.'__'.$c_spec.'__'.$c_year.'__'.$extra_activity.'__'.$alumini_message.'__'.$signature.'__'.$receipt_number.'__'.$date_of_payment.'__'.$residential_address.'__'.$alumini_name.'__'.$contact_details.'__'.$alumini_email_address.'__'.$alumini_mobile_number.'__'.$alumini_mate_id;
					
					if($alumini_mate_id >= 2){
						for($i=2;$i<=$alumini_mate_id;$i++){
							$concate_data .= '__'.$this->input->post('alumini_name'.$i).'__'.$this->input->post('contact_details'.$i).'__'.$this->input->post('alumini_email_address'.$i).'__'.$this->input->post('alumini_mobile_number'.$i); 
						}
					}
					$udf1 = $concate_data;
					$udf2 = '';
					$udf3 = '';
					$udf4 = '';
					$udf5 = '';
					$udf6 = '';

					$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
					
					$hash = strtolower(hash('sha512', $hashstring));
					$action = $PAYU_BASE_URL . '/_payment';			
					
					$success = base_url() . 'AluminiPaymentStatusCopy';
					$fail = base_url() . 'AluminiPaymentStatusCopy';
					$cancel = base_url() . 'AluminiPaymentStatusCopy';

					$data = array(
						'MERCHANT_KEY' =>   $MERCHANT_KEY,
						'txnid' =>          $txnid,
						'hash' =>           $hash,
						'amount' =>         $amount,
						'productinfo' =>    $productinfo,
						'firstname' =>      $firstname,
						'lastname' =>      	$lastname,
						'email' =>          $email,
						'phone' =>          $phone,
						'salt' =>           $SALT,
						'udf1' =>           $udf1,
						'udf2' =>           $udf2,
						'udf3' =>           $udf3,
						'udf4' =>           $udf4,
						'udf5' =>           $udf5,
						'action' =>         $action,
						'success' =>        $success,
						'failure' =>        $fail,
						'cancel' =>         $cancel
					);
					
					$fp = fopen('./uploads/logs/'.$txnid.'.json', 'w');
                    fwrite($fp, json_encode($data));
					
					$this->load->view('confirmation', $data);
				}
			} 
		}else{
			$this->session->set_flashdata('error', 'Fill all mandatory fields.');
            $this->index();
		}
	}
	
	public function file_check($str){
        $allowed_mime_type_arr = array('image/jpeg','image/pjpeg','image/png','image/x-png');
        $mime = get_mime_by_extension($_FILES['photo']['name']);
        if(isset($_FILES['photo']['name']) && $_FILES['photo']['name']!=""){
            if(in_array($mime, $allowed_mime_type_arr)){
                return true;
            }else{
                $this->form_validation->set_message('file_check', 'Please select only jpg/png file.');
                return false;
            }
        }else{
            $this->form_validation->set_message('file_check', 'Please choose a file to upload.');
            return false;
        }
    }
	
	public function alumini_event(){		
		if(!empty($_POST))
		{	
			$this->load->helper('form');
			$this->load->library('form_validation');
			$this->form_validation->set_rules('name', 'name', 'required');
			$this->form_validation->set_rules('phone_number', 'phone_number', 'required');	
			$this->form_validation->set_rules('email_address', 'email_address', 'required');	
			$this->form_validation->set_rules('residential_address', 'residential_address', 'required');	
			$this->form_validation->set_rules('specialization', 'specialization', 'required');	
			$this->form_validation->set_rules('year_of_passing', 'year_of_passing', 'required');	
			/* $this->form_validation->set_rules('working_status', 'working_status', 'required');	
			$this->form_validation->set_rules('higher_education_status', 'higher_education_status', 'required'); */	
			if ($this->form_validation->run() === FALSE) 
			{			
				$this->session->set_flashdata('msg', 'Error occured! Please fill up the form carefully.');	
				redirect("alumini/alumini_event/");		
			}
			else
			{
				$fees_amount = $this->input->post('fees_amount');
				$amount = $this->input->post('total_amount');
				//$MERCHANT_KEY = "BC50nb";
				$MERCHANT_KEY = "7aizVQa5";//"hDkYGPQe";
				//$SALT = "Bwxo1cPe";
				$SALT = "mLXPoA2jsp";//"yIEkykqEH3";
				// End point - change to https://secure.payu.in for LIVE mode
				$PAYU_BASE_URL = "https://secure.payu.in";//"https://test.payu.in";
				$txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
				$action = '';
				$productinfo = $this->input->post('specialization');
				$firstname = $this->input->post('name');
				$lastname = $this->input->post('residential_address');
				$email = $this->input->post('email_address');
				$phone = $this->input->post('phone_number');
				$c_spec = $this->input->post('specialization');
				$c_year = $this->input->post('year_of_passing');
				$student_field = $this->input->post('student_field');
				$current_org = $this->input->post('current_org');
				$current_designation = $this->input->post('current_designation');
				$total_exp = $this->input->post('total_exp');
				$higher_education_status = $this->input->post('higher_education_status');
				$residential_address = $this->input->post('residential_address');
				$name_of_qualification = $this->input->post('name_of_qualification');
				$college_university_name = $this->input->post('college_university_name');
				$concate_data = $c_spec.'__'.$c_year.'__'.$current_org.'__'.$current_designation.'__'.$total_exp.'__'.$residential_address.'__'.$name_of_qualification.'__'.$college_university_name.'__'.$student_field.'__'.$higher_education_status;
				
				$udf1 = $concate_data;
				$udf2 = '';
				$udf3 = '';
				$udf4 = '';
				$udf5 = '';
				$udf6 = '';

				$hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
				$hash = strtolower(hash('sha512', $hashstring));
				$action = $PAYU_BASE_URL . '/_payment';			
				
				$success = base_url() . 'AluminiRegistrationStatus';
				$fail = base_url() . 'AluminiRegistrationStatus';
				$cancel = base_url() . 'AluminiRegistrationStatus';

				$data = array(
					'MERCHANT_KEY' =>   $MERCHANT_KEY,
					'txnid' =>          $txnid,
					'hash' =>           $hash,
					'amount' =>         $amount,
					'productinfo' =>    $productinfo,
					'firstname' =>      $firstname,
					'lastname' =>      	$lastname,
					'email' =>          $email,
					'phone' =>          $phone,
					'salt' =>           $SALT,
					'udf1' =>           $udf1,
					'udf2' =>           $udf2,
					'udf3' =>           $udf3,
					'udf4' =>           $udf4,
					'udf5' =>           $udf5,
					'action' =>         $action,
					'success' =>        $success,
					'failure' =>        $fail,
					'cancel' =>         $cancel
				);
				
					$fp = fopen('./uploads/logs/'.$txnid.'.json', 'w');
                    fwrite($fp, json_encode($data));
				
				$this->load->view('confirmation', $data);
			}			
		}
		else
		{
			$this->load->view('alumini-event');
		}
	} 
	
	public function refund_policy(){
		$this->load->view('refund-policy');
	}
}?>