<?php 
defined('BASEPATH') OR exit('No direct script access allowed'); 
	ob_start();
	header("Content-type: application/vnd.ms-excel");
	header("Content-Disposition: inline; filename=Reporting-file-".date('d-M-Y').".xls");
	if( isset($result) && count($result) ) { ?>
	<style>
	    .t-heading{ background-color: #3f96d8; color:#fff; }
	    .t-row { background-color: #def1ff; border-bottom:1px solid #dee2e6; }
	    .fs-16{ font-size:16px; }
	</style>
	<table border="0" class="table table-bordered table-responsive">
		<thead>
			<tr class="t-heading">
				<th class="fs-16">SerialNumber</th>
				<th class="fs-16">SeatNumber</th>
				<th class="fs-16">RollNumber</th>
				<th class="fs-16">College_Registration_No_</th>
				<th class="fs-16">LastName</th>
				<th class="fs-16">FirstName</th>
				<th class="fs-16">FatherName</th>				
			</tr>
		</thead>
		<tbody>
			<?php foreach($result as $row_data ){?>
				<tr class="t-row">
					<?php foreach($row_data as $key=>$value) {?>
						<td class="fs-16" align="center"><?php echo $value; ?></td>
					<?php }?>
				</tr>			
			<?php } ?>				
		</tbody>
	</table>
<?php ob_end_clean();
} ?>			