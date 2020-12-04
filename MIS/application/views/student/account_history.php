<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
		<li class="breadcrumb-item">
			<a href="<?php echo base_url('student/home'); ?>">Home</a>
		</li>
		<li class="breadcrumb-item">Account History</li>
    </ol>

    <!-- DataTables Example -->
    <div class="card mb-3">
      <div class="card-header">
        <i class="fas fa-history"></i>
        View Account History 
      </div>
      <div class="card-body">
        <div class="table-responsive">
          <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0" style="text-transform:capitalize;">
            <thead>
              <tr>
                <th>Challan No.</th>
                <th>Date Paid</th>             
                <th>Amount</th>
                <th>Payment Mode</th>
                <th>Remark</th>
                <th>Status</th>
              </tr>
            </thead>          
			<?php if(!empty($transaction_details)){?>
				<tbody>
				<?php 
				foreach ($transaction_details as $transaction_detail) {?>
				<tr>
					<td><?php echo $transaction_detail->challan_number ?></td>
					<td><?php echo date('d-m-Y',strtotime($transaction_detail->fee_paid_date)); ?></td>
					<td><?php echo $transaction_detail->total_paid ?></td>
					<td><?php echo $transaction_detail->payment_mode ?></td>
					<td><?php echo $transaction_detail->remark ?></td>
					<?php 
					if($transaction_detail->transaction_status){
						$paid_status = $transaction_detail->transaction_status;
					} else {
						$paid_status = $transaction_detail->paid_status;
					}
					if($paid_status == 'Success' || $paid_status == 'success'){?>
						<td>
							<span class="label badge badge-success">
								<?php echo $paid_status;?>
							</span>
						</td>
					<?php
					} else {?>
						<td>
							<span class="badge badge-danger">
								<?php echo $paid_status;?>
							</span>
						</td> 
					<?php } ?>
					</tr>
				<?php } ?>              
				</tbody>
			<?php }else{ ?>
				<tbody>     
					<tr class="odd"><td valign="top" colspan="5" class="dataTables_empty">You haven't done any transactions yet</td></tr>
				</tbody>
			<?php } ?>
          </table>
        </div>
      </div>
      <div class="card-footer"></div>
    </div>
  </div>
  <!-- /.container-fluid -->
  