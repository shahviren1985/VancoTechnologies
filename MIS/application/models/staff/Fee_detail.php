<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Fee_detail extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }

    public function get_entry($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);

    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('fee_details', array('course_id' => $course_id));
        return $query->result();
    }

}