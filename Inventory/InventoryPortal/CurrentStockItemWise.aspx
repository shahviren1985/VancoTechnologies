<%@ Page Language="C#" MasterPageFile="~/Master/Master.master"
    AutoEventWireup="true" CodeFile="CurrentStockItemWise.aspx.cs" Inherits="CurrentStockItemWise" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2010" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href=" <%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
    <style type="text/css">
        .table th, .table td {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a> &nbsp;>&nbsp;Item Wise Stock Report
    </div>
    <div id="Form">
        <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
            <div class="HeaderText" style="text-align: left; font: bold;">
                Item Wise Stock Report
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
                        <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true"></asp:TextBox>
                        <rjs:PopCalendar ID="pcPrValidFrom" runat="server" ShowToday="true" From-Today="false"
                            ValidationGroup="" TextMessage="*" Control="txtStartDate" Format="dd mmm yyyy" />
                        <rjs:PopCalendarMessageContainer ID="pcmPrValidFrom" runat="server" Calendar="pcPrValidFrom" />

                        End Date
                     
                        <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true"></asp:TextBox>
                        <rjs:PopCalendar ID="Popcalendar1" runat="server" ShowToday="true" From-Today="false"
                            ValidationGroup="" TextMessage="*" Control="txtEndDate" Format="dd mmm yyyy" />
                        <rjs:PopCalendarMessageContainer ID="Popcalendarmessagecontainer1" runat="server" Calendar="pcPrValidFrom" />

                        <asp:LinkButton ID="btnSubmit" CssClass="SubmitButton" runat="server" OnClick="btnAdd_Click"
                            ValidationGroup="SoftwareDetails">Submit</asp:LinkButton>
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

                <div class="Record" style="width: 100%; height: 500px; overflow: scroll;">
                    <asp:GridView CssClass="table" runat="server" ID="gvIssueDetails" AutoGenerateColumns="true"
                        GridLines="None" Width="100%" EmptyDataText="Currently you don't have any Details for selected date range">
                        <RowStyle BackColor="#EFF3FB" CssClass="details" Width="100%" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="headerStyle"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
