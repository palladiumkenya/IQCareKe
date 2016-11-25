<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.Admin.ControlListSelector"
    CodeBehind="frmAdmin_ControlListSelector.aspx.cs" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head id="Head1" runat="server">
    <title id="lblHeader" runat="server">Custom List</title>
</head>
<script language="javascript" type="text/javascript">
    /*
    Author : Amitava Sinha
    Creation Date :30-Jan-2007
    Purpose: Validate List Box
    */
    function Validate(objListBox) {
        var i = objListBox.length;
        if (i == 0) {
            alert("Please enter atleast one Item !")
            return;
        }
    }
    function CheckDuplicate(sel) {
        var selobj = document.getElementById(sel);
        var fld = document.getElementById('<%=txtList.ClientID%>');
        var len = selobj.length;
        for (var i = 0; i < len; i++) {
            if (selobj.options[i].text == fld.value) {
                alert("Duplicate Items !")
            }
        }

    }
</script>
<body>
    <form id="ListSelection" class="border" runat="server" style="height: 300px; width: 600px;">
    <div align="left" style="width: 600px">
        <asp:Label ID="Label1" runat="server" Text="Field Label" Width="84px"></asp:Label>
        <asp:Label ID="lblField" runat="server" Text="" Width="50%"></asp:Label>
    </div>
    <div align="left" style="width: 60%">
        <asp:Label ID="Label2" runat="server" Text="Enter Value" Width="77px"></asp:Label>
        <asp:TextBox ID="txtList" runat="server" Width="50%" AutoPostBack="false"></asp:TextBox>&nbsp;
        <asp:Button ID="btnAdd" runat="server" Width="80px" Text="Add" OnClick="btnAdd_Click" /></div>
    <div style="width: 600px; height: 300px">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg" style="width: 508px; height: 218px">
                        <asp:ListBox ID="lstControlList" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div class="border" align="Center" style="width: 600px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Close" OnClick="btnBack_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
    </script>
    </form>
</body>