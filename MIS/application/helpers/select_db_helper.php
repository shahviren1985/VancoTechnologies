<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

// selecting database based on connectionString
if (!function_exists('SelectDB')) {
    function SelectDB($connectionString) {
        $cstring = '';
        if( $connectionString == 'officeadmin1' || $connectionString == 'clg_db1' ){
            $cstring = 'clg_db1';
        }else if( $connectionString == 'officeadmin2' || $connectionString == 'clg_db2' ){
            $cstring = 'clg_db2';
        }else if( $connectionString == 'officeadmin3' || $connectionString == 'clg_db3' ){
            $cstring = 'clg_db3';
        }else{
            $cstring = 'clg_db2';
        }
        return $cstring;
    }
}


if (!function_exists('test')) {
    function test($data) {
        $course_id = $data['course_id'];
        $category = strtolower($data["category"]);
        
        $CI =& get_instance();
        $CI->load->model('student/fee_detail');
        $fee_details = $CI->fee_detail->get_entry($course_id);
        $output =  '
                    <style>
                        tbody td { border-bottom:1px solid black !important; padding: 8px;}
                        tfoot tr { background-color:aliceblue; }
                        tfoot th { text-align: left; padding: 10px !important; }
                        table tr{ text-align:left; }
                    </style>
                    <table style="width:80%;">
                        <thead style="background-color:aliceblue;">
                            <tr>
                                <th style="padding:8px;text-align: left;">S. No.</th>
                                <th style="padding:8px;text-align: left;">Fee Head</th>
                                <th style="padding:8px;text-align: left;">Amount</th>
                            </tr>
                        </thead>
                        <tbody>';
                        if( isset($fee_details) && count($fee_details) > 0 ) { 
                            $count = 0;
                            $fee_head_total = 0;
                            $pay_percentage = 0;
                            $gst = 0;
                            $grand_total = 0;

                            if($category == "open") {
                              foreach( $fee_details as $key => $fee_detail ) {
                                $count++; 
                                $fee_head_total += $fee_details[$key]->amount;
                                $output .=  '<tr><td style="padding:8px;">'.$count.'</td><td style="padding:8px;">'.$fee_detail->fee_head.'</td><td style="padding:8px;">'.number_format($fee_detail->amount).'</td></tr>';
                                } 
                            }

                            if($category != "open") {
                              foreach( $fee_details as $key => $fee_detail ) {
                                $count++; 
                                $fee_head_total += $fee_details[$key]->reserved_amount;;
                                $output .=  '<tr><td style="padding:8px;">'.$count.'</td><td style="padding:8px;">'.$fee_detail->fee_head.'</td><td style="padding:8px;">'.number_format($fee_detail->reserved_amount).'</td></tr>';
                                } 
                            }
                        }

        $output .=     '</tbody>
                        <tfoot>
                            <tr style="background-color:aliceblue;">
                                <th style="padding:8px; text-align: left;"></th>
                                <th style="padding:8px; text-align: left;">Fee Head Total</th>
                                <th style="padding:8px; text-align: left;">Rs. '.number_format($fee_head_total, "2").'</th>
                            </tr>
                            <tr style="background-color:aliceblue;">
                                <th style="padding:8px; text-align: left;"></th>
                                <th style="padding:8px; text-align: left;">Online pay percentage(2%)</th>
                                <th style="padding:8px; text-align: left;">Rs. ';
                                $pay_percentage = (2/100)*$fee_head_total;
                                if($pay_percentage >= "500"){ 
                                    $output .= number_format("500", "2"); 
                                }else{
                                    $output .= number_format($pay_percentage, "2");
                                }
        $output .=              '</th>
                            </tr>
                            <tr style="background-color:aliceblue;">
                                <td style="padding:8px; text-align: left;"></td>
                                <th style="padding:8px; text-align: left;">GST(18%)</th>
                                <th style="padding:8px; text-align: left;">Rs. ';
                                $gst = (18/100)*($pay_percentage);
                                  $output .= number_format($gst, "2");
        $output .=              '</th>
                            </tr>
                            <tr style="background-color:aliceblue;">
                                <th style="padding:8px; text-align: left;"></th>
                                <th style="padding:8px; text-align: left;">Grand Total</th>
                                <th style="padding:8px; text-align: left;">Rs. ';
                                $grand_total = $fee_head_total+$pay_percentage+$gst;
                                  $output .= number_format($grand_total, "2")."/-";
        $output .=              '</th>
                            </tr>
                        </tfoot>
                    </table>';

        return $output;
    }
}