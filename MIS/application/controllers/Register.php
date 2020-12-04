<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Register extends CI_Controller {

	public function __construct()
	{
		parent::__construct();
		$this->load->model('user');
        $this->load->library('session');
		$this->load->library('form_validation');
	}

	public function index()
	{
		$this->load->view('register');
	}

	public function register_action()
	{
		$this->form_validation->set_rules('firstName', 'First Name', 'trim|required|min_length[3]');
		$this->form_validation->set_rules('lastName', 'Last Name', 'trim|required|min_length[3]');
		$this->form_validation->set_rules('inputEmail', 'Email ID', 'trim|required|valid_email');
		$this->form_validation->set_rules('inputPassword', 'Password', 'trim|required|min_length[8]');
		$this->form_validation->set_rules('confirmPassword', 'Password Confirmation', 'trim|required|matches[inputPassword]');

		//validate form input
        if ($this->form_validation->run() == FALSE)
        {
            // fails
            $this->load->view('register');
        }
        else
        {
            //insert the user registration details into database
            $data = array(
                'first_name' => trim($this->input->post('firstName')),
                'last_name' => trim($this->input->post('lastName')),
                'email' => trim($this->input->post('inputEmail')),
                'password' => password_hash(trim($this->input->post('inputPassword')), PASSWORD_BCRYPT)
            );
            //print_r($data);
            //exit;
            // insert form data into database
            // if ($this->user->registerUser($data))
            // {

            $result = $this->user->registerUser($data);

            //print_r($result); exit;
                // successfully sent mail
                $this->session->set_flashdata('msg','<div class="alert alert-success text-center">You are Successfully Registered! </div>');
                    redirect('register');
            // }
            // else
            // {
            //     // error
            //     $this->session->set_flashdata('msg','<div class="alert alert-danger text-center">Oops! Error.  Please try again later!!!</div>');
            //         redirect('register');
            // }
        }
		
	}
}
