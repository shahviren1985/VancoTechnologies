<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Cron_model extends CI_Model {
	
	public function get_todays_transaction() {
		$db_query = $this->load->database('clg_db2', TRUE);
		$date = date("Y-m-d");
    	$date_now = date('Y-m-d', strtotime($date. ' - 1 days'));
		//$date_now = date('Y-m-d');
		// $date_now = "2019-11-24";
		
		$db_query->select('transaction_details.*,course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form')
		 ->from('transaction_details')
		 ->join('course_details', 'transaction_details.course_id = course_details.id');
		$db_query->where('fee_paid_date = "'.$date_now.'" AND transaction_status = "Paid"');
		$db_query->order_by('id','desc');
		$query = $db_query->get();
		$records = $query->result();
				
		$transactions = array();
		foreach($records as $key =>$record){
			if(!empty($record->course_type)){
				$course_name = $record->year.'-Honors';
			}else {
				$course_name = $record->year.'-Regulars';
			}
			$transactions[] = array(
				'college_registration_number'=>$record->student_id,
				'course_name'=>$course_name,
				'specialization'=>$record->specialization,
				'category'=>$record->category,
				'email'=>$record->email_id,
				'challan_number'=>$record->challan_number,
				'fee_paid_date'=>date('d/m/Y',strtotime($record->fee_paid_date)),
				'lastname'=>$record->lastname,
				'firstname'=>$record->firstname,
				'fathername'=>$record->middlename,
				'mothername'=>$record->mothername,
				'payment_mode'=>$record->payment_mode,
				'payment_type'=>$record->payment_type,
				'transaction_number'=>$record->transaction_ref_number,
				'transaction_status'=>$record->transaction_status,
				'bank_name'=>$record->bank_name,
				'branch_name'=>$record->branch_name,
				'cheque_number'=>$record->cheque_number,
				'cheque_date'=>(!empty($record->cheque_date)) ? date('d/m/Y',strtotime($record->cheque_date)) : '',
				/*'total_amount'=>number_format($record->total_amount,2),*/
				'total_fees'=>$record->total_amount,
				'remark'=>$record->remark,
				/*'total_paid'=>number_format($record->total_paid,2),*/
				'grand_total'=>$record->total_paid,
			);
		}
		return $transactions;
 	}

 	public function get_todays_transaction_mis1() {
		$db_query = $this->load->database('clg_db1', TRUE);
		$date = date("Y-m-d");
    	$date_now = date('Y-m-d', strtotime($date. ' - 1 days'));
		// $date_now = "2019-11-24";
		
		$db_query->select('transaction_details.*,course_details.course_name as course_type,course_details.year, course_details.specialization,course_details.short_form')
		 ->from('transaction_details')
		 ->join('course_details', 'transaction_details.course_id = course_details.id');
		$db_query->where('fee_paid_date = "'.$date_now.'" AND transaction_status = "Paid"');
		$db_query->order_by('id','desc');
		$query = $db_query->get();
		$records = $query->result();
				
		$transactions = array();
		foreach($records as $key =>$record){
			if(!empty($record->course_type)){
				$course_name = $record->year.'-Honors';
			}else {
				$course_name = $record->year.'-Regulars';
			}
			$transactions[] = array(
				'college_registration_number'=>$record->student_id,
				'course_name'=>$course_name,
				'specialization'=>$record->specialization,
				'category'=>$record->category,
				'email'=>$record->email_id,
				'challan_number'=>$record->challan_number,
				'fee_paid_date'=>date('d/m/Y',strtotime($record->fee_paid_date)),
				'lastname'=>$record->lastname,
				'firstname'=>$record->firstname,
				'fathername'=>$record->middlename,
				'mothername'=>$record->mothername,
				'payment_mode'=>$record->payment_mode,
				'payment_type'=>$record->payment_type,
				'transaction_number'=>$record->transaction_ref_number,
				'transaction_status'=>$record->transaction_status,
				'bank_name'=>$record->bank_name,
				'branch_name'=>$record->branch_name,
				'cheque_number'=>$record->cheque_number,
				'cheque_date'=>(!empty($record->cheque_date)) ? date('d/m/Y',strtotime($record->cheque_date)) : '',
				/*'total_amount'=>number_format($record->total_amount,2),*/
				'total_fees'=>$record->total_amount,
				'remark'=>$record->remark,
				/*'total_paid'=>number_format($record->total_paid,2),*/
				'grand_total'=>$record->total_paid,
			);
		}
		return $transactions;
 	}
	
	public function get_todays_alumni_transaction() {
		$db_query = $this->load->database('clg_db2', TRUE);
		$date_now = date('Y-m-d');
		//$date_now = "2019-12-23";
		
		$db_query->select('alumini_payment_details.*,alumini_registration_details.name,alumini_registration_details.residential_address, alumini_registration_details.phone_number, alumini_registration_details.landline, alumini_registration_details.email_address, alumini_registration_details.dob, alumini_registration_details.age, alumini_registration_details.department, alumini_registration_details.specialization, alumini_registration_details.year_of_passing, alumini_registration_details.extra_activity, alumini_registration_details.alumini_message, alumini_registration_details.signature, alumini_registration_details.receipt_number, alumini_registration_details.date_of_payment')
		 ->from('alumini_payment_details')
		 ->join('alumini_registration_details', 'alumini_payment_details.alumini_registration_id = alumini_registration_details.id');
		$db_query->where('fee_paid_date = "'.$date_now.'" AND transaction_status = "Paid"');
		$db_query->order_by('id','desc');
		$query = $db_query->get();
		$records = $query->result();
				
		$transactions = array();
		foreach($records as $key =>$record){
			$transactions[] = array(
				'id'=>$record->id,
				'name' => $record->name,
				'date_of_transaction' => date('d-m-Y'),
				'residential_address'=>$record->residential_address,
				'phone_number'=>$record->phone_number,
				'landline' => $record->landline,
				'email_address' => $record->email_address,
				'dob'=>$record->dob,
				'age'=>$record->age,
				'department'=>$record->department,
				'specialization'=>$record->specialization,
				'year_of_passing'=>$record->year_of_passing,
				'extra_curricular_activity' => $record->extra_activity,
				'contribute_towards_growth' => $record->alumini_message,
				'signature' => $record->signature,
				'receipt_number' => $record->receipt_number,
				'date_of_payment' => $record->date_of_payment,
				'fee_paid_date'=>date('d/m/Y',strtotime($record->fee_paid_date)),
				'payment_mode'=>$record->payment_mode,
				'payment_type'=>$record->payment_type,
				'transaction_number'=>$record->transaction_ref_number,
				'transaction_status'=>$record->transaction_status,
				'total_amount'=>$record->total_amount,
				'remark'=>$record->remark,
				'total_paid'=>$record->total_paid,
			);
		}
		return $transactions;
 	}

	public function get_alumni_event_transaction() {
		$db_query = $this->load->database('clg_db2', TRUE);
		$date_now = date('Y-m-d');
		//$date_now = "2019-12-23";
		
		$db_query->select('alumini_event_registration_payment_details.*,alumini_event_registration.name,alumini_event_registration.phone_number, alumini_event_registration.email_address, alumini_event_registration.residential_address, alumini_event_registration.specialization, alumini_event_registration.year_of_passing, alumini_event_registration.working_status, alumini_event_registration.current_org, alumini_event_registration.current_designation, alumini_event_registration.total_exp, alumini_event_registration.higher_education_status, alumini_event_registration.name_of_qualification, alumini_event_registration.college_university_name')
		 ->from('alumini_event_registration_payment_details')
		 ->join('alumini_event_registration', 'alumini_event_registration_payment_details.alumini_event_registration_id = alumini_event_registration.id');
		$db_query->where('fee_paid_date = "'.$date_now.'" AND transaction_status = "Paid"');
		$db_query->order_by('id','desc');
		$query = $db_query->get();
		$records = $query->result();
				
		$transactions = array();
		foreach($records as $key =>$record){
			$transactions[] = array(
				'id'=>$record->id,
				'name'=>$record->name,
				'date_of_transaction' => date('d-m-Y'),
				'phone_number'=>$record->phone_number,
				'email_address'=>$record->email_address,
				'fee_paid_date'=>date('d/m/Y',strtotime($record->fee_paid_date)),
				'residential_address'=>$record->residential_address,
				'specialization'=>$record->specialization,
				'year_of_passing'=>$record->year_of_passing,
				'working_status'=>$record->working_status,
				'current_org'=>$record->current_org,
				'current_designation'=>$record->current_designation,
				'total_exp'=>$record->total_exp,
				'higher_education_status'=>$record->higher_education_status,
				'name_of_qualification'=>$record->name_of_qualification,
				'college_university_name'=>$record->college_university_name,
				'payment_mode'=>$record->payment_mode,
				'payment_type'=>$record->payment_type,
				'transaction_number'=>$record->transaction_ref_number,
				'transaction_status'=>$record->transaction_status,
				'total_amount'=>$record->total_amount,
				'remark'=>$record->remark,
				'total_paid'=>$record->total_paid,
			);
		}
		return $transactions;
 	}
	
	
	public function get_all_staff_data_cron()
	{
		$db_query = $this->load->database('clg_db2', TRUE);
		$db_query->select('staff_details.*')
		 ->from('staff_details');
		$query = $db_query->get();
		 return $records = $query->result(); 
		
	}
	public function update_leave_balance_cron($employee_id, $update_data)
	{
		$db_query = $this->load->database('clg_db2', TRUE);
		$query = $db_query->update('staff_details', $update_data, array('id' => $employee_id));
		
	}
}