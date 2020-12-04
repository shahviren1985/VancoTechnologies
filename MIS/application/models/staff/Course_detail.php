<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Course_detail extends CI_Model {

	public function __construct() {
        parent::__construct();
    }

    public function get_entry($course_id) {
    	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if($connectionString == 'clg_db1'){
            $database = 'clg_db1';
        }else if($connectionString == 'clg_db2'){
            $database = 'clg_db2';
        }else{
            $database = 'clg_db3';
        }

    	$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('course_details', array('id' => $course_id));
        return $query->result();
    }

}