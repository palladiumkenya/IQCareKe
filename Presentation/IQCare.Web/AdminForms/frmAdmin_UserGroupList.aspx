<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.UserGroupList" Title="Untitled Page" Codebehind="frmAdmin_UserGroupList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            List of User Groups</h1>
        <div class="center">
            <table cellspacing="6" cellpadding="0" border="0" width="100%">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    User Group List</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 300px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="grdUsergroup" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        AllowSorting="true" PageIndex="1" CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0"
                                                        BorderWidth="0" GridLines="None" OnRowDataBound="grdUsergroup_RowDataBound" OnSelectedIndexChanging="grdUsergroup_SelectedIndexChanging"
                                                        BackColor="White" OnSorting="grdUsergroup_Sorting">
                                                        <HeaderStyle HorizontalAlign="Left" />
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
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnAddgroup" Text="Add" runat="server" OnClick="btnAddgroup_Click" />
                            <asp:Button ID="btnCancelgroup" Text="Close" runat="server" OnClick="btnCancelgroup_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
