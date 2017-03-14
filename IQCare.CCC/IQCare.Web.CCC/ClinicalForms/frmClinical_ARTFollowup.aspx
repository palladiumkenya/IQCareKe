<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.ARTFollowup"
    Title="Untitled Page" Codebehind="frmClinical_ARTFollowup.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function addDays(id) { 

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
                document.getElementById('<%=ddlAppReason.ClientID%>').value = 0;
                document.getElementById('<%=ddlCounsellorSignature.ClientID%>').value = 0;
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
            document.getElementById('divReasonMissed').className = '';
            document.getElementById('divpreComplain').className = '';
            document.getElementById('divArvsideleft').className = '';
            document.getElementById('divARVsideright').className = '';
            document.getElementById('divTBScreen').className = '';
            document.getElementById('diventerillnessleft').className = '';
            document.getElementById('diventerillnessright').className = '';
            window.print();
            document.getElementById('divReasonMissed').className = 'divborder';
            document.getElementById('divpreComplain').className = 'checkbox';
            document.getElementById('divArvsideleft').className = 'checkboxleft';
            document.getElementById('divARVsideright').className = 'checkboxright';
            document.getElementById('diventerillnessleft').className = 'checkboxright';
            document.getElementById('diventerillnessright').className = 'checkboxleft';
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

        function SendCodeName() {

            var id = document.getElementById('<%=txtvisitDate.ClientID%>').value;
            if (id.length <= 0) {
                return true;
            }
            else {
                CallServer(id, "This is context from client");
                return true;
            }

        }

        function ReceiveServerData(args, context) {
            var a = args;
            var temp = new Array(); temp = a.split('zzzz');
            if (window.ActiveXObject) {
                var obj = new ActiveXObject("MSXML2.DOMDocument"); obj.loadXML(temp[0]);
                var dsRoot = obj.documentElement;
                //CD4 Values
                document.getElementById('<%=txtTestCD4Results.ClientID%>').value = "";
                document.getElementById('<%=txtTestResultsDate.ClientID%>').value = "";
                if (dsRoot.childNodes(0).childNodes(0).firstChild.text > 0) {
                    document.getElementById('<%=txtTestCD4Results.ClientID%>').value = dsRoot.childNodes(0).childNodes(0).firstChild.text;
                    formattedDate = dsRoot.childNodes(0).childNodes(1).firstChild.text;
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    var dd = formattedDate.substr(0, 2);
                    formattedDate = dd + "-" + mm + "-" + yr;
                    document.getElementById('<%=txtTestResultsDate.ClientID%>').value = formattedDate;
                }
                //Viral Load Values 
                document.getElementById('<%=txtmostRecentViralLoad.ClientID%>').value = "";
                document.getElementById('<%=txtmostRecentViralLoadDate.ClientID%>').value = "";
                if (dsRoot.childNodes(1).childNodes(0).firstChild.text > 0) {
                    document.getElementById('<%=txtmostRecentViralLoad.ClientID%>').value = dsRoot.childNodes(1).childNodes(0).firstChild.text;
                    formattedDate = dsRoot.childNodes(1).childNodes(1).firstChild.text;
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    var dd = formattedDate.substr(0, 2);
                    formattedDate = dd + "-" + mm + "-" + yr;
                    document.getElementById('<%=txtmostRecentViralLoadDate.ClientID%>').value = formattedDate;
                }
                //Regimen Type 
                document.getElementById('<%=txtRegimenType.ClientID%>').value = "";
                document.getElementById('<%=txtPrescribedARVStartDate.ClientID%>').value = "";
                if (dsRoot.childNodes(2).childNodes(0).firstChild.text != 0) {
                    document.getElementById('<%=txtRegimenType.ClientID%>').value = dsRoot.childNodes(2).childNodes(0).firstChild.text;
                    formattedDate = dsRoot.childNodes(2).childNodes(1).firstChild.text;
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    formattedDate = mm + "-" + yr;
                    document.getElementById('<%=txtPrescribedARVStartDate.ClientID%>').value = formattedDate;
                }
                //Prior to CD4
                document.getElementById('<%=txtpriorARVsCD4.ClientID%>').value = "";
                document.getElementById('<%=txtpriorARVsCD4Date.ClientID%>').value = "";
                document.getElementById('<%=hdnVisitIDIE.ClientID%>').value = "";
                if (dsRoot.childNodes(3).childNodes(0).firstChild.text > 0) {
                    document.getElementById('<%=txtpriorARVsCD4.ClientID%>').value = dsRoot.childNodes(3).childNodes(0).firstChild.text;
                    document.getElementById('<%=txtpriorARVsCD4Date.ClientID%>').value = dsRoot.childNodes(3).childNodes(1).firstChild.text;
                    document.getElementById('<%=hdnVisitIDIE.ClientID%>').value = dsRoot.childNodes(3).getElementsByTagName("VisitIDIE")[0].firstChild.nodeValue;
                }
                //Height Data 
                var obj1 = new ActiveXObject("MSXML2.DOMDocument"); obj1.loadXML(temp[1]);
                var dsRoot1 = obj1.documentElement;
                document.getElementById('<%=txtphysHeight.ClientID%>').value = "";
                if (dsRoot1.childNodes(0).childNodes(1).firstChild.text == 18) {
                    document.getElementById('<%=txtphysHeight.ClientID%>').value = dsRoot1.childNodes(0).childNodes(0).firstChild.text;
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
                        show('rdopregnantyesno');
                        hide('spdelivery');
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = ""
                        document.getElementById('<%=txtLMPdate.ClientID%>').value = ""
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

        function SenderPregnantLMP() {
            //   var id	= document.getElementById('<%=txtvisitDate.ClientID%>').value;
            //   if (id.length<=0)
            //    {
            //	return true;
            //	}
            //	else
            //	{
            //	CallPregnantLMPServer(id);
            //	return true;
            //	}
        }


        function RecievePregnantData(args, context) {
            //    var temp = new Array(); temp = args.split('zzzz');
            //    if (window.ActiveXObject)
            //    {
            //        var objPreg = new ActiveXObject("MSXML2.DOMDocument");
            //        objPreg.loadXML(temp[2]);
            //        var dsRootPreg=objPreg.documentElement;
            //        if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text != 0);
            //        {   
            //            document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
            //            show('rdopregnantyesno');
            //            hide('spdelivery');
            //        }
            //    }
        }
    </script>
    <div class="center" style="padding: 8px;">
        
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" align="center" width="50%">
                            <label id="lblvdate" class="required right35">
                                *Visit Date:</label>
                            <input id="txtvisitDate" maxlength="11" size="8" name="visitDate" runat="server" />
                            <img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                            <input id="hdnVisitIDIE" type="hidden" value="0" runat="server" />
                        </td>
                        <td class="border pad5 whitebg" align="center">
                            <label class="right25">
                                Last CD4 Count:</label>
                            <input id="txtTestCD4Results" width="12%" readonly="readonly" runat="server" size="4" />
                            <span class="smallerlabel">c/mm<sup>3</sup></span>
                            <label class="margin10">
                                Date:</label>
                            <input readonly="readonly" id="txtTestResultsDate" maxlength="11" size="8" name="TestResultsDate"
                                runat="server" />
                            <img id="ImgCD4" onclick="w_displayDatePicker('<%=txtTestResultsDate.ClientID%>')"
                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                border="0" runat="server" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <label>
                                            CD4 Prior to Starting ARVs:</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txtpriorARVsCD4" runat="server" Width="12%" MaxLength="4"></asp:TextBox>
                                        <span class="smallerlabel">c/mm<sup>3</sup></span>
                                        <label class="margin10">
                                            Date:</label>
                                        <input id="txtpriorARVsCD4Date" maxlength="11" size="8" name="priorARVsCD4Date" runat="server" />
                                        <img onclick="w_displayDatePicker('<%= txtpriorARVsCD4Date.ClientID%>');" height="22"
                                            alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="border pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <label class="right25">
                                            Last Viral Load:</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <input id="txtmostRecentViralLoad" width="12%" readonly="readonly" runat="server"
                                            size="4" />
                                        <span class="smallerlabel">c/ml</span>
                                        <label class="margin10">
                                            Date:</label>
                                        <input readonly="readonly" id="txtmostRecentViralLoadDate" maxlength="11" size="8"
                                            name="mostRecentViralLoadDate" runat="server" />
                                        <img id="ImgMostRecentViralLoad" onclick="w_displayDatePicker('<%= txtmostRecentViralLoadDate.ClientID%>');"
                                            height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                            border="0" runat="server" />
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" align="center">
                            <label class="right35" for="RegimenType">
                                Regimen:</label>
                            <input id="txtRegimenType" readonly="readonly" runat="server" />
                        </td>
                        <td class="border pad5 whitebg" align="center" width="50%">
                            <label class="right35">
                                Current Regimen Began:
                            </label>
                            <input id="txtPrescribedARVStartDate" maxlength="11" size="8" readonly="readonly"
                                runat="server" />
                            <span class="smallerlabel">(MMM-YYYY)</span>
                        </td>
                    </tr>
                    <!-- if gender is female then-->
                    <tr>
                        <td id="tdPregnant" class="form" runat="server" colspan="2">
                            <label class="right10" for="LMP">
                                LMP:</label>
                            <input id="txtLMPdate" maxlength="11" size="8" name="LMP" runat="server" />
                            <img onclick="w_displayDatePicker('<%=txtLMPdate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span id="spdelivery" style="display: none">
                                <label id="lbldelivered">
                                    Delivered since last visit:
                                </label>
                                <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanDelDate'); hide('spanEDD')"
                                    type="radio" value="1" name="delivered" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdoDeliveredNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); hide('spanDelDate')"
                                    type="radio" value="0" name="delivered" runat="server" />
                                <label>
                                    No</label>
                                <span id="spanDelDate" style="display: none">
                                    <label id="lblDelDate" class="right10" for="DelDate">
                                        Delivered Date:</label>
                                    <input id="txtDeliDate" runat="server" maxlength="11" size="11" />
                                    <img id="imgDelDate" onclick="w_displayDatePicker('<%=txtDeliDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                        border="0" />
                                    <span class="smallerlabel">(DD-MMM-YYYY)</span> </span></span>
                            <%--<label id="lblpregnant" class="right10" runat="server">
                            Pregnant:
                            </label>--%>
                            <span id="rdopregnantyesno" style="display: inline">
                                <label id="lblpregnanttmp" class="right10">
                                    Pregnant:
                                </label>
                                <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); "
                                    type="radio" value="Y" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
                                    type="radio" value="N" name="pregnant" runat="server" />
                                <label>
                                    No</label></span> <span id="spanEDD" style="display: none">
                                        <label id="lblEDD" class="right10" for="EDD">
                                            EDD:</label>
                                        <input id="txtEDDDate" runat="server" maxlength="11" size="11" />
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
            <br>
            <h2 class="forms" align="left">
                Adherence</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" width="65%" valign="top">
                            <table width="100%" border="0">
                                <tr>
                                    <td align="right" style="width: 50%">
                                        <label>
                                            Number of Doses Missed:</label>
                                    </td>
                                    <td style="width: 50%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td align="left" style="width: 80%">
                                        <label id="lbldosmissed">
                                            Last Week:</label>
                                        <div id="MissedLastWeek" style="display: inline">
                                            <input type="text" id="txtMissedLastWeek" name="MissedLastWeek" size="2" maxlength="4"
                                                runat="server" />
                                        </div>
                                        <input type="checkbox" id="chMissedLastWeeknone" name="MissedLastWeeknone" size="2"
                                            runat="server" />
                                        <span class="smalllabel">None</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td align="left" style="width: 80%">
                                        <label>
                                            Last Month:</label>
                                        <div id="MissedLastMonth" style="display: inline">
                                            <input type="text" id="txtMissedLastMonth" name="MissedLastMonth" size="2" maxlength="4"
                                                runat="server" />
                                        </div>
                                        <input type="checkbox" id="chMissedLastMonthnone" name="MissedLastMonthnone" runat="server" />
                                        <span class="smalllabel">None</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="border pad5 whitebg" valign="top">
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="right" style="width: 50%">
                                                    <label>
                                                        DOT:</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumDOTPerWeek" runat="server" MaxLength="4" Width="30"></asp:TextBox>
                                                    <span class="smalllabel">times/week</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="right" style="width: 50%">
                                                    <label>
                                                        Home Visits:</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumHomeVisitsPerWeek" runat="server" MaxLength="4" Width="30"></asp:TextBox>
                                                    <span class="smalllabel">times/week</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="right" style="width: 50%">
                                                    <label>
                                                        Support Group:</label>
                                                </td>
                                                <td>
                                                    <input type="checkbox" id="ckSupportGroup" name="SupportGroup" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" valign="top">
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 50%">
                                                    <label>
                                                        Rx was interrupted (unintentional):</label>
                                                </td>
                                                <td align="left">
                                                    <input type="radio" id="rdoInterrupted" name="intStop" value="interrupted" runat="server" onmouseup="up(this);"
                                                        onfocus="up(this);" onclick="down(this); toggle('interruptedDate'); hide('stopDate');" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div id="interruptedDate" style="display: none;">
                                                        <label>
                                                            Date:
                                                        </label>
                                                        <input id="txtInterruptedDate" maxlength="11" size="8" name="InterruptedDate" runat="server" />
                                                        <img onclick="w_displayDatePicker('<%=txtInterruptedDate.ClientID%>');" height="22"
                                                            alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0" /><span
                                                                class="smallerlabel">(DD-MMM-YYYY)</span>
                                                        <input type="text" id="TxtInterruptedNumDays" name="InterruptedNumDays" size="2"
                                                            maxlength="3" runat="server" />
                                                        <span class="smalllabel"># of days</span></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 50%">
                                                    <label>
                                                        Rx was stopped (intentional):</label>
                                                </td>
                                                <td align="left">
                                                    <input type="radio" id="rdostopped" name="intStop" runat="server" onfocus="up(this);" onmouseup="up(this);"
                                                        onclick="down(this); toggle('stopDate'); hide('interruptedDate');" value="stopped" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div id="stopDate" style="display: none">
                                                        <label>
                                                            Date:
                                                        </label>
                                                        <input id="txtstoppedDate" maxlength="11" size="8" name="stoppedDate" runat="server" />
                                                        <img onclick="w_displayDatePicker('<%=txtstoppedDate.ClientID%>');" height="22" alt="Date Helper"
                                                            hspace="5" src="../images/cal_icon.gif" width="22" border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                        <input type="text" id="txtstoppedNumDays" name="stoppedNumDays" size="2" maxlength="3"
                                                            runat="server" />
                                                        <span class="smalllabel"># of days</span></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 50%">
                                                    <label>
                                                        Patient reports taking herbal medications:</label>
                                                </td>
                                                <td align="left">
                                                    <input type="checkbox" id="ckHerbalMeds" name="HerbalMeds" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="border pad5 whitebg" valign="top" align="left" >
                            <label id="lblreasonmissed" class="margin">
                                Reason Missed:</label>
                            <div class="divborder" id="divReasonMissed" nowrap="noWrap">
                                <div>
                                    <asp:CheckBoxList ID="cblAdheranceMissedReason" RepeatLayout="Flow" Width="0%" runat="server">
                                    </asp:CheckBoxList>
                                    <label style="font-weight: bold" for="otherAdheranceMissedReason">
                                        <span id="otherAdheranceMissedReason" style="display: none">Specify:
                                            <input id="txtotherAdheranceMissedReason" size="10" name="otherAdheranceMissedReason"
                                                runat="server" /></span></label> 
                                </div>
                                <div>
                                    <table id="tblReasonMissed" cellpadding="0" cellspacing="1" runat="server">
                                    </table>
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
                Presenting Complaints</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" width="50%" colspan="2" align="left">
                            <label>
                                Presenting Complaints:</label>
                            <input id="chkpresentingComplaintsNone" type="checkbox" checked="checked" value="None"
                                name="presentingComplaintsNone" runat="server" />
                            <span class="smalllabel">None</span><input id="chkpresentingComplaintsNonehidden"
                                checked="checked" value="None" class="textstylehidden" type="checkbox" runat="server" />
                            <div id="presentingComplaintsShow" style="display: none" runat="server">
                                <br />
                                <div class="checkbox" id="divpreComplain" nowrap="nowrap">
                                    <asp:CheckBoxList ID="cblPresentingComplaints" RepeatLayout="Flow" Width="20%" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" width="50%" colspan="2" align="left">
                            <label>
                                TB Screening:</label>
                            <input id="rdoTBScreenPerformed" onmouseup="up(this);" onfocus="up(this);" onclick=" down(this); toggle('TBSCreenSelected');"
                                type="radio" name="Sym" runat="server" />
                            <span id="Span1" class="smallerlabel">Performed</span>
                            <input id="rdoTBScreenNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('TBSCreenSelected');"
                                type="radio" value="Not Documented" name="Sym" runat="server" />
                            <span id="Span2" class="smallerlabel">Not Documented</span>
                            <div id="TBSCreenSelected" style="display: none">
                                <div class="checkbox" id="divTBScreen" nowrap="nowrap">
                                    <asp:CheckBoxList ID="cblTBScreen" RepeatLayout="Flow"  Width="50%" runat="server">
                                    </asp:CheckBoxList>
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
                        <td class="border pad5 whitebg formcenter" colspan="2" valign="bottom"> 
                            <label>
                                Temp:</label>
                            <asp:TextBox ID="txtphysTemp" runat="server" MaxLength="4" Width="5%"></asp:TextBox>
                            <span class="smallerlabel">C</span>
                            <label class="margin5">
                                RR:</label>
                            <asp:TextBox ID="txtphysRR" runat="server" MaxLength="4" Width="5%"></asp:TextBox>
                            <span class="smallerlabel">bpm</span>
                            <label class="margin5">
                                HR:</label>
                            <asp:TextBox ID="txtphysHR" runat="server" MaxLength="4" Width="5%"></asp:TextBox>
                            <span class="smallerlabel">bpm</span>
                            <label class="margin5">
                                BP:</label>
                            <asp:TextBox ID="txtphysBPSystolic" runat="server" MaxLength="4" Width="5%"></asp:TextBox>/
                            <asp:TextBox ID="txtphysBPDiastolic" runat="server" MaxLength="4" Width="5%"></asp:TextBox>
                            <span class="smallerlabel">(mm/Hg)</span>
                            <label id="lblHT" class="margin5">
                                HT:</label>
                            <asp:TextBox ID="txtphysHeight" runat="server" MaxLength="4" Width="5%"></asp:TextBox>
                            <span class="smallerlabel">cm</span>
                            <label id="lblWT" class="margin5">
                                WT:</label>
                            <asp:TextBox ID="txtphysWeight" runat="server" MaxLength="4" Width="5%"></asp:TextBox>
                            <span class="smallerlabel">kg</span> 
                              <label class="margin5">
                                BMI :</label>                            
                            <input id="txtanotherbmi" size="6" name="anotherbmi" runat="server" readonly="readonly"/>
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
                        <td class="border pad5 whitebg" valign="top" colspan="2" align="left" >
                            <label class="right20">
                                ARV Side Effects:</label>
                            <input id="rdoARVSideEffectsNone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('sideEffectsSelected');"
                                type="radio" value="none" name="SideEffects" runat="server" checked />
                            <span id="lblARVNone" class="smallerlabel">None</span>
                            <input id="rdoARVSideEffectsNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('sideEffectsSelected');"
                                type="radio" value="Not Documented" name="SideEffects" runat="server" />
                            <span id="lblARVNotdocumented" class="smallerlabel">Not Documented</span>
                            <input id="rdoARVSideEffectsYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('sideEffectsSelected');"
                                type="radio" value="show" name="SideEffects" runat="server" />
                            <span id="lblARVSideEffect" class="smallerlabel">Enter ARV Side Effects</span>
                            <br />
                            <div id="sideEffectsSelected" style="display: none">
                                <div class="checkboxleft" id="divArvsideleft" nowrap="noWrap">
                                    <asp:CheckBoxList ID="cblARVSideEffectleft" RepeatLayout="Flow" Width="50%" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                <div class="checkboxright" id="divARVsideright" nowrap="noWrap">
                                    <asp:CheckBoxList ID="cblARVSideEffectright" RepeatLayout="Flow" Width="50%" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" valign="top" colspan="2">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="left">
                                        <label class="right20">
                                            OIs or AIDS Defining Illnesses:</label>
                                        <input id="rdoOIsAIDsIllnessNone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('pultb'); hide('assocSelected'); "
                                            type="radio" value="none" name="assocNone" runat="server" />
                                        <span id="lblOIAIDNone" class="smallerlabel">None</span>
                                        <input id="rdoOIsAIDsIllnessNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('pultb'); hide('assocSelected');"
                                            type="radio" value="Not Documented" name="assocNone" runat="server" />
                                        <span id="lblOIAIDNotdocumented" class="smallerlabel">Not Documented</span>
                                        <input id="rdoOIsAIDsIllnessYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('assocSelected');"
                                            type="radio" value="show" name="assocNone" runat="server" />
                                        <span id="lblOIAIDillness" class="smallerlabel">Enter Illnesses</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="assocSelected" style="display: none">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 49%">
                                                            <div class="checkboxright" id="diventerillnessleft" nowrap="noWrap" style="width: 96%">
                                                                <table id="tblOIsAIDsleft" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                                    runat="server">
                                                                    <tbody style="width: 50%">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td style="width: 49%">
                                                            <div class="checkboxleft" id="diventerillnessright" nowrap="noWrap" style="width: 96%">
                                                                <table id="tblOIsAIDsright" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                                    runat="server">
                                                                    <tbody style="width: 50%">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
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
                Assessment</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg formcenter" valign="top" colspan="2" align="left">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label id="lblassessment">
                                            Clinical Assessment:</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        <div id="assessment">
                                            <span class="smalllabel">
                                                <asp:CheckBoxList ID="cblAssessment" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                    Width="0%" CellPadding="0" CellSpacing="0">
                                                </asp:CheckBoxList>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" align="center" valign="middle" width="50%">
                            <label id="lblWHO" class="right35">
                                WHO Stage:</label>
                            <asp:DropDownList ID="ddlWHOStage" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="border pad5 whitebg" align="center" valign="middle" width="50%">
                            <label id="lblWAB" class="right35">
                                WAB Stage:</label>
                            <asp:DropDownList ID="ddlphysWABStage" runat="server">
                                <asp:ListItem Value="0" Selected="true">Select</asp:ListItem>
                                <asp:ListItem Value="84">Working</asp:ListItem>
                                <asp:ListItem Value="85">Ambulatory</asp:ListItem>
                                <asp:ListItem Value="86">Bedridden</asp:ListItem>
                            </asp:DropDownList>
                            <br>
                        </td>
                    </tr>
                </tbody>
            </table>
            <h2 class="forms" align="left">
                Plan</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border whitebg formcenter" valign="middle">
                            <label id="lblarvplan">
                                *ARV Therapy:</label>
                            <select id="lstclinPlanFU" onchange="specifyChangeStop(this.id);" name="clinPlanFU"
                                runat="server">
                                <option value="0" selected="selected">Select</option>
                                <option value="94">Treatment not indicated now</option>
                                <option value="95">Continue current treatment</option>
                                <option value="96">Restart treatment</option>
                                <option value="97">Start new treatment</option>
                                <option value="98">Change regimen</option>
                                <option value="99">Stop treatment</option>
                            </select>
                            <div id="arvTherapyChange" style="display: none">
                                <label class="required margin10">
                                    *Change Regimen Reason:</label>
                                <asp:DropDownList ID="ddlArvTherapyChangeCode" runat="server">
                                </asp:DropDownList>
                                <div id="otherarvTherapyChangeCode" style="display: none">
                                    <label class="required right45" for="arvTherapyChangeCodeOtherName">
                                        *Specify:</label>
                                    <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName"
                                        runat="server"></div>
                            </div>
                            <div id="arvTherapyStop" style="display: none">
                                <label id="lblrARTdate" class="required">
                                    *Date ART Ended</label>
                                <input id="txtARTEndeddate" runat="server" maxlength="11" size="10" name="txtARTEndeddate" />
                                <img id="imgdate" onclick="w_displayDatePicker('<%=txtARTEndeddate.ClientID%>');"
                                    height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                    border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                                <label class="required">
                                    *Stop Regimen Reason:</label>
                                <asp:DropDownList ID="ddlArvTherapyStopCode" runat="server">
                                </asp:DropDownList>
                                <div id="otherarvTherapyStopCode" style="display: none">
                                    <label class="required right15" for="arvTherapyStopCodeOtherName">
                                        *Specify:</label>
                                    <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName"
                                        runat="server"></div>
                            </div>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlPanelNotes" runat="server" Visible="false">
                        <tr>
                            <td class="border pad5 whitebg formcenter" id="PlanNotesID" runat="server" colspan="2">
                                <label id="lblNotes" style="vertical-align: top">
                                    Notes</label>
                                <textarea id="MulttxtclinPlanNotes" name="clinPlanNotes" rows="4" cols="160" runat="server"></textarea>
                            </td>
                        </tr>
                    </asp:Panel>
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
                            <asp:TextBox ID="txtClinicalNotes" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
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
                                <option value="30">4 weeks</option>
                                <option value="60">2 months</option>
                                <option value="90">3 months</option>
                                <option value="180">6 months</option>
                            </select>
                        </td>
                        <td class="border pad5 whitebg" align="center">
                            <label class="right40">
                                Appointment Reason:</label>
                            <asp:DropDownList ID="ddlAppReason" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" align="center" valign="top">
                            <label class="right50">
                                Specify Date:</label>
                            <input id="txtappDate" maxlength="11" size="8" name="appDate" runat="server"/>
                            <img onclick="w_displayDatePicker('<%=txtappDate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0"/><span class="smallerlabel"
                                    id="appDatespan">(DD-MMM-YYYY)</span>
                        </td>
                        <td class="border pad5 whitebg" align="center" valign="middle">
                            <label class="right40">
                                Signature:</label>
                            <asp:DropDownList ID="ddlCounsellorSignature" runat="server">
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
                        <td class="pad5 center" colspan="2">
                            <br/>
                            <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnsave_Click" />
                            <asp:Button ID="btndataquality" Text="Data Quality check" runat="server" OnClick="btndataquality_Click" />
                            <asp:Button ID="btnclose" Text="Close" runat="server" OnClick="btnclose_Click" />
                            <asp:Button ID="btnOk" Text="OK" CssClass="textstylehidden" runat="server" OnClick="theBtn_Click" />
                            <asp:Button ID="theBtnDQ" Text="DQ" CssClass="textstylehidden" runat="server" OnClick="theBtnDQ_Click" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
