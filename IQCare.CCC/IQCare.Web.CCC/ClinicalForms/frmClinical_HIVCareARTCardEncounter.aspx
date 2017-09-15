<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.HIVCareARTCardEncounter"
    Title="Create HIV Care ART Card Encounter Form" Codebehind="frmClinical_HIVCareARTCardEncounter.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
   
    <br />
    <div style="padding-left: 8px; padding-right: 8px;">
        <script language="javascript" type="text/javascript">
            function WindowPrint() {
                window.print(); 
            }

            function WindowHistory() { 
                history.go(-1);
                return false;
            }
            function fnPageOpen(pageopen) {
                if (pageopen == "Pharmacy") {
                    window.open('../Pharmacy/frmPharmacyform.aspx?opento=ArtForm');
                }
                else if (pageopen == "Labratory") {
                    window.open('../Laboratory/frmLabOrder.aspx?opento=ArtForm');
                }
                else if (pageopen == "LabTest") {
                    window.open('../Laboratory/frmLabOrderTests.aspx?opento=ArtForm');
                }
            }
        </script>
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" align="center" width="50%">
                            <label class="required left" id="lblVisitDate" runat="server">
                                *Visit Date:</label>
                            <asp:TextBox ID="txtVisitDate" MaxLength="11" Columns="8" runat="server"></asp:TextBox>
                            <img height="22" alt="Date Helper" onclick="w_displayDatePicker('<%=txtVisitDate.ClientID%>');"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0">
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            <label>
                                If Scheduled:</label>
                            <input id="chkifschedule" type="checkbox" runat="server" />
                        </td>
                        <td class="border pad5 whitebg" align="center">
                            <label class="center">
                                Alternate Pick up Name:</label>
                            <asp:TextBox ID="txtTreatmentSupporterName" Columns="23" MaxLength="23" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" align="center">
                            <label class="center">
                                Alternate Pick up Contact:</label>
                            <asp:TextBox ID="txtTreatmentSupporterContact" Columns="23" MaxLength="23" runat="server"></asp:TextBox>
                        </td>
                        <td class="border pad5 whitebg" align="center" valign="top">
                            <label id="lblFollowUpDate" class="center" runat="server">
                                Follow up Date:</label>
                            <asp:TextBox ID="txtFollowUpDate" MaxLength="11" Columns="8" runat="server"></asp:TextBox>
                            <img height="22" alt="Date Helper" onclick="w_displayDatePicker('<%=txtFollowUpDate.ClientID%>');"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0">
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg formcenter" align="center" colspan="2">
                            <label class="center">
                                Duration Since:</label>
                            <span class="smalllabel">ART Start</span>
                            <asp:TextBox ID="txtARTStart" CssClass="margin10" MaxLength="3" Columns="2" ReadOnly="true"
                                runat="server"></asp:TextBox>
                            <span class="smalllabel">Months</span> <span class="smalllabel margin20">Starting Current
                                Regimen</span>
                            <asp:TextBox ID="txtStartingCurrentRegimen" CssClass="margin10" MaxLength="3" Columns="2"
                                ReadOnly="true" runat="server"></asp:TextBox>
                            <span class="smalllabel">Months</span>
                        </td>
                    </tr>
            </table>
        </div>
          <br/>
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                Clinical Status</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" colspan="2">
                            <table width="100%" border="0">
                                <td align="center" style="width: 30%;">
                                    <label id="lblWeight" runat="server">
                                        Weight:</label>
                                    <asp:TextBox ID="txtPhysWeight" runat="server" MaxLength="5" Columns="4"></asp:TextBox>
                                    <span class="smallerlabel">kg</span>
                                </td>
                                <td align="center" style="width: 30%;">
                                    <label id="lblHeight" runat="server">
                                        Height:</label>
                                    <asp:TextBox ID="txtPhysHeight" runat="server" MaxLength="3" Columns="4"></asp:TextBox>
                                    <span class="smallerlabel">cm</span>
                                </td>
                                <td align="center" style="width: 30%; display: none" id="tdOedema" runat="server">
                                    <label>
                                        Oedema:</label>
                                    <asp:RadioButton ID="rdoOedemaPlus" GroupName="Oedema" runat="server" />
                                    <label>
                                        +</label>
                                    <asp:RadioButton ID="rdoOedemaMinus" GroupName="Oedema" runat="server" />
                                    <label>
                                        -</label>
                                </td>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg formcenter" id="tdPregnant" colspan="2" runat="server">
                            <table width="100%" border="0">
                                <tr align="center">
                                    <td align="center" style="width: 100%; display: inline-table" runat="server">
                                        <label id="lblPregnant" runat="server">
                                            Pregnant:</label>
                                        <input id="rdoPregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD');hide('spanEDDNopregnant'); "
                                            type="radio" value="Y" name="pregnant" runat="server">
                                        <label>
                                            Yes</label>
                                        <input id="rdoPregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');show('spanEDDNopregnant');"
                                            type="radio" value="N" name="pregnant" runat="server">
                                        <label>
                                            No</label>
                                        <span id="spanEDD" style="display: none">
                                            <label id="lblEDD" class="margin20" for="EDD">
                                                EDD:</label>
                                            <input id="txtEDD" runat="server" maxlength="11" size="11">
                                            <img id="imgEDD" onclick="w_displayDatePicker('<%=txtEDD.ClientID%>');" height="22"
                                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0">
                                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            <label id="lblGestation" for="Gestation">
                                                Gestation:</label>
                                            <asp:TextBox ID="txtGestation" runat="server" MaxLength="3" Columns="2"></asp:TextBox>
                                            <span class="smalllabel">Weeks</span>
                                            <label id="lblPMTCTPregnant" for="PMTCTPregnant" class="margin20">
                                                PMTCT?</label>
                                            <input id="rdoPMTCT" onclick="toggle('spanpmctcancno');" type="checkbox" value="Y"
                                                name="rdoPMTCT" runat="server" />
                                            <span style="display: none" id="spanpmctcancno">
                                                <label id="lblpmctanc" for="pmctcanc" class="margin20">
                                                    PMTCT/ANC No:</label>
                                                <asp:TextBox ID="txtPMTCTANCNumber" runat="server" MaxLength="4" Columns="13"></asp:TextBox>
                                            </span></span><span id="spanEDDNopregnant" style="display: none">
                                                <label id="lblDelivery" class="right10" for="DeliveryPregnantNo">
                                                    Delivery Date:</label>
                                                <asp:TextBox ID="txtDeliveryDate" MaxLength="11" Columns="8" runat="server"></asp:TextBox>
                                                <img height="22" alt="Date Helper" onclick="w_displayDatePicker('<%=txtDeliveryDate.ClientID%>');"
                                                    hspace="5" src="../images/cal_icon.gif" width="22" border="0">
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                <label id="lblPostPartum" class="right10" for="PostPartum">
                                                    Post Partum:</label>
                                                <asp:CheckBox ID="cbPostPartum" runat="server" />
                                            </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad10 whitebg" valign="top" align="left" colspan="2" id="tdFamilyPlanning"
                            runat="server">
                            <label class="margin20">
                                Family Planning:</label>
                            <asp:DropDownList ID="ddlFamilyPanningStatus" runat="server">
                            </asp:DropDownList>
                            <div class="divborder checkbox" id="divFamilyPlanningMethod" style="display: none">
                                <%-- <asp:CheckBoxList ID="cblFamilyPlanningMethod"  RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList> --%>
                                <%--<table id="tbFamilyPlanningMethod" cellpadding="0" cellspacing="1" runat="server">
                                </table> --%>
                                <asp:Panel ID="PnlFamilyPlanningMethod" runat="server">
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" colspan="2" width="100%" style="height: 35px; display: none"
                            id="tdMUACAge" runat="server">
                            <table width="100%" border="0">
                                <tr>
                                    <td style="width: 50%" align="center">
                                        <label>
                                            MUAC:</label>
                                        <asp:TextBox ID="txtMUAC" runat="server" MaxLength="4" Columns="4"></asp:TextBox>
                                        <span class="smallerlabel">cm</span>
                                    </td>
                                    <td style="width: 50%" align="center">
                                        <label>
                                            Age:</label>
                                        <asp:TextBox ID="txtAge" runat="server" ReadOnly="true" MaxLength="4" Columns="3"></asp:TextBox>
                                        <span class="smallerlabel">Months</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" colspan="2" width="100%" style="height: 35px">
                            <table width="100%" border="0">
                                <tr align="center">
                                    <td style="width: 30%; display: inline" align="center">
                                        <label id="lblTBStatus" runat="server">
                                            TB Status:</label>
                                        <asp:DropDownList ID="ddlTBStatus" AutoPostBack="false" runat="server">
                                        </asp:DropDownList>
                                        <div id="divTBStatusTBRX" style="display: none">
                                            <label>
                                                TB Rx Start:</label>
                                            <input id="txtTBRxStart" maxlength="8" size="8" name="txtTBRxStart" runat="server"
                                                onfocus="javascript:vDateType='4'" onkeyup="DateFormat(this,this.value,event,false,'4')" /><span
                                                    class="smallerlabel">MMM-YYYY</span>
                                            <label>
                                                TB Rx Stop:</label>
                                            <input id="txtTBRxStop" maxlength="8" size="8" name="txtTBRxStop" runat="server"
                                                onfocus="javascript:vDateType='4'" onkeyup="DateFormat(this,this.value,event,false,'4')" /><span
                                                    class="smallerlabel">MMM-YYYY</span>
                                            <label>
                                                TB Reg #:</label>
                                            <input id="txtTBRegNumber" maxlength="8" size="8" name="txtTBRegNumber" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad10 whitebg" width="50%" valign="top" align="left">
                            <label class="margin20">
                                Potential Side Effects:</label>
                            <br />
                            <div id="divPotentialSideEffect" class="divborder margin20" style="margin-top: 10;
                                margin-bottom: 10">
                                <div style="font-weight: normal">
                                    <asp:Panel ID="PnlPotentialSideEffect" runat="server">
                                    </asp:Panel>
                                </div>
                            </div>
                        </td>
                        <td class="border pad10 whitebg" width="50%" valign="top" align="left">
                            <label class="margin20">
                                New OIs, Other Problems:</label>
                            <br />
                            <div class="divborder margin20" style="margin-top: 10; margin-bottom: 10">
                                <%--<table id="tblNewOIsProblemsOther" cellpadding="0" cellspacing="1" runat="server">
                                </table>--%>
                                <asp:Panel ID="PnlNewOIsProblemsOther" runat="server">
                                </asp:Panel>
                            </div>
                            <label class="margin20" id="lblNutritionalProblems" style="visibility: hidden" runat="server">
                                Nutritional Problems:</label>
                            <asp:DropDownList ID="ddlNutritionalProblems" Style="visibility: hidden" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad10 whitebg" width="50%" valign="top" align="left">
                            <label id="lblWABStage" runat="server">
                                WAB Stage:</label>
                            <asp:DropDownList ID="ddlWABStage" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="border pad10 whitebg" width="50%" valign="top" align="left">
                            <label id="lblWHOStage" runat="server">
                                WHO Stage:</label>
                            <asp:DropDownList ID="ddlWHOStage" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <br/>
            <h2 id="H1" class="forms" align="left">
                Pharmacy</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" align="left">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr align="left">
                                    <td style="width: 30%; display: inline" >
                                        <label class="margin10">
                                            CPT:
                                        </label>
                                        <label class="margin10">
                                            Adhere:
                                        </label>
                                        <asp:DropDownList ID="ddlCPTAdhere" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display: inline" align="left">
                                        <label class="margin10" id="lblARVDrugsAdhere" runat="server">
                                            ARV Drugs:
                                        </label>
                                        <label class="margin10">
                                            Adhere:
                                        </label>
                                        <asp:DropDownList ID="ddlARVDrugsAdhere" runat="server">
                                        </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <span id="divARVDrugsPoorFair" class="right15">
                                            <label class="right15">
                                                Why Poor/Fair
                                            </label>
                                            <asp:DropDownList ID="ddlReasonARVDrugsPoorFair" Enabled="false" runat="server">
                                            </asp:DropDownList>
                                        </span>
                                        <div id="divReasonARVDrugsother" style="display: none">
                                            <label class="right15">
                                                Other Reason:</label>
                                            <asp:TextBox ID="txtReasonARVDrugsPoorFairOther" runat="server" MaxLength="10" Columns="10"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; display: inline" align="left">
                                        <label id="lblarvplan" class="required margin10">
                                            *Substitution/Interruption:</label>
                                        <asp:DropDownList ID="lstclinPlanFU" runat="server">
                                        </asp:DropDownList>
                                        <div id="arvTherapyChange" style="display: none">
                                            <label class="required margin10">
                                                *Change Regimen Reason:</label>
                                            <asp:DropDownList ID="ddlArvTherapyChangeCode" runat="server">
                                            </asp:DropDownList>
                                            <div id="otherarvTherapyChangeCode" style="display: none">
                                                <label class="required margin10" for="arvTherapyChangeCodeOtherName">
                                                    *Specify:</label>
                                                <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName"
                                                    runat="server"/></div>
                                        </div>
                                        <div id="arvTherapyStop" style="display: none">
                                            <label id="lblrARTdate" class="required margin10">
                                                *Date ART Ended</label>
                                            <input id="txtARTEndeddate" runat="server" maxlength="11" size="10" name="txtARTEndeddate" />
                                            <img id="imgdate" onclick="w_displayDatePicker('<%=txtARTEndeddate.ClientID%>');"
                                                height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                                border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            <br />
                                            <br />
                                            <label class="required margin10">
                                                *Stop Regimen Reason:</label>
                                            <asp:DropDownList ID="ddlArvTherapyStopCode" runat="server">
                                            </asp:DropDownList>
                                            <div id="otherarvTherapyStopCode" style="display: none">
                                                <label class="required margin10" for="arvTherapyStopCodeOtherName">
                                                    *Specify:</label>
                                                <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName"
                                                    runat="server"/></div>
                                        </div>
                                    </td>
                                    <%--                                    <td align="center" style="width:20%;display:table-cell">
                                        <asp:RadioButton ID="rdoLost" GroupName="patientStopOrLost" runat="server" />                                        
                                        <label>Lost</label>
                                        <asp:RadioButton ID="rdoStop" GroupName="patientStopOrLost" runat="server" />                                        
                                        <label>Stop</label>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg center">
                             <div id="divAdultPharmacy" runat="server">
                                <asp:Button ID="btnpharmacy" Text="Prescribe Drugs" runat="server" OnClick="btnpharmacy_Click">
                                </asp:Button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <br/>
            <h2 id="H2" class="forms" align="left">
                Investigations</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg formcenter" align="center" colspan="2">
                            <div id="divLaboratory" runat="server">
                                <asp:Button ID="btnLabratory" Text="Laboratory" runat="server" OnClick="btnLabratory_Click" />
                            </div>
                            <div id="divLabOrderTest" runat="server">
                                <asp:Button ID="btnOrderLabTest" Text="LabOrderTest" runat="server" OnClick="btnOrderLabTest_Click" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="border center formbg">
            <br/>
            <h2 id="H3" class="forms" align="left">
                Referrals and Consultations</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad10 whitebg" valign="top" align="left" width="50%">
                            <label class="margin20">
                                Referred To:
                            </label>
                            <%-- <asp:DropDownList ID="ddlReferredTo"  runat="server">
                            </asp:DropDownList>--%>
                            <div class="divborder margin20" style="margin-top: 10; margin-bottom: 10">
                                <div>
                                    <%-- <table id="tblReferredTo" cellpadding="0" cellspacing="1" runat="server">
                                </table>--%>
                                    <asp:Panel ID="PnlReferredTo" runat="server">
                                    </asp:Panel>
                                </div>
                            </div>
                            <%-- <div  id="divotherReferredTo" style="display: none">
                            <label class="margin10" id="lblReferredToOther" >
                                Other (specify):</label> 
                            <asp:TextBox ID="txtReferredToOther" runat="server" MaxLength="11" Columns="11"></asp:TextBox>
                          </div>--%>
                        </td>
                        <td class="border pad5 whitebg" align="center">
                            <label id="Label17" class="margin15">
                                # of Days Hospitalized:</label>
                            <asp:TextBox ID="txtNumOfDaysHospitalized" runat="server" MaxLength="4" Columns="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" align="center" width="50%">
                            <label>
                                Nutritional Support:</label>
                            <asp:DropDownList ID="ddlNutritionalSupport" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td id="tdInfantFeedingPractice" style="display: none" runat="server" class="border pad5 whitebg"
                            align="center">
                            <label>
                                Infant Feeding Practice:</label>
                            <asp:DropDownList ID="ddlInfantFeedingPractice" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center" colspan="2">
                            <label>
                                Attending Clinician:</label>
                            <asp:DropDownList ID="ddlAttendingClinician" runat="server">
                            </asp:DropDownList>
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
                            <br/>
                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" />
                            <asp:Button ID="btnDataQualityCheck" Text="Data quality check" OnClick="btnDataQualityCheck_Click"
                                runat="server" />
                            <asp:Button ID="btnClose" Text="Close" OnClick="btnClose_Click" runat="server" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
