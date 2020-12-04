<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class User extends CI_Model {

	function __construct() {
        parent::__construct();
    }

    public function is_user_exist( $username, $password ) {
		$query = $this->db->get_where('login_details', array('userID' => $username), 1);		
		$row = $query->row();
        //  print_r($row); die;
		if (isset($row)) {
			if(password_verify($password, $row->password)) {
				return $row;
			}
		} else {
			return false;
		}
	}

	public function my_script($admin, $officeadmin1, $officeadmin2, $officeadmin3) {
		//print_r($admin);
		//echo $data['admin']['userID'];
		$query = $this->db->query('SELECT * FROM login_details');
		$rows = $query->num_rows();
		if($rows == '0'){
			$this->db->insert('login_details', $admin);
			$this->db->insert('login_details', $officeadmin1);
			$this->db->insert('login_details', $officeadmin2);
			$this->db->insert('login_details', $officeadmin3);
		}
	}

	public function insert_into_user_details($data) {
		//print_r($data);
		$database = 'clg_db2';

		$db_query = $this->load->database($database, TRUE);
	  	$query = $db_query->insert('student_details', $data);
	  	return $query;
 	}

 	public function get_last_row_userId() {
		//print_r($data);
		$database = 'clg_db2';
		$db_query = $this->load->database($database, TRUE);
		$last_row = $db_query->select('*')->order_by('id',"desc")->limit(1)->get('student_details')->row();
	  	return $last_row;
 	}
		
	public function get_last_row_userIdNew() {
		//print_r($data);
		$database = 'clg_db3';
		$db_query = $this->load->database($database, TRUE);
		$last_row = $db_query->select('*')->order_by('id',"desc")->limit(1)->get('student_details')->row();
	  	return $last_row;
 	}
	
	public function junior_college_user_details($data) {
		//print_r($data);
		$database = 'clg_db3';
		$db_query = $this->load->database($database, TRUE);
	  	$query = $db_query->insert('student_details', $data);
	  	return $query;
 	}

}

?>