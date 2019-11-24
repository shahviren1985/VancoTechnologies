<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="ManageStoreLocations.aspx.cs" Inherits="Admin_ManageStoreLocations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Correspondance Management System :: Manage Users</title>
    <link href="../static/stylesheets/bootstrap.min.css" rel="stylesheet" />
    <link href="../static/stylesheets/jquery.tagit.css" rel="stylesheet" type="text/css" />
    <link href="../static/stylesheets/tagit.ui-zendesk.css" rel="stylesheet" type="text/css" />

    <script src="../static/scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../static/scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../static/scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../static/scripts/tag-it.js" type="text/javascript"></script>
    <script src="../static/scripts/controls/aa.controls.js" type="text/javascript"></script>
    <script src="../static/scripts/controls/aa.communicate.js" type="text/javascript"></script>
    <style type="text/css">
        .form-container {
            margin: 20px;
            margin-left: 10%;
        }

        #add-user, #update-user, .active-user {
            margin-right: 10px !important;
            margin-top: -5px;
        }

        .grid {
            margin-top: 20px;
            display: none;
        }

        .form {
            margin-top: 20px;
        }

        #department {
            margin: 0px;
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-container">
        <h3>Manage Storage Location</h3>
        <div class="record">
            <input type="radio" id="add-user" value="0" checked="checked" name="user" />Add New Store Location<br />
            <input type="radio" id="update-user" value="1" name="user" />Update Existing Store Locations<br />
        </div>
        <div class="grid">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <asp:GridView ID="gvLocations" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Found" OnRowCommand="gvLocations_OnRowCommand"
                        PageSize="50" OnRowDataBound="gvLocations_DataBound" OnPageIndexChanging="gvLocations_OnPageIndexChanging"
                        Style="font-size: 13px; float: left;" CssClass="StudentGrid">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="#2E87C8" />
                        <PagerStyle BackColor="#FFFFFF" ForeColor="#2E87C8" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#2E87C8" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" CommandArgument='<%#Eval ("Id") %>' CausesValidation="false"
                                        runat="Server" CommandName="OnEdit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div runat="server" id="EditForm" visible="false" style="float: left; width: 100%; margin-top: 20px;">
                        <div>
                            <asp:TextBox runat="server" ID="txtEditRoomNumber" placeholder="Room Number"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditUserName" Display="Dynamic" ErrorMessage="Room number is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditRoomNumber" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditCupbord" placeholder="Cupbord Name"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditFirstName" Display="Dynamic" ErrorMessage="Cupbord name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditCupbord" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditShelf" placeholder="Shelf Name"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditLastName" Display="Dynamic" ErrorMessage="Shelf name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditShelf" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditFileName" placeholder="File Name"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ErrorMessage="File name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditFileName" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditComments" placeholder="Comments" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <asp:HiddenField ID="hfLocationId" runat="server" Value="0"/>
                        <div style="margin-top: 20px;">
                            <asp:Button runat="server" ID="btnUpdate" Text="Save" OnClick="btnUpdate_Click" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="UserEditForm" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvLocations" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div class="form">
            <div runat="server" id="Error" style="color: red"></div>
            <div>
                <asp:TextBox runat="server" ID="txtRoomNumber" placeholder="Room Number"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvUserName" Display="Dynamic" ErrorMessage="Room number is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtRoomNumber" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtCupbord" placeholder="Cupbord Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvPassword" Display="Dynamic" ErrorMessage="Cupbord is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtCupbord" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtShelf" placeholder="Shelf Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFirstName" Display="Dynamic" ErrorMessage="Shelf is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtShelf" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtFileName" placeholder="File Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvLastName" Display="Dynamic" ErrorMessage="File name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtFileName" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtComments" placeholder="Comments" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div style="margin-top: 20px;">
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="UserForm" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#add-user").click(function () {

                if ($(this).prop("checked") == "true") {
                    $(".form").hide();
                    $(".grid").show();
                }
                else {
                    $(".form").show();
                    $(".grid").hide();
                }
            });

            $("#update-user").click(function () {
                if ($(this).prop("checked") == "true") {

                    $(".form").show();
                    $(".grid").hide();
                }
                else {
                    $(".form").hide();
                    $(".grid").show();
                }
            });

            /*var department = new FormBuilder().GetTagField("department");
            $(".department-container").append(department);
            $('#department').tagit();*/
        });
    </script>
</asp:Content>