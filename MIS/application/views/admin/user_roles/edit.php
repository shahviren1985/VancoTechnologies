<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<div id="content-wrapper">
  <div class="container-fluid">

    <?php
      $role = $user_role;
      if (isset($role[0]->id) && !empty($role[0]->id)) {
    ?>
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
          Update user role
        </div>
        <div class="card-body">
          <form method="post" action="<?php echo base_url("admin/user/role/update"); ?>">
            <div class="form-row">
              <div class="col-md-4 mb-3">
                <label for="rolw">Role*</label>
                <?php echo $role_err = form_error('role'); ?>
                <input type="text" class="form-control <?php echo !empty($role_err)?'is-invalid':'';?>" name="role" placeholder="User Role" value="<?php echo $role[0]->role; ?>">
                <?php echo form_error('role', '<div class="invalid-feedback">', '</div>'); ?>
              </div>
            </div>
            <input type="hidden" name="id" value="<?php echo $role[0]->id; ?>">
            <input type="hidden" name="action" value="update">
            <button class="btn btn-success" type="submit">Update</button>
          </form>
        </div>
      </div>
    <?php    
      }else{
        redirect('admin/user/roles');
      }
    ?>

  </div>
</div>
<!-- /.container-fluid -->