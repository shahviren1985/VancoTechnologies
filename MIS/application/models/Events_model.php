<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Events_model extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }
	
	public function get_transaction_number_event_registration(){
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->select('transaction_ref_number')
            ->get('events_payment_details');
		return $query->result();	
	}
	
	public function insert_event_registration_data($data) {
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->insert_batch('events_registration_details', $data);
		return $db_query->insert_id();
 	}	
	
	public function insert_transaction($data) {
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert_batch('events_payment_details', $data);
		return $query;
 	}
	
	public function fetch_course_year() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('year');
		$db_query->group_by('year');
		$query = $db_query->get('course_details');        
        return $query->result();
	}
	
	public function fetch_course_spec() {		
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('specialization,short_form');
		$db_query->group_by('specialization');
		$query = $db_query->get('course_details');        
        return $query->result();
	}
	
	public function fetch_application_fee() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->get('fees_structure');
		return $query->result();
	}
	
	public function fetch_course_name($id) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->where('id',$id);
		$query = $db_query->get('course_details');
		$course_array = $query->result();
		$course_name = $course_array[0]->course_name;
		if(empty($course_array[0]->course_name)){
			$course_name = 'Regular';
		}
		return $course_name;
	}	
	
	public function fetch_course_detail($id) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->where('id',$id);
		$query = $db_query->get('course_details');
		return $query->result();
	}
	
	
	public function fetch_course_id($year,$spec,$name) {
		if($name != 'Honors'){
			$name = '';
		}
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('id');
		$db_query->where("year = '".$year."' AND course_name = '".$name."' AND short_form = '".$spec."'");
		$query = $db_query->get('course_details');      
		$result = $query->result();	
		return $result[0]->id;
	}
	
	public function get_entry($userID) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->get_where('student_details', array('userID' => $userID));
		return $query->result();
	}

	public function get_challan_number(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);		
		$query = $db_query->order_by('id',"desc")
            ->limit(1)
            ->get('transaction_details')
            ->row();
		return $query;
	}	
	
/* 	public function insert_transaction($data) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if(empty($connectionString)){
			$connectionString = 'clg_db2';
		}
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert_batch('transaction_details', $data);
		return $query;
 	}	 */
	
	
		
	
	public function insert_alumini_mate_details($data) {
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert('alumini_mate_details', $data);
		return $query;
 	}	
	
	
	public function insert_alumini_event_registration_transaction($data) {
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert_batch('alumini_event_registration_payment_details', $data);
		return $query;
 	}		
	public function alumini_event_registration($data)
	{
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->insert_batch('alumini_event_registration', $data);		
		return $db_query->insert_id();
	}		
	
	

	public function get_transaction_number(){
		$database = '';
        $connectionString = 'clg_db2';
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->select('transaction_ref_number')
            ->get('alumini_event_registration_payment_details');
		return $query->result();	
	}
}?>