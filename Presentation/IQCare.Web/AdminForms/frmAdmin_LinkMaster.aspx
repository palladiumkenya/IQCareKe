<%@ Page Language="C#" Buffer="true" AutoEventWireup="True" MasterPageFile="~/MasterPage/IQCare.master"
    Inherits="IQCare.Web.Admin.LinkMaster" CodeBehind="frmAdmin_LinkMaster.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <script language="javascript" type="text/javascript">
        </script>
        <h1 class="margin">
            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h1>
        <div class="center" style="padding: 5px;">
            <div style="padding-bottom: 1%" class="border center formbg">
                <table class="center" cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%" class="form" valign="middle">
                                <table width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblfrom" runat="server" CssClass="required"></asp:Label>
                                            </td>
                                            <td style="width: 60%">
                                                <asp:DropDownList ID="ddlRegion" runat="Server" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="width: 50%" class="form">
                                <table width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblto" runat="server" CssClass="required"></asp:Label>
                                            </td>
                                            <td style="width: 80%">
                                                <div>
                                                    <asp:DropDownList ID="ddDistrict" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="center" colspan="2">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" Width="45px" OnClick="btnAdd_Click">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" colspan="2">
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
                                                    <div id="grd_custom" class="GridView whitebg" style="cursor: pointer; height: 280px;
                                                        overflow: auto">
                                                        <asp:GridView ID="grdLink" runat="server" PageSize="1" AutoGenerateColumns="False"
                                                            CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None"
                                                            AllowSorting="True" OnRowDataBound="grdLink_RowDataBound" OnRowDeleting="grdLink_RowDeleting"
                                                            OnSorting="grdLink_Sorting">
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
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px" class="form" align="center" colspan="2">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" Width="45px">
                                </asp:Button>
                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel">
                                </asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
