<%@ Page Title="Test Reports" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="TestStatics.aspx.cs" Inherits="TestStatics" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <style>
        a
        {
            color: #0088cc;
            text-decoration: none;
        }
        a:hove
        {
            color: #0088cc;
            text-decoration: underline;
        }
        
        .Record
        {
            float: left;
            width: 100%;
            margin-bottom: 10px;
        }
        .Column1, .Column2
        {
            float: left;
            width: 255px;
        }
        .Column2
        {
            width: 60%;
        }
        .complete-percent
        {
            width: 35%;
            float: right;
            margin-right: 2%;
            background-color: #7FFF00;
            color: #666;
            border-radius: 5px;
            padding: 10px;
        }
        .pending-percent
        {
            width: 35%;
            float: left;
            margin-left: 2%;
            background-color: #F7464A;
            color: #fff;
            border-radius: 5px;
            padding: 10px;
        }
    </style>
    <div style="width: 80%; float: left; margin-left: 10%; margin-top: 1%; margin-bottom: 40px;">
        <div>
            <%--<ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li><li>
                    <div id="divBreadCrumb" runat="server">
                        Test Report</div>
                </li>
            </ul>--%>
            <ul class="breadcrumb" id="divBreadCrumb" runat="server">
            </ul>
        </div>
        <asp:HiddenField ID="hfTestQuizs" runat="server" Value="[]" />
        <asp:HiddenField ID="hfTestCompleteGraphData" runat="server" Value="[]" />
        <asp:HiddenField ID="hfCourseName" Value="[]" runat="server" />
        <div class="Record">
            <div class="Column1">
                Paper Title
            </div>
            <div class="Column2">
                <asp:Label ID="lblCourseName" runat="server" Style="font-size: 18px;"></asp:Label>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Select Class Test
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlTest" runat="server" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"
                    AutoPostBack="true" ValidationGroup="VGSubmit">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlTest" runat="server" ErrorMessage="Please select test.">*
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="graph" style="width: 28%; float: left; padding: 1%; text-align: center;">
            <div style="width: 98%; float: left; text-align: center" id="header" runat="server">
                Your Performance
            </div>
            <div style="width: 98%; float: left">
                <canvas id="canvasWirteWrongAnswer" width="200" height="200">
                        </canvas>
            </div>
            <div style="width: 98%; float: left">
                <div id="completePer" class="complete-percent" style="width: 40%">
                </div>
                <div id="pendingPer" class="pending-percent" style="width: 40%">
                </div>
            </div>
        </div>
        <div id="quiz" style="width: 65%; float: left">
        </div>
    </div>
    <script type="text/javascript">

        function PopulateGraphTestQuiz() {
            PopulateGraph();
            PopulateTestQuiz();
        }

        function PopulateGraph() {
            var dbCourseCompChartData = [];
            dbCourseCompChartData = JSON.parse($("#Content_hfTestCompleteGraphData").val());

            var myDoughnut = new Chart(document.getElementById("canvasWirteWrongAnswer").getContext("2d")).Doughnut(dbCourseCompChartData);

            $("#completePer").html(dbCourseCompChartData[0].value + "% correct");
            $("#pendingPer").html(dbCourseCompChartData[1].value + "% incorrect");
        }

        function PopulateTestQuiz() {
            var Quiz = [];
            Quiz = JSON.parse($("#Content_hfTestQuizs").val());

            for (var i = 0; i < Quiz.length; i++) {
                PopulateQuiz(Quiz[i], i);
            }
        }

        function PopulateQuiz(quiz, counter) {
            var maincontainer = $("<div/>");
            $(maincontainer).attr("class", "quizContainer");
            //end

            var errorStatus = $("<div/>");
            $(errorStatus).attr("class", "ErrorContainer");
            $(errorStatus).attr("id", "errorStatus_" + counter);
            $(errorStatus).html("Please select appropriate answer.");
            $(errorStatus).attr("style", "display:none");
            $(maincontainer).append(errorStatus);

            // creating question text container
            var questionText = $("<div/>");
            $(questionText).attr("id", "QuesionText_" + counter);
            $(questionText).attr("class", "questionText");
            $(questionText).attr("quesId", quiz.Id);
            $(questionText).html("Question " + (parseInt(counter) + 1) + " : " + decodeURIComponent(quiz.QuestionText));

            $(maincontainer).append(questionText);
            // end

            // creating question option text
            var questionOption = $("<div/>");
            $(questionOption).attr("id", "QuestionOption_" + counter);
            $(questionOption).attr("class", "questionText");

            for (var i = 0; i < quiz.QuestionOptionList.length; i++) {
                var div = $("<div/>");
                $(div).attr("id", "QO" + i);
                $(div).html((i + 1) + ".  " + quiz.QuestionOptionList[i].QuestionOption);

                $(questionOption).append(div);
            }

            $(maincontainer).append(questionOption);
            // end

            // creating answer option text
            var answerOption = $("<div/>");
            $(answerOption).attr("id", "AnswerOption_" + counter);
            $(answerOption).attr("class", "answerOption");

            var html = "";
            for (var i = 0; i < quiz.AnswerOptionList.length; i++) {

                var div = $("<div/>");

                $(div).attr("id", "AO" + i);
                //$(div).attr("class", "radioDiv");

                // check if question has mulitiple correct answer
                if (!quiz.IsMultiTrueAnswer) {
                    // populating single correct answer mode
                    var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
                    s = s.replace("%2B", "+");
                    s = s.replace("%2B", "+");
                    s = s.replace("%2B", "+");

                    if (s == quiz.AnswerText) {                        
                        html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
                    }
                    else {                        
                        html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
                    }
                }
                else {
                    // populating multi correct answer mode
                    var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
                    s = s.replace("%2B", "+");
                    s = s.replace("%2B", "+");
                    s = s.replace("%2B", "+");

                    var isSelectedAns = false;

                    if (quiz.AnswerText != null && quiz.AnswerText != undefined) {
                        var userAnswers = quiz.AnswerText.split(",");

                        for (var uaIndex = 0; uaIndex < userAnswers.length; uaIndex++) {
                            if (s == userAnswers[uaIndex]) {
                                isSelectedAns = true;
                                break;
                            }
                        }
                    }
                                        
                    if (isSelectedAns) {                        
                        html += "<input type='checkbox' id='Ans_" + i + "_" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
                    }
                    else {                        
                        html += "<input type='checkbox' id='Ans_" + i + "_" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
                    }
                }

                //$(answerOption).append(div);
            }

            $(answerOption).html(html);

            $(maincontainer).append(answerOption);

            // creating submit button in populate quiz function
            var btnField = $("<div/>");
            $(btnField).attr("id", "btnField_" + counter);
            $(btnField).attr("style", "margin-top: 25px; width: 100%; float: left;");
            //added by anup
            if (quiz.IsAnsGiven) {
                var status = $("<div/>");
                $(status).attr("id", "status_" + counter);

                if (quiz.IsCorrect) {
                    $(status).html("Your answer is correct.<br/>Your answer is '<b>" + quiz.AnswerText + "'</b>");
                    $(status).attr("style", "color: green");
                }
                else {
                    if (quiz.CorrectAnswer.length > 0)
                        quiz.CorrectAnswer = quiz.CorrectAnswer.substring(quiz.CorrectAnswer.length - 1, 0);

                    $(status).html("Your answer is incorrect.<br/>Your answer is '<b>" + quiz.AnswerText + "'</b>");
                    //$(status).html("Your answer is incorrect.<br/>Correct answer is '<b>" + quiz.CorrectAnswer + "'</b>");
                    $(status).attr("style", "color: red");
                }

                $(btnField).append(status);
            }
            else {

                var btn = $("<div/>");
                $(btn).attr("id", "btn_" + counter);
                $(btn).attr("class", "btn btn-primary");
                $(btn).html("Submit");
                $(btn).attr("onclick", "SaveQuiz(" + counter + ")");
                $(btnField).append(btn);

                var status = $("<div/>");
                $(status).attr("id", "status_" + counter);
                $(btnField).append(status);

                var loader = $("<div/>");
                $(loader).attr("id", "loader_" + counter);
                $(loader).html("<img src='" + BASE_URL + "static/images/waiting-loader.gif' />");
                $(loader).hide();
                $(btnField).append(loader);
            }


            $(maincontainer).append(btnField);
            //end
            //$(".content-main").append(maincontainer);
            //$("#content_navigation_2").attr("style", "padding-top:15px");
            //$(maincontainer).insertBefore("#content_navigation_2");

            $("#quiz").append(maincontainer)
        }

        var drop = $("#drop");
        $(drop).css("display", "none");

        //function PopulateCourseName() {
        var drop = $("#label");
        $(drop).css("display", "block");
        $(drop).html("You are learning - " + $("#Content_hfCourseName").val());
        //document.title = $("#Content_hfCourseName").val();
        //}
    </script>
</asp:Content>
