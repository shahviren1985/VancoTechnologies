<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class User_roles extends CI_Model {

	function __construct() {
        parent::__construct();
    }

    public function get_entries( $limit = 0 )
	{
		$query = $this->db->get('user_roles', $limit);
		return $query->result();
	}

	public function get_entry( $id )
	{
		$query = $this->db->get_where('user_roles', array('id' => $id));
		return $query->result();
	}

	public function update_entry($data, $id)
	{
		$query = $this->db->update('user_roles', $data, array('id' => $id));
		return $query;
	}

}

?>