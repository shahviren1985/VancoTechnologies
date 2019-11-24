<%@ Page Language="C#" MasterPageFile="~/Master/Master.master"
    AutoEventWireup="true" CodeFile="IssueReport.aspx.cs" Inherits="IssueReport" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2010" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href=" <%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a> &nbsp;>&nbsp;Issue Details Report
    </div>
    <div id="Form">
        <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
            <div class="HeaderText" style="text-align: left; font: bold;">
                Issue Details Report
            </div>
            <br />
        </div>
        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <div id="Status" runat="server" class="Record">
                </div>
                <div class="Record">
                    <div class="Column2 calendar">
                        Start Date
                        <asp:TextBox ID="txtDateOfIssue" runat="server" ReadOnly="true"></asp:TextBox>
                        <rjs:PopCalendar ID="pcPrValidFrom" runat="server" ShowToday="true" From-Today="false"
                            ValidationGroup="" TextMessage="*" Control="txtDateOfIssue" Format="dd mmm yyyy" />
                        <rjs:PopCalendarMessageContainer ID="pcmPrValidFrom" runat="server" Calendar="pcPrValidFrom" />

                        End Date
                     
                        <asp:TextBox ID="txtIssueEndDate" runat="server" ReadOnly="true"></asp:TextBox>
                        <rjs:PopCalendar ID="Popcalendar1" runat="server" ShowToday="true" From-Today="false"
                            ValidationGroup="" TextMessage="*" Control="txtIssueEndDate" Format="dd mmm yyyy" />
                        <rjs:PopCalendarMessageContainer ID="Popcalendarmessagecontainer1" runat="server" Calendar="pcPrValidFrom" />

                            <asp:LinkButton ID="btnSubmit" CssClass="SubmitButton" runat="server" OnClick="btnAdd_Click"
                                ValidationGroup="SoftwareDetails">Submit</asp:LinkButton>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2 calendar">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                            ErrorMessage="Please enter issue start date  " ControlToValidate="txtDateOfIssue"></asp:RequiredFieldValidator>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                                ErrorMessage=" Please enter issue end date" ControlToValidate="txtIssueEndDate"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="Record">
                    <asp:LinkButton ID="lnkExportExcel" CssClass="SubmitButton" runat="server" Visible="false"
                        OnClick="lnkExportExcel_Click">Export to Excel</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" PostBackUrl="~/Dashboard.aspx" CssClass="SubmitButton" Visible="false"
                        runat="server">Cancel</asp:LinkButton>
                </div>
                <div id="divDownResUser" runat="server"></div>

                <br />
                <br />

                <div class="Record">
                    <asp:GridView runat="server" ID="gvIssueDetails" DataKeyNames="Id" AutoGenerateColumns="False"
                        GridLines="None" Width="100%" EmptyDataText="Currently you don't have any Details for selected date range">
                        <RowStyle BackColor="#EFF3FB" CssClass="details" Width="100%" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="headerStyle"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <%--<asp:TemplateField HeaderText="Serial Number" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvlsrNo" runat="server" Text='<%#Eval("srNo")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblDepartment" runat="server" Text='<%#Eval("DepartmentName")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue To Teacher Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlIssueToTeacherName" runat="server" Text='<%#Eval("TeacherName")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inventory Item">
                                <ItemTemplate>
                                    <asp:Label ID="gvlInventoryItem" runat="server" Text='<%#Eval("InventoryName")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="gvlIssueQuantity" runat="server" Text='<%#Eval("IssueQuantity") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of Issue">
                                <ItemTemplate>
                                    <asp:Label ID="gvlDateofIssue" runat="server" Text='<%#Eval("IssueDate") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IssuerName">
                                <ItemTemplate>
                                    <asp:Label ID="gvlIssuerName" runat="server" Text='<%#Eval("IssuerName") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
