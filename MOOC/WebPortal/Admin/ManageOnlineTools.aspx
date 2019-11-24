<%@ Page Title="Manage Online Tools" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ManageOnlineTools.aspx.cs" Inherits="Admin_ManageOnlineTools" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Manage Online Tools
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
        color: Red; float: left; width: 100%;" visible="false">
    </div>
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
        Student Details Updated Successfully.
    </div>
    <div class="Record">
        <div class="Column1">
            Select Course
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_Change">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlCourse" runat="server" ControlToValidate="ddlCourse"
                InitialValue="0" ForeColor="Red" ErrorMessage="Please select college" ValidationGroup="Search">*</asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record" id="AddChapter" runat="server" visible="false">
        <div class="Column2">            
            <asp:Button ID="btnAddNewTool" runat="server" CssClass="btn btn-success" Text="Add New Online Tool" OnClick="btnAddNewTool_Click"/>
        </div>
    </div>
    <div class="Record">
        <asp:GridView ID="gvOnlineTools" PageSize="25" runat="server" Visible="true" AllowPaging="false"
            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
            Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            EmptyDataText="You haven't added any chapter yet" BorderStyle="None" BorderWidth="1px"
            EnableModelValidation="true" onrowcommand="gvOnlineTools_RowCommand" 
            onrowdatabound="gvOnlineTools_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Sr No.">
                    <ItemTemplate>
                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="gvlblTitle" CssClass="gridRows" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="gvlblDescription" CssClass="gridRows" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Website">
                    <ItemTemplate>
                        <a target="_blank" href='<%#Eval("ToolURL") %>'>
                            <%#Eval("ToolURL") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Display Date">
                    <ItemTemplate>
                        <asp:Label ID="gvlblDisplayDate" CssClass="gridRows" runat="server" Text='<%#Eval("ToolDisplayDateNormal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:CheckBox ID="gvchkActive" CssClass="gridRows" runat="server" Checked='<%#Eval("IsActive") %>'>
                        </asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditDetails"
                            runat="server" CssClass="gridRows">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="gvlnkbtnDelete" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteDetails"
                            runat="server" CssClass="gridRows" OnClientClick="return confirm('Are you sure to want to delete this online tool?')">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BorderWidth="0" BorderStyle="None" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <RowStyle HorizontalAlign="Left" />
        </asp:GridView>
    </div>
    <div class="Record">
        <div class="Column2">
            <asp:Button ID="btnCancel" runat="server" CssClass="btn convert" Text="Back" PostBackUrl="~/Admin/Dashboard.aspx" />
        </div>
    </div>
</asp:Content>
