<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    Inherits="IQCare.Web.Clinical.InitialFollowupVisit" CodeBehind="frmClinical_InitialFollowupVisit.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <br />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <span class="fa fa-thumb-tack fa-1x pull-left text-info"><strong>Patient Followup </strong>
                </span>
                <br />
            </div>
            <div class="panel-body">
                <div style="padding-left: 8px; padding-right: 8px;">
                    <script language="javascript" type="text/javascript">
                        function WindowPrint() {
                            window.print();
                        }
                        function fnPageOpen(pageopen, refill, offset) {
                            if (pageopen == "Pharmacy") {

                                if (refill == '1') window.open('../Pharmacy/frmPharmacyform.aspx?opento=ArtForm&RefillFromLast=true&offset=' + offset);
                                else window.open('../Pharmacy/frmPharmacyform.aspx?opento=ArtForm');
                            }
                            else if (pageopen == "Labratory") {
                                window.open('../Laboratory/LabRecordEntry.aspx?opento=ArtForm');
                            }
                            else if (pageopen == "LabTest") {
                                window.open('../Laboratory/LabRequestForm.aspx?opento=ArtForm');
                            }
                        }


                        function WindowHistory() {
                            history.go(-1);
                            return false;
                        }
                        function fnchange() {
                            var e = document.getElementById("<%=ddlpregnancy.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;


                            if (strtext == "Select") {
                                hide('spanEDD');
                                hide('miscarriage');
                                hide('Abortion');
                                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                                document.getElementById("<%=txtdatemiscarriage.ClientID %>").value = "";
                                document.getElementById("<%=txtdateinducedabortion.ClientID %>").value = "";
                                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
                            }
                            else if (strtext == "Yes") {
                                show('spanEDD');
                                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").selectedIndex = "0";
                                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = true;
                                hide('divFamilyPlanningMethod');
                                hide('notfamilyplanning');
                                hide('miscarriage');
                                hide('Abortion');
                                document.getElementById("<%=txtdatemiscarriage.ClientID %>").value = "";
                                document.getElementById("<%=txtdateinducedabortion.ClientID %>").value = "";
                            }
                            else if (strtext == "No") {
                                hide('spanEDD');
                                hide('miscarriage');
                                hide('Abortion');
                                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
                            }
                            else if (strtext == "No - Induced Abortion (ab)") {
                                hide('spanEDD')
                                hide('miscarriage')
                                show('Abortion');
                                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
                            }
                            else if (strtext == "No - Miscarriage (mc)") {
                                show('miscarriage');
                                hide('spanEDD');
                                hide('Abortion');
                                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
                            }
                        }
                        function fnfamilyplanning() {
                            var e = document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;

                            if (strtext == "Select") {
                                hide('divFamilyPlanningMethod');
                                hide('notfamilyplanning');

                            }
                            else if (strtext == "Currently on Family Planning" || strtext == "Wants Family Planning") {
                                show('divFamilyPlanningMethod');
                                hide('notfamilyplanning');
                            }
                            else if (strtext == "Not on Family Planning") {
                                show('notfamilyplanning');
                                hide('divFamilyPlanningMethod');
                            }
                        }
                        function fnTBStatus() {
                            var e = document.getElementById("<%=ddlTBStatus.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;
                            if (strtext == "Select") {
                                hide('tbstartdate');
                                hide('tbtreatment');
                                document.getElementById("<%=txttbstartdate.ClientID %>").value = "";
                                document.getElementById("<%=txtTBtreatmentNumber.ClientID %>").value = "";

                            }
                            else if (strtext == "TB Rx") {
                                show('tbstartdate');
                                show('tbtreatment');
                            }
                            else {
                                hide('tbstartdate');
                                hide('tbtreatment');
                                document.getElementById("<%=txttbstartdate.ClientID %>").value = "";
                                document.getElementById("<%=txtTBtreatmentNumber.ClientID %>").value = "";
                            }
                        }
                        function fnSubsituations() {
                            var e = document.getElementById("<%=ddlsubsituationInterruption.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;
                            hide('autopopulate')
                            if (strtext == "Change regimen") {
                                show('arvTherapyChange');
                                hide('arvTherapyStop');

                            }
                            else if (strtext == "Stop treatment") {
                                show('arvTherapyStop');
                                hide('arvTherapyChange');
                            }
                            else if (strtext == "Continue current treatment") {
                                show('autopopulate');
                                hide('arvTherapyChange');
                                hide('arvTherapyStop');

                            }
                            else {
                                hide('arvTherapyChange');
                                hide('arvTherapyStop');
                            }
                        }
                        function fnRegimenChange() {
                            var e = document.getElementById("<%=ddlArvTherapyChangeCode.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;
                            if (strtext == "Other reason (specify)") {
                                show('otherarvTherapyChangeCode');

                            }
                            else {
                                hide('otherarvTherapyChangeCode');

                            }
                        }
                        function fnStopReason() {
                            var e = document.getElementById("<%=ddlArvTherapyStopCode.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;
                            if (strtext == "Other (specify)") {
                                show('otherarvTherapyStopCode');

                            }
                            else {
                                hide('otherarvTherapyStopCode');

                            }
                        }
                        function fnarvdrugother() {
                            var e = document.getElementById("<%=ddlwhypoorfair.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;
                            if (strtext == "Other (specify)") {
                                show('divReasonARVDrugsother');

                            }
                            else {
                                hide('divReasonARVDrugsother');

                            }
                        }
                        function fnARVDrug() {
                            var e = document.getElementById("<%=ddlarvdrugadhere.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;

                            if (strtext == "G=Good") {
                                document.getElementById("<%=ddlwhypoorfair.ClientID %>").disabled = true;
                            }
                            else {
                                document.getElementById("<%=ddlwhypoorfair.ClientID %>").disabled = false;
                            }
                        }
                        function fnBMI() {
                            var weight = document.getElementById("<%=txtPhysWeight.ClientID %>").value;
                            var height = document.getElementById("<%=txtPhysHeight.ClientID %>").value;

                            var bmi = weight / (height / 100 * height / 100);
                            if (weight != "" && height != "") {
                                document.getElementById("<%=txtBMI.ClientID %>").value = Math.round(bmi, 2);
                            }
                        }

                        function CalcualteBMI(txtBMI, txtWeight, txtHeight) {
                            var weight = document.getElementById(txtWeight).value;
                            var height = document.getElementById(txtHeight).value;
                            if (weight == "" || height == "") {
                                weight = 0; height = 0;
                            }
                            weight = parseFloat(weight);
                            height = parseFloat(height);
                            var BMI = weight / ((height / 100) * (height / 100));
                            BMI = BMI.toFixed(2);
                            document.getElementById(txtBMI).value = BMI;
                        }
                        function fnvisittype() {
                            var e = document.getElementById("<%=ddlvisittype.ClientID%>");
                            var strtext = e.options[e.selectedIndex].text;

                            if (strtext == "Select") {
                                document.getElementById("<%=txtTreatmentSupporterName.ClientID %>").disabled = false;
                                document.getElementById("<%=txtTreatmentSupporterContact.ClientID %>").disabled = false;
                                show('clinicalstatus');
                                show('labinvestigations');
                                show('referrals');
                                show('positiveprevention');
                            }
                            else if (strtext == "TS - Treatment Supporter") {
                                document.getElementById("<%=txtTreatmentSupporterName.ClientID %>").disabled = false;
                                document.getElementById("<%=txtTreatmentSupporterContact.ClientID %>").disabled = false;
                                hide('clinicalstatus');
                                hide('labinvestigations');
                                hide('referrals');
                                hide('positiveprevention');
                            }
                            else if (strtext == "SF - Self") {
                                document.getElementById("<%=txtTreatmentSupporterName.ClientID %>").disabled = true;
                                document.getElementById("<%=txtTreatmentSupporterContact.ClientID %>").disabled = true;
                                show('clinicalstatus');
                                show('labinvestigations');
                                show('referrals');
                                show('positiveprevention');
                            }
                        }
                    </script>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" align="center" width="50%">
                                        <div class="col-md-12">
                                            <label class="required left control-label pull-left" id="lblVisitDate" runat="server">
                                                *Visit Date:<small class="text-muted"> [DD-MMM-YYYY]</small></label>
                                        </div>
                                        <div class="col-md-8" style="padding-right: 0%">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtVisitDate" CssClass="form-control" Style="padding-left: 0px"
                                                    MaxLength="11" Columns="8" runat="server"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtVisitDate.ClientID%>');"
                                                        hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                            </div>
                                        </div>
                                        <%--<div class="col-md-2" style="padding-left: 0%">
                                            <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtVisitDate.ClientID%>');"
                                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                        </div>--%>
                                        <div class="col-md-4">
                                            <label>
                                                If Scheduled:</label><input id="chkifschedule" type="checkbox" runat="server" />
                                        </div>
                                    </td>
                                    <td class="border pad5 whitebg" align="center">
                                        <div class="col-md-12">
                                            <label class="center control-label pull-left">
                                                Visit Type:</label></div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlvisittype" CssClass="form-control" onchange="fnvisittype()"
                                                runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg">
                                        <div class="col-md-11">
                                            <label class="pull-left control-label">
                                                If drug pick only, Treatment Supporter Name:
                                            </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTreatmentSupporterName" CssClass="form-control" Columns="23"
                                                MaxLength="23" runat="server"></asp:TextBox></div>
                                    </td>
                                    <td class="border pad5 whitebg" align="center" valign="top">
                                        <div class="col-md-11">
                                            <label id="Label1" class="control-label pull-left" runat="server">
                                                Treatment Supporter Contact:
                                            </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTreatmentSupporterContact" CssClass="form-control" Columns="23"
                                                MaxLength="23" runat="server"></asp:TextBox></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" colspan="2">
                                        <div class="col-md-2" style="padding-right: 0;">
                                            <label class="control-label pull-left" style="vertical-align: middle">
                                                <strong>Duration (Months) Since </strong>
                                            </label>
                                        </div>
                                        <div class="col-md-3" style="padding-right: 0;">
                                            <div class="input-group">
                                                <span class="input-group-addon">ART Start</span>
                                                <asp:TextBox ID="txtARTStart" CssClass="margin10 form-control" MaxLength="3" ReadOnly="true"
                                                    runat="server"></asp:TextBox>
                                                <span class="input-group-addon">months</span>
                                            </div>
                                        </div>
                                        <div class="col-md-4" style="padding-right: 0;">
                                            <div class="input-group">
                                                <span class="input-group-addon">Starting Current Regimen</span>
                                                <asp:TextBox ID="txtStartingCurrentRegimen" CssClass="margin10 form-control" MaxLength="3"
                                                    ReadOnly="true" runat="server"></asp:TextBox>
                                                <span class="input-group-addon">months</span>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="border center formbg" id="clinicalstatus">
                        <h2 class="forms" align="left">
                            Clinical Status</h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" colspan="2">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="center" style="width: 15%; padding-right: 20px">
                                                    <label class="control-label pull-left">
                                                        Temperature <span class="smallerlabel">(C)</span>:</label>
                                                    <asp:TextBox ID="txtphysTemp" CssClass="form-control" runat="server" MaxLength="4"
                                                        Columns="4"></asp:TextBox>
                                                </td>
                                                <td align="center" style="width: 15%; padding-right: 20px">
                                                    <label id="lblWeight" class="pull-left control-label" runat="server">
                                                        Weight <span class="smallerlabel">(kg) :</span></label>
                                                    <asp:TextBox ID="txtPhysWeight" CssClass="form-control" runat="server" MaxLength="5"
                                                        Columns="4" AutoPostBack="True" OnTextChanged="txtPhysWeight_TextChanged"></asp:TextBox>
                                                </td>
                                                <td align="center" style="width: 15%; padding-right: 20px">
                                                    <label id="lblHeight" class=" control-label pull-left" runat="server">
                                                        Height<span class="smallerlabel">(cm)</span>:</label>
                                                    <asp:TextBox ID="txtPhysHeight" CssClass="form-control" runat="server" MaxLength="3"
                                                        Columns="4" AutoPostBack="True" OnTextChanged="txtPhysHeight_TextChanged"></asp:TextBox>
                                                </td>
                                                <td align="center" style="width: 15%; padding-right: 20px; white-space: nowrap">
                                                    <label id="lblBmi" class="control-label pull-left" runat="server">
                                                        BMI:</label>
                                                    <asp:TextBox ID="txtBMI" CssClass="form-control" runat="server" MaxLength="6" Columns="4"></asp:TextBox>
                                                </td>
                                                <td align="center" style="width: 15%; padding-right: 20px">
                                                    <label id="labelMuac" class="control-label pull-left" runat="server" for="textMuac">
                                                        MUAC(mm) <span class="smallerlabel">child</span>:</label>
                                                    <asp:TextBox ID="textMuac" CssClass="form-control" runat="server" MaxLength="6" Columns="6"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FteMUAC" runat="server" TargetControlID="textMuac"
                                                        FilterType="Numbers, Custom" ValidChars="." />
                                                    <asp:RangeValidator ID="rgMuac" runat="server" ControlToValidate="textMuac" Type="Double"
                                                        MinimumValue="0" MaximumValue="300" ErrorMessage="*" Display="Dynamic" Enabled="True" />
                                                </td>
                                                <td id="Td1" align="center" style="width: 24%;" runat="server">
                                                    <label class="pull-left cpntrol-label">
                                                        BP:</label><br />
                                                    <div class="col-md-3 pull-left">
                                                        <asp:TextBox ID="txtBPSystolic" CssClass="form-control" runat="server" MaxLength="3"
                                                            Columns="4"></asp:TextBox></div>
                                                    <div class="col-md-1">
                                                        <label class="control-label">
                                                            /</label></div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtBPDiastolic" CssClass="form-control" runat="server" MaxLength="3"
                                                            Columns="4"></asp:TextBox></div>
                                                    <div class="col-md-2">
                                                        <span class="smallerlabel">(mm/Hg)</span></div>
                                                </td>
                                            </tr>
                                            <tr id="divscore">
                                                <td align="center" style="width: 19%;">
                                                </td>
                                                <td align="left" style="white-space: nowrap; width: 19%; vertical-align: top">
                                                    <asp:Label ID="lblWALabel" runat="server" Text="Weight for age:" Font-Bold="True"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanelWA" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div style="white-space: nowrap">
                                                                <asp:Label ID="lblWA" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lblWAClassification" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;</div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtPhysHeight" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtPhysWeight" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td align="left" style="white-space: nowrap; width: 19%; vertical-align: top">
                                                    <asp:Label ID="lblWHLabel" runat="server" Text="Weight for height:" Font-Bold="True"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanelWH" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div style="white-space: nowrap;">
                                                                <asp:Label ID="lblWH" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lblWHClassification" runat="server" Font-Bold="True"></asp:Label></div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtPhysHeight" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtPhysWeight" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td align="left" style="white-space: nowrap; vertical-align: top">
                                                    <asp:Label ID="lblBMIzLabel" runat="server" Font-Bold="True" Text="BMI ZScore:"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanelBMIz" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div style="white-space: nowrap">
                                                                <asp:Label ID="lblBMIz" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="lblBMIzClassification" runat="server" Font-Bold="True"></asp:Label></div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtPhysHeight" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtPhysWeight" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg formcenter" id="tdPregnant" colspan="2" runat="server">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="width: 100%;">
                                                    <div class="col-md-2">
                                                        <label id="lblPregnant" class="control-label pull-left" runat="server">
                                                            Pregnancy:
                                                        </label>
                                                        <asp:DropDownList ID="ddlpregnancy" CssClass="form-control col-md-2" onchange="fnchange()"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-10" id="spanEDD" style="display: none">
                                                        <div class="row">
                                                            <br />
                                                            <div class="col-md-3">
                                                                <label class="pull-left">
                                                                    EDD:<small class="text-muted"> [DD-MMM-YYYY]</small>
                                                                </label>
                                                                <div class="input-group col-md-12">
                                                                    <input id="txtEDD" runat="server" class="form-control" maxlength="11" />
                                                                    <span class="input-group-addon">
                                                                        <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtEDD.ClientID%>');"
                                                                            hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                                        <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label class="pull-left">
                                                                    ANC No:
                                                                </label>
                                                                <asp:TextBox ID="txtANCNumber" CssClass="form-control" runat="server" MaxLength="4"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <br />
                                                                <label>
                                                                    Referred to PMTCT (ac):</label>
                                                                <asp:CheckBox ID="chkrefpmtct" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-10" id="miscarriage" style="display: none">
                                                        <br />
                                                        <div class="col-md-4">
                                                            <label class="pull-left">
                                                                Date of Miscarriage:<small class="text-muted"> [DD-MMM-YYYY]</small>
                                                            </label>
                                                            <div class="input-group col-md-12">
                                                                <input id="txtdatemiscarriage" runat="server" maxlength="11" class="form-control" />
                                                                <span class="input-group-addon">
                                                                    <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtdatemiscarriage.ClientID%>');"
                                                                        hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-10" id="Abortion" style="display: none">
                                                        <br />
                                                        <div class="col-md-4">
                                                            <label class="pull-left">
                                                                Date of Induced Abortion:<small class="text-muted"> [DD-MMM-YYYY]</small>
                                                            </label>
                                                            <div class="input-group col-md-12">
                                                                <input id="txtdateinducedabortion" runat="server" maxlength="11" class="form-control" />
                                                                <span class="input-group-addon">
                                                                    <img id="img1" onclick="w_displayDatePicker('<%=txtdateinducedabortion.ClientID%>');"
                                                                        height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                                                        border="0" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span> </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad10 whitebg" valign="top" align="left" colspan="2" id="tdFamilyPlanning"
                                        runat="server">
                                        <table width="100%" border="0">
                                            <tr align="center">
                                                <td>
                                                    <div class="col-md-2">
                                                        <label class="control-label pull-left" style="padding: 0">
                                                            Family Planning:</label>
                                                        <asp:DropDownList ID="ddlFamilyPanningStatus" CssClass="form-control col-md-4" onchange="fnfamilyplanning();"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-10">
                                                        <div class="divborder margin10 col-md-12" style="margin-top: 10; display: none; margin-bottom: 10"
                                                            id="divFamilyPlanningMethod">
                                                            <asp:Panel ID="PnlFamilyPlanningMethod" runat="server">
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-md-4" id="notfamilyplanning" style="display: none;">
                                                            <label class="pull-left control-label">
                                                                Reason Not on Family Planning:
                                                            </label>
                                                            <asp:DropDownList ID="ddlnotfamilyplanning" CssClass="form-control pull-left" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad10 whitebg" colspan="2" width="100%" valign="top" align="left">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <div class="col-md-2">
                                                        <label id="lblTBStatus" class="control-label pull-left" runat="server">
                                                            TB Status:</label>
                                                        <asp:DropDownList ID="ddlTBStatus" CssClass="form-control" onchange="fnTBStatus();"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <div id="tbstartdate" style="display: none" class="col-md-12">
                                                            <div class="col-md-7">
                                                                <label class="control-label pull-left">
                                                                    TB Rx Start Date:<small class="text-muted"> [DD-MMM-YYYY]</small>
                                                                </label>
                                                                <div class="input-group col-md-12">
                                                                    <input id="txttbstartdate" class="form-control" runat="server" maxlength="11" />
                                                                    <span class="input-group-addon">
                                                                        <img id="img2" onclick="w_displayDatePicker('<%=txttbstartdate.ClientID%>');" height="22"
                                                                            alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                                                        <span class="smallerlabel">(DD-MMM-YYYY)</span> </span>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5" style="padding-right: 0px; display: none" id="tbtreatment">
                                                                <label class="control-label pull-left">
                                                                    TB Rx #:
                                                                </label>
                                                                <input id="txtTBtreatmentNumber" class="form-control input-sm col-md-5" maxlength="8"
                                                                    size="8" name="txtTBtreatmentNumber" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pad5 whitebg" colspan="2" width="100%" style="height: 35px">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td class="border pad10 whitebg" align="left" width="100%" valign="top">
                                                    <div class="col-md-12">
                                                        <label class="control-label pull-left col-md-12">
                                                            Potential Side Effects:</label>
                                                        <div id="divPotentialSideEffect" class="divborder col-md-12" style="margin-top: 10;
                                                            margin-bottom: 10">
                                                            <asp:Panel ID="PnlPotentialSideEffect" runat="server">
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <label class="control-label pull-left col-md-12">
                                                            New OIs, Other Problems:</label>
                                                        <div class="divborder col-md-12" style="margin-top: 10; margin-bottom: 10">
                                                            <asp:Panel ID="PnlNewOIsProblemsOther" runat="server">
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label class="control-label pull-left" id="lblNutritionalProblems" style="visibility: hidden"
                                                            runat="server">
                                                            Nutritional Problems:</label>
                                                        <asp:DropDownList ID="ddlNutritionalProblems" class="form-control" Style="visibility: hidden"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad10 whitebg" align="center" width="35%">
                                        <label id="lblWHOStage" class=" control-label pull-left" runat="server">
                                            WHO Stage:</label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlWHOStage" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="border center formbg" id="labinvestigations">
                        <br />
                        <table cellspacing="2" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="width: 50%;">
                                    <h2 id="H2" class="forms text-info" align="left">
                                        Laboratory Investigations</h2>
                                </td>
                                <td style="width: 50%;">
                                    <label class="margin10">
                                        Use lab order test button to order labs and document results.
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg formcenter" align="center" colspan="2">
                                        <div id="divLaboratory" runat="server">
                                            <asp:Button ID="btnLabratory" CssClass="btn btn-info" Text="Laboratory" runat="server"
                                                OnClick="btnLabratory_Click" />
                                        </div>
                                        <div id="divLabOrderTest" runat="server">
                                            <asp:Button ID="btnOrderLabTest" CssClass="btn btn-info" Text="Order Lab Tests" runat="server"
                                                OnClick="btnOrderLabTest_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="border center formbg">
                        <br />
                        <table cellspacing="2" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="width: 50%;">
                                    <h2 id="H1" class="forms" align="left">
                                        Pharmacy</h2>
                                </td>
                                <td style="width: 50%;">
                                    <label class="margin10">
                                        Use pharmacy form for INH dispensing and other medications dispensed.
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="form" style="width: 100%;" align="left">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr align="left">
                                                <td style="width: 25%" align="left">
                                                    <div class="col-md-12">
                                                        <label class="control-label pull-left">
                                                            Adherence: CTX / Dapsone:</label>
                                                        <asp:DropDownList ID="ddlCotrimoxazoleAdhere" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td style="padding-left: 30px; width: 25%">
                                                    <label class="margin10" id="lblARVDrugsAdhere" runat="server">
                                                        ARV:
                                                    </label>
                                                    <label class="margin10">
                                                        Adherence:
                                                    </label>
                                                    <asp:DropDownList ID="ddlarvdrugadhere" CssClass="form-control" onchange="fnARVDrug();"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding-left: 30px; width: 25%">
                                                    <label class="control-label pull-left">
                                                        Why Poor/Fair:
                                                    </label>
                                                    <asp:DropDownList ID="ddlwhypoorfair" CssClass="form-control" onchange="fnarvdrugother();"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding-left: 30px">
                                                    <div id="divReasonARVDrugsother" style="display: none">
                                                        <label class="right15">
                                                            Other Reason:</label>
                                                        <asp:TextBox ID="txtReasonARVDrugsPoorFairOther" CssClass="form-control" runat="server"
                                                            MaxLength="10" Columns="10"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <%--   <tr align="left">--%>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <%-- <tr align="left">
                                                <td style="width: 100%; display: inline" align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>--%>
                                            <tr align="left">
                                                <%-- <td style="width: 30%; display: inline" align="left">--%>
                                                <td style="width: 100%; display: inline" align="left">
                                                    <div class="col-md-3">
                                                        <label class="required  control-label pull-left">
                                                            *ARV Substitutions/Interruptions:
                                                        </label>
                                                        <asp:DropDownList ID="ddlsubsituationInterruption" CssClass="form-control col-md-4"
                                                            onchange="fnSubsituations();" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="arvTherapyChange" style="display: none" class="col-md-8">
                                                        <div class="col-md-4">
                                                            <label class="required control-label pull-left">
                                                                *Change Regimen Reason:</label>
                                                            <asp:DropDownList ID="ddlArvTherapyChangeCode" CssClass="form-control" onchange="fnRegimenChange();"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="otherarvTherapyChangeCode" style="display: none" class="col-md-3">
                                                            <label class="required control-label pull-left" for="arvTherapyChangeCodeOtherName">
                                                                *Specify:</label>
                                                            <input id="txtarvTherapyChangeCodeOtherName" class="form-control" maxlength="20"
                                                                size="10" name="arvTherapyChangeCodeOtherName" runat="server" /></div>
                                                    </div>
                                                    <div id="arvTherapyStop" style="display: none" class="col-md-8">
                                                        <div class="col-md-4">
                                                            <label id="lblrARTdate" class="required control-label pull-left">
                                                                *Date ART Ended</label>
                                                            <div class="input-group col-md-12">
                                                                <input id="txtARTEndeddate" class="form-control" runat="server" maxlength="11" name="txtARTEndeddate" />
                                                                <span class="input-group-addon">
                                                                    <img id="img3" onclick="w_displayDatePicker('<%=txtARTEndeddate.ClientID%>');" height="22"
                                                                        alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span> </span>
                                                            </div>
                                                            <%-- <input id="txtARTEndeddate" runat="server" maxlength="11" size="10" name="txtARTEndeddate" />
                                                            <img id="imgdate" onclick="w_displayDatePicker('<%=txtARTEndeddate.ClientID%>');"
                                                                height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                                                border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>--%>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label class="required control-label pull-left">
                                                                *Stop Regimen Reason:</label>
                                                            <asp:DropDownList ID="ddlArvTherapyStopCode" onchange="fnStopReason();" runat="server"
                                                                class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="otherarvTherapyStopCode" style="display: none" class="col-md-4">
                                                            <label class="required control-label pull-left" for="arvTherapyStopCodeOtherName">
                                                                *Specify:</label>
                                                            <input id="txtarvTherapyStopCodeOtherName" maxlength="20" name="arvTherapyStopCodeOtherName"
                                                                runat="server" class="form-control" /></div>
                                                    </div>
                                                    <%--<div style="display: none">--%>
                                                        <div id="autopopulate" style="display: none" class="col-md-2">
                                                            <br />
                                                            <label style="padding-top: 10px" class="control-label pull-left">
                                                                Auto Populate Prescription?&nbsp;&nbsp;<asp:CheckBox ID="ckb_AutoPopPresc" runat="server" /></label>
                                                        </div>
                                                    <%--</div>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg center">
                                        <div id="divAdultPharmacy" runat="server">
                                            <asp:Button ID="btnpharmacy" CssClass="btn btn-info" Text="Prescribe Drugs" runat="server"
                                                OnClick="btnpharmacy_Click"></asp:Button>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="border center formbg" id="referrals">
                        <br />
                        <h2 id="H3" class="forms" align="left">
                            Referrals and Consultations</h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border  whitebg" valign="top" width="100%" align="left" colspan="2">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">
                                                Referred To:
                                            </label>
                                        </div>
                                        <div class="divborder margin20 col-md-12" style="margin-top: 10; margin-bottom: 10">
                                            <%--  <div>--%>
                                            <asp:Panel ID="PnlReferredTo" runat="server">
                                            </asp:Panel>
                                            <%--</div>--%>
                                        </div>
                                        <div class="col-md-2 ">
                                            <label id="Label17" class="control-label pull-left">
                                                # of Days Hospitalized:</label>
                                            <asp:TextBox ID="txtNumOfDaysHospitalized" CssClass="form-control" runat="server"
                                                MaxLength="4"></asp:TextBox>
                                            <br />
                                        </div>
                                    </td>
                                    <%-- <td class="border pad5 whitebg" align="center">--%>
                                    <%--<div class="col-md-12">
                                            <label id="Label17" class="margin15 pull-left">
                                                # of Days Hospitalized:</label>
                                        </div>
                                        <div class="col-md-2 pull-left">
                                            <asp:TextBox ID="txtNumOfDaysHospitalized" CssClass="form-control pull-left" runat="server"
                                                MaxLength="4" Columns="4"></asp:TextBox>
                                        </div>--%>
                                    <%--</td>--%>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" align="center" width="100%" colspan="2">
                                        <div class="col-md-3">
                                            <label class="control-label pull-left">
                                                Nutritional Support:</label>
                                            <asp:DropDownList ID="ddlNutritionalSupport" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3" id="tdInfantFeedingPractice" style="display: none" runat="server">
                                            <label class="control-label pull-left">
                                                Infant Feeding Practice:</label>
                                            <asp:DropDownList ID="ddlInfantFeedingPractice" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <%--<td id="tdInfantFeedingPractice" style="display: none" runat="server" class="border pad5 whitebg"
                                        align="center">
                                        <div class="col-md-6 border pad5 whitebg" id="tdInfantFeedingPractice" style="display: none" runat="server">
                                            <label class="control-label pull-left">
                                                Infant Feeding Practice:</label>
                                        
                                            <asp:DropDownList ID="ddlInfantFeedingPractice" CssClass="form-control"
                                                runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="border center formbg" id="positiveprevention">
                        <br />
                        <h2 id="H4" class="forms" align="left">
                            Positive Prevention
                        </h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad10 whitebg" align="left" valign="top" width="50%">
                                        <label class="margin20">
                                            At Risk Population:
                                        </label>
                                        <div class="margin20" style="margin-top: 10; margin-bottom: 10">
                                            <div>
                                                <asp:Panel ID="pnlriskpopulation" runat="server">
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </td>
                                    <td class="border pad10 whitebg" align="left" valign="top" width="50%">
                                        <label class="margin20">
                                            At Risk Population Services:
                                        </label>
                                        <div class=" margin20" style="margin-top: 10; margin-bottom: 10">
                                            <div>
                                                <asp:Panel ID="pnlriskpopulationservice" runat="server">
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad10 whitebg" valign="top" align="left" width="50%">
                                        <label class="margin20">
                                            Prevention with positives (PwP):
                                        </label>
                                        <div class=" margin20" style="margin-top: 10; margin-bottom: 10">
                                            <div>
                                                <asp:Panel ID="pnlprewithpositive" runat="server">
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </td>
                                    <td class="border pad10 whitebg" valign="top" width="50%">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="border center formbg" id="appointment">
                        <br />
                        <h2 id="H5" class="forms" align="left">
                            Appointment
                        </h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" align="center" width="50%">
                                        <div class="col-md-12">
                                            <label id="lblNextAppointment" class="pull-left" runat="server">
                                                Date of Next Appointment:<small class="text-muted"> [DD-MMM-YYYY]</small>
                                            </label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <input id="txtdatenextappointment" class="form-control" runat="server" maxlength="11"
                                                    size="11" />
                                                <span class="input-group-addon">
                                                    <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtdatenextappointment.ClientID%>');"
                                                        hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                            </div>
                                        </div>
                                    </td>
                                    <td class="border pad5 whitebg" align="center" width="50%">
                                        <div class="col-md-12">
                                            <label class=" pull-left control-label">
                                                Attending Clinician:</label></div>
                                        <div class="col-md-8 pull-left">
                                            <asp:DropDownList ID="ddlattendingclinician" CssClass="form-control pull-left" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form" colspan="2">
                                        <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                            Wrap="true">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pad5 center whitebg border" colspan="2">
                                        <br />
                                        <asp:Button ID="buttonICF" Text="Save & Open ICF Form" runat="server" OnClick="buttonICF_Click"
                                            Visible="False" Style="display: none" />
                                        <asp:Button ID="btnSave" Text="Save Follow-up" CssClass="btn btn-info" runat="server"
                                            OnClick="btnSave_Click" />
                                        <asp:Button ID="btnDataQualityCheck" CssClass="btn btn-warning" Text="Data Quality check"
                                            runat="server" OnClick="btnDataQualityCheck_Click" />
                                        <asp:Button ID="btnClose" Text="Close" CssClass="btn btn-danger" runat="server" OnClick="btnClose_Click" />
                                        <asp:Button ID="btnPrint" Text="Print" CssClass="btn btn-info" runat="server" OnClientClick="WindowPrint()" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!-- .panel-body-->
        </div>
        <!-- .panel-->
    </div>
    <!-- .row -->
</asp:Content>
