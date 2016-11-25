<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.LabTestlist" Title="Untitled Page" Codebehind="frmAdmin_LabTestlist.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

    <div>
        <h1 id="H1" runat="server" class="margin" style="padding-left: 10px;">
            Laboratory Tests </h1>
        <div class="center">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
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
                                            <div class="mid" style="height: 300px; overflow: auto" >
                                                <div id="grd_custom" class="GridView whitebg" >
                                                    <asp:GridView ID="grdLab" runat="server" OnRowDataBound="grdLab_RowDataBound" AutoGenerateColumns="False"
                                                        Width="100%" AllowSorting="True" BorderWidth="0" GridLines="None" CssClass="datatable table-striped table-responsive" DataKeyNames="LabTestID"
                                                        CellPadding="0" CellSpacing="0" Height="99px" OnSelectedIndexChanging="grdLab_SelectedIndexChanging"
                                                        OnSorting="grdLab_Sorting" onrowcommand="grdLab_RowCommand">
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>   
</asp:Content>
