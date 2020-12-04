<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Reporting_model extends CI_Model {

	  public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }


  	public function fetch_data($data) {
		$where_query = '';
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$lastname = $data['lastname'];
		$challan_no = $data['challan_no'];
		$start_date = $data['start_date'];
		$end_date = $data['end_date'];
		
		/* $db_query->select('transaction_details.*, course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form,student_details.id as student_edit_id')
         ->from('transaction_details')
         ->join('course_details', 'transaction_details.course_id = course_details.id')
		 ->join('student_details', 'transaction_details.student_id = student_details.userID');
		$where_query .= '1=1'; */
		
		$db_query->select('transaction_details.*, course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form')
         ->from('transaction_details')
         ->join('course_details', 'transaction_details.course_id = course_details.id');
		$where_query .= '1=1';
		
		if($start_date && $end_date){
			$where_query .= ' AND fee_paid_date BETWEEN "'. date('Y-m-d', strtotime($start_date)). '" and "'. date('Y-m-d', strtotime($end_date)).'"';
		} else {
			if($start_date){
				$where_query .= ' OR fee_paid_date BETWEEN "'. date('Y-m-d', strtotime($start_date)). '" and "'. date('Y-m-d', time()).'"';
			}
		}
		if($lastname){
			$where_query .= ' AND lastname LIKE "%' . $lastname.'%"';
		}
		if($challan_no){
			$where_query .= ' AND challan_number IN ("'.$challan_no.'")';
		}
		$db_query->where($where_query);
		$query = $db_query->get();
		return $query->result();
   	}
	
	public function fetch_export_data($data) {
		$where_query = '';
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);

		$db_query = $this->load->database($database, TRUE);

		$lastname = $data['lastname'];
		$challan_no = $data['challan_no'];
		$start_date = $data['start_date'];
		$end_date = $data['end_date'];
		
		$db_query->select('transaction_details.*, course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form')
         ->from('transaction_details')
         ->join('course_details', 'transaction_details.course_id = course_details.id');
		$where_query .= '1=1';
		
		if($start_date && $end_date){
			$where_query .= ' AND fee_paid_date BETWEEN "'. date('Y-m-d', strtotime($start_date)). '" and "'. date('Y-m-d', strtotime($end_date)).'"';
		}
		if($lastname){
			$where_query .= ' AND lastname LIKE "%' . $lastname.'%"';
		}
		if($challan_no){
			$where_query .= ' AND challan_number IN ("'.$challan_no.'")';
		}
		$db_query->where($where_query);
		$db_query->order_by('fee_paid_date', 'ASC');
		$db_query->order_by('id', 'ASC');
		$query = $db_query->get();
		return $query->result();
   	}
}