<%@ Page Title="Import Students" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ImportStudents.aspx.cs" Inherits="Admin_ImportStudents" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 5%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Import Students
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
        color: Red; float: left; width: 100%;" visible="false">
    </div>
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
        Student Imported Successfully.
    </div>
    <div class="Record">
        <div class="Column1">
            Select File
        </div>
        <div class="Column2">
            <asp:FileUpload ID="fuStudents" runat="server" />
        </div>
    </div>
    <div class="Record">
        <div class="Column2">
            <asp:Button ID="btnUpload" CssClass="btn btn-success" runat="server" Text="Upload"
                OnClick="btnUpload_Click" />
            <asp:Button ID="btnCancle" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
        </div>
    </div>
    <div class="Record">
        <asp:GridView ID="gvStudents" PageSize="18" runat="server" Visible="true" AllowPaging="true"
            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
            Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" EnableModelValidation="true" OnPageIndexChanging="gvStudents_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <asp:Label ID="gvlblFirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <asp:Label ID="gvlblLastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="gvlblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Roll Number">
                    <ItemTemplate>
                        <asp:Label ID="gvlblRollNo" runat="server" Text='<%#Eval("RollNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile No">
                    <ItemTemplate>
                        <asp:Label ID="gvlblMobile" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BorderWidth="0" BorderStyle="None" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
            <RowStyle HorizontalAlign="Left" />
        </asp:GridView>
    </div>
    <div class="Record">
        <div class="Column2">
            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click"
                Visible="false" />
            <asp:Button ID="btnCancel2" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx"
                Visible="false" />
        </div>
    </div>
</asp:Content>
