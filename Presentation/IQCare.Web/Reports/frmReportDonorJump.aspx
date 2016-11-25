<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportDonorJump" Title="Untitled Page" Codebehind="frmReportDonorJump.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--  <head runat="server">
    <link rel="stylesheet" type="text/css" href="../Style/StyleSheet.css" />
    </head>--%>
    <script language="javascript" type="text/javascript">



        ////For differentiate between ART & PMTCT
        function ShowHide(Id) {

            if (Id == "1") {

                //     document.getElementById('divPMTCT').style.display = 'inline';
                //     document.getElementById('divART').style.display = 'none';
                document.getElementById('<%=spanTrack1PMTCT.ClientID%>').style.display = 'inline';
                document.getElementById('<%=spanHIVExposed.ClientID%>').style.display = 'inline';
                if (document.getElementById('<%=hdCountryId.ClientID%>').value == "161") {

                    document.getElementById('<%=spanOGAC.ClientID%>').style.display = 'none';
                    document.getElementById('<%=spanMOH.ClientID%>').style.display = 'none';
                    document.getElementById('<%=spanMR.ClientID%>').style.display = 'none';

                }
                else if (document.getElementById('<%=hdCountryId.ClientID%>').value == "304") {
                    document.getElementById('<%=spanOGAC.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanMOH.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanMR.ClientID%>').style.display = 'none';

                }
                else if (document.getElementById('<%=hdCountryId.ClientID%>').value == "216") {
                    document.getElementById('<%=spanMR.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanOGAC.ClientID%>').style.display = 'none';
                    document.getElementById('<%=spanMOH.ClientID%>').style.display = 'none';

                }
                else {
                    document.getElementById('<%=spanOGAC.ClientID%>').style.display = 'none';
                    document.getElementById('<%=spanMOH.ClientID%>').style.display = 'none';
                    document.getElementById('<%=spanMR.ClientID%>').style.display = 'none';

                }
                //        document.getElementById('<%=spanNMonthlyReport.ClientID%>').style.display ='none';
                //        document.getElementById('<%=spanTrackReport.ClientID%>').style.display = 'none';
                //        document.getElementById('<%=spanTMonthlyReport.ClientID%>').style.display = 'none';
                //        document.getElementById('<%=spanNACPCohort.ClientID%>').style.display='none';
                //        document.getElementById('<%=spanTrackReport.ClientID%>').style.display = 'none' ;
            }
            else if (Id == "2") {

                //        document.getElementById('divART').style.display = 'inline';
                //        document.getElementById('divPMTCT').style.display = 'none';
                if (document.getElementById('<%=hdSystemId.ClientID%>').value == 1) {
                    document.getElementById('<%=spanNMonthlyReport.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanTrackReport.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanTMonthlyReport.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanNACPCohort.ClientID%>').style.display = 'none';

                }
                else if (document.getElementById('<%=hdSystemId.ClientID%>').value == 2) {
                    document.getElementById('<%=spanTMonthlyReport.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanNACPCohort.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanTrackReport.ClientID%>').style.display = 'inline';
                    document.getElementById('<%=spanNMonthlyReport.ClientID%>').style.display = 'none';


                }
                //        document.getElementById('<%=spanTrack1PMTCT.ClientID%>').style.display = 'none';
                //        document.getElementById('<%=spanHIVExposed.ClientID%>').style.display = 'none';
                //        document.getElementById('<%=spanOGAC.ClientID%>').style.display = 'none';
                //        document.getElementById('<%=spanMOH.ClientID%>').style.display = 'none';
                //        document.getElementById('<%=spanMR.ClientID%>').style.display = 'none';


            }


        }


        function Validate() {

            var blnCDCReport = document.getElementById('<%=rdoCDCReport.ClientID%>').checked;
            var blnNMonthlyReport = document.getElementById('<%=rdoNMonthlyReport.ClientID%>').checked;
            var blnTMonthlyReport = document.getElementById('<%=rdoTMonthlyReport.ClientID%>').checked;
            var blnNACPCohortReport = document.getElementById('<%=rdoNACPCohort.ClientID %>').checked;
            var blnNigeriaMR = document.getElementById('<%=rdoNigeriaMR.ClientID%>').checked;
            var blnUgandaMonthly = document.getElementById('<%=rdoUgandaMonthly.ClientID%>').checked;
            var blnUgandaOGAC = document.getElementById('<%=rdoUgandaOGAC.ClientID%>').checked;
            var blnTrackHIVExposedPMTCT = document.getElementById('<%=rdotrackHIVExposedPMTCT.ClientID%>').checked;
            var blnrdoTrack1PMTCT = document.getElementById('<%=rdoTrack1PMTCT.ClientID%>').checked;

            if (blnCDCReport == false && blnNMonthlyReport == false && blnTMonthlyReport == false && blnNACPCohortReport == false && blnrdoTrack1PMTCT == false && blnTrackHIVExposedPMTCT == false && blnUgandaOGAC == false && blnNigeriaMR == false && blnUgandaMonthly == false) {
                alert("Select Option Button !");
                return false;
            }

        }

        function UnCheck(rdoBtn) {
            document.getElementById(rdoBtn).checked = false;
        }
        function bgPmtct() {
            document.getElementById('btnART').style.backgroundColor = "Silver";
            document.getElementById('btnPMTCT').style.backgroundColor = "White";

        }
        function bgArt() {
            document.getElementById('btnART').style.backgroundColor = "White";
            document.getElementById('btnPMTCT').style.backgroundColor = "Silver";

        }

        function clientActiveTabChanged(sender, args) {

            //  if the tab does not exist and it is the active tab, 
            //  trigger the async-postback
            if (sender.get_activeTabIndex() == 0) {
                // load tab1
                ShowHide(2);
            }
            else if (sender.get_activeTabIndex() == 1) {
                // load tab2
                ShowHide(1);
            }
        }
    
    </script>
    <div>
        <%-- <form id="ReportDonorJump" method="post" runat="server" >--%>
      <%--  <asp:ScriptManager ID="mst" runat="server">
        </asp:ScriptManager>--%>
        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
            <ContentTemplate>
                <h1 class="nomargin">
                    Donor Reports</h1>
                <%--<div style="background-color: Gray">
        <input id="btnART" type="button" class="Tab" onclick="ShowHide(2);bgArt();" value="HIV Care"
            style="background-color: White" />
        <input id="btnPMTCT" type="button" class="Tab" onclick="ShowHide(1);bgPmtct();" value="PMTCT"
        style="background-color:Silver" />
    </div>--%>
                <div id="divCommon">
                    <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6" style="height: 273px">
                        <tr>
                            <%--<td class="border center whitebg" style="padding-left: 20" align="left" valign="top">--%>
                            <td class="border pad5" style="background-color: Gray" align="left" valign="top">
                                <act:TabContainer ID="tabControl" runat="server" Height="500px" ActiveTabIndex="0"
                                    CssClass="ajax__tab_technorati-theme" OnClientActiveTabChanged="clientActiveTabChanged">
                                    <act:TabPanel ID="tbpnlgeneral" runat="server" Font-Size="Medium" HeaderText="HIV">
                                        <ContentTemplate>
                                            <label>
                                                Select Report:</label>
                                            <br />
                                            <%--<div id="divART" style="display: inline; margin: 3px 3px 3px 3px; overflow: auto;
                              width   : 100%; height: 100px; text-align: left">--%>
                                            <table id="TableART" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td class="bold pad18" style="width: 50%">
                                                        <span id="spanTrackReport" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoCDCReport" value="Track 1.0 Facility Based Quarterly Report"
                                                                onmouseup="up(this);" onfocus="up(this);" onclick="down(this); " runat="server" name="rptName" />Track
                                                            1.0 Facility Based Quarterly Report </span>
                                                        <br />
                                                        <span id="spanNMonthlyReport" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoNMonthlyReport" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                                runat="server" name="rptName" />Monthly NACA Report<br />
                                                        </span><span id="spanTMonthlyReport" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoTMonthlyReport" value="NACP Monthly/Quarterly Report" onmouseup="up(this);"
                                                                onfocus="up(this);" onclick="down(this);" runat="server" name="rptName" />NACP
                                                            Monthly/Quarterly Report
                                                            <br />
                                                        </span><span id="spanNACPCohort" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoNACPCohort" value="NACP Cohort Tracking Report" onmouseup="up(this);" onfocus="up(this);"
                                                                onclick="down(this);" runat="server" name="rptName" />NACP Cohort Tracking Report
                                                            <br />
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</div>--%>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="tbpnldynamic" HeaderText="PMTCT" runat="server" TabIndex="2">
                                        <ContentTemplate>
                                            <label>
                                                Select Report:</label>
                                            <br />
                                            <%--<div id="divPMTCT1" style="display: inline; margin: 3px 3px 3px 3px; overflow: auto;
                         width: 100%; height: 100px; text-align: left">--%>
                                            <table id="TablePMTCT" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td class="bold pad18" style="width: 100%">
                                                        <span id="spanTrack1PMTCT" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoTrack1PMTCT" value="Track 1.0 PMTCT " onmouseup="up(this);" onfocus="up(this);"
                                                                onclick="down(this);" runat="server" name="rptName" />Track 1.0 PMTCT
                                                            <br />
                                                        </span><span id="spanHIVExposed" style="display: inline" runat="server">
                                                            <input type="radio" id="rdotrackHIVExposedPMTCT" value="Track 1.0 HIV Exposed Infants" onmouseup="up(this);"
                                                                onfocus="up(this);" onclick="down(this); " runat="server" name="rptName" />Track
                                                            1.0 HIV Exposed Infants
                                                            <br />
                                                        </span><span id="spanOGAC" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoUgandaOGAC" value="Uganda OGAC" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); "
                                                                runat="server" name="rptName" />OGAC
                                                            <br />
                                                        </span><span id="spanMOH" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoUgandaMonthly" value="Monthly Report Form-MOH" onmouseup="up(this);" onfocus="up(this);"
                                                                onclick="down(this);  " runat="server" name="rptName" />Monthly Report Form-MOH
                                                            <br />
                                                        </span><span id="spanMR" style="display: inline" runat="server">
                                                            <input type="radio" id="rdoNigeriaMR" value="MR Report" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); "
                                                                runat="server" name="rptName" />MR Report
                                                            <br />
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</div> --%>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                </act:TabContainer>
                                <%-- <label>
                        Select Report:</label>
                    <br />--%>
                                <input type="hidden" id="hdSystemId" runat="server" />
                                <%--<input type="hidden" id="hdModuleId" runat="server" />--%>
                                <input type="hidden" id="hdCountryId" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit"></asp:PostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="tabControl" EventName="ActiveTabChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
