<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Home extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '4') {
			redirect(site_url(), 'refresh');
        }
		$this->load->model('staff/staff');
		//$this->load->model('student/course_detail');
		$this->load->library('form_validation');
	}

	public function index() {
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->student->get_entry( $userID );
		$course_id = $data['staff_details'][0]->course_id;
		$data['course_details'] = $this->course_detail->get_entry( $course_id );
		$this->load->view('staff/common/header');
		$this->load->view('staff/home', $data);
		$this->load->view('staff/common/footer');
	}

	public function ftl() {
		$user_id = $this->input->post('id');
		$this->staff->ftl($user_id);
	}

	public function update_basic_details() {
		$userID = $this->input->post('userID');
		$data['email_id'] = $this->input->post('email');
		$data['mobile_number'] = $this->input->post('mobile');
		$data['permanent_address'] = $this->input->post('address');
		$data['state'] = $this->input->post('state');
		$data['pin'] = $this->input->post('zip');
		//$this->student->update_basic_details($data, $id);

		if( $this->student->update_basic_details($data, $userID) ) {
			echo $msg = '<div class="alert alert-success alert-dismissible pop-notice"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Success:</strong> Details updated successfully</div>';
		} else {
			echo $msg = '<div class="alert alert-danger alert-dismissible pop-notice"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Success:</strong> Account cannot updated! Please try again.</span></div>';
		}
	} 
}