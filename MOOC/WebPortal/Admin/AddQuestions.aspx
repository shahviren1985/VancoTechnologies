<%@ Page Title="Add Question" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="AddQuestions.aspx.cs" Inherits="Admin_AddQuestions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
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
            width: 100%;
            float: left;
            margin-bottom: 5px;
        }
        
        .subcontainer
        {
            border: 1px solid gray;
            float: left;
            width: 291px;
            padding: 10px;
            border-radius: 5px;
        }
    </style>
    <script type="text/javascript">
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
            $(subContainer).attr("class", "subcontainer");
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
            $(mainDiv).attr("id", "mainDiv_" + count);

            var subContainer = $("<div/>");
            $(subContainer).attr("id", "subContainer_" + count);
            $(subContainer).attr("class", "subcontainer");
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

            $(subContainer).append(record2);

            $(mainDiv).append(subContainer);

            $('#moreAnswer').append(mainDiv);
            //$('#moreAnswer').append(subContainer);

            $("#<%=hfMoreAnswer.ClientID%>").val(count);
        }

        function RemoveMoreAnswer(count) {
            var subContainer = "#mainDiv_" + count;
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
                //$("#btnAddMoreAnswer").show();
            }
            else {
                $("#btnAddMoreOption").hide();
                //$("#btnAddMoreAnswer").hide();
            }
        }

        // Getting Dynamic Fields Value
        function GetFieldsValue() {

            var isCurrectAnswerMatchtoAnswerOption = false;

            var JSONAnswerOptions = [];
            var JSONQuestionOptions = [];

            var quesOptionCount;
            var ansOptionCount;

            var currectAnswer = $("#<%=txtCorrectAns.ClientID%>").val();

            if (currectAnswer == "") {
                alert("Please enter currect answer!");
                return false;
            }

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
                alert("Please Enter Answer Option!");
                $("#<%=txtAnswerOption.ClientID%>").focus();
                return false;
            }
            else {
                var ansOptn = $("#<%=txtAnswerOption.ClientID%>").val();
                var isCurrect = false;

                if (ansOptn == currectAnswer) {
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
                        if (ansOptn == currectAnswer) {
                            isCurrectAnswerMatchtoAnswerOption = true;
                            isCurrect = true;
                        }

                        JSONAnswerOptions.push({ "AnswerOption": ansOptn, "IsCurrect": isCurrect });
                    }

                }
            }

            // creating string json of question-option json
            $("#<%=hfMoreOptionValue.ClientID%>").val(JSON.stringify(JSONQuestionOptions));
            // creating string json of answer-option json
            $("#<%=hfMoreAnswerValue.ClientID%>").val(JSON.stringify(JSONAnswerOptions));

            //alert($("#<%=hfMoreOptionValue.ClientID%>").val());
            //alert($("#<%=hfMoreAnswerValue.ClientID%>").val());

            if (isCurrectAnswerMatchtoAnswerOption)
                return true;
            else {
                alert("Your currect answer doesn't match with you answer options. Please make currection!");
                $("#<%=txtCorrectAns.ClientID%>").focus();
                return false;
            }
        }

    </script>
    <div>
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Add New Question.
            </h3>
        </div>
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
                    color: Red; float: left" visible="false">
                </div>
                <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                    Successfully added new Question.
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:DropDownList ID="ddlCourseId" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                            ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course name!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:DropDownList ID="ddlChapterName" runat="server" OnSelectedIndexChanged="ddlChapter_Change"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlChapterName" InitialValue="0" ForeColor="Red"
                            ValidationGroup="VGSubmit" ControlToValidate="ddlChapterName" runat="server"
                            ErrorMessage="Please select chapter name!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:DropDownList ID="ddlSectionId" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlSectionId" InitialValue="0" ForeColor="Red"
                            ValidationGroup="VGSubmit" ControlToValidate="ddlSectionId" runat="server" ErrorMessage="Please select section id!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:TextBox ID="txtQuestion" Width="500px" Height="90px" Placeholder="Enter your question."
                            MaxLength="255" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <span style="color: Red;">*</span>
                        <asp:RequiredFieldValidator ID="rfvtxtChapterName" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="txtQuestion" runat="server" ErrorMessage="Please provide question!">*
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1" style="width: 200px;">
                        Is Question Options Present?
                    </div>
                    <div class="Column2" style="width: 60%;">
                        <asp:RadioButton ID="rbYes" runat="server" Text="Yes" GroupName="QuestionPresent"
                            onclick="ShowHide('1');" />
                        <asp:RadioButton ID="brNo" runat="server" Text="No" GroupName="QuestionPresent" Checked="true"
                            onclick="ShowHide('2');" />
                    </div>
                </div>
                <!--start more option text -->
                <asp:HiddenField ID="hfMoreOption" Value="0" runat="server" />
                <asp:HiddenField ID="hfMoreOptionValue" runat="server" />
                <div class="Record">
                    <input type="button" id="btnAddMoreOption" class="btn btn-success" value="Add Question Options"
                        onclick="AddMoreOption();" />
                </div>
                <div class="Record" style="margin-bottom: 5px;">
                    <div class="subcontainer">
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
                    </div>
                </div>
                <div class="Record" id="moreOption">
                </div>
                <!--end -->
                <!--start more option anser -->
                <asp:HiddenField ID="hfMoreAnswer" Value="0" runat="server" />
                <asp:HiddenField ID="hfMoreAnswerValue" runat="server" />
                <div class="Record">
                    <input type="button" id="btnAddMoreAnswer" class="btn btn-success" value="Add Answer Option"
                        onclick="AddMoreAnswer();" />
                </div>
                <div class="Record" style="margin-bottom: 5px;">
                    <div class="subcontainer">
                        <div class="Record">
                            <div class="Column1" style="width: 255px;">
                                <asp:TextBox ID="txtAnswerOption" Width="220px" Placeholder="Answer Text" MaxLength="255"
                                    runat="server"></asp:TextBox>
                                <span style="color: Red;">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="VGSubmit"
                                    ControlToValidate="txtAnswerOption" runat="server" ErrorMessage="Please provide question!">*
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Record" id="moreAnswer">
                </div>
                <!-- end -->
                <div class="Record">
                    <div class="Column2">
                        <asp:TextBox ID="txtCorrectAns" Width="220px" Placeholder="Correct Answer." MaxLength="255"
                            runat="server"></asp:TextBox>
                        <span style="color: Red;">*</span>
                        <asp:RequiredFieldValidator ID="rfvtxtCorrectAns" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="txtCorrectAns" runat="server" ErrorMessage="Please provide correct answer!">*
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" CausesValidation="true"
                            ValidationGroup="VGSubmit" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return GetFieldsValue();" />
                        <asp:Button ID="btnClear" runat="server" CssClass="btn convert" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
