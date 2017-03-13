<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.PatientHistory" Title="Untitled Page" CodeBehind="frmPatient_History.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div class="center" style="padding: 8px;">
        <asp:Label ID="lblDate" CssClass="smallerlabel" runat="server" Text=""></asp:Label>
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" align="center" colspan="2">
                            <div class="treeview">
                                <h1>
                                    <asp:TreeView ID="TreeViewExisForm" ForeColor="#000000" runat="server" Width="100%"
                                        OnSelectedNodeChanged="TreeViewExisForm_SelectedNodeChanged">
                                    </asp:TreeView>
                                </h1>
                            </div>
                            <asp:Button ID="btnExit" runat="server" Text="Back" OnClick="btnExit_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
