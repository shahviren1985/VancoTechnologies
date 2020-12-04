<?php
defined('BASEPATH') OR exit('No direct script access allowed');
// default route
$route['default_controller'] = 'users';
$route['test'] = 'welcome/test';
$route['api']['post'] = 'users/test';
$route['register'] = 'users/register';
$route['hostel-form'] = 'users/hostel_form';
$route['generate-hostel-pdf'] = 'users/hostel_form_api';
$route['online-program'] = 'users/online_program_form';
$route['ami'] = 'users/test_code';
// user login
$route['user_login']['post'] = 'users/login_action';
// admin
$route['admin/home'] = 'admin/admin';
//$route['admin/user/roles'] = 'admin/userroles';
//$route['admin/user/role/edit/(:any)']['get'] = "admin/userroles/edit/$1";
//$route['admin/user/role/update']['post'] = 'admin/userroles/update';


// office admin home
$route['officeadmin/home'] = 'office_admin/office_admin';
$route['officeadmin/upload/excel'] = 'office_admin/import_excel';
$route['import_user_details'] = 'office_admin/import_excel/import';
$route['export_user_details'] = 'office_admin/StudentExcelExport/export';
$route['officeadmin/update/excel_update'] = 'office_admin/import_excel/update_excel';
$route['update_user_excel'] = 'office_admin/import_excel/update_import';
$route['export_update_user_details'] = 'office_admin/StudentExcelExport/export_update_excel';
$route['export_userRollno_excel'] = 'office_admin/StudentExcelExport/export_userRollno_excel';
$route['student-export'] = 'StudentApiExport/students_export';
$route['officeadmin/reporting'] = 'office_admin/reporting';
$route['officeadmin/leaving-certificate/(:any)'] = 'office_admin/LeavingCertificate/$1';
$route['officeadmin/leaving-certificate/add/(:any)'] = 'office_admin/LeavingCertificate/add/$1';
$route['officeadmin/leaving-certificate'] = 'office_admin/LeavingCertificate';
$route['officeadmin/export-student-list'] = 'office_admin/StudentExcelExport';
$route['officeadmin/export-staff-list'] = 'office_admin/StaffExcelExport';
$route['officeadmin/testmail'] = 'office_admin/StaffExcelExport/testmail';
$route['officeadmin/staff-leave-list'] = 'office_admin/StaffExcelExport/staff_leave_list';
$route['export_staff_leave_details'] = 'office_admin/StaffExcelExport/staff_leave_list_export';
$route['staff_annual_leave_export/(:any)'] = 'office_admin/StaffExcelExport/staff_annual_leave_export/$1';
$route['officeadmin/studentlist/studentdetails/edit/(:any)'] = 'office_admin/StudentExcelExport/edit/$1';
$route['officeadmin/stafflist/staffdetails/edit/(:any)'] = 'office_admin/StaffExcelExport/edit/$1';
//$route['staff/requestlist/requestdetails/edit/(:any)'] = 'staff/staffs/edit_application/$1';
$route['staff/requestlist/requestdetails/delete/(:any)'] = 'staff/staffs/delete_record/$1';

// $route['junior_student_export'] = 'office_admin/StudentExcelExport/student_export_report';
$route['officeadmin/search'] = 'office_admin/search';
$route['abhishek'] = 'office_admin/search/abhishek';
$route['officeadmin/application_form'] = 'office_admin/StudentExcelExport/application_form';
$route['search_student'] = 'office_admin/search/searchStudent';
$route['report/export-excel-report'] = 'office_admin/reporting/export_report';
$route['report/search-report'] = 'office_admin/reporting/search_report';
$route['generate_challan'] = 'office_admin/search/generate_challan';
$route['generate-challan-offline'] = 'office_admin/search/generate_challan_offline';
$route['update_transaction_status'] = 'office_admin/search/update_transaction_status';
$route['alumini-registration'] = 'Alumini';
$route['alumini-registration-copy'] = 'AluminiCopy';
$route['alumini-event'] = 'Alumini/alumini_event'; 
$route['refund-policy'] = 'Alumini/refund_policy'; 
$route['miscellaneous-refund-policy'] = 'student/ExamPayment/refund_policy'; 
$route['officeadmin/upload_transaction'] = 'office_admin/import_excel/upload_transaction';
$route['import_transaction'] = 'office_admin/import_excel/import_transaction';
$route['generate-mixed-challan-offline'] = 'office_admin/search/generate_mixed_challan_offline';




// $route['search/fees'] = 'office_admin/search_fees';

// student
//$route['success'] = 'student/fee_payment/success';
//$route['fail'] = 'student/fee_payment/fail';
$route['student/details/edit/(:any)'] = 'student/students/edit/$1';
$route['staff/details/edit/(:any)'] = 'staff/staffs/edit/$1';
$route['check_payment']['post'] = 'student/fee_payment/check';
$route['change_first_time_login'] = 'student/students/ftl';
$route['update_basic_details'] = 'student/students/update_basic_details';
$route['student/home'] = 'student/students';
$route['student/application_form'] = 'student/students/application_form';
$route['student/fee/payment'] = 'student/fee_payment';
$route['student/account/history'] = 'student/account_history';
$route['student/documents'] = 'pages/documents';
$route['student/feedback'] = 'pages/feedback';
$route['student/performance'] = 'pages/performance';
$route['student/applications'] = 'pages/applications';
$route['generate-pdf'] = 'users/register_api';
$route['pay-online'] = 'student/ExamPayment/pay_online';
$route['miscellaneous-payment'] = 'student/ExamPayment';
$route['student/subject-fees-payment'] = 'student/SubjectPayment';
$route['student/choose_elective'] = 'student/ChooseElective';
$route['student/pay-online'] = 'student/SubjectPayment/pay_online';
$route['staff/documents'] = 'pagesstaff/documents';
$route['staff/view_attendance'] = 'pagesstaff/view_attendance';
$route['staff/approve_elective'] = 'pagesstaff/approve_elective';
$route['staff/leave_application'] = 'pagesstaff/leave_application';
$route['staff/leave_request_list'] = 'pagesstaff/leave_request_list';
$route['leaveSubmit'] = 'staff/staffs/leaveSubmit';

$route['fee-payment-form'] = 'student/new_payment';
$route['fee_payment_post'] = 'student/new_payment/fee_payment_post';
$route['officeadmin/addon-subject-student'] = 'office_admin/office_admin/addon_subject_student';
$route['officeadmin/addon_subject_student_post'] = 'office_admin/office_admin/addon_subject_student_post';
$route['generate-challan-junior'] = 'office_admin/search/generate_challan_junior';
//Rest Api routes


//logout
$route['logout'] = 'users/logout';

$route['404_override'] = 'errors/html/error_404';
$route['translate_uri_dashes'] = FALSE;


/*
| -------------------------------------------------------------------------
| URI ROUTING
| -------------------------------------------------------------------------
| This file lets you re-map URI requests to specific controller functions.
|
| Typically there is a one-to-one relationship between a URL string
| and its corresponding controller class/method. The segments in a
| URL normally follow this pattern:
|
|	example.com/class/method/id/
|
| In some instances, however, you may want to remap this relationship
| so that a different class/function is called than the one
| corresponding to the URL.
|
| Please see the user guide for complete details:
|
|	https://codeigniter.com/user_guide/general/routing.html
|
| -------------------------------------------------------------------------
| RESERVED ROUTES
| -------------------------------------------------------------------------
|
| There are three reserved routes:
|
|	$route['default_controller'] = 'welcome';
|
| This route indicates which controller class should be loaded if the
| URI contains no data. In the above example, the "welcome" class
| would be loaded.
|
|	$route['404_override'] = 'errors/page_missing';
|
| This route will tell the Router which controller/method to use if those
| provided in the URL cannot be matched to a valid route.
|
|	$route['translate_uri_dashes'] = FALSE;
|
| This is not exactly a route, but allows you to automatically route
| controller and method names that contain dashes. '-' isn't a valid
| class or method name character, so it requires translation.
| When you set this option to TRUE, it will replace ALL dashes in the
| controller and method URI segments.
|
| Examples:	my-controller/index	-> my_controller/index
|		my-controller/my-method	-> my_controller/my_method
*/

