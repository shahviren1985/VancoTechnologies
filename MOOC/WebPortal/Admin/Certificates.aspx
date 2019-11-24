<%@ Page Title="Certificates" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Certificates.aspx.cs" Inherits="Admin_Certificates" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script type="text/javascript" src="../static/zebra_datepicker.src.js"></script>
    <link href="../static/styles/zebra-css.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            // assuming the controls you want to attach the plugin to 
            // have the "datepicker" class set
            $("#Content_txtCertIssueDate").Zebra_DatePicker({
                format: 'F d, Y',
                default_position: "below"                
            });
        });
    </script>
    <div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left; color: Red; float: left; width: 100%;"
                    visible="false">
                </div>
                <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
                </div>
                <div id="View" runat="server">
                    <div style="margin-top: 5%;">
                        <div id="logo" style="float: left;">
                            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
                        </div>
                        <h3>Print Certificate
                        </h3>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Select Course
                        </div>
                        <div class="Column2">
                            <asp:DropDownList ID="ddlCourseId" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlCourseId" InitialValue="0" ForeColor="Red"
                                ValidationGroup="VGSubmit" ControlToValidate="ddlCourseId" runat="server" ErrorMessage="Please select course name!">*
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Select Stream
                        </div>
                        <div class="Column2">
                            <asp:DropDownList ID="ddlStream" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlStream" InitialValue="0" ForeColor="Red" ValidationGroup="VGSubmit"
                                ControlToValidate="ddlStream" runat="server" ErrorMessage="Please select course name!">*
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column1">
                            Certificate Issue Date
                        </div>
                        <div class="Column2">
                            <asp:TextBox ID="txtCertIssueDate" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red"
                                ValidationGroup="VGSubmit" ControlToValidate="txtCertIssueDate" runat="server" ErrorMessage="Please select date!">*
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" ValidationGroup="VGSubmit"
                                Text="Print" OnClick="btnPrint_Click" />
                            <asp:Button ID="btnPrintAll" runat="server" CssClass="btn btn-success" Text="Print All"
                                OnClick="btnPrintAll_Click" ValidationGroup="VGSubmit" />
                            <asp:Button ID="btnCancle" runat="server" CssClass="btn convert" Text="Cancel" PostBackUrl="~/Admin/Dashboard.aspx" />
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2" style="width: 100%;">
                            <a id="front_Certificate" runat="server" class="btn btn-link" target="_blank"></a>
                            <a id="back_Certificate" runat="server" class="btn btn-link" target="_blank"></a>
                            <a id="notPrintStudents" runat="server" class="btn btn-link" target="_blank"></a>
                        </div>
                    </div>
                    <div class="Record">
                        <asp:GridView ID="gvUserDetails" PageSize="200" runat="server" Visible="true" AllowPaging="true"
                            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
                            Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                            EmptyDataText="You haven't any user yet" BorderStyle="None" BorderWidth="1px"
                            EnableModelValidation="true" OnRowCommand="gvUserDetails_RowCommand" OnPageIndexChanging="gvUserDetails_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlknbtnPrint" runat="server" CommandArgument='<%#Eval("username") %>'
                                            CommandName="Print">Print</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblLastName" CssClass="gridRows" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFirstName" runat="server" Text='<%#Eval("firstName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Father Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblFather" CssClass="gridRows" runat="server" Text='<%#Eval("FatherName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mother Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMother" CssClass="gridRows" runat="server" Text='<%#Eval("MotherName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No.">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblMobile" CssClass="gridRows" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
