<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="CourseWiseStudentCountAttemptTyping.aspx.cs"
    Inherits="Admin_CourseWiseStudentCountAttemptTyping" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Course Wise Student Count
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" visible="false">
    </div>
     <div class="Record">
        <asp:GridView ID="gvStudentCount" PageSize="20" runat="server" Visible="true"
            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
            Width="99%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" EnableModelValidation="true">
            <Columns>                
                <asp:TemplateField HeaderText="Course">
                    <ItemTemplate>
                        <asp:Label ID="gvlblCourse" CssClass="gridRows" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Count">
                    <ItemTemplate>
                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BorderWidth="0" BorderStyle="None" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" />
            <RowStyle VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:GridView>
    </div>
    <div class="Record" id="divPrintButton" style="display: block;" runat="server">
        <div class="Column2">
            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Print" OnClick="btnPrint_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
        </div>
    </div>
</asp:Content>
