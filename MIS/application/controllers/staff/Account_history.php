<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Account_history extends CI_Controller {
	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
        $this->load->helper('select_db');
		$this->load->model('student/account_history_model');
	}

	public function index() {
		$userID = $this->session->userdata('userID');
        $data['transaction_details'] = $this->account_history_model->get_transaction($userID);
		$this->load->view('student/common/header');
		$this->load->view('student/account_history',$data);
		$this->load->view('student/common/footer');
	}

}
