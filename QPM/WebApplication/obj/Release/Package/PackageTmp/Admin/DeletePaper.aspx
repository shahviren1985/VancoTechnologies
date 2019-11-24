<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeletePaper.aspx.cs" Inherits="WebApplication.Admin.DeletePaper" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h3>Delete Paper</h3>
        <div class="col-md-6 col-md-offset-3">
            <br />
            <div runat="server" id="gvStatus" class="bg-success"></div>
            <br />
            <div class="Record">
                <div class="Column1">
                    Course
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlCourse" runat="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChange"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlExam" runat="server" ControlToValidate="ddlCourse"
                        InitialValue="0" ForeColor="Red" ErrorMessage="Please Select Course." Text="Please Select Course."
                        Display="Dynamic" ValidationGroup="Create"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <div class="Column1">
                    Subject
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlSubject" runat="server" OnSelectedIndexChanged="ddlSubject_SelectedIndexChange"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSubject" runat="server" ControlToValidate="ddlSubject"
                        InitialValue="0" ForeColor="Red" ErrorMessage="Please Select Subject." Text="Please Select Subject."
                        Display="Dynamic" ValidationGroup="Create"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <div class="Column1">
                    Paper
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlPaper" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlPaper" runat="server" ControlToValidate="ddlPaper" InitialValue="0" ForeColor="Red"
                        ErrorMessage="Please Select Paper." Text="Please Select Paper." Display="Dynamic" ValidationGroup="Create"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="Record">
                <div class="Column1">
                </div>
                <div class="Column2">
                    <asp:Button ID="Button2" runat="server" Text="Filter" OnClick="btnFilter_Click"
                        ValidationGroup="Create" CssClass="btn btn-success" />
                </div>
            </div>

            <div style="clear: both; height: 2em;">
            </div>
            <%--this is gridview of the  staff --%>
            <hr />
            <div id="Grid">
                <%--OnRowDataBound="gvCareerUsers_RowDataBound" OnPageIndexChanging="gvCareerUsers_PageIndexChanging" OnRowCommand="gvCareerUsers_RowCommand" --%>
                <asp:GridView ID="gvPaper" runat="server" AutoGenerateColumns="false" GridLines="Horizontal"
                    Width="100%" CellPadding="4" CssClass="Grid" ForeColor="Black" BackColor="#1C3C52"
                    BorderColor="#CCCCCC" BorderWidth="1px" EmptyDataText="No Data Present" Visible="true"
                    OnRowDataBound="gvCareerUsers_RowDataBound" OnRowCommand="gvCareerUsers_RowCommand"
                    BorderStyle="Dotted" AllowPaging="true" PageSize="100" AllowSorting="True" Font-Size="Medium">
                    <RowStyle BackColor="#EFF3FB" CssClass="details" />
                    <PagerStyle CssClass="pagination" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Top" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="headerStyle" />
                    <AlternatingRowStyle BackColor="White" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id")%>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="gvlblDel" runat="server" Text='<%#Eval("IsUsed")%>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Used Status" ItemStyle-CssClass="nt_title" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="gvlblCheck" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Name" ItemStyle-CssClass="nt_title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblSAId" runat="server" Text='<%#Eval("Subject") %>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paper Title" ItemStyle-CssClass="nt_title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblFAId" runat="server" Text='<%#Eval("Paper") %>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upload Date" ItemStyle-CssClass="nt_title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblDate" runat="server" Text='<%#Eval("Date") %>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--    <asp:TemplateField HeaderText="Document Second" ItemStyle-CssClass="button_Col" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnStatusSec" runat="server" Text="Download" Width="87px" CommandName="downloadLink"
                                    CommandArgument='<%#Eval("DocumentSecond") %>' OnClick="DownloadFile" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        --%>
                        <asp:TemplateField HeaderText="Used Status" ItemStyle-CssClass="nt_title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblUsedStatus" runat="server" Text='<%#Eval("UsedStatus") %>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document First" ItemStyle-CssClass="button_Col" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnStatus" runat="server" Text="Download" Width="87px" CommandName="downloadLink"
                                    CommandArgument='<%#Eval("DocumentFirst") %>' OnClick="DownloadFile" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Used" ItemStyle-CssClass="button_Col" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" runat="server" Text="Delete" Width="87px" CommandName="reqDel"
                                    CommandArgument='<%#Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <%-- <div class="Record">
                    <div class="Column1">
                    </div>
                    <div class="Column2">
                        <asp:Button ID="Button1" runat="server" Text="Update Used Status" OnClick="btnSubmit_Click"
                            ValidationGroup="Create" CssClass="btn btn-primary" />
                    </div>
                </div>--%>
            </div>
        </div>
</asp:Content>
