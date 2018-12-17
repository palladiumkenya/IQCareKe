<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPharmacyPrescription.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPharmacyPrescription" %>

<asp:HiddenField ID="drugID" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="drugAbbr" runat="server" ClientIDMode="Static" />
<style>

    .VisibleFrequency{
         visibility: hidden;
         display: none;
    }
    .VisibleMorningEvening{
        visibility: visible;
        display:table-row;
    }
</style>
<div class="col-md-12" style="padding-top: 10px" id="PharmacySection" data-parsley-validate="true" data-show-errors="true">
    <%--<div class="panel panel-info">--%>

        <%--<div class="panel-body">--%>
                <div class="col-md-12">
                    <div class="col-md-12"><hr /></div>
                            <div class="col-md-12">
                                    <div class="col-md-3 form-group">  
                                        <div class="col-md-12"><label class="control-label pull-left">Treatment Program</label></div>
                                        <div class="col-md-12 pull-right">
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm " id="ddlTreatmentProgram" onChange="treatmentProgram();" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" OnSelectedIndexChanged="ddlTreatmentProgram_SelectedIndexChanged" />
                                        </div>                    
                                    </div>   
                                    <div class="col-md-3 form-group">
                                        <div class="col-md-12"><label class="control-label pull-left">Period Taken</label></div>
                                        <div class="col-md-12  pull-right">
                                            <asp:DropDownList runat="server" id="ddlPeriodTaken" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                        </div>                  
                                    </div>

                                    <div class="col-md-3 form-group">  
                                        <div class="col-md-12"><label class="control-label pull-left">Treatment Plan</label></div>
                                        <div class="col-md-12 pull-right">
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm " id="ddlTreatmentPlan" ClientIDMode="Static" onChange="drugSwitchInterruptionReason();getCurrentRegimen();" data-parsley-min="1" data-parsley-min-message="Value Required" />
                                        </div>                    
                                    </div>   
                                    <div class="col-md-3 form-group">
                                        <div class="col-md-12"><label class="control-label pull-left">Reason</label></div>
                                        <div class="col-md-12  pull-right">
                                            <asp:DropDownList runat="server" id="ddlSwitchInterruptionReason" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                        </div>                  
                                    </div>

       
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3 form-group">                  
                                    <div class="col-md-12"><label class="control-label pull-left">Regimen Line </label></div>     
                                    <div class="col-md-12  pull-right">
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="regimenLine" ClientIDMode="Static" onChange="selectRegimens(this.value);" />
                                    </div>                        
                                </div> 
                                <div class="col-md-3 form-group">                  
                                    <div class="col-md-12"><label class="control-label pull-left">Regimen </label></div>     
                                    <div class="col-md-12  pull-right">
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlRegimen" ClientIDMode="Static" />
                                    </div>                        
                                </div> 
                            </div>

                            <div class="col-md-12"><hr /></div>
                                <div class="col-md-12">
                                    <div class="col-md-1 pull-left"><label class="control-label pull-left">Drug</label></div>
                                    <div class="col-md-11">
                                        <input id="txtDrugs" type="text" class="form-control input-sm" ClientIDMode="Static" runat="server" placeholder="Type to search..." style="width:100%" />
                                    </div>                        
                                </div>
                                
                                <div class="col-md-12" style="padding-top:10px">
                                    <div class="row">
                                        
                                        <div class="col-md-12">
                                            <div class="col-md-2 pull-left"><label class="control-label pull-left">Batch</label></div>
                                            <div class="col-md-1 pull-left  VisibleFrequency"><label class="control-label pull-left">Dose</label></div>
                                            <div class="col-md-2 pull-left  VisibleFrequency"><label class="control-label pull-left">Frequency</label></div>


                                            <div class="col-md-1 pull-left VisibleMorningEvening"><label class="control-label pull-left">Morning</label></div>
                                            <div class="col-md-1 pull-left VisibleMorningEvening"><label class="control-label pull-left">Midday</label></div>
                                            <div class="col-md-1 pull-left VisibleMorningEvening"><label class="control-label pull-left">Evening</label></div>
                                            <div class="col-md-1 pull-left  VisibleMorningEvening"><label class="control-label pull-left">Night</label></div>

                                            <div class="col-md-1 pull-left"><label class="control-label pull-left">Duration</label></div>
                                            <div class="col-md-2 pull-left"><label class="control-label pull-left">Qty Prescribed</label></div>
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left">Qty Dispensed</label></div>
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left">Prophylaxis</label></div>
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left"></label></div>
                                        </div>  
                                        <div class="col-md-12 panel-body">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                          <div class="col-md-1 VisibleFrequency"><input type="text" class="form-control input-sm " runat="server" id="txtDose" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-2 VisibleFrequency">
                                                <asp:DropDownList ID="ddlFreq" runat="server" CssClass="form-control input-sm " ClientIDMode="Static" onchange="CalculateQtyPrescribed();"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 VisibleMorningEvening"><input type="text" class="form-control input-sm" runat="server" id="txtMorning" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-1 VisibleMorningEvening"><input type="text" class="form-control input-sm " runat="server" id="txtMidday" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-1 VisibleMorningEvening"><input type="text" class="form-control input-sm " runat="server" id="txtEvening" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-1 VisibleMorningEvening"><input type="text" class="form-control input-sm" runat="server" id="txtNight" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>

                                            <div class="col-md-1"><input type="text" class="form-control input-sm" runat="server" id="txtDuration" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="txtQuantityPres" ClientIDMode="Static" /> </div>
                                            <div class="col-md-1"><input type="text" class="form-control input-sm" runat="server" id="txtQuantityDisp" ClientIDMode="Static" onblur="ChkQtyDispensed();" /> </div>
                                            <div class="col-md-1"><input type="checkbox" runat="server" id="chkProphylaxis" ClientIDMode="Static" /> </div>
                                            <div class="col-md-1 pull-left">
                                                <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddDrugs" onclick="AddCorrectDrugPrescription();">Add</button>
                                            </div>
                                        </div>
                                        <div class="col-md-12 VisibleFrequency">
                                            <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: auto;">
                                                <table id="dtlDrugPrescriptionFrequency" class="table table-bordered table-striped" style="width:100%">
                                                    <thead>
                                                        <tr>
                                                            <th><span class="text-primary">DrugId</span></th>
                                                            <th><span class="text-primary">BatchId</span></th>
                                                            <th> <span class="text-primary">FreqId</span></th>
                                                            <th><span class="text-primary">DrugAbbr</span></th>
                                                            <th><span class="text-primary">Drug</span></th>
                                                            <th><span class="text-primary">Batch</span></th>
                                                            <th ><span class="text-primary">Dose</span></th>
                                                            <th><span class="text-primary" >Frequency</span></th>
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
                                                <table id="dtlDrugPrescription" class="table table-bordered table-striped" style="width:100%">
                                                    <thead>
                                                        <tr>
                                                            <th><span class="text-primary">DrugId</span></th>
                                                            <th><span class="text-primary">BatchId</span></th>
                                                            <th><span class="text-primary">DrugAbbr</span></th>
                                                            <th><span class="text-primary">Drug</span></th>
                                                            <th><span class="text-primary">Batch</span></th>
                                                            <th><span class="text-primary ">Morning</span></th>
                                                            <th ><span class="text-primary">Midday</span></th>
                                                            <th ><span class="text-primary">Evening</span></th>
                                                            <th ><span class="text-primary">Night</span></>
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
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm pull-left" ID="txtPrescriptionDate" onBlur="checkEnrolmentPrescriptionDates();ValidatePrescriptionDate();DateFormat(this,this.value,event,false,'3');" data-parsley-required="true" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                    <div class="col-md-12"><hr/></div>
                    <div class="col-md-4"></div>
                                            
                    <div class="col-md-8">
                    <%--<div class="col-md-2"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-info btn-sm fa fa-plus-circle" OnClick="saveUpdatePharmacy();"> Save Prescription</asp:LinkButton></div>--%>
                        <div class="col-md-3"><button type="button" id="btnSavePrescription" name="btnSavePrescription" clientidmode="Static" Class="btn btn-info btn-sm fa fa-plus-circle" onclick="saveUpdatePharmacy();">Save Prescription</button></div>
                        <div class="col-md-3"><button type="button" Class="btn btn-warning btn-sm fa fa-refresh" onclick="resetPharmacyForm();">Reset Prescription</button></div>
                        <div class="col-md-3">
                            <button type="button" Class="btn btn-danger btn-sm  fa fa-times" id="btnClosePrecriptionModal" data-dismiss="modal">Close Prescription</button>
                            <button type="button" class="btn btn-danger btn-sm  fa fa-times" id="btnClosePrecription">Close Prescription</button>
                        </div>
                    </div>
                                             
            </div>
        <%--</div>--%><%-- .panel-body--%>

    <%--</div>--%><%-- .panel--%>

</div><%-- .col-md-12--%>

<script type="text/javascript">
    var pmscm = "<%=PMSCM%>";
    var tp = "";
    var pmscmSamePointDispense = "<%=PMSCMSAmePointDispense%>";
    var pmscmFlag = "0";
    var prescriptionDate = "<%= this.prescriptionDate %>";
    var dispenseDate = "<%= this.dispenseDate %>";
    var enrolmentDate = "<%= this.enrolmentDate %>";
    var startTreatment = "<%=StartTreatment.ToString().ToLower() %>";
    var patType = "<%=patType.ToString().ToLower() %>";
    var gender = "<%=Session["Gender"]%>";
    var age = "<%=Session["Age"]%>";
    var DosageFrequency = "<%= this.DosageFrequency %>";
    var DrugPrescriptionTable;
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

                    if (DrugPrescriptionTable.data().any()) {
                        toastr.error("Drug Prescription Error", "Remove added drug(s) before changing treatment program.");
                        //evt.preventDefault();
                        return false;
                    }
					
					tp=$("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text();
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

        $("#btnClosePrecription").click(function() {
            setTimeout(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';}, 2000);
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

                var index = drugNameArr.indexOf($(this).parents('tr').find('td:eq(0)').text());
                if (index > -1) {
                    drugNameArr.splice(index, 1);
                }

                var index1 = batchNoArr.indexOf($(this).parents('tr').find('td:eq(1)').text());
                if (index1 > -1) {
                    batchNoArr.splice(index1, 1);
                }
            });
    $("#dtlDrugPrescriptionFrequency").on('click', '.btnDelete',
        function () {

            var res = $(this).parents('tr').find('td:eq(0)').text();
            var res2 = $(this).parents('tr').find('td:eq(1)').text();
            var res3= $(this).parents('tr').find('td:eq(2)').text();
            console.log(res);
            console.log(res2);
            console.log(res3);
            console.log(drugNameArr);
            console.log(batchNoArr);
                var index = drugNameArr.indexOf($(this).parents('tr').find('td:eq(1)').text()); 
                console.log(index);

                if (index > -1) {

                    drugNameArr.splice(index, 1);
                }

                var index1 = batchNoArr.indexOf($(this).parents('tr').find('td:eq(2)').text());
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
            });

    
    
           function SelectDrug() {
                   var result = this.value.split("~");
                   if (pmscmSamePointDispense ==="PM/SCM With Same point dispense"){ getBatches(result[0]);}
                   this.value = result[2];
                   $("#<%=drugID.ClientID%>").val(result[0]);
                   $("#<%=drugAbbr.ClientID%>").val(result[1]);
           }
    
           function drugList(pmscm,tps) {
               
               var drugInput = document.getElementById('<%= txtDrugs.ClientID %>');
               var awesomplete = new Awesomplete(drugInput, {
                   minChars: 2
               });
               
               document.getElementById('<%= txtDrugs.ClientID %>').addEventListener('awesomplete-selectcomplete', SelectDrug);
               
               $.ajax({
                   url: '../WebService/PatientEncounterService.asmx/GetDrugList',
                   type: 'POST',
                   dataType: 'json',
                   data: "{'PMSCM':'" + pmscm + "','treatmentPlan':'" + $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").text() +"'}",
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
           
    function resetPharmacyForm()
    {
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
    }

       function getBatches(drugPk)
       {
           $.ajax({
               url: '../WebService/PatientEncounterService.asmx/GetDrugBatches',
               type: 'POST',
               dataType: 'json',
               data: "{'DrugPk':'" + drugPk + "'}",
               contentType: "application/json; charset=utf-8",
           
               success: function (data) {
                   if(pmscmFlag === "1")
                   {
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
                    drugSwitchInterruptionReason();
                    getCurrentRegimen();
                }
            });
        }else if (valSelected == "Non-ART" || valSelected == "HBV") {
            $("#<%=ddlPeriodTaken.ClientID%>").prop('disabled', true);
            $("#<%=ddlPeriodTaken.ClientID%>").val("");
            $("#<%=ddlTreatmentPlan.ClientID%>").prop('disabled', true);
            $("#<%=ddlTreatmentPlan.ClientID%>").val("");
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', true);
            $("#<%=ddlSwitchInterruptionReason.ClientID%>").val("");
            $("#<%=regimenLine.ClientID%>").prop('disabled', true);
            <%--$("#<%=regimenLine.ClientID%>").val("");--%>
            $("#<%=ddlRegimen.ClientID%>").prop('disabled', true);
           <%-- $("#<%=ddlRegimen.ClientID%>").val("");--%>
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

        DrugPrescriptionTable
                    .clear()
                    .draw();

        drugNameArr = [];
    }

       function drugSwitchInterruptionReason()
       {
           var treatmentPlan = $("#ddlTreatmentPlan option:selected").text();

           var valSelected = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
           if (valSelected === "Continue current treatment" || valSelected === "Select" || valSelected === "Start Treatment")
           {
                $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', true);
           }
           else{
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

    var drugNameArr = new Array();
var batchNoArr = new Array();
    function AddCorrectDrugPrescription() {
        console.log(DosageFrequency.toString());
        if (DosageFrequency.toString() == "1") {
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

            drugFound = $.inArray("" + drugName + "", drugNameArr);
            batchFound = $.inArray("" + batchText + "", batchNoArr);



            if (drugFound > -1 && batchFound > -1) {
                toastr.error("Error", drugName + " and/or batch no. " + batchText + " already exists in the List");
                return false; // message box herer
            }
            else {
                drugNameArr.push("" + drugName + "");
                batchNoArr.push("" + batchText + "");

                arrDrugPrescriptionUI = [];

                arrDrugPrescriptionUI.push([
                    drugId, batchId, freqId, drugAbbr, drugName, batchText, dose, freqTxt, duration, quantityPres, quantityDisp,
                    prophylaxis,
                    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                ]);

                //arrDrugPrescriptionUI.push([
                //    drugId, batchId, drugAbbr, drugName, batchText, morning, midday, evening, night, duration, quantityPres, quantityDisp,
                //    prophylaxis,
                //    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                //]);

                DrawDataTable("dtlDrugPrescriptionFrequency", arrDrugPrescriptionUI);
                console.log(arrDrugPrescriptionUI);


                
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
            else {
                drugNameArr.push("" + drugName + "");
                batchNoArr.push("" + batchText + "");

                arrDrugPrescriptionUI = [];

                //arrDrugPrescriptionUI.push([
                //    drugId, batchId, freqId, drugAbbr, drugName, batchText, dose, freqTxt, duration, quantityPres, quantityDisp,
                //    prophylaxis,
                //    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                //]);

                arrDrugPrescriptionUI.push([
                    drugId, batchId, drugAbbr, drugName, batchText, morning, midday, evening, night, duration, quantityPres, quantityDisp,
                    prophylaxis,
                    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                ]);

                DrawDataTable("dtlDrugPrescription", arrDrugPrescriptionUI);

                $("#txtDrugs").val("");
                $("#ddlBatch").val("");
                $("#txtMorning").val("0");
                $('#ddlMidday').val("0");
                $('#ddlEvening').val("0");
                $('#ddlNight').val("0");
                $("#txtDuration").val("0");
                $("#txtQuantityPres").val("0");
                $("#txtQuantityDisp").val("0");
                $('#chkProphylaxis').attr('checked', false);
            }
        } 
}
       function selectRegimens(regimenLine)
       {
           var valSelected = $("#<%=regimenLine.ClientID%>").find(":selected").text();
           
           if(valSelected === "Select")
           {
                $("#<%=ddlRegimen.ClientID%>").prop('disabled', true);
           }
           else{
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

            //console.log(datePrescribed);
            //console.log(dateDispensed);
            //return false;

            if (!DrugPrescriptionTable.data().any()) {
                toastr.error("Drug Prescription Error", "Add drugs to prescribe.");
                //evt.preventDefault();
                return false;
            }

            if (regimen === undefined)
                regimen = '0';
            if (treatmentPlanReason == typeof undefined || treatmentPlanReason === treatmentPlanReason) {
                treatmentPlanReason = 0;
            }


            var allAbbr = "";
            ///////////////////////////////////////////////////////////////////
         
            var drugPrescriptionArray = new Array();
            try {
                
              
                   
                if (DosageFrequency.toString() == "1") {
                    var rowCount = $('#dtlDrugPrescriptionFrequency tbody tr').length;
                    console.log(DrugPrescriptionTable);
                    for (var i = 0; i < rowCount; i++) {
                        drugPrescriptionArray[i] = {
                            "DrugId": DrugPrescriptionTable.row(i).data()[0],
                            "BatchId": DrugPrescriptionTable.row(i).data()[1],
                            "FreqId": DrugPrescriptionTable.row(i).data()[2],
                            "DrugAbbr": DrugPrescriptionTable.row(i).data()[3],
                            "Dose": DrugPrescriptionTable.row(i).data()[6],
                            // "Morning": DrugPrescriptionTable.row(i).data()[5],
                            // "Midday": DrugPrescriptionTable.row(i).data()[6],
                            // "Evening": DrugPrescriptionTable.row(i).data()[7],
                            // "Night": DrugPrescriptionTable.row(i).data()[8],

                            "Duration": DrugPrescriptionTable.row(i).data()[8],
                            "qtyPres": DrugPrescriptionTable.row(i).data()[9],
                            "qtyDisp": DrugPrescriptionTable.row(i).data()[10],
                            "prophylaxis": DrugPrescriptionTable.row(i).data()[11]
                        }
                    }
                    console.log(drugPrescriptionArray);
                }
                else {
                    var rowCount = $('#dtlDrugPrescription tbody tr').length;
                    for (var i = 0; i < rowCount; i++) {
                        drugPrescriptionArray[i] = {
                            "DrugId": DrugPrescriptionTable.row(i).data()[0],
                            "BatchId": DrugPrescriptionTable.row(i).data()[1],
                            //"FreqId": DrugPrescriptionTable.row(i).data()[2],
                            "DrugAbbr": DrugPrescriptionTable.row(i).data()[2],
                            //"Dose": DrugPrescriptionTable.row(i).data()[6],
                            "Morning": DrugPrescriptionTable.row(i).data()[5],
                            "Midday": DrugPrescriptionTable.row(i).data()[6],
                            "Evening": DrugPrescriptionTable.row(i).data()[7],
                            "Night": DrugPrescriptionTable.row(i).data()[8],

                            "Duration": DrugPrescriptionTable.row(i).data()[9],
                            "qtyPres": DrugPrescriptionTable.row(i).data()[10],
                            "qtyDisp": DrugPrescriptionTable.row(i).data()[11],
                            "prophylaxis": DrugPrescriptionTable.row(i).data()[12]
                        }
                    }
                }
                    if (!allAbbr.toUpperCase().includes(DrugPrescriptionTable.row(i).data()[2].toUpperCase())) {
                        if (DrugPrescriptionTable.row(i).data()[2] != "")
                            allAbbr += DrugPrescriptionTable.row(i).data()[2] + "/";
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
                var regimenText = regimenText.match(/\(([^)]+)\)/)[1];
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
                if (treatmentProgramName === 'ART' || treatmentProgramName === 'PMTCT') {
                    if (regimenLine === "0") {
                        toastr.error("Error", "Please select the Regimen Line");
                        return;
                    }
                }  

                if (sumAllAbbr !== sumSelectedRegimen && (treatmentProgramName === 'ART' || treatmentProgramName === 'PMTCT') && sumSelectedRegimen < 1500) {
                    toastr.error("Error", "Selected Regimen is not equal to Prescribed Regimen!");
                    return;
                }
                else {
                    $("#btnSavePrescription").attr("disabled", true);
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
                            $("#btnSavePrescription").attr("disabled", true);
                            toastr.success(data.d, "Saved successfully");
                            //$('#pharmacyModal').modal('hide');

                        },
                        error: function (data) {
                            $("#btnSavePrescription").removeAttr("disabled");
                            toastr.error(data.d, "Error");
                        }
                    });
                }
            }
            else {
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
        else {
            toastr.error("Please enter missing fields.");
        }

       }

    function CalculateQtyPrescribed() {
        
        var morning, midday, evening, night;
        if (DosageFrequency.toString() == "1") {
            
            var dose = $("#<%=txtDose.ClientID%>").val();
            var frequencyID = $("#<%=ddlFreq.ClientID%>").find(":selected").val();
                var duration = $("#<%=txtDuration.ClientID%>").val();
            var multiplier = 0;
            if(dose == "")
                dose = "0";
            if(duration == "")
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
          var   midday = $("#<%=txtMidday.ClientID%>").val();
           var  evening = $("#<%=txtEvening.ClientID%>").val();
           var  night = $("#<%=txtNight.ClientID%>").val();

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
                    try{
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
                    catch(err){}
                    

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

    function checkEnrolmentPrescriptionDates()
    {
        var prescriptionDt = $("#txtPrescriptionDate").val();
        if (enrolmentDate != "" && prescriptionDt != "")
        {
            var presDt = Date.parse(prescriptionDt);
            var enrolDt = Date.parse(enrolmentDate);
            if (presDt < enrolDt)
            {
                toastr.error("Prescription date cannot be less than Enrollment date.");
                $("#txtPrescriptionDate").val("");
            }
        }
    }
    

</script>