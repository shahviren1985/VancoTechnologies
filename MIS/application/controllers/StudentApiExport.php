<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class StudentApiExport extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		$this->load->helper('select_db');
		$this->load->model('SearchApiStudentsModel');
		$this->load->helper('json_output');
	}
	
	public function students_export(){	
		$course = strtolower($_GET['course']);
		$type = strtolower($_GET['type']);
		$sem = $_GET['sem'];	
		$year = $_GET['year'];
		if(empty($course)){
			$error = 'Invalid Request';
		}
		if(empty($type)){
			$error = 'Invalid Request';
		}		
		if(empty($year)){
			$error = 'Invalid Request';
		}
		
		if(empty($error)){
			$fileName = $course.'-'.$type.'_sem'.$sem.'_'.$year.'_Regular.csv';
			$data = $this->SearchApiStudentsModel->get_students_result($course,$year,$type,$sem);			
			if(!empty($data)){
				header("Content-type: application/csv");
				header("Content-Disposition: attachment; filename=\"$fileName\"");
				header("Pragma: no-cache");
				header("Expires: 0");
				$handle = fopen('php://output', 'w');
				$header = array();
				$row_count = 1;
				foreach($data as $header_v){
					if($row_count == 1){
						foreach($header_v as $key=>$value){					
							$header[] = $key;
						}
					}
					$row_count++;
				}
				fputcsv($handle, $header);
				
				foreach ($data as $data) {
					fputcsv($handle, $data);
				}
				fclose($handle);
				exit;
			} else {			
				$error = 'No result found.';		
			}
		} 
		$response['status'] = 400;
		json_output($response['status'],$error);		
	}	
}?>