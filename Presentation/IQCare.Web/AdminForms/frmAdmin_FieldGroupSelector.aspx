<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.FieldGroupSelector" Codebehind="frmAdmin_FieldGroupSelector.aspx.cs" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head runat="server">
    <title id="lblHeader" runat="server">Untitled Page</title>
</head>
<body style="padding-left:10px;">
    <form id="FieldGroupSelection" class="border" runat="server" style="height: 308px;
    width: 622px;">
    <div style="width: 600px; height: 300px">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg">
                        <asp:ListBox ID="lstAvailable" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                    <td>
                        <div>
                            <asp:Button ID="btnAdd" runat="server" Width="80px" Text="Add >>" OnClick="btnAdd_Click" />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnRemove" runat="server" Width="80px" Text="<< Remove" OnClick="btnRemove_Click" />
                        </div>
                    </td>
                    <td class="border formbg">
                        <asp:ListBox ID="lstSelected" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <br />
        <br />
        <br />
        <div class="border" align="Center" style="width: 620px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
      
    </script>
    </form>
</body>
