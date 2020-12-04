<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Pages extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
		//$this->load->model('user');
		$this->load->library('form_validation');
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
	}

	public function documents() {
		$userID = $this->session->userdata("userID");
		$data['user_details'] = $this->student->get_entry($userID);
		$user_data = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry($user_data[0]->course_id );
		$this->load->view('student/common/header',$data);
		$this->load->view('student/documents', $data);
		$this->load->view('student/common/footer');
	}
	
	public function feedback() {
		$userID = $this->session->userdata("userID");
		$data['user_details'] = $this->student->get_entry($userID);
		$user_data = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry($user_data[0]->course_id );
		$this->load->view('student/common/header',$data);
		$this->load->view('student/feedback');
		$this->load->view('student/common/footer');
	}
	
	public function performance() {
		$this->load->model('student/student');
		$userID = $this->session->userdata("userID");
		$data['student'] = $this->student->get_entry( $userID );
		$user_data = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry($user_data[0]->course_id );
		$this->load->view('student/common/header', $data);
		$this->load->view('student/performance',$data);
		$this->load->view('student/common/footer');
	}
	public function applications() {
		$userID = $this->session->userdata("userID");
		$data['user_details'] = $this->student->get_entry($userID);
		$user_data = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry($user_data[0]->course_id );
		$this->load->view('student/common/header', $data);
		$this->load->view('student/applications');
		$this->load->view('student/common/footer');
	}
}
