<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditChapter.aspx.cs" MasterPageFile="~/Master/AdminMasterPage.master"
    Inherits="Admin_EditChapter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfSectionListCount" Value="0" runat="server" />
                <asp:HiddenField ID="hfSectionJson" Value="" runat="server" />
                <asp:HiddenField ID="hfCounter" Value="0" runat="server" />
                <asp:HiddenField ID="hfSelectedChapterFileName" Value="" runat="server" />
                <asp:HiddenField ID="hfOldSelectedChapterFileName" Value="" runat="server" />
                <asp:HiddenField ID="hfChapterId" Value="0" runat="server" />
                <asp:HiddenField ID="hfDeletedSectionId" Value="[]" runat="server" />
                <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
                    color: Red; float: left; width: 100%;" visible="false">
                </div>
                <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                    Chapter Updated Successfully.
                </div>
                <div id="View" runat="server">
                    <div class="Record">
                        <div style="margin-top: 5%;">
                            <div id="logo" style="float: left;">
                                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                            </div>
                            <h3>
                                Chapter Details
                            </h3>
                        </div>
                        <asp:GridView ID="gvChapterDetails" PageSize="10" runat="server" Visible="true" AllowPaging="true"
                            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                            Width="75%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                            EmptyDataText="You haven't added any chapter yet" BorderStyle="None" BorderWidth="1px"
                            EnableModelValidation="true" OnRowCommand="gvChapterDetails_RowCommand" OnPageIndexChanging="gvChapterDetails_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditDetails"
                                            runat="server" CssClass="gridRows">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlnkbtnDelete" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteDetails"
                                            runat="server" CssClass="gridRows">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                            
                                <asp:TemplateField HeaderText="Language">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLanguage" CssClass="gridRows" runat="server" Text='<%#Eval("Language") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Content Version">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblContentVersion" runat="server" Text='<%#Eval("ContentVersion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Name" SortExpression="CourseName">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCourseName" runat="server" Text='<%#Eval("CourseName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BorderWidth="0" BorderStyle="None" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn convert" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
                        </div>
                    </div>
                </div>
                <div id="Edit" runat="server">
                    <%--  <asp:Label runat="server" ID="Temp" Text="List of selected section id"></asp:Label>--%>
                    <div style="margin-top: 5%;">
                        <div id="Div1" style="float: left;">
                            <asp:Image Style="width: 60%;" ID="Image1" runat="server" />
                        </div>
                        <h3>
                            Edit Chapter
                        </h3>
                    </div>
                    <div id="Div2" class="ErrorContainer" runat="server" style="text-align: left; color: Red;
                        float: left; width: 100%;" visible="false">
                    </div>
                    <%--<div id="Div3" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                        Chapter Updated Successfully.
                    </div>--%>
                    <div class="Record">
                        <div class="Column2">
                            <asp:DropDownList ID="ddlCourseId" runat="server" Width="170px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                                ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course name!">
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
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" CausesValidation="true"
                                    ValidationGroup="VGSubmit" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return ValidateDynamicSection();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn convert" Text="Cancle" OnClick="ClearData"
                                    OnDisposed="ClearData" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
    <script type="text/javascript">

        // function to populate dynamically added section.

        function PopulateDynamicSections(section, count) {

            $("#<%=hfCounter.ClientID%>").val(count);
            // alert("Inside Add New Section");
            var sectionDiv = $("<div/>");
            var sectionDivContainer = $("<div/>");


            $(sectionDiv).attr("id", "sectionDiv_" + count);
            $(sectionDiv).attr("class", "DynamicSection");
            $(sectionDivContainer).attr("id", "DynamicSectionContainer_" + count);
            $(sectionDivContainer).attr("class", "DivContainer");


            var sectinName = $("<input/>");
            $(sectinName).attr("type", "text");
            $(sectinName).attr("id", "txtSectionName_" + count);
            $(sectinName).attr("placeholder", "Section Name");
            $(sectinName).attr("class", "Dynamictxt");
            $(sectinName).attr("width", "170px");
            $(sectinName).attr("secId", section.Id);
            $(sectinName).val(section.SectionName);


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
            $(sectinlblFileName).html(section.SectionFileName);


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
        }

        // function to add new section during update.

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
            $(sectinName).attr("secId", "0");

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
            $(sectinlblFileName).html("No file selected");


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
            // alert("Count : " + count);
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
            else {
                $("#<%=hfSelectedChapterFileName.ClientID%>").val($("#<%=lblSelectedChapterFiles.ClientID%>").html());
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
                // alert("File extension of chapter file name" + extensionChapterFileName);
                // alert("File extension of section file name" + extensionSectionFileName);

                if (extensionChapterFileName.toLowerCase() == "htm" || extensionChapterFileName.toLowerCase() == "html") {
                    if (extensionSectionFileName.toLowerCase() == "htm" || extensionSectionFileName.toLowerCase() == "html") {

                        JSONSectionId.push({ "ID": $("#<%=txtSectionName.ClientID%>").attr("secId"), "ChapterId": 0, "SectionName": $("#<%=txtSectionName.ClientID%>").val(), "SectionFileName": $("#<%=lblSelectedSectionFile.ClientID%>").html(), "DateCreated": "" });
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
                    //  alert("undefined true");
                    // alert($('#txtSectionName_' + i).val());
                    //alert($('#lblSelectedSectionFileName_' + i).html().toLowerCase());

                    if ($('#txtSectionName_' + i).val() == "" || $('#lblSelectedSectionFileName_' + i).html() == "No file selected" || $('#lblSelectedSectionFileName_' + i).html() == "") {
                        // alert(1);
                        // alert("section name OR section file name is blank");
                        // alert("Selected Section File Name" + $('#lblSelectedSectionFileName_' + i).html());

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
                                //JSONSectionId.push({ "SectionName": tempSectioName, "SectionFileName": tempSectioFileName, "FilePath": "" });
                                JSONSectionId.push({ "ID": $('#txtSectionName_' + i).attr("secId"), "ChapterId": 0, "SectionName": tempSectioName, "SectionFileName": tempSectioFileName, "DateCreated": "" });
                            }
                            else {
                                //alert("HI1");
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
                            // JSONSectionId.push({ "SectionName": tempSectioName, "SectionFileName": tempSectioFileName, "FilePath": "" });
                            JSONSectionId.push({ "ID": $('#txtSectionName_' + i).attr("secId"), "ChapterId": 0, "SectionName": tempSectioName, "SectionFileName": tempSectioFileName, "DateCreated": "" });
                        }
                        else {
                            // alert("HI2");
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
        var deletedSectionList = [];
        function RemoveDynamicSection(count) {
            // remove Dynamic Section
            var sectionDivId = "#DynamicSectionContainer_" + count;
            var secId = $("#txtSectionName_" + count).attr("secId");
            deletedSectionList.push({ "Id": secId });
            $("#<%=hfDeletedSectionId.ClientID%>").val(JSON.stringify(deletedSectionList));
            // alert(sectionDivId);
            $(sectionDivId).remove();
        }

        // function to populate static section and call a new function which furher populate dynamically add section.
        var sections = [];
        function PopulateSections() {
            //alert("inside PopulateSections");
            //var sections = [];
            sections = JSON.parse($("#<%=hfSectionJson.ClientID%>").val());
            var sectionCount = $("#<%=hfSectionListCount.ClientID%>").val();

            // alert($("#<%=hfSectionJson.ClientID%>").val());
            // alert(sectionCount);

            for (var i = 0; i < sections.length; i++) {
                if (i == 0) {
                    $("#<%=txtSectionName.ClientID%>").val(sections[i].SectionName);
                    $("#<%=lblSelectedSectionFile.ClientID%>").text(sections[i].SectionFileName);
                    $("#<%=txtSectionName.ClientID%>").attr("secId", sections[i].Id);
                }
                else {
                    PopulateDynamicSections(sections[i], i);
                }
            }
        }        
    </script>
    <script type="text/javascript" language="javascript">

        // for selecting files list. 
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
