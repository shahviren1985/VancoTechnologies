<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="InventoryIssue.aspx.cs" Inherits="InventoryIssue" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2010" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href="<%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 110px; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a>&nbsp;>&nbsp;Issue Inventory
    </div>
     <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
    <div id="Form">
        <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
            <div class="HeaderText" style="text-align: left; font: bold;">
                Issue Inventory
            </div>
            <br />
        </div>
        <div id="divresult" runat="server" style="color: Green; text-align: left;" visible="false">
            Inventory issued Sucessfully.
        </div>
        <div id="divError" runat="server" style="color: Red; text-align: left;" visible="false"></div>
        <br />
        <div class="Record">
            <div class="Column1">
                Department
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="rvddlDepartment" ValidationGroup="SoftwareDetails"
                    InitialValue="0" runat="server" ControlToValidate="ddlDepartment" ErrorMessage="Please Select Department"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Issue to Teacher
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlTeacher" runat="server">
                    <asp:ListItem Value="0" Text="--Select Teacher--" Selected="True"></asp:ListItem>                   
                </asp:DropDownList>
                <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="SoftwareDetails"
                    InitialValue="0" runat="server" ControlToValidate="ddlTeacher" ErrorMessage="Please Select Issue to Teacher."></asp:RequiredFieldValidator>
            </div>
        </div>
         <div class="Record">
            <div class="Column1">
                Inventory Item
            </div>
            <div class="Column2">
                <asp:DropDownList ID="ddlInventoryItem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInventoryItem_SelectedIndexChanged" >
                    <asp:ListItem Value="0" Text="Select Inventory"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="RequiredFieldValidator4" ValidationGroup="SoftwareDetails"
                    InitialValue="0" runat="server" ControlToValidate="ddlInventoryItem" ErrorMessage="Please Select Inventory."></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Available quantity
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtAvailableQuantity" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Issue Quantity
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvQuantity" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                    ErrorMessage="Please Fill the Quantity" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                    Operator="DataTypeCheck" ControlToValidate="txtQuantity" ErrorMessage="Please enter valid quantity."
                    ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                    Operator="LessThanEqual" ValueToCompare="100"  ControlToValidate="txtQuantity" ErrorMessage="Please enter quantity less than available quantity."
                    ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" runat="server" Type="Integer" ValidationGroup="SoftwareDetails"
                    Operator="GreaterThan" ValueToCompare="0"  ControlToValidate="txtQuantity" ErrorMessage="Please enter quantity greater than 0."
                    ForeColor="Red" SetFocusOnError="true"></asp:CompareValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Date of Issue
            </div>
            <div class="Column2 calendar">
                <asp:TextBox ID="txtDateOfIssue" runat="server"  ReadOnly="true" ></asp:TextBox>
                <rjs:PopCalendar ID="pcPrValidFrom" runat="server" ShowToday="true" From-Today="false"
                    ValidationGroup="" TextMessage="*"  Control="txtDateOfIssue" Format="dd mmm yyyy" />
                <rjs:PopCalendarMessageContainer ID="pcmPrValidFrom" runat="server" Calendar="pcPrValidFrom" />
                    
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
                ErrorMessage="Please Select Date of Issue" ControlToValidate="txtDateOfIssue"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="Record">
            <div class="Column1">
                Issuer Name
            </div>
            <div class="Column2">
                <asp:TextBox ID="txtIssuerName" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <%--<asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />--%>
        <asp:LinkButton ID="btnAdd" CssClass="SubmitButton" runat="server" OnClick="btnAdd_Click"
            ValidationGroup="SoftwareDetails">Save</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" CssClass="SubmitButton" PostBackUrl="~/Dashboard.aspx"
            CausesValidation="false" runat="server">Cancel</asp:LinkButton>
    </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
