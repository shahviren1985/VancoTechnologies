<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class UserRoles extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '1') {
			redirect(site_url(), 'refresh');
        }
        $this->load->model('admin/user_roles');
		$this->load->library('form_validation');
		$this->load->library('session');
	}

	public function index() {
		$data['user_roles'] = $this->user_roles->get_entries();
		//print_r($data);die;
		$this->load->view('admin/common/header');
		$this->load->view('admin/user_roles/view', $data);
		$this->load->view('admin/common/footer');
	}

	public function edit( $id ) {
		$data['user_role'] = $this->user_roles->get_entry( $id );
		//print_r($data);die;
		$this->load->view('admin/common/header');
		$this->load->view('admin/user_roles/edit', $data);
		$this->load->view('admin/common/footer');
	}

	public function update() {
		$id = trim($this->input->post('id'));
		
		if( !isset($id) || empty($id) ) {
			redirect('admin/user/roles');
		}

		$this->form_validation->set_rules('role', 'Role', 'trim|required|regex_match[/^[a-zA-Z\s]+$/]');
		
		$data['role'] = strtolower(trim($this->input->post('role')));

		if ( $this->form_validation->run() == FALSE ) {
			$data['user_role'] = $this->user_roles->get_entry( $id );
			$this->load->view('admin/common/header');
			$this->load->view('admin/user_roles/edit', $data);
			$this->load->view('admin/common/footer');
		} else {
			
			if( $this->user_roles->update_entry($data, $id) ) {
				$msg = '<span class="text-success ml-5">User Role updated successfully.</span>';
				$this->session->set_tempdata( 'message', $msg, 5);
				redirect('admin/user/roles');
			} else {
				$msg = '<div class="text-danger  ml-5">Transport mode cannot update! Please try again.</span>';
				$this->session->set_tempdata( 'message', $msg, 5);
				$this->load->view('admin/common/header');
				$this->load->view('admin/user_roles/view');
				$this->load->view('admin/common/footer');
			}
		}
	}
}

?>