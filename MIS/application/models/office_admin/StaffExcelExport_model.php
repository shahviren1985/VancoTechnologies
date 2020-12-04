<?php defined('BASEPATH') OR exit('No direct script access allowed');

class StaffExcelExport_model extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }
	
	public function add(){
		$data = array(
		'firstname' => $this->input->post('member_name'),
		'lastname' => $this->input->post('member_lname'),
		'username' => $this->input->post('username'),
		'email' => $this->input->post('email'),
		'employee_code' => $this->input->post('employee_code'),
		'department' => $this->input->post('department'),
		'type' => $this->input->post('type'),
		'state' => $this->input->post('state'),
		'mobile_number' => $this->input->post('mobile_num'),
		'date_of_joining' => $this->input->post('joining_date'),
		'date_of_retire' => $this->input->post('retire_date'),
		'qualification' => $this->input->post('qualification'),
		'total_experience' => $this->input->post('total_experience'),
		'industry_experience' => $this->input->post('industry_experience'),
		'role' => $this->input->post('role'),
		'pan_number' => $this->input->post('pan'),
		'aadhaar_number' => $this->input->post('aadhaar'),
		'bank_account_number' => $this->input->post('account_number'),
		'bank_name' => $this->input->post('bank_name'),
		'ifsc_code' => $this->input->post('ifsc_code'),
		'account_holder_name' => $this->input->post('account_holder_name'),
		'casual_leave_balance' => $this->input->post('casual_balance'),
		'sick_leave_balance' => $this->input->post('sick_balance'),
		'paid_leave_balance' => $this->input->post('paid_balance'),
		'created_at' => date('Y-m-d')
        ); 		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->insert('staff_details', $data);
	}
	
	public function insert_login_details($data){
		$query = $this->db->insert('login_details', $data);
		return $query;
	}
	
	public function get_staff_details(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('*');
		$query = $db_query->get('staff_details');        
		return $query->result();
       // return $query->row_array();
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
		$searchByType = $this->input->post('searchByType');
		$searchByState = $this->input->post('searchByState');
		$searchByDegree = $this->input->post('searchByDegree');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('*')
         ->from('staff_details');
		if($searchByType){
			$db_query->where('type like "%'.$searchByType.'%"');
		}
		if($searchByState){
			$db_query->where('state like "%'.$searchByState.'%"');
		}
		if($searchByDegree){
			$db_query->where('qualification like "%'.$searchByDegree.'%"');
		}
	
		if($searchValue){
			$db_query->where("department like '%".$searchValue."%' or employee_code like '%".$searchValue."%' or firstname like '%".$searchValue."%' or type like '%".$searchValue."%' or email like '%".$searchValue."%' or state like '%".$searchValue."%' or mobile_number like '%".$searchValue."%' or qualification like'%".$searchValue."%' or role like '%".$searchValue."%'");
			//$db_query->where('(student_details.first_name like "%'.$searchValue.'%" OR student_details.middle_name like "%'.$searchValue.'%" OR student_details.last_name like "%'.$searchValue.'%" OR student_details.mother_first_name like "%'.$searchValue.'%" OR student_details.mobile_number like "%'.$searchValue.'%" OR student_details.roll_number = "'.$searchValue.'" OR student_details.userID = "'.$searchValue.'")');
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
		return $db_query->count_all_results('staff_details');	
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
		$searchByType = $this->input->post('searchByType');
		$searchByState = $this->input->post('searchByState');
		$searchByDegree = $this->input->post('searchByDegree');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('*')
         ->from('staff_details');
		if($searchByType){
			$db_query->where('type like "%'.$searchByType.'%"');
		}
		if($searchByState){
			$db_query->where('state like "%'.$searchByState.'%"');
		}
		if($searchByDegree){
			$db_query->where('qualification like "%'.$searchByDegree.'%"');
		}
	
		if($searchValue){
			$db_query->where("employee_code like '%".$searchValue."%' or firstname like '%".$searchValue."%' or lastname like '%".$searchValue."%' or type like '%".$searchValue."%' or email like '%".$searchValue."%' or state like '%".$searchValue."%' or mobile_number like '%".$searchValue."%' or qualification like'%".$searchValue."%' or role like '%".$searchValue."%'");
			//$db_query->where('(student_details.first_name like "%'.$searchValue.'%" OR student_details.middle_name like "%'.$searchValue.'%" OR student_details.last_name like "%'.$searchValue.'%" OR student_details.mother_first_name like "%'.$searchValue.'%" OR student_details.mobile_number like "%'.$searchValue.'%" OR student_details.roll_number = "'.$searchValue.'" OR student_details.userID = "'.$searchValue.'")');
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
   	
   	public function get_staff_by_id($id = 0) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        if ($id === 0)
        {
            $query = $db_query->get('staff_details');
            return $query->result_array();
        }
        $query = $db_query->get_where('staff_details', array('id' => $id));
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
			'employee_code' => $this->input->post('employee_code'),
			'firstname' => $this->input->post('member_name'),
			'lastname' => $this->input->post('member_lname'),
			'username' => $this->input->post('username'),
			'type' => $this->input->post('type'),
			'department' => $this->input->post('department'),
			'email' => $this->input->post('email'),
			'state' => $this->input->post('state'),
			'mobile_number' => $this->input->post('mobile_number'),
			'date_of_joining' => date('Y-m-d',strtotime($this->input->post('joining_date'))),
			'date_of_retire' => date('Y-m-d',strtotime($this->input->post('retire_date'))),
			'qualification' => $this->input->post('qualification'),
			'total_experience' => $this->input->post('total_experience'),
			'industry_experience' => $this->input->post('industry_experience'),
			'role' => $this->input->post('role'),
			'pan_number' => $this->input->post('pan'),
			'aadhaar_number' => $this->input->post('aadhaar'),
			'bank_account_number' => $this->input->post('account_number'),
			'bank_name' => $this->input->post('bank_name'),
			'ifsc_code' => $this->input->post('ifsc_code'),
			'account_holder_name' => $this->input->post('account_holder_name'),
			'casual_leave_balance' => $this->input->post('casual_balance'),
			'sick_leave_balance' => $this->input->post('sick_balance'),
			'paid_leave_balance' => $this->input->post('paid_balance'),
			'updated_at' => date('Y-m-d')
			
        ); 	
/* 		echo "<pre>"; print_r($data); echo "</pre>";
		die; */
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->where('id', $id);
		return $db_query->update('staff_details', $data);
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

	public function get_staff_details_for_leave_excel(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('employee_code,firstname,lastname,type,casual_leave_balance as Total Leave,casual_leave_balance as Leave Details');
		$db_query->where("type ='Permanent Teaching' OR type ='Permanent Non Teaching'");
		$query = $db_query->get('staff_details');        
		return $query->result();
       // return $query->row_array();
	}
	public function get_all_leave_detail_for_export() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$db_query->select('staff_id,leave_type,leave_from,leave_to');
		$db_query->where("principal_approval_status ='Approved'");
        $query = $db_query->get('leave_application');
		return $query->result();
    } 
	public function staff_leave_data($startDate,$endDate){		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$startDate = $this->input->post('startdate');
		$endDate = $this->input->post('enddate');

		$db_query->select('*')
		 ->from('leave_application');
		//if(!empty($startDate)){
			$db_query->where('leave_from BETWEEN "'.$startDate.'" AND "'.$endDate.'"');
		//}
		$query = $db_query->get();
		//echo $this->db->get_compiled_select();
		//echo $this->db->last_query(); die('dsdsdsd');
		 
        return $query->result();		
	}
	public function get_all_leave_detail_for_list() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$startDate = $this->input->post('startdate');
		$endDate = $this->input->post('enddate');
		$db_query->select('staff_id,employee_code,firstname,lastname,type,casual_leave_balance,sick_leave_balance,paid_leave_balance,compensate_leave,half_day_leave,unpaid_leave,leave_type,leave_from,leave_to')
			 ->from('leave_application')
			 ->join('staff_details','staff_details.employee_code = leave_application.staff_id');
		if(!empty($startDate) && !empty($endDate)){
			$db_query->where('leave_from BETWEEN "'.$startDate.'" AND "'.$endDate.'"');
		}
		else{
			$db_query->where('leave_from BETWEEN "'.date('Y-m-1').'" AND "'.date('Y-m-30').'"');
		}		 
		$db_query->where("principal_approval_status ='Approved'");
        $query = $db_query->get();
		return $query->result();
    }
	public function get_all_leave_detail_for_exportE() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$startDate = $this->input->post('startdateExcel');
		$endDate = $this->input->post('enddateExcel');
		$db_query->select('staff_id,employee_code,firstname,lastname,type,casual_leave_balance as Total Leave Taken,casual_leave_balance as CL,sick_leave_balance as SL,paid_leave_balance as PL,compensate_leave as Comp Off,half_day_leave as Half Pay,unpaid_leave as Unpaid,casual_leave_balance as Leave Details,leave_type,leave_from,leave_to')
			 ->from('leave_application')
			 ->join('staff_details','staff_details.employee_code = leave_application.staff_id');
		if(!empty($startDate) && !empty($endDate)){
			$db_query->where('leave_from BETWEEN "'.$startDate.'" AND "'.$endDate.'"');
		}else{
			$db_query->where('leave_from BETWEEN "'.date('Y-m-1').'" AND "'.date('Y-m-30').'"');
		}	 
		$db_query->where("principal_approval_status ='Approved'");
        $query = $db_query->get();
		return $query->result();
    } 
    public function get_all_leave_detail_for_exportAnnual($employee_code) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$startDate = $this->input->post('startdateExcel');
		$endDate = $this->input->post('enddateExcel');
		//$employee_code = $this->input->post('employee_code');
		//$employee_code = '00028';
		$db_query->select('staff_id,employee_code,firstname,lastname,type,casual_leave_balance as Total Leave Taken,casual_leave_balance as CL,sick_leave_balance as SL,paid_leave_balance as PL,compensate_leave as Comp Off,half_day_leave as Half Pay,unpaid_leave as Unpaid,casual_leave_balance as Leave Details,leave_type,leave_from,leave_to')
			 ->from('leave_application')
			 ->join('staff_details','staff_details.employee_code = leave_application.staff_id');
		if(!empty($startDate) && !empty($endDate)){
			$db_query->where('leave_from BETWEEN "'.$startDate.'" AND "'.$endDate.'"');
		}	
		$db_query->where('employee_code ="'.$employee_code.'"'); 
		$db_query->where("principal_approval_status ='Approved'");
        $query = $db_query->get();
		return $query->result();
    }  
}