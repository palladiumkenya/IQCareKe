<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.Laboratory.LabSelector"
    CodeBehind="frmLabSelector.aspx.cs" %>

<html>
<head runat="server">
    <title id="lblHeader" runat="server">Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../style/styles.css" />
</head>
<body>
    <form id="LabSelection" class="border" runat="server" style="height: 300px; width: 640px;">
    <div align="Center" style="width: 640px; height: 300px;">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg">
                        <asp:ListBox ID="lstLabList" runat="server" Height="180px" Width="210px"></asp:ListBox>
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
                        <asp:ListBox ID="lstSelectedLab" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div align="left" style="width: 640px">
            <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
            <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
        </div>
        <br />
        <div class="border" align="Center" style="width: 635px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        /*
        Author : Amitava Sinha
        Creation Date : 03-april-2007
        Purpose:atleast one item will selected 
        */

        function listBox_selected(sel) {
            var listBox = document.getElementById(sel);
            var intCount = listBox.options.length;

            for (i = 0; i < intCount; i++) {
                if (listBox.options(i).selected) {
                    return true;
                }

            }
            alert("Select atleast one item !");
            return false;

        }    
    </script>
    </form>
</body>
</html>
