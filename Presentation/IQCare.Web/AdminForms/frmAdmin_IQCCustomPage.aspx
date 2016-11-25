<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.IQCCustomPage" Codebehind="frmAdmin_IQCCustomPage.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="form1" runat="server">--%>
    <div>
        <h3 class="margin" align="left" style="padding-left: 10px;">
            <asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br />
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" width="50%" nowrap="nowrap">
                                <asp:Label ID="lblcode" runat="server" Text="Code:" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
                            </td>
                            <td class="border center pad5 whitebg" width="50%" nowrap="nowrap">
                                <asp:Label ID="lblName" runat="server" Text="Name :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdStatus" runat="server" class="border center pad5 whitebg" width="50%">
                                <asp:Label ID="lblStatus" runat="server" Text="Status :" Font-Bold="true"></asp:Label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td id="tdPriority" runat="server" class="border center pad5 whitebg" width="50%">
                                <asp:Label ID="lblPriority" runat="server" Text="Priority :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtSeqNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td class="pad5 center" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
