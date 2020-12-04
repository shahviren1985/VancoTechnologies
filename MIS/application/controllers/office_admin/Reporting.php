<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Reporting extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
        $this->load->model('office_admin/Reporting_model');
        $this->load->model('office_admin/search_model');
		$this->load->helper('form');
        $this->load->library('form_validation');
	}

	public function index() {	
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/reporting');
		$this->load->view('office_admin/common/footer');
	}	

	public function search_report(){	
		$data['lastname'] = $this->input->post('name');
		$data['challan_no'] = $this->input->post('challan_no');
		$data['start_date'] = $this->input->post('start_date');
		$data['end_date'] = $this->input->post('end_date');
		$search_report = $this->input->post('search_report');	
		if(!empty($search_report)){
			if(empty($data['lastname']) && empty($data['challan_no']) && empty($data['start_date']) && empty($data['end_date'])){
				$this->session->set_flashdata('error', 'Atleast 1 field required.');
				$this->load->view('office_admin/common/header');
				$this->load->view('office_admin/reporting');
				$this->load->view('office_admin/common/footer');
			} else {
				// get data 
				$data['result'] = $this->Reporting_model->fetch_data($data);
				$this->load->view('office_admin/common/header');
				$this->load->view('office_admin/reporting', $data);
				$this->load->view('office_admin/common/footer');
			}
		} else {
			$this->form_validation->set_rules('start_date', 'Start Date', 'required');
			$this->form_validation->set_rules('end_date', 'End Date', 'required');			
			if ($this->form_validation->run() === FALSE) {
				$this->load->view('office_admin/common/header');
				$this->load->view('office_admin/reporting');
				$this->load->view('office_admin/common/footer');
			} else {
			   // get data 
				$data['result'] = $this->Reporting_model->fetch_export_data($data);	
				$connectionString = $this->session->userdata("connectionString");
				if ($connectionString=='officeadmin3') {
					// echo "<pre>";print_r($data['result']);die;
				}
				$this->load->view('office_admin/generate-report', $data);
			}	
		}
	}	
}

?>