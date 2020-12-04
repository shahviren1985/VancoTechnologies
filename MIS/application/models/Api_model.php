<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Api_model extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
	}
	
	public function get_specialization_result($year=''){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if($connectionString == 'officeadmin1'){
			 $db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}	
        
	/* 	$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database('clg_db2', TRUE); */
		$db_query->distinct('specialization');
		$db_query->select('specialization,year,short_form');
		$db_query->order_by('specialization');
		$query = $db_query->get('course_details');		
		$spec = $query->result();		
		$spec_array = array();
		foreach($spec as $new_spec){			
			$spec_array[$new_spec->specialization][] = $new_spec->year;
		}
				
		$api_array = array();
		$count = 0;
		$total_fy = $total_sy = $total_ty = $total_ts = 0;
		foreach($spec_array as $key=>$course_name){
			$api_array[$count]['title'] = $key;
			//$db_query1 = $this->load->database('clg_db2', TRUE);
			if($connectionString == 'officeadmin1'){
				$db_query1 = $this->load->database('clg_db1', TRUE);
			}else{
				$db_query1 = $this->load->database('clg_db2', TRUE);
			}
			$ts = $fy = $sy = $ty = 0;
			foreach($course_name as $value){
				$db_query1->select('count(*) as count');
				$db_query1->where('specialization',$key);
				$db_query1->where('course_name',$value);
				$db_query1->where('dropped','No');
				$db_query1->where('left_college','No');
				if($year){
					$db_query1->where('academic_year',$year);
				}
				$query1 = $db_query1->get('student_details');
				//echo $db_query1->last_query().'<br>';
				$result = $query1->result();
				if($value == 'FY'){
					$fy += $result[0]->count;
				} elseif($value == 'SY'){
					$sy += $result[0]->count;
				} elseif($value == 'TY'){
					$ty += $result[0]->count;
				}
				$ts = $fy+$sy+$ty;
			}
			$api_array[$count]['fy'] = $fy;
			$api_array[$count]['sy'] = $sy;
			$api_array[$count]['ty'] = $ty;
			$api_array[$count]['ts'] = $ts;
			
			$total_fy += $fy;
			$total_sy += $sy;
			$total_ty += $ty;
			$total_ts += $ts;		
			$count++;
		}		
		$api_array[$count]['title'] = 'Total';
		$api_array[$count]['total_fy'] = $total_fy;
		$api_array[$count]['total_sy'] = $total_sy;
		$api_array[$count]['total_ty'] = $total_ty;
		$api_array[$count]['total_ts'] = $total_ts;		
		return $api_array;
	}	
	
	public function get_caste_result($year='',$category=''){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		if($connectionString == 'officeadmin1'){
		 $db_query = $this->load->database('clg_db1', TRUE);
		}elseif($connectionString == 'officeadmin2'){
			 $db_query = $this->load->database('clg_db2', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}
	/* 	$database = '';
        $connectionString = $this->session->userdata("connectionString");   
		$db_query = $this->load->database('clg_db2', TRUE); */
		$arrCat = array('EWS','NT1','NT2','NT3','OBC','SBC','SC','SEBC','ST','VJ');
		$arrCoursname = array('FY','SY','TY');
		$combine_array = array();
		foreach($arrCat as $key=> $value){
			foreach($arrCoursname as $coursename){
				$combine_array[$value][] = $coursename;
			}			
		}
		$api_array = array();
		$count = 0;
		$total_fy = $total_sy = $total_ty = $total_ts = 0;
		//print_r($combine_array);
		foreach($combine_array as $key1=>$course_name){
			$api_array[$count]['title'] = $key1;	
			$ts = $fy = $sy = $ty = 0;
			foreach($course_name as $course){
				$db_query->select('count(*) as count');
				if($key1 == 'NRI'){
					$db_query->where('nri','Yes');
				} elseif($key1 == 'Divyangjan'){
					$db_query->where('physical_disability','Yes');
				} else {
					$db_query->where('(caste like "'.$key1.'%" OR caste = "'.strtolower($key1).'%" OR caste = "'.strtoupper($key1).'%")'); 
				}				
				$db_query->where('course_name',$course);
				$db_query->where('left_college','No');
				$db_query->where('dropped','No');
				if($year){
					$db_query->where('academic_year',$year);
				}
				$query = $db_query->get('student_details');				
				$result = $query->result();
				if($course == 'FY'){
					$fy += $result[0]->count;
				} elseif($course == 'SY'){
					$sy += $result[0]->count;
				} elseif($course == 'TY'){
					$ty += $result[0]->count;
				}
				$ts = $fy+$sy+$ty;
			}
			$api_array[$count]['fy'] = $fy;
			$api_array[$count]['sy'] = $sy;
			$api_array[$count]['ty'] = $ty;
			$api_array[$count]['ts'] = $ts;
			$total_fy += $fy;
			$total_sy += $sy;
			$total_ty += $ty;
			$total_ts += $ts;
			$count++;
		}
		$api_array[$count]['title'] = 'Total';
		$api_array[$count]['total_fy'] = $total_fy;
		$api_array[$count]['total_sy'] = $total_sy;
		$api_array[$count]['total_ty'] = $total_ty;
		$api_array[$count]['total_ts'] = $total_ts;
		
		
		//PD, Open, NRI
		//$db_query1 = $this->load->database('clg_db2', TRUE);
		if($connectionString == 'officeadmin1'){
		 $db_query1 = $this->load->database('clg_db1', TRUE);
		}else{
			 $db_query1 = $this->load->database('clg_db2', TRUE);
		}
		$addCaste = array('Open','NRI','Divyangjan');
		$combine_array1 = array();
		foreach($addCaste as $value){
			foreach($arrCoursname as $coursename){
				$combine_array1[$value][] = $coursename;
			}
		}
		
		$api_array1 = array();
		$count1 = 0;
		foreach($combine_array1 as $key2=>$course_name1){
			$api_array1[$count1]['title'] = $key2;			
			$ts1 = $fy1 = $sy1 = $ty1 = 0;
			foreach($course_name1 as $course1){
				$db_query1->select('count(*) as count1');
				if($key2 === 'Open'){
					$db_query1->where('(caste = "Open" or caste_category = "Open")'); 
				} elseif($key2 === 'NRI'){
					$db_query1->where('nri','Yes');
				} elseif($key2 === 'Divyangjan'){
					$db_query1->where('physical_disability','Yes');
				}				
				$db_query1->where('course_name',$course1);
				$db_query1->where('dropped','No');
				$db_query1->where('left_college','No');
				if($year){
					$db_query1->where('academic_year',$year);
				}
				/* if($year){
					$db_query1->where('academic_year',$year);
				}else {
					$db_query1->where('left_college','No');
				} */	
				$query1 = $db_query1->get('student_details'); 				
				$result1 = $query1->result();
			
				if($course1 == 'FY'){
					$fy1 += $result1[0]->count1;
				} elseif($course1 == 'SY'){
					$sy1 += $result1[0]->count1 ;
				} elseif($course1 == 'TY'){
					$ty1 += $result1[0]->count1;
				}
				$ts1 = $fy1+$sy1+$ty1;
			}
			$api_array1[$count1]['fy'] = $fy1;
			$api_array1[$count1]['sy'] = $sy1;
			$api_array1[$count1]['ty'] = $ty1;
			$api_array1[$count1]['ts'] = $ts1;
			$count1++;
		}		
		$api_caste = array_merge($api_array,$api_array1);
		return $api_caste;
	}	
	
	public function get_students_result($course,$year,$type,$sem){
		if($course == 'bsc'){
			$db_query = $this->load->database('clg_db2', TRUE);
		} else {			
			$db_query = $this->load->database('clg_db1', TRUE);
		}
		$db_query->select('student_details.*,course_details.course_type,course_details.year as c_year,course_details.short_form as c_spec')
			 ->from('student_details')
			 ->join('course_details', 'student_details.course_id = course_details.id');
		if($type == 'honors'){
			$db_query->where('course_details.course_name = "Honors"');
		} else {
			$db_query->where('course_details.course_name != "Honors"');
		}
		$db_query->where('academic_year = "'.$year.'"');
		$db_query->where('dropped = "No"');
		$db_query->where('left_college = "No"');
		$db_query->order_by('roll_number', 'ASC');
		$query = $db_query->get();
		$student_list = $query->result();
		
		return $student_list;
		die;
		$data = array();
		$count = 0;
		foreach($student_list as $list){
			$count++;
			$data[] = array(
				'SerialNumber'=> $count,
				'SeatNumber'=> '',
				'RollNumber'=> $list->roll_number,
				'College_Registration_No_'=> $list->userID,
				'LastName'=> $list->last_name,
				'FirstName'=> $list->first_name,
				'FatherName'=> $list->middle_name,
				'MotherName'=> $list->mother_first_name,
				'Course'=> $list->c_year .' '.$list->course_type,
				'Specialisation'=> $list->c_spec,
				'PRN'=> $list->PRN_number."'",
				'Address'=> '',
				'PhoneNumber'=> $list->mobile_number,
				'PhotoPath'=> $list->photo_path,				
				'ExamType'=> 'Regular',
				'Year'=> $list->academic_year,
				'Semester'=> '',
				'Paper1Appeared'=> '',				
				'Code1'=> '',
				'InternalC1'=> '',
				'ExternalSection1C1'=> '',
				'ExternalSection2C1'=> '',
				'ExternalTotalC1'=> '',
				'GraceC1'=> '',
				'PracticalMarksC1'=> '',
				'Attempt1'=> '',
				'Remarks1'=> '',
				'Paper2Appeared'=> '',				
				'Code2'=> '',
				'InternalC2'=> '',
				'ExternalSection1C2'=> '',
				'ExternalSection2C2'=> '',
				'ExternalTotalC2'=> '',
				'GraceC2'=> '',
				'PracticalMarksC2'=> '',
				'Attempt2'=> '',
				'Remarks2'=> '',
				'Paper3Appeared'=> '',
				'Code3'=> '',
				'InternalC3'=> '',
				'ExternalSection1C3'=> '',
				'ExternalSection2C3'=> '',
				'ExternalTotalC3'=> '',
				'GraceC3'=> '',
				'PracticalMarksC3'=> '',
				'Attempt3'=> '',
				'Remarks3'=> '',
				'Paper4Appeared'=> '',				
				'Code4'=> '',
				'InternalC4'=> '',
				'ExternalSection1C4'=> '',
				'ExternalSection2C4'=> '',
				'ExternalTotalC4'=> '',
				'GraceC4'=> '',
				'PracticalMarksC4'=> '',
				'Attempt4'=> '',
				'Remarks4'=> '',
				'Paper5Appeared'=> '',
				'Code5'=> '',
				'InternalC5'=> '',
				'ExternalSection1C5'=> '',				
				'ExternalSection2C5'=> '',
				'ExternalTotalC5'=> '',
				'GraceC5'=> '',
				'PracticalMarksC5'=> '',
				'Attempt5'=> '',
				'Remarks5'=> '',
				'Paper6Appeared'=> '',
				'Code6'=> '',
				'InternalC6'=> '',
				'ExternalSection1C6'=> '',
				'ExternalSection2C6'=> '',				
				'ExternalTotalC6'=> '',
				'GraceC6'=> '',
				'PracticalMarksC6'=> '',
				'Attempt6'=> '',
				'Remarks6'=> '',
				'Paper7Appeared'=> '',
				'Code7'=> '',
				'InternalC7'=> '',
				'ExternalSection1C7'=> '',
				'ExternalSection2C7'=> '',
				'ExternalTotalC7'=> '',
				'GraceC7'=> '',
				'PracticalMarksC7'=> '',
				'Attempt7'=> '',
				'Remarks7'=> '',
				'Paper8Appeared'=> '',
				'Code8'=> '',
				'InternalC8'=> '',
				'ExternalSection1C8'=> '',
				'ExternalSection2C8'=> '',
				'ExternalTotalC8'=> '',
				'GraceC8'=> '',
				'PracticalMarksC8'=> '',
				'Attempt8'=> '',				
				'Remarks8'=> '',
				'Paper9Appeared'=> '',
				'Code9'=> '',
				'InternalC9'=> '',
				'ExternalSection1C9'=> '',
				'ExternalSection2C9'=> '',
				'ExternalTotalC9'=> '',
				'GraceC9'=> '',
				'PracticalMarksC9'=> '',
				'Attempt9'=> '',
				'Remarks9'=> '',
				'Paper10Appeared'=> '',
				'Code10'=> '',
				'InternalC10'=> '',
				'ExternalSection1C10'=> '',
				'ExternalSection2C10'=> '',				
				'ExternalTotalC10'=> '',
				'GraceC10'=> '',
				'PracticalMarksC10'=> '',
				'Attempt10'=> '',
				'Remarks10'=> '',
				'Paper11Appeared'=> '',
				'Code11'=> '',
				'InternalC11'=> '',
				'ExternalSection1C11'=> '',
				'ExternalSection2C11'=> '',
				'ExternalTotalC11'=> '',
				'GraceC11'=> '',
				'PracticalMarksC11'=> '',
				'Attempt11'=> '',
				'Remarks11'=> '',
				'Paper12Appeared'=> '',				
				'Code12'=> '',
				'InternalC12'=> '',
				'ExternalSection1C12'=> '',
				'ExternalSection2C12'=> '',
				'ExternalTotalC12'=> '',
				'GraceC12'=> '',
				'PracticalMarksC12'=> '',
				'Attempt12'=> '',
				'Remarks12'=> '',						
			);
		}		
		return $data;
	}
	public function update_into_user_details($userId,$data) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        if($connectionString == 'officeadmin1'){
			 $db_query = $this->load->database('clg_db1', TRUE);
		}else{
			$db_query = $this->load->database('clg_db2', TRUE);
		}
		//$query =  $db_query->update_batch('student_details', $data,'userID');
		
		$db_query->where('userID', $userId);
		//$query = $db_query->get('student_details');		
		//$spec = $query->result();
		$result = $db_query->update('student_details', $data);
		return $result;
 	}
}