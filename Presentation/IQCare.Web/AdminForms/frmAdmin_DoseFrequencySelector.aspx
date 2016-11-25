<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.DoseFrequencySelector" Codebehind="frmAdmin_DoseFrequencySelector.aspx.cs" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head id="Head1" runat="server">
    <title id="lblHeader" runat="server">Untitled Page</title>
</head>
<body>
    <script language="javascript" type="text/javascript">
        function closeMe() {
            var win = window.open("", "_self");
            win.close();
        }
    </script>
    <form id="DrugSelection" class="border" runat="server" style="height: 300px; width: 600px;">
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
        <div id="showAbbv" runat="server" class="border" align="left" style="width: 600px">
            <asp:Label ID="lblAdd" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="txtAdd" MaxLength="200" runat="server"></asp:TextBox>
            <asp:Label ID="lblAbbv" runat="server" Text="Add Abbrevation" Visible="false"></asp:Label>
            <asp:TextBox ID="txtAbbv" MaxLength="20" runat="server" Visible="false"></asp:TextBox>
            <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" OnClick="btnSave_Click" /></div>
        <br />
        <div class="border center" align="Center" style="width: 600px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    </form>
</body>
