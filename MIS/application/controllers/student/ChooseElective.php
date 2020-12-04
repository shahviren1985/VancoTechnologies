<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class ChooseElective extends CI_Controller {

	public function __construct(){
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
		$this->load->helper('url');    
		$this->load->helper('form');
		$this->load->library('form_validation');
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
	}
	
	public function index() {
		$userID = $this->session->userdata("userID");
		$db_query = $this->load->database('clg_db2', TRUE);
		
		 
		
		
		$data['user_details'] = $this->student->get_entry($userID);
		$user_data = $this->student->get_entry($userID);
		$data['course_details'] = $this->course_detail->get_entry($user_data[0]->course_id );
		//$data['sem'] = 6;
		//$data['year'] = 2020;
		/*if($data['course_details'][0]->course_name == 'Honors'){
			redirect(site_url(), 'refresh');
		} */
		
		
		$this->form_validation->set_rules('post', 'Test', 'trim|required');
         $this->form_validation->set_error_delimiters('<div class="error">', '</div>');
         if ($this->form_validation->run() == true) {
			 
			 
			$field=array();
			$field['elective1']=@$this->input->post('elective1');
			$field['elective2']=@$this->input->post('elective2');
			$field['elective3']=@$this->input->post('elective3');
			$field['elective4']=@$this->input->post('elective4');
			$field['elective5']=@$this->input->post('elective5');
			$field['elective6']=@$this->input->post('elective6');
			$field['elective7']=@$this->input->post('elective7');
			
			
			if (date('m') >= 6 && date('m') < 12) {
				$current_year = date('Y');
			}
			else {
				$current_year = date('Y')+1;
			}
			
			 
			$acyear=  $db_query->from('electives')
			->where('crn',$userID)
			->where('semester',$_REQUEST['semester'])
			//->where('specialisation',$_REQUEST['specialisation'])
			//->where('academicyear',$current_year)
			->get()->row_array();
			
			//echo '<pre>'; print_r($_REQUEST);  echo $db_query->last_query(); die;
			
			if($acyear['id']!=''){
				$db_query->where('id',$acyear['id'])->update('electives',$field);			
			}
			else
			{
				$field['rollnumber']=$data['user_details'][0]->roll_number;
				$field['studentname']=$data['user_details'][0]->first_name.' '.$data['user_details'][0]->middle_name.' '.$data['user_details'][0]->last_name;
				$field['stream']=$_REQUEST['program'];
				$field['semester']=$_REQUEST['semester'];
				$field['specialisation']=@$this->input->post('specialisation');
				$field['department']=@$this->input->post('department');
				$field['crn']=$userID;
				$field['academicyear']=$current_year ;
				$db_query->insert('electives',$field);
			}
			 
			$this->session->set_flashdata('msg','<div class="alert alert-success text-center">Detail updated successfully.</div>');
			redirect('student/choose_elective?program='.$this->input->post('program').'&semester='.$this->input->post('semester'));
        }
		
		
		
		$query= $db_query->from('electives')->where('crn',$userID);
		
		if(@$_REQUEST['program']!=''  &&   @$_REQUEST['semester']!=''){
			$api= file_get_contents("https://vancotech.com/Exams/bachelors/API/api/User/GetGEList?Course=bsc&specialization=".$_REQUEST['program']."&sem=".$_REQUEST['semester']."");
			$data['json'] = json_decode($api, true );
			
			$query->where('semester',$_REQUEST['semester']);
			
		}
		
		
		if (date('m') >= 6 && date('m') < 12) {
				$current_year = date('Y');
		}
		else {
			$current_year = date('Y')+1;
		}
		
		//$query->where('academicyear',$current_year);
		
		$data['feedback']=  $query->get()->row_array();
		
		//echo '<pre>'; echo $db_query->last_query();  echo '</pre>';
		$this->load->view('student/common/header', $data);
		$this->load->view('student/choose-elective',$data);
		$this->load->view('student/common/footer');
	}
}