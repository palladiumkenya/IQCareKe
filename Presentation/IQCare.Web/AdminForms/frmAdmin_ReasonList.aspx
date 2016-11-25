<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.ReasonList" Title="Untitled Page" Codebehind="frmAdmin_ReasonList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
   
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Reason Administration</h1>
        <div class="formbg border center">
            <br />
            <h2 align="left" class="forms">
                List of Reason
            </h2>
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg" style="width: 904px">
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
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="grdReason" BorderWidth="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                        CellPadding="0" CellSpacing="0" runat="server" OnRowDataBound="grdReason_RowDataBound"
                                                        AutoGenerateColumns="False" Width="100%" PageSize="1" AllowSorting="True" Height="99px"
                                                        OnSelectedIndexChanging="grdReason_SelectedIndexChanging" OnSorting="grdReason_Sorting">
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
                        <td class="pad5 center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add Reason" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
