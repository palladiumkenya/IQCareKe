<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmPharmacyDispense_PatientOrder.aspx.cs" Inherits="IQCare.Web.PMSCM.frmPharmacyDispense_PatientOrder"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="~/UC/UserControl_Loading.ascx" TagPrefix="IQ" TagName="UserControl_Loading" %>

<%--<%@ Register Src="../ClinicalForms/UserControl/UserControl_Loading.ascx" TagName="UserControl_Loading"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <link href="../Content/bootstrap/css/bootstrap.css" rel="stylesheet"  type="text/css" />
    <link href="../Content/bootstrap/css/iqcare.ke-font.css" rel="stylesheet" />
    <link href="../Content/bootstrap/css/parsley.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap/css/CustomStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Content/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Content/bootstrap/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Content/bootstrap/plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>

    <script src="../Content/bootstrap/js/bootstrap-switch.js" type="text/javascript"></script>
    <script src="../Content/bootstrap/js/highlight.js" type="text/javascript"></script>
    <script src="../Content/bootstrap/plugins/select2/select2.full.js" type="text/javascript"></script>
    
    <script src="../Content/bootstrap/dist/js/demo.js" type="text/javascript"></script>
    <script src="../Content/bootstrap/bootbox.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hidDifferenciatedCare").val() == 1) {
                $("#chkYesterdayMed").bootstrapSwitch('state', true);
            }
            $("#chkDifferenciatedCare").on('switchChange.bootstrapSwitch', function (event, state) {
                if (GetSwitchValue("chkDifferenciatedCare") == 1) {
                    $("#hidDifferenciatedCare").val("1");
                } else {
                    $("#hidDifferenciatedCare").val("0");
                }
            });
        });
        function GetSwitchValue(ctrlName) {
            var ctrlVal = $("#" + ctrlName).bootstrapSwitch('state');
            if (ctrlVal)
                ctrlVal = 1;
            else
                ctrlVal = 0;
            return ctrlVal;
        }

        function ace1_itemSelected(source, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();
        }

        function openDrugHistory() {
            window.open('frmPharmacy_DrugHistory.aspx', '_blank', 'height=500,width=1100,scrollbars=yes');
        }

        function openAllergyPage() {
            var PatientID = '<%= Session["PatientID"] %>';
            window.open('frmAllergy.aspx?name=Add&PatientId=' + PatientID + '&opento=popup', '_blank', 'height=500,width=1100,scrollbars=yes');
        }

        function ClearTextBox(txtDrug) {
            document.getElementById(txtDrug).value = "";
        }

        function ValidateRequired() {
            var name = document.getElementById('<%=ddlTreatmentProg.ClientID %>').value;
            if (name == "0") {
                alert("Please Select Treatment Program ");
                return false;
            }
            name = document.getElementById('<%=ddlregimenLine.ClientID %>').value;
            visible = document.getElementById('<%=ddlregimenLine.ClientID %>').style.visibility;
            if (name == "0" && visible != 'hidden') {

                alert("Please Select Regimen line");
                document.getElementById("<%= ddlregimenLine.ClientID%>").style.visibility = "visible";
                document.getElementById("<%= lblregimenLine.ClientID%>").style.visibility = "visible";
               document.getElementById("<%=hdnregimenLine.ClientID %>").value = "visible";
                //Regimen Code
                document.getElementById("<%= ddlRegimenCode.ClientID%>").style.visibility = "visible";
                document.getElementById("<%= lblregimenCode.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=hdnregimenCode.ClientID %>").value = "visible";
                return false;
                
            }
            return true;
        }

        function chkQtyDispGreaterQtyPres(qtyPres, qtyDisp, qtyRefillQty) {
            var disp = document.getElementById(qtyDisp).value;
            var pres = document.getElementById(qtyPres).value;
            var rfill = document.getElementById(qtyRefillQty).value;
            var totalDesp = parseFloat(disp) + parseFloat(rfill);
            if (parseFloat(totalDesp) > parseFloat(pres)) {
                alert('Quantity dispensed is greater than quantity prescribed.');
            }
        }

        function disbleDateImage() {
            //document.getElementById("Img1").style.display = 'none'; //.setAttribute('disabled', 'disabled');
        }

        function DispenseBySelect(selectId, qtyDisp1, qtyDisp2) {
            if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Select") {
                //document.getElementById(qtyDisp1).value = "";
                //document.getElementById(qtyDisp2).value = "";
                document.getElementById(qtyDisp1).disabled = true;
                //document.getElementById(qtyDisp2).disabled = true;
            }
            else {
                //document.getElementById(otherControlID).value = "";
                document.getElementById(qtyDisp1).disabled = false;
                //document.getElementById(qtyDisp2).disabled = false;
            }
        }

        function disableGVColumn(strGV, strColName) //gv id as string 
        {
            //var gridViewID = document.getElementById(gv);
            var index = 0;
            for (i = 2; i <= strGV.rows + 1; i++) {
                if ((i.toString()).length == 1) //concatenate like 01, 02 if row length is less than 10 
                {
                    index = "0" + i.toString();
                }
                else {
                    index = i.toString(); //else index of column would be 11.... on words 
                }
                var colID = strGV + "_ctl" + index + "_" + strColName;
                alert(colID);
                document.getElementById(colID).disabled = true;
            }
        }

        function hideColumn(selectId, gv) {
            rows = document.getElementById(gv).rows;
            if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Select") {

                for (i = 0; i < rows.length; i++) {
                    //rows[i].cells[12].style.display = "none";
                    rows[i].cells[12].disabled = 'disabled';
                }
            }
            else {
                for (i = 0; i < rows.length; i++) {
                    //rows[i].cells[12].style.display = "block";
                    //rows[i].cells[12].disabled = 'disabled';
                    rows[i].cells[12].setAttribute('disabled', true);
                    //alert(rows[i].cells[12].text);
                }
            }
        }

        function showRegimenDDown(show) {
            if (show == 'false') {

                document.getElementById("<%= ddlregimenLine.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%= lblregimenLine.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=hdnregimenLine.ClientID %>").value = "hidden";
                //Regimen Code
                document.getElementById("<%= ddlRegimenCode.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%= lblregimenCode.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=hdnregimenCode.ClientID %>").value = "hidden";
            }
            else {
                document.getElementById("<%= ddlregimenLine.ClientID%>").style.visibility = "visible";
                document.getElementById("<%= lblregimenLine.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=hdnregimenLine.ClientID %>").value = "visible";
                //Regimen Code
                document.getElementById("<%= ddlRegimenCode.ClientID%>").style.visibility = "visible";
                document.getElementById("<%= lblregimenCode.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=hdnregimenCode.ClientID %>").value = "visible";
            }

            var IsEDC = $("#hidDifferenciatedCare").val();
            if (IsEDC > 0) {
                $("#chkYesterdayMed").bootstrapSwitch('state', true);
            }
        }

        function resetPosition(object, args) {
            var tb = object._element;
            var tbposition = findPositionWithScrolling(tb);
            var xposition = tbposition[0] - 75;
            var yposition = tbposition[1] + 10; // 22 textbox height 
            var ex = object._completionListElement;
            //if (ex)
            // $common.setLocation(ex, new Sys.UI.Point(xposition, yposition));
        }
        function findPositionWithScrolling(oElement) {
            if (typeof (oElement.offsetParent) != 'undefined') {
                var originalElement = oElement;
                for (var posX = 0, posY = 0; oElement; oElement = oElement.offsetParent) {
                    posX += oElement.offsetLeft;
                    posY += oElement.offsetTop;
                    if (oElement != originalElement && oElement != document.body && oElement != document.documentElement) {
                        posX -= oElement.scrollLeft;
                        posY -= oElement.scrollTop;
                    }
                }
                return [posX, posY];
            } else {
                return [oElement.x, oElement.y];
            }
        }

        function AlertRegimen(txtLastReg) {
            //var retVal = confirm("You have picked a different regimen. This patient is currently on " + txtLastReg + "");
            var retVal = confirm("The regimen prescribed is different from the one previously dispensed would you like to continue?");
            if (retVal == true) {
                document.getElementById('btnHidAddDrug').click();
            }
            else {
                return false;
            }
        }
        //


    </script>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtprescriptionDate').datepicker({ autoclose: true });
            $('#txtDispenseDate').datepicker({ autoclose: true });
            $("#chkDifferenciatedCare").bootstrapSwitch('state', false);

        }
        //Calling MyFunction when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling MyFunction when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <div class="content-wrapper">
        <div class="box-body">
            <asp:HiddenField ID="hdnPanelStatus" runat="server" Value="C"></asp:HiddenField>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                Patient Order</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content--%>
                            <div class="row" align="center">
                                <br />
                                <asp:Button ID="btnNewOrder" runat="server" Text="New Order" OnClick="btnNewOrder_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-plus" style="margin-left: -2%; vertical-align: sub;
                                    color: #fff; margin-right: 2%;">
                                </label>
                                <asp:Button ID="btnPendingOrders" runat="server" CssClass="btn btn-primary" Text="View Pending Orders"
                                    Height="30px" Width="13%" Style="text-align: left;" />
                                <label class="fa fa-eye" style="margin-left: -2%; vertical-align: sub; color: #fff;
                                    margin-right: 2%;">
                                </label>
                                <asp:Button ID="btnDrugHistory" runat="server" CssClass="btn btn-primary" Text="Drug History"
                                    OnClientClick="openDrugHistory()" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="fa fa-history" style="margin-left: -2%; vertical-align: sub; color: #fff;
                                    margin-right: 2%;">
                                </label>
                                <asp:Button ID="btnAddAllergy" runat="server" CssClass="btn btn-primary" Text="Allergy History"
                                    OnClientClick="openAllergyPage()" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-plus" style="margin-left: -2%; vertical-align: sub;
                                    color: #fff; margin-right: 2%;">
                                </label>
                                <act:ModalPopupExtender ID="btnPendingOrders_ModalPopupExtender" runat="server" TargetControlID="btnPendingOrders"
                                    PopupControlID="tblPendingOrders" BackgroundCssClass="modalBackground" CancelControlID="btnPendingOrdersClose">
                                </act:ModalPopupExtender>
                                <act:ModalPopupExtender ID="btnPrintLabels_ModalPopupExtender" runat="server" TargetControlID="btnPrintLabels"
                                    PopupControlID="tblPrintLabels" BackgroundCssClass="modalBackground" CancelControlID="btnClosePrintLabels">
                                </act:ModalPopupExtender>
                                &nbsp;
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:Panel ID="PatientInfoHeader" runat="server">
                                        <h5 class="forms" align="left">
                                            <asp:ImageButton ID="imgClientInfo" runat="server" ImageUrl="~/Images/arrow-up.gif" />
                                            <asp:Label ID="lblClientInfo" runat="server" Text="Current Patient Information"></asp:Label>
                                        </h5>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row" style="margin-left: 10px;">
                                <asp:Panel ID="PatientInfoBody" runat="server" Style="overflow: hidden;">
                                    <div class="row">
                                        <div class="col-xs-7">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left" >
                                                    <label for="inputEmail3" class="control-label">
                                                        Start Weight (kg)</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Start Height (cm)</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Start BSA</label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtStartWeight" runat="server" Enabled="False" Width="125px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtStartheight" runat="server" Enabled="False" Width="125px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtStartBSA" runat="server" Enabled="False" Width="125px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Current Weight (kg)</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Current Height (cm)</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Current BSA</label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtCurrentWeight" runat="server" Width="125px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtCurrentHeight" runat="server" Width="125px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtCurrentBSA" runat="server" Enabled="False" Width="125px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Start regimen at this facility</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Start Regimen Line</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Start Regimen Date</label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtStartReg" runat="server" Width="125px" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtStartRegLine" runat="server" Width="125px" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtStartRegDate" runat="server" Width="125px" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Last Regimen</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Last Regimen Line</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtLastReg" runat="server" Width="125px" ReadOnly="True" Enabled="False"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtLastRegLine" runat="server" Width="125px" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                </div>
                                            </div>
                                        </div>
                                        <%--Col 2 Start--%>
                                        <div class="col-xs-5">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        On TB Treatment</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        <asp:Label ID="lblIPTStartDate" runat="server" Font-Bold="True" Text="IPT Start Date:"></asp:Label>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <asp:RadioButtonList ID="rbOnTBTreatment" runat="server" Enabled="False" Font-Bold="True"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <div style="width: 100%; margin: 0px auto;">
                                                        <div style="float: left">
                                                            <asp:TextBox ID="txtIPTStartDate" runat="server" Width="110px" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left">
                                                            <img id="appDateimg3" onclick="w_displayDatePicker('<%=txtIPTStartDate.ClientID%>');"
                                                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                border="0" name="appDateimg0" />
                                                        </div>
                                                        <div style="float: left">
                                                            <span class="smallerlabel" id="appDatespan3">(DD-MMM-YYYY)</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        <asp:Label ID="lblIPTEndDate" runat="server" Font-Bold="True" Text="IPT End Date"></asp:Label>
                                                    </label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <div style="width: 100%; margin: 0px auto;">
                                                        <div style="float: left">
                                                            <asp:TextBox ID="txtIPTEndDate" runat="server" Width="110px" CssClass="form-control"></asp:TextBox></div>
                                                        <div style="float: left">
                                                            <img id="appDateimg4" onclick="w_displayDatePicker('<%=txtIPTEndDate.ClientID%>');"
                                                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                border="0" name="appDateimg1" style="vertical-align: bottom; margin-bottom: 2px;" /></div>
                                                        <div style="float: left">
                                                            <span class="smallerlabel" id="appDatespan4">(DD-MMM-YYYY)</span></div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label">
                                                        Days to Appointment</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label">
                                                        Appointment Date</label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Previous Appointment</label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="margin-left: 5px;">
                                                    <asp:TextBox ID="txtDaysToPreviousAppt" runat="server" Width="65px" Enabled="False"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                    <asp:TextBox ID="txtPreviousApptDate" runat="server" Width="108px" Enabled="False"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        Days to Next appointment</label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="margin-left: 5px;">
                                                    <asp:TextBox ID="txtDaysToNextAppt" runat="server" Width="65px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <div style="width: 100%; margin: 0px auto;">
                                                        <div style="float: left">
                                                            <asp:TextBox ID="txtNextApptDate" runat="server" Width="108px" CssClass="form-control"></asp:TextBox></div>
                                                        <div style="float: left">
                                                            <img id="Img2" onclick="w_displayDatePicker('<%=txtNextApptDate.ClientID%>');" height="22"
                                                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                                                name="appDateimg0" /></div>
                                                        <div style="float: left">
                                                            <span class="smallerlabel" id="Span4">(DD-MMM-YYYY)</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group text-left">
                                                    <label for="inputEmail3" class="control-label">
                                                        WHO Stage</label>
                                                </div>
                                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                    <asp:DropDownList ID="ddlWHOStage" runat="server" Width="99%" Enabled="False" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            
                                        </div>
                                    </div>
                                </asp:Panel>
                                <act:CollapsiblePanelExtender ID="CollapsiblePanelExtenderPatientInfo" runat="server"
                                    SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PatientInfoBody"
                                    CollapseControlID="PatientInfoHeader" ExpandControlID="PatientInfoHeader" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="True" ImageControlID="imgClientInfo" Enabled="True"></act:CollapsiblePanelExtender>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-xs-4">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                            <label for="inputEmail3" class="control-label">
                                                Patient Classification</label>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlPtnClassification" ClientIDMode="Static" runat="server" Width="95%"
                                                Enabled="True" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                            <label for="inputEmail3" class="control-label">
                                                Differenciated Care</label>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <asp:HiddenField ID="hidDifferenciatedCare" ClientIDMode="Static" runat="server" Value="0" />
                                            <input id="chkDifferenciatedCare" runat="server" clientidmode="Static" name="switch-size"
                                                type="checkbox" checked data-size="small" data-on-text="Yes" data-off-text="No">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                            <label for="inputEmail3" class="control-label">
                                                Treatment Plan</label>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlTreatmentPlan" runat="server" Width="95%" Enabled="True"
                                                CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                            <label for="inputEmail3" class="control-label">
                                                Treatment Program</label>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlTreatmentProg" runat="server" Width="95%" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                            <label for="inputEmail3" class="control-label required">
                                                <asp:Label ID="lblregimenLine" runat="server" Text="Regimen Line:" Style="visibility: hidden;"></asp:Label>
                                            </label>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlregimenLine" runat="server" Width="95%" CssClass="form-control"
                                                Style="visibility: hidden;">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnregimenLine" runat="server" Value="hidden" />
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-xs-4">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                            <label for="inputEmail3" class="control-label">
                                                <asp:Label ID="lblregimenCode" runat="server" Text="Regimen Code:" CssClass="required"
                                                    Style="visibility: hidden;"></asp:Label>
                                            </label>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlRegimenCode" runat="server" Width="95%" OnSelectedIndexChanged="ddlRegimenCode_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="form-control" Style="visibility: hidden;" EnableViewState="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnregimenCode" runat="server" Value="hidden" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4">
                                    
                                </div>
                                <div class="col-xs-4">
                                    <div class="row">
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row">
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-1 text-nowrap">
                                    <label for="inputEmail3" class="control-label">
                                        <asp:Label ID="lblDispensingStoreLabel" runat="server" Font-Bold="True" Text="Dispensing Store:"
                                            CssClass="required"></asp:Label>
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDispensingStore" runat="server" Width="98%" OnSelectedIndexChanged="ddlDispensingStore_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 text-nowrap">
                                    <label for="inputEmail3" class="control-label">
                                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Prescribed by:" CssClass="required"></asp:Label>
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlPrescribedBy" runat="server" Width="98%" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 text-nowrap">
                                    <label for="inputEmail3" class="control-label">
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Text="Prescription date:"
                                            CssClass="required"></asp:Label>
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtprescriptionDate" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 150px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                    <label for="inputEmail3" class="control-label">
                                        <asp:Label ID="lblDispensedBy" runat="server" Font-Bold="True" Text="Dispensed by:"
                                            CssClass="required"></asp:Label>
                                    </label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlDispensedBy" runat="server" Width="98%" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                    <label for="inputEmail3" class="control-label">
                                        <asp:Label ID="lblDispenseDate" runat="server" Font-Bold="True" Text="Dispense date:"
                                            CssClass="required"></asp:Label>
                                    </label>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDispenseDate" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 150px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                </div>
                            </div>
                            <div class="row" align="center">
                                <div class="GridView whitebg" style="cursor: pointer;">
                                    <div class="grid">
                                        <div class="rounded">
                                            <div class="top-outer">
                                                <div class="top-inner">
                                                    <div class="top">
                                                        <h2 class="center">
                                                            Dispense Drugs</h2>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="row">
                                                        <br />
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:Label ID="Label30" runat="server" Text="Drug Name:" Font-Bold="true" Style="vertical-align: top"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtDrug" runat="server" Width="100%" OnTextChanged="txtDrug_TextChanged"
                                                                AutoPostBack="True" class="form-control"></asp:TextBox>
                                                            <asp:Panel ID="divwidth" runat="server" ScrollBars="Vertical" Height="200px" Style="z-index:99" />
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" align="left">
                                                            <asp:CheckBox ID="chkAvailDrugs" runat="server" Text="Available Only" TextAlign="Right"
                                                                Checked="true" OnCheckedChanged="chkAvailDrugs_CheckedChanged" AutoPostBack="true" />
                                                        </div>
                                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group" align="left">
                                                            <asp:Button ID="btnPriorPrescription" runat="server" Font-Bold="True" Text="Copy Prior Prescription"
                                                                OnClick="btnPriorPrescription_Click" CssClass="btn btn-primary" Height="30px"
                                                                Width="55%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-copy" style="margin-left: -8%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                        </div>
                                                        <act:AutoCompleteExtender ServiceMethod="SearchDrugs" MinimumPrefixLength="2" CompletionInterval="30"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtDrug" ID="AutoCompleteExtender1"
                                                            OnClientShown="resetPosition" runat="server" FirstRowSelected="false" CompletionListElementID="divwidth"
                                                            OnClientItemSelected="ace1_itemSelected">
                                                        </act:AutoCompleteExtender>
                                                        <asp:HiddenField ID="hdCustID" runat="server" />
                                                        <asp:Button ID="btnHidAddDrug" runat="server" ClientIDMode="Static" Style="visibility: hidden;
                                                            display: none;" OnClick="btnHidAddDrug_Click" />
                                                        <hr />
                                                    </div>
                                                    <div class="row" align="center">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                            <div class="mid" style="height: 300px; overflow: auto; width: 100%; margin-right: 5px;
                                                                text-align: center; z-index:-1;">
                                                                <div id="div-gridview" class="GridView whitebg" style="padding-right: 20px;">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvDispenseDrugs" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                                Width="100%" BorderWidth="0px" CellPadding="0" CssClass="table table-bordered table-hover"
                                                                                OnRowDataBound="gvDispenseDrugs_RowDataBound" DataKeyNames="DrugId, DispensingUnitId, orderId, QtyUnitDisp, syrup, UserID, StoreId"
                                                                                GridLines="None" OnRowDeleting="gvDispenseDrugs_RowDeleting">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Drug Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDrugName" runat="server" Text='<%# Bind("DrugName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Batch No" HeaderStyle-Width="150px">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlBatchNo" runat="server" Width="99%" OnSelectedIndexChanged="ddlBatchNo_SelectedIndexChanged"
                                                                                                AutoPostBack="true" class="form-control">
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField HeaderText="Price" HeaderStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSellingPrice" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-Width="80px">
                                                                                        <ItemTemplate>
                                                                                            <asp:HiddenField ID="hBatchQty" runat="server" />
                                                                                            <asp:Label ID="lblExpiryDate" runat="server" Width="100%" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Morning" HeaderImageUrl="~/Images/sunrise.png" HeaderStyle-Width="80px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtMorning" runat="server" Width="100%" Text='<%# Bind("Morning") %>'
                                                                                                onkeyup="chkDecimal('<%=txtMorning.ClientID%>')" class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Midday" HeaderImageUrl="~/Images/lunch.png" HeaderStyle-Width="80px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtMidday" runat="server" Width="100%" Text='<%# Bind("Midday") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Evening" HeaderImageUrl="~/Images/sunset.png" HeaderStyle-Width="80px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEvening" runat="server" Width="100%" Text='<%# Bind("Evening") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Night" HeaderImageUrl="~/Images/night.png" HeaderStyle-Width="80px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtNight" runat="server" Width="100%" Text='<%# Bind("Night") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Duration" HeaderStyle-Width="90px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtDuration" runat="server" Width="100%" Text='<%# Bind("Duration") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty Presc" HeaderStyle-Width="120px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQtyPrescribed" runat="server" Width="90%" Text='<%# Bind("QtyPrescribed") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                                                                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQtyPrescribed"
                                                                                                ValidationGroup='RequiredForSave'></asp:RequiredFieldValidator>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Pill Count" HeaderStyle-Width="120px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtPillCount" runat="server" Width="90%" Text='<%# Bind("PillCount") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Refill" HeaderStyle-Width="55px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRefillQty1" runat="server" Width="90%" Text='<%# Bind("QtyDispensed") %>'
                                                                                                class="form-control" disabled="disabled"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty Disp" HeaderStyle-Width="75px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQtyDispensed" runat="server" Width="90%" Text="" class="form-control"></asp:TextBox>
                                                                                            <asp:RangeValidator ID="RangeValidatorQtyDisp" runat="server" ErrorMessage="Error"
                                                                                                MinimumValue="1" MaximumValue="50000" Enabled="false" ControlToValidate="txtQtyDispensed"
                                                                                                Type="Double" Display="Dynamic" ValidationGroup='RequiredForSave'></asp:RangeValidator>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorQtyDisp" runat="server" ErrorMessage="Required"
                                                                                                ControlToValidate="txtQtyDispensed" Enabled="false" ValidationGroup='RequiredForSave'
                                                                                                Display="Dynamic"></asp:RequiredFieldValidator></ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="PPx">
                                                                                        <ItemTemplate>
                                                                                            <div style="text-align: center">
                                                                                                <asp:CheckBox ID="chkProphylaxis" runat="server" Checked='<%# (Eval("Prophylaxis").ToString() == "1" ? true : false) %>'
                                                                                                    ToolTip="Prophylaxis" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Print Rx" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkPrintPrescrip" runat="server" Checked='<%# (Eval("PrintPrescriptionStatus").ToString() == "1" ? true : false) %>'
                                                                                                ToolTip="Prophylaxis" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Comments" HeaderStyle-Width="200px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtComments" runat="server" Width="98%" Text='<%# Bind("Comments") %>'
                                                                                                class="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/Images/del.gif" CommandName="Delete"
                                                                                                OnClientClick="return confirm('Are you sure you want to remove this drug?');"
                                                                                                AlternateText="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Instructions" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblInstructions" runat="server" Width="90%" Text='<%# Bind("Instructions") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="GenericAbbrevation" HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblregimen" runat="server" Width="90%" Text='<%# Bind("GenericAbbrevation") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlDispensingStore" EventName="SelectedIndexChanged" />
                                                                            <asp:AsyncPostBackTrigger ControlID="txtDrug" EventName="TextChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                        <ProgressTemplate>
                                                                            <%--<uc1:UserControl_Loading ID="UserControl_Loading1" runat="server" />--%>
                                                                            <IQ:UserControl_Loading runat="server" ID="UserControl_Loading" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="bottom-outer">
                                                <div class="bottom-inner">
                                                    <div class="bottom">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" align="center">
                                <br />
                                <asp:Button ID="btnFullyDispensed" runat="server" Text="Mark as Fully Dispensed"
                                    Height="30px" Width="16%" Style="text-align: left;" OnClick="btnFullyDispensed_Click"
                                    CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure you want to mark this order as Fully Dispensed?');" />
                                <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -2%; margin-right: 0%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnPrintLabels" runat="server" class="hidden" Text="Print Labels"
                                    Height="30px" Width="8%" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right: 2%;
                                    vertical-align: sub; color: #fff; visibility: hidden">
                                </label>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
                                    ValidationGroup='RequiredForSave' Height="30px" Width="7%" align="left" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right: 1%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnPrintPres" runat="server" CssClass="btn btn-primary" OnClick="btnPrintPres_Click"
                                    Text="Print Prescription" Height="30px" Width="15%" />
                                <label class="glyphicon glyphicon-print" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnPrintLabel" Text="Print Labels" CssClass="btn btn-primary" runat="server"
                                    OnClick="btnPrintLabel_Click" Height="30px" Width="8%" />
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <asp:Table ID="tblPendingOrders" runat="server" Width="400px" Height="300px" BackColor="White"
            CssClass="table-condensed" Style="background-color: #3c8dbc;">
            <asp:TableRow>
                <asp:TableCell Height="20px" BackColor="#3c8dbc">
                    <asp:Table ID="Table11" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White"
                                Font-Bold="true">
                                Pending Orders
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                                <asp:ImageButton ID="btnPendingOrdersClose" runat="server" ImageUrl="~/Images/closeButton1.png"
                                    Height="20px" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="100%">
                    <div class="grid" id="divBills" style="width: 100%;">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: 400px; overflow: auto">
                                        <div id="div1" class="GridView whitebg">
                                            <asp:GridView ID="gvPendingorders" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                Width="100%" BorderColor="white" PageIndex="1" BorderWidth="1" GridLines="None"
                                                CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" DataKeyNames="ptn_pharmacy_pk,visitID"
                                                OnSelectedIndexChanged="gvPendingorders_SelectedIndexChanged" OnRowDataBound="gvPendingorders_RowDataBound">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Transaction Date" DataField="TransactionDate" />
                                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                                  
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="bottom-outer">
                                <div class="bottom-inner">
                                    <div class="bottom">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblPrintLabels" runat="server" Width="600px" Height="70%" BackColor="White"
            CssClass="table-condensed">
            <asp:TableRow>
                <asp:TableCell Height="20px" BackColor="#3c8dbc">
                    <asp:Table ID="Table4" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White"
                                Font-Bold="true">
                                
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                                <asp:ImageButton ID="btnClosePrintLabels" runat="server" ImageUrl="~/Images/closeButton1.png"
                                    Height="20px" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="100%">
                    <iframe style="width: 100%; height: 100%;" id="Iframe3" src="frmPharmacy_PrintLabels.aspx"
                        runat="server"></iframe>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <script type="text/javascript">
        $('#aspnetForm').keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
            }
        });
    </script>
</asp:Content>
