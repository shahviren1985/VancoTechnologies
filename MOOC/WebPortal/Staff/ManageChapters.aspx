<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageChapters.aspx.cs" Inherits="Staff_ManageChapters"
    MasterPageFile="~/Master/StaffMasterPage.master" Title="Manage Chapter" %>

<asp:Content ID="pageContentHolder" ContentPlaceHolderID="Content" runat="server">
    <div>
        <asp:HiddenField ID="hfSelectedChapterId" runat="server" />
        <asp:HiddenField ID="hfIsEdit" runat="server" />
        <asp:HiddenField ID="hfSelectedChapterIdToDelete" runat="server" />
        <div id="View" runat="server">
            <div style="margin-top: 0%;">
                <div id="logo" style="float: left;">
                    <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                </div>
                <h3>
                    Manage Chapters
                </h3>
            </div>
            <div>
                <ul class="breadcrumb">
                    <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                    <li>Manage Chapters</li>
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
            <div class="Record" id="AddChapter" runat="server" visible="false">
                <div class="Column2">
                    <a data-toggle="modal" href="#RegisterChapter" onclick="AddChapter();" id="aAddChapter"
                        class="btn btn-success">Add New Chapter</a>
                </div>
            </div>
            <div class="Record">
                <asp:GridView ID="gvChapterDetails" PageSize="10" runat="server" Visible="true" AllowPaging="false"
                    AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                    Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    EmptyDataText="You haven't added any chapter yet" BorderStyle="None" BorderWidth="1px"
                    EnableModelValidation="true" OnPageIndexChanging="gvChapterDetails_PageIndexChanging"
                    OnRowCommand="gvChapterDetails_RowCommand" 
                    onrowdatabound="gvChapterDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chapter Title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a data-toggle="modal" href="#RegisterChapter" onclick="EditChapter(this);" chaptrename='<%#Eval("Title") %>'
                                    chapterid='<%#Eval("Id") %>' id='<%#Eval("Id") %>'>Edit</a>
                                <%-- <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditDetails"
                                    runat="server" CssClass="gridRows">Edit</asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="gvlnkbtnDelete" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteDetails"
                                    runat="server" CssClass="gridRows" chaptrename='<%#Eval("Title") %>' OnClientClick="return DeleteChapter(this);">Delete</asp:LinkButton>
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
    <div class="modal fade" id="RegisterChapter" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="Heading" runat="server">
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="Record">
                        <div class="Column2">
                            <asp:Label ID="lblChapterName" runat="server"></asp:Label>
                            <asp:TextBox ID="txtNewChapterName" runat="server" placeholder="Enter Chapter Title"
                                Style="margin-top: 6px;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="Javascript:void(0)" class="btn" data-dismiss="modal" onclick="$('#RegisterChapter').fadeOut(500);"
                        title="click to close popup">Close</a>
                    <asp:Button ID="btnAddUpdate" runat="server" Text="" CssClass="btn btn-primary" OnClick="btnAddUpdate_Click"
                        OnClientClick=" return Validate()" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        function EditChapter(a) {
            // alert("Inside EditChapter");
            $("#<%=Heading.ClientID%>").html("Edit Chapter Title");
            var chapterName = $(a).attr("chaptrename");
            var chapterId = $(a).attr("chapterid");
            var selectedChapterID = $(a).attr("Id");
            //alert("Chapter Id : " + selectedChapterID);
            $("#<%=hfSelectedChapterId.ClientID%>").val(selectedChapterID);
            //alert("HF : " + $("#<%=hfSelectedChapterId.ClientID%>").val());
            $("#<%=lblChapterName.ClientID%>").html("Chapter Title:");
            $("#<%=txtNewChapterName.ClientID%>").val(chapterName);
            $("#<%=btnAddUpdate.ClientID%>").val("Edit Chapter Title");
            $("#<%=hfIsEdit.ClientID%>").val("true");
        }

        function AddChapter() {
            // alert("Inside EditChapter");
            $("#<%=Heading.ClientID%>").html("Add New Chapter");
            //var chapterName = $(a).attr("chaptrename");
            // var chapterId = $(a).attr("chapterid");
            $("#<%=lblChapterName.ClientID%>").html("Chapter Title:");
            $("#<%=txtNewChapterName.ClientID%>").val("");
            $("#<%=btnAddUpdate.ClientID%>").val("Add Chapter");
            $("#<%=hfIsEdit.ClientID%>").val("false");
        }

        function DeleteChapter(button) {
            var chapterName = $(button).attr("chaptrename");
            return confirm("Are you sure you want to delete " + '"' + chapterName + '"' + "?");
        }

        function Validate() {
            if ($("#<%=txtNewChapterName.ClientID%>").val() == "") {
                $("#<%=txtNewChapterName.ClientID%>").focus();
                alert("Please enter chapter name");
                return false;
            }
        }
    </script>
</asp:Content>
