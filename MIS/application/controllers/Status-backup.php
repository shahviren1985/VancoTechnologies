<?php 
defined('BASEPATH') OR exit('No direct script access allowed');
class Status extends CI_Controller {
	public function __construct() {
        parent::__construct();
        $this->load->helper('url');
        $this->load->helper('select_db');    
    }
	public function index() {
    $status = $this->input->post('status');
    if (empty($status)) {
      redirect('student/fee/payment');
    }
       
    $firstname = $this->input->post('firstname');
    $amount = $this->input->post('amount');
    $txnid = $this->input->post('txnid');
    $posted_hash = $this->input->post('hash');
    $key = $this->input->post('key');
    $productinfo = $this->input->post('productinfo');
    $email = $this->input->post('email');
    $mobile = $this->input->post('phone');
    $salt = "RjWAdXh0"; //  Your salt
    $add = $this->input->post('additionalCharges');

    $udf1 = $this->input->post('udf1');
    $udf2 = $this->input->post('udf2');
    $udf3 = $this->input->post('udf3');
    // $caste = $this->input->post('caste');

    If (isset($add)) {
        $additionalCharges = $this->input->post('additionalCharges');
        $retHashSeq = $additionalCharges . '|' . $salt . '|' . $status . '|||||||||||' . $email . '|' . $firstname . '|' . $productinfo . '|' . $amount . '|' . $txnid . '|' . $key;
    } else {
        $retHashSeq = $salt . '|' . $status . '|||||||||||' . $udf3 . '|' . $udf2 . '|' . $udf1 . '|' . $email . '|' . $firstname . '|' . $productinfo . '|' . $amount . '|' . $txnid . '|' . $key;
    }

    $data['hash'] = hash("sha512", $retHashSeq);
    $data['amount'] = $amount;
    $data['txnid'] = $txnid;
    $data['posted_hash'] = $posted_hash;
    $data['status'] = $status;
    $data['name'] = $firstname;

    if($status == 'success'){

      // generate pdf
      $mpdf = new \Mpdf\Mpdf([
        'format' => 'A4',
      ]);

      $rupee_img = base_url().'assets/rupee.png';

      $stylesheet = file_get_contents(base_url().'assets/css/pdf.css');
      $mpdf->WriteHTML($stylesheet, \Mpdf\HTMLParserMode::HEADER_CSS);
      //$html = $this->load->view('receipt',[],true);
      $html = '<body><h2 align="center" style="margin-bottom: 15px;">DETAILS OF YEARLY FEES</h2><h3 align="center">SYJC ELIGIBLE(XI)</h3><table align="center" cellpadding="0" cellspacing="0" border="0" style="border:1px solid #000; border-radius: 2px; height: 3000px;"><thead><tr><th style="border-right: 1px solid #000; width: 58%; text-align: left;font-size: 12px;">Particulars</th><th style="width: 42%; font-size: 10px;">Total yearly fees to be collected <img src="'.$rupee_img.'" width="10" height="13" style="margin-top:2px;"></th></tr></thead><tbody><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Admission Fees</td><td style="font-size: 14px; width: 42%; text-align: center;">300</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Tution Fees</td><td style="font-size: 14px; width: 42%; text-align: center;">800</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Laboratory Fees</td><td style="font-size: 14px; width: 42%; text-align: center;">2000</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Gymkhana Fees</td><td style="font-size: 14px; width: 42%; text-align: center;">500</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Libaray Fees </td><td style="font-size: 14px; width: 42%; text-align: center;">600</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">SNDTWU Data Base Fees</td><td style="font-size: 14px; width: 42%; text-align: center;">400</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Development Fees</td><td style="font-size: 14px; width: 42%; text-align: center;">500</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Final Theory Exam</td><td style="font-size: 14px; width: 42%; text-align: center;">1500</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Final Practicals Exam</td><td style="font-size: 14px; width: 42%; text-align: center;">1000</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Maintenance Fee</td><td style="font-size: 14px; width: 42%; text-align: center;">1000</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Periodic Test</td><td style="font-size: 14px; width: 42%; text-align: center;">100</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Computer Fee</td><td style="font-size: 14px; width: 42%; text-align: center;">5000</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Students Aid</td><td style="font-size: 14px; width: 42%; text-align: center;">100</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Identity Card</td><td style="font-size: 14px; width: 42%; text-align: center;">150</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">E Service Fees ( U )</td><td style="font-size: 14px; width: 42%; text-align: center;">50</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Medical Check Up ( U )</td><td style="font-size: 14px; width: 42%; text-align: center;">35</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Students Welfare ( U )</td><td style="font-size: 14px; width: 42%; text-align: center;">75</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Sports Fees ( U )</td><td style="font-size: 14px; width: 42%; text-align: center;">100</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Ashwamedh  ( U )</td><td style="font-size: 14px; width: 42%; text-align: center;">30</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Convocation Fees ( U )</td><td style="font-size: 14px; width: 42%; text-align: center;">0</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Syllabus</td><td style="font-size: 14px; width: 42%; text-align: center;">270</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Caution Money Deposit</td><td style="font-size: 14px; width: 42%; text-align: center;">100</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Laboratory Deposit</td><td style="font-size: 14px; width: 42%; text-align: center;">300</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Library Deposit</td><td style="font-size: 14px; width: 42%; text-align: center;">1500</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Insurance Fee</td><td style="font-size: 14px; width: 42%; text-align: center;">22</td></tr><tr><td style="width: 58%; font-size: 14px; border-right: 1px solid #000;">Generic Elective Courses</td><td style="font-size: 14px; width: 42%; text-align: center;">2000</td></tr><tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000; border-top: 1px solid #000;"><b>Total</b></td><td style="font-size: 14px; width: 42%; border-top: 1px solid #000; text-align: center;"><b>19,972</b></td></tr></tbody></table>';

      $html .= '<table align="center" cellpadding="0" cellspacing="0" border="0" style="margin-top:30px; border:1px solid #000; border-radius: 2px; height: 3000px;">';
      $html .= '<tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000;">Name : </td><td style="font-size: 14px; width: 42%; text-align: center;">'.$firstname.'</td></tr>';
      $html .= '<tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000; border-top: 1px solid #000;">Email : </td><td style="font-size: 14px; width: 42%; border-top: 1px solid #000; text-align: center;">'.$email.'</td></tr>';
      $html .= '<tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000; border-top: 1px solid #000;">Mobile No. : </td><td style="font-size: 14px; width: 42%; border-top: 1px solid #000; text-align: center;">'.$mobile.'</td></tr>';
      $html .= '<tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000; border-top: 1px solid #000;">Fee Type : </td><td style="font-size: 14px; width: 42%; border-top: 1px solid #000; text-align: center;">'.$udf1.'</td></tr>';
      $html .= '<tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000; border-top: 1px solid #000;">Course : </td><td style="font-size: 14px; width: 42%; border-top: 1px solid #000; text-align: center;">'.$udf2.'</td></tr>';
      $html .= '<tr><td style="width: 58%; text-align: center; font-size: 14px; border-right: 1px solid #000; border-top: 1px solid #000;">Year : </td><td style="font-size: 14px; width: 42%; border-top: 1px solid #000; text-align: center;">'.$udf3.'</td></tr>';
      $html .= '</table>';
      $html .= '</body>';
      //$mpdf->SetWatermarkText('SVT MIS');
      //$mpdf->showWatermarkText = true;
      $mpdf->WriteHTML($html);
      $mpdf->Output(); // opens in browser

      //$content = $mpdf->Output('', 'S'); // Saving pdf to attach to email 
      //$content = chunk_split(base64_encode($content));

      //Email settings
      // $mailto = $email;
      // $from_name = 'SVT MIS';
      // $from_mail = 'apncoders@gmail.com';
      // $replyto = 'apncoders@gmail.com';
      // $uid = md5(uniqid(time())); 
      // $subject = 'Transaction Details';
      // $message = 'Download the attached pdf';
      // $filename = 'transaction_receipt.pdf';
      // $header = "From: ".$from_name." <".$from_mail.">\r\n";
      // $header .= "Reply-To: ".$replyto."\r\n";
      // $header .= "MIME-Version: 1.0\r\n";
      // $header .= "Content-Type: multipart/mixed; boundary=\"".$uid."\"\r\n\r\n";
      // $header .= "This is a multi-part message in MIME format.\r\n";
      // $header .= "--".$uid."\r\n";
      // $header .= "Content-type:text/plain; charset=iso-8859-1\r\n";
      // $header .= "Content-Transfer-Encoding: 7bit\r\n\r\n";
      // $header .= $message."\r\n\r\n";
      // $header .= "--".$uid."\r\n";
      // $header .= "Content-Type: application/pdf; name=\"".$filename."\"\r\n";
      // $header .= "Content-Transfer-Encoding: base64\r\n";
      // $header .= "Content-Disposition: attachment; filename=\"".$filename."\"\r\n\r\n";
      // $header .= $content."\r\n\r\n";
      // $header .= "--".$uid."--";
      // $is_sent = @mail($mailto, $subject, $message, $header);

      // redirect to success page
      $this->load->view('success', $data);

      // $this->load->library('email');
      // $from_email = 'apncoders@gmail.com';
      // $this->email->from($from_email, 'APN ADMIN'); 
      // $this->email->to('amansaxenaapn@gmail.com');
      // $this->email->subject('TRANSACTION SUCCESS'); 
      // $this->email->message('Your TRANSACTION has been SUCCESSfull.');
      // $this->email->send();
    }
    else{
      $this->load->view('failure', $data); 
    }
     
  }
 
}