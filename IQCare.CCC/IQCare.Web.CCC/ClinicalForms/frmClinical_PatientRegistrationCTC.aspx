<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.PatientRegistrationCTC"
    Title="Untitled Page" MaintainScrollPositionOnPostback="true" Codebehind="frmClinical_PatientRegistrationCTC.aspx.cs" %>
     <%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        function showControl() {
            alert(1);
        }
        function GetControl() {
            var browserName = navigator.appName;
            if (browserName != "Microsoft Internet Explorer") {
                document.forms[0].submit();
            }
            else {
                document.forms[0].submit(); 
            }

        }

        function OpenDobPopup() {
            if (document.getElementById('<%= TxtRegistrationDate.ClientID %>').value == "")
            { alert("Please Fill Registration Date First"); }
            else
                window.open('frmCalculateAgeCTC.aspx?regDate="' + document.getElementById('<%=TxtRegistrationDate.ClientID%>').value + '"', '', 'toolbars=no,location=no,directories=no,dependent=yes,top=300,left=400,maximize=no,resize=no,width=320,height=180,scrollbars=yes');

        }
        function Redirect(id) {

            window.location.href = "frmPatient_Home.aspx?PatientId=" + id;
        }
        function ShowHidePriorExposure() {
            if (document.getElementById('<%=DDPriorExposure.ClientID%>').value == 265) {
                document.getElementById('<%=DDWHOStage.ClientID%>').value = 0;
                document.getElementById('<%=DDFunctionalStatus.ClientID%>').value = 0;
                document.getElementById('<%=TxtWeight.ClientID%>').value = "";
                document.getElementById('<%=TxtCD4.ClientID%>').value = "";
                document.getElementById('<%=TxtCD4Percent.ClientID%>').value = "";
                document.getElementById('<%=TxtTLC.ClientID%>').value = "";
                document.getElementById('<%=TxtTLCPercent.ClientID%>').value = "";
                document.getElementById('divPriorExposure').style.display = 'inline'
                document.getElementById('TrART_I').style.display = 'inline'
                document.getElementById('TrART_II').style.display = 'inline'
            }
            else if (document.getElementById('<%=DDPriorExposure.ClientID%>').value == 289) {
                document.getElementById('<%=TxtArtStartDate.ClientID%>').value = "";
                document.getElementById('<%=DDWhyEligible.ClientID%>').value = 0;
                document.getElementById('<%=ddregimen.ClientID%>').value = 0;
                document.getElementById('<%=TxtInitialRegimen.ClientID%>').value = "";
                document.getElementById('divPriorExposure').style.display = 'inline'
                document.getElementById('TrART_I').style.display = 'none'
                document.getElementById('TrART_II').style.display = 'none'
            }
            else
                document.getElementById('divPriorExposure').style.display = 'none'
        }

        function othertextbox() {
            if (document.getElementById('<%=DDReferredFrom.ClientID%>').value == 244)
                document.getElementById('divreferredfromOther').style.display = 'inline'
            else
                document.getElementById('divreferredfromOther').style.display = 'none'
        }

        //Function to Calculate Age in Year and Months
        function CalculateAge(txtAge, txtmonth, txtDOB, txtRegSysDate) {
            //alert(txtDOB);
            //alert(txtRegSysDate);
            var YR1 = document.getElementById(txtDOB).value.toString().substr(7, 4);
            var YR2 = document.getElementById(txtRegSysDate).value.toString().substr(7, 4);

            var mm1 = document.getElementById(txtDOB).value.toString().substr(3, 3);
            var mm2 = document.getElementById(txtRegSysDate).value.toString().substr(3, 3);

            var dd1 = document.getElementById(txtDOB).value.toString().substr(0, 2);
            var dd2 = document.getElementById(txtRegSysDate).value.toString().substr(0, 2);

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
        function OnDateValidate(result) {
            if (result != '') {
                document.getElementById('ctl00_IQCareContentPlaceHolder_' + result).disabled = true;
            }
            if (document.getElementById('ctl00_IQCareContentPlaceHolder_img' + result) != null) {
                document.getElementById('ctl00_IQCareContentPlaceHolder_' + result).disabled = true;
            }
        }
        function OnDuplicateFind(result) {
            if (result != "") {
                document.getElementById('search_popup').style.display = 'inline';
                document.getElementById('showresult').innerHTML = result;
            }
            else {
                document.getElementById('search_popup').style.display = 'none';
            }
        }
        //**********************************************
        function OnPageError(error) {
            if (error) alert("IQCare Application Framework encountered an unrecoverable error:\n" + error.get_message());
            else alert("IQCare Application Framework encountered an unrecoverable error.");
        }
        function fnshow() {

            var status = '<%=Request.QueryString["name"]%>';
            if (status != 'Edit') {
                var fname = document.getElementById('<%=TxtFirstName.ClientID %>').value;
                var mname = document.getElementById('<%=TxtMiddleName.ClientID %>').value;
                var lname = document.getElementById('<%=TxtLastName.ClientID %>').value;
                var address = document.getElementById('<%=TxtContactDetails.ClientID %>').value;
                var phone = document.getElementById('<%=TxtPhone.ClientID %>').value;
                if (fname != '') {
                    PageMethods.GetDuplicate(fname, mname, lname, address, phone, OnDuplicateFind, OnPageError);
                  //  var result = ClinicalForms_frmClinical_PatientRegistrationCTC.GetDuplicateRecord(fname, mname, lname, address, phone).value;
//                    if (result != "") {
//                        document.getElementById('search_popup').style.display = 'inline';
//                        document.getElementById('showresult').innerHTML = result;
//                    }
//                    else {
//                        document.getElementById('search_popup').style.display = 'none';
//                    }
                }
            }
        }
        function WindowPrint() {
            window.print();
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


    </script>
    <form id="patientregistrationctc" onmousemove="javascript:checkLoadedObjects(event);"
    ondblclick="javascript:dropLoadedObject(event);" method="post" enableviewstate="true"
    runat="server">
<%--    <asp:ScriptManager ID="mst" runat="server" EnablePageMethods="true" ScriptMode="Auto" EnablePartialRendering="true" 
        OnAsyncPostBackError="ActionScriptManager_AsyncPostBackError"> </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
        <ContentTemplate>
            <div runat="server">
                <%--   <h1 class="margin">
                    Enrollment</h1>
                --%>
                <div class="border center formbg">
                    <div class="popupWindow" id='search_popup' onclick="javascript:getReadyToMove('search_popup', event);"
                        name='styled_popup' style="display: none;">
                        <table cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                            <tr bgcolor="#666699">
                                <td align="right">
                                    <span style="cursor: hand" onclick="jsAreaClose('search_popup')">
                                        <img alt="Hide Popup" src="../Images/close.gif" border="0"></span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 150px">
                                    <div id="showresult">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="alert">
                                    Please check we have found similar record in the database
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="border pad5 whitebg" style="display: marker">
                                    <label id="EnrolmentID" class="required" for="PatientID">
                                        *Patient ID:</label>
                                    <span class="smallerlabel">Region:</span>
                                    <asp:TextBox ID="TxtRegion" runat="server" Width="10%"></asp:TextBox>
                                    <span class="smallerlabel">District:</span>
                                    <asp:TextBox ID="TxtDistrict" runat="server" Width="10%"></asp:TextBox>
                                    <span id="lblsat" class="smallerlabel">Facility-Satellite #:</span>
                                    <asp:TextBox ID="TxtFacility" runat="server" Width="10%"></asp:TextBox>
                                    <span id="ptnID" class="smallerlabel">Ptn #:</span>
                                    <asp:TextBox ID="TxtPtnEnrollment" runat="server" Width="10%" MaxLength="8"></asp:TextBox>
                                    <a></a>
                                </td>
                                <td class="border pad5 whitebg">
                                    <label id="lblfreference" class="margin20" for="freference">
                                        File Reference:</label>
                                    <asp:TextBox ID="TxtFreference" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 550px" class="border pad5 whitebg">
                                    <label id="lblPName" class="required" for="patientname">
                                        *Patient Name:</label>
                                    <span id="FName" class="smallerlabel">First: </span>
                                    <asp:TextBox ID="TxtFirstName" onchange="fnshow();" onblur="fnshow();" runat="server"
                                        Width="122px" MaxLength="50"></asp:TextBox>
                                    <span id="MName" class="smallerlabel">Middle: </span>
                                    <asp:TextBox ID="TxtMiddleName" onchange="fnshow();" onblur="fnshow();" runat="server"
                                        Width="122px" MaxLength="50"></asp:TextBox>
                                    <span id="LName" class="smallerlabel">Last: </span>
                                    <asp:TextBox ID="TxtLastName" onchange="fnshow();" onblur="fnshow();" runat="server"
                                        Width="122px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="border pad5 whitebg" width="35%">
                                    <label id="lblregistrationdate" class="required" for="RegistrationDate">
                                        *Registration Date:</label>
                                    <asp:TextBox ID="TxtRegistrationDate" runat="server" Width="25%" MaxLength="11"></asp:TextBox>
                                    <img onclick="w_displayDatePicker('<%= TxtRegistrationDate.ClientID %>');" height="22"
                                        alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 550px" class="border pad5 whitebg" nowrap>
                                    <label id="lblgender" class="required" for="gender">
                                        *Sex:</label>
                                    <asp:DropDownList ID="DDGender" runat="server" OnSelectedIndexChanged="DDGender_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <label id="lblDOB" class="required margin15" for="DOB">
                                        *Date of Birth:</label>
                                    <asp:TextBox ID="TxtDOB" runat="server" Width="70px" MaxLength="11"></asp:TextBox>
                                    <img onclick="w_displayDatePicker('<%=TxtDOB.ClientID %>');" height="22" alt="Date Helper"
                                        hspace="3" src="../images/cal_icon.gif" width="20" border="0" />
                                    <span class="smallerlabel">DD-MMM-YYYY </span>
                                    <input id="RbtnDOBPrecExact" onfocus="up(this)" onclick="down(this)" type="radio"
                                        value="1" name="dobPrecision" runat="server" />
                                    <span class="smalllabel">Exact </span>
                                    <input id="RbtnDOBPrecEstimated" onfocus="up(this)" onclick="down(this)" type="radio"
                                        value="0" name="dobPrecision" runat="server" />
                                    <span class="smalllabel">Estimated</span>
                                    <br />
                                    <br />
                                    <span style="margin-left: 390px">
                                        <input id="btnCalDOB" onclick="OpenDobPopup()" type="button" value="Calculate Date of Birth"
                                            width="120px" />
                                    </span>
                                </td>
                                <td class="border pad5 whitebg">
                                    <label class="margin20" for="Age">
                                        Age: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:TextBox ID="TxtAgeCurrentYears" runat="server" Width="10%" MaxLength="2" ReadOnly="true"></asp:TextBox>
                                    <span class="smallerlabel">yrs</span>
                                    <asp:TextBox ID="TxtAgeCurrentMonths" runat="server" Width="10%" MaxLength="2" ReadOnly="true"></asp:TextBox>
                                    <span class="smallerlabel">mths</span>
                                    <br />
                                    <br />
                                    <label class="margin20" for="AgeatEnrolment">
                                        Age at Enrollment:</label>
                                    <asp:TextBox ID="TxtAgeEnrollmentYears" runat="server" Width="10%" MaxLength="2"
                                        ReadOnly="true"></asp:TextBox>
                                    <span class="smallerlabel">yrs</span>
                                    <asp:TextBox ID="TxtAgeEnrollmentMonths" runat="server" Width="10%" MaxLength="2"
                                        ReadOnly="true"></asp:TextBox>
                                    <span class="smallerlabel">mths</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px" class="form" align="center" colspan="2">
                                    <label for="MaritalStatus">
                                        Marital Status:</label>
                                    <asp:DropDownList ID="DDMaritalStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div class="border center formbg">
                    <br />
                    <h2 class="forms" align="left">
                        Contact Information</h2>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="border pad5 whitebg" style="width: 50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label>
                                                    Phone Number:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtPhone" onchange="fnshow();" onblur="fnshow();" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg" style="width: 50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="ContactDetails">
                                                    Contact Details:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtContactDetails" onchange="fnshow();" onblur="fnshow();" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border  whitebg" width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Region">
                                                    Region:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:DropDownList ID="DDRegion" runat="server" OnSelectedIndexChanged="ddregion_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="District">
                                                    District:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:DropDownList ID="DDDistrict" runat="server" OnSelectedIndexChanged="dddistrict_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg" width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Division">
                                                    Division:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:DropDownList ID="DDDivision" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg" width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Ward">
                                                    Ward:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:DropDownList ID="DDWard" runat="server" OnSelectedIndexChanged="ddWard_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Village">
                                                    Village/Street:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:DropDownList ID="DDVillage" runat="server" OnSelectedIndexChanged="DDVillage_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Chairperson">
                                                    Chairperson:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtChairPerson" ReadOnly="true" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="TenCellLeader">
                                                    Ten Cell Leader:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtTCLeader" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="TenCellLeaderContact">
                                                    Ten Cell Leader Contact:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtTCLContact" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Householdhead">
                                                    Household Head:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtHHHead" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="Householdheadcontact">
                                                    Household Head Contact:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtHHHcontact" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <h2 class="forms" align="left">
                        Treatment Supporter Information</h2>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="border pad5 whitebg" width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="treatmentsupportername">
                                                    Treatment Supporter Name:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtTreatSupporterName" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg" width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="treatmentsupporteraddress">
                                                    Treatment Supporter Address:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtTsAddress" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg" width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="treatmentsupporterPhone">
                                                    Treatment Supporter Phone:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtTSPhone" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label for="CommunitySupportOrganization">
                                                    Community Support Organization:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtComsOrganization" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <h1 class="margin">
                    HIV Related History</h1>
                <div class="border center formbg">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="width: 50%" class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label id="lbldateposhiv" for="DateofFirstHIVTest">
                                                    Date of First Positive HIV Test:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtDtPosHivTest" runat="server" Width="70px" MaxLength="11"></asp:TextBox>
                                                <img onclick="w_displayDatePicker('<%= TxtDtPosHivTest.ClientID %>');" height="22"
                                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="20" border="0" />
                                                <span class="smallerlabel">DD-MMM-YYYY </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="border pad5 whitebg">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%" align="right">
                                                <label id="lbldateconfirmhivpos" for="confirmedHIVpositive">
                                                    Date Confirmed HIV Positive:</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="TxtConfirmHivPositive" runat="server" Width="70px" MaxLength="11"></asp:TextBox>
                                                <img onclick="w_displayDatePicker('<%= TxtConfirmHivPositive.ClientID %>');" height="22"
                                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="20" border="0" />
                                                <span class="smallerlabel">DD-MMM-YYYY </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg">
                                    <label style="margin-left: 35px" for="ReferredFrom">
                                        Referred From:
                                        <asp:DropDownList ID="DDReferredFrom" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                    <div style="display: none" id="divreferredfromOther">
                                        <label class="required right70" for="ReferredfromOther">
                                            *Specify:</label>
                                        <asp:TextBox ID="TxtReferredFromOther" runat="server" MaxLength="20" size="13" name="referredfromother"></asp:TextBox>
                                    </div>
                                </td>
                                <td class="border pad5 whitebg">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <label class="left" for="DrugAllergies">
                                        Drug Allergies:</label>
                                    <asp:TextBox ID="txtdrugallergies" runat="server" Width="315px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg" colspan="2">
                                    <label style="margin-left: 27%" for="Priorexposure">
                                        Prior Exposure:</label>
                                    <asp:DropDownList ID="DDPriorExposure" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                </td>
                            </tr>
                        </tbody>
                </div>
                <br />
                <div class="center">
                    <table style="display: none" id="divPriorExposure" class="border formbg" cellspacing="6"
                        cellpadding="0" width="90%">
                        <tbody>
                            <tr>
                                <td>
                                    <h3 class="forms" align="left">
                                        Transfer in with Records/Start at another Clinic–All Indicators at ART START</h3>
                                </td>
                            </tr>
                            <tr id="TrART_I">
                                <td class="whitebg border pad5">
                                    <label class="right25">
                                        ART Start Date:</label>
                                    <asp:TextBox ID="TxtArtStartDate" runat="server" Width="70px" MaxLength="11"></asp:TextBox>
                                    <img onclick="w_displayDatePicker('<%= TxtArtStartDate.ClientID %>');" height="22"
                                        alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="20" border="0" />
                                    <span class="smallerlabel">DD-MMM-YYYY </span>
                                    <label class="right20">
                                        Why Eligible:</label>
                                    <asp:DropDownList ID="DDWhyEligible" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="TrART_II">
                                <td class="whitebg whitebg center border pad5">
                                    <label id="lbliregimen" class="required">
                                        *Initial Regimen:</label>
                                    <asp:DropDownList ID="ddregimen" runat="server" OnSelectedIndexChanged="ddregimen_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TxtInitialRegimen" ReadOnly="true" runat="server" Width="150px"></asp:TextBox>
                                    <asp:Button ID="btnRegimen" Text="Add" runat="server" OnClick="btnRegimen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="whitebg border pad5">
                                    <label class="right11" style="font-weight: bold" for="WhoStatus">
                                        WHO Status:</label>
                                    <asp:DropDownList ID="DDWHOStage" runat="server" Width="100px">
                                    </asp:DropDownList>
                                    <label class="right25" for="FunctionalStatus">
                                        Functional Status:</label>
                                    <asp:DropDownList ID="DDFunctionalStatus" runat="server" Width="100px">
                                    </asp:DropDownList>
                                    <label class="right20">
                                        Weight:</label>
                                    <asp:TextBox ID="TxtWeight" runat="server" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="whitebg whitebg border pad5">
                                    <label class="right10">
                                        CD4:</label>
                                    <asp:TextBox ID="TxtCD4" runat="server" Width="100px"></asp:TextBox>
                                    <span class="smallerlabel">c/mm^3</span>
                                    <asp:TextBox ID="TxtCD4Percent" runat="server" Width="100px"></asp:TextBox>
                                    <span class="smallerlabel">Percent</span>
                                    <label class="right10">
                                        TLC:</label>
                                    <asp:TextBox ID="TxtTLC" runat="server" Width="100px"></asp:TextBox>
                                    <span class="smallerlabel">10^3 cells/mcl</span>
                                    <asp:TextBox ID="TxtTLCPercent" runat="server" Width="100px"></asp:TextBox>
                                    <span class="smallerlabel">Percent</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div class="border center formbg">
                    <table cellspacing="6" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td class="form" colspan="2">
                                <asp:Panel ID="pnlCustomList" runat="server" Visible="false" Height="100%" Width="100%"
                                    Wrap="true">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="border center formbg">
                    <table cellspacing="6" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <asp:TextBox ID="txtSysDate" runat="server" CssClass="textstylehidden"></asp:TextBox><td
                                    style="height: 53px" class="pad5 center" colspan="2">
                                    <asp:Button ID="btnsave" OnClick="btnsave_Click" Text="Save" runat="server"></asp:Button>
                                    <asp:Button ID="btnDQ" OnClick="btnDQ_Click" Text="Data quality check" runat="server">
                                    </asp:Button>
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Close" runat="server">
                                    </asp:Button>
                                    <asp:Button ID="btnOKDQ" Text="DQ" runat="server" CssClass="textstylehidden"></asp:Button>
                                    <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRegimen"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnDQ"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="DDGender" />
            <asp:PostBackTrigger ControlID="DDRegion" />
            <asp:PostBackTrigger ControlID="DDDistrict" />
            <asp:PostBackTrigger ControlID="DDDivision" />
            <asp:PostBackTrigger ControlID="DDWard" />
            <asp:PostBackTrigger ControlID="DDVillage" />
            <asp:PostBackTrigger ControlID="ddregimen" />
        </Triggers>
    </asp:UpdatePanel>
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
    </form>
</asp:Content>
