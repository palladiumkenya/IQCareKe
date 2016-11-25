<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.Occupationlist" Title="Untitled Page" Codebehind="frmAdmin_Occupationlist.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Administration->Occupation Master</h1>
        <div class="formbg border center">
            <br />
            <h2 align="left" class="forms">
                List of Occupations
            </h2>
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg">
                            <br />
                            <label>
                                Select all that apply:</label>
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
                                                    <asp:GridView ID="grdOccupation" runat="server" OnRowDataBound="grdOccupation_RowDataBound"
                                                        AutoGenerateColumns="False" Width="100%" PageSize="1" AllowSorting="True" CssClass="datatable table-striped table-responsive"
                                                        CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None" Height="99px"
                                                        OnPageIndexChanging="grdOccupation_PageIndexChanging" OnSelectedIndexChanging="grdOccupation_SelectedIndexChanging"
                                                        OnSorting="grdOccupation_Sorting">
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add " OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
