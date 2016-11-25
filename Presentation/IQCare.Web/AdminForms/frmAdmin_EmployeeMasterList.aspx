<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.EmployeeMasterList"
    Title="Untitled Page" Codebehind="frmAdmin_EmployeeMasterList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
  
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            <asp:Label ID="lblHeader" runat="server"></asp:Label></h1>
        <div class="center">
            <table width="100%" cellpadding="0" cellspacing="5">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid">
                                                <div id="grd_custom" class="GridView whitebg" style="cursor: pointer; height: 280px; overflow: auto"> 
                                                    <asp:GridView ID="grdMasterEmployee" runat="server" AllowSorting="True" OnRowDataBound="grdMasterEmployee_RowDataBound"
                                                        AutoGenerateColumns="False" Width="100%" BorderWidth="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                        CellPadding="0" CellSpacing="0" PageIndex="1" OnSelectedIndexChanging="grdMasterEmployee_SelectedIndexChanging"
                                                        OnSorting="grdMasterEmployee_Sorting">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
