<%@ Page Title="Migrate Course to Different Databases" Language="C#" MasterPageFile="~/Master/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="MigrateCourseToDifferentDB.aspx.cs" Inherits="SuperAdmin_MigrateCourseToDifferentDB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-top: 0%;">
        <div id="logo" style="float: left;">
            <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
        </div>
        <h3>
            Migrate Course to Different Databases
        </h3>
    </div>
    <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
        color: Red; float: left; width: 100%;" visible="false">
    </div>
    <div id="Success" class="SuccessContainer" runat="server" visible="false" style="width: 100%;">
        Section Updated Successfully.
    </div>
    <div class="Record">
        <div class="Column1">
            Source Connection-String
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlSourceCnxnString" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSourceCnxnString_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rvfddlddlSourceCnxnString" InitialValue="0" ForeColor="Red"
                ValidationGroup="VGSubmit" ControlToValidate="ddlSourceCnxnString" runat="server">*
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            Course
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlCourse" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlCourse" InitialValue="0" ForeColor="Red" ValidationGroup="VGSubmit"
                ControlToValidate="ddlCourse" runat="server">*
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record">
        <div class="Column1">
            Destination Connection-String
        </div>
        <div class="Column2">
            <asp:DropDownList ID="ddlDestinationCnxnString" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDestinationCnxnString" InitialValue="0" ForeColor="Red"
                ValidationGroup="VGSubmit" ControlToValidate="ddlDestinationCnxnString" runat="server">*
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="Record" id="buttons">
        <asp:Button ID="btnMoveCourse" CssClass="btn btn-success" runat="server" Text="Migrated Course"
            OnClick="btnMoveCourse_Click" ValidationGroup="VGSubmit" OnClientClick="return ValidateControls();"/>
        <asp:Button ID="btnCancel" CssClass="btn" runat="server" Text="Cancel" PostBackUrl="~/SuperAdmin/Dashboard.aspx" />
    </div>
    <div class="Record" id="lodingLabel" style="display: none">
        <%--<img src="../static/images/loading.gif" alt="migrating please wait" />--%>
        <label class="btn btn-large-primary">Migrating please wait...</label>
    </div>

    <script type="text/javascript">
        function ValidateControls() {
            var sourse = $("#Content_ddlSourceCnxnString").val();
            var destination = $("#Content_ddlDestinationCnxnString").val();
            var course = $("#Content_ddlCourse").val();

            if (sourse == "0" || sourse == "") {
                alert("Please select sourse college");
                return false;
            }

            if (destination == "0" || destination == "") {
                alert("Please select destination college");
                return false;
            }

            if (course == "0" || course == "") {
                alert("Please select course");
                return false;
            }

            if (sourse.toLowerCase() == destination.toLowerCase()) {
                alert("Source and destination college should be different.");
                return false;
            }

            $("#buttons").attr("style", "display:none");
            $("#lodingLabel").attr("style", "display:block");

            return true;
        }
    </script>
</asp:Content>
