<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Pharmacy.Requests.EditPharmacyPrescription" CodeBehind="EditPharmacyPrescription.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }

        function fnCheckUnCheck() {
            var chk = document.getElementById('<%=hidchkbox.ClientID %>').value;
            var chksplit = chk.split(',');
            if (document.getElementById('<%=ddlTreatment.ClientID %>').value == "222") {

                for (var i = 0; i < chksplit.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksplit[i];
                    if (document.getElementById(cid) != null) {
                        document.getElementById(cid).disabled = true;
                    }
                }
            }
            else {
                for (var i = 0; i < chksplit.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksplit[i];
                    if (document.getElementById(cid) != null) {
                        document.getElementById(cid).disabled = false;
                    }
                }
            }
            if (document.getElementById('<%=ddlTreatment.ClientID %>').value == "225") {
                document.getElementById('<%=pnlPedia.ClientID %>').disabled = true;



            }
            else {
                document.getElementById('<%=pnlPedia.ClientID %>').disabled = false;
            }
        }

        function Redirect(id, name, status) {

            if (name == "Add") {
                window.location.href = "../../ClinicalForms/frmPatient_Home.aspx";
            }
            if (name == "Edit") {
                window.location.href = "../../ClinicalForms/frmPatient_History.aspx";
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

        function OnDoseSucess(result) {
            //  var varduration = document.getElementById(valduration).value;
            var varduration = document.getElementById(this._durationControl).value;
            var vardose = document.getElementById(this._singleDoseControl).value;

            if (result != "0" && vardose != "" && varduration != "") {
                var TotalDose = Math.ceil(vardose * varduration * result)
                //document.getElementById(valTotDose).value = TotalDose;
                document.getElementById(_totalDoseControl).value = TotalDose;
            }
            else {
                //document.getElementById(valTotDose).value = "";
                //document.getElementById(_totalDoseControl).value;
            }
        }
        //**********************************************
        function OnDoseError(error) {

        }
        var _durationControl;
        var _frequencyControl;
        var _singleDoseControl;
        var _totalDoseControl;


        function CalculateTotalDailyDose(valsingleDose, valFrequency, valduration, valTotDose) {

            this._durationControl = valduration;
            this._frequencyControl = valFrequency;
            this._singleDoseControl = valsingleDose;
            this._totalDoseControl = valTotDose;

            var vardose = document.getElementById(valsingleDose).value; // to get value of text box
            var selText = document.getElementById(valFrequency).options[document.getElementById(valFrequency).selectedIndex].text // to get selected text
            var varduration = document.getElementById(valduration).value;
            // var result = Pharmacy_frmPharmacyForm.fnGetFrequencyMultiplier(selText).value;
            PageMethods.fnGetFrequencyMultiplier(selText, OnDoseSucess, OnDoseError);
            //            if (result != "0" && vardose!="" && valduration!="" ) {
            //                var TotalDose = vardose * varduration * result
            //                document.getElementById(valTotDose).value = TotalDose;
            //            }
            //            else {
            //                document.getElementById(valTotDose).value = "";
            //            }



        }


        function fnchecked(blnchecked) {
            var drugid = blnchecked.substring(20);
            //alert(drugid);

            if (document.getElementById("ctl00_IQCareContentPlaceHolder_" + blnchecked).checked) {
                //document.getElementById("divPtnIns" + drugid).style.display = "";

                document.getElementById("ctl00_IQCareContentPlaceHolder_lblPtnInstructions" + drugid).style.display = "";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).style.display = "";
            }
            else {

                //document.getElementById("ptnIns" + drugid).value = "";
                //document.getElementById("ctl00_IQCareContentPlaceHolder_ptnIns" + drugid).value = "";
                //document.getElementById("divPtnIns" + drugid).style.display = "none";
                document.getElementById("ctl00_IQCareContentPlaceHolder_lblPtnInstructions" + drugid).style.display = "none";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).value = "";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).style.display = "none";
            }

        }

    </script>
    <script type="text/javascript">
        function ace1_itemSelected(source, e) {
            var results = eval('(' + e.get_value() + ')');
            var index = source._selectIndex;
            if (index != -1) {
                //source.get_element().value = removeHTMLTags(source.get_completionList().childNodes[index]._value);
                var hdCustID = $get('<%= hdCustID.ClientID %>');
                hdCustID.value = results.DrugId;
            }
        }


        function onClientPopulated(sender, e) {
            var propertyPeople = sender.get_completionList().childNodes;
            for (var i = 0; i < propertyPeople.length; i++) {
                var div = document.createElement("span");
                var results = eval('(' + propertyPeople[i]._value + ')');
                div.innerHTML = "<span style=' float:right; font-weight:bold;margin-right: 5px;'> " + results.AvlQty + "</span>";
                //div.innerHTML = results.AvlQty;
                propertyPeople[i].appendChild(div);

            }

        }


    </script>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        
        .autoextender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 800px;
        }
        .autoextenderlist
        {
            cursor: pointer;
            color: black;
        }
        .autoextenderhighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 800px !important;
        }
        #divwidth div
        {
            width: 800px !important;
        }
    </style>
    <%--<tr>
                                <td class="form" colspan="2">
                                </td>
                            </tr>--%>
    <div class="row center" style="padding-top: 8px; padding-left: 8px; padding-right: 8px;">
        <%--                                            <asp:Button ID="btnPresPrint" CssClass="btn btn-info" Text="Save and Print Prescription"
                                                runat="server" OnClick="btnPresPrint_Click" />--%>
        <asp:UpdatePanel ID="Updatepanel" runat="server">
            <ContentTemplate>
                <%--   Define panel here--%>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-6">
                                <span class="pull-left fa fa-hospital-o fa-1x"><strong class="text-info">Pharmacy Prescription</strong></span><br />
                            </div>
                            <div class="col-md-6">
                                <asp:LinkButton ID="btnRandom"
                                    runat="server"
                                    CssClass="btn btn-success  pull-right"
                                    OnClick="btnRandom_Click">
                                    <span aria-hidden="true" class="glyphicon glyphicon-print"></span> Print Prescription
                                </asp:LinkButton>
                                <%--<asp:Button ID="btnPresPrint" CssClass="btn btn-success  pull-right" Text="Print Prescription"
                                    runat="server" OnClick="btnPresPrint_Click" />--%>
                            </div>

                        </div>

                    </div>
                    <div class="panel-body">
                        <div class="border center pad5 formbg">
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="form" align="center" width="50%">
                                            <div class="col-md-1">
                                                <label class="control-label">
                                                    Age:</label>
                                            </div>
                                            <div class="col-md-2" style="padding-right: 0%">
                                                <asp:TextBox ID="txtYr" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1" style="padding-right: 0">
                                                <label class="control-label pull-left" style="padding-left: 0">
                                                    Yrs</label>
                                            </div>
                                            <div class="col-md-3" style="padding-left: 0%; padding-right: 0%">
                                                <asp:TextBox CssClass="form-control" ID="txtMon" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="padding-left: 0">
                                                <label class="control-label pull-left" style="padding-left: 0">
                                                    Months</label>
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </td>
                                        <td class="form" align="center" width="50%">
                                            <div class="col-md-4 pull-right" style="padding-left: 0%">
                                                <asp:TextBox ID="txtDOB" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 pull-right">
                                                <label class="control-label">
                                                    DOB:</label>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                            <asp:HiddenField ID="hidchkbox" runat="server" />
                                            <asp:HiddenField ID="hdOrderId" runat="server" Value="-1" />
                                            <asp:HiddenField ID="hdOrderStatus" runat="server" Value="-1" />
                                            <asp:HiddenField ID="hdPrescribedBy" runat="server" Value="-1" />
                                            <asp:HiddenField ID="hdDispensedBy" runat="server" Value="-1" />
                                            <asp:HiddenField ID="hdSignature" runat="server" Value="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 35%" align="center">
                                                        <div class="col-md-6 pull-left">
                                                            <label class="patientInfo pull-left">
                                                                Weight (Kg):</label><label class="smalllabel text-info" id="dtwt" runat="server"></label>
                                                                    <asp:TextBox ID="txtWeight" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                                                                
                                                            <div>
                                                    </td>
                                                    <td style="width: 35%" align="center">
                                                        <div class="col-md-6 pull-left">
                                                            <label class="patientInfo pull-left">
                                                                Height (cm):</label><label class="smalllabel text-info" id="dtht" runat="server"></label>
                                                            <asp:TextBox ID="txtHeight" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td style="width: 30%" align="center">
                                                        <div class="col-md-8 pull-left">
                                                            <label class="patientInfo pull-left">
                                                                Body Surface Area( m<sup>2</sup>):</label>
                                                            <asp:Label ID="lblBSA" runat="server" Text=""></asp:Label>
                                                            <asp:TextBox ID="txtBSA" CssClass="form-control input-sm" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 35%" align="left">
                                                        <div class="col-md-6 pull-left">
                                                            <label class="required pull-left">
                                                                *Treatment Program:</label>
                                                            <asp:DropDownList ID="ddlTreatment" CssClass="form-control input-sm" runat="server"
                                                                Style="padding-right: 10%" AutoPostBack="true" OnSelectedIndexChanged="ddlTreatment_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td style="width: 35%" align="center">
                                                        <div class="col-md-6 pull-left">
                                                            <label class="patientInfo pull-left">
                                                                Period Taken:</label>
                                                            <asp:DropDownList ID="ddlPeriodTaken" CssClass="form-control col-md-8 input-sm" runat="server"
                                                                Style="z-index: 2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td style="width: 30%" align="center">
                                                        <div class="col-md-8 pull-left">
                                                            <label class="required pull-left">
                                                                *Drug Provider:</label>
                                                            <asp:DropDownList ID="ddlProvider" CssClass="form-control input-sm" runat="server"
                                                                Style="z-index: 2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trHIVsetFields" runat="server" visible="true">
                                        <td class="form" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 35%" align="center">
                                                        <div class="col-md-6 pull-left">
                                                            <label class="required pull-left">
                                                                *Regimen Line:</label>
                                                            <asp:DropDownList ID="ddlregimenLine" CssClass="form-control" runat="server" Style="z-index: 2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td style="width: 35%" align="center">
                                                        <div class="col-md-8 pull-left">
                                                            <label class="">
                                                                Next Appointment Date:</label>
                                                            <div class="input-group">
                                                                <input id="txtpharmAppntDate" class="form-control" maxlength="11" name="pharmAppointmentDate"
                                                                    runat="server" />
                                                                <span class="input-group-addon">
                                                                    <img id="Img1" onclick="w_displayDatePicker('<%=txtpharmAppntDate.ClientID%>');"
                                                                        height="22" alt="Date Helper" " src="../images/cal_icon.gif" width="22"
                                                                        border="0"  /><span class="smallerlabel" id="Span1">(DD-MMM-YYYY)</span></span>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td style="width: 30%" align="center">
                                                        <div class="col-md-8 pull-left">
                                                            <label class="patientInfo pull-left">
                                                                Reason:</label>
                                                            <asp:DropDownList ID="ddlAppntReason" CssClass="form-control" runat="server" Style="z-index: 2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- .row -->
                        <br />
                        <div class="border center formbg">
                            <br>
                            <table cellspacing="6" cellpadding="0" border="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="border pad5 whitebg" align="left">
                                            <div class="border pad5">
                                                <label class="control-label">
                                                    Select Drug:</label>
                                                <asp:TextBox ID="txtautoDrugName" Width="600px" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" AutoComplete="off" OnTextChanged="txtautoDrugName_TextChanged"
                                                    Font-Names="Courier New"></asp:TextBox>
                                                <div id="divwidth">
                                                </div>
                                                <act:AutoCompleteExtender ServiceMethod="SearchDrugs" MinimumPrefixLength="2" CompletionInterval="30"
                                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtautoDrugName"
                                                    BehaviorID="AutoCompleteEx" OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1"
                                                    runat="server" OnClientPopulated="onClientPopulated" FirstRowSelected="false"
                                                    CompletionListCssClass="autoextender" CompletionListItemCssClass="autoextenderlist"
                                                    CompletionListHighlightedItemCssClass="autoextenderhighlight" CompletionListElementID="divwidth">
                                                    <Animations>
                                              <OnShow>
                                              <Sequence>
                                              <%-- Make the completion list transparent and then show it --%>
                                              <OpacityAction Opacity="0" />
                                              <HideAction Visible="true" />

                                              <%--Cache the original size of the completion list the first time
                                                the animation is played and then set it to zero --%>
                                              <ScriptAction Script="// Cache the size and setup the initial size
                                                                            var behavior = $find('AutoCompleteEx');
                                                                            if (!behavior._height) {
                                                                                var target = behavior.get_completionList();
                                                                                behavior._height = target.offsetHeight - 2;
                                                                                target.style.height = '0px';
                                                                            }" />
                                              <%-- Expand from 0px to the appropriate size while fading in --%>
                                              <Parallel Duration=".4">
                                              <FadeIn />
                                              <Length PropertyKey="height" StartValue="0" 
	                                            EndValueScript="$find('AutoCompleteEx')._height" />
                                              </Parallel>
                                              </Sequence>
                                              </OnShow>
                                              <OnHide>
                                              <%-- Collapse down to 0px and fade out --%>
                                              <Parallel Duration=".4">
                                              <FadeOut />
                                              <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                              </Parallel>
                                              </OnHide>
                                                    </Animations>
                                                </act:AutoCompleteExtender>
                                                <asp:HiddenField ID="hdCustID" runat="server" />
                                            </div>
                                            <br />
                                            <div class="border pad5 whitebg" id="pnlARV" runat="server" visible="false" style="padding-bottom: 2%">
                                                <asp:Panel ID="pnlPedia" runat="server" Height="100%" Width="100%" Wrap="true">
                                                </asp:Panel>
                                            </div>
                                            <br />
                                             <div class="border pad5 whitebg" id="divVaccine" runat="server" visible="false" style="padding-bottom: 2%">
                                                <asp:Panel ID="panelVaccine" runat="server" Height="100%" Width="100%" Wrap="true" CssClass="panel panel-default">
                                                </asp:Panel>
                                            </div>
                                            <br />
                                            <div class="border" id="pnlOI" runat="server" visible="false">
                                                <asp:Panel ID="PnlOIARV" runat="server" Height="100%" Width="100%" Wrap="true" Style="padding-bottom: 2%">
                                                </asp:Panel>
                                            </div>
                                            <script language="javascript" type="text/javascript">
                                                function GetControl() {
                                                    document.forms[0].submit();
                                                }

                                                function CalcualteBSF(txtBSF, Weight, Height) {
                                                    var YR1 = document.getElementById(Weight).value;
                                                    var YR2 = document.getElementById(Height).value;
                                                    if (YR1 == "" || YR2 == "") {
                                                        YR1 = 0;
                                                        YR2 = 0;
                                                    }

                                                    YR1 = parseInt(YR1);
                                                    YR2 = parseInt(YR2);

                                                    var BSF = Math.sqrt(YR1 * YR2 / 3600);
                                                    BSF = BSF.toFixed(2);
                                                    document.getElementById(txtBSF).value = BSF;
                                                }
                                            </script>
                                            <br />
                                            <div class="border" id="pnlOther" runat="server" visible="false">
                                                <asp:Panel ID="PnlOtMed" runat="server" Height="100%" Width="100%" Wrap="true" Style="padding-bottom: 2%">
                                                </asp:Panel>
                                            </div>
                                            <br />
                                            <div class="border" id="pnlTB" runat="server" visible="false">
                                                <asp:Panel ID="pnlOtherTBMedicaton" runat="server" Height="100%" Width="100%" Wrap="true"
                                                    Style="padding-bottom: 2%">
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- .border -->
                        <br>
                        <div class="border center formbg">
                            <h5 class="forms" align="left">Prescription Notes</h5>
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="form">
                                            <asp:TextBox ID="txtClinicalNotes" TextMode="MultiLine" CssClass="form-control textarea"
                                                Width="100%" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                            Wrap="true">
                        </asp:Panel>
                        <div class="border center formbg">
                            <h5 class="forms" align="left">Approval and Signatures</h5>
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="form" width="100%" colspan="2">
                                            <div class="col-md-8" style="padding-right: 0;">
                                                <label class="required pull-left control-label" for="spanorderedby" runat="server"
                                                    id="labeleModiyreason">
                                                    *Reason for modification:</label>
                                            </div>
                                            <div class="col-md-12 pull-left" style="padding-left: 0;">
                                                <asp:TextBox runat="server" ID="txtEditReason" TextMode="MultiLine" Width="100%" CssClass="form-control pull-left textarea"></asp:TextBox>
                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form" width="50%">
                                            <div class="col-md-4" style="padding-right: 0;">
                                                <label class="required pull-left control-label" for="spanorderedby" runat="server"
                                                    id="labelForPrescribedBy">
                                                    *Prescribed by:</label>
                                            </div>
                                            <div class="col-md-6 pull-left" style="padding-left: 0;">
                                                <span  id="spanorderedby">
                                                    <asp:DropDownList ID="ddlPharmOrderedbyName" CssClass="form-control pull-left" runat="Server">
                                                    </asp:DropDownList>
                                                </span><span >
                                                    <asp:Label ID="labelOrderByName" runat="server" Visible="true" Text="" Font-Bold="true"></asp:Label></span>
                                            </div>
                                        </td>
                                        <td class="form" width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left"></td>
                                                    <td align="left">
                                                        <div class="col-md-4">
                                                            <label class="required control-label pull-left" for="pharmOrderedbyDate" runat="server"
                                                                id="labelForPrescribedByDate">
                                                                *Prescribed By Date:</label>
                                                        </div>
                                                        <div class="col-md-6 pull-left">
                                                            <div class="input-group" style="padding-left: 0; padding-right: 0; ">
                                                                <input id="txtpharmOrderedbyDate" class="form-control" maxlength="11" name="pharmOrderedbyDate"
                                                                    runat="server" />
                                                                <span class="input-group-addon">
                                                                    <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtpharmOrderedbyDate.ClientID%>');"
                                                                        height="22" alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                                                        border="0" name="appDateimg" />
                                                                    <span class="smallerlabel" id="appDatespan1">(DD-MMM-YYYY)</span> </span>
                                                            </div>
                                                            <span>
                                                                <asp:Label ID="labelOrderByDate" runat="server" Visible="true" Text="" Font-Bold="true"></asp:Label></span>
                                                        </div>
                                                        <%--<div class="col-md-2 pull-left" style="padding-left: 0%">
                                                            <span style='display: <%= sEdit %>'></span>
                                                        </div>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trDispense" style="display: <%= svDispense %>">
                                        <td class="form" width="50%">
                                            <div class="col-md-4" style="padding-right: 0;">
                                                <label class="required pull-left control-label" runat="server" id="labelForDispensedBy"
                                                    for="spandispensed">
                                                    Dispensed by :</label>
                                            </div>
                                            <div class="col-md-6 pull-left" style="padding-left: 0;">
                                                <span  id="spandispensed">
                                                    <asp:DropDownList ID="ddlDispensedBy" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </span><span>
                                                    <asp:Label ID="labelDispensedBy" runat="server" Visible="true" Text="" Font-Bold="true"></asp:Label></span>
                                            </div>
                                        </td>
                                        <td class="form" width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <div class="col-md-4">
                                                            <label class="required control-label pull-left" for="pharmReportedbyDate" runat="server"
                                                                id="lbldispensedbydate">
                                                                Dispensed by Date:</label>
                                                        </div>
                                                        <div class="col-md-6 pull-left">
                                                            <div class="input-group" style="padding-left: 0; padding-right: 0;">
                                                                <input id="txtpharmReportedbyDate" class="form-control" maxlength="11" name="pharmReportedbyDate"
                                                                    runat="server" />
                                                                <span class="input-group-addon">
                                                                    <img id="appDateimg2" onclick="w_displayDatePicker('<%=txtpharmReportedbyDate.ClientID%>');"
                                                                        height="22" alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                                                        border="0" name="appDateimg2" />
                                                                    <span class="smallerlabel" id="Span2">(DD-MMM-YYYY)</span></span>
                                                            </div>
                                                            <span>
                                                                <asp:Label ID="labelDispensedDate" runat="server" Visible="true" Text="" Font-Bold="true"></asp:Label></span>
                                                        </div>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                <td class="form" colspan="2">
                                </td>
                            </tr>--%>
                                    <tr>
                                        <td class="form" align="center" valign="middle" colspan="2">
                                            <div class="row">
                                                <div class="col-md-4">
                                                </div>
                                                <!-- .col-md-4-->
                                                <div class="col-md-4">
                                                    <label class="required control-label pull-left">
                                                        *Signature:</label>
                                                    <div class="form-group col-md-5">
                                                        <span style='display: <%= sEdit %>'>
                                                            <asp:DropDownList ID="ddlPharmSignature" CssClass="form-control" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </span><span style='display: <%= sView %>'>
                                                            <asp:Label ID="labelSignature" runat="server" Visible="true" Text="" Font-Bold="true"></asp:Label></span>
                                                    </div>
                                                </div>
                                                <!-- .col-md-4-->
                                                <div class="col-md-4">
                                                </div>
                                                <!-- .col-md-4-->
                                            </div>
                                            <!-- .row -->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pad5 center" colspan="2">
                                            <asp:Button ID="btnsave" CssClass="btn btn-info" runat="server" Text="Save Prescription"
                                                OnClick="btnsave_Click" />
                                            <asp:Button ID="btnOk" CssClass="textstylehidden btn btn-success" Style="display: none"
                                                runat="server" Text="OK " OnClick="btnOk_Click" />
                                            <asp:Button ID="btnPrint" CssClass="btn btn-info" Text="Print Pharmacy Form" runat="server"
                                                OnClientClick="WindowPrint()" />
                                            <%--                                            <asp:Button ID="btnPresPrint" CssClass="btn btn-info" Text="Save and Print Prescription"
                                                runat="server" OnClick="btnPresPrint_Click" />--%>
                                           
                                            <asp:Button ID="btncancel"  runat="server" Text="Close" CssClass="btn btn-info"   OnClick="btncancel_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- .panel-body-->
                </div>
                <!-- .panel-default -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtautoDrugName"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btncancel"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnPrint"></asp:PostBackTrigger>
               <%-- <asp:PostBackTrigger ControlID="btnRandom"></asp:PostBackTrigger>--%>
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </div>
</asp:Content>
