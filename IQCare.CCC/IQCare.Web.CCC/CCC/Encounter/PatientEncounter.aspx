<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PatientEncounter" EnableEventValidation="false" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="uc" TagName="ucExtruder" %>
<%@ Register Src="~/CCC/UC/ucPharmacyPrescription.ascx" TagPrefix="uc" TagName="ucPharmacyPrescription" %>
<%@ Register Src="~/CCC/UC/ucPatientClinicalEncounter.ascx" TagPrefix="uc" TagName="ucPatientClinicalEncounter" %>
<%@ Register Src="~/CCC/UC/ucPatientLabs.ascx" TagPrefix="uc" TagName="ucPatientLabs" %>




<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
     <div class="col-md-12">
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    </div>
    <div class="col-md-12 col-xs-12">

        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#encounter" aria-controls="encounter" role="tab" data-toggle="tab"><i class="fa fa-exchange fa-lg" aria-hidden="true"></i>Clinical Encounter</a></li>
            <li role="presentation"><a href="#vlTracker" aria-controls="vlTracker" role="tab" data-toggle="tab"><i class="fa fa-line-chart fa-lg" aria-hidden="true"></i>Viraload Tracker</a></li>
            <%--<li role="presentation"><a href="#Laboratory" aria-controls="Laboratory" role="tab" data-toggle="tab"><i class="fa fa-flask fa-lg" aria-hidden="true"></i>Laboratory</a></li>
            <li role="presentation"><a href="#Pharmacy" aria-controls="Pharmacy" role="tab" data-toggle="tab"><i class="fa fa-tint fa-lg" aria-hidden="true"></i>Pharmacy</a></li>--%>
        </ul>
    </div>
    <!-- .col-md-12 -->

    <div class="col-md-12 col-xs-12">

         <div class="tab-content">

            <div role="tabpanel" class="tab-pane active" id="encounter">
               
                <uc:ucPatientClinicalEncounter runat="server" id="ucPatientClinicalEncounter" />
            </div>

            <div role="tabpanel" class="tab-pane fade" id="vlTracker">


                <div class="col-md-6">
                    <div class="col-md-12 bs-callout bs-callout-danger">
                        <h4 class="pull-left"><strong>Pending VL Test(s):</strong> </h4>
                            <div class="col-md-12 form-group ">
                                  <div id ="tblPendingVlScrollable" style="overflow: scroll; height:200px;"">   
                           
                            <table class="table table-striped table-condensed" id="tblVlpending" clientidmode="Static" runat="server">
                                                 <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">VL Test</span></th>
                                                                    <th><span class="text-primary">Test Reason</span></th>
                                                                    <th><span class="text-primary">Test Date</span></th>
                                                                    <th><span class="text-primary">Status</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                    <tbody>                        
                                                </tbody>                  
                                                </table>
                                      </div>
                            </div>
                      </div>
                         <div class="col-md-12 bs-callout bs-callout-info">
                                         <h4 class="pull-left"> <strong>Complete VL Test(s):</strong> </h4> 
                                      <div class="col-md-12 form-group "> 
                                             <div id ="tblCompleteVlScrollable" style="overflow: scroll; height:250px;"">    
                                              <table class="table table-striped table-condensed" id="tblVL" clientidmode="Static" runat="server">
                                                
                                                   <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">VL Test</span></th>
                                                                    <th><span class="text-primary">Test Reason</span></th>
                                                                    <th><span class="text-primary">Test Date</span></th>
                                                                    <th><span class="text-primary">Results</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                  
                                                   <tbody>                        
                                                  </tbody>                  
                                                </table>
                                            </div> 
                                         </div>      
                                    </div>
                         </div>
                      <div class="col-md-6">              
                             <div id="container" style="min-width: 700px; height: 500px; margin: 0"></div> 
                       </div>                                    
            <%--       <div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div> --%>
                      <!-- pw .implementation of viral load tracker line graph here-->
                </div><!-- .viraload tracker-->
    
        </div>
    </div>
   <uc:ucExtruder runat="server" ID="ucExtruder" />
    <div class="modal"  id="AppointmentModal" tabindex="-1" role="dialog" aria-labelledby="Appointmentlabel" aria-hidden="true" clientidmode="Static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content" >
                <div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Appointment Details</h4>
                    </div>

                    <div class="modal-body">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Date</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="datepicker fuelux form-group" id="PersonAppointmentDate">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="AppointmentDate"></asp:TextBox>
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
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left">Service Area</label>
                                    </div>
                                    <div class="col-md-12 pull-right">
                                        <asp:DropDownList runat="server" ID="ServiceArea" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="reason" class="control-label pull-left">Reason</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="reason" class="control-label pull-left">Differentiated Care</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="DifferentiatedCare" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="description" class="control-label pull-left">Description</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="description" CssClass="form-control input-sm" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="status" class="control-label pull-left">Status</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="status" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12">
                            <asp:LinkButton runat="server" ID="btnSaveAppointment" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Appointment </asp:LinkButton>                        
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- ajax begin -->
    <script type="text/javascript">
        var patientId = '<%=PatientId%>';
        var patientMasterVisitId = '<%=PatientMasterVisitId%>';    
       
      
        var jan_vl = "";
        var feb_vl = "";
        var mar_vl = "";
        var apr_vl = "";
        var may_vl = "";
        var jun_vl = "";
        var jul_vl = "";
        var aug_vl = "";
        var sep_vl = "";
        var oct_vl = "";
        var nov_vl = "";
        var dec_vl = "";



        $(document).ready(function () {

           $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetvlTests",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];    ;
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                        // alert(date);

                        table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.ResultValues + '</td></tr>';
                    });

                    $('#tblVL').append(table);
                    $('#tblVL tr:not(:first-child').each(function(idx){
                        $(this).children(":eq(0)").html(idx + 1);
                    });
                    $('#tblCompleteVlScrollable').append(tblVL);
                    $('#tblCompleteVlScrollable').scroll();

                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });

            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetPendingvlTests",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    // console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];    
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                        // alert(date);

                        table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';
                    });

                    $('#tblVlpending').append(table);
                    $('#tblVlpending tr:not(:first-child').each(function(idx){
                        $(this).children(":eq(0)").html(idx + 1);
                    });

                    $('#tblPendingVlScrollable').append(tblVlpending);
                    $('#tblPendingVlScrollable').scroll();
                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });

           
            $('#PersonAppointmentDate').datepicker({
                allowPastDates: false,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#AppointmentDate").change(function () {
                AppointmentCount();
            });

            $('#PersonAppointmentDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function(event,date) {
                AppointmentCount();
            });

            $("#AppointmentDate").val("");
            $("#btnSaveAppointment").click(function () {
                if ($('#AppointmentForm').parsley().validate()) {
                    var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
                    var appDate = $("#<%=AppointmentDate.ClientID%>").val();
                    if (moment('' + appDate + '').isAfter(futureDate)) {
                        toastr.error("Appointment date cannot be set to over 7 months");
                        return false;
                    }
                    checkExistingAppointment();
                } else {
                    return false;
                }
            });

            $("#AddAppointment").click(function () {
                $("#peripheralNeoropathy").prop('required',false);
                $("#rash").prop('required',false);
                $("#hepatotoxicity").prop('required',false);
                $('#AppointmentModal').modal('show');
                $('#AppointmentDate').val('');
            });
            
          
            function getViralLoad() {
                
                //console.log("get viral load  called");
                $.ajax({
                    url: '../WebService/LabService.asmx/GetViralLoad',
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function (response) {
                        //console.log(response.d);
                        var items = response.d;
                        items.forEach(function (item, i) {

                            if (item.Month == 1) {

                                jan_vl = item.ResultValue;
                                   
                            } else if (item.Month == 2) {

                                feb_vl = item.ResultValue;                                   
                           
                           } else if (item.Month == 3) {

                                 mar_vl = item.ResultValue;                                   
                            }
                           else if (item.Month == 4) {

                               apr_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 5) {

                               may_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 6) {

                               jun_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 7) {

                               jul_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 8) {

                               aug_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 9) {

                               sep_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 10) {

                               oct_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 11) {

                               nov_vl = item.ResultValue;                                   
                           }
                           else if (item.Month == 12) {

                               dec_vl = item.ResultValue;                                   
                           }
                        });

                    }

                });
            }
            // function viralLoadGraph() {
            $(function() {
                $.when(getViralLoad()).then(function () {
                    setTimeout(function () {
                        viralLoadGraph();
                    },
                                          2000);
                });
            });
       
         function viralLoadGraph() {
              
               // console.log("encounter viral load  graph called")
                console.log(jan_vl);
                console.log(feb_vl);
                console.log(mar_vl);
                console.log(apr_vl);
                console.log(may_vl);               

                $('#container').highcharts({
                    title: {
                        text: 'Viral Load Trend',
                        x: -20 //center
                    },
                    subtitle: {
                        text: 'VL cp/ml',
                        x: -20
                    },
                    xAxis: {
                        //categories: ['Jan', 'Mar', 'May', 'Jul', 'Sep', 'Nov', 'Dec']
                        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul','Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: 'Viral Load cp/ml'
                        },
                        plotLines: [
                            {
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }
                        ]
                    },
                    tooltip: {
                        valueSuffix: 'cp/ml'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: 'VL',
                            data: [jan_vl, feb_vl, mar_vl, apr_vl, may_vl, jun_vl, jul_vl, aug_vl, sep_vl, oct_vl, nov_vl, dec_vl]
                        }, {
                            name: 'Threshold',
                            data: [1000, 1000, 1000, 1000, 1000, 1000, 1000,1000, 1000, 1000, 1000, 1000]
                        }
                    ]
                });
            };
        });
       
        ////////////////////////////////////End doc ready///////////////////////////////////////////////////////////////////////////////

        function checkExistingAppointment() {
            var patientId = "<%=PatientId%>";
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            jQuery.support.cors = true;
            $.ajax(
            {
                type: "POST",
                url: "../WebService/PatientService.asmx/GetExistingPatientAppointment",
                data: "{'patientId':'" + patientId + "','appointmentDate': '" + appointmentDate + "','serviceAreaId': '" + serviceArea + "','reasonId': '" + reason + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async:false,
                cache: false,
                success: function (response) {
                    if (response.d != null) {
                        toastr.error("Appointment already exists");
                        return false;
                    }
                    addPatientAppointment();
                },
                error: function (msg) {
                    alert(msg.responseText);
                }
            });
        }
        
        function addPatientAppointment() {
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            var description = $("#<%=description.ClientID%>").val();
            var status = $("#<%=status.ClientID%>").val();
            var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
            /*if (status === '') { status = null }*/
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientAppointment",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Appointment saved successfully");
                    resetAppointmentFields();
                },
                error: function (response) {
                    toastr.error(response.d, "Appointment not saved");
                }
            });
        }

        function AppointmentCount() {
            jQuery.support.cors = true;
            var date = $("#<%=AppointmentDate.ClientID%>").val();
            $.ajax(
            {
                type: "POST",
                url: "../WebService/PatientService.asmx/GetPatientAppointmentCount",
                data: "{'date':'" + date + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    var count = response.d;
                    var message = count + " appointment(s) scheduled on the chosen date.";
                    alert(message);
                },

                error: function(msg) {
                    alert(msg.responseText);
                }
            });
        }

        function resetAppointmentFields(parameters) {
            $("#ServiceArea").val("");
            $("#Reason").val("");
            $("#DifferentiatedCare").val("");
            $("#description").val("");
            $("#AppointmentDate").val("");
        }
            

    </script>

</asp:Content>