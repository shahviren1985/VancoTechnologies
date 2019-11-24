<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="Staff_AddQuestion"
    MasterPageFile="~/Master/StaffMasterPage.master" Title="Add Question" %>

<asp:Content ID="pageContentHolder" ContentPlaceHolderID="Content" runat="server">
    <style type="text/css">
        .Column2 input[type="radio"]
        {
            float: left;
        }
        .Column2 label
        {
            float: left;
            margin-left: 5px;
            margin-right: 10px;
        }
        
        .mainDiv
        {
            width: 97%;
            float: left;
            margin-bottom: 5px;
            background-color: #EEC4D0;
            border-radius: 5px;
            padding: 5px;
        }
        
        .subcontainer
        {
            border: 1px solid #ccc;
            float: left;
            width: 300px;
            padding: 10px;
            border-radius: 5px;
        }
    </style>
    <script type="text/javascript">

        function getPainKavValue() {
            var radioButtons = document.getElementsByName("chk");

            for (var i = 0; i < radioButtons.length; i++) {
                if (radioButtons[i].checked) {
                    painKavValue = radioButtons[i].value;
                    document.getElementById("painKav").style.display = "none";
                    alert(radioButtons[i].value);
                }
            }
        }


        // function to populate static section and call a new function which furher populate dynamically add section.
        var questionOptions = [];
        var answerOptions = [];

        function PopulateSections() {
            //alert("inside PopulateSections");
            //sections = JSON.parse($("#<%=hfSectionJson.ClientID%>").val());
            questionOptions = JSON.parse($("#<%=hfQuestionOptionCount.ClientID%>").val());
            answerOptions = JSON.parse($("#<%=hfAnswerOptionCount.ClientID%>").val());
            if (questionOptions != null) {
                for (var i = 0; i < questionOptions.length; i++) {
                    if (i == 0) {
                        $("#<%=txtOptionValue.ClientID%>").val(questionOptions[i].QuestionOption);
                        $("#<%=fuOptionImage.ClientID%>").val(questionOptions[i].File);
                    }
                    else {
                        //PopulateQuestionOptions(questionOptions[i], i);

                    }
                }
            }
            if (answerOptions != null) {
                for (var j = 0; j < answerOptions.length; j++) {
                    if (j == 0) {
                        $("#<%=txtAnswerOption.ClientID%>").val(answerOptions[j].AnswerOption);

                        if (answerOptions[j].IsCurrect) {
                            
                            $("#chkOption_0").attr("checked", "checked");
                            $("#answerMainDiv_0").css("background-color", "rgb(208, 255, 202)");
                        }

                    }
                    else {
                        PopulateAnswerOptions(answerOptions[j], j);
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">

        function PopulateQuestionOptions(questionOption, count) {
            $("#<%=hfMoreOption.ClientID%>").val(count);

            var mainDiv = $("<div/>");
            $(mainDiv).attr("class", "mainDiv");
            $(mainDiv).attr("id", "mainDiv_" + count);

            var subContainer = $("<div/>");
            $(subContainer).attr("id", "subContainer_" + count);
            //$(subContainer).attr("class", "subcontainer");
            // creating first row with text box
            var record1 = $("<div/>");
            $(record1).attr("class", "Record");

            var row1col1 = $("<div/>");
            $(row1col1).attr("class", "Column1");

            // creating Option textbox
            var txtMoreOption = $("<input/>");
            $(txtMoreOption).attr("type", "text");
            $(txtMoreOption).attr("id", "txtOption_" + count);
            $(txtMoreOption).attr("placeholder", "Option Text");
            //$(txtMoreOption).attr("class", "DynamicSection");
            $(txtMoreOption).attr("style", "width:220px");
            $(txtMoreOption).val(questionOption.QuestionOption);

            $(row1col1).append(txtMoreOption);
            $(record1).append(row1col1);

            // creating second row with file upload and delete button
            var record2 = $("<div/>");
            $(record2).attr("class", "Record");

            var row2col1 = $("<div/>");
            $(row2col1).attr("class", "Column1");

            // creating Option textbox
            var fuImage = $("<input/>");
            $(fuImage).attr("type", "file");
            $(fuImage).attr("id", "fuImage_" + count);
            $(fuImage).attr("placeholder", "Option Text");
            //$(txtMoreOption).attr("class", "DynamicSection");
            $(fuImage).attr("style", "width:220px");
            $(fuImage).val(questionOption.File);

            $(row2col1).append(fuImage);
            $(record2).append(row2col1);

            var row2col2 = $("<div/>");
            $(row2col2).attr("class", "Column2");
            $(row2col2).attr("style", "width:36px");

            var btnDelete = $("<input/>");
            $(btnDelete).attr("type", "Button");
            $(btnDelete).attr("Value", "X");
            $(btnDelete).attr("id", "btnDelete_" + count);
            $(btnDelete).attr("class", "btn btn-danger");
            $(btnDelete).attr("OnClick", "RemoveMoreOption('" + count + "')");

            $(row2col2).append(btnDelete);
            $(record2).append(row2col2);

            $(subContainer).append(record1);
            $(subContainer).append(record2);

            $(mainDiv).append(subContainer);

            $('#moreOption').append(mainDiv);
            //$('#moreOption').append(subContainer);

        }

        function PopulateAnswerOptions(answerOption, count) {
            $("#<%=hfMoreAnswer.ClientID%>").val(count);

            var mainDiv = $("<div/>");
            $(mainDiv).attr("class", "mainDiv");
            $(mainDiv).attr("id", "answerMainDiv_" + count);

            var subContainer = $("<div/>");
            $(subContainer).attr("id", "subContainer_" + count);
            //$(subContainer).attr("class", "subcontainer");
            // creating first row with text box
            var record2 = $("<div/>");
            $(record2).attr("class", "Record");

            var row2col1 = $("<div/>");
            $(row2col1).attr("class", "Column1");

            // creating Option textbox
            var txtAnswer = $("<input/>");
            $(txtAnswer).attr("type", "text");
            $(txtAnswer).attr("id", "txtOptionAnswer_" + count);
            $(txtAnswer).attr("placeholder", "Answer Text");
            //$(txtMoreOption).attr("class", "DynamicSection");
            $(txtAnswer).attr("style", "width:220px");
            $(txtAnswer).val(answerOption.AnswerOption);

            $(row2col1).append(txtAnswer);
            $(record2).append(row2col1);

            var row2col2 = $("<div/>");
            $(row2col2).attr("class", "Column2");
            $(row2col2).attr("style", "width:36px");

            var btnDelete = $("<input/>");
            $(btnDelete).attr("type", "Button");
            $(btnDelete).attr("Value", "X");
            $(btnDelete).attr("id", "btnDelete_" + count);
            $(btnDelete).attr("class", "btn btn-warning");
            $(btnDelete).attr("OnClick", "RemoveMoreAnswer('" + count + "')");

            $(row2col2).append(btnDelete);
            $(record2).append(row2col2);

            //creating checkbox
            var checkRow = $("<div/>");
            $(checkRow).attr("class", "Column1");

            var checkBox = $("<input/>");
            $(checkBox).attr("type", "checkbox");
            $(checkBox).attr("id", "chkOption_" + count);
            $(checkBox).attr("style", "float: left;");
            $(checkBox).attr("onClick", "SecColor(" + count + ")");

            if (answerOption.IsCurrect) {
                $(checkBox).attr("checked", "checked");
                $(mainDiv).css("background-color", "rgb(208, 255, 202)");
            }

            var checklabel = $("<label/>");
            $(checklabel).attr("type", "label");
            $(checklabel).attr("for", "chkOption_" + count);
            $(checklabel).attr("style", "float: left;margin-left: 5px;");

            $(checklabel).append(document.createTextNode('This is correct answer'));

            $(checkRow).append(checkBox);
            $(checkRow).append(checklabel);

            $(record2).append(checkRow);
            //end

            $(subContainer).append(record2);

            $(mainDiv).append(subContainer);

            $('#moreAnswer').append(mainDiv);
            //$('#moreAnswer').append(subContainer);

        }


        function AddMoreOption() {
            //alert(1);
            var count = 1;

            if ($("#<%=hfMoreOption.ClientID%>").val() != '') {
                count = $("#<%=hfMoreOption.ClientID%>").val();
                count++;
            }

            var mainDiv = $("<div/>");
            $(mainDiv).attr("class", "mainDiv");
            $(mainDiv).attr("id", "mainDiv_" + count);

            var subContainer = $("<div/>");
            $(subContainer).attr("id", "subContainer_" + count);
            //$(subContainer).attr("class", "subcontainer");
            // creating first row with text box
            var record1 = $("<div/>");
            $(record1).attr("class", "Record");

            var row1col1 = $("<div/>");
            $(row1col1).attr("class", "Column1");

            // creating Option textbox
            var txtMoreOption = $("<input/>");
            $(txtMoreOption).attr("type", "text");
            $(txtMoreOption).attr("id", "txtOption_" + count);
            $(txtMoreOption).attr("placeholder", "Option Text");
            //$(txtMoreOption).attr("class", "DynamicSection");
            $(txtMoreOption).attr("style", "width:220px");

            $(row1col1).append(txtMoreOption);
            $(record1).append(row1col1);

            // creating second row with file upload and delete button
            var record2 = $("<div/>");
            $(record2).attr("class", "Record");

            var row2col1 = $("<div/>");
            $(row2col1).attr("class", "Column1");

            // creating Option textbox
            var fuImage = $("<input/>");
            $(fuImage).attr("type", "file");
            $(fuImage).attr("id", "fuImage_" + count);
            $(fuImage).attr("placeholder", "Option Text");
            //$(txtMoreOption).attr("class", "DynamicSection");
            $(fuImage).attr("style", "width:220px");

            $(row2col1).append(fuImage);
            $(record2).append(row2col1);

            var row2col2 = $("<div/>");
            $(row2col2).attr("class", "Column2");
            $(row2col2).attr("style", "width:36px");

            var btnDelete = $("<input/>");
            $(btnDelete).attr("type", "Button");
            $(btnDelete).attr("Value", "X");
            $(btnDelete).attr("id", "btnDelete_" + count);
            $(btnDelete).attr("class", "btn btn-danger");
            $(btnDelete).attr("OnClick", "RemoveMoreOption('" + count + "')");

            $(row2col2).append(btnDelete);
            $(record2).append(row2col2);

            $(subContainer).append(record1);
            $(subContainer).append(record2);

            $(mainDiv).append(subContainer);

            $('#moreOption').append(mainDiv);
            //$('#moreOption').append(subContainer);

            $("#<%=hfMoreOption.ClientID%>").val(count);
        }

        // var deletedQuestionOptionList = []; written and commented by anup for a while
        function RemoveMoreOption(count) {
            var subContainer = "#mainDiv_" + count;
            $(subContainer).remove();
        }


        function AddMoreAnswer() {
            var count = 1;

            if ($("#<%=hfMoreAnswer.ClientID%>").val() != '') {
                count = $("#<%=hfMoreAnswer.ClientID%>").val();
                count++;
            }

            var mainDiv = $("<div/>");
            $(mainDiv).attr("class", "mainDiv");
            $(mainDiv).attr("id", "answerMainDiv_" + count);

            var subContainer = $("<div/>");
            $(subContainer).attr("id", "subContainer_" + count);
            //$(subContainer).attr("class", "subcontainer");
            // creating first row with text box
            var record2 = $("<div/>");
            $(record2).attr("class", "Record");

            var row2col1 = $("<div/>");
            $(row2col1).attr("class", "Column1");

            // creating Option textbox
            var txtAnswer = $("<input/>");
            $(txtAnswer).attr("type", "text");
            $(txtAnswer).attr("id", "txtOptionAnswer_" + count);
            $(txtAnswer).attr("placeholder", "Answer Text");
            //$(txtMoreOption).attr("class", "DynamicSection");
            $(txtAnswer).attr("style", "width:220px");

            $(row2col1).append(txtAnswer);
            $(record2).append(row2col1);

            var row2col2 = $("<div/>");
            $(row2col2).attr("class", "Column2");
            $(row2col2).attr("style", "width:36px");

            var btnDelete = $("<input/>");
            $(btnDelete).attr("type", "Button");
            $(btnDelete).attr("Value", "X");
            $(btnDelete).attr("id", "btnDelete_" + count);
            $(btnDelete).attr("class", "btn btn-warning");
            $(btnDelete).attr("OnClick", "RemoveMoreAnswer('" + count + "')");

            $(row2col2).append(btnDelete);
            $(record2).append(row2col2);

            //creating checkbox
            var checkRow = $("<div/>");
            $(checkRow).attr("class", "Column1");

            var checkBox = $("<input/>");
            $(checkBox).attr("type", "checkbox");
            $(checkBox).attr("id", "chkOption_" + count);
            $(checkBox).attr("style", "float: left;");
            $(checkBox).attr("onClick", "SecColor(" + count + ")");

            var checklabel = $("<label/>");
            $(checklabel).attr("type", "label");
            $(checklabel).attr("for", "chkOption_" + count);
            $(checklabel).attr("style", "float: left;margin-left: 5px;");

            $(checklabel).append(document.createTextNode('This is correct answer'));

            $(checkRow).append(checkBox);
            $(checkRow).append(checklabel);

            $(record2).append(checkRow);
            //end

            $(subContainer).append(record2);

            $(mainDiv).append(subContainer);

            $('#moreAnswer').append(mainDiv);
            //$('#moreAnswer').append(subContainer);

            $("#<%=hfMoreAnswer.ClientID%>").val(count);
        }

        function SecColor(index) {
            //alert($("#chkOption_" + index).attr("checked"));
            if ($("#chkOption_" + index).attr("checked")) {
                $("#answerMainDiv_" + index).css("background-color", "#D0FFCA");
            }
            else {
                $("#answerMainDiv_" + index).css("background-color", "#EEC4D0");
            }
        }

        function RemoveMoreAnswer(count) {
            var subContainer = "#answerMainDiv_" + count;
            $(subContainer).remove();
        }

        ShowHide(2);

        $(document).ready(function () {
            ShowHide(2);
            //alert(1);
        });

        function ShowHide(val) {
            if (val == 1) {
                $("#btnAddMoreOption").show();
                $("#questionoptions").show();
            }
            else {
                $("#btnAddMoreOption").hide();
                $("#questionoptions").hide();
            }
        }

        // Getting Dynamic Fields Value
        function GetFieldsValue() {

            var isCurrectAnswerMatchtoAnswerOption = false;

            var JSONAnswerOptions = [];
            var JSONQuestionOptions = [];

            var quesOptionCount;
            var ansOptionCount;

            // get Question Option Counter value
            if ($("#<%=hfMoreOption.ClientID%>").val() != '') {
                quesOptionCount = $("#<%=hfMoreOption.ClientID%>").val();
            }

            // get Answer Option Counter value
            if ($("#<%=hfMoreAnswer.ClientID%>").val() != '') {
                ansOptionCount = $("#<%=hfMoreAnswer.ClientID%>").val();
            }

            //alert($("#Content_rbYes").prop("checked"));

            if ($("#Content_rbYes").prop("checked") == true) {

                // get static control value for Ques-Option
                if (($("#<%=txtOptionValue.ClientID%>").val() == "")) {
                    alert("Please Enter Question Option!");
                    $("#<%=txtOptionValue.ClientID%>").focus();
                    return false;
                }
                else if (($("#<%=fuOptionImage.ClientID%>").val() == "")) {
                    alert("Please Select Question Option file!");
                    $("#<%=fuOptionImage.ClientID%>").attr('style', 'color : red');
                    return false;
                }
                else {
                    var quesOptn = $("#<%=txtOptionValue.ClientID%>").val();
                    var file = $("#<%=fuOptionImage.ClientID%>").val();
                    JSONQuestionOptions.push({ "QuestionOption": quesOptn, "File": file });
                }

                // getting Dynamic Controls Value for Question-Option
                var counter = 0;
                for (var i = 1; i <= quesOptionCount; i++) {
                    if ($('#txtOption_' + i).val() != undefined) {

                        if ($('#txtOption_' + i).val() == "") {
                            alert("Please Enter Question Option!");
                            $('#txtOption_' + i).focus();
                            return false;
                        }
                        else {
                            var quesOptn = $('#txtOption_' + i).val();
                            var file = $('#fuImage_' + i).val();
                            JSONQuestionOptions.push({ "QuestionOption": quesOptn, "File": file });
                        }
                    }
                }
            }
            //end

            // get static control value for Ans-Option
            if (($("#<%=txtAnswerOption.ClientID%>").val() == "")) {
                alert("Please enter choices for an answer");
                $("#<%=txtAnswerOption.ClientID%>").focus();
                return false;
            }
            else {
                var ansOptn = $("#<%=txtAnswerOption.ClientID%>").val();
                var isCurrect = false;

                //if (ansOptn == currectAnswer) {
                if ($("#chkOption_" + 0).attr("checked")) {
                    isCurrectAnswerMatchtoAnswerOption = true;
                    isCurrect = true;
                }

                JSONAnswerOptions.push({ "AnswerOption": ansOptn, "IsCurrect": isCurrect });
            }
            // getting Dynamic Controls Value for Answer-Option
            for (var i = 1; i <= ansOptionCount; i++) {
                if ($('#txtOptionAnswer_' + i).val() != undefined) {

                    if ($('#txtOptionAnswer_' + i).val() == "") {
                        alert("Please Enter Answer Option!");
                        $('#txtOptionAnswer_' + i).focus();
                        return false;
                    }
                    else {
                        var ansOptn = $('#txtOptionAnswer_' + i).val();
                        var isCurrect = false;
                        //if (ansOptn == currectAnswer) {chkOption_1
                        if ($("#chkOption_" + i).attr("checked")) {
                            isCurrectAnswerMatchtoAnswerOption = true;
                            isCurrect = true;
                        }

                        JSONAnswerOptions.push({ "AnswerOption": ansOptn, "IsCurrect": isCurrect });
                    }

                }
            }

            console.log(JSONAnswerOptions);

            // creating string json of question-option json
            $("#<%=hfMoreOptionValue.ClientID%>").val(JSON.stringify(JSONQuestionOptions));
            // creating string json of answer-option json
            $("#<%=hfMoreAnswerValue.ClientID%>").val(JSON.stringify(JSONAnswerOptions));

            //alert($("#<%=hfMoreOptionValue.ClientID%>").val());
            //alert($("#<%=hfMoreAnswerValue.ClientID%>").val());

            if (isCurrectAnswerMatchtoAnswerOption)
                return true;
            else {
                alert("Please select correct answer!");                
                return false;
            }
        }

    </script>
    <div>
        <asp:HiddenField ID="hfQuestionOptionCount" Value="0" runat="server" />
        <asp:HiddenField ID="hfAnswerOptionCount" Value="0" runat="server" />
        <asp:HiddenField ID="hfSectionJson" Value="" runat="server" />
        <asp:HiddenField ID="hfDeletedQuestionOptions" Value="[]" runat="server" />
        <asp:HiddenField ID="hfDeletedAnswerOptions" Value="[]" runat="server" />
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Add Question
            </h3>
        </div>
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li><a href="ManageQuestions.aspx" target="_top">Manage Questions</a><a></a> <span
                    class="divider">/</span></li>
                <li>Add Questions</li>
            </ul>
        </div>
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
                    color: Red; float: left" visible="false">
                </div>
                <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                    Successfully added new Question.
                </div>
                <div id="mainContentDiv" runat="server">
                    <div class="Record">
                        <div class="Column2">
                            <asp:DropDownList ID="ddlChapterName" runat="server" OnSelectedIndexChanged="ddlChapter_Change"
                                AutoPostBack="true" Enabled="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlChapterName" InitialValue="0" ForeColor="Red"
                                ValidationGroup="VGSubmit" ControlToValidate="ddlChapterName" runat="server"
                                ErrorMessage="Please select chapter name!">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:DropDownList ID="ddlSectionId" runat="server" Enabled="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSectionId" InitialValue="0" ForeColor="Red"
                                ValidationGroup="VGSubmit" ControlToValidate="ddlSectionId" runat="server" ErrorMessage="Please select section id!">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:HiddenField ID="hfOldQuestionText" runat="server" />
                            <asp:TextBox ID="txtQuestion" Width="500px" Height="90px" Placeholder="Enter your question."
                                MaxLength="255" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <span style="color: Red;">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtChapterName" ForeColor="Red" ValidationGroup="VGSubmit"
                                ControlToValidate="txtQuestion" runat="server" ErrorMessage="Please provide question!">*
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            Do you want to add options to your question?
                        </div>
                        <div class="Column2">
                            <asp:RadioButton ID="rbYes" runat="server" Text="Yes" GroupName="QuestionPresent"
                                onclick="ShowHide('1');" />
                            <asp:RadioButton ID="brNo" runat="server" Text="No" GroupName="QuestionPresent" Checked="true"
                                onclick="ShowHide('2');" />
                        </div>
                    </div>
                    <!--start more option text -->
                    <asp:HiddenField ID="hfMoreOption" Value="0" runat="server" />
                    <asp:HiddenField ID="hfMoreOptionValue" runat="server" />
                    <div class="Record" style="margin-bottom: 5px; display: none" id="questionoptions">
                        <div class="subcontainer">
                            <div class="Record">
                                <input type="button" id="btnAddMoreOption" class="btn btn-success" value="Add Question Options"
                                    onclick="AddMoreOption();" style="display: none" />
                            </div>
                            <div class="Record">
                                <div class="Column1" style="width: 255px;">
                                    <asp:TextBox ID="txtOptionValue" Width="220px" Placeholder="Option Text" MaxLength="255"
                                        runat="server"></asp:TextBox>
                                    <span style="color: Red;">*</span>
                                </div>
                            </div>
                            <div class="Record">
                                <div class="Column1" style="width: 255px;">
                                    <asp:FileUpload ID="fuOptionImage" runat="server" Width="220px" />
                                </div>
                            </div>
                            <div class="Record" id="moreOption">
                            </div>
                        </div>
                    </div>
                    <!--end -->
                    <!--start more option anser -->
                    <asp:HiddenField ID="hfMoreAnswer" Value="0" runat="server" />
                    <asp:HiddenField ID="hfMoreAnswerValue" runat="server" />
                    <div class="Record" style="margin-bottom: 5px;">
                        <div class="subcontainer">
                            <div class="Record">
                                <input type="button" id="btnAddMoreAnswer" class="btn btn-success" value="Add Answer Option"
                                    onclick="AddMoreAnswer();" />
                            </div>
                            <div class="mainDiv" id="answerMainDiv_0">
                                <div class="Column1" style="width: 255px;">
                                    <asp:TextBox ID="txtAnswerOption" Width="220px" Placeholder="Answer Text" MaxLength="255"
                                        runat="server"></asp:TextBox>
                                    <span style="color: Red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="VGSubmit"
                                        ControlToValidate="txtAnswerOption" runat="server" ErrorMessage="Please provide question!">*
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="Column1" style="width: 255px;">
                                    <input type="checkbox" name="option" id="chkOption_0" style="float: left;" onclick="SecColor(0);">
                                    <label for="chkOption_0" style="float: left; margin-left: 5px">This is correct answer</label>
                                </div>
                            </div>
                            <div class="Record" id="moreAnswer">
                            </div>
                        </div>
                    </div>
                    <!-- end -->
                    <%--<div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtCorrectAns" Width="220px" Placeholder="Correct Answer." MaxLength="255"
                                runat="server"></asp:TextBox>
                            <span style="color: Red;">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtCorrectAns" ForeColor="Red" ValidationGroup="VGSubmit"
                                ControlToValidate="txtCorrectAns" runat="server" ErrorMessage="Please provide correct answer!">*
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                    <div class="Record">
                        <div class="Column2">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" CausesValidation="true"
                                ValidationGroup="VGSubmit" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return GetFieldsValue();" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn convert" Text="Cancel" PostBackUrl="~/Staff/ManageQuestions.aspx" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        PopulateSections();
    </script>
</asp:Content>
