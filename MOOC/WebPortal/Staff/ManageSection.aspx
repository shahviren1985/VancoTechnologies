<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageSection.aspx.cs" Inherits="Staff_ManageSection"
    MasterPageFile="~/Master/StaffMasterPage.master" Title="Manage Sections" %>

<asp:Content ID="pageContentHolder" ContentPlaceHolderID="Content" runat="server">
    <div>
        <asp:HiddenField ID="hfSelectedSectionId" runat="server" />
        <asp:HiddenField ID="hfIsEdit" runat="server" />
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            Section Updated Successfully.
        </div>
        <div id="View" runat="server">
            <div style="margin-top: 0%;">
                <div id="logo" style="float: left;">
                    <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                </div>
                <h3>
                    Manage Sections
                </h3>
            </div>
            <div>
                <ul class="breadcrumb">
                    <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                    <li>Manage Sections</li>
                </ul>
            </div>
            <div class="Record">
                <div class="Column1">
                    <asp:Label ID="lblSelectCourse" Text="Select Course" runat="server">
                    </asp:Label>
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlCourseId" runat="server" Width="220px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCourseId_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                        ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course name!">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <div class="Column1">
                    <asp:Label ID="lblSelectedChapter" Text="Select Chapter" runat="server">
                    </asp:Label>
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlChapterId" runat="server" Width="220px" OnSelectedIndexChanged="ddlChapterId_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlChapterId" InitialValue="0" ForeColor="Red"
                        ValidationGroup="VGSubmit" ControlToValidate="ddlChapterId" runat="server" ErrorMessage="Please select chapter name!">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record" id="AddSection" runat="server" visible="false">
                <div class="Column2">
                    <a data-toggle="modal" href="#RegisterSection" onclick="AddSection();" id="aAddChapter"
                        class="btn btn-success">Add New Section</a>
                </div>
            </div>
            <div class="Record">
                <asp:GridView ID="gvSectionDetails" PageSize="10" runat="server" Visible="true" AllowPaging="false"
                    AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                    Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    EmptyDataText="You haven't added any section yet!" BorderStyle="None" BorderWidth="1px"
                    EnableModelValidation="true" OnPageIndexChanging="gvSectionDetails_PageIndexChanging"
                    OnRowCommand="gvSectionDetails_RowCommand" 
                    onrowdatabound="gvSectionDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section Title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblSectionName" CssClass="gridRows" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a data-toggle="modal" href="#RegisterSection" onclick="EditSection(this);" sectionname='<%#Eval("Title")%>'
                                    id='<%#Eval("Id") %>'>Edit</a>
                                <%-- <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditSection"
                                    runat="server" CssClass="gridRows">Edit</asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="gvlnkbtnDelete" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteSection"
                                    runat="server" CssClass="gridRows" sectionname='<%#Eval("Title")%>' OnClientClick="return DeleteSection(this)">Delete</asp:LinkButton>
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
    <div class="modal fade" id="RegisterSection" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
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
                            <asp:Label ID="lblSectionName" runat="server"></asp:Label>
                            <asp:TextBox ID="txtNewSectionName" runat="server" placeholder="Enter Section Name"
                                Style="margin-top: 6px;"></asp:TextBox>
                        </div>
                        <div class="Record">
                            <div class="Column2">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="Javascript:void(0)" class="btn" data-dismiss="modal" onclick="$('#RegisterSection').fadeOut(500);"
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
        function EditSection(a) {
            // alert("Inside EditChapter");
            $("#<%=Heading.ClientID%>").html("Edit Section Title");
            var sectionName = $(a).attr("sectionname");
            //alert(sectionName);
            //var sectionId = $(a).attr("sectionid");
            var selectedSectionID = $(a).attr("Id");
            // alert("Section Id : " + selectedSectionID);
            $("#<%=hfSelectedSectionId.ClientID%>").val(selectedSectionID);
            // alert("HF : " + $("#<%=hfSelectedSectionId.ClientID%>").val());
            $("#<%=lblSectionName.ClientID%>").html("Section Title : ");
            $("#<%=txtNewSectionName.ClientID%>").val(sectionName);
            $("#<%=btnAddUpdate.ClientID%>").val("Edit Section Title");
            $("#<%=hfIsEdit.ClientID%>").val("true");
        }

        function AddSection() {
            // alert("Inside EditChapter");
            $("#<%=Heading.ClientID%>").html("Add New Section");
            //var chapterName = $(a).attr("chaptrename");
            // var chapterId = $(a).attr("chapterid");
            $("#<%=lblSectionName.ClientID%>").html("Section Title : ");
            $("#<%=txtNewSectionName.ClientID%>").val("");
            $("#<%=btnAddUpdate.ClientID%>").val("Add Section");
            $("#<%=hfIsEdit.ClientID%>").val("false");
        }

        function DeleteSection(button) {
            var sectionname = $(button).attr("sectionname");
            return confirm("Are you sure you want to delete " + '"' + sectionname + '"' + "?");
        }

        function Validate() {
            if ($("#<%=txtNewSectionName.ClientID%>").val() == "") {
                $("#<%=txtNewSectionName.ClientID%>").focus();
                alert("Please enter section name !");
                return false;
            }
        }
    </script>
</asp:Content>
