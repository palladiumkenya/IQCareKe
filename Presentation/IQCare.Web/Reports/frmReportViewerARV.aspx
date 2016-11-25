<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportViewerARV" Title="Untitled Page" Codebehind="frmReportViewerARV.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <h1 class="nomargin" id="hBar" runat="server">
        Report</h1>
    <script language="javascript">
        function btn_Back() {
            history.back();

        }
        function fnPrient() {
            window.open("..\\ExcelFiles\\PView.pdf", "mywindow", "status=1,toolbar=0");
            // window.print(); 
        }
    </script>
    <div>
        <%--<form id ="PatientReports" method="post" runat="server">--%>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="pad5 center">
                    </td>
                </tr>
                <tr>
                    <td align="right" width="50%" style="height: 36px">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" OnClick="btnPrint_Click" /><br />
                    </td>
                    <td align="left" width="50%" style="height: 36px">
                        <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="120px" Visible="false"
                            OnClick="btnExcel_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" colspan="2" align="center">
                        <div class="CrystalReportViewer">
                            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                                GroupTreeStyle-ShowLines="false" EnableToolTips="true" HasDrillUpButton="False"
                                HasSearchButton="False" HasToggleGroupTreeButton="False" HasZoomFactorList="False"
                                EnableDatabaseLogonPrompt="true" Height="50px" EnableDrillDown="False"
                                HasExportButton="false" HasPrintButton="false" HyperlinkTarget="_Self" 
                                ToolPanelWidth="200px" Width="350px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 center" colspan="2">
                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" /><br />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
