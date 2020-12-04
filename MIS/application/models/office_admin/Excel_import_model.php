<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Excel_import_model extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }

	public function insert_into_user_details($data) {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert_batch('student_details', $data);
		return $query;
 	}

 	public function insert_into_login_details($data1) {
		//$dbd = $this->load->database('default', TRUE);
		$query = $this->db->insert_batch('login_details', $data1);
		return $query;
 	}
 	
 	public function update_into_user_details($data) {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$query =  $db_query->update_batch('student_details', $data,'userID');
		return $query;
 	}

 	public function insert_into_transaction_details($data) {
		$database = '';
		$connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$query = $db_query->insert_batch('transaction_details', $data);
		return $query;
 	}
}