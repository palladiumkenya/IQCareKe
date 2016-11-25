<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportCustom" Title="Untitled Page" Codebehind="frmReportCustom.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div style="padding-left: 5px; padding-right: 5px; padding-top: 0px;">
        <h1 class="nomargin" id="hBar" runat="server">
            Custom Reports</h1>
        <script language="javascript" type="text/javascript">

            function hide(divId) {
                if (document.layers) document.layers[divId].visibility = 'hide';
                else if (document.all) document.all[divId].style.display = 'none';
                else if (document.getElementById) document.getElementById(divId).style.display = 'none';
            }

            //shows div
            function show(divId) {
                if (document.layers) document.layers[divId].visibility = 'show';
                else if (document.all) document.all[divId].style.display = 'inline';
                else if (document.getElementById) document.getElementById(divId).style.display = 'inline';
            }

            function CheckReport(ddlReportName) {
                if (document.getElementById(ddlReportName).value == "") {
                    alert('No Report Selected.');
                    return false;
                }
                return true;
            }
            function fnRedirect(id) {
                //alert(id);
                window.location.href = "frmReportCustomNew.aspx?ReportId=" + id + "&ReportImpMode=RIEdit";
            }
            function redirectNew() {
                window.location.href = "frmReportCustomNew.aspx";
            }
        
        </script>
        <%-- <form id="CustomReports" method="post" runat="server"--%>
       <%-- <asp:ScriptManager ID="scm_report" runat="server">
        </asp:ScriptManager>--%>
        <div class="border center formbg">
            <asp:UpdatePanel ID="up_report" runat="server">
                <ContentTemplate>
                    <table class="center" cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="width: 65%" align="right">
                                </td>
                                <td style="width: 35%; padding-right: 40px;" align="right">
                                    <div>
                                        <asp:Button ID="btnNew" runat="server" Text="Create New Report" BackColor="#99cc66"
                                            OnClick="btnNew_Click" />
                                        <%--                                        <input type="button" id = "btnNew" style="background-color: #99cc66" value="Create New Report"
                                            onclick="btnNewRport_Click" />
                                        --%>
                                        <!-- <a class="button" href="frmReportCustomNew.aspx" runat="server">Create New Report</a>-->
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg" valign="top" colspan="2">
                                    <table width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        Saved Reports:</label>&nbsp;
                                                    <asp:DropDownList ID="ddCategory" runat="server" Width="120px" OnSelectedIndexChanged="ddCategory_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 40%" align="center">
                                                    <asp:DropDownList ID="ddTitle" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:Button ID="btnRun" OnClick="btnRun_Click" runat="server" Text="Run" Width="50px">
                                                    </asp:Button>&nbsp;&nbsp;
                                                    <asp:Button ID="btnEdit" OnClick="btnEdit_Click" runat="server" Text="Edit" Width="50px">
                                                    </asp:Button>&nbsp;&nbsp;
                                                    <asp:Button ID="btnExportReport" OnClick="btnExportReport_Click" runat="server" Text="Export Report"
                                                        Width="90px"></asp:Button>
                                                    <asp:HiddenField ID="hidMessage" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hidorgmsg" runat="server"></asp:HiddenField>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 30%" align="right">
                                                    <label>
                                                        Import Reports:</label>
                                                </td>
                                                <td style="width: 40%" align="center">
                                                    <%--<input style="width: 306px; height: 23px" id="inptReport" title="Select XML File for import"
                                                        type="file" runat="server" />--%>
                                                    <asp:FileUpload ID="inptReport" Style="width: 306px; height: 23px" runat="server" />
                                                </td>
                                                <td style="height: 27px; width: 30%; padding-left: 30px;" align="left">
                                                    <asp:Button ID="btnEditImport" OnClick="btnEditImport_Click" runat="server" Text="Edit"
                                                        Width="50px"></asp:Button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Text="Close" Width="50">
                                    </asp:Button>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="60" OnClick="btnRefresh_Click">
                                    </asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnEditImport" />
                    <asp:PostBackTrigger ControlID="btnExportReport" />
                    <asp:PostBackTrigger ControlID="btnEdit" />
                    <asp:PostBackTrigger ControlID="btnRefresh" />
                    <asp:PostBackTrigger ControlID="btnNew" />
                    <asp:AsyncPostBackTrigger ControlID="btnRun" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
