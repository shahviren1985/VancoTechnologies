<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="Documents.aspx.cs" Inherits="Documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System</title>
    <link href="static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="static/stylesheets/bootstrap.css" rel="stylesheet" />
    <link href="static/stylesheets/styleSheet.css" rel="stylesheet" />
    <link href="static/stylesheets/aa.group.css" rel="stylesheet" />
    <link href="static/stylesheets/aa.comment.css" rel="stylesheet" />
    <link href="static/stylesheets/jquery.tagit.css" rel="stylesheet" type="text/css" />
    <link href="static/stylesheets/tagit.ui-zendesk.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="static/stylesheets/reset.css" type="text/css" />
    <link rel="stylesheet" href="static/stylesheets/style.css" type="text/css" />


    <script src="static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.controls.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.comments.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.communicate.js" type="text/javascript"></script>
    <script src="static/scripts/controls/aa.store.location.js" type="text/javascript"></script>
    <script src="static/scripts/jquery-ui.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="static/scripts/tag-it.js" type="text/javascript" charset="utf-8"></script>
    <script src="static/scripts/controls/aa.uploader.js" type="text/javascript"></script>

    <script type="text/javascript" src="static/scripts/zebra_datepicker.src.js"></script>
    <script type="text/javascript" src="static/scripts/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="static/scripts/controls/aa.dataobject.js"></script>
    <style type="text/css">
        #popup_window {
            padding: 10px;
            background: #267E8A;
            cursor: pointer;
            color: #FCFCFC;
            margin: 200px 0px 0px 200px;
        }

        .popup-overlay {
            width: 100%;
            height: 100%;
            position: fixed;
            background: rgba(196, 196, 196, .85);
            top: 0;
            left: 100%;
            opacity: 0;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
            -webkit-transition: opacity .2s ease-out;
            -moz-transition: opacity .2s ease-out;
            -ms-transition: opacity .2s ease-out;
            -o-transition: opacity .2s ease-out;
            transition: opacity .2s ease-out;
        }

        .overlay .popup-overlay {
            opacity: 1;
            left: 0;
        }

        .popup {
            position: fixed;
            top: 25%;
            left: 50%;
            z-index: -9999;
        }

            .popup .popup-body {
                background: #ffffff;
                background: -moz-linear-gradient(top, #ffffff 0%, #f7f7f7 100%);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #ffffff), color-stop(100%, #f7f7f7));
                background: -webkit-linear-gradient(top, #ffffff 0%, #f7f7f7 100%);
                background: -o-linear-gradient(top, #ffffff 0%, #f7f7f7 100%);
                background: -ms-linear-gradient(top, #ffffff 0%, #f7f7f7 100%);
                background: linear-gradient(to bottom, #ffffff 0%, #f7f7f7 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffff', endColorstr='#f7f7f7', GradientType=0);
                opacity: 0;
                min-height: 150px;
                width: 400px;
                margin-left: -200px;
                padding: 20px;
                -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
                -webkit-transition: opacity .2s ease-out;
                -moz-transition: opacity .2s ease-out;
                -ms-transition: opacity .2s ease-out;
                -o-transition: opacity .2s ease-out;
                transition: opacity .2s ease-out;
                position: relative;
                -moz-box-shadow: 1px 2px 3px 1px rgb(185, 185, 185);
                -webkit-box-shadow: 1px 2px 3px 1px rgb(185, 185, 185);
                box-shadow: 1px 2px 3px 1px rgb(185, 185, 185);
                text-align: center;
                border: 1px solid #e9e9e9;
            }

            .popup.visible, .popup.transitioning {
                z-index: 9999;
            }

                .popup.visible .popup-body {
                    opacity: 1;
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
                }

            .popup .popup-exit {
                cursor: pointer;
                display: block;
                width: 24px;
                height: 24px;
                font-weight: bold;
                position: absolute;
                top: 10px;
                right: 5px;
            }

            .popup .popup-content {
                overflow-y: auto;
            }

        .popup-content .popup-title {
            font-size: 24px;
            border-bottom: 1px solid #e9e9e9;
            padding-bottom: 10px;
        }

        .popup-content p {
            font-size: 13px;
            text-align: justify;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#subject").change(function () {
                $("#documenttitle").val($("#subject").val());
            });
        });

        $(window).load(function () {
            jQuery(document).ready(function ($) {

                $('[data-popup-target]').click(function () {
                    $('html').addClass('overlay');
                    var activePopup = $(this).attr('data-popup-target');
                    $(activePopup).addClass('visible');

                });

                $(document).keyup(function (e) {
                    if (e.keyCode == 27 && $('html').hasClass('overlay')) {
                        clearPopup();
                    }
                });

                $('.popup-exit').click(function () {
                    clearPopup();

                });

                $('.popup-overlay,#close-popup').click(function () {
                    clearPopup();
                });

                $("#approve-document").click(function () {
                    var dialogType = "";
                    $("#credential-popup").find(".popup-title").each(function () {
                        if ($(this).html() == "Approve Document") {
                            dialogType = "ApproveDocument";
                        }
                        else {
                            dialogType = "DiscardDocument";
                        }
                    });

                    if ($("#approve-user").val() != "" && $("#approve-password").val() != "") {
                        AuthenticateUser(dialogType);
                    }
                    else
                        $("#approve-error").show();
                });
            });

            $("#inward-document-link").click(function () {
                GetDocumentId();
                if (DOCUMENT_ID > 0) {
                    if (PARENT_ID > 0)
                        document.location.href = "Documents.aspx?parentid=" + PARENT_ID + "&doctype=inward";
                    else
                        document.location.href = "Documents.aspx?parentid=" + DOCUMENT_ID + "&doctype=inward";
                }
                else {
                    alert("Please save the document before creating new related inward document.");
                }
                // check the document id - if it is not saved - alert user to save the document
                // if document is saved - redirect user to new form with query string
            });

            $("#outward-document-link").click(function () {
                GetDocumentId();
                if (DOCUMENT_ID > 0) {
                    if (PARENT_ID > 0)
                        document.location.href = "Documents.aspx?parentid=" + PARENT_ID + "&doctype=outward";
                    else
                        document.location.href = "Documents.aspx?parentid=" + DOCUMENT_ID + "&doctype=outward";
                }
                else {
                    alert("Please save the document before creating new related outward document.");
                }
                // check the document id - if it is not saved - alert user to save the document
                // if document is saved - redirect user to new form with query string
            });
        });

        function clearPopup() {
            $('.popup.visible').addClass('transitioning').removeClass('visible');
            $('html').removeClass('overlay');

            setTimeout(function () {
                $('.popup').removeClass('transitioning');
            }, 200);
        }

        function SendEmail() {

            var to = $("#email-to").val();
            var subject = $("#email-subject").val();
            var body = $("#email-body").val();
            var hasAttachments = $("#email-attachment").prop("checked");

            // Validate the email fields
            if (to == "") {
                $("#email-error").html("Please enter Email To").show();
            }
            else if (subject == "") {
                $("#email-error").html("Please enter Email Subject").show();
            }
            else if (body == "") {
                $("#email-error").html("Please enter Email Message").show();
            }
            else {
                // Send the email request through communicate control
                var fullUrl = BASE_URL + "api/SendEmail.ashx";
                var dataobj = {
                    DocumentId: DOCUMENT_ID,
                    To: to,
                    Subject: subject,
                    Message: body,
                    AttachDocument: hasAttachments
                };

                $("#email-buttons").hide();
                $("#email-loader").show();

                $.ajax({
                    url: fullUrl,
                    data: dataobj,
                    type: 'POST',
                    dataType: 'json',
                    success: function (data) {
                        $("#email-buttons").show();
                        $("#email-loader").hide();

                        if (data == "true") {
                            $("#email-error").html("Email message sent successfully").css("color", "green").show();
                            $("#email-to").val("");
                            $("#email-subject").val("");
                            $("#email-body").val("");
                            $("#email-attachment").removeProp("checked");
                            setTimeout(function () {
                                $("#email-error").html("");
                            }, 10000);
                        }
                        else {
                            $("#email-error").html("Unable to send an email message. Please try again later").css("color", "red").show();
                        }
                    }
                });
            }
        }

    </script>

        <link href="../static/stylesheets/bootstrap.icon-large.min.css" rel="stylesheet" />
        <script type="text/javascript">
            var BASE_URL = "";
            var College = 'mnwc';

            if (window.location.host.indexOf("localhost") > -1) {
                BASE_URL = "http://localhost:45117/";
            }
            else {
                //BASE_URL = "http://myclasstest.com/dms/";
                BASE_URL = "https://vancotech.com/dms/";
            }

            LoadNotifications();

            $(document).ready(function () {
                $(".nav").find("li a").each(function () {
                    var href = $(this).attr("href");
                    if (href != "undefined" && href != null && href.indexOf(".aspx") > -1) {
                        if (href.indexOf("http") > -1) {
                            $(this).attr("href", $(this).attr("href"));
                        }
                        else {
                            $(this).attr("href", BASE_URL + $(this).attr("href"));
                        }
                    }
                });

                $("#a-not").click(function () {
                    if ($("#notifications").attr("display") == "block") {
                        $("#notifications").hide(500);
                        $("#notifications").attr("display", "none");
                    }
                    else {
                        $("#notifications").show(500);
                        $("#notifications").attr("display", "block");
                    }
                });

                $("#btnclose").click(function () {
                    $("#notifications").hide(500);
                    $("#notifications").attr("display", "none");
                });

                $(".globe").attr("src", BASE_URL + "static/images/notification-globe.png");
            });

            function LoadNotifications() {
                var fullUrl = BASE_URL + "api/AdvancedSearch.ashx?t=nt";
                $.ajax({
                    url: fullUrl,
                    type: 'GET',
                    dataType: 'text',
                    success: PopulateNotifications
                });
            }

            function PopulateNotifications(data) {
                data = JSON.parse(data);

                $("#not-body").empty();

                if (data.length > 0) {
                    $("#not-link").attr("style", "background-color:red");
                    for (var i = 0; i < data.length; i++) {

                        var title = data[i].FriendlyName;
                        if (data[i].FriendlyName.length > 26)
                            data[i].FriendlyName = data[i].FriendlyName.substring(0, 26) + " [...]";

                        var ahref = "<a title='" + title + "' href='" + BASE_URL + "Documents.aspx?id=" + data[i].Id + "&doctype=" + data[i].DocumentType + "'>" + data[i].FriendlyName + " (" + data[i].Deadline + ")</a>";
                        $("#not-body").append($("<div/>").attr("class", "not-row").html(ahref));
                    }
                }
                else {
                    $("#not-link").attr("style", "background-color:transparent");
                    $("#not-body").append($("<div/>").attr("class", "not-row").html("No documents founds."));
                }
            }
        </script>
        <style>
            #notifications {
                width: 300px;
                /* height: 200px; */
                position: absolute;
                /* float: right; */
                right: 0;
                border: 1px solid #ddd;
                background-color: #fff;
                text-align: left;
                cursor: pointer;
            }

            #not-header {
                float: left;
                width: 97%;
                padding: 5px;
                border-bottom: 1px solid #ddd;
            }

            #not-body {
                float: left;
                width: 100%;
                /* border-bottom: 1px solid #ddd; */
            }

            .not-row {
                padding: 5px;
                width: 97%;
                border-bottom: 1px solid #ddd;
                padding: 10px 5px 10px 5px;
            }

                .not-row:hover {
                    background-color: aliceblue;
                }

            #not-counter {
                background-color: red;
                padding: 2px 4px;
                color: white;
                border-radius: 4px;
                font-size: 10px;
                position: absolute;
                top: 10px;
                right: 5px;
            }

            .notification-globe {
                display: inline-block;
                height: 28px;
                line-height: 28px;
                vertical-align: text-bottom;
                width: 28px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="related-document-links">
        <div id="inward-document-link" class="btn btn-danger">Related Inward Document</div>
        <div id="outward-document-link" class="btn btn-warning">Related Outward Document</div>
    </div>
    <div id="form-container"></div>
    <div id="example-popup" class="popup">
        <div class="popup-body">
            <span class="popup-exit">X</span>

            <div class="popup-content">
                <h2 class="popup-title">Approve Document</h2>
                <div id="approve-error" style="display: none; color: red; text-align: left;">Please enter user name and password to approve the document.</div>
                <div>
                    <input id="approve-user" type="text" placeholder="User name" style="width: 50%; height: 20px; margin-right: 20%; float: left; text-align: left;" />
                    <input id="approve-password" type="password" placeholder="Password" style="width: 50%; height: 20px; float: left; text-align: left;" />
                    <div style="margin-top: 10px; width: 90%; float: left;">
                        <div id="approve-document" class="btn btn-success" style="float: left; margin-right: 10px;">Save</div>
                        <div id="close-popup" class="btn" style="float: left" onclick="clearPopup();">Cancel</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="popup-overlay"></div>
</asp:Content>

