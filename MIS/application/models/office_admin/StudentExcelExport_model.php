<?php defined('BASEPATH') OR exit('No direct script access allowed');

class StudentExcelExport_model extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }
	
  	public function fetch_data() {	
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByCaste = $this->input->post('searchByCaste');
		$searchByReligion = $this->input->post('searchByReligion');
		$searchByState = $this->input->post('searchByState');
		$searchByAcademic = $this->input->post('searchByAcademic');
		$searchByCourseYear = $searchByCourseName = '';
		if($this->input->post('searchByCourseYear')){
			$searchByCourseArr = explode('-',$this->input->post('searchByCourseYear'));
			$searchByCourseYear = $searchByCourseArr[0];
			$searchByCourseName = $searchByCourseArr[1];
		}
		$searchBySpec = $this->input->post('searchBySpec');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('student_details.*, course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form')
         ->from('student_details')
         ->join('course_details', 'student_details.course_id = course_details.id');
		if($searchByCourseYear){
			$db_query->where('course_details.year like "%'.$searchByCourseYear.'%"');
		}
		if(!empty($searchByCourseName)){
			if($searchByCourseName == 'Regular'){
				$db_query->where('course_details.course_name != "Honors"');
			} else {
				$db_query->where('course_details.course_name = "'.$searchByCourseName.'"');
			}
		}
		if($searchBySpec){
			$db_query->where('course_details.specialization like "%'.$searchBySpec.'%"');
		}
		$db_query->where('academic_year IN("'.$searchByAcademic.'")');
		//$db_query->where('blood_group like "%'.$searchByBlood.'%"');
		if($searchByState == 'Maharashtra'){
			$db_query->where('permanent_state IN("'.$searchByState.'")');
		}elseif($searchByState == 'Outside Maharashtra'){
			$db_query->where('permanent_state NOT IN("Maharashtra")');
		}
		if($searchByReligion){
			$db_query->where('religion like "%'.$searchByReligion.'%"');
		}
		if($searchByCaste == 'Open'){
			$db_query->where('caste_category like "%'.$searchByCaste.'%"');
		} 
		if($searchByCaste && $searchByCaste != 'Open'){
			$db_query->where('caste like "%'.$searchByCaste.'%"');
		}	
		if($searchValue){
			$db_query->where('(student_details.first_name like "%'.$searchValue.'%" OR student_details.middle_name like "%'.$searchValue.'%" OR student_details.last_name like "%'.$searchValue.'%" OR student_details.mother_first_name like "%'.$searchValue.'%" OR student_details.mobile_number like "%'.$searchValue.'%" OR student_details.roll_number = "'.$searchValue.'" OR student_details.userID = "'.$searchValue.'")');
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		if($_POST['length'] != -1)
		$db_query->limit($_POST['length'],$_POST['start']);
		$query = $db_query->get();
		//echo $db_query->last_query();
		return $query->result();
	}
	
	public function count_all(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->count_all_results('student_details');	
	}
	
	public function count_filtered(){
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByCaste = $this->input->post('searchByCaste');
		$searchByReligion = $this->input->post('searchByReligion');
		$searchByState = $this->input->post('searchByState');
		$searchByAcademic = $this->input->post('searchByAcademic');
		$searchByCourseYear = $searchByCourseName = '';
		if($this->input->post('searchByCourseYear')){
			$searchByCourseArr = explode('-',$this->input->post('searchByCourseYear'));
			$searchByCourseYear = $searchByCourseArr[0];
			$searchByCourseName = $searchByCourseArr[1];
		}
		$searchBySpec = $this->input->post('searchBySpec');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('student_details.*, course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form')
         ->from('student_details')
         ->join('course_details', 'student_details.course_id = course_details.id');
		if($searchByCourseYear){
			$db_query->where('course_details.year like "%'.$searchByCourseYear.'%"');
		}
		if(!empty($searchByCourseName)){
			if($searchByCourseName == 'Regular'){
				$db_query->where('course_details.course_name != "Honors"');
			} else {
				$db_query->where('course_details.course_name = "'.$searchByCourseName.'"');
			}
		}
		if($searchBySpec){
			$db_query->where('course_details.specialization like "%'.$searchBySpec.'%"');
		}
		$db_query->where('academic_year IN("'.$searchByAcademic.'")');
		//$db_query->where('blood_group like "%'.$searchByBlood.'%"');
		if($searchByState == 'Maharashtra'){
			$db_query->where('permanent_state IN("'.$searchByState.'")');
		}elseif($searchByState == 'Outside Maharashtra'){
			$db_query->where('permanent_state NOT IN("Maharashtra")');
		}
		if($searchByReligion){
			$db_query->where('religion like "%'.$searchByReligion.'%"');
		}
		if($searchByCaste == 'Open'){
			$db_query->where('caste_category like "%'.$searchByCaste.'%"');
		} 
		if($searchByCaste && $searchByCaste != 'Open'){
			$db_query->where('caste like "%'.$searchByCaste.'%"');
		}	
		if($searchValue){
			$db_query->where('(student_details.first_name like "%'.$searchValue.'%" OR student_details.middle_name like "%'.$searchValue.'%" OR student_details.last_name like "%'.$searchValue.'%" OR student_details.mother_first_name like "%'.$searchValue.'%" OR student_details.mobile_number like "%'.$searchValue.'%" OR student_details.roll_number = "'.$searchValue.'" OR student_details.userID = "'.$searchValue.'")');
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		$query = $db_query->get();
		return $query->num_rows();
	}
	
	public function export_data(){		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$all_export = $this->input->post('all_export');		
		if($all_export){
			$s_caste = $this->input->post('s_caste');
			$s_religion = $this->input->post('s_religion');
			$s_state = $this->input->post('s_state');
			$s_academic_year = $this->input->post('s_academic_year');
			//$s_course_name = $this->input->post('s_course_name');
			$course_year = $course_name = '';
			if($this->input->post('s_course_name')){
				$searchByCourseArr = explode('-',$this->input->post('s_course_name'));
				$course_year = $searchByCourseArr[0];
				$course_name = $searchByCourseArr[1];
			}
			$s_specialization = $this->input->post('s_specialization');
			$db_query->select('student_details.*')
			 ->from('student_details')
			 ->join('course_details', 'student_details.course_id = course_details.id');
			$db_query->where('course_details.year like "%'.$course_year.'%"');
			if(!empty($course_name)){
				if($course_name == 'Regular'){
					$db_query->where('course_details.course_name != "Honors"');
				} else {
					$db_query->where('course_details.course_name = "'.$course_name.'"');
				}
			}
			$db_query->where('course_details.specialization like "%'.$s_specialization.'%"');
			$db_query->where('academic_year = "'.$s_academic_year.'"');
			$db_query->where('permanent_state like "%'.$s_state.'%"');
			$db_query->where('religion like "%'.$s_religion.'%"');
			if(!empty($s_caste)){
				$db_query->where('caste like "%'.$s_caste.'%"');
			}
			$query = $db_query->get();
		} else {
			$db_query->limit(2);
			$query = $db_query->get('student_details');
		}	
        return $query->result();		
	}
	
	public function fetch_course_name() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->distinct('year, course_name');
		$db_query->select('year,course_name,course_type');
		$query = $db_query->get('course_details');        
        return $query->result();
	}
	
	public function fetch_specialization() { 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->distinct('specialization');
		$db_query->select('specialization');
		$query = $db_query->get('course_details');        		
        return $query->result();
	}
	
	public function get_course_details($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
            $database = SelectDB($connectionString);
        }else if(empty($connectionString)){
            $database = 'clg_db2';
        }else{
            $database = 'clg_db2';
        }
    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('course_details', array('id' => $course_id));
        return $query->result();
    }

	public function add_addon_subject_student(){
		$data = array(
			'userID' => $this->input->post('userID'),
			'semester' => $this->input->post('semester'),
			'subject' => $this->input->post('subject_name'),
			'subject_code' => $this->input->post('subject'),
			'credit' => $this->input->post('credits'),
			'active' => $this->input->post('active'),
			'paid_status' => 'Pending'
        ); 		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->insert('subject_payment_settings', $data);
	}
	
	public function popuplate_course_data($id) { 
		if (empty($id)) {
			show_404();
        }
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->get_where('course_details', array('id' => $id));
		return $query->result();		
	}
	
	public function get_course_id() {		
		$year = $this->input->post('year');
        $specialization = $this->input->post('specialization');
        $course_name = $this->input->post('course_name');
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('id');
		$query = $db_query->get_where('course_details', array('year' => $year,'specialization'=>$specialization,'course_name'=>$course_name ));
		return $query->result();	
	}
   	
   	public function get_student_by_id($id = 0) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        if ($id === 0)
        {
            $query = $db_query->get('student_details');
            return $query->result_array();
        }
        $query = $db_query->get_where('student_details', array('id' => $id));
        return $query->row_array();
    }
	
	public function get_student_by_userId($userid = 0) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('student_details', array('userID' => $userid));
        return $query->row_array();
    }
    
    public function update($id = 0) {
		$data = array(
			'userID' => $this->input->post('college_reg'),
			'course_id' => $this->input->post('courseId'),
			'course_name' => $this->input->post('course_name'),
			'specialization' => $this->input->post('specialization'),
			'roll_number' => $this->input->post('roll_number'),
			'division' => $this->input->post('division'),
			'last_name' => $this->input->post('lastname'),
            'first_name' => $this->input->post('firstname'),
            'middle_name' => $this->input->post('middlename'),			
			'mother_first_name' => $this->input->post('mother_first_name'),
			'date_of_birth' => date('Y-m-d',strtotime($this->input->post('date_of_birth'))),
			'birth_place' => $this->input->post('birth_place'),
			'gender' => $this->input->post('gender'),
			'religion' => $this->input->post('religion'),
			'caste' => $this->input->post('caste'),
			'sub_caste' => $this->input->post('sub_caste'),
			'blood_group' => $this->input->post('blood_group'),
			'correspondance_address' => $this->input->post('correspondance_address'),
			'city' => $this->input->post('city'),
			'location_category' => $this->input->post('location_category'),
			'pin' => $this->input->post('pin'),			
			'state' => $this->input->post('state'),		
			'country' => $this->input->post('country'),
			'permanent_address' => $this->input->post('permanent_address'),
			'permanent_city' => $this->input->post('permanent_city'),
			'permanent_taluka' => $this->input->post('permanent_taluka'),
			'permanent_pin' => $this->input->post('permanent_pin'),
			'permanent_state' => $this->input->post('permanent_state'),
			'permanent_country' => $this->input->post('permanent_country'),
			'mobile_number' => $this->input->post('mobile_number'),
			'email_id' => $this->input->post('email_id'),
			'guardian_income' => $this->input->post('guardian_income'),
			'physical_disability' => (!$this->input->post('physical_disability')) ? 'No' : 'Yes',
			'disability_type' => $this->input->post('disability_type'),
			'disability_number' => $this->input->post('disability_number'),
			'disability_percentage' => $this->input->post('disability_percentage'),
			'previous_exam_state' => $this->input->post('previous_exam_state'),		
			'school_college' => $this->input->post('school_college'),			
			'PRN_number' => $this->input->post('PRN_number'),
			'marital_status' => $this->input->post('marital_status'),
			'aadhaar_number' => $this->input->post('aadhaar_number'),
			'stream' => $this->input->post('stream'),
			'marks_obtained' => $this->input->post('marks_obtained'),			
			'marks_outof' => $this->input->post('marks_outof'),		
			'academic_year' => $this->input->post('academic_year'),
			'name_of_examination' => $this->input->post('name_of_examination'),			
			'left_college' => ($this->input->post('left_college')== 'on')? 'Yes' : 'No',
			'left_college_date' => $this->input->post('left_college_date'),
			'photo_path' => $_POST['photo_path'],
			'father_name' => $this->input->post('middlename'),			
			'mother_tongue' => $this->input->post('mother_tongue'),
			'bank_acc_no' => $this->input->post('bank_acc_no'),
			'ifsc_code' => $this->input->post('ifsc_code'),
			'guardian_name' => $this->input->post('guardian_name'),		
			'guardian_address' => $this->input->post('guardian_address'),
			'acc_holder_name' => $this->input->post('account_holder_name'),
			'guardian_email' => $this->input->post('guardian_email'),
			'guardian_mobile' => $this->input->post('guardian_mobile'),	
			'guardian_profession' => $this->input->post('guardian_profession'),
			'relationship_to_guardian' => $this->input->post('relationship_to_guardian'),
			'percentage_in_ssc' => $this->input->post('percentage_in_ssc'),			
			'year_of_passing' => $this->input->post('year_of_passing'),			
			'name_of_board' => $this->input->post('name_of_board'),			
			'name_of_school' => $this->input->post('name_of_school'),	
			'address_of_school' => $this->input->post('address_of_school'),	
			'exam_seat_no' => $this->input->post('exam_seat_no'),			
			//'extra_curricular_achivements' => $this->input->post('extra_curricular_achivements'),			
			'caste_category' => $this->input->post('caste_category'),	
			'pan_number' => $this->input->post('pan_number'),	
			'voter_id' => $this->input->post('voter_id'),
			'emergency_number' => $this->input->post('emergency_contact'),
			'dropped' => ($this->input->post('dropped_college')== 'on')? 'Yes' : 'No',
        ); 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->where('id', $id);
		$db_query->update('student_details', $data);
		return true;
    }
	
	public function check_reg_exist() {		
		$college_reg = $this->input->post('college_reg');
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('id');
		$db_query->where('userID', $college_reg);
		$query = $db_query->get('student_details');
		return $query->result();
	}	
	
	public function update_password() {
		$new_password = $this->input->post('new_password');
		$conf_password = $this->input->post('conf_password');
		$userid = $this->input->post('cur_user_id');
		$encrypted_pwd = password_hash($new_password, PASSWORD_BCRYPT);
		$this->db->where('userID', $userid);
		$this->db->update('login_details', array('password' => $encrypted_pwd));
		return true;		
	}
	
	
	public function update_photo($file_name) {
		$new_photo = $file_name;
		$userid = $this->input->post('cur_user_id');
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->where('userID', $userid);
		$result = $db_query->update('student_details', array('photo_path' => $new_photo));
		return true;  
	}
	public function update_application_data() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$status = $this->input->post('status');
		$id = $this->input->post('id');
		$db_query->where('id', $id);
		$result = $db_query->update('application_form', array('status' => $status));
		return true;		
	}
	
	public function export_userRollno_data(){		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$course_name = $this->input->post('course_nameE');		
		if($course_name){
			$db_query->select('userID,first_name,last_name,student_details.course_name,course_details.course_name as course_name_type,student_details.specialization,division')
			 ->from('student_details')
			 ->join('course_details', 'student_details.course_id = course_details.id');
			$db_query->where('student_details.course_name = "'.$course_name.'"');
			$db_query->where("left_college LIKE '%No%'  AND dropped LIKE '%No%'");
			//$db_query->limit(3);
			$query = $db_query->get();
		}	
        return $query->result();		
	}

	public function clean_all_rollnumber(){		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$course_name = $this->input->post('course_nameE');		
		if($course_name){
			$db_query->set('roll_number','');
			$db_query->where('course_name = "'.$course_name.'"');
			$db_query->where("left_college LIKE '%No%'  AND dropped LIKE '%No%'");
			//$db_query->limit(3);
			$result = $db_query->update('student_details');
        	return $result;	
		}		
	}
}