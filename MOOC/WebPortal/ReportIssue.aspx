<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ReportIssue.aspx.cs" Inherits="ReportIssue" %>


<html>
<head>
    <title>MOOC Academy
    </title>
    <link rel="Stylesheet" href="static/styles/chapter-style.css">
    <link rel="Stylesheet" href="static/bootstrap.min.css">
    <script src="static/scripts/jquery-1.9.1.js"></script>    
    <script src="static/scripts/AA.core.js"></script>
    <script type="text/javascript" src="static/scripts/bootstrap.min.js"></script>
    
    <style type="text/css">
        .ErrorContainer, .SuccessContainer {
            padding: 10px;
            float: left;
            border-radius: 5px;
            float: left;
            background-color: pink;
            margin-bottom: 10px;
        }

        .SuccessContainer {
            background-color: lightgreen;
        }

        .Dynamictxt {
            color: Black;
            margin-left: 5px;
        }

        #currentCourse a:hover {
            color: #fff;
            text-decoration: underline;
        }

        #ql_Display {
            background-color: #404040 !important;
        }

        .typing-image {
            width: 70px !important;
        }
    </style>
    <style type="text/css">
        .content input[type='text'] {
            width: 30%;
            font-size: 14px;
        }

        .content textarea {
            width: 30%;
            font-size: 14px;
        }
    </style>
    <script type="text/javascript">
        $(".top-navigation").css("padding-bottom", "15px");
        $("#currentCourse").hide();
    </script>
</head>
<body>
    <div class="content-container" style="margin-left: 0%; width: 100%; padding: 0%;">
        <div class="content" style="margin-left: 0%; width: 94%; border: 1px solid #CCC; box-shadow: 0 2px 2px #999; padding: 2%;">
            <h2>Report Issue</h2>
            <br />

            <div id="reportIssueStatus" class="ErrorContainer" style="display: none"></div>
            <div class="Record">
                <div class="Column2">
                    <input style="min-height: 30px;" type="text" id="txtTitle" placeholder="Title" /><span style="color: Red">*</span>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <input type="text" style="min-height: 30px;" id="txtDescription" placeholder="Description" /><span style="color: Red">*</span>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <input type="text" style="min-height: 30px;" id="txtEmail" placeholder="E-mail" /><span style="color: Red">*</span>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <textarea id="txtExpectedContent" placeholder="Expected content" cols="20" rows="2" style="max-height: 100px; margin-bottom: 3%; padding: 6px;"></textarea><span style="color: Red">*</span>
                </div>
            </div>
            <div class="Record">
                <div class="Column2"></div>
            </div>

            <div class="Record">
                <input id="btnSave" type="button" value="Submit" class="btn btn-primary" onclick="SaveReportIssue();" />
                <input id="btnCancel" type="button" value="Cancel" class="btn btn-warning" onclick="CancelReportIssue();" />
            </div>
        </div>
    </div>
</body>
</html>


