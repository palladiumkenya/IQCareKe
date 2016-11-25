<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.OIDrugSelector" Title="Untitled Page" Codebehind="frmClinical_OIDrugSelector.aspx.cs" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head id="Head1" runat="server">
    <title id="lblHeader" runat="server">Untitled Page</title>
</head>
<body>
    <form id="DrugSelection" class="border" runat="server" style="height: 300px; width: 600px;">
    <div style="width: 600px; height: 300px">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg">
                        <asp:ListBox ID="lstDrugList" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                    <td>
                        <div>
                            <asp:Button ID="btnAdd" runat="server" Width="80px" Text="ADD >>" OnClick="btnAdd_Click" />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnRemove" runat="server" Width="80px" Text="<< Remove" OnClick="btnRemove_Click" />
                        </div>
                    </td>
                    <td class="border formbg">
                        <asp:ListBox ID="lstSelectedDrug" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div align="left" style="width: 600px">
            <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
            <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
        </div>
        <br />
        <div class="border" align="Center" style="width: 600px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
    
    </script>
    </form>
</body>
