<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" CodeBehind="frmLinearReport.aspx.cs" Inherits="IQCare.Web.IQTools.frmLinearReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div style="padding-left: 5px; padding-right: 5px; padding-top: 0px; width: 950">
        <div class="nomargin">
            <h2 class="nomargin">
                Clincal Indicators: &nbsp;&nbsp;&nbsp;&nbsp; <span>
                    <asp:Button ID="btrRefresh" runat="server" Text="Refresh Indicators" OnClick="btrRefresh_Click" /></span>
            </h2>
        </div>
        <div class="border center formbg" style="width: 950">
            <asp:UpdatePanel ID="panelFilter" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="width: 10%; white-space: nowrap" align="right">
                                    Select Category:
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:DropDownList ID="ddlCategory" Width="180px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%; white-space: nowrap" align="right">
                                    Select Query:
                                </td>
                                <td style="width: 60%" align="left">
                                    <asp:DropDownList ID="ddlReport" Width="100%" runat="server" Height="30px">
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="2" valign="middle">
                                    <asp:Button ID="btnGenerate" runat="server" Text="View" OnClick="btnGenerate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; white-space: nowrap" align="right">
                                    Start Date
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:TextBox runat="server" ID="textDateFrom" Width="80px" />
                                    <ajaxToolkit:CalendarExtender runat="server" TargetControlID="textDateFrom" Format="dd-MMM-yyyy"
                                        ID="ceStartDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 10%; white-space: nowrap" align="right">
                                    End Date:
                                </td>
                                <td style="width: 60%" align="left">
                                    <asp:TextBox runat="server" ID="textDateTo" Width="80px" />
                                    <ajaxToolkit:CalendarExtender runat="server" TargetControlID="textDateTo" Format="dd-MMM-yyyy"
                                        ID="ceEndDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 10%; white-space: nowrap">
                                    CD4 Cutoff For ART:
                                </td>
                                <td align="left" style="width: 30%">
                                    <asp:TextBox ID="textCD4" runat="server" Text="350" Width="80px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteCD4" runat="server" FilterType="Numbers"
                                        TargetControlID="textCD4" />
                                    <asp:HiddenField ID="hCutOffCD4" runat="server" Value="350" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="height: 5px;">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                        <asp:HiddenField ID="queryString" runat="server" />
                        <asp:HiddenField ID="thetableName" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlNoData" runat="server" Visible="false">
                        <span style="color: #600000; font-weight: bold;">There is no data at the moment for
                            the selected report </span>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="panelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="grid">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="max-height: 480px; height: 280px; overflow: auto">
                                        <div class="whitebg" id="div-gridview">
                                            <asp:GridView ID="gridResult" runat="server" CellPadding="0" CellSpacing="0" EnableModelValidation="True"
                                                BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="100%" UseAccessibleHeader="true"
                                                AllowSorting="false" GridLines="Both" CssClass="datatable" BorderColor="#99CCFF"
                                                EmptyDataText="The report returned no data">
                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                                    Wrap="False" />
                                                <RowStyle CssClass="gridrow" HorizontalAlign="Left" />
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
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
                <ProgressTemplate>
                    <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                        <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                            background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                    <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                                </td>
                                <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px;
                                    height: 22px; padding-left: 5px">
                                    Processing....
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>
