<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <a href="<?php echo base_url('admin/home'); ?>">Dashboard</a>
      </li>
      <li class="breadcrumb-item active">User Roles</li>
    </ol>

    <!-- DataTables Example -->
    <div class="card mb-3">
      <div class="card-header">
        <i class="fas fa-user"></i>
        View user roles
      </div>
      <div class="card-body">
        <div class="table-responsive">
          <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0" style="text-transform:capitalize;">
            <thead>
              <tr>
                <th>S. No.</th>
                <th>Role</th>             
                <th>Action</th>
              </tr>
            </thead>          
            <tbody>
              <?php if( isset($user_roles) && count($user_roles) > 0 ) { ?>
                <?php 
                  $count = 0;
                  foreach( $user_roles as $key => $user_role ) {
                  $count++; 
                ?>
                  <tr>
                    <td><?php echo $count; ?></td>
                    <td><?php echo $user_role->role; ?></td>
                    <td>
                      <a class="btn btn-info btn-md" href="<?php echo base_url("admin/user/role/edit/".$user_roles[$key]->id); ?>" role="button">Edit</a>
                      <!-- <a class="btn btn-danger btn-sm delete-transport" href="javascript:void(0);" data-ref="<?php //echo base_url("admin/transport/delete/".$transport->id); ?>" role="button">Delete</a> -->
                    </td>
                  </tr>
                <?php } ?>
              <?php } ?>
            </tbody>
          </table>
        </div>
      </div>
      <div class="card-footer">
        <?php echo $this->session->tempdata('message'); ?>
      </div>
    </div>


            

  </div>
  <!-- /.container-fluid -->