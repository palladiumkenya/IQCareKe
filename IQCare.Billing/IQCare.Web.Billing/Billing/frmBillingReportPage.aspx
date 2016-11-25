<%@ Page Title="Facility Billing Reports" Language="C#" MasterPageFile="~/MasterPage/Module.master"
    AutoEventWireup="true" CodeBehind="frmBillingReportPage.aspx.cs" Inherits="IQCare.Web.Billing.ReportPage" %>
    <%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="ctReportPage" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
   
    <script type="text/javascript">
        function printPage() {
            window.open("./frmCustomReportPrint.aspx");
            // div.innerHTML = '<iframe src="./frmCustomReportPrint.aspx" onload="this.contentWindow.print();"></iframe>';
        }
        function openReportPage(path) {
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=yes,width=950,height=650,scrollbars=yes');
        }
    </script>
    <div>
        <h2 class="forms" align="left">
            Billing Reports
            <br />
        </h2>
        <div id="div-BillingReports" class="GridView whitebg" style="width: 950">
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
                <ajaxToolkit:TabPanel ID="tbpnlStaticreports" runat="server">
                    <HeaderTemplate>
                        Standard Reports
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="grid">
                            <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                HorizontalAlign="Left" Visible="False">
                                <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"></asp:Label>
                            </asp:Panel>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="cursor: pointer; height: 280px; overflow: auto; border: 1px solid #666699;">
                                        <div id="div-gridview" class="whitebg" style="width: 950; overflow: auto; margin-top: 5px;
                                            margin-bottom: 5px; max-height: 480px; height: 280px">
                                            <asp:GridView ID="grdBillilingReports" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive" GridLines="None"
                                                Width="100%" DataKeyNames="Code,fileName,procedureName,tableNames,hasPatientData" OnRowCommand="grdBillilingReports_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="Name">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayItemDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:ButtonField CommandName="Generate" Text="Generate" />
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <RowStyle CssClass="gridrow" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnRaisePopup" runat="server" Text="Save" Width="60px" Style="display: none" />
                        <asp:HiddenField ID="HReport_ID" runat="server" />
                        <asp:HiddenField ID="HFileName" runat="server" />
                        <asp:HiddenField ID="HQuery" runat="server" />
                        <asp:HiddenField ID="HTableNames" runat="server" />
                         <asp:HiddenField ID="HPatientData" runat="server" />
                        <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 320px;
                            border: solid 1px #808080; background-color: #F0F0F0">
                            <asp:Panel ID="divTitleCustomReport" runat="server" Style="border: solid 1px #808080;
                                margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="width: 100%;">
                                            Specify the following values for filtering
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <br />
                            <table width="100%" border="0" style="margin: 20px">
                                <tbody>
                                    <tr>
                                        <td style="width: 10%; white-space: nowrap" align="right">
                                            Start Date
                                        </td>
                                        <td style="width: 30%" align="left">
                                            <asp:TextBox runat="server" ID="textDateFrom" Width="80px" />
                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="textDateFrom" Format="dd-MMM-yyyy"
                                                ID="ceStartDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; white-space: nowrap" align="right">
                                            End Date:
                                        </td>
                                        <td style="width: 60%" align="left">
                                            <asp:TextBox runat="server" ID="textDateTo" Width="80px" />
                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="textDateTo" Format="dd-MMM-yyyy"
                                                ID="ceEndDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <br />
                            <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                                text-align: center; padding-top: 5px; padding-bottom: 5px">
                                <asp:Button ID="btnActionOK" runat="server" Text="Continue" Width="80px" />
                                <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" Width="80px" />
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="programmaticModalPopupBehavior"
                            TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
                            CancelControlID="btnActionCancel" DropShadow="True" PopupDragHandleControlID="divTitleCustomReport"
                            DynamicServicePath="" Enabled="True">
                        </ajaxToolkit:ModalPopupExtender>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tbpnlCustomreports" runat="server">
                    <HeaderTemplate>
                        Custom reports
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td style="width: 10%; white-space: nowrap" align="center">
                                        Select Report:
                                    </td>
                                    <td style="width: 60%" align="center">
                                        <asp:DropDownList ID="ddlReport" Width="100%" runat="server" Height="30px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:Panel ID="divErrorCustomReport" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="False">
                            <asp:Label ID="lblErrorCustomReport" runat="server" Style="font-weight: bold; color: #800000"></asp:Label>
                            <asp:HiddenField ID="queryString" runat="server" />
                            <asp:HiddenField ID="thetableName" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlNoData" runat="server" Visible="False">
                            <span style="color: #600000; font-weight: bold;">There is no data at the moment for
                                the selected report </span>
                        </asp:Panel>
                        <asp:Panel ID="panelResult" runat="server" UpdateMode="Conditional">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="border: 1px solid #666699;">
                                        <div class="whitebg" id="div-gridview" style="width: 950; overflow: auto; margin-top: 5px;
                                            margin-bottom: 5px; max-height: 480px; height: 280px">
                                            <asp:GridView ID="gridResult" runat="server" CellPadding="0" BorderStyle="Solid"
                                                BorderWidth="1px" HorizontalAlign="Left" Width="100%" CssClass="datatable table-striped table-responsive" BorderColor="#99CCFF"
                                                EmptyDataText="The report returned no data">
                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                                    Wrap="False" />
                                                <RowStyle CssClass="gridrow" HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click"
                                            Visible="False" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="False" OnClientClick="printPage()" />
                                        <asp:Button ID="btn_close" runat="server" OnClick="btn_close_Click" Text="Close" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div id="printerDiv" style="display: none">
                        </div>
                        <asp:Button ID="btnRaisePopupCustomReport" runat="server" Text="Save" Width="60px"
                            Style="display: none" />
                        <asp:Panel ID="divCustomReportParameters" runat="server" Style="display: none; width: 420px;
                            border: solid 1px #808080; background-color: #F0F0F0">
                            <asp:Panel ID="divCustomReportTitle" runat="server" Style="border: solid 1px #808080;
                                margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="width: 100%;">
                                            Specify the following values for filtering
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <br />
                            <asp:GridView ID="gridParameter" runat="server" CssClass="datatable table-striped table-responsive" AutoGenerateColumns="False"
                                DataKeyNames="ParameterName" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ParameterName" HeaderText="Name" ReadOnly="True" />
                                    <asp:BoundField DataField="DataType" HeaderText="Type" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="paramValue"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                            <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                                text-align: center; padding-top: 5px; padding-bottom: 5px">
                                <asp:Button ID="btnActionOKCustomReport" runat="server" Text="Continue" Width="80px"
                                    OnClick="btnActionOKCustomReport_Click" />
                                <asp:Button ID="btnActionCancelCustomReport" runat="server" Text="Cancel" Width="80px" />
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="parameterPopupCustomR" runat="server" TargetControlID="btnRaisePopupCustomReport"
                            PopupControlID="divCustomReportParameters" BackgroundCssClass="modalBackground"
                            CancelControlID="btnActionCancelCustomReport" DropShadow="True" PopupDragHandleControlID="divCustomReportTitle"
                            DynamicServicePath="" Enabled="True">
                        </ajaxToolkit:ModalPopupExtender>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                </triggers>
            </ajaxToolkit:TabContainer>
           <%-- <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
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
            </asp:UpdateProgress>--%>
           
        </div>
    </div>
</asp:Content>
