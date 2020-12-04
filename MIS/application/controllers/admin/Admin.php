<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Admin extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '1') {
			redirect(site_url(), 'refresh');
        }
	}

	public function index() {	
		$this->load->view('admin/common/header');
		$this->load->view('admin/home');
		$this->load->view('admin/common/footer');
	}

}

?>