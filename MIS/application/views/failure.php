<?php defined('BASEPATH') OR exit('No direct script access allowed');?>

<div class="container mt-5">
<div class="row">
     <div class="col-md-12">
        <div class="card">
            <h4 class="card-header badge-danger">Fee Payment Failed. <i class="fas fa-times-circle"></i></h4>
            <div class="card-body">
                <?php 
				echo "<h4>Your payment status is ". $status .".</h4>";
				echo "<h6>Your transaction id for this transaction is ".$txnid.".</h6>";
				if(@$semester){
					echo "<h6>Semester. " . ucfirst($semester) . "</h6>";
					echo "<h6>Subject. " . $subject . "</h6>";
					echo "<h6>Specialization. " . $spec . "</h6>";
				}
				if(!empty($this->session->userdata("id"))):
					echo "<a href='".base_url().'student/home'."'><i class='fas fa-arrow-left'></i> Go Back & Try Again</a>";
				endif;?>
            </div>
        </div>
     </div> 
    <div class="col-md-2"></div>
</div>
</div> 
