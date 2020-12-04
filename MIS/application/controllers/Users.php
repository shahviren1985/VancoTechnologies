<?php

defined('BASEPATH') OR exit('No direct script access allowed');

class Users extends CI_Controller {

	public function __construct() {
		header('Access-Control-Allow-Origin: *');
		parent::__construct();
		$this->load->model('user');
		$this->load->model('student/student');
		$this->load->model('staff/staff');
		$this->load->library('form_validation');
	}

	// login view
	public function index() {
		//initial script for creating admin & office admins
		$admin['userID'] = 'admin';
		$admin['password'] = password_hash('123', PASSWORD_DEFAULT);
		$admin['connectionString'] = ADMINString;
		$admin['role'] = ADMIN;

		$officeadmin1['userID'] = 'officeadmin1';
		$officeadmin1['password'] = password_hash('123', PASSWORD_DEFAULT);
		$officeadmin1['connectionString'] = OFFICEADMIN1;
		$officeadmin1['role'] = OFFICEADMIN;

		$officeadmin2['userID'] = 'officeadmin2';
		$officeadmin2['password'] = password_hash('123', PASSWORD_DEFAULT);
		$officeadmin2['connectionString'] = OFFICEADMIN2;
		$officeadmin2['role'] = OFFICEADMIN;

		$officeadmin3['userID'] = 'officeadmin3';
		$officeadmin3['password'] = password_hash('123', PASSWORD_DEFAULT);
		$officeadmin3['connectionString'] = OFFICEADMIN3;
		$officeadmin3['role'] = OFFICEADMIN;
		// calling the my_script model
		$this->user->my_script($admin, $officeadmin1, $officeadmin2, $officeadmin3);

		// loading the login view
		$this->load->view('index');
	}

	// login action
	public function login_action(){
		$this->form_validation->set_rules('userID', 'User ID', 'trim|required');
		$this->form_validation->set_rules('password', 'Password', 'trim|required');

		//print_r($_POST);

		//validate form input
		if ($this->form_validation->run() == FALSE) {
			// fails
			$this->session->set_flashdata('error', 'Invalid User ID or password!, please try again!');
            redirect(base_url());
		}
		else
        {
            $userID = trim($this->input->post('userID'));
			$password = trim($this->input->post('password'));

			//echo password_hash(trim($this->input->post('password')), PASSWORD_BCRYPT); die;
            
			$result = $this->user->is_user_exist($userID, $password);

			if($result != false && !empty($result->userID)) {

				$this->session->set_userdata('id', $result->id);
				$this->session->set_userdata('userID', $result->userID);
				$this->session->set_userdata('connectionString', $result->connectionString);
				$this->session->set_userdata('role', $result->role);
				$this->session->set_userdata('first_time_login', $result->first_time_login);
				$this->session->set_userdata('logged_in', '1');

				// storing user first & last name into session
				if($result->role == STUDENT){
					$student_data = $this->student->get_entry($result->userID);
					if( $student_data[0]->first_name || $student_data[0]->middle_name || $student_data[0]->last_name ) {
						//$student_name = $student_data[0]->first_name." ".$student_data[0]->middle_name." ".$student_data[0]->last_name;
						$student_name = $student_data[0]->first_name." ".$student_data[0]->last_name;
					}
					
					$this->session->set_userdata('student_name', $student_name);
				}
				//echo $student_data[0]->first_name; die;
				//print_r($student_data); die;
				//print_r($this->session->userdata);
				
				// redirecting user acc. to their roles
				if( isset($result->role) && !empty($result->role) && $result->role == ADMIN ) {
					redirect('admin/home');
				} else if( isset($result->role) && !empty($result->role) && $result->role == OFFICEADMIN ){
					redirect('officeadmin/home');
				} else if( isset($result->role) && !empty($result->role) && $result->role == STUDENT ){
					redirect('student/home');
				}else if(isset($result->role) && !empty($result->role) && $result->role == 4){
					$staff_data = $this->staff->get_entry($result->userID);
					if( $staff_data[0]->firstname || $staff_data[0]->last_name) {
						$staff_name = $staff_data[0]->firstname." ".$staff_data[0]->lastname;
					}
					$this->session->set_userdata('staff_name', $staff_name);
					redirect('staff/staffs');
				}
				else {
					redirect('index');
				}	
			} else {
				$this->session->set_flashdata('error', 'Invalid User ID or password!, please try again!');
				redirect(base_url());
			}
        }
	}

	// register view
	public function register() {
        $this->load->view('register-form');

	}

	// register action as API
	public function register_api()
    {
	 // 	 echo "<pre>"; print_r($_POST);print_r($_FILES); echo "</pre>";
		// die;  
		
    	$location = '';
    	$response = array();
    	$database = 'clg_db3';
        $db_query = $this->load->database($database, TRUE);
        $start_with = 1001 ; 
        $userID = '';
        $userdata = $this->user->get_last_row_userIdNew();
		if(empty($userdata)){
     		$userID = $start_with;
			} 
			else {
    		 $user_prev_id = $userdata->userID;
      		 $userID  = $user_prev_id +1 ;
			}
      

        $input = $this->input->post();
   
    	//image upload
        $config['file_name']			= rand().'_'.date('d-m-Y');
        $config['upload_path']          = './uploads/student_photo';
        $config['allowed_types']        = 'jpeg|jpg|png';

        $this->load->library('upload', $config);

        $allowed = array('jpeg', 'png', 'jpg');

		$photoname = $_FILES['photo']['name'];
		$signaturename = $_FILES['signature_photo']['name'];
		$parent_signaturename = $_FILES['parent_signature']['name'];
		$photo_ext = $signature_ext = $parent_signature_ext = 0;
		$photo_ext_msg = '';
		$ext1 = pathinfo($photoname, PATHINFO_EXTENSION);
		$ext2 = pathinfo($signaturename, PATHINFO_EXTENSION);
		$ext3 = pathinfo($parent_signaturename, PATHINFO_EXTENSION);
		if (!in_array($ext1, $allowed)) {
		    $photo_ext =1;
		    $photo_ext_msg = 'photo';
		}
		if (!in_array($ext2, $allowed)) {
		    $signature_ext =1;
		    $photo_ext_msg = 'signature';
		}
		if (!in_array($ext2, $allowed)) {
		    $parent_signature_ext =1;
		    $photo_ext_msg = 'parent_signature';
		}
        if($photo_ext ==1 || $signature_ext ==1 || $parent_signature_ext ==1){
        	$response =array(
			"message" => "Please upload $photo_ext_msg with jpeg,jpg,png file extension only...",
			"error" => $photo_ext_msg,
			"status" => 'error',
			);
			echo  json_encode($response); 
			exit;
			
        }

        //photograpgh upload
        if ( ! $this->upload->do_upload('photo'))
        {
            $photo_error = array('error' => $this->upload->display_errors());
            $response =array(
			"message" => $photo_error,
			"status" => 'error',
			"error" => 'photo',
			);
			echo  json_encode($response); 
			exit;
        }
        else
        {
            $image_data = array('upload_data' => $this->upload->data());
        }

        $photo_path = base_url() . "uploads/student_photo/" . $image_data['upload_data']['file_name'];
		//$photo_path2 =  "data:image/jpeg;base64,".base64_encode($photo_path);

    	//signature upload
        if ( ! $this->upload->do_upload('signature_photo')){
            $signature_error = array('error' => $this->upload->display_errors());
            //print_r($signature_error);
		 	$response =array(
			"message" => $signature_error,
			"status" => 'error',
			"error" => 'signature',
			);
			
			echo  json_encode($response); 
			exit;
        }
        else{
            $signature_data = array('upload_data' => $this->upload->data());
        }

        $signature_path = base_url() . "uploads/student_photo/" . $signature_data['upload_data']['file_name'];
 
    	//parent signature upload
        if ( ! $this->upload->do_upload('parent_signature')){
            $signature_error = array('error' => $this->upload->display_errors());
            //print_r($signature_error);
		 	$response =array(
			"message" => $signature_error,
			"status" => 'error',
			"error" => 'parent_signature',
			);
			
			echo  json_encode($response); 
			exit;
        }
        else{
            $parent_signature_data = array('upload_data' => $this->upload->data());
        }

        $parent_signature_path = base_url() . "uploads/student_photo/" . $parent_signature_data['upload_data']['file_name'];
 
   		//categories stuff
	    $sub_cat = array();
	    $string_sub = '';
	    $string_sub2 = '';
	    $disability_type = '';
	    $disability_percentage = '';
	    $disability_number = '';
	    $under_caste = '';
	    $category = $input['category_sought'];
	    $sub_caste = $input['sub_caste'];

	    if($category == 'General'){
	    	$sub_categories = $this->input->post('category_sought_general');
			if(!empty($sub_categories)){
				foreach ($sub_categories as $value) {
					$string_sub .= $value.",";
					$string_sub2 .= $value.",";
					$sub = $value;
					array_push($sub_cat, $sub);
				}
			}
	    	
	    	// print_r($sub_cat);die;

	    	if(in_array('Handicapped', $sub_cat)){
	    		 $disability_type = $input['DisabilityType'];
				 $disability_percentage = $input['DisabilityPercentage'];
				 $disability_number = $input['DisabilityNumber'];
	    	}
	    }

	    if($category == 'Reservation'){
	    	$sub_categories = $this->input->post('category_sought_other');
	    	foreach ($sub_categories as $value) {
	    			$string_sub .= $value.",";
	    			$string_sub2 .= $value.",";
	    			$sub = $value;
	    			array_push($sub_cat, $sub);
	    	}
	    	$string_sub .= "(".$sub_caste.")";
	    	// print_r($sub_cat);die;

	    	if(in_array('Handicapped', $sub_cat)){
	    		 $disability_type = $input['DisabilityType'];
				 $disability_percentage = $input['DisabilityPercentage'];
				 $disability_number = $input['DisabilityNumber'];
	    	}

	    	if(in_array('Caste', $sub_cat)){
	    		 
	    		 $under_caste = $input['under_caste'];
	    	}
	    }

	    $course_id='';
	    $course_name ='';
	    if($_POST['standard']=='XI'){
	    	$course_name ='XI '.$_POST['specialization'];
	    	if($_POST['specialization']=='Home Science'){
	    		if($_POST['course_type']=='Eligible'){
	    			$course_id = 3;
	    		}else{
	    			$course_id = 4;
	    		}
	    	}else if($_POST['specialization']=='Science'){
	    		if($_POST['course_type']=='Eligible'){
	    			$course_id = 1;
	    		}else{
	    			$course_id = 2;
	    		}
	    	}
	    }
	    if($_POST['standard']=='XII'){
	    	$course_name ='XII '.$_POST['specialization'];
	    	if($_POST['specialization']=='Home Science'){
	    		if($_POST['course_type']=='Eligible'){
	    			$course_id = 7;
	    		}else{
	    			$course_id = 8;
	    		}
	    	}else if($_POST['specialization']=='Science'){
	    		if($_POST['course_type']=='Eligible'){
	    			$course_id = 5;
	    		}else{
	    			$course_id = 6;
	    		}
	    	}
	    }
	    
	    $pdf_name_date = date('_mdY_his', time());
	    $date = new DateTime();
		$timestamp = $date->getTimestamp();

		$data = array( 
         "division" => $_POST['standard'], //==null,
         "specialization" => $_POST['course_type'], //== eligible,non-eligible,
         "course_id" => $course_id,
         "course_name" => $course_name, //==XI-Science,
         "choice_subject_XI" => $_POST['choice_subject_XI'],
         "caste_category" => $input['category_sought'],
         "language_of_choice" => $input['language_of_choice'],
         "photo_path" => $photo_path,
         "first_name" => $input['first_name'],
         "last_name" => $input['last_name'],
         "father_name" => $input['father_name'],
         "mother_first_name" => $input['mother_name'],
         "caste" => $under_caste,
         "gender" => 'Female',
         "sub_caste" => @$string_sub,
         "disability_type" => $disability_type,
         "disability_percentage" => $disability_percentage,
         "disability_number" => $disability_number,
         "religion" => $input['religion'],
         "mother_tongue" => $input['mother_tongue'],
         "aadhaar_number" => $input['aadhar_no'],
         "blood_group" => $input['blood_group'],
         "marital_status" => $input['married_status'],
         "date_of_birth" => date('Y-m-d', strtotime($input['date_of_birth'])) ,
         "birth_place" => $input['place_of_birth'],
         "permanent_address" => $input['address'],
         "correspondance_address" => $input['address_native'],
         "email_id" => $input['student_email'],
         "mobile_number" => $input['student_mobile'],
         "bank_acc_no" => $input['bank_acc_no'],
         "ifsc_code" => $input['ifsc_code'],
         "guardian_name" => $input['guardian_name'],
         "guardian_address" => $input['guardian_address'],
         "guardian_email" => $input['guardian_email'],
         "guardian_mobile" => $input['guardian_mobile'],
         "guardian_profession" => $input['guardian_profession'],
         "guardian_income" => $input['annual_income'],
         "relationship_to_guardian" => $input['relationship_to_guardian'],
         "percentage_in_ssc" => $input['percentage_in_ssc'],
         "name_of_examination" => $input['name_of_examination'],
         "year_of_passing" => $input['year_of_passing'],
         "name_of_board" => $input['name_of_board'],
         "name_of_school" => $input['name_of_school'],
         "marks_obtained" => $input['total_marks_obtained'],
         "marks_outof" => $input['out_of'],
         "exam_seat_no" => $input['exam_seat_no'],
         "extra_curricular_achivements" => $input['extra_curricular_achivements'],
         "pdf_path" =>base_url()."uploads/student_pdf/".$timestamp.'-SVT_JR_'.$userID.'_'.$input['last_name'].'_'.$input['first_name'].$pdf_name_date.'.pdf',
         "userID" => $userID,
         "signature_path" => $signature_path,
         "parent_signature" => $parent_signature_path,
         "address_of_school" => $input['address_of_school'],
		);
/* echo "<pre>"; print_r($data); echo "</pre>";
		die; */
		 // $this->user->junior_college_user_details($data);
		// $response =array(
		// 		"message" => "Thank you! You have been successfully registered",
		// 		"pdf_download" => base_url()."uploads/student_pdf/".$timestamp.'-SVT_JR_'.$userID.'_'.$input['last_name'].'_'.$input['first_name'].$pdf_name_date.'.pdf',
		// 		);		
		// echo  json_encode($response);
		// exit;
		// TIMEZONE

		$pdfData = array( 
			         "division" => $_POST['standard'],
			         "specialization" => $_POST['specialization'],
			         "course_id" => $course_id,
			         "course_name" => $course_name, //==XI-Sceince,
			         "course_name" => $_POST['choice_subject_XI'],
			         "choice_subject_XI" => $_POST['choice_subject_XI'],
			         "caste_category" => $input['category_sought'],
			         "language_of_choice" => $input['language_of_choice'],
			         "photo_path" => $photo_path,
			         "first_name" => $input['first_name'],
			         "last_name" => $input['last_name'],
			         "father_name" => $input['father_name'],
			         "mother_first_name" => $input['mother_name'],
			         "caste" => $under_caste."(".$sub_caste.")",
			         "gender" => 'Female',
			         "sub_caste" => @$string_sub2,
			         "disability_type" => $disability_type,
			         "disability_percentage" => $disability_percentage,
			         "disability_number" => $disability_number,
			         "religion" => $input['religion'],
			         "mother_tongue" => $input['mother_tongue'],
			         "aadhaar_number" => $input['aadhar_no'],
			         "blood_group" => $input['blood_group'],
			         "marital_status" => $input['married_status'],
			         "date_of_birth" => date('Y-m-d', strtotime($input['date_of_birth'])) ,
			         "birth_place" => $input['place_of_birth'],
			         "permanent_address" => $input['address'],
			         "correspondance_address" => $input['address_native'],
			         "email_id" => $input['student_email'],
			         "mobile_number" => $input['student_mobile'],
			         "bank_acc_no" => $input['bank_acc_no'],
			         "ifsc_code" => $input['ifsc_code'],
			         "guardian_name" => $input['guardian_name'],
			         "guardian_address" => $input['guardian_address'],
			         "guardian_email" => $input['guardian_email'],
			         "guardian_mobile" => $input['guardian_mobile'],
			         "guardian_profession" => $input['guardian_profession'],
			         "guardian_income" => $input['annual_income'],
			         "relationship_to_guardian" => $input['relationship_to_guardian'],
			         "percentage_in_ssc" => $input['percentage_in_ssc'],
			         "name_of_examination" => $input['name_of_examination'],
			         "year_of_passing" => $input['year_of_passing'],
			         "name_of_board" => $input['name_of_board'],
			         "name_of_school" => $input['name_of_school'],
			         "marks_obtained" => $input['total_marks_obtained'],
			         "marks_outof" => $input['out_of'],
			         "exam_seat_no" => $input['exam_seat_no'],
			         "extra_curricular_achivements" => $input['extra_curricular_achivements'],
			         "pdf_path" =>base_url()."uploads/student_pdf/".$timestamp.'-SVT_JR_'.$userID.'_'.$input['last_name'].'_'.$input['first_name'].$pdf_name_date.'.pdf',
			         "userID" => $userID,
			         "signature_path" => $signature_path,
			         "parent_signature" => $parent_signature_path,
			         "address_of_school" => $input['address_of_school'],
				);





		date_default_timezone_set("Asia/Calcutta");

		
		$parentData = base64_encode($this->file_get_contents_by_curl($parent_signature_path));
		$parentSignature = 'data:image/'.pathinfo($parent_signature_path, PATHINFO_EXTENSION).';base64,'.$parentData;

		 // PDF NAME
		$pdf_name = $timestamp.'-SVT_JR_'.$userID.'_'.$input['last_name'].'_'.$input['first_name'].$pdf_name_date.'.pdf';
 
 		ini_set('max_execution_time', '300');
		ini_set("pcre.backtrack_limit", "5000000");
		// PDF GENERATION
		$mpdf = new \Mpdf\Mpdf([
			'mode' => 'utf-8',
			 'format' => 'A4',
			'autoPageBreak' => true
		]);
		
		
		$test = '';
		$test .= '<table autosize="1" style="width:100%;border:1px solid black; font-family: Arial;padding:13px 10px 25px 13px;">';
		$test .= '<tr><td colspan="2" style="text-align: center;"><h3>INSTRUCTIONS</h3></td></tr>';
		$test .= '<tr><td colspan="2"><br/>Read these carefully before filling up the form<br/><br/>*Under Government directives a certain percentage of seats are reserved under the heads given.Applicants in the Caste category must submit a caste certificate issued by Government of Maharashtra at the time of admission. Applicants in O.B.C category have to attach a Non-Creamy layer certificate in addition to Caste Certification.<br/><br/>To apply in the sport category the candidate must possess certificate issued by the Government of Sports Authority at the District, State or National Level.<br/><br/>#if the applicants not passed the S.S.C examination of the Maharashtra Board, she has to obtain an Eligibility certificate form the Maharashtra Board, failing which the admission will be cancelled.<br/><br/>Please contact the collage office for instructions after admission is finalized.<br/>- A student is eligible for tuition fee exemption if her parents are domiciled in Maharashtra ( at least 15 years ) and if she is the 1st, 2nd, or 3rd child of her parents. Proof of this must be submitted in the form of a Ration Card and a Declaration signed by a parent.<br/><br/><br/><strong>DECLARATION BY PARENT / GUARDIAN</strong><br/><br/>I have permitted my daughter / ward to join your college. The information supplied / her is correct to the best of my knowledge. I have acquainted myself with the rules and fees, dues of my daughter /ward and to see that she observes, the said rules in totally. I hereby give my consent for us of my contact details for educational communication.</td></tr>';
		$test .= '<tr><td><br/><br/>Date: '.date('d/m/Y').'</td><td><br/><br/><div style="display:block;text-align:center;"><img src="'.$parentSignature.'"  style="display:block;width:150px;height:50px;margin:0 auto;"></div>(Signature to the Parent / Guardian)</td><tr/>';
		$test .= '<tr><td><br/><br/>Application Form Submitted on - '.date('d/m/Y h:i:s').'</td><tr/>';
		$test .= '</table>';

	    $html = $this->load->view('student/admission_form_pdf',$pdfData,true); 
	    
	    $mpdf->WriteHTML($html);
	    $mpdf->AddPage();
	    $mpdf->WriteHTML($test);

	    // PDF SERVER LOCATION
	    $location = "uploads/student_pdf/";
	    // OUTPUT PDF SAVING IN DESTINATION
		
		$mpdf->Output($location.$pdf_name, \Mpdf\Output\Destination::FILE);
		// $mpdf->Output($pdf_name, 'D');

		$this->user->junior_college_user_details($data);

	 	$response =array(
			"status" => 'success',
			"message" => "Thank you! You have been successfully registered",
			"pdf_download" => base_url()."uploads/student_pdf/".$timestamp.'-SVT_JR_'.$userID.'_'.$input['last_name'].'_'.$input['first_name'].$pdf_name_date.'.pdf',
		);
		
		echo  json_encode($response); 
		exit;
    }
    
    function file_get_contents_by_curl($url, $params=null) {
        $arrContextOptions=array(
                                "ssl"=>array(
                                    'verify_peer' => false,
                                    'verify_peer_name' => false
                                ),
                            );
    
        $response = file_get_contents($url, false, stream_context_create($arrContextOptions));
        return $response;
    }
    
	//logout
    public function logout() {
    	$this->load->driver('cache');
		$this->session->set_userdata('user_data', null);
		$this->session->sess_destroy();
		$this->cache->clean();
		redirect(site_url());
    }

    function clear_cache() {
        $this->output->set_header("Cache-Control: no-store, no-cache, must-revalidate, no-transform, max-age=0, post-check=0, pre-check=0");
        $this->output->set_header("Pragma: no-cache");
    }

    // ONLINE PROGRAM view
	public function online_program_form() {
        $this->load->view('online_program_form');

	}

    // register view
	public function hostel_form() {
        $this->load->view('hostel-form');

	}

	// hostel_form action as API
	public function hostel_form_api()
    {
	 	 echo "<pre>"; print_r($_POST);print_r($_FILES); echo "</pre>";
		die; 
	}
	
    function test_code() {
        echo "<pre><div style='text-align:center'><h3><b>Its Ak testing code Function </h3></b></div><br>";
        // echo "<pre><div style='text-align:center'><b> </b></div><br>";
    }
  
}
