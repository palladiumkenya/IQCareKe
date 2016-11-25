<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.Reason" Title="Untitled Page" Codebehind="frmAdmin_Reason.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="adduser" method="post" runat="server">--%>
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Reason Master</h1>
        <div class="border center formbg">
            <br>
            <h2 class="forms" align="left">
                <asp:Label ID="lblH2" runat="server" Text="Add/Edit Reason"></asp:Label></h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" width="26%">
                            <label class="left" for="LastName">
                                Reason:</label>
                            <asp:TextBox ID="txtReasonName" runat="server"></asp:TextBox>
                        </td>
                        <td class="border pad5 whitebg" width="37%">
                            <label class="right" for="SecondName">
                                Category:</label>
                            <asp:DropDownList ID="ddCategory" CssClass="font" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="border pad5 whitebg" width="15%">
                            <label class="right" for="ThirdName">
                                SRNo:</label>
                            <asp:TextBox ID="txtSRNo" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="border center pad5 whitebg" width="22%">
                            <asp:Label class="right30" runat="server" ID="lblStatus">Status:</asp:Label>
                            <asp:DropDownList ID="ddStatus" runat="server">
                                <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="4">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
