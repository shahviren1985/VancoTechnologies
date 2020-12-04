<?php 
//if ( ! defined('BASEPATH')) exit('No direct script access allowed');
class Cron extends CI_Controller{
    /**
     * This is default constructor of the class
     */
    public function __construct(){
        parent::__construct();
		$this->load->library('email');
		$this->load->model('Cron_model');
    }
    
    /**
     * This function is used to update the age of users automatically
     * This function is called by cron job once in a day at midnight 00:00
     */



    public function sendDailyTransactions(){
		$userEmail='shah.viren1985@gmail.com';
		//$userEmail='apncoders@gmail.com';
		$subject = "SVT Fees Payment - Online Transactions-".date('d-m-Y')."";
		$body = "<table>
				<tr><td colspan=2>Hello Administrator,</td></tr>
				<tr><td colspan=2 style='padding-bottom:15px;'>Please find attached transaction report for today's transactions dated ".date('d-m-Y')." </td></tr>
				<tr><td colspan=2>Thank You,</td></tr>
				<tr><td colspan=2>SVT College of Home Science.</td></tr>
			</table>";
		$config = array(
			'mailtype' => 'html',
			'charset'  => 'utf-8',
			'priority' => '1'
		);
		$this->email->initialize($config);
		$this->email->set_newline("\r\n");
		$this->email->from('info@vancotech.com', 'SVT');
		
		$fileName = 'transactions-details-'.date('d-m-Y').'.xls';
		// load excel library
        $this->load->library('excel');
		$todays_transaction = $this->Cron_model->get_todays_transaction();
		//echo "<pre>";
		//print_r($todays_transaction);
		//echo "<pre>";
		//die;
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);		
		// set Header
		$row = 1;
		foreach($todays_transaction as $header_data ){			
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){	
					$key = ucwords(str_replace("_", " ", $key)); 
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
					$col++;	
				}
			}
			$row++;
		}		
        // set Row        
		$rowCount = 2;
		foreach($todays_transaction as $row_data ){			
			$col = 0;
			foreach($row_data as $key=>$value) {
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
				$col++;
			}
			$rowCount++;
		}
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
        $objWriter->save('uploads/transaction_export_excel/'.$fileName);
		$excel_path = realpath(APPPATH . '../uploads/transaction_export_excel');
		
		// download file
        //header("Content-Type: application/vnd.ms-excel");
		$this->email->to($userEmail);  // replace it with receiver mail id
		$this->email->cc('svtcollege2019@gmail.com');
		$this->email->subject($subject); // replace it with relevant subject
		$this->email->message($body);  
		$this->email->attach($excel_path.'/'.$fileName);
		$this->email->send();
	} 

	
    public function sendDailyTransactionsMis1(){
		$userEmail='shah.viren1985@gmail.com';
		//$userEmail='apncoders@gmail.com';
		$subject = "SVT Fees Payment - Online Transactions-Mis1-".date('d-m-Y')."";
		$body = "<table>
				<tr><td colspan=2>Hello Administrator,</td></tr>
				<tr><td colspan=2 style='padding-bottom:15px;'>Please find attached transaction report for today's transactions dated ".date('d-m-Y')." </td></tr>
				<tr><td colspan=2>Thank You,</td></tr>
				<tr><td colspan=2>SVT College of Home Science.</td></tr>
			</table>";
		$config = array(
			'mailtype' => 'html',
			'charset'  => 'utf-8',
			'priority' => '1'
		);
		$this->email->initialize($config);
		$this->email->set_newline("\r\n");
		$this->email->from('info@vancotech.com', 'SVT');
		
		$fileName = 'transactions-details-Mis1-'.date('d-m-Y').'.xls';
		// load excel library
        $this->load->library('excel');
		$todays_transaction = $this->Cron_model->get_todays_transaction_mis1();
		//echo "<pre>";
		//print_r($todays_transaction);
		//echo "<pre>";
		//die;
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);		
		// set Header
		$row = 1;
		foreach($todays_transaction as $header_data ){			
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){	
					$key = ucwords(str_replace("_", " ", $key)); 
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
					$col++;	
				}
			}
			$row++;
		}		
        // set Row        
		$rowCount = 2;
		foreach($todays_transaction as $row_data ){			
			$col = 0;
			foreach($row_data as $key=>$value) {
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
				$col++;
			}
			$rowCount++;
		}
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
        $objWriter->save('uploads/transaction_export_excel/'.$fileName);
		$excel_path = realpath(APPPATH . '../uploads/transaction_export_excel');
		
		// download file
        //header("Content-Type: application/vnd.ms-excel");
		$this->email->to($userEmail);  // replace it with receiver mail id
		$this->email->cc('svtcollege2019@gmail.com');
		$this->email->subject($subject); // replace it with relevant subject
		$this->email->message($body);  
		$this->email->attach($excel_path.'/'.$fileName);
		$this->email->send();
	} 


	
	
	public function sendAlumniTransactions(){
		$userEmail='shah.viren1985@gmail.com';
		//$userEmail='apncoders@gmail.com';
		$subject="SVT Alumni Registration - Online Transactions - ".date('d-m-Y')."";
		$body = "<table>
				<tr><td colspan=2>Hello Alumni Association,</td></tr>
				<tr><td colspan=2 style='padding-bottom:15px;'>Please find attached transaction report for today's transactions dated ".date('d-m-Y')." </td></tr>
				<tr><td colspan=2>Thank You,</td></tr>
				<tr><td colspan=2>SVT College of Home Science.</td></tr>
			</table>";
		$config = array(
			'mailtype' => 'html',
			'charset'  => 'utf-8',
			'priority' => '1'
		);
		$this->email->initialize($config);
		$this->email->set_newline("\r\n");
		$this->email->from('info@vancotech.com', 'SVT');
		
		$fileName = 'transactions-details-'.date('d-m-Y').'.xls';
		// load excel library
        $this->load->library('excel');
		$todays_transaction = $this->Cron_model->get_todays_alumni_transaction();
		//echo "<pre>";
		//print_r($todays_transaction);
		//echo "<pre>";
		//die;
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);		
		// set Header
		$row = 1;
		foreach($todays_transaction as $header_data ){			
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){	
					$key = ucwords(str_replace("_", " ", $key)); 
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
					$col++;	
				}
			}
			$row++;
		}		
        // set Row        
		$rowCount = 2;
		foreach($todays_transaction as $row_data ){			
			$col = 0;
			foreach($row_data as $key=>$value) {
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
				$col++;
			}
			$rowCount++;
		}
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
        $objWriter->save('uploads/transaction_export_excel/'.$fileName);
		$excel_path = realpath(APPPATH . '../uploads/transaction_export_excel');
		
		// download file
        //header("Content-Type: application/vnd.ms-excel");
		$this->email->to($userEmail);  // replace it with receiver mail id
		$this->email->cc('svtcollege2019@gmail.com');
		$this->email->subject($subject); // replace it with relevant subject
		$this->email->message($body);  
		$this->email->attach($excel_path.'/'.$fileName);
		$this->email->send();
	}	
	
	
	public function sendAlumniEventTransactions(){
		$userEmail='shah.viren1985@gmail.com';
		//$userEmail='apncoders@gmail.com';
		$subject="SVT Alumni Registration - Online Transactions - ".date('d-m-Y')."";
		$body = "<table>
			<tr><td colspan=2>Hello Alumni Association,</td></tr>
			<tr><td colspan=2 style='padding-bottom:15px;'>Please find attached transaction report for today's transactions dated ".date('d-m-Y')." </td></tr>
			<tr><td colspan=2>Thank You,</td></tr>
			<tr><td colspan=2>SVT College of Home Science.</td></tr>
		</table>";
		$config = array(
			'mailtype' => 'html',
			'charset'  => 'utf-8',
			'priority' => '1'
		);
		$this->email->initialize($config);
		$this->email->set_newline("\r\n");
		$this->email->from('info@vancotech.com', 'SVT');
		
		$fileName = 'transactions-details-'.date('d-m-Y').'.xls';
		// load excel library
        $this->load->library('excel');
		$todays_transaction = $this->Cron_model->get_alumni_event_transaction();
		//echo "<pre>";
		//print_r($todays_transaction);
		//echo "<pre>";
		//die;
		$objPHPExcel = new PHPExcel();
        $objPHPExcel->setActiveSheetIndex(0);		
		// set Header
		$row = 1;
		foreach($todays_transaction as $header_data ){			
			$col = 0;
			foreach($header_data as $key=>$value) {
				if($row == 1){	
					$key = ucwords(str_replace("_", " ", $key)); 
					$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $row, $key);
					$col++;	
				}
			}
			$row++;
		}		
        // set Row        
		$rowCount = 2;
		foreach($todays_transaction as $row_data ){			
			$col = 0;
			foreach($row_data as $key=>$value) {
				$objPHPExcel->getActiveSheet()->setCellValueByColumnAndRow($col, $rowCount, $value);
				$col++;
			}
			$rowCount++;
		}
		$objWriter = new PHPExcel_Writer_Excel2007($objPHPExcel);
        $objWriter->save('uploads/transaction_export_excel/'.$fileName);
		$excel_path = realpath(APPPATH . '../uploads/transaction_export_excel');
		
		// download file
        //header("Content-Type: application/vnd.ms-excel");
		$this->email->to($userEmail);  // replace it with receiver mail id
		$this->email->cc('svtcollege2019@gmail.com');
		$this->email->subject($subject); // replace it with relevant subject
		$this->email->message($body);  
		$this->email->attach($excel_path.'/'.$fileName);
		$this->email->send();
	}
	
	
	public function resetleavesyearly()
	{
		// echo "<pre>";
		$today = date("m-d H:i:s"); //"01-05 00:00:01"; // eg.
		$janDate = "01-05 00:00:01";
		$mayDate = "01-05 00:00:01";
		if($today==$janDate || $today==$mayDate){
			$data = $this->Cron_model->get_all_staff_data_cron();
			$update_data1 = $update_data2 = $permanent_teaching_Staff = $permanent_non_teaching_Staff = $other_teaching_Staff = array();
			$count1 = $count2 = $count3 = 0;
			if($today==$mayDate){
			 	foreach ($data as $key => $value) {
			 		if ($value->type=='Permanent Teaching') {
				 		$permanent_teaching_Staff[]= $value;
				 		$update_data1['casual_leave_balance'] = 15;
				 		$update_data1['sick_leave_balance'] = $value->sick_leave_balance + 20;
				 		$update_data1['paid_leave_balance'] = 0;
				 		$this->Cron_model->update_leave_balance_cron($value->id, $update_data1);
				 		$count1++;
				 	}
				 }
			}
			if($today==$janDate){
				foreach ($data as $key => $value) {
				 	if ($value->type=='Permanent Non Teaching') {
				 		$permanent_non_teaching_Staff[]= $value;
				 		$update_data2['casual_leave_balance'] = 8;
				 		$update_data2['sick_leave_balance'] = $value->sick_leave_balance + 20;
				 		$update_data2['paid_leave_balance'] = $value->paid_leave_balance + 30;
				 		 $this->Cron_model->update_leave_balance_cron($value->id, $update_data2);
				 		 $count2++;
				 	}
				 	if ($value->type!='Permanent Non Teaching' && $value->type!='Permanent Teaching') {
				 		$other_teaching_Staff[]= $value;
				 		$update_data3['casual_leave_balance'] = 0;
				 		$update_data3['sick_leave_balance'] = 0;
				 		$update_data3['paid_leave_balance'] = 0;
				 		 $this->Cron_model->update_leave_balance_cron($value->id, $update_data3);
				 		 $count3++;
				 	}
				 }
			}
			echo "Permanent Teaching Staff leaves updated :".$count1."<br>Permanent Non Teaching Staff leaves updated :".$count2."<br>Other Teaching Staff leaves updated :".$count3;
			// print_r($data);
			// echo "permanent_teaching_Staff *****<br>"; print_r($permanent_teaching_Staff);
			 // echo "permanent_non_teaching_Staff ******<br>"; print_r($permanent_non_teaching_Staff);
			exit; 
		}
	}
}?>