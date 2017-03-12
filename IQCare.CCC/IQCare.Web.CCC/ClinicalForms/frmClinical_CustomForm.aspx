<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.CustomForm" Title="Untitled Page" MaintainScrollPositionOnPostback="true"
    EnableEventValidation="false" CodeBehind="frmClinical_CustomForm.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div style="padding-top: 1px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px">
        <script language="javascript" type="text/javascript">
            function CalcualteBMIGet() {

                var wid = document.getElementById('<%=hdnWeight.ClientID %>').value;
                var hid = document.getElementById('<%=hdnHeight.ClientID %>').value;
                var hbmi = document.getElementById('<%=hdnBNI.ClientID %>').value;
                var weight = "";
                var height = "";
                if (wid != "")
                    weight = document.getElementById(wid).value;
                if (hid != "")
                    height = document.getElementById(hid).value;

                if (weight == "" || height == "") {
                    weight = 0;
                    height = 0;
                }

                weight = parseFloat(weight);
                height = parseFloat(height);

                var BMI = weight / ((height / 100) * (height / 100));
                BMI = BMI.toFixed(2);
                if (hbmi != "")
                    document.getElementById(hbmi).value = BMI;
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



            function WindowPrint() {
                window.print();
            }
            function WindowPrintAll() {
                window.print();
            }

            function OpenPharmacyDialog(DrgId) {
                window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=' + DrgId + '&BtnDrg=customfrmDrug', 'DrugSelection', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');
            }

            function OpenRegimenDialog(DrgId, ControlId) {
                window.open('../Pharmacy/frmDrugSelector.aspx?RegType=' + DrgId + '&BtnRegimen=customfrmReg&Cntrl=' + ControlId + '', 'DrugSelection', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');
            }
            function AdditionalLab() {
                window.open('../Laboratory/frmLabSelector.aspx?Mode=All', 'LabSelection', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');
            }
            function ICD10PopUp(FieldId, VisitId) {
                window.open('frmClinicalICD10Selector.aspx?Param=' + FieldId + ' &Param1=' + VisitId + '', 'ICD10Selection', 'toolbars=no,location=no,directories=no,dependent=yes,top=30,left=70,maximize=no,resize=no,width=950,height=470,scrollbars=yes');
            }
            function GetControl() {
                document.forms[0].submit();
            }
            function SetValue(theObject, theValue) {
                document.getElementById('ctl00_IQCareContentPlaceHolder_' + theObject).value = theValue;
                document.forms[0].submit();
            }

            function EnableControlFalse(theID) {

                try {

                    document.getElementById(theID).disabled = true;
                }
                catch (Error) {
                    alert(Error.Message);
                }

            }

            function EnableControlTrue(theID) {
                try {
                    document.getElementById(theID).disabled = false;
                }
                catch (Error) {
                    alert(Error.Message);
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

            function OnControlSuccess(result) {
                //                if (result != "") {
                //                    document.getElementById('search_popup').style.display = 'inline';
                //                    document.getElementById('showresult').innerHTML = result;
                //                }
                //                else {
                //                    document.getElementById('search_popup').style.display = 'none';
                //                }
            }
            //**********************************************
            function OnControlError(error) {
                if (error) alert("IQCare Application Framework encountered an unrecoverable error:\n" + error.get_message());
                else alert("IQCare Application Framework encountered an unrecoverable error.");
            }
            function fncontrolstatus(ctrlid) {

                try {

                    var d = document.getElementById(ctrlid);
                    alert(d.disabled);
                    if (d.disabled) {
                        PageMethods.SetContrlStatus(ctrlid, OnControlSuccess, OnControlError);
                    }
                    else {
                        PageMethods.removecontrolstatus(ctrlid, OnControlSuccess, OnControlError);
                    }
                }
                catch (Error) {
                    alert(Error.Message);
                }
            }
            function divunhide() {
                document.getElementById('ctl00_IQCareContentPlaceHolder_TAB').style.visibility = 'visible';
            }
            function EnableValueYes(theID) {
                if (theID != "") {

                    document.getElementById(theID).disabled = false;
                    //frmClinical_CustomForm.removecontrolstatus(theID);
                    PageMethods.removecontrolstatus(theID, OnControlSuccess, OnControlError);
                    var span = document.getElementById(theID).getElementsByTagName('span');
                    var inputchkbox = document.getElementById(theID).getElementsByTagName('input');
                    for (var i = 0; i < span.length; i++) {
                        span[i].disabled = false;
                    }

                    for (var i = 0; i < inputchkbox.length; i++) {
                        if (inputchkbox[i].type.toString().toLowerCase() == "checkbox") {
                            document.getElementById(inputchkbox[i].id).disabled = false;
                        }
                    }
                }
                else {
                    document.getElementById(theID).disabled = true;

                }
            }

            function EnableValueNo(theID) {
                if (theID != "") {

                    //frmClinical_CustomForm.SetContrlStatus(theID);
                    PageMethods.SetContrlStatus(theID, OnControlSuccess, OnControlError);
                    if (theID.indexOf("SELECTLIST") != -1) {
                        document.getElementById(theID).selectedIndex = 0;
                    }
                    if (theID.indexOf("TXT") != -1) {
                        document.getElementById(theID).value = '';
                    }
                    if (theID.indexOf("RADIO") != -1) {
                        document.getElementById(theID).checked = false;
                    }
                    if (theID.indexOf("Chk") != -1) {
                        document.getElementById(theID).checked = false;
                    }
                    document.getElementById(theID).disabled = true;

                    var span = document.getElementById(theID).getElementsByTagName('span');
                    var inputchkbox = document.getElementById(theID).getElementsByTagName('input');
                    for (var i = 0; i < span.length; i++) {
                        span[i].disabled = true;
                    }
                    for (var i = 0; i < inputchkbox.length; i++) {
                        if (inputchkbox[i].type.toString().toLowerCase() == "checkbox") {
                            document.getElementById(inputchkbox[i].id).checked = false;
                            document.getElementById(inputchkbox[i].id).disabled = true;
                        }
                    }
                }
            }

            function EnableValueDropdown(thedropdownId, theID, ddvalue) {
                //alert(theID);
                if (theID != "") {
                    var e = document.getElementById(thedropdownId); // select element    
                    var strUser = e.options[e.selectedIndex].value;
                    //alert(strUser);
                    //alert(ddvalue);
                    if (strUser == ddvalue) {

                        document.getElementById(theID).disabled = false;
                        // frmClinical_CustomForm.removecontrolstatus(theID);
                        PageMethods.removecontrolstatus(theID, OnControlSuccess, OnControlError);
                        var span = document.getElementById(theID).getElementsByTagName('span');
                        var inputchkbox = document.getElementById(theID).getElementsByTagName('input');
                        for (var i = 0; i < span.length; i++) {
                            span[i].disabled = false;
                        }

                        for (var i = 0; i < inputchkbox.length; i++) {
                            if (inputchkbox[i].type.toString().toLowerCase() == "checkbox") {
                                document.getElementById(inputchkbox[i].id).disabled = false;
                            }

                        }
                    }
                    else {
                        var span = document.getElementById(theID).getElementsByTagName('span');
                        var inputchkbox = document.getElementById(theID).getElementsByTagName('input');
                        if (theID.indexOf("SELECTLIST") != -1) {
                            document.getElementById(theID).selectedIndex = 0;
                        }
                        if (theID.indexOf("TXT") != -1) {
                            document.getElementById(theID).value = '';
                        }
                        for (var i = 0; i < inputchkbox.length; i++) {
                            if (inputchkbox[i].type.toString().toLowerCase() == "checkbox") {
                                document.getElementById(inputchkbox[i].id).checked = false;
                                document.getElementById(inputchkbox[i].id).disabled = true;
                            }
                        }
                        document.getElementById(theID).disabled = true;

                    }
                }
            }


            function EnableValuechkbox(thechkboxId, theID, fieldId, confieldId) {
                if (theID != "") {
                    if (document.getElementById(thechkboxId).checked == true && fieldId == confieldId) {
                        document.getElementById(theID).disabled = false;

                        // frmClinical_CustomForm.removecontrolstatus(theID);
                        PageMethods.removecontrolstatus(theID, OnControlSuccess, OnControlError);
                        var span = document.getElementById(theID).getElementsByTagName('span');
                        var inputchkbox = document.getElementById(theID).getElementsByTagName('input');
                        for (var i = 0; i < span.length; i++) {
                            span[i].disabled = false;
                        }

                        for (var i = 0; i < inputchkbox.length; i++) {
                            if (inputchkbox[i].type.toString().toLowerCase() == "checkbox") {
                                document.getElementById(inputchkbox[i].id).disabled = false;
                            }
                        }
                    }
                    if (document.getElementById(thechkboxId).checked == false && fieldId == confieldId) {
                        if (theID.indexOf("TXT") != -1) {
                            document.getElementById(theID).value = '';
                        }
                        if (theID.indexOf("SELECTLIST") != -1) {
                            document.getElementById(theID).selectedIndex = 0;
                        }
                        document.getElementById(theID).disabled = true;

                    }
                }
            }




            function SendCodeName(str) {
                var id = document.getElementById(str).value;
                if (id.length <= 0) {
                    return false;
                }
                else {
                    CallServer(id, "This is context from client");
                    return true;
                }
            }
            function ReceiveServerData(args, context) {

                try {
                    if (window.navigator.appName == "Microsoft Internet Explorer") {
                        xmlDoc = new ActiveXObject("MSXML2.DOMDocument");
                        xmlDoc.loadXML(args);
                        var dsRoot = xmlDoc.documentElement;

                        for (var i = 0; i < dsRoot.childNodes.length; i++) {
                            var ID = dsRoot.childNodes(i).childNodes(0).firstChild.text;
                            var Value = dsRoot.childNodes(i).childNodes(1).firstChild.text;
                            var type = dsRoot.childNodes(i).childNodes(2).firstChild.text;

                            if ((Value == 0) && (type != 'DropDown')) {
                                document.getElementById('ctl00_IQCareContentPlaceHolder_' + ID).value = '';
                            }
                            else {
                                document.getElementById('ctl00_IQCareContentPlaceHolder_' + ID).value = Value;
                            }


                        }
                        document.getElementById('ctl00_IQCareContentPlaceHolder_' + ID).disabled = true;
                    }
                    else {
                        parser = new DOMParser();
                        xmlDoc = parser.parseFromString(args, "text/xml");
                        var xmllength = xmlDoc.getElementsByTagName("theDTAuto").length;
                        for (var i = 0; i < xmllength; i++) {
                            var vtag = xmlDoc.getElementsByTagName("theDTAuto")[i];
                            var ID = vtag.getElementsByTagName("ID")[0].childNodes[0].nodeValue;
                            var Value = vtag.getElementsByTagName("Value")[0].childNodes[0].nodeValue;
                            var type = vtag.getElementsByTagName("Ctrl")[0].childNodes[0].nodeValue;
                            if ((Value == 0) && (type != 'DropDown')) {
                                document.getElementById('ctl00_IQCareContentPlaceHolder_' + ID).value = '';
                            }
                            else {
                                document.getElementById('ctl00_IQCareContentPlaceHolder_' + ID).value = Value;
                            }
                            document.getElementById('ctl00_IQCareContentPlaceHolder_' + ID).disabled = true;


                        }


                    }

                }
                catch (err) {
                    // message.innerHTML = "Input " + err;
                }

            }

            function CallMe(src) {
                var ctrl = document.getElementById(src).value;
                // call server side method
                //alert(ctrl);
            }
            function OpenTreeViewPopup(param1, param2) {
                var val1 = document.getElementById('ctl00$IQCareContentPlaceHolder$' + param1).value;
                var val = param1 + '%' + param2;
                window.open('frmClinicalTreeviewICDCode.aspx?param=' + val + '&ParamValue=' + val1 + '', '', 'toolbars=no,location=no,directories=no,dependent=yes,top=300,left=400,maximize=no,resize=no,width=600,height=500,scrollbars=yes');
            }

            function DataEncounter() {
                document.getElementById('Img8').disabled = true;
                document.getElementById('<%=txtvisitDate.ClientID%>').disabled = true;
            }
            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }



            function ClearTabData(TabId) {
                var elem = document.getElementById("<%= divhidden.ClientID %>").childNodes;
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text") {
                        var TabArray = document.getElementById(elem[i].id).id.split(/-/);
                        var TabArray1 = document.getElementById(elem[i].id).id.split(/_/);
                        if (TabArray[4] == TabId) {
                            if (document.getElementById(elem[i].id).value != '0') {
                                document.getElementById(elem[i].id).value = "";
                            }
                        }
                    }

                    else if (elem[i].type == "radio") {
                        var TabArray = document.getElementById(elem[i].id).id.split(/-/);
                        if (TabArray[4] == TabId) {
                            if (document.getElementById(elem[i].id).checked == true) {
                                document.getElementById(elem[i].id).checked = false;
                            }
                        }
                    }
                    else if (elem[i].childNodes.length > 0) {
                        if (elem[i].childNodes[0].type == "checkbox") {
                            var TabArray = document.getElementById(elem[i].childNodes[0].id).id.split(/_/);
                            if (TabArray[3] == TabId) {
                                if (document.getElementById(elem[i].childNodes[0].id).checked == true) {
                                    document.getElementById(elem[i].childNodes[0].id).checked = false;
                                }
                            }
                        }
                    }

                }

            }

            function StringASCII(TabId) {
                var elem = document.getElementById("<%= divhidden.ClientID %>").childNodes;
                var TabData = "";
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text") {
                        var TabArray = document.getElementById(elem[i].id).id.split(/-/);
                        if (TabArray[4] == TabId) {
                            if (document.getElementById(elem[i].id).value != '0') {
                                TabData = TabData + document.getElementById(elem[i].id).value;
                            }
                        }
                    }

                    else if (elem[i].type == "radio") {
                        var TabArray = document.getElementById(elem[i].id).id.split(/-/);
                        if (TabArray[4] == TabId) {
                            if (document.getElementById(elem[i].id).checked == true) {
                                TabData = TabData + document.getElementById(elem[i].id).value;
                            }
                        }
                    }
                    else if (elem[i].childNodes.length > 0) {
                        if (elem[i].childNodes[0].type == "checkbox") {
                            var TabArray = document.getElementById(elem[i].childNodes[0].id).id.split(/_/);
                            if (TabArray[3] == TabId) {
                                if (document.getElementById(elem[i].childNodes[0].id).checked == true) {
                                    TabData = TabData + document.getElementById(elem[i].childNodes[0].id).value;
                                }
                            }
                        }
                    }

                }
                document.getElementById("<%= hdnStringASCIIValue.ClientID%>").value = TabData;
            }

            function ValidateSave(sender, args) {

                var PrevTabName = document.getElementById("<%=hdnPrevTabName.ClientID%>").value;
                var TabId = document.getElementById("<%=hdnPrevTabId.ClientID%>").value;
                var PrevTabIndex = document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value;

                var elem = document.getElementById("<%= divhidden.ClientID %>").childNodes;
                var TabData = "";
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text") {
                        var TabArray = document.getElementById(elem[i].id).id.split(/-/);
                        if (TabArray[4] == TabId) {
                            if (document.getElementById(elem[i].id).value != '0') {
                                TabData = TabData + document.getElementById(elem[i].id).value;
                            }
                        }
                    }

                    else if (elem[i].type == "radio") {
                        var TabArray = document.getElementById(elem[i].id).id.split(/-/);
                        if (TabArray[4] == TabId) {
                            if (document.getElementById(elem[i].id).checked == true) {
                                TabData = TabData + document.getElementById(elem[i].id).value;
                            }
                        }
                    }
                    else if (elem[i].childNodes.length > 0) {
                        if (elem[i].childNodes[0].type == "checkbox") {
                            var TabArray = document.getElementById(elem[i].childNodes[0].id).id.split(/_/);
                            if (TabArray[3] == TabId) {
                                if (document.getElementById(elem[i].childNodes[0].id).checked == true) {
                                    TabData = TabData + document.getElementById(elem[i].childNodes[0].id).value;
                                }
                            }
                        }
                    }

                }
                var PrevTabData = document.getElementById("ctl00_IQCareContentPlaceHolder_hdnStringASCIIValue").value
                if (TabData != PrevTabData) {
                    var userSelectedYesElement = confirm("" + PrevTabName + " Tab has unsaved data. Do you want to save?");
                    //get the hidden field reference:
                    var CurrenttabId = sender.get_activeTab().get_id().split('_');
                    var CurrentTabIndex = sender._activeTabIndex;
                    var CurrentTabName = sender.get_activeTab()._header.innerHTML;
                    CurrenttabId = CurrenttabId[3];
                    document.getElementById("<%=hdnCurrentTabId.ClientID%>").value = CurrenttabId;
                    document.getElementById("<%=hdnSaveTabData.ClientID%>").value = userSelectedYesElement;
                    document.getElementById("<%=hdnCurrenTabName.ClientID%>").value = CurrentTabName;
                    //document.getElementById("<%=hdnCurrenTabIndex.ClientID%>").value = CurrentTabIndex;
                    if (userSelectedYesElement) {
                        document.getElementById("<%=hdnCurrenTabIndex.ClientID%>").value = CurrentTabIndex;
                        var clickButton = document.getElementById('ctl00_IQCareContentPlaceHolder_TAB_' + TabId + '_btnSave-' + TabId + '');
                        clickButton.click();
                    }
                    else {
                        document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value = CurrentTabIndex;
                        document.getElementById("<%=hdnPrevTabId.ClientID%>").value = CurrenttabId;
                        document.getElementById("<%=hdnPrevTabName.ClientID%>").value = CurrentTabName;
                        if (document.getElementById("<%=hdnVisitId.ClientID%>").value == "0") {
                            ClearTabData(TabId);
                        }
                        else {
                            //window.location.reload() 
                            StringASCII(CurrenttabId);
                        }
                    }
                    //returning what the user selected
                    return userSelectedYesElement;
                }
                else {
                    var CurrenttabId = sender.get_activeTab().get_id().split('_');
                    var CurrentTabName = sender.get_activeTab()._header.innerHTML;
                    var CurrentTabIndex = sender._activeTabIndex;
                    CurrenttabId = CurrenttabId[3];
                    document.getElementById("<%=hdnSaveTabData.ClientID%>").value = false;
                    document.getElementById("<%=hdnPrevTabId.ClientID%>").value = CurrenttabId;
                    document.getElementById("<%=hdnPrevTabName.ClientID%>").value = CurrentTabName;
                    document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value = CurrentTabIndex;
                    StringASCII(CurrenttabId);
                }
            }
        </script>
        <div id="DIVCustomForm" runat="server">
            <h1 class="margin" id="theHeader" runat="server" visible="false">
            </h1>
            <div id="DivVisitDate" class="border center formbg" runat="server">
                <table id="tbl1" cellspacing="6" cellpadding="0" width="100%" border="0" runat="server">
                    <tbody>
                        <tr>
                            <td class="form" align="center" valign="top" colspan="2">
                                <label class="required" for="VisitDate" id="lblvisitdate">
                                    <asp:Label ID="Label1" runat="server" Text="Visit Date:"></asp:Label>
                                </label>
                                <asp:TextBox ID="txtvisitDate" MaxLength="11" runat="server" Width="8%"></asp:TextBox>
                                <img id="Img8" onclick="w_displayDatePicker('<%= txtvisitDate.ClientID %>');" height="22"
                                    alt="Date Helper" hspace="3" src="../images/cal_icon1.gif" width="22" border="0"
                                    style="vertical-align: middle;" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                <asp:HiddenField ID="hdnVisitData" runat="server" />
                                <asp:HiddenField ID="hdnWeight" runat="server" />
                                <asp:HiddenField ID="hdnHeight" runat="server" />
                                <asp:HiddenField ID="hdnBNI" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <asp:Panel class="border formbg" align="left" ID="PnlforTab" runat="server" Width="100%">
                <br />
            </asp:Panel>
            <br />
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%">
                    <tbody>
                        <tr id="TrSignatureAll" runat="server" visible="false">
                            <td class="form" align="center" valign="top" colspan="2">
                                <label for="VisitDate">
                                    Signature:</label>
                                <asp:DropDownList ID="ddSignature" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <asp:TextBox ID="txtSysDate" runat="server" CssClass="textstylehidden"></asp:TextBox>
                            <td class="form" align="center" colspan="2">
                                <asp:HiddenField ID="hdfldDOB" runat="server" />
                                <asp:HiddenField ID="theHitCntrl" runat="server" />
                                <asp:HiddenField ID="hdnPrevTabId" runat="server" />
                                <asp:HiddenField ID="hdnCurrentTabId" runat="server" />
                                <asp:HiddenField ID="hdnPrevTabIndex" runat="server" />
                                <asp:HiddenField ID="hdnCurrenTabIndex" runat="server" />
                                <asp:HiddenField ID="hdnSaveTabData" runat="server" />
                                <asp:HiddenField ID="hdnStringASCIIValue" runat="server" />
                                <asp:HiddenField ID="hdnVisitId" runat="server" />
                                <asp:HiddenField ID="hdnPrevTabName" runat="server" />
                                <asp:HiddenField ID="hdnCurrenTabName" runat="server" />
                                <asp:Button ID="btnsave" Text="Save" runat="server" Visible="false" OnClick="btnsave_Click" />
                                <asp:Button ID="btncomplete" runat="server" Visible="false" Text="Data Quality Check"
                                    OnClick="btncomplete_Click" />
                                <asp:Button ID="btnCancel" Text="Close" runat="server" OnClick="btnCancel_Click">
                                </asp:Button>
                                <asp:Button ID="btnPrint" Text="Print All" Visible="false" runat="server" OnClientClick="WindowPrintAll()" />
                            </td>
                        </tr>                        
                    </tbody>
                </table>
            </div>
        </div>
        <div id="divhidden" runat="server" style="display: none">
        </div>
    </div>
</asp:Content>
