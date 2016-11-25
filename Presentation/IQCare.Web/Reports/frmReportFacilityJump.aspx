<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportFacilityJump"
    Title="Untitled Page" Codebehind="frmReportFacilityJump.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript"> 
        function Validate() {
            var blnCDCReport = document.getElementById('<%=rdoARVPickup.ClientID%>').checked;
            var blnCDCReport2 = document.getElementById('<%=rdoMissARVPickup.ClientID%>').checked;
            var blnPatientEnrolMnth = document.getElementById('<%=rdoPatiEnrollMonth.ClientID%>').checked;
            var blnTBStatusReport = document.getElementById('<%=rdoTBStatus.ClientID%>').checked;
            var blnARVRegimen = document.getElementById('<%=rdoARVRegimen.ClientID%>').checked;
            var blnTBPatientwithARVWithoutARV = document.getElementById('<%=rdoTBCase.ClientID %>').checked;
            var blnNot_ArtPatientReport = document.getElementById('<%=rdoNonArtPatientsReport.ClientID%>').checked;
            var blnptnotvisitedUnknown = document.getElementById('<%=rdoptnotvisitedUnknown.ClientID%>').checked;
            var blnGeoPatientsDistribution = document.getElementById('<%=rdoGeoPatientsDistribution.ClientID%>').checked;
            var blnUserDetails = document.getElementById('<%=rdoUserDetail.ClientID%>').checked;
            var blnUserDetails1 = document.getElementById('<%=RdoUserDetail1.ClientID%>').checked;
            var blnKenyaHealth = document.getElementById('<%=rdoKenyaHealth.ClientID%>').checked;
            var blnBornToLive = document.getElementById('<%=rdoBornToLive.ClientID%>').checked;
            var blnNASCOP = document.getElementById('<%=rdoNASCOP.ClientID%>').checked;
            var blnUgandaMOH = document.getElementById('<%=rdoUgandaMOH.ClientID%>').checked;
            var blnNNRIMS = document.getElementById('<%=rdoNNRIMS.ClientID%>').checked;


            if (blnTBPatientwithARVWithoutARV == false && blnCDCReport == false
            && blnKenyaHealth == false && blnCDCReport2 == false && blnPatientEnrolMnth == false
            && blnTBStatusReport == false && blnARVRegimen == false && blnNot_ArtPatientReport == false
            && blnptnotvisitedUnknown == false && blnGeoPatientsDistribution == false && blnUserDetails == false
            && blnBornToLive == false && blnNASCOP == false && blnUgandaMOH == false && blnNNRIMS == false && blnARVRegimen == false && blnUserDetails1 == false) {
                alert("Select Option Button !");
                return false;
            }

        }
        function DisableEndDate() {

            var blnNot_ArtPatientReport = document.getElementById('<%=rdoNonArtPatientsReport.ClientID%>').checked;
            var blnptnotvisitedUnknown = document.getElementById('<%=rdoptnotvisitedUnknown.ClientID%>').checked;

            if (blnptnotvisitedUnknown == true || blnNot_ArtPatientReport == true) {

                document.getElementById('<%=txtEndDate.ClientID%>').value = '<%=Application["AppCurrentDate"]%>'
                document.getElementById('<%=txtEndDate.ClientID%>').disabled = true;
                document.getElementById('imgdatepicker').disabled = true;
            }
            else {
                document.getElementById('<%=txtEndDate.ClientID%>').value = '';
                document.getElementById('<%=txtEndDate.ClientID%>').disabled = false;
                document.getElementById('imgdatepicker').disabled = false;
            }

        }
        function HideShowButton(ModuleId) {

            if (ModuleId == "1") {
                document.getElementById('btnART').style.display = 'none';
                document.getElementById('btnPMTCT').style.display = 'inline';
                ShowHide(1);

            }
            else if (ModuleId == "2") {
                document.getElementById('btnART').style.display = 'inline';
                document.getElementById('btnPMTCT').style.display = 'none';
                ShowHide(2);
            }
            else {
                document.getElementById('btnART').style.display = 'inline';
                document.getElementById('btnPMTCT').style.display = 'inline';
                ShowHide(2);
            }

        }



        function ShowHide(Id) {

            if (Id == "1") {

                //     document.getElementById('divPMTCT').style.display = 'inline';
                //     document.getElementById('divART').style.display = 'none';
                document.getElementById('UserDetail').style.display = 'inline';
                document.getElementById('UserDetail1').style.display = 'none';
                document.getElementById('<%=RdoUserDetail1.ClientID%>').checked = false;
                if (document.getElementById('<%=hdCountryId.ClientID%>').value == "161") {

                    document.getElementById('kenyahealthReport').style.display = 'inline';
                    document.getElementById('Nascop').style.display = 'inline';
                    document.getElementById('BornToLive').style.display = 'inline';

                }
                else {
                    document.getElementById('kenyahealthReport').style.display = 'none';
                    document.getElementById('Nascop').style.display = 'none';
                    document.getElementById('BornToLive').style.display = 'none';


                }
                if (document.getElementById('<%=hdCountryId.ClientID%>').value == "290") {
                    document.getElementById('UgandaMOHReport').style.display = 'inline';

                }
                else {

                    document.getElementById('UgandaMOHReport').style.display = 'none';

                }
                if (document.getElementById('<%=hdCountryId.ClientID%>').value == "216") {
                    document.getElementById('NNRIMS').style.display = 'inline';

                }
                else {

                    document.getElementById('NNRIMS').style.display = 'none';
                }


                document.getElementById('ARVPickup').style.display = 'none';
                document.getElementById('MissARVPickup').style.display = 'none';
                document.getElementById('PatiEnrollMonth').style.display = 'none';
                document.getElementById('TBStatus').style.display = 'none';
                document.getElementById('TBCase').style.display = 'none';
                document.getElementById('ARVRegimen').style.display = 'none';
                document.getElementById('GeoPatientsDistribution').style.display = 'none';
                document.getElementById('NonArtPatientsReport').style.display = 'none';
                document.getElementById('ptnotvisitedUnknown').style.display = 'none';
                bgPmtct(); hide('UgandaMOH'); hide('KenyaHealth'); hide('AskDate'); fnblank(); hide('Cohort'); hide('DefaulterAsOf'); hide('ARVPickupPatients'); hide('UserDetails'); DisableEndDate();
            }
            else if (Id == "2") {

                //        document.getElementById('divART').style.display = 'inline';
                //        document.getElementById('divPMTCT').style.display = 'none';
                document.getElementById('UserDetail1').style.display = 'inline';
                document.getElementById('UserDetail').style.display = 'none';
                if (document.getElementById('<%=hdSystemID.ClientID%>').value == 1) {
                    document.getElementById('ARVPickup').style.display = 'inline';
                    document.getElementById('MissARVPickup').style.display = 'inline';
                    document.getElementById('PatiEnrollMonth').style.display = 'inline';
                    document.getElementById('TBStatus').style.display = 'none';
                    document.getElementById('TBCase').style.display = 'none';
                    document.getElementById('ARVRegimen').style.display = 'none';
                    document.getElementById('GeoPatientsDistribution').style.display = 'none';
                    document.getElementById('NonArtPatientsReport').style.display = 'none';
                    document.getElementById('ptnotvisitedUnknown').style.display = 'none';
                    document.getElementById('BornToLive').style.display = 'none';
                    document.getElementById('Nascop').style.display = 'none';
                    document.getElementById('kenyahealthReport').style.display = 'none';
                    document.getElementById('UgandaMOHReport').style.display = 'none';
                    document.getElementById('NNRIMS').style.display = 'none';

                }
                else if (document.getElementById('<%=hdSystemID.ClientID%>').value == 2) {
                    document.getElementById('ARVPickup').style.display = 'inline';
                    document.getElementById('MissARVPickup').style.display = 'inline';
                    document.getElementById('PatiEnrollMonth').style.display = 'inline';
                    document.getElementById('TBStatus').style.display = 'inline';
                    document.getElementById('TBCase').style.display = 'inline';
                    document.getElementById('ARVRegimen').style.display = 'inline';
                    document.getElementById('GeoPatientsDistribution').style.display = 'inline';
                    document.getElementById('NonArtPatientsReport').style.display = 'inline';
                    document.getElementById('ptnotvisitedUnknown').style.display = 'inline';
                    document.getElementById('BornToLive').style.display = 'none';
                    document.getElementById('Nascop').style.display = 'none';
                    document.getElementById('kenyahealthReport').style.display = 'none';
                    document.getElementById('UgandaMOHReport').style.display = 'none';
                    document.getElementById('NNRIMS').style.display = 'none';


                }
                bgArt(); hide('UgandaMOH'); hide('KenyaHealth'); hide('AskDate'); fnblank(); hide('Cohort'); hide('DefaulterAsOf'); hide('ARVPickupPatients'); hide('UserDetails'); DisableEndDate();

            }


        }

        function bgPmtct() {
            //        document.getElementById('btnART').style.backgroundColor ="Silver";
            //        document.getElementById('btnPMTCT').style.backgroundColor ="White"; 
            document.getElementById('<%=rdoUserDetail.ClientID%>').checked = false;
            document.getElementById('<%=rdoGeoPatientsDistribution.ClientID%>').checked = false;
            document.getElementById('<%=rdoptnotvisitedUnknown.ClientID%>').checked = false;
            document.getElementById('<%=rdoNonArtPatientsReport.ClientID%>').checked = false;
            document.getElementById('<%=rdoARVRegimen.ClientID%>').checked = false;
            document.getElementById('<%=rdoTBCase.ClientID%>').checked = false;
            document.getElementById('<%=rdoTBStatus.ClientID%>').checked = false;
            document.getElementById('<%=rdoPatiEnrollMonth.ClientID%>').checked = false;
            document.getElementById('<%=rdoMissARVPickup.ClientID%>').checked = false;
            document.getElementById('<%=rdoARVPickup.ClientID%>').checked = false;

        }
        function bgArt() {
            //        document.getElementById('btnART').style.backgroundColor ="White";
            //        document.getElementById('btnPMTCT').style.backgroundColor ="Silver"; 
            document.getElementById('<%=rdoUgandaMOH.ClientID%>').checked = false;
            document.getElementById('<%=rdoKenyaHealth.ClientID%>').checked = false;
            document.getElementById('<%=rdoNNRIMS.ClientID%>').checked = false;
            document.getElementById('<%=rdoBornToLive.ClientID%>').checked = false;
            document.getElementById('<%=rdoNASCOP.ClientID%>').checked = false;

        }


        function fnblank() {
            document.getElementById('<%=txtStartDate.ClientID %>').value = '';
        }
        function chkAllUser_onclick() {
            if (document.getElementById('<%=chkAllUser.ClientID%>').checked == true) {
                document.getElementById('lblSelectUser').style.display = 'none';
                document.getElementById('<%=ddlSelectUser.ClientID %>').style.display = 'none';
            }
            else {
                document.getElementById('lblSelectUser').style.display = 'inline';
                document.getElementById('<%=ddlSelectUser.ClientID %>').style.display = 'inline';
            }

        }



        function rdoUserDetail_onclick() {
            document.getElementById('ctl00_IQCareContentPlaceHolder_TxtFromDate').value = '';
            document.getElementById('ctl00_IQCareContentPlaceHolder_TxtToDate').value = '';
        }
        function RdoUserDetail1_onclick() {
            document.getElementById('ctl00_IQCareContentPlaceHolder_TxtFromDate').value = '';
            document.getElementById('ctl00_IQCareContentPlaceHolder_TxtToDate').value = '';
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
   <%-- <asp:ScriptManager ID="mst" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
        <ContentTemplate>
            <div style="padding-top: 30px; padding-left: 5px; padding-right: 5px;">
                <h1 class="nomargin">
                    Facility Reports</h1>
                <div style="background-color: Gray;" class="border pad5">
                    <table border="0" cellpadding="0" cellspacing="0" width="99%" style="height: 515px">
                        <tr>
                            <td style="height: 100%">
                                <%--TabContainer Table--%>
                                <act:TabContainer ID="tabControl" runat="server" Height="500px" ActiveTabIndex="0"
                                    CssClass="ajax__tab_technorati-theme" OnClientActiveTabChanged="clientActiveTabChanged">
                                    <act:TabPanel ID="tbpnlgeneral" runat="server" Font-Size="Medium" HeaderText="HIV">
                                        <HeaderTemplate>
                                            HIV
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 19%">
                                                <tr>
                                                    <td>
                                                        <label id="Label2">
                                                            Select Report:</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">
                                                        <table id="TableART" cellspacing="0" cellpadding="0" border="0" style="width: 50%">
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="ARVPickup" style="width: 450px">
                                                                        <input type="radio" id="rdoARVPickup" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('ARVPickupPatients');hide('Cohort');hide('AskDate');hide('DefaulterAsOf');hide('patientId');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="lblARVPickup">ARV Pick up Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="MissARVPickup" style="width: 450px">
                                                                        <input type="radio" id="rdoMissARVPickup" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('DefaulterAsOf');hide('Cohort');hide('AskDate');hide('ARVPickupPatients');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="lblMissedARVPickup">Missed ARV Pick up Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="PatiEnrollMonth" style="width: 450px">
                                                                        <input type="radio" id="rdoPatiEnrollMonth" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('AskDate');fnblank();hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="lblPatientEnrollMonth">Patient Enrolled by Month Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="UserDetail1" style="width: 450px">
                                                                        <input type="radio" id="RdoUserDetail1" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');hide('AskDate');show('UserDetails');fnblank();hide('DefaulterAsOf');hide('Cohort');hide('ARVPickupPatients');DisableEndDate();RdoUserDetail1_onclick()" />
                                                                        &nbsp;<label id="LblUserDetail1">Quality Assurance</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="TBStatus" style="width: 450px">
                                                                        <input type="radio" id="rdoTBStatus" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('AskDate');fnblank();hide('DefaulterAsOf');hide('Cohort');hide('ARVPickupPatients');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="lblTBStatus">TB Status by Age and Sex Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="TBCase" style="width: 450px">
                                                                        <input type="radio" id="rdoTBCase" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('AskDate');fnblank();hide('DefaulterAsOf');hide('Cohort');hide('ARVPickupPatients');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="lblTBCases">TB Cases before and after starting ARVs</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="ARVRegimen" style="width: 450px">
                                                                        <input type="radio" id="rdoARVRegimen" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('AskDate');fnblank();hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="lblARVRegimenforAandC">ARV Regimen for Adult/Child Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="NonArtPatientsReport" style="width: 450px">
                                                                        <input type="radio" id="rdoNonArtPatientsReport" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('AskDate');fnblank();hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');DisableEndDate();" />
                                                                        &nbsp;<label id="Label1">Patients who have not visited recently with Non-ART Status</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="ptnotvisitedUnknown" style="width: 450px">
                                                                        <input type="radio" id="rdoptnotvisitedUnknown" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');show('AskDate');hide('UserDetails');hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');DisableEndDate();" />
                                                                        &nbsp;<label id="lblptnotvisitedUnknown">Patients who have not visited recently with
                                                                            Unknown Status</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="GeoPatientsDistribution" style="width: 450px">
                                                                        <input type="radio" id="rdoGeoPatientsDistribution" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth'); hide('AskDate');hide('Cohort');hide('ARVPickupPatients');hide('DefaulterAsOf');DisableEndDate();" />
                                                                        &nbsp;<label id="lblGeoPatientsDistribution">Geographical Patients Distribution Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="tbpnldynamic" runat="server" HeaderText="PMTCT">
                                        <ContentTemplate>
                                            <table style="width: 19%">
                                                <tr>
                                                    <td colspan="2" nowrap="nowrap">
                                                        <label id="Label19">
                                                            Select Report:</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" nowrap="nowrap">
                                                        <table id="TablePMTCT" cellspacing="0" cellpadding="0" width="50%" border="0">
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="UserDetail" style="width: 450px">
                                                                        <input type="radio" id="rdoUserDetail" name="0" runat="server" onclick="hide('UgandaMOH');hide('KenyaHealth');hide('AskDate');show('UserDetails');fnblank();hide('DefaulterAsOf');hide('Cohort');hide('ARVPickupPatients');DisableEndDate();rdoUserDetail_onclick()" />
                                                                        <label id="lbluserDetail">
                                                                            Quality Assurance</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="BornToLive" style="width: 450px">
                                                                        <input type="radio" id="rdoBornToLive" name="0" runat="server" onclick="hide('UgandaMOH');show('KenyaHealth'); hide('AskDate');hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');" />
                                                                        <label id="lblBornToLive">
                                                                            Born-To-Live</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="Nascop" style="width: 450px">
                                                                        <input type="radio" id="rdoNASCOP" name="0" runat="server" onclick="hide('UgandaMOH');show('KenyaHealth');hide('AskDate');hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');" />
                                                                        <label id="lblNASCOP">
                                                                            NASCOP Monthly Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="NNRIMS" style="width: 450px">
                                                                        <input type="radio" id="rdoNNRIMS" name="0" runat="server" onclick="show('KenyaHealth');hide('UgandaMOH');hide('AskDate');hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');" />
                                                                        <label id="lblNNRIMS">
                                                                            NNRIMS Monthly Report</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="kenyahealthReport" style="width: 450px">
                                                                        <input type="radio" id="rdoKenyaHealth" name="0" runat="server" onclick="hide('UgandaMOH');show('KenyaHealth'); hide('AskDate');hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');" />
                                                                        <label id="lblKenyaHealth" runat="server">
                                                                            Kenya National Integrated Form(711B)
                                                                        </label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="UgandaMOHReport" style="width: 450px">
                                                                        <input type="radio" id="rdoUgandaMOH" name="0" runat="server" onclick="show('UgandaMOH'); hide('KenyaHealth'); hide('AskDate');hide('Cohort');hide('DefaulterAsOf');hide('ARVPickupPatients');hide('UserDetails');" />
                                                                        <label id="lblUgandaMOH" runat="server">
                                                                            PMTCT Monthly Report Form - MOH
                                                                        </label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="tbpnlProgramManagement" runat="server" HeaderText="Program Management">
                                        <ContentTemplate>
                                            <html>
                                            <head>
                                                <title>Facility.Reports</title>
                                                <style type="text/css">
                                                    html, body
                                                    {
                                                        overflow: auto;
                                                    }
                                                    body
                                                    {
                                                        padding: 0;
                                                        margin: 0;
                                                    }
                                                    #silverlightControlHost
                                                    {
                                                        text-align: center;
                                                    }
                                                </style>
                                                <script type="text/javascript" src="../Incl/Silverlight.js"></script>
                                                <script type="text/javascript">
                                                    function onSilverlightError(sender, args) {
                                                        var appSource = "";
                                                        if (sender != null && sender != 0) {
                                                            appSource = sender.getHost().Source;
                                                        }

                                                        var errorType = args.ErrorType;
                                                        var iErrorCode = args.ErrorCode;

                                                        if (errorType == "ImageError" || errorType == "MediaError") {
                                                            return;
                                                        }

                                                        var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

                                                        errMsg += "Code: " + iErrorCode + "    \n";
                                                        errMsg += "Category: " + errorType + "       \n";
                                                        errMsg += "Message: " + args.ErrorMessage + "     \n";

                                                        if (errorType == "ParserError") {
                                                            errMsg += "File: " + args.xamlFile + "     \n";
                                                            errMsg += "Line: " + args.lineNumber + "     \n";
                                                            errMsg += "Position: " + args.charPosition + "     \n";
                                                        }
                                                        else if (errorType == "RuntimeError") {
                                                            if (args.lineNumber != 0) {
                                                                errMsg += "Line: " + args.lineNumber + "     \n";
                                                                errMsg += "Position: " + args.charPosition + "     \n";
                                                            }
                                                            errMsg += "MethodName: " + args.methodName + "     \n";
                                                        }

                                                        throw new Error(errMsg);
                                                    }
                                                </script>
                                            </head>
                                            <body>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr style="height: 20px; padding-left: 35px;">
                                                        <td nowrap="nowrap">
                                                            <h1>
                                                                Integrated Program Management</h1>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px; padding-left: 35px;">
                                                        <td nowrap="nowrap">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <b>Date From</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtTranDateFrom" MaxLength="11" size="8"></asp:TextBox>
                                                                        <span class="smallerlabel">(MMM-YYYY)</span>
                                                                    </td>
                                                                    <td>
                                                                        <b>To</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtTranDateTo" MaxLength="11" size="8"></asp:TextBox>
                                                                        <span class="smallerlabel">(MMM-YYYY)</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="btnView" Text="View" PostBackUrl="~/Reports/frmReportFacilityJump.aspx" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px; padding-left: 35px;">
                                                        <td>
                                                            <h3>
                                                                <asp:LinkButton ID="lnkCostPerPatientPerVisit" runat="server" OnClick="lnkCostPerPatientPerVisit_Click">Cost per Patient per Visit Report</asp:LinkButton>
                                                            </h3>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px; padding-left: 35px;">
                                                        <td nowrap="nowrap">
                                                            <h3>
                                                                Patients Cost Per Month</h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap">
                                                            <div id="silverlightControlHost" style="height: 350px;">
                                                                <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                                                                    width="100%" height="100%">
                                                                    <param name="source" value="../ClientBin/Facility.Reports.xap" />
                                                                    <param name="onError" value="onSilverlightError" />
                                                                    <param name="background" value="white" />
                                                                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                                                                    <param name="autoUpgrade" value="true" />
                                                                    <param name="initparams" value="<%=string.Format("TranDateFrom={0}, TranDateTo={1}, ReportID={2}", txtTranDateFrom.Text, txtTranDateTo.Text, 1) %>" />
                                                                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none">
                                                                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                                                                            style="border-style: none" />
                                                                    </a>
                                                                </object>
                                                                <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;
                                                                    border: 0px"></iframe>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </body>
                                            </html>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                </act:TabContainer>
                            </td>
                            <td style="height: 100%; padding: 18px 0 2px 3px;">
                                <%--HideUnhide Table--%>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 100%; background-color: #ffffff">
                                    <tr>
                                        <td valign="middle" align="left" id='DefaulterAsOf' style="display: none; width: 100%;
                                            height: 100%;">
                                            <table border="0" width="100%" style="border: #666699 0px solid; background-color: #ffffff;
                                                height: 500px;">
                                                <tr>
                                                    <td align="left" style="padding: 0 0 0 5px" nowrap="nowrap">
                                                        <label>
                                                            Defaulter as of:</label>
                                                    </td>
                                                    <td align="left" style="padding: 0 5px 0 0" nowrap="nowrap">
                                                        <asp:TextBox ID="txtDefaulterAsOf" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                                                        <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" border="0" onclick="w_displayDatePicker('<%= txtDefaulterAsOf.ClientID%>');" />
                                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="middle" align="left" id='ARVPickupPatients' style="display: none; width: 100%;
                                            height: 100%;">
                                            <table border="0" width="100%" style="border: #666699 0px solid; left: 0px; background-color: #ffffff;
                                                height: 500px;">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <input type="radio" id="rdoARVPickupForAll" name="IncludedPatient" runat="server"
                                                                        value="0" onmouseup="up(this);" onfocus="up(this);" onclick="hide('patientId'); down(this);" />ARV
                                                                    Pick up For All patients
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <input type="radio" id="rdoARVPickupForAPatient" name="IncludedPatient" runat="server"
                                                                        value="1" onmouseup="up(this);" onfocus="up(this);" onclick="toggle('patientId'); down(this);" />ARV
                                                                    Pick up For single patient
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <div id="patientId">
                                                                        <span class="smallerlabel">Country/Region:</span>
                                                                        <asp:TextBox ID="txtCountryNo" MaxLength="4" Width="25px" runat="server"> </asp:TextBox>
                                                                        <span class="smallerlabel">LPTF#/District:</span>
                                                                        <asp:TextBox ID="txtPosNo" MaxLength="4" Width="25px" runat="server"> </asp:TextBox>
                                                                        <span class="smallerlabel">Satellite/Facility#:</span>
                                                                        <asp:TextBox ID="txtSatelliteNo" MaxLength="4" Width="25px" runat="server"> </asp:TextBox>
                                                                        <span class="smallerlabel">Enrollment</span>
                                                                        <asp:TextBox ID="txtPatientId" MaxLength="8" Width="40px" runat="server"> </asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="middle" align="left" id='AskDate' style="display: none; width: 100%; height: 100%">
                                            <table border="0" width="100%" style="border: #666699 0px solid; left: 0px; background-color: #ffffff;
                                                height: 500px;">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <label>
                                                                        Date Ordered From:</label>
                                                                    <asp:TextBox ID="txtStartDate" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                                                                    <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" border="0" onclick="w_displayDatePicker('<%= txtStartDate.ClientID%>');" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <label>
                                                                        Date Ordered To:</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:TextBox ID="txtEndDate" MaxLength="11" Width="75px" runat="server" EnableViewState="true"></asp:TextBox>
                                                                    <img id="imgdatepicker" src="../Images/cal_icon.gif" height="22" alt="Date Helper"
                                                                        border="0" onclick="w_displayDatePicker('<%= txtEndDate.ClientID%>');" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="middle" align="left" id='Cohort' style="display: none; width: 100%; height: 100%">
                                            <table border="0" style="height: 500px; border: #666699 0px solid; left: 0px; background-color: #ffffff">
                                                <tr style="height: 100%">
                                                    <td style="width: 60%" nowrap="nowrap">
                                                        <label>
                                                            Patientwho started ARV in :</label>
                                                        <asp:DropDownList ID="ddMonth" runat="server" Width="90px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtYear" MaxLength="8" size="5" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td nowrap="nowrap">
                                                        <label>
                                                            View progress as of ARV in
                                                        </label>
                                                        <asp:DropDownList ID="ARVddMonth" runat="server" Width="90px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtyears" MaxLength="4" size="5" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 40%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="middle" align="left" id='UserDetails' style="display: none; width: 100%;
                                            height: 100%">
                                            <table border="0" style="height: 500px; border: #666699 0px solid; left: 0px; background-color: #ffffff">
                                                <tr style="height: 100%">
                                                    <td style="width: 60%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        From:</label>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtFromDate" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                                                                    <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" border="0" onclick="w_displayDatePicker('<%= TxtFromDate.ClientID%>');" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        To:</label>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtToDate" MaxLength="11" Width="75px" runat="server" EnableViewState="true"></asp:TextBox>
                                                                    <img id="img2" src="../Images/cal_icon.gif" height="22" alt="Date Helper" border="0"
                                                                        onclick="w_displayDatePicker('<%= TxtToDate.ClientID%>');" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    <label id="lblAllUser">
                                                                        All Users</label>
                                                                    <input type="checkbox" id="chkAllUser" name="0" runat="server" onclick="return chkAllUser_onclick()" />
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <label id='lblSelectUser'>
                                                                        Select User
                                                                    </label>
                                                                    <asp:DropDownList ID="ddlSelectUser" runat="server" Width="90px" OnSelectedIndexChanged="ddlSelectUser_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="middle" align="left" id='KenyaHealth' style="display: none; width: 100%;
                                            height: 100%">
                                            <table border="0" style="height: 500px; width: 100%; border: #666699 0px solid; left: 0px;
                                                background-color: #ffffff">
                                                <tr style="height: 100%">
                                                    <td style="width: 100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <label id='lblMonth'>
                                                                        Select Month
                                                                    </label>
                                                                    <asp:DropDownList ID="ddlMonthKenyaHealth" runat="server" Width="90px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <label id="lblYear">
                                                                        Year</label>
                                                                    <asp:TextBox ID="txtYearKenyaHealth" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="middle" align="left" id='UgandaMOH' style="display: none; width: 100%; height: 100%">
                                            <table border="0" style="height: 500px; width: 100%; border: #666699 0px solid; left: 0px;
                                                background-color: #ffffff">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <table width="100%" style="height: 0px">
                                                            <tr style="height: 0px">
                                                                <td style="height: 0px">
                                                                    <label id='lblUgandaMonth'>
                                                                        Select Month
                                                                    </label>
                                                                    <asp:DropDownList ID="ddlUgandaMOH" runat="server" Width="90px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <label id="lblUgandaYear">
                                                                        Year</label>
                                                                    <asp:TextBox ID="txtUgandaMOH" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <input type="hidden" id="hdModuleID" runat="server" />
                                                        <input type="hidden" id="hdCountryId" runat="server" />
                                                        <input type="hidden" id="hdSystemID" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="border center formbg" style="width: 912px;">
                        <table width="100%">
                            <caption>
                                <label class="alert">
                                    **Warning:Some reports can take time to display depending on the type of the report
                                    and the speed of your computer. Please be patient and allow 2-10 minutes.</label>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="tabControl" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
