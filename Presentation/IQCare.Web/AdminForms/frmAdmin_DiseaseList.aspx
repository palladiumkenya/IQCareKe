<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.DiseaseList" Title="Untitled Page" Codebehind="frmAdmin_DiseaseList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />
<form id ="listDiseaseMaster" method="post" runat="server"> --%>
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Master Forms</h1>
        <div class="formbg border center">
            <br />
            <h2 align="left" class="forms">
                List of Diseases</h2>
            <table width="80%" border="0" cellpadding="0" cellspacing="6">
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
                                                    <asp:GridView ID="grdDisease" BorderWidth="0" GridLines="None" CssClass="datatable"
                                                        CellPadding="0" CellSpacing="0" runat="server" OnRowDataBound="grdDisease_RowDataBound"
                                                        AutoGenerateColumns="False" Width="100%" PageSize="1" AllowSorting="True" BorderColor="#666699"
                                                        Height="99px" OnSelectedIndexChanging="grdDisease_SelectedIndexChanging" OnSorting="grdDisease_Sorting">
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add Disease" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
