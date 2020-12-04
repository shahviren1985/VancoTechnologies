<?php require APPPATH . 'libraries/REST_Controller.php';
class Api extends REST_Controller {   
	/**
	* Get All Data from this method.
	*
	* @return Response
	*/

    public function __construct() {
		parent::__construct();
		//	header('Access-Control-Allow-Origin: *');
		//	header("Access-Control-Allow-Methods: GET, POST, OPTIONS, PUT, DELETE");
		$this->load->model('Api_model');
		$this->load->helper('select_db');
		//$this->load->library('excel');
		$this->load->library('email');
		//$this->load->helper('download');
    }   

    /**
     * Get All Data from this method.
     *
     * @return Response
    */

	public function students_get(){	
		$year = $this->get('year');		
		$action = $this->get('action');
		if($action == 'view_spec') {
			$data = $this->Api_model->get_specialization_result($year);
			if($data){
				$this->response($data, 200); // 200 being the HTTP response code
			} else {
				$this->response('No result found.', 404);
			}
		} else {
			$this->response('Invalid Request', 404);
		}			
	}
	
	public function students_caste_get(){
		$year = $this->get('year');	
		$data = $this->Api_model->get_caste_result($year);		
        if($data){
            $this->response($data, 200); // 200 being the HTTP response code
        } else {
            $this->response(NULL, 404);
        }
	}

	
	public function update_students_get(){
		$userId = $this->get('crn');
		$library_card_no = $this->get('lcn');
		$feedback_received = $this->get('fr');
		$action = $this->get('action');
		$data=array();
		$result=0;
		//die('ak');
		if($action == 'updatestudent') {
			if (!empty($userId)) {
				if(!empty($feedback_received) && !empty($feedback_received)) {
				$data = array(
					'library_card_no' => $library_card_no,
					'feedback_received' => $feedback_received
				);
				$result = $this->Api_model->update_into_user_details($userId,$data);
				}elseif(!empty($feedback_received)) {
					$data = array(
						'feedback_received' => $feedback_received
					);
					$result = $this->Api_model->update_into_user_details($userId,$data);
				} elseif(!empty($library_card_no)) {
					$data = array(
						'library_card_no' => $library_card_no
					);
					$result = $this->Api_model->update_into_user_details($userId,$data);
				}
				// echo "<pre>"; print_r($result); die;
				if($result){
					//$this->response($result, 200); // 200 being the HTTP response code
					$this->response('Data updated successfully.', 200);
				} else {
					$this->response('No data to update.', 404);
				}
			}else {
				$this->response('User Id required.', 404);
			}
			
		} else {
			$this->response('Invalid Request', 404);
		}			
	}		
}