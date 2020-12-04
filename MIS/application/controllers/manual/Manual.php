<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Manual extends CI_Controller {

	public function __construct(){
		parent::__construct();	
		require_once APPPATH.'third_party/qrcode/qrlib.php';
	}
	
	public function index() {
		if($this->input->get('course_name')==''){
			echo 'course name is required.'; die;
		}
		$course_name= $this->input->get('course_name');
		$db_query = $this->load->database('clg_db2', TRUE);
        $result = $db_query->where("PRN_number!=''")
				->where("userID !=''")
				->where('roll_number!=0')
				->where("emergency_number !=''")
				->where('course_name',$course_name)
				->get('student_details')->result_array();
		//echo '<pre>';print_r($db_query->last_query()); echo '<br/>'; echo count($result); die;
		
		if($result){
			foreach($result as $res ){
				
				// qr code generator
				$qr_array=array();
				$qr_array['prn']=$res['PRN_number'];
				$qr_array['emergencyNumber']=$res['emergency_number'];
				$qr_array['crn']=$res['userID'];

				$codeContents['prn']= $res['PRN_number'];
                $codeContents['crn']  = $res['userID'];
                $codeContents['emergency']  = $res['emergency_number'];

				$pngAbsoluteFilePath = FCPATH . '/uploads/ID-Card/'.$res['userID'].'.png';
                $smallAbsoluteFilePath = FCPATH . '/uploads/Library-Card/'.$res['userID'].'.png';

				if (!file_exists($pngAbsoluteFilePath)) {
					$myfile = fopen($pngAbsoluteFilePath, "w") or die("Unable to open file!");
					fclose($myfile);
				}

                if (!file_exists($smallAbsoluteFilePath)) {
                    $myfile = fopen($smallAbsoluteFilePath, "w") or die("Unable to open file!");
                    fclose($myfile);
                }

                $small  = $res['userID'];
				QRcode::png(json_encode($codeContents), $pngAbsoluteFilePath , QR_ECLEVEL_L ,2.9  );
                QRcode::png($small, $smallAbsoluteFilePath , QR_ECLEVEL_L ,4  );
				// qr code generator
				
				//bar code
				//$generator = new Picqer\Barcode\BarcodeGeneratorPNG();
				//file_put_contents(FCPATH . '/uploads/student_bar_code/'.$res['id'].'.png', $generator->getBarcode($res['library_card_no'], $generator::TYPE_CODE_128, 3, 50));
				
			}
		}
		
		echo 'Process completed successfully.';
		
	}
	
	public function renamefile() {
		
		$dir = FCPATH . '/uploads/student/old';
			if (is_dir($dir)){
			if ($dh = opendir($dir)){
				while (($file = readdir($dh)) !== false){
					
					if($file=='.' || $file=='..'){
						continue;
					}
					
					$file_content= file_get_contents($dir.'/'.$file);
					$json = json_decode($file_content,true );
					
					$user_id= ltrim( $json['userId'],102);;
					
					$db_query = $this->load->database('clg_db2', TRUE);
					$result = $db_query->where('userId',$user_id)->get('student_details')->row_array();;
					$json['a2']	= array($result['first_name'].' '.$result['last_name']  );
					
					
					
				   $filefirst=explode('SSS',$file );
				   $filefirst[0]='StudentCurriculum';
				   $file_name=implode('',$filefirst);
				  // echo '<pre/>';print_r($json); die;
				   
				   file_put_contents(FCPATH . '/uploads/student/new/'.$file_name,json_encode($json,JSON_PRETTY_PRINT));
				 
				}
			closedir($dh);
			}
		}
		
		echo 'Process completed successfully.';
		
	}
	
	
	public function createQuestion() {
		die;
		$dir = FCPATH . '/uploads/feedback/FeedbackQuestions';
		$arrmatch=array();
			if (is_dir($dir)){
			if ($dh = opendir($dir)){
				while (($file = readdir($dh)) !== false){
					if($file=='.' || $file=='..' ){ continue; }
						array_push($arrmatch, $file);
				   }
				}
			closedir($dh);
			}
			
		
		
		$db_query = $this->load->database('clg_db2', TRUE);
		$db_query->truncate('feedback_options');
		$db_query->truncate('feedbacks');

		foreach($arrmatch as $key=>$file_name){
			
			$remove= str_replace("_Questions.json","",$file_name);
			$remove= str_replace("102_","",$remove);
			
			$feedback_type= ''; //SSS  StudentCurriculum PO  TA
			if (strpos($remove, 'SSS') !== false) {
				$feedback_type= 'SSS';
			}
			elseif (strpos($remove, 'StudentCurriculum') !== false) {
				$feedback_type= 'StudentCurriculum';
			}
			elseif (strpos($remove, 'PO') !== false) {
				$feedback_type= 'PO';
			}
			elseif (strpos($remove, 'TA') !== false) {
				$feedback_type= 'TA';
			}
			
			$program= '';  //FY SY  TY MSY MFY
			if (strpos($remove, 'MFY') !== false) {
				$program= 'MFY';
			}
			elseif (strpos($remove, 'FY') !== false) {
				$program= 'FY';
			}
			elseif (strpos($remove, 'MSY') !== false) {
				$program= 'MSY';
			}
			elseif (strpos($remove, 'SY') !== false) {
				$program= 'SY';
			}
			elseif (strpos($remove, 'TY') !== false) {
				$program= 'TY';
			}
			
			
			$stackholder= ''; //Student Parents  Teacher Alumni Employer
			if (strpos($remove, 'Student') !== false) {
				$stackholder= 'Student';
			}
			elseif (strpos($remove, 'Parents') !== false) {
				$stackholder= 'Parents';
			}
			elseif (strpos($remove, 'Teachers') !== false) {
				$stackholder= 'Teachers';
			}
			elseif (strpos($remove, 'Teacher') !== false) {
				$stackholder= 'Teacher';
			}
			elseif (strpos($remove, 'Alumni') !== false) {
				$stackholder= 'Alumni';
			}
			elseif (strpos($remove, 'Employer') !== false) {
				$stackholder= 'Employer';
			}
			
			elseif (strpos($remove, 'PG') !== false) {
				$stackholder= 'PG';
			}
			
			$specialization= '';  //DC ECCE  FND HTM  DRM  MCE TAD 
			if (strpos($remove, 'DC') !== false) {
				$specialization= 'DC';
			}
			elseif (strpos($remove, 'ECCE') !== false) {
				$specialization= 'ECCE';
			}
			elseif (strpos($remove, 'FND') !== false) {
				$specialization= 'FND';
			}
			elseif (strpos($remove, 'DRM') !== false) {
				$specialization= 'DRM';
			}elseif (strpos($remove, 'MCE') !== false) {
				$specialization= 'MCE';
			}elseif (strpos($remove, 'TAD') !== false) {
				$specialization= 'TAD';
			}elseif (strpos($remove, 'HTM') !== false) {
				$specialization= 'HTM';
			}
			
			echo  $key.' '.$file_name.'  ---- '.$feedback_type.' '. $program.' '.  $stackholder . ' '. $specialization. '<br/>' ;
			
			$question_data = json_decode(file_get_contents($dir.'/'. $file_name) , true);
			
			foreach($question_data as $question){
				$data=array();
				$data['college_id']=102;
				$data['type']= @$question['type'];
				$data['question_number']=str_replace("a","",$question['id']);
				$data['feedback_type']=@$feedback_type;
				$data['question_group']=$remove;
				$data['program']=$program;
				$data['stackholder']=$stackholder;
				$data['specialization']= $specialization;
				$data['question']=@$question['question'];
				$data['placeholder']=@$question['placeHolder'];
				$data['control_count']=@$question['controlCount'];
				$data['created_at']=date('Y-m-d H:i:s');
				$data['updated_at']=date('Y-m-d H:i:s');
				//print_r($data);  die;
				$db_query->insert('feedbacks', $data);
				$insert_id= $db_query->insert_id();
				if($question['type']=='dropdown' || $question['type']=='radio' ){
					foreach($question['optionValues'] as $key=>$value){
						$option=array();
						$option['feedback_id']=$insert_id;
						$option['type']= $question['type'];
						$option['options']=$value;
						$option['priority']=$key+1;
						$db_query->insert('feedback_options', $option);
					}
				}
			}
		}
		
		echo 'Question data inserted successfully.';
	}
	
	
		public function createAnswer() {
		die;
		$dir = FCPATH . '/uploads/feedback/102';
		$arrmatch=array();
			if (is_dir($dir)){
			if ($dh = opendir($dir)){
				while (($file = readdir($dh)) !== false){
					if($file=='.' || $file=='..' ){ continue; }
						array_push($arrmatch, $file);
				   }
				}
			closedir($dh);
			}
			
		//echo '<pre>'; print_r($arrmatch); die;
		
		$db_query = $this->load->database('clg_db2', TRUE);
		//$db_query->truncate('feedback_answers');
		
		foreach($arrmatch as $key=>$file_name){
			
			$change= str_replace(".json","",$file_name);
			$exp= explode("_", $change);
			$datetime= $exp[1];
			$datetime=   preg_replace('/:/', ' ', str_replace(" ",":",$datetime  ), 1);
			$answer_data = json_decode(file_get_contents($dir.'/'. $file_name) , true);
			
			//print_r($answer_data);  die;
			
			$remove= $answer_data['userType'];
			
			$feedback_type= ''; //SSS  StudentCurriculum PO  TA
			if (strpos($remove, 'SSS') !== false) {
				$feedback_type= 'SSS';
			}
			elseif (strpos($remove, 'StudentCurriculum') !== false) {
				$feedback_type= 'StudentCurriculum';
			}
			elseif (strpos($remove, 'PO') !== false) {
				$feedback_type= 'PO';
			}
			elseif (strpos($remove, 'TA') !== false) {
				$feedback_type= 'TA';
			}
			
			$program= '';  //FY SY  TY MSY MFY
			if (strpos($remove, 'MFY') !== false) {
				$program= 'MFY';
			}
			elseif (strpos($remove, 'FY') !== false) {
				$program= 'FY';
			}
			elseif (strpos($remove, 'MSY') !== false) {
				$program= 'MSY';
			}
			elseif (strpos($remove, 'SY') !== false) {
				$program= 'SY';
			}
			elseif (strpos($remove, 'TY') !== false) {
				$program= 'TY';
			}
			
			
			$stackholder= ''; //Student Parents  Teacher Alumni Employer
			if (strpos($remove, 'Student') !== false) {
				$stackholder= 'Student';
			}
			elseif (strpos($remove, 'Parents') !== false) {
				$stackholder= 'Parents';
			}
			elseif (strpos($remove, 'Teachers') !== false) {
				$stackholder= 'Teachers';
			}
			elseif (strpos($remove, 'Teacher') !== false) {
				$stackholder= 'Teacher';
			}
			elseif (strpos($remove, 'Alumni') !== false) {
				$stackholder= 'Alumni';
			}
			elseif (strpos($remove, 'Employer') !== false) {
				$stackholder= 'Employer';
			}
			
			$specialization= '';  //DC ECCE  FND HTM  DRM  MCE TAD 
			if (strpos($remove, 'DC') !== false) {
				$specialization= 'DC';
			}
			elseif (strpos($remove, 'ECCE') !== false) {
				$specialization= 'ECCE';
			}
			elseif (strpos($remove, 'FND') !== false) {
				$specialization= 'FND';
			}
			elseif (strpos($remove, 'DRM') !== false) {
				$specialization= 'DRM';
			}elseif (strpos($remove, 'MCE') !== false) {
				$specialization= 'MCE';
			}elseif (strpos($remove, 'TAD') !== false) {
				$specialization= 'TAD';
			}elseif (strpos($remove, 'HTM') !== false) {
				$specialization= 'HTM';
			}
			
			if($specialization==''){
				$specialization= @$answer_data['specialisation'];
			}
			
			$month= date('m', strtotime($datetime) );
			$year= date('Y', strtotime($datetime) );
			if($month>5){
				
				$year_from= $year;
				$year_to= $year+1;
			}
			else
			{
				$year_from= $year-1;
				$year_to= $year;
			}
			
			echo  $key.' '.$file_name.'  ---- '.$feedback_type.' '. $program.' '.  $stackholder . ' '. $specialization. ' '.$year_from.'  '.$year_to . '<br/>' ;
			//die;
			 for( $i=0 ; $i<=50 ; $i++ ){
				 
				$varb= "a$i";
				//echo 
				if( isset( $answer_data[$varb]  ) ){
					 
					 if( is_array($answer_data[$varb]  )){
						  $value= $answer_data[$varb][0];
					 }
					 else{
						  $value= $answer_data[$varb]; 
					 }
					 
					$data=array();
					$data['college_id']=102;
					$data['user_id']= str_replace("102","",$answer_data['userId']);
					$data['year_from']=$year_from;
					$data['year_to']=$year_to;
					$data['teacher_code']=$answer_data['teacherCode'];
					$data['subject_code']=$answer_data['subjectCode'];
					$data['feedback_type']=$feedback_type;
					$data['question_group']=@$answer_data['userType'];
					$data['program']=$program;
					$data['stackholder']=$stackholder;
					$data['specialization']= $specialization;
					$data['answer']=$value;
					$data['question_number']=$i;
					$data['created_at']=$datetime;
					$data['ip_address']=@$answer_data['ipAddress'];
					//echo '<pre>';  print_r($data); die;
					$db_query->insert('feedback_answers', $data);	 
				 } 
			 }
		}
		
		echo 'Answer data inserted successfully.';
	}
	
	public function updateRollNumber() {
		
		
		if($_SERVER['REQUEST_METHOD']=='POST'){

				$this->load->library('excel');
				 $db_query = $this->load->database('clg_db2', TRUE);
				 //$file_path=FCPATH . '/uploads/test/TY-Roll-Call-Nov-13.xlsx';
				 //$file_path=FCPATH . '/uploads/test/SY-Roll-Call-Nov-13.xlsx';
				 $file_path = $_FILES["file"]["tmp_name"];
				$object = PHPExcel_IOFactory::load($file_path);	
				echo '<pre>';
				$student=array();
				foreach($object->getWorksheetIterator() as $worksheet) {	
					$highestRow = $worksheet->getHighestRow();	
					$highestColumn = $worksheet->getHighestColumn();
					
						for($row=2; $row<=$highestRow; $row++) {
							$roll_number = $worksheet->getCellByColumnAndRow(0, $row)->getValue();
							$userID = $worksheet->getCellByColumnAndRow(1, $row)->getValue();
							$PRN_number = $worksheet->getCellByColumnAndRow(2, $row)->getValue();
							if($userID>0){
								$student[] =array('userID'=>$userID, 'roll_number'=>$roll_number ,'PRN_number'=>str_replace("'","",$PRN_number));
							}
							
							
						}
				}
				
				$db_query->update_batch('student_details', $student,'userID');

				//echo $db_query->last_query();
				//print_r($student);  die;
				echo 'Details updated successfully.';
		}
		
		?>
		
		 <form method="post"  enctype="multipart/form-data">
		  <input type="file" name="file">
		  <input type="submit" value="upload"/>
		 </form>
		
		
		<?php 
		
		
		
		
	}
	
	
	public function query() {
		
		
		if($_SERVER['REQUEST_METHOD']=='POST'){

				$db_query = $this->load->database('clg_db2', TRUE);
				echo '<pre>';
				$result= $db_query->query($_POST['query'])->result_array();
				print_r($result); 
		}
		
		?>
		
		 <form method="post"  enctype="multipart/form-data">
		 <textarea name="query"  rows="10" cols="70" ><?php echo  @$_POST['query'];  ?></textarea><br/>
		  <input type="submit" value="submit"/>
		 </form>
		<?php 
	}
	
	
}