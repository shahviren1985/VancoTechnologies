<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Fee_payment extends CI_Controller {

	public function __construct() {
		parent::__construct();
		if( $this->session->userdata("logged_in") == null || $this->session->userdata("id") == null || $this->session->userdata("role") != '3') {
			redirect(site_url(), 'refresh');
        }
        $this->load->helper('url');    
		$this->load->model('student/student');
		$this->load->model('student/course_detail');
		$this->load->model('student/fee_detail');
        $this->load->model('office_admin/search_model');
	}

	public function index() {
		$userID = $this->session->userdata("userID");
		$data['student_details'] = $this->student->get_entry( $userID );
		$course_id = $data['student_details'][0]->course_id;
		$data['course_details'] = $this->course_detail->get_entry( $course_id );
		$data['fee_details'] = $this->fee_detail->get_entry( $course_id );
        $student_transaction = $this->student->get_transaction($userID);
        $bannedStudent  = array(1111,
                        /* TY Honors */
5971,5979,5981,5984,5986,5987,5991,5992,
5997,6002,
5876,5878,5881,5882,5883,5887,5888,5890,5891,5895,5897,5898,5906,5911,5912,5913,5917,5919,5920,5923,5924,5925,5928,5933,5934,5937,5938,5941,5943,5945,5946,5948,5950,5951,5954,5957,5960,5967,
5810,5811,5815,5828,5819,5822,
5831,5836,5837,5838,5842,5843,5846,5847,5849,5850,5853,5855,5857,5860,5861,5862,5863,5864,5865,5823,5872,5870,
6011,6025,6013,6014,6018,6020,6023,
6034,6038,6039,6042,6043,6046,6048,6057,6059,6062,6099,6072,6075,6074,6081,6092,6094,

/* TY Regular 5690 */ 
5656, 5970,5972,5973,5974,5975,5978,5980,5985,5988,5989,5645, 
5998,5999,6000,6003,6004,6005,6007,6010,
5877,5880,5885,5886,5892,5894,5896,5899,5900,5901,5902,5903,5904,5905,5907,5908,5909,5910,5915,5916,5918,5921,5926,5929,5930,5931,5932,5935,5936,5939,5940,5942,5947,5949,5952,5953,5955,5958,5959,5961,5962,5963,5964,5966,5968,5969,
5812,5814,5818,5820,5821,5827,
5830,5833,5840,5841,5844,5845,5848,5851,5852,5854,5858,5859,5866,5867,5868,5875,
6021,6024,6026,
5783,5788,5791,6027,6030,6032,6033,6035,6036,6037,6040,6041,6044,6045,6047,6051,6052,6054,6055,6056,6063,6064,6065,6067,6068,6069,6070,6073,6076,6077,6078,6079,6082,6083,6085,6087,6091,6093,6095,6096,6097,6098,6100,6050,6058,6080,6090,5802,5893,6088, // NRI

/* TY NRI */
5976, 5871,  

/* SY Honors */
6103,6109,6110,6112,6115,6120,6128,6129,6131,6132,6133,6134,6135,6136,6137,
6139,6140,6146,6147,6152,6153,6154,6156,6158,6159,6163,6166,6171,6172,6173,
6177,6178,6180,6181,6185,6186,6187,6191,6193,6196,6197,6199,6200,6204,
6205,6210,6211,6213,6214,6217,6219,6221,6224,6225,6226,6231,6233,6235,
6238,6241,6244,6246,6251,6254,6257,6260,6263,6265,6267,6269,6271,6272,6274,
6275,6279,6280,6281,6286,6293,6295,6296,6301,6305,6306,6309,6312,6314,6315,
6319,6321,6323,6325,6327,6330,6332,6334,6335,6340,6342,6345,6352,6354,6355,
6365,

/* SY Regular */
/*5825,6143,6168,6215,6250,6362,6089,6348,6341,6337,6320,6304,6302,6291,6294,6287,6285,6252*/
6031,6101,6102,6105,6108,6111,6113,6114,6118,6121,6122,6123,
6124,6126,6127,6130,6138,6141,6142,6144,6145,6148,6149,6150,6151,6155,
6160,6161,6162,6164,6165,6167,6169,6170,6174,6175,6176,6179,6183,6184,
6188,6189,6192,6194,6195,6198,6201,6202,6203,6206,6207,6208,6209,6212,
6218,6220,6222,6223,6227,6228,6229,6230,6232,6234,6236,6237,6239,6240,6242,
6243,6245,6247,6248,6249,6253,6255,6256,6258,6259,6261,6262,6264,
6266,6268,6270,6273,6276,6277,6278,6282,6283,6284,6288,6289,6290,
6292,6297,6298,6299,6300,6303,6307,6308,6310,6311,6313,
6316,6317,6322,6324,6326,6328,6329,6331,6333,6336,6338,6339,
6343,6346,6347,6349,6350,6351,6353,6356,6357,6358,6360,6361,6363,6364,5994,6086,

/* SY NRI */
6190, 6216,

/* Added Later */ 
5648, 5995, 5588, 5688, 5777, 5769, 5542, 5552, 5869,

/*ATKT */
6022,5826,6012,6017,6015,6016,5446, 6019, 5678, 5690,
5544, 5332, 5834, 5824, 5813,
5285,5889,5628,5721

                    );

         $partialStudent  = array(1111,6345,6080,6358,6099,6057,6297,6007,5552,6102,
         5815,5811,5937,6271,6018,6023,6173,6154,6172,5844,5888,6161,6146,5842,5994,
         6088, 6019 /* NRI partial payment */
         );
         if(!in_array($data['student_details'][0]->userID, $bannedStudent)){
            $this->session->set_flashdata('msg', 'Not Authorized to access this payment...');
            redirect(site_url('student/home'));
           
        }
        if(in_array($data['student_details'][0]->userID, $bannedStudent)){
           /* if (!empty($student_transaction)) {
                $this->session->set_flashdata('msg', 'Have you already paid fees for this academic year? Please contact Admissions Team at svtcollege2019@gmail.com');
                redirect(site_url('student/home'));
            }*/
        }
		//print("<pre>".print_r($data,true)."</pre>"); die;
		$this->load->view('student/common/header', $data);

        // CONDITION FOR PARTIAL PAYMENT STudents
        if(in_array($data['student_details'][0]->userID, $partialStudent)){
            $this->load->view('student/fee_payment_partial', $data);
        }else{
            $this->load->view('student/fee_payment', $data);
        }
        
		$this->load->view('student/common/footer');
	}

    public function check() {
        $fee_head_total_with_comma = $this->input->post('fee_head_total');
        $fee_head_total = str_replace(',', '', $fee_head_total_with_comma);
        $amount_with_comma = $this->input->post('amount');
        $amount = str_replace(',', '', $amount_with_comma);  
        $commission_with_comma = $this->input->post('commission');
        $commission = str_replace(',', '', $commission_with_comma); 
        $late_fee = $this->input->post('late_fee');
        if (empty($late_fee)) {
            $late_fee = '';
        }
        // echo "<pre>";print_r($_POST); die;
            $connectionString = $this->session->userdata("connectionString");
            $userID = $this->session->userdata("userID");
            if($connectionString == 'clg_db1'){

                // ==== LIVE KEYS ====
                $firstSplitArr = array("name"=>$userID, "value"=>$fee_head_total, "merchantId"=>"6891255", "description"=>"payment splitting", "commission"=>$commission); // LIVE ACCOUNT
                $MERCHANT_KEY = "qk9XqxqH"; // LIVE ACCOUNT
                $SALT = "fZGlLKDEO9"; // LIVE ACCOUNT*/
                

                /*// ==== TEST KEYS ====
                $firstSplitArr = array("name"=>$userID, "value"=>$fee_head_total, "merchantId"=>"4825051", "description"=>"payment splitting", "commission"=>$commission); // TEST ACCOUNT
                $MERCHANT_KEY = "BC50nb"; // TEST ACCOUNT
                $SALT = "Bwxo1cPe"; // TEST ACCOUNT*/

           } elseif($connectionString == 'clg_db2'){

                // ==== LIVE KEYS ====
                $firstSplitArr = array("name"=>$userID, "value"=>$fee_head_total, "merchantId"=>"6888893", "description"=>"payment splitting", "commission"=>$commission); // LIVE ACCOUNT
                $MERCHANT_KEY = "qk9XqxqH"; // LIVE ACCOUNT
                $SALT = "fZGlLKDEO9"; // LIVE ACCOUNT*/
                

               // ==== TEST KEYS ====
               /*$firstSplitArr = array("name"=>$userID, "value"=>$fee_head_total, "merchantId"=>"4825050", "description"=>"payment splitting", "commission"=>$commission); // TEST ACCOUNT
                $MERCHANT_KEY = "BC50nb"; // TEST ACCOUNT
                $SALT = "Bwxo1cPe"; // TEST ACCOUNT*/
            }



		//$firstSplitArr = array("name"=>"splitID1", "value"=>$fee_head_total, "merchantId"=>"4825050", "description"=>"payment splitting", "commission"=>$commission);
        //$firstSplitArr = array("name"=>"splitID1", "value"=>$fee_head_total, "merchantId"=>"6888893", "description"=>"payment splitting", "commission"=>$commission);
        // payu child merchant id - 4825050

        $paymentPartsArr = array($firstSplitArr);   
        $finalInputArr = array("paymentParts" => $paymentPartsArr);
        $Prod_info = json_encode($finalInputArr);

        // Merchant key here as provided by Payu
/*
        $connectionString = $this->session->userdata("connectionString");
        if($connectionString == 'clg_db1'){
            $MERCHANT_KEY = "BC50nb";
            $SALT = "Bwxo1cPe";
        }else if($connectionString == 'clg_db2'){
            $MERCHANT_KEY = "BC50nb";
            $SALT = "Bwxo1cPe";
        }else{
            $MERCHANT_KEY = "BC50nb";
            $SALT = "Bwxo1cPe";
        }
*/

        //$MERCHANT_KEY = "qk9XqxqH";
        //$SALT = "fZGlLKDEO9";
        // End point - change to https://secure.payu.in for LIVE mode
        //$PAYU_BASE_URL = "https://test.payu.in";
        $PAYU_BASE_URL = "https://secure.payu.in";
        $txnid = substr(hash('sha256', mt_rand() . microtime()), 0, 20);
        $action = '';

        $productinfo = $Prod_info;
        $firstname = $this->input->post('firstname');
		$lastname = '';
        $email = $this->input->post('email');
        $phone = $this->input->post('mobile');
        $udf1 = $fee_head_total;
        $udf2 = $late_fee;
        $udf3 = '';
        $udf4 = '';
        $udf5 = '';

        $hashstring = $MERCHANT_KEY . '|' . $txnid . '|' . $amount . '|' . $productinfo . '|' . $firstname . '|' . $email . '|' . $udf1 . '|' . $udf2 . '|' . $udf3 . '|' . $udf4 . '|' . $udf5 . '||||||' . $SALT;
		
		$hash = strtolower(hash('sha512', $hashstring));
        $action = $PAYU_BASE_URL . '/_payment';

        $success = base_url() . 'Status';  
        $fail = base_url() . 'Status';
        $cancel = base_url() . 'Status';

        $data = array(
            'MERCHANT_KEY' =>   $MERCHANT_KEY,
            'txnid' =>          $txnid,
            'hash' =>           $hash,
            'amount' =>         $amount,
            'productinfo' =>    $productinfo,
            'firstname' =>      $firstname,
			'lastname' =>      	$lastname,
            'email' =>          $email,
            'phone' =>          $phone,
            'salt' =>           $SALT,
            'udf1' =>           $udf1,
            'udf2' =>           $udf2,
            'udf3' =>           $udf3,
            'udf4' =>           $udf4,
            'udf5' =>           $udf5,
            'action' =>         $action,
            'success' =>        $success,
            'failure' =>        $fail,
            'cancel' =>         $cancel
        );
		
		$fp = fopen('./uploads/logs/'.$txnid.'.json', 'w');
        fwrite($fp, json_encode($data));
		
		$this->load->view('confirmation', $data);
    }
}