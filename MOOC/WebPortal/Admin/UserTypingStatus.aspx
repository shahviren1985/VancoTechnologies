<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserTypingStatus.aspx.cs"
    MasterPageFile="~/Master/AdminMasterPage.master" Inherits="Admin_UserTypingStatus" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
        color: Red; float: left; width: 100%;" visible="false">
    </div>
    <div id="View" runat="server">
        <div class="Record">
            <div style="margin-top: 5%;">
                <div id="logo" style="float: left;">
                    <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                </div>
                <h3>
                    User Typing Performance Report
                </h3>
                <div class="Record">
                    <div class="Column2">
                        <asp:DropDownList ID="ddlLevel" runat="server" ValidationGroup="VGSubmit" Width="170px">
                            <asp:ListItem Text="--Select Level--" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Level 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Level 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Level 3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Level 4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Level 5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Level 6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Level 7" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Level 8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Level 9" Value="9"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlLevel" InitialValue="0" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="ddlLevel" runat="server" ErrorMessage="Please select Level!">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlCourse" runat="server" Width="170px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlCourse" InitialValue="0" ForeColor="Red" ValidationGroup="VGSubmit"
                            ControlToValidate="ddlCourse" runat="server" ErrorMessage="Please select Course!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="Record">
                    <div class="Column2">
                        <asp:Button ID="btnGetUser" runat="server" CssClass="btn btn-success" Text="Get Users"
                            CausesValidation="true" ValidationGroup="VGSubmit" OnClick="btnGetUser_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn convert" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
                    </div>
                </div>
            </div>
            <asp:GridView ID="gvTypingPerformance" PageSize="20" runat="server" Visible="true"
                AllowPaging="true" AutoGenerateColumns="false" AllowSorting="false" CellPadding="4"
                CssClass="Grid" Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White"
                BorderColor="#CCCCCC" EmptyDataText="You haven't added any user yet" BorderStyle="None"
                BorderWidth="1px" EnableModelValidation="true" OnPageIndexChanging="gvTypingPerformance_PageIndexChanging">
                <%-- OnRowCommand="gvChapterDetails_RowCommand" OnPageIndexChanging="gvChapterDetails_PageIndexChanging">--%>
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("FirstName") + " " + Eval("LastName") + " " + Eval("FatherName") + " "+ Eval("MotherName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lavel">
                        <ItemTemplate>
                            <asp:Label ID="gvlblLevel" CssClass="gridRows" runat="server" Text='<%#Eval("Level") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time (M : S)">
                        <ItemTemplate>
                            <asp:Label ID="gvlblTimeSpend" runat="server" Text='<%#Eval("TimeSpanInNormal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accuracy" SortExpression="CourseName">
                        <ItemTemplate>
                            <asp:Label ID="gvlblAccuracy" runat="server" Text='<%#Eval("Accuracy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GrossWPM" SortExpression="CourseName">
                        <ItemTemplate>
                            <asp:Label ID="gvlblGrossWPM" runat="server" Text='<%#Eval("GrossWPM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NetWPM" SortExpression="CourseName">
                        <ItemTemplate>
                            <asp:Label ID="gvlblNetWPM" runat="server" Text='<%#Eval("NetWPM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BorderWidth="0" BorderStyle="None" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" />
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:Button ID="btnSavePDF" runat="server" CssClass="btn btn-success" Text="Save as PDF"
                    Visible="false" OnClick="btnPrint_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
