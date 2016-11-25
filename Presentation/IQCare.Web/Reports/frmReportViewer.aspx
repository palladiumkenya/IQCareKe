<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportViewer" Title="Untitled Page" Codebehind="frmReportViewer.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <h1 class="margin" id="hBar" runat="server">
        Report</h1>
    <script language="javascript" type="text/javascript" >

        function btn_Back() {
            //history.back(); 

        }
        function fnPrient() {
            window.open("..\\ExcelFiles\\PView.pdf", "mywindow", "status=1,toolbar=0");
            //window.print(); 
        }
    </script>
    <div style="padding: 8px;">
        <%--<form id ="PatientReports" method="post" runat="server">--%>
        <div class="border center formbg" style="overflow:auto;">
            <table class="center" width="900px" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td align="right" width="50%">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" OnClick="btnPrint_Click" /><br />
                    </td>
                    <td align="left" width="50%">
                        <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="120px" Visible="false"
                            OnClick="btnExcel_Click" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" colspan="2" align="center">
                        <div class="crystalreportviewer" style="width: 100px;">
                            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                                GroupTreeStyle-ShowLines="false" EnableToolTips="true" HasDrillUpButton="False"
                                HasSearchButton="False" HasToggleGroupTreeButton="False" HasZoomFactorList="False"
                                EnableDatabaseLogonPrompt="true" HasExportButton="False" HasPrintButton="False"
                                Height="50px" Width="350px" />
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
