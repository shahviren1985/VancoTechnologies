<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Pagesstaff extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '4') {
			redirect(site_url(), 'refresh');
        }
		$this->load->library('form_validation');
		
	}

	public function documents() {
		$this->load->model('staff/staff');
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry( $userID );
		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/documents');
		$this->load->view('staff/common/footer');
	}
	
	public function view_attendance() {
		$this->load->model('staff/staff');
		$userID = $this->session->userdata("userID");
		$data['staff'] = $this->staff->get_entry( $userID );
		$data['staff_details'] = $this->staff->get_entry( $userID );
		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/view-attendance',$data);
		$this->load->view('staff/common/footer');
	}
	
	public function approve_elective() {
/* 		$this->load->model('staff/staff');
		$userID = $this->session->userdata("userID");
		$data['staff'] = $this->staff->get_entry( $userID ); */
		$this->load->model('staff/staff');
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry( $userID );
		$db_query = $this->load->database('clg_db2', TRUE);
		if(@$_REQUEST['program']!=''  &&   @$_REQUEST['semester']!=''){
			$api= file_get_contents("https://vancotech.com/Exams/bachelors/API/api/User/GetGEList?Course=bsc&specialization=".$_REQUEST['program']."&sem=".$_REQUEST['semester']."");
			$data['json'] = json_decode($api, true );
		}
		
		if(@$_REQUEST['search']=='Search' || @$_REQUEST['search']=='Export'){
				
				$data['student']=  $db_query->from('electives')
				->where('electives.semester',$_REQUEST['semester'])
				->where('electives.academicyear',$_REQUEST['year'])
				->where('electives.stream',$_REQUEST['program'])
				->where('electives.specialisation',$_REQUEST['specialization'])
				->where('electives.rollnumber !=',0)
				->order_by("electives.rollnumber", "asc")
				->get()->result_array();
				
				//echo  $db_query->last_query(); die;
		}
		
		if(@$_REQUEST['search']=='Save'){
			
				//echo '<pre>'; print_r($_REQUEST); die;
			
			
				foreach($_REQUEST['id']  as  $new_id){
				  
					 $arr_av= array('approvedelective1'=>Null,'approvedelective2'=>Null,'approvedelective3'=>Null,'approvedelective4'=>Null,'approvedelective5'=>Null,'approvedelective6'=>Null,'approvedelective7'=>Null);
					 $db_query->where('id',$new_id)->update('electives', $arr_av); 
				}
			
			
			  foreach($_REQUEST['elective']  as $id=> $value){  
					 $db_query->where('id',$id)->update('electives', $value); 
				  //echo  $db_query->last_query(); die;
			  }
			  
			 // echo  $db_query->last_query(); die;
			  
			$this->session->set_flashdata('msg','<div class="alert alert-success text-center">Elective successfully saved.</div>');
			redirect('staff/approve_elective?semester='.$_REQUEST['semester'].'&year='.$_REQUEST['year'].'&program='.$_REQUEST['program'].'&specialization='.$_REQUEST['specialization'].'&search=Search');
		}
		elseif(@$_REQUEST['search']=='Export'){
		
				
					$this->load->library('excel');
					$objPHPExcel = new PHPExcel();
					$objPHPExcel->setActiveSheetIndex(0);

					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(0, 1, 'Serial #');
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(1, 1, 'Roll #');
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow(2, 1, 'Student Name');
					$total_code=array();
					foreach($data['json'] as $jso){
					if($jso['specialisationCode']==$_REQUEST['specialization']){   
						$stat=3;
						foreach($jso['ElectiveSubject'] as $key=>$display){
							
									 foreach($display['Subject'] as $elect=>$display_res){  
									$total_code[$display_res['Code']]=$display_res['Code'];
								$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($stat, 1, $display_res['Title']  );
							$stat++;
					 } } }}
					 
					 
					 								
									$total=array();
									
								 $start=2;
								foreach($data['student'] as $key=>$stu){


									$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow( 0, $start , $key+1);  
									$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow( 1, $start , $stu['rollnumber']); 
									$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow( 2, $start , $stu['studentname']); 
								
								
								foreach($data['json'] as $jso){
								if($jso['specialisationCode']==$_REQUEST['specialization']){   
								
											 foreach($jso['ElectiveSubject'] as $key=>$display){ 
										
												 $electstart=3;
												 foreach($display['Subject'] as $elect=>$display_res){ 

													$total_code= count($display['Subject']);	
											
											$ae1= $stu['approvedelective1'];
											$ae2= $stu['approvedelective2'];
											$ae3= $stu['approvedelective3'];
											$ae4= $stu['approvedelective4'];
											$ae5= $stu['approvedelective5'];
											$ae6= $stu['approvedelective6'];
											$ae7= $stu['approvedelective7'];
											
											//if ($ae1 || $ae2 || $ae3 || $ae4 || $ae5 || $ae6 || $ae7) {
												 $electivePresent1 = $stu["approvedelective1"] ==$display_res['Code'] || $stu["approvedelective2"] == $display_res['Code'] || $stu["approvedelective3"] == $display_res['Code'] || $stu["approvedelective4"] == $display_res['Code'] || $stu["approvedelective5"] == $display_res['Code'] || $stu["approvedelective6"] == $display_res['Code'] || $stu["approvedelective7"] ==$display_res['Code'];
											//}
											//else{
												$electivePresent2 = $stu["elective1"] == $display_res['Code'] || $stu["elective2"] == $display_res['Code'] || $stu["elective3"] == $display_res['Code'] || $stu["elective4"] == $display_res['Code'] || $stu["elective5"] == $display_res['Code'] || $stu["elective6"] == $display_res['Code'] || $stu["elective7"] == $display_res['Code'];
											//}
											$checked='';
											if($electivePresent1  || $electivePresent2 ){
												
												$total[$display_res['Code']]  =  @$total[$display_res['Code']]+1;
												$checked='checked';
											}
											
											if($checked=='checked'){
												
												$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow( $electstart, $start , 'Yes'); 
											}
											else{
												$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow( $electstart, $start , ''); 
											}
											$electstart++;
								 } 
								 
								 } }}
									
								
									$start++;
								
								 }  
					 
					 
					 
					
					$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);					
					header('Content-Type: application/vnd.ms-excel');
					header("Content-Disposition: attachment;Filename=elective.xls");
					header('Cache-Control: max-age=0');
					$objWriter = PHPExcel_IOFactory::createWriter($objPHPExcel, 'Excel5');
					$objWriter->save('php://output');
				
		}
		
		
			

		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/approve-elective');
		$this->load->view('staff/common/footer');
	}
	
	public function leave_application() {
		$this->load->model('staff/staff');
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry( $userID );
		$employee_code = $data['staff_details'][0]->employee_code;
		$data['leave_request'] = $this->staff->get_leave_request($employee_code);
		$data['request'] = $this->staff->get_request_by_id($userID);
		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/leave_application', $data);
		$this->load->view('staff/common/footer');
	}	
	
	public function leave_request_list() {
		$this->load->model('staff/staff');
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry($userID );
		$role = $data['staff_details'][0]->role;
		$dept = $data['staff_details'][0]->department;
		if($role == 'HOD'){
			$data['staff'] = $this->staff->get_staff_by_department($dept);
		}else{
			$data['leave_request'] = $this->staff->get_all_leave_details();
		}
		$employee_code = $data['staff_details'][0]->employee_code;
		$data['request'] = $this->staff->get_request_by_id($userID);
		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/leave_request_list', $data);
		$this->load->view('staff/common/footer');
	}
	
/* 	public function feedback() {
		$this->load->view('student/common/header');
		$this->load->view('student/feedback');
		$this->load->view('student/common/footer');
	}
	
	public function performance() {
		$this->load->model('student/student');
		$userID = $this->session->userdata("userID");
		$data['student'] = $this->student->get_entry( $userID );
		$this->load->view('student/common/header');
		$this->load->view('student/performance',$data);
		$this->load->view('student/common/footer');
	}
	public function applications() {
		$this->load->view('student/common/header');
		$this->load->view('student/applications');
		$this->load->view('student/common/footer');
	} */
}
