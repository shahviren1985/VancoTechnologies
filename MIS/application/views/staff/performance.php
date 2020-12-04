<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('student/home'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Academic Performance</li>
		</ol>
			 		
		<div class="card">
			<div class="card-header" id="parent_feedback">
				Academic Performance
			</div>
			<div class="card-body">
				<div style="width:75%;">
					<canvas id="performanceChart"></canvas>
				</div>
			</div>
			<div class="semster-tabs">
				<div class="card-header">	
					<ul class="nav nav-tabs card-header-tabs" role="tablist">
						<li class="nav-item active"><a id="sem1-link" class="nav-link" data-toggle="tab" href="#sem1">Sem1</a></li>
						<li class="nav-item"><a id="sem2-link" class="nav-link" data-toggle="tab" href="#sem2">Sem2</a></li>
						<li class="nav-item"><a id="sem3-link" class="nav-link" data-toggle="tab" href="#sem3">Sem3</a></li>
						<li class="nav-item"><a id="sem4-link" class="nav-link" data-toggle="tab" href="#sem4">Sem4</a></li>
						<li class="nav-item"><a id="sem5-link" class="nav-link" data-toggle="tab" href="#sem5">Sem5</a></li>
						<li class="nav-item"><a id="sem6-link" class="nav-link"data-toggle="tab" href="#sem6">Sem6</a></li>
					</ul>
				</div>
				<div class="tab-content">
					<div id="sem1" class="tab-pane fade in active show">
						<div class="card-body">
							<canvas id="graphcanvas1"></canvas>
						</div>
					</div>
					<div id="sem2" class="tab-pane fade">
						<div class="card-body">
							<canvas id="graphcanvas2"></canvas>
						</div>
					</div>
					<div id="sem3" class="tab-pane fade">
						<div class="card-body">
							<canvas id="graphcanvas3"></canvas>
						</div>
					</div>
					<div id="sem4" class="tab-pane fade">
						<div class="card-body">
							<canvas id="graphcanvas4"></canvas>
						</div>
					</div>
					<div id="sem5" class="tab-pane fade">
						<div class="card-body">
							<canvas id="graphcanvas5"></canvas>
						</div>
					</div>
					<div id="sem6" class="tab-pane fade">
						<div class="card-body">
							<canvas id="graphcanvas6"></canvas>
						</div>
					</div>
				</div>
			</div>
		</div>	
	</div>
<!-- /.container-fluid -->  