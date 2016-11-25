<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="frmTemplateReport.aspx.cs" Inherits="IQCare.Web.IQTools.frmTemplateReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script language="javascript" type="text/javascript">
    //**********************************************
    function PrintReport() {
        var url = "PrintReport.aspx";
        //frmIQToolsQuery.RunReport();
        OpenNewWindow(url, "PrintReport");
    }
    //**********************************************    
    function OpenNewWindow(pageurl, pgname) {

        var w = screen.width - 60;
        var h = screen.height - 60;
        var winprops = "location=no,scrollbars=yes,resizable=yes,status=no";
        var frmwin = window.open(pageurl, pgname, winprops);

        if (parseInt(navigator.appVersion) >= 4) {
            frmwin.window.focus();
        }
    }
    function ShowParameterPopUP() {
        var modalPopupBehaviorCtrl = $find('programmaticModalPopupBehavior');
        modalPopupBehaviorCtrl.show();
    }    
    </script>
      <div style="padding-left: 5px; padding-right: 5px; padding-top: 0px; width: 950">
        <div class="nomargin">
            <h2 class="nomargin">
                IQTools Reports &nbsp;&nbsp;&nbsp;&nbsp; <span>
                    <asp:Button ID="btrRefresh" runat="server" Text="Refresh Reports" OnClick="btrRefresh_Click" /></span>
            </h2>
        </div>
        <div class="border center" style="width: 950">
            <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                HorizontalAlign="Left" Visible="false">
                <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                    Text=""></asp:Label>
            </asp:Panel>
            <asp:UpdatePanel ID="panelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="grid">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="max-height: 480px; height: 280px; overflow: auto">
                                        <div class="whitebg" id="div-gridview">
                                            <asp:GridView ID="gridResult" runat="server" CellPadding="0" EnableModelValidation="True"
                                                BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="100%" CssClass="datatable"
                                                EmptyDataText="There are no reports at the moment" AutoGenerateColumns="False"
                                                DataKeyNames="ReportID,FullFileName" OnRowCommand="gridResult_RowCommand" OnSelectedIndexChanged="gridResult_SelectedIndexChanged"
                                                PageSize="15" OnRowDataBound="gridResult_RowDataBound">
                                                <PagerSettings Mode="Numeric" Visible="true" />
                                                <Columns>
                                                    <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Button ID="buttonSelect" runat="server" CausesValidation="false" CommandName="Select"
                                                                Text="Select" CommandArgument="<%# Container.DataItemIndex %>" Visible="true"
                                                                ForeColor="Blue" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ReportName" HeaderText="Name" ReadOnly="True" />
                                                    <asp:BoundField DataField="HasTemplate" HeaderText="Has Template" ReadOnly="True" />
                                                    <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Button ID="buttonRun" runat="server" CausesValidation="false" CommandName="RUN"
                                                                Text="Run The Report" CommandArgument="<%# Container.DataItemIndex %>" Visible="true"
                                                                ForeColor="Blue" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                                    Wrap="False" />
                                                <PagerStyle HorizontalAlign="Right" VerticalAlign="Top" />
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
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">                               
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnRaisePopup" runat="server" Text="Save" Width="60px" Style="display: none" />
                    <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 320px;
                        border: solid 1px #808080; background-color: #F0F0F0">
                        <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                            cursor: move; height: 18px">
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
                        <table width="100%" border="0">
                            <tbody>
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
                                </tr>
                                <tr>
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
                                    <td style="width: 10%; white-space: nowrap" align="right">
                                        CD4 Cutoff For ART:
                                    </td>
                                    <td style="width: 60%" align="left">
                                        <asp:TextBox runat="server" ID="textCD4" Width="80px" Text="350" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCD4" runat="server" TargetControlID="textCD4"
                                            FilterType="Numbers" />
                                        <asp:HiddenField ID="hCutOffCD4" runat="server" Value="350" />
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
                        CancelControlID="btnActionCancel" DropShadow="true" PopupDragHandleControlID="divTitle">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updReportTemplates" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="divData" runat="server" Style="width: 460px;" HorizontalAlign="Left"
                        Wrap="False" Visible="false">
                        <table cellpadding="1" cellspacing="1" border="0" width="100%" style="border: solid 1px #808080;
                            margin-bottom: 10px">
                            <tr>
                                <td valign="top" colspan="2" style="background-color: #C0C0C0; color: #FFFFFF; font-weight: bold;
                                    padding: 3px">
                                    <asp:Label ID="lblTitle" runat="server" Text="Report Information"></asp:Label>
                                    <asp:HiddenField ID="HReport_ID" EnableViewState="true" runat="server" Value="" />
                                    <asp:HiddenField ID="HDateParams" EnableViewState="true" runat="server" Value="" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="white-space: nowrap">
                                    Name:
                                </td>
                                <td align="left" style="white-space: nowrap">
                                    <asp:TextBox ID="txtreportName" runat="server" Width="80px"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlFileUpload" runat="server" Visible="false">
                                        <table cellpadding="2" cellspacing="0" border="0">
                                            <tr>
                                                <td align="right" style="width: 160px; white-space: nowrap" valign="middle">
                                                    Available Template:
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:Button ID="DocumentTemplateLink" runat="server" Visible="false" CssClass="doclist"
                                                        OnClick="DocumentTemplateLink_Click" />
                                                    <asp:Label ID="lblNoTemplateFile" runat="server">No template file available</asp:Label>
                                                    <asp:Panel ID="panelAllowPrint" runat="server" Visible="false">
                                                        <br />
                                                        <input type="button" id="cmdPrint" value="Run The Report" onclick="ShowParameterPopUP();"
                                                            runat="server" style="cursor: pointer; border: solid 1px #FFFFFF; background-repeat: no-repeat;
                                                            padding-left: 25px; width: 120px; height: 30px; margin-left: 10px;" /></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 160px; white-space: nowrap; background-color: #C8C8C8">
                                                    Upload the Template: only(xsl)
                                                </td>
                                                <td valign="top" align="left" style="white-space: nowrap; background-color: #C8C8C8">
                                                    <asp:FileUpload ID="ctrlFileUpload" Width="280px" Height="19px" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 160px; white-space: nowrap">
                                                    <asp:Button ID="cmdUpload" Width="140px" Height="19px" runat="server" Text=" Save &amp; Upload"
                                                        OnClick="cmdUpload_Click" />
                                                </td>
                                                <td valign="top" align="left" nowrap="nowrap">
                                                    <asp:Button ID="buttonReportXML" runat="server" Text="Get ReportXML" Width="160px"
                                                        OnClick="buttonReportXML_Click" />
                                                    <asp:Button ID="cmdTFileDelete" runat="server" Text="Remove Template File" Width="160px" />
                                                    <asp:Button ID="buttonClose" runat="server" Text="Close" Width="160px" OnClick="buttonClose_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <!-- Template File Delete Confirmation Popup -->
                    <asp:Panel ID="divTFPopup" runat="server" Style="display: none; width: 320px; border: solid 1px #808080;
                        background-color: #E0E0E0;">
                        <asp:Panel ID="divTFPopup_Title" runat="server" Style="border: solid 1px #808080;
                            margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="width: 100%;">
                                        <span style="font-weight: bold; color: White">&nbsp;Remove Template File</span>
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="width: 48px" valign="middle" align="center">
                                    <img src="../Images/caution.png" alt="" width="32" height="32" />
                                </td>
                                <td style="width: 100%;" valign="middle" align="center">
                                    <p style="text-align: left; margin-top: 5px; margin-bottom: 5px;">
                                        Would you really like to remove this template?
                                    </p>
                                </td>
                            </tr>
                        </table>
                        <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                            text-align: center; padding-top: 5px; padding-bottom: 5px">
                            <asp:Button ID="cmdTFConfirmDelete" runat="server" Text="Delete" Width="80px" Style="border: solid 1px #808080;"
                                OnClick="cmdTFConfirmDelete_Click" />
                            <asp:Button ID="cmdTFConfirmCancel" runat="server" Text="Cancel" Width="80px" Style="border: solid 1px #808080;" />
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="deleteTFModalPopupExtender" runat="server" TargetControlID="cmdTFileDelete"
                        PopupControlID="divTFPopup" BackgroundCssClass="modalBackground" CancelControlID="cmdTFConfirmCancel"
                        DropShadow="true" PopupDragHandleControlID="divTFPopup_Title" Enabled="True">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="cmdUpload" />
                    <asp:PostBackTrigger ControlID="buttonReportXML" />
                    <asp:PostBackTrigger ControlID="DocumentTemplateLink" />
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

