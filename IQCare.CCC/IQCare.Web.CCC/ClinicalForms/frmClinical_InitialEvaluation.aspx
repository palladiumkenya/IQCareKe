<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.InitialEvaluation"
    MaintainScrollPositionOnPostback="true" Title="Untitled Page" Codebehind="frmClinical_InitialEvaluation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
 
    <div class="center" style="padding: 8px;">
        <script language="javascript" type="text/javascript">
            function addDays() {
                txtdate = document.getElementById('<%= txtvisitDate.ClientID%>').value
                var yr1 = txtdate.substr(7, 4);
                var mm1 = txtdate.substr(3, 3);
                var dd1 = txtdate.substr(0, 2);
                var nmm1;
                switch (mm1.toLowerCase()) {
                    case "jan": nmm1 = "01";
                        break;
                    case "feb": nmm1 = "02";
                        break;
                    case "mar": nmm1 = "03";
                        break;
                    case "apr": nmm1 = "04";
                        break;
                    case "may": nmm1 = "05";
                        break;
                    case "jun": nmm1 = "06";
                        break;
                    case "jul": nmm1 = "07";
                        break;
                    case "aug": nmm1 = "08";
                        break;
                    case "sep": nmm1 = "09";
                        break;
                    case "oct": nmm1 = "10";
                        break;
                    case "nov": nmm1 = "11";
                        break;
                    case "dec": nmm1 = "12";
                        break;
                }
                dt1 = nmm1 + "/" + dd1 + "/" + yr1;
                dateParts = dt1.split('/');
                newDays = document.getElementById('<%= lstappPeriod.ClientID %>').value;

                if (newDays == 0) {
                    document.getElementById('<%=txtappDate.ClientID%>').value = "";
                    document.getElementById('<%=ddappReason.ClientID%>').value = 0;
                    document.getElementById('<%=ddinterviewer.ClientID%>').value = 0;
                }

                else {
                    year = Number(dateParts[2]);
                    month = Number(dateParts[0]) - 1;
                    day = Number(dateParts[1]) + parseInt(newDays);
                    newDate = new Date(year, month, day);
                    year = newDate.getFullYear();
                    month = newDate.getMonth() + 1;
                    month = (month < 10) ? '0' + month : month;
                    day = newDate.getDate();
                    day = (day < 10) ? '0' + day : day;

                    formattedDate = month + '/' + day + '/' + year;

                    var yr2 = formattedDate.substr(6, 4);

                    var mm2 = formattedDate.substr(0, 2);
                    var dd2 = formattedDate.substr(3, 2);
                    var nmm2;
                    switch (mm2) {
                        case "01": nmm2 = "Jan";
                            break;
                        case "02": nmm2 = "Feb";
                            break;
                        case "03": nmm2 = "Mar";
                            break;
                        case "04": nmm2 = "Apr";
                            break;
                        case "05": nmm2 = "May";
                            break;
                        case "06": nmm2 = "Jun";
                            break;
                        case "07": nmm2 = "Jul";
                            break;
                        case "08": nmm2 = "Aug";
                            break;
                        case "09": nmm2 = "Sep";
                            break;
                        case "10": nmm2 = "Oct";
                            break;
                        case "11": nmm2 = "Nov";
                            break;
                        case "12": nmm2 = "Dec";
                            break;
                    }
                    dt2 = dd2 + "-" + nmm2 + "-" + yr2;
                    document.getElementById('<%=txtappDate.ClientID%>').value = dt2;
                    if (txtdate == "") {
                        document.getElementById('<%=txtappDate.ClientID%>').value = "";
                    }
                }
                return;
            }
            function WindowPrint() {
                document.getElementById('divwhoto').className = '';
                document.getElementById('divPresenting').className = '';
                document.getElementById('divdiseaseHistory').className = '';
                //document.getElementById('divhivassociated').className='';
                window.print();
                document.getElementById('divwhoto').className = 'checkbox';
                document.getElementById('divPresenting').className = 'checkbox';
                document.getElementById('divdiseaseHistory').className = 'divborder';
                //document.getElementById('divhivassociated').className='checkboxLeft';
            }
            function GetControl() {
                document.forms[0].submit();
            }

            function SenderPregnantLMP() {
                var id = document.getElementById('<%=txtvisitDate.ClientID%>').value;
                if (id.length <= 0) {
                    return true;
                }
                else {
                    CallPregnantLMPServer(id);
                    return true;
                }
            }


            function RecievePregnantData(args, context) {
                var temp = new Array(); temp = args.split('ZZZ');
                if (window.ActiveXObject) {
                    var objPreg = new ActiveXObject("MSXML2.DOMDocument");
                    objPreg.loadXML(temp[1]);
                    var dsRootPreg = objPreg.documentElement;
                    if (dsRootPreg.text != '') {
                        if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                            document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                            show('rdopregnantyesno');
                            hide('spdelivery');
                        }
                        else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 0 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                            show('rdopregnantyesno');
                            hide('spdelivery');
                        }
                        else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 1) {
                            show('rdopregnantyesno');
                            hide('spdelivery');
                            document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                            document.getElementById('<%=txtLMPdate.ClientID%>').value = "";
                        }
                        else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 9 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                            show('rdopregnantyesno');
                            hide('spdelivery');
                        }
                        else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 9) {
                            show('spdelivery');
                            hide('rdopregnantyesno');
                            if (dsRootPreg.childNodes(0).childNodes(3).firstChild.text != "") {
                                formattedDate = dsRootPreg.childNodes(0).childNodes(3).firstChild.text;
                                var yr = formattedDate.substr(7, 4);
                                var mm = formattedDate.substr(3, 3);
                                var dd = formattedDate.substr(0, 2);
                                formattedDate = dd + "-" + mm + "-" + yr;
                                document.getElementById('<%=txtEDDDate.ClientID%>').value = formattedDate;
                            }
                        }
                        else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                            show('spdelivery');
                            hide('rdopregnantyesno');
                            if (dsRootPreg.childNodes(0).childNodes(3).firstChild.text != "") {
                                formattedDate = dsRootPreg.childNodes(0).childNodes(3).firstChild.text;
                                var yr = formattedDate.substr(7, 4);
                                var mm = formattedDate.substr(3, 3);
                                var dd = formattedDate.substr(0, 2);
                                formattedDate = dd + "-" + mm + "-" + yr;
                                document.getElementById('<%=txtEDDDate.ClientID%>').value = formattedDate;
                            }

                        }
                    }
                }
            }


            function SenderARTStatus(pID) {
                CallServer(pID, "This is context from client");
                return true;

            }


            function CalcualteBMI(txtBMI, txtWeight, txtHeight) {
                var weight = document.getElementById(txtWeight).value;
                var height = document.getElementById(txtHeight).value;
                if (weight == "" || height == "") {
                    weight = 0;
                    height = 0;
                }

                weight = parseFloat(weight);
                height = parseFloat(height);

                var BMI = weight / ((height / 100) * (height / 100));
                BMI = BMI.toFixed(2);
                document.getElementById(txtBMI).value = BMI;
            }


            function ReceiveServerData(args, context) {
                if (window.ActiveXObject) {
                    var obj = new ActiveXObject("MSXML2.DOMDocument");
                    obj.loadXML(args);
                    var dsRoot = obj.documentElement;
                    if ((dsRoot.childNodes(0).childNodes(0).firstChild.text == 1) && (document.getElementById('<%=rdoprevARVExposureNone.ClientID%>').checked) == true) {
                        alert("None Cannot Selected as this patient is already in ART");
                    }
                    else { return true }
                }
            }
    
        </script>
      
        <asp:HiddenField ID="HdnPregDeli" runat="server" />
        
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <%--<tr>
                        <td class="form" align="center" colspan="2">
                            <label class="patientInfo">
                                Patient Name:</label>
                            <asp:Label ID="lblpatientname" runat="server" Text="Mary Longlastname"></asp:Label>
                            <label class="patientInfo">
                                *Patient ID:</label>
                            <asp:Label ID="lblpatientenrolment" runat="server" Text="444545"></asp:Label>
                            <label class="patientInfo">
                                Existing Hosp/Clinic #:</label>
                            <asp:Label ID="lblexisclinicid" runat="server" Text="12345678-444444-596A"></asp:Label></td>
                    </tr>--%>
                    <tr>
                        <td class="form" align="center" width="50%">
                            <label id="Vdate" class="required right35">
                                *Visit Date:
                            </label>
                            <asp:TextBox ID="txtvisitDate" runat="server" Width="17%" MaxLength="11"></asp:TextBox>
                            <img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            <%--<input id="txtvisitDate" maxlength="11" size="11" name="visitDate" runat="server" />
                        <img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID%>');" height="22" alt="Date Helper"
                            hspace="5" src="../images/cal_icon.gif" width="22" border="0"><span class="smallerlabel">(DD-MMM-YYYY)</span>
                            --%>
                        </td>
                        <td class="form" align="center">
                            <label class="right35">
                                Date of HIV Diagnosis:
                            </label>
                            <input id="txtHIVDiagnosisdate" maxlength="11" size="11" name="dateHIVDiagnosis"
                                runat="server" />&nbsp;
                            <img onclick="w_displayDatePicker('<%= txtHIVDiagnosisdate.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center">
                            <label class="right35">
                                Diagnosis Verified:</label>
                            <input id="rdoHIVDiagnosisVerifiedYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" name="HIVDiagnosisVerified1" runat="server" />
                            <label>
                                Yes</label>
                            <input id="rdoHIVDiagnosisVerifiedNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" type="radio"
                                name="HIVDiagnosisVerified1" runat="server" />
                            <label>
                                No</label>
                        </td>
                        <td class="form" align="center" nowrap="nowrap">
                            <label class="right35">
                                Disclosed:</label>
                            <input id="rdoDisclosureYes" onmouseup="up(this);" onfocus="up(this);" onclick="show('showdisclosureName');"
                                type="radio" value="1" name="disclosure" runat="server" />&nbsp;
                            <label>
                                Yes</label>
                            <input id="rdoDisclosureNo" onmouseup="up(this);" onfocus="up(this);" type="radio" value="0" name="disclosure"
                                runat="server" />
                            <label>
                                No</label>
                            <div id="showdisclosureName" style="display: none">
                                <br />
                                <label class="right15">
                                    Who to:</label><br />
                                <div class="center">
                                    <div id="divwhoto" class="checkbox">
                                        <table id="tblHIVdisclosure" cellpadding="0" cellspacing="2" width="100%" border="0"
                                            runat="server">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <!-- if gender is female then-->
                    <tr>
                        <td id="tdPregnant" class="form" runat="server" colspan="2">
                            <label id="LMP" for="LMP">
                                LMP:</label>
                            <input id="txtLMPdate" runat="server" maxlength="11" size="11" />
                            <img id="imgLMP" onclick="w_displayDatePicker('<%=txtLMPdate.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span> <span id="spdelivery" style="display: none">
                                <label id="lbldelivered">
                                    &nbsp; Delivered since last visit:
                                </label>
                                <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanDelDate'); hide('spanEDD')"
                                    type="radio" value="1" name="delivered" runat="server" />
                                <label id="lblDeliveredYes" runat="server">
                                    Yes</label>
                                <input id="rdoDeliveredNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); hide('spanDelDate')"
                                    type="radio" value="0" name="delivered" runat="server" />
                                <label id="lblDeliveredNo" runat="server">
                                    No</label>
                                &nbsp;<span id="spanDelDate" class="right10" style="display: none">
                                    <label id="lblDelDate" class="right15" for="DelDate">
                                        Delivered Date:</label>
                                    <input id="txtDeliDate" runat="server" maxlength="11" size="11" />
                                    <img id="imgDelDate" onclick="w_displayDatePicker('<%=txtDeliDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                        border="0" />
                                    <span class="smallerlabel">(DD-MMM-YYYY)</span> </span></span><span id="rdopregnantyesno"
                                        style="display: inline">
                                        <label id="lblpregnanttmp" class="right10">
                                            Pregnant:
                                        </label>
                                        <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD')"
                                            type="radio" value="1" name="pregnant" runat="server" />
                                        <label>
                                            Yes</label>
                                        <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD')"
                                            type="radio" value="0" name="pregnant" runat="server" />
                                        <label>
                                            No</label></span> <span id="spanEDD" style="display: none">
                                                <label id="lblEDD" class="right10" for="EDD">
                                                    EDD:</label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtEDDDate"
                                                    runat="server" maxlength="11" size="11" />
                                                <img id="imgEDD" onclick="w_displayDatePicker('<%=txtEDDDate.ClientID%>');" height="22"
                                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span> </span>
                        </td>
                    </tr>
                    <!-- end if gender is female -->
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                Presenting Complaints</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" width="50%" colspan="2" align="left">
                            <label>
                                Presenting Complaints:</label>
                            <input id="chkpresentingComplaintsNone" enableviewstate="true" type="checkbox" checked="checked"
                                value="None" name="presentingComplaintsNone" runat="server" />
                            <span class="smalllabel">None</span><input id="chkpresentingComplaintsNonehidden"
                                checked="checked" value="None" class="textstylehidden" type="checkbox" runat="server" />
                            <div id="presentingComplaintsShow" enableviewstate="true" style="display: none" runat="server">
                                <div class="checkbox" id="divPresenting" nowrap="noWrap">
                                    <asp:CheckBoxList ID="cblPresentingComplaints" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" width="50%" colspan="2" align="left">
                            <label>
                                TB Screening:</label>
                            <input id="rdoPerformed" onmouseup="up(this);" onfocus="up(this);" onclick="toggle('divTBPerformed'); down(this); show('divTBPerformed');"
                                type="radio" value="show" name="Symptom" runat="server" />
                            <span id="SpanTBPerformed" class="smallerlabel">Performed</span>
                            <%-- <input id="rdoNone" onfocus="up(this);" onclick="toggle('divTBPerformed'); down(this); hide('divTBPerformed');"
                            type="radio" value="None" name="Symptom" runat="server">
                        <span id="SpanTBNone" class="smallerlabel">None</span>--%>
                            <input id="rdoNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="toggle('divTBPerformed');down(this); hide('divTBPerformed');"
                                type="radio" value="Not Documented" name="Symptom" runat="server" />
                            <span id="SpanTBNotDocumented" class="smallerlabel">Not Documented</span>
                            <div class="divborder" id="divTBPerformed" nowrap="noWrap" style="display: none">
                                <asp:CheckBoxList ID="cblTBScreen"  runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <br />
                </tbody>
            </table>
        </div>
        <br />
        <div class="border formbg">
            <br />
            <h2 class="forms" align="left">
                Medical History</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" align="left">
                            <label>
                                Disease History:</label>
                            <input class="margin20" id="rdoMedHistNone" onmouseup="up(this);" onfocus="up(this);" type="radio" value="none"
                                name="medicalHis" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input class="margin20" id="rdoMedHistnotdocumented" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="Not documented" name="medicalHis" runat="server" /> 
                            <span class="smallerlabel">Not Documented</span>
                            <div id="divdiseaseHistory" nowrap="nowrap">
                                <asp:GridView ID="GrdMedHist" runat="server" Width="100%" AutoGenerateColumns="False"
                                    ShowHeader="False">
                                </asp:GridView>
                            </div>
                        </td>
                        <td class="form" valign="top" width="50%" align="left">
                            <label>
                                Drug Allergies/Toxicities:</label><br/>
                            <input id="rdoAllergynone" onmouseup="up(this);" onfocus="up(this);" type="radio" value="none" name="noneAllergy"
                                runat="server" />
                            <span id="lblAllergyNone" class="smallerlabel">None</span>
                            <input class="margin10" id="rdoAllergynotdocumented" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="Not Documented" name="noneAllergy" runat="server" />
                            <span id="lblAllergyNotDocumented" class="smallerlabel">Not Documented</span>
                            <input class="margin10" id="chksulfaAllergy" onclick="SetCheckBoxes('aspnetForm','noneAllergy',false);"
                                type="checkbox" value="Yes" name="sulfaAllergy" runat="server" />
                            <span id="lblAllergySulfa" class="smallerlabel">Sulfa Drugs</span>
                            <input class="margin10" id="chkotherAllergy" type="checkbox" value="yes" name="otherAllergy"
                                runat="server" />
                            <span id="lblAllergyOther" class="smallerlabel">Other (specify):</span> <span id="otherAllergyName"
                                style="display: none">
                                <input class="inputsmall" id="txtotherAllergyName" maxlength="40" size="10" name="otherAllergyName"
                                    runat="server" />
                            </span>
                            <br />
                            <br />
                            <div class="bold">
                                Current Long Term Medications (select all that apply):</div>
                            <div class="checkboxns">
                                <table>
                                    <tr>
                                        <td>
                                            <label class="check" for="longTermMedsSulfa">
                                                <input id="chklongTermMedsSulfa" onclick="toggle('longTermMedsSulfaSelected');" type="checkbox"
                                                    name="longTermMedsSulfa" runat="server" />Sulfa/TMP</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="longTermMedsSulfaSelected" style="display: none">
                                                <label class="margin15">
                                                    Describe:</label>
                                                <input id="txtlongTermMedsSulfaDesc" maxlength="40" size="30" name="longTermMedsSulfaDesc"
                                                    runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="check" for="longTermMedsARV">
                                                <input id="chkLongTermTBMed" onclick="toggle('longTermMedsTBSelected');" type="checkbox"
                                                    name="LongTermTBMed" runat="server" />TB Rx (Specify)</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="longTermMedsTBSelected" style="display: none">
                                                <label class="margin15">
                                                    Describe:</label>
                                                <input id="txtLongTermTBMedDesc" maxlength="40" size="10" name="LongTermTBMedDesc"
                                                    runat="server" />
                                                <label class="margin10">
                                                    Start Date:</label>
                                                <input id="txtLongTermTBStartDate" onkeyup="DateFormat(this,this.value,event,false,'4')"
                                                    onfocus="javascript:vDateType='4'" maxlength="8" size="5" name="LongTermTBStartDate"
                                                    runat="server" /><span class="smallerlabel margin10">MMM-YYYY</span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="check" for="longTermMedsOther1">
                                                <input id="chklongTermMedsOther1" onclick="toggle('longTermMedsOther1Selected');"
                                                    type="checkbox" value="Other1" name="longTermMedsOther1" runat="server" />
                                                Other (Specify)</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="longTermMedsOther1Selected" style="display: none">
                                                <label class="margin15">
                                                    Describe:</label>
                                                <input id="txtlongTermMedsOther1Desc" maxlength="40" size="30" name="longTermMedsOther1Desc"
                                                    runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="check" for="longTermMedsOther2">
                                                <input id="chklongTermMedsOther2" onclick="toggle('longTermMedsOther2Selected');"
                                                    type="checkbox" value="Other2" name="longTermMedsOther2" runat="server" />
                                                Other (Specify)</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="longTermMedsOther2Selected" style="display: none">
                                                <label class="margin15">
                                                    Describe:</label>
                                                <input id="txtlongTermMedsOther2Desc" maxlength="40" size="30" name="longTermMedsOther2Desc"
                                                    runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
        </div>
        <br />
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                HIV-Related History</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" style="width: 449px" align="left"  >
                            <label>
                                Lowest CD4:</label>
                            <input id="rdoprevLowestCD4none" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" type="radio"
                                name="prevLowestCD4none" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdoprevLowestCD4notdocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="prevLowestCD4none" runat="server" /><span class="smallerlabel">Not
                                    Documented</span><br />
                            <input class="margin50" id="txtprevLowestCD4" maxlength="4" size="3" name="prevLowestCD4"
                                runat="server" />
                            <span class="smallerlabel">c/mm<sup>3</sup></span>
                            <input class="margin10" id="txtprevLowestCD4Percent" maxlength="3" size="2" name="prevLowestCD4Percent"
                                runat="server" />
                            <span class="smallerlabel">%</span>
                            <label class="margin10">
                                Date:</label>
                            <input id="txtprevLowestCD4Date" maxlength="11" size="8" name="prevLowestCD4Date"
                                runat="server" />
                            <img onclick="w_displayDatePicker('<%= txtprevLowestCD4Date.ClientID%>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span><br />
                            <label>
                                CD4 Prior to Starting ARVs:</label>
                            <input id="rdopriorARVsCD4none" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" type="radio"
                                value="" name="priorARVsCD4none" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdopriorARVsCD4notdocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="priorARVsCD4none" runat="server" />
                            <span class="smallerlabel">Not Documented</span><br />
                            <input class="margin50" id="txtpriorARVsCD4" maxlength="4" size="3" name="priorARVsCD4"
                                runat="server" />
                            <span class="smallerlabel">c/mm<sup>3</sup></span>
                            <input class="margin10" id="txtpriorARVsCD4Percent" maxlength="3" size="2" name="priorARVsCD4Percent"
                                runat="server" />
                            <span class="smallerlabel">%</span>
                            <label class="margin10">
                                Date:</label>
                            <input id="txtpriorARVsCD4Date" maxlength="11" size="8" name="priorARVsCD4Date" runat="server" />
                            <img onclick="w_displayDatePicker('<%= txtpriorARVsCD4Date.ClientID%>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            <label>
                                Most Recent CD4:</label>
                            <input id="rdomostRecentCD4none" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" type="radio"
                                value="" name="mostRecentCD4none" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdomostRecentCD4notdocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="mostRecentCD4none" runat="server" />
                            <span class="smallerlabel">Not Documented</span><br>
                            <input class="margin50" id="txtmostRecentCD4" maxlength="4" size="3" name="mostRecentCD4"
                                runat="server" />
                            <span class="smallerlabel">c/mm<sup>3</sup></span>
                            <input class="margin10" id="txtmostRecentCD4Percent" maxlength="3" size="2" name="mostRecentCD4Percent"
                                runat="server" />
                            <span class="smallerlabel">%</span>
                            <label class="margin10">
                                Date:</label>
                            <input id="txtmostRecentCD4Date" maxlength="11" size="8" name="mostRecentCD4Date"
                                runat="server" />
                            <img onclick="w_displayDatePicker('<%=txtmostRecentCD4Date.ClientID%>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            <label>
                                Pre-therapy Viral Load:</label>
                            <input id="rdomostRecentViralLoadnone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="mostRecentViralLoadnone" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdomostRecentViralLoadnotdocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="mostRecentViralLoadnone" runat="server" />
                            <span class="smallerlabel">Not Documented</span><br />
                            <input class="margin50" id="txtmostRecentViralLoad" maxlength="9" size="11" name="mostRecentViralLoad"
                                runat="server" />
                            <span class="smallerlabel">c/ml</span>
                            <label class="margin50">
                                Date:</label>
                            <input id="txtmostRecentViralLoadDate" maxlength="11" size="8" name="mostRecentViralLoadDate"
                                runat="server" />
                            <img onclick="w_displayDatePicker('<%= txtmostRecentViralLoadDate.ClientID%>');"
                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                        <td class="form" valign="top" width="50%" align="left">
                            <label class="right20">
                                ARV Exposure:</label>
                            <input id="rdoprevARVExposureNone" onmouseup="up(this);" onfocus="up(this);" type="radio" value="" name="prevARVExposure"
                                runat="server" />
                            <span id="lblprevARVExposureNone" class="smallerlabel">None</span>
                            <input id="rdoprevARVExpnotdocumented" onmouseup="up(this);" onfocus="up(this);" type="radio" value=""
                                name="prevARVExposure" runat="server" />
                            <span id="lblprevARVExposurenotdocumented" class="smallerlabel">Not Documented</span>
                            <input id="rdopreviousARV" onmouseup="up(this);" onfocus="up(this);" onclick="show('prevexpdiv');" type="radio"
                                value="" name="prevARVExposure" runat="server" />
                            <span id="lblpreviousARV" class="smallerlabel">Enter ARV Exposure</span>
                            <div id="prevexpdiv" style="display: none;">
                                <br />
                                <br />
                                <label id="Cuart" class="right20 required">
                                    *Historical ART:</label>
                                <input id="txtcurrentART" size="16" name="currentART" readonly="readonly" runat="server" />
                                <asp:Button ID="btnRegimen" runat="server" Text="..." OnClick="btnRegimen_Click" />
                                <span class="smalllabel margin10">Start Date:</span>
                                <input id="txtcurrentARTDate" maxlength="8" size="5" onkeyup="DateFormat(this,this.value,event,false,'4')"
                                    onfocus="javascript:vDateType='4'" name="currentARTDate" runat="server" /><span class="smallerlabel margin10">MMM-YYYY</span>
                                <br />
                                <br />
                                <label>
                                    Previous ARV Exposure:</label>
                                <br />
                                <input id="chkprevSDNVPNVP" onclick="toggle('prevSingleDoseNVPSelected');" value="Yes"
                                    type="checkbox" name="prevSingleDoseNVP" runat="server" class="margin50" />
                                <label id="lblNVP">
                                    Single Dose NVP</label>
                                <span id="prevSingleDoseNVPSelected" style="display: none">
                                    <br />
                                    <label class="right25 margin80">
                                        Date:</label>
                                    <input class="inputsmall" id="txtprevSingleDoseNVPDate1" onkeyup="DateFormat(this,this.value,event,false,'4')"
                                        onfocus="javascript:vDateType='4'" maxlength="8" size="5" name="prevSingleDoseNVPDate"
                                        runat="server" />
                                    <span class="smallerlabel">MMM-YYYY</span>
                                    <br />
                                    <label class="right25 margin80">
                                        Date:</label>
                                    <input class="inputsmall" id="txtprevSingleDoseNVPDate2" onkeyup="DateFormat(this,this.value,event,false,'4')"
                                        onfocus="javascript:vDateType='4'" maxlength="8" size="5" name="prevSingleDoseNVPDate"
                                        runat="server" />
                                    <span class="smallerlabel">MMM-YYYY</span> </span>
                                <br />
                                <label for="prevARVRegimen">
                                    <input id="chkprevARVRegimen" onclick="toggle('prevARVReg');" type="checkbox" value="No"
                                        name="prevARVRegimen" class="margin50" runat="server" /><label id="lblprevARVRegimen">Previous
                                            Regimens</label></label>
                                     <br />
                                <div id="prevARVReg" style="display: none">
                                    <label class="right25 margin50" for="prevARVRegimen1Name">
                                        Regimen:</label>
                                    <input id="txtprevARVRegimen1Name" size="15" name="prevARVRegimen1Name" readonly="readonly"
                                        runat="server" />
                                    <asp:Button ID="btnRegimen1" runat="server" Text="..." OnClick="btnRegimen1_Click" />
                                    <span class="smalllabel margin10">Months on Rx*</span>
                                    <input id="txtprevARVRegimen1Months" maxlength="7" size="5" name="prevARVRegimen1Months"
                                        runat="server" /><br />
                                    <label class="right25 margin50" for="prevARVRegimen2Name">
                                        Regimen:</label>
                                    <input id="txtprevARVRegimen2Name" size="15" name="prevARVRegimen2Name" readonly="readonly"
                                        runat="server" />
                                    <asp:Button ID="btnRegimen2" runat="server" Text="..." OnClick="btnRegimen2_Click" />
                                    <span class="smalllabel margin10">Months on Rx*</span>
                                    <input id="txtprevARVRegimen2Months" maxlength="7" size="5" name="prevARVRegimen2Months"
                                        runat="server" /><br />

                                    <label class="right25 margin50" for="prevOtherRegimen1Name">
                                        Regimen:</label>
                                    <input id="txtprevARVRegimen3Name" size="15" name="prevOtherRegimen1Name" readonly="readonly"
                                        runat="server" />
                                    <asp:Button ID="btnRegimen3" runat="server" Text="..." OnClick="btnRegimen3_Click" />
                                    <span class="smalllabel margin10">Months on Rx*</span>
                                    <input id="txtprevARVRegimen3Months" maxlength="7" size="5" name="prevOtherRegimen1Months"
                                        runat="server" /><br />
                                    <label class="right25 margin50" for="prevOtherRegimen2Name">
                                        Regimen:</label>
                                    <input id="txtprevARVRegimen4Name" size="15" name="prevOtherRegimen2Name" readonly="readonly"
                                        runat="server" />
                                    <asp:Button ID="btnRegimen4" runat="server" Text="..." OnClick="btnRegimen4_Click" />
                                    <span class="smalllabel margin10">Months on Rx*</span>
                                    <input id="txtprevARVRegimen4Months" maxlength="7" size="5" name="prevOtherRegimen2Months"
                                        runat="server" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" valign="top" colspan="2" style="height: 136px" align="left">
                            <label>
                                HIV-Associated Conditions:</label>
                            <input id="rdoHIVassocNone" onmouseup="up(this);" onfocus="up(this);" type="radio" value="none" name="assocNone"
                                runat="server" />
                            <span id="lblHIVassocNone" class="smallerlabel">None</span>
                            <input id="rdoPrevHIVassocNotDocumented" onmouseup="up(this);" onfocus="up(this);" type="radio" value="Not Documented"
                                name="assocNone" runat="server" />
                            <span id="lblHIVassocNotdocumented" class="smallerlabel">Not Documented</span>
                            <input id="rdoHIVassociate" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); toggle('assocSelected');"
                                type="radio" value="show" name="assocNone" runat="server" />
                            <span id="lblHIVassociate" class="smallerlabel">Enter HIV-Associated Conditions</span>
                            <div id="assocSelected" style="display: none">
                                <div id="diventerillnessleft" class="checkboxleft">
                                    <table id="tblHIVAIDSleft" cellpadding="0" cellspacing="0" width="100%" border="0"
                                        runat="server">
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="checkboxright" id="diventerillnessright">
                                    <table id="tblHIVAIDSright" cellpadding="0" cellspacing="0" width="100%" border="0"
                                        runat="server">
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
        </div>
        <br />
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                Physical Exam</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form center" colspan="2" valign="middle">
                     
                            <label  id="TempVal" class="margin3">
                                Temp:</label>
                            <input id="txtphysTemp" maxlength="4" size="3" name="physTemp" runat="server" />
                            <span class="smallerlabel">C</span>
                            <label class="margin5">
                                RR:</label><input id="txtphysRR" maxlength="4" size="3" name="physRR" runat="server" />
                            <span class="smallerlabel">bpm</span>
                            <label class="margin5">
                                HR:</label>
                            <input id="txtphysHR" maxlength="3" size="3" name="physHR" runat="server" /><span
                                class="smallerlabel"> bpm</span>
                            <label class="margin5">
                                BP:</label>
                            <input id="txtphysBPSystolic" maxlength="4" size="3" name="physBPSystolic" runat="server" />
                            /
                            <input id="txtphysBPDiastolic" maxlength="4" size="3" name="physBPDiastolic" runat="server" /><span
                                class="smallerlabel"> (mm/Hg)</span>
                            <label id="lblHT" class="margin5">
                                HT:</label>
                            <input id="txtphysHeight" maxlength="4" size="3" name="physHeight" runat="server" />
                            <span class="smallerlabel">cm</span>
                            <label id="lblWT" class="margin5">
                                WT:</label>
                            <input id="txtphysWeight" maxlength="4" size="3" name="physWeight" runat="server" />
                            <span class="smallerlabel">kg</span> 
                            
                              <label class="margin5">
                                BMI :</label>                            
                            <input id="txtanotherbmi" size="3" name="anotherbmi" runat="server" readonly="readonly"/>

                            <label class="margin5">
                                Pain:</label>
                            <select id="ddlPain" name="pain" runat="server">
                                <option value="0" selected="selected">Select</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center" valign="middle" width="50%">
                            <label id="lblWAB" class="right35">
                                WAB Stage:</label>
                            <asp:DropDownList ID="ddlphysWABStage" runat="server">
                                <asp:ListItem Value="0" Selected="true">Select</asp:ListItem>
                                <asp:ListItem Value="84">Working</asp:ListItem>
                                <asp:ListItem Value="85">Ambulatory</asp:ListItem>
                                <asp:ListItem Value="86">Bedridden</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="form" align="center" valign="middle" width="50%">
                            <label id="lblWHO" class="right35">
                                WHO Stage:</label>
                            <asp:DropDownList ID="ddlWHOStage" runat="server">
                                <asp:ListItem Value="0" Selected="true">Select</asp:ListItem>
                                <asp:ListItem Value="155">N/A</asp:ListItem>
                                <asp:ListItem Value="87">I</asp:ListItem>
                                <asp:ListItem Value="88">II</asp:ListItem>
                                <asp:ListItem Value="89">III</asp:ListItem>
                                <asp:ListItem Value="90">IV</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
        </div>
        <br />
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                Assessment, Plan and Regimen</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" width="50%" align="left">
                            <label>
                                Assessment:</label>
                            <br />
                            <input name="clinAssessmentInitialNone" id="rdoclinAssessment_Plan_RegimenNone" onmouseup="up(this);" onfocus="up(this);"
                                type="radio" value="No Illness Found" runat="server" />
                            <span id="lblAssessNone" class="smallerlabel">No Illness Found</span>
                            <input name="clinAssessmentInitial" id="chkclinAssessmentInitial1" onclick="SetCheckBoxes('aspnetForm','clinAssessmentInitialNone',false);"
                                type="checkbox" value="HIV-Related Illness" runat="server" />
                            <span id="lblHIVrelated" class="smallerlabel">HIV-Related Illness/OI</span>
                            <input name="clinAssessmentInitial" id="chkclinAssessmentInitial2" onclick="SetCheckBoxes('aspnetForm','clinAssessmentInitialNone',false);"
                                type="checkbox" value="Non-HIV-Related Illness" runat="server" />
                            <span id="lblHIVrelatedNon" class="smallerlabel">Non-HIV-Related Illness</span>
                            <br />
                            <div class="center">
                                <textarea name="clinAssessmentNotes" rows="5" cols="70" id="MulttxtclinAssessmentNotes"
                                    runat="server"></textarea>
                            </div>
                        </td>
                        <td class="form" valign="top" align="left" nowrap="nowrap">
                            <label>
                                Plan:</label>
                            <br />
                            <input class="margin10left" id="chkclinPlanInitial" type="checkbox" value="Lab evaluation/TB screen"
                                name="clinPlanInitial" runat="server" />
                            <span class="smallerlabel">Lab evaluation/TB screen</span>
                            <input id="chkclinPlanInitial2" type="checkbox" value="OI prophylaxis/treatment"
                                name="clinPlanInitial" runat="server" />
                            <span class="smallerlabel">OI prophylaxis/treatment</span>
                            <input id="chkclinPlanInitial3" type="checkbox" value="Treatment Preparation" name="clinPlanInitial"
                                runat="server" />
                            <span class="smallerlabel">Treatment Preparation</span>
                            <input id="chkclinPlanInitial4" type="checkbox" value="Other" name="clinPlanInitial"
                                runat="server" />
                            <span class="smallerlabel">Other</span><br />
                            <div class="center">
                                <textarea name="clinPlanNotes" rows="5" cols="60" id="MulttxtclinPlanNotes" runat="server"></textarea>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form center" valign="top" colspan="2">
                            <label>
                                *ARV Therapy:</label>
                            <select id="lstclinPlanIE" onchange="specifyChangeStop(this.id);" name="clinPlanIE"
                                runat="server">
                                <option value="0" selected="selected">Select</option>
                                <option value="94">Treatment not indicated now</option>
                                <option value="95">Continue current treatment</option>
                                <option value="96">Restart treatment</option>
                                <option value="97">Start new treatment(naive patient)</option>
                                <option value="98">Change regimen</option>
                                <option value="99">Stop treatment</option>
                            </select>
                            <div id="arvTherapyChange" style="display: none">
                                <label class="required margin80">
                                    *Change Regimen Reason:</label>
                                <asp:DropDownList ID="ddlTherapyChange" runat="server">
                                </asp:DropDownList>
                                <div id="otherarvTherapyChangeCode" style="display: none">
                                    <label class="required right45" for="arvTherapyChangeCodeOtherName">
                                        *Specify:</label>
                                    <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName"
                                        runat="server" /></div>
                            </div>
                            <div id="arvTherapyStop" style="display: none; padding-left: 20px">
                                <label class="required margin80">
                                    *Stop Regimen Reason:</label>
                                <asp:DropDownList ID="ddlTherapyStop" runat="server">
                                </asp:DropDownList>
                                <div id="otherarvTherapyStopCode" style="display: none">
                                    <label class="required right45" for="arvTherapyStopCodeOtherName">
                                        *Specify:</label>
                                    <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName"
                                        runat="server" /></div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br>
        </div>
        <br />
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                Clinical Notes</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" width="100%">
                            <asp:TextBox ID="txtclinicalNotes" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                Appointment and Signature</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" align="center">
                            <label class="right50">
                                When is the patient's next appointment?</label>
                            <select id="lstappPeriod" name="appPeriod" runat="server" onchange="addDays()">
                                <option value="0" selected="selected">Select</option>
                                <option value="7">1 week</option>
                                <option value="14">2 weeks</option>
                                <option value="28">4 weeks</option>
                                <option value="58">2 months</option>
                                <option value="88">3 months</option>
                                <option value="180">6 months</option>
                            </select>
                        </td>
                        <td class="border pad5 whitebg" align="center">
                            <label class="right40">
                                Appointment Reason:</label>
                            <asp:DropDownList ID="ddappReason" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" align="center">
                            <label class="right50">
                                Specify Date:</label>
                            <input id="txtappDate" maxlength="11" size="11" name="appDate" runat="server" />
                            <img onclick="w_displayDatePicker('<%=txtappDate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0"/><span class="smallerlabel"
                                    id="appDatespan">(DD-MMM-YYYY)</span>
                        </td>
                        <td class="border pad5 whitebg" align="center">
                            <label id="lblsignature" class="right40">
                                Signature:</label>
                            <asp:DropDownList ID="ddinterviewer" runat="server">
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
                        <td class="form" colspan="2">
                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                            <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click" />
                            <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
 
</asp:Content>
