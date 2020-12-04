<?php  defined('BASEPATH') OR exit('No direct script access allowed');

class Status extends CI_Controller {

	public function __construct() {
        parent::__construct();
        $this->load->helper('url');
        $this->load->helper('select_db');    
    }
	public function index() {
        $status = $this->input->post('status');
        // if (empty($status)) {
        //   redirect('student/fee/payment');
        // }
           
        $firstname = $this->input->post('firstname');
        $amount = $this->input->post('amount');
        $txnid = $this->input->post('txnid');
        $posted_hash = $this->input->post('hash');
        $key = $this->input->post('key');
        $productinfo = $this->input->post('productinfo');
        $email = $this->input->post('email');
        $mobile = $this->input->post('phone');
        $salt = "Bwxo1cPe"; //  Your salt - RjWAdXh0,e5iIg1jwi8
        $add = $this->input->post('additionalCharges');

        $udf1 = $this->input->post('udf1'); // fee type
        $udf2 = $this->input->post('udf2'); // course
        $udf3 = $this->input->post('udf3'); //  year
        $udf4 = $this->input->post('udf4'); // category
        $udf5 = $this->input->post('udf5'); // course_id

        If (isset($add)) {
            $additionalCharges = $this->input->post('additionalCharges');
            $retHashSeq = $additionalCharges . '|' . $salt . '|' . $status . '|||||||||||' . $email . '|' . $firstname . '|' . $productinfo . '|' . $amount . '|' . $txnid . '|' . $key;
        } else {
            $retHashSeq = $salt . '|' . $status . '|||||||||||' . $udf5 . '|' . $udf4 . '|' . $udf3 . '|' . $udf2 . '|' . $udf1 . '|' . $email . '|' . $firstname . '|' . $productinfo . '|' . $amount . '|' . $txnid . '|' . $key;
        }

        $data['key'] = $key;
        $data['hash'] = hash("sha512", $retHashSeq);
        $data['amount'] = $amount;
        $data['txnid'] = $txnid;
        $data['posted_hash'] = $posted_hash;
        $data['status'] = $status;
        $data['firstname'] = $firstname;
        $data['email'] = $email;
        $data['productinfo'] = $productinfo;
        $data['udf1'] = $udf1;
        $data['udf2'] = $udf2;
        $data['udf3'] = $udf3;
        $data['udf4'] = $udf4;
        $data['udf5'] = $udf5;
        $data['category'] = $udf4;
        $data['course_id'] = $udf5;
        

        if($status == 'success'){

          $message = '';
          $message .= '<table style="width:30%;">
                      <tr><td>Name: </td><td>'.$firstname.'</td></tr>
                      <tr><td>Email: </td><td>'.$email.'</td></tr>
                      <tr><td>Mobile No.: </td><td>'.$mobile.'</td></tr>
                      <tr><td>Fee Type: </td><td>'.$udf1.'</td></tr>
                      <tr><td>Year/Course: </td><td>'.$udf3."/".$udf2.'</td></tr>
                      <tr><td>Grand Total: </td><td>'.$amount.'</td></tr>
                      </table>
                      <div style="height:20px;"></div>';

          $message .= test($data);

          $this->load->library('email');

          $config['mailtype'] = 'html';
          $this->email->initialize($config);
          $this->email->from($email, $firstname);
          $this->email->to('amansaxenaapn@gmail.com');
          $this->email->subject('Payment Transaction');
          $this->email->message($message);

          //$this->email->send();

          // redirect to success page
          $this->load->view('student/common/header');
          $this->load->view('Success', $data);
          $this->load->view('student/common/footer');
        }
        else{
          $this->load->view('student/common/header');
          $this->load->view('failure', $data);
          $this->load->view('student/common/footer');
        }
     
    }
 
}