<?php defined('BASEPATH') OR exit('No direct script access allowed');?>
<div class="container mt-5">
	<div class="row"> 
        <div class="col-md-12">
        	<div class="card">
        		<h4 class="card-header badge-success">
                    Fee Payment Successful. <i class="fas fa-check-circle"></i>
                </h4>
        		<div class="card-body">
        			<?php
					echo "<h4>Thank You. Your payment status is ". $status .".</h4>";
					echo "<h5>An email is sent to your email address.</h5>";
					echo "<h6>Your Transaction ID for this transaction is ".$txnid.".</h6>";
					echo "<h6>We have received a payment of Rs. " . $amount . "</h6>";
					if(@$semester){
						echo "<h6>Semester. " . ucfirst($semester) . "</h6>";
						echo "<h6>Subject. " . $subject . "</h6>";
						echo "<h6>Specialization. " . $spec . "</h6>";
					}
                    if(!empty($this->session->userdata("id"))):?>
                    <p class="mt-5">
                        <a href="<?php echo base_url().'student/account/history'; ?>">Click here to see your account history.</a>
                    </p>
					<?php endif;?>
                     <?php  if(!empty($pdf_download)):?>
                        <p class="mt-5">
                            <a href="<?php echo $pdf_download; ?>">Click here to download Fee receipt.</a>
                        </p>
                    <?php endif;?>
        		</div>
        	</div>
         </div>
    </div>
</div>