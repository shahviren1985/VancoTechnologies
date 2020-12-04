<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Feedback extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->helper('form');
		$this->load->library('form_validation');
	}
	
	public function index() {
		
		
		//cc=102&type=Parent&crn=6101&year=2019
		 $db_query = $this->load->database('clg_db2', TRUE);
		$question_group=@$_GET['type'];
		$college_id=@$_GET['cc'];
		$year=@$_GET['year'];
		$crn=@$_GET['crn'];
		
		
		
		$this->form_validation->set_rules('feedback_group_id', 'Group', 'trim|required');
        if ($this->form_validation->run() == true){	
	
			$feedback =$this->input->post('rd');
			
			foreach($feedback as $feedback_id=>$answer){
				$insert=array();
				$insert['feedback_group_id'] = $this->input->post('feedback_group_id');
				$insert['feedback_id'] = $feedback_id;
				$insert['user_id'] = ($crn!='')?$crn:@$this->session->userdata('id');
				$insert['year_from'] = $year;
				$insert['year_to'] =  $year+1;
				$insert['answer'] = $answer;
				$insert['created_at'] = date('Y-m-d H:i:s');
				$insert['ip_address'] = $_SERVER['REMOTE_ADDR'];
				$db_query->insert('feedback_answers',$insert );
				// die;
				//echo '<pre>';print_r($insert);'</pre>'; die;
				
			}
			
			$this->session->set_flashdata('msg','<div class="alert alert-success text-center">Feedback successfully saved.</div>');
			redirect("feedback?cc=$college_id&type=$question_group&crn=$crn&year=$year", "refresh");
        }
		
		$db_query->select('*');
		$db_query->from('feedback_groups');
		$group= $db_query->where('question_group',@$question_group)->get()->row_array();
		
		
		$feeback_submitted=false;
		if($crn!=''){
			$db_query->select('*')
			->from('feedback_answers')
			->where('feedback_group_id',@$group['id'])
			->where('user_id',$crn)
			->where('year_from',$year)
			->where('year_to',($year+1));
			$total= $db_query->get()->num_rows();
			if($total>0){
				$feeback_submitted=true;
			}
		}
		
		$data['feeback_submitted'] = $feeback_submitted;
		
		
		
		
		
		$uType='';
		switch ($group['question_group']) {
        case "TeacherPO":
            $uType = "Teacher Program Outcome";
            break;
        case "AlumniPODC":
            $uType = "Alumni (DC) Program Outcome";
            break;
        case "AlumniPOECCE":
            $uType = "Alumni (ECCE) Program Outcome";
            break;
        case "AlumniPOFND":
            $uType = "Alumni (FND) Program Outcome";
            break;
        case "AlumniPOHTM":
            $uType = "Alumni (HTM) Program Outcome";
            break;
        case "AlumniPOIDRM":
            $uType = "Alumni (IDRM) Program Outcome";
            break;
        case "AlumniPOMCE":
            $uType = "Alumni (MCE) Program Outcome";
            break;
        case "AlumniPOTAD":
            $uType = "Alumni (TAD) Program Outcome";
            break;
        case "ParentsPODC":
            $uType = "Parents (DC) Program Outcome";
            break;
        case "ParentsPOECCE":
            $uType = "Parents (ECCE) Program Outcome";
            break;
        case "ParentsPOFND":
            $uType = "Parents (FND) Program Outcome";
            break;
        case "ParentsPOHTM":
            $uType = "Parents (HTM) Program Outcome";
            break;
        case "ParentsPOIDRM":
            $uType = "Parents (IDRM) Program Outcome";
            break;
        case "ParentsPOMCE":
            $uType = "Parents (MCE) Program Outcome";
            break;
        case "ParentsPOTAD":
            $uType = "Parents (TAD) Program Outcome";
            break;
		case "SSSFY":
		case "SSSSY":
		case "SSSTY":
		case "SSSMFY":
		case "SSSMSY":
			$uType = "Student Satisfaction Survey";
			break;
		case "StudentCurriculumFY":
		case "StudentCurriculumSY":
		case "StudentCurriculumTY":
			$uType = "Student Curriculum";
			break;
        default:
            $uType = $group['question_group'];
    }
		$year_from=$year;
		$year_to= $year+1;
		$db_query->select('*');
		$db_query->from('feedbacks');
		$db_query->where('status','active');
		$db_query->where('college_id',$college_id);
		$db_query->where('year_from',$year_from);
		$db_query->where('year_to',$year_to);
		$db_query->where('feedback_group_id',@$group['id']);
		$question_array= $db_query->get()->result_array();
		if($question_array){
			foreach($question_array as $key=>$value){
				
					$question_option= $db_query->from('feedback_options')
					->where('feedback_id',$value['id'])
					->order_by('priority','asc')
					->get()
					->result_array();
				$question_array[$key]['options']= $question_option;
			}
		}
		//echo $db_query->last_query();
		//echo '<pre>';print_r($question_array);'</pre>';
		
		$data['uType'] = $uType;
		$data['group'] = $group;
		$data['question_array'] = $question_array;
		$this->load->view('feedback', $data);
	}
	
}

