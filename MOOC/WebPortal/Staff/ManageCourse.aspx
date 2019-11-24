<%@ Page Title="Manage Courses" Language="C#" MasterPageFile="~/Master/StaffMasterPage.master"
    AutoEventWireup="true" CodeFile="ManageCourse.aspx.cs" Inherits="Staff_ManageCourse" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:HiddenField ID="hfCourseId" runat="server" Value="0" />
    <div class="Record">
        <div style="margin-top: 0%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Manage Course
            </h3>
        </div>
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li>Manage Course</li>
            </ul>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
        </div>
        <div class="Record">
            <div class="Column1">
                Course Name
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtCourseName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfCourseName" runat="server" ErrorMessage="Please enter course-name"
                    ForeColor="Red" ControlToValidate="txtCourseName" ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <asp:Button ID="btnAddUpdate" runat="server" Text="Add New Course" CssClass="btn btn-success"
                OnClick="btnAddUpdate_Click" ValidationGroup="Save" />
            <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn" PostBackUrl="~/Staff/Dashboard.aspx" />
        </div>
        <div class="Record">
        </div>
        <div class="Record">
            <asp:GridView ID="gvCourseDetails" PageSize="10" runat="server" Visible="true" AllowPaging="false"
                AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid" style="cursor:pointer"
                Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                EmptyDataText="You haven't added any chapter yet" BorderStyle="None" BorderWidth="1px"
                EnableModelValidation="true" OnPageIndexChanging="gvCourseDetails_PageIndexChanging"
                OnRowCommand="gvCourseDetails_RowCommand" 
                onrowdatabound="gvCourseDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No.">
                        <ItemTemplate>
                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Title">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("CourseName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditDetails"
                                runat="server" CssClass="gridRows">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkbtnDelete" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteDetails"
                                runat="server" CssClass="gridRows" OnClientClick="return confirm('Are you sure to delete selected course?');">Delete</asp:LinkButton>
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
