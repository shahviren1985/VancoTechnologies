<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Account_history extends CI_Controller {
	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
        $this->load->helper('select_db');
		$this->load->model('student/account_history_model');
		$this->load->model('student/course_detail');
		$this->load->model('student/student');
	}

	public function index() {
		$userID = $this->session->userdata('userID');
		$user_details = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry( $user_details[0]->course_id );
        $data['transaction_details'] = $this->account_history_model->get_transaction($userID);
		$this->load->view('student/common/header', $data);
		$this->load->view('student/account_history',$data);
		$this->load->view('student/common/footer');
	}

}
