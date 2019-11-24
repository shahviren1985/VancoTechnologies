<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="AddInventory.aspx.cs" Inherits="AddInventory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href="<%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a>&nbsp;>&nbsp;Add Inventory Details
    </div>
    <div id="Form">
        <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
            <div class="HeaderText" style="text-align: left; font: bold;">
                Add Inventory Details
            </div>
            <br />
        </div>
        <div id="divresult" runat="server" style="color: Green; text-align: left;" visible="false">
            Inventory Details Sucessfully Added.
        </div>
        <br />
        <div class="Record">
            <div class="Column1">
                Department
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlDepartment" runat="server">
                    <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="rvddlDepartment" ValidationGroup="SoftwareDetails"
                    InitialValue="0" runat="server" ControlToValidate="ddlDepartment" ErrorMessage="Please Select Department"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Category
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="SoftwareDetails"
                    InitialValue="0" runat="server" ControlToValidate="ddlDepartment" ErrorMessage="Please Select Category"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Item Name
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill the Item Name" ControlToValidate="txtName"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Specification
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtSpecifiation" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSpecification" ForeColor="Red" runat="server" ErrorMessage="Please Fill the Specification"
                    ControlToValidate="txtSpecifiation"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Quantity (Current)
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvQuantity" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill The Quantity" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>

                <asp:CompareValidator ID="CompareValidator3" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                    Operator="DataTypeCheck" ControlToValidate="txtQuantity" ErrorMessage="Please enter valid quantity"
                    ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Quantity (Recommended)
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtRecQuantity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill The Recommended Quantity" ControlToValidate="txtRecQuantity"></asp:RequiredFieldValidator>

                <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                    Operator="DataTypeCheck" ControlToValidate="txtRecQuantity" ErrorMessage="Please enter valid Quantity (Recommended)"
                    ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Manufacturer
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtManufacturer" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill The Recommended Quantity" ControlToValidate="txtRecQuantity"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Vendor
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtVendor" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVersion" ForeColor="Red" runat="server" ErrorMessage="Please Fill the Version"
                    ControlToValidate="txtVendor"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Price Purchased
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ForeColor="Red" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill the Price" ControlToValidate="txtPrice"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                    Operator="DataTypeCheck" ControlToValidate="txtPrice" ErrorMessage="Please enter valid purchased price"
                    ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Model No
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="rfvModelNo" runat="server" ErrorMessage="Please Fill the SerialKey"
                    ControlToValidate="txtModelNo"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Location
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLocation" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill the Location" ControlToValidate="txtLocation"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Remarks
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="rfvCompatibility" runat="server" ErrorMessage="Please Fill the Compatibility"
                    ControlToValidate="txtCompatibility"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <%--<asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />--%>
        <asp:LinkButton ID="btnAdd" CssClass="SubmitButton" runat="server" OnClick="btnAdd_Click"
            ValidationGroup="SoftwareDetails">Add</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" CssClass="SubmitButton" PostBackUrl="~/Dashboard.aspx"
            CausesValidation="false" runat="server">Cancel</asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
