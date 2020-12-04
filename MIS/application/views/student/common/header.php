<?php defined('BASEPATH') OR exit('No direct script access allowed');
$this->load->model('student/student');

//echo "<pre>"; print_r($course_details); echo "</pre>";
$current_db = $this->session->userdata('connectionString');
$data['student'] = $this->student->get_student_by_id($this->session->userdata("userID"));
$student_transaction = $this->student->get_transaction($this->session->userdata("userID"));
$addon_student = $this->student->get_addon_subject_by_userid($this->session->userdata("userID"));
$bannedStudent  = array(
						1111,
                        /* TY Honors */
5971,5979,5981,5984,5986,5987,5991,5992,
5997,6002,
5876,5878,5881,5882,5883,5887,5888,5890,5891,5895,5897,5898,5906,5911,5912,5913,5917,5919,5920,5923,5924,5925,5928,5933,5934,5937,5938,5941,5943,5945,5946,5948,5950,5951,5954,5957,5960,5967,
5810,5811,5815,5828,5819,5822,
5831,5836,5837,5838,5842,5843,5846,5847,5849,5850,5853,5855,5857,5860,5861,5862,5863,5864,5865,5823,5872,5870,
6011,6025,6013,6014,6018,6020,6023,
6034,6038,6039,6042,6043,6046,6048,6057,6059,6062,6099,6072,6075,6074,6081,6092,6094,

/* TY Regular 5690 */ 
5656,5970,5972,5973,5974,5975,5978,5980,5985,5988,5989,5645,
5998,5999,6000,6003,6004,6005,6007,6010,
5877,5880,5885,5886,5892,5894,5896,5899,5900,5901,5902,5903,5904,5905,5907,5908,5909,5910,5915,5916,5918,5921,5926,5929,5930,5931,5932,5935,5936,5939,5940,5942,5947,5949,5952,5953,5955,5958,5959,5961,5962,5963,5964,5966,5968,5969,
5812,5814,5818,5820,5821,5827,
5830,5833,5840,5841,5844,5845,5848,5851,5852,5854,5858,5859,5866,5867,5868,5875,
6021,6024,6026,
5783,5788,5791,6027,6030,6032,6033,6035,6036,6037,6040,6041,6044,6045,6047,6051,6052,6054,6055,6056,6063,6064,6065,6067,6068,6069,6070,6073,6076,6077,6078,6079,6082,6083,6085,6087,6091,6093,6095,6096,6097,6098,6100,6050,6058,6080,6090,5802,5893,

/* TY NRI */
5976, 5871, 6088, 

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
5994,6031,6101,6102,6105,6108,6111,6113,6114,6118,6121,6122,6123,
6124,6126,6127,6130,6138,6141,6142,6144,6145,6148,6149,6150,6151,6155,
6160,6161,6162,6164,6165,6167,6169,6170,6174,6175,6176,6179,6183,6184,
6188,6189,6192,6194,6195,6198,6201,6202,6203,6206,6207,6208,6209,6212,
6218,6220,6222,6223,6227,6228,6229,6230,6232,6234,6236,6237,6239,6240,6242,
6243,6245,6247,6248,6249,6253,6255,6256,6258,6259,6261,6262,6264,
6266,6268,6270,6273,6276,6277,6278,6282,6283,6284,6288,6289,6290,
6292,6297,6298,6299,6300,6303,6307,6308,6310,6311,6313,
6316,6317,6322,6324,6326,6328,6329,6331,6333,6336,6338,6339,
6343,6346,6347,6349,6350,6351,6353,6356,6357,6358,6360,6361,6363,6364,6086,

/* SY NRI */
6190, 6216,

5648, 5995, 5588, 5688, 5777, 5769, 5542, 5552, 5869,

/*ATKT */
6022,5826,6012,6017,6015,6016,5446, 6019, 5678, 5690,
5544, 5332, 5834, 5824, 5813,
5285,5889,5628,5721
					);
 ?>
<!DOCTYPE html>
<html lang="en">

	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta name="description" content="">
		<meta name="author" content="">

		<title>Student</title>

		<!-- Bootstrap core CSS-->
		<link href="<?php echo base_url('assets/vendor/bootstrap/css/bootstrap.min.css'); ?>" rel="stylesheet">

		<!-- Custom fonts for this template-->
		<link href="<?php echo base_url('assets/vendor/fontawesome-free/css/all.min.css'); ?>" rel="stylesheet">

		<!-- Page level plugin CSS-->
		<link href="<?php echo base_url('assets/vendor/datatables/dataTables.bootstrap4.css'); ?>" rel="stylesheet">
			
		<link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.10/css/select2.min.css" rel="stylesheet" />
		
		<link rel="stylesheet" href="<?php echo base_url('assets/css/datepicker3.css" rel="stylesheet'); ?>">
		<!-- Custom styles for this template-->
		<link href="<?php echo base_url('assets/css/sb-admin.css" rel="stylesheet'); ?>">

		<!-- Custom styles for this template-->
		<link href="<?php echo base_url('assets/css/student.css" rel="stylesheet'); ?>">
	</head>

  <body id="page-top">
	
    <nav class="navbar navbar-expand navbar-dark bg-dark static-top">	
		
		<a class="navbar-brand mr-1" href="<?php echo base_url('student/home'); ?>">
			<img src="<?php echo base_url('assets/img/logo.png');?>" height="50px"  /> - Student
		</a>

		<button class="btn btn-link btn-sm text-dark order-1 order-sm-0" id="sidebarToggle" href="#">
			<i class="fas fa-bars"></i>
		</button>

		<!-- Navbar -->		
		<ul class="navbar-nav ml-auto">
			<li class="nav-item dropdown no-arrow">
				<a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
				Welcome, <?php echo strtoupper($this->session->userdata("student_name")); ?>
					<i class="fas fa-user-circle fa-fw"></i>
				</a>
				<div class="dropdown-menu dropdown-menu-right" aria-labelledby="userDropdown">
					<a class="dropdown-item" href="<?php echo base_url("logout"); ?>">Logout</a>
				</div>
			</li>
		</ul>
    </nav>

    <div id="wrapper">

        <!-- Sidebar -->		
        <ul class="sidebar navbar-nav">
			<li class="nav-item <?php echo($this->uri->segment(2)=="home") ? 'active' : '';?>">
				<a class="nav-link"  href="<?php echo base_url('student/home'); ?>">
					<i class="fas fa-fw fa-tachometer-alt"></i>
					<span>Home</span>
				</a>
			</li>
			<?php if(in_array($data['student']['userID'], $bannedStudent)){
					//if (empty($student_transaction)) {
		  	 ?>
				<li class="nav-item <?php echo($this->uri->segment(3)=="payment") ? 'active' : '';?>">
					<a class="nav-link" href="<?php echo base_url('student/fee/payment'); ?>">
						<i class="far fa-credit-card"></i>
						<span>Fee Payment</span>
					</a>
				</li>
			<?php // } ?>
				<li class="nav-item <?php echo($this->uri->segment(3)=="history") ? 'active' : '';?>">
					<a class="nav-link" href="<?php echo base_url('student/account/history'); ?>">
						<i class="fas fa-history"></i>
						<span>Account History</span>
					</a>
				</li>
			<?php } ?>
			<li class="nav-item <?php echo($this->uri->segment(2)=="documents") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('student/documents'); ?>">
					<i class="far fa-folder-open"></i>
					<span>Documents</span>
				</a>
			</li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="feedback") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('student/feedback'); ?>">
					<i class="far fa-comments"></i>
					<span>Feedback</span>
				</a>
			</li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="performance") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('student/performance'); ?>">
					<i class="fas fa-chart-bar"></i>
					<span>Academic Performance</span>
				</a>
			</li>
			<li class="nav-item <?php echo($this->uri->segment(2)=="application_form") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('student/application_form'); ?>">
					<i class="fas fa-chart-bar"></i>
					<span>Application Form</span>
				</a>
			</li>
			<?php /*<li class="nav-item <?php echo($this->uri->segment(2)=="applications") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('student/applications'); ?>">
					<i class="far fa-calendar-check"></i>
					<span>Applications</span>
				</a>
			</li> */?>
			<!--<li class="nav-item <?php echo($this->uri->segment(1)=="miscellaneous-payment") ? 'active' : '';?>">
				<a class="nav-link" href="<?php echo base_url('miscellaneous-payment'); ?>">
					<i class="far fa-credit-card"></i>
					<span>Miscellaneous Payment</span>
				</a>
			</li>-->
			<?php if($current_db == 'clg_db2'){
				/* php if($course_details[0]->course_name == 'Honors' || $course_details[0]->course_name == 'honors'){ 
				}else{ */ 
					if(!empty($addon_student)){
					?>
					<li class="nav-item <?php echo($this->uri->segment(2)=="subject-fees-payment") ? 'active' : '';?>">
						<a class="nav-link" href="<?php echo base_url('student/subject-fees-payment'); ?>">
							<i class="far fa-credit-card"></i>
							<span>Value Added Add On Course</span>
						</a>
					</li>
				<?php } /* } */ ?>
				<li class="nav-item <?php echo($this->uri->segment(2)=="choose_elective") ? 'active' : '';?>">
					<a class="nav-link" href="<?php echo base_url('student/choose_elective'); ?>">
						<i class="far fa-credit-card"></i>
						<span>Choose Elective</span>
					</a>
				</li>
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="SY" && $data['student']['specialization'] == "Food, Nutrition and Dietitics"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=FCIV14&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>FCIV14">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Therapeutic Dietetics)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="SY" && $data['student']['specialization'] == "Food, Nutrition and Dietitics"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=FCIV13&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>FCIV13">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Functional foods and introduction to Nutrigenomics)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="SY" && $data['student']['specialization'] == "Food, Nutrition and Dietitics"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=FCIV16&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>FCIV16">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (MACRONUTRIENTS IN HEALTH AND DISEASESMACRONUTRIENTS IN HEALTH AND DISEASES)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="TY" && $data['student']['specialization'] == "Food, Nutrition and Dietitics"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=FCVI22&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>FCVI22">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Geriatric Nutrition)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="TY" && $data['student']['specialization'] == "Food, Nutrition and Dietitics"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=FCVI27&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>FCVI27">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Food Analysis)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="TY" && $data['student']['specialization'] == "Food, Nutrition and Dietitics"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=Demo&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>Demo">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Demo)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="FY"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=Demo&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>Demo">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Demo)</span>
					</a>
				</li>-->
			<?php } ?>
			<?php if($current_db == 'clg_db2' && $data['student']['course_name']=="FY"){ ?>
			    <!--<li class="nav-item">
					<a class="nav-link" target="_new" href="http://exams.vancotech.com/ui/svtexams.html?code=FG101&examid=<?php print_r($data['student']['roll_number']) ?><?php print_r($data['student']['userID']) ?>FG101">
						<i class="far fa-credit-card"></i>
						<span>Online Exam (Nutrition for Health Promotion)</span>
					</a>
				</li>-->
			<?php } ?>
        </ul>
