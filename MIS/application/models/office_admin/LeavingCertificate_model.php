<?php defined('BASEPATH') OR exit('No direct script access allowed');
class LeavingCertificate_model extends CI_Model { 
	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }
	
	public function add()
	{
		$data = $_POST;
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->insert('leaving_certificates', $data);
	}	
	public function get_leaving_certificate_details(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('*');
		$query = $db_query->get('leaving_certificates');        
		return $query->result();       
	}
	public function get_certificate_by_user_id($userid = 0) {
       	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		
		$query = $db_query->query("SELECT * FROM leaving_certificates WHERE id = (SELECT MAX(id) FROM leaving_certificates WHERE registration_number = '".$userid."')");
		if ($query->num_rows()== 1)
		{
			return $query->result(); 			
		}
    }
	public function count_all_certificate_by_user_id($userid = 0){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->query("SELECT count(*) as count FROM leaving_certificates WHERE id = (SELECT MAX(id) FROM leaving_certificates WHERE registration_number = '".$userid."')");
		if ($query->num_rows()== 1)
		{
			return $query->row();			
		}
		/* $db_query->where('registration_number = "'.$userid.'"');
		$db_query->order_by("created_at", "DESC");
		return $db_query->count_all_results('leaving_certificates');	 */
	}
}