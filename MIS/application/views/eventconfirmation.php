<?php defined('BASEPATH') OR exit('No direct script access allowed'); ?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Payment Confirmation</title>
</head>
<body>     
    <form action="<?php echo $action; ?>" method="post" id="payuForm" name="payuForm" style="display: none;">
		<input type="hidden" name="key" value="<?php echo $MERCHANT_KEY ?>" />
		<input type="hidden" name="productinfo" value="<?php echo htmlspecialchars($productinfo); ?>" />
		<input type="hidden" name="hash" value="<?php echo $hash ?>"/>
		<input type="hidden" name="txnid" value="<?php echo $txnid ?>" />
		<table>
 
        <tr>
          <td>TxnId: </td>
          <td><input name="txnid" id="txnid" value="<?php echo (empty($txnid)) ? '' : $txnid; ?>" />
          </td>
        </tr>
        <tr>
          <td>Amount: </td>
          <td><input name="amount" value="<?php echo (empty($amount)) ? '' : $amount ?>" /></td>
          <td>First Name: </td>
          <td><input name="firstname" id="firstname" value="<?php echo (empty($firstname)) ? '' : $firstname; ?>" /></td>
        </tr>
		<tr>
          <td>Last Name: </td>
          <td><input name="lastname" id="lastname" value="<?php echo (empty($lastname)) ? '' : $lastname; ?>" /></td>
          <td>Cancel URI: </td>
          <td><input name="curl" value="<?php echo (empty($cancel)) ? '' : $cancel; ?>" /></td>
        </tr>
        <tr>
          <td>Email: </td>
          <td><input name="email" id="email" value="<?php echo (empty($email)) ? '' : $email; ?>" /></td>
          <td>Phone: </td>
          <td><input name="phone" value="<?php echo (empty($phone)) ? '' : $phone; ?>" /></td>
        </tr>
        <tr>
          <td>Success URI: </td>
          <td colspan="3"><input name="surl" value="<?php echo (empty($success)) ? '' : $success ?>"/></td>
        </tr>
        <tr>
          <td>Failure URI: </td>
          <td colspan="3"><input name="furl" value="<?php echo (empty($failure)) ? '' : $failure ?>" /></td>
        </tr>

        <tr>
          <td colspan="3"><input type="hidden" name="service_provider" value="payu_paisa" size="64" /></td>
        </tr>
 
        <tr>
          <td>UDF1: </td>
          <td><input name="udf1" value="<?php echo (empty($udf1)) ? '' : $udf1; ?>" /></td>
          <td>UDF2: </td>
          <td><input name="udf2" value="<?php echo (empty($udf2)) ? '' : $udf2; ?>" /></td>
        </tr>
        <tr>
          <td>UDF3: </td>
          <td><input name="udf3" value="<?php echo (empty($udf3)) ? '' : $udf3; ?>" /></td>
          <td>UDF4: </td>
          <td><input name="udf4" value="<?php echo (empty($udf4)) ? '' : $udf4; ?>" /></td>
        </tr>
        <tr>
          <td>UDF5: </td>
          <td><input name="udf5" value="<?php echo (empty($udf5)) ? '' : $udf5; ?>" /></td>
        </tr>
        <tr>
          <?php if($hash) { ?>
            <td colspan="4"><input type="submit" value="Submit" /></td>
          <?php } ?>
        </tr>
      </table>
    </form>

</body>

<script>
	var payuForm = document.getElementById("payuForm");
    payuForm.submit();
</script>

</html>