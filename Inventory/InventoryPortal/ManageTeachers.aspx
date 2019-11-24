<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.master" AutoEventWireup="true"
    CodeFile="ManageTeachers.aspx.cs" Inherits="MangeInventory" %>

<script runat="server">

   
</script>


<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="Server">
    <link rel="Stylesheet" href="<%=Util.BASE_URL%>/static/StyleSheet/Form.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            SetGridPageStyle();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
    <div id="Breadcrumbs" style="position: absolute; top: 18%; left: 0%; text-align: left; padding: 10px; padding-left: 20px; margin-left: 7%;">
        <a href="Dashboard.aspx">Dashboard</a>&nbsp;>&nbsp;Manage Teachers
    </div>
    <div id="Form">
        <asp:UpdatePanel runat="server" ID="TecherPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="text-align: left; float: left; width: 100%; margin-bottom: 10px;">
                    <div class="HeaderText" style="text-align: left; font: bold;">
                        Manage Teachers 
                    </div>                                 
                </div>
                   
               <div id="Status" runat="server" class="Record" style="color: red;">
                </div>
                <div id="divresult" runat="server" style="color: Green; text-align: left; "
                        visible="false">                        
                    </div> 
                <div class="Record">
                    <div class="Column1">
                        Department Name
                    </div>
                    <div class="Column2">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                               
                 <div id="divDownResUser" runat="server"></div>
                <br /><br />
                <div class="Record">
                    <asp:GridView runat="server" ID="gvTeacherDetails" ShowFooter = "true" DataKeyNames="TeacherName" AutoGenerateColumns="False"
                        GridLines="None" Width="60%" EmptyDataText="Currently  don't have any Teachers for selected department"
                        OnRowCancelingEdit="gvTeacherDetails_RowCancelingEdit" OnRowDeleting="gvTeacherDetails_RowDeleting" OnRowEditing="gvTeacherDetails_RowEditing"
                        OnRowUpdating="gvTeacherDetails_RowUpdating">
                        
                        <RowStyle BackColor="#EFF3FB" CssClass="details" Width="100%" />
                        
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="headerStyle"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowEditButton="true" />  
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%#Eval("TeacherName") %>' OnClientClick = "return confirm('Do you want to delete?')" Text = "Delete" OnClick="lnkRemove_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teacher Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblName" runat="server" Text='<%#Eval("TeacherName") %>'>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditTeacherName" runat="server"  Text='<%# Eval("TeacherName")%>'></asp:TextBox>
                                </EditItemTemplate> 
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTeacherName" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblManufacturer" runat="server" Text='<%#Eval("DepartmentName") %>'>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="btnAdd" CssClass="SubmitButton" CommandName="Insert"  runat="server" OnClick="btnAdd_Click1" ValidationGroup="Teacherdetails">Add</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
               
            </ContentTemplate>
               
        </asp:UpdatePanel>
     
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder" runat="Server">
</asp:Content>
