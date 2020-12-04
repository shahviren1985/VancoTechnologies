<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<style type="text/css">
  #divLoading {
    display : none;
  }
  #divLoading.show {
    display : block;
    position : fixed;
    z-index: 100;
    background-image : url('<?php echo base_url(); ?>assets/img/loader.gif');
    background-color: #000;
    opacity: 0.7;
    background-repeat : no-repeat;
    background-position : center;
    left : 0;
    bottom : 0;
    right : 0;
    top : 0;
  }
  #loadinggif.show {
    left : 50%;
    top : 50%;
    position : absolute;
    z-index : 101;
    width : 32px;
    height : 32px;
    margin-left : -16px;
    margin-top : -16px;
  }
  div.content {
    width : 1000px;
    height : 1000px;
  }
</style>
<div id="content-wrapper">
	<div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('officeadmin/home'); ?>">Dashboard</a>
      </li>
      <li class="breadcrumb-item active">Import Students Detail</li>
    </ol>
	
	
	<div id="msg"></div>

    <div class="card mb-3">
		<div class="card-header">
        <i class="fas fa-file-excel"></i>
        Upload Student Details</div>
		<div class="card-body">
			<form method="post" id="import_form" enctype="multipart/form-data">
				<p><label>Select File</label>
				<input type="file" name="file" id="file" required accept=".xls, .xlsx" /></p>
				<br />
				<input type="submit" name="import" value="Import" class="btn btn-primary" />
			</form>			
		</div>
		<div class="card-footer small text-muted">
			<form method="post" action="<?php echo base_url(); ?>export_user_details">
				<input type="submit" class="btn btn-primary" id="export_sample_file" value="Download Sample file" name="export_sample_file">
			</form>
		</div>
    </div>

    <!-- Image loader -->
    <div id='divLoading'></div>
    <!-- Image loader -->    

  </div>
  <!-- /.container-fluid -->

  <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
  <script>
    jQuery(document).ready(function(){

		//jQuery("div#divLoading").addClass('show');
		jQuery('#import_form').on('submit', function(event){
			event.preventDefault();
			jQuery.ajax({
				url: "<?php echo base_url(); ?>import_user_details",
				method: "POST",
				data: new FormData(this),
				contentType: false,
				cache: false,
				processData: false,
				beforeSend: function(){
					// Show image container
					jQuery("div#divLoading").addClass('show');
				},
				success:function(data){					
					jQuery('#file').val('');
					jQuery("div#divLoading").removeClass('show');
					jQuery('#msg').addClass('alert alert-success').html(data);
					//jQuery("#excel-btn").click();
					//jQuery(".modal-excel").text(data);
				}
			});
		});
    });
  </script>