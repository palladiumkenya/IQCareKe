<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPharmacyPrescription.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPharmacyPrescription" %>

<asp:HiddenField ID="drugID" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="drugAbbr" runat="server" ClientIDMode="Static" />

<div class="col-md-12" style="padding-top: 10px" id="PharmacySection" data-parsley-validate="true" data-show-errors="true">
    <%--<div class="panel panel-info">--%>

        <%--<div class="panel-body">--%>
                <div class="col-md-12">
                    <div class="col-md-12"><hr /></div>
                            <div class="col-md-12">
                                    <div class="col-md-3 form-group">  
                                        <div class="col-md-12"><label class="control-label pull-left">Treatment Program</label></div>
                                        <div class="col-md-12 pull-right">
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm " id="ddlTreatmentProgram" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
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
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm " id="ddlTreatmentPlan" ClientIDMode="Static" onChange="drugSwitchInterruptionReason(this.value);getCurrentRegimen();" data-parsley-min="1" data-parsley-min-message="Value Required" />
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
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left">Dose</label></div>
                                            <div class="col-md-2 pull-left"><label class="control-label pull-left">Frequency</label></div>
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left">Duration</label></div>
                                            <div class="col-md-2 pull-left"><label class="control-label pull-left">Qty Prescribed</label></div>
                                            <div class="col-md-2 pull-left"><label class="control-label pull-left">Qty Dispensed</label></div>
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left">Prophylaxis</label></div>
                                            <div class="col-md-1 pull-left"><label class="control-label pull-left"></label></div>
                                        </div>  
                                        <div class="col-md-12 panel-body">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1"><input type="text" class="form-control input-sm" runat="server" id="txtDose" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlFreq" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" onchange="CalculateQtyPrescribed();"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1"><input type="text" class="form-control input-sm" runat="server" id="txtDuration" ClientIDMode="Static" onkeyup="CalculateQtyPrescribed();" /> </div>
                                            <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="txtQuantityPres" ClientIDMode="Static" /> </div>
                                            <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="txtQuantityDisp" ClientIDMode="Static" onblur="ChkQtyDispensed();" /> </div>
                                            <div class="col-md-1"><input type="checkbox" runat="server" id="chkProphylaxis" ClientIDMode="Static" /> </div>
                                            <div class="col-md-1 pull-left">
                                                <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddDrugs" onclick="AddDrugPrescription();">Add</button>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                <table id="dtlDrugPrescription" class="table table-bordered table-striped" style="width:100%">
                                                    <thead>
                                                        <tr>
                                                            <th><span class="text-primary">DrugId</span></th>
                                                            <th><span class="text-primary">BatchId</span></th>
                                                            <th><span class="text-primary">FreqId</span></th>
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
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm pull-left" ID="txtPrescriptionDate" data-parsley-required="true"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtDateDispensed"></asp:TextBox>
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
                </div>
                                         
            <div class="col-md-12">
                    <div class="col-md-12"><hr/></div>
                    <div class="col-md-4"></div>
                                            
                    <div class="col-md-8">
                    <%--<div class="col-md-2"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-info btn-sm fa fa-plus-circle" OnClick="saveUpdatePharmacy();"> Save Prescription</asp:LinkButton></div>--%>
                        <div class="col-md-2"><button type="button" Class="btn btn-info btn-sm fa fa-plus-circle" onclick="saveUpdatePharmacy();">Save Prescription</button></div>
                        <%--<div class="col-md-2"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-primary btn-sm  fa fa-print"> Print Prescription</asp:LinkButton></div>--%>
                        <%--<div class="col-md-2"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-warning btn-sm fa fa-refresh"> Reset Prescription</asp:LinkButton></div>--%>
                        <div class="col-md-2"><button type="button" Class="btn btn-warning btn-sm fa fa-refresh" onclick="resetPharmacyForm();">Reset Prescription</button></div>
                        <div class="col-md-2"><button type="button" Class="btn btn-danger btn-sm  fa fa-times" data-dismiss="modal">Close Prescription</button></div>
                    </div>
                                             
            </div>
        <%--</div>--%><%-- .panel-body--%>

    <%--</div>--%><%-- .panel--%>

</div><%-- .col-md-12--%>

<script type="text/javascript">
    var pmscm = "<%=PMSCM%>";
    var pmscmSamePointDispense = "<%=PMSCMSAmePointDispense%>";
    var pmscmFlag = "0";
    var prescriptionDate = "<%= this.prescriptionDate %>";
    var dispenseDate = "<%= this.dispenseDate %>";

    if (prescriptionDate == '' || prescriptionDate == '01-Jan-1900')
        prescriptionDate = 0;

    if (dispenseDate == '' || dispenseDate == '01-Jan-1900')
        dispenseDate = 0;
    
    $(document).ready(function () {
        
        //alert(pmscmSamePointDispense);
        if (pmscmSamePointDispense === "PM/SCM With Same point dispense") {
            pmscmFlag = "1";
            drugList(1);
            $("#ddlBatch").prop('disabled', false);
            $("#txtQuantityDisp").prop('disabled', false);
        }
        else if (pmscm === "PM/SCM") {
            drugList(1);
            $("#ddlBatch").prop('disabled', true);
            $("#txtQuantityDisp").prop('disabled', true);
        }
        else {
            drugList(0);
            $("#ddlBatch").prop('disabled', true);
            $("#txtQuantityDisp").prop('disabled', false);
        }

        $('#PrescriptionDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            date: prescriptionDate
        });

        $('#DispenseDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            date: dispenseDate
        });
    });

    $(function () {
        var regExp = /[a-z]/i;
        $('#txtDose').on('keydown keyup', function (e) {
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

      var DrugPrescriptionTable = $('#dtlDrugPrescription').DataTable({
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
                },
                {
                    "targets": [3],
                    "visible": false,
                    "searchable": false
                }
                    ]
            });

           $("#dtlDrugPrescription").on('click',
                '.btnDelete',
                function () {
                    DrugPrescriptionTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();
                });
       
              
           function drugList(pmscm) {
               
               var drugInput = document.getElementById('<%= txtDrugs.ClientID %>');
               var awesomplete = new Awesomplete(drugInput, {
                   minChars: 1
               });
               
               document.getElementById('<%= txtDrugs.ClientID %>').addEventListener('awesomplete-selectcomplete',function(){
                   var result = this.value.split("~");
                   getBatches(result[0]);
                   this.value = result[2];
                   $("#<%=drugID.ClientID%>").val(result[0]);
                   $("#<%=drugAbbr.ClientID%>").val(result[1]);
               });
               
               $.ajax({
                   url: '../WebService/PatientEncounterService.asmx/GetDrugList',
                   type: 'POST',
                   dataType: 'json',
                   data: "{'PMSCM':'" + pmscm + "'}",
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

        DrugPrescriptionTable
                    .clear()
                    .draw();
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

       function drugSwitchInterruptionReason(treatmentPlan)
       {
           var valSelected = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
           if (valSelected === "Continue current treatment" || valSelected === "Select")
           {
                $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', true);
           }
           else{
               $("#<%=ddlSwitchInterruptionReason.ClientID%>").prop('disabled', false);
           }
           
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


        function saveUpdatePharmacy()
        {
            if ($('#PharmacySection').parsley().validate()) {

                var treatmentProgram = $("#<%=ddlTreatmentProgram.ClientID%>").find(":selected").val();
                var periodTaken = $("#<%=ddlPeriodTaken.ClientID%>").find(":selected").val();
                var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").val();
                var treatmentPlanReason = $("#<%=ddlSwitchInterruptionReason.ClientID%>").find(":selected").val();
                var regimenLine = $("#<%=regimenLine.ClientID%>").find(":selected").val();
                var regimen = $("#<%=ddlRegimen.ClientID%>").find(":selected").val();
                var regimenText = $("#<%=ddlRegimen.ClientID%>").find(":selected").text();
                var datePrescribed = $("#txtPrescriptionDate").val();
                var dateDispensed = $("#txtDateDispensed").val();
                
                if (regimen === undefined)
                    regimen = '0';
                

                var allAbbr = "";
                ///////////////////////////////////////////////////////////////////
                var rowCount = $('#dtlDrugPrescription tbody tr').length;
                var drugPrescriptionArray = new Array();
                try {
                    for (var i = 0 ; i < rowCount; i++) {
                        drugPrescriptionArray[i] = {
                            "DrugId": DrugPrescriptionTable.row(i).data()[0],
                            "BatchId": DrugPrescriptionTable.row(i).data()[1],
                            "FreqId": DrugPrescriptionTable.row(i).data()[2],
                            "DrugAbbr": DrugPrescriptionTable.row(i).data()[3],
                            "Dose": DrugPrescriptionTable.row(i).data()[6],
                            "Duration": DrugPrescriptionTable.row(i).data()[8],
                            "qtyPres": DrugPrescriptionTable.row(i).data()[9],
                            "qtyDisp": DrugPrescriptionTable.row(i).data()[10],
                            "prophylaxis": DrugPrescriptionTable.row(i).data()[11]
                        }

                        if (!allAbbr.toUpperCase().includes(DrugPrescriptionTable.row(i).data()[3].toUpperCase())) {
                            if (DrugPrescriptionTable.row(i).data()[3] != "")
                                allAbbr += DrugPrescriptionTable.row(i).data()[3] + "/";
                        }
                    }
                }
                catch (ex) { }
                //////////////////////////////////////////////////////////////////
                allAbbr = allAbbr.replace(/\/$/, "");
                
                var sumAllAbbr = 0;
                var sumSelectedRegimen = 0;
                try {
                    for (var i = 0; i < allAbbr.length; i++) {
                        sumAllAbbr += allAbbr.charCodeAt(i);
                    }
                }
                catch (err) { }

                //try {
                    //var regExp = /\(([^)]+)\)/;
                    //var matches = regExp.exec(regimenText);
                    var selectedRegimen = regimenText.replace(/\+/g, '/').replace(/ /g, '');
                    //alert(rmv);
                    //alert(matches);
                    //var selectedRegimen = matches[1].replace(/ /g, '').replace(/\+/g, '/');
                    //alert(selectedRegimen);
                    for (var i = 0; i < selectedRegimen.length; i++) {
                        sumSelectedRegimen += selectedRegimen.charCodeAt(i);
                    }

                //}
                //catch (err) { alert(err.message) }
           
                
                if (sumAllAbbr > 0) {
                    if (regimenLine == "0")
                    {
                        toastr.error("Error", "Please select the Regimen Line");
                        return;
                    }
                    

                    if (sumAllAbbr != sumSelectedRegimen) {
                        toastr.error("Error", "Selected Regimen is not equal to Prescribed Regimen!");
                        return;
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
                                toastr.success(data.d, "Saved successfully");
                            },
                            error: function (data) {
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
                            toastr.success(data.d, "Saved successfully");
                        },
                        error: function (data) {
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
                    //toastr.error(data.d, "Failed to get Multiplier");
                }
            });


        }

    function getCurrentRegimen() {
        var treatmentPlan = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").text();
        var treatmentPlanId = $("#<%=ddlTreatmentPlan.ClientID%>").find(":selected").val();
        

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

</script>