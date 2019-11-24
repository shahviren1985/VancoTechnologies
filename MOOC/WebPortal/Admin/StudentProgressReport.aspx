<%@ Page Title="Student Progress Report" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master" AutoEventWireup="true" CodeFile="StudentProgressReport.aspx.cs" Inherits="Admin_StudentProgressReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Student Progress Report
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" visible="false">
    </div>
    <div class="Record">
        <div class="Column1">
            Select Course
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlCourse" runat="server" onchange="$('#Content_divGenerateButton').css('display', 'block'); $('#Content_divPrintButton').css('display', 'none');">
            </asp:DropDownList>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            Select Stream
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlStream" runat="server" onchange="$('#Content_divGenerateButton').css('display', 'block'); $('#Content_divPrintButton').css('display', 'none');">
            </asp:DropDownList>
        </div>
    </div>
    <div class="Record" id="divGenerateButton" runat="server">
        <div class="Column2">
            <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Generate" OnClick="btnGenerate_Click" />
            <asp:Button ID="btnCancle" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
        </div>
    </div>
    <div class="Record">
        <asp:GridView ID="gvUserDetails" PageSize="20" runat="server" Visible="true" AllowPaging="false"
            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
            Width="99%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" EnableModelValidation="true">
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
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
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="gvlblEmail" CssClass="gridRows" runat="server" Text='<%#Eval("EmailAddress") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile No.">
                    <ItemTemplate>
                        <asp:Label ID="gvlblMobile" CssClass="gridRows" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course">
                    <ItemTemplate>
                        <asp:Label ID="gvlblCourse" CssClass="gridRows" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub course" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="gvlblSubCourse" CssClass="gridRows" runat="server" Text='<%#Eval("SubCourse") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" Visible="false">
                    <HeaderTemplate>
                        <asp:CheckBox ID="gvchkheader" runat="server" onclick="javascript:SelectAllCheckboxes1('Content_gvUserDetails',this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="gvchkSelectStudent" runat="server" />
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BorderWidth="0" BorderStyle="None" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" />
            <RowStyle VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:GridView>
    </div>
    <div class="Record" id="divPrintButton" style="display: none;" runat="server">
        <div class="Column2">
            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Save As PDF" OnClick="btnPrint_Click" />
                <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Save As Excel" OnClick="btnGenerateExcel_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
        </div>
    </div>
</asp:Content>

