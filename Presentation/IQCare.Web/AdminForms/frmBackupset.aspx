<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.Admin.Backupset" CodeBehind="frmBackupset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="./style/styles.css" />
    <title>Untitled Page</title>
</head>
<body>
    <form id="BackUpSet" runat="server" class="border" style="width: 600px; height: 398px;
    padding-left: 10px">
    <div style="width: 588px; height: 300px;">
        <table class="center formbg" cellpadding="18" width="100%" height="50%" border="0">
            <tbody>
                <tr>
                    <td class="border center formbg" style="height: 160px">
                        <asp:ListBox ID="lstBackupFile" runat="server" Height="150px" Width="538px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:Button ID="btnLoadBkp" runat="server" Text="LoadBackupSet" OnClick="btnLoadBkp_Click" />
        <div class="center">
            <table cellpadding="18" width="100%" height="50%" border="0">
                <tbody>
                    <tr>
                        <td class="border center formbg" style="height: 180px; width: 560px">
                            <div class="gridviewbackup whitebg">
                                <asp:GridView ID="grdBackupset" runat="server" AllowSorting="false" AutoGenerateColumns="false"
                                    Width="100%" BackColor="white" CellSpacing="1" OnRowDataBound="grdBackupsetDataBound"
                                    OnSelectedIndexChanging="grdBackupset_SelectedIndexChanging">
                                    <HeaderStyle CssClass="tableheaderstyleunsort" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
    </div>
    </form>
</body>
</html>
