<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.NonARTFollowUp"
    Title="Untitled Page" Codebehind="frmClinical_NonARTFollowUp.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        //For appointment
        function WindowPrint() { 
            window.print();
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
        function addDays(id) { 
            if (id >= 0) {
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
                    document.getElementById('<%=txtSpecifyDate.ClientID%>').value = "";
                    document.getElementById('<%=ddlAppReason.ClientID%>').value = 0;
                    // document.getElementById('<%=ddlPharmOrderedbyName.ClientID%>').value=0;
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
                    document.getElementById('<%=txtSpecifyDate.ClientID%>').value = dt2;
                    if (txtdate == "") {
                        document.getElementById('<%=txtSpecifyDate.ClientID%>').value = "";
                        document.getElementById('<%=ddlAppReason.ClientID%>').value = 0;
                        // document.getElementById('<%=lstappPeriod.ClientID%>').value = 0;
                    }
                }
                return;
            }
        }
        //code Name   
        function SendCD4() {
            var id = document.getElementById('<%=txtvisitDate.ClientID%>').value;
            if (id.length <= 0) {
                //			alert('Please enter visit Date');
                return true;
            }
            else {
                CallServer(id, "This is context from client");
                return true;
            }
        }
        function ReceiveServerData(args, context) {
            var a = args;
            var temp = new Array(); temp = a.split('zz');
            if (window.ActiveXObject) {
                //var obj = new ActiveXObject("Microsoft.XMLDOM")
                var obj = new ActiveXObject("MSXML2.DOMDocument");
                obj.loadXML(temp[0]);
                var dsRoot = obj.documentElement;
                //CD4 Values
                document.getElementById('<%=txtTestResults.ClientID%>').value = "";
                document.getElementById('<%=txtTestResultsDate.ClientID%>').value = "";
                if (dsRoot.childNodes(0).childNodes(0).firstChild.text > 0) {
                    document.getElementById('<%=txtTestResults.ClientID%>').value = dsRoot.childNodes(0).childNodes(0).firstChild.text;
                    formattedDate = dsRoot.childNodes(0).childNodes(1).firstChild.text
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    var dd = formattedDate.substr(0, 2);
                    formattedDate = dd + "-" + mm + "-" + yr;
                    document.getElementById('<%=txtTestResultsDate.ClientID%>').value = formattedDate
                }


                //Height 
                var obj1 = new ActiveXObject("MSXML2.DOMDocument");
                obj1.loadXML(temp[1]);
                var dsRoot1 = obj1.documentElement;
                document.getElementById('<%=txtphysHeight.ClientID%>').value = "";
                var obj1 = dsRoot1.childNodes(0).childNodes(1).firstChild.text;
                if (obj1 == 18) {
                    document.getElementById('<%=txtphysHeight.ClientID%>').value = dsRoot1.childNodes(0).childNodes(0).firstChild.text;
                }
                var obj2 = dsRoot1.childNodes(1).childNodes(0).firstChild.text;
                if (obj2 == 1) {
                    var ans = true;
                    alert('You Cannot Create Pharmacy and Non-ART form for a Patient on same date');
                    if (ans == true) {
                        //window.location.href('frmPatient_Home.aspx?PatientId=<%=Request.QueryString["PatientId"]%>');
                        document.getElementById('<%=txtvisitDate.ClientID%>').value = "";
                        document.getElementById('<%=txtvisitDate.ClientID%>').focus();
                    }
                }
                var objPreg = new ActiveXObject("MSXML2.DOMDocument");
                objPreg.loadXML(temp[2]);
                var dsRootPreg = objPreg.documentElement;
                //if (dsRootPreg.childNodes.length > 0)
                if (dsRootPreg.text != '') {
                    if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 0 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 1) {
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 0 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 1) {
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        document.getElementById('<%=txtLMP.ClientID%>').value = "";
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 9 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 9) {
                        hide('rdopregnantyesno');
                        show('spdelivery');
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
    </script>
    <div class="center" style="padding: 8px;">
       
        <div class="border center formbg">
            <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" width="50%" align="center" style="height: 38px">
                            <label id="Vdate" class="required right35">
                                Visit Date:
                            </label>
                            <asp:TextBox ID="txtvisitDate" MaxLength="11" size="8" runat="server"></asp:TextBox>
                            <img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" id="IMG1"/><span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                        <td class="form" align="center">
                            <label>
                                Last CD4 Count:</label>
                            <asp:TextBox ID="txtTestResults" Width="50px" MaxLength="4" ReadOnly="true" runat="server"></asp:TextBox>
                            <span class="smallerlabel">c/mm<sup>3</sup></span>
                            <label class="margin10">
                                Date:</label>
                            <input id="txtTestResultsDate" maxlength="11" size="11" name="TestResultsDate" readonly="readonly"
                                runat="server"/>
                            <img id="theIMG" runat="server" onclick="w_displayDatePicker('<%=txtTestResultsDate.ClientID%>');"
                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                border="0"/>
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <!-- if gender is female then-->
                    <tr>
                        <td id="tdPregnant" class="form" runat="server" colspan="2">
                            <label id="LMPdate" class="right10" for="LMP">
                                LMP:</label>
                            <input id="txtLMP" maxlength="11" size="11" runat="server"/>
                            <img id="imgLMP" onclick="w_displayDatePicker('<%=txtLMP.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0"/>
                            <span class="smallerlabel">(DD-MMM-YYYY)</span> <span id="spdelivery" style="display: none">
                                <label id="lbldelivered">
                                    Delivered since last visit:
                                </label>
                                <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanDelDate'); hide('spanEDD')"
                                    type="radio" value="1" name="delivered" runat="server"/>
                                <label>
                                    Yes</label>
                                <input id="rdoDeliveredNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); hide('spanDelDate')"
                                    type="radio" value="0" name="delivered" runat="server"/>
                                <label>
                                    No</label>
                                <span id="spanDelDate" style="display: none">
                                    <label id="lblDelDate" class="right10" for="DelDate">
                                        Delivered Date:</label>
                                    <input id="txtDeliDate" runat="server" maxlength="11" size="11"/>
                                    <img id="imgDelDate" onclick="w_displayDatePicker('<%=txtDeliDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                        border="0"/>
                                    <span class="smallerlabel">(DD-MMM-YYYY)</span> </span></span>&nbsp;<%--<label id="PregnantVal" class="right10" runat="server">
                            Pregnant:
                        </label>--%><span id="rdopregnantyesno" style="display: inline"><label id="PregnantVal"
                            class="right10">Pregnant:
                        </label>
                            <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD');"
                                type="radio" value="Y" name="pregnant" runat="server"/>
                            <label>
                                Yes</label>
                            <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
                                type="radio" value="N" name="pregnant" runat="server"/><label>
                                    No</label></span> <span id="spanEDD" style="display: none">
                                        <label id="lblEDD" class="right10" for="EDD">
                                            EDD:</label>
                                        <input id="txtEDDDate" runat="server" maxlength="11" size="11"/>
                                        <img id="imgEDD" onclick="w_displayDatePicker('<%=txtEDDDate.ClientID%>');" height="22"
                                            alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0"/>
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span> </span>&nbsp;
                        </td>
                    </tr>
                    <!-- end if gender is female -->
                </tbody>
            </table>
        </div>
        <br/>
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                Presenting Complaints</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form leftallign" colspan="2" style="width: 25%" >
                            <label>
                                Presenting Complaints:</label>
                            <input id="chkpresentingComplaintsNone" type="checkbox" checked="checked" value="None" name="presentingComplaintsNone"
                                runat="server"/>
                            <span class="smalllabel">None</span>
                            <div nowrap="nowrap">
                                <input id="chkpresentingComplaintsNonehidden" checked="checked" value="None" class="textstylehidden"
                                    type="checkbox" runat="server"/>
                                <div id="presentingComplaintsShow" style="display: none" runat="server">
                                    <br/>
                                    <div class="checkbox" nowrap="nowrap">
                                        <asp:CheckBoxList ID="cblPresentingComplaints" runat="server" RepeatLayout="Flow"
                                            Width="50%">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <%--<td class="form" colspan="2" style="width: 50%">
                        <label class="required">
                            TB Screening:</label>
                        <input id="rbtperformed" type="radio" name="TBScreening" runat="server">
                        <span class="smalllabel">Performed</span>
                        <input id="rbtperformedNone" type="radio" name="TBScreening" runat="server">
                        <span class="smalllabel">Not Documented</span>
                        <div id="DivTBScreen" style="display: none" runat="server" >
                                    <asp:CheckBoxList ID="cblTBScreen" runat="server" RepeatLayout="Flow"
                                        Width="0%">
                                    </asp:CheckBoxList>
                            </div>
                    </td>--%>
                        <td class="form leftallign" colspan="2" style="width: 50%">
                            <label class="required">
                                TB Screening:</label>
                            <input id="rdoPerformed" onmouseup="up(this);" onfocus="up(this);" type="radio" value="show" name="Symptom"
                                runat="server"/>
                            <span class="smalllabel">Performed</span>
                            <%--     <input id="rdoTBNone" onfocus="up(this);" type="radio" value="None" name="Symptom" runat="server">
                        <span class="smalllabel">None</span>
                            --%>
                            <input id="rdoNotDocumented" onmouseup="up(this);" onfocus="up(this);" type="radio" value="Not Documented"
                                name="Symptom" runat="server"/>
                            <span class="smalllabel">Not Documented</span>
                            <div class="checkbox" id="TBSymptom" nowrap="noWrap" style="display: none">
                                <asp:CheckBoxList ID="cblTBScreen" RepeatLayout="Flow" Width="50%" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br/>
        </div>
        <br/>
        <br/>
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                Physical Exam</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="formcenter" colspan="2" valign="middle">
                            <label id="TempVal">
                                Temp:</label>
                            <asp:TextBox ID="txtphysTemp" runat="server" MaxLength="4" Width="4%"></asp:TextBox>
                            <span class="smallerlabel">C</span>
                            <label class="margin5">
                                RR:</label>
                            <asp:TextBox ID="txtphysRR" runat="server" MaxLength="4" Width="4%"></asp:TextBox>
                            <span class="smallerlabel">bpm</span>
                            <label class="margin5">
                                HR:</label>
                            <asp:TextBox ID="txtphysHR" runat="server" MaxLength="3" Width="4%"></asp:TextBox>
                            <span class="smallerlabel">bpm</span>
                            <label class="margin5">
                                BP:</label>
                            <asp:TextBox ID="txtphysBPSystolic" runat="server" MaxLength="3" Width="4%"></asp:TextBox>/
                            <asp:TextBox ID="txtphysBPDiastolic" runat="server" MaxLength="3" Width="4%"></asp:TextBox>
                            <span class="smallerlabel">(mm/Hg)</span>
                            <label id="physHeightVal" class="margin5">
                                HT:</label>
                            <asp:TextBox ID="txtphysHeight" runat="server" MaxLength="3" Width="4%"></asp:TextBox>
                            <span class="smallerlabel">cm</span>
                            <label id="physWeightVal" class="margin5">
                                WT:</label>
                            <asp:TextBox ID="txtphysWeight" runat="server" MaxLength="4" Width="4%"></asp:TextBox> 
                            <span class="smallerlabel">kg</span>
                             <label class="margin5">
                                BMI :</label>                            
                            <input id="txtanotherbmi" size="6" name="anotherbmi" runat="server" readonly="readonly"/>
                            <label class="margin5">
                                Pain:</label>
                            <select id="ddlPain" name="pain" runat="server">
                                <option value="" selected="selected">Select</option>
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
                </tbody>
            </table>
        </div>
        <br/>
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                OIs or AIDS Defining Illnesses</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" colspan="2" align="left" style="height: 136px">
                            <label>
                                OIs or AIDS Defining Illnesses:</label>
                            <input id="rdoHIVassocNone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);  hide('assocSelected'); "
                                type="radio" checked value="none" name="assocNone" runat="server"/>
                            <span id="lblHIVassocNone" class="smallerlabel">None</span>
                            <input id="PrevHIVassocNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);  hide('assocSelected');"
                                type="radio" value="Not Documented" name="assocNone" runat="server"/>
                            <span id="lblHIVassocNotdocumented" class="smallerlabel">Not Documented</span>
                            <input id="rdoHIVassociate" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('assocSelected');"
                                type="radio" value="show" name="assocNone" runat="server"/>
                            <span id="lblHIVassociate" class="smallerlabel">Enter Illnesses</span>
                            <br />
                            <div id="assocSelected" style="display: None">
                                <div class="checkboxleft">
                                    <div>
                                        <table id="tblOIsAIDsleft" cellpadding="0" cellspacing="2" width="100%" border="0"
                                            runat="server">
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="checkboxright">
                                    <div>
                                        <table id="tblOIsAIDsright" cellpadding="0" cellspacing="2" width="100%" border="0"
                                            runat="server">
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <br>
            <h2 class="forms" align="left">
                Assessment</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" width="50%">
                            <table>
                                <tr>
                                    <td align="left">
                                        <label id="AssessmentVal">
                                            Clinical Assessment:</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        <div class="divborder" align="left" id="Div1" style="width: 369px;">
                                            <asp:CheckBoxList ID="chkAssessment" runat="server" Width="30%" CssClass="divborder"
                                                RepeatLayout="Flow">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="form" valign="top" width="50%" align="left">
                            <label id="ARTAssessmentVal" class="right35">
                                ART Assessment:</label>
                            <input id="rdoartAssessmentID" type="radio" value="" name="artAssessmentID" runat="server" onmouseup="up(this);"
                                onfocus="up(this);" onclick="down(this);" /><span class="smalllabel">Re-evaluate</span>
                            <input id="rdoartAssessmentID2" type="radio" value="" name="artAssessmentID" runat="server" onmouseup="up(this);"
                                onfocus="up(this);" onclick="down(this);" /><span class="smalllabel">Prepare for ART</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center" valign="middle" width="50%">
                            <label id="WHOSt" class="right35">
                                WHO Stage:</label>
                            <asp:DropDownList ID="ddlphysWHOStage" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="form" align="center" valign="middle" width="50%">
                            <label id="WABSt" class="right35">
                                WAB Stage:</label>
                            <asp:DropDownList ID="ddlphysWABStage" runat="server">
                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="84">Working</asp:ListItem>
                                <asp:ListItem Value="85">Ambulatory</asp:ListItem>
                                <asp:ListItem Value="86">Bedridden</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br/>
        </div>
        <br/>
        <div class="border center formbg">
            <br/>
            <h2 id="OI" class="forms" align="left">
                OI Prophylaxis</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" align="center" valign="middle" style="height: 45px">
                            <label class="right20">
                                Sulfa/TMX:</label>
                            <span class="smalllabel margin10">Dose:</span>
                            <select id="lstpharmSulfaTMXDose" name="pharmSulfaTMXDose" runat="server">
                                <option value="" selected="selected">Select</option>
                                <option value="108">80/400mg</option>
                                <option value="109">160/800mg</option>
                                <option value="134">40/200mg/5ml</option>
                            </select>
                            <span class="smalllabel margin10">Frequency:</span>
                            <select id="lstpharmSulfaTMXFreq" name="pharmSulfaTMXFreq" runat="server">
                                <option value="" selected="selected">Select</option>
                                <option value="1">OD</option>
                                <option value="2">BD</option>
                                <option value="6">QID</option>
                            </select>
                            <span class="smalllabel margin10">Duration:</span>
                            <input id="txtpharmSulfaTMXDuration" maxlength="5" size="2" name="pharmSulfaTMXDuration"
                                runat="server"/>
                            <span class="smalllabel margin10">Qty. Prescribed:</span>
                            <input id="txtpharmSulfaTMXQty" maxlength="5" size="2" name="pharmSulfaTMXQty" runat="server"/>
                            <span class="smalllabel margin10">Qty. Dispensed:</span>
                            <input id="txtpharmSulfaTMXDispensed" onblur="dispense(this.id);" maxlength="5" size="2"
                                name="pharmSulfaTMXDispensed" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center" valign="middle">
                            <label class="right20">
                                Fluconazole:</label>
                            <span class="smalllabel margin10">Dose:</span>
                            <select id="lstpharmFluconazoleDose" name="pharmFluconazoleDose" runat="server">
                                <option value="" selected="selected">Select</option>
                                <option value="7">200 mg</option>
                                <option value="8">400 mg</option>
                                <option value="26">800 mg</option>
                            </select>
                            <span class="smalllabel margin10">Frequency:</span>
                            <select id="lstpharmFluconazoleFreq" name="pharmFluconazoleFreq" runat="server">
                                <option value="" selected="selected">Select</option>
                                <option value="1">OD</option>
                                <option value="2">BD</option>
                            </select>
                            <span class="smalllabel margin10">Duration:</span>
                            <input id="txtpharmFluconazoleDuration" maxlength="5" size="2" name="pharmFluconazoleDuration"
                                runat="server"/>
                            <span class="smalllabel margin10">Qty. Prescribed:</span>
                            <input id="txtpharmFluconazoleQty" maxlength="5" size="2" name="pharmFluconazoleQty"
                                runat="server"/>
                            <span class="smalllabel margin10">Qty. Dispensed:</span>
                            <input id="txtpharmFluconazoleDispensed" onblur="dispense(this.id);" maxlength="5"
                                size="2" name="pharmFluconazoleDispensed" runat="server"/>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="border">
            <div align="center" id="divAddOI" runat="server">
                <asp:Button ID="OtherMedication" runat="server" Text="OI Treatment and Other Medications"
                    OnClick="OtherMedication_Click" />
            </div>
            <asp:Panel ID="PnlAddOtherMedication" runat="server" Width="100%" Wrap="true" HorizontalAlign="Left">
            </asp:Panel>
        </div>
        <br />
        <script language="javascript" type="text/javascript">
            function GetControl() {
                document.forms[0].submit();
            } 
        </script>
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                Clinical Notes</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" valign="top" width="100%">
                            <asp:TextBox ID="txtClinicalNotes" CssClass="textarea" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                Appointment and Signature</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" colspan="3" width="100%" style="height: 35px">
                            <label class="margin5">
                                When is the patient's next appointment?</label>
                            <select id="lstappPeriod" name="appPeriod" runat="server" onchange="addDays(0)">
                                <option value="0" selected="selected">Select</option>
                                <option value="7">1 week</option>
                                <option value="14">2 weeks</option>
                                <option value="30">4 weeks</option>
                                <option value="60">2 months</option>
                                <option value="90">3 months</option>
                                <option value="180">6 months</option>
                            </select>
                            <%--</TD>
        
        <TD class="border pad5 whitebg" >--%>
                            <label class="margin5">
                                Specify Date:</label>
                            <%--<asp:TextBox id="txtSpecifyDate" ReadOnly="true" Width=70px MaxLength=10 runat="server" ></asp:TextBox>--%>
                            <input id="txtSpecifyDate" maxlength="11" size="10" name="SpecifyDate" runat="server"/>
                            <img onclick="w_displayDatePicker('<%= txtSpecifyDate.ClientID %>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="20" border="0"/>
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>&nbsp;
                            <label class="margin5">
                                Appointment Reason:</label>
                            <asp:DropDownList ID="ddlAppReason" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" style="height: 38px" width="50%">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 50%">
                                        <label id="OrderedBy" class="required">
                                            Ordered by:</label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPharmOrderedbyName" runat="Server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="form" align="center" style="height: 38px" width="50%">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 50%">
                                        <label id="OrderedByDate" class="required" for="pharmOrderedbyDate">
                                            Ordered By Date:</label>
                                    </td>
                                    <td>
                                        <input id="txtpharmOrderedbyDate" maxlength="11" size="11" name="pharmOrderedbyDate"
                                            runat="server"/>
                                        <img onclick="w_displayDatePicker('<% =txtpharmOrderedbyDate.ClientID %>');" height="22"
                                            alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"/>
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center" style="height: 38px" width="50%">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 50%">
                                        <label id="DispensedBy">
                                            Dispensed by:</label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPharmReportedbyName" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="form" align="center" style="height: 38px" width="50%">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 50%">
                                        <label id="DispensedByDate" for="pharmReportedbyDate">
                                            Dispensed by Date:</label>
                                    </td>
                                    <td>
                                        <input id="txtpharmReportedbyDate" maxlength="11" size="11" name="pharmReportedbyDate"
                                            runat="server"/>
                                        <img onclick="w_displayDatePicker('<% =txtpharmReportedbyDate.ClientID %>');" height="22"
                                            alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                            name="appDateimg"/><span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" align="center" colspan="2">
                            <label id="Signature" class="required right30">
                                Signature:</label>
                            <select id="ddlPharmSignature" onchange="specifyChangeStop(this.id);" name="Signature"
                                runat="server">
                                <option value="0" selected="selected">Select</option>
                                <option value="1">No Signature</option>
                                <option value="2">Patient's Signature</option>
                                <option value="3">Adherance counsellor signature</option>
                            </select>
                            <div id="Adherance_counsellor_signature" style="display: none">
                                <label id="CounSign" class="required margin20" visible="true">
                                    Specify Counselor Name:
                                </label>
                                <asp:DropDownList ID="ddlCounselerName" runat="server">
                                </asp:DropDownList>
                            </div>
                            <%--<DIV id="txtCName" style="display:none">
     <%--<LABEL ID="lblCounselorName" class=margin20 visible="true" runat ="server">Specify Counselor Name: </LABEL> 
     <%--<asp:TextBox ID=txtCounselorName runat="server">
      </asp:TextBox>
      </DIV>---%>
                            <asp:Label ID="Label2" runat="server" Visible="false" Text="Label"></asp:Label>
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
                        <td class="pad5 center" colspan="2">
                            <br/>
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
                            <asp:Button ID="btnQualityCheck" Text="Data Quality check" runat="server" OnClick="btnQualityCheck_Click" />
                            <asp:Button ID="btnBack" Text="Close" runat="server" OnClick="btnBack_Click" />
                            <asp:Button ID="theBtn1" Text="Yes" CssClass="textstylehidden" runat="server" OnClick="theBtn1_Click" />
                            <asp:Button ID="theBtn" Text="OK" CssClass="textstylehidden" runat="server" OnClick="theBtn_Click" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
