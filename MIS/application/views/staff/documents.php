<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('staff/staffs'); ?>">Home</a>
			</li>
			<li class="breadcrumb-item">Documents</li>
		</ol>
		
		<div class="uploadMessage"></div>		 
		
		<div class="accordion" id="accordionExample">
			<div class="card">
				<div class="card-header" id="uploadDoc">
					<a data-toggle="collapse" data-target="#uploadDocCont" aria-expanded="true" aria-controls="uploadDocCont">
						<i class="fas fa-folder-open"></i> Upload New Document
						<span class="toggleCard"><i class="fas fa-sort-down"></i></span>
					</a>
				</div>
				<div id="uploadDocCont" class="collapse" aria-labelledby="uploadDoc" data-parent="#accordionExample">
					<div class="card-body">
						<form method="post" class="form-horizontal upload_new_doc" id="upload_new_doc" enctype="multipart/form-data"> 
							<div class="form-group">
								<label class="control-label col-sm-6" for="doc_type">Document Type:</label>
								<div class="col-sm-6">
									<select name="doc_type" id="doc_type" class="form-control" data-rule-required="true">
										<option value="">--Choose--</option>
										<option value="Appointment-Letter">Appointment Letter</option>
										<option value="Eligibility-Certificate">Eligibility Certificate</option>
										<option value="SSC-Marksheet">SSC Marksheet</option>
										<option value="HSC-Marksheet">HSC Marksheet</option>
										<option value="Graduation-Marksheet">Graduation Marksheet</option>
										<option value="Post-Graduate-Marksheet">Post Graduate Marksheet</option>
										<option value="PhD-Marksheet">PhD Marksheet</option>
										<option value="Degree-Certificate">Degree Certificate</option>
										<option value="Masters-Certificate">Masters Certificate</option>
										<option value="Fixation">Fixation</option>
										<option value="Seminar-Certificate">Seminar Certificate</option>
										<option value="Caste-Certificate">Caste Certificate</option>
										<option value="Caste-Validity">Caste Validity</option>
										<option value="Aadhar-Card">Aadhar Card</option>
										<option value="PAN-Card">PAN Card</option>
										<option value="Cancelled-Cheque">Cancelled Cheque</option>
										<option value="Marriage-Certificate">Marriage-Certificate</option>
										<option value="Employee-Provident-Fund">Employee Provident Fund</option>
										<option value="Confirmation Letter">Confirmation Letter</option>
									</select>
								</div>
							</div>
							<div class="form-group">
								<label class="control-label col-sm-6" for="file">Upload Document:</label>
								<div class="col-sm-6"> 
									<input type="file" class="form-control" name="doc" id="doc" data-rule-required="true"/>
								</div>
							</div>
							<!--<div class="form-group"> 
								<label class="control-label col-sm-6" for="file">Document Title:</label>
								<div class="col-sm-6"> 
									<input type="text" class="form-control" data-rule-required="true" name="doc_name" id="doc_name">
								</div>
							</div>-->
							<div class="form-group"> 
								<div class="col-sm-offset-2 col-sm-10">
									<input type="submit" class="btn btn-success" value="Upload"/>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
			<div class="card">
				<div class="card-header" id="prevuploadDoc">
					<a data-toggle="collapse" data-target="#prevuploadCont" aria-expanded="true" aria-controls="prevuploadCont">
						<i class="fas fa-folder-open"></i> Documents
						<span class="toggleCard"><i class="fas fa-sort-down"></i></span>
					</a>
				</div>
				<div id="prevuploadCont" class="collapse show" aria-labelledby="prevuploadDoc" data-parent="#accordionExample">
					<div class="card-body">
						<?php $documents = @file_get_contents('https://vancotech.com/dms/api/GetDocuments.ashx?admissionYear=staff&crn='.$this->session->userdata('userID'));
						$docs = json_decode($documents);						
						if($docs):
							echo '<div class="list_files row">';
							foreach($docs as $doc){
								$doc_array = explode('/',$doc);?>
								<div class="col-md-2">
									<a href="https://vancotech.com/dms/api/DownloadDocument.ashx?admissionYear=staff&crn=<?php echo $this->session->userdata('userID');?>&p=<?php echo $doc;?>"><i class="fas fa-file-pdf"></i> <?php echo $doc_array[0];?>									
									</a>
								</div>
								<?php
							}
							echo '</div>';
						endif;?>													
					</div>
				</div>
			</div>
		</div>
	</div>
<!-- /.container-fluid -->  