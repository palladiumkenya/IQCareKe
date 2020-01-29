<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPharmacyPrescription.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPharmacyPrescription" %>

<asp:HiddenField ID="drugID" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="drugAbbr" runat="server" ClientIDMode="Static" />
<style>
    .notVisible {
        visibility: hidden;
        display: none;
    }

    .VisibleFrequency {
        visibility: hidden;
        display: none;
    }

    .VisibleMorningEvening {
        visibility: visible;
        display: table-row;
    }
    .modal-lg{
        width: 1000px;
    }
</style>
<div class="col-md-12" style="padding-top: 10px" id="PharmacySection" data-parsley-validate="true" data-show-errors="true">
    <%--<div class="panel panel-info">--%>

    <%--<div class="panel-body">--%>
    <div class="col-md-12">
        <div class="col-md-12">
            <hr />
        </div>
        <div class="col-md-12">
            <div class="col-md-3 form-group">
                <div class="col-md-12">
                    <label class="control-label pull-left">Treatment Program</label></div>
                <div class="col-md-12 pull-right">
                    <asp:DropDownList runat="server" CssClass="form-control input-sm " ID="ddlTreatmentProgram" onChange="treatmentProgram();" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" OnSelectedIndexChanged="ddlTreatmentProgram_SelectedIndexChanged" />
                </div>
            </div>
            <div class="col-md-3 form-group">
                <div class="col-md-12">
                    <label class="control-label pull-left">Period Taken</label></div>
                <div class="col-md-12  pull-right">
                    <asp:DropDownList runat="server" ID="ddlPeriodTaken" CssClass="form-control input-sm" ClientIDMode="Static" />
                </div>
            </div>

            <div class="col-md-3 form-group">
                <div class="col-md-12">
                    <label class="control-label pull-left">Treatment Plan</label></div>
                <div class="col-md-12 pull-right">
                    <asp:DropDownList runat="server" CssClass="form-control input-sm " ID="ddlTreatmentPlan" ClientIDMode="Static" onChange="drugSwitchInterruptionReason();getCurrentRegimen();" data-parsley-min="1" data-parsley-min-message="Value Required" />
                </div>
            </div>
            <div class="col-md-3 form-group">
                <div class="col-md-12">
                    <label class="control-label pull-left">Reason</label></div>
                <div class="col-md-12  pull-right">
                    <asp:DropDownList runat="server" ID="ddlSwitchInterruptionReason" CssClass="form-control input-sm" ClientIDMode="Static" />
                </div>
            </div>


        </div>
        <div class="col-md-12">
            <div class="col-md-3 form-group">
                <div class="col-md-12">
                    <label class="control-label pull-left">Regimen Line </label>
                </div>
                <div class="col-md-12  pull-right">
                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="regimenLine" ClientIDMode="Static" onChange="selectRegimens(this.value);" />
                </div>
            </div>
            <div class="col-md-3 form-group">
                <div class="col-md-12">
                    <label class="control-label pull-left">Regimen </label>
                </div>
                <div class="col-md-12  pull-right">
                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlRegimen" ClientIDMode="Static" onChange="SelectRegimenLine()" />
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <hr />
        </div>
        <div class="col-md-12">
            <div class="col-md-1 pull-left">
                <label class="control-label pull-left">Drug</label></div>
            <div class="col-md-11">
                <input id="txtDrugs" type="text" class="form-control input-sm awesomplete-selectcomplete" clientidmode="Static" runat="server" placeholder="Type to search..." style="width: 100%" />
            </div>
        </div>

        <div class="col-md-12" style="padding-top: 10px">
            <div class="row">

                <div class="col-md-12">
                    <div class="col-md-2 pull-left">
                        <label class="control-label pull-left">Batch</label></div>
                    <div class="col-md-1 pull-left  VisibleFrequency">
                        <label class="control-label pull-left">Dose</label></div>
                    <div class="col-md-2 pull-left  VisibleFrequency">
                        <label class="control-label pull-left">Frequency</label></div>


                    <div class="col-md-1 pull-left VisibleMorningEvening">
                        <label class="control-label pull-left">Morning</label></div>
                    <div class="col-md-1 pull-left VisibleMorningEvening">
                        <label class="control-label pull-left">Midday</label></div>
                    <div class="col-md-1 pull-left VisibleMorningEvening">
                        <label class="control-label pull-left">Evening</label></div>
                    <div class="col-md-1 pull-left  VisibleMorningEvening">
                        <label class="control-label pull-left">Night</label></div>

                    <div class="col-md-1 pull-left">
                        <label class="control-label pull-left">Duration</label></div>
                    <div class="col-md-2 pull-left">
                        <label class="control-label pull-left">Qty Prescribed</label></div>
                    <div class="col-md-1 pull-left">
                        <label class="control-label pull-left">Qty Dispensed</label></div>
                    <div class="col-md-1 pull-left">
                        <label class="control-label pull-left">Prophylaxis</label></div>
                    <div class="col-md-1 pull-left">
                        <label class="control-label pull-left"></label>
                    </div>
                </div>
                <div class="col-md-12 panel-body">
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                    <div class="col-md-1 VisibleFrequency">
                        <input type="text" class="form-control input-sm " runat="server" id="txtDose" clientidmode="Static" onkeyup="CalculateQtyPrescribed();" />
                    </div>
                    <div class="col-md-2 VisibleFrequency">
                        <asp:DropDownList ID="ddlFreq" runat="server" CssClass="form-control input-sm " ClientIDMode="Static" onchange="CalculateQtyPrescribed();"></asp:DropDownList>
                    </div>
                    <div class="col-md-1 VisibleMorningEvening">
                        <input type="text" class="form-control input-sm" runat="server" id="txtMorning" clientidmode="Static" onkeyup="CalculateQtyPrescribed();" />
                    </div>
                    <div class="col-md-1 VisibleMorningEvening">
                        <input type="text" class="form-control input-sm " runat="server" id="txtMidday" clientidmode="Static" onkeyup="CalculateQtyPrescribed();" />
                    </div>
                    <div class="col-md-1 VisibleMorningEvening">
                        <input type="text" class="form-control input-sm " runat="server" id="txtEvening" clientidmode="Static" onkeyup="CalculateQtyPrescribed();" />
                    </div>
                    <div class="col-md-1 VisibleMorningEvening">
                        <input type="text" class="form-control input-sm" runat="server" id="txtNight" clientidmode="Static" onkeyup="CalculateQtyPrescribed();" />
                    </div>

                    <div class="col-md-1">
                        <input type="text" class="form-control input-sm" runat="server" id="txtDuration" clientidmode="Static" onkeyup="CalculateQtyPrescribed();" />
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="form-control input-sm" runat="server" id="txtQuantityPres" clientidmode="Static" />
                    </div>
                    <div class="col-md-1">
                        <input type="text" class="form-control input-sm" runat="server" id="txtQuantityDisp" clientidmode="Static" onblur="ChkQtyDispensed();" />
                    </div>
                    <div class="col-md-1">
                        <input type="checkbox" runat="server" id="chkProphylaxis" clientidmode="Static" />
                    </div>
                    <div class="col-md-1 pull-left">
                        <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddDrugs" onclick="AddCorrectDrugPrescription();">Add</button>
                    </div>
                </div>
                <div class="col-md-12 VisibleFrequency">
                    <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: auto;">
                        <table id="dtlDrugPrescriptionFrequency" class="table table-bordered table-striped" style="width: 100%">
                            <thead>
                                <tr>
                                    <th><span class="text-primary notVisible">ProgID</span></th>
                                    <th><span class="text-primary ">TreatmentProgram</span></th>
                                    <th><span class="text-primary  notVisible">TreatmentPlanValue</span></th>
                                    <th><span class="text-primary ">TreatmentPlan</span></th>
                                    <th><span class="text-primary">Reason</span></th>

                                    <th><span class="text-primary  notVisible">TreatmentPlanReasonId</span></th>
                                    <th><span class="text-primary">RegimenLine</span></th>
                                    <th><span class="text-primary  notVisible">RegimenLineId</span></th>
                                    <th><span class="text-primary">Regimen</span></th>
                                    <th><span class="text-primary  notVisible">RegimenId</span></th>
                                    <th><span class="text-primary notVisible">PeriodTakenId</span></th>
                                    <th><span class="text-primary">PeriodTaken</span></th>
                                    <th><span class="text-primary notVisible">DrugId</span></th>
                                    <th><span class="text-primary notVisible">BatchId</span></th>
                                    <th><span class="text-primary notVisible">FreqId</span></th>
                                    <th><span class="text-primary">DrugAbbr</span></th>
                                    <th><span class="text-primary">Drug</span></th>
                                    <th><span class="text-primary">Batch</span></th>
                                    <th><span class="text-primary">Dose</span></th>
                                    <th><span class="text-primary">Frequency</span></th>
                                    <th><span class="text-primary">Duration</span></th>
                                    <th><span class="text-primary">Qty Prescribed</span></th>
                                    <th><span class="text-primary">Qty Dispensed</span></th>
                                    <th><span class="text-primary">Prophylaxis</span></th>
                                    <th><span class="text-primary"></span></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>

                <div class="col-md-12 VisibleMorningEvening">
                    <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: auto;">
                        <table id="dtlDrugPrescription" class="table table-bordered table-striped" style="width: 100%">
                            <thead>
                                <tr>
                                    <th><span class="text-primary notVisible">ProgID</span></th>
                                    <th><span class="text-primary ">TreatmentProgram</span></th>
                                    <th><span class="text-primary  notVisible">TreatmentPlanValue</span></th>
                                    <th><span class="text-primary ">TreatmentPlan</span></th>
                                    <th><span class="text-primary">Reason</span></th>

                                    <th><span class="text-primary  notVisible">ReasonValue</span></th>
                                    <th><span class="text-primary">RegimenLine</span></th>
                                    <th><span class="text-primary  notVisible">RegimenLineId</span></th>
                                    <th><span class="text-primary">Regimen</span></th>
                                    <th><span class="text-primary  notVisible">RegimenId</span></th>
                                    <th><span class="text-primary notVisible">PeriodTakenId</span></th>
                                    <th><span class="text-primary">PeriodTaken</span></th>
                                    <th><span class="text-primary notVisible">DrugId</span></th>
                                    <th><span class="text-primary notVisible">BatchId</span></th>

                                    <th><span class="text-primary">DrugAbbr</span></th>
                                    <th><span class="text-primary">Drug</span></th>
                                    <th><span class="text-primary">Batch</span></th>
                                    <th><span class="text-primary ">Morning</span></th>
                                    <th><span class="text-primary">Midday</span></th>
                                    <th><span class="text-primary">Evening</span></th>
                                    <th><span class="text-primary">Night</span>
                                    </>
                                                            <th><span class="text-primary">Duration</span></th>
                                    <th><span class="text-primary">Qty Prescribed</span></th>
                                    <th><span class="text-primary">Qty Dispensed</span></th>
                                    <th><span class="text-primary">Prophylaxis</span></th>
                                    <th><span class="text-primary"></span></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <hr />
        <div class="col-md-6 pull-left">
            <div class="form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left">Date Prescribed :</label>
                </div>
                <div class="col-md-6">
                    <div class="datepicker fuelux form-group pull-left" id="PrescriptionDate">
                        <div class="input-group pull-left">
                            <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm pull-left" ID="txtPrescriptionDate"
                                onBlur="checkEnrolmentPrescriptionDates();ValidatePrescriptionDate();DateFormat(this,this.value,event,false,'3');" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                            <div class="input-group-btn">
                                <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                    <span class="sr-only">Toggle Calendar</span>
                                </button>
                                <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                    <div class="datepicker-calendar">
                                        <div class="datepicker-calendar-header">
                                            <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                            <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                            <button type="button" class="title" data-month="11" data-year="2014">
                                                <span class="month">
                                                    <span data-month="0">January</span>
                                                    <span data-month="1">February</span>
                                                    <span data-month="2">March</span>
                                                    <span data-month="3">April</span>
                                                    <span data-month="4">May</span>
                                                    <span data-month="5">June</span>
                                                    <span data-month="6">July</span>
                                                    <span data-month="7">August</span>
                                                    <span data-month="8">September</span>
                                                    <span data-month="9">October</span>
                                                    <span data-month="10">November</span>
                                                    <span data-month="11" class="current">December</span>
                                                </span><span class="year">2017</span>
                                            </button>
                                        </div>
                                        <table class="datepicker-calendar-days">
                                            <thead>
                                                <tr>
                                                    <th>Su</th>
                                                    <th>Mo</th>
                                                    <th>Tu</th>
                                                    <th>We</th>
                                                    <th>Th</th>
                                                    <th>Fr</th>
                                                    <th>Sa</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                        <div class="datepicker-calendar-footer">
                                            <button type="button" class="datepicker-today">Today</button>
                                        </div>
                                    </div>
                                    <div class="datepicker-wheels" aria-hidden="true">
                                        <div class="datepicker-wheels-month">
                                            <h2 class="header">Month</h2>
                                            <ul>
                                                <li data-month="0">
                                                    <button type="button">Jan</button></li>
                                                <li data-month="1">
                                                    <button type="button">Feb</button></li>
                                                <li data-month="2">
                                                    <button type="button">Mar</button></li>
                                                <li data-month="3">
                                                    <button type="button">Apr</button></li>
                                                <li data-month="4">
                                                    <button type="button">May</button></li>
                                                <li data-month="5">
                                                    <button type="button">Jun</button></li>
                                                <li data-month="6">
                                                    <button type="button">Jul</button></li>
                                                <li data-month="7">
                                                    <button type="button">Aug</button></li>
                                                <li data-month="8">
                                                    <button type="button">Sep</button></li>
                                                <li data-month="9">
                                                    <button type="button">Oct</button></li>
                                                <li data-month="10">
                                                    <button type="button">Nov</button></li>
                                                <li data-month="11">
                                                    <button type="button">Dec</button></li>
                                            </ul>
                                        </div>
                                        <div class="datepicker-wheels-year">
                                            <h2 class="header">Year</h2>
                                            <ul></ul>
                                        </div>
                                        <div class="datepicker-wheels-footer clearfix">
                                            <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                            <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left">Date Dispensed :</label>
                </div>
                <div class="col-md-6">
                    <div class="datepicker fuelux form-group pull-left" id="DispenseDate">
                        <div class="input-group">
                            <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtDateDispensed" onBlur="ValidateDispensedDate();DateFormat(this,this.value,event,false,'3');" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                            <div class="input-group-btn">
                                <button id="btnDateDisp" type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                    <span class="sr-only">Toggle Calendar</span>
                                </button>
                                <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                    <div class="datepicker-calendar">
                                        <div class="datepicker-calendar-header">
                                            <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                            <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                            <button type="button" class="title" data-month="11" data-year="2014">
                                                <span class="month">
                                                    <span data-month="0">January</span>
                                                    <span data-month="1">February</span>
                                                    <span data-month="2">March</span>
                                                    <span data-month="3">April</span>
                                                    <span data-month="4">May</span>
                                                    <span data-month="5">June</span>
                                                    <span data-month="6">July</span>
                                                    <span data-month="7">August</span>
                                                    <span data-month="8">September</span>
                                                    <span data-month="9">October</span>
                                                    <span data-month="10">November</span>
                                                    <span data-month="11" class="current">December</span>
                                                </span><span class="year">2017</span>
                                            </button>
                                        </div>
                                        <table class="datepicker-calendar-days">
                                            <thead>
                                                <tr>
                                                    <th>Su</th>
                                                    <th>Mo</th>
                                                    <th>Tu</th>
                                                    <th>We</th>
                                                    <th>Th</th>
                                                    <th>Fr</th>
                                                    <th>Sa</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                        <div class="datepicker-calendar-footer">
                                            <button type="button" class="datepicker-today">Today</button>
                                        </div>
                                    </div>
                                    <div class="datepicker-wheels" aria-hidden="true">
                                        <div class="datepicker-wheels-month">
                                            <h2 class="header">Month</h2>
                                            <ul>
                                                <li data-month="0">
                                                    <button type="button">Jan</button></li>
                                                <li data-month="1">
                                                    <button type="button">Feb</button></li>
                                                <li data-month="2">
                                                    <button type="button">Mar</button></li>
                                                <li data-month="3">
                                                    <button type="button">Apr</button></li>
                                                <li data-month="4">
                                                    <button type="button">May</button></li>
                                                <li data-month="5">
                                                    <button type="button">Jun</button></li>
                                                <li data-month="6">
                                                    <button type="button">Jul</button></li>
                                                <li data-month="7">
                                                    <button type="button">Aug</button></li>
                                                <li data-month="8">
                                                    <button type="button">Sep</button></li>
                                                <li data-month="9">
                                                    <button type="button">Oct</button></li>
                                                <li data-month="10">
                                                    <button type="button">Nov</button></li>
                                                <li data-month="11">
                                                    <button type="button">Dec</button></li>
                                            </ul>
                                        </div>
                                        <div class="datepicker-wheels-year">
                                            <h2 class="header">Year</h2>
                                            <ul></ul>
                                        </div>
                                        <div class="datepicker-wheels-footer clearfix">
                                            <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                            <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div class="col-md-12">
            <hr />
        </div>
        <div class="col-md-4"></div>
        <div class="col-md-12">
            <%--<div class="col-md-2"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-info btn-sm fa fa-plus-circle" OnClick="saveUpdatePharmacy();"> Save Prescription</asp:LinkButton></div>--%>
            <div class="col-md-3">
                <button type="button" id="btnSavePrescription" name="btnSavePrescription" clientidmode="Static" class="btn btn-info btn-sm fa fa-plus-circle" onclick="saveUpdatePharmacy();">Save Prescription</button></div>
            <div class="col-md-3">
                <button type="button" id="btnopendispensingmodal" class="btn btn-Success btn-sm fa fa-floppy-o" data-toggle="modal" data-target="#dispensingModal">Dispense Drugs</button></div>
            <div class="col-md-3">
                <button type="button" class="btn btn-warning btn-sm fa fa-refresh" onclick="resetPharmacyForm();">Reset Prescription</button></div>
            <div class="col-md-3">
                <button type="button" class="btn btn-danger btn-sm  fa fa-times" id="btnClosePrecriptionModal" data-dismiss="modal">Close Prescription</button>
                <button type="button" class="btn btn-danger btn-sm  fa fa-times" id="btnClosePrecription">Close Prescription</button>
            </div>
        </div>

    </div>
    <%--</div>--%><%-- .panel-body--%>

    <%--</div>--%><%-- .panel--%>
</div>
<%-- .col-md-12--%>

<!--- dispensing modal --->
<div class="modal fade" id="dispensingModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">DISPENSE</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="form-group">
            <div class="col-md-2">
                <label class="control-label pull-left">Dispense Date</label>
            </div>
            <div class="col-md-4">
                <div class='input-group date' id='PersonAppointmentDate'>
					<span class="input-group-addon">
						<span class="glyphicon glyphicon-calendar"></span>
					</span>
					<asp:TextBox runat="server"  CssClass="form-control input-sm" ID="AppointmentDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" required ="True" data-parsley-min-message="Input the appointment date"></asp:TextBox>
				</div>
            </div> 
        </div>
          <br />
        <table id="dtlDrugDispense" class="table table-bordered table-striped dispensingtable" style="width: 100%">
            <thead>
                <tr>
                    <th><span class="text-primary">Id</span></th>
                    <th><span class="text-primary">Drug</span></th>
                    <th><span class="text-primary">Date Prescribed</span></th>
                    <th><span class="text-primary">Qty Prescribed</span></th>
                    <th><span class="text-primary">Total Dispensed</span></th>
                    <th><span class="text-primary">Qty Remaining</span></th>
                    <th><span class="text-primary">Qty Dispensed</span></th>
                    <%--<th><span class="text-primary">Next Pickup Date</span></th>
                    <th><span class="text-primary">Save</span></th>--%>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
          <br />
        <div class="form-group">
            <div class="col-md-2">
                <label class="control-label pull-left">Next Pickup Date</label>
            </div>
            <div class="col-md-4">
                <div class='input-group date' id='NextAppointmentDate'>
					<span class="input-group-addon">
						<span class="glyphicon glyphicon-calendar"></span>
					</span>
					<asp:TextBox runat="server"  CssClass="form-control input-sm" ID="NextPickupDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" required ="True" data-parsley-min-message="Input the appointment date"></asp:TextBox>
				</div>
            </div> 
        </div>
          <br />
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" id="savedispensingbtn">Save changes</button>
      </div>
    </div>
  </div>
</div>

<script type="text/javascript">
    var regimen = "";
    var pmscm = "<%=PMSCM%>";
    var tp = "";
    var pmscmSamePointDispense = "<%=PMSCMSAmePointDispense%>";
    var pmscmFlag = "0";
    var patientweight = "<%=patientweight%>";
    var prescriptionDate = "<%= this.prescriptionDate %>";
    var dispenseDate = "<%= this.dispenseDate %>";
    var enrolmentDate = "<%= this.enrolmentDate %>";
    var startTreatment = "<%=StartTreatment.ToString().ToLower() %>";
    var patType = "<%=patType.ToString().ToLower() %>";
    var gender = "<%=Session["Gender"]%>";
    var age = "<%=Session["Age"]%>";
    var DosageFrequency = "<%= this.DosageFrequency %>";
    var DrugPrescriptionTable;
    var DrugTableArray = new Array();
    //Date processing
    var today = new Date();
    var tomorrow = new Date();
    tomorrow.setDate(today.getDate() + 1);

    if (prescriptionDate === '' || prescriptionDate === '01-Jan-1900')
        prescriptionDate = 0;

    if (dispenseDate === '' || dispenseDate === '01-Jan-1900')
        dispenseDate = 0;

    if (enrolmentDate === '' || enrolmentDate === '01-Jan-1900')
        enrolmentDate = 0;

    $(document).ready(function () {
        
        $( "#savedispensingbtn" ).click(function() {
            //var submitid = $(this).attr('id');
            //get row identifier
            //var rowidentifier = submitid.replace('savebtnid', '');
            //get value of qtydispensed
            //get value of the next visit
            var nextpickupdate = $("#<%=NextPickupDate.ClientID%>").val();
            var dispenseDate = $("#<%=AppointmentDate.ClientID%>").val();
            //var nextpickupdate = $("#" + rowidentifier + "nextpickupdateinputid").val();
            //get patient id
            //get ptn_pk
            var ndd;
            $('.txtqtyeverdispensed').each(function(i, obj) {
                var qtydispensed = $(this).val();
                var dispenseDate = $("#<%=AppointmentDate.ClientID%>").val();
                var inputid = $(this).attr('id');
                
                var rowidentifier = inputid.replace('qtyeverdispensedinputid', '');
                alert('qty:'+qtydispensed+' ident:'+rowidentifier+' Date:'+dispenseDate);
                $.ajax({
                    url: '../WebService/PatientEncounterService.asmx/saveDispensing',
                    type: 'POST',
                     dataType: 'json',
                     data: "{'qtydis':'" + qtydispensed + "','rowid':'" + rowidentifier + "','dispensedate':'"+dispenseDate+"'}",
                    contentType: "application/json; charset=utf-8",

                     success: function (data) {
                         toastr.success(data.d, "Saved Successfully");
                    }
                });
            });
             savenextpickup(nextpickupdate);
        });

        function savenextpickup(nextpickupdate) {
            vistid = "";
            var urlvisitid = "";
            if ((<%=Request.QueryString.ToString().Length%>) > 0) {
                urlvisitid = "<%=Request.QueryString["visitid"]%>";
            }
            var sessionvisitid = <%=Convert.ToInt32(Session["PatientMasterVisitID"])%>;
            if (urlvisitid > 0) {
                visitid = urlvisitid;
            }
            else {
                visitid = sessionvisitid;
            }
            var period = getUrlParameter('visitid');
            $.ajax({
                url: '../WebService/PatientEncounterService.asmx/savenextpickupdate',
                type: 'POST',
                    dataType: 'json',
                    data: "{'nextpickupdate':'" + nextpickupdate + "','visitid':'" + visitid + "'}",
                contentType: "application/json; charset=utf-8",

                    success: function (data) {
                        toastr.success(data.d, "All dispensing Saved Successfully");
                        //location.reload();
                }
            });
        }

        var getUrlParameter = function getUrlParameter(sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                }
            }
        };

        $("#dtlDrugDispense").on('keyup', 'input', function () {
            var qtydis = $(this).val();
            var qtydistoday;
            if (qtydis == "") {
                qtydistoday = 0;
            }
            else {
                qtydistoday = qtydis;
            }
            //var qtypres = 
            //get id of qty dispensed input
            var inputid = $(this).attr('id');
            //get id of the ordered qty id 
            var rowidentifier = inputid.replace('qtydispensedinputid', '');
            //get quantity ordered
            var qtyordered = $('#' + rowidentifier + 'qtyorderedid').val(); 
            var totaldisp = $('#' + rowidentifier + 'qtyeverdispensedid').val(); 
            
            var totaldispensed = parseInt(totaldisp) + parseInt(qtydistoday);
            var drugbalance = qtyordered - totaldispensed;
            //alert("Order" + qtyordered+"totalbefore"+totaldisp+"totaltoday"+totaldispensed+"balnace"+drugbalance);
            //alert("TotalEver" + totaldispensed + "total" + totaldisp + " drugbalance" + drugbalance + "Ordered" + qtyordered);
            $('#' + rowidentifier + 'qtyremaininginputid').val("");
            $('#' + rowidentifier + 'qtyremaininginputid').val(drugbalance);
            $('#' + rowidentifier + 'qtyeverdispensedinputid').val("");
            $('#' + rowidentifier + 'qtyeverdispensedinputid').val(totaldispensed);
            var dateofdispense = $("#<%=AppointmentDate.ClientID%>").val();
             var pickupdateinput = rowidentifier + "nextpickupdateinputid";
            //var nextpickupdate = getTheNextPickupDate(qtydis, dateofdispense, pickupdateinput);
        });

        if (DosageFrequency.toString() == "1") {
            $('.VisibleFrequency').css('visibility', 'visible');

            $('.VisibleMorningEvening').css('visibility', 'hidden');

            $('.VisibleMorningEvening').css('display', 'none');
            $('.VisibleFrequency').css('display', 'inline');


        }
        else {
            $('.VisibleFrequency').css('visibility', 'hidden');
            $('.VisibleFrequency').css('display', 'none');
            $('.VisibleMorningEvening').css('display', 'inline');
            $('.VisibleMorningEvening').css('visibility', 'visible');
        }

        //alert(pmscmSamePointDispense);
        ///////////////////////////////////////////////////////////////////////////////////////
        tp = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();

        if (pmscmSamePointDispense === "PM/SCM With Same point dispense") {
            // tp = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();

            pmscmFlag = "1";
            drugList(1, tp);
            $("#ddlBatch").prop('disabled', false);
            $("#txtQuantityDisp").prop('disabled', false);
            $("#txtDateDispensed").prop('disabled', false);
            $("#btnDateDisp").prop('disabled', false);

        }
        else if (pmscm === "PM/SCM") {

            drugList(1, tp);
            $("#ddlBatch").prop('disabled', true);
            $("#txtQuantityDisp").prop('disabled', true);
            $("#txtDateDispensed").prop('disabled', true);
            $("#btnDateDisp").prop('disabled', true);
        }
        else {

            drugList(0, tp);
            $("#ddlBatch").prop('disabled', true);
            $("#txtQuantityDisp").prop('disabled', false);
            $("#txtDateDispensed").prop('disabled', false);
            $("#btnDateDisp").prop('disabled', false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////









        $("#<%=ddlTreatmentProgram.ClientID%>").on('change',
            function () {

                /* if (DrugPrescriptionTable.data().any()) {
                     toastr.error("Drug Prescription Error", "Remove added drug(s) before changing treatment program.");
                     //evt.preventDefault();
                     return false;
                 }*/

                tp = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
						// check if patient can have PMTC Regimens
						//var treatmentProgram = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();

                    //tp = treatmentProgram;
                    if (gender === "Female" && age >= 9 && tp === "PMTCT") {

                    } else if (tp === "PMTCT" && gender !== "Female" && age < 9) {
                        toastr.error("PMTCT is for female patients only who are older than 9 years", "Error");
                        $("#<%=ddlTreatmentProgram.ClientID%>").val("");
                    }


                    if (pmscmSamePointDispense === "PM/SCM With Same point dispense") {
                       // tp = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();

                    pmscmFlag = "1";
                    drugList(1, tp);
                    $("#ddlBatch").prop('disabled', false);
                    $("#txtQuantityDisp").prop('disabled', false);
                    $("#txtDateDispensed").prop('disabled', false);
                    $("#btnDateDisp").prop('disabled', false);

                }
                else if (pmscm === "PM/SCM") {

                    drugList(1, tp);
                    $("#ddlBatch").prop('disabled', true);
                    $("#txtQuantityDisp").prop('disabled', true);
                    $("#txtDateDispensed").prop('disabled', true);
                    $("#btnDateDisp").prop('disabled', true);
                }
                else {

                    drugList(0, tp);
                    $("#ddlBatch").prop('disabled', true);
                    $("#txtQuantityDisp").prop('disabled', false);
                    $("#txtDateDispensed").prop('disabled', false);
                    $("#btnDateDisp").prop('disabled', false);
                }

            });



        $('#PrescriptionDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            date: prescriptionDate,
            restricted: [{ from: tomorrow, to: Infinity }]
        });

        $('#DispenseDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            date: dispenseDate,
            restricted: [{ from: tomorrow, to: Infinity }]
        });



        $("#<%=ddlTreatmentPlan.ClientID%>").change(function () {
            var treatmentProgram = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
            var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();

            //console.log(treatmentProgram);
            //console.log(treatmentPlan);
            //console.log(startTreatment);

            if (startTreatment === "true" && treatmentProgram === "ART" && treatmentPlan === "Start Treatment") {
                $("#<%=ddlTreatmentPlan.ClientID%>").val("");
                toastr.error("The Patient has already started treatment", "Error");
            }
        });


        if ($('#pharmacyModal').is(':visible')) {
            $("#btnClosePrecriptionModal").show("fast");
            $("#btnClosePrecription").hide("fast");
        } else {
            $("#btnClosePrecriptionModal").hide("fast");
            $("#btnClosePrecription").show("fast");
        }

        $("#btnClosePrecription").click(function () {
            setTimeout(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';
            }, 2000);
        });

        treatmentProgram();

    });

    //$(function () {
    //    var regExp = /[a-z]/i;
    //    $('#txtDose').on('keydown keyup', function (e) {
    //        var value = String.fromCharCode(e.which) || e.key;

    //        // No letters
    //        if (regExp.test(value)) {
    //            e.preventDefault();
    //            return false;
    //        }
    //    });
    //});
    $(function () {
        var regExp = /[a-z]/i;
        $('#txtMorning').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtMidday').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtEvening').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtNight').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtDuration').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtQuantityPres').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtQuantityDisp').on('keydown keyup', function (e) {
            var value = String.fromCharCode(e.which) || e.key;

            // No letters
            if (regExp.test(value)) {
                e.preventDefault();
                return false;
            }
        });
    });


    if (DosageFrequency.toString() == "1") {
        DrugPrescriptionTable = $('#dtlDrugPrescriptionFrequency').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetPharmacyPrescriptionDetails",
                dataSrc: function (json) {
                    console.log('pharmacyprescriptions');
                    console.log(json.d);

                    var data = json.d;
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            DrugTableArray.push({
                                "ProgID": data[i][0],
                                "TreatmentProgram": data[i][1],
                                "TreatmentPlan": data[i][2],
                                "TreatmentPlanText": data[i][3],
                                "TreatmentPlanReason": data[i][4],
                                "TreatmentPlanReasonId": data[i][5],
                                "RegimenLine": data[i][6],
                                "RegimenLineId": data[i][7],
                                "Regimen": data[i][8],
                                "RegimenId": data[i][9],
                                "Period": data[i][10],
                                "PeriodTakenText": data[i][11],
                                "DrugId": data[i][12],
                                "BatchId": data[i][13],
                                "FreqId": data[i][14],

                                "DrugAbbr": data[i][15],
                                "DrugName": data[i][16],
                                "batchName": data[i][17],
                                "Dose": data[i][18],
                                "Freqtext": data[i][19],
                                "freq": data[i][14],
                                // "Morning": DrugPrescriptionTable.row(i).data()[5],
                                // "Midday": DrugPrescriptionTable.row(i).data()[6],
                                // "Evening": DrugPrescriptionTable.row(i).data()[7],
                                // "Night": DrugPrescriptionTable.row(i).data()[8],

                                "Duration": data[i][20],
                                "qtyPres": data[i][21],
                                "qtyDisp": data[i][22],
                                "prophylaxis": data[i][23]
                            });
                        }
                    }
                    return json.d;
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json"

            },
            paging: false,
            searching: false,
            info: false,
            ordering: false,

            columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [2],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [5],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [7],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [9],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [10],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [11],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [12],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [13],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [14],
                    "visible": false,
                    "searchable": false
                }

                //,
                //{
                //    "targets": [3],
                //    "visible": false,
                //    "searchable": false
                //}
            ]
        });
    }
    else {
        DrugPrescriptionTable = $('#dtlDrugPrescription').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetPharmacyPrescriptionDetails",
                //dataSrc: 'd',
                dataSrc: function (json) {
                    console.log('pharmacyprescriptions');
                    var data = json.d;
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            DrugTableArray.push({
                                "ProgID": data[i][0],
                                "TreatmentProgram": data[i][1],
                                "TreatmentPlan": data[i][2],
                                "TreatmentPlanText": data[i][3],
                                "TreatmentPlanReason": data[i][4],
                                "TreatmentPlanReasonId": data[i][5],
                                "RegimenLine": data[i][6],
                                "RegimenLineId": data[i][7],
                                "Regimen": data[i][8],
                                "RegimenId": data[i][9],
                                "Period": data[i][10],
                                "PeriodTakenText": data[i][11],
                                "DrugId": data[i][12],
                                "BatchId": data[i][13],



                                "DrugAbbr": data[i][14],
                                "DrugName": data[i][15],
                                "batchName": data[i][16],

                                "Morning": data[i][17],
                                "Midday": data[i][18],
                                "Evening": data[i][19],
                                "Night": data[i][20],

                                "Duration": data[i][21],
                                "qtyPres": data[i][22],
                                "qtyDisp": data[i][23],
                                "prophylaxis": data[i][24]
                            });
                        }
                    }
                    return json.d;

                },
                contentType: "application/json; charset=utf-8",
                dataType: "json"

            },
            paging: false,
            searching: false,
            info: false,
            ordering: false,
            targets: 10,
            columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [2],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [5],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [7],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [9],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [10],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [12],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [13],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [14],
                    "visible": false,
                    "searchable": false
                },
                /*{
                    "targets": [11],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [11],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [12],
                    "visible": false,
                    "searchable": false
                } */
                //,
                //{
                //    "targets": [3],
                //    "visible": false,
                //    "searchable": false
                //}
            ]
        });

    }
    $("#dtlDrugPrescription").on('click', '.btnDelete',
        function () {
            DrugPrescriptionTable
                .row($(this).parents('tr'))
                .remove()
                .draw();

            console.log($(this).parents('tr').find('td:eq(6)').text().toString());
            console.log(drugNameArr);

            var index = drugNameArr.indexOf($(this).parents('tr').find('td:eq(6)').text());
            if (index > -1) {
                drugNameArr.splice(index, 1);

                  console.log(DrugTableArray);
                DrugTableArray.splice(index, 1);
                arrDrugPrescriptionUI.splice(index, 1);

                console.log('Remove tables');
                console.log(drugNameArr);
                console.log(arrDrugPrescriptionUI);
                console.log(DrugTableArray);

            }

            var index1 = batchNoArr.indexOf($(this).parents('tr').find('td:eq(7)').text());

            console.log($(this).parents('tr').find('td:eq(7)').text().toString());
            console.log(drugNameArr);
            if (index1 > -1) {
                batchNoArr.splice(index1, 1);

            }

            console.log('Delete Drug');
            console.log(DrugTableArray);
        });
    $("#dtlDrugPrescriptionFrequency").on('click', '.btnDelete',
        function () {

            var res = $(this).parents('tr').find('td:eq(6)').text();
            var res2 = $(this).parents('tr').find('td:eq(5)').text();
            var res3 = $(this).parents('tr').find('td:eq(4)').text();
            console.log('the text values');
            console.log(res);
            console.log(res2);
            console.log(res3);
            console.log(drugNameArr);
            console.log(batchNoArr);
            var index = drugNameArr.indexOf($(this).parents('tr').find('td:eq(6)').text());
            console.log(index);

            if (index > -1) {

                console.log(index);
                console.log(DrugTableArray);
                drugNameArr.splice(index, 1);
                console.log(DrugTableArray);
                DrugTableArray.splice(index, 1);
                arrDrugPrescriptionUI.splice(index, 1);
            }

            var index1 = batchNoArr.indexOf($(this).parents('tr').find('td:eq(7)').text());
            console.log(index1);
            if (index1 > -1) {
                batchNoArr.splice(index1, 1);


            }

            DrugPrescriptionTable
                .row($(this).parents('tr'))
                .remove()
                .draw();

            console.log(drugNameArr);
            console.log(batchNoArr);

            
                console.log('Remove tables');
                console.log(drugNameArr);
                console.log(arrDrugPrescriptionUI);
                console.log(DrugTableArray);
        });

    function SelectRegimenLine() {

        regimen = $("#<%=ddlRegimen.ClientID%>").find(":selected").text();



        if (regimen == "AF2E(TDF + 3TC + DTG)") {


            if (patientweight != "") {
                if (parseInt(patientweight) < 35 && parseInt(age) < 15) {
                    toastr.error("This regimen is recommended for paeds who are  15 years old  or weight of 35 kg and above");
                    $("#<%=ddlRegimen.ClientID%>").val("");
                     return false;
                 }
             }
             if (age < 15 && patientweight == "") {
                 toastr.error("Kindly take the weight for the patient vitals since the regimen is recommended for paeds who are 15 years old or weight of 35 kg and above ");
                 $("#<%=ddlRegimen.ClientID%>").val("");
                return false;
            }




        }


    }

    function SelectDrug() {
        var result = this.value.split("~");
        if (pmscmSamePointDispense === "PM/SCM With Same point dispense") { getBatches(result[0]); }
        this.value = result[2];
        $("#<%=drugID.ClientID%>").val(result[0]);
               $("#<%=drugAbbr.ClientID%>").val(result[1]);
    }

    function drugList(pmscm, tps) {

        var drugInput = document.getElementById('<%= txtDrugs.ClientID %>');
               var awesomplete = new Awesomplete(drugInput, {
                   minChars: 2,
                   maxItems: 50
               });

               document.getElementById('<%= txtDrugs.ClientID %>').addEventListener('awesomplete-selectcomplete', SelectDrug);

               $.ajax({
                   url: '../WebService/PatientEncounterService.asmx/GetDrugList',
                   type: 'POST',
                   dataType: 'json',
                   data: "{'PMSCM':'" + pmscm + "','treatmentPlan':'" + $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text() + "'}",
            contentType: "application/json; charset=utf-8",

            success: function (data) {

                var serverData = data.d;
                var drugList = [];

                for (var i = 0; i < serverData.length; i++) {
                    //drugList.push(serverData[i][1]);
                    drugList.push({ label: serverData[i][1], value: serverData[i][0] });
                }
                awesomplete.list = drugList;
            }
        });

    }

    function resetPharmacyForm() {
        $("#ddlTreatmentProgram").val("0");
        $("#ddlPeriodTaken").val("0");
        $("#ddlTreatmentPlan").val("0");
        $("#ddlSwitchInterruptionReason").val("0");
        $("#regimenLine").val("0");
        $("#ddlRegimen").val("0");
        $("#txtPrescriptionDate").val("");
        $("#txtDateDispensed").val("");

        $("#ddlBatch").val("0");
        if (DosageFrequency.toString() == "1") {
            $("#txtDose").val("");
            $("#ddlFreq").val("0");
        }
        else {

            $("#txtMorning").val("0");
            $("#txtMidday").val("0");
            $("#txtEvening").val("0");
            $("#txtNight").val("0");
        }
        $("#txtDuration").val("");
        $("#txtQuantityDisp").val("");
        $("#txtQuantityPres").val("");

        DrugPrescriptionTable
            .clear()
            .draw();
        drugNameArr = [];
        DrugTableArray = [];
    }

    function getBatches(drugPk) {

        $.ajax({
            url: '../WebService/PatientEncounterService.asmx/GetDrugBatches',
            type: 'POST',
            dataType: 'json',
            data: "{'DrugPk':'" + drugPk + "'}",
            contentType: "application/json; charset=utf-8",

            success: function (data) {
                if (pmscmFlag === "1") {
                    var serverData = data.d;
                    var batchList = [];
                    $("#<%=ddlBatch.ClientID%>").find('option').remove().end();
                       $("#<%=ddlBatch.ClientID%>").append('<option value="0">Select</option>');
                       for (var i = 0; i < serverData.length; i++) {
                           $("#<%=ddlBatch.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
                    }
                }
            }
        });
    }

    function treatmentProgram() {
        var valSelected = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
        //console.log(startTreatment);


        $.ajax({
            url: '../WebService/PatientEncounterService.asmx/GetRegimenLineBasedOnTreatmentProgram',
            type: 'POST',
            dataType: 'json',
            data: "{'treatmentprogram':'" + valSelected + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var serverData = data.d;
                $("#<%=regimenLine.ClientID%>").find('option').remove().end();

                $("#<%=ddlRegimen.ClientID%>").find('option').remove().end();

                $("#<%=regimenLine.ClientID%>").append('<option value="0">Select</option>');
                for (var i = 0; i < serverData.length; i++) {
                    $("#<%=regimenLine.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
                }
            }
        });

  

        if (valSelected === "PMTCT") {
            $("#<%=ddlPeriodTaken.ClientID%>").prop('disabled', false);
            <%--$("#<%=ddlPeriodTaken.ClientID%>").val("");--%>
            $("#<%=ddlTreatmentPlan.ClientID%>").prop('disabled', false);
            $("#<%=ddlTreatmentPlan.ClientID%>").val("");
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', false);
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").val("");
            $("#<%=regimenLine.ClientID%>").prop('disabled', false);
           <%-- $("#<%=regimenLine.ClientID%>").val("");--%>
            $("#<%=ddlRegimen.ClientID%>").prop('disabled', false);
            <%--$("#<%=ddlRegimen.ClientID%>").val("");--%>
        } else if (startTreatment === "false" && valSelected === "ART") {
            $("#<%=ddlTreatmentPlan.ClientID%> option").each(function () {
                if ($(this).text() === "Start Treatment") {
                    $("#<%=ddlPeriodTaken.ClientID%>").prop('disabled', true);
                    $("#<%=ddlPeriodTaken.ClientID%>").val("");
                    $("#<%=ddlTreatmentPlan.ClientID%>").val($(this).val());
                    $("#<%=ddlTreatmentPlan.ClientID%>").prop("disabled", true);
                    drugSwitchInterruptionReason();
                    $("#<%=regimenLine.ClientID%>").prop('disabled', false);
                    <%--$("#<%=regimenLine.ClientID%>").val("");--%>
                    $("#<%=ddlRegimen.ClientID%>").prop('disabled', false);
                  <%--  $("#<%=ddlRegimen.ClientID%>").val("");--%>
                }
            });
        } else if (startTreatment === "true" && valSelected === "ART") {
            $("#<%=ddlTreatmentPlan.ClientID%> option").each(function () {
                if ($(this).text() === "Continue current treatment") {
                    $("#<%=ddlPeriodTaken.ClientID%>").prop('disabled', true);
                    $("#<%=ddlPeriodTaken.ClientID%>").val("");
                    $("#<%=ddlTreatmentPlan.ClientID%>").val($(this).val());
                    $("#<%=ddlTreatmentPlan.ClientID%>").prop("disabled", false);
                      $("#<%=regimenLine.ClientID%>").prop('disabled', false);
           
                    $("#<%=ddlRegimen.ClientID%>").prop('disabled',false);
                    drugSwitchInterruptionReason();
                    getCurrentRegimen();
                }
            });
        } else if (valSelected == "Non-ART" || valSelected== "Treatment" || valSelected== "prophylaxis") {
            $("#<%=ddlPeriodTaken.ClientID%>").prop('disabled', true);
            $("#<%=ddlPeriodTaken.ClientID%>").val("");
            $("#<%=ddlTreatmentPlan.ClientID%>").prop('disabled', true);
            $("#<%=ddlTreatmentPlan.ClientID%>").val("");
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', true);
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").val("");
            $("#<%=regimenLine.ClientID%>").prop('disabled', true);
           $("#<%=regimenLine.ClientID%>").val("");
            $("#<%=ddlRegimen.ClientID%>").prop('disabled', true);
            $("#<%=ddlRegimen.ClientID%>").val("");
        } else {
            $("#ddlPeriodTaken").val("0");
            $("#<%=ddlPeriodTaken.ClientID%>").prop('disabled', true);
            $("#<%=ddlPeriodTaken.ClientID%>").val("");
            $("#<%=ddlTreatmentPlan.ClientID%>").prop('disabled', false);
            $("#<%=ddlTreatmentPlan.ClientID%>").val("");
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', false);
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").val("");
            $("#<%=regimenLine.ClientID%>").prop('disabled', false);
           <%-- $("#<%=regimenLine.ClientID%>").val("");--%>
            $("#<%=ddlRegimen.ClientID%>").prop('disabled', false);
            <%--$("#<%=ddlRegimen.ClientID%>").val("");--%>
        }

        /*DrugPrescriptionTable
                    .clear()
                    .draw();*/

        drugNameArr = [];


    }

    function drugSwitchInterruptionReason() {
        var treatmentPlan = $("#ddlTreatmentPlan option:selected").text();

        var valSelected = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
           if (valSelected === "Continue current treatment" || valSelected === "Select" || valSelected === "Start Treatment") {
               $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', true);
           }
           else {
               $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', false);
           }

           treatmentPlan = treatmentPlan.replace(/\s/g, '');

           $.ajax({
               url: '../WebService/PatientEncounterService.asmx/GetDrugSwitchReasons',
               type: 'POST',
               dataType: 'json',
               data: "{'TreatmentPlan':'" + treatmentPlan + "'}",
               contentType: "application/json; charset=utf-8",
               success: function (data) {
                   var serverData = data.d;
                   $("#<%=ddlSwitchInterruptionReason.ClientID%>").find('option').remove().end();
                   $("#<%=ddlSwitchInterruptionReason.ClientID%>").append('<option value="0">Select</option>');
                   for (var i = 0; i < serverData.length; i++) {
                       $("#<%=ddlSwitchInterruptionReason.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
                }
            }
        });
    }

    function DrawDataTable(ctrlName, arrUI) {

        if (arrUI.length > 0) {
            var table = $("#" + ctrlName).DataTable();
            table.rows.add(arrUI).draw().nodes();
        }
    }
    function removeDups(names) {
        let unique = {};
        names.forEach(function (i) {
            if (!unique[i]) {
                unique[i] = true;
            }
        });
        return Object.keys(unique);
    }

    var drugNameArr = new Array();
    var batchNoArr = new Array();
    var arrDrugPrescriptionUI = [];
   
    function AddCorrectDrugPrescription() {
        console.log(DosageFrequency.toString());
        $('#PharmacySection').parsley().destroy();
        $('#PharmacySection').parsley({
            excluded:
                "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
        });

        if ($('#PharmacySection').parsley().validate()) {

            var treatmentProgram = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").val();
            var treatmentProgramName = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
            var periodTaken = $("#<%=ddlPeriodTaken.ClientID%>").find(":selected").val();
            var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").val();
            var treatmentPlanName = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
            var treatmentPlanReason = $("#<%=ddlSwitchInterruptionReason.ClientID%>").find(":selected").val();
            var regimenLine = $("#<%=regimenLine.ClientID%>").find(":selected").val();
            var regimen = $("#<%=ddlRegimen.ClientID%>").find(":selected").val();
            var regimenText = $("#<%=ddlRegimen.ClientID%>").find(":selected").text();
            var datePrescribed = $("#txtPrescriptionDate").val();
            var dateDispensed = $("#txtDateDispensed").val();





            if (DosageFrequency.toString() == "1") {
                // arrDrugPrescriptionUI = [];
                var treatmentProgram = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").val();
                var treatmentProgramName = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
                var periodTaken = $("#<%=ddlPeriodTaken.ClientID%>").find(":selected").val();
                var periodTakenText = $("#<%=ddlPeriodTaken.ClientID%>").find(":selected").text();
                var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").val();
                var treatmentPlanName = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
                var treatmentPlanReason = $("#<%=ddlSwitchInterruptionReason.ClientID%>").find(":selected").val();
                var treatmentPlanReasonName = $("#<%=ddlSwitchInterruptionReason.ClientID%>").find(":selected").text();
                var regimenLine = $("#<%=regimenLine.ClientID%>").find(":selected").val();
                var regimenLineName = $("#<%=regimenLine.ClientID%>").find(":selected").text();
                var regimen = $("#<%=ddlRegimen.ClientID%>").find(":selected").val();
                var regimenText = $("#<%=ddlRegimen.ClientID%>").find(":selected").text();
                var datePrescribed = $("#txtPrescriptionDate").val();
                var dateDispensed = $("#txtDateDispensed").val();
                var drugId = $("#drugID").val();
                var drugAbbr = $("#drugAbbr").val();
                var drugName = $("#txtDrugs").val();
                var batchId = $('#ddlBatch').find(":selected").val();
                var batchText = $('#ddlBatch').find(":selected").text();
                var dose = $("#txtDose").val();
                var freqId = $('#ddlFreq').find(":selected").val();
                var freqTxt = $('#ddlFreq').find(":selected").text();
                //var morning = $("#txtMorning").val();
                //var midday = $("#txtMidday").val();
                //var evening = $("#txtEvening").val();
                //var night = $("#txtNight").val();

                var duration = $("#txtDuration").val();
                var quantityPres = $("#txtQuantityPres").val();
                var quantityDisp = $("#txtQuantityDisp").val();
                batchText = batchText.substring(0, batchText.indexOf('~'));


                if ($('#chkProphylaxis').is(":checked")) {
                    var prophylaxis = 1;
                }
                else {
                    var prophylaxis = 0;
                }

                //Validate duplication
                if (batchId == undefined)
                    batchId = 0;

                var drugFound = 0;
                var batchFound = 0;

                if (drugName === "") {
                    toastr.error("Error", "Please select drug");
                    return false;
                }

                if (dose === "" || dose === "0") {
                    toastr.error("Error", "Please enter the dose");
                    return false;
                }

                if (freqId === "0") {
                    toastr.error("Error", "Please enter the frequency");
                    return false;
                }
                //if ((parseInt(morning) || 0) + (parseInt(midday) || 0) + (parseInt(evening) || 0) + (parseInt(night) || 0) === 0) {
                //    toastr.error("Error", "Please enter the dose");
                //    return false;
                //}

                if (duration == "0" || duration === "") {
                    toastr.error("Error", "Please enter the duration");
                    return false;
                }
              

                if (quantityPres === "0" || quantityPres === "") {
                    toastr.error("Error", "Please enter the quantity prescribed");
                    return false;
                }
                if (treatmentProgramName !== "Treatment" && treatmentProgramName !== "prophylaxis" && treatmentProgramName !== "Non-ART") {
                    if (DrugTableArray.length > 0) {
                        ExistingProgramvalue = new Array();
                        ExistingProgramvalue = [];
                        ExistingRegimenLine = new Array();
                        valuesprogram = new Array();
                        valuesprogram = [];
                        ExistingProgramvalue = DrugTableArray.filter(x => x.TreatmentProgram.toString() != treatmentProgramName.toString()
                            && x.TreatmentProgram.toString() !== "Treatment" && x.TreatmentProgram.toString() !== "prophylaxis" &&  treatmentProgramName !== "Non-ART")

                        if (ExistingProgramvalue.length > 0) {

                            valuesprogram = removeDups(ExistingProgramvalue);

                            {
                                toastr.error("TreatmentProgram", "Kindly one can only add one treatment program and either Non-ART , Treatment or prophylaxis program only." +
                                    "Kindly clear the drugs for " + valuesprogram.toString() + "treatment program in order to add drugs for  the current program");
                                return false;
                            }

                        };

                        if (regimen > 0) {
                            console.log("DrugTableArray", DrugTableArray);
                            ExistingRegimenLine = DrugTableArray.filter(x => x.RegimenId.toString() !== regimen.toString());
                            console.log("ExistingRegimenLine", ExistingRegimenLine);
                            console.log("regimen", regimen);
                            if (ExistingRegimenLine.length > 0) {
                                 toastr.error("Regimen", "Kindly one cannot have different regimen at the same time. Kindly prescibe the correct regimen" 
                                   );
                                return false;
                            }

                            
                        }


                    }
                }

               

                drugFound = $.inArray("" + drugName + "", drugNameArr);
                batchFound = $.inArray("" + batchText + "", batchNoArr);



                if (drugFound > -1 && batchFound > -1) {
                    toastr.error("Error", drugName + " and/or batch no. " + batchText + " already exists in the List");
                    return false; // message box herer
                }
                else {
                    drugNameArr.push("" + drugName + "");
                    batchNoArr.push("" + batchText + "");



                    DrugTableArray.push({
                        "ProgID": treatmentProgram,
                        "TreatmentProgram": treatmentProgramName,
                        "TreatmentPlan": treatmentPlan,
                        "TreatmentPlanText": treatmentPlanName ,
                        "TreatmentPlanReason": treatmentPlanReasonName,
                        "TreatmentPlanReasonId": treatmentPlanReason,
                        "RegimenLine": regimenLineName,
                        "RegimenLineId": regimenLine,
                        "Regimen": regimenText,
                        "RegimenId": (regimen == undefined)?0:regimen,
                        "Period": periodTaken,
                        "PeriodTakenText": periodTakenText == "Select" ? "" : periodTakenText,
                        "DrugId": drugId,
                        "BatchId": batchId,
                        "FreqId": freqId,

                        "DrugAbbr": drugAbbr,
                        "DrugName": drugName,
                        "batchName": batchText,
                        "Dose": dose,
                        "Freqtext": freqTxt,
                        "freq": freqId,
                        "Duration": duration,
                        "qtyPres": quantityPres,
                        "qtyDisp": quantityDisp,
                        "prophylaxis": prophylaxis
                    });
                    arrDrugPrescriptionUI.push([treatmentProgram,
                        treatmentProgramName,
                        treatmentPlan,
                        (treatmentPlanName== "Select")?"":treatmentPlanName,
                        (treatmentPlanReasonName == "Select") ? "" : treatmentPlanReasonName,
                        treatmentPlanReason,

                        (regimenLineName== "Select")? "":regimenLineName,
                        (regimenLine==undefined)?0:regimenLine,

                        regimenText,
                        (regimen== undefined)? 0:regimen,
                        periodTaken,
                        (periodTakenText == "Select") ? "" : periodTakenText,
                        drugId, batchId, freqId, drugAbbr, drugName, batchText, dose, freqTxt, duration, quantityPres, quantityDisp,
                        prophylaxis,
                        "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                    ]);

                    //arrDrugPrescriptionUI.push([
                    //    drugId, batchId, drugAbbr, drugName, batchText, morning, midday, evening, night, duration, quantityPres, quantityDisp,
                    //    prophylaxis,
                    //    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                    //]);

                    DrugPrescriptionTable
                        .clear()
                        .draw();
                    DrawDataTable("dtlDrugPrescriptionFrequency", arrDrugPrescriptionUI);
                    console.log(arrDrugPrescriptionUI);
                    console.log('Add Drug');
                    console.log(DrugTableArray);


                    $("#txtDrugs").val("");
                    $("#ddlBatch").val("");
                    $("#txtDose").val("");
                    $('#ddlFreq').val("0");
                    $("#txtDuration").val("0");
                    $("#txtQuantityPres").val("0");
                    $("#txtQuantityDisp").val("0");
                    $('#chkProphylaxis').attr('checked', false);

                }

            }
            else {
                var treatmentProgram = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").val();
                var treatmentProgramName = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
                var periodTaken = $("#<%=ddlPeriodTaken.ClientID%>").find(":selected").val();
                var periodTakenText = $("#<%=ddlPeriodTaken.ClientID%>").find(":selected").text();
                var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").val();
                var treatmentPlanName = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
                var treatmentPlanReason = $("#<%=ddlSwitchInterruptionReason.ClientID%>").find(":selected").val();
                var treatmentPlanReasonName = $("#<%=ddlSwitchInterruptionReason.ClientID%>").find(":selected").text();
                var regimenLine = $("#<%=regimenLine.ClientID%>").find(":selected").val();
                var regimenLineName = $("#<%=regimenLine.ClientID%>").find(":selected").text();
                var regimen = $("#<%=ddlRegimen.ClientID%>").find(":selected").val();
                var regimenText = $("#<%=ddlRegimen.ClientID%>").find(":selected").text();
                var datePrescribed = $("#txtPrescriptionDate").val();
                var dateDispensed = $("#txtDateDispensed").val();
                var drugId = $("#drugID").val();
                var drugAbbr = $("#drugAbbr").val();
                var drugName = $("#txtDrugs").val();
                var batchId = $('#ddlBatch').find(":selected").val();
                var batchText = $('#ddlBatch').find(":selected").text();
                //var dose = $("#txtDose").val();
                //var freqId = $('#ddlFreq').find(":selected").val();
                //var freqTxt = $('#ddlFreq').find(":selected").text();
                var morning = $("#txtMorning").val();
                var midday = $("#txtMidday").val();
                var evening = $("#txtEvening").val();
                var night = $("#txtNight").val();

                var duration = $("#txtDuration").val();
                var quantityPres = $("#txtQuantityPres").val();
                var quantityDisp = $("#txtQuantityDisp").val();
                batchText = batchText.substring(0, batchText.indexOf('~'));


                if ($('#chkProphylaxis').is(":checked")) {
                    var prophylaxis = 1;
                }
                else {
                    var prophylaxis = 0;
                }

                //Validate duplication
                if (batchId == undefined)
                    batchId = 0;

                var drugFound = 0;
                var batchFound = 0;

                if (drugName === "") {
                    toastr.error("Error", "Please select drug");
                    return false;
                }

                //if (dose === "" || dose === "0") {
                //    toastr.error("Error", "Please enter the dose");
                //    return false;
                //}

                //if (freqId === "0") {
                //    toastr.error("Error", "Please enter the frequency");
                //    return false;
                //}
                if ((parseInt(morning) || 0) + (parseInt(midday) || 0) + (parseInt(evening) || 0) + (parseInt(night) || 0) === 0) {
                    toastr.error("Error", "Please enter the dose");
                    return false;
                }

                if (duration == "0" || duration === "") {
                    toastr.error("Error", "Please enter the duration");
                    return false;
                }

                if (quantityPres === "0" || quantityPres === "") {
                    toastr.error("Error", "Please enter the quantity prescribed");
                    return false;
                }

                drugFound = $.inArray("" + drugName + "", drugNameArr);
                batchFound = $.inArray("" + batchText + "", batchNoArr);



                if (drugFound > -1 && batchFound > -1) {
                    toastr.error("Error", drugName + " and/or batch no. " + batchText + " already exists in the List");
                    return false; // message box herer
                }
                  if (treatmentProgramName !== "Treatment" && treatmentProgramName !== "prophylaxis" && treatmentProgramName !== "Non-ART") {
                    if (DrugTableArray.length > 0) {
                        ExistingProgramvalue = new Array();
                        ExistingProgramvalue = [];
                        ExistingRegimenLine = new Array();
                        valuesprogram = new Array();
                        valuesprogram = [];
                        ExistingProgramvalue = DrugTableArray.filter(x => x.TreatmentProgram.toString() != treatmentProgramName.toString()
                            && x.TreatmentProgram.toString() !== "Treatment" && x.TreatmentProgram.toString() !== "prophylaxis" &&  treatmentProgramName !== "Non-ART")

                        if (ExistingProgramvalue.length > 0) {

                            valuesprogram = removeDups(ExistingProgramvalue);

                            {
                                toastr.error("TreatmentProgram", "Kindly one can only add one treatment program and either Non-ART , Treatment or prophylaxis program only." +
                                    "Kindly clear the drugs for " + valuesprogram.toString() + "treatment program in order to add drugs for  the current program");
                                return false;
                            }

                        };

                        if (regimen > 0) {
                            console.log('DrugTableArray', DrugTableArray);
                            ExistingRegimenLine = DrugTableArray.filter(x => x.RegimenId.toString() !== regimen.toString());
                            console.log('ExistingRegimenLine', ExistingRegimenLine);
                            if (ExistingRegimenLine.length > 0) {
                                 toastr.error("Regimen", "Kindly one cannot have different regimen at the same time. Kindly prescibe the correct regimen" 
                                   );
                                return false;
                            }

                            
                        }


                    }
                }
            

                
                    drugNameArr.push("" + drugName + "");
                    batchNoArr.push("" + batchText + "");

                    //arrDrugPrescriptionUI = [];

                    DrugTableArray.push({
                        "ProgID": treatmentProgram,
                        "TreatmentProgram": treatmentProgramName,
                        "TreatmentPlan": treatmentPlan,
                        "TreatmentPlanText": treatmentPlanName,
                        "TreatmentPlanReason": treatmentPlanReasonName,
                        "TreatmentPlanReasonId": treatmentPlanReason,
                        "RegimenLine": regimenLineName,
                        "RegimenLineId": regimenLine,
                        "Regimen": regimenText,
                        "RegimenId":(regimen == undefined)?0:regimen,
                        "Period": periodTaken,
                        "PeriodTakenText": periodTakenText,
                        "DrugId": drugId,
                        "BatchId": batchId,


                        "DrugAbbr": drugAbbr,
                        "DrugName": drugName,
                        "batchName": batchText,

                        "Morning": morning,
                        "Midday": midday,
                        "Evening": evening,
                        "Night": night,

                        "Duration": duration,
                        "qtyPres": quantityPres,
                        "qtyDisp": quantityDisp,
                        "prophylaxis": prophylaxis
                    });

                 

                    arrDrugPrescriptionUI.push([treatmentProgram,
                        treatmentProgramName,
                        treatmentPlan,
                        (treatmentPlanName =="Select")?"":treatmentPlanName,
                        (treatmentPlanReasonName == "Select") ? "" : treatmentPlanReasonName,
                        treatmentPlanReason,
                        (regimenLineName=="Select") ? "":regimenLineName,
                        regimenLine,
                        regimenText,
                        (regimen== undefined)? 0:regimen,
                        periodTaken, (periodTakenText == "Select") ? "" : periodTakenText,
                        drugId, batchId, drugAbbr, drugName, batchText, morning, midday, evening, night, duration, quantityPres, quantityDisp,
                        prophylaxis,
                        "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                    ]);

                        console.log(arrDrugPrescriptionUI);
                        DrugPrescriptionTable
            .clear()
            .draw();
                    DrawDataTable("dtlDrugPrescription", arrDrugPrescriptionUI);
                    console.log('Add Drug');
                    console.log(DrugTableArray);
                    $("#txtDrugs").val("");
                    $("#ddlBatch").val("");
                    $("#txtMorning").val("");
                    $("#txtMorning").val("");
                    $("#txtMidday").val("");
                    $("#txtEvening").val("");
                     $("#txtNight").val("");
                   
               
                    $('#ddlMidday').val("");
                    $('#ddlEvening').val("");
                    $('#ddlNight').val("");
                    $("#txtDuration").val("0");
                    $("#txtQuantityPres").val("0");
                    $("#txtQuantityDisp").val("0");
                $('#chkProphylaxis').attr('checked', false);

                }
            }
 
        else {
                toastr.error("Enter missing field");
            }
    }
    function selectRegimens(regimenLine) {
        var valSelected = $("#<%=regimenLine.ClientID%>").find(":selected").text();

           if (valSelected === "Select") {
               $("#<%=ddlRegimen.ClientID%>").prop('disabled', true);
           }
           else {
               $("#<%=ddlRegimen.ClientID%>").prop('disabled', false);
           }

           valSelected = valSelected.replace(/\s/g, '');

           $.ajax({
               url: '../WebService/PatientEncounterService.asmx/GetRegimensBasedOnRegimenLine',
               type: 'POST',
               dataType: 'json',
               data: "{'RegimenLine':'" + valSelected + "'}",
               contentType: "application/json; charset=utf-8",
               success: function (data) {
                   var serverData = data.d;
                   $("#<%=ddlRegimen.ClientID%>").find('option').remove().end();
                   $("#<%=ddlRegimen.ClientID%>").append('<option value="0">Select</option>');
                   for (var i = 0; i < serverData.length; i++) {
                       $("#<%=ddlRegimen.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
                }
            }
        });
    }


         function saveUpdatePharmacy() {
        $('#PharmacySection').parsley().destroy();
        $('#PharmacySection').parsley({
            excluded:
            "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
        });

     //   if ($('#PharmacySection').parsley().validate()) {

            //console.log(datePrescribed);
            //console.log(dateDispensed);
            //return false;

            /*(if (!DrugPrescriptionTable.data().any()) {
                toastr.error("Drug Prescription Error", "Add drugs to prescribe.");
                //evt.preventDefault();
                return false;
            } */


              var datePrescribed = $("#txtPrescriptionDate").val();
              var dateDispensed = $("#txtDateDispensed").val();
            if (datePrescribed == "" || datePrescribed === undefined) {
                toastr.error("Drug Prescription Error", "Kindly fill the date prescribed");
            }

            var allAbbr = "";
            ///////////////////////////////////////////////////////////////////
         
            var drugPrescriptionArray = new Array();
            try {



                if (DosageFrequency.toString() == "1") {
                    // var rowCount = $('#dtlDrugPrescriptionFrequency tbody tr').length;
                    var rowCount = DrugTableArray.length;
                    console.log(DrugTableArray);
                    for (var i = 0; i < rowCount; i++) {
                        drugPrescriptionArray[i] = {
                            "DrugId": DrugTableArray[i].DrugId,
                            "BatchId": DrugTableArray[i].BatchId,
                            "FreqId": DrugTableArray[i].FreqId,
                            "DrugAbbr": DrugTableArray[i].DrugAbbr,
                            "Dose": DrugTableArray[i].Dose,
                            // "Morning": DrugPrescriptionTable.row(i).data()[5],
                            // "Midday": DrugPrescriptionTable.row(i).data()[6],
                            // "Evening": DrugPrescriptionTable.row(i).data()[7],
                            // "Night": DrugPrescriptionTable.row(i).data()[8],
                            //"DrugAbbr": DrugPrescriptionTable.row(i).data()[3],

                            "Duration": DrugTableArray[i].Duration,
                            "qtyPres": DrugTableArray[i].qtyPres,
                            "qtyDisp": DrugTableArray[i].qtyDisp,
                            "prophylaxis": DrugTableArray[i].prophylaxis
                        }
                    }
                    console.log(drugPrescriptionArray);
                }
                else {
                    //var rowCount = $('#dtlDrugPrescription tbody tr').length;
                    var rowCount = DrugTableArray.length;
                    for (var i = 0; i < rowCount; i++) {
                        drugPrescriptionArray[i] = {
                            "DrugId": DrugTableArray[i].DrugId,
                            "BatchId": DrugTableArray[i].BatchId,
                            //"FreqId": DrugPrescriptionTable.row(i).data()[2],
                            "DrugAbbr": DrugTableArray[i].DrugAbbr,
                            //"Dose": DrugPrescriptionTable.row(i).data()[6],
                            "Morning": DrugTableArray[i].Morning,
                            "Midday": DrugTableArray[i].Midday,
                            "Evening": DrugTableArray[i].Evening,
                            "Night": DrugTableArray[i].Night,

                            "Duration": DrugTableArray[i].Duration,
                            "qtyPres": DrugTableArray[i].qtyPres,
                            "qtyDisp": DrugTableArray[i].qtyDisp,
                            "prophylaxis": DrugTableArray[i].prophylaxis
                        }
                    }
                }

                if (DrugTableArray.length > 0) {
                    DrugTableArray.forEach(x => {
                        if (!allAbbr.toUpperCase().includes(x.DrugAbbr)) {
                            if (x.DrugAbbr != "")
                                allAbbr += x.DrugAbbr + "/";
                        }
                    });
                }
            }
            catch (ex) { }
          
            //////////////////////////////////////////////////////////////////
            allAbbr = allAbbr.replace(/\/$/, "");

            console.log(allAbbr);

            var sumAllAbbr = 0;
            var sumSelectedRegimen = 0;
            try {
                for (var i = 0; i < allAbbr.length; i++) {
                    sumAllAbbr += allAbbr.charCodeAt(i);
                }
            }
            catch (err) { }

            try {
                //var regExp = /\(([^)]+)\)/;
                var regimentext;
                var DrugArray = DrugTableArray.filter(x => x.TreatmentProgram.toString() !== "Treatment" && x.TreatmentProgram.toString() !== "prophylaxis" && x.TreatmentProgram.toString()!== "Non-ART").map(x => { return x.Regimen });
                var regimenText = DrugArray[0].toString();
                 regimenText = regimenText.match(/\(([^)]+)\)/)[1];
                //var matches = regExp.exec(regimenText);
                var selectedRegimen = regimenText.replace(/\+/g, '/').replace(/ /g, '');
                //alert(rmv);
                //alert(matches);
                //var selectedRegimen = matches[1].replace(/ /g, '').replace(/\+/g, '/');
                //alert(selectedRegimen);
                for (var i = 0; i < selectedRegimen.length; i++) {
                    sumSelectedRegimen += selectedRegimen.charCodeAt(i);
                }
               

            }
            catch (err) {
                toastr.error("Error", "");
            }

            if (sumAllAbbr > 0) {
              /*  if (treatmentProgramName === 'ART' || treatmentProgramName === 'PMTCT') {
                    if (regimenLine === "0") {
                        toastr.error("Error", "Please select the Regimen Line");
                        return;
                    }
                }  */
                  var DrugTreatment = DrugTableArray.filter(x => x.TreatmentProgram.toString() !== "Treatment" && x.TreatmentProgram.toString() !== "prophylaxis" && x.TreatmentProgram.toString()!== "Non-ART").map(x => { return x.TreatmentProgram });

                if (sumAllAbbr !== sumSelectedRegimen && (DrugTreatment[0] === 'ART' || DrugTreatment[0]  === 'PMTCT') && sumSelectedRegimen < 1500) {
                    toastr.error("Error", "Selected Regimen is not equal to Prescribed Regimen!");
                    return;
                }
                else {
                $("#btnSavePrescription").attr("disabled", true);
                $.ajax({
                        url: '../WebService/PatientEncounterService.asmx/savePatientPharmacy',
                        type: 'POST',
                        dataType: 'json',
                        data: "{'prescription':'" +
                            JSON.stringify(DrugTableArray)  + "','pmscm':'" + pmscmFlag + "','PrescriptionDate':'" +
                            datePrescribed + "','DispensedDate':'" + dateDispensed + "'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            $("#btnSavePrescription").attr("disabled", true);
                            toastr.success(data.d, "Saved successfully");
                            //$('#pharmacyModal').modal('hide');

                        },
                        error: function (data) {
                            $("#btnSavePrescription").removeAttr("disabled");
                            toastr.error(data.d, "Error");
                        }
                    });
                  /*  $.ajax({
                        url: '../WebService/PatientEncounterService.asmx/savePatientPharmacy',
                        type: 'POST',
                        dataType: 'json',
                        data: "{'TreatmentProgram':'" + treatmentProgram + "','PeriodTaken':'" + periodTaken + "','TreatmentPlan':'" +
                            treatmentPlan + "','TreatmentPlanReason':'" + treatmentPlanReason + "','RegimenLine':'" +
                            regimenLine + "','Regimen':'" + regimen + "','pmscm':'" + pmscmFlag + "','PrescriptionDate':'" +
                            datePrescribed + "','DispensedDate':'" + dateDispensed + "', 'drugPrescription':'" +
                            JSON.stringify(drugPrescriptionArray) + "', 'regimenText':'" + regimenText + "'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            $("#btnSavePrescription").attr("disabled", true);
                            toastr.success(data.d, "Saved successfully");
                            //$('#pharmacyModal').modal('hide');

                        },
                        error: function (data) {
                            $("#btnSavePrescription").removeAttr("disabled");
                            toastr.error(data.d, "Error");
                        }
                    }); */
                } 
            }
         /*   else {
                $.ajax({
                    url: '../WebService/PatientEncounterService.asmx/savePatientPharmacy',
                    type: 'POST',
                    dataType: 'json',
                    data: "{'TreatmentProgram':'" + treatmentProgram + "','PeriodTaken':'" + periodTaken + "','TreatmentPlan':'" +
                            treatmentPlan + "','TreatmentPlanReason':'" + treatmentPlanReason + "','RegimenLine':'" +
                            regimenLine + "','Regimen':'" + regimen + "','pmscm':'" + pmscmFlag + "','PrescriptionDate':'" +
                            datePrescribed + "','DispensedDate':'" + dateDispensed + "', 'drugPrescription':'" +
                            JSON.stringify(drugPrescriptionArray) + "', 'regimenText':'" + regimenText + "'}",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $("#btnSavePrescription").prop("disabled", true);
                        toastr.success(data.d, "Saved successfully");
                    },
                    error: function (data) {
                        $("#btnSavePrescription").prop("disabled", false);
                        toastr.error(data.d, "Error");
                    }
                });
            }
 
        } 

*/
       // else {
         //   toastr.error("Please enter missing fields.");
       // }

       }
    function CalculateQtyPrescribed() {

        var morning, midday, evening, night;
        if (DosageFrequency.toString() == "1") {

            var dose = $("#<%=txtDose.ClientID%>").val();
            var frequencyID = $("#<%=ddlFreq.ClientID%>").find(":selected").val();
            var duration = $("#<%=txtDuration.ClientID%>").val();
            var multiplier = 0;
            if (dose == "")
                dose = "0";
            if (duration == "")
                duration == "0"

            $.ajax({
                url: '../WebService/PatientEncounterService.asmx/getDrugFrequencyMultiplier',
                type: 'POST',
                dataType: 'json',
                data: "{'freqID':'" + frequencyID + "'}",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    multiplier = data.d;

                    result = dose * multiplier * duration;
                    $("#<%=txtQuantityPres.ClientID%>").val(result);

                },
                error: function (data) {
                    toastr.error(data.d, "Failed to get Multiplier");
                }
            });
        }
        else {
            var morning = $("#<%=txtMorning.ClientID%>").val();
            var midday = $("#<%=txtMidday.ClientID%>").val();
            var evening = $("#<%=txtEvening.ClientID%>").val();
            var night = $("#<%=txtNight.ClientID%>").val();

            var duration = $("#<%=txtDuration.ClientID%>").val();

            result = ((parseInt(morning) || 0) + (parseInt(midday) || 0) + (parseInt(evening) || 0) + (parseInt(night) || 0)) * duration;
            $("#<%=txtQuantityPres.ClientID%>").val(result);
        }




    }

    function getCurrentRegimen() {
        var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
        var treatmentPlanId = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").val();

        console.log("here");

        if (treatmentPlan == "Continue current treatment") {
            $.ajax({
                url: '../WebService/PatientEncounterService.asmx/GetCurrentRegimen',
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    try {
                        var serverData = data.d;
                        $("#<%=regimenLine.ClientID%>").val(serverData[0][0]);
                        selectRegimens(serverData[0][0]);
                        //loadDataContinueCurrentTreatment();

                        function waitForRegimens(callback) {
                            window.setTimeout(function () {  //acting like this is an Ajax call
                                $("#<%=ddlRegimen.ClientID%>").val(serverData[0][1]);
                            }, 1000);
                        }

                        waitForRegimens();
                    }
                    catch (err) { }


                },
                error: function (data) {
                    //toastr.error(data.d, "Failed to get Multiplier");
                }
            });
        }

    }

    function loadDataContinueCurrentTreatment() {
        if (pmscmSamePointDispense != "PM/SCM With Same point dispense") {
            if (DosageFrequency.toString == "1") {
                DrugPrescriptionTable.destroy();
                DrugPrescriptionTable = $('#dtlDrugPrescriptionFrequency').DataTable({
                    ajax: {
                        type: "POST",
                        url: "../WebService/PatientEncounterService.asmx/GetLatestPharmacyPrescriptionDetails",
                        //data: "{'PMSCM':'" + pmscm + "'}",
                        dataSrc: 'd',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    },
                    paging: false,
                    searching: false,
                    info: false,
                    ordering: false,
                    columnDefs: [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [1],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [2],
                            "visible": false,
                            "searchable": false
                        }
                        //    ,
                        //{
                        //    "targets": [3],
                        //    "visible": false,
                        //    "searchable": false
                        //}
                    ]
                });
            }
            else {
                DrugPrescriptionTable.destroy();

                DrugPrescriptionTable = $('#dtlDrugPrescription').DataTable({
                    ajax: {
                        type: "POST",
                        url: "../WebService/PatientEncounterService.asmx/GetLatestPharmacyPrescriptionDetails",
                        //data: "{'PMSCM':'" + pmscm + "'}",
                        dataSrc: 'd',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    },
                    paging: false,
                    searching: false,
                    info: false,
                    ordering: false,
                    columnDefs: [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [1],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [2],
                            "visible": false,
                            "searchable": false
                        }
                        //    ,
                        //{
                        //    "targets": [3],
                        //    "visible": false,
                        //    "searchable": false
                        //}
                    ]
                });
            }
        }

    }

    function checkEnrolmentPrescriptionDates() {
        var prescriptionDt = $("#txtPrescriptionDate").val();
        if (enrolmentDate != "" && prescriptionDt != "") {
            var presDt = Date.parse(prescriptionDt);
            var enrolDt = Date.parse(enrolmentDate);
            if (presDt < enrolDt) {
                toastr.error("Prescription date cannot be less than Enrollment date.");
                $("#txtPrescriptionDate").val("");
            }
        }
    }

     var DrugDispenseTable = $('#dtlDrugDispense').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetPharmacyDispensingDetails",
                dataSrc: function (json) {
                    console.log('pharmacyprescriptions');
                    console.log(json.d);
                    var data = json.d;
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            DrugTableArray.push({
                                "ProgID": data[i][0],
                                "DrugName": data[i][16],
                                //"DatePrescribed": data[i][18],
                                "Dose": data[i][3]
                                //"Qty Dispensed": data[i][14],
                                //"Qty Remaining": data[i][20],
                                //"Next Pickup Date": data[i][21],
                                //"Save": data[i][22]
                            });
                        }
                    }
                    return json.d;
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json"

            },
            paging: false,
            searching: false,
            info: false,
            ordering: false,

            columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
    });

    $("#btnopendispensingmodal").click(function () {
        var prescdate = $('#txtPrescriptionDate').val();
        $('.presdate').html(prescdate);
    });

    $("input").on("keyup", function () {
        //alert('keyup');
    });

    var dateNow = new Date();
    $("#PersonAppointmentDate").datetimepicker({
        defaultDate: dateNow,
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: true
    });

    var dateNow = new Date();
    
    $("#NextAppointmentDate").datetimepicker({
        defaultDate: dateNow,
        format: 'DD-MMM-YYYY',
        allowInputToggle: true
    });

    function getTheNextPickupDate(qtydis, dateofdispense, pickupdateinput) {
        var ndd;
         $.ajax({
            url: '../WebService/PatientEncounterService.asmx/GetNextPickupDate',
            type: 'POST',
             dataType: 'json',
             data: "{'qtydis':'" + qtydis + "','dateofdispense':'" + dateofdispense + "'}",
            contentType: "application/json; charset=utf-8",

             success: function (data) {
                 //alert(data.d);
                 var date2 = new Date();
                 //alert(date2);
                //$( "#NextAppointmentDate" ).datepicker( "option", "Format", "DD-MMM-YYYY" );
                 //$('#NextAppointmentDate').datepicker('setDate', new Date(data.d), 'format', 'DD-MMM-YYYY');
                 //$('#NextAppointmentDate').datepicker().datepicker('setDate', data.d);
                 //$("#NextPickupDate").val(data.d);
                 var dbDate = data.d;
                var date2 = new Date(dbDate);
                // $('NextAppointmentDate').datepicker('setDate', null);
                 $('#NextAppointmentDate').datepicker({
                        format: 'DD-MMM-YYYY'
                }).datepicker(
                        // Initialize the date to be 00:00 local timezone on October 19, 2016
                        'setDate', new Date('19-10-2016 00:00')
                    );
                
                 //$('NextAppointmentDate').datepicker('setDate', date2);
                 //$('NextAppointmentDate').val(data.d);
            }
        });
        return ndd;
    }

    function saveDispensing(submitid) {
        //var submitid = $(this).attr('id');
        alert(submitid);
    }

   
</script>
