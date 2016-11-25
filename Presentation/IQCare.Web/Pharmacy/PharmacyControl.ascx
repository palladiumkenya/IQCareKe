<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PharmacyControl.ascx.cs"
    Inherits="IQCare.Web.Pharmacy.PharmacyControl" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
            window.location.href = "../ClinicalForms/frmPatient_Home.aspx";
        }
        if (name == "Edit") {
            window.location.href = "../ClinicalForms/frmPatient_History.aspx";
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
            var TotalDose = vardose * varduration * result
            //document.getElementById(valTotDose).value = TotalDose;
            document.getElementById(_totalDoseControl).value = TotalDose;
        }
        else {
            //document.getElementById(valTotDose).value = "";
            document.getElementById(_totalDoseControl).value;
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

        var vardose = document.getElementById(valsingleDose).value;
        var selText = document.getElementById(valFrequency).options[document.getElementById(valFrequency).selectedIndex].text
        var varduration = document.getElementById(valduration).value;
        PageMethods.fnGetFrequencyMultiplier(selText, OnDoseSucess, OnDoseError);
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
<style type="text/css">
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
<div class="center" style="padding-top: 8px; padding-left: 8px; padding-right: 8px;">
    <asp:UpdatePanel ID="upLevel1" runat="server">
        <ContentTemplate>
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" align="center" width="50%">
                                <label class="right35">
                                    Age:</label>
                                <asp:TextBox ID="txtYr" runat="server" Width="50"></asp:TextBox>Yrs
                                <asp:TextBox ID="txtMon" runat="server" Width="50"></asp:TextBox>Months
                            </td>
                            <td class="form" align="center" width="50%">
                                <label class="right35">
                                    DOB:</label>
                                <asp:TextBox ID="txtDOB" runat="server" Width="80"></asp:TextBox>
                                <asp:HiddenField ID="hidchkbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="center" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 35%" align="center">
                                            <label class="right35" style="width: 5%">
                                                Weight:</label>
                                            <asp:TextBox ID="txtWeight" MaxLength="4" runat="server"></asp:TextBox>Kg
                                            <label class="smalllabel" id="dtwt" runat="server">
                                            </label>
                                        </td>
                                        <td style="width: 35%" align="center">
                                            <label class="right35" style="width: 7%">
                                                Height:</label>
                                            <asp:TextBox ID="txtHeight" MaxLength="4" runat="server"></asp:TextBox>cm
                                            <label class="smalllabel" id="dtht" runat="server">
                                            </label>
                                        </td>
                                        <td style="width: 30%" align="center">
                                            <label class="patientInfo">
                                                Body Surface Area:</label>
                                            <asp:Label ID="lblBSA" runat="server" Text=""></asp:Label>
                                            <asp:TextBox ID="txtBSA" runat="server" />m<sup>2</sup>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 30%" align="center">
                                            <label class="required">
                                                *Treatment Program:</label>
                                            <asp:DropDownList ID="ddlTreatment" runat="server" Style="z-index: 2" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTreatment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 30%" align="center">
                                            <label class="">
                                                Period Taken:</label>
                                            <asp:DropDownList ID="ddlPeriodTaken" runat="server" Style="z-index: 2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 30%" align="center">
                                            <label class="required">
                                                *Drug Provider:</label>
                                            <asp:DropDownList ID="ddlProvider" runat="server" Style="z-index: 2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trHIVsetFields" runat="server" visible="true">
                            <td class="form" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 30%" align="center">
                                            <label class="required">
                                                *Regimen Line:</label>
                                            <asp:DropDownList ID="ddlregimenLine" runat="server" Style="z-index: 2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40%" align="center">
                                            <label class="">
                                                Next Appointment Date:</label>
                                            <input id="txtpharmAppntDate" maxlength="11" size="11" name="pharmAppointmentDate"
                                                runat="server" />
                                            <img id="Img1" onclick="w_displayDatePicker('<%=txtpharmAppntDate.ClientID%>');"
                                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                border="0" name="appDateimg" /><span class="smallerlabel" id="Span1">(DD-MMM-YYYY)</span>
                                        </td>
                                        <td style="width: 30%" align="center">
                                            <label>
                                                Reason:</label>
                                            <asp:DropDownList ID="ddlAppntReason" runat="server" Style="z-index: 2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upLevel2" runat="server">
        <ContentTemplate>
            <div class="border pad5" id="dataEntry">
                <table style="width: 100%;" width="100%" class="border pad5 whitebg">
                    <tr style="height: 20px; width: 880px;">
                        <th align="left" style="padding-bottom:2%">
                            Select Drug
                        </th>
                        <td class="border pad5 whitebg">
                           <div class="col-md-12">
                            <asp:TextBox ID="txtautoDrugName" CssClass="form-control" Width="400px" runat="server" AutoPostBack="true"
                                AutoComplete="off" OnTextChanged="txtautoDrugName_TextChanged" Font-Names="Courier New"
                                TabIndex="13"></asp:TextBox></div> 

                            <div id="divwidth" class="col-md-12">
                            </div>
                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchDrugs" MinimumPrefixLength="2"
                                CompletionInterval="30" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtautoDrugName"
                                BehaviorID="AutoCompleteEx" OnClientItemSelected="ace1_itemSelected" ID="aceSearchDrugs"
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
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:HiddenField ID="hdCustID"  runat="server" />
                        </td>
                    </tr>
                    <tr style="height: 20px; width: 880px;" style="padding-top:2%">
                        <th align="left">
                            Dose
                        </th>
                        <td class="border pad5 whitebg">
                            <asp:TextBox ID="textDose"  CssClass="form-control" runat="server" AutoCompleteType="None" Width="50px" TabIndex="14"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDose" runat="server" TargetControlID="textDose"
                                FilterType="Custom, Numbers" ValidChars="." />
                        </td>
                    </tr>
                    <tr>
                        <th align="left">
                            Frequency
                        </th>
                        <td width="50px">
                            <telerik:RadComboBox ID="ddlFrequency" runat="server" Width="50px" AutoPostBack="false"
                                MarkFirstMatch="false" HighlightTemplatedItems="true" AppendDataBoundItems="false"
                                TabIndex="15" OnItemDataBound="ddlFrequency_ItemDataBound" >
                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="white-space: nowrap; text-align: left;">
                                                <%# DataBinder.Eval(Container.DataItem, "FrequencyName")%>
                                            </td>
                                            <td style="display: none;">
                                                <%# DataBinder.Eval(Container.DataItem, "FrequencyId")%>
                                                <%# DataBinder.Eval(Container.DataItem, "multiplier")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderTemplate>
                                </HeaderTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="left">
                            Duration (Days)
                        </th>
                        <td>
                            <asp:TextBox ID="textDuration" runat="server" AutoCompleteType="None" Width="50px"
                                TabIndex="16"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDuration" runat="server" TargetControlID="textDuration"
                                FilterType="Custom, Numbers" ValidChars="." />
                        </td>
                    </tr>
                    <tr>
                        <th align="left">
                            Quantity Prescribed
                        </th>
                        <td>
                            <asp:TextBox ID="textQuantityPrescribed" runat="server" AutoCompleteType="None" Width="50px"
                                TabIndex="17"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftePrescribed" runat="server" TargetControlID="textQuantityPrescribed"
                                FilterType="Custom, Numbers" ValidChars="." />
                        </td>
                    </tr>
                    <tr>
                        <th align="left">
                            Quantity Dispensed
                        </th>
                        <td>
                            <asp:TextBox ID="textQuantityDispensed" runat="server" AutoCompleteType="None" Width="50px"
                                TabIndex="18"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDispensed" runat="server" TargetControlID="textQuantityDispensed"
                                FilterType="Custom, Numbers" ValidChars="." />
                        </td>
                    </tr>
                    <tr>
                        <th align="left">
                            Prophylaxis
                        </th>
                        <td align="left">
                            <asp:RadioButtonList ID="rblProhylaxis" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false">
                                <asp:ListItem Text="Yes" Value="Yes" />
                                <asp:ListItem Text="No" Value="No" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th align="left">
                            Patient Instructions:
                        </th>
                        <td>
                            <asp:TextBox ID="textPatientInstruction" runat="server" AutoCompleteType="None" Columns="45"
                                Rows="3" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td align="center" style="width: 60px; height: 20px">
                            <asp:Button ID="buttonAdd" runat="server" Text="Add" Width="60px" Height="20px" TabIndex="21"
                                OnClick="buttonAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr class="form">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upLevel3" runat="server">
        <ContentTemplate>
            <div class="border pad5 whitebg">
                <asp:Repeater ID="rptDrugs" runat="server" OnItemCommand="rptDrugs_ItemCommand" OnItemDataBound="rptDrugs_ItemDataBound">
                    <HeaderTemplate>
                        <table cellpadding="2" cellspacing="1" border="0" style="width: 100%">
                            <tr style="background-color: #DCDCDF">
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px">
                                    Drug Name
                                </td>
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px; width: 40px;">
                                    Dose
                                </td>
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px; width: 50px;">
                                    Frequency
                                </td>
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px; width: 60px;"
                                    title="Duration days">
                                    Duration (days)
                                </td>
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px; width: 60px"
                                    title="Quantity prescribed">
                                    Qty Prescribed
                                </td>
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px; width: 60px"
                                    title="Quantity Dispensed">
                                    Qty Dispensed
                                </td>
                                <td style="padding: 2px; padding-left: 5px; font-weight: bold; font-size: 11px" title="Instructions">
                                    Instruction
                                </td>
                                <td style="width: 16px">
                                    &nbsp;
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td colspan="8" style="padding-left: 3px; padding-right: 3px">
                                <asp:Panel ID="pnlDrugDivider" runat="server" Visible="false" Style="font-weight: bold;
                                    padding: 2px; margin-top: 8px; background-color: #E0E0E0; border-bottom: solid 1px #C0C0C0;
                                    width: 100%">
                                    <asp:Label ID="lblDrugDivider" runat="server"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[drugname]")%>
                            </td>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[drugdose]")%>
                            </td>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[frequency]")%>
                            </td>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[duration]")%>
                            </td>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[qtyprescribed]")%>
                            </td>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[qtydispensed]")%>
                            </td>
                            <td style="padding: 2px; padding-left: 5px;">
                                <%# DataBinder.Eval(Container.DataItem, "[instructions]")%>
                            </td>
                            <td>
                                <div style="white-space: nowrap">
                                    <asp:ImageButton ID="cmdDepUpdate" ToolTip="Modify record" runat="server" ImageUrl="~/Images/Edit.gif"
                                        CommandName="UPDATE" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"[entryId]") %>'>
                                    </asp:ImageButton>
                                    <span style="display: <% = sVid %>; white-space: nowrap">
                                        <asp:ImageButton ID="cmdDepDelete" ToolTip="Remove record" runat="server" ImageUrl="~/Images/del.gif"
                                            CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"[entryId]") %>'>
                                        </asp:ImageButton>
                                    </span>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upLevel4" runat="server">
        <ContentTemplate>
            <div class="border center formbg">
                <h2 class="forms" align="left">
                    Prescription Notes</h2>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form">
                                <asp:TextBox ID="txtClinicalNotes" TextMode="MultiLine" CssClass="textarea" Width="100%"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="border center formbg">
                <h2 class="forms" align="left">
                    Approval and Signatures</h2>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <label class="required">
                                                *Prescribed by:</label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlPharmOrderedbyName" runat="Server">
                                            </asp:DropDownList><asp:Label
                                                    ID="labelPrescribedBy" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="form" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <label class="required" for="pharmOrderedbyDate">
                                                *Prescribed By Date:</label>
                                        </td>
                                        <td align="left">
                                            <input id="txtpharmOrderedbyDate" maxlength="11" size="11" name="pharmOrderedbyDate"
                                                runat="server" />
                                            <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtpharmOrderedbyDate.ClientID%>');"
                                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                border="0" name="appDateimg" />
                                            <span class="smallerlabel" id="appDatespan1">(DD-MMM-YYYY)</span><asp:Label
                                                    ID="labelPrescriptionDate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trDispense" runat="server">
                            <td class="form" width="50%">
                                <asp:Panel runat="server" ID="pnlDispBy">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td align="right">
                                                <label id="lbldispensedby" class="required" runat="server">
                                                    Dispensed by :</label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlPharmReportedbyName" runat="server">
                                                </asp:DropDownList><asp:Label
                                                    ID="labelDispensedBy" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td class="form" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <label id="lbldispensedbydate" class="required" runat="server" for="pharmReportedbyDate">
                                                Dispensed by Date:</label>
                                        </td>
                                        <td align="left">
                                            <input id="txtpharmReportedbyDate" maxlength="11" size="11" name="pharmReportedbyDate"
                                                runat="server" />
                                            <img id="appDateimg2" onclick="w_displayDatePicker('<%=txtpharmReportedbyDate.ClientID%>');"
                                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                border="0" name="appDateimg" /><span class="smallerlabel" id="appDatespan2">(DD-MMM-YYYY)</span><asp:Label
                                                    ID="labelDispensedDate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="center" valign="middle" colspan="2">
                                <label class="required right35" style="width: 1%">
                                    *Signature:</label>
                                <asp:DropDownList ID="ddlPharmSignature" runat="server">
                                    <asp:ListItem Selected="True" Text="Select"></asp:ListItem>
                                    <asp:ListItem Text="No Signature"></asp:ListItem>
                                    <asp:ListItem Text="Patient's Signature"></asp:ListItem>
                                    <asp:ListItem Text="Adherance counsellor signature"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="labelSignature" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2" style="text-align: center">
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                <asp:Button ID="btncancel" runat="server" Text="Close" OnClick="btncancel_Click" />
                                <asp:Button ID="btnOk" runat="server" CssClass="textstylehidden" Text="OK" OnClick="btnOk_Click" />
                                <asp:Button ID="btnPrint" Text="Print Pharmacy Form" runat="server" OnClientClick="WindowPrint()" />
                                <asp:Button ID="btnPresPrint" Text="Print Prescription" runat="server" OnClick="btnPresPrint_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
