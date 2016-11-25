<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.DBBackupSetup" Codebehind="frmDBBackupSetup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" type="text/css" href="./style/styles.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server" style="height: 75px; width: 430px;">
    <h3 class="left">
        Backup/Restore Setup</h3>
    <div class="border formbg" style="height: 75px; width: 430px;">
        <table>
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <label class="right" for="lblBackupTime">
                            Auto-Backup Time:</label>
                        <asp:DropDownList ID="ddBackupTime" runat="server" Width="73px">
                        </asp:DropDownList>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <label class="right" for="lblBackupDrive">
                            Backup Drive:</label>
                        <asp:DropDownList ID="ddBackupDrive" runat="server" Width="73px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>C:</asp:ListItem>
                            <asp:ListItem>D:</asp:ListItem>
                            <asp:ListItem>E:</asp:ListItem>
                            <asp:ListItem>F:</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="whitebg">
                    <td colspan="2">
                        <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" />
                        <input type="button" onclick="window.close();" value="Close" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
