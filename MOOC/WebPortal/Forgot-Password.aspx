<%@ Page Title="MOOC Academy - Forget password" Language="C#" MasterPageFile="~/Master/AnonymusMaster.master" AutoEventWireup="true" CodeFile="Forgot-Password.aspx.cs" Inherits="Forgot_Password" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="SyllbusContainer">
        <div>
            <ul class="breadcrumb">
                <li><a href="Default.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li>
                    <div id="divBreadCrumb">Forgot Password</div>
                </li>
            </ul>
        </div>
        <div style="float: left; width: 100%" id="frameComtainer">
            <div class="ErrorContainer" style="display: none; width: 98%;">
            </div>
            <div id="radio-container">
                <input id="rbEmailPas" type="radio" name="Password" value="rbEmailPas" checked="checked" /><label for="rbEmailPas">Email me my password</label>
                <input id="rbShowPas" type="radio" name="Password" value="rbShowPas" /><label for="rbShowPas">Show my password</label>
            </div>
            <div id="email-mode" class="fields-container">
                <div>
                    <div>
                        <input type="text" id="txtUserName" placeholder="Enter you email address" style="width: 25%;" />
                    </div>
                    <div>
                        <div class="btn btn-success" id="btnGetPassword">Email Password</div>
                        <div id="sendingMail" class="btn btn-success" style="display: none;">Sending password... please wait</div>
                    </div>
                </div>
            </div>
            <div id="quiz-mode" class="fields-container" style="display: none">
                <div>
                    <input type="text" id="txtUname" placeholder="User name" style="width: 25%;" />
                </div>
                <div>
                    <input type="text" id="txtFname" placeholder="First name" style="width: 25%;" />
                </div>
                <div>
                    <input type="text" id="txtLname" placeholder="Last name" style="width: 25%;" />
                </div>
                <div>
                    <input type="text" id="txtFatherName" placeholder="Father name" style="width: 25%;" />
                </div>
                <div>
                    <input type="text" id="txtMotherName" placeholder="Mother name" style="width: 25%;" />
                </div>
                <div>
                    <input type="text" id="txtMobileNo" placeholder="Mobile number" style="width: 25%;" />
                </div>
                <div>
                    <div class="btn btn-success" id="btnShowPassword">Show Password</div>
                    <div id="sendingText" class="btn btn-success" style="display: none;">Getting password... please wait</div>
                </div>
            </div>
        </div>
        <style type="text/css">
            #radio-container label {
                margin-left: 7px;
                float: left;
                margin-right: 30px;
            }

            #radio-container input[type="radio"] {
                float: left;
            }

            .fields-container {
                width: 100%;
                float: left;
                margin-top: 15px;
            }
        </style>
        <script type="text/javascript">
            $(".search-container").hide();

            $(document).ready(function () {
                $("#radio-container").find("input[type='radio']").each(function () {
                    $(this).click(function () {
                        $(".ErrorContainer").hide();
                        if ($(this).prop("checked") == true) {
                            if ($(this).attr("value") == "rbEmailPas") {
                                $("#email-mode").show();
                                $("#quiz-mode").hide();
                            }
                            else if ($(this).attr("value") == "rbShowPas") {
                                $("#email-mode").hide();
                                $("#quiz-mode").show();
                            }
                        }
                    });
                });

                // email password
                $("#btnGetPassword").click(function () {
                    SendPasswordViaEmail();
                });

                // show password
                $("#btnShowPassword").click(function () {
                    ShowPassword();
                });
            });


            function SendPasswordViaEmail() {
                if (ValidateEmailPassword()) {

                    var userName = encodeURI($("#txtUserName").val());

                    $("#btnGetPassword").hide();
                    $("#sendingMail").show();

                    var path = "Handler/ForgetPassword.ashx?t=email&un=" + userName;

                    CallHandler(path, function (data) {
                        $("#btnGetPassword").show();
                        $("#sendingMail").hide();

                        if (data.Status == "Ok") {
                            $(".ErrorContainer").attr("style", "background-color:lightgreen");
                            $(".ErrorContainer").show();
                            $(".ErrorContainer").html(data.Message);

                            $("#txtUserName").val("");
                        }
                        else {
                            $(".ErrorContainer").html(data.Message);
                            $(".ErrorContainer").show();
                        }
                    })
                }
                else {
                    $(".ErrorContainer").show();
                }
            }

            function ShowPassword() {

                if (ValidateShowPassword()) {
                    var userName = encodeURI($("#txtUname").val());
                    var fName = encodeURI($("#txtFname").val());
                    var lName = encodeURI($("#txtLname").val());
                    var father = encodeURI($("#txtFatherName").val());
                    var mother = encodeURI($("#txtMotherName").val());
                    var mobile = encodeURI($("#txtMobileNo").val());

                    $("#btnShowPassword").hide();
                    $("#sendingText").show();

                    var path = "Handler/ForgetPassword.ashx?t=show&un=" + userName + "&fn=" + fName + "&ln=" + lName + "&f=" + father + "&m=" + mother + "&mn=" + mobile;

                    CallHandler(path, function (data) {
                        $("#btnShowPassword").show();
                        $("#sendingText").hide();

                        if (data.Status == "Ok") {
                            $(".ErrorContainer").attr("style", "background-color:lightgreen");
                            $(".ErrorContainer").show();
                            $(".ErrorContainer").html(data.Message);

                            $("#txtUname").val("");
                            $("#txtFname").val("");
                            $("#txtLname").val("");
                            $("#txtFatherName").val("");
                            $("#txtMotherName").val("");
                            $("#txtMobileNo").val("");
                        }
                        else {
                            $(".ErrorContainer").html(data.Message);
                            $(".ErrorContainer").show();
                        }
                    })
                }
                else {
                    $(".ErrorContainer").show();
                }
            }

            function ValidateEmailPassword() {
                if ($("#txtUserName").val() == "") {
                    $(".ErrorContainer").html("Please enter your email address");
                    return false;
                }

                return true;
            }

            function ValidateShowPassword() {
                if ($("#txtUname").val() == "") {
                    $(".ErrorContainer").html("Please enter your username");
                    return false;
                }
                if ($("#txtFname").val() == "") {
                    $(".ErrorContainer").html("Please enter your first name");
                    return false;
                }
                if ($("#txtLname").val() == "") {
                    $(".ErrorContainer").html("Please enter your last name");
                    return false;
                }
                if ($("#txtFatherName").val() == "") {
                    $(".ErrorContainer").html("Please enter your father name");
                    return false;
                }
                if ($("#txtMotherName").val() == "") {
                    $(".ErrorContainer").html("Please enter your mother name");
                    return false;
                }
                if ($("#txtMobileNo").val() == "") {
                    $(".ErrorContainer").html("Please enter your mobile number");
                    return false;
                }

                return true;
            }
        </script>
</asp:Content>

