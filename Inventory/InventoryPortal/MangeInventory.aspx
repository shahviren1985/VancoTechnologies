<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="MangeInventory.aspx.cs" Inherits="MangeInventory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href="<%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            SetGridPageStyle();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a>&nbsp;>&nbsp;Manage Inventory Details
    </div>
    <div id="Form">
        <asp:UpdatePanel runat="server" ID="InventoryPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
                    <div class="HeaderText" style="text-align: left; font: bold;">
                        Manage Inventory Details
                    </div>
                    <div id="divresult" runat="server" style="color: Green; margin-left: 20px; text-align: left; margin-top: 2%;"
                        visible="false">
                        Inventory Details Updated Sucessfully.
                    </div>
                    <br />
                </div>
                <div>
                    <%--<div runat="server" id="trade">
                        <div class="Record">
                            <div class="Column1">
                                Department Name</div>
                            <div class="Column2">
                                <asp:DropDownList ID="ddlTradeId" runat="server" OnSelectedIndexChanged="ddlTradeId_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div id="divgrid" runat="server">
                    <div id="divupdatestatus" runat="server">
                    </div>
                    <asp:GridView runat="server" ID="gridedit" DataKeyNames="Id" PageSize="4" AutoGenerateColumns="False"
                        GridLines="None" Width="60%" EmptyDataText="You haven't added any inventory details yet"
                        OnRowCommand="gridedit_RowCommand"
                        OnPageIndexChanging="gridedit_PageIndexChanging"
                        OnRowDataBound="gridedit_RowDataBound">
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
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="gvLnkButton" CommandArgument='<%#Eval("Id")%>' CommandName="Remove"
                                        OnClientClick="return confirm(' Are you sure you want to delete this User?');"
                                        runat="server">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="gvLnkViewButton" CommandArgument='<%#Eval("Id")%>' CommandName="ViewUpdate"
                                        runat="server">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblDepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblName" runat="server" Text='<%#Eval("Name") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblQuantity" runat="server" Text='<%#Eval("Quantity") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="Record" style="margin-top: 10px;">
                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="SubmitButton" Text="Cancel"
                            PostBackUrl="~/Dashboard.aspx"></asp:LinkButton>
                    </div>
                </div>
                <div id="InventoryDetailsForm" runat="server" style="margin-top: 20px" visible="false">
                    <b style="display: block; margin-bottom: 10px;">Edit Inventory Details </b>
                    <div id="Div1" class="Record" runat="server" visible="false">
                        <div class="Column1">
                            Id
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtid" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Department Name
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
                                <%--<asp:ListItem Value="0" Text="Select Category" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Hardware" Text="Hardware"></asp:ListItem>
                                <asp:ListItem Value="Software" Text="Software"></asp:ListItem>
                                <asp:ListItem Value="Stationary" Text="Stationary"></asp:ListItem>
                                <asp:ListItem Value="Chemicals" Text="Chemicals"></asp:ListItem>
                                <asp:ListItem Value="General" Text="General"></asp:ListItem>--%>
                                <asp:ListItem Value="0" Text="Select Category" Selected="True"></asp:ListItem>
                                <%--<asp:ListItem Value="Hardware" Text="Hardware"></asp:ListItem>
                                <asp:ListItem Value="Software" Text="Software"></asp:ListItem>--%>
                                <asp:ListItem Value="Examination" Text="Examination"></asp:ListItem>
                                <%--<asp:ListItem Value="Chemicals" Text="Chemicals"></asp:ListItem>--%>
                                <asp:ListItem Value="General Stationary" Text="General Stationary"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Style="color: Red;" Display="Dynamic" ID="RequiredFieldValidator1"
                                ValidationGroup="SoftwareDetails" InitialValue="0" runat="server" ControlToValidate="ddlDepartment"
                                ErrorMessage="Please Select Category"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Item Name
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ForeColor="Red" ValidationGroup="SoftwareDetails"
                                ErrorMessage="Please Fill the Name" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Specification
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtSpecifiation" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSpecification" runat="server" ForeColor="Red" ErrorMessage="Please Fill the Specification"
                                ControlToValidate="txtSpecifiation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Quantity (Current)
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ForeColor="Red" ValidationGroup="SoftwareDetails"
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
                                ValidationGroup="SoftwareDetails" ControlToValidate="txtVendor"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Price Purchased
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPrice" ForeColor="Red" runat="server" ValidationGroup="SoftwareDetails"
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
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Remarks
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <asp:LinkButton ID="btnUpdate" CssClass="SubmitButton" CausesValidation="true" runat="server"
                            ValidationGroup="SoftwareDetails" OnClick="btnUpdate_Click">save</asp:LinkButton>
                        <asp:LinkButton ID="lbtnGoBack" CssClass="SubmitButton" OnClick="lbtnGoBack_Click"
                            CausesValidation="false" runat="server">Cancel</asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridedit" />
                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                <asp:AsyncPostBackTrigger ControlID="lbtnGoBack" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
