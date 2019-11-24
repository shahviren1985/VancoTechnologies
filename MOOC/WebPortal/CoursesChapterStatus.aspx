<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="CoursesChapterStatus.aspx.cs" Inherits="CoursesChapterStatus" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <link rel="Stylesheet" href="static/styles/chapter-style.css" />
    <style>
        .content-container table th, td {
            padding-left: 10px;
        }
    </style>

    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
    </div>
    <asp:HiddenField ID="hfCourseName" Value="[]" runat="server" />

    <div class="content-container" style="width: 80%; margin-left: 10%; margin-bottom: 5%;">
        <div>
            <ul class="breadcrumb">
                <li><a href="Dashboard.aspx" target="_top">Home</a><span class='divider'>/</span></li>
                <li>Course Status</li>
            </ul>
        </div>
        <div>
            <div class="Record">
                <asp:GridView ID="gvUserDetails" PageSize="200" runat="server" AllowPaging="false"
                    AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                    Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    EmptyDataText="You haven't started typing tutorial yet" BorderStyle="None" BorderWidth="1px" Style="line-height: 25px;" OnRowDataBound="gvUserDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Course">
                            <ItemTemplate>
                                <asp:Label ID="gvlblCourse" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chapter">
                            <ItemTemplate>
                                <asp:Label ID="gvlblChapter" runat="server" Text='<%#Eval("Chapter") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="gvlblSection" runat="server" Text='<%#Eval("Section") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="gvlblStatus" CssClass="gridRows" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completed Date">
                            <ItemTemplate>
                                <asp:Label ID="gvlblDate" CssClass="gridRows" runat="server" Text='<%#Eval("Completed Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Review Section">
                            <ItemTemplate>
                                <a href='<%#Eval("Review Section") %>' title="click here to review this section" target="_blank">Review Section</a>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                    </Columns>
                    <FooterStyle BorderWidth="0" BorderStyle="None" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle VerticalAlign="Middle" HorizontalAlign="Left" />
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

        $(drop).html("You are learning - " + $("#Content_hfCourseName").val());
    </script>
</asp:Content>

