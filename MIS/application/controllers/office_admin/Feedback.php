<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Feedback extends CI_Controller {
	public function __construct() {
		parent::__construct();
		
		
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '2') {
			redirect(site_url(), 'refresh');
        }
		
        $this->load->helper('select_db');
		$this->load->library('form_validation');
	}

	public function index() {
		
		$data= array();
		$db_query = $this->load->database('clg_db2', TRUE);
		
		// get feedback group
		
		
		
		$group=array();;
		$feedback_type=  @$_GET['feedback_type'];
		if($feedback_type!=''){
			$db_query->select('*');
			$db_query->from('feedback_groups');
			$db_query->where('feedback_type',$_GET['feedback_type']);
			if($feedback_type=='SSS' ||  $feedback_type== 'StudentCurriculum'){
				if(@$_GET['program']!=''){
					$db_query->where('program',@$_GET['program']);
				}
			}
			elseif($feedback_type=='PO'){
				
				if(@$_GET['stakeholder']!=''){
					$db_query->where('stackholder',@$_GET['stakeholder']);
				}
				
				if(@$_GET['specialization']!=''){
					$db_query->where('specialization',@$_GET['specialization']);
				}
			}
			
			$group= $db_query->limit(1)->get()->row_array();
		}
		
		//echo $db_query->last_query();
		$year = explode("-", @$_GET['feedback_year']);
		$year_from= @$year[0];
		$year_to= @$year[1];
		
	
		$db_query->select('*');
		$db_query->from('feedbacks');
		if($year_from!=''){
			$db_query->where('year_from',$year_from);
		}
		if($year_to!=''){
			$db_query->where('year_to',$year_to);
		}
		
		if($feedback_type!=''){
			
			if(@$group['id']!=''){
				$db_query->where('feedback_group_id',@$group['id']);
			}
			else{
				$db_query->where('feedback_group_id','');
			}
		}
		$data['feedbacks']= $db_query->get()->result_array();
		//echo $db_query->last_query();
		
		$this->load->view('office_admin/common/header', $data);
		$this->load->view('office_admin/feedback/index',$data);
		$this->load->view('office_admin/common/footer');
	}
	
	
	public function add(){
		
		$data= array();
		$db_query = $this->load->database('clg_db2', TRUE);
		
		// get feedback group
		
		$group= array();
		$feedback_type=  @$_POST['feedback_type'];
		
		$insert_val=array();
		if($feedback_type!=''){
			$db_query->select('*');
			$db_query->from('feedback_groups');
			$db_query->where('feedback_type',$_POST['feedback_type']);
			
			$insert_val['feedback_type']=$_POST['feedback_type'];
			
			if($feedback_type=='SSS' ||  $feedback_type== 'StudentCurriculum'){
				if(@$_POST['program']!=''){
					$db_query->where('program',@$_POST['program']);
					$insert_val['program']=$_POST['program'];
				}
			}
			elseif($feedback_type=='PO'){
				
				if(@$_POST['stakeholder']!=''){
					$db_query->where('stackholder',@$_POST['stakeholder']);
					$insert_val['stackholder']=$_POST['stakeholder'];
				}
				
				if(@$_POST['specialization']!=''){
					$db_query->where('specialization',@$_POST['specialization']);
					$insert_val['specialization']=$_POST['specialization'];
				}
			}
			
			$group= $db_query->limit(1)->get()->row_array();
			
				if(@$group['id']==''){
					$insert_val['question_group']=implode("", array_values($insert_val));
					$db_query->insert('feedback_groups',$insert_val);
					$group['id']= $db_query->insert_id();
				}
		}
		
		
		
		//echo $db_query->last_query();
		$year = explode("-", @$_POST['feedback_year']);
		$year_from= @$year[0];
		$year_to= @$year[1];
		
		
		$this->form_validation->set_rules('feedback_year', 'Year', 'trim|required');
		$this->form_validation->set_rules('feedback_type', 'Feedback Type', 'trim|required');
		$this->form_validation->set_rules('type', 'Type', 'trim|required');
		
		if(@$this->input->post('type')=='radio'){
			$this->form_validation->set_rules('option_value', 'Option value', 'trim|required');
		}
		
        if ($this->form_validation->run() == true)
        {	
			$data = array(
                'feedback_group_id' => $group['id'],
				'college_id' => 102,
                'year_from' => $year_from,
                'year_to' => $year_to,
				'type' => $this->input->post('type'),
                'question' => $this->input->post('question'),
            );
			if($this->input->post('type')=='text'){
				$data['placeholder']= @$this->input->post('placeholder');
			}
			else{
				$data['placeholder']= '';
			}
			
			
            $db_query->insert('feedbacks',$data);
			$inser_id= $db_query->insert_id();
			
			if($this->input->post('type')=='radio'){
				
				$option_value= explode(",", $this->input->post('option_value'));
				if($option_value){
					foreach($option_value as $key=>$value){
				
						$data = array(
						'feedback_id' =>$inser_id,
						'type' => $this->input->post('type'),
						'options' =>$value,
						'priority' => ($key+1)
						);
						
						$db_query->insert('feedback_options',$data);
					}
				}
			}
			
			$this->session->set_flashdata('msg','<div class="alert alert-success text-center">New Feedback successfully created.</div>');
			redirect('office_admin/feedback');
        }
		
		$this->load->view('office_admin/common/header', $data);
		$this->load->view('office_admin/feedback/add',$data);
		$this->load->view('office_admin/common/footer');
	}

	
	public function edit($id){
		
		$data= array();
		$db_query = $this->load->database('clg_db2', TRUE);
		
		// get feedback group
		
		$group= array();
		$feedback_type=  @$_POST['feedback_type'];
		
		$insert_val=array();
		if($feedback_type!=''){
			$db_query->select('*');
			$db_query->from('feedback_groups');
			$db_query->where('feedback_type',$_POST['feedback_type']);
			
			$insert_val['feedback_type']=$_POST['feedback_type'];
			
			if($feedback_type=='SSS' ||  $feedback_type== 'StudentCurriculum'){
				if(@$_POST['program']!=''){
					$db_query->where('program',@$_POST['program']);
					$insert_val['program']=$_POST['program'];
				}
			}
			elseif($feedback_type=='PO'){
				
				if(@$_POST['stakeholder']!=''){
					$db_query->where('stackholder',@$_POST['stakeholder']);
					$insert_val['stackholder']=$_POST['stakeholder'];
				}
				
				if(@$_POST['specialization']!=''){
					$db_query->where('specialization',@$_POST['specialization']);
					$insert_val['specialization']=$_POST['specialization'];
				}
			}
			
			$group= $db_query->limit(1)->get()->row_array();
			
			
			if(@$group['id']==''){
				$insert_val['question_group']=implode("", array_values($insert_val));
				$db_query->insert('feedback_groups',$insert_val);
				$group['id']= $db_query->insert_id();
			}
			
		}
		
		//echo $db_query->last_query(); die;
		
		
		$year = explode("-", @$_POST['feedback_year']);
		$year_from= @$year[0];
		$year_to= @$year[1];
		
		
		$this->form_validation->set_rules('feedback_year', 'Year', 'trim|required');
		$this->form_validation->set_rules('feedback_type', 'Feedback Type', 'trim|required');
		$this->form_validation->set_rules('type', 'Type', 'trim|required');
		
		if(@$this->input->post('type')=='radio'){
			$this->form_validation->set_rules('option_value', 'Option value', 'trim|required');
		}
		
        if ($this->form_validation->run() == true)
        {	
			$data = array(
                'feedback_group_id' => $group['id'],
                'year_from' => $year_from,
                'year_to' => $year_to,
				'type' => $this->input->post('type'),
                'question' => $this->input->post('question'),
            );
			
			if($this->input->post('type')=='text'){
				$data['placeholder']= @$this->input->post('placeholder');
			}
			else{
				$data['placeholder']= '';
			}
			
			$db_query->where('id',$id);
            $db_query->update('feedbacks',$data);
			
			
			$db_query->where('feedback_id',$id);
			$db_query->delete('feedback_options');
			if($this->input->post('type')=='radio'){
				
				$option_value= explode(",", $this->input->post('option_value'));
				if($option_value){
					foreach($option_value as $key=>$value){
						
						$data = array(
						'feedback_id' =>$id,
						'type' => $this->input->post('type'),
						'options' =>$value,
						'priority' => ($key+1)
						);
						
						$db_query->insert('feedback_options',$data);
					}
				}
			}
			
			$this->session->set_flashdata('msg','<div class="alert alert-success text-center">Feedback updated successfully created.</div>');
			redirect('office_admin/feedback');
        }
		
		$data['feedback_db'] = $db_query->from('feedbacks')->where('id',$id)->get()->row_array();
		$data['feedback_grp'] = $db_query->from('feedback_groups')->where('id',$data['feedback_db']['feedback_group_id'])->get()->row_array();
		
		$data['feedback_options'] = $db_query->from('feedback_options')->select('group_concat(options) as options')
		->where('feedback_id',$data['feedback_db']['id'])->order_by('priority','asc')->get()->row_array();
		
		//echo '<pre>'; print_r($data); echo '</pre>';
		
		$this->load->view('office_admin/common/header', $data);
		$this->load->view('office_admin/feedback/edit',$data);
		$this->load->view('office_admin/common/footer');
	}
}
