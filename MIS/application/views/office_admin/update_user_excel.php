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
      <li class="breadcrumb-item active">Update Students Detail</li>
    </ol>
	
	
	<div id="msg"></div>

    <div class="card mb-3">
		<div class="card-header">
      <div class="row">
        <div class="col-sm-6 col-md-3">
          <i class="fas fa-file-excel"></i>
          Upload to Update Student
        </div>
        <div class="col-sm-6 col-md-9 text-right">
          <button type="button" data-toggle="modal" data-target="#downloadExcelModal" class="btn btn-primary">Download Student List</button>
        </div>
      </div>
        
    </div>
		<div class="card-body">
			<form method="post" id="import_update_form" enctype="multipart/form-data">
				<p><label>Select File</label>
				<input type="file" name="file" id="file" required accept=".xls, .xlsx" /></p>
				<br />
				<input type="submit" name="import" value="Import" class="btn btn-primary" />
			</form>			
		</div>
		<div class="card-footer small text-muted">
			<form method="post" action="<?php echo base_url(); ?>export_update_user_details">
				<input type="submit" class="btn btn-primary" id="export_sample_file" value="Download Sample file" name="export_sample_file">
			</form>
		</div>
    </div>

    <!-- Image loader -->
    <div id='divLoading'></div>
    <!-- Image loader -->    
    <div class="modal fade" id="downloadExcelModal" tabindex="-1" role="dialog" aria-labelledby="downloadExcelModal" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Download Course wise Student List</h5>
              <button class="close" type="button" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body">
            <form id="download_courseexceldf" method="post" action="<?php echo base_url(); ?>export_userRollno_excel">
              <div class="row"> 
                <div class="col-md-6">
                  <div class="form-group">
                    <label for="course_name" class="control-label">Course Name</label>
                    <select name="course_nameE" data-rule-required="true" id="course_nameE" class="form-control">
                      <option value="FY">FY</option>
                      <option value="SY">SY</option>
                      <option value="TY">TY</option>
                    </select>
                  </div>
                </div>
                <div class="col-md-12">
                  <div class="form-group">
                    <input type="submit" class="btn btn-primary" name="dw_exceldfd" id="dw_exceldf" value="Download">
                    <input type="button" class="btn btn-primary" name="remove_rollNo" id="remove_rollNo" value="Clean Roll Number">
                    <div class="print-error-msg text-center mt-3"></div>
                  </div>
                </div>          
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- /.container-fluid -->

