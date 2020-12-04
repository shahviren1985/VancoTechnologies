<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Search_model extends CI_Model {

	 public function get_student($userID) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db2';
        }
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('student_details', array('userID' => $userID));
        return $query->result();
    }

     public function get_course($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('course_details', array('id' => $course_id));
        return $query->result();
    }

    public function get_fees($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db2';
        }

    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('fee_details', array('course_id' => $course_id));
        return $query->result();
    }
    
    public function get_all_fees_head() {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db2';
        }
    	$db_query = $this->load->database($database, TRUE);
    	$db_query->select('DISTINCT(fee_head)')->from('fee_details');
		$query = $db_query->get();
        return $query->result();
    }

    public function get_late_fees_settings() {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
       if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db2';
        }
    	$db_query = $this->load->database($database, TRUE);
        $db_query->select('key,value')
         ->from('config_settings');
		$db_query->where('key = "late fee date" OR key = "late fee amount"');
		$query = $db_query->get();
		//echo $db_query->last_query();
        return $query->result();
    }

    public function get_late_fees_amount() {
    	$late_fee_amount = 0;
    	$current_date = date('Y-m-d'); //'2020-06-09';//
        $late_fee_data = $this->get_late_fees_settings();
		/*echo "<br>".*/$config_late_date = $late_fee_data[0]->value;
		/*echo "<br>".*/$config_late_fees = $late_fee_data[1]->value;
        /*echo "<br>".*/$cal = (strtotime($current_date) - strtotime($config_late_date))/60/60/24;
        if ($cal<0) {
        	$late_fee_amount = 0;
        }else {
        	/*echo "<br>".*/$late_fee_amount = abs(ceil($cal/7)*$config_late_fees);        	
        }
		
        return $late_fee_amount;
    }
    
    public function count_student(){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);

        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->count_all_results('student_details');
        return $query;
    }
	
	public function get_challan_number(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        /* $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);	 */
        if($connectionString == 'officeadmin1'){
			$db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}		
		$query = $db_query->order_by('id',"desc")
            ->limit(1)
            ->get('transaction_details')
            ->row();
		return $query;
	}
	
	public function insert_transaction($data) {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		/* $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE); */
		if($connectionString == 'officeadmin1'){
			$db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}
		$query = $db_query->insert_batch('transaction_details', $data);
		return $query;
 	}	
	
	public function get_recent_transaction($limit) {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		/* $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE); */
		if($connectionString == 'officeadmin1'){
			$db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}
		$query = $db_query->order_by('id',"desc")
            ->limit($limit)
            ->get('transaction_details');
		return $query->result();
 	}
	
	public function get_todays_transaction() {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		/* $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE); */
		if($connectionString == 'officeadmin1'){
			$db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}
		$date_now = date('Y-m-d');
		$query = $db_query->get_where('transaction_details', array('fee_paid_date' => $date_now));
		return $query->result();
 	}
	
	public function update_transaction_status($data,$id){
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		/* $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE); */
		if($connectionString == 'officeadmin1'){
			$db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}
		$query = $db_query->update('transaction_details', $data, array('id' => $id));
		return $query;
	}

	public function fetch_transaction_data($data) {
		$where_query = '';
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);

		$db_query = $this->load->database($database, TRUE);

		$lastname = @$data['lastname'];
		$challan_no = $data['challan_no'];
		$start_date = @$data['start_date'];
		$end_date = @$data['end_date'];
		
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
		$db_query->order_by('id', 'ASC');
		$query = $db_query->get();
		return $query->result();
   	}

   	public function get_junior_transaction_data($data) {
		$where_query = '';
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		/*if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db3';
        }*/
        $database = 'clg_db3'; // SET FOR FY JC

		$db_query = $this->load->database($database, TRUE);

		$lastname = @$data['lastname'];
		$challan_no = $data['challan_no'];
		$start_date = @$data['start_date'];
		$end_date = @$data['end_date'];
		
		$db_query->select('transaction_details.*')
         ->from('transaction_details');
        // ->join('course_details', 'transaction_details.course_id = course_details.id');
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
		$db_query->order_by('id', 'ASC');
		$query = $db_query->get();
		return $query->result();
   	}

   	public function get_student_junior($userID) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db3';
        }
        $database = 'clg_db3';
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('student_details', array('userID' => $userID));
        return $query->result();
    }

    public function get_course_junior($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db3';
        }
        $database = 'clg_db3';
    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('course_details', array('id' => $course_id));
        return $query->result();
    }

    public function get_junior_fees($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if(!empty($connectionString)){
        	$database = SelectDB($connectionString);
        }else {
            $database = 'clg_db3';
        }
        $database = 'clg_db3';
    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('fee_details', array('course_id' => $course_id));
        return $query->result();
    }
	
}