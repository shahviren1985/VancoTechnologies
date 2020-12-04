<?php
defined('BASEPATH') OR exit('No direct script access allowed');

// require_once APPPATH . "/third_party/phpexcel/PHPExcel.php";
require_once (APPPATH . "/third_party/phpexcel/Classes/PHPExcel.php");
class Excel extends PHPExcel {
    public function __construct() {
        parent::__construct();
    }
}

?>