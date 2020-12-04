<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Staffs extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '4') {
			redirect(site_url(), 'refresh');
        }
		$this->load->model('staff/staff');
		//$this->load->model('student/course_detail');
		$this->load->library('form_validation');
	}

	public function index() {
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry($userID);
		$data['userID'] = $userID;
		if ($data['staff_details'][0]->role=='Principal' || $data['staff_details'][0]->role=='Vice Principal') {
			redirect(site_url('staff/staffs/dashboard'), 'refresh');
		}
		//echo "<pre>"; print_r($data); die;
		//$course_id = $data['staff_details'][0]->course_id;
		//$data['course_details'] = $this->course_detail->get_entry( $course_id );
		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/home', $data);
		$this->load->view('staff/common/footer');
	}


	public function dashboard() {
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry($userID);
		$data['userID'] = $userID;
		$data['leave_request'] = $this->staff->staff_leave_data();
		$data['staff_attendance_data'] = $this->staff->staff_attendance_data();
		/* Code for json Files */
		$arrmatch = $arrFull = $data['answer_data'] = array();
		if(isset($_POST['feedback'])) {  
			if (!empty($_POST['filter_feedback_head'])) {
				//echo "<pre>"; 
				//print_r($_POST); die;
				$strFileContents = file_get_contents("uploads/feedback/2019-20/question/102_AluminiPODC_Questions.json");
				 $strjsonFileContents = file_get_contents("uploads/feedback/2019-20/answer/AlumniPODC_2020-06-16 22 02 07.json");
				//var_dump($strjsonFileContents);
					//var_dump($strFileContents);
				$question_data = json_decode($strFileContents);
				$answer_data= json_decode($strjsonFileContents);
				$data['answer_data'] = $answer_data;
				$data['question_data'] = $question_data;
			} else {
				/*echo "<pre>"; 
				print_r($_POST); die;*/

				/* feedback question code */
				// [feedback_year] => 2019-20
				$year = $_POST['feedback_year'];
				$dir = "uploads/feedback/$year/answer/";
				// Open a directory, and read its contents
				if($_POST['feedback_type']=='PO'){
					$mystring = $_POST['stakeholder'].$_POST['feedback_type'].$_POST['specialization'];
				
					if (is_dir($dir)){
						if ($dh = opendir($dir)){
							while (($file = readdir($dh)) !== false){
							   $filefirst=explode('_',$file );
							   if($filefirst[0]==$mystring){
							   		array_push($arrmatch, $file);
							   }
							}
						closedir($dh);
						}
					}
					if(!empty($arrmatch)){
						foreach ( $arrmatch as $filepath) {
							$fullPathanswer = "uploads/feedback/$year/answer/".$filepath;
							$danswer_data = json_decode(file_get_contents($fullPathanswer));
							array_push($arrFull, $danswer_data);
						}					
						$data['answer_data'] = $arrFull;
					}
				} elseif ($_POST['feedback_type']=='SSS'){
					$mystring = $_POST['feedback_type'].$_POST['Program'];	
					if (is_dir($dir)){
						if ($dh = opendir($dir)){
							while (($file = readdir($dh)) !== false){
							   $filefirst=explode('_',$file );
							   if($filefirst[0]==$mystring){
							   		array_push($arrmatch, $file);
							   }
							}
						closedir($dh);
						}
					}
					if(!empty($arrmatch)){
						foreach ( $arrmatch as $filepath) {
							$fullPathanswer = "uploads/feedback/$year/answer/".$filepath;
							$danswer_data = json_decode(file_get_contents($fullPathanswer));
							array_push($arrFull, $danswer_data);
						}					
						$data['answer_data'] = $arrFull;
					}
				} elseif ($_POST['feedback_type']=='Teachers'){
					$mystring = 'teachers';	
					if (is_dir($dir)){
						if ($dh = opendir($dir)){
							while (($file = readdir($dh)) !== false){
							   $filefirst=explode('_',$file );
							   if($filefirst[0]==$mystring){
							   		array_push($arrmatch, $file);
							   }
							}
						closedir($dh);
						}
					}
					if(!empty($arrmatch)){
						foreach ( $arrmatch as $filepath) {
							$fullPathanswer = "uploads/feedback/$year/answer/".$filepath;
							$danswer_data = json_decode(file_get_contents($fullPathanswer));
							array_push($arrFull, $danswer_data);
						}					
						$data['answer_data'] = $arrFull;
					}
				} 
				else{
					$mystring = $_POST['feedback_type'];	
				}

				/* feedback question code */

				$specialization = $_POST['specialization'];;	
				$teacher = $_POST['stakeholder'];;	
				$st = '_Questions.json';	
				$stringss = $_POST['feedback_type'];
				$stringfy = $_POST['Program'];

				if($stringss=='SSS') {
					$studentFeedback="uploads/feedback/$year/question/102_$stringss$stringfy$st";
					$studentfeed = file_get_contents($studentFeedback);
					//var_dump($studentfeed);
					$feedback_data = json_decode($studentfeed);
					$data['question_data'] = $feedback_data;
				} elseif($teacher=='Teacher' || $teacher=='Teachers') { 
					//$teacherFeedback="uploads/feedback/$year/question/102_$teacher$stringss$st";
					$teacherFeedback="uploads/feedback/$year/question/102_$stringss$st";
					$teacherfeed = file_get_contents($teacherFeedback);
					//var_dump($studentfeed);
					$teacher_data = json_decode($teacherfeed);
					$data['question_data'] = $teacher_data;
				} else {
					$fullPath = "uploads/feedback/$year/question/102_$teacher$stringss$specialization$st";
					//if(file_exists($fullPath)){
						$strFileContents = file_get_contents($fullPath);
						//var_dump($strFileContents);
						$question_data = json_decode($strFileContents);
					//}
					$data['question_data'] = $question_data;
				// echo "<pre>"; print_r($data['question_data']); die;
				}
			}
		}
		// echo "<pre>"; print_r($data); die;
		$this->load->view('staff/common/header', $data);
		$this->load->view('staff/dashboard', $data);
		$this->load->view('staff/common/footer');
	}

	public function edit($id) {
		if (empty($id)) {
			show_404();
        }
        $this->load->helper('form');
        $this->load->library('form_validation');
		$userID = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry($userID);
		$data['userID'] = $userID;
		$this->form_validation->set_rules('email', 'Email Id', 'required');
		$this->form_validation->set_rules('mobile_number', 'Mobile Number', 'trim|required|numeric');
		
		if ($this->form_validation->run() === FALSE) {
            $this->load->view('staff/common/header', $data);
			$this->load->view('staff/home', $data);
			$this->load->view('staff/common/footer');
        } else {
        	//echo "<pre>"; print_r($_POST); //die;
        	$date_of_joining =  (!empty($this->input->post('joining_date'))) ? date('Y-m-d',strtotime($this->input->post('joining_date'))) : "";
        	$date_of_retire = (!empty($this->input->post('retire_date'))) ? date('Y-m-d',strtotime($this->input->post('retire_date'))) : "";
        	$update_data = array(
				'email' => $this->input->post('email'),
				'state' => $this->input->post('state'),
				'mobile_number' => $this->input->post('mobile_number'),
				'date_of_joining' => $date_of_joining,
				'date_of_retire' => $date_of_retire,
				'qualification' => $this->input->post('qualification'),
				'total_experience' => $this->input->post('total_experience'),
				'industry_experience' => $this->input->post('industry_experience'),
				'pan_number' => $this->input->post('pan'),
				'aadhaar_number' => $this->input->post('aadhaar'),
				'bank_account_number' => $this->input->post('account_number'),
				'bank_name' => $this->input->post('bank_name'),
				'ifsc_code' => $this->input->post('ifsc_code'),
				'account_holder_name' => $this->input->post('account_holder_name'),
	        ); 	
        	//print_r($update_data); die;
            $this->staff->update_details($id,$update_data);
			$this->session->set_flashdata('msg', 'Details successfully Updated');
			//redirect('controller_name/addRoom');
			redirect(site_url('staff/staffs'));
        }
    }

	public function ftl() {
		$user_id = $this->input->post('id');
		$this->staff->ftl($user_id);
	}

	
	public function leaveSubmit(){
		$this->load->helper('form');
		$this->load->library('form_validation');
		$role = $this->input->post('role', true);
		$leave_from = $this->input->post('leave_from', true);
		$leave_to = $this->input->post('leave_to', true);
		$userID = $this->session->userdata('userID');
		$userdata = $this->staff->get_entry($userID);
		$department = $userdata[0]->department;
		$role = $userdata[0]->role;
		if($role == "Teaching Staff"){
			$head_data = $this->staff->get_hod_data($department);
		}elseif($role == "HOD" or $role == "Non-teaching Staff"){
			$head_data = $this->staff->get_os_data();
		}elseif($role = "OS"){
			$head_data = $this->staff->get_principal_data();
		}
		
		$email_id = $head_data['email'];
		//$employee_data = $this->StaffExcelExport_model->get_staff_details();
		$this->form_validation->set_rules('leave_from', 'Leave From', 'required');
		$this->form_validation->set_rules('leave_to', 'Leave To', 'trim|required');
		$this->form_validation->set_rules('leave_reason', 'Leave Reason', 'trim|required');
		$this->form_validation->set_rules('leave_pending_work', 'Leave Pending Work', 'trim|required');
		$this->form_validation->set_rules('leave_hand_given', 'Leave Hand Given', 'trim|required');
		if($leave_from == $leave_to && $role = "Non-teaching Staff"){
			$this->form_validation->set_rules('leave_for', 'Leave For', 'required');
		}
		if ($this->form_validation->run() == false) {
			$response = array(
				'status' => 'error',
				'message' => validation_errors()
			);
		}
		else {
			$leave_for ="";
			if ($this->input->post('leave_from')==$this->input->post('leave_to') && $this->input->post('leave_type')!="Half-Day-Leave") {
				$leave_for = $this->input->post('leave_for');
			}
			//echo "<pre>"; print_r($_POST);print_r($_FILES); die;
			$doc_name ='';
			if(($_FILES['leave_doc']['name'] != "" )){
				$new_name = time();
				$file_ext = pathinfo($_FILES["leave_doc"]['name'],PATHINFO_EXTENSION);
				$file_name = $new_name.".".$file_ext;
				$config = array(
					'file_name' => $file_name,
					'upload_path' => './uploads/leave_documents',
					'allowed_types' => 'jpg|png|pdf',
					'overwrite' => TRUE,
					'max_size' => "30270",
				);
				$this->load->library('upload', $config);
				if(!$this->upload->do_upload('leave_doc')){
					$error = array('error' => $this->upload->display_errors());
					echo json_encode(['success'=> false, 'msg' => "Document not uploaded..$error"]);
				}else{
					$doc_name = $file_name;
				}
			}

			$data = array(
				'staff_id' => $this->input->post('staff_id', true),
				'leave_type' => $this->input->post('leave_type', true),
				'leave_from' => date('Y-m-d',strtotime($this->input->post('leave_from', true))),
				'leave_to' => date('Y-m-d',strtotime($this->input->post('leave_to', true))),
				'leave_reason' => $this->input->post('leave_reason', true),
				'leave_for' => $leave_for,
				'leave_pending_work' => $this->input->post('leave_pending_work', true),
				'leave_hand_given' => $this->input->post('leave_hand_given', true),
				'leave_doc' => $doc_name,
				'date_applied' => date('Y-m-d')
					
			);
			$data = $this->security->xss_clean($data);
			$this->staff->insert_contact($data);
			$date_from = $this->input->post('leave_from');
			$date_to = $this->input->post('leave_to');
			$name = $userdata[0]->firstname." ".$userdata[0]->lastname;
			$to = $email_id;
			$subject = "Application For ".$this->input->post('leave_type')."";
			$message = "".$name. " wants to take a ".$this->input->post('leave_type').". Leave details are given below:";
			$message .= "<table style='width:100%'>
			<tr>
				<td>
					<div>
						<p><span style='font-weight:500'>Leave From</span>: " .$date_from."</p>
						<p><span style='font-weight:500'>Leave To</span>: " .$date_to."</p>
						<p><span style='font-weight:500'>Reason</span>: " .$this->input->post('leave_reason')."</p>
						<p><span style='font-weight:500'>Backlog/Pending work</span>: " .$this->input->post('leave_pending_work')."</p>
					</div>
				</td>
			</tr>
			</table>";
			$header = "From: ".$userdata[0]->email." \r\n";
			//$header .= "Cc:afgh@somedomain.com \r\n";
			$header .= "MIME-Version: 1.0\r\n";
			$header .= "Content-type: text/html\r\n";
			$retval = mail ($to,$subject,$message,$header);

		/* 	if( $retval == true ) {
			echo "Message sent successfully...";
			}else {
			echo "Message could not be sent...";
			} */
			echo json_encode(['success'=> true, 'msg' => 'Your request has been successfully submitted.']);
			$this->session->set_flashdata('msg', 'Your request has been successfully submitted.');
		}
	}
	
	public function searchRequest() {
		$userID = $this->session->userdata("userID");
		$leave_data = $this->staff->get_entry( $userID );
		$employee_code = $leave_data[0]->employee_code;
		$details = $this->staff->get_entry($userID);
		$emp_name = $details[0]->firstname.' '.$details[0]->lastname;
		$role = $details[0]->role;
		$records = $this->staff->fetch_data($employee_code, $role);
		$draw = $this->input->post('draw');
		## Fetch records
		$data = array();
		$count = $_POST['start'];
		$n = 1;
		foreach($records as $record){
			$count++;
			//$course_type = (empty($record->course_type)) ? 'Regular': 'Honors';
			//$course_name = $record->year . '-'.$course_type;
			
			$visibility = "";
			if($record->hod_approval_status == "Approved" || $record->status == "HoD Approved" || $record->status == "OS Approved" || $record->status == "Principal Approved"){
				$visibility = "isdisabled";
			}
			
			if(!empty($record->leave_from) && $record->leave_from != '0000-00-00'){
				$leave_from = date("d-m-Y", strtotime($record->leave_from));
			}else{
				$leave_from = "";
			}
			
			if(!empty($record->leave_to) && $record->leave_to != '0000-00-00'){
				$leave_to =  date('d-m-Y',strtotime($record->leave_to));
			}else{
				$leave_to = "";
			}
			
			if(!empty($record->date_applied) && $record->date_applied != '0000-00-00'){
				$date_applied =  date('d-m-Y',strtotime($record->date_applied));
			}else{
				$date_applied = "";
			}
			
			$staff_id = $record->staff_id;
			$emp_detail = $this->staff->employee_data_byid($staff_id);
			$emp_name = $emp_detail[0]->firstname.' '.$emp_detail[0]->lastname;
			
			$hod_approval_status = $record->hod_approval_status;
			$os_approval_status = $record->os_approval_status;
			$principal_approval_status = $record->principal_approval_status;
			
			if($hod_approval_status == "Approved" && $os_approval_status == "Approved" && $principal_approval_status == "Approved" && $record->role == "Teaching Staff"){
				$status = "Approved";
				$hod_approval_status = $record->hod_approval_status;
				$os_approval_status = $record->os_approval_status;
				$principal_approval_status = $record->principal_approval_status;
			}elseif($hod_approval_status == "Pending" && $os_approval_status == "Approved" && ($record->role == "Non-teaching Staff" || $record->role == "HOD")){
				$status = $record->status;
				$hod_approval_status = "";
				$os_approval_status = $record->os_approval_status;
				$principal_approval_status = $record->principal_approval_status;
			}elseif($hod_approval_status == "Pending" && $os_approval_status == "Pending" && $record->role == "OS"){
				$status = $record->status;
				$hod_approval_status = "";
				$os_approval_status = "";
				$principal_approval_status = $record->principal_approval_status;
			}else{
				$hod_approval_status = $record->hod_approval_status;
				$os_approval_status = $record->os_approval_status;
				$principal_approval_status = $record->principal_approval_status;
				$status = $record->status;
			}	
				
				
			$data[] = array(
				"s_no" =>$n,
				"staff_id" => $emp_name,
				"leave_from" => $leave_from,
				"leave_to" => $leave_to,
				"hod_approval_status" => $hod_approval_status,
				"os_approval_status" => $os_approval_status,
				"principal_approval_status" => $principal_approval_status,
				"status" => $status,
				'view_details' => '<button type="button" data-toggle="modal" data-target="#viewDetails" class="btn btn-success">Details</button>',
				'date_applied' => $date_applied,
				"action" => '<a class="remove '.$visibility.'" title="delete-record" data-id="'.$record->id.'" href="'.site_url('staff/requestlist/requestdetails/delete/'.$record->id).'"><i class="fa fa-trash btn btn-danger" aria-hidden="true"></i></a>'
			); 
			$n++;
		}

		## Response
		$response = array(
			"draw" => intval($draw),
			"recordsTotal" => $this->staff->count_all_by_id($employee_code),
			"recordsFiltered" => $this->staff->count_filtered($employee_code, $role),
			"aaData" => $data
		);
		echo json_encode($response);
	}
	
	
	public function searchRequestHead() {
		$userID = $this->session->userdata("userID");
		$leave_data = $this->staff->get_entry( $userID );
		$details = $this->staff->get_entry($userID);
		$role = $details[0]->role;
		$dept = $details[0]->department;
		if($role == 'HOD'){
			//$data['staff'] = $this->staff->get_staff_by_department($dept);
			$records = $this->staff->fetch_data_by_dept($dept);
		}else{
			$records = $this->staff->fetch_data_head($role);
		}
		$draw = $this->input->post('draw');
		## Fetch records
		$data = array();
		$count = $_POST['start'];
		$n = 1;
		foreach($records as $record){
			//echo "<pre>"; print_r($record); echo "</pre>";
			$count++;
			//$course_type = (empty($record->course_type)) ? 'Regular': 'Honors';
			//$course_name = $record->year . '-'.$course_type;
			
			$visibility = "";
			if($record->status == "Approved"){
				$visibility = "isdisabled";
			}
			
			if(!empty($record->leave_from) && $record->leave_from != '0000-00-00'){
				$leave_from = date("d-m-Y", strtotime($record->leave_from));
			}else{
				$leave_from = $record->leave_from;
			}
			
			if(!empty($record->leave_to) && $record->leave_to != '0000-00-00'){
				$leave_to =  date('d-m-Y',strtotime($record->leave_to));
			}else{
				$leave_to = $record->leave_to;
			}
			
			if(!empty($record->date_applied) && $record->date_applied != '0000-00-00'){
				$date_applied =  date('d-m-Y',strtotime($record->date_applied));
			}else{
				$date_applied = $record->date_applied;
			}
			
			$id = $record->id;
			$staff_id = $record->staff_id;
			$emp_detail = $this->staff->employee_data_byid($staff_id);
			$emp_name = $emp_detail[0]->firstname.' '.$emp_detail[0]->lastname;
			
			//echo "<pre>"; print_r($record); echo "</pre>";
			
			
			$approved = '';
			$rejected = '';
			
			if($role == 'HOD' && $record->hod_approval_status != 'Pending'){
				if($record->hod_approval_status == 'Approved'){
					$approved = 'checked';
				}else{
					$rejected = 'checked';
				}
				$comment = $record->hod_reason;
			}elseif($role == 'HOD' && $record->hod_approval_status == 'Pending'){
				$approved = '';
				$rejected = '';
				$comment = '';
			}
			
			if($role == 'Office Supretendent' && $record->os_approval_status != 'Pending'){
				if($record->os_approval_status == 'Approved'){
					$approved = 'checked';
				}else{
					$rejected = 'checked';
				}
				$comment = $record->os_reason;
			}elseif($role == 'Office Supretendent' && $record->os_approval_status == 'Pending'){
				$approved = '';
				$rejected = '';
				$comment = '';
			}
			
			if($role == 'Principal' && $record->principal_approval_status != 'Pending'){
				if($record->principal_approval_status == 'Approved'){
					$approved = 'checked';
				}else{
					$rejected = 'checked';
				}
				$comment = $record->principal_reason;
			}elseif($role == 'Principal' && $record->principal_approval_status == 'Pending'){
				$approved = '';
				$rejected = '';
				$comment = '';
			}
			
			//date_default_timezone_set('Asia/Kolkata'); 
			$current_date = date("d-m-Y");
			$curr_date = strtotime($current_date);
			$leave_from1 = strtotime($leave_from);
			$leave_to1 = strtotime($leave_to);
			if($leave_from1 >= $curr_date && $leave_to1 >= $curr_date){
				$display = 'block';
			}else{
				$display = 'none';
			} 
	
			
			$data[] = array(
				"s_no" =>$n,
				"staff_id" => $emp_name,
				"leave_from" => $leave_from,
				"leave_to" => $leave_to,
				"status" => $record->status,
				'view_details' => '<button type="button" id="'.$id.'" data-toggle="modal" data-leavetype="'.$record->leave_type.'" data-from="'.$leave_from.'" data-to="'.$leave_to.'" data-reason="'.$record->leave_reason.'" data-backlog="'.$record->leave_pending_work.'" data-handover="'.$record->leave_hand_given.'" data-leavedoc="'.$record->leave_doc.'" class="details_modal_button btn btn-success">Details</button>',
				'approve_reject' => '<form method="post" id="approve_reject"><input type="hidden" name="id" id="id" value="'.$id.'"> <input type="hidden" name="staff_id" id="staff_id" value="'.$staff_id.'"> <input type="hidden" name="role" id="role" value="'.$role.'"> <input type="hidden" name="os_appoval_status" id="os_appoval_status" value="'.$record->os_approval_status.'"> <input type="hidden" name="hod_approval_status" id="hod_approval_status" value="'.$record->hod_approval_status.'"> <input type="hidden" name="principal_approval_date" id="principal_approval_date" value="'.$record->principal_approval_date.'">  <div class="form-group">  <input type="radio" class="radio" name="apv_rej" id="apv_rej" value="Approved" '.$approved.'> <label for="approve" class="form-check-label">Approve</label> &nbsp; <input class="radio" type="radio" name="apv_rej" id="apv_rej" value="Rejected" '.$rejected.'> <label for="reject" class="form-check-label">Reject</label> </div> <div class="form-group" style="margin-top:10px;"> <label for="comment" class="control-label">Comments</label> </br><textarea name="comment" id="comment" placeholder="Comment">'.$comment.'</textarea></div> <div class="form-group" style="display:'.$display.'" data-id="'.$leave_from.'"><input type="submit" class="btn btn-primary" name="apv_reject_save" id="apv_reject_save" value="Save"></div></form>',
				'date_applied' => $date_applied,
			);
			$n++;
		}

		## Response
		if($role == 'HOD'){
			$response = array(
				"draw" => intval($draw),
				"recordsTotal" => $this->staff->count_all_by_dept($dept),
				"recordsFiltered" => $this->staff->count_filtered_head_by_dept($dept),
				"aaData" => $data
			);
		}else{
			$response = array(
				"draw" => intval($draw),
				"recordsTotal" => $this->staff->count_all_by_role($role),
				"recordsFiltered" => $this->staff->count_filtered_head_by_role($role),
				"aaData" => $data
			);
		}
		echo json_encode($response);
	}
	
/* 	public function edit_application($id) {
		if (empty($id)) {
			show_404();
        }
        $this->load->helper('form');
        $this->load->library('form_validation');
		$data['request'] = $this->staff->get_request_by_id($id);
		$userID = $data['request']['staff_id'];
		$id = $this->session->userdata("userID");
		$data['staff_details'] = $this->staff->get_entry($id);
		$this->load->view('staff/common/header',$data);
		$this->load->view('staff/edit_request',$data);
		$this->load->view('staff/common/footer');
    }  */
	
	public function delete_record($id) {
		$this->staff->delete_request($id);
		$this->session->set_flashdata('msg', 'Application Successfully Deleted');
		echo json_encode(['success'=> true, 'msg' => 'Application Successfully Deleted.']);
		//redirect(site_url('staff/leave_application'));
    } 
	
	public function application_reject_approve(){
		//echo "<pre>"; print_r($_POST); echo "</pre>";
		$staff_id = $_POST['staff_id'];
		$id = $_POST['id'];
		$role = $_POST['role'];
		$req_status = $_POST['apv_rej'];
		$hod_approval_status = $_POST['hod_approval_status'];
		$os_approval_status = $_POST['os_appoval_status'];
		$comment = $_POST['comment'];
		$staff_role = $this->staff->get_staff_role($staff_id);
		$leave_details = $this->staff->get_leave_details($id);
		$current_user = $this->session->userdata('userID');
		$head_data = $this->staff->get_head_data($current_user);
		$firstname = $staff_role['firstname'];
		//  echo "<pre>"; print_r($staff_role); echo "</pre>";
		// die; 
		date_default_timezone_set('Asia/Kolkata');
		$date = new DateTime();
		if($role == 'HOD'){
			$data = array(
				'status' => 'HoD '.$req_status,
				'hod_approval_status' => $req_status,
				'hod_approval_date' => $date->format('Y-m-d H:i:s'),
				'hod_reason' => $comment,
			);
		}elseif($role == 'Office Supretendent' && ($hod_approval_status == 'Approved' ) || ($hod_approval_status == 'Pending' && $staff_role['role'] == 'Non-teaching Staff' && $os_approval_status == 'Pending') || ($staff_role['role'] == 'HOD' && $os_approval_status == 'Pending' && $hod_approval_status == 'Pending')){
			$data = array(
				'status' => 'OS '.$req_status,
				'os_approval_status' => $req_status,
				'os_approval_date' => $date->format('Y-m-d H:i:s'),
				'os_reason' => $comment,
			);
		}elseif($role == 'Principal' && ($os_approval_status == 'Approved' && ($staff_role['role'] == 'Non-teaching Staff' || $staff_role['role'] == 'Teaching Staff' || $staff_role['role'] == 'HOD'))  || ($os_approval_status == 'Pending' && $staff_role['role'] == 'Office Supretendent')){
			$data = array(
				'status' => 'Principal '.$req_status,
				'principal_approval_status' => $req_status,
				'principal_approval_date' => $date->format('Y-m-d H:i:s'),
				'principal_reason' => $comment,
			);
		}
		/* echo "<pre>"; print_r($data); echo "</pre>";
		die; */
		$data = $this->security->xss_clean($data);
		$result = $this->staff->application_status($data, $id);
		if($result){
			if($head_data['role'] == 'HOD'){
				// Email notification to staff who apply for leave
				$email_id = $staff_role['email'];
				$to = $email_id;
				//$to = 'prashantbahuguna5693@gmail.com';
				$subject = $leave_details['leave_type'].'-'.$req_status."-".$leave_details['leave_from']." to ".$leave_details['leave_to'];
				$message = '';
				$message .= '<tr><td colspan=2>Hello '.$firstname.',</td></tr>
					<tr><td colspan=2 style="padding-bottom:15px;">' .$head_data['role'].' has '.$req_status.' your leave '.$leave_details['leave_from'].' to '.$leave_details['leave_to'].'</td></tr>
					<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Comments by ' .$head_data['role'].'</td></tr>
					<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">'.$leave_details['hod_reason'].'</td></tr>
					<tr><td colspan=2>Thank You,</td></tr>
					<tr><td colspan=2>SVT College of Home Science</td></tr>';
				$header = "From: ".$head_data['email']." \r\n";
				//$header .= "Cc:afgh@somedomain.com \r\n";
				$header .= "MIME-Version: 1.0\r\n";
				$header .= "Content-type: text/html\r\n";
				$retval = mail ($to,$subject,$message,$header);
				
				//Email notification to management
				
				$name = $staff_role['firstname'].' '.$staff_role['lastname'];
				$user_role = 'Office Supretendent';
				$head_data = $this->staff->get_head_data_by_role($user_role);
				$head_to = $head_data['email'];
				//$head_to = "negipooja137@gmail.com";
				$subject1 = "Application For ".$leave_details['leave_type']."";
				$message1 = "".$name. " wants to take a ".$leave_details['leave_type'].". Leave details are given below:";
				$message1 .= "<table style='width:100%'>
				<tr>
					<td>
						<div>
							<p><span style='font-weight:500'>Leave From</span>: " .date('d-m-Y',strtotime($leave_details['leave_from']))."</p>
							<p><span style='font-weight:500'>Leave To</span>: " .date('d-m-Y',strtotime($leave_details['leave_to']))."</p>
							<p><span style='font-weight:500'>Reason</span>: " .$leave_details['leave_reason']."</p>
							<p><span style='font-weight:500'>Backlog/Pending work</span>: " .$leave_details['leave_pending_work']."</p>
						</div>
					</td>
				</tr>
				</table>";
				$header1 = "From: ".$staff_role['email']." \r\n";
				//$header .= "Cc:afgh@somedomain.com \r\n";
				$header1 .= "MIME-Version: 1.0\r\n";
				$header1 .= "Content-type: text/html\r\n";
				//$retval1 = mail($head_to,$subject1,$message1,$header1);
				$retval = mail($head_to, $subject1, $message1, $header1);
			}elseif($head_data['role'] == 'Office Supretendent'){
				// Email notification to staff who apply for leave
				$email_id = $staff_role['email'];
				$to = $email_id;
				//$to = 'prashantbahuguna5693@gmail.com';
				$subject = "Your Request " .$req_status. "";
				$message = "Your leave request has been ".$req_status ." by ".$head_data['role']."";
				$subject = $leave_details['leave_type'].'-'.$req_status."-".$leave_details['leave_from']." to ".$leave_details['leave_to'];
				$message = '';
				$message .= '<tr><td colspan=2>Hello '.$firstname.',</td></tr>
					<tr><td colspan=2 style="padding-bottom:15px;">' .$head_data['role'].' has '.$req_status.' your leave '.$leave_details['leave_from'].' to '.$leave_details['leave_to'].'</td></tr>
					<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Comments by ' .$head_data['role'].'</td></tr>
					<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">'.$leave_details['os_reason'].'</td></tr>
					<tr><td colspan=2>Thank You,</td></tr>
					<tr><td colspan=2>SVT College of Home Science</td></tr>';
				$header = "From: ".$head_data['email']." \r\n";
				//$header .= "Cc:afgh@somedomain.com \r\n";
				$header .= "MIME-Version: 1.0\r\n";
				$header .= "Content-type: text/html\r\n";
				$retval = mail ($to,$subject,$message,$header);
				
				//Email notification to management
				
				$name = $staff_role['firstname'].' '.$staff_role['lastname'];
				$user_role = 'Principal';
				$head_data = $this->staff->get_head_data_by_role($user_role);
				$head_to = $head_data['email'];
				//$head_to = "negipooja137@gmail.com";
				$subject1 = "Application For ".$leave_details['leave_type']."";
				$message1 = "".$name. " wants to take a ".$leave_details['leave_type'].". Leave details are given below:";
				$message1 .= "<table style='width:100%'>
				<tr>
					<td>
						<div>
							<p><span style='font-weight:500'>Leave From</span>: " .date('d-m-Y',strtotime($leave_details['leave_from']))."</p>
							<p><span style='font-weight:500'>Leave To</span>: " .date('d-m-Y',strtotime($leave_details['leave_to']))."</p>
							<p><span style='font-weight:500'>Reason</span>: " .$leave_details['leave_reason']."</p>
							<p><span style='font-weight:500'>Backlog/Pending work</span>: " .$leave_details['leave_pending_work']."</p>
						</div>
					</td>
				</tr>
				</table>";
				$header1 = "From: ".$staff_role['email']." \r\n";
				//$header .= "Cc:afgh@somedomain.com \r\n";
				$header1 .= "MIME-Version: 1.0\r\n";
				$header1 .= "Content-type: text/html\r\n";
				//$retval1 = mail($head_to,$subject1,$message1,$header1);
				$retval = mail($head_to, $subject1, $message1, $header1);
				
			}elseif($head_data['role'] == 'Principal'){
				$employee_code = $staff_role['employee_code'];
				$principal_approval_status = $leave_details['principal_approval_status'];
				$status = $leave_details['status'];
				$d1 = $leave_details['leave_from'];
				$d2 = $leave_details['leave_to'];
				$diff = strtotime($d2) - strtotime($d1);
				$days =  round($diff / (60 * 60 * 24));	
				// echo $days."<br>";
				if($d1 == $d2 && $leave_details['leave_for'] == 'Half-Day'){
					$days = 0.5;
				}elseif($d1 == $d2 && $leave_details['leave_for'] == 'Full-Day'){
					$days = 1;
				}elseif($d1 == $d2){
					$days = 1;
				}else{
					$days = $days + 1;
				}
				$casual_leave = $staff_role['casual_leave_balance'];
				$sick_leave = $staff_role['sick_leave_balance'];
				$paid_leave = $staff_role['paid_leave_balance'];
				$unpaid_leave = $staff_role['unpaid_leave'];
				$half_day_leave = $staff_role['half_day_leave'];
				$compensate_leave = $staff_role['compensate_leave'];

				$pending_casual_leave = $casual_leave - $days;
				$pending_sick_leave = $sick_leave - $days;
				$pending_paid_leave = $paid_leave - $days;
				$total_unpaid_leave = $unpaid_leave + $days;
				$total_half_day_leave = $half_day_leave + $days;
				$total_compensate_leave = $compensate_leave + $days;

				if($leave_details['leave_type'] == 'Casual-Leave' && $status == 'Principal Approved' && $principal_approval_status == 'Approved'){
					$update_data = array(
						'leave_type' => $leave_details['leave_type'],
						'days' => $pending_casual_leave
					);
					$this->staff->update_leave_balance($employee_code, $update_data); 
				}elseif($leave_details['leave_type'] == 'Sick-Leave' && $status == 'Principal Approved' && $principal_approval_status == 'Approved'){
					$update_data = array(
						'leave_type' => $leave_details['leave_type'],
						'days' => $pending_sick_leave
					);
					$this->staff->update_leave_balance($employee_code, $update_data);
				}elseif($leave_details['leave_type'] == 'Paid-Leave' && $status == 'Principal Approved' && $principal_approval_status == 'Approved'){
					$update_data = array(
						'leave_type' => $leave_details['leave_type'],
						'days' => $pending_paid_leave
					);
					$this->staff->update_leave_balance($employee_code, $update_data);
				}elseif($leave_details['leave_type'] == 'Unpaid-Leave' && $status == 'Principal Approved' && $principal_approval_status == 'Approved'){
					$update_data = array(
						'leave_type' => $leave_details['leave_type'],
						'days' => $total_unpaid_leave
					);
					$this->staff->update_leave_balance($employee_code, $update_data);
				}elseif($leave_details['leave_type'] == 'Half-Day-Leave' && $status == 'Principal Approved' && $principal_approval_status == 'Approved'){
					$update_data = array(
						'leave_type' => $leave_details['leave_type'],
						'days' => $total_half_day_leave
					);
					$this->staff->update_leave_balance($employee_code, $update_data);
				}elseif($leave_details['leave_type'] == 'Compensate-Leave' && $status == 'Principal Approved' && $principal_approval_status == 'Approved'){
					$update_data = array(
						'leave_type' => $leave_details['leave_type'],
						'days' => $total_compensate_leave
					);
					$this->staff->update_leave_balance($employee_code, $update_data);
				}  
				// Email notification to staff who apply for leave
				$email_id = $staff_role['email'];
				$to = $email_id;
				//$to = 'prashantbahuguna5693@gmail.com';
				$subject = $leave_details['leave_type'].'-'.$req_status."-".$leave_details['leave_from']." to ".$leave_details['leave_to'];
				$message = '';
				$message .= '<tr><td colspan=2>Hello '.$firstname.',</td></tr>
					<tr><td colspan=2 style="padding-bottom:15px;">' .$head_data['role'].' has '.$req_status.' your leave '.$leave_details['leave_from'].' to '.$leave_details['leave_to'].'</td></tr>
					<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">Comments by ' .$head_data['role'].'</td></tr>
					<tr><td colspan=2 style="padding-top:15px; padding-bottom:15px;">'.$leave_details['principal_reason'].'</td></tr>
					<tr><td colspan=2>Thank You,</td></tr>
					<tr><td colspan=2>SVT College of Home Science</td></tr>';
				//$message = "".$head_data['role']." " .$req_status ." your request for leave.";
				$header = "From: ".$head_data['email']." \r\n";
				//$header .= "Cc:afgh@somedomain.com \r\n";
				$header .= "MIME-Version: 1.0\r\n";
				$header .= "Content-type: text/html\r\n";
				$retval = mail ($to,$subject,$message,$header); 
			
			}
			echo json_encode(['success'=> true, 'msg' => 'Request '.$req_status.' Successfully']);
			$this->session->set_flashdata('msg', 'Request '.$req_status.' Successfully.');
		}
		
	}
	
	function approve_elective(){
		$data = array(
			'program' => $_POST['program'],
			'semester' => $_POST['sem'],
			'year' => $_POST['year']
		);
		echo json_encode(['success'=> true, 'program' => $data['program'], 'semester' => $data['semester'], 'year' => $data['year']]);
	}
	public function change_password() {
		$this->load->helper('form');
        $this->load->library('form_validation');
		$this->form_validation->set_rules('new_password', 'New Password', 'required|min_length[3]|max_length[20]');
		$this->form_validation->set_rules('conf_password', 'Confirm Password', 'required|min_length[3]|max_length[20]');
		if($this->form_validation->run()){			
			$this->staff->update_password();
			echo json_encode(['success'=>'Password Changed Succesfully']);
		} else {
			$errors = validation_errors();
            echo json_encode(['error'=>$errors]);
		}		
	}
}