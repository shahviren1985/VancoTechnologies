<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Admin_ManageUsers" %>

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
        <h3>Manage Users</h3>
        <div class="record">
            <input type="radio" id="add-user" value="0" checked="checked" name="user" />Add New User<br />
            <input type="radio" id="update-user" value="1" name="user" />Update Existing Users<br />
        </div>
        <div class="grid">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Found" OnRowCommand="gvUsers_OnRowCommand"
                        PageSize="50" OnRowDataBound="gvUsers_DataBound" OnPageIndexChanging="gvUsers_OnPageIndexChanging"
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
                                    <asp:LinkButton ID="lnkedit" CommandArgument='<%#Eval ("UserName") %>' CausesValidation="false"
                                        runat="Server" CommandName="OnEdit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div runat="server" id="EditForm" visible="false" style="float: left; width: 100%; margin-top: 20px;">
                        <div>
                            <asp:TextBox runat="server" ID="txtEditUserName" placeholder="User name" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditUserName" Display="Dynamic" ErrorMessage="User name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditUserName" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditFirstName" placeholder="First Name"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditFirstName" Display="Dynamic" ErrorMessage="First name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditFirstName" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditLastName" placeholder="Last Name"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditLastName" Display="Dynamic" ErrorMessage="Last name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtEditLastName" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="txtEditEmailAddress" placeholder="Email Address"></asp:TextBox>
                        </div>

                        <div>
                            <asp:DropDownList runat="server" ID="ddEditRole" AutoPostBack="false"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEditRole" Display="Dynamic" ErrorMessage="Role is mandatory field" Text="*" ForeColor="Red" ControlToValidate="ddEditRole" InitialValue="-1" ValidationGroup="UserEditForm"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <input type="checkbox" runat="server" id="chkEditActiveUser" checked="checked" class="active-user" name="user" />Is Active User<br />
                        </div>
                        <div class="department-container">
                        </div>
                        <div style="margin-top: 20px;">
                            <asp:Button runat="server" ID="btnUpdate" Text="Save" OnClick="btnUpdate_Click" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="UserEditForm" />
                        </div>
                    </div>
                    <div runat="server" id="ChangePasswordForm" style="float: left; width: 100%; margin-top: 20px;">
                        <div>
                            <h6>Change Password</h6>
                        </div>
                        <div id="cpStatus" runat="server"></div>
                        <br />
                        <asp:HiddenField ID="hfUserName" runat="server" />
                        <asp:TextBox runat="server" ID="txtOldPassword" placeholder="Old Password"></asp:TextBox><br />
                        <asp:TextBox ID="txtUPassword" runat="server" placeholder="New Password"></asp:TextBox><br />
                        <asp:TextBox ID="txtURePassword" runat="server" placeholder="Re-enter New Password"></asp:TextBox><br />
                        <asp:Button runat="server" ID="btnChangePassword" CssClass="btn btn-success" CausesValidation="false" OnClick="btnChangePassword_Click" Text="Change Password" />
                        <br /><br />
                        <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnDeleteUser_Click" Text="Delete Current User" />
                        
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvUsers" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div class="form">
            <div runat="server" id="Error" style="color: red"></div>
            <div>
                <asp:TextBox runat="server" ID="txtUserName" placeholder="User name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvUserName" Display="Dynamic" ErrorMessage="User name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtUserName" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvPassword" Display="Dynamic" ErrorMessage="Password is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtPassword" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtFirstName" placeholder="First Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFirstName" Display="Dynamic" ErrorMessage="First name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtLastName" placeholder="Last Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvLastName" Display="Dynamic" ErrorMessage="Last name is mandatory field" Text="*" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtEmail" placeholder="Email Address"></asp:TextBox>
            </div>

            <div>
                <asp:DropDownList runat="server" ID="ddRole" AutoPostBack="false"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvRole" Display="Dynamic" ErrorMessage="Role is mandatory field" Text="*" ForeColor="Red" ControlToValidate="ddRole" InitialValue="-1" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
            </div>
            <div>
                <input type="checkbox" runat="server" id="chkActive" checked="checked" class="active-user" name="user" />Is Active User<br />
            </div>
            <div class="department-container">
                <%--<asp:TextBox runat="server" ID="txtDepartment" placeholder="Departments" class="departments"></asp:TextBox>--%>
            </div>
            <asp:HiddenField runat="server" ID="hfDepartments" />
            <div style="margin-top: 20px;">
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClientClick="PopulateDepartmentDetails();" OnClick="btnSave_Click" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="UserForm" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function PopulateDepartmentDetails() {
            var dept = "";
            $("#department").find("li").each(function () {
                if ($(this).find("span.tagit-label").html() != undefined)
                    dept += $(this).find("span.tagit-label").html() + ",";
            });


            if (dept.length > 2)
                $("#<%=hfDepartments.ClientID%>").val(dept.substring(0, dept.length - 1));
        }

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

            var department = new FormBuilder().GetTagField("department");
            $(".department-container").append(department);
            $('#department').tagit();
        });
    </script>
</asp:Content>

