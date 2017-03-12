<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master"
    EnableEventValidation="false"  AutoEventWireup="True" Inherits="IQCare.Web.Clinical.PriorArtHivCare" Codebehind="frm_PriorArt_HivCare.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    
<br />
    <div style="padding-left:8px;padding-right:8px;">
  
    <script language="Javascript" type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
      
    </script>

    <script language="javascript" type="text/javascript">

        function WindowPrint() {
           
            window.print();
        }


        function GetControl() {
            document.forms[0].submit();
        }

        function FocusValue() {
            window.scrollTo(0, 0);

            document.getElementById('<%=rdoDisclosureYes.ClientID%>').focus();
            //            alert('d');
            //            window.scrollTo(0, 0);
        }

        function ShowValue() {
            document.getElementById('Img1').disabled = true;
            document.getElementById('Img2').disabled = true;
            document.getElementById('Img6').disabled = true;
        }
        function CalEnbleDisble(a, b, c) {
            if (a == 0) {
                document.getElementById('Img1').disabled = true;
            }
            if (b == 0) {
                document.getElementById('Img2').disabled = true;
            }
            if (c == 0) {
                document.getElementById('Img6').disabled = true;
            }
        } 
    
    </script>

    <script language="javascript" type="text/javascript">
        function EnableDis(a) {
            var rdoVal = a;
            var ChkPEP = document.getElementById('<%=chkPEP.ClientID%>');
            var ChkPMTCT = document.getElementById('<%=chkPMTCTOnly.ClientID%>');
            var ChkEarlier = document.getElementById('<%=chkEarlierArvNotTransfer.ClientID%>');
            var PepWhere = document.getElementById('<%=txtPEPWhere.ClientID%>');
            var PepStartDt = document.getElementById('<%=txtPEPStartDate.ClientID%>');
            var Regimen = document.getElementById('<%=btnRegimen.ClientID%>');
            var PepARV = document.getElementById('<%=txtPEPARVs.ClientID%>');
            var PmtctWhere = document.getElementById('<%=txtPMTCTWhere.ClientID%>');
            var PmtctStartDt = document.getElementById('<%=txtPMTCTStartDate.ClientID%>');
            var Regimen1 = document.getElementById('<%=btnRegimen1.ClientID%>');
            var PmtctPepARV = document.getElementById('<%=txtPMTCTARVs.ClientID%>');
            var EarlierWhere = document.getElementById('<%=txtEarlierArvWhere.ClientID%>');
            var EarlierStartDt = document.getElementById('<%=txtEarlierArvStartDate.ClientID%>');
            var Regimen2 = document.getElementById('<%=btnRegimen2.ClientID%>');
            var EarlierARV = document.getElementById('<%=txtEarlierArvNotTransferArv.ClientID%>');
            if (rdoVal.value == "1") {
                ChkPEP.disabled = false;
                ChkPMTCT.disabled = false;
                ChkEarlier.disabled = false;
                PepWhere.value = '';
                PepARV.value = '';
                PepStartDt.value = '';
                PmtctWhere.value = '';
                PmtctStartDt.value = '';
                PmtctPepARV.value = '';
                EarlierWhere.value = '';
                EarlierStartDt.value = '';
                EarlierARV.value = '';
                PepStartDt.disabled = true;
                PepWhere.disabled = true;
                Regimen.disabled = true;
                PepARV.disabled = true;
                PmtctWhere.disabled = true;
                PmtctStartDt.disabled = true;
                EarlierStartDt.disabled = true;
                Regimen1.disabled = true;
                PmtctPepARV.disabled = true;
                EarlierWhere.disabled = true;
                Regimen2.disabled = true;
                EarlierARV.disabled = true;
                document.getElementById('Img1').disabled = true;
                document.getElementById('Img2').disabled = true;
                document.getElementById('Img6').disabled = true;
            }
            else {

                ChkPEP.checked = false;
                ChkPMTCT.checked = false;
                ChkEarlier.checked = false;
                document.getElementById('Img1').disabled = true;
                document.getElementById('Img2').disabled = true;
                document.getElementById('Img6').disabled = true;
                ChkPEP.disabled = true;
                ChkPMTCT.disabled = true;
                ChkEarlier.disabled = true;
                PepWhere.value = '';
                PepARV.value = '';
                PepStartDt.value = '';
                PmtctWhere.value = '';
                PmtctStartDt.value = '';
                PmtctPepARV.value = '';
                EarlierWhere.value = '';
                EarlierARV.value = '';
                EarlierStartDt.value = '';
                PepStartDt.disabled = true;
                PepWhere.disabled = true;
                Regimen.disabled = true;
                PepARV.disabled = true;
                PmtctWhere.disabled = true;
                PmtctStartDt.disabled = true;
                EarlierStartDt.disabled = true;
                Regimen1.disabled = true;
                PmtctPepARV.disabled = true;
                EarlierWhere.disabled = true;
                Regimen2.disabled = true;
                EarlierARV.disabled = true;
            }
        }
        function PriorArvPEP(id) {
            var PepWhere = document.getElementById('<%=txtPEPWhere.ClientID%>');
            var Regimen = document.getElementById('<%=btnRegimen.ClientID%>');
            var PepARV = document.getElementById('<%=txtPEPARVs.ClientID%>');
            var PepStartDt = document.getElementById('<%=txtPEPStartDate.ClientID%>');

            var pepChk = id;
            if (pepChk.checked == true) {
                PepStartDt.disabled = false;
                PepWhere.disabled = false;
                Regimen.disabled = false;
                PepARV.disabled = false;
                document.getElementById('Img1').disabled = false; 
            }
            else {
                PepStartDt.value = '';
                PepWhere.value = '';
                PepARV.value = '';
                PepStartDt.value = '';
                PepWhere.disabled = true;
                Regimen.disabled = true;
                PepARV.disabled = true;
                PepStartDt.disabled = true;
                document.getElementById('Img1').disabled = true;
            }
        }
        function PriorArvPMTCTOnly(id) {
            var PmtctWhere = document.getElementById('<%=txtPMTCTWhere.ClientID%>');
            var Regimen1 = document.getElementById('<%=btnRegimen1.ClientID%>');
            var PmtctPepARV = document.getElementById('<%=txtPMTCTARVs.ClientID%>');
            var PmtctStartDT = document.getElementById('<%=txtPMTCTStartDate.ClientID%>');
            var PmtctChk = id;
            if (PmtctChk.checked == true) {
                PmtctWhere.disabled = false;
                Regimen1.disabled = false;
                PmtctPepARV.disabled = false;
                PmtctStartDT.disabled = false;
                document.getElementById('Img2').disabled = false;
            }
            else {
                PmtctWhere.value = '';
                PmtctPepARV.value = '';
                PmtctStartDT.value = '';
                PmtctWhere.disabled = true;
                Regimen1.disabled = true;
                PmtctPepARV.disabled = true;
                PmtctStartDT.disabled = true;
                document.getElementById('Img2').disabled = true;
            }
        }
        function PriorArvEarlierArv(id) {
            var EarlierWhere = document.getElementById('<%=txtEarlierArvWhere.ClientID%>');
            var Regimen2 = document.getElementById('<%=btnRegimen2.ClientID%>');
            var EarlierARV = document.getElementById('<%=txtEarlierArvNotTransferArv.ClientID%>');
            var EarlierStartDT = document.getElementById('<%=txtEarlierArvStartDate.ClientID%>');
            var EarlierChk = id;
            if (EarlierChk.checked == true) {
                EarlierWhere.disabled = false;
                Regimen2.disabled = false;
                EarlierARV.disabled = false;
                EarlierStartDT.disabled = false;
                document.getElementById('Img6').disabled = false;
            }
            else {

                EarlierWhere.value = '';
                EarlierARV.value = '';
                EarlierStartDT.value = '';
                EarlierWhere.disabled = true;
                Regimen2.disabled = true;
                EarlierARV.disabled = true;
                EarlierStartDT.disabled = true;
                document.getElementById('Img6').disabled = true;
            }
        }
    </script>

    <div class="border center formbg">
     <br />
        <h2 class="forms" align="left">
            Prior ART</h2>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr align="left">
                <td align="left">
                    <label>
                        Prior ART:</label>
                    <input id="rdoDisclosureYes" onmouseup="up(this);" onfocus="up(this);" type="radio" value="1" name="disclosure"
                        onclick=" EnableDis(this)" runat="server" />&nbsp;
                    <label>
                        Yes</label>
                    <input id="rdoDisclosureNo" onmouseup="up(this);" onfocus="up(this);" type="radio" value="0" name="disclosure"
                        onclick="EnableDis(this)" runat="server" />
                    <label>
                        No</label>
                </td>
            </tr>
        </table>
        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="divwhoto">
            <tbody>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" align="left" nowrap="nowrap">
                                    <label class="">
                                        PEP:</label>
                                    <input type="checkbox" onclick="PriorArvPEP(this);" id="chkPEP" runat="server" />
                                </td>
                                <td style="width: 35%" align="center" nowrap="nowrap">
                                    <label class="">
                                        Start Date:</label>
                                    <input id="txtPEPStartDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled" />
                                    <img id="Img1" onclick="w_displayDatePicker('<%=txtPEPStartDate.ClientID%>');" height="22"
                                        alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                        name="appDateimg" />
                                    <span class="smallerlabel" id="Span1">(DD-MMM-YYYY)</span>
                                </td>
                                <td style="width: 20%" align="center" nowrap="nowrap">
                                    <label class="">
                                        Where:</label>
                                    <asp:TextBox ID="txtPEPWhere" Enabled="false" runat="server" MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="width: 20%" align="center" nowrap="nowrap">
                                    <label class="">
                                        ARVs:
                                    </label>
                                    <input id="txtPEPARVs" size="16" name="currentART" readonly="readonly" runat="server"
                                        disabled="disabled" />
                                    <asp:Button ID="btnRegimen" Enabled="false" runat="server" Text="..." OnClick="btnRegimen_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" align="left" nowrap="nowrap">
                                    <label class="">
                                        PMTCT Only:</label>
                                    <input type="checkbox" id="chkPMTCTOnly" onclick="PriorArvPMTCTOnly(this);" runat="server" />
                                </td>
                                <td style="width: 35%" align="center" nowrap="nowrap">
                                    <label class="">
                                        Start Date:</label>
                                    <input id="txtPMTCTStartDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled" />
                                    <img id="Img2" onclick="w_displayDatePicker('<%=txtPMTCTStartDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                        border="0" name="appDateimg" />
                                    <span class="smallerlabel" id="Span2">(DD-MMM-YYYY)</span>
                                </td>
                                <td style="width: 20%" align="center" nowrap="nowrap">
                                    <label class="">
                                        Where:</label>
                                    <asp:TextBox ID="txtPMTCTWhere" Enabled="false" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 20%" align="center" nowrap="nowrap">
                                    <label class="">
                                        ARVs:
                                    </label>
                                    <input id="txtPMTCTARVs" size="15" name="prevARVRegimen1Name" readonly="readonly"
                                        disabled="disabled" runat="server" />
                                    <asp:Button ID="btnRegimen1" Enabled="false" runat="server" Text="..." OnClick="btnRegimen1_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" align="left" nowrap="nowrap">
                                    <label class="">
                                        Earlier ARV Not Transfer:</label>
                                    <input type="checkbox" id="chkEarlierArvNotTransfer" onclick="PriorArvEarlierArv(this);"
                                        runat="server" />
                                </td>
                                <td style="width: 35%" align="center" nowrap="nowrap">
                                    <label class="">
                                        Start Date:</label>
                                    <input id="txtEarlierArvStartDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled"/>
                                    <img id="Img6" onclick="w_displayDatePicker('<%=txtEarlierArvStartDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                        border="0" name="appDateimg" />
                                    <span class="smallerlabel" id="Span6">(DD-MMM-YYYY)</span>
                                </td>
                                <td style="width: 20%" align="center" nowrap="nowrap">
                                    <label class="">
                                        Where:</label>
                                    <asp:TextBox ID="txtEarlierArvWhere" Enabled="false" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 20%" align="center" nowrap="nowrap">
                                    <label class="">
                                        ARVs:</label>
                                    <input id="txtEarlierArvNotTransferArv" size="15" name="prevARVRegimen2Name" readonly="readonly"
                                        disabled="disabled" runat="server" />
                                    <asp:Button ID="btnRegimen2" runat="server" Enabled="false" Text="..." OnClick="btnRegimen2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <div class="border center formbg">
        <br/>
        <h2 class="forms" align="left">
            HIV Care</h2>
        <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="form" nowrap="nowrap">
                        <table width="100%">
                            <tr>
                                <td style="width: 42%" align="right" nowrap="nowrap">
                                    <label class="" id="lblHIVConfirmHIVPosDate"  runat="server">
                                        Date Confirmed HIV Positive:</label>
                                    <input id="txtHIVConfirmHIVPosDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server"/>
                                    <img id="Img3" onclick="w_displayDatePicker('<%=txtHIVConfirmHIVPosDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                        border="0" name="appDateimg"/>
                                    <span class="smallerlabel" id="Span3">(DD-MMM-YYYY)</span>
                                </td>
                                <td style="width: 4%" align="right">
                                </td>
                                <td style="width: 25%" align="left" nowrap="nowrap">
                                    <label class="">
                                        Test Type:</label>
                                    <asp:DropDownList ID="ddlHivTestType" runat="server" Width="110px" Enabled="false"
                                        Style="z-index: 2">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 30%" align="left" nowrap="nowrap">
                                    <label class="">
                                        Where:</label>
                                    <asp:TextBox ID="txtHIVCareWhere" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="width: 42%" align="right" nowrap="nowrap">
                                    <label class="">
                                        Date Eligible for ART:</label>
                                    <input id="txtHIVEligibleDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server"/>
                                    <img id="Img4" onclick="w_displayDatePicker('<%=txtHIVEligibleDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                        border="0" name="appDateimg"/>
                                    <span class="smallerlabel" id="Span4">(DD-MMM-YYYY)</span>
                                </td>
                                <td style="width: 4%" align="right">
                                </td>
                                <td style="width: 25%" align="left" nowrap="nowrap">
                                    <label class="">
                                        Clinical Stage:</label>
                                    <asp:DropDownList ID="ddlHIVClincialWHOStage" runat="server" Width="110px" Style="z-index: 2">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%" align="center" nowrap="nowrap">
                                    <label class="">
                                        CD4:</label>
                                    <asp:TextBox ID="txtHIVPrevARVsCD4" onkeypress="return isNumberKey(event)" MaxLength="4"
                                        Width="30" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 15%" align="center" nowrap="nowrap">
                                    <label class="">
                                        CD4 %:
                                    </label>
                                    <asp:TextBox ID="txtHIVPrevARVsCD4Percent" MaxLength="4" onkeypress="return isNumberKey(event)"
                                        Width="30" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="width: 42%"; align="right" nowrap="nowrap">
                                    <label class="">
                                        Date HIV Enrolled:</label>
                                        <input id="txtenrollmentDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'" readonly="readonly"
                                        maxlength="11" size="11" name="pharmReportedbyDate" runat="server" />
                                    <img id="Img7"  
                                        height="22" runat="server" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                        border="0" name="appDateimg" visible="true" style="visibility:hidden;" /><span class="smallerlabel" id="Span7" style="visibility:hidden;">(DD-MMM-YYYY)</span>
                                  
                                </td>
                                <td style="width: 4%" align="right">
                                </td>
                                <td style="width: 40%" align="left" nowrap="nowrap">
                                    <label class="">
                                        HIV care Transfer in From:
                                    </label>
                                     <asp:DropDownList ID="ddlarttransferinfrom" runat="Server" Font-Size="10px">
                                    </asp:DropDownList> 
                                </td>
                                <td style="width: 15%" align="left">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="width: 42%" align="right" nowrap="nowrap">
                                    <label class="">
                                        Eligible and Ready:</label>
                                    <input id="txtHIVReadyDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server"/>
                                    <img id="Img5" onclick="w_displayDatePicker('<%=txtHIVReadyDate.ClientID%>');" height="22"
                                        alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                        name="appDateimg"/>
                                    <span class="smallerlabel" id="Span5">(DD-MMM-YYYY)</span>
                                </td>
                                <td style="width: 4%" align="right">
                                </td>
                                <td style="width: 25%" align="left" nowrap="nowrap">
                                    <label class="">
                                        Presumptive HIV diagnosis:
                                    </label>
                                    <input type="checkbox" id="chkHIVPresumptiveDiagnosis" runat="server" />
                                </td>
                                <td style="width: 30%" align="left">
                                    <label class="">
                                        PCR-infant:</label>
                                    <input type="checkbox" id="chkHIVPcrInfant" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <div class="border center formbg">
        <br/>
        <h2 class="forms" align="left">
            Drug Allergies and Relevant Medical Conditions
        </h2>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%" nowrap="nowrap">
                                    <label class="required">
                                        *Drug Allergy:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDrugAllergies" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%" nowrap="nowrap">
                                    <label class="required" for="pharmOrderedbyDate">
                                        *Type of Reaction:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTypeOfReaction" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="tr3" runat="server">
                    <td class="form" nowrap="nowrap">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%" >
                                    <label id="lbldispensedby" runat="server">
                                        Date of Allergy:</label>
                                </td>
                                <td nowrap="nowrap" align="left">
                                    <input id="txtDateAllergy" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="pharmReportedbyDate"
                                        runat="server" />
                                    <img id="appDateimg2" onclick="w_displayDatePicker('<%=txtDateAllergy.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                        border="0" name="appDateimg" /><span class="smallerlabel" id="appDatespan2">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="form" nowrap="nowrap">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%" nowrap="nowrap">
                                    <label id="Label3" for="pharmReportedbyDate" runat="server">
                                        Relevant Medical Conditions:</label>
                                </td>
                                <td nowrap="nowrap" align="left">
                                    <asp:TextBox ID="txtRelevantMedicalCondition" MaxLength="150" TextMode="MultiLine" CssClass="textarea"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td class="pad5 center" colspan="2" nowrap="nowrap">
                        <asp:Button ID="btnAddAllergy" runat="server" Text="Add Allergy" OnClick="btnAddAllergy_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="pad5 formbg border" colspan="2" nowrap="nowrap">
                        <div class="grid" id="divDrugAllergyMedicalAlr" style="width:100%;">
                            <div class="rounded">
                              <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center>
                                                       </center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                 <div class="mid-outer">
                                  <div class="mid-inner">
                                   <div class="mid" style="height: 100px; overflow: auto">
                                <div id="div-gridview" class="gridview whitebg" style="height: 100">
                                    <asp:GridView Height="50px" ID="gvDrugAllergiesMedicalCondition" runat="server" AutoGenerateColumns="False"
                                        Width="100%" AllowSorting="True" BorderWidth="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                        CellPadding="0" CellSpacing="0" OnSorting="gvDrugAllergiesMedicalCondition_Sorting"
                                        HeaderStyle-HorizontalAlign="Left" RowStyle-CssClass="gridrow"
                                        OnRowDataBound="gvDrugAllergiesMedicalCondition_RowDataBound" OnRowDeleting="gvDrugAllergiesMedicalCondition_RowDeleting"
                                        OnSelectedIndexChanging="gvDrugAllergiesMedicalCondition_SelectedIndexChanging">
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
                    <td colspan="2" class="form" > 
                        <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%" Wrap="true">
                        </asp:Panel>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <div class="border center formbg">
        <br />
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr align="center">
                    <td class="form" nowrap="nowrap">
                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                        <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click" />
                        <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" />
                        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                            OnClick="btnPrint_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
 </div>
</asp:Content>
