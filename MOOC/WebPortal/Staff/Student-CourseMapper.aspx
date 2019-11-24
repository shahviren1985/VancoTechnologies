<%@ Page Title="Student Course Mapper" Language="C#" MasterPageFile="~/Master/StaffMasterPage.master"
    AutoEventWireup="true" CodeFile="Student-CourseMapper.aspx.cs" Inherits="Staff_Student_CourseMapper" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div>
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Student Course Mapper
            </h3>
        </div>
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                <li>Student Course Mapper</li>
            </ul>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            float: left; width: 100%;" visible="false">
        </div>
        <div class="Record">
            <div class="Column1">
                Select Course
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlCourse" runat="server" Width="220px" AutoPostBack="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                    ValidationGroup="VGSubmit" ControlToValidate="ddlCourse" runat="server" ErrorMessage="Please select course.">*
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                <span style="margin-right: 20px;">Select Stream</span>
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlStream" runat="server" Width="220px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlStream_OnChange" ValidationGroup="VGSubmit">
                </asp:DropDownList>
            </div>
        </div>
        <div class="Record">
            <asp:GridView ID="gvUserDetails" PageSize="20" runat="server" Visible="true" AllowPaging="false"
                AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                Width="99%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" EnableModelValidation="true" 
                OnPageIndexChanging="gvUserDetails_PageIndexChanging" 
                onrowdatabound="gvUserDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sr No." HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student">
                        <ItemTemplate>
                            <asp:Label ID="gvlblStudent" CssClass="gridRows" runat="server" Text='<%#Eval("LastName") + " " + Eval("FirstName") + " " + Eval("FatherName") + " " + Eval("MotherName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblEmail" CssClass="gridRows" runat="server" Text='<%#Eval("EmailAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile No." Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblMobile" CssClass="gridRows" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course">
                        <ItemTemplate>
                            <asp:Label ID="gvlblCourse" CssClass="gridRows" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="gvchkheader" runat="server" onclick="javascript:SelectAllCheckboxes1('Content_gvUserDetails',this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="gvchkSelectStudent" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BorderWidth="0" BorderStyle="None" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            </asp:GridView>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:Button ID="btnMap" runat="server" Text="Map" CssClass="btn btn-success" OnClick="btnMap_Click"
                    ValidationGroup="VGSubmit" Visible="false" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" PostBackUrl="~/Staff/Dashboard.aspx" />
            </div>
        </div>
    </div>
</asp:Content>
