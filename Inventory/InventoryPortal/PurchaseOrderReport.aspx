<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="PurchaseOrderReport.aspx.cs" Inherits="PurchaseOrderReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href=" <%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left;
        padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a> &nbsp;>&nbsp;Purchase Order Report
    </div>
    <div id="Form">
        <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
            <div class="HeaderText" style="text-align: left; font: bold;">
                Purchase Order Report
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
                        <asp:DropDownList ID="ddlDepartment" runat="server">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="rvddlDepartment"
                    InitialValue="0" runat="server" ControlToValidate="ddlDepartment" ValidationGroup="Select"
                    ErrorMessage="Please Select Department Name."></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column1">
                        Category
                    </div>
                    <div class="Column2">
                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" ValidationGroup="Select"
                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="RequiredFieldValidator1"
                    InitialValue="0" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Please Select Category."></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div id="divDownResUser" runat="server"></div>
                <br /><br />
                <div class="Record">
                    <asp:GridView runat="server" ID="gvInventory" DataKeyNames="Id" AutoGenerateColumns="False"
                        GridLines="None" Width="100%" EmptyDataText="No stock available for selected department">
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
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblName" runat="server" Text='<%#Eval("Name") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manufacturer">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblManufacturer" runat="server" Text='<%#Eval("Manufacturer") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblSpecification" runat="server" Text='<%#Eval("Specification") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblQuantity" runat="server" Text='<%#Eval("QuantityRecommended") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price per Unit">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblPrice" runat="server" Text='<%#Eval("ModelNo") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Price">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblPrice" runat="server" Text='<%#Eval("Price") %>'>'></asp:Label>
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
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkExportExcel" />
                <asp:AsyncPostBackTrigger ControlID="ddlCategory" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
