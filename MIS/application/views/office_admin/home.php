<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">
        <div class="card mb-3">
			<div class="card-header">
				<i class="fas fa-list"></i>
				Summary List 
			</div>
			<div class="card-body">
				<div class="row summary_table">				
					<div class="col-md-7">
						<table class="table table-bordered" width="100%" id="specializationTable" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Specialization</th>
									<th class="fs-16">FY</th>
									<th class="fs-16">SY</th>
									<th class="fs-16">TY</th>
									<th class="fs-16">Total</th>
								</tr>
							</thead>							
						</table>
						
						<table class="table table-bordered" width="100%" id="casteTable" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Caste</th>
									<th class="fs-16">FY</th>
									<th class="fs-16">SY</th>
									<th class="fs-16">TY</th>
									<th class="fs-16">Total</th>
								</tr>
							</thead>							
						</table>
					</div>
					
					<div class="col-md-5">
						<?php
						$total_amount = 0;
						if($todays_transaction){							
							foreach($todays_transaction as $transaction){
								$total_amount += $transaction->total_amount;
							}
						}?>
						<h5>Today's Transaction</h5>						
						<table class="table table-bordered" width="100%" id="transactionTable" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Transactions</th>
									<th class="fs-16">Transaction Amount</th>									
								</tr>
							</thead>
							<tbody>
								<tr>
									<td><?php echo count($todays_transaction);?></td>
									<td>&#x20b9; <?php echo number_format($total_amount, 2); ?></td>						
								</tr>
							</tbody>
						</table>
						
						<h5>Recent Transactions</h5>						
						<?php if(!empty($recent_transaction[0])):?>
						<table class="table table-bordered" width="100%" id="transactionTable" cellspacing="0" style="text-transform:capitalize;">
							<thead>
								<tr class="t-heading">
									<th class="fs-16">Name</th>
									<th class="fs-16">Challan No.</th>
									<th class="fs-16">Amount</th>
									<th class="fs-16">Date</th>
								</tr>
							</thead>
							<tbody>
								<?php foreach($recent_transaction as $rcnt_trans):?>
								<tr>
									<td><?php echo $rcnt_trans->firstname.' '.$rcnt_trans->lastname;?></td>
									<td><?php echo $rcnt_trans->challan_number;?></td>
									<td>&#x20b9; <?php echo number_format($rcnt_trans->total_amount, 2); ?></td>
									<td><?php echo date('d-m-Y',strtotime($rcnt_trans->fee_paid_date));?></td>
								</tr>
								<?php endforeach;?>
							</tbody>
						</table>
						<?php else:?>
						No transaction found
						<?php endif;?>
					</div>					
				</div>
			</div>
		</div>
	</div>
	<!-- /.container-fluid --> 