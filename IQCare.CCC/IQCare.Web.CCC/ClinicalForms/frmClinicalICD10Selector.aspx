<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.ICD10Selector" Codebehind="frmClinicalICD10Selector.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="lblHeader" runat="server">ICD10 Disease Selector</title>
    <link rel="Stylesheet" type="text/css" href="../Style/StyleSheetBrowser.css" />
    <style type="text/css">
        div.border
        {
            border-right: #666699 1px solid;
            border-top: #666699 1px solid;
            border-left: #666699 1px solid;
            border-bottom: #666699 1px solid;
            overflow: auto;
        }
        .formbg
        {
            background-color: #e1e1e1;
        }
        .pad5
        {
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 5px;
            padding-top: 5px;
        }
        .whitebg
        {
            background-color: #ffffff;
        }
        div.treeview
        {
            border-right: #666699 1px solid;
            border-top: #666699 1px solid;
            display: inline;
            margin: 15px 6px 25px 6px;
            overflow: auto;
            border-left: #666699 1px solid;
            width: 100%;
            border-bottom: #666699 1px solid;
            height: 300px;
            text-align: left;
        }
        td.form
        {
            border-right: #666699 1px solid;
            border-top: #666699 1px solid;
            border-left: #666699 1px solid;
            border-bottom: #666699 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 5px;
            padding-top: 5px;
            background-color: #ffffff;
        }
    </style>
</head>
<body>
    <form id="ICD10" method="post" runat="server">
    <script language="javascript" type="text/javascript">
        function PostPage() {
            window.close();
        }
        function closeMe() {
            var win = window.open("", "_self");
            win.close();
        }
        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
        var browserName = navigator.appName;
        if (browserName != "Microsoft Internet Explorer") {
            if (sPage == "frmConfig_Customfields.aspx") {
                document.write('<link rel="stylesheet" type="text/css" href=="../style/StyleSheetBrowser.css" />');
                document.write("<style>#container {z-index: 1; margin: 0px auto; width: 950px; height:900px ; position: relative; background-color: #ffffff; text-align: left</style>")
            }
            else {
                document.write('<link rel="stylesheet" type="text/css" href="../style/StyleSheetBrowser.css" />');
            }

        }
        else {
            document.write('<link rel="stylesheet" type="text/css" href="../style/styles.css" />');
        }
      
    </script>
    <div align="center">
        <table cellpadding="6" cellspacing="0" border="0">
            <tbody>
                <tr>
                    <td class="whitebg form" align="left" nowrap="nowrap">
                        <div style="overflow: scroll; overflow-x; width: 400px; height: 400px;">
                            <asp:TreeView ID="TVICD10" Font-Size="Small" Font-Bold="true" Font-Names="arial, helvetica, verdana, sans-serif"
                                runat="server" NodeWrap="true" PopulateNodesFromClient="true" EnableClientScript="true"
                                OnSelectedNodeChanged="TVICD10_SelectedNodeChanged">
                            </asp:TreeView>
                            <asp:TextBox ID="txtvalue" runat="server" Visible="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnvalue" runat="server" />
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Width="80px" Text="Add >>" OnClick="btnAdd_Click" />
                        <br />
                        <asp:Button ID="btnRemove" runat="server" Width="80px" Text="<< Remove" OnClick="btnRemove_Click" />
                    </td>
                    <td class="whitebg form" valign="top">
                        <div style="overflow: scroll; overflow-x: hidden; width: 400px; height: 400px;">
                            <asp:ListBox ID="lstSelectedICD10" runat="server" Width="400px" Height="400px"></asp:ListBox>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div align="left" style="display: none;">
            <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
            <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server"></asp:TextBox>
        </div>
    </div>
    <br />
    <div align="center" style="padding-left: 10px;">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClientClick="PostPage()" />
    </div>
    </form>
</body>
</html>
