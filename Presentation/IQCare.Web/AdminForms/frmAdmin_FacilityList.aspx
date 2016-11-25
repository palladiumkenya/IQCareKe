<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.FacilityList" Title="Untitled Page" Codebehind="frmAdmin_FacilityList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Facility/Satellite</h1>
        <div class="center">
            <table id="tblSystem" runat="server" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="formbg center">
                            <div class="whitebg">
                                <label>
                                    System Type:</label>
                                <asp:DropDownList ID="cmbSystem" runat="server" Width="20%">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr class="formbg center">
                        <td>
                            <div class="whitebg pad20">
                                <asp:Button ID="btnSystemSave" runat="server" Text="Set System" OnClick="btnSystemSave_Click" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="GridView whitebg" style="cursor: pointer;">
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        Facility List</h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 300px; overflow: auto">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView ID="grdMasterFacility" runat="server" OnRowDataBound="grdMasterFacility_RowDataBound"
                                                            AutoGenerateColumns="False" Width="100%" PageSize="1" AllowSorting="True" CssClass="datatable table-striped table-responsive"
                                                            CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None" OnSelectedIndexChanging="grdMasterFacility_SelectedIndexChanging"
                                                            OnSorting="grdMasterFacility_Sorting">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <RowStyle CssClass="gridrow" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
