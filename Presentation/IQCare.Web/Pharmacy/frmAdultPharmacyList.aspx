<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Pharmacy.AdultPharmacyList"
    Title="Untitled Page" Codebehind="frmAdultPharmacyList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id ="frmAdultPharmacyList" method="post" runat="server" title="Adult Pharmacy List"> --%>
    <div style="padding-top: 1px;">
        <h1 class="margin">
            Adult Pharmacy List</h1>
        <div class="center">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="GridView  whitebg">
                                <asp:GridView ID="grdAdultPharmacyList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" PageSize="1" AllowSorting="true" HorizontalAlign="Left" CellSpacing="1"
                                    BackColor="White" OnSorting="grdAdultPharmacyList_Sorting" OnRowDataBound="grdAdultPharmacyList_RowDataBound"
                                    OnSelectedIndexChanging="grdAdultPharmacyList_SelectedIndexChanging">
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
