<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Codeigniter mail templates</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
	<div style="padding:20px;">
		<div style="background:#fff;border:1px solid #ddd;padding:20px;">
		<div style="font-size: 26px;font-weight: 700;letter-spacing: -0.02em;line-height: 32px;color: #41637e;font-family: sans-serif;text-align: center" align="center" id="emb-email-header">
			<img src="<?php echo base_url('assets/img/logo.png');?>" alt="" width="152">
		</div>		
		<?php if(!empty($todays_transaction[0])):?>		
			<table width="100%" border="1" cellpadding="0" cellspacing="0" style="text-align:left;">
				<thead>
					<tr>
						<th style="padding:5px 10px;">NAME</th>
						<th style="padding:5px 10px;">CHALLAN NO.</th>
						<th style="padding:5px 10px;">PAYMENT MODE</th>
						<th style="padding:5px 10px;">TRANSACTION NUMBER</th>
						<th style="padding:5px 10px;">TRANSACTION STATUS</th>
						<th style="padding:5px 10px;">AMOUNT</th>
						<th style="padding:5px 10px;">DATE</th>
					</tr>
				</thead>
				<tbody>
				<?php foreach($todays_transaction as $transaction):?>
					<tr>
						<td style="padding:5px 10px;"><?php echo $transaction->firstname.' '.$transaction->lastname;?></td>
						<td style="padding:5px 10px;"><?php echo $transaction->challan_number;?></td>
						<td style="padding:5px 10px;"><?php echo $transaction->payment_mode;?></td>
						<td style="padding:5px 10px;"><?php echo (empty($transaction->transaction_ref_number)) ? '-' : $transaction->transaction_ref_number;?></td>
						<td style="padding:5px 10px;"><?php echo $transaction->transaction_status;?></td>
						<td style="padding:5px 10px;">&#x20b9; <?php echo number_format($transaction->total_amount, 2);?></td>
						<td style="padding:5px 10px;"><?php echo date('d/m/Y',strtotime($transaction->fee_paid_date));?></td>
					</tr>
				<?php endforeach;?>
				</tbody>
			</table>	
		<?php endif;?>
		</div>
	</div>
</body>
</html>