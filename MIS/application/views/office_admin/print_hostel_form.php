<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Hostel Accomodation Form</title>
    </head>
    <style>
    	.content-wrapper{
    		align-items: center;
    		text-align: center;
    	}
		table{
			text-transform: uppercase;
		}
		body{
			font-size: 1em;
			line-height:1.375em;
		}
		.data_table1{
			width: 100%;
		}
		.data_table1 tr th, .data_table1 tr td{
			border: 1px solid #555;
		}
		.data_table1 tr:nth-child(1) th{
			border-top: 2px solid #555;
		}
		.data_table1 tr:nth-last-child(1) td{
			border-left: 2px solid #555;
			border-bottom: 2px solid #555;
		}

    </style>
    <body>
	<?php
	  	function file_get_contents_by_curl($url) {
		    $arrContextOptions=array(
		                            "ssl"=>array(
		                                'verify_peer' => false,
		                                'verify_peer_name' => false
		                            ),
		                        );
		    $response = file_get_contents($url, false, stream_context_create($arrContextOptions));
		    return $response;
		}
		$logo_path = base_url().'assets/img/vedanta_logo.png';
		$logoData = base64_encode(file_get_contents_by_curl($logo_path));
		$logo = 'data:image/'.pathinfo($logo_path, PATHINFO_EXTENSION).';base64,'.$logoData;

    ?>
		<table cellpadding="5" cellspacing ="2" style="text-align:center;width:100%; border:3px solid black; padding: -5px">
			<tr>
				<td>
					<table style="text-align:center;width:100%; border:1px solid black;">
						<tr>
							<td colspan="2">
								<br><br>
								<img src="<?php echo $logo; ?>" style="height: 120px">
								<br><br><br><br>
								<h1><u>Vedantaa Institute of Medical Sciences</u></h1>
								<h1>Vedantaa Hospital & Research Centre</h1>
								<h3>(Unit of: Vedantaa Institutes of Academic Excellence Pvt. Ltd.)</h3><br>
								<p>Village: Saswand, Post: Dhundalwadi, Tal –Dahanu, Dist: Palghar – 401606. Ph.:02528-266200</p>
								<p>Website : <a href="www.vedantaa.institute">www.vedantaa.institute</a>, E-mail : info@vedantaa.institute</p>
							</td>
						</tr>
						<tr>
							<td colspan="2">
								<br>
								<h2 style="font-size: 22px; font-weight: normal;">APPLICATION FORM <br> FOR HOSTEL ACCOMMODATION</h2>
								<br>
								<h2 style="font-size: 15px; font-weight: normal;">ACADEMIC YEAR<br> 2019-2020</h2>
							</td>
						</tr>
						<br><br>
					</table>
				</td>
			</tr>
		</table>



		<table cellpadding="5" cellspacing ="2" style="text-align:center;width:100%; border:3px solid black; padding: -5px">
			<tbody>
				<tr>
					<th colspan="2" style="padding: 20px 15px 8px;"><h1 style="font-size: 20px; font-weight: 500; text-align: center;">APPLICATION FORM FOR HOSTEL ACCOMMODATION</h1></th>
				</tr>
				<tr>
					<td colspan="2">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td><img src="<?php echo $logo; ?>" style="height: 100px"></td>
								<td>
									<h5 style="font-size: 18px; display: inline-block; border-bottom: 1px solid #555; padding-bottom: 5px;">Vedantaa Institute of MedicalSciences</h5>
									<h5 style="font-size: 18px; display: inline-block; border-bottom: 1px solid #555; padding-bottom: 5px;">Vedantaa Hospital & Research Centre</h5><br>
									<h6 style="font-size: 14px;">Village: Saswand, Post: Dhundalwadi, Tal.:Dahanu, Dist.: Palghar – 401606, Ph-02528-266200</h6>
									<p style="font-size: 12px; font-weight: 400;">Website <a href="#"> :www.vedantaa.institute </a>, E-mail : info@vedantaa.instittute</p><br><br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 0px 15px; border: 1px dashed #000; height: 1px;"></td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px;">
						<br>
						<div><h3 style="display: inline-block;font-size: 16px; font-weight: normal; background-color: yellow; color: #000;padding: 5px 10px; border: 1px solid #000;">NEW /RENEWAL</h3></div>
						<br>
					</td>
				</tr>
				<tr>
					<td style="padding: 10px 15px; width: 50%; text-align: left;">
						<h4 style="font-size: 16px; font-weight: normal;">ACADEMIC YEAR <span style="display: inline-block; border: 1px solid #555; padding: 5px 10px;">2019-20</span></h4>
					</td>
					<td style="padding: 10px 15px; width: 50%; text-align: left;">
						<h4 style="font-size: 16px; font-weight: normal;">Room No. <span style="display: inline-block; padding: 5px 20px; border: 1px solid #000;">2</span></h4>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; width: 50%; text-align: left;">
						<h4 style="font-size: 16px; font-weight: normal;">TYPE OF HOSTEL APPLIED: <span style="display: inline-block; padding: 5px 20px 10px; border-bottom: 1px solid #000;">Boys</span></h4>
					</td>
					<td style="padding: 10px 15px; width: 50%; text-align: right;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="width: 100px; padding: 5px; height: 100px; border: 1px solid #000; text-align: center;">Affix your passport size photograph here.</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<div><h3 style="display: inline-block;font-size: 16px;  border-bottom: 2px solid #000;">NOTE:</h3></div>
						<br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px;"><span style="display: inline-block; font-size: 15px; font-weight: 600;"> 1) </span>The accommodation inhostel is limited and will be allotted subject to availability and according to the appropriate criteria decided by the college authority. This application thus does not guarantee Hostel Accommodation.</h4><br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px;"><span style="display: inline-block; font-size: 15px; font-weight: 600;"> 2) </span> In case you have failed or not appeared in the Examination during the last academic year, then you are not eligible for hostel accommodation during upcoming academic year. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px;"><span style="display: inline-block; font-size: 15px; font-weight: 600;"> 3) </span>	The students who have not paid hostel charges of previous academic year in-time and/or the students who have failed to comply with the rules &regulations of hostel during last academic year, may not be given hostel accommodation in this academic year. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;"><span style="display: inline-block; font-size: 16px; font-weight: 600;"> 4) </span>	Please write in CAPITAL LETTERS only & tick √ wherever applicable.  </h4>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="">
									<table cellpadding="0" cellspacing ="0">
										<tr>
											<td><h4 style="font-weight: normal; font-size: 14px;">Name of the Candidate <br>(In BLOCK LETTERS) &nbsp; &nbsp;</h4></td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
											<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Date of Birth: DD  &nbsp; &nbsp; &nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="font-size: 15px;">&nbsp; &nbsp; MM &nbsp; &nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="font-size: 15px;">&nbsp; &nbsp; YEAR &nbsp; &nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="border: 1px solid #555; padding: 5px; width: 22px; height: 22px;">&nbsp;</td>
								<td style="font-size: 15px;">&nbsp; &nbsp; &nbsp; Single/Married: &nbsp;</td>
								<td style="padding: 5px; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Age:  &nbsp;</td>
								<td style="padding: 5px; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; years:  &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">Male/Female:  &nbsp;</td>
								<td style="padding: 5px; border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">Blood Group: &nbsp;</td>
								<td style="padding: 5px; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; </td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 50%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Course:  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; years &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 50%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Year of Study:  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 50%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Date of Admission:  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;"> &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 50%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">G.R. Number:  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 50%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Contact No. of the Candidate: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">years</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 50%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">E-mail: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Permanent Address (Home):  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Name of Parent/Guardian: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Occupation: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Address of Business/Service: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Tel. No.: (Res.) &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; (Off.) &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; (Mob.) &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; Fax: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Email: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Name of the Local Guardian (Optional): &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Address of the Local Guardian: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Tel: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; Fax: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; Email &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Address for Correspondence:  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Tel: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; Fax: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; Email &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Whether the candidate has stayed in hostel before: &nbsp; Yes / No</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Whether the candidate has any medical history of ailments: &nbsp; Yes / No.</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">(If yes, please state briefly and attach medical certificate):  &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Details of accommodation in the College Hostel (since last three years):</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table class="data_table1" cellpadding="0" cellspacing ="0">
							<tr>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">Year</th>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">Name of the Hostel</th>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">Room No.</th>
							</tr>
							<tr>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
							</tr>
							<tr>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
							</tr>
							<tr>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px; padding: 20px 0px;">If you were punished for misconduct/ violation of Hostel rules/indiscipline etc. give particulars</td>
							</tr>
							<tr>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table class="data_table1" cellpadding="0" cellspacing ="0">
							<tr>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">Last examination passed </th>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">University/ Board </th>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">Year</th>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">Roll No.</th>
								<th style="font-size: 15px; font-weight: 600; padding: 10px;">% of Marks obtd</th>
							</tr>
							<tr>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
							</tr>
							<tr>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
								<td style="font-size: 14px; padding: 10px;">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px; width: 35%;">Any Vehicle? Motorcycle/Scooter /Moped/Car/Cycle</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Make/Model of the Vehicle:  &nbsp; </td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">&nbsp; Vehicle Reg. No: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;">
						<div><h3 style="display: inline-block;font-size: 14px;  border-bottom: 2px solid #000;">DECLARATION BY THE STUDENTS</h3></div>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
								<td style="font-size: 15px;">have read the rules and regulations for my admission into hostel accommodation facilities of Vedantaa Institute of Medical Sciences, Palghar. </td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px;">I agree not to indulge in groupism of any kind and would live in harmony with others in the hostel. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px;">I understand that smoking and consumption of alcohol, drugs and other objectionable material in the hostel is strictly prohibited and will abstain from such acts. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px;">I understand that indulgence in any anti-institutional or anti-social activity in the hostel is highly punishable and will be liable for severe penalties and punishments for indulging in such acts. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;"> I declare that I am physically and medically fit to live in the hostel. I also declare that every information about my being medically/ psychologically unfit, if so, to any degree or manner, has been brought to the information of the college authorities at the time of applying for hostel accommodation. I will not hold the management, college authorities, or the hostel authorities responsible for any consequence which will be a result of my non-disclosure. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;"> I undertake to conduct myself as a diligent student within the hostel and in the vicinity and not misbehave in any manner including using inappropriate language, physical tiffs and fights with other inmates /employees/ and others in the hostel’s neighborhood. </h4><br>

						<h4 style="font-weight: normal; font-size: 14px;"> I will abide by the rules that prohibit use of mobile phones time to time, if any.  </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;"> I agree not to cook, not to use electric and electronic gadgets, not use washing machine and nor iron clothes in the hostel rooms. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;"> I will not cause any damage whatsoever, including defacing the property of the hostel and understand that I will be liable for penalties and punishments for doing so.  </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;">I accept to stay within the hostel premises during the stipulated time and will not stay out without proper prior permission from concerned authorities. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;">I undertake to abide by all the rules that govern my stay in the hostel and also all changes to the rules that may be made, from time to time. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;">I will not damage any furniture or appearance of room and will agree to pay the fine, if done so.  </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;">Finally, I agree to abide by all the rules and regulations of the institution with regard to hostel stay, which may be framed from time to time and accept the decision of management, in all respects, as final and binding on me for compliance. </h4><br>
						<h4 style="font-weight: normal; font-size: 14px;">I will not indulge in any behavior or act that may come under the definition of ragging. </h4>
						<h4 style="font-weight: normal; font-size: 14px;">I will not participate in or abet or propagate ragging in any form.</h4>
						<h4 style="font-weight: normal; font-size: 14px;">I will not hurt anyone physically or psychologically or cause any other harm. </h4>
						<h4 style="font-weight: normal; font-size: 14px;">I hereby agree that if found guilty of any aspect of ragging, I may be punished as per the provisions of the MCI Regulations and /or as per the law in force. </h4>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Place: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
						<table cellpadding="0" cellspacing ="0" style="padding-top: 15px;">
							<tr>
								<td style="font-size: 15px;">Date: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;">
						<div><h3 style="display: inline-block;font-size: 14px;  border-bottom: 2px solid #000;">DECLARATION BY PARENT/GUARDIAN</h3></div>
						<br><br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px; text-align: left;">I assure that my ward will abide by the rules and regulations of the Hostel. I have no objection if any action is taken against my ward, as per the rules.  </h4>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Place: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
						<table cellpadding="0" cellspacing ="0" style="padding-top: 15px;">
							<tr>
								<td style="font-size: 15px;">Date: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of the Parent/Guardian</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 20px 15px 8px; font-size: 16px; font-weight: bold;"><h1 style="font-size: 18px; font-weight: 500; text-align: center;">FOR RENEWAL OF HOSTEL ADMISSION</h1></td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;">
						<div><h3 style="display: inline-block;font-size: 14px; border-bottom: 2px solid #000;">NO DUES CERTIFICATE</h3></div>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">This is to certify that Mr. /Ms <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span>who was staying in Room No <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> in a <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> accommodation of <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> Hostel during the academic year 201-201has, </td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">(a) Cleared all his/ her dues and there are no dues pending against him/her.</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">(b) Not cleared his /her dues and has to pay Rs <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span>(Rupees)<span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> (towards) <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> (mention the particulars). </td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Date: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of the Authority with stamp</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px; text-align: left;">(Note: Above dues include hostel, mess, laundry and other associated charges such as breakage etc) </h4>
						========================================================================================================================================
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;"><br><br><br>
						<div><h3 style="display: inline-block;font-size: 14px;  border-bottom: 2px solid #000;">RECOMMENDATION OF THE HOSTEL ADMISSION AUTHORITY</h3></div>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;">
						<br><br>
						<h4 style="font-weight: normal; font-size: 14px; padding-bottom: 5px; text-align: left;">The particulars of the above applicant have been checked and the applicant may be recommended /not recommended for Hostel Admission.</h4>
					</td>
				</tr>


				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Place: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of the Authority with stamp</td>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;"><br><br><br>
						<div><h3 style="display: inline-block;font-size: 14px;  border-bottom: 2px solid #000;">HOSTEL ALLOTMENT LETTER</h3></div><br>
						<div><h3 style="display: inline-block;font-size: 14px;">(OFFICE COPY)</h3></div>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Name of the student <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> G.R. No. <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Name of Programme:  <span style="display: inline-block; border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; </span> Year of Programme: <span style="display: inline-block; border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; </span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Type of Hostel: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span>Room No.: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Roommate’s Name:<span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span>Name & year ofRoommate’sProgramme:<span style="display: inline-block; border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; </span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Room handed over with the following furniture: <span style="display: inline-block;border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Admitted to Hostel on:  <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Amount Paid: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Receipt No.: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Place: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Authority</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: center;"><br><br><br>
						<div><h3 style="display: inline-block;font-size: 14px;  border-bottom: 2px solid #000;">HOSTEL ALLOTMENT LETTER</h3></div><br>
						<div><h3 style="display: inline-block;font-size: 14px;">(OFFICE COPY)</h3></div>
					</td>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Name of the student <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> G.R. No.   <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Name of Programme:  <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;.</span> Year of Programme: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Type of Hostel: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span>Room No.: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;.</span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Roommate’s Name:<span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span>Name & year ofRoommate’sProgramme:<span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span> </td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Room handed over with the following furniture: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Admitted to Hostel on:  <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Amount Paid: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
							<tr>
								<td style="font-size: 15px;">Receipt No.: <span style="display: inline-block; border-bottom: 1px solid #000;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</span></td>
								<br><br><br>
							</tr>
						</table>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Place: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Authority</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;"><br><br><br>
						<div><h3 style="display: inline-block;font-size: 16px;  border-bottom: 2px solid #000;">RULES AND REGULATIONS FOR HOSTEL ACCOMMODATION AT VEDANTAA INSTITUTE OF MEDICAL SCIENCES.</h3></div><br><br><br>
						<h4 style="font-size: 15px; text-transform: uppercase;">The rules and regulations of the hostel accommodation will come into effect from the academic year 2019-20 and onwards. And they will be in force until further orders of the VIMS authorities. Students & Residents who have availed hostel facility have to follow said rules and rules amended from time to time and other rules of VIMS framed in past, as well, and which are in force. These rules are applicable to all the inmates of hostel.</h4><br><br><br>

						<h4 style="font-size: 15px; text-transform: uppercase;">A. ACCOMMODATION</h4><br><br><br>
						<p style="font-size: 15px;">1. Hostel accommodation shall be provided only to registeredstudents of the Institute.</p><br><br>
						<p style="font-size: 15px;">2. At the time of admission of a student into the hostel, each student/ resident is required to submit a duly completed Application Form. The telephone number(s) of parents must be provided. Local Guardian’s address and phone number is optional. Email address of the parent (if available) should also be provided. Any change in address / telephone number of the parent / local guardian, at any point of time, has to be intimated immediately to the Administrative Officer, in writing.</p><br><br>
						<p style="font-size: 15px;">3. Allotment of rooms shall be the sole discretion of College Administration, which may allot the rooms either on first-come-first-served or any other basis, depending upon the situation, prevailing factors and objectives e.g. fostering cross-cultural relationships.</p><br><br>
						<p style="font-size: 15px;">4. Students must occupy the respective rooms allotted to them. Rooms once allotted to the students for an academic year will not be changed except under special situations. Under no circumstances the inmates should exchange seats/rooms without the knowledge of College Administration. </p><br><br>
						<p style="font-size: 15px;">5. The Hostel Management will generally provide minimum furniture and fittings for each room consisting of cot, mattress, bed sheet, pillow,blanket, window curtain, cupboard, table, chair, ceiling fan with regulator and, a tube light fitting, geyser and bathroom fittings, bucket, mug.</p><br><br>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<p style="font-size: 15px;">6. It should be clearly understood by all students/residents that no tenancy shall be created by their occupation or use of hostel premises and property, and that each of them is merely permitted by College Management, under the rules and regulations framed by the Management which may be changed, altered, modified, varied wholly or partly and can be replaced by Management at their discretion and without assigning any reason for same. Upon such revocation the students/ resident shall not be entitled to stay and/or enter the Hostel/Institute or any part or portion thereof. If she/ he does not leave, he/ she shall be liable to be forcibly removed.</p><br><br>
						<p style="font-size: 15px;">7. Any misleading or false statement or information in the application form shall render the admission for termination and on such termination, students/residents shall not be entitled to stay and/or enter the hostel or part thereof. If she/he does not leave the premises of the Hostel she/he shall be liable to be forcibly removed from the hostel.</p><br><br>
						<p style="font-size: 15px;">8. The management reserves the right to terminate the occupancy of the student for any willful disobedience or defiance of authority, non-observance or frequent violation of hostel rules, causing damage to person or property or indulging in anti-national or undesirable activities. In such cases the deposit and/or hostel fees shall be forfeited and fees will NOT be refunded except the mess charges(if applicable)on pro-rata basis.</p><br><br>
						<p style="font-size: 15px;">9. <span style="display: inline-block; border-bottom: 1px solid #000;">Change/Inter-Change of Room: </span> A student shall not exchange/ interchange his /her room with another student or shift into a vacant room without the previous written permission of the Administrative Officer. The Administrative Officer has the right to shift a student from her/his room to another room in the hostel at any time without assigning any reason.</p><br><br>
						<p style="font-size: 15px;">10. If an unauthorized student stays in the hostel then he/ she will be levied room rent, as decided from time to time and /or other disciplinary action including denial of admission to the hostel for the following years may be taken.</p><br><br>
						<p style="font-size: 15px;">11. The hostel Housing In-charge / Administrative Officer will provide students, with keys of the allotted room. Students shall NOT use other lock and key for locking their rooms. Students are responsible for their possession of all valuables and they should be kept in the cupboard under lock and key. Students shall not leave mobile, ornaments and other valuables unguarded. Students cannot change lock and key without the permission of the Administrative officer and are advised to get duplicate keys made against loss of keys with the permission of Administrative officer.</p><br><br>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<p style="font-size: 15px;">12. The Rector/AdministrativeOfficer /Designated Authority has the discretion to inspect any room at any time.</p><br><br>
						<p style="font-size: 15px;">13. Students shall not indulge in any political or communal activity which is detrimental to the law and order and/or against the Government. Students shall not carry on any propaganda or publicity of any nature whatsoever in respect of anything or any matter including political or communal.</p><br><br>
						<p style="font-size: 15px;">14.	Students shall take prior written permission of the Rector/Administrative Officer/Designated Authority before giving any information or interview regarding Hostel to any member of the Press, Radio, Television or any other media or before making any speech containing any information regarding the Hostel.</p><br><br>
						<p style="font-size: 15px;">15.	A minimum of two months’ notice is necessary in case a student wishes to vacate the hostel. If the student stays in hostel up to the period of three months and then he/she desires to vacate the hostel then he/she has to bear six months hostel fees and rest of the fees will be refunded to the student. If the student stays for more than three months and then desires to vacate the room, then he/she will not be refunded any fees. </p><br><br>
						<p style="font-size: 15px;">16.	The student has to pay hostel fee for one year in advance at the time of admission. The term for the hostel fee is one academic year as per the academic calendar of the University/ Institute.</p><br><br>
						<p style="font-size: 15px;">17.<span style="display: inline-block; border-bottom: 1px solid #000;">	Dress Code: </span> The students should be decently dressed when they are out of rooms. The decision, as to what constitutes a decent dress remains vested with the Warden/Rector/Designated Authority.</p><br><br>
						<p style="font-size: 15px;">18.	No student shall bring or store any firearm, ammunition, explosive and inflammable goods on the premises of the hostel.</p><br><br>
						<p style="font-size: 15px;">19.<span style="display: inline-block; border-bottom: 1px solid #000;"> Alcohol / Drugs / Smoking: </span> Students shall not bring, take and/ or consumealcohol/ intoxicating drink, drug or substance of any kind whatsoever and/or smoke in the room and/or any part of premises. The same shall apply to visitors also. An occurrence of such behavior, shall invite strict disciplinary action leading to rustication from the Institute.</p><br><br>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<p style="font-size: 15px;">20.	If any common property is damaged or lost, the same shall be charged in equal shares to the students who are in common use of that property. Students shall not drive any pegs or nails into walls or stick posters on walls, windows and doors.</p><br><br>
						<p style="font-size: 15px;">21.	<span style="display: inline-block; border-bottom: 1px solid #000;"> Fixtures </span> Student shall not bring any extra furniture or other fixtures in the room. All furniture and fixtures in the rooms allotted to students must be cared for properly. Students will be required to pay double the original cost of any item found missing from their room. Students will also be required to pay twice the charges for repair of items found to have been willfully damaged or have been damaged on account of misuse or unfair wear and tear.</p><br><br>
						<p style="font-size: 15px;">22. <span style="display: inline-block; border-bottom: 1px solid #000;"> Interchange of Furniture/Fixtures: </span> Students are prohibited from interchanging any furniture/fixtures from one point/location in the hostel, to another. Besides a penal recovery, students involved in such activities will be expelled from the hostel.</p><br><br>
						<p style="font-size: 15px;">23.	<span style="display: inline-block; border-bottom: 1px solid #000;">  Assets in Common Areas/Corridors: </span> Theft/damage to hostel assets in common areas/corridors will be recovered from all students of the flank/wing involved. In case of theft/damage, of / to items that pertain to usage by the complete hostel, the recoveries will be made from all the occupants of the hostel. </p><br><br>
						<p style="font-size: 15px;">24.	Hostel authorities will not be responsible for any loss of money, jewellery or personal belongings of any student. Students are advised not to keep any cash/jewellery or any costly items in the room</p><br><br>
						<p style="font-size: 15px;">25.<span style="display: inline-block; border-bottom: 1px solid #000;">	Ragging: </span> Ragging in any form is <span style="display: inline-block; border-bottom: 1px solid #000;"> BANNED. </span> It is a cognizable offence and violation will invite action as per law of the land in addition to rustication from the Institute. Being a silent spectator and not reporting/stopping others indulging in ragging is also an offence and will invite similar disciplinary action. Accepting/undergoing ragging and not reporting it, is also an offence. Please report any incident immediately to any member of the Anti-Ragging Committee/Warden/Housing-In-charge/Administrative officer /Security Personnel/Deputy Director/Dean / Director directly at any time of the day/night.</p><br><br>

					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<p style="font-size: 15px;">26. <span style="display: inline-block; border-bottom: 1px solid #000;"> Electricity Restrictions: </span> No electric appliances shall be permitted in the room, failing which the Rector/Housing-In-charge/ Administrative Officer will have the right to confiscate the gadget. The lights in the bathroom should be used only as and when necessary and shall not be kept switched“on” when not in use. While leaving their rooms, students should take care to switch“off” the lights, fans and A.C. if any,without fail. In case of default, a fine will be charged on every such occasion. Table lamps can be used for study purpose after 12.30 am.</p><br><br>
						<p style="font-size: 15px;">27.	<span style="display: inline-block; border-bottom: 1px solid #000;"> Attendance Register: </span> Attendance will be taken in presence of Rector / Warden before 9.30 pm every night. Students have to sign the attendance register every day between 9.00 pm to 9.30 pm.  Failure to do so will lead to action/fine.</p><br><br>
						<p style="font-size: 15px;">28.	Students desirous of staying outside during nights for any justifiable reason should get prior permission from the Administrative officer / Dean. In case of girl students, they must get permission from their parents as well.</p><br><br>
						<p style="font-size: 15px;">29.	No notices shall be put up or distributed, or no meetings, parties, dinners, etc., should be held in the hostel premises without the prior permission of the Rector/ Administrative Officer.</p><br><br>
						<p style="font-size: 15px;">30.	The student shall be back in the hostel for attendance before 9.30 pm every day. </p><br><br>

						<p style="font-size: 15px;">31.	<span style="display: inline-block; border-bottom: 1px solid #000;">Night out Permission: </span> Night out shall not be allowed without substantive reason. Night out in the permissive sense is hereby abolished. The Dean/Administrative Officer may still permit the students in exceptional circumstances for academic purpose and it should be requested in writing, at least one day before the Night out. Hostel-dwellers/ Boarders may be permitted to go home during holidays with written permission from parents along with the copy of journey ticket, to be submitted to and approved by the Rector / Warden / Administrative officer. All students /residents shall invariably be in the hostel by 9.30 P.M. sharp. If the student comes after 9.30 P. M., he/ she needs to sign the late night register. When taken night out, the student will stay out and will report back in the hostel after 6.00 am in the morning.</p><br><br>
						<p style="font-size: 15px;">32.	Hostel accommodation (room) can be changed by the college management, if they find the same is necessary at any point of time.</p><br><br>

					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian:</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<p style="font-size: 15px;">33.	Hostel Residents are responsible for keeping their rooms and the common areas clean and tidy at all times. All fans, lights and electrical appliances must be switched off when not in use.</p><br><br>
						<p style="font-size: 15px;">34.	Any damage to the hostel property must be reported immediately to the Rector /Warden/Administrative officer. For any damage to the hostel property in a room, the cost of repair / replacement is to be borne by all the occupants of that room or corridor.</p><br><br>
						<p style="font-size: 15px;">35.	All the fittings in the washrooms of the respective rooms should be handled carefully. If there is any breakage due to manhandling, it will be the responsibility of the inmates residing in the respective room and appropriate action will be taken if found breakage is due to manhandling.</p><br><br>
						<p style="font-size: 15px;">36.	<span style="display: inline-block; border-bottom: 1px solid #000;"> Celebration of Festivals and Birthdays: </span> Student shall take prior permission of the Administrative officer for celebrating any festivals and birthdays. Birthdays should be held in a common place between 8.00 pm to 10.00 pm strictly. There should not be any kind of physical discomfort to others. No outside guest or interference of any kind will be allowed.</p><br><br>
						<p style="font-size: 15px;">37. <span style="display: inline-block; border-bottom: 1px solid #000;">Visitors/Parents: </span> Visitors/ Parents are allowed to visit a student only in the visitor’s room between 9.00 am to 11.00 am and 6.00 pm to 8.00 pm on working days and between 11.00 am to 5.00 pm on Sundays and public holidays. No student shall keep talking with visitors in compound, either in or outside the gate/lane. No student shall take any visitor including her/his parents to the room. Personal Servants/Domestic helpers are not allowed inside the rooms. The parents should give an undertaking to cooperate with the authority and should be available on call.</p><br><br>

						<p style="font-size: 15px;">38.	All visitors must first make an entry in the register at the Security cabin and provide all details and documents as requested by the Security personnel before entering the visitor’s area of the hostel complex.</p><br><br>
						<p style="font-size: 15px;">39.	Hostel Residents are not permitted to allow visitors of the opposite gender into the rooms at any time for any reason whatsoever. Any hostel resident found violating this rule will be expelled from the hostel and will also be liable for disciplinary action. Non-compliance of the above shall lead to fines / penalty and / or expulsion from Hostel.</p><br><br>

					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian:</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">
						<p style="font-size: 15px;">40.	<span style="display: inline-block; border-bottom: 1px solid #000;"> Leave: </span> Students are permitted for leave with prior permission of Rector / Warden / Administrative Officer. They must specify the purpose of the same and the time period of the leave to be availed. Duly filled Leave form must be submitted one day prior to avail any leave.</p><br><br>
						<p style="font-size: 15px;">41.	<span style="display: inline-block; border-bottom: 1px solid #000;"> Mess: </span>The Student shall pay the mess fee as defined, from time to time, at the time of admission to the hostel. Outside food is not allowed in hostel room. Student must inform their non-availability or night out to the Mess Supervisor well in advance. </p><br><br>

						<p style="font-size: 15px;">If a student is sick, written application by the student endorsed by the Rector / Warden and medical certificate by VIMS should be given to Supervisor for serving food in the room. </p><br><br>

						<p style="font-size: 15px;">Students are requested not to waste food. Outsiders are not allowed in the mess. Guests are allowed in mess with prior permission of the Supervisor and charges are applicable. </p><br><br>

						<p style="font-size: 15px; font-weight: 600;">Timings for Mess:</p><br><br>
						<p style="font-size: 15px; font-weight: 600;">Breakfast - 7.30 am to 8.30 am</p><br>
						<p style="font-size: 15px; font-weight: 600;">Lunch - 12.30pm to 1.30 pm</p><br>
						<p style="font-size: 15px; font-weight: 600;">Evening Tea/ Snacks: 5.30pm to 6.30pm</p><br>
						<p style="font-size: 15px; font-weight: 600;">Dinner - 7.30 pm to 8.30 pm </p><br>
						<p style="font-size: 15px;">(These timings are subject to change but shall be strictly followed by the students.)</p><br><br>

						<p style="font-size: 15px;">42. <span style="display: inline-block; border-bottom: 1px solid #000;">Suggestion Box/Register: </span> Suggestions and complaints should be either deposited in the ‘’Suggestion Box’’ or should be entered in the ‘’Suggestion Register’’ kept in the hostel premises. Suggestion form is also available in the Hostel.</p><br><br>

						<p style="font-size: 15px;">43.	The College Management has the right to discontinue Hostel accommodation given to a student on account of misconduct and/or violation of rules and regulations. </p><br><br>
						<p style="font-size: 15px;">44.	The hostel management reserves the right to revise the rules and regulations from time to time and will keep the hostel-dwellers informed of any changes in the form of notices on the hostel notice boards.</p><br><br>
						<p style="font-size: 15px;">45.	Ignorance of rules will not be accepted as an excuse.</p><br><br>
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian:</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>

				<tr>
					<td colspan="2" style="padding: 10px 15px; text-align: left;">

						<p style="font-size: 15px; font-weight: 600;">46. HOSTEL AND MESS COMMITTEES</p><br><br>
						<p style="font-size: 15px;">The committees are constituted to monitor the smooth functioning of the hostels, mess and to implement developmental activities of hostel. The motto of all the committees is welfare of students and creation of a conducive academic atmosphere. The committees are Hostel Committee, Hostel Welfare Committee and Mess Committee. The said committees will be constituted at the beginning of each academic year.</p><br>
						
					</td>
				</tr>

				<tr>
					<td style="padding: 10px 15px; text-align: left; width: 60%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Student: &nbsp;</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="padding: 10px 15px; text-align: left; width: 40%;">
						<table cellpadding="0" cellspacing ="0">
							<tr>
								<td style="font-size: 15px;">Signature of Parent/Guardian:</td>
								<td style="border-bottom: 1px solid #000;"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;</td>
							</tr>
						</table>
					</td><br><br><br><br>
				</tr>
				
			</tbody>
		</table>


    </body>
</html>
