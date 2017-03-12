<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="False"
    AutoEventWireup="True" Inherits="IQCare.Web.Clinical.Enrolment"
    Title="Untitled Page" Codebehind="frmClinical_Enrolment.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function CalcualteAge(txtAge, txtmonth, txtDT1, txtDT2, flag) {
            var YR1 = document.getElementById(txtDT1).value.toString().substr(7, 4);
            var YR2 = document.getElementById(txtDT2).value.toString().substr(7, 4);

            var mm1 = document.getElementById(txtDT1).value.toString().substr(3, 3);
            var mm2 = document.getElementById(txtDT2).value.toString().substr(3, 3);

            var dd1 = document.getElementById(txtDT1).value.toString().substr(0, 2); 
            var dd2 = document.getElementById(txtDT2).value.toString().substr(0, 2);

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
            var nmm2;
            switch (mm2.toLowerCase()) {
                case "jan": nmm2 = "01";
                    break;
                case "feb": nmm2 = "02";
                    break;
                case "mar": nmm2 = "03";
                    break;
                case "apr": nmm2 = "04";
                    break;
                case "may": nmm2 = "05";
                    break;
                case "jun": nmm2 = "06";
                    break;
                case "jul": nmm2 = "07";
                    break;
                case "aug": nmm2 = "08";
                    break;
                case "sep": nmm2 = "09";
                    break;
                case "oct": nmm2 = "10";
                    break;
                case "nov": nmm2 = "11";
                    break;
                case "dec": nmm2 = "12";
                    break;
            }
            var dt1 = nmm1 + "/" + dd1 + "/" + YR1;
            var dt2 = nmm2 + "/" + dd2 + "/" + YR2;

            var val1 = dateDiff("d", dt1, dt2, "", "") / 365;
            var val2 = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
            if (val2 > val1) {
                document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365)) - 1;
                var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365) - 1;
                document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
            }
            else {
                document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
                var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365);
                document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
            }

            if (flag = 1) {
                if (val2 > val1) {
                    var age, month;
                    age = Math.round((dateDiff("d", dt1, dt2, "", "") / 365)) - 1;
                    var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365) - 1;
                    month = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
                    if (age < 5) {
                        show('HIVStatusChild');
                    }
                    else {
                        hide('HIVStatusChild');
                    }

                }
                else {
                    var age, month;
                    age = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
                    var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365);
                    month = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
                    if (age < 5) {
                        show('HIVStatusChild');
                    }
                    else {
                        hide('HIVStatusChild');
                    }
                }
            }
        }
        //Function for Potential barriers   
        function CheckItem(checkBoxList, Value) {
            var chkBoxList = document.getElementById(checkBoxList);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = chkBoxList.getElementsByTagName("label");

            if (Value == 54) {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (arrayOfCheckBoxLabels[i].innerText != 'None') {
                        chkBoxCount[i].checked = false;
                    }
                }
            }
            if (Value != 54) {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (arrayOfCheckBoxLabels[i].innerText == 'None') {
                        chkBoxCount[i].checked = false;
                    }
                }
            }

        }
        function Button1_onclick() {

        }

        function Button2_onclick() {

        }

        function Button1_onclick() {

        }


        function jsAreaClose(id) {
            document.getElementById(id).style.display = 'none';
        }

        var pickedUp = new Array("", false);
        function getReadyToMove(element, evt) {
            pickedUp[0] = element;
            pickedUp[1] = true;
        }

        function checkLoadedObjects(evt) {
            if (pickedUp[1] == true) {
                var currentSelection = document.getElementById(pickedUp[0]);

                currentSelection.style.position = "absolute";
                currentSelection.style.top = (evt.clientY + 1) + "px";
                currentSelection.style.left = (evt.clientX + 1) + "px";
            }
        }

        function dropLoadedObject(evt) {
            if (pickedUp[1] == true) {
                var currentSelection = document.getElementById(pickedUp[0]);
                currentSelection.style.position = "absolute";
                currentSelection.style.top = (evt.clientY + 1) + "px";
                currentSelection.style.left = (evt.clientX + 1) + "px";

                pickedUp = new Array("", false);
            }

        }
        function WindowPrint() {
            window.print();
        }
    </script>
    <div onmousemove="javascript:checkLoadedObjects(event);" ondblclick="javascript:dropLoadedObject(event);">
      <%--  <asp:ScriptManager ID="mst" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        </asp:ScriptManager>--%>
        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
            <ContentTemplate>
                <div class="center" style="padding: 8px;">
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%" align="left">
                                        <label id="lblenroldate" class="required" for="enrollmentDate">
                                            HIV Care Enrollment Date:</label>
                                        <asp:TextBox ID="txtenrollmentDate" MaxLength="11" runat="server" Width="25%"></asp:TextBox>
                                        <img onclick="w_displayDatePicker('<%= txtenrollmentDate.ClientID %>');" height="22"
                                            alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                    <td class="border pad5 whitebg" width="50%" align="left">
                                        <label class="margin20" for="AgeatEnrolment">
                                            Age at Enrollment:</label>
                                        <asp:TextBox ID="txtageEnrollmentYears" ReadOnly="true" MaxLength="2" runat="server"
                                            Width="10%"></asp:TextBox>
                                        <span class="smallerlabel">yrs</span>
                                        <asp:TextBox ID="txtageEnrollmentMonths" ReadOnly="true" MaxLength="2" runat="server"
                                            Width="10%"></asp:TextBox>
                                        <span class="smallerlabel">mths</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%" style="height: 34px" align="left">
                                        <label class="margin20" for="PatientTransferin">
                                            Transfer In:</label>
                                        <asp:CheckBox ID="chkPatientTransferin" runat="server"></asp:CheckBox>
                                    </td>
                                    <td class="border pad5 whitebg" width="50%" style="height: 34px" align="left">
                                        <label class="margin20" for="LPTFPatientTransferfrom">
                                            LPTF Transferred From:</label>
                                        <asp:DropDownList ID="ddlptfTransfer" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                    <div class="border center formbg">
                        <br>
                        <h2 class="forms" align="left">
                            Demographic and Prior HIV History</h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" align="center" colspan="2" style="height: 58px">
                                        <label for="contactperson">
                                            Does contact person know your HIV Status?</label>
                                        <input id="rbtnknownHIVStatusYes" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('discuss');"
                                            type="radio" value="Y" name="knownHIVStatus" />
                                        <label for="y">
                                            Yes</label>
                                        <input id="rbtnknownHIVStatusNo" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('discuss');"
                                            type="radio" value="N" name="knownHIVStatus" />
                                        <label for="n">
                                            No</label>
                                        <asp:TextBox ID="txtknownHIVStatus" CssClass="textstylehidden" runat="server"></asp:TextBox>
                                        <div class="margin80" id="discuss" style="display: none">
                                            <label for="hivstatus">
                                                May we discuss your status with this person?</label>
                                            <input id="rbtnHIVdiscussStatusYes" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                type="radio" value="Y" name="discussStatus" />
                                            <label for="y">
                                                Yes</label>
                                            <input id="rbtnHIVdiscussStatusNo" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                type="radio" value="N" name="discussStatus" />
                                            <label for="n">
                                                No</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" align="center" colspan="2">
                                        <label class="margin20" for="educationallevel">
                                            Educational Level:</label>
                                        <asp:DropDownList ID="ddeducationLevel" runat="server">
                                        </asp:DropDownList>
                                        <div id="otherddeducationLevel" style="display: none;" class="margin20">
                                            <label>
                                                Specify:</label>
                                            <asp:TextBox ID="txteducationother" Width="10%" runat="server"></asp:TextBox>
                                        </div>
                                        <label class="margin20" for="literacy">
                                            Literacy:</label>
                                        <asp:DropDownList ID="ddLiterarcy" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" style="width: 50%; height: 175px;" nowrap="nowrap">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label for="receiveHIVCare">
                                                        Have you ever received care for HIV/AIDS?</label>
                                                </td>
                                                <td>
                                                    <input id="rbtnprevHIVCareYes" onmouseup="up(this);" onfocus="up(this);" onclick="show('caretype');down(this);"
                                                        type="radio" value="1" name="prevHIVCare" runat="server" />
                                                    <label for="y">
                                                        Yes</label>
                                                    <input id="rbtnPrevHIVCareNo" onmouseup="up(this);" onfocus="up(this);" onclick="hide('caretype');down(this);"
                                                        type="radio" value="0" name="prevHIVCare" runat="server" />
                                                    <label for="n">
                                                        No</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div id="caretype" style="display: none">
                                                        <div class="divborder">
                                                            <table width="100%" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <label for="specify">
                                                                            Specify type (check all that apply):</label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table id="tblreceiveHIVCare" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                                            runat="server">
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg" valign="top" width="50%" style="height: 175px">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <label for="ARTs">
                                                        Have you ever been on ARTs?</label>
                                                </td>
                                                <td nowrap="nowrap">
                                                    <input id="rbtnprevARTYes" onmouseup="up(this);" onfocus="up(this);" onclick="show('artSponsor'); show('medRecordsdiv');down(this);"
                                                        type="radio" value="Yes" name="prevART" runat="server" />
                                                    <label for="y">
                                                        Yes</label>
                                                    <input id="rbtnprevARTNo" onmouseup="up(this);" onfocus="up(this);" onclick="hide('artSponsor');hide('medRecordsdiv');down(this);"
                                                        type="radio" value="No" name="prevART" runat="server" />
                                                    <label for="n">
                                                        No</label>
                                                    <input id="rbtnprevARTUnknown" onmouseup="up(this);" onfocus="up(this);" onclick="hide('artSponsor');hide('medRecordsdiv');down(this);"
                                                        type="radio" value="Unknown" name="prevART" runat="server" />
                                                    <label for="ukwn">
                                                        Unknown</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap" colspan="2">
                                                    <div id="artSponsor" style="display: none" align="left">
                                                        <div class="divborder">
                                                            <label class="pad5" id="lblSponsor" runat="server" for="Specify">
                                                                Specify(select all that apply):</label><br>
                                                            <table id="tblArtSponsor" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                                runat="server">
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap" colspan="2">
                                                    <div class="pad5" id="medRecordsdiv" style="display: none">
                                                        <br />
                                                        <label for="Medrecords">
                                                            May we have a copy of your medical records today?</label>
                                                        <input id="rbtnmedRecordsYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" type="radio"
                                                            value="0" name="medRecords" runat="server" />
                                                        <label for="y">
                                                            Yes</label>
                                                        <input id="rbtnmedRecordsNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" type="radio"
                                                            value="1" name="medRecords" runat="server" />
                                                        <label for="n">
                                                            No</label></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" align="center" colspan="2" style="height: 40px">
                                        <label for="Empstatus">
                                            Employment Status:</label>
                                        <asp:DropDownList ID="ddemploymentstatus" runat="server">
                                        </asp:DropDownList>
                                        <label class="margin50" for="occupation">
                                            Occupation:</label>
                                        <asp:DropDownList ID="ddoccuption" runat="server">
                                        </asp:DropDownList>
                                        <label class="margin50" for="monthlyIncome">
                                            Monthly Income:</label>
                                        <asp:TextBox ID="txtmonthlyIncome" Style="text-align: right" MaxLength="8" Width="10%"
                                            runat="server"></asp:TextBox>
                                        <span class="smalllabel"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="numChildren">
                                                        How many children do you have?</label>
                                                </td>
                                                <td style="width: 50%" align="left"  >
                                                    <asp:TextBox ID="txtChildren" MaxLength="2" size="3" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 70%" align="right">
                                                    <label for="numPeopleHousehold">
                                                        How many people are in your household?</label>
                                                </td>
                                                <td style="width: 30%" align="left">
                                                    <asp:TextBox ID="txtPeopleHousehold" MaxLength="2" size="3" name="numPeopleHousehold"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="width: 70%" align="left">
                                                    <label for="distanceTravelled">
                                                        Distance travelled from residence to point of service:</label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtdistanceTravelled" MaxLength="3" size="3" runat="server" Width="59px"></asp:TextBox>
                                                    <span class="smalllabel">Kilometers</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 60%" align="right">
                                                    <label for="timeTravelled">
                                                        How long did it take you to get here today?</label>
                                                </td>
                                                <td style="width: 40%" align="left">
                                                    <asp:TextBox ID="txttimeTravelled" MaxLength="4" size="4" runat="server"></asp:TextBox>
                                                    <asp:DropDownList ID="ddtimetravelledUnits" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%">
                                        <label for="HIVStatus">
                                            HIV Status:</label>
                                        <asp:DropDownList ID="ddHIVStatus" runat="Server">
                                        </asp:DropDownList>
                                        <div id="HIVStatusChild" style="display: none">
                                            <label id="lblHIVStatus_Child" style="text-align: right" for="HIVposmother">
                                                Child born to HIV positive mother:
                                            </label>
                                            <asp:DropDownList ID="ddHIVStatus_Child" runat="server">
                                                <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="2">Unknown</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td class="border pad5 whitebg" width="50%" nowrap="nowrap">
                                        <label for="HIVdiscl">
                                            HIV disclosure:</label>
                                        <input id="rbtnHIVdisclosureYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);show('showHIVdisclosureName');"
                                            type="radio" value="0" name="HIVdisclosure" runat="server" />
                                        <label for="Dis">
                                            Disclosed</label>
                                        <input id="rbtnHIVdisclosureNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('showHIVdisclosureName');"
                                            type="radio" value="1" name="HIVdisclosure" runat="server" />
                                        <label for="NotDis">
                                            Not Disclosed</label>
                                        <div id="showHIVdisclosureName" style="display: none">
                                            <br>
                                            <label for="WhoTo">
                                                Who to:</label><br>
                                            <div class="checkbox" align="left">
                                                <table id="tblHIVdisclosure" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                    runat="server">
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" align="left">
                                        <label for="numHouseholdHIVTest">
                                            How many member(s) of your household do you know have done an HIV Test?</label>
                                        <asp:TextBox ID="txtHouseholdHIVTest" MaxLength="2" runat="server"> </asp:TextBox>
                                    </td>
                                    <td class="border pad5 whitebg" valign="top" align="left">
                                        <label for="numHouseholdHIVPositive">
                                            How many member(s) of your household do you know have tested positive for HIV?</label>
                                        <asp:TextBox ID="txtHouseholdHIVPositive" MaxLength="2" runat='server'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" align="left">
                                        <label for="numHouseholdHIVDied">
                                            How many member(s) of your household do you know are deceased from HIV?</label>
                                        <asp:TextBox ID="numHouseholdHIVDied" MaxLength="2" size="3" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="border pad5 whitebg" valign="top" align="left">
                                        <label for="Membershipsupportgroup">
                                            Membership in HIV support group:</label>
                                        <input id="rbtnsupportGroupYes" type="radio" value="1" name="supportGroup" runat="server" onmouseup="up(this);"
                                            onfocus="up(this);" onclick="down(this); show('supportName');" />
                                        <label for="Y">
                                            Yes</label>
                                        <input id="rbtnsupportGroupNo" type="radio" value="0" name="supportGroup" runat="server" onmouseup="up(this);"
                                            onfocus="up(this);" onclick="down(this); hide('supportName');" />
                                        <label for="N">
                                            No</label>
                                        <div id="supportName" style="display: none">
                                            <br>
                                            <label for="supportGroupName">
                                                Specify:</label>
                                            <asp:TextBox ID="txtsupportGroupName" Width="50%" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" width="50%" nowrap="nowrap">
                                        <strong>Patient Referred From:</strong><br>
                                        <div class="divborder" align="left">
                                            <!--<asp:RadioButtonList ID="rdopatientreferredby" Width=1% runat="server" RepeatLayout="Flow">
                                         </asp:RadioButtonList>-->
                                            <asp:Panel ID="pnlprb" runat="server">
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td class="border pad5 whitebg" valign="top" width="50%" nowrap="nowrap">
                                        <strong>Potential Barriers to successful HIV care (select all that apply):</strong>
                                        <div class="divborder" align="left">
                                            <asp:CheckBoxList ID="cblbarrierstocare2" runat="server" RepeatLayout="Flow" Width="1%">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pad5 center" colspan="2" style="height: 15px">
                                        <asp:Button ID="btnFamilyInfo" Visible="false" runat="server" Text="Family Information"
                                            OnClick="btnFamilyInfo_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form" align="center" colspan="2" style="height: 20px">
                                        <label for="NameofInterviewer">
                                            Name of Interviewer:</label>
                                        <asp:DropDownList ID="lstenrollmentInterviewerName" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form" colspan="2">
                                        <asp:Panel ID="pnlCustomList" runat="server" Visible="false" Height="100%" Width="100%"
                                            Wrap="true">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <asp:TextBox ID="txtSysDate" CssClass="textstylehidden" runat="server"> </asp:TextBox>
                                    <td class="pad5 center" colspan="2" style="height: 53px">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btncomplete" runat="server" Text="Data Quality check" OnClick="btncomplete_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnOk" CssClass="textstylehidden" runat="server" Text="Ok" OnClick="btnOk_Click" />
                                        <asp:Button ID="theBtnDQ" Text="DQ" CssClass="textstylehidden" runat="server" OnClick="theBtnDQ_Click" />
                                        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnOk"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btncomplete"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="theBtnDQ"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script language="javascript" type="text/javascript">
        if (typeof (Sys) !== 'undefined')
            Sys.Application.notifyScriptLoaded();
        var pageManager = Sys.WebForms.PageRequestManager.getInstance();
        var uiId = '';
        pageManager.add_beginRequest(myBeginRequestCallback);
        function myBeginRequestCallback(sender, args) {
            var postbackElem = args.get_postBackElement();
            uiId = postbackElem.id;
            postbackElem.disabled = true;

        }
        pageManager.add_endRequest(myEndRequestCallback);
        function myEndRequestCallback(sender, args) {
            var status = '<%=Request.QueryString["name"]%>';
            if (status != 'Edit') {
                fnshow();
            }
        }
    </script>
</asp:Content>
