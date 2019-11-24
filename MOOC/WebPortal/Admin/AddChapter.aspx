<%@ Page Title="Add Chapter" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/AdminMasterPage.master"
    CodeFile="AddChapter.aspx.cs" Inherits="Admin_AddChapter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script language="javascript" type="text/javascript">

        function AddNewSection() {
            //alert("Inside Add New Section");
            var sectionDiv = $("<div/>");
            var sectionDivContainer = $("<div/>");
            var count;

            if ($("#<%=hfCounter.ClientID%>").val() != '') {
                count = $("#<%=hfCounter.ClientID%>").val();
                count++;
            }

            $(sectionDiv).attr("id", "sectionDiv_" + count);
            $(sectionDiv).attr("class", "DynamicSection");
            $(sectionDivContainer).attr("id", "DynamicSectionContainer_" + count);
            $(sectionDivContainer).attr("class", "DivContainer")

            var sectinName = $("<input/>");
            $(sectinName).attr("type", "text");
            $(sectinName).attr("id", "txtSectionName_" + count);
            $(sectinName).attr("placeholder", "Section Name");
            $(sectinName).attr("class", "Dynamictxt");
            $(sectinName).attr("width", "170px");


            var sectinFileName = $("<a/>");
            //$(sectinFileName).attr("type", "anchor");
            $(sectinFileName).attr("id", "anSectionFileName_" + count);
            $(sectinFileName).attr("href", "#ChooseFiles");
            $(sectinFileName).attr("onClick", "SetTypes('section'," + count + ")");
            $(sectinFileName).html("Select Section Files");
            $(sectinFileName).attr("data-toggle", "modal");
            $(sectinFileName).attr("class", "Dynamictxt");

            var sectinlblFileName = $("<label/>");
            //$(sectinlblFileName).attr("type", "label");
            $(sectinlblFileName).attr("id", "lblSelectedSectionFileName_" + count);
            $(sectinlblFileName).attr("class", "Dynamiclbl");
            $(sectinlblFileName).html("No file selected * ");


            var DeleteButton = $("<input/>");
            $(DeleteButton).attr("type", "Button");
            $(DeleteButton).attr("Value", "X");
            $(DeleteButton).attr("id", "btnDelete_" + count);
            $(DeleteButton).attr("class", "btn btn-danger");
            $(DeleteButton).attr("OnClick", "RemoveDynamicSection('" + count + "')");

            sectionDiv.append(sectinName);
            sectionDiv.append(sectinFileName);
            sectionDiv.append(sectinlblFileName);
            sectionDiv.append(DeleteButton);

            sectionDivContainer.append(sectionDiv);
            $("#mainSectionContainer").append(sectionDivContainer);

            $("#<%=hfCounter.ClientID%>").val(count);
        }

        function ValidateDynamicSection() {
            // alert("Inside ValidateDynamicSection");

            var JSONSectionId = [];

            var count;
            var tempSectioName = "";
            var tempSectioFileName = "";

            if ($("#<%=hfCounter.ClientID%>").val() != '') {
                count = $("#<%=hfCounter.ClientID%>").val();
            }
            //alert(count);
            if (($("#<%=txtChapterName.ClientID%>").val() == "")) {
                alert("Please Enter Chapter Name");
                $("#<%=txtChapterName.ClientID%>").focus();
                return false;
            }
            if (($("#<%=lblSelectedChapterFiles.ClientID%>").html() == "No file selected")) {
                alert("Please Enter Chapter File Name");
                $("#<%=lblSelectedChapterFiles.ClientID%>").attr('style', 'color : red');
                return false;
            }
            if (($("#<%=txtSectionName.ClientID%>").val() == "")) {
                alert("Please Enter Section Name");
                $("#<%=txtSectionName.ClientID%>").focus();
                return false;
            }
            if (($("#<%=lblSelectedSectionFile.ClientID%>").html() == "No file selected")) {
                alert("Please Enter Section File Name");
                $("#<%=lblSelectedSectionFile.ClientID%>").attr('style', 'color : Red');
                return false;
            }
            else {
                //alert("Inside else");
                var extensionChapterFileName = $("#<%=lblSelectedChapterFiles.ClientID%>").html().replace(/^.*\./, '');
                var extensionSectionFileName = $("#<%=lblSelectedSectionFile.ClientID%>").html().replace(/^.*\./, '');

                if (extensionChapterFileName.toLowerCase() == "htm" || extensionChapterFileName.toLowerCase() == "html") {
                    if (extensionSectionFileName.toLowerCase() == "htm" || extensionSectionFileName.toLowerCase() == "html") {

                        JSONSectionId.push({ "SectionName": $("#<%=txtSectionName.ClientID%>").val(), "SectionFileName": $("#<%=lblSelectedSectionFile.ClientID%>").html(), "FilePath": "" });
                    }
                    else {
                        alert("Please select section file name with .htm or.html extension!");
                        $("#<%=lblSelectedSectionFile.ClientID%>").attr('style', 'color : red');
                        return false;
                    }
                }
                else {
                    alert("Please select chapter file name with .htm or.html extension!");
                    $("#<%=lblSelectedChapterFiles.ClientID%>").attr('style', 'color : red');
                    return false;
                }
            }

            for (var i = 1; i <= count; i++) {
                // alert("Inside for and dynamically added section count is : " + count);
                if ($('#txtSectionName_' + i).val() != undefined) {
                    //alert(i);
                    if ($('#txtSectionName_' + i).val() == "" || $('#lblSelectedSectionFileName_' + i).html() == "No file selected") {

                        if ($('#txtSectionName_' + i).val() == "") {
                            alert("Please Enter Section Name");
                            $('#txtSectionName_' + i).focus();
                            return false;
                        }
                        if ($('#lblSelectedSectionFileName_' + i).html() == "No file selected") {
                            alert("Please Select Section File Name");
                            $('#lblSelectedSectionFileName_' + i).attr('style', 'color : red');
                            return false;
                        }
                        else {
                            //alert("else1");
                            var extensionOfDynamicSectionFileName = $('#lblSelectedSectionFileName_' + i).html().replace(/^.*\./, '');
                            //alert(extensionOfDynamicSectionFileName);
                            if (extensionOfDynamicSectionFileName.toLowerCase() == "htm" || extensionOfDynamicSectionFileName.toLowerCase() == "html") {

                                tempSectioName = $('#txtSectionName_' + i).val();
                                tempSectioFileName = $('#lblSelectedSectionFileName_' + i).html();
                                //alert(tempCode + i + 'counter');
                                JSONSectionId.push({ "SectionName": tempSectioName, "SectionFileName": tempSectioFileName, "FilePath": "" });
                            }
                            else {
                                alert("Please select section file name with .htm or.html extension!");
                                $('#lblSelectedSectionFileName_' + i).attr('style', 'color : red');
                                return false;
                            }
                        }
                    }
                    else {
                        var extensionOfDynamicSectionFileName = $('#lblSelectedSectionFileName_' + i).html().replace(/^.*\./, '');
                        //alert(extensionOfDynamicSectionFileName);
                        if (extensionOfDynamicSectionFileName.toLowerCase() == "htm" || extensionOfDynamicSectionFileName.toLowerCase() == "html") {

                            tempSectioName = $('#txtSectionName_' + i).val();
                            tempSectioFileName = $('#lblSelectedSectionFileName_' + i).html();
                            //alert(tempCode + i + 'counter');
                            JSONSectionId.push({ "SectionName": tempSectioName, "SectionFileName": tempSectioFileName, "FilePath": "" });
                        }
                        else {
                            alert("Please select section file name with .htm or.html extension!");
                            $('#lblSelectedSectionFileName_' + i).attr('style', 'color : red');
                            return false;
                        }
                        // return true;
                    }
                }
            }

            // add hidden filed name sectionjson. 
            // hidden field value = JSON.stringify(GraceMarksList)
            $("#<%=hfSectionJson.ClientID%>").val(JSON.stringify(JSONSectionId));
            // alert($("#<%=hfSectionJson.ClientID%>").val());
            return true;
        }
        function RemoveDynamicSection(count) {

            // remove Dynamic Section
            var sectionDivId = "#DynamicSectionContainer_" + count;
            $(sectionDivId).remove();
        }
    </script>
    <div>
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Add New Chapter
            </h3>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            Successfully added new chapter.
        </div>
        <asp:HiddenField ID="hfCounter" Value="0" runat="server" />
        <asp:HiddenField ID="hfSectionJson" Value="" runat="server" />
        <asp:HiddenField ID="hfSelectedChapterFileName" Value="" runat="server" />
        <div class="Record">
            <div class="Column2">
                <asp:DropDownList ID="ddlCourseId" runat="server" Width="170px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course Id!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:DropDownList ID="ddlLanguage" runat="server" Width="170px">
                    <asp:ListItem Text="--Select Language--" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="English" Value="En"></asp:ListItem>
                    <asp:ListItem Text="Hindi" Value="Hin"></asp:ListItem>
                    <asp:ListItem Text="Marathi" Value="Mar"></asp:ListItem>
                    <asp:ListItem Text="Gujrati" Value="Guj"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlLanguage" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlLanguage" runat="server" ErrorMessage="Please select language!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Record">
                <div class="Column2">
                    <asp:TextBox ID="txtChapterName" Width="170px" Placeholder="Chapter Name" MaxLength="255"
                        runat="server"></asp:TextBox>
                    <span style="color: Red;">*</span> E.g. Introduction to computer
                    <asp:RequiredFieldValidator ID="rfvtxtChapterName" ForeColor="Red" ValidationGroup="VGSubmit"
                        ControlToValidate="txtChapterName" runat="server" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <a data-toggle="modal" href="#ChooseFiles" id="aViewChapterFileList" runat="server"
                        onclick="SetTypes('chapter',0);">Select Chapter Files</a><asp:Label ID="lblSelectedChapterFiles"
                            runat="server" Text="No file selected" CssClass="lbl"></asp:Label><span style="color: Red;">*</span>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:TextBox ID="txtSectionName" Width="170px" Placeholder="Section Name" MaxLength="255"
                        runat="server"></asp:TextBox>
                    <span style="color: Red;">*</span> E.g. Sec-Comp
                    <asp:RequiredFieldValidator ID="rfvtxtSectionName" ForeColor="Red" ValidationGroup="VGSubmit"
                        ControlToValidate="txtSectionName" runat="server" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <a data-toggle="modal" href="#ChooseFiles" id="aViewSectionFileList" runat="server"
                        onclick="SetTypes('section',0);">Select Section Files</a><asp:Label ID="lblSelectedSectionFile"
                            runat="server" Text="No file selected" CssClass="lbl"></asp:Label><span style="color: Red;">*</span>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <input type="button" id="AddSection" class="btn convert" value="Add New Section"
                        onclick="AddNewSection();" />
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <div id="mainSectionContainer" class="Column2">
                    </div>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" CausesValidation="true"
                        ValidationGroup="VGSubmit" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return ValidateDynamicSection();" />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn convert" OnClick="ClearData"
                        Text="Clear" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ChooseFiles" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Choose Chapter Files</h4>
                </div>
                <div class="modal-body">
                    <asp:RadioButtonList ID="rblFiles" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                    </asp:RadioButtonList>
                </div>
                <div class="modal-footer">
                    <input type="button" id="SelectFile" class="btn" runat="server" value="Select File"
                        onclick="SelectFiles();" data-dismiss="modal" />
                    <a href="Javascript:void(0)" class="btn" data-dismiss="modal" onclick="$('#ChooseFiles').fadeOut(500);"
                        title="click to close popup">Close</a>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript" language="javascript">

        var fileType, count;

        function SetTypes(file, cnt) {
            fileType = file;
            count = cnt;
        }

        function SelectFiles() {
            //var fileList = document.elementByID("rblFiles");
            var selectedFile = $("#<%=rblFiles.ClientID %> input:radio:checked").val();
            // alert(selectedFile);
            // alert("type: " + fileType + " count: " + count);

            if (count == 0) {
                if (fileType.toLowerCase() == "chapter") {
                    // get chapter file container lable and set it's value to selecte file value
                    // alert("inside chapter");
                    $("#Content_lblSelectedChapterFiles").html(selectedFile);
                    $("#<%=hfSelectedChapterFileName.ClientID%>").val(selectedFile);
                    $("#Content_lblSelectedChapterFiles").attr('style', 'color : black');
                    // alert("inside chapter");
                } else if (fileType.toLowerCase() == "section") {
                    // get section file container lable and set it's value to selecte file value
                    $("#Content_lblSelectedSectionFile").html(selectedFile);
                    $("#Content_lblSelectedSectionFile").attr('style', 'color : black');
                }
            }
            else {

                $("#lblSelectedSectionFileName_" + count).html(selectedFile);
                $("#lblSelectedSectionFileName_" + count).attr('style', 'color : black');
            }

            //$('#ChooseFiles').hide();
        }
    
    </script>
</asp:Content>
