<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.PatientRecordCTC"
    Title="Untitled Page" Codebehind="frmClinical_PatientRecordCTC.aspx.cs" %>
     <%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function WindowPrint() {

            document.getElementById('divReferTo').className = '';
            document.getElementById('<%=divIllness1.ClientID %>').className = '';
            document.getElementById('<%=divIllness2.ClientID %>').className = '';
            document.getElementById('divArvReasonprint').className = '';
            window.print();
            document.getElementById('divReferTo').className = 'checkboxLeft';
            document.getElementById('<%=divIllness1.ClientID %>').className = 'checkboxRight';
            document.getElementById('<%=divIllness2.ClientID %>').className = 'checkboxLeft';
            document.getElementById('divArvReasonprint').className = 'checkboxLeft';
        }

        function funHtWtValidate(txtId) {
            var v1 = document.getElementById(txtId).value;
            var pos1 = v1.indexOf(".");
            var pos2 = v1.indexOf(".", pos1 + 1);
            if (pos2 > 0)
                document.getElementById(txtId).value = v1.substr(0, v1.length - 1);

            if ((pos1 > -1) && (v1.charAt(pos1 + 2) != ''))
                document.getElementById(txtId).value = v1.substr(0, v1.length - 1);
        }

        function fnClearTextbox(txtId) {
            var Id = 'ctl00_IQCareContentPlaceHolder_' + txtId;
            document.getElementById(Id).value = "";
        }
        function pageLoad() {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndAJAXRequestHandler);
        }
        function EndAJAXRequestHandler(sender, args) {

            if (args.get_error() != undefined && args.get_error().httpStatusCode == '500') {
                var errorMessage = args.get_error().message;
                args.set_errorHandled(true);
                alert("IQCare Application Framework encountered an unrecoverable error:\n" + errorMessage + "\n\nPlease report this error to the support team.");
            }

        }

        function OnHeightSuccess(result) {
            var val = new Array();
            val = result.split("*");

            if (val[0] == 0)
                val[0] = "";


            document.getElementById('<%=txtHeight.ClientID %>').value = (val[0]);
            document.getElementById('<%=txtCD4Count.ClientID %>').value = (val[1]);
            if (val[2] != '01-Jan-1900')
                document.getElementById('<%=txtCD4Date.ClientID %>').value = (val[2]);

            document.getElementById('<%=txtRegimen.ClientID %>').value = (val[3]);

            if (val[4] != '01-Jan-1900')
                document.getElementById('<%=txtRegimenDate.ClientID %>').value = (val[4]);
        }
        //**********************************************
        function OnHeightError(error) {
           
        }
        function fnGetHeightCD4Regimen() {
            var VisitDate = document.getElementById('<%=txtvisitDate.ClientID%>').value;
            if (VisitDate.length >= 11) {
                // var result = frmClinical_PatientRecordCTC.GetHeightCD4Regimen(VisitDate).value;
                PageMethods.GetHeightCD4Regimen(VisitDate, OnHeightSuccess, OnHeightError);
//                var val = new Array();
//                val = result.split("*");

//                if (val[0] == 0)
//                    val[0] = "";


//                document.getElementById('<%=txtHeight.ClientID %>').value = (val[0]);
//                document.getElementById('<%=txtCD4Count.ClientID %>').value = (val[1]);
//                if (val[2] != '01-Jan-1900')
//                    document.getElementById('<%=txtCD4Date.ClientID %>').value = (val[2]);

//                document.getElementById('<%=txtRegimen.ClientID %>').value = (val[3]);

//                if (val[4] != '01-Jan-1900')
//                    document.getElementById('<%=txtRegimenDate.ClientID %>').value = (val[4]);
            }
            else {
                document.getElementById('<%=txtHeight.ClientID%>').value = "";
            }

        }
        function fnsubmit() {
            document.forms[0].submit();
        }
        function funShowWhyEligible() {
            document.getElementById('divWhyEligible').style.display = 'inline';
        }

        function DeliverDate() {
            if (document.getElementById('divChkDelivered').style.display == 'none') {
                document.getElementById('divChkDelivered').style.display = 'inline';
            }
            else {
                document.getElementById('divChkDelivered').style.display = 'none';
                document.getElementById('ctl00_IQCareContentPlaceHolder_txtDOB').value = '';
            }
        }

        function ShowHideTBStatus() {
            if (document.getElementById('<%=ddlTBStatus.ClientID%>').selectedIndex == 2) {
                alert("You have selected INH as TBStatus");
            }
            if (document.getElementById('<%=ddlTBStatus.ClientID%>').selectedIndex == 6)
                document.getElementById('divTBStatus').style.display = 'inline'
            else {
                document.getElementById('divTBStatus').style.display = 'none'
                document.getElementById('<%=txtTBID.ClientID %>').value = "";
            }
        }

        function ShowHideARVStaus(ARVAdhReasonCount, ARVReasonCount) //--used
        {
            document.getElementById('<%=ddlARVReason.ClientID%>').value = 0;
            document.getElementById('<%=ddlWhyEligible.ClientID %>').value = 0;

            for (i = 0; i <= 101; i++) {
                if (document.getElementById('ctl00_IQCareContentPlaceHolder_ARVReason' + i) != null)
                    document.getElementById('ctl00_IQCareContentPlaceHolder_ARVReason' + i).checked = false;
            }

            for (i = 0; i <= 54; i++) {
                if (document.getElementById('ctl00_IQCareContentPlaceHolder_ARVAdhReason' + i) != null)
                    document.getElementById('ctl00_IQCareContentPlaceHolder_ARVAdhReason' + i).checked = false;
            }

            if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 0) {
                document.getElementById('divARVReason').style.display = 'none';
                document.getElementById('divWhyEligible').style.display = 'none';
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'none';
            }
            else if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 1) {
                document.getElementById('divARVReason').style.display = 'inline';
                document.getElementById('divWhyEligible').style.display = 'none';
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'none';
            }
            else if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 2) {
                document.getElementById('divARVReason').style.display = 'none';
                document.getElementById('divWhyEligible').style.display = 'inline'
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'none';

            }
            else if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 3)// 
            {
                document.getElementById('divARVReason').style.display = 'none';
                document.getElementById('divWhyEligible').style.display = 'none'
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';
                document.getElementById('divARVAdhStatus').style.display = 'inline';
                document.getElementById('divARVAdhReason').style.display = 'none';
                document.getElementById('divLblARVReason2').style.display = 'none';
                document.getElementById('divARVReason2').style.display = 'none';
                document.getElementById('<%=rdoAdhStatusGood.ClientID%>').checked = false;
                document.getElementById('<%=rdoAdhStatusNotDoc.ClientID%>').checked = false;
                document.getElementById('<%=rdoAdhStatusPoor.ClientID%>').checked = false;
            }
            else if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 4) {
                document.getElementById('divWhyEligible').style.display = 'none'
                document.getElementById('divARVAdhStatus').style.display = 'inline';
                document.getElementById('divARVAdhReason').style.display = 'none';
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';
                document.getElementById('divARVReason').style.display = 'none';
                document.getElementById('divLblARVReason2').style.display = 'inline';
                document.getElementById('divARVReason2').style.display = 'inline';
                document.getElementById('<%=rdoAdhStatusGood.ClientID%>').checked = false;
                document.getElementById('<%=rdoAdhStatusNotDoc.ClientID%>').checked = false;
                document.getElementById('<%=rdoAdhStatusPoor.ClientID%>').checked = false;
            }
            else if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 5) {
                document.getElementById('divARVAdhStatus').style.display = 'inline';
                document.getElementById('divARVAdhReason').style.display = 'none';
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';
                document.getElementById('divARVReason').style.display = 'none';
                document.getElementById('divLblARVReason2').style.display = 'inline';
                document.getElementById('divARVReason2').style.display = 'inline';
                document.getElementById('divWhyEligible').style.display = 'none';

                document.getElementById('<%=rdoAdhStatusGood.ClientID%>').checked = false;
                document.getElementById('<%=rdoAdhStatusNotDoc.ClientID%>').checked = false;
                document.getElementById('<%=rdoAdhStatusPoor.ClientID%>').checked = false;
            }
            else if ((document.getElementById('<%=ddlARVStatus.ClientID%>').value) == 6) {
                document.getElementById('divARVReason').style.display = 'none';
                document.getElementById('divWhyEligible').style.display = 'none';
                document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'none';
            }
        }

        function ShowHideARVAdhReason(obj) {

            for (i = 0; i <= 54; i++) {
                if (document.getElementById('ctl00_IQCareContentPlaceHolder_ARVAdhReason' + i) != null)
                    document.getElementById('ctl00_IQCareContentPlaceHolder_ARVAdhReason' + i).checked = false;
            }

            if (obj.value == 'P')
                document.getElementById('divARVAdhReason').style.display = 'inline'
            else if (obj.value == 'G')
                document.getElementById('divARVAdhReason').style.display = 'none'
            else if (obj.value == 'ND')
                document.getElementById('divARVAdhReason').style.display = 'none'
        }

        function HideShowPregnant(obj) {
            if (obj.value == 'PY') {
                document.getElementById('divPregnant').style.display = 'inline'
            }
            else {
                document.getElementById('divPregnant').style.display = 'none';
                document.getElementById('<%=txtEDD.ClientID%>').value = '';
            }
        }

        function ShowHideProbSymp(obj) {
            if (obj.value == 'illness')
                document.getElementById('probSymptSideEffect').style.display = 'inline'
            else
                document.getElementById('probSymptSideEffect').style.display = 'none'
        }

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
            newDays = document.getElementById('<%= ddlNextAppoint.ClientID %>').value;

            if (newDays == 0) {
                document.getElementById('<%=txtAppDate.ClientID%>').value = "";
                document.getElementById('<%=ddlAppointmentReason.ClientID%>').value = 0;
                document.getElementById('<%=ddlSignature.ClientID%>').value = 0;
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
                document.getElementById('<%=txtAppDate.ClientID%>').value = dt2;
                if (txtdate == "") {
                    document.getElementById('<%=txtAppDate.ClientID%>').value = "";
                }
            }
            return;
        }

    </script>
   <%-- <asp:ScriptManager ID="mst" runat="server" ScriptMode="Auto" EnablePageMethods="true" EnablePartialRendering="true" OnAsyncPostBackError="ActionScriptManager_AsyncPostBackError">
        </asp:ScriptManager>--%>
    <div style="padding-top: 1px;">
        <%--  <form id="PatientRecordCTC" method="post" runat="server">--%>
        <!-- Patient Record CTC 2 starts-->
        <%--    <h1 class="margin">
            Patient Record CTC 2</h1>
        --%>
        <div class="border center formbg">
            <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <%--  <tr>
                    <td class="form" align="center" colspan="2">
                        <label class="patientInfo">
                            Patient Name:</label>
                        <asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label>
                        <label class="patientInfo">
                            Patient ID:</label>
                        <asp:Label ID="lblPatEnrolNo" runat="server" Text=""></asp:Label>
                        <label id="lblFileRef" runat="server" class="patientInfo">
                        </label>
                        <asp:Label ID="lblFileNo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td class="form" width="50%">
                        <label id="lblVisitDate" class="required right35">
                            *Visit Date:
                        </label>
                        <input id="txtvisitDate" maxlength="11" size="11" name="visitDate" onblur="javascript:fnGetHeightCD4Regimen();"
                            onkeyup="fnGetHeightCD4Regimen();" onkeydown="fnGetHeightCD4Regimen();" runat="server"
                            onchange="fnGetHeightCD4Regimen();" onmousemove="fnGetHeightCD4Regimen();" onmouseout="fnGetHeightCD4Regimen();"
                            onfocus="fnGetHeightCD4Regimen();" />
                        <img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID%>');fnGetHeightCD4Regimen();"
                            height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                            border="0" onmousemove="fnGetHeightCD4Regimen();" />
                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                    </td>
                    <td class="form">
                        <label class="right35">
                            Type of Visit:
                        </label>
                        <asp:DropDownList ID="ddlVisitType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form" align="center" colspan="2">
                        <label class="patientInfo">
                            Last CD4 Count:</label>
                        <input id="txtCD4Count" runat="server" maxlength="11" size="11" readonly="readonly" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <label class="patientInfo">
                            Date:</label>
                        <input id="txtCD4Date" runat="server" maxlength="11" size="11" readonly="readonly" />
                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
                <tr>
                    <td id="tdRegimen" class="form" runat="server">
                        <label id="lblRegimen" class="right35">
                            Regimen:
                        </label>
                        <input id="txtRegimen" runat="server" maxlength="20" size="20" readonly="readonly" />
                    </td>
                    <td id="tdReginBegin" class="form" runat="server">
                        <label id="RegimenBegin" class="right35" for="REGIMENBEGIN">
                            Current Regimen Began:</label>
                        <input id="txtRegimenDate" runat="server" maxlength="11" size="11" readonly="readonly" />
                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <!-- Physical Examination starts -->
        <div class="border center formbg">
            <h2 class="forms" align="left">
                Physical Examination</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="border pad5 whitebg" width="50%" align="center">
                        <label id="lblWeight">
                            Weight:</label>
                        <input id="txtWeight" runat="server" maxlength="11" size="11" />
                        <label id="lblHeight" style="margin-left: 40px">
                            Height:</label>
                        <input id="txtHeight" runat="server" maxlength="11" size="11" />
                    </td>
                    <td class="border pad5 whitebg" width="50%" align="center">
                        <label id="lblWHOStage">
                            WHO Stage:</label>
                        <asp:DropDownList ID="ddlWHOStage" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        Problems, Symptoms, Side Effects:</label>
                                </td>
                                <td style="width: 70%">
                                    <input class="margin20" id="rdoIlnessNone" onmouseup="up(this);" onfocus="up(this);" onclick="ShowHideProbSymp(this)"
                                        type="radio" value="none" name="medicalHis" runat="server" />
                                    <span class="smallerlabel">None</span>
                                    <input class="margin20" id="rdoIllnessNotDoc" onmouseup="up(this);" onfocus="up(this);" onclick="ShowHideProbSymp(this)"
                                        type="radio" value="Notdocumented" name="medicalHis" runat="server" />
                                    <span class="smallerlabel">Not Documented</span>
                                    <input class="margin20" id="rdoIllness" onmouseup="up(this);" onfocus="up(this);" onclick="ShowHideProbSymp(this)"
                                        type="radio" value="illness" name="medicalHis" runat="server" />
                                    <span class="smallerlabel">Enter Illnesses</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="probSymptSideEffect" style="display: none">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="width: 50%">
                                                    <div id="divIllness1" runat="server" class="checkboxRight" nowrap="noWrap" style="height: 120px;
                                                        width: 93%">
                                                        <table id="tblIllness1" runat="server">
                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="width: 50%">
                                                    <div id="divIllness2" runat="server" class="checkboxLeft" nowrap="noWrap" style="height: 120px;
                                                        width: 93%">
                                                        <table id="tblIllness2" runat="server">
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" width="100%" colspan="3" align="center" style="height: 34px">
                        <label id="lblHospital" runat="server">
                            Hospitalisation, other problems / complications:</label>
                        <asp:TextBox ID="txtComplication" MaxLength="200" runat="server" Width="570px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form" align="center" colspan="2">
                        <div id="divPregnantShow" style="display: none">
                            <label>
                                Pregnant:</label>
                            <input id="rdoPregYes" onmouseup="up(this);" onfocus="up(this);" onclick="HideShowPregnant(this)" type="radio"
                                value="PY" name="rdoPreg" runat="server" />
                            <span>Yes</span>
                            <input id="rdoPregNo" onmouseup="up(this);" onfocus="up(this);" onclick="HideShowPregnant(this)" type="radio"
                                value="PN" name="rdoPreg" runat="server" />
                            <span>No</span>
                            <div id="divPregnant" style="display: none;">
                                <label id="lblEDD" class="right10" for="EDD">
                                    EDD:</label>
                                <input id="txtEDD" runat="server" maxlength="11" size="11" />
                                <img id="img1" onclick="w_displayDatePicker('<%=txtEDD.ClientID%>');" height="22"
                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                <asp:CheckBox ID="chkDelivered" Text="Delivered" TextAlign="Right" runat="server" />
                                <div id="divChkDelivered" style="display: none;">
                                    <label id="lblDOB" class="right10" for="DOB">
                                        DOB:</label>
                                    <input id="txtDOB" runat="server" maxlength="11" size="11" />
                                    <img id="img3" onclick="w_displayDatePicker('<%=txtDOB.ClientID%>');" height="22"
                                        alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" width="50%" align="center">
                        <label id="lblFuncStatus">
                            Functional Status:</label>
                        <asp:DropDownList ID="ddlFuncStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="border pad5 whitebg" width="50%" align="center">
                        <label id="lblTBStatus" for="TBStatus">
                            TB Status:</label>
                        <asp:DropDownList ID="ddlTBStatus" runat="server" onchange="ShowHideTBStatus()">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <div id="divTBStatus" style="display: none">
                            <label>
                                TB ID Number</label>
                            <asp:TextBox ID="txtTBID" Width="100px" runat="server"></asp:TextBox></div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <!-- ARV Therapy & Adherence starts -->
        <div class="border center formbg">
            <h2 class="forms" align="left">
                ARV Therapy and Adherence</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="border pad5 whitebg">
                        <!-- ARV Status-->
                        <div class="center" id="divARVStatus">
                            <label id="lblARVStatus">
                                <b>ARV Status:</b></label>
                            <asp:DropDownList ID="ddlARVStatus" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div id="divARVReason" style="display: none">
                            <hr style="color: Black; height: 1px; font-weight: normal; font-size: 100%;" />
                            <table id="tblARVReason">
                                <tr>
                                    <td style="width: 43%">
                                    </td>
                                    <td style="width: 57%">
                                        <asp:Label ID="lblARVReason" class="required" Font-Bold="true" Text="*ARV Reason"
                                            runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlARVReason" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="center" id="divWhyEligible" style="display: none">
                            <hr style="color: Black; height: 1px; font-weight: normal; font-size: 100%;" />
                            <table id="tblWhyEligible">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblWhyEligible" Text="Why Eligible:" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlWhyEligible" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnCD4TLC" Text="Enter CD4/TLC" runat="server" OnClick="btnCD4TLC_OnClick" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="lbldatemedically" style="margin-left: 60px">
                                            Date Medically Eligible:</label>
                                        <input id="txtEligibleDate" maxlength="11" size="11" name="Datemedically" runat="server" />
                                        <img onclick="w_displayDatePicker('<%= txtEligibleDate.ClientID%>');" height="22"
                                            alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0" /><span
                                                class="smallerlabel">(DD-MMM-YYYY)</span>
                                        <label id="lbldateeligible" style="margin-left: 100px">
                                            Date Eligible and Ready:</label>
                                        <input id="txtReadyDate" maxlength="11" size="11" name="Datemedically" runat="server" />
                                        <img onclick="w_displayDatePicker('<%= txtReadyDate.ClientID%>');" height="22" alt="Date Helper"
                                            hspace="5" src="../images/cal_icon.gif" width="22" border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divARVAdhStatusANDAdhReason" style="display: none">
                            <hr style="color: Black; height: 1px; font-weight: normal; font-size: 100%;" />
                            <table id="tblARVAdhStatusANDAdhReasn" width="100%">
                                <!-- -->
                                <tr>
                                    <td colspan="2">
                                        <div id="divLblARVReason2" style="margin-left: 270px">
                                            <asp:Label ID="Label2" class="required" Font-Bold="true" Text="*ARV Reason" runat="server"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="Label3" Font-Bold="true" Text="(select all that apply):" runat="server"></asp:Label>
                                        </div>
                                        <div id="divARVReason2">
                                            <div style="margin-left: 270px; width: 505px; height: 100px" id="divArvReasonprint"
                                                class="checkboxLeft" nowrap="noWrap">
                                                <table id="tblARVReason2" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                    runat="server">
                                                </table>
                                            </div>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <hr style="color: Black; height: 1px; font-weight: normal; font-size: 100%;" />
                                        </div>
                                    </td>
                                </tr>
                                <!-- -->
                                <tr>
                                    <td style="vertical-align: top; width: 50%">
                                        <div style="display: none" id="divARVAdhStatus">
                                            <asp:Label ID="lblARVAdhStatus" class="required" Font-Bold="true" Text="*ARV Adherence Status"
                                                runat="server"></asp:Label>
                                            &nbsp;<br />
                                            <input id="rdoAdhStatusGood" onmouseup="up(this);" onfocus="up(this);" onclick="ShowHideARVAdhReason(this)"
                                                type="radio" value="G" name="rdoARVAdhSatus" runat="server" />
                                            <span>Good</span>
                                            <input id="rdoAdhStatusNotDoc" onmouseup="up(this);" onfocus="up(this);" onclick="ShowHideARVAdhReason(this)"
                                                type="radio" value="ND" name="rdoARVAdhSatus" runat="server" />
                                            <span>Not Documented</span>
                                            <input id="rdoAdhStatusPoor" onmouseup="up(this);" onfocus="up(this);" onclick="ShowHideARVAdhReason(this)"
                                                type="radio" value="P" name="rdoARVAdhSatus" runat="server" />
                                            <span>Poor</span>
                                            <br />
                                        </div>
                                    </td>
                                    <td style="width: 50%">
                                        <div id="divARVAdhReason">
                                            <asp:Label ID="lblAdhReason" Font-Bold="true" Text="ARV Adherence Reasons (select all that apply):"
                                                runat="server"></asp:Label>
                                            <br />
                                            <div id="divChkARVAdhReason" class="checkboxLeft" nowrap="noWrap" style="width: 360px;
                                                height: 100px">
                                                <table id="tbARVAdhReason" cellpadding="0" cellspacing="2" width="100%" border="0"
                                                    runat="server">
                                                </table>
                                            </div>
                                        </div>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <!-- ARV Therapy & Adherence ends -->
        <!---- Patient Support starts ---->
        <div class="border center formbg">
            <h2 class="forms" align="left">
                Patient Support</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="border pad5 whitebg" align="center" valign="middle" style="width: 437px">
                        <label>
                            Nutritional Support Needed:</label>
                        <input id="rdoNutSupportNeededYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this)" type="radio"
                            value="0" name="rdoNutSupportNeeded" runat="server" />
                        <span>Yes</span>
                        <input id="rdoNutSupportNeededNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this)" type="radio"
                            value="1" name="rdoNutSupportNeeded" runat="server" />
                        <span>No</span>
                    </td>
                    <td class="border pad5 whitebg" align="left">
                        <div id="div1" style="display: inline">
                            <label>
                                Referred To: (select all that apply):</label>
                        </div>
                        <br />
                        <div id="divReferTo" class="checkboxLeft" style="width: 390px; height: 100px">
                            <table id="tblRefer" cellpadding="0" cellspacing="2" width="100%" border="0" runat="server">
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <!---- Appointment and Signature starts ---->
            <div class="border center formbg">
                <h2 class="forms" align="left">
                    Appointment and Signature</h2>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="border pad5 whitebg" width="50%" align="center">
                            <label>
                                When is the patient's next appointment?:</label>
                            <asp:DropDownList ID="ddlNextAppoint" onchange="addDays()" runat="server">
                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="1 week" Value="7"></asp:ListItem>
                                <asp:ListItem Text="2 week" Value="14"></asp:ListItem>
                                <asp:ListItem Text="3 week" Value="21"></asp:ListItem>
                                <asp:ListItem Text="1 month" Value="28"></asp:ListItem>
                                <asp:ListItem Text="2 months" Value="58"></asp:ListItem>
                                <asp:ListItem Text="3 months" Value="88"></asp:ListItem>
                                <asp:ListItem Text="6 months" Value="180"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="border pad5 whitebg" width="50%" align="center">
                            <label class="">
                                Appointment Reason:</label>
                            <asp:DropDownList ID="ddlAppointmentReason" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td id="td2" class="border pad whitebg" runat="server">
                            <label id="lblAppDate" class="right35" for="SpecifyDate">
                                Specify Date:</label>
                            <input id="txtAppDate" runat="server" maxlength="11" size="11" />
                            <img id="img2" onclick="w_displayDatePicker('<%=txtAppDate.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                        <td class="border pad5 whitebg" width="50%" align="center">
                            <label class="">
                                Signature:</label>
                            <asp:DropDownList ID="ddlSignature" runat="server">
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
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
                            <asp:Button ID="btnDQ" Text="Data Quality Check" runat="server" OnClick="btnDQ_Click" />
                            <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
