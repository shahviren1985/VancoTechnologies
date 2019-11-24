<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageContent.aspx.cs" Inherits="Staff_ManageContent"
    MasterPageFile="~/Master/StaffMasterPage.master" Title="Manage Section Content" %>

<asp:Content ID="pageContentHolder" ContentPlaceHolderID="Content" runat="server">
    <div>
        <asp:HiddenField ID="hfSelectedSectionId" runat="server" />
        <asp:HiddenField ID="hfIsEdit" runat="server" />
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 100%;" visible="false">
        </div>
        <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            Section Updated Successfully.
        </div>
        <div id="View" runat="server">
            <div style="margin-top: 0%;">
                <div id="logo" style="float: left;">
                    <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                </div>
                <h3>
                    Manage Section Content
                </h3>
            </div>
            <div>
                <ul class="breadcrumb">
                    <li><a href="Dashboard.aspx" target="_top">Home</a><a></a> <span class="divider">/</span></li>
                    <li>Manage Section Content</li>
                </ul>
            </div>
            <div class="Record">
                <div class="Column1">
                    <asp:Label ID="lblSelectCourse" Text="Select Course" runat="server">
                    </asp:Label>
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlCourseId" runat="server" Width="220px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCourseId_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvfddlCourseId" InitialValue="0" ForeColor="Red"
                        ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course name!">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <div class="Column1">
                    <asp:Label ID="lblSelectedChapter" Text="Select Chapter" runat="server">
                    </asp:Label>
                </div>
                <div class="Column2">
                    <asp:DropDownList ID="ddlChapterId" runat="server" Width="220px" OnSelectedIndexChanged="ddlChapterId_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlChapterId" InitialValue="0" ForeColor="Red"
                        ValidationGroup="VGSubmit" ControlToValidate="ddlChapterId" runat="server" ErrorMessage="Please select chapter name!">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="Record">
                <asp:GridView ID="gvSectionDetails" PageSize="10" runat="server" Visible="true" AllowPaging="false"
                    AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                    Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    EmptyDataText="You haven't added any section yet!" BorderStyle="None" BorderWidth="1px"
                    EnableModelValidation="true" OnPageIndexChanging="gvSectionDetails_PageIndexChanging"
                    OnRowCommand="gvSectionDetails_RowCommand" 
                    onrowdatabound="gvSectionDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No.">
                            <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section Title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblSectionName" CssClass="gridRows" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="EditContent"
                                    runat="server" CssClass="gridRows">Edit Content</asp:LinkButton>
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
        </div>
        <div class="Record">
            <div class="Column2">
                <asp:Button ID="btnCancel" runat="server" CssClass="btn convert" Text="Back" PostBackUrl="~/Staff/Dashboard.aspx" />
            </div>
        </div>
    </div>
</asp:Content>
