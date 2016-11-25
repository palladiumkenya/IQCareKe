<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.RearrangeList" Codebehind="frmAdmin_RearrangeList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HeadRearrangeList" runat="server">
    <title id="lblHeader" runat="server">Rearrange Custom Fields</title>
    <link rel="stylesheet" type="text/css" href="../style/styles.css" />
    <style type="text/css">
        .style1
        {
            width: 43%;
        }
    </style>
</head>
<script language="javascript" type="text/javascript">

</script>
<body>
    <form id="RearrangeListItems" class="border" runat="server" style="height: 300px;
    width: 600px;">
    <div align="left" style="width: 600px">
        <asp:Label ID="lblFormName" runat="server" Text="Form Name" Width="84px"></asp:Label>
        <asp:DropDownList ID="ddlFormName" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlFormName_SelectedIndexChanged"
            Height="24px" Width="134px">
        </asp:DropDownList>
    </div>
    <br />
    <table width="100%" border="2">
        <tr>
            <td width="60%">
                <asp:ListBox ID="lstRearrangeListItems" runat="server" Height="180px" Width="344px">
                    <asp:ListItem></asp:ListItem>
                </asp:ListBox>
            </td>
            <td width="10%">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="btnUp" runat="server" Text="Up" Width="15px" OnClick="btnUp_Click"
                                ImageUrl="~/Images/btnUp.Image.gif" />
                        </td>
                    </tr>
                    <tr style="height: 50%">
                        <td align="center">
                            <asp:Label ID="lblMove" runat="server" Text="Move"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="btnDown" runat="server" Text="Down" Width="15px" OnClick="btnDown_Click"
                                ImageUrl="~/Images/btnDown.Image.gif" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="30%" style="border-style: none">
            </td>
        </tr>
    </table>
    <br />
    <div class="border" align="center" style="width: 600px">
        <asp:Button ID="btnOk" runat="server" Width="80px" Text="OK" OnClick="btnOk_Click" />
        <asp:Button ID="btnCancel" runat="server" Width="80px" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
    </form>
</body>
</html>
