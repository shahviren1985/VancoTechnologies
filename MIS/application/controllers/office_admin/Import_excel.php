<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Import_excel extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
		$this->load->helper('select_db');
		$this->load->library('excel');
		$this->load->model('office_admin/excel_import_model');
		$this->load->model('office_admin/StudentExcelExport_model');
		$this->load->helper('url');
	}

	public function index() {	
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/upload_user_excel');
		$this->load->view('office_admin/common/footer');
	}
	public function update_excel() {	
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/update_user_excel');
		$this->load->view('office_admin/common/footer');
	}
	// importing excel data into MYSQL
	public function import() {
		if(isset($_FILES["file"]["name"])) {			
			$filename = $_FILES["file"]["name"];
		    $path = $_FILES["file"]["tmp_name"];
		    $file_directory = "uploads/userDetails";
			$new_file_name = date("d-m-Y")."_".rand(000000, 999999);

			$config['upload_path'] = $file_directory;
            $config['allowed_types'] = 'xls|xlsx';
            $config['file_name'] = $new_file_name;
            $this->load->library('upload', $config);
            if ( ! $this->upload->do_upload('file')) {
                $error = array('error' => $this->upload->display_errors());
            }
            else {
                //$data = array('upload_data' => $this->upload->data());
                $object = PHPExcel_IOFactory::load($path);				
			    foreach($object->getWorksheetIterator() as $worksheet) {					
					$highestRow = $worksheet->getHighestRow();			        
					$highestColumn = $worksheet->getHighestColumn();
					$invID=0;
					for($row=2; $row<=$highestRow; $row++) {
						$data1 = array();
						$data = array();
						$invID++;
			            //$id = $worksheet->getCellByColumnAndRow(0, $row)->getValue();
						$college_registration_id = $worksheet->getCellByColumnAndRow(1, $row)->getValue();
						if($college_registration_id){
							$password = $worksheet->getCellByColumnAndRow(2, $row)->getValue();
							$course_id = $worksheet->getCellByColumnAndRow(3, $row)->getValue();
							$roll_number = $worksheet->getCellByColumnAndRow(4, $row)->getValue();
							$division = $worksheet->getCellByColumnAndRow(5, $row)->getValue();
							$last_name = $worksheet->getCellByColumnAndRow(6, $row)->getValue();
							$first_name = $worksheet->getCellByColumnAndRow(7, $row)->getValue();
							$middle_name = $worksheet->getCellByColumnAndRow(8, $row)->getValue();			            
							$mother_first_name = $worksheet->getCellByColumnAndRow(9, $row)->getValue();
							$date = $worksheet->getCellByColumnAndRow(10, $row)->getValue();
							$date_of_birth = strtotime(  PHPExcel_Style_NumberFormat::toFormattedString($date,'YYYY-MM-DD' ));
							$birth_place = $worksheet->getCellByColumnAndRow(11, $row)->getValue();
							$gender = $worksheet->getCellByColumnAndRow(12, $row)->getValue();
							$religion = $worksheet->getCellByColumnAndRow(13, $row)->getValue();
							$category = $worksheet->getCellByColumnAndRow(14, $row)->getValue();
							$sub_caste = $worksheet->getCellByColumnAndRow(15, $row)->getValue();
							$blood_group = $worksheet->getCellByColumnAndRow(16, $row)->getValue();
							$correspondance_address = $worksheet->getCellByColumnAndRow(17, $row)->getValue();
							$city = $worksheet->getCellByColumnAndRow(18, $row)->getValue();
							$location_category = $worksheet->getCellByColumnAndRow(19, $row)->getValue();
							$pin = $worksheet->getCellByColumnAndRow(20, $row)->getValue();
							$state = $worksheet->getCellByColumnAndRow(21, $row)->getValue();
							$country = $worksheet->getCellByColumnAndRow(22, $row)->getValue();
							$permanent_address = $worksheet->getCellByColumnAndRow(23, $row)->getValue();
							$permanent_city = $worksheet->getCellByColumnAndRow(24, $row)->getValue();
							$permanent_taluka = $worksheet->getCellByColumnAndRow(25, $row)->getValue();
							$permanent_pin = $worksheet->getCellByColumnAndRow(26, $row)->getValue();
							$permanent_state = $worksheet->getCellByColumnAndRow(27, $row)->getValue();
							$permanent_country = $worksheet->getCellByColumnAndRow(28, $row)->getValue();
							$mobile_number = $worksheet->getCellByColumnAndRow(29, $row)->getValue();
							$email_id = $worksheet->getCellByColumnAndRow(30, $row)->getValue();
							$guardian_income = $worksheet->getCellByColumnAndRow(31, $row)->getValue();
							$physical_disability = $worksheet->getCellByColumnAndRow(32, $row)->getValue();
							$disability_type = $worksheet->getCellByColumnAndRow(33, $row)->getValue();						
							$disability_number = $worksheet->getCellByColumnAndRow(34, $row)->getValue();
							$disability_percentage = $worksheet->getCellByColumnAndRow(35, $row)->getValue();
							$previous_exam_state = $worksheet->getCellByColumnAndRow(36, $row)->getValue();
							$school_college = $worksheet->getCellByColumnAndRow(37, $row)->getValue();
							$PRN_number = $worksheet->getCellByColumnAndRow(38, $row)->getValue();
							$marital_status = $worksheet->getCellByColumnAndRow(39, $row)->getValue();
							$aadhaar_number = $worksheet->getCellByColumnAndRow(40, $row)->getValue();
							$stream = $worksheet->getCellByColumnAndRow(41, $row)->getValue();
							$marks_obtained = $worksheet->getCellByColumnAndRow(42, $row)->getValue();
							$marks_outof = $worksheet->getCellByColumnAndRow(43, $row)->getValue();
							$academic_year = $worksheet->getCellByColumnAndRow(44, $row)->getValue();
							$name_of_examination = $worksheet->getCellByColumnAndRow(45, $row)->getValue();
							$left_college = $worksheet->getCellByColumnAndRow(46, $row)->getValue();
							$photo_path = $worksheet->getCellByColumnAndRow(47, $row)->getValue();						
							$choice_subject_XI = $worksheet->getCellByColumnAndRow(48, $row)->getValue();
							$language_of_choice = $worksheet->getCellByColumnAndRow(49, $row)->getValue();
							$mother_tongue = $worksheet->getCellByColumnAndRow(50, $row)->getValue();
							$bank_acc_no = $worksheet->getCellByColumnAndRow(51, $row)->getValue();
							$ifsc_code = $worksheet->getCellByColumnAndRow(52, $row)->getValue();
							$guardian_name = $worksheet->getCellByColumnAndRow(53, $row)->getValue();
							$guardian_address = $worksheet->getCellByColumnAndRow(54, $row)->getValue();
							$guardian_email = $worksheet->getCellByColumnAndRow(55, $row)->getValue();
							$guardian_mobile = $worksheet->getCellByColumnAndRow(56, $row)->getValue();
							$guardian_profession = $worksheet->getCellByColumnAndRow(57, $row)->getValue();
							$relationship_to_guardian = $worksheet->getCellByColumnAndRow(58, $row)->getValue();
							$percentage_in_ssc = $worksheet->getCellByColumnAndRow(59, $row)->getValue();
							$examination_name = $worksheet->getCellByColumnAndRow(60, $row)->getValue();
							$year_of_passing = $worksheet->getCellByColumnAndRow(61, $row)->getValue();		
							$name_of_board = $worksheet->getCellByColumnAndRow(62, $row)->getValue();
							//$name_of_school = $worksheet->getCellByColumnAndRow(63, $row)->getValue();
							$address_of_school = $worksheet->getCellByColumnAndRow(63, $row)->getValue();
							$exam_seat_no = $worksheet->getCellByColumnAndRow(64, $row)->getValue();
							$extra_curricular_achivements = $worksheet->getCellByColumnAndRow(65, $row)->getValue();
							$pdf_path = $worksheet->getCellByColumnAndRow(66, $row)->getValue();
							$signature_path = $worksheet->getCellByColumnAndRow(67, $row)->getValue();
							$caste_category = $worksheet->getCellByColumnAndRow(68, $row)->getValue();
							$nri = $worksheet->getCellByColumnAndRow(69, $row)->getValue();
							$bank_name = $worksheet->getCellByColumnAndRow(70, $row)->getValue();
							$branch = $worksheet->getCellByColumnAndRow(71, $row)->getValue();
							$pan_number = $worksheet->getCellByColumnAndRow(72, $row)->getValue();
							$voter_id = $worksheet->getCellByColumnAndRow(73, $row)->getValue();
							$acc_holder_name = $worksheet->getCellByColumnAndRow(74, $row)->getValue();
							$svt_student = $worksheet->getCellByColumnAndRow(75, $row)->getValue();
							$employement_status = $worksheet->getCellByColumnAndRow(76, $row)->getValue();
							$emergency_number = $worksheet->getCellByColumnAndRow(77, $row)->getValue();
							$hostel_student = $worksheet->getCellByColumnAndRow(78, $row)->getValue();
							$dropped = $worksheet->getCellByColumnAndRow(79, $row)->getValue();
							if($course_id){
								$course_data = $this->StudentExcelExport_model->popuplate_course_data($course_id);
								$course_name = $course_data[0]->year;
								$specialization = $course_data[0]->specialization;
							}
							// encrypted password
							$encrypted_pwd = password_hash($password, PASSWORD_BCRYPT);
							// data for student_details table				
							$data[] = array(
								'userID' => $college_registration_id,
								'password' => $encrypted_pwd,
								'course_id'   => $course_id,
								'course_name'=>$course_name,
								'specialization'=>$specialization,
								'roll_number' => $roll_number,
								'division'=>$division,
								'last_name' => $last_name,
								'first_name' => $first_name,
								'middle_name' => $middle_name,			                
								'mother_first_name' => $mother_first_name,
								'date_of_birth' => ($date_of_birth) ? date('Y-m-d',$date_of_birth) : '',
								'birth_place' => $birth_place,
								'gender' => $gender,
								'religion' => $religion,
								'caste' => $category,
								'sub_caste' => $sub_caste,
								'blood_group' => $blood_group,
								'correspondance_address' => $correspondance_address,
								'city' => $city,
								'location_category' => $location_category,
								'pin' => $pin,
								'state' => $state,
								'country' => $country,
								'permanent_address' => $permanent_address,
								'permanent_city' => $permanent_city,
								'permanent_taluka' => $permanent_taluka,
								'permanent_pin' => $permanent_pin,
								'permanent_state' => $permanent_state,
								'permanent_country' => $permanent_country,
								'mobile_number' => $mobile_number,
								'email_id' => strtolower($email_id),
								'guardian_income' => $guardian_income,
								'physical_disability' => ($physical_disability) ? 'Yes' : 'No',
								'disability_type' => $disability_type,
								'disability_number' => $disability_number,	
								'disability_percentage' => $disability_percentage,							
								'previous_exam_state' => $previous_exam_state,
								'school_college' => $school_college,
								'PRN_number' => str_replace("'", "", $PRN_number),
								'marital_status' => $marital_status,
								'aadhaar_number' => $aadhaar_number,
								'stream'=>$stream,							
								'marks_obtained' => $marks_obtained,
								'marks_outof' => $marks_outof,
								'academic_year' => $academic_year,
								'name_of_examination' => $name_of_examination,
								'left_college' => ($left_college) ? 'Yes' : 'No',
								'photo_path' => $photo_path,
								'choice_subject_XI' => $choice_subject_XI,
								'father_name' => $middle_name,
								'mother_tongue' => $mother_tongue,
								'bank_acc_no' => $bank_acc_no,
								'ifsc_code' => $ifsc_code,
								'guardian_name' => $guardian_name,
								'guardian_address' => $guardian_address,
								'guardian_email' => $guardian_email,
								'guardian_mobile' => $guardian_mobile,
								'guardian_profession' => $guardian_profession,
								'relationship_to_guardian' => $relationship_to_guardian,
								'percentage_in_ssc' => $percentage_in_ssc,							
								'year_of_passing' => $year_of_passing,
								'name_of_board' => $name_of_board,
								//'name_of_school' => $name_of_school,
								'address_of_school' => $address_of_school,
								'exam_seat_no' => $exam_seat_no,
								'extra_curricular_achivements' => $extra_curricular_achivements,
								'pdf_path' => $pdf_path,
								'signature_path' => $signature_path,
								'caste_category' => $caste_category,
								'nri'=>($nri) ? 'Yes' : 'No',
								'admission_year'=>$academic_year,
								'bank_name'=>$bank_name,
								'branch'=>$branch,
								'pan_number'=>strtoupper($pan_number),
								'voter_id'=>$voter_id,
								'acc_holder_name'=>$acc_holder_name,
								'svt_student'=>($svt_student) ? 'Yes' : 'No',
								'employement_status'=>($employement_status) ? 'Employed' : 'Unemployed',
								'emergency_number'=>$emergency_number,
								'hostel_student'=>($hostel_student) ? 'Yes' : 'No',
								'dropped'=>($dropped) ? 'Yes' : 'No',
							);	
							
							// checking connectionString
							$cstring = '';
							$connectionString = $this->session->userdata("connectionString");
							$cstring = SelectDB($connectionString);
							
							
							// data for login_details table
							$data1[] = array(
								//'id' => '',
								'userID' => $college_registration_id,
								'password' => $encrypted_pwd,
								'connectionString' => $cstring,
								'role' => STUDENT
							);
							$this->excel_import_model->insert_into_user_details($data);
							$this->excel_import_model->insert_into_login_details($data1);
							
						}
					}
			    }
				//$this->excel_import_model->insert_into_user_details($data);
				//$this->excel_import_model->insert_into_login_details($data1);
			    echo 'Excel Sheet Data imported successfully!';
            }
		}
	}
	
	// importing excel data to Update into MYSQL
	public function update_import() {
		if(isset($_FILES["file"]["name"])) {			
			$filename = $_FILES["file"]["name"];
		    $path = $_FILES["file"]["tmp_name"];
		    $file_directory = "uploads/user_Detail_Update";
			$new_file_name = date("d-m-Y")."_".rand(000000, 999999);

			$config['upload_path'] = $file_directory;
            $config['allowed_types'] = 'xls|xlsx';
            $config['file_name'] = $new_file_name;
            $this->load->library('upload', $config);
            if ( ! $this->upload->do_upload('file')) {
                $error = array('error' => $this->upload->display_errors());
            }
            else {
                //$data = array('upload_data' => $this->upload->data());
                $object = PHPExcel_IOFactory::load($path);				
			    foreach($object->getWorksheetIterator() as $worksheet) {					
					$highestRow = $worksheet->getHighestRow();			        
					$highestColumn = $worksheet->getHighestColumn();
					$invID=0;
					for($row=2; $row<=$highestRow; $row++) {
						$data = array();
						$data1 = array();
						$data2 = array();
						$data3 = array();
						$data4 = array();
						$data5 = array();
						$data6 = array();
						$data7 = array();
						$data8 = array();
						$data9 = array();
						$finalData = array();
						$invID++;
			            //$id = $worksheet->getCellByColumnAndRow(0, $row)->getValue();
						$college_registration_id = $worksheet->getCellByColumnAndRow(0, $row)->getValue();
						if($college_registration_id){
							$PRN_number = $worksheet->getCellByColumnAndRow(1, $row)->getValue();
							$left_college = $worksheet->getCellByColumnAndRow(2, $row)->getValue();
							$left_college_date = $worksheet->getCellByColumnAndRow(3, $row)->getValue();
							$dropped = $worksheet->getCellByColumnAndRow(4, $row)->getValue();
							$library_card_no = $worksheet->getCellByColumnAndRow(5, $row)->getValue();
							$final_grade = $worksheet->getCellByColumnAndRow(6, $row)->getValue();
							$feedback_received = $worksheet->getCellByColumnAndRow(7, $row)->getValue();
							$roll_number = $worksheet->getCellByColumnAndRow(8, $row)->getValue();
							$course_name = $worksheet->getCellByColumnAndRow(9, $row)->getValue();
							$course_id = $worksheet->getCellByColumnAndRow(10, $row)->getValue();
							// data for student_details table
							
						// ************ Update conditions Only For PRN ***************	
							/*if(!empty($PRN_number)) {
								$data[] = array(
									'userID' => $college_registration_id,
									'PRN_number' => $PRN_number,
									'left_college' => ($left_college) ? 'Yes' : 'No',
									'dropped'=>($dropped) ? 'Yes' : 'No',
								);	
							} else {
								$data[] = array(
									'userID' => $college_registration_id,
									'left_college' => ($left_college) ? 'Yes' : 'No',
									'dropped'=>($dropped) ? 'Yes' : 'No',
								);	
							}*/

				// ************ Update conditions START ***************
							
							$data = array(
									'userID' => $college_registration_id
								);

							if(!empty($PRN_number)) {
								$data1= array(
									'PRN_number' => str_replace("'", "", $PRN_number)
								);	
							} 

							if(!empty($left_college)) {
								if (!empty($left_college_date)) {
									$data2 = array(
										'left_college' => 'Yes',
										'left_college_date' => $left_college_date
									);
								}else {
									$data2 = array(
										'left_college' => $left_college,
										'left_college_date' => ''
									);
								}	
							} 

							if(!empty($dropped)) {
								$data3 = array(
									'dropped' => $dropped
								);
							} 

							if(!empty($library_card_no)) {
								$data4 = array(
									'library_card_no' => $library_card_no
								);
							} 
							if(!empty($final_grade)) {
								$data5 = array(
									'final_grade' => $final_grade
								);
							} 
							if(!empty($feedback_received)) {
								$data6 = array(
									'feedback_received' => $feedback_received
								);
							} 
							if(!empty($roll_number)) {
								$data7 = array(
									'roll_number' => $roll_number
								);
							} 
							if(!empty($course_name)) {
								$data8 = array(
									'course_name' => $course_name
								);
							} 
							if(!empty($course_id)) {
								$data9 = array(
									'course_id' => $course_id
								);
							} 

							$finalData[] = array_merge($data,$data1,$data2,$data3,$data4,$data5,$data6,$data7,$data8,$data9);
					// ************ Update conditions END ***************

							// checking connectionString
							$cstring = '';
							$connectionString = $this->session->userdata("connectionString");
							$cstring = SelectDB($connectionString);
							//echo "<pre>"; print_r($finalData);
							$this->excel_import_model->update_into_user_details($finalData);
							
						}
					}
			    }
			    echo 'Excel Sheet Data imported and updated successfully!';
            }
		}
	}


	// importing excel data into MYSQL FOR TRANSACTIONS PAYMENT VIEW
	public function upload_transaction() {	
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/upload_transaction_excel');
		$this->load->view('office_admin/common/footer');
	}
	// importing excel data into MYSQL FOR TRANSACTIONS PAYMENT
	public function import_transaction() {
		if(isset($_FILES["file"]["name"])) {			
			$filename = $_FILES["file"]["name"];
		    $path = $_FILES["file"]["tmp_name"];
		    $file_directory = "uploads/transaction_uploaded";
			$new_file_name = date("d-m-Y")."_".rand(000000, 999999);

			$config['upload_path'] = $file_directory;
            $config['allowed_types'] = 'xls|xlsx';
            $config['file_name'] = $new_file_name;
            $this->load->library('upload', $config);
            if ( ! $this->upload->do_upload('file')) {
                $error = array('error' => $this->upload->display_errors());
            }
            else {
                //$data = array('upload_data' => $this->upload->data());
               // $path = "uploads/transaction_uploaded/02-10-2020_756746.xlsx";
                $object = PHPExcel_IOFactory::load($path);				
			    foreach($object->getWorksheetIterator() as $worksheet) {					
					$highestRow = $worksheet->getHighestRow();			        
					$highestColumn = $worksheet->getHighestColumn();
					$invID=0;
					for($row=2; $row<=$highestRow; $row++) {
						$data1 = array();
						$data = array();
						$invID++;
			            //$id = $worksheet->getCellByColumnAndRow(0, $row)->getValue();
						$student_id = $worksheet->getCellByColumnAndRow(1, $row)->getValue();
						if($student_id){
							$course_id = $worksheet->getCellByColumnAndRow(2, $row)->getValue();
							$category = $worksheet->getCellByColumnAndRow(3, $row)->getValue();
							$email_id = $worksheet->getCellByColumnAndRow(4, $row)->getValue();
							$challan_number = $worksheet->getCellByColumnAndRow(5, $row)->getValue();
							$previous_challan_number = $worksheet->getCellByColumnAndRow(6, $row)->getValue();
							$fee_paid_date = $worksheet->getCellByColumnAndRow(7, $row)->getValue();
							$firstname = $worksheet->getCellByColumnAndRow(8, $row)->getValue();			            
							$middlename = $worksheet->getCellByColumnAndRow(9, $row)->getValue();
							$lastname = $worksheet->getCellByColumnAndRow(10, $row)->getValue();
							$mothername = $worksheet->getCellByColumnAndRow(11, $row)->getValue();
							$payment_mode = $worksheet->getCellByColumnAndRow(12, $row)->getValue();
							$payment_type = $worksheet->getCellByColumnAndRow(13, $row)->getValue();
							$transaction_ref_number = $worksheet->getCellByColumnAndRow(14, $row)->getValue();
							$transaction_status = $worksheet->getCellByColumnAndRow(15, $row)->getValue();
							$bank_name = $worksheet->getCellByColumnAndRow(16, $row)->getValue();
							$branch_name = $worksheet->getCellByColumnAndRow(17, $row)->getValue();
							$cheque_number = $worksheet->getCellByColumnAndRow(18, $row)->getValue();
							$cheque_date = $worksheet->getCellByColumnAndRow(19, $row)->getValue();
							$late_fee = $worksheet->getCellByColumnAndRow(20, $row)->getValue();
							$total_amount = $worksheet->getCellByColumnAndRow(21, $row)->getValue();
							$balance_amount = $worksheet->getCellByColumnAndRow(22, $row)->getValue();
							$remark = $worksheet->getCellByColumnAndRow(23, $row)->getValue();
							$total_paid = $worksheet->getCellByColumnAndRow(24, $row)->getValue();

							
							// data for student_details table				
							$data[] = array(
								'student_id' => $student_id,
								'course_id'   => $course_id,
								'category'   => $category,
								'email_id'=>$email_id,
								'challan_number'=>$challan_number,
								'previous_challan_number' => $previous_challan_number,
								'fee_paid_date'=>$fee_paid_date,
								'firstname' => $firstname,
								'middlename' => $middlename,
								'lastname' => $lastname,			                
								'mothername' => $mothername,
								'payment_mode' => $payment_mode,
								'payment_type' => $payment_type,
								'transaction_ref_number' => $transaction_ref_number,
								'transaction_status' => $transaction_status,
								'bank_name' => $bank_name,
								'branch_name' => $branch_name,
								'cheque_number' => $cheque_number,
								'cheque_date' => $cheque_date,
								'late_fee' => $late_fee,
								'total_amount' => $total_amount,
								'balance_amount' => $balance_amount,
								'remark' => $remark,
								'total_paid' => $total_paid
							);	
							//echo "<pre>"; print_r($data); die;
							// checking connectionString
							$cstring = '';
							$connectionString = $this->session->userdata("connectionString");
							$cstring = SelectDB($connectionString);
							
							$this->excel_import_model->insert_into_transaction_details($data);
							
						}
					}
			    }
			    echo 'Excel Sheet Data imported successfully!';
            }
		}
	}	
		
}
?>