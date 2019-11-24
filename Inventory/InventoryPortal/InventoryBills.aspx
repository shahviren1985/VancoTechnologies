<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="InventoryBills.aspx.cs" Inherits="InventoryBills" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2010"  Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href="<%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 110px; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a>&nbsp;>&nbsp;Bill Details
    </div>
    <style>
        #Content_gvInventoryBill th {
            padding:3px;
            padding-left:5px;
        }
        #Content_gvInventoryBill td {
            padding-left:5px;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <div id="Form">
                <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
                    <div class="HeaderText" style="text-align: left; font: bold;">
                        Inventory Bill Detail
                    </div>
                    <br />
                </div>
                <div id="divresult" runat="server" style="color: Green; text-align: left;" visible="false">
                    Inventory bill saved Sucessfully.
                </div>
                <div id="divError" runat="server" style="color: Red; text-align: left;" visible="false"></div>
                <br />
                <div style="vertical-align: middle; align-items: center; padding: 1px">
                    <table style="width: 100%">
                        <tr>
                            <td style="vertical-align: baseline"><span style="float: left; width: 80px;">Vendor Name</span></td>
                            <td style="vertical-align: middle">
                                <div class="form-group">
                                    <asp:TextBox ID="txtVendorName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" ForeColor="Red" Display="Dynamic" runat="server" ValidationGroup="SoftwareDetails"
                                        ErrorMessage="Please Fill the Vendor Name" ControlToValidate="txtVendorName"></asp:RequiredFieldValidator>
                                </div>
                            </td>

                            <td style="vertical-align: baseline"><span style="float: left; width: 80px; padding-left: 15px">Bill No #</span></td>
                            <td style="vertical-align: middle">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBillNo" runat="server" Width="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                                        ErrorMessage="Please Fill the Bill No" ControlToValidate="txtBillNo"></asp:RequiredFieldValidator>
                                </div>
                            </td>

                            <td style="vertical-align: baseline"><span style="float: left; width: 80px; padding-left: 15px">Bill Date</span></td>
                            <td style="vertical-align: middle">
                                <div class="form-group" style="width: 183px">
                                    <asp:TextBox ID="txtDateOfBill" runat="server" ReadOnly="true"></asp:TextBox>
                                    <rjs:PopCalendar ID="pcPrValidFrom" runat="server" ShowToday="true" From-Today="false" Format="dd mmm yyyy"
                                        ValidationGroup="" TextMessage="*" Control="txtDateOfBill" />
                                    <rjs:PopCalendarMessageContainer ID="pcmPrValidFrom" runat="server" Calendar="pcPrValidFrom" />
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                                        ErrorMessage="Please Select Bill Date" ControlToValidate="txtDateOfBill"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="vertical-align: baseline"><span style="float: left; width: 80px; padding-left: 15px">Department</span></td>
                            <td style="vertical-align: middle">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="rvddlDepartment" ValidationGroup="SoftwareDetails"
                                        InitialValue="0" runat="server" ControlToValidate="ddlDepartment" ErrorMessage="Please Select Department"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: baseline"><span style="float: left; width: 80px;">Item Name</span></td>
                            <td style="vertical-align: middle">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlInventoryItem" runat="server">
                                        <asp:ListItem Value="0" Text="Select Inventory"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="RequiredFieldValidator4" ValidationGroup="SoftwareDetails"
                                        InitialValue="0" runat="server" ControlToValidate="ddlInventoryItem" ErrorMessage="Please Select Inventory."></asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="vertical-align: baseline"><span style="float: left; width: 105px; padding-left: 15px">Quantity Received</span></td>
                            <td style="vertical-align: baseline">
                                <div class="form-group">
                                    <asp:TextBox ID="txtQuantity" runat="server" Width="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuantity" Display="Dynamic" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                                        ErrorMessage="Please Fill the Quantity." ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator3" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                                        Operator="DataTypeCheck" ControlToValidate="txtQuantity" ErrorMessage="Please enter valid quantity."
                                        ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                                        Operator="GreaterThan" ValueToCompare="0" ControlToValidate="txtQuantity" ErrorMessage="Please enter quantity greater than 0."
                                        ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
                                </div>
                            </td>
                            <td style="vertical-align: baseline"><span style="float: left; width: 96px; padding-left: 15px">Price Purchased</span></td>
                            <td style="vertical-align: baseline">
                                <div class="form-group">
                                    <asp:TextBox ID="txtPricePurchased" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPricePurchased" Display="Dynamic" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                                        ErrorMessage="Please Fill the Purchased Price." ControlToValidate="txtPricePurchased"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Type="Double" ValidationGroup="SoftwareDetails"
                                        Operator="DataTypeCheck" ControlToValidate="txtPricePurchased" ErrorMessage="Please enter valid purchased price."
                                        ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator4" Display="Dynamic" runat="server" Type="Double" ValidationGroup="SoftwareDetails"
                                        Operator="GreaterThan" ValueToCompare="0" ControlToValidate="txtPricePurchased" ErrorMessage="Please enter purchased price greater than 0."
                                        ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
                                </div>
                            </td>
                            <td colspan="2">
                                <div class="form-group" style="text-align:right">
                                    <asp:LinkButton ID="btnAdd" CssClass="SubmitButton" runat="server" OnClick="btnInsert_Click"
                                        ValidationGroup="SoftwareDetails">Add</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" CssClass="SubmitButton" PostBackUrl="~/Dashboard.aspx"
                                        CausesValidation="false" runat="server">Cancel</asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:GridView runat="server" ID="gvInventoryBill" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" style="width:100%" OnRowEditing="GridView1_RowEditing" 
                     PageSize="4" GridLines="None" Width="60%" EmptyDataText="You haven't added any inventory details yet">
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" CssClass="details" Width="100%" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="headerStyle"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" />
                        <asp:BoundField DataField="BillDate" HeaderText="Bill Date" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                        <asp:BoundField DataField="QuantityReceived" HeaderText="Quantity Received" />
                        <asp:BoundField DataField="PricePurchased" HeaderText="Price Purchased" />
                        <asp:TemplateField HeaderText="Options" >
                            <ItemTemplate>
                                <%--<asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />--%>
                                <asp:LinkButton ID="OnDelete" Text="Delete" runat="server" OnClick="OnDelete"><img style="height:25px" src="https://findicons.com/files/icons/766/base_software/128/deletered.png" alt="delete Item" /></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                                <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div class="Record" style="padding-top:10px;">
                    <asp:LinkButton ID="btnSubmitBill" Visible="false" CssClass="SubmitButton" runat="server" OnClick="btnSubmitBill_Click">Save</asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <%--<asp:PostBackTrigger ControlID="OnDelete" />--%>
            <asp:PostBackTrigger ControlID="btnSubmitBill" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>