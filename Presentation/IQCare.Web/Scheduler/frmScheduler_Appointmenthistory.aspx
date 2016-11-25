<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Scheduler.Appointmenthistory"
    Title="Untitled Page" Codebehind="frmScheduler_Appointmenthistory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div style="padding-top: 20px;">
        <div class="center" style="padding-left: 1px;">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border pad5 whitebg" align="right">
                        <asp:Button ID="btnNewAppointment" runat="server" Text="New Appointment" Font-Bold="true"
                            Enabled="True" OnClick="btnNewAppointment_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top">
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <center>
                                                <h2>
                                                    Appointments</h2>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="cursor: pointer; height: 380px; overflow: auto;border: 1px solid #666699;">
                                            <div id="div-gridview" class="GridView whitebg" style="height: 300px">
                                                <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" BorderWidth="0"
                                                    GridLines="None" CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0" AutoGenerateColumns="False"
                                                    Width="100%" PageSize="1" OnSorting="grdSearchResult_Sorting" OnRowDataBound="grdSearchResult_RowDataBound"
                                                    OnSelectedIndexChanging="grdSearchResult_SelectedIndexChanging">
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
                    <td class="pad5 center" align="center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>
