<%@ Page Language="C#" MasterPageFile="~/Master/AnonymusMaster.master" AutoEventWireup="true" CodeFile="Contact-Us.aspx.cs" Inherits="Contact_Us"
    Title="MOOC Academy - Contact Us" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="SyllbusContainer">
        <div>
            <ul class="breadcrumb">
                <li><a href="Default.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li>
                    <div id="divBreadCrumb">Contact Us</div>
                </li>
            </ul>
        </div>
        <div style="float: left; width: 100%" id="frameComtainer">
            <div class="ErrorContainer" style="display: none;">
            </div>
            <div>
                <input type="text" id="txtname" placeholder="Enter your Name" style="width: 50%;" />
            </div>
            <div>
                <input type="text" id="txtMobile" placeholder="Enter your Mobile No" style="width: 50%;" />
            </div>
            <div>
                <input type="text" id="txtEmail" placeholder="Enter your Email" style="width: 50%;" />
            </div>
            <div>
                <input type="text" id="txtCity" placeholder="Enter your City" style="width: 50%;" />
            </div>
            <div>
                <textarea id="txtQuery" placeholder="Enter your Query" style="width: 50%; min-height: 160px"></textarea>
            </div>
            <div>
                <div class="btn btn-success" id="btnSend">Send</div>
                <div id="sendingText" class="btn btn-success" style="display:none;">Sending... please wait</div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(".search-container").hide();

        $("#btnSend").click(function () {
            Send();
        });

        function Send() {
            if (Validate()) {
                $(".ErrorContainer").hide();

                var name = encodeURI($("#txtname").val());
                var mobile = encodeURI($("#txtMobile").val());
                var email = encodeURI($("#txtEmail").val());
                var city = encodeURI($("#txtCity").val());
                var query = encodeURI($("#txtQuery").val());                

                //send contact us mail
                $("#btnSend").hide();
                $("#sendingText").show();

                var path = "Handler/SendContactUsMail.ashx?name=" + name + "&mobileNo=" + mobile + "&query=" + query + "&city=" + city + "&email=" + email;

                CallHandler(path, function (data) {
                    $("#btnSend").show();
                    $("#sendingText").hide();

                    if (data.Status == "Ok") {
                        $(".ErrorContainer").attr("style", "background-color:lightgreen");
                        $(".ErrorContainer").show();
                        $(".ErrorContainer").html("Your query sent successfully. Our team will contact to you soon.");

                        $("#txtname").val("");  
                        $("#txtMobile").val("");
                        $("#txtEmail").val("");
                        $("#txtCity").val("");
                        $("#txtQuery").val("");
                    }
                    else {
                        $(".ErrorContainer").html("Unble to send your query. Please try again after some time");
                        $(".ErrorContainer").show();
                    }
                })
            }
            else {
                $(".ErrorContainer").show();
            }
        }

        function Validate() {
            var name = $("#txtname").val();
            var mobile = $("#txtMobile").val();
            var email = $("#txtEmail").val();
            var query = $("#txtQuery").val();
            var city = $("#txtCity").val();

            if (name == "") {
                $(".ErrorContainer").html("Please enter name");
                return false;
            }

            if (mobile == "") {
                $(".ErrorContainer").html("Please enter mobile number");
                return false;
            }
            else {
                if (!isNumber(mobile) || mobile.length != 10) {
                    $(".ErrorContainer").html("Please enter valid mobile number");
                    return false;
                }
            }

            if (email == "") {
                $(".ErrorContainer").html("Please enter email");
                return false;
            }
            else {
                if (!validateEmail(email)) {
                    $(".ErrorContainer").html("Please enter valid email");
                    return false;
                }
            }

            if (city == "") {
                $(".ErrorContainer").html("Please enter city");
                return false;
            }

            if (query == "") {
                $(".ErrorContainer").html("Please enter your query");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

