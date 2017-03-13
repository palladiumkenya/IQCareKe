<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Laboratory.LabOrderList" Title="Untitled Page" Codebehind="frmLabOrderList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id ="frmLabOrderList" method="post" runat="server" title="Lab Order List"> --%>
    <div style="padding-top: 1px;">
        <h1 class="margin">
            Lab Order List</h1>
        <div class="center">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="GridView  whitebg">
                                <asp:GridView ID="grdLabOrderList" runat="server" AutoGenerateColumns="False" Width="100%"
                                    PageSize="1" AllowSorting="true" HorizontalAlign="Left" CellSpacing="1" BackColor="White"
                                    OnSorting="grdLabOrderList_Sorting" OnRowDataBound="grdLabOrderList_RowDataBound"
                                    OnSelectedIndexChanging="grdLabOrderList_SelectedIndexChanging">
                                    <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                        Wrap="false" />
                                    <AlternatingRowStyle BackColor="White" BorderColor="White" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
