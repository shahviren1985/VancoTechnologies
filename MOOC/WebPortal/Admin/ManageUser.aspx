<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageUser.aspx.cs" MasterPageFile="~/Master/AdminMasterPage.master"
    Inherits="Admin_ManageUser" Title="Mange Students" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>Manage Students
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left; color: Red; float: left; width: 100%;"
        visible="false">
    </div>
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
        Student Details Updated Successfully.
    </div>
    <div class="Record">
        <div class="Column1">
            Select College
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlCollege" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                InitialValue="0" ForeColor="Red" ErrorMessage="Please select college" ValidationGroup="Search"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            User Name
        </div>
        <div class="Column2">
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            First or Last Name
        </div>
        <div class="Column2">
            <asp:TextBox ID="txtFirstLastName" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="Record">
        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-primary"
            ValidationGroup="Search" OnClick="btnSearch_Click" />
        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn" PostBackUrl="~/Admin/Dashboard.aspx" />
    </div>
    <div class="Record">
        <asp:GridView ID="gvUserDetails" PageSize="25" runat="server" Visible="true" AllowPaging="true"
            AutoGenerateColumns="false" AllowSorting="false" CellPadding="4" CssClass="Grid"
            Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            EmptyDataText="Your no student found according to you search inputs" BorderStyle="None"
            BorderWidth="1px" EnableModelValidation="true" OnPageIndexChanging="gvUserDetails_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Sr No.">
                    <ItemTemplate>
                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="gvlblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="gvlblStudent" runat="server" Text='<%#Eval("StudentFullName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="gvlblCourse" CssClass="gridRows" runat="server" Text='<%#Eval("Course") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="gvlblUserName" CssClass="gridRows" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is Active">
                    <ItemTemplate>
                        <asp:CheckBox ID="gvchkIsActive" runat="server" Checked='<%#Eval("IsActive") %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <a data-toggle="modal" href="#RegisterStudent" onclick="ShowModul(this,'cp');" userid='<%#Eval("username") %>'
                            password='<%#Eval("Password") %>' id="aViewPaper">Change Password</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <a data-toggle="modal" href="#ChangeUserName" onclick="ShowModul(this,'cu');" userid='<%#Eval("username") %>'
                            password='<%#Eval("Password") %>' id="aViewPaper">Change Username</a>
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
    <asp:HiddenField ID="hfSelectedUserName" runat="server" />
    <div class="Record" style="margin-top: 15px">
        <div class="Column2">
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" CausesValidation="true"
                Visible="false" ValidationGroup="VGSubmit" Text="Update Active Flag" OnClick="btnUpdate_Click" />
        </div>
    </div>
    <div class="modal fade" id="RegisterStudent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Change Password</h4>
                </div>
                <div class="modal-body">
                    <div class="Record">
                        <div class="Column2">
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtOldPassword" runat="server" placeholder="Old Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtNewPassword" runat="server" placeholder="Type New Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtRetypeNewPassword" runat="server" placeholder="Re-type New Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="Javascript:void(0)" class="btn" data-dismiss="modal" onclick="$('#RegisterStudent').fadeOut(500);"
                        title="click to close popup">Close</a>
                    <asp:Button ID="btnChangePwd" runat="server" Text="Change Password" CssClass="btn btn-primary"
                        OnClientClick="return ChangePassword();" OnClick="btnChangePwd_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <%--Show change username/email popup--%>
    <div class="modal fade" id="ChangeUserName" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Change Username/Email</h4>
                </div>
                <div class="modal-body">
                    <div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtOldUsername" runat="server" placeholder="Old Username"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtNewUsername" runat="server" placeholder="Type New Username"></asp:TextBox>
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                            <asp:TextBox ID="txtConfirmUsername" runat="server" placeholder="Re-type New Username"></asp:TextBox>
                            <asp:HiddenField ID="hfUserPassword" runat="server" />
                        </div>
                    </div>
                    <div class="Record">
                        <div class="Column2">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="Javascript:void(0)" class="btn" data-dismiss="modal" onclick="$('#ChangeUserName').fadeOut(500);"
                        title="click to close popup">Close</a>
                    <asp:Button ID="btnChangeUsername" runat="server" Text="Change Username" CssClass="btn btn-primary"
                        OnClientClick="return ChangeUsername();" OnClick="btnChangeUsername_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        function ShowModul(a, t) {
            var user = $(a).attr("userId");
            var pass = $(a).attr("password");
            if (t == "cp") {
                $("#Content_txtOldPassword").val(pass);
                $("#Content_hfSelectedUserName").val(user);
                $("#Content_lblUserName").html("User Name: <b>" + user + "</b>");
            }
            else {
                $("#Content_txtOldUsername").val(user);
                $("#Content_hfUserPassword").val(pass);
            }
        }

        function ChangePassword() {
            if ($("#Content_txtOldPassword").val() == "") {
                $("#Content_txtOldPassword").focus();
                alert("Please enter old password");
                return false;
            }

            if ($("#Content_txtNewPassword").val() == "") {
                $("#Content_txtNewPassword").focus();
                alert("Please enter new password");
                return false;
            }

            if ($("#Content_txtRetypeNewPassword").val() == "") {
                $("#Content_txtRetypeNewPassword").focus();
                alert("Please re-type new password");
                return false;
            }

            if ($("#Content_txtRetypeNewPassword").val() != $("#Content_txtNewPassword").val()) {
                alert("New password and retype password not match");
                $("#Content_txtRetypeNewPassword").val("");
                $("#Content_txtRetypeNewPassword").focus();
                return false;
            }
        }

        function ChangeUsername() {
            if ($("#Content_txtOldUsername").val() == "") {
                $("#Content_txtOldUsername").focus();
                alert("Please enter old username");
                return false;
            }

            if ($("#Content_txtNewUsername").val() == "") {
                $("#Content_txtNewUsername").focus();
                alert("Please enter new username");
                return false;
            }

            if ($("#Content_txtConfirmUsername").val() == "") {
                $("#Content_txtConfirmUsername").focus();
                alert("Please re-type new username");
                return false;
            }

            if ($("#Content_txtNewUsername").val() != $("#Content_txtConfirmUsername").val()) {
                alert("New password and retype password not match");
                $("#Content_txtConfirmUsername").val("");
                $("#Content_txtConfirmUsername").focus();
                return false;
            }
        }
    </script>
</asp:Content>
