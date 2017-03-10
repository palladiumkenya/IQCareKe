<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.TreeviewICDCode" Codebehind="frmClinicalTreeviewICDCode.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<?xml version="1.0" ?>
<html lang="en-US" xml:lang="en-US" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ICD Codes</title>
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
            overflow: scroll;
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
    <form id="ICDCode" method="post" runat="server">
    <script language="javascript" type="text/javascript">
        function PostPage() {
            var temp = new Array();
            var Val = '<%=TVICD10.SelectedValue%>';
            temp = Val.split('-');
            window.opener.document.getElementById('ctl00_IQCareContentPlaceHolder_' + document.getElementById('<%=Hpother.ClientID %>').value).value = temp[1];
            window.close();
        }
    </script>
    <div class="border formbg">
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="pad5 whitebg form" align="left" width="100%" nowrap="nowrap">
                        <div class="treeview">
                            <asp:TreeView ID="TVICD10" runat="server" PopulateNodesFromClient="true" EnableClientScript="true"
                                Font-Size="Smaller" Font-Bold="true" Font-Names="arial, helvetica, verdana, sans-serif"
                                OnSelectedNodeChanged="TVICD10_SelectedNodeChanged">
                            </asp:TreeView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="pad5 whitebg form" width="100%">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            OnClientClick="PostPage()" />
                        <asp:Button ID="btnExit" runat="server" Text="Close" OnClientClick="PostPage()" />
                        <asp:HiddenField ID="Hpother" runat="server" />
                        <asp:HiddenField ID="HpNodeId" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
