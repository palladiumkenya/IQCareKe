<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="Registration.aspx.cs" Inherits="IQCare.Web.Patient.Register" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">

        function CalcualteAge(txtAge, txtmonth, txtDT1, txtDT2) {
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
                if (dt1.length < 11) {
                    document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365)) - 1;
                    var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365) - 1;

                    document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
                }
                else {
                    document.getElementById(txtAge).value = "";
                    document.getElementById(txtmonth).value = "";
                }
            }
            else {
                if (dt1.length < 11) {
                    document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
                    var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365);
                    document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
                }
                else {
                    document.getElementById(txtAge).value = "";
                    document.getElementById(txtmonth).value = "";
                }
            }
        }
        function jsAreaClose(id) {
            document.getElementById(id).style.display = 'none';
        }
        var pickedUp = new Array("", false);
        function getReadyToMove(element, evt) {
            pickedUp[0] = element;
            pickedUp[1] = true;
        }
        function SetValue(theObject, theValue) {
            document.getElementById('ctl00_IQCareContentPlaceHolder_' + theObject).value = theValue;
            document.forms[0].submit();
        }
        function WindowPrint() {
            window.print();
        }
        function pageLoad() {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(EndAJAXRequestHandler);
        }

        function EndAJAXRequestHandler(sender, args) {

            if (args.get_error() != undefined && args.get_error().httpStatusCode == '500') {
                var errorMessage = args.get_error().message;
                args.set_errorHandled(true);
                alert("IQCare Application Framework encountered an unrecoverable error:\n" + errorMessage + "\n\nPlease report this error to the support team.");
            }
            var panelProg = $get('divProgressBar');
            panelProg.style.display = 'none';

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
        function onSuccess() { }
        function onFailure() { }
        function calculateDOB() {
            // PageMethods.btncalculate_DOB_Click(onSuccess, onFailure);
        }
        //**********************************************
        function OnPageError(error) {
            if (error) alert("IQCare Application Framework encountered an unrecoverable error:\n" + error.get_message());
            else alert("IQCare Application Framework encountered an unrecoverable error.");
        }
        function ValidateAge() {
            var hidid = document.getElementById('<%=hdnIds.ClientID %>').value;
            var Age = document.getElementById('<%=txtageCurrentYears.ClientID %>').value;
            PageMethods.EnableControlAge(hidid, Age, OnDateValidate, OnPageError);

        }
        function fnshow() {

            var status = '<%=Request.QueryString["name"]%>';
            if (status != 'Edit') {
                var fname = document.getElementById('<%=txtfirstName.ClientID %>').value;
                var mname = "";
                var lname = document.getElementById('<%=txtlastName.ClientID %>').value;
                var address = "";
                var phone = "";
                if (fname != '' && lname != '') {
                    PageMethods.GetDuplicateRecord(fname, mname, lname, address, phone, OnDuplicateFind, OnPageError);

                }
            }
        }
    </script>
    <div class="row">
        <span class="text-capitalize pull-left glyphicon-text-size= fa-2x" id="tHeading"
            runat="server"><i class="fa fa-cubes fa-2x" aria-hidden="true"></i><span class="text-info">Patient Registration</span></span>
    </div>
    <hr />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel panel-heading">
                <i class="glyphicon glyphicon-list-alt fa-1x pull-left" aria-hidden="true"><strong>Registration</strong></i><br />
            </div>
            <div class="panel-body" style="margin-top: 0; padding-top: 5px">
                <asp:Panel class="border center formbg" ID="panelStatic" Width="100%" runat="server"
                    Style="margin-top: 0; padding-top: 0px">
                    <div class="popupWindow center formbg" id='search_popup' onclick="javascript:getReadyToMove('search_popup', event);"
                        style="display: none; padding: 5px; z-index: 1000; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; background-color: Black">
                        <table cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                            <tr bgcolor="#666699">
                                <td align="right">
                                    <span style="cursor: hand" onclick="jsAreaClose('search_popup')">
                                        <img alt="Hide Popup" src="../Images/close.gif" border="0" /></span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 150px">
                                    <div id="showresult">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="alert">Please check for duplicates
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label id="lblPName" class="required control-label pull-left " for="patientname">
                                    <span class="text-danger">*</span> Patient Names:</label><br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="FName" class="required control-label pull-left">
                                        <small>First Name :</small></label>
                                    <asp:TextBox ID="txtfirstName" CssClass="form-control input-sm" MaxLength="50" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEFName" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtfirstName" ValidChars="-,.@*' ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                                <!-- .form-group-->
                            </div>
                            <!-- .col-md-3 -->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="MName" class="control-label pull-left">
                                        <small>Middle Name : </small>
                                    </label>
                                    <asp:TextBox ID="txtmiddleName" CssClass="form-control input-sm" MaxLength="50" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEMName" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtmiddleName" ValidChars="-,.@*' ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                                <!-- .form-group-->
                            </div>
                            <!-- .col-md-3 -->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <span id="LName" class="required control-label pull-left"><small>Last Name: </small>
                                    </span>
                                    <asp:TextBox ID="txtlastName" CssClass="form-control input-sm" MaxLength="50" onchange="fnshow();"
                                        onblur="fnshow();" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTELName" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtlastName" ValidChars="-,.@*' ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                                <!-- .form-group-->
                            </div>
                            <!-- .col-md-3 -->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="lblIQRef" class="control-label pull-left">
                                        IQCare Reference:</label>
                                    <asp:TextBox ID="txtIQCareRef" CssClass="form-control input-sm" MaxLength="11" runat="server"
                                        TabIndex="100" ReadOnly="true"></asp:TextBox>
                                </div>
                                <!-- .form-group -->
                            </div>
                            <!-- .col-md-3 -->
                        </div>
                        <!-- .row -->
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="lblgender" class="required control-label pull-left" for="gender">
                                        <span class="text-danger">*</span>Sex :</label>
                                    <asp:DropDownList ID="ddgender" CssClass="form-control input-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <!-- .form-group-->
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label pull-left" for="maritalstatus">
                                        Marital Status:</label>
                                    <asp:DropDownList ID="ddmaritalStatus" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <!-- .form-group-->
                            </div>
                            <!-- .col-md-3 -->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="lblenroldate" class="required control-label pull-left" for="RegDate">
                                        *Registration Date:<small class="text-muted">[DD-MMM-YYYY]</small></label>
                                    <div class="col-md-11" style="padding-right: 0px; padding-left: 0px">
                                        <asp:TextBox ID="txtRegDate" CssClass="form-control" runat="server" MaxLength="11"
                                            onblur="DateFormat(this,this.value,event,false,'3');ValidateAge();" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onfocus="javascript:vDateType='3'"></asp:TextBox>
                                    </div>
                                    <!-- .col-md-11 -->
                                    <div class="col-md-1" style="padding-left: 0px">
                                        <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtRegDate.ClientID %>');"
                                            src="../Images/cal_icon.gif" width="20" /><%--<small>DD-MMM-YYYY</small>  --%>
                                    </div>
                                    <!-- .col-md-1-->
                                </div>
                                <!-- .form-group -->
                            </div>

                        </div>
                        <!-- .row -->
                        <div class="row">
                            <!-- .col-md-3-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="lblDOB" class="required control-label pull-left" for="DOB">
                                        <span class="text-danger">*</span> Date of Birth:<small class="text-muted"> [DD-MMM-YYYY]</small></label>
                                    <div class="col-md-11" style="padding-right: 0px; padding-left: 0px">
                                        <asp:TextBox ID="TxtDOB" CssClass=" form-control input-sm" MaxLength="11" runat="server"
                                            onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onfocus="javascript:vDateType='3'"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="col-md-1" style="padding-left: 0px">
                                        <img onclick="w_displayDatePicker('<%= TxtDOB.ClientID %>');" height="22" alt="Date Helper"
                                            hspace="3" src="../Images/cal_icon.gif" width="20" border="0" style="z-index: auto" />
                                    </div>
                                </div>
                                <!-- .fomr-group-->
                            </div>
                            <!-- .col-md-3-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <%--<span class="control-label pull-left text-bold" style="padding-right:20px">DD-MMM-YYYY </span>--%>
                                    <input id="rbtndobPrecExact" onmouseup="up(this);" onfocus="up(this);" onclick="down(this)"
                                        type="radio" class="pull-left" value="1" name="dobPrecision" runat="server" />
                                    <span class="control-label pull-left" style="padding-left: 10px; padding-right: 10px">Exact </span>
                                    <input id="rbtndobPrecEstimated" onmouseup="up(this);" onfocus="up(this);" onclick="down(this)"
                                        type="radio" value="0" name="dobPrecision" runat="server" class="pull-left" />
                                    <span class="control-label pull-left " style="padding-left: 10px">Estimated</span>
                                </div>
                            </div>
                            <!-- .col-md-3-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="required control-label pull-left" for="Age">
                                                Age:</label>
                                        </div>
                                        <div class="col-md-3" style="padding-right: 0px">
                                            <asp:TextBox ID="txtageCurrentYears" CssClass="form-control pull-left" MaxLength="2"
                                                runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1" style="padding-left: 1%">
                                            <span class="control-label"><strong>Yrs</strong></span>
                                        </div>
                                        <div class="col-md-2" style="padding-right: 1%; padding-left: 1%">
                                            <asp:TextBox ID="txtageCurrentMonths" MaxLength="2" ReadOnly="true" runat="server"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1" style="padding-left: 1%">
                                            <span class="control-label"><strong>mths</strong></span>
                                        </div>
                                        <div class="col-md-4" style="padding-left: 5%">
                                            <asp:Button ID="btncalculate_DOB" CssClass="btn btn-info" runat="server" Text="Calculate DOB"
                                                OnClick="btncalculate_DOB_Click" />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <!-- .form-group -->
                                </div>
                                <!-- .form-group -->
                            </div>
                            <!-- .col-md-3-->
                        </div>
                        <!-- .row-->
                    </div>
                </asp:Panel>
                <asp:Panel class="border center formbg" ID="PnlDynamicElements" runat="server" Style="padding-top: 5px">
                </asp:Panel>
                <div class="row">
                    <asp:Panel class="center formbg" ID="panelButtons" runat="server">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSysDate" CssClass="textstylehidden" runat="server"> </asp:TextBox>
                            <asp:HiddenField ID="HdnCntrl" runat="server" />
                            <asp:HiddenField ID="hdnIds" runat="server" />
                            <asp:Button ID="btncontinue" runat="server" Text="Continue" OnClick="btncontinue_Click"
                                CssClass="btn btn-info col-md-3" Style="margin-right: 5px" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                                CssClass="btn btn-danger col-md-3" Style="margin-right: 5px" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-info col-md-3" />
                        </div>
                        <div class="col-md-3">
                        </div>
                    </asp:Panel>
                </div>
                <br />
            </div>
            <!-- .panel-body-->
        </div>
        <!--.panel -->
    </div>
    <!-- .row well-sm-->
</asp:Content>
