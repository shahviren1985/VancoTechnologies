<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="BillDetailsReport.aspx.cs" Inherits="BillDetailsReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href=" <%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a> &nbsp;>&nbsp;Bill Details Report
    </div>
    <div id="Form">
        <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
            <div class="HeaderText" style="text-align: left; font: bold;">
                Bill Details Report
            </div>
            <br />
        </div>
        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <div id="Status" runat="server" class="Record">
                </div>
                <div class="Record">
                    <div class="Column1">
                        Department Name
                    </div>
                    <div class="Column2">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divDownResUser" runat="server"></div>

                <br />
                <br />
                <div class="Record">
                    <asp:GridView runat="server" ID="gvBillDetails" DataKeyNames="Id" AutoGenerateColumns="False"
                        GridLines="None" Width="100%" EmptyDataText="Currently you don't have any Details for selected department">
                        <RowStyle BackColor="#EFF3FB" CssClass="details" Width="100%" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="headerStyle"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DepartmentId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblDepartmentId" runat="server" Text='<%#Eval("DepartmentId")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InventoryId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblInventoryId" runat="server" Text='<%#Eval("InventoryId")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="BillNo">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblBillNo" runat="server" Text='<%#Eval("BillNo")%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Department Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblDepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblInventoryName" runat="server" Text='<%#Eval("InventoryName") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblVendorName" runat="server" Text='<%#Eval("VendorName") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblReceivedQuantity" runat="server" Text='<%#Eval("ReceivedQuantity") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Date">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblBillDate" runat="server" Text='<%#Eval("BillDate") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purchased Price">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblPurchasedPrice" runat="server" Text='<%#Eval("PurchasedPrice") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                 <div class="Record">
                    <asp:LinkButton ID="lnkExportExcel" CssClass="SubmitButton" runat="server" Visible="false"
                        OnClick="lnkExportExcel_Click">Export to Excel</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" PostBackUrl="~/Dashboard.aspx" CssClass="SubmitButton"
                        runat="server">Cancel</asp:LinkButton>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>

