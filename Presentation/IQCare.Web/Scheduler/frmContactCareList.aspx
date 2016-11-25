<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Scheduler.ContactCareList"
    Title="Untitled Page" Codebehind="frmContactCareList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="ContactCareList" runat="server" method="post">--%>
    <div style="padding-top: 20px;">
        <h1 class="margin" style="padding-left: 10px;">
            ContactCare</h1>
        <div class="center">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="GridView whitebg">
                                <asp:GridView ID="grdContactCareList" AutoGenerateColumns="false" PageIndex="1" CellPadding="1"
                                    CellSpacing="1" AllowSorting="true" runat="server" Width="100%" OnRowDataBound="grdContactCareList_RowDataBound"
                                    OnSelectedIndexChanging="grdContactCareList_SelectedIndexChanging" BackColor="white"
                                    OnSorting="grdContactCareList_Sorting">
                                    <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="left" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" OnClick="btnCancel_Click" />
                            <asp:Label ID="l" runat="server" Visible="false" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
