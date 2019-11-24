<%@ Page Title="Student Test Performance Report" Language="C#" MasterPageFile="~/Master/StaffMasterPage.master"
    AutoEventWireup="true" CodeFile="StudentTestPerformanceReport.aspx.cs" Inherits="Staff_StudentTestPerformanceReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Student Test Performance Report
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" visible="false">
    </div>
    <div class="Record">
        <div class="Column1">
            Select Stream
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlCourse" runat="server" onchange="$('#Content_divGenerateButton').css('display', 'block'); $('#Content_divPrintButton').css('display', 'none');">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select course"
                ForeColor="Red" ValidationGroup="Gen" ControlToValidate="ddlCourse" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            Select Test
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlTests" runat="server" onchange="$('#Content_divGenerateButton').css('display', 'block'); $('#Content_divPrintButton').css('display', 'none');">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select test"
                ForeColor="Red" ValidationGroup="Gen" ControlToValidate="ddlTests" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record" id="divGenerateButton" runat="server">
        <div class="Column2">
            <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Generate" OnClick="btnGenerate_Click" ValidationGroup="Gen" />
            <asp:Button ID="btnCancle" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Staff/Dashboard.aspx" />
        </div>
    </div>
    <div class="Record">
        <asp:GridView ID="gvUserDetails" PageSize="20" runat="server" Visible="true" AllowPaging="false"
            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
            Width="99%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" EnableModelValidation="true" 
            onrowdatabound="gvUserDetails_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sr No">
                    <ItemTemplate>
                        <asp:Label ID="gvlblSrno" CssClass="gridRows" runat="server" Text='<%#Eval("Sr No")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student">
                    <ItemTemplate>
                        <asp:Label ID="gvlblStudent" CssClass="gridRows" runat="server" Text='<%#Eval("Student")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course">
                    <ItemTemplate>
                        <asp:Label ID="gvlblCourse" CssClass="gridRows" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:Label ID="gvlblTotalQuiz" CssClass="gridRows" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Attempted">
                    <ItemTemplate>
                        <asp:Label ID="gvlblAttempt" CssClass="gridRows" runat="server" Text='<%#Eval("Attempt") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Correct">
                    <ItemTemplate>
                        <asp:Label ID="gvlblCorrect" CssClass="gridRows" runat="server" Text='<%#Eval("Correct") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Incorrect" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="gvlblIncorrect" CssClass="gridRows" runat="server" Text='<%#Eval("Incorrect") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BorderWidth="0" BorderStyle="None" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <RowStyle VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>
    <div class="Record" id="divPrintButton" style="display: none;" runat="server">
        <div class="Column2">
            <asp:Button ID="btnAsPDF" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Save As PDF" OnClick="btnAsPDF_Click" />
            <asp:Button ID="btnAsExcel" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Text="Save As Excel" OnClick="btnAsExcel_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="btn convert" CausesValidation="true"
                ValidationGroup="VGSubmit" Text="Cancel" PostBackUrl="~/Staff/Dashboard.aspx" />
        </div>
    </div>
</asp:Content>
