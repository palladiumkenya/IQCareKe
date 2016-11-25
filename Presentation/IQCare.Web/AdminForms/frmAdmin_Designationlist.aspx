<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.Designationlist" Title="Untitled Page" CodeBehind="frmAdmin_Designationlist.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />
<form id ="listofDesignation" method="post" runat="server"> --%>
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Administration->Designation Master</h1>
        <div class="center" style="padding: 5px;">
            <div class="formbg border center">
                <br />
                <h2 align="left" class="forms">
                    List of Designation
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
                                                        <asp:GridView ID="grdDesignation" CssClass="datatable" CellPadding="0" CellSpacing="0"
                                                            runat="server" OnRowDataBound="grdDesignation_RowDataBound" AutoGenerateColumns="False"
                                                            Width="100%" PageSize="1" AllowSorting="True" BorderWidth="0" GridLines="None"
                                                            Height="99px" OnSelectedIndexChanging="grdDesignation_SelectedIndexChanging"
                                                            OnSorting="grdDesignation_Sorting">
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
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>