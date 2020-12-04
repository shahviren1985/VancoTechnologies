<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>

<html>
<head>
    <style>
        table.borderTable {
            border-collapse: collapse;
        }

        table.borderTable {
            border: 1px solid black;
        }

        th.borderTable, td.borderTable {
            border: 1px solid black;
            padding: 5px;
        }

        .marks td {
            font-size: 12px;
            text-align: center;
        }

        tr.bottombor {
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <table autosize="1" style="width:100%;border:1px solid black; font-family: Arial;">
        <tr>
            <td style="padding:10px;">
                <table style="width:100%">
                    <tr style="page-break-before:always">
                        <td>
                            <table autosize="1" style="width:100%; border-bottom: 1px solid; padding-bottom: 10px;">
                                <tr style="padding: 2px;">
                                    <td style="width:20%;">
                                    	<img height="180" width="180" src="<?php echo base_url().'assets/img/logo.png'; ?>">                              
                                    </td>
                                    <td style="width:80%;text-align:center;font-size: 55px; font-weight: bold;">
                                        Sir Vithaldas Thackersey College of Home Science (Autonomous) <br />
                                        S.N.D.T. Women's University Juhu, Mumbai - 400 049<br/>
                                        Junior College – Registration Form (Std. XI)
                                    </td>
                                   <!--  <td style="width:20%;text-align: right;">
                                        <img style="border:1px solid gray;" height="280" width="250" src="<?php //echo $photo_path; ?>">
                                    </td> -->
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        	 <table cellpadding="0" cellspacing="0" autosize="1" style="width:100%;" class="borderTable">
                        	 	<tr>
                            		<td class="" style="width: 80%; font-weight: bold; font-size: 35px;">
                            			<table cellpadding="0" cellspacing="0" style="width: 100%; padding: 0px;">
                            		 <tr>
                                    <td class="borderTable" style="width: 40%; font-weight: bold; font-size: 35px; padding: 15px;">Choice of subject For Std.XI</td>
                                    <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $choice_subject_XI .'/ Language'. ' (' . $language_of_choice. ')';  ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 40%; font-weight: bold; font-size: 35px; padding: 15px;">Name of the student:</td>
                                    <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($last_name." ".$first_name ." ".$father_name." ".$mother_first_name); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 40%; font-weight: bold; font-size: 35px; padding: 15px;">Father's Name:</td>
                                    <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($father_name); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 40%; font-weight: bold; font-size: 35px; padding: 15px;">Mother's Name:</td>
                                    <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($mother_first_name); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 40%; font-weight: bold; font-size: 35px; padding: 15px;">Percentage obtained in Xth examination (correct to two decimal places</td>
                                    <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $percentage_in_ssc; ?></td>
                                </tr> 
                            			</table>
                            		</td>
                            		<td class="borderTable" style="width: 20%; font-weight: bold; font-size: 35px;">
                            			<table cellpadding="0" cellspacing="0" style="width: 100%;">
                            				<tr>
                            					<td style="text-align: center;"><img src="<?php echo $photo_path; ?>" width="180" height="180"></td>
                            				</tr>
                            			</table>
                            		</td>
                            	</tr>
                        	 </table>
                            <table cellpadding="0" cellspacing="0" autosize="1" style="width:100%;" class="borderTable">
                            	
                                <!-- <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Choice of subject For Std.XI</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php //echo $choice_subject_XI .'/ Language'. ' (' . $language_of_choice. ')';  ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Name of the student:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php //echo strtoupper($last_name." ".$first_name ." ".$father_name." ".$mother_first_name); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Father's Name:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php //echo strtoupper($father_name); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Mother's Name:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php //echo strtoupper($mother_first_name); ?></td>
                                </tr> -->
                                
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Name of examination:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($name_of_examination); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Year of Passing::</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $year_of_passing; ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">*Category under which admission is sought</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($category); ?></td>
                                </tr>
                                <?php if($category == 'General'){
                                	?>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Sub Category:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $sub_cat; ?></td>
                                </tr>
                                <?php if (strpos($sub_cat, 'Handicapped') !== false) {
                                	?>
									  <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Disability Type:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $disability_type; ?></td>
                                	</tr>
                                	<tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Disability Percentage:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $disability_percentage; ?>%</td>
                                	</tr>
                                	<tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Disability Certificate Number:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $disability_number; ?></td>
                                	</tr>		
									<?php
										}
									?>
                                <?php
                            	}
                            	?>

                            	<?php if($category == 'Reservation'){
                                	?>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Sub Category:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $sub_cat; ?></td>
                                </tr>
                                <?php if (strpos($sub_cat, 'Handicapped') !== false) {
                                	?>
									  <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Disability Type:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $disability_type; ?></td>
                                	</tr>
                                	<tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Disability Percentage:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $disability_percentage; ?></td>
                                	</tr>
                                	<tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Disability Certificate Number:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $disability_number; ?></td>
                                	</tr>		
									<?php
										}
									?>
									<?php if (strpos($sub_cat, 'Caste') !== false) {
                                	?>
									  <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Caste:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $caste; ?></td>
                                	</tr>
									<?php
										}
									?>
                                <?php
                            	}
                            	?>
                              
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Mother Tongue:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo ucfirst($mother_tongue); ?></td>
                                </tr>
                               
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Aadhar Card No</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $aadhaar_number; ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Blood Group:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($blood_group); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Bank A/c NO.:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $bank_acc_no; ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">IFSC code:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($ifsc_code); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Status Married/Unmarried:</td>
                                    <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $marital_status; ?></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="padding: 20px 0px;">
                            <table autosize="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 50%; padding: 30px 10px 30px 0px; border-top: 1px solid #000; border-right: 1px solid #000; border-bottom: 1px solid #000;">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td style="padding-bottom: 25px; font-weight: bold; font-size: 45px;">FOR OFFICE USE ONLY</td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" class="borderTable" style="width: 100%;">
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Admitted to</td>
                                                <td style="width: 40%; font-size: 35px; padding: 15px; border-top: 1px solid #000; border-right: 1px solid #000;">H.Sc &nbsp;&nbsp; P.Sc</td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Eligible for free exemption</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;">YES &nbsp;&nbsp; NO</td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">From Maharashtra Board</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;">YES &nbsp;&nbsp; NO</td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">Language of choice</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;">Hindi &nbsp; Marathi &nbsp; Gujrati</td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">Documents submitted</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">X Marsheet</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">~Ration Card</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">~Declaration form</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">Caste Certificate</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Aadhar Card</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">Eligibility Criteria</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">Migration/Transfer</td>
                                                <td class="borderTable" style="width: 40%; padding: 5px 0 5px 10px;">
                                                    <table autosize="1" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="border: 1px solid #000; padding: 15px; width: 40px; height: 25px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 30px; font-weight: bold;">Sign:</td>
                                                <td class="borderTable" style="width: 40%; padding: 30px; font-size: 35px;">___________________</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%; padding: 30px 10px 30px 0px; border-top: 1px solid #000; border-right: 1px solid #000; border-bottom: 1px solid #000;">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td style="padding-bottom: 25px; padding-left: 15px; font-weight: bold; font-size: 45px;">DETAILS OF S.S.C(STD.X)</td>
                                            </tr>
                                        </table>
                                        <table  cellpadding="0" cellspacing="0" class="borderTable" style="width: 100%;">
                                            <tr>
                                                <td class="borderTable" style="width: 40%; font-weight: bold; font-size: 35px; padding: 15px; border-top: 1px solid #000; border-right: 1px solid #000;">Name of Board</td>
                                                <td style="width: 60%; font-size: 35px; padding: 15px; border-top: 1px solid #000; border-right: 1px solid #000;"><?php echo strtoupper($name_of_board); ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px; font-weight: bold;">Exam seat No.:</td>
                                                <td class="borderTable" style="width: 40%; padding: 15px; font-size: 35px;"><?php echo $exam_seat_no; ?></td>
                                            </tr>
                                            <tr>
                                            	<td class="borderTable" style="width: 60%; font-size: 35px; padding: 15px;"><span style="font-weight: bold;">Total Marks obtained:</span> <?php echo $marks_obtained; ?></td>
                                                <td class="borderTable" style="width: 40%; padding: 15px; font-size: 35px;"><span style="font-weight: bold;">out of:</span> <?php echo $marks_outof; ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style=" font-weight: bold; font-size: 35px; padding: 15px;">Name and Address of School:</td>
                                                <td class="borderTable" style=" font-size: 35px; padding: 15px;"><?php echo ucwords($name_of_school)."/".ucwords($address_of_school); ?></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="font-weight: bold; font-size: 45px; padding: 40px;">PERSONAL DETAILS</td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Date of Birth:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo $date_of_birth; ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Place of Birth:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo ucfirst($birth_place); ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Name of father/guardian:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo strtoupper($guardian_name); ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Address:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo ucwords($permanent_address); ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Parent's Email:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo $guardian_email; ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Parent's Mobile:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo $guardian_mobile; ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Student's Mobile No.:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo $mobile_number; ?></td>
                                            </tr>
                                            <tr>
                                                <td class="borderTable" style="width: 60%; font-weight: bold; font-size: 35px; padding: 15px;">Student's Email:</td>
                                                <td class="borderTable" style="width: 40%; font-size: 35px; padding: 15px;"><?php echo $email_id; ?></td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="width:100%;" class="borderTable">
                                <tr>
                                    <td class="borderTable" style="width: 50%; font-weight: bold; font-size: 35px; padding: 15px;">Tel/(Res):</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $guardian_mobile; ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px; font-weight: bold;">(Office):</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $guardian_mobile; ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px; font-weight: bold;">Address (Native):</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo ucwords($correspondance_address); ?></td>
                                </tr>

                                <tr>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px; font-weight: bold;">Father's Profession :</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo ucwords($guardian_profession); ?></td>
                                </tr>

                                <tr>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px; font-weight: bold;">Annual Income:</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $guardian_income; ?></td>
                                </tr>

                                <tr>
                                    <td class="borderTable" style="width: 50%; font-weight: bold; font-size: 35px; padding: 15px;">Applicant's relationship to guardian:</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo $relationship_to_guardian; ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 50%; font-weight: bold; font-size: 35px; padding: 15px;">Religion:</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo strtoupper($religion); ?></td>
                                </tr>
                                <tr>
                                    <td class="borderTable" style="width: 50%; font-weight: bold; font-size: 35px; padding: 15px;">Extra curricular achivements:</td>
                                    <td class="borderTable" style="width: 50%; font-size: 35px; padding: 15px;" colspan="3"><?php echo ucfirst($extra_curricular_achivements); ?></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:100%;" class="">
                                <tr>
                                    <td colspan="2">
                                        <table cellpadding="0" cellspacing="0" style="width:100%;">
                                            <tr>
                                                <td style="width: 10%; font-weight: bold; font-size: 35px; padding: 15px;">Date:</td>
                                                <td style="width: 10%; font-size: 35px; padding: 15px;">______________</td>
                                                <td style="width: 10%; font-size: 35px; padding: 15px; font-weight: bold;">Signature:</td>
                                                <td style="width: 20%; font-size: 35px; padding: 15px;">____________________</td>
                                                <td style="width: 30%; font-size: 35px; padding: 15px; font-weight: bold;">Signature of parent/guardian:</td>
                                                <td style="width: 20%; font-size: 35px; padding: 15px;">____________________</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>