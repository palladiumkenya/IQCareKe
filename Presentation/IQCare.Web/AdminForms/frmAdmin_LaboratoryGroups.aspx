<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" CodeBehind="frmAdmin_LaboratoryGroups.aspx.cs" 
Inherits="IQCare.Web.Admin.LaboratoryGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

 <div>
        <h1 class="margin" style="padding-left: 10px;">
            Laboratory Test Groups</h1>
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
                                            <div class="mid">
                                                <div id="grd_custom" class="GridView whitebg">
                                                    <asp:GridView ID="grdLab" runat="server" OnRowDataBound="grdLab_RowDataBound" AutoGenerateColumns="False"
                                                        Width="100%" AllowSorting="True" BorderWidth="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                        CellPadding="0" CellSpacing="0" Height="99px" OnSelectedIndexChanging="grdLab_SelectedIndexChanging"
                                                        OnSorting="grdLab_Sorting">
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
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>   
</asp:Content>
