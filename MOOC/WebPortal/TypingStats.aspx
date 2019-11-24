<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="TypingStats.aspx.cs" Inherits="TypingStats" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <link rel="Stylesheet" href="static/styles/chapter-style.css" />
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>

    <div class="content-container" style="width: 80%; margin-left: 10%; margin-bottom: 5%;">
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><span class='divider'>/</span></li>
                <li>Typing Stats</li>
            </ul>
        </div>
        <div>
            <div class="Record">
                <asp:GridView ID="gvUserDetails" PageSize="200" runat="server" AllowPaging="false"
                    AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                    Width="50%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    EmptyDataText="You haven't started typing tutorial yet" BorderStyle="None" BorderWidth="1px" Style="line-height: 20px;" OnRowDataBound="gvUserDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Typing Levels">
                            <ItemTemplate>
                                <asp:Label ID="gvlblLevel" runat="server" Text='<%#Eval("Level") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level Status">
                            <ItemTemplate>
                                <asp:Label ID="gvlblStatus" CssClass="gridRows" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completed Date">
                            <ItemTemplate>
                                <asp:Label ID="gvlblDate" CssClass="gridRows" runat="server" Text='<%#Eval("CompletedDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BorderWidth="0" BorderStyle="None" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" />
                    <RowStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //BASE_URL = '<%=Util.BASE_URL%>';
        var drop = $("#drop");
        $(drop).css("display", "none");

        //function PopulateCourseName() {
        var drop = $("#label");
        $(drop).css("display", "block");
        $(drop).html("");
    </script>
</asp:Content>

