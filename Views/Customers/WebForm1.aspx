<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="Multilinks.WebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        #Submit1 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:Panel ID="pnlProfile" runat="server" Visible="true">
            <hr />
            <table>
                <tr>
                    <td rowspan="6" valign="top">
                        <asp:Image ID="ProfileImage" runat="server" Width="50" Height="50" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ID:
                        <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name:
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Verified Email:
                        <asp:Label ID="lblVerified" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Text="Clear" runat="server" OnClick="Clear" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
