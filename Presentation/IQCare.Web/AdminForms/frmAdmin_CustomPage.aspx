<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.CustomPage" Title="Untitled Page" CodeBehind="frmAdmin_CustomPage.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="addeditDisease" method="post" runat="server">--%>
    <div>
        <h3 class="margin" align="left" style="padding-left: 10px;">
            <asp:Label ID="lblHeader" runat="server" Text="OI or AIDS Defining Illness"></asp:Label></h3>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br />
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" width="40%" nowrap="nowrap">
                                <asp:Label ID="lblName" runat="server" Text="OI or AIDS Defining Illness" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                <div id="divmultiplier" runat="server">
                                    <br />
                                    <asp:Label ID="lblMultiplier" runat="server" Text="Multiplier:" Font-Bold="true"></asp:Label>
                                    <asp:RadioButton ID="rdnomultiplier" GroupName="mul" Text="No Multiplier" runat="server" />
                                    <asp:RadioButton ID="rdinteger" GroupName="mul" Text="Integer" runat="server" />
                                    <asp:TextBox ID="txtmultiplier" MaxLength="2" runat="server" Width="20px"></asp:TextBox>
                                </div>
                            </td>
                            <td id="tdStatus" runat="server" class="border center pad5 whitebg" width="30%">
                                <asp:Label ID="lblStatus" runat="server" Text="Status :" Font-Bold="true"></asp:Label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="border center pad1 whitebg" width="30%">
                                <asp:Label ID="lblPriority" runat="server" Text="Priority :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtSeqNo" Width="20px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click" />
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