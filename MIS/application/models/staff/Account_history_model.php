<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Account_history_model extends CI_Model {

    public function get_transaction($userID){
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);

        $db_query = $this->load->database($database, TRUE);
		$query = $db_query->order_by('id','DESC')->get_where('transaction_details', array('student_id' => $userID));
		return $query->result(); 
    }
}