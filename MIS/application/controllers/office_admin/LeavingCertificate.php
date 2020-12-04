<?php
defined('BASEPATH') OR exit('No direct script access allowed'); 
class LeavingCertificate extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
        $this->load->model('office_admin/LeavingCertificate_model');
        $this->load->model('office_admin/StudentExcelExport_model');
		$this->load->model('student/account_history_model');
	}

	public function index() {	
		//$data['result'] = $this->StudentExcelExport_model->fetch_data();
		$data['courseList'] = $this->StudentExcelExport_model->fetch_course_name();
		$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		$data['leaving_certificate_data'] = $this->LeavingCertificate_model->get_leaving_certificate_details();
		$this->load->view('office_admin/common/header');
		$this->load->view('office_admin/leaving_certificate',$data);
		$this->load->view('office_admin/common/footer');
	}
	public function add($id=NULL) 
	{	
		$data['courseList'] = $this->StudentExcelExport_model->fetch_course_name();
		$data['specialization'] = $this->StudentExcelExport_model->fetch_specialization();
		if(!empty($_POST))
		{						
			$this->load->helper('form');
			$this->load->library('form_validation');
			$this->form_validation->set_rules('registration_number', 'registration_number', 'required');			
			if ($this->form_validation->run() === FALSE) 
			{				
				$this->session->set_flashdata('msg', 'Error occured! Please fill up the form carefully.');	
				redirect("officeadmin/leaving-certificate/add");		
			}
			else
			{				
				if($this->LeavingCertificate_model->add())
				{
					//$this->session->set_flashdata('msg', 'Leaving Certificate Successfully generated');
					//redirect("officeadmin/leaving-certificate");		
					$data['certificate_data'] = $_POST;
					$mpdf = new \Mpdf\Mpdf([
											'format' => 'A4',
											'orientation' => 'P',
											'default_font_size' => 7,
											'default_font'=>'helvetica'
											]);
					
					$title_string = str_replace(' ', '-', $data['certificate_data']['name_of_the_student']);
					$data['certificate_data']['title_string'] = $data['certificate_data']['registration_number']."-".$title_string."-Leaving-Certificate.pdf";					
					$filename = $data['certificate_data']['title_string'];
					//$mpdf->SetHTMLHeader('');			
					$html = $this->load->view('office_admin/leaving_certificate/print_certificate',$data,true);
					$mpdf->WriteHTML($html);
					//$mpdf->Output($filename, 'I');
					$pdf_path = realpath(APPPATH . '../uploads/pdf');
					$mpdf->Output($pdf_path.'/'.$filename, 'F'); 


					$pdf_file = new CURLFile($pdf_path.'/'.$filename,'application/pdf',$data['certificate_data']['registration_number']."-".$title_string);
					// your CURL here
					
					
					$ch = curl_init();					
					curl_setopt($ch, CURLOPT_URL,"https://vancotech.com/dms/api/UploadDocument.ashx?admissionYear=2016&crn=".$data['certificate_data']['registration_number']."&docType=LC");
					curl_setopt($ch, CURLOPT_POST, 1);
					//curl_setopt($ch, CURLOPT_POSTFIELDS, array('name' => 'pdf', 'file' => $pdf_path.'/'.$filename));
					curl_setopt($ch, CURLOPT_POSTFIELDS, ['pdf' => $pdf_file]);					
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
					$server_output = curl_exec($ch);
					if(curl_errno($ch))
					{
						echo 'Request Error:' . curl_error($ch);
					}					
					curl_close ($ch);					
					$mpdf->Output();						
				}
			}				
		}
		else
		{
			
			$data['student'] = $this->StudentExcelExport_model->get_student_by_id($id);			
			$certificate_data = $this->LeavingCertificate_model->get_certificate_by_user_id($data['student']['userID']);			
			$certificate_count = $this->LeavingCertificate_model->count_all_certificate_by_user_id($data['student']['userID']);
			
			if($certificate_count->count > 0)
			{
				$data['student']['userID'] = $certificate_data[0]->registration_number;
				$data['student']['caste'] = $certificate_data[0]->caste;
				$data['student']['sub_caste'] = $certificate_data[0]->sub_caste;
				$data['student']['nationality'] = $certificate_data[0]->nationality;
				$data['student']['religion'] = $certificate_data[0]->religion;
				$data['student']['date_of_birth'] = $certificate_data[0]->date_of_birth;
				$data['student']['birth_place'] = $certificate_data[0]->place_of_birth;
				$data['student']['school_college'] = $certificate_data[0]->last_college_attended;
				$data['student']['date_of_admission'] = $certificate_data[0]->date_of_admission;
				$data['student']['date_of_leaving'] = $certificate_data[0]->date_of_leaving;
				$data['student']['division'] = $certificate_data[0]->grade;
				$data['student']['conduct'] = $certificate_data[0]->conduct;
				$data['student']['reason_for_leaving'] = $certificate_data[0]->reason_for_leaving;
				$data['student']['remarks'] = $certificate_data[0]->remarks;	
				$data['student']['certificate_issued_date'] = $certificate_data[0]->certificate_issued_date;
			}			
			$userID = $data['student']['userID'];
			$data['transaction_details'] = $this->account_history_model->get_transaction($userID);
			
			$data['cur_course'] = $this->StudentExcelExport_model->popuplate_course_data($data['student']['course_id']);
			$this->load->view('office_admin/common/header');
			$this->load->view('office_admin/leaving_certificate/add',$data);
			$this->load->view('office_admin/common/footer');
		}
	}	
}?>