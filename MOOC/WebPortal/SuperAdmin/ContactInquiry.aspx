<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactInquiry.aspx.cs" MasterPageFile="~/Master/AdminMasterPage.master"
    Inherits="SuperAdmin_ContactInquiry" Title="Solve Student's Queries"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="View" runat="server">
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Student Contact Details
            </h3>
        </div>
        <div class="Record">
            <asp:GridView ID="gvStudentsContactDetails" PageSize="100" runat="server" Visible="true"
                AllowPaging="true" AutoGenerateColumns="false" AllowSorting="false" CellPadding="4"
                CssClass="Grid" Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White"
                BorderColor="#CCCCCC" EmptyDataText="Student haven't sent any query yet" BorderStyle="None"
                BorderWidth="1px" EnableModelValidation="true" OnRowCommand="gvStudentsContactDetails_RowCommand"
                OnPageIndexChanging="gvStudentsContactDetails_PageIndexChanging" OnRowDataBound="gvStudentsContactDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="User Name" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblStudentName" CssClass="gridRows" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="College">
                        <ItemTemplate>
                            <asp:Label ID="gvlblCollegeName" CssClass="gridRows" runat="server" Text='<%#Eval("CollegeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobil No." Visible ="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblMobilNo" CssClass="gridRows" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Posted">
                        <ItemTemplate>
                            <asp:Label ID="gvlblDatePosted" runat="server" Text='<%#Eval("DatePosted") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Query" SortExpression="CourseName" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="gvlblQuery" runat="server" Text='<%#Eval("Query") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="gvlblEamil" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <ItemTemplate>
                            <asp:Label ID="gvlblStatus" runat="server" Text='<%#Eval("queryStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkClose" runat="server" Text="Close" CommandArgument='<%#Eval("Id") + "-" + Eval("UserName") %>'
                                CommandName="Close"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <a href="ViewandReplayQuery.aspx?id=<%#Eval("Id") + "&username=" + Eval("UserName") %>">view & response</a>                                
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BorderWidth="0" BorderStyle="None" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            </asp:GridView>
        </div>
        <div class="Record">
            <asp:Button ID="btnCloseAll" runat="server" Text="Close All" CssClass="btn btn-success"
                OnClientClick="return confirm('Are you sure to close all queries?');" OnClick="btnCloseAll_Click"/>
            <asp:Button ID="bntCancel" runat="server" Text="Cancel" CssClass="btn" PostBackUrl="~/SuperAdmin/Dashboard.aspx"/>
        </div>
    </div>
</asp:Content>
