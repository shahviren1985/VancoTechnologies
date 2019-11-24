<%@ Page Title="Manage Class Tests" Language="C#" MasterPageFile="~/Master/StaffMasterPage.master"
    AutoEventWireup="true" CodeFile="TestDetails.aspx.cs" Inherits="Staff_TestDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div>
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Manage Class Tests
            </h3>
        </div>
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li>Manage Class Tests</li>
            </ul>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
        </div>
        <div class="Record">
            <div class="Column1">
                Select Course
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlCourse" runat="server" Width="220px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlCourse" runat="server" ErrorMessage="Please select course.">*
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" OnClick="btnAdd_Click"
                Text="Add New Test" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn convert" Text="Back" PostBackUrl="~/Staff/Dashboard.aspx" />
        </div>
        <div class="Record">
            <asp:GridView ID="gvChapterDetails" PageSize="10" runat="server" Visible="true" AllowPaging="false"
                AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                EmptyDataText="Currently you don't have any class test for selected course" BorderStyle="None"
                BorderWidth="1px" EnableModelValidation="true" OnRowCommand="gvChapterDetails_RowCommand"
                OnRowDataBound="gvChapterDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No.">
                        <ItemTemplate>
                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Test Title">
                        <ItemTemplate>
                            <asp:Label ID="gvlblTest" CssClass="gridRows" runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Chapters">
                        <ItemTemplate>
                            <asp:Label ID="gvlblChapters" CssClass="gridRows" runat="server" Text='<%#Eval("Chapters") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status Active" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%--<asp:Label ID="gvlblStatus" CssClass="gridRows" runat="server" Text='<%#Eval("IsTestActive") %>'></asp:Label>--%>
                            <asp:CheckBox ID="gvchkStatus" Checked='<%#Eval("IsTestActive") %>' runat="server"
                                Enabled="false" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>'
                                Text="Edit" CommandName="Edit1"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>'
                                Text="Delete" CommandName="Delete1" OnClientClick="return confirm('Are you sure to want to delete test?');"></asp:LinkButton>
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
</asp:Content>
