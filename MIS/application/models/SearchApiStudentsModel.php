<?php 
defined('BASEPATH') OR exit('No direct script access allowed');

class SearchApiStudentsModel extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
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
		$data = array();
		$count = 0;
		foreach($student_list as $list){
			$ld = $list->physical_disability;
			if($ld == 'Yes'){
				$ld1 = 'TRUE';
			}else{
				$ld1 = 'FALSE';
			}
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
				'Semester'=> $sem,
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
				'Status' => '',
				'LD' => $ld1	
			);
		}
		return $data;
	}

}