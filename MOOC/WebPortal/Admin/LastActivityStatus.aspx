<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LastActivityStatus.aspx.cs"
    Inherits="Admin_LastActivityStatus" MasterPageFile="~/Master/AdminMasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
                    color: Red; float: left; width: 100%;" visible="false">
                </div>
                <%-- <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                    Chapter Updated Successfully.
                </div>--%>
                <div id="View" runat="server">
                    <div style="margin-top: 5%;">
                        <div id="logo" style="float: left;">
                            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                        </div>
                        <h3>
                            Student Last Activity List
                        </h3>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Last Activity:</div>
                        <div class="Column2">
                            <asp:DropDownList ID="ddlNumOfDays" runat="server" Width="170px">
                                <asp:ListItem Text="--Select Days--" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="1 Day" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 Days" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 Days" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4 Days" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5 Days" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6 Days" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7 Days" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8 Days" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9 Days" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10 Days" Value="10"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvfddlNumOfDays" InitialValue="0" ForeColor="Red"
                                ValidationGroup="VGSubmit" ControlToValidate="ddlNumOfDays" runat="server" ErrorMessage="Please select number of days!">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record" id="getButtons" runat="server">
                        <div class="Column2">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" CausesValidation="true"
                                ValidationGroup="VGSubmit" Text="Get Students" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn convert" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
                        </div>
                    </div>
                    <div class="Record">
                        <asp:GridView ID="gvActivityDetails" PageSize="10" runat="server" Visible="true"
                            AllowPaging="true" AutoGenerateColumns="false" AllowSorting="false" CellPadding="4"
                            CssClass="Grid" Width="99%" ForeColor="Black" GridLines="Horizontal" BackColor="White"
                            BorderColor="#CCCCCC" EmptyDataText="You haven't added any chapter yet" BorderStyle="None"
                            BorderWidth="1px" EnableModelValidation="true" OnRowCommand="gvActivityDetails_RowCommand"
                            OnPageIndexChanging="gvActivityDetails_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblChapterName" CssClass="gridRows" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblContentVersion" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Father Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLanguage" CssClass="gridRows" runat="server" Text='<%#Eval("FatherName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Activity" SortExpression="CourseName">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCourseName" runat="server" Text='<%#Eval("LastActivityDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll Number" SortExpression="CourseName">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCourseName" runat="server" Text='<%#Eval("RollNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No." SortExpression="CourseName">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblCourseName" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BorderWidth="0" BorderStyle="None" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    <div class="Record" id="saveButtons" runat="server" style="display: none">
                        <div class="Column2">
                            <asp:Button ID="btnSaveAsPDF" runat="server" CssClass="btn btn-success" Text="Save as PDF"
                                OnClick="btnSaveAsPDF_Click" />
                            <asp:Button ID="btncancelSave" runat="server" CssClass="btn convert" Text="Cancel"
                                OnClick="btnCancelSave_Click" PostBackUrl="~/Admin/Dashboard.aspx" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
