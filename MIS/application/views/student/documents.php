<?php defined('BASEPATH') OR exit('No direct script access allowed');
/* echo "<pre>"; print_r($user_details); echo "</pre>";
die; */
 ?>
<div id="content-wrapper">
	<div class="container-fluid">
		
		<!-- Breadcrumbs-->
		<ol class="breadcrumb">
			<li class="breadcrumb-item">
				<a href="<?php echo base_url('student/home'); ?>">Home</a>
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
										<option value="Marksheet">Marksheet</option>
										<option value="Domicile-Certificate">Domicile Certificate</option>
										<option value="Birth Certificate">Birth Certificate</option>
										<option value="Bonafide Certificate">Bonafide Certificate</option>
										<option value="Passing-Certificate">Passing Certificate</option>
										<option value="Eligibility-Certificate">Eligibility Certificate</option>
										<option value="Caste-Certificate">Caste Certificate</option>
										<option value="Disability-Certificate">Disability Certificate</option>
										<option value="Ration-Card">Ration Card</option>
										<option value="Transfer-Certificate">Transfer Certificate</option>
										<option value="Aadhar-Card">Aadhar Card</option>
										<option value="Pan-Card">PAN Card</option>
										<option value="Voter-Card">Voter Card</option>
										<option value="Migration-Certificate">Migration Certificate</option>
										<option value="Name-Change-Certificate">Name Change Certificate</option>
										<option value="Internship-Certificate">Internship Certificate</option>
										<option value="LC">LC</option>
										<option value="Sports-Certificate">Sports Certificate</option>
										<option value="Bank-Statement">Bank Statement</option>
										<option value="Passbook">Passbook</option>
										<option value="Gap-Certificate">Gap Certificate</option>
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
									<input type="hidden" name="student_academic_year" id="student_academic_year" value="<?php echo $user_details[0]->academic_year; ?>">
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
						<?php $documents = @file_get_contents('https://vancotech.com/dms/api/GetDocuments.ashx?admissionYear='.$user_details[0]->academic_year.'&crn='.$this->session->userdata('userID'));
						$docs = json_decode($documents);						
						if($docs):
							echo '<div class="list_files row">';
							foreach($docs as $doc){
								$doc_array = explode('/',$doc);?>
								<div class="col-md-2">
									<a href="https://vancotech.com/dms/api/DownloadDocument.ashx?admissionYear=<?php echo $user_details[0]->academic_year; ?>&crn=<?php echo $this->session->userdata('userID');?>&p=<?php echo $doc;?>"><i class="fas fa-file-pdf"></i> <?php if ($doc_array[0]=='Fee-Receipt') {echo $doc;}else{echo $doc_array[0];} ?>									
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