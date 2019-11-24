<%@ Page Title="Content Report Issue" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ContentReportIssue.aspx.cs" Inherits="SuperAdmin_ContentReportIssue" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 5%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Report Issue
        </h3>
    </div>
    <div class="Record">
        <asp:GridView ID="gvStudentsContactDetails" PageSize="12" runat="server" Visible="true"
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
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="gvlblEmail" CssClass="gridRows" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expected Content">
                    <ItemTemplate>
                        <asp:Label ID="gvlblExptContent" runat="server" Text='<%#Eval("ExpectedContent") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Chapter" SortExpression="CourseName">
                    <ItemTemplate>
                        <asp:Label ID="gvlblChapter" runat="server" Text='<%#Eval("ChapterName") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Section" SortExpression="CourseName">
                    <ItemTemplate>
                        <asp:Label ID="gvlblSection" runat="server" Text='<%#Eval("SectionName") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Posted" SortExpression="Status">
                    <ItemTemplate>
                        <asp:Label ID="gvlblPostedDate" runat="server" Text='<%#Eval("DatePosted") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="gvlnkClose" runat="server" Text="Fix" CommandArgument='<%#Eval("Id") + "-" + Eval("UserName") %>'
                            CommandName="Close"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
            <FooterStyle BorderWidth="0" BorderStyle="None" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>            
        </asp:GridView>
    </div>
    <div class="Record">
        <asp:Button ID="btnCloseAll" runat="server" Text="Close All" CssClass="btn btn-success"
            OnClientClick="return confirm('Are you sure to close all queries?');" OnClick="btnCloseAll_Click" Visible="false"/>
        <asp:Button ID="bntCancel" runat="server" Text="Cancel" CssClass="btn" PostBackUrl="~/SuperAdmin/Dashboard.aspx"/>
    </div>
</asp:Content>
