<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Staff extends CI_Model {

	public function __construct() {
        parent::__construct();
        $this->load->helper('select_db');
    }

    public function ftl($id) {		
    	$this->db->where('id', $id);
	    $this->db->update('login_details', array('first_time_login' => '1'));
	    return true;
    }

    public function get_all_leave_details() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get('leave_application');
		return $query->result();
    } 
	
	public function get_all_staff_details() {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get('staff_details');
		return $query->result();
    }   

	public function employee_data_byid($staff_id) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('employee_code' => $staff_id));
		return $query->result();
    } 
	
	public function get_staff_by_department($dept) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
		$db_query->select('*')
         ->from('staff_details');
		$db_query->where('department like "%'.$dept.'%" and role != "HOD"');
		$query = $db_query->get();
		return $query->result();
    } 
	
	public function get_entry($userID) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('username' => $userID));
		return $query->result();
    }
	
	public function get_leave_request($employee_code) {
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
		$db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('leave_application', array('staff_id' => $employee_code));
		return $query->result();
    }   
	
	public function insert_contact($data){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->insert('leave_application', $data);
		//die('test');
    }
	
	public function fetch_data($id, $role) {	
		$username = $this->session->userdata('userID');
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByStatus = $this->input->post('searchByStatus');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('leave_application.*, staff_details.role')
         ->from('leave_application');
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		if($searchByStatus == "Pending"){
			$db_query->where('status like "%'.$searchByStatus.'%" and staff_id = '.$id.'');
		}elseif($searchByStatus == "HoD Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and staff_id = '.$id.'');
		}elseif($searchByStatus == "OS Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and staff_id = '.$id.'');
		}elseif($searchByStatus == "All"){
			$db_query->where('(status like "%Pending%" or status like "%OS Approved%" or status like "%OS Rejected%" or status like "%Principal Approved%" or status like "%Principal Rejected%" or status like "%HoD Approved%" or status like "%HoD Rejected%") and staff_id = '.$id.' and role = "'.$role.'"');
		}
		if($searchValue){
			$db_query->where("status like '%".$searchValue."%' or staff_id like '%".$searchValue."%' or leave_type like '%".$searchValue."%'");
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		if($_POST['length'] != -1)
		$db_query->limit($_POST['length'],$_POST['start']);
		$query = $db_query->get();
		//echo $db_query->last_query();
		return $query->result();
	}	
	
	
	public function fetch_data_head($role) {	
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByStatus = $this->input->post('searchByStatus');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		
		$db_query->select('leave_application.*, staff_details.role')
		->from('leave_application');
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		if($role == 'Principal' && $searchByStatus == "Pending"){
			$db_query->where("role != '".$role."' and (status != 'Principal Approved' and status != 'Principal Rejected') and (status like '%OS Approved%' or (role = 'Non-teaching Staff' and os_approval_status like '%Approved%') or (role = 'Office Supretendent' and os_approval_status like '%Pending%'))");
		}elseif($role == 'Principal' && $searchByStatus == "HoD Approved"){
			$db_query->where("role != '".$role."' and status like '%HoD Approved%'");
		}elseif($role == 'Principal' && $searchByStatus == "OS Approved"){
			$db_query->where("role != '".$role."' and status like '%OS Approved%'");
		}elseif($role == 'Principal' && $searchByStatus == "All"){
			$db_query->where("role != '".$role."' and (status like '%HoD Approved%' or status like '%HoD Rejected%' or status like '%OS Approved%' or status like '%OS Rejected%' or status like '%Pending%' or status like '%Principal Rejected%' or status like '%Principal Approved%')");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "Pending"){
			$db_query->where("role != '".$role."' and (hod_approval_status like '%Approved%' or role = 'HOD' or role = 'Non-teaching Staff') and (status like '%HoD Approved%' or (role = 'HOD' and status like '%Pending%') or (role = 'Non-teaching Staff' and status like '%Pending%'))");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "HoD Approved"){
			$db_query->where("role != '".$role."' and status like '%HoD Approved%'");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "OS Approved"){
			$db_query->where("role != '".$role."' and status like '%OS Approved%'");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "All"){
			$db_query->where("role != '".$role."' and (hod_approval_status like '%Approved%' or role = 'HOD' or role = 'Non-teaching Staff') and (os_approval_status like '%Rejected%' or os_approval_status like '%Pending%' or os_approval_status like '%Approved%') and (status like '%HoD Approved%' or status like '%HoD Rejected%' or status like '%OS Approved%' or status like '%OS Rejected%' or status like '%Pending%')");
		}	
	
		if($searchValue){
			$db_query->where("status like '%".$searchValue."%' or staff_id like '%".$searchValue."%' or leave_type like '%".$searchValue."%'");
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		if($_POST['length'] != -1)
		$db_query->limit($_POST['length'],$_POST['start']);
		$query = $db_query->get();
		  //echo $db_query->last_query();
		return $query->result();
	}
	
	public function fetch_data_by_dept($dept) {
		$username = $this->session->userdata('userID');
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByStatus = $this->input->post('searchByStatus');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('leave_application.*, staff_details.username')
         ->from('leave_application');
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		$db_query->where("department = '".$dept."'");		
		if($searchByStatus == "Pending"){
			$db_query->where('status like "%'.$searchByStatus.'%" and hod_approval_status like "%Pending%" and username NOT LIKE "%'.$username.'%"');
		}elseif($searchByStatus == "HoD Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and username NOT LIKE "%'.$username.'%"');
		}elseif($searchByStatus == "OS Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and username NOT LIKE "%'.$username.'%"');
		}elseif($searchByStatus == "All"){
			$db_query->where('(status like "%Pending%" or status like "%HoD Approved%" or status like "%HoD Rejected%" or status like "%OS Approved%" or status like "%OS Rejected%" or status like "%Principal Approved%" or status like "%Principal Rejected%") and username NOT LIKE "%'.$username.'%"');
		}
		
		if($searchValue){
			$db_query->where("status like '%".$searchValue."%' or staff_id like '%".$searchValue."%' or leave_type like '%".$searchValue."%'");
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		if($_POST['length'] != -1)
		$db_query->limit($_POST['length'],$_POST['start']);
		$query = $db_query->get();
		//echo $db_query->last_query();
		return $query->result();
	}
	
	public function count_all(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		return $db_query->count_all_results('leave_application');	
	}

	public function count_all_by_id($id){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->where('staff_id = '.$id.'');
		return $db_query->count_all_results('leave_application');	
	}	
	
	public function count_all_by_dept($dept){
		$username = $this->session->userdata('userID');
		$searchByStatus = $this->input->post('searchByStatus');
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		$db_query->where("department = '".$dept."' and username != '".$username."'");
		//echo $db_query->last_query();
		return $db_query->count_all_results('leave_application');	
	}
	
	public function count_all_by_role($role){
		$searchByStatus = $this->input->post('searchByStatus');
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		if($role == 'Principal' && $searchByStatus == "Pending"){
			$db_query->where("role != '".$role."' and (status != 'Principal Approved' and status != 'Principal Rejected') and (status like '%OS Approved%' or (role = 'Non-teaching Staff' and os_approval_status like '%Approved%') or (role = 'Office Supretendent' and os_approval_status like '%Pending%'))");
		}elseif($role == 'Principal' && $searchByStatus == "HoD Approved"){
			$db_query->where("role != '".$role."' and status like '%HoD Approved%'");
		}elseif($role == 'Principal' && $searchByStatus == "OS Approved"){
			$db_query->where("role != '".$role."' and status like '%OS Approved%'");
		}elseif($role == 'Principal' && $searchByStatus == "All"){
			$db_query->where("role != '".$role."' and (status like '%HoD Approved%' or status like '%HoD Rejected%' or status like '%OS Approved%' or status like '%OS Rejected%' or status like '%Pending%' or status like '%Principal Rejected%' or status like '%Principal Approved%')");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "Pending"){
			$db_query->where("role != '".$role."' and (hod_approval_status like '%Approved%' or role = 'HOD' or role = 'Non-teaching Staff') and (status like '%HoD Approved%' or (role = 'HOD' and status like '%Pending%') or (role = 'Non-teaching Staff' and status like '%Pending%'))");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "HoD Approved"){
			$db_query->where("role != '".$role."' and status like '%HoD Approved%'");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "OS Approved"){
			$db_query->where("role != '".$role."' and status like '%OS Approved%'");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "All"){
			$db_query->where("role != '".$role."' and (hod_approval_status like '%Approved%' or role = 'HOD' or role = 'Non-teaching Staff') and (os_approval_status like '%Rejected%' or os_approval_status like '%Pending%' or os_approval_status like '%Approved%') and (status like '%HoD Approved%' or status like '%HoD Rejected%' or status like '%OS Approved%' or status like '%OS Rejected%' or status like '%Pending%')");
		}
		return $db_query->count_all_results('leave_application');	
	}
	
	public function count_filtered($id, $role){
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByStatus = $this->input->post('searchByStatus');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('*')
         ->from('leave_application');
		 $db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		if($searchByStatus == "Pending"){
			$db_query->where('status like "%'.$searchByStatus.'%" and staff_id = '.$id.'');
		}elseif($searchByStatus == "HoD Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and staff_id = '.$id.'');
		}elseif($searchByStatus == "OS Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and staff_id = '.$id.'');
		}elseif($searchByStatus == "All"){
			$db_query->where('(status like "%Pending%" or status like "%OS Approved%" or status like "%OS Rejected%" or status like "%Principal Approved%" or status like "%Principal Rejected%" or status like "%HoD Approved%" or status like "%HoD Rejected%") and staff_id = '.$id.' and role = "'.$role.'"');
		}
		if($searchValue){
			$db_query->where("status like '%".$searchValue."%' or staff_id like '%".$searchValue."%' or leave_type like '%".$searchValue."%'");
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		$query = $db_query->get();
		//echo $db_query->last_query();
		return $query->num_rows();
	}
	
	
	public function count_filtered_head_by_dept($dept){
		$username = $this->session->userdata('userID');
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByStatus = $this->input->post('searchByStatus');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
				
		$db_query->select('*')
         ->from('leave_application');
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		$db_query->where("department = '".$dept."'");		
		if($searchByStatus == "Pending"){
			$db_query->where('status like "%'.$searchByStatus.'%" and hod_approval_status like "%Pending%" and username NOT LIKE "%'.$username.'%"');
		}elseif($searchByStatus == "HoD Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and username NOT LIKE "%'.$username.'%"');
		}elseif($searchByStatus == "OS Approved"){
			$db_query->where('status like "%'.$searchByStatus.'%" and username NOT LIKE "%'.$username.'%"');
		}elseif($searchByStatus == "All"){
			$db_query->where('(status like "%Pending%" or status like "%Approved%" or status like "%Rejected%") and username NOT LIKE "%'.$username.'%"');
		}
		
		if($searchValue){
			$db_query->where("status like '%".$searchValue."%' or staff_id like '%".$searchValue."%' or leave_type like '%".$searchValue."%'");
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		$query = $db_query->get();
		//echo $db_query->last_query();
		return $query->num_rows();
	}
	
	public function count_filtered_head_by_role($role){
		$order = $this->input->post('order');
		$columnSortOrder = $order[0]['dir']; // asc or desc
		$columnIndex = $order[0]['column']; // Column index		
		$column = $this->input->post('columns');
		$columnName = $column[$columnIndex]['data']; // Column name
		$searchData = $this->input->post('search');
		$searchValue = $searchData['value']; // Search value		

		## Custom Field value
		$searchByStatus = $this->input->post('searchByStatus');
		
		## Search 
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->select('*')
		->from('leave_application');
		$db_query->join('staff_details','staff_details.employee_code = leave_application.staff_id','INNER');
		if($role == 'Principal' && $searchByStatus == "Pending"){
			$db_query->where("role != '".$role."' and (status != 'Principal Approved' and status != 'Principal Rejected') and (status like '%OS Approved%' or (role = 'Non-teaching Staff' and os_approval_status like '%Approved%') or (role = 'Office Supretendent' and os_approval_status like '%Pending%'))");
		}elseif($role == 'Principal' && $searchByStatus == "HoD Approved"){
			$db_query->where("role != '".$role."' and status like '%HoD Approved%'");
		}elseif($role == 'Principal' && $searchByStatus == "OS Approved"){
			$db_query->where("role != '".$role."' and status like '%OS Approved%'");
		}elseif($role == 'Principal' && $searchByStatus == "All"){
			$db_query->where("role != '".$role."' and (status like '%HoD Approved%' or status like '%HoD Rejected%' or status like '%OS Approved%' or status like '%OS Rejected%' or status like '%Pending%' or status like '%Principal Rejected%' or status like '%Principal Approved%')");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "Pending"){
			$db_query->where("role != '".$role."' and (hod_approval_status like '%Approved%' or role = 'HOD' or role = 'Non-teaching Staff') and (status like '%HoD Approved%' or (role = 'HOD' and status like '%Pending%') or (role = 'Non-teaching Staff' and status like '%Pending%'))");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "HoD Approved"){
			$db_query->where("role != '".$role."' and status like '%HoD Approved%'");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "OS Approved"){
			$db_query->where("role != '".$role."' and status like '%OS Approved%'");
		}elseif($role == 'Office Supretendent' && $searchByStatus == "All"){
			$db_query->where("role != '".$role."' and (hod_approval_status like '%Approved%' or role = 'HOD' or role = 'Non-teaching Staff') and (os_approval_status like '%Rejected%' or os_approval_status like '%Pending%' or os_approval_status like '%Approved%') and (status like '%HoD Approved%' or status like '%HoD Rejected%' or status like '%OS Approved%' or status like '%OS Rejected%' or status like '%Pending%')");
		}		
			
		if($searchValue){
			$db_query->where("status like '%".$searchValue."%' or staff_id like '%".$searchValue."%' or leave_type like '%".$searchValue."%'");
		}		
		$db_query->order_by($columnName, $columnSortOrder);
		$query = $db_query->get();
		//echo $db_query->last_query();
		return $query->num_rows();
	}
	
	 public function get_request_by_id($id = 0) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        if ($id === 0)
        {
            $query = $db_query->get('leave_application');
            return $query->result_array();
        }
        $query = $db_query->get_where('leave_application', array('id' => $id));
        return $query->row_array();
    }
	
	public function delete_request($id){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        if ($id === 0)
        {
            $query = $db_query->get('leave_application');
            return $query->result_array();
        }
        $query = $db_query->delete('leave_application', array('id' => $id));
        return true;
	}
	
	public function application_status($data, $id) {
        $database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);		
        $db_query = $this->load->database($database, TRUE);
    	$query = $db_query->update('leave_application', $data, array('id' => $id));
		//echo $db_query->last_query();
		return $query;
    } 
	
	public function get_hod_data($dept){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('department' => $dept));
        return $query->row_array();
	}	
	
	public function get_os_data(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('role' => 'Office Supretendent'));
        return $query->row_array();
	}
	
	public function get_principal_data(){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('role' => 'Principal'));
        return $query->row_array();
	}
	
	public function get_staff_role($staff_id){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('employee_code' => $staff_id));
        return $query->row_array();
	}
	
	public function get_head_data($id){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('username' => $id));
        return $query->row_array();
	}
	
	public function get_head_data_by_role($role){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('staff_details', array('role' => $role));
        return $query->row_array();
	}
	
	public function get_leave_details($id){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
        $query = $db_query->get_where('leave_application', array('id' => $id));
        return $query->row_array();
	}
	public function update_leave_balance($employee_code, $update_data){
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
        $database = SelectDB($connectionString);		
        $db_query = $this->load->database($database, TRUE);
		if($update_data['leave_type'] == 'Casual-Leave'){
			$data = array(
				'casual_leave_balance' => $update_data['days']
			);
		}elseif($update_data['leave_type'] == 'Sick-Leave'){
			$data = array(
				'sick_leave_balance' => $update_data['days']
			);
		}elseif($update_data['leave_type'] == 'Paid-Leave'){
			$data = array(
				'paid_leave_balance' => $update_data['days']
			);
		}elseif($update_data['leave_type'] == 'Unpaid-Leave'){
			$data = array(
				'unpaid_leave' => $update_data['days']
			);
		}elseif($update_data['leave_type'] == 'Half-Day-Leave'){
			$data = array(
				'half_day_leave' => $update_data['days']
			);
		}elseif($update_data['leave_type'] == 'Compensate-Leave'){
			$data = array(
				'compensate_leave' => $update_data['days']
			);
		}
    	$query = $db_query->update('staff_details', $data, array('employee_code' => $employee_code));
		//echo $db_query->last_query();
		return $query;
	}

	public function update_password() {
		$new_password = $this->input->post('new_password');
		$conf_password = $this->input->post('conf_password');
		$userid = $this->input->post('cur_user_id');
		$encrypted_pwd = password_hash($new_password, PASSWORD_BCRYPT);
		$this->db->where('userID', $userid);
		$this->db->update('login_details', array('password' => $encrypted_pwd));
		return true;		
	}

	public function update_details($id,$update_data) {
		
/* 		echo "<pre>"; print_r($update_data); echo "</pre>";
		die; */
		$database = '';
        $connectionString = $this->session->userdata("connectionString");
		$database = SelectDB($connectionString);
        $db_query = $this->load->database($database, TRUE);
		$db_query->where('id', $id);
		return $db_query->update('staff_details', $update_data);
    }
	
}