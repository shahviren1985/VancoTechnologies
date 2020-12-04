<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Student extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }

    public function ftl($id) {		
    	$this->db->where('id', $id);
	    $this->db->update('login_details', array('first_time_login' => '1'));
	    return true;
    }

    public function update_basic_details($data, $userID) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);		
        $db_query = $this->load->database($database, TRUE);
    	$query = $db_query->update('student_details', $data, array('userID' => $userID));
		return $query;
    }

    public function get_entry($userID) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($userID)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }
        
		$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('student_details', array('userID' => $userID));
		return $query->result();
    }
	
	public function insert_transaction($data) {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db2';
        }
		$db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert_batch('transaction_details', $data);
		return $query;
 	}	
	
	public function get_challan_number(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db2';
        }
        $db_query = $this->load->database($database, TRUE);		
		$query = $db_query->order_by('id',"desc")
            ->limit(1)
            ->get('transaction_details')
            ->row();
		return $query;
	}

	public function get_transaction($userID){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($userID)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }

        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->order_by('id','DESC')->get_where('transaction_details', array('student_id' => $userID));
		return $query->result(); 
    }

    public function get_transaction_by_transactionId($userID){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($userID)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }

        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->order_by('id','DESC')->get_where('transaction_details', array('transaction_ref_number' => $transaction_ref_number));
		return $query->result(); 
    }
	
	 public function get_student_by_id($id = 0) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($id)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }
        $db_query = $this->load->database($database, TRUE);
        if ($id === 0)
        {
            $query = $db_query->get('student_details');
            return $query->result_array();
        }
        $query = $db_query->get_where('student_details', array('userID' => $id));
        return $query->row_array();
    }
	
	public function update($id = 0) {
		$data = array(
			'userID' => $this->input->post('college_reg'),
			'course_id' => $this->input->post('courseId'),
			'course_name' => $this->input->post('curr_course'),
			'specialization' => $this->input->post('curr_specialization'),
			'roll_number' => $this->input->post('roll_number'),
			'division' => $this->input->post('curr_division'),
			'last_name' => $this->input->post('lastname'),
            'first_name' => $this->input->post('firstname'),
            'middle_name' => $this->input->post('middlename'),			
			'mother_first_name' => $this->input->post('mother_first_name'),
			'date_of_birth' => date('Y-m-d',strtotime($this->input->post('date_of_birth'))),
			'birth_place' => $this->input->post('birth_place'),
			'gender' => $this->input->post('curr_gender'),
			'religion' => $this->input->post('religion'),
			'caste' => $this->input->post('curr_caste'),
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
			'academic_year' => $this->input->post('curr_academic_year'),
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
		$db_query->where('userID', $id);
		return $db_query->update('student_details', $data);
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
	public function get_applications_by_id($id = 0) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        if ($id === 0)
        {
            $query = $db_query->get('application_form');
            return $query->result();
        }
        $query = $db_query->get_where('application_form', array('user_id' => $id));
		return $query->result();
    }

    public function add_application(){
		$data = array(
			'application_type' => $this->input->post('application_type'),
			'application_reason' => $this->input->post('application_reason'),
			'user_id' => $this->input->post('user_id'),
			'user_name' => $this->input->post('user_name'),
			'application_doc' => $this->input->post('application_doc'),
			'status' => 'Pending',
			'date_applied' => date('Y-m-d')
        ); 		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->insert('application_form', $data);
	}

	public function get_addon_subject_by_userid($userID){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($userID)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }

        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->order_by('id','DESC')->get_where('subject_payment_settings', array('userID' => $userID,'active' => 1,'paid_status' => 'Pending'));
		return $query->result(); 
    }

     public function update_addon_subject_by_userid($userID){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($userID)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }

        $db_query = $this->load->database($database, TRUE);
        
        $db_query->where('userID', $userID);
        $db_query->where('active', 1);
        $db_query->where('paid_status', 'Pending');
		return $db_query->update('subject_payment_settings', array('paid_status' => 'Paid','active' => 2,));
    }
    public function update_addon_subject_by_subject_id($subject_id){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else if(empty($connectionString)){
        	if(is_numeric($userID)){
            	$database = 'clg_db2';
        	}else{
            	$database = 'clg_db1';        		
        	}
        }else{
            $database = 'clg_db2';
        }

        $db_query = $this->load->database($database, TRUE);
        
        $db_query->where('id', $subject_id);
		return $db_query->update('subject_payment_settings', array('paid_status' => 'Paid','active' => 2,));
    }
}