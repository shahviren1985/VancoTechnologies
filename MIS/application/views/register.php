<?php
defined('BASEPATH') OR exit('No direct script access allowed');
?>
<!DOCTYPE html>
<html lang="en">

  <head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Register</title>

    <!-- Bootstrap core CSS-->
    <link href="<?php echo base_url('assets/vendor/bootstrap/css/bootstrap.min.css'); ?>" rel="stylesheet">

    <!-- Custom fonts for this template-->
    <link href="<?php echo base_url('assets//fontawesome-free/css/all.min.css" rel="stylesheet'); ?>" type="text/css">

    <!-- Custom styles for this template-->
    <link href="<?php echo base_url('assets/css/sb-admin.css" rel="stylesheet'); ?>">
  </head>

  <body class="bg-dark">

    <div class="container">
      <div class="card card-register mx-auto mt-5">
        <div class="card-header">Register an Account</div>
        <div class="card-body">
          <form action="user_register" method="post" autocomplete='off'>
            <div class="form-group">
              <div class="form-row">
                <div class="col-md-6">
                  <div class="form-label-group">
                    <input type="text" id="firstName" name="firstName" class="form-control" value="" placeholder="First name">
                    <label for="firstName">First name</label>
                    <span class="text-danger"><?php echo form_error('firstName'); ?></span>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-label-group">
                    <input type="text" id="lastName" name="lastName" class="form-control" value="" placeholder="Last name">
                    <label for="lastName">Last name</label>
                    <span class="text-danger"><?php echo form_error('lastName'); ?></span>
                  </div>
                </div>
              </div>
            </div>
            <div class="form-group">
              <div class="form-label-group">
                <input type="email" id="inputEmail" name="inputEmail" class="form-control" value="" placeholder="Email address">
                <label for="inputEmail">Email address</label>
                <span class="text-danger"><?php echo form_error('inputEmail'); ?></span>
              </div>
            </div>
            <div class="form-group">
              <div class="form-row">
                <div class="col-md-6">
                  <div class="form-label-group">
                    <input type="password" id="inputPassword" name="inputPassword" class="form-control" value="" placeholder="Password">
                    <label for="inputPassword">Password</label>
                    <span class="text-danger"><?php echo form_error('inputPassword'); ?></span>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-label-group">
                    <input type="password" id="confirmPassword" name="confirmPassword" class="form-control" value="" placeholder="Confirm password">
                    <label for="confirmPassword">Confirm password</label>
                    <span class="text-danger"><?php echo form_error('confirmPassword'); ?></span>
                  </div>
                </div>
              </div>
            </div>

            <button class="btn btn-primary btn-block">Register</button>
          </form>
          <div class="text-center">
            <a class="d-block small mt-3" href="<?php echo base_url(); ?>">Login Page</a>
          </div>
          <?php if(isset($_SESSION['msg'])){ echo $_SESSION['msg']; } ?>
        </div>
      </div>
    </div>

    <!-- Bootstrap core JavaScript-->
    <script src="<?php echo base_url('assets/vendor/jquery/jquery.min.js'); ?>"></script>
    <script src="<?php echo base_url('assets/vendor/bootstrap/js/bootstrap.bundle.min.js'); ?>"></script>

    <!-- Core plugin JavaScript-->
    <script src="<?php echo base_url('assets/vendor/jquery-easing/jquery.easing.min.js'); ?>"></script>

  </body>

</html>
