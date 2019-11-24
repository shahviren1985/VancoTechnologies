<%@ Page Title="MOOC Academy - Change Password" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="content-container" style="margin-left: 0%; width: 98%; padding: 0%;">
        <div class="content" style="margin-left: 12%; width: 74%; border: 1px solid #CCC; box-shadow: 0 2px 2px #999; padding: 2%;">
            <h2 style="font-size: 31.5px;">Change Password</h2>
            <br />
            <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
            </div>
            <div id="errorSummary" class="ErrorContainer" runat="server" visible="false" style="width: 100%;">
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:TextBox ID="txtoldPassword" Style="min-height: 30px;" TextMode="Password" runat="server"
                        placeholder="Old Password" autocomplete='off'></asp:TextBox>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:TextBox ID="txtNewPassword" Style="min-height: 30px;" TextMode="Password" runat="server"
                        placeholder="Type New Password"></asp:TextBox>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:TextBox ID="txtRetypeNewPassword" Style="min-height: 30px;" TextMode="Password"
                        runat="server" placeholder="Re-type New Password"></asp:TextBox>
                </div>
            </div>
            <div class="Record">
                <div class="Column2">
                    <asp:Button ID="btnChangePwd" runat="server" Text="Change Password" CssClass="btn btn-primary"
                        OnClick="btnChangePwd_Click" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        var index = document.location.href.lastIndexOf('ref=');
        var ref = 'n';
        if (index != -1)
            ref = document.location.href.substring(document.location.href.lastIndexOf('ref=') + 4);

        if (ref == 'ps') {
            $("#chgnePs").hide();
            $("#currentCourse").hide();
            $("#A1").hide();
            $("#homeLink").hide();
        }
        else {
            $("#currentCourse").hide();
        }

        $("#txtSearch").val("");
        $(".search-container").hide();

    </script>
</asp:Content>
