<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCertificate.aspx.cs"
    Inherits="PrintCertificate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Certificate</title>
    <link href="static/bootstrap.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <div id="controlbuttons" style="clear: both; text-align: left; margin-left: 2%; margin: 10px;
                width: 80%; position: static;">
                <asp:LinkButton runat="server" ID="lnkBack" Text="Back" CssClass="btn convert"></asp:LinkButton>
            </div>
            <div runat="server" id="Container" visible="false">
            </div>
            <iframe runat="server" id="PrintMarksheet" width="100%" style="border: none; height: 820px">
            </iframe>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
