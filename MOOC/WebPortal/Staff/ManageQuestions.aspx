<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageQuestions.aspx.cs"
    Inherits="Staff_ManageQuestions" MasterPageFile="~/Master/StaffMasterPage.master"
    Title="Manage Questions" %>

<asp:Content ID="pageContentHolder" ContentPlaceHolderID="Content" runat="server">
    <div>
        <%--<div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            Chapter Updated Successfully.
        </div>--%>
        <div id="View" runat="server">
            <div class="Record">
                <div style="margin-top: 0%;">
                    <div id="logo" style="float: left;">
                        <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                    </div>
                    <h3>
                        Manage Questions
                    </h3>
                </div>
                <div>
                    <ul class="breadcrumb">
                        <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                        <li>Manage Questions</li>
                    </ul>
                </div>
                <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
                    color: Red; float: left; width: 100%;" visible="false">
                </div>
                <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                    
                </div>
                <div class="Record">
                    <div class="Column1">
                        <asp:Label ID="lblSelectCourse" Text="Select Course" runat="server">
                        </asp:Label>
                    </div>
                    <div class="Column2">
                        <asp:DropDownList ID="ddlCourseId" runat="server" Width="220px" OnSelectedIndexChanged="ddlCourseId_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                            ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course name!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record" id="AddQuestions" runat="server" visible="false">
                    <div class="Column2">
                        <asp:Button ID="btnAddMoreQuestion" runat="server" CssClass="btn btn-success" Text="Add more questions"
                            OnClick="btnAddMoreQuestion_Click" />
                    </div>
                </div>
                <div class="Record">
                    <asp:GridView ID="gvQuestionsDetails" PageSize="50" runat="server" Visible="true"
                        AllowPaging="true" AutoGenerateColumns="false" AllowSorting="false" CellPadding="4"
                        CssClass="Grid" Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White"
                        BorderColor="#CCCCCC" EmptyDataText="You haven't added any question yet" BorderStyle="None"
                        BorderWidth="1px" EnableModelValidation="true" OnPageIndexChanging="gvQuestionsDetails_PageIndexChanging"
                        OnRowCommand="gvQuestionsDetails_RowCommand" 
                        onrowdatabound="gvQuestionsDetails_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Question Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblQuestionId" CssClass="gridRows" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Question Text">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("QuestionText") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chapter Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblChapterName" CssClass="gridRows" runat="server" Text='<%#Eval("ChapterName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section Title">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblSectionName" CssClass="gridRows" runat="server" Text='<%#Eval("SectionName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditQuestions"
                                        runat="server" CssClass="gridRows">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="gvlnkbtnDelete" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteQuestion"
                                        runat="server" CssClass="gridRows" questiontext='<%#Eval("QuestionText") %>'
                                        OnClientClick="return DeleteQuestion(this);">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BorderWidth="0" BorderStyle="None" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn convert" Text="Back" PostBackUrl="~/Staff/Dashboard.aspx" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function DeleteQuestion(button) {
            var QuestionText = $(button).attr("questiontext");
            return confirm("Are you sure you want to delete " + '"' + QuestionText + '"' + "?");
        }
    </script>
</asp:Content>
