<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.IQCStageCustomPage"
    Title="Untitled Page" Codebehind="frmAdmin_IQCStageCustomPage.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--  <form id="frmstagecstmpage" runat="server">--%>
    <div>
        <h3 class="margin" style="padding-left: 10px;">
            <asp:Label ID="lblHeader" runat="server" Text="AIDS Defining Events"></asp:Label></h3>
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 center whitebg" width="50%" align="right">
                            <label class="margin11">
                                <b>Code:</b>
                                <asp:TextBox ID="txtcode" runat="server"></asp:TextBox></label>
                        </td>
                        <td class="border pad5 center whitebg" width="50%" align="right">
                            <asp:Label ID="lblName" runat="server" Text="Name:" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdstage" class="border pad5 center whitebg" width="50%" align="right" runat="server">
                            <label class="margin11">
                                <b>Stage:</b>
                                <asp:TextBox ID="txtStage" runat="server"></asp:TextBox></label>
                        </td>
                        <td id="tdpriority" class="border pad5 center whitebg" width="50%" align="right"
                            runat="server">
                            <asp:Label ID="lblPriority" runat="server" Text="Priority:" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtSeqNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdStatus" runat="server" class="border center pad5 whitebg" align="center"
                            colspan="2">
                            <asp:Label ID="lblStatus" runat="server" Text="Status:" Font-Bold="true"></asp:Label>
                            <asp:DropDownList ID="ddStatus" runat="server" Width="155px">
                                <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">InActive</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="2">
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
    >
</asp:Content>
